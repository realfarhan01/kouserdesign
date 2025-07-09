
Partial Class Admin_AddLibCategory
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindroom()
            hfId.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            BLL.AddLibCategory(hfId.Value, txtname.Text, ddlrack.SelectedValue, txtnarration.Text)
            litmsg.Text = Notifications.SuccessMessage("Added Successfully.")
            hfId.Value = 0
            bindroom()
            txtname.Text = ""
            ddlrack.SelectedValue = ""
            txtnarration.Text = ""
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_LibBooksCategories where CategoryId=@CategoryId", "@CategoryId", e.CommandArgument)
            If dr.Read() Then
                txtname.Text = dr("categoryname")
                txtnarration.Text = dr("narration")
                ddlrack.SelectedValue = dr("rackno")
                hfId.Value = e.CommandArgument

            End If

        End If
    End Sub
    Sub bindroom()
        DataDisplay.DataSource = BLL.ExecDataTable("select * from tbl_LibBooksCategories")
        DataDisplay.DataBind()
    End Sub
End Class
