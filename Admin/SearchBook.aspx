<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="SearchBook.aspx.vb" Inherits="SearchBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
 <div class="panel panel-default">
                <div class="panel-heading"><h6 class="panel-title">Search Book</h6></div>
                <div class="table-responsive">
                <table class="table">
                  <tfoot>
                            <tr>
                                <th><asp:DropDownList ID="ddlcat" class="select-search" AppendDataBoundItems="true"  runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList></th>
                                <th><asp:TextBox ID="txtbookid" runat="server" placeHolder="Book Id" class="form-control"></asp:TextBox></th>
                                <th> <asp:TextBox ID="txtBookTag" runat="server" placeHolder="Book Tag" class="form-control"></asp:TextBox></th>
                             
                                
                                <th><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" /></th>
                               
                            </tr>
                        </tfoot></table>
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                         <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="Categoryname" HeaderText="Category Name"></asp:BoundField>
                         <asp:BoundField DataField="BookName" HeaderText="Book Name"></asp:BoundField>
                         <asp:BoundField DataField="Auther" HeaderText="Auther"></asp:BoundField>
                        <asp:BoundField DataField="TotalQty" HeaderText="Total Qty"></asp:BoundField>
                        <asp:BoundField DataField="AvailableQty" HeaderText="Available Qty"></asp:BoundField>
                       
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

