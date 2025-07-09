
Partial Class ExamParticular
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindExam()
            BindClass()
            'bind()
            hfId.Value = 0

        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            'Dim dt As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
            'For Each row As DataRow In dt.Rows
            '    Dim res As String = BLL.AddExamParticulars(ddlexam.SelectedValue, "", 1, ddlClass.SelectedValue, row("SubjectCode"), 0, 0, 0, 0, 0, 0, 0)
            'Next row
            'litmsg.Text = Notifications.SuccessMessage("Inserted Sucessfully")
            bind()
            btnUpdate.Visible = True

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            Dim txtExamDate As New TextBox
            Dim txtMaxMarks As New TextBox
            Dim txtPassmarks As New TextBox
            Dim txtPeriodictest As New TextBox
            Dim txtNoteBook As New TextBox
            Dim txtSubjectEnrichment As New TextBox
            Dim txtMaxPracticalMarks As New TextBox
            Dim txtPassPracticalMarks As New TextBox
            'Dim hdfSubjectId As New HiddenField
            For Each mRow As GridViewRow In DataDisplay.Rows
                txtExamDate = mRow.FindControl("txtExamDate")
                txtMaxMarks = mRow.FindControl("txtMaxTheoryMarks")
                txtPassmarks = mRow.FindControl("txtPassTheoryMarks")
                txtPeriodictest = mRow.FindControl("txtPeriodictest")
                txtNoteBook = mRow.FindControl("txtNoteBook")
                txtSubjectEnrichment = mRow.FindControl("txtSubjectEnrichment")
                txtMaxPracticalMarks = mRow.FindControl("txtMaxPracticalMarks")
                txtPassPracticalMarks = mRow.FindControl("txtPassPracticalMarks")
                'hdfSubjectId = mRow.FindControl("hdfSubjectId")
                Dim res As String = BLL.AddExamParticulars(ddlexam.SelectedValue, txtExamDate.Text, 1, ddlClass.SelectedValue, 0, txtMaxMarks.Text, txtMaxPracticalMarks.Text, txtPassmarks.Text, txtPassPracticalMarks.Text, txtPeriodictest.Text, txtNoteBook.Text, txtSubjectEnrichment.Text)

            Next
            litmsg.Text = Notifications.SuccessMessage("Updated Sucessfully")
            'bind()

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand

    End Sub

    Sub bind()
        Dim dt As DataTable = BLL.Get_ExamPeriodicTest(ddlexam.SelectedValue, ddlClass.SelectedValue)
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()

        Dim txtMaxPracticalMarks As New TextBox
        Dim txtPassPracticalMarks As New TextBox
        Dim txtPeriodictest As New TextBox
        Dim txtNoteBook As New TextBox
        Dim txtSubjectEnrichment As New TextBox
        For Each mRow As GridViewRow In DataDisplay.Rows
            txtMaxPracticalMarks = mRow.FindControl("txtMaxPracticalMarks")
            txtPassPracticalMarks = mRow.FindControl("txtPassPracticalMarks")
            txtPeriodictest = mRow.FindControl("txtPeriodictest")
            txtNoteBook = mRow.FindControl("txtNoteBook")
            txtSubjectEnrichment = mRow.FindControl("txtSubjectEnrichment")
            If ddlClass.SelectedValue = "15" Or ddlClass.SelectedValue = "17" Or ddlClass.SelectedValue = "16" Or ddlClass.SelectedValue = "18" Or ddlClass.SelectedValue = "19" Or ddlClass.SelectedValue = "20" Or ddlClass.SelectedValue = "21" Or ddlClass.SelectedValue = "22" Or ddlClass.SelectedValue = "23" Then
                txtMaxPracticalMarks.ReadOnly = False
                txtPassPracticalMarks.ReadOnly = False
                txtPeriodictest.ReadOnly = True
                txtNoteBook.ReadOnly = True
                txtSubjectEnrichment.ReadOnly = True
            Else
                txtMaxPracticalMarks.ReadOnly = True
                txtPassPracticalMarks.ReadOnly = True
                txtPeriodictest.ReadOnly = False
                txtNoteBook.ReadOnly = False
                txtSubjectEnrichment.ReadOnly = False

            End If
        Next

    End Sub

    Sub bindExam()

        ddlexam.DataSource = BLL.BindExams
        ddlexam.DataTextField = "Examname"
        ddlexam.DataValueField = "Examid"
        ddlexam.DataBind()
    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindClass()

        ddlClass.DataTextField = "ClassName"
        ddlClass.DataValueField = "ClassId"
        ddlClass.DataBind()
    End Sub

    Sub bindExamShift()



    End Sub

    Protected Sub ddlexam_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlexam.SelectedIndexChanged
        bindExamShift()

    End Sub


End Class
