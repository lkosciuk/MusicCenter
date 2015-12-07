    function UserScope() {

        this.Init = function () {
            $('#avatar').change(this.UpdateUsersAvatar);
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
    }

$(document).ready((function() {
    var UScope = new UserScope();
    UScope.Init();

}));
