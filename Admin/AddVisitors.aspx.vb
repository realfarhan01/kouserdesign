
Partial Class AddVisitors
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
          
            bind()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddVisitors(txtVisitdate.Text, txtStudentId.Text, txtEmployeeid.Text, txtVisitorName.Text, txtrelation.Text, txtNOP.Text, txtVisitDetails.Text)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = ""
                bind()
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    

    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from TransUsers")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

End Class
