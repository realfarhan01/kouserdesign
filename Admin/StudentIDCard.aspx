<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="StudentIDCard.aspx.vb" Inherits="Admin_StudentIDCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .select2-container {
            width: 100% !important;
        }
    </style>
    <link href="css/styles.css" rel="stylesheet" />

    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("Idcontents").innerHTML;
            var printWindow = window.open();
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
        function PrintDivStudentdetails() {
            var divContents1 = document.getElementById("Idcontents").innerHTML;
            var divContents2 = document.getElementById("IdStDetails").innerHTML;
            var printWindow = window.open();
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents1 + divContents2);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">

    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">Student List</h6>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tfoot>
                    <tr>
                        <th>
                            <asp:DropDownList ID="ddlsearch" runat="server" class="select-search" AutoPostBack="true" AppendDataBoundItems="true" Width="100%">
                                <asp:ListItem Value="">Search</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                    </tr>
                </tfoot>
            </table>

        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h6 class="panel-title">Upload Photo</h6>
        </div>
        <table style="margin-top: 10px; margin-left: 10px; margin-bottom: 10px;">
            <tr>
                <td>
                    <asp:FileUpload ID="FileUpload1" class="form-control" runat="server" /></td>
                <td>
                    <asp:Button ID="btnUploadPhoto" OnClick="btnUploadPhoto_Click" class="btn btn-primary" Visible="false" runat="server" Text="Upload Photo" /></td>
            </tr>
        </table>

    </div>
    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Student Id Card</h6>
                <div class="form-actions text-right">
                    <input type="button" id="btnExport" value="Print" onclick="PrintDiv();" class="btn btn-primary" />
                </div>
            </div>
            <div id="Idcontents">
                <table border="1" align="center" style="margin-top: 15px; margin-bottom: 15px;" id="tblCard" visible="false" runat="server">
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Literal runat="server" ID="ltrSchool"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td>
                            <table style="margin: 5px">
                                <tr>
                                    <td>Adm No.:</td>
                                    <td>
                                        <asp:Label ID="lblAdm" runat="server"></asp:Label></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Student Name :</td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server"></asp:Label></td>
                                    <td rowspan="6" style="text-align: center">
                                        <asp:Image ID="imgStudent" Width="100px" Height="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>Class :</td>
                                    <td>
                                        <asp:Label ID="lblClass" runat="server"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblSec" runat="server"></asp:Label></td>
                                </tr>



                                <tr>
                                    <td>Father Name  :</td>
                                    <td>
                                        <asp:Label ID="lblFName" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Mother Name</td>
                                    <td>
                                        <asp:Label ID="lblMName" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>DOB :</td>
                                    <td>
                                        <asp:Label ID="lblDOB" runat="server"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>Gender  :</td>
                                    <td>
                                        <asp:Label ID="lblgender" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Blood Group  :</td>
                                    <td>
                                        <asp:Label ID="lblBlood" runat="server"></asp:Label></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Address  :</td>
                                    <td>
                                        <asp:Label ID="lblAddress" Style="width: 300px !important; display: inline-flex;" runat="server"></asp:Label></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">Emergency Contact No.  
                <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>




    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Student Id Card Detail's</h6>
                <div class="form-actions text-right">
                    <input type="button" id="btnExport1" value="Print" onclick="PrintDivStudentdetails();" class="btn btn-primary" />
                </div>
            </div>
            <div id="IdStDetails">
                <div class="form-horizontal" id="pnlStudentdetails" visible="false" runat="server">
                    <div class="panel panel-default">

                        <table border="1" width="100%" cellspacing="0" cellpadding="0">
                            <caption>REGISTRATION DETAILS</caption>
                            <br />
                            <tr>
                                <td>Form No.</td>
                                <td>
                                    <asp:Label ID="lblFormNo" class="form-control" runat="server"></asp:Label></td>
                                <td>Class</td>
                                <td>
                                    <asp:Label ID="lblRegClass" class="form-control" runat="server"></asp:Label></td>
                                <td>Registration No.(Student Id)</td>
                                <td>
                                    <asp:Label ID="lblRegistrationNo" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Adm Dat.</td>
                                <td>
                                    <asp:Label ID="lblAdmdate" class="form-control" runat="server"></asp:Label></td>
                                <td>Section</td>
                                <td>
                                    <asp:Label ID="lblSection" class="form-control" runat="server"></asp:Label></td>
                                <td>Roll No.</td>
                                <td>
                                    <asp:Label ID="lblrollno" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel panel-default">
                        <table border="1" width="100%" cellspacing="0" cellpadding="0">
                            <caption>STUDENT's DETAILS</caption>
                            <br />
                            <tr>
                                <td>Student Name</td>
                                <td>
                                    <asp:Label ID="lblStuName" class="form-control" runat="server"></asp:Label></td>
                                <td>Gender</td>
                                <td>
                                    <asp:Label ID="lblGen" class="form-control" runat="server"></asp:Label></td>
                                <td>DOB</td>
                                <td>
                                    <asp:Label ID="lblDOBS" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Place of Birth</td>
                                <td>
                                    <asp:Label ID="lblPOB" class="form-control" runat="server"></asp:Label></td>
                                <td>Nationality</td>
                                <td>
                                    <asp:Label ID="lblNation" class="form-control" runat="server"></asp:Label></td>
                                <td>Mother Tongue</td>
                                <td>
                                    <asp:Label ID="lblTongue" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Second Language</td>
                                <td>
                                    <asp:Label ID="lblSecLang" class="form-control" runat="server"></asp:Label></td>
                                <td>Blood Group</td>
                                <td colspan="3">
                                    <asp:Label ID="lblBGroop" class="form-control" runat="server"></asp:Label></td>


                            </tr>

                        </table>

                    </div>
                    <div class="panel panel-default">
                        <table border="1" width="100%" cellspacing="0" cellpadding="0">
                            <caption>PARENT's DETAILS</caption>
                            <br />
                            <tr>
                                <td>Father Name</td>
                                <td>
                                    <asp:Label ID="lblfaname" class="form-control" runat="server"></asp:Label></td>
                                <td>Occupation</td>
                                <td>
                                    <asp:Label ID="lblOccup" class="form-control" runat="server"></asp:Label></td>
                                <td>Designation</td>
                                <td>
                                    <asp:Label ID="lblDesignation" class="form-control" runat="server"></asp:Label></td>
                                <td>Qualification</td>
                                <td>
                                    <asp:Label ID="lblQuali" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                
                                <td>Income</td>
                                <td>
                                    <asp:Label ID="lblInCome" class="form-control" runat="server"></asp:Label></td>
                                <td>Mobile</td>
                                <td>
                                    <asp:Label ID="lblMobile" class="form-control" runat="server"></asp:Label></td>
                                <td>Mother Name</td>
                                <td>
                                    <asp:Label ID="lblMoName" class="form-control" runat="server"></asp:Label></td>

                                 <td>Occupation</td>
                                <td>
                                    <asp:Label ID="lblmOccup" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                
                               
                                <td>Designation</td>
                                <td>
                                    <asp:Label ID="lblMdesignation" class="form-control" runat="server"></asp:Label></td>
                                <td>Qualification</td>
                                <td>
                                    <asp:Label ID="lblMoQuali" class="form-control" runat="server"></asp:Label></td>
                                 <td>Income</td>
                                <td>
                                    <asp:Label ID="lblMoIncome" class="form-control" runat="server"></asp:Label></td>
                                <td>Mobile</td>
                                <td>
                                    <asp:Label ID="lblMoMobile" class="form-control" runat="server"></asp:Label></td>
                               
                            </tr>
                           
                            <tr>
                                 <td>Guardian's</td>
                                <td>
                                    <asp:Label ID="lblGDetails" class="form-control" runat="server"></asp:Label></td>
                                <td>Occupation</td>
                                <td>
                                    <asp:Label ID="lblGoccup" class="form-control" runat="server"></asp:Label></td>
                                <td>Designation</td>
                                <td>
                                    <asp:Label ID="lblGDesig" class="form-control" runat="server"></asp:Label></td>

                                <td>Qualification</td>
                                <td>
                                    <asp:Label ID="lblGQuali" class="form-control" runat="server"></asp:Label></td>
                                 
                            </tr>
                            <tr>
                               <td>Income</td>
                                <td>
                                    <asp:Label ID="lblGIncome" class="form-control" runat="server"></asp:Label></td>
                                <td>State</td>
                                <td>
                                    <asp:Label ID="lblGState" class="form-control" runat="server"></asp:Label></td>
                                <td>City</td>
                                <td>
                                    <asp:Label ID="lblDCity" class="form-control" runat="server"></asp:Label></td>

                                <td>Pin Code </td>
                                <td>
                                    <asp:Label ID="lblPinCode" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Mobile</td>
                                <td>
                                    <asp:Label ID="lblGMob" class="form-control" runat="server"></asp:Label></td>

                                <td>Email</td>
                                <td>
                                    <asp:Label ID="lblGEmail" class="form-control" runat="server"></asp:Label></td>
                                <td>Permanent Address</td>
                                <td colspan="3">
                                    <asp:Label ID="lblGAddress" class="form-control" runat="server"></asp:Label></td>

                            </tr>
                        </table>

                    </div>

                    <div class="panel panel-default">


                        <table border="1" width="100%" cellspacing="0" cellpadding="0">
                            <caption>ADDRESS DETAILS</caption>
                            <br />
                            <tr>
                                <td>Address</td>
                                <td colspan="5">
                                    <asp:Label ID="lblAddres" class="form-control" runat="server"></asp:Label></td>

                            </tr>
                            <tr>
                                <td>State</td>
                                <td>
                                    <asp:Label ID="lblState" class="form-control" runat="server"></asp:Label></td>
                                <td>City</td>
                                <td>
                                    <asp:Label ID="lblCity" class="form-control" runat="server"></asp:Label></td>
                                <td>Locality</td>
                                <td>
                                    <asp:Label ID="lblLocalty" class="form-control" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td>Pin Code</td>
                                <td>
                                    <asp:Label ID="lblPin" class="form-control" runat="server"></asp:Label></td>
                                <td>Email</td>
                                <td>
                                    <asp:Label ID="lblEmail" class="form-control" runat="server"></asp:Label></td>
                                <td>Mobile</td>
                                <td>
                                    <asp:Label ID="lblAddressmobile" class="form-control" runat="server"></asp:Label></td>
                            </tr>

                        </table>

                    </div>

                    <div class="panel panel-default">

                        <table border="1" width="100%" cellspacing="0" cellpadding="0">
                            <caption>PREVIOUS SCHOOL DETAILS</caption>
                            <br />
                            <tr>
                                <td>School Name</td>
                                <td>
                                    <asp:Label ID="lblSchoolName" class="form-control" runat="server"></asp:Label></td>
                                <td>Class</td>
                                <td>
                                    <asp:Label ID="lblPClass" class="form-control" runat="server"></asp:Label></td>
                                <td>Session</td>
                                <td>
                                    <asp:Label ID="lblPSession" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                            <tr>

                                <td>Percent</td>
                                <td>
                                    <asp:Label ID="lblPercent" class="form-control" runat="server"></asp:Label></td>
                                <td>CGPA</td>
                                <td>
                                    <asp:Label ID="lblCGPA" class="form-control" runat="server"></asp:Label></td>
                                <td>Board</td>
                                <td>
                                    <asp:Label ID="lblBoard" class="form-control" runat="server"></asp:Label></td>
                            </tr>

                            <tr>

                                <td>Result</td>
                                <td>
                                    <asp:Label ID="lblResult" class="form-control" runat="server"></asp:Label></td>
                                <td>Reason For Leaving Last School</td>
                                <td colspan="4">
                                    <asp:Label ID="lblLeavingschool" class="form-control" runat="server"></asp:Label></td>
                            </tr>

                        </table>
                    </div>


                    <div class="panel panel-default">

                        <table border="1" width="100%" cellspacing="0" cellpadding="0">
                            <caption>OTHERS DETAILS</caption>
                            <br />
                            <tr>
                                <td>Transport Facility</td>
                                <td>
                                    <asp:Label ID="lblOtherFacility" class="form-control" runat="server"></asp:Label></td>
                                <td>Religion</td>
                                <td>
                                    <asp:Label ID="lblOtherReligion" class="form-control" runat="server"></asp:Label></td>
                                <td>RTE</td>
                                <td>
                                    <asp:Label ID="lblOtherRate" class="form-control" runat="server"></asp:Label></td>
                                <td>Admission Fees</td>
                                <td>
                                    <asp:Label ID="lblOthAdm" class="form-control" runat="server"></asp:Label></td>
                            </tr>
                            <tr>

                                <td>Tution Fees</td>
                                <td>
                                    <asp:Label ID="lblOthTution" class="form-control" runat="server"></asp:Label></td>
                                <td>Category</td>
                                <td>
                                    <asp:Label ID="lblCategory" class="form-control" runat="server"></asp:Label></td>

                                <td>House</td>
                                <td colspan="3">
                                    <asp:Label ID="lblhouse" class="form-control" runat="server"></asp:Label></td>
                            </tr>

                        </table>

                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

