<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="EmployeeMonthlyWorkingSheet.aspx.vb" Inherits="Admin_EmployeeMonthlyWorkingSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript"  src="datepicker/js/jquery.mtz.monthpicker.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">

    <!-- Form components --> 
    <div class="form-horizontal">

          <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Employee Monthly Working Sheet</h6>
                <div class="form-actions text-right">
                    <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Export to Excel" Visible="false"></asp:LinkButton>
                </div>
            </div>
             <div class="panel-body">
                <div class="form-group">
                    <label class="col-sm-1 control-label">Month </label>
                    <div class="col-sm-1">
                        <asp:TextBox ID="txtMonth" class="form-control monthpicker" Width="100px" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-1"></div> 
                    <label class="col-sm-2 control-label">Working Days </label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtWorkingDays" class="form-control" Width="100px" runat="server">30</asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Submit" />
                    </div>
                </div>
            </div> 
            <div class="table-responsive">
                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="false" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div id="divLoading" class="progressdiv">
                        <img src="images/Loader.gif" alt="Loading, please wait" />
                    </div>
                </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:GridView ID="DataDisplay" class="table table-striped table-bordered table-check" DataKeyNames="EmployeeId" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EmployeeId" HeaderText="Employee Id"></asp:BoundField>
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name"></asp:BoundField>
                        <asp:BoundField DataField="PayScaleTitle" HeaderText="Pay Scale"></asp:BoundField>
                        <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary"></asp:BoundField>
                        <asp:TemplateField HeaderText="Working Days" HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <asp:Label ID="LblWorkingDays" runat="server" Text='<%# Eval("TotalWorkingDays")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leaves" HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLeaves" OnTextChanged="txtLeaves_TextChanged" AutoPostBack="true"  class="form-control" Width="100px" runat="server">0</asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paid Leaves" HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <asp:Label ID="LblPaidLeaves" runat="server" Text='<%# Eval("TotalPaidLeaves")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salary Days" HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <asp:Label ID="LblSalaryDays" runat="server" Text='<%# Eval("SalaryDays")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Carry Leaves" HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <asp:Label ID="LblCarryLeaves" runat="server" Text='<%# Eval("CarryLeaves")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Generate?">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkGenerate" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    <br />
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
             <div class="panel-body" runat="server" id="GenBox"  Visible="false">
                <div class="form-group">
                    <div class="col-sm-2"></div> 
                    <div class="col-sm-4"></div> 
                        <asp:CheckBox ID="chkAll" AutoPostBack="true"  runat="server" />Select All
                    <div class="col-sm-2">
                        <asp:Button ID="BtnGen" runat="server" class="btn btn-primary" Text="Generate Salary"  />
                    </div>
                </div>
            </div>
        </div>
    </div>
      <script type="text/javascript">
          $('.monthpicker').monthpicker();
        </script>
    <style>
        .mtz-monthpicker-year
        {
            color:black !important;
        }
    </style>
    <!-- /form components -->
</asp:Content>

