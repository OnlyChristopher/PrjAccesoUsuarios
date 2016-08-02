'Imports Microsoft.VisualBasic
'Imports System.Web
'Imports System.Web.UI
Public Class Cls_Validacion
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Protected i As Integer

    Public Sub Asignar_textoLimpiar(ByVal valor As String, ByVal ParamArray TextBoxLabelBox() As System.Object)
        For i = 0 To TextBoxLabelBox.GetUpperBound(0)
            TextBoxLabelBox(i).text = valor
        Next
        i = 0
    End Sub

    Public Sub Act_vis(ByVal ParamArray Objectos() As System.Object)
        Dim i As Integer
        For i = 0 To Objectos.GetUpperBound(0)
            Objectos(i).visible = True
        Next
        i = 0
    End Sub

    Public Sub Desact_vis(ByVal ParamArray Objectos() As System.Object)
        For i = 0 To Objectos.GetUpperBound(0)
            Objectos(i).visible = Not True
        Next
        i = 0
    End Sub

    Public Sub Combo_SelIndex(ByVal valor As Integer, ByVal ParamArray Combobox() As System.Object)
        For i = 0 To Combobox.GetUpperBound(0)
            Combobox(i).SelectedIndex = valor
        Next
        i = 0
    End Sub

    Public Function FechaActual() As Date
        Try
            Dim fc As Date
            fc = ObjsRegNeg.ConsultaScalar("select now()")
            Return fc
        Catch ex As Exception
            MsgBox("Error Clase Mster: " & ex.Message, MsgBoxStyle.Critical, "Advertencia")
            Exit Function
        End Try
    End Function

    Public Sub Mostrar_Mensaje(ByVal lblmensaje As Object, ByVal Texto_Msje As String, ByVal Visible As Boolean)
        lblmensaje.Text = Texto_Msje
        lblmensaje.Visible = Visible
    End Sub

    Public Sub MessageBox_in_updatepanel(ByVal pages As System.Object, ByVal msje As String, ByVal Titulo As String)
        pages.ScriptManager.RegisterClientScriptBlock(pages, pages.GetType(), Titulo, "alert('" & msje & "');", True)
    End Sub

    Public Sub MessageBox(ByVal pages As System.Object, ByVal msje As String, ByVal Titulo As String)
        pages.ClientScript.RegisterStartupScript(pages.GetType, Titulo, "<script language ='javascript'>alert('" & msje & "');</script> ")
    End Sub

    Public Sub CerrarVentana(ByVal pages As System.Object)
        pages.ClientScript.RegisterStartupScript(pages.GetType, "cerrar", "<script language ='javascript'>window.close();</script> ")
    End Sub

    Public Function Ejecuta_Query_DataReader(ByVal SqlTexto As String) As Object
        Return ObjsRegNeg.Consulta_Data_Reader(SqlTexto)
    End Function

    Public Function UltimoDiaMes(ByVal Fecha As Date) As Integer
        Return Day(DateSerial(Year(Fecha), Month(Fecha) + 1, 0))
    End Function

    Public Function CantidadSemanasMes(ByVal Fecha As Date) As Integer
        Dim cantidad As Integer = 0
        Dim FechaHoy As Date = DateTime.Parse("01/" & Month(Fecha).ToString("00") & "/" & Year(Fecha).ToString, Globalization.CultureInfo.CreateSpecificCulture("es-PE").DateTimeFormat)
        Dim nro_de_dia As Integer = Weekday(FechaHoy, Microsoft.VisualBasic.FirstDayOfWeek.Monday)
        Dim Fecha_Inicia_Semana As Date = DateAdd(DateInterval.Day, -(nro_de_dia - 1), FechaHoy)

        For i As Integer = 0 To 5 ' 5 semanas
            Dim Fecha_T As Date = Fecha_Inicia_Semana
            For x As Integer = 1 To 4 ' solo de lunes a viernes
                If Month(Fecha_T) = Month(Fecha) Then
                    'If Fecha.DayOfWeek <> DayOfWeek.Saturday And Fecha.DayOfWeek <> DayOfWeek.Sunday Then
                    cantidad += 1
                    Exit For
                    'End If
                End If
                Fecha_T = Fecha_T.AddDays(1)
            Next
            Fecha_Inicia_Semana = Fecha_Inicia_Semana.AddDays(7)
        Next
        Return cantidad
    End Function

    Public Function Cantidad_Lunes_Mes(ByVal Fecha As Date) As Integer
        Dim cantidad As Integer = 0
        Dim fechaRef As New Date(Year(Fecha), Month(Fecha), 1)
        While fechaRef.DayOfWeek <> DayOfWeek.Monday
            fechaRef = fechaRef.AddDays(1)
        End While
        Dim xfecha As Date
        For i As Integer = 0 To 5
            xfecha = fechaRef.AddDays(i * 7)
            If xfecha.Month = Month(Fecha) Then
                cantidad = cantidad + 1
            Else
                Exit For
            End If
        Next
        Return cantidad
    End Function
End Class