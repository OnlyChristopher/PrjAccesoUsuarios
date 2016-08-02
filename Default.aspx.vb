Imports System.Data
Imports System.Data.SqlClient
Imports system.IO

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim Cnx As New SqlConnection(Conexion)
    Private TextoSql As String
    Private Sr As StreamReader

    Private Estado_Id As Integer
    Private Usuario_Id_Mod As Integer
    Private Usuario_Id As Integer
    Private Usuario_Seleccionado As Integer
    Private Ip As String = ""

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        'ChkAccesoCaja.Attributes.Add("onclick", "ShowHide(this);")
        If Not Page.IsPostBack Then
            Call Cargar_Usuarios()
            Call Cargar_EstadosUsuario()
            Call Cargar_Datos_Usuario()
            Call Cargar_RolUsuarios(Usuario_Seleccionado)
            Call Cargar_Acceso_Modulos(Usuario_Seleccionado)
            Call Cargar_Acceso_TipoOperaciones(Usuario_Seleccionado)
            Call Mostrar_Acceso_Fraccionamiento(Usuario_Seleccionado)
            Call Mostrar_Acceso_MultasAdm(Usuario_Seleccionado)
            'caja
            Call Cargar_Agencias_Caja()
            Call Cargar_Estado_Caja()
            Call Cargar_Tipo_Usuario_Caja(Usuario_Seleccionado)
            Call Cargar_Condicion_Usuario_Caja()
            Call Cargar_Caja_Acceso_Opciones_MenuPrincipal(Usuario_Seleccionado)
        End If
    End Sub

    Private Sub Cargar_Usuarios()
        TextoSql = "select login as campo1, " & _
                          "convert(varchar(3),usuario_id) +'|'+ ape_paterno +'|'+ ape_materno +'|'+ nombre +'|'+ convert(varchar(1),flag_activo) as campo2 " & _
                   "from tg_usuario order by login "
        Dim da1 = New SqlDataAdapter(TextoSql, Cnx)
        Dim ds1 = New DataSet
        da1.Fill(ds1)
        DdlUsuario.DataSource = ds1.Tables(0).DefaultView
        DdlUsuario.DataTextField = "campo1"
        DdlUsuario.DataValueField = "campo2"
        DdlUsuario.DataBind()
    End Sub

    Private Sub Cargar_Agencias_Caja()
        TextoSql = "select convert(varchar(1),agencia_id) +' - '+ descripcion as campo1, agencia_id " & _
                   "from re_agencia"
        Dim daeu = New SqlDataAdapter(TextoSql, Cnx)
        Dim dseu = New DataSet
        daeu.Fill(dseu)
        DdlAgencia.DataSource = dseu.Tables(0).DefaultView
        DdlAgencia.DataTextField = "campo1"
        DdlAgencia.DataValueField = "agencia_id"
        DdlAgencia.DataBind()
    End Sub

    Private Sub Cargar_Estado_Caja()
        TextoSql = "select convert(varchar(1),estado_id) +' - '+ descripcion as campo1, estado_id " & _
                   "from re_estado_caja"
        Dim daeu = New SqlDataAdapter(TextoSql, Cnx)
        Dim dseu = New DataSet
        daeu.Fill(dseu)
        DdlEstadoCaja.DataSource = dseu.Tables(0).DefaultView
        DdlEstadoCaja.DataTextField = "campo1"
        DdlEstadoCaja.DataValueField = "estado_id"
        DdlEstadoCaja.DataBind()
    End Sub

    Private Sub Cargar_EstadosUsuario()
        TextoSql = "select convert(varchar(1),condicion_usuario_id) +' - '+ descripcion as campo1, condicion_usuario_id " & _
                   "from re_condicion_usuario"
        Dim daeu = New SqlDataAdapter(TextoSql, Cnx)
        Dim dseu = New DataSet
        daeu.Fill(dseu)
        DdlEstadoUsuario.DataSource = dseu.Tables(0).DefaultView
        DdlEstadoUsuario.DataTextField = "campo1"
        DdlEstadoUsuario.DataValueField = "condicion_usuario_id"
        DdlEstadoUsuario.DataBind()
    End Sub

    Private Sub Cargar_RolUsuarios(ByVal g_Usuario_Id As String)
        TextoSql = "select convert(varchar(1),rol_id) +' - '+ desc_corta as campo1, rol_id " & _
                   "from sg_rol where estado_id = 1"
        Dim da3 = New SqlDataAdapter(TextoSql, Cnx)
        Dim ds3 = New DataSet
        da3.Fill(ds3)
        DdlRolUsuario.DataSource = ds3.Tables(0).DefaultView
        DdlRolUsuario.DataTextField = "campo1"
        DdlRolUsuario.DataValueField = "rol_id"
        DdlRolUsuario.DataBind()

        TextoSql = "select rol_id from sg_rol_usuario where usuario_id = '" & g_Usuario_Id & "' and estado_id = '1'"
        Dim da4 = New SqlDataAdapter(TextoSql, Cnx)
        Dim ds4 = New DataSet
        da4.Fill(ds4)
        If ds4.Tables(0).Rows.Count > 0 Then
            DdlRolUsuario.SelectedValue = ds4.Tables(0).Rows(0)("rol_id")
        End If
    End Sub

    Private Sub Cargar_Acceso_Modulos(ByVal g_Usuario_Id As String)
        TextoSql = "select a.modulo_id, a.descripcion, (case when b.modulo_id is null then 'false' else 'true' end) as acceso " & _
                   "from tg_modulo a " & _
                   "left join (select distinct modulo_id, usuario_id " & _
                              "from tg_modulo_usuario) b on b.modulo_id = a.modulo_id and b.usuario_id = " & g_Usuario_Id & " " & _
                   "where a.estado_id = 1"
        Dim damo = New SqlDataAdapter(TextoSql, Cnx)
        Dim dsmo = New DataSet
        damo.Fill(dsmo)
        DgvModulo.DataSource = dsmo.Tables(0).DefaultView
        DgvModulo.DataBind()
    End Sub

    Private Sub Cargar_Acceso_TipoOperaciones(ByVal g_Usuario_Id As String)
        TextoSql = "select a.tipo_operacion_id, a.descripcion, (case when b.tipo_operacion_id is null then 'false' else 'true' end) as acceso " & _
                   "from tg_tipo_operacion a " & _
                   "left join (select distinct usuario_id, tipo_operacion_id " & _
                              "from tg_usuario_acceso " & _
                              "where usuario_id = " & g_Usuario_Id & " and estado_id = 1) b on b.tipo_operacion_id = a.tipo_operacion_id " & _
                   "where Estado_Id = 1"
        Dim dato = New SqlDataAdapter(TextoSql, Cnx)
        Dim dsto = New DataSet
        dato.Fill(dsto)
        DgvTipoOperacion.DataSource = dsto.Tables(0).DefaultView
        DgvTipoOperacion.DataBind()
    End Sub

    Private Sub Mostrar_Acceso_Fraccionamiento(ByVal g_Usuario_Id As String)
        TextoSql = "select * from fr_acceso where usuario_id = " & g_Usuario_Id
        Dim dafr = New SqlDataAdapter(TextoSql, Cnx)
        Dim dsfr = New DataSet
        Dim ValorAcc() As String
        Dim i As Integer
        dafr.Fill(dsfr)
        ChkAccesoSolicitudFR.Checked = False
        ChkAccesoReportesFR.Checked = False
        If dsfr.Tables(0).Rows.Count > 0 Then
            ValorAcc = Split(dsfr.Tables(0).Rows(0)("opciones"), ",")
            For i = 0 To ValorAcc.Length - 1
                If ValorAcc(i).ToString = "1" Then ChkAccesoSolicitudFR.Checked = True
                If ValorAcc(i).ToString = "2" Then ChkAccesoReportesFR.Checked = True
            Next
        End If
    End Sub

    Private Sub Mostrar_Acceso_MultasAdm(ByVal g_Usuario_Id As String)
        TextoSql = "select * from ma_h_acceso_editar where usuario_id = " & g_Usuario_Id
        Dim dama = New SqlDataAdapter(TextoSql, Cnx)
        Dim dsma = New DataSet
        Dim i As Integer = 0
        dama.Fill(dsma)
        If dsma.Tables(0).Rows.Count > 0 Then
            ChkEditaMultasAdm.Checked = True
        Else
            ChkEditaMultasAdm.Checked = False
        End If
    End Sub

    Private Sub Cargar_Tipo_Usuario_Caja(ByVal g_Usuario_Id As String)
        If g_Usuario_Id = "" Then
            ' Falta recuperar el valor de tipo cajero para ponerlo en la condicion.
            ' Recuperar o Listar opciones
            TextoSql = "select convert(varchar(1),tipo_usuario_id) +' - '+ descripcion as campo1, tipo_usuario_id from re_tipo_usuario "
        Else
            TextoSql = "sp_cargar_tipo_usuario " & g_Usuario_Id
        End If

        Dim datu = New SqlDataAdapter(TextoSql, Cnx)
        Dim dstu = New DataSet
        datu.Fill(dstu)
        DdlTipoUsuarioCaja.DataSource = dstu.Tables(0).DefaultView
        DdlTipoUsuarioCaja.DataTextField = "campo1"
        DdlTipoUsuarioCaja.DataValueField = "tipo_usuario_id"
        DdlTipoUsuarioCaja.DataBind()
    End Sub

    Private Sub Cargar_Condicion_Usuario_Caja()
        TextoSql = "select convert(varchar(1),condicion_usuario_id) +' - '+ descripcion as campo1, condicion_usuario_id " & _
                   "from re_condicion_usuario"
        Dim daco = New SqlDataAdapter(TextoSql, Cnx)
        Dim dsco = New DataSet
        daco.Fill(dsco)
        DdlCondicionUsuarioCaja.DataSource = dsco.Tables(0).DefaultView
        DdlCondicionUsuarioCaja.DataTextField = "campo1"
        DdlCondicionUsuarioCaja.DataValueField = "condicion_usuario_id"
        DdlCondicionUsuarioCaja.DataBind()
    End Sub

    Private Sub Cargar_Caja_Acceso_Opciones_MenuPrincipal(ByVal g_Usuario_Id As String)
        TextoSql = "stp_Select_acceso_caja " & g_Usuario_Id
        Dim damp = New SqlDataAdapter(TextoSql, Cnx)
        Dim dsmp = New DataSet
        damp.Fill(dsmp)
        DgvCajaMenuPrincipal.DataSource = dsmp.Tables(0).DefaultView
        DgvCajaMenuPrincipal.DataBind()

        DgvCajaAccesoReporte.DataSource = dsmp.Tables(1).DefaultView
        DgvCajaAccesoReporte.DataBind()

        If dsmp.Tables(3).Rows.Count > 0 Then
            If dsmp.Tables(2).Rows.Count > 0 Then
                DdlAgencia.SelectedValue = dsmp.Tables(2).Rows(0)("agencia_id")
                DdlEstadoCaja.SelectedValue = dsmp.Tables(2).Rows(0)("estado_id")
                TxtIP.Text = dsmp.Tables(2).Rows(0)("ip")
            End If
            If dsmp.Tables(3).Rows(0)("estado_id") = 1 Then
                ChkAccesoCaja.Checked = True
                Panel2.Visible = True
            Else
                ChkAccesoCaja.Checked = False
                Panel2.Visible = False
            End If
        Else
            ChkAccesoCaja.Checked = False
            Panel2.Visible = False
        End If
    End Sub

    Private Sub Cargar_Datos_Usuario()
        'recuperamos los datos guardados en DdlUsuario.SelectedValue
        'usuario_id|ape_paterno|ape_materno|nombre|flag_activo
        Dim DatosUsu As String
        Dim i As Integer
        Dim Valor_Estado As Integer

        DatosUsu = DdlUsuario.SelectedValue
        Valor_Estado = Left(StrReverse(DatosUsu), 1)
        DdlEstadoUsuario.SelectedValue = Valor_Estado

        For i = 0 To 3
            If i = 0 Then Usuario_Seleccionado = Left(DatosUsu, InStr(DatosUsu, "|") - 1)
            If i = 1 Then LblPaterno.Text = Left(DatosUsu, InStr(DatosUsu, "|") - 1)
            If i = 2 Then LblMaterno.Text = Left(DatosUsu, InStr(DatosUsu, "|") - 1)
            If i = 3 Then LblNombres.Text = Left(DatosUsu, InStr(DatosUsu, "|") - 1)
            DatosUsu = Right(DatosUsu, Len(DatosUsu) - InStr(DatosUsu, "|"))
        Next
    End Sub

    Private Function Conexion() As String
        Dim line As String = ""
        Dim conta As Integer = 0
        Dim servername As String = ""
        Dim DatabaseName As String = ""
        Dim UserID As String = ""
        Dim Password As String = ""
        Dim TxtCon As String = ""

        Try
            'Sr = New StreamReader("C:\INFOSAT\WebMultasAdministrativas\modulo\Edita_Multas\Conexion.cnx")
            'Sr = New StreamReader("E:\Proyectos_VbNET\PrjAccesoUsuarios\Conexion.cnx")
            Sr = New StreamReader("F:\Proyectos\PrjAccesoUsuarios\Conexion.cnx")
            Do While Not line Is Nothing
                line = Sr.ReadLine()
                conta = conta + 1
                If conta = 1 Then servername = line
                If conta = 2 Then DatabaseName = line
                If conta = 3 Then UserID = line
                If conta = 4 Then Password = line
            Loop
            Sr.Close()
            TxtCon = "server=" + servername + "; uid=" + UserID + "; pwd=" + Password + "; database=" + DatabaseName
            'MessageBox(txtcon)
            'Cnx = New SqlConnection(TxtCon)
            'Cnx.Open()
            Return TxtCon 'True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetAppPath() As String
        Dim l_intCharPos As Integer = 0, l_intReturnPos As Integer
        Dim l_strAppPath As String

        l_strAppPath = System.Reflection.Assembly.GetExecutingAssembly.Location()

        While (1)
            l_intCharPos = InStr(l_intCharPos + 1, l_strAppPath, "\", CompareMethod.Text)
            If l_intCharPos = 0 Then
                If Right(Mid(l_strAppPath, 1, l_intReturnPos), 1) <> "\" Then
                    Return Mid(l_strAppPath, 1, l_intReturnPos) & "\"
                Else
                    Return Mid(l_strAppPath, 1, l_intReturnPos)
                End If
                Exit Function
            End If
            l_intReturnPos = l_intCharPos
        End While
        Return ""
    End Function

    Private Function HostIP(ByVal mStrHost As String) As String
        Dim IpCliente As String
        IpCliente = Request.ServerVariables("HTTP_X_FORWARDED_FOR") ' se chequea si hay un proxy
        If IpCliente = "" Then IpCliente = Request.ServerVariables("REMOTE_ADDR")
        Return IpCliente
    End Function

    Protected Sub DdlUsuario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlUsuario.SelectedIndexChanged
        Call Cargar_EstadosUsuario()
        Call Cargar_Datos_Usuario()
        Call Cargar_RolUsuarios(Usuario_Seleccionado)
        Call Cargar_Acceso_Modulos(Usuario_Seleccionado)
        Call Cargar_Acceso_TipoOperaciones(Usuario_Seleccionado)
        Call Mostrar_Acceso_Fraccionamiento(Usuario_Seleccionado)
        Call Mostrar_Acceso_MultasAdm(Usuario_Seleccionado)
        'caja
        Call Cargar_Tipo_Usuario_Caja(Usuario_Seleccionado)
        Call Cargar_Condicion_Usuario_Caja()
        Call Cargar_Caja_Acceso_Opciones_MenuPrincipal(Usuario_Seleccionado)
    End Sub

    Protected Sub BtnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGrabar.Click
        BtnGrabar.Enabled = False
        Ip = HostIP(System.Net.Dns.GetHostName)
        Try
            Call Cargar_Datos_Usuario()
            TextoSql = "Stp_Grabar_Accesos_Usuarios_tmp"
            Dim dag As New System.Data.SqlClient.SqlDataAdapter
            Dim dsg As New System.Data.DataSet
            Dim Cmd As New SqlClient.SqlCommand
            With Cmd
                .CommandText = TextoSql
                .Connection = Cnx
                .CommandTimeout = 0
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@usuario_id", SqlDbType.Int).Value = Usuario_Seleccionado
                .Parameters.Add("@flag_activo", SqlDbType.Char, 1).Value = DdlEstadoUsuario.SelectedValue
                .Parameters.Add("@rol_id", SqlDbType.Int).Value = DdlRolUsuario.SelectedValue
                .Parameters.Add("@acceso_modulo", SqlDbType.VarChar, 100).Value = Cadena_Accesos(DgvModulo)
                .Parameters.Add("@acceso_tipo_operacion", SqlDbType.VarChar, 100).Value = Cadena_Accesos(DgvTipoOperacion)
                .Parameters.Add("@acceso_fraccionamiento", SqlDbType.VarChar, 100).Value = Cadena_Acceso_Fracc()
                .Parameters.Add("@acceso_edita_multa", SqlDbType.Int).Value = ChkEditaMultasAdm.Checked
                .Parameters.Add("@acceso_caja", SqlDbType.Int).Value = ChkAccesoCaja.Checked
                .Parameters.Add("@tipo_usuario_caja", SqlDbType.Int).Value = Val(DdlTipoUsuarioCaja.SelectedValue.ToString)
                .Parameters.Add("@condicion_usuario_caja", SqlDbType.Int, 100).Value = DdlCondicionUsuarioCaja.SelectedValue
                .Parameters.Add("@acceso_caja_menu_principal", SqlDbType.VarChar, 100).Value = Cadena_Accesos(DgvCajaMenuPrincipal)
                .Parameters.Add("@acceso_caja_reportes", SqlDbType.VarChar, 100).Value = Cadena_Accesos(DgvCajaAccesoReporte)
                .Parameters.Add("@usuario_actualizacion_id", SqlDbType.Int).Value = Usuario_Id
                .Parameters.Add("@terminal", SqlDbType.VarChar, 20).Value = Ip
                .Parameters.Add("@agencia_id", SqlDbType.Int, 20).Value = DdlAgencia.SelectedValue
                .Parameters.Add("@estadocaja", SqlDbType.Int, 20).Value = DdlEstadoCaja.SelectedValue
                .Parameters.Add("@ipcaja", SqlDbType.VarChar, 20).Value = TxtIP.Text.Trim
            End With
            dag.SelectCommand = Cmd
            dag.Fill(dsg)
            Dim x As String = ""

            If dsg.Tables(0).Rows.Count > 0 Then
                x = dsg.Tables(0).Rows(0)(1).ToString
                If dsg.Tables(0).Rows(0)(0) = "1" Then
                    Call MessageBox(dsg.Tables(0).Rows(0)(1))
                    'lblMensaje.Text = dsg.Tables(0).Rows(0)(1) --
                    'Response.Redirect("/WebMultasAdministrativas/modulo/multas_1_lista_multas.aspx?cod=" & Codigo_Multado & "&usuario_id=" & Usuario_Id)
                    BtnGrabar.Enabled = True
                Else
                    Call MessageBox(dsg.Tables(0).Rows(0)(1))
                    BtnGrabar.Enabled = True
                End If
            Else
                Call MessageBox("No existen datos revisar actualizacion")
                BtnGrabar.Enabled = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function Cadena_Accesos(ByVal Dgv As GridView) As String
        Dim Cadena As String = ""
        For Each row As GridViewRow In Dgv.Rows
            ' Access the CheckBox
            Dim cb As CheckBox = row.FindControl("ChkAcceso")
            If cb.Checked Then
                Cadena = Cadena & row.Cells(0).Text & ","
            End If
        Next
        If Cadena.Trim <> "" Then Cadena = Left(Cadena, Len(Cadena) - 1)
        Return cadena
    End Function

    Private Function Cadena_Acceso_Fracc() As String
        Dim cadena As String = ""
        cadena = IIf(ChkAccesoSolicitudFR.Checked, "1", "") & IIf(ChkAccesoReportesFR.Checked, "2", "")
        If Len(cadena) > 1 Then cadena = Replace(cadena, "2", ",2")
        Return cadena
    End Function

    Protected Sub BtnMarcarTodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMarcarTodos.Click
        Check_Estados(True, DgvTipoOperacion)
    End Sub

    Protected Sub BtnDesmarcarTodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesmarcarTodos.Click
        Check_Estados(False, DgvTipoOperacion)
    End Sub

    Protected Sub BtnMarcaTodosMod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMarcaTodosMod.Click
        Check_Estados(True, DgvModulo)
    End Sub

    Protected Sub BtnDesmarcaTodosMod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesmarcaTodosMod.Click
        Check_Estados(False, DgvModulo)
    End Sub

    Protected Sub BtnMarcarTodosCaj_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMarcarTodosCaj.Click
        Check_Estados(True, DgvCajaMenuPrincipal)
        Check_Estados(True, DgvCajaAccesoReporte)
    End Sub

    Protected Sub BtnDescMarcarTodosCaj_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDescMarcarTodosCaj.Click
        Check_Estados(False, DgvCajaMenuPrincipal)
        Check_Estados(False, DgvCajaAccesoReporte)
    End Sub

    Private Sub Check_Estados(ByVal CheckEstado As Boolean, ByVal GrvDatos As GridView)
        ' Iterate through the Products.Rows property
        For Each row As GridViewRow In grvdatos.Rows
            ' Access the CheckBox
            Dim cb As CheckBox = row.FindControl("ChkAcceso")
            If cb IsNot Nothing Then
                cb.Checked = CheckEstado
            End If
        Next
    End Sub

    Protected Sub ChkAccesoCaja_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAccesoCaja.CheckedChanged
        If ChkAccesoCaja.Checked Then
            Panel2.Visible = True
        Else
            Panel2.Visible = False
        End If
    End Sub

    Private Sub MessageBox(ByVal msje As String)
        Page.ClientScript.RegisterStartupScript(Page.GetType, "Mensaje", "<script language ='javascript'>alert('" & msje & "');</script> ")
    End Sub


End Class
