Imports Microsoft.VisualBasic
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web

Public Class JsonToDataTable


    Public Function JsonStringToDataTable(jsonString As String) As DataTable
        Dim dt As New DataTable()
        Try
            'string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

            'List<string> ColumnsName = new List<string>();
            'foreach (string jSA in jsonStringArray)
            '{
            '    string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            '    foreach (string ColumnsNameData in jsonStringData)
            '    {
            '        try
            '        {
            '            int idx = ColumnsNameData.IndexOf(":");
            '            string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
            '            if (!ColumnsName.Contains(ColumnsNameString))
            '            {
            '                ColumnsName.Add(ColumnsNameString);
            '            }
            '        }
            '        catch (Exception ex)
            '        {
            '            throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
            '        }
            '    }
            '    break; // TODO: might not be correct. Was : Exit For
            '}
            'foreach (string AddColumnName in ColumnsName)
            '{
            '    dt.Columns.Add(AddColumnName);
            '}
            'foreach (string jSA in jsonStringArray)
            '{
            '    string[] RowData__1 = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            '    DataRow nr = dt.NewRow();
            '    foreach (string rowData__2 in RowData__1)
            '    {
            '        try
            '        {
            '            int idx = rowData__2.IndexOf(":");
            '            string RowColumns = rowData__2.Substring(0, idx - 1).Replace("\"", "");
            '            string RowDataString = rowData__2.Substring(idx + 1).Replace("\"", "");
            '            nr[RowColumns] = RowDataString;

            '        }
            '        catch (Exception ex)
            '        {
            '        }
            '    }
            '    dt.Rows.Add(nr);
            '}

            dt = DirectCast(JsonConvert.DeserializeObject(jsonString, (GetType(DataTable))), DataTable)
        Catch ex As Exception
            dt = Nothing
        End Try
        Return dt
    End Function



    Public Function DataTableToJSONWithStringBuilder(table As DataTable) As String
        Dim JSONString = New StringBuilder()
        Try
            If table.Rows.Count > 0 Then
                JSONString.Append("[")
                For i As Integer = 0 To table.Rows.Count - 1
                    JSONString.Append("{")
                    For j As Integer = 0 To table.Columns.Count - 1
                        If j < table.Columns.Count - 1 Then
                            JSONString.Append("""" + table.Columns(j).ColumnName.ToString() + """:" + """" + table.Rows(i)(j).ToString() + """,")
                        ElseIf j = table.Columns.Count - 1 Then
                            JSONString.Append("""" + table.Columns(j).ColumnName.ToString() + """:" + """" + table.Rows(i)(j).ToString() + """")
                        End If
                    Next
                    If i = table.Rows.Count - 1 Then
                        JSONString.Append("}")
                    Else
                        JSONString.Append("},")
                    End If
                Next
                JSONString.Append("]")
            End If
            Return JSONString.ToString()
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function JsonStringToDataTablePackage(jsonString As String) As DataTable
        Dim dt As New DataTable()
        Try

            Dim jsonStringArray As String() = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{")

            Dim ColumnsName As New List(Of String)()
            For Each jSA As String In jsonStringArray
                Dim jsonStringData As String() = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",")
                For Each ColumnsNameData As String In jsonStringData
                    Try
                        Dim idx As Integer = ColumnsNameData.IndexOf(":")
                        Dim ColumnsNameString As String = ColumnsNameData.Substring(0, idx - 1).Replace("""", "")
                        If Not ColumnsName.Contains(ColumnsNameString) Then
                            ColumnsName.Add(ColumnsNameString)
                        End If
                    Catch ex As Exception
                        Throw New Exception(String.Format("Error Parsing Column Name : {0}", ColumnsNameData))
                    End Try
                Next
                ' TODO: might not be correct. Was : Exit For
                Exit For
            Next
            For Each AddColumnName As String In ColumnsName
                dt.Columns.Add(AddColumnName)
            Next
            For Each jSA As String In jsonStringArray
                Dim RowData__1 As String() = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",")
                Dim nr As DataRow = dt.NewRow()
                For Each rowData__2 As String In RowData__1
                    Try
                        Dim idx As Integer = rowData__2.IndexOf(":")
                        Dim RowColumns As String = rowData__2.Substring(0, idx - 1).Replace("""", "")
                        Dim RowDataString As String = rowData__2.Substring(idx + 1).Replace("""", "")

                        nr(RowColumns) = RowDataString
                    Catch ex As Exception
                    End Try
                Next
                dt.Rows.Add(nr)

            Next
        Catch ex As Exception
            dt = Nothing
        End Try
        Return dt
    End Function

End Class
