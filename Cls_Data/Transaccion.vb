Public Class Transaccion
    Public Function ComenzarTransaccion() As IDbTransaction
        Try
            Dim Cn As IDbConnection = New System.Data.SqlClient.SqlConnection
            Cn.ConnectionString = ConecTionSQL.ConectionString
            Cn.Open()
            Return Cn.BeginTransaction(IsolationLevel.Serializable)
        Catch ex As Exception
            Throw New Exception("Capa Data - Transaccion.ComenzarTransaccion:" & vbCrLf & ex.Message, ex)
        End Try
    End Function
End Class
