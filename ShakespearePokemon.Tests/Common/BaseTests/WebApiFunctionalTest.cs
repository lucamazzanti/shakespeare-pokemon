using System.Net.Http;
using NUnit.Framework;
using ShakespearePokemon.Tests.Common.Bootstrap;

namespace ShakespearePokemon.Tests.Common.BaseTests
{
    public abstract class WebApiFunctionalTest : FunctionalTest
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
