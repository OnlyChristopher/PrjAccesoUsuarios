
Imports Cls_Dll_Web
Imports System.Data
Imports System.Configuration
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Partial Class Main_Comisiones_registro
    Inherits System.Web.UI.Page
    Private Obj_Load As New Cls_Registro
    Private Sub frmregistrocomision_Load(sender As Object, e As EventArgs) Handles frmregistrocomision.Load
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

        End If
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function GetDepartamento() As String

        Dim ObjListarD As New Cls_Registro
        Dim varTablaDepartamento As DataSet
        Dim msg As String = ""

        varTablaDepartamento = ObjListarD.Listado_departamento()

        Dim firstTable As DataTable = varTablaDepartamento.Tables(0)

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
    <System.Web.Services.WebMethod()>
    Public Shared Function SetComision(ByVal maspe_carne As String, ByVal ttidocu As String, ByVal nro_doc As String, ByVal siglas As String, ByVal fecha_doc As String _
                                      , ByVal tipo_control As Integer, ByVal control_doc As String, ByVal tipo_movimiento As Integer, ByVal observacion As String, ByVal fecha_inicio As String _
                                      , ByVal fecha_termino As String, ByVal ubigeo_d As String, ByVal tpais_cod As String) As String
        Dim ObjComision As New Cls_Registro
        Dim ope_registra As String = HttpContext.Current.Session("OPE_COD")
        Dim ip As String = HttpContext.Current.Session("HOST")
        Return ObjComision.Insertar_Comision(maspe_carne, ttidocu, nro_doc, siglas, fecha_doc, tipo_control, control_doc, tipo_movimiento, observacion, fecha_inicio, fecha_termino, ubigeo_d, tpais_cod, ope_registra, ip)

    End Function

End Class
