<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ShowEmployee.aspx.vb" Inherits="ShowEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

 <div class="panel panel-default">
                <div class="panel-heading"><h6 class="panel-title">Employee List</h6></div>
                <div class="table-responsive">
                <table class="table">
                  <tfoot>
                            <tr>
                                
                                <th><asp:TextBox ID="txtemployeeid" runat="server" placeHolder="Employee Id" class="form-control"></asp:TextBox></th>
                                <th><asp:TextBox ID="txtemployeename" runat="server" placeHolder="Employee Name" class="form-control"></asp:TextBox></th>
                                 <th><asp:DropDownList ID="ddldesignation" AppendDataBoundItems="true" class="select-search" runat="server">
                                <asp:ListItem Value="">--Select Designation--</asp:ListItem>
                                </asp:DropDownList></th>
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
                                                                 <asp:BoundField DataField="Employeename" HeaderText="Employee Name"></asp:BoundField>
                         <asp:BoundField DataField="fathername" HeaderText="Father Name"></asp:BoundField>
                        <asp:BoundField DataField="Mothername" HeaderText="Mother Name "></asp:BoundField>
                        <asp:BoundField DataField="LoginID" HeaderText="Login Id"></asp:BoundField>
                        <asp:BoundField DataField="Password" HeaderText="Password"></asp:BoundField>

                        <asp:BoundField DataField="City" HeaderText="City"></asp:BoundField>

                        <asp:BoundField DataField="Emailid" HeaderText="Email Id"></asp:BoundField>
                         <asp:TemplateField>
                         <ItemTemplate>
                         <a href='AddEmplyee.aspx?Eid=<%# eval("Employeeid") %>'>Edit</a>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

