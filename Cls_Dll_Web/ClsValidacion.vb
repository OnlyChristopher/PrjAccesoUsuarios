
Public Class ClsValidacion

    Protected i As Integer
    'Protected ObjsRegneg As New Cls_Negocio.ClsRegNegPru

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

    Public Sub MessageBox(ByVal pages As System.Object, ByVal msje As String)
        pages.ClientScript.RegisterStartupScript(pages.GetType, "Mensaje", "<script language ='javascript'>alert('" & msje & "');</script> ")
    End Sub
End Class