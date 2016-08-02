Public Class Cls_Proforma
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Private Id_Empresa As Integer = System.Configuration.ConfigurationSettings.AppSettings("id_empresa")

    Public Enum Servicio As Integer
        pre_produccion = 1
        produccion = 2
        post_produccion = 3
    End Enum

    Public Enum Estado_Proforma As Integer
        Anulado = 0
        Genereado = 1
        Aprobado = 2
    End Enum

    Public Sub Listar_Personal(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.* from personas a " & _
                   "join usuarios b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona and tipo_usuario = 0 " & _
                   "where a.id_empresa = " & Id_Empresa & " and estado = 1 order by id_persona;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Elementos_PreProduccion(ByVal obj As Object, ByVal servic As Servicio)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select * from elementos where id_empresa = " & Id_Empresa & " and id_medida <> 5 and id_servicio = " & servic & " order by orden;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Clientes(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_cliente, cliente from clientes where id_empresa = " & Id_Empresa & " order by cliente;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Listar_Nombre_Clientes(ByVal id_cliente As String) As DataSet
        SqlTexto = "select id_cliente, cliente from clientes where id_empresa = " & Id_Empresa & " and id_cliente = " & id_cliente & " order by cliente;"
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Function Listar_Elemento_Comision(ByVal id_servicio As Integer) As DataSet
        'comision de talentos
        SqlTexto = "select id_elemento, id_servicio, id_medida, elemento, costo from elementos where id_empresa = " & Id_Empresa & " and id_medida = 5 and id_servicio = " & id_servicio & "; "
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    'Public Function Listar_Elemento_Gastos(ByVal id_servicio As Integer) As DataSet
    '    ' gastos operativos
    '    SqlTexto = "select id_elemento, id_servicio, id_medida, elemento, costo from elementos where id_empresa = " & Id_Empresa & " and id_medida = 6 and id_servicio = " & id_servicio & "; "
    '    Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    'End Function

    Public Sub Listar_Productos(ByVal obj As Object, ByVal id_cliente As String)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_producto, producto from productos where id_empresa = " & Id_Empresa & " and id_cliente = " & id_cliente & " order by id_producto;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    'Public Sub Listar_Proyectos(ByVal obj As Object)
    '    Dim ds As New Data.DataSet
    '    ds = ObjsRegNeg.ConsultaxParametros("select id_proyecto, proyecto from proyectos order by id_proyecto")
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        obj.enabled = True
    '    Else
    '        obj.enabled = Not True
    '    End If
    '    obj.DataSource = ds.Tables(0).DefaultView
    '    ds.Dispose()
    'End Sub

    Public Sub Listar_Tipo_Proyectos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_tipo_proyecto, tipo_proyecto from tipoproyectos where id_empresa = " & Id_Empresa & " order by id_tipo_proyecto;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Formatos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_formato, formato from formatos where id_empresa = " & Id_Empresa & " order by id_formato;")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Categorias(ByVal obj As Object)
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_categoria, concat(descripcion, ' (',categoria, ')') as descripcion from categorias where id_empresa = " & Id_Empresa & " order by id_categoria;")
        'ds = ObjsRegNeg.ConsultaxParametros("select id_categoria, descripcion from categorias order by id_categoria")
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Obtener_Id_Proforma() As Integer
        SqlTexto = "select ifnull(max(id_proforma),0) + 1 from proforma where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Function Obtener_Id_Proforma_Linea_Servicios() As Integer
        SqlTexto = "select ifnull(max(id_proforma_linea_servicio),0) + 1 from proformalineaservicios where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Proforma(ByVal id_proforma As Integer, ByVal descripcion As String, ByVal id_cliente As Integer, ByVal id_producto As Integer, _
                                 ByVal id_categoria As Integer, ByVal id_formato As Integer, ByVal profit As Double, ByVal profit_outside As Double, ByVal participa_inhouse As Double, ByVal participa_outside As Double, ByVal fecha As String, ByVal id_usuario As Integer)
        Try
            'estado: anulado(0), generado(1), aprobado(2)
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_Clientes", cliente, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into proforma (id_proforma, descripcion, id_cliente, id_producto, id_categoria, id_formato, profit, profit_outside, participa_inhouse, participa_outside, fecha, id_usuario, ip, estado, id_empresa) values " & _
                       "(" & id_proforma & ", '" & descripcion.Replace("'", "''") & "', " & id_cliente & ", " & id_producto & ", " & id_categoria & ", " & id_formato & ", " & profit & ",  " & profit_outside & ",  " & participa_inhouse & ",  " & participa_outside & ", '" & fecha & "', " & id_usuario & ", '" & Ip & "', '1', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Insertar_Proforma_Linea_Servicios(ByVal id_proforma_linea_servicio As Integer, ByVal id_proforma As String, ByVal id_servicio As Integer, ByVal id_elemento As Integer, _
                                 ByVal id_medida As Integer, ByVal rate As Double, ByVal unidades As Double, ByVal costo As Double, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_Clientes", cliente, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into proformalineaservicios (id_proforma_linea_servicio, id_proforma, id_servicio, id_elemento, id_medida, rate, unidades, costo, id_usuario, ip, id_empresa) values " & _
                       "(" & id_proforma_linea_servicio & ", " & id_proforma & ", " & id_servicio & ", " & id_elemento & ", " & id_medida & ", " & rate & ", " & unidades & ", " & costo & ", " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Obtener_Id_Proforma_Linea_Personas() As Integer
        SqlTexto = "select ifnull(max(id_proforma_linea_persona),0) + 1 from proformalineapersonal where id_empresa = " & Id_Empresa & "; "
        Return Int(ObjsRegNeg.ConsultaxParametros(SqlTexto).Tables(0).Rows(0)(0).ToString)
    End Function

    Public Sub Insertar_Proforma_Linea_Personal(ByVal id_proforma_linea_persona As Integer, ByVal id_proforma As String, ByVal id_servicio As Integer, ByVal release As Integer, _
                                 ByVal id_persona As Integer, ByVal valor As Double, ByVal horas As Double, ByVal costo As Double, ByVal id_usuario As Integer)
        Try

            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            'ObjsRegNeg.InsertarxActualizaxBorra("Inserta_Clientes", cliente, nombre, direccion, telefono, id_usuario, ip)
            SqlTexto = "insert into proformalineapersonal (id_proforma_linea_persona, id_proforma, id_servicio, `release`, id_persona, valor, horas, costo, id_usuario, ip, id_empresa) values " & _
                       "(" & id_proforma_linea_persona & ", " & id_proforma & ", " & id_servicio & ", " & release & ", " & id_persona & ", " & valor & ", " & horas & ", " & costo & ", " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Modificar_Costo_Elementos(ByVal id_elemento As Integer, ByVal id_servicio As Integer, ByVal costo As Double, ByVal id_usuario As Integer)
        Try
            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)
            SqlTexto = "update elementos set costo = " & costo & ", " & _
                              "id_usuario = " & id_usuario & ", " & _
                              "Ip = '" & Ip & "' " & _
                       "where id_empresa = " & Id_Empresa & " and id_elemento = " & id_elemento & " and id_servicio = " & id_servicio & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Listar_Elementos_Proforma_Preview(ByVal obj As Object, ByVal id_proforma As Integer, ByVal servic As Servicio, ByVal Margen As Double)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_proforma, a.id_servicio, a.id_elemento, a.id_medida, a.rate, a.unidades, a.costo, (a.unidades * a.costo) as total, c.elemento, 0 as facturainho, 0 as facturaousi, " & _
                           "((ifnull(a.unidades,0) * ifnull(a.costo,0)) * (100 + " & Margen & ") / 100) as factura " & _
                   "from proformalineaservicios a " & _
                   "join servicios b on b.id_empresa = a.id_empresa and b.id_servicio = a.id_servicio " & _
                   "join elementos c on c.id_empresa = a.id_empresa and c.id_servicio = a.id_servicio and c.id_elemento = a.id_elemento " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_proforma = " & id_proforma & " and a.id_servicio = " & servic & " order by c.orden; "
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Elementos_Proforma_Preview_JointVenture(ByVal obj As Object, ByVal id_proforma As Integer, ByVal servic As Servicio, ByVal MargenInHo As Double, ByVal MargenOuSi As Double, ByVal ParticipaInHo As Double, ByVal ParticipaOuSi As Double)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_proforma, a.id_servicio, a.id_elemento, a.id_medida, a.rate, a.unidades, a.costo, (a.unidades * a.costo) as total, c.elemento, " & _
                          "(((ifnull(a.unidades,0) * ifnull(a.costo,0)) * (" & ParticipaInHo & " / 100)) * (100 + " & MargenInHo & ") / 100) as facturainho, " & _
                          "(((ifnull(a.unidades,0) * ifnull(a.costo,0)) * (" & ParticipaOuSi & " / 100)) * (100 + " & MargenOuSi & ") / 100) as facturaousi, " & _
                          "((((ifnull(a.unidades,0) * ifnull(a.costo,0)) * (" & ParticipaInHo & " / 100)) * (100 + " & MargenInHo & ") / 100) + " & _
                          "(((ifnull(a.unidades,0) * ifnull(a.costo,0)) * (" & ParticipaOuSi & " / 100)) * (100 + " & MargenOuSi & ") / 100)) as factura " & _
                   "from proformalineaservicios a " & _
                   "join servicios b on b.id_empresa = a.id_empresa and b.id_servicio = a.id_servicio " & _
                   "join elementos c on b.id_empresa = a.id_empresa and c.id_servicio = a.id_servicio and c.id_elemento = a.id_elemento " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_proforma = " & id_proforma & " and a.id_servicio = " & servic & " order by c.orden; "
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Costo_Horas_Personal_Proforma_Preview(ByVal obj As Object, ByVal id_proforma As Integer, ByVal Margen As Double, ByVal id_servicio As Servicio)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_proforma, a.id_servicio, a.id_persona, a.valor as costo, a.horas as unidades, (a.horas * a.valor) as total, b.persona, 0 as facturainho, 0 as facturaousi, " & _
                           "((ifnull(a.horas,0) * ifnull(a.valor,0)) * (100 + " & Margen & ") / 100) as factura " & _
                    "from proformalineapersonal a " & _
                    "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                    "where a.id_empresa = " & Id_Empresa & " and a.id_proforma = " & id_proforma & " and a.id_servicio = " & id_servicio & " " & _
                    "order by a.id_persona;"

        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Costo_Horas_Personal_Proforma_Preview_JointVenture(ByVal obj As Object, ByVal id_proforma As Integer, ByVal MargenInHo As Double, ByVal MargenOuSi As Double, ByVal ParticipaInHo As Double, ByVal ParticipaOuSi As Double, ByVal id_servicio As Servicio)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_proforma, a.id_servicio, a.id_persona, a.valor as costo, a.horas as unidades, (a.horas * a.valor) as total, b.persona, " & _
                          "(((ifnull(a.horas,0) * ifnull(a.valor,0)) * (" & ParticipaInHo & " / 100)) * (100 + " & MargenInHo & ") / 100) as facturainho, " & _
                          "(((ifnull(a.horas,0) * ifnull(a.valor,0)) * (" & ParticipaOuSi & " / 100)) * (100 + " & MargenOuSi & ") / 100) as facturaousi, " & _
                          "((((ifnull(a.horas,0) * ifnull(a.valor,0)) * (" & ParticipaInHo & " / 100)) * (100 + " & MargenInHo & ") / 100) + " & _
                          "(((ifnull(a.horas,0) * ifnull(a.valor,0)) * (" & ParticipaOuSi & " / 100)) * (100 + " & MargenOuSi & ") / 100)) as factura " & _
                    "from proformalineapersonal a " & _
                    "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                    "where a.id_empresa = " & Id_Empresa & " and a.id_proforma = " & id_proforma & " and a.id_servicio = " & id_servicio & " " & _
                    "order by a.id_persona;"

        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Proformas(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_proforma, a.descripcion, a.id_cliente, a.id_producto, a.id_categoria, a.id_formato, " & _
                          "a.fecha, a.id_usuario, a.fecha_creacion, a.ip, " & _
                          "case a.estado when  '0' then 'Anulado' when '1' then 'Generado' when '2' then 'Aprobado' end as estado, " & _
                          "b.cliente, c.producto, d.categoria, e.formato, " & _
                          "((select ifnull(sum((unidades * costo)),0) from proformalineaservicios where id_empresa = a.id_empresa and id_proforma = a.id_proforma) + " & _
                          " (select ifnull(sum((horas * valor)),0) from proformalineapersonal where id_empresa = a.id_empresa and id_proforma = a.id_proforma)) as total, a.profit, profit_outside, participa_inhouse, participa_outside " & _
                   "from proforma a " & _
                   "join clientes b on b.id_empresa = a.id_empresa and b.id_cliente = a.id_cliente " & _
                   "join productos c on c.id_empresa = a.id_empresa and c.id_producto = a.id_producto and c.id_cliente = a.id_cliente " & _
                   "join categorias d on d.id_empresa = a.id_empresa and d.id_categoria = a.id_categoria " & _
                   "join formatos e on e.id_empresa = a.id_empresa and e.id_formato = a.id_formato " & _
                   "where a.id_empresa = " & Id_Empresa & " order by a.id_proforma;"

        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Anular_Modifica_Activa_Proforma(ByVal id_proforma As Integer, ByVal Estado As Estado_Proforma) As String
        Try
            SqlTexto = "update proforma set estado = '" & Estado & "' where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            Return "exito"
        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Function

    Public Sub Listar_Costo_Horas_Personal(ByVal id_categoria As Integer, ByVal id_servicios As String, ByVal Margen As Double, ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select a.id_persona, b.persona, ifnull(a.horas,0) as unidades, ifnull(b.costo,0) as costo, " & _
                          "(ifnull(a.horas,0) * ifnull(b.costo,0)) as total, 0 as facturainho, 0 as facturaousi, " & _
                          "((ifnull(a.horas,0) * ifnull(b.costo,0)) * (100 + " & Margen & ") / 100) as factura " & _
                   "from horaspersonaformato a " & _
                   "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                   "join categorias c on c.id_empresa = a.id_empresa and c.id_categoria = a.id_categoria " & _
                   "join servicios d on d.id_empresa = a.id_empresa and d.id_servicio = a.id_servicio " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_categoria = " & id_categoria & " and a.id_servicio in (" & id_servicios & ") and a.activo = 1 and a.horas > 0 " & _
                   "group by a.id_persona, b.persona, ifnull(b.costo,0) " & _
                   "order by a.id_persona;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Sub Listar_Costo_Horas_Personal_JointVenture(ByVal id_categoria As Integer, ByVal id_servicios As String, ByVal MargenInHo As Double, ByVal MargenOuSi As Double, ByVal ParticipaInHo As Double, ByVal ParticipaOuSi As Double, ByVal obj As Object)
        Dim ds As New Data.DataSet
        '"((ifnull(a.horas,0) * ifnull(b.costo,0)) * (100 + " & MargenInHo & ") / 100) as factura " & _

        SqlTexto = "select a.id_persona, b.persona, ifnull(a.horas,0) as unidades, ifnull(b.costo,0) as costo, " & _
                          "(ifnull(a.horas,0) * ifnull(b.costo,0)) as total, " & _
                          "(((ifnull(a.horas,0) * ifnull(b.costo,0)) * (" & ParticipaInHo & " / 100)) * (100 + " & MargenInHo & ") / 100) as facturainho, " & _
                          "(((ifnull(a.horas,0) * ifnull(b.costo,0)) * (" & ParticipaOuSi & " / 100)) * (100 + " & MargenOuSi & ") / 100) as facturaousi, " & _
                          "((((ifnull(a.horas,0) * ifnull(b.costo,0)) * (" & ParticipaInHo & " / 100)) * (100 + " & MargenInHo & ") / 100) + " & _
                          "(((ifnull(a.horas,0) * ifnull(b.costo,0)) * (" & ParticipaOuSi & " / 100)) * (100 + " & MargenOuSi & ") / 100)) as factura " & _
                   "from horaspersonaformato a " & _
                   "join personas b on b.id_empresa = a.id_empresa and b.id_persona = a.id_persona " & _
                   "join categorias c on c.id_empresa = a.id_empresa and c.id_categoria = a.id_categoria " & _
                   "join servicios d on d.id_empresa = a.id_empresa and d.id_servicio = a.id_servicio " & _
                   "where a.id_empresa = " & Id_Empresa & " and a.id_categoria = " & id_categoria & " and a.id_servicio in (" & id_servicios & ") and a.activo = 1 and a.horas > 0 " & _
                   "group by a.id_persona, b.persona, ifnull(b.costo,0) " & _
                   "order by a.id_persona;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Listar_Proforma_Editar(ByVal id_proforma As String) As DataSet
        SqlTexto = "select * from proforma where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & "; "
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Function Listar_Proforma_Linea_Personal_Editar(ByVal id_proforma As Integer, ByVal id_servicio As Integer) As DataSet
        SqlTexto = "select * from proformalineapersonal where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & " and id_servicio = " & id_servicio & "; "
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Function Listar_Proforma_Linea_Personal_Default(ByVal id_proforma As Integer, ByVal id_servicio As Integer, ByVal id_categoria As Integer) As DataSet
        SqlTexto = "select * from proformalineapersonal_default where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & " and id_servicio = " & id_servicio & " and id_categoria = " & id_categoria & "; "
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Function Listar_Proforma_Linea_Servicio_Editar(ByVal id_proforma As String, ByVal id_servicio As Integer) As DataSet
        SqlTexto = "select * from proformalineaservicios where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & " and id_servicio = " & id_servicio & "; "
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Function Listar_Proforma_Linea_Servicio_Default(ByVal id_proforma As String, ByVal id_servicio As Integer, ByVal id_categoria As Integer) As DataSet
        SqlTexto = "select * from proformalineaservicios_default where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & " and id_servicio = " & id_servicio & " and id_categoria = " & id_categoria & "; "
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Sub Eliminar_Proforma(ByVal id_proforma As Integer)
        Try
            ' eliminamos proformalineaservicios
            SqlTexto = "delete from proformalineaservicios where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            ' eliminamos proformalineapersonal
            SqlTexto = "delete from proformalineapersonal where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            ' eliminamos proforma
            SqlTexto = "delete from proforma where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Categoria_Modo_Proceso_Manual(ByVal id_categoria As Integer) As Boolean
        '0=AUTOMATICO
        '1=MANUAL
        Dim ds As New Data.DataSet
        SqlTexto = "select * from categorias where id_empresa = " & Id_Empresa & " and id_categoria = " & id_categoria & " and modo_proceso = 1;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return Not True
        End If
        ds.Dispose()
    End Function

    Public Function Lista_Id_Categorias_Manual() As String
        '0=AUTOMATICO
        '1=MANUAL
        Dim ds As New Data.DataSet
        Dim Cadena As String = ""
        SqlTexto = "select id_categoria from categorias where id_empresa = " & Id_Empresa & " and modo_proceso = 1;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim MiTabla As DataTable = ds.Tables(0)
            For Each MiDataRow As DataRow In MiTabla.Rows
                Dim id_categoria As String = MiDataRow("id_categoria").ToString
                Cadena &= id_categoria & ","
            Next
        End If
        ds.Dispose()
        If cadena <> "" Then cadena = Left(cadena, Len(cadena) - 1)
        Return cadena
    End Function

    Public Function Listar_Servicios() As DataSet
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select servicio from servicios where id_empresa = " & Id_Empresa & " order by id_servicio")
        Return ds
    End Function

    Public Function Listar_Id_Clientes_x_Nombre(ByVal Nombre_Cliente As String) As Integer
        Dim ds As New Data.DataSet
        Dim Id_Cliente As Integer = 0
        ds = ObjsRegNeg.ConsultaxParametros("select id_cliente from clientes where id_empresa = " & Id_Empresa & " and rtrim(ltrim(cliente)) = '" & Nombre_Cliente & "';")
        If ds.Tables(0).Rows.Count > 0 Then
            Id_Cliente = ds.Tables(0).Rows(0)(0).ToString()
        End If
        ds.Dispose()
        Return Id_Cliente
    End Function

    Public Function Listar_Proforma_Elementos_Personal(ByVal id_proforma As Integer) As DataSet
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_empresa, id_proforma, id_servicio, id_elemento, id_persona, costo, horas from proforma_elementos_personal where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & " order by id_servicio, id_elemento, id_persona")
        Return ds
    End Function

    Public Function Listar_Proforma_Elementos_Personal_Default(ByVal id_proforma As Integer, ByVal id_categoria As Integer) As DataSet
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_empresa, id_proforma, id_servicio, id_elemento, id_persona, costo, horas from proforma_elementos_personal_default where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & " and id_categoria = " & id_categoria & " order by id_servicio, id_elemento, id_persona")
        Return ds
    End Function

    Public Sub Insertar_Proforma_Elementos_Personal(ByVal id_proforma As Integer, ByVal id_servicio As Integer, ByVal id_elemento As Integer, _
                             ByVal id_persona As Integer, ByVal costo As Double, ByVal horas As Double, ByVal id_usuario As Integer)
        Try

            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "insert into proforma_elementos_personal (id_proforma, id_servicio, id_elemento, id_persona, costo, horas, id_usuario, ip, id_empresa) values " & _
                       "(" & id_proforma & ", " & id_servicio & ", " & id_elemento & ", " & id_persona & ", " & costo & ", " & horas & ", " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Eliminar_Proforma_Elementos_Personal(ByVal id_proforma As Integer)
        Try
            ' eliminamos proformalineaservicios
            SqlTexto = "delete from proforma_elementos_personal where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Listar_Gastos_Operativos(ByVal obj As Object)
        Dim ds As New Data.DataSet
        SqlTexto = "select id_gasto, descripcion, costo, id_usuario, ip from gastos_operativos where id_empresa = " & Id_Empresa & " order by id_gasto;"
        ds = ObjsRegNeg.ConsultaxParametros(SqlTexto)
        If ds.Tables(0).Rows.Count > 0 Then
            obj.enabled = True
        Else
            obj.enabled = Not True
        End If
        obj.DataSource = ds.Tables(0).DefaultView
        ds.Dispose()
    End Sub

    Public Function Listar_Proforma_Elementos_Gastos(ByVal id_proforma As Integer) As DataSet
        Dim ds As New Data.DataSet
        ds = ObjsRegNeg.ConsultaxParametros("select id_empresa, id_proforma, id_servicio, id_elemento, id_gasto, costo from proforma_gastos_operativos where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & " order by id_servicio, id_elemento, id_gasto")
        Return ds
    End Function

    Public Sub Eliminar_Proforma_Elementos_Gastos(ByVal id_proforma As Integer)
        Try
            ' eliminamos proformalineaservicios
            SqlTexto = "delete from proforma_gastos_operativos where id_empresa = " & Id_Empresa & " and id_proforma = " & id_proforma & ";"
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Sub Insertar_Proforma_Elementos_Gastos(ByVal id_proforma As Integer, ByVal id_servicio As Integer, ByVal id_elemento As Integer, _
                         ByVal id_gasto As Integer, ByVal costo As Double, ByVal id_usuario As Integer)
        Try

            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            SqlTexto = "insert into proforma_gastos_operativos (id_proforma, id_servicio, id_elemento, id_gasto, costo, id_usuario, ip, id_empresa) values " & _
                       "(" & id_proforma & ", " & id_servicio & ", " & id_elemento & ", " & id_gasto & ", " & costo & ", " & id_usuario & ", '" & Ip & "', " & Id_Empresa & ");"

            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)
        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Public Function Duplicar_Proforma(ByVal Id_Proforma As Integer, ByVal Fecha As String, ByVal Id_Usuario As Integer) As Integer
        Try
            Dim ds As New Data.DataSet
            ds = ObjsRegNeg.ConsultaxParametros("select * from proforma where id_empresa = " & Id_Empresa & " and id_proforma = " & Id_Proforma & "; ")
            If ds.Tables(0).Rows.Count <= 0 Then
                Return 0
                Exit Function
            End If

            Dim Ip As String = ""
            Ip = ObjsRegNeg.HostIP(System.Net.Dns.GetHostName)

            ' duplicamos tabla proforma
            Dim id_proforma_max As Integer = Obtener_Id_Proforma()
            SqlTexto = "insert into proforma select id_empresa," & id_proforma_max & ",descripcion,id_cliente,id_producto,id_categoria,id_formato,'" & Fecha & "','" & Id_Usuario & "','" & Fecha & "','" & Ip & "',estado,profit,profit_outside,participa_inhouse,participa_outside from proforma where id_empresa = " & Id_Empresa & " and id_proforma = " & Id_Proforma & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            ' duplicamos tabla proformalineapersonal
            Dim id_proforma_linea_persona_max As Integer = Obtener_Id_Proforma_Linea_Personas()
            SqlTexto = "insert into proformalineapersonal select id_empresa, id_proforma_linea_persona + " & id_proforma_linea_persona_max & ", " & id_proforma_max & ", id_servicio, `release`, id_persona, valor, horas, costo, " & Id_Usuario & ", '" & Fecha & "', '" & Ip & "' from proformalineapersonal where id_empresa = " & Id_Empresa & " and id_proforma = " & Id_Proforma & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            ' duplicamos tabla proformalineaservicios
            Dim id_proforma_linea_servicio_max As Integer = Obtener_Id_Proforma_Linea_Servicios()
            SqlTexto = "insert into proformalineaservicios select id_empresa, id_proforma_linea_servicio + " & id_proforma_linea_servicio_max & ", " & id_proforma_max & ", secuencia, `release`, id_servicio, id_elemento, id_medida, rate, unidades, costo, " & Id_Usuario & ", '" & Fecha & "', '" & Ip & "' from proformalineaservicios where id_empresa = " & Id_Empresa & " and id_proforma = " & Id_Proforma & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            ' duplicamos tabla proforma_gastos_operativos
            SqlTexto = "insert into proforma_gastos_operativos select id_empresa, " & id_proforma_max & ", id_servicio, id_elemento, id_gasto, costo, " & Id_Usuario & ", '" & Fecha & "', '" & Ip & "' from proforma_gastos_operativos where id_empresa = " & Id_Empresa & " and id_proforma = " & Id_Proforma & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            ' duplicamos tabla proforma_elementos_personal
            SqlTexto = "insert into proforma_elementos_personal select id_empresa, " & id_proforma_max & ", id_servicio, id_elemento, id_persona, costo, horas, " & Id_Usuario & ", '" & Fecha & "', '" & Ip & "' from proforma_elementos_personal where id_empresa = " & Id_Empresa & " and id_proforma = " & Id_Proforma & "; "
            ObjsRegNeg.InsertarxActualizaxBorra(SqlTexto)

            Return id_proforma_max

        Catch ex As Exception
            Throw New Exception("Cls_Proforma :" & vbCrLf & ex.Message, ex)
        End Try

    End Function
End Class
