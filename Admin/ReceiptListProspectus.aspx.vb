
Partial Class Admin_ReceiptListProspectus
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindMainClass()
            bind()
            ltrSchool.Text = BLL.BindSchoolHeader()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.Get_ReceiptProspectusList(txtFromDate.Text, txtToDate.Text, ddlClassFrom.SelectedValue, ddlClassTo.SelectedValue, txtReceiptNo.Text, txtFormNo.Text, txtStudentName.Text, txtFathername.Text, "", 0, ddlSession.SelectedValue)
        If dt.Rows.Count > 0 Then
            DataDisplay.DataSource = dt
            DataDisplay.DataBind()
            reportheader.Visible = True
        Else
            reportheader.Visible = False
            DataDisplay.DataSource = Nothing
            DataDisplay.DataBind()

        End If

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "view1" Then
            Response.Redirect("ReceiptProspectus.aspx?rid=" + (New Encryption64).Encrypt(e.CommandArgument))
        ElseIf e.CommandName = "edit1" Then
            Response.Redirect("ReceiptGenerateProspectus.aspx?rid=" + (New Encryption64).Encrypt(e.CommandArgument))
        ElseIf e.CommandName = "delete1" Then
            BLL.ExecNonQuery("update tbl_ProspectusSale Set DaleteDate=dbo.getdate() Where Cnt=@Cnt", "@Cnt", e.CommandArgument)
            bind()
        End If
    End Sub
    Sub BindMainClass()
        ddlClassFrom.DataSource = BLL.BindMainClass()
        ddlClassFrom.DataTextField = "MainClassName"
        ddlClassFrom.DataValueField = "MainClassId"
        ddlClassFrom.DataBind()
        ddlClassTo.DataSource = BLL.BindMainClass()
        ddlClassTo.DataTextField = "MainClassName"
        ddlClassTo.DataValueField = "MainClassId"
        ddlClassTo.DataBind()
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As System.EventArgs) Handles btnsearch.Click
        bind()
    End Sub

    'Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
    '    'BLL.ExportToExcel(DataDisplay, "ProspectusReceiptList.xls")
    '    Dim DTable As New DataTable
    '    Dim dgGrid As New GridView
    '    DTable = BLL.Get_ReceiptProspectusList("", "", ddlClass.SelectedValue, txtReceiptNo.Text, txtFormNo.Text, txtStudentName.Text, txtFathername.Text, "", 1)
    '    dgGrid.DataSource = DTable
    '    dgGrid.DataBind()
    '    BLL.ExportToExcel(dgGrid, "ProspectusReceiptList.xls")
    'End Sub
End Class
