
    function SoundCloudScope() {

        this.Init = function () {
            this.SetupSoundCloud();
            $('#SoundCloudLogo').click(this.ConnectSoundCloud);
        }

        this.SetupSoundCloud = function () {
            var url = 'http://localhost:56536/Account/SoundCloudCallback';
            SC.initialize({
                client_id: '6895e74306f08ac18acb7b703672ee62',
                client_secret: 'b9b5b82f716f3322149d1fa038573e0e',
                redirect_uri: url,
                access_token: sessionStorage['scToken'],
            });
        }

        this.ConnectSoundCloud = function () {
            // initiate authentication popup
            SC.connect(function () {
                // This gets the authenticated user's username
                sessionStorage['scToken'] = SC.accessToken();

                SC.get('/me', function (me) {
                    $.post('Account/SoundCloudConnect', me, function () {
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