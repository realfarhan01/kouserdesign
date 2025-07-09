
<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="FeeSubmit.aspx.vb" Inherits="FeeSubmit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
 <asp:Panel ID="pnlgrid"  runat="server">
<div class="panel panel-default">
                           
                <div class="panel-heading"><h6 class="panel-title">Unpaid Receipt List</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="ReceiptNo" HeaderText="Receipt No"></asp:BoundField>

                         <asp:BoundField DataField="StudentId" HeaderText="Student Id"></asp:BoundField>
                    <asp:BoundField DataField="ReceiptAmount" HeaderText="Receipt Amount"></asp:BoundField>
                   <asp:BoundField DataField="CreateDate" HeaderText="Create Date"></asp:BoundField>
                   <asp:BoundField DataField="DueDate" HeaderText="Due Date"></asp:BoundField>

                       
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("cnt") %>'  OnClientClick="aspnetForm.target ='_blank';" CommandName="edit1" runat="server">View</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                          <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton2" CommandArgument='<%# eval("cnt") %>' CommandName="paid" runat="server">Pay Receipt</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                           <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton3" CommandArgument='<%# eval("cnt") %>' CommandName="delete1" runat="server">Delete</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>

                         </Columns>
                        </asp:GridView>
                        </div></div>
                        </asp:Panel>
    <asp:Panel ID="pnlsubmit" Visible="false" runat="server">


                           <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Make a New Receipt</h6></div>
                    <div class="panel-body">
                    <asp:HiddenField ID="hfId" runat="server" />
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                   
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                       <%-- <div class="form-group">
                            <label class="col-sm-2 control-label">Student Id </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtStudentId" class="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtStudentId"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div></div>--%>

                         <div class="form-group">
                            <label class="col-sm-2 control-label">Receipt No. </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtReceiptno" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReceiptno"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">PaidDate</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtDueDate" class="form-control datepicker" runat="server"></asp:TextBox>[DD/MM/YYYY]
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDueDate"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Remark </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtremark" class="form-control" runat="server"></asp:TextBox>
                              
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Late Fee</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtlatefees" class="form-control"   runat="server">0</asp:TextBox>
                            </div>
                        </div>
                          <div class="form-group">
                            <label class="col-sm-2 control-label">Payment Mode</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlpmode" class="form-control" runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                 <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                  <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                   <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ControlToValidate="ddlpmode"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                           <div class="form-actions text-right">
                           <asp:Button ID="btnback" runat="server"  class="btn btn-primary" Text="Back" />
                       
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div></div></div>

                            </asp:Panel>
</asp:Content>

