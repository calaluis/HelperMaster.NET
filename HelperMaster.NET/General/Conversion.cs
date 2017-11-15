using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperMaster.NET.General
{
    /// <summary>
    /// Clase que define los métodos de conversión de datos complementarios a los que 
    /// trae .NET Framework.
    /// </summary>
    public class Conversion
    {
        /// <summary>
        /// Metodo que permite convertir un string a decimal.
        /// </summary>
        /// <param name="Numero">El numero a convertir.</param>
        /// <param name="NumeroPorDefecto">El numero por defecto en caso que la conversion falle.</param>
        /// <returns>TRUE = El nujmero convertido; FALSE = el numero por defecto.</returns>
        public static decimal StringToDecimal(string Numero, decimal NumeroPorDefecto = 0)
        {
            decimal DecTemp;
            decimal Resp = decimal.TryParse(Numero, out DecTemp)
                ? decimal.Parse(Numero)
                : NumeroPorDefecto;
            return Resp;
        }
        /// <summary>
        /// Metodo que permite convertir un string a DateTime.
        /// </summary>
        /// <param name="FechaStr">La fecha en string.</param>
        /// <param name="FechaPorDefecto">La fecha por defecto en caso que la conversion falle.</param>
        /// <returns>TRUE = La fecha convertida; FALSE = La fecha por defecto.</returns>
        public static DateTime? StringToDateTime(string FechaStr, DateTime? FechaPorDefecto = null)
        {
            DateTime DatTemp;
            DateTime? Resp = DateTime.TryParse(FechaStr, out DatTemp)
                ? DateTime.Parse(FechaStr)
                : FechaPorDefecto;
            return Resp;
        }
        /// <summary>
        /// Metodo que permite convertir un string a double.
        /// </summary>
        /// <param name="Numero">El numero a convertir.</param>
        /// <param name="NumeroPorDefecto">El numero por defecto en caso que la conversion falle.</param>
        /// <returns>TRUE = El nujmero convertido; FALSE = el numero por defecto.</returns>
        public static double StringToDouble(string Numero, double NumeroPorDefecto = 0)
        {
            double TempDouble;
            double Resp = double.TryParse(Numero, out TempDouble)
                ? double.Parse(Numero)
                : NumeroPorDefecto;
            return Resp;
        }
        /// <summary>
        /// Método que convierte una lista de string en una sola variable de string.
        /// </summary>
        /// <param name="Lista">La lista de string proporcionada.</param>
        /// <param name="Separador">Caracter separador</param>
        /// <returns>TRUE = Los datos procesados; FALSE = Datos vacíos.</returns>
        public static string ListOfStringToString(List<string> Lista, string Separador = ",")
        {
            if (Lista != null)
            {
                if (Lista.Any())
                {
                    if (Lista.Count() < 2)
                    { return string.Empty; }
                    return string.Join(Separador, Lista);
                }
            }
            return string.Empty;
        }
    }
}
