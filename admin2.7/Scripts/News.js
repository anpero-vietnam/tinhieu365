function bindCat() {
    $.ajax({
        type: "post", url: "/Handler/categoryHandler.ashx",
        dataType: "text",
        data: { op: "getCatList", st: Anpero.CurentStore },
        success: function (msg) {
            $(".select").html(msg);

        }
    });
}
var news = {
    cat: 0,
    id: 0,
    addCategory: function () {
        var valid = true;
        
        var subName = $("#subName").val();
        if (subName == "" || subName == null) {
            valid = false;
            alert("Vui lòng nhập tên chủ đề cho tin");
            return;
        }
        
            if (valid) {
                $.ajax({
                    type: "post",
                    dataType: "text",
                    url: "/Handler/categoryHandler.ashx",
                    data: { op: "addCategory", title: subName, st: Anpero.CurentStore },
                    success: function (msg) {
                        bindSub();
                        showNotice("Thông báo", msg);
                        Common.PlaySuccessSound();
                    }
                });
            }
    },
    UpdateNew: function () {
        var valid = true;
        var editor = CKEDITOR.instances.editor1;
        var content = editor.getData();
        var tittle = $("#tittle").val();
        var _shortDesc = $("#shortDesc").val();
        var view = $("#view").val();
        var publish = $("#publish option:selected").val();
        var prioty = $("#prioty").val();

        if (tittle.length < 5) {
            showNotice("Thông báo", "Tiêu đề quá ngắn");
            $("#tittle").removeClass("valid");
            $("#tittle").addClass("error");
            valid = false;
            return;
        }
        var thumb = $("#prThumb").attr("src");
        var tag = $("#tag").val();
        if (news.cat == "" || news.cat == null) {
            showNotice("Thông báo", "VUi lòng chọn chủ đề con");
            Common.PlayErrorSound();
            valid = false;
            return;
        }
        if (valid) {
            $.ajax({
                type: "post",
                dataType: "text",
                data: { op: "Updatenews", id: news.id, tittle: tittle, shortDes: _shortDesc, thumb: thumb, tag: tag, newContent: content, SubCatId: news.cat, prioty: prioty, publish: publish, view: view, st: Anpero.CurentStore },
                url: "/handler/newhandler.ashx",
                success: function (msg) {
                    alert("Cập nhật tin thành công");
                    playSound02();
                    window.location = "/news/search?st=" + Anpero.CurentStore;
                }
            });
        }
    },
    addNew: function () {
        var editor = CKEDITOR.instances.editor1;
        var content = editor.getData();
        var tittle = $("#tittle").val();
        var view = $("#view").val();
        var publish = $("#publish").val();
        var prioty = $("#prioty").val();
        if (tittle.length < 5) {
            showNotice("Thông báo", "Tiêu đề tin quá ngắn");
            $("#tittle").removeClass("valid");
            $("#tittle").addClass("error");
            Common.PlayErrorSound();
            return;
        }
        var thumb = $("#prThumb").attr("src");
        var shotdesc = $("#shotdesc").val();
        if (shotdesc.length < 5) {
            showNotice("Thông báo", "Bạn chưa nhập miêu tả ngắn");
            return;
        }
        var tag = $("#tag").val();

        if (news.cat == 0) {
            showNotice("Thông báo", "Vui lòng chọn chủ đề cho tin");
            Common.PlayErrorSound();
            return;
        }

        $.ajax({
            type: "post",
            dataType: "html",
            data: { op: "Addnews", tittle: tittle, thumb: thumb, shotDes: shotdesc, tag: tag, newContent: content, SubCatId: news.cat, prioty: prioty, publish: publish, view: view, st: Anpero.CurentStore },
            url: "/handler/newhandler.ashx",
            success: function (msg) {
                Common.PlaySuccessSound();
                showNotice("Thông báo", "Thêm tin thành công");
                window.location = "/news/search?st=" + Anpero.CurentStore;
            }
        });
    },
    delNews: function (id) {
        $.ajax({
            type: "post", url: "/Handler/newHandler.ashx", dataType: "text",
            data: { op: "delNews", id: id, st: Anpero.CurentStore },
            success: function (msg) {
                location.reload();
                showNotice("Thông báo", "Xóa thành công");
                Common.PlaySuccessSound();
            }
        });

    },
    search: function (_page) {
        var _scat = $("input[name='radiocat']:checked").val();
        var _block = 1;
        if (!$('#isPublish').is(':checked')) {
            _block = 0;
        }
        if (_scat == null) { _scat = 0;}
        $.ajax({
            type: "post", url: "/Handler/newHandler.ashx", dataType: "text",
            data: { op: "search", block: _block, scat: _scat, st: Anpero.CurentStore, page: _page },
            success: function (msg) {
                $("#newsSeachContent").html(msg);
            }
        });

    }

};

function bindSub() {
    var catId = $("#cat").val();
    $.ajax({
        type: "post", url: "/Handler/categoryHandler.ashx",
        dataType: "text",
        data: { op: "getArticleCategory", catId: catId, st: Anpero.CurentStore },
        success: function (msg) {
            $(".table").html(msg);
        }
    });
}
function delSubCat(ids) {
    if (confirm("Bạn có chắc muốn xóa chủ đề này chứ?")) {
        $.ajax({
            type: "post", url: "/Handler/categoryHandler.ashx",
            dataType: "text",
            data: { op: "delSubCat", id: ids, st: Anpero.CurentStore },
            success: function (msg) {
                bindSub();
                showNotice("Thông báo", msg);
            }
        });
    }

}
function updateSubCat(ids) {
    $.ajax({
        type: "post", url: "/Handler/categoryHandler.ashx",
        dataType: "text",
        data: { op: "updateSubCat", id: ids, desc: $("#txt" + ids).val(), st: Anpero.CurentStore },
        success: function (msg) {
            bindSub();
            showNotice("Thông báo", msg);

        }
    });
}
function updateCat(ids) {
    $.ajax({
        type: "post",
        datatype: "text",
        data: { op: "updateCat", id: ids, desc: $("#txt" + ids).val(), st: Anpero.CurentStore },
        url: "/Handler/categoryHandler.ashx",
        success: function (msg) {
            showNotice("Thông báo", msg);
        }
    });

}
function bindSelectSub() {
    var catId = $("#cat").val();
    $.ajax({
        type: "post", url: "/Handler/categoryHandler.ashx",
        dataType: "text",
        data: { op: "bindSelectArticleCategory", catId: catId },
        success: function (msg) {
            $("#ScatContent").html(msg);
        }
    });
}



function bindTableCat() {
    $.ajax({
        type: "post", url: "/Handler/categoryHandler.ashx", dataType: "text",
        data: { op: "binCat", st: Anpero.CurentStore },
        success: function (msg) {
            $(".table").html(msg);
        }
    });
}
