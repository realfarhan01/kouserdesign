
Partial Class Admin_CustomerRegistration
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindState()
            ddlState.SelectedValue = "Rajasthan"
            BindCity(ddlState.SelectedValue)
            ddlCity.SelectedValue = "JAIPUR"
            BindSessions()


            'txtRegNo.Text = BLL.ExecScalarProc("GetNewCustomerId")
            'txtRegNo.ReadOnly = True

            hfId.Value = "0"
            Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            txtRegdate.Text = todaysdate

            If Not Request.QueryString("cid") Is Nothing Then
                Dim dt1 As DataTable = BLL.GetCustomers(Session("CompanyId"), Request.QueryString("cid"), "", "", "", "", "")
                If dt1.Rows.Count > 0 Then
                    ddlSession.SelectedValue = dt1.Rows(0)("RegSession")
                    txtCustomerId.Text = dt1.Rows(0)("CustomerId")
                    txtaddress.Text = dt1.Rows(0)("address")
                    txtRegdate.Text = dt1.Rows(0)("Regdate")
                    txtName.Text = dt1.Rows(0)("CustomerName")
                    txtDOB.Text = dt1.Rows(0)("DOB")
                    txtEmail.Text = dt1.Rows(0)("Email")
                    txtGST.Text = dt1.Rows(0)("GSTNo")
                    txtMobile.Text = dt1.Rows(0)("Mobile")
                    txtPincode.Text = dt1.Rows(0)("Pincode")
                    ddlCity.SelectedValue = dt1.Rows(0)("City")
                    ddlState.SelectedValue = dt1.Rows(0)("State")
                    If dt1.Rows(0)("Status") = "Active" Then
                        ddlStatus.SelectedValue = 0
                    Else
                        ddlStatus.SelectedValue = 1
                    End If

                Else
                    litmsg.Text = Notifications.ErrorMessage("No Data Found")
                End If
            End If

        End If
    End Sub

    Sub BindState()
        ddlState.DataSource = BLL.BindState()

        ddlState.DataTextField = "State"
        ddlState.DataValueField = "State"
        ddlState.DataBind()
    End Sub
    Sub BindCity(ByVal State As String)
        ddlCity.DataSource = BLL.BindCity(State)

        ddlCity.DataTextField = "City"
        ddlCity.DataValueField = "City"
        ddlCity.DataBind()
    End Sub

    Sub BindSessions()
        ddlSession.DataSource = BLL.BindSessions()

        ddlSession.DataTextField = "SessionName"
        ddlSession.DataValueField = "SessionName"
        ddlSession.DataBind()
    End Sub
    Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlState.SelectedIndexChanged
        BindCity(ddlState.SelectedValue)
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Dim res As String = ""
        Try

            Dim FeeCategory As String = ""
            'Dim SchoolCode As String = BLL.ExecScalar("Select Top 1 SchoolCode from schoolmaster")

            'Dim dob As String = ddlDate.SelectedValue.ToString() & "/" & ddlMonth.SelectedValue.ToString() & "/" & ddlYear.SelectedValue.ToString()
            ' If hfId.Value = "" Then
            'Dim StudentName As String = txtstudent.Text + " " + txtstudentLastName.Text
            Dim DOJ As String = txtRegdate.Text
            '(ByVal SNo As Integer, ByVal CustomerId As String, ByVal CustomerName As String, ByVal Email As String, ByVal Mobile As String, ByVal DOB As String,
            'ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Pincode As String, ByVal GSTNo As String, ByVal RegSession As String, ByVal CompanyId As Integer, ByVal Deactivated As Integer)

            res = BLL.AddUpdateCustomer(txtCustomerId.Text, txtName.Text, txtEmail.Text, txtMobile.Text, txtDOB.Text, txtaddress.Text, ddlCity.SelectedValue, ddlState.SelectedValue, txtPincode.Text, txtGST.Text, ddlSession.SelectedValue, Session("CompanyId"), ddlStatus.SelectedValue)
            If res.Chars(0) = "#" Then
                Dim id As String = res.Split(":")(1)
                'Response.Redirect("StudentRegistration.aspx?Pid=" & id)
                litmsg.Text = Notifications.SuccessMessage(res)
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('" + res + "')", True)

                Response.Redirect("ShowCustomers.aspx")
            Else
                litmsg.Text = Notifications.ErrorMessage(res)
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('" + res + "')", True)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('" + ex.ToString() + "')", True)
        End Try

    End Sub

End Class
