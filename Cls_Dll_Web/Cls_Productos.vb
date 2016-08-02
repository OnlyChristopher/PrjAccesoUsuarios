Public Class Cls_Productos
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Productos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_producto, a.producto,a.descripcion,a.id_cliente,a.id_usuario,a.fecha_creacion,a.ip,b.cliente " & _
                   "from productos a " & _
                   "join clientes b on b.id_empresa = a.id_empresa and b.id_cliente = a.id_cliente " & _
                   "where a.id_empresa = " & Id_Empresa & "; "
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Clientes(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_cliente, cliente from clientes where id_empresa = " & Id_Empresa & " order by cliente;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_Id_Producto() As Integer
        SqlTexto = "select ifnull(max(id_producto),0) + 1 from productos where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Producto(ByVal id_producto As Integer, ByVal producto As String, ByVal descripcion As String, ByVal id_cliente As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            SqlTexto = "insert into productos (id_producto, producto, descripcion, id_cliente, id_usuario, ip, id_empresa) values " & _
                       "(" & id_producto & ",'" & producto.Replace("'", "''") & "','" & descripcion.Replace("'", "''") & "'," & id_cliente & "," & id_usuario & ",'" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Productos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Producto(ByVal id_producto As String, ByVal producto As String, ByVal descripcion As String, ByVal id_cliente As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            SqlTexto = "update productos set producto = '" & producto.Replace("'", "''") & "', " & _
                               "descripcion = '" & descripcion.Replace("'", "''") & "', " & _
                               "id_cliente = " & id_cliente & ", " & _
                               "id_usuario = " & id_usuario & ", " & _
                               "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_producto = " & id_producto & "; "

            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Productos", id_producto, producto, descripcion, id_cliente, id_usuario, Ip)
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Productos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Producto(ByVal id_producto As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from proforma where id_empresa = " & Id_Empresa & " and id_producto = " & id_producto & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, la proforma Nro. " & ds.Tables(0).Rows(0)("id_proforma").ToString & ", contiene a este producto "
            End If

            SqlTexto = "delete from productos where id_empresa = " & Id_Empresa & " and id_producto = " & id_producto & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Productos :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
End Class
