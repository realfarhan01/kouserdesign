<%@ Page Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false"
    EnableEventValidation="false" CodeFile="UserDetail.aspx.vb" Inherits="UserDetail"
    Title=":: Total operator ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="C1" runat="Server">
    <script type="text/javascript" language="javascript">
        function isValid() {
            if (!IsNumericOnly($('[id$=txtPageno]'))) {
                return false;
            }
            return true;
        }
    </script>
                     <div class="row-fluid">
					<div class="span12">
						<div class="box box-color box-bordered">
							<div class="box-title">
								<h3>
									<i class="icon-table"></i>
									Operators
								</h3>
							</div>
							<div class="box-content nopadding">
                             

  
                            <asp:GridView ID="DataDisplay" runat="server" CssClass="table table-nomargin" AutoGenerateColumns="false"
                                AllowPaging="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("Loginid") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CommandName="PDelete"
                                                CommandArgument='<%# Eval("Loginid") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LoginId" HeaderText="Login ID" />
                                    <asp:BoundField DataField="UserName" HeaderText="Operator Name" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" />
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                </Columns>
                                <RowStyle CssClass="RowStyle" />
                                <PagerStyle CssClass="PagerStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <PagerSettings Position="TopAndBottom" />
                            </asp:GridView>
                      </div></div></div></div>
</asp:Content>
