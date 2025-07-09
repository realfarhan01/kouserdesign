
Partial Class Admin_ProductReceipt
    Inherits BasePage
    Dim bll As New BusinessLogicLayer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.QueryString("rid") Is Nothing Then
            FillReceiptDetails((New Encryption64).Decrypt(Request.QueryString("rid")))
        End If
    End Sub
    Sub FillReceiptDetails(ByVal cnt As Integer)
        Dim dt As DataTable = bll.ExecDataTableProc("Get_ProductReceipt", "@ReceiptCnt", cnt)
        rptdetails.DataSource = dt
        rptdetails.DataBind()

    End Sub

    Protected Sub rptdetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptdetails.ItemDataBound
        Dim item As RepeaterItem = e.Item
        If (item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem) Then
            Dim rptbill As Repeater = DirectCast(item.FindControl("rptbill"), Repeater)
            Dim Repeater1 As Repeater = DirectCast(item.FindControl("Repeater1"), Repeater)
            Dim drv As DataRowView = DirectCast(item.DataItem, DataRowView)
            Dim dt As DataTable = bll.ExecDataTableProc("Get_ProductReceiptdetails", "@ReceiptNoCnt", drv("cnt"))
            rptbill.DataSource = dt
            rptbill.DataBind()
            Repeater1.DataSource = dt
            Repeater1.DataBind()
        End If
    End Sub
End Class
