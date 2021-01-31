using System;

namespace ShakespearePokemon.API.Services.Pokemon
{
    public class PokemonService : IPokemonService
    {
        public PokemonDescription GetPokemonDescription(string name)
        {
            if (string.Equals("charizard", name, StringComparison.CurrentCultureIgnoreCase))
            {
                return new PokemonDescription
                {
                    Name = "charizard",
                    Description = "CHARIZARD flies around the sky in search of powerful opponents." +
                                  "It breathes fire of such great heat that it melts anything. " +
                                  "However, it never turns its fiery breath on any opponent weaker than itself."
                };
            }

            return null;
        }
    }
}
