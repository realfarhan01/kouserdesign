
Partial Class Admin_TrSheet
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'bind()
            'ltrSchool.Text = BLL.BindSchoolHeader()
            'Literal1.Text = ltrSchool.Text
            bindExam()
            BindClass()
        End If
    End Sub
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Literal5.Text = ddlClass.SelectedItem.Text
            If ddlexam.SelectedValue = "10" Then
                Literal4.Text = ""
                ltrlSubject.Text = ""
                Literal1.Text = ""
                Literal2.Text = ""
                If ddlClass.SelectedValue = 12 Then
                    Dim SubjectCode As String = ""
                    Dim dt1 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
                    If dt1.Rows.Count > 0 Then
                        ltrlSubject.Text = "<tr><td rowspan='3'>S.No.</td><td rowspan='3'>Roll No.</td><td rowspan='3'>Adm No.</td><td rowspan='3'>Student Name</td>"
                        Literal1.Text = "<tr>"
                        Literal2.Text = "<tr>"
                        Dim str As String = ""
                        Dim str1 As String = ""
                        Dim str2 As String = ""
                        For Each row As DataRow In dt1.Rows
                            SubjectCode = row("SubjectCode").ToString()
                            str &= "<td colspan='6'>" & row("SubjectName").ToString() & "</td>"
                            str1 &= "<td colspan='6'>Academic Year(100 marks)</td>"
                            str2 &= "<td>PT(10)</td><td>NoteBook(5)</td><td>SE(5)</td><td>YE(80)</td><td>Total(100)</td><td>Grade</td>"
                        Next
                        str &= "<td colspan='4' rowspan='2'>FIT</td>"
                        str2 &= "<td>T(40)</td><td>P(60)</td><td>Total(100)</td><td>Grade</td>"
                        If str <> "" Then
                            str &= "<td rowspan='3'>GRAND TOTAL</td><td rowspan='3'>%</td><td rowspan='3'>Div</td><td rowspan='3'>Rank</td><td rowspan='3'>Attendance</td><td rowspan='3'>Result</td>"
                            ltrlSubject.Text &= str & "</tr>"
                        End If
                        If str1 <> "" Then
                            Literal1.Text &= str1 & "</tr>"
                        End If
                        If str2 <> "" Then
                            Literal2.Text &= str2 & "</tr>"
                        End If
                        Dim dt As DataTable = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                        'Dim dt As DataTable = BLL.ExecDataTable("Select * From StudentMaster where ClassId=@ClassId and  isBlock=0 and TCGenerateDate is null order by StudentName", "@ClassId", ddlClass.SelectedValue)
                        Dim str3 As String = ""

                        If dt.Rows.Count > 0 Then
                            Dim i As Integer = 1
                            Dim GrandTotal As Integer = 0
                            Dim percentage As Integer = 0
                            Dim TotalMarks As Integer = 0
                            Dim GrandTotalSum As Int32 = 0
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

                            For Each row As DataRow In dt.Rows
                                'Dim data As DataTable = BLL.Get_ExamResult(SubjectCode, "9", row("StudentId"))
                                Dim data As DataTable = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", "9", "@StudentId", row("StudentId"), "@MainClassId", ddlClass.SelectedValue, "@SubjectId", "")
                                Literal4.Text &= "<tr><td>" & i & "</td><td>" & row("RollNo") & "</td><td>" & row("StudentId") & "</td><td>" & row("StudentName") & "</td>"
                                If data.Rows.Count > 0 Then
                                    For Each row1 As DataRow In data.Rows
                                        Dim Annual_Marks As Integer = Convert.ToInt32(row1("TotalMarks")) + Convert.ToInt32(row1("PeriodicTestMarks")) + Convert.ToInt32(row1("NotebookMarks")) + Convert.ToInt32(row1("SubEnrichmentMarks"))
                                        table.Rows.Add(Convert.ToInt32(row1("SubjectCode")), row1("SubjectName").ToString(), Convert.ToInt32(row1("PeriodicTestMarks")), Convert.ToInt32(row1("NotebookMarks")), Convert.ToInt32(row1("SubEnrichmentMarks")), Convert.ToInt32(row1("TotalMarks")), Annual_Marks, row1("Grade").ToString(), 0, 0, 0, 0, 0, "", "", "")
                                        Dim data1 As DataTable = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", ddlexam.SelectedValue, "@StudentId", row("StudentId"), "@MainClassId", ddlClass.SelectedValue, "@SubjectId", row1("SubjectCode").ToString())
                                        If data1.Rows.Count > 0 And data.Rows.Count > 0 Then

                                            Dim Row2() As Data.DataRow
                                            Row2 = table.Select("SubjectCode = '" & row1("SubjectCode") & "'")
                                            Row2(0)("PeriodicTestMarks2") = Math.Round((Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(Row2(0)("PeriodicTestMarks1"))) / 2)
                                            Row2(0)("NotebookMarks2") = Math.Round((Convert.ToInt32(data1.Rows(0)("NotebookMarks")) + Convert.ToInt32(Row2(0)("NotebookMarks1"))) / 2)
                                            Row2(0)("SubEnrichmentMarks2") = Math.Round((Convert.ToInt32(data1.Rows(0)("SubEnrichmentMarks")) + Convert.ToInt32(Row2(0)("SubEnrichmentMarks1"))) / 2)
                                            Row2(0)("TotalMarks2") = Math.Round((Convert.ToInt32(data1.Rows(0)("TotalMarks")) + Convert.ToInt32(Row2(0)("TotalMarks1"))) / 2)

                                            Dim AvgMarks As Int32 = Convert.ToInt32(Row2(0)("TotalMarks2")) + Convert.ToInt32(Row2(0)("PeriodicTestMarks2")) + Convert.ToInt32(Row2(0)("NotebookMarks2")) + Convert.ToInt32(Row2(0)("SubEnrichmentMarks2"))
                                            Row2(0)("Annual_Marks2") = AvgMarks
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
                                            Row2(0)("Grade2") = Grade
                                        End If
                                    Next
                                End If

                                If table.Rows.Count > 0 Then
                                    For Each row1 As DataRow In table.Rows
                                        Dim PeriodicTestMarks2 As String = row1("PeriodicTestMarks2").ToString
                                        If PeriodicTestMarks2 = "0" Then
                                            PeriodicTestMarks2 = "-"
                                        End If
                                        Dim NotebookMarks2 As String = row1("NotebookMarks2").ToString
                                        If NotebookMarks2 = "0" Then
                                            NotebookMarks2 = "-"
                                        End If
                                        Dim SubEnrichmentMarks2 As String = row1("SubEnrichmentMarks2").ToString
                                        If SubEnrichmentMarks2 = "0" Then
                                            SubEnrichmentMarks2 = "-"
                                        End If
                                        Dim TotalMarks2 As String = row1("TotalMarks2").ToString
                                        If TotalMarks2 = "0" Then
                                            TotalMarks2 = "-"
                                        End If
                                        Dim Grade2 As String = row1("Grade2").ToString
                                        Dim Annual_Marks2 As String = row1("Annual_Marks2").ToString
                                        If Annual_Marks2 = "0" Then
                                            Annual_Marks2 = "-"
                                            Grade2 = ""
                                        End If

                                        str3 &= "<td>" & PeriodicTestMarks2 & "</td><td>" & NotebookMarks2 & "</td><td>" & SubEnrichmentMarks2 & "</td><td>" & TotalMarks2 & "</td><td>" & Annual_Marks2 & "</td><td>" & Grade2 & "</td>"
                                        GrandTotalSum = GrandTotalSum + Convert.ToInt32(row1("Annual_Marks2"))
                                    Next row1
                                End If
                                table.Clear()
                                Dim Divisin As String = ""
                                If str3 <> "" Then
                                    Dim TotalMarksFit As String = ""
                                    Dim MaxTotal As String = ""
                                    Dim GradeFIT As String = ""
                                    Dim MxaPracticalMarksFit As String = ""
                                    Dim dtFIT As DataTable = BLL.ExecDataTableProc("Prc_GetGradeFIT", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue, "@StudentId", row("StudentId"))
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
                                        str3 &= "<td>" & TotalMarksFit & "</td><td>" & MxaPracticalMarksFit & "</td><td>" & MaxTotal & "</td><td>" & GradeFIT & "</td>"
                                    Else
                                        str3 &= "<td></td><td></td><td></td><td></td>"
                                    End If
                                    str3 &= "<td>" & GrandTotalSum & "</td><td></td><td>" & Divisin & "</td><td></td><td></td><td></td>"
                                    Literal4.Text &= str3 & "</tr>"
                                End If
                                GrandTotalSum = 0
                                i = Convert.ToInt32(i + 1)
                                str3 = ""
                                GrandTotal = 0
                                percentage = 0
                                TotalMarks = 0
                                Divisin = ""
                            Next row
                        End If

                    End If

                ElseIf (ddlClass.SelectedValue = "14" Or ddlClass.SelectedValue = "15" Or ddlClass.SelectedValue = "16" Or ddlClass.SelectedValue = "17") Then
                    Dim SubjectCode As String = ""
                    Dim dt1 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
                    If dt1.Rows.Count > 0 Then
                        ltrlSubject.Text = "<tr><td rowspan='3'>S.No.</td><td rowspan='3'>Roll No.</td><td rowspan='3'>Adm No.</td><td rowspan='3'>Student Name</td>"
                        Literal1.Text = "<tr>"
                        Literal2.Text = "<tr>"
                        Dim str As String = ""
                        Dim str1 As String = ""
                        Dim str2 As String = ""
                        For Each row As DataRow In dt1.Rows
                            SubjectCode = row("SubjectCode").ToString()
                            str &= "<td colspan='8'>" & row("SubjectName").ToString() & "</td>"
                            str1 &= "<td rowspan='2'>PT(50)</td><td colspan='2'>HY</td><td rowspan='2'>TOTAL(100)</td><td colspan='2'>Annual</td><td rowspan='2'>TOTAL(100)</td><td rowspan='2'>GRAND TOTAL</td>"
                            str2 &= "<td>P</td><td>T</td><td>P</td><td>T</td>"
                        Next
                        If str <> "" Then
                            str &= "<td rowspan='3'>GRAND TOTAL</td><td rowspan='3'>%</td><td rowspan='3'>Div</td><td rowspan='3'>Rank</td><td rowspan='3'>Attendance</td><td rowspan='3'>Result</td>"
                            ltrlSubject.Text &= str & "</tr>"
                        End If
                        If str1 <> "" Then
                            'str1 &= "<td></td><td></td><td></td><td></td><td></td><td></td>"
                            Literal1.Text &= str1 & "</tr>"
                        End If
                        If str2 <> "" Then
                            'str2 &= "<td></td><td></td><td></td><td></td><td></td><td></td>"
                            Literal2.Text &= str2 & "</tr>"
                        End If
                        Dim dt As DataTable = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                        'Dim dt As DataTable = BLL.ExecDataTable("Select * From StudentMaster where ClassId=@ClassId and  isBlock=0 and TCGenerateDate is null order by StudentName", "@ClassId", ddlClass.SelectedValue)
                        Dim str3 As String = ""

                        If dt.Rows.Count > 0 Then
                            Dim i As Integer = 1
                            Dim GrandTotal As Integer = 0
                            Dim percentage As Integer = 0
                            Dim TotalMarks As Integer = 0
                            Dim GrandTotalSum As Int32 = 0
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

                            For Each row As DataRow In dt.Rows
                                'Dim data As DataTable = BLL.Get_ExamResult(SubjectCode, "9", row("StudentId"))
                                Dim data As DataTable = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", "9", "@StudentId", row("StudentId"), "@MainClassId", ddlClass.SelectedValue, "@SubjectId", "")
                                Literal4.Text &= "<tr><td>" & i & "</td><td>" & row("RollNo") & "</td><td>" & row("StudentId") & "</td><td>" & row("StudentName") & "</td>"
                                If data.Rows.Count > 0 Then
                                    For Each row1 As DataRow In data.Rows
                                        Dim TotalTerm1 As Integer = Convert.ToInt32(row1("TotalMarks")) + Convert.ToInt32(row1("MxaPracticalMarks"))
                                        table.Rows.Add(row1("SubjectCode").ToString(), row1("SubjectName").ToString(), 0, Convert.ToInt32(row1("TotalMarks")), Convert.ToInt32(row1("MxaPracticalMarks")), TotalTerm1, 0, 0, 0, TotalTerm1)
                                        Dim data1 As DataTable = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", ddlexam.SelectedValue, "@StudentId", row("StudentId"), "@MainClassId", ddlClass.SelectedValue, "@SubjectId", row1("SubjectCode").ToString())
                                        If data1.Rows.Count > 0 And data.Rows.Count > 0 Then

                                            Dim Row2() As Data.DataRow
                                            Row2 = table.Select("SubjectCode = '" & row1("SubjectCode").ToString() & "'")
                                            Row2(0)("Periodic") = Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks"))
                                            Row2(0)("Theory2") = Convert.ToInt32(data1.Rows(0)("TotalMarks"))
                                            Row2(0)("Practical2") = Convert.ToInt32(data1.Rows(0)("MxaPracticalMarks"))
                                            Row2(0)("TotalMarks2") = (Row2(0)("Theory2") + Row2(0)("Practical2"))
                                            Row2(0)("GrandTotal") = (Row2(0)("Periodic") + Row2(0)("TotalMarks1") + Row2(0)("TotalMarks2"))
                                        Else

                                        End If
                                    Next
                                End If

                                'Dim data1 As DataTable = BLL.Get_ExamResult(SubjectCode, ddlexam.SelectedValue, row("StudentId"))

                                'For Each row1 As DataRow In dt2.Rows
                                '    Dim total As Integer = Convert.ToInt32(row1("PeriodicTestMarks")) + Convert.ToInt32(row1("NotebookMarks")) + Convert.ToInt32(row1("SubEnrichmentMarks"))
                                '    Dim MaxTotal As Integer = total + Convert.ToInt32(row1("TotalMarks"))
                                '    GrandTotal = GrandTotal + (MaxTotal + total)
                                '    str3 &= "<td>" & row1("PeriodicTestMarks").ToString() & "</td><td>" & row1("TotalMarks").ToString() & "</td><td>" & MaxTotal & " " & row1("Grade").ToString() & "</td>"
                                'Next

                                If table.Rows.Count > 0 Then
                                    For Each row1 As DataRow In table.Rows
                                        Dim Practical1 As String = row1("Practical1").ToString
                                        If Practical1 = "0" Then
                                            Practical1 = "-"
                                        End If
                                        Dim Practical2 As String = row1("Practical2").ToString
                                        If Practical2 = "0" Then
                                            Practical2 = "-"
                                        End If
                                        Dim Periodic As String = row1("Periodic").ToString
                                        If Periodic = "0" Then
                                            Periodic = "-"
                                        End If
                                        Dim Theory1 As String = row1("Theory1").ToString
                                        If Theory1 = "0" Then
                                            Theory1 = "-"
                                        End If
                                        Dim TotalMarks1 As String = row1("TotalMarks1").ToString
                                        If TotalMarks1 = "0" Then
                                            TotalMarks1 = "-"
                                        End If
                                        Dim Theory2 As String = row1("Theory2").ToString
                                        If Theory2 = "0" Then
                                            Theory2 = "-"
                                        End If
                                        Dim TotalMarks2 As String = row1("TotalMarks2").ToString
                                        If TotalMarks2 = "0" Then
                                            TotalMarks2 = "-"
                                        End If
                                        GrandTotal = row1("GrandTotal").ToString
                                        If GrandTotal = "0" Then
                                            GrandTotal = "-"
                                        End If

                                        str3 &= "<td>" & Periodic & "</td><td>" & Practical1 & "</td><td>" & Theory1 & "</td><td>" & TotalMarks1 & "</td><td>" & Practical2 & "</td><td>" & Theory2 & "</td><td>" & TotalMarks2 & "</td><td>" & GrandTotal & "</td>"
                                        GrandTotalSum = GrandTotalSum + Convert.ToInt32(row1("GrandTotal"))
                                    Next row1
                                End If
                                table.Clear()
                                Dim Divisin As String = ""
                                If str3 <> "" Then
                                    'percentage = (GrandTotal / TotalMarks) * 100
                                    'percentage = Math.Round(percentage, 0)

                                    'If percentage >= 60 Then
                                    '    Divisin = "I"
                                    'End If
                                    'If percentage >= 48 And percentage <= 59 Then
                                    '    Divisin = "II"
                                    'End If
                                    'If percentage < 48 Then
                                    '    Divisin = "III"
                                    'End If
                                    str3 &= "<td>" & GrandTotalSum & "</td><td></td><td>" & Divisin & "</td><td></td><td></td><td></td>"
                                    Literal4.Text &= str3 & "</tr>"
                                End If
                                GrandTotalSum = 0
                                i = Convert.ToInt32(i + 1)
                                str3 = ""
                                GrandTotal = 0
                                percentage = 0
                                TotalMarks = 0
                                Divisin = ""
                            Next row
                        End If

                    End If

                Else
                    Dim SubjectCode As String = ""
                    Dim dt1 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
                    If dt1.Rows.Count > 0 Then
                        ltrlSubject.Text = "<tr><td rowspan='3'>S.No.</td><td rowspan='3'>Roll No.</td><td rowspan='3'>Adm No.</td><td rowspan='3'>Student Name</td>"
                        Literal1.Text = "<tr>"
                        Literal2.Text = "<tr>"
                        Dim str As String = ""
                        Dim str1 As String = ""
                        Dim str2 As String = ""
                        For Each row As DataRow In dt1.Rows
                            SubjectCode = row("SubjectCode").ToString()
                            str &= "<td colspan='12'>" & row("SubjectName").ToString() & "</td>"
                            str1 &= "<td colspan='6'>Term-1</td><td colspan='6'>Term-2</td>"
                            str2 &= "<td>PT</td><td>Notebook</td><td>SE</td><td>MidTerm</td><td>Total</td><td>Grade</td><td>PT</td><td>Notebook</td><td>SE</td><td>MidTerm</td><td>Total</td><td>Grade</td>"
                        Next
                        If str <> "" Then
                            str &= "<td rowspan='3'>GRAND TOTAL</td><td rowspan='3'>%</td><td rowspan='3'>Div</td><td rowspan='3'>Rank</td><td rowspan='3'>Attendance</td><td rowspan='3'>Result</td>"
                            ltrlSubject.Text &= str & "</tr>"
                        End If
                        If str1 <> "" Then
                            Literal1.Text &= str1 & "</tr>"
                        End If
                        If str2 <> "" Then
                            Literal2.Text &= str2 & "</tr>"
                        End If
                        Dim dt As DataTable = BLL.ExecDataTableProc("Prc_GetExamStudents", "@ExamId", ddlexam.SelectedValue, "@ClassId", ddlClass.SelectedValue)
                        Dim str3 As String = ""

                        If dt.Rows.Count > 0 Then
                            Dim i As Integer = 1
                            Dim GrandTotal As Integer = 0
                            Dim percentage As Integer = 0
                            Dim TotalMarks As Integer = 0
                            Dim GrandTotalSum As Int32 = 0
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

                            For Each row As DataRow In dt.Rows
                                Dim data As DataTable = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", "9", "@StudentId", row("StudentId"), "@MainClassId", ddlClass.SelectedValue, "@SubjectId", "")
                                Literal4.Text &= "<tr><td>" & i & "</td><td>" & row("RollNo") & "</td><td>" & row("StudentId") & "</td><td>" & row("StudentName") & "</td>"
                                If data.Rows.Count > 0 Then
                                    'Dim TotalTerm1 As Integer = Convert.ToInt32(data.Rows(0)("TotalMarks")) + Convert.ToInt32(data.Rows(0)("MxaPracticalMarks"))
                                    For Each row1 As DataRow In data.Rows
                                        Dim Annual_Marks As Integer = Convert.ToInt32(row1("TotalMarks")) + Convert.ToInt32(row1("PeriodicTestMarks")) + Convert.ToInt32(row1("NotebookMarks")) + Convert.ToInt32(row1("SubEnrichmentMarks"))
                                        table.Rows.Add(Convert.ToInt32(row1("SubjectCode")), row1("SubjectName").ToString(), Convert.ToInt32(row1("PeriodicTestMarks")), Convert.ToInt32(row1("NotebookMarks")), Convert.ToInt32(row1("SubEnrichmentMarks")), Convert.ToInt32(row1("TotalMarks")), Annual_Marks, row1("Grade").ToString(), 0, 0, 0, 0, 0, "", "", "")
                                        Dim data1 As DataTable = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", ddlexam.SelectedValue, "@StudentId", row("StudentId"), "@MainClassId", ddlClass.SelectedValue, "@SubjectId", row1("SubjectCode").ToString())
                                        If data1.Rows.Count > 0 And data.Rows.Count > 0 Then
                                            Dim Row2() As Data.DataRow
                                            Row2 = table.Select("SubjectCode = '" & row1("SubjectCode") & "'")
                                            Row2(0)("PeriodicTestMarks2") = Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks"))
                                            Row2(0)("NotebookMarks2") = Convert.ToInt32(data1.Rows(0)("NotebookMarks"))
                                            Row2(0)("SubEnrichmentMarks2") = Convert.ToInt32(data1.Rows(0)("SubEnrichmentMarks"))
                                            Row2(0)("TotalMarks2") = Convert.ToInt32(data1.Rows(0)("TotalMarks"))
                                            Row2(0)("Annual_Marks2") = Convert.ToInt32(data1.Rows(0)("TotalMarks")) + Convert.ToInt32(data1.Rows(0)("PeriodicTestMarks")) + Convert.ToInt32(data1.Rows(0)("NotebookMarks")) + Convert.ToInt32(data1.Rows(0)("SubEnrichmentMarks"))
                                            Row2(0)("Grade2") = data1.Rows(0)("Grade")
                                            Row2(0)("OveralllTotalMarks") = Convert.ToInt32(Row2(0)("Annual_Marks2")) + Convert.ToInt32(Row2(0)("Annual_Marks1"))
                                            Row2(0)("OverallGrade") = Row2(0)("Grade2")
                                        Else

                                        End If
                                    Next
                                End If

                                'Dim data1 As DataTable = BLL.Get_ExamResult(SubjectCode, ddlexam.SelectedValue, row("StudentId"))

                                'For Each row1 As DataRow In dt2.Rows
                                '    Dim total As Integer = Convert.ToInt32(row1("PeriodicTestMarks")) + Convert.ToInt32(row1("NotebookMarks")) + Convert.ToInt32(row1("SubEnrichmentMarks"))
                                '    Dim MaxTotal As Integer = total + Convert.ToInt32(row1("TotalMarks"))
                                '    GrandTotal = GrandTotal + (MaxTotal + total)
                                '    str3 &= "<td>" & row1("PeriodicTestMarks").ToString() & "</td><td>" & row1("TotalMarks").ToString() & "</td><td>" & MaxTotal & " " & row1("Grade").ToString() & "</td>"
                                'Next

                                If table.Rows.Count > 0 Then
                                    For Each row1 As DataRow In table.Rows
                                        Dim PeriodicTestMarks2 As String = row1("PeriodicTestMarks2").ToString
                                        If PeriodicTestMarks2 = "0" Then
                                            PeriodicTestMarks2 = "-"
                                        End If
                                        Dim NotebookMarks2 As String = row1("NotebookMarks2").ToString
                                        If NotebookMarks2 = "0" Then
                                            NotebookMarks2 = "-"
                                        End If
                                        Dim SubEnrichmentMarks2 As String = row1("SubEnrichmentMarks2").ToString
                                        If SubEnrichmentMarks2 = "0" Then
                                            SubEnrichmentMarks2 = "-"
                                        End If
                                        Dim TotalMarks2 As String = row1("TotalMarks2").ToString
                                        If TotalMarks2 = "0" Then
                                            TotalMarks2 = "-"
                                        End If
                                        Dim Grade2 As String = row1("Grade2").ToString
                                        Dim Annual_Marks2 As String = row1("Annual_Marks2").ToString
                                        If Annual_Marks2 = "0" Then
                                            Annual_Marks2 = "-"
                                            Grade2 = "-"
                                        End If
                                        Dim PeriodicTestMarks1 As String = row1("PeriodicTestMarks1").ToString
                                        If PeriodicTestMarks1 = "0" Then
                                            PeriodicTestMarks1 = "-"
                                        End If
                                        Dim NotebookMarks1 As String = row1("NotebookMarks1").ToString
                                        If NotebookMarks1 = "0" Then
                                            NotebookMarks1 = "-"
                                        End If
                                        Dim SubEnrichmentMarks1 As String = row1("SubEnrichmentMarks1").ToString
                                        If SubEnrichmentMarks1 = "0" Then
                                            SubEnrichmentMarks1 = "-"
                                        End If
                                        Dim Grade1 As String = row1("Grade1").ToString
                                        Dim Annual_Marks1 As String = row1("Annual_Marks1").ToString
                                        If Annual_Marks1 = "0" Then
                                            Annual_Marks1 = "-"
                                            Grade1 = "-"
                                        End If
                                        Dim TotalMarks1 As String = row1("TotalMarks1").ToString
                                        If TotalMarks1 = "0" Then
                                            TotalMarks1 = "-"
                                        End If
                                        Dim OverallGrade As String = row1("OverallGrade").ToString
                                        Dim OveralllTotalMarks As String = row1("OveralllTotalMarks").ToString
                                        If OveralllTotalMarks = "0" Then
                                            OveralllTotalMarks = "-"
                                            OverallGrade = "-"
                                        End If

                                        str3 &= "<td>" & PeriodicTestMarks1 & "</td><td>" & NotebookMarks1 & "</td><td>" & SubEnrichmentMarks1 & "</td><td>" & TotalMarks1 & "</td><td>" & Annual_Marks1 & "</td><td>" & Grade1 & "</td><td>" & PeriodicTestMarks2 & "</td><td>" & NotebookMarks2 & "</td><td>" & SubEnrichmentMarks2 & "</td><td>" & TotalMarks2 & "</td><td>" & Annual_Marks2 & "</td><td>" & Grade2 & "</td>"
                                        'GrandTotalSum = GrandTotalSum + Convert.ToInt32(row1("GrandTotal"))
                                    Next row1
                                End If
                                table.Clear()
                                Dim Divisin As String = ""
                                If str3 <> "" Then
                                    'percentage = (GrandTotal / TotalMarks) * 100
                                    'percentage = Math.Round(percentage, 0)

                                    'If percentage >= 60 Then
                                    '    Divisin = "I"
                                    'End If
                                    'If percentage >= 48 And percentage <= 59 Then
                                    '    Divisin = "II"
                                    'End If
                                    'If percentage < 48 Then
                                    '    Divisin = "III"
                                    'End If
                                    str3 &= "<td></td><td></td><td>" & Divisin & "</td><td></td><td></td><td></td>"
                                    Literal4.Text &= str3 & "</tr>"
                                End If
                                GrandTotalSum = 0
                                i = Convert.ToInt32(i + 1)
                                str3 = ""
                                GrandTotal = 0
                                percentage = 0
                                TotalMarks = 0
                                Divisin = ""
                            Next row
                        End If

                    End If
                End If
                'If ddlexam.SelectedValue = "2" Then
                '    Literal4.Text = ""
                '    ltrlSubject.Text = ""
                '    Literal1.Text = ""
                '    Literal2.Text = ""
                '    Dim dt1 As DataTable = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
                '    If dt1.Rows.Count > 0 Then
                '        ltrlSubject.Text = "<tr><td rowspan='2'>S.No.</td><td rowspan='2'>Roll No.</td><td rowspan='2'>Student Name</td>"
                '        Literal1.Text = "<tr>"
                '        Literal2.Text = "<tr><td></td><td></td><td></td>"
                '        Dim str As String = ""
                '        Dim str1 As String = ""
                '        Dim str2 As String = ""
                '        For Each row As DataRow In dt1.Rows
                '            str &= "<td colspan='6'>" & row("SubjectName").ToString() & "</td>"
                '            str1 &= "<td>PT 1</td><td>HY</td><td>PT II</td><td>PT III</td><td>Annual</td><td>TOTAL</td>"
                '            str2 &= "<td>20</td><td>80</td><td>10</td><td>10</td><td>80</td><td>200</td>"
                '        Next
                '        If str <> "" Then
                '            str &= "<td>GRAND TOTAL</td><td>%</td><td>Div</td><td>Rank</td><td>Attendance</td><td>Result</td>"
                '            ltrlSubject.Text &= str & "</tr>"
                '        End If
                '        If str1 <> "" Then
                '            str1 &= "<td></td><td></td><td></td><td></td><td></td><td></td>"
                '            Literal1.Text &= str1 & "</tr>"
                '        End If
                '        If str2 <> "" Then
                '            str2 &= "<td></td><td></td><td></td><td></td><td></td><td></td>"
                '            Literal2.Text &= str2 & "</tr>"
                '        End If
                '        Dim dt As DataTable = BLL.ExecDataTable("Select * From StudentMaster where ClassId=@ClassId and  isBlock=0 and TCGenerateDate is null order by StudentName", "@ClassId", ddlClass.SelectedValue)
                '        Dim str3 As String = ""
                '        Dim table As New DataTable
                '        table.Columns.Add("SubjectCode", GetType(Integer))
                '        table.Columns.Add("PeriodicTestMarks", GetType(Integer))
                '        table.Columns.Add("TotalMarks1", GetType(Integer))
                '        table.Columns.Add("Grade1", GetType(String))
                '        table.Columns.Add("PeriodicTestMarks2", GetType(Integer))
                '        table.Columns.Add("PeriodicTestMarks3", GetType(Integer))
                '        table.Columns.Add("TotalMarks2", GetType(Integer))
                '        table.Columns.Add("TotalMarksTerm12", GetType(Integer))
                '        table.Columns.Add("Grade2", GetType(String))
                '        If dt.Rows.Count > 0 Then
                '            Dim i As Integer = 1
                '            Dim GrandTotal As Integer = 0
                '            Dim percentage As Integer = 0
                '            Dim TotalMarks As Integer = 0
                '            For Each row As DataRow In dt.Rows
                '                Dim dt2 As DataTable = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", "1", "@StudentId", row("StudentId"), "@MainClassId", ddlClass.SelectedValue, "@SubjectId", "")
                '                Literal4.Text &= "<tr><td>" & i & "</td><td>" & row("RollNo") & "</td><td>" & row("StudentName") & "</td>"
                '                If dt2.Rows.Count > 0 Then
                '                    For Each row1 As DataRow In dt2.Rows

                '                        Dim total As Integer = Convert.ToInt32(row1("PeriodicTestMarks")) + Convert.ToInt32(row1("NotebookMarks")) + Convert.ToInt32(row1("SubEnrichmentMarks"))
                '                        Dim MaxTotal As Integer = Convert.ToInt32(row1("TotalMarks"))
                '                        table.Rows.Add(Convert.ToInt32(row1("SubjectCode")), total, MaxTotal, row1("Grade").ToString(), 0, 0, 0, 0, "")
                '                        TotalMarks = dt2.Rows.Count * 200

                '                        Dim dt12 As DataTable = BLL.ExecDataTableProc("Prc_GetExamResult", "@ExamId", ddlexam.SelectedValue, "@StudentId", row("StudentId"), "@MainClassId", ddlClass.SelectedValue, "@SubjectId", "")
                '                        If dt12.Rows.Count > 0 Then

                '                            Dim Row11() As Data.DataRow
                '                            Row11 = table.Select("SubjectCode = '" & Convert.ToInt32(row1("SubjectCode")) & "'")
                '                            Row11(0)("TotalMarks2") = Convert.ToInt32(dt12.Rows(0)("TotalMarks"))
                '                            Row11(0)("PeriodicTestMarks2") = Convert.ToInt32(dt12.Rows(0)("PeriodicTestMarks2"))
                '                            Row11(0)("PeriodicTestMarks3") = Convert.ToInt32(dt12.Rows(0)("PeriodicTestMarks3"))
                '                            Row11(0)("TotalMarksTerm12") = (Row11(0)("PeriodicTestMarks") + Row11(0)("TotalMarks1") + Row11(0)("PeriodicTestMarks2") + Row11(0)("PeriodicTestMarks3") + Row11(0)("TotalMarks2"))
                '                            Row11(0)("Grade2") = dt12.Rows(0)("Grade").ToString()
                '                            str3 &= "<td>" & total & "</td><td>" & row1("TotalMarks").ToString() & " " & Row11(0)("Grade1").ToString() & "</td><td>" & Row11(0)("PeriodicTestMarks2") & "</td><td>" & Row11(0)("PeriodicTestMarks3") & "</td><td>" & Row11(0)("TotalMarks2") & " " & Row11(0)("Grade2").ToString() & "</td><td>" & Row11(0)("TotalMarksTerm12") & "</td>"
                '                            GrandTotal = GrandTotal + (Row11(0)("TotalMarksTerm12"))
                '                        End If
                '                        table.Clear()
                '                    Next

                '                End If
                '                Dim Divisin As String = ""
                '                If str3 <> "" Then
                '                    percentage = (GrandTotal / TotalMarks) * 100
                '                    percentage = Math.Round(percentage, 0)

                '                    If percentage >= 60 Then
                '                        Divisin = "I"
                '                    End If
                '                    If percentage >= 48 And percentage <= 59 Then
                '                        Divisin = "II"
                '                    End If
                '                    If percentage < 48 Then
                '                        Divisin = "III"
                '                    End If
                '                    str3 &= "<td>" & GrandTotal & "</td><td>" & percentage & "</td><td>" & Divisin & "</td><td></td><td></td><td></td>"
                '                    Literal4.Text &= str3 & "</tr>"
                '                End If
                '                i = Convert.ToInt32(i + 1)
                '                str3 = ""
                '                GrandTotal = 0
                '                percentage = 0
                '                TotalMarks = 0
                '                Divisin = ""
                '            Next
                '        End If

                '    End If
                'End If


            End If
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
        ddlClass.DataValueField = "MainClassId"
        ddlClass.DataBind()
    End Sub

End Class
