using System.Linq;
using System.Web.Mvc;
using PokemonGame.Data; 
using PokemonGame.Data.Model;
using PokemonGame.Data.UnitOfWork;
using PokemonGame.MVC.Filters;

namespace PokemonGame.MVC.Controllers
{
    [AuthorizeRole("Administrador")]
    public class PokedexController : Controller
    {
        private readonly UnitOfWork _uow;

        public PokedexController()
        {
            _uow = new UnitOfWork(new PokemonGameDBEntities());
        }

        public ActionResult Index()
        {
            var entries = _uow.Pokedex.GetAll().ToList();
            return View(entries);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pokedex model)
        {
            if (ModelState.IsValid)
            {
                _uow.Pokedex.Add(model);
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var entry = _uow.Pokedex.GetById(id);
            if (entry == null) return HttpNotFound();
            return View(entry);
        }

        [HttpPost]
        public ActionResult Edit(Pokedex model)
        {
            if (ModelState.IsValid)
            {
                _uow.Pokedex.Update(model);
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var entry = _uow.Pokedex.GetById(id);
            if (entry == null) return HttpNotFound();
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Pokedex.Delete(id);
            _uow.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var entry = _uow.Pokedex.GetById(id);
            if (entry == null) return HttpNotFound();
            return View(entry);
        }
    }
}
