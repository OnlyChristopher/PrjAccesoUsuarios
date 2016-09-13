
Public NotInheritable Class Cls_Oficiales
    Private Shared ObjsRegNeg As New Cls_Negocio.ClsRegNegPru
    Private SqlTexto As String = ""
    Public Function ListarOficiales(ByVal maspe_carne As String, ByVal ape_pat As String, ByVal ape_mat As String, ByVal nombres As String) As DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 1, @maspe_carne='" & maspe_carne & "', @maspe_nomb = '" & nombres & "' , @maspe_pater = '" & ape_pat & "' , @maspe_mater = '" & ape_mat & "'"
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function

    Public Function ListarMovimientos(ByVal maspe_carne As String) As DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 4, @maspe_carne='" & maspe_carne & "'"
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function
    Public Function ListarDetalleMovimientos(ByVal rcontrol_transa As String) As DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 5, @RCONTROL_TRANSA='" & rcontrol_transa & "'"
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function
    Public Function DatosOficial(ByVal maspe_carne As String) As DataSet
        SqlTexto = "[sp_listado_maspe] @mssql = 14, @maspe_carne='" & maspe_carne & "'"
        Return ObjsRegNeg.ConsultaxParametros(SqlTexto)
    End Function
End Class
