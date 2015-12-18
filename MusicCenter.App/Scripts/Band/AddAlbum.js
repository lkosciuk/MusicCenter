AddAlbumScope = function () {

    var songCounter = 0;

    this.Init = function () {
        this.SetupFileInput();
        this.SetupJQueryDatePicker();
        //this.UploadSong();
        $('#CreateAlbumBtn').click(this.CreateAlbum);
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

    //this.AddSongInput = function () {

    //    songCounter++;

    //    var newSongDiv = $(document.createElement('div'))
	//     .attr("id", 'newSongDiv' + songCounter);

    //    newSongDiv.after().html('<div class="col-sm-10"><input id=songInput' + songCounter + ' name="files" type="file" data-preview-file-type="text"></div><div class="col-sm-1"><span class="glyphicon glyphicon-remove" onclick="RemoveSong(this)" id=' + songCounter + '></span></div>');
    //    $("#songInput" + songCounter).fileinput({
    //        showUpload: false
    //    });
    //    newSongDiv.appendTo("#SinglesContainer");
    //}

    var UploadSongs = function () {

        var Songs = [];

        var songsInputs = $('#songInput')[0].files;

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
                async: false,
                processData: false,
                contentType: false,
                xhr: function () {
                    var xhr = $.ajaxSettings.xhr();
                    xhr.upload.onprogress = function (e) {
                        if (e.lengthComputable) {
                            var percent = Math.floor((e.loaded / e.total) * 100);
                            console.log(percent + '% uploaded');
                        }
                    };
                    return xhr;
                },
                error: function (e) {

                }
            }).done(function (e) {
                console.log('Upload Complete!');
                console.dir(e); // This is the JSON object of the resulting track

                var currentSong = {
                    BandName: "",
                    Name: e.title,
                    UrlAddress: e.stream_url
                }

                Songs.push(currentSong);
            });
        }

        //var Songs = JSON.stringify({ 'Songs': Songs });

        //var options = {
        //    data: { Songs: data }
        //}

        $('#AddAlbumForm').ajaxSubmit({
            data: Songs
        });
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