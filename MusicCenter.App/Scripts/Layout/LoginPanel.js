
    function SoundCloudScope() {

        this.Init = function () {
            this.SetupSoundCloud();
            $('#SoundCloudLogo').click(this.ConnectSoundCloud);
        }

        this.SetupSoundCloud = function () {
            var url = 'http://localhost:56536/Account/SoundCloudCallback';
            localStorage['scClientId'] = '6895e74306f08ac18acb7b703672ee62';

            SC.initialize({
                client_id: localStorage['scClientId'],
                client_secret: 'b9b5b82f716f3322149d1fa038573e0e',
                redirect_uri: url,
                access_token: localStorage['scToken'],
            });
        }

        this.ConnectSoundCloud = function () {
            // initiate authentication popup
            SC.connect(function () {
                // This gets the authenticated user's username
                localStorage['scToken'] = SC.accessToken();
                

                SC.get('/me', function (me) {
                    var url = $('#SoundCloudLogo').attr('soundCloundUrl');

                    $.post(url, me, function () {
                        window.location.reload();
                    });
                });
               
            });
        }
    }

$(document).ready((function () {
    var SCScope = new SoundCloudScope();
    SCScope.Init();
}));