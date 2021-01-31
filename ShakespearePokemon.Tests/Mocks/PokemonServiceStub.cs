using ShakespearePokemon.API.Services.Pokemon;
using System;
using System.Threading.Tasks;

namespace ShakespearePokemon.Tests.Mocks
{
    public class PokemonServiceStub : IPokemonService
    {
        public Task<PokemonDescription> GetPokemonDescriptionAsync(string name)
        {
            if (string.Equals("charizard", name, StringComparison.CurrentCultureIgnoreCase))
            {
                return Task.FromResult(new PokemonDescription
                {
                    Name = "charizard",
                    Description = "Charizard flies around the sky in search of powerful opponents. " +
                                  "It breathes fire of such great heat that it melts anything. " +
                                  "However, it never turns its fiery breath on any opponent weaker than itself."
                });
            }

            return Task.FromResult<PokemonDescription>(null);
        }
    }
}
