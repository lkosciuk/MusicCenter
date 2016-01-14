AddConcertScope = function () {

    var map;

    this.Init = function () {
        ShowSelectedBand($('#BandName').val());
        this.SetupAutocomplete();
        this.SetupJQueryDatePicker();
        $('#cover').change(this.UpdateAlbumCover);
        $('#DetailsTab').click(ShowDetailsPanel);
        $('#LocalizationTab').click(ShowLocalizationPanel);
        SetupGoogleMap();
        $('#CreateConcertBtn').click(this.CreateConcert);
    }

    this.CreateConcert = function () {
        var geocoder = new google.maps.Geocoder();

        var address = document.getElementById("search-input").value;

        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                $('#MapLocation').val(results[0].geometry.location);

                if ($('#AddConcertForm').valid())
                {
                    $('#AddConcertForm').submit();
                }
                else
                {
                    if (!$('#Date').valid())
                    {
                        ShowDetailsPanel();
                    }
                    else
                    {
                        ShowLocalizationPanel();
                    }
                }
               
            } else {
                ShowLocalizationPanel();               
            }
        });  
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

        google.maps.event.trigger(map, "resize");
    }

    this.SetupJQueryDatePicker = function () {
        jQuery('#Date').datetimepicker();
    }

    function split(val) {
        return val.split(/,\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }

    this.SetupAutocomplete = function () {

        var bandNames = [];

        $.get($('#BandSelect').data('url'), null, function (data) {

            var index = data.indexOf($('#BandName').val());
            if (index !== -1) {
                data.splice(index, 1);
            }
            bandNames = data;
        })

        $("#BandSelect")
          // don't navigate away from the field on tab when selecting an item
          .bind("keydown", function (event) {
              if (event.keyCode === $.ui.keyCode.TAB &&
                  $(this).autocomplete("instance").menu.active) {
                  event.preventDefault();
              }
          })
          .autocomplete({
              minLength: 0,
              source: function (request, response) {
                  // delegate back to autocomplete, but extract the last term
                  response($.ui.autocomplete.filter(
                    bandNames, extractLast(request.term)));
              },
              focus: function () {
                  // prevent value inserted on focus
                  return false;
              },
              select: function (event, ui) {
                  var terms = split(this.value);
                  // remove the current input
                  terms.pop();

                  if (!$('#BandsContainer:contains("' + ui.item.value + '")').length > 0)
                  {
                      // add the selected item
                      terms.push(ui.item.value);
                      // add placeholder to get the comma-and-space at the end
                      terms.push("");
                      this.value = terms.join(", ");

                      ShowSelectedBand(ui.item.value);
                  }

                  return false;
              },
              appendTo: "#menu-container"
          });
    }

    var ShowSelectedBand = function (BandName) {

            $.get($("#BandsContainer").data('url'), { BandName: BandName }, function (result) {
                $("#BandsContainer").append(result);

                $("input#" + BandName).click(function () {
                    $("div#" + BandName).remove();
                    var BandSelectVal = $('#BandSelect').val();
                    var newValue = BandSelectVal.replace(BandName + ', ', "");

                    $('#BandSelect').val(newValue);

                })
            })
    }

    this.UpdateAlbumCover = function () {

        var input = $('#cover')[0];
        var ext = input.files[0]['name'].substring(input.files[0]['name'].lastIndexOf('.') + 1).toLowerCase();
        if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#concertCover').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        } else {
            $('#concertCover').attr('src', '/Content/Uploads/DefaultAlbumAv.png');
            $('#cover').val("");
        }
    }

    var SetupGoogleMap = function () {

        map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: 52, lng: 15 },
            zoom: 7,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

        google.maps.event.trigger(map, "resize");

        var input = document.getElementById('search-input');
        var searchBox = new google.maps.places.SearchBox(input);
        map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

        // Bias the SearchBox results towards current map's viewport.
        map.addListener('bounds_changed', function () {
            searchBox.setBounds(map.getBounds());
        });

        var markers = [];
        // Listen for the event fired when the user selects a prediction and retrieve
        // more details for that place.
        searchBox.addListener('places_changed', function () {
            var places = searchBox.getPlaces();

            if (places.length == 0) {
                return;
            }

            // Clear out the old markers.
            markers.forEach(function (marker) {
                marker.setMap(null);
            });
            markers = [];

            // For each place, get the icon, name and location.
            var bounds = new google.maps.LatLngBounds();
            places.forEach(function (place) {
                var icon = {
                    url: place.icon,
                    size: new google.maps.Size(71, 71),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(17, 34),
                    scaledSize: new google.maps.Size(25, 25)
                };

                // Create a marker for each place.
                markers.push(new google.maps.Marker({
                    map: map,
                    icon: icon,
                    title: place.name,
                    position: place.geometry.location
                }));

                if (place.geometry.viewport) {
                    // Only geocodes have viewport.
                    bounds.union(place.geometry.viewport);
                } else {
                    bounds.extend(place.geometry.location);
                }
            });
            map.fitBounds(bounds);
        });
    }
}

$(document).ready(function () {
    var scope = new AddConcertScope();
    scope.Init();
})
//&callback=SetupGoogleMap
