var DataFilerScope = function () {

    this.Init = function () {

        InitGenresAutocomplete();
        InitBandsAutocomplete();

        $("#filterDateFrom").datepicker({
            beforeShow: function () {
                setTimeout(function () {
                    $('.ui-datepicker').css('z-index', 99999999999999);
                }, 0);
            },
            onSelect: function () {
                FilterData();
            },
            dateFormat: 'dd-mm-yy'
        });

        $("#filterDateTo").datepicker({
            beforeShow: function () {
                setTimeout(function () {
                    $('.ui-datepicker').css('z-index', 99999999999999);
                }, 0);
            },
            onSelect: function () {
                FilterData();
            },
            dateFormat: 'dd-mm-yy'
        });
    };

    var InitGenresAutocomplete = function () {
        var searchGenresUrl = $("#filterGenres").data('searchgenresurl');

        $("#filterGenres")
          .on( "keydown", function( event ) {
              if ( event.keyCode === $.ui.keyCode.TAB &&
                  $( this ).autocomplete( "instance" ).menu.active ) {
                  event.preventDefault();
              }
          })
          .autocomplete({
              minLength: 0,
              source: function( request, response ) {
                  jQuery.post(searchGenresUrl, {
                      query: extractLast(request.term)
                  }, function (data) {
                      response(data);
                  }, 'json');
              },
              focus: function() {
                  // prevent value inserted on focus
                  return false;
              },
              select: function( event, ui ) {
                  var terms = split( this.value );
                  // remove the current input
                  terms.pop();
                  // add the selected item
                  terms.push( ui.item.value );
                  // add placeholder to get the comma-and-space at the end
                  terms.push("");
                  this.value = terms.join(", ");

                  FilterData();
                  return false;
              }
          });

        var genresAutocompleteInstance = $("#filterGenres").autocomplete('instance');
        genresAutocompleteInstance._resizeMenu = function () {
            var ul = this.menu.element;
            ul.outerWidth(this.element.outerWidth());
        };

    };

    var InitBandsAutocomplete = function () {

        var viewName = $("#filterSearch").data('viewname');
        var searchUrl = $("#filterSearch").data('searchbandsurl');

        if (viewName == "AlbumList") {
            searchUrl = $("#filterSearch").data('searchalbumsurl');
        }

        if (viewName == "SongList") {
            searchUrl = $("#filterSearch").data('searchsongsurl');
        }

        if (viewName == "ConcertList") {
            searchUrl = $("#filterSearch").data('searchconcertsurl');
        }

        $("#filterSearch")
          .on("keydown", function (event) {
              if (event.keyCode === $.ui.keyCode.TAB &&
                  $(this).autocomplete("instance").menu.active) {
                  event.preventDefault();
              }
          })
          .autocomplete({
              minLength: 0,
              source: function (request, response) {
                  jQuery.post(searchUrl, {
                      query: extractLast(request.term)
                  }, function (data) {
                      response(data);
                  }, 'json');
              },
              focus: function () {
                  // prevent value inserted on focus
                  return false;
              },
              select: function (event, ui) {
                  var terms = split(this.value);
                  // remove the current input
                  terms.pop();
                  // add the selected item
                  terms.push(ui.item.value);
                  // add placeholder to get the comma-and-space at the end
                  terms.push("");
                  this.value = terms.join(", ");

                  FilterData();

                  return false;
              }
          });

        var bandsAutocompleteInstance = $("#filterSearch").autocomplete('instance');
        bandsAutocompleteInstance._resizeMenu = function () {
            var ul = this.menu.element;
            ul.outerWidth(this.element.outerWidth());
        };
    };

    var FilterData = function () {
        var form = $("#dataFilterForm");
        form.submit();
    };

    function split(val) {
        return val.split(/,\s*/);
    }

    function extractLast(term) {
        return split(term).pop();
    }
}

$(document).ready(function () {
    dataFilterScope = new DataFilerScope();
    dataFilterScope.Init();
})