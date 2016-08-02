Public Class Cls_fases_tareas
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Fases(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select * from fases where id_empresa = " & Id_Empresa & " order by id_fase;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_Id_Fase() As Integer
        SqlTexto = "select ifnull(max(id_fase),0) + 1 from fases where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Fases(ByVal id_fase As Integer, ByVal fase As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_fases", cliente, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into fases (id_fase, fase, id_usuario, ip, id_empresa) values " & _
                       "(" & id_fase & ",'" & fase.Replace("'", "''") & "'," & id_usuario & ",'" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Fases_Tareas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Fases(ByVal id_fase As String, ByVal fase As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_fases", id_fase, cliente, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update fases set fase = '" & fase.Replace("'", "''") & "', " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_fase = " & id_fase & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Fases_Tareas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Fases(ByVal id_fase As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from tareas where id_empresa = " & Id_Empresa & " and id_fase = " & id_fase & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, la etapa contiene tareas, primero elimine las tareas"
            End If

            SqlTexto = "delete from fases where id_empresa = " & Id_Empresa & " and id_fase = " & id_fase & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Fases_Tareas :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    ' ********************************************
    ' *************** tareas ******************
    ' ********************************************
    Public Sub Listar_Tareas(ByVal obj As Object, ByVal id_fase As Integer)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_tarea, a.id_fase, a.descripcion, a.id_medida, a.costo, a.rate, a.id_usuario, a.fecha_creacion, a.ip, a.grupo, b.medida " & _
                   "from tareas a " & _
                   "left join medidas b on b.id_medida = a.id_medida " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_fase = " & id_fase & " order by a.id_tarea;"

        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Medidas(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_medida, medida from medidas order by medida")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_Id_Tarea(ByVal id_fase As Integer) As Integer
        SqlTexto = "select ifnull(max(id_tarea),0) + 1 from tareas where id_empresa = " & Id_Empresa & " and id_fase = " & id_fase & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Tareas(ByVal id_tarea As Integer, ByVal id_fase As Integer, ByVal descripcion As String, ByVal id_medida As Integer, ByVal costo As Double, ByVal id_usuario As Integer, ByVal grupo As String)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_tareas", cliente, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into tareas (id_tarea, id_fase, descripcion, id_medida, costo, id_usuario, ip, id_empresa, grupo) values " & _
                       "(" & id_tarea & "," & id_fase & ",'" & descripcion.Replace("'", "''") & "'," & id_medida & "," & costo & "," & id_usuario & ",'" & Ip & "', " & Id_Empresa & ", '" & grupo & "');"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Fases_Tareas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Tareas(ByVal id_tarea As Integer, ByVal descripcion As String, ByVal id_fase As Integer, ByVal id_medida As Integer, ByVal costo As Double, ByVal id_usuario As Integer, ByVal grupo As String)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_tareas", id_tarea, cliente, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update tareas set descripcion = '" & descripcion.Replace("'", "''") & "', " & _
                              "id_medida = " & id_medida & ", " & _
                              "costo = " & costo & ", " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "', " & _
                              "grupo = '" & grupo & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_tarea = " & id_tarea & " and id_fase = " & id_fase & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Fases_Tareas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Tareas(ByVal id_tarea As String, ByVal id_fase As Integer) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select t.*, (select persona from personas where id_empresa = t.id_empresa and id_persona = t.id_persona) as persona " & _
                       "from tareasrealizadas t where t.id_empresa = " & Id_Empresa & " and t.id_fase = " & id_fase & " and t.id_tarea = " & id_tarea & "; "
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, el Personal " & ds.Tables(0).Rows(0)("persona").ToString & " tiene un registro Time Sheet con esta tarea, primero elimine ese registro Time Sheet"
            End If

            SqlTexto = "delete from tareas where id_empresa = " & Id_Empresa & " and id_tarea = " & id_tarea & " and id_fase = " & id_fase & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Fases_Tareas :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Sub Listar_Personal(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.* from personas a " & _
                   "join usuarios b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona and tipo_usuario = 0 " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.estado = 1 order by id_persona;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Personal_x_Tarea(ByVal id_Fase As Integer, ByVal id_Tarea As Integer, ByVal id_Persona As Integer) As Integer
        Dim ds As New Data.DataSet
        Dim Cantidad As Integer
        SqlTexto = "select * from tareas_personas where id_empresa = " & Id_Empresa & " and id_fase = " & id_Fase & " and id_tarea = " & id_Tarea & " and id_persona = " & id_Persona & " order by id_persona;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        Cantidad = ds.Tables(0).Rows.Count
        ds.Dispose()

        Return Cantidad
    End Function

    Public Sub Insertar_Personal_x_Tareas(ByVal id_fase As Integer, ByVal id_tarea As Integer, ByVal id_persona As Integer, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            SqlTexto = "insert into tareas_personas (id_fase, id_tarea, id_persona, id_usuario, ip, id_empresa) values " & _
                       "(" & id_fase & "," & id_tarea & "," & id_persona & "," & id_usuario & ",'" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Fases_Tareas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Eliminar_Personal_x_Tareas(ByVal id_fase As Integer, ByVal id_tarea As Integer)
        Try
            SqlTexto = "delete from tareas_personas where id_empresa = " & Id_Empresa & " and id_fase = " & id_fase & " and id_tarea = " & id_tarea & ";"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Fases_Tareas :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

End Class
