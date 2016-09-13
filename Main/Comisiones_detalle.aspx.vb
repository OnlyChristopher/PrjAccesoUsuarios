Imports System.Data
Imports Cls_Dll_Web
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Partial Class Main_Comisiones_detalle
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim objDatos As New Cls_Oficiales
        Dim maspe_carne As String
        Dim varTablaOfi As New DataSet
        Dim datos() As String

        maspe_carne = Request.QueryString("maspe_carne")
        varTablaOfi = objDatos.DatosOficial(maspe_carne)

        If varTablaOfi.Tables(0).Rows.Count > 0 Then
            datos = Split(varTablaOfi.Tables(0).Rows(0)("Datos"), ",")
            For i = 0 To datos.Length - 1
                lbldatos1.Text = datos(0).ToString()
                lbldatos2.Text = datos(1).ToString()
                lbldatos3.Text = datos(2).ToString()
                lbldatos4.Text = datos(3).ToString()
                lbldatos5.Text = datos(4).ToString()
                lbldatos6.Text = datos(5).ToString()
            Next
        End If

    End Sub

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
