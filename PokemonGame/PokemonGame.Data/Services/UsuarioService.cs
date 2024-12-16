using PokemonGame.Data.Model;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PokemonGame.Data.Services
{
    public class UsuarioService
    {
        private readonly PokemonGameDBEntities _context;

        public UsuarioService(PokemonGameDBEntities context)
        {
            _context = context;
        }

        public Usuario Login(string nombreUsuario, string contraseñaSinHash)
        {
            string hashEntrada = CalcularHash(contraseñaSinHash);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.ContraseñaHasheada == hashEntrada);
            return usuario;
        }

        public void RegistrarUsuario(string nombreUsuario, string contraseñaSinHash, string rol, string nombre, byte[] foto)
        {
            string hash = CalcularHash(contraseñaSinHash);

            var nuevoUsuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                ContraseñaHasheada = hash,
                Rol = rol,
                Nombre = nombre,
                Foto = foto
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();
        }

        private string CalcularHash(string input)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}

