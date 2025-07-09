
Partial Class Admin_upgradeclassall
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
            divupgrade.Visible = False
        End If
    End Sub
    Sub bind(ByVal Type As Integer)
        Dim dt As DataTable = BLL.GetStudentListSession("", "", ddlClass.SelectedValue, ddlSection.SelectedValue, "", Type, "", "", "", "", ddlSession.SelectedValue)
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
        divupgrade.Visible = False
        If dt.Rows.Count > 0 Then
            divupgrade.Visible = True
        End If

        If ddlSection.SelectedValue = "" And dt.Rows.Count > 0 Then
            Dim view As DataView = New DataView(dt)
            Dim distinctValues As DataTable = view.ToTable(True, "Section")
            ddlSection.Items.Clear()
            ddlSection.Items.Add(New ListItem("All Section", ""))
            ddlSection.DataSource = distinctValues
            ddlSection.DataTextField = "Section"
            ddlSection.DataValueField = "Section"
            ddlSection.DataBind()
            'ddlSectionUpgrade.DataSource = distinctValues
            'ddlSectionUpgrade.DataTextField = "Section"
            'ddlSectionUpgrade.DataValueField = "Section"
            'ddlSectionUpgrade.DataBind()
        End If
    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindMainClass()

        ddlClass.DataTextField = "MainClassName"
        ddlClass.DataValueField = "MainClassId"
        ddlClass.DataBind()

        ddlClassUpgrade.DataSource = BLL.BindMainClass()
        ddlClassUpgrade.DataTextField = "MainClassName"
        ddlClassUpgrade.DataValueField = "MainClassId"
        ddlClassUpgrade.DataBind()
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        bind(0)
    End Sub


    Protected Sub DataDisplay_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DataDisplay.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Protected Sub btnUpgrade_Click(sender As Object, e As EventArgs) Handles btnUpgrade.Click
        Try
            Dim IsGen As New CheckBox
            For Each mRow As GridViewRow In DataDisplay.Rows
                IsGen = mRow.FindControl("chkUpgrade")
                If IsGen.Checked Then
                    BLL.UpgradeStudentwithSection(DataDisplay.DataKeys(mRow.RowIndex).Value, ddlClassUpgrade.SelectedValue, ddlSectionUpgrade.SelectedValue, ddlSessionUpgrade.SelectedValue)
                End If
            Next
            ClientScript.RegisterStartupScript(Page.[GetType](), "alert", "alert('Student Class Upgraded Successfully!!');window.location='upgradeclassall.aspx';", True)
        Catch ex As Exception

        End Try
        'If hdnRowID.Value <> "" Then
        '    Dim MasterPass As String = BLL.ExecScalar("Select Top 1 MasterPassword from schoolmaster")
        '    If MasterPass = txtMasterPass3.Text Then
        '        BLL.UpgradeStudent(hdnRowID.Value, ddlNewClass.SelectedValue)
        '        bind(0)
        '        ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Student uprgaded Successfully');", True)
        '    Else
        '        ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Please enter a valid Password');", True)
        '    End If
        'Else
        '    ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('invalid details');", True)
        'End If
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim IsGen As New CheckBox
        For Each mRow As GridViewRow In DataDisplay.Rows
            IsGen = mRow.FindControl("chkUpgrade")
            If chkAll.Checked = True Then
                IsGen.Checked = True
            Else
                IsGen.Checked = False
            End If
        Next
    End Sub
End Class
