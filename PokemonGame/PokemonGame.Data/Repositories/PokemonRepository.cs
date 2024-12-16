using PokemonGame.Data.Model;
using PokemonGame.Data.Repositories.Interfaces;

namespace PokemonGame.Data.Repositories.Implementations
{
    public class PokemonRepository : GenericRepository<Pokemone>, IPokemonRepository
    {
        public PokemonRepository(PokemonGameDBEntities context) : base(context) { }
    }
}

