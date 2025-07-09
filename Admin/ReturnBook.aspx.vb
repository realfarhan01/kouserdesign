
Partial Class ReturnBook
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not Request.QueryString("Bid") Is Nothing Then
                Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_LibBooksIssue where issueid=@issueid", "@issueid", Request.QueryString("Bid"))
                If dr.Read() Then
                    txtbookid.Text = dr("bookid")
                    txtStudentId.Text = dr("StudentId")
                    txtEmpId.Text = dr("EmployeeId")
                    txtIssueDate.Text = dr("IssueDate")
                End If
            End If
        End If
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Try
            Dim res As String = BLL.ReturnBook(Request.QueryString("Bid"), txtReturnDate.Text, txtExtraDays.Text, txtFine.Text)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                Clear()
            Else
                litmsg.Text = Notifications.ErrorMessage(res)
            End If


        Catch ex As Exception
            litmsg.Text = Notifications.ErrorMessage("Sorry For Inconvenience.Please Try Again Later")
        End Try

    End Sub
    Sub Clear()
        txtbookid.Text = ""
        txtStudentId.Text = ""
        txtEmpId.Text = ""
        txtIssueDate.Text = ""
        txtReturnDate.Text = ""
        txtExtraDays.Text = ""
        txtFine.Text = ""
    End Sub
    'Protected Sub DataDisplay_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles DataDisplay.RowCommand
    '    If e.CommandName = "edit1" Then
    '        Dim dr As SqlDataReader = BLL.ExecDataReader("select * from tbl_LibBooks b inner join tbl_LibBooksCategories c on b.categoryid=c.categoryid where b.bookid=@bookid", "@bookid", e.CommandArgument)
    '        If dr.Read() Then
    '            txtname.Text = dr("bookname")
    '            txtauther.Text = dr("auther")
    '            ddlcat.SelectedValue = dr("categoryid")
    '            txtbookid.Text = dr("bookid")
    '            txtpublication.Text = dr("publication")
    '            txttags.Text = dr("tags")
    '            txtprice.Text = dr("price")
    '            txttotqty.Text = dr("totalqty")
    '            hfId.Value = e.CommandArgument

    '        End If

    '    End If
    'End Sub
    'Sub bindbook()
    '    DataDisplay.DataSource = BLL.ExecDataTable("select * from tbl_LibBooks")
    '    DataDisplay.DataBind()
    'End Sub
    'Sub bindcat()
    '    ddlcat.DataSource = BLL.ExecDataTable("select * from tbl_LibBooksCategories")
    '    ddlcat.DataTextField = "categoryname"
    '    ddlcat.DataValueField = "categoryid"
    '    ddlcat.DataBind()
    'End Sub

    Protected Sub txtReturnDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtReturnDate.TextChanged
        If txtReturnDate.Text <> "" Then


            Dim dr As SqlDataReader = BLL.ExecDataReader("Select * from fn_GetLibBookReturnFine(@IssueId)", "@IssueId", Request.QueryString("Bid"), "ReturnDate", txtReturnDate.Text)
            If dr.Read() Then
                txtFine.Text = dr("")
                txtExtraDays.Text = dr("")
            End If
        End If
    End Sub
End Class
