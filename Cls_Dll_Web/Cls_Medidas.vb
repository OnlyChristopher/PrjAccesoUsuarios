Public Class Cls_Medidas
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_medidas(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select * from medidas order by id_medida")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_medida() As Integer
        SqlTexto = "select ifnull(max(id_medida),0) + 1 from medidas"
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_medidas(ByVal id_medida As Integer, ByVal medida As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_medidas", medida, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into medidas (id_medida, medida, id_usuario, ip) values " & _
                       "(" & id_medida & ",'" & medida.Replace("'", "''") & "'," & id_usuario & ",'" & Ip & "');"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Medidas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_medidas(ByVal id_medida As String, ByVal medida As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_medidas", id_medida, medida, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update medidas set medida = '" & medida.Replace("'", "''") & "', " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_medida = " & id_medida & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Medidas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_medidas(ByVal id_medida As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from elementos where id_medida = " & id_medida & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, el elemento " & ds.Tables(0).Rows(0)("elemento").ToString & " contiene a esta medida, primero elimine el elemento"
            End If

            SqlTexto = "delete from medidas where id_medida = " & id_medida & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Medidas :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
End Class
