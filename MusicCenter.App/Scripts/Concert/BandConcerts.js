BandConcertsScope = function () {

    this.Init = function () {
        $.each($('[name = EditConcertBtn]'), function () {
            $(this).click(GetUpdateConcertView);
        })

        $.each($('[name = DeleteConcertBtn]'), function () {
            $(this).click(ShowConcertAddr);
        })

        $('#DeleteConfirmBtn').click(DeleteConcert);

    }

    var GetUpdateConcertView = function (e) {
        var url = $(e.currentTarget).data('url');

        location.href = url;
    }

    var ShowConcertAddr = function (e) {
        var address = $(e.currentTarget).data('address');
        var concertId = $(e.currentTarget).data('concertid');

        $('#ConcertAddr').empty();
        $('#ConcertAddr').append(address);

        $('#ConcertAddr').data("concertid", concertId);
    }

    var DeleteConcert = function () {
        var concertid = $('#ConcertAddr').data("concertid");
        var url = $('#DeleteConfirmBtn').data('url');

        $.post(url, { ConcertId: concertid }, function () {
            $('#confirm-submit').modal('hide');
            location.reload();
        })
    }
}

$(document).ready(function () {
    var bandConcerts = new BandConcertsScope();
    bandConcerts.Init();
})

