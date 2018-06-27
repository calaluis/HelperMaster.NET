﻿using EjemploNegocio.Reporteria.Libreria;
using System.Web.Mvc;

namespace EjemploMVC.Controllers
{
    public class ReporteriaController : ControllerBaseController
    {
        // GET: Reporteria
        public ActionResult ITextSharp()
        {
            Reporte Report = new Reporte();
            var Resp = Report.ConsultarReporte("1, 2, 3, ...... probando....");
            if (Resp.decision)
            {
                return File(Resp.estructuraDatos, System.Net.Mime.MediaTypeNames.Application.Pdf);
            }
            else
            {
                TempData["Mensaje"] = Resp.mensaje;
                return RedirectToAction("Index", "Inicio");
            }
        }
    }
}