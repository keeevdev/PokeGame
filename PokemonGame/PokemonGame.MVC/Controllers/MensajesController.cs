using System.Linq;
using System.Web.Mvc;
using PokemonGame.Data;
using PokemonGame.Data.Model;
using PokemonGame.Data.UnitOfWork;
using PokemonGame.MVC.Filters;

namespace PokemonGame.MVC.Controllers
{
    [AuthorizeRole("Entrenador")]
    public class MensajesController : Controller
    {
        private readonly UnitOfWork _uow;

        public MensajesController()
        {
            _uow = new UnitOfWork(new PokemonGameDBEntities());
        }

        public ActionResult Index(string withUser = null)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Login", "Account");

            int usuarioId = (int)Session["UsuarioId"];
            if (!string.IsNullOrEmpty(withUser))
            {
                var receptor = _uow.Usuarios.GetAll().FirstOrDefault(u => u.NombreUsuario == withUser);
                if (receptor == null) return View("ErrorUsuarioNoEncontrado");
                var mensajes = _uow.Mensajes.GetMensajesBetweenUsers(usuarioId, receptor.UsuarioId);
                return View(mensajes);
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string receptorNombre, string contenido)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Login", "Account");

            int usuarioId = (int)Session["UsuarioId"];
            var receptor = _uow.Usuarios.GetAll().FirstOrDefault(u => u.NombreUsuario == receptorNombre);
            if (receptor == null)
            {
                ModelState.AddModelError("", "El usuario receptor no existe.");
                return View();
            }

            var mensaje = new Mensaje
            {
                Contenido = contenido,
                EmisorId = usuarioId,
                ReceptorId = receptor.UsuarioId,
                FechaEnvio = System.DateTime.Now
            };

            _uow.Mensajes.Add(mensaje);
            _uow.Save();
            return RedirectToAction("Index", new { withUser = receptorNombre });
        }
    }
}

