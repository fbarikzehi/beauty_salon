var empty_guid = '00000000-0000-0000-0000-000000000000';
var save_type = 'GetProfile';

$(function () {

    $("#save_btn").click(function () {
        let formdata = new FormData();
        let customer_id = $("#Id").val();
        formdata.append('id', customer_id);

        formdata.append('fileAvatar', $("#avatar_file")[0].files[0]);
        formdata.append('name', $("#Name").val());
        formdata.append('lastName', $("#LastName").val());
        formdata.append('birthdate', $("#Birthdate").val());
        formdata.append('jobStart', $("#JobStart").val());
        formdata.append('jobEnd', $("#JobEnd").val());
        formdata.append('address', $("#Address").val());
        formdata.append('user.id', $("#User_Id").val());
        formdata.append('user.username', $("#User_Username").val());
        formdata.append('user.password', $("#User_Password").val());
        formdata.append('user.confirmpassword', $("#User_ConfirmPassword").val());

        let contacts = $("input[id^=contacts_index_]");
        for (var i = 0; i < contacts.length; i++) {
            let index = $(contacts[i]).val();
            formdata.append('contacts[' + i + '].Id', $("#Contacts_" + index + "__Id").val());
            formdata.append('contacts[' + i + '].type', $("#Contacts_" + index + "__Type").val());
            formdata.append('contacts[' + i + '].value', $("#Contacts_" + index + "__Value").val());
            formdata.append('contacts[' + i + '].isActive', $("#Contacts_" + index + "__IsActive:checked").val());
        }

        formdata.append('__RequestVerificationToken', $("[name=__RequestVerificationToken]").val());

        var l = Ladda.create(this);
        l.start();
        var form = $("#customer_frm")[0];
        $.ajax({
            url: form.action,
            data: formdata,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.isSuccess) {
                    if (customer_id == undefined || customer_id == null || customer_id == '')
                        $("#customer_frm")[0].reset();
                    toastr.success(response.message);
                } else {
                    toastr.error(response.message);
                }
                l.stop();
            }
        });
    });

});

function add_contact(el) {
    let index = $(el).val();

    var data = {
        index: index
    };

    let template = $("#contact_template").html();
    let html = Mustache.render(template, data);
    $("#contacts_wrapper").append(html);

    index++;
    $(el).val(index);
}

function delete_contact(id, index) {
    $.ajax({
        url: '/customer/DeleteContact',
        data: { id: id },
        type: 'POST',
        success: function (response) {
            if (response.isSuccess) {
                toastr.success(response.message);
                $("#contact_item_" + index).remove();
                $("#contacts_index_" + index).remove();
                let contact_count = $("#contact_count").val();
                $("#contact_count").val(contact_count - 1);
                if (contact_count - 1 === 0) {
                    let template = $("#contact_template").html();
                    let html = Mustache.render(template, {
                        index: 0
                    });
                    $("#contacts_wrapper").append(html);
                }

            } else {
                toastr.error(response.message);
            }
        }
    });
}

function choose_avatar() {
    $("#avatar_file").click();
}

function change_avatar(e) {
    src = URL.createObjectURL($(e)[0].files[0]);
    document.getElementById("avatar_holder").src = src;
}

function delete_avatar() {
    document.getElementById("avatar_holder").src = '/images/avatar-placeholder.png';
}

function show_hide_switch(el) {
    if ($(el)[0].checked) {
        $("#User_Password").prop('type', 'text');
        $("#User_ConfirmPassword").prop('type', 'text');
    } else {
        $("#User_Password").prop('type', 'password');
        $("#User_ConfirmPassword").prop('type', 'password');
    }
}

