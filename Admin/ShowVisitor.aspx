<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ShowVisitor.aspx.vb" Inherits="Admin_ShowStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
 <div class="panel panel-default">
                <div class="panel-heading"><h6 class="panel-title">Visitor List</h6></div>
                <div class="table-responsive">
                <table class="table">
                  <tfoot>
                            <tr>
                                <th><asp:TextBox ID="txtStudentid" runat="server" placeHolder="Student Id" class="form-control"></asp:TextBox></th>
                            
                               
                                <th><asp:TextBox ID="txtempid" runat="server" placeHolder="Employee Id" class="form-control"></asp:TextBox></th>
                                    <th><asp:TextBox ID="txtdate" runat="server" placeHolder="Visit Date" class="form-control datepicker"></asp:TextBox></th>
                                
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
                                                                 <asp:BoundField DataField="Studentid" HeaderText="Student Id"></asp:BoundField>
                         <asp:BoundField DataField="Employeeid" HeaderText="Employee Id"></asp:BoundField>
                         <asp:BoundField DataField="VisitDate" HeaderText="Visit Date"></asp:BoundField>
                       
                       
                         
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

