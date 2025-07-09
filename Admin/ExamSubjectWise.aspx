<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ExamSubjectWise.aspx.vb" Inherits="Admin_ExamSubjectWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CalculatePercentage(ID, values) {

            if ($('#' + ID).closest("tr").find(".maxMarks").val() == "") {
                $('#' + ID).closest("tr").find(".maxMarks").val(0)
            }
            if ($('#' + ID).closest("tr").find(".Periodic").val() == "") {
                $('#' + ID).closest("tr").find(".Periodic").val(0)
            }
            if ($('#' + ID).closest("tr").find(".Note").val() == "") {
                $('#' + ID).closest("tr").find(".Note").val(0)
            }
            if ($('#' + ID).closest("tr").find(".Enrichment").val() == "") {
                $('#' + ID).closest("tr").find(".Enrichment").val(0)
            }
            if ($('#' + ID).closest("tr").find(".Practical").val() == "") {
                $('#' + ID).closest("tr").find(".Practical").val(0)
            }

            var ExamTerms = $("#ctl00_C1_ddlexam option:selected").val();
            var Totalmarks = 0;
            Totalmarks += parseInt($('#' + ID).closest("tr").find(".maxMarks").val()) + parseInt($('#' + ID).closest("tr").find(".Periodic").val()) + parseInt($('#' + ID).closest("tr").find(".Note").val()) + parseInt($('#' + ID).closest("tr").find(".Enrichment").val()) + parseInt($('#' + ID).closest("tr").find(".Practical").val());
            $('#' + ID).closest("tr").find(".TotalMarks").val(Totalmarks);
           
            if ($('#' + ID).closest("tr").find(".TotalMarks").val() > 150) {
                $('#' + ID).closest("tr").find(".TotalMarks").focus();
                $('#' + ID).closest("tr").find(".TotalMarks").css("border", "2px solid red");
                alert("Please Check all marks .It is exeeded out of 150");
                return false;
            }
            else

                $('#' + ID).closest("tr").find(".TotalMarks").css("border", "1px solid #d5d5d5");


            // Grade
            var marks = parseInt($('#' + ID).closest("tr").find(".TotalMarks").val());
            if (marks >= 91 && marks <= 100) {
                debugger;
                $('#' + ID).closest("tr").find(".grade").val("A1");
            }
            else if (marks >= 81 && marks <= 90) {
                $('#' + ID).closest("tr").find(".grade").val("A2");
            }
            else if (marks >= 71 && marks <= 80) {
                $('#' + ID).closest("tr").find(".grade").val("B1");
            }
            else if (marks >= 61 && marks <= 70) {
                $('#' + ID).closest("tr").find(".grade").val("B2");
            }
            else if (marks >= 51 && marks <= 60) {
                $('#' + ID).closest("tr").find(".grade").val("C1");
            }
            else if (marks >= 41 && marks <= 50) {
                $('#' + ID).closest("tr").find(".grade").val("C2");
            }
            else if (marks >= 33 && marks <= 44) {
                $('#' + ID).closest("tr").find(".grade").val("D");
            }
            else if (marks > 0 && marks <= 32) {
                $('#' + ID).closest("tr").find(".grade").val("E");
            }
            else
                $('#' + ID).closest("tr").find(".grade").val(0);

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Submission Subject Wise </h6>
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
                    <label class="col-sm-2 control-label">Subject</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddlsubject" class="select-search" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="ddlsubject"
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


    </div>

    <div class="panel panel-default" id="DivExamParticulars" runat="server" visible="false">

        <div class="panel-heading">
            <h6 class="panel-title">Exam Particulars</h6>
            <div class="form-actions text-right">
                <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update"></asp:Button>
            </div>
        </div>
        <div class="table-responsive">

            <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                            <asp:HiddenField ID="hdfStudentid" Value='<%# Eval("Studentid")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="StudentId" HeaderText="Student Id"></asp:BoundField>
                    <asp:BoundField DataField="StudentName" HeaderText="Student Name"></asp:BoundField>
                    <asp:TemplateField HeaderText="Theory Marks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMaxTheoryMarks" onblur="CalculatePercentage(this.id,this.value)" MaxLength="2" Width="120px" Text='<%# Eval("TotalMarks")%>' class="form-control maxMarks" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Pass Practical Marks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMxaPracticalMarks" onblur="CalculatePercentage(this.id,this.value)" Width="120px" Text='<%# Eval("MxaPracticalMarks")%>' MaxLength="2" class="form-control Practical" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Periodic test">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPeriodictest" onblur="CalculatePercentage(this.id,this.value)" MaxLength="2" Width="120px" Text='<%# Eval("PeriodicTestMarks")%>' class="form-control Periodic" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Note Book">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNoteBook" onblur="CalculatePercentage(this.id,this.value)" MaxLength="2" Width="120px" Text='<%# Eval("NotebookMarks")%>' class="form-control Note" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Enrichment">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSubjectEnrichment" onblur="CalculatePercentage(this.id,this.value)" MaxLength="2" Width="120px" Text='<%# Eval("SubEnrichmentMarks")%>' class="form-control Enrichment" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Marks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTotalMarks" Width="120px"  ReadOnly="true" class="form-control TotalMarks" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade">
                        <ItemTemplate>
                            <asp:TextBox ID="txtGrade" Width="120px" Text='<%# Eval("Grade")%>'  class="form-control grade" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField> <asp:TemplateField HeaderText="Absent">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkbAttandance" runat="server"  Checked='<%# If(Eval("Attandance").ToString() = "1", "True", "False")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:HiddenField ID="NotebookMarksMax" runat="server" />
            <asp:HiddenField ID="MaxTheoryMarks" runat="server" />
            <asp:HiddenField ID="SubEnrichmentMarksMax" runat="server" />
            <asp:HiddenField ID="PeriodicTestMarksMax" runat="server" />
        </div>
    </div>
</asp:Content>

