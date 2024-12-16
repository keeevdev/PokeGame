using PokemonGame.Data.Model;
using PokemonGame.Data.Repositories.Implementations;
using PokemonGame.Data.Repositories.Interfaces;

namespace PokemonGame.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PokemonGameDBEntities _context;

        public IUsuarioRepository Usuarios { get; private set; }
        public IPokemonRepository Pokemones { get; private set; }
        public IEntrenadorPokemonRepository EntrenadoresPokemones { get; private set; }
        public IMensajeRepository Mensajes { get; private set; }
        public IPokedexRepository Pokedex { get; private set; }
        public ISolicitudAtencionRepository SolicitudesAtencion { get; private set; }

        public UnitOfWork(PokemonGameDBEntities context)
        {
            _context = context;
            Usuarios = new UsuarioRepository(context);
            Pokemones = new PokemonRepository(context);
            EntrenadoresPokemones = new EntrenadorPokemonRepository(context);
            Mensajes = new MensajeRepository(context);
            Pokedex = new PokedexRepository(context);
            SolicitudesAtencion = new SolicitudAtencionRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}

