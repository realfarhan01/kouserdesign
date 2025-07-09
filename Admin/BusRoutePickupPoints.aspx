<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="BusRoutePickupPoints.aspx.vb" Inherits="BusRoutePickupPoints" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">

<div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Assign Bus Pickup Point Route List </h6></div>
                    <div class="panel-body">
                  
                       <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:Literal ID="lbltxt" runat="server"></asp:Literal>
                      
                        
                         <div class="form-group">
                            <label class="col-sm-2 control-label">Bus Route </label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlRoute" class="select-search" AppendDataBoundItems="true" AutoPostBack="true" runat="server">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                             
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue="0" ControlToValidate="ddlRoute"
                ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    
                        </div>
                        </div></div>
                        <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Add Pickup Points</h6></div>
                    <div class="panel-body">
                    
                        <!-- Left box -->
                        <div class="left-box">
                        
                          
                            <asp:ListBox ID="ListBox1" class="form-control" Height="300px" SelectionMode="Multiple" runat="server"></asp:ListBox>
                        
                           
                        </div>
                        <!-- /left-box -->
                        
                        <!-- Control buttons -->
                        <div class="dual-control">
                        <asp:Button ID="btn1" runat="server" Text=">" Width="50px" Height="30px" class="btn btn-default" onclick="btn1_Click" />

<%--<asp:Button ID="btn2" runat="server" Text=">>" class="btn btn-default" onclick="btn2_Click" />--%>

<asp:Button ID="btn3" runat="server" Text="<" class="btn btn-default" Width="50px" Height="30px" onclick="btn3_Click" />

<%--<asp:Button ID="btn4" runat="server" Text="<<" class="btn btn-default" onclick="btn4_Click" />--%>
                            
                        </div>
                        <!-- /control buttons -->
                        
                        <!-- Right box -->
                        <div class="right-box">
                             <asp:ListBox ID="ListBox2" class="form-control" Height="300px" SelectionMode="Multiple" runat="server"></asp:ListBox>
                        
                        </div>
                        <!-- /right box -->

                         <div class="form-actions text-center">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                       
                        </div>
                        
                    </div>
                </div>
                        
</asp:Content>

