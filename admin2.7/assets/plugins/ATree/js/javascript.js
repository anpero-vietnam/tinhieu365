$(document).ready(function () {
     
    $('.tree-list-item .node-button').html(" <i class=\"minus\"></i><i class=\"plus\"></i>");
    $('.tree-list-item .node-button').click(function () {
        
        $(this).toggleClass('show-child');
        $(this).next().next().toggleClass('show-child');
        var target = $(this).next().next();
        target.hasClass('show-child') ?
            target.addClass('show-child').slideDown(250) :
            target.removeClass('show-child').slideUp(250);

        if ($('.tree-list .tree-list-item:last-child').children('.tree-list-child').hasClass('show-child')) {
            $(this).parent().parent().css('margin-bottom', '85px');
        } else {
            setTimeout(() => {
                $(this).parent().parent().css('margin-bottom', '0');
            }, 210);
        }
    })
})