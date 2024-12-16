using System.ComponentModel.DataAnnotations;

namespace PokemonGame.MVC.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        [RegularExpression("Entrenador|Administrador|Enfermera", ErrorMessage = "Rol inválido")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public byte[] Foto { get; set; }
    }
}
