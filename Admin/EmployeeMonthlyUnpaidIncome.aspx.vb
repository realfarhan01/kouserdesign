Imports System.Web.Services

Partial Class Admin_EmployeeMonthlyUnpaidIncome
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'bind()
            ltrSchool.Text = BLL.BindSchoolHeader()

        End If
    End Sub
    Sub bind()
        Dim monthselector() As String = txtMonth.Text.Split("/")
        Dim month As String = monthselector(0)
        Dim year As String = monthselector(1)
        Dim dt As DataTable = BLL.ExecDataTableProc("Prc_EmployeeMonthlySalaryReport", "@SalaryMonth", month, "@SalaryYear", year, "@EmployeeId", "", "@Type", 0)
        reportheader.Visible = False
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                reportheader.Visible = True
                GenBox.Visible = True
            End If
        End If

        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
    

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtMonth.Text <> "" Then
            bind()
        End If
    End Sub

    Protected Sub BtnGen_Click(sender As Object, e As EventArgs) Handles BtnGen.Click
        Try
            Dim IsGen As New CheckBox
            Dim monthselector() As String = txtMonth.Text.Split("/")
            Dim month As String = monthselector(0)
            Dim year As String = monthselector(1)
            For Each mRow As GridViewRow In DataDisplay.Rows
                IsGen = mRow.FindControl("chkGenerate")
                If IsGen.Checked Then
                    BLL.EmployeeMonthlySalaryPaid(DataDisplay.DataKeys(mRow.RowIndex).Value, month, year)
                End If
            Next
            Response.Redirect("EmployeeMonthlyPaidIncome.aspx")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim IsGen As New CheckBox
        For Each mRow As GridViewRow In DataDisplay.Rows
            IsGen = mRow.FindControl("chkGenerate")
            If chkAll.Checked = True Then
                IsGen.Checked = True
            Else
                IsGen.Checked = False
            End If
        Next
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim IsGen As New CheckBox
            Dim monthselector() As String = txtMonth.Text.Split("/")
            Dim month As String = monthselector(0)
            Dim year As String = monthselector(1)
            For Each mRow As GridViewRow In DataDisplay.Rows
                IsGen = mRow.FindControl("chkGenerate")
                If IsGen.Checked Then
                    BLL.ExecNonQuery("Delete From EmployeeMonthlySalary Where EmployeeId=@EmployeeId and SalaryMonth=@SalaryMonth and SalaryYear=@SalaryYear", "@EmployeeId", DataDisplay.DataKeys(mRow.RowIndex).Value, "@SalaryMonth", month, "@SalaryYear", year)
                End If
            Next
            Response.Redirect("EmployeeMonthlyWorkingSheet.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class
