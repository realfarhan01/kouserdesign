<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="CompanyMaster.aspx.vb" Inherits="CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<!-- Form components -->
            <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Company Details</h6></div>
                    <div class="panel-body">
                    
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfid" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                        
                      
                             <div class="form-group">
                            <label class="col-sm-2 control-label">Company Name</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtname" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtname"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div></div>

                       

                        
                       
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Address </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtaddress" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtaddress"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                          <div class="form-group">
                            <label class="col-sm-2 control-label">State </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" class="select-search" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             

                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlState"
                ErrorMessage="*" ValidationGroup="UserRegistration" InitialValue="">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">City </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlCity" AppendDataBoundItems="true" class="select-search" runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCity"
                ErrorMessage="*" ValidationGroup="UserRegistration" InitialValue="">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        
                        
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Contact No. </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtContactno" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtContactno"
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
                            <label class="col-sm-2 control-label">Domain Name</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtdomain" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdomain"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div></div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Fax No.</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtfax" class="form-control" runat="server"></asp:TextBox>
                               
                            </div></div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Prefix</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtprefix" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtprefix"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div></div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">FBPage</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtfbpage" class="form-control" runat="server"></asp:TextBox>
                                 
                            </div></div>

                       
                        
                            <div class="form-actions text-right">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div></div></div>

            <!-- /form components -->
</asp:Content>

