
var table = null;
$(function () {
    table = $("#personnels").DataTable({
        "processing": true,
        "serverSide": true,
        "paginate": true,
        "paging": true,
        "ordering": true,
        "filter": true,
        "ajax": "/Personnel/GetData",
        "columns": [
            { data: null, render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { "data": "fullName" },
            { "data": "mobile" },
            { "data": "code" },
            { "data": "username" },
            { "data": "cooperationType" },
            {
                "data": null, "render": function (data, type, row, meta) {
                    var actions = '<form action="/personnel/Modify" method="post"><input name="personnelId" type="hidden" value="' + data.id + '">' +
                        '<button type="submit" class="btn btn-primary mr-1 waves-effect waves-light"><i class="feather icon-edit"></i>ویرایش</button>' +
                        '<button type="button" onclick="delete_personnel(\'' + data.id + '\')" class="btn btn-danger mr-1 waves-effect waves-light">' +
                        '<i class="feather icon-x"></i>حذف</button></form > ';

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



    //var l = Ladda.create(btn);
    //l.start();

    //l.stop();
    //toastr.success(response.message);
    //toastr.error(response.message);







});

function delete_personnel(id) {
    Swal.fire({
        title: ' اطلاعات این پرسنل حذف شود؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله حذف شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/personnel/delete', { pId: id }, function (response) {
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