var UserFavouritesScope = function () {

    this.Init = function () {
        HideAllTabContents(function () { return ShowTabContent("bands") });
        $('[name="favTab"]').click(TabClick);

    };

    var TabClick = function () {
        var tab = this;
        $(tab).addClass('active');

        $.each($('[name="favTab"]'), function (index, item) {
            if (item != tab) {
                $(item).removeClass('active');
            }
        });

        HideAllTabContents(function () { return ShowTabContent(tab.id) });
    };

    var ShowTabContent = function (contentName) {
            if (contentName == 'bands') {
                $('#favouritesBands').show();
            }
            else if (contentName == 'albums') {
                $('#favouritesAlbums').show();
            }
            else if (contentName == 'songs') {
                $('#favouritesSongs').show();
            }
            else if (contentName == 'concerts') {
                $('#favouritesConcerts').show();
            }
    };

    var HideAllTabContents = function (callback) {
        var count = $('[name="tabContent"]').length;

        $.each($('[name="tabContent"]'), function (index, item) {
            $(item).hide();
            if (index + 1 == count) {
                callback();
            }
        });
    };
}


$(document).ready(function () {
    var userFavourites = new UserFavouritesScope();
    userFavourites.Init();
});