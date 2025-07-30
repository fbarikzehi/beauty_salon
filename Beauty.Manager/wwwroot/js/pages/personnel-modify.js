let tab_name = 'profile';
const save_btn_texts = {
    profile: 'ذخیره تغییرات پروفایل',
    account: 'ذخیره تغییرات حساب کاربری',
    services: 'ذخیره تغییرات خدمات',
    financial: 'ذخیره تغییرات تنظیمات مالی',
    workingTime: 'ذخیره تغییرات تنظیمات ساعات کاری',
}
const empty_id = '00000000-0000-0000-0000-000000000000';

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
    fetch_tab('Profile', 'Beauty_profile');
    save_btn_text_change(save_btn_texts.profile);

    $("#intro_this_page").click(function () {
        var intro = introJs();
        intro.setOptions({
            'nextLabel': 'بعد',
            'prevLabel': 'قبل',
            'skipLabel': 'خروج',
            'doneLabel': 'اتمام',
            steps: [
                {
                    element: '#intro_1',
                    intro: "اطلاعات پروفایل را  کامل کنید",
                    position: 'right'
                },
                {
                    element: '#required_sample',
                    intro: "فیلدهای ستاره داره اجباری می باشد",
                    position: 'right'
                },
                {
                    element: '#contact_count',
                    intro: "در صورتی که به اطلاعات تماس بیشتر نیاز داشتید اینجا کلیک کنید",
                    position: 'right'
                },
                {
                    element: '#save_btn',
                    intro: "بعد از تکمیل یا تغییر اطلاعات پروفایل آنرا ذخیره کنید",
                    position: 'right'
                },
                {
                    element: '#Beauty_subheader_total',
                    intro: "در صورتی که اطلاعات پروفایل پرسنل با موفقیت ثبت شود کد پرسنل نمایش داده می شود",
                    position: 'right'
                },
                {
                    element: '#intro_3',
                    intro: "اطلاعات حساب کاربری را کامل کنید",
                    position: 'right'
                },
                {
                    element: '#save_btn',
                    intro: "بعد از تکمیل یا تغییر اطلاعات حساب کاربری آنرا ذخیره کنید",
                    position: 'right'
                },
                {
                    element: '#intro_4',
                    intro: "خدماتی را که پرسنل انجام میدهد انتخاب کنید",
                    position: 'right'
                },
                {
                    element: '#save_btn',
                    intro: "بعد از تکمیل یا تغییر خدمات پرسنل آنرا ذخیره کنید",
                    position: 'right'
                },
                {
                    element: '#intro_5',
                    intro: "تنظیمات مالی را  کامل کنید",
                    position: 'right'
                },
                {
                    element: '#save_btn',
                    intro: "بعد از تکمیل یا تغییر تنظیمات مالی آنرا ذخیره کنید",
                    position: 'right'
                },
                {
                    element: '#intro_6',
                    intro: "تنظیمات ساعات کاری را  کامل کنید",
                    position: 'right'
                },
                {
                    element: '#save_btn',
                    intro: "بعد از تکمیل یا تغییر تنظیمات ساعات کاری آنرا ذخیره کنید",
                    position: 'right'
                },
            ]
        });

        intro.start();
    });

    $(".beauty-repeater").each(function () {
        $(this).repeater({
            show: function () {
                $(this).slideDown()
            },
            isFirstItemUndeletable: !0
        })
    });

    $("#save_btn").click(function (e) {
        let save_type = $("#save_tab").val();
        switch (save_type) {
            case 'Profile':
                submit_profile();
                break;
            case 'Account':
                submit_account();
                break;
            case 'Services':
                submit_services();
                break;
            case 'Financial':
                submit_financial();
                break;
            //case 'Attendence':
            //    submit_attendence();
            //    break;
            default:
                break;
        }
    });

});

function save_btn_text_change(text) {
    $("#save_btn").text(text)
}

function selected_services_changed(service_id, el) {
    $("#selected_service_check_" + service_id).val($(el)[0].checked)
    if (!$(el)[0].checked) {
        $("#select__all").prop('checked', false);
    }
}

function select_all_sevices(el) {
    let services = $("input[id^=service___]");

    for (var i = 0; i < services.length; i++) {
        $(services[i]).prop('checked', $(el)[0].checked);
        $("#selected_service_check_" + $(services[i]).data('id')).val($(el)[0].checked)
    }
}

