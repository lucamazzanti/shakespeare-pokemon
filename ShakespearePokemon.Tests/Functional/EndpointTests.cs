using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShakespearePokemon.Tests.BaseTests;
using ShakespearePokemon.Tests.Models;

namespace ShakespearePokemon.Tests.Functional
{
    public class EndpointTests : WebApiFunctionalTest
    {
        [Test]
        public async Task GetPokemon_ReturnsShakespeareDescrpition_GivenExistentName()
        {
            var expectedJson = new GetPokemonResult
            {
                Name = "charizard",
                Description = "Charizard flies 'round the sky in search of powerful opponents." +
                              "'t breathes fire of such most wondrous heat yond 't melts aught. " +
                              "However, 't nev'r turns its fiery breath on any opponent weaker than itself."
            };

            HttpResponseMessage result = await Client.GetAsync("/pokemon/charizard");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            string content = await result.Content.ReadAsStringAsync();
            var contentJson = JsonConvert.DeserializeObject<GetPokemonResult>(content);

            Assert.AreEqual(expectedJson.Name, contentJson.Name);
            Assert.AreEqual(expectedJson.Description, contentJson.Description);
        }
    }
}
