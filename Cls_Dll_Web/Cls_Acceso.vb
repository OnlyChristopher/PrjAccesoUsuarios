Public NotInheritable Class Cls_Acceso
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Enum Tipo_Acceso As Integer
        Acceso = 0
        Agrega = 1
        Modifica = 2
        Elimina = 3
        Imprime = 4
        Supervisor = 5
    End Enum

    Public Function ExisteUsuario(ByVal varUsu As String, ByVal varPas As String) As DataSet
        Dim hash As String = Cls_Encrypta.CodificarPassword(varPas)

        SqlTexto = "Sistema.sp_Login @mssql=1,@operador = '" & varUsu & "' , @password = '" & hash & "' "

        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Sub Listar_Usuarios(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_usuario, cod_usuario from usuarios where id_empresa = " & Id_Empresa & " order by id_usuario")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Modulos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_modulo, modulo from modulos order by id_modulo")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Accesos(ByVal id_usuario As Integer, ByVal id_modulo As Integer, ByVal obj As Object)
        Try
            Dim ds0 As New Data.DataSet
            Dim ds1 As New Data.DataSet
            Dim ds2 As New Data.DataSet
            Dim ds As New Data.DataSet

            ' elimina registros del temporal
            SqlTexto = "delete from accesos_tmp;"
            ds0 = ObjsRegNeg.ConsultaxParametros(SqlTexto)

            ' inserta la lista de programas al temporal
            SqlTexto = "insert into accesos_tmp (id_modulo, id_programa, programa) " & _
                       "select id_modulo, id_programa, programa from programas " & _
                       "where id_modulo = " & id_modulo & ";"
            ds1 = ObjsRegNeg.ConsultaxParametros(SqlTexto)

            ' actualiza los tipos de accesos al temporal
            SqlTexto = "update accesos_tmp a, accesos b set a.acceso = b.acceso, a.agrega = b.agrega, " & _
                                "a.modifica = b.modifica, a.elimina = b.elimina, " & _
                                "a.imprime = b.imprime, a.supervisor = b.supervisor " & _
                       "where b.id_empresa = " & Id_Empresa & " and b.id_usuario = " & id_usuario & " and a.id_modulo = b.id_modulo and a.id_programa = b.id_programa;"

            ds2 = ObjsRegNeg.ConsultaxParametros(SqlTexto)

            ' muestra el resultado del temporal
            SqlTexto = "select id_modulo, id_programa, programa, " & _
                            "case when acceso = 0 then 'False' else 'True' end as acceso, " & _
                            "case when agrega = 0 then 'False' else 'True' end as agrega, " & _
                            "case when modifica = 0 then 'False' else 'True' end as modifica, " & _
                            "case when elimina = 0 then 'False' else 'True' end as elimina, " & _
                            "case when imprime = 0 then 'False' else 'True' end as imprime, " & _
                            "case when supervisor = 0 then 'False' else 'True' end as supervisor " & _
                        "from accesos_tmp order by id_programa"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ' verificar que existan accesos sin acceso al modulo
                    ' accesos where acceso = 0 and (agrega + modifica + elimina + imprime + supervisor) > 0
                    obj.enabled = True
                Else
                    obj.enabled = Not True
                End If
                obj.DataSource = ds.Tables(0).DefaultView
                ds.Dispose()
            End If
        Catch ex As Exception
            Throw New Exception("Cls_Acceso :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Menu_Acceso_Usuario(ByVal id_usuario As Integer) As DataSet
        Dim ds As New Data.DataSet
        SqlTexto = "select a.*, b.programa from accesos a " &
                   "join programas b on b.id_modulo = a.id_modulo and b.id_programa = a.id_programa " &
                   "where a.id_empresa = " & Id_Empresa & " and a.id_usuario = " & id_usuario & " and a.acceso = 1 "
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Sub Insertar_Actualiza_Accesos(ByVal id_usuario As Integer, ByVal id_modulo As Integer, ByVal id_programa As Integer, ByVal index_columna_acces As Tipo_Acceso, ByVal valor As Integer)
        Try
            Dim ds As New Data.DataSet
            Dim Campo As String = ""
            Select Case index_columna_acces
                Case Tipo_Acceso.Acceso : Campo = "acceso"
                Case Tipo_Acceso.Agrega : Campo = "agrega"
                Case Tipo_Acceso.Modifica : Campo = "modifica"
                Case Tipo_Acceso.Elimina : Campo = "elimina"
                Case Tipo_Acceso.Imprime : Campo = "imprime"
                Case Tipo_Acceso.Supervisor : Campo = "supervisor"
            End Select

            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            ds = ObjsRegNeg.ConsultaxParametros("select * from accesos where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario & " and id_modulo  = " & id_modulo & " and id_programa = " & id_programa)
            If ds.Tables(0).Rows.Count > 0 Then
                SqlTexto = "update accesos set ip = '" & Ip & "', " & Campo & " = " & valor & " " & _
                           "where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario & " and id_modulo = " & id_modulo & " and id_programa = " & id_programa & "; "
            Else
                SqlTexto = "insert into accesos (id_usuario, id_modulo, id_programa, ip, id_empresa, " & Campo & ") values " & _
                           "(" & id_usuario & ", " & id_modulo & ", " & id_programa & ", '" & Ip & "', " & Id_Empresa & ", " & valor & ");"
            End If
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Acceso :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Private Function Obtener_Cadena_Acceso(ByVal id_usuario As String, ByVal id_modulo As String, ByVal id_programa As String)
        Dim Cadena_Acceso = ""
        Return Cadena_Acceso
    End Function

    Public Function Ejecuta_MySql_Reader(ByVal String_Sql As String) As Object
        Return ObjsRegNeg.Consulta_Data_Reader(String_Sql)
    End Function

    Public Sub Listar_Empresas(ByVal obj As Object)
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

    Public Sub Copiar_Accesos_De(ByVal id_usuario_origen As Integer, ByVal id_usuario_destino As Integer)
        Try
            Dim ds As New Data.DataSet
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "delete from accesos where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario_destino & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            SqlTexto = "insert into accesos (id_modulo, id_programa, ip, id_empresa, acceso, agrega, modifica, elimina, imprime, supervisor, id_usuario) " & _
                       "select id_modulo, id_programa, '" & Ip & "', id_empresa, acceso, agrega, modifica, elimina, imprime, supervisor, " & id_usuario_destino & " " & _
                       "from accesos where id_empresa = " & Id_Empresa & " and id_usuario = " & id_usuario_origen & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Acceso :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

End Class
