using EjemploMVC.Models.ViewModels;
using HelperMaster.NET.DTO;
using System.Web.Mvc;

namespace EjemploMVC.Controllers
{
    public class InicioController : ControllerBaseController
    {
        // GET: Inicio
        public ActionResult Index()
        {
            SubRespuesta Respuesta = new SubRespuesta();
            InicioVm Datos = new InicioVm();

            ViewData["Mensaje"] = Respuesta;
            return View(Datos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InicioVm Datos, FormCollection Collection)
        {
            SubRespuesta Respuesta = new SubRespuesta();

            ViewData["Mensaje"] = Respuesta;
            return PartialView("_IndexDetalle", Datos);
        }
    }
}