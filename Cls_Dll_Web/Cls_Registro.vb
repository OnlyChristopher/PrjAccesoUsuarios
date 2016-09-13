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
    Public Function Rpt_Movimientos(ByVal maspe_carne As String, ByVal rcontrol_transa As String) As DataTable
        SqlTexto = "[sp_listado_maspe] @mssql = 13, @maspe_carne='" & maspe_carne & "', @RCONTROL_TRANSA='" & rcontrol_transa & "'"
        Dim TablaRpt As DataTable = ObjRegNeg.ConsultaxParametros(SqlTexto).Tables(0)
        Return TablaRpt
    End Function
    Public Sub Insertar_DocumentoReferencia(ByVal comision_id As Integer, ByVal fecha_referencia As String, ByVal documento_referencia As String, ByVal ope_registra As String, ByVal ip As String)
        Try

            SqlTexto = "[Control].[sp_Registro] @mssql=8,@comision_id=" & comision_id & ",@fecha_referencia='" & fecha_referencia & "',@documento_referencia='" & documento_referencia & "',@ope_registra='" & ope_registra & "',@estacion_registra='" & ip & "'"

            ObjRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Medidas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub
End Class
