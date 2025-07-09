
Partial Class MasterExam
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            hfId.Value = 0
            bind()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddExam(hfId.Value, txtexamname.Text, ddlSession.SelectedValue, txtfromdate.Text, txtTodate.Text, ddlactive.SelectedValue)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = 0
                bind()
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_ExamMaster  where ExamId=@ExamId", "@ExamId", e.CommandArgument)
            If dr.Read() Then
                txtexamname.Text = dr("examname")
                ddlSession.SelectedValue = dr("sessionyear")
                txtfromdate.Text = dr("startdate")
                txtTodate.Text = dr("Enddate")
                ddlactive.SelectedValue = dr("Deactivated")

                hfId.Value = e.CommandArgument

            End If

        End If
    End Sub

    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select *,case when deactivated=0 then 'Active' else 'DeActivate' end Status from tbl_ExamMaster")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub




End Class
