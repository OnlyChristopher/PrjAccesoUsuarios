Imports Cls_Dll_Web
Imports System.Data
Imports System.Configuration
Partial Class Login
    Inherits System.Web.UI.Page

    <System.Web.Services.WebMethod()>
    Public Shared Function GetUsuario(ByVal user As String, ByVal pass As String) As String
        Dim ObjUsuario As New Cls_Acceso
        Dim varTablaUsu As DataSet
        Dim msg As String = ""
        varTablaUsu = ObjUsuario.ExisteUsuario(user, pass)

        If varTablaUsu.Tables(0).Rows.Count = 1 Then
            msg = "Ingreso"
            HttpContext.Current.Session("OPE_COD") = varTablaUsu.Tables(0).Rows(0)("OPE_COD").ToString.Trim
            HttpContext.Current.Session("OPE_NOMBRES") = varTablaUsu.Tables(0).Rows(0)("OPE_NOMBRES").ToString.Trim
            HttpContext.Current.Session("TGRAD_DES") = varTablaUsu.Tables(0).Rows(0)("TGRAD_DES").ToString.Trim
            HttpContext.Current.Session("TCARGO_DES") = varTablaUsu.Tables(0).Rows(0)("TCARGO_DES").ToString.Trim
            HttpContext.Current.Session("TSITUA_DES") = varTablaUsu.Tables(0).Rows(0)("TSITUA_DES").ToString.Trim
            HttpContext.Current.Session("PERF_COD") = varTablaUsu.Tables(0).Rows(0)("PERF_COD").ToString.Trim
            HttpContext.Current.Session("COD_UNI") = varTablaUsu.Tables(0).Rows(0)("COD_UNI").ToString.Trim
        Else
            msg = "Error"
        End If

        Return msg
    End Function
    Private Sub CerrarSession()
        Session.Abandon()
        Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        FormsAuthentication.SignOut()
        Response.Redirect("Login.aspx")
    End Sub
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("OPE_NOMBRES") <> "" Then
            Response.Redirect("Main.aspx")
        End If
    End Sub


End Class
