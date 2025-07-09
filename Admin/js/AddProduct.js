$(document).ready(function () {
    $('#example').dataTable({
        "order": [],
        "columnDefs": [{
            "targets": 'nosorting',
            "orderable": false,
        }],
        "lengthMenu": [[25, 50, 75, 100, -1], [25, 50, 75, 100, "All"]]
    });


    $('#FUAddCatalogue').bind('change', function () {

        var fileSize = this.files[0].size;
        var FileActualSize = fileSize / (1024 * 1024);
        var CatalogueSizeMB = FileActualSize.toFixed(2);
        $("#hdfFileSize").val(FileActualSize.toFixed(2));
        $("#FileSize").html("File Size : " + FileActualSize.toFixed(2) + "MB");
        var RoundValue = Math.round(FileActualSize);
        if (RoundValue > 20) {

            // jAlert("Please Choose File Size Of less than 20MB ", 'Message');
            swal({
                title: "Message",
                text: "Please Choose File Size Of less than 5MB ",
                type: "success"
            },
                function () {

                });
            return false;
        }
        //this.files[0].size gets the size of your file.
        var bannerUpload;
        var fileUpload = $("#FUAddCatalogue").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
            bannerUpload = files[i].name;
        }
        var extension = bannerUpload.substring(bannerUpload.lastIndexOf('.'));
        if (extension.toLowerCase() != ".jpg" && extension.toLowerCase() != ".jpeg" && extension.toLowerCase() != ".gif" && extension.toLowerCase() != ".png" && extension.toLowerCase() != ".pdf") {

            //jAlert("Please select only .jpg or .jpeg or GIF or PNG or PDF file format", 'Message');
            swal({
                title: "Message",
                text: "Please select only .jpg or .jpeg or GIF or PNG or PDF file format",
                type: "success"
            },
                function () {

                });
            $("#FUAddCatalogue").val("");
            return false;
        }
        $("#hdfCatalouge").val(extension.toLowerCase());

        $.ajax({
            type: "POST",
            url: "Products.aspx/UploadCatalogue",
            data: '{CatalogueURL:"' + bannerUpload + '",CatalogueSizeMB:"' + CatalogueSizeMB + '",extension:"' + extension + '"}',
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
                    var Message, Success, CatalogueURL;
                    $.each(obj.Result, function (index, value) {
                        Success = value.Success;
                        Message = value.Message;
                        CatalogueURL = value.CatalogueURL;
                    });

                    if ($("#hdfCatCode").val() == "") {
                        $("#hdfCatCode").val(CatalogueURL);
                    }

                }

            },

        });


    });

    //-----------------------------------------MULTI FILE UPLOAD -------------------------------------------//
    $('#multiUpload').on('click', function () {
        var TotalLength = 0, ThumbnailURL = "";
        TotalLength = $("body").find(".file-preview-frame").length;
        //if (TotalLength <= 0) {
        //    ThumbnailURL = "";
        //    alert("Please select at least one file");
        //    return;
        //}
        var files = document.getElementById("file-5").files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }

        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", function (evt) {
            if (evt.lengthComputable) {
                var progress = Math.round(evt.loaded * 100 / evt.total);
                //$(".progressbarMultiFile").css("display", "block");
                //$("#progressbarMultiFile").progressbar("value", progress);
                //$(".progressbarlabelMultiFile").css("width", progress + "%");
            }
        }, false);
        //$("#progressbarMultiFile").progressbar({
        //    max: 100,
        //    change: function (evt, ui) {
        //        //alert("called");
        //        $("#progresslabelMultiFile").text($("#progressbarMultiFile").progressbar("value") + "%");
        //    },
        //    complete: setInterval(function (evt, ui) {

        //        clearMultiFileInput();
        //    }, 5000)
        //});
        xhr.open("POST", "Handler/AddMyCataloguethumbnail.ashx");
        xhr.onreadystatechange = function () {

            if (xhr.readyState == XMLHttpRequest.DONE) {
                var result = xhr.responseText;
                var obj = $.parseJSON(result);
                var ThumbnailURL = "";
                var CatalogueId = "";
                var sizeInMB = "100MB";
                var isDefault = 0
                $("#DivMoreThumbnailNew").html("");
                $.each(obj, function (index, value) {
                    CatalogueId = value.SNo;
                    ThumbnailURL = value.ImageId;
                    sizeInMB = value.ImageSize;
                    isDefault = value.IsDefault;
                    var ActualThumbnail = ThumbnailURL;

                    ActualThumbnail = ActualThumbnail.replace("Thumbnail", "ActualImage");
                    html = '<div class="CountDiv" style="float:left;margin-left:0px; height: auto !important;margin-top:5px; position:relative;margin-bottom:15px;width: 140px;" id="thumbnailAddCat' + CatalogueId + '"><div class="middle_img" style=" width: 100%;height: 100px;" >';
                    html += '<input type="hidden" id="hdfMoreThumbnailAddCat" value="' + ThumbnailURL + '" /><img src="../' + ThumbnailURL + '" style="border: 0 !important;width: 80px;" onerror="javascript:this.src=' + "'../Files/Thumbnail/noimage.jpg'" + '" id="img2" class="thumbnail-saved" /><div style="text-align: center;"></div></div>';
                    html += '<div class="remove-cat visible-md visible-lg" style="top: -3px;left: 8px;display: initial !important; padding-right: 15px;" ><a href="../' + ActualThumbnail + '" target="_blank"  data-id="' + CatalogueId + '" data-CatalogueImageId="' + CatalogueId + '" id="A3" ><span title="click to enlarge" data-toggle="tooltip" data-placement="top" ><i class="fa fa-camera-retro" ></i></span></a></div>';

                    html += '<div class="remove-cat visible-md visible-lg" style="top: -4px; right: 2px;display: initial !important; padding-right: 15px;" ><a href="javascript:;" id="DeleteMoreThumbnailAddCat" data-id="' + CatalogueId + '" data-CatalogueImageId="' + CatalogueId + '" id="A3" ><span title="Remove" data-toggle="tooltip" data-placement="top" ><i class="fa fa-times" ></i></span></a><p style=" position: absolute;bottom: 0;font-size: 11px;margin-left: 15px;margin-bottom: -15px;" >' + sizeInMB + ' MB</p></div>';
                    if (isDefault == "1")
                        html += '<div style="text-align: center;margin-top:15px;display: inline;"><input type="radio" id="btnRadioAddCat"  data-id="' + CatalogueId + '" name="rdb" onclick="SetDefaultThumbnailAddCat(' + CatalogueId + ');" checked></div>';
                    else
                        html += '<div style="text-align: center;margin-top:15px;display: inline;"><input type="radio" id="btnRadioAddCat"  data-id="' + CatalogueId + '" name="rdb" onclick="SetDefaultThumbnailAddCat(' + CatalogueId + ');"></div>';

                    html += '</div>';
                    debugger;
                    $("#DivMoreThumbnailNew").append(html);
                    $("#progresslabelMultiFile").text("File upload successful!");
                    $(".progressbarMultiFile").css("display", "none");
                    $(".progressbarlabelMultiFile").css("width", "0%");
                    $('#multiUpload').css("display", "none");

                });

            }
        }
        xhr.send(data);
        clearMultiFileInput();

    });
    function clearMultiFileInput() {
        debugger;
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
        $("#file-5").html("");
        $("#file-5").val("");

    }
    // Show "Upload Files" button when user selects a file
    $('#file-5').bind('change', function () {
        var NumOfFiles = this.files.length;
        var TotalSize = 0;
        if (NumOfFiles > 0) {

            $('#multiUpload').css("display", "block");
            //$('#multiUpload').trigger('click');
        }
        //for (i = 0; i < NumOfFiles; i++)
        //    TotalSize = TotalSize + this.files[i].size;
        //var sizeInMB = (TotalSize / (1024 * 1024)).toFixed(2);

    });
    //Upload selected files on click of "Upload Files" button


    $('.fileinput-remove').on('click', function () {
        debugger;
        showHideUploadFileButton();
    });
    $('.kv-file-remove').on('click', function () {
        debugger;
        showHideUploadFileButton();
    });
    $("body").on("click", "#btnAddCatalogue", function () {

        var bannerUpload;
        var fileUpload = $("#FUAddCatalogue").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
            bannerUpload = files[i].name;
        }
        var extension = bannerUpload.substring(bannerUpload.lastIndexOf('.'));
        $("#hdfCatalouge").val(extension);

        $.ajax({
            type: "POST",
            url: "Handler/AddMyCatalogue-Catalogue.ashx",
            data: data,
            processData: false,
            contentType: false,
            success: function (result) {

                // jAlert(result, 'Message');
                //alert(result);
                swal({
                    title: "Message",
                    text: result,
                    type: "success"
                },
                    function () {

                    });

            },

        });


    });
    $("body").on("click", "#btnPDFCatalogue", function () {

        var bannerUpload;
        var fileUpload = $("#filePDFCatalogue").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
            bannerUpload = files[i].name;
        }
        var extension = bannerUpload.substring(bannerUpload.lastIndexOf('.'));
        $("#hdfCatalouge").val(extension);
        $.ajax({
            type: "POST",
            url: "Handler/AddMyCatalogue-Catalogue.ashx",
            data: data,
            processData: false,
            contentType: false,
            success: function (result) {
                //alert(result);
                //  jAlert(result, 'Message');
                swal({
                    title: "Message",
                    text: result,
                    type: "success"
                },
                    function () {

                    });

            },

        });


    });
    //Add New My Catalogues
    $("body").on("click", "#btnAddNewCatalogue", function () {
        $("#TabOpen").val("");

        var CatalogueTitle = $("#txtAddTitle").val();
        var CatalogueDescription = $("#txtAddDescription").val();
        var VideoURL = $("#txtAddVideoURL").val();
        var MoreDetailURL = $("#txtAddMoreURL").val();
        var Weight = $("#txtWeight").val();
        var SKUCode = $("#txtSKUCode").val();
        var CategoryID = $("#dropAddCategory option:selected").val();
        var CategoryName = $("#dropAddCategory option:selected").text();
        var ThumbnailURL = $("#hdfThumbnail").val();
        var CatalogueURL = $("#hdfCatalouge").val();
        var CatalogueSizeMB = $("#hdfFileSize").val();
        var CateCode = $("#hdfCatCode").val();
        //var CatalogueTags = $("#AddNewTagsInput").val();
        CatalogueDescription = CatalogueDescription.replace(/\n/g, "<br>");
        var IsThumbnailSuccess = 0;
        var IsCatalogueSuccess = 0;
        var BrandId = $("#ddlBrand").val();
        var ProductSize = $("#txtSize").val()
        var Color = $("#ddlColor").val()
        var Price = $("#txtPrice").val()

        var TotalLength = 0, ThumbnailURL = "";
        TotalLength = $("body").find(".file-preview-frame").length;
        if (TotalLength > 0) {
            //alert("Please Upload Selected Images First!");
            swal({
                title: "Message",
                text: "Please Upload Selected Images First!",
                type: "success"
            },
                function () {

                });
            return false;
        }

        IsCatalogueSuccess = 1;

        if (CatalogueTitle == "") {
            //jAlert('Please Insert Title !', 'Message');
            swal({
                title: "Message",
                text: "Please Insert Title !",
                type: "success"
            },
                function () {

                });
            //alert("Please Insert Title !");
            return false;
        }
        if (CatalogueDescription == "") {
             //alert("Please Insert Description !");
            swal({
                title: "Message",
                text: "Please Insert Description !",
                type: "success"
            },
                function () {

                });
            return false;
        }
        if ($("#dropAddCategory").val() == 0) {
            //jAlert('Please Choose Category !', 'Message');
            //alert("Please Choose Category !");
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

        if (MoreDetailURL != "") {
            if (re.test(MoreDetailURL.toLowerCase())) {

            } else {
                //alert("invalid More Detail URL");

                // jAlert('invalid More Detail URL', 'Message');

                swal({
                    title: "Message",
                    text: "invalid More Detail URL",
                    type: "success"
                },
                    function () {

                    });
                return false;
            }
        }
        debugger;
        $("#img1").fadeIn();
        $.ajax({
            type: "POST",
            url: "Products.aspx/AddCatalogue",
            data: '{CategoryId:"' + CategoryID + '",CatalogueTitle:"' + CatalogueTitle + '",CatalogueDescription:"' + CatalogueDescription + '",VideoURL:"' + VideoURL + '",MoreDetailURL:"' + MoreDetailURL + '",ThumbnailURL:"' + ThumbnailURL + '",CatalogueURL:"",CatalogueSizeMB:"0",CatalogueTags:"",BrandId:"' + BrandId + '", ProductSize:"' + ProductSize + '", Color:"' + Color + '", Price:"' + Price + '", Weight:"' + Weight + '", SKUCode:"' + SKUCode + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                debugger;

                var SessionOut = response.d;
                var obj = $.parseJSON(response.d);
                if (SessionOut == "SessionOut") {

                    jAlert('Login Session has Expired. Please Login Again.', 'Message');
                    window.location.href = 'logout';
                }
                else {
                    var Message, Success, CatalougeId, CatalogueURL, QRCodeURL, CatalogueEmbedCode, ThumbnailURL, catCode;
                    var html = "";
                    $.each(obj.Result, function (index, value) {

                        Success = value.Success;
                        Message = value.Message;
                        CatalougeId = value.CatalogueId;
                        CatalogueURL = value.CatalogueURL;
                        QRCodeURL = value.QRCodeURL;
                        CatalogueEmbedCode = value.CatalogueEmbedCode;
                        catCode = value.CatCode;
                        ThumbnailURL = value.ThumbnailURL;


                    });


                    $("#TabOpen").val("Open");
                    $("#LatestCatCode").val(catCode);
                    $("#hdfCatCode").val(catCode);
                    $("#Catalouge").prepend(html);
                    $("#file-5").val("")
                    $("#FUAddCatalogue").val("")
                    $("#txtAddTitle").val("")
                    $("#txtWeight").val("")
                    $("#txtSKUCode").val("")
                    $("#txtAddDescription").val("")
                    $("#txtAddVideoURL").val("")
                    $("#txtAddMoreURL").val("")
                    $("#AddNewTagsInput").val("");
                    $("#dropAddCategory").val(0)
                    $("#img1").fadeOut(8000);
                    $('#new').modal('hide');
                    // $("#Catalouge").load(location.href + " #Catalouge");
                    // jAlert(Message, 'Message');
                    //alert(Message);
                    window.location.href = "products.aspx";
                    swal({
                        title: "Message",
                        text: Message,
                        type: "success"
                    },
                        function () {

                        });


                }
            },

        });
    });

});
function LimtCharacters(txtMsg) {
    var CharLength = 360;
    chars = txtMsg.value.length;
    $('#characters').text(CharLength - chars);
    if (chars > CharLength) {
        txtMsg.value = txtMsg.value.substring(0, CharLength);
    }
}
function DelayFunction() {

}
function clearFormData() {

    $.ajax({
        type: "POST",
        url: "products.aspx/clearSessionData",
        //data: '{NewsPostId:"' + PostId + '"}',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {


        },

    });
    $("#file-5").val("");
    $("#FUAddCatalogue").val("");
    $("#txtAddTitle").val("");
    $("#txtAddDescription").val("");
    $("#txtAddVideoURL").val("");
    $("#AddNewTagsInput").val("");
    $("#txtAddMoreURL").val("");
    $("#dropAddCategory").val(0);
    $("#ddlBrand").val(0);
    $("#txtSize").val("");
    $("#txtPrice").val("");
    $("#ddlColor").val("");
    $("#txtWeight").val("")
    $("#txtSKUCode").val("")
}
function SetDefaultThumbnailAddCat(SNo) {
    var CatalogueId = $(this).attr("data-id");
    var CatalogueImageId = $(this).attr("data-catalogueimageid");

    jConfirm('Are you sure you want to make this default ?', 'Notification', function (r) {
        if (r) {
            $.ajax({
                type: "POST",
                url: "products.aspx/SetDefaultThumbnailAddCat",
                data: '{ImageSNo:"' + SNo + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = response.d;
                    if (result == "SessionOut") {
                        jAlert('Login Session has Expired. Please Login Again.', 'Message');
                        window.location.href = 'web-logout';
                    }
                    else {

                    }
                },
            });
        }
    });
}