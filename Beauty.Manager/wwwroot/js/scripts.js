"use strict";
var BeautyApp = (function () {
    var t = {},
        e = function (t) {
            var e = t.data("skin") ? "tooltip-" + t.data("skin") : "",
                a = "auto" == t.data("width") ? "tooltop-auto-width" : "",
                n = t.data("trigger") ? t.data("trigger") : "hover";
            t.data("placement") && t.data("placement");
            t.tooltip({
                trigger: n,
                template: '<div class="tooltip ' + e + " " + a + '" role="tooltip">                <div class="arrow"></div>                <div class="tooltip-inner"></div>            </div>',
            });
        },
        a = function () {
            $('[data-toggle="beauty-tooltip"]').each(function () {
                e($(this));
            });
        },
        n = function (t) {
            var e = t.data("skin") ? "popover-" + t.data("skin") : "",
                a = t.data("trigger") ? t.data("trigger") : "hover";
            t.popover({
                trigger: a,
                template:
                    '            <div class="popover ' + e + '" role="tooltip">                <div class="arrow"></div>                <h3 class="popover-header"></h3>                <div class="popover-body"></div>            </div>',
            });
        },
        o = function () {
            $('[data-toggle="beauty-popover"]').each(function () {
                n($(this));
            });
        },
        i = function (t, e) {
            (t = $(t)), new BeautyPortlet(t[0], e);
        },
        l = function () {
            $('[data-Beautyportlet="true"]').each(function () {
                var t = $(this);
                !0 !== t.data("data-Beautyportlet-initialized") && (i(t, {}), t.data("data-Beautyportlet-initialized", !0));
            });
        },
        r = function () {
            new Sticky('[data-sticky="true"]');
        };
    return {
        init: function (e) {
            e && e.colors && (t = e.colors), BeautyApp.initComponents();
        },
        initComponents: function () {
            $('[data-scroll="true"]').each(function () {
                var t = $(this);
                BeautyUtil.scrollInit(this, {
                    mobileNativeScroll: !0,
                    handleWindowResize: !0,
                    rememberPosition: "true" == t.data("remember-position"),
                    height: function () {
                        return BeautyUtil.isInResponsiveRange("tablet-and-mobile") && t.data("mobile-height") ? t.data("mobile-height") : t.data("height");
                    },
                });
            }),
                a(),
                o(),
                $("body").on("click", "[data-close=alert]", function () {
                    $(this).closest(".alert").hide();
                }),
                l(),
                $(".custom-file-input").on("change", function () {
                    var t = $(this).val();
                    $(this).next(".custom-file-label").addClass("selected").html(t);
                }),
                r(),
                $("body").on("show.bs.dropdown", function (t) {
                    var e = $(t.target).find("[data-attach='body']");
                    if (0 !== e.length) {
                        var a = $(t.target).find(".dropdown-menu").detach();
                        e.data("dropdown-menu", a), $("body").append(a), a.css("display", "block"), a.position({ my: "right top", at: "right bottom", of: $(t.relatedTarget) });
                    }
                }),
                $("body").on("hide.bs.dropdown", function (t) {
                    var e = $(t.target).find("[data-attach='body']");
                    if (0 !== e.length) {
                        var a = e.data("dropdown-menu");
                        $(t.target).append(a.detach()), a.hide();
                    }
                });
        },
        initTooltips: function () {
            a();
        },
        initTooltip: function (t) {
            e(t);
        },
        initPopovers: function () {
            o();
        },
        initPopover: function (t) {
            n(t);
        },
        initPortlet: function (t, e) {
            i(t, e);
        },
        initPortlets: function () {
            l();
        },
        initSticky: function () {
            r();
        },
        initAbsoluteDropdown: function (t) {
            !(function (t) {
                var e;
                t &&
                    $("body")
                        .on("show.bs.dropdown", t, function (t) {
                            (e = $(t.target).find(".dropdown-menu")), $("body").append(e.detach()), e.css("display", "block"), e.position({ my: "right top", at: "right bottom", of: $(t.relatedTarget) });
                        })
                        .on("hide.bs.dropdown", t, function (t) {
                            $(t.target).append(e.detach()), e.hide();
                        });
            })(t);
        },
        block: function (t, e) {
            var a,
                n = $(t),
                o =
                    '<div class="beauty-spinner ' +
                    ((e = $.extend(!0, { opacity: 0.05, overlayColor: "#000000", type: "", size: "", state: "brand", centerX: !0, centerY: !0, message: "", shadow: !0, width: "auto" }, e)).type ? "beauty-spinner--" + e.type : "") +
                    " " +
                    (e.state ? "beauty-spinner--" + e.state : "") +
                    " " +
                    (e.size ? "beauty-spinner--" + e.size : "") +
                    '"></div';
            if (e.message && e.message.length > 0) {
                var i = "blockui " + (!1 === e.shadow ? "blockui" : "");
                a = '<div class="' + i + '"><span>' + e.message + "</span><span>" + o + "</span></div>";
                n = document.createElement("div");
                BeautyUtil.get("body").prepend(n),
                    BeautyUtil.addClass(n, i),
                    (n.innerHTML = "<span>" + e.message + "</span><span>" + o + "</span>"),
                    (e.width = BeautyUtil.actualWidth(n) + 10),
                    BeautyUtil.remove(n),
                    "body" == t && (a = '<div class="' + i + '" style="margin-left:-' + e.width / 2 + 'px;"><span>' + e.message + "</span><span>" + o + "</span></div>");
            } else a = o;
            var l = {
                message: a,
                centerY: e.centerY,
                centerX: e.centerX,
                css: { top: "30%", left: "50%", border: "0", padding: "0", backgroundColor: "none", width: e.width },
                overlayCSS: { backgroundColor: e.overlayColor, opacity: e.opacity, cursor: "wait", zIndex: "body" == t ? 1100 : 10 },
                onUnblock: function () {
                    n && n[0] && (BeautyUtil.css(n[0], "position", ""), BeautyUtil.css(n[0], "zoom", ""));
                },
            };
            "body" == t ? ((l.css.top = "50%"), $.blockUI(l)) : (n = $(t)).block(l);
        },
        unblock: function (t) {
            t && "body" != t ? $(t).unblock() : $.unblockUI();
        },
        blockPage: function (t) {
            return BeautyApp.block("body", t);
        },
        unblockPage: function () {
            return BeautyApp.unblock("body");
        },
        progress: function (t, e) {
            var a = "beauty-spinner beauty-spinner--" + (e && e.skin ? e.skin : "light") + " beauty-spinner--" + (e && e.alignment ? e.alignment : "right") + (e && e.size ? " beauty-spinner--" + e.size : "");
            BeautyApp.unprogress(t), BeautyUtil.attr(t, "disabled", !0), $(t).addClass(a), $(t).data("progress-classes", a);
        },
        unprogress: function (t) {
            $(t).removeClass($(t).data("progress-classes")), BeautyUtil.removeAttr(t, "disabled");
        },
        getStateColor: function (e) {
            return t.state[e];
        },
        getBaseColor: function (e, a) {
            return t.base[e][a - 1];
        },
    };
})();
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyApp),
    $(document).ready(function () {
        BeautyApp.init(BeautyAppOptions);
    });
var BeautyAvatar = function (t, e) {
    var a = this,
        n = BeautyUtil.get(t);
    BeautyUtil.get("body");
    if (n) {
        var o = {},
            i = {
                construct: function (t) {
                    return BeautyUtil.data(n).has("avatar") ? (a = BeautyUtil.data(n).get("avatar")) : (i.init(t), i.build(), BeautyUtil.data(n).set("avatar", a)), a;
                },
                init: function (t) {
                    (a.element = n),
                        (a.events = []),
                        (a.input = BeautyUtil.find(n, 'input[type="file"]')),
                        (a.holder = BeautyUtil.find(n, ".beauty-avatar__holder")),
                        (a.cancel = BeautyUtil.find(n, ".beauty-avatar__cancel")),
                        (a.src = BeautyUtil.css(a.holder, "backgroundImage")),
                        (a.options = BeautyUtil.deepExtend({}, o, t));
                },
                build: function () {
                    BeautyUtil.addEvent(a.input, "change", function (t) {
                        if ((t.preventDefault(), a.input && a.input.files && a.input.files[0])) {
                            var e = new FileReader();
                            (e.onload = function (t) {
                                BeautyUtil.css(a.holder, "background-image", "url(" + t.target.result + ")");
                            }),
                                e.readAsDataURL(a.input.files[0]),
                                BeautyUtil.addClass(a.element, "beauty-avatar--changed");
                        }
                    }),
                        BeautyUtil.addEvent(a.cancel, "click", function (t) {
                            t.preventDefault(), BeautyUtil.removeClass(a.element, "beauty-avatar--changed"), BeautyUtil.css(a.holder, "background-image", a.src), (a.input.value = "");
                        });
                },
                eventTrigger: function (t) {
                    for (var e = 0; e < a.events.length; e++) {
                        var n = a.events[e];
                        if (n.name == t) {
                            if (1 != n.one) return n.handler.call(this, a);
                            if (0 == n.fired) return (a.events[e].fired = !0), n.handler.call(this, a);
                        }
                    }
                },
                addEvent: function (t, e, n) {
                    return a.events.push({ name: t, handler: e, one: n, fired: !1 }), a;
                },
            };
        return (
            (a.setDefaults = function (t) {
                o = t;
            }),
            (a.on = function (t, e) {
                return i.addEvent(t, e);
            }),
            (a.one = function (t, e) {
                return i.addEvent(t, e, !0);
            }),
            i.construct.apply(a, [e]),
            a
        );
    }
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyAvatar);
var BeautyDialog = function (t) {
    var e,
        a = this,
        n = BeautyUtil.get("body"),
        o = { placement: "top center", type: "loader", width: 100, state: "default", message: "Loading..." },
        i = {
            construct: function (t) {
                return i.init(t), a;
            },
            init: function (t) {
                (a.events = []), (a.options = BeautyUtil.deepExtend({}, o, t)), (a.state = !1);
            },
            show: function () {
                return (
                    i.eventTrigger("show"),
                    (e = document.createElement("DIV")),
                    BeautyUtil.setHTML(e, a.options.message),
                    BeautyUtil.addClass(e, "beauty-dialog beauty-dialog--shown"),
                    BeautyUtil.addClass(e, "beauty-dialog--" + a.options.state),
                    BeautyUtil.addClass(e, "beauty-dialog--" + a.options.type),
                    "top center" == a.options.placement && BeautyUtil.addClass(e, "beauty-dialog--top-center"),
                    n.appendChild(e),
                    (a.state = "shown"),
                    i.eventTrigger("shown"),
                    a
                );
            },
            hide: function () {
                return e && (i.eventTrigger("hide"), e.remove(), (a.state = "hidden"), i.eventTrigger("hidden")), a;
            },
            eventTrigger: function (t) {
                for (var e = 0; e < a.events.length; e++) {
                    var n = a.events[e];
                    if (n.name == t) {
                        if (1 != n.one) return n.handler.call(this, a);
                        if (0 == n.fired) return (a.events[e].fired = !0), n.handler.call(this, a);
                    }
                }
            },
            addEvent: function (t, e, n) {
                return a.events.push({ name: t, handler: e, one: n, fired: !1 }), a;
            },
        };
    return (
        (a.setDefaults = function (t) {
            o = t;
        }),
        (a.shown = function () {
            return "shown" == a.state;
        }),
        (a.hidden = function () {
            return "hidden" == a.state;
        }),
        (a.show = function () {
            return i.show();
        }),
        (a.hide = function () {
            return i.hide();
        }),
        (a.on = function (t, e) {
            return i.addEvent(t, e);
        }),
        (a.one = function (t, e) {
            return i.addEvent(t, e, !0);
        }),
        i.construct.apply(a, [t]),
        a
    );
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyDialog);
var BeautyHeader = function (t, e) {
    var a = this,
        n = BeautyUtil.get(t),
        o = BeautyUtil.get("body");
    if (void 0 !== n) {
        var i = { classic: !1, offset: { mobile: 150, desBeautyop: 200 }, minimize: { mobile: !1, desBeautyop: !1 } },
            l = {
                construct: function (t) {
                    return BeautyUtil.data(n).has("header") ? (a = BeautyUtil.data(n).get("header")) : (l.init(t), l.build(), BeautyUtil.data(n).set("header", a)), a;
                },
                init: function (t) {
                    (a.events = []), (a.options = BeautyUtil.deepExtend({}, i, t));
                },
                build: function () {
                    var t = 0,
                        e = !0;
                    BeautyUtil.getViewPort().height, BeautyUtil.getDocumentHeight();
                    (!1 === a.options.minimize.mobile && !1 === a.options.minimize.desBeautyop) ||
                        window.addEventListener("scroll", function () {
                            var n,
                                i,
                                r,
                                s = 0;
                            BeautyUtil.isInResponsiveRange("desBeautyop")
                                ? ((s = a.options.offset.desBeautyop), (n = a.options.minimize.desBeautyop.on), (i = a.options.minimize.desBeautyop.off))
                                : BeautyUtil.isInResponsiveRange("tablet-and-mobile") && ((s = a.options.offset.mobile), (n = a.options.minimize.mobile.on), (i = a.options.minimize.mobile.off)),
                                (r = BeautyUtil.getScrollTop()),
                                (BeautyUtil.isInResponsiveRange("tablet-and-mobile") && a.options.classic && a.options.classic.mobile) || (BeautyUtil.isInResponsiveRange("desBeautyop") && a.options.classic && a.options.classic.desBeautyop)
                                    ? r > s
                                        ? (BeautyUtil.addClass(o, n), BeautyUtil.removeClass(o, i), e && (l.eventTrigger("minimizeOn", a), (e = !1)))
                                        : (BeautyUtil.addClass(o, i), BeautyUtil.removeClass(o, n), 0 == e && (l.eventTrigger("minimizeOff", a), (e = !0)))
                                    : (r > s && t < r
                                        ? (BeautyUtil.addClass(o, n), BeautyUtil.removeClass(o, i), e && (l.eventTrigger("minimizeOn", a), (e = !1)))
                                        : (BeautyUtil.addClass(o, i), BeautyUtil.removeClass(o, n), 0 == e && (l.eventTrigger("minimizeOff", a), (e = !0))),
                                        (t = r));
                        });
                },
                eventTrigger: function (t, e) {
                    for (var n = 0; n < a.events.length; n++) {
                        var o = a.events[n];
                        if (o.name == t) {
                            if (1 != o.one) return o.handler.call(this, a, e);
                            if (0 == o.fired) return (a.events[n].fired = !0), o.handler.call(this, a, e);
                        }
                    }
                },
                addEvent: function (t, e, n) {
                    a.events.push({ name: t, handler: e, one: n, fired: !1 });
                },
            };
        return (
            (a.setDefaults = function (t) {
                i = t;
            }),
            (a.on = function (t, e) {
                return l.addEvent(t, e);
            }),
            l.construct.apply(a, [e]),
            !0,
            a
        );
    }
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyHeader);
var BeautyMenu = function (t, e) {
    var a = this,
        n = !1,
        o = BeautyUtil.get(t),
        i = BeautyUtil.get("body");
    if (o) {
        var l = { scroll: { rememberPosition: !1 }, accordion: { slideSpeed: 200, autoScroll: !1, autoScrollSpeed: 1200, expandAll: !0 }, dropdown: { timeout: 500 } },
            r = {
                construct: function (t) {
                    return BeautyUtil.data(o).has("menu") ? (a = BeautyUtil.data(o).get("menu")) : (r.init(t), r.reset(), r.build(), BeautyUtil.data(o).set("menu", a)), a;
                },
                init: function (t) {
                    (a.events = []), (a.eventHandlers = {}), (a.options = BeautyUtil.deepExtend({}, l, t)), (a.pauseDropdownHoverTime = 0), (a.uid = BeautyUtil.getUniqueID());
                },
                update: function (t) {
                    (a.options = BeautyUtil.deepExtend({}, l, t)), (a.pauseDropdownHoverTime = 0), r.reset(), (a.eventHandlers = {}), r.build(), BeautyUtil.data(o).set("menu", a);
                },
                reload: function () {
                    r.reset(), r.build(), r.resetSubmenuProps();
                },
                build: function () {
                    (a.eventHandlers.event_1 = BeautyUtil.on(o, ".beauty-menu__toggle", "click", r.handleSubmenuAccordion)),
                        ("dropdown" === r.getSubmenuMode() || r.isConditionalSubmenuDropdown()) &&
                        ((a.eventHandlers.event_2 = BeautyUtil.on(o, '[data-Beautymenu-submenu-toggle="hover"]', "mouseover", r.handleSubmenuDrodownHoverEnter)),
                            (a.eventHandlers.event_3 = BeautyUtil.on(o, '[data-Beautymenu-submenu-toggle="hover"]', "mouseout", r.handleSubmenuDrodownHoverExit)),
                            (a.eventHandlers.event_4 = BeautyUtil.on(
                                o,
                                '[data-Beautymenu-submenu-toggle="click"] > .beauty-menu__toggle, [data-Beautymenu-submenu-toggle="click"] > .beauty-menu__link .beauty-menu__toggle',
                                "click",
                                r.handleSubmenuDropdownClick
                            )),
                            (a.eventHandlers.event_5 = BeautyUtil.on(
                                o,
                                '[data-Beautymenu-submenu-toggle="tab"] > .beauty-menu__toggle, [data-Beautymenu-submenu-toggle="tab"] > .beauty-menu__link .beauty-menu__toggle',
                                "click",
                                r.handleSubmenuDropdownTabClick
                            ))),
                        (a.eventHandlers.event_6 = BeautyUtil.on(o, ".beauty-menu__item > .beauty-menu__link:not(.beauty-menu__toggle):not(.beauty-menu__link--toggle-skip)", "click", r.handleLinkClick)),
                        a.options.scroll && a.options.scroll.height && r.scrollInit();
                },
                reset: function () {
                    BeautyUtil.off(o, "click", a.eventHandlers.event_1),
                        BeautyUtil.off(o, "mouseover", a.eventHandlers.event_2),
                        BeautyUtil.off(o, "mouseout", a.eventHandlers.event_3),
                        BeautyUtil.off(o, "click", a.eventHandlers.event_4),
                        BeautyUtil.off(o, "click", a.eventHandlers.event_5),
                        BeautyUtil.off(o, "click", a.eventHandlers.event_6);
                },
                scrollInit: function () {
                    a.options.scroll && a.options.scroll.height
                        ? (BeautyUtil.scrollDestroy(o),
                            BeautyUtil.scrollInit(o, { mobileNativeScroll: !0, windowScroll: !1, resetHeightOnDestroy: !0, handleWindowResize: !0, height: a.options.scroll.height, rememberPosition: a.options.scroll.rememberPosition }))
                        : BeautyUtil.scrollDestroy(o);
                },
                scrollUpdate: function () {
                    a.options.scroll && a.options.scroll.height && BeautyUtil.scrollUpdate(o);
                },
                scrollTop: function () {
                    a.options.scroll && a.options.scroll.height && BeautyUtil.scrollTop(o);
                },
                getSubmenuMode: function (t) {
                    return BeautyUtil.isInResponsiveRange("desBeautyop")
                        ? t && BeautyUtil.hasAttr(t, "data-Beautymenu-submenu-toggle") && "hover" == BeautyUtil.attr(t, "data-Beautymenu-submenu-toggle")
                            ? "dropdown"
                            : BeautyUtil.isset(a.options.submenu, "desBeautyop.state.body")
                                ? BeautyUtil.hasClasses(i, a.options.submenu.desBeautyop.state.body)
                                    ? a.options.submenu.desBeautyop.state.mode
                                    : a.options.submenu.desBeautyop.default
                                : BeautyUtil.isset(a.options.submenu, "desBeautyop")
                                    ? a.options.submenu.desBeautyop
                                    : void 0
                        : BeautyUtil.isInResponsiveRange("tablet") && BeautyUtil.isset(a.options.submenu, "tablet")
                            ? a.options.submenu.tablet
                            : !(!BeautyUtil.isInResponsiveRange("mobile") || !BeautyUtil.isset(a.options.submenu, "mobile")) && a.options.submenu.mobile;
                },
                isConditionalSubmenuDropdown: function () {
                    return !(!BeautyUtil.isInResponsiveRange("desBeautyop") || !BeautyUtil.isset(a.options.submenu, "desBeautyop.state.body"));
                },
                resetSubmenuProps: function (t) {
                    var e = BeautyUtil.findAll(o, ".beauty-menu__submenu");
                    if (e) for (var a = 0, n = e.length; a < n; a++) BeautyUtil.css(e[0], "display", ""), BeautyUtil.css(e[0], "overflow", "");
                },
                handleSubmenuDrodownHoverEnter: function (t) {
                    if ("accordion" !== r.getSubmenuMode(this) && !1 !== a.resumeDropdownHover()) {
                        "1" == this.getAttribute("data-hover") && (this.removeAttribute("data-hover"), clearTimeout(this.getAttribute("data-timeout")), this.removeAttribute("data-timeout")), r.showSubmenuDropdown(this);
                    }
                },
                handleSubmenuDrodownHoverExit: function (t) {
                    if (!1 !== a.resumeDropdownHover() && "accordion" !== r.getSubmenuMode(this)) {
                        var e = this,
                            n = a.options.dropdown.timeout,
                            o = setTimeout(function () {
                                "1" == e.getAttribute("data-hover") && r.hideSubmenuDropdown(e, !0);
                            }, n);
                        e.setAttribute("data-hover", "1"), e.setAttribute("data-timeout", o);
                    }
                },
                handleSubmenuDropdownClick: function (t) {
                    if ("accordion" !== r.getSubmenuMode(this)) {
                        var e = this.closest(".beauty-menu__item");
                        "accordion" != e.getAttribute("data-Beautymenu-submenu-mode") &&
                            (!1 === BeautyUtil.hasClass(e, "beauty-menu__item--hover")
                                ? (BeautyUtil.addClass(e, "beauty-menu__item--open-dropdown"), r.showSubmenuDropdown(e))
                                : (BeautyUtil.removeClass(e, "beauty-menu__item--open-dropdown"), r.hideSubmenuDropdown(e, !0)),
                                t.preventDefault());
                    }
                },
                handleSubmenuDropdownTabClick: function (t) {
                    if ("accordion" !== r.getSubmenuMode(this)) {
                        var e = this.closest(".beauty-menu__item");
                        "accordion" != e.getAttribute("data-Beautymenu-submenu-mode") &&
                            (0 == BeautyUtil.hasClass(e, "beauty-menu__item--hover") && (BeautyUtil.addClass(e, "beauty-menu__item--open-dropdown"), r.showSubmenuDropdown(e)), t.preventDefault());
                    }
                },
                handleLinkClick: function (t) {
                    var e = this.closest(".beauty-menu__item.beauty-menu__item--submenu");
                    !1 !== r.eventTrigger("linkClick", this, t) && e && "dropdown" === r.getSubmenuMode(e) && r.hideSubmenuDropdowns();
                },
                handleSubmenuDropdownClose: function (t, e) {
                    if ("accordion" !== r.getSubmenuMode(e)) {
                        var a = o.querySelectorAll(".beauty-menu__item.beauty-menu__item--submenu.beauty-menu__item--hover:not(.beauty-menu__item--tabs)");
                        if (a.length > 0 && !1 === BeautyUtil.hasClass(e, "beauty-menu__toggle") && 0 === e.querySelectorAll(".beauty-menu__toggle").length) for (var n = 0, i = a.length; n < i; n++) r.hideSubmenuDropdown(a[0], !0);
                    }
                },
                handleSubmenuAccordion: function (t, e) {
                    var n,
                        o = e || this;
                    if ("dropdown" === r.getSubmenuMode(e) && (n = o.closest(".beauty-menu__item")) && "accordion" != n.getAttribute("data-Beautymenu-submenu-mode")) t.preventDefault();
                    else {
                        var i = o.closest(".beauty-menu__item"),
                            l = BeautyUtil.child(i, ".beauty-menu__submenu, .beauty-menu__inner");
                        if (!BeautyUtil.hasClass(o.closest(".beauty-menu__item"), "beauty-menu__item--open-always") && i && l) {
                            t.preventDefault();
                            var s = a.options.accordion.slideSpeed;
                            if (!1 === BeautyUtil.hasClass(i, "beauty-menu__item--open")) {
                                if (!1 === a.options.accordion.expandAll) {
                                    var d = o.closest(".beauty-menu__nav, .beauty-menu__subnav"),
                                        c = BeautyUtil.children(d, ".beauty-menu__item.beauty-menu__item--open.beauty-menu__item--submenu:not(.beauty-menu__item--here):not(.beauty-menu__item--open-always)");
                                    if (d && c)
                                        for (var u = 0, p = c.length; u < p; u++) {
                                            var f = c[0],
                                                g = BeautyUtil.child(f, ".beauty-menu__submenu");
                                            g &&
                                                BeautyUtil.slideUp(g, s, function () {
                                                    r.scrollUpdate(), BeautyUtil.removeClass(f, "beauty-menu__item--open");
                                                });
                                        }
                                }
                                BeautyUtil.slideDown(l, s, function () {
                                    r.scrollToItem(o), r.scrollUpdate(), r.eventTrigger("submenuToggle", l, t);
                                }),
                                    BeautyUtil.addClass(i, "beauty-menu__item--open");
                            } else
                                BeautyUtil.slideUp(l, s, function () {
                                    r.scrollToItem(o), r.eventTrigger("submenuToggle", l, t);
                                }),
                                    BeautyUtil.removeClass(i, "beauty-menu__item--open");
                        }
                    }
                },
                scrollToItem: function (t) {
                    BeautyUtil.isInResponsiveRange("desBeautyop") && a.options.accordion.autoScroll && "1" !== o.getAttribute("data-Beautymenu-scroll") && BeautyUtil.scrollTo(t, a.options.accordion.autoScrollSpeed);
                },
                hideSubmenuDropdown: function (t, e) {
                    e && (BeautyUtil.removeClass(t, "beauty-menu__item--hover"), BeautyUtil.removeClass(t, "beauty-menu__item--active-tab")),
                        t.removeAttribute("data-hover"),
                        t.getAttribute("data-Beautymenu-dropdown-toggle-class") && BeautyUtil.removeClass(i, t.getAttribute("data-Beautymenu-dropdown-toggle-class"));
                    var a = t.getAttribute("data-timeout");
                    t.removeAttribute("data-timeout"), clearTimeout(a);
                },
                hideSubmenuDropdowns: function () {
                    var t;
                    if ((t = o.querySelectorAll('.beauty-menu__item--submenu.beauty-menu__item--hover:not(.beauty-menu__item--tabs):not([data-Beautymenu-submenu-toggle="tab"])')))
                        for (var e = 0, a = t.length; e < a; e++) r.hideSubmenuDropdown(t[e], !0);
                },
                showSubmenuDropdown: function (t) {
                    var e = o.querySelectorAll(".beauty-menu__item--submenu.beauty-menu__item--hover, .beauty-menu__item--submenu.beauty-menu__item--active-tab");
                    if (e)
                        for (var a = 0, n = e.length; a < n; a++) {
                            var l = e[a];
                            t !== l && !1 === l.contains(t) && !1 === t.contains(l) && r.hideSubmenuDropdown(l, !0);
                        }
                    BeautyUtil.addClass(t, "beauty-menu__item--hover"), t.getAttribute("data-Beautymenu-dropdown-toggle-class") && BeautyUtil.addClass(i, t.getAttribute("data-Beautymenu-dropdown-toggle-class"));
                },
                createSubmenuDropdownClickDropoff: function (t) {
                    var e,
                        a = (e = BeautyUtil.child(t, ".beauty-menu__submenu") ? BeautyUtil.css(e, "z-index") : 0) - 1,
                        n = document.createElement('<div class="beauty-menu__dropoff" style="background: transparent; position: fixed; top: 0; bottom: 0; left: 0; right: 0; z-index: ' + a + '"></div>');
                    i.appendChild(n),
                        BeautyUtil.addEvent(n, "click", function (e) {
                            e.stopPropagation(), e.preventDefault(), BeautyUtil.remove(this), r.hideSubmenuDropdown(t, !0);
                        });
                },
                pauseDropdownHover: function (t) {
                    var e = new Date();
                    a.pauseDropdownHoverTime = e.getTime() + t;
                },
                resumeDropdownHover: function () {
                    return new Date().getTime() > a.pauseDropdownHoverTime;
                },
                resetActiveItem: function (t) {
                    for (var e, n, i = 0, l = (e = o.querySelectorAll(".beauty-menu__item--active")).length; i < l; i++) {
                        var r = e[0];
                        BeautyUtil.removeClass(r, "beauty-menu__item--active"), BeautyUtil.hide(BeautyUtil.child(r, ".beauty-menu__submenu"));
                        for (var s = 0, d = (n = BeautyUtil.parents(r, ".beauty-menu__item--submenu") || []).length; s < d; s++) {
                            var c = n[i];
                            BeautyUtil.removeClass(c, "beauty-menu__item--open"), BeautyUtil.hide(BeautyUtil.child(c, ".beauty-menu__submenu"));
                        }
                    }
                    if (!1 === a.options.accordion.expandAll && (e = o.querySelectorAll(".beauty-menu__item--open"))) for (i = 0, l = e.length; i < l; i++) BeautyUtil.removeClass(n[0], "beauty-menu__item--open");
                },
                setActiveItem: function (t) {
                    r.resetActiveItem();
                    for (var e = BeautyUtil.parents(t, ".beauty-menu__item--submenu") || [], a = 0, n = e.length; a < n; a++) BeautyUtil.addClass(BeautyUtil.get(e[a]), "beauty-menu__item--open");
                    BeautyUtil.addClass(BeautyUtil.get(t), "beauty-menu__item--active");
                },
                getBreadcrumbs: function (t) {
                    var e,
                        a = [],
                        n = BeautyUtil.child(t, ".beauty-menu__link");
                    a.push({ text: (e = BeautyUtil.child(n, ".beauty-menu__link-text") ? e.innerHTML : ""), title: n.getAttribute("title"), href: n.getAttribute("href") });
                    for (var o = BeautyUtil.parents(t, ".beauty-menu__item--submenu"), i = 0, l = o.length; i < l; i++) {
                        var r = BeautyUtil.child(o[i], ".beauty-menu__link");
                        a.push({ text: (e = BeautyUtil.child(r, ".beauty-menu__link-text") ? e.innerHTML : ""), title: r.getAttribute("title"), href: r.getAttribute("href") });
                    }
                    return a.reverse();
                },
                getPageTitle: function (t) {
                    var e;
                    return BeautyUtil.child(t, ".beauty-menu__link-text") ? e.innerHTML : "";
                },
                eventTrigger: function (t, e, n) {
                    for (var o = 0; o < a.events.length; o++) {
                        var i = a.events[o];
                        if (i.name == t) {
                            if (1 != i.one) return i.handler.call(this, e, n);
                            if (0 == i.fired) return (a.events[o].fired = !0), i.handler.call(this, e, n);
                        }
                    }
                },
                addEvent: function (t, e, n) {
                    a.events.push({ name: t, handler: e, one: n, fired: !1 });
                },
                removeEvent: function (t) {
                    a.events[t] && delete a.events[t];
                },
            };
        return (
            (a.setDefaults = function (t) {
                l = t;
            }),
            (a.scrollUpdate = function () {
                return r.scrollUpdate();
            }),
            (a.scrollReInit = function () {
                return r.scrollInit();
            }),
            (a.scrollTop = function () {
                return r.scrollTop();
            }),
            (a.setActiveItem = function (t) {
                return r.setActiveItem(t);
            }),
            (a.reload = function () {
                return r.reload();
            }),
            (a.update = function (t) {
                return r.update(t);
            }),
            (a.getBreadcrumbs = function (t) {
                return r.getBreadcrumbs(t);
            }),
            (a.getPageTitle = function (t) {
                return r.getPageTitle(t);
            }),
            (a.getSubmenuMode = function (t) {
                return r.getSubmenuMode(t);
            }),
            (a.hideDropdown = function (t) {
                r.hideSubmenuDropdown(t, !0);
            }),
            (a.hideDropdowns = function () {
                r.hideSubmenuDropdowns();
            }),
            (a.pauseDropdownHover = function (t) {
                r.pauseDropdownHover(t);
            }),
            (a.resumeDropdownHover = function () {
                return r.resumeDropdownHover();
            }),
            (a.on = function (t, e) {
                return r.addEvent(t, e);
            }),
            (a.off = function (t) {
                return r.removeEvent(t);
            }),
            (a.one = function (t, e) {
                return r.addEvent(t, e, !0);
            }),
            r.construct.apply(a, [e]),
            BeautyUtil.addResizeHandler(function () {
                n && a.reload();
            }),
            (n = !0),
            a
        );
    }
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyMenu),
    document.addEventListener("click", function (t) {
        var e;
        if ((e = BeautyUtil.get("body").querySelectorAll('.beauty-menu__nav .beauty-menu__item.beauty-menu__item--submenu.beauty-menu__item--hover:not(.beauty-menu__item--tabs)[data-Beautymenu-submenu-toggle="click"]')))
            for (var a = 0, n = e.length; a < n; a++) {
                var o = e[a].closest(".beauty-menu__nav").parentNode;
                if (o) {
                    var i = BeautyUtil.data(o).get("menu");
                    if (!i) break;
                    if (!i || "dropdown" !== i.getSubmenuMode()) break;
                    t.target !== o && !1 === o.contains(t.target) && i.hideDropdowns();
                }
            }
    });
