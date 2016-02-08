$(document).ready(function () {

    $("#my-calendar").zabuto_calendar({
        ajax: {
            url: $("#my-calendar").data('url'),
            modal: true
        }
    });

    //var calendarData = [];

    //$('input[name = ConcertItem]').each(function () {

    //    var concertid = $(this).data("concertid");
    //    var date = $(this).data("concertdate");
    //    var title = $(this).data("bands");;
    //    var body;

    //    $.get($("#my-calendar").data('url'), { ConcertId: concertid }, function (result) {
    //        body = result;
    //    });

    //    var dataItem = {
    //        "date": date,
    //        "badge": true,
    //        "title": title,
    //        "body": body,
    //        "classname": "purple-event"
    //    };

    //    calendarData.push(dataItem);

    //});

    //$("#my-calendar").zabuto_calendar({ data: calendarData, modal: true });
});