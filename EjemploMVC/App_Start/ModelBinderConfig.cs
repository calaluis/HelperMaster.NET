using EjemploMVC.Models.ViewModels;
using System.Web.Mvc;

namespace EjemploMVC.App_Start
{
    public class ModelBinderConfig
    {
        public static void RegistrarModelos()
        {
            ModelBinders.Binders.Add(typeof(InicioVm), new InicioVm());
        }
    }
}