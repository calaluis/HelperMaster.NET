using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMaster.NET.General
{
    /// <summary>
    /// Clase que define los métodos de conversión de datos complementarios a los que 
    /// trae .NET Framework.
    /// </summary>
    public class Conversion
    {
        /// <summary>
        /// Método que permite convertir desde un string a un decimal.
        /// </summary>
        /// <param name="Dato">El dato de tipo string a convertir.</param>
        /// <param name="ValorDefecto">El valor por defecto en caso que la conversión falle. 
        /// Por defecto es cero.</param>
        /// <returns></returns>
        public static decimal StringToDecimal(string Dato, decimal ValorDefecto = 0)
        {
            decimal TempDecimal = 0;
            if(decimal.TryParse(Dato, out TempDecimal))
            {
                return decimal.Parse(Dato);
            }
            return ValorDefecto;
        }
    }
}
