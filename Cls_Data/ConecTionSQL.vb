Public Class ConecTionSQL
    Protected Shared direction As String = "H:\Proyectos\PrjAccesoUsuarios\Conexion2.cnx"
    Private Shared Cn As String = Coneccion()
    Private Shared ret_Val As String = ""
    Private Shared xPos As String = ""
    Private Shared xDato As String = ""
    Private Shared xCar As String = ""
    Private Shared I As Integer

    Public Shared Property ConectionString() As String
        Get
            Return Cn
        End Get
        Set(ByVal Value As String)
            Cn = Value
        End Set
    End Property

    Protected Shared Function Coneccion() As String
        Dim ds As New System.Data.DataSet
        Dim cadena As String = ""
        Try
            ds.ReadXml(direction)
            If UCase(ds.Tables(0).Rows(0)(4)) = "FALSE" Then
                cadena = "Data Source=" & Desencrip(ds.Tables(0).Rows(0)(0), Len(ds.Tables(0).Rows(0)(0))).Trim &
                                   "; initial catalog=" & Desencrip(ds.Tables(0).Rows(0)(1), Len(ds.Tables(0).Rows(0)(1))).Trim &
                                   "; uid=" & Desencrip(ds.Tables(0).Rows(0)(2), Len(ds.Tables(0).Rows(0)(2))).Trim &
                                   ";Pwd=" & Desencrip(ds.Tables(0).Rows(0)(3), Len(ds.Tables(0).Rows(0)(3))).Trim &
                                   ";Connect Timeout=0"  '& "Integrated Security=" & ds.Tables(0).Rows(0)(4)
            ElseIf UCase(ds.Tables(0).Rows(0)(4)) = "TRUE" Then
                cadena = "Data Source=" & Desencrip(ds.Tables(0).Rows(0)(0), Len(ds.Tables(0).Rows(0)(0))).Trim &
                                   "; initial catalog=" & Desencrip(ds.Tables(0).Rows(0)(1), Len(ds.Tables(0).Rows(0)(1))).Trim &
                                   "; " &
                                   "Integrated Security=" & ds.Tables(0).Rows(0)(4) & ";Connect Timeout=0"
            End If

        Catch ex As Exception
            ds.Namespace = "conection"
            ds.Tables.Add("SQLSERVER")
            ds.Tables(0).Columns.Add("Server")
            ds.Tables(0).Columns.Add("DB")
            ds.Tables(0).Columns.Add("Usuario")
            ds.Tables(0).Columns.Add("password")
            ds.Tables(0).Columns.Add("Integrate")

            Dim dr As Data.DataRow
            dr = ds.Tables(0).NewRow
            dr(0) = Encrip("13BDATOS-04", Len("13BDATOS-04"))
            dr(1) = Encrip("DIREJPER-CPV", Len("DIREJPER-CPV"))
            dr(2) = Encrip("sa", Len("sa"))
            dr(3) = Encrip("5348483", Len("5348483"))
            dr(4) = "False"
            ds.Tables(0).Rows.Add(dr)
            ds.WriteXml(direction)
            If UCase(ds.Tables(0).Rows(0)(4)) = "FALSE" Then
                cadena = "Data Source=" & Desencrip(ds.Tables(0).Rows(0)(0), Len(ds.Tables(0).Rows(0)(0))) &
                                   "; initial catalog=" & Desencrip(ds.Tables(0).Rows(0)(1), Len(ds.Tables(0).Rows(0)(1))) &
                                   "; uid=" & Desencrip(ds.Tables(0).Rows(0)(2), Len(ds.Tables(0).Rows(0)(2))) &
                                   ";Pwd=" & Desencrip(ds.Tables(0).Rows(0)(3), Len(ds.Tables(0).Rows(0)(3))) '& "Integrated Security=" & ds.Tables(0).Rows(0)(4)
            ElseIf UCase(ds.Tables(0).Rows(0)(4)) = "TRUE" Then
                cadena = "Data Source=" & Desencrip(ds.Tables(0).Rows(0)(0), Len(ds.Tables(0).Rows(0)(0))) &
                                   "; initial catalog=" & Desencrip(ds.Tables(0).Rows(0)(1), Len(ds.Tables(0).Rows(0)(1))) &
                                   "; " &
                                   "Integrated Security=" & ds.Tables(0).Rows(0)(4) & ";"
            End If
        End Try
        Return cadena
    End Function

    Protected Shared Function Encrip(ByVal P_dato, ByVal P_leng) As String
        ret_Val = ""
        xDato = P_dato + Space(20 - Len(P_dato))
        For I = 1 To Len(xDato)
            xCar = Mid(xDato, I, 1)
            xPos = Chr(Asc(xCar) + I)
            If xPos = "'" Then
                xPos = Chr(Asc(xCar) + I + 3)
            End If
            ret_Val = ret_Val + xPos
        Next
        Encrip = ret_Val
    End Function

    Protected Shared Function Desencrip(ByVal P_dato, ByVal P_leng) As String
        ret_Val = ""
        xDato = P_dato + Space(20 - Len(P_dato))
        For I = 1 To Len(xDato)
            xCar = Mid(xDato, I, 1)
            xPos = Chr(Asc(xCar) - I)
            If xPos = "#" Then
                xPos = Chr(Asc(xCar) - I - 3)
            End If
            ret_Val = ret_Val + xPos
        Next
        Desencrip = ret_Val
    End Function
End Class
