Imports System.Data
Imports System.Data.SqlClient
Imports system.IO

Partial Class Main
    Inherits System.Web.UI.Page
    Private Estado_Id As Integer
    Private Usuario_Id_Mod As Integer
    Private Usuario_Id As Integer
    Private Usuario_Seleccionado As Integer
    Private Ip As String = ""
    Public Event PageIndesChanging As GridViewPageEventHandler

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Ip = System.Net.Dns.GetHostName

        If Session("OPE_NOMBRES") Is Nothing Then
            CerrarSession()
        End If

        If Not Page.IsPostBack Then
            lblusuario.Text = IIf(Session("OPE_NOMBRES").ToString.Trim = "", Session("OPE_NOMBRES").ToString.ToUpper.Trim, Session("OPE_NOMBRES").ToString.ToUpper.Trim) 
            lblgrado.Text = IIf(Session("TGRAD_DES").ToString.Trim = "", Session("TGRAD_DES").ToString.ToUpper.Trim, Session("TGRAD_DES").ToString.ToUpper.Trim)
            lblunidad.Text = IIf(Session("COD_UNI").ToString.Trim = "", Session("COD_UNI").ToString.ToUpper.Trim, Session("COD_UNI").ToString.ToUpper.Trim)
            lblequipo.Text = Ip
        End If
    End Sub
    Private Function HostIP(ByVal mStrHost As String) As String
        Dim IpCliente As String
        IpCliente = Request.ServerVariables("HTTP_X_FORWARDED_FOR") ' se chequea si hay un proxy
        If IpCliente = "" Then IpCliente = Request.ServerVariables("REMOTE_ADDR")
        Return IpCliente
    End Function
    Private Function Cadena_Accesos(ByVal Dgv As GridView) As String
        Dim Cadena As String = ""
        For Each row As GridViewRow In Dgv.Rows
            ' Access the CheckBox
            Dim cb As CheckBox = row.FindControl("ChkAcceso")
            If cb.Checked Then
                Cadena = Cadena & row.Cells(0).Text & ","
            End If
        Next
        If Cadena.Trim <> "" Then Cadena = Left(Cadena, Len(Cadena) - 1)
        Return Cadena
    End Function

    Private Sub Check_Estados(ByVal CheckEstado As Boolean, ByVal GrvDatos As GridView)
        ' Iterate through the Products.Rows property
        For Each row As GridViewRow In GrvDatos.Rows
            ' Access the CheckBox
            Dim cb As CheckBox = row.FindControl("ChkAcceso")
            If cb IsNot Nothing Then
                cb.Checked = CheckEstado
            End If
        Next
    End Sub

    Private Sub MessageBox(ByVal msje As String)
        Page.ClientScript.RegisterStartupScript(Page.GetType, "Mensaje", "<script language ='javascript'>alert('" & msje & "');</script> ")
    End Sub

    Private Sub lbtncerrar_Click(sender As Object, e As EventArgs) Handles lbtncerrar.Click
        CerrarSession()
    End Sub
    Private Sub CerrarSession()
        Session.Abandon()
        Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        FormsAuthentication.SignOut()
        Response.Redirect("Login.aspx")
    End Sub

End Class

