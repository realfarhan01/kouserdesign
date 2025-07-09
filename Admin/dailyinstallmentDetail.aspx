<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="dailyinstallmentDetail.aspx.vb" Inherits="Admin_dailyinstallmentDetail" %>

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
        #ctl00_C1_DlDailyCollection {
            margin-left: 10px !important;
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
            <h6 class="panel-title">Daily Collection Details</h6>
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
                            <asp:TextBox ID="txtFromDate" runat="server" placeHolder="From Date" class="form-control datepicker"></asp:TextBox>

                        </th>
                        <th>
                            <asp:TextBox ID="txtToDate" runat="server" placeHolder="To Date" class="form-control datepicker"></asp:TextBox>

                        </th>
                        <th>
                            <asp:DropDownList ID="ddlType" class="select-search" runat="server">
                                <asp:ListItem Value="0">--All--</asp:ListItem>
                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                <asp:ListItem Value="2">Cheque</asp:ListItem>
                                <asp:ListItem Value="3">Cash+Cheque</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <%--<th>
                            <asp:DropDownList ID="ddlSession" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="1">2017-2018</asp:ListItem>
                            </asp:DropDownList>
                        </th>--%>
                        <th>
                            <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" /></th>
                        <th>
                            <input type="button" id="btnPrint" value="Print" onclick="PrintDiv();" class="btn btn-primary" /></th>
                        <%--<th>
                            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-primary" Enabled="false" OnClick="btnExport_Click" /></th>--%>
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
                        <tr style="line-height: 26px; border-bottom: 1px solid #ddd; width:100%">
                            <td style="width: 5%">DATED</td>
                            <td style="width: 5%; white-space:nowrap">ADM No</td>
                            <td style="width: 10%">NAME</td>
                            <td style="width: 5%">CLASS</td>
                            <td style="width: 4%; white-space:nowrap">INST 1</td>
                            <td style="width: 4%; white-space:nowrap">INST 2</td>
                            <td style="width: 4%; white-space:nowrap">INST 3</td>
                            <td style="width: 4%; white-space:nowrap">INST 4</td>
                            <td style="width: 4%; white-space:nowrap">INST 5</td>
                            <td style="width: 5%; white-space:nowrap">INST 6</td>
                            <td style="width: 5%; white-space:nowrap">INST 7</td>
                            <td style="width: 5%">ADMF</td>
                            <td style="width: 5%">CONV</td>
                            <td style="width: 5%">SPORTS</td>
                            <td style="width: 5%">LATE</td>
                            <td style="width: 5%">OTHER</td>
                            <td style="width: 5%">DISC</td>
                            <td style="width: 5%">TOTAL</td>
                            <td style="width: 5%">CH NO</td>
                            <td style="width: 5%">PERIOD</td>
                        </tr>
                        <asp:DataList runat="server" ID="DlDailyCollection" CssClass="nav-justified" Width="100%" OnItemDataBound="DlDailyCollection_ItemDataBound">
                            <ItemTemplate>
                                <div>
                                    <asp:HiddenField runat="server" ID="hdfDate" Value='<%# Eval("Dated")%>' />
                                </div>
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                <tr style="line-height: 30px; width:100%; border-bottom: 1px dashed black; border-top:1px dashed black;  font-weight:bold;">
                                    <td style="width: 5%"><%# Eval("Dated")%></td>
                                    <td style="width: 5%">&nbsp;</td>
                                    <td style="width: 10%">&nbsp;</td>
                                    <td style="width: 5%">&nbsp;</td>
                                    <td style="width: 4%;"><%# Eval("Installment1")%></td>
                                    <td style="width: 4%"><%# Eval("Installment2")%></td>
                                    <td style="width: 4%"><%# Eval("Installment3")%></td>
                                    <td style="width: 4%"><%# Eval("Installment4")%></td>
                                    <td style="width: 4%"><%# Eval("Installment5")%></td>
                                    <td style="width: 5%"><%# Eval("Installment6")%></td>
                                    <td style="width: 5%"><%# Eval("Installment7")%></td>
                                    <td style="width: 5%"><%# Eval("AdmissionFee")%></td>
                                    <td style="width: 5%"><%# Eval("ConveyanceFee")%></td>
                                    <td style="width: 5%"><%# Eval("SportsFee")%></td>
                                    <td style="width: 5%"><%# Eval("LateFee")%></td>
                                    <td style="width: 5%"><%# Eval("OtherFee")%></td>
                                    <td style="width: 5%"><%# Eval("Discount")%></td>
                                    <td style="width: 5%"><%# Eval("TotalAmount")%></td>
                                    <td style="width: 5%">&nbsp;</td>
                                    <td style="width: 5%">&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:DataList>
                    </table>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

