using ShakespearePokemon.API.Services.Shakespeare.Contracts;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.Shakespeare.Client
{
    public class ShakespeareClient : IShakespeareClient
    {
        private readonly HttpClient _httpClient;

        public ShakespeareClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ShakespearePokemon.API");
            //_httpClient.DefaultRequestHeaders.Add("X-FunTranslations-Api-Secret", "secret");
        }

        public async Task<string> TranslateASync(string text)
        {
            if (text == null) return null;

            // GET
            // HttpResponseMessage response = await _httpClient.GetAsync($"translate/shakespeare.json?text={HttpUtility.UrlEncode(text)}");

            // POST
            HttpResponseMessage response = await _httpClient.PostAsync("translate/shakespeare.json",  
                new StringContent(JsonSerializer.Serialize(new { text = text }), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            await using Stream responseStream = await response.Content.ReadAsStreamAsync();

            Translation translation = await JsonSerializer.DeserializeAsync<Translation>(responseStream) ?? new Translation();

            return translation.Success?.Total > 0 ? translation.Contents?.Translated : text;
        }
    }
}