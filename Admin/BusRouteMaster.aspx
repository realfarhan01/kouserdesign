<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="BusRouteMaster.aspx.vb" Inherits="Admin_BusRouteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
    <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Bus Route Master</h6></div>
                    <div class="panel-body">
                    
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Bus Route </label>
                            <div class="col-sm-10">
                            <asp:TextBox ID="txtBusRoute" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBusRoute"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        
                            <div class="form-actions text-right">
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>

                        <div class="panel panel-default">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                <div class="panel-heading"><h6 class="panel-title">Bus Route List</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1%>
                                </ItemTemplate>            
                            </asp:TemplateField>
                            <asp:BoundField DataField="RouteName" HeaderText="Route Name"></asp:BoundField>

                     
                         <asp:TemplateField>

                         <ItemTemplate>
                             <asp:LinkButton ID="lnkEdit" CommandArgument='<%#Eval("RouteId") %>' CommandName="edit1" runat="server"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                         </ItemTemplate>
                         <ItemTemplate>
                             <asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("RouteId") %>' CommandName="delete1" runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

