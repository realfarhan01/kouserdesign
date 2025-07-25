﻿
Partial Class ProductBilling
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtReceiptno.Text = BLL.ExecScalar("Select dbo.GetNewProductReceiptNoSession(@SchoolSession)", "@SchoolSession", ddlSession.SelectedValue)
            txtReceiptnoPaid.Text = txtReceiptno.Text
            hfId.Value = 0
            CreateTable()
            If Not Session("ReceiptDateProduct") Is Nothing Then
                txtDueDate.Text = Session("ReceiptDateProduct")
            End If

            Dim todaysdate As String = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            txtDueDatePaid.Text = todaysdate
            BindStudentData()
            BuildDataDisplay()
            activateCashChequeDiv()
        End If
    End Sub
    Sub BindStudentData()
        ddlsearch.DataSource = BLL.ExecDataTableProc("Prc_StudentData")
        ddlsearch.DataTextField = "StudentData"
        ddlsearch.DataValueField = "StudentId"
        ddlsearch.DataBind()
    End Sub
    Sub activateCashChequeDiv()
        If ddlpmodePaid.Text = "Cash" Then
            divCashAmount.Style.Add("display", "block")
            divchequeDetails.Style.Add("display", "none")
            divChequeAmount.Style.Add("display", "none")
        ElseIf ddlpmodePaid.Text = "Cheque" Then
            divCashAmount.Style.Add("display", "none")
            divchequeDetails.Style.Add("display", "block")
            divChequeAmount.Style.Add("display", "block")
        ElseIf ddlpmodePaid.Text = "Cash+Cheque" Then
            divCashAmount.Style.Add("display", "block")
            divchequeDetails.Style.Add("display", "block")
            divChequeAmount.Style.Add("display", "block")
        Else
            divCashAmount.Style.Add("display", "none")
            divchequeDetails.Style.Add("display", "none")
            divChequeAmount.Style.Add("display", "none")
        End If
    End Sub
    Sub CreateTable()
        ViewState("tblFee") = Nothing
        Dim workTable As DataTable = New DataTable("tblFee")
        workTable.Columns.Add("RowID", Type.GetType("System.Int32"))
        workTable.Columns.Add("ProductId", Type.GetType("System.Int32"))
        workTable.Columns.Add("ProductName", Type.GetType("System.String"))
        workTable.Columns.Add("ProductType", Type.GetType("System.String"))
        workTable.Columns.Add("Amount", Type.GetType("System.Double"))
        workTable.Columns.Add("Qty", Type.GetType("System.Int32"))
        workTable.Columns.Add("TotalAmount", Type.GetType("System.Double"))
        ViewState("tblFee") = workTable
    End Sub
    Protected Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Response.Redirect("ProductReceipt.aspx?rid=" + (New Encryption64).Encrypt(hdnReceiptId.Value))
    End Sub
    Sub BuildDataDisplay()
        ' Dim _workTable As DataTable
        Dim workTable As DataTable = TryCast(ViewState("tblFee"), DataTable)
        DataDisplay.DataSource = workTable
        DataDisplay.DataBind()
        txtReceiptAmt.Text = workTable.Compute("Sum(TotalAmount)", "").ToString()
        Dim recAmt As Decimal = 0
        If txtReceiptAmt.Text = "" Then
            txtReceiptAmt.Text = 0
        End If

        recAmt = Convert.ToDecimal(txtReceiptAmt.Text)

        If txtDiscount.Text = "" Then
            txtDiscount.Text = 0
        End If
        Dim Discount As Decimal = Convert.ToDecimal(txtDiscount.Text)
        txtNetAmount.Text = ((recAmt) - Discount).ToString("0.00")
        ' _workTable = workTable.Clone()
        'If workTable.Rows.Count > 0 Then

        ' Else
        'Dim dr As DataRow = _workTable.NewRow
        '_workTable.Rows.Add(dr)
        'Dim columnsCount As Integer
        'If DataDisplay.Columns.Count = 0 Then
        '    columnsCount = _workTable.Columns.Count
        'Else
        '    columnsCount = DataDisplay.Columns.Count
        'End If
        'DataDisplay.DataSource = _workTable
        'DataDisplay.DataBind()
        'DataDisplay.Rows(0).Cells.Clear() '// clear all the cells in the row
        'DataDisplay.Rows(0).Cells.Add(New TableCell()) ' //add a new blank cell
        'DataDisplay.Rows(0).Cells(0).ColumnSpan = columnsCount ' //set the column span to the new added cell

        '' //You can set the styles here
        'DataDisplay.Rows(0).Cells(0).HorizontalAlign = HorizontalAlign.Center ';

        'DataDisplay.Rows(0).Cells(0).Font.Bold = True '
        '' //set No Results found to the new added cell
        'DataDisplay.Rows(0).Cells(0).Text = "No Fee Record!" '
        'DataDisplay.DataSource = workTable
        'DataDisplay.DataBind()
        'txtReceiptAmt.Text = "0"

        'End If

    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim workTable As DataTable = DirectCast(ViewState("tblFee"), DataTable)

            Dim dt As New DataTable("temp")

            dt.Columns.Add("Product", Type.GetType("System.String"))
            dt.Columns.Add("ProductId", Type.GetType("System.Int32"))
            dt.Columns.Add("Amount", Type.GetType("System.Double"))
            dt.Columns.Add("Qty", Type.GetType("System.Int32"))
            dt.Columns.Add("TotalAmount", Type.GetType("System.Double"))

            'For Each r As DataRow In workTable.Rows
            '    dt.Rows.Add(New Object() {r.Item("ProductName"), r.Item("ProductId"), r.Item("Amount")})
            'Next

            Dim lblProductName As New Label
            Dim lblAmount As New Label
            Dim txtQty As New TextBox
            Dim hdnProductId As New HiddenField
            Dim rdbselect As New CheckBox

            For Each mRow As GridViewRow In DataDisplay.Rows
                lblProductName = mRow.FindControl("lblFeeType")
                lblAmount = mRow.FindControl("lblAmount")
                txtQty = mRow.FindControl("txtQty")
                rdbselect = mRow.FindControl("rdbselect")
                hdnProductId = mRow.FindControl("HiddenField2")
                Dim Qty As Integer = Val(txtQty.Text)
                Dim ProductAmount As Decimal = Val(lblAmount.Text)
                Dim TotalAmount As Decimal = Qty * ProductAmount
                If rdbselect.Checked Then
                    dt.Rows.Add(New Object() {lblProductName.Text, hdnProductId.Value, ProductAmount, Qty, TotalAmount})
                End If
            Next

            Dim res As String = ""
            If dt.Rows.Count > 0 Then
                res = BLL.AddProductReceipt(txtStudentId.Text, txtReceiptno.Text, Val(txtReceiptAmt.Text), txtDiscount.Text, txtDueDate.Text, Session("Operator"), dt, ddlSession.SelectedValue)
                If res.Chars(0) = "#" Then
                    Dim arr As String() = res.Split("~")
                    If (arr.Length > 1) Then
                        hdnReceiptId.Value = arr(2)
                        Session("ReceiptDateProduct") = txtDueDate.Text
                        txtDueDatePaid.Text = txtDueDate.Text
                        btnPay.Visible = True
                        btnPreview.Visible = True
                        ddlsearch.Enabled = False
                        txtDiscount.ReadOnly = True
                        txtNetAmount.Text = Val(txtReceiptAmt.Text) - Val(txtDiscount.Text)
                    End If
                    litmsg.Text = Notifications.SuccessMessage(res)
                    btnSubmit.Enabled = False
                    ' Response.Redirect("PayProduct.aspx")
                End If
            End If
        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Error in Process. Please Try Later.")
        End Try

    End Sub
    Protected Sub btnSubmitPaid_Click(sender As Object, e As EventArgs) Handles btnSubmitPaid.Click
        Try
            If hdnReceiptId.Value <> "" Then
                Dim chequeAmt As Decimal = 0, cashAmt As Decimal = 0
                If (txtChequeAmount.Text.Trim() <> "") Then
                    chequeAmt = Convert.ToDecimal(txtChequeAmount.Text.Trim())
                End If
                If (txtCashAmount.Text.Trim() <> "") Then
                    cashAmt = Convert.ToDecimal(txtCashAmount.Text.Trim())
                End If
                Dim res As String = BLL.PayProduct(txtReceiptnoPaid.Text, txtDueDatePaid.Text, txtremarkPaid.Text, 0, ddlpmodePaid.SelectedValue, txtChequeNo.Text, txtChequeDate.Text, chequeAmt, ddlBankName.SelectedValue, txtBranchName.Text, cashAmt, ddlSession.SelectedValue)
                If res.Chars(0) = "#" Then
                    litmsg.Text = Notifications.SuccessMessage(res)
                    btnSubmitPaid.Enabled = False
                    btnPay.Visible = False
                    btnPreview.Text = "Print Receipt"
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalscript", "$('#ctl00_C1_pnlpay').css('visibility', 'hidden');", True)
                    'Response.Redirect("ProductReceipt.aspx?rid=" + (New Encryption64).Encrypt(hdnReceiptId.Value))
                Else
                    litmsg.Text = Notifications.ErrorMessage(res)
                End If
            Else
                litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
            End If

        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try
        activateCashChequeDiv()
        pnlpay.Style.Add("visibility", "visible")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim sbvaild As String = (New BusinessLogicLayer).DisabledButtonCode("UserRegistration") & Page.ClientScript.GetPostBackEventReference(btnSubmit, Nothing) & ";"
        btnSubmit.Attributes.Add("onclick", sbvaild)

    End Sub

    Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
        If e.CommandName = "Insert" Then
            Dim workTable As DataTable = DirectCast(ViewState("tblFee"), DataTable)
            Dim ddlnewFeeType As DropDownList = DirectCast(DataDisplay.FooterRow.FindControl("ddlnewFeeType"), DropDownList)
            Dim ddlnewTerm As DropDownList = DirectCast(DataDisplay.FooterRow.FindControl("ddlnewTerm"), DropDownList)
            Dim txtnewAmount As TextBox = DirectCast(DataDisplay.FooterRow.FindControl("txtnewAmount"), TextBox)
            Dim txtnewDueDate As TextBox = DirectCast(DataDisplay.FooterRow.FindControl("txtnewDueDate"), TextBox)
            'Dim FeeRows() As Data.DataRow


            If Val(ddlnewFeeType.Text <> "" And txtnewAmount.Text > 0 And ddlnewTerm.SelectedValue > 0) Then
                hfId.Value = hfId.Value + 1

                'Dim wRows() As Data.DataRow
                'Dim AvlStock As Integer = 0
                'wRows = workTable.Select("FeeType='" & ddlnewFeeType.SelectedValue & "'")
                'Dim s As String = wRows("qty").ToString
                'If wRows.Length > 0 Then
                '    AvlStock = Convert.ToInt32(txtnewqty.Text) + wRows(0)("instock")
                'Else
                '    AvlStock = Convert.ToInt32(txtnewqty.Text)
                'End If
                workTable.Rows.Add(New Object() {hfId.Value, ddlnewFeeType.SelectedValue.ToString, ddlnewTerm.SelectedValue, Val(txtnewAmount.Text), txtnewDueDate.Text, ddlnewTerm.SelectedItem.Text.ToString()})

                ViewState("tblFee") = workTable
            Else
                litmsg.Text = Notifications.ErrorMessage("Enter Valid Data.")
                Exit Sub
            End If


            BuildDataDisplay()
        ElseIf e.CommandName = "updateqty" Then

            Dim dt As New DataTable("temp")

            dt.Columns.Add("RowID", Type.GetType("System.Int32"))
            dt.Columns.Add("ProductId", Type.GetType("System.Int32"))
            dt.Columns.Add("ProductName", Type.GetType("System.String"))
            dt.Columns.Add("ProductType", Type.GetType("System.String"))
            dt.Columns.Add("Amount", Type.GetType("System.Double"))
            dt.Columns.Add("Qty", Type.GetType("System.Int32"))
            dt.Columns.Add("TotalAmount", Type.GetType("System.Double"))

            Dim lblProductName As New Label
            Dim lblAmount As New Label
            Dim lblProductType As New Label
            Dim txtQty As New TextBox
            Dim hdnProductId As New HiddenField
            Dim rdbselect As New CheckBox
            Dim hfRowID As New HiddenField

            For Each mRow As GridViewRow In DataDisplay.Rows
                lblProductName = mRow.FindControl("lblFeeType")
                lblProductType = mRow.FindControl("lblProductType")
                lblAmount = mRow.FindControl("lblAmount")
                txtQty = mRow.FindControl("txtQty")
                hdnProductId = mRow.FindControl("HiddenField2")
                hfRowID = mRow.FindControl("HiddenField1")
                rdbselect = mRow.FindControl("rdbselect")

                Dim Qty As Integer = Val(txtQty.Text)
                Dim ProductAmount As Decimal = Val(lblAmount.Text)
                Dim TotalAmount As Decimal = 0
                TotalAmount = (Qty * ProductAmount)
                If rdbselect.Checked = True Then
                    dt.Rows.Add(New Object() {hfRowID.Value, hdnProductId.Value, lblProductName.Text, lblProductType.Text, ProductAmount, Qty, TotalAmount})
                End If
            Next
            ViewState("tblFee") = dt
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

    'Protected Sub DataDisplay_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataDisplay.RowDataBound
    '    Dim ddlnewFeeType As New System.Web.UI.WebControls.DropDownList
    '    Dim ddlnewTerm As New System.Web.UI.WebControls.DropDownList
    '    'If e.Row.RowType = DataControlRowType.DataRow Then
    '    ddlnewFeeType.DataSource = BLL.BindFeeType()
    '    ddlnewFeeType.DataTextField = "FeeType"
    '    ddlnewFeeType.DataValueField = "FeeType"
    '    ddlnewFeeType.DataBind()

    '    ddlnewTerm.DataSource = BLL.BindTermFrequency()
    '    ddlnewTerm.DataTextField = "Frequency"
    '    ddlnewTerm.DataValueField = "TermId"
    '    ddlnewTerm.DataBind()
    '    'End If
    'End Sub
    Protected Sub DataDisplay_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataDisplay.RowDataBound

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim ddlnewFeeType As DropDownList = DirectCast(e.Row.FindControl("ddlnewFeeType"), DropDownList)
        '    If ddlnewFeeType IsNot Nothing Then
        '        ddlnewFeeType.DataSource = Inv.ExecDataTable("select id,FullName from tbl_inv_products where IsDelete=0 and IsActive=1")
        '        ddlnewFeeType.DataTextField = "FullName"
        '        ddlproduct.DataValueField = "Id"
        '        ddlproduct.DataBind()
        '        ddlproduct.SelectedValue = DataDisplay.DataKeys(e.Row.RowIndex).Values(0).ToString()
        '    End If
        'End If
        'If e.Row.RowType = DataControlRowType.Footer Then
        '    Dim ddlnewFeeType As DropDownList = DirectCast(e.Row.FindControl("ddlnewFeeType"), DropDownList)

        '    ddlnewFeeType.DataSource = BLL.BindFeeType()
        '    ddlnewFeeType.DataTextField = "FeeType"
        '    ddlnewFeeType.DataValueField = "FeeType"
        '    ddlnewFeeType.DataBind()
        '    ddlnewFeeType.Focus()
        'End If
        'If e.Row.RowType = DataControlRowType.Footer Then
        '    Dim ddlnewTerm As DropDownList = DirectCast(e.Row.FindControl("ddlnewTerm"), DropDownList)

        '    ddlnewTerm.DataSource = BLL.BindTermFrequency()
        '    ddlnewTerm.DataTextField = "Frequency"
        '    ddlnewTerm.DataValueField = "TermId"
        '    ddlnewTerm.DataBind()
        '    ddlnewTerm.Focus()
        'End If

    End Sub

    Protected Sub DataDisplay_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DataDisplay.RowDeleting
        Dim workTable As DataTable = DirectCast(ViewState("tblFee"), DataTable)
        Dim id As Integer = Convert.ToInt32(DataDisplay.DataKeys(e.RowIndex).Values(0).ToString())
        'Dim rows As DataRow() = workTable.[Select]("ID = '" & id & "'")
        Dim rowID As Integer = ItemIndexOfID(id, workTable)
        workTable.Rows(rowID).Delete()
        ViewState("tblFee") = workTable
        BuildDataDisplay()
    End Sub

    'Protected Sub DataDisplay_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles DataDisplay.RowEditing
    '    DataDisplay.EditIndex = e.NewEditIndex
    '    BuildDataDisplay()
    'End Sub

    'Protected Sub DataDisplay_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles DataDisplay.RowCancelingEdit
    '    DataDisplay.EditIndex = -1
    '    BuildDataDisplay()
    'End Sub

    'Protected Sub DataDisplay_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles DataDisplay.RowUpdating
    '    'Dim ddlproduct As DropDownList = DirectCast(DataDisplay.Rows(e.RowIndex).FindControl("ddlproduct"), DropDownList)
    '    Dim txtproductcode As TextBox = DirectCast(DataDisplay.Rows(e.RowIndex).FindControl("txtproductcode"), TextBox)
    '    Dim txtqty As TextBox = DirectCast(DataDisplay.Rows(e.RowIndex).FindControl("txtqty"), TextBox)
    '    Dim txtVATvalue As TextBox = DirectCast(DataDisplay.Rows(e.RowIndex).FindControl("txtVATvalue"), TextBox)
    '    'Dim dtProduct As DataTable = Inv.ExecDataTable("select * from tbl_inv_products where IsDelete=0 and IsActive=1 and id=@id", "@id", ddlproduct.SelectedValue)
    '    Dim productsTable As DataTable = DirectCast(ViewState("tblProducts"), DataTable)
    '    Dim productRows() As Data.DataRow
    '    Dim Oldqty As Integer = 0

    '    'If ddlcnf.SelectedValue = 0 Then
    '    productRows = productsTable.Select("itemcode='" & txtproductcode.Text & "'")
    '    'Else
    '    '    productRows = productsTable.Select("code='" & txtproductcode.Text & "' and instock>=" & txtqty.Text)
    '    'End If

    '    If productRows.Length = 0 Then
    '        lblmsg.Text = Notifications.InfoMessage("Selected product is not valid.Select product to order.")
    '        Exit Sub
    '    End If
    '    If String.IsNullOrEmpty(txtqty.Text) Then
    '        lblmsg.Text = Notifications.ErrorMessage("Enter product quantity.")
    '        Exit Sub
    '    End If
    '    Dim workTable As DataTable = DirectCast(ViewState("tblOrder"), DataTable)
    '    For Each r As DataRow In workTable.Rows
    '        If Convert.ToString(r.Item("itemcode")) = productRows(0)("itemcode") Then
    '            Oldqty = Convert.ToInt32(r.Item("Qty"))
    '        End If
    '    Next
    '    If (Val(txtqty.Text) - Oldqty) * productRows(0)("item_Price") > Val(LitRemainingAmount.Text) Then
    '        lblmsg.Text = Notifications.ErrorMessage("Not Available enough Remaining Amount.")
    '    Else

    '        If Val(txtqty.Text > 0) Then
    '            Dim RowID As Integer = ItemIndexOfID(productRows(0)("item_id"), workTable)
    '            Dim mrow As DataRow = workTable.Rows(RowID)
    '            mrow("qty") = txtqty.Text
    '            'mrow("qty") = txtVATvalue.Text
    '            Dim dr As DataRow = workTable.NewRow

    '            dr("ProductID") = productRows(0)("item_id")
    '            dr("itemcode") = productRows(0)("itemcode")
    '            dr("productname") = productRows(0)("item_Name")
    '            dr("UnitCost") = productRows(0)("item_Price")
    '            dr("VATvalue") = txtVATvalue.Text
    '            dr("instock") = productRows(0)("item_Qty")
    '            dr("Qty") = mrow("qty")
    '            dr("Bv") = productRows(0)("pv")
    '            dr("TotalAmount") = productRows(0)("item_Price") * mrow("qty")
    '            dr("TotalVAT") = (mrow("qty") * productRows(0)("item_Price")) / (1 + ((100 - txtVATvalue.Text) / 100))
    '            dr("ActualAmount") = (productRows(0)("item_Price") * mrow("qty")) - (mrow("qty") * productRows(0)("item_Price")) / (1 + ((100 - txtVATvalue.Text) / 100))
    '            dr("totalbv") = productRows(0)("pv") * mrow("Qty")


    '            LitOrderAmount.Text = dr("TotalAmount")
    '            LitRemainingAmount.Text = Val(litTotalAmount.Text) - Val(LitOrderAmount.Text)

    '            workTable.Rows.RemoveAt(e.RowIndex)
    '            workTable.Rows.InsertAt(dr, e.RowIndex)
    '            'workTable.Rows.Add(dr)
    '            workTable.AcceptChanges()
    '            ViewState("tblOrder") = workTable
    '            DataDisplay.EditIndex = -1

    '        Else
    '            lblmsg.Text = Notifications.ErrorMessage("Quantity must be positive or greater then zero (0).")

    '        End If
    '    End If
    '    BuildDataDisplay()
    'End Sub
    Protected Sub ddlsearch_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlsearch.SelectedIndexChanged
        Try
            If ddlsearch.SelectedValue <> "" Then
                Dim studentid As String = ddlsearch.SelectedValue
                Dim dr As SqlDataReader = BLL.ExecDataReader("Select s.StudentId, s.StudentName,c.MainClassId,c.MainClassName from StudentMaster S left join Mainclassmaster c on s.MainClassId=c.MainClassId Where s.StudentId=@StudentId", "@StudentId", studentid)
                If dr.Read() Then
                    txtStudentId.Text = dr("StudentId")
                    txtName.Text = dr("StudentName")
                    txtClass.Text = dr("MainClassName")
                    hfId.Value = dr("MainClassId")

                    Dim ProductTable As DataTable = BLL.ExecDataTable("Select RowId,ProductId,ProductName,ProductType,Amount,1 Qty,Amount TotalAmount from vwProductMaster Where BalanceQuantity>0 and MainClassId=@MainClassId and SchoolSession=@SchoolSession", "@MainClassId", hfId.Value, "@SchoolSession", ddlSession.SelectedValue)
                    ViewState("tblFee") = ProductTable

                    BuildDataDisplay()
                End If
            End If
        Catch ex As Exception
            litmsg.Text = "Error in Process. Please Try Later."
        End Try
    End Sub
    Protected Sub txtStudentId_TextChanged(sender As Object, e As System.EventArgs) Handles txtStudentId.TextChanged
        Try
            Dim dr As SqlDataReader = BLL.ExecDataReader("Select s.StudentName,s.MainClassId,c.ClassName from StudentMaster S left join classmaster c on s.ClassId=c.ClassId Where s.StudentId=@StudentId", "@StudentId", txtStudentId.Text)
            If dr.Read() Then
                txtName.Text = dr("StudentName")
                txtClass.Text = dr("ClassName")
                hfId.Value = dr("MainClassId")

                Dim ProductTable As DataTable = BLL.ExecDataTable("Select RowId,ProductId,ProductName,ProductType,Amount,1 Qty,Amount TotalAmount from vwProductMaster Where MainClassId=@MainClassId and SchoolSession=@SchoolSession", "@MainClassId", hfId.Value, "@SchoolSession", ddlSession.SelectedValue)
                ViewState("tblFee") = ProductTable

                BuildDataDisplay()
            End If
        Catch ex As Exception
            litmsg.Text = "Error in Process. Please Try Later."
        End Try
    End Sub
    Protected Sub ddlSession_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSession.SelectedIndexChanged
        txtReceiptno.Text = BLL.ExecScalar("Select dbo.GetNewProductReceiptNoSession(@SchoolSession)", "@SchoolSession", ddlSession.SelectedValue)
        txtReceiptnoPaid.Text = txtReceiptno.Text


        Dim ProductTable As DataTable = BLL.ExecDataTable("Select RowId,ProductId,ProductName,ProductType,Amount,1 Qty,Amount TotalAmount from vwProductMaster Where BalanceQuantity>0 and MainClassId=@MainClassId and SchoolSession=@SchoolSession", "@MainClassId", hfId.Value, "@SchoolSession", ddlSession.SelectedValue)
        ViewState("tblFee") = ProductTable

        BuildDataDisplay()
    End Sub
End Class
