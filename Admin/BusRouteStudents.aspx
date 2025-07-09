<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="BusRouteStudents.aspx.vb" Inherits="Admin_BusRouteStudents" %>

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
                <h6 class="panel-title">Bus Route-Pickup Point Students list</h6>
                <div class="form-actions text-right">
                    <input type="button" id="btnExport" value="Print" onclick="PrintDiv();" class="btn btn-primary" />
                    &nbsp;
                    <asp:Button ID="btnexportpage" runat="server" OnClientClick="aspnetForm.target ='_blank';" Text="Export Student Data" CssClass="btn btn-primary" />
                </div>
            </div>
        <div class="table-responsive">
            <table class="table">
                <tfoot>
                    <tr>
                        <th>
                            <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:DropDownList ID="ddlSection" class="select" runat="server">
                                <asp:ListItem Value="">All Section</asp:ListItem>
                                <asp:ListItem Value="A">Section A</asp:ListItem>
                                <asp:ListItem Value="B">Section B</asp:ListItem>
                                <asp:ListItem Value="C">Section C</asp:ListItem>
                                <asp:ListItem Value="D">Section D</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:DropDownList ID="ddlRoute" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">All Routes</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:DropDownList ID="ddlPickupPoints" class="select" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">All Pickup Points</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                    </tr>
                    <tr>
                        <th>
                            <asp:TextBox ID="txtStudentid" runat="server" placeHolder="Student Id" class="form-control"></asp:TextBox>

                        </th>
                        <th>
                            <asp:TextBox ID="txtstuname" runat="server" placeHolder="Student Name" class="form-control"></asp:TextBox>

                        </th>
                        <th>
                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" />

                        </th>
                    </tr>
                </tfoot>
            </table>
            </div> 
            <div class="table-responsive"  id="dvContents">
                <div align="center" runat="server" id="reportheader" visible="false" >
                 <caption>
                    <asp:Literal runat="server" ID="ltrSchool"></asp:Literal>
                    <div align="left">Print Date:<%=Date.Today.ToString("dd/MM/yyyy") %></div>
                </caption>
                </div> 
                <asp:GridView ID="DataDisplay" class="table table-striped table-bordered table-check" Width="100%" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="SNo" HeaderText="SNo"></asp:BoundField>
                        <asp:BoundField DataField="StudentId" HeaderText="Adm No"></asp:BoundField>
                        <asp:BoundField DataField="StudentName" HeaderText="Name"></asp:BoundField>
                        <asp:BoundField DataField="ClassName" HeaderText="Class"></asp:BoundField>
                        <asp:BoundField DataField="Section" HeaderText="Section"></asp:BoundField>
                        <asp:BoundField DataField="PickupPoint" HeaderText="Pickup Point"></asp:BoundField>
                        <asp:BoundField DataField="RouteName" HeaderText="Route"></asp:BoundField>
                        <asp:BoundField DataField="MonthlyCharge" HeaderText="Monthly Charge"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">        //On UpdatePanel Refresh 
        var prm = Sys.WebForms.PageRequestManager.getInstance(); if (prm != null) { prm.add_endRequest(function (sender, e) { if (sender._postBackSettings.panelsToUpdate != null) { $(".select-search").select2(); } }); };

    </script>
    <!-- /form components -->
    <!-- /form components -->
</asp:Content>

