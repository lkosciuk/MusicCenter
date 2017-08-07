var BandListScope = function () {

    this.Init = function () {
        $.each($('[name=AddBandToFavBtn]'), function () {
            $(this).click(AddBandToFavourites);
        });
        IfUserAddedBandsToFavourites();
    };

    this.OnGridLoaded = function () {
        IfUserAddedBandsToFavourites();
    }

    var AddBandToFavourites = function () {
        var currentButton = this;
        var bandName = $(currentButton).data('bandname');
        $.post($("#bandListData").data('addtofavurl'), { BandName: bandName }, function () {
            $(currentButton).hide();
        });
    };

    var IfUserAddedBandsToFavourites = function () {

        var user = $("#bandListData").data('user');
        if (user) {
            var url = $("#bandListData").data('checkfavurl');
            var currentPageBandNames = new Array();

            $.each($('[name=AddBandToFavBtn]'), function () {
                currentPageBandNames.push($(this).data('bandname'));
            });

            $.ajax({
                url: url,
                type: "POST",
                data: JSON.stringify({ BandNames: currentPageBandNames }),
                contentType: "application/json",
                dataType: "json",
                success: function (result) {

                    $.each(result, function (index, value) {
                        if (value.IsInFavourites) {
                            $("[name=AddBandToFavBtn][data-bandname='" + value.BandName + "']").hide();
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