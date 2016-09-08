Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Net.NetworkInformation
Partial Class Main
    Inherits System.Web.UI.Page
    Private Ip As String = ""
    Private PC As String = ""
    Private Domanin_PC As String = ""


    Private Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Domanin_PC = System.Net.Dns.GetHostName
        Ip = System.Net.Dns.GetHostByName(Domanin_PC).AddressList(0).ToString()

        'System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString() 


        If Session("OPE_NOMBRES") Is Nothing Then
            CerrarSession()
        End If

        If Not Page.IsPostBack Then
            lblusuario.Text = IIf(Session("OPE_NOMBRES").ToString.Trim = "", Session("OPE_NOMBRES").ToString.ToUpper.Trim, Session("OPE_NOMBRES").ToString.ToUpper.Trim)
            lblgrado.Text = IIf(Session("TGRAD_DES").ToString.Trim = "", Session("TGRAD_DES").ToString.ToUpper.Trim, Session("TGRAD_DES").ToString.ToUpper.Trim)
            lblunidad.Text = IIf(Session("COD_UNI").ToString.Trim = "", Session("COD_UNI").ToString.ToUpper.Trim, Session("COD_UNI").ToString.ToUpper.Trim)
            lblequipo.Text = Ip
            lbldomain_user.Text = Domanin_PC

        End If
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

