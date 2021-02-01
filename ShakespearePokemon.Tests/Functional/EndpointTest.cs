using Newtonsoft.Json;
using NUnit.Framework;
using ShakespearePokemon.Tests.Common;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShakespearePokemon.Tests.Functional
{
    // All the functional tests of ShakespearePokemon.API Web application
    public class EndpointTest : FunctionalTest
    {
        [Test]
        [Description("When called /pokemon/{name} on an existent species it returns the name an description in Shakespeare words.")]
        public async Task GetPokemon_ReturnsShakespeareDescrpition_GivenExistentName()
        {
            var expectedJson = new GetPokemonResult
            {
                Name = "charizard",
                Description = "Charizard flies 'round the sky in search of powerful opponents. " +
                              "'t breathes fire of such most wondrous heat yond 't melts aught. " +
                              "However,  't nev'r turns its fiery breath on any opponent weaker than itself."
            };

            HttpResponseMessage result = await Client.GetAsync("/pokemon/charizard");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            string content = await result.Content.ReadAsStringAsync();
            var contentJson = JsonConvert.DeserializeObject<GetPokemonResult>(content);

            Assert.AreEqual(expectedJson.Name, contentJson.Name);
            Assert.AreEqual(expectedJson.Description, contentJson.Description);
        }

        [Test]
        [Description("When called /pokemon/{name} on an unknown species it returns an error 404.")]
        public async Task GetPokemon_ReturnsNotFound_GivenUnknownName()
        {
            HttpResponseMessage result = await Client.GetAsync("/pokemon/gandalf");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
