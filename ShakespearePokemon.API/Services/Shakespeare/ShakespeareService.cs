using System.Threading.Tasks;
using ShakespearePokemon.API.Services.Shakespeare.Client;

namespace ShakespearePokemon.API.Services.Shakespeare
{
    public class ShakespeareService : IShakespeareService
    {
        private readonly IShakespeareClient _client;

        public ShakespeareService(IShakespeareClient client)
        {
            _client = client;
        }

        public async Task<string> TranslateASync(string text)
        {
            return await _client.TranslateASync(text);
        }
    }
}
