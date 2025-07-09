
Partial Class Admin_MasterRoom
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindroom()
           
        End If
    End Sub

    Protected Sub btnsubmit_Click(sender As Object, e As System.EventArgs) Handles btnsubmit.Click

        Try

       
        BLL.AddRoom(txtroomno.Text, txtblock.Text, txtfloor.Text, txtcapacity.Text, ddlactive.SelectedValue)
            bindroom()
            txtroomno.Text = ""
            txtblock.Text = ""
            txtfloor.Text = ""
            txtcapacity.Text = ""

        'Response.Redirect("StudentRegistration.aspx?Pid=" & id)
        litmsg.Text = Notifications.SuccessMessage("Added Successfully.")

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try




    End Sub

    Protected Sub DataDisplay_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from roommaster where roomno=@roomno", "@roomno", e.CommandArgument)
            If dr.Read() Then
                txtblock.Text = dr("block")
                txtcapacity.Text = dr("capacity")
                txtfloor.Text = dr("schoolfloor")
                txtroomno.Text = dr("roomno")
            End If

        End If
    End Sub
    Sub bindroom()
        DataDisplay.DataSource = BLL.BindRoom()
        DataDisplay.DataBind()
    End Sub


End Class
