Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Public Class DataAccessLayer
    'Dim Con As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
    Dim ctx As HttpContext = HttpContext.Current
    Private Function CreateCommand(ByVal Query As String, ByVal CmdType As CommandType, ByVal ParamArray obj() As Object) As SqlCommand
        Dim cmd As New SqlCommand(Query)
        Try
            cmd.CommandType = CmdType
            For i As Integer = 0 To obj.Length - 1
                If TypeOf obj(i) Is String And i < obj.Length - 1 Then
                    Dim Parm As New SqlParameter
                    Parm.ParameterName = obj(i)
                    i = i + 1
                    Parm.Value = obj(i)
                    cmd.Parameters.Add(Parm)
                ElseIf TypeOf obj(i) Is SqlParameter Then
                    cmd.Parameters.Add(obj(i))
                Else
                    Throw New ArgumentException("Invalid number or type of arguments supplied")
                End If
            Next
        Catch ex As Exception
            Return Nothing
        End Try
        Return cmd
    End Function

    Public Function ExecNonQuery(ByVal Query As String, ByVal ParamArray obj() As Object) As Integer
        Dim result As Integer = 0
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
            Using cmd As SqlCommand = CreateCommand(Query, CommandType.Text, obj)
                Try
                    cmd.Connection = conn
                    conn.Open()
                    result = cmd.ExecuteNonQuery()
                Catch ex As Exception
                    result = 0
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
        Return result
    End Function

    Public Function ExecNonQueryProc(ByVal Proc As String, ByVal ParamArray obj() As Object) As String
        Dim result As String = "!"
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
            Using cmd As SqlCommand = CreateCommand(Proc, CommandType.StoredProcedure, obj)
                Try
                    cmd.Connection = conn
                    conn.Open()
                    result = cmd.ExecuteNonQuery()
                    If obj.Length > 0 AndAlso cmd.Parameters(cmd.Parameters.Count - 1).Direction = ParameterDirection.Output Then
                        result = cmd.Parameters(cmd.Parameters.Count - 1).Value
                    End If
                Catch ex As Exception
                    result = Nothing
                    Email_onError(ex, "Function ExecNonQueryProc")
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
        Return result
    End Function

    Public Function ExecScalar(ByVal Query As String, ByVal ParamArray obj() As Object) As Object
        Dim result As Object
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
            Using cmd As SqlCommand = CreateCommand(Query, CommandType.Text, obj)
                Try
                    cmd.Connection = conn
                    conn.Open()
                    result = cmd.ExecuteScalar()
                Catch ex As Exception
                    result = Nothing
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
        Return result
    End Function

    Public Function ExecScalarProc(ByVal Proc As String, ByVal ParamArray obj() As Object) As String
        Dim result As Object
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
            Using cmd As SqlCommand = CreateCommand(Proc, CommandType.StoredProcedure, obj)
                Try
                    cmd.Connection = conn
                    conn.Open()
                    result = cmd.ExecuteScalar()
                    If obj.Length > 0 AndAlso cmd.Parameters(cmd.Parameters.Count - 1).Direction = ParameterDirection.Output Then
                        result = cmd.Parameters(cmd.Parameters.Count - 1).Value
                    End If
                Catch ex As Exception
                    result = Nothing
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
        Return result
    End Function
    Public Function ExecDataReader(ByVal Query As String, ByVal ParamArray obj() As Object) As SqlDataReader
        Dim result As SqlDataReader
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
        Using cmd As SqlCommand = CreateCommand(Query, CommandType.Text, obj)
            Try
                cmd.Connection = conn
                conn.Open()
                result = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                result = Nothing
            Finally

            End Try
        End Using
        Return result
    End Function

    Public Function ExecDataReaderProc(ByVal Proc As String, ByVal ParamArray obj() As Object) As SqlDataReader
        Dim result As SqlDataReader
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
        Using cmd As SqlCommand = CreateCommand(Proc, CommandType.StoredProcedure, obj)
            Try
                cmd.Connection = conn
                conn.Open()
                result = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                result = Nothing
            Finally
            End Try
        End Using
        Return result
    End Function
    Public Function ExecDataTable(ByVal Query As String, ByVal ParamArray obj() As Object) As DataTable
        Dim Dt As New DataTable
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
            Using cmd As SqlCommand = CreateCommand(Query, CommandType.Text, obj)
                Try
                    cmd.Connection = conn
                    conn.Open()
                    Dt.Load(cmd.ExecuteReader)
                Catch ex As Exception
                    Dt = Nothing
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
        Return Dt
    End Function
    Public Function ExecDataTableProc(ByVal Proc As String, ByVal ParamArray obj() As Object) As DataTable
        Dim result As New DataTable
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
            Using cmd As SqlCommand = CreateCommand(Proc, CommandType.StoredProcedure, obj)
                Try
                    cmd.Connection = conn
                    conn.Open()
                    result.Load(cmd.ExecuteReader)
                Catch ex As Exception
                    result = Nothing
                Finally
                    conn.Close()
                End Try
            End Using
        End Using

        Return result
    End Function
    Public Function GetDataSet(ByVal Query As String, ByVal ParamArray obj() As Object) As DataSet
        Dim ds As New DataSet()
        Using Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
            Using cmd As SqlCommand = CreateCommand(Query, CommandType.Text, obj)
                Using sda As New SqlDataAdapter()
                    Try
                        cmd.Connection = Conn
                        sda.SelectCommand = cmd
                        Conn.Open()
                        sda.Fill(ds)
                    Catch ex As Exception
                        ds = Nothing
                    Finally
                        Conn.Close()
                    End Try
                End Using
            End Using
        End Using
        Return ds
    End Function
    Public Sub Email_onError(ByVal ex As Exception, ByVal MailSub As String)
        If HttpContext.Current.Request.IsLocal Then
            Dim templateVars As New Hashtable()
            templateVars.Add("ErrorIn", ctx.Request.Url)
            templateVars.Add("ErrorMsg", ex.Message)
            templateVars.Add("StackTrace", ex.StackTrace)
            Email.SendEmail("error.htm", templateVars, System.Configuration.ConfigurationManager.AppSettings("email"), System.Configuration.ConfigurationManager.AppSettings("errormail"), MailSub)
        End If
    End Sub
End Class
