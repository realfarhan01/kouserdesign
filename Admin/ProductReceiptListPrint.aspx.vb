
Partial Class Admin_ProductReceiptListPrint
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'bind()
            ltrSchool.Text = BLL.BindSchoolHeader()
            ddlClass.DataSource = BLL.BindMainClass()

            ddlClass.DataTextField = "MainClassName"
            ddlClass.DataValueField = "MainClassId"
            ddlClass.DataBind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.GetProductReceiptSummary(txtFromDate.Text, txtToDate.Text, ddlClass.SelectedValue, txtStudentid.Text, "", ddlSchoolSession.SelectedValue)
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                reportheader.Visible = True
            End If
        End If
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        bind()
    End Sub
    Private total As Decimal = 0
    Protected Sub gvEmp_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ReceiptAmount"))
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim lblamount As Label = DirectCast(e.Row.FindControl("lblTotal"), Label)
            lblamount.Text = total.ToString()
        End If
    End Sub
End Class

