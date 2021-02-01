using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Version = ShakespearePokemon.API.Services.Pokemon.Contracts.Version;

namespace ShakespearePokemon.API.Services.Pokemon
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonClient _client;

        public string[] FilteredVersions { get; set; } =
        {
            "red","blue","yellow","gold","silver","ruby",
            "sapphire","diamond","pearl","omega-ruby","alpha-sapphire"
        };

        public PokemonService(IPokemonClient client)
        {
            _client = client;
        }

        public async Task<PokemonDescription> GetPokemonDescriptionAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            PokemonSpecies pokemonSpecies = await _client.GetPokemonSpeciesAsync(name);

            if (pokemonSpecies == null)
            {
                return null;
            }

            string description = ExtractDescription(pokemonSpecies);

            description = ReplaceSpecialCharactersWithSpaces(description);

            return new PokemonDescription
            {
                Name = pokemonSpecies.Name,
                Description = description
            };
        }

        public string ExtractDescription(PokemonSpecies species)
        {
            return species.FlavorTextEntries
                // get only the english flavor text
                .Where(i => i.Language.Name == "en")
                // filter version when white list specified
                .Where(i => FilteredVersions?.Length > 0 && FilteredVersions.Contains(i.Version.Name))
                // order by number version desc
                .OrderByDescending(i => ExtractVersionNumber(i.Version))
                // extract the flavor text
                .Select(i => i.FlavorText)
                // or returns a string empty
                .FirstOrDefault() ?? string.Empty;
        }

        public static int ExtractVersionNumber(Version version)
        {
            return Convert.ToInt32(new Uri(version.Url).Segments.Last().Replace("/", ""));
        }

        public static string ReplaceSpecialCharactersWithSpaces(string text)
        {
            return Regex.Replace(text, "(\r\n)|[\r\n\f\t]", " ");
        }
    }
}
