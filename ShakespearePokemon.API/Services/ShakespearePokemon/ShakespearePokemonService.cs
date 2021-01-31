using System;

namespace ShakespearePokemon.API.Services.ShakespearePokemon
{
    public class ShakespearePokemonService : IShakespearePokemonService
    {
        public Pokemon GetPokemon(string name)
        {
            if (string.Equals("charizard", name, StringComparison.CurrentCultureIgnoreCase))
            {
                return new Pokemon
                {
                    Name = "charizard",
                    Description = "Charizard flies 'round the sky in search of powerful opponents." +
                                  "'t breathes fire of such most wondrous heat yond 't melts aught. " +
                                  "However, 't nev'r turns its fiery breath on any opponent weaker than itself."
                };
            }

            return null;
        }
    }
}
