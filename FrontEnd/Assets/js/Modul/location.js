
var Location = {
    districtId: 0,
    ProvinceId: 0,
    Init: function () {
        $(document).ready(function () {
            Location.Getlocation();                
            if (Location.ProvinceId > 0) {
                Location.Getlocation(Location.ProvinceId);    
            }
        });
    },
    Getlocation: function (_parentLocation) {        
        $.ajax({
            method: "post",
            url: "/location/getLocation",            
            data: { ParentLocationId: _parentLocation },
            success: function (rs) {     
                
                if (rs != null && rs.length > 0) {
                    var html = "";
                    rs.forEach(function (item) {
                        html += "<a class=\"dropdown-item\" data-id=\"" + item.Id + "\" data-text=\""+item.Name+"\">"+item.Name+"</a>";
                    });
                     
                    if (_parentLocation == null || _parentLocation == 0) {                        
                        $("#select-Province").html(html);
                        $("#select-Province .dropdown-item").unbind("click");
                        $("#select-Province .dropdown-item").click(function () {
                            $("#btn-selectCity").data("id", $(this).data("id"));
                            $("#btn-selectCity").text($(this).data("text"));
                            Location.Getlocation($(this).data("id"));
                        })
                    } else {
                        $("#district-content").html(html);
                        $("#district-content .dropdown-item").unbind("click");
                        $("#district-content .dropdown-item").click(function () {
                            $("#district-selected").data("id", $(this).data("id"));
                            $("#district-selected").text($(this).html());                          
                        })
                        $("#prov").html(rs);
                    }
                }
               
          
            }
        });
    }
};  