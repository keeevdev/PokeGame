using PokemonGame.Data.Model;
using PokemonGame.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGame.Data.Repositories.Implementations
{
    public class MensajeRepository : GenericRepository<Mensaje>, IMensajeRepository
    {
        public MensajeRepository(PokemonGameDBEntities context) : base(context) { }

        public IEnumerable<Mensaje> GetMensajesBetweenUsers(int userId1, int userId2)
        {
            return _dbSet.Where(m => (m.EmisorId == userId1 && m.ReceptorId == userId2)
                                  || (m.EmisorId == userId2 && m.ReceptorId == userId1)).ToList();
        }

        public IEnumerable<Mensaje> GetMensajesByEmisor(int emisorId)
        {
            return _dbSet.Where(m => m.EmisorId == emisorId).ToList();
        }

        public IEnumerable<Mensaje> GetMensajesByReceptor(int receptorId)
        {
            return _dbSet.Where(m => m.ReceptorId == receptorId).ToList();
        }
    }
}

