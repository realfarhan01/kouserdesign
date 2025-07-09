<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="MasterExam.aspx.vb" Inherits="MasterExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Exam </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                      
                      
                      
                         
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Exam Name </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtexamname" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtexamname"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Session Year </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlSession" class="select" runat="server">
                                    <asp:ListItem Value="2017-2018">2017-2018</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                     <div class="form-group">
                            <label class="col-sm-2 control-label">Start Date</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtfromdate" class="form-control datepicker" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtfromdate"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">End Date</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtTodate" class="form-control datepicker" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTodate"
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
                                   
                <div class="panel-heading"><h6 class="panel-title">Exam List</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="ExamName" HeaderText="Exam Name"></asp:BoundField>
                         <asp:BoundField DataField="SessionYear" HeaderText="Session Year"></asp:BoundField>
                           <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                          <asp:BoundField DataField="EndDate" HeaderText="End Date"></asp:BoundField>
                              <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                       
                   

                       
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("ExamId") %>' CommandName="edit1" runat="server">Edit</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div></div>
</asp:Content>

