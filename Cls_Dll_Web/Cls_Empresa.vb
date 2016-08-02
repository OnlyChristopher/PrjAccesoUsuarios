Public Class Cls_Empresa
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Empresa(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select * from empresa order by id_empresa")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_empresa() As Integer
        SqlTexto = "select ifnull(max(id_empresa),0) + 1 from empresa"
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Empresa(ByVal id_empresa As Integer, ByVal empresa As String, ByVal nombre_logo As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_empresa", empresa, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into empresa (id_empresa, empresa, id_usuario, ip, nombre_logo) values " & _
                       "(" & id_empresa & ", '" & empresa.Replace("'", "''") & "', " & id_usuario & ", '" & Ip & "', '" & nombre_logo & "');"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_empresa :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Empresa(ByVal id_empresa As String, ByVal empresa As String, ByVal id_usuario As Integer, ByVal nombre_logo As String)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_empresa", id_empresa, empresa, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update empresa set empresa = '" & empresa.Replace("'", "''") & "', " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "', " & _
                              "nombre_logo = '" & nombre_logo & "' " & _
                       "where id_empresa = " & id_empresa & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_empresa :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Empresa(ByVal x_id_empresa As String) As String
        Try
            Dim ds As New Data.DataSet
            'SqlTexto = "select * from elementos where id_empresa = " & Id_Empresa & ";"
            'ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    Return "No se puede eliminar, el elemento " & ds.Tables(0).Rows(0)("elemento").ToString & " contiene a esta empresa, primero elimine el elemento"
            'End If

            SqlTexto = "delete from tareas where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from tareasrealizadas where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from productos where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from clientes where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from proyectos where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from tipoproyectos where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from proformalineaservicios where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from proformalineapersonal where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from proforma where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from categorias where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from fases where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from formatos where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from servicios where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from elementos where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from personas where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from horaspersonaformato where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from accesos where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from usuarios where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from empresa where id_empresa = " & x_id_empresa & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_empresa :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
End Class
