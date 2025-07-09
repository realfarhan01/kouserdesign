<%@ Page Title="" Language="VB" MasterPageFile="~/web.master" AutoEventWireup="false" CodeFile="productdetail.aspx.vb" Inherits="productdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <section class="pro-content aboutus-content " runat="server" id="divnotfound" visible="false">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="pro-heading-title">
                        <h1>No Product Found</h1>
                    </div>
                </div>
            </div>
        </section>
         <div id="divproduct" class="site-main pt-5" runat="server">
            <div id="main-content" class="main-content">
               <div id="primary" class="content-area">
                  <div  class="site-content" role="main" style="margin-top:50px; margin-bottom: 50px; margin-bottom: 50px;">
                     <div class="shop-details zoom" data-product_layout_thumb="scroll" data-zoom_scroll="true" data-zoom_contain_lens="true" data-zoomtype="inner" data-lenssize="200" data-lensshape="square" data-lensborder="" data-bordersize="2" data-bordercolour="#f9b61e" data-popup="false">
                        <div class="product-top-info">
                           <div class="section-padding">
                              <div class="section-container p-l-r">
                                 <div class="row">
                                    <div class="product-images col-lg-7 col-md-12 col-12">
                                       <div class="row justify-content-end">
                                          <div class="col-md-2">
                                             <div class="content-thumbnail-scroll">
                                                <div class="image-thumbnail slick-carousel slick-vertical" data-asnavfor=".image-additional" data-centermode="true" data-focusonselect="true" data-columns4="4" data-columns3="4" data-columns2="4" data-columns1="4" data-columns="4" data-nav="true" data-vertical="&quot;true&quot;" data-verticalswiping="&quot;true&quot;">
                                                    <asp:Repeater ID="dtlProductThumbnails" runat="server">
                                                        <ItemTemplate>
                                                            <div class="img-item slick-slide">
                                                                <span class="img-thumbnail-scroll">
                                                                    <img width="600" height="600" src="<%# Eval("ThumbnailURL")%>">
                                                                </span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                             </div>
                                          </div>
                                          <div class="col-md-8">
                                             <div class="scroll-image main-image">
                                                <div class="image-additional slick-carousel" data-asnavfor=".image-thumbnail" data-fade="true" data-columns4="1" data-columns3="1" data-columns2="1" data-columns1="1" data-columns="1" data-nav="true">
                                                   <asp:Repeater ID="dtlProductImages" runat="server">
                                                        <ItemTemplate>
                                                            <div class="img-item slick-slide">
                                                              <img width="500" height="500" src="<%# Eval("ThumbnailURL")%>">
                                                           </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                             </div>
                                          </div>
                                       </div>
                                    </div>
                                    <div class="product-info col-lg-5 col-md-12 col-12">
                                       <h1 class="title"><asp:Literal ID="LitProductName" runat="server"></asp:Literal></h1>
                                       <span class="price">
                                          <ins>
                                             <span>INR: <asp:Literal ID="LitPrice" runat="server"></asp:Literal></span>
                                          </ins>
                                       </span>
                                       <div class="product-meta">
                                          <span class="sku-wrapper">SKU: <span class="sku"><asp:Literal ID="LitProductSKUCode" runat="server"></asp:Literal></span>
                                          </span>
                                          <br />
                                          <span class="posted-in">Category: <a href="#." rel="tag"><asp:Literal ID="LitMainCategory" runat="server"></asp:Literal></a>
                                          </span>
                                          <span class="tagged-as">Tags: <a href="#." rel="tag">Hot</a>, <a href="#." rel="tag"><asp:Literal ID="LitCategory" runat="server"></asp:Literal></a>
                                          </span>
                                       </div>
                                       <div class="description">
                                          <p><asp:Literal ID="LitDesc" runat="server"></asp:Literal></p>
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

