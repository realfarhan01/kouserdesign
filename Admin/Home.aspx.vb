
Partial Class Home
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Calendar1.EventDateColumnName = "EventDate"
                Calendar1.EventDescriptionColumnName = "EventDetail"
                Calendar1.EventHeaderColumnName = "EventName"

                Calendar1.EventSource = GetEvents()

                Dim dr As SqlDataReader = BLL.ExecDataReaderProc("Get_AdminDashbordInfo")
                If dr.Read() Then
                    litProductCategories.Text = dr("TotalCategory")
                    litTotalProducts.Text = dr("TotalProducts")
                    litTotalProductImages.Text = dr("TotalProductImages")

                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Function GetEvents() As DataTable
        Dim dt As New DataTable()
        dt = BLL.ExecDataTableProc("dbo.Get_MonthEvents")

        Return dt
    End Function

    'Protected Sub Page_Load(sender As Object, e As EventArgs)
    '    Calendar1.EventDateColumnName = "EventDate"
    '    Calendar1.EventDescriptionColumnName = "EventDescription"
    '    Calendar1.EventHeaderColumnName = "EventHeader"

    '    Calendar1.EventSource = GetEvents()
    'End Sub
    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim theDates As SelectedDatesCollection = Calendar1.SelectedDates
        Dim dtEvents As DataTable = Calendar1.EventSource

        Dim dtSelectedDateEvents As DataTable = dtEvents.Clone()
        Dim dr As DataRow

        For Each drEvent As DataRow In dtEvents.Rows
            For Each selectedDate As DateTime In theDates
                If (Convert.ToDateTime(drEvent(Calendar1.EventDateColumnName))).ToShortDateString() = selectedDate.ToShortDateString() Then
                    dr = dtSelectedDateEvents.NewRow()
                    dr(Calendar1.EventDateColumnName) = drEvent(Calendar1.EventDateColumnName)
                    dr(Calendar1.EventHeaderColumnName) = drEvent(Calendar1.EventHeaderColumnName)
                    dr(Calendar1.EventDescriptionColumnName) = drEvent(Calendar1.EventDescriptionColumnName)
                    dtSelectedDateEvents.Rows.Add(dr)
                End If
            Next
        Next
        bnkdiv.Style.Add("Display", "block")
        'Page.ClientScript.RegisterStartupScript([GetType](), "JSScript", "<script type='text/javascript'> window.onload = function () {$('#BankModal').modal('show');};</script>", True)

        ClientScript.RegisterStartupScript(System.Type.[GetType]("System.String"), "JSScript", "<script type='text/javascript'> window.onload = function () {$('#BankModal').modal('show');};</script>")
        gvSelectedDateEvents.DataSource = dtSelectedDateEvents
        gvSelectedDateEvents.DataBind()

    End Sub
    
End Class
