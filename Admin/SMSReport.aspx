<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="SMSReport.aspx.vb" Inherits="Admin_SMSReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">SMS List</h6>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="DataDisplay" class="table table-bordered table-check" runat="server">

                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>


</asp:Content>

