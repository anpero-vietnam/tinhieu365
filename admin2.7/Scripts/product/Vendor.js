var Vendor = {
    id: 0,
    data: new FormData(),
    init: function () {
       
        $("#branchFile").smallFileUpload({
            Size: "400x400",       
            allowExtension: ".jpg,.png,.jpeg,.svg",
            success: function (data) {
                $("#brach-thumb").fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
            }
        });
        $("#btn-branch-add").click(function () {
            Vendor.addOrigin();
        });
        setTimeout(function () {
            if (Vendor.curentVendor > 0) {
                $("#origin").val(Vendor.curentVendor);
            }
        }, 1000);
       
    },
    curentVendor: 0,
    DelOrigin: function (_id) {
        var _name = $("#originTxt").val();
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            datatype: "text",
            data: { op: "DelOrigin", id: _id, st: Anpero.CurentStore },
            success: function (rs) {
                if (parseInt(rs) > 0) {
                    Vendor.bindOriginHtml();
                    Common.PlaySuccessSound();
                } else {
                    Common.PlayErrorSound();
                    showNotice("Lỗi", "Thương hiệu này được bảo vệ vì đã có sản phẩm liên quan. Bạn không thể xóa được.");
                }
                
            }

        });
    },  
    addOrigin: function () {
        if (Vendor.getAndValidateData()) {
            $.ajax({
                url: "/Branch/AddBranch",
                data: Vendor.data,
                method: "POST",                
                contentType: false,
                processData: false,
                success: function (rs) {
                    if (rs > 0) {
                        if ($("#origin")) {
                            Vendor.bindOriginSelect();
                        }
                        showNotice("Hệ thống", "Cập nhật thành công.");
                        Vendor.reset();
                    } 
                    $("#originModed").modal("hide");
                    if ($("#branch-content").length > 0) {
                        Vendor.bindOriginHtml();
                    }
                }
            });
        }
    },    
    bindOriginSelect: function () {
        $.ajax({
            type: "post",
            url: "/Branch/bindSelectOrigin",
            datatype: "text",
            data: {st: Anpero.CurentStore },
            success: function (rs) {
                $("#origin").html(rs);
                if (Vendor.curentVendor != null && Vendor.curentVendor != 0) {
                    $('#origin option[value="' + Vendor.curentVendor + '"]').attr('selected', 'selected');
                }

            }

        });

    },
    bindOriginHtml: function () {
        $.ajax({
            type: "post",
            url: "/Branch/GetAllOriginHtml",
            datatype: "text",
            data: { st: Anpero.CurentStore },
            success: function (rs) {
                $("#branch-content").html(rs);
                $(".btn-del-branch").unbind("click");
                    $(".btn-del-branch").click(function () {
                        Vendor.DelOrigin($(this).attr("data-id"));
                });
                $(".btn-update-branch").click(function () {
                    Vendor.id = $(this).attr("data-id");
                    Vendor.bindDataToform();
                    $("#originModed").modal("show");
                    $("#btn-branch-add").html("Cập nhật");
                });
            }
        });
    },
    bindDataToform() {
        $.ajax({
            type: "post",
            url: "/Branch/GetByID",
            datatype: "text",
            data: { st: Anpero.CurentStore, id: Vendor.id },
            success: function (rs) {
                $("#originTxt").val(rs.Name);
                $("#rank").val(rs.Rank);
                $("#brach-thumb").attr("src", rs.Images);
                
                if (rs.Images != "") {
                    $("#brach-thumb").show();
                }
                $("#desc").val(rs.Desc);
                
            }
        });
    },
    bindSelectOriginLink: function () {
        $.ajax({
            type: "post",
            url: "/Branch/BindSelectOriginLink",
            datatype: "text",
            data: {st: Anpero.CurentStore },
            success: function (rs) {
                $("#subLink").html(rs);
                $("#p-2").show();
            }
        });
    },
    reset: function () {
        $("#rank").val(1);
        $("#originTxt").val("");
        $("#desc").val("");
        $("#branchFile").val(""); 
        $("#brach-thumb").attr("src","");
        Vendor.data = new FormData();
        Vendor.id = 0;
        $("#btn-branch-add").html("Thêm mới");
    },
    getAndValidateData: function () {
        var _name = $("#originTxt").val();
        var _desc = $("#desc").val();
        var _rank = $("#rank").val();
        var valid = true;
        if (isNaN(_rank)) {
            alert("Thứ tự chỉ có thể là số.");
            valid = false;
        }
        if (_name.length < 2) {
            alert("Tên quá ngắn vui lòng nhập thêm.");
            valid = false;
        }
        if (_desc.length > 4000) {
            alert("Miêu tả ngắn không lớn hơn 4000 ký tự. Vui lòng nhập lại");
            valid = false;
        }
        var datas = new FormData();
        datas.append("name", _name);
        datas.append("id", Vendor.id);
        datas.append("st", Anpero.CurentStore);
        datas.append("desc", _desc);
        datas.append("rank", _rank);
        datas.append("images", $("#brach-thumb").attr("src"));
        Vendor.data = datas;
        return valid;
    }
};
