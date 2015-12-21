AddAlbumScope = function () {

    var songCounter = 0;
    var songLength = 0;

    var model;
    var uploads = [];

    this.Init = function () {
        this.SetupFileInput();
        this.SetupJQueryDatePicker();
        //this.UploadSong();
        $('#CreateAlbumBtn').click(this.CreateAlbum);
        //$("#AddAlbumForm").submit(this.CreateAlbum);
        $('#cover').change(this.UpdateAlbumCover);
        //$('#AddSongBtn').click(this.AddSongInput);
    }

    this.SetupJQueryDatePicker = function () {

        $("#ReleaseDate").datepicker({

            beforeShow: function () {
                setTimeout(function () {
                    $('.ui-datepicker').css('z-index', 99999999999999);
                }, 0);
            },
            dateFormat: 'dd-mm-yy'
        });
    }

    this.SetupFileInput = function () {

        $("#songInput").fileinput({
            uploadUrl: "error",
            showUpload: false,
            fileActionSettings: { uploadIcon: "Invisible", uploadClass: "Invisible" }
        });

    }

    this.CreateAlbum = function () {
        UploadSongs();
    }

    this.UpdateAlbumCover = function () {

        var input = $('#cover')[0];
        var ext = input.files[0]['name'].substring(input.files[0]['name'].lastIndexOf('.') + 1).toLowerCase();
        if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#albumCover').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        } else {
            $('#albumCover').attr('src', '/Content/Uploads/DefaultAlbumAv.png');
            $('#cover').val("");
        }
    }

    var UploadSongs = function () {

        var popupDiv = $(document.createElement('div'))
	                                        .attr("class", 'modal fade bs-example-modal-lg')
                                            .attr("id", "myModal");
        popupDiv.after().html('<div class="modal-dialog modal-lg"><div id="popUpContent" class="modal-content">xyz</div></div>');
        popupDiv.appendTo('#BodyContent');
        $('#myModal').modal('show');

        model = new FormData($('#AddAlbumForm')[0]);

        var Songs = [];

        var songsInputs = $('#songInput')[0].files;
        songLength = songsInputs.length;

        for (var i = 0, song; song = songsInputs[i]; i++) {          

            var fd = new FormData();
            fd.append('oauth_token', sessionStorage["scToken"]);
            fd.append('format', 'json');
            fd.append('track[title]', song.name);
            fd.append('track[asset_data]', song);

            $.ajax({
                url: 'https://api.soundcloud.com/tracks',
                type: 'POST',
                data: fd,
                async: true,
                processData: false,
                contentType: false,
                xhr: function () {
                    var xhr = $.ajaxSettings.xhr();
                    xhr.upload.onprogress = function (e) {
                        if (e.lengthComputable) {
                            var percent = Math.floor((e.loaded / e.total) * 100);

                            $('#popUpContent').append(percent);
                            console.log(percent + '% uploaded');
                        }
                    };
                    return xhr;
                }
            }).done(function (e) {
                console.log('Upload Complete!');
                console.dir(e); // This is the JSON object of the resulting track

                model.append('SongsNames', e.title);
                model.append('SongsUrlAddresses', e.stream_url);

                songCounter++;

                if (songCounter === songLength)
                {
                    SendForm();
                }
            });           
        }
    }

    var SendForm = function () {
        $.ajax({
            type: "POST",
            dataType: "json",
            cache: false,
            processData: false,
            contentType: false,
            url: "AddAlbum",
            data: model

        })
    }
}

$(document).ready(function () {
    var albumScope = new AddAlbumScope();
    albumScope.Init();
})

//RemoveSong = function (btn) {
//    var BtnId = $(btn).attr('id');
//    $("#newSongDiv" + BtnId).remove();
//    $("span#" + BtnId).remove();
//}