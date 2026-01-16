function getPrBytrack(track) {
    var valid = true;
    if (track.trim().length < 2) {
        valid = false;
    }
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "getPrBytrack", tracking: track, st: Anpero.CurentStore },
            success: function (msg) {
                if (msg != 0) {
                    var msgs = msg.split("|");
                    if (msgs[0].length > 0) {
                        
                        if ($("#scaned").val().indexOf(msgs[0]) == -1) {
                            $("#scaned").val($("#scaned").val() + msgs[0] + ",");
                            $.cookie('scaned', $("#scaned").val(), { expires: 7 });

                        }

                        if ($("#trackScaned").val().indexOf(msgs[1]) == -1) {

                            $("#trackScaned").val($("#trackScaned").val() + msgs[1] + ",");
                            $.cookie('trackScaned', $("#trackScaned").val(), { expires: 7 });

                        }
                        $("#inputScan").val("");
                        setUpScanList();
                    }


                }
                
             
            }
        });
    }

}
function setUpScanList() {
    var valid = true;
    var _snList = $("#scaned").val();
    $("#prSearchContent").html("");
    if (_snList.trim() == null || _snList == "") {
        valid = false;
    }
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "scanPr", snList: _snList, st: Anpero.CurentStore },
            success: function (msg) {
                $("#scanContent").html(msg);
                $('.customId').each(function () {
                    $(this).qtip({ // 
                        content: {
                            text: $(this).find('.cont').html()
                        }
                    });
                });
            }
        });
    }

}
function clearHistory() {
    $.cookie('scaned', '');
    $.cookie('trackScaned', '');
    location.reload();
    
}
function UpdateShipPr(i,j) {
    var w = $('#txtW_' + i).val();

    var _type = $('#prtype_' + i+' option:selected').val();
    var _subFee =  $('#surcharge_' + i).val();
    var _shipFee = $('#usaShipFee_' + i).val();
   
    var _processtransactionFee = $('#Feeprocesstransactions_' + i).val();
    var _sst = $('#prsst_' + i + ' option:selected').val();

    setUpAutoPriceV2(i,j);
    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",       
        dataType: "text",
        data: { op: 'updateSPrW', id: i, weigth: w, type: _type, subFee: _subFee, shipFee: _shipFee, paymentFee: 0, processtransactionFee: _processtransactionFee, sst: _sst, st: Anpero.CurentStore },
        success: function (rs) {
            if (rs > 0) {
                if ($("#scaned").val().indexOf(i) == -1) {
                    $("#scaned").val($("#scaned").val() + i + ",");
                    $.cookie('scaned', $("#scaned").val(), { expires: 7 });

                    
                }
                var track = $("#track_" + i).html();
                if ($("#trackScaned").val().indexOf(track) == -1) {

                    $("#trackScaned").val($("#trackScaned").val() + track + ",");
                    $.cookie('trackScaned', $("#trackScaned").val(), { expires: 7 });

                }
              }
            }
        });
    
}
function updateSp() {
    var valid = true;
    var _name = $("#prName").val();
    if (_name.length < 5) {
        valid = false;
        showNotice("Hệ thống", "Tên sản phẩm quá ngắn");
        Common.PlayErrorSound();
        showNotify("#prName");
    }
    var _quantyti = $("#quantyti").val();
    _quantyti = parseFloat(_quantyti.replace(",", ""))
    if (isNaN(_quantyti)) {
        valid = false;
        showNotice("Hệ thống", "Số lượng không nhập đúng định dạng");
        showNotify("#quantyti");
    }
    var _exTime = $("#exTime").val();
    var _price = $("#price").val();
    _price = parseFloat(_price.replace(",", ""))
    if (isNaN(_price)) {
        valid = false;
        showNotify("#price");
        showNotice("Hệ thống", " Giá thành không nhập đúng định dạng");
    }
    var _usaTax = $("#usaTax").val();
    _usaTax = parseFloat(_usaTax.replace(",", ""))
    if (isNaN(_usaTax)) {
        valid = false;
        showNotify("#usaTax");
        showNotice("Hệ thống", " Thuế mỹ không nhập đúng định dạng");
    }
    var _surcharge = $("#surcharge").val();
    _surcharge = parseFloat(_surcharge.replace(",", ""))
    if (isNaN(_surcharge)) {
        valid = false;
        showNotify("#surcharge");
        showNotice("Hệ thống", " Phụ thu không nhập đúng định dạng");
    }
      
    var _usaShipFee = $("#usaShipFee").val();
    _usaShipFee = parseFloat(_usaShipFee.replace(",", ""))
    if (isNaN(_usaShipFee)) {
        valid = false;
        showNotify("#usaShipFee");
        showNotice("Hệ thống", " Phí Ship Mỹ nhập không nhập đúng định dạng");
    }

    var _Feeprocesstransactions = $("#Feeprocesstransactions").val();
    _Feeprocesstransactions = parseFloat(_Feeprocesstransactions.replace(",", ""))
    if (isNaN(_Feeprocesstransactions)) {
        valid = false;
        showNotice("Hệ thống", " Phí xử lý giao dịch nhập không nhập đúng định dạng");
    }
    
    
    var _sst = $("#sst option:selected").val();

    var _prType = $("#prType option:selected").val();
    var _wareHouseId = $("#wareHouseId option:selected").val();
   
    var _weight = $("#weight").val();

    var _tracking = $("#Tracking").val();
    var _freght = $("#warranty").val();
    var _detail = $("#detail").val();
    var _id = getParameterByName("id");
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: {
                op: "updateFullProduct", id: _id, detail: _detail, tracking: _tracking, weight: _weight, freght: _freght, wareHouseId: _wareHouseId, prType: _prType, sst: _sst, 
                Feeprocesstransactions: _Feeprocesstransactions, usaShipFee: _usaShipFee, PaymentFee: 0, surcharge: _surcharge, usaTax: _usaTax,
                price: _price, exTime: _exTime, quantyti: _quantyti, name: _name, st: Anpero.CurentStore


            },
            success: function (msg) {
               
                showNotice("Hệ thống", " Cập thành công " + msg + " bản ghi");
                Common.PlaySuccessSound();
            }
        });
    }

}
function checkPrValid() {

};
function BindProductType1() {
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "BindProductType", st: Anpero.CurentStore },
            success: function (msg) {
                $("#table").html(msg);
            }
        });
    }
}
function BindProductType2() {
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "BindProductType2", st: Anpero.CurentStore },
            success: function (msg) {
                $("#prType").html(msg);
            }
        });
    }
}
function addwh() {
    var valid = true;
    name = $("#whN").val();
    if (name == null || name == "") {
        valid = false;
        showNotice("Hệ thống", "Tên kho quá ngắn");
        showNotify("#whN");
    }
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "addwareHouse", names: name, st: Anpero.CurentStore },
            success: function (msg) {
                if (msg == 1) {
                    BindWhType1();
                    BindWareHouse2();
                    showNotice("Hệ thống", " Cập thành công");

                } else {
                    showNotice("Hệ thống", " Cập thành Lỗi");
                }
            }
        });
    }
}
function BindWareHouse2() {
    var valid = true;
    if (valid) {


        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "BindWareHouse2", st: Anpero.CurentStore },
            success: function (msg) {
                $("#whcontent").html(msg);
            }
        });
    }
}
function BindWhType1() {
    var valid = true;
    if (valid) {


        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "BindWareHouse", st: Anpero.CurentStore },
            success: function (msg) {
                $("#tablewh").html(msg);
            }
        });
    }
}

