<%@ WebHandler Language="VB" Class="AddMyCataloguethumbnail" %>

Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Web.SessionState
Imports Newtonsoft.Json
Imports System

Public Class AddMyCataloguethumbnail : Implements IHttpHandler, IReadOnlySessionState

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim fname As String = ""

        If Not Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/ActualImage/")) Then
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Files/ActualImage/"))
        End If

        If HttpContext.Current.Session("MultiFiles") Is Nothing Then
            createDataTable()
        End If

        Dim dt As DataTable = CType(HttpContext.Current.Session("MultiFiles"), DataTable)

        If context.Request.Files.Count > 0 Then
            Dim files As HttpFileCollection = context.Request.Files

            For i As Integer = 0 To files.Count - 1
                Dim file As HttpPostedFile = files(i)
                Dim Size As String = (Convert.ToDecimal(file.ContentLength) / (1024 * 1024)).ToString("0.00")
                Dim NewId As String = Guid.NewGuid().ToString().Substring(1, 22)
                fname = context.Server.MapPath("~/Files/ActualImage/" & Path.GetFileNameWithoutExtension(NewId) & ".jpg")
                file.SaveAs(fname)
                ResizeImageAndSave(552, 692, fname, "~/Files/Thumbnail/" & NewId & ".jpg")
                Dim count As Integer = dt.Rows.Count + 1
                Dim dtRow As DataRow = dt.NewRow()
                dtRow("SNo") = count
                dtRow("ImageId") = "Files/Thumbnail/" & NewId & ".jpg"
                dtRow("ImageSize") = Size
                dtRow("IsDefault") = 0
                dt.Rows.Add(dtRow)
                dt.AcceptChanges()
                HttpContext.Current.Session("MultiFiles") = dt
            Next
        End If

        Dim json As String = JsonConvert.SerializeObject(dt, Formatting.Indented)
        context.Response.ContentType = "text/plain"
        context.Response.Write(json)
    End Sub
    Public Function ResizeImageAndSave(ByVal Width As Integer, ByVal Height As Integer, ByVal imageUrl As String, ByVal destPath As String) As String
        Dim fullsizeImage As System.Drawing.Image = System.Drawing.Image.FromFile(imageUrl)
        Dim newWidth As Integer = Width
        Dim maxHeight As Integer = Height
        fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
        fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone)

        If fullsizeImage.Width <= newWidth Then
            newWidth = fullsizeImage.Width
        End If

        Dim newHeight As Integer = fullsizeImage.Height * newWidth / fullsizeImage.Width

        If newHeight > maxHeight Then
            newWidth = fullsizeImage.Width * maxHeight / fullsizeImage.Height
            newHeight = maxHeight
        End If

        Dim newImage As System.Drawing.Image = fullsizeImage.GetThumbnailImage(newWidth, newHeight, Nothing, IntPtr.Zero)
        newImage.Save(HttpContext.Current.Server.MapPath(destPath), ImageFormat.Jpeg)
        fullsizeImage.Dispose()
        Return ""
    End Function

    Protected Sub createDataTable()
        Dim dtFiles As DataTable = New DataTable()

        If HttpContext.Current.Session("MultiFiles") Is Nothing Then
            dtFiles.Columns.Add("SNo", GetType(System.Int32))
            dtFiles.Columns.Add("ImageId", GetType(System.String))
            dtFiles.Columns.Add("ImageSize", GetType(System.String))
            dtFiles.Columns.Add("IsDefault", GetType(System.Int32))
            HttpContext.Current.Session("MultiFiles") = dtFiles
        End If
    End Sub
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class