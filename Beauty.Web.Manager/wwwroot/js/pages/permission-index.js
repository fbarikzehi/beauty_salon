

$(function () {

    $("#RoleId").on('change', function () {
        var role_id = $(this).find('option:selected').val();
        if (role_id !== '')
            $.get('/Permission/FindAllModify', { roleId: role_id }, function (res) {
                console.log(res);
                let template = $("#permissions_template").html();
                let html = Mustache.render(template, res);
                $("#permissions_wrapper").empty().append(html);
            });
        else
            $("#permissions_wrapper").empty().append('<div class="alert alert-primary mb-2" role="alert"> برای ویرایش سطح دسترسی هر نقش ابتدا نقش را انتخاب کنید </div>');
    });

});

function update_permission(permissionId, permissionActionId, el) {
    $.ajax({
        url: '/permission/update',
        data: { permissionId: permissionId, permissionActionId: permissionActionId, roleId: $("#RoleId").find('option:selected').val(), isSelected: el.checked, selectedAll: false },
        type: 'POST',
        success: function (response) {
            if (response.isSuccess) {
                toastr.success(response.message);
            } else {
                toastr.error(response.message);
            }
        }
    });
};

function update_all_permission(el) {
    $.ajax({
        url: '/permission/update',
        data: { permissionId: 0, permissionActionId: 0, roleId: $("#RoleId").find('option:selected').val(), isSelected: el.checked, selectedAll: true },
        type: 'POST',
        success: function (response) {
            if (response.isSuccess) {
                var permission_checks = $(".permission_check");
                for (var i = 0; i < permission_checks.length; i++) {
                    $(permission_checks).prop('checked', el.checked)
                }
                toastr.success(response.message);
            } else {
                toastr.error(response.message);
            }
        }
    });
};

