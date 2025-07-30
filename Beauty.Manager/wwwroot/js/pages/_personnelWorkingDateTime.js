
$(function () {
    build_calendar();
    $("#from_time,#to_time").timepicker({
        minuteStep: 1,
        defaultTime: "",
        showSeconds: !1,
        showMeridian: !1,
        snapToStep: !0
    });

    $("#cal_year").on('change', function () {
        build_calendar($(this).find('option:selected').val());
    })

    $("span.date-badge").click(function () {
        var date = $(this).data('date');
        var weekday_badge = $(this).data('weekday-badge');
        var day_badge = $(this).data('day-badge');
        var month_badge = $(this).data('month-badge');
        var checked = $(this).data('checked');
        var id = $("#day_val_" + month_badge + "_" + day_badge).val();

        BeautyApp.block("#day_holder_" + month_badge + "_" + day_badge, {
            overlayColor: "#000000",
            type: "v2",
            state: "success",
            size: "lg"
        });
        $.ajax({
            url: '/Personnel/UpdateWorkingTime',
            data: {
                Id: id,
                PersonnelProfileId: $("#p_id").val(),
                Date: date,
                Selected: !checked
            },
            type: 'POST',
            success: function (response) {
                setTimeout(function () { BeautyApp.unblock("#day_holder_" + month_badge + "_" + day_badge) }, 2e2),
                    function (response) {
                        if (response.isSuccess) {
                            $("#day_val_" + month_badge + "_" + day_badge).val(response.id);
                            day_badge_change(date, weekday_badge, checked);
                        } else
                            toastr.error(response.message);
                    }(response);
            }
        });
    })

});


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
            var day_val_item = model_data.filter(function (item) {
                return item.date === (year + '/' + (temp_month < 10 ? '0' + temp_month : temp_month) + '/' + (temp_day < 10 ? '0' + temp_day : temp_day));
            });
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
    $("#days_calendar").empty().append(template)
}

function dayOfMonth_change(val, checked) {
    if (!checked && val != 7) {
        $("td[data-weekday=" + val + "]").data('selected', 1);
        $("span[data-weekday-badge=" + val + "]").removeClass('beauty-badge--metal').addClass('beauty-badge--success').data('checked', true);
    }
    else if (!checked && val == 7) {
        $("td[data-weekday=" + val + "]").data('selected', 1);
        $("span[data-weekday-badge=" + val + "]").removeClass('beauty-badge--danger').addClass('beauty-badge--success').data('checked', true);
    }
    else if (checked && val == 7) {
        $("td[data-weekday=" + val + "]").data('selected', 1);
        $("span[data-weekday-badge=" + val + "]").removeClass('beauty-badge--success').addClass('beauty-badge--danger').data('checked', false);

    }
    else {
        $("td[data-weekday=" + val + "]").data('selected', 0);
        $("span[data-weekday-badge=" + val + "]").removeClass('beauty-badge--success').addClass('beauty-badge--metal').data('checked', false);
    }
}

function day_badge_change(date, weekday, checked) {
    if (!checked && weekday != 7) {
        $("span[data-date='" + date + "']").removeClass('beauty-badge--metal').addClass('beauty-badge--success').data('checked', true);
    }
    else if (!checked && weekday == 7) {
        $("span[data-date='" + date + "']").removeClass('beauty-badge--danger').addClass('beauty-badge--success').data('checked', true);
    }
    else if (checked && weekday == 7) {
        $("span[data-date='" + date + "']").removeClass('beauty-badge--success').addClass('beauty-badge--danger').data('checked', false);

    }
    else {
        $("span[data-date='" + date + "']").removeClass('beauty-badge--success').addClass('beauty-badge--metal').data('checked', false);
    }
}