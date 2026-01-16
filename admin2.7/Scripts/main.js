var mains = {};
var Anpero = {
    CurentStore: "",
    Init: function () {

        $(document).ajaxStart(function () {
            try {
                window.youtubeLoadingBar();
            } catch (e) {
                //
            }
        }).ajaxStop(function () {
            try {
                window.youtubeLoadingBar(false);
            } catch (e) {
                //
            }
        });

        Anpero.GetNotify();
        $(window).scroll(function () {
            if ($(window).scrollTop() != 0) { $('#top').fadeIn(); } else { $('#top').fadeOut(); }
        });
        $('#top').click(function () { $('html, body').animate({ scrollTop: 0 }, 500); }); function gotop() { $('html, body').animate({ scrollTop: 0 }, 500); }
        $(".mask-money").inputmask("integer", { radixPoint: ".", autoGroup: true, groupSeparator: ",", groupSize: 3, digits: 0 });
        $(".mask-money").focus(function () { this.select(); });
        $(".date-picker").datepicker({
            format: 'dd/mm/yyyy',
            language: "vi",
            autoclose: true
        });
        $('#view').append('<div id="top">Back to Top</div>');
        Anpero.GetCommonGetDashBoad();
        var date = new Date();
        var minutes = 60 * 2;
        if ($.cookie("wellcome") == null && $.cookie("wellcome") != 1) {
            date.setTime(date.getTime() + (minutes * 60 * 1000));
            $.cookie("wellcome", "1", { expires: date });
            playSound04();
            mains.wellcome = 1;
            showNotice("Xin chào", "Chúc bạn ngày làm việc vui vẻ");
        } else {
            minutes = 60 * 4;
            date.setTime(date.getTime() + (minutes * 60 * 1000));
            $.cookie("wellcome", "1", { expires: date });
        }
        var soundCode = $.cookie('sound');
        if (parseInt(soundCode) == 0) {
            $(".ssst").removeClass("fa-volume-up");
            $(".ssst").removeClass("fa-volume-down");
            $(".ssst").removeClass("fa-volume-off");
            $(".ssst").addClass("fa-volume-off");
        }
        if (parseInt(soundCode) == 1) {
            $(".ssst").removeClass("fa-volume-up");
            $(".ssst").removeClass("fa-volume-down");
            $(".ssst").removeClass("fa-volume-off");
            $(".ssst").addClass("fa-volume-down");
        }
        if (parseInt(soundCode) == 2 || parseInt(soundCode) == 3) {
            $(".ssst").removeClass("fa-volume-up");
            $(".ssst").removeClass("fa-volume-down");
            $(".ssst").removeClass("fa-volume-off");
            $(".ssst").addClass("fa-volume-up");
        }
    },
    GetNotify: function () {
        //  window.setInterval(function () {
        setTimeout(function () {
            $.ajax({
                type: "post",
                dataType: "text",
                data: { op: "getNotify", st: Anpero.CurentStore },
                url: "/handler/Notify.ashx",
                success: function (msg) {
                    if (msg != "0" && msg != "") {
                        showNotice("Thông báo từ hệ thống", msg);
                        Common.PlaySuccessSound();
                    }
                }
            });
        }, 3000);
        //}, 30000);
    },
    GetCommonGetDashBoad: function () {
        $.ajax({
            type: "post",
            dataType: "json",
            data: { st: Anpero.CurentStore },
            url: "/home/GetDashBoad",
            success: function (jsonObj) {
                $(".dashboard-stat #home-order").html(jsonObj.OrderWaiting + "/" + jsonObj.OrderPaider);
                $(".dashboard-stat #RequestCount").html(jsonObj.RequestToday);
                $(".dashboard-stat #contact-waiting").html(jsonObj.WaitingContact);
                $(".dashboard-stat #contact-waiting").html(jsonObj.WaitingContact);
                var htmlWaitingNotify = "";
                var htmlWaitingNotifyCount = 0;

                var htmlNotify = "";
                if (jsonObj.AllSynNotify.length > 0) {
                    for (var i = 0; i < jsonObj.AllSynNotify.length; i++) {
                        var item = jsonObj.AllSynNotify[i];

                        if ($("#sysNotify-home").length > 0) {
                            htmlNotify += "<div class=\"item\"><div class=\"item-head\">" + item.NotifyTimeText + "</div><div class=\"item-body\"><a href=\"/systemHistory/detail?id=" + item.Id + "&st=" + item.St + "\">" + item.Title + "</a></div></div>";
                        }
                        if (item.IsLocked) {
                            htmlWaitingNotifyCount += 1;
                            htmlWaitingNotify += "<li><a href=\"/systemHistory/detail?id=" + item.Id + "&st=" + item.St + "\"><span class=\"time\">" + item.NotifyTimeText + "</span><span class=\"details\">" + item.Title + "</span></a></li>";
                        }
                    }
                }
                if (jsonObj.UserMessengeList.length > 0) {
                    var htmlMsg = "";
                    for (var j = 0; j < jsonObj.UserMessengeList.length; j++) {
                        var msgItem = jsonObj.UserMessengeList[j];

                        htmlMsg += "<li><a href=\"/messages/inbox?st=" + Anpero.CurentStore + "&ms=" + msgItem.Id + "\" ><span class=\"time\">" + Common.GetDateStringFromDateOfset(msgItem.SenderDate) + "</span><span class=\"details\">" + msgItem.Title + "</span></a></li>";
                    }
                    htmlMsg += "<li><a href=\"/Messages?st=" + Anpero.CurentStore + "\"><span class=\"label label-sm label-icon label-info\"><i class=\"fa fa-bullhorn\"></i></span>Gửi tin nhắn</a></li>";
                }

                $("#sysNotify-home").html(htmlNotify);
                $("#home-newSysMsgCt").html(htmlWaitingNotify);
                $("#home-sysWaiting").html(htmlWaitingNotifyCount + " hoạt động mới");
                $("#home-sysWaitingCount").html(jsonObj.WaitingSynNotify == null ? 0 : jsonObj.WaitingSynNotify);
                $("#contactCount").html(jsonObj.WaitingContact);
                $(".newMassageCount").html(jsonObj.WaitingMessage);
                $("#home-msg-content").html(htmlMsg);


            }
        });
    }


};
var Location = {
    Init: function () {
        $(document).ready(function () {            
            Location.Getlocation(0);
            $("#country").change(function () {
                Location.Getlocation($("#country option:selected").val());
            });
        });
    },
    Getlocation: function (_parentLocation) {
        if (_parentLocation > 0) {
            $("#prov").html("<option value=0>Đang tải dữ liệu</option>");
        }
        $.ajax({
            method: "post",
            url: "/handler/LocationHandler.ashx",
            datatype: "text/plain",
            data: { ParentLocationId: _parentLocation },
            success: function (rs) {
                if (_parentLocation == 0) {
                    $("#country").html(rs);
                } else {
                    $("#prov").html(rs);
                }
            }
        });
    }
};

