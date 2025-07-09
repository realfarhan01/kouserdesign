
Partial Class BookissuedDetail
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()

        End If
    End Sub
    Sub bind()
        DataDisplay.DataSource = BLL.Getissuedbook(txtbookid.Text, txtStudentid.Text)
        DataDisplay.DataBind()
    End Sub
   
End Class
