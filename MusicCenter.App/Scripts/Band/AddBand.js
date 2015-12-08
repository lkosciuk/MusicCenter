
AddBandScope = function () {

    this.Init = function () {
        this.SetupCreationDatePicker();
        this.SetupResolveDatePicker();
        $('#avatar').change(this.UpdateBandAvatar);
    }

    this.SetupCreationDatePicker = function () {

        $.getScript("/Scripts/bootstrap.js", function () {
            $('#createDate').datepicker();
        });
        
    };

    this.SetupResolveDatePicker = function () {
        $('#resolveDate').datepicker();
    };

    this.UpdateBandAvatar = function () {

        var input = $('#avatar')[0];
        var ext = input.files[0]['name'].substring(input.files[0]['name'].lastIndexOf('.') + 1).toLowerCase();
        if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#bandAvatar').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        } else {
            $('#bandAvatar').attr('src', '/Content/Uploads/DefaultBandAv.png');
            $('#avatar').val("");
        }
    }
}
          
$(document).ready(function () {
    var addBandScope = new AddBandScope();
    addBandScope.Init();
});
