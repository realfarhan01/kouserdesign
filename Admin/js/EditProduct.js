$(document).ready(function (e) {
    // Show "Upload Files" button when user selects a file
    $('#file1').bind('change', function () {
        //var numFiles = $("#file-5")[0].files.length;
        var NumOfFiles = this.files.length;   
        var TotalSize = 0;
        if (NumOfFiles > 0) {
            $('#multiUploadEdit').css("display", "block");
            //$('#multiUploadEdit').click();
        }
        //for (i = 0; i < NumOfFiles; i++)
        //    TotalSize = TotalSize + this.files[i].size;
        //var sizeInMB = (TotalSize / (1024 * 1024)).toFixed(2);

    });


    //Upload selected files on click of "Upload Files" button
    $('#multiUploadEdit').on('click', function () {
        var TotalLength = 0, ThumbnailURL = "";
        //TotalLength = $("body").find(".file-preview-frame").length;
        //if (TotalLength <= 0) {
        //    ThumbnailURL = "";
        //    alert("Please select at least one file");
        //    return;
        //}
        var files = document.getElementById("file1").files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }

        var xhrEdit = new XMLHttpRequest();
        xhrEdit.upload.addEventListener("progress", function (evt) {
            if (evt.lengthComputable) {
                var progress = Math.round(evt.loaded * 100 / evt.total);
                //$(".progressbarMultiFileEdit").css("display", "block");
                //$("#progressbarMultiFileEdit").progressbar("value", progress);
                //$(".progressbarlabelMultiFileEdit").css("width", progress + "%");
            }
        }, false);
        //$("#progressbarMultiFileEdit").progressbar({
        //    max: 100,
        //    change: function (evt, ui) {
        //        //alert("called");
        //        $("#progresslabelMultiFileEdit").text($("#progressbarMultiFileEdit").progressbar("value") + "%");
        //    },
        //    complete: setInterval(function (evt, ui) {
        //    }, 5000)
        //});
        xhrEdit.open("POST", "Handler/AddMyCataloguethumbnail.ashx");
        xhrEdit.onreadystatechange = function () {
            if (xhrEdit.readyState == XMLHttpRequest.DONE) {
                var result = xhrEdit.responseText;
                var obj = $.parseJSON(result);
                var ThumbnailURL = "";
                var CatalogueId = "";
                var sizeInMB = "100MB";
                var isDefault = 0
                $("#DivMoreThumbnail1New").html("");

                $.each(obj, function (index, value) {
                    CatalogueId = value.SNo;
                    ThumbnailURL = value.ImageId;
                    sizeInMB = value.ImageSize;
                    isDefault = value.IsDefault;
                    var ActualThumbnail = ThumbnailURL;

                    ActualThumbnail = ActualThumbnail.replace("Thumbnail", "ActualImage");
                    html = '<div class="CountDiv" style="float:left;margin-left:0px; width: 96px;height: auto !important;margin-top:5px; position:relative;margin-bottom:15px" id="thumbnailAddCat' + CatalogueId + '"><div class="middle_img" style=" width: 100%;height: 100px;" >';
                    html += '<input type="hidden" id="hdfMoreThumbnailAddCat" value="' + ThumbnailURL + '" /><img src="' + ThumbnailURL + '" style="border: 0 !important;" onerror="javascript:this.src=' + "'../Files/Thumbnail/noimage.jpg'" + '" id="img2" class="thumbnail-saved" /><div style="text-align: center;"><p style=" position: absolute;bottom: 0;font-size: 11px;margin-left: 10px;margin-bottom: -15px;" >' + sizeInMB + ' MB</p></div></div>';
                    html += '<div class="remove-cat visible-md visible-lg" style="top: -3px;left: 8px;" ><a href="' + ActualThumbnail + '" target="_blank"  data-id="' + CatalogueId + '" data-CatalogueImageId="' + CatalogueId + '" id="A3" ><span title="click to enlarge" data-toggle="tooltip" data-placement="top" ><i class="fa fa-camera-retro" ></i></span></a></div>';

                    html += '<div class="remove-cat visible-md visible-lg" style="top: -4px; right: 2px;" ><a href="javascript:;" id="DeleteMoreThumbnailAddCat" data-id="' + CatalogueId + '" data-CatalogueImageId="' + CatalogueId + '" id="A3" ><span title="Remove" data-toggle="tooltip" data-placement="top" ><i class="fa fa-times" ></i></span></a></div>';
                    if (isDefault == "1")
                        html += '<div style="text-align: center;"><input type="radio" id="btnRadioAddCat"  data-id="' + CatalogueId + '" name="rdb" onclick="SetDefaultThumbnailAddCat(' + CatalogueId + ');" checked></div>';
                    else
                        html += '<div style="text-align: center;margin-top:15px;"><input type="radio" id="btnRadioAddCat"  data-id="' + CatalogueId + '" name="rdb" onclick="SetDefaultThumbnailAddCat(' + CatalogueId + ');"></div>';

                    html += '</div>';
                    $("#DivMoreThumbnail1New").append(html);
                    $("#progresslabelMultiFile").text("File upload successful!");
                    $(".progressbarMultiFileEdit").css("display", "none");
                    $(".progressbarlabelMultiFile").css("width", "0%");
                    $('#multiUploadEdit').css("display", "none");
                });

            }
        }
        xhrEdit.send(data);
        clearMultiFileInput();
    });
    // Edit Catalogue upload
    $('#filePDFCatalogue').bind('change', function () {
        //this.files[0].size gets the size of your file.
        var fileUpload1 = $("#filePDFCatalogue").get(0);
        var fil = fileUpload1.files;
        if (fil.length < 1) {

            //jAlert("Please select thumbnail", 'Message');

            swal({
                title: "Message",
                text: "Please select thumbnail",
                type: "success"
            },
function () {

});
            $("#filePDFCatalogue").val("");
            return false;
        }

        var fileSize = this.files[0].size;
        var FileActualSize = fileSize / (1024 * 1024);
        var CatalogueSizeMB = FileActualSize.toFixed(2);
        $("#hdfFileSize").val(FileActualSize.toFixed(2));
        $("#FileSize1").html("File Size : " + FileActualSize.toFixed(2) + "MB");
        var RoundValue = Math.round(FileActualSize);
        if (RoundValue > 50) {

            //jAlert("Please choose file size of less than 20MB ", 'Message');

            swal({
                title: "Message",
                text: "Please choose file size of less than 50MB",
                type: "success"
            },
function () {

});
            return false;
        }
        //this.files[0].size gets the size of your file.
        var bannerUpload;
        var fileUpload = $("#filePDFCatalogue").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
            bannerUpload = files[i].name;
        }
        var extension = bannerUpload.substring(bannerUpload.lastIndexOf('.'));
        if (extension.toLowerCase() != ".jpg" && extension.toLowerCase() != ".jpeg" && extension.toLowerCase() != ".gif" && extension.toLowerCase() != ".png" && extension.toLowerCase() != ".pdf") {

            // jAlert("Please select only .jpg or .jpeg or GIF or PNG or PDF file format", 'Message');
            swal({
                title: "Message",
                text: "Please select only .jpg or .jpeg or GIF or PNG or PDF file format",
                type: "success"
            },
function () {

});
            $("#filePDFCatalogue").val("");
            return false;
        }

        $("#hdfCatalouge").val(extension.toLowerCase());

        var CatalogueID = $("#CataImageID").val();
        var CatalogueURL = $("#Catalouge" + CatalogueID).find("#hdfCatCode").val();

        $.ajax({
            type: "POST",
            url: "Products.aspx/EditUploadCatalogue",
            data: '{CatalogueURL:"' + CatalogueURL + '",extension:"' + extension + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var SessionOut = response.d;
                if (SessionOut == "SessionOut") {

                    jAlert('Login Session has Expired. Please Login Again.', 'Message');
                    window.location.href = 'logout.aspx';
                }

            },

        });


    });
    $("body").on("click", "#btnUpdateCatalouge", function () {
        debugger;
        var CatalogueTitle = $("#txtEditTitle").val();
        var descHiddenDIV = document.getElementById("txtEditDescription");
        var CatalogueDescription = $(descHiddenDIV).val();
        CatalogueDescription = CatalogueDescription.replace(/\n/g, "<br>");
        //var CatalogueDescription = $(temp_description).val();
        var VideoURL = $("#txtEditVideoURL").val().trim();
        var MoreDetailURL = $("#txtEditMoreURL").val().trim();
        var CategoryID = $("#dropCategory").val().trim();
        var BrandIdEdit = $("#ddlBrandEdit").val().trim();
        var ProductSizeEdit = $("#txtSizeEdit").val().trim();
        var ColorEdit = $("#ddlColorEdit").val().trim();
        var PriceEdit = $("#txtPriceEdit").val().trim();
        var WeightEdit = $("#txtWeightEdit").val().trim();
        var SKUCodeEdit = $("#txtSKUCodeEdit").val().trim();

        var CateCode = "";
        var ThumbnailURL = "";
        // var CatalogueTags = $("#EditTagsInput").val();
        var Logo = $("#imgLogoFooter").attr("src");
        var CatalogueID = $(this).attr("data-id");
        var RotateValues = $("#hdfRotateValueForCheck").val().trim();
        var CatalogueSizeMB = "";
        //var TotalLength = $("body").find(".file-preview-frame").length;
        //if (TotalLength > 0) {
        //    alert("You have selected Thumbnails yet not uploaded, Please upload those Thumbnails first.");
        //    return false;
        //}
        //var fileUpload2 = $("#fileEditThumbnail").get(0);
        //var files2 = fileUpload2.files;
        //if (files2.length <= 0) {
        //    CateCode = $(this).attr("data-catcode");


        //}
        //else {

        //    var bannerUpload;
        //    var fileUpload = $("#fileEditThumbnail").get(0);
        //    var files = fileUpload.files;
        //    var data1 = new FormData();
        //    for (var i = 0; i < files.length; i++) {
        //        data1.append(files[i].name, files[i]);
        //        bannerUpload = files[i].name;
        //    }
        //    var extension = bannerUpload.substring(bannerUpload.lastIndexOf('.'));
        //    if ($("#hdfThumbnail").val() == "") {
        //        $("#hdfThumbnail").val(extension);
        //    }

        //    UploadThumbnailImageForCatalogue();
        //}
        //var fileUpload1 = $("#filePDFCatalogue").get(0);
        //var files1 = fileUpload1.files;
        //var extensionCatalogueURL = "";
        //if (files1.length <= 0) {
        //    CateCode = $(this).attr("data-catcode");
        //}
        //else {

            //var bannerUpload;
            //var fileUpload = $("#filePDFCatalogue").get(0);
            //var files = fileUpload.files;

            //CatalogueSizeMB = files[0].size;
            //CatalogueSizeMB = CatalogueSizeMB / (1024 * 1024);
            //CatalogueSizeMB = CatalogueSizeMB.toFixed(2);

            //var data = new FormData();
            //for (var i = 0; i < files.length; i++) {
            //    data.append(files[i].name, files[i]);
            //    bannerUpload = files[i].name;
            //}
            //var extension = bannerUpload.substring(bannerUpload.lastIndexOf('.'));
            //extensionCatalogueURL = bannerUpload.substring(bannerUpload.lastIndexOf('.'));
            //if ($("#hdfCatalouge").val() == "") {
            //    $("#hdfCatalouge").val(extension);
            //}
            //$.ajax({
            //    type: "POST",
            //    url: "Handler/AddMyCatalogue-Catalogue.ashx",
            //    data: data,
            //    processData: false,
            //    contentType: false,
            //    success: function (result) {
            //        // alert(result);
            //    },
            //});
            //setTimeout(myFunction, 2000);
        //}

        //if (RotateValues == 1) {
        //    var RotateValue = $("#hdfRotateValue").val();
        //    var fileName = $("#hdfRotatefileName").val();
        //    RotateImageThumbnail(fileName, RotateValue);
        //    $("#hdfRotateValueForCheck").val("");
        //}

        if (CatalogueTitle == "") {

            //jAlert('Please Insert Title !', 'Message');

            swal({
                title: "Message",
                text: "Please Insert Title !",
                type: "success"
            },
    function () {

    });
            return false;
        }
        if (CatalogueDescription == "") {

            // jAlert('Please Insert Description !', 'Message');
            swal({
                title: "Message",
                text: "Please Insert Description !",
                type: "success"
            },
    function () {

    });
            return false;
        }
        if ($("#dropCategory").val() == 0) {

            //jAlert('Please Choose Category !', 'Message');
            swal({
                title: "Message",
                text: "Please Choose Category !",
                type: "success"
            },
    function () {

    });
            return false;
        }
        var re = /(http(s)?:\\)?([\w-]+\.)+[\w-]+[.com|.in|.org]+(\[\?%&=]*)?/

    //    if (VideoURL == "") {
    //        //alert("Please Insert Video URL !");
    //        //return false;
    //    }
    //    else {


    //        var id = matchYoutubeUrl(VideoURL.toLowerCase());
    //        if (id != false) {

    //        } else {

    //            // jAlert('invalid VideoURL', 'Message');

    //            swal({
    //                title: "Message",
    //                text: "invalid VideoURL",
    //                type: "success"
    //            },
    //function () {

    //});
    //            return false;
    //        }
    //    }
        if (MoreDetailURL == "") {
            //alert("Please Insert Video URL !");
            //return false;
        }
        else {


            if (re.test(MoreDetailURL.toLowerCase())) {

            } else {

                // jAlert('invalid MoreDetailURL', 'Message');
                swal({
                    title: "Message",
                    text: "invalid MoreDetailURL",
                    type: "success"
                },
    function () {

    });
                return false;
            }
        }

        var cat = $(this).attr("data-catcode");
        //alert(cat);
        $("#imgEditCatalogue").fadeIn();
        var str = "";
        var k = 0;
        debugger;
        $.ajax({
            type: "POST",
            url: "Products.aspx/GetUploadedThumbnailMultiple",  //pageName.aspx/MethodName
            data: '{}',
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                ThumbnailURL = ThumbnailURL + data.d;
                k = k + 1;
                debugger;
                $.ajax({
                    type: "POST",
                    url: "Products.aspx/UpdateCatalogue",
                    data: '{CatalogueId:"' + CatalogueID + '",CatCode:"' + cat + '",CategoryId:"' + CategoryID + '",CatalogueTitle:"' + encodeURIComponent(CatalogueTitle) + '",CatalogueDescription:"' + encodeURIComponent(CatalogueDescription) + '",VideoURL:"' + VideoURL + '",MoreDetailURL:"' + MoreDetailURL + '",CatalogueURL:"",Logo:"' + Logo + '",CatalogueSizeMB:"",ThumbnailURL:"' + ThumbnailURL + '",BrandId:"' + BrandIdEdit + '", ProductSize:"' + ProductSizeEdit + '", Color:"' + ColorEdit + '", Price:"' + PriceEdit + '", Weight:"' + WeightEdit + '", SKUCode:"' + SKUCodeEdit + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        debugger;
                        var SessionOut = response.d;
                        var obj = $.parseJSON(response.d);
                        if (SessionOut == "SessionOut") {
                            jAlert('Login Session has Expired. Please Login Again.', 'Message');
                            window.location.href = 'web-logout';
                        }
                        else {
                            var Message, Success;
                            $.each(obj.Result, function (index, value) {
                                Success = value.Success;
                                Message = value.Message;
                            });
                            if (Success != 0) {
                                $("#txtEditTitle").val("")
                                $("#txtEditDescription").val("")
                                $("#txtEditVideoURL").val("")
                                $("#txtEditMoreURL").val("")
                                $("#dropCategory").val(0)
                                $("#filePDFCatalogue").val("")
                                $("#fileEditThumbnail").val("")
                                $("#hdfRotateValueForCheck").val("")
                                $("#ddlBrandEdit").val(0);
                                $("#txtSizeEdit").val("");
                                $("#ddlColorEdit").val("");
                                $("#txtPriceEdit").val("");
                                $("#txtWeightEdit").val("");
                                $("#txtSKUCodeEdit").val("");
                            }
                            $("#TabOpen").val("Open");
                             //jAlert(Message, 'Message');
        //                    swal({
        //                        title: "Message",
        //                        text: Message,
        //                        type: "success"
        //                    },
        //function () {
        //    window.location.href = "Products.aspx";
        //});
                            $('#edit').modal('hide');
                            $("#imgEditCatalogue").fadeOut(8000);
                        }
                    },
                  
                });
            },
        });
    });

    $("body").on("click", "#DeleteCatalogue", function () {
        var CatalogueId = $(this).attr("data-id");
        jConfirm('Are you sure you want to remove this product?', 'Notification', function (r) {

            if (r) {
                $.ajax({
                    type: "POST",
                    url: "Products.aspx/DeleteCatalogue",
                    data: '{CatalogueId:"' + CatalogueId + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var SessionOut = response.d;
                        if (SessionOut == "SessionOut") {

                            jAlert('Login Session has Expired. Please Login Again.', 'Message');
                            window.location.href = 'logout';
                        }
                        else {
                            var obj = $.parseJSON(response.d);
                            var Message, Success;

                            $.each(obj.Result, function (index, value) {
                                Success = value.Success;
                                Message = value.Message;
                            });
                            // $("#Catalouge" + CatalogueId).remove();                     
                            jAlert(Message, 'Message');
                            //                            swal({
                            //                                title: "Message",
                            //                                text: Message,
                            //                                type: "success"
                            //                            },
                            //function () {


                            //});
                            window.location.href = "Products.aspx";

                          
                        }

                    },

                });
            }
        });

    });

    $("body").on("click", "#DeleteMoreThumbnail", function () {
        var CatalogueId = $(this).attr("data-id");
        var CatalogueImageId = $(this).attr("data-catalogueimageid");
        jConfirm('Are you sure you want to remove this ?', 'Notification', function (r) {
            if (r) {
                $.ajax({
                    type: "POST",
                    url: "Products.aspx/DeleteMoreThumbnail",
                    data: '{CatalogueImageId:"' + CatalogueImageId + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var SessionOut = response.d;
                        if (SessionOut == "SessionOut") {
                            jAlert('Login Session has Expired. Please Login Again.', 'Message');
                            window.location.href = 'logout.aspx';
                        }
                        else {
                            var obj = $.parseJSON(response.d);
                            var Message, Success;
                            $.each(obj.Result, function (index, value) {
                                Success = value.Success;
                                Message = value.Message;
                            });
                            swal({
                                title: "Message",
                                text: Message,
                                type: "success"
                            },
                            function () {
                                $("#thumbnail" + CatalogueImageId).remove();
                                var NoOfDivs1 = $("#DivMoreThumbnail1 .CountDiv").length;
                                if (NoOfDivs1 == 0) {
                                    $("#Catalouge" + CatalogueId).find("#imgThumbnailURL").attr("src", "../Files/Thumbnail/noimage.jpg");
                                }

                            });
                        }
                    },
                });
            }
        });

    });

    $("body").on("click", "#DeleteMoreThumbnailAddCat", function () {
        var CatalogueId = $(this).attr("data-id");
        var CatalogueImageId = $(this).attr("data-catalogueimageid");
        jConfirm('Are you sure you want to remove this ?', 'Notification', function (r) {
            if (r) {
                $.ajax({
                    type: "POST",
                    url: "Products.aspx/DeleteThumbnailFromDataTable",
                    data: '{ImageSNo:"' + CatalogueImageId + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var result = response.d;
                        if (result == "success") {
                            $("#thumbnailAddCat" + CatalogueImageId).remove();
                            var NoOfDivs1 = $("#DivMoreThumbnailNew .CountDiv").length;
                            //alert(NoOfDivs1);
                            if (NoOfDivs1 == 0) {
                                $("#Catalouge" + CatalogueId).find("#imgThumbnailURL").attr("src", "../Files/Thumbnail/noimage.jpg");
                            }
                        }
                        else {
                            alert("Thumbnail could not remove, please try again.")
                        }
                    },
                });
            }
        });

    });

});

