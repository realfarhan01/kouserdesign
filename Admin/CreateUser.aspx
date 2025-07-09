<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateUser.aspx.vb" MasterPageFile="~/Admin/AdminMaster.master"
    Inherits="CreateUser" Title=":: Create Operator ::" %>

<asp:Content ContentPlaceHolderID="C1" ID="content1" runat="server">
    
   <div class="form-horizontal" >

       

                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Create User </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                     
                         
                       
                        <div class="form-group">
                        <asp:ValidationSummary runat="server" ID="errorsummary" CssClass="validerror"
                                    ValidationGroup="CreateUser" />
                    </div>
                     <div class="form-group">
                            <label class="col-sm-2 control-label">
                           Name <span style="color: #ff0000">*</span> :</label>
                       <div class="col-sm-10">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="ReqName" runat="server" ErrorMessage="Name field can't be empty" 
                                                ControlToValidate="txtName" Display="None" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                     <div class="form-group">
                            <label class="col-sm-2 control-label">
                            Operator Id <span style="color: #ff0000">*</span> :</label>
                     <div class="col-sm-10">
                          <asp:TextBox ID="txtUserid" onblur="return IsOperatorExists(this);" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="ReqUserId" runat="server" 
                                                ErrorMessage="operator id field can't be empty" ControlToValidate="txtUserid" 
                                                Display="None" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                      <div class="form-group">
                            <label class="col-sm-2 control-label">
                            Email <span style="color: #ff0000">*</span> :</label>
                     <div class="col-sm-10">
                          <asp:TextBox ID="txtemail" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="Reqemail" runat="server" ErrorMessage="email field can't be empty" 
                                                ControlToValidate="txtemail" Display="None" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                      <div class="form-group">
                            <label class="col-sm-2 control-label">
                            Mobile<span style="color: #ff0000">*</span> :</label>
                       <div class="col-sm-10">
                      <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                      <div class="form-group">
                            <label class="col-sm-2 control-label">
                          Address <span style="color: #ff0000">*</span> :</label>
                      <div class="col-sm-10">
                            <asp:TextBox ID="txtAddress" runat="server" Height="70px" MaxLength="200" TextMode="MultiLine"
                                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                      <div class="form-group">
                            <label class="col-sm-2 control-label">
                           Password <span style="color: #ff0000">*</span> :</label>
                       <div class="col-sm-10">
                            <asp:TextBox ID="txtPwd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="ReqPassword" runat="server" ErrorMessage="password field can't be empty" 
                                                ControlToValidate="txtPwd" Display="None" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                     <div class="form-group">
                            <label class="col-sm-2 control-label">
                             Re-Enter Password <span style="color: #ff0000">*</span> :</label>
                       <div class="col-sm-10">
                             <asp:TextBox ID="txtRePwd"  CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator
                                                ID="ReqRPassword" runat="server" 
                                                ErrorMessage="confirm password field can't be empty" 
                                                ControlToValidate="txtRePwd" Display="None" ValidationGroup="CreateUser"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                     <div class="form-actions">
                       <asp:Button ID="btnsave" runat="server" Text="save" Width="87px" 
                                                CssClass="btn btn-success" ValidationGroup="CreateUser" />

                                               
                    </div>
                    </div></div></div>
</asp:Content>
