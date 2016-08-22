Imports Cls_Dll_Web
Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Partial Class Reportes_RptContent
    Inherits System.Web.UI.Page
    Private Obj_Load As New Cls_Registro

    Private Sub Reportes_RptContent_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim rpt As New ReportDocument
        rpt.Load(Server.MapPath("~/Reportes/RptMov.rpt"))
        rpt.SetDataSource(Obj_Load.Rpt_Movimientos("00341499", "433809"))
        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.DataBind()
        CrystalReportViewer1.RefreshReport()
    End Sub
End Class
