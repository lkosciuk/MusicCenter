var BandListScope = function () {

    this.Init = function () {
        $('#AddBandToFavBtn').click(AddBandToFavourites);
        IfUserAddedBandToFavourites();
    };

    var AddBandToFavourites = function () {
        var currentButton = this;
        var bandName = $(currentButton).data('bandname');
        $.post($(currentButton).data('addtofavurl'), { BandName: bandName }, function () {
            $(currentButton).hide();
        });
    };

    var IfUserAddedBandToFavourites = function () {

        var user = $('#AddBandToFavBtn').data('user');
        if (user) {
            var url = $('#AddBandToFavBtn').data('checkfavurl');
            var bandNames = new Array();
            bandNames.push($('#AddBandToFavBtn').data('bandname'));

            $.ajax({
                url: url,
                type: "POST",
                data: JSON.stringify({ BandNames: bandNames }),
                contentType: "application/json",
                dataType: "json",
                success: function (result) {

                    $.each(result, function (index, value) {
                        if (value.IsInFavourites) {
                            $('#AddBandToFavBtn').hide();
                        }
                    });
                }
            });
        }
    };
}

$(document).ready(function () {
    bandListScope = new BandListScope();
    bandListScope.Init();
})