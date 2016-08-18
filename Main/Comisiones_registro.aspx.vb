
Imports Cls_Dll_Web
Imports System.Data
Imports System.Configuration
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Partial Class Main_Comisiones_registro
    Inherits System.Web.UI.Page
    Private Obj_Load As New Cls_Registro
    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If Not Page.IsPostBack Then

            Obj_Load.Tipos_Documentos(ddltipodoc)
            ddltipodoc.DataTextField = "VALOR"
            ddltipodoc.DataValueField = "ID"
            ddltipodoc.DataBind()

            Obj_Load.Tipo_Movimiento(ddltipomov)
            ddltipomov.DataTextField = "VALOR"
            ddltipomov.DataValueField = "ID"
            ddltipomov.DataBind()

            Obj_Load.Tipo_Control(ddltipcon)
            ddltipcon.DataTextField = "VALOR"
            ddltipcon.DataValueField = "ID"
            ddltipcon.DataBind()

            Obj_Load.Listado_pais(ddlpais)
            ddlpais.DataTextField = "VALOR"
            ddlpais.DataValueField = "ID"
            ddlpais.DataBind()

            Obj_Load.Listado_departamento(ddldepartamento)
            ddldepartamento.DataTextField = "VALOR"
            ddldepartamento.DataValueField = "D"
            ddldepartamento.DataBind()

        End If
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function GetProvincia(ByVal d As String) As String

        Dim ObjListarP As New Cls_Registro
        Dim varTablaProvincia As DataSet
        Dim msg As String = ""

        varTablaProvincia = ObjListarP.Listado_provincia(d)

        Dim firstTable As DataTable = varTablaProvincia.Tables(0)

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))()
        Dim row As Dictionary(Of String, Object)
        For Each dr As DataRow In firstTable.Rows
            row = New Dictionary(Of String, Object)()
            For Each col As DataColumn In firstTable.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next

        Return serializer.Serialize(rows)

    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function GetCiudad(ByVal d As String, ByVal p As String) As String
        Dim ObjListarC As New Cls_Registro
        Dim varTablaCiudad As DataSet
        Dim msg As String = ""

        varTablaCiudad = ObjListarC.Listado_ciudad(d, p)

        Dim firstTable As DataTable = varTablaCiudad.Tables(0)

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim rows As New List(Of Dictionary(Of String, Object))()
        Dim row As Dictionary(Of String, Object)
        For Each dr As DataRow In firstTable.Rows
            row = New Dictionary(Of String, Object)()
            For Each col As DataColumn In firstTable.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next

        Return serializer.Serialize(rows)

    End Function

End Class
