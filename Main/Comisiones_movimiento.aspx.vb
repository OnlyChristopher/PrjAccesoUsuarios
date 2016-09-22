Imports Cls_Dll_Web
Imports System.Data
Partial Class Main_Comision_movimiento
    Inherits System.Web.UI.Page
    Private objCbo As New Cls_Registro

    Private Sub frmmovimientos_Load(sender As Object, e As EventArgs) Handles frmmovimientos.Load
        If Not Page.IsPostBack Then
            objCbo.Tipo_ControlMov(ddltiporeg)
            ddltiporeg.DataTextField = "VALOR"
            ddltiporeg.DataValueField = "ID"
            ddltiporeg.DataBind()


            objCbo.Tipo_Control(ddltipocon)
            ddltipocon.DataTextField = "VALOR"
            ddltipocon.DataValueField = "ID"
            ddltipocon.DataBind()

            HttpContext.Current.Session("rcontrol_trans") = Request.QueryString("rcontrol_trans")
        End If
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function SetComisionDetalle(ByVal fecha_origen As String, ByVal ttip1control As String, ByVal tipo_control As String, ByVal observacion As String) As String
        Dim ObjComision As New Cls_Registro
        Dim ope_registra As String = HttpContext.Current.Session("OPE_COD")
        Dim ip As String = HttpContext.Current.Session("HOST")
        Dim comision_id = HttpContext.Current.Session("rcontrol_trans")
        Return ObjComision.Insertar_ComisionDetalle(comision_id, fecha_origen, ttip1control, tipo_control, observacion, ope_registra, ip)

    End Function
End Class
