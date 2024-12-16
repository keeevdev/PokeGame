using System.ComponentModel.DataAnnotations;

namespace PokemonGame.MVC.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contrasena es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
