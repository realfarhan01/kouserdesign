<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ProductReceiptListPrint.aspx.vb" Inherits="Admin_ProductReceiptListPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
<script type="text/javascript">
    function PrintDiv() {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <asp:Literal ID="litmsgReceipt" runat="server"></asp:Literal>
    <asp:Panel ID="pnlgrid" runat="server">
        <div class="panel panel-default">

            <div class="panel-heading">
                <h6 class="panel-title">Product Billing Print</h6>
            </div>
            <div class="table-responsive">
                <table class="table">
                <tfoot>
                    <tr>
                        <th>
                            <asp:TextBox ID="txtFromDate" runat="server" placeHolder="From Date" class="form-control datepicker"></asp:TextBox>
                        </th>
                        <th>
                            <asp:TextBox ID="txtToDate" runat="server" placeHolder="To Date" class="form-control datepicker"></asp:TextBox>
                        </th>
                        <th> 
                            <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:DropDownList ID="ddlSchoolSession" class="select" runat="server">
                                        <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                        <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                    </asp:DropDownList>
                        </th>
                        <th> 
                            <asp:TextBox ID="txtStudentid" runat="server" placeHolder="Student Id" class="form-control"></asp:TextBox>

                        </th>
                                
                        <th><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" /></th>
                        <th><input type="button" id="btnPrint" value="Print" onclick="PrintDiv();" class="btn btn-primary" /></th>
                    </tr>
                </tfoot></table>
                <div  id="dvContents">
                <div align="center" runat="server" id="reportheader" visible="false" >
                 <caption>
                    <asp:Literal runat="server" ID="ltrSchool"></asp:Literal>
                    <div align="left">Print Date:<%=Date.Today.ToString("dd/MM/yyyy") %> || School Session: <%# ddlSchoolSession.SelectedValue %></div>
                </caption>
                </div> 
                    <asp:GridView ID="DataDisplay" class="table table-bordered table-check" OnRowDataBound="gvEmp_RowDataBound" ShowFooter="true"   Width="100%" AutoGenerateColumns="false" runat="server">
                    <Columns>
                           <asp:TemplateField HeaderText="S.No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receipt No">
                            <ItemTemplate>
                                <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Eval("ReceiptNo")%>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTot" runat="server" Font-Bold="true" Text="TOTAL"  />
                                
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ReceiptDate" HeaderText="Date"></asp:BoundField>
                        <asp:BoundField DataField="StudentId" HeaderText="Adm No."></asp:BoundField>
                        <asp:BoundField DataField="StudentName" HeaderText="Student Name"></asp:BoundField>
                        <asp:BoundField DataField="MainClassName" HeaderText="Class"></asp:BoundField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblamount" runat="server" Text='<%# Eval("ReceiptAmount")%>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Font-Bold="true"  />
                            </FooterTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>


