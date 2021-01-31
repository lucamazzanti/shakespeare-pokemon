using System.Net.Http;
using NUnit.Framework;

namespace ShakespearePokemon.Tests.BaseTests
{
    public abstract class WebApiIntegrationTest
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
