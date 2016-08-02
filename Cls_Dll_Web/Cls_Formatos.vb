Public Class Cls_Formatos
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Formatos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select * from formatos where id_empresa = " & Id_Empresa & " order by id_formato;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_formato() As Integer
        SqlTexto = "select ifnull(max(id_formato),0) + 1 from formatos where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Formatos(ByVal id_formato As Integer, ByVal formato As String, ByVal descripcion As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_Formato", formato, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into formatos (id_formato, formato, descripcion, id_usuario, ip, id_empresa) values " & _
                       "(" & id_formato & ", '" & formato.Replace("'", "''") & "', '" & descripcion.Replace("'", "''") & "', " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Formatos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Formatos(ByVal id_formato As String, ByVal formato As String, ByVal descripcion As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Formato", id_formato, formato, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update formatos set formato = '" & formato.Replace("'", "''") & "', " & _
                              "descripcion = '" & descripcion.Replace("'", "''") & "', " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_formato = " & id_formato & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Formatos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Formatos(ByVal id_formato As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from proforma where id_empresa = " & Id_Empresa & " and id_formato = " & id_formato & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, la proforma Nro. " & ds.Tables(0).Rows(0)("id_proforma").ToString & ", se genero con este formato "
            End If

            SqlTexto = "delete from formatos where id_empresa = " & Id_Empresa & " and id_formato = " & id_formato & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Formatos :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

End Class
