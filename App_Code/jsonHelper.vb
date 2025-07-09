Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Net
Imports System.Runtime.Serialization
Imports Newtonsoft.Json

Public Class JsonHelper
    Public Sub JsonHelper()
        Response = New ResponseId()
    End Sub

    Private _Response As ResponseId
    Public Property Response() As ResponseId
        Get
            Return _Response
        End Get
        Set(ByVal value As ResponseId)
            _Response = value
        End Set
    End Property

    'Public Shared Function Deserialise(Of T)(ByVal json As String) As T
    '    Using ms = New MemoryStream(Encoding.Unicode.GetBytes(json))
    '        Dim serialiser = New DataContractJsonSerializer(GetType(T))
    '        Return DirectCast(serialiser.ReadObject(ms), T)
    '    End Using
    'End Function
End Class
<DataContract()> Public Class ResponseId
    Private _txid As String
    Private _user_txid As String
    Private _status As String
    Private _amount As Decimal
    Private _operator_code As String
    Private _operator_ref As String
    Private _error_code As String
    Private _message As String
    Private _time As String
    Private _your_cost As Decimal
    Private _balance As Decimal


    Public Property txid() As String
        Get
            Return _txid
        End Get
        Set(ByVal value As String)
            _txid = value
        End Set
    End Property
    Public Property user_txid() As String
        Get
            Return _user_txid
        End Get
        Set(ByVal value As String)
            _user_txid = value
        End Set
    End Property

    Public Property status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

    Public Property amount() As Decimal
        Get
            Return _amount
        End Get
        Set(ByVal value As Decimal)
            _amount = value
        End Set
    End Property
    Public Property your_cost() As Decimal
        Get
            Return _your_cost
        End Get
        Set(ByVal value As Decimal)
            _your_cost = value
        End Set
    End Property
    Public Property balance() As Decimal
        Get
            Return _balance
        End Get
        Set(ByVal value As Decimal)
            _balance = value
        End Set
    End Property
    <JsonProperty(PropertyName:="operator")> Public Property operator_code() As String
        Get
            Return _operator_code
        End Get
        Set(ByVal value As String)
            _operator_code = value
        End Set
    End Property
    Public Property operator_ref() As String
        Get
            Return _operator_ref
        End Get
        Set(ByVal value As String)
            _operator_ref = value
        End Set
    End Property
    Public Property error_code() As String
        Get
            Return _error_code
        End Get
        Set(ByVal value As String)
            _error_code = value
        End Set
    End Property

    Public Property message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property
    Public Property time() As String
        Get
            Return _time
        End Get
        Set(ByVal value As String)
            _time = value
        End Set
    End Property
End Class
