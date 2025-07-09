Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Web.Mail
Imports System.Xml
Imports System.Data

Public Class BusinessLogicLayer
    Inherits DataAccessLayer
    Dim ctx As HttpContext = HttpContext.Current
    Public Function GenerateRandomString(ByRef iLength As Integer) As String
        Dim rdm As New Random()
        Dim allowChrs() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim sResult As String = ""

        For i As Integer = 0 To iLength - 1
            sResult += allowChrs(rdm.Next(0, allowChrs.Length))
        Next

        Return sResult
    End Function
    Public Function isImage(ByVal strm As System.IO.Stream) As Boolean
        Dim bool As Boolean
        Try

            Dim image As System.Drawing.Image = System.Drawing.Image.FromStream(strm)
            Dim FormetType As String = String.Empty
            If image.RawFormat.Guid = System.Drawing.Imaging.ImageFormat.Tiff.Guid Then
                FormetType = "TIFF"
                bool = True
            ElseIf image.RawFormat.Guid = System.Drawing.Imaging.ImageFormat.Gif.Guid Then
                FormetType = "GIF"
                bool = True
            ElseIf image.RawFormat.Guid = System.Drawing.Imaging.ImageFormat.Jpeg.Guid Then
                FormetType = "JPG"
                bool = True
            ElseIf image.RawFormat.Guid = System.Drawing.Imaging.ImageFormat.Bmp.Guid Then
                FormetType = "BMP"
                bool = True
            ElseIf image.RawFormat.Guid = System.Drawing.Imaging.ImageFormat.Png.Guid Then
                FormetType = "PNG"
                bool = True
            ElseIf image.RawFormat.Guid = System.Drawing.Imaging.ImageFormat.Icon.Guid Then
                FormetType = "ICO"
                bool = True
            Else
                bool = False
            End If
        Catch exp As System.ArgumentException
            bool = False
        Catch ex As Exception
            bool = False
        End Try
        Return bool
    End Function
    Public Sub ExportToExcel(ByVal gv As GridView, ByVal fileName As String)
        ctx.Response.Clear()
        ctx.Response.Buffer = True
        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        ctx.Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
        ctx.Response.Charset = ""
        ctx.Response.ContentType = "application/vnd.ms-excel"
        gv.RenderControl(hw)
        ctx.Response.Write(sw.ToString())
        ctx.Response.End()
    End Sub
    Public Sub ExportToWord(ByVal gv As GridView, ByVal fileName As String)
        ctx.Response.Clear()
        ctx.Response.Buffer = True
        ctx.Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
        ctx.Response.Charset = ""
        ctx.Response.ContentType = "application/vnd.ms-word "
        Dim sw As New StringWriter()
        Dim hw As New HtmlTextWriter(sw)
        gv.RenderControl(hw)
        ctx.Response.Output.Write(sw.ToString())
        ctx.Response.Flush()
        ctx.Response.[End]()
    End Sub

    Public Sub ExportToCsv(ByVal dt As DataTable, ByVal fileName As String)
        ctx.Response.Clear()
        ctx.Response.Buffer = True
        ctx.Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
        ctx.Response.Charset = ""
        ctx.Response.ContentType = "application/text "


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)

        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                'add separator 
                sb.Append(dt.Rows(i).Item(k).ToString() + ","c)

            Next
            'append new line 
            sb.Append(vbCr & vbLf)

        Next
        ctx.Response.Output.Write(sb.ToString())
        ctx.Response.Flush()
        ctx.Response.End()
    End Sub

    Public Function GenerateCode(ByVal len As Integer) As String
        Dim KeyGen As New KeyGenerate
        Dim RandomKey As String
        KeyGen = New KeyGenerate
        'KeyGen.KeyLetters = "abcdefghjklmnpqrstuvwxyz"
        KeyGen.KeyLetters = "123456789"
        KeyGen.KeyNumbers = "23456789"
        KeyGen.KeyChars = len
        RandomKey = KeyGen.Generate().ToUpper()
        Return RandomKey
    End Function

    Public Function GetDistinctValues(ByVal array As String()) As String()
        Dim list As New System.Collections.Generic.List(Of String)()
        For i As Integer = 0 To array.Length - 1
            If list.Contains(array(i)) Then
                Continue For
            End If
            list.Add(array(i))
        Next
        Return list.ToArray()
    End Function

    Public Function CreateMenuNew() As String
        Dim menu As String = String.Empty
        Dim dt As DataTable
        Dim a As String = ctx.Session("menustr")
        Dim mstr As String = Left(ctx.Session("menustr"), ctx.Session("menustr").ToString.Length - 1)
        menu = "<ul class='navigation'>"
        Dim str As String = "select Row_number() over(order by sno) rno,Sno,MenuName,isnull(Url,'') Url,ParentMenuid from DynamicMenu where Active=1 and sno in (" & mstr & ")"
        dt = ExecDataTable(str)
        Dim Childstr As String = ""
        Dim drow As DataRow() = dt.Select("ParentMenuid=0")

        Dim K As Integer = 1
        For i As Integer = 0 To drow.Length - 1
            Dim childrow As DataRow() = dt.Select("Parentmenuid=" & drow(i).Item("Sno"))
            If childrow.Length > 0 Then
                menu = menu & String.Format("<li><a href='{0}' class='expand'><i class='fa fa-align-justify'></i>{2}</a>", drow(i).Item("Url"), K, drow(i).Item("MenuName"))
                For j As Integer = 0 To childrow.Length - 1
                    If j = 0 Then
                        menu = menu & "<ul ><li><a href='" & childrow(j).Item("Url") & "'>" & childrow(j).Item("MenuName") & "</a>" & GetStr(dt.Select("Parentmenuid=" & childrow(j).Item("Sno"))) & "</li>"
                        K = K + 1
                    Else
                        menu = menu & "<li><a href='" & childrow(j).Item("Url") & "'>" + childrow(j).Item("MenuName") & "</a>" & GetStr(dt.Select("Parentmenuid=" & childrow(j).Item("Sno"))) & "</li>"
                    End If
                    If j = childrow.Length - 1 Then
                        menu = menu & "</ul></li>"
                    End If
                Next
            Else
                menu = menu & String.Format("<li><a href='{0}' ><i class='fa fa-align-justify'></i>{1}</a></li>", drow(i).Item("Url"), drow(i).Item("MenuName"))
            End If

        Next
        menu = menu & "</ul>"
        'Childstr = Childstr & "</ul>"

        Return menu

    End Function

    Public Function CreateMenu() As String
        Dim menu As String = String.Empty
        Dim dt As DataTable
        Dim a As String = ctx.Session("menustr")
        Dim mstr As String = Left(ctx.Session("menustr"), ctx.Session("menustr").ToString.Length - 1)
        menu = "<table cellspacing='0' cellpadding='0' border='0'><tr> <td><div class='topmainmenu' id='ddtopmenubar'><ul id='main_menu'>"
        Dim str As String = "select Row_number() over(order by sno) rno,Sno,MenuName,isnull(Url,'') Url,ParentMenuid from DynamicMenu where Active=1 and sno in (" & mstr & ")"
        dt = ExecDataTable(str)
        Dim Childstr As String = ""
        Dim drow As DataRow() = dt.Select("ParentMenuid=0")
        Dim K As Integer = 1
        For i As Integer = 0 To drow.Length - 1
            Dim childrow As DataRow() = dt.Select("Parentmenuid=" & drow(i).Item("Sno"))
            If childrow.Length > 0 Then
                menu = menu & String.Format("<li><a href='{0}' rel='ddsubmenu{1}'>{2}</a></li>", drow(i).Item("Url"), K, drow(i).Item("MenuName"))
            Else
                menu = menu & String.Format("<li><a href='{0}' >{1}</a></li>", drow(i).Item("Url"), drow(i).Item("MenuName"))
            End If
            For j As Integer = 0 To childrow.Length - 1
                If j = 0 Then
                    Childstr = Childstr & "<ul visible='false' class='ddsubmenustyle' id='ddsubmenu" & (K).ToString() & "'><li><a href='" & childrow(j).Item("Url") & "'>" & childrow(j).Item("MenuName") & "</a>" & GetStr(dt.Select("Parentmenuid=" & childrow(j).Item("Sno"))) & "</li>"
                    K = K + 1
                Else
                    Childstr = Childstr & "<li><a href='" & childrow(j).Item("Url") & "'>" + childrow(j).Item("MenuName") & "</a>" & GetStr(dt.Select("Parentmenuid=" & childrow(j).Item("Sno"))) & "</li>"
                End If
                If j = childrow.Length - 1 Then
                    Childstr = Childstr & "</ul>"
                End If
            Next
        Next
        menu = menu & "</ul></div><script type='text/javascript'>ddlevelsmenu.setup('ddtopmenubar', 'topbar') //ddlevelsmenu.setup('mainmenuid', 'topbar|sidebar')</script>"
        Childstr = Childstr & "</td></tr></table>"

        Return menu + Childstr

    End Function
    Public Sub CreateOperatorMenuFile(ByVal Loginid As String, ByVal Menustr As String)
        Dim mstr As String = Left(Menustr, Menustr.Length - 1)
        Dim xd As New System.Xml.XmlDocument
        Dim MenusNode As XmlNode = xd.CreateElement("Menus")
        xd.AppendChild(MenusNode)

        Dim TopNode, ChildNode As XmlNode
        Dim attr As XmlAttribute
        Dim dr As SqlDataReader = ExecDataReader("select Row_number() over(order by sno) rno,Sno,MenuName,isnull(Url,'') Url,ParentMenuid from DynamicMenu where Active=1 and ParentMenuid=0 and sno in (" & mstr & ")")
        While dr.Read
            TopNode = xd.CreateElement("Menu")
            attr = xd.CreateAttribute("MenuName")
            attr.Value = dr("MenuName")
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Url")
            attr.Value = dr("Url")
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Id")
            attr.Value = dr("sno")
            TopNode.Attributes.Append(attr)
            Dim dr2 As SqlDataReader = ExecDataReader("select Row_number() over(order by sno) rno,Sno,MenuName,isnull(Url,'') Url,ParentMenuid from DynamicMenu where Active=1 and sno in (" & mstr & ") and ParentMenuid=@id", "@id", dr("sno"))
            While dr2.Read
                ChildNode = xd.CreateElement("ChildMenu")
                attr = xd.CreateAttribute("MenuName")
                attr.Value = dr2("MenuName")
                ChildNode.Attributes.Append(attr)
                attr = xd.CreateAttribute("Url")
                attr.Value = dr2("Url")
                ChildNode.Attributes.Append(attr)
                TopNode.AppendChild(ChildNode)
            End While
            MenusNode.AppendChild(TopNode)
        End While
        xd.Save(ctx.Server.MapPath("~/Xml/") & Loginid & ".xml")
    End Sub
    Public Sub CreateAdminMenuFile(ByVal Loginid As String, ByVal Menustr As String)
        Dim mstr As String = Left(Menustr, Menustr.Length - 1)
        Dim xd As New System.Xml.XmlDocument
        Dim MenusNode As XmlNode = xd.CreateElement("Menus")
        xd.AppendChild(MenusNode)

        Dim TopNode, ChildNode As XmlNode
        Dim attr As XmlAttribute
        Dim dr As SqlDataReader = ExecDataReader("select Row_number() over(order by sno) rno,Sno,MenuName,isnull(Url,'') Url,ParentMenuid from DynamicMenu where Active=1 and ParentMenuid=0 and sno in (" & mstr & ")")
        While dr.Read
            TopNode = xd.CreateElement("Menu")
            attr = xd.CreateAttribute("MenuName")
            attr.Value = dr("MenuName")
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Url")
            attr.Value = dr("Url")
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Id")
            attr.Value = dr("sno")
            TopNode.Attributes.Append(attr)
            Dim dr2 As SqlDataReader = ExecDataReader("select Row_number() over(order by sno) rno,Sno,MenuName,isnull(Url,'') Url,ParentMenuid from DynamicMenu where Active=1 and sno in (" & mstr & ") and ParentMenuid=@id", "@id", dr("sno"))
            While dr2.Read
                ChildNode = xd.CreateElement("ChildMenu")
                attr = xd.CreateAttribute("MenuName")
                attr.Value = dr2("MenuName")
                ChildNode.Attributes.Append(attr)
                attr = xd.CreateAttribute("Url")
                attr.Value = dr2("Url")
                ChildNode.Attributes.Append(attr)
                TopNode.AppendChild(ChildNode)
            End While
            MenusNode.AppendChild(TopNode)
        End While
        xd.Save(ctx.Server.MapPath("~/Xml/") & Loginid & ".xml")
    End Sub
    Public Function GetStr(ByVal mrow As DataRow()) As String
        Dim str As String = "<ul>"
        For i As Integer = 0 To mrow.Length - 1
            str = str & "<li><a href='" & mrow(i).Item("url") + "'>" & mrow(i).Item("menuName") + "</a></li>"
        Next
        If str = "<ul>" Then
            str = ""
        Else
            str = str & "</ul>"
        End If
        Return str
    End Function
    Public Function GetWalletStatusRecharge(ByVal MemberId As String) As SqlDataReader
        Dim Param As New SqlParameter("@MemberId", SqlDbType.VarChar, 100)
        Param.Value = MemberId
        Dim dr As SqlDataReader = ExecDataReaderProc("Prc_WalletStatus_recharge", Param)
        Return dr
    End Function

    Public Function IsValidForPage() As Boolean
        Dim pagename As String = GetCurrentPageName()
        Dim pageid As String = ExecScalar("Select sno from  DynamicMenu where pagename=@PageName", "@PageName", pagename)
        pageid = "," & pageid & ","
        Dim mstr As String = "," & ctx.Session("menustr").ToString()
        If mstr.Contains(pageid) Then
            Return True
        Else
            Return False
        End If
    End Function

    'Public Function GetCurrentPageName() As String
    '    Dim sPath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
    '    Dim oInfo As System.IO.FileInfo = New System.IO.FileInfo(sPath)
    '    Dim sRet As String = oInfo.Name
    '    Return sRet
    'End Function
    Public Function GetCurrentPageName() As String
        Dim sPath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        'Dim oInfo As System.IO.FileInfo = New System.IO.FileInfo(sPath)
        Dim sRet As String
        If sPath.Contains("AspireSchool") Then
            sRet = sPath.Replace("/AspireSchool_CABH/Admin/", "")
        Else
            sRet = sPath.Replace("/Admin/", "")
        End If

        Return sRet
    End Function
    Public Function getPageParentid(ByVal pagename As String) As Integer
        Dim pageid As Integer = 0
        pageid = ExecScalar("Select MenuParentId from  MemberMenu where MenuPageName=@MenuPageName", "@MenuPageName", pagename)
        If IsDBNull(pageid) Then
            pageid = 0
        End If
        Return pageid
    End Function
    Public Function getAdminPageParentid(ByVal pagename As String) As Integer
        Dim pageid As Integer = 0
        pageid = ExecScalar("Select ParentMenuId from DynamicMenu where PageName=@PageName", "@PageName", pagename)
        If IsDBNull(pageid) Then
            pageid = 0
        End If
        Return pageid
    End Function



    Sub CreatePaging(ByVal intTotalRecords As Integer, ByVal intTotalPages As Integer, ByVal RecordsPerPage As Integer, ByVal CurrentPage As Integer, ByVal TotalMessages As Label, ByVal PagingLabel As Label, ByVal RecordsCount As Label)
        If intTotalRecords Mod RecordsPerPage = 0 Then
            intTotalPages = CInt(Int(intTotalRecords / RecordsPerPage))
        Else
            intTotalPages = CInt(Int(intTotalRecords / RecordsPerPage) + 1)
        End If
        TotalMessages.Text = "Page <b>" & CurrentPage & "</b> of <b>" & intTotalPages & "</b>"
        RecordsCount.Text = "<b>" & intTotalRecords & "</b> Records"
        Dim i As Integer
        Dim NavigationText As String = ""
        If CurrentPage > 1 Then
            NavigationText += "<a href=" & HttpContext.Current.Request.ServerVariables("SCRIPT_NAME") & "?Page=" & CurrentPage - 1 & "><<</a> "
        End If
        For i = 1 To intTotalPages
            If CurrentPage = i Then
                NavigationText += "<b>" & i & "</b>    "
            Else
                NavigationText += "<a href=" & HttpContext.Current.Request.ServerVariables("SCRIPT_NAME") & "?Page=" & i & ">" & i & "</a> "
            End If
        Next i
        If CurrentPage < intTotalPages Then
            NavigationText += "<a href=" & HttpContext.Current.Request.ServerVariables("SCRIPT_NAME") & "?Page=" & CurrentPage + 1 & ">>></a> "
        End If
        PagingLabel.Text = NavigationText
    End Sub

    Public Sub ShowNoResultFound(ByVal source As DataTable, ByVal gv As GridView)
        Dim dt As DataTable = source.Clone
        For Each c As DataColumn In dt.Columns
            c.AllowDBNull = True
        Next
        dt.Rows.Add(dt.NewRow()) ' // create a new blank row to the DataTable
        '// Bind the DataTable which contain a blank row to the GridView
        gv.DataSource = dt
        gv.DataBind()
        '// Get the total number of columns in the GridView to know what the Column Span should be
        Dim columnsCount As Integer
        If gv.Columns.Count = 0 Then
            columnsCount = source.Columns.Count
        Else
            columnsCount = gv.Columns.Count
        End If

        gv.Rows(0).Cells.Clear() '// clear all the cells in the row
        gv.Rows(0).Cells.Add(New TableCell()) ' //add a new blank cell
        gv.Rows(0).Cells(0).ColumnSpan = columnsCount ' //set the column span to the new added cell

        ' //You can set the styles here
        gv.Rows(0).Cells(0).HorizontalAlign = HorizontalAlign.Center ';
        gv.Rows(0).Cells(0).ForeColor = System.Drawing.Color.Red '
        gv.Rows(0).Cells(0).Font.Bold = True '
        ' //set No Results found to the new added cell
        gv.Rows(0).Cells(0).Text = "NO RESULT FOUND!" '
    End Sub


    Public Function GeneratePassword(ByVal length As Integer, ByVal numberOfNonAlphanumericCharacters As Integer) As String
        'Make sure length and numberOfNonAlphanumericCharacters are valid....
        If ((length < 1) OrElse (length > 128)) Then
            Throw New ArgumentException("Membership_password_length_incorrect")
        End If

        If ((numberOfNonAlphanumericCharacters > length) OrElse (numberOfNonAlphanumericCharacters < 0)) Then
            Throw New ArgumentException("Membership_min_required_non_alphanumeric_characters_incorrect")
        End If

        Do While True
            Dim i As Integer
            Dim nonANcount As Integer = 0
            Dim buffer1 As Byte() = New Byte(length - 1) {}

            'chPassword contains the password's characters as it's built up
            Dim chPassword As Char() = New Char(length - 1) {}

            'chPunctionations contains the list of legal non-alphanumeric characters
            Dim chPunctuations As Char() = "!@@$%^^*()_-+=[{]};:>|./?".ToCharArray()

            'Get a cryptographically strong series of bytes
            Dim rng As New System.Security.Cryptography.RNGCryptoServiceProvider
            rng.GetBytes(buffer1)

            For i = 0 To length - 1
                'Convert each byte into its representative character
                Dim rndChr As Integer = (buffer1(i) Mod 87)
                If (rndChr < 10) Then
                    chPassword(i) = Convert.ToChar(Convert.ToUInt16(48 + rndChr))
                Else
                    If (rndChr < 36) Then
                        chPassword(i) = Convert.ToChar(Convert.ToUInt16((65 + rndChr) - 10))
                    Else
                        If (rndChr < 62) Then
                            chPassword(i) = Convert.ToChar(Convert.ToUInt16((97 + rndChr) - 36))
                        Else
                            chPassword(i) = chPunctuations(rndChr - 62)
                            nonANcount += 1
                        End If
                    End If
                End If
            Next

            If nonANcount < numberOfNonAlphanumericCharacters Then
                Dim rndNumber As New Random
                For i = 0 To (numberOfNonAlphanumericCharacters - nonANcount) - 1
                    Dim passwordPos As Integer
                    Do
                        passwordPos = rndNumber.Next(0, length)
                    Loop While Not Char.IsLetterOrDigit(chPassword(passwordPos))
                    chPassword(passwordPos) = chPunctuations(rndNumber.Next(0, chPunctuations.Length))
                Next
            End If

            Return New String(chPassword)
        Loop
    End Function

    Private Function PrepareCommand(ByVal ParamName As String, ByVal ParamValue As Object, ByVal ParamType As SqlDbType, ByVal ParamSize As Int16, ByVal ParamDir As ParameterDirection) As SqlParameter
        Dim Param As New SqlParameter
        Param.ParameterName = ParamName
        If ParamValue Is Nothing Then
            Param.Value = DBNull.Value
        Else
            Param.Value = ParamValue
        End If
        Param.SqlDbType = ParamType
        Param.Size = ParamSize
        Param.Direction = ParamDir
        Return Param
    End Function
    Public Function FundTransfer(ByVal FromId As String, ByVal ToId As String, ByVal scode As String, ByVal Amount As String) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", FromId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@issueamount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Toid", ToId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@scode", scode, SqlDbType.VarChar, 20, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("dbo.sp_fundtransfer", arrList.ToArray())
        Return result
    End Function
    Public Function FundTransfer_W2(ByVal FromId As String, ByVal ToId As String, ByVal scode As String, ByVal EmailCode As String, ByVal Amount As String) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", FromId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@issueamount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Toid", ToId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@scode", scode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailCode", EmailCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("Prc_fundtransfer_W2", arrList.ToArray())
        Return result
    End Function
    Public Function FundTransfer_W3(ByVal FromId As String, ByVal ToId As String, ByVal scode As String, ByVal EmailCode As String, ByVal Amount As String) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", FromId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@issueamount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Toid", ToId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@scode", scode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailCode", EmailCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("Prc_fundtransfer_W3", arrList.ToArray())
        Return result
    End Function
    Public Function GetMemberName(ByVal Memberid As String) As String
        Dim MemberName As String
        Try
            Dim arrList As New ArrayList
            arrList.Add(PrepareCommand("@MemberId", Memberid, SqlDbType.VarChar, 20, ParameterDirection.Input))
            arrList.Add(PrepareCommand("@MemberName", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
            MemberName = ExecScalarProc("getMemberName", arrList.ToArray)
            If String.IsNullOrEmpty(MemberName) Then
                MemberName = "!"
            End If
        Catch ex As Exception
            MemberName = "!"
        End Try
        Return MemberName
    End Function
    Public Function CheckEpin(ByVal Epin As String) As String
        Dim Result As String

        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Epin", Epin, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Result = ExecScalarProc("chkepin", arrList.ToArray)

        Return Result
    End Function
    Public Function isLoginUser(ByVal memberid As String, ByVal sessionId As String) As Boolean
        Dim bool As Boolean
        Try
            bool = ExecScalarProc("isLoginUser", "@MemberId", memberid, "@SessionId", sessionId)
        Catch ex As Exception
            bool = False
        Finally

        End Try
        Return bool
    End Function

    Public Function GetMsrnoById(ByVal Memberid As String) As Integer
        Dim Param As New SqlParameter("@MemberId", SqlDbType.VarChar, 20)
        Param.Value = Memberid
        Dim Msrno As Integer = ExecScalarProc("Prc_Get_MsrnoByID", Param)
        Return Msrno
    End Function

    Public Function GetMemberIdByMsrno(ByVal Msrno As String) As String
        Dim Param As New SqlParameter("@MemberId", SqlDbType.Int)
        Param.Value = Msrno
        Dim MemberId As String = ExecScalarProc("Prc_Get_MemberIdByMsrno", Param)
        Return MemberId
    End Function

    Public Function CheckAdminLogin(ByVal UserId As String, ByVal Password As String, ByVal IPAddress As String) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@UserId", UserId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAddress", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("CheckAdminLogin", arrList.ToArray())
        Return dr
    End Function

    Public Function CheckMemberLogin(ByVal UserId As String, ByVal Password As String, ByVal SessionId As String, ByVal IPAddress As String) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@UserId", UserId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAddress", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionId", SessionId, SqlDbType.VarChar, 50, ParameterDirection.Input))
        'arrList.Add(PrepareCommand("@UserEmail", UserEmail, SqlDbType.VarChar, 50, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("CheckMemberLogin", arrList.ToArray())
        Return dr
    End Function

    'Public Function SaveUserDetails(ByVal MemberId As String, _
    '                                    ByVal Title As String, _
    '                                    ByVal MemberName As String, _
    '                                    ByVal CareOfTitle As String, _
    '                                    ByVal CareOfName As String, _
    '                                    ByVal Dob As String, _
    '                                    ByVal Address As String, _
    '                                    ByVal Country As String, _
    '                                    ByVal State As String, _
    '                                    ByVal City As String, _
    '                                    ByVal Mobile As String, _
    '                                    ByVal Email As String, _
    '                                    ByVal Scheme As Integer, _
    '                                    ByVal IntroId As String, _
    '                                    ByVal RefId As String, _
    '                                    ByVal leg As String, _
    '                                    ByVal BankName As String, _
    '                                    ByVal BranchName As String, _
    '                                    ByVal AcNo As String, _
    '                                    ByVal PanNo As String, _
    '                                    ByVal IFSC As String, _
    '                                    ByVal Epin As String, _
    '                                    ByVal Password As String, ByVal DTUser As String, ByVal IPAddress As String) As String

    '    Dim arrList As New ArrayList
    '    arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@IntroId", IntroId, SqlDbType.VarChar, 20, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@RefId", RefId, SqlDbType.VarChar, 20, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Leg", leg, SqlDbType.VarChar, 1, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Title", Title, SqlDbType.VarChar, 10, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@CareOf", CareOfTitle, SqlDbType.VarChar, 10, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@CareOfname", CareOfName, SqlDbType.VarChar, 100, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@DOB", Dob, SqlDbType.VarChar, 10, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@MemberName", MemberName, SqlDbType.VarChar, 100, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 200, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 100, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 100, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Country", Country, SqlDbType.VarChar, 10, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Email", Email, SqlDbType.VarChar, 100, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 10, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Scheme", Scheme, SqlDbType.Int, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@BankName", BankName, SqlDbType.VarChar, 100, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@BranchName", BranchName, SqlDbType.VarChar, 100, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@AcNo", AcNo, SqlDbType.VarChar, 30, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@PanNo", PanNo, SqlDbType.VarChar, 10, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@IFSC", IFSC, SqlDbType.VarChar, 20, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Epin", Epin, SqlDbType.VarChar, 20, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@DTUser", DTUser, SqlDbType.VarChar, 20, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@IP", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
    '    Dim str As String = ExecNonQueryProc("Prc_Registration", arrList.ToArray())
    '    Return str
    'End Function

    Public Function SaveUserDetails(ByVal DOJ As String,
                                    ByVal EpinSno As String,
                                    ByVal Epin As String,
                                    ByVal scheme As Integer,
                                    ByVal introId As String,
                                    ByVal Leg As String,
                                    ByVal UserId As String,
                                    ByVal Title As String,
                                    ByVal Name As String,
                                    ByVal CareOfTitle As String,
                                    ByVal CareOfName As String,
                                    ByVal Dob As String,
                                    ByVal Gender As String,
                                    ByVal Address As String,
                                    ByVal PinCode As String,
                                    ByVal Country As String,
                                    ByVal Nationality As String,
                                    ByVal State As String,
                                    ByVal City As String,
                                    ByVal Email As String,
                                    ByVal CountryCode As String,
                                    ByVal Mobile As String,
                                    ByVal Phone As String,
                                    ByVal BankName As String,
                                    ByVal BranchName As String,
                                    ByVal BranchAddress As String,
                                    ByVal AcHolder As String,
                                    ByVal AcNo As String,
                                    ByVal IFSC As String,
                                    ByVal SwiftCode As String,
                                    ByVal PanNo As String,
                                    ByVal NName As String,
                                    ByVal NAddress As String,
                                    ByVal NDOB As String,
                                    ByVal NGender As String,
                                    ByVal NRelation As String,
                                    ByVal OffersChk As Integer,
                                    ByVal POffersChk As Integer,
                                    ByVal Password As String,
                                    ByVal IPAddress As String) As String
        Dim arrList As New ArrayList
        If Not IsDate(Dob) Then
            Dob = Nothing
        End If
        If Not IsDate(NDOB) Then
            NDOB = Nothing
        End If
        arrList.Add(PrepareCommand("@DOJ", DOJ, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EpinSno", EpinSno, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Epin", Epin, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Scheme", scheme, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IntroId", introId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Leg", Leg, SqlDbType.VarChar, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", UserId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Title", Title, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberName", Name, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Gender", Gender, SqlDbType.Char, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CareOf", CareOfTitle, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CareOfname", CareOfName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DOB", Dob, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PinCode", PinCode, SqlDbType.VarChar, 8, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Country", Country, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Nationality", Nationality, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Email", Email, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CountryCode", CountryCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Phone", Phone, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BankName", BankName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchName", BranchName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchAddress", BranchAddress, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AcHolderName", AcHolder, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AcNo", AcNo, SqlDbType.VarChar, 30, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PanNo", PanNo, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IFSC", IFSC, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SwiftCode", SwiftCode, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NName", NName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NAddress", NAddress, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NGender", NGender, SqlDbType.Char, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NDOB", NDOB, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NRelation", NRelation, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OffersChk", OffersChk, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@POffersChk", POffersChk, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IP", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_Registration", arrList.ToArray())
        Return str
    End Function

    Public Function SaveRefUserDetails(ByVal firstname As String, ByVal lastname As String, ByVal countryname As String, ByVal mobile As String, ByVal email As String, ByVal verificationcode As String, ByVal RefMember As String) As Integer
        Dim val As Integer
        val = 0

        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@firstname", firstname, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@lastname", lastname, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@countryname", countryname, SqlDbType.VarChar, 100, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@mobile", mobile, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@email", email, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@verificationcode", verificationcode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RefMember", RefMember, SqlDbType.VarChar, 50, ParameterDirection.Input))
        val = ExecNonQueryProc("sp_RefRegistration", arrList.ToArray())

        Return val
    End Function
    Public Function UserDeposite(ByVal Msrno As Integer, ByVal Amount As Decimal, ByVal PaymentType As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PaymentType", PaymentType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_Deposite", arrList.ToArray())
        Return str
    End Function

    Public Function GetReferral(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal DownLine As Integer, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DownLine", DownLine, SqlDbType.Int, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_DirectDownLine", arrList.ToArray)
        Return Dt
    End Function
    Public Function LeftRightMemberDetails(ByVal MemberId As String, ByVal Side As String) As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTableProc("LeftRightDetail", "@MemberId", MemberId, "@Side", Side)
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function TotalMembers(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal IntroId As String, ByVal Itemid As Integer, ByVal isActive As String, ByVal Deactive As Integer, ByVal Export As Integer) As SqlDataReader
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IntroId", IntroId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isActive", isActive, SqlDbType.VarChar, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mscheme", Itemid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Deactive", Deactive, SqlDbType.Bit, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("Prc_TotalMembers", arrList.ToArray)
        Return dr
    End Function

    Public Function AccountStatement(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal WalletType As Integer, ByVal TranType As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@WType", WalletType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TranType", TranType, SqlDbType.VarChar, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_AllAccountStatement", arrList.ToArray)
        Return DT
    End Function
    Public Function LevelMemberReport(ByVal MemberID As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberID", MemberID, SqlDbType.VarChar, 100, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_LevelMemberReport", arrList.ToArray)
        Return DT
    End Function


    Public Function UpdateWalletPwd(ByVal MemberId As String, ByVal OldPwd As String, ByVal NewPwd As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OldPwd", OldPwd, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NewPwd", NewPwd, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_UpdateWalletPwd", arrList.ToArray)
        Return result
    End Function
    Public Function SendSms(ByVal ID As String, ByVal MobileNo As String, ByVal Text_Message As String, ByVal Timing As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ID", ID, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MobileNo", MobileNo, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Text_Message", Text_Message, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Timing", Timing, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_SendSms", arrList.ToArray)
        Return result
    End Function

    Public Function GetWalletStatus(ByVal MemberId As String) As SqlDataReader
        Dim Param As New SqlParameter("@MemberId", SqlDbType.VarChar, 20)
        Param.Value = MemberId
        Dim dr As SqlDataReader = ExecDataReaderProc("Prc_WalletStatus", Param)
        Return dr
    End Function
    Public Function GetWalletStatus_W2(ByVal MemberId As String) As SqlDataReader
        Dim Param As New SqlParameter("@MemberId", SqlDbType.VarChar, 20)
        Param.Value = MemberId
        Dim dr As SqlDataReader = ExecDataReaderProc("Prc_WalletStatus_W2", Param)
        Return dr
    End Function
    Public Function GetWalletStatus_W3(ByVal MemberId As String) As SqlDataReader
        Dim Param As New SqlParameter("@MemberId", SqlDbType.VarChar, 20)
        Param.Value = MemberId
        Dim dr As SqlDataReader = ExecDataReaderProc("Prc_WalletStatus_W3", Param)
        Return dr
    End Function
    Public Function WithDrawlRequest(ByVal MemberId As String, ByVal Amount As Decimal, ByVal Remark As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_WithDrawlRequest", arrList.ToArray)
        Return result
    End Function

    Function GetWithDrawlRequest(ByVal Fromdate As String, ByVal Todate As String, ByVal MemberId As String, ByVal Paid As Integer, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(Fromdate) Then
            Fromdate = Nothing
        End If
        If Not IsDate(Todate) Then
            Todate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", Fromdate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", Todate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Paid", Paid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_WalletPaidDetails", arrList.ToArray)
        Return Dt
    End Function

    Public Function AddAmountByAdmin(ByVal WalletType As Integer, ByVal MemberId As String, ByVal Amount As Decimal, ByVal Remark As String, ByVal TType As Integer) As String
        Dim Result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TType", TType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        If WalletType = 1 Then
            Result = ExecNonQueryProc("SaveAmountToWallet", arrList.ToArray)
        ElseIf WalletType = 2 Then
            Result = ExecNonQueryProc("SaveAmountToRoiWallet", arrList.ToArray)
        ElseIf WalletType = 3 Then
            Result = ExecNonQueryProc("SaveAmountTotradingWallet", arrList.ToArray)
        Else
            Result = "Error"
        End If

        Return Result
    End Function

    Public Function GetUpline(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_UplineLine", arrList.ToArray)
        Return Dt
    End Function

    Function GetUserSecurityInfo(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal IntroId As String) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IntroId", IntroId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_GetUserSecurityInfo", arrList.ToArray)
        Return Dt
    End Function

    Public Function UpdateUserDetails(ByVal Msrno As Integer, ByVal Title As String,
                                      ByVal Membername As String,
                                      ByVal CareOfTitle As String,
                                      ByVal CareOfName As String,
                                      ByVal Dob As String,
                                      ByVal Address As String,
                                      ByVal Country As String,
                                      ByVal Nationality As String,
                                      ByVal State As String,
                                      ByVal City As String,
                                      ByVal Mobile As String,
                                      ByVal Email As String,
                                      ByVal BankName As String,
                                      ByVal BranchName As String,
                                      ByVal BranchAddress As String,
                                      ByVal AcNo As String,
                                      ByVal PanNo As String,
                                      ByVal SwiftCode As String,
                                      ByVal IFSC As String,
                                      ByVal Password As String, ByVal DtUser As String _
                                      , ByVal AcName As String, ByVal NName As String, ByVal NAddress As String _
                                      , ByVal Ngender As String, ByVal NRelation As String, ByVal NDob As String _
                                      , ByVal PinCode As String, ByVal Phone As String, ByVal Gender As String) As String

        If Not IsDate(NDob) Then
            NDob = Nothing
        End If
        If Not IsDate(Dob) Then
            Dob = Nothing
        End If
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Title", Title, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberName", Membername, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CareOf", CareOfTitle, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CareOfname", CareOfName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DOB", Dob, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Country", Country, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Nationality", Nationality, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Email", Email, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BankName", BankName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchName", BranchName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchAddress", BranchAddress, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AcNo", AcNo, SqlDbType.VarChar, 30, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PanNo", PanNo, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SwiftCode", SwiftCode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IFSC", IFSC, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DtUser", DtUser, SqlDbType.VarChar, 10, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@AcHolderName", AcName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NName", NName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NAddress", NAddress, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NGender", Ngender, SqlDbType.Char, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NDOB", NDob, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NRelation", NRelation, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PinCode", PinCode, SqlDbType.VarChar, 6, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Phone", Phone, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Gender", Gender, SqlDbType.VarChar, 1, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_UpdateMemberDetails", arrList.ToArray())
        Return str
    End Function

    Public Function GetMemberDetails(ByVal Msrno As Int16, ByVal MemberId As String) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.VarChar, 100, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("Prc_GetMemberDetails", arrList.ToArray)
        Return dr
    End Function
    Public Function GetMemberDetails1(ByVal Msrno As Int16, ByVal MemberId As String, ByVal Email As String) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Email", Email, SqlDbType.VarChar, 100, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("Prc_GetMemberDetails1", arrList.ToArray)
        Return dr
    End Function

    Function GetInvestDetails(ByVal FromDate As String, ByVal Todate As String, ByVal MemberId As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(Todate) Then
            Todate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", Todate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_investmentDetails", arrList.ToArray)
        Return Dt
    End Function

    Function PairsStatistics(ByVal Msrno As Integer) As DataTable
        Dim dt As New DataTable
        dt = ExecDataTableProc("PairsStatistics", "@Msrno", Msrno)
        Return dt
    End Function

    Function GetEpinDetails(ByVal MemberId As String, ByVal FromDate As String, ByVal ToDate As String, ByVal ItemId As Integer, ByVal Status As String, ByVal Pinsno As Integer) As DataTable
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        Dim dt As New DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ItemId", ItemId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Status", Status, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Pinsno", Pinsno, SqlDbType.Int, 0, ParameterDirection.Input))
        dt = ExecDataTableProc("getEpinDetail", arrList.ToArray)
        Return dt
    End Function

    Function GetEpinMasterDetail(ByVal MemberId As String, ByVal FromDate As String, ByVal ToDate As String, ByVal UserOperator As String, ByVal ItemId As Integer) As DataTable
        Dim dt As New DataTable
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ItemId", ItemId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Operator", UserOperator, SqlDbType.VarChar, 100, ParameterDirection.Input))
        dt = ExecDataTableProc("EpinMasterDetail", arrList.ToArray)
        Return dt
    End Function

    Function GetEpinTransferDetail(ByVal FromId As String, ByVal ToId As String, ByVal FromDate As String, ByVal ToDate As String, ByVal ItemId As Integer) As DataTable
        Dim dt As New DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@MemberId", FromId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@toMemberid", ToId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ItemId", ItemId, SqlDbType.Int, 0, ParameterDirection.Input))
        dt = ExecDataTableProc("Prc_EpinTransferDetail", arrList.ToArray)
        Return dt
    End Function

    Function Get_Operator() As SqlDataReader
        Dim dr As SqlDataReader
        dr = ExecDataReader("Select UserName From UserMaster")
        Return dr
    End Function
    Function Get_ItemMaster() As SqlDataReader
        Dim dr As SqlDataReader
        dr = ExecDataReader("Select itemname,itemid  From itemMaster where isBlock=0 ")
        Return dr
    End Function

    Public Sub ActiveDeactive(ByVal Memberid As String, ByVal Type As Char, ByVal AllTeam As Integer, ByVal Remark As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", Memberid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Char, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AllTeam", AllTeam, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        ExecNonQueryProc("Prc_ActiveDeactive", arrList.ToArray)

    End Sub

    Function GenerateEpin(ByVal Msrno As Integer, ByVal NoOfEpin As Integer, ByVal ItemId As Integer, ByVal MemberId As String, ByVal Password As String, ByVal ById As String, ByVal ByIP As String, ByVal ByOperator As String, ByVal PinHead As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NoOfpin", NoOfEpin, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ItemId", ItemId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IssueTo", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ById", ById, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ByIP", ByIP, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ByOperator", ByOperator, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PinHead", PinHead, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("SpGenerateEpin", arrList.ToArray())
        Return str
    End Function
    Function UserGenerateEpin(ByVal Msrno As Integer, ByVal NoOfEpin As Integer, ByVal ItemId As Integer, ByVal Password As String, ByVal TransCode As String, ByVal ByIP As String, ByVal PinHead As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NoOfpin", NoOfEpin, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ItemId", ItemId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ByIP", ByIP, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@emailcode", TransCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PinHead", PinHead, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_UserGenerateEpin", arrList.ToArray())
        Return str
    End Function
    Function UserGenerateEpin_Trading(ByVal Msrno As Integer, ByVal NoOfEpin As Integer, ByVal ItemId As Integer, ByVal Password As String, ByVal TransCode As String, ByVal ByIP As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NoOfpin", NoOfEpin, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ItemId", ItemId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ByIP", ByIP, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@emailcode", TransCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_UserGenerateEpin_W3", arrList.ToArray())
        Return str
    End Function
    Function CreateOperator(ByVal UserName As String, ByVal UserId As String, ByVal EmailId As String, ByVal Address As String, ByVal Password As String, ByVal Mobile As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@UserName", UserName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LoginId", UserId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Email", EmailId, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Sno", "", SqlDbType.Int, 0, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_CreateOperator", arrList.ToArray())
        Return str
    End Function

    Function GetClosingData(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal ClosingType As Integer, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClosingType", ClosingType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_PaidData", arrList.ToArray)
        Return Dt
    End Function
    Function AmountSettlement(ByVal MemberId As String, ByVal Amount As Integer, ByVal EmailCode As String, ByVal Remark As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailCode", EmailCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_AmountSettlement", arrList.ToArray())
        Return str
    End Function
    Function AmountSettlement_W2(ByVal MemberId As String, ByVal Amount As Integer, ByVal EmailCode As String, ByVal Remark As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailCode", EmailCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_AmountSettlement_W2", arrList.ToArray())
        Return str
    End Function

    Function EpinTransfer(ByVal MemberId As Object, ByVal ToId As String, ByVal scode As String, ByVal emailcode As String, ByVal NoOfEpin As Integer, ByVal ItemId As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@toid", ToId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@scode", scode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@emailcode", emailcode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@tot", NoOfEpin, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@itemid", ItemId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("issuePintoid", arrList.ToArray())
        Return str
    End Function
    Public Function CheckWalletLogin(ByVal UserId As String, ByVal Password As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Loginid", UserId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 2, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("checkScode", arrList.ToArray())
        Return Result
    End Function

    Function UpgradeAccount(ByVal Msrno As Integer, ByVal Epin As String, ByVal DtUser As String, ByVal itemid As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Epin", Epin, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DtUser", DtUser, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ItemId", itemid, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_UpgradeAccount", arrList.ToArray())
        Return Result
    End Function
    Function PurchaseSubPanle(ByVal Msrno As Integer, ByVal EpinSno As String, ByVal Epin As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EpinSno", EpinSno, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Epin", Epin, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_PurchaseSubPanel", arrList.ToArray())
        Return Result
    End Function
    Public Function DisabledButtonCode(Optional ByVal validationGroup As String = "") As String
        Dim sbValid As New System.Text.StringBuilder()
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ")
        sbValid.Append("if (Page_ClientValidate('" & validationGroup & "') == false) { return false; }} ")
        sbValid.Append("this.value = 'Please wait...';")
        sbValid.Append("this.disabled = true;")
        Return sbValid.ToString
    End Function
    Public Function isOperator(ByVal LoginId As String) As Boolean
        Dim isExists As Boolean
        Try
            isExists = (New BusinessLogicLayer).ExecScalar("if Exists(select 1 from UserMaster where LoginId=@LoginId) select 1 else select 0", "@LoginId", LoginId)
        Catch ex As Exception
            isExists = True
        Finally

        End Try
        Return isExists
    End Function

    Public Function isUserExists(ByVal MemberId As String) As Integer
        Dim isExists As Integer
        Try
            isExists = (New BusinessLogicLayer).ExecScalar("if Exists(select Msrno from Membermaster where MemberId=@MemberId) select 1 else select 0", "@MemberId", MemberId)
        Catch ex As Exception
            isExists = 1
        Finally

        End Try
        Return isExists
    End Function

    Function EpinStatistics() As DataTable
        Dim dt As New DataTable
        dt = ExecDataTableProc("EpinStatistics")
        Return dt
    End Function

    Function JoiningStatistics(ByVal Msrno As Integer) As DataTable
        Dim dt As New DataTable
        dt = ExecDataTableProc("JoiningStatistics", "@Msrno", Msrno)
        Return dt
    End Function



    Function ProductDetails(ByVal ProductId As Integer, ByVal CategoryId As Integer) As IDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ProductId", ProductId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("Prc_ProductDetails", arrList.ToArray)
        Return dr
    End Function


    'Function AddUpdateProductMaster(ByVal ProductId As Integer, ByVal CategoryId As Integer, ByVal BrandId As Integer, ByVal ProductCode As String, ByVal ProductName As String, ByVal MRP As Decimal, ByVal OfferPrice As Decimal, ByVal ImagePath As String, ByVal SmallDescription As String, ByVal Description As String, ByVal MetaKeyword As String, ByVal MetaDescription As String, ByVal Level1 As Integer, ByVal Level2 As Integer, ByVal Level3 As Integer, ByVal Level4 As Integer) As String
    '    Dim arrList As New ArrayList
    '    arrList.Add(PrepareCommand("@ProductId", ProductId, SqlDbType.Int, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@BrandId", BrandId, SqlDbType.Int, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@ProductCode", ProductCode, SqlDbType.VarChar, 50, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@ProductName", ProductName, SqlDbType.VarChar, 200, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@MRP", MRP, SqlDbType.Decimal, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@OfferPrice", OfferPrice, SqlDbType.Decimal, 0, ParameterDirection.Input))

    '    arrList.Add(PrepareCommand("@SmallDescription", SmallDescription, SqlDbType.VarChar, 200, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Description", Description, SqlDbType.VarChar, 500, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@MetaKeyword", MetaKeyword, SqlDbType.VarChar, 200, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@MetaDescription", MetaDescription, SqlDbType.VarChar, 200, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Level1", Level1, SqlDbType.Decimal, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Level2", Level2, SqlDbType.Decimal, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Level3", Level3, SqlDbType.Decimal, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Level4", Level4, SqlDbType.Decimal, 0, ParameterDirection.Input))
    '    arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
    '    Dim Result As String = ExecNonQueryProc("Prc_AddUpdateProductMaster", arrList.ToArray())
    '    Return Result
    'End Function

    Function AddUpdateBrand(ByVal BrandId As Integer, ByVal BrandName As String, ByVal Description As String, ByVal ImagePath As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@BrandId", BrandId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BrandName", BrandName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Description", Description, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ImagePath", ImagePath, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateBrand", arrList.ToArray())
        Return Result
    End Function

    Public Function AddUpdateWallet(ByVal Msrno As Integer, ByVal TType As String, ByVal Description As String, ByVal Amount As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TType", TType, SqlDbType.VarChar, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Description", Description, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("AddUpdatewallet", arrList.ToArray())
        Return Result
    End Function


    Public Function TrackingUrl(ByVal GroupleadId As Integer, ByVal Leadid As Integer, ByVal Msrno As Integer, ByVal IPAddress As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@GroupleadId", GroupleadId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@leadid", Leadid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAdress", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("TrackingUrl", arrList.ToArray)
        Return str
    End Function
    Public Function TrackingUrlComplete(ByVal groupid As Integer, ByVal Msrno As Integer, ByVal IPAddress As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@groupid", groupid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAdress", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("TrackingUrl_bygroupid", arrList.ToArray)
        Return str
    End Function
    Function GetErrorMessage(ByVal Sno As Integer) As String
        Dim doc As New System.Xml.XmlDocument()
        doc.Load(HttpContext.Current.Server.MapPath("message_08.xml"))

        ' Root element
        Dim root As System.Xml.XmlElement = doc.DocumentElement

        Dim conditie As System.Xml.XmlElement
        conditie = DirectCast(root.ChildNodes(0).ChildNodes(0), System.Xml.XmlElement)
        Dim ErrorString As String = conditie.ChildNodes(Sno).InnerText


        Return ErrorString
    End Function
    Function TotalAds(ByVal FromDate As String, ByVal Todate As String) As IDataReader
        Dim dr As SqlDataReader
        dr = ExecDataReader("select Name,adid,adurl,startdate,enddate,ondate,isExpired from tbladMaster")
        Return dr
    End Function
    Public Function AddUpdateMenu(ByVal Menu_id As Integer, ByVal MenuName As String, ByVal MenuUrl As String, ByVal description As String, ByVal MemberId As String) As String
        Dim arrList As New ArrayList()
        arrList.Add(PrepareCommand("@Menu_id", Menu_id, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuName", MenuName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuUrl", MenuUrl, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Menu_Description", description, SqlDbType.VarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("AddUpdateMenu", arrList.ToArray())
        Return Result
    End Function

    Public Function AddUpdateLink(ByVal Link_id As Integer, ByVal Link As String, ByVal ImagePath As String, ByVal MemberId As Integer) As String
        Dim arrList As New ArrayList()
        arrList.Add(PrepareCommand("@Link_Id", Link_id, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Link", Link, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ImagePath", ImagePath, SqlDbType.VarChar, 2000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberID", MemberId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("AddUpdateLink", arrList.ToArray())
        Return Result
    End Function

    Function PurchaseSubPanel(ByVal Msrno As Integer, ByVal epin As String, ByVal courseid As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Epin", epin, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@courseid", courseid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("dbo.SubPanelPurchase", arrList.ToArray())
        Return Result
    End Function
    Function SubPanelPurchaseEPinChk(ByVal epin As String) As Integer
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Epin", epin, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.Int, 0, ParameterDirection.Output))
        Dim Result As Integer = ExecNonQueryProc("dbo.SubPanelPurchaseEPinChk", arrList.ToArray())
        Return Result
    End Function

    Public Function AddGroup(ByVal GroupName As String, ByVal IsFrom As String, ByVal IsTo As String, ByVal Active As Integer, ByVal ActionType As Integer, ByVal groupImage As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@GroupName", GroupName, SqlDbType.VarChar, 1000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsFrom", IsFrom, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsTo", IsTo, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Active", Active, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ActionType", ActionType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@groupImage", groupImage, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_AddGroupmaster", arrList.ToArray())
        Return str
    End Function

    Public Function GetGroupDetails(ByVal GroupId As Integer, ByVal isActive As Integer?, ByVal MsrNo As Integer) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@GroupId", GroupId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isActive", isActive, SqlDbType.Bit, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MsrNo", MsrNo, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("GetGroupDetails", arrList.ToArray)
        Return dr
    End Function

    Public Function GetGroupWiseLead(ByVal GroupId As Integer, ByVal IsExists As Integer) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@GroupId", GroupId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsExists", IsExists, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("GetGroupWiseLead", arrList.ToArray)
        Return dr
    End Function

    Public Function GetLeadIdDetails(ByVal LeadId As Integer, ByVal isActive As Integer?) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@LeadId", LeadId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isActive", isActive, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("GetLeadDetails", arrList.ToArray)
        Return dr
    End Function

    Public Function SaveUserDetails_bulk(ByVal TotalId As Integer,
                                   ByVal scheme As Integer,
                                   ByVal introId As String,
                                   ByVal Leg As String,
                                   ByVal Title As String,
                                   ByVal Name As String,
                                   ByVal CareOfTitle As String,
                                   ByVal CareOfName As String,
                                   ByVal Dob As String,
                                   ByVal Gender As String,
                                   ByVal Address As String,
                                   ByVal PinCode As String,
                                   ByVal Country As String,
                                   ByVal State As String,
                                   ByVal City As String,
                                   ByVal Email As String,
                                   ByVal Mobile As String,
                                   ByVal Phone As String,
                                   ByVal BankName As String,
                                   ByVal BranchName As String,
                                   ByVal AcHolder As String,
                                   ByVal AcNo As String,
                                   ByVal IFSC As String,
                                   ByVal PanNo As String,
                                   ByVal NName As String,
                                   ByVal NAddress As String,
                                   ByVal NDOB As String,
                                   ByVal NGender As String,
                                   ByVal NRelation As String,
                                   ByVal Password As String,
                                   ByVal IPAddress As String) As String
        Dim arrList As New ArrayList
        If Not IsDate(Dob) Then
            Dob = Nothing
        End If
        If Not IsDate(NDOB) Then
            NDOB = Nothing
        End If
        arrList.Add(PrepareCommand("@TotalId", TotalId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Scheme", scheme, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IntroId", introId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Leg", Leg, SqlDbType.VarChar, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Title", Title, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberName", Name, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CareOf", CareOfTitle, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CareOfname", CareOfName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DOB", Dob, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Gender", Gender, SqlDbType.Char, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PinCode", PinCode, SqlDbType.VarChar, 8, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Country", Country, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Email", Email, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Phone", Phone, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BankName", BankName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchName", BranchName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AcHolderName", AcHolder, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AcNo", AcNo, SqlDbType.VarChar, 30, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PanNo", PanNo, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IFSC", IFSC, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NName", NName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NAddress", NAddress, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NGender", NGender, SqlDbType.Char, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NDOB", NDOB, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NRelation", NRelation, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IP", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_Registration_bulk", arrList.ToArray())
        Return str
    End Function

    Public Function ShowEPIN(ByVal PinId As Integer, ByVal MsrNo As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@PinId", PinId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MsrNo", MsrNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 20, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("spActivateEPin", arrList.ToArray())
        Return Result
    End Function
    Function GetMyCourses(ByVal Msrno As Integer) As DataTable
        Dim dt As New DataTable
        dt = ExecDataTableProc("GetMyCourses", "@Msrno", Msrno)
        Return dt
    End Function
    Public Function InsertMail(ByVal _From As String, ByVal _To As String, ByVal Subject As String, ByVal Message As String, ByVal MsgType As String, ByVal InsertBy As String) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@From", _From, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@To", _To, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Subject", Subject, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Message", Message, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MsgType", MsgType, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@InsertBy", InsertBy, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("prc_InsertMail", arrList.ToArray())
        Return result
    End Function
    Public Function CheckEmailLink(ByVal _refcode As String, ByVal _refmember As String) As DataTable
        'Dim arrList As New ArrayList
        'arrList.Add(PrepareCommand("@RefID", _refmember, SqlDbType.VarChar, 50, ParameterDirection.Input))
        'arrList.Add(PrepareCommand("@RefCode", _refcode, SqlDbType.VarChar, 500, ParameterDirection.Input))
        'arrList.Add(PrepareCommand("@Result", "", SqlDbType.Int, 0, ParameterDirection.Output))
        'Dim Result As String = ExecNonQueryProc("sp_emailvarify", arrList.ToArray())
        'Return Result
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@RefID", _refmember, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RefCode", _refcode, SqlDbType.VarChar, 500, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("sp_emailvarify", arrList.ToArray)
        Return dt
    End Function
    Public Function UpdateLeg(ByVal _memberid As String, ByVal _leg As String) As Integer
        Dim val As Integer
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", _memberid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Leg", _leg, SqlDbType.VarChar, 10, ParameterDirection.Input))
        val = ExecNonQuery("update  tbl_MemberFlag set activeleg=@Leg where msrno=(select msrno from MemberMaster where MemberId=@MemberId)", arrList)
        Return val
    End Function
    Public Function UpdateDeleteExam(ByVal ExamId As String) As Integer
        Dim val As Integer
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ExamId", ExamId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        val = ExecNonQuery("update  tbl_ExamMaster set 	DeActivated=1 where ExamId=@ExamId)", arrList)
        Return val
    End Function
    Public Sub CreateMengerMenuFile(ByVal MenuStr As String)

        Dim xd As New System.Xml.XmlDocument
        Dim MenusNode As XmlNode = xd.CreateElement("Menus")
        xd.AppendChild(MenusNode)

        Dim TopNode, ChildNode As XmlNode
        Dim attr As XmlAttribute
        Dim sql As String = "select row_number() over(order by menuid) as Sno,* from MemberMenu where IsMenuActive=1 And MenuLevel=1 And MenuID in (" & MenuStr & ")"
        Dim dr As SqlDataReader = ExecDataReader(sql)
        While dr.Read
            TopNode = xd.CreateElement("Menu")
            attr = xd.CreateAttribute("MenuName")
            attr.Value = dr("MenuName").ToString()
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Url")
            attr.Value = dr("VirtualName").ToString()
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Id")
            attr.Value = dr("MenuID")
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("MenuIcon")
            attr.Value = dr("MenuIcon")
            TopNode.Attributes.Append(attr)
            Dim dr2 As SqlDataReader = ExecDataReader("select row_number() over(order by menuid) as Sno,* from MemberMenu where IsMenuActive=1 And MenuLevel=2 And MenuParentID='" & dr("MenuID") & "' And MenuID in (" & MenuStr & ")")
            While dr2.Read
                ChildNode = xd.CreateElement("ChildMenu")
                attr = xd.CreateAttribute("MenuName")
                attr.Value = dr2("MenuName").ToString()
                ChildNode.Attributes.Append(attr)
                attr = xd.CreateAttribute("Url")
                attr.Value = dr2("VirtualName").ToString()
                ChildNode.Attributes.Append(attr)
                TopNode.AppendChild(ChildNode)
            End While
            MenusNode.AppendChild(TopNode)
        End While
        xd.Save(ctx.Server.MapPath("~/Xml/") & "membermenu.xml")
    End Sub

    Public Sub CreateMengerMenuFileVirtual(ByVal MenuStr As String)

        Dim xd As New System.Xml.XmlDocument
        Dim MenusNode As XmlNode = xd.CreateElement("Menus")
        xd.AppendChild(MenusNode)


        Dim TopNode, ChildNode As XmlNode
        Dim attr As XmlAttribute
        Dim sql As String = "select row_number() over(order by menuid) as Sno,* from MemberMenu where IsMenuActive=1 And MenuLevel=1 And MenuID in (" & MenuStr & ")"
        Dim dr As SqlDataReader = ExecDataReader(sql)
        While dr.Read
            TopNode = xd.CreateElement("Menu")
            attr = xd.CreateAttribute("MenuName")
            attr.Value = dr("MenuName").ToString()
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Id")
            attr.Value = dr("MenuID")
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Url")
            attr.Value = dr("VirtualName").ToString()
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("Title")
            attr.Value = dr("PageTitle").ToString()
            TopNode.Attributes.Append(attr)
            attr = xd.CreateAttribute("PageHeader")
            attr.Value = dr("PageHeader").ToString()
            TopNode.Attributes.Append(attr)


            Dim dr2 As SqlDataReader = ExecDataReader("select row_number() over(order by menuid) as Sno,* from MemberMenu where IsMenuActive=1 And MenuLevel=2 And MenuParentID='" & dr("MenuID") & "' And MenuID in (" & MenuStr & ")")
            While dr2.Read
                ChildNode = xd.CreateElement("ChildMenu")
                attr = xd.CreateAttribute("MenuName")
                attr.Value = dr2("MenuName").ToString()
                ChildNode.Attributes.Append(attr)
                attr = xd.CreateAttribute("Id")
                attr.Value = dr2("MenuId")
                ChildNode.Attributes.Append(attr)
                attr = xd.CreateAttribute("Url")
                attr.Value = dr2("VirtualName").ToString()
                ChildNode.Attributes.Append(attr)
                attr = xd.CreateAttribute("Title")
                attr.Value = dr2("PageTitle").ToString()
                ChildNode.Attributes.Append(attr)
                attr = xd.CreateAttribute("PageHeader")
                attr.Value = dr2("PageHeader").ToString()
                ChildNode.Attributes.Append(attr)

                TopNode.AppendChild(ChildNode)
            End While
            MenusNode.AppendChild(TopNode)
        End While
        xd.Save(ctx.Server.MapPath("~/Xml/") & "virtualmenu.xml")
    End Sub
    Public Sub ReadXML()
        Dim reader As New XmlTextReader("books.xml")
        While reader.Read()
            Dim str As String = reader.ReadInnerXml("Url").ToString()

        End While
    End Sub

    Public Sub CreateMengerMenuFileVirtual1(ByVal MenuStr As String)

        Dim xd As New System.Xml.XmlDocument
        Dim MenusNode As XmlNode = xd.CreateElement("Menus")
        xd.AppendChild(MenusNode)



        Dim sql As String = "select row_number() over(order by menuid) as Sno,* from MemberMenu where MenuID in (" & MenuStr & ")"
        Dim dr As SqlDataReader = ExecDataReader(sql)
        While dr.Read


            Dim Menu As XmlElement = xd.CreateElement("Menu")


            Dim Id As XmlElement = xd.CreateElement("Id")
            Id.InnerText = dr("MenuID")
            Menu.AppendChild(Id)
            Dim MenuName As XmlElement = xd.CreateElement("MenuName")
            MenuName.InnerText = dr("MenuName").ToString()
            Menu.AppendChild(MenuName)
            Dim Url As XmlElement = xd.CreateElement("Url")
            Url.InnerText = dr("MenuUrl").ToString()
            Menu.AppendChild(Url)
            Dim VirtualName As XmlElement = xd.CreateElement("VirtualName")
            VirtualName.InnerText = dr("VirtualName").ToString()
            Menu.AppendChild(VirtualName)
            Dim Title As XmlElement = xd.CreateElement("Title")
            Title.InnerText = dr("PageTitle").ToString()
            Menu.AppendChild(Title)
            Dim PageHeader As XmlElement = xd.CreateElement("PageHeader")
            PageHeader.InnerText = dr("PageHeader").ToString()
            Menu.AppendChild(PageHeader)
            Dim HelpText As XmlElement = xd.CreateElement("HelpText")
            HelpText.InnerText = dr("HelpText").ToString()
            Menu.AppendChild(HelpText)

            MenusNode.AppendChild(Menu)






        End While
        xd.Save(ctx.Server.MapPath("~/Xml/") & "virtualmenu1.xml")
    End Sub




    Public Function SetSMSTemplate(ByVal TemplateName As String, ByVal ParamArray obj() As Object) As String
        'Dim xmldoc As New XmlDocument()
        'xmldoc.Load("SmsTemplate.xml")

        Dim Template As String = ""
        'Dim elemList As XmlNodeList = xmldoc.GetElementsByTagName("Tfor")
        Dim reader As New XmlTextReader(HttpContext.Current.Server.MapPath("~/Xml/SmsTemplate.xml"))
        Dim dt As New DataSet
        dt.ReadXml(reader)
        Dim table As DataTable = dt.Tables(0)
        Dim foundRows() As DataRow
        Dim Isactive As String = ""
        foundRows = table.Select("TFor = '" & TemplateName & "'")
        For Each row In foundRows
            If row("Isactive") = "Active" Then
                Template = row("Message")
            End If
        Next

        'Dim doc As New XmlDocument()
        'doc.Load(reader)
        'reader.Close()
        'Dim oldCd As XmlNode
        'Dim root As XmlElement = doc.DocumentElement
        'oldCd = root.SelectSingleNode("//templateAd[@Tfor='" & TemplateName & "' ]")

        Dim VName, VValue As String
        For i As Integer = 0 To obj.Length - 1
            VName = obj(i)
            i = i + 1
            VValue = obj(i)
            Template = Template.Replace(VName, VValue)
        Next
        Return Template
    End Function

    Public Function SetPageUrl(ByVal VirtualName As String, ByVal ParamArray obj() As Object) As String


        Dim MenuName As String

        Dim reader As New XmlTextReader(HttpContext.Current.Server.MapPath("~/Xml/virtualmenu1.xml"))
        Dim dt As New DataSet
        dt.ReadXml(reader)
        Dim table As DataTable = dt.Tables(0)
        Dim foundRows() As DataRow
        foundRows = table.Select("VirtualName = '" & VirtualName & "'")
        For Each row In foundRows
            MenuName = row("Url")
        Next




        Dim VName, VValue As String
        For i As Integer = 0 To obj.Length - 1
            VName = obj(i)
            i = i + 1
            VValue = obj(i)
            MenuName = MenuName.Replace(VName, VValue)
        Next
        Return MenuName
    End Function

    Public Function Notification(ByVal faction As String, ByVal fid As Integer, ByVal fsubject As String, ByVal fmessage As String) As Integer
        Dim val As Integer
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@faction", faction, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fid", fid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fsubject", fsubject, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fmessage", fmessage, SqlDbType.VarChar, 500, ParameterDirection.Input))
        val = ExecNonQueryProc("prc_managenotification", arrList)
        Return val
    End Function
    Public Function GetNotification(ByVal fid As Integer, ByVal listcount As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@fid", fid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@listcount", listcount, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("prc_getnotification", arrList.ToArray)
        Return dt
    End Function
    Public Function GetItemWiseMember(ByVal introid As String) As DataTable

        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@introid", introid, SqlDbType.VarChar, 100, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("prc_SelectItemWiseMember", arrList.ToArray)
        Return dt
    End Function
    Public Function SelectLineChart(ByVal memberid As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@memberid", memberid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("prc_selectLineChart", arrList.ToArray)
        Return dt
    End Function
    Public Function Add_AdminBankDetails(ByVal id As Integer, ByVal Bankname As String, ByVal branch As String, ByVal Ifscode As String, ByVal acno As String, ByVal acholder As String, ByVal actype As String, ByVal isactive As String, ByVal BankImage As String, ByVal UsedFor As String) As String

        Dim result As String
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@Id", id, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BankName", Bankname, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchName", branch, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Ifscode", Ifscode, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AcNo", acno, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AcHolderName ", acholder, SqlDbType.VarChar, 30, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@acType", actype, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isActive", isactive, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BankImage", BankImage, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UsedFor", UsedFor, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 20, ParameterDirection.Output))

        result = ExecNonQueryProc("Add_AdminBankDetails", arrList.ToArray())
        Return result
    End Function
    Public Function Payfrom_wallet(ByVal walletmemberid As String, ByVal password As String, ByVal scode As String, ByVal trtype As String, ByVal memberid As String, ByVal itemid As Integer, ByVal amount As Integer) As String

        Dim result As String
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@walletmember", walletmemberid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@password", password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@scode", scode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@trtype", trtype, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@memberid", memberid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@itemid", itemid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@amount", amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("paynow_wallet", arrList.ToArray())
        Return result
    End Function
    Public Function Payfrom_wirebank(ByVal trtype As String, ByVal memberid As String, ByVal itemid As Integer, ByVal amount As Integer, ByVal bankid As Integer, ByVal transactionnum As String, ByVal bankslip As String, ByVal comments As String) As String

        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@trtype", trtype, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@memberid", memberid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@itemid", itemid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@amount", amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@bankid", bankid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@transactionnum", transactionnum, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@bankslip", bankslip, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@comments", comments, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("PayNowWireBankComments", arrList.ToArray())
        Return result
    End Function
    Public Function Payfrom_insertcomments(ByVal cnt As Integer, ByVal comments As String, ByVal commentby As String, ByVal memberid As String) As String

        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@cnt", cnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@comments", comments, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@commentby", commentby, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@memberid", memberid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("PayNowWireBankComments", arrList.ToArray())
        Return result
    End Function
    Public Function Payfrom_ApproveWireBankTranasfer(ByVal cnt As Integer) As String

        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@cnt", cnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("paynow_wirebankApprove", arrList.ToArray())
        Return result
    End Function
    Function save_recharge(ByVal UserName As String, ByVal service As String, ByVal operatorCode As Integer, ByVal operatorname As String, ByVal circleCode As Integer, ByVal circle As String, ByVal amount As Integer, ByVal accountNumber As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", UserName, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@service", service, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@operatorCode", operatorCode, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@circleCode", circleCode, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@operator", operatorname, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@circle", circle, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@amount", amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@accountNumber", accountNumber, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("dbo.save_recharge", arrList.ToArray)
        Return result
    End Function

    Function ProcessBlockRecharge(ByVal RequestId As Integer, ByVal NewStatus As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@RequestId", RequestId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NewStatus", NewStatus, SqlDbType.VarChar, 100, ParameterDirection.Input))
        Dim result As String = ExecNonQueryProc("dbo.Prc_ProcessBlockRecharge", arrList.ToArray)
        Return result
    End Function

    Function update_recharge(ByVal Msrno As Integer, ByVal Requestid As Integer, ByVal status As String, ByVal txid As String, ByVal user_txid As String, ByVal operator_ref As String, ByVal error_code As String, ByVal message As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Requestid", Requestid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@status", status, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@txid", txid, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@user_txid", user_txid, SqlDbType.VarChar, 14, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@operator_ref", operator_ref, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@error_code", error_code, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@message", message, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("dbo.update_recharge", arrList.ToArray)
        Return result
    End Function
    Public Function RechargeReport(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal RechargeNumber As String, ByVal Status As String, ByVal Service As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RechargeNumber", RechargeNumber, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Status", Status, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Service", Service, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_Recharge_Data", arrList.ToArray)
        Return DT
    End Function

    Public Sub CreateMainmenu(ByVal Sno As Integer, ByVal MenuName As String, ByVal Url As String, ByVal ParentMenuid As Integer, ByVal Pagename As String, ByVal Active As Integer, ByVal Snostr As String, ByVal MenuClass As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Sno", Sno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuName", MenuName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Url", Url, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ParentMenuid", ParentMenuid, SqlDbType.Int, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Pagename", Pagename, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Active", Active, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Snostr", Snostr, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuClass", MenuClass, SqlDbType.VarChar, 200, ParameterDirection.Input))
        ExecNonQueryProc("Prc_CreateMainMenu", arrList.ToArray)
    End Sub
    Public Sub CreateSubmenu(ByVal Sno As Integer, ByVal MenuName As String, ByVal Url As String, ByVal ParentMenuid As Integer, ByVal Pagename As String, ByVal Active As Integer, ByVal Snostr As String, ByVal MenuClass As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Sno", Sno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuName", MenuName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Url", Url, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ParentMenuid", ParentMenuid, SqlDbType.Int, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Pagename", Pagename, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Active", Active, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Snostr", Snostr, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuClass", MenuClass, SqlDbType.VarChar, 20, ParameterDirection.Input))
        ExecNonQueryProc("Prc_CreateSubMenu", arrList.ToArray)
    End Sub



    '---------------------------------Ticket Support System-----------------------------------
    Public Function SupportOpenTicket(ByVal MsrNo As Integer, ByVal TicketId As String, ByVal Deptid As Integer, ByVal subject As String, ByVal description As String, ByVal filepath As String, ByVal generatedby As String) As String

        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@fuserid", MsrNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fticketid", TicketId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fdeptid", Deptid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fsubject", subject, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fdescription", description, SqlDbType.VarChar, 5000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ffilepath", filepath, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fgenerateby", generatedby, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@return", "", SqlDbType.VarChar, 500, ParameterDirection.Output))
        result = ExecNonQueryProc("Prc_SupportOpenTicket", arrList.ToArray())
        Return result
    End Function
    Public Function SupportTicketReply(ByVal TicketId As String, ByVal description As String, ByVal filepath As String, ByVal generatedby As String) As String

        Dim result As String
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@fticketid", TicketId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fdescription", description, SqlDbType.VarChar, 5000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ffilepath", filepath, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@fgenerateby", generatedby, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@return", "", SqlDbType.VarChar, 500, ParameterDirection.Output))
        result = ExecNonQueryProc("Prc_SupportInsertReply", arrList.ToArray())
        Return result
    End Function

    Public Function AnnualRenewal(ByVal Msrno As Integer) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("dbo.Prc_YearlyRenewal", arrList.ToArray())
        Return result
    End Function



    Public Function SelectSupportTicket(ByVal FromDate As String, ByVal ToDate As String, ByVal Status As String, ByVal MemberId As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Status", Status, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim dr As DataTable = ExecDataTableProc("Prc_SupportSelectTicket", arrList.ToArray)
        Return dr
    End Function

    Public Function GetPagePopupScript(ByVal Page As String) As String
        Dim PopupScript As String = ""
        Dim PopupImage As String = ""
        Dim NotificationHeader As String = ""
        Dim NotificationMsg As String = ""
        Dim NotificationType As String = ""
        Dim reader As New XmlTextReader(HttpContext.Current.Server.MapPath("~/Xml/popup.xml"))
        Dim dt As New DataSet
        dt.ReadXml(reader)
        Dim table As DataTable = dt.Tables(0)
        Dim foundRows() As DataRow
        foundRows = table.Select("MenuUrl = '" & Page & "'")
        For Each row In foundRows
            NotificationType = row("NotificationType")
            NotificationHeader = row("NotificationHeader")
            NotificationMsg = row("NotificationMsg")
            PopupImage = row("PopupImage")
        Next
        If NotificationType <> "" Then
            If NotificationType = "Image" Then
                PopupScript = "<link href='popup/popup.css' rel='stylesheet' type='text/css' /><script type='text/javascript'> window.onload = function () {$('#myModal').modal('show');};</script><div class='modal fade popupbg' id='myModal' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'><span class='btnpopupclose b-close' data-dismiss='modal'><span>X</span></span><img src='popup/" & PopupImage & "'></div>"
            ElseIf NotificationType = "Text" Then
                PopupScript = "<link href='popup/popup.css' rel='stylesheet' type='text/css' /><script type='text/javascript'> window.onload = function () {$('#myModal').modal('show');};</script><div class='modal fade popupbg' id='myModal' tabindex='-1' role='dialog' aria-abelledby='myModalLabel' aria-hidden='true'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button><h3>" & NotificationHeader & "</h3> </div><div class='modal-body'><p>" & NotificationMsg & "</p></div><div class='modal-footer'><button type='button' class='btn btn-default' data-dismiss='modal'>Close</button></div></div>"
            End If
        End If

        Dim sPath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        If Not sPath.Contains("user/") Then
            PopupScript = Replace(PopupScript, "popup/", "user/popup/")
        End If


        Return PopupScript
    End Function
    Public Function GetPageVirtualName(ByVal PageUrl As String, ByVal ParamArray obj() As Object) As String
        Dim VirtualName As String = ""
        Dim reader As New XmlTextReader(HttpContext.Current.Server.MapPath("~/Xml/virtualmenu1.xml"))
        Dim dt As New DataSet
        dt.ReadXml(reader)
        Dim table As DataTable = dt.Tables(0)
        Dim foundRows() As DataRow
        foundRows = table.Select("Url = '" & PageUrl & "'")
        For Each row In foundRows
            VirtualName = row("VirtualName")
        Next
        Return VirtualName
    End Function

    Public Function GetBalanceWallet(ByVal MemberId As String, ByVal WalletType As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@WalletType", WalletType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BalanceAmt", "", SqlDbType.Decimal, 0, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_AllBalanceAmount", arrList.ToArray)
        Return result
    End Function

    Public Function FundTransfertoRecharge(ByVal FromId As String, ByVal ToId As String, ByVal scode As String, ByVal EmailCode As String, ByVal Amount As String) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", FromId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@issueAmount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Toid", ToId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@scode", scode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailCode", EmailCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("dbo.Prc_fundtransfer_recharge", arrList.ToArray())
        Return result
    End Function
    Public Function FundTransfertoTrading(ByVal FromId As String, ByVal ToId As String, ByVal scode As String, ByVal EmailCode As String, ByVal Amount As String) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberId", FromId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@issueAmount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Toid", ToId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@scode", scode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailCode", EmailCode, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        result = ExecNonQueryProc("dbo.Prc_fundtransfer_M2T", arrList.ToArray())
        Return result
    End Function
    Public Function AddAdvertisement(ByVal aff_Provider As Integer, ByVal OfferId As Integer, ByVal Title As String, ByVal URL As String, ByVal Note As String, ByVal BannerUrl As String, ByVal FlashUrl As String, ByVal Active As Integer, ByVal ActionType As Integer, ByVal AdTime As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@aff_Provider", aff_Provider, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OfferId", OfferId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Title", Title, SqlDbType.VarChar, 1000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@URL", URL, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BannerUrl", BannerUrl, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FlashUrl", FlashUrl, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Note", Note, SqlDbType.VarChar, 5000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Active", Active, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ActionType", ActionType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AdTime", AdTime, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_AddAdvertisement", arrList.ToArray())
        Return str
    End Function
    Public Function GetMemberLeadHistory(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal LeadId As Integer, ByVal ispaid As Integer) As SqlDataReader
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = ""
        End If
        If Not IsDate(ToDate) Then
            ToDate = ""
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LeadId", LeadId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ispaid", ispaid, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("GetMemberLeadHistory", arrList.ToArray)
        Return dr
    End Function

    '----------------- Ads Function-----------

    Public Function AddAdvertisement_DL(ByVal aff_Provider As Integer, ByVal OfferId As Integer, ByVal Title As String, ByVal URL As String, ByVal Note As String, ByVal BannerUrl As String, ByVal FlashUrl As String, ByVal Active As Integer, ByVal ActionType As Integer, ByVal ImgLink As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@aff_Provider", aff_Provider, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OfferId", OfferId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Title", Title, SqlDbType.VarChar, 1000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@URL", URL, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BannerUrl", BannerUrl, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FlashUrl", FlashUrl, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Note", Note, SqlDbType.VarChar, 5000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Active", Active, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ActionType", ActionType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ImgLink", ImgLink, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("Prc_AddAdvertisement_DL", arrList.ToArray())
        Return str
    End Function
    Public Function GetLeadIdDetails_DL(ByVal GroupId As Integer) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@GroupId", GroupId, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("GetLeadDetails_DL", arrList.ToArray)
        Return dr
    End Function
    Public Function GetSelectedLeads_DL(ByVal GroupId As Integer) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@GroupId", GroupId, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("GetSelectedLeads_DL", arrList.ToArray)
        Return dr
    End Function

    Function GetDirectEarnings(ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = ""
        End If
        If Not IsDate(ToDate) Then
            ToDate = ""
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))

        Dim Dt As DataTable = ExecDataTableProc("Prc_DirectDownlineearnings", arrList.ToArray)
        Return Dt
    End Function
    Public Function Add_Brand(ByVal cnt As Integer, ByVal BrandName As String, ByVal Image As String, ByVal isactive As String, ByVal UseIn As String, ByVal Point As Integer, ByVal Terms As String) As String

        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@cnt", cnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BrandName", BrandName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Image", Image, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsActive", isactive, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UseIn", UseIn, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@WalletPoint", Point, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Terms", Terms, SqlDbType.NVarChar, 2000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 20, ParameterDirection.Output))

        result = ExecNonQueryProc("Add_Brand", arrList.ToArray())
        Return result
    End Function
    Public Function Add_RequestCard(ByVal msrno As Integer, ByVal Brand As Integer, ByVal Courier As String, ByVal FirstName As String, ByVal LastName As String, ByVal Address As String, ByVal Paddress As String, ByVal PIN As String, ByVal State As String, ByVal City As String, ByVal MobileNo As String, ByVal Email As String) As String

        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@msrno", msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Brand", Brand, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Courier", Courier, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FirstName", FirstName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastName", LastName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PAddress", Paddress, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PIN", PIN, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MobileNo", MobileNo, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Email", Email, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 20, ParameterDirection.Output))

        result = ExecNonQueryProc("Add_RequestCard", arrList.ToArray())
        Return result
    End Function


    Public Function SaveEpinRequest(ByVal msrno As Integer, ByVal rqdate As String, ByVal Itemid As Integer, ByVal noofepin As Integer, ByVal amount As Decimal, ByVal WalletAmount As Decimal, ByVal Bankname As String, ByVal acno As String, ByVal pmode As Integer, ByVal MBankname As String, ByVal Macno As String, ByVal chno As String, ByVal Tcode As String, ByVal document As String, ByVal Lacno As String, ByVal remarks As String, ByVal dt As DataTable) As String
        Dim result As String

        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@msrno", msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@itemId", Itemid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@nopin", noofepin, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ondate", rqdate, SqlDbType.DateTime, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TotalAmount", amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@WalletAmount", WalletAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CBankName", Bankname, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CBankAC", acno, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MBankName", MBankname, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MBankAC ", Macno, SqlDbType.VarChar, 30, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PMode", pmode, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LRAccount", Lacno, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Cheque", chno, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TCode", Tcode, SqlDbType.VarChar, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Document", document, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", remarks, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EpinDetails", dt, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 20, ParameterDirection.Output))

        result = ExecNonQueryProc("SaveEpinRequestNew", arrList.ToArray())
        Return result
    End Function

    Function GetEpinRequestdetails_new(ByVal RequestId As Integer, ByVal FromDate As String, ByVal ToDate As String, ByVal MemberId As String, ByVal status As System.Nullable(Of Integer), ByVal requestType As System.Nullable(Of Integer)) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@RequestId", RequestId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Approved", status, SqlDbType.Bit, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@requestType", requestType, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("prc_epinrequestdetails_new", arrList.ToArray)
        Return Dt
    End Function

    Public Function GetEpinRequestdetails(ByVal FromDate As String, ByVal ToDate As String, ByVal msrno As Integer, ByVal Approve As Integer, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Approve", Approve, SqlDbType.Bit, 0, ParameterDirection.Input))
        'arrList.Add(PrepareCommand("@IntroId", IntroId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Msrno", msrno, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim dr As DataTable = ExecDataTableProc("prc_EpinRequestDetails", arrList.ToArray)
        Return dr
    End Function

    Function GenerateEpin_fromRequest(ByVal Msrno As Integer, ByVal cnt As Integer, ByVal LoginId As String, ByVal ByOperator As Object, ByVal ByIP As String, ByVal remark As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Cnt", cnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LoginId", LoginId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ByIP", ByIP, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ByOperator", ByOperator, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim str As String = ExecNonQueryProc("GenerateEpin_fromRequest", arrList.ToArray())
        Return str
    End Function

    Function RejectEpinRequest(ByVal Msrno As Integer, ByVal cnt As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@cnt", cnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 100, ParameterDirection.Output))
        Dim res As String = ExecNonQueryProc("dbo.RejectEpinRequest", arrList.ToArray)
        Return res
    End Function
    Function CardRequestDetails(ByVal FromDate As String, ByVal ToDate As String, ByVal CourierId As String, ByVal CRNumber As String) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CourierId", CourierId, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CRNumber", CRNumber, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("CardRequestDetails", arrList.ToArray)
        Return Dt
    End Function

    Public Function Add_Courier(ByVal eid As Integer, ByVal Name As String, ByVal Detail As String, ByVal IsBlock As Integer) As String

        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CourierId", eid, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CourierName", Name, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CourierDetails", Detail, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsBlock", IsBlock, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 20, ParameterDirection.Output))

        result = ExecNonQueryProc("Add_Courier", arrList.ToArray())
        Return result
    End Function

    Function AdminFeed(ByVal Rows As Integer, ByVal ToDate As String) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(ToDate) Then
            ToDate = ""
        End If
        arrList.Add(PrepareCommand("@Rows", Rows, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Date", ToDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_AdminFeeds", arrList.ToArray)
        Return Dt
    End Function
    Function EpinRequestSearch(ByVal Msrno As Integer, ByVal ToDate As String) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(ToDate) Then
            ToDate = ""
        End If
        arrList.Add(PrepareCommand("@Msrno", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Date", ToDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_EpinRequestSearch", arrList.ToArray)
        Return Dt
    End Function

    Function InsertUpdateCategory(ByVal id As Integer, ByVal code As String, ByVal shortname As String, ByVal fullname As String, ByVal isactive As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@id", id, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryCode", code, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryShortName", shortname, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryFullName", fullname, SqlDbType.NVarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsActive", isactive, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Operator", ctx.Session("Loginid"), SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Return", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_Inv_ProductCategory", arrList.ToArray())
        Return Result
    End Function
    Function InsertUpdateProducts(ByVal id As Integer, ByVal code As String, ByVal shortname As String, ByVal fullname As String, ByVal Duration As String, ByVal MRP As String, ByVal DP As String, ByVal PV As String, ByVal CategoryId As String, ByVal isactive As String, ByVal ImageName As String, ByVal Description As String, ByVal taxvalue As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@id", id, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Code", code, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ShortName", shortname, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FullName", fullname, SqlDbType.NVarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Duration", Duration, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MRP", MRP, SqlDbType.NVarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DP", DP, SqlDbType.NVarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PV", PV, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.NVarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsActive", isactive, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Operator", ctx.Session("Loginid"), SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@imagename", ImageName, SqlDbType.NVarChar, 5000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@description", Description, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@taxvalue", taxvalue, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Return", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_Inv_Product", arrList.ToArray())
        Return Result
    End Function


    Function SaveDiscountCoupon(ByVal CouponId As Integer, ByVal CouponCode As String, ByVal isMultiUse As Integer, ByVal ValidFrom As String, ByVal ValidUpto As String, ByVal OneUserOneTime As Integer, ByVal MinOrderValue As Decimal, ByVal DiscountPer As Decimal, ByVal DiscountAmt As Decimal, ByVal isactive As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CouponId", CouponId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CouponCode", CouponCode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isMultiUse", isMultiUse, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ValidFrom", ValidFrom, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ValidUpto", ValidUpto, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OneUserOneTime", OneUserOneTime, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MinOrderValue", MinOrderValue, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DiscountPer", DiscountPer, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DiscountAmt", DiscountAmt, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsActive", isactive, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("SaveDiscountCoupon", arrList.ToArray())
        Return Result
    End Function
    Public Function BindLocality() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select * from dbo.LocalityMaster")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function


    Public Function BindRoom() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select * from dbo.RoomMaster")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindState() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select * from dbo.tblState")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindPendingProspectusFormNo() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTableProc("Get_ProspectusFormNo", "@Type", 0)
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindPendingProspectusFormNoSession(ByVal SchoolSession As String) As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTableProc("Get_ProspectusFormNoSession", "@Type", 0, "@SchoolSession", SchoolSession)
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindClass() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTableProc("Prc_AvlClassMaster")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindMainClass() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select * from dbo.MainClassMaster order by SNo")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindSessions() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("Select SessionId,SessionName from tbl_Session Where Deactivated=0 order by FromDate desc")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindPickupLocation() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("Select PickupPointId,PickupPoint from tbl_PickupLocation order by Amount")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindCity(ByVal State As String) As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select * from dbo.City where State=@State", "@State", State)
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindDesignation() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select * from DesignationMaster")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function



    Function AddParents(ByVal ParentId As String, ByVal FatherName As String, ByVal MotherName As String, ByVal Nationality As String, ByVal Gender As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal LocalityId As Integer, ByVal ContactNo As String, ByVal EmailId As String, ByVal isBlock As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ParentId", ParentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherName", FatherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherName", MotherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Nationality", Nationality, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Gender", Gender, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LocalityId", LocalityId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContactNo", ContactNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailId", EmailId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isBlock", isBlock, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_RegistrationParent", arrList.ToArray())
        Return Result


    End Function


    Function AddStudent(ByVal StudentId As String, ByVal ParentId As String, ByVal StudentName As String, ByVal FatherName As String, ByVal MotherName As String, ByVal DOB As String, ByVal Nationality As String, ByVal Gender As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal LocalityId As Integer, ByVal ContactNo As String, ByVal EmailId As String, ByVal isBlock As Integer, ByVal ClassId As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ParentId", ParentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentName", StudentName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherName", FatherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherName", MotherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Nationality", Nationality, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DOB", DOB, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Gender", Gender, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LocalityId", LocalityId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContactNo", ContactNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailId", EmailId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isBlock", isBlock, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_RegisterStudent", arrList.ToArray())
        Return Result
    End Function

    Function AddStudentComplete(ByVal StudentId As String, ByVal ParentId As String, ByVal DOJ As String, ByVal StudentName As String, ByVal FatherName As String, ByVal MotherName As String, ByVal DOB As String,
            ByVal Nationality As String, ByVal Gender As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal LocalityId As Integer, ByVal ContactNo As String,
            ByVal EmailId As String, ByVal isBlock As Integer, ByVal ClassId As Integer, ByVal RegNo As String, ByVal RegFormNo As String, ByVal PlaceofBirth As String, ByVal MotherTongue As String,
            ByVal SecondLanguage As String, ByVal BloodGroup As String, ByVal PinCode As String, ByVal FatherTitle As String, ByVal FatherFirstName As String, ByVal FatherLastName As String,
            ByVal FatherOccupation As String, ByVal FatherDesignation As String, ByVal FatherQualification As String, ByVal FatherIncome As String,
            ByVal MotherTitle As String, ByVal MotherFirstName As String, ByVal MotherLastName As String, ByVal MotherOccupation As String,
            ByVal MotherDesignation As String, ByVal MotherQualification As String, ByVal MotherIncome As String, ByVal GTitle As String,
            ByVal GFirstName As String, ByVal GLastName As String, ByVal GOccupation As String, ByVal GDesignation As String, ByVal GQualification As String,
            ByVal GIncome As String, ByVal GAddress As String, ByVal GState As String, ByVal GCity As String, ByVal GEmail As String, ByVal GMobile As String,
            ByVal GPinCode As String, ByVal LastSchool As String, ByVal LastClassId As Integer, ByVal LastSession As String, ByVal LastPercentage As Decimal,
            ByVal LastCGPA As String, ByVal LastBoard As String, ByVal LastResult As String, ByVal LastLeavingReason As String, ByVal Religion As String,
            ByVal isTransport As Integer, ByVal pickupPoint As Integer, ByVal RTE As String, ByVal isAdmissionFeesFree As Integer,
            ByVal isTuitionFeesFree As Integer, ByVal FatherMobile As String, ByVal MotherMobile As String, ByVal House As String, ByVal Category As String,
            ByVal FeeCategory As String, ByVal Section As String, ByVal StudentFirstName As String, ByVal StudentLastName As String, ByVal isOnewayTransoprt As Integer,
            ByVal TuitionFeeConcession As Integer, ByVal SchoolSession As String) As String

        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ParentId", ParentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DOJ", DOJ, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentName", StudentName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherName", FatherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherName", MotherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DOB", DOB, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Nationality", Nationality, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Gender", Gender, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LocalityId", LocalityId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContactNo", ContactNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailId", EmailId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isBlock", isBlock, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RegNo", RegNo, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RegFormNo", RegFormNo, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PlaceofBirth", PlaceofBirth, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherTongue", MotherTongue, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SecondLanguage", SecondLanguage, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BloodGroup", BloodGroup, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherTitle", FatherTitle, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherFirstName", FatherFirstName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherLastName", FatherLastName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherOccupation", FatherOccupation, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherDesignation", FatherDesignation, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherQualification", FatherQualification, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherIncome", FatherIncome, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherTitle", MotherTitle, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherFirstName", MotherFirstName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherLastName", MotherLastName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherOccupation", MotherOccupation, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherDesignation", MotherDesignation, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherQualification", MotherQualification, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherIncome", MotherIncome, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GTitle", GTitle, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GFirstName", GFirstName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GLastName", GLastName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GOccupation", GOccupation, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GDesignation", GDesignation, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GQualification", GQualification, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GIncome", GIncome, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GAddress", GAddress, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GState", GState, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GCity", GCity, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GEmail", GEmail, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GMobile", GMobile, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GPinCode", GPinCode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastSchool", LastSchool, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastClassId", LastClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastSession", LastSession, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastPercentage", LastPercentage, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastCGPA", LastCGPA, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastBoard", LastBoard, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastResult", LastResult, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LastLeavingReason", LastLeavingReason, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Religion", Religion, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isTransport", isTransport, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@pickupPoint", pickupPoint, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RTE", RTE, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PinCode", PinCode, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isAdmissionFeesFree", isAdmissionFeesFree, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isTuitionFeesFree", isTuitionFeesFree, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherMobile", FatherMobile, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherMobile", MotherMobile, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@House", House, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Category", Category, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FeeCategory", FeeCategory, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Section", Section, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentFirstName", StudentFirstName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentLastName", StudentLastName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isOnewayTransoprt", isOnewayTransoprt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TuitionFeeConcession", TuitionFeeConcession, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_RegisterStudentNew", arrList.ToArray())
        Return Result
    End Function

    Sub AddClass(ByVal ClassId As Integer, ByVal ClassName As String, ByVal Section As String, ByVal ClassRoomNo As Integer, ByVal ClassHead As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassName", ClassName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Section", Section, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassRoomNo", ClassRoomNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassHead", ClassHead, SqlDbType.VarChar, 220, ParameterDirection.Input))

        ExecNonQueryProc("Prc_AddClass", arrList.ToArray())

    End Sub


    Sub AddRoom(ByVal RoomNo As String, ByVal Block As String, ByVal SchoolFloor As Integer, ByVal Capacity As Integer, ByVal DeActivated As Integer)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@RoomNo", RoomNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Block", Block, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolFloor", SchoolFloor, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Capacity", Capacity, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.VarChar, 220, ParameterDirection.Input))

        ExecNonQueryProc("Prc_AddUpdateRoom", arrList.ToArray())

    End Sub


    Sub AddSubject(ByVal SubjectCode As String, ByVal SubjectName As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SubjectCode", SubjectCode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubjectName", SubjectName, SqlDbType.VarChar, 200, ParameterDirection.Input))


        ExecNonQueryProc("Prc_AddUpdateSubject", arrList.ToArray())

    End Sub



    Function AddEmployee(ByVal EmployeeId As String, ByVal EmployeeName As String, ByVal FatherName As String, ByVal MotherName As String, ByVal DOB As String, ByVal Nationality As String, ByVal Gender As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal LocalityId As Integer, ByVal ContactNo As String, ByVal EmailId As String, ByVal isBlock As Integer, ByVal Designation As String, ByVal Department As String, ByVal EmpType As String, ByVal Bank As String, ByVal Pan As String, ByVal ESI As String, ByVal PF As String, ByVal payScale As String, ByVal Salary As String, ByVal JoinDate As String, ByVal Confirm As String, ByVal Increment As String, ByVal Leaving As String, ByVal FileNo As String, ByVal Retirnment As String, ByVal Qualification As String, ByVal Subject As String, ByVal NextIncr As String, ByVal Category As String, ByVal ProvisionDate As String, ByVal ContractExDate As String, ByVal PFStratDate As String, ByVal Grade As String, ByVal MaritalStatus As String, ByVal Blood As String, ByVal Adhar As String, ByVal DrivLicnce As String, ByVal ElectionCard As String, ByVal RationCard As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeName", EmployeeName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherName", FatherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MotherName", MotherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Nationality", Nationality, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DOB", DOB, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Gender", Gender, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LocalityId", LocalityId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContactNo", ContactNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmailId", EmailId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isBlock", isBlock, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Designation", Designation, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Department", Department, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmpType", EmpType, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Bank", Bank, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Pan", Pan, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ESI", ESI, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PF", PF, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@payScale", payScale, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Salary", Salary, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@JoinDate", JoinDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Confirm", Confirm, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Increment", Increment, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Leaving", Leaving, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FileNo", FileNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Retirnment", Retirnment, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Qualification", Qualification, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Subject", Subject, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NextIncr", NextIncr, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Category", Category, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ProvisionDate", ProvisionDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContractExDate", ContractExDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PFStratDate", PFStratDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Grade", Grade, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MaritalStatus", MaritalStatus, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Blood", Blood, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Adhar", Adhar, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DrivLicnce", DrivLicnce, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ElectionCard", ElectionCard, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RationCard", RationCard, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_RegisterEmployee", arrList.ToArray())
        Return Result
    End Function


    Function GetParentList(ByVal ParentId As String, ByVal StudentId As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@ParentId", ParentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Get_ParentMaster", arrList.ToArray)
        Return Dt
    End Function



    Function GetStudentList(ByVal StudentId As String, ByVal StudentName As String, ByVal ClassId As Integer, ByVal Section As String, ByVal ParentId As String, ByVal Type As Integer, ByVal BirthdayFrom As String, ByVal BirthdayTo As String, ByVal AdmFrom As String, ByVal AdmTo As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@ParentId", ParentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentName", StudentName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BirthdayFrom", BirthdayFrom, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BirthdayTo", BirthdayTo, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Section", Section, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AdmFrom", AdmFrom, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AdmTo", AdmTo, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Get_StudentMaster", arrList.ToArray)

        Return Dt
    End Function

    Function GetCollection(ByVal ClassIdFrom As Integer, ByVal MainClassIdTo As Integer, ByVal FromDate As String, ByVal ToDate As String, ByVal SessionId As Integer, ByVal Type As Integer, ByVal SchoolSession As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MainClassIdFrom", ClassIdFrom, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassIdTo", MainClassIdTo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionId", SessionId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_GetDailyCollectionReportSummary", arrList.ToArray)
        Return Dt
    End Function
    Function GetCollectionDetails(ByVal MainClassIdFrom As Integer, ByVal MainClassIdTo As Integer, ByVal FromDate As String, ByVal SessionId As Integer, ByVal Type As Integer, ByVal SchoolSession As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@MainClassIdFrom", MainClassIdFrom, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassIdTo", MainClassIdTo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Date", FromDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionId", SessionId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_GetDailyCollectionDetails", arrList.ToArray)
        Return Dt
    End Function


    Function GetEmployeeList(ByVal EmployeeId As String, ByVal EmployeeName As String, ByVal Designation As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeName", EmployeeName, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Designation", Designation, SqlDbType.VarChar, 20, ParameterDirection.Input))

        Dim Dt As DataTable = ExecDataTableProc("Get_EmployeeMaster", arrList.ToArray)
        Return Dt
    End Function

    Public Function CheckStudentLogin(ByVal UserId As String, ByVal Password As String, ByVal IPAddress As String) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@UserId", UserId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAddress", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("CheckStudentLogin", arrList.ToArray())
        Return dr
    End Function

    Sub AddSchool(ByVal SchoolName As String, ByVal Address As String, ByVal City As String, ByVal State As String, ByVal ContactNo As String, ByVal EMailId As String, ByVal DomainName As String, ByVal Prefix As String, ByVal FaxNo As String, ByVal FBPage As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SchoolName", SchoolName, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContactNo", ContactNo, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EMailId", EMailId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DomainName", DomainName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FaxNo", FaxNo, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Prefix", Prefix, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FBPage", FBPage, SqlDbType.VarChar, 200, ParameterDirection.Input))
        ExecNonQueryProc("Prc_SchoolMasterUpdate", arrList.ToArray())
    End Sub

    Sub AddDesignation(ByVal SNo As Integer, ByVal Designation As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SNo", SNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Designation", Designation, SqlDbType.VarChar, 200, ParameterDirection.Input))
        ExecNonQueryProc("Prc_AddUpdateDesignation", arrList.ToArray())
    End Sub
    Public Function CheckEmployeeLogin(ByVal UserId As String, ByVal Password As String, ByVal IPAddress As String) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@UserId", UserId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAddress", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("CheckEmployeeLogin", arrList.ToArray())
        Return dr
    End Function
    Public Function CheckParentLogin(ByVal UserId As String, ByVal Password As String, ByVal IPAddress As String) As SqlDataReader
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@UserId", UserId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAddress", IPAddress, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim dr As SqlDataReader = ExecDataReaderProc("CheckParentLogin", arrList.ToArray())
        Return dr
    End Function


    Sub AddNews(ByVal cnt As Integer, ByVal Title As String, ByVal News As String, ByVal Deactivated As Integer)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@cnt", cnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Title", Title, SqlDbType.VarChar, 1000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@News", News, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Deactivated", Deactivated, SqlDbType.Int, 0, ParameterDirection.Input))


        ExecNonQueryProc("Prc_AddUpdateNews", arrList.ToArray())

    End Sub



    Function AddSubjectDetail(ByVal SubjectCode As String, ByVal ClassId As Integer, ByVal SubjectDetail As String, ByVal UpdateBy As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SubjectCode", SubjectCode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubjectDetail", SubjectDetail, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UpdateBy", UpdateBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddSubjectDetail", arrList.ToArray())
        Return Result
    End Function



    Sub UpgradeClass(ByVal StudentIds As String, ByVal OldClassId As Integer, ByVal NewClassId As Integer)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentIds, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NewClassId", NewClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        ExecNonQueryProc("Prc_UpgradeStudent", arrList.ToArray())
    End Sub

    Sub UpgradeStudent(ByVal StudentId As String, ByVal NewMainClassId As Integer)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NewMainClassId", NewMainClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        ExecNonQueryProc("Prc_UpgradeStudent", arrList.ToArray())
    End Sub
    Sub UpgradeStudentwithSection(ByVal StudentId As String, ByVal NewMainClassId As Integer, ByVal Section As String, ByVal SchoolSessionActive As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NewMainClassId", NewMainClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Section", Section, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSessionActive", SchoolSessionActive, SqlDbType.VarChar, 20, ParameterDirection.Input))
        ExecNonQueryProc("Prc_UpgradeStudentwithSection", arrList.ToArray())
    End Sub

    Function AddNotice(ByVal Notice As String, ByVal NoticeFor As String, ByVal NoticeBy As String, ByVal ClassId As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Notice", Notice, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NoticeFor", NoticeFor, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NoticeBy", NoticeBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddNotice", arrList.ToArray())
        Return Result
    End Function



    Sub AddLibCategory(ByVal CategoryId As Integer, ByVal CategoryName As String, ByVal RackNo As Integer, ByVal Narration As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.Int, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryName", CategoryName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RackNo", RackNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Narration", Narration, SqlDbType.VarChar, 500, ParameterDirection.Input))
        ExecNonQueryProc("Prc_AddUpdateLibBooksCategories", arrList.ToArray())
    End Sub



    Function AddLibBook(ByVal BookId As String, ByVal CategoryId As Integer, ByVal BookName As String, ByVal Auther As String, ByVal Publication As String, ByVal Tags As String, ByVal Price As Decimal, ByVal TotalQty As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@BookId", BookId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BookName", BookName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Auther", Auther, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Publication", Publication, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Tags", Tags, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Price", Price, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TotalQty", TotalQty, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateLibBooks", arrList.ToArray())
        Return Result
    End Function

    Sub AddAttedance(ByVal UniqueId As String, ByVal MType As String, ByVal Status As String, ByVal SavedBy As String, ByVal ClassId As Integer, ByVal SchoolSession As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@UniqueId", UniqueId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MType", MType, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Status", Status, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Session", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SavedBy", SavedBy, SqlDbType.VarChar, 500, ParameterDirection.Input))

        ExecNonQueryProc("Prc_Attendance", arrList.ToArray())
    End Sub



    Function AddVehicle(ByVal VehicleId As String, ByVal VehicleNo As String, ByVal VehicleType As String, ByVal VehicleCompany As String, ByVal TotalSeats As Integer, ByVal VehicleRCNo As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@VehicleId", VehicleId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@VehicleNo", VehicleNo, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@VehicleType", VehicleType, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@VehicleCompany", VehicleCompany, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TotalSeats", TotalSeats, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@VehicleRCNo", VehicleRCNo, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateVehicle", arrList.ToArray())
        Return Result
    End Function



    Function AddTransEmployee(ByVal VehicleId As String, ByVal EmployeeId As String, ByVal DLNo As String, ByVal DLExpiryDate As String, ByVal BloodGroup As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@VehicleId", VehicleId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DLNo", DLNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DLExpiryDate", DLExpiryDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BloodGroup", BloodGroup, SqlDbType.VarChar, 50, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateTransEmployee", arrList.ToArray())
        Return Result
    End Function



    Function AddTransUser(ByVal VehicleId As String, ByVal UniqueId As String, ByVal MType As String, ByVal StartDate As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@VehicleId", VehicleId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UniqueId", UniqueId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MType", MType, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StartDate", StartDate, SqlDbType.VarChar, 200, ParameterDirection.Input))


        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateTransUsers", arrList.ToArray())
        Return Result
    End Function
    Function AddReceipt(ByVal StudentId As String, ByVal ReceiptNo As String, ByVal ReceiptAmount As Decimal, ByVal Discount As Decimal, ByVal LateFee As Decimal,
                        ByVal DueDate As String, ByVal SavedBy As String, ByVal DiscountType As String, ByVal DiscountPer As Decimal, ByVal StudentFeeStuctureTableType As DataTable) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptAmount", ReceiptAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Discount", Discount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LateFee", LateFee, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DueDate", DueDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SavedBy", SavedBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DiscountType", SavedBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DiscountPer", DiscountPer, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FeeStucture", StudentFeeStuctureTableType, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_CreateReceipt", arrList.ToArray())
        Return Result
    End Function
    Function AddReceiptSession(ByVal StudentId As String, ByVal ReceiptNo As String, ByVal ReceiptAmount As Decimal, ByVal Discount As Decimal, ByVal LateFee As Decimal,
                        ByVal DueDate As String, ByVal SavedBy As String, ByVal DiscountType As String, ByVal DiscountPer As Decimal, ByVal StudentFeeStuctureTableType As DataTable, ByVal SchoolSession As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptAmount", ReceiptAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Discount", Discount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LateFee", LateFee, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DueDate", DueDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SavedBy", SavedBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DiscountType", SavedBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DiscountPer", DiscountPer, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FeeStucture", StudentFeeStuctureTableType, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_CreateSessionReceipt", arrList.ToArray())
        Return Result
    End Function
    Public Function BindFeeType() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select * from dbo.tbl_FeeType")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindTermFrequency() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("Select * from tbl_TermFrequency")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function



    Function AddFees(ByVal ReceiptNo As String, ByVal PaidDate As String, ByVal Remark As String, ByVal LateFee As Decimal, ByVal PMode As String, ByVal ChequeNo As String, ByVal ChequeDate As String, ByVal Chequeamount As Decimal, ByVal BankName As String, ByVal BranchName As String, CashAmount As Decimal, DueAmount As Decimal) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PaidDate", PaidDate, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LateFee", LateFee, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PMode", PMode, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeNo", ChequeNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeDate", ChequeDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeAmount", Chequeamount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BankName", BankName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchName", BranchName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CashAmount", CashAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DueAmount", DueAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))

        Dim Result As String = ExecNonQueryProc("Prc_FeeSubmit", arrList.ToArray())
        Return Result
    End Function
    Function AddFeesSession(ByVal ReceiptNo As String, ByVal PaidDate As String, ByVal Remark As String, ByVal LateFee As Decimal, ByVal PMode As String, ByVal ChequeNo As String, ByVal ChequeDate As String, ByVal Chequeamount As Decimal, ByVal BankName As String, ByVal BranchName As String, CashAmount As Decimal, DueAmount As Decimal, ByVal SchoolSession As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PaidDate", PaidDate, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LateFee", LateFee, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PMode", PMode, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeNo", ChequeNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeDate", ChequeDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeAmount", Chequeamount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BankName", BankName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchName", BranchName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CashAmount", CashAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DueAmount", DueAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))

        Dim Result As String = ExecNonQueryProc("Prc_FeeSubmitSession", arrList.ToArray())
        Return Result
    End Function
    Function PayProduct(ByVal ReceiptNo As String, ByVal PaidDate As String, ByVal Remark As String, ByVal LateFee As Decimal, ByVal PMode As String, ByVal ChequeNo As String, ByVal ChequeDate As String, ByVal Chequeamount As Decimal, ByVal BankName As String, ByVal BranchName As String, CashAmount As Decimal, ByVal SchoolSession As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PaidDate", PaidDate, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@LateFee", LateFee, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PMode", PMode, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeNo", ChequeNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeDate", ChequeDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ChequeAmount", Chequeamount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BankName", BankName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BranchName", BranchName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CashAmount", CashAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_PayProduct", arrList.ToArray())
        Return Result
    End Function
    Public Function BindTeachers() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("Select EmployeeId,EmployeeName from dbo.EmployeeMaster Where isBlock=0 order by EmployeeName")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindSubjects() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("Select SubjectCode,SubjectName from dbo.SubjectMaster order by SubjectName")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindClasses() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("Select ClassId,ClassName + '-' +Section Class from ClassMaster Order by ClassId")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Function AddClassTimeTable(ByVal ClassId As Integer, ByVal SavedBy As String, ByVal ClassTimeTableType As DataTable) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SavedBy", SavedBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassTimeTable", ClassTimeTableType, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddClassTimeTable", arrList.ToArray())
        Return Result
    End Function
    Public Function BindAvailableTeachers(ByVal ClassId As Integer, ByVal WeekNo As Integer, ByVal LecNo As Integer) As DataTable
        Dim mDataTable As DataTable
        Try
            Dim arrList As New ArrayList
            arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
            arrList.Add(PrepareCommand("@WeekNo", WeekNo, SqlDbType.Int, 0, ParameterDirection.Input))
            arrList.Add(PrepareCommand("@LecNo", LecNo, SqlDbType.Int, 0, ParameterDirection.Input))
            mDataTable = ExecDataTableProc("Get_AvailableTeachers", arrList.ToArray())
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Function Getbook(ByVal CategoryId As Integer, ByVal BookId As String, ByVal BookTag As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BookId", BookId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BookTag", BookTag, SqlDbType.VarChar, 200, ParameterDirection.Input))

        Dim Dt As DataTable = ExecDataTableProc("Get_LibBooks", arrList.ToArray)
        Return Dt
    End Function

    Function Getissuedbook(ByVal BookId As String, ByVal StudentId As String) As DataTable
        Dim arrList As New ArrayList


        arrList.Add(PrepareCommand("@BookId", BookId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))

        Dim Dt As DataTable = ExecDataTableProc("Get_LibIssueBooks", arrList.ToArray)
        Return Dt
    End Function

    Function IssueBook(ByVal BookId As String, ByVal StudentId As String, ByVal EmployeeId As String, ByVal IssueDate As String, ByVal IssueBy As String, ByVal IssueDays As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@BookId", BookId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IssueDate", IssueDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IssueBy", IssueBy, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IssueDays", IssueDays, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_IssueLibBooks", arrList.ToArray())
        Return Result
    End Function

    Function ReturnBook(ByVal IssueId As Integer, ByVal ReturnDate As String, ByVal ExtraDays As Integer, ByVal Fine As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@IssueId", IssueId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReturnDate", ReturnDate, SqlDbType.VarChar, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ExtraDays", ExtraDays, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Fine", Fine, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_LibBookReturn", arrList.ToArray())
        Return Result
    End Function



    Function AddHostel(ByVal HostelId As Integer, ByVal HostelName As String, ByVal HostelAddress As String, ByVal ContactNo1 As String, ByVal ContactNo2 As String, ByVal Email As String, ByVal HostelInfo As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@HostelId", HostelId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HostelName", HostelName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HostelAddress", HostelAddress, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Email", Email, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContactNo1", ContactNo1, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContactNo2", ContactNo2, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HostelInfo", HostelInfo, SqlDbType.NVarChar, 2000, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateHostelInfo", arrList.ToArray())
        Return Result
    End Function


    Function AddHolydays(ByVal HolidaysStuctureTableType As DataTable) As String
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@Holidaystbl", HolidaysStuctureTableType, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_Holidays", arrList.ToArray())
        Return Result
    End Function

    Function AddHostelRoom(ByVal HostelRoomId As Integer, ByVal HostelId As Integer, ByVal RoomNo As String, ByVal RoomType As String, ByVal TotalBed As Integer, ByVal AllotedBed As Integer, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@HostelRoomId", HostelRoomId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HostelId", HostelId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RoomNo", RoomNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RoomType", RoomType, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TotalBed", TotalBed, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AllotedBed", AllotedBed, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateHostelRoom", arrList.ToArray())
        Return Result
    End Function

    Function AddHostelSession(ByVal SessionId As Integer, ByVal SessionName As String, ByVal SessionYear As String, ByVal FromDate As String, ByVal ToDate As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SessionId", SessionId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionName", SessionName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionYear", SessionYear, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateSession", arrList.ToArray())
        Return Result
    End Function
    Function AddSession(ByVal SessionId As Integer, ByVal SessionName As String, ByVal SessionYear As String, ByVal FromDate As String, ByVal ToDate As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SessionId", SessionId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionName", SessionName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionYear", SessionYear, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateSession", arrList.ToArray())
        Return Result
    End Function


    Function IssueBook(ByVal HostelRoomId As Integer, ByVal StudentId As String, ByVal EmployeeId As String, ByVal StartDate As String, ByVal EndDate As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@HostelRoomId", HostelRoomId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StartDate", StartDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EndDate", EndDate, SqlDbType.VarChar, 500, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_IssueHostelRoom", arrList.ToArray())
        Return Result
    End Function

    Function AddWarden(ByVal WardenId As Integer, ByVal HostelId As Integer, ByVal SessionId As String, ByVal EmployeeId As String, ByVal AllotmentDate As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@WardenId", WardenId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HostelId", HostelId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionId", SessionId, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AllotmentDate", AllotmentDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddWarden", arrList.ToArray())
        Return Result
    End Function
    Function AddEvent(ByVal EventId As Integer, ByVal EventName As String, ByVal EventDetail As String, ByVal FromDate As String, ByVal ToDate As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@EventId", EventId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EventName", EventName, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EventDetail", EventDetail, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateEvent", arrList.ToArray())
        Return Result
    End Function

    Function AddVisitors(ByVal VisitDate As String, ByVal StudentId As String, ByVal EmployeeId As String, ByVal VisitorName As String, ByVal Relation As String, ByVal NOP As Integer, ByVal VisitDetails As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@VisitDate", VisitDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@VisitorName", VisitorName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Relation", Relation, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NOP", NOP, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@VisitDetails", VisitDetails, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))

        Dim Result As String = ExecNonQueryProc("Prc_AddVisitors", arrList.ToArray())
        Return Result
    End Function


    Function AddExam(ByVal ExamId As Integer, ByVal ExamName As String, ByVal SessionYear As String, ByVal StartDate As String, ByVal EndDate As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ExamId", ExamId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ExamName", ExamName, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionYear", SessionYear, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StartDate", StartDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EndDate", EndDate, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))

        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateExamMaster", arrList.ToArray())
        Return Result
    End Function

    Function AddExamShift(ByVal ShiftId As Integer, ByVal ExamId As Integer, ByVal ShiftTime As String, ByVal DeActivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ShiftId", ShiftId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ExamId", ExamId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ShiftTime", ShiftTime, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateExamShift", arrList.ToArray())
        Return Result
    End Function
    Public Function BindExams() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select Examid,ExamName + ' ' + SessionYear ExamName from  tbl_ExamMaster where Deactivated=0 order by Startdate")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindExamshifts(ByVal examid As Integer) As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select ShiftId,ShiftTime from  tbl_ExamShifts where Deactivated=0 and examid=@examid", "@examid", examid)
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function


    Function AddExamParticulars(ByVal ExamId As Integer, ByVal ExamDate As String, ByVal ShiftId As Integer, ByVal ClassId As Integer, ByVal SubjectCode As String, ByVal MaxTheoryMarks As Integer, ByVal MaxPracticalMarks As Integer, ByVal PassTheoryMarks As Integer, ByVal PassPracticalMarks As Integer, ByVal PeriodicTestMarksMax As Integer, ByVal NotebookMarksMax As Integer, ByVal SubEnrichmentMarksMax As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ShiftId", ShiftId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ExamId", ExamId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ExamDate", ExamDate, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubjectCode", SubjectCode, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MaxTheoryMarks", MaxTheoryMarks, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MaxPracticalMarks", MaxPracticalMarks, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PassTheoryMarks", PassTheoryMarks, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PassPracticalMarks", PassPracticalMarks, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PeriodicTestMarksMax", PeriodicTestMarksMax, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NotebookMarksMax", NotebookMarksMax, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubEnrichmentMarksMax", SubEnrichmentMarksMax, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))

        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateExamParticulars", arrList.ToArray())
        Return Result
    End Function
    Function Get_ExamPeriodicTest(ByVal ExamId As Integer, ByVal ClassId As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ExamId", ExamId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))

        Dim dt As DataTable = ExecDataTableProc("Get_ExamPeriodicTest", arrList.ToArray())
        Return dt
    End Function
    Function AddExamPeriodicTest(ByVal ExamId As Integer, ByVal ClassId As Integer, ByVal MaxTheoryMarks As Integer, ByVal PeriodicTestMarksMax1 As Integer, ByVal NotebookMarksMax As Integer, ByVal SubEnrichmentMarksMax As Integer, ByVal PassTotalMarks As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ExamId", ExamId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MaxTheoryMarks", MaxTheoryMarks, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PeriodicTestMarksMax1", PeriodicTestMarksMax1, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NotebookMarksMax", NotebookMarksMax, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubEnrichmentMarksMax", SubEnrichmentMarksMax, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PassTotalMarks", PassTotalMarks, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))

        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateExamPeriodicTest", arrList.ToArray())
        Return Result
    End Function
    Function AddExamMarks(ByVal ExamId As Integer, ByVal StudentId As String, ByVal SubjectId As Integer, ByVal TeacherId As String, ByVal PeriodicTestMarksMax As Decimal, ByVal PeriodicTestMarks As Decimal, ByVal NotebookMarksMax As Decimal, ByVal NotebookMarks As Decimal, ByVal SubEnrichmentMarksMax As Decimal, ByVal SubEnrichmentMarks As Decimal, ByVal HalfYearlyMarksMax As Decimal, ByVal HalfYearlyMarks As Decimal, ByVal TotalMarksMax As Decimal, ByVal TotalMarks As Decimal, ByVal Grade As String, ByVal ClassId As Integer, ByVal MxaPracticalMarks As Integer, ByVal Attandance As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ExamId", ExamId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubjectId", SubjectId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TeacherId", TeacherId, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PeriodicTestMarksMax", PeriodicTestMarksMax, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PeriodicTestMarks", PeriodicTestMarks, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NotebookMarksMax", NotebookMarksMax, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NotebookMarks", NotebookMarks, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubEnrichmentMarksMax", SubEnrichmentMarksMax, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubEnrichmentMarks", SubEnrichmentMarks, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HalfYearlyMarksMax", HalfYearlyMarksMax, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HalfYearlyMarks", HalfYearlyMarks, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TotalMarksMax", TotalMarksMax, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TotalMarks", TotalMarks, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Grade", Grade, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MxaPracticalMarks", MxaPracticalMarks, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Attandance", Attandance, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))

        Dim Result As String = ExecNonQueryProc("Prc_AddExamMarks", arrList.ToArray())
        Return Result
    End Function



    Function AddExamRoom(ByVal ExamRoomId As Integer, ByVal ShiftId As Integer, ByVal RoomNo As Integer, ByVal TotClasses As Integer, ByVal RRows As Integer, ByVal RCols As Integer, ByVal SType As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ShiftId", ShiftId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ExamRoomId", ExamRoomId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RoomNo", RoomNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TotClasses", TotClasses, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RRows", RRows, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RCols", RCols, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SType", SType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateExamRooms", arrList.ToArray())
        Return Result
    End Function
    Function Get_ExamParticulars(ByVal ExamId As Integer, ByVal ClassId As Integer, ByVal ShiftId As Integer, ByVal SubjectCode As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ShiftId", ShiftId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ExamId", ExamId, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubjectCode", SubjectCode, SqlDbType.NVarChar, 400, ParameterDirection.Input))

        Dim dt As DataTable = ExecDataTableProc("Get_ExamParticulars", arrList.ToArray())
        Return dt
    End Function
    Function UploadStudata(ByVal StudentStuctureTableType As DataTable) As String
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@Studenttbl", StudentStuctureTableType, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_StudentBulkRegistration", arrList.ToArray())
        Return Result
    End Function


    Function AddDocument(ByVal DocumentName As String, ByVal DocumentDetail As String, ByVal DownloadURL As String, ByVal UploadBy As String, ByVal SubjectCode As String, ByVal ClassId As Integer, ByVal IsAssignment As Integer, ByVal ContentType As String, ByVal isGlobal As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@DocumentName", DocumentName, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DocumentDetail", DocumentDetail, SqlDbType.NVarChar, 4000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DownloadURL", DownloadURL, SqlDbType.VarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UploadBy", UploadBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubjectCode", SubjectCode, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsAssignment", IsAssignment, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ContentType", ContentType, SqlDbType.VarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isGlobal", isGlobal, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))

        Dim Result As String = ExecNonQueryProc("Prc_UploadDocuments", arrList.ToArray())
        Return Result
    End Function



    Function AddExamRoomClasses(ByVal ExamRoomId As Integer, ByVal ClassId As Integer, ByVal StartRollNo As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ExamRoomId", ExamRoomId, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@StartRollNo", StartRollNo, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateExamRoomClasses", arrList.ToArray())
        Return Result
    End Function
    Public Function UpdateEmployeePwd(ByVal EmployeeId As String, ByVal OldPwd As String, ByVal NewPwd As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OldPwd", OldPwd, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NewPwd", NewPwd, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_UpdateEmployeePwd", arrList.ToArray)
        Return result
    End Function




    Function Get_Notice(ByVal EmployeeId As String, ByVal StudenId As String, ByVal ParentId As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudenId", StudenId, SqlDbType.VarChar, 20, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@ParentId", ParentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Get_Notice", arrList.ToArray())
        Dim Notice As String = ""
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Notice = row.Item("Notice") + "</br>"
            Next
        End If


        Return Notice
    End Function

    Public Function UpdateStudentPwd(ByVal EmployeeId As String, ByVal OldPwd As String, ByVal NewPwd As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", EmployeeId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OldPwd", OldPwd, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NewPwd", NewPwd, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_UpdateStudentPwd", arrList.ToArray)
        Return result
    End Function

    Public Function AddComplaints(ByVal StudentId As String, ByVal ParentId As String, ByVal ComplaintBehalf As String, ByVal ComplaintSubject As String, ByVal ComplaintBefore As String, ByVal TeacherName As String, ByVal PreviousComplaintDate As String, ByVal PreviousComplaintStatus As String, ByVal ComplaintDetail As String, ByVal RefComplaintNo As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ParentID", ParentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ComplaintBehalf", ComplaintBehalf, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ComplaintSubject", ComplaintSubject, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ComplaintBefore", ComplaintBefore, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TeacherName", TeacherName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PreviousComplaintDate", PreviousComplaintDate, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PreviousComplaintStatus", PreviousComplaintStatus, SqlDbType.VarChar, 5000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ComplaintDetail", ComplaintDetail, SqlDbType.VarChar, 5000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RefComplaintNo", RefComplaintNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_AddComplaints", arrList.ToArray)
        Return result
    End Function

    Public Function AddExamSupervisors(ByVal ExamRoomId As Integer, ByVal EmployeeId As String, ByVal IsDelete As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ExamRoomId", ExamRoomId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsDelete", IsDelete, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_AddDeleteExamSupervisors", arrList.ToArray)
        Return result
    End Function




    Function AddSubjectTopicDetail(ByVal TopicId As Integer, ByVal SubjectCode As String, ByVal ClassId As Integer, ByVal Topic As String, ByVal TopicDetail As String, ByVal UpdateBy As String, ByVal TopicSno As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@TopicId", TopicId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SubjectCode", SubjectCode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Topic", Topic, SqlDbType.NVarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TopicDetail", TopicDetail, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TopicSno", TopicSno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UpdateBy", UpdateBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddSubjectTopic", arrList.ToArray())
        Return Result
    End Function



    Function WallPostAdd(ByVal Msg As String, ByVal ImgUrl As String, ByVal ById As String, ByVal Operato As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Msg", Msg, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ImgUrl", ImgUrl, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ById", ById, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Operator", Operato, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_WallPostAdd", arrList.ToArray())
        Return Result
    End Function

    Function WallPostCommentAdd(ByVal PostId As Integer, ByVal Msg As String, ByVal ImgUrl As String, ByVal ById As String, ByVal Operato As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@PostId", PostId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Msg", Msg, SqlDbType.NVarChar, 8000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ImgUrl", ImgUrl, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ById", ById, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Operator", Operato, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_WallPostCommentAdd", arrList.ToArray())
        Return Result
    End Function
    Function InsertUpdateProducts(ByVal id As Integer, ByVal code As String, ByVal ParentId As Integer, ByVal shortname As String, ByVal fullname As String, ByVal Duration As String, ByVal MRP As String, ByVal DP As String, ByVal PV As String, ByVal CategoryId As String, ByVal isactive As String, ByVal ImageName As String, ByVal Description As String, ByVal taxvalue As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@id", id, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ParentId", ParentId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Code", code, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ShortName", shortname, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FullName", fullname, SqlDbType.NVarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Duration", Duration, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MRP", MRP, SqlDbType.NVarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DP", DP, SqlDbType.NVarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PV", PV, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.NVarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsActive", isactive, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Operator", ctx.Session("Loginid"), SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@imagename", ImageName, SqlDbType.NVarChar, 5000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@description", Description, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@taxvalue", taxvalue, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Return", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_Inv_Product", arrList.ToArray())
        Return Result
    End Function

    Function AddUpdateProductCategories(ByVal CategoryId As Integer, ByVal CategoryName As String, ByVal MainClassId As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryName", CategoryName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassId", MainClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateProductCategories", arrList.ToArray())
        Return Result
    End Function
    Function AddUpdateProductMaster(ByVal ProductId As Integer, ByVal ProductName As String, ByVal MRP As Decimal, ByVal Quantity As Integer, ByVal Publisher As String, ByVal Supplier As String, ByVal ProductCategoryId As Integer, ByVal ProductType As String, ByVal isActive As Integer, ByVal SchoolSession As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ProductId", ProductId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ProductName", ProductName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MRP", MRP, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Quantity", Quantity, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Publisher", Publisher, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Supplier", Supplier, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ProductCategoryId", ProductCategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ProductType", ProductType, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isActive", isActive, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateProductMaster", arrList.ToArray())
        Return Result
    End Function
    Function Get_PDCBillingByStudent(ByVal StudenId As String, ByVal type As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudenId, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@type", type, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Get_PDCBillingByStudent", arrList.ToArray())
        Return dt
    End Function
    Function Get_PDCBillingByStudentFuture(ByVal StudenId As String, ByVal type As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudenId, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@type", type, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Get_PDCBillingByStudentFuture", arrList.ToArray())
        Return dt
    End Function
    Function AddProductReceipt(ByVal StudentId As String, ByVal ReceiptNo As String, ByVal ReceiptAmount As Decimal, ByVal Discount As Decimal, ByVal DueDate As String, ByVal SavedBy As String, ByVal DetailStucture As DataTable, ByVal SchoolSession As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptAmount", ReceiptAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Discount", Discount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DueDate", DueDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SavedBy", SavedBy, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DetailStucture", DetailStucture, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_CreateProductReceiptNew", arrList.ToArray())
        Return Result
    End Function
    Function ProspectusSale(ByVal ReceiptNo As String, ByVal FormNo As String, ByVal ReceiptDate As String, ByVal StudentName As String, ByVal StudentLastName As String, ByVal FatherName As String, ByVal FatherLastName As String, ByVal Mobile As String, ByVal MainClassId As Integer, ByVal Amount As Decimal, ByVal Remark As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FormNo", FormNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptDate", ReceiptDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentName", StudentName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentLastName", StudentLastName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherName", FatherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherLastName", FatherLastName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassId", MainClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_ProspectusSale", arrList.ToArray())
        Return Result
    End Function
    Function ProspectusSaleSession(ByVal ReceiptNo As String, ByVal FormNo As String, ByVal ReceiptDate As String, ByVal StudentName As String, ByVal StudentLastName As String, ByVal FatherName As String, ByVal FatherLastName As String, ByVal Mobile As String, ByVal MainClassId As Integer, ByVal Amount As Decimal, ByVal Remark As String, ByVal SchoolSession As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FormNo", FormNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptDate", ReceiptDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentName", StudentName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentLastName", StudentLastName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherName", FatherName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherLastName", FatherLastName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassId", MainClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_ProspectusSaleSession", arrList.ToArray())
        Return Result
    End Function
    Function Get_OutstandingFeeReport(ByVal FromDate As String, ByVal ToDate As String, ByVal MainClassId As Integer, ByVal StudenId As String, ByVal FeeType As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassId", MainClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudenId, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FeeType", FeeType, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Prc_OutstandingFees", arrList.ToArray())
        Return dt
    End Function
    Function DeleteReceipt(ByVal SCode As String, ByVal ReceiptCnt As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SCode", SCode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptCnt", ReceiptCnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_DeleteReceipt", arrList.ToArray())
        Return Result
    End Function
    Function CancelReceipt(ByVal SCode As String, ByVal ReceiptCnt As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SCode", SCode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptCnt", ReceiptCnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_CancelReceipt", arrList.ToArray())
        Return Result
    End Function
    Function Get_ReceiptProspectusList(ByVal FromDate As String, ByVal ToDate As String, ByVal MainClassIdFrom As Integer, ByVal MainClassIdTo As Integer, ByVal ReceiptNo As String, ByVal FormNo As String,
                                       ByVal StudentName As String, ByVal FatherName As String, ByVal Mobile As String, ByVal Type As Integer, ByVal SchoolSession As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassIdFrom", MainClassIdFrom, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassIdTo", MainClassIdTo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptNo", ReceiptNo, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FormNo", FormNo, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentName", StudentName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FatherName", FatherName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Get_ReceiptProspectusList", arrList.ToArray())
        Return dt
    End Function
    Function Get_UnpaidPDCBillingByStudent(ByVal StudenId As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudenId, SqlDbType.VarChar, 50, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Get_UnpaidPDCBillingByStudent", arrList.ToArray())
        Return dt
    End Function
    Function Get_PaidUnpaidPDCBillingByStudent(ByVal StudenId As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudenId, SqlDbType.VarChar, 50, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Get_PaidUnpaidPDCBillingByStudent", arrList.ToArray())
        Return dt
    End Function
    Function Delete_PDCBillingByPDCId(ByVal PDCId As Integer, ByVal SCode As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@PDCId", PDCId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SCode", SCode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Delete_PDCBillingByPDCId", arrList.ToArray())
        Return Result
    End Function
    Function Update_PDCBillingMaster(ByVal StudenId As String, ByVal ClassId As Integer, ByVal SchoolSession As String, ByVal isAdmissionFeesFree As Integer, ByVal isTuitionFeesFree As Integer, ByVal isTransport As Integer, ByVal pickupPoint As Integer, ByVal action As String, ByVal SCode As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@StudentId", StudenId, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isAdmissionFeesFree", isAdmissionFeesFree, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isTuitionFeesFree", isTuitionFeesFree, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isTransport", isTransport, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@pickupPoint", pickupPoint, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@action", action, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SCode", SCode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Update_PDCBillingMaster", arrList.ToArray())
        Return Result
    End Function
    Function AddUpdatePickupPoints(ByVal PickupPointId As Integer, ByVal PickupPoint As String, ByVal Amount As Decimal) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@PickupPointId", PickupPointId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PickupPoint", PickupPoint, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdatePickupPoints", arrList.ToArray())
        Return Result
    End Function
    Function CancelProductReceipt(ByVal SCode As String, ByVal ReceiptCnt As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SCode", SCode, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ReceiptCnt", ReceiptCnt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_CancelProductReceipt", arrList.ToArray())
        Return Result
    End Function


    Public Function AddUpdatePayScaleMaster(ByVal PSID As Integer, ByVal PayScaleName As String, ByVal DA As String, ByVal DAPer As Decimal, ByVal DAFix As Decimal, ByVal HRA As String, ByVal HRAPer As Decimal, ByVal HRAFix As Decimal, ByVal OtherAllowance As String, ByVal OAPer As Decimal, ByVal OAFix As Decimal, ByVal PF As String, ByVal PFPer As Decimal, ByVal PFDA As Decimal, ByVal PFFix As Decimal, ByVal TDS As String, ByVal TDSPer As Decimal, ByVal TDSFix As Decimal, ByVal OD As String, ByVal ODPer As Decimal, ODFix As Decimal, ByVal ESI As String, ByVal ESIPer As Decimal, ByVal ESIDA As Decimal, ByVal ESIFix As Decimal, ByVal PaidLeaves As Decimal) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@PSId", PSID, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PayScaleName", PayScaleName, SqlDbType.VarChar, 200, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@DAType", DA, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DAFix", DAFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DABasicPer", DAPer, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@HRAType", HRA, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HRAFix", HRAFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HRABasicPer", HRAPer, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@OAType", OtherAllowance, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OAFix", OAFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OABasicPer", OAPer, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@PFType", PF, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PFFix", PFFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PFBasicPer", PFPer, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PFDAPer", PFDA, SqlDbType.Decimal, 0, ParameterDirection.Input))


        arrList.Add(PrepareCommand("@TDSType", TDS, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TDSFix", TDSFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TDSBasicPer", TDSPer, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@OtherDeduction", ODFix, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@ESIType", ESI, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ESIFix", ESIFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ESIBasicPer", ESIPer, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ESIODPer", ESIDA, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@PaidLeaves", PaidLeaves, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Deactivated", 0, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_PayScaleMaster", arrList.ToArray())
        Return Result
    End Function
    Function GetDailyCollectionSummary(ByVal MainClassIdFrom As Integer, ByVal MainClassIdTo As Integer, ByVal FromDate As String, ByVal ToDate As String, ByVal StudentId As String, ByVal SessionId As Integer, ByVal Type As Integer, ByVal SchoolSession As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@MainClassIdFrom", MainClassIdFrom, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassIdTo", MainClassIdTo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionId", SessionId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_GetInstallmentDailySummary", arrList.ToArray)
        Return Dt
    End Function
    Function GetDailyCollectionDetails(ByVal MainClassIdFrom As Integer, ByVal MainClassIdTo As Integer, ByVal FromDate As String, ByVal StudentId As String, ByVal SessionId As Integer, ByVal Type As Integer, ByVal SchoolSession As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@MainClassIdFrom", MainClassIdFrom, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassIdTo", MainClassIdTo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Dated", FromDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SessionId", SessionId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_GetInstallmentDetails", arrList.ToArray)
        Return Dt
    End Function
    Function GetEmployeeSalary(ByVal EmployeeId As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 20, ParameterDirection.Input))

        Dim Dt As DataTable = ExecDataTableProc("Prc_GetEmployeePayScaleMaster", arrList.ToArray)
        Return Dt
    End Function
    Public Function BindPayScale() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("select PSId,PayScaleName from dbo.PayScaleMaster")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function

    Public Function AddUpdateEmployeePayScaleMaster(ByVal EmployeeId As String, ByVal PSID As Integer, ByVal DA As String, ByVal DAPer As Decimal, ByVal DAFix As Decimal, ByVal HRA As String, ByVal HRAPer As Decimal, ByVal HRAFix As Decimal, ByVal OtherAllowance As String, ByVal OAPer As Decimal, ByVal OAFix As Decimal, ByVal PF As String, ByVal PFPer As Decimal, ByVal PFDA As Decimal, ByVal PFFix As Decimal, ByVal TDS As String, ByVal TDSPer As Decimal, ByVal TDSFix As Decimal, ByVal OD As String, ByVal ODPer As Decimal, ODFix As Decimal, ByVal ESI As String, ByVal ESIPer As Decimal, ByVal ESIOD As Decimal, ByVal ESIFix As Decimal, ByVal PaidLeaves As Decimal, ByVal ConvenceCharges As Decimal, ByVal AdvanceAmount As Decimal) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@PSId", PSID, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 200, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@DAType", DA, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DAFix", DAFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DABasicPer", DAPer, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@HRAType", HRA, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HRAFix", HRAFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HRABasicPer", HRAPer, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@OAType", OtherAllowance, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OAFix", OAFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OABasicPer", OAPer, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@PFType", PF, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PFFix", PFFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PFBasicPer", PFPer, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PFDAPer", PFDA, SqlDbType.Decimal, 0, ParameterDirection.Input))


        arrList.Add(PrepareCommand("@TDSType", TDS, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TDSFix", TDSFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TDSBasicPer", TDSPer, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@OtherDeduction", ODFix, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@ESIType", ESI, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ESIFix", ESIFix, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ESIBasicPer", ESIPer, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ESIODPer", ESIOD, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@PaidLeaves", PaidLeaves, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Deactivated", 0, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@ConvenceCharges", ConvenceCharges, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Advance", AdvanceAmount, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_EmployeePayScaleMaster", arrList.ToArray())
        Return Result
    End Function

    Public Function EmployeeMonthlySalaryGeneration(ByVal EmployeeId As String, ByVal SalaryMonth As Integer, ByVal SalaryYear As Integer, ByVal WorkingDays As Integer,
                                                    ByVal Leaves As Decimal, ByVal PaidLeaves As Decimal, ByVal SalaryDays As String, ByVal CarryPaidLeaves As Decimal) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SalaryMonth", SalaryMonth, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SalaryYear", SalaryYear, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@WorkingDays", WorkingDays, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Leaves", Leaves, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PaidLeaves", PaidLeaves, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SalaryDays", SalaryDays, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CarryPaidLeaves", CarryPaidLeaves, SqlDbType.Decimal, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_EmployeeMonthlySalaryGeneration", arrList.ToArray())
        Return Result
    End Function
    Public Function EmployeeMonthlySalaryPaid(ByVal EmployeeId As String, ByVal SalaryMonth As Integer, ByVal SalaryYear As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@EmployeeId", EmployeeId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SalaryMonth", SalaryMonth, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SalaryYear", SalaryYear, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_EmployeeMonthlySalaryPaid", arrList.ToArray())
        Return Result
    End Function
    Public Function BindSchoolHeader() As String
        Dim dt As DataTable
        dt = ExecDataTable("select * from schoolmaster")
        Dim Result As String = "<div style='padding: 5px;'><span style='font-weight: bold; font-size: 16px;'>" & dt.Rows(0)("SchoolName").ToString() & "</span> <br />"
        Result &= " <span style='font-size: 14px;'>" & dt.Rows(0)("Address").ToString() & "," & dt.Rows(0)("City").ToString() & "," & dt.Rows(0)("State").ToString() & "<br />"
        Result &= "Phone: " & dt.Rows(0)("ContactNo").ToString() & "&nbsp;&nbsp;&nbsp;&nbsp;Email:" & dt.Rows(0)("EMailId").ToString() & "</span></div>"
        Return Result
    End Function
    Public Function BindSchoolName() As String
        Dim dt As DataTable
        dt = ExecDataTable("select * from schoolmaster")
        Dim Result As String = dt.Rows(0)("SchoolName").ToString()
        Return Result
    End Function
    Function GetProductMaster(ByVal ProductName As String, ByVal Publisher As String, ByVal Supplier As String, ByVal ProductType As String, ByVal ProductCategoryId As Integer, ByVal SchoolSession As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ProductName", ProductName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Publisher", Publisher, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Supplier", Supplier, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ProductType", ProductType, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ProductCategoryId", ProductCategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Prc_GetProductMaster", arrList.ToArray())
        Return dt
    End Function


    Function Get_ExamResult(ByVal SubjectCode As String, ByVal ExamID As Integer, ByVal StduentId As String) As DataTable

        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@SubjectCode", SubjectCode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ExamId", ExamID, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StduentId, SqlDbType.VarChar, 200, ParameterDirection.Input))

        Dim dt As DataTable = ExecDataTableProc("Get_ExamResult", arrList.ToArray())
        Return dt
    End Function
    Function GetProductReceiptSummary(ByVal FromDate As String, ByVal ToDate As String, ByVal MainClassId As Integer, ByVal StudentId As String, ByVal PMode As String, ByVal SchoolSession As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainClassId", MainClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PMode", PMode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSession", SchoolSession, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim dt As DataTable = ExecDataTableProc("Prc_GetProductReceiptSummary", arrList.ToArray())
        Return dt
    End Function
    Sub AddUpdateBusRoute(ByVal RouteId As Integer, ByVal RouteName As String, ByVal PickupPoints As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@RouteId", RouteId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RouteName", RouteName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PickupPoints", PickupPoints, SqlDbType.VarChar, 2000, ParameterDirection.Input))
        ExecNonQueryProc("Prc_AddUpdateBusRoute", arrList.ToArray())

    End Sub
    Function GetConvenceStudents(ByVal StudentId As String, ByVal StudentName As String, ByVal ClassId As Integer, ByVal Section As String, ByVal RouteId As Integer, ByVal PickupPointId As Integer) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentName", StudentName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Section", Section, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RouteId", RouteId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PickupPointId", PickupPointId, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Get_ConvenceStudents", arrList.ToArray)

        Return Dt
    End Function
    Public Function BindBusRouteMaster() As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("Select RouteId,RouteName from BusRouteMaster Where Deactivated is Null order by RouteName")
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Public Function BindBusRoutePickupPoints(ByVal RouteId As Integer) As DataTable
        Dim mDataTable As DataTable
        Try
            mDataTable = ExecDataTable("Select PickupPointId,PickupPoint from tbl_PickupLocation Where (@RouteId=0 or RouteId=@RouteId) order by Amount", "@RouteId", RouteId)
        Catch ex As Exception
            mDataTable = Nothing
        Finally

        End Try
        Return mDataTable
    End Function
    Function GetStudentListSession(ByVal StudentId As String, ByVal StudentName As String, ByVal ClassId As Integer, ByVal Section As String, ByVal ParentId As String, ByVal Type As Integer, ByVal BirthdayFrom As String, ByVal BirthdayTo As String, ByVal AdmFrom As String, ByVal AdmTo As String, ByVal SchoolSessionActive As String) As DataTable
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@ParentId", ParentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentId", StudentId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StudentName", StudentName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BirthdayFrom", BirthdayFrom, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BirthdayTo", BirthdayTo, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ClassId", ClassId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Section", Section, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AdmFrom", AdmFrom, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AdmTo", AdmTo, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SchoolSessionActive", SchoolSessionActive, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Get_StudentMasterSession", arrList.ToArray)

        Return Dt
    End Function

    Public Function AddUpdateCustomer(ByVal CustomerId As String, ByVal CustomerName As String, ByVal Email As String, ByVal Mobile As String, ByVal DOB As String,
            ByVal Address As String, ByVal City As String, ByVal State As String, ByVal Pincode As String, ByVal GSTNo As String, ByVal RegSession As String, ByVal CompanyId As Integer, ByVal Deactivated As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CustomerId", CustomerId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CustomerName", CustomerName, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Email", Email, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DOB", DOB, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Address", Address, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@City", City, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@State", State, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Pincode", Pincode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@GSTNo", GSTNo, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@RegSession", RegSession, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CompanyId", CompanyId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Deactivated", Deactivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_AddUpdateCustomer", arrList.ToArray())
        Return Result
    End Function
    Function GetCustomers(ByVal CompanyId As Integer, ByVal CustomerId As String, ByVal CustomerName As String, ByVal FromDate As String, ByVal ToDate As String, ByVal Mobile As String, ByVal Session As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CompanyId", CompanyId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CustomerId", CustomerId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CustomerName", CustomerName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ToDate", ToDate, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Mobile", Mobile, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Session", Session, SqlDbType.VarChar, 20, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Prc_GetCustomers", arrList.ToArray)

        Return Dt
    End Function
    Function GetCategories(ByVal Type As Integer, ByVal MainCategoryId As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainCategoryId", MainCategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_GetCategories", arrList.ToArray)

        Return Dt
    End Function
    Public Function AddEditCategories(ByVal CategoryId As Integer, ByVal CategoryName As String, ByVal MainCategoryId As Integer, ByVal AppearinHomePage As Integer, ByVal SequenceNo As Integer, ByVal CreateBy As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryName", CategoryName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MainCategoryId", MainCategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@AppearinHomePage", AppearinHomePage, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SequenceNo", SequenceNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CreateBy", CreateBy, SqlDbType.VarChar, 100, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_AddEditCategories", arrList.ToArray)

        Return Dt
    End Function
    Function SetCategorySequence(ByVal TotalCount As Integer, ByVal SequenceString As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@TotalCount", TotalCount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SequenceString", SequenceString, SqlDbType.VarChar, 2000, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_SetCategorySequence", arrList.ToArray)

        Return Dt
    End Function
    Function SetCategoryStatus(ByVal ProcessType As String, ByVal UniqueId As Integer, ByVal DeActivated As Integer, ByVal IPAddress As String, ByVal UpdateBy As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ProcessType", ProcessType, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UniqueId", UniqueId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAddress", IPAddress, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UpdateBy", UpdateBy, SqlDbType.VarChar, 50, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_SetActiveStatus", arrList.ToArray)

        Return Dt
    End Function
    Function GetColors(ByVal ColorId As Integer, ByVal ColorName As String, ByVal ColorCode As String, ByVal Action As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ColorId", ColorId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ColorName", ColorName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ColorCode", ColorCode, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Action", Action, SqlDbType.VarChar, 50, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("Sp_GetColors", arrList.ToArray)

        Return Dt
    End Function
    Function GetBrands(ByVal BrandID As Integer, ByVal BrandCode As String, ByVal BrandName As String, ByVal BrandImage As String, ByVal Action As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@BrandID", BrandID, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BrandCode", BrandCode, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BrandName", BrandName, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BrandImage", BrandImage, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Action", Action, SqlDbType.VarChar, 50, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_Brands", arrList.ToArray)

        Return Dt
    End Function
    Function GetProductsListing(ByVal Param As String, ByVal CategoryCode As String, ByVal PageNo As Integer, ByVal Record As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Param", Param, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryCode", CategoryCode, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PageNo", PageNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Record", Record, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_GetProductsListing", arrList.ToArray)

        Return Dt
    End Function
    Function GetProductsListingPagesCount(ByVal Param As String, ByVal CategoryCode As String, ByVal PageNo As Integer, ByVal Record As Integer) As Integer
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Param", Param, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryCode", CategoryCode, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PageNo", PageNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Record", Record, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim Result As Integer = ExecScalarProc("sp_GetProductsListingPagesCount", arrList.ToArray)

        Return Result
    End Function
    Function AddEditProducts(ByVal CatalogueId As Integer, ByVal CatCode As String, ByVal MemberSNo As Integer, ByVal CategoryId As Integer, ByVal CatalogueTitle As String, ByVal CatalogueDescription As String,
                              ByVal ThumbnailURL As String, ByVal CatalogueURL As String, ByVal CatalogueSizeMB As Decimal, ByVal CatalogueTags As String, ByVal VideoURL As String, ByVal MoreDetailURL As String,
                             ByVal CreateBy As String, ByVal BrandId As Integer, ByVal Size As String, ByVal Color As String, ByVal price As Decimal, ByVal Weight As String, ByVal SKUCode As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CatalogueId", CatalogueId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatCode", CatCode, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberSNo", MemberSNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CategoryId", CategoryId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueTitle", CatalogueTitle, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueDescription", CatalogueDescription, SqlDbType.NVarChar, 4000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ThumbnailURL", ThumbnailURL, SqlDbType.VarChar, 2000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueURL", CatalogueURL, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueSizeMB", CatalogueSizeMB, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueTags", CatalogueTags, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@VideoURL", VideoURL, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MoreDetailURL", MoreDetailURL, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CreateBy", CreateBy, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BrandId", BrandId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Size", Size, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Color", Color, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@price", price, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Weight", Weight, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@SKUCode", SKUCode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_AddEditProducts", arrList.ToArray)

        Return Dt
    End Function
    Function UploadProduct(ByVal CatalogueId As Integer, ByVal MemberSNo As Integer, ByVal CatalogueURL As String, ByVal CatalogueSizeMB As Decimal, ByVal CreateBy As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CatalogueId", CatalogueId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberSNo", MemberSNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueURL", CatalogueURL, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueSizeMB", CatalogueSizeMB, SqlDbType.Decimal, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CreateBy", CreateBy, SqlDbType.VarChar, 50, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_UploadProduct", arrList.ToArray)

        Return Dt
    End Function
    Function DeleteCatalogueImagesThumbnail(ByVal ProcessType As String, ByVal UniqueId As Integer, ByVal DeActivated As Integer, ByVal IPAddress As String, ByVal UpdateBy As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ProcessType", ProcessType, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UniqueId", UniqueId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DeActivated", DeActivated, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IPAddress", IPAddress, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UpdateBy", UpdateBy, SqlDbType.VarChar, 50, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_DeleteCatalogueImagesThumbnail", arrList.ToArray)

        Return Dt
    End Function
    Function SetProductDefaultImage(ByVal CatalogueId As Integer, ByVal MemberSNo As Integer, ByVal CatalogueImageId As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CatalogueId", CatalogueId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberSNo", MemberSNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueImageId", CatalogueImageId, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_SetProductDefaultImage", arrList.ToArray)

        Return Dt
    End Function
    Function MemberProductDelete(ByVal MemberSNo As Integer, ByVal CatalogueId As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MemberSNo", MemberSNo, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CatalogueId", CatalogueId, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_MemberProductDelete", arrList.ToArray)

        Return Dt
    End Function
    Function GetProductImages(ByVal CatalogueId As Integer, ByVal Type As Integer) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@CatalogueId", CatalogueId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.Int, 0, ParameterDirection.Input))
        Dim Dt As DataTable = ExecDataTableProc("sp_GetProductImages", arrList.ToArray)

        Return Dt
    End Function

End Class


