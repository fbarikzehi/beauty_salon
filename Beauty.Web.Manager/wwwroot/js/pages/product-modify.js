var empty_guid = '00000000-0000-0000-0000-000000000000';
var save_type = 'GetProfile';


$(function () {
    console.log(document);
    $("#save_btn").click(function () {
        let formdata = new FormData();
        let product_id = $("#Id").val();
        formdata.append('id', product_id);
        formdata.append('name', $("#Name").val());
        formdata.append('code', $("#Code").val());
        formdata.append('description', $("#Description").val());
        formdata.append('unitId', $("#UnitId").find('option:selected').val());
        let images = $("input[id^=image_file_]");
        let index = 0;
        for (var i = 0; i < images.length; i++) {
            if ($(images[i])[0].files[0] !== undefined) {
                formdata.append('Images[' + index + '].File', $(images[i])[0].files[0]);
                index++;
            }
        }
        formdata.append('__RequestVerificationToken', $("[name=__RequestVerificationToken]").val());

        var l = Ladda.create(this);
        l.start();
        var form = $("#product_frm")[0];
        $.ajax({
            url: form.action,
            data: formdata,
            type: 'POST',
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.isSuccess) {
                    if ($("#Id").val() === empty_guid) {
                        $("#product_frm")[0].reset();
                        $("#image_wrapper").empty();
                        $("#btn_add_image").val(0);
                    }
                    toastr.success(response.message);
                } else {
                    toastr.error(response.message);
                }
                l.stop();
            }
        });
    });

});


function add_image(el) {
    let index = $(el).val();

    var data = {
        index: index,
    };

    let template = $("#image_template").html();
    let html = Mustache.render(template, data);
    $("#image_wrapper").append(html);

    $("#image_file_" + index).click();
    index++;
    $(el).val(index);
}

function set_image(el, index) {
    src = URL.createObjectURL($(el)[0].files[0]);
    document.getElementById("image_prev_" + index).src = src;
}

function delete_image(index, id) {

    if (id === undefined) {
        $("#image_div_" + index).remove();
        var last_index = $("#btn_add_image").val();
        $("#btn_add_image").val(last_index - 1);
    }
    else {
        $.ajax({
            url: '/Product/DeleteIamge',
            data: { imageId: id },
            type: 'POST',
            success: function (response) {
                if (response.isSuccess) {
                    $("#image_div_" + index).remove();
                    var last_index = $("#btn_add_image").val();
                    $("#btn_add_image").val(last_index - 1);
                }
                else
                    toastr.error(response.message);
            }
        });
    }


}