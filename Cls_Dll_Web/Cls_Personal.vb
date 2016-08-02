Public Class Cls_Personal
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Personal(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select *, concat(rtrim(ltrim(nombres)), ' ', rtrim(ltrim(apellidos))) as nombresapellidos, case when estado = 1 then 'Activo' else 'Inactivo' end as estado_desc from personas where id_empresa = " & Id_Empresa & " order by id_persona;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_id_persona() As Integer
        SqlTexto = "select ifnull(max(id_persona),0) + 1 from personas where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Function Insertar_Personal(ByVal id_persona As Integer, ByVal persona As String, ByVal nombres As String, ByVal apellidos As String, ByVal direccion As String, ByVal telefono As String, ByVal costo As String, ByVal estado As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""

            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_Personal", persona, nombre, costo, telefono, id_usuario, ip)
            SqlTexto = "insert into personas (id_persona, persona, nombres, apellidos, direccion, telefono, costo, estado, id_usuario, ip, id_empresa) values " & _
                       "(" & id_persona & ", '" & persona & "', '" & nombres & "', '" & apellidos & "', '" & direccion & "', '" & telefono & "', " & costo & ", " & estado & ", " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Personal :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Sub Modificar_Personal(ByVal id_persona As String, ByVal persona As String, ByVal nombres As String, ByVal apellidos As String, ByVal direccion As String, ByVal telefono As String, ByVal costo As String, ByVal estado As String, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Modifica_Personal", id_persona, persona, nombre, costo, telefono, id_usuario, fecha_creacion, Ip)
            SqlTexto = "update personas set persona = '" & persona & "', " & _
                              "nombres = '" & nombres & "', " & _
                              "apellidos = '" & apellidos & "', " & _
                              "direccion = '" & direccion & "', " & _
                              "telefono = '" & telefono & "', " & _
                              "estado = " & estado & ", " & _
                              "costo = " & costo & ", " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_persona = " & id_persona & "; "

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Personal :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Eliminar_Personal(ByVal id_persona As String) As String
        Try
            Dim ds As New Data.DataSet
            SqlTexto = "select * from tareasrealizadas where id_empresa = " & Id_Empresa & " and id_persona = " & id_persona & ";"
            ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
            If ds.Tables(0).Rows.Count > 0 Then
                Return "No se puede eliminar, el Personal tiene un registro Time Sheet de fecha " & ds.Tables(0).Rows(0)("fecha").ToString & ", primero elimine ese registro Time Sheet"
            End If

            SqlTexto = "delete from personas where id_empresa = " & Id_Empresa & " and id_persona = " & id_persona & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Personal :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
End Class
