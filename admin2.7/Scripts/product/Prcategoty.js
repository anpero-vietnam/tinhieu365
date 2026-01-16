
function addProductType() {
    var valid = true;
    name = $("#CatN").val();
    if (name == null || name == "") {
        valid = false;
        showNotice("Hệ thống", "Tên danh mục sản phẩm quá ngắn");
        showNotify("#CatN");
    }
    if (valid) {

        var _detail = SetRuleString();
        var _warranty=$("#warranty").val();
        var _subchar = $("#subCharge").val();
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "addProductType", names: name, detail: _detail, warranty: _warranty, subchar: _subchar, st: Anpero.CurentStore },
            success: function (msg) {
                if (msg == 1) {
                    BindProductTypeTable();
                   
                    showNotice("Hệ thống", " Cập thành công");
                    playSound02();
                } else {

                    showNotice("Hệ thống", " Cập thành Lỗi");
                    playSound01();
                }
            }
        });
    }
}
function BindProductTypeTable() {
    var valid = true;
    if (valid) {

        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "BindProductType", st: Anpero.CurentStore },
            success: function (msg) {
                $("#table").html(msg);
            }
        });
    }
}
function delCatPr(id) {
    var valid = true;
    if (valid) {

        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "delCatPr", id: id, st: Anpero.CurentStore },
            success: function (msg) {
                BindProductTypeTable();
                playSound02();
            }
        });
    }
}

var data = null;
var subCharge = 0;
var warranty = 0;