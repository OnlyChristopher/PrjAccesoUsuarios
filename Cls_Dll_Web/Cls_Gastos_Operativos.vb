Public Class Cls_Gastos_Operativos
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_gastos_operativos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select * from gastos_operativos where id_empresa = " & Id_Empresa & " order by id_gasto;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_gasto() As Integer
        SqlTexto = "select ifnull(max(id_gasto),0) + 1 from gastos_operativos where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_gastos_operativos(ByVal id_gasto As Integer, ByVal descripcion As String, ByVal costo As Double, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_gasto", gasto, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into gastos_operativos (id_gasto, descripcion, costo, id_usuario, ip, id_empresa) values " & _
                       "(" & id_gasto & ", '" & descripcion.Replace("'", "''") & "', " & costo & ", " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_gastos_operativos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_gastos_operativos(ByVal id_gasto As String, ByVal descripcion As String, ByVal costo As Double, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_gasto", id_gasto, gasto, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update gastos_operativos set descripcion = '" & descripcion.Replace("'", "''") & "', " & _
                              "costo = " & costo & ", " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_gasto = " & id_gasto & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_gastos_operativos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_gastos_operativos(ByVal id_gasto As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from proforma_gastos_operativos where id_empresa = " & Id_Empresa & " and id_gasto = " & id_gasto & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, la proforma Nro. " & ds.Tables(0).Rows(0)("id_proforma").ToString & ", se genero con este gasto "
            End If

            SqlTexto = "delete from gastos_operativos where id_empresa = " & Id_Empresa & " and id_gasto = " & id_gasto & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_gastos_operativos :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Sub Listar_Gastos_Operativos_Detalle(ByVal obj As Object, ByVal anua As String, ByVal id_gasto As Integer)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_mes, b.id_gasto, b.anua, a.mes, ifnull(b.costo,0) as costo " & _
                   "from meses a " & _
                   "left join gastos_operativos_detalle b on b.mes = a.id_mes and b.id_empresa = " & Id_Empresa & " and b.anua = '" & anua & "' and b.id_gasto = " & id_gasto & " " & _
                   "order by a.id_mes;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Elimina_Gastos_operativo_detalle(ByVal id_gasto As Integer, ByVal anua As String)
        Try
            SqlTexto = "delete from gastos_operativos_detalle where id_empresa = " & Id_Empresa & " and id_gasto = " & id_gasto & " and anua = '" & anua & "';"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_gastos_operativos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Insertar_gastos_operativos_detalle(ByVal id_gasto As Integer, ByVal anua As String, ByVal mes As String, ByVal costo As Double, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "insert into gastos_operativos_detalle (id_gasto, anua, mes, costo, id_usuario, ip, id_empresa) values " & _
                       "(" & id_gasto & ", '" & anua & "', " & mes & ", " & costo & ", " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_gastos_operativos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Actualiza_Total_Gastos_Operativos(ByVal id_gasto As Integer, ByVal costo_promedio As Double)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            'SqlTexto = "update gastos_operativos set costo = (select ifnull(costo,0) as costo from gastos_operativos_detalle where id_empresa = " & Id_Empresa & " and id_gasto = " & id_gasto & " and anua = '" & anua & "' and mes = " & id_mes & ") " & _
            '"where id_empresa = " & Id_Empresa & " and id_gasto = " & id_gasto & ";"

            SqlTexto = "update gastos_operativos set costo = " & costo_promedio & " " & _
                       "where id_empresa = " & Id_Empresa & " and id_gasto = " & id_gasto & ";"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_gastos_operativos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

End Class
