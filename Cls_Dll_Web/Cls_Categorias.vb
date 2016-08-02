Public Class Cls_Categorias
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Categorias(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select *, concat(descripcion, ' (',categoria, ')') as descrip, " & _
                                                "case modo_proceso when '0' then 'Automatico' when '1' then 'Manual' end as desc_modo_proceso " & _
                                            "from categorias where id_empresa = " & Id_Empresa & " order by id_categoria;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_categoria() As Integer
        SqlTexto = "select ifnull(max(id_categoria),0) + 1 from categorias where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Categorias(ByVal id_categoria As Integer, ByVal categoria As String, ByVal descripcion As String, ByVal id_usuario As Integer, ByVal modo_proceso As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_categoria", categoria, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into categorias (id_categoria, categoria, descripcion, id_usuario, ip, modo_proceso, id_empresa) values " & _
                       "(" & id_categoria & ", '" & categoria.Replace("'", "''") & "', '" & descripcion.Replace("'", "''") & "', " & id_usuario & ", '" & Ip & "', " & modo_proceso & ", " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Categorias :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Categorias(ByVal id_categoria As String, ByVal categoria As String, ByVal descripcion As String, ByVal id_usuario As Integer, ByVal modo_proceso As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_categoria", id_categoria, categoria, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update categorias set categoria = '" & categoria.Replace("'", "''") & "', " & _
                              "descripcion = '" & descripcion.Replace("'", "''") & "', " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "', " & _
                              "modo_proceso = " & modo_proceso & " " & _
                       "where id_empresa = " & Id_Empresa & " and id_categoria = " & id_categoria & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Categorias :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Categorias(ByVal id_categoria As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from proforma where id_empresa = " & Id_Empresa & " and id_categoria = " & id_categoria & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, la proforma Nro. " & ds.Tables(0).Rows(0)("id_proforma").ToString & ", se genero con esta categoria "
            End If

            SqlTexto = "delete from categorias where id_empresa = " & Id_Empresa & " and id_categoria = " & id_categoria & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Categorias :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
End Class
