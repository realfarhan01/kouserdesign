Imports System.Web.Services
Imports Newtonsoft.Json
Partial Class index
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim MostPopulerProducts As DataTable = BLL.ExecDataTableProc("sp_GetMostPopulerProducts", "@PageNo", 1, "@Record", 8)
            Dim NewListedProducts As DataTable = BLL.ExecDataTableProc("sp_GetNewListedProducts", "@PageNo", 1, "@Record", 8)
            dtlMostPopulerProducts.DataSource = MostPopulerProducts
            dtlMostPopulerProducts.DataBind()
            dtlNewListedProducts.DataSource = NewListedProducts
            dtlNewListedProducts.DataBind()


            'If Request.QueryString("jfid") IsNot Nothing Then

            'End If
        End If
    End Sub
End Class
