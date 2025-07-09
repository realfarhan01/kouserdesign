Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Enum ImageSize
    [Default] = 120
    Small = 120
    Medium = 240
    Large = 480
    [Custom]
End Enum
Public Class Resizer
    'Private Shared Sub Main(ByVal args As String())
    '    For Each Image As String In args
    '        ' string Image = args[0];
    '        If Image Is Nothing Then
    '            ErrorResult()
    '            Return
    '        End If


    '        Dim Size As Integer = CInt(ImageSize.[Default])

    '        'if (args.Length > 1)
    '        '{
    '        '    string sSize = args[1];
    '        '    if (sSize != null)
    '        '        Size = Int32.Parse(sSize);
    '        '}

    '        Dim Path As String = Image
    '        Dim bmp As Bitmap = CreateThumbnail(Path, Size, Size)

    '        If bmp Is Nothing Then
    '            ErrorResult()
    '            Return
    '        End If

    '        Dim OutputFilename As String = Nothing
    '        Dim OutputFile As New FileInfo(Path)
    '        OutputFilename = OutputFile.DirectoryName & "\" & OutputFile.Name.Remove(OutputFile.Name.Length - (OutputFile.Extension.Length)) & "[" & Size.ToString().Trim() & "]" & OutputFile.Extension

    '        If OutputFilename IsNot Nothing Then
    '            Try
    '                bmp.Save(OutputFilename)

    '            Catch ex As Exception
    '                bmp.Dispose()
    '                Return
    '            End Try
    '        End If


    '        bmp.Dispose()
    '    Next

    'End Sub

    Public Shared Function CreateThumbnail(ByVal lcFilename As String, ByVal lnWidth As Integer, ByVal lnHeight As Integer) As Bitmap
        Dim bmpOut As Bitmap = Nothing

        Try

            Dim loBMP As New Bitmap(lcFilename)
            Dim loFormat As ImageFormat = loBMP.RawFormat
            Dim lnRatio As Decimal
            Dim lnNewWidth As Integer = 0
            Dim lnNewHeight As Integer = 0

            '*** If the image is smaller than a thumbnail just return it
            If loBMP.Width < lnWidth AndAlso loBMP.Height < lnHeight Then
                Return loBMP
            End If

            If loBMP.Width > loBMP.Height Then
                lnRatio = CDec(lnWidth) / loBMP.Width
                lnNewWidth = lnWidth
                Dim lnTemp As Decimal = loBMP.Height * lnRatio
                lnNewHeight = CInt(Math.Truncate(lnTemp))
            Else
                lnRatio = CDec(lnHeight) / loBMP.Height
                lnNewHeight = lnHeight
                Dim lnTemp As Decimal = loBMP.Width * lnRatio
                lnNewWidth = CInt(Math.Truncate(lnTemp))
            End If

            ' System.Drawing.Image imgOut = 
            '      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,
            '                              null,IntPtr.Zero);

            ' *** This code creates cleaner (though bigger) thumbnails and properly
            ' *** and handles GIF files better by generating a white background for
            ' *** transparent images (as opposed to black)

            bmpOut = New Bitmap(lnNewWidth, lnNewHeight)
            Dim g As Graphics = Graphics.FromImage(bmpOut)
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight)
            g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight)

            loBMP.Dispose()
        Catch
            Return Nothing
        End Try
        Return bmpOut
    End Function
End Class

