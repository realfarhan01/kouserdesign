Imports Microsoft.VisualBasic

Public Class Notifications
    Public Shared Function ErrorMessageBlock(ByVal Msg As String) As String
        Return "<div class='alert alert-error alert-block'><button class='close' data-dismiss='alert'>×</button><strong></strong> " & Msg & "</div>"
    End Function
    Public Shared Function ErrorMessage(ByVal Msg As String) As String
        Return "<div class='alert alert-error'><button class='close' data-dismiss='alert'>×</button><strong></strong>" & Msg & "</div>"
    End Function
    Public Shared Function WarningMessage(ByVal Msg As String) As String
        Return " <div class='alert'><button class='close' data-dismiss='alert'>×</button><strong>Warning!</strong> " & Msg & "</div>"
    End Function
    Public Shared Function SuccessMessage(ByVal Msg As String) As String
        Return "<div class='alert alert-success'><button class='close' data-dismiss='alert'>×</button><strong>Success!</strong> " & Msg & "</div>"
    End Function
    Public Shared Function InfoMessage(ByVal Msg As String) As String
        Return "<div class='alert alert-info'><button class='close' data-dismiss='alert'>×</button><strong>Info!</strong> " & Msg & "</div>"
    End Function
End Class
