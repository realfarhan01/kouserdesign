Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Web.Mail
Imports System.Xml
Imports System.Data

Public Class DeveloperLogicLayer
    Inherits DataAccessLayer
    Dim ctx As HttpContext = HttpContext.Current
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
    Public Function DisabledButtonCode(Optional ByVal validationGroup As String = "") As String
        Dim sbValid As New System.Text.StringBuilder()
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ")
        sbValid.Append("if (Page_ClientValidate('" & validationGroup & "') == false) { return false; }} ")
        sbValid.Append("this.value = 'Please wait...';")
        sbValid.Append("this.disabled = true;")
        Return sbValid.ToString
    End Function
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
    Public Function ManageItemMaster(ByVal ItemID As Integer, ByVal ItemName As String, ByVal Amount As Decimal, ByVal PV As Integer, ByVal CapAt As Integer, ByVal BinaryI As Decimal, ByVal DirectI As Decimal, ByVal IsPaid As String, ByVal isBlock As Integer, ByVal isTopUp As Integer, ByVal forRegistration As Integer, ByVal forSubPanel As Integer) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ItemID", ItemID, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ItemName", ItemName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PV", PV, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CapAt", CapAt, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@BinaryI", BinaryI, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DirectI", DirectI, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsPaid", IsPaid, SqlDbType.VarChar, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isBlock", isBlock, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isTopUp", isTopUp, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@forRegistration", forRegistration, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@forSubPanel", forSubPanel, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 20, ParameterDirection.Output))

        result = ExecNonQueryProc("dev_ManageItemMaster", arrList.ToArray())
        Return result
    End Function
    Public Sub Addmilestone(ByVal Milestone As String, ByVal DateWork As Date, ByVal Isimportant As Integer, ByVal Iscompleted As Integer)
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@Milestone", Milestone, SqlDbType.VarChar, 350, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DateWork", DateWork, SqlDbType.Date, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Isimportant", Isimportant, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Iscompleted", Iscompleted, SqlDbType.Int, 0, ParameterDirection.Input))

        ExecNonQueryProc("Prc_Add_Milestone", arrList.ToArray)
    End Sub
    Public Function BulkRegistration(ByVal totalid As Integer, ByVal Scheme As Integer, ByVal IntroId As String, ByVal IsSameIntro As Integer, ByVal Leg As String, ByVal DefaultID As String, ByVal MemberName As String, ByVal Country As String, ByVal Password As String, ByVal IP As String, ByVal DTUser As String) As String
        Dim result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@totalid", totalid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Scheme", Scheme, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IntroId", IntroId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsSameIntro", IsSameIntro, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Leg", Leg, SqlDbType.VarChar, 1, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DefaultID", DefaultID, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberName", MemberName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Country", Country, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IP", IP, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@DTUser", DTUser, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 20, ParameterDirection.Output))

        result = ExecNonQueryProc("dev_Registration_bulk", arrList.ToArray())
        Return result
    End Function
    Public Function GetUnpaidMembers(ByVal IntroId As String, ByVal MemberId As String) As DataTable
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@IntroId", IntroId, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MemberId", MemberId, SqlDbType.VarChar, 20, ParameterDirection.Input))

        Dim Dt As DataTable = ExecDataTableProc("dev_GetUnpaidMembers", arrList.ToArray)
        Return Dt
    End Function
    Function Get_RegistrationProduct() As SqlDataReader
        Dim dr As SqlDataReader
        dr = ExecDataReader("Select itemname,itemid  From itemMaster where isBlock=0 and forRegistration=1")
        Return dr
    End Function
    Function Get_TopupProduct() As SqlDataReader
        Dim dr As SqlDataReader
        dr = ExecDataReader("Select Itemid,Itemname From ItemMaster Where isTopUp=1 and isBlock=0")
        Return dr
    End Function
    Public Sub CreatePopupXml(ByVal MenuStr As String)

        Dim xd As New System.Xml.XmlDocument
        Dim MenusNode As XmlNode = xd.CreateElement("Popups")
        xd.AppendChild(MenusNode)



        Dim sql As String = "select row_number() over(order by menuid) as Sno,* from MemberMenu where MenuID in (" & MenuStr & ")"
        Dim dr As SqlDataReader = ExecDataReader(sql)
        While dr.Read


            Dim Popup As XmlElement = xd.CreateElement("Popup")


            Dim Id As XmlElement = xd.CreateElement("MenuID")
            Id.InnerText = dr("MenuID")
            Popup.AppendChild(Id)
            Dim MenuName As XmlElement = xd.CreateElement("MenuName")
            MenuName.InnerText = dr("MenuName")
            Popup.AppendChild(MenuName)
            Dim Url As XmlElement = xd.CreateElement("MenuUrl")
            Url.InnerText = dr("MenuUrl")
            Popup.AppendChild(Url)
            Dim PopupImage As XmlElement = xd.CreateElement("PopupImage")
            PopupImage.InnerText = dr("PopupImage")
            Popup.AppendChild(PopupImage)
            Dim Message As XmlElement = xd.CreateElement("NotificationMsg")
            Message.InnerText = dr("NotificationMsg")
            Popup.AppendChild(Message)
            Dim Type As XmlElement = xd.CreateElement("NotificationType")
            Type.InnerText = dr("NotificationType")
            Popup.AppendChild(Type)
       
            Dim Header As XmlElement = xd.CreateElement("NotificationHeader")
            Header.InnerText = dr("NotificationHeader").ToString()
            Popup.AppendChild(Header)

            MenusNode.AppendChild(Popup)






        End While
        xd.Save(ctx.Server.MapPath("~/Xml/") & "Popup.xml")
    End Sub


    Public Sub UpdatePopup(ByVal MenuId As Integer, ByVal PopupImage As String, ByVal NotificationMsg As String, ByVal NotificationType As String, ByVal NotificationHeader As String)
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@MenuId", MenuId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PopupImage", PopupImage, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NotificationMsg", NotificationMsg, SqlDbType.NVarChar, 400, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NotificationType", NotificationType, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NotificationHeader", NotificationHeader, SqlDbType.VarChar, 200, ParameterDirection.Input))

        ExecNonQueryProc("dev_UpdatePopup", arrList.ToArray)
    End Sub


    Public Sub CreateMemberMainmenu(ByVal MenuId As Integer, ByVal MenuName As String, ByVal MenuLevel As Integer, ByVal MenuUrl As String, ByVal MenuParentid As Integer, ByVal MenuPagename As String, ByVal IsMenuActive As Integer, ByVal MenuStr As String)
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@MenuId", MenuId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuName", MenuName, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuLevel", MenuLevel, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuUrl", MenuUrl, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuParentid", MenuParentid, SqlDbType.Int, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuPagename", MenuPagename, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsMenuActive", IsMenuActive, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuStr", MenuStr, SqlDbType.VarChar, 20, ParameterDirection.Input))
        ExecNonQueryProc("dev_CreateMemberMainMenu", arrList.ToArray)
    End Sub

    Public Sub UpdateVirtualName(ByVal MenuId As Integer, ByVal VirtualName As String, ByVal PageTitle As String, ByVal PageHeader As String, ByVal HelpText As String, ByVal MenuIcon As String)
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@MenuId", MenuId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@VirtualName", VirtualName, SqlDbType.NVarChar, 255, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PageTitle", PageTitle, SqlDbType.NVarChar, 255, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@PageHeader", PageHeader, SqlDbType.NVarChar, 255, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@HelpText", HelpText, SqlDbType.NVarChar, 255, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MenuIcon", MenuIcon, SqlDbType.NVarChar, 255, ParameterDirection.Input))
        ExecNonQueryProc("dev_UpdateVirtualName", arrList.ToArray)
    End Sub
End Class


