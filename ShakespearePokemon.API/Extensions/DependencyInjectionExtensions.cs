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

            // uncomment here to test the service without cache, comment the next part
            // services.AddScoped<IPokemonService, PokemonService>();

            // service with cache
            // caching set using a decorator pattern to avoid to spread boilerplate code inside the service
            // (the vanilla dependecy injection is not simple to configure whith a decorator pattern)
            services.AddScoped<PokemonService, PokemonService>();
            services.AddScoped<IPokemonService, CachedPokemonService>(p =>
                new CachedPokemonService(
                    p.GetRequiredService<PokemonService>(), 
                    p.GetRequiredService<IReadOnlyPolicyRegistry<string>>()
                ));

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
