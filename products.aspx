<%@ Page Title="" Language="VB" MasterPageFile="~/web.master" AutoEventWireup="false" CodeFile="products.aspx.vb" Inherits="products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style>
         .products-thumb a img {
              height: 100%; 
         }
         .block.block-product-cats {
             border-right: 1px solid #f55f1e;
         }
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <div id="site-main" class="site-main">
            <div id="main-content" class="main-content">
               <div id="primary" class="content-area">
                  <div id="content" class="site-content" role="main">
                     <div class="section-padding" style="margin-top:50px;">
                        <div class="section-container p-l-r">
                           <div class="row">
                              <div class="col-xl-3 sidebar left-sidebar md-b-50 p-t-10">
                                 <div class="block block-product-cats">
                                    <div class="block-title">
                                       <h2><a href="products.html">Silver Collection</a></h2>
                                    </div>
                                    <div class="block-content">
                                       <div class="product-cats-list">
                                                                  <ul>
                            <%
                                Dim searchstring As String = ""
                                If Request.QueryString("cat") IsNot Nothing Then
                                    searchstring = Request.QueryString("cat").ToString().ToLower()
                                End If
                                If Len(searchstring) = 0 Then
                                    searchstring = "na"
                                End If

                                 %>
                            <li><a href='cat-na'><span>All Categories</span></a></li>
                                <% Dim dtCategories As DataTable = HttpContext.Current.Session("Categories")

                                    If dtCategories IsNot Nothing Then
                                        Dim menu As DataRow() = dtCategories.Select("MainCategoryId=0")
                                        If menu IsNot Nothing Then
                                            For Each li As DataRow In menu
                                                Dim CategoryId As String = li("CategoryId").ToString()
                                                Dim CategoryName As String = li("CategoryName").ToString()
                                                Dim MainCategoryId As String = li("MainCategoryId").ToString()
                                                Dim MainCategoryName As String = li("MainCategoryName").ToString()
                                                Dim SubMenu As DataRow() = dtCategories.[Select]("MainCategoryId='" & CategoryId & "'")
                                                Dim CategoryCode As String = li("CategoryCode").ToString()
                                                If searchstring = CategoryCode.ToLower() Then
                                                    If SubMenu.Length <> 0 Then
                                                        %>
                                                        <li class='has-sub active'><a href='cat-<%=CategoryCode %>'><span><%=CategoryName %></span></a>
                                                        <%Else%>
                                                        <li class='active'><a href='cat-<%=CategoryCode %>'><span><%=CategoryName %></span></a>

                                                    <% End If %>
                                                    <% Else %>
                                                    <% If SubMenu.Length <> 0 Then %>
                                                                                    <li class='has-sub'><a href='cat-<%=CategoryCode %>'><span><%=CategoryName %></span></a>
                                                                                        <% Else %>
                                                                                        <li><a href='cat-<%=CategoryCode %>'><span><%=CategoryName %></span></a>
                                                                                    <% End If%>
                                                        <%End If%>
                                                    <% If SubMenu.Length <> 0 Then %>
                                                        <ul class="navinner">
                                                            <%For Each ul As DataRow In SubMenu
                                                                    Dim subCategoryName As String = ul("CategoryName").ToString()
                                                                    Dim subCategoryId As String = ul("CategoryId").ToString()
                                                                    Dim subMainCategoryId As String = ul("MainCategoryId").ToString()
                                                                    Dim subMainCategoryName As String = ul("MainCategoryName").ToString()
                                                                    Dim subCategoryCode As String = ul("CategoryCode").ToString()
                                                                    If (searchstring = subCategoryCode.ToLower()) Then%>
                                                                        <li class="activeinner"><a href='cat-<%=subCategoryCode %>'><span><%=subCategoryName %></span></a></li>
                                                                        <% Else %>
                                                                        <li><a href='cat-<%=subCategoryCode %>'><span><%=subCategoryName %></span></a></li>
                                                                    <%End If %>
                                                                <% Next %>
                                                            </ul>
                                                    <%End If%>
                                                </li>
                                                <% Next %>
                                                <% End If %>
                                            <% End If %>
                                         </ul>
                                       </div>
                                    </div>
                                    <hr style="background: #f55f1e;">
                                 </div>
                              </div>
                              <div class="col-xl-9 col-lg-9 col-md-12 col-12">
                                 <div class="tab-content">
                                    <div class="tab-pane fade show active" id="layout-grid" role="tabpanel">
                                       <div class="products-list grid">
                                           <h1 class="text-center"><asp:Literal ID="LitCat" runat="server"></asp:Literal></h1>
                                          <div class="row">
                                              <asp:Repeater ID="dtlMostPopulerProducts" runat="server">
                                                <ItemTemplate>
                                                    <div class="col-xl-3 col-lg-3 col-md-3 col-sm-6">
                                                        <div class="products-entry clearfix product-wapper">
                                                           <div class="products-thumb">
                                                              <a href="<%# Eval("CatCode")%>">
                                                                 <img src="<%# Eval("ThumbnailURL")%>" class="post-image" alt="">
                                                              </a>
                                                           </div>
                                                           <div class="products-content">
                                                              <div class="contents text-center">
                                                                 <h3 class="product-title">
                                                                    <a href="<%# Eval("CatCode")%>"><%# Eval("CatalogueTitle")%></a>
                                                                 </h3>
                                                                 <span class="price"><%# Eval("Price")%></span>
                                                              </div>
                                                           </div>
                                                        </div>
                                                     </div>                                                    
                                                </ItemTemplate>
                                            </asp:Repeater>
                                          </div>
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
            </div>
         </div>
</asp:Content>

