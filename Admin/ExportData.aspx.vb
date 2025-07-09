
Partial Class Admin_ExportData
    Inherits System.Web.UI.Page

    Dim BLL As New BusinessLogicLayer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataDisplay.DataSource = BLL.GetStudentList(Request.QueryString("sid"), "", Request.QueryString("class"), Request.QueryString("section"), "", 0, Request.QueryString("birthdayfrom"), Request.QueryString("birthdayto"), Request.QueryString("admfrom"), Request.QueryString("admto"))
            DataDisplay.DataBind()
        End If
    End Sub

End Class
