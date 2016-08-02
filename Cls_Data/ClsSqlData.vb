'Clase querializaLos Procesos dCon Transacciones En las Funciones Caracteristica
' Clase de Datos SQLHELPDATA Hecho en  Visual BAsic
' CReado Por System SoftBuaExi
' Version 3.5 2007

' Modificado por Alex y adaptado para uso con MySql
' 27-05-2011

Imports System.Data
Imports System.IO
Imports System.Xml
Imports Microsoft.VisualBasic

Namespace SqlHelpData

    Public Class ClsSqlData

        'Variable que contiene en memoria los ultimos estore o consulta que se ejecuta

        Shared MColComandos As New System.Collections.Hashtable


#Region "Objeto Comando Ejecutador o contenedor una consulta"

        Public Overloads Function ComandoProcAlmConSql(ByVal Proc_Alm_ConsulSql As String) As System.Data.IDbCommand
            'Variable de transacion y transaccion en via sql Server
            Dim Transacciones As New Transaccion
            Dim Tran As IDbTransaction = Transacciones.ComenzarTransaccion
            Dim Coneccion As IDbConnection = Tran.Connection

            Dim MComando As New System.Data.SqlClient.SqlCommand

            Try
                'Dim trans As IDbTransaction = Transacciones.ComenzarTransaccion
                If MColComandos.Contains(Proc_Alm_ConsulSql) Then
                    If Coneccion.State = ConnectionState.Closed Then Coneccion.Open()
                    MComando = CType(MColComandos.Item(Proc_Alm_ConsulSql), System.Data.SqlClient.SqlCommand)
                Else
                    Dim i, c As Integer
                    Dim a As String

                    For i = 1 To Len(Proc_Alm_ConsulSql)
                        a = Mid(Proc_Alm_ConsulSql, i, 1)
                        If a = " " Then
                            c = c + 1
                        End If
                    Next

                    If Coneccion.State = ConnectionState.Closed Then Coneccion.Open()
                    MComando = New System.Data.SqlClient.SqlCommand(Proc_Alm_ConsulSql, Coneccion, Tran)
                    MComando.Connection = Coneccion
                    MComando.CommandTimeout = 0
                    MComando.Transaction = Tran
                    Dim MConstructor As Object = Nothing
                    MConstructor = New System.Data.SqlClient.SqlCommandBuilder


                    If c > 0 Then
                        MComando.CommandType = CommandType.Text
                    Else
                        MComando.CommandType = CommandType.StoredProcedure

                        'Dim MConstructor As New MySql.Data.MySqlClient.MySqlCommandBuilder
                        MConstructor.DeriveParameters(MComando)
                    End If
                    MColComandos.Add(Proc_Alm_ConsulSql, MComando)
                End If
                Tran.Commit()
                Return MComando
            Catch ex As Exception
                Tran.Rollback()
                Throw New Exception("Capa Data - ClsSqlData.ComandoProcAlmConSql:" & vbCrLf & ex.Message, ex)
            Finally
                Coneccion.Close()
                MComando.Dispose()
            End Try
        End Function

#End Region

#Region "Carga de los parametros del comandos y los objetos"

        Protected Shared Sub CargarParametro(ByVal comandos As System.Data.SqlClient.SqlCommand,
                                             ByRef args() As System.Object)
            Dim I As Integer
            With comandos
                Try
                    For I = 0 To args.GetUpperBound(0)
                        Dim Parametro As System.Data.SqlClient.SqlParameter = CType(.Parameters(I + 1), System.Data.SqlClient.SqlParameter)
                        If Parametro.Direction = ParameterDirection.Input Or Parametro.Direction = ParameterDirection.InputOutput Then
                            Parametro.Value = args(I)
                        End If
                    Next
                Catch ex As Exception
                    Throw New Exception("Capa Data - ClsSqlData.CargarParametro:" & vbCrLf & ex.Message, ex)
                End Try
            End With
        End Sub
#End Region

