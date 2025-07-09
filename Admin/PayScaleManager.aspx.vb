Imports System.Web.Services

Partial Class Admin_PayScaleManager
    Inherits System.Web.UI.Page
    Public dataTable As DataTable
    Dim datatableJson As New JsonToDataTable
    Dim BLL As New BusinessLogicLayer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
            hdnRowID.Value = 0
            BindDA()
            BindHRA()
            BindESI()
            BindOA()
            BindPF()
            BindTDS()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("Select * from vwPayScaleMaster order by PSID desc")
        btnExport.Visible = False
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                btnExport.Visible = True
            End If
        End If

        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
   
    'Protected Sub ddlsearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsearch.SelectedIndexChanged
    '    Dim dr As SqlDataReader = BLL.ExecDataReaderProc("Prc_GetEmployeePayScaleMaster", "@EmployeeId", ddlsearch.SelectedValue)
    '    If dr.Read() Then
    '        btnSubmit.Visible = True
    '        hfid.Value = dr("PSID").ToString()
    '        txtPayScaleName.Value = dr("PayScaleName").ToString()
    '        txtBasicSalary.Value = dr("BasicSalary").ToString()

    '        Dim DAType As String = dr("DAType").ToString()
    '        Dim HRAType As String = dr("HRAType").ToString()
    '        Dim OAType As String = dr("OAType").ToString()
    '        Dim PFType As String = dr("PFType").ToString()
    '        Dim TDSType As String = dr("TDSType").ToString()
    '        Dim ESIType As String = dr("ESIType").ToString()

    '        ddlDA.SelectedValue = DAType.ToUpper()
    '        ddlHRA.SelectedValue = HRAType.ToUpper()
    '        ddlPF.SelectedValue = PFType.ToUpper()
    '        ddlTDS.SelectedValue = TDSType.ToUpper()
    '        ddlESI.SelectedValue = ESIType.ToUpper()
    '        ddlOtherAllo.SelectedValue = OAType.ToUpper()

    '        txtDAFix.Value = dr("DAFix").ToString()
    '        txtDAPer.Value = dr("DABasicPer").ToString()
    '        txtAdvance.Value = dr("Advance").ToString()
    '        txtConvenceCharge.Value = dr("ConvenceCharge").ToString()
    '        txtHRAFix.Value = dr("HRAFix").ToString()
    '        txtHRAPer.Value = dr("HRABasicPer").ToString()
    '        txtESIFix.Value = dr("ESIFix").ToString()
    '        txtESIPerBasic.Value = dr("ESIBasicPer").ToString()
    '        txtESIPerOD.Value = dr("ESIODPer").ToString()
    '        txtODFix.Value = dr("OtherDeduction").ToString()
    '        txtOthAlloFix.Value = dr("OAFix").ToString()
    '        txtOthAlloPer.Value = dr("OABasicPer").ToString()
    '        txtPaidLeaves.Value = dr("PaidLeaves").ToString()
    '        txtPFFix.Value = dr("PFFix").ToString()
    '        txtPFPerBasic.Value = dr("PFBasicPer").ToString()
    '        txtPFPerDA.Value = dr("PFDAPer").ToString()
    '        txtTDSFix.Value = dr("TDSFix").ToString()
    '        txtTDSPer.Value = dr("TDSBasicPer").ToString()

    '        BindDA()
    '        BindHRA()
    '        BindESI()
    '        BindOA()
    '        BindPF()
    '        BindTDS()
    '    Else
    '        litmsg.Text = Notifications.ErrorMessage("Please assign Pay Scale to this Employee.")
    '        btnSubmit.Visible = False
    '    End If
    'End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try

            Dim res As String = ""
            Dim DAPer As Decimal = 0
            Dim DAFix As Decimal = 0
            If Not Decimal.TryParse(txtDAFix.Value, DAFix) Then
                DAFix = 0
            End If
            If Not Decimal.TryParse(txtDAPer.Value, DAPer) Then
                DAPer = 0
            End If
            Dim HRAPer As Decimal = 0
            Dim HRAFix As Decimal = 0
            If Not Decimal.TryParse(txtHRAFix.Value, HRAFix) Then
                HRAFix = 0
            End If
            If Not Decimal.TryParse(txtHRAPer.Value, HRAPer) Then
                HRAPer = 0
            End If
            Dim OAPer As Decimal = 0
            Dim OAFix As Decimal = 0
            If Not Decimal.TryParse(txtOthAlloFix.Value, OAFix) Then
                OAFix = 0
            End If
            If Not Decimal.TryParse(txtOthAlloPer.Value, OAPer) Then
                OAPer = 0
            End If
            Dim PFPerBasic As Decimal = 0
            Dim PFPerDA As Decimal = 0
            Dim PFFix As Decimal = 0
            If Not Decimal.TryParse(txtPFFix.Value, PFFix) Then
                PFFix = 0
            End If
            If Not Decimal.TryParse(txtPFPerBasic.Value, PFPerBasic) Then
                PFPerBasic = 0
            End If
            If Not Decimal.TryParse(txtPFPerDA.Value, PFPerDA) Then
                PFPerDA = 0
            End If
            Dim TDSPer As Decimal = 0
            Dim TDSFix As Decimal = 0
            If Not Decimal.TryParse(txtTDSFix.Value, TDSFix) Then
                TDSFix = 0
            End If
            If Not Decimal.TryParse(txtTDSPer.Value, TDSPer) Then
                TDSPer = 0
            End If
            Dim ODPer As Decimal = 0
            Dim ODFix As Decimal = 0
            If Not Decimal.TryParse(txtODFix.Value, ODFix) Then
                ODFix = 0
            End If
            If Not Decimal.TryParse(txtODPer.Value, ODPer) Then
                ODPer = 0
            End If
            Dim ESIPerBasic As Decimal = 0
            Dim ESIPerOD As Decimal = 0
            Dim ESIFix As Decimal = 0
            If Not Decimal.TryParse(txtESIFix.Value, ESIFix) Then
                ESIFix = 0
            End If
            If Not Decimal.TryParse(txtESIPerBasic.Value, ESIPerBasic) Then
                ESIPerBasic = 0
            End If
            If Not Decimal.TryParse(txtESIPerOD.Value, ESIPerOD) Then
                ESIPerOD = 0
            End If
            Dim PaidLeaves As Decimal = 0
            Dim ConvenceCharge As Decimal = 0
            Dim Advance As Decimal = 0
            If Not Decimal.TryParse(txtPaidLeaves.Value, PaidLeaves) Then
                PaidLeaves = 0
            End If

            res = BLL.AddUpdatePayScaleMaster(hdnRowID.Value, txtPayScaleName.Value, ddlDA.SelectedValue, DAPer, DAFix, ddlHRA.SelectedValue, HRAPer, HRAFix, _
                    ddlOtherAllo.SelectedValue, OAPer, OAFix, ddlPF.SelectedValue, PFPerBasic, PFPerDA, PFFix, ddlTDS.SelectedValue, TDSPer, TDSFix, _
                    ddlOD.Value, ODPer, ODFix, ddlESI.SelectedValue, ESIPerBasic, ESIPerOD, ESIFix, PaidLeaves)

            If res.Chars(0) = "#" Then
                bind()
                litmsg.Text = Notifications.SuccessMessage(res)
            Else
                litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
            End If
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
    End Sub

    Protected Sub ddlDA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDA.SelectedIndexChanged
        BindDA()
    End Sub
    Protected Sub ddlHRA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHRA.SelectedIndexChanged
        BindHRA()
    End Sub
    Protected Sub ddlOtherAllo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOtherAllo.SelectedIndexChanged
        BindOA()
    End Sub
    Protected Sub ddlTDS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTDS.SelectedIndexChanged
        BindTDS()
    End Sub
    Protected Sub ddlPF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPF.SelectedIndexChanged
        BindPF()
    End Sub
    Protected Sub ddlESI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlESI.SelectedIndexChanged
        BindESI()
    End Sub

    Sub BindDA()
        If ddlDA.SelectedValue = "PER" Then
            txtDAPer.Visible = True
            txtDAFix.Visible = False
            LitDA.Text = "Basic Salary (%)"
        Else
            txtDAPer.Visible = False
            txtDAFix.Visible = True
            LitDA.Text = "Fix Amount"
        End If
    End Sub
    Sub BindHRA()
        If ddlHRA.SelectedValue = "PER" Then
            txtHRAPer.Visible = True
            txtHRAFix.Visible = False
            LitHRA.Text = "Basic Salary (%)"
        Else
            txtHRAPer.Visible = False
            txtHRAFix.Visible = True
            LitHRA.Text = "Fix Amount"
        End If
    End Sub
    Sub BindOA()
        If ddlOtherAllo.SelectedValue = "PER" Then
            txtOthAlloPer.Visible = True
            txtOthAlloFix.Visible = False
            LitOA.Text = "Basic Salary (%)"
        Else
            txtOthAlloPer.Visible = False
            txtOthAlloFix.Visible = True
            LitOA.Text = "Fix Amount"
        End If
    End Sub
    Sub BindTDS()
        If ddlTDS.SelectedValue = "PER" Then
            txtTDSPer.Visible = True
            txtTDSFix.Visible = False
            LitTDS.Text = "Basic Salary (%)"
        Else
            txtTDSPer.Visible = False
            txtTDSFix.Visible = True
            LitTDS.Text = "Fix Amount"
        End If
    End Sub
    Sub BindPF()
        If ddlPF.SelectedValue = "PER" Then
            txtPFPerBasic.Visible = True
            txtPFPerDA.Visible = True
            txtPFFix.Visible = False
            LitPF.Text = "Basic Salary (%)<br /><br /><br />DA (%)"
        Else
            txtPFPerBasic.Visible = False
            txtPFPerDA.Visible = False
            txtPFFix.Visible = True
            LitPF.Text = "Fix Amount"
        End If
    End Sub
    Sub BindESI()
        If ddlESI.SelectedValue = "PER" Then
            txtESIPerBasic.Visible = True
            txtESIPerOD.Visible = True
            txtESIFix.Visible = False
            LitESI.Text = "Basic Salary (%)<br /><br /><br />OD (%)"
        Else
            txtESIPerBasic.Visible = False
            txtESIPerOD.Visible = False
            txtESIFix.Visible = True
            LitESI.Text = "Fix Amount"
        End If
    End Sub
    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If hdnRowID.Value <> "" Then
            Dim MasterPass As String = BLL.ExecScalar("Select Top 1 MasterPassword from schoolmaster")
            If MasterPass = txtMasterPass.Text Then
                'Response.Redirect("Studentregistration.aspx?Sid=" & hdnRowID.Value)
                'bind()
                Dim dr As SqlDataReader = BLL.ExecDataReader("Select * from PayScaleMaster Where PSID=@PSID", "@PSID", hdnRowID.Value)
                If dr.Read() Then
                    btnSubmit.Visible = True
                    txtPayScaleName.Value = dr("PayScaleName").ToString()

                    Dim DAType As String = dr("DAType").ToString()
                    Dim HRAType As String = dr("HRAType").ToString()
                    Dim OAType As String = dr("OAType").ToString()
                    Dim PFType As String = dr("PFType").ToString()
                    Dim TDSType As String = dr("TDSType").ToString()
                    Dim ESIType As String = dr("ESIType").ToString()

                    ddlDA.SelectedValue = DAType.ToUpper()
                    ddlHRA.SelectedValue = HRAType.ToUpper()
                    ddlPF.SelectedValue = PFType.ToUpper()
                    ddlTDS.SelectedValue = TDSType.ToUpper()
                    ddlESI.SelectedValue = ESIType.ToUpper()
                    ddlOtherAllo.SelectedValue = OAType.ToUpper()

                    txtDAFix.Value = dr("DAFix").ToString()
                    txtDAPer.Value = dr("DABasicPer").ToString()
                    txtHRAFix.Value = dr("HRAFix").ToString()
                    txtHRAPer.Value = dr("HRABasicPer").ToString()
                    txtESIFix.Value = dr("ESIFix").ToString()
                    txtESIPerBasic.Value = dr("ESIBasicPer").ToString()
                    txtESIPerOD.Value = dr("ESIODPer").ToString()
                    txtODFix.Value = dr("OtherDeduction").ToString()
                    txtOthAlloFix.Value = dr("OAFix").ToString()
                    txtOthAlloPer.Value = dr("OABasicPer").ToString()
                    txtPaidLeaves.Value = dr("PaidLeaves").ToString()
                    txtPFFix.Value = dr("PFFix").ToString()
                    txtPFPerBasic.Value = dr("PFBasicPer").ToString()
                    txtPFPerDA.Value = dr("PFDAPer").ToString()
                    txtTDSFix.Value = dr("TDSFix").ToString()
                    txtTDSPer.Value = dr("TDSBasicPer").ToString()

                    BindDA()
                    BindHRA()
                    BindESI()
                    BindOA()
                    BindPF()
                    BindTDS()
                End If
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Please enter a valid Password');", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('invalid details');", True)
        End If
    End Sub
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If hdnRowID.Value <> "" Then
            Dim MasterPass As String = BLL.ExecScalar("Select Top 1 MasterPassword from schoolmaster")
            If MasterPass = txtMasterPass2.Text Then
                BLL.ExecNonQuery("Update PayScaleMaster Set Deactivated=dbo.getdate() Where PSID=@PSID", "@PSID", hdnRowID.Value)
                bind()
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Pay Scale Deleted Successfully');", True)
            Else
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Please enter a valid Password');", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('invalid details');", True)
        End If
    End Sub
End Class
