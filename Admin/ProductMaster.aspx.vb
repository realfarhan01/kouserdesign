
Partial Class ProductMaster
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'binddata()
            bindcat()
            divPublisher.Visible = False
            divSupplier.Visible = False
            hfId.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddUpdateProductMaster(hfId.Value, txtname.Text, txtMRP.Text, Val(txtQuantity.Text), txtPublisher.Text, txtSupplier.Text, ddlcat.SelectedValue, ddlProductType.SelectedValue, ddlStatus.SelectedValue, ddlSession.SelectedValue)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage("Added Successfully.")
                ddlcatsearch.SelectedValue = ddlcat.SelectedValue
                binddata()
                'bindcat()
                'divPublisher.Visible = False
                'divSupplier.Visible = False
                'ddlcat.SelectedValue = 0
                'ddlProductType.SelectedValue = ""
                txtname.Text = ""
                txtMRP.Text = ""
                txtQuantity.Text = 0
                txtPublisher.Text = ""
                txtSupplier.Text = ""
                ddlStatus.SelectedValue = 1
                hfId.Value = 0
            Else
                litmsg.Text = Notifications.ErrorMessage(res)
            End If
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("Select * from vwProductMaster Where Productid=@Productid", "@Productid", e.CommandArgument)
            If dr.Read() Then
                txtname.Text = dr("ProductName")
                txtMRP.Text = dr("MRP")
                txtQuantity.Text = dr("Quantity")
                ddlcat.SelectedValue = dr("ProductCategoryId")
                txtPublisher.Text = dr("Publisher")
                txtSupplier.Text = dr("Supplier")
                ddlProductType.SelectedValue = dr("ProductType")
                ddlStatus.SelectedValue = dr("isActive")
                hfId.Value = e.CommandArgument

                divPublisher.Visible = False
                divSupplier.Visible = False
                If ddlProductType.SelectedValue = "Book" Then
                    divPublisher.Visible = True
                ElseIf ddlProductType.SelectedValue = "Stationery" Then
                    divSupplier.Visible = True
                End If
            End If
        ElseIf e.CommandName = "delete1" Then
            BLL.ExecNonQuery("update ProductMaster Set DeleteDate=getdate() Where ProductId=@ProductId", "@ProductId", e.CommandArgument)
            binddata()
        End If
    End Sub
    Sub binddata()
        'DataDisplay.DataSource = BLL.ExecDataTable("select * from vwProductMaster")
        DataDisplay.DataSource = BLL.GetProductMaster(txtProductNamesearch.Text, txtPublishersearch.Text, txtSuppliersearch.Text, ddlProductTypesearch.SelectedValue, ddlcatsearch.SelectedValue, ddlSessionsearch.SelectedValue)
        DataDisplay.DataBind()
        lblClass.Text = ddlSessionsearch.SelectedValue
    End Sub
    Sub bindcat()
        ddlcat.DataSource = BLL.ExecDataTable("select * from ProductCategoryMaster")
        ddlcat.DataTextField = "categoryname"
        ddlcat.DataValueField = "categoryid"
        ddlcat.DataBind()

        ddlcatsearch.DataSource = BLL.ExecDataTable("select * from ProductCategoryMaster")
        ddlcatsearch.DataTextField = "categoryname"
        ddlcatsearch.DataValueField = "categoryid"
        ddlcatsearch.DataBind()
    End Sub

    Protected Sub ddlProductType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProductType.SelectedIndexChanged
        divPublisher.Visible = False
        divSupplier.Visible = False
        If ddlProductType.SelectedValue = "Book" Then
            divPublisher.Visible = True
        ElseIf ddlProductType.SelectedValue = "Stationery" Then
            divSupplier.Visible = True
        End If
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        binddata()
    End Sub
End Class