var BeautyOffcanvas = function (t, e) {
    var a = this,
        n = BeautyUtil.get(t),
        o = BeautyUtil.get("body");
    if (n) {
        var i = {},
            l = {
                construct: function (t) {
                    return BeautyUtil.data(n).has("offcanvas") ? (a = BeautyUtil.data(n).get("offcanvas")) : (l.init(t), l.build(), BeautyUtil.data(n).set("offcanvas", a)), a;
                },
                init: function (t) {
                    (a.events = []),
                        (a.options = BeautyUtil.deepExtend({}, i, t)),
                        a.overlay,
                        (a.classBase = a.options.baseClass),
                        (a.classShown = a.classBase + "--on"),
                        (a.classOverlay = a.classBase + "-overlay"),
                        (a.state = BeautyUtil.hasClass(n, a.classShown) ? "shown" : "hidden");
                },
                build: function () {
                    if (a.options.toggleBy)
                        if ("string" == typeof a.options.toggleBy)
                            BeautyUtil.addEvent(a.options.toggleBy, "click", function (t) {
                                t.preventDefault(), l.toggle();
                            });
                        else if (a.options.toggleBy && a.options.toggleBy[0])
                            if (a.options.toggleBy[0].target)
                                for (var t in a.options.toggleBy)
                                    BeautyUtil.addEvent(a.options.toggleBy[t].target, "click", function (t) {
                                        t.preventDefault(), l.toggle();
                                    });
                            else
                                for (var t in a.options.toggleBy)
                                    BeautyUtil.addEvent(a.options.toggleBy[t], "click", function (t) {
                                        t.preventDefault(), l.toggle();
                                    });
                        else
                            a.options.toggleBy &&
                                a.options.toggleBy.target &&
                                BeautyUtil.addEvent(a.options.toggleBy.target, "click", function (t) {
                                    t.preventDefault(), l.toggle();
                                });
                    var e = BeautyUtil.get(a.options.closeBy);
                    e &&
                        BeautyUtil.addEvent(e, "click", function (t) {
                            t.preventDefault(), l.hide();
                        });
                },
                isShown: function (t) {
                    return "shown" == a.state;
                },
                toggle: function () {
                    l.eventTrigger("toggle"), "shown" == a.state ? l.hide(this) : l.show(this);
                },
                show: function (t) {
                    "shown" != a.state &&
                        (l.eventTrigger("beforeShow"),
                            l.togglerClass(t, "show"),
                            BeautyUtil.addClass(o, a.classShown),
                            BeautyUtil.addClass(n, a.classShown),
                            (a.state = "shown"),
                            a.options.overlay &&
                            ((a.overlay = BeautyUtil.insertAfter(document.createElement("DIV"), n)),
                                BeautyUtil.addClass(a.overlay, a.classOverlay),
                                BeautyUtil.addEvent(a.overlay, "click", function (e) {
                                    e.stopPropagation(), e.preventDefault(), l.hide(t);
                                })),
                            l.eventTrigger("afterShow"));
                },
                hide: function (t) {
                    "hidden" != a.state &&
                        (l.eventTrigger("beforeHide"),
                            l.togglerClass(t, "hide"),
                            BeautyUtil.removeClass(o, a.classShown),
                            BeautyUtil.removeClass(n, a.classShown),
                            (a.state = "hidden"),
                            a.options.overlay && a.overlay && BeautyUtil.remove(a.overlay),
                            l.eventTrigger("afterHide"));
                },
                togglerClass: function (t, e) {
                    var n,
                        o = BeautyUtil.attr(t, "id");
                    if (a.options.toggleBy && a.options.toggleBy[0] && a.options.toggleBy[0].target) for (var i in a.options.toggleBy) a.options.toggleBy[i].target === o && (n = a.options.toggleBy[i]);
                    else a.options.toggleBy && a.options.toggleBy.target && (n = a.options.toggleBy);
                    if (n) {
                        var l = BeautyUtil.get(n.target);
                        "show" === e && BeautyUtil.addClass(l, n.state), "hide" === e && BeautyUtil.removeClass(l, n.state);
                    }
                },
                eventTrigger: function (t, e) {
                    for (var n = 0; n < a.events.length; n++) {
                        var o = a.events[n];
                        if (o.name == t) {
                            if (1 != o.one) return o.handler.call(this, a, e);
                            if (0 == o.fired) return (a.events[n].fired = !0), o.handler.call(this, a, e);
                        }
                    }
                },
                addEvent: function (t, e, n) {
                    a.events.push({ name: t, handler: e, one: n, fired: !1 });
                },
            };
        return (
            (a.setDefaults = function (t) {
                i = t;
            }),
            (a.isShown = function () {
                return l.isShown();
            }),
            (a.hide = function () {
                return l.hide();
            }),
            (a.show = function () {
                return l.show();
            }),
            (a.on = function (t, e) {
                return l.addEvent(t, e);
            }),
            (a.one = function (t, e) {
                return l.addEvent(t, e, !0);
            }),
            l.construct.apply(a, [e]),
            !0,
            a
        );
    }
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyOffcanvas);
var BeautyPortlet = function (t, e) {
    var a = this,
        n = BeautyUtil.get(t),
        o = BeautyUtil.get("body");
    if (n) {
        var i = {
            bodyToggleSpeed: 400,
            tooltips: !0,
            tools: { toggle: { collapse: "Collapse", expand: "Expand" }, reload: "Reload", remove: "Remove", fullscreen: { on: "Fullscreen", off: "Exit Fullscreen" } },
            sticky: { offset: 300, zIndex: 101 },
        },
            l = {
                construct: function (t) {
                    return BeautyUtil.data(n).has("portlet") ? (a = BeautyUtil.data(n).get("portlet")) : (l.init(t), l.build(), BeautyUtil.data(n).set("portlet", a)), a;
                },
                init: function (t) {
                    (a.element = n),
                        (a.events = []),
                        (a.options = BeautyUtil.deepExtend({}, i, t)),
                        (a.head = BeautyUtil.child(n, ".beauty-portlet__head")),
                        (a.foot = BeautyUtil.child(n, ".beauty-portlet__foot")),
                        BeautyUtil.child(n, ".beauty-portlet__body") ? (a.body = BeautyUtil.child(n, ".beauty-portlet__body")) : BeautyUtil.child(n, ".beauty-form") && (a.body = BeautyUtil.child(n, ".beauty-form"));
                },
                build: function () {
                    var t = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=remove]");
                    t &&
                        BeautyUtil.addEvent(t, "click", function (t) {
                            t.preventDefault(), l.remove();
                        });
                    var e = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=reload]");
                    e &&
                        BeautyUtil.addEvent(e, "click", function (t) {
                            t.preventDefault(), l.reload();
                        });
                    var n = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=toggle]");
                    n &&
                        BeautyUtil.addEvent(n, "click", function (t) {
                            t.preventDefault(), l.toggle();
                        });
                    var o = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=fullscreen]");
                    o &&
                        BeautyUtil.addEvent(o, "click", function (t) {
                            t.preventDefault(), l.fullscreen();
                        }),
                        l.setupTooltips();
                },
                initSticky: function () {
                    a.options.sticky.offset;
                    a.head && window.addEventListener("scroll", l.onScrollSticky);
                },
                onScrollSticky: function (t) {
                    var e = a.options.sticky.offset;
                    if (!isNaN(e)) {
                        var i = BeautyUtil.getScrollTop();
                        i >= e && !1 === BeautyUtil.hasClass(o, "beauty-portlet--sticky")
                            ? (l.eventTrigger("stickyOn"), BeautyUtil.addClass(o, "beauty-portlet--sticky"), BeautyUtil.addClass(n, "beauty-portlet--sticky"), l.updateSticky())
                            : 1.5 * i <= e &&
                            BeautyUtil.hasClass(o, "beauty-portlet--sticky") &&
                            (l.eventTrigger("stickyOff"), BeautyUtil.removeClass(o, "beauty-portlet--sticky"), BeautyUtil.removeClass(n, "beauty-portlet--sticky"), l.resetSticky());
                    }
                },
                updateSticky: function () {
                    var t, e, n;
                    a.head &&
                        BeautyUtil.hasClass(o, "beauty-portlet--sticky") &&
                        ((t = a.options.sticky.position.top instanceof Function ? parseInt(a.options.sticky.position.top.call(this, a)) : parseInt(a.options.sticky.position.top)),
                            (e = a.options.sticky.position.left instanceof Function ? parseInt(a.options.sticky.position.left.call(this, a)) : parseInt(a.options.sticky.position.left)),
                            (n = a.options.sticky.position.right instanceof Function ? parseInt(a.options.sticky.position.right.call(this, a)) : parseInt(a.options.sticky.position.right)),
                            BeautyUtil.css(a.head, "z-index", a.options.sticky.zIndex),
                            BeautyUtil.css(a.head, "top", t + "px"),
                            BeautyUtil.css(a.head, "left", e + "px"),
                            BeautyUtil.css(a.head, "right", n + "px"));
                },
                resetSticky: function () {
                    a.head && !1 === BeautyUtil.hasClass(o, "beauty-portlet--sticky") && (BeautyUtil.css(a.head, "z-index", ""), BeautyUtil.css(a.head, "top", ""), BeautyUtil.css(a.head, "left", ""), BeautyUtil.css(a.head, "right", ""));
                },
                remove: function () {
                    !1 !== l.eventTrigger("beforeRemove") &&
                        (BeautyUtil.hasClass(o, "beauty-portlet--fullscreen") && BeautyUtil.hasClass(n, "beauty-portlet--fullscreen") && l.fullscreen("off"), l.removeTooltips(), BeautyUtil.remove(n), l.eventTrigger("afterRemove"));
                },
                setContent: function (t) {
                    t && (a.body.innerHTML = t);
                },
                getBody: function () {
                    return a.body;
                },
                getSelf: function () {
                    return n;
                },
                setupTooltips: function () {
                    if (a.options.tooltips) {
                        var t = BeautyUtil.hasClass(n, "beauty-portlet--collapse") || BeautyUtil.hasClass(n, "beauty-portlet--collapsed"),
                            e = BeautyUtil.hasClass(o, "beauty-portlet--fullscreen") && BeautyUtil.hasClass(n, "beauty-portlet--fullscreen"),
                            i = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=remove]");
                        if (i) {
                            var l = e ? "bottom" : "top",
                                r = new Tooltip(i, {
                                    title: a.options.tools.remove,
                                    placement: l,
                                    offset: e ? "0,10px,0,0" : "0,5px",
                                    trigger: "hover",
                                    template:
                                        '<div class="tooltip tooltip-portlet tooltip bs-tooltip-' +
                                        l +
                                        '" role="tooltip">                            <div class="tooltip-arrow arrow"></div>                            <div class="tooltip-inner"></div>                        </div>',
                                });
                            BeautyUtil.data(i).set("tooltip", r);
                        }
                        var s = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=reload]");
                        if (s) {
                            (l = e ? "bottom" : "top"),
                                (r = new Tooltip(s, {
                                    title: a.options.tools.reload,
                                    placement: l,
                                    offset: e ? "0,10px,0,0" : "0,5px",
                                    trigger: "hover",
                                    template:
                                        '<div class="tooltip tooltip-portlet tooltip bs-tooltip-' +
                                        l +
                                        '" role="tooltip">                            <div class="tooltip-arrow arrow"></div>                            <div class="tooltip-inner"></div>                        </div>',
                                }));
                            BeautyUtil.data(s).set("tooltip", r);
                        }
                        var d = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=toggle]");
                        if (d) {
                            (l = e ? "bottom" : "top"),
                                (r = new Tooltip(d, {
                                    title: t ? a.options.tools.toggle.expand : a.options.tools.toggle.collapse,
                                    placement: l,
                                    offset: e ? "0,10px,0,0" : "0,5px",
                                    trigger: "hover",
                                    template:
                                        '<div class="tooltip tooltip-portlet tooltip bs-tooltip-' +
                                        l +
                                        '" role="tooltip">                            <div class="tooltip-arrow arrow"></div>                            <div class="tooltip-inner"></div>                        </div>',
                                }));
                            BeautyUtil.data(d).set("tooltip", r);
                        }
                        var c = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=fullscreen]");
                        if (c) {
                            (l = e ? "bottom" : "top"),
                                (r = new Tooltip(c, {
                                    title: e ? a.options.tools.fullscreen.off : a.options.tools.fullscreen.on,
                                    placement: l,
                                    offset: e ? "0,10px,0,0" : "0,5px",
                                    trigger: "hover",
                                    template:
                                        '<div class="tooltip tooltip-portlet tooltip bs-tooltip-' +
                                        l +
                                        '" role="tooltip">                            <div class="tooltip-arrow arrow"></div>                            <div class="tooltip-inner"></div>                        </div>',
                                }));
                            BeautyUtil.data(c).set("tooltip", r);
                        }
                    }
                },
                removeTooltips: function () {
                    if (a.options.tooltips) {
                        var t = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=remove]");
                        t && BeautyUtil.data(t).has("tooltip") && BeautyUtil.data(t).get("tooltip").dispose();
                        var e = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=reload]");
                        e && BeautyUtil.data(e).has("tooltip") && BeautyUtil.data(e).get("tooltip").dispose();
                        var n = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=toggle]");
                        n && BeautyUtil.data(n).has("tooltip") && BeautyUtil.data(n).get("tooltip").dispose();
                        var o = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=fullscreen]");
                        o && BeautyUtil.data(o).has("tooltip") && BeautyUtil.data(o).get("tooltip").dispose();
                    }
                },
                reload: function () {
                    l.eventTrigger("reload");
                },
                toggle: function () {
                    BeautyUtil.hasClass(n, "beauty-portlet--collapse") || BeautyUtil.hasClass(n, "beauty-portlet--collapsed") ? l.expand() : l.collapse();
                },
                collapse: function () {
                    if (!1 !== l.eventTrigger("beforeCollapse")) {
                        BeautyUtil.slideUp(a.body, a.options.bodyToggleSpeed, function () {
                            l.eventTrigger("afterCollapse");
                        }),
                            BeautyUtil.addClass(n, "beauty-portlet--collapse");
                        var t = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=toggle]");
                        t && BeautyUtil.data(t).has("tooltip") && BeautyUtil.data(t).get("tooltip").updateTitleContent(a.options.tools.toggle.expand);
                    }
                },
                expand: function () {
                    if (!1 !== l.eventTrigger("beforeExpand")) {
                        BeautyUtil.slideDown(a.body, a.options.bodyToggleSpeed, function () {
                            l.eventTrigger("afterExpand");
                        }),
                            BeautyUtil.removeClass(n, "beauty-portlet--collapse"),
                            BeautyUtil.removeClass(n, "beauty-portlet--collapsed");
                        var t = BeautyUtil.find(a.head, "[data-Beautyportlet-tool=toggle]");
                        t && BeautyUtil.data(t).has("tooltip") && BeautyUtil.data(t).get("tooltip").updateTitleContent(a.options.tools.toggle.collapse);
                    }
                },
                fullscreen: function (t) {
                    if ("off" === t || (BeautyUtil.hasClass(o, "beauty-portlet--fullscreen") && BeautyUtil.hasClass(n, "beauty-portlet--fullscreen")))
                        l.eventTrigger("beforeFullscreenOff"),
                            BeautyUtil.removeClass(o, "beauty-portlet--fullscreen"),
                            BeautyUtil.removeClass(n, "beauty-portlet--fullscreen"),
                            l.removeTooltips(),
                            l.setupTooltips(),
                            a.foot && (BeautyUtil.css(a.body, "margin-bottom", ""), BeautyUtil.css(a.foot, "margin-top", "")),
                            l.eventTrigger("afterFullscreenOff");
                    else {
                        if ((l.eventTrigger("beforeFullscreenOn"), BeautyUtil.addClass(n, "beauty-portlet--fullscreen"), BeautyUtil.addClass(o, "beauty-portlet--fullscreen"), l.removeTooltips(), l.setupTooltips(), a.foot)) {
                            var e = parseInt(BeautyUtil.css(a.foot, "height")),
                                i = parseInt(BeautyUtil.css(a.foot, "height")) + parseInt(BeautyUtil.css(a.head, "height"));
                            BeautyUtil.css(a.body, "margin-bottom", e + "px"), BeautyUtil.css(a.foot, "margin-top", "-" + i + "px");
                        }
                        l.eventTrigger("afterFullscreenOn");
                    }
                },
                eventTrigger: function (t) {
                    for (var e = 0; e < a.events.length; e++) {
                        var n = a.events[e];
                        if (n.name == t) {
                            if (1 != n.one) return n.handler.call(this, a);
                            if (0 == n.fired) return (a.events[e].fired = !0), n.handler.call(this, a);
                        }
                    }
                },
                addEvent: function (t, e, n) {
                    return a.events.push({ name: t, handler: e, one: n, fired: !1 }), a;
                },
            };
        return (
            (a.setDefaults = function (t) {
                i = t;
            }),
            (a.remove = function () {
                return l.remove(html);
            }),
            (a.initSticky = function () {
                return l.initSticky();
            }),
            (a.updateSticky = function () {
                return l.updateSticky();
            }),
            (a.resetSticky = function () {
                return l.resetSticky();
            }),
            (a.destroySticky = function () {
                l.resetSticky(), window.removeEventListener("scroll", l.onScrollSticky);
            }),
            (a.reload = function () {
                return l.reload();
            }),
            (a.setContent = function (t) {
                return l.setContent(t);
            }),
            (a.toggle = function () {
                return l.toggle();
            }),
            (a.collapse = function () {
                return l.collapse();
            }),
            (a.expand = function () {
                return l.expand();
            }),
            (a.fullscreen = function () {
                return l.fullscreen("on");
            }),
            (a.unFullscreen = function () {
                return l.fullscreen("off");
            }),
            (a.getBody = function () {
                return l.getBody();
            }),
            (a.getSelf = function () {
                return l.getSelf();
            }),
            (a.on = function (t, e) {
                return l.addEvent(t, e);
            }),
            (a.one = function (t, e) {
                return l.addEvent(t, e, !0);
            }),
            l.construct.apply(a, [e]),
            a
        );
    }
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyPortlet);
var BeautyScrolltop = function (t, e) {
    var a = this,
        n = BeautyUtil.get(t),
        o = BeautyUtil.get("body");
    if (n) {
        var i = { offset: 300, speed: 600, toggleClass: "beauty-scrolltop--on" },
            l = {
                construct: function (t) {
                    return BeautyUtil.data(n).has("scrolltop") ? (a = BeautyUtil.data(n).get("scrolltop")) : (l.init(t), l.build(), BeautyUtil.data(n).set("scrolltop", a)), a;
                },
                init: function (t) {
                    (a.events = []), (a.options = BeautyUtil.deepExtend({}, i, t));
                },
                build: function () {
                    navigator.userAgent.match(/iPhone|iPad|iPod/i)
                        ? (window.addEventListener("touchend", function () {
                            l.handle();
                        }),
                            window.addEventListener("touchcancel", function () {
                                l.handle();
                            }),
                            window.addEventListener("touchleave", function () {
                                l.handle();
                            }))
                        : window.addEventListener("scroll", function () {
                            l.handle();
                        }),
                        BeautyUtil.addEvent(n, "click", l.scroll);
                },
                handle: function () {
                    window.pageYOffset > a.options.offset ? BeautyUtil.addClass(o, a.options.toggleClass) : BeautyUtil.removeClass(o, a.options.toggleClass);
                },
                scroll: function (t) {
                    t.preventDefault(), BeautyUtil.scrollTop(0, a.options.speed);
                },
                eventTrigger: function (t, e) {
                    for (var n = 0; n < a.events.length; n++) {
                        var o = a.events[n];
                        if (o.name == t) {
                            if (1 != o.one) return o.handler.call(this, a, e);
                            if (0 == o.fired) return (a.events[n].fired = !0), o.handler.call(this, a, e);
                        }
                    }
                },
                addEvent: function (t, e, n) {
                    a.events.push({ name: t, handler: e, one: n, fired: !1 });
                },
            };
        return (
            (a.setDefaults = function (t) {
                i = t;
            }),
            (a.on = function (t, e) {
                return l.addEvent(t, e);
            }),
            (a.one = function (t, e) {
                return l.addEvent(t, e, !0);
            }),
            l.construct.apply(a, [e]),
            !0,
            a
        );
    }
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyScrolltop);
var BeautyToggle = function (t, e) {
    var a = this,
        n = BeautyUtil.get(t);
    BeautyUtil.get("body");
    if (n) {
        var o = { togglerState: "", targetState: "" },
            i = {
                construct: function (t) {
                    return BeautyUtil.data(n).has("toggle") ? (a = BeautyUtil.data(n).get("toggle")) : (i.init(t), i.build(), BeautyUtil.data(n).set("toggle", a)), a;
                },
                init: function (t) {
                    (a.element = n),
                        (a.events = []),
                        (a.options = BeautyUtil.deepExtend({}, o, t)),
                        (a.target = BeautyUtil.get(a.options.target)),
                        (a.targetState = a.options.targetState),
                        (a.togglerState = a.options.togglerState),
                        (a.state = BeautyUtil.hasClasses(a.target, a.targetState) ? "on" : "off");
                },
                build: function () {
                    BeautyUtil.addEvent(n, "mouseup", i.toggle);
                },
                toggle: function (t) {
                    return i.eventTrigger("beforeToggle"), "off" == a.state ? i.toggleOn() : i.toggleOff(), i.eventTrigger("afterToggle"), t.preventDefault(), a;
                },
                toggleOn: function () {
                    return i.eventTrigger("beforeOn"), BeautyUtil.addClass(a.target, a.targetState), a.togglerState && BeautyUtil.addClass(n, a.togglerState), (a.state = "on"), i.eventTrigger("afterOn"), i.eventTrigger("toggle"), a;
                },
                toggleOff: function () {
                    return (
                        i.eventTrigger("beforeOff"), BeautyUtil.removeClass(a.target, a.targetState), a.togglerState && BeautyUtil.removeClass(n, a.togglerState), (a.state = "off"), i.eventTrigger("afterOff"), i.eventTrigger("toggle"), a
                    );
                },
                eventTrigger: function (t) {
                    for (var e = 0; e < a.events.length; e++) {
                        var n = a.events[e];
                        if (n.name == t) {
                            if (1 != n.one) return n.handler.call(this, a);
                            if (0 == n.fired) return (a.events[e].fired = !0), n.handler.call(this, a);
                        }
                    }
                },
                addEvent: function (t, e, n) {
                    return a.events.push({ name: t, handler: e, one: n, fired: !1 }), a;
                },
            };
        return (
            (a.setDefaults = function (t) {
                o = t;
            }),
            (a.getState = function () {
                return a.state;
            }),
            (a.toggle = function () {
                return i.toggle();
            }),
            (a.toggleOn = function () {
                return i.toggleOn();
            }),
            (a.toggleOff = function () {
                return i.toggleOff();
            }),
            (a.on = function (t, e) {
                return i.addEvent(t, e);
            }),
            (a.one = function (t, e) {
                return i.addEvent(t, e, !0);
            }),
            i.construct.apply(a, [e]),
            a
        );
    }
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyToggle),
    Element.prototype.matches || (Element.prototype.matches = Element.prototype.msMatchesSelector || Element.prototype.webkitMatchesSelector),
    Element.prototype.closest ||
    (Element.prototype.matches || (Element.prototype.matches = Element.prototype.msMatchesSelector || Element.prototype.webkitMatchesSelector),
        (Element.prototype.closest = function (t) {
            var e = this;
            if (!document.documentElement.contains(this)) return null;
            do {
                if (e.matches(t)) return e;
                e = e.parentElement;
            } while (null !== e);
            return null;
        })),
    (function (t) {
        for (var e = 0; e < t.length; e++)
            !window[t[e]] ||
                "remove" in window[t[e]].prototype ||
                (window[t[e]].prototype.remove = function () {
                    this.parentNode.removeChild(this);
                });
    })(["Element", "CharacterData", "DocumentType"]),
    (function () {
        for (var t = 0, e = ["webkit", "moz"], a = 0; a < e.length && !window.requestAnimationFrame; ++a)
            (window.requestAnimationFrame = window[e[a] + "RequestAnimationFrame"]), (window.cancelAnimationFrame = window[e[a] + "CancelAnimationFrame"] || window[e[a] + "CancelRequestAnimationFrame"]);
        window.requestAnimationFrame ||
            (window.requestAnimationFrame = function (e) {
                var a = new Date().getTime(),
                    n = Math.max(0, 16 - (a - t)),
                    o = window.setTimeout(function () {
                        e(a + n);
                    }, n);
                return (t = a + n), o;
            }),
            window.cancelAnimationFrame ||
            (window.cancelAnimationFrame = function (t) {
                clearTimeout(t);
            });
    })(),
    [Element.prototype, Document.prototype, DocumentFragment.prototype].forEach(function (t) {
        t.hasOwnProperty("prepend") ||
            Object.defineProperty(t, "prepend", {
                configurable: !0,
                enumerable: !0,
                writable: !0,
                value: function () {
                    var t = Array.prototype.slice.call(arguments),
                        e = document.createDocumentFragment();
                    t.forEach(function (t) {
                        var a = t instanceof Node;
                        e.appendChild(a ? t : document.createTextNode(String(t)));
                    }),
                        this.insertBefore(e, this.firstChild);
                },
            });
    }),
    (window.BeautyUtilElementDataStore = {}),
    (window.BeautyUtilElementDataStoreID = 0),
    (window.BeautyUtilDelegatedEventHandlers = {});
