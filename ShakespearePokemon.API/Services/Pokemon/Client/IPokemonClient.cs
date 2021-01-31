using System.Net.Http;
using System.Threading.Tasks;
using ShakespearePokemon.API.Services.Pokemon.Contracts;

namespace ShakespearePokemon.API.Services.Pokemon.Client
{
    public interface IPokemonClient
    {
        Task<PokemonSpecies> GetPokemonSpeciesAsync(string species);
    }
}