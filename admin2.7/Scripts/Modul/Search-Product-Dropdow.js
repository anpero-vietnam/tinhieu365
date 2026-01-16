
var SearchProductDropdown = (function () {
    
    var selected = {};
    function init(_callBackFunction) {        
        if (typeof (_callBackFunction) != "undefined") {
            callBackFunction = _callBackFunction;
        }
        $(".dropdown input[name=keyWord]").change(function (e) {            
            seachProduct($(this).val());
        });
        seachProduct();
    }
    var callBackFunction = function () { };
    function seachProduct(productName) {
        var _page = 1;
        $("#sarchResultContent").html("<li><i class=\"fa fa-circle-o-notch fa-spin\"></i>Loading</li>");
        var valid = true;
        if (valid) {
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/pr/SearchProductByName",
                data: { curentPage: _page, PrName: productName },
                success: function (data) {
                    var html = "";
                    if (data != null && data.ItemList.length > 0) {
                        
                        data.ItemList.forEach(function (item) {
                            html += "<li><a href=\"javascript:void(0);\" data-id=\"" + item.Id + "\"  data-name=\"" + item.Name + "\" data-wr=\"" + item.Warranty + "\" >" + item.Name + "</a></li>";
                        });
                        $("#sarchResultContent").html(html);
                        $("#sarchResultContent li a").click(function (e) {
                            var datas = $(this).data();
                            
                            SearchProductDropdown.selected = datas;
                            $(".pr-info").show();
                            $("#wr").html(datas.wr + " tháng");
                            $("#pr-name").html("<a target=\"_blank\" href=\"/pr/update?sp=" + datas.id + "&st=" + Anpero.CurentStore + "\">" + datas.name + " <i class=\"fa fa-pencil\"></i></a>");
                            callBackFunction(datas.id);
                        });
                    } else {
                        $("#sarchResultContent").html("<li>Không tìm thấy sản phẩm</li>");
                    }
                    
                }
            });
        }
    }

    return {
        init: init,
        selected: selected
    };
})();
