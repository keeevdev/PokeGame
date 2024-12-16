using System.Linq;
using System.Web.Mvc;
using PokemonGame.Data;
using PokemonGame.Data.Model;
using PokemonGame.Data.UnitOfWork;
using PokemonGame.MVC.Filters;

namespace PokemonGame.MVC.Controllers
{
    [AuthorizeRole("Enfermera")]
    public class SolicitudesAtencionController : Controller
    {
        private readonly UnitOfWork _uow;

        public SolicitudesAtencionController()
        {
            _uow = new UnitOfWork(new PokemonGameDBEntities());
        }

        public ActionResult Index()
        {
            var pendientes = _uow.SolicitudesAtencion.GetPendientes();
            return View(pendientes.ToList());
        }

        public ActionResult Atender(int id)
        {
            var solicitud = _uow.SolicitudesAtencion.GetById(id);
            if (solicitud == null) return HttpNotFound();
            return View(solicitud);
        }

        [HttpPost, ActionName("Atender")]
        public ActionResult AtenderConfirmed(int id)
        {
            var solicitud = _uow.SolicitudesAtencion.GetById(id);
            if (solicitud != null)
            {
                solicitud.Estado = "Atendida";
                _uow.SolicitudesAtencion.Update(solicitud);
                _uow.Save();
            }
            return RedirectToAction("Index");
        }
    }
}
