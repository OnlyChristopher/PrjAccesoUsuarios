Public Class Cls_Time_Sheet
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private TextoSQL As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Personal(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_persona, persona from personas where id_empresa = " & Id_Empresa & " and estado = 1 order by id_persona;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Etapa(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_fase, fase from fases where id_empresa = " & Id_Empresa & " order by id_fase;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Tarea_Ejecutada(ByVal obj As Object, ByVal id_etapa As String)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_tarea, descripcion, grupo from tareas where id_empresa = " & Id_Empresa & " and id_fase = '" & id_etapa & "' order by id_tarea;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Tareas_Personal_Proyecto(ByVal obj As Object, ByVal id_persona As Integer, ByVal id_proyecto As Integer, ByVal FechaI As String, ByVal FechaF As String)
        Dim ds As New Data.DataSet
        Dim Texto_Where As String = " where a.id_empresa = " & Id_Empresa
        If id_persona > 0 Then
            Texto_Where &= " and a.id_persona = " & id_persona
        End If
        If id_proyecto > 0 Then
            Texto_Where &= " and a.id_proyecto = " & id_proyecto
        End If

        Texto_Where &= " and a.fecha between '" & FechaI & "' and '" & FechaF & "' "

        'Dim fechainicio As String
        'Dim fechafin As String

        'fechainicio = DateTime.Parse(FechaI.ToString("yyyy-MM-dd") & " 00:00:00")
        'fechafin = DateTime.Parse(FechaF.ToString("yyyy-MM-dd") & " 23:59:59")

        'fechainicio = DateTime.Parse(FechaI & " 00:00:00")
        'fechafin = DateTime.Parse(FechaF & " 23:59:59")

        '"date_format(concat('2011-01-01 ', hora_duracion,':', minuto_duracion), '%l:%i') as hora_min_duracion " & _

        TextoSQL = "select b.persona, c.fase, d.descripcion, e.proyecto, a.hora_inicio, a.minuto_inicio, a.hora_duracion, a.minuto_duracion, " & _
                          "a.id_persona, a.fecha, a.id_tareas_realizadas, " & _
                          "date_format(concat('2011-01-01 ', hora_inicio,':', minuto_inicio), '%l:%i %p') as hora_min_inicio, " & _
                          "cast((concat(case when hora_duracion < 10 then '0' else '' end, hora_duracion, ':', case when minuto_duracion < 10 then '0' else '' end, minuto_duracion)) as char) as hora_min_duracion, " & _
                          "a.hora_continua, a.minuto_continua, a.id_estado, a.id_fase, a.id_tarea " & _
                   "from tareasrealizadas a " & _
                   "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                   "join fases c on c.id_empresa = a.id_empresa and c.id_fase = a.id_fase " & _
                   "join tareas d on d.id_empresa = a.id_empresa and d.id_tarea = a.id_tarea and d.id_fase = a.id_fase " & _
                   "join proyectos e on e.id_empresa = a.id_empresa and e.id_proyecto = a.id_proyecto " & Texto_Where & " order by a.id_persona, a.id_tareas_realizadas; "
        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Tareas_Personal(ByVal obj As Object, ByVal id_persona As Integer, ByVal FechaI As String, ByVal FechaF As String)
        Dim ds As New Data.DataSet
        Dim Texto_Where As String = " where a.id_empresa = " & Id_Empresa
        If id_persona > 0 Then
            Texto_Where &= " and a.id_persona = " & id_persona
        End If

        Texto_Where &= " and a.fecha between '" & FechaI & "' and '" & FechaF & "' "

        'Dim fechainicio As String
        'Dim fechafin As String

        'fechainicio = DateTime.Parse(FechaI.ToString("yyyy-MM-dd") & " 00:00:00")
        'fechafin = DateTime.Parse(FechaF.ToString("yyyy-MM-dd") & " 23:59:59")

        'fechainicio = DateTime.Parse(FechaI & " 00:00:00")
        'fechafin = DateTime.Parse(FechaF & " 23:59:59")

        '"date_format(concat('2011-01-01 ', hora_duracion,':', minuto_duracion), '%l:%i') as hora_min_duracion " & _

        TextoSQL = "select b.persona, c.fase, d.descripcion, e.proyecto, a.hora_inicio, a.minuto_inicio, a.hora_duracion, a.minuto_duracion, " & _
                          "a.id_persona, a.fecha, a.id_tareas_realizadas, " & _
                          "date_format(concat('2011-01-01 ', hora_inicio,':', minuto_inicio), '%l:%i %p') as hora_min_inicio, " & _
                          "cast((concat(case when hora_duracion < 10 then '0' else '' end, hora_duracion, ':', case when minuto_duracion < 10 then '0' else '' end, minuto_duracion)) as char) as hora_min_duracion, " & _
                          "a.hora_continua, a.minuto_continua, a.id_estado " & _
                   "from tareasrealizadas a " & _
                   "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                   "join fases c on c.id_empresa = a.id_empresa and c.id_fase = a.id_fase " & _
                   "join tareas d on d.id_empresa = a.id_empresa and d.id_tarea = a.id_tarea and d.id_fase = a.id_fase " & _
                   "join proyectos e on e.id_empresa = a.id_empresa and e.id_proyecto = a.id_proyecto " & Texto_Where & " order by a.id_persona, a.id_tareas_realizadas; "
        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Existe_Tarea_del_Dia(ByVal id_persona As String, ByVal FechaI As String, ByVal FechaF As String) As Boolean
        Dim ds As New Data.DataSet
        'TextoSQL = "select a.id_tareas_realizadas " & _
        '           "from tareasrealizadas a " & _
        '           "where a.id_empresa = " & Id_Empresa & " and a.id_persona = " & id_persona & " and a.fecha between '" & FechaI & "' and '" & FechaF & "' and (a.hora_duracion + a.minuto_duracion) = 0; "

        TextoSQL = "select a.id_tareas_realizadas " & _
                   "from tareasrealizadas a " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_persona = " & id_persona & " and a.fecha between '" & FechaI & "' and '" & FechaF & "' and id_estado <> 3; "

        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
        ds.Dispose()
    End Function

    Public Sub Listar_Proyectos(ByVal obj As Object)
        ' solo los proyectos aprobados
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_proyecto, proyecto from proyectos where id_empresa = " & Id_Empresa & " and id_estado = 2 order by id_proyecto; ")
        'ds = ObjsRegNeg.ConsultaxParametros("select id_cliente as id_proyecto, cliente as descripcion from clientes order by id_cliente")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_Id_TimeSheet() As Integer
        TextoSQL = "select ifnull(max(id_tareas_realizadas),0) + 1 from tareasrealizadas where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(TextoSQL).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub InsertarTimeSheet(ByVal id_tareas_realizadas As Integer, ByVal id_persona As Integer, ByVal id_tarea As Integer, _
                                    ByVal id_fase As Integer, ByVal fecha As String, ByVal hora_inicio As Integer, ByVal minuto_inicio As Integer, _
                                    ByVal hora_duracion As Integer, ByVal minuto_duracion As Integer, ByVal id_cliente As Integer, ByVal id_proyecto As Integer, _
                                    ByVal id_usuario As Integer, Optional ByVal id_estado As Integer = 1)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            ObjsRegNeg.InsertarxActualizaxBorra("insert into tareasrealizadas (id_tareas_realizadas, id_persona, id_tarea, id_fase, fecha, hora_inicio, minuto_inicio, hora_duracion, minuto_duracion, id_cliente, id_proyecto, id_usuario, ip, id_empresa, id_estado) values " & _
                                                 "(" & id_tareas_realizadas & "," & id_persona & "," & id_tarea & "," & id_fase & ",'" & fecha & "'," & hora_inicio & "," & minuto_inicio & "," & hora_duracion & "," & minuto_duracion & "," & id_cliente & "," & id_proyecto & "," & id_usuario & ",'" & Ip & "', " & Id_Empresa & ", " & id_estado & ");")
        Catch ex As Exception
            Throw New Exception("Cls_Time_Sheet :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Tareas_Realizadas(ByVal id_tareas_realizadas As String) As String
        Try
            TextoSQL = "delete from tareasrealizadas where id_empresa = " & Id_Empresa & " and id_tareas_realizadas = " & id_tareas_realizadas & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(TextoSQL)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Time_Sheet :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Function Finalizar_Tareas_Realizadas(ByVal id_tareas_realizadas As String, ByVal hora_duracion As Integer, ByVal minuto_duracion As Integer) As String
        Try
            TextoSQL = "update tareasrealizadas set hora_duracion = " & hora_duracion & ", minuto_duracion = " & minuto_duracion & ", id_estado = 3 " & _
                       "where id_empresa = " & Id_Empresa & " and id_tareas_realizadas = " & id_tareas_realizadas & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(TextoSQL)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Time_Sheet :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Function retorna_ds_reporte_timeshet(ByVal FechaI As Date, ByVal FechaF As Date) As DataSet
        Try
            Dim ds As New Data.DataSet
            Dim fechainicio As String
            Dim fechafin As String

            fechainicio = FechaI.ToString("yyyy-MM-dd-") & " 00:00:00"
            fechafin = FechaF.ToString("yyyy-MM-dd") & " 23:59:59"

            TextoSQL = "select b.persona, c.fase, d.descripcion, a.hora_inicio, a.minuto_inicio, a.hora_duracion, a.minuto_duracion, a.id_persona, a.fecha, a.id_tareas_realizadas " & _
                       "from tareasrealizadas a " & _
                       "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                       "join fases c on c.id_empresa = a.id_empresa and c.id_fase = a.id_fase " & _
                       "join tareas d on d.id_empresa = a.id_empresa and d.id_tarea = a.id_tarea and d.id_fase = a.id_fase " & _
                       "where a.id_empresa = " & Id_Empresa & " and a.fecha between '" & fechainicio & "' and '" & fechafin & "' " & _
                       "order by a.id_tareas_realizadas "

            Return ObjsRegNeg.ConsultaxParametros(TextoSQL)
        Catch ex As Exception
            Throw New Exception("Cls_Time_Sheet :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Function Pausar_Tareas_Realizadas(ByVal id_tareas_realizadas As String, ByVal hora_pausa As Integer, ByVal minuto_pausa As Integer, ByVal hora_continua As Integer, ByVal minuto_continua As Integer, ByVal estado As Integer) As String
        Try
            TextoSQL = "update tareasrealizadas set " & _
                               "hora_duracion = " & hora_pausa & ", " & _
                               "minuto_duracion = " & minuto_pausa & ", " & _
                               "hora_continua = " & hora_continua & ", " & _
                               "minuto_continua = " & minuto_continua & ", " & _
                               "id_estado = " & estado & " " & _
                       "where id_empresa = " & Id_Empresa & " and id_tareas_realizadas = " & id_tareas_realizadas & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(TextoSQL)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Time_Sheet :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Function Continuar_Tareas_Realizadas(ByVal id_tareas_realizadas As String, ByVal hora_continua As Integer, ByVal minuto_continua As Integer, ByVal estado As Integer) As String
        Try
            TextoSQL = "update tareasrealizadas set " & _
                               "hora_continua = " & hora_continua & ", " & _
                               "minuto_continua = " & minuto_continua & ", " & _
                               "id_estado = " & estado & " " & _
                       "where id_empresa = " & Id_Empresa & " and id_tareas_realizadas = " & id_tareas_realizadas & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(TextoSQL)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Time_Sheet :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Function Recupera_Cantidad_Tarea_x_Dia(ByVal id_persona As String, ByVal FechaI As String, ByVal FechaF As String) As DataSet
        Try
            Dim Texto_Where As String = ""
            Texto_Where = " where id_empresa = " & Id_Empresa & " and id_persona = " & id_persona & " and fecha between '" & FechaI & "' and '" & FechaF & "' "

            TextoSQL = "select concat(day(fecha),month(fecha),year(fecha)) as fechas, count(*) as cantidad " & _
                       "from tareasrealizadas " & Texto_Where & " group by concat(day(fecha),month(fecha),year(fecha)) " & _
                       "order by 2 desc"

            Return ObjsRegNeg.ConsultaxParametros(TextoSQL)
        Catch ex As Exception
            Throw New Exception("Cls_Time_Sheet :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Function Retorna_Ds_Tarea_x_Dia_Persona(ByVal Id_Persona As String, ByVal FechaI As String, ByVal FechaF As String) As DataSet
        Try

            TextoSQL = "select c.fase, d.descripcion, a.hora_inicio, a.minuto_inicio, a.hora_duracion, a.minuto_duracion, a.id_persona, a.fecha, a.id_tareas_realizadas, " & _
                            "date_format(concat('2011-01-01 ', a.hora_inicio,':', a.minuto_inicio), '%l:%i %p') as hora_min_inicio, " & _
                            "cast((concat(case when a.hora_duracion < 10 then '0' else '' end, a.hora_duracion, ':', case when a.minuto_duracion < 10 then '0' else '' end, a.minuto_duracion)) as char) as hora_min_duracion, proyecto " & _
                       "from tareasrealizadas a " & _
                       "join fases c on c.id_empresa = a.id_empresa and c.id_fase = a.id_fase " & _
                       "join tareas d on d.id_empresa = a.id_empresa and d.id_tarea = a.id_tarea and d.id_fase = a.id_fase " & _
                       "join proyectos b on b.id_empresa = a.id_empresa and b.id_proyecto = a.id_proyecto " & _
                       "where a.id_empresa = " & Id_Empresa & " and a.id_persona = " & Id_Persona & " and a.fecha between '" & FechaI & "' and '" & FechaF & "' " & _
                       "order by a.id_tareas_realizadas "

            Return ObjsRegNeg.ConsultaxParametros(TextoSQL)
        Catch ex As Exception
            Throw New Exception("Cls_Time_Sheet :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Sub Listar_Personal_Encargado_Tarea(ByVal obj As Object, ByVal id_fase As Integer, ByVal id_tarea As Integer)
        Dim ds As New Data.DataSet
        TextoSQL = "select a.id_fase, a.id_tarea, a.id_persona, b.persona " & _
                   "from tareas_personas a " & _
                   "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_fase = " & id_fase & " and a.id_tarea = " & id_tarea & " order by a.id_persona;"

        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Listar_Tareas_x_Proyecto(ByVal obj As Object, ByVal id_proyecto As Integer) As DataSet
        Dim ds As New Data.DataSet
        TextoSQL = "select a.descripcion, concat(cast(left(horas,length(horas)-3) + (right(horas,2) / 60) as char),'h:', cast((right(horas,2) % 3600 % 60) as char), 'm') as horas_establecidas, ifnull(horas,0) as horas, " & _
                        "a.id_fase, a.id_tarea, d.fase, " & _
                        "sum(ifnull(c.hora_duracion,0)) as horas_duracion, " & _
                        "sum(ifnull(c.minuto_duracion,0)) as minutos_duracion " & _
                   "from tareas a " & _
                   "left join proyectos_tareas b on b.id_empresa = a.id_empresa and b.id_fase = a.id_fase and b.id_tarea = a.id_tarea and b.id_proyecto = " & id_proyecto & " " & _
                   "left join tareasrealizadas c on c.id_empresa = a.id_empresa and c.id_fase = a.id_fase and c.id_tarea = a.id_tarea and c.id_proyecto = " & id_proyecto & " " & _
                   "left join fases d on d.id_empresa = a.id_empresa and d.id_fase = a.id_fase " & _
                   "where a.id_empresa = " & Id_Empresa & " " & _
                   "group by a.descripcion, ifnull(b.horas,0), a.id_fase, a.id_tarea, d.fase " & _
                   "order by a.id_fase, a.id_tarea"

        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)

        '"(sum(ifnull(c.hora_duracion,0)) + (sum(ifnull(c.minuto_duracion,0)) % 3600 / 60)) as horas_duracion, " & _
        '"(sum(ifnull(c.minuto_duracion,0)) % 3600 % 60) as minutos_duracion " & _

        'If ds.Tables(0).Rows.Count > 0 Then
        '    obj.enabled = True
        'Else
        '    obj.enabled = Not True
        'End If
        'obj.DataSource = ds.Tables(0).DefaultView
        'ds.Dispose()
        Return ds
    End Function

    Public Function Total_Horas_Proforma(ByVal id_proyecto As Integer) As String
        Dim Total_Horas As String = "0h:0m"
        Dim ds As New Data.DataSet
        TextoSQL = "select concat(cast(sum(left(horas,length(horas)-3)) + sum((right(horas,2)) / 60) as char),'h:', cast((sum(right(horas,2)) % 3600 % 60) as char), 'm') as total_horas " & _
                   "from proforma_elementos_personal where id_empresa = " & Id_Empresa & " and id_proforma in " & _
                        "(select id_proforma from proyectos where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & "); "
        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        If ds.Tables(0).Rows.Count > 0 Then
            Total_Horas = ds.Tables(0).Rows(0)(0).ToString
        End If

        Return Total_Horas

    End Function

    Public Function Fecha_Inicio_Proyecto(ByVal id_proyecto As Integer) As String
        Dim Fecha_Inicio As String = ""
        Dim ds As New Data.DataSet
        TextoSQL = "select cast(fecha_inicio as date) as fecha_inicio from proyectos where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & "; "
        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        If ds.Tables(0).Rows.Count > 0 Then
            Fecha_Inicio = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)), "", ds.Tables(0).Rows(0)(0).ToString)
        End If

        Return Fecha_Inicio
    End Function

    Public Function Recupera_Proyecto_Costo_x_Dia(ByVal id_proyecto As Integer, ByVal fecha As Date) As DataSet
        Dim ds As New Data.DataSet
        TextoSQL = "select sum((a.hora_duracion * b.costo) + (a.minuto_duracion * (b.costo / 60))) as costo_horas, " & _
                          "sum(a.hora_duracion) as hora_duracion, sum(a.minuto_duracion) as minuto_duracion " & _
                   "from tareasrealizadas a " & _
                   "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_proyecto = " & id_proyecto & " and a.id_estado = 3 and (year(a.fecha) = " & Year(fecha) & " and month(a.fecha) = " & Month(fecha) & " and day(a.fecha) = " & Day(fecha) & ") ; "
        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)

        Return ds
    End Function

    Public Function Total_Gasto_Proyecto(ByVal id_proyecto As Integer) As Double
        Dim Total_Gasto As Double = 0
        Dim ds As New Data.DataSet
        TextoSQL = "select sum(ifnull(costo,0)) as costo from proforma_gastos_operativos where id_empresa = " & Id_Empresa & " and id_proforma = (select id_proforma from proyectos where id_empresa = " & Id_Empresa & " and id_proyecto = " & id_proyecto & "); "
        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        If ds.Tables(0).Rows.Count > 0 Then
            Total_Gasto = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)), 0, ds.Tables(0).Rows(0)(0).ToString)
        End If

        Return Total_Gasto
    End Function

    Public Function Select_Chart(ByVal id_proyecto As Integer) As Data.DataSet
        Dim ds As Data.DataSet
        TextoSQL = "select bb.descripcion, ifnull(concat(cast(sum(left(aa.horas,length(aa.horas)-3)) + sum((right(aa.horas,2)) / 60) as char),'.', cast((sum(right(aa.horas,2)) % 3600 % 60) as char)),0) as horas_proyecto, " & _
                   "(select cast(sum(a.hora_duracion) + (sum(a.minuto_duracion) / 60) as char) as horas " & _
                           "from tareasrealizadas a " & _
                           "join proyectos b on b.id_empresa = a.id_empresa and b.id_proyecto = a.id_proyecto " & _
                           "where a.id_empresa = aa.id_empresa and b.id_proforma = cc.id_proforma " & _
                           "group by a.id_proyecto, b.id_proforma) as horas_trabajadas " & _
                   "from proyectos_tareas_personas aa " & _
                   "join proyectos cc on cc.id_empresa = aa.id_empresa and cc.id_proyecto = aa.id_proyecto " & _
                   "join proforma bb on bb.id_empresa = aa.id_empresa and bb.id_proforma = cc.id_proforma " & _
                   "where aa.id_empresa = " & Id_Empresa & " and aa.id_proyecto = " & id_proyecto & " " & _
                   "group by bb.descripcion "

        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        Return ds

        'TextoSQL = "select b.descripcion, sum(a.costo) as costo " & _
        '           "from proforma_gastos_operativos a " & _
        '           "join gastos_operativos b on b.id_gasto = a.id_gasto " & _
        '           "group by b.descripcion; "

        'mysql()
        'select id_proforma, ifnull(concat(cast(sum(left(horas,length(horas)-3)) + sum((right(horas,2)) / 60) as char),'.', cast((sum(right(horas,2)) % 3600 % 60) as char)),0) as horas
        'from proforma_elementos_personal
        'where id_empresa = 2
        'group by id_proforma

        'SELECT id_proyecto, concat(cast(sum(hora_duracion) + (sum(minuto_duracion) / 60) as char),'.', (sum(minuto_duracion) % 3600 % 60)) as horas
        'FROM tareasrealizadas
        'where id_empresa = 2
        'group by id_proyecto
    End Function

    Public Function Select_Chart_Detalle(ByVal id_proyecto As Integer) As Data.DataSet
        Dim ds As Data.DataSet
        'TextoSQL = "select bb.descripcion, ifnull(concat(cast(sum(left(aa.horas,length(aa.horas)-3)) + sum((right(aa.horas,2)) / 60) as char),'.', cast((sum(right(aa.horas,2)) % 3600 % 60) as char)),0) as horas_proyecto, " & _
        '           "(select cast(sum(a.hora_duracion) + (sum(a.minuto_duracion) / 60) as char) as horas " & _
        '                   "from tareasrealizadas a " & _
        '                   "join proyectos b on b.id_empresa = a.id_empresa and b.id_proyecto = a.id_proyecto " & _
        '                   "where a.id_empresa = aa.id_empresa and b.id_proforma = cc.id_proforma " & _
        '                   "group by a.id_proyecto, b.id_proforma) as horas_trabajadas " & _
        '           "from proyectos_tareas_personas aa " & _
        '           "join proyectos cc on cc.id_empresa = aa.id_empresa and cc.id_proyecto = aa.id_proyecto " & _
        '           "join proforma bb on bb.id_empresa = aa.id_empresa and bb.id_proforma = cc.id_proforma " & _
        '           "where aa.id_empresa = " & Id_Empresa & " " & _
        '           "group by bb.descripcion "

        TextoSQL = "select a.descripcion, ifnull(b.horas,0) as horas, " & _
                          "(sum(ifnull(c.hora_duracion,0)) + cast((sum(ifnull(c.minuto_duracion,0)) % 3600 / 60) as unsigned)) as horas_duracion " & _
                   "from tareas a " & _
                   "left join proyectos_tareas b on b.id_empresa = a.id_empresa and b.id_fase = a.id_fase and b.id_tarea = a.id_tarea and b.id_proyecto = " & id_proyecto & " " & _
                   "left join tareasrealizadas c on c.id_empresa = a.id_empresa and c.id_fase = a.id_fase and c.id_tarea = a.id_tarea and c.id_proyecto = " & id_proyecto & " " & _
                   "left join fases d on d.id_empresa = a.id_empresa and d.id_fase = a.id_fase " & _
                   "where a.id_empresa = " & Id_Empresa & " " & _
                   "group by a.descripcion, ifnull(b.horas,0), a.id_fase, a.id_tarea, d.fase " & _
                   "order by a.id_fase, a.id_tarea"

        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
        Return ds

        'TextoSQL = "select b.descripcion, sum(a.costo) as costo " & _
        '           "from proforma_gastos_operativos a " & _
        '           "join gastos_operativos b on b.id_gasto = a.id_gasto " & _
        '           "group by b.descripcion; "

        'mysql()
        'select id_proforma, ifnull(concat(cast(sum(left(horas,length(horas)-3)) + sum((right(horas,2)) / 60) as char),'.', cast((sum(right(horas,2)) % 3600 % 60) as char)),0) as horas
        'from proforma_elementos_personal
        'where id_empresa = 2
        'group by id_proforma

        'SELECT id_proyecto, concat(cast(sum(hora_duracion) + (sum(minuto_duracion) / 60) as char),'.', (sum(minuto_duracion) % 3600 % 60)) as horas
        'FROM tareasrealizadas
        'where id_empresa = 2
        'group by id_proyecto
    End Function

    'SELECT fecha_inicio FROM proyectos where id_empresa = 2 and id_proyecto = 3
    'Public Sub Configura_Reporte(ByVal nombreRep As String, ByVal xCristal As Object, ByRef cadena As String, ByVal FechaI As Date, ByVal FechaF As Date)
    '    Try
    '        Dim TextoSQL As String = ""
    '        Dim ds As New Data.DataSet
    '        Dim fechainicio As String
    '        Dim fechafin As String

    '        fechainicio = FechaI.ToString("yyyy-MM-dd-") & " 00:00:00"
    '        fechafin = FechaF.ToString("yyyy-MM-dd") & " 23:59:59"

    '        TextoSQL = "select b.persona, c.fase,d.descripcion, a.hora_inicio, a.minuto_inicio, a.hora_duracion, a.minuto_duracion, a.id_persona, a.fecha " & _
    '                   "from tareasrealizadas a " & _
    '                   "join personas b on b.id_persona = a.id_persona " & _
    '                   "join fases c on c.id_fase = a.id_fase " & _
    '                   "join tareas d on d.id_tarea = a.id_tarea and d.id_fase = a.id_fase " & _
    '                   "where a.fecha between '" & fechainicio & "' and '" & fechafin & "' " & _
    '                   "order by a.id_tareas_realizadas "

    '        ds = ObjsRegNeg.ConsultaxParametros(TextoSQL)
    '        If ds.Tables(0).Rows.Count <= 0 Then
    '            cadena = "No Hay Datos que Mostrar"
    '        Else
    '            Dim rpt As New ReportDocument
    '            rpt.Load(nombreRep)
    '            'rpt.Database.Tables(0).SetDataSource(ds)
    '            rpt.SetDataSource(ds.Tables(0))
    '            xCristal.ReportSource = rpt
    '            'xCristal.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX
    '            xCristal.AutoDataBind = True
    '            xCristal.DataBind()
    '            cadena = ""
    '        End If
    '        'ds.Dispose()
    '    Catch ex As Exception
    '        cadena = ex.Message
    '    End Try
    'End Sub
End Class
