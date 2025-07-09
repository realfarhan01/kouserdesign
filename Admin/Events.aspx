<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="Events.aspx.vb" Inherits="Admin_Events" %>
<%@ Register TagPrefix="ECalendar" Namespace="ExtendedControls" Assembly="EventCalendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
  .CSSTableGenerator {
	margin:0px;padding:0px;
	width:100%;
	box-shadow: 10px 10px 5px #888888;
	border:1px solid #bf5f00;
	
	-moz-border-radius-bottomleft:13px;
	-webkit-border-bottom-left-radius:13px;
	border-bottom-left-radius:13px;
	
	-moz-border-radius-bottomright:13px;
	-webkit-border-bottom-right-radius:13px;
	border-bottom-right-radius:13px;
	
	-moz-border-radius-topright:13px;
	-webkit-border-top-right-radius:13px;
	border-top-right-radius:13px;
	
	-moz-border-radius-topleft:13px;
	-webkit-border-top-left-radius:13px;
	border-top-left-radius:13px;
}.CSSTableGenerator table{
    border-collapse: collapse;
        border-spacing: 0;
	width:100%;
	height:100%;
	margin:0px;padding:0px;
}.CSSTableGenerator tr:last-child td:last-child {
	-moz-border-radius-bottomright:13px;
	-webkit-border-bottom-right-radius:13px;
	border-bottom-right-radius:13px;
}
.CSSTableGenerator table tr:first-child td:first-child {
	-moz-border-radius-topleft:13px;
	-webkit-border-top-left-radius:13px;
	border-top-left-radius:13px;
}
.CSSTableGenerator table tr:first-child td:last-child {
	-moz-border-radius-topright:13px;
	-webkit-border-top-right-radius:13px;
	border-top-right-radius:13px;
}.CSSTableGenerator tr:last-child td:first-child{
	-moz-border-radius-bottomleft:13px;
	-webkit-border-bottom-left-radius:13px;
	border-bottom-left-radius:13px;
}.CSSTableGenerator tr:hover td{
	
}
.CSSTableGenerator tr:nth-child(odd){ background-color:#ffd4aa; }
.CSSTableGenerator tr:nth-child(even)    { background-color:#ffffff; }.CSSTableGenerator td{
	vertical-align:middle;
	
	
	border:1px solid #bf5f00;
	border-width:0px 1px 1px 0px;
	text-align:left;
	padding:7px;
	font-size:10px;
	font-family:Arial;
	font-weight:bold;
	color:#000000;
}.CSSTableGenerator tr:last-child td{
	border-width:0px 1px 0px 0px;
}.CSSTableGenerator tr td:last-child{
	border-width:0px 0px 1px 0px;
}.CSSTableGenerator tr:last-child td:last-child{
	border-width:0px 0px 0px 0px;
}
.CSSTableGenerator tr:first-child td{
		background:-o-linear-gradient(bottom, #ff7f00 5%, #ff7f00 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #ff7f00), color-stop(1, #ff7f00) );
	background:-moz-linear-gradient( center top, #ff7f00 5%, #ff7f00 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#ff7f00", endColorstr="#ff7f00");	background: -o-linear-gradient(top,#ff7f00,ff7f00);

	background-color:#ff7f00;
	border:0px solid #bf5f00;
	text-align:center;
	border-width:0px 0px 1px 1px;
	font-size:14px;
	font-family:Arial;
	font-weight:bold;
	color:#ffffff;
}
.CSSTableGenerator tr:first-child:hover td{
	background:-o-linear-gradient(bottom, #ff7f00 5%, #ff7f00 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #ff7f00), color-stop(1, #ff7f00) );
	background:-moz-linear-gradient( center top, #ff7f00 5%, #ff7f00 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#ff7f00", endColorstr="#ff7f00");	background: -o-linear-gradient(top,#ff7f00,ff7f00);

	background-color:#ff7f00;
}
.CSSTableGenerator tr:first-child td:first-child{
	border-width:0px 0px 1px 0px;
}
.CSSTableGenerator tr:first-child td:last-child{
	border-width:0px 0px 1px 1px;
}
  
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
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
               

  
  
  
 
        <br />

       
        <ECalendar:EventCalendar ID="Calendar1" runat="server" 
        BackColor="White" BorderColor="Silver" CssClass="CSSTableGenerator"
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
    

</asp:Content>

