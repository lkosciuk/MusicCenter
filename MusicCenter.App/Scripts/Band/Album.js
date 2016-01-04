AlbumScope = function () {

    this.Init = function () {
        $('#AddAlbumToFavBtn').click(this.AddAlbumToFavourites);
        $.each($('[name=AddSongToFav]'), function () {
            $(this).click(AddSongToFavourites);
        })
        this.IfUserAddedAlbumToFavourites();
        this.IfUserAddedSongToFavourites();
    }

    this.AddAlbumToFavourites = function () {
        var albumName = $('#AddAlbumToFavBtn').attr('name');
        $.post($('#AddAlbumToFavBtn').data('url'), { AlbumName: albumName }, function () {
            $('#AddAlbumToFavBtn').attr('disabled', true);
        });
    }

    var AddSongToFavourites = function (e) {

        var songId = $(e.currentTarget).attr('id');
        $.post($('#' + songId).data('url'), { SongId: songId }, function () {
            $('#' + songId).attr('disabled', true);
        });
    }

    this.IfUserAddedAlbumToFavourites = function() {

        var url = $('#AddAlbumToFavBtn').data('checkurl');
        var albumName = $('#AddAlbumToFavBtn').attr('name');

        $.post(url, {AlbumName: albumName}, function (result) {
            if (result === 'True') {
                $('#AddAlbumToFavBtn').attr('disabled', true);
            }
            else {
                $('#AddAlbumToFavBtn').attr('disabled', false);
            }
        });
    }

    this.IfUserAddedSongToFavourites = function () {

        var url = $('[name = AddSongToFav]').data('checkurl');

        $.each($('[name=AddSongToFav]'), function () {
            var songId = $(this).attr('id');

            $.post(url, { SongId: songId }, function (result) {
                if (result === 'True') {
                    $('#' + songId).attr('disabled', true);
                }
                else {
                    $('#' + songId).attr('disabled', false);
                }
            });
        })

    }
}

$(document).ready(function () {
    var albumScope = new AlbumScope();
    albumScope.Init();
})