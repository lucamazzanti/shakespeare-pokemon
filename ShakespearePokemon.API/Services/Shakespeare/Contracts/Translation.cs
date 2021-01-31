using System.Text.Json.Serialization;

namespace ShakespearePokemon.API.Services.Shakespeare.Contracts
{
    public class Translation
    {
        [JsonPropertyName("success")] 
        public Success Success { get; set; } = new() {Total = 0};

        [JsonPropertyName("contents")]
        public Contents Contents { get; set; } = new();
    }
}