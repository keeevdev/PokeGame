using PokemonGame.Data.Model;
using System.Collections.Generic;

namespace PokemonGame.Data.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
        Usuario GetByNombreUsuario(string nombreUsuario);
        void Add(Usuario user);
        void Update(Usuario user);
        void Delete(int id);
    }
}

