using System;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ShakespearePokemon.API.Services.Pokemon.Client
{
    public class PokemonClient : IPokemonClient
    {
        private readonly HttpClient _httpClient;

        public PokemonClient(HttpClient httpClient, IOptions<PokemonClientConfig> config)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ShakespearePokemon.API");

            if (!string.IsNullOrWhiteSpace(config?.Value?.Url))
            {
                _httpClient.BaseAddress = new Uri(config.Value.Url);
            }
        }

        public async Task<PokemonSpecies> GetPokemonSpeciesAsync(string species)
        {
            if (species == null)
            {
                throw new ArgumentNullException(nameof(species));
            }

            // fix case sensitive resource in API
            HttpResponseMessage response = await _httpClient.GetAsync($"pokemon-species/{species.ToLower()}");

            // a Not Found is a status error we can accept more, we can return a default
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            await using Stream responseStream = await response.Content.ReadAsStreamAsync();

            PokemonSpecies pokemonSpecies = await JsonSerializer.DeserializeAsync<PokemonSpecies>(responseStream) ?? new PokemonSpecies();

            return pokemonSpecies;
        }
    }
}
