
Partial Class ParentSMS
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
        End If
    End Sub


    'Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
    '    If e.CommandName = "edit1" Then
    '        Dim dr As SqlDataReader = BLL.ExecDataReader("select * from TransUsers  where uniqueid=@uniqueid", "@uniqueid", e.CommandArgument)
    '        If dr.Read() Then
    '            txtEmployeeid.Text = dr("e.CommandArgument")
    '            txtmtype.Text = dr("mtype")
    '            ddlactive.SelectedValue = dr("Deactivated")

    '            ddlVehicleid.SelectedValue = dr("Vehicleid")

    '            hfId.Value = e.CommandArgument

    '        End If

    '    End If
    'End Sub

    Sub BindClass()

        ddlClass.DataSource = BLL.BindMainClass()
        ddlClass.DataTextField = "MainClassName"
        ddlClass.DataValueField = "MainClassId"
        ddlClass.DataBind()

    End Sub
    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        bind(ddlClass.SelectedValue)
    End Sub
    Sub bind(ByVal ClassID As String)
        Dim dt As DataTable = BLL.ExecDataTableProc("Prc_GetSMSContacts", "@MainClassID", ClassID)
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

    Protected Sub btnSms_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSms.Click
        Dim chk As CheckBox
        For Each mRow As GridViewRow In DataDisplay.Rows
            chk = CType(mRow.FindControl("chkmsg"), CheckBox)
            If chk.Checked Then
                SendSmsToMobile.SendGatewaySms(mRow.Cells(3).Text, txtDetail.Text)
                Dim Result As String = BLL.SendSms(mRow.Cells(1).Text, mRow.Cells(3).Text, txtDetail.Text, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
            End If
        Next
    End Sub
End Class
