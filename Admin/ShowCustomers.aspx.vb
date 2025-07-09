
Partial Class Admin_ShowCustomers
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindStudentData()
            BindSession()
            txtTo.Text = ""
            bind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.GetCustomers(Session("CompanyId"), txtCustomerId.Text, txtCustomerName.Text, txtFrom.Text, txtTo.Text, "", ddlSession.SelectedValue)
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
    Sub BindSession()
        ddlSession.DataSource = BLL.BindSessions()
        ddlSession.DataTextField = "SessionName"
        ddlSession.DataValueField = "SessionName"
        ddlSession.DataBind()
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        bind()
    End Sub


    Protected Sub DataDisplay_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DataDisplay.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If hdnRowID.Value <> "" Then
            Dim MasterPass As String = BLL.ExecScalar("Select Top 1 MasterPassword from schoolmaster")
            If MasterPass = txtMasterPass.Text Then
                Response.Redirect("CustomerRegistration.aspx?cid=" & hdnRowID.Value)
                'ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Receipt deleted successfully');", True)
                bind()
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Please enter a valid Password');", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('invalid details');", True)
        End If
        'spanErrorMsg.InnerHtml = "Please enter a valid password!"
        'ClientScript.RegisterStartupScript(Page.GetType(), "alert", "showmodal();", True)
        ' litmsgReceipt.Text = Notifications.ErrorMessage("Please enter a valid password!")
    End Sub
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If hdnRowID.Value <> "" Then
            Dim MasterPass As String = BLL.ExecScalar("Select Top 1 MasterPassword from schoolmaster")
            If MasterPass = txtMasterPass2.Text Then
                BLL.ExecNonQueryProc("Prc_DeleteCustomer", "@CustomerId", hdnRowID.Value)
                bind()
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Customer Deleted Successfully');", True)
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Please enter a valid Password');", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('invalid details');", True)
        End If
    End Sub

    Sub BindStudentData()
        ddlsearch.DataSource = BLL.ExecDataTableProc("Prc_CustomerData")
        ddlsearch.DataTextField = "CustomerData"
        ddlsearch.DataValueField = "CustomerId"
        ddlsearch.DataBind()
    End Sub

    Protected Sub ddlsearch_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlsearch.SelectedIndexChanged
        txtCustomerId.Text = ddlsearch.SelectedValue
        bind()
    End Sub
End Class
