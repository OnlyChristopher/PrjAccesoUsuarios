Imports Cls_Dll_Web
Imports System.Data
Imports System.Configuration
Imports System.Globalization

Partial Class Reportes_RptReferencia
    Inherits System.Web.UI.Page

    Public Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        HttpContext.Current.Session("rcontrol_trans") = Request.QueryString("rcontrol_trans")

    End Sub



    <System.Web.Services.WebMethod()>
    Public Shared Function SetDocumentoReferencia(ByVal fecha_referencia As String, ByVal documento_referencia As String) As String
        Dim ObjRegistro As New Cls_Registro
        'Dim varTablaDocRef As DataSet
        Dim ope_registra As String = HttpContext.Current.Session("OPE_COD")
        Dim comision_id As Integer = HttpContext.Current.Session("rcontrol_trans")
        Dim ip As String = HttpContext.Current.Session("HOST")

        Const msg As String = "Registro Correcto"

        ObjRegistro.Insertar_DocumentoReferencia(comision_id, fecha_referencia, documento_referencia, ope_registra, ip)
        Return msg
    End Function

End Class
