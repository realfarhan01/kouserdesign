
Partial Class ExamSupervisors
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            bindExamRoom()
            hfId.Value = 0
            bind()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddExamSupervisors(ddlexamroom.SelectedValue, ddlEmployee.SelectedValue, 0)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = 0
                bind()
            Else
                litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim str As String = e.CommandArgument
            Dim Employeeid As String() = str.Split(",")
            Dim res As String = BLL.AddExamSupervisors(Employeeid(0), Employeeid(1), 1)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)

                bind()
            Else
                litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
            End If
        End If
    End Sub

    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from tbl_ExamSupervisors")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub



    Sub bindExamRoom()

        ddlexamroom.DataSource = BLL.ExecDataTable("select * from  tbl_ExamRooms")
        ddlexamroom.DataTextField = "RoomNo"
        ddlexamroom.DataValueField = "ExamRoomid"
        ddlexamroom.DataBind()

        ddlEmployee.DataSource = BLL.ExecDataTable("select * from  EmployeeMaster")
        ddlEmployee.DataTextField = "EmployeeId"
        ddlEmployee.DataValueField = "EmployeeId"
        ddlEmployee.DataBind()

    End Sub


End Class