#Region "Retornar Parametros de Salida"
        Protected Shared Sub RetornarParametros(ByRef ArgDev As ArrayList, _
                                                ByVal comando As System.Data.IDbCommand, _
                                                ByVal Args() As Object)
            Dim I As Integer
            With comando
                For I = 0 To Args.GetUpperBound(0)
                    Try
                        Dim param As System.Data.SqlClient.SqlParameter = CType(.Parameters(I + 1), System.Data.SqlClient.SqlParameter)
                        If param.Direction = ParameterDirection.Output Or param.Direction = ParameterDirection.InputOutput Then
                            param.Direction = ParameterDirection.Output
                        End If
                        ArgDev.Add(param.Value)
                    Catch ex As Exception
                        Throw New Exception("Capa Data - ClsSqlData.RetornarParametros:" & vbCrLf & ex.Message, ex)
                    End Try
                Next
            End With
        End Sub
#End Region

#Region "Componente para la ejecucion de los diferentes para  metros que existieras"

        Public Overloads Function Ejecutar(ByVal Proc_Alm_ConsulSql As String, _
                                          ByVal ParamArray Argumentos() As System.Object) As Integer
            Dim Resp As Integer
            Dim cmd As System.Data.SqlClient.SqlCommand = ComandoProcAlmConSql(Proc_Alm_ConsulSql)
            Try

                Dim i, c As Integer
                Dim a As String

                For i = 1 To Len(Proc_Alm_ConsulSql)
                    a = Mid(Proc_Alm_ConsulSql, i, 1)
                    If a = " " Then
                        c = c + 1
                    End If
                Next
                If c > 0 Then
                    cmd.CommandType = CommandType.Text
                Else
                    cmd.CommandType = CommandType.StoredProcedure
                End If

                CargarParametro(cmd, Argumentos)
                If cmd.Connection.State = ConnectionState.Closed Then cmd.Connection.Open()
                Resp = cmd.ExecuteNonQuery()
            Catch ex As System.Data.SqlClient.SqlException
                Resp = 0
                Throw New Exception("Capa Data - ClsSqlData.Ejecutar 2 entradas:" & vbCrLf & ex.Message, ex)
            Finally
                cmd.Connection.Close()
                cmd.Dispose()
            End Try

            Return Resp
        End Function

        Public Overloads Function Ejecutar(ByVal Proc_Alm_ConsulSql As String, _
                                           ByRef ArgDev As ArrayList, _
                                           ByVal ParamArray Argumentos() As System.Object) As Integer
            Dim Resp As Integer
            Dim cmd As System.Data.SqlClient.SqlCommand = ComandoProcAlmConSql(Proc_Alm_ConsulSql)

            Try

                Dim i, c As Integer
                Dim a As String

                For i = 1 To Len(Proc_Alm_ConsulSql)
                    a = Mid(Proc_Alm_ConsulSql, i, 1)
                    If a = " " Then
                        c = c + 1
                    End If
                Next
                If c > 0 Then
                    cmd.CommandType = CommandType.Text
                Else
                    cmd.CommandType = CommandType.StoredProcedure
                End If
                CargarParametro(cmd, Argumentos)
                If cmd.Connection.State = ConnectionState.Closed Then cmd.Connection.Open()
                Resp = cmd.ExecuteNonQuery()
                RetornarParametros(ArgDev, cmd, Argumentos)
            Catch ex As System.Data.SqlClient.SqlException
                Throw New Exception("Capa Data - ClsSqlData.Ejecutar 3 entradas:" & vbCrLf & ex.Message, ex)
                Resp = 0
            Finally
                cmd.Connection.Close()
                cmd.Dispose()
            End Try

            Return Resp
        End Function


#End Region

#Region "Crear Adaptador para la transmision de objetos"

        Public Overloads Function CrearDataAdapterProcConsul(ByVal Proc_Alm_ConsulSql As String, _
                                                                ByVal ParamArray Argumentos() As System.Object) As System.Data.IDataAdapter
            Try

                Dim Mcomando As System.Data.SqlClient.SqlCommand = ComandoProcAlmConSql(Proc_Alm_ConsulSql) 'Tran,
                If Not Argumentos Is Nothing Then
                    CargarParametro(Mcomando, Argumentos)
                End If
                Return New System.Data.SqlClient.SqlDataAdapter(Mcomando)
            Catch ex As Exception
                Throw New Exception("Capa Data - ClsSqlData.CrearDataAdapterProcConsul:" & vbCrLf & ex.Message, ex)
            End Try
        End Function
#End Region