function matchYoutubeUrl(url) {
    var p = /^(?:https?:\/\/)?(?:www\.)?(?:youtu\.be\/|youtube\.com\/(?:embed\/|v\/|watch\?v=|watch\?.+&v=))((\w|-){11})(?:\S+)?$/;
    var matches = url.match(p);
    if (matches) {
        return matches[1];
    }
    return false;
}
function LimtCharacters1(txtMsg) {
    var CharLength = 360;
    chars = txtMsg.value.length;
    $('#characters1').text(CharLength - chars);
    if (chars > CharLength) {
        txtMsg.value = txtMsg.value.substring(0, CharLength);
    }
}

function ClearRotation() {
    $("#hdfRotateValueForCheck").val("");
    var TotalLength = $("body").find(".file-preview-frame").length;
    var previewIdarr = new Array();
    if (TotalLength > 0) {
        for (i = 0; i < TotalLength; i++) {
            var previewId = $("body").find(".file-preview-frame").eq(i).attr("id");
            previewIdarr[i] = previewId;
        }
        for (j = 0; j < previewIdarr.length; j++) {
            $("#" + previewIdarr[j]).remove();
        }
    }
}
function ShowPopup(dataId) {
    debugger;
    $.ajax({
        type: "POST",
        url: "Products.aspx/CheckAddUpdateCataloguePermission",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            var SessionOut = result.d;
            if (SessionOut == "SessionOut") {

                jAlert('Login Session has Expired. Please Login Again.', 'Message');
                window.location.href = 'logout.aspx';
            }
            else {
                getMoreThumbnails(dataId);
                $("#CataImageID").val(dataId);
                //alert($("#CataImageID").val());
                $("#btnUpdateCatalouge").attr("data-id", dataId);
                var id = $("#Catalouge" + dataId).attr("data-id");
                var CatalogueTitle = $("#Catalouge" + dataId).find("#CatalogueTitle").html().trim();
                var CatalogueDescription = $("#Catalouge" + dataId).find("#CatalogueDescription").html().trim();
                //CatalogueDescription = CatalogueDescription.replace("<br/>", "\n").replace("<br>", "\n");
                CatalogueDescription = CatalogueDescription.replace(/<br\s*[\/]?>/gi, "\n")
                // CatalogueDescription = CatalogueDescription.replace("<br>", "\n");
                var CategoryName = $("#Catalouge" + dataId).find("#CategoryName").html().trim();
                var CategoryID = $("#Catalouge" + dataId).find("#CategoryName").attr("data-id");
                var VideoURL = $("#Catalouge" + dataId).find("#VideoURL").html().trim();
                var MoreDetailURL = $("#Catalouge" + dataId).find("#MoreDetailURL").html().trim();

                if (MoreDetailURL != undefined)
                    MoreDetailURL = MoreDetailURL.replace(/\s+/, "");
                var BrandId = $("#Catalouge" + dataId).find("#CatalogueBrand").attr("data-id");
                var Color = $("#Catalouge" + dataId).find("#CatalogueColor").html().trim();
                var Size = $("#Catalouge" + dataId).find("#CatalogueSize").html().trim();
                var Price = $("#Catalouge" + dataId).find("#CataloguePrice").html().trim();
                var Weight = $("#Catalouge" + dataId).find("#CatalogueWeight").html().trim();
                var SKUCode = $("#Catalouge" + dataId).find("#CatalogueSKUCode").html().trim();

                var CateCode = $("#Catalouge" + dataId).find("#hdfCatCode").val().trim();
                //var tagsInput = $("#Catalouge" + dataId).find("#tagsInput").val();
                var CatalogueDownloadURL = $("#Catalouge" + dataId).find("#txtCatalogueURL").val().trim();
                var imgThumbnailURL = $("#Catalouge" + dataId).find("#imgThumbnailURL").attr("src");
                $("#btnUpdateCatalouge").attr("data-catcode", CateCode);
                $("#hdfDATAID").val(CateCode);
                
                $("#imgEditThumbnail").attr("src", imgThumbnailURL);
                $("#linkEditCatalogue").attr("href", "../"+CatalogueDownloadURL);
                $("#txtEditTitle").val(CatalogueTitle)
                $("#txtEditDescription").val(CatalogueDescription)
                $("#txtEditVideoURL").val(VideoURL)
                $("#txtEditMoreURL").val(MoreDetailURL)
                var isExists = 0;

                var exists = $('#dropCategory option').filter(function () { if ($(this).val() == CategoryID) { isExists = 1; return;} }).length;               
                if (isExists > 0) {
                    $("#dropCategory").val(CategoryID)
                }
                else {
                    $("#dropCategory").val(0)
                }
               
                $("#txtSizeEdit").val(Size)
                $("#txtPriceEdit").val(Price)
                $("#ddlBrandEdit").val(BrandId)
                $("#ddlColorEdit").val(Color)
                $("#txtSizeEdit").val(Size)
                $("#txtWeightEdit").val(Weight)
                $("#txtSKUCodeEdit").val(SKUCode)
               
                $("#edit").modal("show");
            }
        },

    });
}
function getMoreThumbnails(dataId) {

    $.ajax({
        type: "POST",
        url: "Products.aspx/GetMoreThumbnail",
        data: '{CatalogueId:"' + dataId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var SessionOut = response.d;
            if (SessionOut == "SessionOut") {
                jAlert('Login Session has Expired. Please Login Again.', 'Message');
                window.location.href = 'web-logout';
            }
            else {
                var obj = $.parseJSON(response.d);
                var CatalogueImageId, CatalogueId, ThumbnailURL, html, SizeMB, isDefault;
                $("#DivMoreThumbnail1").empty();
                $.each(obj.Result, function (index, value) {
                    CatalogueImageId = value.CatalogueImageId;
                    CatalogueId = value.CatalogueId;
                    ThumbnailURL = value.ThumbnailURL;
                    SizeMB = value.SizeMB;
                    isDefault = value.isDefault;
                    var ActualThumbnail = ThumbnailURL;
                    ActualThumbnail = ActualThumbnail.replace("Thumbnail", "ActualImage");
                    html = '<div class="CountDiv" style="float:left;margin-left:0px; height: auto !important;margin-top:5px; position:relative;margin-bottom:15px;width: 140px;" id="thumbnail' + CatalogueImageId + '"><div class="middle_img" style=" width: 100%;height: 100px;" >';
                    html += '<input type="hidden" id="hdfMoreThumbnail" value="' + ThumbnailURL + '" /><img src="' + ThumbnailURL + '" style="border: 0 !important;width: 80px;" onerror="javascript:this.src=' + "'../Files/Thumbnail/noimage.jpg'" + '" id="img2" class="thumbnail-saved" /><div style="text-align: center;"><p style=" position: absolute;bottom: 0;font-size: 11px;margin-left: 10px;margin-bottom: -15px;" >' + SizeMB + ' MB</p></div></div>';
                    html += '<div class="remove-cat visible-md visible-lg" style="top: -3px;left: 8px;display: initial !important; padding-right: 15px;" ><a href="' + ActualThumbnail + '" target="_blank"  data-id="' + CatalogueId + '" data-CatalogueImageId="' + CatalogueImageId + '" id="A3" ><span title="click to enlarge" data-toggle="tooltip" data-placement="top" ><i class="fa fa-camera-retro" ></i></span></a></div>';
                    if (CatalogueImageId != 0) {
                        html += '<div class="remove-cat visible-md visible-lg" style="top: -4px; right: 2px;display: initial !important; padding-right: 15px;" ><a href="javascript:;" id="DeleteMoreThumbnail" data-id="' + CatalogueId + '" data-CatalogueImageId="' + CatalogueImageId + '" id="A3" ><span title="Remove" data-toggle="tooltip" data-placement="top" ><i class="fa fa-times" ></i></span></a></div>';
                    }
                    //html += '<p style="text-align: center;" >Size : ' + SizeMB + ' MB</p>';
                    if (isDefault == 1) {
                        html += '<div style="text-align: center;margin-top:15px;display: inline;"><input type="radio" id="btnRadio"  data-id="' + CatalogueImageId + '" name="rdb" onclick="SetDefaultThumnail(' + CatalogueId + ',' + CatalogueImageId + ');" checked /></div>';
                    }
                    else {
                        html += '<div style="text-align: center;margin-top:15px;display: inline;"><input type="radio" id="btnRadio"  data-id="' + CatalogueImageId + '" name="rdb" onclick="SetDefaultThumnail(' + CatalogueId + ',' + CatalogueImageId + ');"></div>';
                    }
                    html += '</div>';
                    $("#DivMoreThumbnail1").append(html);
                });

            }
        },
    });
}
function SetDefaultThumnail(CatalogueId, CatalogueImageId) {
    //alert(CatalogueId +","+CatalogueImageId);
    $.ajax({
        type: "POST",
        url: "Products.aspx/SetCatalogueDefaultImage",
        data: '{CatalogueId:"' + CatalogueId + '",CatalogueImageId:"' + CatalogueImageId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var SessionOut = response.d;
            if (SessionOut == "SessionOut") {
                jAlert('Login Session has Expired. Please Login Again.', 'Message');
                window.location.href = 'web-logout';
            }
            else {
                var obj = $.parseJSON(response.d);
                var Message, Success;
                $.each(obj.Result, function (index, value) {
                    Success = value.Success;
                    Message = value.Message;
                });
                swal({
                    title: "Message",
                    text: Message,
                    type: "success"
                },
                    function () {
                    });

                var ThumbnailURL = $("#thumbnail" + CatalogueImageId).find("#img2").attr("src");
                //var Thumbnail = $("#thumbnail" + CatalogueImageId).find("#hdfMoreThumbnail").val();
                //alert(ThumbnailURL + ' ' + Thumbnail);
                $("#Catalouge" + CatalogueId).find("#imgThumbnailURL").attr("src", ThumbnailURL);
            }
        },
    });
}

