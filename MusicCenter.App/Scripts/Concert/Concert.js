ConcertScope = function () {

    var map;

    this.Init = function () {
        $('#DetailsTab').click(ShowDetailsPanel);
        $('#LocalizationTab').click(ShowLocalizationPanel);
        SetupGoogleMap();
    }

    var ShowDetailsPanel = function () {
        $('#DetailsPanel').css("visibility", "visible");
        $('#DetailsPanel').css("height", "auto");
        $('#DetailsPanel').css("width", "auto");

        $('#LocalizationPanel').css("visibility", "hidden");
        $('#LocalizationPanel').css("height", 0);
        $('#LocalizationPanel').css("width", 0);

        $('#DetailsTab').attr("class", "active");
        $('#LocalizationTab').attr("class", "");
    }

    var ShowLocalizationPanel = function () {
        $('#DetailsPanel').css("visibility", "hidden");
        $('#DetailsPanel').css("height", 0);
        $('#DetailsPanel').css("width", 0);

        $('#LocalizationPanel').css("visibility", "visible");
        $('#LocalizationPanel').css("height", "auto");
        $('#LocalizationPanel').css("width", "auto");

        $('#DetailsTab').attr("class", "");
        $('#LocalizationTab').attr("class", "active");

        var Latitude = parseFloat($("#Latitude").val());
        var Longitude = parseFloat($("#Longitude").val());

        var LatLng = { lat: Latitude, lng: Longitude };

        google.maps.event.trigger(map, "resize");
        map.setCenter(LatLng);
    }

    var SetupGoogleMap = function () {

        var Latitude = parseFloat($("#Latitude").val());
        var Longitude = parseFloat($("#Longitude").val());

        var LatLng = { lat: Latitude, lng: Longitude };

        map = new google.maps.Map(document.getElementById('map'), {
            center: LatLng,
            zoom: 15,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

        var marker = new google.maps.Marker({
            position: LatLng,
            map: map,
        });

        google.maps.event.trigger(map, "resize");
        map.setCenter(LatLng);
    }
}

$(document).ready(function () {

    var scope = new ConcertScope();
    scope.Init();
})