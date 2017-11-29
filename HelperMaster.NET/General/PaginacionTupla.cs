namespace HelperMaster.NET.General
{
    /// <summary>
    /// Resumen: Clase que permite realizar paginación a través de un procedimiento almacenado, es decir, 
    /// la paginación debe ser realizada por un procedimiento almacenado por medio de los parámetros 
    /// entregados por esta clase aplicada. Para más información de su utilización, visite la página web del proyecto.
    /// </summary>
    public class PaginacionTupla
    {
        private int NroFilas; // numero de filas obtenidas desde la BD.

        /// <summary>
        /// Obtiene o establece el numero de filas obtenidas desde la BD.
        /// </summary>
        public int Nro_Filas
        {
            get { return NroFilas; }
            set { NroFilas = value; }
        }
        private int NroFilasPorPagina; // Numero de filas por pagina.

        /// <summary>
        /// Obtiene o establece el numero de filas por pagina.
        /// </summary>
        public int Nro_Filas_Por_Pagina
        {
            get { return NroFilasPorPagina; }
            set { NroFilasPorPagina = value; }
        }
        private int NroPag; // Numero de pagina mostrada 

        /// <summary>
        /// Obtiene o establece el numero de pagina mostrada.
        /// </summary>
        public int Nro_Pag
        {
            get { return NroPag; }
            set { NroPag = value; }
        }
        private int Paginas; // Numero de paginas totales con respecto al numero de filas.

        /// <summary>
        /// Obtiene o establece el numero de paginas totales con respecto al numero de filas.
        /// </summary>
        public int Paginas_Total
        {
            get { return Paginas; }
            set { Paginas = value; }
        }
        private int Resto; // Cantidad de filas minoritarias que es menor al nro. de filas por pagina.

        /// <summary>
        /// Obtiene o establece la cantidad de filas minoritarias que es menor al nro. de filas por pagina.
        /// </summary>
        public int Resto_Filas
        {
            get { return Resto; }
            set { Resto = value; }
        }
        private int PagIni; // Representa la fila inicial al delimitar la tupla.

        /// <summary>
        /// Obtiene o establece la representacion de la fila inicial al delimitar la tupla.
        /// </summary>
        public int Pag_Ini
        {
            get { return PagIni; }
            set { PagIni = value; }
        }
        private int PagFin; // Representa la fila final al delimitar la tupla.

        /// <summary>
        /// Obtiene o establece la representacion de la fila final al delimitar la tupla.
        /// </summary>
        public int Pag_Fin
        {
            get { return PagFin; }
            set { PagFin = value; }
        }
        private int NumeroDePagina;

        /// <summary>
        /// Obtiene o establece el numero de pagina ingresada por el usuario.
        /// </summary>
        public int Numero_De_Pagina
        {
            get { return NumeroDePagina; }
            set { NumeroDePagina = value; }
        }

        /// <summary>
        /// Metodo que realiza el calculo de la paginacion.
        /// </summary>
        /// <param name="Pag">Los datos a paginar.</param>
        /// <returns>El calculo con los resultados en los datos cambiados.</returns>
        private PaginacionTupla CalculoPaginacion(PaginacionTupla Pag)
        {
            Pag.Paginas_Total = Pag.Nro_Filas / Pag.Nro_Filas_Por_Pagina;
            Pag.Resto_Filas = Pag.Nro_Filas % Pag.Nro_Filas_Por_Pagina;
            if (Pag.Resto_Filas != 0)
            {
                Pag.Paginas_Total = Pag.Paginas_Total + 1;
            }
            if (Pag.Nro_Filas < Pag.Nro_Filas_Por_Pagina)
            {
                Pag.Nro_Filas = Pag.Nro_Filas_Por_Pagina;
                Pag.Paginas_Total = Pag.Nro_Filas / Pag.Nro_Filas_Por_Pagina;
            }
            return Pag;
        }

        /// <summary>
        /// Metodo que permite realizar la consulta en forma paginada.
        /// </summary>
        /// <param name="Pag">Los valores necesarios para realizar la paginacion.</param>
        /// <returns>Los valores para paginar.</returns>
        public PaginacionTupla ConsultaPaginacion(PaginacionTupla Pag)
        {
            Pag = CalculoPaginacion(Pag);
            Pag.Pag_Ini = 1;
            Pag.Nro_Pag = 1;
            Pag.Pag_Fin = Pag.Nro_Filas_Por_Pagina;
            return Pag;
        }

        /// <summary>
        /// Metodo que permite obtener la primera pagina de la tupla.
        /// </summary>
        /// <param name="Pag">Los datos necesarios a paginar.</param>
        /// <returns>Los valores para establecer la primera pagina.</returns>
        public PaginacionTupla IndicePrimero(PaginacionTupla Pag)
        {
            Pag = CalculoPaginacion(Pag);
            Pag.Pag_Ini = 1;
            Pag.Nro_Pag = 1;
            Pag.Pag_Fin = Pag.Nro_Filas_Por_Pagina;
            return Pag;
        }

        /// <summary>
        /// Metodo que permite obtener la pagina anterior de toda la tupla.
        /// </summary>
        /// <param name="Pag">Los datos necesarios para paginar.</param>
        /// <returns>Los valores para establecer la pagina anterior.</returns>
        public PaginacionTupla IndiceAnterior(PaginacionTupla Pag)
        {
            Pag = CalculoPaginacion(Pag);
            Pag.Pag_Ini = Pag.Pag_Ini - Pag.Nro_Filas_Por_Pagina;
            Pag.Pag_Fin = Pag.Pag_Fin - Pag.Nro_Filas_Por_Pagina;
            Pag.Nro_Pag = Pag.Nro_Pag - 1;
            if (Pag.Nro_Pag < 1)
            {
                Pag.Pag_Ini = 1;
                Pag.Nro_Pag = 1;
                Pag.Pag_Fin = Pag.Nro_Filas_Por_Pagina;
            }
            return Pag;
        }

        /// <summary>
        /// Metodo que permite obtener la pagina elegida por el usuario.
        /// </summary>
        /// <param name="Pag">Los datos necesarios para paginar.</param>
        /// <returns>Los valores para establecer la pagina elegida por el usuario.</returns>
        public PaginacionTupla NumeroPagina(PaginacionTupla Pag)
        {
            Pag = CalculoPaginacion(Pag);
            Pag.Pag_Ini = 1;
            Pag.Nro_Pag = 1;
            Pag.Pag_Fin = Pag.Nro_Filas_Por_Pagina;
            if (Pag.Numero_De_Pagina > Pag.Paginas_Total)
            {
                Pag.Numero_De_Pagina = Pag.Paginas_Total;
            }
            if (Pag.Numero_De_Pagina < 1)
            {
                Pag.Numero_De_Pagina = Pag.Pag_Ini;
            }
            for (int i = 1; i < Pag.Numero_De_Pagina; i++)
            {
                Pag.Pag_Ini = Pag.Pag_Ini + Pag.Nro_Filas_Por_Pagina;
                Pag.Pag_Fin = Pag.Pag_Fin + Pag.Nro_Filas_Por_Pagina;
                Pag.Nro_Pag = Pag.Nro_Pag + 1;
            }
            return Pag;
        }

        /// <summary>
        /// Metodo que permite obtener la pagina siguiente de toda la tupla.
        /// </summary>
        /// <param name="Pag">Los datos necesarios para paginar.</param>
        /// <returns>Los valores para establecer la pagina siguiente.</returns>
        public PaginacionTupla IndicePosterior(PaginacionTupla Pag)
        {
            Pag = CalculoPaginacion(Pag);
            Pag.Pag_Ini = Pag.Pag_Ini + Pag.Nro_Filas_Por_Pagina;
            Pag.Pag_Fin = Pag.Pag_Fin + Pag.Nro_Filas_Por_Pagina;
            Pag.Nro_Pag = Pag.Nro_Pag + 1;
            if (Pag.Nro_Pag > Pag.Paginas_Total)
            {
                Pag.Pag_Ini = Pag.Pag_Ini - Pag.Nro_Filas_Por_Pagina;
                Pag.Pag_Fin = Pag.Pag_Fin - Pag.Nro_Filas_Por_Pagina;
                Pag.Nro_Pag = Pag.Nro_Pag - 1;
            }
            return Pag;
        }

        /// <summary>
        /// Metodo que permite obtener la ultima pagina de toda la tupla.
        /// </summary>
        /// <param name="Pag">Los datos necesarios para paginar.</param>
        /// <returns>Los valores para establecer la ultima pagina.</returns>
        public PaginacionTupla IndiceUltimo(PaginacionTupla Pag)
        {
            Pag = CalculoPaginacion(Pag);
            Pag.Pag_Ini = (Pag.Nro_Filas - Pag.Nro_Filas_Por_Pagina) + 1;
            if (Pag.Resto_Filas != 0)
            {
                Pag.Pag_Ini = (Pag.Nro_Filas - Pag.Resto_Filas) + 1;
            }
            Pag.Pag_Fin = (Pag.Pag_Ini + Pag.Nro_Filas_Por_Pagina) - 1;
            Pag.Nro_Pag = Pag.Paginas_Total;
            return Pag;
        }
    }
}
