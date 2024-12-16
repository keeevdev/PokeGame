using PokemonGame.Data.Model;
using System.Collections.Generic;

namespace PokemonGame.Data.Repositories.Interfaces
{
    public interface ISolicitudAtencionRepository : IGenericRepository<SolicitudesAtencion>
    {
        IEnumerable<SolicitudesAtencion> GetByUsuarioId(int usuarioId);
        IEnumerable<SolicitudesAtencion> GetPendientes();
    }
}

