using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.IO;
using System.Text;

namespace HelperMaster.NET.General
{
    /// <summary>
    /// Clase que permite realizar la conversión de una plantilla de documento HTML a PDF.
    /// </summary>
    public class PDF
    {
        /// <summary>
        /// Atributo que guarda o establece los datos de la plantilla XML.
        /// </summary>
        public string PlantillaPDF { get; set; }
        /// <summary>
        /// Atributo que guarda o establece el archivo PDF convertido.
        /// </summary>
        public byte[] ArchivoPDF { get; set; }

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
        /// Método constructor que permite cargar el archivo XML en memoria, para ser manipulada.
        /// </summary>
        /// <param name="PlantillaBinario">El archivo binario de la plantilla.</param>
        public PDF(byte[] PlantillaBinario)
        {
            using (MemoryStream MEM = new MemoryStream(PlantillaBinario))
            {
                using (StreamReader R = new StreamReader(MEM))
                {
                    this.PlantillaPDF = R.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Método que permite realizar la conversión de XML a PDF.
        /// </summary>
        public void GenerarPDF()
        {
            using (MemoryStream MEM = new MemoryStream())
            {
                using (var document = new Document(PageSize.LETTER))
                {
                    using (var pdfWriter = PdfWriter.GetInstance(document, MEM))
                    {
                        document.Open();
                        var TagProcesador = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
                        TagProcesador.RemoveProcessor(HTML.Tag.IMG);
                        TagProcesador.AddProcessor(HTML.Tag.IMG, new CustomImageTagProcessor());
                        CssFilesImpl cssFiles = new CssFilesImpl();
                        cssFiles.Add(XMLWorkerHelper.GetInstance().GetDefaultCSS());
                        var cssResolver = new StyleAttrCSSResolver(cssFiles);
                        var hpc = new HtmlPipelineContext(new CssAppliersImpl(new XMLWorkerFontProvider()));
                        hpc.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(TagProcesador);
                        var HtmlPipeline = new HtmlPipeline(hpc, new PdfWriterPipeline(document, pdfWriter));
                        var pipeline = new CssResolverPipeline(cssResolver, HtmlPipeline);
                        var worker = new XMLWorker(pipeline, true);
                        var XmlParser = new XMLParser(true, worker, Encoding.UTF8);
                        using (StringReader TxtXml = new StringReader(this.PlantillaPDF))
                        {
                            XmlParser.Parse(TxtXml);
                        }
                        document.Close();
                    }
                }
                this.ArchivoPDF = MEM.ToArray();
            }
        }
        /// <summary>
        /// Método que permite realizar la conversión de HTML a PDF.
        /// </summary>
        /// <param name="ListaRutasArchivosCSS">La lista de rutas de archivos CSS a incluir en el informe.</param>
        public void GenerarPDF(System.Collections.Generic.List<string> ListaRutasArchivosCSS)
        {
            using (MemoryStream MEM = new MemoryStream())
            {
                using (var document = new Document(PageSize.LETTER))
                {
                    using (var pdfWriter = PdfWriter.GetInstance(document, MEM))
                    {
                        document.Open();
                        var TagProcesador = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
                        TagProcesador.RemoveProcessor(HTML.Tag.IMG);
                        TagProcesador.AddProcessor(HTML.Tag.IMG, new CustomImageTagProcessor());
                        CssFilesImpl cssFiles = new CssFilesImpl();
                        cssFiles.Add(XMLWorkerHelper.GetInstance().GetDefaultCSS());
                        var cssResolver = new StyleAttrCSSResolver(cssFiles);
                        foreach (var item in ListaRutasArchivosCSS)
                        {
                            cssResolver.AddCssFile(item, true);
                        }
                        var hpc = new HtmlPipelineContext(new CssAppliersImpl(new XMLWorkerFontProvider()));
                        hpc.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(TagProcesador);
                        var HtmlPipeline = new HtmlPipeline(hpc, new PdfWriterPipeline(document, pdfWriter));
                        var pipeline = new CssResolverPipeline(cssResolver, HtmlPipeline);
                        var worker = new XMLWorker(pipeline, true);
                        var XmlParser = new XMLParser(true, worker, Encoding.UTF8);
                        using (StringReader TxtXml = new StringReader(this.PlantillaPDF))
                        {
                            XmlParser.Parse(TxtXml);
                        }
                        document.Close();
                    }
                }
                this.ArchivoPDF = MEM.ToArray();
            }
        }
    }
}
