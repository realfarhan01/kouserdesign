
Partial Class Admin_AddNews
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            hfId.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            BLL.AddNews(hfId.Value, txttitle.Text, txtNews.Text, ddlStatus.SelectedValue)
            hfId.Value = 0
            litmsg.Text = Notifications.SuccessMessage("Added Successfully.")
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
       
    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_News where cnt=@cnt", "@cnt", e.CommandArgument)
            If dr.Read() Then
                txtNews.Text = dr("News")
                txttitle.Text = dr("Title")
                ddlStatus.SelectedValue = dr("Deactivated")
                hfId.Value = e.CommandArgument
            End If

        End If
    End Sub
    Sub bindroom()
        DataDisplay.DataSource = BLL.ExecDataTable("select *,case when Deactivated=0 then 'Active' else 'Deactive' End Status from tbl_News")
        DataDisplay.DataBind()
    End Sub

End Class
