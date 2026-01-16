//function addwh() {
//    BindWHSelect();
//    BindWhTable();
//    var valid = true;
//    name = $("#whN").val();
//    if (name == null || name == "") {
//        valid = false;
//        showNotice("Hệ thống", "Tên kho quá ngắn");
//        showNotify("#whN");
//    }
//    if (valid) {
//        $.ajax({
//            type: "post",
//            url: "/handler/orderHandler.ashx",
//            data: { op: "addwareHouse", names: name, st: Anpero.CurentStore },
//            success: function (msg) {
//                if (msg == 1) {
//                    BindWHSelect();
//                    BindWhTable();
//                    showNotice("Hệ thống", " Cập thành công");
//                    Common.PlaySuccessSound();

//                } else {
//                    showNotice("Hệ thống", " Cập Lỗi");
//                    Common.PlayErrorSound();
//                }
//            }
//        });
//    }
//}
//function BindWHSelect() {
//    var valid = true;
//    if (valid) {
//        $.ajax({
//            type: "post",
//            url: "/handler/orderHandler.ashx",
//            data: { op: "BindWareHouse2", st: Anpero.CurentStore },
//            success: function (msg) {
//                $("#whcontent").html(msg);
//            }
//        });
//    }
//}
//function BindWhTable() {
//    var valid = true;
//    if (valid) {


//        $.ajax({
//            type: "post",
//            url: "/handler/orderHandler.ashx",
//            data: { op: "BindWareHouse", st: Anpero.CurentStore },
//            success: function (msg) {
//                $("#tablewh").html(msg);
//            }
//        });
//    }
//}
//function BindProductType2() {
//    var valid = true;
//    if (valid) {
//        $.ajax({
//            type: "post",
//            url: "/handler/orderHandler.ashx",
//            data: { op: "BindProductType2", st: Anpero.CurentStore },
//            success: function (msg) {
//                $("#prType").html(msg);
//            }
//        });
//    }
//}
//function delWh(id) {
//    $.ajax({
//        type: "post",
//        url: "/handler/orderHandler.ashx",
//        data: { op: "delWh", id: id, st: Anpero.CurentStore },
//        success: function (msg) {
//            if (parseInt(msg) > 0) {
//                showNotice("Hệ thống", "Bạn đã xóa 1 kho");
//                BindWhTable();
//                BindWHSelect();
//                Common.PlaySuccessSound();
//            } else {
//                showNotice("Hệ thống", "Xóa thất bại vì Kho bãi này đã có giao dịch liên quan");
//                Common.PlayErrorSound();
//            }

            
//        }
//    });
//}
//function UpdateWh(id) {
//    var valid = true;

//    var name = $("#wh" + id).val();
//    if (valid) {

//        $.ajax({
//            type: "post",
//            url: "/handler/orderHandler.ashx",
//            data: { op: "UpdateWh", id: id, name: name, st: Anpero.CurentStore },
//            success: function (msg) {
//                BindWhTable();
//                BindWareHouse2();

//            }
//        });
//    }
//}
//function s(page) {

//    var _prDetail = $("#prDetail").val();
//    var _makh = $("#makh").val();
//    var _prType = $("#prType :selected").val();
//    var _prSst = $("#prSst :selected").val();

//    var valid = true;
//    if (valid) {
//        $.ajax({
//            type: "post",
//            url: "/handler/orderHandler.ashx",
//            data: { op: "searchPr", prDetail: _prDetail, makh: _makh, prType: _prType, prSst: _prSst, p: page, st: Anpero.CurentStore },
//            success: function (msg) {
//                $("#prSearchContent").html(msg);


//            }
//        });
//    }

//}
//function addWareHouseCard() {
//    var valid = true;
//    var _sst = $("#sst option:selected").val();
//    var _soluong = $("#sl").val();
//    if (_soluong == 0 || _soluong == "" || parseInt(_soluong) <= 0) {
//        valid = false;
//        showNotice("Hệ thống", "Số lượng không hợp lệ");
//        Common.PlayErrorSound()
//    }
//    var _prid = $("#prid").val();
//    var _seria = $("#seria").val();
//    var _detail = $("#detail").val();

