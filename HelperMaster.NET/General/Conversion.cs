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
        /// <returns>TRUE = El número convertido; FALSE = el numero por defecto.</returns>
        public static decimal StringToDecimal(string Numero, decimal NumeroPorDefecto = 0)
        {
            decimal DecTemp;
            decimal Resp = decimal.TryParse(Numero, out DecTemp)
                ? decimal.Parse(Numero)
                : NumeroPorDefecto;
            return Resp;
        }
        /// <summary>
        /// Metodo que permite convertir un string a int de 32 bits.
        /// </summary>
        /// <param name="Numero">El numero a convertir.</param>
        /// <param name="NumeroPorDefecto">El numero por defecto en caso que la conversion falle.</param>
        /// <returns>TRUE = El número convertido; FALSE = el numero por defecto.</returns>
        public static int StringToInt32(string Numero, int NumeroPorDefecto = 0)
        {
            int IntTemp;
            int Resp = int.TryParse(Numero, out IntTemp)
                ? int.Parse(Numero)
                : NumeroPorDefecto;
            return Resp;
        }
        /// <summary>
        /// Metodo que permite convertir un string a int de 64 bits.
        /// </summary>
        /// <param name="Numero">El numero a convertir.</param>
        /// <param name="NumeroPorDefecto">El numero por defecto en caso que la conversion falle.</param>
        /// <returns>TRUE = El número convertido; FALSE = el numero por defecto.</returns>
        public static long StringToInt64(string Numero, long NumeroPorDefecto = 0)
        {
            long LongTemp;
            long Resp = long.TryParse(Numero, out LongTemp)
                ? long.Parse(Numero)
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
        /// <returns>TRUE = El número convertido; FALSE = el numero por defecto.</returns>
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
        /// <summary>
        /// Método que convierte una lista de enteros en una sola variable de string.
        /// </summary>
        /// <param name="Lista">La lista de enteros proporcionada.</param>
        /// <param name="Separador">Caracter separador</param>
        /// <returns>TRUE = Los datos procesados; FALSE = Datos vacíos.</returns>
        public static string ListOfIntToString(List<int> Lista, string Separador = ",")
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
        /// <summary>
        /// Método que permite convertir de un arreglo de dos dimensiones a una dimensión.
        /// </summary>
        /// <param name="Datos">El arreglo de dos dimensiones.</param>
        /// <returns>El arreglo de una dimensión.</returns>
        public static int[] Arreglo2DTo1D(int[,] Datos)
        {
            int EjeX = Datos.GetLength(0);
            int EjeY = Datos.GetLength(1);
            int[] Resp = new int[EjeX * EjeY];

            int Indice1D = 0;
            for (int Fila = 0; Fila < EjeX; Fila++)
            {
                for (int Columna = 0; Columna < EjeY; Columna++)
                {
                    Resp[Indice1D] = Datos[Fila, Columna];
                    Indice1D++;
                }
            }

            return Resp;
        }
        /// <summary>
        /// Método que permite convertir un arreglo de una dimensión a un arreglo de dos dimensiones.
        /// </summary>
        /// <param name="Datos">El arreglo de una dimensión.</param>
        /// <param name="Filas">Las filas del arreglo.</param>
        /// <param name="Columnas">Las columnas del arreglo.</param>
        /// <returns>El arreglo de dos dimensiones.</returns>
        public static int[,] Arreglo1DTo2D(int[] Datos, int Filas, int Columnas)
        {
            int[,] Resp = new int[Filas, Columnas];

            int Indice1D = 0;
            for (int Fila = 0; Fila < Filas; Fila++)
            {
                for (int Columna = 0; Columna < Columnas; Columna++)
                {
                    Resp[Fila, Columna] = Datos[Indice1D];
                    Indice1D++;
                }
            }

            return Resp;
        }
    }
}
