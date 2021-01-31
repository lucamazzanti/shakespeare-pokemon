using System;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Shakespeare;
using ShakespearePokemon.API.Services.ShakespearePokemon;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<IPokemonService, PokemonService>(c =>
            {
                c.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                c.DefaultRequestHeaders.Add("User-Agent", "ShakespearePokemon.API");
                c.DefaultRequestHeaders.Add("Accept", "application/json");

            });
            services.AddScoped<IShakespeareService, ShakespeareService>();
            services.AddScoped<IShakespearePokemonService, ShakespearePokemonService>();
            
            return services;
        }
    }
}
