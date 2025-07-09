<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ProductMaster.aspx.vb" Inherits="ProductMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .select2-container {
            display: inherit !important;
            width: 100% !important;
        }
    </style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="false" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    <div id="divLoading" class="progressdiv">
        <img src="images/Loader.gif" alt="Loading, please wait" />
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
   <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Product Master </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                      
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Select Category </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlcat" class="select-search" AppendDataBoundItems="true"  runat="server">
                                <asp:ListItem Value="0">--Select Category--</asp:ListItem>
                                </asp:DropDownList>
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValu="0" ControlToValidate="ddlcat"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Product Name </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtname" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtname"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">MRP </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtMRP" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMRP"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-sm-2 control-label">Quantity </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtQuantity" class="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Product Type</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlProductType" class="select" AutoPostBack="true" runat="server">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="Book">Book</asp:ListItem>
                                <asp:ListItem Value="Stationery">Stationery</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue="" ControlToValidate="ddlProductType"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                      <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>--%>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                       <div class="form-group" runat="server" id="divPublisher">
                            <label class="col-sm-2 control-label">Publisher </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtPublisher" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                       <div class="form-group" runat="server" id="divSupplier">
                            <label class="col-sm-2 control-label">Supplier </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtSupplier" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>                        
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlProductType" EventName="SelectedIndexChanged" />
                        </Triggers>
                        </asp:UpdatePanel>
                         
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Select Status</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlStatus" class="select" runat="server">
                                <asp:ListItem Value="1">Activate</asp:ListItem>
                                <asp:ListItem Value="0">De-Activate</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ControlToValidate="ddlProductType"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Select Session</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlSession" class="select" runat="server">
                                    <asp:ListItem Value="2017-2018">2017-2018</asp:ListItem>
                                    <asp:ListItem Value="2018-2019" Selected="True">2018-2019</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                            <div class="form-actions text-right">
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                     
                        </div>
                        </div></div>
   <div class="panel panel-default">
                            
                <div class="panel-heading"><h6 class="panel-title">Product List</h6></div>
                <div class="table-responsive">
                        <table class="table">
                        <tfoot>
                            <tr>
                                <th>
                                    <asp:DropDownList ID="ddlcatsearch" class="select-search" AppendDataBoundItems="true"  runat="server">
                                        <asp:ListItem Value="0">--Select Category--</asp:ListItem>
                                    </asp:DropDownList>
                                </th>
                                <th>
                                    <asp:DropDownList ID="ddlProductTypesearch" class="select" runat="server">
                                        <asp:ListItem Value="">Product Type</asp:ListItem>
                                        <asp:ListItem Value="Book">Book</asp:ListItem>
                                        <asp:ListItem Value="Stationery">Stationery</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </th>
                                <th>
                                    <asp:DropDownList ID="ddlSessionsearch" class="select" runat="server">
                                        <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                        <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                    </asp:DropDownList>
                                </th>
                                <th> <asp:TextBox ID="txtProductNamesearch" runat="server" placeHolder="Product Name" class="form-control"></asp:TextBox></th>
                                <th> <asp:TextBox ID="txtPublishersearch" runat="server" placeHolder="Publisher" class="form-control"></asp:TextBox></th>
                                <th> <asp:TextBox ID="txtSuppliersearch" runat="server" placeHolder="Supplier" class="form-control"></asp:TextBox></th>
                                
                                <th><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" /></th>
                                <th><input type="button" id="btnExport" value="Print" onclick="PrintDiv();" class="btn btn-primary" /></th>
                            </tr>
                        </tfoot></table>
                        <div class="table-responsive"  id="dvContents">
                            <div style="font-size: x-large;font-weight: 600;" align="center">
                            <asp:Label ID="lblClass"  runat="server"></asp:Label></div> 
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name"></asp:BoundField>
                            <asp:BoundField DataField="MRP" HeaderText="MRP"></asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Total"></asp:BoundField>
                            <asp:BoundField DataField="SellQuantity" HeaderText="Sell"></asp:BoundField>
                            <asp:BoundField DataField="BalanceQuantity" HeaderText="Stock"></asp:BoundField>
                            <asp:BoundField DataField="Publisher" HeaderText="Publisher"></asp:BoundField>
                            <asp:BoundField DataField="Supplier" HeaderText="Supplier"></asp:BoundField>
                            <asp:BoundField DataField="CategoryName" HeaderText="Category"></asp:BoundField>
                            <asp:BoundField DataField="ProductType" HeaderText="Type"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                       
                   
                            
                       
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("Productid") %>' CommandName="edit1" runat="server"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="delete1"  CommandArgument='<%# Eval("Productid")%>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                            
                         </Columns>
                           
                        </asp:GridView>
                        </div>
                        </div></div>
</ContentTemplate>
</asp:UpdatePanel>


    <script type="text/javascript">        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance(); if (prm != null) { prm.add_endRequest(function (sender, e) { if (sender._postBackSettings.panelsToUpdate != null) { $(".select-search").select2(); $(".select").select2(); $(".select-full").select2(); } }); }; </script>

</asp:Content>

