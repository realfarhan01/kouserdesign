
Partial Class AddLibBook
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindbook()
            bindcat()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            BLL.AddLibBook(txtbookid.Text, ddlcat.Text, txtname.Text, txtauther.Text, txtpublication.Text, txttags.Text, txtprice.Text, txttotqty.Text)
            litmsg.Text = Notifications.SuccessMessage("Added Successfully.")
            bindcat()
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_LibBooks b inner join tbl_LibBooksCategories c on b.categoryid=c.categoryid where b.bookid=@bookid", "@bookid", e.CommandArgument)
            If dr.Read() Then
                txtname.Text = dr("bookname")
                txtauther.Text = dr("auther")
                ddlcat.SelectedValue = dr("categoryid")
                txtbookid.Text = dr("bookid")
                txtpublication.Text = dr("publication")
                txttags.Text = dr("tags")
                txtprice.Text = dr("price")
                txttotqty.Text = dr("totalqty")
                hfId.Value = e.CommandArgument

            End If

        End If
    End Sub
    Sub bindbook()
        DataDisplay.DataSource = BLL.ExecDataTable("select * from tbl_LibBooks")
        DataDisplay.DataBind()
    End Sub
    Sub bindcat()
        ddlcat.DataSource = BLL.ExecDataTable("select * from tbl_LibBooksCategories")
        ddlcat.DataTextField = "categoryname"
        ddlcat.DataValueField = "categoryid"
        ddlcat.DataBind()
    End Sub
End Class