function delWh(id){
    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",
        data: { op: "delWh", id: id, st: Anpero.CurentStore },
        success: function (msg) {
            BindWhType1();
            BindWareHouse2();
        }
    });
}
function UpdateWh(id) {
    var valid = true;

    var name = $("#wh" + id).val();
    if (valid) {

        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "UpdateWh", id: id, name: name, st: Anpero.CurentStore },
            success: function (msg) {
                BindWhType1();
                BindWareHouse2();

            }
        });
    }
}
function addBank() {
    var valid = true;
    name = $("#bank").val();
    if (name == null || name == "") {
        valid = false;
        showNotice("Hệ thống", "Tên kho quá ngắn");
        showNotify("#bank");
        
    }
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "addBank", names: name, st: Anpero.CurentStore },
            success: function (msg) {
                if (msg == 1) {
                    BindBType2();
                    BindBType1();
                    showNotice("Hệ thống", " Cập thành công");

                } else {
                    showNotice("Hệ thống", " Cập thành Lỗi");
                }
            }
        });
    }
}
function BindBType1() {
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "BindBank", st: Anpero.CurentStore },
            success: function (msg) {
                $("#tableBank").html(msg);
            }
        });
    }
}
function BindBType2() {
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "BindBankType2", st: Anpero.CurentStore },
            success: function (msg) {
                $("#BankContent").html(msg);
            }
        });
    }
}
function delB(id) {
    $.ajax({
        type: "post",
        url: "/handler/orderHandler.ashx",
        data: { op: "delBank", id: id, st: Anpero.CurentStore },
        success: function (msg) {
            BindBType2();
            BindBType1();
        }
    });
}
function bindAll() {
    BindProductType2();
    BindProductType1();
    BindWhType1();
    BindWareHouse2();
    BindBType1();
    BindBType2();
    getCashById();
}
function getCashById() {
    var valid = true;
    var _prId = getParameterByName("id");
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "getCashById", prId: "ODS" + _prId, st: Anpero.CurentStore },
            success: function (msg) {
                $("#cashContent").html(msg);
            }
        });
    }
}
function UpdatePrCustom() {
    var _cusId=$("#customerName").val();
    var valid = true;
    var _prId = getParameterByName("id");
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "updateOrderAddByPrId", id: _prId, cusId: _cusId, st: Anpero.CurentStore },
            success: function (msg) {
                showNotice("Hệ thống", " Cập nhật thành công " + msg + " đơn hàng");
                playSound02();
            }
        });
    }
}
function s(page) {
    var _prDetail = $("#prDetail").val();
    var _makh = $("#makh").val();
    var _prType = $("#prType :selected").val();
    var _prSst = $("#prSst :selected").val();   
    var valid = true;
    if (valid) {
        $.ajax({
            type: "post",
            url: "/handler/orderHandler.ashx",
            data: { op: "searchPr", prDetail: _prDetail, makh: _makh, prType: _prType, prSst: _prSst, p: page, st: Anpero.CurentStore },
            success: function (msg) {
                $("#prSearchContent").html(msg);
                $("input:text").focus(function () { $(this).select(); });
                $('.customId').each(function () { 
                    $(this).qtip({ // 
                        content: {
                            text: $(this).find('.cont').html() 
                        }
                    });
                });

            }
        });
    }

}
