


var parentCat = 0;
var isParent = 0;
var parentName = 0;
var Product = {
    categoryId:0,
    CalcSalePercen: function (price, salePrice) {
        price = price.replace(",", "").replace(",", "").replace(",", "").replace(",", "");
        salePrice = salePrice.replace(",", "").replace(",", "").replace(",", "").replace(",", "");
        return parseInt(salePrice / price * 100);

    },
    CalcSalePrice: function (price, percent) {
        price = price.replace(",", "").replace(",", "").replace(",", "").replace(",", "");
        return parseInt(price - price * percent / 100);

    },
    Create: function () {
        var property = [];
        var _location = $("#country option:selected").val();
        var _subLocation = $("#prov option:selected").val();
        $('.priotyList input[type=checkbox]:checked').each(function () {
            property.push({
                Values: $(this).val(),
                Price: $(this).attr("data-price"),
                IsInStock: $(this).attr("data-isinstock")
            });
        });
        var editor = CKEDITOR.instances.editor1;
        var _detail = editor.getData();
        var valid = true;
        var _prName = $("#prName").val();
        var _script = $("#scriptCode").val();
        var _thumbLink = $("#prThumb").attr("src");
        var _price = $("#price").val().replace(",", "").replace(",", "").replace(",", "");
        var _vprice = $("#vprice").val().replace(",", "").replace(",", "").replace(",", "");
        var _type = 0;
        var _shortDesc = $("#shortDesc").val();
        var _specifications = $("#specifications").val();
        var _tag = $("#tag").val();

        var _keywords = $("#keywords").val();
        var _prioty = $("#prioty option:selected").val();
        var _origin = $("#origin option:selected").val();

        var _wr = $("#wr").val();
        if (_wr.trim() == "" || _wr.trim() == null) {
            _wr = "0";
        }
        var _param = $("#param").val();
        var _unit = $("#unit").val();
        if (_price.trim() == "" || _price.trim() == null) {
            _price = 0;
        }
        if (_vprice.trim() == "" || _vprice.trim() == null) {
            _vprice = 0;
        }
        var _isSales = $('#isPublish').is(':checked');
        if (_isSales) { _isSales = 1; } else { _isSales = 0 }
        if (_prName.length < 5) {
            valid = false;
            playSound01();
            showNotice("Thông báo", "Bạn chưa nhập tên / tiêu đề");

        }
        if (Product.categoryId == 0) {
            valid = false;
            playSound01();
            showNotice("Thông báo", "Bạn chưa chọn danh mục.");
        }

        if (valid) {
            var datas = {
                keywords: _keywords,
                shortDesc: _shortDesc,
                prioty: _prioty,
                Name: _prName,
                detail: _detail,
                thumbLink: _thumbLink,
                price: _price,
                SalePrice: _vprice,
                wareHouseId: 0,
                type: _type,
                IsParent: isParent,
                StatusId: 0,
                Warranty: _wr,
                IsSales: _isSales,
                CateGoryId: Product.categoryId,
                parentCatId: parentCat,
                prioties: "",
                origin: _origin,
                script: _script,
                location: _location,
                subLocation: _subLocation,
                specifications: _specifications,
                tag: _tag,
                st: Anpero.CurentStore,
                Property: property
            };
            $.ajax({
                type: "post",
                url: "/pr/CreateProduct",
                data: datas,
                success: function (msg) {
                    editor.setData("");
                    $("#prName").val("");
                    $("#thumbLink").val("");
                    $("#price").val("");
                    $("#vprice").val("");
                    showNotice("Thông báo", "Thêm thành công");
                    Common.PlaySuccessSound();
                    setTimeout(function () {
                        window.location.href = "/pr/update?sp=" + msg + "&st=" + Anpero.CurentStore;
                    }, 3000);

                }
            });
        }
    },
    updatePrTime: function () {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            datatype: "text",
            data: { op: "updatePrTime", id: getParameterByName("sp"), st: Anpero.CurentStore },
            success: function (rs) {
                playSound03();
                showNotice("Cập nhật", "thành công " + rs + " sản phẩm");
            }
        });
    },
    UpdatePr: function () {
        var property = [];
        $('.priotyList input[type=checkbox]:checked').each(function () {
            property.push({
                Id: $(this).val(),
                Values: $(this).val(),
                Price: $(this).attr("data-price"),
                IsInStock: $(this).attr("data-isinstock")
            });
        });

        var _location = $("#country option:selected").val();
        var _subLocation = $("#prov option:selected").val();
        var editor = CKEDITOR.instances.editor1;
        var _detail = editor.getData();
        var valid = true;
        var _prName = $("#prName").val();
        var _thumbLink = $("#prThumb").attr("src");
        var _price = $("#price").val().replace(",", "").replace(",", "").replace(",", "");
        var _vprice = $("#vprice").val().replace(",", "").replace(",", "").replace(",", "");
        var _type = 1;
        var _wr = $("#wr").val();
        var _shortDesc = $("#shortDesc").val();
        var _keywords = $("#keywords").val();
        var _origin = $("#origin option:selected").val();
        var _prioty = $("#prioty option:selected").val();
        var _isSales = $('#isPublish').is(':checked');
        if (_isSales) { _isSales = 1; } else { _isSales = 0 }
        var _saleEndTime = $("#saleEndTime").val();

        if (_saleEndTime.length > 2 && parseFloat(_price.replace(",", "").replace(",", "")) <= parseFloat(_vprice.replace(",", "").replace(",", ""))) {
            if (Common.stringToDate(_saleEndTime, "dd/MM/yyyy", "/") > Date.now()) {
                showNotice("Thông báo", "Giá khuyến mãi phải nhỏ hơn giá bán");
                valid = false;
            }

        }
        if (_prName.length < 5) {
            valid = false;
            Common.PlayErrorSound();
            showNotice("Thông báo", "Bạn chưa nhập tên sản phẩm");

        }
        if (Product.categoryId == 0) {
            valid = false;
            Common.PlayErrorSound();
            showNotice("Thông báo", "Bạn chưa chọn danh mục sản phẩm");
        }
        if (_thumbLink.length < 5) {
            valid = false;
            Common.PlayErrorSound();
            showNotice("Thông báo", "Bạn chưa nhập ảnh");
        }
        var _specifications = $("#Specifications").val();
        var _tag = $("#tag").val();
        var _id = getParameterByName("sp");
        var datas = {

            Specifications: _specifications,
            Tag: _tag, Keywords: _keywords,
            ShortDesc: _shortDesc,
            Prioty: _prioty,
            Name: _prName,
            Detail: _detail,
            ThumbLink: _thumbLink,
            Price: _price,
            SalePrice: _vprice,
            wareHouseId: 0,
            type: _type,
            Warranty: _wr,
            IsSales: _isSales,
            CateGoryId: Product.categoryId,
            Id: _id,
            Origin: _origin,
            Script: $("#scriptCode").val(),
            SaleEndTimeStr: _saleEndTime,
            st: Anpero.CurentStore,
            location: _location, subLocation: _subLocation,
            IsParent: isParent,
            parentCatId: parentCat,
            Property: property
        };
        if (valid) {
            $.ajax({
                type: "post",
                url: "/pr/AjaxUpdateProduct",
                data: datas,
                success: function (msg) {
                    showNotice("Thông báo", "Cập nhật " + msg + " sản phẩm");
                    Common.PlaySuccessSound();
                }
            });
        }
    },
    AddPrPiotiGroup: function () {
        var valid = true;
        var _grDesc = $("#GRPr option:selected").val();
        var check = $(".prig:input");
        for (var i = 0; i < check.length; i++) {
            if ($(check[i]).val() == 0) {
                valid = false;
                showNotice("Lỗi", "Có sản phẩm chưa chọn giá trị thuộc tính. Chưa tạo được nhóm ");
            }
        }

        if (_grDesc == "0") {
            valid = false;
            showNotice("Lỗi", "Bạn chưa chọn tên thuộc tính cho nhóm");
        }

        if (valid) {

            for (var i = 0; i < check.length; i++) {
                var thisId = $(check[i]).attr("id").replace('priotty_', '');
                updatePrPri(thisId);
            }
        }
        if (valid) {
            $.ajax({
                dataType: "text",
                mothod: "post",
                url: "/handler/prpriotyGroup.ashx",
                data: { op: "AddPrPiotyGroup", grDesc: _grDesc, st: Anpero.CurentStore },
                success: function (msg) {
                    BindPrInGroup();
                    showNotice("Thêm nhóm", "Bạn đã thêm " + msg + "nhóm");
                }
            });
        }
    },
    RemoveSale: function () {
        $("#saleEndTime").val("");
        Product.UpdatePr();
    },
    IsInStock: function (isInStock) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            datatype: "text",
            data: { op: "IsInStock", id: getParameterByName("sp"), IsInStock: isInStock, st: Anpero.CurentStore },
            success: function (rs) {
                playSound03();
                if (parseInt(isInStock) == 1 && parseInt(rs) > 0) {
                    showNotice("Cập nhật", "Báo còn hàng thành công");
                    $("#btnInstock").show();
                    $("#btnOuttock").hide();
                }
                if (parseInt(isInStock) == 0 && parseInt(rs) > 0) {
                    showNotice("Cập nhật", "Báo hết hàng thành công");
                    $("#btnInstock").hide();
                    $("#btnOuttock").show();
                }

            }
        });
    },
    bindPrCatLink: function () {
        var typeName = $("#prTypeName").val();
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "bindPrCatLink", st: Anpero.CurentStore },
            success: function (msg) {
                $("#subLink").html(msg);
            }
        });

    },
    bindPrCat: function () {
        var typeName = $("#prTypeName").val();
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "bindPrCat", st: Anpero.CurentStore },
            success: function (msg) {
                $("#prcatSelect").html(msg);
            }
        });
    },
    seachProduct: function (_page) {
        if (_page == null) {
            _page = 1;
        }
        var product_id = $("#product_id").val();
        var productName = $("#product_name").val();
        var _productSn = $("#Pr_seria").val();
        var _category = $("#prcatSelect option:selected").val();
        var _sst = $("#prStatus option:selected").val();

        var valid = true;
        if (valid) {
            $(".productContent").html('<span style="display: block;text-align: center;"><img src="/Images/lightbox-ico-loading.gif" /></span>');
            $.ajax({
                type: "post",
                url: "/pr/SearchProduct",
                data: {
                    Id: product_id == "" ? -1 : product_id,
                    curentPage: _page,
                    PrName: productName,
                    Status: _sst,
                    Category: _category,
                    seria: _productSn,
                    st: Anpero.CurentStore
                },
                success: function (msg) {
                    $(".productContent").html(msg);
                    var type = $("#stName").attr("store-type");
                    switch (type) {
                        case "2":
                            break;
                        case "3":
                            break;
                        case "4":
                            $(".btnAddProduct").hide();
                            $(".c1").hide();
                            break;
                        default:

                    }
                }
            });
        }
    }
};





