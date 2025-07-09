Imports System.Web.Services
Imports System.Data.SqlClient
Partial Class Admin_ExamPrintNew
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

    <WebMethod()>
    Public Shared Function SaveAttendance(ByVal StudentId As String, ByVal ClassId As Integer, ByVal ExamId As Integer, ByVal Attendance As String) As String
        Dim ReturnValue As String = "!"
        Try
            ReturnValue = (New BusinessLogicLayer).ExecNonQueryProc("Prc_AddUpdateExamAttendance", "@StudentId", StudentId, "@ExamId", ExamId, "@ClassId", ClassId, "@Attendance", Attendance)
        Catch ex As Exception
            ReturnValue = "!"
        Finally

        End Try
        Return ReturnValue
    End Function
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            hdnStudentId.Value = ddlstudent.SelectedValue
            hdnExamId.Value = ddlexam.SelectedValue
            hdnClassId.Value = ddlClass.SelectedValue
            'Correct
            If ddlClass.SelectedValue = "3" Or ddlClass.SelectedValue = "29" Or ddlClass.SelectedValue = "1" Or ddlClass.SelectedValue = "32" Then
                tr1.Visible = True
            Else
                tr1.Visible = False
            End If
            DivIXFIT.Visible = False
            If (ddlClass.SelectedValue = "14" Or ddlClass.SelectedValue = "35" Or ddlClass.SelectedValue = "31" Or ddlClass.SelectedValue = "13" Or ddlClass.SelectedValue = "11" Or ddlClass.SelectedValue = "26" Or ddlClass.SelectedValue = "27" Or ddlClass.SelectedValue = "2" Or ddlClass.SelectedValue = "4" Or ddlClass.SelectedValue = "5" Or ddlClass.SelectedValue = "6" Or ddlClass.SelectedValue = "7" Or ddlClass.SelectedValue = "8" Or ddlClass.SelectedValue = "9" Or ddlClass.SelectedValue = "10" Or ddlClass.SelectedValue = "36") Then

                lblElTwTerm1Discipline.Text = "5 point (A-E)"
                lblElTwTerm1Scholastic.Text = "5 point (A-E)"
                'lblElTerm1Grade.Text = "Failed"

                lblterm1Scholastic.Text = "3 point (A-C)"
                lblterm1Discipline.Text = "3 point (A-C)"
                term1grade.Text = "Needs Improvement"
            Else

                lblElTwTerm1Discipline.Text = "5 point (A-E)"
                lblElTwTerm1Scholastic.Text = "5 point (A-E)"
                'lblElTerm1Grade.Text = "Failed"

                lblterm1Scholastic.Text = "5 point (A-E)"
                lblterm1Discipline.Text = "5 point (A-E)"

                'If (ddlClass.SelectedValue = "1" Or ddlClass.SelectedValue = "3") Then
                term1grade.Text = "Failed"
                'Else
                '    term1grade.Text = "Needs Improvement"
                'End If
            End If
            Dim termId As String
            Dim dtExamMaster As DataTable = BLL.ExecDataTable("select * from tbl_ExamMaster where ExamId=@ExamId", "@ExamId", ddlexam.SelectedValue)
            If dtExamMaster.Rows.Count > 0 Then
                termId = dtExamMaster.Rows(0)("TermId").ToString()
                lblSession1.InnerHtml = "Session " & dtExamMaster.Rows(0)("SessionYear").ToString()
                lblSession2.InnerHtml = "Session " & dtExamMaster.Rows(0)("SessionYear").ToString()
                lblSession3.InnerHtml = "Session " & dtExamMaster.Rows(0)("SessionYear").ToString()
                lblSession4.InnerHtml = "Session " & dtExamMaster.Rows(0)("SessionYear").ToString()
                lblSession5.InnerHtml = "Session " & dtExamMaster.Rows(0)("SessionYear").ToString()
            End If

            'Correct
            If ddlClass.SelectedValue = "1" And termId = "1" Then
                tblTerm1.Visible = True
                tblTerm12.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblTerm1"
                txtAttendancetblTerm1.Text = BLL.ExecScalar("Select Attendance from tblExamAttendance Where ExamId=@ExamId and StudentId=@StudentId and ClassId=@ClassId", "@StudentId", hdnStudentId.Value, "@ExamId", hdnExamId.Value, "@ClassId", hdnClassId.Value)

                Dim dtParticular1 As DataTable = BLL.ExecDataTable("select top 1 * from tbl_Examparticulars where ExamId=@ExamId and ClassId=@ClassId", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                If dtParticular1.Rows.Count > 0 Then
                    lblPer.Text = dtParticular1.Rows(0)("PeriodicTestMarksMax")
                    lblNote.Text = dtParticular1.Rows(0)("NotebookMarksMax")
                    lblSubEnrich.Text = dtParticular1.Rows(0)("SubEnrichmentMarksMax")
                    lblAnnualExamination.Text = dtParticular1.Rows(0)("MaxTheoryMarks")
                    lblTotalMarksObtained.Text = dtParticular1.Rows(0)("MaxTotalmarks")
                End If
                Dim dtGetStudent1 As DataTable = BLL.ExecDataTable("select Top 1 * from tbl_ExamResult where studentId=@StudentId and ClassId=@ClassId and ExamId=@ExamId", "@StudentId", ddlstudent.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@ExamId", ddlexam.SelectedValue)
                If dtGetStudent1.Rows.Count > 0 Then

                    lblStudentName.Text = dtGetStudent1.Rows(0)("StudentName")
                    lblRollN.Text = dtGetStudent1.Rows(0)("RollNo")
                    If dtGetStudent1.Rows(0)("Section") <> "" Then
                        lblSec.Text = ddlClass.SelectedItem.Text
                    Else
                        lblSec.Text = ddlClass.SelectedItem.Text
                    End If
                    lblDOBs.Text = dtGetStudent1.Rows(0)("DOB")
                    lblAdmNoTerm1.Text = dtGetStudent1.Rows(0)("StudentId")
                    lblterm1FName.Text = dtGetStudent1.Rows(0)("FatherName")
                    lblterm1MName.Text = dtGetStudent1.Rows(0)("MotherName")
                End If

                Dim dt1 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)

                ltrlMark.Text = ""
                ltrlScholastic.Text = ""
                Dim TotalMarks As String = ""
                Dim PeriodicTestMarks As String = ""
                Dim NotebookMarks As String = ""
                Dim SubEnrichmentMarks As String = ""
                Dim Grade As String = ""
                Dim AnnualMarks As String = ""

                If dt1.Rows.Count > 0 Then
                    ltrlMark.Text = ""
                    For Each row As DataRow In dt1.Rows
                        Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, ddlstudent.SelectedValue)
                        If data.Rows.Count > 0 Then

                            If data.Rows(0)("TotalMarks") = 0 Then
                                TotalMarks = "-"
                            Else
                                TotalMarks = data.Rows(0)("TotalMarks").ToString()
                            End If
                            If data.Rows(0)("PeriodicTestMarks") = 0 Then
                                PeriodicTestMarks = "-"
                            Else
                                PeriodicTestMarks = data.Rows(0)("PeriodicTestMarks").ToString()
                            End If
                            If data.Rows(0)("NotebookMarks") = 0 Then
                                NotebookMarks = "-"
                            Else
                                NotebookMarks = data.Rows(0)("NotebookMarks").ToString()
                            End If
                            If data.Rows(0)("SubEnrichmentMarks") = 0 Then
                                SubEnrichmentMarks = "-"
                            Else
                                SubEnrichmentMarks = data.Rows(0)("SubEnrichmentMarks").ToString()
                            End If
                            If data.Rows(0)("Grade") = "0" Then
                                Grade = "-"
                            Else
                                Grade = data.Rows(0)("Grade").ToString()
                            End If

                            Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data.Rows(0)("NotebookMarks")) + Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks"))

                            If Annual_Marks = 0 Then
                                AnnualMarks = "-"
                            Else
                                AnnualMarks = Annual_Marks
                            End If
                            If Convert.ToInt32(data.Rows(0)("Attandance")) = 1 Then
                                ltrlMark.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                            Else
                                ltrlMark.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>" & PeriodicTestMarks & "</td><td>" & NotebookMarks & "</td><td>" & SubEnrichmentMarks & "</td><td>" & TotalMarks & "</td><td>" & AnnualMarks & "</td><td>" & Grade & "</td></tr>"
                            End If
                        Else
                            ltrlMark.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                        End If
                    Next row
                Else
                    ltrlMark.Text &= "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                End If
                Dim gradeScho As String = ""
                ltrlScholastic.Text = ""
                Dim dtGrade1 As DataTable = BLL.ExecDataTableProc("Prc_GetGrade", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtGrade1.Rows.Count > 0 Then
                    For Each row As DataRow In dtGrade1.Rows
                        If row("Grade") = "0" Then
                            gradeScho = "-"
                        Else
                            gradeScho = row("Grade").ToString()
                        End If
                        ltrlScholastic.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & gradeScho & "</td></tr>"
                    Next row
                Else
                    ltrlScholastic.Text &= "<tr><td ></td><td class='tds'></td></tr>"
                End If
                ltrlDiscipline.Text = ""
                Dim GradeDisp As String = ""
                Dim dtDecipline As DataTable = BLL.ExecDataTableProc("Prc_GetGradeDecipline", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtDecipline.Rows.Count > 0 Then
                    If dtDecipline.Rows(0)("Grade") = "0" Then
                        GradeDisp = "-"
                    Else
                        GradeDisp = dtDecipline.Rows(0)("Grade").ToString()
                    End If
                    ltrlDiscipline.Text = GradeDisp
                Else
                    ltrlDiscipline.Text = "-"

                End If
                ltrlFIT.Text = ""
                Dim TotalMarksFit As String = ""
                Dim MaxTotal As String = ""
                Dim GradeFIT As String = ""
                Dim MxaPracticalMarksFit As String = ""
                Dim dtFIT As DataTable = BLL.ExecDataTableProc("Prc_GetGradeFIT", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtFIT.Rows.Count > 0 Then

                    If dtFIT.Rows(0)("SubjectName").ToString() = "FIT" Then
                        lblTh.InnerHtml = "40"
                        lblPrc.InnerHtml = "60"
                    Else
                        lblTh.InnerHtml = "30"
                        lblPrc.InnerHtml = "70"
                    End If
                    If dtFIT.Rows(0)("TotalMarks") = 0 Then
                        TotalMarksFit = "-"
                    Else
                        TotalMarksFit = dtFIT.Rows(0)("TotalMarks").ToString()
                    End If
                    If dtFIT.Rows(0)("MxaPracticalMarks") = 0 Then
                        MxaPracticalMarksFit = "-"
                    Else
                        MxaPracticalMarksFit = dtFIT.Rows(0)("MxaPracticalMarks").ToString()
                    End If
                    If dtFIT.Rows(0)("MaxTotal") = 0 Then
                        MaxTotal = "-"
                    Else
                        MaxTotal = dtFIT.Rows(0)("MaxTotal").ToString()
                    End If
                    If dtFIT.Rows(0)("Grade") = "0" Then
                        GradeFIT = "-"
                    Else
                        GradeFIT = dtFIT.Rows(0)("Grade").ToString()
                    End If

                    ltrlFIT.Text = "<tr><td  class='tds'>" & dtFIT.Rows(0)("SubjectName").ToString() & "</td><td class='tds'>" & TotalMarksFit & "</td><td class='tds'>" & MxaPracticalMarksFit & "</td><td class='tds'>" & MaxTotal & "</td><td class='tds'>" & GradeFIT & "</td></tr>"
                Else
                    ltrlFIT.Text = "-"
                    lblTh.InnerHtml = "0"
                    lblPrc.InnerHtml = "0"
                End If
                'Correct
            ElseIf termId = "1" And (ddlClass.SelectedValue = "15" Or ddlClass.SelectedValue = "17" Or ddlClass.SelectedValue = "16" Or ddlClass.SelectedValue = "18" Or ddlClass.SelectedValue = "20" Or ddlClass.SelectedValue = "21" Or ddlClass.SelectedValue = "22" Or ddlClass.SelectedValue = "23" Or ddlClass.SelectedValue = "19" Or ddlClass.SelectedValue = "37") Then

                tblEleTwe1.Visible = True
                tblTerm1.Visible = False
                tblTerm12.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblEleTwe1"
                txtAttendancetblEleTwe1.Text = BLL.ExecScalar("Select Attendance from tblExamAttendance Where ExamId=@ExamId and StudentId=@StudentId and ClassId=@ClassId", "@StudentId", hdnStudentId.Value, "@ExamId", hdnExamId.Value, "@ClassId", hdnClassId.Value)

                Dim dtParticular11 As DataTable = BLL.ExecDataTable("select top 1 * from tbl_Examparticulars where ExamId=@ExamId and ClassId=@ClassId", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                If dtParticular11.Rows.Count > 0 Then
                    lblPer.Text = dtParticular11.Rows(0)("PeriodicTestMarksMax")
                    lblNote.Text = dtParticular11.Rows(0)("NotebookMarksMax")
                    lblSubEnrich.Text = dtParticular11.Rows(0)("SubEnrichmentMarksMax")
                    lblAnnualExamination.Text = dtParticular11.Rows(0)("MaxTheoryMarks")
                    lblTotalMarksObtained.Text = dtParticular11.Rows(0)("MaxTotalmarks")
                End If
                Dim dtGetStudent11 As DataTable = BLL.ExecDataTable("select Top 1 * from tbl_ExamResult where studentId=@StudentId and ClassId=@ClassId and ExamId=@ExamId", "@StudentId", ddlstudent.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@ExamId", ddlexam.SelectedValue)
                If dtGetStudent11.Rows.Count > 0 Then

                    lblelSName.Text = dtGetStudent11.Rows(0)("StudentName")
                    lblelRN.Text = dtGetStudent11.Rows(0)("RollNo")
                    If dtGetStudent11.Rows(0)("Section") <> "" Then
                        lblelClass.Text = ddlClass.SelectedItem.Text
                    Else
                        lblelClass.Text = ddlClass.SelectedItem.Text
                    End If
                    lblElDOBS.Text = dtGetStudent11.Rows(0)("DOB")
                    'lblElGuar.Text = dtGetStudent11.Rows(0)("FatherName")
                    lblAdmNoTerm2.Text = dtGetStudent11.Rows(0)("StudentId")
                    lblterm2FName.Text = dtGetStudent11.Rows(0)("FatherName")
                    lblterm2MName.Text = dtGetStudent11.Rows(0)("MotherName")

                End If

                Dim dt11 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
                lblElMarks.Text = ""
                lblElScholastic.Text = ""
                Dim TotalMarks As String = ""
                Dim MxaPracticalMarks As String = ""
                Dim AnnualMarks As String = ""
                If dt11.Rows.Count > 0 Then
                    For Each row As DataRow In dt11.Rows
                        Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, ddlstudent.SelectedValue)
                        If data.Rows.Count > 0 Then
                            If data.Rows(0)("TotalMarks") = 0 Then
                                TotalMarks = "-"
                            Else
                                TotalMarks = data.Rows(0)("TotalMarks").ToString()
                            End If
                            If data.Rows(0)("MxaPracticalMarks") = 0 Then
                                MxaPracticalMarks = "-"
                            Else
                                MxaPracticalMarks = data.Rows(0)("MxaPracticalMarks").ToString()
                            End If
                            Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("MxaPracticalMarks"))
                            If Annual_Marks = 0 Then
                                AnnualMarks = "-"
                            Else
                                AnnualMarks = Annual_Marks

                            End If
                            If Convert.ToInt32(data.Rows(0)("Attandance")) = 1 Then
                                lblElMarks.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>-</td><td>-</td><td>-</td></tr>"
                            Else
                                lblElMarks.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>" & TotalMarks & "</td><td>" & MxaPracticalMarks & "</td><td>" & AnnualMarks & "</td></tr>"
                            End If
                        Else
                            lblElMarks.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>-</td><td>-</td><td>-</td></tr>"
                        End If
                    Next row
                Else
                    lblElMarks.Text &= "<tr><td></td><td></td><td></td><td></td></tr>"
                End If
                Dim gradeScho1 As String = ""
                Dim dtGrade11 As DataTable = BLL.ExecDataTableProc("Prc_GetGrade", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtGrade11.Rows.Count > 0 Then
                    For Each row As DataRow In dtGrade11.Rows
                        If row("Grade") = "0" Then
                            gradeScho1 = "-"
                        Else
                            gradeScho1 = row("Grade").ToString()
                        End If
                        lblElScholastic.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & gradeScho1 & "</td></tr>"
                    Next row
                Else
                    lblElScholastic.Text &= "<tr><td ></td><td class='tds'></td></tr>"
                End If
                ltrlEltwDiscipline.Text = ""
                Dim gradedis As String = ""
                Dim dtDecipline As DataTable = BLL.ExecDataTableProc("Prc_GetGradeDecipline", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtDecipline.Rows.Count > 0 Then
                    If dtDecipline.Rows(0)("Grade") = "0" Then
                        gradedis = "-"
                    Else
                        gradedis = dtDecipline.Rows(0)("Grade").ToString()
                    End If
                    ltrlEltwDiscipline.Text = gradedis

                Else
                    ltrlEltwDiscipline.Text = "-"

                End If
                'Correct
            ElseIf (ddlClass.SelectedValue = "15" Or ddlClass.SelectedValue = "17" Or ddlClass.SelectedValue = "16" Or ddlClass.SelectedValue = "18") And termId = "2" Then

                tblTerm12.Visible = False
                tblTerm1.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = True
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblEleTwe12"
                txtAttendancetblEleTwe12.Text = BLL.ExecScalar("Select Attendance from tblExamAttendance Where ExamId=@ExamId and StudentId=@StudentId and ClassId=@ClassId", "@StudentId", hdnStudentId.Value, "@ExamId", hdnExamId.Value, "@ClassId", hdnClassId.Value)

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
                Dim dtGetStudent111 As DataTable = BLL.ExecDataTable("select Top 1 * from tbl_ExamResult where studentId=@StudentId and ClassId=@ClassId and ExamId=@ExamId", "@StudentId", ddlstudent.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@ExamId", ddlexam.SelectedValue)
                If dtGetStudent111.Rows.Count > 0 Then
                    lblStudentNameEleTwe12.Text = dtGetStudent111.Rows(0)("StudentName")
                    lblRollNoEleTwe12.Text = dtGetStudent111.Rows(0)("RollNo")
                    If dtGetStudent111.Rows(0)("Section") <> "" Then
                        lblSecEleTwe12.Text = ddlClass.SelectedItem.Text
                    Else
                        lblSecEleTwe12.Text = ddlClass.SelectedItem.Text
                    End If
                    lblDOBEleTwe12.Text = dtGetStudent111.Rows(0)("DOB")
                    lblAdmNoEleTwe12.Text = dtGetStudent111.Rows(0)("StudentId")
                    lblFNameEleTwe12.Text = dtGetStudent111.Rows(0)("FatherName")
                    lblMNameEleTwe12.Text = dtGetStudent111.Rows(0)("MotherName")
                End If

                Dim dt123 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
                Literal4.Text = ""
                'Literal6.Text = ""
                Literal7.Text = ""
                If dt123.Rows.Count > 0 Then
                    For Each row As DataRow In dt123.Rows
                        Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), "9", ddlstudent.SelectedValue)
                        If data.Rows.Count > 0 Then
                            Dim TotalTerm1 As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("MxaPracticalMarks"))
                            table.Rows.Add(Convert.ToInt32(row("SubjectCode")), row("SubjectName").ToString(), 0, Convert.ToInt32(data.Rows(0)("TotalMarks")), Convert.ToInt32(data.Rows(0)("MxaPracticalMarks")), TotalTerm1, 0, 0, 0, TotalTerm1)
                        Else
                        End If

                        Dim data1 As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, ddlstudent.SelectedValue)
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

                Dim dtGrade123 As DataTable = BLL.ExecDataTableProc("Prc_GetGrade", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtGrade123.Rows.Count > 0 Then
                    For Each row As DataRow In dtGrade123.Rows
                        Literal7.Text &= "<tr><td style='line-height:25px;'>" & row("SubjectName").ToString() & "</td><td class='tds'>" & row("Grade").ToString() & "</td></tr>"
                    Next row
                Else
                    Literal7.Text &= "<tr><td style='line-height:25px;'></td><td class='tds'></td></tr>"
                End If

                LitDisciplineEleTwe12.Text = ""
                Dim gradedis As String = ""
                Dim dtDecipline As DataTable = BLL.ExecDataTableProc("Prc_GetGradeDecipline", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtDecipline.Rows.Count > 0 Then
                    If dtDecipline.Rows(0)("Grade") = "0" Then
                        gradedis = ""
                    Else
                        gradedis = dtDecipline.Rows(0)("Grade").ToString()
                    End If
                    LitDisciplineEleTwe12.Text = gradedis

                Else
                    LitDisciplineEleTwe12.Text = ""

                End If


                'Dim dtGrade1234 As DataTable = BLL.ExecDataTableProc("Prc_GetGrade", "@ExamId", "9", "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                'If dtGrade1234.Rows.Count > 0 Then
                '    For Each row As DataRow In dtGrade1234.Rows
                '        Literal6.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & row("Grade").ToString() & "</td></tr>"

                '    Next row
                'Else
                '    Literal6.Text &= "<tr><td ></td><td class='tds'></td></tr>"

                'End If

                'Correct
            ElseIf termId = "2" And (ddlClass.SelectedValue = "1" Or ddlClass.SelectedValue = "20" Or ddlClass.SelectedValue = "21" Or ddlClass.SelectedValue = "22" Or ddlClass.SelectedValue = "23" Or ddlClass.SelectedValue = "19") Then
                tblTerm12.Visible = False
                tblTerm1.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                'Correct
            ElseIf termId = "2" And ddlClass.SelectedValue = 3 Then
                tblTerm1.Visible = False
                tblTerm12.Visible = False
                tblTerm12IX2.Visible = True
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False

                hdnSection.Value = "tblTerm12IX2"
                txtAttendancetblTerm12IX2.Text = BLL.ExecScalar("Select Attendance from tblExamAttendance Where ExamId=@ExamId and StudentId=@StudentId and ClassId=@ClassId", "@StudentId", hdnStudentId.Value, "@ExamId", hdnExamId.Value, "@ClassId", hdnClassId.Value)

                Dim dtParticular22 As DataTable = BLL.ExecDataTable("select top 1 * from tbl_Examparticulars where ExamId=@ExamId and ClassId=@ClassId", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                If dtParticular22.Rows.Count > 0 Then
                    lblPeriodicTest2IX2.Text = dtParticular22.Rows(0)("PeriodicTestMarksMax")
                    lblNoteBook2IX2.Text = dtParticular22.Rows(0)("NotebookMarksMax")
                    lblSubEnrich2IX2.Text = dtParticular22.Rows(0)("SubEnrichmentMarksMax")
                    lblHalfExam2IX2.Text = dtParticular22.Rows(0)("MaxTheoryMarks")
                    lblTotalMarks2IX2.Text = dtParticular22.Rows(0)("MaxTotalmarks")
                End If
                Dim dtGetStudent111 As DataTable = BLL.ExecDataTable("select Top 1 * from tbl_ExamResult where studentId=@StudentId and ClassId=@ClassId and ExamId=@ExamId", "@StudentId", ddlstudent.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@ExamId", ddlexam.SelectedValue)
                If dtGetStudent111.Rows.Count > 0 Then
                    lblTeaherremarksIX2.Text = dtGetStudent111.Rows(0)("StudentName")
                    lblRollNoIX2.Text = dtGetStudent111.Rows(0)("RollNo")
                    If dtGetStudent111.Rows(0)("Section") <> "" Then
                        lblSectionIX2.Text = ddlClass.SelectedItem.Text
                    Else
                        lblSectionIX2.Text = ddlClass.SelectedItem.Text
                    End If
                    lblDOBIX2.Text = dtGetStudent111.Rows(0)("DOB")
                    lblFNameIX2.Text = dtGetStudent111.Rows(0)("FatherName")
                    lblMNameIX2.Text = dtGetStudent111.Rows(0)("MotherName")
                    lblAdmNoIX2.Text = dtGetStudent111.Rows(0)("StudentId")
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
                ltrlTerm1.Text = ""
                ltrlTerm2.Text = ""

                If dt123.Rows.Count > 0 Then
                    For Each row As DataRow In dt123.Rows
                        Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), "9", ddlstudent.SelectedValue)
                        If data.Rows.Count > 0 Then
                            Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data.Rows(0)("NotebookMarks")) + Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks"))
                            table.Rows.Add(Convert.ToInt32(row("SubjectCode")), row("SubjectName").ToString(), Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")), Convert.ToInt32(data.Rows(0)("NotebookMarks")), Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks")), Convert.ToInt32(data.Rows(0)("TotalMarks")), Annual_Marks, data.Rows(0)("Grade").ToString(), 0, 0, 0, 0, 0, "", "", "")
                        Else
                        End If
                        Dim data1 As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, ddlstudent.SelectedValue)
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

                ltrlTerm2IX2.Text = ""
                Dim dtGrade123 As DataTable = BLL.ExecDataTableProc("Prc_GetGrade", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtGrade123.Rows.Count > 0 Then
                    For Each row As DataRow In dtGrade123.Rows
                        'ltrlTerm1.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & row("Grade").ToString() & "</td></tr>"
                        ltrlTerm2IX2.Text &= "<tr><td style='line-height:25px;'>" & row("SubjectName").ToString() & "</td><td class='tds'>" & row("Grade").ToString() & "</td></tr>"
                    Next row
                Else
                    'ltrlTerm1.Text &= "<tr><td >0</td><td class='tds'>0</td></tr>"
                    ltrlTerm2IX2.Text &= "<tr><td style='line-height:25px;'></td><td class='tds'></td></tr>"
                End If

                'Correct
                If ddlClass.SelectedValue = 3 Then
                    LitIXFITIX2.Text = ""
                    Dim TotalMarksFit As String = ""
                    Dim MaxTotal As String = ""
                    Dim GradeFIT As String = ""
                    Dim MxaPracticalMarksFit As String = ""
                    Dim dtFIT As DataTable = BLL.ExecDataTableProc("Prc_GetGradeFIT", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                    If dtFIT.Rows.Count > 0 Then
                        If dtFIT.Rows(0)("TotalMarks") = 0 Then
                            TotalMarksFit = "-"
                        Else
                            TotalMarksFit = dtFIT.Rows(0)("TotalMarks").ToString()
                        End If
                        If dtFIT.Rows(0)("MxaPracticalMarks") = 0 Then
                            MxaPracticalMarksFit = "-"
                        Else
                            MxaPracticalMarksFit = dtFIT.Rows(0)("MxaPracticalMarks").ToString()
                        End If
                        If dtFIT.Rows(0)("MaxTotal") = 0 Then
                            MaxTotal = "-"
                        Else
                            MaxTotal = dtFIT.Rows(0)("MaxTotal").ToString()
                        End If
                        If dtFIT.Rows(0)("Grade") = "0" Then
                            GradeFIT = "-"
                        Else
                            GradeFIT = dtFIT.Rows(0)("Grade").ToString()
                        End If
                        DivIXFITIX2.Visible = True
                        LitIXFITIX2.Text = "<tr><td style='line-height:25px;' class='tds'>" & dtFIT.Rows(0)("SubjectName").ToString() & "</td><td class='tds'>" & TotalMarksFit & "</td><td class='tds'>" & MxaPracticalMarksFit & "</td><td class='tds'>" & MaxTotal & "</td><td class='tds'>" & GradeFIT & "</td></tr>"
                    Else
                        DivIXFITIX2.Visible = False
                        LitIXFITIX2.Text = ""

                    End If
                End If
                LitDisciplineTerm12IX.Text = ""
                Dim gradedis As String = ""
                Dim dtDecipline As DataTable = BLL.ExecDataTableProc("Prc_GetGradeDecipline", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtDecipline.Rows.Count > 0 Then
                    If dtDecipline.Rows(0)("Grade") = "0" Then
                        gradedis = "-"
                    Else
                        gradedis = dtDecipline.Rows(0)("Grade").ToString()
                    End If
                    LitDisciplineTerm12IX.Text = gradedis

                Else
                    LitDisciplineTerm12IX.Text = "-"

                End If

            ElseIf termId = "2" Then
                tblTerm12.Visible = True
                tblTerm1.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblTerm12"
                txtAttendancetblTerm12.Text = BLL.ExecScalar("Select Attendance from tblExamAttendance Where ExamId=@ExamId and StudentId=@StudentId and ClassId=@ClassId", "@StudentId", hdnStudentId.Value, "@ExamId", hdnExamId.Value, "@ClassId", hdnClassId.Value)

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
                Dim dtGetStudent111 As DataTable = BLL.ExecDataTable("select Top 1 * from tbl_ExamResult where studentId=@StudentId and ClassId=@ClassId and ExamId=@ExamId", "@StudentId", ddlstudent.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@ExamId", ddlexam.SelectedValue)
                If dtGetStudent111.Rows.Count > 0 Then
                    lblTeaherremarks.Text = dtGetStudent111.Rows(0)("StudentName")
                    lblRollNo.Text = dtGetStudent111.Rows(0)("RollNo")
                    If dtGetStudent111.Rows(0)("Section") <> "" Then
                        lblSection.Text = ddlClass.SelectedItem.Text
                    Else
                        lblSection.Text = ddlClass.SelectedItem.Text
                    End If
                    lblDOB.Text = dtGetStudent111.Rows(0)("DOB")
                    lblFName.Text = dtGetStudent111.Rows(0)("FatherName")
                    lblMName.Text = dtGetStudent111.Rows(0)("MotherName")
                    lblAdmNo.Text = dtGetStudent111.Rows(0)("StudentId")
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
                ltrlTerm1.Text = ""
                ltrlTerm2.Text = ""

                If dt123.Rows.Count > 0 Then
                    For Each row As DataRow In dt123.Rows
                        Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), "9", ddlstudent.SelectedValue)
                        If data.Rows.Count > 0 Then
                            Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data.Rows(0)("NotebookMarks")) + Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks"))
                            table.Rows.Add(Convert.ToInt32(row("SubjectCode")), row("SubjectName").ToString(), Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")), Convert.ToInt32(data.Rows(0)("NotebookMarks")), Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks")), Convert.ToInt32(data.Rows(0)("TotalMarks")), Annual_Marks, data.Rows(0)("Grade").ToString(), 0, 0, 0, 0, 0, "", "", "")
                        Else
                        End If
                        Dim data1 As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, ddlstudent.SelectedValue)
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
                ltrlTerm2.Text = ""
                Dim dtGrade123 As DataTable = BLL.ExecDataTableProc("Prc_GetGrade", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtGrade123.Rows.Count > 0 Then
                    For Each row As DataRow In dtGrade123.Rows
                        'ltrlTerm1.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & row("Grade").ToString() & "</td></tr>"
                        ltrlTerm2.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & row("Grade").ToString() & "</td></tr>"
                    Next row
                Else
                    'ltrlTerm1.Text &= "<tr><td >0</td><td class='tds'>0</td></tr>"
                    ltrlTerm2.Text &= "<tr><td ></td><td class='tds'></td></tr>"
                End If

                ltrlTerm1.Text = ""
                Dim dtGrade1234 As DataTable = BLL.ExecDataTableProc("Prc_GetGrade", "@ExamId", "9", "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtGrade1234.Rows.Count > 0 Then
                    For Each row As DataRow In dtGrade1234.Rows
                        ltrlTerm1.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & row("Grade").ToString() & "</td></tr>"
                        'ltrlTerm2.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & row("Grade").ToString() & "</td></tr>"
                    Next row
                Else
                    ltrlTerm1.Text &= "<tr><td ></td><td class='tds'></td></tr>"
                    'ltrlTerm2.Text &= "<tr><td >0</td><td class='tds'>0</td></tr>"
                End If

                LitDisciplineTerm121.Text = ""
                Dim gradedis As String = ""
                Dim dtDecipline As DataTable = BLL.ExecDataTableProc("Prc_GetGradeDecipline", "@ExamId", "9", "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtDecipline.Rows.Count > 0 Then
                    If dtDecipline.Rows(0)("Grade") = "0" Then
                        gradedis = "-"
                    Else
                        gradedis = dtDecipline.Rows(0)("Grade").ToString()
                    End If
                    LitDisciplineTerm121.Text = gradedis

                Else
                    LitDisciplineTerm121.Text = "-"

                End If

                LitGKTerm12.Text = ""
                Dim dtGK As DataTable = BLL.ExecDataTableProc("Prc_GetGradeGK", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtGK.Rows.Count > 0 Then
                    If dtGK.Rows(0)("Grade") = "0" Then
                        gradedis = "-"
                    Else
                        gradedis = dtGK.Rows(0)("Grade").ToString()
                    End If
                    LitGKTerm12.Text = gradedis

                Else
                    LitGKTerm12.Text = "-"

                End If

                LitDisciplineTerm122.Text = ""
                Dim gradedis2 As String = ""
                Dim dtDecipline2 As DataTable = BLL.ExecDataTableProc("Prc_GetGradeDecipline", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtDecipline2.Rows.Count > 0 Then
                    If dtDecipline.Rows(0)("Grade") = "0" Then
                        gradedis2 = "-"
                    Else
                        gradedis2 = dtDecipline2.Rows(0)("Grade").ToString()
                    End If
                    LitDisciplineTerm122.Text = gradedis2

                Else
                    LitDisciplineTerm122.Text = "-"

                End If
                'Correct
                If ddlClass.SelectedValue = 3 Then
                    LitIXFIT.Text = ""
                    Dim TotalMarksFit As String = ""
                    Dim MaxTotal As String = ""
                    Dim GradeFIT As String = ""
                    Dim MxaPracticalMarksFit As String = ""
                    Dim dtFIT As DataTable = BLL.ExecDataTableProc("Prc_GetGradeFIT", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                    If dtFIT.Rows.Count > 0 Then
                        If dtFIT.Rows(0)("TotalMarks") = 0 Then
                            TotalMarksFit = "-"
                        Else
                            TotalMarksFit = dtFIT.Rows(0)("TotalMarks").ToString()
                        End If
                        If dtFIT.Rows(0)("MxaPracticalMarks") = 0 Then
                            MxaPracticalMarksFit = "-"
                        Else
                            MxaPracticalMarksFit = dtFIT.Rows(0)("MxaPracticalMarks").ToString()
                        End If
                        If dtFIT.Rows(0)("MaxTotal") = 0 Then
                            MaxTotal = "-"
                        Else
                            MaxTotal = dtFIT.Rows(0)("MaxTotal").ToString()
                        End If
                        If dtFIT.Rows(0)("Grade") = "0" Then
                            GradeFIT = "-"
                        Else
                            GradeFIT = dtFIT.Rows(0)("Grade").ToString()
                        End If
                        DivIXFIT.Visible = True
                        LitIXFIT.Text = "<tr><td class='tds' >" & dtFIT.Rows(0)("SubjectName").ToString() & "</td><td class='tds'>" & TotalMarksFit & "</td><td class='tds'>" & MxaPracticalMarksFit & "</td><td class='tds'>" & MaxTotal & "</td><td class='tds'>" & GradeFIT & "</td></tr>"
                    Else
                        DivIXFIT.Visible = False
                        LitIXFIT.Text = "-"

                    End If
                End If
            ElseIf termId = "1" Then

                tblTerm1.Visible = True
                tblTerm12.Visible = False
                tblEleTwe1.Visible = False
                tblEleTwe12.Visible = False
                tblTerm12IX2.Visible = False
                hdnSection.Value = "tblTerm1"
                txtAttendancetblTerm1.Text = BLL.ExecScalar("Select Attendance from tblExamAttendance Where ExamId=@ExamId and StudentId=@StudentId and ClassId=@ClassId", "@StudentId", hdnStudentId.Value, "@ExamId", hdnExamId.Value, "@ClassId", hdnClassId.Value)

                Dim dtParticular1 As DataTable = BLL.ExecDataTable("select top 1 * from tbl_Examparticulars where ExamId=@ExamId and ClassId=@ClassId", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                If dtParticular1.Rows.Count > 0 Then
                    lblPer.Text = dtParticular1.Rows(0)("PeriodicTestMarksMax")
                    lblNote.Text = dtParticular1.Rows(0)("NotebookMarksMax")
                    lblSubEnrich.Text = dtParticular1.Rows(0)("SubEnrichmentMarksMax")
                    lblAnnualExamination.Text = dtParticular1.Rows(0)("MaxTheoryMarks")
                    lblTotalMarksObtained.Text = dtParticular1.Rows(0)("MaxTotalmarks")
                End If
                Dim dtGetStudent1 As DataTable = BLL.ExecDataTable("select Top 1 * from tbl_ExamResult where studentId=@StudentId and ClassId=@ClassId and ExamId=@ExamId", "@StudentId", ddlstudent.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@ExamId", ddlexam.SelectedValue)
                If dtGetStudent1.Rows.Count > 0 Then

                    lblStudentName.Text = dtGetStudent1.Rows(0)("StudentName")
                    lblRollN.Text = dtGetStudent1.Rows(0)("RollNo")
                    If dtGetStudent1.Rows(0)("Section") <> "" Then
                        lblSec.Text = ddlClass.SelectedItem.Text
                    Else
                        lblSec.Text = ddlClass.SelectedItem.Text
                    End If
                    lblDOBs.Text = dtGetStudent1.Rows(0)("DOB")
                    lblAdmNoTerm1.Text = dtGetStudent1.Rows(0)("StudentId")
                    lblterm1FName.Text = dtGetStudent1.Rows(0)("FatherName")
                    lblterm1MName.Text = dtGetStudent1.Rows(0)("MotherName")

                End If

                Dim dt1 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
                ltrlMark.Text = ""
                ltrlScholastic.Text = ""
                Dim TotalMarks As String = ""
                Dim PeriodicTestMarks As String = ""
                Dim NotebookMarks As String = ""
                Dim SubEnrichmentMarks As String = ""
                Dim Grade As String = ""
                Dim AnnualMarks As String = ""
                If dt1.Rows.Count > 0 Then
                    For Each row As DataRow In dt1.Rows
                        Dim data As DataTable = BLL.Get_ExamResult(row("SubjectCode"), ddlexam.SelectedValue, ddlstudent.SelectedValue)
                        If data.Rows.Count > 0 Then
                            If data.Rows(0)("TotalMarks") = 0 Then
                                TotalMarks = "-"
                            Else
                                TotalMarks = data.Rows(0)("TotalMarks").ToString()
                            End If
                            If data.Rows(0)("PeriodicTestMarks") = 0 Then
                                PeriodicTestMarks = "-"
                            Else
                                PeriodicTestMarks = data.Rows(0)("PeriodicTestMarks").ToString()
                            End If
                            If data.Rows(0)("NotebookMarks") = 0 Then
                                NotebookMarks = "-"
                            Else
                                NotebookMarks = data.Rows(0)("NotebookMarks").ToString()
                            End If
                            If data.Rows(0)("SubEnrichmentMarks") = 0 Then
                                SubEnrichmentMarks = "-"
                            Else
                                SubEnrichmentMarks = data.Rows(0)("SubEnrichmentMarks").ToString()
                            End If
                            If data.Rows(0)("Grade") = "0" Then
                                Grade = "-"
                            Else
                                Grade = data.Rows(0)("Grade").ToString()
                            End If

                            Dim Annual_Marks As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data.Rows(0)("NotebookMarks")) + Convert.ToInt32(data.Rows(0)("SubEnrichmentMarks"))
                            If Annual_Marks = 0 Then
                                AnnualMarks = "-"
                            Else
                                AnnualMarks = Annual_Marks
                            End If
                            If Convert.ToInt32(data.Rows(0)("Attandance")) = 1 Then
                                ltrlMark.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                            Else
                                ltrlMark.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td>" & PeriodicTestMarks & "</td><td>" & NotebookMarks & "</td><td>" & SubEnrichmentMarks & "</td><td>" & TotalMarks & "</td><td>" & AnnualMarks & "</td><td>" & Grade & "</td></tr>"
                            End If
                        Else
                            ltrlMark.Text &= "<tr><td>" & row("SubjectName").ToString() & "</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                        End If
                    Next row
                Else
                    ltrlMark.Text &= "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>"
                End If
                Dim gradeScho1 As String = ""
                Dim dtGrade1 As DataTable = BLL.ExecDataTableProc("Prc_GetGrade", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtGrade1 IsNot Nothing Then
                    If dtGrade1.Rows.Count > 0 Then
                        For Each row As DataRow In dtGrade1.Rows
                            If row("Grade") = "0" Then
                                gradeScho1 = "-"
                            Else
                                gradeScho1 = row("Grade").ToString()
                            End If
                            ltrlScholastic.Text &= "<tr><td >" & row("SubjectName").ToString() & "</td><td class='tds'>" & gradeScho1 & "</td></tr>"
                        Next row
                    Else
                        ltrlScholastic.Text &= "<tr><td ></td><td class='tds'></td></tr>"
                    End If
                End If
                ltrlDiscipline.Text = ""
                    Dim GradeDisp As String = ""
                    Dim dtDecipline As DataTable = BLL.ExecDataTableProc("Prc_GetGradeDecipline", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtDecipline IsNot Nothing Then
                    If dtDecipline.Rows.Count > 0 Then
                        If dtDecipline.Rows(0)("Grade") = "0" Then
                            GradeDisp = "-"
                        Else
                            GradeDisp = dtDecipline.Rows(0)("Grade").ToString()
                        End If
                        ltrlDiscipline.Text = GradeDisp
                    Else
                        ltrlDiscipline.Text = "-"

                    End If
                End If
                ltrlFIT.Text = ""
                    Dim TotalMarksFit As String = ""
                    Dim MaxTotal As String = ""
                    Dim GradeFIT As String = ""
                    Dim MxaPracticalMarksFit As String = ""
                    Dim dtFIT As DataTable = BLL.ExecDataTableProc("Prc_GetGradeFIT", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", ddlstudent.SelectedValue)
                If dtFIT IsNot Nothing Then
                    If dtFIT.Rows.Count > 0 Then
                        If dtFIT.Rows(0)("SubjectName").ToString() = "FIT" Then
                            lblTh.InnerHtml = "40"
                            lblPrc.InnerHtml = "60"
                        Else
                            lblTh.InnerHtml = "30"
                            lblPrc.InnerHtml = "70"
                        End If
                        If dtFIT.Rows(0)("TotalMarks") = 0 Then
                            TotalMarksFit = "-"
                        Else
                            TotalMarksFit = dtFIT.Rows(0)("TotalMarks").ToString()
                        End If
                        If dtFIT.Rows(0)("MxaPracticalMarks") = 0 Then
                            MxaPracticalMarksFit = "-"
                        Else
                            MxaPracticalMarksFit = dtFIT.Rows(0)("MxaPracticalMarks").ToString()
                        End If
                        If dtFIT.Rows(0)("MaxTotal") = 0 Then
                            MaxTotal = "-"
                        Else
                            MaxTotal = dtFIT.Rows(0)("MaxTotal").ToString()
                        End If
                        If dtFIT.Rows(0)("Grade") = "0" Then
                            GradeFIT = "-"
                        Else
                            GradeFIT = dtFIT.Rows(0)("Grade").ToString()
                        End If

                        ltrlFIT.Text = "<tr><td class='tds' >" & dtFIT.Rows(0)("SubjectName").ToString() & "</td><td class='tds'>" & TotalMarksFit & "</td><td class='tds'>" & MxaPracticalMarksFit & "</td><td class='tds'>" & MaxTotal & "</td><td class='tds'>" & GradeFIT & "</td></tr>"
                    Else
                        ltrlFIT.Text = ""
                        lblTh.InnerHtml = "0"
                        lblPrc.InnerHtml = "0"
                    End If
                End If

            End If

                litmsg.Text = ""

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged

        ddlstudent.Items.Clear()
        ddlstudent.Items.Insert(0, New ListItem("--Select--", "0"))
        ddlstudent.DataSource = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
        'ddlstudent.DataSource = BLL.ExecDataTable("select * from studentmaster where Mainclassid=@classid order by StudentName", "@classid", ddlClass.SelectedValue)
        ddlstudent.DataTextField = "StudentName"
        ddlstudent.DataValueField = "StudentId"
        ddlstudent.DataBind()

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



End Class
