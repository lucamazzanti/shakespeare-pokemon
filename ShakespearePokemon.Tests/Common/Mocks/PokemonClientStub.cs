using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using ShakespearePokemon.Tests.Common.Builders;
using System;
using System.Threading.Tasks;

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