$(document).ready(function () {

    $("#catTonggo").click(function () {
        Category.BindParentCatSelect();
    });
    $("#parentCat").change(function () {
        parentCat = $("#parentCat option:selected").val();
        parentName = $("#parentCat option:selected").text();
        $("#lbcatNameMd").html("Danh mục con thuộc <strong>" + parentName + "</strong>");
        if (parentCat != 0) {
            $("#parentLink").html($("#parentCat option:selected").attr("data-link"));
            $("#currentParentCt").show();
        } else {
            $("#lbcatNameMd").html("Bạn đang xem tất cả danh mục cha");
            $("#lb-selectedCat").html();
        }
        Category.ParentCategoryID = parentCat;
        Category.BindCatTable();

    });
    $("#changeCat").change(function () {
        parentCat = $("#changeCat option:selected").val();
        parentName = $("#changeCat option:selected").text();
        $("#updateCat").val($("#changeCat option:selected").text());
    });
    $('#Pr_seria').on("keyup", function (e) {
        var code = e.which;
        if (code == 13) {
            getProductBySeria();
        }
    });

});

function BindWHSelect2() {
    var valid = true;
    //if (valid) {
    //    $.ajax({
    //        type: "post",
    //        url: "/handler/orderHandler.ashx",
    //        data: { op: "BindWareHouse3", st: Anpero.CurentStore },
    //        success: function (msg) {
    //            $("#wareHouseId").html(msg);
    //            $("#wareHouseId2").html(msg);
    //        }
    //    });
    //}
}
function BindCashHistoryOfProduct(id) {
}

