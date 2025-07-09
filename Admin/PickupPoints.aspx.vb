
Partial Class PickupPoints
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            binddata()
            hfId.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddUpdatePickupPoints(hfId.Value, txtname.Text, txtAmount.Text)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = 0
                binddata()
                txtname.Text = ""
                txtAmount.Text = "0"
            Else
                litmsg.Text = Notifications.ErrorMessage(res)
            End If
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("Select PickupPoint,Amount from tbl_PickupLocation Where PickupPointId=@PickupPointId", "@PickupPointId", e.CommandArgument)
            If dr.Read() Then
                txtname.Text = dr("PickupPoint")
                txtAmount.Text = dr("Amount")
                hfId.Value = e.CommandArgument

            End If

        End If
    End Sub
    Sub binddata()
        DataDisplay.DataSource = BLL.ExecDataTable("Select PickupPointId,PickupPoint,Amount from tbl_PickupLocation order by PickupPointId")
        DataDisplay.DataBind()
    End Sub
End Class
