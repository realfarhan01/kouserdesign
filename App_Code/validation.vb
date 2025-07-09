Imports System.Text.RegularExpressions
Public Class validation
    Public Shared Function isName(ByVal str As String) As Boolean
        If str.Trim() = "" Then
            Return False
        End If
        Dim reg As New Regex("^([a-zA-Z.\s]{1,100})+$")
        Return reg.IsMatch(str)
    End Function
    Public Shared Function isEmail(ByVal str As String) As Boolean
        Dim reg As New Regex("^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$")
        Return reg.IsMatch(str)
    End Function
    Public Shared Function isID(ByVal str As String) As Boolean
        Dim reg As New Regex("^([0-9a-zA-Z]{4,20})$")
        Return reg.IsMatch(str)
    End Function
End Class
