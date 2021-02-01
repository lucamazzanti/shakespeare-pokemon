using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using ShakespearePokemon.API.Services.ShakespearePokemon;
using ShakespearePokemon.Tests.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShakespearePokemon.Tests.Integration
{
    // Integration tests on the ShakespearePokemon.API controller. All other services are mocked to test the middleware.
    public class PokemonControllerTest : IntegrationTest
    {
        private WebApplicationFactory _webApplicationFactory;
        protected HttpClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // setup a mock of IShakespearePokemonService used by the controller, it has only a single scenario with "charizard"
            var shakespearePokemonServiceMock = new Mock<IShakespearePokemonService>();
            shakespearePokemonServiceMock.Setup(i => i.GetPokemonAsync(It.Is<string>(value => value == "charizard")))
                .ReturnsAsync(new ShakespearePokemonDescription
                {
                    Name = "charizard",
                    Description = "Charizard flies 'round the sky in search of powerful opponents. " +
                                  "'t breathes fire of such most wondrous heat yond 't melts aught. " +
                                  "However,  't nev'r turns its fiery breath on any opponent weaker than itself."
                });

            _webApplicationFactory = new WebApplicationFactory(services =>
            {
                // mocking business services used by the controller to test the middleware
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IShakespearePokemonService)));
                services.AddScoped(_ => shakespearePokemonServiceMock.Object);
            });
            Client = _webApplicationFactory.CreateClient();
        }

        [Test]
        [Description("When called /pokemon/{name} on an existent species it returns a correct response.")]
        public async Task GetPokemon_ReturnsOK_GivenExistentName()
        {
            HttpResponseMessage result = await Client.GetAsync("/pokemon/charizard");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        [Description("When called /pokemon/{name} on an unknown species it returns an error 404.")]
        public async Task GetPokemon_ReturnsNotFound_GivenUnknownName()
        {
            HttpResponseMessage result = await Client.GetAsync("/pokemon/gandalf");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        [Description("When called /pokemon/{name} with empty spaces it returns an error 400.")]
        public async Task GetPokemon_ReturnsBadRequest_GivenEmptyName()
        {
            HttpResponseMessage result = await Client.GetAsync("/pokemon/%20%20%20%20%20");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        [Description("Limiting to a max number of characters the parameter name.")]
        public async Task GetPokemon_ReturnsBadRequest_GivenTooLongName()
        {
            HttpResponseMessage result = await Client.GetAsync("/pokemon/xxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

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

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
            _webApplicationFactory.Dispose();
        }
    }
}
