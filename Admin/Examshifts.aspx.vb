
Partial Class Examshifts
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindExam()
            bind()
            hfId.Value = 0
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddExamShift(hfId.Value, ddlexam.SelectedValue, txtshifttime.Text, ddlactive.SelectedValue)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = 0
                bind()
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "edit1" Then
            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_ExamShifts  where ShiftId=@ShiftId", "@ShiftId", e.CommandArgument)
            If dr.Read() Then
                ddlexam.SelectedValue = dr("Examid")
                txtshifttime.Text = dr("shifttime")
                ddlactive.SelectedValue = dr("Deactivated")

                hfId.Value = e.CommandArgument

            End If

        End If
    End Sub

    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select s.*,e.examname,case when s.deactivated=0 then 'Active' else 'DeActivate' end Status from tbl_ExamShifts s inner join tbl_ExamMaster e on e.examid=s.examid")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

    Sub bindExam()

        ddlexam.DataSource = BLL.BindExams
        ddlexam.DataTextField = "Examname"
        ddlexam.DataValueField = "Examid"
        ddlexam.DataBind()
    End Sub


End Class
