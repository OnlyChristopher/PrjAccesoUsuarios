Imports System.Text
Imports System.Security.Cryptography

Friend Class Cls_Encrypta

    Public Shared Function CodificarPassword(ByVal Password As String) As String

        Dim sha1 As SHA1 = New SHA1CryptoServiceProvider()
        Dim inputBytes As Byte() = (New UnicodeEncoding()).GetBytes(Password)
        Dim hash As Byte() = sha1.ComputeHash(inputBytes)

        Return Convert.ToBase64String(hash)
    End Function

End Class
