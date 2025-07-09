
Partial Class ExamRoomClass
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bindExam()
            BindClass()

        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddExamRoomClasses(ddlexam.SelectedValue, ddlClass.SelectedValue, txtStartRollNo.Text)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)


            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later.")
        End Try

    End Sub
    'Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
    '    If e.CommandName = "edit1" Then
    '        Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_ExamParticulars  where Examid=@Examid", "@Examid", e.CommandArgument)
    '        If dr.Read() Then
    '            ddlexam.SelectedValue = e.CommandArgument
    '            ddlexamdate.SelectedValue = dr("examdate")
    '            ddlshifts.SelectedValue = dr("shiftid")
    '            ddlClass.SelectedValue = dr("Classid")
    '            ddlsubject.SelectedValue = dr("Subjectcode")
    '            txtMaxTheoryMarks.Text = dr("MaxTheoryMarks")
    '            txtMaxPracticalMarks.Text = dr("MaxPracticalMarks")
    '            txtPassTheoryMarks.Text = dr("PassTheoryMarks")
    '            txtPassPracticalMarks.Text = dr("PassPracticalMarks")



    '        End If

    '    End If
    'End Sub

    'Sub bind()
    '    Dim dt As DataTable = BLL.Get_ExamParticulars(0, 0, 0, "")
    '    DataDisplay.DataSource = dt
    '    DataDisplay.DataBind()
    'End Sub

    Sub bindExam()

        ddlexam.DataSource = BLL.ExecDataTable("select * from tbl_ExamRooms ")
        ddlexam.DataTextField = "ExamRoomid"
        ddlexam.DataValueField = "ExamRoomid"
        ddlexam.DataBind()
    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindClass()

        ddlClass.DataTextField = "ClassName"
        ddlClass.DataValueField = "ClassId"
        ddlClass.DataBind()
    End Sub
    
   
End Class
