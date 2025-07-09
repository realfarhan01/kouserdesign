<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExportData.aspx.vb" Inherits="Admin_ExportData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
     <link href="https://cdn.datatables.net/1.10.15/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
   
    <script src="//cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js" type="text/javascript"></script>
    
    <link href="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
             $("#DataDisplay").prepend($("<thead></thead>").append($("#DataDisplay").find("tr:first"))).dataTable({
                "dom": 'lBfrtip',
                'sSwfPath': '//cdn.datatables.net/tabletools/2.2.4/swf/copy_csv_xls_pdf.swf',
                select: {
                    style: 'multi'
                },
                buttons: [
                    'colvis',
                    'selectAll',
                    'selectNone',
                    {
                        extend: 'collection',
                        text: 'Export Selected',
                        buttons: ['copy', 'csv', 'excel', 'pdf', 'print'],
                        exportOptions: {
                            rows: { selected: true }
                        }
                    }
                ],
                "lengthMenu": [50, 100, 200]

            });
            
        });
    </script>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="datatable_wrapper"></div>
     <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a id="lnkDelete" data-id='<%# eval("Studentid") %>' onclick="showPopUp(this.id);" runat="server" title="Edit">Edit</a>
                           <%-- <a href='Studentregistration.aspx?Sid=<%# eval("Studentid") %>'>Edit</a>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Studentid" HeaderText="Student Id"></asp:BoundField>
                    <asp:BoundField DataField="Studentname" HeaderText="Student Name"></asp:BoundField>
                    <asp:BoundField DataField="fathername" HeaderText="Father Name"></asp:BoundField>
                    <asp:BoundField DataField="Mothername" HeaderText="Mother Name "></asp:BoundField>
                    <asp:BoundField DataField="Parentid" HeaderText="Parent Id"></asp:BoundField>
                    <asp:BoundField DataField="ClassName" HeaderText="Class"></asp:BoundField>
                    <asp:BoundField DataField="House" HeaderText="House"></asp:BoundField>
                    <asp:BoundField DataField="LoginID" HeaderText="Login Id"></asp:BoundField>
                    <asp:BoundField DataField="Password" HeaderText="Password"></asp:BoundField>
                    <asp:BoundField DataField="BirthDate" HeaderText="Date of Birth"></asp:BoundField>
                    <asp:BoundField DataField="RegDate" HeaderText="Adm Date"></asp:BoundField>                  
                    <asp:BoundField DataField="Emailid" HeaderText="Email Id"></asp:BoundField>
                    <asp:BoundField DataField="Nationality" HeaderText="Nationality"></asp:BoundField>
                    <asp:BoundField DataField="Gender" HeaderText="Gender"></asp:BoundField>                 
                    <asp:BoundField DataField="Address" HeaderText="Address"></asp:BoundField>
                    <asp:BoundField DataField="City" HeaderText="City"></asp:BoundField>
                    <asp:BoundField DataField="State" HeaderText="State"></asp:BoundField>
                    <asp:BoundField DataField="ContactNo" HeaderText="ContactNo"></asp:BoundField>                
                    <asp:BoundField DataField="RollNo" HeaderText="Roll No."></asp:BoundField> 
                      <asp:TemplateField HeaderText="Transport Facility?">
                        <ItemTemplate>
                          <%# If(Eval("isTransport").ToString() = "1", "Yes", "No")%>
                        </ItemTemplate>
                    </asp:TemplateField>                              
                    <asp:BoundField DataField="PickupPoint" HeaderText="Pickup Point"></asp:BoundField>                 
                    <asp:BoundField DataField="RegFormNo" HeaderText="Reg. Form No."></asp:BoundField>
                    <asp:BoundField DataField="PlaceofBirth" HeaderText="Place of Birth"></asp:BoundField>
                    <asp:BoundField DataField="MotherTongue" HeaderText="Mother Tongue"></asp:BoundField>
                    <asp:BoundField DataField="SecondLanguage" HeaderText="Second Language"></asp:BoundField>
                    <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group"></asp:BoundField>
                    <asp:BoundField DataField="LastSchool" HeaderText="Last School"></asp:BoundField>
                    <asp:BoundField DataField="LastSession" HeaderText="Last Session"></asp:BoundField>
                    <asp:BoundField DataField="LastPercentage" HeaderText="Last Percentage"></asp:BoundField>
                    <asp:BoundField DataField="LastCGPA" HeaderText="Last CGPA"></asp:BoundField>
                    <asp:BoundField DataField="LastBoard" HeaderText="Last Board"></asp:BoundField>
                    <asp:BoundField DataField="LastResult" HeaderText="Last Result"></asp:BoundField>
                    <asp:BoundField DataField="LastLeavingReason" HeaderText="Last Leaving Reason"></asp:BoundField>
                    <asp:BoundField DataField="Religion" HeaderText="Religion"></asp:BoundField>                    
                    <asp:TemplateField HeaderText="RTE">
                        <ItemTemplate>
                           <%# If(Eval("RTE").ToString() = "Y", "Yes", "No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RegNo" HeaderText="Reg No."></asp:BoundField>
                     <asp:TemplateField HeaderText="Admission Fees Free?">
                        <ItemTemplate>
                           <%# If(Eval("isAdmissionFeesFree").ToString() = "1", "Yes", "No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tuition Fees Free?">
                        <ItemTemplate>
                          <%# If(Eval("isTuitionFeesFree").ToString() = "1", "Yes", "No")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
