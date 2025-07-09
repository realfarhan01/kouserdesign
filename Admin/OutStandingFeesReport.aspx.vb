
Partial Class Admin_OutStandingFeesReport
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindStudent()
            BindClass()
            'bind()
        End If
    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindMainClass()

        ddlClass.DataTextField = "MainClassName"
        ddlClass.DataValueField = "MainClassId"
        ddlClass.DataBind()
    End Sub
    Sub bindStudent()
        ddlStudent.DataSource = BLL.GetStudentList("", "", ddlClass.SelectedValue, "", "", 0, "", "", "", "")

        ddlStudent.DataTextField = "Studentname"
        ddlStudent.DataValueField = "StudentId"
        ddlStudent.DataBind()
    End Sub

    'Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClass.SelectedIndexChanged
    '    bindStudent()
    'End Sub
    Sub bind()
        btnExport.Visible = False
        Dim dt As DataTable = BLL.Get_OutstandingFeeReport(txtFromDate.Text, txtToDate.Text, Convert.ToInt32(ddlClass.SelectedValue), ddlStudent.Text, Convert.ToInt32(ddlFeeType.Text))
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                btnExport.Visible = True
            End If
        End If

        DataDisplay.DataSource = BLL.Get_OutstandingFeeReport(txtFromDate.Text, txtToDate.Text, Convert.ToInt32(ddlClass.SelectedValue), ddlStudent.Text, Convert.ToInt32(ddlFeeType.Text))
        DataDisplay.DataBind()
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        bind()
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        BLL.ExportToExcel(DataDisplay, "OutstandingFeesReport.xls")

    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

        ' Verifies that the control is rendered

    End Sub
End Class
