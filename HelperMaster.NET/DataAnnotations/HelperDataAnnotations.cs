using HelperMaster.NET.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using HelperMaster.NET.Enum;
using HelperMaster.NET.General;

namespace HelperMaster.NET.DataAnnotations
{
    /// <summary>
    /// Clase de ayudante orientado a procesar Data Annotations.
    /// </summary>
    public class HelperDataAnnotations
    {
        /// <summary>
        /// Metodo que permite validar los campos de un formulario que utiliza Data Annotations.
        /// </summary>
        /// <param name="Formulario">El objeto de formulario a validar.</param>
        /// <returns>TRUE = No hay errores; FALSE = Hay errores en el formulario.</returns>
        private static Respuesta<IList<ValidationResult>> ValidarCamposFormulario(object Formulario)
        {
            Respuesta<IList<ValidationResult>> Resp = new Respuesta<IList<ValidationResult>>();
            ValidationContext contextFormulario = new ValidationContext(Formulario);
            IList<ValidationResult> errorsFormulario = new List<ValidationResult>();
            if (Validator.TryValidateObject(Formulario, contextFormulario, errorsFormulario, true))
            {
                Resp.decision = true;
                Resp.mensaje = "Validar Campos Formulario.";
                Resp.mensajeDetalle = "Todos los campos han pasado  la validacion.";
                Resp.estructuraDatos = errorsFormulario;
            }
            else
            {
                Resp.decision = false;
                Resp.mensaje = "Validar Campos Formulario.";
                Resp.mensajeDetalle = "Hay errores de validacion en los campos del formulario. Vea el sumario para mas detalles.";
                Resp.estructuraDatos = errorsFormulario;
            }
            return Resp;
        }
        /// <summary>
        /// Metodo que permite validar grandes formularios que contienen jerarquia de clases.
        /// </summary>
        /// <param name="Objeto">El objeto de gran formulario a validar.</param>
        /// <returns>TRUE = No hay errores; FALSE = Hay errores en el formulario.</returns>
        private static Respuesta<IList<ValidationResult>> BucearObjetosParaValidar(object Objeto)
        {
            Respuesta<IList<ValidationResult>> Resp = new Respuesta<IList<ValidationResult>>();
            Resp.estructuraDatos = new List<ValidationResult>();
            if (Objeto == null)
            {
                Resp.decision = false;
                Resp.mensaje = "Bucear Objeto Para Validar.";
                Resp.mensajeDetalle = "Se han detectado errores tecnicos, para los cuales, revise el detalle de validaciones realizadas.";
                Resp.TipoRespuesta = TipoRespuesta.ErrorTecnico;
                Resp.estructuraDatos.Add(new ValidationResult("El objeto ingresado no puede ser nulo."));
                return Resp;
            }
            if (Objeto.GetType().IsClass)
            {
                var Result = ValidarCamposFormulario(Objeto);
                if (!Result.decision)
                {
                    Resp = Resp.MensajeGenerico(Result.CapturarDatos());
                    foreach (var E in Result.estructuraDatos)
                    {
                        Resp.estructuraDatos.Add(E);
                    }
                }
                foreach (var item in Objeto.GetType().GetProperties())
                {
                    if (item.PropertyType.IsClass && !item.PropertyType.IsGenericType && !item.PropertyType.IsSealed)
                    {
                        var SubClase = BucearObjetosParaValidar(item.GetValue(Objeto));
                        if (!SubClase.decision)
                        {
                            Resp = Resp.MensajeGenerico(SubClase.CapturarDatos());
                            foreach (var E in SubClase.estructuraDatos)
                            {
                                Resp.estructuraDatos.Add(E);
                            }
                        }
                    }
                }
            }
            if (Resp.estructuraDatos.Any())
            {
                Resp.decision = false;
                Resp.mensaje = "Bucear Objeto Para Validar.";
                Resp.mensajeDetalle = "Se han detectado errores, para los cuales, revise el detalle de validaciones realizadas.";
                Resp.TipoRespuesta = TipoRespuesta.ErrorReglaNegocio;
            }
            else
            {
                Resp.decision = true;
                Resp.mensaje = "Bucear Objeto Para Validar.";
                Resp.mensajeDetalle = "No hay errores.";
                Resp.TipoRespuesta = TipoRespuesta.CorrectoNegocio;
            }
            return Resp;
        }
        /// <summary>
        /// Metodo que permite validar un formulario cualquiera, utilizando DataAnnotations. 
        /// NOTA: El alcance del validador no ve mas alla dentro del contenido de una lista generica, porque la lista es una clase de .NET Framework; 
        /// para ello, debe aplicarse este metodo por segunda vez a la clase DTO que compone la lista declarada; 
        /// a excepcion para el caso que el flujo de validaciones sea tratado en el mismo lugar (por un bucle que trate la coleccion de datos).
        /// </summary>
        /// <param name="Datos">El objeto del formulario proporcionado.</param>
        /// <returns>TRUE = La validacion ha sido exitosa; FALSE = Error(es) en la validacion.</returns>
        public static Respuesta<List<string>> ValidadorMaestro(object Datos)
        {
            Respuesta<List<string>> Resp = new Respuesta<List<string>>();
            Resp.estructuraDatos = new List<string>();
            var ValidacionDatos = HelperDataAnnotations.BucearObjetosParaValidar(Datos);
            if (!ValidacionDatos.decision)
            {
                #region Logica de muestreo de errores.

                List<string> LErrores = new List<string>();
                if (ValidacionDatos.TipoRespuesta == TipoRespuesta.ErrorTecnico)
                {
                    LErrores.Add(ValidacionDatos.mensajeDetalle + Environment.NewLine + Environment.NewLine);
                }
                string MensajeError = "Error de validacion de Campos.";
                foreach (var item in ValidacionDatos.estructuraDatos)
                {
                    if (item.MemberNames.GetType() == typeof(List<string>))
                    {
                        foreach (var SubErrores in item.MemberNames)
                        {
                            LErrores.Add("- " + SubErrores);
                        }
                    }
                    else
                    {
                        LErrores.Add("- " + item.ErrorMessage);
                    }
                }
                string Errores = Conversion.ListOfStringToString(LErrores, Environment.NewLine);
                Resp.estructuraDatos = LErrores;
                Resp.decision = false;
                Resp.mensaje = MensajeError;
                Resp.mensajeDetalle = Errores;
                Resp.TipoRespuesta = TipoRespuesta.ErrorReglaNegocio;

                #endregion
            }
            else
            {
                Resp.decision = true;
                Resp.mensaje = "Validacion de Campos.";
                Resp.mensajeDetalle = "La validacion ha sido exitosa.";
                Resp.TipoRespuesta = TipoRespuesta.CorrectoNegocio;
            }
            return Resp;
        }
    }
}
