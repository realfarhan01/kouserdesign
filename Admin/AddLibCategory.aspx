<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="AddLibCategory.aspx.vb" Inherits="Admin_AddLibCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Library Category Master </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                      
                        
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Category Name </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtname" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtname"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                     <div class="form-group">
                            <label class="col-sm-2 control-label">Rack No</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlrack" class="select-search" runat="server">
                             
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                 <asp:ListItem Value="1">1</asp:ListItem>

                             <asp:ListItem Value="2">2</asp:ListItem>
                             <asp:ListItem Value="3">3</asp:ListItem>
                             <asp:ListItem Value="4">4</asp:ListItem>
                             <asp:ListItem Value="5">5</asp:ListItem>
                             <asp:ListItem Value="6">6</asp:ListItem>
                             <asp:ListItem Value="7">7</asp:ListItem>
                             <asp:ListItem Value="8">8</asp:ListItem>
                             <asp:ListItem Value="9">9</asp:ListItem>
                             <asp:ListItem Value="10">10</asp:ListItem>
                             <asp:ListItem Value="11">11</asp:ListItem>
                             <asp:ListItem Value="12">12</asp:ListItem>
                                  <asp:ListItem Value="13">13</asp:ListItem>
                                  <asp:ListItem Value="14">14</asp:ListItem>
                                   <asp:ListItem Value="15">15</asp:ListItem>
                                  <asp:ListItem Value="16">16</asp:ListItem>
                                   <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                     <asp:ListItem Value="19">19</asp:ListItem>
                                      <asp:ListItem Value="20">20</asp:ListItem>

                                </asp:DropDownList>
                             
                                  
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Narration</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtnarration" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnarration"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                            <div class="form-actions text-right">
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                            
                <div class="panel-heading"><h6 class="panel-title">Category List</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="CategoryName" HeaderText="Category Name"></asp:BoundField>
                         <asp:BoundField DataField="Rackno" HeaderText="Rack No"></asp:BoundField>
                       
                   

                       
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("CategoryId") %>' CommandName="edit1" runat="server">Edit</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div></div>
</asp:Content>

