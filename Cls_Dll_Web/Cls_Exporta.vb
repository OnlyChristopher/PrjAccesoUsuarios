﻿Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Public Class Cls_Exporta

    Public Sub Export(ByVal fileName As String, ByVal gv As GridView)
        Try
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))
            HttpContext.Current.Response.ContentType = "application/ms-excel"

            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    '  Create a table to contain the grid
                    Dim table As New Table()

                    '  include the gridline settings
                    table.GridLines = gv.GridLines

                    '  add the header row to the table
                    If gv.HeaderRow IsNot Nothing Then
                        Cls_Exporta.PrepareControlForExport(gv.HeaderRow)
                        table.Rows.Add(gv.HeaderRow)
                    End If

                    '  add each of the data rows to the table
                    For Each row As GridViewRow In gv.Rows
                        Cls_Exporta.PrepareControlForExport(row)
                        table.Rows.Add(row)
                    Next

                    '  add the footer row to the table
                    If gv.FooterRow IsNot Nothing Then
                        Cls_Exporta.PrepareControlForExport(gv.FooterRow)
                        table.Rows.Add(gv.FooterRow)
                    End If

                    '  render the table into the htmlwriter
                    table.RenderControl(htw)

                    '  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString())
                    HttpContext.Current.Response.[End]()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("ClsDllWeb.Cls_Exporta :" & vbCrLf & ex.Message, ex)
        End Try
    End Sub

    Private Shared Sub PrepareControlForExport(ByVal control As Control)
        For i As Integer = 0 To control.Controls.Count - 1
            Dim current As Control = control.Controls(i)
            If TypeOf current Is LinkButton Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))
            ElseIf TypeOf current Is ImageButton Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))
            ElseIf TypeOf current Is HyperLink Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))
            ElseIf TypeOf current Is DropDownList Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))
            ElseIf TypeOf current Is CheckBox Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False")))
            End If

            If current.HasControls() Then
                Cls_Exporta.PrepareControlForExport(current)
            End If
        Next
    End Sub
End Class
