using HelperMaster.NET.Enum;
using System;

namespace HelperMaster.NET.DTO
{
    /// <summary>
    /// Clase que permite estandarizar una respuesta del sistema interno.
    /// </summary>
    /// <typeparam name="T">Clase generica que acepta la estructura de datos necesitado en ese momento.</typeparam>
    public class Respuesta<T>
    {
        /// <summary>
        /// Atributo que guarda o establece si la respuesta de la operacion es exitosa (TRUE) o si es fallido (FALSE).
        /// </summary>
        public bool decision { get; set; }
        /// <summary>
        /// Atributo que guarda o establece el mensaje de justificacion de la decision tomada.
        /// </summary>
        public string mensaje { get; set; }
        /// <summary>
        /// Atributo que guarda o establece el mensaje que detalla al mensaje de justificacion tomada.
        /// </summary>
        public string mensajeDetalle { get; set; }
        /// <summary>
        /// Atributo que guarda o establece la estructura de datos que se necesita retornar.
        /// </summary>
        public T estructuraDatos { get; set; }
        /// <summary>
        /// Atributo que guarda o establece el tipo de respuesta devuelto.
        /// </summary>
        public TipoRespuesta TipoRespuesta { get; set; }
        /// <summary>
        /// Atributo que guarda o establece la respuesta encapsulada.
        /// </summary>
        private SubRespuesta SubRespuesta { get; set; }

        /// <summary>
        /// Metodo que permite obtener un mensaje generico para ser mostrado hacia la capa de presentacion.
        /// </summary>
        /// <returns>Los datos genericos de error del sistema.</returns>
        public Respuesta<T> MensajeGenerico()
        {
            this.decision = false;
            this.mensaje = "Problemas Técnicos.";
            this.mensajeDetalle = "Comuníquese con Informática reportando esta anomalía. " +
                "Fecha y hora del error: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            this.TipoRespuesta = TipoRespuesta.ErrorTecnico;
            return this;
        }
        /// <summary>
        /// Metodo que permite capturar los datos de respuesta de forma ordenada.
        /// </summary>
        /// <returns>Los datos solicitados.</returns>
        public SubRespuesta CapturarDatos()
        {
            this.SubRespuesta = new SubRespuesta();
            this.SubRespuesta.decision = this.decision;
            this.SubRespuesta.mensaje = this.mensaje;
            this.SubRespuesta.mensajeDetalle = this.mensajeDetalle;
            this.SubRespuesta.TipoRespuesta = this.TipoRespuesta;
            return this.SubRespuesta;
        }
        /// <summary>
        /// Metodo que permite obtener un mensaje generico para ser mostrado hacia la capa de presentacion.
        /// </summary>
        /// <returns>Los datos genericos de error del sistema.</returns>
        public Respuesta<T> MensajeGenerico(SubRespuesta Datos)
        {
            this.decision = Datos.decision;
            this.mensaje = Datos.mensaje;
            this.mensajeDetalle = Datos.mensajeDetalle;
            this.TipoRespuesta = Datos.TipoRespuesta;
            return this;
        }
    }
}
