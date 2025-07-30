const white_space = /\s/g, comma = /,/g;
var empty_guid = '00000000-0000-0000-0000-000000000000';

var time_row_items = 0;

$(function () {
    date_updown();

    $('#CreateDateTime').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });
    $('#current_date').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });
    $('#appointmentCustomerDate').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });

    $("#Mobile").inputmask("09999999999", {
        numericInput: !0
    });

    $(document).on('click', function () {
        if (getStateAppiontmentPanel()) {
            //$("#ServiceId").empty().append('<option value="">خدمت را انتخاب کنید</option>');
            //$("#Time").val('');
            //$("#appointment-customer-details").slideToggle("fast");
            //$('#appointment-customer-details').data('state', 'hidden');
        }

    });

    $("#current_date").on('change', function () {
        get_scheduler_data();
    });
    get_scheduler_data();
    $("#save_appointment").click(function (e) {

        var appointment_frm = $("#appointment_frm");

        $.ajax({
            url: '/Appointment/Create',
            data: appointment_frm.serialize(),
            type: 'POST',
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    get_scheduler_data();
                } else {
                    toastr.error(response.message);
                }
            }
        });
    });

    $("#submit_UpdateOpeningAndClosingTime").click(function (e) {

        var openingAndClosingTime_frm = $("#openingAndClosingTime_frm");
        $.ajax({
            url: '/Salon/UpdateOpeningAndClosingTime',
            data: openingAndClosingTime_frm.serialize(),
            type: 'POST',
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    get_scheduler_data();
                    $("#SalonOpeningTime_clock").text($("#OpeningTime").val());
                    $("#SalonClosingTime_clock").text($("#ClosingTime").val());
                } else {
                    toastr.error(response.message);
                }
            }
        });
    });

    $("#submit_change_personnel").click(function (e) {

        $.ajax({
            url: '/Appointment/ChangeServicePersonnel',
            data: { personnelId: $("#ChangePersonnelId").find('option:selected').val(), appointmentServiceId: $("#ChangePersonnel_appointmentServiceId").val(), appointmentId: $("#customer_appointent_details_appointmentId").val() },
            type: 'POST',
            success: function (response) {
                if (response.isSuccess) {
                    get_scheduler_data();
                    get_customer_details($("#customer_appointment_details_customerId").val(), $("#appointmentCustomerDate").val());
                    $("#ChangePersonnel_appointmentServiceId").val('0');
                } else {
                    toastr.error(response.message);
                }
            }
        });
    });

    $("#submit_service_details").click(function (e) {

        var modifyServiceDetails_appointment_frm = $("#modifyServiceDetails_appointment_frm");
        $.ajax({
            url: '/Appointment/ModifyServiceDetails',
            data: modifyServiceDetails_appointment_frm.serialize(),
            type: 'POST',
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    get_customer_details($("#customer_appointment_details_customerId").val(), $("#appointmentCustomerDate").val());
                } else {
                    toastr.error(response.message);
                }
            }
        });
    });

    $("#submit_add_discount").click(function (e) {
        var amount = $("#discount_price").val();
        $.ajax({
            url: '/Appointment/AddDiscount',
            data: { appointmentId: $("#customer_appointent_details_appointmentId").val(), amount: amount },
            type: 'POST',
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    $("#appointment_dicounts").append(`<tr id="payment_row_` + response.id + `">
                                                        <th scope="row">` + ($("#appointment_dicounts").find('th').length + 1) + `</th>
                                                        <td>`+ amount + `</td>
                                                        <td>`+ response.createDateTime + `</td>
                                                        <td> <button type="button" class="btn mr-1  btn-danger btn-sm waves-effect waves-light" onclick="delete_discount('`+ response.id + `')">حذف</button></td>

                                                   </tr>`);
                    get_customer_details($("#customer_appointment_details_customerId").val(), $("#appointmentCustomerDate").val());
                } else {
                    toastr.error(response.message);
                }
            }
        });
    });

});