//    var _Type = $('input[name="tp"]:checked').val();
//    if (_Type == null) {
//        valid = false;
//        showNotice("Hệ thống", "Vui lòng chọn loại thẻ nhập hay xuất ?");
//    }
//    var _whId = $("#wareHouseId option:selected").val();
//    if (_whId == 0) {
//        valid = false;
//        showNotice("Hệ thống", "Vui lòng chọn kho");
//    }
//    if (valid) {
//        $.ajax({
//            type: "post",
//            url: "/handler/wareHouse.ashx",
//            data: { op: "addWareHouseCard", sst: _sst, sl: _soluong, prId: _prid, whId: _whId, Type: _Type, seria: _seria, detail: _detail, st: Anpero.CurentStore },
//            success: function (msg) {
//                if (parseInt(msg) > 0) {
//                    showNotice("Hệ thống", "Cập nhật thành công  thẻ kho số" + msg);
//                    $("#sl").val("0");
//                }
//            }
//        });
//    }
//}
//function exportWhHistory() {

//    var valid = true;
//    var _isInport = $('input[name="ex"]:checked').val();
//    if (_isInport == null) {
//        _isInport = "-1";
//    }
//    var _check = $('input[name="check"]:checked').val();
//    if (_check == null) {
//        _check = "-1";
//    }
//    var _beginDate = "0";
//    var _endDate = "299912312460";
//    if ($("#beginDate").val() != null) {
//        _beginDate = $("#beginDate").val();
//    }
//    var _whId = $("#wareHouseId option:selected").val();
//    if ($("#endDate").val() != null) {
//        _endDate = $("#endDate").val();
//    }
//    if (valid) {
//        $("#inportHistoty").html('<img src="/Images/lightbox-ico-loading.gif" />');
//        $.ajax({
//            type: "post",
//            url: "/handler/wareHouse.ashx",
//            data: { op: "exportWhHistory", _prId: getParameterByName("sp"), isInport: _isInport, check: _check, page: 1, beginDate: _beginDate, endDate: _endDate, whId: _whId, st: Anpero.CurentStore },
//            success: function (msg) {
//                if (msg > 0) {
//                    alert("Đã lưu ra file " + msg + " bản ghi");
//                    window.location.href = "/a/data.txt";
//                }

//            }
//        });
//    }
//}
//function getInPortByProductId(_page) {
//    if (_page == null) {
//        _page = 1;
//    }
//    var valid = true;
//    var _isInport = $('input[name="ex"]:checked').val();
//    if (_isInport == null) {
//        _isInport = "-1";
//    }
//    var _check = $('input[name="check"]:checked').val();
//    if (_check == null) {
//        _check = "-1";
//    }
//    var _beginDate = "0";
//    var _endDate = "299912312460";
//    if ($("#beginDate").val() != null) {
//        _beginDate = $("#beginDate").val();
//    }
//    var _whId = $("#wareHouseId option:selected").val();
//    if ($("#endDate").val() != null) {
//        _endDate = $("#endDate").val();
//    }
//    if (valid) {
//        $("#inportHistoty").html('<img src="/Images/lightbox-ico-loading.gif" />');
//        $.ajax({
//            type: "post",
//            url: "/handler/wareHouse.ashx",
//            data: { op: "getInportByPrId", _prId: getParameterByName("sp"), isInport: _isInport, check: _check, page: _page, beginDate: _beginDate, endDate: _endDate, whId: _whId, st: Anpero.CurentStore },
//            success: function (msg) {
//                $("#inportHistoty").html(msg);                           
//            }
//        });
//    }
//}
//function getTranByProductId(_page) {
//    if (_page == null) {
//        _page = 1;
//    }
//    var __prid = "sp" + getParameterByName("sp");
//    if (__prid == null) {
//        __prid = "%";
//    }
//    var valid = true;
//    var _type = $('input[name="tran"]:checked').val();
//    if (_type == null) {
//        _type = "0";
//    }
//    if (valid) {
//        $("#TransHistoty").html('<img src="/Images/lightbox-ico-loading.gif" />');
//        $.ajax({
//            type: "post",
//            url: "/handler/cashHandler.ashx",
//            data: { op: "getCahOfProduct", _prId: __prid, type: _type, page: _page, st: Anpero.CurentStore },
//            success: function (msg) {
//                $("#TransHistoty").html(msg);
//            }
//        });
//    }
//}
//function seachImportProduct(_page) {
//    if (_page == null) {
//        _page = 1;
//    }
//    var product_id = $("#product_id").val();
//    var productName = $("#product_name").val();
//    var _productSn = $("#Pr_seria").val();
//    var _wh = $("#wareHouseId2 option:selected").val();
//    var _type = 1;//$("#prType option:selected").val();
//    var _sst = $("#prStatus option:selected").val();