var BeautyUtil = (function () {
    var t = [],
        e = { sm: 544, md: 768, lg: 1024, xl: 1200 },
        a = function () {
            var e = !1;
            window.addEventListener("resize", function () {
                clearTimeout(e),
                    (e = setTimeout(function () {
                        !(function () {
                            for (var e = 0; e < t.length; e++) t[e].call();
                        })();
                    }, 250));
            });
        };
    return {
        init: function (t) {
            t && t.breakpoints && (e = t.breakpoints), a();
        },
        addResizeHandler: function (e) {
            t.push(e);
        },
        removeResizeHandler: function (e) {
            for (var a = 0; a < t.length; a++) e === t[a] && delete t[a];
        },
        runResizeHandlers: function () {
            _runResizeHandlers();
        },
        resize: function () {
            if ("function" == typeof Event) window.dispatchEvent(new Event("resize"));
            else {
                var t = window.document.createEvent("UIEvents");
                t.initUIEvent("resize", !0, !1, window, 0), window.dispatchEvent(t);
            }
        },
        getURLParam: function (t) {
            var e,
                a,
                n = window.location.search.substring(1).split("&");
            for (e = 0; e < n.length; e++) if ((a = n[e].split("="))[0] == t) return unescape(a[1]);
            return null;
        },
        isMobileDevice: function () {
            return this.getViewPort().width < this.getBreakpoint("lg");
        },
        isDesBeautyopDevice: function () {
            return !BeautyUtil.isMobileDevice();
        },
        getViewPort: function () {
            var t = window,
                e = "inner";
            return "innerWidth" in window || ((e = "client"), (t = document.documentElement || document.body)), { width: t[e + "Width"], height: t[e + "Height"] };
        },
        isInResponsiveRange: function (t) {
            var e = this.getViewPort().width;
            return (
                "general" == t ||
                ("desBeautyop" == t && e >= this.getBreakpoint("lg") + 1) ||
                ("tablet" == t && e >= this.getBreakpoint("md") + 1 && e < this.getBreakpoint("lg")) ||
                ("mobile" == t && e <= this.getBreakpoint("md")) ||
                ("desBeautyop-and-tablet" == t && e >= this.getBreakpoint("md") + 1) ||
                ("tablet-and-mobile" == t && e <= this.getBreakpoint("lg")) ||
                ("minimal-desBeautyop-and-below" == t && e <= this.getBreakpoint("xl"))
            );
        },
        getUniqueID: function (t) {
            return t + Math.floor(Math.random() * new Date().getTime());
        },
        getBreakpoint: function (t) {
            return e[t];
        },
        isset: function (t, e) {
            var a;
            if (-1 !== (e = e || "").indexOf("[")) throw new Error("Unsupported object path notation.");
            e = e.split(".");
            do {
                if (void 0 === t) return !1;
                if (((a = e.shift()), !t.hasOwnProperty(a))) return !1;
                t = t[a];
            } while (e.length);
            return !0;
        },
        getHighestZindex: function (t) {
            for (var e, a, n = BeautyUtil.get(t); n && n !== document;) {
                if (("absolute" === (e = BeautyUtil.css(n, "position")) || "relative" === e || "fixed" === e) && ((a = parseInt(BeautyUtil.css(n, "z-index"))), !isNaN(a) && 0 !== a)) return a;
                n = n.parentNode;
            }
            return null;
        },
        hasFixedPositionedParent: function (t) {
            for (; t && t !== document;) {
                if ("fixed" === BeautyUtil.css(t, "position")) return !0;
                t = t.parentNode;
            }
            return !1;
        },
        sleep: function (t) {
            for (var e = new Date().getTime(), a = 0; a < 1e7 && !(new Date().getTime() - e > t); a++);
        },
        getRandomInt: function (t, e) {
            return Math.floor(Math.random() * (e - t + 1)) + t;
        },
        isAngularVersion: function () {
            return void 0 !== window.Zone;
        },
        deepExtend: function (t) {
            t = t || {};
            for (var e = 1; e < arguments.length; e++) {
                var a = arguments[e];
                if (a) for (var n in a) a.hasOwnProperty(n) && ("object" == typeof a[n] ? (t[n] = BeautyUtil.deepExtend(t[n], a[n])) : (t[n] = a[n]));
            }
            return t;
        },
        extend: function (t) {
            t = t || {};
            for (var e = 1; e < arguments.length; e++) if (arguments[e]) for (var a in arguments[e]) arguments[e].hasOwnProperty(a) && (t[a] = arguments[e][a]);
            return t;
        },
        get: function (t) {
            var e;
            return t === document ? document : t && 1 === t.nodeType ? t : (e = document.getElementById(t)) ? e : (e = document.getElementsByTagName(t)).length > 0 ? e[0] : (e = document.getElementsByClassName(t)).length > 0 ? e[0] : null;
        },
        getByID: function (t) {
            return t && 1 === t.nodeType ? t : document.getElementById(t);
        },
        getByTag: function (t) {
            var e;
            return (e = document.getElementsByTagName(t)) ? e[0] : null;
        },
        getByClass: function (t) {
            var e;
            return (e = document.getElementsByClassName(t)) ? e[0] : null;
        },
        hasClasses: function (t, e) {
            if (t) {
                for (var a = e.split(" "), n = 0; n < a.length; n++) if (0 == BeautyUtil.hasClass(t, BeautyUtil.trim(a[n]))) return !1;
                return !0;
            }
        },
        hasClass: function (t, e) {
            if (t) return t.classList ? t.classList.contains(e) : new RegExp("\\b" + e + "\\b").test(t.className);
        },
        addClass: function (t, e) {
            if (t && void 0 !== e) {
                var a = e.split(" ");
                if (t.classList) for (var n = 0; n < a.length; n++) a[n] && a[n].length > 0 && t.classList.add(BeautyUtil.trim(a[n]));
                else if (!BeautyUtil.hasClass(t, e)) for (var o = 0; o < a.length; o++) t.className += " " + BeautyUtil.trim(a[o]);
            }
        },
        removeClass: function (t, e) {
            if (t && void 0 !== e) {
                var a = e.split(" ");
                if (t.classList) for (var n = 0; n < a.length; n++) t.classList.remove(BeautyUtil.trim(a[n]));
                else if (BeautyUtil.hasClass(t, e)) for (var o = 0; o < a.length; o++) t.className = t.className.replace(new RegExp("\\b" + BeautyUtil.trim(a[o]) + "\\b", "g"), "");
            }
        },
        triggerCustomEvent: function (t, e, a) {
            var n;
            window.CustomEvent ? (n = new CustomEvent(e, { detail: a })) : (n = document.createEvent("CustomEvent")).initCustomEvent(e, !0, !0, a), t.dispatchEvent(n);
        },
        triggerEvent: function (t, e) {
            var a;
            if (t.ownerDocument) a = t.ownerDocument;
            else {
                if (9 != t.nodeType) throw new Error("Invalid node passed to fireEvent: " + t.id);
                a = t;
            }
            if (t.dispatchEvent) {
                var n = "";
                switch (e) {
                    case "click":
                    case "mouseenter":
                    case "mouseleave":
                    case "mousedown":
                    case "mouseup":
                        n = "MouseEvents";
                        break;
                    case "focus":
                    case "change":
                    case "blur":
                    case "select":
                        n = "HTMLEvents";
                        break;
                    default:
                        throw "fireEvent: Couldn't find an event class for event '" + e + "'.";
                }
                var o = "change" != e;
                (i = a.createEvent(n)).initEvent(e, o, !0), (i.synthetic = !0), t.dispatchEvent(i, !0);
            } else if (t.fireEvent) {
                var i;
                ((i = a.createEventObject()).synthetic = !0), t.fireEvent("on" + e, i);
            }
        },
        index: function (t) {
            for (var e = (t = BeautyUtil.get(t)).parentNode.children, a = 0; a < e.length; a++) if (e[a] == t) return a;
        },
        trim: function (t) {
            return t.trim();
        },
        eventTriggered: function (t) {
            return !!t.currentTarget.dataset.triggered || ((t.currentTarget.dataset.triggered = !0), !1);
        },
        remove: function (t) {
            t && t.parentNode && t.parentNode.removeChild(t);
        },
        find: function (t, e) {
            if ((t = BeautyUtil.get(t))) return t.querySelector(e);
        },
        findAll: function (t, e) {
            if ((t = BeautyUtil.get(t))) return t.querySelectorAll(e);
        },
        insertAfter: function (t, e) {
            return e.parentNode.insertBefore(t, e.nextSibling);
        },
        parents: function (t, e) {
            Element.prototype.matches ||
                (Element.prototype.matches =
                    Element.prototype.matchesSelector ||
                    Element.prototype.mozMatchesSelector ||
                    Element.prototype.msMatchesSelector ||
                    Element.prototype.oMatchesSelector ||
                    Element.prototype.webkitMatchesSelector ||
                    function (t) {
                        for (var e = (this.document || this.ownerDocument).querySelectorAll(t), a = e.length; --a >= 0 && e.item(a) !== this;);
                        return a > -1;
                    });
            for (var a = []; t && t !== document; t = t.parentNode) e ? t.matches(e) && a.push(t) : a.push(t);
            return a;
        },
        children: function (t, e, a) {
            if (t && t.childNodes) {
                for (var n = [], o = 0, i = t.childNodes.length; o < i; ++o) 1 == t.childNodes[o].nodeType && BeautyUtil.matches(t.childNodes[o], e, a) && n.push(t.childNodes[o]);
                return n;
            }
        },
        child: function (t, e, a) {
            var n = BeautyUtil.children(t, e, a);
            return n ? n[0] : null;
        },
        matches: function (t, e, a) {
            var n = Element.prototype,
                o =
                    n.matches ||
                    n.webkitMatchesSelector ||
                    n.mozMatchesSelector ||
                    n.msMatchesSelector ||
                    function (t) {
                        return -1 !== [].indexOf.call(document.querySelectorAll(t), this);
                    };
            return !(!t || !t.tagName) && o.call(t, e);
        },
        data: function (t) {
            return (
                (t = BeautyUtil.get(t)),
                {
                    set: function (e, a) {
                        null != t &&
                            void 0 !== t &&
                            (void 0 === t.customDataTag && (window.BeautyUtilElementDataStoreID++, (t.customDataTag = window.BeautyUtilElementDataStoreID)),
                                void 0 === window.BeautyUtilElementDataStore[t.customDataTag] && (window.BeautyUtilElementDataStore[t.customDataTag] = {}),
                                (window.BeautyUtilElementDataStore[t.customDataTag][e] = a));
                    },
                    get: function (e) {
                        if (void 0 !== t) return null == t || void 0 === t.customDataTag ? null : this.has(e) ? window.BeautyUtilElementDataStore[t.customDataTag][e] : null;
                    },
                    has: function (e) {
                        return void 0 !== t && null != t && void 0 !== t.customDataTag && !(!window.BeautyUtilElementDataStore[t.customDataTag] || !window.BeautyUtilElementDataStore[t.customDataTag][e]);
                    },
                    remove: function (e) {
                        t && this.has(e) && delete window.BeautyUtilElementDataStore[t.customDataTag][e];
                    },
                }
            );
        },
        outerWidth: function (t, e) {
            var a;
            return !0 === e ? ((a = parseFloat(t.offsetWidth)), (a += parseFloat(BeautyUtil.css(t, "margin-left")) + parseFloat(BeautyUtil.css(t, "margin-right"))), parseFloat(a)) : (a = parseFloat(t.offsetWidth));
        },
        offset: function (t) {
            var e, a;
            if ((t = BeautyUtil.get(t))) return t.getClientRects().length ? ((e = t.getBoundingClientRect()), (a = t.ownerDocument.defaultView), { top: e.top + a.pageYOffset, left: e.left + a.pageXOffset }) : { top: 0, left: 0 };
        },
        height: function (t) {
            return BeautyUtil.css(t, "height");
        },
        visible: function (t) {
            return !(0 === t.offsetWidth && 0 === t.offsetHeight);
        },
        attr: function (t, e, a) {
            if (null != (t = BeautyUtil.get(t))) return void 0 === a ? t.getAttribute(e) : void t.setAttribute(e, a);
        },
        hasAttr: function (t, e) {
            if (null != (t = BeautyUtil.get(t))) return !!t.getAttribute(e);
        },
        removeAttr: function (t, e) {
            null != (t = BeautyUtil.get(t)) && t.removeAttribute(e);
        },
        animate: function (t, e, a, n, o, i) {
            var l = {};
            if (
                ((l.linear = function (t, e, a, n) {
                    return (a * t) / n + e;
                }),
                    (o = l.linear),
                    "number" == typeof t && "number" == typeof e && "number" == typeof a && "function" == typeof n)
            ) {
                "function" != typeof i && (i = function () { });
                var r =
                    window.requestAnimationFrame ||
                    function (t) {
                        window.setTimeout(t, 20);
                    },
                    s = e - t;
                n(t);
                var d = window.performance && window.performance.now ? window.performance.now() : +new Date();
                r(function l(c) {
                    var u = (c || +new Date()) - d;
                    u >= 0 && n(o(u, t, s, a)), u >= 0 && u >= a ? (n(e), i()) : r(l);
                });
            }
        },
        actualCss: function (t, e, a) {
            var n,
                o = "";
            if ((t = BeautyUtil.get(t)) instanceof HTMLElement != !1)
                return t.getAttribute("beauty-hidden-" + e) && !1 !== a
                    ? parseFloat(t.getAttribute("beauty-hidden-" + e))
                    : ((o = t.style.cssText),
                        (t.style.cssText = "position: absolute; visibility: hidden; display: block;"),
                        "width" == e ? (n = t.offsetWidth) : "height" == e && (n = t.offsetHeight),
                        (t.style.cssText = o),
                        t.setAttribute("beauty-hidden-" + e, n),
                        parseFloat(n));
        },
        actualHeight: function (t, e) {
            return BeautyUtil.actualCss(t, "height", e);
        },
        actualWidth: function (t, e) {
            return BeautyUtil.actualCss(t, "width", e);
        },
        getScroll: function (t, e) {
            return (e = "scroll" + e), t == window || t == document ? self["scrollTop" == e ? "pageYOffset" : "pageXOffset"] || (browserSupportsBoxModel && document.documentElement[e]) || document.body[e] : t[e];
        },
        css: function (t, e, a) {
            if ((t = BeautyUtil.get(t)))
                if (void 0 !== a) t.style[e] = a;
                else {
                    var n = (t.ownerDocument || document).defaultView;
                    if (n && n.getComputedStyle) return (e = e.replace(/([A-Z])/g, "-$1").toLowerCase()), n.getComputedStyle(t, null).getPropertyValue(e);
                    if (t.currentStyle)
                        return (
                            (e = e.replace(/\-(\w)/g, function (t, e) {
                                return e.toUpperCase();
                            })),
                            (a = t.currentStyle[e]),
                            /^\d+(em|pt|%|ex)?$/i.test(a)
                                ? (function (e) {
                                    var a = t.style.left,
                                        n = t.runtimeStyle.left;
                                    return (t.runtimeStyle.left = t.currentStyle.left), (t.style.left = e || 0), (e = t.style.pixelLeft + "px"), (t.style.left = a), (t.runtimeStyle.left = n), e;
                                })(a)
                                : a
                        );
                }
        },
        slide: function (t, e, a, n, o) {
            if (!(!t || ("up" == e && !1 === BeautyUtil.visible(t)) || ("down" == e && !0 === BeautyUtil.visible(t)))) {
                a = a || 600;
                var i = BeautyUtil.actualHeight(t),
                    l = !1,
                    r = !1;
                BeautyUtil.css(t, "padding-top") && !0 !== BeautyUtil.data(t).has("slide-padding-top") && BeautyUtil.data(t).set("slide-padding-top", BeautyUtil.css(t, "padding-top")),
                    BeautyUtil.css(t, "padding-bottom") && !0 !== BeautyUtil.data(t).has("slide-padding-bottom") && BeautyUtil.data(t).set("slide-padding-bottom", BeautyUtil.css(t, "padding-bottom")),
                    BeautyUtil.data(t).has("slide-padding-top") && (l = parseInt(BeautyUtil.data(t).get("slide-padding-top"))),
                    BeautyUtil.data(t).has("slide-padding-bottom") && (r = parseInt(BeautyUtil.data(t).get("slide-padding-bottom"))),
                    "up" == e
                        ? ((t.style.cssText = "display: block; overflow: hidden;"),
                            l &&
                            BeautyUtil.animate(
                                0,
                                l,
                                a,
                                function (e) {
                                    t.style.paddingTop = l - e + "px";
                                },
                                "linear"
                            ),
                            r &&
                            BeautyUtil.animate(
                                0,
                                r,
                                a,
                                function (e) {
                                    t.style.paddingBottom = r - e + "px";
                                },
                                "linear"
                            ),
                            BeautyUtil.animate(
                                0,
                                i,
                                a,
                                function (e) {
                                    t.style.height = i - e + "px";
                                },
                                "linear",
                                function () {
                                    n(), (t.style.height = ""), (t.style.display = "none");
                                }
                            ))
                        : "down" == e &&
                        ((t.style.cssText = "display: block; overflow: hidden;"),
                            l &&
                            BeautyUtil.animate(
                                0,
                                l,
                                a,
                                function (e) {
                                    t.style.paddingTop = e + "px";
                                },
                                "linear",
                                function () {
                                    t.style.paddingTop = "";
                                }
                            ),
                            r &&
                            BeautyUtil.animate(
                                0,
                                r,
                                a,
                                function (e) {
                                    t.style.paddingBottom = e + "px";
                                },
                                "linear",
                                function () {
                                    t.style.paddingBottom = "";
                                }
                            ),
                            BeautyUtil.animate(
                                0,
                                i,
                                a,
                                function (e) {
                                    t.style.height = e + "px";
                                },
                                "linear",
                                function () {
                                    n(), (t.style.height = ""), (t.style.display = ""), (t.style.overflow = "");
                                }
                            ));
            }
        },
        slideUp: function (t, e, a) {
            BeautyUtil.slide(t, "up", e, a);
        },
        slideDown: function (t, e, a) {
            BeautyUtil.slide(t, "down", e, a);
        },
        show: function (t, e) {
            void 0 !== t && (t.style.display = e || "block");
        },
        hide: function (t) {
            void 0 !== t && (t.style.display = "none");
        },
        addEvent: function (t, e, a, n) {
            null != (t = BeautyUtil.get(t)) && t.addEventListener(e, a);
        },
        removeEvent: function (t, e, a) {
            null !== (t = BeautyUtil.get(t)) && t.removeEventListener(e, a);
        },
        on: function (t, e, a, n) {
            if (e) {
                var o = BeautyUtil.getUniqueID("event");
                return (
                    (window.BeautyUtilDelegatedEventHandlers[o] = function (a) {
                        for (var o = t.querySelectorAll(e), i = a.target; i && i !== t;) {
                            for (var l = 0, r = o.length; l < r; l++) i === o[l] && n.call(i, a);
                            i = i.parentNode;
                        }
                    }),
                    BeautyUtil.addEvent(t, a, window.BeautyUtilDelegatedEventHandlers[o]),
                    o
                );
            }
        },
        off: function (t, e, a) {
            t && window.BeautyUtilDelegatedEventHandlers[a] && (BeautyUtil.removeEvent(t, e, window.BeautyUtilDelegatedEventHandlers[a]), delete window.BeautyUtilDelegatedEventHandlers[a]);
        },
        one: function (t, e, a) {
            (t = BeautyUtil.get(t)).addEventListener(e, function t(e) {
                return e.target && e.target.removeEventListener && e.target.removeEventListener(e.type, t), a(e);
            });
        },
        hash: function (t) {
            var e,
                a = 0;
            if (0 === t.length) return a;
            for (e = 0; e < t.length; e++) (a = (a << 5) - a + t.charCodeAt(e)), (a |= 0);
            return a;
        },
        animateClass: function (t, e, a) {
            var n,
                o = { animation: "animationend", OAnimation: "oAnimationEnd", MozAnimation: "mozAnimationEnd", WebkitAnimation: "webkitAnimationEnd", msAnimation: "msAnimationEnd" };
            for (var i in o) void 0 !== t.style[i] && (n = o[i]);
            BeautyUtil.addClass(t, "animated " + e),
                BeautyUtil.one(t, n, function () {
                    BeautyUtil.removeClass(t, "animated " + e);
                }),
                a && BeautyUtil.one(t, n, a);
        },
        transitionEnd: function (t, e) {
            var a,
                n = { transition: "transitionend", OTransition: "oTransitionEnd", MozTransition: "mozTransitionEnd", WebkitTransition: "webkitTransitionEnd", msTransition: "msTransitionEnd" };
            for (var o in n) void 0 !== t.style[o] && (a = n[o]);
            BeautyUtil.one(t, a, e);
        },
        animationEnd: function (t, e) {
            var a,
                n = { animation: "animationend", OAnimation: "oAnimationEnd", MozAnimation: "mozAnimationEnd", WebkitAnimation: "webkitAnimationEnd", msAnimation: "msAnimationEnd" };
            for (var o in n) void 0 !== t.style[o] && (a = n[o]);
            BeautyUtil.one(t, a, e);
        },
        animateDelay: function (t, e) {
            for (var a = ["webkit-", "moz-", "ms-", "o-", ""], n = 0; n < a.length; n++) BeautyUtil.css(t, a[n] + "animation-delay", e);
        },
        animateDuration: function (t, e) {
            for (var a = ["webkit-", "moz-", "ms-", "o-", ""], n = 0; n < a.length; n++) BeautyUtil.css(t, a[n] + "animation-duration", e);
        },
        scrollTo: function (t, e, a) {
            a = a || 500;
            var n,
                o,
                i = (t = BeautyUtil.get(t)) ? BeautyUtil.offset(t).top : 0,
                l = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
            e && (l += e),
                (n = l),
                (o = i),
                BeautyUtil.animate(n, o, a, function (t) {
                    (document.documentElement.scrollTop = t), (document.body.parentNode.scrollTop = t), (document.body.scrollTop = t);
                });
        },
        scrollTop: function (t, e) {
            BeautyUtil.scrollTo(null, t, e);
        },
        isArray: function (t) {
            return t && Array.isArray(t);
        },
        ready: function (t) {
            (document.attachEvent ? "complete" === document.readyState : "loading" !== document.readyState) ? t() : document.addEventListener("DOMContentLoaded", t);
        },
        isEmpty: function (t) {
            for (var e in t) if (t.hasOwnProperty(e)) return !1;
            return !0;
        },
        numberString: function (t) {
            for (var e = (t += "").split("."), a = e[0], n = e.length > 1 ? "." + e[1] : "", o = /(\d+)(\d{3})/; o.test(a);) a = a.replace(o, "$1,$2");
            return a + n;
        },
        detectIE: function () {
            var t = window.navigator.userAgent,
                e = t.indexOf("MSIE ");
            if (e > 0) return parseInt(t.substring(e + 5, t.indexOf(".", e)), 10);
            if (t.indexOf("Trident/") > 0) {
                var a = t.indexOf("rv:");
                return parseInt(t.substring(a + 3, t.indexOf(".", a)), 10);
            }
            var n = t.indexOf("Edge/");
            return n > 0 && parseInt(t.substring(n + 5, t.indexOf(".", n)), 10);
        },
        isRTL: function () {
            return "rtl" == BeautyUtil.attr(BeautyUtil.get("html"), "direction");
        },
        scrollInit: function (t, e) {
            function a() {
                var a, n;
                if (((n = e.height instanceof Function ? parseInt(e.height.call()) : parseInt(e.height)), (e.mobileNativeScroll || e.disableForMobile) && BeautyUtil.isInResponsiveRange("tablet-and-mobile")))
                    (a = BeautyUtil.data(t).get("ps"))
                        ? (e.resetHeightOnDestroy ? BeautyUtil.css(t, "height", "auto") : (BeautyUtil.css(t, "overflow", "auto"), n > 0 && BeautyUtil.css(t, "height", n + "px")), a.destroy(), (a = BeautyUtil.data(t).remove("ps")))
                        : n > 0 && (BeautyUtil.css(t, "overflow", "auto"), BeautyUtil.css(t, "height", n + "px"));
                else if ((n > 0 && BeautyUtil.css(t, "height", n + "px"), e.desBeautyopNativeScroll)) BeautyUtil.css(t, "overflow", "auto");
                else {
                    BeautyUtil.css(t, "overflow", "hidden"),
                        (a = BeautyUtil.data(t).get("ps"))
                            ? a.update()
                            : (BeautyUtil.addClass(t, "beauty-scroll"),
                                (a = new PerfectScrollbar(t, {
                                    wheelSpeed: 0.5,
                                    swipeEasing: !0,
                                    wheelPropagation: !1 !== e.windowScroll,
                                    minScrollbarLength: 40,
                                    maxScrollbarLength: 300,
                                    suppressScrollX: "true" != BeautyUtil.attr(t, "data-scroll-x"),
                                })),
                                BeautyUtil.data(t).set("ps", a));
                    var o = BeautyUtil.attr(t, "id");
                    if (!0 === e.rememberPosition && Cookies && o) {
                        if (Cookies.get(o)) {
                            var i = parseInt(Cookies.get(o));
                            i > 0 && (t.scrollTop = i);
                        }
                        t.addEventListener("ps-scroll-y", function () {
                            Cookies.set(o, t.scrollTop);
                        });
                    }
                }
            }
            t &&
                (a(),
                    e.handleWindowResize &&
                    BeautyUtil.addResizeHandler(function () {
                        a();
                    }));
        },
        scrollUpdate: function (t) {
            var e = BeautyUtil.data(t).get("ps");
            e && e.update();
        },
        scrollUpdateAll: function (t) {
            for (var e = BeautyUtil.findAll(t, ".ps"), a = 0, n = e.length; a < n; a++) BeautyUtil.scrollUpdate(e[a]);
        },
        scrollDestroy: function (t) {
            var e = BeautyUtil.data(t).get("ps");
            e && (e.destroy(), (e = BeautyUtil.data(t).remove("ps")));
        },
        setHTML: function (t, e) {
            BeautyUtil.get(t) && (BeautyUtil.get(t).innerHTML = e);
        },
        getHTML: function (t) {
            if (BeautyUtil.get(t)) return BeautyUtil.get(t).innerHTML;
        },
        getDocumentHeight: function () {
            var t = document.body,
                e = document.documentElement;
            return Math.max(t.scrollHeight, t.offsetHeight, e.clientHeight, e.scrollHeight, e.offsetHeight);
        },
        getScrollTop: function () {
            return (document.scrollingElement || document.documentElement).scrollTop;
        },
        getAlert: function (type, message) {
            return $(
                '<div class="alert alert-bold alert-solid-' +
                type +
                ' alert-dismissible" role="alert">\t\t\t<div class="alert-text">' +
                message +
                '</div>\t\t\t<div class="alert-close">                <i class="flaticon2-cross beauty-icon-sm" data-dismiss="alert"></i>            </div>\t\t</div>'
            );
        },
    };
})();
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyUtil),
    BeautyUtil.ready(function () {
        BeautyUtil.init();
    }),
    (window.onload = function () {
        BeautyUtil.removeClass(BeautyUtil.get("body"), "beauty-page--loading");
    });
