using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;
using Polly.Registry;
using ShakespearePokemon.API.Services.Pokemon;
using ShakespearePokemon.API.Services.Pokemon.Client;
using ShakespearePokemon.API.Services.Shakespeare;
using ShakespearePokemon.API.Services.Shakespeare.Client;
using ShakespearePokemon.API.Services.ShakespearePokemon;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        // Useful registry to store all the resilient rules to request in the application
        public static IServiceCollection AddPollyPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IAsyncCacheProvider, MemoryCacheProvider>();
            services.AddSingleton<IReadOnlyPolicyRegistry<string>, PolicyRegistry>(provider =>
            {
                var policyRegistry = new PolicyRegistry
                {
                    {
                        // A read-through cache, with a sliding window time to live of 5 minutes
                        "pokemon-cache", 
                        Policy.CacheAsync(provider.GetRequiredService<IAsyncCacheProvider>(),
                            new SlidingTtl(TimeSpan.FromMinutes(5)))
                    }
                };
                return policyRegistry;
            });

            return services;
        }

        // all the business services registrations
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPokemonService(configuration)
                .AddShakespeareService(configuration)
                .AddShakespearePokemonService(configuration);

            return services;
        }

        public static IServiceCollection AddPokemonService(this IServiceCollection services, IConfiguration configuration)
        {
            /* features for an http client:
             * .AddPolicyHandlerFromRegistry() can add a policy from the register, for example the cache used in the service, in this scenario doesn't fit well
             * .SetHandlerLifetime() manage the http client pool recycle lifetime
             * .AddTransientHttpErrorPolicy() add retry and circuit breaker policies on network, server, timeout errors
             */
            services.Configure<PokemonClientConfig>(configuration.GetSection(PokemonClientConfig.ConfigPath));
            services.AddHttpClient<IPokemonClient, PokemonClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddScoped<IPokemonService, PokemonService>();

            // caching is set using a decorator pattern to keep the single responsability
            // it also honor the concrecte class name: a pokemon service, and a cached pokemon service
            services.Decorate<IPokemonService, CachedPokemonService>();

            return services;
        }

        public static IServiceCollection AddShakespeareService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ShakespeareClientConfig>(configuration.GetSection(ShakespeareClientConfig.ConfigPath));
            services.AddHttpClient<IShakespeareClient, ShakespeareClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            services.AddScoped<IShakespeareService, ShakespeareService>();

            return services;
        }

        public static IServiceCollection AddShakespearePokemonService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IShakespearePokemonService, ShakespearePokemonService>();

            return services;
        }
    }
}
