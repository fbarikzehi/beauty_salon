const white_space = /\s/g, comma = /,/g;
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
    "timeOut": "5000",
    "extendedTimeOut": "3500",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};
var index = 1;


$(function () {
    $('#Date').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });
    $('#CreateDateTime').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });
    $('#Birthdate').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });

    $("#Time").inputmask("99:99", {
        numericInput: !0
    });
    $("#Mobile").inputmask("09999999999", {
        numericInput: !0
    });

    build_calendar();

    $("#save_customer").click(function (e) {

        let formdata = new FormData();
        formdata.append('AvatarFile', $("#avatar_file")[0].files[0]);
        formdata.append('Name', $("#Name").val());
        formdata.append('LastName', $("#LastName").val());
        formdata.append('Birthdate', $("#Birthdate").val());
        formdata.append('contacts[0].type', 0);
        formdata.append('contacts[0].value', $("#Mobile").val());
        formdata.append('contacts[0].isActive', true);

        formdata.append('__RequestVerificationToken', $("[name=__RequestVerificationToken]").val());

        BeautyApp.progress($("#save_customer"));
        var form = $("#customer_frm")[0];
        $.ajax({
            url: form.action,
            data: formdata,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (response) {
                BeautyApp.unprogress($("#save_customer")),
                    function (response) {
                        if (response.isSuccess) {
                            toastr.success(response.message);
                        } else {
                            toastr.error(response.message);
                        }
                    }(response);
            }
        });
    });

    BeautyTemplateBuilder.build('service_template', {
        index: 0,
        ServiceId: 0,
        ServiceTitle: "",
        DurationMinutes: "",
        Price: "",
        Prepayment: ""
    }, 'services_wrapper', false);

    $("#add_service").click(function () {
        BeautyTemplateBuilder.build('service_template', {
            index: index,
            ServiceId: 0,
            ServiceTitle: "",
            DurationMinutes: "",
            Price: "",
            Prepayment: ""
        }, 'services_wrapper', false);
        index++;
    });

    $(document).on('click', function () {
        $(".select-dropdown").addClass('hide-select-dropdown').empty();
    });

    $("#save_appointment").click(function (e) {

        BeautyApp.progress($("#save_appointment"));
        var appointment_frm = $("#appointment_frm");
        console.log(appointment_frm.serialize());
        $.ajax({
            url: '/Appointment/Create',
            data: appointment_frm.serialize(),
            type: 'POST',
            success: function (response) {
                BeautyApp.unprogress($("#save_appointment")),
                    function (response) {
                        if (response.isSuccess) {
                            toastr.success(response.message);
                            build_calendar();
                            reset_frm_appointment();
                        } else {
                            toastr.error(response.message);
                        }
                    }(response);
            }
        });
    });
});

