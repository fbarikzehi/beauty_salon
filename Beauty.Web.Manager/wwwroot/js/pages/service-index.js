

var table = null;
$(function () {
    table = $("#services").DataTable({
        "processing": true,
        "serverSide": true,
        "paginate": true,
        "paging": true,
        "ordering": true,
        "filter": true,
        "ajax": "/service/GetData",
        "columns": [
            { data: null, render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { "data": "title" },
            { "data": "currentPrice" },
            { "data": "durationMinutes" },
            { "data": "score" },
            { "data": "customerCount" },
            { "data": "rate" },
            {
                "data": null, "render": function (data, type, row, meta) {
                    var actions = '<button type="button" onclick="edit_set(\'' + data.id + '\')" class="btn btn-primary mr-1 waves-effect waves-light"><i class="feather icon-edit"></i>ویرایش</button>' +
                        '<button type="button" onclick="delete_service(\'' + data.id + '\')" class="btn btn-danger mr-1 waves-effect waves-light"><i class="feather icon-x"></i>حذف</button>';
                    if (data.isActive) {
                        //
                    }
                    return actions;
                }
            }
        ],
        "searching": true,
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "همه"]],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Persian.json"
        },
    });

    $("#Entity_Price").inputmask("999,999", {
        numericInput: !0
    });
    $("#Entity_Prepayment").inputmask("999,999", {
        numericInput: !0
    });

    $("#submit_create_service").click(function (e) {
        var l = Ladda.create(this);
        l.start();
        var form = $("#service_form");
        $.post({
            type: 'POST',
            url: "/service/create",
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    table.draw('page');
                    form[0].reset();
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

    $("#submit_edit_service").click(function (e) {
        var l = Ladda.create(this);
        l.start();
        var form = $("#service_form");
        $.post({
            type: 'POST',
            url: "/service/update",
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    table.draw('page');
                    form[0].reset();

                    $("#submit_edit_service").css('display', 'none');
                    $("#submit_create_service").css('display', '');

                    $("#details_wrapper").empty();
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

function add_detail(el) {
    let index = $(el).val();

    var data = {
        index: index,
        id: 0,
        price: '',
        serviceId: 0,
        title: ''
    };

    let template = $("#details_template").html();
    let html = Mustache.render(template, data);
    if (index == 0)
        $("#details_wrapper").empty().append(html);
    else
        $("#details_wrapper").append(html);

    index++;
    $(el).val(index);
}

function delete_service(id) {
    Swal.fire({
        title: ' اطلاعات این خدمت حذف شود؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله حذف شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/service/delete', { id: id }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    table.draw('page');
                }
                else {
                    toastr.error(response.message);
                }
            });
        }
    })
}
function edit_set(id) {
    $.get('/service/getbyid', { id: id }, function (res) {
        var data = res.entity;
        $("#Entity_Id").val(data.id);
        $("#Entity_Title").val(data.title);
        $("#Entity_DurationMinutes").val(data.durationMinutes);
        $("#Entity_Price").val(data.currentPrice);
        $("#Entity_Score").val(data.score);
        $("#Entity_TakeItFreeCount").val(data.takeItFreeCount);
        $("#Entity_Prepayment").val(data.prepayment);
        $("#Entity_LineId").val(data.lineId);

        $("#details_wrapper").empty();
        for (var i = 0; i < res.entity.details.length; i++) {
            var data = {
                index: i,
                id: res.entity.details[i].id,
                price: res.entity.details[i].price,
                serviceId: res.entity.details[i].serviceId,
                title: res.entity.details[i].title
            };

            let template = $("#details_template").html();
            let html = Mustache.render(template, data);
            $("#details_wrapper").append(html);
        }
        $("#details_count").val(res.entity.details.length);

        $("#submit_edit_service").css('display', '');
        $("#submit_create_service").css('display', 'none');

        $("#modal_create").click();
    });
}

//function create_set() {
//    $("#Entity_Id").val('');
//    $("#Entity_Title").val('');
//    $("#Entity_DurationMinutes").val('0');
//    $("#Entity_Price").val('0');
//    $("#Entity_Score").val('0');
//    $("#Entity_TakeItFreeCount").val('0');
//    $("#Entity_Prepayment").val('0');

//    $("#submit_create_service").css('display', '');
//    $("#submit_edit_service").css('display', 'none');

//    $("#modal_create").click();
//}

function change_active(id) {
    $.post('/service/changeactive', { id: id }, function (response) {
        if (response.isSuccess) {
            toastr.success(response.message);
        }
        else {
            toastr.error(response.message);
        }
    });
}