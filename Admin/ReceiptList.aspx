<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ReceiptList.aspx.vb" Inherits="Admin_ReceiptList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function showPopUp(id) {
            var RowID = $("#" + id).attr("data-id");
            $("#ctl00_C1_hdnRowID").val(RowID);
            $("#ctl00_C1_spanErrorMsg").val("");
            showmodal();
        }
        function calculateNetAmount() {
            var recTotalAmount = document.getElementById("ctl00_C1_txtReceiptAmt").value;
            var discount = document.getElementById("ctl00_C1_txtDiscount").value;
            var latefees = document.getElementById("ctl00_C1_txtlatefeesPaid").value;

            if (discount == "")
                discount = 0;
            if (recTotalAmount == "")
                recTotalAmount = 0;
            if (latefees == "")
                latefees = 0;
            var netAmt = parseFloat(recTotalAmount) + parseFloat(latefees) - parseFloat(discount);
            document.getElementById("ctl00_C1_txtNetAmount").value = netAmt.toFixed(2);
            var ChequeAmount = document.getElementById("ctl00_C1_txtChequeAmount").value;
            var CashAmount = document.getElementById("ctl00_C1_txtCashAmount").value;
            var dueAmt = netAmt - parseFloat(ChequeAmount) - parseFloat(CashAmount);
            document.getElementById("ctl00_C1_txtDueAmount").value = dueAmt.toFixed(2);
        }
        function showmodal() {
            $("#divDeleteAuth").modal("toggle");
        }
        function showPopUp2(id) {
            var RowID2 = $("#" + id).attr("data-id");
            $("#ctl00_C1_hdnRowID2").val(RowID2);
            $("#ctl00_C1_spanErrorMsg").val("");
            showmodal2();
        }
        function showmodal2() {
            $("#divCancelAuth").modal("toggle");
        }
        function showPopUp3(id) {
            var RowID3 = $("#" + id).attr("data-id");
            $("#ctl00_C1_hdnRowID3").val(RowID3);
            $("#ctl00_C1_spanErrorMsg").val("");
            showmodal3();
        }
        function showmodal3() {
            $("#divEditAuth").modal("toggle");
        }
        function ShowHideChequeDetails(value) {
            resetValidator();
            if (value == "Cash") {
                $("#ctl00_C1_divCashAmount").css("display", "block");
                $("#ctl00_C1_divChequeAmount").css("display", "none");
                $("#ctl00_C1_divchequeDetails").css("display", "none");
                $("#ctl00_C1_txtChequeNo").val("");
                $("#ctl00_C1_txtChequeDate").val("");
//                $("#ctl00_C1_txtBankName").val("");
//                $("#ctl00_C1_txtBranchName").val("");
                $("#ctl00_C1_txtChequeAmount").val("0.00");
                $("#ctl00_C1_txtCashAmount").val(document.getElementById("ctl00_C1_txtNetAmount").value);
                $("#ctl00_C1_txtDueAmount").val("0");
            }
            else if (value == "Cash+Cheque") {
                $("#ctl00_C1_divCashAmount").css("display", "block");
                $("#ctl00_C1_divChequeAmount").css("display", "block");
                $("#ctl00_C1_divchequeDetails").css("display", "block");
                $("#ctl00_C1_txtChequeAmount").val("0.00");
                $("#ctl00_C1_txtCashAmount").val("0.00");
                var NetAmount = document.getElementById("ctl00_C1_txtNetAmount").value;
                $("#ctl00_C1_txtDueAmount").val(NetAmount);
            }
            else if (value == "Cheque") {
                $("#ctl00_C1_divCashAmount").css("display", "none");
                $("#ctl00_C1_divChequeAmount").css("display", "block");
                $("#ctl00_C1_divchequeDetails").css("display", "block");
                $("#ctl00_C1_txtCashAmount").val("0.00");
                $("#ctl00_C1_txtChequeAmount").val(document.getElementById("ctl00_C1_txtNetAmount").value);
                $("#ctl00_C1_txtDueAmount").val("0");
            }
            else {
                $("#ctl00_C1_divCashAmount").css("display", "none");
                $("#ctl00_C1_divChequeAmount").css("display", "none");
                $("#ctl00_C1_divchequeDetails").css("display", "none");
                $("#ctl00_C1_txtChequeNo").val("");
                $("#ctl00_C1_txtChequeDate").val("");
//                $("#ctl00_C1_txtBankName").val("");
//                $("#ctl00_C1_txtBranchName").val("");
                $("#ctl00_C1_txtChequeAmount").val("0.00");
                $("#ctl00_C1_txtCashAmount").val("0.00");
                $("#ctl00_C1_txtDueAmount").val("0");
            }
        }
        function validatePayForm() {
            resetValidator();
            var result = true;
            var pMode = $("#ctl00_C1_ddlpmodePaid").val();
            if (pMode == "Cash+Cheque") {
                var cashresult = validateCash();
                var chequeresult = validateCheque();
                if (cashresult == false || chequeresult == false)
                    result = false;
            }
            else if (pMode == "Cash") {
                result = validateCash();
            }
            else if (pMode = "Cheque") {
                result = validateCheque();
            }
            var NetAmount = document.getElementById("ctl00_C1_txtNetAmount").value;
            var ChequeAmount = document.getElementById("ctl00_C1_txtChequeAmount").value;
            var CashAmount = document.getElementById("ctl00_C1_txtCashAmount").value;
            var DueAmount = document.getElementById("ctl00_C1_txtDueAmount").value;

            var tot = parseFloat(ChequeAmount) + parseFloat(CashAmount) + parseFloat(DueAmount);
            if (tot < NetAmount) {
                alert("Cash/Cheque + Due Amount should be equal to Net Amount.");
                result = false;
            }
            return result;
        }
        //Functionto validate cheque amount
        function validateCash() {
            var myresult = true;
            if ($("#ctl00_C1_txtCashAmount").val() == "" || parseFloat($("#ctl00_C1_txtCashAmount").val()) <= 0) {
                $("#spanCashAmount").html("*");
                myresult = false;
            }

            return myresult;
        }
        //Functionto validate cheque amount
        function validateCheque() {
            var myresult = true;
            if ($("#ctl00_C1_txtChequeNo").val() == "") {
                $("#spanChequeNo").html("*");
                myresult = false;
            }

            if ($("#ctl00_C1_txtChequeDate").val() == "") {
                $("#spanChequeDate").html("*");
                myresult = false;
            }

            if ($("#ctl00_C1_txtBankName").val() == "") {
                $("#spanBankName").html("*");
                myresult = false;
            }

            if ($("#ctl00_C1_txtBranchName").val() == "") {
                $("#spanBranchName").html("*");
                myresult = false;
            }

            if ($("#ctl00_C1_txtChequeAmount").val() == "" || parseFloat($("#ctl00_C1_txtChequeAmount").val()) <= 0) {
                $("#spanChequeAmount").html("*");
                myresult = false;
            }

            return myresult;
        }
        function resetValidator() {
            $("#spanChequeAmount").html("");
            $("#spanBranchName").html("");
            $("#spanBankName").html("");
            $("#spanChequeDate").html("");
            $("#spanChequeNo").html("");
            $("#spanCashAmount").html("");
        }
    </script>
    <link href="css/styles.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <asp:Literal ID="litmsgReceipt" runat="server"></asp:Literal>
    <asp:Panel ID="pnlgrid" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Receipt List</h6>
                <div class="form-actions text-right">
                    <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Export to Excel" Visible="false"></asp:LinkButton>
                </div>
            </div>
            <div class="table-responsive">
            <table class="table">
                  <tfoot>
                            <tr>
                                <th><asp:TextBox ID="txtfromDate" runat="server" placeHolder="From Date" class="form-control datepicker"></asp:TextBox></th>
                                <th><asp:TextBox ID="txtToDate" runat="server" placeHolder="To Date" class="form-control datepicker"></asp:TextBox></th>
                                <th><asp:TextBox ID="txtStudentId" runat="server" placeHolder="Student Id" class="form-control"></asp:TextBox></th>
                                <th><asp:TextBox ID="txtReceiptNo" runat="server" placeHolder="Receipt No" class="form-control"></asp:TextBox></th>
                                <th><asp:DropDownList ID="ddlSchoolSession" class="select" runat="server">
                                        <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                        <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                    </asp:DropDownList></th>
                                <th><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" /></th>
                            </tr>
                        </tfoot></table>
                <asp:GridView ID="DataDisplay" class="table table-striped table-bordered table-check" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" CommandArgument='<%# Eval("Cnt")%>' OnClientClick="aspnetForm.target ='_blank';" CommandName="edit1" runat="server" ToolTip="View"><i class="fa fa-eye"> </i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <a id="lnkUpdate" data-id='<%# Eval("Cnt")%>' onclick="showPopUp3(this.id);" runat="server" title="Edit"><i class="fa fa-pencil-square-o"></i></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <a id="lnkCancel" data-id='<%# Eval("Cnt")%>' onclick="showPopUp2(this.id);" runat="server" title="Cancel"><i class="fa fa-times"></i></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <a id="lnkDelete" data-id='<%# Eval("Cnt")%>' onclick="showPopUp(this.id);" runat="server" title="Delete"><i class="fa fa-trash-o"></i></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblPayStatus" runat="server" Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkPay" CommandArgument='<%# Eval("Cnt")%>' CommandName="paid" runat="server">Pay Receipt</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SchoolSession" HeaderText="Session"></asp:BoundField>
                        <asp:BoundField DataField="SessionReceiptNo" HeaderText="Receipt No"></asp:BoundField>
                        <asp:BoundField DataField="StudentId" HeaderText="Student Id"></asp:BoundField>
                        <asp:BoundField DataField="StudentName" HeaderText="Student Name"></asp:BoundField>
                        <asp:BoundField DataField="FatherName" HeaderText="Father's Name"></asp:BoundField>
                        <asp:BoundField DataField="MainClassName" HeaderText="Class"></asp:BoundField>
                        <asp:BoundField DataField="ReceiptAmount" HeaderText="Receipt Amount"></asp:BoundField>
                        <asp:BoundField DataField="PMode" HeaderText="Payment Mode"></asp:BoundField>
                        <asp:BoundField DataField="CashAmount" HeaderText="Cash Amount"></asp:BoundField>
                        <asp:BoundField DataField="ChequeAmount" HeaderText="Cheque Amount"></asp:BoundField>
                        <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                        <asp:BoundField DataField="LateFee" HeaderText="Late Fine"></asp:BoundField>
                        <asp:BoundField DataField="CreateDate" HeaderText="Create Date"></asp:BoundField>
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date"></asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlsubmit" Visible="false" runat="server">
        <div class="form-horizontal">
            <!-- Basic inputs -->
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h6 class="panel-title">Pay Receipt</h6>
                </div>
                <div class="panel-body">
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:Literal ID="litmsgPaid" runat="server"></asp:Literal>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Receipt No. </label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtReceiptnoPaid" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8Paid" runat="server" ControlToValidate="txtReceiptnoPaid"
                                ErrorMessage="*" ValidationGroup="UserRegistrationPaid">*</asp:RequiredFieldValidator>

                        </div>
                        <label class="col-sm-2 control-label">Session </label>
                        <div class="col-sm-4">
                                <asp:DropDownList ID="ddlSession" class="select" runat="server">
                                    <asp:ListItem Value="2017-2018">2017-2018</asp:ListItem>
                                    <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                    </div>
                      <div class="form-group">
                         <label class="col-sm-2 control-label">Receipt Amount</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtReceiptAmt" class="form-control" runat="server" ReadOnly="true" onchange="calculateNetAmount();" />
                        </div>
                             <label class="col-sm-2 control-label">Late Fee</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtlatefeesPaid" class="form-control" runat="server" onchange="calculateNetAmount();" />
                        </div>
                      
                       
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Discount</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtDiscount" class="form-control" runat="server"  ReadOnly="true" onchange="calculateNetAmount();" />
                        </div>
                        <label class="col-sm-2 control-label">Net Amount</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtNetAmount" class="form-control" runat="server" Style=" background: #fcf0b4;" Enabled="false" />
                        </div>
                       
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">PaidDate</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtDueDatePaid" class="form-control datepicker" runat="server"></asp:TextBox>[DD/MM/YYYY]
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9Paid" runat="server" ControlToValidate="txtDueDatePaid"
                                       ErrorMessage="*" ValidationGroup="UserRegistrationPaid">*</asp:RequiredFieldValidator>
                        </div>

                        <label class="col-sm-2 control-label">Payment Mode</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlpmodePaid" class="form-control" runat="server" onchange="ShowHideChequeDetails(this.value);">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                <asp:ListItem Value="Cash+Cheque">Cash+Cheque</asp:ListItem>
                                <%--<asp:ListItem Value="NEFT">NEFT</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ControlToValidate="ddlpmodePaid"
                                ErrorMessage="*" ValidationGroup="UserRegistrationPaid">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                <div class="form-group" style="margin-bottom: 20px;">
                    <div id="divCashAmount" runat="server" style="display: none;">
                        <label class="col-sm-2 control-label">Cash Amount</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtCashAmount" class="form-control" runat="server" onchange="calculateNetAmount();" Text="0.00"></asp:TextBox><span class="error" id="spanCashAmount"></span>
                        </div>
                    </div>
                    <div id="divChequeAmount" runat="server" style="display: none;">
                        <label class="col-sm-2 control-label">Cheque Amount</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtChequeAmount" class="form-control" runat="server" onchange="calculateNetAmount();" Text="0.00"></asp:TextBox><span class="error" id="spanChequeAmount"></span>
                        </div>
                    </div>
                </div>
                <div id="divchequeDetails" runat="server" style="display: none;">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">ChequeNo </label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtChequeNo" class="form-control" runat="server"></asp:TextBox><span class="error" id="spanChequeNo"></span>
                        </div>
                        <label class="col-sm-2 control-label">ChequeDate </label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtChequeDate" class="form-control datepicker" runat="server"></asp:TextBox>[DD/MM/YYYY]<span class="error" id="spanChequeDate"></span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">BankName </label>
                        <div class="col-sm-4">
                            <%--<asp:TextBox ID="txtBankName" class="form-control" runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlBankName" class="form-control" runat="server">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList><span class="error" id="spanBankName"></span>
                        </div>
                        <label class="col-sm-2 control-label">BranchName </label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtBranchName" class="form-control" runat="server"></asp:TextBox><span class="error" id="spanBranchName"></span>
                        </div>
                    </div>

                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Remark </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtremarkPaid" class="form-control" runat="server"></asp:TextBox>
                    </div>
                <label class="col-sm-2 control-label">Due Amount</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtDueAmount" class="form-control" runat="server"></asp:TextBox>
                </div>
                </div>
                </div>
                <div class="form-actions text-right">
                    <asp:Button ID="btnbackPaid" runat="server" CssClass="btn btn-primary" Text="Cancel" />
                    <asp:Button ID="btnSubmitPaid" runat="server" ValidationGroup="UserRegistrationPaid" OnClientClick="return validatePayForm();" class="btn btn-primary" Text="Pay Receipt" />
                </div>
            </div>
        </div>
    </asp:Panel>

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
                                    <asp:TextBox ID="txtMasterPass" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1Paid" runat="server" InitialValue="" ControlToValidate="txtMasterPass"
                                        ErrorMessage="*" ValidationGroup="DeleteAuthentication">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnDelete" Text="Submit" ValidationGroup="DeleteAuthentication" />
                                <input type="hidden" id="hdnRowID" runat="server" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="modal fade" id="divCancelAuth" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-sm" style="width: 350px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Cancel Authetication </h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="panel">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Password</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtMasterPass2" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ControlToValidate="txtMasterPass2"
                                        ErrorMessage="*" ValidationGroup="CancelAuthentication">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnCancel" Text="Cancel" ValidationGroup="CancelAuthentication" />
                                <input type="hidden" id="hdnRowID2" runat="server" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
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
                                    <asp:TextBox ID="txtMasterPass3" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="" ControlToValidate="txtMasterPass3"
                                        ErrorMessage="*" ValidationGroup="EditAuthentication">*</asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnEdit" Text="Edit" ValidationGroup="EditAuthentication" />
                                <input type="hidden" id="hdnRowID3" runat="server" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


</asp:Content>

