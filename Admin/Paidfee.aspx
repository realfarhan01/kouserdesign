
<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="Paidfee.aspx.vb" Inherits="Paidfee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

<div class="panel panel-default">
                           
                <div class="panel-heading"><h6 class="panel-title">Paid Fee</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="ReceiptNo" HeaderText="Receipt No"></asp:BoundField>

                         <asp:BoundField DataField="StudentId" HeaderText="Student Id"></asp:BoundField>
                    <asp:BoundField DataField="ReceiptAmount" HeaderText="Receipt Amount"></asp:BoundField>
                   <asp:BoundField DataField="CreateDate" HeaderText="Create Date"></asp:BoundField>
                   <asp:BoundField DataField="DueDate" HeaderText="Due Date"></asp:BoundField>

                       
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("cnt") %>' OnClientClick="aspnetForm.target ='_blank';"  CommandName="edit1" runat="server">View</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         
                         </Columns>
                        </asp:GridView>
                        </div></div>
</asp:Content>

