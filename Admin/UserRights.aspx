<%@ Page Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false"
    CodeFile="UserRights.aspx.vb" Inherits="UserRights" Title="Operator Rights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="C1" runat="Server">
     <style type="text/css">
        .round_td
        {
            border: 1px solid #CCC;
            border-radius: 5px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
        }
        .title_header
        {
            background: linear-gradient(top, #fff,  #e0e0e0 25px);
            background: -webkit-linear-gradient(top, #fff,  #e0e0e0 25px);
            background: -ms-linear-gradient(top, #fff,  #e0e0e0 25px);
            background: -moz-linear-gradient(top, #fff,  #e0e0e0 25px);
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-bottom-color: #CCC;
            color: #045279;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            line-height: 25px;
            font-weight: bold;
            padding-left: 10px;
        }
        .list ul
        {
            display: block;
            margin: 0px;
            padding: 0px;
        }
        .list li
        {
            display: block;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            padding-top: 7px;
            padding-bottom: 7px;
            margin-left: 15px;
        }
    </style>
 <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">User Rights</h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                           <div class="form-group">
                            <label class="col-sm-2 control-label">
                           Operator Name:<span style="color: #ff0000">*</span> :</label>
                        <div class="col-sm-10">
                       <asp:DropDownList ID="ddlMemberid" runat="server" class="select" AutoPostBack="True">
                                            </asp:DropDownList>
                        </div>
                    </div>
                            <div class="form-actions">
                                  <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-success" />          
                                  </div>
                            </div>
                            </div></div>
    
   
                            <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Menu List</h6></div>
                    <div class="panel-body">   
			
        <table width="100%" border="0" cellpadding="10" cellspacing="0" class="table table-nomargin"
            style="margin-top: 10px;">
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="10" cellspacing="0">
                        
                        <tr>
                            <td>
                                             <asp:Repeater runat="server" ID="DataDisplay">
                                    <ItemTemplate>
                                    <div style="float:left; width:25%;">
                                        <table width="97%" border="0" align="left" cellpadding="5" cellspacing="0" class="round_td">
                                            <tr>
                                                <td class="title_header">
                                                    <%# Container.DataItem("MenuName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="list">
                                                    <asp:Repeater runat="server" ID="datalist1">
                                                        <HeaderTemplate>
                                                            <ul>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <li>
                                                                <asp:CheckBox runat="server" ID="chk1" Text='<%# Container.DataItem("menuName") %>'
                                                                    Checked='<%# Container.DataItem("checked") %>' />
                                                                <asp:Label runat="server" ID="lbl1" Text='<%# Container.DataItem("snostr") %>' Visible="false"></asp:Label></li>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </ul></FooterTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                        </table></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                        </td>
                                    </tr>
                                   </table>
                                   </td>
                                    </tr>
                                   </table>
                                  </div></div>
       </div>
</asp:Content>
