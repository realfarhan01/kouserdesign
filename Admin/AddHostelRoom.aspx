<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="AddHostelRoom.aspx.vb" Inherits="AddHostelRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
  <!-- Form components -->
            <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Hostel Room</h6> </div>
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
                            <label class="col-sm-2 control-label">Room No </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtRoomno" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoomno"
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
                            <label class="col-sm-2 control-label">Room Type</label>
                            <div class="col-sm-10">
                               <asp:DropDownList ID="ddlroomtype" class="form-control" runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                 <asp:ListItem Value="Non AC">Non AC</asp:ListItem>
                                  <asp:ListItem Value="AC">AC</asp:ListItem>
                                   <asp:ListItem Value="Single Room">Single Room</asp:ListItem>
                                    <asp:ListItem Value="Double Room">Double Room</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="" runat="server" ControlToValidate="ddlroomtype"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                </div></div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Total Bed</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtTotBed" class="form-control" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTotBed"
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
                <div class="panel-heading"><h6 class="panel-title">Hostel Room List</h6></div>
                <div class="table-responsive">
               
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="HostelName" HeaderText="Hostel Name"></asp:BoundField>
                         <asp:BoundField DataField="RoomNo" HeaderText="Room No"></asp:BoundField>
                        <asp:BoundField DataField="RoomType" HeaderText="Room Type "></asp:BoundField>
                        <asp:BoundField DataField="TotalBed" HeaderText="TotalBed "></asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="DeActivated"></asp:BoundField>

                       
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("HostelRoomId") %>' CommandName="edit1" runat="server">Edit</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         </Columns>
                        </asp:GridView>
                        </div></div>

            <!-- /form components -->

</asp:Content>

