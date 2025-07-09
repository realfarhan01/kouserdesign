$(document).ready(function (e) {

    $("body").on("click", "#EditCategory", function () {
        var categoryId = $(this).attr("data-id");
        var categoryName = $(this).attr("data-name");
        var AppearinHomePage = $(this).attr("data-appearinhomepage");
        if (AppearinHomePage == 1) {
            $("#chk").prop("checked", true);
        }
        else {
            $("#chk").prop("checked", false);
        }
        var MainCategoryId = $(this).attr("data-maincategoryid");
        $("#btnUpdateCategory").attr("data-id", categoryId);
        $("#btnUpdateCategory").attr("data-appearinhomepage", AppearinHomePage);
        $("#btnUpdateCategory").attr("data-maincategoryid", MainCategoryId);
        $("#txtEditCategoryName").val(categoryName);
    });
    $("body").on("click", "#EditSubCategory", function () {
        var categoryId = $(this).attr("data-id");
        var categoryName = $(this).attr("data-name");
        var AppearinHomePage = $(this).attr("data-appearinhomepage");

        if (AppearinHomePage == 1) {
            $("#chk").prop("checked", true);
        }
        else {
            $("#chk").prop("checked", false);
        }
        var MainCategoryId = $(this).attr("data-maincategoryid");
        $("#btnUpdateCategory").attr("data-id", categoryId);
        $("#btnUpdateCategory").attr("data-appearinhomepage", AppearinHomePage);
        $("#btnUpdateCategory").attr("data-maincategoryid", MainCategoryId);
        $("#txtEditCategoryName").val(categoryName);
    });
    $("body").on("click", "#btnUpdateCategory", function () {
        var CategoryID = $(this).attr("data-id");
        var MainCategoryId = $(this).attr("data-maincategoryid");
        var txtName = $("#txtEditCategoryName").val();
        var value = $("#chk").is(":checked") ? 1 : 0;
        $.ajax({
            type: "POST",
            url: "ProductCategory.aspx/UpdateCategory",
            data: '{CategoryId:"' + CategoryID + '",CategoryName:"' + txtName + '",MainCategoryId:"' + MainCategoryId + '",AppearinHomePage:"' + value + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var retvalues = response.d;
                if (retvalues == 0)
                {
                    alert("You have no rights to update category");
                }
                else
                {

                var obj = $.parseJSON(response.d);
                var Message, Success;
                $.each(obj.Result, function (index, value) {
                    Success = value.Success;
                    Message = value.Message;
                });
                if (Success != 0) {
                    var id = $("#li" + CategoryID).attr("id");
                    //alert(id);
                    //alert($("#li" + CategoryID).attr("data-maincategoryid"));
                    if ($("#li" + CategoryID).attr("data-maincategoryid") == 0) {
                        $("#li" + CategoryID).find("#categoryName" + CategoryID).html(txtName);
                        $("#li" + CategoryID).find("#EditCategory").attr("data-appearinhomepage", value);
                        $("#li" + CategoryID).find("#EditCategory").attr("data-name", txtName);

                    }
                    else {
                        $("#li" + CategoryID).find("#categoryName" + CategoryID).html(txtName);
                        $("#li" + CategoryID).find("#EditSubCategory").attr("data-appearinhomepage", value);
                        $("#li" + CategoryID).find("#EditSubCategory").attr("data-name", txtName);

                    }
                    $("#txtEditCategoryName").val("");
                    $('#view').modal('hide');
                }
                alert(Message);
                //location.reload();
                }
                
            },
            error: function (response) {
            }
        });

    });
    $("body").on("click", "#btnAddCategory", function () {

        var txtName = $("#txtAddCategory").val();
        var values = $("#chkAddAppear").is(":checked") ? 1 : 0;
        if (txtName == "")
        {
            alert("Please Insert Category Name ");
            return false;
        }

        $.ajax({
            type: "POST",
            url: "ProductCategory.aspx/AddCategory",
            data: '{CategoryName:"' + txtName + '",AppearinHomePage:"' + values + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var retvalues = response.d;
                if (retvalues == 0) {
                    alert("You have no rights to Add category");
                }
                else {

                    var obj = $.parseJSON(response.d);
                    var html = "";
                    var Message, Success, CategoryId, SequenceNo;
                    $.each(obj.Result, function (index, value) {
                        Success = value.Success;
                        Message = value.Message;
                        CategoryId = value.CategoryId;
                        SequenceNo = value.SequenceNo;
                       // html = "<li class='dd-item dd3-item' id='\li" + value.CategoryId + "\' data-maincategoryid='" + 0 + "' data-id='" + value.CategoryId + "'><div class='dd-handle dd3-handle'><i class='fa fa-arrows'></i></div><div class='dd3-content' id='categoryName" + value.CategoryId + "'>" + txtName + "</div><div class='dd-handle-edit dd3-handle-edit'><a class='detail' data-appearinhomepage='" + values + "' data-maincategoryid='" + 0 + "' data-toggle='modal' id='EditCategory' data-name='" + txtName + "' data-id='" + value.CategoryId + "' href='#view'><i class='fa fa-pencil'></i></a></div><div class='dd-handle-end dd3-handle-end'><a class='detail' id='DeleteCategory' data-appearinhomepage='" + values + "' data-id='" + value.CategoryId + "' href='#'><i class='fa fa-times'></i></a></div></li>";
                        //+"<div class='dd3-content' id='categoryName'>" + txtName + "</div>" +
                        //+" <div class='dd-handle-edit dd3-handle-edit'><a class='detail' data-appearinhomepage='" + value + "' data-maincategoryid='" + 0 + "' data-toggle='modal' id='EditCategory' data-name='" + txtName + "' data-id='" + value.CategoryId + "' href='#view'><i class='fa fa-pencil'></i></a></div>" +
                        //+" <div class='dd-handle-end dd3-handle-end'><a class='detail' id='DeleteCategory' data-id='" + value.CategoryId + "' href='#'><i class='fa fa-times'></i></a></div></li>";
                    });
                    if (Success != 0) {

                   // $("#MainOl").append(html)
                        
                    }
                    $("#txtAddCategory").val("");
                    $('#new').modal('hide');
                    alert(Message);
                    location.reload();
                }
            },
            error: function (response) {
            }
        });
    });
    $("body").on("click", "#DeleteCategory,#DeleteSubCategory", function () {
        var li = $(this).closest('li');
        var categorId = $(this).attr("data-id");
        var datamainId = li.attr("data-maincategoryid");
        var liID = li.attr("id");

            if (confirm("Are you sure want to delete this record ??")) {
                $.ajax({
                    type: "POST",
                    url: "ProductCategory.aspx/DeleteCategory",
                    data: '{UniqueId:"' + categorId + '"}',

                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //var result = JSON.stringify(response);
                        var retvalues = response.d;
                        if (retvalues == 0) {
                            alert("You have no rights to delete category");
                        }
                        else {

                            var obj = $.parseJSON(response.d);
                            var Message, Success;
                            var html = "";
                            $.each(obj.Result, function (index, value) {
                                Success = value.Success;
                                Message = value.Message;
                            });
                            if (Success != 0) {

                                $("#MainOl").find("#" + liID).remove();

                            }
                            alert(Message);
                        }
                    },
                    error: function (response) {
                    }
                });
            }

    });
    $("body").on("click", "#btnSaveCategory", function () {
        //var arr = $("textarea#nestable2-output").val();
        var arr = $('#nestable3').data('output').val();
        var TotalCount = GetCategoryTotalCount(JSON.parse(arr), 0);
        var CommaSeperatedCategorySequenceIds = GetCategorySequenceString(JSON.parse(arr), 1, 0, "");
        $.ajax({
            type: "POST",
            url: "ProductCategory.aspx/SetCategorySequence",
            data: '{TotalCount:"' + TotalCount + '",SequenceString:"' + CommaSeperatedCategorySequenceIds + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var retvalues = response.d;
                if (retvalues == 0) {
                    alert("You have no rights to update Category");
                }
                else {
                    var obj = $.parseJSON(response.d);
                    var Message, Success;
                    $.each(obj.Result, function (index, value) {
                        Success = value.Success;
                        Message = value.Message;
                    });
                }
                alert(Message);
                location.reload();

            },
            error: function (response) {

            }
        });

    });
    $("body").on("click", "#btnclearForm", function () {
        clearForm();
    });
    $("body").on("click", "#btnclearFormEdit", function () {
        clearFormEdit();
    });
    
});

