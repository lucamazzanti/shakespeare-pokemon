using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Shakespeare;

namespace ShakespearePokemon.Tests.BaseTests
{
    public abstract class WebApiFunctionalTest
    {
        private APIWebApplicationFactory _factory;
        protected HttpClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new APIWebApplicationFactory();
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
