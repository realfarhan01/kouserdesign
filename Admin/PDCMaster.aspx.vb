
Partial Class Admin_PDCMaster
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            hfId.Value = 0
            BindStudentData()
            BindPickupLocation()
            bindclass()
        End If
    End Sub
    Sub bindclass()
        ddlclass.DataSource = BLL.ExecDataTable("select * from MainClassMaster")
        ddlclass.DataTextField = "MainClassName"
        ddlclass.DataValueField = "MainClassId"
        ddlclass.DataBind()
    End Sub
    Sub BindPickupLocation()
        ddlPickupLocation.DataSource = BLL.BindPickupLocation()
        ddlPickupLocation.DataTextField = "PickupPoint"
        ddlPickupLocation.DataValueField = "PickupPointId"
        ddlPickupLocation.DataBind()
    End Sub
    Protected Sub ddlTrasport_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTrasport.SelectedIndexChanged
        If ddlTrasport.SelectedValue = "1" Then
            ddlPickupLocation.Enabled = True
        Else
            ddlPickupLocation.Enabled = False
        End If
        'ClientScript.RegisterStartupScript(Page.GetType(), "alert1", "showConvPopup();", True)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert1", "showConvPopup();", True)
    End Sub

    'Dim TotalInstallmentAmount As Decimal = 0
    Sub BindStudentData()
        ddlsearch.DataSource = BLL.ExecDataTableProc("Prc_StudentData")
        ddlsearch.DataTextField = "StudentData"
        ddlsearch.DataValueField = "StudentId"
        ddlsearch.DataBind()
    End Sub
    Protected Sub txtStudentId_TextChanged(sender As Object, e As System.EventArgs) Handles txtStudentId.TextChanged
        ShowStudentData(txtStudentId.Text)
        'Try

        '    Dim dr As SqlDataReader = BLL.ExecDataReader("Select s.StudentName,s.MainClassId,c.MainClassName from StudentMaster S left join Mainclassmaster c on s.MainClassId=c.MainClassId Where s.StudentId=@StudentId", "@StudentId", txtStudentId.Text)
        '    If dr.Read() Then
        '        txtName.Text = dr("StudentName")
        '        txtClass.Text = dr("MainClassName")
        '        'hfId.Value = dr("MainClassId")
        '    End If

        '    bindPDCBilling()
        '    'lnkAddConveyance.Visible = False

        'Catch ex As Exception

        'End Try
    End Sub
    Protected Sub ddlsearch_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlsearch.SelectedIndexChanged
        If ddlsearch.SelectedValue <> "" Then
            Dim studentid As String = ddlsearch.SelectedValue
            ShowStudentData(studentid)
        End If
    End Sub
    Sub ShowStudentData(ByVal studentid As String)
        Try

            'If ddlsearch.SelectedValue <> "" Then
            '    Dim studentid As String = ddlsearch.SelectedValue
            Dim dr As SqlDataReader = BLL.ExecDataReader("Select s.StudentId,s.StudentName,s.MainClassId,c.MainClassName,PickupPoint,isTransport,isAdmissionFeesFree,isTuitionFeesFree,s.TuitionFeeConcession from StudentMaster S left join Mainclassmaster c on s.MainClassId=c.MainClassId Where s.StudentId=@StudentId", "@StudentId", studentid)
            If dr.Read() Then
                txtStudentId.Text = dr("studentid")
                txtName.Text = dr("StudentName")
                txtClass.Text = dr("MAinClassName")
                hfId.Value = dr("MainClassId")
                ddlClass.SelectedValue = hfId.Value

                ddlTrasport.Text = dr("isTransport")
                If ddlTrasport.Text = "1" Then
                    ddlPickupLocation.Enabled = True
                Else
                    ddlPickupLocation.Enabled = False
                End If
                Dim TuitionFeeConcession As Decimal = Val(dr("TuitionFeeConcession"))
                txtConcession.Text = TuitionFeeConcession
                'If TuitionFeeConcession > 0 Then
                '    txtConcession.ReadOnly = True
                '    btnSubmit.Visible = False
                'End If

                ddlPickupLocation.Text = dr("PickupPoint")
                ddlisAdmissionFeesFree.Text = dr("isAdmissionFeesFree")
                ddlisTuitionFeesFree.Text = dr("isTuitionFeesFree")
            End If

            bindPDCBilling()

            'End If
        Catch ex As Exception

        End Try
    End Sub
    Sub bindPDCBilling()
        Dim dt As DataTable = New DataTable()
        If txtStudentId.Text <> "" Then
            dt = BLL.Get_UnpaidPDCBillingByStudent(txtStudentId.Text.Trim())
            'If dt IsNot Nothing Then
            '    For Each mrow As DataRow In dt.Rows
            '        hfId.Value = hfId.Value + 1
            '        Dim FeeType As String = mrow("FeeType").ToString()
            '        Dim PDCId As String = Convert.ToInt32(mrow("PDCId"))
            '        workTable.Rows.Add(New Object() {hfId.Value, FeeType, 0, Convert.ToDecimal(mrow("Amount")), Convert.ToDecimal(mrow("lateFee")), PDCId})

            '        ViewState("tblFee") = workTable
            '    Next
            'End If
            DataDisplay.DataSource = dt
            DataDisplay.DataBind()

        End If
    End Sub
    Protected Sub DataDisplay_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DataDisplay.RowDeleting
        Dim dt As DataTable = New DataTable()
        Dim id As Integer = Convert.ToInt32(DataDisplay.DataKeys(e.RowIndex).Values(0).ToString())
        If id > 0 Then
            'dt = BLL.Delete_PDCBillingByPDCId(id, txtMasterPass.Text)
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        bindPDCBilling()
            '        Return
            '    End If
            'End If
        End If
        litmsg.Text = Notifications.ErrorMessage("Record could not deleted, please try again later!")
    End Sub

    'Protected Sub btnAddPDC_Click(sender As Object, e As EventArgs) Handles btnAddPDC.Click
    '    Dim dt As DataTable = New DataTable()
    '    dt = BLL.Update_PDCBillingMaster(ddlsearch.Text, hfId.Value, Convert.ToInt32(ddlisAdmissionFeesFree.Text), Convert.ToInt32(ddlisTuitionFeesFree.Text), 0, 0, "updateTuition")
    '    bindPDCBilling()
    '    '      
    'End Sub

    'Protected Sub btnAddConveyance_Click(sender As Object, e As EventArgs) Handles btnAddConveyance.Click
    '    Dim dt As DataTable = New DataTable()
    '    dt = BLL.Update_PDCBillingMaster(txtStudentId.Text, 0, Convert.ToInt32(ddlisAdmissionFeesFree.Text), Convert.ToInt32(ddlisTuitionFeesFree.Text), ddlTrasport.Text, ddlPickupLocation.Text, "updateConveyance")
    '    bindPDCBilling()
    'End Sub

    Protected Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        Dim action As String = hidType.Value
        If action = "delete" Then
            If hdnRowID.Value <> "" And Convert.ToInt32(hdnRowID.Value) > 0 Then
                Dim res As String = BLL.Delete_PDCBillingByPDCId(hdnRowID.Value, txtMasterPass.Text)
                If res.Chars(0) = "#" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('PDC deleted successfully');", True)
                    bindPDCBilling()
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" & res & "');", True)
                End If
            Else                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please enter a valid password');showAuthPopup();", True)
            End If
        Else
            Dim res As String = BLL.Update_PDCBillingMaster(txtStudentId.Text, ddlClass.SelectedValue, ddlSession.SelectedValue, Convert.ToInt32(ddlisAdmissionFeesFree.Text), Convert.ToInt32(ddlisTuitionFeesFree.Text), ddlTrasport.Text, ddlPickupLocation.Text, action, txtMasterPass.Text)
            If res.Chars(0) = "#" Then
                bindPDCBilling()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert3", "alert('Fee Added Successfully');", True)

            Else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert2", "alert('Please enter a valid password');", True)
                If action.ToLower() = "updatetuition" Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert3", "showPDCPopup();", True)
                Else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert3", "showConvPopup();", True)
                End If
            End If
        End If
    
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        If Val(txtConcession.Text) > 0 And txtConcession.Text <> "" Then
            BLL.ExecNonQueryProc("Prc_UpdateTuitionFeeConcession", "@StudentId", txtStudentId.Text.Trim(), "@TuitionFeeConcession", Val(txtConcession.Text))
            If Val(txtConcession.Text) > 0 Then
                txtConcession.ReadOnly = True
                btnSubmit.Visible = False
            End If
            bindPDCBilling()

        End If
    End Sub
End Class
