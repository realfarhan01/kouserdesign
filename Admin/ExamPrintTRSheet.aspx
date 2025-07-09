<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" EnableViewState="true" CodeFile="ExamPrintTRSheet.aspx.vb" Inherits="Admin_ExamPrintTRSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function PrintDiv() {
            
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
    </script>

    <style type="text/css">
        .tds {
            text-align: center;
        }

        #tblRecords td {
            text-align: center;
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
                        <asp:DropDownList ID="ddlClass" class="select-search" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue="0" ControlToValidate="ddlClass"
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

                <div class="nav-justified" id="tblTerm12" visible="false" runat="server" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <asp:Repeater runat="server" ID="RptrTerm12">
                        <ItemTemplate>
                            <table style="width: 100%;">
                            <tr>
                                <td colspan="16">
                                     <table class="nav-justified" cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                        <tr>
                                            <td style="line-height:25px;">Student Name :</td>
                                            <td>
                                                <%# Eval("StudentName")%></td>
                                            <td>Class/Section</td>
                                            <td>
                                                <%# Eval("Class")%></td>
                                            <td>Adm No :</td>
                                            <td>
                                                <%# Eval("StudentId")%></td>
                                        </tr>
                                    </table>
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
                                            <td>Subject</td>
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
                                <td colspan="16">&nbsp;</td>
                            </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="nav-justified" id="tblTerm12IX2" visible="false" runat="server" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <asp:Repeater runat="server" ID="RepeaterTerm12IX2">
                        <ItemTemplate>
                        <table style="width: 100%;">
                        <tr>
                            <td colspan="16" style="padding-top:20px">
                                <table class="nav-justified" cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                    <tr>
                                        <td style="line-height:25px;">Student Name :</td>
                                        <td>
                                            <%# Eval("StudentName")%></td>
                                        <td>Class/Section</td>
                                        <td>
                                            <%# Eval("Class")%></td>
                                        <td>Adm No :</td>
                                        <td>
                                            <%# Eval("StudentId")%></td>
                                    </tr>
                                </table>
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
                                    <table class="nav-justified" id="tblRecords9fitIX2" cellpadding="0" cellspacing="0" style="width: 100%;" border="1">
                                        <tr>
                                            <td class='tds' style="width: 25%;line-height:25px;">Subject</td>
                                            <td class='tds'>Theory(40)</td>
                                            <td class='tds'>Practical(60)</td>
                                            <td class='tds' style="width: 25%;">Marks Obtained(100)</td>
                                            <td class='tds' style="width: 25%;">Grade</td>
                                        </tr>
                                        <asp:Literal ID="LitIXFITIX2" runat="server"></asp:Literal>
                                    </table>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="16">&nbsp;</td>
                        </tr></table>
                        </ItemTemplate>
                    </asp:Repeater> 
                </div>

                <div class="nav-justified" id="Div1" visible="false" runat="server" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <asp:Repeater runat="server" ID="Repeater1">
                        <ItemTemplate>
                            <table class="nav-justified" align="center" id="tblTerm1" cellpadding="0" cellspacing="0"   style="width: 100%;">
                                <tr>
                                    <td colspan="7">
                                        <table class="nav-justified" cellpadding="0" cellspacing="0" border="1">
                                            <tr>
                                                <td>Student's Name</td>
                                                <td class="tds">
                                                    <%# Eval("StudentName")%></td>
                                                <td>Roll No :</td>
                                                <td class="tds">
                                                    <%# Eval("StudentId")%></td>
                                                <td>Class/Section</td>
                                                <td class="tds">
                                                    <%# Eval("Class")%></td>
                                            </tr>
                                        </table>
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
                        <td colspan="7">&nbsp;</td>
                    </tr>

                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="nav-justified" id="tblEleTwe1" visible="false" runat="server" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <asp:Repeater runat="server" ID="RepeaterEleTwe1">
                        <ItemTemplate>
                <table class="nav-justified" align="center" id="tblEleTwe1" cellpadding="0" cellspacing="0" style="width: 100%;">

                    <tr>
                        <td colspan="7">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" border="1">
                                <tr>
                                    <td>Student's Name</td>
                                    <td class="tds">
                                         <%# Eval("StudentName")%></td>
                                    <td>Roll No :</td>
                                    <td class="tds">
                                         <%# Eval("StudentId")%></td>
                                    <td>Class/Section</td>
                                    <td class="tds">
                                         <%# Eval("Class")%></td>
                                </tr>
                            </table>
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
                </table>
                             </ItemTemplate>
                         </asp:Repeater>
                    </div>


                <div class="nav-justified" id="tblEleTwe12" visible="false" runat="server" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <asp:Repeater runat="server" ID="RepeaterEleTwe12">
                        <ItemTemplate>

                    <table style="width: 100%;">
                    <tr>
                        <td colspan="16" style="padding-top:20px">
                            <table class="nav-justified" cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                <tr>
                                    <td style="line-height:25px;">Student Name :</td>
                                    <td colspan="2">
                                        <%# Eval("StudentName")%></td>
                                    <td>Adm No :</td>
                                    <td>
                                        <%# Eval("StudentId")%></td>
                                    <td>Class/Section</td>
                                    <td>
                                        <%# Eval("Class")%></td>
                                </tr>
                            </table>
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
                    </table>
                        </ItemTemplate>
                    </asp:Repeater> 
                </div>

            </div>
        </div>
    </div>
<asp:HiddenField ID="hdnStudentId" runat="server" />
<asp:HiddenField ID="hdnExamId" runat="server" />
<asp:HiddenField ID="hdnClassId" runat="server" />
<asp:HiddenField ID="hdnSection" runat="server" />
</asp:Content>


