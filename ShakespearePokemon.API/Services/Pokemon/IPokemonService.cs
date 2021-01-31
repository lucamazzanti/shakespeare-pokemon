namespace ShakespearePokemon.API.Services.Pokemon
{
    public interface IPokemonService
    {
        PokemonDescription GetPokemonDescription(string name);
    }
}
