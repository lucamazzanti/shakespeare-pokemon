using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using ShakespearePokemon.Tests.Common.Builders;
using Version = ShakespearePokemon.API.Services.Pokemon.Contracts.Version;

namespace ShakespearePokemon.Tests.Common.Mocks
{
    public class PokemonClientStub : IPokemonClient
    {
        public Task<PokemonSpecies> GetPokemonSpeciesAsync(string species)
        {
            if (string.Equals("charizard", species, StringComparison.CurrentCultureIgnoreCase))
            {
                return Task.FromResult(PokemonSpeciesBuilder.BuildCharizardPokemonRed());
            }

            return Task.FromResult<PokemonSpecies>(null);
        }
    }
}