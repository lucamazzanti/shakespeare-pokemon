using Polly;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Shakespeare;
using ShakespearePokemon.API.Services.ShakespearePokemon;
using System;

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
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<IShakespeareService, ShakespeareService>();
            services.AddScoped<IShakespearePokemonService, ShakespearePokemonService>();
            
            return services;
        }
    }
}
