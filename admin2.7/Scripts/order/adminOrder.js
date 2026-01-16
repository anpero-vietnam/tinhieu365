function updatePrice(id) {
    var newprice = $("#pricePr_" + id).val();

    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",
        data: { op: "updatePrice", id: id, newprice: newprice, sptype2: -2, st: Anpero.CurentStore },
        success: function (msg) {
            if (msg = 1) {
                showNotice("Hệ thống", " Cập nhật giá thành công");
                window.location.reload(true);
            }
        }
    });

};

function updaeOrderSst(id) {
    var sst = $("#orderSst").val();
    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",
        data: { op: "updateOrderSst", id: id, sst: sst },
        success: function () {
            window.location.reload(true);
        }
    });
}
function addOrderSst() {
    var _deTail = $("#prsst").val();
    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",
        data: { op: "addOrderSst", deTail: _deTail, st: Anpero.CurentStore },
        success: function (msg) {
            BindOrderSstV1();
        }
    });
}
function BindOrderSstV1() {
    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",
        data: { op: "BindOrderSstV1", st: Anpero.CurentStore },
        success: function (msg) {
            $("#prSstV1").html(msg);
        }
    });
}
function updatePrSSt(id) {
    var _id = "#sstId_" + id;
    var name = $(_id).val();

    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",
        data: { op: "updatePrSSt", id: id, name: name, st: Anpero.CurentStore },
        success: function (msg) {
            showNotice("Hệ thống", "Cập nhật thành công " + msg + " trạng thái ");

        }
    });
}
function delPrSst(id) {
    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",
        data: { op: "delPrSst", id: id, st: Anpero.CurentStore },
        success: function (msg) {
            showNotice("Hệ thống", "Xóa thành công " + msg + " trạng thái ");
            BindOrderSstV1();
        }
    });
}