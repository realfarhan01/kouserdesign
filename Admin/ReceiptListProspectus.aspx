
<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ReceiptListProspectus.aspx.vb" Inherits="Admin_ReceiptListProspectus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         $(document).ready(function () {
             $("#DataDisplay").prepend($("<thead></thead>").append($("#DataDisplay").find("tr:first"))).dataTable();
             $("#DataDisplay").dataTable();
         });
    </script>
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
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

<div class="panel panel-default">
                           
                <div class="panel-heading"><h6 class="panel-title">Prospectus Sale Receipt List</h6>
                    <div class="form-actions text-right">
                        <input type="button" id="btnExport" value="Print" onclick="PrintDiv();" class="btn btn-primary" />
                    </div>
                </div>
                    <div class="panel-body">
                <div class="form-group">
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlSession" class="select-search" runat="server">
                                    <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                    <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlClassFrom" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--From Class--</asp:ListItem>
                                </asp:DropDownList>
                             
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlClassTo" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--To Class--</asp:ListItem>
                                </asp:DropDownList>
                             
                            </div>
                            <div class="col col-lg-2">
                                <asp:TextBox ID="txtFormNo" class="form-control" placeHolder="Form No." runat="server"></asp:TextBox>
                            </div>
                            </div>
                                <br />
                                <br />
                <div class="form-group">
                             <div class="col col-lg-2">
                                <asp:TextBox ID="txtFromDate" runat="server" placeHolder="From Date" class="form-control datepicker"></asp:TextBox>
                            </div>
                           <div class="col col-lg-2">
                                <asp:TextBox ID="txtToDate" runat="server" placeHolder="To Date" class="form-control datepicker"></asp:TextBox>
                            </div>
                            <div class="col col-lg-2">
                                <asp:TextBox ID="txtReceiptNo" class="form-control" placeHolder="Receipt No." runat="server"></asp:TextBox>
                            </div>
                            <div class="col col-lg-2">
                                <asp:TextBox ID="txtStudentName" class="form-control" placeHolder="Student Name" runat="server"></asp:TextBox>
                            </div>
                            <div class="col col-lg-2">
                                <asp:TextBox ID="txtFathername" class="form-control" placeHolder="Father Name" runat="server"></asp:TextBox>
                            </div>
                            
                            <div class="col-sm-1">
                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" />
                            </div> 
                                
                                </div>
                                <br />
                                <br />
                                <br />
                        <div class="table-responsive"  id="dvContents">
                        <div align="center" runat="server" id="reportheader" visible="false" >
                            <caption>
                            <asp:Literal runat="server" ID="ltrSchool"></asp:Literal>
                            <div align="left">Print Date:<%=Date.Today.ToString("dd/MM/yyyy") %></div>
                        </caption>
                        </div> 
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                        <asp:TemplateField HeaderText="S.No" >
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                                                                   
                        </asp:TemplateField>
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("cnt") %>' OnClientClick="aspnetForm.target ='_blank';"  CommandName="view1" runat="server">View</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("cnt")%>' OnClientClick="aspnetForm.target ='_blank';"  CommandName="edit1" runat="server"><%# If(Eval("RegistrationId").ToString() = "", "Edit", "")%></asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton3" CommandArgument='<%# eval("cnt") %>' OnClientClick="aspnetForm.target ='_blank';"  CommandName="delete1" runat="server"><%# If(Eval("RegistrationId").ToString() = "", "Delete", "")%></asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                        <asp:BoundField DataField="SchoolSession" HeaderText="Session"></asp:BoundField>
                        <asp:BoundField DataField="SessionReceiptNo" HeaderText="Receipt No"></asp:BoundField>
                        <asp:BoundField DataField="FormNo" HeaderText="Form No"></asp:BoundField>
                        <asp:BoundField DataField="StudentName" HeaderText="Student Name"></asp:BoundField>
                        <asp:BoundField DataField="MainClassName" HeaderText="Class"></asp:BoundField>
                        <asp:BoundField DataField="FatherName" HeaderText="Father Name"></asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile"></asp:BoundField>
                        <asp:BoundField DataField="Remark" HeaderText="Remark"></asp:BoundField>
                        <asp:BoundField DataField="ReceiptDated" HeaderText="Sale Date"></asp:BoundField>
                        <asp:BoundField DataField="RegistrationId" HeaderText="Adm No"></asp:BoundField>
                         
                         </Columns>
                        </asp:GridView>
                        </div>
                        </div> </div>
</asp:Content>

