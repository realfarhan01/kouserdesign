<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="MasterHostel.aspx.vb" Inherits="MasterHostel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
  <!-- Form components -->
            <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Hostel</h6> </div>
                    <div class="panel-body">
                        <asp:HiddenField ID="hfId" runat="server" />
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Hostel Name </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtHostelName" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="ReqIntro" runat="server" ControlToValidate="txtHostelName"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Hostel Address </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtHostelAddress" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHostelAddress"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <%--<div class="form-group">
                            <label class="col-sm-2 control-label">Gender </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlGender" class="select-full" runat="server">
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGender"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>--%>
                         
                        
                         
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Contact No1. </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtContactno1" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtContactno1"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Contact No2. </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtContactno2" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContactno2"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Email</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtEmailid" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmailid"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailid"
                ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Hostel info </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txthostelinfo" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txthostelinfo"
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
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlactive"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                            <div class="form-actions text-right">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                <div class="panel-heading"><h6 class="panel-title">Hostel List</h6></div>
                <div class="table-responsive">
               
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="HostelName" HeaderText="Hostel Name"></asp:BoundField>
                         <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                        <asp:BoundField DataField="ContactNo1" HeaderText="ContactNo "></asp:BoundField>
                        
                        <asp:BoundField DataField="Status" HeaderText="DeActivated"></asp:BoundField>

                       
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("HostelId") %>' CommandName="edit1" runat="server">Edit</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div></div>

            <!-- /form components -->

</asp:Content>