//Scheduler
var cardHeight = 130;
function get_scheduler_data() {
    sppiner_toggle('scheduler_loader', true);
    $.get('/appointment/GetSchedulerData', { date: $("#current_date").text() }, function (response) {
        //console.table(response.data);
        var scheduler_template = `<table class="table table-responsive table-bordered border-info  font-bnazanin" id="scheduler-table"><thead><tr>`;
        for (var i = 0; i < response.data.length; i++) {
            var personnel = response.data[i];
            scheduler_template += `<th scope="col" class="text-center time-header-cell personnel-name" id="` + personnel.personnelId + `" style="padding: 0 1rem 0 1rem;">
                                   <form action="/personnel/Modify" method="post" target="_blank">
                                        <button  onclick="add_appointment({id:'`+ personnel.personnelId + `',name:'` + personnel.personnelFullName + `'},'00:00')" type="button" data-toggle="tooltip" data-original-title="افزودن نوبت" class="btn btn-icon btn-icon rounded-circle btn-success waves-effect waves-light" style="margin-top: -31px;margin-left: 0rem!important;"><i class="feather icon-plus"></i></button>
                                        <input name="personnelId" type="hidden" value="` + personnel.personnelId + `">
                                        <a href="#" onclick="this.parentNode.submit();"  title="برای ویرایش کلیک کنید" class="btn-flat-dark waves-effect waves-light" style="padding: 7px 52px;margin-top: 3px;">` + personnel.personnelFullName + `</a>
                                   </form>
                                   </th>`;
        }
        scheduler_template += '</tr></thead><tbody><tr style="height: calc(100vh - 489px);">';
        for (var i = 0; i < response.data.length; i++) {
            var personnel = response.data[i];
            scheduler_template += '<td style="vertical-align: top;padding: 5px;border: 1px solid #65656540; background: #efefef66; height: ' + cardHeight + 'px">';
            for (var j = 0; j < personnel.cards.length; j++) {
                var card = personnel.cards[j];
                if (card.status == true || card.status == null) {
                    scheduler_template += ` <div class="appointment-card ` + (card.isDone ? 'appointment-card-done' : 'appointment-card-pendding') + `">
                                                <p>
                                                    <span class="appointment-cell-name">`+ card.customerFullName + `</span>
                                                    <span class="appointment-cell-mobile">`+ card.customerMobile + `</span>
                                                    <span class="appointment-cell-time">`+ (card.status == null ? 'خارج' : card.startTime) + `</span>
                                                </p>
                                                <p style="margin-top:42px">`+ card.serviceTitle + `</p>
                                                    <div class="app-action">
                                                        <div class="action-right">
                                                            <ul class="list-inline m-0">`+
                        (card.isDone == false ? `<li class="list-inline-item"><span class="action-icon" title="حذف خدمت از نوبت">
                                                    <button type="button" onclick="delete_service('`+ card.appointmentServiceId + `',` + false + `)" class="btn btn-flat-dark waves-effect waves-light" style="padding: 6px 20px;background: #0d0d0d47;"><i class="feather icon-trash-2" style="color: #fff705;"></i></button></span>
                                                </li>
                                               <li class="list-inline-item"><span class="action-icon" title="انجام نوبت">
                                                    <button type="button" onclick="done_service('`+ card.appointmentId + `','` + card.appointmentServiceId + `',` + false + `)" class="btn btn-flat-dark waves-effect waves-light" style="padding: 6px 20px;background: #0d0d0d47;"><i class="feather icon-check" style="color: #2bff05;"></i></button></span>
                                                </li>`: `<li class="list-inline-item"><div class="badge badge-success">انجام شده</div></li>`)
                        + `<li class="list-inline-item"><span class="action-icon" title="مشاهده"  data-toggle="modal" data-target="#modal_customer_appointment" onclick="get_customer_details('` + card.customerId + `','` + card.date + `')"><i class="feather icon-file-text" style="color: azure;"></i></span></li>
                                                             </ul>
                                                         </div>
                                                    </div>
                                            </div>`;
                }
                //else {
                //    scheduler_template += `<div class="appointment-card appointment-card-empty">
                //                                  <button type="button" class="btn btn-icon btn-icon rounded-circle btn-success waves-effect waves-light" onclick="add_appointment({id:'`+ personnel.personnelId + `',name:'` + personnel.personnelFullName + `'},'` + card.startTime + `')"><i class="feather icon-plus"></i></button>
                //                                  <span class="appointment-cell-time" >`+ card.startTime + `</span>
                //                           </div>`;
                //}
            }
            scheduler_template += '</td>';
        }
        scheduler_template += '</tr></tbody></table>';

        $("#scheduler").empty().append(scheduler_template);
        sppiner_toggle('scheduler_loader', false);

    });
}
function scheduler_right_scroll() {
    event.preventDefault();
    //console.log($("#scheduler-table").scrollLeft());
    if ($("#scheduler-table").scrollLeft() < 0)
        $("#scheduler-table").animate({ scrollLeft: "+=1790px" }, "slow");
}
function scheduler_left_scroll() {
    event.preventDefault();
    if ($("#scheduler-table").scrollLeft() >= 0)
        $("#scheduler-table").animate({ scrollLeft: "-=1790px" }, "slow");
}
function call_appointment(id) {
    alert(id);
}
const getStateAppiontmentPanel = () => {
    return $('#appointment-customer-details').data('state');
}
const setStateAppiontmentPanel = (state) => {
    $('#appointment-customer-details').data('state', state);
}
function add_appointment(personnel, time, is_time_readonly) {
    show_appointment(personnel, time, is_time_readonly);
}
function add_out_of_appointment(time) {
    //$("#ServiceId").empty().append('<option value="">خدمت را انتخاب کنید</option>');
    //$.get('/service/GetAll', {}, function (response) {
    //    response.data.map(((item) => {
    //        $("#ServiceId").append('<option value="' + item.id + '">' + item.title + '</option>')
    //    }));
    //});
    load_personnel(null, false);

    $("#Time").val(time).prop('readonly', 'readonly');

    $("#PersonnelId").val(empty_guid);
    var date = $("#current_date").text();
    $("#Date").val(date);
    $("#personnel_name").text('');
    if (!getStateAppiontmentPanel()) {
        $("#appointment-customer-details").slideToggle("fast");
        setStateAppiontmentPanel(true);
    }
}
function show_appointment(personnel, time, is_time_readonly) {
    load_personnel(personnel.id, true);
    load_personnel_services(personnel.id);

    $("#Time").val(time);
    if (is_time_readonly) {
        $("#Time").prop('readonly', 'readonly');
    } else {
        $("#Time").prop('readonly', '');
    }
    $("#PersonnelId").val(personnel.id);
    var date = $("#current_date").text();
    $("#Date").val(date);
    $("#personnel_name").text(personnel.name);
    if (!getStateAppiontmentPanel()) {
        $("#appointment-customer-details").slideToggle("fast");
        setStateAppiontmentPanel(true);
    }
}
function dismiss_appiontment_info() {
    $("#appointment-customer-details").slideToggle("fast");
    setStateAppiontmentPanel(false);
}
function load_personnel(personnelId, isDisabled) {
    $("#PersonnelId").empty().append('<option value="">پرسنل را انتخاب کنید</option>');
    $.get('/personnel/GetAll', {}, function (response) {
        response.selectList.map(((item) => {
            if (item.value === personnelId) {
                $("#PersonnelId").append('<option value="' + item.value + '" selected="selected">' + item.text + '</option>');
            } else {
                $("#PersonnelId").append('<option value="' + item.value + '">' + item.text + '</option>');
            }
        })).join('');
    });
}
function load_personnel_services(personnelId) {
    if (personnelId === undefined || personnelId === null)
        personnelId = $("#PersonnelId").find('option:selected').val();
    $("#ServiceId").empty().append('<option value="">خدمت را انتخاب کنید</option>');
    $.get('/personnel/GetServicesAsJson', { pid: personnelId }, function (response) {
        //console.log(response);
        response.data.map(((item) => {
            $("#ServiceId").append('<option value="' + item.serviceId + '">' + item.serviceTitle + '</option>')
        })).join('');

    });
}
function sppiner_toggle(id, status) {
    if (status) {
        $("#" + id).css('display', '');
    } else {
        $("#" + id).css('display', 'none');
    }
}
function date_updown(dir) {
    var days = dir === 'prev' ? -1 : 1;
    $.get('/appointment/getevents', { date: $("#current_date").text(), days: days }, function (data) {
        $("#current_date").text(data.date);
        get_scheduler_data();
    });
}
function customer_dropselect(search_text, dropdown, input, bindder) {
    if (search_text !== '') {
        $.get('/Customer/FindAllBySearch', { val: search_text }, function (response) {
            $("#" + dropdown).removeClass('hide-select-dropdown').empty();
            if (response.data.length > 0) {
                for (var i = 0; i < response.data.length; i++) {
                    $("#" + dropdown).append('<li onclick="' + bindder + '(\'' + response.data[i].id + '\',\'' + response.data[i].fullName + '\',\'' + response.data[i].memberCode + '\',\'' + response.data[i].mobile + '\')"><img style="width: 21px;margin-left: 3px;" alt="' + response.data[i].fullName + '" src="' + response.data[i].avatar + '"/><span>' + response.data[i].fullName + ' (' + response.data[i].mobile + ')' + '</span></li>');
                }
            } else {
                $("#" + input).val('');
                $("#" + dropdown).append(`<li style="text-align: right;padding: 11px 2px;">
                                       <form action="/Customer/Modify" method="post" target="_blank">
                                        <input id="customerId" name="customerId" type="hidden" value="00000000-0000-0000-0000-000000000000">
                                        <button type="button" onclick="this.parentNode.submit();" class="btn btn-flat-primary btn-sm waves-effect waves-light" style="font-size: 13px;">مشتری یافت نشد.برای ثبت مشتری جدید اینجا کلیک کنید</button>
                                    </form></li>`);
            }

        })
    } else {
        $("#" + input).val('');
        $("#" + dropdown).empty().removeClass('hide-select-dropdown');
    }
}
function set_customer_values_appointment(id, name, memberCode, mobile) {
    $("#CustomerProfileId").val(id);
    $("#CustomerProfileName").val(name);
    $("#MemberCode").val(memberCode);
    $("#CustomerMobile").val(mobile);

    $("#customerProfile_search_item").addClass('hide-select-dropdown').empty();
}
function set_customer_values_appointment_details(id, name, memberCode, mobile) {
    $("#appointmentCustomerProfileId").val(id);
    $("#appointmentCustomerProfileName").val(name);
    $("#appointmentCustomerMemberCode").val(memberCode);
    $("#appointmentCustomerMobile").val(mobile);

    $("#appointmentcustomerProfile_search_item").addClass('hide-select-dropdown').empty();

}

