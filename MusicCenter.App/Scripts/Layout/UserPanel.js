
    $(document).ready(function () {
        $('#LogOutUrl').click(function() {
            localStorage.removeItem('scToken');
        });
    });
