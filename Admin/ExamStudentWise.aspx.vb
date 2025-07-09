
Partial Class Admin_ExamStudentWise
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindExam()
            BindClass()
        End If
    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindClass()

        ddlClass.DataTextField = "ClassName"
        ddlClass.DataValueField = "classid"
        ddlClass.DataBind()
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            'Dim dt As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
            'For Each row As DataRow In dt.Rows
            ' Dim res As String = BLL.AddExamParticulars(ddlexam.SelectedValue, "", 1, ddlClass.SelectedValue, row("SubjectCode"), 0, 0, 0, 0, 0, 0, 0)
            'Next row
            'litmsg.Text = Notifications.SuccessMessage("Inserted Sucessfully")
            ' bind()
            DivExamParticulars.Visible = True
            bind()
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            Dim MaxTheoryMarks As String
            Dim PeriodicTestMarksMax As Integer
            Dim NotebookMarks As Integer
            Dim SubEnrichmentMarks As Integer

            Dim dt As DataTable = BLL.Get_ExamPeriodicTest(ddlexam.SelectedValue, ddlClass.SelectedValue)
            If dt.Rows.Count > 0 Then
                MaxTheoryMarks = Convert.ToInt32(dt.Rows(0)("MaxTheoryMarks"))
                PeriodicTestMarksMax = Convert.ToInt32(dt.Rows(0)("PeriodicTestMarksMax"))
                NotebookMarks = Convert.ToInt32(dt.Rows(0)("NotebookMarksMax"))
                SubEnrichmentMarks = Convert.ToInt32(dt.Rows(0)("SubEnrichmentMarksMax"))
                'Else
                '    MaxTheoryMarks = 0
                '    PeriodicTestMarksMax = 0
                '    NotebookMarks = 0
                '    SubEnrichmentMarks = 0
            End If



            Dim txtMaxPracticalMarks As New TextBox
            Dim txtMaxTheoryMarks As New TextBox
            'Dim txtPassTheoryMarks As New TextBox
            Dim txtPeriodictest As New TextBox
            Dim txtNoteBook As New TextBox
            Dim txtSubjectEnrichment As New TextBox
            Dim txtGrade As New TextBox
            Dim chkbAttandance As New CheckBox
            Dim hdfStudentid As New HiddenField
            Dim Attandance As Integer
            For Each mRow As GridViewRow In DataDisplay.Rows

                txtMaxTheoryMarks = mRow.FindControl("txtMaxTheoryMarks")
                ' txtPassTheoryMarks = mRow.FindControl("txtPassTheoryMarks")
                txtPeriodictest = mRow.FindControl("txtPeriodictest")
                txtNoteBook = mRow.FindControl("txtNoteBook")
                txtSubjectEnrichment = mRow.FindControl("txtSubjectEnrichment")
                hdfStudentid = mRow.FindControl("SubjectId")
                txtGrade = mRow.FindControl("txtGrade")
                txtMaxPracticalMarks = mRow.FindControl("txtMxaPracticalMarks")
                chkbAttandance = mRow.FindControl("chkbAttandance")
                If chkbAttandance.Checked Then
                    Attandance = 1
                Else
                    Attandance = 0
                End If
                Dim res As String = BLL.AddExamMarks(ddlexam.SelectedValue, ddlsubject.SelectedValue, hdfStudentid.Value, "0", PeriodicTestMarksMax, txtPeriodictest.Text, NotebookMarks, txtNoteBook.Text, SubEnrichmentMarks, txtSubjectEnrichment.Text, 0, 0, MaxTheoryMarks, txtMaxTheoryMarks.Text, txtGrade.Text, ddlClass.SelectedValue, txtMaxPracticalMarks.Text, Attandance)

            Next
            litmsg.Text = Notifications.SuccessMessage("Marks Submitted Sucessfully")
            'bind()

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub
    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        ddlsubject.Items.Clear()
        Dim SessionYear As String = BLL.ExecScalar("Select SessionYear From tbl_ExamMaster Where ExamId=@ExamId", "@ExamId", ddlexam.SelectedValue)
        ddlsubject.Items.Insert(0, New ListItem("--Select--", "0"))
        ddlsubject.DataSource = BLL.ExecDataTable("select * from studentmaster where classid=@classid and SchoolSessionActive=@SessionYear and TCGenerateDate is Null and isBlock=0 order by StudentName", "@classid", ddlClass.SelectedValue, "@SessionYear", SessionYear)
        ddlsubject.DataTextField = "StudentName"
        ddlsubject.DataValueField = "StudentId"
        ddlsubject.DataBind()

    End Sub

    Sub bind()
        DataDisplay.DataSource = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", ddlexam.SelectedValue, "@StudentId", ddlsubject.SelectedValue, "@MainClassId", ddlClass.SelectedValue, "@SubjectId", "")
        DataDisplay.DataBind()
        Dim txtMxaPracticalMarks As New TextBox
        Dim txtMaxTheoryMarks As New TextBox
        Dim txtTotalMarks As New TextBox
        Dim txtPeriodictest As New TextBox
        Dim txtNoteBook As New TextBox
        Dim txtSubjectEnrichment As New TextBox
        Dim SubjectId As New HiddenField

        For Each mRow As GridViewRow In DataDisplay.Rows

            txtMaxTheoryMarks = mRow.FindControl("txtMaxTheoryMarks")
            txtTotalMarks = mRow.FindControl("txtTotalMarks")
            txtMxaPracticalMarks = mRow.FindControl("txtMxaPracticalMarks")
            txtPeriodictest = mRow.FindControl("txtPeriodictest")
            txtNoteBook = mRow.FindControl("txtNoteBook")
            txtSubjectEnrichment = mRow.FindControl("txtSubjectEnrichment")
            SubjectId = mRow.FindControl("SubjectId")

            If ddlClass.SelectedValue = "15" Or ddlClass.SelectedValue = "16" Or ddlClass.SelectedValue = "17" Or ddlClass.SelectedValue = "18" Or ddlClass.SelectedValue = "19" Or ddlClass.SelectedValue = "20" Or ddlClass.SelectedValue = "21" Or ddlClass.SelectedValue = "22" Then
                txtMxaPracticalMarks.ReadOnly = False
                txtPeriodictest.ReadOnly = True
                txtNoteBook.ReadOnly = True
                txtSubjectEnrichment.ReadOnly = True
                If txtMxaPracticalMarks.Text = "" Then
                    txtMxaPracticalMarks.Text = 0
                End If
                If ddlClass.SelectedValue = "16" Then
                    txtPeriodictest.ReadOnly = False
                End If
                txtTotalMarks.Text = Convert.ToInt32(txtMaxTheoryMarks.Text) + Convert.ToInt32(txtMxaPracticalMarks.Text)
            ElseIf (ddlClass.SelectedValue = "37") Then
                txtMxaPracticalMarks.ReadOnly = False
                txtPeriodictest.ReadOnly = True
                txtNoteBook.ReadOnly = True
                txtSubjectEnrichment.ReadOnly = True
            Else
                'If (ddlClass.SelectedValue = "12" Or ddlClass.SelectedValue = "13") And SubjectId.Value = 124 Then
                If (ddlClass.SelectedValue = "1" Or ddlClass.SelectedValue = "3") And (SubjectId.Value = 124 Or SubjectId.Value = 126) Then
                        txtMxaPracticalMarks.ReadOnly = False
                        txtPeriodictest.ReadOnly = True
                        txtNoteBook.ReadOnly = True
                        txtSubjectEnrichment.ReadOnly = True
                    Else
                        txtMxaPracticalMarks.ReadOnly = True
                    txtPeriodictest.ReadOnly = False
                    txtNoteBook.ReadOnly = False
                    txtSubjectEnrichment.ReadOnly = False
                End If
                If ddlClass.SelectedValue = "14" Then
                    txtMxaPracticalMarks.ReadOnly = False
                End If
                txtTotalMarks.Text = Convert.ToInt32(txtMaxTheoryMarks.Text) + Convert.ToInt32(txtPeriodictest.Text) + Convert.ToInt32(txtNoteBook.Text) + Convert.ToInt32(txtSubjectEnrichment.Text)
            End If
        Next

    End Sub
    Sub bindExam()
        ddlexam.DataSource = BLL.BindExams
        ddlexam.DataTextField = "Examname"
        ddlexam.DataValueField = "Examid"
        ddlexam.DataBind()

    End Sub
End Class
