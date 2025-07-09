<%@ Page Title="" Language="VB" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="false" CodeFile="ProductCategory.aspx.vb" Inherits="Admin_ProductCategory" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/treeview.css" rel="stylesheet" />
    <script src="js/jquery.nestable.js"></script>
    <script src="js/category.js"></script>
    <script>

            $(document).ready(function () {

                var updateOutput = function (e) {
                    var list = e.length ? e : $(e.target),
                        output = list.data('output');
                    if (window.JSON) {
                        output.val(window.JSON.stringify(list.nestable('serialize')));//, null, 2));
                    } else {
                        output.val('JSON browser support required for this demo.');
                    }
                };
                $('#nestable-menu').on('click', function (e) {
                    var target = $(e.target),
                        action = target.data('action');
                    if (action === 'expand-all') {
                        $('.dd').nestable('expandAll');
                    }
                    if (action === 'collapse-all') {
                        $('.dd').nestable('collapseAll');
                    }
                });
                // activate Nestable for list 2
                $('#nestable3').nestable({
                    group: 1
                })
                 .on('change', updateOutput);
                updateOutput($('#nestable3').data('output', $('#nestable2-output')));

                $('#nestable3').nestable();

            });
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
        <!-- BEGIN MODAL FORM-->
    <div class="modal fade" id="view" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Edit Category</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="form-body">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <input type="text" class="form-control" placeholder="Category" id="txtEditCategoryName" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <input name="" type="checkbox" id="chk" value="" />
                                            Appear in Home page
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnUpdateCategory" data-appearinhomepage="" data-maincategoryid="" data-id="">Save</button>
                    <button type="button" class="btn btn-default" id="btnclearFormEdit" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!-- BEGIN MODAL FORM-->
    <div class="modal fade" id="new" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add Category</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="form-body">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <input type="text" class="form-control" id="txtAddCategory" maxlength="35" placeholder="Category" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <input name="" type="checkbox" id="chkAddAppear" value="" />
                                    Appear in Home page
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnAddCategory">Save</button>
                    <button type="button" class="btn btn-default" id="btnclearForm"  data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <!-- END MODAL FORM-->

            <div class="form-horizontal" >



                <!-- Basic inputs -->
                <div class="panel panel-default">
                    <div class="panel-heading"><h6 class="panel-title">Category</h6>
                        <div class="pull-right"><a class="btn btn-primary btn-xs" href="#new" data-toggle="modal"><i class="fa fa-plus"></i>Category</a></div>

                    </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <menu id="nestable-menu">
                                        <button type="button" data-action="expand-all">Expand All</button>
                                        <button type="button" data-action="collapse-all">Collapse All</button>
                                    </menu>
                                </div>
                            </div>
                            <div class="dd" id="nestable3">
                                <ol class="dd-list" id="MainOl">

                                    <% Dim BLL As New BusinessLogicLayer %>
                                    <%  Dim dt As DataTable = CType(BLL.GetCategories(3, 0), DataTable) %>
                                    <% Session("CategoryListing") = dt %>
                                    <% Dim TotalRows As Integer = dt.Rows.Count%>
                                    <input type="hidden" id="hfTotalRows" value="<% =TotalRows%>" />
                                    <% If Not dt Is Nothing Then %>

                                    <% Dim menu As DataRow() = dt.[Select]("MainCategoryId=0") %>

                                    <%If menu IsNot Nothing Then %>

                                    <% For Each li As DataRow In menu %>


                                    <% Dim CategoryName As String = li("CategoryName").ToString()%>
                                    <% Dim CategoryId As Integer = Convert.ToInt32(li("CategoryId"))%>
                                    <% Dim MainCategoryId As Integer = Convert.ToInt32(li("MainCategoryId"))%>
                                    <% Dim AppearinHomePage As Integer = Convert.ToInt32(li("AppearinHomePage"))%>
                                    <% Dim SubMenu As DataRow() = dt.[Select]("MainCategoryId='" & CategoryId & "'") %>

                                    <li class="dd-item dd3-item" id="li<% =CategoryId%>" data-maincategoryid="<% =MainCategoryId%>" data-id="<% =CategoryId%>">
                                        <div class="dd-handle dd3-handle"><i class="fa fa-arrows"></i></div>
                                        <div class="dd3-content" id="categoryName<% =CategoryId%>"><% =CategoryName%> </div>
                                        <div class="dd-handle-edit dd3-handle-edit"><a class="detail" data-appearinhomepage="<% =AppearinHomePage%>" data-maincategoryid="<% =MainCategoryId%>" data-toggle="modal" id="EditCategory" data-name="<% =CategoryName%>" data-id="<% =CategoryId%>" href="#view"><i class="fa fa-pencil"></i></a></div>
                                        <div class="dd-handle-end dd3-handle-end"><a class="detail" id="DeleteCategory"  data-name="<% =CategoryName%>" data-id="<% =CategoryId%>" href="#"><i class="fa fa-times"></i></a></div>

                                        <%If SubMenu IsNot Nothing Then %>
                                        <ol class="dd-list" id="subOl">
                                            <% For Each ul As DataRow In SubMenu %>
                                            <% Dim SubCategoryName As String = ul("CategoryName")%>
                                            <% Dim SubCategoryId As Integer = Convert.ToInt32(ul("CategoryId"))%>
                                            <% Dim SubMainCategoryId As Integer = Convert.ToInt32(ul("MainCategoryId"))%>
                                            <% Dim SubAppearinHomePage As Integer = Convert.ToInt32(ul("AppearinHomePage"))%>

                                            <li class="dd-item dd3-item" id="li<% =SubCategoryId%>" data-maincategoryid="<% =SubMainCategoryId%>" data-id="<% =SubCategoryId%>">
                                                <div class="dd-handle dd3-handle"><i class="fa fa-arrows"></i></div>
                                                <div class="dd3-content" id="categoryName<% =SubCategoryId%>"><% =SubCategoryName%> </div>
                                                <div class="dd-handle-edit dd3-handle-edit"><a class="detail" data-toggle="modal" id="EditSubCategory" data-appearinhomepage="<% =SubAppearinHomePage%>" data-maincategoryid="<% =SubMainCategoryId%>" data-name="<% =SubCategoryName%>" data-id="<% =SubCategoryId%>" href="#view"><i class="fa fa-pencil"></i></a></div>
                                                <div class="dd-handle-end dd3-handle-end"><a class="detail" id="DeleteSubCategory" data-name="<% =SubCategoryName%>" data-id="<% =SubCategoryId%>" href="javascript:;"><i class="fa fa-times"></i></a></div>

                                            </li>



                                            <%Next %>
                                        </ol>


                                        <% End If %>
                                    </li>



                                    <%Next%>

                                    <% End If %>

                                    <% End If %>
                                </ol>
                            </div>
                        </div>
                        <div class="panel-footer" align="center">

                            <a class="btn btn-primary btn-sm" id="btnSaveCategory" href="javascript:;">Save</a> <a class="btn btn-default btn-sm" href="#">Cancel</a>
                        </div>
            <!-- /#page-wrapper -->
        </div>
    </div>
     <textarea id="nestable2-output" style="display:none;"></textarea>
</asp:Content>

