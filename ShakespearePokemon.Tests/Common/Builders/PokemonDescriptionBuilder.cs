using ShakespearePokemon.API.Services.Pokemon;

namespace ShakespearePokemon.Tests.Common.Builders
{
    public static class PokemonDescriptionBuilder
    {
        public static PokemonDescription BuildCharizardDescription()
        {
            return new()
            {
                Name = "charizard",
                Description = "Charizard flies around the sky in search of powerful opponents. " +
                              "It breathes fire of such great heat that it melts anything. " +
                              "However, it never turns its fiery breath on any opponent weaker than itself."
            };
        }
    }
}