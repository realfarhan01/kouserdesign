
Partial Class StuSMS
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()

        End If
    End Sub


    Sub BindClass()
        ddlClass.DataSource = BLL.BindClass()

        ddlClass.DataTextField = "ClassName"
        ddlClass.DataValueField = "ClassId"
        ddlClass.DataBind()
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

    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        bind()
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from studentmaster where classid=@classid", "@classid", ddlClass.SelectedValue)
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

    Protected Sub btnSms_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSms.Click
        Dim chk As CheckBox
        For Each mRow As GridViewRow In DataDisplay.Rows
            chk = CType(mRow.FindControl("chkmsg"), CheckBox)
            If chk.Checked Then
                SendSmsToMobile.SendSms(mRow.Cells(3).Text, txtDetail.Text)
            End If
        Next
    End Sub
End Class
