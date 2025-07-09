Imports System.Web.Services
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.IO
Imports iTextSharp.tool.xml
Partial Class Admin_dailyinstallmentsummary
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Dim Json As New JsonToDataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
            ltrSchool.Text = BLL.BindSchoolHeader()
            'bind()

        End If
    End Sub

    Sub BindClass()
        ddlClass.DataSource = BLL.BindMainClass()
        ddlClass.DataTextField = "MainClassName"
        ddlClass.DataValueField = "MainClassId"
        ddlClass.DataBind()

        ddlClassTo.DataSource = BLL.BindMainClass()
        ddlClassTo.DataTextField = "MainClassName"
        ddlClassTo.DataValueField = "MainClassId"
        ddlClassTo.DataBind()
    End Sub
    Sub bind()
        Dim SessionId As Integer = 0
        'SessionId = ddlSession.SelectedValue
        DlDailyCollection.DataSource = BLL.GetDailyCollectionSummary(ddlClass.SelectedValue, ddlClassTo.SelectedValue, txtFromDate.Text, txtToDate.Text, "", SessionId, ddlType.SelectedValue, ddlSession.SelectedValue)
        DlDailyCollection.DataBind()
        littotal.Text = " || " + ddlType.SelectedItem.Text + " Installment Collection"
        If txtFromDate.Text <> "" And txtToDate.Text <> "" Then
            littotal.Text &= " || Dated " + txtFromDate.Text + " to " + txtToDate.Text
        End If
        If ddlClass.SelectedValue > 0 And ddlClassTo.SelectedValue > 0 Then
            littotal.Text &= " || Class " + ddlClass.SelectedItem.Text + " to " + ddlClassTo.SelectedItem.Text
        End If
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        bind()
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    End Sub

    '<WebMethod> _
    'Public Shared Function BindHeader() As String
    '    Dim dt As DataTable
    '    dt = (New BusinessLogicLayer).ExecDataTable("select * from schoolmaster")


    '    Dim result As String = ""
    '    result = (New JsonToDataTable).DataTableToJSONWithStringBuilder(dt)
    '    Return result
    'End Function
End Class
