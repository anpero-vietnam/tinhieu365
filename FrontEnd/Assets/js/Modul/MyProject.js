var MyProjectControl = {
    Init: function () {
        MyProjectControl.setUpEvent();
        
    },
    setUpEvent: function () {
        _data.Element = "#home .row";
        _data.IsPublish = true;
        MyProjectControl.getProject(1);
        $("#homeTab").click(function () {
            _data.closed = false;
            _data.IsPublish = true;
            _data.Element = "#home .row";
            MyProjectControl.getProject(1);
        });
        $("#close-porject-tab").click(function () {
            _data.closed = true;
            _data.IsPublish = true;
            _data.Element = "#menu1 .row";
            MyProjectControl.getProject(1);
        });
        $(".Waiting-project-tab").click(function () {
            _data.closed = false;
            _data.IsPublish = false;
            _data.Element = "#menu2 .row";
            MyProjectControl.getProject(1);
        });        
    },
    getProject: function (_page,elementContainer) {
        
        $.ajax({
            type: "post",
            url: "/ajax/SearchProjectAuth",
            data: { IsPublish: _data.IsPublish, Page: _page ?? 1, closed: _data.closed },
            success: function (rs) {
                $(_data.Element).html(rs);
            }
        });
    }

};
$(document).ready(function () {
    MyProjectControl.Init();
});