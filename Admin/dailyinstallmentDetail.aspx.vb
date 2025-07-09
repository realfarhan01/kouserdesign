Imports System.Web.Services
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.IO
Imports iTextSharp.tool.xml
Partial Class Admin_dailyinstallmentDetail
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Dim Json As New JsonToDataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
            ltrSchool.Text = BLL.BindSchoolHeader()
            bind()

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
        littotal.Text = " || " + ddlType.SelectedItem.Text + " Installment Detail"
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
        Dim dtSelectedDateEvents As DataTable = BLL.GetDailyCollectionDetails(ddlClass.SelectedValue, ddlClassTo.SelectedValue, hdfDate.Value, "", SessionId, ddlType.SelectedValue, ddlSession.SelectedValue)
        Dim row As DataRow
        If dtSelectedDateEvents IsNot Nothing Then
            For Each row In dtSelectedDateEvents.Rows
                Literal1.Text &= "<tr style='height: 30px; width:100%;'><td style='width: 5%'>&nbsp;</td><td style='width: 5%'>" & row("StudentId") & "</td><td style='width: 10%'>" & row("StudentName") & "</td><td style='width: 4%'>" & row("Class") & "</td><td style='width: 4%;'>" & row("Installment1") & "</td><td style='width: 4%'>" & row("Installment2") & "</td><td style='width: 4%'>" & row("Installment3") & "</td><td style='width: 4%'>" & row("Installment4") & "</td><td style='width: 4%'>" & row("Installment5") & "</td><td style='width: 5%'>" & row("Installment6") & "</td><td style='width: 5%'>" & row("Installment7") & "</td><td style='width: 5%'>" & row("AdmissionFee") & "</td><td style='width: 5%'>" & row("ConveyanceFee") & "</td><td style='width: 5%'>" & row("SportsFee") & "</td><td style='width: 5%'>" & row("LateFee") & "</td><td style='width: 5%'>" & row("OtherFee") & "</td><td style='width: 5%'>" & row("Discount") & "</td><td style='width: 5%'>" & row("TotalAmount") & "</td><td style='width: 5%'>" & row("ChequeNo") & "</td><td style='width: 5%'>" & row("Period") & "</td></tr>"
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
