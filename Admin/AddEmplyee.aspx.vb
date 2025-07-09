
Partial Class Admin_AddEmplyee
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindLocality()
            BindState()
            bindDesignation()
            BindPayScale()
            If Not Request.QueryString("Eid") Is Nothing Then
                Dim dt As DataTable = BLL.GetEmployeeList(Request.QueryString("Eid"), "", "")
                If dt.Rows.Count > 0 Then
                    txtEmployeeCode.Text = Request.QueryString("Eid")
                    txtEmployeeCode.ReadOnly = True
                    txtname.Text = dt.Rows(0)("EmployeeName")
                    txtFname.Text = dt.Rows(0)("fatherName")
                    txtMname.Text = dt.Rows(0)("mothername")
                    txtaddress.Text = dt.Rows(0)("Address")
                    txtContactno.Text = dt.Rows(0)("Contactno")
                    txtEmailid.Text = dt.Rows(0)("Emailid")
                    ddlState.SelectedValue = dt.Rows(0)("State")
                    BindCity(ddlState.SelectedValue)
                    ddlCity.SelectedValue = dt.Rows(0)("City")
                    ddlNationality.SelectedValue = dt.Rows(0)("Nationality")
                    ddlLocality.SelectedValue = dt.Rows(0)("LocalityId")
                    ddlactive.SelectedValue = dt.Rows(0)("Isblock")
                    ddldesignation.SelectedValue = dt.Rows(0)("designation")
                    ddlDate.Text = dt.Rows(0)("Department").ToString()
                    ddlMonth.Text = dt.Rows(0)("dobMM").ToString()
                    ddlYear.Text = dt.Rows(0)("dobYYYY").ToString()
                    hfid.Value = Request.QueryString("Eid").ToString()
                    ddlDepartment.SelectedValue = dt.Rows(0)("Department").ToString()
                    ddlEmpType.SelectedValue = dt.Rows(0)("EmpType").ToString()
                    txtbank.Text = dt.Rows(0)("Bank").ToString()
                    txtPan.Text = dt.Rows(0)("Pan").ToString()
                    'txtESI.Text = dt.Rows(0)("ESI").ToString()
                    'txtPF.Text = dt.Rows(0)("PF").ToString()
                    ddlpayScale.SelectedValue = dt.Rows(0)("PSID").ToString()
                    txtSalary.Text = dt.Rows(0)("BasicSalary").ToString()
                    txtJoinDate.Text = dt.Rows(0)("JoinDate").ToString()
                    txtConfirm.Text = dt.Rows(0)("Confirm").ToString()
                    'txtIncrement.Text = dt.Rows(0)("Increment").ToString()
                    'txtLeaving.Text = dt.Rows(0)("Leaving").ToString()
                    txtFileNo.Text = dt.Rows(0)("FileNo").ToString()
                    txtRetirnment.Text = dt.Rows(0)("Retirnment").ToString()
                    txtQualification.Text = dt.Rows(0)("Qualification").ToString()
                    'txtSubject.Text = dt.Rows(0)("Subject").ToString()
                    'txtNextIncr.Text = dt.Rows(0)("NextIncr").ToString()
                    ddlCategory.SelectedValue = dt.Rows(0)("Category").ToString()
                    txtProvisionDate.Text = dt.Rows(0)("ProvisionDate").ToString()
                    txtContractExDate.Text = dt.Rows(0)("ContractExDate").ToString()
                    txtPFStratDate.Text = dt.Rows(0)("PFStratDate").ToString()
                    txtGrade.Text = dt.Rows(0)("Grade").ToString()
                    txtMaritalStatus.Text = dt.Rows(0)("MaritalStatus").ToString()
                    ddlblood.SelectedValue = dt.Rows(0)("Blood").ToString()
                    txtAdhar.Text = dt.Rows(0)("Adhar").ToString()
                    txtDrivLicnce.Text = dt.Rows(0)("DrivLicnce").ToString()
                    txtElectionCard.Text = dt.Rows(0)("ElectionCard").ToString()
                    txtRationCard.Text = dt.Rows(0)("RationCard").ToString()
                Else
                    litmsg.Text = Notifications.ErrorMessage("No Data Found")
                End If
            Else
                hfid.Value = BLL.ExecScalarProc("Prc_GetNewEmployeeId")
                txtEmployeeCode.Text = hfid.Value
            End If
        End If
    End Sub

    Sub BindLocality()
        ddlLocality.DataSource = BLL.BindLocality()

        ddlLocality.DataTextField = "Locality"
        ddlLocality.DataValueField = "LocalityId"
        ddlLocality.DataBind()
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
    Sub BindPayScale()
        ddlpayScale.DataSource = BLL.BindPayScale()
        ddlpayScale.DataTextField = "PayScaleName"
        ddlpayScale.DataValueField = "PSId"
        ddlpayScale.DataBind()
    End Sub
    Sub bindDesignation()
        ddldesignation.DataSource = BLL.BindDesignation()

        ddldesignation.DataTextField = "Designation"
        ddldesignation.DataValueField = "Designation"
        ddldesignation.DataBind()
    End Sub
    Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlState.SelectedIndexChanged
        BindCity(ddlState.SelectedValue)
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try


            Dim res As String = ""
            Dim dob As String = ddlDate.SelectedValue.ToString() & "/" & ddlMonth.SelectedValue.ToString() & "/" & ddlYear.SelectedValue.ToString()
            res = BLL.AddEmployee(hfid.Value, txtname.Text, txtFname.Text, txtMname.Text, dob, ddlNationality.SelectedValue, ddlGender.SelectedValue, txtaddress.Text, ddlCity.SelectedValue, ddlState.Text, ddlLocality.SelectedValue, txtContactno.Text, txtEmailid.Text, ddlactive.SelectedValue, ddldesignation.SelectedValue, ddlDepartment.SelectedValue, ddlEmpType.SelectedValue, txtbank.Text, txtPan.Text, "", "", ddlpayScale.SelectedValue, txtSalary.Text, txtJoinDate.Text, txtConfirm.Text, "", "", txtFileNo.Text, txtRetirnment.Text, txtQualification.Text, "", "", ddlCategory.SelectedValue, txtProvisionDate.Text, txtContractExDate.Text, txtPFStratDate.Text, txtGrade.Text, txtMaritalStatus.Text, ddlblood.SelectedValue, txtAdhar.Text, txtDrivLicnce.Text, txtElectionCard.Text, txtRationCard.Text)


            If res.Chars(0) = "#" Then
                'Dim id As String = res.Split(":")(1)
                'Response.Redirect("StudentRegistration.aspx?Pid=" & id)
                litmsg.Text = Notifications.SuccessMessage(res)
            Else
                litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
            End If
            'If hfid.Value = "" Then
            'Else
            '    res = BLL.AddEmployee(hfid.Value, txtname.Text, txtFname.Text, txtMname.Text, dob, ddlNationality.SelectedValue, ddlGender.SelectedValue, txtaddress.Text, ddlCity.SelectedValue, ddlState.Text, ddlLocality.SelectedValue, txtContactno.Text, txtEmailid.Text, ddlactive.SelectedValue, ddldesignation.SelectedValue, ddlDepartment.SelectedValue, ddlEmpType.SelectedValue, txtbank.Text, txtPan.Text, txtESI.Text, txtPF.Text, ddlpayScale.SelectedValue, txtSalary.Text, txtJoinDate.Text, txtConfirm.Text, txtIncrement.Text, txtLeaving.Text, txtFileNo.Text, txtRetirnment.Text, txtQualification.Text, txtSubject.Text, txtNextIncr.Text, ddlCategory.SelectedValue, txtProvisionDate.Text, txtContractExDate.Text, txtPFStratDate.Text, txtGrade.Text, txtMaritalStatus.Text, ddlblood.SelectedValue, txtAdhar.Text, txtDrivLicnce.Text, txtElectionCard.Text, txtRationCard.Text)

            '    If res.Chars(0) = "#" Then
            '        litmsg.Text = Notifications.SuccessMessage(res)
            '        hfid.Value = ""
            '    End If

            'End If
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
    End Sub


End Class
