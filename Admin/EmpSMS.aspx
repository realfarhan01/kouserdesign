<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="EmpSMS.aspx.vb" Inherits="EmpSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function countLength(id) {
            var c = document.getElementById("lblmsgcount");
            if (parseInt(id.value.length) > 160) {
                alert("caracters limit exceeded .only 160 is allowed");
                id.value = id.value.substr(0, 160)
            }
            if (parseInt(id.value.length) <= 160) {
                c.innerText = parseInt(id.value.length);
            }
        }
        function selectAll(chk) {
            var t;
            if (chk.checked == true) {
                t = true;
            }
            else {
                t = false;
            }
            var tbl = document.getElementById('<%=DataDisplay.ClientID %>').getElementsByTagName("input");
            for (var i = 0; i < tbl.length; i++) {
                if (tbl[i].type == "checkbox") {
                    tbl[i].checked = t;
                }

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="form-horizontal">



        <!-- Basic inputs -->
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Employee SMS </h6>
            </div>
            <div class="panel-body">

                <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:Literal ID="litmsg" runat="server"></asp:Literal>





                <div class="form-group">
                    <label class="col-sm-2 control-label">SMS Detail</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtDetail" class="form-control" TextMode="multiLine" MaxLength="160" onkeypress="countLength(ctl00_C1_txtDetail);" onkeydown="countLength(ctl00_C1_txtDetail);" Columns="60" Rows="20" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDetail"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                        <label id="lblmsgcount">
                            1</label>
                    </div>
                </div>



            </div>
        </div>
    </div>
    <div class="panel panel-default">

        <div class="panel-heading">
            <h6 class="panel-title">Employee List</h6>
        </div>
        <div class="table-responsive">

            <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Employeeid" HeaderText="Employee Id"></asp:BoundField>
                    <asp:BoundField DataField="Employeename" HeaderText="Employee Name"></asp:BoundField>
                    <asp:BoundField DataField="Contactno" HeaderText="Mobile"></asp:BoundField>
                    <asp:TemplateField HeaderText="Send Message">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkmsg" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnSms" runat="server" Text="Send SMS" Width="78px" /><input id="chk"
                onclick="selectAll(this);" type="checkbox" /><label for="chk">Select All</label><br />
        </div>
    </div>

</asp:Content>

