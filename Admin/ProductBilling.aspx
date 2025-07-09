<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ProductBilling.aspx.vb" Inherits="ProductBilling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .error {
            color: red;
        }
    </style>
     <style type="text/css">
        .button-a {
            float: right;
        }

        .select2-container {
            display: inherit !important;
            width: 100% !important;
        }
    </style>

    <script type="text/javascript">
        function showPayDiv() {
            $("#ctl00_C1_pnlpay").css("visibility", "visible");
        }
        function hidePayReceipt() {
            $("#ctl00_C1_pnlpay").css("visibility", "hidden");
        }
        function calculateNetAmount() {
            var recTotalAmount = document.getElementById("ctl00_C1_txtReceiptAmt").value;
            var discount = document.getElementById("ctl00_C1_txtDiscount").value;
            if (discount == "")
                discount = 0;
            if (recTotalAmount == "")
                recTotalAmount = 0;
          
            var netAmt = parseFloat(recTotalAmount) - parseFloat(discount);
            document.getElementById("ctl00_C1_txtNetAmount").value = netAmt.toFixed(2);
        }
        function ShowHideChequeDetails(value) {
            resetValidator();
            if (value == "Cash") {
                $("#ctl00_C1_divCashAmount").css("display", "block");
                $("#ctl00_C1_divChequeAmount").css("display", "none");
                $("#ctl00_C1_divchequeDetails").css("display", "none");
                $("#ctl00_C1_txtChequeNo").val("");
                $("#ctl00_C1_txtChequeDate").val("");
                //$("#ctl00_C1_txtBankName").val("");
                //$("#ctl00_C1_txtBranchName").val("");
                $("#ctl00_C1_txtChequeAmount").val("0.00");
                $("#ctl00_C1_txtCashAmount").val(document.getElementById("ctl00_C1_txtNetAmount").value);
            }
            else if (value == "Cash+Cheque") {
                $("#ctl00_C1_divCashAmount").css("display", "block");
                $("#ctl00_C1_divChequeAmount").css("display", "block");
                $("#ctl00_C1_divchequeDetails").css("display", "block");
                $("#ctl00_C1_txtChequeAmount").val("0.00");
                $("#ctl00_C1_txtCashAmount").val("0.00");
            }
            else if (value == "Cheque") {
                $("#ctl00_C1_divCashAmount").css("display", "none");
                $("#ctl00_C1_divChequeAmount").css("display", "block");
                $("#ctl00_C1_divchequeDetails").css("display", "block");
                $("#ctl00_C1_txtCashAmount").val("0.00");
                $("#ctl00_C1_txtChequeAmount").val(document.getElementById("ctl00_C1_txtNetAmount").value);
            }
            else {
                $("#ctl00_C1_divCashAmount").css("display", "none");
                $("#ctl00_C1_divChequeAmount").css("display", "none");
                $("#ctl00_C1_divchequeDetails").css("display", "none");
                $("#ctl00_C1_txtChequeNo").val("");
                $("#ctl00_C1_txtChequeDate").val("");
                //$("#ctl00_C1_txtBankName").val("");
                //$("#ctl00_C1_txtBranchName").val("");
                $("#ctl00_C1_txtChequeAmount").val("0.00");
                $("#ctl00_C1_txtCashAmount").val("0.00");
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

            var tot = parseFloat(ChequeAmount) + parseFloat(CashAmount);
            if (tot < NetAmount) {
                alert("Cash/Cheque amount should be equal to Net Amount.");
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

            //if ($("#ctl00_C1_txtBankName").val() == "") {
            //    $("#spanBankName").html("*");
            //    myresult = false;
            //}

            //if ($("#ctl00_C1_txtBranchName").val() == "") {
            //    $("#spanBranchName").html("*");
            //    myresult = false;
            //}

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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <!-- Basic inputs -->
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
                                <asp:TextBox ID="txtStudentId" class="form-control" runat="server" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtStudentId"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                            <%--<label class="col-sm-2 control-label">Student Name</label>--%>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtName" class="form-control" ReadOnly="true"  placeholder="Student Name" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="txtDueDate" class="form-control datepicker" runat="server"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDueDate"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">

                            <asp:GridView ID="DataDisplay" runat="server" class="table table-bordered table-check" AutoGenerateColumns="false"
                                Width="100%"
                                ShowFooter="True" DataKeyNames="RowID">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("RowId") %>' />
                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("ProductId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeeType" runat="server" Text='<%#Eval("ProductName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Type" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductType" runat="server" Text='<%#Eval("ProductType") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" Width="50px" Text='<%#Eval("Qty") %>' runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server" CommandName="updateqty" Text="Update Total"></asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="rdbselect" Checked="true" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle Font-Bold="true" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    No Fee Record!
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Receipt Total Amount</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtReceiptAmt" class="form-control" Enabled="false" runat="server">0</asp:TextBox>
                            </div>

                            <label class="col-sm-2 control-label">Discount</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtDiscount" class="form-control" runat="server" onchange="calculateNetAmount();">0</asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label"><b>Net Amount</b></label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtNetAmount" class="form-control" ReadOnly="true" runat="server">0</asp:TextBox>
                            </div>
                        </div>

                        <div class="form-actions text-right">
                            <a id="btnPay" runat="server" class="btn btn-primary" visible="false" onclick="showPayDiv();">Pay Receipt</a>
                            <asp:Button ID="btnPreview" runat="server" class="btn btn-primary" OnClientClick="aspnetForm.target ='_blank';" Text="Preview" Visible="false" />
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Generate Receipt" />
                            <input type="hidden" id="hdnReceiptId" runat="server" />

                        </div>

                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:AsyncPostBackTrigger ControlID="txtStudentId"
                    EventName="SelectedIndexChanged" />--%>
                <%--    <asp:PostBackTrigger ControlID="btnAddPDC" />--%>
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <asp:PostBackTrigger ControlID="btnPreview" />
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
                                <label class="col-sm-2 control-label">ChequeAmount</label>
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
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtremarkPaid" class="form-control" runat="server"></asp:TextBox>

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
    <script type="text/javascript">        //On UpdatePanel Refresh 
        var prm = Sys.WebForms.PageRequestManager.getInstance(); if (prm != null) { prm.add_endRequest(function (sender, e) { if (sender._postBackSettings.panelsToUpdate != null) { $(".select-search").select2(); } }); };

    </script>
    <!-- /form components -->

</asp:Content>

