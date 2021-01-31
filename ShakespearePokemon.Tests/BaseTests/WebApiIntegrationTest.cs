﻿using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Shakespeare;

namespace ShakespearePokemon.Tests.BaseTests
{
    public abstract class WebApiIntegrationTest
    {
        private APIWebApplicationFactory _factory;
        protected HttpClient Client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new APIWebApplicationFactory(services =>
            {
                // mocking external services
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IPokemonService)));
                services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IShakespeareService)));

                // actually remains the same because are fakes
                services.AddScoped<IPokemonService, PokemonService>();
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
