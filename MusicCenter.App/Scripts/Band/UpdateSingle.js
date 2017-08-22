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
            dateFormat: 'yy-mm-dd'
        });
    }
}

$(document).ready(function () {
    var scope = new UpdateSingleScope();
    scope.Init();
})