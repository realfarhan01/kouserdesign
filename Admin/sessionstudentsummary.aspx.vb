Imports System.Web.Services
Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.IO
Imports iTextSharp.tool.xml
Partial Class Admin_sessionstudentsummary
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Dim Json As New JsonToDataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ltrSchool.Text = BLL.BindSchoolHeader()
            bind()
        End If
    End Sub

    Sub bind()
        DlDailyCollection.DataSource = BLL.ExecDataTableProc("Prc_GetStudentSessionSummary")
        DlDailyCollection.DataBind()
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim stringWriter As New StringWriter()
        Dim htmlTextWriter As New HtmlTextWriter(stringWriter)
        employeelistDiv.RenderControl(htmlTextWriter)

        Dim stringReader As New StringReader(stringWriter.ToString())
        Dim Doc As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
        Dim htmlparser As New HTMLWorker(Doc)
        PdfWriter.GetInstance(Doc, Response.OutputStream)

        Doc.Open()
        htmlparser.Parse(stringReader)
        Doc.Close()
        Response.Write(Doc)
        Response.[End]()
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
