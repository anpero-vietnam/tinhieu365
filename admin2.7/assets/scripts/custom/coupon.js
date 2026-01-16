function generateCode () {
    var e, t, n, a, i;
    for (e = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", a = "", t = i = 12; i >= 1; t = --i) n = Math.floor(Math.random() * e.length), a += e.substring(n, n + 1);
    return a
}
$('#generate-code').click(function() {
    $('#coupon-code').val( generateCode );
    return false;
});
// loai ma giam gia
$('.coupon-group').hide();
  $('#option1').show();
  $('#coupon-type').change(function () {
    $('.coupon-group').hide();
    $('#'+$(this).val()).show();
  })
// ap dung cho
$('.apply-group').hide();
  $('#option3').show();
  $('#apply-for').change(function () {
    $('.apply-group').hide();
    $('#'+$(this).val()).show();
  }) 
//gia tien mask
$(".mask-money").inputmask("decimal",{
            numericInput: false,
            groupSeparator: ',',
            autoGroup: 'true',
            digits: 0,
            digitsOptional: 'false',
            rightAlignNumerics: false,
            greedy: false
        });