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
    Public Function Rpt_Movimientos(ByVal maspe_carne As String, ByVal rcontrol_transa As String, ByVal id_referencia As Integer) As DataTable
        SqlTexto = "[sp_listado_maspe] @mssql = 13, @maspe_carne='" & maspe_carne & "', @RCONTROL_TRANSA='" & rcontrol_transa & "', @ID_DOCREFERENCIA=" & id_referencia & ""
        Dim TablaRpt As DataTable = ObjRegNeg.ConsultaxParametros(SqlTexto).Tables(0)
        Return TablaRpt
    End Function
    Public Function Insertar_DocumentoReferencia(ByVal comision_id As Integer, ByVal fecha_referencia As String, ByVal documento_referencia As String, ByVal ope_registra As String, ByVal ip As String) As String
        Try
            SqlTexto = "[Control].[sp_Registro] @mssql=8,@comision_id=" & comision_id & ",@fecha_referencia='" & fecha_referencia & "',@documento_referencia='" & documento_referencia & "',@ope_registra='" & ope_registra & "',@estacion_registra='" & ip & "'"
            Return ObjRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString
        Catch ex As Exception
            Throw New Exception("Cls_Medidas :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
    Public Function Insertar_Comision(ByVal maspe_carne As String, ByVal ttidocu As String, ByVal nro_doc As String, ByVal siglas As String, ByVal fecha_doc As String _
                                      , ByVal tipo_control As Integer, ByVal control_doc As String, ByVal tipo_movimiento As Integer, ByVal observacion As String, ByVal fecha_inicio As String _
                                      , ByVal fecha_termino As String, ByVal ubigeo_d As String, ByVal tpais_cod As String, ByVal ope_registra As String, ByVal estacion_registra As String) As String
        Try
            SqlTexto = "[Control].[sp_Registro] @mssql=4,@maspe_carne='" & maspe_carne & "',@ttidocu='" & ttidocu & "',@nro_doc='" & nro_doc & "',@siglas='" & siglas & "',@fecha_doc='" & fecha_doc & "', " &
                        "@tipo_control='" & tipo_control & "',@control_doc='" & control_doc & "',@tipo_movimiento='" & tipo_movimiento & "',@observacion='" & observacion & "',@fecha_inicio='" & fecha_inicio & "', " &
                        "@fecha_termino='" & fecha_termino & "',@ubigeo_d='" & ubigeo_d & "',@tpais_cod='" & tpais_cod & "',@ope_registra='" & ope_registra & "',@estacion_registra='" & estacion_registra & "'"

            Return ObjRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString
        Catch ex As Exception
            Throw New Exception("Cls_Medidas :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
    Public Function Insertar_ComisionDetalle(ByVal comision_id As String, ByVal fecha_origen As String, ByVal ttip1control As String, ByVal tipo_control As String, ByVal observacion As String _
                                      , ByVal ope_registra As String, ByVal estacion_registra As String) As String
        Try
            SqlTexto = "[Control].[sp_Registro] @mssql=5,@comision_id='" & comision_id & "',@fecha_origen='" & fecha_origen & "',@ttip1control='" & ttip1control & "',@tipo_control='" & tipo_control & "',@observacion='" & observacion & "', " &
                        "@ope_registra='" & ope_registra & "',@estacion_registra='" & estacion_registra & "'"

            Return ObjRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString
        Catch ex As Exception
            Throw New Exception("Cls_Medidas :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
    Public Sub Tipo_ControlMov(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 15"
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
