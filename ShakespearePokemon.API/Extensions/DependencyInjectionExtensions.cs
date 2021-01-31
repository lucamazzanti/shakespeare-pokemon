using Polly;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Shakespeare;
using ShakespearePokemon.API.Services.ShakespearePokemon;
using System;
using ShakespearePokemon.API.Services.Shakespeare.Client;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<IPokemonClient, PokemonClient>(c =>
                {
                    c.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            
            services.AddHttpClient<IShakespeareClient, ShakespeareClient>(c =>
                {
                    c.BaseAddress = new Uri("https://api.funtranslations.com/");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<IShakespeareService, ShakespeareService>();
            services.AddScoped<IShakespearePokemonService, ShakespearePokemonService>();
            
            return services;
        }
    }
}
