
Partial Class Admin_ProductReceiptList
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select RM.*,convert(varchar(20),RM.DueDate,103) ReceiptDate,convert(varchar(20),RM.PaidDate,103) PdDate,SM.StudentName,SM.FatherName,MCM.MainClassName,(Case When isCancel=1 then 'Cancelled' else 'Active' End) [Status] from ProductReceiptMaster RM inner join StudentMaster SM on RM.StudentId=SM.StudentId left join MainClassMaster MCM on SM.MAinClassId = MCM.MainClassId where  isdelete=0  order by cnt desc")
        btnExport.Visible = False
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                btnExport.Visible = True
            End If
        End If
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Response.Redirect("ProductReceipt.aspx?rid=" + (New Encryption64).Encrypt(e.CommandArgument))

        End If
        If e.CommandName = "paid" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select *,convert(varchar(20),DueDate,103) PayDate from ProductReceiptMaster  where cnt=@cnt", "@cnt", e.CommandArgument)
            If dr.Read() Then
                txtReceiptnoPaid.Text = dr("ReceiptnoSession")
                txtNetAmount.Text = dr("TotalAmount")
                txtlatefeesPaid.Text = dr("LateFee")
                txtReceiptAmt.Text = dr("ReceiptAmount")
                txtDiscount.Text = dr("Discount")
                ddlSession.SelectedValue = dr("SchoolSession")
                txtDueDatePaid.Text = dr("PayDate")
                pnlgrid.Visible = False
                pnlsubmit.Visible = True
                hfId.Value = e.CommandArgument
            End If
        End If
        If e.CommandName = "delete1" Then
            BLL.ExecNonQuery("update ProductReceiptMaster set isdelete=1 where cnt=@cnt", "@cnt", e.CommandArgument)
            bind()
        End If
    End Sub

    Protected Sub btnSubmitPaid_Click(sender As Object, e As EventArgs) Handles btnSubmitPaid.Click
        Try
            If hfId.Value <> "" Then
                Dim chequeAmt As Decimal = 0, cashAmt As Decimal = 0
                If (txtChequeAmount.Text.Trim() <> "") Then
                    chequeAmt = Convert.ToDecimal(txtChequeAmount.Text.Trim())
                End If
                If (txtCashAmount.Text.Trim() <> "") Then
                    cashAmt = Convert.ToDecimal(txtCashAmount.Text.Trim())
                End If
                Dim res As String = BLL.PayProduct(txtReceiptnoPaid.Text, txtDueDatePaid.Text, txtremarkPaid.Text, txtlatefeesPaid.Text, ddlpmodePaid.SelectedValue, txtChequeNo.Text, txtChequeDate.Text, chequeAmt, txtBankName.Text, txtBranchName.Text, cashAmt, ddlSession.SelectedValue)
                If res.Chars(0) = "#" Then
                    litmsgPaid.Text = Notifications.SuccessMessage(res)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Receipt paid successfully')", True)
                    goToListView()
                Else
                    litmsgPaid.Text = Notifications.ErrorMessage(res)
                End If
            Else
                litmsgPaid.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
            End If

        Catch ex As Exception
            litmsgPaid.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
        activateCashChequeDiv()

    End Sub
    Protected Sub DataDisplay_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DataDisplay.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPayStatus As Label = DirectCast(e.Row.FindControl("lblPayStatus"), Label)
            Dim lnkPay As LinkButton = DirectCast(e.Row.FindControl("lnkPay"), LinkButton)
            Dim lnkDelete As HtmlAnchor = DirectCast(e.Row.FindControl("lnkDelete"), HtmlAnchor)

            If DataBinder.Eval(e.Row.DataItem, "PaidDate") IsNot Nothing And DataBinder.Eval(e.Row.DataItem, "PaidDate").ToString() <> "" Then              
                lnkPay.Visible = False
                lblPayStatus.Text = "Receipt Paid"
                lblPayStatus.Visible = True
                lnkDelete.Visible = False
            Else
                lnkPay.Visible = True
                lnkDelete.Visible = True
            End If
        End If

    End Sub
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If hdnRowID.Value <> "" And Convert.ToInt32(hdnRowID.Value) > 0 Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("Select * from SchoolMaster where MasterPassword=@MasterPassword", "@MasterPassword", txtMasterPass.Text)
            If dr.Read() Then
                Dim pass = dr("MasterPassword")
                If pass = txtMasterPass.Text Then
                    BLL.ExecNonQuery("update ProductReceiptMaster set isdelete=1 where cnt=@cnt", "@cnt", hdnRowID.Value)
                    bind()
                    ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Row deleted successfully');", True)
                    Return
                End If
            End If
        End If
        spanErrorMsg.InnerHtml = "Please enter a valid password!"
        ClientScript.RegisterStartupScript(Page.GetType(), "alert", "showmodal();", True)
        'litmsgReceipt.Text = Notifications.ErrorMessage("Please enter a valid password!")
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If hdnRowID2.Value <> "" And Convert.ToInt32(hdnRowID2.Value) > 0 Then
            Dim res As String = BLL.CancelProductReceipt(txtMasterPass2.Text, hdnRowID2.Value)
            If res.Chars(0) = "#" Then
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Product Receipt cancelled successfully');", True)
                bind()
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('" & res & "');", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Please enter a valid password');", True)
        End If
        'spanErrorMsg.InnerHtml = "Please enter a valid password!"
        'ClientScript.RegisterStartupScript(Page.GetType(), "alert", "showmodal();", True)
        ' litmsgReceipt.Text = Notifications.ErrorMessage("Please enter a valid password!")
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        BLL.ExportToExcel(DataDisplay, "ProductReceiptList.xls")

    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub
    Protected Sub btnbackPaid_Click(sender As Object, e As EventArgs) Handles btnbackPaid.Click
        goToListView()
    End Sub
    Protected Sub goToListView()
        pnlgrid.Visible = True
        pnlsubmit.Visible = False
        hfId.Value = ""
        txtReceiptnoPaid.Text = ""
        txtNetAmount.Text = ""
        txtlatefeesPaid.Text = ""
        txtReceiptAmt.Text = ""
        txtDiscount.Text = ""
        bind()
    End Sub
    Sub activateCashChequeDiv()
        If ddlpmodePaid.Text = "Cash" Then
            divCashAmount.Style.Add("display", "block")
            divchequeDetails.Style.Add("display", "none")
            divChequeAmount.Style.Add("display", "none")
        ElseIf ddlpmodePaid.Text = "Cheque" Then
            divCashAmount.Style.Add("display", "none")
            divchequeDetails.Style.Add("display", "block")
            divChequeAmount.Style.Add("display", "block")
        ElseIf ddlpmodePaid.Text = "Cash+Cheque" Then
            divCashAmount.Style.Add("display", "block")
            divchequeDetails.Style.Add("display", "block")
            divChequeAmount.Style.Add("display", "block")
        Else
            divCashAmount.Style.Add("display", "none")
            divchequeDetails.Style.Add("display", "none")
            divChequeAmount.Style.Add("display", "none")
        End If
    End Sub
End Class

