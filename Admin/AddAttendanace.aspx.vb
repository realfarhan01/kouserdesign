
Partial Class Admin_AddAttendanace
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
        End If
    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindClass()

        ddlClass.DataTextField = "ClassName"
        ddlClass.DataValueField = "ClassId"
        ddlClass.DataBind()
    End Sub
    Sub BindData()
        DataDisplay.DataSource = BLL.ExecDataTableProc("Prc_GetStudentAttendences", "@ClassId", ddlClass.SelectedValue, "@SchoolSession", ddlSession.SelectedValue, "@Date", txtDate.Text)
        DataDisplay.DataBind()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim rdPresent As RadioButton
        Dim rdAbsent As RadioButton
        Dim rdLeave As RadioButton
        Dim attendance As String
        Try
            For Each mrow As GridViewRow In DataDisplay.Rows
                rdPresent = mrow.FindControl("rdPresent")
                rdAbsent = mrow.FindControl("rdAbsent")
                rdLeave = mrow.FindControl("rdLeave")
                If rdAbsent.Checked = True Then
                    attendance = "A"
                ElseIf rdPresent.Checked = True Then
                    attendance = "P"
                Else
                    attendance = "L"
                End If
                BLL.AddAttedance(DataDisplay.DataKeys(mrow.RowIndex).Value, "Student", attendance, Session("Operator"), ddlClass.SelectedValue, ddlSession.SelectedValue)


            Next
            litmsg.Text = Notifications.SuccessMessage("Attendance Save Successfully.")
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try


    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        BindData()
    End Sub

    Private Sub DataDisplay_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DataDisplay.RowDataBound
        Dim rdPresent As RadioButton
        Dim rdAbsent As RadioButton
        Dim rdLeave As RadioButton
        Try
            For Each mrow As GridViewRow In DataDisplay.Rows
                rdPresent = mrow.FindControl("rdPresent")
                rdAbsent = mrow.FindControl("rdAbsent")
                rdLeave = mrow.FindControl("rdLeave")
                Dim Status As String = DataBinder.Eval(e.Row.DataItem, "Status").ToString()
                If Status = "A" Then
                    rdAbsent.Checked = True
                ElseIf Status = "P" Then
                    rdPresent.Checked = True
                ElseIf Status = "L" Then
                    rdLeave.Checked = True
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub
End Class
