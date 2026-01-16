var ChagePassword = (function () {
    function _init() {

        $("#btn-changePassword").click(function () {
            var valid = true;
            var oldPass = $("#txt-oldPassword").val();
            var newPass = $("#txt-newPassword").val();
            var confirmNewPass = $("#txt-confirmNewPassword").val();
            if (oldPass == "" || newPass == "" || confirmNewPass == "") {
                valid = false;
                Common.showNotice("System", "Password con not be empty");
            } else if (newPass != confirmNewPass){
                valid = false;
                Common.showNotice("System","New password and confirm new password do not match");
            }
            if (newPass.length<5) {
                valid = false;
                Common.showNotice("System", "Password minimum 5 characters");
            }
            var _data = {
                OldPass: oldPass,
                NewPass: newPass,
                ConfirmNewPass: confirmNewPass,                
                __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val()
            };
            if (valid) {
                $.ajax({
                    url: "/Account/ChangePassword",
                    type: "post",
                    data: _data,
                    success: function (data) {
                        Common.showNotice("System", data.messenger);
                    }
                });
            }


        });
    }

    return {
        init: _init
    };
})();

$(document).ready(function () {
    ChagePassword.init();
});