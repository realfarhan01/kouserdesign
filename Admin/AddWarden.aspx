<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="AddWarden.aspx.vb" Inherits="AddWarden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
  <!-- Form components -->
            <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Warden</h6> </div>
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
                                <asp:DropDownList ID="ddlhostel" class="form-control" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="ReqIntro" InitialValue="0" runat="server" ControlToValidate="ddlhostel"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Hostel Session </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlhostelsession" class="form-control" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server" ControlToValidate="ddlhostelsession"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>

                         <div class="form-group">
                            <label class="col-sm-2 control-label">Employee Id </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtEmployeeId" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmployeeId"
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
                            <label class="col-sm-2 control-label">Allotment Date</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtAllotmentDate" class="form-control datepicker" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAllotmentDate"
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
                <div class="panel-heading"><h6 class="panel-title">Hostel Warden</h6></div>
                <div class="table-responsive">
               
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="HostelName" HeaderText="Hostel Name"></asp:BoundField>
                         <asp:BoundField DataField="employeeid" HeaderText="Warden"></asp:BoundField>
                        <asp:BoundField DataField="employeeName" HeaderText="EmployeeName "></asp:BoundField>
                   <%--     <asp:BoundField DataField="TotalBed" HeaderText="TotalBed "></asp:BoundField>--%>
                        <asp:BoundField DataField="Status" HeaderText="DeActivated"></asp:BoundField>

                       
                       
                         </Columns>
                        </asp:GridView>
                        </div></div>

            <!-- /form components -->

</asp:Content>

