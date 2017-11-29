using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperMaster.NET.General
{
    /// <summary>
    /// Clase que permite gestionar los valores al momento de realizar 
    /// una paginación de una consulta a la base de datos. Este tipo de paginación 
    /// es muy parecido, cuando se quiera particiionar un set de datos en partes (split).
    /// </summary>
    public class PaginacionEnVolumenes<T>
    {
        private List<T> tupla;
        /// <summary>
        /// Atributo que guarda o establece el resultado de la consulta.
        /// </summary>
        public List<T> Tupla
        {
            get { return tupla; }
            set { tupla = value; }
        }
        private int numeroPagina;
        /// <summary>
        /// Atributo que guarda o establece el número de página.
        /// </summary>
        public int NumeroPagina
        {
            get { return numeroPagina; }
            set { numeroPagina = value; }
        }
        private int numeroPaginas;
        /// <summary>
        /// Atributo que guarda o establece el número de páginas que tiene el resultado.
        /// </summary>
        public int NumeroPaginas
        {
            get { return numeroPaginas; }
            set { numeroPaginas = value; }
        }
        private int filasPorPagina;
        /// <summary>
        /// Atributo que guarda o establece la cantidad de filas por página.
        /// </summary>
        public int FilasPorPagina
        {
            get { return filasPorPagina; }
            set { filasPorPagina = value; }
        }
        private bool esPaginaAnterior;
        /// <summary>
        /// Atributo que guarda o establece si hay o no una página anterior.
        /// </summary>
        public bool EsPaginaAnterior
        {
            get { return esPaginaAnterior; }
            set { esPaginaAnterior = value; }
        }
        private bool esPaginaSiguiente;
        /// <summary>
        /// Atributo que guarda o establece si hay o no una página siguiente.
        /// </summary>
        public bool EsPaginaSiguiente
        {
            get { return esPaginaSiguiente; }
            set { esPaginaSiguiente = value; }
        }
        private bool esUltimaPagina;
        /// <summary>
        /// Atributo que guarda o establece si la páfgina actual es la última página.
        /// </summary>
        public bool EsUltimaPagina
        {
            get { return esUltimaPagina; }
            set { esUltimaPagina = value; }
        }

        /// <summary>
        /// Método Constructor que permite inicializar los parámetros necesarios para realizar paginación.
        /// </summary>
        /// <param name="NumeroPagina">Número de página.</param>
        /// <param name="FilasPorPagina">Filas por página.</param>
        public PaginacionEnVolumenes(int NumeroPagina, int FilasPorPagina)
        {
            this.NumeroPagina = NumeroPagina;
            this.FilasPorPagina = FilasPorPagina;
        }

        /// <summary>
        /// Método que permite aplicar paginación a una tupla genérica.
        /// </summary>
        /// <param name="DatosTupla">La información de los datos a paginar.</param>
        /// <returns>La información procesada y paginada.</returns>
        public PaginacionEnVolumenes<T> RealizarPaginacion(PaginacionEnVolumenes<T> DatosTupla)
        {
            #region Conversión a Consulta.

            IQueryable<T> Tupla = DatosTupla.Tupla.AsQueryable();

            #endregion

            #region Cálculo de Paginación.

            int CountQuery = Tupla == null ? 0 : Tupla.Count();
            DatosTupla.NumeroPaginas = CountQuery > 0 ?
                (int)Math.Ceiling(CountQuery / (double)DatosTupla.FilasPorPagina)
                : 0;
            if (DatosTupla.NumeroPagina > DatosTupla.NumeroPaginas)
            {
                DatosTupla.NumeroPagina = DatosTupla.NumeroPaginas;
            }
            DatosTupla.EsPaginaAnterior = DatosTupla.NumeroPagina > 1;
            DatosTupla.EsPaginaSiguiente = DatosTupla.NumeroPagina < DatosTupla.NumeroPaginas;
            DatosTupla.EsUltimaPagina = DatosTupla.NumeroPagina >= DatosTupla.NumeroPaginas;

            #endregion

            #region Segmentación de la paginación.

            Tupla = Tupla
                    .Skip((DatosTupla.NumeroPagina - 1) * DatosTupla.FilasPorPagina)
                    .Take(DatosTupla.FilasPorPagina);
            DatosTupla.Tupla = Tupla.ToList();

            #endregion

            return DatosTupla;
        }

        /// <summary>
        /// Método que permite obtener el número de páginas total de una tupla de lista genérica.
        /// </summary>
        /// <param name="DatosTupla">Los datos de la tupla a procesar.</param>
        /// <param name="FilasPorPagina">la cantidad de filas por página.</param>
        /// <returns>El número de páginas total.</returns>
        public int NumeroDePaginas(List<T> DatosTupla, int FilasPorPagina)
        {
            #region Cálculo de Número de páginas.

            int CountQuery = DatosTupla == null ? 0 : DatosTupla.Count();
            int NumeroPaginas = CountQuery > 0 ?
                (int)Math.Ceiling(CountQuery / (double)FilasPorPagina)
                : 0;

            #endregion

            return NumeroPaginas;
        }
    }
}
