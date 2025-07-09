Imports System.Web.Services

Partial Class Admin_EmployeeMonthlyWorkingSheet
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'bind()
        End If
    End Sub
    Sub bind()
        Dim monthselector() As String = txtMonth.Text.Split("/")
        Dim month As String = monthselector(0)
        Dim year As String = monthselector(1)
        Dim dt As DataTable = BLL.ExecDataTableProc("Prc_GetEmployeeSalaryPayScale", "@SalaryMonth", month, "@SalaryYear", year)
        btnExport.Visible = False
        Dim workTable As DataTable = DirectCast(ViewState("tbl"), DataTable)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                btnExport.Visible = True
                For Each row As DataRow In dt.Rows
                    workTable.Rows.Add(New Object() {row.Item("RowID"), row.Item("EmployeeId"), row.Item("EmployeeName"), row.Item("PayScaleTitle"), Convert.ToDecimal(row.Item("BasicSalary")), Convert.ToDecimal(txtWorkingDays.Text), 0, Convert.ToDecimal(row.Item("TotalPaidLeaves")), Convert.ToDecimal(txtWorkingDays.Text), Convert.ToDecimal(row.Item("TotalPaidLeaves"))})
                Next row
                btnSubmit.Visible = False
                GenBox.Visible = True
                txtMonth.ReadOnly = True
                txtWorkingDays.ReadOnly = True
            End If
        End If

        DataDisplay.DataSource = workTable
        DataDisplay.DataBind()
    End Sub
    Sub CreateTable()
        ViewState("tbl") = Nothing
        Dim workTable As DataTable = New DataTable("tbl")
        workTable.Columns.Add("RowID", Type.GetType("System.Int32"))
        workTable.Columns.Add("EmployeeId", Type.GetType("System.String"))
        workTable.Columns.Add("EmployeeName", Type.GetType("System.String"))
        workTable.Columns.Add("PayScaleTitle", Type.GetType("System.String"))
        workTable.Columns.Add("BasicSalary", Type.GetType("System.Double"))
        workTable.Columns.Add("TotalWorkingDays", Type.GetType("System.Double"))
        workTable.Columns.Add("Leaves", Type.GetType("System.Double"))
        workTable.Columns.Add("TotalPaidLeaves", Type.GetType("System.Double"))
        workTable.Columns.Add("SalaryDays", Type.GetType("System.Double"))
        workTable.Columns.Add("CarryLeaves", Type.GetType("System.Double"))
        ViewState("tbl") = workTable
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            If txtMonth.Text <> "" And Val(txtWorkingDays.Text) > 0 Then
                CreateTable()
                bind()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub txtLeaves_TextChanged(sender As Object, e As EventArgs)
        Dim currentRow As GridViewRow = DirectCast(DirectCast(sender, TextBox).Parent.Parent, GridViewRow)

        Dim txtLeaves As TextBox = DirectCast(currentRow.FindControl("txtLeaves"), TextBox)
        Dim LblSalaryDays As Label = DirectCast(currentRow.FindControl("LblSalaryDays"), Label)
        Dim LblCarryLeaves As Label = DirectCast(currentRow.FindControl("LblCarryLeaves"), Label)
        Dim LblWorkingDays As Label = DirectCast(currentRow.FindControl("LblWorkingDays"), Label)
        Dim LblPaidLeaves As Label = DirectCast(currentRow.FindControl("LblPaidLeaves"), Label)
        'txtLeaves.Text = Convert.ToString(count + 10)
        Dim Leaves As Decimal = txtLeaves.Text
        Dim SalaryDays As Decimal = LblSalaryDays.Text
        Dim CarryLeaves As Decimal = LblCarryLeaves.Text
        Dim WorkingDays As Decimal = LblWorkingDays.Text
        Dim PaidLeaves As Decimal = LblPaidLeaves.Text

        If PaidLeaves >= Leaves Then
            LblCarryLeaves.Text = PaidLeaves - Leaves
            LblSalaryDays.Text = WorkingDays
        Else
            LblCarryLeaves.Text = 0
            LblSalaryDays.Text = WorkingDays + PaidLeaves - Leaves
        End If
        'LblCarryLeaves.Text=
    End Sub

    'Protected Sub DataDisplay_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles DataDisplay.RowCommand
    '    Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent,  _
    '                                GridViewRow)
    '    Dim index As Integer = gvRow.RowIndex
    '    'If e.Row.RowType = DataControlRowType.DataRow Then

    '    'End If
    '    'Dim drv As DataRowView = TryCast(e.Item.DataItem, DataRowView)
    '    'Dim DlDailyCollectionDetail As DataList = TryCast(e.Item.FindControl("DlDailyCollectionDetail"), DataList)

    '    Dim txtLeaves As TextBox = DirectCast(e.FindControl("txtLeaves"), TextBox)
    '    Dim count As Int32 = Convert.ToInt32(txtLeaves.Text)
    '    txtLeaves.Text = Convert.ToString(count + 10)

    'End Sub

    Protected Sub BtnGen_Click(sender As Object, e As EventArgs) Handles BtnGen.Click
        Try

            Dim chkStr As String = ""
            Dim IsGen As New CheckBox
            Dim txtLeaves As New TextBox
            Dim LblSalaryDays As New Label
            Dim LblCarryLeaves As New Label
            Dim LblWorkingDays As New Label
            Dim LblPaidLeaves As New Label
            Dim monthselector() As String = txtMonth.Text.Split("/")
            Dim month As String = monthselector(0)
            Dim year As String = monthselector(1)
            For Each mRow As GridViewRow In DataDisplay.Rows
                IsGen = mRow.FindControl("chkGenerate")
                txtLeaves = DirectCast(mRow.FindControl("txtLeaves"), TextBox)
                LblSalaryDays = DirectCast(mRow.FindControl("LblSalaryDays"), Label)
                LblCarryLeaves = DirectCast(mRow.FindControl("LblCarryLeaves"), Label)
                LblWorkingDays = DirectCast(mRow.FindControl("LblWorkingDays"), Label)
                LblPaidLeaves = DirectCast(mRow.FindControl("LblPaidLeaves"), Label)
                If IsGen.Checked Then
                    BLL.EmployeeMonthlySalaryGeneration(DataDisplay.DataKeys(mRow.RowIndex).Value, month, year, LblWorkingDays.Text, Val(txtLeaves.Text), LblPaidLeaves.Text, LblSalaryDays.Text, LblCarryLeaves.Text)
                End If
            Next
            Response.Redirect("EmployeeMonthlyUnpaidIncome.aspx")
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
End Class