var BeautyWizard = function (t, e) {
    var a = this,
        n = BeautyUtil.get(t);
    BeautyUtil.get("body");
    if (n) {
        var o = { startStep: 1, clickableSteps: !0 },
            i = {
                construct: function (t) {
                    return BeautyUtil.data(n).has("wizard") ? (a = BeautyUtil.data(n).get("wizard")) : (i.init(t), i.build(), BeautyUtil.data(n).set("wizard", a)), a;
                },
                init: function (t) {
                    (a.element = n),
                        (a.events = []),
                        (a.options = BeautyUtil.deepExtend({}, o, t)),
                        (a.steps = BeautyUtil.findAll(n, '[data-Beautywizard-type="step"]')),
                        (a.btnSubmit = BeautyUtil.find(n, '[data-Beautywizard-type="action-submit"]')),
                        (a.btnNext = BeautyUtil.find(n, '[data-Beautywizard-type="action-next"]')),
                        (a.btnPrev = BeautyUtil.find(n, '[data-Beautywizard-type="action-prev"]')),
                        (a.btnLast = BeautyUtil.find(n, '[data-Beautywizard-type="action-last"]')),
                        (a.btnFirst = BeautyUtil.find(n, '[data-Beautywizard-type="action-first"]')),
                        (a.events = []),
                        (a.currentStep = 1),
                        (a.stopped = !1),
                        (a.totalSteps = a.steps.length),
                        a.options.startStep > 1 && i.goTo(a.options.startStep),
                        i.updateUI();
                },
                build: function () {
                    BeautyUtil.addEvent(a.btnNext, "click", function (t) {
                        t.preventDefault(), i.goTo(i.getNextStep(), !0);
                    }),
                        BeautyUtil.addEvent(a.btnPrev, "click", function (t) {
                            t.preventDefault(), i.goTo(i.getPrevStep(), !0);
                        }),
                        BeautyUtil.addEvent(a.btnFirst, "click", function (t) {
                            t.preventDefault(), i.goTo(1, !0);
                        }),
                        BeautyUtil.addEvent(a.btnLast, "click", function (t) {
                            t.preventDefault(), i.goTo(a.totalSteps, !0);
                        }),
                        !0 === a.options.clickableSteps &&
                        BeautyUtil.on(n, '[data-Beautywizard-type="step"]', "click", function () {
                            var t = Array.prototype.indexOf.call(a.steps, this) + 1;
                            t !== a.currentStep && i.goTo(t, !0);
                        });
                },
                goTo: function (t, e) {
                    if (!(t === a.currentStep || t > a.totalSteps || t < 0)) {
                        var n;
                        if (((t = t ? parseInt(t) : i.getNextStep()), !0 === e && (n = t > a.currentStep ? i.eventTrigger("beforeNext") : i.eventTrigger("beforePrev")), !0 !== a.stopped))
                            return (
                                !1 !== n && (!0 === e && i.eventTrigger("beforeChange"), (a.currentStep = t), i.updateUI(), !0 === e && i.eventTrigger("change")),
                                !0 === e ? (t > a.startStep ? i.eventTrigger("afterNext") : i.eventTrigger("afterPrev")) : (a.stopped = !0),
                                a
                            );
                        a.stopped = !1;
                    }
                },
                stop: function () {
                    a.stopped = !0;
                },
                start: function () {
                    a.stopped = !1;
                },
                isLastStep: function () {
                    return a.currentStep === a.totalSteps;
                },
                isFirstStep: function () {
                    return 1 === a.currentStep;
                },
                isBetweenStep: function () {
                    return !1 === i.isLastStep() && !1 === i.isFirstStep();
                },
                updateUI: function () {
                    var t = "",
                        e = a.currentStep - 1;
                    (t = i.isLastStep() ? "last" : i.isFirstStep() ? "first" : "between"), BeautyUtil.attr(a.element, "data-Beautywizard-state", t);
                    var n = BeautyUtil.findAll(a.element, '[data-Beautywizard-type="step"]');
                    if (n && n.length > 0)
                        for (var o = 0, l = n.length; o < l; o++)
                            o == e ? BeautyUtil.attr(n[o], "data-Beautywizard-state", "current") : o < e ? BeautyUtil.attr(n[o], "data-Beautywizard-state", "done") : BeautyUtil.attr(n[o], "data-Beautywizard-state", "pending");
                    var r = BeautyUtil.findAll(a.element, '[data-Beautywizard-type="step-info"]');
                    if (r && r.length > 0) for (o = 0, l = r.length; o < l; o++) o == e ? BeautyUtil.attr(r[o], "data-Beautywizard-state", "current") : BeautyUtil.removeAttr(r[o], "data-Beautywizard-state");
                    var s = BeautyUtil.findAll(a.element, '[data-Beautywizard-type="step-content"]');
                    if (s && s.length > 0) for (o = 0, l = s.length; o < l; o++) o == e ? BeautyUtil.attr(s[o], "data-Beautywizard-state", "current") : BeautyUtil.removeAttr(s[o], "data-Beautywizard-state");
                },
                getNextStep: function () {
                    return a.totalSteps >= a.currentStep + 1 ? a.currentStep + 1 : a.totalSteps;
                },
                getPrevStep: function () {
                    return a.currentStep - 1 >= 1 ? a.currentStep - 1 : 1;
                },
                eventTrigger: function (t, e) {
                    for (var n = 0; n < a.events.length; n++) {
                        var o = a.events[n];
                        if (o.name == t) {
                            if (1 != o.one) return o.handler.call(this, a);
                            if (0 == o.fired) return (a.events[n].fired = !0), o.handler.call(this, a);
                        }
                    }
                },
                addEvent: function (t, e, n) {
                    return a.events.push({ name: t, handler: e, one: n, fired: !1 }), a;
                },
            };
        return (
            (a.setDefaults = function (t) {
                o = t;
            }),
            (a.goNext = function (t) {
                return i.goTo(i.getNextStep(), t);
            }),
            (a.goPrev = function (t) {
                return i.goTo(i.getPrevStep(), t);
            }),
            (a.goLast = function (t) {
                return i.goTo(a.totalSteps, t);
            }),
            (a.goFirst = function (t) {
                return i.goTo(1, t);
            }),
            (a.goTo = function (t, e) {
                return i.goTo(t, e);
            }),
            (a.stop = function () {
                return i.stop();
            }),
            (a.start = function () {
                return i.start();
            }),
            (a.getStep = function () {
                return a.currentStep;
            }),
            (a.isLastStep = function () {
                return i.isLastStep();
            }),
            (a.isFirstStep = function () {
                return i.isFirstStep();
            }),
            (a.on = function (t, e) {
                return i.addEvent(t, e);
            }),
            (a.one = function (t, e) {
                return i.addEvent(t, e, !0);
            }),
            i.construct.apply(a, [e]),
            a
        );
    }
};
"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyWizard),
    //BeautyDatatable
    (function (t) {
        var e = "BeautyDatatable",
            a = BeautyUtil,
            n = BeautyApp;
        if (void 0 === a) throw new Error("Util class is required and must be included before BeautyDatatable");
        (t.fn.BeautyDatatable = function (o) {
            if (0 !== t(this).length) {
                var i = this;
                (i.debug = !1), (i.API = { record: null, value: null, params: null });
                var l = {
                    isInit: !1,
                    cellOffset: 110,
                    iconOffset: 15,
                    stateId: "meta",
                    ajaxParams: {},
                    pagingObject: {},
                    init: function (e) {
                        var a,
                            n = !1;

                        null === e.data.source && (l.extractTable(), (n = !0)),
                            l.setupBaseDOM.call(),
                            l.setupDOM(i.table),
                            t(i).on("beauty-datatable--on-layout-updated", l.afterRender),
                            i.debug && l.stateRemove(l.stateId),
                            l.setDataSourceQuery(l.getOption("data.source.read.params.query")),
                            t.each(l.getOption("extensions"), function (e, a) {
                                "function" == typeof t.fn.BeautyDatatable[e] && ("object" != typeof a && (a = t.extend({}, a)), new t.fn.BeautyDatatable[e](i, a));
                            }),
                            l.spinnerCallback(!0),
                            ("remote" !== e.data.type && "local" !== e.data.type) ||
                            ((!1 === e.data.saveState || (!1 === e.data.saveState.cookie && !1 === e.data.saveState.webstorage)) && l.stateRemove(l.stateId),
                                "local" === e.data.type && "object" == typeof e.data.source && (i.dataSet = i.originalDataSet = l.dataMapCallback(e.data.source)),
                                l.dataRender()),
                            n && (t(i.tableHead).find("tr").remove(), t(i.tableFoot).find("tr").remove()),
                            l.setHeadTitle(),
                            l.getOption("layout.footer") && l.setHeadTitle(i.tableFoot),
                            void 0 !== e.layout.header && !1 === e.layout.header && t(i.table).find("thead").remove(),
                            void 0 !== e.layout.footer && !1 === e.layout.footer && t(i.table).find("tfoot").remove(),
                            (null !== e.data.type && "local" !== e.data.type) || (l.setupCellField.call(), l.setupTemplateCell.call(), l.setupSubDatatable.call(), l.setupSystemColumn.call(), l.redraw());
                        var o = !1;
                        t(window).resize(function () {
                            t(this).width() !== a && ((a = t(this).width()), l.fullRender()), o || ((a = t(this).width()), (o = !0));
                        }),
                            t(i).height("");
                        var r = "";
                        return (
                            t(l.getOption("search.input")).on("keyup", function (e) {
                                if (!l.getOption("search.onEnter") || 13 === e.which) {
                                    var a = t(this).val();
                                    r !== a && (l.search(a), (r = a));
                                }
                            }),
                            i
                        );
                    },
                    extractTable: function () {
                        var e = [],
                            n = t(i)
                                .find("tr:first-child th")
                                .get()
                                .map(function (a, n) {
                                    var i = t(a).data("field");
                                    void 0 === i && (i = t(a).text().trim());
                                    var l = { field: i, title: i };
                                    for (var r in o.columns) o.columns[r].field === i && (l = t.extend(!0, {}, o.columns[r], l));
                                    return e.push(l), i;
                                });
                        o.columns = e;
                        var l = [],
                            r = [];
                        t(i)
                            .find("tr")
                            .each(function () {
                                t(this).find("td").length && l.push(t(this).prop("attributes"));
                                var e = {};
                                t(this)
                                    .find("td")
                                    .each(function (t, a) {
                                        e[n[t]] = a.innerHTML.trim();
                                    }),
                                    a.isEmpty(e) || r.push(e);
                            }),
                            (o.data.attr.rowProps = l),
                            (o.data.source = r);
                    },
                    layoutUpdate: function () {
                        l.setupSubDatatable.call(),
                            l.setupSystemColumn.call(),
                            l.setupHover.call(),
                            void 0 === o.detail && 1 === l.getDepth() && l.locBeautyable.call(),
                            l.resetScroll(),
                            l.isLocked() || (l.redraw.call(), l.isSubtable() || !0 !== l.getOption("rows.autoHide") || l.autoHide(), t(i.table).find(".beauty-datatable__row").css("height", "")),
                            l.columnHide.call(),
                            l.rowEvenOdd.call(),
                            l.sorting.call(),
                            l.scrollbar.call(),
                            l.isInit ||
                            (l.dropdownFix(),
                                t(i).trigger("beauty-datatable--on-init", {
                                    table: t(i.wrap).attr("id"),
                                    options: o,
                                }),
                                (l.isInit = !0)),
                            t(i).trigger("beauty-datatable--on-layout-updated", { table: t(i.wrap).attr("id") });
                    },
                    dropdownFix: function () {
                        var e;
                        t("body")
                            .on("show.bs.dropdown", ".beauty-datatable .beauty-datatable__body", function (a) {
                                (e = t(a.target).find(".dropdown-menu")),
                                    t("body").append(e.detach()),
                                    e.css("display", "block"),
                                    e.position({ my: "right top", at: "right bottom", of: t(a.relatedTarget) }),
                                    i.closest(".modal").length && e.css("z-index", "2000");
                            })
                            .on("hide.bs.dropdown", ".beauty-datatable .beauty-datatable__body", function (a) {
                                t(a.target).append(e.detach()), e.hide();
                            }),
                            t(window).on("resize", function (t) {
                                void 0 !== e && e.hide();
                            });
                    },
                    locBeautyable: function () {
                        var e = {
                            lockEnabled: !1,
                            init: function () {
                                (e.lockEnabled = l.lockEnabledColumns()), (0 === e.lockEnabled.left.length && 0 === e.lockEnabled.right.length) || e.enable();
                            },
                            enable: function () {
                                t(i.table)
                                    .find("thead,tbody,tfoot")
                                    .each(function () {
                                        var a = this;
                                        0 === t(this).find(".beauty-datatable__lock").length &&
                                            t(this).ready(function () {
                                                !(function (a) {
                                                    if (t(a).find(".beauty-datatable__lock").length > 0) l.log("Locked container already exist in: ", a);
                                                    else if (0 !== t(a).find(".beauty-datatable__row").length) {
                                                        var n = t("<div/>").addClass("beauty-datatable__lock beauty-datatable__lock--left"),
                                                            o = t("<div/>").addClass("beauty-datatable__lock beauty-datatable__lock--scroll"),
                                                            r = t("<div/>").addClass("beauty-datatable__lock beauty-datatable__lock--right");
                                                        t(a)
                                                            .find(".beauty-datatable__row")
                                                            .each(function () {
                                                                var e = t("<tr/>").addClass("beauty-datatable__row").data("obj", t(this).data("obj")).appendTo(n),
                                                                    a = t("<tr/>").addClass("beauty-datatable__row").data("obj", t(this).data("obj")).appendTo(o),
                                                                    i = t("<tr/>").addClass("beauty-datatable__row").data("obj", t(this).data("obj")).appendTo(r);
                                                                t(this)
                                                                    .find(".beauty-datatable__cell")
                                                                    .each(function () {
                                                                        var n = t(this).data("locked");
                                                                        void 0 !== n ? ((void 0 === n.left && !0 !== n) || t(this).appendTo(e), void 0 !== n.right && t(this).appendTo(i)) : t(this).appendTo(a);
                                                                    }),
                                                                    t(this).remove();
                                                            }),
                                                            e.lockEnabled.left.length > 0 && (t(i.wrap).addClass("beauty-datatable--lock"), t(n).appendTo(a)),
                                                            (e.lockEnabled.left.length > 0 || e.lockEnabled.right.length > 0) && t(o).appendTo(a),
                                                            e.lockEnabled.right.length > 0 && (t(i.wrap).addClass("beauty-datatable--lock"), t(r).appendTo(a));
                                                    } else l.log("No row exist in: ", a);
                                                })(a);
                                            });
                                    });
                            },
                        };
                        return e.init(), e;
                    },
                    fullRender: function () {
                        t(i.tableHead).empty(),
                            l.setHeadTitle(),
                            l.getOption("layout.footer") && (t(i.tableFoot).empty(),
                                l.setHeadTitle(i.tableFoot)),
                            l.spinnerCallback(!0),
                            t(i.wrap).removeClass("beauty-datatable--loaded"),
                            l.insertData();
                    },
                    lockEnabledColumns: function () {
                        var e = t(window).width(),
                            n = o.columns,
                            i = { left: [], right: [] };
                        return (
                            t.each(n, function (t, n) {
                                void 0 !== n.locked &&
                                    (void 0 !== n.locked.left && a.getBreakpoint(n.locked.left) <= e && i.left.push(n.locked.left), void 0 !== n.locked.right && a.getBreakpoint(n.locked.right) <= e && i.right.push(n.locked.right));
                            }),
                            i
                        );
                    },
                    afterRender: function (e, a) {
                        t(i).ready(function () {
                            l.isLocked() && l.redraw(), t(i.tableBody).css("visibility", ""), t(i.wrap).addClass("beauty-datatable--loaded"), l.spinnerCallback(!1);
                        });
                    },
                    hoverTimer: 0,
                    isScrolling: !1,
                    setupHover: function () {
                        t(window).scroll(function (t) {
                            clearTimeout(l.hoverTimer), (l.isScrolling = !0);
                        }),
                            t(i.tableBody)
                                .find(".beauty-datatable__cell")
                                .off("mouseenter", "mouseleave")
                                .on("mouseenter", function () {
                                    if (
                                        ((l.hoverTimer = setTimeout(function () {
                                            l.isScrolling = !1;
                                        }, 200)),
                                            !l.isScrolling)
                                    ) {
                                        var e = t(this).closest(".beauty-datatable__row").addClass("beauty-datatable__row--hover"),
                                            a = t(e).index() + 1;
                                        t(e)
                                            .closest(".beauty-datatable__lock")
                                            .parent()
                                            .find(".beauty-datatable__row:nth-child(" + a + ")")
                                            .addClass("beauty-datatable__row--hover");
                                    }
                                })
                                .on("mouseleave", function () {
                                    var e = t(this).closest(".beauty-datatable__row").removeClass("beauty-datatable__row--hover"),
                                        a = t(e).index() + 1;
                                    t(e)
                                        .closest(".beauty-datatable__lock")
                                        .parent()
                                        .find(".beauty-datatable__row:nth-child(" + a + ")")
                                        .removeClass("beauty-datatable__row--hover");
                                });
                    },
                    adjustLockContainer: function () {
                        if (!l.isLocked()) return 0;
                        var e = t(i.tableHead).width(),
                            a = t(i.tableHead).find(".beauty-datatable__lock--left").width(),
                            n = t(i.tableHead).find(".beauty-datatable__lock--right").width();
                        void 0 === a && (a = 0), void 0 === n && (n = 0);
                        var o = Math.floor(e - a - n);
                        return t(i.table).find(".beauty-datatable__lock--scroll").css("width", o), o;
                    },
                    dragResize: function () {
                        var e,
                            a,
                            n = !1,
                            o = void 0;
                        t(i.tableHead)
                            .find(".beauty-datatable__cell")
                            .mousedown(function (i) {
                                (o = t(this)), (n = !0), (e = i.pageX), (a = t(this).width()), t(o).addClass("beauty-datatable__cell--resizing");
                            })
                            .mousemove(function (l) {
                                if (n) {
                                    var r = t(o).index(),
                                        s = t(i.tableBody),
                                        d = t(o).closest(".beauty-datatable__lock");
                                    if (d) {
                                        var c = t(d).index();
                                        s = t(i.tableBody).find(".beauty-datatable__lock").eq(c);
                                    }
                                    t(s)
                                        .find(".beauty-datatable__row")
                                        .each(function (n, o) {
                                            t(o)
                                                .find(".beauty-datatable__cell")
                                                .eq(r)
                                                .width(a + (l.pageX - e))
                                                .children()
                                                .width(a + (l.pageX - e));
                                        }),
                                        t(o)
                                            .children()
                                            .css("width", a + (l.pageX - e));
                                }
                            })
                            .mouseup(function () {
                                t(o).removeClass("beauty-datatable__cell--resizing"), (n = !1);
                            }),
                            t(document).mouseup(function () {
                                t(o).removeClass("beauty-datatable__cell--resizing"), (n = !1);
                            });
                    },
                    initHeight: function () {
                        if (o.layout.height && o.layout.scroll) {
                            var e = t(i.tableHead).find(".beauty-datatable__row").outerHeight(),
                                a = t(i.tableFoot).find(".beauty-datatable__row").outerHeight(),
                                n = o.layout.height;
                            e > 0 && (n -= e), a > 0 && (n -= a), (n -= 2), t(i.tableBody).css("max-height", Math.floor(parseFloat(n)));
                        }
                    },
                    setupBaseDOM: function () {
                        (i.initialDatatable = t(i).clone()),
                            "TABLE" === t(i).prop("tagName")
                                ? ((i.table = t(i).removeClass("beauty-datatable").addClass("beauty-datatable__table")),
                                    0 === t(i.table).parents(".beauty-datatable").length &&
                                    (i.table.wrap(
                                        t("<div/>")
                                            .addClass("beauty-datatable")
                                            .addClass("beauty-datatable--" + o.layout.theme)
                                    ),
                                        (i.wrap = t(i.table).parent())))
                                : ((i.wrap = t(i)
                                    .addClass("beauty-datatable")
                                    .addClass("beauty-datatable--" + o.layout.theme)),
                                    (i.table = t("<table/>").addClass("beauty-datatable__table").appendTo(i))),
                            void 0 !== o.layout.class && t(i.wrap).addClass(o.layout.class),
                            t(i.table).removeClass("beauty-datatable--destroyed").css("display", "block"),
                            void 0 === t(i).attr("id") && (l.setOption("data.saveState", !1), t(i.table).attr("id", a.getUniqueID("beauty-datatable--"))),
                            l.getOption("layout.minHeight") && t(i.table).css("min-height", l.getOption("layout.minHeight")),
                            l.getOption("layout.height") && t(i.table).css("max-height", l.getOption("layout.height")),
                            null === o.data.type && t(i.table).css("width", "").css("display", ""),
                            (i.tableHead = t(i.table).find("thead")),
                            0 === t(i.tableHead).length && (i.tableHead = t("<thead/>").prependTo(i.table)),
                            (i.tableBody = t(i.table).find("tbody")),
                            0 === t(i.tableBody).length && (i.tableBody = t("<tbody/>").appendTo(i.table)),
                            void 0 !== o.layout.footer && o.layout.footer && ((i.tableFoot = t(i.table).find("tfoot")), 0 === t(i.tableFoot).length && (i.tableFoot = t("<tfoot/>").appendTo(i.table)));
                    },
                    setupCellField: function (e) {
                        void 0 === e && (e = t(i.table).children());
                        var a = o.columns;
                        t.each(e, function (e, n) {
                            t(n)
                                .find(".beauty-datatable__row")
                                .each(function (e, n) {
                                    t(n)
                                        .find(".beauty-datatable__cell")
                                        .each(function (e, n) {
                                            void 0 !== a[e] && t(n).data(a[e]);
                                        });
                                });
                        });
                    },
                    setupTemplateCell: function (e) {
                        void 0 === e && (e = i.tableBody);
                        var a = o.columns;
                        t(e)
                            .find(".beauty-datatable__row")
                            .each(function (e, n) {
                                var o = t(n).data("obj");
                                if (void 0 !== o) {
                                    var r = l.getOption("rows.callback");
                                    "function" == typeof r && r(t(n), o, e);
                                    var s = l.getOption("rows.beforeTemplate");
                                    "function" == typeof s && s(t(n), o, e),
                                        void 0 === o &&
                                        ((o = {}),
                                            t(n)
                                                .find(".beauty-datatable__cell")
                                                .each(function (e, n) {
                                                    var i = t.grep(a, function (e, a) {
                                                        return t(n).data("field") === e.field;
                                                    })[0];
                                                    void 0 !== i && (o[i.field] = t(n).text());
                                                })),
                                        t(n)
                                            .find(".beauty-datatable__cell")
                                            .each(function (n, r) {
                                                var s = t.grep(a, function (e, a) {
                                                    return t(r).data("field") === e.field;
                                                })[0];
                                                if (void 0 !== s && void 0 !== s.template) {
                                                    var d = "";
                                                    "string" == typeof s.template && (d = l.dataPlaceholder(s.template, o)),
                                                        "function" == typeof s.template && (d = s.template(o, e, i)),
                                                        "undefined" != typeof DOMPurify && (d = DOMPurify.sanitize(d));
                                                    var c = document.createElement("span");
                                                    (c.innerHTML = d), t(r).html(c), void 0 !== s.overflow && (t(c).css("overflow", s.overflow), t(c).css("position", "relative"));
                                                }
                                            });
                                    var d = l.getOption("rows.afterTemplate");
                                    "function" == typeof d && d(t(n), o, e);
                                }
                            });
                    },
                    setupSystemColumn: function () {
                        if (((i.dataSet = i.dataSet || []), 0 !== i.dataSet.length)) {
                            var e = o.columns;
                            t(i.tableBody)
                                .find(".beauty-datatable__row")
                                .each(function (a, n) {
                                    t(n)
                                        .find(".beauty-datatable__cell")
                                        .each(function (a, n) {
                                            var o = t.grep(e, function (e, a) {
                                                return t(n).data("field") === e.field;
                                            })[0];
                                            if (void 0 !== o) {
                                                var i = t(n).text();
                                                if (void 0 !== o.selector && !1 !== o.selector) {
                                                    if (t(n).find('.beauty-checkbox [type="checkbox"]').length > 0) return;
                                                    t(n).addClass("beauty-datatable__cell--check");
                                                    var r = t("<label/>")
                                                        .addClass("beauty-checkbox beauty-checkbox--single")
                                                        .append(
                                                            t("<input/>")
                                                                .attr("type", "checkbox")
                                                                .attr("value", i)
                                                                .on("click", function () {
                                                                    t(this).is(":checked") ? l.setActive(this) : l.setInactive(this);
                                                                })
                                                        )
                                                        .append("&nbsp;<span></span>");
                                                    void 0 !== o.selector.class && t(r).addClass(o.selector.class), t(n).children().html(r);
                                                }
                                                if (void 0 !== o.subtable && o.subtable) {
                                                    if (t(n).find(".beauty-datatable__toggle-subtable").length > 0) return;
                                                    t(n)
                                                        .children()
                                                        .html(
                                                            t("<a/>")
                                                                .addClass("beauty-datatable__toggle-subtable")
                                                                .attr("href", "#")
                                                                .attr("data-value", i)
                                                                .append(t("<i/>").addClass(l.getOption("layout.icons.rowDetail.collapse")))
                                                        );
                                                }
                                            }
                                        });
                                });
                            var a = function (a) {
                                var n = t.grep(e, function (t, e) {
                                    return void 0 !== t.selector && !1 !== t.selector;
                                })[0];
                                if (void 0 !== n && void 0 !== n.selector && !1 !== n.selector) {
                                    var o = t(a).find('[data-field="' + n.field + '"]');
                                    if (t(o).find('.beauty-checkbox [type="checkbox"]').length > 0) return;
                                    t(o).addClass("beauty-datatable__cell--check");
                                    var i = t("<label/>")
                                        .addClass("beauty-checkbox beauty-checkbox--single beauty-checkbox--all")
                                        .append(
                                            t("<input/>")
                                                .attr("type", "checkbox")
                                                .on("click", function () {
                                                    t(this).is(":checked") ? l.setActiveAll(!0) : l.setActiveAll(!1);
                                                })
                                        )
                                        .append("&nbsp;<span></span>");
                                    void 0 !== n.selector.class && t(i).addClass(n.selector.class), t(o).children().html(i);
                                }
                            };
                            o.layout.header && a(t(i.tableHead).find(".beauty-datatable__row").first()), o.layout.footer && a(t(i.tableFoot).find(".beauty-datatable__row").first());
                        }
                    },
                    maxWidthList: {},
                    adjustCellsWidth: function () {
                        var e = t(i.tableBody).innerWidth() - l.iconOffset,
                            a = t(i.tableBody).find(".beauty-datatable__row:first-child").find(".beauty-datatable__cell").not(".beauty-datatable__toggle-detail").not(":hidden").length;
                        if (a > 0) {
                            e -= l.iconOffset * a;
                            var n = Math.floor(e / a);
                            n <= l.cellOffset && (n = l.cellOffset),
                                t(i.table)
                                    .find(".beauty-datatable__row")
                                    .find(".beauty-datatable__cell")
                                    .not(".beauty-datatable__toggle-detail")
                                    .not(":hidden")
                                    .each(function (e, a) {
                                        var o = n,
                                            r = t(a).data("width");
                                        if (void 0 !== r)
                                            if ("auto" === r) {
                                                var s = t(a).data("field");
                                                if (l.maxWidthList[s]) o = l.maxWidthList[s];
                                                else {
                                                    var d = t(i.table).find('.beauty-datatable__cell[data-field="' + s + '"]');
                                                    o = l.maxWidthList[s] = Math.max.apply(
                                                        null,
                                                        t(d)
                                                            .map(function () {
                                                                return t(this).outerWidth();
                                                            })
                                                            .get()
                                                    );
                                                }
                                            } else o = r;
                                        t(a).children().css("width", Math.ceil(o));
                                    });
                        }
                        return i;
                    },
                    adjustCellsHeight: function () {
                        t.each(t(i.table).children(), function (e, a) {
                            for (var n = t(a).find(".beauty-datatable__row").first().parent().find(".beauty-datatable__row").length, o = 1; o <= n; o++) {
                                var i = t(a).find(".beauty-datatable__row:nth-child(" + o + ")");
                                if (t(i).length > 0) {
                                    var l = Math.max.apply(
                                        null,
                                        t(i)
                                            .map(function () {
                                                return t(this).outerHeight();
                                            })
                                            .get()
                                    );
                                    t(i).css("height", Math.ceil(l));
                                }
                            }
                        });
                    },
                    setupDOM: function (e) {
                        t(e).find("> thead").addClass("beauty-datatable__head"),
                            t(e).find("> tbody").addClass("beauty-datatable__body"),
                            t(e).find("> tfoot").addClass("beauty-datatable__foot"),
                            t(e).find("tr").addClass("beauty-datatable__row"),
                            t(e).find("tr > th, tr > td").addClass("beauty-datatable__cell"),
                            t(e)
                                .find("tr > th, tr > td")
                                .each(function (e, a) {
                                    0 === t(a).find("span").length && t(a).wrapInner(t("<span/>").css("width", l.cellOffset));
                                });
                    },
                    scrollbar: function () {
                        var e = {
                            scrollable: null,
                            tableLocked: null,
                            initPosition: null,
                            init: function () {
                                var n = a.getViewPort().width;
                                if (o.layout.scroll) {
                                    t(i.wrap).addClass("beauty-datatable--scroll");
                                    var r = t(i.tableBody).find(".beauty-datatable__lock--scroll");
                                    t(r).find(".beauty-datatable__row").length > 0 && t(r).length > 0
                                        ? ((e.scrollHead = t(i.tableHead).find("> .beauty-datatable__lock--scroll > .beauty-datatable__row")),
                                            (e.scrollFoot = t(i.tableFoot).find("> .beauty-datatable__lock--scroll > .beauty-datatable__row")),
                                            (e.tableLocked = t(i.tableBody).find(".beauty-datatable__lock:not(.beauty-datatable__lock--scroll)")),
                                            l.getOption("layout.customScrollbar") && 10 != a.detectIE() && n > a.getBreakpoint("lg") ? e.initCustomScrollbar(r[0]) : e.initDefaultScrollbar(r))
                                        : t(i.tableBody).find(".beauty-datatable__row").length > 0 &&
                                        ((e.scrollHead = t(i.tableHead).find("> .beauty-datatable__row")),
                                            (e.scrollFoot = t(i.tableFoot).find("> .beauty-datatable__row")),
                                            l.getOption("layout.customScrollbar") && 10 != a.detectIE() && n > a.getBreakpoint("lg") ? e.initCustomScrollbar(i.tableBody) : e.initDefaultScrollbar(i.tableBody));
                                }
                            },
                            initDefaultScrollbar: function (a) {
                                (e.initPosition = t(a).scrollLeft()), t(a).css("overflow-y", "auto").off().on("scroll", e.onScrolling), t(a).css("overflow-x", "auto");
                            },
                            onScrolling: function (n) {
                                var o = t(this).scrollLeft(),
                                    i = t(this).scrollTop();
                                a.isRTL() && (o -= e.initPosition),
                                    t(e.scrollHead).css("left", -o),
                                    t(e.scrollFoot).css("left", -o),
                                    t(e.tableLocked).each(function (e, a) {
                                        l.isLocked() && (i -= 1), t(a).css("top", -i);
                                    });
                            },
                            initCustomScrollbar: function (a) {
                                (e.scrollable = a), l.initScrollbar(a), (e.initPosition = t(a).scrollLeft()), t(a).off().on("scroll", e.onScrolling);
                            },
                        };
                        return e.init(), e;
                    },
                    initScrollbar: function (e, n) {
                        if (e && e.nodeName) {
                            t(i.tableBody).css("overflow", "");
                            var o = t(e).data("ps");
                            a.hasClass(e, "ps") && void 0 !== o
                                ? o.update()
                                : ((o = new PerfectScrollbar(e, Object.assign({}, { wheelSpeed: 0.5, swipeEasing: !0, minScrollbarLength: 40, maxScrollbarLength: 300, suppressScrollX: l.getOption("rows.autoHide") && !l.isLocked() }, n))),
                                    t(e).data("ps", o)),
                                t(window).resize(function () {
                                    o.update();
                                });
                        }
                    },
                    setHeadTitle: function (e) {
                        void 0 === e && (e = i.tableHead), (e = t(e)[0]);
                        var n = o.columns,
                            r = e.getElementsByTagName("tr")[0],
                            s = e.getElementsByTagName("td");
                        void 0 === r && ((r = document.createElement("tr")), e.appendChild(r)),
                            t.each(n, function (e, n) {
                                var o = s[e];
                                if (
                                    (void 0 === o && ((o = document.createElement("th")), r.appendChild(o)),
                                        void 0 !== n.title &&
                                        ((o.innerHTML = n.title),
                                            o.setAttribute("data-field", n.field),
                                            a.addClass(o, n.class),
                                            void 0 !== n.autoHide && (!0 !== n.autoHide ? o.setAttribute("data-autohide-disabled", n.autoHide) : o.setAttribute("data-autohide-enabled", n.autoHide)),
                                            t(o).data(n)),
                                        void 0 !== n.attr &&
                                        t.each(n.attr, function (t, e) {
                                            o.setAttribute(t, e);
                                        }),
                                        void 0 !== n.textAlign)
                                ) {
                                    var l = void 0 !== i.textAlign[n.textAlign] ? i.textAlign[n.textAlign] : "";
                                    a.addClass(o, l);
                                }
                            }),
                            l.setupDOM(e);
                    },
                    dataRender: function (e) {
                        t(i.table).siblings(".beauty-datatable__pager").removeClass("beauty-datatable--paging-loaded");
                        var a = function () {
                            (i.dataSet = i.dataSet || []),
                                l.localDataUpdate();
                            var e = l.getDataSourceParam("pagination");
                            0 === e.perpage && (e.perpage = o.data.pageSize || 10), (e.total = i.dataSet.length);
                            var a = Math.max(e.perpage * (e.page - 1), 0),
                                n = Math.min(a + e.perpage, e.total);
                            return (i.dataSet = t(i.dataSet).slice(a, n)), e;
                        },
                            n = function (e) {
                                var n = function (e, a) {
                                    t(e.pager).hasClass("beauty-datatable--paging-loaded") || (t(e.pager).remove(), e.init(a)),
                                        t(e.pager).off().on("beauty-datatable--on-goto-page", function (n) {
                                            t(e.pager).remove(), e.init(a);
                                        });
                                    var n = Math.max(a.perpage * (a.page - 1), 0),
                                        o = Math.min(n + a.perpage, a.total);
                                    l.localDataUpdate(), (i.dataSet = t(i.dataSet).slice(n, o)), l.insertData();
                                };
                                if ((t(i.wrap).removeClass("beauty-datatable--error"), o.pagination))
                                    if (o.data.serverPaging && "local" !== o.data.type) {
                                        var r = l.getObject("meta", e || null);
                                        console.log(r);
                                        l.pagingObject = null !== r ? l.paging(r) : l.paging(a(), n);
                                    } else l.pagingObject = l.paging(a(), n);
                                else l.localDataUpdate();
                                l.insertData();
                            };
                        "local" === o.data.type || (!1 === o.data.serverSorting && "sort" === e) || (!1 === o.data.serverFiltering && "search" === e)
                            ? setTimeout(function () {
                                n(), l.setAutoColumns();
                            })
                            : l.getData().done(n);
                    },
                    insertData: function () {
                        //console.log(i.dataSet);
                        i.dataSet = i.dataSet || [];
                        var e = l.getDataSourceParam(),
                            n = e.pagination,
                            r = (Math.max(n.page, 1) - 1) * n.perpage,
                            s = Math.min(n.page, n.pages) * n.perpage,
                            d = {};
                        void 0 !== o.data.attr.rowProps && o.data.attr.rowProps.length && (d = o.data.attr.rowProps.slice(r, s));
                        var c = document.createElement("tbody");
                        c.style.visibility = "hidden";
                        var u = o.columns.length;
                        if (
                            (t.each(i.dataSet, function (n, r) {
                                var s = document.createElement("tr");
                                s.setAttribute("data-row", n),
                                    t(s).data("obj", r),
                                    void 0 !== d[n] &&
                                    t.each(d[n], function () {
                                        s.setAttribute(this.name, this.value);
                                    });
                                for (var p = 0; p < u; p += 1) {
                                    var f = o.columns[p],
                                        g = [];
                                    if ((l.getObject("sort.field", e) === f.field && g.push("beauty-datatable__cell--sorted"), void 0 !== f.textAlign)) {
                                        var h = void 0 !== i.textAlign[f.textAlign] ? i.textAlign[f.textAlign] : "";
                                        g.push(h);
                                    }
                                    void 0 !== f.class && g.push(f.class);
                                    var v = document.createElement("td");
                                    a.addClass(v, g.join(" ")),
                                        v.setAttribute("data-field", f.field),
                                        void 0 !== f.autoHide && (!0 !== f.autoHide ? v.setAttribute("data-autohide-disabled", f.autoHide) : v.setAttribute("data-autohide-enabled", f.autoHide)),
                                        (v.innerHTML = l.getObject(f.field, r)),
                                        v.setAttribute("aria-label", l.getObject(f.field, r)),
                                        s.appendChild(v);
                                }
                                c.appendChild(s);
                            }),
                                0 === i.dataSet.length)
                        ) {
                            var p = document.createElement("span");
                            a.addClass(p, "beauty-datatable--error"),
                                (p.innerHTML = l.getOption("translate.records.noRecords")),
                                c.appendChild(p),
                                t(i.wrap).addClass("beauty-datatable--error beauty-datatable--loaded"),
                                l.spinnerCallback(!1);
                        }
                        t(i.tableBody).replaceWith(c), (i.tableBody = c), l.setupDOM(i.table), l.setupCellField([i.tableBody]), l.setupTemplateCell(i.tableBody), l.layoutUpdate();
                    },
                    updateTableComponents: function () {
                        (i.tableHead = t(i.table).children("thead").get(0)), (i.tableBody = t(i.table).children("tbody").get(0)), (i.tableFoot = t(i.table).children("tfoot").get(0));
                    },
                    getData: function () {
                        var e = { dataType: "json", method: "POST", data: {}, timeout: l.getOption("data.source.read.timeout") || 3e4 };
                        if (("local" === o.data.type && (e.url = o.data.source), "remote" === o.data.type)) {
                            var getDataSourceParam = l.getDataSourceParam();

                            //if (a.pagination.pages === null) {
                            //    a.pagination.pages = a.pagination.total;
                            //    a.pagination.perpage = a.pagination.total;
                            //}
                            l.getOption("data.serverPaging") || delete getDataSourceParam.pagination,
                                l.getOption("data.serverSorting") || delete getDataSourceParam.sort,
                                (e.data = t.extend({}, e.data, l.getOption("data.source.read.params"), getDataSourceParam)),
                                "string" != typeof (e = t.extend({}, e, l.getOption("data.source.read"))).url && (e.url = l.getOption("data.source.read")),
                                "string" != typeof e.url && (e.url = l.getOption("data.source"));
                        }
                        if (e.data.pagination.perpage === null) e.data.pagination.perpage = 10;
                        return t
                            .ajax(e)
                            .done(function (e, a, n) {
                                //console.log(n);
                                (i.lastResponse = e), (i.dataSet = i.originalDataSet = l.dataMapCallback(e)), l.setAutoColumns(), t(i).trigger("beauty-datatable--on-ajax-done", [i.dataSet]);
                            })
                            .fail(function (e, a, n) {
                                t(i).trigger("beauty-datatable--on-ajax-fail", [e]),
                                    t(i.tableBody).html(t("<span/>").addClass("beauty-datatable--error").html(l.getOption("translate.records.noRecords"))),
                                    t(i.wrap).addClass("beauty-datatable--error beauty-datatable--loaded"),
                                    l.spinnerCallback(!1);
                            })
                            .always(function () { });
                    },
                    paging: function (e, n) {
                        var o = {
                            meta: null,
                            pager: null,
                            paginateEvent: null,
                            pagerLayout: { pagination: null, info: null },
                            callback: null,
                            init: function (e) {
                                console.log(e);
                                (o.meta = e),
                                    (o.meta.page = parseInt(o.meta.page)),
                                    (o.meta.pages = parseInt(o.meta.pages)),
                                    (o.meta.perpage = parseInt(o.meta.perpage)),
                                    (o.meta.total = parseInt(o.meta.total)),
                                    (o.meta.pages = Math.max(Math.ceil(o.meta.total / o.meta.perpage), 1)),
                                    o.meta.page > o.meta.pages && (o.meta.page = o.meta.pages),
                                    (o.paginateEvent = l.getTablePrefix("paging")),
                                    (o.pager = t(i.table).siblings(".beauty-datatable__pager")),
                                    t(o.pager).hasClass("beauty-datatable--paging-loaded") ||
                                    (t(o.pager).remove(),
                                        0 !== o.meta.pages &&
                                        (l.setDataSourceParam("pagination", {
                                            page: o.meta.page,
                                            pages: o.meta.pages,
                                            perpage: o.meta.perpage,
                                            total: o.meta.total
                                        }),
                                            (o.callback = o.serverCallback),
                                            "function" == typeof n && (o.callback = n),
                                            o.addPaginateEvent(),
                                            o.populate(),
                                            (o.meta.page = Math.max(o.meta.page || 1, o.meta.page)),
                                            t(i).trigger(o.paginateEvent, o.meta),
                                            o.pagingBreakpoint.call(),
                                            t(window).resize(o.pagingBreakpoint)));
                            },
                            serverCallback: function (t, e) {
                                l.dataRender();
                            },
                            populate: function () {
                                var e = l.getOption("layout.icons.pagination"),
                                    a = l.getOption("translate.toolbar.pagination.items.default");
                                o.pager = t("<div/>").addClass("beauty-datatable__pager beauty-datatable--paging-loaded");
                                var n = t("<ul/>").addClass("beauty-datatable__pager-nav");
                                (o.pagerLayout.pagination = n),
                                    t("<li/>")
                                        .append(
                                            t("<a/>").attr("title", a.first).addClass("beauty-datatable__pager-link beauty-datatable__pager-link--first").append(t("<i/>").addClass(e.first)).on("click", o.gotoMorePage).attr("data-page", 1)
                                        )
                                        .appendTo(n),
                                    t("<li/>")
                                        .append(t("<a/>").attr("title", a.prev).addClass("beauty-datatable__pager-link beauty-datatable__pager-link--prev").append(t("<i/>").addClass(e.prev)).on("click", o.gotoMorePage))
                                        .appendTo(n),
                                    t("<li/>")
                                        .append(t("<a/>").attr("title", a.more).addClass("beauty-datatable__pager-link beauty-datatable__pager-link--more-prev").html(t("<i/>").addClass(e.more)).on("click", o.gotoMorePage))
                                        .appendTo(n),
                                    t("<li/>")
                                        .append(
                                            t("<input/>")
                                                .attr("type", "text")
                                                .addClass("beauty-pager-input form-control")
                                                .attr("title", a.input)
                                                .on("keyup", function () {
                                                    t(this).attr("data-page", Math.abs(t(this).val()));
                                                })
                                                .on("keypress", function (t) {
                                                    13 === t.which && o.gotoMorePage(t);
                                                })
                                        )
                                        .appendTo(n);

                                var r = l.getOption("toolbar.items.pagination.pages.desBeautyop.pagesNumber");
                                if (o.meta.total <= o.meta.perPage && isNaN(o.meta.pages)) {
                                    r = 1;
                                    o.meta.pages = 1;
                                }

                                var s = Math.ceil(o.meta.page / r) * r,
                                    d = s - r;
                                s > o.meta.pages && (s = o.meta.pages);
                                for (var c = d; c < s; c++) {
                                    var u = c + 1;
                                    t("<li/>").append(t("<a/>").addClass("beauty-datatable__pager-link beauty-datatable__pager-link-number").text(u).attr("data-page", u).attr("title", u).on("click", o.gotoPage)).appendTo(n);
                                }
                                t("<li/>")
                                    .append(t("<a/>").attr("title", a.more).addClass("beauty-datatable__pager-link beauty-datatable__pager-link--more-next").html(t("<i/>").addClass(e.more)).on("click", o.gotoMorePage))
                                    .appendTo(n),
                                    t("<li/>")
                                        .append(t("<a/>").attr("title", a.next).addClass("beauty-datatable__pager-link beauty-datatable__pager-link--next").append(t("<i/>").addClass(e.next)).on("click", o.gotoMorePage))
                                        .appendTo(n),
                                    t("<li/>")
                                        .append(
                                            t("<a/>")
                                                .attr("title", a.last)
                                                .addClass("beauty-datatable__pager-link beauty-datatable__pager-link--last")
                                                .append(t("<i/>").addClass(e.last))
                                                .on("click", o.gotoMorePage)
                                                .attr("data-page", o.meta.pages)
                                        )
                                        .appendTo(n),
                                    l.getOption("toolbar.items.info") && (o.pagerLayout.info = t("<div/>").addClass("beauty-datatable__pager-info").append(t("<span/>").addClass("beauty-datatable__pager-detail"))),
                                    t.each(l.getOption("toolbar.layout"), function (e, a) {
                                        t(o.pagerLayout[a]).appendTo(o.pager);
                                    });

                                var p = t("<select/>")
                                    .addClass("selectpicker beauty-datatable__pager-size")
                                    .attr("title", l.getOption("translate.toolbar.pagination.items.default.select"))
                                    .attr("data-width", "60px")
                                    .attr("data-container", "body")
                                    .val(o.meta.perpage)
                                    .on("change", o.updatePerpage)
                                    .prependTo(o.pagerLayout.info),
                                    f = l.getOption("toolbar.items.pagination.pageSizeSelect");
                                0 == f.length && (f = [5, 10, 20, 30, 50, 100]),
                                    t.each(f, function (e, a) {
                                        var n = a;
                                        -1 === a && (n = l.getOption("translate.toolbar.pagination.items.default.all")), t("<option/>").attr("value", a).html(n).appendTo(p);
                                    }),
                                    t(i).ready(function () {
                                        t(".selectpicker")
                                            .selectpicker()
                                            .on("hide.bs.select", function () {
                                                t(this).closest(".bootstrap-select").removeClass("dropup");
                                            })
                                            .siblings(".dropdown-toggle")
                                            .attr("title", l.getOption("translate.toolbar.pagination.items.default.select"));
                                    }),
                                    o.paste();
                            },
                            paste: function () {
                                t.each(t.unique(l.getOption("toolbar.placement")), function (e, a) {
                                    "bottom" === a && t(o.pager).clone(!0).insertAfter(i.table), "top" === a && t(o.pager).clone(!0).addClass("beauty-datatable__pager--top").insertBefore(i.table);
                                });
                            },
                            gotoMorePage: function (e) {
                                if ((e.preventDefault(), "disabled" === t(this).attr("disabled"))) return !1;
                                var a = t(this).attr("data-page");
                                return void 0 === a && (a = t(e.target).attr("data-page")), o.openPage(parseInt(a)), !1;
                            },
                            gotoPage: function (e) {
                                e.preventDefault(), t(this).hasClass("beauty-datatable__pager-link--active") || o.openPage(parseInt(t(this).data("page")));
                            },
                            openPage: function (e) {
                                (o.meta.page = parseInt(e)), t(i).trigger(o.paginateEvent, o.meta), o.callback(o, o.meta), t(o.pager).trigger("beauty-datatable--on-goto-page", o.meta);
                            },
                            updatePerpage: function (e) {
                                if (o.meta.perpage === null) o.meta.perpage = 10;
                                if (o.meta.pages === null) o.meta.pages = Math.ceil(o.meta.total / o.meta.perpage);

                                e.preventDefault(),
                                    t(this).selectpicker("toggle"),
                                    (o.pager = t(i.table).siblings(".beauty-datatable__pager").removeClass("beauty-datatable--paging-loaded")),
                                    e.originalEvent && (o.meta.perpage = parseInt(t(this).val())),
                                    t(o.pager).find("select.beauty-datatable__pager-size").val(o.meta.perpage).attr("data-selected", o.meta.perpage),
                                    l.setDataSourceParam("pagination", {
                                        page: o.meta.page,
                                        pages: o.meta.pages,
                                        perpage: o.meta.perpage,
                                        total: o.meta.total,
                                    }),
                                    t(o.pager).trigger("beauty-datatable--on-update-perpage", o.meta),
                                    t(i).trigger(o.paginateEvent, o.meta),
                                    o.callback(o, o.meta),
                                    o.updateInfo.call();
                            },
                            addPaginateEvent: function (e) {
                                t(i)
                                    .off(o.paginateEvent)
                                    .on(o.paginateEvent, function (e, a) {
                                        l.spinnerCallback(!0), (o.pager = t(i.table).siblings(".beauty-datatable__pager"));
                                        var n = t(o.pager).find(".beauty-datatable__pager-nav");
                                        t(n).find(".beauty-datatable__pager-link--active").removeClass("beauty-datatable__pager-link--active"),
                                            t(n)
                                                .find('.beauty-datatable__pager-link-number[data-page="' + a.page + '"]')
                                                .addClass("beauty-datatable__pager-link--active"),
                                            isNaN(a.page)
                                                ? t(n).find(".beauty-datatable__pager-link--prev").attr("disabled", "disabled").attr("data-page", 0)
                                                : t(n)
                                                    .find(".beauty-datatable__pager-link--prev")
                                                    .attr("disabled", "")
                                                    .attr("data-page", Math.max(a.page - 1, 1)),
                                            isNaN(a.page)
                                                ? t(n).find(".beauty-datatable__pager-link--next").attr("disabled", "disabled").attr("data-page", 0)
                                                : t(n)
                                                    .find(".beauty-datatable__pager-link--next")
                                                    .attr("disabled", "")
                                                    .attr("data-page", Math.min(a.page + 1, a.pages)),
                                            t(o.pager).each(function () {
                                                t(this).find('.beauty-pager-input[type="text"]').prop("value", a.page);
                                            }),
                                            l.setDataSourceParam("pagination", {
                                                page: o.meta.page,
                                                pages: o.meta.pages,
                                                perpage: o.meta.perpage,
                                                total: o.meta.total,
                                            }),
                                            t(o.pager).find("select.beauty-datatable__pager-size").val(a.perpage).attr("data-selected", a.perpage),
                                            t(i.table).find('.beauty-checkbox > [type="checkbox"]').prop("checked", !1),
                                            t(i.table).find(".beauty-datatable__row--active").removeClass("beauty-datatable__row--active"),
                                            o.updateInfo.call(),
                                            o.pagingBreakpoint.call();
                                    });
                            },
                            updateInfo: function () {
                                if (isNaN(o.meta.pages) && isNaN(o.meta.perpage)) {
                                    o.meta.perpage = 10;
                                    o.meta.pages = Math.ceil(o.meta.total / 10);
                                }

                                var e = Math.max(o.meta.perpage * (o.meta.page - 1) + 1, 1),
                                    a = Math.min(e + o.meta.perpage - 1, o.meta.total);
                                t(o.pager)
                                    .find(".beauty-datatable__pager-info")
                                    .find(".beauty-datatable__pager-detail")
                                    .html(
                                        l.dataPlaceholder(l.getOption("translate.toolbar.pagination.items.info"), {
                                            start: 0 === o.meta.total ? 0 : e,
                                            end: -1 === o.meta.perpage ? o.meta.total : a,
                                            pageSize: -1 === o.meta.perpage || o.meta.perpage >= o.meta.total ? o.meta.total : o.meta.perpage,
                                            total: o.meta.total,
                                        })
                                    );
                            },
                            pagingBreakpoint: function () {
                                var e = t(i.table).siblings(".beauty-datatable__pager").find(".beauty-datatable__pager-nav");
                                if (0 !== t(e).length) {
                                    var n = l.getCurrentPage(),
                                        r = t(e).find(".beauty-pager-input").closest("li");
                                    t(e).find("li").show(),
                                        t.each(l.getOption("toolbar.items.pagination.pages"), function (i, s) {
                                            if (a.isInResponsiveRange(i)) {
                                                switch (i) {
                                                    case "desBeautyop":
                                                    case "tablet":
                                                        Math.ceil(n / s.pagesNumber), s.pagesNumber, s.pagesNumber;
                                                        t(r).hide(), (o.meta = l.getDataSourceParam("pagination")), o.paginationUpdate();
                                                        break;
                                                    case "mobile":
                                                        t(r).show(),
                                                            t(e).find(".beauty-datatable__pager-link--more-prev").closest("li").hide(),
                                                            t(e).find(".beauty-datatable__pager-link--more-next").closest("li").hide(),
                                                            t(e).find(".beauty-datatable__pager-link-number").closest("li").hide();
                                                }
                                                return !1;
                                            }
                                        });
                                }
                            },
                            paginationUpdate: function () {
                                var e = t(i.table).siblings(".beauty-datatable__pager").find(".beauty-datatable__pager-nav"),
                                    a = t(e).find(".beauty-datatable__pager-link--more-prev"),
                                    n = t(e).find(".beauty-datatable__pager-link--more-next"),
                                    r = t(e).find(".beauty-datatable__pager-link--first"),
                                    s = t(e).find(".beauty-datatable__pager-link--prev"),
                                    d = t(e).find(".beauty-datatable__pager-link--next"),
                                    c = t(e).find(".beauty-datatable__pager-link--last"),
                                    u = t(e).find(".beauty-datatable__pager-link-number"),
                                    p = Math.max(t(u).first().data("page") - 1, 1);
                                t(a).each(function (e, a) {
                                    t(a).attr("data-page", p);
                                }),
                                    1 === p ? t(a).parent().hide() : t(a).parent().show();
                                var f = Math.min(t(u).last().data("page") + 1, o.meta.pages);
                                t(n).each(function (e, a) {
                                    t(n).attr("data-page", f).show();
                                }),
                                    f === o.meta.pages && f === t(u).last().data("page") ? t(n).parent().hide() : t(n).parent().show(),
                                    1 === o.meta.page
                                        ? (t(r).attr("disabled", !0).addClass("beauty-datatable__pager-link--disabled"), t(s).attr("disabled", !0).addClass("beauty-datatable__pager-link--disabled"))
                                        : (t(r).removeAttr("disabled").removeClass("beauty-datatable__pager-link--disabled"), t(s).removeAttr("disabled").removeClass("beauty-datatable__pager-link--disabled")),
                                    o.meta.page === o.meta.pages
                                        ? (t(d).attr("disabled", !0).addClass("beauty-datatable__pager-link--disabled"), t(c).attr("disabled", !0).addClass("beauty-datatable__pager-link--disabled"))
                                        : (t(d).removeAttr("disabled").removeClass("beauty-datatable__pager-link--disabled"), t(c).removeAttr("disabled").removeClass("beauty-datatable__pager-link--disabled"));
                                var g = l.getOption("toolbar.items.pagination.navigation");
                                g.first || t(r).remove(), g.prev || t(s).remove(), g.next || t(d).remove(), g.last || t(c).remove(), g.more || (t(a).remove(), t(n).remove());
                            },
                        };
                        return o.init(e), o;
                    },
                    columnHide: function () {
                        var e = a.getViewPort().width;
                        t.each(o.columns, function (n, o) {
                            if (void 0 !== o.responsive || void 0 !== o.visible) {
                                var r = o.field,
                                    s = t.grep(t(i.table).find(".beauty-datatable__cell"), function (e, a) {
                                        return r === t(e).data("field");
                                    });
                                setTimeout(function () {
                                    !1 === l.getObject("visible", o)
                                        ? t(s).hide()
                                        : (a.getBreakpoint(l.getObject("responsive.hidden", o)) >= e ? t(s).hide() : t(s).show(), a.getBreakpoint(l.getObject("responsive.visible", o)) <= e ? t(s).show() : t(s).hide());
                                });
                            }
                        });
                    },
                    setupSubDatatable: function () {
                        var e = l.getOption("detail.content");
                        if ("function" == typeof e && !(t(i.table).find(".beauty-datatable__subtable").length > 0)) {
                            t(i.wrap).addClass("beauty-datatable--subtable"), (o.columns[0].subtable = !0);
                            var a = function (a) {
                                a.preventDefault();
                                var n = t(this).closest(".beauty-datatable__row"),
                                    r = t(n).next(".beauty-datatable__row-subtable");
                                0 === t(r).length &&
                                    ((r = t("<tr/>").addClass("beauty-datatable__row-subtable beauty-datatable__row-loading").hide().append(t("<td/>").addClass("beauty-datatable__subtable").attr("colspan", l.getTotalColumns()))),
                                        t(n).after(r),
                                        t(n).hasClass("beauty-datatable__row--even") && t(r).addClass("beauty-datatable__row-subtable--even")),
                                    t(r).toggle();
                                var s = t(r).find(".beauty-datatable__subtable"),
                                    d = t(this).closest("[data-field]:first-child").find(".beauty-datatable__toggle-subtable").data("value"),
                                    c = t(this).find("i").removeAttr("class");
                                t(n).hasClass("beauty-datatable__row--subtable-expanded")
                                    ? (t(c).addClass(l.getOption("layout.icons.rowDetail.collapse")), t(n).removeClass("beauty-datatable__row--subtable-expanded"), t(i).trigger("beauty-datatable--on-collapse-subtable", [n]))
                                    : (t(c).addClass(l.getOption("layout.icons.rowDetail.expand")), t(n).addClass("beauty-datatable__row--subtable-expanded"), t(i).trigger("beauty-datatable--on-expand-subtable", [n])),
                                    0 === t(s).find(".beauty-datatable").length &&
                                    (t.map(i.dataSet, function (t, e) {
                                        return d === t[o.columns[0].field] && ((a.data = t), !0);
                                    }),
                                        (a.detailCell = s),
                                        (a.parentRow = n),
                                        (a.subTable = s),
                                        e(a),
                                        t(s)
                                            .children(".beauty-datatable")
                                            .on("beauty-datatable--on-init", function (e) {
                                                t(r).removeClass("beauty-datatable__row-loading");
                                            }),
                                        "local" === l.getOption("data.type") && t(r).removeClass("beauty-datatable__row-loading"));
                            },
                                n = o.columns;
                            t(i.tableBody)
                                .find(".beauty-datatable__row")
                                .each(function (e, o) {
                                    t(o)
                                        .find(".beauty-datatable__cell")
                                        .each(function (e, o) {
                                            var i = t.grep(n, function (e, a) {
                                                return t(o).data("field") === e.field;
                                            })[0];
                                            if (void 0 !== i) {
                                                var r = t(o).text();
                                                if (void 0 !== i.subtable && i.subtable) {
                                                    if (t(o).find(".beauty-datatable__toggle-subtable").length > 0) return;
                                                    t(o).html(
                                                        t("<a/>")
                                                            .addClass("beauty-datatable__toggle-subtable")
                                                            .attr("href", "#")
                                                            .attr("data-value", r)
                                                            .attr("title", l.getOption("detail.title"))
                                                            .on("click", a)
                                                            .append(t("<i/>").css("width", t(o).data("width")).addClass(l.getOption("layout.icons.rowDetail.collapse")))
                                                    );
                                                }
                                            }
                                        });
                                });
                        }
                    },
                    dataMapCallback: function (t) {
                        var e = t;
                        return "function" == typeof l.getOption("data.source.read.map") ? l.getOption("data.source.read.map")(t) : (void 0 !== t && void 0 !== t.data && (e = t.data), e);
                    },
                    isSpinning: !1,
                    spinnerCallback: function (t, e) {
                        void 0 === e && (e = i);
                        var a = l.getOption("layout.spinner");
                        void 0 !== a &&
                            a &&
                            (t
                                ? l.isSpinning || (void 0 !== a.message && !0 === a.message && (a.message = l.getOption("translate.records.processing")), (l.isSpinning = !0), void 0 !== n && n.block(e, a))
                                : ((l.isSpinning = !1), void 0 !== n && n.unblock(e)));
                    },
                    sortCallback: function (e, a, n) {
                        var o = n.type || "string",
                            i = n.format || "",
                            l = n.field;
                        return t(e).sort(function (n, r) {
                            var s = n[l],
                                d = r[l];
                            switch (o) {
                                case "date":
                                    if ("undefined" == typeof moment) throw new Error("Moment.js is required.");
                                    var c = moment(s, i).diff(moment(d, i));
                                    return "asc" === a ? (c > 0 ? 1 : c < 0 ? -1 : 0) : c < 0 ? 1 : c > 0 ? -1 : 0;
                                case "number":
                                    return (
                                        isNaN(parseFloat(s)) && null != s && (s = Number(s.replace(/[^0-9\.-]+/g, ""))),
                                        isNaN(parseFloat(d)) && null != d && (d = Number(d.replace(/[^0-9\.-]+/g, ""))),
                                        (s = parseFloat(s)),
                                        (d = parseFloat(d)),
                                        "asc" === a ? (s > d ? 1 : s < d ? -1 : 0) : s < d ? 1 : s > d ? -1 : 0
                                    );
                                case "html":
                                    return t(e).sort(function (e, n) {
                                        return (s = t(e[l]).text()), (d = t(n[l]).text()), "asc" === a ? (s > d ? 1 : s < d ? -1 : 0) : s < d ? 1 : s > d ? -1 : 0;
                                    });
                                case "string":
                                default:
                                    return "asc" === a ? (s > d ? 1 : s < d ? -1 : 0) : s < d ? 1 : s > d ? -1 : 0;
                            }
                        });
                    },
                    log: function (t, e) {
                        void 0 === e && (e = ""), i.debug && console.log(t, e);
                    },
                    autoHide: function () {
                        var e = !1,
                            a = t(i.table).find("[data-autohide-enabled]");
                        a.length && ((e = !0), a.hide());
                        var n = function (e) {
                            e.preventDefault();
                            var a = t(this).closest(".beauty-datatable__row"),
                                n = t(a).next();
                            if (t(n).hasClass("beauty-datatable__row-detail")) t(this).find("i").removeClass(l.getOption("layout.icons.rowDetail.expand")).addClass(l.getOption("layout.icons.rowDetail.collapse")), t(n).remove();
                            else {
                                t(this).find("i").removeClass(l.getOption("layout.icons.rowDetail.collapse")).addClass(l.getOption("layout.icons.rowDetail.expand"));
                                var i = t(a).find(".beauty-datatable__cell:hidden").clone().show();
                                n = t("<tr/>").addClass("beauty-datatable__row-detail").insertAfter(a);
                                var r = t("<td/>").addClass("beauty-datatable__detail").attr("colspan", l.getTotalColumns()).appendTo(n),
                                    s = t("<table/>");
                                t(i).each(function () {
                                    var e = t(this).data("field"),
                                        a = t.grep(o.columns, function (t, a) {
                                            return e === t.field;
                                        })[0];
                                    (void 0 !== a && !1 === a.visible) ||
                                        t(s).append(
                                            t('<tr class="beauty-datatable__row"></tr>')
                                                .append(t('<td class="beauty-datatable__cell"></td>').append(t("<span/>").append(a.title)))
                                                .append(this)
                                        );
                                }),
                                    t(r).append(s);
                            }
                        };
                        setTimeout(function () {
                            t(i.table).find(".beauty-datatable__cell").show(),
                                t(i.tableBody).each(function () {
                                    for (var a = 0; t(this)[0].offsetWidth < t(this)[0].scrollWidth && a < o.columns.length;)
                                        t(i.table)
                                            .find(".beauty-datatable__row")
                                            .each(function (a) {
                                                var n = t(this).find(".beauty-datatable__cell:not(:hidden):not([data-autohide-disabled])").last();
                                                t(n).hide(), (e = !0);
                                            }),
                                            a++;
                                }),
                                e &&
                                t(i.tableBody)
                                    .find(".beauty-datatable__row")
                                    .each(function () {
                                        0 === t(this).find(".beauty-datatable__toggle-detail").length &&
                                            t(this).prepend(
                                                t("<td/>")
                                                    .addClass("beauty-datatable__cell beauty-datatable__toggle-detail")
                                                    .append(
                                                        t("<a/>")
                                                            .addClass("beauty-datatable__toggle-detail")
                                                            .attr("href", "")
                                                            .on("click", n)
                                                            .append('<i class="' + l.getOption("layout.icons.rowDetail.collapse") + '"></i>')
                                                    )
                                            ),
                                            0 === t(i.tableHead).find(".beauty-datatable__toggle-detail").length
                                                ? (t(i.tableHead).find(".beauty-datatable__row").first().prepend('<th class="beauty-datatable__cell beauty-datatable__toggle-detail"><span></span></th>'),
                                                    t(i.tableFoot).find(".beauty-datatable__row").first().prepend('<th class="beauty-datatable__cell beauty-datatable__toggle-detail"><span></span></th>'))
                                                : t(i.tableHead).find(".beauty-datatable__toggle-detail").find("span");
                                    });
                        }),
                            l.adjustCellsWidth.call();
                    },
                    setAutoColumns: function () {
                        l.getOption("data.autoColumns") &&
                            (t.each(i.dataSet[0], function (e, a) {
                                0 ===
                                    t.grep(o.columns, function (t, a) {
                                        return e === t.field;
                                    }).length && o.columns.push({ field: e, title: e });
                            }),
                                t(i.tableHead).find(".beauty-datatable__row").remove(),
                                l.setHeadTitle(),
                                l.getOption("layout.footer") && (t(i.tableFoot).find(".beauty-datatable__row").remove(), l.setHeadTitle(i.tableFoot)));
                    },
                    isLocked: function () {
                        var t = l.lockEnabledColumns();
                        return t.left.length > 0 || t.right.length > 0;
                    },
                    isSubtable: function () {
                        return a.hasClass(i.wrap[0], "beauty-datatable--subtable") || !1;
                    },
                    getExtraSpace: function (e) {
                        return parseInt(t(e).css("paddingRight")) + parseInt(t(e).css("paddingLeft")) + (parseInt(t(e).css("marginRight")) + parseInt(t(e).css("marginLeft"))) + Math.ceil(t(e).css("border-right-width").replace("px", ""));
                    },
                    dataPlaceholder: function (e, a) {
                        var n = e;
                        return (
                            t.each(a, function (t, e) {
                                n = n.replace("{{" + t + "}}", e);
                            }),
                            n
                        );
                    },
                    getTableId: function (e) {
                        void 0 === e && (e = "");
                        var a = t(i).attr("id");
                        return void 0 === a && (a = t(i).attr("class").split(" ")[0]), a + e;
                    },
                    getTablePrefix: function (t) {
                        return void 0 !== t && (t = "-" + t), l.getTableId() + "-" + l.getDepth() + t;
                    },
                    getDepth: function () {
                        var e = 0,
                            a = i.table;
                        do {
                            (a = t(a).parents(".beauty-datatable__table")), e++;
                        } while (t(a).length > 0);
                        return e;
                    },
                    stateKeep: function (t, e) {
                        if (isNaN(e.pagination.pages) || (e.pagination.pages === null && e.pagination.perpage === null)) {
                            e.pagination.pages = Math.ceil(e.pagination.total / 10);
                            e.pagination.perpage = 10;
                        }
                        (t = l.getTablePrefix(t)),
                            !1 !== l.getOption("data.saveState") &&
                            (l.getOption("data.saveState.webstorage") && localStorage && localStorage.setItem(t, JSON.stringify(e)), l.getOption("data.saveState.cookie") && Cookies.set(t, JSON.stringify(e)));
                    },
                    stateGet: function (t, e) {
                        if (((t = l.getTablePrefix(t)), !1 !== l.getOption("data.saveState"))) {
                            var a = null;
                            return null != (a = l.getOption("data.saveState.webstorage") && localStorage ? localStorage.getItem(t) : Cookies.get(t)) ? JSON.parse(a) : void 0;
                        }
                    },
                    stateUpdate: function (e, a) {
                        var n = l.stateGet(e);
                        null == n && (n = {}), l.stateKeep(e, t.extend({}, n, a));
                    },
                    stateRemove: function (t) {
                        (t = l.getTablePrefix(t)), localStorage && localStorage.removeItem(t), Cookies.remove(t);
                    },
                    getTotalColumns: function (e) {
                        return void 0 === e && (e = i.tableBody), t(e).find(".beauty-datatable__row").first().find(".beauty-datatable__cell").length;
                    },
                    getOneRow: function (e, a, n) {
                        void 0 === n && (n = !0);
                        var o = t(e).find(".beauty-datatable__row:not(.beauty-datatable__row-detail):nth-child(" + a + ")");
                        return n && (o = o.find(".beauty-datatable__cell")), o;
                    },
                    sortColumn: function (e, a, n) {
                        void 0 === a && (a = "asc"), void 0 === n && (n = !1);
                        var o = t(e).index(),
                            l = t(i.tableBody).find(".beauty-datatable__row"),
                            r = t(e).closest(".beauty-datatable__lock").index();
                        -1 !== r &&
                            (l = t(i.tableBody)
                                .find(".beauty-datatable__lock:nth-child(" + (r + 1) + ")")
                                .find(".beauty-datatable__row"));
                        var s = t(l).parent();
                        t(l)
                            .sort(function (e, i) {
                                var l = t(e)
                                    .find("td:nth-child(" + o + ")")
                                    .text(),
                                    r = t(i)
                                        .find("td:nth-child(" + o + ")")
                                        .text();
                                return n && ((l = parseInt(l)), (r = parseInt(r))), "asc" === a ? (l > r ? 1 : l < r ? -1 : 0) : l < r ? 1 : l > r ? -1 : 0;
                            })
                            .appendTo(s);
                    },
                    sorting: function () {
                        var e = {
                            init: function () {
                                o.sortable && (t(i.tableHead).find(".beauty-datatable__cell:not(.beauty-datatable__cell--check)").addClass("beauty-datatable__cell--sort").off("click").on("click", e.sortClick), e.setIcon());
                            },
                            setIcon: function () {
                                var e = l.getDataSourceParam("sort");
                                if (!t.isEmptyObject(e)) {
                                    var a = l.getColumnByField(e.field);
                                    if (void 0 === a || void 0 === a.sortable || !1 !== a.sortable) {
                                        var n = t(i.tableHead)
                                            .find('.beauty-datatable__cell[data-field="' + e.field + '"]')
                                            .attr("data-sort", e.sort),
                                            o = t(n).find("span"),
                                            r = t(o).find("i"),
                                            s = l.getOption("layout.icons.sort");
                                        t(r).length > 0 ? t(r).removeAttr("class").addClass(s[e.sort]) : t(o).append(t("<i/>").addClass(s[e.sort])), t(n).addClass("beauty-datatable__cell--sorted");
                                    }
                                }
                            },
                            sortClick: function (n) {
                                var r = l.getDataSourceParam("sort"),
                                    s = t(this).data("field"),
                                    d = l.getColumnByField(s);
                                if (
                                    (void 0 === d.sortable || !1 !== d.sortable) &&
                                    (t(i.tableHead).find("th").removeClass("beauty-datatable__cell--sorted"),
                                        a.addClass(this, "beauty-datatable__cell--sorted"),
                                        t(i.tableHead).find(".beauty-datatable__cell > span > i").remove(),
                                        o.sortable)
                                ) {
                                    l.spinnerCallback(!0);
                                    var c = "desc";
                                    l.getObject("field", r) === s && (c = l.getObject("sort", r)),
                                        (r = { field: s, sort: (c = void 0 === c || "desc" === c ? "asc" : "desc") }),
                                        l.setDataSourceParam("sort", r),
                                        e.setIcon(),
                                        setTimeout(function () {
                                            l.dataRender("sort"), t(i).trigger("beauty-datatable--on-sort", r);
                                        }, 300);
                                }
                            },
                        };
                        e.init();
                    },
                    localDataUpdate: function () {
                        var e = l.getDataSourceParam();
                        void 0 === i.originalDataSet && (i.originalDataSet = i.dataSet);
                        var a = l.getObject("sort.field", e),
                            n = l.getObject("sort.sort", e),
                            o = l.getColumnByField(a);
                        if (
                            (void 0 !== o && !0 !== l.getOption("data.serverSorting")
                                ? "function" == typeof o.sortCallback
                                    ? (i.dataSet = o.sortCallback(i.originalDataSet, n, o))
                                    : (i.dataSet = l.sortCallback(i.originalDataSet, n, o))
                                : (i.dataSet = i.originalDataSet),
                                "object" == typeof e.query && !l.getOption("data.serverFiltering"))
                        ) {
                            e.query = e.query || {};
                            var r = function (t) {
                                for (var e in t)
                                    if (t.hasOwnProperty(e))
                                        if ("string" == typeof t[e]) {
                                            if (t[e].toLowerCase() == s || -1 !== t[e].toLowerCase().indexOf(s)) return !0;
                                        } else if ("number" == typeof t[e]) {
                                            if (t[e] === s) return !0;
                                        } else if ("object" == typeof t[e] && r(t[e])) return !0;
                                return !1;
                            },
                                s = t(l.getOption("search.input")).val();
                            void 0 !== s && "" !== s && ((s = s.toLowerCase()), (i.dataSet = t.grep(i.dataSet, r)), delete e.query[l.getGeneralSearchKey()]),
                                t.each(e.query, function (t, a) {
                                    "" === a && delete e.query[t];
                                }),
                                (i.dataSet = l.filterArray(i.dataSet, e.query)),
                                (i.dataSet = i.dataSet.filter(function () {
                                    return !0;
                                }));
                        }
                        return i.dataSet;
                    },
                    filterArray: function (e, a, n) {
                        if ("object" != typeof e) return [];
                        if ((void 0 === n && (n = "AND"), "object" != typeof a)) return e;
                        if (((n = n.toUpperCase()), -1 === t.inArray(n, ["AND", "OR", "NOT"]))) return [];
                        var o = Object.keys(a).length,
                            i = [];
                        return (
                            t.each(e, function (e, r) {
                                var s = r,
                                    d = 0;
                                t.each(a, function (t, e) {
                                    e = e instanceof Array ? e : [e];
                                    var a = l.getObject(t, s);
                                    if (void 0 !== a && a) {
                                        var n = a.toString().toLowerCase();
                                        e.forEach(function (t, e) {
                                            (t.toString().toLowerCase() != n && -1 === n.indexOf(t.toString().toLowerCase())) || d++;
                                        });
                                    }
                                }),
                                    (("AND" == n && d == o) || ("OR" == n && d > 0) || ("NOT" == n && 0 == d)) && (i[e] = r);
                            }),
                            (e = i)
                        );
                    },
                    resetScroll: function () {
                        void 0 === o.detail && 1 === l.getDepth() && (t(i.table).find(".beauty-datatable__row").css("left", 0), t(i.table).find(".beauty-datatable__lock").css("top", 0), t(i.tableBody).scrollTop(0));
                    },
                    getColumnByField: function (e) {
                        var a;
                        if (void 0 !== e)
                            return (
                                t.each(o.columns, function (t, n) {
                                    if (e === n.field) return (a = n), !1;
                                }),
                                a
                            );
                    },
                    getDefaultSortColumn: function () {
                        var e;
                        return (
                            t.each(o.columns, function (a, n) {
                                if (void 0 !== n.sortable && -1 !== t.inArray(n.sortable, ["asc", "desc"])) return (e = { sort: n.sortable, field: n.field }), !1;
                            }),
                            e
                        );
                    },
                    getHiddenDimensions: function (e, a) {
                        var n = { position: "absolute", visibility: "hidden", display: "block" },
                            o = { width: 0, height: 0, innerWidth: 0, innerHeight: 0, outerWidth: 0, outerHeight: 0 },
                            i = t(e).parents().addBack().not(":visible");
                        a = "boolean" == typeof a && a;
                        var l = [];
                        return (
                            i.each(function () {
                                var t = {};
                                for (var e in n) (t[e] = this.style[e]), (this.style[e] = n[e]);
                                l.push(t);
                            }),
                            (o.width = t(e).width()),
                            (o.outerWidth = t(e).outerWidth(a)),
                            (o.innerWidth = t(e).innerWidth()),
                            (o.height = t(e).height()),
                            (o.innerHeight = t(e).innerHeight()),
                            (o.outerHeight = t(e).outerHeight(a)),
                            i.each(function (t) {
                                var e = l[t];
                                for (var a in n) this.style[a] = e[a];
                            }),
                            o
                        );
                    },
                    getGeneralSearchKey: function () {
                        var e = t(l.getOption("search.input"));
                        return l.getOption("search.key") || t(e).prop("name");
                    },
                    getObject: function (t, e) {
                        return t.split(".").reduce(function (t, e) {
                            return null !== t && void 0 !== t[e] ? t[e] : null;
                        }, e);
                    },
                    extendObj: function (t, e, a) {
                        var n = e.split("."),
                            o = 0;
                        return (
                            (function t(e) {
                                var i = n[o++];
                                void 0 !== e[i] && null !== e[i] ? "object" != typeof e[i] && "function" != typeof e[i] && (e[i] = {}) : (e[i] = {}), o === n.length ? (e[i] = a) : t(e[i]);
                            })(t),
                            t
                        );
                    },
                    rowEvenOdd: function () {
                        t(i.tableBody).find(".beauty-datatable__row").removeClass("beauty-datatable__row--even"),
                            t(i.wrap).hasClass("beauty-datatable--subtable")
                                ? t(i.tableBody).find(".beauty-datatable__row:not(.beauty-datatable__row-detail):even").addClass("beauty-datatable__row--even")
                                : t(i.tableBody).find(".beauty-datatable__row:nth-child(even)").addClass("beauty-datatable__row--even");
                    },
                    timer: 0,
                    redraw: function () {
                        return l.adjustCellsWidth.call(), l.isLocked() && (l.scrollbar(), l.resetScroll(), l.adjustCellsHeight.call()), l.adjustLockContainer.call(), l.initHeight.call(), i;
                    },
                    load: function () {
                        return l.reload(), i;
                    },
                    reload: function () {
                        return (
                            (function (t, e) {
                                clearTimeout(l.timer), (l.timer = setTimeout(t, e));
                            })(function () {
                                o.data.serverFiltering || l.localDataUpdate(), l.dataRender(), t(i).trigger("beauty-datatable--on-reloaded");
                            }, l.getOption("search.delay")),
                            i
                        );
                    },
                    getRecord: function (e) {
                        return (
                            void 0 === i.tableBody && (i.tableBody = t(i.table).children("tbody")),
                            t(i.tableBody)
                                .find(".beauty-datatable__cell:first-child")
                                .each(function (a, n) {
                                    if (e == t(n).text()) {
                                        var o = t(n).closest(".beauty-datatable__row").index() + 1;
                                        return (i.API.record = i.API.value = l.getOneRow(i.tableBody, o)), i;
                                    }
                                }),
                            i
                        );
                    },
                    getColumn: function (e) {
                        return l.setSelectedRecords(), (i.API.value = t(i.API.record).find('[data-field="' + e + '"]')), i;
                    },
                    destroy: function () {
                        t(i).parent().find(".beauty-datatable__pager").remove();
                        var e = t(i.initialDatatable).addClass("beauty-datatable--destroyed").show();
                        return t(i).replaceWith(e), t((i = e)).trigger("beauty-datatable--on-destroy"), (l.isInit = !1), (e = null);
                    },
                    sort: function (e, a) {
                        (a = void 0 === a ? "asc" : a), l.spinnerCallback(!0);
                        var n = { field: e, sort: a };
                        return (
                            l.setDataSourceParam("sort", n),
                            setTimeout(function () {
                                l.dataRender("sort"), t(i).trigger("beauty-datatable--on-sort", n), t(i.tableHead).find(".beauty-datatable__cell > span > i").remove();
                            }, 300),
                            i
                        );
                    },
                    getValue: function () {
                        return t(i.API.value).text();
                    },
                    setActive: function (e) {
                        "string" == typeof e && (e = t(i.tableBody).find('.beauty-checkbox--single > [type="checkbox"][value="' + e + '"]')), t(e).prop("checked", !0);
                        var a = [];
                        t(e).each(function (e, n) {
                            t(n).closest("tr").addClass("beauty-datatable__row--active");
                            var o = t(n).attr("value");
                            void 0 !== o && a.push(o);
                        }),
                            t(i).trigger("beauty-datatable--on-check", [a]);
                    },
                    setInactive: function (e) {
                        "string" == typeof e && (e = t(i.tableBody).find('.beauty-checkbox--single > [type="checkbox"][value="' + e + '"]')), t(e).prop("checked", !1);
                        var a = [];
                        t(e).each(function (e, n) {
                            t(n).closest("tr").removeClass("beauty-datatable__row--active");
                            var o = t(n).attr("value");
                            void 0 !== o && a.push(o);
                        }),
                            t(i).trigger("beauty-datatable--on-uncheck", [a]);
                    },
                    setActiveAll: function (e) {
                        var a = t(i.table).find("> tbody, > thead").find("tr").not(".beauty-datatable__row-subtable").find('.beauty-datatable__cell--check [type="checkbox"]');
                        e ? l.setActive(a) : l.setInactive(a);
                    },
                    setSelectedRecords: function () {
                        return (i.API.record = t(i.tableBody).find(".beauty-datatable__row--active")), i;
                    },
                    getSelectedRecords: function () {
                        return l.setSelectedRecords(), (i.API.record = i.rows(".beauty-datatable__row--active").nodes()), i.API.record;
                    },
                    getOption: function (t) {
                        //if (t === "data.pageSize")
                        //    return o.data.pageSize;
                        return l.getObject(t, o);
                    },
                    setOption: function (t, e) {
                        o = l.extendObj(o, t, e);
                    },
                    search: function (e, a) {
                        void 0 !== a && (a = t.makeArray(a)),
                            (n = function () {
                                var n = l.getDataSourceQuery();
                                if (void 0 === a && void 0 !== e) {
                                    var r = l.getGeneralSearchKey();
                                    n[r] = e;
                                }
                                "object" == typeof a &&
                                    (t.each(a, function (t, a) {
                                        n[a] = e;
                                    }),
                                        t.each(n, function (e, a) {
                                            ("" === a || t.isEmptyObject(a)) && delete n[e];
                                        })),
                                    l.setDataSourceQuery(n),
                                    i.setDataSourceParam("pagination", Object.assign({}, i.getDataSourceParam("pagination"), { page: 1 })),
                                    o.data.serverFiltering || l.localDataUpdate(),
                                    l.dataRender("search");
                            }),
                            (r = l.getOption("search.delay")),
                            clearTimeout(l.timer),
                            (l.timer = setTimeout(n, r));
                        var n, r;
                    },
                    setDataSourceParam: function (e, a) {
                        if (i.API.params !== null) if (isNaN(i.API.params.pagination.perpage)) i.API.params.pagination.perpage = 10;
                        (i.API.params = t.extend(
                            {},
                            {
                                pagination: {
                                    page: 1,
                                    perpage: l.getOption("data.pageSize"),
                                },
                                sort: l.getDefaultSortColumn(),
                                query: {},
                            },
                            i.API.params,
                            l.stateGet(l.stateId)
                        )),
                            (i.API.params = l.extendObj(i.API.params, e, a)),
                            l.stateKeep(l.stateId, i.API.params);
                    },
                    getDataSourceParam: function (e) {
                        if (isNaN(i.API.params.pagination.pages) && isNaN(i.API.params.pagination.perpage)) {
                            i.API.params.pagination.perpage = 10;
                            i.API.params.pagination.pages = Math.ceil(i.API.params.pagination.total / 10);
                        }
                        return (
                            (i.API.params = t.extend(
                                {},
                                {
                                    pagination: {
                                        page: 1,
                                        perpage: l.getOption("data.pageSize"),
                                    },
                                    sort: l.getDefaultSortColumn(),
                                    query: {},
                                },
                                i.API.params,
                                l.stateGet(l.stateId)
                            )),
                            "string" == typeof e ? l.getObject(e, i.API.params) : i.API.params
                        );
                    },
                    getDataSourceQuery: function () {
                        return l.getDataSourceParam("query") || {};
                    },
                    setDataSourceQuery: function (t) {
                        l.setDataSourceParam("query", t);
                    },
                    getCurrentPage: function () {
                        return t(i.table).siblings(".beauty-datatable__pager").last().find(".beauty-datatable__pager-nav").find(".beauty-datatable__pager-link.beauty-datatable__pager-link--active").data("page") || 1;
                    },
                    getPageSize: function () {
                        return t(i.table).siblings(".beauty-datatable__pager").last().find("select.beauty-datatable__pager-size").val() || 10;
                    },
                    getTotalRows: function () {
                        return i.API.params.pagination.total;
                    },
                    getDataSet: function () {
                        return i.originalDataSet;
                    },
                    nodeTr: [],
                    nodeTd: [],
                    nodeCols: [],
                    recentNode: [],
                    table: function () {
                        if (void 0 !== i.table) return i.table;
                    },
                    row: function (e) {
                        return l.rows(e), (l.nodeTr = l.recentNode = t(l.nodeTr).first()), i;
                    },
                    rows: function (e) {
                        return (
                            l.isLocked()
                                ? (l.nodeTr = l.recentNode = t(i.tableBody).find(e).filter(".beauty-datatable__lock--scroll > .beauty-datatable__row"))
                                : (l.nodeTr = l.recentNode = t(i.tableBody).find(e).filter(".beauty-datatable__row")),
                            i
                        );
                    },
                    column: function (e) {
                        return (l.nodeCols = l.recentNode = t(i.tableBody).find(".beauty-datatable__cell:nth-child(" + (e + 1) + ")")), i;
                    },
                    columns: function (e) {
                        var a = i.table;
                        l.nodeTr === l.recentNode && (a = l.nodeTr);
                        var n = t(a).find('.beauty-datatable__cell[data-field="' + e + '"]');
                        return n.length > 0 ? (l.nodeCols = l.recentNode = n) : (l.nodeCols = l.recentNode = t(a).find(e).filter(".beauty-datatable__cell")), i;
                    },
                    cell: function (e) {
                        return l.cells(e), (l.nodeTd = l.recentNode = t(l.nodeTd).first()), i;
                    },
                    cells: function (e) {
                        var a = t(i.tableBody).find(".beauty-datatable__cell");
                        return void 0 !== e && (a = t(a).filter(e)), (l.nodeTd = l.recentNode = a), i;
                    },
                    remove: function () {
                        return t(l.nodeTr.length) && l.nodeTr === l.recentNode && t(l.nodeTr).remove(), l.layoutUpdate(), i;
                    },
                    visible: function (e) {
                        if (t(l.recentNode.length)) {
                            var a = l.lockEnabledColumns();
                            if (l.recentNode === l.nodeCols) {
                                var n = l.recentNode.index();
                                if (l.isLocked()) {
                                    var i = t(l.recentNode).closest(".beauty-datatable__lock--scroll").length;
                                    i ? (n += a.left.length + 1) : t(l.recentNode).closest(".beauty-datatable__lock--right").length && (n += a.left.length + i + 1);
                                }
                            }
                            e ? (l.recentNode === l.nodeCols && delete o.columns[n].visible, t(l.recentNode).show()) : (l.recentNode === l.nodeCols && l.setOption("columns." + n + ".visible", !1), t(l.recentNode).hide()),
                                l.columnHide(),
                                l.redraw();
                        }
                    },
                    nodes: function () {
                        return l.recentNode;
                    },
                    dataset: function () {
                        return i;
                    },
                    gotoPage: function (t) {
                        void 0 !== l.pagingObject && ((l.isInit = !0), l.pagingObject.openPage(t));
                    },
                };
                if (
                    (t.each(l, function (t, e) {
                        i[t] = e;
                    }),
                        void 0 !== o)
                )
                    if ("string" == typeof o) {
                        var r = o;
                        void 0 !== (i = t(this).data(e)) && ((o = i.options), l[r].apply(this, Array.prototype.slice.call(arguments, 1)));
                    } else
                        i.data(e) ||
                            t(this).hasClass("beauty-datatable--loaded") ||
                            ((i.dataSet = null),
                                (i.textAlign = { left: "beauty-datatable__cell--left", center: "beauty-datatable__cell--center", right: "beauty-datatable__cell--right" }),
                                (o = t.extend(!0, {}, t.fn.BeautyDatatable.defaults, o)),
                                (i.options = o),
                                l.init.apply(this, [o]),
                                t(i.wrap).data(e, i));
                else void 0 === (i = t(this).data(e)) && t.error("BeautyDatatable not initialized"), (o = i.options);
                return i;
            }
            console.warn("No BeautyDatatable element exist.");
        }),
            (t.fn.BeautyDatatable.defaults = {
                data: {
                    type: "local",
                    source: null,
                    pageSize: 10,
                    saveState: {
                        cookie: !1,
                        webstorage: !0,
                    },
                    serverPaging: !1,
                    serverFiltering: !1,
                    serverSorting: !1,
                    autoColumns: !1,
                    attr: {
                        rowProps: [],
                    },
                },
                layout: {
                    theme: "default",
                    class: "beauty-datatable--brand",
                    scroll: !1,
                    height: null,
                    minHeight: null,
                    footer: !1,
                    header: !0,
                    customScrollbar: !0,
                    spinner: {
                        overlayColor: "#000000",
                        opacity: 0,
                        type: "loader",
                        state: "brand",
                        message: !0,
                    },
                    icons: {
                        sort: {
                            asc: "flaticon2-arrow-up",
                            desc: "flaticon2-arrow-down",
                        },
                        pagination: {
                            next: "flaticon2-next",
                            prev: "flaticon2-back",
                            first: "flaticon2-fast-back",
                            last: "flaticon2-fast-next",
                            more: "flaticon-more-1",
                        },
                        rowDetail: {
                            expand: "fa fa-caret-down",
                            collapse: "fa fa-caret-right",
                        },
                    },
                },
                sortable: !0,
                resizable: !1,
                filterable: !1,
                pagination: !0,
                editable: !1,
                columns: [],
                search: {
                    onEnter: !1,
                    input: null,
                    delay: 400,
                    key: null,
                },
                rows: {
                    callback: function () { },
                    beforeTemplate: function () { },
                    afterTemplate: function () { },
                    autoHide: !0,
                },
                toolbar: {
                    layout: ["pagination", "info"],
                    placement: ["bottom"],
                    items: {
                        pagination: {
                            type: "default",
                            pages: {
                                desBeautyop: {
                                    layout: "default",
                                    pagesNumber: 1,
                                },
                                tablet: {
                                    layout: "default",
                                    pagesNumber: 1,
                                },
                                mobile: {
                                    layout: "compact",
                                },
                            },
                            navigation: {
                                prev: !0,
                                next: !0,
                                first: !0,
                                last: !0,
                                more: !1,
                            },
                            pageSizeSelect: [],
                        },
                        info: !0,
                    },
                },
                translate: {
                    records: {
                        processing: "لطفا منتظر بمانید ...",
                        noRecords: "آیتمی برای نمایش موجود نیست",
                    },
                    toolbar: {
                        pagination: {
                            items: {
                                default: {
                                    first: "اولین",
                                    prev: "قبلی",
                                    next: "بعدی",
                                    last: "آخرین",
                                    more: "صفحات بیشتر",
                                    input: "شماره صفحه",
                                    select: "اندازه صفحات را انتخاب کنید",
                                    all: "همه",
                                },
                                info: "نمایش {{start}} - {{end}} از {{total}}",
                            },
                        },
                    },
                },
                extensions: {},
            });
    })(jQuery),
    (function (t) {
        (t.fn.BeautyDatatable = t.fn.BeautyDatatable || {}),
            (t.fn.BeautyDatatable.checkbox = function (e, a) {
                var n = {
                    selectedAllRows: !1,
                    selectedRows: [],
                    unselectedRows: [],
                    init: function () {
                        n.selectorEnabled() &&
                            (e.setDataSourceParam(a.vars.selectedAllRows, !1),
                                e.stateRemove("checkbox"),
                                a.vars.requestIds && e.setDataSourceParam(a.vars.requestIds, !0),
                                t(e).on("beauty-datatable--on-reloaded", function () {
                                    e.stateRemove("checkbox"), e.setDataSourceParam(a.vars.selectedAllRows, !1), (n.selectedAllRows = !1), (n.selectedRows = []), (n.unselectedRows = []);
                                }),
                                (n.selectedAllRows = e.getDataSourceParam(a.vars.selectedAllRows)),
                                t(e).on("beauty-datatable--on-layout-updated", function (a, o) {
                                    o.table == t(e.wrap).attr("id") &&
                                        e.ready(function () {
                                            n.initVars(), n.initEvent(), n.initSelect();
                                        });
                                }),
                                t(e).on("beauty-datatable--on-check", function (a, o) {
                                    o.forEach(function (t) {
                                        n.selectedRows.push(t), (n.unselectedRows = n.remove(n.unselectedRows, t));
                                    });
                                    var i = {};
                                    (i.selectedRows = t.unique(n.selectedRows)), (i.unselectedRows = t.unique(n.unselectedRows)), e.stateKeep("checkbox", i);
                                }),
                                t(e).on("beauty-datatable--on-uncheck", function (a, o) {
                                    o.forEach(function (t) {
                                        n.unselectedRows.push(t), (n.selectedRows = n.remove(n.selectedRows, t));
                                    });
                                    var i = {};
                                    (i.selectedRows = t.unique(n.selectedRows)), (i.unselectedRows = t.unique(n.unselectedRows)), e.stateKeep("checkbox", i);
                                }));
                    },
                    initEvent: function () {
                        t(e.tableHead)
                            .find('.beauty-checkbox--all > [type="checkbox"]')
                            .click(function (o) {
                                if (((n.selectedRows = n.unselectedRows = []), e.stateRemove("checkbox"), t(this).is(":checked") ? (n.selectedAllRows = !0) : (n.selectedAllRows = !1), !a.vars.requestIds)) {
                                    t(this).is(":checked") &&
                                        (n.selectedRows = t.makeArray(
                                            t(e.tableBody)
                                                .find('.beauty-checkbox--single > [type="checkbox"]')
                                                .map(function (e, a) {
                                                    return t(a).val();
                                                })
                                        ));
                                    var i = {};
                                    (i.selectedRows = t.unique(n.selectedRows)), e.stateKeep("checkbox", i);
                                }
                                e.setDataSourceParam(a.vars.selectedAllRows, n.selectedAllRows), t(e).trigger("beauty-datatable--on-click-checkbox", [t(this)]);
                            }),
                            t(e.tableBody)
                                .find('.beauty-checkbox--single > [type="checkbox"]')
                                .click(function (o) {
                                    var i = t(this).val();
                                    t(this).is(":checked") ? (n.selectedRows.push(i), (n.unselectedRows = n.remove(n.unselectedRows, i))) : (n.unselectedRows.push(i), (n.selectedRows = n.remove(n.selectedRows, i))),
                                        !a.vars.requestIds && n.selectedRows.length < 1 && t(e.tableHead).find('.beauty-checkbox--all > [type="checkbox"]').prop("checked", !1);
                                    var l = {};
                                    (l.selectedRows = t.unique(n.selectedRows)), (l.unselectedRows = t.unique(n.unselectedRows)), e.stateKeep("checkbox", l), t(e).trigger("beauty-datatable--on-click-checkbox", [t(this)]);
                                });
                    },
                    initSelect: function () {
                        n.selectedAllRows && a.vars.requestIds
                            ? (e.hasClass("beauty-datatable--error") || t(e.tableHead).find('.beauty-checkbox--all > [type="checkbox"]').prop("checked", !0),
                                e.setActiveAll(!0),
                                n.unselectedRows.forEach(function (t) {
                                    e.setInactive(t);
                                }))
                            : (n.selectedRows.forEach(function (t) {
                                e.setActive(t);
                            }),
                                !e.hasClass("beauty-datatable--error") &&
                                t(e.tableBody).find('.beauty-checkbox--single > [type="checkbox"]').not(":checked").length < 1 &&
                                t(e.tableHead).find('.beauty-checkbox--all > [type="checkbox"]').prop("checked", !0));
                    },
                    selectorEnabled: function () {
                        return t.grep(e.options.columns, function (t, e) {
                            return t.selector || !1;
                        })[0];
                    },
                    initVars: function () {
                        var t = e.stateGet("checkbox");
                        void 0 !== t && ((n.selectedRows = t.selectedRows || []), (n.unselectedRows = t.unselectedRows || []));
                    },
                    getSelectedId: function (t) {
                        if ((n.initVars(), n.selectedAllRows && a.vars.requestIds)) {
                            void 0 === t && (t = a.vars.rowIds);
                            var o = e.getObject(t, e.lastResponse) || [];
                            return (
                                o.length > 0 &&
                                n.unselectedRows.forEach(function (t) {
                                    o = n.remove(o, parseInt(t));
                                }),
                                o
                            );
                        }
                        return n.selectedRows;
                    },
                    remove: function (t, e) {
                        return t.filter(function (t) {
                            return t !== e;
                        });
                    },
                };
                return (
                    (e.checkbox = function () {
                        return n;
                    }),
                    "object" == typeof a && ((a = t.extend(!0, {}, t.fn.BeautyDatatable.checkbox.default, a)), n.init.apply(this, [a])),
                    e
                );
            }),
            (t.fn.BeautyDatatable.checkbox.default = { vars: { selectedAllRows: "selectedAllRows", requestIds: "requestIds", rowIds: "meta.rowIds" } });
    })(jQuery);

