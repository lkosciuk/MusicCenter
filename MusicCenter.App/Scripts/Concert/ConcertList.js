var ConcertListScope = function () {

    this.Init = function () {
        $.each($('[name=AddConcertToFavBtn]'), function () {
            $(this).click(AddConcertToFavourites);
        });
        IfUserAddedConcertsToFavourites();
    };

    this.OnGridLoaded = function () {
        IfUserAddedConcertsToFavourites();
        $.each($('[name=AddConcertToFavBtn]'), function () {
            $(this).click(AddConcertToFavourites);
        });
    }

    var AddConcertToFavourites = function () {
        var currentButton = this;
        var concertId = $(currentButton).data('concertid');
        $.post($("#concertListData").data('addtofavurl'), { ConcertId: concertId }, function () {
            $(currentButton).hide();
        });
    };

    var IfUserAddedConcertsToFavourites = function () {

        var user = $("#concertListData").data('user');
        if (user) {
            var url = $("#concertListData").data('checkfavurl');
            var currentPageConcertIds = new Array();

            $.each($('[name=AddConcertToFavBtn]'), function () {
                currentPageConcertIds.push($(this).data('concertid'));
            });

            $.ajax({
                url: url,
                type: "POST",
                data: JSON.stringify({ ConcertIds: currentPageConcertIds }),
                contentType: "application/json",
                dataType: "json",
                success: function (result) {

                    $.each(result, function (index, value) {
                        if (value.IsInFavourites) {
                            $("[name=AddConcertToFavBtn][data-concertid='" + value.Name + "']").hide();
                        }
                    });

                }
            });
        }
    };
}

$(document).ready(function () {
    concertListScope = new ConcertListScope();
    concertListScope.Init();
})