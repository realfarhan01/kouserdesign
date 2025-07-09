
Partial Class Admin_viewtimetableiframe
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim lblTeacher As Label
    Dim lblSubject As Label
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("User") = "Student" Then
                ddlClass.Visible = False
                TimeTable(Session("classId"))
                divsearch.Visible = False
            ElseIf Session("User") = "Employee" Then
                ddlClass.Visible = False
                TimeTableTeacher(Session("UserId"))
                divsearch.Visible = False
            Else
                ddlClass.DataSource = BLL.BindClasses()
                ddlClass.DataTextField = "Class"
                ddlClass.DataValueField = "ClassId"
                ddlClass.DataBind()
                divsearch.Visible = True
            End If
        End If
    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        If ddlClass.SelectedValue > 0 Then
            TimeTable(ddlClass.SelectedValue)
        End If
    End Sub
    Sub TimeTable(ByVal ClassId As Integer)
        Dim rowno As Integer = 0
        Dim dt As DataTable = BLL.ExecDataTableProc("Get_ClassTimeTable", "@ClassId", ClassId)
        i = 1
        While i <= 9
            j = 1
            While j <= 6
                lblTeacher = Me.FindControl("lblSubject" & i & j)
                lblSubject = Me.FindControl("lblTeacher" & i & j)

                Dim result() As DataRow = dt.Select("[Weekday] = " & j & " and LecNo=" & i & "")

                For rowno = 0 To result.GetUpperBound(0)
                    lblTeacher.Text = result(rowno)(3)
                    If result(rowno)(5) <> "" Then
                        lblSubject.Text = "(" & result(rowno)(5) & ")"
                    Else
                        lblSubject.Text = ""
                    End If
                Next rowno

                j = j + 1
            End While
            i = i + 1
        End While
    End Sub
    Sub TimeTableTeacher(ByVal TeacherId As String)
        Dim rowno As Integer = 0
        Dim dt As DataTable = BLL.ExecDataTableProc("Get_TeacherTimeTable", "@TeacherId", TeacherId)
        i = 1
        While i <= 9
            j = 1
            While j <= 6
                lblTeacher = Me.FindControl("lblSubject" & i & j)
                lblSubject = Me.FindControl("lblTeacher" & i & j)

                Dim result() As DataRow = dt.Select("[Weekday] = " & j & " and LecNo=" & i & "")

                For rowno = 0 To result.GetUpperBound(0)
                    lblTeacher.Text = result(rowno)(3)
                    If result(rowno)(5) <> "" Then
                        lblSubject.Text = "(" & result(rowno)(5) & ")"
                    Else
                        lblSubject.Text = ""
                    End If
                Next rowno

                j = j + 1
            End While
            i = i + 1
        End While
    End Sub
End Class
