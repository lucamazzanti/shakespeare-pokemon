using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShakespearePokemon.API.Models;
using ShakespearePokemon.API.Services.ShakespearePokemon;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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

        // 1 hour of client caching, see startup configuration for details
        [ResponseCache(CacheProfileName = "one-hour")]
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
                    detail: $"Pokemon name '{name}' not found");
            }

            return new PokemonViewModel
            {
                Name = result.Name,
                Description = result.Description
            };
        }
    }
}
