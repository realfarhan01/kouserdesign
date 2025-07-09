<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="CustomerRegistration.aspx.vb" Inherits="Admin_CustomerRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .select {
            width: 200px;
        }

        .select-full {
            width: 200px;
        }

        .select-search {
            width: 200px;
        }
    </style>
    <script type="text/javascript">
        function isValidForm(source, arguments) {

            if ($("#ctl00_C1_hfId").val().trim() == "") {
                if (arguments.Value != "") {
                    arguments.IsValid = true;
                } else {
                    arguments.IsValid = false;
                }
            }
            else {
                arguments.IsValid = true;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <!-- Form components -->

    <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="false" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div id="divLoading" class="progressdiv">
                <img src="images/Loader.gif" alt="Loading, please wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="form-horizontal">



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h6 class="panel-title">Registration Details</h6>
                    </div>
                    <div class="panel-body">
                        <asp:HiddenField ID="hfId" runat="server" />
                        <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>

                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlSession" class="select" runat="server">
                                </asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label">Customer Id </label>
                            <div class="col col-lg-2">
                                <asp:TextBox ID="txtCustomerId" class="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label ">Register Date </label>
                            <div class="col col-lg-2">
                                <asp:TextBox ID="txtRegdate" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="txtRegdate"
                                    ErrorMessage="*Required" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h6 class="panel-title">Customer's Details</h6>
                    </div>
                    <div class="panel-body">


                        <div class="form-group">

                            <label class="col-sm-2 control-label">Name </label>
                            <div class="col col-lg-4">
                                <asp:TextBox ID="txtName" class="form-control" placeholder="Customer Name" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtName"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*Required</asp:RequiredFieldValidator>
                            </div>
                            <label class="col-sm-2 control-label">Date Of Birth </label>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtDOB" class="form-control datepicker" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Email</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>



                            <label class="col-sm-2 control-label">Mobile</label>
                            <div class="col col-lg-2">
                                <asp:TextBox ID="txtMobile" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">

                            <label class="col-sm-2 control-label">Address </label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtaddress" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtaddress"
                                    ErrorMessage="*" ValidationGroup="UserRegistration">*Required</asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">

                            <label class="col-sm-2 control-label">State </label>
                            <div class="col col-lg-4">
                                <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" class="select-search" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-1 control-label">City </label>
                            <div class="col col-lg-3">
                                <asp:DropDownList ID="ddlCity" AppendDataBoundItems="true" class="select-search" runat="server">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="form-group">

                            <label class="col-sm-2 control-label">Pin Code </label>
                            <div class="col col-lg-3">
                                <asp:TextBox ID="txtPincode" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col col-lg-1">
                            </div>
                            <label class="col-sm-1 control-label">GST No.</label>
                            <div class="col col-lg-3">
                                <asp:TextBox ID="txtGST" class="form-control" runat="server"></asp:TextBox>
                            </div>


                        </div>
                        <div class="form-group">

                            <label class="col-sm-2 control-label">Status </label>
                            <div class="col col-lg-4">
                                <asp:DropDownList ID="ddlStatus" class="select" runat="server">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Deactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            </div>

                        </div>
                    </div>
                </div>




                <div class="form-actions text-right">
                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />

                </div>

                <!-- /form components -->







            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance(); if (prm != null) { prm.add_endRequest(function (sender, e) { if (sender._postBackSettings.panelsToUpdate != null) { $(".select-search").select2(); $(".select").select2(); $(".select-full").select2(); } }); }; </script>

</asp:Content>

