
Partial Class Paidfee
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from ReceiptMaster where isdelete=0 and paiddate is not null")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Response.Redirect("Receipt.aspx?rid=" + (New Encryption64).Encrypt(e.CommandArgument))
        End If
    End Sub
End Class
