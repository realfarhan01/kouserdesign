<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ExamParticular.aspx.vb" Inherits="ExamParticular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" runat="Server">
    <div class="form-horizontal">
        <!-- Basic inputs -->
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Add Exam Particular </h6>
            </div>
            <div class="panel-body">

                <%-- <div class="alert alert-info fade in widget-inner">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            Default form components, including bootstrap additions and tags
                        </div>--%>
                <asp:HiddenField ID="hfId" runat="server" />
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
                        <asp:DropDownList ID="ddlClass" class="select-search"  AppendDataBoundItems="true" runat="server">
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
    </div>
    <div class="panel panel-default">

        <div class="panel-heading">
            <h6 class="panel-title">Exam Particulars</h6>
            <div class="form-actions text-right">
                    <asp:Button ID="btnUpdate"  runat="server" CssClass="btn btn-primary" Text="Update" Visible="false"></asp:Button>
                </div>
        </div>
        <div class="table-responsive">

            <asp:GridView ID="DataDisplay" class="table table-bordered table-check" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ClassName" HeaderText="Class Name"></asp:BoundField>
                    <asp:BoundField DataField="ExamName" HeaderText="Exam Name"></asp:BoundField>

                    <asp:TemplateField HeaderText="Exam Date">
                        <ItemTemplate>
                          <asp:TextBox ID="txtExamDate" Width="120px" class="form-control datepicker" Text='<%# Eval("ExamDate")%>'  runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Theory Marks">
                        <ItemTemplate>
                          <asp:TextBox ID="txtMaxTheoryMarks" Width="120px" Text='<%# Eval("MaxTheoryMarks")%>' class="form-control" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Practical Marks">
                        <ItemTemplate>
                          <asp:TextBox ID="txtMaxPracticalMarks" Width="120px" Text='<%# Eval("MaxPracticalMarks")%>' class="form-control" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Periodic test">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPeriodictest" Width="120px" Text='<%# Eval("PeriodicTestMarksMax")%>' class="form-control" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Note Book">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNoteBook" Width="120px" Text='<%# Eval("NoteBookMarksmax")%>' class="form-control" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Subject Enrichment">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSubjectEnrichment" Width="120px" Text='<%# Eval("SubEnrichmentMarksmax")%>' class="form-control" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                 
                    <asp:TemplateField HeaderText="Pass Marks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPassTheoryMarks" Width="120px" Text='<%# Eval("PassTheoryMarks")%>' class="form-control" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pass Practical Marks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPassPracticalMarks" Width="120px" Text='<%# Eval("PassPracticalMarks")%>' class="form-control" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

