
Partial Class productdetail
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                If Request.QueryString("pc") IsNot Nothing Then
                    Dim dr As SqlDataReader = BLL.ExecDataReaderProc("sp_GetProductDetails", "@CatCode", Request.QueryString("pc").ToString())
                    If dr.Read() Then
                        Dim CatalogueId As Integer = dr("CatalogueId")
                        LitProductName.Text = dr("CatalogueTitle")
                        LitPrice.Text = dr("price")
                        'LitProductCode.Text = dr("CatCode")
                        'LitProductWeight.Text = dr("Weight")
                        LitProductSKUCode.Text = dr("SKUCode")
                        LitCategory.Text = dr("CategoryName")
                        LitDesc.Text = dr("CatalogueDescription")
                        LitMainCategory.Text = dr("MainCategoryName")

                        Dim dtProductImages As DataTable = BLL.ExecDataTableProc("sp_GetProductImages", "@CatalogueId", CatalogueId, "@Type", 3)
                        dtlProductImages.DataSource = dtProductImages
                        dtlProductImages.DataBind()

                        Dim dtProductThumbnails As DataTable = BLL.ExecDataTableProc("sp_GetProductImages", "@CatalogueId", CatalogueId, "@Type", 3)
                        dtlProductThumbnails.DataSource = dtProductImages
                        dtlProductThumbnails.DataBind()
                    End If

                    divproduct.Visible = True
                    divnotfound.Visible = False
                Else
                    divproduct.Visible = False
                    divnotfound.Visible = True
                End If
            Catch ex As Exception
                divproduct.Visible = False
                divnotfound.Visible = True
            End Try


            'If Request.QueryString("jfid") IsNot Nothing Then

            'End If
        End If
    End Sub

End Class
