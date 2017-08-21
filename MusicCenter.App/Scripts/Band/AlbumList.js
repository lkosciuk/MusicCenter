var AlbumListScope = function () {

    this.Init = function () {
        $.each($('[name=AddAlbumToFavBtn]'), function () {
            $(this).click(AddAlbumToFavourites);
        });
        IfUserAddedAlbumsToFavourites();
    };

    this.OnGridLoaded = function () {
        IfUserAddedAlbumsToFavourites();
        $.each($('[name=AddAlbumToFavBtn]'), function () {
            $(this).click(AddAlbumToFavourites);
        });
    }

    var AddAlbumToFavourites = function () {
        var currentButton = this;
        var albumName = $(currentButton).data('albumname');
        $.post($("#albumListData").data('addtofavurl'), { AlbumName: albumName }, function () {
            $(currentButton).hide();
        });
    };

    var IfUserAddedAlbumsToFavourites = function () {

        var user = $("#albumListData").data('user');
        if (user) {
            var url = $("#albumListData").data('checkfavurl');
            var currentPageAlbumNames = new Array();

            $.each($('[name=AddAlbumToFavBtn]'), function () {
                currentPageAlbumNames.push($(this).data('albumname'));
            });

            $.ajax({
                url: url,
                type: "POST",
                data: JSON.stringify({ AlbumNames: currentPageAlbumNames }),
                contentType: "application/json",
                dataType: "json",
                success: function (result) {

                    $.each(result, function (index, value) {
                        if (value.IsInFavourites) {
                            $("[name=AddAlbumToFavBtn][data-albumname='" + value.Name + "']").hide();
                        }
                    });

                }
            });
        }
    };
}

$(document).ready(function () {
    albumListScope = new AlbumListScope();
    albumListScope.Init();
})