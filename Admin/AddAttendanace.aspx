<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="AddAttendanace.aspx.vb" Inherits="Admin_AddAttendanace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Attendance </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                      
                        
                         <div class="form-group">
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlClass" class="select" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                                </asp:DropDownList>
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue="0" ControlToValidate="ddlClass"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtDate" class="form-control datepicker" placeholder="Date" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDueDate"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlSession" class="select-search" runat="server">
                                    <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                    <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnShow" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                            </div>
                        </div>
                    
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                            
                <div class="panel-heading"><h6 class="panel-title">Fill Attendance</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" DataKeyNames="Studentid" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="Studentname" HeaderText="Student Name"></asp:BoundField>
                        
                       
                   

                       
                         <asp:TemplateField HeaderText="Present">
                         <ItemTemplate>
                             <asp:RadioButton ID="rdPresent"   GroupName="a" runat="server" />
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Absent">
                         <ItemTemplate>
                             <asp:RadioButton ID="rdAbsent"  GroupName="a" runat="server" />
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Leave">
                         <ItemTemplate>
                             <asp:RadioButton ID="rdLeave"  GroupName="a" runat="server" />
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div>
                        <br />
                         <div class="form-actions text-center">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" CssClass="btn btn-primary" Text="Submit" />
                       
                        </div></div>
</asp:Content>

