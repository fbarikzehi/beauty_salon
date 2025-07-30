


$(function () {
    $("#open_calendar").click(function () {
        $.get('/sms/GetCalendar', { year: 0 }, function (response) {
            let calendar = response.entity;
            calendar_template_builder(calendar);
        });
    });

    $("#save_btn").click(function () {
        var l = Ladda.create(this);
        l.start();
        var form = $("#sms_frm");
        $.post({
            type: 'POST',
            url: "/sms/UpdateMessage",
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                }
                else
                    toastr.error(response.message);

                l.stop();
            },
            error: function (e) {
                console.error(e);
                toastr.error('خطایی رخ داد با مدیریت تماس بگیرید', 'خطا');
                l.stop();
            }
        });
    });
});

function calendar_template_builder(calendar) {
    console.log(calendar);


    var template = `<table class="table table-bordered m-table">
              <thead>
                  <tr>
                      <th class="border-0">
                      </th>
                      <th>
                      <div>
                          <label>
                             شنبه       
                          </label>
                      </div>
                      </th>
                      <th>
                      <div>
                          <label>
                             یکشنبه       
                          </label>
                      </div>
                      </th>
                      <th>
                     <div>
                          <label>
                             دوشنبه       
                          </label>
                      </div>
                      </th>
                      <th>
                      <div>
                          <label>
                             سه شنبه       
                          </label>
                      </div>
                      </th>
                      <th> 
                     <div>
                          <label>
                             چهارشنبه       
                          </label>
                      </div>
                      </th>
                      <th>
                      <div>
                          <label>
                             پنجشنبه       
                          </label>
                      </div>
                      </th>
                      <th>
                      <div>
                          <label>
                             جمعه       
                          </label>
                      </div>
                      </th>
                  </tr>
              </thead>
              <tbody>`;

    for (var m = 1; m <= calendar.months.length; m++) {
        let month_name = calendar.months[m].title;
        var daysInMonth = calendar.months[m].days.length;

    }
}

