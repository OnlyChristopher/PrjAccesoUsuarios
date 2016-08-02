Public Class Cls_Servicio
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Servicios(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select * from servicios where id_empresa = " & Id_Empresa & " order by id_servicio;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_Id_Servicio() As Integer
        SqlTexto = "select ifnull(max(id_servicio),0) + 1 from servicios where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Servicios(ByVal id_servicio As Integer, ByVal servicio As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_Servicios", cliente, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into servicios (id_servicio, servicio, id_usuario, ip, id_empresa) values " & _
                       "(" & id_servicio & ",'" & servicio.Replace("'", "''") & "'," & id_usuario & ",'" & Ip & "', " & Id_Empresa & ");"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Servicio :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Servicios(ByVal id_servicio As String, ByVal servicio As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Servicios", id_servicio, cliente, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update servicios set servicio = '" & servicio.Replace("'", "''") & "', " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_servicio = " & id_servicio & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Servicio :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Servicios(ByVal id_servicio As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from proformalineaservicios where id_empresa = " & Id_Empresa & " and id_servicio = " & id_servicio & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, la proforma Nro. " & ds.Tables(0).Rows(0)("id_proforma").ToString & ", se genero con este servicio "
            End If

            SqlTexto = "delete from servicios where id_empresa = " & Id_Empresa & " and id_servicio = " & id_servicio & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Servicio :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    ' ********************************************
    ' *************** ELEMENTOS ******************
    ' ********************************************
    Public Sub Listar_Elementos(ByVal obj As Object, ByVal id_servicio As Integer)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_elemento, a.elemento, a.id_servicio, a.descripcion, a.id_medida, a.costo, a.id_usuario, a.fecha_creacion, a.ip, b.medida " & _
                   "from elementos a " & _
                   "left join medidas b on b.id_medida = a.id_medida " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_servicio = " & id_servicio & " order by a.orden;"

        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Listar_Elementos_DataSet(ByVal id_servicio As Integer) As DataSet
        SqlTexto = "select a.id_elemento, a.elemento, a.id_servicio, a.descripcion, a.id_medida, a.costo, a.id_usuario, a.fecha_creacion, a.ip " & _
                   "from elementos a " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_servicio = " & id_servicio & " order by a.orden;"
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Sub Listar_Medidas(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_medida, medida from medidas order by medida;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_elemento(ByVal id_servicio As Integer) As Integer
        SqlTexto = "select ifnull(max(id_elemento),0) + 1 from elementos where id_empresa = " & Id_Empresa & "; " ' where id_servicio = " & id_servicio
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Elementos(ByVal id_elemento As Integer, ByVal elemento As String, ByVal id_servicio As Integer, ByVal id_medida As Integer, ByVal costo As Double, ByVal grupo As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_Elementos", cliente, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into elementos (id_elemento, elemento, id_servicio, id_medida, costo, descripcion, id_usuario, ip, id_empresa) values " & _
                       "(" & id_elemento & ",'" & elemento.Replace("'", "''") & "'," & id_servicio & "," & id_medida & "," & costo & ",'" & grupo.Replace("'", "''") & "'," & id_usuario & ",'" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Servicio :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Elementos(ByVal id_elemento As Integer, ByVal elemento As String, ByVal id_servicio As Integer, ByVal id_medida As Integer, ByVal costo As Double, ByVal grupo As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Elementos", id_elemento, cliente, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update elementos set elemento = '" & elemento.Replace("'", "''") & "', " & _
                              "id_medida = " & id_medida & ", " & _
                              "costo = " & costo & ", " & _
                              "descripcion = '" & grupo.Replace("'", "''") & "', " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_elemento = " & id_elemento & " and id_servicio = " & id_servicio & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Servicio :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Elementos(ByVal id_elemento As String, ByVal id_servicio As Integer) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from proformalineaservicios where id_empresa = " & Id_Empresa & " and id_servicio = " & id_servicio & " and id_elemento = " & id_elemento & "; "
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, la proforma Nro. " & ds.Tables(0).Rows(0)("id_proforma").ToString & ", se genero con este elemento "
            End If

            SqlTexto = "delete from elementos where id_empresa = " & Id_Empresa & " and id_elemento = " & id_elemento & " and id_servicio = " & id_servicio & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Servicio :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Sub Modificar_Orden_Elementos(ByVal id_elemento As Integer, ByVal id_servicio As Integer, ByVal id_orden As Integer, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Elementos", id_elemento, cliente, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update elementos set orden = " & id_orden & ", " & _
                              "id_usuario = " & id_usuario & " " & _
                       "where id_empresa = " & Id_Empresa & " and id_elemento = " & id_elemento & " and id_servicio = " & id_servicio & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Servicio :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

End Class
