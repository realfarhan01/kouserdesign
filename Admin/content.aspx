<%@ Page Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="content.aspx.vb" Inherits="content" title="ADD Content" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="C1" Runat="Server">

    <script type="text/javascript" src="tiny_mce/tiny_mce.js"></script>
<script type="text/javascript">
    tinyMCE.init({
        mode: "textareas",
        theme: "simple"
    });
</script>
<div class="row-fluid">
					<div class="span12">
						<div class="box box-bordered">
							<div class="box-title">
								<h3><i class="icon-th-list"></i> News and Popupr</h3>
							</div>
							<div class="box-content nopadding">
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmsg" runat="server" Font-Bold="true" ForeColor="Coral"></asp:Label>
								<div class='form-horizontal form-bordered'>
                   
                    <div class="control-group">
                        <label class="control-label">
                          Content Type  <span style="color: #ff0000">*</span> :</label>
                        <div class="controls">
                               <asp:DropDownList ID="ddlContent" runat="server" Width="149px" AutoPostBack="True">
                
              </asp:DropDownList>&nbsp;
              <asp:CheckBox ID="chkshow" runat="server" Text="Yes I  want to Show this on home Page" />
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Content  <span style="color: #ff0000">*</span> :</label>
                        <div class="controls">
                         <asp:TextBox ID="txtcontent" CssClass="span6" TextMode="multiLine" Columns="60" Rows="20" runat="server"></asp:TextBox></td>
                        </div>
                    </div>
                    <div class="form-actions">
                       <asp:Button ID="btnsave" runat="server" Text="save"  
                                                CssClass="btn btn-success" ValidationGroup="CreateUser" />

                                           
                       
                    </div>

                    </div></div></div></div></div>


   
</asp:Content>

