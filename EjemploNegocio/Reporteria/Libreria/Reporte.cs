using HelperMaster.NET.DTO;
using HelperMaster.NET.Enum;
using HelperMaster.NET.General;
using System;

namespace EjemploNegocio.Reporteria.Libreria
{
    /// <summary>
    /// Clase que define la usabilidad de generar reportes en archivos PDF.
    /// </summary>
    public partial class Reporte
    {
        /// <summary>
        /// Atributo que guarda o establece el motor de reporteria.
        /// </summary>
        private PDF P { get; set; }
        /// <summary>
        /// Método de ejemplo que permite generar reporte en PDF.
        /// </summary>
        /// <param name="Test">Cadena de entrada con fines docentes.</param>
        /// <returns>TRUE = Reporte construido sin problemas; FALSE = Problemas al consultar el reporte.</returns>
        public Respuesta<byte[]> ConsultarReporte(string Test)
        {
            Respuesta<byte[]> Resp = new Respuesta<byte[]>();

            var ObtPlantilla = Helper.ConsultarArchivoDesdeEnsamblado("ReporteEjemplo.html");
            if (!ObtPlantilla.decision)
            { return Resp.MensajeGenerico(); }

            this.P = new PDF(ObtPlantilla.estructuraDatos);
            this.P.PlantillaPDF = this.P.PlantillaPDF.Replace("[FechaCreacion]", DateTime.Now.ToString());
            var Imagen = Helper.ConsultarArchivoDesdeEnsamblado("001_hola_mundo.jpg");
            if (!Imagen.decision)
            { return Resp.MensajeGenerico(); }

            this.P.PlantillaPDF = this.P.PlantillaPDF.Replace("[IMAGEN]", "data:image/jpg;base64," + Convert.ToBase64String(Imagen.estructuraDatos));
            this.P.PlantillaPDF = this.P.PlantillaPDF.Replace("[TEST]", "Reporte de ejemplo que demuestra la usabilidad " +
                "de otorger reportería a un sistema de información administrativo.");

            this.P.GenerarPDF();

            Resp.estructuraDatos = this.P.ArchivoPDF;

            Resp.decision = true;
            Resp.mensaje = "Completar Reporte.";
            Resp.mensajeDetalle = "Reporte construido sin problemas.";
            Resp.TipoRespuesta = TipoRespuesta.CorrectoNegocio;

            return Resp;
        }
    }
}
