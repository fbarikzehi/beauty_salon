var empty_guid = '00000000-0000-0000-0000-000000000000';
var save_type = 'GetProfile';


$(function () {


    fetch_tab('GetProfile', $("#profile_tab"));

    $(".choose_tab").click(function () {
        var tab_name = $(this).data('name');
        save_type = tab_name;
        fetch_tab(tab_name, this);
    });

    $("#save_btn").click(function () {
        let = $("#save_tab").val();
        switch (save_type) {
            case 'GetProfile':
                submit_profile(this);
                break;
            case 'GetAccount':
                submit_account(this);
                break;
            case 'GetServices':
                toastr.success('خدمات پرسنل با موفقیت ثبت شد');
                break;
            case 'GetFinancial':
                submit_financial(this);
                break;
            case 'GetWorkingTime':
                submit_workingTime();
                break;
            default:
                break;
        }
    });

});

function fetch_tab(tabname, tab) {
    let pId = $("#personnelId").val();
    if (tabname !== 'GetProfile' && pId === empty_guid) {
        toastr.error('ابتدا اطلاعات پرفایل را کامل کنید');
        return false;
    }

    let tabs = $(".nav>.nav-item>button.active");
    for (var i = 0; i < tabs.length; i++) {
        $(tabs[i]).removeClass('active');
    }
    $(tab).addClass('active');

    let url = '/Personnel/' + tabname;
    $.get(url, { pId: pId }, function (view) {
        $("#tab_content").empty().append(view);

    });
}


//Profile
function add_contact(el) {
    let index = $(el).val();

    var data = {
        index: index
    };

    let template = $("#contact_template").html();
    let html = Mustache.render(template, data);
    $("#contacts_wrapper").append(html);

    index++;
    $(el).val(index);
}

function delete_contact(id, index) {
    $.ajax({
        url: '/personnel/DeleteContact',
        data: { id: id },
        type: 'POST',
        success: function (response) {
            if (response.isSuccess) {
                toastr.success(response.message);
                $("#contact_item_" + index).remove();
                $("#contacts_index_" + index).remove();
                let contact_count = $("#contact_count").val();
                $("#contact_count").val(contact_count - 1);
                if (contact_count - 1 === 0) {
                    let template = $("#contact_template").html();
                    let html = Mustache.render(template, {
                        index: 0
                    });
                    $("#contacts_wrapper").append(html);
                }

            } else {
                toastr.error(response.message);
            }
        }
    });
}

function choose_avatar() {
    $("#avatar_file").click();
}

function change_avatar(e) {
    src = URL.createObjectURL($(e)[0].files[0]);
    document.getElementById("avatar_holder").src = src;
}

function delete_avatar() {
    document.getElementById("avatar_holder").src = '/images/avatar-placeholder.png';
}

function submit_profile(btn) {

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

    var l = Ladda.create(btn);
    l.start();
    var form = $("#profile_frm")[0];
    $.ajax({
        url: form.action,
        data: formdata,
        type: 'POST',
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.isSuccess) {
                if (response.id !== empty_guid && $("#Id").val() === empty_guid) {
                    $("#personnel_code").html(response.code);
                    $("#Id").val(response.id);
                    $("#personnelId").val(response.id);
                    $("#profile_frm").prop('action', '/Manager/Personnel/UpdateProfile');
                }
                toastr.success(response.message);
            } else {
                toastr.error(response.message);
            }
            l.stop();
        }
    });
}

//Account
function show_hide_switch(el) {
    if ($(el)[0].checked) {
        $("#Password").prop('type', 'text');
        $("#ConfirmPassword").prop('type', 'text');
    } else {
        $("#Password").prop('type', 'password');
        $("#ConfirmPassword").prop('type', 'password');
    }
}

function submit_account(btn) {
    var l = Ladda.create(btn);
    l.start();
    var form_action = $("#account_frm")[0].action;
    var form = $("#account_frm");
    $.ajax({
        url: form_action,
        data: form.serialize(),
        type: 'POST',
        success: function (response) {
            if (response.isSuccess) {
                if (response.id !== empty_guid && $("#UserId").val() === empty_guid) {
                    $("#UserId").val(response.id);
                    $("#account_frm").prop('action', '/Manager/Personnel/UpdateAccount')
                }
                toastr.success(response.message);
            } else {
                toastr.error(response.message);
            }
            l.stop();
        }
    });
}

//Services
function search_service(el) {

    var search_text = $(el).val();
    if (search_text !== '') {
        $.get('/Service/SearchAllByName', { val: search_text }, function (response) {
            console.log(response);
            $("#search_select").css('display', '').empty();
            if (response.data.length > 0) {
                for (var i = 0; i < response.data.length; i++) {
                    $("#search_select").append('<li onclick="choose_service(\'' + response.data[i].title + '\',\'' + response.data[i].id + '\')"><i class="feather icon-plus-circle text-primary" style="font-size:18px;font-weight:bolder;margin-left: 2%;"></i>' + response.data[i].title + '</li>');
                }
            } else {
                $("#search_select").empty().append('<li style="text-align: right;padding: 11px 2px;"><i class="feather icon-info text-danger" style="font-size:18px;font-weight:bolder;"></i> خدمت یافت نشد.خدمت را به لیست خدمات اضافه کنید <a href="/Service/Index" target="_blank">کلیک کنید</a> </li>');
            }

        })
    } else {
        $("#search_select").empty().css('display', '');
    }
}

