<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ParentSMS.aspx.vb" Inherits="ParentSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function countLength(id) {
            var c = document.getElementById("lblmsgcount");
            if (parseInt(id.value.length) > 160) {
                alert("caracters limit exceeded .only 160 is allowed");
                id.value = id.value.substr(0, 160)
            }
            if(parseInt(id.value.length)<=160)
            {
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

        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Parent SMS </h6>
            </div>
            <div class="panel-body">
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:Literal ID="litmsg" runat="server"></asp:Literal>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Class</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlClass" class="select-search" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue="0" ControlToValidate="ddlClass"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
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
            <h6 class="panel-title">Parent List</h6>
        </div>
        <div class="table-responsive">

            <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Parentid" HeaderText="Parent Id"></asp:BoundField>

                    <asp:BoundField DataField="Fathername" HeaderText="Father Name"></asp:BoundField>
                    <asp:BoundField DataField="Contactno" HeaderText="Mobile"></asp:BoundField>

                    <asp:TemplateField HeaderText="Send Message">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkmsg" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnSms" runat="server" Text="Send SMS" Width="78px" />
            <input id="chk" onclick="selectAll(this);" type="checkbox" /><label for="chk">Select All</label><br />
        </div>
    </div>

</asp:Content>

