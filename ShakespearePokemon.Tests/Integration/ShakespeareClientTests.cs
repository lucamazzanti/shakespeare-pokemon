using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Shakespeare.Client;
using ShakespearePokemon.Tests.Common.BaseTests;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShakespearePokemon.Tests.Integration
{
    public class ShakespeareClientTests : IntegrationTest
    {
        private HttpClient _httpClient;
        private IShakespeareClient _shakespeareClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _httpClient = new HttpClient();
            var shakespeareClientConfig = Configuration.GetSection(ShakespeareClientConfig.ConfigPath).Get<ShakespeareClientConfig>();
            _shakespeareClient = new ShakespeareClient(_httpClient, Options.Create(shakespeareClientConfig));
        }

        [Test]
        public void TranslateASync_ThrowsArgumentNullException_GivenANullText()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _shakespeareClient.TranslateASync(null));
        }

        [Test]
        public async Task TranslateASync_TranslateInShakespeareWords_GivenAnExampleText()
        {
            var expectedText = "Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,  't nev'r turns its fiery breath on any opponent weaker than itself.";
            var text = "Charizard flies around the sky in search of powerful opponents. It breathes fire of such great heat that it melts anything. However, it never turns its fiery breath on any opponent weaker than itself.";
            
            string translation = await _shakespeareClient.TranslateASync(text);
            
            Assert.AreEqual(expectedText, translation);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _httpClient.Dispose();
        }
    }
}
