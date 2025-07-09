
Partial Class Admin_Settimetable
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim ddlTeacher As DropDownList
    Dim ddlSubject As DropDownList
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddlClass.DataSource = BLL.BindClasses()
            ddlClass.DataTextField = "Class"
            ddlClass.DataValueField = "ClassId"
            ddlClass.DataBind()
           

        End If
    End Sub
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim sbvaild As String = (New BusinessLogicLayer).DisabledButtonCode("UserRegistration") & Page.ClientScript.GetPostBackEventReference(btnSubmit, Nothing) & ";"
        btnSubmit.Attributes.Add("onclick", sbvaild)
    End Sub
    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        If ddlClass.SelectedValue > 0 Then
            Dim dtteachers As DataTable
            Dim dtSubjects As DataTable
            Dim rowno As Integer = 0
            Dim dt As DataTable = BLL.ExecDataTableProc("Get_ClassTimeTable", "@ClassId", ddlClass.SelectedValue)
            dtteachers = BLL.BindTeachers()
            dtSubjects = BLL.ExecDataTableProc("Get_ClassSubjects", "@classid", ddlClass.SelectedValue)
            If dt.Rows.Count() > 0 Then

                i = 1
                While i <= 9
                    j = 1
                    While j <= 6
                        ddlTeacher = Me.FindControl("ddlTeacher" & i & j)
                        ddlSubject = Me.FindControl("ddlSubject" & i & j)

                        ddlTeacher.DataSource = BLL.BindAvailableTeachers(ddlClass.SelectedValue, j, i)
                        ddlTeacher.DataTextField = "EmployeeName"
                        ddlTeacher.DataValueField = "EmployeeId"
                        ddlTeacher.DataBind()

                        ddlSubject.DataSource = dtSubjects
                        ddlSubject.DataTextField = "SubjectName"
                        ddlSubject.DataValueField = "SubjectCode"
                        ddlSubject.DataBind()


                        Dim result() As DataRow = dt.Select("[Weekday] = " & j & " and LecNo=" & i & "")

                        For rowno = 0 To result.GetUpperBound(0)
                            ddlTeacher.SelectedValue = result(rowno)(4)
                            ddlSubject.SelectedValue = result(rowno)(2)
                        Next rowno

                        j = j + 1
                    End While
                    i = i + 1
                End While
            Else
                i = 1
                While i <= 9
                    j = 1
                    While j <= 6
                        ddlTeacher = Me.FindControl("ddlTeacher" & i & j)
                        ddlSubject = Me.FindControl("ddlSubject" & i & j)

                        ddlTeacher.DataSource = dtteachers
                        ddlTeacher.DataTextField = "EmployeeName"
                        ddlTeacher.DataValueField = "EmployeeId"
                        ddlTeacher.DataBind()

                        ddlSubject.DataSource = dtSubjects
                        ddlSubject.DataTextField = "SubjectName"
                        ddlSubject.DataValueField = "SubjectCode"
                        ddlSubject.DataBind()

                        j = j + 1
                    End While
                    i = i + 1
                End While
            End If

        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim isExists As Integer = 0
            If ddlClass.SelectedValue > 0 Then

                Dim dt As New DataTable("temp")

                dt.Columns.Add("WeekDay", Type.GetType("System.Int32"))
                dt.Columns.Add("LecNo", Type.GetType("System.Int32"))
                dt.Columns.Add("TeacherId", Type.GetType("System.String"))
                dt.Columns.Add("SubjectId", Type.GetType("System.String"))


                i = 1
                While i <= 9
                    j = 1
                    While j <= 6
                        ddlTeacher = Me.FindControl("ddlTeacher" & i & j)
                        ddlSubject = Me.FindControl("ddlSubject" & i & j)

                        isExists = BLL.ExecScalar("Select Case When Exists(Select 1 From SchoolTimeTable Where [WeekDay]=@WeekDay and LecNo=@LecNo and TeacherId=@TeacherId and classId<>@classid) then 1 Else 0 End", "@WeekDay", j, "@LecNo", i, "@TeacherId", ddlTeacher.SelectedValue, "@classid", ddlClass.SelectedValue)
                        If isExists > 0 Then
                            dt.Rows.Add(New Object() {j, i, "", ddlSubject.SelectedValue})
                        Else
                            dt.Rows.Add(New Object() {j, i, ddlTeacher.SelectedValue, ddlSubject.SelectedValue})
                        End If

                        j = j + 1
                    End While
                    i = i + 1
                End While


                Dim res As String = ""
                res = BLL.AddClassTimeTable(ddlClass.SelectedValue, "Admin", dt)
                If res.Chars(0) = "#" Then
                    litmsg.Text = Notifications.SuccessMessage(res)
                Else
                    litmsg.Text = Notifications.ErrorMessage(res)
                End If
            Else
                litmsg.Text = Notifications.ErrorMessage("Please Select Class !!")
            End If
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Error: " & ex.ToString())
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
        Response.Redirect("home.aspx")
    End Sub
End Class
