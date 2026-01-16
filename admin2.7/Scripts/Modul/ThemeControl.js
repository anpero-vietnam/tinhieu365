var themeControl = (function () {
    var _data = {};
    var _selectedAtribute = "";
    function getAndValidateData() {
        var valid = true;
        var editor = CKEDITOR.instances.editor1;
      

        _data = {
            Title: $("#Title").val(),
            Description: editor.getData(),            
            //IsDeFault: $("#chk-default").prop("checked"),
            IsPublish: $("#chk-publish").prop("checked"),
            Thumb: $("#viewImg").attr("src"),
            LinkDemo: $("#txt-linkDemo").val(),
            ImagesListJson: getImagesSlide(),
            ListAtributeId: getPrioty(),
            Id: getParameterByName("id"),
            Rank: $("#txt-Rank").val(),
            Type: "PERSONAL"
        }
        if (_data.Title.length < 5) {
            showNotice("Hệ thống", " Tiêu đề quá ngắn");
            valid = false;
        }
        if (_data.Thumb.length == 0) {
            showNotice("Hệ thống", "Vui lòng chọn ảnh đại diện cho theme này");
            valid = false;
        }
        if (!valid) {
            Common.PlayErrorSound()
        }
        return valid;
    }
    function getImagesSlide() {
        var slideArr = [];        
        if ($("#slideContent img").length > 0) {
            $("#slideContent img").each(function () {
                slideArr.push($(this).attr("src"));
                
            });
        }
        return slideArr.join("|") ;
    }
    function init() {
        initPrioty();
        
        $(".slide-item .close").click(function () {
            $(this).parent(".slide-item").remove();
        });
        $("#addThumb").smallFileUpload({
            allowExtension: ".jpg,.png,.jpeg,.svg,.gif,.ico",
            success: function (data) {
                $("#viewImg").fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
                setUpFancyBox();
            }
        });
        $("#themeSlide").smallFileUpload({
            allowExtension: ".jpg,.png,.jpeg,.svg,.gif,.ico",
            success: function (data) {
                $("#slideContent").fadeIn().append("<div class=\"slide-item col-md-3\"><span class=\"close\"></span><a class=\"fancybox\" href=\""+ data.uploadedImages[0].Url +"\" data-fancybox=\"gallery\" rel=\"gallery\" data-type=\"image\"><img src=\"" + data.uploadedImages[0].Url + "\" class=\"img-thumbnail \"></div>");
                setUpFancyBox();
                $(".slide-item .close").click(function () {
                    $(this).parent(".slide-item").remove();
                });
            }
        });
        $("#Update").click(function () {
            if (getAndValidateData()) {
                _data.Type = "PERSONAL";
                update();
            }
        });
        $("#btn-setdefault").click(function () {
            setDefault();
        });
        if ($("#UpdateSystem")) {
            $("#UpdateSystem").click(function () {
                if (getAndValidateData()) {
                    _data.Type = "SYSTEM";
                    update();
                }
            });
        }
        setUpFancyBox();
    }
    function setDefault() {
        var _id = getParameterByName("id");
        $.ajax({
            type: "post",
            url: "/theme/updateThemeDefault",
            data: { id: _id },
            success: function (msg) {
                if (msg >= 1) {
                    Common.PlaySuccessSound();
                    var id = getParameterByName("id");
                    if (id > 0) {
                        showNotice("Hệ thống", " Cập nhật thành công");
                    } else {
                        showNotice("Hệ thống", " Thêm mới thành công");
                    }
                    setTimeout(function () {
                        window.location.href = "/theme/createnewtheme?st=" + Anpero.CurentStore + "&id=" + _id;
                    }, 2000);
                } else {
                    showNotice("Cập nhật Lỗi", " Hệ thống đã thu thập lỗi này và và báo cho đội ngũ kỹ thuật. Thành thật xin lỗi quý khách vì sự bất tiện này!");
                }
            }
        });
        
    }
    function setUpFancyBox() {        
        $(".fancybox").fancybox({
            openEffect: 'none',
            closeEffect: 'none'
        });        
    }
    function update() {
        $.ajax({
            type: "post",
            url: "/theme/CreateNewTheme",
            data: _data,
            success: function (msg) {
                if (msg >= 1) {
                    Common.PlaySuccessSound();
                    var id = getParameterByName("id");
                    if (id > 0) {
                        showNotice("Hệ thống", " Cập nhật thành công");
                    } else {
                        showNotice("Hệ thống", " Thêm mới thành công");
                    }
                    setTimeout(function () {
                        window.location.href = "/theme/createnewtheme?st=" + Anpero.CurentStore + "&id=" + msg;
                    }, 2000);
                } else {
                    showNotice("Cập nhật Lỗi", " Hệ thống đã thu thập lỗi này và và báo cho đội ngũ kỹ thuật. Thành thật xin lỗi quý khách vì sự bất tiện này!");
                }
            }
        });
    }
    function bindThemePriotyDll() {
        $.ajax({
            type: "post",
            url: "/Properties/GetThemeData",
            dataType: "json",
            success: function (rs) {
                var htmlContent = "";

                if (rs.length > 0) {
                    for (var i = 0; i < rs.length; i++) {
                        
                        htmlContent += '<li> <div class="item"><strong>' + rs[i].Name + '</strong></div></li>';
                        if (rs[i].Values !== null && rs[i].Values.length > 0) {
                            var valueObject = rs[i].Values;
                            if (valueObject.length > 0) {
                                valueObject.forEach(function (item) {
                                    htmlContent += "<li><div class=\"item\"> <label class=\"checkbox-inline \"> <input type=\"checkbox\" value=\"" + item.Id + "\">" + item.Values + "</label></div></li>";
                                });
                            }
                        }
                        
                    } 
                    htmlContent +="<li><a><i class=\"fa fa-search\"></i> Tìm kiếm</a></li>";
                   $("#themeAttrContent").html(htmlContent);
                }
                $('.page-content-wrapper .dropdown-menu').click(function (e) {
                    e.stopPropagation();
                });
            }
        });
    }
    function initPrioty() {
        $.ajax({
            type: "post",
            url: "/Properties/GetThemeData",
            dataType: "json",
            success: function (rs) {
                var htmlContent = "";

                if (rs.length > 0) {
                    for (var i = 0; i < rs.length; i++) {
                        htmlContent += "<div class=\"form-group row\" style=\"width:100%;\">";
                        htmlContent += "<div class=\"icheck-list\">";
                        htmlContent += "<h4>" + rs[i].Name + "</h4>";
                        htmlContent += "<div class=\"input-group\" style=\"width:100%;\">";
                        htmlContent += "<div class=\"icheck-list\">";
                        if (rs[i].Values !== null && rs[i].Values.length > 0) {

                            var valueObject = rs[i].Values;
                            if (valueObject.length > 0) {
                                valueObject.forEach(function (item) {
                                    htmlContent += "<label><input data-checkbox=\"icheckbox_square-grey\" type=\"checkbox\" class=\"icheck attrCheck\"  value=\"" + item.Id + "\" data-price=\"" + item.Price + "\" data-isInstock=\"" + item.IsInStock + "\">" + item.Values + "</label>";
                                });


                            }
                        }
                        htmlContent += "</div></div></div></div>";                        
                     
                    }
                }

                if (htmlContent !== "") {
                    $(".priotyList").html(htmlContent);
                    $('.priotyList input').iCheck({
                        checkboxClass: 'icheckbox_square-grey',
                        increaseArea: '20%'
                    });
                    $("#prioty-ct").show();
                    var checkedArr = themeControl.selectedAtribute.split(",");
                    checkedArr.forEach(function (item) {
                        var checkbox = $('.priotyList input[type=checkbox][value="' + item + '"]');
                        checkbox.iCheck('check');
                    })                  
                }
            }
        });
        $("#btn-attr-add").click(function () {
            if (Id > 0) {
                var price = $("#attrPrice").val();
                var attrIsInstock = $("#attrIsInstock").prop("checked");
                $(".attrCheck[value=" + Id + "]").attr("data-price", parseInt(price.replace(",", "").replace(",", "").replace(",", "")));
                $(".attrCheck[value=" + Id + "]").attr("data-isinstock", attrIsInstock ? true : false);
                $("#db-attr-price-" + Id).html(price);
                $("#db-attr-isInstock-" + Id).html(attrIsInstock ? "Còn hàng" : "Hết Hàng");
                var productID = getParameterByName("sp");

                $("#atributeModal").modal("hide");
            }
        });
    }
    function getPrioty() {
        var property = [];
        $('.priotyList input[type=checkbox]:checked').each(function () {
            property.push(
                $(this).val()
            );
        });
        return property;
    }
    return {
        bindThemePriotyDll: bindThemePriotyDll,
        init: init,
        selectedAtribute: _selectedAtribute
    }
})()