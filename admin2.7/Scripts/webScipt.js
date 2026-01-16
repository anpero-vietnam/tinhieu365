
function smng(code) {
    $.cookie('sound', code, { expires: 2147483647 });
    if (parseInt(code) == 0) {
        $(".ssst").removeClass("fa-volume-up");
        $(".ssst").removeClass("fa-volume-down");
        $(".ssst").removeClass("fa-volume-off");
        $(".ssst").addClass("fa-volume-off");
    }
    if (parseInt(code) == 1) {
        $(".ssst").removeClass("fa-volume-up");
        $(".ssst").removeClass("fa-volume-down");
        $(".ssst").removeClass("fa-volume-off");
        $(".ssst").addClass("fa-volume-down");
    }
    if (parseInt(code) == 2 || parseInt(code) == 3) {
        $(".ssst").removeClass("fa-volume-up");
        $(".ssst").removeClass("fa-volume-down");
        $(".ssst").removeClass("fa-volume-off");
        $(".ssst").addClass("fa-volume-up");
    }
}

function toURLEncode(s) { s = encodeURIComponent(s); s = s.replace(/\~/g, '%7E').replace(/\!/g, '%21').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\'/g, '%27'); s = s.replace(/%20/g, '+'); return s; };
function toURLDecode(s) { s = s.replace(/\+/g, '%20'); s = decodeURIComponent(s); return s; }
function ValidURL(str) {
    var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
        '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
        '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
        '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
        '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
        '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator
    if (!pattern.test(str)) {
        return false;
    } else {
        return true;
    }
}
function ValidImagesURL(str) {
    var pattern = new RegExp('https?://(?:[a-z\-\.]+\.)+[a-z]{2,6}(?:/[^/#?]+)+\.(?:jpg|gif|png|jpeg)$', 'i'); // fragment locator
    if (!pattern.test(str)) {
        return false;
    } else {
        return true;
    }
}
function SetContents(string) {
    var editor = CKEDITOR.instances.editor1;
    editor.setData(string);
}
function InsertHTML(String) {
    // Get the editor instance that we want to interact with.
    var editor = CKEDITOR.instances.editor1;
    // Check the active editing mode.
    if (editor.mode == 'wysiwyg') {

        editor.insertHtml(String);
    }
    else
        alert('You must be in WYSIWYG mode!');
}
function getnewId() {
    if (window.location.href.lastIndexOf("/trang") > 0) {
        return window.location.href.substring(window.location.href.lastIndexOf("-") + 1, window.location.href.lastIndexOf("/trang"));
    } else {
        return window.location.href.substring(window.location.href.lastIndexOf("-") + 1, window.location.href.length);
    }
}
function showNotice(tittle, text) {

    //if (!("Notification" in window)) {
    //    var unique_id = $.gritter.add({
    //        // (string | mandatory) the heading of the notification
    //        title: tittle,
    //        // (string | mandatory) the text inside the notification
    //        text: text,
    //        // (string | optional) the image to display on the left
    //        image: '/images/logo2.png',
    //        // (bool | optional) if you want it to fade out on its own or just sit there
    //        sticky: true,
    //        // (int | optional) the time you want it to be alive for before fading out
    //        time: '1200',
    //        // (string | optional) the class name you want to apply to that specific message
    //        class_name: 'my-sticky-class'
    //    });
    //    setTimeout(function () {
    //        $.gritter.remove(unique_id, {
    //            fade: true,
    //            speed: 'slow'
    //        });
    //    }, 8000);
    //}
    //else if (Notification.permission === "granted") {
    //    var options = {
    //        body: text,
    //        icon: "/images/logo2.png",
    //        dir: "ltr"
    //    };
    //    var notification = new Notification(tittle, options);
    //}
    //else if (Notification.permission == 'denied') {
    //    Notification.requestPermission().then(function (permission) {
    //        if (permission == "granted") {
    //            var options = {
    //                body: text,
    //                icon: "/images/logo2.png",
    //                dir: "ltr"
    //            };
    //            var notification = new Notification(tittle, options);
    //        } else {
    //            gritterNotice(tittle, text);       
    //        }
    //    });


    //} else {
    //    gritterNotice(tittle, text);
    //}
    gritterNotice(tittle, text);
}
function gritterNotice(tittle, text) {
    var unique_id = $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: tittle,
        // (string | mandatory) the text inside the notification
        text: text,
        // (string | optional) the image to display on the left
        image: '/images/logo2.png',
        // (bool | optional) if you want it to fade out on its own or just sit there
        sticky: true,
        // (int | optional) the time you want it to be alive for before fading out
        time: '1200',
        // (string | optional) the class name you want to apply to that specific message
        class_name: 'my-sticky-class'
    });
    setTimeout(function () {
        $.gritter.remove(unique_id, {
            fade: true,
            speed: 'slow'
        });
    }, 8000);
}
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
function playSound03() {
    var audioElement = document.createElement('audio');
    audioElement.setAttribute('src', '/assets/sound/ring.mp3');
    audioElement.setAttribute('autoplay', 'autoplay');
    $.get();
    audioElement.addEventListener("load", function () {
        audioElement.play();
    }, true);

    //$('.play').click(function () {
    //    audioElement.play();
    //});

    //$('.pause').click(function () {
    //    audioElement.pause();
    //});
}
function playSound01() {
    var soudCode = $.cookie('sound');
    if (soudCode != 0) {
        var audioElement = document.createElement('audio');
        audioElement.setAttribute('src', '/assets/sound/ring2.wav');
        audioElement.setAttribute('autoplay', 'autoplay');
        $.get();
        audioElement.addEventListener("load", function () {
            audioElement.play();
        }, true);
    }
    //$('.play').click(function () {
    //    audioElement.play();
    //});

    //$('.pause').click(function () {
    //    audioElement.pause();
    //});
}
function playSound02() {
    if ($.cookie('sound') != 0) {
        var audioElement = document.createElement('audio');
        audioElement.setAttribute('src', '/assets/sound/alert_47.mp3');
        audioElement.setAttribute('autoplay', 'autoplay');
        //audioElement.load()

        $.get();

        audioElement.addEventListener("load", function () {
            audioElement.play();
        }, true);
    }
    //$('.play').click(function () {
    //    audioElement.play();
    //});

    //$('.pause').click(function () {
    //    audioElement.pause();
    //});
}
function playSound() {
    if ($.cookie('sound') != 0) {
        var audioElement = document.createElement('audio');
        audioElement.setAttribute('src', '/assets/sound/success.wav');
        audioElement.setAttribute('autoplay', 'autoplay');
        //audioElement.load()

        $.get();

        audioElement.addEventListener("load", function () {
            audioElement.play();
        }, true);

    }
    //$('.play').click(function () {
    //    audioElement.play();
    //});

    //$('.pause').click(function () {
    //    audioElement.pause();
    //});
}
function playSound04() {
    if ($.cookie('sound') != 0) {
        var audioElement = document.createElement('audio');
        audioElement.setAttribute('src', '/assets/sound/startup.mp3');
        audioElement.setAttribute('autoplay', 'autoplay');
        //audioElement.load()

        $.get();

        audioElement.addEventListener("load", function () {
            audioElement.play();
        }, true);
    }
    //$('.play').click(function () {
    //    audioElement.play();
    //});

    //$('.pause').click(function () {
    //    audioElement.pause();
    //});
}
function showNotify(element) {
    $(element).pulsate({
        color: "#66bce6",
        repeat: 1
    });
}
function isEmail(n) { var t = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/; return t.test(n) }
function isVnFone(n) { return (n = n.replace("+84", "0").replace("+", "0").replace("-", "").replace(" ", "").replace(".", ""), n.length > 15 || n.length < 9) ? !1 : (n = n.replace("+", "0"), isNaN(n)) ? !1 : !0 }
function DragScroll(element, haveInput) {
    var x, y, top, left, down;
    var txtDFocus = false;
    $(element).mousedown(function (e) {
        if (!txtDFocus) {
            if (!haveInput) {
                e.preventDefault();
            }
            down = true;
            x = e.pageX;
            y = e.pageY;
            top = $(this).scrollTop();
            left = $(this).scrollLeft();
        }
    });
    $("body").mousemove(function (e) {
        if (!txtDFocus) {
            if (down) {
                var newX = e.pageX;
                var newY = e.pageY;

                //console.log(y+", "+newY+", "+top+", "+(top+(newY-y)));

                $(element).scrollTop(top - newY + y);
                $(element).scrollLeft(left - newX + x);
            }
        }
    });
    $("body").mouseup(function (e) { down = false; });
}
function scrollY() {

    var clicked = false, clickY;

    $(document).on({
        'mousemove': function (e) {
            clicked && updateScrollPos(e);
        },
        'mousedown': function (e) {
            clicked = true;
            clickY = e.pageY;

        },
        'mouseup': function () {
            clicked = false;
            $('html').css('cursor', 'auto');
        }
    });

    var updateScrollPos = function (e) {
        $('html').css('cursor', 'row-resize');
        $(window).scrollTop($(window).scrollTop() + (clickY - e.pageY));

    };
}
function countLine(element, isUnique) {
    var text = $(element).val();
    var lines = text.split("\n");
    var count = 0;
    if (isUnique) {
        if (lines.length >= 2) {
            var input = lines[lines.length - 2];
            for (var j = 0; j < lines.length - 2; j++) {
                if (lines[j] == input) {
                    playSound01();
                    text = text.replace(input, "");
                    text = text.replace("\n", "");
                    $(element).val(text);
                }
            }
        }
    }
    lines = text.split("\n");
    for (var i = 0; i < lines.length - 1; i++) {
        if (lines[i].trim() != "" && lines[i].trim() != null) {
            count += 1;
        }
    }
    return count;

}
function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}
function htmlDecode(n) { return n ? $("<div />").html(n).text() : "" }
var uniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
var KoDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
function UnicodeToKoDau(s) {
    var retVal = "";
    var pos = "";
    for (var i = 0; i < s.length; i++) {
        pos = uniChars.indexOf(s[i]);
        if (pos >= 0)
            retVal += KoDauChars[pos];
        else
            retVal += s[i];
    }
    return retVal.replace('$', '').replace('&', '').replace('$', '').replace('à', 'a');
}
var Common = {

    PlaySuccessSound: function () {
        var soundCode = $.cookie('sound');
        if (soundCode != 0) {
            switch (soundCode) {
                case 1:
                    playSound01();
                    break;
                case 2:
                    playSound02();
                    break;
                case 3:
                    playSound();
                    break;
                default:
                    playSound02();
                    break;
            }
        }
    },
    PlayErrorSound: function () {
        var soundCode = $.cookie('sound');
        if (soundCode != 0) {
            switch (soundCode) {
                case 1:
                    playSound01();
                    break;
                case 2:
                    playSound01();
                    break;
                case 3:
                    playSound01();
                    break;
                default:
                    playSound01();
                    break;
            }
        }
    },
    CopyToClipboard: function (element) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val($(element).val()).select();
        document.execCommand("copy");
        $temp.remove();
    },
    //stringToDate("17/9/2014", "dd/MM/yyyy", "/");
    //stringToDate("9/17/2014", "mm/dd/yyyy", "/")
    //stringToDate("9-17-2014", "mm-dd-yyyy", "-")
    stringToDate: function (_date, _format, _delimiter) {
        if (typeof _format == "undefined") {
            _format = "dd/MM/yyyy";
        }
        if (typeof _delimiter == "undefined") {
            _delimiter = "/";
        }
        var formatLowerCase = _format.toLowerCase();
        var formatItems = formatLowerCase.split(_delimiter);
        var dateItems = _date.split(_delimiter);
        var monthIndex = formatItems.indexOf("mm");
        var dayIndex = formatItems.indexOf("dd");
        var yearIndex = formatItems.indexOf("yyyy");
        var month = parseInt(dateItems[monthIndex]);
        month -= 1;
        var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
        return formatedDate;
    },
    ActiveLinkByCurrentUrl: function (container) {
        var currentPath = window.location.pathname.toLocaleLowerCase() + window.location.search.toLocaleLowerCase();
        currentPath = decodeURI(currentPath);
        $(container + " a").each(function () {
            if ($(this).attr("href").toLocaleLowerCase() == currentPath) {
                $(this).addClass("active");
            }
        });
    },
    GetDateStringFromDateOfset: function (input) {
        if (input != null && input != "") {
            input = input.toString()
            return input.substring(6, 8) + "/" + input.substring(4, 6) + "/" + input.substring(0, 4);
        } else {
            return "";
        }

    },
    toMoneyFormat: function (input) {
        var nf = new Intl.NumberFormat();
        return nf.format(input);
    },
    jsonToDate(jsonInput) {
        var milli = jsonInput.replace(/\/Date\((-?\d+)\)\//, '$1');
        return new Date(parseInt(milli));
    },
    jsonToDateStr(jsonInput) {
        var milli = jsonInput.replace(/\/Date\((-?\d+)\)\//, '$1');
        var date = new Date(parseInt(milli));
        var year = date.getFullYear(),
            month = date.getMonth() + 1, // months are zero indexed
            day = date.getDate(),
            hour = date.getHours(),
            minute = date.getMinutes(),
            second = date.getSeconds(),
            hourFormatted = hour % 12 || 12, // hour returned in 24 hour format
            minuteFormatted = minute < 10 ? "0" + minute : minute,
            morning = hour < 12 ? "am" : "pm";

        return day + "/" + month + "/" + year + " " + hourFormatted + ":" + minuteFormatted + morning;

    }
};
