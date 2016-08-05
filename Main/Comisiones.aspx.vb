Imports Cls_Dll_Web
Imports System.Data
Imports System.Configuration
Imports System.Web.Script.Serialization
Imports System.Collections.Generic


Partial Class Main_Comisiones

    Inherits System.Web.UI.Page

    <System.Web.Services.WebMethod()>
    Public Shared Function GetOficiales(ByVal maspe_carne As String, ByVal ape_pat As String, ByVal ape_mat As String, ByVal nombres As String) As String


        Dim ObjOficiales As New Cls_Oficiales
        Dim varTablaOfi As DataSet
        Dim msg As String = ""
        varTablaOfi = ObjOficiales.ListarOficiales(maspe_carne, ape_pat, ape_mat, nombres)


        Dim firstTable As DataTable = varTablaOfi.Tables(0)

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
    Public Shared Function GetMovimientos(ByVal maspe_carne As String) As String
        Dim ObjMovimientos As New Cls_Oficiales
        Dim varTablaMov As DataSet
        Dim msg As String = ""


        varTablaMov = ObjMovimientos.ListarMovimientos(maspe_carne)

        Dim firstTable As DataTable = varTablaMov.Tables(0)

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
    Public Shared Function GetDetallesMovimientos(ByVal rcontrol_transa As String) As String
        Dim ObjDetalleMovimientos As New Cls_Oficiales
        Dim varTablaDetMov As DataSet
        Dim msg As String = ""
        varTablaDetMov = ObjDetalleMovimientos.ListarDetalleMovimientos(rcontrol_transa)

        Dim firstTable As DataTable = varTablaDetMov.Tables(0)

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
