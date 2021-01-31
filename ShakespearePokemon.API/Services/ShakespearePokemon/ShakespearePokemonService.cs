using System;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Shakespeare;

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
        public ShakespearePokemonDescription GetPokemon(string name)
        {
            PokemonDescription pokemonDescription = _pokemonService.GetPokemonDescription(name);

            if (pokemonDescription == null) return null;

            string translation = _shakespeareService.Translate(pokemonDescription.Description);

            return new ShakespearePokemonDescription
            {
                Name = pokemonDescription.Name,
                Description = translation ?? pokemonDescription.Description
            };
        }
    }
}
