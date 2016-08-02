Public Class Cls_Clientes
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Clientes(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select * from clientes where id_empresa = " & Id_Empresa & " order by id_cliente;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_Id_Cliente() As Integer
        SqlTexto = "select ifnull(max(id_cliente),0) + 1 from clientes where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Clientes(ByVal id_cliente As Integer, ByVal cliente As String, ByVal nombre As String, ByVal direccion As String, _
                                 ByVal telefono As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_Clientes", cliente, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into clientes (id_cliente, cliente, nombre, direccion, telefono, id_usuario, ip, id_empresa) values " & _
                       "(" & id_cliente & ",'" & cliente.Replace("'", "''") & "','" & nombre.Replace("'", "''") & "','" & direccion.Replace("'", "''") & "','" & telefono.Replace("'", "''") & "'," & id_usuario & ",'" & Ip & "', " & Id_Empresa & " );"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            ' insertamos producto debido al cambio solicitado por vicente en el cual no se elige el producto en la proforma
            SqlTexto = "select ifnull(max(id_producto),0) + 1 from productos where id_empresa = " & Id_Empresa & "; "
            Dim Id_Producto = Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)

            SqlTexto = "insert into productos (id_producto, producto, descripcion, id_cliente, id_usuario, ip, id_empresa) values " & _
                       "(" & Id_Producto & ",'','Producto'," & id_cliente & "," & id_usuario & ",'" & Ip & "', " & Id_Empresa & ");"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Clientes :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Clientes(ByVal id_cliente As String, ByVal cliente As String, ByVal nombre As String, ByVal direccion As String, _
                                 ByVal telefono As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Clientes", id_cliente, cliente, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update clientes set cliente = '" & cliente.Replace("'", "''") & "', " & _
                              "nombre = '" & nombre.Replace("'", "''") & "', " & _
                              "direccion = '" & direccion.Replace("'", "''") & "', " & _
                              "telefono = '" & telefono.Replace("'", "''") & "', " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_cliente = " & id_cliente & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Clientes :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Clientes(ByVal id_cliente As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from proforma where id_empresa = " & Id_Empresa & " and id_cliente = " & id_cliente & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, la proforma Nro. " & ds.Tables(0).Rows(0)("id_proforma").ToString & ", contiene a este Cliente "
            End If

            SqlTexto = "delete from productos where id_empresa = " & Id_Empresa & " and id_cliente = " & id_cliente & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from clientes where id_empresa = " & Id_Empresa & " and id_cliente = " & id_cliente & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Clientes :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
End Class