function fetch_tab(currentView, wrapper) {
    let tabs = $(".tab-pane");
    for (var i = 0; i < tabs.length; i++) {
        $(tabs[i]).empty();
    }
    let url = '/Personnel/GetProfile';
    let pId = $("#p_id").val();

    switch (currentView) {
        case 'Profile':
            url = '/Personnel/GetProfile?pId=' + pId;
            tab_name = 'profile';
            save_btn_text_change(save_btn_texts.profile);
            break;
        case 'Account':
            if (pId === empty_id) {
                fetch_tab('Profile', 'Beauty_profile');
                change_active_tab('profile');
                toastr.error("ابتدا اطلاعات پروفایل را کامل و ثبت کنید");
                save_btn_text_change(save_btn_texts.profile);
                break;
            }
            url = '/Personnel/GetAccount?pId=' + pId;
            tab_name = 'account';
            save_btn_text_change(save_btn_texts.account);
            break;
        case 'Services':
            if (pId === empty_id) {
                fetch_tab('Profile', 'Beauty_profile');
                change_active_tab('profile');
                toastr.error("ابتدا اطلاعات پروفایل را کامل و ثبت کنید");
                save_btn_text_change(save_btn_texts.profile);
                break;
            }
            url = '/Personnel/GetServices?pId=' + pId;
            tab_name = 'services';
            save_btn_text_change(save_btn_texts.services);
            break;
        case 'Financial':
            if (pId === empty_id) {
                fetch_tab('Profile', 'Beauty_profile');
                change_active_tab('profile');
                toastr.error("ابتدا اطلاعات پروفایل را کامل و ثبت کنید");
                save_btn_text_change(save_btn_texts.profile);
                break;
            }
            url = '/Personnel/GetFinancial?pId=' + pId;
            tab_name = 'financial';
            save_btn_text_change(save_btn_texts.financial);
            break;
        case 'WorkingTime':
            if (pId === empty_id) {
                fetch_tab('Profile', 'Beauty_profile');
                change_active_tab('profile');
                toastr.error("ابتدا اطلاعات پروفایل را کامل و ثبت کنید");
                save_btn_text_change(save_btn_texts.profile);
                break;
            }
            url = '/Personnel/GetWorkingTime?pId=' + pId;
            tab_name = 'workingTime';
            save_btn_text_change(save_btn_texts.workingTime);
            break;
        default:
            break;
    }
    $.get(url, function (view) {
        $("#" + wrapper).empty().append(view);
        $("#save_tab").val(currentView);
        change_active_tab(tab_name);

        if (currentView === 'Financial') {
            let cooperation_type = $("[name=CooperationType]:checked").val();
            if (cooperation_type === 'Salary') {
                cooperationType_changed(0);
            } else {
                cooperationType_changed(1);
            }
        }
        else if (currentView === 'Profile') {

        }
    })
}

function change_active_tab(name) {
    var tabs = $(".personnel_tab");
    for (var i = 0; i < tabs.length; i++)
        $(tabs[i]).removeClass('active');

    console.log(name);
    $('a[href^="#Beauty_' + name + '"]').addClass('active');
}

function add_contact(el) {
    let index = $(el).val();
    BeautyTemplateBuilder.build('contact_template', {
        index: index
    }, 'contacts_wrapper', false);
    index++;
    $(el).val(index);
}

function avatar_change(e) {
    src = URL.createObjectURL($(e)[0].files[0]);
    document.getElementById("avatar_holder").style.backgroundImage = 'url(' + src + ')';
}

function submit_profile() {
    //console.log($("#profile_frm").validate());
    //if (!$("#profile_frm").valid()) {
    //    console.log($("#profile_frm").resetForm());;

    //    return false;
    //}

    let formdata = new FormData();
    let personnel_id = $("#Id").val();
    formdata.append('id', personnel_id);
    formdata.append('fileAvatar', $("#avatar_file")[0].files[0]);
    formdata.append('name', $("#Name").val());
    formdata.append('lastName', $("#LastName").val());
    formdata.append('birthdate', $("#Birthdate").val());
    formdata.append('jobStart', $("#JobStart").val());
    formdata.append('jobEnd', $("#JobEnd").val());
    formdata.append('address', $("#Address").val());

    let contacts = $("input[id^=contacts_index_]");
    for (var i = 0; i < contacts.length; i++) {
        let index = $(contacts[i]).val();
        formdata.append('contacts[' + i + '].Id', $("#Contacts_" + index + "__Id").val());
        formdata.append('contacts[' + i + '].type', $("#Contacts_" + index + "__Type").val());
        formdata.append('contacts[' + i + '].value', $("#Contacts_" + index + "__Value").val());
        formdata.append('contacts[' + i + '].isActive', $("#Contacts_" + index + "__IsActive:checked").val());
    }

    formdata.append('__RequestVerificationToken', $("[name=__RequestVerificationToken]").val());

    BeautyApp.progress($("#save_btn"));
    var form = $("#profile_frm")[0];
    $.ajax({
        url: form.action,
        data: formdata,
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) { 
            BeautyApp.unprogress($("#save_btn")),
                function (response) {
                    if (response.isSuccess) {
                        console.log(form.action);
                        if (response.id !== '00000000-0000-0000-0000-000000000000' && $("#p_id").val() === "00000000-0000-0000-0000-000000000000") {
                            $("#personnel_code").html(response.code);
                            $("#Id").val(response.id);
                            $("#p_id").val(response.id);
                            $("#profile_frm").prop('action', '/Manager/Personnel/UpdateProfile')
                        }
                        toastr.success(response.message);
                    } else {
                        toastr.error(response.message);
                    }
                }(response);
        }
    });
}

