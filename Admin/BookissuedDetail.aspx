<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="BookissuedDetail.aspx.vb" Inherits="BookissuedDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
 <div class="panel panel-default">
                <div class="panel-heading"><h6 class="panel-title">Search Issued Book</h6></div>
                <div class="table-responsive">
                <table class="table">
                  <tfoot>
                            <tr>
                               
                                <th><asp:TextBox ID="txtbookid" runat="server" placeHolder="Book Id" class="form-control"></asp:TextBox></th>
                                <th> <asp:TextBox ID="txtStudentid" runat="server" placeHolder="Student Id" class="form-control"></asp:TextBox></th>
                             
                                
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
                                                                
                        
                         <asp:BoundField DataField="BookId" HeaderText="Book Id"></asp:BoundField>
                          <asp:BoundField DataField="BookName" HeaderText="Book Name"></asp:BoundField>
                         <asp:BoundField DataField="StudentId" HeaderText="Student Id"></asp:BoundField>
                        <asp:BoundField DataField="IssueDate" HeaderText="IssueDate"></asp:BoundField>
                        <asp:BoundField DataField="IssueDays" HeaderText="Issue Days"></asp:BoundField>
                         <asp:TemplateField>
                         <ItemTemplate>
                         <a href='ReturnBook.aspx?Bid=<%# eval("IssueId") %>'>Edit</a>
                         </ItemTemplate>
                         </asp:TemplateField>
                       
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

