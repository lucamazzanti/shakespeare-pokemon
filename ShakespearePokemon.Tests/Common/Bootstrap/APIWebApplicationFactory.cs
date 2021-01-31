using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ShakespearePokemon.Tests.Common.Bootstrap
{
    public class APIWebApplicationFactory : WebApplicationFactory<API.Startup>
    {
        private readonly Action<IServiceCollection> _configureServies;

        public APIWebApplicationFactory(Action<IServiceCollection> configureServies = null)
        {
            _configureServies = configureServies;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                _configureServies?.Invoke(services);
            });
        }
    }
}
