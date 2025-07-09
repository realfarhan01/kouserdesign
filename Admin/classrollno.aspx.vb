
Partial Class Admin_classrollno
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
        End If
    End Sub
    Sub bind(ByVal Type As Integer)
        Dim dt As DataTable = BLL.GetStudentList("", "", ddlClass.SelectedValue, ddlSection.SelectedValue, "", Type, "", "", "", "")
        dt.DefaultView.Sort = "StudentName ASC"
        dt = dt.DefaultView.ToTable
        DataDisplay2.DataSource = dt
        DataDisplay2.DataBind()
        If dt.Rows.Count > 0 Then
            btnRollNo.Visible = True
            ltrSchool.Text = BLL.BindSchoolHeader()
            reportheader.Visible = True
            btnUpdate.Visible = True
        End If

        'If ddlSection.SelectedValue = "" And dt.Rows.Count > 0 Then
        '    Dim view As DataView = New DataView(dt)
        '    Dim distinctValues As DataTable = view.ToTable(True, "Section")
        '    ddlSection.Items.Clear()
        '    ddlSection.Items.Add(New ListItem("All Section", ""))
        '    ddlSection.DataSource = distinctValues
        '    ddlSection.DataTextField = "Section"
        '    ddlSection.DataValueField = "Section"
        '    ddlSection.DataBind()
        '    ddlSectionUpgrade.DataSource = distinctValues
        '    ddlSectionUpgrade.DataTextField = "Section"
        '    ddlSectionUpgrade.DataValueField = "Section"
        '    ddlSectionUpgrade.DataBind()
        'End If
    End Sub
    Sub BindClass()
        ddlClass.DataSource = BLL.BindMainClass()

        ddlClass.DataTextField = "MainClassName"
        ddlClass.DataValueField = "MainClassId"
        ddlClass.DataBind()
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        bind(0)
    End Sub


    Protected Sub DataDisplay_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DataDisplay2.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If
    End Sub
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim txtRollNo As New TextBox
            For Each mRow As GridViewRow In DataDisplay2.Rows
                txtRollNo = mRow.FindControl("txtRollNo")
                BLL.ExecNonQueryProc("Prc_UpdateStudentRollNo", "@StudentId", DataDisplay2.DataKeys(mRow.RowIndex).Value, "@RollNo", txtRollNo.Text)
            Next
            ClientScript.RegisterStartupScript(Page.[GetType](), "alert", "alert('Students Roll No Updated Successfully!!');window.location='classrollno.aspx';", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnRollNo_Click(sender As Object, e As EventArgs) Handles btnRollNo.Click
        Dim dt As DataTable = BLL.GetStudentList("", "", ddlClass.SelectedValue, ddlSection.SelectedValue, "", 0, "", "", "", "")

        Dim RollNo As Integer = 0
        If txtStartRollNo.Text <> "" Then
            RollNo = Val(txtStartRollNo.Text)
        End If
        If ddlType.SelectedValue = "1" Then
            dt.DefaultView.Sort = "StudentName ASC"
        ElseIf ddlType.SelectedValue = "2" Then
            dt.DefaultView.Sort = "StudentId ASC"
        End If

        dt = dt.DefaultView.ToTable
        DataDisplay2.DataSource = dt
        DataDisplay2.DataBind()
        If dt.Rows.Count > 0 Then
            btnUpdate.Visible = True
        End If


        Dim txtRollNo As New TextBox
        For Each mRow As GridViewRow In DataDisplay2.Rows
            txtRollNo = mRow.FindControl("txtRollNo")
            txtRollNo.Text = RollNo
            RollNo = RollNo + 1
        Next
    End Sub
End Class
