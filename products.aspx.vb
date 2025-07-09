Imports System.Web.Services
Imports Newtonsoft.Json
Partial Class products
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim dtCategories As DataTable = BLL.GetCategories(0, 0)
            HttpContext.Current.Session("Categories") = dtCategories
            Dim catID As String = "0"
            Dim catcode As String = "na"
            If Request.QueryString("cat") IsNot Nothing Then
                catcode = Request.QueryString("cat").ToString()
            End If
            If catcode = "na" Then
                LitCat.Text = "Love & Luxury Store"
            Else

            End If
            Dim dtProducts As DataTable = BLL.ExecDataTableProc("sp_GetCategoryProducts", "@CategoryCode", catcode, "@PageNo", 1, "@Record", 500)
            dtlMostPopulerProducts.DataSource = dtProducts
            dtlMostPopulerProducts.DataBind()
        End If
    End Sub
End Class
