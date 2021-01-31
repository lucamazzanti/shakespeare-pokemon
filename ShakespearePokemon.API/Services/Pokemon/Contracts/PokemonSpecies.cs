using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShakespearePokemon.API.Services.Pokemon.Contracts
{
    public class PokemonSpecies
    {
        public PokemonSpecies()
        {
            FlavorTextEntries = new List<FlavorTextEntry>();
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }
    }
}
