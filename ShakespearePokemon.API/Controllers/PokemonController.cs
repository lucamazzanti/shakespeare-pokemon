﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShakespearePokemon.API.Models;
using ShakespearePokemon.API.Services.ShakespearePokemon;

namespace ShakespearePokemon.API.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IShakespearePokemonService _shakespearePokemonService;

        public PokemonController(IShakespearePokemonService shakespearePokemonService)
        {
            _shakespearePokemonService = shakespearePokemonService;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpGet("{name}")]
        public async Task<ActionResult<PokemonViewModel>> GetPokemonAsync([FromRoute][Required][MaxLength(20)] string name)
        {
            ShakespearePokemonDescription result = await _shakespearePokemonService.GetPokemonAsync(name);

            if (result == null)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound, 
                    detail: $"PokemonViewModel name '{name}' not found");
            }

            return new PokemonViewModel
            {
                Name = result.Name,
                Description = result.Description
            };
        }
    }
}
