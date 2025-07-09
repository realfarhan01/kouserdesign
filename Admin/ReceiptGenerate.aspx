<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ReceiptGenerate.aspx.vb" Inherits="Admin_ReceiptGenerate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .error {
            color: red;
        }
        .txtper
        {
            width:50px !important;
        }
    </style>
    <script type="text/javascript">
        function showPayDiv() {
            $("#ctl00_C1_pnlpay").css("visibility", "visible");
        }
        function hidePayReceipt() {
            $("#ctl00_C1_pnlpay").css("visibility", "hidden");
        }
        function hidePDC() {
            $("#divaddMorePDC").modal("toggle");
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
                $("#ctl00_C1_txtChequeAmount").val("0");
                $("#ctl00_C1_txtCashAmount").val(document.getElementById("ctl00_C1_txtNetAmount").value);
                $("#ctl00_C1_txtDueAmount").val("0");
            }
            else if (value == "Cash+Cheque") {
                $("#ctl00_C1_divCashAmount").css("display", "block");
                $("#ctl00_C1_divChequeAmount").css("display", "block");
                $("#ctl00_C1_divchequeDetails").css("display", "block");
                $("#ctl00_C1_txtChequeAmount").val("0.00");
                $("#ctl00_C1_txtCashAmount").val("0");
                var NetAmount = document.getElementById("ctl00_C1_txtNetAmount").value;
                $("#ctl00_C1_txtDueAmount").val(NetAmount);
            }
            else if (value == "Cheque") {
                $("#ctl00_C1_divCashAmount").css("display", "none");
                $("#ctl00_C1_divChequeAmount").css("display", "block");
                $("#ctl00_C1_divchequeDetails").css("display", "block");
                $("#ctl00_C1_txtCashAmount").val("0");
                $("#ctl00_C1_txtDueAmount").val("0");
                $("#ctl00_C1_txtChequeAmount").val(document.getElementById("ctl00_C1_txtNetAmount").value);
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

//            if ($("#ctl00_C1_txtBankName").val() == "") {
//                $("#spanBankName").html("*");
//                myresult = false;
//            }

//            if ($("#ctl00_C1_txtBranchName").val() == "") {
//                $("#spanBranchName").html("*");
//                myresult = false;
//            }

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
        function calculateDiscount(value) {
            var totalInstallmentAmount = 0;
            $("#ctl00_C1_txtDiscountPer").val(value);
            var inputElements = document.getElementsByClassName('donotchange');
            for (var i = 0; i < inputElements.length; i++) {
                var myElement = inputElements[i];
                // Filter through the input types looking for checkboxes
                var id = myElement.id;
                var amountID = id.replace("lblFeeType", "lblAmount");
                var feeType = $("#" + id).html().toLocaleLowerCase();
                if (feeType.indexOf('installment') > 0 && feeType.indexOf('conveyance') <= 0) {
                    totalInstallmentAmount = totalInstallmentAmount + parseFloat($("#" + amountID).html());
                }
            }
            var totalDiscount = totalInstallmentAmount * parseFloat(value) / 100;
            $("#ctl00_C1_txtDiscount").val(totalDiscount.toFixed(2));

            calculateNetAmount();
        }
        function calculateDiscountPer(value) {
            var totalInstallmentAmount = 0;
            var inputElements = document.getElementsByClassName('donotchange');
            for (var i = 0; i < inputElements.length; i++) {
                var myElement = inputElements[i];
                // Filter through the input types looking for checkboxes
                var id = myElement.id;
                var amountID = id.replace("lblFeeType", "lblAmount");
                var feeType = $("#" + id).html().toLocaleLowerCase();
                if (feeType.indexOf('installment') > 0 && feeType.indexOf('conveyance') <= 0) {
                    totalInstallmentAmount = totalInstallmentAmount + parseFloat($("#" + amountID).html());
                }
            }
            var totalDiscount = totalInstallmentAmount * parseFloat(value) / 100;
            $("#ctl00_C1_txtDiscount").val(totalDiscount.toFixed(2));

            calculateNetAmount();
        }
    </script>
    <style type="text/css">
        .button-a {
            float: right;
        }

        .select2-container {
            display: inherit !important;
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <!-- Form components -->
    <div class="form-horizontal">
        <!-- Basic inputs -->

        <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="false" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div id="divLoading" class="progressdiv">
                    <img src="images/Loader.gif" alt="Loading, please wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h6 class="panel-title">Make a New Receipt</h6>
                    </div>
                    <div class="panel-body">
                        <asp:HiddenField ID="hfId" runat="server" />
                        <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>

                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Search Student</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlsearch" runat="server" class="select-search" AutoPostBack="true" AppendDataBoundItems="true" Width="100%">
                                    <asp:ListItem Value="">Search</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Student Id </label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtStudentId" class="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtStudentId"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                            <%--<label class="col-sm-2 control-label">Student Name</label>--%>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtName" placeholder="Student Name" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlSession" class="select-search" AutoPostBack="true" runat="server">
                                    <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                    <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Class</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtClass" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClass"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>

                            <label class="col-sm-2 control-label">Receipt No. </label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtReceiptno" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReceiptno"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                            <label class="col-sm-2 control-label">Receipt Date</label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtDueDate" class="form-control datepicker" runat="server"></asp:TextBox>[DD/MM/YYYY]
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDueDate"
                                               ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--<div class="form-group">
                    <label class="col-sm-2 control-label">Due Date</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtDueDate" class="form-control datepicker" runat="server"></asp:TextBox>[DD/MM/YYYY]
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDueDate"
                                       ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>--%>

                        <div class="form-group">
                            <%--data-toggle="modal" data-target="#divaddMorePDC"  data-toggle="modal" data-target="#divaddMoreConveyance"--%>
                            <a id="lnkAddPDC" runat="server" class="button-a blue" visible="true" onclick="showPopUpByType(0)" style="margin-bottom: 5px; float: right; margin-right: 5px; text-decoration: underline;">Add Fees</a>
                            <a id="lnkAddConveyance" runat="server" class="button-a blue" visible="true" onclick="showPopUpByType(1)" style="margin-bottom: 5px; float: right; margin-right: 10px; text-decoration: underline;">Add Conveyance Fees</a>
                            <input type="hidden" id="hidPDCType" runat="server" value="0" />
                            <asp:Button ID="btnBindPdcORConveyance" runat="server" Style="display: none;" />
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
                                    <asp:TemplateField HeaderText="Fee Type" HeaderStyle-Width="40%" ItemStyle-Width="35%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeeType" CssClass="donotchange" runat="server" Text='<%#Eval("FeeType") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlFeeType" runat="server" AppendDataBoundItems="true" Width="100%">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtnewFeeType" class="form-control" runat="server" />
                                            <%--<asp:DropDownList ID="ddlnewFeeType" class="form-control" runat="server" AppendDataBoundItems="true" Width="100%">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                            </asp:DropDownList>--%>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Term" HeaderStyle-Width="40%" ItemStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTerm" class="form-control" runat="server" Text='<%#Eval("TermType") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTerm" class="form-control" runat="server" AppendDataBoundItems="true" Width="100%">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                    <asp:DropDownList ID="ddlnewTerm" class="form-control" runat="server" AppendDataBoundItems="true" Width="100%">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="7%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtnewAmount" class="form-control" runat="server" Style="width: 120px; background: #fcf0b4;" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Late Fee" HeaderStyle-Width="7%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLateFee" runat="server" Text='<%#Eval("LateFee") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="txtLateFee" runat="server" Text='<%#Eval("LateFee") %>' />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtnewLateFee" class="form-control" runat="server" Style="width: 120px; background: #fcf0b4;" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Due Date" HeaderStyle-Width="13%" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDueDate" runat="server" Text='<%#Eval("NextDueDate") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDueDate" class="form-control" runat="server" Text='<%#Eval("NextDueDate") %>' Style="width: 120px;
                                        background: #fcf0b4;" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtnewDueDate" class="form-control datepicker" runat="server" Style="width: 120px; background: #fcf0b4;" />
                                </FooterTemplate>
                            </asp:TemplateField>--%>
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
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Receipt Total Amount</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtReceiptAmt" class="form-control" Enabled="false" runat="server">0</asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">Late Fee</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtlatefeesPaid" class="form-control" runat="server" onchange="calculateNetAmount();">0</asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label" style="padding-top: 0px;">
                                <asp:DropDownList ID="ddlDiscount" Style="font-weight: bold;" runat="server" class="form-control" onchange="calculateDiscount(this.value);">
                                    <asp:ListItem Value="0">Discount</asp:ListItem>
                                    <asp:ListItem Value="25">Discount(25%)</asp:ListItem>
                                    <asp:ListItem Value="50">Discount(50%) </asp:ListItem>
                                    <asp:ListItem Value="50">Staff Discount(50%) </asp:ListItem>
                                    <asp:ListItem Value="100">100% Free(100%)</asp:ListItem>
                                </asp:DropDownList></label>
                            <div class="col-sm-1">
                                <asp:TextBox ID="txtDiscountPer" class="form-control txtper" runat="server" onchange="calculateDiscount(this.value);">0</asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                            %
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtDiscount" class="form-control" runat="server" onchange="calculateNetAmount();">0</asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label"><b>Net Amount</b></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtNetAmount" class="form-control" ReadOnly="true" runat="server">0</asp:TextBox>
                            </div>

                        </div>
                        <div class="form-actions text-right">
                            <a id="btnnewreceipt" runat="server" class="btn btn-primary" visible="false" href="ReceiptGenerate.aspx">New Receipt</a>
                            <a id="btnPay" runat="server" class="btn btn-primary" visible="false" onclick="showPayDiv();">Pay Receipt</a>
                            <asp:Button ID="btnPreview" runat="server" class="btn btn-primary" OnClientClick="aspnetForm.target ='_blank';" Text="Preview" Visible="false" />
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Generate Receipt" />
                            <input type="hidden" id="hdnReceiptId" runat="server" />
                        </div>

                    </div>
                </div>
                <div class="modal fade" id="divaddMorePDC" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="addMoreHead" runat="server">Add More PDC </h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <asp:Repeater ID="rptMorePBCBilling" runat="server">
                                        <HeaderTemplate>
                                            <table id="example" class="table table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th class="nosorting">
                                                            <input type="checkbox" name="checkbox" id="chkSelectAll" onchange="selectAllItem(this);showHideAction();"></th>
                                                        <th>S.No.</th>
                                                        <th>Fee Type</th>
                                                        <th>Amount</th>
                                                        <th>Late Fee</th>

                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <td>
                                                <input type="checkbox" name="checkbox2" class="griditemcheckbox" id="chkSelect" runat="server" onchange="showHideAction();" /></td>
                                            <td>
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFeeType" runat="server" Text='<%#Eval("FeeType")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <input type="hidden" id="hidPDCId" runat="server" value='<%#Eval("PDCId")%>' />
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLateFee" runat="server" Text='<%#Eval("LateFee")%>'></asp:Label>
                                            </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnAddPDC" Text="Add to List" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlsearch"
                    EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="btnAddPDC" />
                <asp:PostBackTrigger ControlID="btnBindPdcORConveyance" />
                <asp:PostBackTrigger ControlID="btnPreview" />
                <asp:PostBackTrigger ControlID="btnSubmit"/>
                    
            </Triggers>
        </asp:UpdatePanel>
        <asp:Panel ID="pnlpay" Style="visibility: hidden;" runat="server">
            <div class="form-horizontal">
                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h6 class="panel-title">Pay Receipt</h6>
                    </div>
                    <div class="panel-body">
                        <asp:Literal ID="litmsgPaid" runat="server"></asp:Literal>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Receipt No. </label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtReceiptnoPaid" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8Paid" runat="server" ControlToValidate="txtReceiptnoPaid"
                                    ErrorMessage="*" ValidationGroup="UserRegistrationPaid">*</asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1Paid" runat="server" InitialValue="" ControlToValidate="ddlpmodePaid"
                                    ErrorMessage="*" ValidationGroup="UserRegistrationPaid">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group" style="margin-bottom: 20px;">
                            <div id="divCashAmount" runat="server" style="display: none;">
                                <label class="col-sm-2 control-label">Cash Amount</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtCashAmount" class="form-control" runat="server" Text="0.00"></asp:TextBox><span class="error" id="spanCashAmount"></span>
                                </div>
                            </div>
                            <div id="divChequeAmount" runat="server" style="display: none;">
                                <label class="col-sm-2 control-label">Cheque Amount</label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtChequeAmount" class="form-control" runat="server" Text="0.00"></asp:TextBox><span class="error" id="spanChequeAmount"></span>
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
                                    <asp:DropDownList ID="ddlBankName" class="select" runat="server">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="txtBankName" class="form-control" runat="server"></asp:TextBox>--%>
                                    <span class="error" id="spanBankName"></span>
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
                        <div class="form-actions text-right">
                            <a id="btnbackPaid" runat="server" class="btn btn-primary" onclick="hidePayReceipt();">Cancel</a>

                            <asp:Button ID="btnSubmitPaid" runat="server" ValidationGroup="UserRegistrationPaid" OnClientClick="return validatePayForm();" class="btn btn-primary" Text="Pay Receipt" />

                        </div>

                    </div>
                </div>
            </div>
        </asp:Panel>

    </div>


    <!-- /form components -->
    <script type="text/javascript">
        function showHideAction() {
            var isChecked = false;
            var isEmpty = false;
            var obj = document.getElementsByClassName("griditemcheckbox")
            var CheckboxGUIDCollection = ''
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].type == "checkbox") {
                    if (obj[i].checked) {
                        isChecked = true;
                    }
                }
            }
            if (!isChecked) {
                $("#ctl00_C1_btnAddPDC").css("display", "none");
            }
            else {
                $("#ctl00_C1_btnAddPDC").css("display", "inline");

            }
        }
        function selectAllItem(invoker) {

            var inputElements = document.getElementsByClassName('griditemcheckbox');
            for (var i = 0; i < inputElements.length; i++) {
                var myElement = inputElements[i];
                // Filter through the input types looking for checkboxes
                if (myElement.type === "checkbox") {
                    // Use the invoker (our calling element) as the reference 
                    //  for our checkbox status
                    if (myElement.disabled == false) {
                        myElement.checked = invoker.checked;
                    }
                }
            }
        }
        function showPopUpByType(typeid) {
            var currentTypeId = $("#ctl00_C1_hidPDCType").val();
            // alert(currentTypeId);
            if (currentTypeId == typeid) {
                $("#divaddMorePDC").modal("toggle");
            }
            else {
                //  $("ctl00_C1_hidPDCType").val(typeid);
                $("#ctl00_C1_btnBindPdcORConveyance").click();
                //$("#divaddMorePDC").modal("toggle");
            }
        }
        function showPDCPopup() {
            $("#divaddMorePDC").modal("toggle");
        }
    </script>
    <script type="text/javascript">        //On UpdatePanel Refresh 
        var prm = Sys.WebForms.PageRequestManager.getInstance(); if (prm != null) { prm.add_endRequest(function (sender, e) { if (sender._postBackSettings.panelsToUpdate != null) { $(".select-search").select2(); } }); }; </script>

</asp:Content>

