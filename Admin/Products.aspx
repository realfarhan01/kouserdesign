<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Admin_Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>--%>
    <script src="https://code.jquery.com/jquery-migrate-1.0.0.js"></script>
    <%--<link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" type="text/css" rel="stylesheet">--%>
    <script src="js/fileinput.js"></script>
    <link rel="stylesheet" href="css/fileinput.css" />
    <script src="js/sweet-alert.min.js"></script>
    <script src="js/jquery.alerts.js"></script>
    <link href="css/sweet-alert.css" rel="stylesheet" />
    <link href="css/jquery.alerts.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C1" Runat="Server">
    <div class="form-horizontal">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h6 class="panel-title">Products List</h6>
                            <div class="pull-right">
                                <button type="button" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#new">Add New</button>
                            </div>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                <table class="table">
                  <tfoot>
                            <tr>
                                
                                <th><asp:TextBox ID="txtproductcode" runat="server" placeHolder="Search Product" class="form-control"></asp:TextBox></th>
                                 <th><asp:DropDownList ID="ddlCat" AppendDataBoundItems="true" class="select-search" runat="server">
                                <asp:ListItem Value="na">--Select Calegory--</asp:ListItem>
                                </asp:DropDownList></th>
                                <th>Page:<asp:DropDownList ID="ddlpage" AppendDataBoundItems="true" class="select" runat="server">
                                </asp:DropDownList></th>
                                <th><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearch_Click" /></th>
                               
                            </tr>
                        </tfoot></table>
                    <asp:Repeater ID="rptData" runat="server">
                        <HeaderTemplate>
                            <table id="example" class="table table-striped table-bordered table table-striped table-bordered dataTable no-footer" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th>Product Name</th>
                                        <th>Product Code</th>
                                        <th>Main Category</th>
                                        <th>Category Name</th>
                                        <th>Price</th>
                                        <th>Weight</th>
                                        <th>SKU Code</th>
                                        <th>Status</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <img src='../<%#Eval("ThumbnailURL") %>' width="100" />
                                </td>
                                <td>
                                    <%#Eval("CatalogueTitle") %>
                                </td>
                                <td>
                                    <%#Eval("CatCode") %>
                                </td>
                                <td>
                                    <%#Eval("MainCategoryName") %>
                                </td>
                                <td>
                                    <%#Eval("CategoryName") %>
                                </td>
                                <td>
                                    <%#Eval("Price") %>
                                </td>
                                <td>
                                    <%#Eval("Weight") %>
                                </td>
                                <td>
                                    <%#Eval("SKUCode") %>
                                </td>
                                <td>
                                    <%#Eval("Status") %>
                                </td>
                                <td>
                                    <a href="javascript" class="btnEditProd" data-toggle="modal" id="EditCatalouge<%#Eval("CatalogueId") %>" onclick="ShowPopup(<%#Eval("CatalogueId") %>)" data-id="<%#Eval("CatalogueId") %>">
                                        <i class="fa fa-edit"></i></a><br /><br />
                                    <a href="javascript" class="btnDeleteProd" data-toggle="modal" id="DeleteCatalogue" data-id='<%#Eval("CatalogueId") %>'>
                                        <i class="fa fa-minus-square"></i></a>
                                    <div class="form-body " id="Catalouge<%#Eval("CatalogueId")%>" data-id="<%#Eval("CatalogueId")%>" style="display: none;">
                                        <input type="hidden" id="hdfCatCode" value="<%#Eval("CatCode")%>" />
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3">Title</label>
                                            <div class="col-md-9">
                                                <p class="form-control-static" id="CatalogueTitle"><%#Eval("CatalogueTitle") %></p>
                                            </div>
                                        </div>
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3">Description</label>
                                            <div class="col-md-9 ">
                                                <p class="form-control-static" id="CatalogueDescription"><%#Eval("CatalogueDescription")%></p>
                                                <p class="form-control-static" data-id="<%#Eval("BrandId") %>" id="CatalogueBrand"><%#Eval("BrandName")%></p>
                                                <p class="form-control-static" id="CatalogueColor"><%#Eval("Color")%></p>
                                                <p class="form-control-static" id="CatalogueSize"><%#Eval("Size")%></p>
                                                <p class="form-control-static" id="CataloguePrice"><%#Eval("Price")%></p>
                                                <p class="form-control-static" id="CatalogueWeight"><%#Eval("Weight")%></p>
                                                <p class="form-control-static" id="CatalogueSKUCode"><%#Eval("SKUCode")%></p>
                                            </div>
                                        </div>
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3">View Catalog</label>
                                            <div class="col-md-9 ">
                                                <p class="form-control-static"><a href=" <%#Eval("CatalogueURL") %>" target="_blank" id="ViewCatalog">View Uploaded Products </a><i class="fa fa-link"></i></p>
                                            </div>
                                        </div>
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3">Category</label>
                                            <div class="col-md-9">
                                                <p class="form-control-static" data-id="<%#Eval("CategoryId") %>" id="CategoryName"><%#Eval("CategoryName") %></p>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Catalog URL</label>
                                            <div class="col-md-9">
                                                <input type="text" id="txtCatalogueURL" class="form-control input-sm" value=" <%#Eval("CatalogueURL") %>" readonly />
                                            </div>
                                        </div>
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3">Embed Code</label>
                                            <div class="col-md-9">
                                                <%--<textarea class="form-control input-sm" id="txtCatalogueEmbedCode" style="height: 85px" readonly> <%#Eval("CatalogueEmbedCode")%></textarea>--%>
                                            </div>
                                        </div>
                                        <%-- <%if ( Eval("VideoURL") != "" && Eval("VideoURL") != null)
                                        { %>--%>
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3">Product Buy URL</label>
                                            <div class="col-md-9">
                                                <p class="form-control-static"><a href="<%#Eval("VideoURL") %>" target="_blank" id="VideoURL"><%#Eval("VideoURL")%></a><i class="fa fa-link"></i></p>
                                            </div>
                                        </div>
                                        <%--  <%} %>--%>

                                        <%--  <%if (Eval("MoreDetailURL") != "" && Eval("MoreDetailURL") != null)
                                        { %>--%>
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3">More Detail</label>
                                            <div class="col-md-9">
                                                <p class="form-control-static"><a href=" <%#Eval("MoreDetailURL") %>" target="_blank" id="MoreDetailURL"><%#Eval("MoreDetailURL") %> </a><i class="fa fa-link"></i></p>
                                            </div>
                                        </div>
                                        <%-- <%} %>--%>

                                        <div class="form-group catalogue" style="margin-top: 5px;" id="tagsBind">
                                            <label class="control-label col-md-3">Tags <a tabindex="0" class="btn-label" role="button" data-toggle="popover" data-trigger="hover" data-content=" Please key in the product or services classification of your catalog, e.g. restaurant, health supplement, cosmetics, welding equipment, beauty salon, mobile phone, etc. for Keyword Search purposes"><i class="fa fa-info-circle"></i></a></label>
                                            <div class="col-md-8">

                                                <input type="text" id="tagsInput" class="form-control" placeholder="Enter your keyword and press ENTER" data-cursor="pointer" style="cursor: pointer;" data-role="tagsinput" data-id=" <%#Eval("CatalogueId")%>" value=" <%#Eval("CatalogueTags") %>" />

                                            </div>
                                            <div class="col-md-1 desk_view">
                                                <img src="../frontend/images/save-tags.jpg" id="imgTags" style="max-width: 60px !important; cursor: pointer;" onclick="AddRemoveTags( <%#Eval("CatalogueId")%>);" />
                                            </div>
                                        </div>
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3"></label>
                                            <div class="col-md-8">

                                                <p style="color: blue; margin-top: -7px;">*Type the keywords comma(,) Seperated</p>

                                            </div>
                                            <div class="col-md-1">
                                            </div>
                                        </div>
                                        <div class="form-group catalogue">
                                            <label class="control-label col-md-3"></label>
                                            <div class="col-md-8">

                                                <p style="color: blue;">*Allow multiple entries</p>

                                            </div>
                                            <div class="col-md-1">
                                            </div>
                                        </div>
                                        <div class="form-group catalogue">
                                            <div style="display: none;" class="col-md-8 mob_view">
                                                <img src="../frontend/images/save-tags.jpg" id="img2" style="max-width: 60px !important; cursor: pointer;" onclick="AddRemoveTags(<%#Eval("CatalogueId")%>);" />
                                            </div>
                                        </div>

                                    </div>
                                </td>
                            </tr>

                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>

                    </asp:Repeater>
                </div>
            </div>
            <div class="clearfix"></div>

        </div>
    </div>
    <div class="modal fade" id="new" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog" style="margin-bottom: 300px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="clearFormData();"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Add New Product</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-body">
                            <div class="form-group">
                                <label class="control-label col-md-3">Title<span class="required" style="color: red;">* </span></label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtAddTitle" placeholder="" />
                                </div>
                            </div>
                            <div class="form-group catalogue">
                                <label class="control-label col-md-3">Product Images<span class="required" style="color: red;">* </span></label>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <input id="file-5" class="file" type="file" multiple="multiple" data-preview-file-type="image/x-png,image/gif,image/jpeg,image/jpg" data-upload-url="#" />
                                        <a id="multiUpload" class="btn btn-primary" style="position: absolute; margin-top: -38px; right: -20px; display: none;">Upload Files</a>
                                    </div>
                                    <div class="help-block">File Format : JPG, GIF, PNG;</div>
                                    <div id="progressbarMultiFile" class="progressbarMultiFile">
                                        <div id="progresslabelMultiFile" class="progressbarlabelMultiFile"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-group catalogue" style="margin-top: 10px;">
                                <div class="col-md-12" id="DivMoreThumbnailNew">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Category  <span class="required" style="color: red;">* </span></label>
                                <div class="col-md-8">
                                    <select class="form-control input-sm input-inline" id="dropAddCategory">
                                        <option value="0">--Please Select Category--</option>
                                        <%  DataTable AddCategoryListing = (DataTable)HttpContext.Current.Session["CategoryListing"]; %>
                                        <% if (AddCategoryListing != null)
                                           { %>
                                        <% DataRow[] menu = AddCategoryListing.Select("MainCategoryId=0"); %>

                                        <%if (menu != null)
                                          { %>
                                        <% foreach (DataRow li in menu)
                                           {%>

                                        <% var CategoryName = li["CategoryName"].ToString();%>
                                        <%int CategoryId = Convert.ToInt32(li["CategoryId"]);%>
                                        <%int MainCategoryId = Convert.ToInt32(li["MainCategoryId"]);%>
                                        <% DataRow[] SubMenu = AddCategoryListing.Select("MainCategoryId='" + CategoryId + "'"); %>

                                        <optgroup label="<%=CategoryName %>">
                                            <%if (SubMenu != null)
                                              { %>
                                            <%foreach (DataRow ul in SubMenu)
                                              {%>
                                            <% var SubCategoryName = ul["CategoryName"].ToString();%>
                                            <%int SubCategoryId = Convert.ToInt32(ul["CategoryId"]);%>
                                            <%int SubMainCategoryId = Convert.ToInt32(ul["MainCategoryId"]);%>

                                            <option value="<%=SubCategoryId %>"><%=SubCategoryName %></option>

                                            <%} %>
                                            <%} %>
                                        </optgroup>

                                        <%} %>
                                        <%} %>
                                        <%} %>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Price</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtPrice" placeholder="Enter Price" maxlength="6" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Weight</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtWeight" placeholder="Enter Product Weight" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">SKU Code</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtSKUCode" placeholder="Enter Product SKU Code" />
                                </div>
                            </div>
                            <div class="form-group" style="position: relative;">
                                <label class="control-label col-md-3">Description  <span class="required" style="color: red;">* </span></label>
                                <div class="col-md-8">
                                    <textarea class="form-control input-sm" style="width:100%;height:150px" id="txtAddDescription" placeholder=""></textarea>
                                </div>
                                <%--<div class="col-md-1" style="position: absolute; right: 0px; bottom: 0px; width: 73px;">
                                    <span id="characters" style="color: #999;">360</span> <span style="color: #999;">left</span>
                                </div>--%>
                            </div>
                            <div class="form-group" style="display:none;">
                                <label class="control-label col-md-3">Brand</label>
                                <div class="col-md-8">
                                    <select class="form-control input-sm input-inline" id="ddlBrand">
                                        <option value="0">--Please Select Brand--</option>
                                        <%  DataTable BrandListing = (DataTable)HttpContext.Current.Session["BrandListing"]; %>
                                        <% if (BrandListing != null)
                                           { %>
                                        <% DataRow[] Brands = BrandListing.Select(); %>

                                        <%if (Brands != null)
                                          { %>
                                        <% foreach (DataRow row in Brands)
                                           {%>

                                        <% var BrandName = row["BrandName"].ToString();%>
                                        <%var BrandId = Convert.ToInt32(row["BrandId"]);%>
                                        <option value="<%=BrandId %>"><%=BrandName %></option>
                                        <%} %>
                                        <%} %>
                                        <%} %>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="control-label col-md-3">Size</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtSize" placeholder="Enter Product Size" />
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="control-label col-md-3">Color</label>
                                <div class="col-md-8">
                                    <select class="form-control input-sm input-inline" id="ddlColor">
                                        <option value="">--Please Select Color--</option>
                                        <%  DataTable ColorListing = (DataTable)HttpContext.Current.Session["ColorListing"]; %>
                                        <% if (ColorListing != null)
                                           { %>
                                        <% DataRow[] colors = ColorListing.Select(); %>

                                        <%if (colors != null)
                                          { %>
                                        <% foreach (DataRow row in colors)
                                           {%>

                                        <% var ColorName = row["ColorName"].ToString();%>
                                        <%var ColorCode = row["ColorCode"].ToString();%>
                                        <option value="<%=ColorCode %>"><%=ColorName %></option>

                                        <%} %>
                                        <%} %>
                                        <%} %>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group" style="display:none;">
                                <label class="control-label col-md-3">Product Buy URL <%--<span class="required" style="color: red;">*--%><%-- </span>--%></label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtAddVideoURL" placeholder="Enter URL of Product Buy" />
                                    <div class="help-block">URL Format : https://www.amazon.in </div>
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="control-label col-md-3">More Detail  </label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtAddMoreURL" placeholder="Enter URL of Related Sites" />
                                    <div class="help-block">URL Format : https://www.amazon.in </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <img src="images/processing.gif" id="img1" style="display: none;" />
                    <button type="button" class="btn btn-primary" id="btnAddNewCatalogue">Save</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="clearFormData();">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="edit" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" onclick="ClearRotation();" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Edit Product</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-body">


                            <div class="form-group">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Title <span class="required" style="color: red;">* </span></label>
                                    <div class="col-md-8">
                                        <input type="text" class="form-control input-sm" id="txtEditTitle" placeholder="" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Product Images </label>
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <input id="file1" class="file" type="file" multiple="multiple" data-preview-file-type="image/x-png,image/gif,image/jpeg,image/jpg" data-upload-url="#" />
                                        <a id="multiUploadEdit" class="btn btn-primary" style="position: absolute; margin-top: -38px; right: -20px; display: none;">Upload Files</a>
                                    </div>
                                    <div class="help-block">File Format : JPG, GIF, PNG;</div>
                                    <div id="progressbarMultiFileEdit" class="progressbarMultiFileEdit">
                                        <div id="progressbarlabelMultiFileEdit" class="progressbarlabelMultiFileEdit"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" style="margin-top: 10px;">
                                <label class="control-label col-md-3"></label>
                                <div class="col-md-8">
                                    <div class="form-group" id="DivMoreThumbnail1New">
                                    </div>
                                    <div class="form-group" id="DivMoreThumbnail1">
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="control-label col-md-3">Category <span class="required" style="color: red;">* </span></label>
                                <div class="col-md-8">

                                    <select class="form-control input-sm input-inline" id="dropCategory">
                                        <option value="0">--Please Select Category--</option>

                                        <%  DataTable EditCategoryListing = (DataTable)HttpContext.Current.Session["CategoryListing"]; %>
                                        <% if (EditCategoryListing != null)
                                           { %>
                                        <% DataRow[] menu = EditCategoryListing.Select("MainCategoryId=0"); %>

                                        <%if (menu != null)
                                          { %>
                                        <% foreach (DataRow li in menu)
                                           {%>

                                        <% var CategoryName = li["CategoryName"].ToString();%>
                                        <%int CategoryId = Convert.ToInt32(li["CategoryId"]);%>
                                        <%int MainCategoryId = Convert.ToInt32(li["MainCategoryId"]);%>
                                        <% DataRow[] SubMenu = EditCategoryListing.Select("MainCategoryId='" + CategoryId + "'"); %>

                                        <optgroup label="<%=CategoryName %>">
                                            <%if (SubMenu != null)
                                              { %>
                                            <%foreach (DataRow ul in SubMenu)
                                              {%>
                                            <% var SubCategoryName = ul["CategoryName"].ToString();%>
                                            <%int SubCategoryId = Convert.ToInt32(ul["CategoryId"]);%>
                                            <%int SubMainCategoryId = Convert.ToInt32(ul["MainCategoryId"]);%>

                                            <option value="<%=SubCategoryId %>"><%=SubCategoryName %></option>

                                            <%} %>
                                            <%} %>
                                        </optgroup>

                                        <%} %>
                                        <%} %>
                                        <%} %>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Price</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtPriceEdit" placeholder="Enter Price" maxlength="6" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">Weight</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtWeightEdit" placeholder="Enter Product Weight" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-3">SKU Code</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtSKUCodeEdit" placeholder="Enter Product SKU Code" />
                                </div>
                            </div>
                            <div class="form-group" style="position: relative;">
                                <label class="control-label col-md-3">Description <span class="required" style="color: red;">* </span></label>
                                <div class="col-md-8">
                                    <textarea rows="7" class="form-control input-sm" style="width:100%;height:150px" id="txtEditDescription" placeholder=""></textarea>
                                </div>
                               <%-- <div class="col-md-1" style="position: absolute; right: 0px; bottom: 0px; width: 73px;">
                                    <span id="characters1" style="color: #999;">360</span> <span style="color: #999;">left</span>
                                </div>--%>
                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="control-label col-md-3">Brand</label>
                                <div class="col-md-8">
                                    <select class="form-control input-sm input-inline" id="ddlBrandEdit">
                                        <option value="0">--Please Select Brand--</option>
                                        <%  DataTable BrandListingEdit = (DataTable)HttpContext.Current.Session["BrandListing"]; %>
                                        <% if (BrandListingEdit != null)
                                           { %>
                                        <% DataRow[] Brands = BrandListingEdit.Select(); %>

                                        <%if (Brands != null)
                                          { %>
                                        <% foreach (DataRow row in Brands)
                                           {%>

                                        <% var BrandName = row["BrandName"].ToString();%>
                                        <%var BrandId = Convert.ToInt32(row["BrandId"]);%>
                                        <option value="<%=BrandId %>"><%=BrandName %></option>
                                        <%} %>
                                        <%} %>
                                        <%} %>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="control-label col-md-3">Size</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtSizeEdit" placeholder="Enter Product Size" />
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="control-label col-md-3">Color</label>
                                <div class="col-md-8">
                                    <select class="form-control input-sm input-inline" id="ddlColorEdit">
                                        <option value="">--Please Select Color--</option>
                                        <%  DataTable ColorListingEdit = (DataTable)HttpContext.Current.Session["ColorListing"]; %>
                                        <% if (ColorListingEdit != null)
                                           { %>
                                        <% DataRow[] colors = ColorListingEdit.Select(); %>

                                        <%if (colors != null)
                                          { %>
                                        <% foreach (DataRow row in colors)
                                           {%>

                                        <% var ColorName = row["ColorName"].ToString();%>
                                        <%var ColorCode = row["ColorCode"].ToString();%>
                                        <option value="<%=ColorCode %>"><%=ColorName %></option>

                                        <%} %>
                                        <%} %>
                                        <%} %>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group" style="display:none;">
                                <label class="control-label col-md-3">Product Buy URL</label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtEditVideoURL" placeholder="Enter URL of Product Buy" />
                                    <div class="help-block">URL Format : https://www.amazon.in</div>
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="control-label col-md-3">More Detail </label>
                                <div class="col-md-8">
                                    <input type="text" class="form-control input-sm" id="txtEditMoreURL" placeholder="Enter URL of Related Sites" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <img src="images/processing.gif" id="imgEditCatalogue" style="display: none;" />
                    <button type="button" class="btn btn-primary" data-id="" data-catcode="" id="btnUpdateCatalouge">Save</button>
                    <button type="button" class="btn btn-default" onclick="ClearRotation();" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <input type="hidden" id="hdfRotateValue" value="" />
    <input type="hidden" id="hdfRotatefileName" value="" />
    <input type="hidden" id="hdfRotateValueForCheck" value="" />
    <input type="hidden" id="hdfFolderPopup" value="" />
    <input type="hidden" id="hdfCatalogIds" value="" />
    <input type="hidden" id="hdfmultipleupload" value="" />
    <input type="hidden" id="hdfRotateImagePath" value="" />
    <input type="hidden" id="CataImageID" value="" />
    <script type="text/javascript" src="js/AddProduct.js"></script>
    <script type="text/javascript" src="js/EditProduct.js"></script>
</asp:Content>

