let tab_name = 'salon_info';

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
persianDate.toLocale('fa');
$(function () {
    fetch_tab('salon_info');

    $.get('/shared/getInfo', function (data) {
        localStorage.setItem('salon_logo', data.logo);
        localStorage.setItem('salon_title', data.title);

        $("#salon_logo").prop('src', data.logo)
        $("#salon_title").html(data.title);
    });

    $("#OpeningTime,#ClosingTime").inputmask("24:59", {
        numericInput: !0
    });
});

function fetch_tab(view_name) {

    let url = '/Salon/GetSalonInfo';
    let sId = $("#s_id").val();

    switch (view_name) {
        case 'salon_info':
            url = '/Salon/GetSalonInfo?sId=' + sId;
            tab_name = 'salon_info';
            break;
        case 'account':
            if (sId === empty_id) {
                fetch_tab('salon_info');
                change_active_tab('salon_info_tab');
                toastr.error("ابتدا اطلاعات سالن را کامل و ثبت کنید");
                break;
            }
            url = '/Salon/GetAccount?sId=' + sId;
            tab_name = 'account';
            break;
        case 'working_time':
            if (sId === empty_id) {
                fetch_tab('salon_info');
                change_active_tab('salon_info_tab');
                toastr.error("ابتدا اطلاعات سالن را کامل و ثبت کنید");
                break;
            }
            url = '/Salon/GetWorkingDateTimes?sId=' + sId;
            tab_name = 'working_time';
            break;
        case 'sms':
            break;
        case 'appointment':
            break;
        case 'personnel':
            break;
        default:
            break;
    }
    $.get(url, function (view) {
        $("#content_wrapper").empty().append(view);
        if (view_name === 'salon_info')
            $("#s_id").val($("#Id").val());
    })
}

function change_active_tab(name) {
    var tabs = $(".personnel_tab");
    for (var i = 0; i < tabs.length; i++)
        $(tabs[i]).removeClass('active');

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

function logo_change(e) {
    src = URL.createObjectURL($(e)[0].files[0]);
    document.getElementById("logo_holder").style.backgroundImage = 'url(' + src + ')';
}

function submit_salon_info() {

    let formdata = new FormData();
    formdata.append('Id', $("#Id").val());
    formdata.append('FileLogo', $("#FileLogo")[0].files[0]);
    formdata.append('Title', $("#Title").val());
    formdata.append('EstablishYear', $("#EstablishYear").val());
    formdata.append('About', $("#About").val());
    formdata.append('Address', $("#Address").val());
    formdata.append('OpeningTime', $("#OpeningTime").val());
    formdata.append('ClosingTime', $("#ClosingTime").val());

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
    var form = $("#saloninfo_frm")[0];
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
                        if (response.id !== '00000000-0000-0000-0000-000000000000' && $("#s_id").val() === "00000000-0000-0000-0000-000000000000") {
                            $("#Id").val(response.id);
                            $("#s_id").val(response.id);
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
    alert(id);
    $.ajax({
        url: '/Salon/DeleteContact',
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