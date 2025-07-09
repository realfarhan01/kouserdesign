
Partial Class AddTransEmployee
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim dt As DataTable = BLL.ExecDataTable("select * from TransVehicle where Deactivated=0")
            ddlVehicleid.DataSource = dt
            ddlVehicleid.DataTextField = "Vehicleid"
            ddlVehicleid.DataValueField = "Vehicleid"
            ddlVehicleid.DataBind()
            bind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from TransEmployee")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddTransEmployee(ddlVehicleid.SelectedValue, txtEmployeeid.Text, txtdlno.Text, txtDLExpiryDate.Text, txtBloodGroup.Text, ddlactive.SelectedValue)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = ""
                txtEmployeeid.Text = ""
                txtdlno.Text = ""
                txtDLExpiryDate.Text = ""
                txtBloodGroup.Text = ""
                bind()
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from TransEmployee  where Employeeid=@Employeeid", "@Employeeid", e.CommandArgument)
            If dr.Read() Then
                txtEmployeeid.Text = e.CommandArgument
                txtBloodGroup.Text = dr("BloodGroup")
                ddlactive.SelectedValue = dr("Deactivated")
                txtDLExpiryDate.Text = dr("DLExpiryDate")
                txtdlno.Text = dr("dlno")
                ddlVehicleid.SelectedValue = dr("Vehicleid")

                hfId.Value = e.CommandArgument

            End If

        End If
    End Sub

End Class
