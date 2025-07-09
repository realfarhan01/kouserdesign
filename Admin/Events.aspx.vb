
Partial Class Admin_Events
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Private Function GetEvents() As DataTable
        Dim dt As New DataTable()
        dt = BLL.ExecDataTableProc("dbo.Get_MonthEvents")

        'dt.Columns.Add("EventDate", Type.[GetType]("System.DateTime"))
        'dt.Columns.Add("EventHeader", Type.[GetType]("System.String"))
        'dt.Columns.Add("EventDescription", Type.[GetType]("System.String"))

        'Dim dr As DataRow

        '' Last Week's Events
        'dr = dt.NewRow()
        'dr("EventDate") = DateTime.Now.AddDays(-7)
        'dr("EventHeader") = "My Last Week's Event 1"
        'dr("EventDescription") = "My Last Week's Event 1 Description"
        'dt.Rows.Add(dr)

        '' Yesterday's Events
        'dr = dt.NewRow()
        'dr("EventDate") = DateTime.Now.AddDays(-1)
        'dr("EventHeader") = "My Yesterday's Event 1"
        'dr("EventDescription") = "My Yesterday's Event 1 Description"
        'dt.Rows.Add(dr)

        '' Todays Events
        'dr = dt.NewRow()
        'dr("EventDate") = DateTime.Now
        'dr("EventHeader") = "My Todays Event 1"
        'dr("EventDescription") = "My Todays Event 1 Description"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("EventDate") = DateTime.Now
        'dr("EventHeader") = "My Todays Event 2"
        'dr("EventDescription") = "My Todays Event 2 Description"
        'dt.Rows.Add(dr)

        '' Tomorrow's Events
        'dr = dt.NewRow()
        'dr("EventDate") = DateTime.Now.AddDays(1)
        'dr("EventHeader") = "My Tomorrow's Event 1"
        'dr("EventDescription") = "My Tomorrow's Event 1 Description"
        'dt.Rows.Add(dr)

        Return dt
    End Function

    'Protected Sub Page_Load(sender As Object, e As EventArgs)
    '    Calendar1.EventDateColumnName = "EventDate"
    '    Calendar1.EventDescriptionColumnName = "EventDescription"
    '    Calendar1.EventHeaderColumnName = "EventHeader"

    '    Calendar1.EventSource = GetEvents()
    'End Sub
    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs)
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


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Calendar1.EventDateColumnName = "EventDate"
            Calendar1.EventDescriptionColumnName = "EventDetail"
            Calendar1.EventHeaderColumnName = "EventName"

            Calendar1.EventSource = GetEvents()
        End If
        
    End Sub
End Class
