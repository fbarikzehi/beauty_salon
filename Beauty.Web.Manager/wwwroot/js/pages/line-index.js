

$(function () {

    $("#submit_create_line").click(function (e) {
        var l = Ladda.create(this);
        l.start();
        var form = $("#line_form");
        $.post({
            type: 'POST',
            url: "/line/create",
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    var id = response.data;
                    var title = $("#Title").val();
                    $("#lines").append(`<div class="col-xl-2 col-md-4 col-sm-6" id="card_id_` + id + `">
                                          <div class="card text-center">
                                            <div class="card-content">
                                                <div class="card-body">
                                                    <div class="avatar bg-rgba-info p-50 m-0 mb-1">
                                                     <div class="avatar-content">
                                                              <i class="feather icon-award text-info font-medium-5"></i>
                                                     </div>
                                                   </div>
                                                   <h2 class="text-bold-700">0</h2>
                                                   <p class="mb-0 line-ellipsis" id="line_title_id_` + id + `">` + title + `</p>
                                                   <hr />
                                                   <button type="button" class="btn mr-1 mb-1 btn-success btn-sm waves-effect waves-light" onclick="edit_set('`+ id + `','` + title + `')"><i class="feather icon-plus"></i> ویرایش</button>
                                                   <button type="button" class="btn mr-1 mb-1 btn-danger btn-sm ml-25 waves-effect waves-light" onclick="delete_line('`+ id + `')"><i class="feather icon-x"></i> حذف</button> 
                                               </div>
                                           </div>
                                         </div>
                                 </div>`);
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

    $("#submit_edit_line").click(function (e) {
        var l = Ladda.create(this);
        l.start();
        var form = $("#line_form");
        $.post({
            type: 'POST',
            url: "/line/update",
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    $("#line_title_id_" + $("#Id").val()).text($("#Title").val());
                    form[0].reset();

                    $("#submit_edit_line").css('display', 'none');
                    $("#submit_create_line").css('display', '');
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

function delete_line(id) {
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
            $.post('/line/delete', { id: id }, function (response) {
                if (response.isSuccess) {
                    $("#card_id_" + id).remove();
                    toastr.success(response.message);
                }
                else {
                    toastr.error(response.message);
                }
            });
        }
    })
}
function edit_set(id, title) {
    alert();
    $("#Id").val(id);
    $("#Title").val(title);

    $("#submit_edit_line").css('display', '');
    $("#submit_create_line").css('display', 'none');

    $("#line_modal_btn").click();
}
