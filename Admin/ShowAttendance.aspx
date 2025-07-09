<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ShowAttendance.aspx.vb" Inherits="ShowAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

 <div class="panel panel-default">
                <div class="panel-heading"><h6 class="panel-title">Search Attendance</h6></div>
                <div class="table-responsive">
                <table class="table">
                  <tfoot>
                            <tr>
                                
                                <th><asp:TextBox ID="txtParentId" runat="server" placeHolder="Unique Id" class="form-control"></asp:TextBox></th>
                                <th><asp:TextBox ID="txtsave" runat="server" placeHolder="Saved By" class="form-control"></asp:TextBox></th>
                                <th><asp:TextBox ID="txtdate" runat="server" placeHolder="Date (dd/mm/yyyy)" class="form-control"></asp:TextBox></th>
                                <th><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" /></th>
                               
                            </tr>
                        </tfoot></table>
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server" EmptyDataText="No Record Found">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                         <asp:BoundField DataField="uniqueid" HeaderText="Unique Id"></asp:BoundField>
                        <asp:BoundField DataField="OnDate" HeaderText="Date "></asp:BoundField>
                        <asp:BoundField DataField="Intime" HeaderText="Intime"></asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>

                      
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

