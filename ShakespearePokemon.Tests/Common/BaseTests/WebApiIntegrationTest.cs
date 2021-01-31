using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Shakespeare;
using ShakespearePokemon.Tests.Common.Bootstrap;
using ShakespearePokemon.Tests.Common.Mocks;

namespace ShakespearePokemon.Tests.Common.BaseTests
{
    public abstract class WebApiIntegrationTest : IntegrationTest
    {
        private APIWebApplicationFactory _factory;
        protected HttpClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new APIWebApplicationFactory(services =>
            {
                // mocking external services
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IPokemonClient)));
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IShakespeareService)));

                services.AddScoped<IPokemonClient, PokemonClientStub>();
                services.AddScoped<IShakespeareService, ShakespeareService>();
            });
            Client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Client.Dispose();
            _factory.Dispose();
        }
    }
}
