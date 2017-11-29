using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace HelperMaster.NET.General
{
    /// <summary>
    /// Clase que permite el mapeo dinámico de una tupla de objeto DataTable hacia otro 
    /// objeto de forma optimizada, utilizado en ADO.NET.
    /// </summary>
    public static class DataTableX
    {
        /// <summary>
        /// Metodo que permite convertir un DataTable a un objeto dinamico.
        /// </summary>
        /// <param name="table">El DataTable a procesar.</param>
        /// <returns>La tupla dinamica.</returns>
        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            // Validate argument here..

            return table.AsEnumerable().Select(row => new DynamicRow(row));
        }
        /// <summary>
        /// Metodo que permite saber, si el campo a ser mostrado existe o no.
        /// </summary>
        /// <param name="Estructura">La estructura de la tupla obtenida.</param>
        /// <param name="NombreCampo">El nombre del campo.</param>
        /// <returns>TRUE = Existe el campo; FALSE = No existe.</returns>
        private static bool ExisteCampo(dynamic Estructura, string NombreCampo)
        {
            DataRow Fila = Estructura.Row;
            if (Fila.Table.Columns.Contains(NombreCampo))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Metodo que permite saber, si el campo a ser mostrado existe o no, y tambien si tiene datos.
        /// </summary>
        /// <param name="Estructura">La estructura de la tupla obtenida.</param>
        /// <param name="NombreCampo">El nombre del campo.</param>
        /// <returns>TRUE = Existe el campo; FALSE = No existe.</returns>
        private static bool ExisteCampoConDatos(dynamic Estructura, string NombreCampo)
        {
            DataRow Fila = Estructura.Row;
            if (Fila.Table.Columns.Contains(NombreCampo))
            {
                if (Fila[NombreCampo] != DBNull.Value)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Metodo que permite obtener el dato tipificado.
        /// </summary>
        /// <param name="Estructura">La estructura de la tupla obtenida.</param>
        /// <param name="NombreCampo">El nombre del campo.</param>
        /// <param name="Tipificacion">La tipificacion final deseada. 
        /// Si no se sabe la tipifacion de origen, debe poner DBNull.Value.GetType(), 
        /// para lo cual, el sistema lo tomara como string.</param>
        /// <returns>TRUE = El dato tipificado; FALSE = El dato seteado como vacio y sin significado alguno para el negocio.</returns>
        public static dynamic ObtenerTipificacion(dynamic Estructura, string NombreCampo, Type Tipificacion)
        {
            decimal TempDecimal;
            bool TempBool;
            if (Estructura.GetType() == typeof(DynamicRow))
            {
                DataRow Fila = Estructura.Row;
                if (ExisteCampo(Estructura, NombreCampo))
                {
                    if (ExisteCampoConDatos(Estructura, NombreCampo))
                    {
                        switch (Tipificacion.Name)
                        {
                            case "Decimal":
                                if (decimal.TryParse(Fila[NombreCampo].ToString(), out TempDecimal))
                                {
                                    string Dec = Fila[NombreCampo].ToString();
                                    if (Dec.Contains(","))
                                    {
                                        string Decimales = Dec.Split(',')[1];
                                        if (Conversion.StringToDecimal(Decimales) < 1)
                                        {
                                            Dec = Dec.Split(',')[0];
                                        }
                                    }
                                    return Conversion.StringToDecimal(Dec);
                                }
                                else
                                {
                                    return 0;
                                }
                            case "Int32":
                                return Conversion.StringToInt32(Fila[NombreCampo].ToString());
                            case "DateTime":
                                return Conversion.StringToDateTime(Fila[NombreCampo].ToString(), new DateTime(1800, 1, 1));
                            case "Double":
                                return Conversion.StringToDouble(Fila[NombreCampo].ToString());
                            case "Int64":
                                return Conversion.StringToInt64(Fila[NombreCampo].ToString());
                            case "Boolean":
                                if (bool.TryParse(Fila[NombreCampo].ToString(), out TempBool))
                                {
                                    return bool.Parse(Fila[NombreCampo].ToString());
                                }
                                else
                                {
                                    if (Fila[NombreCampo].ToString().Contains("1"))
                                    {
                                        return true;
                                    }
                                    return false;
                                }
                            case "Enum":
                                return Fila[NombreCampo].ToString().Trim();
                            default:
                                return Fila[NombreCampo].ToString().Trim();
                        }
                    }
                    else
                    {
                        switch (Tipificacion.Name)
                        {
                            case "Decimal":
                                return 0;
                            case "Int32":
                                return 0;
                            case "DateTime":
                                return new DateTime(1800, 1, 1);
                            case "Double":
                                return 0;
                            case "Int64":
                                return 0;
                            case "Boolean":
                                return false;
                            case "Enum":
                                return "-1";
                            default:
                                return string.Empty;
                        }
                    }
                }
                else
                {
                    return "(Campo no existente en la tupla resultante).";
                }
            }
            else
            {
                return "ERROR: La estructura ingresada no es una fila.";
            }
        }
        /// <summary>
        /// Metodo que permite obtener los nombres de columnas de una tupla.
        /// </summary>
        /// <param name="Datos">La tupla llenada.</param>
        /// <returns>La lista de columnas con sus tipificaciones.</returns>
        public static Dictionary<string, string> ObtenerListaColumnas(DataTable Datos)
        {
            Dictionary<string, string> Resp = new Dictionary<string, string>();
            foreach (DataColumn Columna in Datos.Columns)
            {
                Resp.Add(Columna.ColumnName, Columna.DataType.Name);
            }
            return Resp;
        }

        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;

            public DataRow Row
            {
                get { return _row; }
            }

            internal DynamicRow(DataRow row) { _row = row; }

            // Interprets a member-access as an indexer-access on the 
            // contained DataRow.
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var retVal = _row.Table.Columns.Contains(binder.Name);
                result = retVal ? _row[binder.Name] : null;
                return retVal;
            }
        }
    }
}
