using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Pokemon.Contracts;
using ShakespearePokemon.Tests.Common.BaseTests;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShakespearePokemon.Tests.Integration
{
    public class PokemonClientTests : IntegrationTest
    {
        private HttpClient _httpClient;
        private PokemonClient _pokemonClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _httpClient = new HttpClient();
            var pokemonClientConfig = Configuration.GetSection(PokemonClientConfig.ConfigPath).Get<PokemonClientConfig>();
            _pokemonClient = new PokemonClient(_httpClient, Options.Create(pokemonClientConfig));
        }

        [Test]
        public void GetPokemonSpeciesAsync_ThrowsArgumentNullException_GivenANullText()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _pokemonClient.GetPokemonSpeciesAsync(null));
        }

        [Test]
        public async Task GetPokemonSpeciesAsync_ReturnsPokemonSpecies_GivenExistentName()
        {
            PokemonSpecies pokemonSpecies = await _pokemonClient.GetPokemonSpeciesAsync("magikarp");

            Assert.IsNotNull(pokemonSpecies);
            Assert.AreEqual("magikarp", pokemonSpecies.Name);
            Assert.IsNotEmpty(pokemonSpecies.FlavorTextEntries);

            FlavorTextEntry redVersion = pokemonSpecies.FlavorTextEntries
                .SingleOrDefault(i => i.Language.Name == "en" && i.Version.Name == "red");

            Assert.IsNotNull(redVersion, "magikarp english red version not found");
            Assert.AreEqual("https://pokeapi.co/api/v2/version/1/", redVersion.Version.Url);
            Assert.AreEqual("In the distant\npast, it was\nsomewhat stronger\fthan the horribly\nweak descendants\nthat exist today.", redVersion.FlavorText);
        }

        [Test]
        public async Task GetPokemonSpeciesAsync_ReturnsPokemonSpecies_GivenExistentNameWithDifferentCasing()
        {
            PokemonSpecies pokemonSpecies = await _pokemonClient.GetPokemonSpeciesAsync("cHaRiZaRd");

            Assert.IsNotNull(pokemonSpecies);
            Assert.AreEqual("charizard", pokemonSpecies.Name);
            Assert.IsNotEmpty(pokemonSpecies.FlavorTextEntries);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _httpClient.Dispose();
        }
    }
}
