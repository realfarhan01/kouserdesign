<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="upgradeclassall.aspx.vb" Inherits="Admin_upgradeclassall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .select2-container {
            width: 100% !important;
        }
    </style>
    <link href="css/styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">Student List</h6>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tfoot>
                    <tr>
                        <th>
                            <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                            </asp:DropDownList></th>
                        <th>
                            <asp:DropDownList ID="ddlSection" class="select" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="">All Section</asp:ListItem>
                                <asp:ListItem Value="A">Section A</asp:ListItem>
                                <asp:ListItem Value="B">Section B</asp:ListItem>
                                <asp:ListItem Value="C">Section C</asp:ListItem>
                            </asp:DropDownList></th>
                        <th>
                                <asp:DropDownList ID="ddlSession" class="select" runat="server">
                                    <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                    <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                </asp:DropDownList>
                            </th>
                        <th>
                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" /></th>
                    </tr>
                </tfoot>
            </table>
            <asp:GridView ID="DataDisplay" DataKeyNames="Studentid" class="table-bordered table-check" AllowPaging="false"  AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Studentid" HeaderText="Student Id"></asp:BoundField>
                    <asp:BoundField DataField="Studentname" HeaderText="Student Name"></asp:BoundField>
                    <asp:BoundField DataField="fathername" HeaderText="Father Name"></asp:BoundField>
                    <asp:BoundField DataField="ClassName" HeaderText="Class"></asp:BoundField>
                    <asp:BoundField DataField="Section" HeaderText="Section"></asp:BoundField>
                    <asp:TemplateField HeaderText="Upgrade?">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkUpgrade" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div runat="server" id="divupgrade">
            <table class="table">
                <tfoot>
                    <tr>
                        <th>Upgrade to Class</th>
                        <th>
                            <asp:DropDownList ID="ddlClassUpgrade" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                            </asp:DropDownList></th>
                        <th>
                            <asp:DropDownList ID="ddlSectionUpgrade" class="select" runat="server">
                                <asp:ListItem Value="A">Section A</asp:ListItem>
                                <asp:ListItem Value="B">Section B</asp:ListItem>
                                <asp:ListItem Value="C">Section C</asp:ListItem>
                            </asp:DropDownList></th>
                        <th>
                            <asp:DropDownList ID="ddlSessionUpgrade" class="select" runat="server">
                                    <asp:ListItem Value="2018-2019" Selected="True">2018-2019</asp:ListItem>
                                </asp:DropDownList>
                        </th>
                        <th><asp:CheckBox ID="chkAll" AutoPostBack="true"  runat="server" />Select All</th>
                        <th>
                            <asp:Button ID="btnUpgrade" runat="server" Text="Upgrade" CssClass="btn btn-primary" /></th>
                    </tr>
                </tfoot>
            </table></div>
        </div>
    </div>
    
    
    
    
</asp:Content>

