$(document).ready(function () {
    $(".mask-money").inputmask("decimal", { radixPoint: ".", autoGroup: true, groupSeparator: ",", groupSize: 3 });
    $("#am").keyup(function () {
        $.ajax({
            dataType: "text",
            mothod: "post",
            url: "/handler/StringHandler.ashx",
            data: { op: "getMoneyString", val: $("#am").val(), st: Anpero.CurentStore },
            success: function (msg) {
                $('#moneyString').html(msg);


            }
        });
    });
    //bindSelectSunsection();
    //bindTableSunsection();
})
function seachAddress() {
    var valid = true;
    if ($("#add").val() == "") {
        showNotify("#add");
        valid = false;
    }
    if (valid) {
        $.ajax({
            dataType: "text",
            mothod: "post",
            url: "/handler/address.ashx",
            data: { op: "seachAddressBook", detail: $("#add").val(), st: Anpero.CurentStore },
            success: function (msg) {
                $('#findContent').html(msg);
                //$('#addcustomer').modal('toggle');

            }
        });
    }

}
function delSubSection(_id) {
    var valid = true;
   
    if (valid) {
        $.ajax({
            dataType: "text",
            mothod: "post",
            url: "/handler/orderhandler.ashx",
            data: { op: "delSubSection", id: _id, st: Anpero.CurentStore },
            success: function (msg) {
                if (msg == 1) {
                    bindTableSunsection();
                    bindSelectSunsection();
                    playSound02();
                    showNotice("Cập nhật chỉ mục", "Xóa thành công " + msg + " phụ mục");
                } else {
                    showNotice("Cập nhật chỉ mục", "Xóa lỗi " + msg + " phụ mục");
                }
            }
        });

    }


}
function updateSubSection(_id) {
    var valid = true;
    var nameContent = "#sub_" + _id;
    var _name = $(nameContent).val();
    if (_name.trim() == "") {
        valid = false;
    }
    if (valid) {
        $.ajax({
            dataType: "text",
            mothod: "post",
            url: "/handler/orderhandler.ashx",
            data: { op: "updateSubSection", id: _id, name: _name, st: Anpero.CurentStore },
            success: function (msg) {
                if (msg == 1) {
                    bindTableSunsection();
                    bindSelectSunsection();
                    playSound02();
                    showNotice("Cập nhật chỉ mục", "Cập nhật thành công " + msg + " phụ mục");
                } else {
                    showNotice("Cập nhật chỉ mục", "Cập nhật lỗi " + msg + " phụ mục");
                }
            }
        });

    }
    

}
function bindTableSunsection() {
    $.ajax({
        dataType: "text",
        mothod: "post",
        url: "/handler/orderhandler.ashx",
        data: { op: "bindTableSunsection", st: Anpero.CurentStore },
        success: function (msg) {
            $("#tablesubsection").html(msg);
        }
    });
}
function bindSelectSunsection() {   
        $.ajax({
            dataType: "text",
            mothod: "post",
            url: "/handler/orderhandler.ashx",
            data: { op: "bindSelectSunsection", st: Anpero.CurentStore },
            success: function (msg) {
                $("#subsection").html(msg);
                $("#subsection2").html(msg);
                
            }
        });  
}
function addSubsection() {
    var valid = true;
    var _name = $("#subsectionName").val();
    if (_name.trim() == "") {
        valid = false;
    }
    if (valid) {
        $.ajax({
            dataType: "text",
            mothod: "post",
            url: "/handler/orderhandler.ashx",
            data: { op: "addSubsection", name: _name, st: Anpero.CurentStore },
            success: function (msg) {               
                $('#subsectionModer').modal('toggle');
                showNotice("Thêm phụ lục", "Thêm phụ lục thành công");
                //bindSelectSunsection();
                //bindTableSunsection();
            }
        });

    }
}
var curentCustomId = 0;
function setAddr(addid, cName) {
    $("#customerName").val(cName);
    curentCustomId = addid;
    $("#makh").val('KH' + addid);
    $("#addId").val('KH' + addid);
    $('.infouser').hide();
}