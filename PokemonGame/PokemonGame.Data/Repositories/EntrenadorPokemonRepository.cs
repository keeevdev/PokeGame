using PokemonGame.Data.Model;
using PokemonGame.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGame.Data.Repositories.Implementations
{
    public class EntrenadorPokemonRepository : GenericRepository<EntrenadoresPokemone>, IEntrenadorPokemonRepository
    {
        public EntrenadorPokemonRepository(PokemonGameDBEntities context) : base(context) { }

        public IEnumerable<EntrenadoresPokemone> GetPokemonesByEntrenadorId(int entrenadorId)
        {
            return _dbSet.Where(e => e.UsuarioId == entrenadorId).ToList();
        }
    }
}

