using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System;
using System.IO;
using System.Xml;

namespace HelperMaster.NET.General
{
    /// <summary>
    /// Clase que permite realizar la conversión de una plantilla de documento iTextXML o HTML a PDF.
    /// </summary>
    public class PDF
    {
        private string plantillaPDF;
        /// <summary>
        /// Atributo que guarda o establece los datos de la plantilla XML.
        /// </summary>
        public string PlantillaPDF
        {
            get { return plantillaPDF; }
            set { plantillaPDF = value; }
        }
        private byte[] archivoPDF;
        /// <summary>
        /// Atributo que guarda o establece el archivo PDF convertido.
        /// </summary>
        public byte[] ArchivoPDF
        {
            get { return archivoPDF; }
            set { archivoPDF = value; }
        }

        /// <summary>
        /// Método constructor que permite cargar el archivo XML en memoria, para ser manipulada.
        /// </summary>
        /// <param name="RutaPlantillaPDF">La ruta del archivo XML de plantilla.</param>
        public PDF(string RutaPlantillaPDF)
        {
            try
            {
                if (Path.HasExtension(RutaPlantillaPDF))
                {
                    using (StreamReader R = new StreamReader(RutaPlantillaPDF))
                    {
                        this.PlantillaPDF = R.ReadToEnd();
                    }
                }
            }
            catch (ArgumentException)
            {
                this.PlantillaPDF = RutaPlantillaPDF;
            }
        }

        /// <summary>
        /// Método que permite realizar la conversión de XML a PDF.
        /// </summary>
        public void GenerarPDF()
        {
            XmlParser parser;
            XmlDocument reader = GetXmlReader(this.PlantillaPDF);
            if (reader.NodeType == XmlNodeType.Document && reader.DocumentElement.Name == "itext")
                parser = new XmlParser();
            else
                parser = new HtmlParser();
            using (MemoryStream MEM = new MemoryStream())
            {
                var document = new Document();
                document.Open();
                var pdfWriter = PdfWriter.GetInstance(document, MEM);
                parser.Go(document, reader);
                pdfWriter.Flush();
                pdfWriter.Close();
                document.Close();
                this.ArchivoPDF = MEM.ToArray();
            }
        }

        /// <summary>
        /// Método que permite obtener una plantilla XML procesado.
        /// </summary>
        /// <param name="Origen">El XML en formato string.</param>
        /// <returns>El XML procesado.</returns>
        private XmlDocument GetXmlReader(string Origen)
        {
            var xtr = new XmlDocument();
            xtr.LoadXml(Origen);
            return xtr;
        }
    }
}
