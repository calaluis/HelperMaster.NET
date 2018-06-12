using HelperMaster.NET.DTO;
using HelperMaster.NET.Enum;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HelperMaster.NET.General
{
    /// <summary>
    /// Clase de ayuda que permite subsanar fragmentos de funcionalidades técnicas.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Metodo que permite obtener un archivo incrustado desde el ensamblado para ser guardado en disco.
        /// </summary>
        /// <param name="NombreArchivo">El nombre del archivo a obtener dentro del ensamblado.</param>
        /// <returns>TRUE = La ruta fisica del archivo rescatado; FALSE = El archivo no fue encontrado dentro del ensamblado.</returns>
        public static Respuesta<byte[]> ConsultarArchivoDesdeEnsamblado(string NombreArchivo)
        {
            Respuesta<byte[]> Resp = new Respuesta<byte[]>();
            Assembly A = Assembly.GetExecutingAssembly();
            using (MemoryStream MEM = new MemoryStream())
            {
                var Recursos = A.GetManifestResourceNames();
                foreach (var item in Recursos)
                {
                    string NombreArchivoEncontrado = item.Split('.')[item.Split('.').Length - 2] +
                    "." +
                    item.Split('.')[item.Split('.').Length - 1];
                    if (NombreArchivo == NombreArchivoEncontrado)
                    {
                        A.GetManifestResourceStream(item).CopyTo(MEM);
                        Resp.decision = true;
                        Resp.mensaje = "Crear Archivo Desde Ensamblado.";
                        Resp.mensajeDetalle = "Archivo obtenido correctamente.";
                        Resp.TipoRespuesta = TipoRespuesta.CorrectoNegocio;
                        Resp.estructuraDatos = MEM.ToArray();
                        return Resp;
                    }
                }
            }
            Resp.decision = false;
            Resp.mensaje = "Crear Archivo Desde Ensamblado.";
            Resp.mensajeDetalle = "El archivo no fue encontrado dentro del ensamblado.";
            Resp.TipoRespuesta = TipoRespuesta.ErrorReglaNegocio;
            Resp.estructuraDatos = null;
            return Resp;
        }
        /// <summary>
        /// Método que permite compatibilizar un metacaracter en XML o HTML.
        /// </summary>
        /// <param name="Palabra">La cadena a compatibilizar.</param>
        /// <returns>La cadena compatibilizada.</returns>
        public static string CompatibilizarCaracterEnXML(string Palabra)
        {
            if (string.IsNullOrEmpty(Palabra))
            {
                return string.Empty;
            }
            Palabra = Palabra.Replace("\"", "&quot;");
            Palabra = Palabra.Replace("&", "&amp;");
            Palabra = Palabra.Replace("<", "&lt;");
            Palabra = Palabra.Replace(">", "&gt;");
            return Palabra;
        }
        /// <summary>
        /// Metodo que permite agregar un item a un arreglo de cadenas.
        /// </summary>
        /// <param name="Datos">Los datos del arreglo actual.</param>
        /// <param name="Dato">El dato de cadena a agregar.</param>
        /// <returns>El arreglo de cadenas resultante.</returns>
        public static string[] AddItem(string[] Datos, string Dato)
        {
            List<string> Resp = new List<string>();
            foreach (var item in Datos)
            {
                Resp.Add(item);
            }
            Resp.Add(Dato);
            return Resp.ToArray();
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
