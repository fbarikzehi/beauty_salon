
var table = null;
$(function () {
    table = $("#users").DataTable({
        "processing": true,
        "serverSide": true,
        "paginate": true,
        "paging": true,
        "ordering": true,
        "filter": true, 
        "ajax": "/user/GetData",
        "columns": [
            { data: null, render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { "data": "fullName" },
            { "data": "username" },
            { "data": "role" },
            { "data": "lockStatus" },
            { "data": "lastLogin" },
            {
                "data": null, "render": function (data, type, row, meta) {
                    var actions = '<button type="button" onclick="edit_set(\'' + data.id + '\',\'' + data.fullName + '\',\'' + data.username + '\',\'' + data.roleId + '\')" class="btn btn-primary mr-1 waves-effect waves-light"><i class="feather icon-edit"></i>ویرایش</button>' +
                        '<button type="button" onclick="delete_user(\'' + data.id + '\')" class="btn btn-danger mr-1 waves-effect waves-light"><i class="feather icon-x"></i>حذف</button>';
                  
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

    $("#submit_create_user").click(function (e) {
        var l = Ladda.create(this);
        l.start();
        var form = $("#user_form");
        $.post({
            type: 'POST',
            url: "/user/create",
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

    $("#submit_edit_user").click(function (e) {
        var l = Ladda.create(this);
        l.start();
        var form = $("#user_form");
        $.post({
            type: 'POST',
            url: "/user/update",
            data: form.serialize(),
            success: function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    table.draw('page');
                    form[0].reset();

                    $("#submit_edit_user").css('display', 'none');
                    $("#submit_create_user").css('display', '');
                    $("#password_change_text").css('display', 'none');
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

function delete_user(id) {
    Swal.fire({
        title: ' اطلاعات این کاربر حذف شود؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله حذف شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/user/delete', { id: id }, function (response) {
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

function edit_set(id, fullName, username, roleId) {
    $("#Entity_Id").val(id);
    $("#Entity_FullName").val(fullName);
    $("#Entity_Username").val(username);

    $("#Entity_Password").val('');
    $("#Entity_ConfirmPassword").val('');
    $("#password_change_text").css('display', '');

    $("#Entity_RoleId").val(roleId);

    $("#submit_edit_user").css('display', '');
    $("#submit_create_user").css('display', 'none');

    $("#modal_create").click();
}

