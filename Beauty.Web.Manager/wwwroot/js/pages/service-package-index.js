

var table = null;
$(function () {
    table = $("#service-packages").DataTable({
        "processing": true,
        "serverSide": true,
        "paginate": true,
        "paging": true,
        "ordering": true,
        "filter": true,
        "ajax": "/servicepackage/GetData",
        "columns": [
            {
                "targets": -1,
                "data": null,
                "orderable": false,
                "className": 'details-control',
                "defaultContent": ''
            },
            { data: null, render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { "data": "title" },
            { "data": "from" },
            { "data": "to" },
            { "data": "extendTo" },
            { "data": "discountPrice" },
            {
                "data": null, "render": function (data, type, row, meta) {
                    var actions = '<a href="/servicepackage/Update?packageId=' + data.id + '" class="btn btn-primary mr-1 waves-effect waves-light"><i class="feather icon-edit"></i>ویرایش</a>' +
                        '<button type="button" onclick="delete_package(\'' + data.id + '\')" class="btn btn-danger mr-1 waves-effect waves-light"><i class="feather icon-x"></i>حذف</button>';
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
    $('#service-packages tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            row.child(format_details(row.data())).show();
            tr.addClass('shown');
        }
    });
});
function format_details(rowdata) {
    console.log(rowdata);
    var template = $('#item_template').html();
    var html = Mustache.render(template, rowdata);

    return html;
}

function delete_package(packageId) {
    Swal.fire({
        title: ' اطلاعات این پکیج حذف شود؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله حذف شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/servicepackage/ServicePackageDelete', { packageId: packageId }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    table.draw('page');
                }
                else
                    toastr.error(response.message);
            });
        }
    })


}

function delete_service(servicePackageItemId) {
    Swal.fire({
        title: ' اطلاعات این خدمت پکیج حذف شود؟',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله حذف شود',
        cancelButtonText: 'انصراف'
    }).then((result) => {
        if (result.value) {
            $.post('/ServicePackage/ServicePackageDeleteService', { servicePackageItemId: servicePackageItemId }, function (response) {
                if (response.isSuccess) {
                    toastr.success(response.message);
                    $('#row_service_item_' + servicePackageItemId).remove();
                }
                else
                    toastr.error(response.message);
            });
        }
    })

}