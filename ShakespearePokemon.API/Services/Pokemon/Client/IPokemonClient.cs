using ShakespearePokemon.API.Services.Pokemon.Contracts;
using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.Pokemon.Client
{
    public interface IPokemonClient
    {
        Task<PokemonSpecies> GetPokemonSpeciesAsync(string species);
    }
}