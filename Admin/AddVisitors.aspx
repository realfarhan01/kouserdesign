<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="AddVisitors.aspx.vb" Inherits="AddVisitors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Visitors </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                     
                       
                      
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Visit Date</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtVisitdate" class="form-control datepicker" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVisitdate"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Student Id</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtStudentId" class="form-control" runat="server"></asp:TextBox>
                                 
                            </div>
                        </div>
                     <div class="form-group">
                            <label class="col-sm-2 control-label">Employee Id</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtEmployeeid" class="form-control" runat="server"></asp:TextBox>
                                
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Visitor Name </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtVisitorName" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtVisitorName"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Relation</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtrelation" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrelation"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Number Of Persons</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtNOP" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNOP"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Visit Details</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtVisitDetails" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtVisitDetails"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>

                       
                            <div class="form-actions text-right">
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                            
                <div class="panel-heading"><h6 class="panel-title">Visitors List</h6></div>
                <div class="table-responsive">
         
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="VisitDate" HeaderText="Visit Date"></asp:BoundField>
                         <asp:BoundField DataField="StudentId" HeaderText="Student Id"></asp:BoundField>
                        <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id"></asp:BoundField>
                   <asp:BoundField DataField="VisitorName" HeaderText="Visitor Name"></asp:BoundField>

                       
                         
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

