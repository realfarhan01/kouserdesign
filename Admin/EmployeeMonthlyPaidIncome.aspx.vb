Imports System.Web.Services
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.IO
Imports iTextSharp.tool.xml

Partial Class Admin_EmployeeMonthlyPaidIncome
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ltrSchool.Text = BLL.BindSchoolHeader()
        End If
    End Sub
    Sub bind()
        Dim monthselector() As String = txtMonth.Text.Split("/")
        Dim month As String = monthselector(0)
        Dim year As String = monthselector(1)
        Dim dt As DataTable = BLL.ExecDataTableProc("Prc_EmployeeMonthlySalaryReport", "@SalaryMonth", month, "@SalaryYear", year, "@EmployeeId", "", "@Type", 1)
        reportheader.Visible = False
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                reportheader.Visible = True
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
End Class
