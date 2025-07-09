<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ReceiptGenerateProspectus.aspx.vb" Inherits="Admin_ReceiptGenerateProspectus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Make a New Prospectus Sale Receipt</h6>
            </div>
            <div class="panel-body">
                <asp:HiddenField ID="hfId" runat="server" />

                <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Receipt No. </label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtReceiptno" class="form-control" ReadOnly="true"  runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReceiptno"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Form No </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtFormNo" class="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtFormNo"
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
                    <label class="col-sm-2 control-label">Receipt Date </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtDate" class="form-control datepicker"  runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDate"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Receipt Amount </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtAmount" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAmount"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>

                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Student Name</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtName" placeholder="First Name" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtLastName" placeholder="Last Name" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLastName"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Class</label>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlClass" InitialValue="0"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Father Name</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtFatherName" class="form-control" placeholder="First Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFatherName"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtFLastName" class="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFatherName"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                    <label class="col-sm-2 control-label">Mobile No</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtMobileNo" class="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobileNo"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Remark </label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtRemark" class="form-control" runat="server"></asp:TextBox>

                    </div>
                </div>
                <div class="form-actions text-right">
                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Generate Receipt" />
                    <asp:Button ID="btnPreview" runat="server" class="btn btn-primary" OnClientClick="aspnetForm.target ='_blank';" Text="Print Receipt" Visible="false" />
                    <input type="hidden" id="hdnReceiptId" runat="server" />
                </div>

            </div>
        </div>
    </div>
    
</asp:Content>

