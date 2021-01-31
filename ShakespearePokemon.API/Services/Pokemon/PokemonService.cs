using ShakespearePokemon.API.Services.Pokemon.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.Pokemon
{
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;

        public string[] FilteredVersions { get; set; } = { "alpha-sapphire" };

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PokemonDescription> GetPokemonDescriptionAsync(string name)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"pokemon-species/{name}");

            response.EnsureSuccessStatusCode();

            await using Stream responseStream = await response.Content.ReadAsStreamAsync();

            PokemonSpecies result = await JsonSerializer.DeserializeAsync<PokemonSpecies>(responseStream) 
                                    ?? new PokemonSpecies();

            string latestEnglishDescription = result.FlavorTextEntries
                .Where(i => i.Language.Name == "en")
                .Where(i => FilteredVersions?.Length > 0 && FilteredVersions.Contains(i.Version.Name))
                .OrderByDescending(i => i.Version.Url)
                .Select(i => new { i.FlavorText, i.Version.Name })
                .FirstOrDefault()?.FlavorText ?? string.Empty;

            latestEnglishDescription = Regex.Replace(latestEnglishDescription, @"[\r\n\f]", " ");

            return new PokemonDescription
            {
                Name = result.Name,
                Description = latestEnglishDescription
            };
        }
    }
}
