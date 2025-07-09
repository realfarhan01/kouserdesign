
Partial Class FeeSubmit
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from ReceiptMaster where paiddate is null order by cnt desc")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Response.Redirect("Receipt.aspx?rid=" + (New Encryption64).Encrypt(e.CommandArgument))

        End If
        If e.CommandName = "paid" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from ReceiptMaster  where cnt=@cnt", "@cnt", e.CommandArgument)
            If dr.Read() Then
                txtReceiptno.Text = dr("Receiptno")
                pnlgrid.Visible = False
                pnlsubmit.Visible = True

                hfId.Value = e.CommandArgument

            End If

        End If
        If e.CommandName = "delete1" Then
            BLL.ExecNonQuery("update ReceiptMaster set isdelete=1 where cnt=@cnt", "@cnt", e.CommandArgument)
            bind()
        End If


    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim res As String = BLL.AddFees(txtReceiptno.Text, txtDueDate.Text, txtremark.Text, txtlatefees.Text, ddlpmode.SelectedValue, "", "", 0, "", "", 0, 0)
            If res.Chars(0) = "" Then
                litmsg.Text = Notifications.SuccessMessage(res)
            Else
                litmsg.Text = Notifications.ErrorMessage(res)
            End If
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
    End Sub

    Protected Sub btnback_Click(sender As Object, e As System.EventArgs) Handles btnback.Click
        pnlgrid.Visible = True
        pnlsubmit.Visible = False
        bind()
    End Sub
End Class
