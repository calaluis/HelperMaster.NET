using System;

namespace HelperMaster.NET.MVC
{
    public partial class VisorReporteTemporal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HtmlHelperMVC.Reporte.ID = VisorReporte.ID;

                VisorReporte.ProcessingMode = HtmlHelperMVC.Reporte.ProcessingMode;
                VisorReporte.SizeToReportContent = HtmlHelperMVC.Reporte.SizeToReportContent;
                VisorReporte.Width = HtmlHelperMVC.Reporte.Width;
                VisorReporte.Height = HtmlHelperMVC.Reporte.Height;
                VisorReporte.LocalReport.ReportPath = HtmlHelperMVC.Reporte.LocalReport.ReportPath;
                VisorReporte.LocalReport.DataSources.Clear();
                foreach (var item in HtmlHelperMVC.Reporte.LocalReport.DataSources)
                {
                    VisorReporte.LocalReport.DataSources.Add(item);
                }
                VisorReporte.LocalReport.Refresh();
            }
        }
    }
}