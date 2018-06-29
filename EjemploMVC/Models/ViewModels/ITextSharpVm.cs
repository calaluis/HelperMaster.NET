using System.Web.Mvc;

namespace EjemploMVC.Models.ViewModels
{
    public class ITextSharpVm : IModelBinder
    {
        public string NombreVistaEjecutora { get; set; }
        public string CodigoReporte { get; set; }
        public string DatosEntrada { get; set; }

        public ITextSharpVm()
        {
            foreach (var item in this.GetType().GetProperties())
            {
                if (item.PropertyType.Name.Contains("String"))
                {
                    item.SetValue(this, string.Empty);
                }
            }
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ITextSharpVm VM = new ITextSharpVm();

            return VM;
        }
    }
}