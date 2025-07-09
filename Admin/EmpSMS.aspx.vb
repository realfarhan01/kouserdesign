
Partial Class EmpSMS
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            bind()
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

   
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from Employeemaster")
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
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "alert(Message sent Successfully !)")
    End Sub
End Class
