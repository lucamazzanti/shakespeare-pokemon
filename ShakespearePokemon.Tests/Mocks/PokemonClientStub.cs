using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using Version = ShakespearePokemon.API.Services.Pokemon.Contracts.Version;

namespace ShakespearePokemon.Tests.Mocks
{
    public class PokemonClientStub : IPokemonClient
    {
        public Task<PokemonSpecies> GetPokemonSpeciesAsync(string species)
        {
            if (string.Equals("charizard", species, StringComparison.CurrentCultureIgnoreCase))
            {
                return Task.FromResult(new PokemonSpecies
                {
                    Name = "charizard",
                    FlavorTextEntries = new List<FlavorTextEntry>
                    {
                        new()
                        {
                            FlavorText = "Charizard flies around the sky in search of powerful opponents. " +
                                         "It breathes fire of such great heat that it melts anything. " +
                                         "However, it never turns its fiery breath on any opponent weaker than itself.",
                            Language = new Language { Name = "en" },
                            Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/" }
                        },
                    }
                });
            }

            return Task.FromResult<PokemonSpecies>(null);
        }
    }
}