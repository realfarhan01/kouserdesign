<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="classrollno.aspx.vb" Inherits="Admin_classrollno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .select2-container {
            width: 100% !important;
        }
    </style>
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
</script>
    <link href="css/styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">Student List</h6>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tfoot>
                    <tr>
                        <th>
                            <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select Class--</asp:ListItem>
                            </asp:DropDownList></th>
                        <th>
                            <asp:DropDownList ID="ddlSection" class="select" runat="server">
                                <asp:ListItem Value="">All Section</asp:ListItem>
                                <asp:ListItem Value="A">Section A</asp:ListItem>
                                <asp:ListItem Value="B">Section B</asp:ListItem>
                                <asp:ListItem Value="C">Section C</asp:ListItem>
                                <asp:ListItem Value="D">Section D</asp:ListItem>
                            </asp:DropDownList></th>


                        <th>
                            <asp:Button ID="btnsearch" runat="server" Text="Show Student" CssClass="btn btn-primary" /></th>
                        <th>
                            <asp:TextBox ID="txtStartRollNo" placeholder="Start Roll No" runat="server"></asp:TextBox>
                        </th>
                        <th>
                            <asp:DropDownList ID="ddlType" class="select" runat="server">
                                <asp:ListItem Value="1">Alphabetical Order</asp:ListItem>
                                <asp:ListItem Value="2">Admission No Order</asp:ListItem>
                            </asp:DropDownList></th>
                        <th>
                            <asp:Button ID="btnRollNo" runat="server" Text="Show New Roll No" Visible="false" CssClass="btn btn-primary" /></th>
                        <th>
                            <input type="button" id="btnPrint" value="Print" onclick="PrintDiv();" class="btn btn-primary" />
                        </th>
                    </tr>
                </tfoot>
            </table>
            <div class="table-responsive"  id="dvContents">
            <div align="center" runat="server" id="reportheader" visible="false" >
                <caption>
                <asp:Literal runat="server" ID="ltrSchool"></asp:Literal>
                <div align="left">Print Date:<%=Date.Today.ToString("dd/MM/yyyy") %></div>
            </caption>
            </div> 
            <asp:GridView ID="DataDisplay2" DataKeyNames="Studentid" Width="100%" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Studentid" HeaderText="Adm No"></asp:BoundField>
                    <asp:BoundField DataField="Studentname" HeaderText="Student Name"></asp:BoundField>
                    <asp:BoundField DataField="fathername" HeaderText="Father Name"></asp:BoundField>
                    <asp:BoundField DataField="ClassName" HeaderText="Class"></asp:BoundField>
                    <asp:BoundField DataField="Section" HeaderText="Section"></asp:BoundField>
                    <asp:TemplateField HeaderText="Roll No">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRollNo" placeholder="Roll No" Text='<%#Eval("RollNo") %>' runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div> 
            <div runat="server" id="divupgrade">
            <table class="table">
                <tfoot>
                    <tr>
                        <th>&nbsp;</th>
                        <th>
                            &nbsp;</th>
                        <th>
                            &nbsp;</th>
                        <th>&nbsp;</th>
                        <th>
                            <asp:Button ID="btnUpdate" Visible="false" runat="server" Text="Update Roll No" CssClass="btn btn-primary" />
                            </th>
                        <th>
                            &nbsp;</th>
                    </tr>
                </tfoot>
            </table></div>
        </div>
    </div>
    
    
    
    
</asp:Content>

