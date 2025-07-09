
Partial Class UserDetail
    Inherits BasePage
    Public enc As New Encryption64
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildDataSource()
        End If

    End Sub
    Sub BuildDataSource()
        Dim DTable As DataTable

        DTable = BLL.ExecDataTable("select * from adminlogins WHERE Sno>1 and isBlock=0")
        DataDisplay.DataSource = DTable
        DataDisplay.DataBind()


    End Sub


    Protected Sub DataDisplay_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DataDisplay.PageIndexChanging
        DataDisplay.PageIndex = e.NewPageIndex
        BuildDataSource()
    End Sub


    'Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
    '    Dim DTable As New DataTable
    '    Con.Open()
    '    Dim cmd As New SqlCommand
    '    Dim condition As String = " WHERE Sno>1 and isBlock=0 "

    '    If txtIntroid.Text <> "" Then
    '        condition = condition & " and Loginid =@Loginid"
    '        cmd.Parameters.Add("@Loginid", SqlDbType.VarChar, 20).Value = txtIntroid.Text
    '    End If
    '    Dim Q As String = ""

    '    cmd.CommandText = "select username,LoginId,Password,Email,Address,Mobile from usermaster" & condition
    '    cmd.Connection = Con
    '    Dim dgGrid As New GridView

    '    DTable.Load(cmd.ExecuteReader())
    '    dgGrid.DataSource = DTable
    '    dgGrid.DataBind()
    '    Con.Close()
    '    If ddlExport.SelectedValue = "1" Then
    '        BLL.ExportToExcel(dgGrid, "TotalMembers.xls")
    '    ElseIf ddlExport.SelectedValue = "2" Then

    '    ElseIf ddlExport.SelectedValue = "3" Then
    '        BLL.ExportToWord(dgGrid, "TotalMembers.doc")
    '    ElseIf ddlExport.SelectedValue = "4" Then
    '        BLL.ExportToCsv(DTable, "TotalMembers.csv")
    '    End If

    'End Sub
    ''Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    '    ' Verifies that the control is rendered 
    'End Sub

    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "Delete" Then
        ElseIf e.CommandName = "PDelete" Then
            BLL.ExecNonQuery("Update adminlogins set isBlock=1 where LoginId=@LoginId", "@LoginId", e.CommandArgument)
            BuildDataSource()
        Else
            If e.CommandName = "Edit" Then
                Response.Redirect("CreateUser.aspx?ID=" & HttpUtility.HtmlEncode(enc.Encrypt(e.CommandArgument)))
            End If
        End If
    End Sub

End Class
