<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="UploadEdoc.aspx.vb" Inherits="Admin_Uploadstudata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<div class="form-horizontal" >
  <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Upload Student Data </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                  
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Class Name </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlClass"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <%--<div class="form-group">
                            <label class="col-sm-2 control-label">Subject</label>
                    <div class="col-sm-10">
                                <asp:DropDownList ID="ddlsubject" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Document Title</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtDocumentName" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDocumentName"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--<div class="form-group">
                            <label class="col-sm-2 control-label">Document Detail</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtDocumentDetail" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDocumentDetail"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>--%>
                          <div class="form-group">
                            <label class="col-sm-2 control-label">Type </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlactive" class="select-full" runat="server">
                                 <asp:ListItem Value="1">Student Assignment</asp:ListItem>
                                <asp:ListItem Value="0">Parent Circular</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Select File </label>
                            <div class="col-sm-10">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                             
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"  ControlToValidate="FileUpload1"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload1"  ErrorMessage="Upload only png, jpg, pdf, doc and txt files" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|doc|pdf|PDF|doc|DOC|docx|DOCX|txt|TXT|txtx|TXTX)$"></asp:RegularExpressionValidator>
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
                            <asp:TemplateField>
                            <ItemTemplate>
                                <a href='../upload/document/<%#Eval("DownloadURL") %>' target="_blank">View</a> 
                            </ItemTemplate>                         
                         </asp:TemplateField>
                         <asp:BoundField DataField="DocumentName" HeaderText="Document Title"></asp:BoundField>
                        <asp:BoundField DataField="MainClassName" HeaderText="Class"></asp:BoundField>
                       
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("sno") %>' CommandName="delete1" runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div></div>
</asp:Content>

