Public Class Cls_Horas_Personal

    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Sub Listar_Horas_Personal(ByVal id_persona As Integer, ByVal id_servicio As Integer, ByVal obj As Object)
        Try
            Dim ds0 As New Data.DataSet
            Dim ds1 As New Data.DataSet
            Dim ds2 As New Data.DataSet
            Dim ds As New Data.DataSet

            ' elimina registros del temporal
            SqlTexto = "delete from horas_persona_tmp;"
            ds0 = ObjsRegNeg.ConsultaxParametros(SqlTexto)

            ' inserta la lista de programas al temporal
            SqlTexto = "insert into horas_persona_tmp (id_categoria, categoria) " & _
                       "select id_categoria, concat(descripcion, ' (',categoria, ')') as descripcion from categorias where id_empresa = " & Id_Empresa & "; "
            ds1 = ObjsRegNeg.ConsultaxParametros(SqlTexto)

            ' actualiza los tipos de accesos al temporal
            SqlTexto = "update horas_persona_tmp a, horaspersonaformato b set a.horas = ifnull(b.horas,0), a.activo = b.activo " & _
                       "where b.id_empresa = " & Id_Empresa & " and b.id_servicio = " & id_servicio & " and a.id_categoria = b.id_categoria and b.id_persona = " & id_persona & ";"
            ds2 = ObjsRegNeg.ConsultaxParametros(SqlTexto)

            ' muestra el resultado del temporal
            SqlTexto = "select id_categoria, categoria, horas, " & _
                            "case when activo = 0 then 'False' else 'True' end as activo " & _
                        "from horas_persona_tmp order by id_categoria"
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
            Throw New Exception("Cls_Horas_Personal :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Obtener_id_horaspersonaformato() As Integer
        SqlTexto = "select ifnull(max(id_horas_persona_formato),0) + 1 from horaspersonaformato where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Function Inserta_Actualiza_Hora_Personal(ByVal id_horas_persona_formato As Integer, ByVal id_persona As Integer, ByVal id_servicio As Integer, ByVal id_categoria As Integer, ByVal horas As Double, ByVal activo As Integer, ByVal id_usuario As Integer) As String
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            Dim resultado As String = "Datos guardados satisfactoriamente"
            Dim ds As New Data.DataSet
            ds = ObjsRegNeg.ConsultaxParametros("select * from horaspersonaformato where id_empresa = " & Id_Empresa & " and id_persona = " & id_persona & " and id_servicio = " & id_servicio & " and id_categoria = " & id_categoria)
            If ds.Tables(0).Rows.Count > 0 Then
                SqlTexto = "update horaspersonaformato set horas = " & horas & ", activo = " & activo & ", id_usuario = " & id_usuario & ", Ip = '" & Ip & "' " & _
                           "where id_empresa = " & Id_Empresa & " and id_persona = " & id_persona & " and id_servicio = " & id_servicio & " and id_categoria = " & id_categoria & "; "
            Else
                SqlTexto = "insert into horaspersonaformato (id_horas_persona_formato, id_persona, id_servicio, id_categoria, horas, activo, id_usuario, ip, id_empresa) values " & _
                           "(" & id_horas_persona_formato & ", " & id_persona & ", " & id_servicio & ", " & id_categoria & ", " & horas & ", " & activo & ", " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"
            End If
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
            Return resultado
        Catch ex As Exception
            Throw New Exception("Cls_Horas_Personal :" & vbCrLf & ex.Message, ex)
        End Try
    End Function
End Class
