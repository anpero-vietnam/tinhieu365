var Common = {
    scrollTo: function (element, timeOut) {
        if (timeOut == null || timeOut == "undefined") {
            timeOut = 1500;
        }
        $([document.documentElement, document.body]).animate({
            scrollTop: $(element).offset().top-120
        }, timeOut);
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
       return new Date(dateItems[yearIndex], month, dateItems[dayIndex]);        
    },
    isValidDate: function (_date, _format, _delimiter) {
       var x= Common.stringToDate(_date, _format, _delimiter)
        return !isNaN(x.getTime())
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

    },
    ckediTorSetContents: function (string) {
        var editor = CKEDITOR.instances.editor1;
        editor.setData(string);
    },
    ckeditorInsertHTML: function (String) {
        // Get the editor instance that we want to interact with.
        var editor = CKEDITOR.instances.editor1;
        // Check the active editing mode.
        if (editor.mode == 'wysiwyg') {

            editor.insertHtml(String);
        }
        else
            alert('You must be in WYSIWYG mode!');
    },
    ValidURL: function (str) {
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
    },
    getParameterByName: function (name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    },
    isEmail: function (n) { var t = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/; return t.test(n) },
    isVnFone: function (n) { return (n = n.replace("+84", "0").replace("+", "0").replace("-", "").replace(" ", "").replace(".", ""), n.length > 15 || n.length < 9) ? !1 : (n = n.replace("+", "0"), isNaN(n)) ? !1 : !0 },
    htmlDecode: function (n) { return n ? $("<div />").html(n).text() : "" },
    formatNumber: function (num) { return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") },
    showNotice: function (tittle, text) {
        var unique_id = $.gritter.add({
            title: tittle,
            text: text,
            image: '/Assets/images/logo.png',
            sticky: true,
            time: '2000',
            class_name: 'my-sticky-class'
        });
        setTimeout(function () {
            $.gritter.remove(unique_id, {
                fade: true,
                speed: 'slow'
            });
        }, 8000);
    },
    numberToWords: function (s) {
        var th_val = ['', 'thousand', 'million', 'billion', 'trillion'];
        var dg_val = ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
        var tn_val = ['ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
        var tw_val = ['twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
        s = s.toString();
        s = s.replace(/[\, ]/g, '');
        if (s != parseFloat(s))
            return 'not a number ';
        var x_val = s.indexOf('.');
        if (x_val == -1)
            x_val = s.length;
        if (x_val > 15)
            return 'too big';
        var n_val = s.split('');
        var str_val = '';
        var sk_val = 0;
        for (var i = 0; i < x_val; i++) {
            if ((x_val - i) % 3 == 2) {
                if (n_val[i] == '1') {
                    str_val += tn_val[Number(n_val[i + 1])] + ' ';
                    i++;
                    sk_val = 1;
                } else if (n_val[i] != 0) {
                    str_val += tw_val[n_val[i] - 2] + ' ';
                    sk_val = 1;
                }
            } else if (n_val[i] != 0) {
                str_val += dg_val[n_val[i]] + ' ';
                if ((x_val - i) % 3 == 0)
                    str_val += 'hundred ';
                sk_val = 1;
            }
            if ((x_val - i) % 3 == 1) {
                if (sk_val)
                    str_val += th_val[(x_val - i - 1) / 3] + ' ';
                sk_val = 0;
            }
        }
        if (x_val != s.length) {
            var y_val = s.length;
            str_val += 'point ';
            for (var i = x_val + 1; i < y_val; i++)
                str_val += dg_val[n_val[i]] + ' ';
        }
        return str_val.replace(/\s+/g, ' ');
    },
    logOff: function () {
        $.ajax({
            url: "/account/logoff",
            type: "post", 
            success: function () {
                window.location.reload();
            }
        });
    }


};
$(document).ready(function () {
    if ($("#langModal").length > 0 && localStorage.getItem('lang') == null) {
        $("#langModal").modal("show");
    }
});
