
Imports System.Data
Imports Cls_Dll_Web
Imports System.Web.UI
Partial Class Main_Comisiones_registro
    Inherits System.Web.UI.Page
    Private Obj_Load As New Cls_Registro

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If Not Page.IsPostBack Then
            Obj_Load.Tipos_Documentos(dptipodoc)
            dptipodoc.DataTextField = "VALOR"
            dptipodoc.DataValueField = "ID"
            dptipodoc.DataBind()
        End If
    End Sub
End Class
