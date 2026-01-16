var _address = 0;
var _sender = 0;
function getPrByU() {
    var IsValid = true;
    if (IsValid) {
        $.ajax({
            type: "post",
            url: "/handler/shipproduct.ashx",
            data: { op: 'getPrByU' },
            success: function (msg) {
                if (msg != "0" && msg != "" && msg != null) {
                    $("#PrTable").html(msg);
                    $("#orcont").show();

                }
            }
        });
    }
}
function addProductshipping() {
    var _img = "";
    var _name = $("#prName").val().trim();
    var _quantity = $("#quantity").val().trim();
    var _moreDetail = $("#more_o").val().trim();
    var _prPrice = $("#prPrice").val().trim();
    var _weigth = $("#weigth").val().trim();
    var _cat = $("#prType option:selected").val();
    var _subcharge = $("#wr").val().trim();
    var _track = $("#track").val().trim();

    var IsValid = true;
    if (isNaN(_weigth.replace(",", "")) || _weigth == "") {
        showNotice("Thông báo", "Vui lòng nhập trọng lượng");
        showNotify("#weigth");
        IsValid = false;
    }
    if (isNaN(_prPrice.replace(",", "").replace(",", "")) || _prPrice == "") {
        showNotice("Thông báo", "Vui lòng giá trị hàng");
        showNotify("#prPrice");
        IsValid = false;
    }
    if (_name == null || _name == "") {
        showNotice("Thông báo", "Vui lòng nhập tên sản phẩm");
        showNotify("#prName");
        IsValid = false;
    }
    if (isNaN(_quantity.replace(",", "")) || _quantity == "") {
        showNotice("Thông báo", "Vui lòng nhập số lượng");
        showNotify("#quantity");
        IsValid = false;
    }

    if (_moreDetail == null || _moreDetail == "") {
        showNotice("Thông báo", "Mô tả thêm về sản phẩm tăng tính chính xác của đơn hàng");
    }
    if (IsValid) {
        $.ajax({
            type: "post",
            url: "/handler/ShipProduct.ashx",
            data: { op: 'addProductShip', subcharge: _subcharge.replace(",", ""), img: _img, name: _name, quantity: _quantity, moreDetail: _moreDetail, weigth: _weigth, price: _prPrice, recipId: _address, cat: _cat, track: _track },
            success: function (msg) {
                if (msg != "0") {
                    showNotice("Thông báo", "Thêm sản phẩm thành công");
                    getPrByU();
                    refressh2();

                    $("#orcont").show();
                } else {

                }
            }
        });
    }

}
function addProductOrder() {
    var Img = $("#thumb").attr("src");
    var Name = $("#prName").val();
    var Quantity = $("#quantity").val();
    var MoreDetail = $("#more_o").val();
    var Link = $("#linkPr").val();
    var prPrice = $("#prPrice").val();
    var IsValid = true;
    if (prPrice == null || prPrice == "") { prPrice = 0; }

    if (Link == null || Link == "") {
        showNotice("Thông báo", "Vui lòng nhập link sản phẩm");
        showNotify("#linkPr");
        IsValid = false;
    }
    if (Name == null || Name == "") {
        showNotice("Thông báo", "Vui lòng nhập tên sản phẩm");
        showNotify("#prName");
        IsValid = false;
    }
    if (Img == null || Img == "") {
        showNotice("Thông báo", "Bạn nên cập nhật ảnh");
    }

    if (MoreDetail == null || MoreDetail == "") {
        showNotice("Thông báo", "Mô tả thêm về sản phẩm tăng tính chính xác của đơn hàng");
    }
    if (IsValid) {
        $.ajax({
            type: "post",
            url: "/handler/ShipProduct.ashx",
            data: { op: 'addProduct', img: Img, name: Name, quantity: Quantity, moreDetail: MoreDetail, link: Link, price: prPrice, recipId: _address },
            success: function (msg) {
                if (msg != "0") {
                    showNotice("Thông báo", "Thêm sản phẩm thành công");
                    getPrByU();
                    refressh();
                    $("#orcont").show();
                } else { }
            }
        });
    }
}
$(document).ready(function () {
    $("#addPr_ship").click(function () {
        addProductshipping();
    });
    $("#aml").click(function () {
        $("#orcont").hide();
        $("#pral").show();
    });
    getPrByU();
    $("#createOrderShip").click(function () {
        createOrder(2);
    });
    $("#createOrder").click(function () {
        createOrder(1);
    });
    $("#addPr_od").click(function () {
        addProductOrder();
    });
    $("#linkPr").change(function () {

        getInfo();
    });
    $("#linkPr").keyup(function () {
        getInfo();
    });

    $("#thumbLink2").change(function () {
        getTokenData2("#thumbLink2");
    });

    $("#refreshbtn").click(function () {
        refressh();
    });
    $("#addLBtn").click(function () {
        var src = $("#addLTxt").val();
        $("#thumb").attr("src", src);
        $("#uploadUrl").foundation('reveal', 'close');
    });
    $("#delImg").click(function () {
        $("#thumb").attr("src", "/images/imgupload.png");
    });
    $("#addImg").click(function () {
        getTokenData("#file2");
    });

});
function refressh2() {
    $("#thumb").attr("src", "/images/imgupload.png");
    $("#prName").val("");
    $("#prPrice").val("");
    $("#more_o").val("");
    $("#weigth").val("");
}
function refressh() {
    $("#thumb").attr("src", "/images/imgupload.png");
    $("#linkPr").val("");
    $("#prName").val("");
    $("#prPrice").val("");
    $("#more_o").val("");
}
function getInfo() {
    var link = $("#linkPr").val();
    if (link.length >= 10) {
        loadHtml(link);
    }
}
function loadHtml(html) {

    $.ajax({
        type: "post", url: "/Handler/htmlHandler.ashx", dataType: "text",
        data: { op: "getHtml", Url: html },
        success: function (msg) {
            $.unblockUI();
            var matchedString = msg.match(/<title[^>]*>([^<]+)<\/title>/);
            if (matchedString != null) {
                $("#prName").val(htmlDecode(matchedString[1]));
            }
            matchedString = msg.match(/<meta[^>]*property=\"og:image\"[^>]*content=\"([^>]*)\"[^>]*>/);
            if (matchedString != null) {
                $("#thumb").attr("src", matchedString[1]);
            }
            if (html.indexOf("amazon.com") > 0) {
                setupAmazon(msg);
            }
            if (html.indexOf("ebay.com") > 0) {
                setupEbay(msg);
            }
            if (html.indexOf("ebay.vn") > 0) {
                setupEbayVn(msg);
            }
            if (html.indexOf("hm.com") > 0) {
                setupHm(msg);
            }
            if (html.indexOf("forever21.com") > 0) {
                setupForever21(msg);
            }
        }
    });
}
function setupForever21(msg) {

    matchedString = msg.match(/<meta[^>]*property=\"og:title\"[^>]*content=\"([^>]*)\"[^>]*>/);
    if (matchedString != null) {
        $("#prName").val(htmlDecode(matchedString[1]));
    }
    matchedString = msg.match(/<p[^>]*class=\"product-price\"[^>]*>([^<]+)<\/p>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }
}
function setupHm(msg) {

    matchedString = msg.match(/<meta[^>]*property=\"og:title\"[^>]*content=\"([^>]*)\"[^>]*>/);
    if (matchedString != null) {
        var n = matchedString[1].indexOf("$");
        var m = matchedString[1].length;
        $("#prName").val(htmlDecode(matchedString[1]).substring(0, n));
        $("#prPrice").val(matchedString[1].substring(n, m));
    }


}
function setupEbayVn(msg) {

    matchedString = msg.match(/<meta[^>]*name=\"price\"[^>]*content=\"([^>]*)\"[^>]*>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }
    matchedString = msg.match(/http:\/\/i\.ebayimg.com\/[^>]*\.JPG/);
    if (matchedString != null) {
        $("#thumb").attr("src", matchedString[0]);
    }
}
function setupEbay(msg) {

    matchedString = msg.match(/<span[^>]*id=\"convbidPrice\"[^>]*>([^<]+)<\/span>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }
    matchedString = msg.match(/<span[^>]*id=\"convbidPrice\"[^>]*>([^<]+)<span>[^<>]*<\/span><\/span>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }
    matchedString = msg.match(/<span[^>]*id=\"mm-saleDscPrc\"[^>]*>([^<]+)<span>[^<>]*<\/span><\/span>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }

    matchedString = msg.match(/<span[^>]*id=\"convbinPrice\"[^>]*>([^<]+)<span>[^<>]*<\/span><\/span>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }
    matchedString = msg.match(/<span[^>]*id=\"mm-saleDscPrc\"[^>]*>([^<]+)<\/span>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }



}
function setupAmazon(msg) {
    matchedString = msg.match(/<span id=\"current-price\" [^>]*>([^<]+)<\/span>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }
    matchedString = msg.match(/<b class=\"priceLarge kitsunePrice\"[^>]*>([^<]+)<\/b>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));
    }
    matchedString = msg.match(/<span id=\"buyingPriceValue\"[^>]*>([^<]+)<\/span>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));

    }
    matchedString = msg.match(/<b[^>]*class=\"priceLarge[^>]*\"[^>]*>([^<]+)<\/b>/);
    if (matchedString != null) {
        $("#prPrice").val(htmlDecode(matchedString[1]));

    }
    matchedString = msg.match(/http:\/\/ecx\.images-amazon\.com\/images\/[^>]*\.jpg/);
    if (matchedString != null) {
        $("#thumb").attr("src", matchedString[0]);
    }

    matchedString = msg.match(/<img.*?src=\"(.*?)\".*? id=\"prodImage\" .*?>/);
    if (matchedString != null) {
        $("#thumb").attr("src", matchedString[1]);
    }
    matchedString = msg.match(/<img.*? class=\"kib-ma kib-image-ma\" .*? src=\"(.*?)\" .*?>/);
    if (matchedString != null) {
        $("#thumb").attr("src", matchedString[1]);
    }
    matchedString = msg.match(/<img.*?>src=\"(.*?)\"[^>]* id=\"landingImage\".*?>/);
    if (matchedString != null) {
        $("#thumb").attr("src", matchedString[1]);
    }
    matchedString = msg.match(/<img[^>]*id=\"landingImage\"[^>]* src=\"(.*?)\"[^>]* >/);
    if (matchedString != null) {
        $("#thumb").attr("src", matchedString[1]);
    }

}
function delPrOd(ids) {
    var IsValid = true;
    if (IsValid) {
        $.ajax({
            type: "post",
            url: "/handler/shipproduct.ashx",
            data: { op: 'delPrOd', id: ids },
            success: function (msg) {
                getPrByU();
                showNotice("Thông báo", "Xóa thành công");
            }
        });
    }
}
function createOrder(_type) {
    var IsValid = true;
    if (_address == 0) {
        var isConfirm = confirm("Bạn chưa chọn địa chỉ từ sổ địa chỉ.Bấm OK để cập nhật địa chỉ sau");
        if (isConfirm == false) {
            IsValid = false;

        }
    }
    if (IsValid) {
        $.ajax({
            type: "post",
            url: "/handler/order.ashx",
            data: { op: 'createOrder', address: _address, sender: _sender, type: _type },
            success: function (msg) {
                if (msg = 0) {
                    showNotice("Thông báo", "Đơn hàng chưa có sản phẩm. ");
                } else {
                    showNotice("Thông báo", "Gửi yêu cầu báo giá thành công. Chúng tôi sẽ phản hồi sớm nhất. ");
                    window.setTimeout(function () {
                        window.location.href = "/order?product_id=&product_created_from=&product_created_to=&customerName=&KhId=&orderType=0&product_quantity_from=&product_quantity_to=&orderStatus=0";
                    }, 2000);
                }
            }
        });
    }

}


