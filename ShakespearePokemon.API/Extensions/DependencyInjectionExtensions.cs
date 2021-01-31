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
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<IShakespeareService, ShakespeareService>();
            services.AddScoped<IShakespearePokemonService, ShakespearePokemonService>();
            
            return services;
        }
    }
}
