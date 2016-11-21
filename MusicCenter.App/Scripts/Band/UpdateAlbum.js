UpdateAlbumScope = function()
{
    var songCounter = 0;
    var songLength = 0;

    var model;
    var songsToRemove;
    var uploads = [];

    this.Init = function () {
        SetupFileInput();
        SetupJQueryDatePicker();
        $('#UpdateAlbumBtn').click(this.UpdateAlbum);
        $('#cover').change(this.UpdateAlbumCover);
        $.each($("[name = RemoveSongBtn]"), function () {
            $(this).click(RemoveSlectedSong);
        })
    }

    var RemoveSlectedSong = function (e) {
        var songId = $(e.currentTarget).data("songid");

        if (songId)
        {
            songsToRemove.push(songId);
            $('div#' + songId).remove();
        }        
    }

    var SetupJQueryDatePicker = function () {

        $("#UpdateReleaseDate").datepicker({

            beforeShow: function () {
                setTimeout(function () {
                    $('.ui-datepicker').css('z-index', 99999999999999);
                }, 0);
            },
            dateFormat: 'dd-mm-yy'
        });
    }

    var SetupFileInput = function () {

        $("#UpdatesongInput").fileinput({
            uploadUrl: "error",
            showUpload: false,
            pluginLoading: true,
            fileActionSettings: { uploadIcon: "Invisible", uploadClass: "Invisible" }
        });

    }

    var ValidateSongs = function () {

        if ($('#UpdatesongInput')[0].files.length > 0 || $('iframe').length > 0) {
            $('#songInput_validate').empty();
            return true;
        }
        else {
            $('#songInput_validate').append("Songs can't be empty.");
            return false;
        }
    }

    this.UpdateAlbum = function () {
        if ($('#UpdateAlbumForm').valid() && ValidateSongs()) {
            model = new FormData($('#UpdateAlbumForm')[0]);

            if (songsToRemove !== undefined) {
                model.append('SongsToRemove', songsToRemove);
            }
            
            UploadSongs();
        }
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
        popupDiv.after().html('<div class="modal-dialog modal-lg"><div id="popUpContent" class="modal-content"></div></div>');
        popupDiv.appendTo('#BodyContent');
        $('#popUpContent').append('<b>Album upload progress:<b>' + '<div class="progress"><div id="progressBar" class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"></div></div>');    
        $('#myModal').on('hidden.bs.modal', function (e) {
            $('#myModal').remove();
        })

        var Songs = [];

        var songsInputs = $('#UpdatesongInput')[0].files;
        songLength = songsInputs.length;
        songCounter = 0;

        if (songLength > 0) {

            $('#myModal').modal('show');

            for (var i = 0, song; song = songsInputs[i]; i++) {

                var fd = new FormData();
                fd.append('oauth_token', localStorage["scToken"]);
                fd.append('format', 'json');
                fd.append('track[title]', song.name);
                fd.append('track[asset_data]', song);

                $.ajax({
                    url: 'https://api.soundcloud.com/tracks?client_id=' + localStorage['scClientId'],
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

                                $('#progressBar').attr("aria-valuenow", percent);
                                $('#progressBar').empty();
                                $('#progressBar').append(percent + '%');
                                $('#progressBar').css('width', percent + '%');
                                console.log(percent + '% uploaded');
                            }
                        };
                        return xhr;
                    }
                }).done(function (e) {
                    console.log('Upload Complete!');
                    console.dir(e); // This is the JSON object of the resulting track
                    $('#popUpContent').append('<b>' + e.title + '</b>' + '<b style="color:green">- Upload complete!</b></br>');

                    model.append('NewSongsNames', e.title);
                    model.append('NewSongsUrlAddresses', e.id);//e.uri);

                    songCounter++;

                    if (songCounter === songLength) {
                        $('#popUpContent').append('<input type="button" id="CloseUploadPopupBtn" onclick="CloseUploadPopup()" class="btn btn-info pull-right" value="Close"/> ');
                        SendForm();
                    }
                });
            }
        }
        else {
            SendForm();
        } 
    }

    var SendForm = function () {
        $.ajax({
            type: "POST",
            dataType: "json",
            cache: false,
            processData: false,
            contentType: false,
            url: "UpdateAlbum",
            data: model

        })
    }
}

$(document).ready(function () {
    var albumScope = new UpdateAlbumScope();
    albumScope.Init();
})

CloseUploadPopup = function () {
    $('#myModal').modal('hide');
}