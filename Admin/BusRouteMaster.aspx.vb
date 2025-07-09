
Partial Class Admin_BusRouteMaster
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            hfId.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(sender As Object, e As System.EventArgs) Handles btnsubmit.Click
        Try
            BLL.AddUpdateBusRoute(hfId.Value, txtBusRoute.Text, "")
            If btnsubmit.Text = "Update" Then
                hfId.Value = 0
                litmsg.Text = Notifications.SuccessMessage("Updated Successfully.")
                txtBusRoute.Text = ""
                BindData()
                btnsubmit.Text = "Submit"
            Else
                hfId.Value = 0
                litmsg.Text = Notifications.SuccessMessage("Added Successfully.")
                txtBusRoute.Text = ""
                BindData()
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub

    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("Select RouteId,RouteName from BusRouteMaster Where RouteId=@RouteId and Deactivated is Null", "@RouteId", e.CommandArgument)
            If dr.Read() Then
                txtBusRoute.Text = dr("RouteName")
                hfId.Value = dr("RouteId")
                btnsubmit.Text = "Update"
            End If
        ElseIf e.CommandName = "delete1" Then
            BLL.ExecNonQuery("update BusRouteMaster Set Deactivated=dbo.getdate() Where RouteId=@RouteId", "@RouteId", e.CommandArgument)
            BindData()
        End If
    End Sub

    Sub BindData()
        DataDisplay.DataSource = BLL.ExecDataTable("Select RouteId,RouteName from BusRouteMaster Where Deactivated is Null order by RouteName")
        DataDisplay.DataBind()
    End Sub
End Class
