var markers = []; var map; var marker; var mk = 0; var md = 0;
var initialLocation;
var browserSupportFlag = new Boolean();
function setMap() {
    if (confirm("Bạn muốn chọn tọa độ này cho bài viết!")) {
        circle_centre = map.getCenter();
        var lat = circle_centre.lat();
        var lng = circle_centre.lng();
        mk = lat;
        md = lng;
    }

}
function setMapV2() {

    circle_centre = map.getCenter();
    var lat = circle_centre.lat();
    var lng = circle_centre.lng();
    var r = $("#radius option:selected").val();
    window.location.href = "/news/local?lat=" + lat + "&lng=" + lng + "&r=" + r;

}
function GmapInitializeWithInput() {
    $("#pac-input").hide();
    setTimeout(function () {
        $("#pac-input").show();
        GmapInitialize();
    }, 3000);

}


function GmapInitialize() {
    var mapOptions = {
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.DRIVING,
        center: new google.maps.LatLng(21.029041309706972, 105.85201621055603)
    };

    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    // Create the search box and link it to the UI element.
    var input = (document.getElementById('pac-input'));
    //  RIGHT_BOTTOM
   // map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    var searchBox = new google.maps.places.SearchBox(input);

    google.maps.event.addListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();
        if (places.length == 0) {
            return;
        }
        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }
        // For each place, get the icon, place name, and location.
        markers = [];
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {
            var image = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };
            // Create a marker for each place.
            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                title: place.name,
                position: place.geometry.location
            });

            markers.push(marker);
            bounds.extend(place.geometry.location);
        }
        map.fitBounds(bounds);
        map.setZoom(16);
    });
    google.maps.event.addListener(map, 'bounds_changed', function () {
        var bounds = map.getBounds();
        searchBox.setBounds(bounds);
    });
    marker = new google.maps.Marker({
        position: map.getCenter(),
        map: map,
        title: '...'
    });
    google.maps.event.addListener(map, 'center_changed', function () {
        marker.setMap(null);
        marker = new google.maps.Marker({
            position: map.getCenter(),
            map: map,
            // uncomment để hiệu ứng rơi maker	
            //draggable: true,
            // animation: google.maps.Animation.DROP,
            title: 'Click to zoom',
            url: 'https://ola88.com',
            labelContent: "chọn địa điểm này ",
            labelAnchor: new google.maps.Point(3, 30),
            labelClass: "labels",
            labelInBackground: true
        });
        // mk = map.getCenter().k;
        //md = map.getCenter().D;
        google.maps.event.addListener(marker, 'click', function (event) {
            map.setZoom(8);
            event.latLng
            map.setCenter(event.latLng);
            map.setCenter(marker.getPosition());
            //    window.location.href = marker.url;
            if (confirm("Bạn muốn chọn tọa độ này cho bài viết!")) {
                mk = event.latLng.k;
                md = event.latLng.D;
            }
        });
    });
    //google.maps.event.addListener(marker, 'click', toggleBounce);
    google.maps.event.addListener(marker, 'click', function (event) {
        // map.setZoom(8);
        marker.setMap(null);

        map.setCenter(event.latLng);
        // map.setCenter(marker.getPosition());
        //window.location.href = marker.url;
        //alert(marker.url);
        if (confirm("Bạn muốn chọn tọa độ này cho bài viết!")) {
            mk = event.latLng.k;
            md = event.latLng.D;
        }

    });   
}
function toggleBounce() {
    if (marker.getAnimation() != null) {
        marker.setAnimation(null);
    } else {
        marker.setAnimation(google.maps.Animation.BOUNCE);
    }
}