function get_customer_details(customerId, date) {
    if (customerId === undefined || customerId == null) {
        customerId = $("#appointmentCustomerProfileId").val();
    }
    if (date === undefined || date == null) {
        date = $("#appointmentCustomerDate").val();
    } else {
        $("#appointmentCustomerDate").val(date);
    }
    $.get('/appointment/GetCustomerDetails', { customerId: customerId, date: date }, function (response) {
        if (!response.isSuccess) {
            $("#customer_appointent_services_wrapper").empty().append(`<div class="alert alert-danger" role="alert">
              <p class="mb-0 text-center" style="font-weight: bold;">
               `+ response.message + `
              </p>
            </div>`);
        } else {
            var data = response.entity;
            $("#appointmentCustomerProfileId").val(customerId);
            $("#appointmentCustomerProfileName").val(data.customerFullName);
            $("#appointmentCustomerMemberCode").val(data.customerMemberCode);
            $("#appointmentCustomerMobile").val(data.customerMobile);
            //console.table(data)
            var template = `<input type="hidden" value="` + data.appointmentId + `" id="customer_appointent_details_appointmentId" />
                            <input type="hidden" value="` + customerId + `" id="customer_appointment_details_customerId" />
                                <div class="row">
                                    <div class="col-md-12 col-lg-12 col-sm-12 col-12">
                                      <div id="invoice">
                                        <div class="row">
                                            <div class="col-md-2 col-lg-2 col-sm-12 col-12">
                                                <label>نام/نام خانوادگی: </label><span>`+ data.customerFullName + `</span>
                                            </div>
                                            <div class="col-md-2 col-lg-2 col-sm-12 col-12">
                                                <label>کد اشتراک: </label><span>`+ data.customerMemberCode + `</span>
                                            </div>
                                            <div class="col-md-2 col-lg-2 col-sm-12 col-12">
                                                <label>موبایل: </label><span>`+ data.customerMobile + `</span>
                                            </div>
                                            <div class="col-md-2 col-lg-1 col-sm-12 col-12">
                                                <label>تاریخ نوبت: </label><span>`+ data.appointmentDate + `</span>
                                            </div>
                                            <div class="col-md-2 col-lg-1 col-sm-12 col-12">
                                                <label>زمان نوبت: </label><span>`+ data.appointmentTime + `</span>
                                            </div>
                                            <div class="col-md-2 col-lg-2 col-sm-12 col-12">
                                                <label>تاریخ ثبت نوبت: </label><span>`+ data.appointmentCreateDate + `</span>
                                            </div>
                                            <div class="col-md-2 col-lg-2 col-sm-12 col-12">
                                                <label>شماره پیگیری: </label><span>`+ data.followingCode + `</span>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-md-12 col-lg-12 col-sm-12 col-12">
                                                <table class="table table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>ردیف</th>
                                                            <th>عنوان خدمت</th>
                                                            <th>پرسنل ارئه خدمت</th>
                                                            <th>قیمت واحد</th>
                                                            <th><i class="feather icon-settings"></i></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        `+ data.services.map((item, index) => {

                return `<tr  ` + (item.isDone == false ? 'style="background:#ffeb3ba6"' : "") + `>
                                                                          <td>` + (index + 1) + `</td>
                                                                          <td>` + item.serviceTitle + `</td>
                                                                          <td>` + item.personnelFullName + `</td>
                                                                          <td>` + item.servicePrice + `</td>
                                                                          <td>
                                                                              `+ item.serviceDetails.map((sd) => {

                    return ` <input type="hidden" id="details_` + sd.appointmentServiceDetailId + `_totalPrice"  value="` + sd.totalPrice + `" />`
                }).join('') + `
                                                                             <button type="button" class="btn btn-dark waves-effect waves-light"  data-toggle="modal" data-target="#change_personnel_modal" onclick="set_change_personnel_id('`+ item.appointmentServiceId + `')"><i class="feather icon-users"></i> تعویض پرسنل</button>
                                                                             `+ (item.hasServiceDetails == true ? '<button type="button" class="btn btn-info waves-effect waves-light" data-toggle="modal" data-target="#add_service_details_modal" onclick="set_appointment_service_details(\'' + item.appointmentServiceId + '\',\'' + item.serviceId + '\')"><i class="feather icon-align-right"></i> ثبت ریزهزینه</button>' : '') + `
                                                                             `+ (item.isDone == false ? ' <button type="button" class="btn btn-primary waves-effect waves-light" onclick="done_service(\'' + data.appointmentId + '\',\'' + item.appointmentServiceId + '\',' + true + ')"><i class="feather icon-check-circle"></i> انجام شد</button>' +
                        ' <button type="button" class="btn btn-danger waves-effect waves-light"  onclick="delete_service(\'' + item.appointmentServiceId + '\',' + true + ')"><i class="feather icon-x"></i> حذف</button>' : "") + `
                                                                          </td>
                                                                       </tr>`
            }).join('') + `
                                                    </tbody> 
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="3" style="border-bottom: none; border-right: none; text-align: left;border-top: 1px solid #00000038;">جمع کل (تومان)</td>
                                                            <td style="border: 1px solid #00000038;background: #daeeff;">`+ data.totalPrice + `</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="border-bottom: none; border-right: none; text-align: left;border-top: none;">مبلغ کل پرداخت شده (تومان)</td>
                                                            <td style="border: 1px solid #00000038;background: #ffeeda;"> `+ data.paidPrepaymentPrice + `</td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="3" style="border-bottom: none; border-right: none; text-align: left;border-top: none;">مبلغ کل تخفیف (تومان)</td>
                                                            <td style="border: 1px solid #00000038;background: #8c3b3b0d;"> `+ data.totalDiscountPrice + `</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="border-bottom: none; border-right: none; text-align: left;border-top: none;">مبلغ باقی مانده (تومان)</td>
                                                            <td style="border: 1px solid #00000038;background: #dafff1;"> `+ data.finalPayPrice + `</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-md-12 col-lg-12 col-sm-12 col-12">
                                                <button type="button" class="btn btn-dark mr-1 mb-1 waves-effect waves-light" onclick="printHTML('invoice_print_section')"><i class="feather icon-printer"></i> پرینت</button>
                                               <div class="btn-group dropdown mr-1 mb-1">
                                                   <button type="button" class="btn btn-primary">پرداخت</button>
                                                   <button type="button" class="btn btn-primary dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="sr-only">Toggle Dropdown</span></button>
                                                   <div class="dropdown-menu">
                                                   <a class="dropdown-item" href="javascript();"  data-toggle="modal" data-target="#payment_list"> ثبت وجه نقد</a>
                                                   <a class="dropdown-item" href="#">ثبت کارت خوان</a>
                                                   <a class="dropdown-item" href="javascript:;" onclick="load_cheque('`+ data.appointmentDate + `')"   data-toggle="modal" data-target="#cheque_list">ثبت چک</a>
                                                   <div class="dropdown-divider"></div>
                                                   <a class="dropdown-item" href="#">خلاصه وضعیت پرداخت</a>
                                                   </div>
                                                </div>
                                                <button type="button" class="btn bg-gradient-info mr-1 mb-1 waves-effect waves-light" data-toggle="modal" data-target="#add_discount" onclick="get_discounts('` + data.appointmentId + `')"><i class="feather icon-external-link"></i> ثبت تخفیف</button>` +
                opration_builder(data.isDelete, data.isDone, data.isCanceled, data.appointmentId)
                + `</div>
                                        </div>
                                    </div>
                                </div>
                                    <div class="modal fade text-left" id="payment_list" tabindex="-1" role="dialog"
                    aria-labelledby="myModalLabel17" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg" role="document">
                      <div class="modal-content" style="border: 1px solid #8BC34A;border-radius: 0;box-shadow: 0px 20px 20px 20px rgba(0,0,0,.1);">
                        <div class="modal-header">
                          <h4 class="modal-title" id="myModalLabel17">لیست پرداخت ها مشتری برای این نوبت</h4>
                          <span class="form-text text-danger" style=" font-size: .8rem !important;">برای خروج از لیست بروی فضای بیرون آن کلیک کنید</span>
                        </div>
                        <div class="modal-body">
                         <fieldset>
                                  <div class="input-group">
                                    <input type="text" id="payment_price" onkeyup="to_price(this)" class="form-control" placeholder="مبلغ جدید را وارد کنید" aria-describedby="button-addon2">
                                    <div class="input-group-append" id="button-addon2">
                                      <button class="btn btn-primary waves-effect waves-light" onclick="add_payment('`+ data.appointmentId + `')" type="button">ثبت مبلغ <i class="feather icon-plus"></i></button>
                                    </div>
                                  </div>
                        </fieldset>
                   <hr/>
                   <div class="table-responsive">
                     <table class="table table-bordered mb-0">
                         <thead>
                             <tr>
                                 <th>ردیف</th>
                                 <th>مبلغ</th>
                                 <th>تاریخ و ساعت</th>
                                 <th>عملیات</th>
                             </tr>
                         </thead>
                         <tbody id="payments">
                            `+ data.payments.map((item, index) => {
                    return `<tr  id="payment_row_` + item.id + `">
                                                        <th scope="row">` + (index + 1) + `</th>
                                                        <td>`+ item.amount + `</td>
                                                        <td>`+ item.createDateTime + `</td>
                                                        <td> <button type="button" class="btn mr-1 btn-danger btn-sm waves-effect waves-light" onclick="delete_payment('`+ item.id + `')">حذف</button></td>

                                                   </tr>`
                }).join('') + `
                         </tbody>
                     </table>
                </div>
                        </div>
                      </div>
                    </div>
                  </div>`;

            $("#customer_appointent_services_wrapper").empty().append(template);

            var print_template = `<div style="width:99.5%;border:1px solid #0000007d;padding:0;direction: rtl;padding-right: 10%;">
    <div style="width:98%;border-bottom:1px solid #0000007d;margin:0;position:center;direction: rtl;">
            <p style="font-family: tahoma;font-size: 13px;text-align: center;font-weight: bolder; ">سالن زیبایی آیریانا</p>
            <p style="font-family: tahoma;font-size: 10px;text-align: center;font-weight: bolder; ">تاریخ نوبت: `+ data.appointmentDate + `   زمان نوبت: ` + data.appointmentTime + `</p>
            <p style="font-family: tahoma;font-size: 10px;text-align: center;font-weight: bolder; ">کد پیگیری: ` + data.followingCode + `</p>
    </div>                                        
    <div style="width:100%;border-bottom:1px solid #0000007d;margin:0;position:center;padding-right: 6%;padding-top:6px;padding-bottom:6px;">
        <span style="font-family: tahoma;font-size: 10px;font-weight: bolder;text-align: center;">کد اشتراک `+ data.customerMemberCode + ` خانم ` + data.customerFullName + `</span>
    </div>
    <div style="direction: rtl;padding-right: 5%;">
        <table style="direction: rtl;">
            <thead>
                <tr style="">
                    <th style="border-left: 1px solid #00000040;border-bottom: 1px solid #00000040;background-color: #e2e2de;font-family: tahoma;font-size: 9px;font-weight: bolder;text-align: center;">ردیف</th>
                    <th style="border-left: 1px solid #00000040;border-bottom: 1px solid #00000040;background-color: #e2e2de;font-family: tahoma;font-size: 9px;font-weight: bolder;">عنوان خدمت</th>
                    <th style="border-left: 1px solid #00000040;border-bottom: 1px solid #00000040;background-color: #e2e2de;font-family: tahoma;font-size: 9px;font-weight: bolder;">پرسنل خدمت</th>
                    <th style="border-left: 1px solid #00000040;border-bottom: 1px solid #00000040;background-color: #e2e2de;font-family: tahoma;font-size: 9px;font-weight: bolder;"> قیمت</th>
                </tr>
            </thead>
            <tbody>
  `+ data.services.map((item, index) => {
                return ` <tr>
                    <td style="border-left: 1px solid #00000040;border-bottom: 1px solid #00000040;font-family: tahoma;font-size: 9px;font-weight: bolder;text-align: center;">` + (index + 1) + `</td>
                    <td style="border-left: 1px solid #00000040;border-bottom: 1px solid #00000040;font-family: tahoma;font-size: 9px;font-weight: bolder;">` + item.serviceTitle + `</td>
                    <td style="border-left: 1px solid #00000040;border-bottom: 1px solid #00000040;font-family: tahoma;font-size: 9px;font-weight: bolder;">` + item.personnelFullName + `</td>
                    <td style="border-left: 1px solid #00000040;border-bottom: 1px solid #00000040;font-family: tahoma;font-size: 9px;font-weight: bolder;">` + item.servicePrice + `</td>
                </tr>`
            }).join('') + `
               
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="3"  style="font-family: tahoma;font-size: 9px;font-weight: bolder;text-align: left;">جمع کل (تومان)</td>
                    <td style="font-family: tahoma;font-size: 9px;font-weight: bolder;">`+ data.totalPrice + `</td>
                </tr>
                <tr>
                    <td colspan="3" style="font-family: tahoma;font-size: 9px;font-weight: bolder;text-align: left;">مبلغ کل پرداخت شده (تومان)</td>
                    <td style="font-family: tahoma;font-size: 9px;font-weight: bolder;">`+ data.paidPrepaymentPrice + `</td>
                </tr>
                <tr>
                    <td colspan="3" style="font-family: tahoma;font-size: 9px;font-weight: bolder;text-align: left;">مبلغ کل تخفیف (تومان)</td>
                    <td style="font-family: tahoma;font-size: 9px;font-weight: bolder;">`+ data.totalDiscountPrice + `</td>
                </tr>
                <tr>
                    <td colspan="3" style="font-family: tahoma;font-size: 9px;font-weight: bolder;text-align: left;;background-color: #000;color:#fff;padding-top:8px;padding-bottom:8px;">مبلغ باقی مانده (تومان)</td>
                    <td style="font-family: tahoma;font-size: 9px;font-weight: bolder;background-color: #000;color:#fff;padding-top:8px;padding-bottom:8px;">`+ data.finalPayPrice + `</td>
                </tr>
            </tfoot>
        </table>
    </div>
    <p style="font-family: tahoma;font-size: 10px;text-align: center;font-weight: bolder;">سامانه بیوتیکا</p>
</div>`;


            $("#invoice_print_section").css('display', 'none').empty().append(print_template)
        }
    });
}
function set_change_personnel_id(appointmentServiceId) {
    $("#ChangePersonnel_appointmentServiceId").val(appointmentServiceId);
}
function set_appointment_service_details(appointmentServiceId, serviceId) {
    $('#add_service_details_items').empty();
    $("#add_service_details_appointmentServiceId").val(appointmentServiceId);
    $.get('/appointment/GetAppointmentServiceDetails', { appointmentServiceId: appointmentServiceId }, function (response) {
        if (response.isSuccess) {

            //console.log(response.data);
            $('#add_service_details_items').append(response.data.map((item, index) => {
                //console.log(index);
                return ` <div class="row  mb-75">
                            <input class="form-control" type="hidden" name="Details[`+ index + `].ServiceDetailId"  value="` + item.serviceDetailId + `" />
                            <div class="col-md-4 col-lg-4 col-sm-12 col-12">
                                <fieldset class="form-group position-relative" style="margin-bottom: 0;">
                                    <input class="form-control" type="text" readonly="readonly" value="`+ item.title + `"  style="background-color: #c8c8c8; "/>
                                </fieldset>
                            </div>
                            <div class="col-md-3 col-lg-3 col-sm-12 col-12">
                                <fieldset class="form-group position-relative" style="margin-bottom: 0;">
                                    <input class="form-control" type="text"  onkeyup="detail_item_sum_on_price(this,`+ index + `)"  name="Details[` + index + `].Price"  value="` + item.price + `"/>
                                </fieldset>
                            </div>
                            <div class="col-md-2 col-lg-2 col-sm-12 col-12">
                                <fieldset class="form-group position-relative" style="margin-bottom: 0;">
                                    <input type="number" class="form-control count_item_details" onkeyup="detail_item_sum_on_count(this,`+ index + `)"   name="Details[` + index + `].Count" value="` + item.count + `">
                                </fieldset>
                            </div>
                            <div class="col-md-3 col-lg-3 col-sm-12 col-12">
                                <fieldset class="form-group position-relative" style="margin-bottom: 0;">
                                    <input class="form-control" type="text" id="Details[`+ index + `]_TotalPrice" name="Details[` + index + `].TotalPrice"  value="` + item.totalPrice + `" />
                                </fieldset>
                            </div>
                    </div>`;
            }).join(''));

        }
    });
}

