<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="AddHolydaylist.aspx.vb" Inherits="AddHolydaylist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
  <!-- Form components -->
            <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Holiday List</h6></div>
                    <div class="panel-body">
                    <asp:HiddenField ID="hfId" runat="server" />
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                          
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                        
                         <div class="form-group">

                         <asp:GridView ID="DataDisplay" runat="server" class="table table-bordered table-check" AutoGenerateColumns="false"
                        Width="100%" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="Record Not Fount !"
                        ShowFooter="True" DataKeyNames="RowID">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hfpid" runat="server" Value='<%#Eval("RowId") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                           
                            
                            <asp:TemplateField HeaderText="Holiday" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblHoliday" runat="server" Text='<%#Eval("Holiday") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtHoliday" runat="server" Text='<%#Eval("Holiday") %>' />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtnewHoliday" class="form-control" runat="server" Style="width: 120px; background: #fcf0b4;" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Date" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFromDate" class="form-control" runat="server" Text='<%#Eval("FromDate") %>' Style="width: 120px;
                                        background: #fcf0b4;" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtnewFromDate" class="form-control datepicker" runat="server" Style="width: 120px; background: #fcf0b4;" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtToDate" class="form-control" runat="server" Text='<%#Eval("ToDate") %>' Style="width: 120px;
                                        background: #fcf0b4;" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtnewToDate" class="form-control datepicker" runat="server" Style="width: 120px; background: #fcf0b4;" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Active" HeaderStyle-Width="25%" ItemStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:Label ID="lblActive" class="form-control" runat="server" Text='<%#Eval("isActive") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlActive" class="form-control" runat="server" AppendDataBoundItems="true" Width="100%">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                         <asp:ListItem Value="0">Active</asp:ListItem>
                                <asp:ListItem Value="1">Deactive</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <%--<asp:TextBox ID="txtnewproductcode" runat="server" />--%>
                                    <asp:DropDownList ID="ddlNewActive" class="form-control" runat="server" AppendDataBoundItems="true" Width="100%">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                         <asp:ListItem Value="0">Active</asp:ListItem>
                                <asp:ListItem Value="1">Deactive</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                        Text="Update"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="lnkAdd" runat="server" CssClass="button-a blue" CausesValidation="False" CommandName="Insert"
                                        Text="Add To List" />
                                </FooterTemplate>
                                <ItemTemplate>
                                   <%-- <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        Text="Edit"></asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>
                        </div>
                        
                            <div class="form-actions text-right">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div></div></div>

            <!-- /form components -->

</asp:Content>

