BandAlbumsScope = function () {

    this.Init = function () {
        $.each($('[name = EditAlbumBtn]'), function () {
            $(this).click(GetUpdateAlbumView);
        })

        $.each($('[name = DeleteAlbumBtn]'), function () {
            $(this).click(ShowAlbumName);
        })

        $('#DeleteConfirmBtn').click(DeleteAlbum);
        
    }

    var GetUpdateAlbumView = function (e) {
        var url = $(e.currentTarget).data('url');

        location.href = url;
    }

    var ShowAlbumName = function (e) {
        var albumName = $(e.currentTarget).data('album');
        $('#AlbumName').empty();
        $('#AlbumName').append(albumName);
    }

    var DeleteAlbum = function () {
        var albumName = $('#AlbumName').text();
        var url = $('#DeleteConfirmBtn').data('url');

        $.post(url, { AlbumName: albumName }, function () {
            $('#confirm-submit').modal('hide');
            location.reload();
        })
    }
}

$(document).ready(function () {
    var bandAlbums = new BandAlbumsScope();
    bandAlbums.Init();
})

