Imports Microsoft.VisualBasic
Imports TemplateParser
Imports System.Net.Mail
Public Class Email
    Public Shared Function SendEmail(ByVal FilePath As String, ByVal templateVars As Hashtable, ByVal Mailfrom As String, ByVal MailTo As String, ByVal Subject As String, ByVal ParamArray BCC() As String) As String
        Dim parser As New Parser(HttpContext.Current.Server.MapPath("~/MailTemplates/" & FilePath), templateVars)
        Return SendEmail(Mailfrom, MailTo, Subject, parser.Parse(), BCC)
    End Function

    Public Shared Function SendEMail(ByVal Mailfrom As String, ByVal MailTo As String, ByVal Subject As String, ByVal body As String, ByVal ParamArray BCC() As String) As String
        Try
            Dim mailadd As New System.Net.Mail.MailAddress(Mailfrom, System.Configuration.ConfigurationManager.AppSettings("smtp_DisplayName"))
            Dim mailmsg As New System.Net.Mail.MailMessage
            mailmsg.To.Add(MailTo)
            For I As Integer = 0 To UBound(BCC)
                mailmsg.Bcc.Add(BCC(I))
            Next I
            mailmsg.From = mailadd
            mailmsg.Subject = Subject
            mailmsg.Body = body
            mailmsg.IsBodyHtml = True
            Dim cred As New System.Net.NetworkCredential
            cred.UserName = System.Configuration.ConfigurationManager.AppSettings("smtp_username")
            cred.Password = System.Configuration.ConfigurationManager.AppSettings("smtp_pwd")
            Dim mailsmtp As New System.Net.Mail.SmtpClient
            mailsmtp.Credentials = cred
            mailsmtp.Host = System.Configuration.ConfigurationManager.AppSettings("smtp_host")

            mailsmtp.Send(mailmsg)
            Return "Email successfully sent."
        Catch ex As Exception
            Return "Send Email Failed." & ex.Message
        End Try

    End Function

End Class
