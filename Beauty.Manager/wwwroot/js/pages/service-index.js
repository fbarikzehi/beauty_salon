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
    localStorage.setItem('table_count', 5);
    get_services(1);

    $("#service_search").on('keyup', function () {
        var value = $(this).val();
        if (value !== '')
            get_services(1, [value]);
        else
            get_services(1,[]);
    });
});

function count_change() {
    localStorage.setItem('table_count', $("#page_count").find('option:selected').val());
    get_services(1);
}
function get_services(page, searchValues = []) {
    localStorage.setItem('table_page', page);
    BeautyTemplateBuilder.build('pager_template', {
        currentCount: 0,
        totalCount: 0,
        prev: 0,
        prevDisabled: 'disabled',
        next: 2,
        nextDisabled: 'disabled',
    }, 'pager_wrapper', true);
    $.get('/service/FindAllByPage', { count: localStorage.getItem('table_count'), page: page, "searchValues[0]": searchValues[0] }, function (response) {
        console.log(response);
        BeautyTemplateBuilder.build('services_template', {
            services: response.entities.map((item, index) => {
                return {
                    currentPrice: item.currentPrice,
                    cutomerCount: item.cutomerCount,
                    durationMinutes: item.durationMinutes,
                    id: item.id,
                    activeText: item.isActive ? "غیرفعال کردن" : "فعال کردن",
                    activeBtnType: item.isActive ? "btn-warning" : "btn-brand",
                    prices: item.prices,
                    rate: item.rate,
                    title: item.title,
                    score: item.score,
                    customerMinScore: item.customerMinScore,
                    prepayment: item.prepayment,
                }
            }),
        }, 'services_wrapper', true);

        let pager_template_data = {
            currentCount: response.totalDisplayRecords,
            totalCount: response.totalRecords,
            pageCount: response.pageCount,
            prev: response.prevPage,
            prevDisabled: response.prevPage === 0 ? 'disabled' : '',
            next: response.nextPage,
            nextDisabled: response.nextPage === 0 ? 'disabled' : '',
            selectedCount: localStorage.getItem('table_count')
        };
        BeautyTemplateBuilder.build('pager_template', pager_template_data, 'pager_wrapper', true);
    });
}

function delete_service(id) {
    $.post('/service/delete', { id: id }, function (response) {
        if (response.isSuccess) {
            toastr.success(response.message);
            get_services(localStorage.getItem('table_page'));
        }
        else {
            toastr.error(response.message);
        }
    });
}

function change_active(id) {
    $.post('/service/changeactive', { id: id }, function (response) {
        if (response.isSuccess) {
            toastr.success(response.message);
            get_services(localStorage.getItem('table_page'));
        }
        else {
            toastr.error(response.message);
        }
    });
}