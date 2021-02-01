using System;
using ShakespearePokemon.API.Services.Shakespeare.Contracts;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ShakespearePokemon.API.Services.Shakespeare.Client
{
    public class ShakespeareClient : IShakespeareClient
    {
        private readonly HttpClient _httpClient;

        public ShakespeareClient(HttpClient httpClient, IOptions<ShakespeareClientConfig> config)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ShakespearePokemon.API");

            if (!string.IsNullOrWhiteSpace(config?.Value?.Url))
            {
                _httpClient.BaseAddress = new Uri(config.Value.Url);
            }

            if (!string.IsNullOrWhiteSpace(config?.Value?.AuthenticationToken))
            {
                _httpClient.DefaultRequestHeaders.Add("X-FunTranslations-Api-Secret", config.Value.AuthenticationToken);
            }
        }

        public async Task<string> TranslateASync(string text)
        {
            if (text == null) {
                throw new ArgumentNullException(nameof(text));
            }

            HttpResponseMessage response = await _httpClient.PostAsync("translate/shakespeare.json",  
                new StringContent(JsonSerializer.Serialize(new { text = text }), 
                    Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            await using Stream responseStream = await response.Content.ReadAsStreamAsync();

            Translation translation = await JsonSerializer.DeserializeAsync<Translation>(responseStream) ?? new Translation();

            return translation.Success?.Total > 0 ? translation.Contents?.Translated : text;
        }
    }
}
