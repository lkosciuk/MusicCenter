AddConcertScope = function () {

    this.Init = function () {
        
        this.SetupAutocomplete();
        this.SetupJQueryDatePicker();
    }

    this.SetupJQueryDatePicker = function () {

        //$("#Date").datepicker({

        //    beforeShow: function () {
        //        setTimeout(function () {
        //            $('.ui-datepicker').css('z-index', 99999999999999);
        //        }, 0);
        //    },
        //    dateFormat: 'dd-mm-yy'
        //});
        jQuery('#Date').datetimepicker();
    }

    function split(val) {
        return val.split(/,\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }

    this.SetupAutocomplete = function () {

        var bandNames = [//testowo
          "Band1",
          "Band2", ];

        //$("#BandSelect").autocomplete({
        //    source: bandNames,
        //    appendTo: '#menu-container'
        //});

        

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
                  // add the selected item
                  terms.push(ui.item.value);
                  // add placeholder to get the comma-and-space at the end
                  terms.push("");
                  this.value = terms.join(", ");
                  return false;
              },
              appendTo: "#menu-container"
          });
    }
}

$(document).ready(function () {
    var scope = new AddConcertScope();
    scope.Init();
})