function getQuantytiInWh() {
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/wareHouse.ashx",
            data: { op: "getQuantytiInWh", id: getParameterByName("sp"), st: Anpero.CurentStore },
            success: function (msg) {
                $("#whCont").html(msg);
            }
        });
    }
}
function sale(_prId) {
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "addPROD", prId: _prId, st: Anpero.CurentStore },
            success: function (msg) {
                BindPROD();
            }
        });
    }
}

function BindPROD() {
    var valid = true;
    var id = getParameterByName("id");
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "BindPROD", OID: id, st: Anpero.CurentStore },
            success: function (msg) {
                if (msg == '') {
                    $('#exempcard').show();
                    $('#cardContent').hide();
                } else {
                    $('#exempcard').hide();
                    $("#cardContent").html(msg);
                    $("#cardContent").addClass("table-scrollable");
                    $('#cardContent').show();
                }
                $(".mask-money").inputmask("decimal", { radixPoint: ".", autoGroup: true, groupSeparator: ",", groupSize: 3 });
                $('.spSeria').on("keyup", function (e) {
                    var code = e.which;
                    var id = this.id;
                    if (code == 13) {
                        UpdatePrOdByseria(id.replace("spSeria_", ""));
                    }
                });
                $('.prVal').on("keyup", function (e) {

                    // var code = e.which;                    
                    var _id = this.id.replace('spprice_', '').replace('spVat_', '').replace('spquantyti_', '');

                    UpdatePrOd(_id, false);
                    getOrderPriceAndVat();

                });
                $('.wrVal').on("keyup", function (e) {
                    var _id = this.id.replace('spprice_', '').replace('spVat_', '').replace('spquantyti_', '').replace('spWr_', '');
                    UpdatePrOd(_id, false);
                    getOrderPriceAndVat();

                });

            }
        });
    }
}
function getOrderPriceAndVat() {
    var valid = true;
    var id = getParameterByName("id");
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "getOrderPriceAndVat", OID: id, st: Anpero.CurentStore },
            success: function (msg) {
                var s = msg.split("|");
                $("#priceCont").html(s[0]);
                $("#vatCont").html(s[1]);

                CalcPayVAT();
                CalcPayment();
                CalcOweVAT();
                CalcRend();
                CalTtp2();
            }
        });
    }
}
function UpdatePrOdByseria(_id) {
    var qt = "#spquantyti_" + _id;
    var price = "#spprice_" + _id;
    var vat = "#spVat_" + _id;
    var sn = "#spSeria_" + _id;
    var quantyti = countLine("#spSeria_" + _id, true);
    $('#spquantyti_' + _id.replace("spSeria_", "")).val(quantyti);
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "updatePrOd", id: _id, quantyti: $(qt).val(), price: $(price).val(), vat: $(vat).val(), isDel: 0, seria: $(sn).val(), st: Anpero.CurentStore },
            success: function (msg) {

            }
        });
    }
}
function UpdatePrOd(_id, isBindAgain) {
    if (isBindAgain == null) { isBindAgain = true; }
    var qt = "#spquantyti_" + _id;
    var price = "#spprice_" + _id;
    var vat = "#spVat_" + _id;
    var sn = "#spSeria_" + _id;
    var wr = "#spWr_" + _id;
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "updatePrOd", id: _id, quantyti: $(qt).val(), price: $(price).val(), vat: $(vat).val(), isDel: 0, seria: $(sn).val(), _wr: $(wr).val(), st: Anpero.CurentStore },
            success: function (msg) {
                if (isBindAgain) {
                    BindPROD();
                }

            }
        })
    }
}
function delPrOd(_id) {
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "updatePrOd", id: _id, isDel: 1, st: Anpero.CurentStore },
            success: function (msg) {
                if (isNaN(msg) && msg != "") {
                    showNotice("Thông báo", msg);
                }
                BindPROD();
            }
        })
    }
}
function addProd(_id) {
    var qt = "#spquantyti_" + _id;
    var price = "#spprice_" + _id;
    var vat = "#spVat_" + _id;
    var nqt = 1;
    if (parseInt($(qt).val()) > 2) {
        nqt = parseInt($(qt).val()) - 1;
    }
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "updatePrOd", id: _id, quantyti: nqt, price: $(price).val(), vat: $(vat).val(), isDel: 0, st: Anpero.CurentStore },
            success: function (msg) {
                BindPROD();
            }
        })
    }
}
var curentCustomId = "";
function delPr() {
    var _id = getParameterByName("sp");
    if (_id > 0) {
        $.ajax({
            type: "post",
            url: "/handler/product.ashx",
            data: { op: "DelPr", id: _id, st: Anpero.CurentStore },
            success: function (msg) {
                if (parseInt(msg) > 0) {
                    showNotice("Thông báo", "Xóa xong " + msg + " sản phẩm");
                    window.location.href = "/pr/List?st=" + Anpero.CurentStore;
                } else {
                    showNotice("Thông báo", "Không xóa được sản phẩm, sản phẩm đã có giao dịch liên quan không thể xóa được");
                }

            }
        });
    }
}
var Category = {
    ParentCategoryID: 0,
    CategoryID: 0,
    BindParentCatSelect: function (elementId) {
        $.ajax({
            type: "post",
            dataType: "text",
            url: "/handler/product.ashx",
            data: { op: "getParentCatSelect", st: Anpero.CurentStore },
            success: function (msg) {
                $(elementId).html(msg);
                Category.BindCatTable();
            }
        });
    },
    AddPrCat: function () {
        var valid = true;
        var _catName = $("#addCat").val();

        var _type = $("#prTypeSelect option:selected").val();
        if (_catName.length < 2) {
            valid = false;
            showNotice("Thông báo", "Tên danh mục sảnh phẩm quá ngắn");
            playSound01();
        }

        if (valid) {
            $.ajax({
                type: "post",
                url: "/handler/product.ashx",
                data: { op: "addCat", catName: _catName, parentCat: Category.ParentCategoryID, type: _type, st: Anpero.CurentStore },
                success: function (msg) {
                    Category.CategoryID = 0;
                    if (msg > 0) {
                        showNotice("Thông báo", "Thêm " + msg + " danh mục sản phẩm thành công");

                        if ($("#catTableContent").length > 0) {
                            Category.BindCatTable();
                        }
                        try {
                            $("#addCat").val("");
                            $('#ztree').jstree("destroy");
                            setNote();
                        } catch (e) {
                            //
                        }
                    }
                }
            });
        }
    },
    DeleteCat: function (_id) {
        $.ajax({
            type: "post",
            dataType: "text",
            url: "/Category/DeleteCateGory",
            data: { id: _id, st: Anpero.CurentStore },
            success: function (msg) {
                if (msg > 0) {
                    Category.ParentCategoryID = 0;
                    Category.BindCatTable();
                    Category.BindParentCatSelect("#changeCat");
                    Category.BindParentCatSelect("#parentCat");
                    playSound02();
                    showNotice("Thông báo", "Xóa thành công " + msg + " danh mục sản phẩm");
                    $("#updatecatNotice").html("Xóa thành công " + msg + " danh mục sản phẩm");
                } else {
                    playSound01();
                    showNotice("Thông báo", "Xóa không thành công, danh mục đã có sản phẩm không xóa được");
                    $("#updatecatNotice").html("Xóa không thành công, danh mục đã có sản phẩm không xóa được");
                }
            }
        });
    },
    UpdateRank: function (_id, _isPlus) {
        $.ajax({
            type: "post",
            url: "/handler/CategoryHandler.ashx",
            data: { op: "UpdateRank", id: _id, isPlus: _isPlus, st: Anpero.CurentStore },
            success: function (msg) {
                Category.BindCatTable();
            }
        });
    },

    BindCatTable: function () {

        $.ajax({
            dataType: "text",
            url: "/pr/GetCategoryTable",
            data: { ParentID: Category.ParentCategoryID, st: Anpero.CurentStore },
            success: function (msg) {
                $("#catTableContent").html(msg);
                $(".update-cat-item").click(function () {
                    $("#changeCat").val($("#parentCat").val());
                });
            }
        });
    },
    ShowUpdate: function (_id,_parenId) {
        Category.CategoryID = _id;
        $("#updateCategoryModel").modal("show");
        $.ajax({
            type: "Post",
            url: "/category/GetCategoryById",
            data: { id: _id, st: Anpero.CurentStore },
            success: function (msg) {
                $("#catPrioty").val(msg.Rank);
                $("#catShotDesc").val(msg.Description);
                $("#catkeywords").val(msg.Keywords);
                $("#CatName").val(msg.Name);
                $("#imgCatThum").attr("src", msg.Images);
                $("#changeCat").val(_parenId);
            }
        });
    },
    AddOrUpdateCat: function () {
        var valid = true;
        // private functions & variables
        var _prioty = $("#catPrioty").val();
        var _desc = $("#catShotDesc").val();
        var _keyword = $("#catkeywords").val();
        var _name = $("#CatName").val();
        var _thumb = $("#imgCatThum").attr("src");
        var _parentCat = $("#changeCat").val();
        if (isNaN(_prioty)) {
            valid = false;
            alert("Độ ưu tiên hiển thị phải là số");
        }
        if (Category.CategoryID == _parentCat && _parentCat != 0) {
            valid = false;
            alert("Danh mục không hợp lệ. Bạn đang chọn chính danh mục hiện tại làm danh mục cha");
        }
        if (valid) {
            $.ajax({
                type: "Post",
                url: "/category/UpdateCateGory",
                data: { id: Category.CategoryID, parentId: _parentCat, prioty: _prioty, desc: _desc, keyword: _keyword, name: _name, thumb: _thumb, st: Anpero.CurentStore },
                success: function (msg) {
                    Category.CategoryID = 0;
                    if (msg > 0) {
                        Category.ResetModal();
                        showNotice("Thông báo", msg + " bản ghi đã được cập nhật thành công");
                        Common.PlaySuccessSound();
                        if ($("#catTableContent").length > 0) {
                            Category.ParentCategoryID = 0;
                            Category.BindCatTable();
                            Category.BindParentCatSelect("#changeCat");
                            Category.BindParentCatSelect("#parentCat");

                        }
                        try {
                            $("#addCat").val("");
                            $('#ztree').jstree("destroy");
                            setNote();
                        } catch (e) {
                            console.log("error");
                        }
                    }
                }
            });
        } else {
            Common.PlayErrorSound();
        }

    },
    ResetModal: function () {
        $("#catPrioty").val("1");
        $("#catShotDesc").val("");
        $("#catkeywords").val("");
        $("#CatName").val("");
        $("#imgCatThum").attr("src", "/images/noimages.png");
        $("#updateCategoryModel").modal("hide");
    }
};


