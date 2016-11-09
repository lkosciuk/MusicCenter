UserBandsScope = function () {

    this.Init = function () {
        this.SetupBandCreateButton();
    }

    this.SetupBandCreateButton = function () {
        if (localStorage['scToken'] != null) {

            $('input[name=BandLoginBtn]').each(function () {
                $(this).show();
            });

            $('#AddBandButton').show();
            $('#SoundCloundLogin').hide();
        } else {

            $('input[name=BandLoginBtn]').each(function () {
                $(this).hide();
            });

            $('#AddBandButton').hide();
            $('#SoundCloundLogin').show();
        }
    }
}

$(document).ready(function () {
    var scope = new UserBandsScope();
    scope.Init();
})