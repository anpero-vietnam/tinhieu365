
var Waranty = (function () {
    var column = 0;
    var productId = 0;
    var tempData = {};
    function init() {
        $("#btn-search").click(function () {
            bindWarranty();
        });
        $("#btn-update-warranty").click(function () {
            tempData.__RequestVerificationToken = $("input[name=__RequestVerificationToken]").val();
            if (getAndValidateData()) {
                
                $.ajax({
                    url: "/Warehouse/UpdateWarrantyCard",
                    type: "post",
                    datatype: "html",
                    data: tempData,
                    success: function (rs) {
                        bindWarranty();
                        showNotice("Thông báo", "Cập nhật thành công");
                        $("#myModal").modal("hide");
                    }

                });
            }
        });
        $("#export").click(function () {
            window.location.href="/Export/ExportWarranty?st=" + Anpero.CurentStore + "&beginTime=" + $("#beginTime").val() + "&endTime=" + $("#endTime").val();
        });
        $("#btn-delete-choise").click(function () {
            if (confirm("Bạn có muốn xóa các bản ghi này không")) {
                deleteChoise();
            }
            
        });
    }
    function getAndValidateData() {

        var valid = true;
        var warranty = $("#wr").val();
        var fromDate = $("#fromDate").val();
        var toDate = $("#toDate").val();
        var reseller = $("#reseller").val();
        var customerName = $("#customerName").val();
        var IdCard = $("#IdCard").val();
        var email = $("#email").val();
        var phone = $("#phone").val();
        var note = $("#note").val();
        tempData.Warranty = warranty;
        tempData.BeginDate = Common.stringToDate(fromDate).toISOString();
        tempData.EndDate = Common.stringToDate(toDate).toISOString();
        tempData.Reseller = reseller;        
        tempData.contact.Name = customerName;
        tempData.contact.Mail = email;
        tempData.contact.IdCard = IdCard;        
        tempData.contact.Address = $("#address").val();
        tempData.Note = note;
        
        
        if (tempData.Id) {
            if (isNaN(warranty)) {
                showNotice("Thông báo", "Số tháng bảo hành không hợp lệ");
                valid = false;
            } 
            if (!isEmail(email)) {
                valid = false;
                showNotice("Thông báo", "Email không đúng định dạng");
            }
            if (!isVnFone(phone)) {
                valid = false;
                showNotice("Thông báo", "Điện thoại không đúng định dạng");
            }
            if (fromDate == "") {
                valid = false;
                showNotice("Thông báo", "Vui lòng chọn ngày bắt đầu");
            }
            if (toDate == "") {
                valid = false;
                showNotice("Thông báo", "Vui lòng chọn ngày kết thúc");
            }

        } else {
            valid = false;
        }
        return valid;
    }
    function bindWarranty() {

        var datas = {};
        datas.productId = Waranty.productId;
        datas.beginTime = $("#beginTime").val();
        datas.endTime = $("#endTime").val();
        datas.page = 1;
        datas.__RequestVerificationToken = $("input[name=__RequestVerificationToken]").val();
        $.ajax({
            url: "/Warehouse/GetWarrantyCard",
            type: "post",
            datatype: "html",
            data: datas,
            success: function (rs) {
                $("#rs-content").html(rs);
                $("#checkAll").click(function () {
                    if ($(this).is(":checked")) {
                        $("input[name=cblDelete]").prop("checked", true);
                    } else {
                        $("input[name=cblDelete]").prop("checked", false);
                    }
                });
            }

        });
    }
    function deleteChoise() {
        var arrId = [];
        $("input[name=cblDelete]:checked").each(function () {
            arrId.push($(this).val());
        });
        if (arrId.length > 0) {
            var datas = {};
            datas.__RequestVerificationToken = $("input[name=__RequestVerificationToken]").val();
            datas.arrId = arrId;
            $.ajax({
                url: "/Warehouse/DeleteWarranty",
                type: "json",
                datatype: "text",
                data: datas,
                success: function (rs) {                    
                    showNotice("Thông báo", "Xóa thành công");                    
                    bindWarranty();
                }
            });
        } else {
            showNotice("Lỗi", "bạn chưa chọn seria để xóa");
        }
    }
    function showUpdate(_id) {
        
        $("#myModal").modal("show");
        $.ajax({
            url: "/Warehouse/GetWarrantyCardById",
            type: "post",
            datatype: "json",
            data: { id: _id, __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val()},
            success: function (rs) {        
                tempData = rs;
                $("#myModalLabel").html("Cập nhật phiếu bảo hành: <strong>"+_id+"</strong>");
                $("#phone").val(rs.contact.Phone);
                $("#email").val(rs.contact.Mail);
                $("#customerName").val(rs.contact.Name);
                $("#address").val(rs.contact.Address);
                $("#note").val(rs.Note);
                $("#reseller").val(rs.Reseller);
                $("#wr").val(rs.Warranty);
                $("#IdCard").val(rs.contact.IdCard);
                //$("#fromDate").val(Common.jsonToDate(rs.BeginDate));
                //$("#toDate").val(Common.jsonToDate(rs.EndDate));
                $('#fromDate').datepicker('setDate', Common.jsonToDate(rs.BeginDate));
                $('#toDate').datepicker('setDate', Common.jsonToDate(rs.EndDate));
                Common.jsonToDate(rs.EndDate);
            }

        });
    }
 
    return {
        init: init,
        productId: productId,
        showUpdate: showUpdate
    };
})();

