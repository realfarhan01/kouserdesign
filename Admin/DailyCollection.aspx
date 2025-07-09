<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="DailyCollection.aspx.vb" Inherits="Admin_DailyCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function PrintDiv() {
            //alert("Chau Mau");
            var divContents = document.getElementById("dvContents").innerHTML;
            var printWindow = window.open();
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
    
    <style>
        td, th {
            padding-left: 12px !important;
        }

        .border {
            border: 1px solid gray !important;
        }
        .select2-container {
            width: 100% !important;
        }
        .schsession
        {
            height: 30px;
            width: 200px;
            margin-top: 6px;
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">Daily Collection</h6>
                    <div class="form-actions text-right" style="vertical-align:middle">
                                <asp:DropDownList ID="ddlSession" CssClass="schsession" runat="server">
                                    <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                    <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                </asp:DropDownList>
                    </div>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tfoot>
                    <tr>

                        <th>
                            <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--From Class--</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:DropDownList ID="ddlClassTo" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--To Class--</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:TextBox ID="txtFromDate" runat="server" placeHolder="From Date" Width="100px"  class="form-control datepicker"></asp:TextBox></th>
                        <th>
                            <asp:TextBox ID="txtToDate" runat="server" placeHolder="To Date" Width="100px"  class="form-control datepicker"></asp:TextBox></th>
                        <%--<th>
                            <asp:DropDownList ID="ddlSession" class="select" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="1">2017-2018</asp:ListItem>
                            </asp:DropDownList>
                        </th>--%>
                        <th>
                            <asp:DropDownList ID="ddlType" class="select" runat="server">
                                <asp:ListItem Value="0">Cheque+Cash</asp:ListItem>
                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                <asp:ListItem Value="2">Cheque</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" /></th>
                        <th>
                            <input type="button" id="btnPrint" value="Print" onclick="PrintDiv();" class="btn btn-primary" /></th>
                    </tr>
                </tfoot>
            </table>


            <div id="dvContents">
                <div id="employeelistDiv" runat="server">

                    <table class="nav-justified table table-bordered table-check" border="1" width="100%" cellpadding="0" cellspacing="0">
                        <caption>
                            <asp:Literal runat="server" ID="ltrSchool"></asp:Literal>
                            <div align="left">Print Date:<%=Date.Today.ToString("dd/MM/yyyy") %><asp:Literal ID="littotal" runat="server"></asp:Literal></div>
                        </caption>

                        <tr style="line-height: 26px; width:100%; border-bottom: 1px solid #ddd;">
                            <td style="width: 25%" align="left" >RECEIPT DATE</td>
                            <td style="width: 25%">CASH AMOUNT</td>
                            <td style="width: 25%">CHEQUE AMOUNT</td>
                            <td style="width: 25%">TOTAL COLLECTION</td>
                        </tr>
                        <asp:DataList runat="server" ID="DlDailyCollection" Width="100%">
                            <ItemTemplate>
                                <tr style="line-height: 26px; width:100%;">
                                    <td style="width: 25%" align="center" ><%# Eval("Dated")%></td>
                                    <td style="width: 25%"><%# Eval("CashAmount")%></td>
                                    <td style="width: 25%"><%# Eval("ChequeAmount")%></td>
                                    <td style="width: 25%"><%# Eval("TotalAmount")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:DataList>
                    </table>
                </div>
            </div>


        </div>
    </div>
</asp:Content>

