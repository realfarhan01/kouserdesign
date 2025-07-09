
Partial Class AddHostelRoom
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindHostel()
            bindHostelRoom()
            hfId.Value = 0
        End If
    End Sub



    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try


            Dim res As String = ""
            res = BLL.AddHostelRoom(hfId.Value, ddlhostel.SelectedValue, txtRoomno.Text, ddlroomtype.SelectedValue, txtTotBed.Text, 0, ddlactive.SelectedValue)
            If res.Chars(0) = "#" Then
                hfId.Value = 0
                litmsg.Text = Notifications.SuccessMessage(res)
                bindHostel()
                txtRoomno.Text = ""
                txtTotBed.Text = ""
                bindHostelRoom()
            End If


        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_HostelRooms where HostelRoomId=@HostelRoomId", "@HostelRoomId", e.CommandArgument)
            If dr.Read() Then
                ddlhostel.SelectedValue = dr("Hostelid")
                txtRoomno.Text = dr("roomno")
                ddlroomtype.SelectedValue = dr("roomtype")
                txtTotBed.Text = dr("totalbed")

                
                ddlactive.SelectedValue = dr("deactivated")
                hfId.Value = e.CommandArgument
            End If

        End If
    End Sub
    Sub bindHostelRoom()
        DataDisplay.DataSource = BLL.ExecDataTable("select hr.*,h.hostelname,case when hr.deactivated=0 then 'Active' else 'DeActive' end Status from tbl_HostelRooms hr inner join tbl_HostelInfo h on h.hostelid=hr.hostelid")
        DataDisplay.DataBind()
    End Sub
    Sub bindHostel()
        ddlhostel.DataSource = BLL.ExecDataTable("select * from tbl_HostelInfo")
        ddlhostel.DataTextField = "Hostelname"
        ddlhostel.DataValueField = "hostelid"
        ddlhostel.DataBind()
    End Sub
End Class
