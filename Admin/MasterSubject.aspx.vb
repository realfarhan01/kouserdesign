
Partial Class Admin_MasterSubject
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    'Sub BindClass()
    '    ddlClass.DataSource = BLL.BindClass()

    '    ddlClass.DataTextField = "ClassName"
    '    ddlClass.DataValueField = "ClassId"
    '    ddlClass.DataBind()
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindsubject()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try

            If btnsubmit.Text = "Update" Then
                BLL.AddSubject(txtcode.Text, txtname.Text)
                litmsg.Text = Notifications.SuccessMessage("Updated Successfully.")
                btnsubmit.Text = "Submit"
            Else
                BLL.AddSubject(txtcode.Text, txtname.Text)
                litmsg.Text = Notifications.SuccessMessage("Added Successfully.")

            End If
           
            hfid.Value = ""
            txtcode.Text = ""
            txtname.Text = ""
            bindsubject()
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try aain Later")
        End Try
       
    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from subjectmaster where subjectcode=@subjectcode", "@subjectcode", e.CommandArgument)
            If dr.Read() Then
                txtcode.Text = dr("subjectcode")
                txtname.Text = dr("subjectname")
                btnsubmit.Text = "Update"

            End If

        End If
    End Sub
    Sub bindsubject()
        DataDisplay.DataSource = BLL.ExecDataTable("select * from subjectmaster")
        DataDisplay.DataBind()
    End Sub
End Class