//    var valid = true;
//    if (valid) {
//        $(".productContent").html('<img src="/Images/lightbox-ico-loading.gif" />');
//        $.ajax({
//            type: "post",
//            url: "/handler/wareHouse.ashx",
//            data: { op: "seachImportProduct", prId: product_id, type: _type, page: _page, prName: productName, productSn: _productSn, sst: _sst, wh: _wh, st: Anpero.CurentStore },
//            success: function (msg) {
//                $(".productContent").html(msg);             
//            }
//        });
//    }
//}
function getProductBySeria() {

    var _seria = $("#Pr_seria").val();
    var valid = true;
    if (valid) {
        $(".productContent").html('<img src="/Images/lightbox-ico-loading.gif" />');
        $.ajax({
            type: "post",
            url: "/handler/wareHouse.ashx",
            data: { op: "getProductBySeria", seria: _seria,st:Anpero.CurentStore },
            success: function (msg) {
                $(".productContent").html(msg);             
                $("#Pr_seria").val("");
            }
        });
    }
}
function updateToChecked(_id) {
    $.ajax({
        type: "post",
        url: "/handler/wareHouse.ashx",
        data: { op: "updateToChecked", id: _id, isDelete: 0, st: Anpero.CurentStore },
        success: function (msg) {
            if (parseInt(msg) > 0) {
                showNotice("Hệ thống", "Cập nhật thành công " + msg + " thẻ kho  ");
                playSound01();
                getInPortByProductId();
            } else {
                showNotice("Hệ thống", "Cập nhật thất bại " + msg + " thẻ kho  ");
                Common.PlayErrorSound();
            }
        }
    });
}
//function delWhCard(_id, crtype) {
//    if (confirm("Bạn có muốn xóa thẻ kho này không")) {
//        $.ajax({
//            type: "post",
//            url: "/handler/wareHouse.ashx",
//            data: { op: "updateToChecked", id: _id, isDelete: 777, curentType: crtype,st:Anpero.CurentStore },
//            success: function (msg) {
//                if (parseInt(msg) > 0) {
//                    showNotice("Hệ thống", "Hủy thành công " + msg + " thẻ kho  ");
//                    playSound01();
//                    getInPortByProductId();
//                } else {
//                    showNotice("Hệ thống", "Hủy thất bại " + msg + " thẻ kho  ");
//                }
//            }
//        });
//    }

//}

var curentCustomId = 0;
function setAddr(addid, cName) {    
    $("#customerName").val(cName);
    curentCustomId = addid;
    $("#makh").val('KH' + addid);    
    $("#addId").val('KH' + addid);
    $('.infouser').hide();
}

function seachAddress(e) {    
    var valid = false;
    
    if (e.keyCode === 13) {
        e.preventDefault(); // Ensure it is only this code that rusn
        valid = true;
    }    
 
    if ($(".suggest-kh").val() == "") {    
         valid = false;       
    }    
    if (valid) {       
        $.ajax({
            dataType: "text",
            mothod: "post",
            url: "/handler/address.ashx",
            data: {
                op: "seachAddressBook",
                detail: $(".suggest-kh").val(),
                st:Anpero.CurentStore
            },
            success: function (msg) {
                $('.infouser').html(msg);
                $('.infouser').show();
                $(".setaddresshng").click(function () {
                    var id = $(this).data("id");                   
                    var cName = $(this).data("name");
                    setAddr(id, cName);
                    
                });

            }
        });
    }
}



