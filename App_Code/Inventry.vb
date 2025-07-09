Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Web.Mail

Public Class Inventry
    Inherits DataAccessLayer

    Dim ctx As HttpContext = HttpContext.Current
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
    Private Function CreateCommand(ByVal Query As String, ByVal CmdType As CommandType, ByVal ParamArray obj() As Object) As SqlCommand
        Dim cmd As New SqlCommand(Query)
        Try
            cmd.CommandType = CmdType
            For i As Integer = 0 To obj.Length - 1
                If TypeOf obj(i) Is String And i < obj.Length - 1 Then
                    Dim Parm As New SqlParameter
                    Parm.ParameterName = obj(i)
                    i = i + 1
                    Parm.Value = obj(i)
                    cmd.Parameters.Add(Parm)
                ElseIf TypeOf obj(i) Is SqlParameter Then
                    cmd.Parameters.Add(obj(i))
                Else
                    Throw New ArgumentException("Invalid number or type of arguments supplied")
                End If
            Next
        Catch ex As Exception
            Return Nothing
        End Try
        Return cmd
    End Function
    Public Function ExecScalar(ByVal Query As String, ByVal ParamArray obj() As Object) As Object
        Dim result As Object
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("TCConnection").ConnectionString)
            Using cmd As SqlCommand = CreateCommand(Query, CommandType.Text, obj)
                Try
                    cmd.Connection = conn
                    conn.Open()
                    result = cmd.ExecuteScalar()
                Catch ex As Exception
                    result = Nothing
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
        Return result
    End Function
    Function InsertUpdateProducts(ByVal id As Integer, ByVal code As String, ByVal shortname As String, ByVal fullname As String, ByVal Duration As String, ByVal MRP As String, ByVal PV As String, ByVal CategoryId As String, ByVal isactive As String, ByVal ImageName As String, ByVal Description As String, ByVal taxvalue As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@id", id, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Code", code, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@ShortName", shortname, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@FullName", fullname, SqlDbType.NVarChar, 220, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Duration", Duration, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@MRP", MRP, SqlDbType.NVarChar, 10, ParameterDirection.Input))
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
    Function InsertSalesOrder(ByVal memberid As String, ByVal franchiseeid As String, ByVal orderitems As String, ByVal totalmrpamount As String, ByVal totaldpamount As String, ByVal totalamount As String, ByVal totaltaxamount As String, ByVal totalbv As String, ByVal isprintinvoice As String, ByVal paymentmode As String, ByVal Ordertype As String, ByVal CouponCode As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@memberid", memberid, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@franchiseeid", franchiseeid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@orderitems", orderitems, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@totalmrpamount", totalmrpamount, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@totaldpamount", totaldpamount, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@totalamount", totalamount, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@totaltexamount", totaltaxamount, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@totalbv", totalbv, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isprintinvoice", isprintinvoice, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@paymentmode", paymentmode, SqlDbType.NVarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Ordertype", Ordertype, SqlDbType.NVarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CouponCode", CouponCode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_Inv_SalesOrder", arrList.ToArray())
        Return Result
    End Function
    Function InsertSalesOrderDetails(ByVal orderid As String, ByVal franchiseeid As String, ByVal salesinvitems As DataTable) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@orderid", orderid, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@franchiseeid", franchiseeid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@salesinvitems", salesinvitems, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_inv_SalesOrderDetails", arrList.ToArray())
        Return Result
    End Function
    Function InsertUserSalesOrder(ByVal memberid As String, ByVal isprintinvoice As String, ByVal orderitems As String, ByVal totalamount As String, ByVal totalbv As String, ByVal totalmrpamount As String, ByVal salesinvitems As DataTable) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@memberid", memberid, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@isprintinvoice", isprintinvoice, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@orderitems", orderitems, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@totalamount", totalamount, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@totalbv", totalbv, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@totalmrpamount", totalmrpamount, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@salesinvitems", salesinvitems, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_Inv_SalesOrderUser", arrList.ToArray())
        Return Result
    End Function


    Function InsertUserSalesOrderPayment(ByVal Orderid As Integer, ByVal Ptype As Integer, ByVal Bankname As String, ByVal Accountno As String, ByVal Holdername As String, ByVal Chqddno As String, ByVal Reciept As String, ByVal Remark As String, ByVal Branchname As String, ByVal Pdate As String) As String
        Dim arrList As New ArrayList

        arrList.Add(PrepareCommand("@Orderid", Orderid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Ptype", Ptype, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Bankname", Bankname, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Accountno", Accountno, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Holdername", Holdername, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Chqddno", Chqddno, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Reciept", Reciept, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Branchname", Branchname, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Pdate", Pdate, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("prc_inv_insertOrderPayment", arrList.ToArray())
        Return Result
    End Function
    Public Function cnf_AccountStatement(ByVal FromDate As String, ByVal ToDate As String, ByVal cnfid As String, ByVal TranType As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@cnfid", cnfid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TranType", TranType, SqlDbType.VarChar, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_inv_walletstatement_cnf", arrList.ToArray)
        Return DT
    End Function
    Public Function franchisee_AccountStatement(ByVal FromDate As String, ByVal ToDate As String, ByVal cnfid As String, ByVal TranType As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@cnfid", cnfid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TranType", TranType, SqlDbType.VarChar, 2, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_inv_walletstatement_franchise", arrList.ToArray)
        Return DT
    End Function
    Public Function cnf_AddRemoveAmount(ByVal cnfid As String, ByVal Amount As Decimal, ByVal Remark As String, ByVal TType As Integer) As String
        Dim Result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@cnfid", cnfid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TType", TType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Result = ExecNonQueryProc("inv_franchiseAddRemoveAmountToWallet", arrList.ToArray)

        Return Result
    End Function
    Public Function franchisee_AddRemoveAmount(ByVal cnfid As String, ByVal Amount As Decimal, ByVal Remark As String, ByVal TType As Integer) As String
        Dim Result As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@cnfid", cnfid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Amount", Amount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Remark", Remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TType", TType, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Result = ExecNonQueryProc("inv_franchiseAddRemoveAmountToWallet", arrList.ToArray)

        Return Result
    End Function
    Public Function Bindstate()
        Dim dt As DataTable
        dt = ExecDataTable("select ID,state from dbo.State order by state")
        Return dt
    End Function
    Public Function CNF_Registration(ByVal Msrno As Integer, ByVal Name As String, ByVal EmailID As String, ByVal Mobile As String, ByVal StateID As Integer,
                                    ByVal Address As String, ByVal Password As String, ByVal Fax As String, ByVal Tin As String,
                                    ByVal contactperson As String, ByVal contactpersonphone As String, ByVal operatorid As String, ByVal status As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@id", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmname", Name, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@loginid", EmailID, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmphone", Mobile, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@StateID", StateID, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmaddress", Address, SqlDbType.NVarChar, 2000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmfaxno", Fax, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmtinno", Tin, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@contactperson", contactperson, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@contactpersonphone", contactpersonphone, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@operatorid", operatorid, SqlDbType.VarChar, 150, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@status", status, SqlDbType.NVarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_Inv_CNFManage", arrList.ToArray())
        Return Result
    End Function
    Public Function Inv_AddEditFranchise(ByVal Msrno As Integer, ByVal cnfid As Integer, ByVal cityid As Integer, ByVal Name As String, ByVal EmailID As String, ByVal Mobile As String, ByVal StateID As Integer,
                                  ByVal Address As String, ByVal Password As String, ByVal Fax As String, ByVal Tin As String,
                                  ByVal contactperson As String, ByVal contactpersonphone As String, ByVal operatorid As String, ByVal Ftype As String, ByVal initialamount As Integer, ByVal walletamount As Integer, ByVal Memberid As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@id", Msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@cnfid", cnfid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@cityid", cityid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmname", Name, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@loginid", EmailID, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmphone", Mobile, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@stateid", StateID, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmaddress", Address, SqlDbType.NVarChar, 2000, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Password", Password, SqlDbType.NVarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmfaxno", Fax, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@firmtinno", Tin, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@contactperson", contactperson, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@contactpersonphone", contactpersonphone, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@operatorid", operatorid, SqlDbType.VarChar, 150, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Ftype", Ftype, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@initialamount", initialamount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@walletamount", walletamount, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Memberid", Memberid, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("Prc_Inv_AddEditFranchise", arrList.ToArray())
        Return Result
    End Function
    Function UpdateProductstock(ByVal Categoryid As Integer, ByVal Productid As Integer, ByVal Quantity As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Categoryid", Categoryid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Productid", Productid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Quantity", Quantity, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("prc_inv_stockmaster", arrList.ToArray())
        Return Result
    End Function

    
    Function cnfstocktransfer(ByVal franchiseeid As Integer, ByVal itemTotal As Integer, ByVal mrpTotal As Integer, ByVal dpTotal As Integer, ByVal taxTotal As String, ByVal bvTotal As String, ByVal netTotal As String, ByVal TransferFrom As String, ByVal Paytype As String, ByVal transferStatus As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@franchiseeid", franchiseeid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@itemTotal", itemTotal, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@mrpTotal", mrpTotal, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@dpTotal", dpTotal, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@taxTotal", taxTotal, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@bvTotal", bvTotal, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@netTotal", netTotal, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@TransferFrom", TransferFrom, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Paytype", Paytype, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@transferStatus", transferStatus, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("inv_franchiseeStockTransfer", arrList.ToArray())
        Return Result
    End Function
    Function cnfstocktransferdetails(ByVal franchiseeid As Integer, ByVal frStockTransferId As Integer, ByVal salesinvitems As DataTable) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@franchiseeid", franchiseeid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@frStockTransferId", frStockTransferId, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@salesinvitems", salesinvitems, SqlDbType.Structured, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("inv_franchiseeStockTransferDetails", arrList.ToArray())
        Return Result
    End Function
    Public Function cnf_UpdatePassword(ByVal cnfid As String, ByVal OldPwd As String, ByVal NewPwd As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@cnfid", cnfid, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@OldPwd", OldPwd, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@NewPwd", NewPwd, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_inv_franchisechangepassword", arrList.ToArray)
        Return result
    End Function
    Function Franchisestocktransfer(ByVal frid As Integer, ByVal Categoryid As Integer, ByVal Productid As Integer, ByVal Quantity As Integer, ByVal remark As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@frid", frid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Categoryid", Categoryid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Productid", Productid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Quantity", Quantity, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@remark", remark, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("prc_inv_frstocktransfer", arrList.ToArray())
        Return Result
    End Function
    Public Function Addocuments(ByVal Title As String, ByVal Doc As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@Title", Title, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Doc", Doc, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_Addocumentsbyadmin", arrList.ToArray)
        Return result
    End Function

    Public Function AddDocument(ByVal ID As Integer, ByVal msrno As Integer, ByVal pancard As String, ByVal addressproof As String, ByVal cancelledcheque As String, ByVal Type As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@ID", ID, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@msrno", msrno, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@pancard", pancard, SqlDbType.NVarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@addressproof", addressproof, SqlDbType.NVarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@cancelledcheque", cancelledcheque, SqlDbType.NVarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.NVarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("prc_Adddocument", arrList.ToArray())
        Return Result
    End Function


    Public Function Kycdeocumentdetails(ByVal MemberID As String, ByVal FromDate As String, ByVal ToDate As String) As DataTable
        Dim mDataTable As DataTable
        Dim arrList As New ArrayList
        'If Not IsDate(FromDate) Then
        '    FromDate = Nothing
        'End If
        'If Not IsDate(ToDate) Then
        '    ToDate = Nothing
        'End If
        arrList.Add(PrepareCommand("@MemberID", MemberID, SqlDbType.VarChar, 50, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Frmdate", FromDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 20, ParameterDirection.Input))
        mDataTable = ExecDataTableProc("Prc_deocumentdetails", arrList.ToArray)
        Return mDataTable
    End Function
    Public Function AlbumManage(ByVal AlbumID As Integer, ByVal Name As String, ByVal Pic As String, ByVal Type As String) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@AlbumID", AlbumID, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Name", Name, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Pic", Pic, SqlDbType.VarChar, 500, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Type", Type, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim result As String = ExecNonQueryProc("Prc_AlbumManage", arrList.ToArray)
        Return result
    End Function
    Public Function franchisee_SalesReport(ByVal FromDate As String, ByVal ToDate As String, ByVal cnfid As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@franchiseeid", cnfid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_Inv_SalesReport", arrList.ToArray)
        Return DT
    End Function
    Public Function franchisee_SalesProductWiseReport(ByVal FromDate As String, ByVal ToDate As String, ByVal cnfid As String, ByVal Ordertyoe As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@franchiseeid", cnfid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Ordertype", Ordertyoe, SqlDbType.VarChar, 100, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_Inv_SalesProductWiseReport", arrList.ToArray)
        Return DT
    End Function
    Public Function franchisee_CommissionReport(ByVal FromDate As String, ByVal ToDate As String, ByVal cnfid As String, ByVal IsPaid As Integer, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@franchiseeid", cnfid, SqlDbType.VarChar, 20, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@IsPaid", IsPaid, SqlDbType.Int, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_Inv_FrCommissionReport", arrList.ToArray)
        Return DT
    End Function
    Public Function franchisee_PaidCommission(ByVal FromDate As String, ByVal ToDate As String, ByVal cnfid As String, ByVal Export As Integer) As DataTable
        Dim arrList As New ArrayList
        If Not IsDate(FromDate) Then
            FromDate = Nothing
        End If
        If Not IsDate(ToDate) Then
            ToDate = Nothing
        End If
        arrList.Add(PrepareCommand("@FromDate", FromDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@Todate", ToDate, SqlDbType.VarChar, 10, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@franchiseeid", cnfid, SqlDbType.VarChar, 20, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Export", Export, SqlDbType.Bit, 0, ParameterDirection.Input))
        Dim DT As DataTable = ExecDataTableProc("Prc_Inv_FrPaidCommission", arrList.ToArray)
        Return DT
    End Function
    Function ApplyDiscountCoupon(ByVal OrderValue As Decimal, ByVal CouponCode As String, ByVal UserId As Integer) As String
        Dim arrList As New ArrayList
        arrList.Add(PrepareCommand("@OrderValue", OrderValue, SqlDbType.Decimal, 0, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@CouponCode", CouponCode, SqlDbType.VarChar, 200, ParameterDirection.Input))
        arrList.Add(PrepareCommand("@UserId", UserId, SqlDbType.Int, 0, ParameterDirection.Input))

        arrList.Add(PrepareCommand("@Result", "", SqlDbType.VarChar, 200, ParameterDirection.Output))
        Dim Result As String = ExecNonQueryProc("ApplyDiscountCoupon", arrList.ToArray())
        Return Result
    End Function

End Class