var defaults = {
    layout: {
        icons: { pagination: { next: "flaticon2-next", prev: "flaticon2-back", first: "flaticon2-fast-back", last: "flaticon2-fast-next", more: "flaticon-more-1" }, rowDetail: { expand: "fa fa-caret-down", collapse: "fa fa-caret-right" } },
    },
};
BeautyUtil.isRTL() &&
    (defaults = { layout: { icons: { pagination: { next: "flaticon2-back", prev: "flaticon2-next", first: "flaticon2-fast-next", last: "flaticon2-fast-back" }, rowDetail: { collapse: "fa fa-caret-down", expand: "fa fa-caret-right" } } } }),
    $.extend(!0, $.fn.BeautyDatatable.defaults, defaults);
var BeautyAsideSecondary = (function () {
    BeautyUtil.get("Beauty_aside_secondary");
    var t,
        e = BeautyUtil.get("Beauty_aside_secondary_tab_1"),
        a = BeautyUtil.get("Beauty_aside_secondary_tab_2"),
        n = BeautyUtil.get("Beauty_aside_secondary_tab_3"),
        o = BeautyUtil.find(e, ".beauty-aside-secondary__content-body"),
        i = BeautyUtil.find(a, ".beauty-aside-secondary__content-body"),
        l = BeautyUtil.find(n, ".beauty-aside-secondary__content-body"),
        r = function (t) {
            var e,
                a = BeautyUtil.find(t, ".beauty-aside-secondary__content-head");
            BeautyUtil.find(t, ".beauty-aside-secondary__content-body");
            return (
                (e = parseInt(BeautyUtil.getViewPort().height) - parseInt(BeautyUtil.actualHeight(a)) - 60),
                BeautyUtil.isInResponsiveRange("desBeautyop") ? (e -= BeautyUtil.actualHeight(BeautyUtil.get("Beauty_header"))) : (e -= BeautyUtil.actualHeight(BeautyUtil.get("Beauty_header_mobile"))),
                e
            );
        };
    return {
        init: function () {
            $('#Beauty_aside_secondary_nav a[data-toggle="tab"]').on("click", function (e) {
                t && t.is($(this)) && $("body").hasClass("beauty-aside-secondary--expanded") ? BeautyLayout.closeAsideSecondary() : ((t = $(this)), BeautyLayout.openAsideSecondary());
            }),
                $("#Beauty_aside_secondary_close").on("click", function (t) {
                    BeautyLayout.closeAsideSecondary();
                }),
                $('#Beauty_aside_secondary_nav a[data-toggle="tab"]').on("shown.bs.tab", function (t) {
                    BeautyUtil.scrollUpdate(o), BeautyUtil.scrollUpdate(i), BeautyUtil.scrollUpdate(l);
                }),
                new BeautyToggle("Beauty_aside_secondary_mobile_nav_toggler", { target: "body", targetState: "beauty-aside-secondary--mobile-nav-expanded" }),
                BeautyUtil.scrollInit(o, {
                    disableForMobile: !0,
                    resetHeightOnDestroy: !0,
                    handleWindowResize: !0,
                    height: function () {
                        return r(e);
                    },
                }),
                BeautyUtil.scrollInit(i, {
                    disableForMobile: !0,
                    resetHeightOnDestroy: !0,
                    handleWindowResize: !0,
                    height: function () {
                        return r(a);
                    },
                }),
                BeautyUtil.scrollInit(l, {
                    disableForMobile: !0,
                    resetHeightOnDestroy: !0,
                    handleWindowResize: !0,
                    height: function () {
                        return r(n);
                    },
                });
        },
    };
})();
BeautyUtil.ready(function () {
    BeautyAsideSecondary.init();
});
var BeautyDemoPanel = (function () {
    var t,
        e = BeautyUtil.getByID("Beauty_demo_panel");
    return {
        init: function () {
            !(function () {
                t = new BeautyOffcanvas(e, { overlay: !0, baseClass: "beauty-demo-panel", closeBy: "Beauty_demo_panel_close", toggleBy: "Beauty_demo_panel_toggle" });
                var a = BeautyUtil.find(e, ".beauty-demo-panel__head"),
                    n = BeautyUtil.find(e, ".beauty-demo-panel__body");
                BeautyUtil.scrollInit(n, {
                    disableForMobile: !0,
                    resetHeightOnDestroy: !0,
                    handleWindowResize: !0,
                    height: function () {
                        var t = parseInt(BeautyUtil.getViewPort().height);
                        return a && ((t -= parseInt(BeautyUtil.actualHeight(a))), (t -= parseInt(BeautyUtil.css(a, "marginBottom")))), (t -= parseInt(BeautyUtil.css(e, "paddingTop"))), (t -= parseInt(BeautyUtil.css(e, "paddingBottom")));
                    },
                }),
                    void 0 === t ||
                    $.isEmptyObject(t) ||
                    t.on("hide", function () {
                        alert(1);
                        var t = new Date(new Date().getTime() + 36e5);
                        Cookies.set("Beauty_demo_panel_shown", 1, { expires: t });
                    });
            })(),
                ("keenthemes.com" != encodeURI(window.location.hostname) && "www.keenthemes.com" != encodeURI(window.location.hostname)) ||
                setTimeout(function () {
                    if (!Cookies.get("Beauty_demo_panel_shown")) {
                        var e = new Date(new Date().getTime() + 9e5);
                        Cookies.set("Beauty_demo_panel_shown", 1, { expires: e }), t.show();
                    }
                }, 4e3);
        },
    };
})();

