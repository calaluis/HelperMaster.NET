using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HelperMaster.NET.Criptografia
{
    /// <summary>
    /// Clase que define los métodos de encriptación y desencriptación AES de datos complementarios a los que 
    /// trae .NET Framework.
    /// </summary>
    public class AES
    {
        /// <summary>
        /// Método que permite encriptar un texto.
        /// </summary>
        /// <param name="plainMessage">El texto a encriptar.</param>
        /// <returns>El texto encriptado.</returns>
        public static string Encriptar(String plainMessage)
        {
            // Crear una instancia del algoritmo de Rijndael

            Rijndael RijndaelAlg = Rijndael.Create();

            byte[] Key = UTF8Encoding.UTF8.GetBytes("CALALUIS");
            byte[] IV = UTF8Encoding.UTF8.GetBytes("CALALUIS");

            Array.Resize(ref Key, 32);
            Array.Resize(ref IV, 16);

            // Establecer un flujo en memoria para el cifrado

            MemoryStream memoryStream = new MemoryStream();

            // Crear un flujo de cifrado basado en el flujo de los datos

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         RijndaelAlg.CreateEncryptor(Key, IV),
                                                         CryptoStreamMode.Write);

            // Obtener la representación en bytes de la información a cifrar

            byte[] plainMessageBytes = UTF8Encoding.UTF8.GetBytes(plainMessage);

            // Cifrar los datos enviándolos al flujo de cifrado

            cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);

            cryptoStream.FlushFinalBlock();

            // Obtener los datos datos cifrados como un arreglo de bytes

            byte[] cipherMessageBytes = memoryStream.ToArray();

            // Cerrar los flujos utilizados

            memoryStream.Close();
            cryptoStream.Close();

            // Retornar la representación de texto de los datos cifrados

            return Convert.ToBase64String(cipherMessageBytes);
        }
        /// <summary>
        /// Método que permite desencriptar un texto.
        /// </summary>
        /// <param name="encryptedMessage">El texto encriptado.</param>
        /// <returns>El texto desencriptado.</returns>
        public static string Desencriptar(String encryptedMessage)
        {
            // Obtener la representación en bytes del texto cifrado

            byte[] cipherTextBytes = Convert.FromBase64String(encryptedMessage);

            byte[] Key = UTF8Encoding.UTF8.GetBytes("CALALUIS");
            byte[] IV = UTF8Encoding.UTF8.GetBytes("CALALUIS");

            Array.Resize(ref Key, 32);
            Array.Resize(ref IV, 16);

            // Crear un arreglo de bytes para almacenar los datos descifrados

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Crear una instancia del algoritmo de Rijndael

            Rijndael RijndaelAlg = Rijndael.Create();

            // Crear un flujo en memoria con la representación de bytes de la información cifrada

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Crear un flujo de descifrado basado en el flujo de los datos

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         RijndaelAlg.CreateDecryptor(Key, IV),
                                                         CryptoStreamMode.Read);

            // Obtener los datos descifrados obteniéndolos del flujo de descifrado

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            // Cerrar los flujos utilizados

            memoryStream.Close();
            cryptoStream.Close();

            // Retornar la representación de texto de los datos descifrados

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
