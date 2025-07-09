
Partial Class UserRights
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fill()
            If Session("uid") Is Nothing Then
            Else
                ddlMemberid.Text = Session("uid")
                ddlMemberid.Enabled = True
            End If
            Try
                Dim count As Int32 = BLL.ExecScalar("Select COUNT(*) from adminlogins where sno>1 and IsBlock=0")
                If count > 0 Then
                    ShowData(ddlMemberid.SelectedValue)
                    btnsave.Enabled = True
                Else
                    btnsave.Enabled = False
                End If
            Catch ex As Exception

            End Try
        End If

    End Sub
    Public Sub ShowData(ByVal sno As Integer)
        Dim str As String = BLL.ExecScalar("select left(menustr,len(menustr)-1) from adminlogins where sno=@sno", "@sno", sno)
        Dim dt As New DataTable
        'If tcCon.State = ConnectionState.Closed Then : tcCon.Open() : End If
        'Dim str As String = New SqlCommand("select left(menustr,len(menustr)-1) from userMaster where sno=" & sno, tcCon).ExecuteScalar
        'Dim cmd As SqlCommand
        'If str.Length <= 0 Then
        '    cmd = New SqlCommand("select menuName,sno,snostr, 0 checked from dbo.DynamicMenu where Parentmenuid<>0 and sno not in (select Parentmenuid from DynamicMenu) and Active=1 and sno not in (4,5)", tcCon)
        'Else
        '    cmd = New SqlCommand("select menuName,sno,snostr, case when sno in (" & str & ") then 1 else 0 end checked from dbo.DynamicMenu where Parentmenuid<>0 and sno not in (select Parentmenuid from DynamicMenu) and Active=1 and sno not in (2,3,4,5) ", tcCon)
        'End If
        'Dim dt As New DataTable
        'dt.Load(cmd.ExecuteReader)
        'tcCon.Close()
        'datalist1.DataSource = dt
        'datalist1.DataBind()
        dt = BLL.ExecDataTable("select MenuName,sno,snostr from dbo.DynamicMenu where Parentmenuid=0 and Active=1 and Url='#' and sno not in (2) ")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
     Public Sub fill()
        ddlMemberid.DataSource = BLL.ExecDataTable("Select sno,UserName from adminlogins where sno>1 and IsBlock=0")
        ddlMemberid.DataTextField = "UserName"
        ddlMemberid.DataValueField = "Sno"
        ddlMemberid.DataBind()
    End Sub

    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim str As String = "1,"
        Dim i As Integer = 0
        Dim chk As CheckBox
        Dim lbl As Label
        Dim rep As Repeater
        For Each item As RepeaterItem In DataDisplay.Items
            rep = CType(item.FindControl("datalist1"), Repeater)
            For Each item2 As RepeaterItem In rep.Items
                chk = CType(item2.FindControl("chk1"), CheckBox)
                lbl = CType(item2.FindControl("lbl1"), Label)
                If chk.Checked Then
                    str = str + lbl.Text
                End If
            Next
        Next
        If str.Length > 0 Then
            'str = Left(str, str.Length - 1)
            BLL.ExecNonQuery("Update adminlogins set menustr=@str where sno=@sno", "@str", str, "@sno", ddlMemberid.SelectedValue)
        End If
    End Sub

    Protected Sub ddlMemberid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMemberid.SelectedIndexChanged
        ShowData(ddlMemberid.SelectedValue)
    End Sub
    Protected Sub DataDisplay_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles DataDisplay.ItemDataBound
        Dim data As Repeater

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            data = e.Item.FindControl("datalist1")
            Dim drv As DataRowView = e.Item.DataItem
            Dim sno As Integer = drv.Row("sno")
            Dim str As String = BLL.ExecScalar("select left(menustr,len(menustr)-1) from adminlogins where sno=@sno", "@sno", ddlMemberid.SelectedValue)
            Dim dt As DataTable = BLL.ExecDataTable("select menuName,sno,snostr ,case when sno in (" & str & ") then 1 else 0 end checked from dbo.DynamicMenu where Parentmenuid=@PId and Active=1 and sno not in (2) ", "@Pid", sno)
            data.DataSource = dt
            data.DataBind()

        End If
    End Sub
End Class
