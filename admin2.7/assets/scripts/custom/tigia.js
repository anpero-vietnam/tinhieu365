$.ajax({
"url": "https://www.kimonolabs.com/api/21pmpjns?apikey=eblQ399vyTyq2Gm9rQhxfA1cHSVTuYrz&callback=kimonoCallback",
"crossDomain": true,
"dataType": "jsonp", data: {},
jsonpCallback: function (data) {
}
});
function kimonoCallback(data) {
var count = data.count;
$("#updateTime").html(data.results.collection1[count-1].updateTime);
$("#muatienmat").html(data.results.collection1[count-1].muatienmat + ' đ');
$("#muachuyenkhoan").html(data.results.collection1[count-1].muachuyenkhoan + ' đ');
$("#banra").html(data.results.collection1[count-1].ban + ' đ');
}
$("#mask_currency").inputmask('99,999.99 đ',{
            numericInput: false,
            rightAlignNumerics: false,
            greedy: false
        });


