using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Shakespeare;
using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.ShakespearePokemon
{
    public class ShakespearePokemonService : IShakespearePokemonService
    {
        private readonly IPokemonService _pokemonService;
        private readonly IShakespeareService _shakespeareService;

        public ShakespearePokemonService(IPokemonService pokemonService, IShakespeareService shakespeareService)
        {
            _pokemonService = pokemonService;
            _shakespeareService = shakespeareService;
        }

        public async Task<ShakespearePokemonDescription> GetPokemonAsync(string name)
        {
            PokemonDescription pokemonDescription = await _pokemonService.GetPokemonDescriptionAsync(name);

            if (pokemonDescription == null) return null;

            string translation = await _shakespeareService.TranslateASync(pokemonDescription.Description);

            return new ShakespearePokemonDescription
            {
                Name = pokemonDescription.Name,
                Description = translation ?? pokemonDescription.Description
            };
        }
    }
}
