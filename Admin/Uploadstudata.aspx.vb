Imports System.Data.OleDb
Imports System.IO
Partial Class Admin_Uploadstudata
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer

    Protected Sub btnsubmit_Click(sender As Object, e As System.EventArgs) Handles btnsubmit.Click
        Dim strConnection As [String] = "ConnectionString"
        Dim connectionString As String = ""
        Try
            If FileUpload1.HasFile Then
                Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim fileExtension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
                Dim fileLocation As String = Server.MapPath("~/upload/excel/" & fileName)
                FileUpload1.SaveAs(fileLocation)
                If fileExtension = ".xls" Then
                    connectionString = (Convert.ToString("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=") & fileLocation) + ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=2"""
                ElseIf fileExtension = ".xlsx" Then
                    connectionString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & fileLocation) + ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
                End If
                Dim con As New OleDbConnection(connectionString)
                Dim cmd As New OleDbCommand()
                cmd.CommandType = System.Data.CommandType.Text
                cmd.Connection = con
                Dim dAdapter As New OleDbDataAdapter(cmd)
                Dim dtExcelRecords As New DataTable()
                con.Open()
                Dim dtExcelSheetName As DataTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim getExcelSheetName As String = dtExcelSheetName.Rows(0)("Table_Name").ToString()
                cmd.CommandText = (Convert.ToString("SELECT * FROM [") & getExcelSheetName) + "]"
                dAdapter.SelectCommand = cmd
                dAdapter.Fill(dtExcelRecords)

                Dim res As String = BLL.UploadStudata(dtExcelRecords)
                If res.Chars(0) = "#" Then
                    litmsg.Text = Notifications.SuccessMessage(res)
                Else
                    litmsg.Text = Notifications.ErrorMessage(res)
                End If



            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Please Match Your Data With Sample Data.")
        End Try
    End Sub
End Class
