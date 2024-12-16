using System.Linq;
using System.Web.Mvc;
using PokemonGame.Data;
using PokemonGame.Data.Model;
using PokemonGame.Data.UnitOfWork;
using PokemonGame.MVC.Filters;

namespace PokemonGame.MVC.Controllers
{
    [AuthorizeRole("Entrenador")]
    public class PokemonController : Controller
    {
        private readonly UnitOfWork _uow;

        public PokemonController()
        {
            _uow = new UnitOfWork(new PokemonGameDBEntities());
        }

        public ActionResult Index()
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Login", "Account");

            int usuarioId = (int)Session["UsuarioId"];
            var misPokemones = _uow.EntrenadoresPokemones
                .GetPokemonesByEntrenadorId(usuarioId)
                .Select(ep => ep.Pokemone)
                .ToList();

            return View(misPokemones);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pokemone model)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _uow.Pokemones.Add(model);
                _uow.Save();
                int usuarioId = (int)Session["UsuarioId"];
                var ep = new EntrenadoresPokemone { UsuarioId = usuarioId, PokemonId = model.PokemonId };
                _uow.EntrenadoresPokemones.Add(ep);
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Login", "Account");

            int usuarioId = (int)Session["UsuarioId"];
            var ep = _uow.EntrenadoresPokemones.Find(e => e.UsuarioId == usuarioId && e.PokemonId == id).FirstOrDefault();
            if (ep == null) return HttpNotFound();
            var pokemon = _uow.Pokemones.GetById(id);
            return View(pokemon);
        }

        [HttpPost]
        public ActionResult Edit(Pokemone model)
        {
            if (ModelState.IsValid)
            {
                _uow.Pokemones.Update(model);
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            if (Session["UsuarioId"] == null)
                return RedirectToAction("Login", "Account");

            int usuarioId = (int)Session["UsuarioId"];
            var ep = _uow.EntrenadoresPokemones.Find(e => e.UsuarioId == usuarioId && e.PokemonId == id).FirstOrDefault();
            if (ep == null) return HttpNotFound();

            var pokemon = _uow.Pokemones.GetById(id);
            return View(pokemon);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            int usuarioId = (int)Session["UsuarioId"];
            var ep = _uow.EntrenadoresPokemones.Find(e => e.UsuarioId == usuarioId && e.PokemonId == id).FirstOrDefault();
            if (ep != null)
            {
                _uow.EntrenadoresPokemones.Delete(ep);
                _uow.Pokemones.Delete(id);
                _uow.Save();
            }
            return RedirectToAction("Index");
        }
    }
}
