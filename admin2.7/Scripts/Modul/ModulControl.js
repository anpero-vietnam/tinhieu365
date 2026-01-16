var ModulControl = (function (){
    var id = 0;
    var data = {};
    function getAndValidateData() {
        var valid = true;
        if (id === 0) {
            data.ModulName = $("#modulName").val();
        } else {
            data.ModulName = $("#modulName").html();
        }
        data.Id = id;                
        data.ConnectionString = $("#txtConnectionString").val();
        data.IsDefault = $("#isDefault").is(":checked")?"true":"false";                
        data.Description = $("#txtDesc").val(); 
        if (data.ConnectionString==="") {
            valid = false;
            showNotice("Lỗi", "Bạn cần nhập ConnectionString");
        }
    
        return valid;
    }
    function init(_id) {
       id = _id;
        $(".button-submit").click(function () {
            if (getAndValidateData()) {
                $.ajax({
                    type: "post",
                    url: "/SuperAdmin/CreateConnectionModul",
                    data: data,
                    success: function (msg) {
                        if (msg == "True") {
                            location.href = "/SuperAdmin/ModulList";
                            showNotice("Hệ thống", "Cập nhật thành công");
                        } else {
                            showNotice("Lỗi", "Hệ thống cập nhật lỗi. Thông tin lỗi đã được gửi cho ban quản trị.");
                        }
                    }
                });
            }
        });
    }
    return {
        init: init
    };
})();