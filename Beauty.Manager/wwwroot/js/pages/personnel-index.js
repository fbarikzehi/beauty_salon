"use strict";

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "100000",
    "timeOut": "5000",
    "extendedTimeOut": "3500",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

$(function () {
    let table = $("#Beauty_apps_user_list_datatable").BeautyDatatable({
        data: {
            type: "remote",
            source: {
                read: {
                    url: "/personnel/GetData",
                    map: function (t) {
                        var e = t;
                        return void 0 !== t.data && (e = t.data), e
                    }
                }
            },
            pageSize: 10,
            serverPaging: !0,
            serverFiltering: !0,
            serverSorting: !0
        },
        layout: {
            scroll: !1,
            footer: !1
        },
        sortable: !0,
        pagination: !0,
        search: {
            input: $("#generalSearch"),
            delay: 400
        },
        columns: [{
            field: "id",
            title: "ردیف",
            sortable: !1,
            width: 20,
            selector: {
                class: "beauty-checkbox--solid"
            },
            textAlign: "center"
        }, {
            field: "name",
            title: " ",
            sortable: !1,
            width: 40,
            template: function (t, e) {

                return `<div class="beauty-user-card-v2">
                                <div class="beauty-user-card-v2__pic">
                                <img src="` + t.avatar + `" alt="` + t.name + `">
                                </div>`;
            }
        }, {
            field: "lastName",
            title: "نام و نام خانوادگی",
            template: function (t) {
                return `<span style="width: 202px;">` + t.name + " " + t.lastName + `</span>`
            }
        }, {
            field: "code",
            title: "کد پرسنل",
            width: 80,
            sortable: !1,
            template: function (t) {
                return `<span class="number-font beauty-badge beauty-badge--lg beauty-badge--elevate beauty-badge--inline beauty-badge--focus font-weight-bold">` + t.code + `</span>`;
            }
        }, {
            field: "username",
            title: "نام کاربری",
            width: 80,
            template: function (t) {
                return `<span class="beauty-font-bolder beauty-font-danger">` + (t.username === '' ? '<i class="flaticon2-information"></i>' : t.username) + `</span>`;
            }
        }, {
            field: "mobile",
            title: "موبایل",
            sortable: !1,
        }, {
            field: "cooperationType",
            title: "نوع همکاری",
            width: 100,
            template: function (t) {
                var e = {
                    'حقوقی': {
                        title: "حقوقی",
                        class: " btn-label-brand"
                    },
                    'درصدی': {
                        title: "درصدی",
                        class: " btn-label-danger"
                    }
                };
                return '<span class="btn btn-bold btn-sm btn-font-sm ' + e[t.cooperationType].class + '">' + e[t.cooperationType].title + "</span>"
            }
        }, {
            field: "Actions",
            width: 80,
            title: "عملیات",
            sortable: !1,
            autoHide: !1,
            overflow: "visible",
            template: function (t) {
                return BeautyTableActionsBuilder.build([
                    { text: 'ویرایش', _blank: false, url: '/personnel/Modify', isVoid: false, icon: 'flaticon2-contract', classes: '', data: '', haveForm: true, hiddenValu: t.id, hiddenId: 'personnelId', hiddenName: 'personnelId' }])
            }
        }]
    });

    $("#Beauty_form_status").on("change", function () {
        table.search($(this).val().toLowerCase(), "Status")
    });

    table.on("beauty-datatable--on-check beauty-datatable--on-uncheck beauty-datatable--on-layout-updated", function (e) {
        var a = table.rows(".beauty-datatable__row--active").nodes().length;
        $("#Beauty_subheader_group_selected_rows").html(a), a > 0 ? ($("#Beauty_subheader_search").addClass("beauty-hidden"),
            $("#Beauty_subheader_group_actions").removeClass("beauty-hidden")) : ($("#Beauty_subheader_search").removeClass("beauty-hidden"),
                $("#Beauty_subheader_group_actions").addClass("beauty-hidden"))
    });

    $("#Beauty_subheader_group_actions_delete_all").on("click", function () {
        var e = table.rows(".beauty-datatable__row--active").nodes().find('.beauty-checkbox--single > [type="checkbox"]').map(function (t, e) {
            return $(e).val()
        });
        var data = {};
        for (var i = 0; i < e.length; i++) {
            data["[" + i + "].Id"] = e[i];
        }
        $.post('/personnel/deleterange', data, function (response) {
            if (response.isSuccess) {
                toastr.success(response.message);
                table.reload();
            }
            else
                toastr.error(response.message);
        })

    });

    table.on("beauty-datatable--on-layout-updated", function () { });

});