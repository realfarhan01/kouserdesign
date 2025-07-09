<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="EmpAttendanace.aspx.vb" Inherits="EmpAttendanace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Employee Attendance </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                      
                        
                        <%-- <div class="form-group">
                            <label class="col-sm-2 control-label">Class Name </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" AutoPostBack="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValu="0" ControlToValidate="ddlClass"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>--%>
                    
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                            
                <div class="panel-heading"><h6 class="panel-title">Fill Attendance</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" DataKeyNames="Employeeid" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="Employeename" HeaderText="Employee Name"></asp:BoundField>
                        
                       
                   

                       
                         <asp:TemplateField HeaderText="Present">
                         <ItemTemplate>
                             <asp:RadioButton ID="rdPresent"   GroupName="a"  runat="server" />
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
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div></div>
</asp:Content>

