Imports System.Web.Services
Imports System.Data.SqlClient
Partial Class Admin_ExamPrintTRSheet
    Inherits System.Web.UI.Page


    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'bind()
            'ltrSchool.Text = BLL.BindSchoolHeader()
            'Literal1.Text = ltrSchool.Text
            'Literal2.Text = ltrSchool.Text
            'Literal3.Text = ltrSchool.Text
            bindExam()
            BindClass()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            hdnExamId.Value = ddlexam.SelectedValue
            hdnClassId.Value = ddlClass.SelectedValue
            Dim termId As String
            Dim dtExamMaster As DataTable = BLL.ExecDataTable("select * from tbl_ExamMaster where ExamId=@ExamId", "@ExamId", ddlexam.SelectedValue)
            If dtExamMaster.Rows.Count > 0 Then
                termId = dtExamMaster.Rows(0)("TermId").ToString()
            End If

            If (ddlClass.SelectedValue = "1" Or ddlClass.SelectedValue = "32") And termId = "1" Then
                Div1.Visible = True
                tblTerm12.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblTerm1"

                Repeater1.DataSource = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                Repeater1.DataBind()



                'Correct
            ElseIf termId = "1" And (ddlClass.SelectedValue = "15" Or ddlClass.SelectedValue = "17" Or ddlClass.SelectedValue = "16" Or ddlClass.SelectedValue = "30" Or ddlClass.SelectedValue = "18" Or ddlClass.SelectedValue = "20" Or ddlClass.SelectedValue = "21" Or ddlClass.SelectedValue = "22" Or ddlClass.SelectedValue = "23" Or ddlClass.SelectedValue = "19") Then
                Div1.Visible = False
                tblTerm12.Visible = False
                tblEleTwe1.Visible = True
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblTerm1"
                RepeaterEleTwe1.DataSource = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                RepeaterEleTwe1.DataBind()


            ElseIf (ddlClass.SelectedValue = "15" Or ddlClass.SelectedValue = "17" Or ddlClass.SelectedValue = "16" Or ddlClass.SelectedValue = "30" Or ddlClass.SelectedValue = "18") And termId = "2" Then

                tblTerm12.Visible = False
                Div1.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = True
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblEleTwe12"


                RepeaterEleTwe12.DataSource = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                RepeaterEleTwe12.DataBind()



                'Correct
            ElseIf termId = "2" And (ddlClass.SelectedValue = "1" Or ddlClass.SelectedValue = "32" Or ddlClass.SelectedValue = "20" Or ddlClass.SelectedValue = "21" Or ddlClass.SelectedValue = "22" Or ddlClass.SelectedValue = "23" Or ddlClass.SelectedValue = "19") Then
                tblTerm12.Visible = False
                Div1.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
            ElseIf termId = "2" And (ddlClass.SelectedValue = 3 Or ddlClass.SelectedValue = 29 Or ddlClass.SelectedValue = 34) Then
                Div1.Visible = False
                tblTerm12.Visible = False
                tblTerm12IX2.Visible = True
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False

                hdnSection.Value = "tblTerm12IX2"
                RepeaterTerm12IX2.DataSource = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                RepeaterTerm12IX2.DataBind()

            ElseIf termId = "2" Then
                tblTerm12.Visible = True
                Div1.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblTerm12"

                RptrTerm12.DataSource = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                RptrTerm12.DataBind()

            ElseIf termId = "1" Then
                Div1.Visible = True
                tblTerm12.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblTerm1"
                Repeater1.DataSource = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                Repeater1.DataBind()

            End If

            litmsg.Text = ""

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

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
    Private Sub Repeater1_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        Dim lblPeriodicTest As Label
        Dim lblNoteBook As Label
        Dim lblSubEnrich As Label
        Dim lblHalfExam As Label
        Dim lblTotalMarks As Label
        Dim ltrlMark As Literal
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            lblPeriodicTest = e.Item.FindControl("lblPer")
            lblNoteBook = e.Item.FindControl("lblNote")
            lblSubEnrich = e.Item.FindControl("lblSubEnrich")
            lblHalfExam = e.Item.FindControl("lblAnnualExamination")
            lblTotalMarks = e.Item.FindControl("lblTotalMarksObtained")
            ltrlMark = e.Item.FindControl("ltrlMark")

            Dim dtParticular12 As DataTable = BLL.ExecDataTable("select top 1 * from tbl_Examparticulars where ExamId=@ExamId and ClassId=@ClassId", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
            If dtParticular12.Rows.Count > 0 Then
                lblPeriodicTest.Text = dtParticular12.Rows(0)("PeriodicTestMarksMax")
                lblNoteBook.Text = dtParticular12.Rows(0)("NotebookMarksMax")
                lblSubEnrich.Text = dtParticular12.Rows(0)("SubEnrichmentMarksMax")
                lblHalfExam.Text = dtParticular12.Rows(0)("MaxTheoryMarks")
                lblTotalMarks.Text = dtParticular12.Rows(0)("MaxTotalmarks")
            End If

            Dim table As New DataTable

            table.Columns.Add("SubjectCode", GetType(Integer))
            table.Columns.Add("SubjectName", GetType(String))
            table.Columns.Add("PeriodicTestMarks1", GetType(Integer))
            table.Columns.Add("NotebookMarks1", GetType(Integer))
            table.Columns.Add("SubEnrichmentMarks1", GetType(Integer))
            table.Columns.Add("TotalMarks1", GetType(Integer))
            table.Columns.Add("Annual_Marks1", GetType(Integer))
            table.Columns.Add("Grade1", GetType(String))

            'table.Columns.Add("OveralllTotalMarks", GetType(String))
            'table.Columns.Add("OverallGrade", GetType(String))

            table.Clear()
            Dim dt123 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
            ltrlMark.Text = ""

            If dt123.Rows.Count > 0 Then
                For Each row As DataRow In dt123.Rows
                    Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, e.Item.DataItem("StudentId"))
                    If data.Rows.Count > 0 Then
                        Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data.Rows(0)("NotebookMarks")) + Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks"))
                        table.Rows.Add(Convert.ToInt32(row("SubjectCode")), row("SubjectName").ToString(), Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")), Convert.ToInt32(data.Rows(0)("NotebookMarks")), Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks")), Convert.ToInt32(data.Rows(0)("TotalMarks")), Annual_Marks, data.Rows(0)("Grade").ToString())
                    Else
                    End If
                Next row
            End If

            If table.Rows.Count > 0 Then
                For Each row As DataRow In table.Rows
                    Dim PeriodicTestMarks2 As String = row("PeriodicTestMarks1").ToString
                    If PeriodicTestMarks2 = "0" Then
                        PeriodicTestMarks2 = "-"
                    End If
                    Dim NotebookMarks2 As String = row("NotebookMarks1").ToString
                    If NotebookMarks2 = "0" Then
                        NotebookMarks2 = "-"
                    End If
                    Dim SubEnrichmentMarks2 As String = row("SubEnrichmentMarks1").ToString
                    If SubEnrichmentMarks2 = "0" Then
                        SubEnrichmentMarks2 = "-"
                    End If
                    Dim TotalMarks2 As String = row("TotalMarks1").ToString
                    If TotalMarks2 = "0" Then
                        TotalMarks2 = "-"
                    End If
                    Dim Grade2 As String = row("Grade1").ToString
                    Dim Annual_Marks2 As String = row("Annual_Marks1").ToString
                    If Annual_Marks2 = "0" Then
                        Annual_Marks2 = "-"
                        Grade2 = "-"
                    End If
                    Dim PeriodicTestMarks1 As String = row("PeriodicTestMarks1").ToString
                    If PeriodicTestMarks1 = "0" Then
                        PeriodicTestMarks1 = "-"
                    End If
                    Dim NotebookMarks1 As String = row("NotebookMarks1").ToString
                    If NotebookMarks1 = "0" Then
                        NotebookMarks1 = "-"
                    End If
                    Dim SubEnrichmentMarks1 As String = row("SubEnrichmentMarks1").ToString
                    If SubEnrichmentMarks1 = "0" Then
                        SubEnrichmentMarks1 = "-"
                    End If
                    Dim Grade1 As String = row("Grade1").ToString
                    Dim Annual_Marks1 As String = row("Annual_Marks1").ToString
                    If Annual_Marks1 = "0" Then
                        Annual_Marks1 = "-"
                        Grade1 = "-"
                    End If
                    Dim TotalMarks1 As String = row("TotalMarks1").ToString
                    If TotalMarks1 = "0" Then
                        TotalMarks1 = "-"
                    End If


                    ltrlMark.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>" & PeriodicTestMarks1 & "</td><td>" & NotebookMarks1 & "</td><td>" & SubEnrichmentMarks1 & "</td><td>" & TotalMarks1 & "</td><td>" & Annual_Marks1 & "</td><td>" & Grade1 & "</td></tr>"
                    'ltrlMarks2.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>" & PeriodicTestMarks1 & "</td><td>" & NotebookMarks1 & "</td><td>" & SubEnrichmentMarks1 & "</td><td>" & TotalMarks1 & "</td><td>" & Annual_Marks1 & "</td><td>" & Grade1 & "</td><td>" & PeriodicTestMarks2 & "</td><td>" & NotebookMarks2 & "</td><td>" & SubEnrichmentMarks2 & "</td><td>" & TotalMarks2 & "</td><td>" & Annual_Marks2 & "</td><td>" & Grade2 & "</td><td>" & OveralllTotalMarks & "</td><td>" & OverallGrade & "</td></tr>"
                Next row
            End If

        End If


    End Sub

    Private Sub RepeaterEleTwe1_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles RepeaterEleTwe1.ItemDataBound
        Dim lblElMarks As Literal
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            lblElMarks = e.Item.FindControl("lblElMarks")

            Dim table As New DataTable

            table.Columns.Add("SubjectCode", GetType(Integer))
            table.Columns.Add("SubjectName", GetType(String))
            table.Columns.Add("TotalMarks", GetType(Integer))
            table.Columns.Add("MxaPracticalMarks", GetType(Integer))
            table.Columns.Add("AllTotalMarks", GetType(Integer))

            table.Clear()

            Dim dt123 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
            lblElMarks.Text = ""
            Dim TotalMarks As String = ""
            Dim MxaPracticalMarks As String = ""
            Dim AnnualMarks As String = ""
            If dt123.Rows.Count > 0 Then
                For Each row As DataRow In dt123.Rows
                    Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, e.Item.DataItem("StudentId"))
                    If data.Rows.Count > 0 Then
                        If data.Rows(0)("TotalMarks") = 0 Then
                            TotalMarks = ""
                        Else
                            TotalMarks = data.Rows(0)("TotalMarks").ToString()
                        End If
                        If data.Rows(0)("MxaPracticalMarks") = 0 Then
                            MxaPracticalMarks = ""
                        Else
                            MxaPracticalMarks = data.Rows(0)("MxaPracticalMarks").ToString()
                        End If
                        Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("MxaPracticalMarks"))
                        If Annual_Marks = 0 Then
                            AnnualMarks = ""
                        Else
                            AnnualMarks = Annual_Marks

                        End If
                        table.Rows.Add(Convert.ToInt32(row("SubjectCode")), row("SubjectName").ToString(), Convert.ToInt32(data.Rows(0)("TotalMarks")), Convert.ToInt32(data.Rows(0)("MxaPracticalMarks")), Annual_Marks)
                    Else

                    End If
                Next row
            End If

            If table.Rows.Count > 0 Then
                For Each row As DataRow In table.Rows
                    Dim Theory As String = row("TotalMarks").ToString
                    If Theory = "0" Then
                        Theory = "-"
                    End If
                    Dim Practical As String = row("MxaPracticalMarks").ToString
                    If Practical = "0" Then
                        Practical = "-"
                    End If
                    Dim AllTotal As String = row("AllTotalMarks").ToString
                    If AllTotal = "0" Then
                        AllTotal = "-"
                    End If

                    lblElMarks.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>" & Theory & "</td><td>" & Practical & "</td><td>" & AllTotal & "</td></tr>"

                Next row
            End If

        End If


    End Sub



    Private Sub RptrTerm12_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles RptrTerm12.ItemDataBound
        Dim lblPeriodicTest1 As Label
        Dim lblNoteBook1 As Label
        Dim lblSubEnrich1 As Label
        Dim lblHalfExam1 As Label
        Dim lblTotalMarks1 As Label
        Dim lblPeriodicTest2 As Label
        Dim lblNoteBook2 As Label
        Dim lblSubEnrich2 As Label
        Dim lblHalfExam2 As Label
        Dim lblTotalMarks2 As Label
        Dim ltrlMarks2 As Literal
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            lblPeriodicTest1 = e.Item.FindControl("lblPeriodicTest1")
            lblNoteBook1 = e.Item.FindControl("lblNoteBook1")
            lblSubEnrich1 = e.Item.FindControl("lblSubEnrich1")
            lblHalfExam1 = e.Item.FindControl("lblHalfExam1")
            lblTotalMarks1 = e.Item.FindControl("lblTotalMarks1")
            lblPeriodicTest2 = e.Item.FindControl("lblPeriodicTest2")
            lblNoteBook2 = e.Item.FindControl("lblNoteBook2")
            lblSubEnrich2 = e.Item.FindControl("lblSubEnrich2")
            lblHalfExam2 = e.Item.FindControl("lblHalfExam2")
            lblTotalMarks2 = e.Item.FindControl("lblTotalMarks2")
            ltrlMarks2 = e.Item.FindControl("ltrlMarks2")

            Dim dtParticular12 As DataTable = BLL.ExecDataTable("select top 1 * from tbl_Examparticulars where ExamId=@ExamId and ClassId=@ClassId", "@ExamId", 9, "@ClassId", ddlClass.SelectedValue)
            If dtParticular12.Rows.Count > 0 Then
                lblPeriodicTest1.Text = dtParticular12.Rows(0)("PeriodicTestMarksMax")
                lblNoteBook1.Text = dtParticular12.Rows(0)("NotebookMarksMax")
                lblSubEnrich1.Text = dtParticular12.Rows(0)("SubEnrichmentMarksMax")
                lblHalfExam1.Text = dtParticular12.Rows(0)("MaxTheoryMarks")
                lblTotalMarks1.Text = dtParticular12.Rows(0)("MaxTotalmarks")
            End If
            Dim dtParticular22 As DataTable = BLL.ExecDataTable("select top 1 * from tbl_Examparticulars where ExamId=@ExamId and ClassId=@ClassId", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
            If dtParticular22.Rows.Count > 0 Then
                lblPeriodicTest2.Text = dtParticular22.Rows(0)("PeriodicTestMarksMax")
                lblNoteBook2.Text = dtParticular22.Rows(0)("NotebookMarksMax")
                lblSubEnrich2.Text = dtParticular22.Rows(0)("SubEnrichmentMarksMax")
                lblHalfExam2.Text = dtParticular22.Rows(0)("MaxTheoryMarks")
                lblTotalMarks2.Text = dtParticular22.Rows(0)("MaxTotalmarks")
            End If
            Dim table As New DataTable

            table.Columns.Add("SubjectCode", GetType(Integer))
            table.Columns.Add("SubjectName", GetType(String))
            table.Columns.Add("PeriodicTestMarks1", GetType(Integer))
            table.Columns.Add("NotebookMarks1", GetType(Integer))
            table.Columns.Add("SubEnrichmentMarks1", GetType(Integer))
            table.Columns.Add("TotalMarks1", GetType(Integer))
            table.Columns.Add("Annual_Marks1", GetType(Integer))
            table.Columns.Add("Grade1", GetType(String))
            table.Columns.Add("PeriodicTestMarks2", GetType(Integer))
            table.Columns.Add("NotebookMarks2", GetType(Integer))
            table.Columns.Add("SubEnrichmentMarks2", GetType(Integer))
            table.Columns.Add("TotalMarks2", GetType(Integer))
            table.Columns.Add("Annual_Marks2", GetType(Integer))
            table.Columns.Add("Grade2", GetType(String))
            table.Columns.Add("OveralllTotalMarks", GetType(String))
            table.Columns.Add("OverallGrade", GetType(String))

            table.Clear()
            Dim dt123 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
            ltrlMarks2.Text = ""

            If dt123.Rows.Count > 0 Then
                For Each row As DataRow In dt123.Rows
                    Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), "9", e.Item.DataItem("StudentId"))
                    If data.Rows.Count > 0 Then
                        Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data.Rows(0)("NotebookMarks")) + Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks"))
                        table.Rows.Add(Convert.ToInt32(row("SubjectCode")), row("SubjectName").ToString(), Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")), Convert.ToInt32(data.Rows(0)("NotebookMarks")), Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks")), Convert.ToInt32(data.Rows(0)("TotalMarks")), Annual_Marks, data.Rows(0)("Grade").ToString(), 0, 0, 0, 0, 0, "", "", "")
                    Else
                    End If
                    Dim data1 As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, e.Item.DataItem("StudentId"))
                    If data1.Rows.Count > 0 And data.Rows.Count > 0 Then

                        Dim Row1() As Data.DataRow
                        Row1 = table.Select("SubjectCode = '" & row("SubjectCode") & "'")
                        Row1(0)("PeriodicTestMarks2") = Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks"))
                        Row1(0)("NotebookMarks2") = Convert.ToInt32(data1.Rows(0)("NotebookMarks"))
                        Row1(0)("SubEnrichmentMarks2") = Convert.ToInt32(data1.Rows(0)("SubEnrichmentMarks"))
                        Row1(0)("TotalMarks2") = Convert.ToInt32(data1.Rows(0)("TotalMarks"))
                        Row1(0)("Annual_Marks2") = Convert.ToInt32(data1.Rows(0)("TotalMarks")) + Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data1.Rows(0)("NotebookMarks")) + Convert.ToInt32(data1.Rows(0)("SubEnrichmentMarks"))
                        Row1(0)("Grade2") = data1.Rows(0)("Grade")
                        Row1(0)("OveralllTotalMarks") = Convert.ToInt32(Row1(0)("Annual_Marks2")) + Convert.ToInt32(Row1(0)("Annual_Marks1"))
                        Row1(0)("OverallGrade") = Row1(0)("Grade2")
                    End If

                Next row

            End If

            ltrlMarks2.Text = ""
            If table.Rows.Count > 0 Then
                For Each row As DataRow In table.Rows
                    Dim PeriodicTestMarks2 As String = row("PeriodicTestMarks2").ToString
                    If PeriodicTestMarks2 = "0" Then
                        PeriodicTestMarks2 = "-"
                    End If
                    Dim NotebookMarks2 As String = row("NotebookMarks2").ToString
                    If NotebookMarks2 = "0" Then
                        NotebookMarks2 = "-"
                    End If
                    Dim SubEnrichmentMarks2 As String = row("SubEnrichmentMarks2").ToString
                    If SubEnrichmentMarks2 = "0" Then
                        SubEnrichmentMarks2 = "-"
                    End If
                    Dim TotalMarks2 As String = row("TotalMarks2").ToString
                    If TotalMarks2 = "0" Then
                        TotalMarks2 = "-"
                    End If
                    Dim Grade2 As String = row("Grade2").ToString
                    Dim Annual_Marks2 As String = row("Annual_Marks2").ToString
                    If Annual_Marks2 = "0" Then
                        Annual_Marks2 = "-"
                        Grade2 = "-"
                    End If
                    Dim PeriodicTestMarks1 As String = row("PeriodicTestMarks1").ToString
                    If PeriodicTestMarks1 = "0" Then
                        PeriodicTestMarks1 = "-"
                    End If
                    Dim NotebookMarks1 As String = row("NotebookMarks1").ToString
                    If NotebookMarks1 = "0" Then
                        NotebookMarks1 = "-"
                    End If
                    Dim SubEnrichmentMarks1 As String = row("SubEnrichmentMarks1").ToString
                    If SubEnrichmentMarks1 = "0" Then
                        SubEnrichmentMarks1 = "-"
                    End If
                    Dim Grade1 As String = row("Grade1").ToString
                    Dim Annual_Marks1 As String = row("Annual_Marks1").ToString
                    If Annual_Marks1 = "0" Then
                        Annual_Marks1 = "-"
                        Grade1 = "-"
                    End If
                    Dim TotalMarks1 As String = row("TotalMarks1").ToString
                    If TotalMarks1 = "0" Then
                        TotalMarks1 = "-"
                    End If
                    Dim OverallGrade As String = row("OverallGrade").ToString
                    Dim OveralllTotalMarks As String = row("OveralllTotalMarks").ToString
                    If OveralllTotalMarks = "0" Then
                        OveralllTotalMarks = "-"
                        OverallGrade = "-"
                    End If

                    ltrlMarks2.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>" & PeriodicTestMarks1 & "</td><td>" & NotebookMarks1 & "</td><td>" & SubEnrichmentMarks1 & "</td><td>" & TotalMarks1 & "</td><td>" & Annual_Marks1 & "</td><td>" & Grade1 & "</td><td>" & PeriodicTestMarks2 & "</td><td>" & NotebookMarks2 & "</td><td>" & SubEnrichmentMarks2 & "</td><td>" & TotalMarks2 & "</td><td>" & Annual_Marks2 & "</td><td>" & Grade2 & "</td></tr>"
                    'ltrlMarks2.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>" & PeriodicTestMarks1 & "</td><td>" & NotebookMarks1 & "</td><td>" & SubEnrichmentMarks1 & "</td><td>" & TotalMarks1 & "</td><td>" & Annual_Marks1 & "</td><td>" & Grade1 & "</td><td>" & PeriodicTestMarks2 & "</td><td>" & NotebookMarks2 & "</td><td>" & SubEnrichmentMarks2 & "</td><td>" & TotalMarks2 & "</td><td>" & Annual_Marks2 & "</td><td>" & Grade2 & "</td><td>" & OveralllTotalMarks & "</td><td>" & OverallGrade & "</td></tr>"
                Next row
            End If

        End If


    End Sub

    Private Sub RepeaterTerm12IX2_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles RepeaterTerm12IX2.ItemDataBound
        Dim lblPeriodicTest2IX2 As Label
        Dim lblNoteBook2IX2 As Label
        Dim lblSubEnrich2IX2 As Label
        Dim lblHalfExam2IX2 As Label
        Dim lblTotalMarks2IX2 As Label
        Dim ltrlMarks2IX2 As Literal
        Dim LitIXFITIX2 As Literal
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            lblPeriodicTest2IX2 = e.Item.FindControl("lblPeriodicTest2IX2")
            lblNoteBook2IX2 = e.Item.FindControl("lblNoteBook2IX2")
            lblSubEnrich2IX2 = e.Item.FindControl("lblSubEnrich2IX2")
            lblHalfExam2IX2 = e.Item.FindControl("lblHalfExam2IX2")
            lblTotalMarks2IX2 = e.Item.FindControl("lblTotalMarks2IX2")
            ltrlMarks2IX2 = e.Item.FindControl("ltrlMarks2IX2")
            LitIXFITIX2 = e.Item.FindControl("LitIXFITIX2")

            Dim dtParticular22 As DataTable = BLL.ExecDataTable("select top 1 * from tbl_Examparticulars where ExamId=@ExamId and ClassId=@ClassId", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
            If dtParticular22.Rows.Count > 0 Then
                lblPeriodicTest2IX2.Text = dtParticular22.Rows(0)("PeriodicTestMarksMax")
                lblNoteBook2IX2.Text = dtParticular22.Rows(0)("NotebookMarksMax")
                lblSubEnrich2IX2.Text = dtParticular22.Rows(0)("SubEnrichmentMarksMax")
                lblHalfExam2IX2.Text = dtParticular22.Rows(0)("MaxTheoryMarks")
                lblTotalMarks2IX2.Text = dtParticular22.Rows(0)("MaxTotalmarks")
            End If

            Dim table As New DataTable

            table.Columns.Add("SubjectCode", GetType(Integer))
            table.Columns.Add("SubjectName", GetType(String))
            table.Columns.Add("PeriodicTestMarks1", GetType(Integer))
            table.Columns.Add("NotebookMarks1", GetType(Integer))
            table.Columns.Add("SubEnrichmentMarks1", GetType(Integer))
            table.Columns.Add("TotalMarks1", GetType(Integer))
            table.Columns.Add("Annual_Marks1", GetType(Integer))
            table.Columns.Add("Grade1", GetType(String))
            table.Columns.Add("PeriodicTestMarks2", GetType(Integer))
            table.Columns.Add("NotebookMarks2", GetType(Integer))
            table.Columns.Add("SubEnrichmentMarks2", GetType(Integer))
            table.Columns.Add("TotalMarks2", GetType(Integer))
            table.Columns.Add("Annual_Marks2", GetType(Integer))
            table.Columns.Add("Grade2", GetType(String))
            table.Columns.Add("OveralllTotalMarks", GetType(String))
            table.Columns.Add("OverallGrade", GetType(String))

            table.Clear()
            Dim dt123 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
            'ltrlMarks2.Text = ""

            If dt123.Rows.Count > 0 Then
                For Each row As DataRow In dt123.Rows
                    Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), "9", e.Item.DataItem("StudentId"))
                    If data.Rows.Count > 0 Then
                        Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data.Rows(0)("NotebookMarks")) + Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks"))
                        table.Rows.Add(Convert.ToInt32(row("SubjectCode")), row("SubjectName").ToString(), Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")), Convert.ToInt32(data.Rows(0)("NotebookMarks")), Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks")), Convert.ToInt32(data.Rows(0)("TotalMarks")), Annual_Marks, data.Rows(0)("Grade").ToString(), 0, 0, 0, 0, 0, "", "", "")
                    Else
                    End If
                    Dim data1 As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, e.Item.DataItem("StudentId"))
                    If data1.Rows.Count > 0 And data.Rows.Count > 0 Then

                        Dim Row1() As Data.DataRow
                        Row1 = table.Select("SubjectCode = '" & row("SubjectCode") & "'")
                        Row1(0)("PeriodicTestMarks2") = Math.Round((Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(Row1(0)("PeriodicTestMarks1"))) / 2)
                        Row1(0)("NotebookMarks2") = Math.Round((Convert.ToInt32(data1.Rows(0)("NotebookMarks")) + Convert.ToInt32(Row1(0)("NotebookMarks1"))) / 2)
                        Row1(0)("SubEnrichmentMarks2") = Math.Round((Convert.ToInt32(data1.Rows(0)("SubEnrichmentMarks")) + Convert.ToInt32(Row1(0)("SubEnrichmentMarks1"))) / 2)
                        Row1(0)("TotalMarks2") = Math.Round((Convert.ToInt32(data1.Rows(0)("TotalMarks")) + Convert.ToInt32(Row1(0)("TotalMarks1"))) / 2)

                        Dim AvgMarks As Int32 = Convert.ToInt32(Row1(0)("TotalMarks2")) + Convert.ToInt32(Row1(0)("PeriodicTestMarks2")) + Convert.ToInt32(Row1(0)("NotebookMarks2")) + Convert.ToInt32(Row1(0)("SubEnrichmentMarks2"))
                        'Dim AvgMarks As Int32 = Convert.ToInt32(((Convert.ToInt32(data1.Rows(0)("TotalMarks")) + Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data1.Rows(0)("NotebookMarks")) + Convert.ToInt32(data1.Rows(0)("SubEnrichmentMarks")) + Convert.ToInt32(Row1(0)("Annual_Marks1"))) + 1) / 2)
                        Row1(0)("Annual_Marks2") = AvgMarks
                        Dim Grade As String = ""
                        If AvgMarks >= 91 Then
                            Grade = "A1"
                        ElseIf AvgMarks >= 81 And AvgMarks <= 90 Then
                            Grade = "A2"
                        ElseIf AvgMarks >= 71 And AvgMarks <= 80 Then
                            Grade = "B1"
                        ElseIf AvgMarks >= 61 And AvgMarks <= 70 Then
                            Grade = "B2"
                        ElseIf AvgMarks >= 51 And AvgMarks <= 60 Then
                            Grade = "C1"
                        ElseIf AvgMarks >= 41 And AvgMarks <= 50 Then
                            Grade = "C2"
                        ElseIf AvgMarks >= 33 And AvgMarks <= 40 Then
                            Grade = "D"
                        Else
                            Grade = "E"
                        End If
                        Row1(0)("Grade2") = Grade
                        'Row1(0)("OveralllTotalMarks") = Convert.ToInt32(Row1(0)("Annual_Marks2")) + Convert.ToInt32(Row1(0)("Annual_Marks1"))
                        'Row1(0)("OverallGrade") = Row1(0)("Grade2")
                    End If

                Next row

            End If

            ltrlMarks2IX2.Text = ""
            If table.Rows.Count > 0 Then
                Dim Annual_Marks2Sum As Int32 = 0
                For Each row As DataRow In table.Rows

                    Dim PeriodicTestMarks2 As String = row("PeriodicTestMarks2").ToString
                    If PeriodicTestMarks2 = "0" Then
                        PeriodicTestMarks2 = "-"
                    End If
                    Dim NotebookMarks2 As String = row("NotebookMarks2").ToString
                    If NotebookMarks2 = "0" Then
                        NotebookMarks2 = "-"
                    End If
                    Dim SubEnrichmentMarks2 As String = row("SubEnrichmentMarks2").ToString
                    If SubEnrichmentMarks2 = "0" Then
                        SubEnrichmentMarks2 = "-"
                    End If
                    Dim TotalMarks2 As String = row("TotalMarks2").ToString
                    If TotalMarks2 = "0" Then
                        TotalMarks2 = "-"
                    End If
                    Dim Grade2 As String = row("Grade2").ToString
                    Dim Annual_Marks2 As String = row("Annual_Marks2").ToString
                    If Annual_Marks2 = "0" Then
                        Annual_Marks2 = "-"
                        Grade2 = ""
                    End If

                    ltrlMarks2IX2.Text &= "<tr><td style='line-height:25px;'>" & row("SubjectName").ToString() & "</td><td>" & PeriodicTestMarks2 & "</td><td>" & NotebookMarks2 & "</td><td>" & SubEnrichmentMarks2 & "</td><td>" & TotalMarks2 & "</td><td>" & Annual_Marks2 & "</td><td>" & Grade2 & "</td></tr>"
                    Annual_Marks2Sum = Annual_Marks2Sum + Convert.ToInt32(row("Annual_Marks2"))
                Next row
                ltrlMarks2IX2.Text &= "<tr><td style='line-height:25px;'>Total</td><td colspan='4'></td><td>" & Annual_Marks2Sum.ToString() & "</td><td></td></tr>"
            End If


            If ddlClass.SelectedValue = 12 Then
                LitIXFITIX2.Text = ""
                Dim TotalMarksFit As String = ""
                Dim MaxTotal As String = ""
                Dim GradeFIT As String = ""
                Dim MxaPracticalMarksFit As String = ""
                Dim dtFIT As DataTable = BLL.ExecDataTableProc("Prc_GetGradeFIT", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", e.Item.DataItem("StudentId"))
                If dtFIT.Rows.Count > 0 Then
                    If dtFIT.Rows(0)("TotalMarks") = 0 Then
                        TotalMarksFit = ""
                    Else
                        TotalMarksFit = dtFIT.Rows(0)("TotalMarks").ToString()
                    End If
                    If dtFIT.Rows(0)("MxaPracticalMarks") = 0 Then
                        MxaPracticalMarksFit = ""
                    Else
                        MxaPracticalMarksFit = dtFIT.Rows(0)("MxaPracticalMarks").ToString()
                    End If
                    If dtFIT.Rows(0)("MaxTotal") = 0 Then
                        MaxTotal = ""
                    Else
                        MaxTotal = dtFIT.Rows(0)("MaxTotal").ToString()
                    End If
                    If dtFIT.Rows(0)("Grade") = "0" Then
                        GradeFIT = ""
                    Else
                        GradeFIT = dtFIT.Rows(0)("Grade").ToString()
                    End If
                    LitIXFITIX2.Text = "<tr><td style='line-height:25px;' class='tds'>" & dtFIT.Rows(0)("SubjectName").ToString() & "</td><td class='tds'>" & TotalMarksFit & "</td><td class='tds'>" & MxaPracticalMarksFit & "</td><td class='tds'>" & MaxTotal & "</td><td class='tds'>" & GradeFIT & "</td></tr>"
                Else
                    LitIXFITIX2.Text = ""

                End If
            End If
        End If

    End Sub

    Private Sub RepeaterEleTwe12_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles RepeaterEleTwe12.ItemDataBound
        Dim Literal4 As Literal
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Literal4 = e.Item.FindControl("Literal4")

            Dim table As New DataTable

            table.Columns.Add("SubjectCode", GetType(Integer))
            table.Columns.Add("SubjectName", GetType(String))
            table.Columns.Add("Periodic", GetType(Integer))
            table.Columns.Add("Theory1", GetType(Integer))
            table.Columns.Add("Practical1", GetType(Integer))
            table.Columns.Add("TotalMarks1", GetType(Integer))
            table.Columns.Add("Theory2", GetType(Integer))
            table.Columns.Add("Practical2", GetType(Integer))
            table.Columns.Add("TotalMarks2", GetType(Integer))
            table.Columns.Add("GrandTotal", GetType(Integer))
            table.Clear()

            Dim dt123 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
            Literal4.Text = ""
            'Literal6.Text = ""
            If dt123.Rows.Count > 0 Then
                For Each row As DataRow In dt123.Rows
                    Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), "9", e.Item.DataItem("StudentId"))
                    If data.Rows.Count > 0 Then
                        Dim TotalTerm1 As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("MxaPracticalMarks"))
                        table.Rows.Add(Convert.ToInt32(row("SubjectCode")), row("SubjectName").ToString(), 0, Convert.ToInt32(data.Rows(0)("TotalMarks")), Convert.ToInt32(data.Rows(0)("MxaPracticalMarks")), TotalTerm1, 0, 0, 0, TotalTerm1)
                    Else
                    End If

                    Dim data1 As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, e.Item.DataItem("StudentId"))
                    If data1.Rows.Count > 0 And data.Rows.Count > 0 Then

                        Dim Row1() As Data.DataRow
                        Row1 = table.Select("SubjectCode = '" & row("SubjectCode") & "'")
                        Row1(0)("Periodic") = Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks"))
                        Row1(0)("Theory2") = Convert.ToInt32(data1.Rows(0)("TotalMarks"))
                        Row1(0)("Practical2") = Convert.ToInt32(data1.Rows(0)("MxaPracticalMarks"))
                        Row1(0)("TotalMarks2") = (Row1(0)("Theory2") + Row1(0)("Practical2"))
                        Row1(0)("GrandTotal") = (Row1(0)("Periodic") + Row1(0)("TotalMarks1") + Row1(0)("TotalMarks2"))
                    Else

                    End If
                Next row

            Else

            End If

            If table.Rows.Count > 0 Then
                Dim GrandTotalSum As Int32 = 0
                For Each row As DataRow In table.Rows
                    Dim Practical1 As String = row("Practical1").ToString
                    If Practical1 = "0" Then
                        Practical1 = "-"
                    End If
                    Dim Practical2 As String = row("Practical2").ToString
                    If Practical2 = "0" Then
                        Practical2 = "-"
                    End If
                    Dim Periodic As String = row("Periodic").ToString
                    If Periodic = "0" Then
                        Periodic = "-"
                    End If
                    Dim Theory1 As String = row("Theory1").ToString
                    If Theory1 = "0" Then
                        Theory1 = "-"
                    End If
                    Dim TotalMarks1 As String = row("TotalMarks1").ToString
                    If TotalMarks1 = "0" Then
                        TotalMarks1 = "-"
                    End If
                    Dim Theory2 As String = row("Theory2").ToString
                    If Theory2 = "0" Then
                        Theory2 = "-"
                    End If
                    Dim TotalMarks2 As String = row("TotalMarks2").ToString
                    If TotalMarks2 = "0" Then
                        TotalMarks2 = "-"
                    End If
                    Dim GrandTotal As String = row("GrandTotal").ToString
                    If GrandTotal = "0" Then
                        GrandTotal = "-"
                    End If

                    Literal4.Text &= "<tr><td style='line-height:25px;'>" & row("SubjectName").ToString() & "</td><td>" & Periodic & "</td><td>" & Theory1 & "</td><td>" & Practical1 & "</td><td>" & TotalMarks1 & "</td><td>" & Theory2 & "</td><td>" & Practical2 & "</td><td>" & TotalMarks2 & "</td><td>" & GrandTotal & "</td></tr>"
                    GrandTotalSum = GrandTotalSum + Convert.ToInt32(row("GrandTotal"))
                Next row
                Literal4.Text &= "<tr><td style='line-height:25px;'>Grand Total</td><td colspan='7'></td><td>" & GrandTotalSum.ToString() & "</td></tr>"
            Else
                Literal4.Text &= "<tr><td style='line-height:25px;'></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
            End If
        End If




    End Sub
End Class
