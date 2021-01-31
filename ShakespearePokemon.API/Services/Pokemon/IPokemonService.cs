using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.Pokemon
{
    public interface IPokemonService
    {
        Task<PokemonDescription> GetPokemonDescriptionAsync(string name);
    }
}
