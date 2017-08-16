var SongListScope = function () {

    this.Init = function () {
        $.each($('[name=AddSongToFavBtn]'), function () {
            $(this).click(AddSongToFavourites);
        });
        IfUserAddedSongsToFavourites();
    };

    this.OnGridLoaded = function () {
        IfUserAddedSongsToFavourites();
    }

    var AddSongToFavourites = function () {
        var currentButton = this;
        var songId = $(currentButton).data('songid');
        $.post($("#songListData").data('addtofavurl'), { SongId: songId }, function () {
            $(currentButton).hide();
        });
    };

    var IfUserAddedSongsToFavourites = function () {

        var user = $("#songListData").data('user');
        if (user) {
            var url = $("#songListData").data('checkfavurl');
            var currentPageSongIds = new Array();

            $.each($('[name=AddSongToFavBtn]'), function () {
                currentPageSongIds.push($(this).data('songid'));
            });

            $.ajax({
                url: url,
                type: "POST",
                data: JSON.stringify({ SongIds: currentPageSongIds }),
                contentType: "application/json",
                dataType: "json",
                success: function (result) {

                    $.each(result, function (index, value) {
                        if (value.IsInFavourites) {
                            $("[name=AddSongToFavBtn][data-songid='" + value.Name + "']").hide();
                        }
                    });

                }
            });
        }
    };
}

$(document).ready(function () {
    songListScope = new SongListScope();
    songListScope.Init();
})