<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="Uploadstudata.aspx.vb" Inherits="Admin_Uploadstudata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<div class="form-horizontal" >
  <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Upload E-Document </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                      
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Upload </label>
                            <div class="col-sm-10">
                            <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                             
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"  ControlToValidate="FileUpload1"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload1"  ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.zip|.rar)$"  ErrorMessage="Please Upload Only .zip and .rar Files"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                         
                 
                            <div class="form-actions text-right">

                            <asp:HyperLink ID="HyperLink1" runat="server"  class="btn btn-primary"
                       NavigateUrl="~/sampledata.xlsx">Sample File</asp:HyperLink>
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div>
</div>
</asp:Content>

