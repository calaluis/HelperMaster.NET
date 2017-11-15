namespace HelperMaster.NET.Enum
{
    /// <summary>
    /// Enumeracion que permite clasificar una respuesta del sistema interno.
    /// </summary>
    public enum TipoRespuesta
    {
        /// <summary>
        /// Desconocido.
        /// </summary>
        Desconocido = -1,
        /// <summary>
        /// Respuesta correcta o que el proceso ha dado una decision verdadera del negocio.
        /// </summary>
        CorrectoNegocio = 0,
        /// <summary>
        /// Respuesta correcta con una pequeña advertencia en el flujo de negocioque debe ser subsanado por el usuario a la brevedad posible.
        /// </summary>
        AdvReglaNegocio = 1,
        /// <summary>
        /// Respuesta correcta o que el proceso tecnico ha funcionado correctamente.
        /// </summary>
        CorrectoTecnico = 2,
        /// <summary>
        /// Respuesta de error cuando no se ha cumplido una regla de negocio especifica.
        /// </summary>
        ErrorReglaNegocio = 3,
        /// <summary>
        /// Respuesta de error cuando ha dado un error tecnico de TRY CATCH.
        /// </summary>
        ErrorTecnico = 4,
        /// <summary>
        /// Respuesta de caracter informativo para el usuario.
        /// </summary>
        Informacion = 5
    }
}
