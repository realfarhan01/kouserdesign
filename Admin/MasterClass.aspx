<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="MasterClass.aspx.vb" Inherits="Admin_MasterClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
    <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Class Master</h6></div>
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
                                <%--<asp:TextBox ID="txtcname" class="form-control" runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlClass" InitialValue="0"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Section </label>
                            <div class="col-sm-10">
                               <asp:DropDownList ID="ddlSection" class="select-search" runat="server">
                                <asp:ListItem Value="A">A</asp:ListItem>
                                <asp:ListItem Value="B">B</asp:ListItem>
                                 <asp:ListItem Value="C">C</asp:ListItem>
                                  <asp:ListItem Value="D">D</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlSection"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                     <%--    <div class="form-group">
                            <label class="col-sm-2 control-label">Class Room </label>
                            <div class="col-sm-10">
                               <asp:DropDownList ID="txtclassroom" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtclassroom"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                      
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Class Head </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtHead" class="form-control" runat="server"></asp:TextBox>
                                
                            </div>
                        </div>--%>
                        
                            <div class="form-actions text-right">
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>

                        <div class="panel panel-default">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                <div class="panel-heading"><h6 class="panel-title">Class List</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="ClassName" HeaderText="Class Name"></asp:BoundField>
                         <asp:BoundField DataField="Section" HeaderText="Section"></asp:BoundField>
                      <%--  <asp:BoundField DataField="ClassRoomNo" HeaderText="Class Room No "></asp:BoundField>
                        <asp:BoundField DataField="ClassHead" HeaderText="ClassHead"></asp:BoundField>
                  

                     
                         <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("Classid") %>' CommandName="edit1" runat="server">Edit</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>--%>
                         </Columns>
                        </asp:GridView>
                        </div></div>

</asp:Content>

