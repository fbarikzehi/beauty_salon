"use strict";

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-left",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "100000",
    "timeOut": "3000",
    "extendedTimeOut": "3500",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

var BeautyLoginPage = function () {
    var run = function () {
        BeautyClearLocalStorage.clear(['active_menu']);

        $("#Beauty_login_submit").click(function (e) {
            e.preventDefault();
            var submit_btn = $(this),
                form = $("#Beauty_login_form");
            form.validate({
                rules: {
                    Username: {
                        required: !0,
                        minlength: 2
                    },
                    Password: {
                        required: !0,
                        minlength: 10
                    }
                },
                messages: {
                    Username: {
                        required: "We need your email address to contact you",
                        minlength: jQuery.validator.format("At least {0} characters required!")
                    },
                    Password: {
                        required: "We need your email address to contact you",
                        minlength: jQuery.validator.format("At least {0} characters required!")
                    }
                }
            }), form.valid() && (BeautyApp.progress(submit_btn[0]), form.ajaxSubmit({
                url: "/account/login",
                data: form.serialize(),
                success: function (response) {
                    BeautyApp.unprogress(submit_btn[0]),
                        function (from, response) {
                            //var alert = BeautyUtil.getAlert(response.alertType, response.message);
                            //from.find(".alert").remove(),
                            //    alert.prependTo(from),
                            //    BeautyUtil.animateClass(alert[0], "fadeIn animated");

                            if (response.isSuccess) {
                                toastr.success(response.message + ' لطفا منتظر باشید');
                                setTimeout(function () {
                                    location.assign(response.redirectUrl);
                                }, 3000);
                            }
                            else
                                toastr.error(response.message);
                        }(form, response);
                },
                error: function (e) {
                    console.error(e);
                    toastr.error('خطایی رخ داد با مدیریت تماس بگیرید');
                }
            }))
        })
    };
    return {
        init: function () {
            run()
        }
    }
}();
jQuery(document).ready(function () {
    BeautyLoginPage.init()
});