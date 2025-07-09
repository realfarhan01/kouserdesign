<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="EmployeeMonthlyUnpaidIncome.aspx.vb" Inherits="Admin_EmployeeMonthlyUnpaidIncome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript"  src="datepicker/js/jquery.mtz.monthpicker.js"></script>
<script type="text/javascript">
    function PrintDiv() {
        var divContents = document.getElementById("dvContents").innerHTML;
        var printWindow = window.open();
        printWindow.document.write('<html><head>');
        printWindow.document.write('</head><body >');
        printWindow.document.write(divContents);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">

    <!-- Form components --> 
    <div class="form-horizontal">
        <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="false" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="divLoading" class="progressdiv">
                <img src="images/Loader.gif" alt="Loading, please wait" />
            </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Employee Monthly Unpaid Salary Sheet</h6>
                <div class="form-actions text-right">
                    <input type="button" id="btnExport" value="Print" onclick="PrintDiv();" class="btn btn-primary" />
                </div>
            </div>
             <div class="panel-body">
                <div class="form-group">
                    <label class="col-sm-1 control-label">Month </label>
                    <div class="col-sm-1">
                        <asp:TextBox ID="txtMonth" class="form-control monthpicker" Width="100px" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-1"></div> 
                    <div class="col-sm-2">
                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Submit" />
                    </div>
                </div>
            </div> 
            <div class="table-responsive"  id="dvContents">
                <div align="center" runat="server" id="reportheader" visible="false" >
                 <caption>
                    <asp:Literal runat="server" ID="ltrSchool"></asp:Literal>
                    <div align="left">Print Date:<%=Date.Today.ToString("dd/MM/yyyy") %></div>
                </caption>
                </div> 
                <asp:GridView ID="DataDisplay" class="table table-striped table-bordered table-check" AutoGenerateColumns="false" DataKeyNames="EmployeeId" runat="server">
                    <Columns>
                        <asp:BoundField DataField="SNo" HeaderText="SNo"></asp:BoundField>
                        <asp:BoundField DataField="EmployeeId" HeaderText="Employee Code"></asp:BoundField>
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name"></asp:BoundField>
                        <asp:BoundField DataField="SalaryDays" HeaderText="Salary Days"></asp:BoundField>
                        <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary"></asp:BoundField>
                        <asp:BoundField DataField="DA" HeaderText="DA"></asp:BoundField>
                        <asp:BoundField DataField="HRA" HeaderText="HRA"></asp:BoundField>
                        <asp:BoundField DataField="OA" HeaderText="OA"></asp:BoundField>
                        <asp:BoundField DataField="GrossSalary" HeaderText="Gross"></asp:BoundField>
                        <asp:BoundField DataField="PF" HeaderText="PF"></asp:BoundField>
                        <asp:BoundField DataField="TDS" HeaderText="TDS"></asp:BoundField>
                        <asp:BoundField DataField="ESI" HeaderText="ESI"></asp:BoundField>
                        <asp:BoundField DataField="ConvenceCharges" HeaderText="Conv."></asp:BoundField>
                        <asp:BoundField DataField="Advance" HeaderText="Adv."></asp:BoundField>
                        <asp:BoundField DataField="OtherDeduction" HeaderText="OD"></asp:BoundField>
                        <asp:BoundField DataField="TotalDeduction" HeaderText="Deduction"></asp:BoundField>
                        <asp:BoundField DataField="NetSalary" HeaderText="Net Salary"></asp:BoundField>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkGenerate" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="panel-body" runat="server" id="GenBox"  Visible="false">
                <div class="form-group">
                    <div class="col-sm-2"></div> 
                    <div class="col-sm-4"> <asp:CheckBox ID="chkAll" AutoPostBack="true"  runat="server" />Select All</div> 
                    <div class="col-sm-2">
                        <asp:Button ID="BtnGen" runat="server" class="btn btn-primary" Text="Paid Salary"  />
                    </div> 
                    <div class="col-sm-2">
                        <asp:Button ID="btnDelete" runat="server" class="btn btn-primary" Text="Delete"  />
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
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

