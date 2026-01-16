var slideControl = (function (){
    var referenceId = "";    
    var container = "#thumbContent-slide";
    var contentElement = "#thumbContent-slide";
    function ThumbTriggerInit() {
        $(".thumb-trigger").click(function () {
            event.preventDefault();
            var thisId = $(this).attr("id");

            var originId = thisId.replace("lbthumb-", "");
            var fileUploadId = "#fileUpload-" + originId;
            $(fileUploadId).click();
            $(fileUploadId).smallFileUpload({
                Size: "full",
                allowExtension: ".jpg,.png,.jpeg,.svg",
                success: function (data) {
                    $("#thumb-" + originId).fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
                }
            });
        });
    }
    function GetThumbByContent(referenceId,content) {
        
        if (slideControl.referenceId == "") {
            slideControl.referenceId = "sp" + getParameterByName("sp");
        }
        $.ajax({
            type: "post",
            url: "/pr/GetProductThumb",
            data: { Prod: (referenceId == "undefined" ? slideControl.referenceId : referenceId), contanerElement: content },
            success: function (msg) {    
                if (content) {
                    $(content).html(msg);
                } else {
                    $(slideControl.contentElement).html(msg);
                }
                
               ThumbTriggerInit();
            }
        });
    }
    function delPrThumb(id, _productId) {
        $.ajax({
            type: "post",
            url: "/pr/DelPrThumb",
            data: { imgId: id, productId: _productId },
            success: function (msg) {
                //"ps" + refId, "#thumbContent-slide2"
                
                var content = "#thumbContent-slide";
                if (_productId.indexOf("ps") >= 0) {
                    content = "#thumbContent-slide2";
                }
                slideControl.getThumbByContent(_productId, content);
                showNotice("Đã Xóa", "Đã xóa thành công");
            }
        });
    }
    function UpdatePrThumb(id, referencesId, containerElement ="#thumbContent-slide") {
        var _txtLinkClick = $("#txtLinkClick" + id).val();
        var _txtDesc = $("#txtDesc_" + id).val();
        var _prioty = $("#prioty_" + id).val();
        var _imagesLink = $("#thumb-" + id).attr("src");        
        if (isNaN(_prioty)) {
            _prioty = 0;
        }
        $.ajax({
            type: "post",
            url: "/handler/theme.ashx",
            data: { op: "UpdatePrThumb", imgId: id, txtLinkClick: _txtLinkClick, txtDesc: _txtDesc, st: Anpero.CurentStore, prioty: _prioty, imagesLink: _imagesLink, referID: referencesId },
            success: function (msg) {   
                GetThumbByContent(referencesId, containerElement);
                showNotice("Đã cập nhật", "Đã cập nhật thành công");
            }
        });
    }
    function addSlide(imagesUrl, description, clickUrl, referenceId, content, prioty = 0) {
        $.ajax({
            type: "post",
            url: "/theme/AddSlide",
            data: { ReferenceId: referenceId, Prioty: prioty, ImagesUrl: imagesUrl, Description: description, ClickUrl: clickUrl, __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val()},
            success: function (msg) {       
                if (parseInt(msg) == 1) {
                    GetThumbByContent(referenceId, content);                    
                } else {
                    showNotice("Đã có lỗi xảy ra với", "Hệ thống đã thu thập lỗi này và thông báo cho kỹ thuật viên.");
                }
                
                
            }
        });
    }
    function init(_referenceId) {
        referenceId = _referenceId;
        slideControl.getThumbByContent();
    }
    return {
        init: init,
        container: container,
        referenceId: referenceId,
        contentElement: contentElement,
        getThumbByContent: GetThumbByContent,
        ThumbTriggerInit: ThumbTriggerInit,
        UpdatePrThumb: UpdatePrThumb,
        delPrThumb: delPrThumb,
        addSlide: addSlide
    };
})();