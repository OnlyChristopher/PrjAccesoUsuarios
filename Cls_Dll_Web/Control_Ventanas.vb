Imports Microsoft.VisualBasic
Public Class Control_Ventanas

    '******************************************************************************************************
    '******************************************************************************************************
    '
    '                           INICIO DE LA FUNCI�N DE 
    '                  ACTUALIZAR VENTANA PADRE AL CERRAR VENTANA HIJA
    '
    '
    ' Est� Funci�n se aplica a la ventana padre.
    ' Esta funci�n se encarga de abrir una ventana hija
    ' Est� funci�n se llama desde JavaScript de la siguiente forma:
    '
    '  -->>Dim url As String = "NuevoMensaje.aspx?Categoria=" & Categoria
    '      Dim wh As String = "width=600,height=400"
    '      Ejecutar la funcion JavaScript de Abrir Ventana Hija 
    '      Dim strScript As String = ""
    '      strScript &= "<script language=JavaScript>" & vbCrLf
    '      strScript &= "openChild('" & url & "','" & wh & "')"
    '      strScript &= "</script>" & vbCrLf
    '      Me.Page.ClientScript.RegisterClientScriptBlock(Me.Page.GetType(), "EjecutarAbrirPopUp", strScript)
    '

    Public Sub Abrir_VentanaHija(ByVal pag As Object)

        Dim strScript As String = ""
        strScript &= "<script language=""jscript"">" & vbCrLf
        strScript &= "function openChild(URL,wh)" & vbCrLf
        strScript &= "{" & vbCrLf
        strScript &= "var winName='child'" & vbCrLf
        strScript &= "var winAtts=""toolbar=no,directories=no,top=0"";" & vbCrLf
        strScript &= "myChild = window.open(URL,winName,wh,winAtts);" & vbCrLf
        strScript &= "}" & vbCrLf
        strScript &= "</script>" & vbCrLf

        pag.ClientScript.RegisterClientScriptBlock(pag.GetType(), "VentanaPadre", strScript)

    End Sub

    ' Est� funci�n se aplica a la ventana Hija
    ' Esta funci�n se encarga de Redireccionar en la ventana Padre cuando se cierra la ventana Hija.
    ' En el Html de la p�gina Hija debe incluir * <body onload="setParent()" onunload="reloadParent()"> *
    '
    '   -->> Como entrada recibe la p�gina a la que aplicar el script, la Url de la p�gina Padre,
    '   -->> De forma opcional recibe la categoria.

    Public Sub ActualizarVentanaPadre_AlCerrar(ByVal pag As Object, ByVal url As String, Optional ByVal Categoria As String = "")

        Dim strScript As String = ""
        strScript &= "<script language=""jscript"">" & vbCrLf

        strScript &= "var pWin" & vbCrLf
        strScript &= "function setParent(){" & vbCrLf
        strScript &= "pWin = top.window.opener" & vbCrLf
        strScript &= "}" & vbCrLf

        strScript &= "function reloadParent(){" & vbCrLf
        'strScript &= "pWin.location.reload(true)" & vbCrLf

        If Categoria <> "" Then
            url &= "Categoria=" & Categoria
        End If

        strScript &= "pWin.location.href='" & url & "'" & vbCrLf
        strScript &= "}" & vbCrLf
        strScript &= "</script>" & vbCrLf

        pag.ClientScript.RegisterClientScriptBlock(pag.GetType(), "VentanaHija", strScript)

    End Sub

    '
    '                           FIN DEL SISTEMA DE REDIRECCI�N AL
    '                               CERRAR VENTANA HIJA.
    '
    '******************************************************************************************************
    '******************************************************************************************************

    ' Funci�n para cerrar la ventana del navegador transcurrido un determinado tiempo.
    '
    ' -->> Como entrada recibe la p�gina a la que aplicar la funci�n y el n�mero de segundos 
    '     tras los cuales se cerrar� la ventana del navegador.
    '
    Public Sub CerrarPantalla_TimeOut(ByVal pag As Object, ByVal Segundos As Integer)

        Dim strScript As String
        Segundos = Segundos * 1000

        strScript = "<script type=""text/javascript"">" & vbCrLf
        strScript &= "function cerrar() " & vbCrLf
        strScript &= "{" & vbCrLf
        strScript &= "var ventana = window.self" & vbCrLf
        strScript &= "ventana.opener = window.self" & vbCrLf
        strScript &= "ventana.close()" & vbCrLf
        strScript &= "}" & vbCrLf
        strScript &= "setTimeout(""cerrar()"", " & Segundos & ")" & vbCrLf
        strScript &= "</script>" & vbCrLf

        pag.ClientScript.RegisterStartupScript(pag.GetType(), "CerrarVentanaTimeOut", strScript)

    End Sub

    ' Funci�n que Deshabilita el Click derecho del rat�n en una p�gina.
    '
    '   --> Como entrada recibe la p�gina a la que aplicar la funci�n.

    Public Sub Deshabilitar_ClickDerecho(ByVal pagina As Object)

        Dim strScript As String = ""
        strScript &= "<script language=JavaScript>" & vbCrLf
        strScript &= "var message="""";" & vbCrLf
        strScript &= "function clickIE() {if (document.all) {(message);return false;}}" & vbCrLf
        strScript &= "function clickNS(e) {if " & vbCrLf
        strScript &= "(document.layers||(document.getElementById&&!document.all)) {" & vbCrLf
        strScript &= "if (e.which==2||e.which==3) {(message);return false;}}}" & vbCrLf
        strScript &= "if (document.layers)" & vbCrLf
        strScript &= "{document.captureEvents(Event.MOUSEDOWN);document.onmousedown=clickNS;}" & vbCrLf
        strScript &= "else{document.onmouseup=clickNS;document.oncontextmenu=clickIE;}" & vbCrLf
        strScript &= "document.oncontextmenu=new Function(""return false"")" & vbCrLf
        strScript &= "</script>" & vbCrLf

        pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType(), "noClickDerecho", strScript)

    End Sub


    ' Funci�n que maximiza el tama�o de la ventana del navegador hasta la resoluci�n
    ' que tenga el cliente establecida.
    '
    '   --> Como entrada recibe la  p�gina a la que aplicar el script.

    Public Sub MaximizarVentana_Tama�oPantalla(ByVal pagina As Object)

        Dim strScript As String = ""
        strScript &= "<script language=""JavaScript1.2"">" & vbCrLf
        strScript &= "window.moveTo(0,0);" & vbCrLf
        strScript &= "if (document.all) {" & vbCrLf
        strScript &= "top.window.resizeTo(screen.availWidth,screen.availHeight);" & vbCrLf
        strScript &= "}" & vbCrLf
        strScript &= "else if (document.layers||document.getElementById) {" & vbCrLf
        strScript &= "if (top.window.outerHeight<screen.availHeight||top.window.outerWidth<screen.availWidth){" & vbCrLf
        strScript &= "top.window.outerHeight = screen.availHeight;" & vbCrLf
        strScript &= "top.window.outerWidth = screen.availWidth;" & vbCrLf
        strScript &= "}" & vbCrLf
        strScript &= "}" & vbCrLf
        strScript &= "</script>" & vbCrLf

        pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType(), "MaximizarPantalla", strScript)

    End Sub

End Class

