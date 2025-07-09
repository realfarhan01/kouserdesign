<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BusRouteStudentsExport.aspx.vb" Inherits="Admin_BusRouteStudentsExport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        button.dt-button, div.dt-button, a.dt-button {
            border-radius: 0px !important;
        }
        div.dt-button-collection button.dt-button:active:not(.disabled), div.dt-button-collection button.dt-button.active:not(.disabled), div.dt-button-collection div.dt-button:active:not(.disabled), div.dt-button-collection div.dt-button.active:not(.disabled), div.dt-button-collection a.dt-button:active:not(.disabled), div.dt-button-collection a.dt-button.active:not(.disabled) {
            color: white;
            background-image: -webkit-linear-gradient(top, rgba(63, 148, 189, 0.97) 0%, #106ea5 100%) !important;
        }
        th
        {    
            text-align: left !important;
        }
        h1
        {
            display: none !important;
        }
    </style>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
     <link href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
   
    <script src="//cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js" type="text/javascript"></script>
    
    <link href="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#DataDisplay").prepend($("<thead></thead>").append($("#DataDisplay").find("tr:first"))).dataTable({
                "dom": 'Bfrtip',
                buttons: [
                            {
                                extend: 'colvis',
                                collectionLayout: 'fixed four-column'
                            },
                            {
                                extend: 'excel',
                                exportOptions: {
                                    columns: ':visible'
                                }
                            },
                            {
                                extend: 'print',
                                autoPrint: false,
                                exportOptions: {
                                    columns: ':visible'
                                },
                                customize: function (win) {
                                    $(win.document.body)
                                        .css('font-size', '12pt')
                                        .prepend(
                                            '<div style="padding: 5px;line-height: 1.5;text-align: center; font-family: monospace;"><span style="font-weight: bold; font-size: 16px;">Central Academy Sr. Sec. School</span> <br> <span style="font-size: 14px;">Ajmer Road,BEAWAR,Rajasthan</span></div><hr>'
                                        );
                                    $(win.document.body).find('table')
                                        .addClass('compact')
                                        .css('font-size', 'inherit');
                                }
                            }
                        ],
                        columnDefs: [{
                            targets: [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19],
                            visible: false
                        }],
                "lengthMenu": [1000]
            });
        });
    </script>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="datatable_wrapper"></div>
        <div>
     <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                        <Columns>
                        <asp:TemplateField HeaderText="S.No" >
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>                                                                   
                        </asp:TemplateField>
                        <asp:BoundField DataField="StudentId" HeaderText="Adm No"></asp:BoundField>
                        <asp:BoundField DataField="StudentName" HeaderText="Name"></asp:BoundField>
                        <asp:BoundField DataField="ClassName" HeaderText="Class"></asp:BoundField>
                        <asp:BoundField DataField="Section" HeaderText="Section"></asp:BoundField>
                        <asp:BoundField DataField="PickupPoint" HeaderText="Pickup Point"></asp:BoundField>
                        <asp:BoundField DataField="RouteName" HeaderText="Route"></asp:BoundField>
                        <asp:BoundField DataField="MonthlyCharge" HeaderText="Monthly Charge"></asp:BoundField>
                        <asp:BoundField DataField="FatherName" HeaderText="FatherName"></asp:BoundField>
                        <asp:BoundField DataField="Address" HeaderText="Address"></asp:BoundField>
                        <asp:BoundField DataField="City" HeaderText="City"></asp:BoundField>
                        <asp:BoundField DataField="ContactNo" HeaderText="ContactNo"></asp:BoundField>
                        <asp:BoundField DataField="BloodGroup" HeaderText="BloodGroup"></asp:BoundField>
                        <asp:BoundField DataField="RTE" HeaderText="RTE"></asp:BoundField>
                        <asp:BoundField DataField="Category" HeaderText="Category"></asp:BoundField>
                        <asp:BoundField DataField="FeeCategory" HeaderText="FeeCategory"></asp:BoundField>
                        <asp:BoundField DataField="RegDate" HeaderText="RegDate"></asp:BoundField>
                        <asp:BoundField DataField="Locality" HeaderText="Locality"></asp:BoundField>
                        <asp:BoundField DataField="FatherMobile" HeaderText="FatherMobile"></asp:BoundField>
                        <asp:BoundField DataField="MotherMobile" HeaderText="MotherMobile"></asp:BoundField>
                         </Columns>
            </asp:GridView></div>
    </div>
    </form>
</body>
</html>
	