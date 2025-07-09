Imports System.Web.Services
Imports Newtonsoft.Json
Partial Class Admin_ProductCategory
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Public Class ResultSet
        Public Property Success As Boolean
        Public Property Total As Int32
        Public Property Result As Object
    End Class
    <WebMethod()>
    Public Shared Function AddCategory(ByVal CategoryName As String, ByVal AppearinHomePage As Integer) As String
        Dim ReturnPostResponse = ""
        Dim res As ResultSet = New ResultSet()

        Try
            Dim BLL As New BusinessLogicLayer
            res.Result = BLL.AddEditCategories(0, CategoryName, 0, AppearinHomePage, 0, HttpContext.Current.Session("LoginId").ToString())
            res.Success = True
            res.Total = 1
        Catch ex As Exception
            res.Total = 0
            res.Success = False
        End Try

        ReturnPostResponse = JsonConvert.SerializeObject(res)
        Dim CategoryListing As DataTable = CType(HttpContext.Current.Session("CategoryListing"), DataTable)
        Dim dt As DataTable = CType(res.Result, DataTable)
        Dim CategoryId As Integer = Convert.ToInt32(dt.Rows(0)("CategoryId"))
        Dim SequenceNo As Integer = Convert.ToInt32(dt.Rows(0)("SequenceNo"))
        Dim CatRow As DataRow = CategoryListing.NewRow()
        CatRow("CategoryId") = CategoryId
        CatRow("CategoryName") = CategoryName
        CatRow("MainCategoryId") = 0
        CatRow("AppearinHomePage") = AppearinHomePage
        CatRow("Deactivated") = AppearinHomePage
        CatRow("CreatedBy") = HttpContext.Current.Session("LoginId").ToString()
        CatRow("CreatedDate") = ""
        CatRow("LastUpdatedBy") = ""
        CatRow("LastUpdatedDate") = ""
        CatRow("CategoryCode") = ""
        CatRow("MainCategoryName") = ""
        CatRow("SequenceNo") = SequenceNo
        CatRow("Status") = "Active"
        CategoryListing.Rows.Add(CatRow)
        HttpContext.Current.Session("CategoryListing") = CategoryListing
        Return ReturnPostResponse
    End Function
    <WebMethod()>
    Public Shared Function UpdateCategory(ByVal CategoryId As Integer, ByVal CategoryName As String, ByVal MainCategoryId As Integer, ByVal AppearinHomePage As Integer) As String
        Dim ReturnPostResponse = ""
        Dim res As ResultSet = New ResultSet()

        Try
            Dim BLL As New BusinessLogicLayer
            res.Result = BLL.AddEditCategories(CategoryId, CategoryName, MainCategoryId, AppearinHomePage, 0, HttpContext.Current.Session("LoginId").ToString())
            res.Success = True
            res.Total = 1
        Catch ex As Exception
            res.Total = 0
            res.Success = False
        End Try

        ReturnPostResponse = JsonConvert.SerializeObject(res)
        Dim CategoryListing As DataTable = CType(HttpContext.Current.Session("CategoryListing"), DataTable)
        Dim CatRow As DataRow() = CategoryListing.[Select]("CategoryId =" & CategoryId)
        CatRow(0)("CategoryName") = CategoryName
        CatRow(0)("AppearinHomePage") = AppearinHomePage
        HttpContext.Current.Session("CategoryListing") = CategoryListing
        Return ReturnPostResponse
    End Function
    <WebMethod()>
    Public Shared Function SaveCategory(ByVal CategoryId As Integer, ByVal CategoryName As String, ByVal MainCategoryId As Integer, ByVal AppearinHomePage As Integer, ByVal SequenceNo As Integer) As String
        Dim ReturnPostResponse = ""
        Dim res As ResultSet = New ResultSet()

        Try
            Dim BLL As New BusinessLogicLayer
            res.Result = BLL.AddEditCategories(CategoryId, CategoryName, MainCategoryId, AppearinHomePage, SequenceNo, HttpContext.Current.Session("LoginId").ToString())
            res.Success = True
            res.Total = 1
        Catch ex As Exception
            res.Total = 0
            res.Success = False
        End Try

        ReturnPostResponse = JsonConvert.SerializeObject(res)
        Dim CategoryListing As DataTable = CType(HttpContext.Current.Session("CategoryListing"), DataTable)
        Dim CatRow As DataRow() = CategoryListing.[Select]("CategoryId =" & CategoryId)
        CatRow(0)("CategoryName") = CategoryName
        CatRow(0)("AppearinHomePage") = AppearinHomePage
        CatRow(0)("SequenceNo") = SequenceNo
        HttpContext.Current.Session("CategoryListing") = CategoryListing
        Return ReturnPostResponse
    End Function
    <WebMethod()>
    Public Shared Function DeleteCategory(ByVal UniqueId As Integer) As String
        Dim ReturnPostResponse = ""
        Dim res As ResultSet = New ResultSet()

        Try
            Dim BLL As New BusinessLogicLayer
            res.Result = BLL.SetCategoryStatus("CategoryMaster", UniqueId, 2, "", HttpContext.Current.Session("LoginId").ToString())
            res.Success = True
            res.Total = 1
        Catch ex As Exception
            res.Total = 0
            res.Success = False
        End Try

        ReturnPostResponse = JsonConvert.SerializeObject(res)
        Dim CategoryListing As DataTable = CType(HttpContext.Current.Session("CategoryListing"), DataTable)
        Dim CatRow As DataRow() = CategoryListing.[Select]("CategoryId =" & UniqueId)
        CatRow(0)("Deactivated") = 2
        For row As Integer = 0 To CatRow.Length - 1
            CatRow(row).Delete()
            CategoryListing.AcceptChanges()
        Next
        HttpContext.Current.Session("CategoryListing") = CategoryListing
        Return ReturnPostResponse
    End Function
    <WebMethod()>
    Public Shared Function SetCategorySequence(ByVal TotalCount As Integer, ByVal SequenceString As String) As String
        Dim ReturnPostResponse = ""
        Dim res As ResultSet = New ResultSet()

        Try
            Dim BLL As New BusinessLogicLayer
            res.Result = BLL.SetCategorySequence(TotalCount, SequenceString)
            res.Success = True
            res.Total = 1
        Catch ex As Exception
            res.Total = 0
            res.Success = False
        End Try

        ReturnPostResponse = JsonConvert.SerializeObject(res)
        Return ReturnPostResponse
    End Function
End Class
