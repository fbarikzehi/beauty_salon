

$(function () {

    $('#Entity_From').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });
    $('#Entity_To').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });
    $('#Entity_ExtendTo').JalaliDateTimePicker({
        dateFormat: 'yyyy/MM/dd',
        textFormat: 'yyyy/MM/dd',
    });

    $("#Entity_DiscountPrice").inputmask("999,999", {
        numericInput: !0
    });

    $("#save_btn").click(function (e) {
        var l = Ladda.create(this);
        l.start();
        var form = $("#servicepackage_frm");
        $.post({
            type: 'POST',
            url: "/servicepackage/update",
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

function search_service(el) {

    var search_text = $(el).val();
    if (search_text !== '') {
        $.get('/Service/SearchAllByName', { val: search_text }, function (response) {
            $("#search_select").empty().css('display', '');
            if (response.data.length > 0) {
                for (var i = 0; i < response.data.length; i++) {
                    $("#search_select").append('<li onclick="choose_service(\'' + response.data[i].title + '\',\'' + response.data[i].id + '\',\'' + response.data[i].price + '\')"><i class="feather icon-plus-circle text-primary" style="font-size:18px;font-weight:bolder;margin-left: 2%;"></i>' + response.data[i].title + '</li>');
                }
            } else {
                $("#search_select").empty().append('<li style="text-align: right;padding: 11px 2px;"><i class="feather icon-info text-danger" style="font-size:18px;font-weight:bolder;"></i> خدمت یافت نشد.خدمت را به لیست خدمات اضافه کنید <a href="/Service/Index" target="_blank">کلیک کنید</a> </li>');
            }
        })
    } else {
        $("#search_select").empty().css('display', 'none');
    }
}

function choose_service(title, id, price) {
    let index = $("#row_index").val();

    var data = {
        index: index,
        serviceId: id,
        serviceTitle: title,
        servicePrice: price,
        afterDiscountPrice: ''
    };

    let template = $("#item_template").html();
    let html = Mustache.render(template, data);
    $("#services_wrapper").append(html);

    index++;
    $("#row_index").val(index);
    $("#service_search").val('');
    $("#search_select").empty().css('display', 'none');
}


function delete_service(index, servicePackageItemId) {
    var inputs = $("#Entity_Items_" + index).find('input');
    if (servicePackageItemId === 0 || servicePackageItemId === undefined) {
        for (var i = 0; i < inputs.length; i++) {
            $(inputs[i]).val('0');
        }
        $("#Entity_Items_" + index).hide();
    }
    else {
        $.post('/servicepackage/ServicePackageDeleteService', { servicePackageItemId: servicePackageItemId }, function (response) {
            if (response.isSuccess) {
                for (var i = 0; i < inputs.length; i++) {
                    $(inputs[i]).val('0');
                }
                $("#Entity_Items_" + index).hide();
            } else {
                toastr.error(response.message);
            }
        });
    }
}