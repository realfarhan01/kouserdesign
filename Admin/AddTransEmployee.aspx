<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="AddTransEmployee.aspx.vb" Inherits="AddTransEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Transport Employee </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                     
                        
                       
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Vehicle Id </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlVehicleid" CssClass="form-control" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0"  ControlToValidate="ddlVehicleid"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                     <div class="form-group">
                            <label class="col-sm-2 control-label">Employee Id</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtEmployeeid" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmployeeid"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">DL No</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtdlno" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtdlno"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">DL Expiry Date</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtDLExpiryDate" class="form-control datepicker" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDLExpiryDate"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">BloodGroup</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtBloodGroup" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBloodGroup"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Status </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlactive" class="select-full" runat="server">
                                <asp:ListItem Value="0">Active</asp:ListItem>
                                <asp:ListItem Value="1">Deactive</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlactive"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                            <div class="form-actions text-right">
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                            
                <div class="panel-heading"><h6 class="panel-title">Transport Employee List</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="VehicleId" HeaderText="Vehicle Id"></asp:BoundField>
                         <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id"></asp:BoundField>
                        <asp:BoundField DataField="Dlno" HeaderText="DL No"></asp:BoundField>
                   

                       
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("EmployeeId") %>' CommandName="edit1" runat="server">Edit</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

