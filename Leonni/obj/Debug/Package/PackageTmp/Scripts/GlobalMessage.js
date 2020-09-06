/// <reference path="jquery-1.5.1.js" />
/*
* jQuery BBQ: Back Button & Query Library - v1.2.1 - 2/17/2010
* http://benalman.com/projects/jquery-bbq-plugin/
*
* Copyright (c) 2010 "Cowboy" Ben Alman
* Dual licensed under the MIT and GPL licenses.
* http://benalman.com/about/license/
*/
(function ($, p) { var i, m = Array.prototype.slice, r = decodeURIComponent, a = $.param, c, l, v, b = $.bbq = $.bbq || {}, q, u, j, e = $.event.special, d = "hashchange", A = "querystring", D = "fragment", y = "elemUrlAttr", g = "location", k = "href", t = "src", x = /^.*\?|#.$/g, w = /^.*\#/, h, C = {}; function E(F) { return typeof F === "string" } function B(G) { var F = m.call(arguments, 1); return function () { return G.apply(this, F.concat(m.call(arguments))) } } function n(F) { return F.replace(/^[^#]#?(.*)$/, "$1") } function o(F) { return F.replace(/(?:^[^?#]*\?([^#]*).*$)?.*/, "$1") } function f(H, M, F, I, G) { var O, L, K, N, J; if (I !== i) { K = F.match(H ? /^([^#]*)\#?(.*)$/ : /^([^#?]*)\??([^#]*)(#?.*)/); J = K[3] || ""; if (G === 2 && E(I)) { L = I.replace(H ? w : x, "") } else { N = l(K[2]); I = E(I) ? l[H ? D : A](I) : I; L = G === 2 ? I : G === 1 ? $.extend({}, I, N) : $.extend({}, N, I); L = a(L); if (H) { L = L.replace(h, r) } } O = K[1] + (H ? "#" : L || !K[1] ? "?" : "") + L + J } else { O = M(F !== i ? F : p[g][k]) } return O } a[A] = B(f, 0, o); a[D] = c = B(f, 1, n); c.noEscape = function (G) { G = G || ""; var F = $.map(G.split(""), encodeURIComponent); h = new RegExp(F.join("|"), "g") }; c.noEscape(",/"); $.deparam = l = function (I, F) { var H = {}, G = { "true": !0, "false": !1, "null": null }; $.each(I.replace(/\+/g, " ").split("&"), function (L, Q) { var K = Q.split("="), P = r(K[0]), J, O = H, M = 0, R = P.split("]["), N = R.length - 1; if (/\[/.test(R[0]) && /\]$/.test(R[N])) { R[N] = R[N].replace(/\]$/, ""); R = R.shift().split("[").concat(R); N = R.length - 1 } else { N = 0 } if (K.length === 2) { J = r(K[1]); if (F) { J = J && !isNaN(J) ? +J : J === "undefined" ? i : G[J] !== i ? G[J] : J } if (N) { for (; M <= N; M++) { P = R[M] === "" ? O.length : R[M]; O = O[P] = M < N ? O[P] || (R[M + 1] && isNaN(R[M + 1]) ? {} : []) : J } } else { if ($.isArray(H[P])) { H[P].push(J) } else { if (H[P] !== i) { H[P] = [H[P], J] } else { H[P] = J } } } } else { if (P) { H[P] = F ? i : "" } } }); return H }; function z(H, F, G) { if (F === i || typeof F === "boolean") { G = F; F = a[H ? D : A]() } else { F = E(F) ? F.replace(H ? w : x, "") : F } return l(F, G) } l[A] = B(z, 0); l[D] = v = B(z, 1); $[y] || ($[y] = function (F) { return $.extend(C, F) })({ a: k, base: k, iframe: t, img: t, input: t, form: "action", link: k, script: t }); j = $[y]; function s(I, G, H, F) { if (!E(H) && typeof H !== "object") { F = H; H = G; G = i } return this.each(function () { var L = $(this), J = G || j()[(this.nodeName || "").toLowerCase()] || "", K = J && L.attr(J) || ""; L.attr(J, a[I](K, H, F)) }) } $.fn[A] = B(s, A); $.fn[D] = B(s, D); b.pushState = q = function (I, F) { if (E(I) && /^#/.test(I) && F === i) { F = 2 } var H = I !== i, G = c(p[g][k], H ? I : {}, H ? F : 2); p[g][k] = G + (/#/.test(G) ? "" : "#") }; b.getState = u = function (F, G) { return F === i || typeof F === "boolean" ? v(F) : v(G)[F] }; b.removeState = function (F) { var G = {}; if (F !== i) { G = u(); $.each($.isArray(F) ? F : arguments, function (I, H) { delete G[H] }) } q(G, 2) }; e[d] = $.extend(e[d], { add: function (F) { var H; function G(J) { var I = J[D] = c(); J.getState = function (K, L) { return K === i || typeof K === "boolean" ? l(I, K) : l(I, L)[K] }; H.apply(this, arguments) } if ($.isFunction(F)) { H = F; return G } else { H = F.handler; F.handler = G } } }) })(jQuery, this);
/*
* jQuery hashchange event - v1.2 - 2/11/2010
* http://benalman.com/projects/jquery-hashchange-plugin/
*
* Copyright (c) 2010 "Cowboy" Ben Alman
* Dual licensed under the MIT and GPL licenses.
* http://benalman.com/about/license/
*/
(function ($, i, b) { var j, k = $.event.special, c = "location", d = "hashchange", l = "href", f = $.browser, g = document.documentMode, h = f.msie && (g === b || g < 8), e = "on" + d in i && !h; function a(m) { m = m || i[c][l]; return m.replace(/^[^#]*#?(.*)$/, "$1") } $[d + "Delay"] = 100; k[d] = $.extend(k[d], { setup: function () { if (e) { return false } $(j.start) }, teardown: function () { if (e) { return false } $(j.stop) } }); j = (function () { var m = {}, r, n, o, q; function p() { o = q = function (s) { return s }; if (h) { n = $('<iframe src="javascript:0"/>').hide().insertAfter("body")[0].contentWindow; q = function () { return a(n.document[c][l]) }; o = function (u, s) { if (u !== s) { var t = n.document; t.open().close(); t[c].hash = "#" + u } }; o(a()) } } m.start = function () { if (r) { return } var t = a(); o || p(); (function s() { var v = a(), u = q(t); if (v !== t) { o(t = v, u); $(i).trigger(d) } else { if (u !== t) { i[c][l] = i[c][l].replace(/#.*/, "") + "#" + u } } r = setTimeout(s, $[d + "Delay"]) })() }; m.stop = function () { if (!n) { r && clearTimeout(r); r = 0 } }; return m })() })(jQuery, this);

/**
* Dragdealer JS v0.9.5
* http://code.ovidiu.ch/dragdealer-js
*
* Copyright (c) 2010, Ovidiu Chereches
* MIT License
* http://legal.ovidiu.ch/licenses/MIT
*/

/* Cursor */

var Cursor = {
    x: 0, y: 0,
    init: function () {
        this.setEvent('mouse');
        this.setEvent('touch');
    },
    setEvent: function (type) {
        var moveHandler = document['on' + type + 'move'] || function () { };
        document['on' + type + 'move'] = function (e) {
            moveHandler(e);
            Cursor.refresh(e);
        }
    },
    refresh: function (e) {
        if (!e) {
            e = window.event;
        }
        if (e.type == 'mousemove') {
            this.set(e);
        }
        else if (e.touches) {
            this.set(e.touches[0]);
        }
    },
    set: function (e) {
        if (e.pageX || e.pageY) {
            this.x = e.pageX;
            this.y = e.pageY;
        }
        else if (e.clientX || e.clientY) {
            this.x = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
            this.y = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
        }
    }
};
Cursor.init();

/* Position */

var Position = {
    get: function (obj) {
        var curleft = curtop = 0;
        if (obj.offsetParent) {
            do {
                curleft += obj.offsetLeft;
                curtop += obj.offsetTop;
            }
            while ((obj = obj.offsetParent));
        }
        return [curleft, curtop];
    }
};

/* Dragdealer */

var Dragdealer = function (wrapper, options) {
    if (typeof (wrapper) == 'string') {
        wrapper = document.getElementById(wrapper);
    }
    if (!wrapper) {
        return;
    }
    var handle = wrapper.getElementsByTagName('div')[0];
    if (!handle || handle.className.search(/(^|\s)handle(\s|$)/) == -1) {
        return;
    }
    this.init(wrapper, handle, options || {});
    this.setup();
};

Dragdealer.prototype = {
    init: function (wrapper, handle, options) {
        this.wrapper = wrapper;
        this.handle = handle;
        this.options = options;

        this.disabled = this.getOption('disabled', false);
        this.horizontal = this.getOption('horizontal', true);
        this.vertical = this.getOption('vertical', false);
        this.slide = this.getOption('slide', true);
        this.steps = this.getOption('steps', 0);
        this.snap = this.getOption('snap', false);
        this.loose = this.getOption('loose', false);
        this.speed = this.getOption('speed', 10) / 100;
        this.xPrecision = this.getOption('xPrecision', 0);
        this.yPrecision = this.getOption('yPrecision', 0);

        this.callback = options.callback || null;
        this.animationCallback = options.animationCallback || null;

        this.bounds = {
            left: options.left || 0, right: -(options.right || 0),
            top: options.top || 0, bottom: -(options.bottom || 0),
            x0: 0, x1: 0, xRange: 0,
            y0: 0, y1: 0, yRange: 0
        };
        this.value = {
            prev: [-1, -1],
            current: [options.x || 0, options.y || 0],
            target: [options.x || 0, options.y || 0]
        };
        this.offset = {
            wrapper: [0, 0],
            mouse: [0, 0],
            prev: [-999999, -999999],
            current: [0, 0],
            target: [0, 0]
        };
        this.change = [0, 0];

        this.activity = false;
        this.dragging = false;
        this.tapping = false;
    },
    getOption: function (name, defaultValue) {
        return this.options[name] !== undefined ? this.options[name] : defaultValue;
    },
    setup: function () {
        this.setWrapperOffset();
        this.setBoundsPadding();
        this.setBounds();
        this.setSteps();

        this.addListeners();
    },
    setWrapperOffset: function () {
        this.offset.wrapper = Position.get(this.wrapper);
    },
    setBoundsPadding: function () {
        if (!this.bounds.left && !this.bounds.right) {
            this.bounds.left = Position.get(this.handle)[0] - this.offset.wrapper[0];
            this.bounds.right = -this.bounds.left;
        }
        if (!this.bounds.top && !this.bounds.bottom) {
            this.bounds.top = Position.get(this.handle)[1] - this.offset.wrapper[1];
            this.bounds.bottom = -this.bounds.top;
        }
    },
    setBounds: function () {
        this.bounds.x0 = this.bounds.left;
        this.bounds.x1 = this.wrapper.offsetWidth + this.bounds.right;
        this.bounds.xRange = (this.bounds.x1 - this.bounds.x0) - this.handle.offsetWidth;

        this.bounds.y0 = this.bounds.top;
        this.bounds.y1 = this.wrapper.offsetHeight + this.bounds.bottom;
        this.bounds.yRange = (this.bounds.y1 - this.bounds.y0) - this.handle.offsetHeight;

        this.bounds.xStep = 1 / (this.xPrecision || Math.max(this.wrapper.offsetWidth, this.handle.offsetWidth));
        this.bounds.yStep = 1 / (this.yPrecision || Math.max(this.wrapper.offsetHeight, this.handle.offsetHeight));
    },
    setSteps: function () {
        if (this.steps > 1) {
            this.stepRatios = [];
            for (var i = 0; i <= this.steps - 1; i++) {
                this.stepRatios[i] = i / (this.steps - 1);
            }
        }
    },
    addListeners: function () {
        var self = this;

        this.wrapper.onselectstart = function () {
            return false;
        }
        this.handle.onmousedown = this.handle.ontouchstart = function (e) {
            self.handleDownHandler(e);
        };
        this.wrapper.onmousedown = this.wrapper.ontouchstart = function (e) {
            self.wrapperDownHandler(e);
        };
        var mouseUpHandler = document.onmouseup || function () { };
        document.onmouseup = function (e) {
            mouseUpHandler(e);
            self.documentUpHandler(e);
        };
        var touchEndHandler = document.ontouchend || function () { };
        document.ontouchend = function (e) {
            touchEndHandler(e);
            self.documentUpHandler(e);
        };
        var resizeHandler = window.onresize || function () { };
        window.onresize = function (e) {
            resizeHandler(e);
            self.documentResizeHandler(e);
        };
        this.wrapper.onmousemove = function (e) {
            self.activity = true;
        }
        this.wrapper.onclick = function (e) {
            return !self.activity;
        }

        this.interval = setInterval(function () { self.animate() }, 25);
        self.animate(false, true);
    },
    handleDownHandler: function (e) {
        this.activity = false;
        Cursor.refresh(e);

        this.preventDefaults(e, true);
        this.startDrag();
        this.cancelEvent(e);
    },
    wrapperDownHandler: function (e) {
        Cursor.refresh(e);

        this.preventDefaults(e, true);
        this.startTap();
    },
    documentUpHandler: function (e) {
        this.stopDrag();
        this.stopTap();
        //this.cancelEvent(e);
    },
    documentResizeHandler: function (e) {
        this.setWrapperOffset();
        this.setBounds();

        this.update();
    },
    enable: function () {
        this.disabled = false;
        this.handle.className = this.handle.className.replace(/\s?disabled/g, '');
    },
    disable: function () {
        this.disabled = true;
        this.handle.className += ' disabled';
    },
    setStep: function (x, y, snap) {
        this.setValue(
this.steps && x > 1 ? (x - 1) / (this.steps - 1) : 0,
this.steps && y > 1 ? (y - 1) / (this.steps - 1) : 0,
snap
);
    },
    setValue: function (x, y, snap) {
        this.setTargetValue([x, y || 0]);
        if (snap) {
            this.groupCopy(this.value.current, this.value.target);
        }
    },
    startTap: function (target) {
        if (this.disabled) {
            return;
        }
        this.tapping = true;

        if (target === undefined) {
            target = [
Cursor.x - this.offset.wrapper[0] - (this.handle.offsetWidth / 2),
Cursor.y - this.offset.wrapper[1] - (this.handle.offsetHeight / 2)
];
        }
        this.setTargetOffset(target);
    },
    stopTap: function () {
        if (this.disabled || !this.tapping) {
            return;
        }
        this.tapping = false;

        this.setTargetValue(this.value.current);
        this.result();
    },
    startDrag: function () {
        if (this.disabled) {
            return;
        }
        this.offset.mouse = [
Cursor.x - Position.get(this.handle)[0],
Cursor.y - Position.get(this.handle)[1]
];
        this.dragging = true;
    },
    stopDrag: function () {
        if (this.disabled || !this.dragging) {
            return;
        }
        this.dragging = false;

        var target = this.groupClone(this.value.current);
        if (this.slide) {
            var ratioChange = this.change;
            target[0] += ratioChange[0] * 4;
            target[1] += ratioChange[1] * 4;
        }
        this.setTargetValue(target);
        this.result();
    },
    feedback: function () {
        var value = this.value.current;
        if (this.snap && this.steps > 1) {
            value = this.getClosestSteps(value);
        }
        if (!this.groupCompare(value, this.value.prev)) {
            if (typeof (this.animationCallback) == 'function') {
                this.animationCallback(value[0], value[1]);
            }
            this.groupCopy(this.value.prev, value);
        }
    },
    result: function () {
        if (typeof (this.callback) == 'function') {
            this.callback(this.value.target[0], this.value.target[1]);
        }
    },
    animate: function (direct, first) {
        if (direct && !this.dragging) {
            return;
        }
        if (this.dragging) {
            var prevTarget = this.groupClone(this.value.target);

            var offset = [
Cursor.x - this.offset.wrapper[0] - this.offset.mouse[0],
Cursor.y - this.offset.wrapper[1] - this.offset.mouse[1]
];
            this.setTargetOffset(offset, this.loose);

            this.change = [
this.value.target[0] - prevTarget[0],
this.value.target[1] - prevTarget[1]
];
        }
        if (this.dragging || first) {
            this.groupCopy(this.value.current, this.value.target);
        }
        if (this.dragging || this.glide() || first) {
            this.update();
            this.feedback();
        }
    },
    glide: function () {
        var diff = [
this.value.target[0] - this.value.current[0],
this.value.target[1] - this.value.current[1]
];
        if (!diff[0] && !diff[1]) {
            return false;
        }
        if (Math.abs(diff[0]) > this.bounds.xStep || Math.abs(diff[1]) > this.bounds.yStep) {
            this.value.current[0] += diff[0] * this.speed;
            this.value.current[1] += diff[1] * this.speed;
        }
        else {
            this.groupCopy(this.value.current, this.value.target);
        }
        return true;
    },
    update: function () {
        if (!this.snap) {
            this.offset.current = this.getOffsetsByRatios(this.value.current);
        }
        else {
            this.offset.current = this.getOffsetsByRatios(
this.getClosestSteps(this.value.current)
);
        }
        this.show();
    },
    show: function () {
        if (!this.groupCompare(this.offset.current, this.offset.prev)) {
            if (this.horizontal) {
                this.handle.style.left = String(this.offset.current[0]) + 'px';
            }
            if (this.vertical) {
                this.handle.style.top = String(this.offset.current[1]) + 'px';
            }
            this.groupCopy(this.offset.prev, this.offset.current);
        }
    },
    setTargetValue: function (value, loose) {
        var target = loose ? this.getLooseValue(value) : this.getProperValue(value);

        this.groupCopy(this.value.target, target);
        this.offset.target = this.getOffsetsByRatios(target);
    },
    setTargetOffset: function (offset, loose) {
        var value = this.getRatiosByOffsets(offset);
        var target = loose ? this.getLooseValue(value) : this.getProperValue(value);

        this.groupCopy(this.value.target, target);
        this.offset.target = this.getOffsetsByRatios(target);
    },
    getLooseValue: function (value) {
        var proper = this.getProperValue(value);
        return [
proper[0] + ((value[0] - proper[0]) / 4),
proper[1] + ((value[1] - proper[1]) / 4)
];
    },
    getProperValue: function (value) {
        var proper = this.groupClone(value);

        proper[0] = Math.max(proper[0], 0);
        proper[1] = Math.max(proper[1], 0);
        proper[0] = Math.min(proper[0], 1);
        proper[1] = Math.min(proper[1], 1);

        if ((!this.dragging && !this.tapping) || this.snap) {
            if (this.steps > 1) {
                proper = this.getClosestSteps(proper);
            }
        }
        return proper;
    },
    getRatiosByOffsets: function (group) {
        return [
this.getRatioByOffset(group[0], this.bounds.xRange, this.bounds.x0),
this.getRatioByOffset(group[1], this.bounds.yRange, this.bounds.y0)
];
    },
    getRatioByOffset: function (offset, range, padding) {
        return range ? (offset - padding) / range : 0;
    },
    getOffsetsByRatios: function (group) {
        return [
this.getOffsetByRatio(group[0], this.bounds.xRange, this.bounds.x0),
this.getOffsetByRatio(group[1], this.bounds.yRange, this.bounds.y0)
];
    },
    getOffsetByRatio: function (ratio, range, padding) {
        return Math.round(ratio * range) + padding;
    },
    getClosestSteps: function (group) {
        return [
this.getClosestStep(group[0]),
this.getClosestStep(group[1])
];
    },
    getClosestStep: function (value) {
        var k = 0;
        var min = 1;
        for (var i = 0; i <= this.steps - 1; i++) {
            if (Math.abs(this.stepRatios[i] - value) < min) {
                min = Math.abs(this.stepRatios[i] - value);
                k = i;
            }
        }
        return this.stepRatios[k];
    },
    groupCompare: function (a, b) {
        return a[0] == b[0] && a[1] == b[1];
    },
    groupCopy: function (a, b) {
        a[0] = b[0];
        a[1] = b[1];
    },
    groupClone: function (a) {
        return [a[0], a[1]];
    },
    preventDefaults: function (e, selection) {
        if (!e) {
            e = window.event;
        }
        if (e.preventDefault) {
            e.preventDefault();
        }
        e.returnValue = false;

        if (selection && document.selection) {
            document.selection.empty();
        }
    },
    cancelEvent: function (e) {
        if (!e) {
            e = window.event;
        }
        if (e.stopPropagation) {
            e.stopPropagation();
        }
        e.cancelBubble = true;
    }
};


(function ($, undefined) {

    var Leonni = {};

    Leonni.PrepareGlobalErrorMessage = function () {
        var container = $(".global-error-success-message");
        container.empty();
        container.append("<div class='global-error-message'><div class='message-closer'><a href='#'>×</a></div><div><ul></ul></div></div>");
        return container;
    };

    Leonni.PrepareGlobalSuccessMessage = function () {
        var container = $(".global-error-success-message");
        container.empty();
        container.append("<div class='global-success-message'><div class='message-closer'><a href='#'>×</a></div><div><ul></ul></div></div>");
        //container.append("<div class='global-error-message'><div class='message-closer'><a href='#'>×</a></div><div class='message-text'></div></div>");
        return container;
    };

    Leonni.ShowErrorMessage = function (errors) {
        var container = Leonni.PrepareGlobalErrorMessage();
        var ul = container.find("ul");

        if (typeof errors == "string") {
            ul.html("<li>" + errors + "</li><div style='border-bottom:1px solid #ccc'>&nbsp;</div>");
        }
        else if (jQuery.isArray(errors)) {
            var html = "";
            jQuery.each(errors, function (i, item) {
            var arr = item.split("---");
            //arr[0] - Gives the Error Definition
            //arr[1] - Give the Error Description
            if(arr.length == 2)
                html += "<li>" + arr[0] + "<div style='border-bottom:1px solid #ccc'>&nbsp;</div></li><li>" + arr[1] + "</li>"
                else
                {
                html += "<li>" + arr[0] + "<div style='border-bottom:1px solid #ccc'>&nbsp;</div></li>"
                }
            });
            ul.html(html);
        }
    };

    Leonni.ShowSuccessMessage = function (msg) {
        var container = Leonni.PrepareGlobalSuccessMessage();
       var ul = container.find("ul");
        if (typeof msg == "string") {
            ul.html("<li>" + msg + "</li><div>&nbsp;</div>");
        }
        else if (jQuery.isArray(msg)) {
            var html = "";
            jQuery.each(errors, function (i, item) {
            var arr = item.split("---");
            //arr[0] - Gives the Error Definition
            //arr[1] - Give the Error Description
            if(arr.length == 2)
                html += "<li>" + arr[0] + "<div style='border-bottom:1px solid #ccc'>&nbsp;</div></li><li>" + arr[1] + "</li>"
                else
                {
                html += "<li>" + arr[0] + "<div style='border-bottom:1px solid #ccc'>&nbsp;</div></li>"
                }
            });
            ul.html(html);
        }
    };

    $(function () {
        $(".message-closer").live("click", function () {
            var messageDiv = $(this).parent();
            messageDiv.fadeOut("slow", function () { messageDiv.remove(); });
        });
    });

    window["Leonni"] = Leonni;

    var $window = $(window);
    var winhashchanged = function () {

        if ($window.data("hardTrigger") === true) {
            $window.data("hardTrigger", false);
        }
        else {
            $(".global-error-success-message:first").html("");
        }

        var show_box = $.bbq.getState("show") || "login";
        if (show_box == "login") {
            $(".register-container").hide();
            $(".forgotPassword-container").hide();
            $(".logon-container").show();
        }
        else if (show_box == "register") {
            $(".register-container").show();
            $(".forgotPassword-container").hide();
            $(".logon-container").hide();
        }
        else if (show_box == "forgotpassword") {
            $(".register-container").hide();
            $(".forgotPassword-container").show();
            $(".logon-container").hide();
        }
    };

    $window.bind("hashchange", function (e) {
        winhashchanged();
    });

    $(document).ready(function () {

        $window.data("hardTrigger", true);
        $window.trigger('hashchange');

        var btnShowSignUpFields = $("#show_signup_fields");
        var linkForgotPassword = $("#link-forgot-password");

        linkForgotPassword.click(function (e) {
            e.preventDefault();
            $.bbq.pushState({ "show": "forgotpassword" });
            $("#Email").val($("#Logon_Username").val());
        });

        btnShowSignUpFields.click(function (e) {
            $("#Register_Username").val($("#Logon_Username").val());
            $("#Register_Password").val($("#Logon_Password").val());
            $.bbq.pushState({ "show": "register" });
        });

    });

    //end document ready

    $(window).scroll(function () {
        if ($(window).scrollTop() > 131) {
            $(".global-error-success-message").css({ "position": "fixed", "top": 0, "width": "432px", "background": "#fff" });
        }
        else {
            $(".global-error-success-message").css({ "position": "" });
        }
    });

})(jQuery);
