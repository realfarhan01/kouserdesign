
Partial Class ExamRooms
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
          
            txtclassroom.DataSource = BLL.BindRoom()

            txtclassroom.DataTextField = "RoomNo"
            txtclassroom.DataValueField = "RoomNo"
            txtclassroom.DataBind()
            hfId.Value = 0
            bind()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddExamRoom(hfId.Value, ddlshifts.SelectedValue, txtclassroom.SelectedValue, txtTotClasses.Text, txtRRows.Text, txtRCols.Text, ddlsittingtype.SelectedValue)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = 0
                bind()
            Else
                litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_ExamRooms  where ExamRoomId=@ExamRoomId", "@ExamRoomId", e.CommandArgument)
            If dr.Read() Then
            
                ddlshifts.SelectedValue = dr("shiftid")

                txtclassroom.SelectedValue = dr("RoomNo")
                txtTotClasses.Text = dr("TotClasses")
                txtRRows.Text = dr("RRows")
                txtRCols.Text = dr("RCols")
                ddlsittingtype.SelectedValue = dr("SType")
                
                hfId.Value = e.CommandArgument


            End If

        End If
    End Sub

    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from tbl_ExamRooms")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

   
   
    Sub bindExamShift()

        ddlshifts.DataSource = BLL.ExecDataTable("select ShiftId,ShiftTime from  tbl_ExamShifts where Deactivated=0")
        ddlshifts.DataTextField = "Shifttime"
        ddlshifts.DataValueField = "Shiftid"
        ddlshifts.DataBind()
    End Sub

   
End Class
