<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" EnableViewState="true" CodeFile="ExamPrintNew.aspx.vb" Inherits="Admin_ExamPrintNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function PrintDiv() {
            var Section = $('#ctl00_C1_hdnSection').val();
            if (Section == "tblTerm12IX2") {
                var atn = $('#ctl00_C1_txtAttendancetblTerm12IX2').val();
                var printButton = document.getElementById("ctl00_C1_txtAttendancetblTerm12IX2");
                printButton.style.visibility = 'hidden';
                document.getElementById("divAttendancetblTerm12IX2").innerHTML = atn;
            }
            if (Section == "tblTerm12") {
                var atn1 = $('#ctl00_C1_txtAttendancetblTerm12').val();
                var printButton1 = document.getElementById("ctl00_C1_txtAttendancetblTerm12");
                printButton1.style.visibility = 'hidden';
                document.getElementById("divAttendancetblTerm12").innerHTML = atn1;
            }
            if (Section == "tblTerm1") {
                var atn2 = $('#ctl00_C1_txtAttendancetblTerm1').val();
                var printButton2 = document.getElementById("ctl00_C1_txtAttendancetblTerm1");
                printButton2.style.visibility = 'hidden';
                document.getElementById("divAttendancetblTerm1").innerHTML = atn2;
            }
            if (Section == "tblEleTwe1") {
                var atn3 = $('#ctl00_C1_txtAttendancetblEleTwe1').val();
                var printButton3 = document.getElementById("ctl00_C1_txtAttendancetblEleTwe1");
                printButton3.style.visibility = 'hidden';
                document.getElementById("divAttendancetblEleTwe1").innerHTML = atn3;
            }
            if (Section == "tblEleTwe12") {
                var atn4 = $('#ctl00_C1_txtAttendancetblEleTwe12').val();
                var printButton4 = document.getElementById("ctl00_C1_txtAttendancetblEleTwe12");
                printButton4.style.visibility = 'hidden';
                document.getElementById("divAttendancetblEleTwe12").innerHTML = atn4;
            }
            var divContents = document.getElementById("Idcontents").innerHTML;
            var printWindow = window.open();
            printWindow.document.write('<html><head>');
            printWindow.document.write('<style type="text/css">.tds {text-align: center;}#tblRecords td {text-align: center;}  td {padding: 1px; }.tdIns{text-align:center;} table td{font-size:smaller} table{width:100%}@page {ize: auto;margin: 0mm;}</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
        function saveAttendanceTerm12IX2() {
            var Attendance = $('#ctl00_C1_txtAttendancetblTerm12IX2').val();
            saveAttendance(Attendance);
            $('#ctl00_C1_txtAttendancetblTerm12IX2').attr('disabled', 'disabled');
        }
        function saveAttendanceTerm12() {
            var Attendance = $('#ctl00_C1_txtAttendancetblTerm12').val();
            saveAttendance(Attendance);
            $('#ctl00_C1_txtAttendancetblTerm12').attr('disabled', 'disabled');
        }
        function saveAttendanceTerm1() {
            var Attendance = $('#ctl00_C1_txtAttendancetblTerm1').val();
            saveAttendance(Attendance);
            $('#ctl00_C1_txtAttendancetblTerm1').attr('disabled', 'disabled');
        }
        function saveAttendanceEleTwe1() {
            var Attendance = $('#ctl00_C1_txtAttendancetblEleTwe1').val();
            saveAttendance(Attendance);
            $('#ctl00_C1_txtAttendancetblEleTwe1').attr('disabled', 'disabled');
        }
        function saveAttendanceEleTwe12() {
            var Attendance = $('#ctl00_C1_txtAttendancetblEleTwe12').val();
            saveAttendance(Attendance);
            $('#ctl00_C1_txtAttendancetblEleTwe12').attr('disabled', 'disabled');
        }




        function saveAttendance(Attendance) {
            var responsedata;
            var studentid = $('#ctl00_C1_hdnStudentId').val();
            var classid = $('#ctl00_C1_hdnClassId').val();
            var examid = $('#ctl00_C1_hdnExamId').val();
            $.ajax({
                type: "POST",
                url: "ExamPrintNew.aspx/SaveAttendance",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{StudentId:'" + studentid + "',ClassId:'" + classid + "',ExamId:'" + examid + "',Attendance:'" + Attendance + "'}",
                async: false,
                success: function (response) {
                    responsedata = response.d;
                    alert("Attendance Updated Successfully !!");
                },
                error: function () {
                }
            });
        }
    </script>

    <style type="text/css">
        .tds {
            text-align: center;
        }

        #tblRecords td {
            text-align: center;
        }

        td {
            padding: 10px;
        }

        .auto-style1 {
            width: 54px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Exam Print</h6>
            </div>
            <div class="panel-body">
                <asp:Literal ID="litmsg" runat="server"></asp:Literal>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Exam Name </label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlexam" AppendDataBoundItems="true" class="select-search" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlexam"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">Class</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlClass" class="select-search" AutoPostBack="true" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue="0" ControlToValidate="ddlClass"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-2 control-label">Student</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlstudent" class="select-search" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="ddlstudent"
                            ErrorMessage="*" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label"></label>
                    <div class="col-sm-10">
                        <div class="form-actions">
                            <asp:Button ID="btnsubmit" runat="server" ValidationGroup="UserRegistration" class="btn btn-primary" Text="Submit" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Exam Print </h6>
                <div class="form-actions text-right">
                    <input type="button" id="btnExport" value="Print" onclick="PrintDiv();" class="btn btn-primary" />
                </div>
            </div>
            <div class="panel-body" id="Idcontents" style="width: 100%;">

                <table class="nav-justified" id="tblTerm12" visible="false" runat="server" cellpadding="0" cellspacing="0" style="width: 100%;">

                    <tr>
                        <td colspan="2" style="width: 40%; text-align: left">
                            <img src="images/logo1.png" style="width: 130px; height: 100px;" /></td>
                        <td colspan="3">&nbsp;</td>
                        <td colspan="6" style="width: 40%;" class="tds"></td>
                        <td colspan="3">&nbsp;</td>
                        <td colspan="2" style="width: 10%;">
                            <img src="images/calogo.png" style="width: 100px; height: 100px;" /></td>
                    </tr>
                    <tr>
                        <td colspan="16" class="tds" style="font-size: large"><span id="lblSession1" runat="server"></span></td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                <tr>
                                    <td style="line-height: 25px;">Student Name :</td>
                                    <td>
                                        <asp:Label ID="lblTeaherremarks" runat="server"></asp:Label></td>
                                    <td>Class/Section</td>
                                    <td>
                                        <asp:Label ID="lblSection" runat="server"></asp:Label></td>
                                    <td>Roll No :</td>
                                    <td>
                                        <asp:Label ID="lblRollNo" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Date of Birth :</td>
                                    <td>
                                        <asp:Label ID="lblDOB" runat="server"></asp:Label></td>
                                    <td>Adm No :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblAdmNo" runat="server"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Father's Name :</td>
                                    <td>
                                        <asp:Label ID="lblFName" runat="server"></asp:Label></td>
                                    <td>Mother's Name :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblMName" runat="server"></asp:Label></td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <table class="nav-justified tds" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td>Scholastic</td>
                                    <td colspan="6">Term-1<br />
                                        (100 marks)</td>
                                    <td colspan="6">Term-2<br />
                                        (100 marks)</td>
                                    <%--<td>Total Marks</td>
                                    <td>Overall Grade</td>--%>
                                </tr>
                                <tr>
                                    <td>Sub Name</td>
                                    <td>Per Test<br />
                                        (<asp:Label runat="server" ID="lblPeriodicTest1"></asp:Label>)
                                    </td>
                                    <td>Note Book
                                        <br />
                                        (<asp:Label runat="server" ID="lblNoteBook1"></asp:Label>)
                                    </td>
                                    <td>Sub Enrichment
                                        <br />
                                        (<asp:Label runat="server" ID="lblSubEnrich1"></asp:Label>)
                                    </td>
                                    <td>Mid Term
                                        <br />
                                        (<asp:Label runat="server" ID="lblHalfExam1"></asp:Label>)
                                    </td>
                                    <td>Marks obtained(<asp:Label runat="server" ID="lblTotalMarks1"></asp:Label>)</td>
                                    <td>Grade</td>
                                    <td>Per Test<br />
                                        (<asp:Label runat="server" ID="lblPeriodicTest2"></asp:Label>)
                                    </td>
                                    <td>Note Book
                                        <br />
                                        (<asp:Label runat="server" ID="lblNoteBook2"></asp:Label>)
                                    </td>
                                    <td>Sub Enrichment
                                        <br />
                                        (<asp:Label runat="server" ID="lblSubEnrich2"></asp:Label>)
                                    </td>
                                    <td>Yearly Exam
                                        <br />
                                        (<asp:Label runat="server" ID="lblHalfExam2"></asp:Label>)
                                    </td>
                                    <td>Marks obtained(<asp:Label runat="server" ID="lblTotalMarks2"></asp:Label>)</td>
                                    <td>Grade</td>
                                    <%--<td>&nbsp;</td>
                                    <td>&nbsp;</td>--%>
                                </tr>
                                <asp:Literal ID="ltrlMarks2" runat="server"></asp:Literal>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="width: 100%; padding-top: 20px;">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td style="line-height: 25px;">Subject</td>
                                    <td style="line-height: 25px; text-align: center;">Grade</td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">GK</td>
                                    <td class="tds">
                                        <asp:Literal ID="LitGKTerm12" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <div runat="server" id="DivIXFIT">
                                <table class="nav-justified" id="tblRecords9fit" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                    <tr>
                                        <td class='tds' style="width: 25%;">Subject</td>
                                        <td class='tds'>Theory(30)</td>
                                        <td class='tds'>Practical(70)</td>
                                        <td class='tds' style="width: 25%;">Marks Obtained(100)</td>
                                        <td class='tds' style="width: 25%;">Grade</td>
                                    </tr>
                                    <asp:Literal ID="LitIXFIT" runat="server"></asp:Literal>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td>Attendance</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="divAttendancetblTerm12"></td>
                                                <td>
                                                    <asp:TextBox ID="txtAttendancetblTerm12" onblur="saveAttendanceTerm12();" BorderWidth="0" BorderColor="Transparent" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="16">
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                            <tr>
                                                <td>Co-Scholastic Areas : Term-1[on a 3 point (A-C)grading scale]</td>
                                                <td class='tds'>Grade</td>
                                            </tr>
                                            <asp:Literal ID="ltrlTerm1" runat="server"></asp:Literal>
                                        </table>
                                    </td>
                                    <td style="width: 50%">
                                        <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                            <tr>
                                                <td>Co-Scholastic Areas : Term-2[on a 3 point (A-C)grading scale]</td>
                                                <td class='tds'>Grade</td>
                                            </tr>
                                            <asp:Literal ID="ltrlTerm2" runat="server"></asp:Literal>
                                        </table>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>

                    <tr>
                        <td colspan="16">
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                            <tr>
                                                <td colspan="2" style="text-align: right;">Grade</td>

                                            </tr>
                                            <tr>
                                                <td>Discipline: Term-1[on a 3 point (A-C)grading scale]</td>
                                                <td class="tds">
                                                    <asp:Literal ID="LitDisciplineTerm121" runat="server"></asp:Literal></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%">
                                        <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                            <tr>
                                                <td colspan="2" style="text-align: right;">Grade</td>

                                            </tr>
                                            <tr>
                                                <td>Discipline: Term-2[on a 3 point (A-C)grading scale]</td>
                                                <td class="tds">
                                                    <asp:Literal ID="LitDisciplineTerm122" runat="server"></asp:Literal></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>

                    <tr>
                        <td colspan="16">
                            <table width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                            <tr>
                                                <td colspan="2" class='tds'>Term-1 Parent Involvement</td>

                                            </tr>
                                            <tr>
                                                <td>Excellent</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>Satisfactory</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>Unsatisfactory</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%">
                                        <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                            <tr>
                                                <td colspan="2" class='tds'>Term-2 Parent Involvement</td>

                                            </tr>
                                            <tr>
                                                <td>Excellent</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>Satisfactory</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>Unsatisfactory</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="width: 100%;">

                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <br />
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="4">Class Teacher's Remarks:
                                        <asp:TextBox ID="TextBox5" Style="border-style: Dashed; border: 0px; border-bottom: 1px dashed; width: 80%;" runat="server"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <br />
                                        <br />
                                    </td>

                                </tr>
                                <tr>
                                    <td>Place : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td>Date :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td>Signature of Class Teacher</td>
                                    <td>Signature of Principal</td>
                                </tr>
                            </table>

                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <table cellpadding="0" cellspacing="0" style="width: 80%; height: 160px;">
                                    <tr>
                                        <td style="font-size: large; font-style: italic; text-align: center">
                                            <span>Education is the most powerful tool to change<br />
                                                the world. Our mission is to aid the<br />
                                                multifaceted developement of children of India
                                <br />
                                                by preparing them for life's many rich experiences
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-size: medium; font-style: italic; padding-right: 100px;">Pt T.N Mishra </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="text-align: center;">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <table class="nav-justified">

                                <tr>
                                    <td></td>
                                    <td class="tdIns">Instructions</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="3">Grading scale for scholatic areas:Grades are awarded on a 8-point grading scale as follows -</td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">
                            <div align="center">
                                <table border="1" class="tds" cellpadding="0" cellspacing="0" style="width: 60%;">
                                    <tr>
                                        <th class='tds' style="padding: 10px;">MARKS RANGE</th>
                                        <th class='tds' style="padding: 10px;">GRADE</th>
                                    </tr>
                                    <tr>
                                        <td>91-100</td>
                                        <td>A1</td>
                                    </tr>
                                    <tr>
                                        <td>81-90</td>
                                        <td>A2</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">71-80</td>
                                        <td class="auto-style1">B1</td>
                                    </tr>
                                    <tr>
                                        <td>61-70</td>
                                        <td>B2</td>
                                    </tr>
                                    <tr>
                                        <td>51-60</td>
                                        <td>C1</td>
                                    </tr>
                                    <tr>
                                        <td>41-50</td>
                                        <td>C2</td>
                                    </tr>
                                    <tr>
                                        <td>33-40</td>
                                        <td>D</td>
                                    </tr>
                                    <tr>
                                        <td>32 & Below</td>
                                        <td>E(Needs improvement)</td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                </table>

                <table class="nav-justified" id="tblTerm12IX2" visible="false" runat="server" cellpadding="0" cellspacing="0" style="width: 100%;">

                    <tr>
                        <td colspan="2" style="width: 40%; text-align: left">
                            <img src="images/logo1.png" style="width: 130px; height: 100px;" /></td>
                        <td colspan="3">&nbsp;</td>
                        <td colspan="6" style="width: 40%;" class="tds"></td>
                        <td colspan="3">&nbsp;</td>
                        <td colspan="2" style="width: 10%;">
                            <img src="images/calogo.png" style="width: 100px; height: 100px;" /></td>
                    </tr>
                    <tr>
                        <td colspan="16" class="tds" style="font-size: large"><span id="lblSession2" runat="server"></span></td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                <tr>
                                    <td style="line-height: 25px;">Student Name :</td>
                                    <td>
                                        <asp:Label ID="lblTeaherremarksIX2" runat="server"></asp:Label></td>
                                    <td>Class/Section</td>
                                    <td>
                                        <asp:Label ID="lblSectionIX2" runat="server"></asp:Label></td>
                                    <td>Roll No :</td>
                                    <td>
                                        <asp:Label ID="lblRollNoIX2" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Date of Birth :</td>
                                    <td>
                                        <asp:Label ID="lblDOBIX2" runat="server"></asp:Label></td>
                                    <td>Adm No :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblAdmNoIX2" runat="server"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Father's Name :</td>
                                    <td>
                                        <asp:Label ID="lblFNameIX2" runat="server"></asp:Label></td>
                                    <td>Mother's Name :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblMNameIX2" runat="server"></asp:Label></td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="padding-top: 20px">
                            <table class="nav-justified tds" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td>Scholastic</td>
                                    <td colspan="6">Academic Year<br />
                                        (100 marks)</td>
                                </tr>
                                <tr>
                                    <td>Sub Name</td>
                                    <td>Per Test<br />
                                        (<asp:Label runat="server" ID="lblPeriodicTest2IX2"></asp:Label>)
                                    </td>
                                    <td>Note Book
                                        <br />
                                        (<asp:Label runat="server" ID="lblNoteBook2IX2"></asp:Label>)
                                    </td>
                                    <td>Sub Enrichment
                                        <br />
                                        (<asp:Label runat="server" ID="lblSubEnrich2IX2"></asp:Label>)
                                    </td>
                                    <td>Yearly Exam
                                        <br />
                                        (<asp:Label runat="server" ID="lblHalfExam2IX2"></asp:Label>)
                                    </td>
                                    <td>Marks obtained(<asp:Label runat="server" ID="lblTotalMarks2IX2"></asp:Label>)</td>
                                    <td>Grade</td>
                                </tr>
                                <asp:Literal ID="ltrlMarks2IX2" runat="server"></asp:Literal>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <div runat="server" id="DivIXFITIX2" style="padding-top: 20px" align="center">
                                <table class="nav-justified" id="tblRecords9fitIX2" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                    <tr>
                                        <td class='tds' style="width: 25%; line-height: 25px;">Subject</td>
                                        <td class='tds'>Theory(30)</td>
                                        <td class='tds'>Practical(70)</td>
                                        <td class='tds' style="width: 25%;">Marks Obtained(100)</td>
                                        <td class='tds' style="width: 25%;">Grade</td>
                                    </tr>
                                    <asp:Literal ID="LitIXFITIX2" runat="server"></asp:Literal>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="padding-top: 20px">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td>Attendance</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="divAttendancetblTerm12IX2"></td>
                                                <td>
                                                    <asp:TextBox ID="txtAttendancetblTerm12IX2" onblur="saveAttendanceTerm12IX2();" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="16" style="width: 100%; padding-top: 20px;">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td style="line-height: 25px;">Co-Scholastic Areas : [on a 5 point (A-E)grading scale]</td>
                                    <td class='tds'>Grade</td>
                                </tr>
                                <asp:Literal ID="ltrlTerm2IX2" runat="server"></asp:Literal>
                            </table>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="16" style="width: 100%; padding-top: 20px;">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td colspan="2" style="text-align: right; line-height: 25px;">Grade</td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Discipline: [on a 5 point (A-E)grading scale]</td>
                                    <td class="tds">
                                        <asp:Literal ID="LitDisciplineTerm12IX" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="16" style="width: 100%; padding-top: 20px">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td colspan="2" class='tds' style="line-height: 25px;">Parent Involvement</td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Excellent</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Satisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Unsatisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="16" style="width: 100%; padding-top: 20px;">

                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td colspan="4"></td>

                                </tr>
                                <tr>
                                    <td colspan="4" style="line-height: 25px;">Class Teacher's Remarks:
                                        <asp:TextBox ID="TextBox6IX2" Style="border-style: Dashed; border: 0px; border-bottom: 1px dashed; width: 80%;" runat="server"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Place : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td>Date :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td>Signature of Class Teacher</td>
                                    <td>Signature of Principal</td>
                                </tr>
                            </table>

                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <table cellpadding="0" cellspacing="0" style="width: 80%; height: 160px;">
                                    <tr>
                                        <td style="font-size: large; font-style: italic; text-align: center">
                                            <span>Education is the most powerful tool to change<br />
                                                the world. Our mission is to aid the<br />
                                                multifaceted developement of children of India
                                <br />
                                                by preparing them for life's many rich experiences
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-size: medium; font-style: italic; padding-right: 100px;">Pt T.N Mishra </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="text-align: center;">
                            <%--<br/><br/><br/><br/><br/><br/><br/>
                            <br/><br/><br/><br/><br/><br/><br/>--%>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <table class="nav-justified">

                                <tr>
                                    <td></td>
                                    <td class="tdIns">INSTRUCTIONS</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="3">Grading scale for scholatic areas:Grades are awarded on a 8-point grading scale as follows -</td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <div align="center">
                                <table border="1" class="tds" cellpadding="0" cellspacing="0" style="width: 60%;">
                                    <tr>
                                        <th class='tds' style="padding: 10px;">MARKS RANGE</th>
                                        <th class='tds' style="padding: 10px;">GRADE</th>
                                    </tr>
                                    <tr>
                                        <td>91-100</td>
                                        <td>A1</td>
                                    </tr>
                                    <tr>
                                        <td>81-90</td>
                                        <td>A2</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">71-80</td>
                                        <td class="auto-style1">B1</td>
                                    </tr>
                                    <tr>
                                        <td>61-70</td>
                                        <td>B2</td>
                                    </tr>
                                    <tr>
                                        <td>51-60</td>
                                        <td>C1</td>
                                    </tr>
                                    <tr>
                                        <td>41-50</td>
                                        <td>C2</td>
                                    </tr>
                                    <tr>
                                        <td>33-40</td>
                                        <td>D</td>
                                    </tr>
                                    <tr>
                                        <td>32 & Below</td>
                                        <td>E(Failed)</td>
                                    </tr>
                                </table>

                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                </table>

                <table class="nav-justified" align="center" id="tblTerm1" cellpadding="0" cellspacing="0" visible="false" runat="server" style="width: 100%;">

                    <tr>
                        <td colspan="7">
                            <table class="nav-justified">
                                <tr>
                                    <td style="width: 40%; text-align: left;">
                                        <img src="images/logo1.png" style="width: 110px; height: 100px;" /></td>
                                    <td>&nbsp;</td>
                                    <td class="tds" style="width: 40%;"></td>
                                    <td>&nbsp;</td>
                                    <td style="width: 10%;">
                                        <img src="images/calogo.png" style="width: 100px; height: 100px; padding-left: 75px;" /></td>
                                </tr>
                            </table>
                            <br />
                        </td>

                    </tr>
                    <tr>
                        <td colspan="7" class="tds" style="font-size: large"><span id="lblSession3" runat="server"></span></td>
                    </tr>
                    <tr>
                        <td colspan="7">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td>Student's Name</td>
                                    <td class="tds">
                                        <asp:Label ID="lblStudentName" runat="server"></asp:Label></td>
                                    <td>Roll No :</td>
                                    <td class="tds">
                                        <asp:Label ID="lblRollN" runat="server"></asp:Label></td>
                                    <td>Class/Section</td>
                                    <td class="tds">
                                        <asp:Label ID="lblSec" runat="server"></asp:Label></td>
                                </tr>
                                <%--<tr>
                                    <td>Date of Birth :</td>
                                    <td class="tds">
                                        <asp:Label ID="lblDOBs" runat="server"></asp:Label></td>
                                    <td>Mother's/Father's/Guardian's Name :</td>
                                    <td class="tds" colspan="3">
                                        <asp:Label ID="lblGurd" runat="server"></asp:Label></td>
                                </tr>--%>
                                <tr>
                                    <td style="line-height: 25px;">Date of Birth :</td>
                                    <td>
                                        <asp:Label ID="lblDOBs" runat="server"></asp:Label></td>
                                    <td>Adm No :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblAdmNoTerm1" runat="server"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Father's Name :</td>
                                    <td>
                                        <asp:Label ID="lblterm1FName" runat="server"></asp:Label></td>
                                    <td>Mother's Name :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblterm1MName" runat="server"></asp:Label></td>

                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="7">&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" id="tblRecords" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td>Scholastic</td>
                                    <td colspan="6">Term-1</td>
                                </tr>
                                <tr>
                                    <td>Sub Name</td>
                                    <td>Periodic Test  (<asp:Label runat="server" ID="lblPer"></asp:Label>)</td>
                                    <td>Note Book(<asp:Label runat="server" ID="lblNote"></asp:Label>)</td>
                                    <td>Sub Enrichment
                                       
                                        (<asp:Label runat="server" ID="lblSubEnrich"></asp:Label>)</td>
                                    <td>Half Yearly  (<asp:Label runat="server" ID="lblAnnualExamination"></asp:Label>)</td>
                                    <td>Marks Obtained(<asp:Label runat="server" ID="lblTotalMarksObtained"></asp:Label>)</td>
                                    <td>Grade</td>
                                </tr>
                                <asp:Literal ID="ltrlMark" runat="server"></asp:Literal>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="7">
                            <div runat="server" id="tr1">
                                <table class="nav-justified" id="tblRecords" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                    <tr>
                                        <td class='tds' style="width: 25%;">Subject</td>
                                        <td class='tds'>Theory(<span id="lblTh" runat="server"></span>)</td>
                                        <td class='tds'>Practical(<span id="lblPrc" runat="server"></span>)</td>
                                        <td class='tds' style="width: 25%;">Marks Obtained(100)</td>
                                        <td class='tds' style="width: 25%;">Grade</td>
                                    </tr>
                                    <asp:Literal ID="ltrlFIT" runat="server"></asp:Literal>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td>Attendance</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="divAttendancetblTerm1"></td>
                                                <td>
                                                    <asp:TextBox ID="txtAttendancetblTerm1" onblur="saveAttendanceTerm1();" BorderWidth="0" BorderColor="Transparent" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>

                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" style="width: 100%;" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td>Co-Scholastic Areas [on a
                                        <asp:Label runat="server" ID="lblterm1Scholastic"></asp:Label>grading scale]</td>
                                    <td class="tds" style="width: 20%;">Grade</td>
                                </tr>
                                <asp:Literal ID="ltrlScholastic" runat="server"></asp:Literal>
                            </table>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" style="width: 100%;" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="tds" style="width: 20%;">Grade</td>
                                </tr>
                                <tr>
                                    <td>Discipline[on a
                                        <asp:Label runat="server" ID="lblterm1Discipline"></asp:Label>
                                        grading scale]</td>
                                    <td class="tds">
                                        <asp:Literal ID="ltrlDiscipline" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td colspan="2" class='tds'>Term-1 Parent Involvement</td>

                                </tr>
                                <tr>
                                    <td>Excellent</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Satisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Unsatisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td colspan="16" style="width: 100%;">

                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td colspan="4"><br /><br /></td>

                                </tr>
                                <tr>
                                    <td colspan="4">Class Teacher's Remarks:
                                        <asp:TextBox ID="TextBox1" Style="border-style: Dashed; border: 0px; border-bottom: 1px dashed; width: 80%;" runat="server"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td colspan="4"> <br /><br /><br /></td>

                                </tr>
                                <tr>
                                    <td>Place : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td>Date :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td>Signature of Class Teacher</td>
                                    <td>Signature of Principal</td>
                                </tr>
                            </table>

                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <br /><br /><br />
                            <div align="center">
                            <table cellpadding="0" cellspacing="0" style="width: 80%; height: 160px;">
                                <tr>
                                    <td style="font-size: large; font-style: italic; text-align:center">
                                        <span>Education is the most powerful tool to change<br />
                                            the world. Our mission is to aid the<br />
                                            multifaceted developement of children of India
                                <br />
                                            by preparing them for life's many rich experiences
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; font-size: medium; font-style: italic; padding-right: 100px;">Pt T.N Mishra </td>
                                </tr>
                            </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="text-align: center;">
                            <br/><br/><br/><br/><br/><br/><br/>
                            <br/><br/><br/><br/><br/><br/><br/>
                            <table class="nav-justified">

                                <tr>
                                    <td></td>
                                    <td class="tdIns">Instructions</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="3">Grading scale for scholatic areas:Grades are awarded on a 8-point grading scale as follows -</td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">
                            <div align="center">
                            <table border="1" class="tds" cellpadding="0" cellspacing="0" style="width: 60%;">
                                <tr>
                                    <th class='tds' style="padding: 10px;">MARKS RANGE</th>
                                    <th class='tds' style="padding: 10px;">GRADE</th>
                                </tr>
                                <tr>
                                    <td>91-100</td>
                                    <td>A1</td>
                                </tr>
                                <tr>
                                    <td>81-90</td>
                                    <td>A2</td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">71-80</td>
                                    <td class="auto-style1">B1</td>
                                </tr>
                                <tr>
                                    <td>61-70</td>
                                    <td>B2</td>
                                </tr>
                                <tr>
                                    <td>51-60</td>
                                    <td>C1</td>
                                </tr>
                                <tr>
                                    <td>41-50</td>
                                    <td>C2</td>
                                </tr>
                                <tr>
                                    <td>33-40</td>
                                    <td>D</td>
                                </tr>
                                <tr>
                                    <td>32 & Below</td>
                                    <td>E(<asp:Label runat="server" ID="term1grade"></asp:Label>)</td>
                                </tr>
                            </table>
                            </div> 
                        </td>
                    </tr>
                </table>


                <table class="nav-justified" align="center" id="tblEleTwe1" cellpadding="0" cellspacing="0" visible="false" runat="server" style="width: 100%;">

                    <tr>
                        <td colspan="7">
                            <table class="nav-justified">
                                <tr>
                                    <td style="width: 40%; text-align: left;">
                                        <img src="images/logo1.png" style="width: 110px; height: 100px;" /></td>
                                    <td>&nbsp;</td>
                                    <td class="tds" style="width: 40%;"></td>
                                    <td>&nbsp;</td>
                                    <td style="width: 10%;">
                                        <img src="images/calogo.png" style="width: 100px; height: 100px; padding-left: 75px;" /></td>
                                </tr>
                            </table>
                            <br />
                        </td>

                    </tr>
                    <tr>
                        <td colspan="7" class="tds" style="font-size: large"><span id="lblSession4" runat="server"></span></td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td>Student's Name</td>
                                    <td class="tds">
                                        <asp:Label ID="lblelSName" runat="server"></asp:Label></td>
                                    <td>Roll No :</td>
                                    <td class="tds">
                                        <asp:Label ID="lblelRN" runat="server"></asp:Label></td>
                                    <td>Class/Section</td>
                                    <td class="tds">
                                        <asp:Label ID="lblelClass" runat="server"></asp:Label></td>
                                </tr>
                                <%-- <tr>
                                    <td>Date of Birth :</td>
                                    <td class="tds">
                                        <asp:Label ID="lblElDOBS" runat="server"></asp:Label></td>
                                    <td>Mother's/Father's/Guardian's Name :</td>
                                    <td class="tds" colspan="3">
                                        <asp:Label ID="lblElGuar" runat="server"></asp:Label></td>
                                </tr>--%>
                                <tr>
                                    <td style="line-height: 25px;">Date of Birth :</td>
                                    <td>
                                        <asp:Label ID="lblElDOBS" runat="server"></asp:Label></td>
                                    <td>Adm No :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblAdmNoTerm2" runat="server"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Father's Name :</td>
                                    <td>
                                        <asp:Label ID="lblterm2FName" runat="server"></asp:Label></td>
                                    <td>Mother's Name :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblterm2MName" runat="server"></asp:Label></td>

                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="7">&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" id="tblRecords" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td rowspan="2" style="width: 25%;">Subject</td>
                                    <td colspan="2" style="width: 50%;">Term 1</td>
                                    <td rowspan="2" style="width: 25%;">Marks Obtained(100)</td>
                                </tr>
                                <tr>
                                    <td>Theory</td>
                                    <td>Practical</td>

                                </tr>
                                <asp:Literal ID="lblElMarks" runat="server"></asp:Literal>
                            </table>
                            <br />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="7">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td>Attendance</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="divAttendancetblEleTwe1"></td>
                                                <td>
                                                    <asp:TextBox ID="txtAttendancetblEleTwe1" onblur="saveAttendanceEleTwe1();" BorderWidth="0" BorderColor="Transparent" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" style="width: 100%;" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td>Co-Scholastic Areas [on a
                                        <asp:Label runat="server" ID="lblElTwTerm1Scholastic"></asp:Label>grading scale]</td>
                                    <td class="tds" style="width: 20%;">Grade</td>
                                </tr>
                                <asp:Literal ID="lblElScholastic" runat="server"></asp:Literal>
                            </table>
                        </td>

                    </tr>


                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" style="width: 100%;" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="tds" style="width: 20%;">Grade</td>
                                </tr>
                                <tr>
                                    <td>Discipline[on a
                                        <asp:Label runat="server" ID="lblElTwTerm1Discipline"></asp:Label>
                                        grading scale]</td>
                                    <td class="tds">
                                        <asp:Literal ID="ltrlEltwDiscipline" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td colspan="2" class='tds'>Term-1 Parent Involvement</td>

                                </tr>
                                <tr>
                                    <td>Excellent</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Satisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Unsatisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   
                    <tr>
                        <td colspan="7">&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td colspan="16" style="width: 100%;padding-top:20px">

                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td colspan="4" style="line-height:25px;">Class Teacher's Remarks:
                                        <asp:TextBox ID="TextBox2" Style="border-style: Dashed; border: 0px; border-bottom: 1px dashed; width: 80%;" runat="server"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td colspan="4">
                            <br/><br/><br/><br/><br/></td>

                                </tr>
                                <tr>
                                    <td style="line-height:25px;">Place : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td>Date :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td>Signature of Class Teacher</td>
                                    <td>Signature of Principal</td>
                                </tr>
                            </table>

                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">
                            <br /><br /><br />
                            <div align="center">
                            <table cellpadding="0" cellspacing="0" style="width: 80%; height: 160px;">
                                <tr>
                                    <td style="font-size: large; font-style: italic; text-align:center">
                                        <span>Education is the most powerful tool to change<br />
                                            the world. Our mission is to aid the<br />
                                            multifaceted developement of children of India
                                <br />
                                            by preparing them for life's many rich experiences
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; font-size: medium; font-style: italic; padding-right: 100px;">Pt T.N Mishra </td>
                                </tr>
                            </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="text-align: center;">
                            <%--<br/><br/><br/><br/><br/><br/><br/>--%>
                            <br/><br/><br/><br/><br/><br/><br/>
                            <br/><br/><br/><br/><br/><br/><br/>
                            <table class="nav-justified">
                                <tr>
                                    <td></td>
                                    <td class="tdIns">INSTRUCTIONS</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="3">Grading scale for scholatic areas:Grades are awarded on a 8-point grading scale as follows -</td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <div align="center">
                            <table border="1" class="tds" cellpadding="0" cellspacing="0" style="width: 60%;">
                                <tr>
                                    <th class='tds' style="padding: 10px;">MARKS RANGE</th>
                                    <th class='tds' style="padding: 10px;">GRADE</th>
                                </tr>
                                <tr>
                                    <td>91-100</td>
                                    <td>A1</td>
                                </tr>
                                <tr>
                                    <td>81-90</td>
                                    <td>A2</td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">71-80</td>
                                    <td class="auto-style1">B1</td>
                                </tr>
                                <tr>
                                    <td>61-70</td>
                                    <td>B2</td>
                                </tr>
                                <tr>
                                    <td>51-60</td>
                                    <td>C1</td>
                                </tr>
                                <tr>
                                    <td>41-50</td>
                                    <td>C2</td>
                                </tr>
                                <tr>
                                    <td>33-40</td>
                                    <td>D</td>
                                </tr>
                                <tr>
                                    <td>32 & Below</td>
                                    <td>E(Failed)</td>
                                </tr>
                            </table>
                            </div> 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                </table>


                <table class="nav-justified" id="tblEleTwe12" visible="false" runat="server" cellpadding="0" cellspacing="0" style="width: 100%;">

                    <tr>
                        <td colspan="2" style="width: 40%; text-align: left;">
                            <img src="images/logo1.png" style="width: 130px; height: 100px;" /></td>
                        <td colspan="3">&nbsp;</td>
                        <td colspan="6" style="width: 40%;" class="tds"></td>
                        <td colspan="3">&nbsp;</td>
                        <td colspan="2" style="width: 10%;">
                            <img src="images/calogo.png" style="width: 100px; height: 100px;" /></td>
                    </tr>
                    <tr>
                        <td colspan="16" class="tds" style="font-size: large"><span id="lblSession5" runat="server"></span></td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                <tr>
                                    <td style="line-height: 25px;">Student Name :</td>
                                    <td colspan="2">
                                        <%--<asp:Label ID="lblTeaherremarks" Style="border-style: Dashed; border: 0px; border-bottom: 1px dashed; width: 100%;" runat="server"></asp:Label></td>--%>
                                        <asp:Label ID="lblStudentNameEleTwe12" runat="server"></asp:Label></td>
                                    <td>Roll N :</td>
                                    <td>
                                        <asp:Label ID="lblRollNoEleTwe12" runat="server"></asp:Label></td>
                                    <td>Class/Section</td>
                                    <td>
                                        <asp:Label ID="lblSecEleTwe12" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Date of Birth :</td>
                                    <td>
                                        <asp:Label ID="lblDOBEleTwe12" runat="server"></asp:Label></td>
                                    <td>Adm No :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblAdmNoEleTwe12" runat="server"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Father's Name :</td>
                                    <td>
                                        <asp:Label ID="lblFNameEleTwe12" runat="server"></asp:Label></td>
                                    <td>Mother's Name :</td>
                                    <td colspan="4">
                                        <asp:Label ID="lblMNameEleTwe12" runat="server"></asp:Label></td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="16" style="padding-top: 20px">

                            <table class="nav-justified" id="tblRecords" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td rowspan="2">Subject</td>
                                    <td rowspan="2">Periodic(50)</td>
                                    <td colspan="2">Half-Yearly</td>
                                    <td rowspan="2">Marks Obtained(100)</td>
                                    <td colspan="2">Annual</td>
                                    <td rowspan="2">Marks Obtained(100)</td>
                                    <td rowspan="2">Grand Total<%--(250)--%></td>
                                </tr>
                                <tr>
                                    <td>Theory</td>
                                    <td>Practical</td>
                                    <td>Theory</td>
                                    <td>Practical</td>

                                </tr>
                                <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="16" style="padding-top: 20px">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td style="line-height: 25px;">Attendance</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="divAttendancetblEleTwe12"></td>
                                                <td>
                                                    <asp:TextBox ID="txtAttendancetblEleTwe12" onblur="saveAttendanceEleTwe12();" BorderWidth="0" BorderColor="Transparent" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <%--<td colspan="8" style="width: 50%;">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td>Co-Scholastic Areas : Term-1[on a 3 point (A-C)grading scale]</td>
                                    <td class='tds'>Grade</td>
                                </tr>
                                <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                            </table>
                        </td>--%>
                        <td colspan="16" style="width: 100%; padding-top: 20px">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td style="line-height: 25px;">Co-Scholastic Areas : [on a 5 point (A-E)grading scale]</td>
                                    <td class='tds'>Grade</td>
                                </tr>
                                <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <%--<td colspan="8" style="width: 50%;">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td colspan="2" style="text-align: right;">Grade</td>

                                </tr>
                                <tr>
                                    <td>Displine: Term-1[on a 3 point (A-C)grading scale]</td>
                                    <td style="width: 10%;"></td>
                                </tr>
                            </table>
                        </td>--%>
                        <td colspan="16" style="width: 100%; padding-top: 20px">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td colspan="2" style="text-align: right; line-height: 25px;">Grade</td>

                                </tr>
                                <tr>
                                    <td>Discipline: [on a 5 point (A-E)grading scale]</td>
                                    <td class="tds">
                                        <asp:Literal ID="LitDisciplineEleTwe12" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- <td colspan="8" style="width: 50%;">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td colspan="2" class='tds'>Term-1 Parent Involvement</td>

                                </tr>
                                <tr>
                                    <td>Excellent</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Satisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>Unsatisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>--%>
                        <td colspan="16" style="width: 100%; padding-top: 20px">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                <tr>
                                    <td colspan="2" style="line-height: 25px;" class='tds'>Parent Involvement</td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Excellent</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Satisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Unsatisfactory</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <%--<td colspan="8" style="width: 50%;">

                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td colspan="3">Term-1</td>

                                </tr>
                                <tr>
                                    <td colspan="3">Class Teacher's Remarks:
                                        <asp:TextBox ID="TextBox10" Style="border-style: Dashed; border: 0px; border-bottom: 1px dashed; width: 80%;" runat="server"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td colspan="3"></td>

                                </tr>
                                <tr>
                                    <td>Place Date :  </td>
                                    <td>Signature of Class Teacher</td>
                                    <td>Signature of Principal</td>
                                </tr>
                            </table>

                        </td>--%>
                        <td colspan="16" style="width: 100%; padding-top: 20px">

                            <table class="nav-justified" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td colspan="4" style="line-height: 25px;">Class Teacher's Remarks:
                                        <asp:TextBox ID="TextBox11" Style="border-style: Dashed; border: 0px; border-bottom: 1px dashed; width: 80%;" runat="server"></asp:TextBox></td>

                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </td>

                                </tr>
                                <tr>
                                    <td style="line-height: 25px;">Place : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                    <td>Date :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td>Signature of Class Teacher</td>
                                    <td>Signature of Principal</td>
                                </tr>
                            </table>

                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <table cellpadding="0" cellspacing="0" style="width: 80%; height: 160px;">
                                    <tr>
                                        <td style="font-size: large; font-style: italic; text-align: center">
                                            <span>Education is the most powerful tool to change<br />
                                                the world. Our mission is to aid the<br />
                                                multifaceted developement of children of India
                                <br />
                                                by preparing them for life's many rich experiences
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-size: medium; font-style: italic; padding-right: 100px;">Pt T.N Mishra </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="text-align: center;">
                            <%--<br/><br/><br/><br/><br/><br/><br/>--%>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <table class="nav-justified">
                                <tr>
                                    <td></td>
                                    <td class="tdIns">INSTRUCTIONS</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="3">Grading scale for scholatic areas:Grades are awarded on a 8-point grading scale as follows -</td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="16">
                            <div align="center">
                                <table border="1" class="tds" cellpadding="0" cellspacing="0" style="width: 60%;">
                                    <tr>
                                        <th class='tds' style="padding: 10px;">MARKS RANGE</th>
                                        <th class='tds' style="padding: 10px;">GRADE</th>
                                    </tr>
                                    <tr>
                                        <td>91-100</td>
                                        <td>A1</td>
                                    </tr>
                                    <tr>
                                        <td>81-90</td>
                                        <td>A2</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">71-80</td>
                                        <td class="auto-style1">B1</td>
                                    </tr>
                                    <tr>
                                        <td>61-70</td>
                                        <td>B2</td>
                                    </tr>
                                    <tr>
                                        <td>51-60</td>
                                        <td>C1</td>
                                    </tr>
                                    <tr>
                                        <td>41-50</td>
                                        <td>C2</td>
                                    </tr>
                                    <tr>
                                        <td>33-40</td>
                                        <td>D</td>
                                    </tr>
                                    <tr>
                                        <td>32 & Below</td>
                                        <td>E(Failed)</td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16">&nbsp;</td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnStudentId" runat="server" />
    <asp:HiddenField ID="hdnExamId" runat="server" />
    <asp:HiddenField ID="hdnClassId" runat="server" />
    <asp:HiddenField ID="hdnSection" runat="server" />
</asp:Content>


