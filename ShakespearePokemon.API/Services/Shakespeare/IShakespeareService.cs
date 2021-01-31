using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.Shakespeare
{
    public interface IShakespeareService
    {
        Task<string> TranslateASync(string text);
    }
}