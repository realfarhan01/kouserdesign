
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Net
Namespace ShoppingCart


    ''' <summary>
    ''' Summary description for ShoppingCart
    ''' </summary>

    Public Class Cart
        Dim ctx As HttpContext = HttpContext.Current
        Public ReadOnly Property TotalItems() As Integer
            Get
                If ctx.Session("Scart") Is Nothing Then
                    Return 0
                Else
                    Dim DTable As DataTable = DirectCast(ctx.Session("Scart"), DataTable)
                    Return IIf(IsDBNull(DTable.Compute("sum(Quantity)", "")), 0, DTable.Compute("sum(Quantity)", ""))
                End If
            End Get
        End Property
        Public ReadOnly Property AllProduct() As String
            Get
                Dim dt As DataTable = ctx.Session("Scart")
                Dim t As String = ""
                If (dt Is Nothing) Then
                    Return ""
                End If
                For Each mrow As DataRow In dt.Rows
                    t = (t & mrow("ProductName") & ",")
                Next
                If t.Length > 0 Then
                    t = Left(t, t.Length - 1)
                End If
                Return t
            End Get
        End Property
        Public ReadOnly Property AllProductId() As String
            Get
                Dim dt As DataTable = ctx.Session("Scart")
                Dim t As String = ""
                If (dt Is Nothing) Then
                    Return ""
                End If
                For Each mrow As DataRow In dt.Rows
                    t = (t & mrow("ProductId").ToString() & ",")
                Next
                If t.Length > 0 Then
                    t = Left(t, t.Length - 1)
                End If
                Return t
            End Get
        End Property
        Public Sub Insert(ByVal ProductID As Integer, ByVal ProductCode As String, ByVal Size As String, ByVal ProductName As String, ByVal Quantity As Integer, ByVal OfferPrice As Double)
            If ctx.Session("Scart") Is Nothing Then
                CreateCart()
            End If
            Dim dt As DataTable = ctx.Session("Scart")
            Dim ItemIndex As Integer = -1 ' ItemIndexOfID(ProductID, dt)
            If (ItemIndex = -1) Then
                dt.Rows.Add(New Object() {ProductID, ProductCode, Size, ProductName, Quantity, OfferPrice})
            Else
                dt.Rows(ItemIndex)("Quantity") = (dt.Rows(ItemIndex)("Quantity") + Quantity)
            End If
            ctx.Session("Scart") = dt
        End Sub

        Public Sub Update(ByVal RowID As Integer, ByVal Quantity As Integer)
            Dim dt As DataTable = ctx.Session("Scart")
            Dim mrow As DataRow = dt.Rows(RowID)
            mrow("Quantity") = Quantity
            ctx.Session("Scart") = dt
        End Sub

        Public Sub DeleteItem(ByVal rowID As Integer)
            Dim dt As DataTable = ctx.Session("Scart")
            dt.Rows(rowID).Delete()
            ctx.Session("Scart") = dt
        End Sub

        Private Function ItemIndexOfID(ByVal ProductID As Integer, ByVal dt As DataTable) As Integer
            Dim index As Integer = 0
            For Each mrow As DataRow In dt.Rows
                If (mrow("ProductId").ToString() = ProductID) Then
                    Return index
                End If
                index = (index + 1)
            Next
            Return -1
        End Function

        Public Sub CreateCart()
            Dim dt As New DataTable
            dt.Columns.Add("ProductId", Type.GetType("System.Int32"))
            dt.Columns.Add("ProductCode")
            dt.Columns.Add("Size")
            dt.Columns.Add("ProductName")
            dt.Columns.Add("Quantity", Type.GetType("System.Int32"))
            dt.Columns.Add("OfferPrice", Type.GetType("System.Double"))
            dt.Columns.Add("subTotal", Type.GetType("System.Double"), "Quantity*OfferPrice")
            ctx.Session("Scart") = dt
        End Sub
     
    End Class


End Namespace

