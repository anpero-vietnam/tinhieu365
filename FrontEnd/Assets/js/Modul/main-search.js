
var searchControl = (function () {
    var typingTimer;      
    
    function _init() {
        $("input[name=keyword]").keyup(function (e) {            
            if (e.key === 'Enter' || e.keyCode === 13) {
                if ($(this).val() != null)
                    window.location.href = "/search?keyword=" + $(this).val();
            }
            else {
                clearTimeout(searchControl.typingTimer);                
                searchControl.typingTimer=  setTimeout(function () {
                    bindData()
                },500)
            }
        });
    }
    function bindData() {
        $.ajax({
            method: "post",
            url: "/project/ajaxsearch",
            data: { keyword: $("input[name=keyword]").val(), __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val()},
            success: function (rs) {
                if (rs != null && rs.length > 0) {
                    var _html = "";
                    rs.forEach(function (item) {
                        _html += "<li><a href=\"/symbols/" + item.StockExchange + "-" + item.Ticker + "\"><strong>" + item.Ticker +"</strong> | "+item.BusinessName+"</a></li>";
                    });
                    $("#sarchResultContent").html(_html);
                }             
            }
        });
    }

    return {
        init: _init     
    }
})();
searchControl.init();