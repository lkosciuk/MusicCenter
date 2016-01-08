AddConcertScope = function () {

    this.Init = function () {
        
        this.SetupAutocomplete();
        this.SetupJQueryDatePicker();
        $('#cover').change(this.UpdateAlbumCover);
        $('#DetailsTab').click(this.ShowDetailsPanel);
        $('#LocalizationTab').click(this.ShowLocalizationPanel);
        $('#GalleryTab').click(this.ShowGalleryPanel);
        SetupGoogleMap();
    }

    this.ShowDetailsPanel = function () {
        $('#DetailsPanel').css("visibility", "visible");
        $('#DetailsPanel').css("height", "auto");
        $('#DetailsPanel').css("width", "auto");

        $('#LocalizationPanel').css("visibility", "hidden");
        $('#LocalizationPanel').css("height", 0);
        $('#LocalizationPanel').css("width", 0);

        $('#GalleryPanel').css("visibility", "hidden");
        $('#GalleryPanel').css("height", 0);
        $('#GalleryPanel').css("width", 0);

        $('#DetailsTab').attr("class", "active");
        $('#LocalizationTab').attr("class", "");
        $('#GalleryTab').attr("class", "");
    }
    
    this.ShowLocalizationPanel = function () {
        $('#DetailsPanel').css("visibility", "hidden");
        $('#DetailsPanel').css("height", 0);
        $('#DetailsPanel').css("width", 0);

        $('#LocalizationPanel').css("visibility", "visible");
        $('#LocalizationPanel').css("height", "auto");
        $('#LocalizationPanel').css("width", "auto");

        $('#GalleryPanel').css("visibility", "hidden");
        $('#GalleryPanel').css("height", 0);
        $('#GalleryPanel').css("width", 0);

        $('#DetailsTab').attr("class", "");
        $('#LocalizationTab').attr("class", "active");
        $('#GalleryTab').attr("class", "");
    }

    this.ShowGalleryPanel = function () {
        $('#DetailsPanel').css("visibility", "hidden");
        $('#DetailsPanel').css("height", 0);
        $('#DetailsPanel').css("width", 0);

        $('#LocalizationPanel').css("height", 0);
        $('#LocalizationPanel').css("width", 0);
        $('#LocalizationPanel').css("visibility", "hidden");

        $('#GalleryPanel').css("visibility", "visible");
        $('#GalleryPanel').css("height", "auto");
        $('#GalleryPanel').css("width", "auto");

        $('#DetailsTab').attr("class", "");
        $('#LocalizationTab').attr("class", "");
        $('#GalleryTab').attr("class", "active");
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

        var map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8
        });

    }
}

$(document).ready(function () {
    var scope = new AddConcertScope();
    scope.Init();
})
//&callback=SetupGoogleMap
