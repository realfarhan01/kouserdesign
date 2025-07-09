
Partial Class Admin_AddVehicle
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddVehicle(hfId.Value, txtVehicleno.Text, txtVehicletype.Text, txtcompany.Text, txtTotseat.Text, txtRno.Text, ddlactive.SelectedValue)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = ""
                bind()
                txtVehicleno.Text = ""
                txtVehicletype.Text = ""
                txtcompany.Text = ""
                txtTotseat.Text = ""
                txtRno.Text = ""
            End If
          
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from TransVehicle  where VehicleId=@VehicleId", "@VehicleId", e.CommandArgument)
            If dr.Read() Then
                txtVehicleno.Text = dr("Vehicleno")
                txtcompany.Text = dr("Vehiclecompany")
                ddlactive.SelectedValue = dr("Deactivated")
                txtRno.Text = dr("VehicleRno")
                txtTotseat.Text = dr("TotalSeats")
                txtVehicletype.Text = dr("Vehicletype")
               
                hfId.Value = e.CommandArgument

            End If

        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from TransVehicle")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
End Class
