namespace HelperMaster.NET.DTO
{
    /// <summary>
    /// Clase que define la estructura de datos complementarios a los atributos de un tag DIV HTML.
    /// </summary>
    public class Div
    {
        /// <summary>
        /// Atributo que guarda o establece si el tag DIV es visible o no.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Atributo que guarda o establece si el contenido del tag DIV es de sólo lectura o no.
        /// </summary>
        public bool ReadOnly { get; set; }
    }
}
