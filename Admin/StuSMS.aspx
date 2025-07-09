<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="StuSMS.aspx.vb" Inherits="StuSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript">
    function countLength(id) {
        var c = document.getElementById("lblmsgcount");
        c.innerText = parseInt(parseInt(id.value.length) / 160) + 1;
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
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Student SMS </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                     
                       
                       
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Class Name </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlClass" class="select-search" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValu="0" ControlToValidate="ddlClass"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                         
                        <div class="form-group">
                            <label class="col-sm-2 control-label">SMS Detail</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtDetail" class="form-control" TextMode="multiLine" onkeydown="countLength(this);" Columns="60" Rows="20" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDetail"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                <label id="lblmsgcount">
                    1</label>
                            </div>
                        </div>
                       
                           
                     
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                            
                <div class="panel-heading"><h6 class="panel-title">Student List</h6></div>
                <div class="table-responsive">
                
                        <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>
                                                                 <asp:BoundField DataField="StudentId" HeaderText="Student Id"></asp:BoundField>

                         <asp:BoundField DataField="Studentname" HeaderText="StudentName"></asp:BoundField>
                        <asp:BoundField DataField="Contactno" HeaderText="Mobile"></asp:BoundField>
                   
                    <asp:TemplateField HeaderText="Send Message">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkmsg" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                       
                         <%--<asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" CommandArgument='<%# eval("uniqueId") %>' CommandName="edit1" runat="server">Edit</asp:LinkButton>
                         </ItemTemplate>
                         
                         </asp:TemplateField>--%>
                         </Columns>
                        </asp:GridView>
                        <br />
                        <asp:Button ID="btnSms" runat="server" Text="Send SMS" Width="78px" /><input id="chk"
                    onclick="selectAll(this);" type="checkbox" /><label for="chk">Select All</label><br />
                        </div></div>

</asp:Content>

