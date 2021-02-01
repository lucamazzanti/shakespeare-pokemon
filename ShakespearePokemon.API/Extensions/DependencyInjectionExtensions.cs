using Polly;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Shakespeare;
using ShakespearePokemon.API.Services.ShakespearePokemon;
using System;
using Microsoft.Extensions.Configuration;
using ShakespearePokemon.API.Services.Shakespeare.Client;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Pokemon API service
            services.Configure<PokemonClientConfig>(configuration.GetSection(PokemonClientConfig.ConfigPath));
            services.AddHttpClient<IPokemonClient, PokemonClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            services.AddScoped<IPokemonService, PokemonService>();

            // Shakespeare API service
            services.Configure<ShakespeareClientConfig>(configuration.GetSection(ShakespeareClientConfig.ConfigPath));
            services.AddHttpClient<IShakespeareClient, ShakespeareClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            services.AddScoped<IShakespeareService, ShakespeareService>();

            // Pokemon Shakespeare service
            services.AddScoped<IShakespearePokemonService, ShakespearePokemonService>();
            
            return services;
        }
    }
}