function build_calendar() {
    var e = moment().startOf("day"),
        t = e.format("YYYY-MM"),
        i = e.clone().subtract(1, "day").format("YYYY-MM-DD"),
        n = e.format("YYYY-MM-DD"),
        r = e.clone().add(1, "day").format("YYYY-MM-DD"),
        o = document.getElementById("Beauty_calendar");
    $(o).empty();
    var calendar = new FullCalendar.Calendar(o, {
        plugins: ["interaction", "dayGrid", "timeGrid", "list"],
        locale: 'fa',
        isRTL: BeautyUtil.isRTL(),
        header: {
            left: "prev,next today",
            center: "title",
            right: "dayGridMonth,dayGridWeek,timeGridDay"
        },
        buttonIcons: {
            prev: 'chevron-right',
            next: 'chevron-left',
            prevYear: 'left-double-arrow',
            nextYear: 'right-double-arrow'
        },
        buttonText: {
            today: 'امروز',
            month: 'ماه',
            week: 'هفته',
            day: 'روز',
            list: 'لیست'
        },
        height: 800,
        contentHeight: 780,
        aspectRatio: 3,
        nowIndicator: !0,
        now: n + "T09:25:00",
        firstDay: 6,
        views: {
            dayGridMonth: {
                buttonText: "ماه"
            },
            dayGridWeek: {
                buttonText: "هفته"
            },
            timeGridDay: {
                buttonText: "روز"
            }
        },
        defaultView: "dayGridWeek",
        selectable: true,
        dateClick: function (info) {
            //console.log(info);
            //alert('Clicked on: ' + info.dateStr);
            //alert('Coordinates: ' + info.jsEvent.pageX + ',' + info.jsEvent.pageY);
            //alert('Current view: ' + info.view.type);
            // change the day's background color just for fun
            //info.dayEl.style.backgroundColor = 'red';
        },
        eventClick: function (info) {
            get_event_info(info.event._def.publicId);
            //alert('Event: ' + info.event.title);
            //alert('Coordinates: ' + info.jsEvent.pageX + ',' + info.jsEvent.pageY);
            //alert('View: ' + info.view.type);

            // change the border color just for fun
            //info.el.style.borderColor = 'red';
        },
        eventMouseEnter: function (info) {
            //console.log(info.event._def.publicId);
            //alert('Event: ' + info.event.title);
            //alert('Coordinates: ' + info.jsEvent.pageX + ',' + info.jsEvent.pageY);
            //alert('View: ' + info.view.type);

            // change the border color just for fun
            //info.el.style.borderColor = 'red';
        },
        eventMouseLeave: function (data, event, view) {
            //console.log(data);
            //console.log(info.event._def.publicId);
            //alert('Event: ' + info.event.title);
            //alert('Coordinates: ' + info.jsEvent.pageX + ',' + info.jsEvent.pageY);
            //alert('View: ' + info.view.type);

            // change the border color just for fun
            //info.el.style.borderColor = 'red';
        },
        defaultDate: n,
        editable: !0,
        eventLimit: !0,
        navLinks: !0,
        events: "/Appointment/GetEvents",
        eventRender: function (e) {
            var t = $(e.el);
            console.log(t);
            e.event.extendedProps &&
                e.event.extendedProps.description &&
                (t.hasClass("fc-day-grid-event") ?
                    (t.data("content", e.event.extendedProps.description),
                        t.data("placement", "top"), BeautyApp.initPopover(t)) :
                    t.hasClass("fc-time-grid-event") ?
                        t.find(".fc-title").append('<div class="fc-description">' + e.event.extendedProps.description + "</div>") :
                        0 !== t.find(".fc-list-item-title").lenght && t.find(".fc-list-item-title").append('<div class="fc-description">' + e.event.extendedProps.description + "</div>"))
        }
    }).render();
}
function get_event_info(app_id) {
    $.get('/Appointment/GetById', { appId: app_id }, function (app) {
        $("#Beauty_demo_panel_toggle").click();
        console.log(app);
        BeautyTemplateBuilder.build('app_info_template', app, 'Beauty_demo_panel', true);
    });
}
function header_btn(date) {
    persianDate.toLocale('fa');
    var gdate = new Date(JSON.parse(date).date);
    var pc_now = new persianDate(gdate).format();
    $("#Date").val(pc_now.split(' ')[0].replace('-', '/').replace('-', '/'));

    $("#Beauty_appointment_modal_btn").click();
}

function reset_frm_appointment() {
    $("#services_wrapper").empty();
    BeautyTemplateBuilder.build('service_template', {
        index: 0,
        ServiceId: 0,
        ServiceTitle: "",
        DurationMinutes: "",
        Price: "",
        Prepayment: ""
    }, 'services_wrapper', false);
    index = 1;
    $("#TotalPrice").html(0);
    $("#TotalPrepayment").html(0);
    $("#TotalPostpayment").html(0);

    $("#CustomerProfileId").val('');
    $("#CustomerProfileName").val('');
    $("#MemberCode").val('');
    $("#CustomerMobile").val('');
    $("#Date").val('');
    $("#Time").val('');
    $("#CustomerProfileName").val('');
    $("#CustomerProfileName").val('');

}

