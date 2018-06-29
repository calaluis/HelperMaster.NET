using System.Web.Mvc;

namespace EjemploMVC.Models.ViewModels
{
    public class InicioVm : IModelBinder
    {
        public InicioVm()
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
            InicioVm VM = new InicioVm();

            return VM;
        }
    }
}