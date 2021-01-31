using System;
using System.Threading.Tasks;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.Tests.Common.Builders;

namespace ShakespearePokemon.Tests.Common.Mocks
{
    public class PokemonServiceStub : IPokemonService
    {
        public Task<PokemonDescription> GetPokemonDescriptionAsync(string name)
        {
            if (string.Equals("charizard", name, StringComparison.CurrentCultureIgnoreCase))
            {
                return Task.FromResult(PokemonDescriptionBuilder.BuildCharizardDescription());
            }

            return Task.FromResult<PokemonDescription>(null);
        }
    }
}
