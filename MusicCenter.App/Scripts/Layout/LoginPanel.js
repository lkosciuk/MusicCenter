
    function SoundCloudScope() {

        this.Init = function () {
            this.SetupSoundCloud();
            $('#SoundCloudLogo').click(this.ConnectSoundCloud);
        }

        this.SetupSoundCloud = function () {
            localStorage['scRedirectUri'] = 'http://localhost:56536/Account/SoundCloudCallback';
            localStorage['scClientId'] = '6895e74306f08ac18acb7b703672ee62';

            SC.initialize({
                client_id: localStorage['scClientId'],
                redirect_uri: localStorage['scRedirectUri']
            });
        }

        this.ConnectSoundCloud = function () {
            // initiate authentication popup
            SC.connect().then(function () {
                return SC.get('/me');
            }).then(function (me) {
                var data;
                
                SC.post('/oauth2/token', {
                    client_id: localStorage['scClientId'],
                    client_secret: 'b9b5b82f716f3322149d1fa038573e0e',
                    redirect_uri: localStorage['scRedirectUri'],
                    grant_type: 'authorization_code',
                    code: localStorage['scCode']
                }).then(function(accessData) {
                    localStorage['scToken'] = accessData.access_token;

                    var url = $('#SoundCloudLogo').attr('soundCloundUrl');

                    $.post(url, me, function () {
                        window.location.reload();
                    });
                }).catch(function (error) {
                    alert('There was an error ' + error.message);
                });
            });
        }
    }

$(document).ready((function () {
    var SCScope = new SoundCloudScope();
    SCScope.Init();
}));