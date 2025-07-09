<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="PayScaleManager2.aspx.vb" Inherits="Admin_PayScaleManager2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {

            // Start DA
            $('#ddlDA').bind('change', function () {

                if ($("#ddlDA option:selected").val() == 'Per') {
                    $("#txtDAPer").show();
                    $("#txtDAFix").hide();
                }
                else {
                    $("#txtDAFix").show();
                    $("#txtDAPer").hide();
                }

            });
            $('#txtDAPer').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtDAFix').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            //End DA

            //Start HRA
            $('#ddlHRA').bind('change', function () {

                if ($("#ddlHRA option:selected").val() == 'Per') {
                    $("#txtHRAFPer").show();
                    $("#txtHRAFix").hide();
                }
                else {
                    $("#txtHRAFix").show();
                    $("#txtHRAFPer").hide();
                }

            });
            $('#txtHRAFPer').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtHRAFix').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            //End HRA

            //Start OA
            $('#ddlOtherAllo').bind('change', function () {

                if ($("#ddlOtherAllo option:selected").val() == 'Per') {
                    $("#txtoThAlloPer").show();
                    $("#txtoThAlloFix").hide();
                }
                else {
                    $("#txtoThAlloFix").show();
                    $("#txtoThAlloPer").hide();
                }

            });
            $('#txtoThAlloPer').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtoThAlloFix').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            //End OA

            //Start PF
            $('#ddlPF').bind('change', function () {

                if ($("#ddlPF option:selected").val() == 'Per') {
                    $("#txtPFPerBasic").show();
                    $("#txtPFPerDA").show();
                    $("#txtPFFix").hide();
                }
                else {
                    $("#txtPFFix").show();
                    $("#txtPFPerBasic").hide();
                    $("#txtPFPerDA").hide();
                }

            });
            $('#txtPFPerBasic').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtPFPerBasic').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtPFFix').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            //End PF

            //Start TDS
            $('#ddlTDS').bind('change', function () {

                if ($("#ddlTDS option:selected").val() == 'Per') {
                    $("#txtTDSPer").show();
                    $("#txtTDSFix").hide();
                }
                else {
                    $("#txtTDSFix").show();
                    $("#txtTDSPer").hide();

                }

            });
            $('#txtTDSPer').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtTDSFix').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            //End TDS

            //Start OD
            $('#ddlOD').bind('change', function () {

                if ($("#ddlOD option:selected").val() == 'Per') {
                    $("#txtODPer").show();
                    $("#txtODFix").hide();
                }
                else {
                    $("#txtODFix").show();
                    $("#txtODPer").hide();

                }

            });
            $('#txtODPer').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtODFix').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            //End OD

            //Start ESI
            $('#ddlESI').bind('change', function () {

                if ($("#ddlESI option:selected").val() == 'Per') {
                    $("#txtESIPerBasic").show();
                    $("#txtESIPerDA").show();
                    $("#txtESIFix").hide();
                }
                else {
                    $("#txtESIFix").show();
                    $("#txtESIPerBasic").hide();
                    $("#txtESIPerDA").hide();

                }

            });

            $('#txtESIPerBasic').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtESIPerDA').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    alert("Please Insert numeric & Decimal only");
                }

            });
            $('#txtESIFix').keyup(function () {
                if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                    //  $('#txtESIFix').val("");
                    alert("Please Insert numeric & Decimal only");
                }

            });
            //End ESI

            $("body").on("click", "#btnSubmit", function () {
                debugger;
                var DAPer = "";
                var DAFix = "";

                var HRAPer = "";
                var HRAFix = "";

                var OAPer = "";
                var OAFix = "";

                var PFPer = "";
                var PFDA = "";
                var PFFix = "";

                var TDSPer = "";
                var TDSFix = "";

                var ODPer = "";
                var ODFix = "";

                var ESIPer = "";
                var ESIDA = "";
                var ESIFix = "";


                //Starrt DA
                if ($("#ddlDA option:selected").val() == "Per")
                {
                    DAPer= $("#txtDAPer").val();
                }
                else
                {
                    DAPer = 0;
                }
                if ($("#ddlDA option:selected").val() == "Fix") {
                    DAFix = $("#txtDAFix").val();
                }
                else {
                    DAFix = 0;
                }
                // End DA

                //Start HRA
                if ($("#ddlHRA option:selected").val() == "Per") {
                    HRAPer = $("#txtHRAPer").val();
                }
                else {
                    HRAPer = 0;
                }
                if ($("#ddlHRA option:selected").val() == "Fix") {
                    HRAFix = $("#txtHRAFix").val();
                }
                else {
                    HRAFix = 0;
                }
                // End HRA

                //Start OA
                if ($("#ddlOtherAllo option:selected").val() == "Per") {
                    OAPer = $("#txtoThAlloPer").val();
                }
                else {
                    OAPer = 0;
                }
                if ($("#ddlOtherAllo option:selected").val() == "Fix") {
                    OAFix = $("#txtoThAlloFix").val();
                }
                else {
                    OAFix = 0;
                }
                //End OA

                //Start PF
                if ($("#ddlPF option:selected").val() == "Per") {
                    PFPer = $("#txtPFPerBasic").val();
                    PFDA = $("#txtPFPerDA").val();
                }
                else {
                    PFPer = 0;
                    PFDA = 0;
                }
                if ($("#ddlPF option:selected").val() == "Fix") {
                    PFFix = $("#txtPFFix").val();
                }
                else {
                    PFFix = 0;
                }
                //End PF

                //Start TDS
                if ($("#ddlTDS option:selected").val() == "Per") {
                    TDSPer = $("#txtTDSPer").val();
                }
                else {
                    TDSPer = 0;
                    
                }
                if ($("#ddlTDS option:selected").val() == "Fix") {
                    TDSFix = $("#txtTDSFix").val();
                }
                else {
                    TDSFix = 0;
                }
                //End TDS


                //Start OD
                if ($("#ddlOD option:selected").val() == "Per") {
                    ODPer = $("#txtODPer").val();
                }
                else {
                    ODPer = 0;

                }
                if ($("#ddlOD option:selected").val() == "Fix") {
                    ODFix = $("#txtODFix").val();
                }
                else {
                    ODFix = 0;
                }
                //End OD

                //Start ESI
                if ($("#ddlESI option:selected").val() == "Per") {
                    ESIPer = $("#txtESIPerBasic").val();
                    ESIDA = $("#txtESIPerDA").val();
                }
                else {
                    ESIPer = 0;
                    ESIDA = 0;

                }
                if ($("#ddlESI option:selected").val() == "Fix") {
                    ESIFix = $("#txtESIFix").val();
                }
                else {
                    ESIFix = 0;
                }
                //End ESI


                $.ajax({
                    type: "POST",
                    url: "PayScaleManager.aspx/PayScaleManager",  //pageName.aspx/MethodName
                    data: '{PayScaleName:"' + $("#txtPayScaleName").val() + '",DA:"' + $("#ddlDA option:selected").val() + '",DAPer:"' + DAPer + '",DAFix:"' + DAFix + '",HRA:"' + $("#ddlHRA option:selected").val() + '",HRAPer:"' + HRAPer + '",HRAFix:"' + HRAFix + '",OtherAllowance:"' + $("#ddlOtherAllo option:selected").val() + '",OAPer:"' + OAPer + '",OAFix:"' + OAFix + '",PF:"' + $("#ddlPF option:selected").val() + '",PFPer:"' + PFPer + '",PFDA:"' + PFDA + '",PFFix:"' + PFFix + '",TDS:"' + $("#ddlTDS option:selected").val() + '",TDSPer:"' + TDSPer + '",TDSFix:"' + TDSFix + '",OD:"' + $("#ddlOD option:selected").val() + '",ODPer:"' + ODPer + '",ODFix:"' + ODFix + '",ESI:"' + $("#ddlESI option:selected").val() + '",ESIPer:"' + ESIPer + '",ESIDA:"' + ESIDA + '",ESIFix:"' + ESIFix + '",PaidLeaves:"' + $("#txtPaidLeaves").val() + '"}',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                    },

                });
            });
        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">

    <!-- Form components -->
    <div class="form-horizontal">



        <!-- Basic inputs -->
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Pay Scale Manager</h6>
            </div>
            <div class="panel-body">

                <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                <asp:HiddenField ID="hfid" runat="server" />
                <asp:Literal ID="litmsg" runat="server"></asp:Literal>

                  <div class="form-group">
                    <label class="col-sm-2 control-label">Pay Scale Title</label>
                    <div class="col-sm-3">
                       <input type="text" id="txtPayScaleName" placeholder="Title" class="form-control" />
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Driving Allowance (DA)</label>
                    <div class="col-sm-3">
                        <select id="ddlDA" class="select">
                            <option value="PER">Percentage</option>
                            <option value="FIX">Fix Amount</option>
                        </select>
                    </div>
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtDAFix" placeholder="DA Fix" class="form-control" style="display: none;" />


                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtDAPer" placeholder="DA Percentage" class="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">House Rent Allowance (HRA)</label>
                    <div class="col-sm-3">
                        <select id="ddlHRA" class="select">
                            <option value="PER">Percentage</option>
                            <option value="FIX">Fix Amount</option>
                        </select>

                    </div>
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtHRAFix" placeholder="HRA Fix" class="form-control" style="display: none;" />


                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtHRAPer" placeholder="HRA Percentage" class="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Other Allowance</label>
                    <div class="col-sm-3">
                        <select id="ddlOtherAllo" class="select">
                            <option value="PER">Percentage</option>
                            <option value="FIX">Fix Amount</option>
                        </select>

                    </div>
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtoThAlloFix" placeholder="Other Allowance Percentage" class="form-control" style="display: none;" />


                    </div>
                    <div class="col-sm-3">

                        <input type="text" id="txtoThAlloPer" placeholder="Other Allowance Percentage" class="form-control" />
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-2 control-label">Provident Fund (PF)</label>
                    <div class="col-sm-3">
                        <select id="ddlPF" class="select">
                            <option value="PER">Percentage</option>
                            <option value="FIX">Fix Amount</option>
                        </select>

                    </div>
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtPFFix" class="form-control" placeholder="PF Fix" style="display: none;" />

                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtPFPerBasic" placeholder="PF Percentage" class="form-control" />
                        <input type="text" id="txtPFPerDA" placeholder="PF DA" class="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Tax Deduction (TDS)</label>
                    <div class="col-sm-3">
                        <select id="ddlTDS" class="select">
                            <option value="PER">Percentage</option>
                            <option value="FIX">Fix Amount</option>
                        </select>
                    </div>
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtTDSFix" placeholder="TDS Fix" class="form-control" style="display: none;" />

                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtTDSPer" placeholder="TDS Percentage" class="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Other Deduction (OD)</label>
                    <div class="col-sm-3">
                        <select id="ddlOD" class="select">
                            <option value="PER">Percentage</option>
                            <option value="FIX">Fix Amount</option>
                        </select>


                    </div>
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtODFix" placeholder="OD Fix" class="form-control" style="display: none;" />


                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtODPer" placeholder="OD Percentage" class="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Employee's State Insurance (ESI)</label>
                    <div class="col-sm-3">
                        <select id="ddlESI" class="select">
                            <option value="PER">Percentage</option>
                            <option value="FIX">Fix Amount</option>
                        </select>
                    </div>
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtESIFix" placeholder="ESI Fix" class="form-control" style="display: none;" />

                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtESIPerBasic" placeholder="ESI Percentage" class="form-control" />
                        <input type="text" id="txtESIPerDA" placeholder="ESI DA" class="form-control" />
                    </div>

                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Paid Leaves</label>
                    <div class="col-sm-3">
                        <input type="text" id="txtPaidLeaves" placeholder="Paid Leaves" class="form-control" />

                    </div>

                </div>



                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>

                    <div class="col-sm-3">
                    </div>

                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <input type="button" id="btnSubmit" class="btn btn-primary" value="Submit" />

                    </div>
                </div>



            </div>
        </div>



          <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Pay Scale Master</h6>
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
                        <asp:BoundField DataField="PayScaleTitle" HeaderText="Title"></asp:BoundField>
                        <asp:BoundField DataField="DA" HeaderText="Driving Allowance (DA)"></asp:BoundField>
                        <asp:BoundField DataField="HRA" HeaderText="House Rent Allowance (HRA)"></asp:BoundField>
                        <asp:BoundField DataField="OA" HeaderText="Other Allowance"></asp:BoundField>
                        <asp:BoundField DataField="PF" HeaderText="Provident Fund (PF)"></asp:BoundField>
                        <asp:BoundField DataField="TDS" HeaderText="Tax Deduction (TDS)"></asp:BoundField>
                        <asp:BoundField DataField="OD" HeaderText="Other Deduction (OD)"></asp:BoundField>
                        <asp:BoundField DataField="ESI" HeaderText="Employee's State Insurance (ESI)"></asp:BoundField>
                        <asp:BoundField DataField="PaidLeaves" HeaderText="Paid Leaves"></asp:BoundField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>

    <!-- /form components -->
</asp:Content>

