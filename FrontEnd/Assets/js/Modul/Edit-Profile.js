var Profile = (function () {
    function _init() {
        $("#upload-avata").smallFileUpload({
            Size: "400x400",
            allowExtension: ".jpg,.png,.jpeg,.svg,.gif,.ico",
            success: function (data) {
                $("#user-thumb").fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
                UpdateAvata(data.uploadedImages[0].Url);
            }
        });
        $("#btn-profile-update").click(function () {
            var valid = true;
            var _data = {
                IntroduceYourself: $("#IntroduceYourself").val(),
                SureName: $("#txt-Nickname").val(),
                __RequestVerificationToken:  $("input[name=__RequestVerificationToken]").val()
            };
            if (valid) {
                $.ajax({
                    url: "/Profile/UpdateNickName",
                    type: "post",
                    data: _data,
                    success: function (data) {
                        Common.showNotice("System", data.message);
                    }
                });
            }
        });
        $("#btn-updateCotact").click(function () {
            var valid = true;
            var _province = $("#btn-selectCity").data("id");
            var _district = $("#district-selected").data("id");
            var _email = $("#contact-Email").val();
            var _phone = $("#Phone").val();
            if (_email !="" && !Common.isEmail(_email)) {
                valid = false;
                Common.showNotice("System", "Invalid Email Address");
            }
            if (_phone != "" && !Common.isVnFone(_phone)) {
                valid = false;
                Common.showNotice("System", "Invalid phone number");
            }
            var _data = {
                Email: _email,
                Phone: _phone,
                LocationId: _district,
                ProvinceId: _province,
                __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val(),
                Address: $("#txt-Address").val()
            };
            if (valid) {
                $.ajax({
                    url: "/Profile/UpdateContact",
                    type: "post",
                    data: _data,
                    success: function (data) {
                        Common.showNotice("System", data.message);
                    }
                });
            }
        });
        $("#btn-Business").click(function () {
            var valid = true;            
            var _data = {
                BusinessName: $("#businessName").val(),
                WebSite: $("#txt-Website").val(),
                IdentityCardNumber: $("#txt-Identity").val(),       
                __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val(),
                IntroduceBusiness: $("#txt-IntroduceCompany").val()
            };
            if (valid) {
                $.ajax({
                    url: "/Profile/UpdateBusiness",
                    type: "post",
                    data: _data,
                    success: function (data) {
                        Common.showNotice("System", data.message);
                    }
                });
            }
        });
        
    }
    function UpdateAvata(avataLink) {
        var _data = {
            __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val(),
            Avata: avataLink       
        };
        if (avataLink != "" && avataLink!=null) {
            $.ajax({
                url: "/Profile/UpdateAvata",
                type: "post",
                data: _data,
                success: function (data) {
                    Common.showNotice("System", data.message);
                }
            });
        }
    }
    return {
        init: _init
    };
})();