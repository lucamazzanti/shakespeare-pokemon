using System.Text.Json.Serialization;

namespace ShakespearePokemon.API.Services.Pokemon.Contracts
{
    public class Version
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
