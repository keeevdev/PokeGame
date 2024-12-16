using System.ComponentModel.DataAnnotations;

namespace PokemonGame.MVC.Models.ViewModels
{
    public class MensajeViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el nombre de usuario del receptor")]
        public string ReceptorNombre { get; set; }

        [Required(ErrorMessage = "El contenido del mensaje es requerido")]
        public string Contenido { get; set; }
    }
}
