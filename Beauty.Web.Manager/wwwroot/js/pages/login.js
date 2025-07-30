toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "2100",//
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

$(function () {
    $("#login_submit").click(function (e) {
        var l = Ladda.create(this);
        l.start();
        var form = $("#login_form");
        $.post({
            url: "/account/login",
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message, ' لطفا منتظر باشید')
                    setTimeout(function () {
                        location.assign(response.redirectUrl);
                    }, 2000);
                }
                else
                    toastr.error(response.message, 'ورود ناموفق');

                l.stop();
            },
            error: function (e) {
                console.error(e);
                toastr.error('خطایی رخ داد با مدیریت تماس بگیرید', 'ورود ناموفق');
                l.stop();
            }
        });
    })
});