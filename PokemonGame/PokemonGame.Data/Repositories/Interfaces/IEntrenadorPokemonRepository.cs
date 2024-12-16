using PokemonGame.Data.Model;
using System.Collections.Generic;

namespace PokemonGame.Data.Repositories.Interfaces
{
    public interface IEntrenadorPokemonRepository : IGenericRepository<EntrenadoresPokemone>
    {
        IEnumerable<EntrenadoresPokemone> GetPokemonesByEntrenadorId(int entrenadorId);
    }
}

