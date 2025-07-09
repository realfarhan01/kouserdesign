Imports System.Web.Services
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.IO
Imports iTextSharp.tool.xml
Partial Class Admin_dailyCollectionDetail
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
        DlDailyCollection.DataSource = BLL.GetCollection(ddlClass.SelectedValue, ddlClassTo.SelectedValue, txtFromDate.Text, txtToDate.Text, SessionId, ddlType.SelectedValue, ddlSession.SelectedValue)
        DlDailyCollection.DataBind()
        littotal.Text = " || " + ddlType.SelectedItem.Text + " Collection"
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

    Protected Sub DlDailyCollection_ItemDataBound(sender As Object, e As DataListItemEventArgs)
        ' If e.Item.ItemType = ListItemType.Item Then
        Dim drv As DataRowView = TryCast(e.Item.DataItem, DataRowView)
        Dim DlDailyCollectionDetail As DataList = TryCast(e.Item.FindControl("DlDailyCollectionDetail"), DataList)
        Dim hdfDate As HiddenField = TryCast(e.Item.FindControl("hdfDate"), HiddenField)
        Dim Literal1 As Literal = TryCast(e.Item.FindControl("Literal1"), Literal)
        ' Dim dd As String = hdfDate.Value
        Dim SessionId As Integer = 0
        'SessionId = ddlSession.SelectedValue
        Dim dtSelectedDateEvents As DataTable = BLL.GetCollectionDetails(ddlClass.SelectedValue, ddlClassTo.SelectedValue, hdfDate.Value, SessionId, ddlType.SelectedValue, ddlSession.SelectedValue)
        Dim row As DataRow
        If dtSelectedDateEvents IsNot Nothing Then
            For Each row In dtSelectedDateEvents.Rows
                Dim Dated As String = row("Dated")
                Dim SNo As Integer = row("SNo")
                If SNo > 1 Then
                    Dated = ""
                End If
                Literal1.Text &= "<tr style='height: 30px;'><td style='width: 20%'>" & Dated & "</td><td style='width: 20%;'>" & row("FeeType") & "</td><td style='width: 20%;'>" & row("CashAmount") & "</td><td style='width: 20%'>" & row("ChequeAmount") & "</td><td style='width: 20%'>" & row("TotalAmount") & "</td></tr>"
            Next row
        End If


        'DlDailyCollectionDetail.DataSource = BLL.GetCollectionDetails(ddlClass.SelectedValue, hdfDate.Value, ddlSession.SelectedValue)
        'DlDailyCollectionDetail.DataBind()
        ' End If

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
