using HelperMaster.NET.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploNegocio.Reporteria.Libreria
{
    /// <summary>
    /// Clase que define la usabilidad de generar reportes en archivos PDF.
    /// </summary>
    public partial class Reporte
    {
        /// <summary>
        /// Atributo que guarda o establece los datos del archivo de plantilla en iTextXML.
        /// </summary>
        public string DatosPlantilla { get; set; }
        /// <summary>
        /// Atributo que guarda o establece el motor de reporteria.
        /// </summary>
        private PDF P { get; set; }
    }
}
