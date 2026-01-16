var config = {
    updateJavascript: function (javascript) {
        var _data = {
            "op": "updateJavascript",
            script: javascript
        };
        $.ajax({
            type: "post",
            datatype: "text",
            data: _data,
            url: "/Handler/Webconfig.ashx",
            success: function (rs) {
                if (rs > 0) {
                    showNotice("Hệ thống", "Cập nhật thành công");
                }
            }

        });
    },
    update: function () {
        var valid = true;
        if ($("#txtTell").val() != "-1" && !isVnFone($("#txtTell").val())) {
            valid = false;
            showNotice("Anpero", "Số điện thoại không đúng. Vui lòng nhập lại");
        }
        var _data = {
            "op": "update",            
            hotline: $("#txtTell").val(),
            Skype: $("#txtSkype").val(),
            facebookLink: $("#txtFb").val(),
            email: $("#txtEmail").val(),
            address: $("#txtAddress").val(),
            otherPhone: $("#txtContactPhone").val(),
            shortDesc: $("#txtShortDesc").val(),
            favicon: $("#thumbFavicon").attr("src"),
            Logo: $("#thumb").attr("src"),
            Title: $("#txtTitle").val()

        };
        if (valid) {
            $.ajax({
                type: "post",
                datatype: "text",
                data: _data,
                url: "/Handler/Webconfig.ashx",
                success: function (rs) {
                    if (rs > 0) {
                        showNotice("Hệ thống", "Cập nhật thành công");
                    }
                }

            });
        }

    },
    updatePaymentConfig: function () {

        var valid = true;
        var _paymentType = $("#paymentType option:selected").val();
        var _merchantId = $("#MerchantId").val();
        var _merchantPass = $("#MerchantPass").val();
        var _email = $("#email").val();
        var _paymentFee = $("#fee").val();
        var _currency = $("#currency").val();
        var _isDefault = 0;
        if ($("#isTurnOnPaymentOnline").is(":checked")) {
            _isDefault = 1;
        }
        switch (_paymentType) {
            case "NL": {
                if (_merchantId == "" || _merchantId == null) {
                    showNotice("Anpero", "Vui lòng nhập merchantId ngân lượng");
                    valid = false;
                }
                if (_merchantPass == "" || _merchantPass == null) {
                    showNotice("Anpero", "Vui lòng nhập MerchantPass ngân lượng");
                    valid = false;
                }
                if (_email == "" || _email == null) {
                    showNotice("Anpero", "Vui lòng nhập Email đăng ký thanh toán trên cổng ngân lượng");
                    valid = false;
                }

                break;
            }
            default:
                valid = false;
                break;

        }
        if (valid) {
            var datas = {};
            
            datas.Name = _paymentType;
            datas.merchantId = _merchantId;
            datas.MerchantPassword = _merchantPass;
            datas.Email = _email;
            datas.PaymentFee = _paymentFee;
            datas.isDefault = _isDefault;
            datas.Currency = _currency;
            datas.PaymentCode = _paymentType;
            $.ajax({
                type: "post",
                datatype: "text",
                data: datas,
                url: "/PaymentConfig/UpdatePaymentConfig",
                success: function (rs) {
                    if (rs > 0) {
                        showNotice("Anpero", "Cập nhật thành công");
                    }
                }
            });
        }

    },
    getPaymentConfig: function () {
        $.ajax({
            type: "post",
            datatype: "text",
            data: { "op": "getPaymentConfig", st: Anpero.CurentStore, paymentType: $("#paymentType option:selected").val() },
            url: "/Handler/Webconfig.ashx",
            success: function (rs) {
                var config = JSON.parse(rs);

                $("#MerchantId").val(config.MerchantId);
                $("#MerchantPass").val(config.MerchantPassword);
                $("#email").val(config.Email);                
                if (config.PaymentFee != null) {
                    $("#fee").val(config.PaymentFee);
                }
                if (config.Isdefault == true) {
                    $("#isTurnOnPaymentOnline").attr('checked', 'checked')
                }
            }

        });


    },
    InitPaymentConfig: function () {
        config.getPaymentConfig();
        $("#btnUpdate").click(function () {
            config.updatePaymentConfig();
        });
        $("#paymentType").change(function () {
            if ($(this).val() == "VTC") {
                $("#currencyContent").removeClass("hide");
                $(".MerchantPass-ct label").html("Vui lòng nhập VTC SecurityKey");
                $(".MerchantId-ct label").html("Vui lòng nhập Id website đăng ký bên VTC");
                $(".email-ct label").html("Số điện thoại đăng ký bên VTC");
                
                $(".fee-ct").hide();
            }
        });
        if (typeof ($("#uploadFile").smallFileUpload) != "undefined") {
            $("#uploadFile").smallFileUpload({
                Size: "full",
                allowExtension: ".jpg,.png,.jpeg,.svg,.gif,.ico",
                success: function (data) {
                    
                    $("#thumb").fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
                }
            });
        }
        if (typeof ($("#uploadFileFavicon").smallFileUpload) != "undefined") {
            $("#uploadFileFavicon").smallFileUpload({
                Size: "full",
                allowExtension: ".jpg,.png,.jpeg,.svg,.gif,.ico",
                success: function (data) {
                    
                    $("#thumbFavicon").fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
                }
            });
        }

    }
};
