using PokemonGame.Data.Model;
using System.Collections.Generic;

namespace PokemonGame.Data.Repositories.Interfaces
{
    public interface IMensajeRepository : IGenericRepository<Mensaje>
    {
        IEnumerable<Mensaje> GetMensajesBetweenUsers(int userId1, int userId2);
        IEnumerable<Mensaje> GetMensajesByEmisor(int emisorId);
        IEnumerable<Mensaje> GetMensajesByReceptor(int receptorId);
    }
}

