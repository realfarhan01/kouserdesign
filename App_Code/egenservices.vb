Imports Microsoft.VisualBasic
Imports JsonHelper
Imports System.Net
Imports System.IO

Public Class egenservices
    Public Enum Method
        [GET]
        POST
    End Enum
    Public Function getBalance(ByVal UserName As String, ByVal Password As String) As ResponseId

        Return New ResponseId
    End Function

    Public Function DoTransaction(ByVal Operator_code As String, ByVal circle_code As String, ByVal accountno As String, ByVal amount As Integer, ByVal RequestId As Integer) As String
        Dim urlstr As String = "http://recharge.dlinfotech.com/api/recharge.php?uid=" & System.Configuration.ConfigurationManager.AppSettings("recharge_uid") & "&pin=" & System.Configuration.ConfigurationManager.AppSettings("recharge_pin") & "&operator=" & Operator_code & "&circle=" & circle_code & "&number=" & accountno & "&amount=" & amount & "&usertx=" & RequestId & "&format=json&version=4&date=" & DateTime.UtcNow.AddMinutes(330).Date.ToString("dd-MM-yyyy")
        Dim json As String = WebRequest(Method.GET, urlstr, String.Empty)
        Return json
    End Function
    Public Function TransactionStatus(ByVal UserName As String, ByVal Password As String, ByVal clientTranId As String) As ResponseId
        Return New ResponseId
    End Function
    Public Function WebRequest(ByVal MType As Method, ByVal url As String, ByVal postData As String) As String
        Dim webReq As HttpWebRequest = Nothing
        Dim requestWriter As StreamWriter = Nothing
        Dim responseData As String = ""
        webReq = TryCast(System.Net.WebRequest.Create(url), HttpWebRequest)
        webReq.Method = MType.ToString()
        webReq.ServicePoint.Expect100Continue = False
        webReq.UserAgent = "[You user agent]"
        webReq.Timeout = 20000
        If MType = Method.POST Then
            webReq.ContentType = "application/x-www-form-urlencoded"
            'POST the data.
            requestWriter = New StreamWriter(webReq.GetRequestStream())
            Try
                requestWriter.Write(postData)
            Catch
                Throw
            Finally
                requestWriter.Close()
                requestWriter = Nothing
            End Try
        End If
        responseData = WebResponseGet(webReq)
        WebRequest = Nothing
        Return responseData
    End Function
    ''' <summary>
    ''' Process the web response.
    ''' </summary>
    ''' <param name="webRequest">The request object.</param>
    ''' <returns>The response data.</returns>
    Public Function WebResponseGet(ByVal webRequest As HttpWebRequest) As String
        Dim responseReader As StreamReader = Nothing
        Dim responseData As String = ""
        Try
            responseReader = New StreamReader(webRequest.GetResponse().GetResponseStream())
            responseData = responseReader.ReadToEnd()
        Catch
            Throw
        Finally
            webRequest.GetResponse().GetResponseStream().Close()
            responseReader.Close()
            responseReader = Nothing
        End Try
        Return responseData
    End Function
End Class
