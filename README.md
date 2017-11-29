# HelperMaster.NET

Contiene métodos ya probados para ser utilizados en proyectos C#.NET (la rueda ya está inventada) para Visual Studio 2017 (recomendado).

A continuación, se mostrará documentación según sus tópico en ser aplicado lo necesitado.

## Reportería

La librería implementa ReportViewer en marco de trabajo MVC 5 en adelante.

Para utilizar la herramienta, se muestra a continuación código fuente de ejemplo.

En el controlador:

```csharp
public ActionResult Index()
{
	Random R = new Random();

	#region Llamada a Datos seteados por código fuente.

		ReportViewer Reporte = new ReportViewer();

	DatosPrueba Datos = new DatosPrueba();
	var Fila = Datos.Tables["Prueba"].NewRow();
	Fila["ID"] = 1;
	Fila["Descripcion"] = "Descripción de prueba N°" + R.Next();
	Datos.Tables["Prueba"].Rows.Add(Fila);

	#endregion

		#region Enlazar Datos llenados al reporte.

		Reporte.ProcessingMode = ProcessingMode.Local;
	Reporte.SizeToReportContent = true;
	Reporte.Width = Unit.Percentage(100);
	Reporte.Height = Unit.Percentage(100);
	Reporte.LocalReport.ReportPath = Server.MapPath("~/Models/Reportes/ReporteEjemplo.rdlc");

	ReportDataSource dsDatosPrueba = new ReportDataSource("TestDatos", Datos.Tables["Prueba"]);

	Reporte.LocalReport.DataSources.Add(dsDatosPrueba);

	#endregion

		ViewData["Reporte"] = Reporte;
	return View();
}
```

La clase DatosPrueba, es un conjunto de datos XSD, para lo cual, el archivo de reporte de extensión RDLC lo lee como un origen de datos.

En la vista:

```csharp
@using HelperMaster.NET.MVC;
@using Microsoft.Reporting.WebForms;

<h2>Ejemplo Reporte</h2>

<div class="embed-responsive embed-responsive-16by9">
    @Html.VisorReporte((ReportViewer)ViewData["Reporte"], new { @class = "embed-responsive-item" })
</div>
```

Se recomienda encarecidamente utilizar BootStrap.
