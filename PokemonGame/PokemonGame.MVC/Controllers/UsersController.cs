using System.Linq;
using System.Web.Mvc;
using PokemonGame.Data;
using PokemonGame.Data.UnitOfWork;
using PokemonGame.MVC.Filters;
using System.Security.Cryptography;
using System.Text;
using PokemonGame.Data.Model;

namespace PokemonGame.MVC.Controllers
{
    [AuthorizeRole("Administrador")]
    public class UsersController : Controller
    {
        private readonly UnitOfWork _uow;

        public UsersController()
        {
            _uow = new UnitOfWork(new PokemonGameDBEntities());
        }

        public ActionResult Index()
        {
            var users = _uow.Usuarios.GetAll().ToList();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuario user, string passwordSinHash)
        {
            if (ModelState.IsValid)
            {
                user.ContraseñaHasheada = CalcularHash(passwordSinHash);
                _uow.Usuarios.Add(user);
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            var user = _uow.Usuarios.GetById(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Usuario user)
        {
            if (ModelState.IsValid)
            {
                _uow.Usuarios.Update(user);
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            var user = _uow.Usuarios.GetById(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.Usuarios.Delete(id);
            _uow.Save();
            return RedirectToAction("Index");
        }

        private string CalcularHash(string input)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha.ComputeHash(bytes);
                return System.Convert.ToBase64String(hash);
            }
        }
    }
}
