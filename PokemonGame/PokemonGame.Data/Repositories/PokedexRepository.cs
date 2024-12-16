using PokemonGame.Data.Model;
using PokemonGame.Data.Repositories.Interfaces;

namespace PokemonGame.Data.Repositories.Implementations
{
    public class PokedexRepository : GenericRepository<Pokedex>, IPokedexRepository
    {
        public PokedexRepository(PokemonGameDBEntities context) : base(context) { }
    }
}

