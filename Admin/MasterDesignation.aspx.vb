
Partial Class Admin_MasterDesignation
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindroom()
            hfid.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try

      
        BLL.AddDesignation(hfid.Value, txtname.Text)
            If btnsubmit.Text = "Update" Then
                litmsg.Text = Notifications.SuccessMessage("Updated Successfully.")
                btnsubmit.Text = "Submit"

                bindroom()
                txtname.Text = ""
                hfid.Value = 0
            Else
                litmsg.Text = Notifications.SuccessMessage("Added Successfully.")
                bindroom()
                txtname.Text = ""
                hfid.Value = 0
            End If
           
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from DesignationMaster where sno=@sno", "@sno", e.CommandArgument)
            If dr.Read() Then

                txtname.Text = dr("Designation")
                hfid.Value = dr("sno")
                btnsubmit.Text = "Update"

            End If

        End If
    End Sub
    Sub bindroom()
        DataDisplay.DataSource = BLL.ExecDataTable("select * from DesignationMaster")
        DataDisplay.DataBind()
    End Sub
End Class
