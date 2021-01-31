using ShakespearePokemon.API.Services.Pokemon.Contracts;
using System.Collections.Generic;

namespace ShakespearePokemon.Tests.Common.Builders
{
    public static class PokemonSpeciesBuilder
    {
        public static PokemonSpecies BuildCharizardPokemonRed()
        {
            return new()
            {
                Name = "charizard",
                FlavorTextEntries = new List<FlavorTextEntry>
                {
                    new()
                    {
                        FlavorText = "Charizard flies around the sky in search of powerful opponents. " +
                                     "It breathes fire of such great heat that it melts anything. " +
                                     "However, it never turns its fiery breath on any opponent weaker than itself.",
                        Language = new Language {Name = "en"},
                        Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/"}
                    },
                }
            };
        }
    }
}
