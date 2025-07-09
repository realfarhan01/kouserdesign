<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="EmployeePayScaleReport.aspx.vb" Inherits="Admin_EmployeePayScaleReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">

    <!-- Form components -->
    <div class="form-horizontal">

          <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Employee Pay Scale</h6>
                <div class="form-actions text-right">
                    <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Export to Excel" Visible="false"></asp:LinkButton>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="DataDisplay" class="table table-striped table-bordered table-check" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <a id="lnkUpdate" data-id='<%# Eval("PSID")%>' onclick="showPopUp3(this.id);" runat="server" title="Edit"><i class="fa fa-pencil-square-o"></i></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <a id="lnkDelete" data-id='<%# eval("PSID") %>' onclick="showPopUp(this.id);" runat="server" title="Delete"><i class="fa fa-trash-o"></i></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id"></asp:BoundField>
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name"></asp:BoundField>
                        <asp:BoundField DataField="PayScaleTitle" HeaderText="Pay Scale"></asp:BoundField>
                        <asp:BoundField DataField="DA" HeaderText="Driving Allowance (DA)"></asp:BoundField>
                        <asp:BoundField DataField="HRA" HeaderText="House Rent Allowance (HRA)"></asp:BoundField>
                        <asp:BoundField DataField="OA" HeaderText="Other Allowance"></asp:BoundField>
                        <asp:BoundField DataField="PF" HeaderText="Provident Fund (PF)"></asp:BoundField>
                        <asp:BoundField DataField="TDS" HeaderText="Tax Deduction (TDS)"></asp:BoundField>
                        <asp:BoundField DataField="OD" HeaderText="Other Deduction (OD)"></asp:BoundField>
                        <asp:BoundField DataField="ESI" HeaderText="Employee's State Insurance (ESI)"></asp:BoundField>
                        <asp:BoundField DataField="PaidLeaves" HeaderText="Paid Leaves"></asp:BoundField>
                        <asp:BoundField DataField="ConvenceCharges" HeaderText="Convence Charges"></asp:BoundField>
                        <asp:BoundField DataField="Advance" HeaderText="Advance Amount"></asp:BoundField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>

    <!-- /form components -->
</asp:Content>

