
var ProjectControl = {
    Images: "",
    fileUploaded: {},
    Init: function () {
        ProjectControl.setUpEvent();

    },
    setUpEvent: function () {
        $("#imgUpdate").smallFileUpload({
            allowExtension: ".jpg,.png,.jpeg,.svg,.gif,.ico",
            Size: "400x400",
            multiple: true,
            onError: function (msg) {
                $("#file-upload-notifycation").html(msg);
            },
            success: function (data) {
                $("#logo").attr("src", data.uploadedImages[0].Url);
                if (data.code == 200) {
                    $("#file-upload-notifycation").html("Upload images successfully");
                } else {
                    $("#file-upload-notifycation").html(data.messege);
                }
            }
        });
        $("#btn-update").click(function () {
            var _data = ProjectControl.getData();

            ProjectControl.UpdateProject(_data);
        });


    },
    UpdateProject: function (_datas) {
        $.ajax({
            type: "post",
            data: _datas,
            url: "/Project/Update",
            success: function (rs) {
                if (rs != null && parseInt(rs) > 0) {
                    window.location.href = "/project/createnew?Ticker=" + _datas.Ticker;
                } else {
                    Common.showNotice("Error", rs.Messenger);
                }
            }
        });
    },        
    getData: function () {    
        var editor = CKEDITOR.instances.editor1;
        var content = editor.getData();
        var data = {};        
        data.Ticker = Common.getParameterByName("Ticker");
        
        data.BusinessName = $("#txt-title").val();
        data.EngName = $("#txt-EnName").val();
        data.StockExchange = $("#txt-StockExchange").val();
        
        data.Email = $("#txt-Email").val();
        data.Website = $("#txt-Website").val();
        data.Phone = $("#txt-Phone").val();
        data.Address = $("#txt-Address").val();
        data.Description = content;
        data.Logo = $("#logo").attr("src");
        data.IsWatchList = $("#ck-watchlist").is(":checked");
        return data;
    }
};
$(document).ready(function () {
    ProjectControl.Init();
});