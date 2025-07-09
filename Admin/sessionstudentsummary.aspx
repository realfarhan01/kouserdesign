<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="sessionstudentsummary.aspx.vb" Inherits="Admin_sessionstudentsummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("dvContents").innerHTML;
            var printWindow = window.open();
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
        BindHeader();
        function BindHeader() {
            $.ajax({
                type: "POST",
                url: "sessionstudentsummary.aspx/BindHeader",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = $.parseJSON(result.d);
                    alert(obj);
                },
            });
        }
    </script>
    
    <style>
        #ctl00_C1_DlDailyCollection {
            margin-left: 10px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">

    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">Session 2017-18</h6>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tfoot>
                    <tr>
                        <th>
                            <input type="button" id="btnPrint" value="Print" onclick="PrintDiv();" class="btn btn-primary" /></th>
                    </tr>
                </tfoot>
            </table>

            <div id="dvContents">
                <div id="employeelistDiv" runat="server">
                    
                    <table class="nav-justified table table-bordered table-check" border="1" cellpadding="0" cellspacing="0">
                        <caption>
                            <asp:Literal runat="server" ID="ltrSchool"></asp:Literal>
                          <div align="left">Print Date:<%=Date.Today.ToString("dd/MM/yyyy") %></div>
                        </caption>
                        <tr style="line-height: 26px; border-bottom: 1px solid #ddd;">
                            <td style="width: 200px">CLASS</td>
                            <td style="width: 200px">SECTION</td>
                            <td style="width: 200px">BOYS</td>
                            <td style="width: 200px">GIRLS</td>
                            <td style="width: 200px">TOTAL</td>
                        </tr>
                        <asp:DataList runat="server" ID="DlDailyCollection" CssClass="nav-justified">
                            <ItemTemplate>

                                <tr style='<%#IIf(Convert.ToString(Eval("CLASS")) = "", "line-height: 30px; text-align:left;", "line-height: 30px; border-top: 1px dashed black; text-align:left;")%>'>
                                    <td style="width: 200px"><%# Eval("CLASS")%></td>
                                    <td style="width: 200px"><%# Eval("SECTION")%></td>
                                    <td style="width: 200px"><%# Eval("BOYS")%></td>
                                    <td style="width: 200px"><%# Eval("GIRLS")%></td>
                                    <td style="width: 200px"><%# Eval("TOTAL")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:DataList>
                    </table>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