#Region "Carga un dataset con parametros y con estores Procedures"

        Public Overloads Function CargarDatasetProcSql(ByVal Proc_Alm_ConsulSql As String, _
                                                       ByVal ParamArray Argumento() As System.Object) As System.Data.DataSet
            Dim Mdataset As New DataSet
            Dim i, c As Integer
            Dim a As String

            For i = 1 To Len(Proc_Alm_ConsulSql)
                a = Mid(Proc_Alm_ConsulSql, i, 1)
                If a = " " Then
                    c = c + 1
                End If
            Next
            Try
                If c <= 0 Then
                    CrearDataAdapterProcConsul(Proc_Alm_ConsulSql, Argumento).Fill(Mdataset)
                Else
                    CrearDataAdapterProcConsul(Proc_Alm_ConsulSql, Nothing).Fill(Mdataset)
                End If
                Return Mdataset
            Catch ex As SystemException
                Dim dr As DataRow
                Mdataset.DataSetName = "Errores"
                Mdataset.Tables.Add("error")
                Mdataset.Tables(0).Columns.Add("NameError")
                dr = Mdataset.Tables(0).NewRow
                dr(0) = CType(ex.Message, String)

                Mdataset.Tables(0).Rows.Add(dr)
                Throw New Exception("Capa Data - ClsSqlData.CargarDatasetProcSql:" & vbCrLf & ex.Message, ex)
            End Try
            Return Mdataset
        End Function


#End Region

#Region "Crea Una tabla de solo lectura parauso de lectura"

        Public Overloads Function CargarDataReaderProcSql(ByVal Proc_Alm_ConsulSql As String, _
                                                          ByVal ParamArray Argumento() As System.Object) As System.Object 'MySql.Data.MySqlClient.MySqlDataReader

            Dim Mcomandos As System.Data.SqlClient.SqlCommand = ComandoProcAlmConSql(Proc_Alm_ConsulSql)
            Dim Mdataset As New DataSet
            Dim i, c As Integer
            Dim a As String

            For i = 1 To Len(Proc_Alm_ConsulSql)
                a = Mid(Proc_Alm_ConsulSql, i, 1)
                If a = " " Then
                    c = c + 1
                End If
            Next

            Try
                If c <= 0 Then
                    If Not Argumento Is Nothing Then
                        CargarParametro(Mcomandos, Argumento)
                    End If
                    Mcomandos.CommandType = CommandType.StoredProcedure
                Else
                    Mcomandos.CommandType = CommandType.Text
                End If

                Dim res As System.Data.SqlClient.SqlDataReader = Mcomandos.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
                Return Mcomandos.ExecuteReader(System.Data.CommandBehavior.CloseConnection)

                'Dim res As MySql.Data.MySqlClient.MySqlDataReader = Mcomandos.ExecuteReader()
                'Return Mcomandos.ExecuteReader()

            Catch ex As Exception
                Throw New Exception("Capa Data - ClsSqlData.CargarDataReaderProcSql:" & vbCrLf & ex.Message, ex)
            End Try

        End Function

#End Region

#Region "Objeto que devuelde escalar"

        Public Overloads Function ObtenerEscalarProcSQL(ByVal Proc_Alm_ConsulSql As String, _
                                                     ByVal ParamArray Argumento() As System.Object) As System.Object
            Dim Mcomandos As System.Data.SqlClient.SqlCommand = ComandoProcAlmConSql(Proc_Alm_ConsulSql) 'Coneccion
            Dim Mdataset As New DataSet
            Dim i, c As Integer
            Dim a As String

            For i = 1 To Len(Proc_Alm_ConsulSql)
                a = Mid(Proc_Alm_ConsulSql, i, 1)
                If a = " " Then
                    c = c + 1
                End If
            Next

            Try

                If c <= 0 Then
                    If Not Argumento Is Nothing Then
                        CargarParametro(Mcomandos, Argumento)
                    End If
                    Mcomandos.CommandType = CommandType.StoredProcedure
                Else
                    Mcomandos.CommandType = CommandType.Text
                End If
                If Mcomandos.Connection.State = ConnectionState.Closed Then Mcomandos.Connection.Open()
                Return Mcomandos.ExecuteScalar

            Catch ex As Exception
                Throw New Exception("Capa Data - ClsSqlData.ObtenerEscalarProcSQL:" & vbCrLf & ex.Message, ex)
                Return "Capa Data - ClsSqlData.ObtenerEscalarProcSQL - Error : " & ex.Message
            Finally
                Mcomandos.Connection.Close()
                Mcomandos.Dispose()
            End Try

        End Function

#End Region

    End Class

End Namespace
