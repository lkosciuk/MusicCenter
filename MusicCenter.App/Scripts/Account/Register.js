    function RegisterScope() {

        this.Init = function () {
            $('#avatar').change(this.UpdateUsersAvatar);
            $('#email').focusout(this.ValidateEmail);
        }

        this.UpdateUsersAvatar = function () {
            
            var input = $('#avatar')[0];
            var ext = input.files[0]['name'].substring(input.files[0]['name'].lastIndexOf('.') + 1).toLowerCase();
            if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#userAvatar').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            } else {
                $('#userAvatar').attr('src', '/Content/Uploads/DefaultUserAv.jpg');
                $('#avatar').val("");
            }
        }

        this.ValidateEmail = function () {

            var email = $('#email').val();

            $.ajax({
                url: '@Url.Action("IsEmailValid", "Account")',
                type: "POST",
                data: {email: email},
                success: function (data) {
                    if (data == "False") 
                    {
                        $('#EmailControls').addClass("has-error");
                        $('#EmailUnique').text('@MusicCenter.Common.Resources.Global.EmailUnique');
                    }
                    else
                    {
                        $('#EmailControls').removeClass("has-error");
                        $('#EmailUnique').text("");
                    }
                }
            })
        }
    }

$(document).ready((function() {
    var RegScope = new RegisterScope();
    RegScope.Init();

}));