function avatar_change(e) {
    src = URL.createObjectURL($(e)[0].files[0]);
    document.getElementById("avatar_holder").style.backgroundImage = 'url(' + src + ')';
}

function service_dropselect(search_text, index) {
    if (search_text !== '') {
        $.get('/Service/FindAllBySearch', { val: search_text }, function (response) {
            console.log(response);
            $("#Services_" + index + "_search_item").removeClass('hide-select-dropdown').empty();
            if (response.entities.length > 0) {
                for (var i = 0; i < response.entities.length; i++) {
                    $("#Services_" + index + "_search_item").append('<li onclick="set_service_values(\'' + response.entities[i].durationMinutes + '\',\'' + response.entities[i].id + '\',\'' + response.entities[i].prepayment + '\',\'' + response.entities[i].price + '\',\'' + response.entities[i].title + '\',\'' + index + '\')">' + response.entities[i].title + '</li>');
                }
            } else {
                $("#Services_" + index + "__ServiceId").val('');
                $("#Services_" + index + "_search_item").append('<li style="text-align: right;padding: 11px 2px;">خدمتی یافت نشد ابتدا آنرا در خدمات اضافه کنید</li>');
            }

        })
    } else {
        $("#Services_" + index + "__ServiceId").val('');
        $("#Services_" + index + "_search_item").empty().removeClass('hide-select-dropdown');
    }
}

function set_service_values(durationMinutes, id, prepayment, price, title, index) {
    $("#Services_" + index + "__DurationMinutes").val(durationMinutes);
    $("#Services_" + index + "__ServiceId").val(id);
    $("#Services_" + index + "__Prepayment").val(prepayment);
    $("#Services_" + index + "__Price").val(price);
    $("#Services_" + index + "__ServiceTitle").val(title);

    $("#Services_" + index + "_search_item").addClass('hide-select-dropdown').empty();
    compute_prices();
}

function delete_service(current_index) {
    $("#Services_" + current_index).remove();

    let data_list = [];

    let services = $("div[id^=Services_]");
    for (var i = 0; i < services.length; i++) {
        let old_index = $(services[i]).data('index');
        if (old_index !== current_index)
            data_list.push({
                index: i,
                ServiceId: $("#Services_" + old_index + "__ServiceId").val(),
                ServiceTitle: $("#Services_" + old_index + "__ServiceTitle").val(),
                DurationMinutes: $("#Services_" + old_index + "__DurationMinutes").val(),
                Price: $("#Services_" + old_index + "__Price").val(),
                Prepayment: $("#Services_" + old_index + "__Prepayment").val(),
                PersonnelId: $("#Services_" + old_index + "__PersonnelId").find('option:selected').val(),
            });
    }
    $("#services_wrapper").empty();
    if (data_list.length > 0) {
        for (var i = 0; i < data_list.length; i++) {
            BeautyTemplateBuilder.build('service_template', data_list[i], 'services_wrapper', false);
        }
        index = data_list.length - 1;
    }
    else {
        BeautyTemplateBuilder.build('service_template', {
            index: 0,
            ServiceId: 0,
            ServiceTitle: "",
            DurationMinutes: "",
            Price: "",
            Prepayment: "",
            PersonnelId: 0
        }, 'services_wrapper', false);
        index = 1;
    }
    compute_prices();
}

function compute_prices() {

    var service_prices = $(".service_price");
    let total_price = 0;
    for (var i = 0; i < service_prices.length; i++) {
        if ($(service_prices[i]).val() !== '')
            total_price += parseInt($(service_prices[i]).val().replace(comma, ''));
    }
    $("#TotalPrice").html(Humanize.intComma(total_price));

    var service_prepayment = $(".service_prepayment");
    let total_prepayment = 0;
    for (var i = 0; i < service_prepayment.length; i++) {
        if ($(service_prepayment[i]).val() !== '')
            total_prepayment += parseInt($(service_prepayment[i]).val().replace(comma, ''));
    }
    $("#TotalPrepayment").html(Humanize.intComma(total_prepayment));

    $("#TotalPostpayment").html(Humanize.intComma((total_price - total_prepayment)));
}