function opration_builder(isDelete, isDone, isCanceled, appointmentId) {
    var opt = '';

    if (!isCanceled && !isDelete && !isDone) {
        opt += '<button type="button" onclick="appointment_cancel(\'' + appointmentId + '\')" class="btn btn-warning mr-1 mb-1 waves-effect waves-light"><i class="feather icon-x"></i> لغو نوبت</button>' +
            '<button type="button" onclick="appointment_delete(\'' + appointmentId + '\')" class="btn btn-danger mr-1 mb-1 waves-effect waves-light"><i class="feather icon-x"></i> حذف نوبت</button>' +
            '<button type="button" onclick="appointment_done(\'' + appointmentId + '\')" class="btn btn-success mr-1 mb-1 waves-effect waves-light"><i class="feather icon-user-check"></i>خاتمه نوبت</button>';
    }
    else if (isCanceled) {
        opt = '<div class="badge badge-warning badge-lg mr-1 mb-1">این نوبت لغو شده </div>'
    }
    else if (isDelete) {
        opt = '<div class="badge badge-danger badge-lg mr-1 mb-1">این نوبت حذف شده </div>'
    }
    else if (isDone) {
        opt = '<div class="badge badge-success badge-lg mr-1 mb-1">این نوبت انجام شده </div>'
    }
    return opt;

}
function appointment_cancel(id) {

    Swal.fire({
        title: ' این عملیات قابل بازگشت نیست:لغو نوبت مطمئن هستید؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: ' بله لغو شود ',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/Appointment/Cancel', { appointmentId: id }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    get_scheduler_data();
                } else {
                    toastr.error(response.message);
                }
            });
        }
    })

}
function appointment_delete(id) {
    Swal.fire({
        title: ' این عملیات قابل بازگشت نیست:برای حذف نوبت مطمئن هستید؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: ' بله حذف شود ',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/Appointment/Delete', { appointmentId: id }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    get_scheduler_data();
                } else {
                    toastr.error(response.message);
                }
            });
        }
    })
}
function appointment_done(id) {
    Swal.fire({
        title: ' این عملیات قابل بازگشت نیست: نوبت انجام شده؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: ' بله انجام شود ',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/Appointment/Done', { appointmentId: id }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    get_scheduler_data();
                } else {
                    toastr.error(response.message);
                }
            });
        }
    })
}
function delete_service(id, isCustomerDetails) {
    Swal.fire({
        title: ' این عملیات قابل بازگشت نیست: این خدمت از نوبت حذف شود؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: ' بله حذف شود ',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: '/Appointment/DeleteService',
                data: { appointmentServiceId: id },
                type: 'POST',
                success: function (response) {
                    if (response.isSuccess) {
                        get_scheduler_data();
                        if (isCustomerDetails)
                            get_customer_details($("#customer_appointment_details_customerId").val(), $("#appointmentCustomerDate").val());
                    } else {
                        toastr.error(response.message);
                    }
                }
            });
        }
    })
}
function done_service(appointmentId, appointmentServiceId, isCustomerDetails) {
    Swal.fire({
        title: ' این عملیات قابل بازگشت نیست: این خدمت انجام شده؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: ' بله انجام شد ',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/Appointment/DoneService', { appointmentId: appointmentId, appointmentServiceId: appointmentServiceId }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    get_scheduler_data();
                    if (isCustomerDetails)
                        get_customer_details($("#customer_appointment_details_customerId").val(), $("#appointmentCustomerDate").val());
                } else {
                    toastr.error(response.message);
                }
            });
        }
    })


}
function to_price(el) {
    $(el).val(Humanize.intComma($(el).val().replace(comma, '')));
}
function add_payment(appointmentId) {
    var pay_amount = $("#payment_price").val().replace(comma, '');
    $.post('/Appointment/AddPayment', { appointmentId: appointmentId, payamount: pay_amount }, function (response) {
        if (response.isSuccess) {
            toastr.success(response.message);
            $("#payments").append(`<tr id="payment_row_` + response.id + `">
                                                        <th scope="row">` + ($("#payments").find('th').length + 1) + `</th>
                                                        <td>`+ Humanize.intComma(pay_amount) + `</td>
                                                        <td>`+ response.createDateTime + `</td>
                                                        <td> <button type="button" class="btn mr-1  btn-danger btn-sm waves-effect waves-light" onclick="delete_payment('`+ response.id + `')">حذف</button></td>

                                                   </tr>`);

            $("#payment_price").val('');
            get_customer_details($("#customer_appointment_details_customerId").val(), $("#appointmentCustomerDate").val());
        } else {
            toastr.error(response.message);
        }
    });
}
function delete_payment(paymentId) {

    Swal.fire({
        title: ' این عملیات قابل بازگشت نیست:از حذف این پرداخت نوبت مطمئن هستید؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: ' بله حذف شود ',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/Appointment/DeletePayment', { paymentId: paymentId }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    $("tr#payment_row_" + paymentId).remove();
                } else {
                    toastr.error(response.message);
                }
            });
        }
    })

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
function reset_frm_appointment() {
    $("#services_wrapper").empty();

    let template = $("#service_template").html();
    let html = Mustache.render(template, {
        index: 0,
        ServiceId: 0,
        ServiceTitle: "",
        DurationMinutes: "",
        Price: "",
        Prepayment: ""
    });
    $("#services_wrapper").append(html);
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

function detail_item_sum_on_count(el, index) {
    //price --> div:eq(1)
    var price = $("#add_service_details_items > div:eq(" + index + ") > div:eq(1) > fieldset > input").val();
    var count = $(el).val();

    //totalPrice --> div:eq(3)
    $("#add_service_details_items > div:eq(" + index + ") > div:eq(3) > fieldset > input").val(Humanize.intComma(count * parseInt(price.replace(comma, ''))));
}
function detail_item_sum_on_price(el, index) {
    var price = $(el).val();

    //count --> div:eq(2)
    var count = $("#add_service_details_items > div:eq(" + index + ") > div:eq(2) > fieldset > input").val();

    $("#add_service_details_items > div:eq(" + index + ") > div:eq(3) > fieldset > input").val(Humanize.intComma(count * parseInt(price.replace(comma, ''))));

    $(el).val(Humanize.intComma(price.replace(comma, '')));
}

function get_discounts(appointmentId) {
    $.get('/appointment/GetAllAppointmentDiscount', { appointmentId: appointmentId }, function (response) {

        $("#appointment_dicounts").empty().append(response.data.map((item, index) => {
            return `  <tr id="discount_row_` + item.id + `">
                                <th scope="row">` + (index + 1) + `</th>
                                <td>` + item.amount + `</td>
                                <td>`+ item.createDateTime + `</td>
                                <td> <button type="button" class="btn mr-1 btn-danger btn-sm waves-effect waves-light" onclick="delete_discount('`+ item.id + `')">حذف</button></td>
                            </tr>`;
        }));
    });
}
function delete_discount(discountId) {

    Swal.fire({
        title: ' این عملیات قابل بازگشت نیست:از حذف این تخفیف نوبت مطمئن هستید؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: ' بله حذف شود ',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/Appointment/DeleteDiscount', { discountId: discountId }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    $("tr#discount_row_" + discountId).remove();
                } else {
                    toastr.error(response.message);
                }
            });
        }
    })

}

function load_cheque(date) {
    $.get('/Customer/GetAllCheque', { CreateDateTime: date, CustomerProfileId: $("#appointmentCustomerProfileId").val() }, function (response) {
        $("#cheques").empty();
        response.data.map((item, index) => {
            $("#cheques").append(`<tr id="cheque_row_${index}">
                                      <th scope="row">${index + 1}</th>
                                      <td>${item.fee}</td>
                                      <td>${item.bankName}</td>
                                      <td>${item.number}</td>
                                      <td>${item.date}</td>
                                      <td>${item.createDateTime}</td>
                                      <td> <button type="button" class="btn mr-1 btn-danger btn-sm waves-effect waves-light" onclick="delete_cheque('${item.id}')">حذف</button></td>
                                 </tr>`);
            return true;
        });
    });
}
