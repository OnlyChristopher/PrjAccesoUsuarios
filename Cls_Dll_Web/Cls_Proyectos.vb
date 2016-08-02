Public Class Cls_Proyectos
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Enum Estado_Proyecto As Integer
        Anulado = 0
        Genereado = 1
        Aprobado = 2
        Finalizado = 3
    End Enum

    Public Sub Listar_proyectos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.*, b.estado from proyectos a " & _
                   "join estado_proyecto b on b.id_estado = a.id_estado " & _
                   "where a.id_empresa = " & Id_Empresa & " order by a.id_proyecto;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Estado_Proyecto(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select id_estado, estado from estado_proyecto order by id_estado;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_proyecto() As Integer
        SqlTexto = "select ifnull(max(id_proyecto),0) + 1 from proyectos where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_proyectos(ByVal id_proyecto As Integer, ByVal proyecto As String, ByVal fecha As String, ByVal id_estado As Integer, ByVal id_usuario As Integer, ByVal id_proforma As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_proyectos", proyecto, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into proyectos (id_proyecto, proyecto, fecha_inicio, id_estado, id_usuario, ip, id_empresa, id_proforma) values " & _
                       "(" & id_proyecto & ",'" & proyecto.Replace("'", "''") & "', '" & fecha & "', " & id_estado & ", " & id_usuario & ",'" & Ip & "', " & Id_Empresa & ", " & id_proforma & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_proyectos(ByVal id_proyecto As String, ByVal proyecto As String, ByVal id_estado As Integer, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_proyectos", id_proyecto, proyecto, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update proyectos set proyecto = '" & proyecto.Replace("'", "''") & "', " & _
                              "id_estado = " & id_estado & ", " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Cambia_Estado_proyectos(ByVal id_proyecto As String, ByVal estado As Estado_Proyecto, ByVal id_usuario As Integer)
        'estado: anulado(0), generado(1), aprobado(2), finalizado(3)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_proyectos", id_proyecto, proyecto, nombre, direccion, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update proyectos set id_estado = " & estado & ", " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_proyectos(ByVal id_proyecto As String) As String
        Try
            SqlTexto = "delete from proyectos where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Function Listar_Tareas_x_Proyecto(ByVal id_proyecto As Integer) As DataSet
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_fase, a.id_tarea, a.descripcion, ifnull(b.horas,0) as horas, c.fase, a.id_elemento " & _
                   "from tareas a " & _
                   "left join proyectos_tareas b on b.id_empresa = a.id_empresa and b.id_fase = a.id_fase and b.id_tarea = a.id_tarea and b.id_proyecto = " & id_proyecto & " " & _
                   "left join fases c on c.id_empresa = a.id_empresa and c.id_fase = a.id_fase " & _
                   "where a.id_empresa = " & Id_Empresa & " " & _
                   "order by a.id_fase, a.id_tarea "
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        'If ds.Tables(0).Rows.Count > 0 Then
        '    obj.enabled = True
        'Else
        '    obj.enabled = Not True
        'End If
        'obj.DataSource = ds.Tables(0).DefaultView
        Return ds
    End Function

    Public Sub Eliminar_Tareas_Proyectos(ByVal id_proyecto As Integer)
        Try

            SqlTexto = "delete from proyectos_tareas where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Insertar_Tareas_Proyectos(ByVal id_proyecto As Integer, ByVal id_fase As Integer, ByVal id_tarea As Integer, ByVal horas As Double, ByVal id_usuario As Integer)
        Try
            SqlTexto = "delete from proyectos_tareas where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & " and id_fase = " & id_fase & " and id_tarea = " & id_tarea & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "insert into proyectos_tareas (id_proyecto, id_fase, id_tarea, horas, id_usuario, ip, id_empresa) values " & _
                       "(" & id_proyecto & "," & id_fase & ", " & id_tarea & ", " & horas & ", " & id_usuario & ",'" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Horas_x_Elemento_Cotizacion(ByVal id_proforma As Integer, ByVal id_elemento As Integer) As Decimal
        Dim ds As New Data.DataSet
        Dim Horas As Decimal = 0

        SqlTexto = "select ifnull(concat(cast(sum(left(horas,length(horas)-3)) + sum((right(horas,2)) / 60) as char),'.', cast((sum(right(horas,2)) % 3600 % 60) as char)),0) as horas " & _
                   "from proforma_elementos_personal " & _
                   "where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & " and id_elemento = " & id_elemento & " " & _
                   "order by id_servicio, id_elemento; "
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            Horas = ds.Tables(0).Rows(0)(0)
        End If
        Return Horas
    End Function

    Public Sub Listar_Personal_x_Tarea_Proyecto(ByVal obj As Object, ByVal id_fase As Integer, ByVal id_tarea As Integer)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_fase, a.id_tarea, a.id_persona, b.persona " & _
                   "from tareas_personas a " & _
                   "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_fase = " & id_fase & " and a.id_tarea = " & id_tarea & "; "
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Horas_x_Persona_Proyecto(ByVal id_proyecto As Integer, ByVal id_fase As Integer, ByVal id_tarea As Integer, ByVal id_persona As Integer) As Decimal
        Dim ds As New Data.DataSet
        Dim Horas As Decimal = 0

        SqlTexto = "select ifnull(horas,0) as horas " & _
                   "from proyectos_tareas_personas " & _
                   "where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & " and id_fase = " & id_fase & " and id_tarea = " & id_tarea & " and id_persona = " & id_persona & ";"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            Horas = ds.Tables(0).Rows(0)(0)
        End If
        Return Horas
    End Function

    Public Sub Eliminar_Tareas_Persona_Proyectos(ByVal id_proyecto As Integer, ByVal id_fase As Integer, ByVal id_tarea As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "delete from proyectos_tareas_personas " & _
                       "where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & " and id_fase = " & id_fase & " and id_tarea = " & id_tarea & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Insertar_Tareas_Persona_Proyectos(ByVal id_proyecto As Integer, ByVal id_fase As Integer, ByVal id_tarea As Integer, ByVal id_persona As Integer, ByVal horas As Double, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "insert into proyectos_tareas_personas (id_proyecto, id_fase, id_tarea, id_persona, horas, id_usuario, ip, id_empresa) values " & _
                       "(" & id_proyecto & "," & id_fase & ", " & id_tarea & ", " & id_persona & ", " & horas & ", " & id_usuario & ",'" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Insertar_Tareas_Persona_Proforma(ByVal id_proforma As Integer, ByVal id_proyecto As Integer, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "insert into proyectos_tareas_personas (id_proyecto, id_fase, id_tarea, id_persona, horas, id_usuario, ip, id_empresa) " & _
                       "select '" & id_proyecto & "', a.id_fase, a.id_tarea, b.id_persona, b.horas, '" & id_usuario & "', '" & Ip & "', '" & Id_Empresa & "' " & _
                       "from tareas a " & _
                       "join proforma_elementos_personal b on b.id_empresa = a.id_empresa and b.id_elemento = a.id_elemento " & _
                       "where a.id_empresa = " & Id_Empresa & " and b.id_proforma = " & id_proforma & " " & _
                       "order by a.id_tarea, a.id_fase, b.id_persona; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Eliminar_Tareas_Persona_Proforma(ByVal id_proyecto As Integer, ByVal id_proforma As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "delete from proyectos_tareas_personas " & _
                       "where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & " and concat(id_fase, id_tarea, id_persona) in " & _
                           "(select concat(a.id_fase, a.id_tarea, b.id_persona) " & _
                           "from tareas a " & _
                           "join proforma_elementos_personal b on b.id_empresa = a.id_empresa and b.id_elemento = a.id_elemento " & _
                           "where a.id_empresa = " & Id_Empresa & " And b.id_proforma = " & id_proforma & "); "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proyectos :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

End Class
