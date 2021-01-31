using System;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon;
using System.Net.Http;
using System.Threading.Tasks;
using ShakespearePokemon.API.Services.Pokemon.Client;

namespace ShakespearePokemon.Tests.Integration
{
    [Category("Integration")]
    public class PokemonServiceTests
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
            var expected = new PokemonDescription
            {
                Name = "charizard",
                Description = "Charizard flies around the sky in search of powerful opponents. " +
                              "It breathes fire of such great heat that it melts anything. " +
                              "However, it never turns its fiery breath on any opponent weaker than itself."
            };

            var service = new PokemonService(Client);

            PokemonDescription result = await service.GetPokemonDescriptionAsync("charizard");

            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Description, result.Description);
        }

        [Test]
        public async Task GetPokemon_ReturnsDescrpition_GivenExistentNameWithDifferentCasing()
        {
            var expected = new PokemonDescription
            {
                Name = "charizard",
                Description = "Charizard flies around the sky in search of powerful opponents. " +
                              "It breathes fire of such great heat that it melts anything. " +
                              "However, it never turns its fiery breath on any opponent weaker than itself."
            };

            var service = new PokemonService(Client);

            PokemonDescription result = await service.GetPokemonDescriptionAsync("cHaRiZaRd");

            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Description, result.Description);
        }
    }
}
