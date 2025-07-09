<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="EmployeeSalary.aspx.vb" Inherits="Admin_EmployeeSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .select2-container {
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

     <!-- Form components -->
    <div class="form-horizontal">
        
    <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="false" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div id="divLoading" class="progressdiv">
            <img src="images/Loader.gif" alt="Loading, please wait" />
        </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>


        <!-- Basic inputs -->
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Employee Pay Scale Manager</h6>
            </div>
           
            <div class="panel-body">
              
                <asp:HiddenField ID="hfid" runat="server" />
                <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                <%--<div class="form-group">
                    <label class="col-sm-2 control-label">EmployeeID </label>
                    <div class="col-sm-3">

                       <input type="text" id="txtEmployeeID" placeholder="EmployeeID" class="form-control" />
                    </div>
                    <label class="col-sm-2 control-label">Employee Name </label>
                    <div class="col-sm-3">

                       <input type="text" id="txtEmployeeName" placeholder="EmployeeName" class="form-control" />
                    </div>
                    
                </div>--%>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Search Employee</label>
                    <div class="col-sm-8">
                    <asp:DropDownList ID="ddlsearch" runat="server" class="select-search" AutoPostBack="true" AppendDataBoundItems="true" Width="100%">
                        <asp:ListItem Value="">Search</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                </div>
                  <div class="form-group">
                    <label class="col-sm-2 control-label">PayScaleName </label>
                    <div class="col-sm-3">
                       <input type="text" id="txtPayScaleName" runat="server"  placeholder="Name" readonly="readonly"  class="form-control" />
                    </div>
                    <label class="col-sm-1 control-label"></label>
                     <label class="col-sm-2 control-label">Basic Salary </label>
                    <div class="col-sm-3">
                       <input type="text" id="txtBasicSalary" runat="server" placeholder="Salary" readonly="readonly" class="form-control" />
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
                    <label class="col-sm-2 control-label">Advance Amount</label>
                    <div class="col-sm-3">
                        <input type="text" id="txtAdvance" runat="server" placeholder="Advance Amount" class="form-control" />

                    </div>
                    <label class="col-sm-1 control-label"></label>
                    <label class="col-sm-1 control-label">Convence Charge</label>
                    <div class="col-sm-3">
                        <input type="text" id="txtConvenceCharge" runat="server" placeholder="Convence Charge" class="form-control" />

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
    </ContentTemplate>
    </asp:UpdatePanel>

    </div>
    
    <script type="text/javascript">        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance(); if (prm != null) { prm.add_endRequest(function (sender, e) { if (sender._postBackSettings.panelsToUpdate != null) { $(".select").select2(); } }); }; </script>

    <!-- /form components -->
</asp:Content>

