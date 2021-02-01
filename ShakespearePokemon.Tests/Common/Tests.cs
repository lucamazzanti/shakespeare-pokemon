using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Shakespeare.Client;

namespace ShakespearePokemon.Tests.Common
{
    // Useful to filter out tests in a single test project
    public static class TestCategories
    {
        public const string Unit = "Unit";
        public const string Integration = "Integration";
        public const string Functional = "Functional";
    }

    // just a common base class for future emprovements
    public abstract class BaseTest
    {

    }

    // just a common base class for future emprovements
    [Category(TestCategories.Unit)]
    public abstract class UnitTest : BaseTest
    {

    }

    /// <summary>
    /// Integration tests can read the Test Project Configuration.
    /// It reads the test project settings "testsettings.json".
    /// </summary>
    [Category(TestCategories.Integration)]
    public abstract class IntegrationTest : BaseTest
    {
        protected static IConfiguration Configuration { get; }

        static IntegrationTest()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json", false)
                .Build();
        }
    }

    /// <summary>
    /// Functional tests run an in memory version of the application.
    /// The configuration of some services can be overridden with the test project configuration.
    /// It reads the API project settings "appsettings.json", then the test project settings "testsettings.json".
    /// </summary>
    [Category(TestCategories.Functional)]
    public abstract class FunctionalTest : BaseTest
    {
        private static IConfiguration Configuration { get; }

        static FunctionalTest()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testsettings.json", false)
                .Build();
        }

        private WebApplicationFactory _factory;
        protected HttpClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory(services =>
            {
                // override the Pokemon API configuration
                services.Configure<PokemonClientConfig>(Configuration.GetSection(PokemonClientConfig.ConfigPath));

                // override the Shakespeare API configuration
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
