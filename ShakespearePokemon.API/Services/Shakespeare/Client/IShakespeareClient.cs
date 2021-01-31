using System.Threading.Tasks;

namespace ShakespearePokemon.API.Services.Shakespeare.Client
{
    public interface IShakespeareClient
    {
        Task<string> TranslateASync(string text);
    }
}
