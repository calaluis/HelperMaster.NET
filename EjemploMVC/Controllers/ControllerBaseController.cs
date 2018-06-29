using EjemploNegocio.Reporteria.Libreria;
using System;
using System.Web.Mvc;

namespace EjemploMVC.Controllers
{
    public class ControllerBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["Loading"] = Url.Content("~/Content/Images/ajax-loader.gif");

            base.OnActionExecuting(filterContext);
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // Setear Variables globales de los tipos ViewData o ViewBag.
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            filterContext.Controller.ViewData["Version"] = fvi.FileVersion;

            base.OnResultExecuting(filterContext);
        }

        public ActionResult Reporte(string Codigo, string Datos)
        {
            if (string.IsNullOrEmpty(Codigo))
            {
                return View();
            }
            else
            {
                Reporte Report = new Reporte();
                switch (Codigo)
                {
                    case "ReporteriaEjemplo":
                        var Resultado = Report.ConsultarReporte(Datos);
                        if (Resultado.decision)
                        {
                            return File(Resultado.estructuraDatos, System.Net.Mime.MediaTypeNames.Application.Pdf);
                        }
                        else
                        {
                            return View();
                        }
                    default:
                        return View();
                }
            }
        }
    }
}