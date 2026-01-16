(function ($) {
    $.fn.drag = function (haveInput) {
       
        var $this = $(this);
            var x, y, top, left, down;
            var txtDFocus = false;
            //$('input[type="text"]').focus(function () {
            //    txtDFocus = true;
            //});
            $($this).mousedown(function (e) {
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

                        $($this).scrollTop(top - newY + y);
                        $($this).scrollLeft(left - newX + x);
                    }
                }
            });

            $("body").mouseup(function (e) { down = false; });           
    }
})(jQuery)