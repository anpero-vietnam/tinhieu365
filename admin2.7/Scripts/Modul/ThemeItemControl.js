var themeItemControl = (function () {
    var _data = {};
    
    function getAndValidateData() {
        var valid = true;
        var editor = CKEDITOR.instances.editor1;
    }      
    function init() {

        $(document).ready(function () {        
            $("#thumbLink2").smallFileUpload({
                Size: "400x400",
                allowExtension: ".jpg,.png,.jpeg,.svg,.gif,.ico",
                success: function (data) {
                    
                    $("#imgThumb").fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
                }
            });
            $("body").addClass("page-sidebar-closed");
            $(".page-sidebar-menu").addClass("page-sidebar-closed");
            $(".page-sidebar-menu").addClass("page-sidebar-menu-closed");
            //$('#ztree').jstree({
            //    core: {
            //        multiple: false,
            //        cascade: 'none',
            //        'themes': { 'icons': false }
            //    },
            //    'plugins': ["unique"],
            //    checkbox: {
            //        three_state: false,
            //        cascade: "none"
            //    }
            //});
            //$('#ztree').jstree('open_all');
        })
    }
    return {
        init: init
    }
})()