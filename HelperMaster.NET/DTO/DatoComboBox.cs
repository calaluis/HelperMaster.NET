using System.Collections.Generic;

namespace HelperMaster.NET.DTO
{
    /// <summary>
    /// Clase que define la estructura de ComboBox o DropDownList utilizados.
    /// </summary>
    public class DatoComboBox
    {
        /// <summary>
        /// Atributo que guarda o establece la descripcion de la opcion.
        /// </summary>
        public string nombreOpcion { get; set; }
        /// <summary>
        /// Atributo que guarda o establece la ID de opcion.
        /// </summary>
        public string numeroOpcion { get; set; }
        /// <summary>
        /// Atributo que guarda o establece si la opcion es seleccionada o no.
        /// </summary>
        public bool esSeleccionado { get; set; }

        /// <summary>
        /// Metodo constructor que inicializa el objeto.
        /// </summary>
        public DatoComboBox()
        {

        }

        /// <summary>
        /// Metodo constructor que inicializa el objeto.
        /// </summary>
        /// <param name="nombreOpcion">La descripcion de la opcion.</param>
        /// <param name="numeroOpcion">La ID de opcion.</param>
        /// <param name="esSeleccionado">TRUE = esta seleccionado; FALSE = no lo esta.</param>
        public DatoComboBox(string nombreOpcion, string numeroOpcion, bool esSeleccionado)
        {
            this.nombreOpcion = nombreOpcion.Trim();
            this.numeroOpcion = numeroOpcion.Trim();
            this.esSeleccionado = esSeleccionado;
        }

        /// <summary>
        /// Metodo que permite seleccionar un elemento especificado en la lista.
        /// </summary>
        /// <param name="ListaElementos">La lista de todos los elementos.</param>
        /// <param name="numeroOpcion">La ID del elemento seleccionado.</param>
        /// <returns>La lista de elementos con el elemento seleccionado.</returns>
        public static List<DatoComboBox> SeleccionarElemento(List<DatoComboBox> ListaElementos, string numeroOpcion)
        {
            foreach (var item in ListaElementos)
            {
                item.esSeleccionado = false;
                item.numeroOpcion = item.numeroOpcion.Trim();
                item.nombreOpcion = item.nombreOpcion.Trim();
            }
            int indice = ListaElementos.FindIndex(o => o.numeroOpcion == numeroOpcion.Trim());
            if (indice >= 0)
            {
                ListaElementos[indice].esSeleccionado = true;
            }
            return ListaElementos;
        }

        /// <summary>
        /// Metodo que permite no dejarseleccionado ningun elemento especificado en la lista.
        /// </summary>
        /// <param name="ListaElementos">La lista de todos los elementos.</param>
        /// <returns>La lista de elementos.</returns>
        public static List<DatoComboBox> SeleccionarElemento(List<DatoComboBox> ListaElementos)
        {
            foreach (var item in ListaElementos)
            {
                item.esSeleccionado = false;
            }
            return ListaElementos;
        }
    }
}
