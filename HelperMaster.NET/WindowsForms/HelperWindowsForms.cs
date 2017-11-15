using HelperMaster.NET.DTO;
using HelperMaster.NET.Enum;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HelperMaster.NET.WindowsForms
{
    /// <summary>
    /// Clase de ayudante orientado al arquetipo WINDOWS FORMS.
    /// </summary>
    public class HelperWindowsForms
    {
        /// <summary>
        /// Metodo estatico que permite realizar una conversion desde un control ComboBox hacia una lista generica equivalente. 
        /// NOTA: Este funciona a cualquier tipo de tipificacion de la ID del item seleccionado, es decir, 
        /// perfectamente la ID puede ser de tipo string.
        /// </summary>
        /// <param name="Datos">Datos que contiene un control ComboBox de Windows Forms.</param>
        /// <returns>Los datos convertidos en una lista generica.</returns>
        public static List<DatoComboBox> ObtenerLista(ComboBox Datos)
        {
            List<DatoComboBox> Resp = new List<DatoComboBox>();
            foreach (DatoComboBox item in Datos.Items)
            {
                item.esSeleccionado = false;
                Resp.Add(item);
            }
            if (Datos.SelectedIndex > -1)
            {
                Resp[Datos.SelectedIndex].esSeleccionado = true;
            }
            return Resp;
        }
        /// <summary>
        /// Metodo estatico que permite realizar una conversion desde un control DataGridViewComboBoxCell hacia una lista generica equivalente. 
        /// NOTA: Este funciona a cualquier tipo de tipificacion de la ID del item seleccionado, es decir, 
        /// perfectamente la ID puede ser de tipo string.
        /// </summary>
        /// <param name="Datos">Datos que contiene un control DataGridViewComboBoxCell de Windows Forms.</param>
        /// <returns>Los datos convertidos en una lista generica.</returns>
        public static List<DatoComboBox> ObtenerLista(DataGridViewComboBoxCell Datos)
        {
            List<DatoComboBox> Resp = new List<DatoComboBox>();
            foreach (DatoComboBox item in Datos.Items)
            {
                item.esSeleccionado = false;
                Resp.Add(item);
            }
            int Indice = Resp.IndexOf(Resp.Where(o => o.numeroOpcion == Datos.Value.ToString()).FirstOrDefault());
            if (Indice > -1)
            {
                Resp[Indice].esSeleccionado = true;
            }
            return Resp;
        }
        /// <summary>
        /// Metodo que permite poblar un ComboBox a partir de los datos estandarizados que se obtienen desde la capa inferior.
        /// </summary>
        /// <param name="EstructuraWinForm">El objeto ComboBox ya definido.</param>
        /// <param name="Datos">Los datos obtenidos.</param>
        /// <returns>El ComboBox poblado.</returns>
        public static ComboBox ListaToComboBox(ComboBox EstructuraWinForm, List<DatoComboBox> Datos)
        {
            EstructuraWinForm.DataSource = null;
            EstructuraWinForm.Items.Clear();
            EstructuraWinForm.DisplayMember = "nombreOpcion";
            EstructuraWinForm.ValueMember = "numeroOpcion";
            EstructuraWinForm.DataSource = Datos;
            if (Datos != null)
            {
                EstructuraWinForm.SelectedIndex = Datos.FindIndex(o => o.esSeleccionado == true);
            }
            return EstructuraWinForm;
        }
        /// <summary>
        /// Metodo que permite poblar un DataGridViewComboBoxCell a partir de los datos estandarizados que se obtienen desde la capa inferior.
        /// </summary>
        /// <param name="EstructuraWinForm">El objeto DataGridViewComboBoxCell ya definido.</param>
        /// <param name="Datos">Los datos obtenidos.</param>
        /// <returns>El ComboBox poblado.</returns>
        public static DataGridViewComboBoxCell ListaToComboBoxCell(DataGridViewComboBoxCell EstructuraWinForm, List<DatoComboBox> Datos)
        {
            EstructuraWinForm.DataSource = null;
            EstructuraWinForm.Items.Clear();
            EstructuraWinForm.DisplayMember = "nombreOpcion";
            EstructuraWinForm.ValueMember = "numeroOpcion";
            EstructuraWinForm.DataSource = Datos;
            if (Datos.Where(o => o.esSeleccionado == true).Any())
            {
                EstructuraWinForm.Value = Datos.Where(o => o.esSeleccionado == true).Select(o => o.numeroOpcion).FirstOrDefault();
            }
            return EstructuraWinForm;
        }
        /// <summary>
        /// Metodo estatico que permite realizar una conversion desde un control ListBox hacia una lista generica equivalente. 
        /// NOTA: Este funciona a cualquier tipo de tipificacion de la ID del item seleccionado, es decir, 
        /// perfectamente la ID puede ser de tipo string.
        /// </summary>
        /// <param name="Datos">Datos que contiene un control ListBox de Windows Forms.</param>
        /// <returns>Los datos convertidos en una lista generica.</returns>
        public static List<DatoComboBox> ObtenerLista(ListBox Datos)
        {
            List<DatoComboBox> Resp = new List<DatoComboBox>();
            foreach (DatoComboBox item in Datos.Items)
            {
                item.esSeleccionado = false;
                Resp.Add(item);
            }
            if (Datos.SelectedIndex > -1)
            {
                Resp[Datos.SelectedIndex].esSeleccionado = true;
            }
            return Resp;
        }
        /// <summary>
        /// Metodo que permite saber, si una ventana de formulario esta activo o no en la ejecucion de la aplicacion.
        /// </summary>
        /// <returns>TRUE = Ventna activa; FALSE = No hay ventana activa.</returns>
        public static bool VentanaActiva<T>()
        {
            foreach (Form Formulario in Application.OpenForms)
            {
                if (Formulario.GetType() == typeof(T))
                {
                    Formulario.Activate();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Metodo que permite cerrar una ventana de WindowsForms especificada.
        /// </summary>
        /// <typeparam name="T">La clase tipificada de ventana a cerrar.</typeparam>
        public static void CerrarVentana<T>()
        {
            foreach (Form Formulario in Application.OpenForms)
            {
                if (Formulario.GetType() == typeof(T))
                {
                    Formulario.Close();
                    break;
                }
            }
        }
        /// <summary>
        /// Metodo que permite obtener un archivo incrustado desde el ensamblado para ser guardado en disco.
        /// </summary>
        /// <param name="NombreArchivo">El nombre del archivo a obtener dentro del ensamblado.</param>
        /// <returns>TRUE = La ruta fisica del archivo rescatado; FALSE = El archivo no fue encontrado dentro del ensamblado.</returns>
        public static Respuesta<string> CrearArchivoDesdeEnsamblado(string NombreArchivo)
        {
            Respuesta<string> Resp = new Respuesta<string>();
            Assembly A = Assembly.GetExecutingAssembly();
            string RutaRaiz = Application.StartupPath;
            using (MemoryStream MEM = new MemoryStream())
            {
                var Recursos = A.GetManifestResourceNames();
                foreach (var item in Recursos)
                {
                    string NombreArchivoEncontrado = item.Split('.')[item.Split('.').Length - 2] +
                    "." +
                    item.Split('.')[item.Split('.').Length - 1];
                    if (NombreArchivo == NombreArchivoEncontrado)
                    {
                        A.GetManifestResourceStream(item).CopyTo(MEM);
                        File.WriteAllBytes(RutaRaiz + "\\" + NombreArchivo, MEM.ToArray());
                        Resp.decision = true;
                        Resp.mensaje = "Crear Archivo Desde Ensamblado.";
                        Resp.mensajeDetalle = "Archivo obtenido correctamente.";
                        Resp.TipoRespuesta = TipoRespuesta.CorrectoNegocio;
                        Resp.estructuraDatos = RutaRaiz + "\\" + NombreArchivo;
                        return Resp;
                    }
                }
            }
            Resp.decision = false;
            Resp.mensaje = "Crear Archivo Desde Ensamblado.";
            Resp.mensajeDetalle = "El archivo no fue encontrado dentro del ensamblado.";
            Resp.TipoRespuesta = TipoRespuesta.ErrorReglaNegocio;
            Resp.estructuraDatos = string.Empty;
            return Resp;
        }
    }
}
