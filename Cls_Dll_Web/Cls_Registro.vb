Public NotInheritable Class Cls_Registro
    Private Shared ObjRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Public Sub Tipos_Documentos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 6"
        ds = ObjRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

End Class
