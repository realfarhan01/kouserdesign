
Partial Class Admin_ReceiptGenerateProspectus
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
            If Not Request.QueryString("rid") Is Nothing Then
                Dim cnt As Integer
                cnt = (New Encryption64).Decrypt(Request.QueryString("rid"))
                Dim dt As DataTable = BLL.ExecDataTableProc("Get_ReceiptProspectus", "@Cnt", cnt)
                If dt.Rows.Count > 0 Then
                    ddlClass.SelectedValue = dt.Rows(0)("MainClassId")
                    txtReceiptno.Text = dt.Rows(0)("SessionReceiptNo")
                    ddlSession.SelectedValue = dt.Rows(0)("SchoolSession")
                    txtFormNo.Text = dt.Rows(0)("FormNo")
                    txtDate.Text = dt.Rows(0)("ReceiptDate")
                    txtName.Text = dt.Rows(0)("StudentName")
                    txtLastName.Text = dt.Rows(0)("StudentLastName")
                    txtFatherName.Text = dt.Rows(0)("FatherName")
                    txtFLastName.Text = dt.Rows(0)("FatherLastName")
                    txtMobileNo.Text = dt.Rows(0)("Mobile")
                    txtAmount.Text = dt.Rows(0)("Amount")
                    txtRemark.Text = dt.Rows(0)("Remark")
                    txtReceiptno.ReadOnly = True
                End If
            Else
                txtReceiptno.Text = BLL.ExecScalar("Select dbo.GetNewSessionReceiptNoProspectus(@SchoolSession)", "@SchoolSession", ddlSession.SelectedValue)
                Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
                txtDate.Text = todaysdate
            End If
        End If
    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindMainClass()

        ddlClass.DataTextField = "MainClassName"
        ddlClass.DataValueField = "MainClassId"
        ddlClass.DataBind()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try

            Dim res As String = ""
            res = BLL.ProspectusSaleSession(txtReceiptno.Text, txtFormNo.Text, txtDate.Text, txtName.Text, txtLastName.Text, txtFatherName.Text, txtFLastName.Text, txtMobileNo.Text, ddlClass.SelectedValue, Val(txtAmount.Text), txtRemark.Text, ddlSession.SelectedValue)
            If res.Chars(0) = "#" Then
                Dim arr As String() = res.Split("~")
                If (arr.Length > 1) Then
                    hdnReceiptId.Value = arr(2)
                    btnPreview.Visible = True
                End If
                litmsg.Text = Notifications.SuccessMessage(arr(1))
                btnSubmit.Enabled = False
                ClientScript.RegisterStartupScript(Page.[GetType](), "alert", "alert('" + (arr(1)) + "');", True)
                'Response.Redirect("FeeSubmit.aspx")
            Else
                litmsg.Text = Notifications.ErrorMessage(res)
            End If
            'End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Error in Process. Please Try Later.")
        End Try

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim sbvaild As String = (New BusinessLogicLayer).DisabledButtonCode("UserRegistration") & Page.ClientScript.GetPostBackEventReference(btnSubmit, Nothing) & ";"
        btnSubmit.Attributes.Add("onclick", sbvaild)

    End Sub


    Private Function ItemIndexOfID(ByVal RowId As Integer, ByVal dt As DataTable) As Integer
        Dim index As Integer = 0
        For Each mrow As DataRow In dt.Rows
            If (mrow("RowId").ToString() = RowId) Then
                Return index
            End If
            index = (index + 1)
        Next
        Return -1
    End Function
    Protected Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Response.Redirect("ReceiptProspectus.aspx?rid=" + (New Encryption64).Encrypt(hdnReceiptId.Value))
    End Sub

    Protected Sub ddlSession_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSession.SelectedIndexChanged

        txtReceiptno.Text = BLL.ExecScalar("Select dbo.GetNewSessionReceiptNoProspectus(@SchoolSession)", "@SchoolSession", ddlSession.SelectedValue)
    End Sub
End Class