function delete_contact(id, index) {
    BeautyApp.progress($(".delete_btn_" + index));
    $.ajax({
        url: '/personnel/DeleteProfileContact',
        data: { id: id },
        type: 'POST',
        success: function (response) {
            BeautyApp.unprogress($(".delete_btn_" + index)),
                function (response) {
                    if (response.isSuccess) {
                        toastr.success(response.message);
                        $("#contact_item_" + index).remove();
                        $("#contacts_index_" + index).remove();
                        let contact_count = $("#contact_count").val();
                        $("#contact_count").val(contact_count - 1);
                        if (contact_count - 1 === 0) {
                            BeautyTemplateBuilder.build('contact_template', {
                                index: 0
                            }, 'contacts_wrapper', false);
                        }

                    } else {
                        toastr.error(response.message);
                    }
                }(response);
        }
    });
}

function submit_account() {
    //if (!$("#account_frm").valid()) return false;

    BeautyApp.progress($("#save_btn"));
    var form_action = $("#account_frm")[0].action;
    var form = $("#account_frm");
    $.ajax({
        url: form_action,
        data: form.serialize(),
        type: 'POST',
        success: function (response) {
            BeautyApp.unprogress($("#save_btn")),
                function (response) {
                    if (response.isSuccess) {
                        if (response.id !== '00000000-0000-0000-0000-000000000000' && $("#UserId").val() === "00000000-0000-0000-0000-000000000000") {
                            $("#UserId").val(response.id);
                            $("#account_frm").prop('action', '/Manager/Personnel/UpdateAccount')
                        }
                        toastr.success(response.message);
                    } else {
                        toastr.error(response.message);
                    }
                }(response);
        }
    });
}

function submit_services() {
    //if (!$("#services_frm").valid()) return false;

    BeautyApp.progress($("#save_btn"));
    var form_action = $("#services_frm")[0].action;
    var form = $("#services_frm");
    $.ajax({
        url: form_action,
        data: form.serialize(),
        type: 'POST',
        success: function (response) {
            BeautyApp.unprogress($("#save_btn")),
                function (response) {
                    if (response.isSuccess) {
                        toastr.success(response.message);
                    } else {
                        toastr.error(response.message);
                    }
                }(response);
        }
    });
}

function cooperationType_changed(type) {
    if (type === 1) {
        $("#service_salary_wrapper").empty();
        $("#service_percent_wrapper").empty();
        $.get('/Personnel/GetServicePercentageData', { pId: $("#PersonnelId").val() }, function (data) {
            if (data.length === 0)
                $("#service_percent_wrapper").append('<div class="alert alert-warning" role="alert"><div class="alert-text"> ابتدا سرویس های پرسنل را از خدمات انتخاب کنید </div ></div>')
            else {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    BeautyTemplateBuilder.build('service_percent_template', {
                        PersonnelServiceId: data[i].personnelServiceId,
                        ServiceTitle: data[i].serviceTitle,
                        PersonnelServicePercentage: data[i].percentage,
                        index: i
                    }, 'service_percent_wrapper', false);
                }
            }
        });

    } else {
        $("#service_salary_wrapper").empty();
        $("#service_percent_wrapper").empty();
        $.get('/Personnel/GetSalary', { pId: $("#PersonnelId").val() }, function (data) {
            BeautyTemplateBuilder.build('salary_template', { value: data }, 'service_salary_wrapper', true);
        });
    }
}

function salary_inputmask(el) {
    $(el).inputmask("9,999,999", {
        numericInput: !0
    })
}

function submit_financial() {
    //if (!$("#financial_frm").valid()) return false;

    BeautyApp.progress($("#save_btn"));
    var form_action = $("#financial_frm")[0].action;
    var form = $("#financial_frm");
    $.ajax({
        url: form_action,
        data: form.serialize(),
        type: 'POST',
        success: function (response) {
            BeautyApp.unprogress($("#save_btn")),
                function (response) {
                    if (response.isSuccess) {
                        toastr.success(response.message);
                    } else {
                        toastr.error(response.message);
                    }
                }(response);
        }
    });
}

function show_hide_switch(el) {
    if ($(el)[0].checked) {
    }
}
