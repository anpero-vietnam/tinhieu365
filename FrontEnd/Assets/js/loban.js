
var rulerLength = 1000;
var trimStart = 0;
var trimEnd = 1000;
var myScroll;
function pullRightAction() {
    if (trimStart > 0) {
        jQuery('#loban-scroller').width(function (i, width) {
            return width + 10000;
        });
        trimStart -= rulerLength;
        var qStr = '?trimStart=' + trimStart + '&rulerLength=' + rulerLength;
        var newLi = jQuery('<li>').append(jQuery('<img/>' ,{class:'img-ruler-1', src: '../images/thuoc522.png' + qStr }))
            .append(jQuery('<img/>', {class:'img-ruler-2', src: '../images/thuoc429.png' + qStr }))
            .append(jQuery('<img/>', {class:'img-ruler-3', src: '../images/thuoc388.png' + qStr }));
        jQuery('#loban-thelist').prepend(newLi);
        myScroll.refresh();
    }
}
function pullLeftAction() {
    if (trimEnd < 50000) {
        jQuery('#loban-scroller').width(function (i, width) {
            return width + 10000;
        });
        var qStr = '?trimStart=' + trimEnd + '&rulerLength=' + rulerLength;
        var newLi = jQuery('<li>').append(jQuery('<img/>', {class:'img-ruler-1', src: '../images/thuoc522.png' + qStr }))
            .append(jQuery('<img/>', {class:'img-ruler-2', src: '../images/thuoc429.png' + qStr }))
            .append(jQuery('<img/>', {class:'img-ruler-3', src: '../images/thuoc388.png' + qStr }));
        trimEnd += rulerLength;
        jQuery('#loban-thelist').append(newLi);
        myScroll.refresh();
    }
}
function loaded() {
    Math.nativeRound = Math.round;
    Math.round = function (i, iDecimals) {
        if (!iDecimals)
            return Math.nativeRound(i);
        else
            return Math.nativeRound(i * Math.pow(10, Math.abs(iDecimals))) / Math.pow(10, Math.abs(iDecimals));
    };
    myScroll = new iScroll('loban-wrapper', {
        useTransition: true,
        leftOffset: jQuery('#pullRight').outerWidth(true),
        onRefresh: function () {
            jQuery('#measuring-stick').css({ 'left': jQuery('#lobanOuter').width() / 2 + 'px' });
            jQuery('#value-box').css({ 'left': (jQuery('#lobanOuter').width() / 2 - 28) + 'px' });
            if (jQuery('#pullRight').hasClass('loading')) {
                jQuery('#pullRight').removeClass('loading');
                jQuery('#pullRight .pullRightLabel').html('Kéo qua phải...');
            } else if (jQuery('#pullLeft').hasClass('loading')) {
                jQuery('#pullLeft').removeClass('loading');
                jQuery('#pullLeft .pullLeftLabel').html('Kéo qua trái...');
            }
            jQuery('#value-loban').html(Math.round((-this.x + jQuery('#lobanOuter').width() / 2) / 100, 1) + ' cm');
            jQuery('#value-loban-input').val(Math.round((-this.x + jQuery('#lobanOuter').width() / 2) / 10, 0));
        },
        onScrollMove: function () {
            jQuery('#value-loban').html(Math.round((-this.x + jQuery('#lobanOuter').width() / 2) / 100, 1) + ' cm');
            jQuery('#value-loban-input').val(Math.round((-this.x + jQuery('#lobanOuter').width() / 2) / 10, 0));
        },
        onScrollEnd: function () {
            console.log("onScrollEnd: " + this.x);
            if (this.x == 0 && !jQuery('#pullRight').hasClass('flip')) {
                console.log("pullRight add class 'flip'");
                jQuery('#pullRight').addClass('flip');
                jQuery('#pullRight .pullRightLabel').html('Thả ra để làm mới...');
            } else if (-this.x > (jQuery('#loban-scroller').width() - 2000) && !jQuery('#pullLeft').hasClass('flip')) {
                console.log("pullLeft add class 'flip'");
                jQuery('#pullLeft').addClass('flip');
                jQuery('#pullLeft .pullLeftLabel').html('Thả ra để làm mới...');
            }
            jQuery('#abc').html('this.x=' + this.x + ' : out=' + (jQuery('#loban-scroller').width() - 2000));
            if (jQuery('#pullRight').hasClass('flip')) {
                jQuery('#pullRight').removeClass('flip');
                jQuery('#pullRight').addClass('loading');
                jQuery('#pullRight .pullRightLabel').html('...');
                console.log("pullRightAction");
                pullRightAction();	// Execute custom function (ajax call?)
            } else if (jQuery('#pullLeft').hasClass('flip')) {
                jQuery('#pullLeft').removeClass('flip');
                jQuery('#pullLeft').addClass('loading');
                jQuery('#pullLeft .pullLeftLabel').html('...');
                console.log("pullLeftAction");
                pullLeftAction();	// Execute custom function (ajax call?)
            }
            jQuery('#value-loban').html(Math.round((-this.x + jQuery('#lobanOuter').width() / 2) / 100, 1) + ' cm');
            jQuery('#value-loban-input').val(Math.round((-this.x + jQuery('#lobanOuter').width() / 2) / 10, 0));
        }
    });
    setTimeout(function () { document.getElementById('loban-wrapper').style.left = '0'; }, 800);    
    jQuery('#value-loban').blur(function () {
        var px = parseFloat(jQuery(this).val()) * 10
        px += jQuery('#lobanOuter').width() / 2;
        var st = Math.ceil((px - trimEnd * 10) / 10000)
        if (st > 0) {
            for (var i = 1; i <= st; i++) {
                pullLeftAction();
            }
            myScroll.refresh();
        }
        myScroll.scrollTo(-(px - Math.round(jQuery('#lobanOuter').width(), 0)), 0, Math.max((st + 1) * 500, 1500))
    })
    jQuery('#value-loban-input').bind('keypress', function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            jQuery(this).blur()
        }
        else {
            //v = $(this).val()
            //alert( /^[0-9.]+$/.test(v)  )
            if (!((code == 46) || (code >= 48 && code <= 57))) {
                return false;
            }
            //alert(String.fromCharCode(e.charCode))
        }
    });
    jQuery(document).ready(function () {
        var px = 2020 + jQuery('#lobanOuter').width() / 2;
        myScroll.scrollTo(-(px - Math.round(jQuery('#lobanOuter').width(), 0)), 0, 500)
    });
}
document.addEventListener('DOMContentLoaded', function () { setTimeout(loaded, 200); }, false);