function choose_service(title, id) {
    $.ajax({
        url: '/Personnel/AddService',
        data: { PersonnelId: $("#personnelId").val(), ServiceId: id },
        type: 'POST',
        success: function (response) {
            if (response.isSuccess) {
                $("#selected_services").append(`<div class="col-md-3 col-lg-2 col-sm-12"  id="service_card_` + id + `">
            <div class="card" style="background: aliceblue; margin-bottom: 1.2rem; padding-bottom: 1rem;">
                <div class="card-header">
                    <p class="card-title" style="font-size: 12px;">`+ title + `</p>
                    <a class="heading-elements-toggle"><i class="fa fa-ellipsis-v font-medium-3"></i></a>
                    <div class="heading-elements" style="top: 12px;left: 4px;">
                        <ul class="list-inline mb-0">
                            <li style=" top: 11px; left: 11px;">
                             <button onclick="delete_service('`+ id + `')" type="button" class="btn btn-icon btn-icon rounded-circle btn-warning mr-1 mb-1 waves-effect waves-light">
                             <i class="feather icon-trash-2"></i>
                             </button></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>`);

                $("#search_select").css('display', 'none').empty();
                toastr.success(response.message);
            } else {
                toastr.error(response.message);
            }
        }
    });


}

function delete_service(id) {
    $.ajax({
        url: '/Personnel/DeleteService',
        data: { PersonnelId: $("#personnelId").val(), ServiceId: id },
        type: 'POST',
        success: function (response) {
            if (response.isSuccess) {
                $("#service_card_" + id).remove();
                toastr.success(response.message);
            } else {
                toastr.error(response.message);
            }
            l.stop();
        }
    });
}

function update_line(el, personnelId, lineId) {
    $.post('/Personnel/UpdateLine', {
        lineId: lineId,
        personnelId: personnelId,
        select: el.checked
    }, function (response) {
        if (!response.isSuccess) {
            $(el).prop('checked', false);
        }
    });
}
//Financial
function cooperationType_changed(type) {
    $("#service_salary_wrapper").empty();
    $("#service_percent_wrapper").empty();
    $("#service_salary_percent_wrapper").empty();

    if (type === 1) {
        $.get('/Personnel/GetServicePercentageData', { pId: $("#personnelId").val() }, function (data) {
            if (data.length === 0)
                $("#service_percent_wrapper").append('<div class="alert alert-warning" role="alert"><div class="alert-text"> ابتدا سرویس های پرسنل را از خدمات انتخاب کنید </div ></div>')
            else {
                var data_service_salary = {};
                for (var i = 0; i < data.length; i++) {

                    data_service_salary = {
                        PersonnelServiceId: data[i].personnelServiceId,
                        ServiceTitle: data[i].serviceTitle,
                        PersonnelServicePercentage: data[i].percentage,
                        index: i
                    };

                    let template = $("#service_percent_template").html();
                    let html = Mustache.render(template, data_service_salary);
                    $("#service_percent_wrapper").append(html);
                }
            }
        });
    }
    else if (type === 0) {
        $.get('/Personnel/GetSalary', { pId: $("#personnelId").val() }, function (data) {
            let template = $("#salary_template").html();
            let html = Mustache.render(template, { value: data });
            $("#service_salary_wrapper").empty().append(html);
        });
    }
    else {
        var data_obj = {
            salary: null,
            servicePercentages: null
        };
        $.get('/Personnel/GetSalary', { pId: $("#personnelId").val() }, function (data) {
            data_obj.salary = data;

            $.get('/Personnel/GetServicePercentageData', { pId: $("#personnelId").val() }, function (data) {
                if (data.length === 0)
                    $("#service_percent_wrapper").append('<div class="alert alert-warning" role="alert"><div class="alert-text"> ابتدا سرویس های پرسنل را از خدمات انتخاب کنید </div ></div>')
                else {
                    var data_service_salary =[];
                    for (var i = 0; i < data.length; i++) {
                        data_service_salary.push({
                            PersonnelServiceId: data[i].personnelServiceId,
                            ServiceTitle: data[i].serviceTitle,
                            PersonnelServicePercentage: data[i].percentage,
                            index: i
                        });
                    }
                    data_obj.servicePercentages = data_service_salary;
                }

                console.log(data_obj);

                let template = $("#salary_percent_template").html();
                let html = Mustache.render(template, data_obj);
                $("#service_salary_percent_wrapper").empty().append(html);
            });

        });
    }
}

function salary_inputmask(el) {
    $(el).inputmask("9,999,999", {
        numericInput: !0
    })
}

function submit_financial(btn) {

    var l = Ladda.create(btn);
    l.start();
    var form_action = $("#financial_frm")[0].action;
    var form = $("#financial_frm");
    $.ajax({
        url: form_action,
        data: form.serialize(),
        type: 'POST',
        success: function (response) {
            if (response.isSuccess) {
                toastr.success(response.message);
            } else {
                toastr.error(response.message);
            }
            l.stop();
        }
    });
}