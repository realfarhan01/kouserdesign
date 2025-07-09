
Partial Class ShowAttendance
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub
    Sub bind()

        DataDisplay.DataSource = BLL.ExecDataTableProc("Get_AttendanceMaster", "@UniqueId", txtParentId.Text, "@SavedBy", txtsave.Text, "OnDate", txtdate.Text)
        DataDisplay.DataBind()
    End Sub

    Protected Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        bind()
    End Sub
End Class
