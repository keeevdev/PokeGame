using System;
using System.Linq;
using System.Web.Mvc;
using PokemonGame.Data;
using PokemonGame.Data.Model;
using PokemonGame.Data.Services;
using PokemonGame.MVC.Models.ViewModels;

namespace PokemonGame.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AccountController()
        {
            var context = new PokemonGameDBEntities();
            _usuarioService = new UsuarioService(context);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _usuarioService.Login(model.NombreUsuario, model.Password);
                if (user != null)
                {
                    Session["UsuarioId"] = user.UsuarioId;
                    Session["NombreUsuario"] = user.NombreUsuario;
                    Session["Rol"] = user.Rol;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PokemonGameDBEntities())
                {
                    if (context.Usuarios.Any(u => u.NombreUsuario == model.NombreUsuario))
                    {
                        ModelState.AddModelError("", "El nombre de usuario ya existe.");
                        return View(model);
                    }
                }

                _usuarioService.RegistrarUsuario(model.NombreUsuario, model.Password, model.Rol, model.Nombre, model.Foto);
                return RedirectToAction("Login");
            }
            return View(model);
        }
    }
}

