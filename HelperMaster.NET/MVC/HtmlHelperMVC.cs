using HelperMaster.NET.Criptografia;
using HelperMaster.NET.DTO;
using HelperMaster.NET.Enum;
using HelperMaster.NET.General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HelperMaster.NET.MVC
{
    /// <summary>
    /// Clase que permite complementar Helpers de los ya existentes hacia la vista.
    /// </summary>
    public static class HtmlHelperMVC
    {
        /// <summary>
        /// Método Helper que permite utilizar el tag DIV de forma automatizada dentro de un ViewModel.
        /// </summary>
        /// <typeparam name="TModel">El modelo a trabajar.</typeparam>
        /// <param name="helper">El Helper de MVC (tomado de forma implícita).</param>
        /// <param name="expression">La expresión Linq, refiriéndose a un elemento del modelo.</param>
        /// <param name="SubTag">El tag hijo para incorporar dentro del tag DIV.</param>
        /// <param name="HtmlAttributes">Atributos adicionales hacia el tag DIV.</param>
        /// <returns>El tag DIV construido.</returns>
        public static MvcHtmlString DivFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, Div>> expression, MvcHtmlString SubTag, object HtmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            TagBuilder div = new TagBuilder("div");

            Div AtributoModel = (Div)data.Model;

            div.GenerateId(propertyName);
            div.Attributes.Add("name", propertyName);

            if (!AtributoModel.Visible)
            {
                div.Attributes.Add("style", "display:none;");
            }
            else if (AtributoModel.ReadOnly)
            {
                div.Attributes.Add("style", "pointer-events:none;");
            }

            if (HtmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(HtmlAttributes);
                div.MergeAttributes(attributes);
            }

            div.InnerHtml = SubTag.ToHtmlString();

            return new MvcHtmlString(div.ToString());
        }
        /// <summary>
        /// Método Helper que permite utilizar el tag DIV de forma automatizada dentro de un ViewModel.
        /// </summary>
        /// <typeparam name="TModel">El modelo a trabajar.</typeparam>
        /// <param name="helper">El Helper de MVC (tomado de forma implícita).</param>
        /// <param name="expression">La expresión Linq, refiriéndose a un elemento del modelo.</param>
        /// <param name="ListaSubTag">El(los) tag(s) hijo(s) para incorporar dentro del tag DIV.</param>
        /// <param name="HtmlAttributes">Atributos adicionales hacia el tag DIV.</param>
        /// <returns>El tag DIV construido.</returns>
        public static MvcHtmlString DivFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, Div>> expression, List<MvcHtmlString> ListaSubTag, object HtmlAttributes = null)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            TagBuilder div = new TagBuilder("div");

            Div AtributoModel = (Div)data.Model;

            div.GenerateId(propertyName);
            div.Attributes.Add("name", propertyName);

            if (!AtributoModel.Visible)
            {
                div.Attributes.Add("style", "display:none;");
            }
            else if (AtributoModel.ReadOnly)
            {
                div.Attributes.Add("style", "pointer-events:none;");
            }

            if (HtmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(HtmlAttributes);
                div.MergeAttributes(attributes);
            }

            foreach (var item in ListaSubTag)
            {
                div.InnerHtml = div.InnerHtml + item.ToHtmlString();
            }

            return new MvcHtmlString(div.ToString());
        }

        /// <summary>
        /// Método Helper que permite utilizar el tag INPUT de tipo HIDDEN de forma automatizada dentro de un ViewModel.
        /// </summary>
        /// <typeparam name="TModel">El modelo a trabajar.</typeparam>
        /// <typeparam name="TValue">El valor a trabajar.</typeparam>
        /// <param name="helper">El Helper de MVC (tomado de forma implícita).</param>
        /// <param name="expression">La expresión Linq, refiriéndose a un elemento del modelo.</param>
        /// <returns>El tag INPUT tipo HIDDEN construido.</returns>
        public static MvcHtmlString HiddenEncryptFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            TagBuilder Hid = new TagBuilder("input");

            var RutaExpresionEnLista = expression.Body.ToString().Split('.').ToList();
            RutaExpresionEnLista.RemoveAt(0);

            Hid.GenerateId(Conversion.ListOfStringToString(RutaExpresionEnLista, "_"));
            Hid.Attributes.Add("name", Conversion.ListOfStringToString(RutaExpresionEnLista, "."));
            Hid.Attributes.Add("type", "hidden");
            Hid.Attributes.Add("value", AES.Encriptar(JsonConvert.SerializeObject(data.Model)));

            return new MvcHtmlString(Hid.ToString(TagRenderMode.SelfClosing));
        }
        /// <summary>
        /// Método Helper que permite utilizar el tag INPUT de tipo CHECKBOX de forma automatizada dentro de un ViewModel.
        /// </summary>
        /// <typeparam name="TModel">El modelo a trabajar.</typeparam>
        /// <param name="helper">El Helper de MVC (tomado de forma implícita).</param>
        /// <param name="expression">La expresión Linq, refiriéndose a un elemento del modelo.</param>
        /// <returns>El tag INPUT tipo CHECKBOX construido.</returns>
        public static MvcHtmlString CheckBoxFor<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, EstadoCheckBox>> expression)
        {
            var data = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string propertyName = data.PropertyName;
            TagBuilder ChkBox = new TagBuilder("input");
            TagBuilder ChkBoxScript = new TagBuilder("script");
            ChkBoxScript.Attributes.Add("type", "text/javascript");

            EstadoCheckBox Valor = (EstadoCheckBox)data.Model;

            var RutaExpresionEnLista = expression.Body.ToString().Split('.').ToList();
            RutaExpresionEnLista.RemoveAt(0);

            ChkBox.GenerateId(Conversion.ListOfStringToString(RutaExpresionEnLista, "_"));
            ChkBox.Attributes.Add("name", Conversion.ListOfStringToString(RutaExpresionEnLista, "."));
            ChkBox.Attributes.Add("type", "checkbox");

            switch (Valor)
            {
                case EstadoCheckBox.Checked:
                    ChkBox.Attributes.Add("checked", "checked");
                    break;
                case EstadoCheckBox.Indeterminate:
                    ChkBoxScript.InnerHtml = "$(function () " +
                                             "{ " +
                                             "    $(\"#" + Conversion.ListOfStringToString(RutaExpresionEnLista, "_") + "\").prop(\"indeterminate\", true); " +
                                             "}); ";
                    break;
                case EstadoCheckBox.Unchecked:
                    ChkBox.Attributes.Remove("checked");
                    break;
            }

            return new MvcHtmlString(ChkBox.ToString(TagRenderMode.SelfClosing) + char.ConvertFromUtf32(13) + ChkBoxScript.ToString());
        }
    }
}
