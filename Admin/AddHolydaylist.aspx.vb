
Partial Class AddHolydaylist
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            hfId.Value = 0
            CreateTable()
            BuildDataDisplay()
        End If
    End Sub
    Sub CreateTable()
        Dim workTable As DataTable = New DataTable("tblHolyday")

        workTable.Columns.Add("RowID", Type.GetType("System.Int32"))
        workTable.Columns.Add("Holiday", Type.GetType("System.String"))
        workTable.Columns.Add("FromDate", Type.GetType("System.String"))
        workTable.Columns.Add("ToDate", Type.GetType("System.String"))
        workTable.Columns.Add("IsActive", Type.GetType("System.Int32"))
        ViewState("tblHolyday") = workTable
    End Sub
    Sub BuildDataDisplay()
        Dim _workTable As DataTable
        Dim workTable As DataTable = TryCast(ViewState("tblHolyday"), DataTable)
        _workTable = workTable.Clone()
        If workTable.Rows.Count > 0 Then
            DataDisplay.DataSource = workTable
            DataDisplay.DataBind()


        Else
            Dim dr As DataRow = _workTable.NewRow
            _workTable.Rows.Add(dr)
            Dim columnsCount As Integer
            If DataDisplay.Columns.Count = 0 Then
                columnsCount = _workTable.Columns.Count
            Else
                columnsCount = DataDisplay.Columns.Count
            End If
            DataDisplay.DataSource = _workTable
            DataDisplay.DataBind()
            DataDisplay.Rows(0).Cells.Clear() '// clear all the cells in the row
            DataDisplay.Rows(0).Cells.Add(New TableCell()) ' //add a new blank cell
            DataDisplay.Rows(0).Cells(0).ColumnSpan = columnsCount ' //set the column span to the new added cell

            ' //You can set the styles here
            DataDisplay.Rows(0).Cells(0).HorizontalAlign = HorizontalAlign.Center ';

            DataDisplay.Rows(0).Cells(0).Font.Bold = True '
            ' //set No Results found to the new added cell
            DataDisplay.Rows(0).Cells(0).Text = "No Fee Record!" '
        End If

    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim workTable As DataTable = DirectCast(ViewState("tblHolyday"), DataTable)

            Dim dt As New DataTable("temp")

            workTable.Columns.Add("Holiday", Type.GetType("System.String"))
            workTable.Columns.Add("FromDate", Type.GetType("System.String"))
            workTable.Columns.Add("ToDate", Type.GetType("System.String"))
            workTable.Columns.Add("IsActive", Type.GetType("System.Int32"))

            For Each r As DataRow In workTable.Rows
                dt.Rows.Add(New Object() {r.Item("Holiday"), r.Item("FromDate"), r.Item("ToDate"), r.Item("IsActive")})
            Next


            Dim res As String = ""
            res = BLL.AddHolydays(dt)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)

            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Error in Process. Please Try Later.")
        End Try

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim sbvaild As String = (New BusinessLogicLayer).DisabledButtonCode("UserRegistration") & Page.ClientScript.GetPostBackEventReference(btnSubmit, Nothing) & ";"
        btnSubmit.Attributes.Add("onclick", sbvaild)

    End Sub

    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "Insert" Then
            Dim workTable As DataTable = DirectCast(ViewState("tblHolyday"), DataTable)
            Dim ddlNewActive As DropDownList = DirectCast(DataDisplay.FooterRow.FindControl("ddlNewActive"), DropDownList)
            Dim txtnewHoliday As TextBox = DirectCast(DataDisplay.FooterRow.FindControl("txtnewHoliday"), TextBox)
            Dim txtnewFromDate As TextBox = DirectCast(DataDisplay.FooterRow.FindControl("txtnewFromDate"), TextBox)
            Dim txtnewToDate As TextBox = DirectCast(DataDisplay.FooterRow.FindControl("txtnewToDate"), TextBox)



            If Val(ddlNewActive.Text <> "" And txtnewFromDate.Text <> "" And txtnewHoliday.Text <> "") Then
                hfId.Value = hfId.Value + 1

               
                workTable.Rows.Add(New Object() {hfId.Value, Val(txtnewHoliday.Text), Val(txtnewFromDate.Text), Val(txtnewToDate.Text), ddlNewActive.SelectedValue})

                ViewState("tblHolyday") = workTable
            Else
                litmsg.Text = Notifications.ErrorMessage("Enter Valid Data.")
                Exit Sub
            End If


            BuildDataDisplay()
        End If
    End Sub

    Private Function ItemIndexOfID(ByVal RowId As Integer, ByVal dt As DataTable) As Integer
        Dim index As Integer = 0
        For Each mrow As DataRow In dt.Rows
            If (mrow("RowId").ToString() = RowId) Then
                Return index
            End If
            index = (index + 1)
        Next
        Return -1
    End Function

   
   

    Protected Sub DataDisplay_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DataDisplay.RowDeleting
        Dim workTable As DataTable = DirectCast(ViewState("tblHolyday"), DataTable)
        Dim id As Integer = Convert.ToInt32(DataDisplay.DataKeys(e.RowIndex).Values(0).ToString())
        'Dim rows As DataRow() = workTable.[Select]("ID = '" & id & "'")
        Dim rowID As Integer = ItemIndexOfID(id, workTable)
        workTable.Rows(rowID).Delete()
        ViewState("tblHolyday") = workTable
        BuildDataDisplay()
    End Sub

    
End Class
