using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.ShakespearePokemon
{
    public interface IShakespearePokemonService
    {
        Task<ShakespearePokemonDescription> GetPokemonAsync(string name);
    }
}
