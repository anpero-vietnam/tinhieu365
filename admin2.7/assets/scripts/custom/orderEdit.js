$('#callbaogia').click( function() { 
	$('#tab_2_link').trigger('click');
	return false; 
} );

//gia tien mask
$(".mask-money").inputmask("decimal",{
            numericInput: false,
            groupSeparator: ',',
            autoGroup: 'true',
            digits: 2,
            digitsOptional: 'false',
            rightAlignNumerics: false,
            greedy: false
        });