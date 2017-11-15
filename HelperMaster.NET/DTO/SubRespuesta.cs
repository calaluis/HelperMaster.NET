using HelperMaster.NET.Enum;

namespace HelperMaster.NET.DTO
{
    /// <summary>
    /// Clase que permite desacoplar los datos semanticos de respuesta, 
    /// para ser tranferidos entre capas.
    /// </summary>
    public class SubRespuesta
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
        /// Atributo que guarda o establece el tipo de respuesta devuelto.
        /// </summary>
        public TipoRespuesta TipoRespuesta { get; set; }
    }
}
