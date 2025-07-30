var BeautyManagerAside = function () {
    return {
        init: function () {
            let active_menu = localStorage.getItem('active_menu');
            if (active_menu !== null)
                get_sub(active_menu, $("#main_menu_" + active_menu))
        }
    }
}();
jQuery(document).ready(function () {
    BeautyManagerAside.init()
});

function get_sub(id, a) {
    $(".main_menu").removeClass('active');
    $(a).addClass('active');

    $.get('/Shared/GetSubmenus', { mId: id }, function (data) {
        localStorage.setItem('active_menu', data.id);

        let menu = `<div id="Beauty_aside_menu" class="beauty-aside-menu aziko" data-Beautymenu-vertical="1" data-Beautymenu-scroll="1"><ul class="beauty-menu__nav">`;
        for (var i = 0; i < data.subMenus.length; i++) {
            menu += `<li class="beauty-menu__item active" aria-haspopup="true">
                       <a href="`+ data.subMenus[i].url + `" class="beauty-menu__link">
                          <span class="beauty-menu__link-text">
                               <i class="`+ data.subMenus[i].icon + ` sub_menu_icon"></i>
                                `+ data.subMenus[i].title + `
                          </span>
                       </a>
                    </li>`;
        }
        menu += `</ul></div>`;
        $("#submenu_wrapper").empty().append(menu);
    });
}