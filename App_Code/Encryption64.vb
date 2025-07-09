Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography
Imports System.Configuration
Imports System.Security
Imports System.Web.Configuration


Public Class Encryption64
    Private key() As Byte = {}
    Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
    Public Function Decrypt(ByVal stringToDecrypt As String) As String
        stringToDecrypt = stringToDecrypt.Replace(" ", "+").Replace("hhKLfw74uoN", "NY/Hfw7oNeo=")
        Dim inputByteArray(stringToDecrypt.Length) As Byte
        Try
            Dim SEncryptionKey As String = "rb&7$8dg@fp!>"
            key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(stringToDecrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV),
                CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch e As Exception
            Return e.Message
        End Try
    End Function

    Public Function Encrypt(ByVal stringToEncrypt As String) As String
        Try
            Dim SEncryptionKey As String = "rb&7$8dg@fp!>"
            key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8))
            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes( _
                stringToEncrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), _
                CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray()).Replace("NY/Hfw7oNeo=", "hhKLfw74uoN")
        Catch e As Exception
            Return e.Message
        End Try
    End Function

End Class


