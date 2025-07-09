<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="PDCMaster.aspx.vb" Inherits="Admin_PDCMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <div class="form-horizontal">
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
                        <h6 class="panel-title">Student PDC Master</h6>
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
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtStudentId" class="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtStudentId"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                            <label class="col-sm-2 control-label">Student Name</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-2 control-label">Class</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtClass" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClass"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Tuition Fee Concession(%)</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtConcession" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConcession"
                                    ErrorMessage="*" ValidationGroup="Concession">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-4">
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Concession" class="btn btn-primary" Text="Update Concession" />
                            </div>
                        </div>
                        <div class="form-group">
                            <%--onclick="showPopUpByType(1)" --%>
                            <a id="lnkAddPDC" runat="server" class="button-a blue" visible="true" data-toggle="modal" data-target="#divaddMorePDC" style="margin-bottom: 5px; float: right; margin-right: 5px; text-decoration: underline;">Add More PDC</a>
                            <a id="lnkAddConveyance" runat="server" class="button-a blue" visible="true" data-toggle="modal" data-target="#divaddMoreConveyance" style="margin-bottom: 5px; float: right; margin-right: 10px; text-decoration: underline;">Add Conveyance Fees</a>
                            <input type="hidden" id="hidPDCType" runat="server" value="0" />
                            <asp:Button ID="btnBindPdcORConveyance" runat="server" Style="display: none;" />

                            <asp:GridView ID="DataDisplay" runat="server" class="table table-bordered table-check" AutoGenerateColumns="false"
                                Width="100%" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="Record Not Fount !"
                                ShowFooter="True" DataKeyNames="PDCID">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Session" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSession" CssClass="donotchange" runat="server" Text='<%#Eval("SchoolSession") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fee Type" HeaderStyle-Width="40%" ItemStyle-Width="35%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeeType" CssClass="donotchange" runat="server" Text='<%#Eval("FeeType") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Due Date" HeaderStyle-Width="7%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDueDate" runat="server" Text='<%#Eval("DueDate")%>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="7%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Late Fee" HeaderStyle-Width="7%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLateFee" runat="server" Text='<%#Eval("LateFee") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderStyle-CssClass="nosorting">
                            <ItemTemplate>
                                <a id="lnkDelete" data-id='<%# Eval("PDCId")%>' onclick="showAuthentication(this.id,'delete');" runat="server" title="Delete"><i class="fa fa-trash-o"></i></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                                
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal fade" id="divaddMorePDC" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title" id="addMoreHead" runat="server">Add More PDC </h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Generate Admission Fees</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlisAdmissionFeesFree" class="select" runat="server">
                                                        <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="1">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="col-sm-2 control-label">Generate Tution Fees</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlisTuitionFeesFree" class="select"  runat="server">
                                                        <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="1">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                                <div class="form-group" id="divsession" runat="server">
                                                    <label class="col-sm-2 control-label">Class</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="ddlClass" class="select" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-sm-2 control-label">Session</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="ddlSession" class="select" runat="server">
                                                            <asp:ListItem Value="2017-2018" Selected="True">2017-2018</asp:ListItem>
                                                            <asp:ListItem Value="2018-2019">2018-2019</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                       <%-- <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnAddPDC" onclick="showAuthentication('updateTuition');" Text="Add to List" />--%>
                                        
                                         <a class="btn btn-primary"  onclick="showAuthentication(this.id,'updateTuition');"  id="A1">Add to List</a> 

                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>
                        <div class="modal fade" id="divaddMoreConveyance" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Add ConveyanceFee </h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Transport Facility</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlTrasport" class="select" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTrasport"
                                                        ErrorMessage="*Required" ValidationGroup="AddConveyance">*</asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-sm-2 control-label">Pickup Location </label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlPickupLocation" class="select-search" AppendDataBoundItems="true" runat="server">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                       <%-- <asp:Button type="button" runat="server" class="btn btn-primary"  onclick="showAuthentication('updateConveyance');" ValidationGroup="AddConveyance" ID="btnAddConveyance" Text="Add to List" />--%>
                                         <a class="btn btn-primary"  onclick="showAuthentication(this.id,'updateConveyance');"  id="btnAddConveyance1">Add to List</a> 
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
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
                                                        <asp:TextBox ID="txtMasterPass" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1Paid" runat="server" InitialValue="" ControlToValidate="txtMasterPass"
                                                            ErrorMessage="*" ValidationGroup="DeleteAuthentication">*</asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button type="button" runat="server" class="btn btn-primary" ID="btnProceed" Text="Submit" ValidationGroup="DeleteAuthentication" />
                                                    <input type="hidden" id="hdnTypeID" runat="server" />
                                                     <input type="hidden" id="hdnRowID" runat="server" />
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <input type="hidden" id="hidType" runat="server" />
                        <input type="hidden" id="hidRo" runat="server" />

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlsearch"
                    EventName="SelectedIndexChanged" />
                <asp:PostBackTrigger ControlID="btnProceed" />
                <%--<asp:PostBackTrigger ControlID="btnAddConveyance" />--%>
                <asp:PostBackTrigger ControlID="btnBindPdcORConveyance" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function showPDCPopup() {
            $("#divaddMorePDC").modal("toggle");
        }
        function showConvPopup() {
            $("#divaddMoreConveyance").modal("toggle");
        }
        function showAuthPopup() {
            $("#divDeleteAuth").modal("toggle");
        }
        function showAuthentication(id,type)
        {
            if (type == "delete") {
                var RowID2 = $("#" + id).attr("data-id");
                $("#ctl00_C1_hdnRowID").val(RowID2);
            }
           
            $("#ctl00_C1_hidType").val(type);
            $("#divDeleteAuth").modal("toggle");

        }
    </script>
    <script type="text/javascript">        //On UpdatePanel Refresh 
        var prm = Sys.WebForms.PageRequestManager.getInstance(); if (prm != null) {
            prm.add_endRequest(function (sender, e)
            { if (sender._postBackSettings.panelsToUpdate != null) { $(".select-search").select2(); $(".select").select2(); $(".select-full").select2(); } });
        }; </script>

</asp:Content>

