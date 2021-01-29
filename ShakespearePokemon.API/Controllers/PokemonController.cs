using Microsoft.AspNetCore.Mvc;
using ShakespearePokemon.API.Models;

namespace ShakespearePokemon.API.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        public PokemonController()
        {
 
        }

        [HttpGet("{name}")]
        public Pokemon Get([FromRoute] string name)
        {
            return new Pokemon()
            {
                Name = name,
                Description = $"the pokemon {name} description"
            };
        }
    }
}