function build_calendar(year = 1399) {
    persianDate.toLocale('en');
    var pc_now = new persianDate().format();
    var currentYear = parseInt(pc_now.split(' ')[0].split('-')[0]);
    var currentMonth = parseInt(pc_now.split(' ')[0].split('-')[1]);
    var currentDay = parseInt(pc_now.split(' ')[0].split('-')[2]);
    var now_date = new persianDate();

    var template = `<table class="table table-bordered m-table">
              <thead>
                  <tr>
                      <th class="border-0">
                           <label class="beauty-checkbox beauty-checkbox--brand">
                              <input type="checkbox" value="0" onchange="dayOfMonth_all_change()">همه
                              <span></span>
                          </label>
                      </th>
                      <th>
                      <div class="beauty-checkbox-inline">
                          <label class="beauty-checkbox beauty-checkbox--brand">
                              <input type="checkbox" value="1" onchange="dayOfMonth_change('1',this.checked)">شنبه
                              <span></span>
                          </label>
                      </div>
                      </th>
                      <th>
                      <div class="beauty-checkbox-inline">
                          <label class="beauty-checkbox beauty-checkbox--brand">
                              <input type="checkbox" value="1" onchange="dayOfMonth_change('2',this.checked)">یکشنبه
                              <span></span>
                          </label>
                      </div>
                      </th>
                      <th>
                      <div class="beauty-checkbox-inline">
                          <label class="beauty-checkbox beauty-checkbox--brand">
                              <input type="checkbox" value="1" onchange="dayOfMonth_change('3',this.checked)">دوشنبه
                              <span></span>
                          </label>
                      </div>
                      </th>
                      <th>
                      <div class="beauty-checkbox-inline">
                          <label class="beauty-checkbox beauty-checkbox--brand">
                              <input type="checkbox" value="1" onchange="dayOfMonth_change('4',this.checked)">سه شنبه
                              <span></span>
                          </label>
                      </div>
                      </th>
                      <th> 
                      <div class="beauty-checkbox-inline">
                          <label class="beauty-checkbox beauty-checkbox--brand">
                              <input type="checkbox" value="1" onchange="dayOfMonth_change('5',this.checked)">چهارشنبه
                              <span></span>
                          </label>
                      </div>
                      </th>
                      <th>
                      <div class="beauty-checkbox-inline">
                          <label class="beauty-checkbox beauty-checkbox--brand">
                              <input type="checkbox" value="1" onchange="dayOfMonth_change('6',this.checked)">پنج شنبه
                              <span></span>
                          </label>
                      </div>
                      </th>
                      <th>
                      <div class="beauty-checkbox-inline">
                          <label class="beauty-checkbox beauty-checkbox--brand">
                              <input type="checkbox" value="1" onchange="dayOfMonth_change('7',this.checked)">جمعه
                              <span></span>
                          </label>
                      </div>
                      </th>
                  </tr>
              </thead>
              <tbody>`;

    for (var temp_month = 1; temp_month <= 12; temp_month++) {
        let month_name = '';
        switch (temp_month) {
            case 1:
                month_name = 'فروردین';
                break;
            case 2:
                month_name = 'اردیبهشت';
                break;
            case 3:
                month_name = 'خرداد';
                break;
            case 4:
                month_name = 'تیر';
                break;
            case 5:
                month_name = 'مرداد';
                break;
            case 6:
                month_name = 'شهریور';
                break;
            case 7:
                month_name = 'مهر';
                break;
            case 8:
                month_name = 'آبان';
                break;
            case 9:
                month_name = 'آذر';
                break;
            case 10:
                month_name = 'دی';
                break;
            case 11:
                month_name = 'بهمن';
                break;
            case 12:
                month_name = 'اسفند';
                break;
            default:
                break;
        }
        var daysInMonth = new persianDate([year, temp_month]).daysInMonth();


        var start_date = new persianDate([year, temp_month, 1]);
        var start_day_of_week = start_date.day();

        template += `<tr><td rowspan="` + Math.ceil(((start_day_of_week - 1) + daysInMonth) / 7) + `" style="width: 99px;text-align: center;vertical-align: middle;"><p style="writing-mode: vertical-rl;text-orientation: mixed;">` + month_name + `</p></td>`;
        for (var i = 0; i < start_day_of_week - 1; i++) {
            template += `<td style="background: aliceblue;"></td>`;
        }

        for (var temp_day = 1; temp_day <= daysInMonth; temp_day++) {
            var temp_date = new persianDate([year, temp_month, temp_day]);
            var day_of_week = temp_date.day();
            var day_val_item = [];
            if ((temp_day + start_day_of_week - 1) % 7 !== 0) {
                if (temp_day < 31)
                    template += `<td  id="day_holder_` + temp_month + `_` + temp_day + `" data-weekday="` + day_of_week + `"  data-year="` + year + `" data-month="` + temp_month + `"  data-day="` + temp_day + `" data-selected="0">
                                      <input type="hidden" id="day_val_` + temp_month + `_` + temp_day + `" value="` + (day_val_item[0] === undefined ? "0" : day_val_item[0].id) + `"/>
                                      <span class="beauty-badge beauty-badge--md beauty-badge--` + (day_val_item[0] === undefined ? (day_of_week === 7 ? "danger" : "metal") : "success") + ` date-badge" data-weekday-badge="` + day_of_week + `" data-day-badge="` + temp_day + `"  data-month-badge="` + temp_month + `"  data-date="` + (year + '/' + temp_month + '/' + temp_day) + `" data-checked="` + (day_val_item[0] === undefined ? "false" : "true") + `" data-fromTime="00:00" data-toTime="00:00">` + temp_day + `</span>
                                 </td>`;
                else
                    template += `<td  id="day_holder_` + temp_month + `_` + temp_day + `" data-weekday="` + day_of_week + `"  data-year="` + year + `" data-month="` + temp_month + `"  data-day="` + temp_day + `" data-selected="0">
                                      <input type="hidden" id="day_val_` + temp_month + `_` + temp_day + `" value="` + (day_val_item[0] === undefined ? "0" : day_val_item[0].id) + `"/>
                                      <span class="beauty-badge beauty-badge--md beauty-badge--` + (day_val_item[0] === undefined ? (day_of_week === 7 ? "danger" : "metal") : "success") + ` date-badge" data-weekday-badge="` + day_of_week + `"  data-day-badge="` + temp_day + `" data-month-badge="` + temp_month + `" data-date="` + (year + '/' + temp_month + '/' + temp_day) + `" data-checked="` + (day_val_item[0] === undefined ? "false" : "true") + `" data-fromTime="00:00" data-toTime="00:00">` + temp_day + `</span>
                                 </td></tr>`;
            }
            else {
                template += `<td id="day_holder_` + temp_month + `_` + temp_day + `" data-weekday="` + day_of_week + `"  data-year="` + year + `" data-month="` + temp_month + `"  data-day="` + temp_day + `" data-selected="0">
                                      <input type="hidden" id="day_val_` + temp_month + `_` + temp_day + `"  value="` + (day_val_item[0] === undefined ? "0" : day_val_item[0].id) + `"/>
                                     <span class="beauty-badge beauty-badge--md beauty-badge--` + (day_val_item[0] === undefined ? (day_of_week === 7 ? "danger" : "metal") : "success") + ` date-badge" data-weekday-badge="` + day_of_week + `"  data-day-badge="` + temp_day + `" data-month-badge="` + temp_month + `" data-date="` + (year + '/' + temp_month + '/' + temp_day) + `" data-checked="` + (day_val_item[0] === undefined ? "false" : "true") + `" data-fromTime="00:00" data-toTime="00:00">` + temp_day + `</span>
                             </td></tr><tr>`;
            }
        }

    }
    template += `</tbody></table>`;
    $("#calendar_wrapper").empty().append(template)
}
