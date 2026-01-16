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
            data: { op: "seachAddressBook", detail: $("#add").val() },
            success: function (msg) {
                $('#findContent').html(msg);
                //$('#addcustomer').modal('toggle');

            }
        });
    }
}
function submitAdd() {
    var valid = true;
    valid = isEmail($("#mail").val());
    _phone = $("#phone").val().trim();
    var _mail = $("#mail").val().trim();
    if (isVnFone(_phone) == false) {
        valid = false;
        showNotify("#phone");
        showNotice("Lỗi thêm địa chỉ", "Số điện thoại không đúng định dạng");
    }
    if (isEmail(_mail) == false) {
        showNotify("#mail");
        showNotice("Lỗi thêm địa chỉ", "Mail không đúng định dạng");
    }
    if ($("#names").val() == "" || $("#names").val() == null) {
        valid = false;
        showNotice("Lỗi thêm địa chỉ", "Tên liên hệ không được để trống");
        showNotify("#names")
    }
    if (valid) {
        $.ajax({
            dataType: "text",
            mothod: "post",
            url: "/handler/address.ashx",
            data: { op: "adminAddAdress", name: $("#names").val(), phone: $("#phone").val(), mail: _mail, address: $("#addr").val(),st:Anpero.CurrentStore },
            success: function (msg) {
                
                $('#addcustomer').modal('toggle');
                showNotice("Thêm địa chỉ", "Thêm địa chỉ thành công");
                setAddr(msg, $("#names").val());
            }
        });
    }
}