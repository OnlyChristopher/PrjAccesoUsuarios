Imports System.Data
Imports Cls_Dll_Web
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Partial Class Main_Comisiones_detalle
    Inherits System.Web.UI.Page
    <System.Web.Services.WebMethod()>
    Public Shared Function SetVariablesRpt(ByVal maspe_carne As String, ByVal nro_transa As String) As String
        Dim Obj_Load As New Cls_Registro
        'Dim Server As New System.Web.UI.Page
        Dim rpt As New ReportDocument
        rpt.Load(System.Web.HttpContext.Current.Server.MapPath("../Reportes/RptMov.rpt"))
        rpt.SetDataSource(Obj_Load.Rpt_Movimientos(maspe_carne, nro_transa))
        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Server.MapPath("../Reportes/Reporte.pdf"))
        Return True
    End Function

End Class
