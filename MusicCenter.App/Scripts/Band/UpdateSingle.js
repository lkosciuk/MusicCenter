UpdateSingleScope = function () {

    this.Init = function () {
        this.SetupJQueryDatePicker();
    }

    this.SetupJQueryDatePicker = function () {

        $("#ReleaseDate").datepicker({

            beforeShow: function () {
                setTimeout(function () {
                    $('.ui-datepicker').css('z-index', 99999999999999);
                }, 0);
            },
            dateFormat: 'dd-mm-yy'
        });
    }
}

$(document).ready(function () {
    var scope = new UpdateSingleScope();
    scope.Init();
})