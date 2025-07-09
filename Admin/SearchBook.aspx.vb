
Partial Class SearchBook
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
            bindcat()
        End If
    End Sub
    Sub bind()
        DataDisplay.DataSource = BLL.Getbook(ddlcat.SelectedValue, txtbookid.Text, txtBookTag.Text)
        DataDisplay.DataBind()
    End Sub
    Sub bindcat()
        ddlcat.DataSource = BLL.ExecDataTable("select * from tbl_LibBooksCategories")
        ddlcat.DataTextField = "categoryname"
        ddlcat.DataValueField = "categoryid"
        ddlcat.DataBind()
    End Sub
End Class
