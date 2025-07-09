
Partial Class content
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ddlContent.DataSource = BLL.ExecDataTable("select contentid,PageName from tblcontent")
            ddlContent.DataValueField = "contentid"
            ddlContent.DataTextField = "PageName"
            ddlContent.DataBind()

            showData()
        End If

    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        BLL.ExecNonQuery("Update tblcontent set ContentText=@ContentText,show=@Show where contentid=@ContenTId", "@ContentText", txtcontent.Text, "@Show", Convert.ToInt16(chkshow.Checked), "@ContenTId", ddlContent.SelectedValue)
       
    End Sub
    Private Sub showData()

        Dim dr As SqlDataReader = BLL.ExecDataReader("select ContentText,show from tblcontent where contentid=@ContentId", "@ContentId", ddlContent.SelectedValue)
        While dr.Read
            txtcontent.Text = dr("ContentText")
            chkshow.Checked = Convert.ToBoolean(dr("show"))
        End While

        If ddlContent.SelectedValue = "2" Then
            lblmsg.Text = "if you blank the textbox then popup not display"
        Else
            lblmsg.Text = "Home Page News"
        End If
    End Sub
   
    Protected Sub ddlContent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlContent.SelectedIndexChanged
        showData()
    End Sub
End Class
