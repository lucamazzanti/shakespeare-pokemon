using System.Text.Json.Serialization;

namespace ShakespearePokemon.API.Services.Pokemon.Contracts
{
    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