BeautyUtil.ready(function () {
    BeautyDemoPanel.init();
});


var BeautyLayout = (function () {
    var t,
        e,
        a,
        n,
        o,
        i = function () {
            return new BeautyPortlet("Beauty_page_portlet", {
                sticky: {
                    offset: 80,
                    zIndex: 90,
                    position: {
                        top: function () {
                            var e = 0;
                            return (
                                BeautyUtil.isInResponsiveRange("desBeautyop")
                                    ? BeautyUtil.hasClass(t, "beauty-subheader--fixed") && (e += parseInt(BeautyUtil.css(BeautyUtil.get("Beauty_subheader"), "height")))
                                    : BeautyUtil.hasClass(t, "beauty-header-mobile--fixed") && (e += parseInt(BeautyUtil.css(BeautyUtil.get("Beauty_header_mobile"), "height"))),
                                e
                            );
                        },
                        left: function (t) {
                            var e = t.getSelf();
                            return BeautyUtil.offset(e).left;
                        },
                        right: function (t) {
                            var e = t.getSelf(),
                                a = parseInt(BeautyUtil.css(e, "width"));
                            return parseInt(BeautyUtil.css(BeautyUtil.get("body"), "width")) - a - BeautyUtil.offset(e).left;
                        },
                    },
                },
            });
        };
    return {
        init: function () {
            (t = BeautyUtil.getByTag("body")),
                this.initAside(),
                this.initScrolltop(),
                this.initPageStickyPortlet(),
                $("#Beauty_aside_menu, #Beauty_header_menu").on("click", '.beauty-menu__link[href="#"]:not(.beauty-menu__toggle)', function (t) {
                    swal("", "You have clicked on a non-functional dummy link!"), t.preventDefault();
                });
        },
        initAside: function () {
            !(function () {
                (e = BeautyUtil.get("Beauty_aside")),
                    (n = new BeautyOffcanvas("Beauty_aside", {
                        baseClass: "beauty-aside",
                        overlay: !0,
                        closeBy: "Beauty_aside_close_btn",
                        toggleBy: { target: "Beauty_aside_mobile_toggler", state: "beauty-header-mobile__toolbar-toggler--active" },
                    }));
                var t,
                    o = BeautyUtil.get("Beauty_aside_menu"),
                    i = ((o = BeautyUtil.getByID("Beauty_aside_menu")), "1" === BeautyUtil.attr(o, "data-Beautymenu-dropdown") ? "dropdown" : "accordion");
                "1" === BeautyUtil.attr(o, "data-Beautymenu-scroll") &&
                    (t = {
                        rememberPosition: !0,
                        height: function () {
                            var t = parseInt(BeautyUtil.getViewPort().height),
                                a = BeautyUtil.find(e, ".beauty-aside__secondary"),
                                n = parseInt(BeautyUtil.css(a, "padding-top")),
                                o = parseInt(BeautyUtil.css(a, "padding-bottom")),
                                i = BeautyUtil.find(e, ".beauty-aside__secondary-top"),
                                l = parseInt(BeautyUtil.height(i)),
                                r = BeautyUtil.find(e, ".beauty-aside__secondary-bottom");
                            return t - l - parseInt(BeautyUtil.css(r, "padding-top")) - parseInt(BeautyUtil.css(r, "padding-bottom")) - n - o;
                        },
                    }),
                    (a = new BeautyMenu("Beauty_aside_menu", {
                        scroll: t,
                        submenu: { desBeautyop: { default: i, state: { body: "beauty-aside--minimize", mode: "dropdown" } }, tablet: "accordion", mobile: "accordion" },
                        accordion: { expandAll: !1 },
                    }));
            })();
        },
        initScrolltop: function () {
            new BeautyScrolltop("Beauty_scrolltop", { offset: 300, speed: 600 });
        },
        initPageStickyPortlet: function () {
            BeautyUtil.get("Beauty_page_portlet") &&
                ((o = i()).initSticky(),
                    BeautyUtil.addResizeHandler(function () {
                        o.updateSticky();
                    }),
                    i());
        },
        getAsideMenu: function () {
            return a;
        },
        closeMobileAsideMenuOffcanvas: function () {
            BeautyUtil.isMobileDevice() && n.hide();
        },
        closeMobileHeaderMenuOffcanvas: function () {
            BeautyUtil.isMobileDevice() && headerMenuOffcanvas.hide();
        },
    };
})();
"undefined" != typeof module && (module.exports = BeautyLayout),
    BeautyUtil.ready(function () {
        BeautyLayout.init();
    });