function clearForm() {
    $("#txtAddCategory").val("");
    $("#chkAddAppear").attr("checked", false);
}
function clearFormEdit() {
    $("#txtEditCategoryName").val("");
    $("#chk").attr("checked", false);
}
function getcategory() {

    $.ajax({
        type: "POST",
        url: "ProductCategory.aspx/GetCategory",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) { },
        error: function (response)
        { }
    });

}
function GetCategorySequenceString(obj, cnt, parentid, CommaSeperatedBannerSequenceIds) {
    $.each(obj, function (i, item) {
        while (CommaSeperatedBannerSequenceIds.indexOf(cnt + ':') != -1) {
            cnt = cnt + 1;
        }
        CommaSeperatedBannerSequenceIds = CommaSeperatedBannerSequenceIds + cnt + ':' + item.id + '|' + parentid + ',';
        if (item.children != "" && item.children != null) {
            CommaSeperatedBannerSequenceIds = GetCategorySequenceString(item.children, cnt + 1, item.id, CommaSeperatedBannerSequenceIds);
        }
        cnt = cnt + 1;
    });
    return CommaSeperatedBannerSequenceIds;
}
function GetCategoryTotalCount(obj, cnt) {
    $.each(obj, function (i, item) {
        if (item.children != "" && item.children != null) {
            cnt = GetCategoryTotalCount(item.children, cnt);
        }
        cnt = cnt + 1;
    });
    return cnt;
}