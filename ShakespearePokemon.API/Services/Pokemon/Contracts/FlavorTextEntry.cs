using System.Text.Json.Serialization;

namespace ShakespearePokemon.API.Services.Pokemon.Contracts
{
    public class FlavorTextEntry
    {
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; } = new();

        [JsonPropertyName("version")]
        public Version Version { get; set; } = new();

    }
}
