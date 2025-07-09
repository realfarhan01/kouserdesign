<%@ Page Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false"
    CodeFile="Home.aspx.vb" Inherits="Home" Title="::Home Page::" %>
    
    <%@ Register TagPrefix="ECalendar" Namespace="ExtendedControls" Assembly="EventCalendar" %>

<asp:Content ID="CH1" ContentPlaceHolderID="C1" runat="Server">
<div id="bnkdiv" runat="server" style="display:none;">
                <div class="modal fade" id="BankModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  
   <div class="modal-dialog">
                                      <div class="modal-content">
                                      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
    <h3>Events</h3>
  </div>
  <div class="modal-body">
   <asp:GridView ID="gvSelectedDateEvents" CssClass="table table-bordered table-hover CSSTableGenerator" runat="server" Width="100%">
        </asp:GridView>
     </div>
        
         
                                                  
          
  <div class="modal-footer">
        
 
  <a  href="#tab_1_5" data-toggle="tab"><button type="button" class="btn btn-default" data-dismiss="modal">Close</button></a>
      
                               
  </div>

</div></div></div></div>
  <!-- Statistics -->
            <ul class="row stats">
                <li class="col-xs-3"><a href="javascript:;" class="btn btn-default"> <asp:Literal ID="litProductCategories" runat="server"></asp:Literal></a> <span>Product Categories</span></li>
                <li class="col-xs-3"><a href="javascript:;" class="btn btn-default"> <asp:Literal ID="litTotalProducts" runat="server"></asp:Literal></a> <span>Total Products</span></li>
                <li class="col-xs-3"><a href="javascript:;" class="btn btn-default"> <asp:Literal ID="litTotalProductImages" runat="server"></asp:Literal></a> <span>Total Product Images</span></li>
            </ul>
            <!-- /statistics -->

     

        
            <!-- Calendar -->
            <div class="panel panel-default">
                <div class="panel-heading"><h5 class="panel-title">Calendar</h5></div>
                <div class="panel-body">
                    <%--<div class="fullcalendar"></div>--%>
                     
               

  
  
  
 
        <br />

       
        <ECalendar:EventCalendar ID="Calendar1" runat="server" 
        BackColor="White" BorderColor="Silver" CssClass="table table-bordered CSSTableGenerator"
            BorderWidth="1px" Font-Names="Verdana"
            Font-Size="9pt" ForeColor="Black" Height="800px"
            Width="100%" FirstDayOfWeek="Monday" NextMonthText="Next &gt;" 
        PrevMonthText="&lt; Prev" ShowGridLines="True" NextPrevFormat="FullMonth" 
            ShowDescriptionAsToolTip="True" BorderStyle="Solid" 
        EventDateColumnName="" EventDescriptionColumnName="" EventHeaderColumnName="" 
        OnSelectionChanged="Calendar1_SelectionChanged">
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TodayDayStyle BackColor="#CCCCCC" />
            <SelectorStyle BorderColor="#404040" BorderStyle="Solid" />
            <DayStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#333333" Font-Bold="True" VerticalAlign="Bottom" />
            <DayHeaderStyle BorderWidth="1px" Font-Bold="True" Font-Size="8pt" />
            <TitleStyle BackColor="White"   Font-Bold="True" BorderColor="Black" BorderWidth="1"
                Font-Size="12pt" ForeColor="#333399" HorizontalAlign="Center" VerticalAlign="Middle" />
        </ECalendar:EventCalendar>

                </div>
            </div>
            <!-- /calendar -->
        

           

</asp:Content>
