<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="UploadSchoolDoc.aspx.vb" Inherits="UploadSchoolDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<div class="form-horizontal" >
  <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Upload School Document </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                  
                      
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Document Name</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtDocumentName" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDocumentName"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Document Detail</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtDocumentDetail" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDocumentDetail"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Upload </label>
                            <div class="col-sm-10">
                            <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                             
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"  ControlToValidate="FileUpload1"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload1"  ErrorMessage="Upload only pdf,doc and txt files" ValidationExpression="^.*\.(pdf|PDF|doc|DOC|docx|DOCX|txt|TXT|txtx|TXTX)$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                         
                 
                            <div class="form-actions text-right">

                           
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div>
</div>
  <div class="panel panel-default">
                            
                <div class="panel-heading"><h6 class="panel-title">Document List</h6></div>
                <div class="table-responsive">
              
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="DocumentName" HeaderText="Document Name"></asp:BoundField>
                         <asp:BoundField DataField="DownloadURL" HeaderText="Download URL"></asp:BoundField>
                        
                       
                         </Columns>
                        </asp:GridView>
                        </div></div>
</asp:Content>

