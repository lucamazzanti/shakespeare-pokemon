using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using ShakespearePokemon.API.Services.Shakespeare.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Version = ShakespearePokemon.API.Services.Pokemon.Contracts.Version;

namespace ShakespearePokemon.Tests.Common
{
    /// <summary>
    /// This is a testing Stub: it returns a fixed subset of results, if not found it returns null.
    /// </summary>
    public class PokemonClientStub : IPokemonClient
    {
        public static readonly PokemonSpecies CharizardSpecies = new ()
        {
            Name = "charizard",
            FlavorTextEntries = new List<FlavorTextEntry>
            {
                new()
                {
                    FlavorText = "Charizard flies around the sky in search of powerful opponents. " +
                                 "It breathes fire of such great heat that it melts anything. " +
                                 "However, it never turns its fiery breath on any opponent weaker than itself.",
                    Language = new Language {Name = "en"},
                    Version = new Version { Name = "red", Url = "https://pokeapi.co/api/v2/version/1/"}
                },
            }
        };

        public Task<PokemonSpecies> GetPokemonSpeciesAsync(string species)
        {
            if (string.Equals("charizard", species, StringComparison.CurrentCultureIgnoreCase))
            {
                return Task.FromResult(CharizardSpecies);
            }

            return Task.FromResult<PokemonSpecies>(null);
        }
    }

    /// <summary>
    /// This is a testing Stub: it returns a fixed subset of results, if not found it returns null.
    /// </summary>
    public class ShakespeareClientStub : IShakespeareClient
    {
        public Task<string> TranslateASync(string text)
        {
            if (text == "Charizard flies around the sky in search of powerful opponents. " +
                "It breathes fire of such great heat that it melts anything. " +
                "However, it never turns its fiery breath on any opponent weaker than itself.")
            {
                return Task.FromResult("Charizard flies 'round the sky in search of powerful opponents. " +
                       "'t breathes fire of such most wondrous heat yond 't melts aught. " +
                       "However,  't nev'r turns its fiery breath on any opponent weaker than itself.");
            }

            return Task.FromResult<string>(null);
        }
    }
}
