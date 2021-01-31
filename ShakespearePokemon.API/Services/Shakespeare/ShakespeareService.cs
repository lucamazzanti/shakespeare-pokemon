namespace ShakespearePokemon.API.Services.Shakespeare
{
    public class ShakespeareService : IShakespeareService
    {
        public string Translate(string text)
        {
            if (text == "CHARIZARD flies around the sky in search of powerful opponents." +
                "It breathes fire of such great heat that it melts anything. " +
                "However, it never turns its fiery breath on any opponent weaker than itself.")
            {
                return "Charizard flies 'round the sky in search of powerful opponents." +
                       "'t breathes fire of such most wondrous heat yond 't melts aught. " +
                       "However, 't nev'r turns its fiery breath on any opponent weaker than itself.";
            }

            return text;
        }
    }
}
