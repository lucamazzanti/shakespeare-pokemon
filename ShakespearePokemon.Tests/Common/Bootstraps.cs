using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

/*
 *  Here all the classes that help starting up a context for test.
 */

namespace ShakespearePokemon.Tests.Common
{
    /// <summary>
    /// In memory startup of the ShakespearePokemon.API web application.
    /// </summary>
    public class WebApplicationFactory : WebApplicationFactory<API.Startup>
    {
        private readonly Action<IServiceCollection> _configureServies;

        /// <summary>
        /// In memory startup of the ShakespearePokemon.API web application.
        /// </summary>
        /// <param name="configureServies">A callback to configure the services after the original configuration.</param>
        public WebApplicationFactory(Action<IServiceCollection> configureServies = null)
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
