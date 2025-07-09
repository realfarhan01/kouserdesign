<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="PayScaleManager.aspx.vb" Inherits="Admin_PayScaleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .select2-container {
            width: 100% !important;
        }
    </style>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Pay Scale Manager</h6>
            </div>
           
            <div class="panel-body">
              
                <asp:HiddenField ID="hfid" runat="server" />
                <asp:Literal ID="litmsg" runat="server"></asp:Literal>

                  <div class="form-group">
                    <label class="col-sm-2 control-label">PayScaleName </label>
                    <div class="col-sm-4">
                       <input type="text" id="txtPayScaleName" runat="server"  placeholder="Name"  class="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Driving Allowance (DA) </label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlDA" class="select" AutoPostBack="true"  runat="server">
                            <asp:ListItem Value="PER">Percentage</asp:ListItem>
                            <asp:ListItem Value="FIX">Fix Amount</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-2 control-label"><asp:Literal ID="LitDA" runat="server"></asp:Literal></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtDAFix" runat="server" placeholder="DA Fix" class="form-control" />


                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtDAPer" runat="server" placeholder="DA Percentage" class="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">House Rent Allowance (HRA)</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlHRA" class="select" AutoPostBack="true"  runat="server">
                            <asp:ListItem Value="PER">Percentage</asp:ListItem>
                            <asp:ListItem Value="FIX">Fix Amount</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-2 control-label"><asp:Literal ID="LitHRA" runat="server"></asp:Literal></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtHRAFix" runat="server" placeholder="HRA Fix" class="form-control" />


                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtHRAPer" runat="server" placeholder="HRA Percentage" class="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Other Allowance</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlOtherAllo" class="select" AutoPostBack="true"  runat="server">
                            <asp:ListItem Value="PER">Percentage</asp:ListItem>
                            <asp:ListItem Value="FIX">Fix Amount</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-2 control-label"><asp:Literal ID="LitOA" runat="server"></asp:Literal></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtOthAlloFix" runat="server" placeholder="Other Allowance Percentage" class="form-control" />


                    </div>
                    <div class="col-sm-3">

                        <input type="text" id="txtOthAlloPer" runat="server" placeholder="Other Allowance Percentage" class="form-control" />
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-2 control-label">Provident Fund (PF)</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlPF" class="select" AutoPostBack="true"  runat="server">
                            <asp:ListItem Value="PER">Percentage</asp:ListItem>
                            <asp:ListItem Value="FIX">Fix Amount</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-2 control-label"><asp:Literal ID="LitPF" runat="server"></asp:Literal></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtPFFix" runat="server" class="form-control" placeholder="PF Fix" />

                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtPFPerBasic" runat="server" placeholder="PF Percentage" class="form-control" />
                        <input type="text" id="txtPFPerDA" runat="server" placeholder="PF DA" class="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Tax Deduction (TDS)</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlTDS" class="select" AutoPostBack="true"  runat="server">
                            <asp:ListItem Value="PER">Percentage</asp:ListItem>
                            <asp:ListItem Value="FIX">Fix Amount</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-2 control-label"><asp:Literal ID="LitTDS" runat="server"></asp:Literal></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtTDSFix" runat="server" placeholder="TDS Fix" class="form-control" />

                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtTDSPer" runat="server" placeholder="TDS Percentage" class="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Other Deduction (OD)</label>
                    <div class="col-sm-3">
                        <select id="ddlOD" runat="server" class="select">
                            <%--<option value="PER" >Percentage</option>--%>
                            <option value="FIX" selected="selected">Fix Amount</option>
                        </select>


                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-2 control-label">Fix Amount</label>
                    <div class="col-sm-3">
                        <input type="text" id="txtODFix" runat="server" placeholder="OD Fix" class="form-control" />
                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtODPer" runat="server" placeholder="OD Percentage" class="form-control" style="display:none"  />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Employee's State Insurance (ESI)</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlESI" class="select" AutoPostBack="true"  runat="server">
                            <asp:ListItem Value="PER">Percentage</asp:ListItem>
                            <asp:ListItem Value="FIX">Fix Amount</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-2 control-label"><asp:Literal ID="LitESI" runat="server"></asp:Literal></label>
                    <div class="col-sm-3">
                        <input type="text" id="txtESIFix" runat="server" placeholder="ESI Fix" class="form-control" />

                    </div>
                    <div class="col-sm-3">
                        <input type="text" id="txtESIPerBasic" runat="server" placeholder="ESI Basic Percentage" class="form-control" />
                        <input type="text" id="txtESIPerOD" runat="server" placeholder="ESI OD Percentage" class="form-control" />
                    </div>

                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Paid Leaves</label>
                    <div class="col-sm-3">
                        <input type="text" id="txtPaidLeaves" runat="server" placeholder="Paid Leaves" class="form-control" />
                    </div>

                </div>


                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>

                    <div class="col-sm-3">
                    </div>

                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-3">
                        <asp:Button ID="btnSubmit" class="btn btn-primary"  runat="server" Text="Submit" />
                    </div>
                </div>



            </div>
        </div>

        <input type="hidden" id="hdnRowID" runat="server" />
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Pay Scale Master</h6>
                <div class="form-actions text-right">
                    <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Export to Excel" Visible="false"></asp:LinkButton>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="DataDisplay" class="table table-striped table-bordered table-check" AutoGenerateColumns="false" DataKeyNames="PSID" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <a id="lnkUpdate" data-id='<%# Eval("PSID")%>' onclick="showPopUp(this.id);" runat="server" title="Edit"><i class="fa fa-pencil-square-o"></i></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <a id="lnkDelete" data-id='<%# eval("PSID") %>' onclick="showPopUp2(this.id);" runat="server" title="Delete"><i class="fa fa-trash-o"></i></a>
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
    </ContentTemplate>
    </asp:UpdatePanel>
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
                                <input type="hidden" id="Hidden1" runat="server" />
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
                                <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnDelete" Text="Delete" ValidationGroup="DeleteAuthentication" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    
    <script type="text/javascript">        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance(); if (prm != null) { prm.add_endRequest(function (sender, e) { if (sender._postBackSettings.panelsToUpdate != null) { $(".select").select2(); } }); }; </script>

    <!-- /form components -->
</asp:Content>

