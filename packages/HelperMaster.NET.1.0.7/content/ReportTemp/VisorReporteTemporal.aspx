<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorReporteTemporal.aspx.cs" Inherits="HelperMaster.NET.MVC.VisorReporteTemporal" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title></title>
    </head>
    <body style="margin: 0px; padding: 0px;">
        <form id="form1" runat="server">
            <div>
                <asp:ScriptManager ID="Script1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="VisorReporte" runat="server"></rsweb:ReportViewer>
            </div>
        </form>
    </body>
</html>