var BeautyLib = {
    initMiniChart: function (t, e, a, n, o, i) {
        if (0 != t.length) {
            (o = void 0 !== o && o), (i = void 0 !== i && i);
            var l = {
                type: "line",
                data: {
                    labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                    datasets: [
                        {
                            label: "",
                            borderColor: a,
                            borderWidth: n,
                            pointHoverRadius: 4,
                            pointHoverBorderWidth: 4,
                            pointBackgroundColor: Chart.helpers.color("#000000").alpha(0).rgbString(),
                            pointBorderColor: Chart.helpers.color("#000000").alpha(0).rgbString(),
                            pointHoverBackgroundColor: BeautyApp.getStateColor("brand"),
                            pointHoverBorderColor: Chart.helpers.color("#000000").alpha(0.1).rgbString(),
                            fill: o,
                            backgroundColor: a,
                            data: e,
                        },
                    ],
                },
                options: {
                    title: { display: !1 },
                    tooltips: !!i && {
                        enabled: !0,
                        intersect: !1,
                        mode: "nearest",
                        bodySpacing: 5,
                        yPadding: 10,
                        xPadding: 10,
                        caretPadding: 0,
                        displayColors: !1,
                        backgroundColor: BeautyApp.getStateColor("brand"),
                        titleFontColor: "#ffffff",
                        cornerRadius: 4,
                        footerSpacing: 0,
                        titleSpacing: 0,
                    },
                    legend: { display: !1, labels: { usePointStyle: !1 } },
                    responsive: !1,
                    maintainAspectRatio: !0,
                    hover: { mode: "index" },
                    scales: { xAxes: [{ display: !1, gridLines: !1, scaleLabel: { display: !1, labelString: "Month" } }], yAxes: [{ display: !1, gridLines: !1, scaleLabel: { display: !1, labelString: "Month" } }] },
                    elements: { line: { tension: 0.5 }, point: { radius: 2, borderWidth: 4 } },
                    layout: { padding: { left: 6, right: 0, top: 4, bottom: 0 } },
                },
            };
            new Chart(t, l);
        }
    },
    initMediumChart: function (t, e, a, n, o) {
        if (document.getElementById(t)) {
            o = o || 2;
            var i = document.getElementById(t).getContext("2d"),
                l = i.createLinearGradient(0, 0, 0, 100);
            l.addColorStop(0, Chart.helpers.color(n).alpha(0.3).rgbString()), l.addColorStop(1, Chart.helpers.color(n).alpha(0).rgbString());
            var r = {
                type: "line",
                data: {
                    labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October"],
                    datasets: [{ label: "Orders", borderColor: n, borderWidth: o, backgroundColor: l, pointBackgroundColor: BeautyApp.getStateColor("brand"), data: e }],
                },
                options: {
                    responsive: !0,
                    maintainAspectRatio: !1,
                    title: { display: !1, text: "Stacked Area" },
                    tooltips: {
                        enabled: !0,
                        intersect: !1,
                        mode: "nearest",
                        bodySpacing: 5,
                        yPadding: 10,
                        xPadding: 10,
                        caretPadding: 0,
                        displayColors: !1,
                        backgroundColor: BeautyApp.getStateColor("brand"),
                        titleFontColor: "#ffffff",
                        cornerRadius: 4,
                        footerSpacing: 0,
                        titleSpacing: 0,
                    },
                    legend: { display: !1, labels: { usePointStyle: !1 } },
                    hover: { mode: "index" },
                    scales: {
                        xAxes: [{ display: !1, scaleLabel: { display: !1, labelString: "Month" }, ticks: { display: !1, beginAtZero: !0 } }],
                        yAxes: [{ display: !1, scaleLabel: { display: !1, labelString: "Value" }, gridLines: { color: "#eef2f9", drawBorder: !1, offsetGridLines: !0, drawTicks: !1 }, ticks: { max: a, display: !1, beginAtZero: !0 } }],
                    },
                    elements: { point: { radius: 0, borderWidth: 0, hoverRadius: 0, hoverBorderWidth: 0 } },
                    layout: { padding: { left: 0, right: 0, top: 0, bottom: 0 } },
                },
            },
                s = new Chart(i, r);
            BeautyUtil.addResizeHandler(function () {
                s.update();
            });
        }
    },
};
"undefined" != typeof module && (module.exports = BeautyLib);
var BeautyOffcanvasPanel = (function () {
    var t = BeautyUtil.get("Beauty_offcanvas_toolbar_notifications"),
        e = BeautyUtil.get("Beauty_offcanvas_toolbar_quick_actions"),
        a = BeautyUtil.get("Beauty_offcanvas_toolbar_profile"),
        n = BeautyUtil.get("Beauty_offcanvas_toolbar_search");
    return {
        init: function () {
            !(function () {
                var e = BeautyUtil.find(t, ".beauty-offcanvas-panel__head"),
                    a = BeautyUtil.find(t, ".beauty-offcanvas-panel__body");
                new BeautyOffcanvas(t, { overlay: !0, baseClass: "beauty-offcanvas-panel", closeBy: "Beauty_offcanvas_toolbar_notifications_close", toggleBy: "Beauty_offcanvas_toolbar_notifications_toggler_btn" });
                BeautyUtil.scrollInit(a, {
                    disableForMobile: !0,
                    resetHeightOnDestroy: !0,
                    handleWindowResize: !0,
                    height: function () {
                        var a = parseInt(BeautyUtil.getViewPort().height);
                        return e && ((a -= parseInt(BeautyUtil.actualHeight(e))), (a -= parseInt(BeautyUtil.css(e, "marginBottom")))), (a -= parseInt(BeautyUtil.css(t, "paddingTop"))), (a -= parseInt(BeautyUtil.css(t, "paddingBottom")));
                    },
                });
            })(),
                (function () {
                    var t = BeautyUtil.find(e, ".beauty-offcanvas-panel__head"),
                        a = BeautyUtil.find(e, ".beauty-offcanvas-panel__body");
                    new BeautyOffcanvas(e, { overlay: !0, baseClass: "beauty-offcanvas-panel", closeBy: "Beauty_offcanvas_toolbar_quick_actions_close", toggleBy: "Beauty_offcanvas_toolbar_quick_actions_toggler_btn" });
                    BeautyUtil.scrollInit(a, {
                        disableForMobile: !0,
                        resetHeightOnDestroy: !0,
                        handleWindowResize: !0,
                        height: function () {
                            var a = parseInt(BeautyUtil.getViewPort().height);
                            return (
                                t && ((a -= parseInt(BeautyUtil.actualHeight(t))), (a -= parseInt(BeautyUtil.css(t, "marginBottom")))), (a -= parseInt(BeautyUtil.css(e, "paddingTop"))), (a -= parseInt(BeautyUtil.css(e, "paddingBottom")))
                            );
                        },
                    });
                })(),
                (function () {
                    var t = BeautyUtil.find(a, ".beauty-offcanvas-panel__head"),
                        e = BeautyUtil.find(a, ".beauty-offcanvas-panel__body");
                    new BeautyOffcanvas(a, { overlay: !0, baseClass: "beauty-offcanvas-panel", closeBy: "Beauty_offcanvas_toolbar_profile_close", toggleBy: "Beauty_offcanvas_toolbar_profile_toggler_btn" });
                    BeautyUtil.scrollInit(e, {
                        disableForMobile: !0,
                        resetHeightOnDestroy: !0,
                        handleWindowResize: !0,
                        height: function () {
                            var e = parseInt(BeautyUtil.getViewPort().height);
                            return (
                                t && ((e -= parseInt(BeautyUtil.actualHeight(t))), (e -= parseInt(BeautyUtil.css(t, "marginBottom")))), (e -= parseInt(BeautyUtil.css(a, "paddingTop"))), (e -= parseInt(BeautyUtil.css(a, "paddingBottom")))
                            );
                        },
                    });
                })(),
                (function () {
                    var t = BeautyUtil.find(n, ".beauty-offcanvas-panel__head"),
                        e = (BeautyUtil.find(n, ".beauty-offcanvas-panel__body"), BeautyUtil.get("Beauty_quick_search_offcanvas")),
                        a = BeautyUtil.find(e, ".beauty-quick-search__form"),
                        o = BeautyUtil.find(e, ".beauty-quick-search__wrapper");
                    new BeautyOffcanvas(n, { overlay: !0, baseClass: "beauty-offcanvas-panel", closeBy: "Beauty_offcanvas_toolbar_search_close", toggleBy: "Beauty_offcanvas_toolbar_search_toggler_btn" });
                    BeautyUtil.scrollInit(o, {
                        disableForMobile: !0,
                        resetHeightOnDestroy: !0,
                        handleWindowResize: !0,
                        height: function () {
                            var e = parseInt(BeautyUtil.getViewPort().height);
                            return (
                                (e -= parseInt(BeautyUtil.actualHeight(a))),
                                (e -= parseInt(BeautyUtil.css(a, "marginBottom"))),
                                t && ((e -= parseInt(BeautyUtil.actualHeight(t))), (e -= parseInt(BeautyUtil.css(t, "marginBottom")))),
                                (e -= parseInt(BeautyUtil.css(n, "paddingTop"))),
                                (e -= parseInt(BeautyUtil.css(n, "paddingBottom")))
                            );
                        },
                    });
                })();
        },
    };
})();
BeautyUtil.ready(function () {
    BeautyOffcanvasPanel.init();
});
var BeautyQuickPanel = (function () {
    var t = BeautyUtil.get("Beauty_quick_panel"),
        e = BeautyUtil.get("Beauty_quick_panel_tab_notifications"),
        a = BeautyUtil.get("Beauty_quick_panel_tab_actions"),
        n = BeautyUtil.get("Beauty_quick_panel_tab_settings"),
        o = function () {
            var e = BeautyUtil.find(t, ".beauty-offcanvas-panel__nav");
            BeautyUtil.find(t, ".beauty-offcanvas-panel__body");
            return parseInt(BeautyUtil.getViewPort().height) - parseInt(BeautyUtil.actualHeight(e)) - parseInt(BeautyUtil.css(e, "margin-bottom")) - 2 * parseInt(BeautyUtil.css(e, "padding-top")) - 10;
        };
    return {
        init: function () {
            new BeautyOffcanvas(t, { overlay: !0, baseClass: "beauty-offcanvas-panel", closeBy: "Beauty_quick_panel_close_btn", toggleBy: "Beauty_quick_panel_toggler_btn" }),
                BeautyUtil.scrollInit(e, {
                    disableForMobile: !0,
                    resetHeightOnDestroy: !0,
                    handleWindowResize: !0,
                    height: function () {
                        return o();
                    },
                }),
                BeautyUtil.scrollInit(a, {
                    disableForMobile: !0,
                    resetHeightOnDestroy: !0,
                    handleWindowResize: !0,
                    height: function () {
                        return o();
                    },
                }),
                BeautyUtil.scrollInit(n, {
                    disableForMobile: !0,
                    resetHeightOnDestroy: !0,
                    handleWindowResize: !0,
                    height: function () {
                        return o();
                    },
                }),
                $(t)
                    .find('a[data-toggle="tab"]')
                    .on("shown.bs.tab", function (t) {
                        BeautyUtil.scrollUpdate(e), BeautyUtil.scrollUpdate(a), BeautyUtil.scrollUpdate(n);
                    });
        },
    };
})();
BeautyUtil.ready(function () {
    BeautyQuickPanel.init();
});
var BeautyQuickSearch = function () {
    var t,
        e,
        a,
        n,
        o,
        i,
        l,
        r,
        s = "",
        d = !1,
        c = !1,
        u = !1,
        p = "beauty-spinner beauty-spinner--input beauty-spinner--sm beauty-spinner--brand beauty-spinner--right",
        f = "beauty-quick-search--has-result",
        g = function () {
            (u = !1), BeautyUtil.removeClass(r, p), n && (a.value.length < 2 ? BeautyUtil.hide(n) : BeautyUtil.show(n, "flex"));
        },
        h = function () {
            l && !BeautyUtil.hasClass(i, "show") && ($(l).dropdown("toggle"), $(l).dropdown("update"));
        },
        v = function () {
            l && BeautyUtil.hasClass(i, "show") && $(l).dropdown("toggle");
        },
        b = function () {
            if (d && s === a.value) return g(), BeautyUtil.addClass(t, f), h(), void BeautyUtil.scrollUpdate(o);
            (s = a.value),
                BeautyUtil.removeClass(t, f),
                (u = !0),
                BeautyUtil.addClass(r, p),
                n && BeautyUtil.hide(n),
                v(),
                setTimeout(function () {
                    $.ajax({
                        url: "https://keenthemes.com/keen/tools/preview/api/quick_search.php",
                        data: { query: s },
                        dataType: "html",
                        success: function (e) {
                            (d = !0), g(), BeautyUtil.addClass(t, f), BeautyUtil.setHTML(o, e), h(), BeautyUtil.scrollUpdate(o);
                        },
                        error: function (e) {
                            (d = !1), g(), BeautyUtil.addClass(t, f), BeautyUtil.setHTML(o, '<span class="beauty-quick-search__message">Connection error. Pleae try again later.</div>'), h(), BeautyUtil.scrollUpdate(o);
                        },
                    });
                }, 1e3);
        },
        m = function (e) {
            (a.value = ""), (s = ""), (d = !1), BeautyUtil.hide(n), BeautyUtil.removeClass(t, f), v();
        },
        k = function () {
            if (a.value.length < 2) return g(), void v();
            1 != u &&
                (c && clearTimeout(c),
                    (c = setTimeout(function () {
                        b();
                    }, 200)));
        };
    return {
        init: function (s) {
            (t = s),
                (e = BeautyUtil.find(t, ".beauty-quick-search__form")),
                (a = BeautyUtil.find(t, ".beauty-quick-search__input")),
                (n = BeautyUtil.find(t, ".beauty-quick-search__close")),
                (o = BeautyUtil.find(t, ".beauty-quick-search__wrapper")),
                (i = BeautyUtil.find(t, ".dropdown-menu")),
                (l = BeautyUtil.find(t, '[data-toggle="dropdown"]')),
                (r = BeautyUtil.find(t, ".input-group")),
                BeautyUtil.addEvent(a, "keyup", k),
                BeautyUtil.addEvent(a, "focus", k),
                (e.onkeypress = function (t) {
                    13 == (t.charCode || t.keyCode || 0) && t.preventDefault();
                }),
                BeautyUtil.addEvent(n, "click", m);
        },
    };
},
    BeautyQuickSearchMobile = BeautyQuickSearch;
