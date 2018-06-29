using EjemploMVC.Models.ViewModels;
using System.Web.Mvc;

namespace EjemploMVC.Controllers
{
    public class ReporteriaController : ControllerBaseController
    {
        // GET: Reporteria
        public ActionResult ITextSharp()
        {
            ITextSharpVm Datos = new ITextSharpVm();
            Datos.NombreVistaEjecutora = "Reporte";
            Datos.CodigoReporte = "ReporteriaEjemplo";
            Datos.DatosEntrada = "1, 2, 3, ...... probando....";
            return View(Datos);
        }
    }
}