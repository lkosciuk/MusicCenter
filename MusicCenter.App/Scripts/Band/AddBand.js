
AddBandScope = function () {

    var BandMemberCounter = 0;

    this.Init = function () {
        this.SetupJQueryDatePicker();
        $('#avatar').change(this.UpdateBandAvatar);
        $('#AddBandMemberBtn').click(this.AddBandMemberTextBox);
        $("#AddBandForm").submit(this.CreateBand);
    }

    this.SetupJQueryDatePicker = function () {
        
        $("#CreationDate").datepicker({
            
            beforeShow: function () {
                setTimeout(function () {
                    $('.ui-datepicker').css('z-index', 99999999999999);
                }, 0);
            },
            dateFormat: 'yy-mm-dd'
        });
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

    this.AddBandMemberTextBox = function () {
        
        BandMemberCounter++;

        var newTextBoxDiv = $(document.createElement('div'))
	     .attr("id", 'TextBoxDiv' + BandMemberCounter);

        newTextBoxDiv.after().html('<div class="row"><div class="col-sm-3"><input type="text" name="BandMembers" class="form-control input-sm" style="margin-bottom:20px" id="textbox' + BandMemberCounter + '" value="" ></div><div class="col-sm-1"><span class="glyphicon glyphicon-remove" onclick="RemoveMember(this)" id=' + BandMemberCounter + '></span></div></div>');

        newTextBoxDiv.appendTo("#BandMembers");
    }

    this.CreateBand = function () {
        $('#AddBandForm').ajaxForm();  
    }


}
          
$(document).ready(function () {
    var addBandScope = new AddBandScope();
    addBandScope.Init();
    
});

RemoveMember = function (btn)
{
    var BtnId = $(btn).attr('id');
    $("#TextBoxDiv" + BtnId).remove();
    $("span#" + BtnId).remove();
}
