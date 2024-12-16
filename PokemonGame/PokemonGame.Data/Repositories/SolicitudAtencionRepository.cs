using PokemonGame.Data.Model;
using PokemonGame.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGame.Data.Repositories.Implementations
{
    public class SolicitudAtencionRepository : GenericRepository<SolicitudesAtencion>, ISolicitudAtencionRepository
    {
        public SolicitudAtencionRepository(PokemonGameDBEntities context) : base(context) { }

        public IEnumerable<SolicitudesAtencion> GetByUsuarioId(int usuarioId)
        {
            return _dbSet.Where(s => s.UsuarioId == usuarioId).ToList();
        }

        public IEnumerable<SolicitudesAtencion> GetPendientes()
        {
            return _dbSet.Where(s => s.Estado == "Pendiente").ToList();
        }
    }
}

