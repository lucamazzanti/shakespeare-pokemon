using System.Text.Json.Serialization;

namespace ShakespearePokemon.API.Services.Shakespeare.Contracts
{
    public class Success
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}