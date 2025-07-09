<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="OutStandingFeesReport.aspx.vb" Inherits="Admin_OutStandingFeesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .select-search {
            width:100% !important;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
      
    <div class="panel panel-default">

        <div class="panel-heading">
            <h6 class="panel-title">Outstanding Fees Report</h6>
             <div class="form-actions text-right">
                    <asp:LinkButton id="btnExport" runat="server" cssclass="btn btn-primary" Text="Export to Excel" Visible="false"></asp:LinkButton>
                </div>
        </div>
           <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="false" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div id="divLoading" class="progressdiv">
                    <img src="images/Loader.gif" alt="Loading, please wait" />
                </div>

            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always">
            <ContentTemplate>
        <div class="table-responsive">
            <table class="table">
                <tfoot>
                    <tr>


                        <th>
                            <asp:DropDownList ID="ddlClass" class="select-search" style="width:100% !important;" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:DropDownList ID="ddlStudent" AppendDataBoundItems="true" class="select-search" style="width:100%;" runat="server">
                                <asp:ListItem Value="">--Select Student--</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <th>
                            <asp:DropDownList ID="ddlFeeType" AppendDataBoundItems="true" class="select-search" style="width:100%;" runat="server">
                                <asp:ListItem Value="0">--Select Fee Type--</asp:ListItem>
                                <asp:ListItem Value="1">Admission</asp:ListItem>
                                <asp:ListItem Value="2">Tuition Fee</asp:ListItem>
                                <asp:ListItem Value="3">Conveyance</asp:ListItem>
                            </asp:DropDownList>

                        </th>
                    </tr>
                    <tr>

                        <th>
                            <asp:TextBox ID="txtFromDate" runat="server" placeHolder="From Date" class="form-control datepicker"></asp:TextBox></th>
                        <th>
                            <asp:TextBox ID="txtToDate" runat="server" placeHolder="To Date" class="form-control datepicker"></asp:TextBox></th>
                        <th>
                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" /></th>

                    </tr>
                </tfoot>
            </table>
            <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="true" runat="server">
            </asp:GridView>
        </div>
            </ContentTemplate>
                </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
     
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(".select-search").select2();
                }
            });
        };
    </script>

</asp:Content>

