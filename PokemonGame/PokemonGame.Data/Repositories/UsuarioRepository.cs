using PokemonGame.Data.Model;
using PokemonGame.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PokemonGame.Data.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PokemonGameDBEntities _context;

        public UsuarioRepository(PokemonGameDBEntities context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public Usuario GetByNombreUsuario(string nombreUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }

        public void Add(Usuario user)
        {
            _context.Usuarios.Add(user);
        }

        public void Update(Usuario user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var user = _context.Usuarios.Find(id);
            if (user != null)
            {
                _context.Usuarios.Remove(user);
            }
        }
    }
}

