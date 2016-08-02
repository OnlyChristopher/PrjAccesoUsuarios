Imports Cls_Data.SqlHelpData
Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Net.Dns

Public Class ClsRegNegPru
    Dim Objsref As New ClsSqlData

    Public Function ConsultaxCamposxTablas(ByVal Campos As String, ByVal tabla As String, ByVal CondicionNoMas1 As String) As Data.DataSet 'con un codigo
        If CondicionNoMas1 = "" Then
            Return Objsref.CargarDatasetProcSql("select " & Campos & " from " & tabla)
        Else
            Return Objsref.CargarDatasetProcSql("select " & Campos & " from " & tabla & " where " & CondicionNoMas1)
        End If
    End Function

    Public Function InsertarxActualizaxBorra(ByVal storeProcedure As String, ByVal ParamArray Parametros() As System.Object) As String
        Return Objsref.Ejecutar(storeProcedure, Parametros)
    End Function

    Public Function InsertarxActualizaxBorraOutput(ByVal storeProcedure As String, ByRef ArgDev As ArrayList, ByVal ParamArray Parametros() As System.Object) As String
        Return Objsref.Ejecutar(storeProcedure, ArgDev, Parametros)
    End Function

    Public Function ConsultaxParametros(ByVal storeProcedure As String, ByVal ParamArray Parametros() As System.Object) As Data.DataSet
        Return Objsref.CargarDatasetProcSql(storeProcedure, Parametros)
    End Function

    Public Function Consulta(ByVal storeProcedure As String) As Data.DataSet
        Return Objsref.CargarDatasetProcSql(storeProcedure)
    End Function

    Public Sub AgregaCampo(ByVal Tabla As String, ByVal NombreCampo As String, ByVal TipoCampo As String, ByVal ValorDefecto As System.Object)
        Try
            If ValorDefecto = "" Then
                Objsref.CargarDatasetProcSql("alter table " & Tabla & " add " & NombreCampo & " " & TipoCampo)
            Else
                Objsref.CargarDatasetProcSql("alter table " & Tabla & " add " & NombreCampo & " " & TipoCampo & " default " & ValorDefecto)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Aviso")
        End Try
    End Sub

    Public Function ConsultaScalar(ByVal storeProcedure As String, ByVal ParamArray Parametros() As System.Object) As Object
        Return Objsref.ObtenerEscalarProcSQL(storeProcedure, Parametros)
    End Function

    Public Function Consulta_Data_Reader(ByVal storeProcedure As String, ByVal ParamArray Parametros() As System.Object) As Object
        Return Objsref.CargarDataReaderProcSql(storeProcedure, Parametros)
    End Function

    Public Function HostIP(ByVal mStrHost As String) As String
        Dim mIpHostEntry As IPHostEntry = GetHostByName(mStrHost)
        Dim mIpAddLst As IPAddress() = mIpHostEntry.AddressList()
        ' para efecto de este ejemplo y reducir codigo
        ' se devolvera la primera direccion IP y no se
        ' incluira manejo de excepciones
        Return mIpAddLst(0).ToString
    End Function

    'Public Function HostIP(ByVal mStrHost As String) As String
    '    'Dim mIpHostEntry As IPHostEntry = GetHostByName(mStrHost)
    '    Dim mIpHostEntry As IPHostEntry = GetHostEntry(mStrHost)
    '    Dim mIpAddLst As IPAddress() = mIpHostEntry.AddressList()
    '    ' para efecto de este ejemplo y reducir codigo
    '    ' se devolvera la primera direccion IP y no se
    '    ' incluira manejo de excepciones
    '    Return mIpAddLst(0).ToString
    'End Function
End Class

