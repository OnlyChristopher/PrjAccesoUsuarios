Public Class Cls_Uusarios_Accesos
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Usuarios(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_usuario, a.cod_usuario, a.password, a.id_persona, b.nombres, a.fecha_creacion, a.ip, a.tipo_usuario, " & _
                           "case tipo_usuario when 0 then 'Personal' when 1 then 'Supervisor' end as tipo_usuario_desc " & _
                   "from usuarios a " & _
                   "left join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
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

    Public Sub Listar_Personal(ByVal obj As Object)
        Dim ds As New Data.DataSet
        'ds = ObjsRegNeg.ConsultaxParametros("select id_persona, nombres from personas where id_empresa = " & Id_Empresa & " and id_persona not in (select id_persona from usuarios where id_empresa = " & Id_Empresa & " and tipo_usuario=0) order by id_persona")
        ds = ObjsRegNeg.ConsultaxParametros("select id_persona, nombres from personas where id_empresa = " & Id_Empresa & " order by id_persona;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_usuario() As Integer
        SqlTexto = "select ifnull(max(id_usuario),0) + 1 from usuarios where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Function Insertar_Usuario(ByVal id_usuario As Integer, ByVal cod_usuario As String, ByVal password As String, ByVal id_persona As String, ByVal Tipo_Usuario As Integer) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from usuarios where id_empresa = " & Id_Empresa & " and cod_usuario = '" & cod_usuario & "';"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "El codigo de personal " & cod_usuario & " ya se encuentra registrado"
            End If

            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            Dim hash As String = Cls_Encrypta.CodificarPassword(String.Concat(cod_usuario.ToUpper, password))
            SqlTexto = "insert into usuarios (id_usuario, cod_usuario, password, id_persona, ip, id_empresa, tipo_usuario) values " & _
                       "(" & id_usuario & ",'" & cod_usuario & "','" & hash & "'," & id_persona & ",'" & Ip & "', " & Id_Empresa & ", " & Tipo_Usuario & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "Exito"
        Catch ex As Exception
            Throw New Exception("Cls_Usuarios_Accesos :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Sub Modificar_Usuario(ByVal id_usuario As String, ByVal cod_usuario As String, ByVal id_persona As String, ByVal Tipo_Usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            SqlTexto = "update usuarios set cod_usuario = '" & cod_usuario & "', " & _
                               "id_persona = " & id_persona & ", " & _
                               "Ip = '" & Ip & "', " & _
                               "tipo_usuario = " & Tipo_Usuario & " " & _
                       "where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario & "; "

            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Usuarios", id_usuario, cod_usuario, descripcion, id_persona, id_usuario, Ip)
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Usuarios_Accesos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Usuario(ByVal id_usuario As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from tareasrealizadas where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario & "; "
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, el usuario realizo cambios en la tabla tareasrealizadas"
            End If

            SqlTexto = "select * from proforma where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario & "; "
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, el usuario realizo cambios en la tabla proformas"
            End If

            SqlTexto = "delete from accesos where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "delete from usuarios where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Usuarios_Accesos :" & vbCrLf & ex.Message, ex)
        End Try
        'ObjsRegNeg.InsertarxActualizaxBorra("Elimina_Usuarios", id_usuario)
    End Function

    Public Function Verifica_Clave(ByVal id_usuario As String, ByVal cod_usuario As String, ByVal password As String) As Boolean
        Dim ds As New Data.DataSet
        Dim hash As String = Cls_Encrypta.CodificarPassword(String.Concat(cod_usuario.ToUpper, password))
        ds = ObjsRegNeg.ConsultaxParametros("select * from usuarios where id_empresa = " & Id_Empresa & " and id_usuario = '" & id_usuario & "' and password = '" & hash & "';")
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
        ds.Dispose()
    End Function

    Public Sub Cambiar_Clave(ByVal id_usuario As String, ByVal password As String, ByVal cod_usuario As String)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            Dim hash As String = Cls_Encrypta.CodificarPassword(String.Concat(cod_usuario.ToUpper, password))
            SqlTexto = "update usuarios set password = '" & hash & "', " & _
                               "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario & "; "

            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Usuarios", id_usuario, cod_usuario, descripcion, id_persona, id_usuario, Ip)
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Usuarios_Accesos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Crear_usuario_ADMIN(ByVal Id_Usuario As Integer, ByVal Cod_Usuario As String, ByVal Password As String, ByVal Tipo_Usuario As Integer, ByVal id_nueva_empresa As Integer) As String
        'Tipo_Usuario: 0-PERSONAL / 1-SUPERVISOR
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from usuarios where id_empresa = " & id_nueva_empresa & " and cod_usuario = '" & Cod_Usuario & "';"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "El codigo de usuario " & Cod_Usuario & " ya se encuentra registrado"
            End If

            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            Dim hash As String = Cls_Encrypta.CodificarPassword(String.Concat(Cod_Usuario.ToUpper, Password))
            SqlTexto = "insert into usuarios (id_usuario, cod_usuario, password, ip, id_empresa, tipo_usuario, id_persona) values " & _
                       "(" & Id_Usuario & ",'" & Cod_Usuario & "','" & hash & "','" & Ip & "', " & id_nueva_empresa & ", " & Tipo_Usuario & ", 0);"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "insert into accesos (id_usuario, id_modulo, id_programa, acceso, agrega, modifica, elimina, imprime, supervisor, ip, id_empresa) " & _
                        "select " & Id_Usuario & ", id_modulo, id_programa, 1, 1, 1, 1, 1, 1, '" & Ip & "', " & id_nueva_empresa & " from programas;"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            Return "Exito"
        Catch ex As Exception
            Throw New Exception("Cls_Usuarios_Accesos :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

End Class
