Imports System.Data.OleDb
Imports System.IO
Partial Class UploadSchoolDoc
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer

    Protected Sub btnsubmit_Click(sender As Object, e As System.EventArgs) Handles btnsubmit.Click

        Try
            If FileUpload1.HasFile Then
                Dim name As String = Guid.NewGuid().ToString
                'File = fileuplod.FileName
                'File = name & System.IO.Path.GetExtension(fileuplod.PostedFile.FileName)
                'fileuplod.SaveAs(FilePath + File)


                Dim fileName As String = name + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName)
                Dim ContentType As String = FileUpload1.PostedFile.ContentType.ToString
                Dim fileExtension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
                Dim fileLocation As String = Server.MapPath("~/upload/document/" & fileName)
                FileUpload1.SaveAs(fileLocation)
                Dim res As String = BLL.AddDocument(txtDocumentName.Text, txtDocumentDetail.Text, fileName, Session("User"), "", 0, 0, ContentType, 1)
                If res.Chars(0) = "#" Then
                    litmsg.Text = Notifications.SuccessMessage(res)
                    bind()
                    txtDocumentName.Text = ""
                    txtDocumentDetail.Text = ""
                Else
                    litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
                End If
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
    End Sub
    'Sub BindClass()
    '    ddlClass.DataSource = BLL.BindClass()

    '    ddlClass.DataTextField = "ClassName"
    '    ddlClass.DataValueField = "ClassId"
    '    ddlClass.DataBind()
    'End Sub
    'Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
    '    Dim subjectid As String = BLL.ExecScalar("select subjectids from classmaster where classid=@classid", "@classid", ddlClass.SelectedValue)
    '    subjectid = Left(subjectid, subjectid.ToString.Length - 1)
    '    ddlsubject.DataSource = BLL.ExecDataTable("select * from subjectmaster where subjectid in (" & subjectid & ")")
    '    ddlsubject.DataTextField = "Subjectname"
    '    ddlsubject.DataValueField = "subjectcode"
    '    ddlsubject.DataBind()
    'End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'BindClass()
            bind()
        End If
    End Sub
    Sub bind()
        DataDisplay.DataSource = BLL.ExecDataTable("select * from tbl_Documents where isGlobal=1")
        DataDisplay.DataBind()
    End Sub
End Class
