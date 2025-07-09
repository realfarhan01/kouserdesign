
Partial Class Admin_MasterClass
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'txtclassroom.DataSource = BLL.BindRoom()

            'txtclassroom.DataTextField = "RoomNo"
            'txtclassroom.DataValueField = "RoomNo"
            'txtclassroom.DataBind()
            BindClass()

            ddlClass.DataSource = BLL.BindMainClass()

            ddlClass.DataTextField = "MainClassName"
            ddlClass.DataValueField = "MainClassId"
            ddlClass.DataBind()
            hfId.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(sender As Object, e As System.EventArgs) Handles btnsubmit.Click
        Try
            BLL.AddClass(hfId.Value, ddlClass.SelectedValue, ddlSection.SelectedValue, 0, "")
            If btnsubmit.Text = "Update" Then
                hfId.Value = 0
                'Response.Redirect("StudentRegistration.aspx?Pid=" & id)
                litmsg.Text = Notifications.SuccessMessage("Updated Successfully.")
                ddlClass.SelectedValue = ""
                'txtHead.Text = ""
                BindClass()
                btnsubmit.Text = "Submit"
            Else
                hfId.Value = 0
                'Response.Redirect("StudentRegistration.aspx?Pid=" & id)
                litmsg.Text = Notifications.SuccessMessage("Added Successfully.")
                ddlClass.SelectedValue = ""
                'txtHead.Text = ""
                BindClass()
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub

    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from Classmaster where Classid=@Classid", "@Classid", e.CommandArgument)
            If dr.Read() Then
                'txtclassroom.SelectedValue = dr("ClassRoomNo")
                ddlClass.SelectedValue = dr("ClassId")
                'txtHead.Text = dr("ClassHead")
                ddlSection.Text = dr("Section")
                hfId.Value = dr("ClassId")
                btnsubmit.Text = "Update"
            End If

        End If
    End Sub

    Sub BindClass()
        DataDisplay.DataSource = BLL.BindClass()

      
        DataDisplay.DataBind()
    End Sub
End Class
