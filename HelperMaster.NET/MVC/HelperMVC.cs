using HelperMaster.NET.Criptografia;
using HelperMaster.NET.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace HelperMaster.NET.MVC
{
    /// <summary>
    /// Clase de ayudante orientado al arquetipo MVC.
    /// </summary>
    public class HelperMVC
    {
        /// <summary>
        /// Método que permite capturar un dato proveniente de la vista.
        /// </summary>
        /// <param name="Valor">El valor proveniente del proveedor de valores.</param>
        /// <param name="ValorDefecto">El valor por defecto que es palabra vacía.</param>
        /// <returns>El valor capturado.</returns>
        public static string ConsultarValorEnlazadorModelo(ValueProviderResult Valor, string ValorDefecto = "")
        {
            if (Valor != null)
            {
                return Valor.AttemptedValue.Trim().Replace("_", "");
            }
            return ValorDefecto;
        }
        /// <summary>
        /// Método que permite capturar un dato proveniente de la vista.
        /// </summary>
        /// <param name="bindingContext">El contexto de enlazador de modelos proporcionado.</param>
        /// <param name="Valor">El valor que especifica la variable declarada en la vista.</param>
        /// <param name="Tipificacion">La tipificación deseada del valor capturado.</param>
        /// <returns>El valor capturado con su tipificación deseada.</returns>
        public static dynamic ConsultarValorEnlazadorModelo(ModelBindingContext bindingContext, string Valor, Type Tipificacion)
        {
            Valor = Valor.Trim();
            if (Tipificacion == typeof(string))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        string ValorRet = (string)bindingContext.ValueProvider.GetValue(Valor).ConvertTo(Tipificacion);
                        return ValorRet.Replace("_", "");
                    }
                }
                return string.Empty;
            }
            if (Tipificacion == typeof(int))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return ExtraerNumeroDeMascara(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue, Tipificacion);
                    }
                }
                return 0;
            }
            if (Tipificacion == typeof(decimal))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return ExtraerNumeroDeMascara(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue, Tipificacion);
                    }
                }
                return 0;
            }
            if (Tipificacion == typeof(DateTime))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return (DateTime)bindingContext.ValueProvider.GetValue(Valor).ConvertTo(Tipificacion);
                    }
                }
                return new DateTime(1800, 1, 1);
            }
            if (Tipificacion == typeof(DateTime?))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return (DateTime)bindingContext.ValueProvider.GetValue(Valor).ConvertTo(Tipificacion);
                    }
                }
                return null;
            }
            if (Tipificacion == typeof(double))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return ExtraerNumeroDeMascara(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue, Tipificacion);
                    }
                }
                return 0;
            }
            if (Tipificacion == typeof(long))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return ExtraerNumeroDeMascara(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue, Tipificacion);
                    }
                }
                return 0;
            }
            if (Tipificacion == typeof(bool))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return (bool)bindingContext.ValueProvider.GetValue(Valor).ConvertTo(Tipificacion);
                    }
                }
                return false;
            }
            if (Tipificacion == typeof(List<DatoComboBox>))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return JsonConvert.DeserializeObject<List<DatoComboBox>>(AES.Desencriptar(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue));
                    }
                }
                return new List<DatoComboBox>();
            }
            if (Tipificacion == typeof(Div))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        return JsonConvert.DeserializeObject<Div>(AES.Desencriptar(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue));
                    }
                }
                return new List<DatoComboBox>();
            }
            if (Tipificacion.BaseType == typeof(System.Enum))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    if (!string.IsNullOrEmpty(bindingContext.ValueProvider.GetValue(Valor).AttemptedValue))
                    {
                        string ValorProcesado = bindingContext.ValueProvider.GetValue(Valor).AttemptedValue;
                        if (ValorProcesado.Contains("="))
                        {
                            return System.Enum.Parse(Tipificacion, AES.Desencriptar(ValorProcesado));
                        }
                        else
                        {
                            return System.Enum.Parse(Tipificacion, ValorProcesado);
                        }
                    }
                }
                return System.Enum.Parse(Tipificacion, "-1");
            }
            if (Tipificacion == typeof(MultiSelectList))
            {
                if (bindingContext.ValueProvider.GetValue(Valor) != null)
                {
                    IEnumerable<string> DatosSeleccionados = (IEnumerable<string>)bindingContext.ValueProvider.GetValue(Valor).RawValue;
                    return DatosSeleccionados;
                }
            }
            return null;
        }
        /// <summary>
        /// Método que permite extraer el número encontrado dentro de una máscara que proviene desde la interfaz usuaria.
        /// </summary>
        /// <param name="NumeroEnMascara">El número que está dentro de una máscara.</param>
        /// <param name="Tipificacion">La tipificación numérica deseada.</param>
        /// <returns>El número sin la máscara con su tipificación específica.</returns>
        public static dynamic ExtraerNumeroDeMascara(string NumeroEnMascara, Type Tipificacion)
        {
            string NumeroProcesado = string.Empty;
            foreach (var item in NumeroEnMascara.ToArray())
            {
                if (char.IsNumber(item))
                {
                    NumeroProcesado = NumeroProcesado + item.ToString();
                }
            }
            if (Tipificacion == typeof(int))
            {
                int Temp;
                return int.TryParse(NumeroProcesado, out Temp) ? Temp : 0;
            }
            if (Tipificacion == typeof(decimal))
            {
                decimal Temp;
                return decimal.TryParse(NumeroProcesado, out Temp) ? Temp : 0;
            }
            if (Tipificacion == typeof(double))
            {
                double Temp;
                return double.TryParse(NumeroProcesado, out Temp) ? Temp : 0;
            }
            if (Tipificacion == typeof(long))
            {
                long Temp;
                return long.TryParse(NumeroProcesado, out Temp) ? Temp : 0;
            }
            return null;
        }
        /// <summary>
        /// Método que permite setear un arreglo de datos en la interfaz usuaria.
        /// </summary>
        /// <returns>El arreglo de datos seteado.</returns>
        public static SelectList ToSelectList()
        {
            List<DatoComboBox> Generico = new List<DatoComboBox>();
            return ToSelectList(Generico);
        }
        /// <summary>
        /// Método que permite parsear una lista de datos a un arreglo de datos de la interfaz usuaria.
        /// </summary>
        /// <param name="Generico">La lista de datos genéricos.</param>
        /// <returns>El arreglo de datos para la interfaz usuaria.</returns>
        public static SelectList ToSelectList(List<DatoComboBox> Generico)
        {
            var Seleccionado = Generico.Find(o => o.esSeleccionado == true);
            if (Seleccionado != null)
            {
                if (!Generico.Where(o => o.numeroOpcion == string.Empty).Any())
                {
                    Generico.Insert(0, new DatoComboBox("(Seleccione Opcion)", "", false));
                }
                return new SelectList(Generico, "numeroOpcion", "nombreOpcion", Seleccionado.numeroOpcion);
            }
            else
            {
                if (!Generico.Where(o => o.numeroOpcion == string.Empty).Any())
                {
                    Generico.Insert(0, new DatoComboBox("(Seleccione Opcion)", "", true));
                }
                return new SelectList(Generico, "numeroOpcion", "nombreOpcion");
            }
        }
        /// <summary>
        /// Método que permite parsear una lista de datos a un arreglo de datos de la interfaz usuaria 
        /// (no incluye la opción (Seleccione Opcion)).
        /// </summary>
        /// <param name="Generico">La lista de datos genéricos.</param>
        /// <returns>El arreglo de datos para la interfaz usuaria.</returns>
        public static SelectList ToSelectListDos(List<DatoComboBox> Generico)
        {
            Generico.RemoveAll(o => o.nombreOpcion == "(Seleccione Opcion)");
            return new SelectList(Generico, "numeroOpcion", "nombreOpcion", Generico.Where(o => o.esSeleccionado).Select(o => o.numeroOpcion).FirstOrDefault());
        }
        /// <summary>
        /// Método que permite parsear una lista de datos genérica a una lista de caja de datos de la interfaz usuaria.
        /// </summary>
        /// <param name="Generico">La lista de datos genéricos.</param>
        /// <returns>El arreglo de lista de caja de datos para la interfaz usuaria.</returns>
        public static MultiSelectList ToMultiSelectList(List<DatoComboBox> Generico)
        {
            return new MultiSelectList(Generico, "numeroOpcion", "nombreOpcion", Generico.Where(o => o.esSeleccionado).Select(o => o.numeroOpcion));
        }
        /// <summary>
        /// Método que permite extraer el nombre de etiqueta de un atributo de entidad de clase específica.
        /// </summary>
        /// <typeparam name="TModel">La clase de entidad proporcionada.</typeparam>
        /// <param name="expression">La expresión lamda determinada (se especifica el atributo a extraer su etiqueta).</param>
        /// <returns>La etiqueta extraída.</returns>
        public static string ObtenerDisplayName<TModel>(Expression<Func<TModel, object>> expression)
        {
            Type type = typeof(TModel);
            MemberExpression memberExpression = null;
            switch (expression.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression)expression.Body;
                    break;
                case ExpressionType.ConvertChecked:
                case ExpressionType.Convert:
                    var ue = (UnaryExpression)expression.Body;
                    memberExpression = (MemberExpression)ue.Operand;
                    break;
            }
            string propertyName = ((memberExpression.Member is PropertyInfo) ? memberExpression.Member.Name : null);
            DisplayNameAttribute Atributo = (DisplayNameAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
            return Atributo.DisplayName;
        }
    }
}
