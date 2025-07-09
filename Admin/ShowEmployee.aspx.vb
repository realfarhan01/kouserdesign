
Partial Class ShowEmployee
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
            bindDesignation()
        End If
    End Sub
    Sub bind()
        DataDisplay.DataSource = BLL.GetEmployeeList(txtemployeeid.Text, txtemployeename.Text, ddldesignation.SelectedValue)
        DataDisplay.DataBind()
    End Sub
    Sub bindDesignation()
        ddldesignation.DataSource = BLL.ExecDataTable("select * from DesignationMaster")
        ddldesignation.DataTextField = "Designation"
        ddldesignation.DataValueField = "Designation"
        ddldesignation.DataBind()
    End Sub
End Class