(function () {

    jQuery(function ($) {
        window.youtubeLoadingBar = function (turnOff) {
            if (turnOff === false) {
                clearInterval(_lb.interval);
                $("#loading-bar")[_lb.direction]("101%");
                setTimeout(function () {
                    $("#loading-bar").fadeOut(300, function () {
                        $(this).remove();
                    });
                }, 300);
            } else {
                if ($('#loading-bar').length === 0) {
                    $('body').append($('<div/>').attr('id', 'loading-bar').addClass(_lb.position));
                    _lb.percentage = Math.random() * 10 + 10;
                    $("#loading-bar")[_lb.direction](_lb.percentage + "%");
                    _lb.interval = setInterval(function () {
                        _lb.percentage = Math.random() * ((100 - _lb.percentage) / 2) + _lb.percentage;
                        $("#loading-bar")[_lb.direction](_lb.percentage + "%");
                    }, 500);
                }
            }
        };

    });
    var _lb = {};
    _lb.position = 'top';
    _lb.direction = 'width';
    _lb.get = function (callback) {
        _lb.loading = true;
        jQuery.ajax({
            url: this.href,
            success: function (response) {
                _lb.loading = false;
                if (typeof (callback) === 'function')
                    callback(response);
            }
        });
    };
})();
$(document).ready(function () {
    Anpero.Init();
});