function customer_dropselect(search_text) {
    if (search_text !== '') {
        $.get('/Customer/FindAllBySearch', { val: search_text }, function (response) {
            console.log(response);
            $("#customerProfile_search_item").removeClass('hide-select-dropdown').empty();
            if (response.entities.length > 0) {
                for (var i = 0; i < response.entities.length; i++) {
                    $("#customerProfile_search_item").append('<li onclick="set_customer_values(\'' + response.entities[i].id + '\',\'' + response.entities[i].fullName + '\',\'' + response.entities[i].memberCode + '\',\'' + response.entities[i].mobile + '\')"><img style="width: 30px;margin-left: 3px;" alt="' + response.entities[i].fullName + '" src="' + response.entities[i].avatar + '"/><span>' + response.entities[i].fullName + ' (' + response.entities[i].mobile + ')' + '</span></li>');
                }
            } else {
                $("#CustomerProfileId").val('');
                $("#customerProfile_search_item").append('<li style="text-align: right;padding: 11px 2px;">مشتری یافت نشد ابتدا آنرا در لیست مشتریان اضافه کنید</li>');
            }

        })
    } else {
        $("#CustomerProfileId").val('');
        $("#customerProfile_search_item").empty().removeClass('hide-select-dropdown');
    }
}

function set_customer_values(id, name, memberCode, mobile) {
    $("#CustomerProfileId").val(id);
    $("#CustomerProfileName").val(name);
    $("#MemberCode").val(memberCode);
    $("#CustomerMobile").val(mobile);

    $("#customerProfile_search_item").addClass('hide-select-dropdown').empty();
}

function appointment_cancel(id) {
    BeautyApp.progress($("#appointment_cancel"));
    $.post('/Appointment/Cancel', { appId: id }, function (response) {
        BeautyApp.unprogress($("#appointment_cancel"));
        if (response.isSuccess) {
            toastr.success(response.message);
            build_calendar();
        } else {
            toastr.error(response.message);
        }
    });
}

function appointment_delete(id) {
    BeautyApp.progress($("#appointment_delete"));
    $.post('/Appointment/Delete', { appId: id }, function (response) {
        BeautyApp.unprogress($("#appointment_delete"));
        if (response.isSuccess) {
            toastr.success(response.message);
            build_calendar();
        } else {
            toastr.error(response.message);
        }
    });
}

function appointment_done(id) {
    BeautyApp.progress($("#appointment_done"));
    $.post('/Appointment/Done', { appId: id }, function (response) {
        BeautyApp.unprogress($("#appointment_done"));
        if (response.isSuccess) {
            toastr.success(response.message);
            build_calendar();
        } else {
            toastr.error(response.message);
        }
    });
}

function appointment_service_done(id, appointmentServiceId, el) {
    BeautyApp.progress($(el));
    $.post('/Appointment/DoneService', { appId: id, sId: appointmentServiceId }, function (response) {
        BeautyApp.unprogress($(el));
        if (response.isSuccess) {
            toastr.success(response.message);
            build_calendar();
            $(el).remove();
            $("#cell_opt_" + id).append('<span class="badge badge-success">انجام شده</span>')
        } else {
            toastr.error(response.message);
        }
    });
}

function appointment_add_pay(id) {
    var pay_amount = $("#pay_amount").val();

    $.post('/Appointment/AddPayment', { appId: id, payamount: pay_amount }, function (response) {
        BeautyApp.unprogress($("#appointment_done"));
        if (response.isSuccess) {
            toastr.success(response.message);
        } else {
            toastr.error(response.message);
        }
    });
}