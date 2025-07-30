toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "100000",
    "timeOut": "5000",
    "extendedTimeOut": "3500",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

$(function () {

    BeautyTemplateBuilder.build('service_template', {
        index: 0
    }, 'services_wrapper', false);

    var index = 1;
    $("#add_service").click(function () {
        BeautyTemplateBuilder.build('service_template', {
            index: index
        }, 'services_wrapper', false);
        index++;
    });

    $("#Beauty_services_submit").click(function (e) {
        var submit_btn = $(this),
            form = $("#service_form");

        BeautyApp.progress(submit_btn[0]);
        $.ajax({
            type: 'POST',
            url: "/service/createRange",
            data: form.serialize(),
            success: function (response) {
                BeautyApp.unprogress(submit_btn[0]),
                    function (from, response) {
                        if (response.isSuccess) {
                            toastr.success(response.message);

                            BeautyTemplateBuilder.build('service_template', {
                                index: 0
                            }, 'services_wrapper', true);
                        }
                        else {
                            toastr.error(response.message);
                        }
                    }(form, response);
            }
        });
    })
});