//<script src="~/assets/plugins/amcharts/amcharts.js"></script>
//<script src="~/assets/plugins/amcharts/pie.js"></script>
//<script src="~/assets/plugins/amcharts/themes/light.js"></script>
function getBalanceOfbank(_type,content) {
    var _contentID = "chartdiv";
    if (content != null) {
        _contentID = content;

    }
    $.ajax({
        dataType: "text",
        mothod: "post",
        url: "/handler/chart.ashx",
        data: { op: "getBalanceByBank",type: _type },
        success: function (msg) {
            if (msg != 0) {
                var pie1Data = [];
                var arr = msg.split("$");
                for (var i = 0; i < arr.length-1; i++) {
                    pieArr = arr[i].split("|");
                    pie1Data.push({
                        "country": pieArr[1],
                        "litres": pieArr[0]
                    });
                }
                var chart = AmCharts.makeChart(_contentID, {
                    "type": "pie",
                    "theme": "light",
                    "dataProvider": pie1Data,
                    "valueField": "litres",
                    "titleField": "country",
                    "exportConfig": {
                        menuItems: [{
                            icon: '/assets/plugins/amcharts/images/export.png',
                            format: 'png'
                        }]
                    }
                });
            }
            

        }
    });

    


}