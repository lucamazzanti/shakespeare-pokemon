using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Shakespeare.Client;
using ShakespearePokemon.Tests.Common.Bootstrap;

namespace ShakespearePokemon.Tests.Common.BaseTests
{
    public abstract class WebApiFunctionalTest : FunctionalTest
    {
        protected static IConfiguration Configuration { get; }

        static WebApiFunctionalTest()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json", false)
                .Build();
        }

        private APIWebApplicationFactory _factory;
        protected HttpClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new APIWebApplicationFactory(services =>
            {
                services.Configure<PokemonClientConfig>(Configuration.GetSection(PokemonClientConfig.ConfigPath));

                services.Configure<ShakespeareClientConfig>(Configuration.GetSection(ShakespeareClientConfig.ConfigPath));

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
