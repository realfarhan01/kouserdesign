
Partial Class Admin_AddNotification
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
   
    Sub BindClassNew()
        ddlnewclass.DataSource = BLL.BindMainClass()

        ddlnewclass.DataTextField = "MainClassName"
        ddlnewclass.DataValueField = "MainClassId"
        ddlnewclass.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClassNew()
            bind()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddNotice(txtnotification.Text, ddlnoti.SelectedValue, Session("User"), ddlnewclass.SelectedValue)
            litmsg.Text = Notifications.SuccessMessage(res)
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
    End Sub
    Sub bind()
        DataDisplay.DataSource = BLL.ExecDataTableProc("Get_Notice")
        DataDisplay.DataBind()
    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "delete1" Then
            BLL.ExecNonQuery("update tbl_NoticeBoard Set Deactivated=1 Where SNo=@SNo", "@SNo", e.CommandArgument)
            bind()
        End If
    End Sub
End Class
