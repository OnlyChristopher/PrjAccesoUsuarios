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

    Public Sub Tipo_Movimiento(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 7"
        ds = ObjRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Tipo_Control(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 8"
        ds = ObjRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listado_pais(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 9"
        ds = ObjRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Listado_departamento() As DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 10"
        Return ObjRegNeg.ConsultaxParametros(SqlTexto)
    End Function
    Public Function Listado_provincia(ByVal d As String) As DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 11, @departamento='" & d & "'"
        Return ObjRegNeg.ConsultaxParametros(SqlTexto)
    End Function
    Public Function Listado_ciudad(ByVal d As String, ByVal p As String) As DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 12, @departamento='" & d & "', @provincia='" & p & "'"
        Return ObjRegNeg.ConsultaxParametros(SqlTexto)
    End Function
End Class
