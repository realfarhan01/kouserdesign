<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ExamRoomClass.aspx.vb" Inherits="ExamRoomClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Exam Room Classes </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                      
                      
                     
                         
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Exam Room </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlexam"  runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlexam"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Class Name </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlClass" class="select-search" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue="0" ControlToValidate="ddlClass"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                       
                         
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Class Start Roll No.</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtStartRollNo" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStartRollNo"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                      
                    
                       
                            <div class="form-actions text-right">
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                                   
                <div class="panel-heading"><h6 class="panel-title">Exam Particulars</h6></div>
                <div class="table-responsive">
                 
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="ExamName" HeaderText="Exam Name"></asp:BoundField>
                         <asp:BoundField DataField="" HeaderText="Exam Date"></asp:BoundField>
                         <asp:BoundField DataField="Classname" HeaderText="Class Name"></asp:BoundField>
                         <asp:BoundField DataField="SubjectCode" HeaderText="Subject Code"></asp:BoundField>
                         <asp:BoundField DataField="ExamDate" HeaderText="Exam Date"></asp:BoundField>
                              <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                       
                   

                       
                       <%--  <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("ExamId") %>' CommandName="edit1" runat="server">Edit</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>--%>
                         </Columns>
                        </asp:GridView>
                        </div></div>
</asp:Content>

