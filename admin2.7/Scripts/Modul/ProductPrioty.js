var ProductProperties = (function () {
    var Id = 0;
    var ValueId = 0;
    var datas = {};
    var isValue = false;
    function Init() {
        $(document).ready(function () {
            bindData();
            $(".addCatBtn").click(function () {
                resetForm();
                $("#updateCategoryModel").modal("show");
            });
            $("#btn-cancel").click(function () {
                Id = 0;
                PropertiesId = 0;
            });
            $("#btn-add").click(function () {
                if (GetAndValidatePropertiesData()) {
                    addOrUpdateProperties();
                }
            });
            $("#addThumb").smallFileUpload({
                Size: "full",           
                allowExtension: ".jpg,.png,.jpeg,.svg",
                success: function (data) {                   
                    $("#imgCatThum").fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
                }
            });
            $("#smallThumb").smallFileUpload({
                Size: "full",
                allowExtension: ".jpg,.png,.jpeg,.svg",
                success: function (data) {
                    $("#smallThumbImg").fadeOut().attr("src", data.uploadedImages[0].Url).fadeIn();
                }
            });           
        });
    }
    function InitProduct() {
        $.ajax({
            type: "post",
            url: "/Properties/GetData",
            dataType: "json",
            data: { productId: getParameterByName("sp") },
            success: function (rs) {
                var htmlContent = "";
                if (rs.length > 0) {
                    for (var i = 0; i < rs.length; i++) {
                        if (rs[i].Values != null && rs[i].Values.length > 0) {
                            var valueObject = rs[i].Values;
                            if (valueObject.length > 0) {
                                htmlContent += "<div class=\"form-group row\" style=\"width:100%;\">";
                                htmlContent += "<div class=\"icheck-list\">";
                                htmlContent += "<h4>" + rs[i].Name + "</h4>";
                                htmlContent += "<div class=\"input-group\" style=\"width:100%;\">";
                                htmlContent += "<div class=\"icheck-list\">";
                                for (var j = 0; j < valueObject.length; j++) {
                                    htmlContent += "<label><input data-checkbox=\"icheckbox_square-grey\" type=\"checkbox\" class=\"icheck attrCheck\"  value=\"" + valueObject[j].Id + "\" data-price=\"" + valueObject[j].Price + "\" data-isInstock=\"" + valueObject[j].IsInStock + "\">" + valueObject[j].Values + "</label>";                                    
                                    var priceStr = (parseInt(valueObject[j].Price) > 0 ? "+" : "") + Common.toMoneyFormat(parseInt(valueObject[j].Price));
                                    htmlContent += "<div class=\"pr-artribute\"><strong>Giá</strong> :<span id=\"db-attr-price-" + valueObject[j].Id + "\"> " + (valueObject[j].Price == 0 ? "Không đổi " : priceStr)+ "</span>, <strong>kho:</strong> <span id=\"db-attr-isInstock-" + valueObject[j].Id +"\">" + (valueObject[j].IsInStock ? "Còn hàng" : "Hết Hàng") + "</span> <a href=\"#\"   data-id=\"" + valueObject[j].Id + "\" data-propertyName=\"" + rs[i].Name + " : " + valueObject[j].Values + "\" class=\"pull-right atribute-update\">Cập nhật</a></div>";
                                }                                
                                htmlContent += "</div></div></div></div>";
                            }
                        }
                    }                  
                }                
                if (htmlContent != "") {
                    $(".priotyList").html(htmlContent);
                    $('.priotyList input').iCheck({
                        checkboxClass: 'icheckbox_square-grey',
                        increaseArea: '20%'
                    });
                    $("#prioty-ct").show();
                    
                    var prId = getParameterByName("sp");
                    if (prId!="" && prId > 0) {
                        $.ajax({
                            type: "post",
                            url: "/Properties/GetValueByProductId",
                            dataType: "json",
                            data: { productId: prId },
                            success: function (rs) {
                                if (rs != null && rs.length > 0) {
                                    for (var i = 0; i < rs.length; i++) {
                                        var checkbox = $('.priotyList input[type=checkbox][value="' + rs[i].Id + '"]');                                        
                                        checkbox.iCheck('check');                                        
                                        var priceStr = (parseInt(rs[i].Price) > 0 ? "+" : "") + Common.toMoneyFormat(parseInt(rs[i].Price));
                                        $("#db-attr-price-" + rs[i].Id).html(rs[i].Price == 0 ? "Không đổi: " : priceStr);
                                        $("#db-attr-isInstock-" + rs[i].Id).html(rs[i].IsInStock ? "Còn hàng" : "Hết Hàng");                                        
                                        checkbox.attr('data-price', parseInt(rs[i].Price));
                                        checkbox.attr('data-isInstock', rs[i].IsInStock);
                                        
                                    }
                                }
                            }
                        });
                    }                  
                    $(".atribute-update").click(function () {
                        Id = $(this).attr("data-id");
                        var thisPrice = $(".attrCheck[value=" + Id + "]").attr("data-price");
                        var thisIsInprock = $(".attrCheck[value=" + Id + "]").attr("data-isinstock");                        
                        $("#attrPrice").val(parseInt(thisPrice.replace(",", "").replace(",", "").replace(",", "")));
                        if (thisIsInprock == -1 || thisIsInprock == 0) {
                            $("#attrIsInstock").prop("checked", false);
                        }
                        $("#lb-atribute").html("Cập nhật thuộc tính "+$(this).attr("data-propertyName"));                        
                        $("#atributeModal").modal("show");
                    });
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
                $("#db-attr-isInstock-" + Id).html(attrIsInstock?"Còn hàng":"Hết Hàng");
                var productID = getParameterByName("sp");
                
                $("#atributeModal").modal("hide");
            }
        });
    }
    function updateProductAtribute(_propertyValueId, _price, _isInstock) {
        
        var datas = {
            productID: getParameterByName("sp"),
            atributeId: _propertyValueId,
            price: _price,
            isInstock: _isInstock
        };
        $.ajax({
            type: "post",
            url: "/Properties/UpdateProductAtribute",
            dataType: "text",
            data: datas,
            success: function (rs) {
                if (rs > 0) {
                    InitProduct();
                } else {
                    showNotice("Lỗi", "Lỗi này đã được thông báo tự động lên admin. Vui lòng cập nhật sau");
                }

            }
        });
    }
    function addOrUpdateProperties() {
        var _url = isValue ? "/Properties/AddorUpdateValue" : "/Properties/AddorUpdate";
        datas.PropertiesId = Id;
        $.ajax({
            type: "post",
            url: _url,
            dataType: "text",
            data: datas,
            success: function (rs) {
                resetForm();
                $("#updateCategoryModel").modal("hide");
                bindData();
            }
        });
    }
    
    function deleteValById(_id) {
        $.ajax({
            type: "post",
            url: "/Properties/deleteValById",
            dataType: "text",
            data: { Id: _id },
            success: function (rs) {
                if (rs > 0) {
                    bindData();
                } else {
                    showNotice("Lỗi", "Giá trị đang có sản phẩm tham chiếu, không thể xóa được");
                }
            }
        });
    }
    function deleteById(_id) {
        $.ajax({
            type: "post",
            url: "/Properties/delete",
            dataType: "text",
            data: { Id: _id },
            success: function (rs) {
                if (rs > 0) {
                    bindData();
                } else {
                    showNotice("Lỗi", "Thuộc tính đang có giá trị. Không thể xóa được");
                }
                
            }
        });
    }
    function GetDataById(_id) {

        $.ajax({
            type: "post",
            url: "/Properties/GetDataById",
            dataType: "json",
            data: {Id:_id},
            success: function (rs) {
                $("#updateCategoryModel").modal("show");
                $("#CatName").val(rs.Name);
                $("#imgCatThum").attr("src",rs.Images);
                $("#catkeywords").val(rs.Keywords);
                $("#catPrioty").val(rs.Rank);
                $("#catShotDesc").val(rs.Description);
            }
        });
    }
    function GetValueById(_id) {
        ValueId = _id;
        $.ajax({
            type: "post",
            url: "/Properties/GetValueById",
            dataType: "json",
            data: { Id: _id },
            success: function (rs) {
                $("#updateCategoryModel").modal("show");
                $("#values").val(rs.Values);
                $("#imgCatThum").attr("src", rs.Images);
                $("#smallThumbImg").attr("src", rs.SmallThumb);
                $("#catPrioty").val(rs.Rank);
                
            }
        });
    }
    
    function bindData() {
        resetForm();
        $.ajax({
            type: "post",
            url: "/Properties/GetData",
            dataType: "json",
            success: function (rs) {
                var htmlContent = "<table class=\"table table-striped table-bordered table-hover table-scrollable\">";
                htmlContent += "<tr><th>Xóa</th><th>Tên thuộc tính</th><th style=\"width:100px;\">Ảnh</th><th>Các giá trị</th><th style=\"width:150px;\">Cập nhật</th><th colspan=\"2\">Thứ tự</th></tr>";
                for (var i = 0; i < rs.length; i++) {
                    var valuesHtml = "";
                    valuesHtml = "<button class=\"btn red btn-add-Value\" data-id=\"" + rs[i].Id + "\"><i class=\"glyphicon glyphicon-plus\"></i>Thêm giá trị</button>";
                    var coldSpan = 0;
                    if (rs[i].Values != null && rs[i].Values.length > 0) {
                        var valueObject = rs[i].Values;
                        coldSpan = parseInt(valueObject.length);
                        valuesHtml += "<table class=\"table table-striped table-bordered table-hover table-scrollable\">";
                        valuesHtml += "<tr><th>Xóa</th><th>Giá trị</th><th>Ảnh lớn</th><th>Ảnh nhỏ</th><th style=\"width:150px;\">Cập nhật</th><th colspan=\"2\">Thứ tự</th></tr>";

                        for (var j = 0; j < valueObject.length; j++) {
                            valuesHtml += "<tr>";
                            valuesHtml += "<td  style=\"width:37px;\"><button class=\"btn green btn-del-val\" data-id=\"" + valueObject[j].Id + "\"><i class=\"fa fa-trash-o\"></i></button></td>";
                            valuesHtml += "<td style=\"width:150px;\">" + valueObject[j].Values + "</td>";
                            valuesHtml += "<td  style=\"width:137px;\"><img src=\"" + valueObject[j].Images + "\"></td>";
                            valuesHtml += "<td  style=\"width:37px;\"><img src=\"" + valueObject[j].SmallThumb + "\"></td>";
                            valuesHtml += "<td><button class=\"btn red btn-update-val\" data-id=\"" + valueObject[j].Id + "\"><i class=\"glyphicon icon-pencil\"></i>Cập nhật</button></td>";
                            valuesHtml += "<td style=\"width:37px;\">" + valueObject[j].Rank + "</td>";
                            valuesHtml += "<td style=\"width:50px;\"><a data-id=\"" + valueObject[j].Id + "\" class=\"btn-val-up\" href=\"javascript:{};\"><i class=\"fa fa-arrow-up\"></i></a><a data-id=\"" + valueObject[j].Id + "\" class=\"btn-val-down\" href=\"javascript:{};\"><i class=\"fa fa-arrow-down\"></i></a></td>";
                            valuesHtml += "</tr>";
                        }
                        valuesHtml += "</table>";
                    }
                    htmlContent += "<tr>";
                    htmlContent += "<td style=\"width:50px;\"><button class=\"btn green btn-del-prioty\" data-id=\"" + rs[i].Id + "\"><i class=\"fa fa-trash-o\"></i></button></td>";
                    htmlContent += "<td style=\"width:150px;\">" + rs[i].Name + "</td>";
                    htmlContent += "<td><img src=\"" + rs[i].Images + "\"></td>";
                    htmlContent += "<td>" + valuesHtml + "</td>";
                    htmlContent += "<td style=\"width:137px;\"><button class=\"btn red btn-update-prioty\" data-id=\"" + rs[i].Id + "\"><i class=\"glyphicon icon-pencil\"></i>Cập nhật</button></td>";
                    htmlContent += "<td style=\"width:37px;\">" + rs[i].Rank + "</td>";
                    htmlContent += "<td style=\"width:50px;\"><a class=\"btn-pri-up\" data-id=\"" + rs[i].Id + "\" href=\"javascript:{};\"><i class=\"fa fa-arrow-up\"></i></a><a data-id=\"" + rs[i].Id + "\" class=\"btn-pri-down\" href=\"javascript:{};\"><i class=\"fa fa-arrow-down\"></i></a></td>";
                    htmlContent += "</tr>";
                }

                htmlContent += "</table>";
                if (rs.length > 0) {
                    $("#catTableContent").html(htmlContent);
                } else {
                    $("#catTableContent").html("");
                }
                $("#catTableContent").show();

                $(".btn-del-prioty").click(function () {
                    var id = $(this).attr("data-id");
                    deleteById(id);
                });
                $(".btn-del-val").click(function () {
                    var id = $(this).attr("data-id");
                    deleteValById(id);
                });
                
                $(".btn-update-prioty").click(function () {
                    Id = $(this).attr("data-id");
                    ValueId = 0;
                    isValue = false;
                    $(".val-ct").hide();
                    $(".pri-ct").show();
                    GetDataById(Id);

                });
                $(".btn-pri-up").click(function () {
                    var id = $(this).attr("data-id");
                    UpdatePriotyRank(1,id);

                });
                $(".btn-pri-down").click(function () {
                    var id = $(this).attr("data-id");
                    UpdatePriotyRank(-1, id);
                });

                $(".btn-val-up").click(function () {
                    var id = $(this).attr("data-id");
                    UpdatePriotyValueRank(1, id);
                });
                $(".btn-val-down").click(function () {
                    var id = $(this).attr("data-id");
                    UpdatePriotyValueRank(-1, id);
                });
                
                $(".btn-add-Value").click(function () {
                    Id = $(this).attr("data-id");
                    $(".val-ct").show();
                    $(".pri-ct").hide();
                    ValueId = 0;
                    isValue = true;
                    $("#updateCategoryModel").modal("show");
                });
                $(".btn-update-val").click(function () {
                    var id = $(this).attr("data-id");
                    isValue = true;
                    $(".val-ct").show();
                    $(".pri-ct").hide();
                    GetValueById(id);

                });
                
                
            }
        });
    }
    function UpdatePriotyValueRank(_rank,_id) {
        $.ajax({
            type: "post",
            url: "/Properties/UpdatePriotyValueRank",
            dataType: "text",
            data: { Id: _id, rank: _rank },
            success: function (rs) {
                bindData();
            }
        });
    }
    function resetForm() {
        isValue = false;
        $("#CatName").val("");
        $("#catPrioty").val("1");
        $("#catShotDesc").val("");
        $("#addThumb").val(null);
        $("#smallThumb").val(null);
        $("#values").val("");
        
        $("#imgCatThum").attr("src", "");
        $("#smallThumbImg").attr("src", "");
        $("#CatName").val("");
        $(".val-ct").hide();
        $(".pri-ct").show();
        Id = 0;
        PropertiesId = 0;
        datas = {};
    }
    function UpdatePriotyRank(_rank, _id) {
        $.ajax({
            type: "post",
            url: "/Properties/UpdatePriotyRank",
            dataType: "text",
            data: { Id: _id, rank: _rank },
            success: function (rs) {
                bindData();
            }
        });
    
    }
    function GetAndValidatePropertiesData() {
        var valid = true;
        if (isValue) {
            
            datas.Id = ValueId;
        } else {
            datas.Id = Id;
        }
        
        datas.Name = $("#CatName").val();
        // datas.st = Anpero.CurentStore;
        datas.PropertiesId = PropertiesId;
        datas.Images = $("#imgCatThum").attr("src");
        datas.SmallThumb = $("#smallThumbImg").attr("src");
        datas.Rank = $("#catPrioty").val();
        datas.Description = $("#catShotDesc").val();
        datas.Keywords = $("#catkeywords").val();
        datas.Values = $("#values").val();

        if (isNaN(datas.Rank)) {
            valid = false;
            showNotice("Lỗi", "Độ ưu tiên của thuộc tính phải là số. Vui lòng nhập lại");

        }
        
        if (isValue) {
            if (datas.Values === "") {
                valid = false;
                showNotice("Lỗi", "Giá trị không được để trống");
            }
        } else {
            if (datas.Name === "" || datas.Name === null) {
                valid = false;
                showNotice("Lỗi", "Thuộc tính phải có giá trị hoặc ảnh");
            }
        }
        
        if (!valid) {
            Common.PlayErrorSound();
        }
        return valid;
    }
    return {
        Init: Init,
        UpdateRank: UpdatePriotyRank,
        InitProduct: InitProduct

    };
})();
