var loginControl = (function () {
    function _init() {
        $(document).ready(function () {
            $('#loginModal').on('shown.bs.modal', function () {
                googleCatchalogin = grecaptcha.render('g-recaptcha-login', {
                    'sitekey': _recapcha_key,
                    'theme': 'light',
                    'hl': 'en'
                });
            });
            $("#btn-googleLogin").click(function () {
                $.ajax({
                    url: "/account/GetGoogleLoginPage",
                    type: "post",                    
                    success: function (data) {                              
                            window.location.href = data;
                    }
                });
            });
        });
        $("#btnLogin").click(function () {
            var valid = true;
            var recapchaResponse = grecaptcha.getResponse(googleCatchalogin);
            if (recapchaResponse == "") {
                valid = false;
                Common.showNotice("Error", "Please click reCapcha check box");
            }
            var _email = $("#Email").val();
            var _password = $("#password").val();
            if (_email == "" || _password == "") {
                valid = false;
                Common.showNotice("Error", "Email and Password can not be empty");
            }
            if (valid) {
                $.ajax({
                    url: "/account/login",
                    type: "post",
                    data: { UserName: _email, password: _password, captcha: recapchaResponse },
                    success: function (data) {
                        Common.showNotice(data.code, data.message);
                        grecaptcha.reset(googleCatchalogin);
                        if (parseInt(data.code) === 200) {
                            window.location.reload();
                        }
                    }
                });
            }
        });
    }    
    return {
        init: _init
    };
})();
var RegisterControl = (function () {
    function _init() {
        setTimeout(function () {
            googleCatchaRegister = grecaptcha.render('g-recaptcha-register', {
                'sitekey': _recapcha_key,
                'theme': 'light',
                'hl': 'en'
            });
        }, 2000);
        $("#btn-register").click(function () {
            var valid = true;
            var recapchaResponse = grecaptcha.getResponse(googleCatchaRegister);
            if (recapchaResponse == "") {
                valid = false;
                Common.showNotice("Error", "Please click reCapcha check box");
            }
            var _email = $("#email").val();
            var _password = $("#Password").val();
            var _confirmPassword = $("#ConfirmPassword").val();
            var _Phone = $("#Phone").val();
            
            var _nicknames = $("#nicknames").val();
            if (_nicknames == "") {
                valid = false;
                Common.showNotice("Error", "Nicknames can not be empty");
            }
            if (_Phone != "" && !Common.isVnFone(_Phone)) {
                valid = false;
                Common.showNotice("Error", "Invalid Phone number");
            }
            if (_email == "") {
                valid = false;
                Common.showNotice("Error", "Email can not be empty");
            } else if (!Common.isEmail(_email)) {
                valid = false;
                Common.showNotice("Error", "Invalid Emails");
            }
            if (_password == "") {
                valid = false;
                Common.showNotice("Error", "password cannot be empty");
            } else if (_password.trim() != _confirmPassword.trim()) {
                valid = false;
                Common.showNotice("Error", "Password and confirm password does not match");
            }
            if (valid) {
                $.ajax({
                    url: "/account/register",
                    type: "post",
                    data: { UserName: _email, Phone: _Phone, password: _password, confirmpassword: _confirmPassword, SureName :_nicknames, captcha: recapchaResponse },
                    success: function (data) {
                        Common.showNotice(data.code, data.message);
                        grecaptcha.reset();
                        if (data.code == 200) {
                            window.location.href = "/account/myprofile";
                        } else if (data.code == 201) {
                            $("input[name=email]").css("border", "2px solid red");
                            $("#emailHelp").html("This username already exists");
                            $('html, body').animate({
                                scrollTop: $("input[name=email]").offset().top-60
                            }, 800);
                        }
                        
                    }
                });
            }
        });
    }
    return {
        init: _init
    };
})();