using System.Threading.Tasks;
using ShakespearePokemon.API.Services.Shakespeare;

namespace ShakespearePokemon.Tests.Common.Mocks
{
    public class ShakespeareServiceStub : IShakespeareService
    {
        public Task<string> TranslateASync(string text)
        {
            if (text == "Charizard flies around the sky in search of powerful opponents. " +
                "It breathes fire of such great heat that it melts anything. " +
                "However, it never turns its fiery breath on any opponent weaker than itself.")
            {
                return Task.FromResult("Charizard flies 'round the sky in search of powerful opponents. " +
                       "'t breathes fire of such most wondrous heat yond 't melts aught. " +
                       "However, 't nev'r turns its fiery breath on any opponent weaker than itself.");
            }

            return Task.FromResult<string>(null);
        }
    }
}
