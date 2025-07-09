
Partial Class Admin_UpgradeClass
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Sub BindClass()
        ddlClass.DataSource = BLL.BindClass()

        ddlClass.DataTextField = "ClassName"
        ddlClass.DataValueField = "ClassId"
        ddlClass.DataBind()
    End Sub
    Sub BindClassNew()
        ddlnewclass.DataSource = BLL.BindClass()

        ddlnewclass.DataTextField = "ClassName"
        ddlnewclass.DataValueField = "ClassId"
        ddlnewclass.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
        End If
    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        ddlStudent.DataSource = BLL.ExecDataTable("select cnt,studentname from studentmaster where classid=@classid", "@classid", ddlClass.SelectedValue)

        ddlStudent.DataTextField = "StudentName"
        ddlStudent.DataValueField = "cnt"
        ddlStudent.DataBind()
        BindClassNew()
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            BLL.UpgradeStudent(ddlStudent.SelectedValue, ddlnewclass.SelectedValue)
            litmsg.Text = Notifications.SuccessMessage("Student Upgraded Successfully.")
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
    End Sub
End Class
