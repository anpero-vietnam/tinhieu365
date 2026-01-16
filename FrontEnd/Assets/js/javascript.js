function mainpage() {
    var windowWidth = jQuery(window).width();
    var widthContainer = $('.container').width();
    var windowHeight = jQuery(window).height();
    var heightHeader = $('header').height();
    var heightFooter = $('footer').height();
    $(".main-page").css("min-height", windowHeight - heightHeader - heightFooter);
    $(".go-top").css("right", (windowWidth - widthContainer) / 2);

}

$(document).ready(function () {
    $('header .navbar-toggle').click(function () {
        $('body').toggleClass('overflow');
    });

    $('.box-cart-fast').click(function () {
        $('body').toggleClass('show-popup-cart');
    });

    $('body').click(function () {
        $('.box-add-item').hide();
    });
    // Tuan update 
    $('.box-material .add-item').click(function () {
        $(this).find('.box-add-item').show();
    });
    $('.box-properties-other .add-item').click(function () {
        $(this).find('.box-add-item').show();
    });
    // end update 

    $('.add-item').click(function (event) {
        event.stopPropagation();
    });

    // $('.add-item .icon-add-item').click(function () {
    //     $(this).parents().find('.box-add-item').show();
    // });

    $('.header .fa-angle-right').click(function () {
        $(this).parent().toggleClass('show-menu');
    });

    $('.header .icon-toggle-menu').click(function () {
        $(this).parent().parent().toggleClass('toggle-menu');
    });

    $('.add-item .close-form').click(function () {
        $(this).parents().parents().find('.box-add-item').hide();
    });

    $('.close-popup').click(function () {
        $('body').removeClass('show-popup-cart');
    });

    $('.btn-show-detail').click(function () {
        $(this).parent().parent().toggleClass('show-info');
    });

    // -------------------Dropdown ------------------ //
    $('.dropdown-custom .dropdown-custom-control').click(function () {
        $(".dropdown-custom").not($(this).parent()).removeClass('show-list');
        $(this).parent().toggleClass('show-list');
    })

    $('.dropdown .dropdown-control .form-control').click(function () {
        $(this).parent().parent().toggleClass('show-list');
    })

    // -------------------End Dropdown ------------------ //

    $('.carousel').each(function () {
        $(this).carousel({
            pause: true,
            interval: false
        });
    });

    $('.filter-item .dropdown-custom .dropdown-custom-control').click(function () {
        list_filter = $('.filter-item .filter-item-left .dropdown-custom');
        if (list_filter.hasClass('show-list')) {
            list_filter.removeClass('show-list');
            $(this).parent().toggleClass('show-list');
        }
    })

    $('.filter-item:not(.filter-hidden) .filter-item-right .btn-filter-expand').click(function () {
       
        $(this).slideToggle();
        $(this).parent().parent().parent().parent().parent().addClass('filter-mobile');
        filter_hidden = $(this).parent().parent().parent().next();
        filter_hidden.slideToggle();
    })

    $('.filter-item.filter-hidden .filter-item-right .btn-filter-hidden').click(function () {

        $(this).parent().parent().parent().slideUp();
        $(this).parent().parent().parent().parent().parent().removeClass('filter-mobile');
        $('.filter-item:not(.filter-hidden) .filter-item-right .btn-filter-expand').show();
    });


    $('.datepicker').datepicker();

    $(window).scroll(function () {
        $(window).scrollTop() > 300 ? $(".go_top").addClass("go_tops") : $(".go_top").removeClass("go_tops")
    });

    //headerMenu();
    mainpage();
    jQuery(window).resize(function () {
        //headerMenu();
        mainpage();
    });

  
});





function outside_click($dropdown_control, $dropdown_target, $dropdown_active) {
    window.onclick = function (e) {
        if (!e.target.matches($dropdown_control)) {
            var target = $(`${$dropdown_target}`);
            if (target.hasClass($dropdown_active));
            target.removeClass($dropdown_active);
        }
    }
}

outside_click('.dropdown-custom-control .dropdown-custom-control-title', '.dropdown-custom', 'show-list');


window.onclick = function (e) {
    dropdown = '.dropdown .dropdown-control .form-control';
    if (!e.target.matches(dropdown)) {
        var target_dropdown = $(`${dropdown}`).parent().parent();
        if (target_dropdown.hasClass('show-list')) {
            target_dropdown.removeClass('show-list');
        }
    }
}
