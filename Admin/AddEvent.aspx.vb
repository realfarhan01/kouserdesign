
Partial Class Admin_AddEvent
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
           
            bind()
            hfId.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddEvent(hfId.Value, txtEventname.Text, txteventdetail.Text, txtfromdate.Text, txtTodate.Text, ddlactive.SelectedValue)
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
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_Events  where EventId=@EventId", "@EventId", e.CommandArgument)
            If dr.Read() Then
                txtEventname.Text = dr("Eventname")
                txteventdetail.Text = dr("eventdetail")
                txtfromdate.Text = dr("fromdate")
                txtTodate.Text = dr("Todate")
                ddlactive.SelectedValue = dr("Deactivated")

                hfId.Value = e.CommandArgument

            End If

        End If
    End Sub

    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select *,case when deactivated=0 then 'Active' else 'DeActivate' end Status from tbl_Events")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub




End Class
