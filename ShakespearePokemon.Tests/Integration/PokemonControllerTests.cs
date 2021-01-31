using NUnit.Framework;
using ShakespearePokemon.Tests.BaseTests;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShakespearePokemon.Tests.Integration
{
    public class PokemonControllerTests : WebApiIntegrationTest
    {
        [Test]
        public async Task GetPokemon_ReturnsOK_GivenExistentName()
        {
            HttpResponseMessage result = await Client.GetAsync("/pokemon/charizard");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetPokemon_ReturnsNotFound_GivenNotFoundName()
        {
            HttpResponseMessage result = await Client.GetAsync("/pokemon/gandalf");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
