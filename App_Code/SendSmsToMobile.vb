Imports System.Web
Imports System.Net
Imports System.IO
Public Class SendSmsToMobile
    Protected Shared Function SendSMSToNum(ByVal strUser As String, ByVal strPassword As String, ByVal senderid As String, ByVal strRecip As String, ByVal strMsgText As String, Optional ByVal strSMSScheduleDate As String = "") As String

        Dim strUrl As String
        strUrl = "http://bulksms.aspiretechnosys.com/app/smsapi/index.php?" _
                         & "key=" & HttpUtility.UrlEncode(strUser) _
                         & "&senderid=" & HttpUtility.UrlEncode(senderid) _
                         & "&type=text" _
                         & "&contacts=" & HttpUtility.UrlEncode(strRecip) _
                         & "&msg=" & HttpUtility.UrlEncode(strMsgText)
        Dim objURI As Uri = New Uri(strUrl)
        Dim objWebRequest As WebRequest = WebRequest.Create(objURI)
        Dim objWebResponse As WebResponse = objWebRequest.GetResponse()
        Dim objStream As Stream = objWebResponse.GetResponseStream()
        Dim objStreamReader As StreamReader = New StreamReader(objStream)
        Dim strHTML As String = objStreamReader.ReadToEnd
        SendSMSToNum = strHTML

    End Function
    Public Shared Function SendGatewaySms(ByVal strMobileNo As String, ByVal strTextMsg As String) As String
        Dim strGatewayResponse As String = "Message not send"
        Try
            If strMobileNo.Length = 10 And IsNumeric(strMobileNo) And strTextMsg.ToString.Length > 0 Then
                'strMobileNo = "91" & strMobileNo
                strGatewayResponse = SendSMSToNum(System.Configuration.ConfigurationManager.AppSettings("sms_key"), "", System.Configuration.ConfigurationManager.AppSettings("sms_senderid"), strMobileNo, strTextMsg, "")
            End If
        Catch ex As Exception
            strGatewayResponse = ex.Message & strGatewayResponse & "Message not send"
        End Try

        Return strGatewayResponse
    End Function
    Public Shared Function SendSms(ByVal strMobileNo As String, ByVal strTextMsg As String) As String
        Dim strGatewayResponse As String = "Message not send"
        Try
            If strMobileNo.Length = 10 And IsNumeric(strMobileNo) And strTextMsg.ToString.Length > 0 Then
                strMobileNo = "91" & strMobileNo
                strGatewayResponse = SendSMSToNum(System.Configuration.ConfigurationManager.AppSettings("sms_userId"), System.Configuration.ConfigurationManager.AppSettings("sms_pwd"), "", strMobileNo, strTextMsg, "")
            End If
        Catch ex As Exception
            strGatewayResponse = ex.Message & strGatewayResponse & "Message not send"
        End Try

        Return strGatewayResponse
    End Function
End Class

