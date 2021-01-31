using System;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon;
using System.Net.Http;
using System.Threading.Tasks;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.Tests.Common.BaseTests;
using ShakespearePokemon.Tests.Common.Builders;

namespace ShakespearePokemon.Tests.Integration
{
    public class PokemonServiceTests : IntegrationTest
    {
        private HttpClient _httpClient;
        protected IPokemonClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };
            Client = new PokemonClient(_httpClient);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public async Task GetPokemon_ReturnsDescrpition_GivenExistentName()
        {
            PokemonDescription expected = PokemonDescriptionBuilder.BuildCharizardDescription();

            var service = new PokemonService(Client);

            PokemonDescription result = await service.GetPokemonDescriptionAsync("charizard");

            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Description, result.Description);
        }

        [Test]
        public async Task GetPokemon_ReturnsDescrpition_GivenExistentNameWithDifferentCasing()
        {
            PokemonDescription expected = PokemonDescriptionBuilder.BuildCharizardDescription();

            var service = new PokemonService(Client);

            PokemonDescription result = await service.GetPokemonDescriptionAsync("cHaRiZaRd");

            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Description, result.Description);
        }
    }
}
