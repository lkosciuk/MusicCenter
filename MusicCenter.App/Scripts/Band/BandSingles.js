BandSinglesScope = function () {

    this.Init = function () {
        $.each($('[name=AddSongToFavBtn]'), function () {
            $(this).click(AddSongToFavourites);
        })

        this.IfUserAddedSongToFavourites();

        $.each($('[name = EditSingleBtn]'), function () {
            $(this).click(GetUpdateSingleView);
        })

        $.each($('[name = DeleteSingleBtn]'), function () {
            $(this).click(ShowSingleName);
        })

        $('#DeleteConfirmBtn').click(DeleteSingle);
    }

    var AddSongToFavourites = function (e) {

        var songId = $(e.currentTarget).attr('id');
        $.post($('#' + songId).data('url'), { SongId: songId }, function () {
            $('#' + songId).attr('disabled', true);
        });
    }

    this.IfUserAddedSongToFavourites = function () {

        var url = $('[name = AddSongToFavBtn]').data('checkurl');

        $.each($('[name=AddSongToFavBtn]'), function () {
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

    var GetUpdateSingleView = function (e) {
        var url = $(e.currentTarget).data('url');

        location.href = url;
    }

    var ShowSingleName = function (e) {
        var singleName = $(e.currentTarget).data('singlename');
        var singleId = $(e.currentTarget).data('singleid');
        $('#SingleName').empty();
        $('#SingleName').append(singleName);
        $('#SingleName').data("songId", singleId);
    }

    var DeleteSingle = function () {
        var singleId = $('#SingleName').data("songId");
        var url = $('#DeleteConfirmBtn').data('url');

        $.post(url, { SingleId: singleId }, function () {
            $('#confirm-submit').modal('hide');
            location.reload();
        })
    }
}

$(document).ready(function () {
    var scope = new BandSinglesScope();
    scope.Init();
})