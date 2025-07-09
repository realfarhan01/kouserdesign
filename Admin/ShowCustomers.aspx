<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ShowCustomers.aspx.vb" Inherits="Admin_ShowCustomers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">


        function showPopUp(id) {
            var RowID = $("#" + id).attr("data-id");
            $("#ctl00_C1_hdnRowID").val(RowID);
            $("#ctl00_C1_spanErrorMsg").val("");
            showmodal();
        }
        function showmodal() {
            $("#divEditAuth").modal("toggle");
        }
        function showPopUp2(id) {
            var RowID = $("#" + id).attr("data-id");
            $("#ctl00_C1_hdnRowID").val(RowID);
            $("#ctl00_C1_spanErrorMsg").val("");
            showmodal2();
        }
        function showmodal2() {
            $("#divDeleteAuth").modal("toggle");
        }
        function showPopUp3(id) {
            var RowID = $("#" + id).attr("data-id");
            $("#ctl00_C1_hdnRowID").val(RowID);
            $("#ctl00_C1_txtUpgradeStudentId").val(RowID);
            $("#ctl00_C1_spanErrorMsg").val("");
            showmodal3();
        }
        function showmodal3() {
            $("#divUpgradeAuth").modal("toggle");
        }
    </script>
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
            <h6 class="panel-title">Customer List</h6>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tfoot>
                    <tr>
                        <th colspan="5">
                            <asp:DropDownList ID="ddlsearch" runat="server" class="select-search" AutoPostBack="true" AppendDataBoundItems="true" Width="100%">
                                <asp:ListItem Value="">Search</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                    </tr>
                    <tr>
                        <th>
                            <asp:TextBox ID="txtCustomerId" runat="server" placeHolder="Customer Id" class="form-control"></asp:TextBox></th>
                        <th>
                            <asp:TextBox ID="txtCustomerName" runat="server" placeHolder="Customer Name" class="form-control"></asp:TextBox></th>
                        <th>
                            <asp:DropDownList ID="ddlSession" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="">--Select Session--</asp:ListItem>
                            </asp:DropDownList></th>
                        <th>
                            <asp:TextBox ID="txtFrom" runat="server" placeHolder="Reg From" class="form-control datepicker"></asp:TextBox></th>
                        <th>
                            <asp:TextBox ID="txtTo" runat="server" placeHolder="Reg To" class="form-control datepicker"></asp:TextBox></th>
                        <th>
                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" /></th>
                    </tr>
                </tfoot>
            </table>
            <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="nosorting">
                        <ItemTemplate>
                            <a id="lnkEdit" data-id='<%#Eval("CustomerId") %>' onclick="showPopUp(this.id);" runat="server" title="Edit"><i class="fa fa-pencil-square-o"></i></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="nosorting">
                        <ItemTemplate>
                            <a id="lnkDelete" data-id='<%# Eval("CustomerId")%>' onclick="showPopUp2(this.id);" runat="server" title="Delete"><i class="fa fa-trash-o"></i></a><%-- <a href='Studentregistration.aspx?Sid=<%# eval("Studentid") %>'>Edit</a>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CustomerId" HeaderText="Customer Id"></asp:BoundField>
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name"></asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                    <asp:BoundField DataField="Mobile" HeaderText="Mobile"></asp:BoundField>
                    <asp:BoundField DataField="DOB" HeaderText="DOB"></asp:BoundField>
                    <asp:BoundField DataField="City" HeaderText="City"></asp:BoundField>
                    <asp:BoundField DataField="State" HeaderText="State"></asp:BoundField>
                    <asp:BoundField DataField="GSTNo" HeaderText="GSTNo"></asp:BoundField>
                    <asp:BoundField DataField="RegSession" HeaderText="Session"></asp:BoundField>
                    <asp:BoundField DataField="RegDate" HeaderText="RegDate"></asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="modal fade" id="divEditAuth" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-sm" style="width: 350px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Edit Authetication </h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="panel">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Password</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtMasterPass" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ControlToValidate="txtMasterPass"
                                        ErrorMessage="*" ValidationGroup="EditAuthentication">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnEdit" Text="Edit" ValidationGroup="EditAuthentication" />
                                <input type="hidden" id="hdnRowID" runat="server" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="modal fade" id="divDeleteAuth" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-sm" style="width: 350px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Delete Authetication </h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="panel">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Password</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtMasterPass2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ControlToValidate="txtMasterPass2"
                                        ErrorMessage="*" ValidationGroup="DeleteAuthentication">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnDelete" Text="Edit" ValidationGroup="DeleteAuthentication" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

