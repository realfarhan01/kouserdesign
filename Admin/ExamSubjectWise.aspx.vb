
Partial Class Admin_ExamSubjectWise


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
        ddlClass.DataValueField = "ClassId"
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

            Dim txtMaxPracticalMarks As New TextBox
            Dim txtMaxTheoryMarks As New TextBox
            'Dim txtPassTheoryMarks As New TextBox
            Dim txtPeriodictest As New TextBox
            Dim txtNoteBook As New TextBox
            Dim txtSubjectEnrichment As New TextBox
            Dim txtGrade As New TextBox
            Dim hdfStudentid As New HiddenField
            Dim chkbAttandance As New CheckBox
            Dim Attandance As Integer
            For Each mRow As GridViewRow In DataDisplay.Rows

                txtMaxTheoryMarks = mRow.FindControl("txtMaxTheoryMarks")
                ' txtPassTheoryMarks = mRow.FindControl("txtPassTheoryMarks")
                txtPeriodictest = mRow.FindControl("txtPeriodictest")
                txtNoteBook = mRow.FindControl("txtNoteBook")
                txtSubjectEnrichment = mRow.FindControl("txtSubjectEnrichment")
                txtGrade = mRow.FindControl("txtGrade")

                hdfStudentid = mRow.FindControl("hdfStudentid")
                txtMaxPracticalMarks = mRow.FindControl("txtMxaPracticalMarks")
                chkbAttandance = mRow.FindControl("chkbAttandance")
                If chkbAttandance.Checked Then
                    Attandance = 1
                Else
                    Attandance = 0
                End If
                Dim res As String = BLL.AddExamMarks(ddlexam.SelectedValue, hdfStudentid.Value, ddlsubject.SelectedValue, "0", PeriodicTestMarksMax.Value, txtPeriodictest.Text, NotebookMarksMax.Value, txtNoteBook.Text, SubEnrichmentMarksMax.Value, txtSubjectEnrichment.Text, 0, 0, MaxTheoryMarks.Value, txtMaxTheoryMarks.Text, txtGrade.Text, ddlClass.SelectedValue, txtMaxPracticalMarks.Text, Attandance)

            Next
            litmsg.Text = Notifications.SuccessMessage("Marks Submitted Sucessfully")
            'bind()

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub
    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged

        Dim subjectid As String = BLL.ExecScalar("select subjectids from classmaster where classid=@classid", "@classid", ddlClass.SelectedValue)
        subjectid = Left(subjectid, subjectid.ToString.Length - 1)
        ddlsubject.Items.Clear()
        ddlsubject.Items.Insert(0, New ListItem("--Select--", "0"))
        ddlsubject.DataSource = BLL.ExecDataTable("select * from subjectmaster where subjectid in (" & subjectid & ")")
        ddlsubject.DataTextField = "Subjectname"
        ddlsubject.DataValueField = "subjectcode"
        ddlsubject.DataBind()

    End Sub

    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTableProc("Prc_GetExamResultSubjectWise", "@ExamId", ddlexam.SelectedValue, "@StudentId", "", "@MainClassId", ddlClass.SelectedValue, "@SubjectId", ddlsubject.SelectedValue)

        Dim dt1 As DataTable = BLL.ExecDataTable("select PeriodicTestMarksMax,NotebookMarksMax,SubEnrichmentMarksMax,MaxTheoryMarks from tbl_ExamParticulars where ExamId=@ExamId  and ClassId=@ClassId", "@ExamId", ddlexam.SelectedValue, "@classid", ddlClass.SelectedValue)
        If dt1.Rows.Count > 0 Then
            PeriodicTestMarksMax.Value = dt1.Rows(0)("PeriodicTestMarksMax")
            NotebookMarksMax.Value = dt1.Rows(0)("NotebookMarksMax")
            SubEnrichmentMarksMax.Value = dt1.Rows(0)("SubEnrichmentMarksMax")
            MaxTheoryMarks.Value = dt1.Rows(0)("MaxTheoryMarks")
        Else
            PeriodicTestMarksMax.Value = 0
            NotebookMarksMax.Value = 0
            SubEnrichmentMarksMax.Value = 0
            MaxTheoryMarks.Value = 0
        End If

        DataDisplay.DataSource = dt
        DataDisplay.DataBind()

        Dim txtMaxTheoryMarks As New TextBox
        Dim txtTotalMarks As New TextBox

        Dim txtMxaPracticalMarks As New TextBox
        Dim txtPeriodictest As New TextBox
        Dim txtNoteBook As New TextBox
        Dim txtSubjectEnrichment As New TextBox
        For Each mRow As GridViewRow In DataDisplay.Rows

            txtMaxTheoryMarks = mRow.FindControl("txtMaxTheoryMarks")
            txtTotalMarks = mRow.FindControl("txtTotalMarks")
            txtMxaPracticalMarks = mRow.FindControl("txtMxaPracticalMarks")
            txtPeriodictest = mRow.FindControl("txtPeriodictest")
            txtNoteBook = mRow.FindControl("txtNoteBook")
            txtSubjectEnrichment = mRow.FindControl("txtSubjectEnrichment")
            If ddlClass.SelectedValue = "15" Or ddlClass.SelectedValue = "16" Or ddlClass.SelectedValue = "17" Or ddlClass.SelectedValue = "18" Or ddlClass.SelectedValue = "19" Or ddlClass.SelectedValue = "20" Or ddlClass.SelectedValue = "21" Or ddlClass.SelectedValue = "22" Then

                txtMxaPracticalMarks.ReadOnly = False
                txtPeriodictest.ReadOnly = True
                txtNoteBook.ReadOnly = True
                txtSubjectEnrichment.ReadOnly = True

                If ddlClass.SelectedValue = "16" Then
                    txtPeriodictest.ReadOnly = False
                End If
                txtTotalMarks.Text = Convert.ToInt32(txtMaxTheoryMarks.Text) + Convert.ToInt32(txtMxaPracticalMarks.Text)
            Else
                If (ddlClass.SelectedValue = "1" Or ddlClass.SelectedValue = "3" Or ddlClass.SelectedValue = "29" Or ddlClass.SelectedValue = "32") And (ddlsubject.SelectedValue = 124 Or ddlsubject.SelectedValue = 126 Or ddlsubject.SelectedValue = 127) Then
                    txtMxaPracticalMarks.ReadOnly = False
                    txtPeriodictest.ReadOnly = True
                    txtNoteBook.ReadOnly = True
                    txtSubjectEnrichment.ReadOnly = True
                    txtTotalMarks.Text = Convert.ToInt32(txtMaxTheoryMarks.Text) + Convert.ToInt32(txtMxaPracticalMarks.Text)
                ElseIf (ddlClass.SelectedValue = "37") Then
                    txtMxaPracticalMarks.ReadOnly = False
                    txtPeriodictest.ReadOnly = True
                    txtNoteBook.ReadOnly = True
                    txtSubjectEnrichment.ReadOnly = True
                    txtTotalMarks.Text = Convert.ToInt32(txtMaxTheoryMarks.Text) + Convert.ToInt32(txtPeriodictest.Text) + Convert.ToInt32(txtNoteBook.Text) + Convert.ToInt32(txtSubjectEnrichment.Text)

                Else
                    txtMxaPracticalMarks.ReadOnly = True
                    txtPeriodictest.ReadOnly = False
                    txtNoteBook.ReadOnly = False
                    txtSubjectEnrichment.ReadOnly = False
                    txtTotalMarks.Text = Convert.ToInt32(txtMaxTheoryMarks.Text) + Convert.ToInt32(txtPeriodictest.Text) + Convert.ToInt32(txtNoteBook.Text) + Convert.ToInt32(txtSubjectEnrichment.Text)
                End If
                If ddlClass.SelectedValue = "14" Then
                    txtMxaPracticalMarks.ReadOnly = False
                End If

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
