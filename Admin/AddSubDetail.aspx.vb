
Partial Class AddSubDetail
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
            bind()
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.AddSubjectDetail(ddlsubject.SelectedValue, ddlClass.SelectedValue, txtDetail.Text, Session("User"))
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = ""
                bind()
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindClass()

        ddlClass.DataTextField = "ClassName"
        ddlClass.DataValueField = "ClassId"
        ddlClass.DataBind()
    End Sub
    'Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
    '    If e.CommandName = "edit1" Then
    '        Dim dr As SqlDataReader = BLL.ExecDataReader("select * from TransUsers  where uniqueid=@uniqueid", "@uniqueid", e.CommandArgument)
    '        If dr.Read() Then
    '            txtEmployeeid.Text = dr("e.CommandArgument")
    '            txtmtype.Text = dr("mtype")
    '            ddlactive.SelectedValue = dr("Deactivated")

    '            ddlVehicleid.SelectedValue = dr("Vehicleid")

    '            hfId.Value = e.CommandArgument

    '        End If

    '    End If
    'End Sub

    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClass.SelectedIndexChanged
        Dim subjectid As String = BLL.ExecScalar("select subjectids from classmaster where classid=@classid", "@classid", ddlClass.SelectedValue)
        subjectid = Left(subjectid, subjectid.ToString.Length - 1)

        ddlsubject.DataSource = BLL.ExecDataTable("select * from subjectmaster where subjectid in (" & subjectid & ")")
        ddlsubject.DataTextField = "Subjectname"
        ddlsubject.DataValueField = "subjectcode"
        ddlsubject.DataBind()
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("select * from tbl_SubjectDetail sd inner join subjectmaster sm on sm.subjectcode=sd.subjectcode")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

End Class
