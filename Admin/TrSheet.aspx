<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="TrSheet.aspx.vb" Inherits="Admin_TrSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("Idcontents").innerHTML;
            var printWindow = window.open();
            printWindow.document.write('<html><head>');
            printWindow.document.write('<style type="text/css">input{display:none;}.tds {text-align: center;}.tdPadding {padding-left: 10px;}#tblRecords td {text-align: center;}#tblRecords2 td {text-align: center;}  td {padding: 1px; }.tdIns{text-align:center;} table td{font-size:medium}</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
    <style type="text/css">
        .tds {
            text-align: center;
        }

        #tblRecords td {
            text-align: center;
        }

        #tblRecords2 td {
            text-align: center;
        }

        td {
            padding: 10px;
        }

        .auto-style1 {
            width: 54px;
        }

        #ctl00_C1_lblError {
            font-size: xx-large;
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Exam Print</h6>
            </div>
            <div class="panel-body">
                <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Exam Name </label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlexam" AppendDataBoundItems="true" class="select-search" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlexam"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Class</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlClass" class="select-search"  AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue="0" ControlToValidate="ddlClass"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-10">
                        <div class="form-actions">
                            <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">TR Sheet</h6>
                <div class="form-actions text-right">
                    <input type="button" id="btnExport" value="Print" onclick="PrintDiv();" class="btn btn-primary" />
                </div>
            </div>
            <div class="panel-body" id="Idcontents" style="width: 100%;">
                <asp:Label runat="server" ID="lblError" Visible="false"></asp:Label>

                <table class="auto-style1 tds" align="center" border="1"  id="tblTermMN1" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <caption>
                         <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                    </caption>
                    <asp:Literal ID="ltrlSubject" runat="server"></asp:Literal>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                    <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                </table>


            </div>
        </div>
    </div>

</asp:Content>

