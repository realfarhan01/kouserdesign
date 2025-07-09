
Partial Class EmpAttendanace
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'BindClass()
            DataDisplay.DataSource = BLL.ExecDataTable("select * from Employeemaster")


            DataDisplay.DataBind()
        End If
    End Sub
    'Sub BindClass()
    '    ddlClass.DataSource = BLL.BindClass()

    '    ddlClass.DataTextField = "ClassName"
    '    ddlClass.DataValueField = "ClassId"
    '    ddlClass.DataBind()
    'End Sub
    'Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged


    'End Sub

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
                'BLL.AddAttedance(DataDisplay.DataKeys(mrow.RowIndex).Value, "Employee", attendance, Session("Operator"))


            Next
            litmsg.Text = Notifications.SuccessMessage("Attendance Save Successfully.")
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try


    End Sub
End Class