BeautyUtil.ready(function () {
    BeautyUtil.get("Beauty_quick_search_dropdown") && BeautyQuickSearch().init(BeautyUtil.get("Beauty_quick_search_dropdown")),
        BeautyUtil.get("Beauty_quick_search_inline") && BeautyQuickSearchMobile().init(BeautyUtil.get("Beauty_quick_search_inline")),
        BeautyUtil.get("Beauty_quick_search_offcanvas") && BeautyQuickSearchMobile().init(BeautyUtil.get("Beauty_quick_search_offcanvas"));
});

var BeautyTemplateBuilder = {
    build: function (templateName, data, wrapper, isReplace) {
        let template = $("#" + templateName).html();
        let html = Mustache.render(template, data);
        if (isReplace)
            $("#" + wrapper)
                .empty()
                .append(html);
        else $("#" + wrapper).append(html);
    },
};

"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyTemplateBuilder);

var BeautyClearLocalStorage = {
    clear: function (items) {
        for (var i = 0; i < items.length; i++)
            localStorage.removeItem(items[i]);
    },
};

"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyClearLocalStorage);

var BeautyTableActionsBuilder = {
    build: function (items) {
        let actions =
            '<div class="dropdown">' +
            '<a href="javascript:;" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown"><i class="flaticon-apps beauty-font-brand"></i></a>' +
            '<div class="dropdown-menu dropdown-menu-right">' +
            '<ul class="beauty-nav">';
        for (var i = 0; i < items.length; i++) {
            if (items[i].haveForm) {
                actions +=
                    '<li class="beauty-nav__item">' +
                    '<form action="' +
                    items[i].url +
                    '" method="post"><input id="' +
                    items[i].hiddenId +
                    '" name="' +
                    items[i].hiddenName +
                    '" type="hidden" value="' +
                    items[i].hiddenValu +
                    '">' +
                    '<button type="submit" ' +
                    '  class="dropdown-item ' +
                    items[i].classes +
                    '"  ' +
                    items[i].data +
                    ">" +
                    '<i class="beauty-nav__link-icon  mr-3 ' +
                    items[i].icon +
                    '"></i>' +
                    '<span class="beauty-nav__link-text">' +
                    items[i].text +
                    "</span></button></form></li>";
            } else {
                actions +=
                    '<li class="beauty-nav__item">' +
                    '<a href="' +
                    (items[i].isVoid ? "javascript: void(0)" : items[i].url) +
                    '"' +
                    '  class="beauty-nav__link ' +
                    items[i].classes +
                    '"  ' +
                    items[i].data +
                    ">" +
                    '<i class="beauty-nav__link-icon ' +
                    items[i].icon +
                    '"></i>' +
                    '<span class="beauty-nav__link-text">' +
                    items[i].text +
                    "</span></a></li>";
            }
        }
        actions += "</ul></div></div>";

        return actions;
    },
};

"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyTableActionsBuilder);

var BeautyDropSelect = {
    build: function (items) {
      
    },
};

"undefined" != typeof module && void 0 !== module.exports && (module.exports = BeautyDropSelect);
