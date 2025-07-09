
Partial Class Admin_BusRouteStudentsExport
    Inherits System.Web.UI.Page

    Dim BLL As New BusinessLogicLayer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim dt As DataTable = BLL.GetConvenceStudents(Request.QueryString("sid"), Request.QueryString("name"), Request.QueryString("class"), Request.QueryString("section"), Request.QueryString("rid"), Request.QueryString("pid"))
            DataDisplay.DataSource = dt
            DataDisplay.DataBind()
        End If
    End Sub
End Class
