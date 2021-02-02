using Polly;
using Polly.Caching;
using Polly.Registry;
using System;
using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.Pokemon
{
    /// <summary>
    /// Cache decorator for an IPokemonService
    /// </summary>
    public class CachedPokemonService : IPokemonService
    {
        private readonly IPokemonService _pokemonService;
        private readonly AsyncPolicy _cachePolicy;

        public CachedPokemonService(IPokemonService pokemonService, IReadOnlyPolicyRegistry<string> policies)
        {
            _pokemonService = pokemonService ?? throw new ArgumentNullException(nameof(pokemonService));
            _cachePolicy = policies.Get<AsyncCachePolicy>("pokemon-cache");
        }

        public async Task<PokemonDescription> GetPokemonDescriptionAsync(string name)
        {
            // use the cache policy registered as pokemon-cache in the boostrap
            // it's a read-through cache, in memory, with a sliding window time to live of x minutes
            // it doesn't store default values, so null results are correctly not stored
            return await _cachePolicy.ExecuteAsync(context => 
                _pokemonService.GetPokemonDescriptionAsync(name), 
                new Context(name));
        }
    }
}
