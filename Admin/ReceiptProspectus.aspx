<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReceiptProspectus.aspx.vb" Inherits="ReceiptProspectus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Receipt</title>
    <link href="css/Receipt.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //function printRec() {
        //    var data = document.getElementById("tdMain").innerHTML;
        //    var mywindow = window.open('', 'new div', 'height=400,width=600');
        //    mywindow.document.write('<html><head><title>my div</title>');
        //    mywindow.document.write('<link rel="stylesheet" href="css/Receipt.css" type="text/css" />');
        //    mywindow.document.write('</head><body>');
        //    mywindow.document.write(data);
        //    mywindow.document.write('</body></html>');

        //    mywindow.print();
        //    mywindow.close();

        //    return true;
        //}
        function printRec() {
            window.print()
        }
    </script>
    <style type="text/css">
        .style1 {
            font-family: "Times New Roman", Times, serif;
            font-size: 24px;
        }
        .style2 {
            font-family: "Times New Roman", Times, serif;
            font-size: 16px;
        }
        .style3 {
            font-size: x-large;
            font-family: "Times New Roman", Times, serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="1000" cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td align="center" valign="top" id="tdMain">
                    <asp:Repeater ID="rptdetails" runat="server" DataMember="orderid">
                        <ItemTemplate>

                            <a onclick="printRec();" style="font-size: 15px; float: right; margin: 10px 0px;" id="printbtn"><i class="fa fa-print"></i></a>
                            <table width="100%" cellpadding="0" cellspacing="0" align="center">
                                <tr>
                                    <td align="center" valign="top" width="49%">
                                        <div style="padding: 5px;">
                                            <div align="center"><asp:Image ID="Image2" runat="server" ImageUrl="~/Admin/images/logo.png" /></div>
                                            <%--<span style="font-weight: bold;" class="style1"><%# Eval("SchoolName")%></span>--%>
                                            <br />
                                            <span class="style2">
                                                <%# Eval("SchoolAddress")%>, <%# Eval("Schoolcity")%>, <%# Eval("SchoolState")%><br />
                                                Phone: <%# Eval("ContactNo")%>&nbsp;&nbsp;&nbsp;&nbsp;Email: <%# Eval("EMailId")%></span>
                                        </div>
                                            <span style="float: right">OFFICE COPY</span>
                                    </td>
                                    <td align="center" valign="top" width="2%"></td>
                                    <td align="center" valign="top" width="49%">
                                        <div style="padding: 5px;">
                                            <div align="center"><asp:Image ID="Image1" runat="server" ImageUrl="~/Admin/images/logo.png" /></div>
                                            <%--<span style="font-weight: bold;" class="style1"><%# Eval("SchoolName")%></span>--%>
                                            <br />
                                            <span class="style2">
                                                <%# Eval("SchoolAddress")%>, <%# Eval("Schoolcity")%>, <%# Eval("SchoolState")%><br />
                                                Phone: <%# Eval("ContactNo")%>&nbsp;&nbsp;&nbsp;&nbsp;Email: <%# Eval("EMailId")%></span>
                                        </div>
                                            <span style="float: right">STUDENT COPY</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" width="49%">
                                        <table width="100%" cellpadding="0" cellspacing="0" align="center">
                                            <tr>
                                                <td align="center" valign="top" class="border">

                                                    <table width="100%" cellpadding="0" cellspacing="0" align="center" id="tblSchoolRec">
                                                        <tr>
                                                            <td align="center" valign="top" class="border-bottom">
                                                                <span class="style3">RECEIPT</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="left" valign="top" width="50%" class="border-right">
                                                                            <table width="100%" cellpadding="2" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="left" valign="middle">Student's Name -
                                                                                        <%# Eval("Studentname")%> <%# Eval("StudentLastName")%>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="middle">Father's Name -
                                                                                        <%# Eval("FatherName")%> <%# Eval("FatherLastName")%></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="middle">Class -
                                                                                        <%# Eval("MainClassName")%></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="left" valign="top" width="50%">
                                                                            <table width="100%" cellpadding="2" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="left" valign="middle">
                                                                                        <table width="100%" cellpadding="2" cellspacing="0">
                                                                                            <tr>
                                                                                                <td align="left" valign="middle">Receipt No
                                                                                                </td>
                                                                                                <td align="center" valign="middle">:
                                                                                                </td>
                                                                                                <td align="left" valign="middle">
                                                                                                    <%# Eval("SessionReceiptNo")%></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" valign="middle">Form No
                                                                                                </td>
                                                                                                <td align="center" valign="middle">:
                                                                                                </td>
                                                                                                <td align="left" valign="middle">
                                                                                                    <%# Eval("FormNo")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" valign="middle">Dated
                                                                                                </td>
                                                                                                <td align="center" valign="middle">:
                                                                                                </td>
                                                                                                <td align="left" valign="middle">
                                                                                                    <%# Eval("ReceiptDated")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" class="border-bottom">
                                                    <table width="100%" cellpadding="2" cellspacing="0" align="center">
                                                        <tr>
                                                            <td align="left" valign="middle" width="10%" class="border-left border-bottom">SNo
                                                            </td>
                                                            <td align="left" valign="middle" width="60%" colspan="2" class="border-left border-bottom">PARTICULARS
                                                            </td>
                                                            <td align="right" valign="middle" width="30%" class="border-left border-right border-bottom">AMOUNT
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">
                                                                1
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">
                                                                Prospectus Session <%#Eval("SchoolSession")%>
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">
                                                                <%#Eval("Amount")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle" colspan="2" class="border-left border-top">Total
                                                            </td>
                                                            <td align="right" valign="middle" class="border-top"></td>
                                                            <td align="right" valign="middle" class="border-left border-right border-top">
                                                                <%# Eval("Amount")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" class="border-left border-right border-bottom">
                                                    <table width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-top" colspan="5">Total Amount -:<%# Eval("Amount")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-top" colspan="5">
                                                            1. Fee once received is non-transferable and non-refundble.<br />
                                                            2. Fee must be deposited before the 8th of each month.<br />
                                                            After 8th Fine of Rs.15 per day will be charged as a late fee
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                                <tr>
                                                <td align="right" class="style2" valign="middle" style="font-size:18px;" colspan="5">
                                                <br /><br />
                                                For : CENTRAL ACADEMY
                                                </td> 
                                                </tr>
                                        </table>
                                    </td>

                                    <td width="2%">
                                        <br />
                                    </td>

                                    <td align="center" valign="top" width="49%">
                                        <table width="100%" cellpadding="0" cellspacing="0" align="center">
                                            <tr>
                                                <td align="center" valign="top" class="border">

                                                    <table width="100%" cellpadding="0" cellspacing="0" align="center" id="Table1">
                                                        <tr>
                                                            <td align="center" valign="top" class="border-bottom">
                                                                <span class="style3">RECEIPT</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="left" valign="top" width="50%" class="border-right">
                                                                            <table width="100%" cellpadding="2" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="left" valign="middle">Student's Name -
                                                                                        <%# Eval("Studentname")%> <%# Eval("StudentLastName")%>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="middle">Father's Name -
                                                                                        <%# Eval("FatherName")%> <%# Eval("FatherLastName")%></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="middle">Class -
                                                                                        <%# Eval("MainClassName")%></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="left" valign="top" width="50%">
                                                                            <table width="100%" cellpadding="2" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="left" valign="middle">
                                                                                        <table width="100%" cellpadding="2" cellspacing="0">
                                                                                            <tr>
                                                                                                <td align="left" valign="middle">Receipt No
                                                                                                </td>
                                                                                                <td align="center" valign="middle">:
                                                                                                </td>
                                                                                                <td align="left" valign="middle">
                                                                                                    <%# Eval("SessionReceiptNo")%></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" valign="middle">Form No
                                                                                                </td>
                                                                                                <td align="center" valign="middle">:
                                                                                                </td>
                                                                                                <td align="left" valign="middle">
                                                                                                    <%# Eval("FormNo")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" valign="middle">Dated
                                                                                                </td>
                                                                                                <td align="center" valign="middle">:
                                                                                                </td>
                                                                                                <td align="left" valign="middle">
                                                                                                    <%# Eval("ReceiptDated")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" class="border-bottom">
                                                    <table width="100%" cellpadding="2" cellspacing="0" align="center">
                                                        <tr>
                                                            <td align="left" valign="middle" width="10%" class="border-left border-bottom">SNo
                                                            </td>
                                                            <td align="left" valign="middle" width="60%" colspan="2" class="border-left border-bottom">PARTICULARS
                                                            </td>
                                                            <td align="right" valign="middle" width="30%" class="border-left border-right border-bottom">AMOUNT
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">
                                                                1
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">
                                                                Prospectus Session <%#Eval("SchoolSession")%>
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">
                                                                <%#Eval("Amount")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="left" colspan="2" valign="middle" class="border-left">&nbsp;
                                                            </td>
                                                            <td align="right" valign="middle" class="border-left border-right">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle" colspan="2" class="border-left border-top">Total
                                                            </td>
                                                            <td align="right" valign="middle" class="border-top"></td>
                                                            <td align="right" valign="middle" class="border-left border-right border-top">
                                                                <%# Eval("Amount")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top" class="border-left border-right border-bottom">
                                                    <table width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-top" colspan="5">Total Amount -:<%# Eval("Amount")%></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle" class="border-top" colspan="5">
                                                            1. Fee once received is non-transferable and non-refundble.<br />
                                                            2. Fee must be deposited before the 8th of each month.<br />
                                                            After 8th Fine of Rs.15 per day will be charged as a late fee
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                                <tr>
                                                <td align="right" class="style2" valign="middle" style="font-size:18px;" colspan="5">
                                                <br /><br />
                                                For : CENTRAL ACADEMY
                                                </td> 
                                                </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
