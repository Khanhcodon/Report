/*
 Highstock JS v8.1.2 (2020-06-16)

 (c) 2009-2018 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (Q, O) { "object" === typeof module && module.exports ? (O["default"] = O, module.exports = Q.document ? O(Q) : O) : "function" === typeof define && define.amd ? define("highcharts/highstock", function () { return O(Q) }) : (Q.Highcharts && Q.Highcharts.error(16, !0), Q.Highcharts = O(Q)) })("undefined" !== typeof window ? window : this, function (Q) {
    function O(p, e, q, B) { p.hasOwnProperty(e) || (p[e] = B.apply(null, q)) } var q = {}; O(q, "parts/Globals.js", [], function () {
        var p = "undefined" !== typeof Q ? Q : "undefined" !== typeof window ? window : {}, e = p.document,
        q = p.navigator && p.navigator.userAgent || "", B = e && e.createElementNS && !!e.createElementNS("http://www.w3.org/2000/svg", "svg").createSVGRect, D = /(edge|msie|trident)/i.test(q) && !p.opera, z = -1 !== q.indexOf("Firefox"), J = -1 !== q.indexOf("Chrome"), G = z && 4 > parseInt(q.split("Firefox/")[1], 10); return {
            product: "Highcharts", version: "8.1.2", deg2rad: 2 * Math.PI / 360, doc: e, hasBidiBug: G, hasTouch: !!p.TouchEvent, isMS: D, isWebKit: -1 !== q.indexOf("AppleWebKit"), isFirefox: z, isChrome: J, isSafari: !J && -1 !== q.indexOf("Safari"), isTouchDevice: /(Mobile|Android|Windows Phone)/.test(q),
            SVG_NS: "http://www.w3.org/2000/svg", chartCount: 0, seriesTypes: {}, symbolSizes: {}, svg: B, win: p, marginNames: ["plotTop", "marginRight", "marginBottom", "plotLeft"], noop: function () { }, charts: [], dateFormats: {}
        }
    }); O(q, "parts/Utilities.js", [q["parts/Globals.js"]], function (p) {
        function e(b, n, a, g) {
            var L = n ? "Highcharts error" : "Highcharts warning"; 32 === b && (b = L + ": Deprecated member"); var k = C(b), c = k ? L + " #" + b + ": www.highcharts.com/errors/" + b + "/" : b.toString(); L = function () {
                if (n) throw Error(c); H.console && -1 === e.messages.indexOf(c) &&
                    console.log(c)
            }; if ("undefined" !== typeof g) { var N = ""; k && (c += "?"); V(g, function (b, n) { N += "\n - " + n + ": " + b; k && (c += encodeURI(n) + "=" + encodeURI(b)) }); c += N } a ? ea(a, "displayError", { code: b, message: c, params: g }, L) : L(); e.messages.push(c)
        } function q() { var b, n = arguments, a = {}, g = function (b, n) { "object" !== typeof b && (b = {}); V(n, function (a, L) { !B(a, !0) || u(a) || r(a) ? b[L] = n[L] : b[L] = g(b[L] || {}, a) }); return b }; !0 === n[0] && (a = n[1], n = Array.prototype.slice.call(n, 2)); var L = n.length; for (b = 0; b < L; b++)a = g(a, n[b]); return a } function B(b,
            n) { return !!b && "object" === typeof b && (!n || !w(b)) } function D(b, n, a) { var g; K(n) ? f(a) ? b.setAttribute(n, a) : b && b.getAttribute && ((g = b.getAttribute(n)) || "class" !== n || (g = b.getAttribute(n + "Name"))) : V(n, function (n, a) { b.setAttribute(a, n) }); return g } function z() { for (var b = arguments, n = b.length, a = 0; a < n; a++) { var g = b[a]; if ("undefined" !== typeof g && null !== g) return g } } function J(b, n) {
                if (!b) return n; var a = b.split(".").reverse(); if (1 === a.length) return n[b]; for (b = a.pop(); "undefined" !== typeof b && "undefined" !== typeof n && null !==
                    n;)n = n[b], b = a.pop(); return n
            } p.timers = []; var G = p.charts, m = p.doc, H = p.win; (e || (e = {})).messages = []; p.error = e; var M = function () {
                function b(b, n, a) { this.options = n; this.elem = b; this.prop = a } b.prototype.dSetter = function () {
                    var b = this.paths, n = b && b[0]; b = b && b[1]; var a = [], g = this.now || 0; if (1 !== g && n && b) if (n.length === b.length && 1 > g) for (var L = 0; L < b.length; L++) { for (var c = n[L], k = b[L], N = [], d = 0; d < k.length; d++) { var l = c[d], f = k[d]; N[d] = "number" === typeof l && "number" === typeof f && ("A" !== k[0] || 4 !== d && 5 !== d) ? l + g * (f - l) : f } a.push(N) } else a =
                        b; else a = this.toD || []; this.elem.attr("d", a, void 0, !0)
                }; b.prototype.update = function () { var b = this.elem, n = this.prop, a = this.now, g = this.options.step; if (this[n + "Setter"]) this[n + "Setter"](); else b.attr ? b.element && b.attr(n, a, null, !0) : b.style[n] = a + this.unit; g && g.call(b, a, this) }; b.prototype.run = function (b, n, a) {
                    var g = this, L = g.options, c = function (b) { return c.stopped ? !1 : g.step(b) }, k = H.requestAnimationFrame || function (b) { setTimeout(b, 13) }, N = function () {
                        for (var b = 0; b < p.timers.length; b++)p.timers[b]() || p.timers.splice(b--,
                            1); p.timers.length && k(N)
                    }; b !== n || this.elem["forceAnimate:" + this.prop] ? (this.startTime = +new Date, this.start = b, this.end = n, this.unit = a, this.now = this.start, this.pos = 0, c.elem = this.elem, c.prop = this.prop, c() && 1 === p.timers.push(c) && k(N)) : (delete L.curAnim[this.prop], L.complete && 0 === Object.keys(L.curAnim).length && L.complete.call(this.elem))
                }; b.prototype.step = function (b) {
                    var n = +new Date, a = this.options, g = this.elem, L = a.complete, c = a.duration, k = a.curAnim; if (g.attr && !g.element) b = !1; else if (b || n >= c + this.startTime) {
                    this.now =
                        this.end; this.pos = 1; this.update(); var N = k[this.prop] = !0; V(k, function (b) { !0 !== b && (N = !1) }); N && L && L.call(g); b = !1
                    } else this.pos = a.easing((n - this.startTime) / c), this.now = this.start + (this.end - this.start) * this.pos, this.update(), b = !0; return b
                }; b.prototype.initPath = function (b, n, a) {
                    function g(b, n) { for (; b.length < v;) { var a = b[0], g = n[v - b.length]; g && "M" === a[0] && (b[0] = "C" === g[0] ? ["C", a[1], a[2], a[1], a[2], a[1], a[2]] : ["L", a[1], a[2]]); b.unshift(a); N && b.push(b[b.length - 1]) } } function L(b, n) {
                        for (; b.length < v;)if (n = b[b.length /
                            d - 1].slice(), "C" === n[0] && (n[1] = n[5], n[2] = n[6]), N) { var a = b[b.length / d].slice(); b.splice(b.length / 2, 0, n, a) } else b.push(n)
                    } var c = b.startX, k = b.endX; n = n && n.slice(); a = a.slice(); var N = b.isArea, d = N ? 2 : 1; if (!n) return [a, a]; if (c && k) { for (b = 0; b < c.length; b++)if (c[b] === k[0]) { var l = b; break } else if (c[0] === k[k.length - c.length + b]) { l = b; var f = !0; break } else if (c[c.length - 1] === k[k.length - c.length + b]) { l = c.length - b; break } "undefined" === typeof l && (n = []) } if (n.length && C(l)) { var v = a.length + l * d; f ? (g(n, a), L(a, n)) : (g(a, n), L(n, a)) } return [n,
                        a]
                }; b.prototype.fillSetter = function () { b.prototype.strokeSetter.apply(this, arguments) }; b.prototype.strokeSetter = function () { this.elem.attr(this.prop, p.color(this.start).tweenTo(p.color(this.end), this.pos), null, !0) }; return b
            }(); p.Fx = M; p.merge = q; var A = p.pInt = function (b, n) { return parseInt(b, n || 10) }, K = p.isString = function (b) { return "string" === typeof b }, w = p.isArray = function (b) { b = Object.prototype.toString.call(b); return "[object Array]" === b || "[object Array Iterator]" === b }; p.isObject = B; var r = p.isDOMElement = function (b) {
                return B(b) &&
                    "number" === typeof b.nodeType
            }, u = p.isClass = function (b) { var n = b && b.constructor; return !(!B(b, !0) || r(b) || !n || !n.name || "Object" === n.name) }, C = p.isNumber = function (b) { return "number" === typeof b && !isNaN(b) && Infinity > b && -Infinity < b }, h = p.erase = function (b, n) { for (var a = b.length; a--;)if (b[a] === n) { b.splice(a, 1); break } }, f = p.defined = function (b) { return "undefined" !== typeof b && null !== b }; p.attr = D; var d = p.splat = function (b) { return w(b) ? b : [b] }, t = p.syncTimeout = function (b, n, a) { if (0 < n) return setTimeout(b, n, a); b.call(0, a); return -1 },
                l = p.clearTimeout = function (b) { f(b) && clearTimeout(b) }, c = p.extend = function (b, n) { var a; b || (b = {}); for (a in n) b[a] = n[a]; return b }; p.pick = z; var a = p.css = function (b, n) { p.isMS && !p.svg && n && "undefined" !== typeof n.opacity && (n.filter = "alpha(opacity=" + 100 * n.opacity + ")"); c(b.style, n) }, x = p.createElement = function (b, n, g, L, k) { b = m.createElement(b); n && c(b, n); k && a(b, { padding: "0", border: "none", margin: "0" }); g && a(b, g); L && L.appendChild(b); return b }, v = p.extendClass = function (b, n) {
                    var a = function () { }; a.prototype = new b; c(a.prototype,
                        n); return a
                }, E = p.pad = function (b, n, a) { return Array((n || 2) + 1 - String(b).replace("-", "").length).join(a || "0") + b }, F = p.relativeLength = function (b, n, a) { return /%$/.test(b) ? n * parseFloat(b) / 100 + (a || 0) : parseFloat(b) }, k = p.wrap = function (b, n, a) { var g = b[n]; b[n] = function () { var b = Array.prototype.slice.call(arguments), n = arguments, L = this; L.proceed = function () { g.apply(L, arguments.length ? arguments : n) }; b.unshift(g); b = a.apply(this, b); L.proceed = null; return b } }, y = p.format = function (b, n, a) {
                    var g = "{", L = !1, c = [], k = /f$/, N = /\.([0-9])/,
                    d = p.defaultOptions.lang, l = a && a.time || p.time; for (a = a && a.numberFormatter || Y; b;) { var f = b.indexOf(g); if (-1 === f) break; var v = b.slice(0, f); if (L) { v = v.split(":"); g = J(v.shift() || "", n); if (v.length && "number" === typeof g) if (v = v.join(":"), k.test(v)) { var x = parseInt((v.match(N) || ["", "-1"])[1], 10); null !== g && (g = a(g, x, d.decimalPoint, -1 < v.indexOf(",") ? d.thousandsSep : "")) } else g = l.dateFormat(v, g); c.push(g) } else c.push(v); b = b.slice(f + 1); g = (L = !L) ? "}" : "{" } c.push(b); return c.join("")
                }, I = p.getMagnitude = function (b) {
                    return Math.pow(10,
                        Math.floor(Math.log(b) / Math.LN10))
                }, P = p.normalizeTickInterval = function (b, n, a, g, L) { var c = b; a = z(a, 1); var k = b / a; n || (n = L ? [1, 1.2, 1.5, 2, 2.5, 3, 4, 5, 6, 8, 10] : [1, 2, 2.5, 5, 10], !1 === g && (1 === a ? n = n.filter(function (b) { return 0 === b % 1 }) : .1 >= a && (n = [1 / a]))); for (g = 0; g < n.length && !(c = n[g], L && c * a >= b || !L && k <= (n[g] + (n[g + 1] || n[g])) / 2); g++); return c = S(c * a, -Math.round(Math.log(.001) / Math.LN10)) }, g = p.stableSort = function (b, n) {
                    var a = b.length, g, L; for (L = 0; L < a; L++)b[L].safeI = L; b.sort(function (b, a) {
                        g = n(b, a); return 0 === g ? b.safeI - a.safeI :
                            g
                    }); for (L = 0; L < a; L++)delete b[L].safeI
                }, b = p.arrayMin = function (b) { for (var n = b.length, a = b[0]; n--;)b[n] < a && (a = b[n]); return a }, n = p.arrayMax = function (b) { for (var n = b.length, a = b[0]; n--;)b[n] > a && (a = b[n]); return a }, L = p.destroyObjectProperties = function (b, n) { V(b, function (a, g) { a && a !== n && a.destroy && a.destroy(); delete b[g] }) }, N = p.discardElement = function (b) { var n = p.garbageBin; n || (n = x("div")); b && n.appendChild(b); n.innerHTML = "" }, S = p.correctFloat = function (b, n) { return parseFloat(b.toPrecision(n || 14)) }, aa = p.setAnimation =
                    function (b, n) { n.renderer.globalAnimation = z(b, n.options.chart.animation, !0) }, Z = p.animObject = function (b) { return B(b) ? q(b) : { duration: b ? 500 : 0 } }, ba = p.timeUnits = { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5, month: 24192E5, year: 314496E5 }, Y = p.numberFormat = function (b, n, a, g) {
                        b = +b || 0; n = +n; var L = p.defaultOptions.lang, c = (b.toString().split(".")[1] || "").split("e")[0].length, k = b.toString().split("e"); if (-1 === n) n = Math.min(c, 20); else if (!C(n)) n = 2; else if (n && k[1] && 0 > k[1]) {
                            var N = n + +k[1]; 0 <= N ?
                                (k[0] = (+k[0]).toExponential(N).split("e")[0], n = N) : (k[0] = k[0].split(".")[0] || 0, b = 20 > n ? (k[0] * Math.pow(10, k[1])).toFixed(n) : 0, k[1] = 0)
                        } var d = (Math.abs(k[1] ? k[0] : b) + Math.pow(10, -Math.max(n, c) - 1)).toFixed(n); c = String(A(d)); N = 3 < c.length ? c.length % 3 : 0; a = z(a, L.decimalPoint); g = z(g, L.thousandsSep); b = (0 > b ? "-" : "") + (N ? c.substr(0, N) + g : ""); b += c.substr(N).replace(/(\d{3})(?=\d)/g, "$1" + g); n && (b += a + d.slice(-n)); k[1] && 0 !== +b && (b += "e" + k[1]); return b
                    }; Math.easeInOutSine = function (b) { return -.5 * (Math.cos(Math.PI * b) - 1) }; var ca =
                        p.getStyle = function (b, n, a) {
                            if ("width" === n) return n = Math.min(b.offsetWidth, b.scrollWidth), a = b.getBoundingClientRect && b.getBoundingClientRect().width, a < n && a >= n - 1 && (n = Math.floor(a)), Math.max(0, n - p.getStyle(b, "padding-left") - p.getStyle(b, "padding-right")); if ("height" === n) return Math.max(0, Math.min(b.offsetHeight, b.scrollHeight) - p.getStyle(b, "padding-top") - p.getStyle(b, "padding-bottom")); H.getComputedStyle || e(27, !0); if (b = H.getComputedStyle(b, void 0)) b = b.getPropertyValue(n), z(a, "opacity" !== n) && (b = A(b));
                            return b
                        }, da = p.inArray = function (b, n, a) { e(32, !1, void 0, { "Highcharts.inArray": "use Array.indexOf" }); return n.indexOf(b, a) }, T = p.find = Array.prototype.find ? function (b, n) { return b.find(n) } : function (b, n) { var a, g = b.length; for (a = 0; a < g; a++)if (n(b[a], a)) return b[a] }; p.keys = function (b) { e(32, !1, void 0, { "Highcharts.keys": "use Object.keys" }); return Object.keys(b) }; var W = p.offset = function (b) {
                            var n = m.documentElement; b = b.parentElement || b.parentNode ? b.getBoundingClientRect() : { top: 0, left: 0 }; return {
                                top: b.top + (H.pageYOffset ||
                                    n.scrollTop) - (n.clientTop || 0), left: b.left + (H.pageXOffset || n.scrollLeft) - (n.clientLeft || 0)
                            }
                        }, fa = p.stop = function (b, n) { for (var a = p.timers.length; a--;)p.timers[a].elem !== b || n && n !== p.timers[a].prop || (p.timers[a].stopped = !0) }, V = p.objectEach = function (b, n, a) { for (var g in b) Object.hasOwnProperty.call(b, g) && n.call(a || b[g], b[g], g, b) }; V({ map: "map", each: "forEach", grep: "filter", reduce: "reduce", some: "some" }, function (b, n) {
                        p[n] = function (a) {
                            var g; e(32, !1, void 0, (g = {}, g["Highcharts." + n] = "use Array." + b, g)); return Array.prototype[b].apply(a,
                                [].slice.call(arguments, 1))
                        }
                        }); var ka = p.addEvent = function (b, n, a, g) {
                        void 0 === g && (g = {}); var L = b.addEventListener || p.addEventListenerPolyfill; var c = "function" === typeof b && b.prototype ? b.prototype.protoEvents = b.prototype.protoEvents || {} : b.hcEvents = b.hcEvents || {}; p.Point && b instanceof p.Point && b.series && b.series.chart && (b.series.chart.runTrackerClick = !0); L && L.call(b, n, a, !1); c[n] || (c[n] = []); c[n].push({ fn: a, order: "number" === typeof g.order ? g.order : Infinity }); c[n].sort(function (b, n) { return b.order - n.order });
                            return function () { ha(b, n, a) }
                        }, ha = p.removeEvent = function (b, n, a) { function g(n, a) { var g = b.removeEventListener || p.removeEventListenerPolyfill; g && g.call(b, n, a, !1) } function L(a) { var L; if (b.nodeName) { if (n) { var c = {}; c[n] = !0 } else c = a; V(c, function (b, n) { if (a[n]) for (L = a[n].length; L--;)g(n, a[n][L].fn) }) } } var c;["protoEvents", "hcEvents"].forEach(function (k, N) { var d = (N = N ? b : b.prototype) && N[k]; d && (n ? (c = d[n] || [], a ? (d[n] = c.filter(function (b) { return a !== b.fn }), g(n, a)) : (L(d), d[n] = [])) : (L(d), N[k] = {})) }) }, ea = p.fireEvent =
                            function (b, n, a, g) {
                                var L; a = a || {}; if (m.createEvent && (b.dispatchEvent || b.fireEvent)) { var k = m.createEvent("Events"); k.initEvent(n, !0, !0); c(k, a); b.dispatchEvent ? b.dispatchEvent(k) : b.fireEvent(n, k) } else a.target || c(a, { preventDefault: function () { a.defaultPrevented = !0 }, target: b, type: n }), function (n, g) { void 0 === n && (n = []); void 0 === g && (g = []); var c = 0, k = 0, N = n.length + g.length; for (L = 0; L < N; L++)!1 === (n[c] ? g[k] ? n[c].order <= g[k].order ? n[c++] : g[k++] : n[c++] : g[k++]).fn.call(b, a) && a.preventDefault() }(b.protoEvents && b.protoEvents[n],
                                    b.hcEvents && b.hcEvents[n]); g && !a.defaultPrevented && g.call(b, a)
                            }, la = p.animate = function (b, n, a) {
                                var g, L = "", c, k; if (!B(a)) { var N = arguments; a = { duration: N[2], easing: N[3], complete: N[4] } } C(a.duration) || (a.duration = 400); a.easing = "function" === typeof a.easing ? a.easing : Math[a.easing] || Math.easeInOutSine; a.curAnim = q(n); V(n, function (N, d) {
                                    fa(b, d); k = new M(b, a, d); c = null; "d" === d && w(n.d) ? (k.paths = k.initPath(b, b.pathArray, n.d), k.toD = n.d, g = 0, c = 1) : b.attr ? g = b.attr(d) : (g = parseFloat(ca(b, d)) || 0, "opacity" !== d && (L = "px")); c ||
                                        (c = N); c && c.match && c.match("px") && (c = c.replace(/px/g, "")); k.run(g, c, L)
                                })
                            }, U = p.seriesType = function (b, n, a, g, L) { var c = ia(), k = p.seriesTypes; c.plotOptions[b] = q(c.plotOptions[n], a); k[b] = v(k[n] || function () { }, g); k[b].prototype.type = b; L && (k[b].prototype.pointClass = v(p.Point, L)); return k[b] }, X, ja = p.uniqueKey = function () { var b = Math.random().toString(36).substring(2, 9) + "-", n = 0; return function () { return "highcharts-" + (X ? "" : b) + n++ } }(), ma = p.useSerialIds = function (b) { return X = z(b, X) }, O = p.isFunction = function (b) {
                                return "function" ===
                                    typeof b
                            }, ia = p.getOptions = function () { return p.defaultOptions }, na = p.setOptions = function (b) { p.defaultOptions = q(!0, p.defaultOptions, b); (b.time || b.global) && p.time.update(q(p.defaultOptions.global, p.defaultOptions.time, b.global, b.time)); return p.defaultOptions }; H.jQuery && (H.jQuery.fn.highcharts = function () { var b = [].slice.call(arguments); if (this[0]) return b[0] ? (new (p[K(b[0]) ? b.shift() : "Chart"])(this[0], b[0], b[1]), this) : G[D(this[0], "data-highcharts-chart")] }); return {
                                Fx: p.Fx, addEvent: ka, animate: la, animObject: Z,
                                arrayMax: n, arrayMin: b, attr: D, clamp: function (b, n, a) { return b > n ? b < a ? b : a : n }, clearTimeout: l, correctFloat: S, createElement: x, css: a, defined: f, destroyObjectProperties: L, discardElement: N, erase: h, error: e, extend: c, extendClass: v, find: T, fireEvent: ea, format: y, getMagnitude: I, getNestedProperty: J, getOptions: ia, getStyle: ca, inArray: da, isArray: w, isClass: u, isDOMElement: r, isFunction: O, isNumber: C, isObject: B, isString: K, merge: q, normalizeTickInterval: P, numberFormat: Y, objectEach: V, offset: W, pad: E, pick: z, pInt: A, relativeLength: F,
                                removeEvent: ha, seriesType: U, setAnimation: aa, setOptions: na, splat: d, stableSort: g, stop: fa, syncTimeout: t, timeUnits: ba, uniqueKey: ja, useSerialIds: ma, wrap: k
                            }
    }); O(q, "parts/Color.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
        var q = e.isNumber, B = e.merge, D = e.pInt; e = function () {
            function e(p) {
            this.parsers = [{ regex: /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]?(?:\.[0-9]+)?)\s*\)/, parse: function (e) { return [D(e[1]), D(e[2]), D(e[3]), parseFloat(e[4], 10)] } }, {
                regex: /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/,
                parse: function (e) { return [D(e[1]), D(e[2]), D(e[3]), 1] }
            }]; this.rgba = []; if (!(this instanceof e)) return new e(p); this.init(p)
            } e.parse = function (p) { return new e(p) }; e.prototype.init = function (p) {
                var G, m; if ((this.input = p = e.names[p && p.toLowerCase ? p.toLowerCase() : ""] || p) && p.stops) this.stops = p.stops.map(function (A) { return new e(A[1]) }); else {
                    if (p && p.charAt && "#" === p.charAt()) {
                        var H = p.length; p = parseInt(p.substr(1), 16); 7 === H ? G = [(p & 16711680) >> 16, (p & 65280) >> 8, p & 255, 1] : 4 === H && (G = [(p & 3840) >> 4 | (p & 3840) >> 8, (p & 240) >> 4 |
                            p & 240, (p & 15) << 4 | p & 15, 1])
                    } if (!G) for (m = this.parsers.length; m-- && !G;) { var M = this.parsers[m]; (H = M.regex.exec(p)) && (G = M.parse(H)) }
                } this.rgba = G || []
            }; e.prototype.get = function (e) { var p = this.input, m = this.rgba; if ("undefined" !== typeof this.stops) { var H = B(p); H.stops = [].concat(H.stops); this.stops.forEach(function (m, A) { H.stops[A] = [H.stops[A][0], m.get(e)] }) } else H = m && q(m[0]) ? "rgb" === e || !e && 1 === m[3] ? "rgb(" + m[0] + "," + m[1] + "," + m[2] + ")" : "a" === e ? m[3] : "rgba(" + m.join(",") + ")" : p; return H }; e.prototype.brighten = function (e) {
                var p,
                m = this.rgba; if (this.stops) this.stops.forEach(function (m) { m.brighten(e) }); else if (q(e) && 0 !== e) for (p = 0; 3 > p; p++)m[p] += D(255 * e), 0 > m[p] && (m[p] = 0), 255 < m[p] && (m[p] = 255); return this
            }; e.prototype.setOpacity = function (e) { this.rgba[3] = e; return this }; e.prototype.tweenTo = function (e, p) {
                var m = this.rgba, H = e.rgba; H.length && m && m.length ? (e = 1 !== H[3] || 1 !== m[3], p = (e ? "rgba(" : "rgb(") + Math.round(H[0] + (m[0] - H[0]) * (1 - p)) + "," + Math.round(H[1] + (m[1] - H[1]) * (1 - p)) + "," + Math.round(H[2] + (m[2] - H[2]) * (1 - p)) + (e ? "," + (H[3] + (m[3] - H[3]) * (1 -
                    p)) : "") + ")") : p = e.input || "none"; return p
            }; e.names = { white: "#ffffff", black: "#000000" }; return e
        }(); p.Color = e; p.color = e.parse; return p.Color
    }); O(q, "parts/SVGElement.js", [q["parts/Color.js"], q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e, q) {
        var B = e.deg2rad, D = e.doc, z = e.hasTouch, J = e.isFirefox, G = e.noop, m = e.svg, H = e.SVG_NS, M = e.win, A = q.animate, K = q.animObject, w = q.attr, r = q.createElement, u = q.css, C = q.defined, h = q.erase, f = q.extend, d = q.fireEvent, t = q.isArray, l = q.isFunction, c = q.isNumber, a = q.isString, x = q.merge,
        v = q.objectEach, E = q.pick, F = q.pInt, k = q.stop, y = q.uniqueKey; ""; q = function () {
            function I() { this.height = this.element = void 0; this.opacity = 1; this.renderer = void 0; this.SVG_NS = H; this.symbolCustomAttribs = "x y width height r start end innerR anchorX anchorY rounded".split(" "); this.width = void 0 } I.prototype._defaultGetter = function (a) { a = E(this[a + "Value"], this[a], this.element ? this.element.getAttribute(a) : null, 0); /^[\-0-9\.]+$/.test(a) && (a = parseFloat(a)); return a }; I.prototype._defaultSetter = function (a, g, b) {
                b.setAttribute(g,
                    a)
            }; I.prototype.add = function (a) { var g = this.renderer, b = this.element; a && (this.parentGroup = a); this.parentInverted = a && a.inverted; "undefined" !== typeof this.textStr && "text" === this.element.nodeName && g.buildText(this); this.added = !0; if (!a || a.handleZ || this.zIndex) var n = this.zIndexSetter(); n || (a ? a.element : g.box).appendChild(b); if (this.onAdd) this.onAdd(); return this }; I.prototype.addClass = function (a, g) {
                var b = g ? "" : this.attr("class") || ""; a = (a || "").split(/ /g).reduce(function (n, a) { -1 === b.indexOf(a) && n.push(a); return n },
                    b ? [b] : []).join(" "); a !== b && this.attr("class", a); return this
            }; I.prototype.afterSetters = function () { this.doTransform && (this.updateTransform(), this.doTransform = !1) }; I.prototype.align = function (c, g, b) {
                var n, L = {}; var k = this.renderer; var d = k.alignedObjects; var l, f; if (c) { if (this.alignOptions = c, this.alignByTranslate = g, !b || a(b)) this.alignTo = n = b || "renderer", h(d, this), d.push(this), b = void 0 } else c = this.alignOptions, g = this.alignByTranslate, n = this.alignTo; b = E(b, k[n], k); n = c.align; k = c.verticalAlign; d = (b.x || 0) + (c.x ||
                    0); var v = (b.y || 0) + (c.y || 0); "right" === n ? l = 1 : "center" === n && (l = 2); l && (d += (b.width - (c.width || 0)) / l); L[g ? "translateX" : "x"] = Math.round(d); "bottom" === k ? f = 1 : "middle" === k && (f = 2); f && (v += (b.height - (c.height || 0)) / f); L[g ? "translateY" : "y"] = Math.round(v); this[this.placed ? "animate" : "attr"](L); this.placed = !0; this.alignAttr = L; return this
            }; I.prototype.alignSetter = function (a) { var g = { left: "start", center: "middle", right: "end" }; g[a] && (this.alignValue = a, this.element.setAttribute("text-anchor", g[a])) }; I.prototype.animate =
                function (a, g, b) { var n = K(E(g, this.renderer.globalAnimation, !0)); E(D.hidden, D.msHidden, D.webkitHidden, !1) && (n.duration = 0); 0 !== n.duration ? (b && (n.complete = b), A(this, a, n)) : (this.attr(a, void 0, b), v(a, function (b, a) { n.step && n.step.call(this, b, { prop: a, pos: 1 }) }, this)); return this }; I.prototype.applyTextOutline = function (a) {
                    var g = this.element, b; -1 !== a.indexOf("contrast") && (a = a.replace(/contrast/g, this.renderer.getContrast(g.style.fill))); a = a.split(" "); var n = a[a.length - 1]; if ((b = a[0]) && "none" !== b && e.svg) {
                    this.fakeTS =
                        !0; a = [].slice.call(g.getElementsByTagName("tspan")); this.ySetter = this.xSetter; b = b.replace(/(^[\d\.]+)(.*?)$/g, function (b, n, a) { return 2 * n + a }); this.removeTextOutline(a); var L = g.textContent ? /^[\u0591-\u065F\u066A-\u07FF\uFB1D-\uFDFD\uFE70-\uFEFC]/.test(g.textContent) : !1; var c = g.firstChild; a.forEach(function (a, k) {
                        0 === k && (a.setAttribute("x", g.getAttribute("x")), k = g.getAttribute("y"), a.setAttribute("y", k || 0), null === k && g.setAttribute("y", 0)); k = a.cloneNode(!0); w(L && !J ? a : k, {
                            "class": "highcharts-text-outline",
                            fill: n, stroke: n, "stroke-width": b, "stroke-linejoin": "round"
                        }); g.insertBefore(k, c)
                        }); L && J && a[0] && (a = a[0].cloneNode(!0), a.textContent = " ", g.insertBefore(a, c))
                    }
                }; I.prototype.attr = function (a, g, b, n) {
                    var L = this.element, c, d = this, l, f, x = this.symbolCustomAttribs; if ("string" === typeof a && "undefined" !== typeof g) { var t = a; a = {}; a[t] = g } "string" === typeof a ? d = (this[a + "Getter"] || this._defaultGetter).call(this, a, L) : (v(a, function (b, g) {
                        l = !1; n || k(this, g); this.symbolName && -1 !== x.indexOf(g) && (c || (this.symbolAttr(a), c = !0), l =
                            !0); !this.rotation || "x" !== g && "y" !== g || (this.doTransform = !0); l || (f = this[g + "Setter"] || this._defaultSetter, f.call(this, b, g, L), !this.styledMode && this.shadows && /^(width|height|visibility|x|y|d|transform|cx|cy|r)$/.test(g) && this.updateShadows(g, b, f))
                    }, this), this.afterSetters()); b && b.call(this); return d
                }; I.prototype.clip = function (a) { return this.attr("clip-path", a ? "url(" + this.renderer.url + "#" + a.id + ")" : "none") }; I.prototype.crisp = function (a, g) {
                    g = g || a.strokeWidth || 0; var b = Math.round(g) % 2 / 2; a.x = Math.floor(a.x ||
                        this.x || 0) + b; a.y = Math.floor(a.y || this.y || 0) + b; a.width = Math.floor((a.width || this.width || 0) - 2 * b); a.height = Math.floor((a.height || this.height || 0) - 2 * b); C(a.strokeWidth) && (a.strokeWidth = g); return a
                }; I.prototype.complexColor = function (a, g, b) {
                    var n = this.renderer, L, c, k, l, f, h, F, P, E, I, u = [], r; d(this.renderer, "complexColor", { args: arguments }, function () {
                        a.radialGradient ? c = "radialGradient" : a.linearGradient && (c = "linearGradient"); if (c) {
                            k = a[c]; f = n.gradients; h = a.stops; E = b.radialReference; t(k) && (a[c] = k = {
                                x1: k[0], y1: k[1],
                                x2: k[2], y2: k[3], gradientUnits: "userSpaceOnUse"
                            }); "radialGradient" === c && E && !C(k.gradientUnits) && (l = k, k = x(k, n.getRadialAttr(E, l), { gradientUnits: "userSpaceOnUse" })); v(k, function (b, n) { "id" !== n && u.push(n, b) }); v(h, function (b) { u.push(b) }); u = u.join(","); if (f[u]) I = f[u].attr("id"); else {
                            k.id = I = y(); var N = f[u] = n.createElement(c).attr(k).add(n.defs); N.radAttr = l; N.stops = []; h.forEach(function (b) {
                                0 === b[1].indexOf("rgba") ? (L = p.parse(b[1]), F = L.get("rgb"), P = L.get("a")) : (F = b[1], P = 1); b = n.createElement("stop").attr({
                                    offset: b[0],
                                    "stop-color": F, "stop-opacity": P
                                }).add(N); N.stops.push(b)
                            })
                            } r = "url(" + n.url + "#" + I + ")"; b.setAttribute(g, r); b.gradient = u; a.toString = function () { return r }
                        }
                    })
                }; I.prototype.css = function (a) {
                    var g = this.styles, b = {}, n = this.element, c = "", k = !g, d = ["textOutline", "textOverflow", "width"]; a && a.color && (a.fill = a.color); g && v(a, function (n, a) { g && g[a] !== n && (b[a] = n, k = !0) }); if (k) {
                        g && (a = f(g, b)); if (a) if (null === a.width || "auto" === a.width) delete this.textWidth; else if ("text" === n.nodeName.toLowerCase() && a.width) var l = this.textWidth =
                            F(a.width); this.styles = a; l && !m && this.renderer.forExport && delete a.width; if (n.namespaceURI === this.SVG_NS) { var x = function (b, n) { return "-" + n.toLowerCase() }; v(a, function (b, n) { -1 === d.indexOf(n) && (c += n.replace(/([A-Z])/g, x) + ":" + b + ";") }); c && w(n, "style", c) } else u(n, a); this.added && ("text" === this.element.nodeName && this.renderer.buildText(this), a && a.textOutline && this.applyTextOutline(a.textOutline))
                    } return this
                }; I.prototype.dashstyleSetter = function (a) {
                    var g = this["stroke-width"]; "inherit" === g && (g = 1); if (a = a && a.toLowerCase()) {
                        var b =
                            a.replace("shortdashdotdot", "3,1,1,1,1,1,").replace("shortdashdot", "3,1,1,1").replace("shortdot", "1,1,").replace("shortdash", "3,1,").replace("longdash", "8,3,").replace(/dot/g, "1,3,").replace("dash", "4,3,").replace(/,$/, "").split(","); for (a = b.length; a--;)b[a] = "" + F(b[a]) * E(g, NaN); a = b.join(",").replace(/NaN/g, "none"); this.element.setAttribute("stroke-dasharray", a)
                    }
                }; I.prototype.destroy = function () {
                    var a = this, g = a.element || {}, b = a.renderer, n = b.isSVG && "SPAN" === g.nodeName && a.parentGroup || void 0, c = g.ownerSVGElement;
                    g.onclick = g.onmouseout = g.onmouseover = g.onmousemove = g.point = null; k(a); if (a.clipPath && c) { var N = a.clipPath;[].forEach.call(c.querySelectorAll("[clip-path],[CLIP-PATH]"), function (b) { -1 < b.getAttribute("clip-path").indexOf(N.element.id) && b.removeAttribute("clip-path") }); a.clipPath = N.destroy() } if (a.stops) { for (c = 0; c < a.stops.length; c++)a.stops[c].destroy(); a.stops.length = 0; a.stops = void 0 } a.safeRemoveChild(g); for (b.styledMode || a.destroyShadows(); n && n.div && 0 === n.div.childNodes.length;)g = n.parentGroup, a.safeRemoveChild(n.div),
                        delete n.div, n = g; a.alignTo && h(b.alignedObjects, a); v(a, function (b, n) { a[n] && a[n].parentGroup === a && a[n].destroy && a[n].destroy(); delete a[n] })
                }; I.prototype.destroyShadows = function () { (this.shadows || []).forEach(function (a) { this.safeRemoveChild(a) }, this); this.shadows = void 0 }; I.prototype.destroyTextPath = function (a, g) {
                    var b = a.getElementsByTagName("text")[0]; if (b) {
                        if (b.removeAttribute("dx"), b.removeAttribute("dy"), g.element.setAttribute("id", ""), this.textPathWrapper && b.getElementsByTagName("textPath").length) {
                            for (a =
                                this.textPathWrapper.element.childNodes; a.length;)b.appendChild(a[0]); b.removeChild(this.textPathWrapper.element)
                        }
                    } else if (a.getAttribute("dx") || a.getAttribute("dy")) a.removeAttribute("dx"), a.removeAttribute("dy"); this.textPathWrapper && (this.textPathWrapper = this.textPathWrapper.destroy())
                }; I.prototype.dSetter = function (a, g, b) {
                t(a) && ("string" === typeof a[0] && (a = this.renderer.pathToSegments(a)), this.pathArray = a, a = a.reduce(function (b, a, g) { return a && a.join ? (g ? b + " " : "") + a.join(" ") : (a || "").toString() }, ""));
                    /(NaN| {2}|^$)/.test(a) && (a = "M 0 0"); this[g] !== a && (b.setAttribute(g, a), this[g] = a)
                }; I.prototype.fadeOut = function (a) { var g = this; g.animate({ opacity: 0 }, { duration: E(a, 150), complete: function () { g.attr({ y: -9999 }).hide() } }) }; I.prototype.fillSetter = function (a, g, b) { "string" === typeof a ? b.setAttribute(g, a) : a && this.complexColor(a, g, b) }; I.prototype.getBBox = function (a, g) {
                    var b, n = this.renderer, c = this.element, k = this.styles, d = this.textStr, v = n.cache, x = n.cacheKeys, t = c.namespaceURI === this.SVG_NS; g = E(g, this.rotation, 0);
                    var y = n.styledMode ? c && I.prototype.getStyle.call(c, "font-size") : k && k.fontSize; if (C(d)) { var h = d.toString(); -1 === h.indexOf("<") && (h = h.replace(/[0-9]/g, "0")); h += ["", g, y, this.textWidth, k && k.textOverflow, k && k.fontWeight].join() } h && !a && (b = v[h]); if (!b) {
                        if (t || n.forExport) {
                            try { var F = this.fakeTS && function (b) { [].forEach.call(c.querySelectorAll(".highcharts-text-outline"), function (n) { n.style.display = b }) }; l(F) && F("none"); b = c.getBBox ? f({}, c.getBBox()) : { width: c.offsetWidth, height: c.offsetHeight }; l(F) && F("") } catch (T) { "" } if (!b ||
                                0 > b.width) b = { width: 0, height: 0 }
                        } else b = this.htmlGetBBox(); n.isSVG && (a = b.width, n = b.height, t && (b.height = n = { "11px,17": 14, "13px,20": 16 }[k && k.fontSize + "," + Math.round(n)] || n), g && (k = g * B, b.width = Math.abs(n * Math.sin(k)) + Math.abs(a * Math.cos(k)), b.height = Math.abs(n * Math.cos(k)) + Math.abs(a * Math.sin(k)))); if (h && 0 < b.height) { for (; 250 < x.length;)delete v[x.shift()]; v[h] || x.push(h); v[h] = b }
                    } return b
                }; I.prototype.getStyle = function (a) { return M.getComputedStyle(this.element || this, "").getPropertyValue(a) }; I.prototype.hasClass =
                    function (a) { return -1 !== ("" + this.attr("class")).split(" ").indexOf(a) }; I.prototype.hide = function (a) { a ? this.attr({ y: -9999 }) : this.attr({ visibility: "hidden" }); return this }; I.prototype.htmlGetBBox = function () { return { height: 0, width: 0, x: 0, y: 0 } }; I.prototype.init = function (a, g) { this.element = "span" === g ? r(g) : D.createElementNS(this.SVG_NS, g); this.renderer = a; d(this, "afterInit") }; I.prototype.invert = function (a) { this.inverted = a; this.updateTransform(); return this }; I.prototype.on = function (a, g) {
                        var b, n, c = this.element,
                        k; z && "click" === a ? (c.ontouchstart = function (a) { b = a.touches[0].clientX; n = a.touches[0].clientY }, c.ontouchend = function (a) { b && 4 <= Math.sqrt(Math.pow(b - a.changedTouches[0].clientX, 2) + Math.pow(n - a.changedTouches[0].clientY, 2)) || g.call(c, a); k = !0; a.preventDefault() }, c.onclick = function (b) { k || g.call(c, b) }) : c["on" + a] = g; return this
                    }; I.prototype.opacitySetter = function (a, g, b) { this[g] = a; b.setAttribute(g, a) }; I.prototype.removeClass = function (c) {
                        return this.attr("class", ("" + this.attr("class")).replace(a(c) ? new RegExp("(^| )" +
                            c + "( |$)") : c, " ").replace(/ +/g, " ").trim())
                    }; I.prototype.removeTextOutline = function (a) { for (var g = a.length, b; g--;)b = a[g], "highcharts-text-outline" === b.getAttribute("class") && h(a, this.element.removeChild(b)) }; I.prototype.safeRemoveChild = function (a) { var g = a.parentNode; g && g.removeChild(a) }; I.prototype.setRadialReference = function (a) {
                        var g = this.element.gradient && this.renderer.gradients[this.element.gradient]; this.element.radialReference = a; g && g.radAttr && g.animate(this.renderer.getRadialAttr(a, g.radAttr));
                        return this
                    }; I.prototype.setTextPath = function (a, g) {
                        var b = this.element, n = { textAnchor: "text-anchor" }, k = !1, d = this.textPathWrapper, l = !d; g = x(!0, { enabled: !0, attributes: { dy: -5, startOffset: "50%", textAnchor: "middle" } }, g); var f = g.attributes; if (a && g && g.enabled) {
                            d && null === d.element.parentNode ? (l = !0, d = d.destroy()) : d && this.removeTextOutline.call(d.parentGroup, [].slice.call(b.getElementsByTagName("tspan"))); this.options && this.options.padding && (f.dx = -this.options.padding); d || (this.textPathWrapper = d = this.renderer.createElement("textPath"),
                                k = !0); var t = d.element; (g = a.element.getAttribute("id")) || a.element.setAttribute("id", g = y()); if (l) for (a = b.getElementsByTagName("tspan"); a.length;)a[0].setAttribute("y", 0), c(f.dx) && a[0].setAttribute("x", -f.dx), t.appendChild(a[0]); k && d && d.add({ element: this.text ? this.text.element : b }); t.setAttributeNS("http://www.w3.org/1999/xlink", "href", this.renderer.url + "#" + g); C(f.dy) && (t.parentNode.setAttribute("dy", f.dy), delete f.dy); C(f.dx) && (t.parentNode.setAttribute("dx", f.dx), delete f.dx); v(f, function (b, a) {
                                    t.setAttribute(n[a] ||
                                        a, b)
                                }); b.removeAttribute("transform"); this.removeTextOutline.call(d, [].slice.call(b.getElementsByTagName("tspan"))); this.text && !this.renderer.styledMode && this.attr({ fill: "none", "stroke-width": 0 }); this.applyTextOutline = this.updateTransform = G
                        } else d && (delete this.updateTransform, delete this.applyTextOutline, this.destroyTextPath(b, a), this.updateTransform(), this.options && this.options.rotation && this.applyTextOutline(this.options.style.textOutline)); return this
                    }; I.prototype.shadow = function (a, g, b) {
                        var n =
                            [], c = this.element, k = !1, d = this.oldShadowOptions; var l = { color: "#000000", offsetX: 1, offsetY: 1, opacity: .15, width: 3 }; var x; !0 === a ? x = l : "object" === typeof a && (x = f(l, a)); x && (x && d && v(x, function (b, n) { b !== d[n] && (k = !0) }), k && this.destroyShadows(), this.oldShadowOptions = x); if (!x) this.destroyShadows(); else if (!this.shadows) {
                                var t = x.opacity / x.width; var y = this.parentInverted ? "translate(-1,-1)" : "translate(" + x.offsetX + ", " + x.offsetY + ")"; for (l = 1; l <= x.width; l++) {
                                    var h = c.cloneNode(!1); var F = 2 * x.width + 1 - 2 * l; w(h, {
                                        stroke: a.color ||
                                            "#000000", "stroke-opacity": t * l, "stroke-width": F, transform: y, fill: "none"
                                    }); h.setAttribute("class", (h.getAttribute("class") || "") + " highcharts-shadow"); b && (w(h, "height", Math.max(w(h, "height") - F, 0)), h.cutHeight = F); g ? g.element.appendChild(h) : c.parentNode && c.parentNode.insertBefore(h, c); n.push(h)
                                } this.shadows = n
                            } return this
                    }; I.prototype.show = function (a) { return this.attr({ visibility: a ? "inherit" : "visible" }) }; I.prototype.strokeSetter = function (a, g, b) {
                    this[g] = a; this.stroke && this["stroke-width"] ? (I.prototype.fillSetter.call(this,
                        this.stroke, "stroke", b), b.setAttribute("stroke-width", this["stroke-width"]), this.hasStroke = !0) : "stroke-width" === g && 0 === a && this.hasStroke ? (b.removeAttribute("stroke"), this.hasStroke = !1) : this.renderer.styledMode && this["stroke-width"] && (b.setAttribute("stroke-width", this["stroke-width"]), this.hasStroke = !0)
                    }; I.prototype.strokeWidth = function () {
                        if (!this.renderer.styledMode) return this["stroke-width"] || 0; var a = this.getStyle("stroke-width"), g = 0; if (a.indexOf("px") === a.length - 2) g = F(a); else if ("" !== a) {
                            var b =
                                D.createElementNS(H, "rect"); w(b, { width: a, "stroke-width": 0 }); this.element.parentNode.appendChild(b); g = b.getBBox().width; b.parentNode.removeChild(b)
                        } return g
                    }; I.prototype.symbolAttr = function (a) { var g = this; "x y r start end width height innerR anchorX anchorY clockwise".split(" ").forEach(function (b) { g[b] = E(a[b], g[b]) }); g.attr({ d: g.renderer.symbols[g.symbolName](g.x, g.y, g.width, g.height, g) }) }; I.prototype.textSetter = function (a) { a !== this.textStr && (delete this.textPxLength, this.textStr = a, this.added && this.renderer.buildText(this)) };
            I.prototype.titleSetter = function (a) { var g = this.element.getElementsByTagName("title")[0]; g || (g = D.createElementNS(this.SVG_NS, "title"), this.element.appendChild(g)); g.firstChild && g.removeChild(g.firstChild); g.appendChild(D.createTextNode(String(E(a, "")).replace(/<[^>]*>/g, "").replace(/&lt;/g, "<").replace(/&gt;/g, ">"))) }; I.prototype.toFront = function () { var a = this.element; a.parentNode.appendChild(a); return this }; I.prototype.translate = function (a, g) { return this.attr({ translateX: a, translateY: g }) }; I.prototype.updateShadows =
                function (a, g, b) { var n = this.shadows; if (n) for (var c = n.length; c--;)b.call(n[c], "height" === a ? Math.max(g - (n[c].cutHeight || 0), 0) : "d" === a ? this.d : g, a, n[c]) }; I.prototype.updateTransform = function () {
                    var a = this.translateX || 0, g = this.translateY || 0, b = this.scaleX, n = this.scaleY, c = this.inverted, k = this.rotation, d = this.matrix, l = this.element; c && (a += this.width, g += this.height); a = ["translate(" + a + "," + g + ")"]; C(d) && a.push("matrix(" + d.join(",") + ")"); c ? a.push("rotate(90) scale(-1,1)") : k && a.push("rotate(" + k + " " + E(this.rotationOriginX,
                        l.getAttribute("x"), 0) + " " + E(this.rotationOriginY, l.getAttribute("y") || 0) + ")"); (C(b) || C(n)) && a.push("scale(" + E(b, 1) + " " + E(n, 1) + ")"); a.length && l.setAttribute("transform", a.join(" "))
                }; I.prototype.visibilitySetter = function (a, g, b) { "inherit" === a ? b.removeAttribute(g) : this[g] !== a && b.setAttribute(g, a); this[g] = a }; I.prototype.xGetter = function (a) { "circle" === this.element.nodeName && ("x" === a ? a = "cx" : "y" === a && (a = "cy")); return this._defaultGetter(a) }; I.prototype.zIndexSetter = function (a, g) {
                    var b = this.renderer, n = this.parentGroup,
                    c = (n || b).element || b.box, k = this.element, d = !1; b = c === b.box; var l = this.added; var f; C(a) ? (k.setAttribute("data-z-index", a), a = +a, this[g] === a && (l = !1)) : C(this[g]) && k.removeAttribute("data-z-index"); this[g] = a; if (l) {
                    (a = this.zIndex) && n && (n.handleZ = !0); g = c.childNodes; for (f = g.length - 1; 0 <= f && !d; f--) { n = g[f]; l = n.getAttribute("data-z-index"); var v = !C(l); if (n !== k) if (0 > a && v && !b && !f) c.insertBefore(k, g[f]), d = !0; else if (F(l) <= a || v && (!C(a) || 0 <= a)) c.insertBefore(k, g[f + 1] || null), d = !0 } d || (c.insertBefore(k, g[b ? 3 : 0] || null),
                        d = !0)
                    } return d
                }; return I
        }(); q.prototype["stroke-widthSetter"] = q.prototype.strokeSetter; q.prototype.yGetter = q.prototype.xGetter; q.prototype.matrixSetter = q.prototype.rotationOriginXSetter = q.prototype.rotationOriginYSetter = q.prototype.rotationSetter = q.prototype.scaleXSetter = q.prototype.scaleYSetter = q.prototype.translateXSetter = q.prototype.translateYSetter = q.prototype.verticalAlignSetter = function (a, c) { this[c] = a; this.doTransform = !0 }; e.SVGElement = q; return e.SVGElement
    }); O(q, "parts/SVGLabel.js", [q["parts/SVGElement.js"],
    q["parts/Utilities.js"]], function (p, e) {
        var q = this && this.__extends || function () { var e = function (m, M) { e = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (e, m) { e.__proto__ = m } || function (e, m) { for (var w in m) m.hasOwnProperty(w) && (e[w] = m[w]) }; return e(m, M) }; return function (m, M) { function A() { this.constructor = m } e(m, M); m.prototype = null === M ? Object.create(M) : (A.prototype = M.prototype, new A) } }(), B = e.defined, D = e.extend, z = e.isNumber, J = e.merge, G = e.removeEvent; return function (e) {
            function m(M, A, p, w, r, u,
                C, h, f, d) { var t = e.call(this) || this; t.init(M, "g"); t.textStr = A; t.x = p; t.y = w; t.anchorX = u; t.anchorY = C; t.baseline = f; t.className = d; "button" !== d && t.addClass("highcharts-label"); d && t.addClass("highcharts-" + d); t.text = M.text("", 0, 0, h).attr({ zIndex: 1 }); if ("string" === typeof r) { var l = /^url\((.*?)\)$/.test(r); if (t.renderer.symbols[r] || l) t.symbolKey = r } t.bBox = m.emptyBBox; t.padding = 3; t.paddingLeft = 0; t.baselineOffset = 0; t.needsBox = M.styledMode || l; t.deferredAttr = {}; t.alignFactor = 0; return t } q(m, e); m.prototype.alignSetter =
                    function (m) { m = { left: 0, center: .5, right: 1 }[m]; m !== this.alignFactor && (this.alignFactor = m, this.bBox && z(this.xSetting) && this.attr({ x: this.xSetting })) }; m.prototype.anchorXSetter = function (m, e) { this.anchorX = m; this.boxAttr(e, Math.round(m) - this.getCrispAdjust() - this.xSetting) }; m.prototype.anchorYSetter = function (m, e) { this.anchorY = m; this.boxAttr(e, m - this.ySetting) }; m.prototype.boxAttr = function (m, e) { this.box ? this.box.attr(m, e) : this.deferredAttr[m] = e }; m.prototype.css = function (e) {
                        if (e) {
                            var A = {}; e = J(e); m.textProps.forEach(function (w) {
                            "undefined" !==
                                typeof e[w] && (A[w] = e[w], delete e[w])
                            }); this.text.css(A); var M = "fontSize" in A || "fontWeight" in A; if ("width" in A || M) this.updateBoxSize(), M && this.updateTextPadding()
                        } return p.prototype.css.call(this, e)
                    }; m.prototype.destroy = function () { G(this.element, "mouseenter"); G(this.element, "mouseleave"); this.text && this.text.destroy(); this.box && (this.box = this.box.destroy()); p.prototype.destroy.call(this) }; m.prototype.fillSetter = function (m, e) { m && (this.needsBox = !0); this.fill = m; this.boxAttr(e, m) }; m.prototype.getBBox =
                        function () { var m = this.bBox, e = this.padding; return { width: m.width + 2 * e, height: m.height + 2 * e, x: m.x - e, y: m.y - e } }; m.prototype.getCrispAdjust = function () { return this.renderer.styledMode && this.box ? this.box.strokeWidth() % 2 / 2 : (this["stroke-width"] ? parseInt(this["stroke-width"], 10) : 0) % 2 / 2 }; m.prototype.heightSetter = function (m) { this.heightSetting = m }; m.prototype.on = function (m, e) {
                            var A = this, w = A.text, r = w && "SPAN" === w.element.tagName ? w : void 0; if (r) {
                                var u = function (u) {
                                ("mouseenter" === m || "mouseleave" === m) && u.relatedTarget instanceof
                                    Element && (A.element.contains(u.relatedTarget) || r.element.contains(u.relatedTarget)) || e.call(A.element, u)
                                }; r.on(m, u)
                            } p.prototype.on.call(A, m, u || e); return A
                        }; m.prototype.onAdd = function () { var m = this.textStr; this.text.add(this); this.attr({ text: B(m) ? m : "", x: this.x, y: this.y }); this.box && B(this.anchorX) && this.attr({ anchorX: this.anchorX, anchorY: this.anchorY }) }; m.prototype.paddingSetter = function (m) { B(m) && m !== this.padding && (this.padding = m, this.updateTextPadding()) }; m.prototype.paddingLeftSetter = function (m) {
                        B(m) &&
                            m !== this.paddingLeft && (this.paddingLeft = m, this.updateTextPadding())
                        }; m.prototype.rSetter = function (m, e) { this.boxAttr(e, m) }; m.prototype.shadow = function (m) { m && !this.renderer.styledMode && (this.updateBoxSize(), this.box && this.box.shadow(m)); return this }; m.prototype.strokeSetter = function (m, e) { this.stroke = m; this.boxAttr(e, m) }; m.prototype["stroke-widthSetter"] = function (m, e) { m && (this.needsBox = !0); this["stroke-width"] = m; this.boxAttr(e, m) }; m.prototype["text-alignSetter"] = function (m) { this.textAlign = m }; m.prototype.textSetter =
                            function (m) { "undefined" !== typeof m && this.text.attr({ text: m }); this.updateBoxSize(); this.updateTextPadding() }; m.prototype.updateBoxSize = function () {
                                var e = this.text.element.style, A = {}, p = this.padding, w = this.paddingLeft, r = z(this.widthSetting) && z(this.heightSetting) && !this.textAlign || !B(this.text.textStr) ? m.emptyBBox : this.text.getBBox(); this.width = (this.widthSetting || r.width || 0) + 2 * p + w; this.height = (this.heightSetting || r.height || 0) + 2 * p; this.baselineOffset = p + Math.min(this.renderer.fontMetrics(e && e.fontSize,
                                    this.text).b, r.height || Infinity); this.needsBox && (this.box || (e = this.box = this.symbolKey ? this.renderer.symbol(this.symbolKey) : this.renderer.rect(), e.addClass(("button" === this.className ? "" : "highcharts-label-box") + (this.className ? " highcharts-" + this.className + "-box" : "")), e.add(this), e = this.getCrispAdjust(), A.x = e, A.y = (this.baseline ? -this.baselineOffset : 0) + e), A.width = Math.round(this.width), A.height = Math.round(this.height), this.box.attr(D(A, this.deferredAttr)), this.deferredAttr = {}); this.bBox = r
                            }; m.prototype.updateTextPadding =
                                function () { var m = this.text, e = this.baseline ? 0 : this.baselineOffset, p = this.paddingLeft + this.padding; B(this.widthSetting) && this.bBox && ("center" === this.textAlign || "right" === this.textAlign) && (p += { center: .5, right: 1 }[this.textAlign] * (this.widthSetting - this.bBox.width)); if (p !== m.x || e !== m.y) m.attr("x", p), m.hasBoxWidthChanged && (this.bBox = m.getBBox(!0), this.updateBoxSize()), "undefined" !== typeof e && m.attr("y", e); m.x = p; m.y = e }; m.prototype.widthSetter = function (m) { this.widthSetting = z(m) ? m : void 0 }; m.prototype.xSetter =
                                    function (m) { this.x = m; this.alignFactor && (m -= this.alignFactor * ((this.widthSetting || this.bBox.width) + 2 * this.padding), this["forceAnimate:x"] = !0); this.xSetting = Math.round(m); this.attr("translateX", this.xSetting) }; m.prototype.ySetter = function (m) { this.ySetting = this.y = Math.round(m); this.attr("translateY", this.ySetting) }; m.emptyBBox = { width: 0, height: 0, x: 0, y: 0 }; m.textProps = "color cursor direction fontFamily fontSize fontStyle fontWeight lineHeight textAlign textDecoration textOutline textOverflow width".split(" ");
            return m
        }(p)
    }); O(q, "parts/SVGRenderer.js", [q["parts/Color.js"], q["parts/Globals.js"], q["parts/SVGElement.js"], q["parts/SVGLabel.js"], q["parts/Utilities.js"]], function (p, e, q, B, D) {
        var z = D.addEvent, J = D.attr, G = D.createElement, m = D.css, H = D.defined, M = D.destroyObjectProperties, A = D.extend, K = D.isArray, w = D.isNumber, r = D.isObject, u = D.isString, C = D.merge, h = D.objectEach, f = D.pick, d = D.pInt, t = D.splat, l = D.uniqueKey, c = e.charts, a = e.deg2rad, x = e.doc, v = e.isFirefox, E = e.isMS, F = e.isWebKit; D = e.noop; var k = e.svg, y = e.SVG_NS, I = e.symbolSizes,
            P = e.win, g = function () {
                function b(b, a, g, c, k, d, l) { this.width = this.url = this.style = this.isSVG = this.imgCount = this.height = this.gradients = this.globalAnimation = this.defs = this.chartIndex = this.cacheKeys = this.cache = this.boxWrapper = this.box = this.alignedObjects = void 0; this.init(b, a, g, c, k, d, l) } b.prototype.init = function (b, a, g, c, k, d, l) {
                    var n = this.createElement("svg").attr({ version: "1.1", "class": "highcharts-root" }); l || n.css(this.getStyle(c)); c = n.element; b.appendChild(c); J(b, "dir", "ltr"); -1 === b.innerHTML.indexOf("xmlns") &&
                        J(c, "xmlns", this.SVG_NS); this.isSVG = !0; this.box = c; this.boxWrapper = n; this.alignedObjects = []; this.url = (v || F) && x.getElementsByTagName("base").length ? P.location.href.split("#")[0].replace(/<[^>]*>/g, "").replace(/([\('\)])/g, "\\$1").replace(/ /g, "%20") : ""; this.createElement("desc").add().element.appendChild(x.createTextNode("Created with Highcharts 8.1.2")); this.defs = this.createElement("defs").add(); this.allowHTML = d; this.forExport = k; this.styledMode = l; this.gradients = {}; this.cache = {}; this.cacheKeys = []; this.imgCount =
                            0; this.setSize(a, g, !1); var L; v && b.getBoundingClientRect && (a = function () { m(b, { left: 0, top: 0 }); L = b.getBoundingClientRect(); m(b, { left: Math.ceil(L.left) - L.left + "px", top: Math.ceil(L.top) - L.top + "px" }) }, a(), this.unSubPixelFix = z(P, "resize", a))
                }; b.prototype.definition = function (b) {
                    function a(b, g) {
                        var c; t(b).forEach(function (b) {
                            var k = n.createElement(b.tagName), L = {}; h(b, function (b, a) { "tagName" !== a && "children" !== a && "textContent" !== a && (L[a] = b) }); k.attr(L); k.add(g || n.defs); b.textContent && k.element.appendChild(x.createTextNode(b.textContent));
                            a(b.children || [], k); c = k
                        }); return c
                    } var n = this; return a(b)
                }; b.prototype.getStyle = function (b) { return this.style = A({ fontFamily: '"Lucida Grande", "Lucida Sans Unicode", Arial, Helvetica, sans-serif', fontSize: "12px" }, b) }; b.prototype.setStyle = function (b) { this.boxWrapper.css(this.getStyle(b)) }; b.prototype.isHidden = function () { return !this.boxWrapper.getBBox().width }; b.prototype.destroy = function () {
                    var b = this.defs; this.box = null; this.boxWrapper = this.boxWrapper.destroy(); M(this.gradients || {}); this.gradients = null;
                    b && (this.defs = b.destroy()); this.unSubPixelFix && this.unSubPixelFix(); return this.alignedObjects = null
                }; b.prototype.createElement = function (b) { var a = new this.Element; a.init(this, b); return a }; b.prototype.getRadialAttr = function (b, a) { return { cx: b[0] - b[2] / 2 + a.cx * b[2], cy: b[1] - b[2] / 2 + a.cy * b[2], r: a.r * b[2] } }; b.prototype.truncate = function (b, a, g, c, k, d, l) {
                    var n = this, L = b.rotation, N, f = c ? 1 : 0, v = (g || c).length, t = v, h = [], y = function (b) { a.firstChild && a.removeChild(a.firstChild); b && a.appendChild(x.createTextNode(b)) }, F = function (L,
                        d) { d = d || L; if ("undefined" === typeof h[d]) if (a.getSubStringLength) try { h[d] = k + a.getSubStringLength(0, c ? d + 1 : d) } catch (ja) { "" } else n.getSpanWidth && (y(l(g || c, L)), h[d] = k + n.getSpanWidth(b, a)); return h[d] }, S; b.rotation = 0; var E = F(a.textContent.length); if (S = k + E > d) { for (; f <= v;)t = Math.ceil((f + v) / 2), c && (N = l(c, t)), E = F(t, N && N.length - 1), f === v ? f = v + 1 : E > d ? v = t - 1 : f = t; 0 === v ? y("") : g && v === g.length - 1 || y(N || l(g || c, t)) } c && c.splice(0, t); b.actualWidth = E; b.rotation = L; return S
                }; b.prototype.buildText = function (b) {
                    var a = b.element, n = this,
                    g = n.forExport, c = f(b.textStr, "").toString(), l = -1 !== c.indexOf("<"), v = a.childNodes, t, F = J(a, "x"), E = b.styles, I = b.textWidth, r = E && E.lineHeight, C = E && E.textOutline, w = E && "ellipsis" === E.textOverflow, e = E && "nowrap" === E.whiteSpace, P = E && E.fontSize, A, p = v.length; E = I && !b.added && this.box; var H = function (b) { var g; n.styledMode || (g = /(px|em)$/.test(b && b.style.fontSize) ? b.style.fontSize : P || n.style.fontSize || 12); return r ? d(r) : n.fontMetrics(g, b.getAttribute("style") ? b : a).h }, K = function (b, a) {
                        h(n.escapes, function (n, g) {
                        a && -1 !==
                            a.indexOf(n) || (b = b.toString().replace(new RegExp(n, "g"), g))
                        }); return b
                    }, G = function (b, a) { var n = b.indexOf("<"); b = b.substring(n, b.indexOf(">") - n); n = b.indexOf(a + "="); if (-1 !== n && (n = n + a.length + 1, a = b.charAt(n), '"' === a || "'" === a)) return b = b.substring(n + 1), b.substring(0, b.indexOf(a)) }, q = /<br.*?>/g; var z = [c, w, e, r, C, P, I].join(); if (z !== b.textCache) {
                        for (b.textCache = z; p--;)a.removeChild(v[p]); l || C || w || I || -1 !== c.indexOf(" ") && (!e || q.test(c)) ? (E && E.appendChild(a), l ? (c = n.styledMode ? c.replace(/<(b|strong)>/g, '<span class="highcharts-strong">').replace(/<(i|em)>/g,
                            '<span class="highcharts-emphasized">') : c.replace(/<(b|strong)>/g, '<span style="font-weight:bold">').replace(/<(i|em)>/g, '<span style="font-style:italic">'), c = c.replace(/<a/g, "<span").replace(/<\/(b|strong|i|em|a)>/g, "</span>").split(q)) : c = [c], c = c.filter(function (b) { return "" !== b }), c.forEach(function (c, d) {
                                var L = 0, l = 0; c = c.replace(/^\s+|\s+$/g, "").replace(/<span/g, "|||<span").replace(/<\/span>/g, "</span>|||"); var N = c.split("|||"); N.forEach(function (c) {
                                    if ("" !== c || 1 === N.length) {
                                        var f = {}, v = x.createElementNS(n.SVG_NS,
                                            "tspan"), h, S; (h = G(c, "class")) && J(v, "class", h); if (h = G(c, "style")) h = h.replace(/(;| |^)color([ :])/, "$1fill$2"), J(v, "style", h); if ((S = G(c, "href")) && !g && -1 === S.split(":")[0].toLowerCase().indexOf("javascript")) { var E = x.createElementNS(n.SVG_NS, "a"); J(E, "href", S); J(v, "class", "highcharts-anchor"); E.appendChild(v); n.styledMode || m(v, { cursor: "pointer" }) } c = K(c.replace(/<[a-zA-Z\/](.|\n)*?>/g, "") || " "); if (" " !== c) {
                                                v.appendChild(x.createTextNode(c)); L ? f.dx = 0 : d && null !== F && (f.x = F); J(v, f); a.appendChild(E || v); !L &&
                                                    A && (!k && g && m(v, { display: "block" }), J(v, "dy", H(v))); if (I) {
                                                        var u = c.replace(/([^\^])-/g, "$1- ").split(" "); f = !e && (1 < N.length || d || 1 < u.length); E = 0; S = H(v); if (w) t = n.truncate(b, v, c, void 0, 0, Math.max(0, I - parseInt(P || 12, 10)), function (b, a) { return b.substring(0, a) + "\u2026" }); else if (f) for (; u.length;)u.length && !e && 0 < E && (v = x.createElementNS(y, "tspan"), J(v, { dy: S, x: F }), h && J(v, "style", h), v.appendChild(x.createTextNode(u.join(" ").replace(/- /g, "-"))), a.appendChild(v)), n.truncate(b, v, null, u, 0 === E ? l : 0, I, function (b, a) {
                                                            return u.slice(0,
                                                                a).join(" ").replace(/- /g, "-")
                                                        }), l = b.actualWidth, E++
                                                    } L++
                                            }
                                    }
                                }); A = A || a.childNodes.length
                            }), w && t && b.attr("title", K(b.textStr || "", ["&lt;", "&gt;"])), E && E.removeChild(a), u(C) && b.applyTextOutline && b.applyTextOutline(C)) : a.appendChild(x.createTextNode(K(c)))
                    }
                }; b.prototype.getContrast = function (b) { b = p.parse(b).rgba; b[0] *= 1; b[1] *= 1.2; b[2] *= .5; return 459 < b[0] + b[1] + b[2] ? "#000000" : "#FFFFFF" }; b.prototype.button = function (b, a, g, c, k, d, l, f, v, x) {
                    var n = this.label(b, a, g, v, void 0, void 0, x, void 0, "button"), L = 0, N = this.styledMode;
                    b = k && k.style || {}; k && k.style && delete k.style; n.attr(C({ padding: 8, r: 2 }, k)); if (!N) { k = C({ fill: "#f7f7f7", stroke: "#cccccc", "stroke-width": 1, style: { color: "#333333", cursor: "pointer", fontWeight: "normal" } }, { style: b }, k); var t = k.style; delete k.style; d = C(k, { fill: "#e6e6e6" }, d); var h = d.style; delete d.style; l = C(k, { fill: "#e6ebf5", style: { color: "#000000", fontWeight: "bold" } }, l); var y = l.style; delete l.style; f = C(k, { style: { color: "#cccccc" } }, f); var F = f.style; delete f.style } z(n.element, E ? "mouseover" : "mouseenter", function () {
                    3 !==
                        L && n.setState(1)
                    }); z(n.element, E ? "mouseout" : "mouseleave", function () { 3 !== L && n.setState(L) }); n.setState = function (b) { 1 !== b && (n.state = L = b); n.removeClass(/highcharts-button-(normal|hover|pressed|disabled)/).addClass("highcharts-button-" + ["normal", "hover", "pressed", "disabled"][b || 0]); N || n.attr([k, d, l, f][b || 0]).css([t, h, y, F][b || 0]) }; N || n.attr(k).css(A({ cursor: "default" }, t)); return n.on("click", function (b) { 3 !== L && c.call(n, b) })
                }; b.prototype.crispLine = function (b, a, g) {
                void 0 === g && (g = "round"); var n = b[0], c = b[1];
                    n[1] === c[1] && (n[1] = c[1] = Math[g](n[1]) - a % 2 / 2); n[2] === c[2] && (n[2] = c[2] = Math[g](n[2]) + a % 2 / 2); return b
                }; b.prototype.path = function (b) { var a = this.styledMode ? {} : { fill: "none" }; K(b) ? a.d = b : r(b) && A(a, b); return this.createElement("path").attr(a) }; b.prototype.circle = function (b, a, g) { b = r(b) ? b : "undefined" === typeof b ? {} : { x: b, y: a, r: g }; a = this.createElement("circle"); a.xSetter = a.ySetter = function (b, a, n) { n.setAttribute("c" + a, b) }; return a.attr(b) }; b.prototype.arc = function (b, a, g, c, k, d) {
                    r(b) ? (c = b, a = c.y, g = c.r, b = c.x) : c = {
                        innerR: c,
                        start: k, end: d
                    }; b = this.symbol("arc", b, a, g, g, c); b.r = g; return b
                }; b.prototype.rect = function (b, a, g, c, k, d) { k = r(b) ? b.r : k; var n = this.createElement("rect"); b = r(b) ? b : "undefined" === typeof b ? {} : { x: b, y: a, width: Math.max(g, 0), height: Math.max(c, 0) }; this.styledMode || ("undefined" !== typeof d && (b.strokeWidth = d, b = n.crisp(b)), b.fill = "none"); k && (b.r = k); n.rSetter = function (b, a, g) { n.r = b; J(g, { rx: b, ry: b }) }; n.rGetter = function () { return n.r }; return n.attr(b) }; b.prototype.setSize = function (b, a, g) {
                    var n = this.alignedObjects, c = n.length;
                    this.width = b; this.height = a; for (this.boxWrapper.animate({ width: b, height: a }, { step: function () { this.attr({ viewBox: "0 0 " + this.attr("width") + " " + this.attr("height") }) }, duration: f(g, !0) ? void 0 : 0 }); c--;)n[c].align()
                }; b.prototype.g = function (b) { var a = this.createElement("g"); return b ? a.attr({ "class": "highcharts-" + b }) : a }; b.prototype.image = function (b, a, g, c, k, d) {
                    var n = { preserveAspectRatio: "none" }, L = function (b, a) {
                        b.setAttributeNS ? b.setAttributeNS("http://www.w3.org/1999/xlink", "href", a) : b.setAttribute("hc-svg-href",
                            a)
                    }, l = function (a) { L(f.element, b); d.call(f, a) }; 1 < arguments.length && A(n, { x: a, y: g, width: c, height: k }); var f = this.createElement("image").attr(n); d ? (L(f.element, "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw=="), n = new P.Image, z(n, "load", l), n.src = b, n.complete && l({})) : L(f.element, b); return f
                }; b.prototype.symbol = function (b, a, g, k, d, l) {
                    var n = this, L = /^url\((.*?)\)$/, v = L.test(b), N = !v && (this.symbols[b] ? b : "circle"), t = N && this.symbols[N], h; if (t) {
                    "number" === typeof a && (h = t.call(this.symbols,
                        Math.round(a || 0), Math.round(g || 0), k || 0, d || 0, l)); var y = this.path(h); n.styledMode || y.attr("fill", "none"); A(y, { symbolName: N, x: a, y: g, width: k, height: d }); l && A(y, l)
                    } else if (v) {
                        var F = b.match(L)[1]; y = this.image(F); y.imgwidth = f(I[F] && I[F].width, l && l.width); y.imgheight = f(I[F] && I[F].height, l && l.height); var E = function () { y.attr({ width: y.width, height: y.height }) };["width", "height"].forEach(function (b) {
                        y[b + "Setter"] = function (b, a) {
                            var n = {}, g = this["img" + a], c = "width" === a ? "translateX" : "translateY"; this[a] = b; H(g) && (l &&
                                "within" === l.backgroundSize && this.width && this.height && (g = Math.round(g * Math.min(this.width / this.imgwidth, this.height / this.imgheight))), this.element && this.element.setAttribute(a, g), this.alignByTranslate || (n[c] = ((this[a] || 0) - g) / 2, this.attr(n)))
                        }
                        }); H(a) && y.attr({ x: a, y: g }); y.isImg = !0; H(y.imgwidth) && H(y.imgheight) ? E() : (y.attr({ width: 0, height: 0 }), G("img", {
                            onload: function () {
                                var b = c[n.chartIndex]; 0 === this.width && (m(this, { position: "absolute", top: "-999em" }), x.body.appendChild(this)); I[F] = { width: this.width, height: this.height };
                                y.imgwidth = this.width; y.imgheight = this.height; y.element && E(); this.parentNode && this.parentNode.removeChild(this); n.imgCount--; if (!n.imgCount && b && !b.hasLoaded) b.onload()
                            }, src: F
                        }), this.imgCount++)
                    } return y
                }; b.prototype.clipRect = function (b, a, g, c) { var n = l() + "-", k = this.createElement("clipPath").attr({ id: n }).add(this.defs); b = this.rect(b, a, g, c, 0).add(k); b.id = n; b.clipPath = k; b.count = 0; return b }; b.prototype.text = function (b, a, g, c) {
                    var n = {}; if (c && (this.allowHTML || !this.forExport)) return this.html(b, a, g); n.x = Math.round(a ||
                        0); g && (n.y = Math.round(g)); H(b) && (n.text = b); b = this.createElement("text").attr(n); c || (b.xSetter = function (b, a, n) { var g = n.getElementsByTagName("tspan"), c = n.getAttribute(a), k; for (k = 0; k < g.length; k++) { var d = g[k]; d.getAttribute(a) === c && d.setAttribute(a, b) } n.setAttribute(a, b) }); return b
                }; b.prototype.fontMetrics = function (b, a) {
                    b = !this.styledMode && /px/.test(b) || !P.getComputedStyle ? b || a && a.style && a.style.fontSize || this.style && this.style.fontSize : a && q.prototype.getStyle.call(a, "font-size"); b = /px/.test(b) ? d(b) :
                        12; a = 24 > b ? b + 3 : Math.round(1.2 * b); return { h: a, b: Math.round(.8 * a), f: b }
                }; b.prototype.rotCorr = function (b, g, c) { var n = b; g && c && (n = Math.max(n * Math.cos(g * a), 4)); return { x: -b / 3 * Math.sin(g * a), y: n } }; b.prototype.pathToSegments = function (b) {
                    for (var a = [], n = [], g = { A: 8, C: 7, H: 2, L: 3, M: 3, Q: 5, S: 5, T: 3, V: 2 }, c = 0; c < b.length; c++)u(n[0]) && w(b[c]) && n.length === g[n[0].toUpperCase()] && b.splice(c, 0, n[0].replace("M", "L").replace("m", "l")), "string" === typeof b[c] && (n.length && a.push(n.slice(0)), n.length = 0), n.push(b[c]); a.push(n.slice(0));
                    return a
                }; b.prototype.label = function (b, a, g, c, k, d, l, f, v) { return new B(this, b, a, g, c, k, d, l, f, v) }; return b
            }(); g.prototype.Element = q; g.prototype.SVG_NS = y; g.prototype.draw = D; g.prototype.escapes = { "&": "&amp;", "<": "&lt;", ">": "&gt;", "'": "&#39;", '"': "&quot;" }; g.prototype.symbols = {
                circle: function (b, a, g, c) { return this.arc(b + g / 2, a + c / 2, g / 2, c / 2, { start: .5 * Math.PI, end: 2.5 * Math.PI, open: !1 }) }, square: function (b, a, g, c) { return [["M", b, a], ["L", b + g, a], ["L", b + g, a + c], ["L", b, a + c], ["Z"]] }, triangle: function (b, a, g, c) {
                    return [["M",
                        b + g / 2, a], ["L", b + g, a + c], ["L", b, a + c], ["Z"]]
                }, "triangle-down": function (b, a, g, c) { return [["M", b, a], ["L", b + g, a], ["L", b + g / 2, a + c], ["Z"]] }, diamond: function (b, a, g, c) { return [["M", b + g / 2, a], ["L", b + g, a + c / 2], ["L", b + g / 2, a + c], ["L", b, a + c / 2], ["Z"]] }, arc: function (b, a, g, c, k) {
                    var n = []; if (k) {
                        var d = k.start || 0, l = k.end || 0, L = k.r || g; g = k.r || c || g; var v = .001 > Math.abs(l - d - 2 * Math.PI); l -= .001; c = k.innerR; v = f(k.open, v); var x = Math.cos(d), N = Math.sin(d), t = Math.cos(l), y = Math.sin(l); d = f(k.longArc, .001 > l - d - Math.PI ? 0 : 1); n.push(["M", b + L * x,
                            a + g * N], ["A", L, g, 0, d, f(k.clockwise, 1), b + L * t, a + g * y]); H(c) && n.push(v ? ["M", b + c * t, a + c * y] : ["L", b + c * t, a + c * y], ["A", c, c, 0, d, H(k.clockwise) ? 1 - k.clockwise : 0, b + c * x, a + c * N]); v || n.push(["Z"])
                    } return n
                }, callout: function (b, a, g, c, k) {
                    var n = Math.min(k && k.r || 0, g, c), d = n + 6, l = k && k.anchorX || 0; k = k && k.anchorY || 0; var f = [["M", b + n, a], ["L", b + g - n, a], ["C", b + g, a, b + g, a, b + g, a + n], ["L", b + g, a + c - n], ["C", b + g, a + c, b + g, a + c, b + g - n, a + c], ["L", b + n, a + c], ["C", b, a + c, b, a + c, b, a + c - n], ["L", b, a + n], ["C", b, a, b, a, b + n, a]]; l && l > g ? k > a + d && k < a + c - d ? f.splice(3, 1,
                        ["L", b + g, k - 6], ["L", b + g + 6, k], ["L", b + g, k + 6], ["L", b + g, a + c - n]) : f.splice(3, 1, ["L", b + g, c / 2], ["L", l, k], ["L", b + g, c / 2], ["L", b + g, a + c - n]) : l && 0 > l ? k > a + d && k < a + c - d ? f.splice(7, 1, ["L", b, k + 6], ["L", b - 6, k], ["L", b, k - 6], ["L", b, a + n]) : f.splice(7, 1, ["L", b, c / 2], ["L", l, k], ["L", b, c / 2], ["L", b, a + n]) : k && k > c && l > b + d && l < b + g - d ? f.splice(5, 1, ["L", l + 6, a + c], ["L", l, a + c + 6], ["L", l - 6, a + c], ["L", b + n, a + c]) : k && 0 > k && l > b + d && l < b + g - d && f.splice(1, 1, ["L", l - 6, a], ["L", l, a - 6], ["L", l + 6, a], ["L", g - n, a]); return f
                }
            }; e.SVGRenderer = g; e.Renderer = e.SVGRenderer;
        return e.Renderer
    }); O(q, "parts/Html.js", [q["parts/Globals.js"], q["parts/SVGElement.js"], q["parts/SVGRenderer.js"], q["parts/Utilities.js"]], function (p, e, q, B) {
        var D = B.attr, z = B.createElement, J = B.css, G = B.defined, m = B.extend, H = B.pick, M = B.pInt, A = p.isFirefox, K = p.isMS, w = p.isWebKit, r = p.win; m(e.prototype, {
            htmlCss: function (u) {
                var r = "SPAN" === this.element.tagName && u && "width" in u, h = H(r && u.width, void 0); if (r) { delete u.width; this.textWidth = h; var f = !0 } u && "ellipsis" === u.textOverflow && (u.whiteSpace = "nowrap", u.overflow =
                    "hidden"); this.styles = m(this.styles, u); J(this.element, u); f && this.htmlUpdateTransform(); return this
            }, htmlGetBBox: function () { var u = this.element; return { x: u.offsetLeft, y: u.offsetTop, width: u.offsetWidth, height: u.offsetHeight } }, htmlUpdateTransform: function () {
                if (this.added) {
                    var u = this.renderer, r = this.element, h = this.translateX || 0, f = this.translateY || 0, d = this.x || 0, t = this.y || 0, l = this.textAlign || "left", c = { left: 0, center: .5, right: 1 }[l], a = this.styles, x = a && a.whiteSpace; J(r, { marginLeft: h, marginTop: f }); !u.styledMode &&
                        this.shadows && this.shadows.forEach(function (a) { J(a, { marginLeft: h + 1, marginTop: f + 1 }) }); this.inverted && [].forEach.call(r.childNodes, function (a) { u.invertChild(a, r) }); if ("SPAN" === r.tagName) {
                            a = this.rotation; var v = this.textWidth && M(this.textWidth), E = [a, l, r.innerHTML, this.textWidth, this.textAlign].join(), F; (F = v !== this.oldTextWidth) && !(F = v > this.oldTextWidth) && ((F = this.textPxLength) || (J(r, { width: "", whiteSpace: x || "nowrap" }), F = r.offsetWidth), F = F > v); F && (/[ \-]/.test(r.textContent || r.innerText) || "ellipsis" === r.style.textOverflow) ?
                                (J(r, { width: v + "px", display: "block", whiteSpace: x || "normal" }), this.oldTextWidth = v, this.hasBoxWidthChanged = !0) : this.hasBoxWidthChanged = !1; E !== this.cTT && (x = u.fontMetrics(r.style.fontSize, r).b, !G(a) || a === (this.oldRotation || 0) && l === this.oldAlign || this.setSpanRotation(a, c, x), this.getSpanCorrection(!G(a) && this.textPxLength || r.offsetWidth, x, c, a, l)); J(r, { left: d + (this.xCorr || 0) + "px", top: t + (this.yCorr || 0) + "px" }); this.cTT = E; this.oldRotation = a; this.oldAlign = l
                        }
                } else this.alignOnAdd = !0
            }, setSpanRotation: function (u,
                r, h) { var f = {}, d = this.renderer.getTransformKey(); f[d] = f.transform = "rotate(" + u + "deg)"; f[d + (A ? "Origin" : "-origin")] = f.transformOrigin = 100 * r + "% " + h + "px"; J(this.element, f) }, getSpanCorrection: function (u, r, h) { this.xCorr = -u * h; this.yCorr = -r }
        }); m(q.prototype, {
            getTransformKey: function () { return K && !/Edge/.test(r.navigator.userAgent) ? "-ms-transform" : w ? "-webkit-transform" : A ? "MozTransform" : r.opera ? "-o-transform" : "" }, html: function (u, r, h) {
                var f = this.createElement("span"), d = f.element, t = f.renderer, l = t.isSVG, c = function (a,
                    c) { ["opacity", "visibility"].forEach(function (d) { a[d + "Setter"] = function (l, f, k) { var v = a.div ? a.div.style : c; e.prototype[d + "Setter"].call(this, l, f, k); v && (v[f] = l) } }); a.addedSetters = !0 }; f.textSetter = function (a) { a !== d.innerHTML && (delete this.bBox, delete this.oldTextWidth); this.textStr = a; d.innerHTML = H(a, ""); f.doTransform = !0 }; l && c(f, f.element.style); f.xSetter = f.ySetter = f.alignSetter = f.rotationSetter = function (a, c) { "align" === c && (c = "textAlign"); f[c] = a; f.doTransform = !0 }; f.afterSetters = function () {
                    this.doTransform &&
                        (this.htmlUpdateTransform(), this.doTransform = !1)
                    }; f.attr({ text: u, x: Math.round(r), y: Math.round(h) }).css({ position: "absolute" }); t.styledMode || f.css({ fontFamily: this.style.fontFamily, fontSize: this.style.fontSize }); d.style.whiteSpace = "nowrap"; f.css = f.htmlCss; l && (f.add = function (a) {
                        var l = t.box.parentNode, v = []; if (this.parentGroup = a) {
                            var h = a.div; if (!h) {
                                for (; a;)v.push(a), a = a.parentGroup; v.reverse().forEach(function (a) {
                                    function k(c, g) { a[g] = c; "translateX" === g ? x.left = c + "px" : x.top = c + "px"; a.doTransform = !0 } var d =
                                        D(a.element, "class"); h = a.div = a.div || z("div", d ? { className: d } : void 0, { position: "absolute", left: (a.translateX || 0) + "px", top: (a.translateY || 0) + "px", display: a.display, opacity: a.opacity, pointerEvents: a.styles && a.styles.pointerEvents }, h || l); var x = h.style; m(a, { classSetter: function (a) { return function (g) { this.element.setAttribute("class", g); a.className = g } }(h), on: function () { v[0].div && f.on.apply({ element: v[0].div }, arguments); return a }, translateXSetter: k, translateYSetter: k }); a.addedSetters || c(a)
                                })
                            }
                        } else h = l; h.appendChild(d);
                        f.added = !0; f.alignOnAdd && f.htmlUpdateTransform(); return f
                    }); return f
            }
        })
    }); O(q, "parts/Tick.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
        var q = e.clamp, B = e.correctFloat, D = e.defined, z = e.destroyObjectProperties, J = e.extend, G = e.fireEvent, m = e.isNumber, H = e.merge, M = e.objectEach, A = e.pick, K = p.deg2rad; e = function () {
            function w(r, u, C, h, f) {
            this.isNewLabel = this.isNew = !0; this.axis = r; this.pos = u; this.type = C || ""; this.parameters = f || {}; this.tickmarkOffset = this.parameters.tickmarkOffset; this.options =
                this.parameters.options; G(this, "init"); C || h || this.addLabel()
            } w.prototype.addLabel = function () {
                var r = this, u = r.axis, C = u.options, h = u.chart, f = u.categories, d = u.logarithmic, t = u.names, l = r.pos, c = A(r.options && r.options.labels, C.labels), a = u.tickPositions, x = l === a[0], v = l === a[a.length - 1]; t = this.parameters.category || (f ? A(f[l], t[l], l) : l); var E = r.label; f = (!c.step || 1 === c.step) && 1 === u.tickInterval; a = a.info; var F, k; if (u.dateTime && a) {
                    var y = h.time.resolveDTLFormat(C.dateTimeLabelFormats[!C.grid && a.higherRanks[l] || a.unitName]);
                    var I = y.main
                } r.isFirst = x; r.isLast = v; r.formatCtx = { axis: u, chart: h, isFirst: x, isLast: v, dateTimeLabelFormat: I, tickPositionInfo: a, value: d ? B(d.lin2log(t)) : t, pos: l }; C = u.labelFormatter.call(r.formatCtx, this.formatCtx); if (k = y && y.list) r.shortenLabel = function () { for (F = 0; F < k.length; F++)if (E.attr({ text: u.labelFormatter.call(J(r.formatCtx, { dateTimeLabelFormat: k[F] })) }), E.getBBox().width < u.getSlotWidth(r) - 2 * A(c.padding, 5)) return; E.attr({ text: "" }) }; f && u._addedPlotLB && u.isXAxis && r.moveLabel(C, c); D(E) || r.movedLabel ?
                    E && E.textStr !== C && !f && (!E.textWidth || c.style && c.style.width || E.styles.width || E.css({ width: null }), E.attr({ text: C }), E.textPxLength = E.getBBox().width) : (r.label = E = r.createLabel({ x: 0, y: 0 }, C, c), r.rotation = 0)
            }; w.prototype.createLabel = function (r, u, C) { var h = this.axis, f = h.chart; if (r = D(u) && C.enabled ? f.renderer.text(u, r.x, r.y, C.useHTML).add(h.labelGroup) : null) f.styledMode || r.css(H(C.style)), r.textPxLength = r.getBBox().width; return r }; w.prototype.destroy = function () { z(this, this.axis) }; w.prototype.getPosition = function (r,
                u, C, h) { var f = this.axis, d = f.chart, t = h && d.oldChartHeight || d.chartHeight; r = { x: r ? B(f.translate(u + C, null, null, h) + f.transB) : f.left + f.offset + (f.opposite ? (h && d.oldChartWidth || d.chartWidth) - f.right - f.left : 0), y: r ? t - f.bottom + f.offset - (f.opposite ? f.height : 0) : B(t - f.translate(u + C, null, null, h) - f.transB) }; r.y = q(r.y, -1E5, 1E5); G(this, "afterGetPosition", { pos: r }); return r }; w.prototype.getLabelPosition = function (r, u, C, h, f, d, t, l) {
                    var c = this.axis, a = c.transA, x = c.isLinked && c.linkedParent ? c.linkedParent.reversed : c.reversed,
                    v = c.staggerLines, E = c.tickRotCorr || { x: 0, y: 0 }, F = f.y, k = h || c.reserveSpaceDefault ? 0 : -c.labelOffset * ("center" === c.labelAlign ? .5 : 1), y = {}; D(F) || (F = 0 === c.side ? C.rotation ? -8 : -C.getBBox().height : 2 === c.side ? E.y + 8 : Math.cos(C.rotation * K) * (E.y - C.getBBox(!1, 0).height / 2)); r = r + f.x + k + E.x - (d && h ? d * a * (x ? -1 : 1) : 0); u = u + F - (d && !h ? d * a * (x ? 1 : -1) : 0); v && (C = t / (l || 1) % v, c.opposite && (C = v - C - 1), u += c.labelOffset / v * C); y.x = r; y.y = Math.round(u); G(this, "afterGetLabelPosition", { pos: y, tickmarkOffset: d, index: t }); return y
                }; w.prototype.getLabelSize =
                    function () { return this.label ? this.label.getBBox()[this.axis.horiz ? "height" : "width"] : 0 }; w.prototype.getMarkPath = function (r, u, C, h, f, d) { return d.crispLine([["M", r, u], ["L", r + (f ? 0 : -C), u + (f ? C : 0)]], h) }; w.prototype.handleOverflow = function (r) {
                        var u = this.axis, C = u.options.labels, h = r.x, f = u.chart.chartWidth, d = u.chart.spacing, t = A(u.labelLeft, Math.min(u.pos, d[3])); d = A(u.labelRight, Math.max(u.isRadial ? 0 : u.pos + u.len, f - d[1])); var l = this.label, c = this.rotation, a = { left: 0, center: .5, right: 1 }[u.labelAlign || l.attr("align")],
                            x = l.getBBox().width, v = u.getSlotWidth(this), E = v, F = 1, k, y = {}; if (c || "justify" !== A(C.overflow, "justify")) 0 > c && h - a * x < t ? k = Math.round(h / Math.cos(c * K) - t) : 0 < c && h + a * x > d && (k = Math.round((f - h) / Math.cos(c * K))); else if (f = h + (1 - a) * x, h - a * x < t ? E = r.x + E * (1 - a) - t : f > d && (E = d - r.x + E * a, F = -1), E = Math.min(v, E), E < v && "center" === u.labelAlign && (r.x += F * (v - E - a * (v - Math.min(x, E)))), x > E || u.autoRotation && (l.styles || {}).width) k = E; k && (this.shortenLabel ? this.shortenLabel() : (y.width = Math.floor(k) + "px", (C.style || {}).textOverflow || (y.textOverflow =
                                "ellipsis"), l.css(y)))
                    }; w.prototype.moveLabel = function (r, u) { var C = this, h = C.label, f = !1, d = C.axis, t = d.reversed, l = d.chart.inverted; h && h.textStr === r ? (C.movedLabel = h, f = !0, delete C.label) : M(d.ticks, function (a) { f || a.isNew || a === C || !a.label || a.label.textStr !== r || (C.movedLabel = a.label, f = !0, a.labelPos = C.movedLabel.xy, delete a.label) }); if (!f && (C.labelPos || h)) { var c = C.labelPos || h.xy; h = l ? c.x : t ? 0 : d.width + d.left; d = l ? t ? d.width + d.left : 0 : c.y; C.movedLabel = C.createLabel({ x: h, y: d }, r, u); C.movedLabel && C.movedLabel.attr({ opacity: 0 }) } };
            w.prototype.render = function (r, u, C) { var h = this.axis, f = h.horiz, d = this.pos, t = A(this.tickmarkOffset, h.tickmarkOffset); d = this.getPosition(f, d, t, u); t = d.x; var l = d.y; h = f && t === h.pos + h.len || !f && l === h.pos ? -1 : 1; C = A(C, 1); this.isActive = !0; this.renderGridLine(u, C, h); this.renderMark(d, C, h); this.renderLabel(d, u, C, r); this.isNew = !1; G(this, "afterRender") }; w.prototype.renderGridLine = function (r, u, C) {
                var h = this.axis, f = h.options, d = this.gridLine, t = {}, l = this.pos, c = this.type, a = A(this.tickmarkOffset, h.tickmarkOffset), x = h.chart.renderer,
                v = c ? c + "Grid" : "grid", E = f[v + "LineWidth"], F = f[v + "LineColor"]; f = f[v + "LineDashStyle"]; d || (h.chart.styledMode || (t.stroke = F, t["stroke-width"] = E, f && (t.dashstyle = f)), c || (t.zIndex = 1), r && (u = 0), this.gridLine = d = x.path().attr(t).addClass("highcharts-" + (c ? c + "-" : "") + "grid-line").add(h.gridGroup)); if (d && (C = h.getPlotLinePath({ value: l + a, lineWidth: d.strokeWidth() * C, force: "pass", old: r }))) d[r || this.isNew ? "attr" : "animate"]({ d: C, opacity: u })
            }; w.prototype.renderMark = function (r, u, C) {
                var h = this.axis, f = h.options, d = h.chart.renderer,
                t = this.type, l = t ? t + "Tick" : "tick", c = h.tickSize(l), a = this.mark, x = !a, v = r.x; r = r.y; var E = A(f[l + "Width"], !t && h.isXAxis ? 1 : 0); f = f[l + "Color"]; c && (h.opposite && (c[0] = -c[0]), x && (this.mark = a = d.path().addClass("highcharts-" + (t ? t + "-" : "") + "tick").add(h.axisGroup), h.chart.styledMode || a.attr({ stroke: f, "stroke-width": E })), a[x ? "attr" : "animate"]({ d: this.getMarkPath(v, r, c[0], a.strokeWidth() * C, h.horiz, d), opacity: u }))
            }; w.prototype.renderLabel = function (r, u, C, h) {
                var f = this.axis, d = f.horiz, t = f.options, l = this.label, c = t.labels,
                a = c.step; f = A(this.tickmarkOffset, f.tickmarkOffset); var x = !0, v = r.x; r = r.y; l && m(v) && (l.xy = r = this.getLabelPosition(v, r, l, d, c, f, h, a), this.isFirst && !this.isLast && !A(t.showFirstLabel, 1) || this.isLast && !this.isFirst && !A(t.showLastLabel, 1) ? x = !1 : !d || c.step || c.rotation || u || 0 === C || this.handleOverflow(r), a && h % a && (x = !1), x && m(r.y) ? (r.opacity = C, l[this.isNewLabel ? "attr" : "animate"](r), this.isNewLabel = !1) : (l.attr("y", -9999), this.isNewLabel = !0))
            }; w.prototype.replaceMovedLabel = function () {
                var r = this.label, u = this.axis, C =
                    u.reversed, h = this.axis.chart.inverted; if (r && !this.isNew) { var f = h ? r.xy.x : C ? u.left : u.width + u.left; C = h ? C ? u.width + u.top : u.top : r.xy.y; r.animate({ x: f, y: C, opacity: 0 }, void 0, r.destroy); delete this.label } u.isDirty = !0; this.label = this.movedLabel; delete this.movedLabel
            }; return w
        }(); p.Tick = e; return p.Tick
    }); O(q, "parts/Time.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
        var q = e.defined, B = e.error, D = e.extend, z = e.isObject, J = e.merge, G = e.objectEach, m = e.pad, H = e.pick, M = e.splat, A = e.timeUnits, K = p.win;
        e = function () {
            function w(r) { this.options = {}; this.variableTimezone = this.useUTC = !1; this.Date = K.Date; this.getTimezoneOffset = this.timezoneOffsetFunction(); this.update(r) } w.prototype.get = function (r, u) { if (this.variableTimezone || this.timezoneOffset) { var C = u.getTime(), h = C - this.getTimezoneOffset(u); u.setTime(h); r = u["getUTC" + r](); u.setTime(C); return r } return this.useUTC ? u["getUTC" + r]() : u["get" + r]() }; w.prototype.set = function (r, u, C) {
                if (this.variableTimezone || this.timezoneOffset) {
                    if ("Milliseconds" === r || "Seconds" ===
                        r || "Minutes" === r) return u["setUTC" + r](C); var h = this.getTimezoneOffset(u); h = u.getTime() - h; u.setTime(h); u["setUTC" + r](C); r = this.getTimezoneOffset(u); h = u.getTime() + r; return u.setTime(h)
                } return this.useUTC ? u["setUTC" + r](C) : u["set" + r](C)
            }; w.prototype.update = function (r) {
                var u = H(r && r.useUTC, !0); this.options = r = J(!0, this.options || {}, r); this.Date = r.Date || K.Date || Date; this.timezoneOffset = (this.useUTC = u) && r.timezoneOffset; this.getTimezoneOffset = this.timezoneOffsetFunction(); this.variableTimezone = !(u && !r.getTimezoneOffset &&
                    !r.timezone)
            }; w.prototype.makeTime = function (r, u, C, h, f, d) { if (this.useUTC) { var t = this.Date.UTC.apply(0, arguments); var l = this.getTimezoneOffset(t); t += l; var c = this.getTimezoneOffset(t); l !== c ? t += c - l : l - 36E5 !== this.getTimezoneOffset(t - 36E5) || p.isSafari || (t -= 36E5) } else t = (new this.Date(r, u, H(C, 1), H(h, 0), H(f, 0), H(d, 0))).getTime(); return t }; w.prototype.timezoneOffsetFunction = function () {
                var r = this, u = this.options, C = K.moment; if (!this.useUTC) return function (h) { return 6E4 * (new Date(h.toString())).getTimezoneOffset() };
                if (u.timezone) { if (C) return function (h) { return 6E4 * -C.tz(h, u.timezone).utcOffset() }; B(25) } return this.useUTC && u.getTimezoneOffset ? function (h) { return 6E4 * u.getTimezoneOffset(h.valueOf()) } : function () { return 6E4 * (r.timezoneOffset || 0) }
            }; w.prototype.dateFormat = function (r, u, C) {
                var h; if (!q(u) || isNaN(u)) return (null === (h = p.defaultOptions.lang) || void 0 === h ? void 0 : h.invalidDate) || ""; r = H(r, "%Y-%m-%d %H:%M:%S"); var f = this; h = new this.Date(u); var d = this.get("Hours", h), t = this.get("Day", h), l = this.get("Date", h), c = this.get("Month",
                    h), a = this.get("FullYear", h), x = p.defaultOptions.lang, v = null === x || void 0 === x ? void 0 : x.weekdays, E = null === x || void 0 === x ? void 0 : x.shortWeekdays; h = D({ a: E ? E[t] : v[t].substr(0, 3), A: v[t], d: m(l), e: m(l, 2, " "), w: t, b: x.shortMonths[c], B: x.months[c], m: m(c + 1), o: c + 1, y: a.toString().substr(2, 2), Y: a, H: m(d), k: d, I: m(d % 12 || 12), l: d % 12 || 12, M: m(this.get("Minutes", h)), p: 12 > d ? "AM" : "PM", P: 12 > d ? "am" : "pm", S: m(h.getSeconds()), L: m(Math.floor(u % 1E3), 3) }, p.dateFormats); G(h, function (a, c) {
                        for (; -1 !== r.indexOf("%" + c);)r = r.replace("%" + c,
                            "function" === typeof a ? a.call(f, u) : a)
                    }); return C ? r.substr(0, 1).toUpperCase() + r.substr(1) : r
            }; w.prototype.resolveDTLFormat = function (r) { return z(r, !0) ? r : (r = M(r), { main: r[0], from: r[1], to: r[2] }) }; w.prototype.getTimeTicks = function (r, u, C, h) {
                var f = this, d = [], t = {}; var l = new f.Date(u); var c = r.unitRange, a = r.count || 1, x; h = H(h, 1); if (q(u)) {
                    f.set("Milliseconds", l, c >= A.second ? 0 : a * Math.floor(f.get("Milliseconds", l) / a)); c >= A.second && f.set("Seconds", l, c >= A.minute ? 0 : a * Math.floor(f.get("Seconds", l) / a)); c >= A.minute && f.set("Minutes",
                        l, c >= A.hour ? 0 : a * Math.floor(f.get("Minutes", l) / a)); c >= A.hour && f.set("Hours", l, c >= A.day ? 0 : a * Math.floor(f.get("Hours", l) / a)); c >= A.day && f.set("Date", l, c >= A.month ? 1 : Math.max(1, a * Math.floor(f.get("Date", l) / a))); if (c >= A.month) { f.set("Month", l, c >= A.year ? 0 : a * Math.floor(f.get("Month", l) / a)); var v = f.get("FullYear", l) } c >= A.year && f.set("FullYear", l, v - v % a); c === A.week && (v = f.get("Day", l), f.set("Date", l, f.get("Date", l) - v + h + (v < h ? -7 : 0))); v = f.get("FullYear", l); h = f.get("Month", l); var E = f.get("Date", l), F = f.get("Hours",
                            l); u = l.getTime(); f.variableTimezone && (x = C - u > 4 * A.month || f.getTimezoneOffset(u) !== f.getTimezoneOffset(C)); u = l.getTime(); for (l = 1; u < C;)d.push(u), u = c === A.year ? f.makeTime(v + l * a, 0) : c === A.month ? f.makeTime(v, h + l * a) : !x || c !== A.day && c !== A.week ? x && c === A.hour && 1 < a ? f.makeTime(v, h, E, F + l * a) : u + c * a : f.makeTime(v, h, E + l * a * (c === A.day ? 1 : 7)), l++; d.push(u); c <= A.hour && 1E4 > d.length && d.forEach(function (a) { 0 === a % 18E5 && "000000000" === f.dateFormat("%H%M%S%L", a) && (t[a] = "day") })
                } d.info = D(r, { higherRanks: t, totalRange: c * a }); return d
            };
            return w
        }(); p.Time = e; return p.Time
    }); O(q, "parts/Options.js", [q["parts/Globals.js"], q["parts/Time.js"], q["parts/Color.js"], q["parts/Utilities.js"]], function (p, e, q, B) {
        q = q.parse; B = B.merge; p.defaultOptions = {
            colors: "#7cb5ec #434348 #90ed7d #f7a35c #8085e9 #f15c80 #e4d354 #2b908f #f45b5b #91e8e1".split(" "), symbols: ["circle", "diamond", "square", "triangle", "triangle-down"], lang: {
                loading: "Loading...", months: "January February March April May June July August September October November December".split(" "),
                shortMonths: "Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec".split(" "), weekdays: "Sunday Monday Tuesday Wednesday Thursday Friday Saturday".split(" "), decimalPoint: ".", numericSymbols: "kMGTPE".split(""), resetZoom: "Reset zoom", resetZoomTitle: "Reset zoom level 1:1", thousandsSep: " "
            }, global: {}, time: { Date: void 0, getTimezoneOffset: void 0, timezone: void 0, timezoneOffset: 0, useUTC: !0 }, chart: {
                styledMode: !1, borderRadius: 0, colorCount: 10, defaultSeriesType: "line", ignoreHiddenSeries: !0, spacing: [10, 10, 15, 10], resetZoomButton: {
                    theme: { zIndex: 6 },
                    position: { align: "right", x: -10, y: 10 }
                }, width: null, height: null, borderColor: "#335cad", backgroundColor: "#ffffff", plotBorderColor: "#cccccc"
            }, title: { text: "Chart title", align: "center", margin: 15, widthAdjust: -44 }, subtitle: { text: "", align: "center", widthAdjust: -44 }, caption: { margin: 15, text: "", align: "left", verticalAlign: "bottom" }, plotOptions: {}, labels: { style: { position: "absolute", color: "#333333" } }, legend: {
                enabled: !0, align: "center", alignColumns: !0, layout: "horizontal", labelFormatter: function () { return this.name }, borderColor: "#999999",
                borderRadius: 0, navigation: { activeColor: "#003399", inactiveColor: "#cccccc" }, itemStyle: { color: "#333333", cursor: "pointer", fontSize: "12px", fontWeight: "bold", textOverflow: "ellipsis" }, itemHoverStyle: { color: "#000000" }, itemHiddenStyle: { color: "#cccccc" }, shadow: !1, itemCheckboxStyle: { position: "absolute", width: "13px", height: "13px" }, squareSymbol: !0, symbolPadding: 5, verticalAlign: "bottom", x: 0, y: 0, title: { style: { fontWeight: "bold" } }
            }, loading: {
                labelStyle: { fontWeight: "bold", position: "relative", top: "45%" }, style: {
                    position: "absolute",
                    backgroundColor: "#ffffff", opacity: .5, textAlign: "center"
                }
            }, tooltip: {
                enabled: !0, animation: p.svg, borderRadius: 3, dateTimeLabelFormats: { millisecond: "%A, %b %e, %H:%M:%S.%L", second: "%A, %b %e, %H:%M:%S", minute: "%A, %b %e, %H:%M", hour: "%A, %b %e, %H:%M", day: "%A, %b %e, %Y", week: "Week from %A, %b %e, %Y", month: "%B %Y", year: "%Y" }, footerFormat: "", padding: 8, snap: p.isTouchDevice ? 25 : 10, headerFormat: '<span style="font-size: 10px">{point.key}</span><br/>', pointFormat: '<span style="color:{point.color}">\u25cf</span> {series.name}: <b>{point.y}</b><br/>',
                backgroundColor: q("#f7f7f7").setOpacity(.85).get(), borderWidth: 1, shadow: !0, style: { color: "#333333", cursor: "default", fontSize: "12px", whiteSpace: "nowrap" }
            }, credits: { enabled: 0, href: "https://www.highcharts.com?credits", position: { align: "right", x: -10, verticalAlign: "bottom", y: -5 }, style: { cursor: "pointer", color: "#999999", fontSize: "9px" }, text: "Highcharts.com" }
        }; ""; p.time = new e(B(p.defaultOptions.global, p.defaultOptions.time)); p.dateFormat = function (e, q, B) { return p.time.dateFormat(e, q, B) }; return {
            dateFormat: p.dateFormat,
            defaultOptions: p.defaultOptions, time: p.time
        }
    }); O(q, "parts/Axis.js", [q["parts/Color.js"], q["parts/Globals.js"], q["parts/Tick.js"], q["parts/Utilities.js"], q["parts/Options.js"]], function (p, e, q, B, D) {
        var z = B.addEvent, J = B.animObject, G = B.arrayMax, m = B.arrayMin, H = B.clamp, M = B.correctFloat, A = B.defined, K = B.destroyObjectProperties, w = B.error, r = B.extend, u = B.fireEvent, C = B.format, h = B.getMagnitude, f = B.isArray, d = B.isFunction, t = B.isNumber, l = B.isString, c = B.merge, a = B.normalizeTickInterval, x = B.objectEach, v = B.pick, E = B.relativeLength,
        F = B.removeEvent, k = B.splat, y = B.syncTimeout, I = D.defaultOptions, P = e.deg2rad; B = function () {
            function g(b, a) {
            this.zoomEnabled = this.width = this.visible = this.userOptions = this.translationSlope = this.transB = this.transA = this.top = this.ticks = this.tickRotCorr = this.tickPositions = this.tickmarkOffset = this.tickInterval = this.tickAmount = this.side = this.series = this.right = this.positiveValuesOnly = this.pos = this.pointRangePadding = this.pointRange = this.plotLinesAndBandsGroups = this.plotLinesAndBands = this.paddedTicks = this.overlap =
                this.options = this.oldMin = this.oldMax = this.offset = this.names = this.minPixelPadding = this.minorTicks = this.minorTickInterval = this.min = this.maxLabelLength = this.max = this.len = this.left = this.labelFormatter = this.labelEdge = this.isLinked = this.height = this.hasVisibleSeries = this.hasNames = this.coll = this.closestPointRange = this.chart = this.categories = this.bottom = this.alternateBands = void 0; this.init(b, a)
            } g.prototype.init = function (b, a) {
                var n = a.isX, g = this; g.chart = b; g.horiz = b.inverted && !g.isZAxis ? !n : n; g.isXAxis = n; g.coll =
                    g.coll || (n ? "xAxis" : "yAxis"); u(this, "init", { userOptions: a }); g.opposite = a.opposite; g.side = a.side || (g.horiz ? g.opposite ? 0 : 2 : g.opposite ? 1 : 3); g.setOptions(a); var c = this.options, l = c.type; g.labelFormatter = c.labels.formatter || g.defaultLabelFormatter; g.userOptions = a; g.minPixelPadding = 0; g.reversed = c.reversed; g.visible = !1 !== c.visible; g.zoomEnabled = !1 !== c.zoomEnabled; g.hasNames = "category" === l || !0 === c.categories; g.categories = c.categories || g.hasNames; g.names || (g.names = [], g.names.keys = {}); g.plotLinesAndBandsGroups =
                        {}; g.positiveValuesOnly = !(!g.logarithmic || c.allowNegativeLog); g.isLinked = A(c.linkedTo); g.ticks = {}; g.labelEdge = []; g.minorTicks = {}; g.plotLinesAndBands = []; g.alternateBands = {}; g.len = 0; g.minRange = g.userMinRange = c.minRange || c.maxZoom; g.range = c.range; g.offset = c.offset || 0; g.max = null; g.min = null; g.crosshair = v(c.crosshair, k(b.options.tooltip.crosshairs)[n ? 0 : 1], !1); a = g.options.events; -1 === b.axes.indexOf(g) && (n ? b.axes.splice(b.xAxis.length, 0, g) : b.axes.push(g), b[g.coll].push(g)); g.series = g.series || []; b.inverted &&
                            !g.isZAxis && n && "undefined" === typeof g.reversed && (g.reversed = !0); g.labelRotation = g.options.labels.rotation; x(a, function (b, a) { d(b) && z(g, a, b) }); u(this, "afterInit")
            }; g.prototype.setOptions = function (b) { this.options = c(g.defaultOptions, "yAxis" === this.coll && g.defaultYAxisOptions, [g.defaultTopAxisOptions, g.defaultRightAxisOptions, g.defaultBottomAxisOptions, g.defaultLeftAxisOptions][this.side], c(I[this.coll], b)); u(this, "afterSetOptions", { userOptions: b }) }; g.prototype.defaultLabelFormatter = function () {
                var b = this.axis,
                a = t(this.value) ? this.value : NaN, g = b.chart.time, c = b.categories, k = this.dateTimeLabelFormat, d = I.lang, l = d.numericSymbols; d = d.numericSymbolMagnitude || 1E3; var f = l && l.length, v = b.options.labels.format; b = b.logarithmic ? Math.abs(a) : b.tickInterval; var x = this.chart, h = x.numberFormatter; if (v) var y = C(v, this, x); else if (c) y = "" + this.value; else if (k) y = g.dateFormat(k, a); else if (f && 1E3 <= b) for (; f-- && "undefined" === typeof y;)g = Math.pow(d, f + 1), b >= g && 0 === 10 * a % g && null !== l[f] && 0 !== a && (y = h(a / g, -1) + l[f]); "undefined" === typeof y &&
                    (y = 1E4 <= Math.abs(a) ? h(a, -1) : h(a, -1, void 0, "")); return y
            }; g.prototype.getSeriesExtremes = function () {
                var b = this, a = b.chart, g; u(this, "getSeriesExtremes", null, function () {
                b.hasVisibleSeries = !1; b.dataMin = b.dataMax = b.threshold = null; b.softThreshold = !b.isXAxis; b.stacking && b.stacking.buildStacks(); b.series.forEach(function (n) {
                    if (n.visible || !a.options.chart.ignoreHiddenSeries) {
                        var c = n.options, k = c.threshold; b.hasVisibleSeries = !0; b.positiveValuesOnly && 0 >= k && (k = null); if (b.isXAxis) {
                            if (c = n.xData, c.length) {
                                g = n.getXExtremes(c);
                                var d = g.min; var l = g.max; t(d) || d instanceof Date || (c = c.filter(t), g = n.getXExtremes(c), d = g.min, l = g.max); c.length && (b.dataMin = Math.min(v(b.dataMin, d), d), b.dataMax = Math.max(v(b.dataMax, l), l))
                            }
                        } else if (n = n.applyExtremes(), t(n.dataMin) && (d = n.dataMin, b.dataMin = Math.min(v(b.dataMin, d), d)), t(n.dataMax) && (l = n.dataMax, b.dataMax = Math.max(v(b.dataMax, l), l)), A(k) && (b.threshold = k), !c.softThreshold || b.positiveValuesOnly) b.softThreshold = !1
                    }
                })
                }); u(this, "afterGetSeriesExtremes")
            }; g.prototype.translate = function (b, a, g,
                c, k, d) { var n = this.linkedParent || this, l = 1, f = 0, v = c ? n.oldTransA : n.transA; c = c ? n.oldMin : n.min; var L = n.minPixelPadding; k = (n.isOrdinal || n.brokenAxis && n.brokenAxis.hasBreaks || n.logarithmic && k) && n.lin2val; v || (v = n.transA); g && (l *= -1, f = n.len); n.reversed && (l *= -1, f -= l * (n.sector || n.len)); a ? (b = (b * l + f - L) / v + c, k && (b = n.lin2val(b))) : (k && (b = n.val2lin(b)), b = t(c) ? l * (b - c) * v + f + l * L + (t(d) ? v * d : 0) : void 0); return b }; g.prototype.toPixels = function (b, a) { return this.translate(b, !1, !this.horiz, null, !0) + (a ? 0 : this.pos) }; g.prototype.toValue =
                    function (b, a) { return this.translate(b - (a ? 0 : this.pos), !0, !this.horiz, null, !0) }; g.prototype.getPlotLinePath = function (b) {
                        function a(b, a, n) { if ("pass" !== y && b < a || b > n) y ? b = H(b, a, n) : w = !0; return b } var g = this, c = g.chart, k = g.left, d = g.top, l = b.old, f = b.value, x = b.translatedValue, h = b.lineWidth, y = b.force, F, E, I, r, C = l && c.oldChartHeight || c.chartHeight, m = l && c.oldChartWidth || c.chartWidth, w, e = g.transB; b = { value: f, lineWidth: h, old: l, force: y, acrossPanes: b.acrossPanes, translatedValue: x }; u(this, "getPlotLinePath", b, function (b) {
                            x =
                            v(x, g.translate(f, null, null, l)); x = H(x, -1E5, 1E5); F = I = Math.round(x + e); E = r = Math.round(C - x - e); t(x) ? g.horiz ? (E = d, r = C - g.bottom, F = I = a(F, k, k + g.width)) : (F = k, I = m - g.right, E = r = a(E, d, d + g.height)) : (w = !0, y = !1); b.path = w && !y ? null : c.renderer.crispLine([["M", F, E], ["L", I, r]], h || 1)
                        }); return b.path
                    }; g.prototype.getLinearTickPositions = function (b, a, g) { var n = M(Math.floor(a / b) * b); g = M(Math.ceil(g / b) * b); var c = [], k; M(n + b) === n && (k = 20); if (this.single) return [a]; for (a = n; a <= g;) { c.push(a); a = M(a + b, k); if (a === d) break; var d = a } return c };
            g.prototype.getMinorTickInterval = function () { var b = this.options; return !0 === b.minorTicks ? v(b.minorTickInterval, "auto") : !1 === b.minorTicks ? null : b.minorTickInterval }; g.prototype.getMinorTickPositions = function () {
                var b = this.options, a = this.tickPositions, g = this.minorTickInterval, c = [], k = this.pointRangePadding || 0, d = this.min - k; k = this.max + k; var l = k - d; if (l && l / g < this.len / 3) {
                    var f = this.logarithmic; if (f) this.paddedTicks.forEach(function (b, a, n) { a && c.push.apply(c, f.getLogTickPositions(g, n[a - 1], n[a], !0)) }); else if (this.dateTime &&
                        "auto" === this.getMinorTickInterval()) c = c.concat(this.getTimeTicks(this.dateTime.normalizeTimeTickInterval(g), d, k, b.startOfWeek)); else for (b = d + (a[0] - d) % g; b <= k && b !== c[0]; b += g)c.push(b)
                } 0 !== c.length && this.trimTicks(c); return c
            }; g.prototype.adjustForMinRange = function () {
                var b = this.options, a = this.min, g = this.max, c = this.logarithmic, k, d, l, f, x; this.isXAxis && "undefined" === typeof this.minRange && !c && (A(b.min) || A(b.max) ? this.minRange = null : (this.series.forEach(function (b) {
                    f = b.xData; for (d = x = b.xIncrement ? 1 : f.length -
                        1; 0 < d; d--)if (l = f[d] - f[d - 1], "undefined" === typeof k || l < k) k = l
                }), this.minRange = Math.min(5 * k, this.dataMax - this.dataMin))); if (g - a < this.minRange) { var t = this.dataMax - this.dataMin >= this.minRange; var y = this.minRange; var h = (y - g + a) / 2; h = [a - h, v(b.min, a - h)]; t && (h[2] = this.logarithmic ? this.logarithmic.log2lin(this.dataMin) : this.dataMin); a = G(h); g = [a + y, v(b.max, a + y)]; t && (g[2] = c ? c.log2lin(this.dataMax) : this.dataMax); g = m(g); g - a < y && (h[0] = g - y, h[1] = v(b.min, g - y), a = G(h)) } this.min = a; this.max = g
            }; g.prototype.getClosest = function () {
                var b;
                this.categories ? b = 1 : this.series.forEach(function (a) { var n = a.closestPointRange, g = a.visible || !a.chart.options.chart.ignoreHiddenSeries; !a.noSharedTooltip && A(n) && g && (b = A(b) ? Math.min(b, n) : n) }); return b
            }; g.prototype.nameToX = function (b) {
                var a = f(this.categories), g = a ? this.categories : this.names, c = b.options.x; b.series.requireSorting = !1; A(c) || (c = !1 === this.options.uniqueNames ? b.series.autoIncrement() : a ? g.indexOf(b.name) : v(g.keys[b.name], -1)); if (-1 === c) { if (!a) var k = g.length } else k = c; "undefined" !== typeof k && (this.names[k] =
                    b.name, this.names.keys[b.name] = k); return k
            }; g.prototype.updateNames = function () {
                var b = this, a = this.names; 0 < a.length && (Object.keys(a.keys).forEach(function (b) { delete a.keys[b] }), a.length = 0, this.minRange = this.userMinRange, (this.series || []).forEach(function (a) {
                a.xIncrement = null; if (!a.points || a.isDirtyData) b.max = Math.max(b.max, a.xData.length - 1), a.processData(), a.generatePoints(); a.data.forEach(function (n, g) {
                    if (n && n.options && "undefined" !== typeof n.name) {
                        var c = b.nameToX(n); "undefined" !== typeof c && c !== n.x &&
                            (n.x = c, a.xData[g] = c)
                    }
                })
                }))
            }; g.prototype.setAxisTranslation = function (b) {
                var a = this, g = a.max - a.min, c = a.axisPointRange || 0, k = 0, d = 0, f = a.linkedParent, x = !!a.categories, t = a.transA, h = a.isXAxis; if (h || x || c) {
                    var y = a.getClosest(); f ? (k = f.minPointOffset, d = f.pointRangePadding) : a.series.forEach(function (b) { var n = x ? 1 : h ? v(b.options.pointRange, y, 0) : a.axisPointRange || 0, g = b.options.pointPlacement; c = Math.max(c, n); if (!a.single || x) b = b.is("xrange") ? !h : h, k = Math.max(k, b && l(g) ? 0 : n / 2), d = Math.max(d, b && "on" === g ? 0 : n) }); f = a.ordinal &&
                        a.ordinal.slope && y ? a.ordinal.slope / y : 1; a.minPointOffset = k *= f; a.pointRangePadding = d *= f; a.pointRange = Math.min(c, a.single && x ? 1 : g); h && (a.closestPointRange = y)
                } b && (a.oldTransA = t); a.translationSlope = a.transA = t = a.staticScale || a.len / (g + d || 1); a.transB = a.horiz ? a.left : a.bottom; a.minPixelPadding = t * k; u(this, "afterSetAxisTranslation")
            }; g.prototype.minFromRange = function () { return this.max - this.range }; g.prototype.setTickInterval = function (b) {
                var n = this, g = n.chart, c = n.logarithmic, k = n.options, d = n.isXAxis, l = n.isLinked,
                f = k.maxPadding, x = k.minPadding, y = k.tickInterval, F = k.tickPixelInterval, E = n.categories, I = t(n.threshold) ? n.threshold : null, r = n.softThreshold; n.dateTime || E || l || this.getTickAmount(); var C = v(n.userMin, k.min); var m = v(n.userMax, k.max); if (l) { n.linkedParent = g[n.coll][k.linkedTo]; var e = n.linkedParent.getExtremes(); n.min = v(e.min, e.dataMin); n.max = v(e.max, e.dataMax); k.type !== n.linkedParent.options.type && w(11, 1, g) } else {
                    if (!r && A(I)) if (n.dataMin >= I) e = I, x = 0; else if (n.dataMax <= I) { var P = I; f = 0 } n.min = v(C, e, n.dataMin); n.max =
                        v(m, P, n.dataMax)
                } c && (n.positiveValuesOnly && !b && 0 >= Math.min(n.min, v(n.dataMin, n.min)) && w(10, 1, g), n.min = M(c.log2lin(n.min), 16), n.max = M(c.log2lin(n.max), 16)); n.range && A(n.max) && (n.userMin = n.min = C = Math.max(n.dataMin, n.minFromRange()), n.userMax = m = n.max, n.range = null); u(n, "foundExtremes"); n.beforePadding && n.beforePadding(); n.adjustForMinRange(); !(E || n.axisPointRange || n.stacking && n.stacking.usePercentage || l) && A(n.min) && A(n.max) && (g = n.max - n.min) && (!A(C) && x && (n.min -= g * x), !A(m) && f && (n.max += g * f)); t(n.userMin) ||
                    (t(k.softMin) && k.softMin < n.min && (n.min = C = k.softMin), t(k.floor) && (n.min = Math.max(n.min, k.floor))); t(n.userMax) || (t(k.softMax) && k.softMax > n.max && (n.max = m = k.softMax), t(k.ceiling) && (n.max = Math.min(n.max, k.ceiling))); r && A(n.dataMin) && (I = I || 0, !A(C) && n.min < I && n.dataMin >= I ? n.min = n.options.minRange ? Math.min(I, n.max - n.minRange) : I : !A(m) && n.max > I && n.dataMax <= I && (n.max = n.options.minRange ? Math.max(I, n.min + n.minRange) : I)); n.tickInterval = n.min === n.max || "undefined" === typeof n.min || "undefined" === typeof n.max ? 1 : l &&
                        !y && F === n.linkedParent.options.tickPixelInterval ? y = n.linkedParent.tickInterval : v(y, this.tickAmount ? (n.max - n.min) / Math.max(this.tickAmount - 1, 1) : void 0, E ? 1 : (n.max - n.min) * F / Math.max(n.len, F)); d && !b && n.series.forEach(function (b) { b.processData(n.min !== n.oldMin || n.max !== n.oldMax) }); n.setAxisTranslation(!0); u(this, "initialAxisTranslation"); n.pointRange && !y && (n.tickInterval = Math.max(n.pointRange, n.tickInterval)); b = v(k.minTickInterval, n.dateTime && !n.series.some(function (b) { return b.noSharedTooltip }) ? n.closestPointRange :
                            0); !y && n.tickInterval < b && (n.tickInterval = b); n.dateTime || n.logarithmic || y || (n.tickInterval = a(n.tickInterval, void 0, h(n.tickInterval), v(k.allowDecimals, .5 > n.tickInterval || void 0 !== this.tickAmount), !!this.tickAmount)); this.tickAmount || (n.tickInterval = n.unsquish()); this.setTickPositions()
            }; g.prototype.setTickPositions = function () {
                var b = this.options, a = b.tickPositions; var g = this.getMinorTickInterval(); var c = b.tickPositioner, k = this.hasVerticalPanning(), d = "colorAxis" === this.coll, l = (d || !k) && b.startOnTick; k =
                    (d || !k) && b.endOnTick; this.tickmarkOffset = this.categories && "between" === b.tickmarkPlacement && 1 === this.tickInterval ? .5 : 0; this.minorTickInterval = "auto" === g && this.tickInterval ? this.tickInterval / 5 : g; this.single = this.min === this.max && A(this.min) && !this.tickAmount && (parseInt(this.min, 10) === this.min || !1 !== b.allowDecimals); this.tickPositions = g = a && a.slice(); !g && (this.ordinal && this.ordinal.positions || !((this.max - this.min) / this.tickInterval > Math.max(2 * this.len, 200)) ? g = this.dateTime ? this.getTimeTicks(this.dateTime.normalizeTimeTickInterval(this.tickInterval,
                        b.units), this.min, this.max, b.startOfWeek, this.ordinal && this.ordinal.positions, this.closestPointRange, !0) : this.logarithmic ? this.logarithmic.getLogTickPositions(this.tickInterval, this.min, this.max) : this.getLinearTickPositions(this.tickInterval, this.min, this.max) : (g = [this.min, this.max], w(19, !1, this.chart)), g.length > this.len && (g = [g[0], g.pop()], g[0] === g[1] && (g.length = 1)), this.tickPositions = g, c && (c = c.apply(this, [this.min, this.max]))) && (this.tickPositions = g = c); this.paddedTicks = g.slice(0); this.trimTicks(g,
                            l, k); this.isLinked || (this.single && 2 > g.length && !this.categories && !this.series.some(function (b) { return b.is("heatmap") && "between" === b.options.pointPlacement }) && (this.min -= .5, this.max += .5), a || c || this.adjustTickAmount()); u(this, "afterSetTickPositions")
            }; g.prototype.trimTicks = function (b, a, g) {
                var n = b[0], c = b[b.length - 1], k = !this.isOrdinal && this.minPointOffset || 0; u(this, "trimTicks"); if (!this.isLinked) {
                    if (a && -Infinity !== n) this.min = n; else for (; this.min - k > b[0];)b.shift(); if (g) this.max = c; else for (; this.max + k <
                        b[b.length - 1];)b.pop(); 0 === b.length && A(n) && !this.options.tickPositions && b.push((c + n) / 2)
                }
            }; g.prototype.alignToOthers = function () { var b = {}, a, g = this.options; !1 === this.chart.options.chart.alignTicks || !1 === g.alignTicks || !1 === g.startOnTick || !1 === g.endOnTick || this.logarithmic || this.chart[this.coll].forEach(function (n) { var g = n.options; g = [n.horiz ? g.left : g.top, g.width, g.height, g.pane].join(); n.series.length && (b[g] ? a = !0 : b[g] = 1) }); return a }; g.prototype.getTickAmount = function () {
                var b = this.options, a = b.tickAmount,
                g = b.tickPixelInterval; !A(b.tickInterval) && !a && this.len < g && !this.isRadial && !this.logarithmic && b.startOnTick && b.endOnTick && (a = 2); !a && this.alignToOthers() && (a = Math.ceil(this.len / g) + 1); 4 > a && (this.finalTickAmt = a, a = 5); this.tickAmount = a
            }; g.prototype.adjustTickAmount = function () {
                var b = this.options, a = this.tickInterval, g = this.tickPositions, c = this.tickAmount, k = this.finalTickAmt, d = g && g.length, l = v(this.threshold, this.softThreshold ? 0 : null), f; if (this.hasData()) {
                    if (d < c) {
                        for (f = this.min; g.length < c;)g.length % 2 || f ===
                            l ? g.push(M(g[g.length - 1] + a)) : g.unshift(M(g[0] - a)); this.transA *= (d - 1) / (c - 1); this.min = b.startOnTick ? g[0] : Math.min(this.min, g[0]); this.max = b.endOnTick ? g[g.length - 1] : Math.max(this.max, g[g.length - 1])
                    } else d > c && (this.tickInterval *= 2, this.setTickPositions()); if (A(k)) { for (a = b = g.length; a--;)(3 === k && 1 === a % 2 || 2 >= k && 0 < a && a < b - 1) && g.splice(a, 1); this.finalTickAmt = void 0 }
                }
            }; g.prototype.setScale = function () {
                var b, a = !1, g = !1; this.series.forEach(function (b) {
                    var n; a = a || b.isDirtyData || b.isDirty; g = g || (null === (n = b.xAxis) ||
                        void 0 === n ? void 0 : n.isDirty) || !1
                }); this.oldMin = this.min; this.oldMax = this.max; this.oldAxisLength = this.len; this.setAxisSize(); (b = this.len !== this.oldAxisLength) || a || g || this.isLinked || this.forceRedraw || this.userMin !== this.oldUserMin || this.userMax !== this.oldUserMax || this.alignToOthers() ? (this.stacking && this.stacking.resetStacks(), this.forceRedraw = !1, this.getSeriesExtremes(), this.setTickInterval(), this.oldUserMin = this.userMin, this.oldUserMax = this.userMax, this.isDirty || (this.isDirty = b || this.min !== this.oldMin ||
                    this.max !== this.oldMax)) : this.stacking && this.stacking.cleanStacks(); a && this.panningState && (this.panningState.isDirty = !0); u(this, "afterSetScale")
            }; g.prototype.setExtremes = function (b, a, g, c, k) { var n = this, d = n.chart; g = v(g, !0); n.series.forEach(function (b) { delete b.kdTree }); k = r(k, { min: b, max: a }); u(n, "setExtremes", k, function () { n.userMin = b; n.userMax = a; n.eventArgs = k; g && d.redraw(c) }) }; g.prototype.zoom = function (b, a) {
                var g = this, n = this.dataMin, c = this.dataMax, k = this.options, d = Math.min(n, v(k.min, n)), l = Math.max(c,
                    v(k.max, c)); b = { newMin: b, newMax: a }; u(this, "zoom", b, function (b) { var a = b.newMin, k = b.newMax; if (a !== g.min || k !== g.max) g.allowZoomOutside || (A(n) && (a < d && (a = d), a > l && (a = l)), A(c) && (k < d && (k = d), k > l && (k = l))), g.displayBtn = "undefined" !== typeof a || "undefined" !== typeof k, g.setExtremes(a, k, !1, void 0, { trigger: "zoom" }); b.zoomed = !0 }); return b.zoomed
            }; g.prototype.setAxisSize = function () {
                var b = this.chart, a = this.options, g = a.offsets || [0, 0, 0, 0], c = this.horiz, k = this.width = Math.round(E(v(a.width, b.plotWidth - g[3] + g[1]), b.plotWidth)),
                d = this.height = Math.round(E(v(a.height, b.plotHeight - g[0] + g[2]), b.plotHeight)), l = this.top = Math.round(E(v(a.top, b.plotTop + g[0]), b.plotHeight, b.plotTop)); a = this.left = Math.round(E(v(a.left, b.plotLeft + g[3]), b.plotWidth, b.plotLeft)); this.bottom = b.chartHeight - d - l; this.right = b.chartWidth - k - a; this.len = Math.max(c ? k : d, 0); this.pos = c ? a : l
            }; g.prototype.getExtremes = function () {
                var b = this.logarithmic; return {
                    min: b ? M(b.lin2log(this.min)) : this.min, max: b ? M(b.lin2log(this.max)) : this.max, dataMin: this.dataMin, dataMax: this.dataMax,
                    userMin: this.userMin, userMax: this.userMax
                }
            }; g.prototype.getThreshold = function (b) { var a = this.logarithmic, g = a ? a.lin2log(this.min) : this.min; a = a ? a.lin2log(this.max) : this.max; null === b || -Infinity === b ? b = g : Infinity === b ? b = a : g > b ? b = g : a < b && (b = a); return this.translate(b, 0, 1, 0, 1) }; g.prototype.autoLabelAlign = function (b) { var a = (v(b, 0) - 90 * this.side + 720) % 360; b = { align: "center" }; u(this, "autoLabelAlign", b, function (b) { 15 < a && 165 > a ? b.align = "right" : 195 < a && 345 > a && (b.align = "left") }); return b.align }; g.prototype.tickSize = function (b) {
                var a =
                    this.options, g = a["tick" === b ? "tickLength" : "minorTickLength"], c = v(a["tick" === b ? "tickWidth" : "minorTickWidth"], "tick" === b && this.isXAxis && !this.categories ? 1 : 0); if (c && g) { "inside" === a[b + "Position"] && (g = -g); var k = [g, c] } b = { tickSize: k }; u(this, "afterTickSize", b); return b.tickSize
            }; g.prototype.labelMetrics = function () { var b = this.tickPositions && this.tickPositions[0] || 0; return this.chart.renderer.fontMetrics(this.options.labels.style && this.options.labels.style.fontSize, this.ticks[b] && this.ticks[b].label) }; g.prototype.unsquish =
                function () {
                    var b = this.options.labels, a = this.horiz, g = this.tickInterval, c = g, k = this.len / (((this.categories ? 1 : 0) + this.max - this.min) / g), d, l = b.rotation, f = this.labelMetrics(), x, y = Number.MAX_VALUE, h, t = this.max - this.min, F = function (b) { var a = b / (k || 1); a = 1 < a ? Math.ceil(a) : 1; a * g > t && Infinity !== b && Infinity !== k && t && (a = Math.ceil(t / g)); return M(a * g) }; a ? (h = !b.staggerLines && !b.step && (A(l) ? [l] : k < v(b.autoRotationLimit, 80) && b.autoRotation)) && h.forEach(function (b) {
                        if (b === l || b && -90 <= b && 90 >= b) {
                            x = F(Math.abs(f.h / Math.sin(P * b)));
                            var a = x + Math.abs(b / 360); a < y && (y = a, d = b, c = x)
                        }
                    }) : b.step || (c = F(f.h)); this.autoRotation = h; this.labelRotation = v(d, l); return c
                }; g.prototype.getSlotWidth = function (b) {
                    var a, g = this.chart, c = this.horiz, k = this.options.labels, d = Math.max(this.tickPositions.length - (this.categories ? 0 : 1), 1), l = g.margin[3]; if (b && t(b.slotWidth)) return b.slotWidth; if (c && k && 2 > (k.step || 0)) return k.rotation ? 0 : (this.staggerLines || 1) * this.len / d; if (!c) {
                        b = null === (a = null === k || void 0 === k ? void 0 : k.style) || void 0 === a ? void 0 : a.width; if (void 0 !== b) return parseInt(b,
                            10); if (l) return l - g.spacing[3]
                    } return .33 * g.chartWidth
                }; g.prototype.renderUnsquish = function () {
                    var b = this.chart, a = b.renderer, g = this.tickPositions, c = this.ticks, k = this.options.labels, d = k && k.style || {}, f = this.horiz, v = this.getSlotWidth(), x = Math.max(1, Math.round(v - 2 * (k.padding || 5))), y = {}, h = this.labelMetrics(), t = k.style && k.style.textOverflow, F = 0; l(k.rotation) || (y.rotation = k.rotation || 0); g.forEach(function (b) { b = c[b]; b.movedLabel && b.replaceMovedLabel(); b && b.label && b.label.textPxLength > F && (F = b.label.textPxLength) });
                    this.maxLabelLength = F; if (this.autoRotation) F > x && F > h.h ? y.rotation = this.labelRotation : this.labelRotation = 0; else if (v) { var E = x; if (!t) { var I = "clip"; for (x = g.length; !f && x--;) { var u = g[x]; if (u = c[u].label) u.styles && "ellipsis" === u.styles.textOverflow ? u.css({ textOverflow: "clip" }) : u.textPxLength > v && u.css({ width: v + "px" }), u.getBBox().height > this.len / g.length - (h.h - h.f) && (u.specificTextOverflow = "ellipsis") } } } y.rotation && (E = F > .5 * b.chartHeight ? .33 * b.chartHeight : F, t || (I = "ellipsis")); if (this.labelAlign = k.align || this.autoLabelAlign(this.labelRotation)) y.align =
                        this.labelAlign; g.forEach(function (b) { var a = (b = c[b]) && b.label, g = d.width, n = {}; a && (a.attr(y), b.shortenLabel ? b.shortenLabel() : E && !g && "nowrap" !== d.whiteSpace && (E < a.textPxLength || "SPAN" === a.element.tagName) ? (n.width = E + "px", t || (n.textOverflow = a.specificTextOverflow || I), a.css(n)) : a.styles && a.styles.width && !n.width && !g && a.css({ width: null }), delete a.specificTextOverflow, b.rotation = y.rotation) }, this); this.tickRotCorr = a.rotCorr(h.b, this.labelRotation || 0, 0 !== this.side)
                }; g.prototype.hasData = function () {
                    return this.series.some(function (b) { return b.hasData() }) ||
                        this.options.showEmpty && A(this.min) && A(this.max)
                }; g.prototype.addTitle = function (b) {
                    var a = this.chart.renderer, g = this.horiz, k = this.opposite, d = this.options.title, l, f = this.chart.styledMode; this.axisTitle || ((l = d.textAlign) || (l = (g ? { low: "left", middle: "center", high: "right" } : { low: k ? "right" : "left", middle: "center", high: k ? "left" : "right" })[d.align]), this.axisTitle = a.text(d.text, 0, 0, d.useHTML).attr({ zIndex: 7, rotation: d.rotation || 0, align: l }).addClass("highcharts-axis-title"), f || this.axisTitle.css(c(d.style)), this.axisTitle.add(this.axisGroup),
                        this.axisTitle.isNew = !0); f || d.style.width || this.isRadial || this.axisTitle.css({ width: this.len + "px" }); this.axisTitle[b ? "show" : "hide"](b)
                }; g.prototype.generateTick = function (b) { var a = this.ticks; a[b] ? a[b].addLabel() : a[b] = new q(this, b) }; g.prototype.getOffset = function () {
                    var b = this, a = b.chart, g = a.renderer, c = b.options, k = b.tickPositions, d = b.ticks, l = b.horiz, f = b.side, y = a.inverted && !b.isZAxis ? [1, 0, 3, 2][f] : f, h, t = 0, F = 0, E = c.title, I = c.labels, r = 0, C = a.axisOffset; a = a.clipOffset; var m = [-1, 1, 1, -1][f], w = c.className, e = b.axisParent;
                    var P = b.hasData(); b.showAxis = h = P || v(c.showEmpty, !0); b.staggerLines = b.horiz && I.staggerLines; b.axisGroup || (b.gridGroup = g.g("grid").attr({ zIndex: c.gridZIndex || 1 }).addClass("highcharts-" + this.coll.toLowerCase() + "-grid " + (w || "")).add(e), b.axisGroup = g.g("axis").attr({ zIndex: c.zIndex || 2 }).addClass("highcharts-" + this.coll.toLowerCase() + " " + (w || "")).add(e), b.labelGroup = g.g("axis-labels").attr({ zIndex: I.zIndex || 7 }).addClass("highcharts-" + b.coll.toLowerCase() + "-labels " + (w || "")).add(e)); P || b.isLinked ? (k.forEach(function (a,
                        g) { b.generateTick(a, g) }), b.renderUnsquish(), b.reserveSpaceDefault = 0 === f || 2 === f || { 1: "left", 3: "right" }[f] === b.labelAlign, v(I.reserveSpace, "center" === b.labelAlign ? !0 : null, b.reserveSpaceDefault) && k.forEach(function (b) { r = Math.max(d[b].getLabelSize(), r) }), b.staggerLines && (r *= b.staggerLines), b.labelOffset = r * (b.opposite ? -1 : 1)) : x(d, function (b, a) { b.destroy(); delete d[a] }); if (E && E.text && !1 !== E.enabled && (b.addTitle(h), h && !1 !== E.reserveSpace)) {
                        b.titleOffset = t = b.axisTitle.getBBox()[l ? "height" : "width"]; var p = E.offset;
                            F = A(p) ? 0 : v(E.margin, l ? 5 : 10)
                        } b.renderLine(); b.offset = m * v(c.offset, C[f] ? C[f] + (c.margin || 0) : 0); b.tickRotCorr = b.tickRotCorr || { x: 0, y: 0 }; g = 0 === f ? -b.labelMetrics().h : 2 === f ? b.tickRotCorr.y : 0; F = Math.abs(r) + F; r && (F = F - g + m * (l ? v(I.y, b.tickRotCorr.y + 8 * m) : I.x)); b.axisTitleMargin = v(p, F); b.getMaxLabelDimensions && (b.maxLabelDimensions = b.getMaxLabelDimensions(d, k)); l = this.tickSize("tick"); C[f] = Math.max(C[f], b.axisTitleMargin + t + m * b.offset, F, k && k.length && l ? l[0] + m * b.offset : 0); c = c.offset ? 0 : 2 * Math.floor(b.axisLine.strokeWidth() /
                            2); a[y] = Math.max(a[y], c); u(this, "afterGetOffset")
                }; g.prototype.getLinePath = function (b) { var a = this.chart, g = this.opposite, c = this.offset, k = this.horiz, d = this.left + (g ? this.width : 0) + c; c = a.chartHeight - this.bottom - (g ? this.height : 0) + c; g && (b *= -1); return a.renderer.crispLine([["M", k ? this.left : d, k ? c : this.top], ["L", k ? a.chartWidth - this.right : d, k ? c : a.chartHeight - this.bottom]], b) }; g.prototype.renderLine = function () {
                this.axisLine || (this.axisLine = this.chart.renderer.path().addClass("highcharts-axis-line").add(this.axisGroup),
                    this.chart.styledMode || this.axisLine.attr({ stroke: this.options.lineColor, "stroke-width": this.options.lineWidth, zIndex: 7 }))
                }; g.prototype.getTitlePosition = function () {
                    var b = this.horiz, a = this.left, g = this.top, c = this.len, k = this.options.title, d = b ? a : g, l = this.opposite, f = this.offset, v = k.x || 0, x = k.y || 0, y = this.axisTitle, h = this.chart.renderer.fontMetrics(k.style && k.style.fontSize, y); y = Math.max(y.getBBox(null, 0).height - h.h - 1, 0); c = { low: d + (b ? 0 : c), middle: d + c / 2, high: d + (b ? c : 0) }[k.align]; a = (b ? g + this.height : a) + (b ? 1 : -1) *
                        (l ? -1 : 1) * this.axisTitleMargin + [-y, y, h.f, -y][this.side]; b = { x: b ? c + v : a + (l ? this.width : 0) + f + v, y: b ? a + x - (l ? this.height : 0) + f : c + x }; u(this, "afterGetTitlePosition", { titlePosition: b }); return b
                }; g.prototype.renderMinorTick = function (b) { var a = this.chart.hasRendered && t(this.oldMin), g = this.minorTicks; g[b] || (g[b] = new q(this, b, "minor")); a && g[b].isNew && g[b].render(null, !0); g[b].render(null, !1, 1) }; g.prototype.renderTick = function (b, a) {
                    var g = this.isLinked, c = this.ticks, n = this.chart.hasRendered && t(this.oldMin); if (!g || b >=
                        this.min && b <= this.max) c[b] || (c[b] = new q(this, b)), n && c[b].isNew && c[b].render(a, !0, -1), c[b].render(a)
                }; g.prototype.render = function () {
                    var b = this, a = b.chart, g = b.logarithmic, c = b.options, k = b.isLinked, d = b.tickPositions, l = b.axisTitle, f = b.ticks, v = b.minorTicks, h = b.alternateBands, F = c.stackLabels, E = c.alternateGridColor, I = b.tickmarkOffset, r = b.axisLine, C = b.showAxis, m = J(a.renderer.globalAnimation), w, P; b.labelEdge.length = 0; b.overlap = !1;[f, v, h].forEach(function (b) { x(b, function (b) { b.isActive = !1 }) }); if (b.hasData() ||
                        k) b.minorTickInterval && !b.categories && b.getMinorTickPositions().forEach(function (a) { b.renderMinorTick(a) }), d.length && (d.forEach(function (a, g) { b.renderTick(a, g) }), I && (0 === b.min || b.single) && (f[-1] || (f[-1] = new q(b, -1, null, !0)), f[-1].render(-1))), E && d.forEach(function (c, n) {
                            P = "undefined" !== typeof d[n + 1] ? d[n + 1] + I : b.max - I; 0 === n % 2 && c < b.max && P <= b.max + (a.polar ? -I : I) && (h[c] || (h[c] = new e.PlotLineOrBand(b)), w = c + I, h[c].options = { from: g ? g.lin2log(w) : w, to: g ? g.lin2log(P) : P, color: E, className: "highcharts-alternate-grid" },
                                h[c].render(), h[c].isActive = !0)
                        }), b._addedPlotLB || ((c.plotLines || []).concat(c.plotBands || []).forEach(function (a) { b.addPlotBandOrLine(a) }), b._addedPlotLB = !0);[f, v, h].forEach(function (b) { var g, c = [], n = m.duration; x(b, function (b, a) { b.isActive || (b.render(a, !1, 0), b.isActive = !1, c.push(a)) }); y(function () { for (g = c.length; g--;)b[c[g]] && !b[c[g]].isActive && (b[c[g]].destroy(), delete b[c[g]]) }, b !== h && a.hasRendered && n ? n : 0) }); r && (r[r.isPlaced ? "animate" : "attr"]({ d: this.getLinePath(r.strokeWidth()) }), r.isPlaced = !0, r[C ?
                            "show" : "hide"](C)); l && C && (c = b.getTitlePosition(), t(c.y) ? (l[l.isNew ? "attr" : "animate"](c), l.isNew = !1) : (l.attr("y", -9999), l.isNew = !0)); F && F.enabled && b.stacking && b.stacking.renderStackTotals(); b.isDirty = !1; u(this, "afterRender")
                }; g.prototype.redraw = function () { this.visible && (this.render(), this.plotLinesAndBands.forEach(function (b) { b.render() })); this.series.forEach(function (b) { b.isDirty = !0 }) }; g.prototype.getKeepProps = function () { return this.keepProps || g.keepProps }; g.prototype.destroy = function (b) {
                    var a = this,
                    g = a.plotLinesAndBands, c; u(this, "destroy", { keepEvents: b }); b || F(a);[a.ticks, a.minorTicks, a.alternateBands].forEach(function (a) { K(a) }); if (g) for (b = g.length; b--;)g[b].destroy(); "axisLine axisTitle axisGroup gridGroup labelGroup cross scrollbar".split(" ").forEach(function (b) { a[b] && (a[b] = a[b].destroy()) }); for (c in a.plotLinesAndBandsGroups) a.plotLinesAndBandsGroups[c] = a.plotLinesAndBandsGroups[c].destroy(); x(a, function (b, g) { -1 === a.getKeepProps().indexOf(g) && delete a[g] })
                }; g.prototype.drawCrosshair = function (a,
                    g) {
                        var b = this.crosshair, c = v(b.snap, !0), n, k = this.cross, d = this.chart; u(this, "drawCrosshair", { e: a, point: g }); a || (a = this.cross && this.cross.e); if (this.crosshair && !1 !== (A(g) || !c)) {
                            c ? A(g) && (n = v("colorAxis" !== this.coll ? g.crosshairPos : null, this.isXAxis ? g.plotX : this.len - g.plotY)) : n = a && (this.horiz ? a.chartX - this.pos : this.len - a.chartY + this.pos); if (A(n)) {
                                var l = { value: g && (this.isXAxis ? g.x : v(g.stackY, g.y)), translatedValue: n }; d.polar && r(l, { isCrosshair: !0, chartX: a && a.chartX, chartY: a && a.chartY, point: g }); l = this.getPlotLinePath(l) ||
                                    null
                            } if (!A(l)) { this.hideCrosshair(); return } c = this.categories && !this.isRadial; k || (this.cross = k = d.renderer.path().addClass("highcharts-crosshair highcharts-crosshair-" + (c ? "category " : "thin ") + b.className).attr({ zIndex: v(b.zIndex, 2) }).add(), d.styledMode || (k.attr({ stroke: b.color || (c ? p.parse("#ccd6eb").setOpacity(.25).get() : "#cccccc"), "stroke-width": v(b.width, 1) }).css({ "pointer-events": "none" }), b.dashStyle && k.attr({ dashstyle: b.dashStyle }))); k.show().attr({ d: l }); c && !b.width && k.attr({ "stroke-width": this.transA });
                            this.cross.e = a
                        } else this.hideCrosshair(); u(this, "afterDrawCrosshair", { e: a, point: g })
                }; g.prototype.hideCrosshair = function () { this.cross && this.cross.hide(); u(this, "afterHideCrosshair") }; g.prototype.hasVerticalPanning = function () { var a, g; return /y/.test((null === (g = null === (a = this.chart.options.chart) || void 0 === a ? void 0 : a.panning) || void 0 === g ? void 0 : g.type) || "") }; g.defaultOptions = {
                    dateTimeLabelFormats: {
                        millisecond: { main: "%H:%M:%S.%L", range: !1 }, second: { main: "%H:%M:%S", range: !1 }, minute: { main: "%H:%M", range: !1 },
                        hour: { main: "%H:%M", range: !1 }, day: { main: "%e. %b" }, week: { main: "%e. %b" }, month: { main: "%b '%y" }, year: { main: "%Y" }
                    }, endOnTick: !1, labels: { enabled: !0, indentation: 10, x: 0, style: { color: "#666666", cursor: "default", fontSize: "11px" } }, maxPadding: .01, minorTickLength: 2, minorTickPosition: "outside", minPadding: .01, showEmpty: !0, startOfWeek: 1, startOnTick: !1, tickLength: 10, tickPixelInterval: 100, tickmarkPlacement: "between", tickPosition: "outside", title: { align: "middle", style: { color: "#666666" } }, type: "linear", minorGridLineColor: "#f2f2f2",
                    minorGridLineWidth: 1, minorTickColor: "#999999", lineColor: "#ccd6eb", lineWidth: 1, gridLineColor: "#e6e6e6", tickColor: "#ccd6eb"
                }; g.defaultYAxisOptions = {
                    endOnTick: !0, maxPadding: .05, minPadding: .05, tickPixelInterval: 72, showLastLabel: !0, labels: { x: -8 }, startOnTick: !0, title: { rotation: 270, text: "Values" }, stackLabels: { allowOverlap: !1, enabled: !1, crop: !0, overflow: "justify", formatter: function () { var a = this.axis.chart.numberFormatter; return a(this.total, -1) }, style: { color: "#000000", fontSize: "11px", fontWeight: "bold", textOutline: "1px contrast" } },
                    gridLineWidth: 1, lineWidth: 0
                }; g.defaultLeftAxisOptions = { labels: { x: -15 }, title: { rotation: 270 } }; g.defaultRightAxisOptions = { labels: { x: 15 }, title: { rotation: 90 } }; g.defaultBottomAxisOptions = { labels: { autoRotation: [-45], x: 0 }, margin: 15, title: { rotation: 0 } }; g.defaultTopAxisOptions = { labels: { autoRotation: [-45], x: 0 }, margin: 15, title: { rotation: 0 } }; g.keepProps = "extKey hcEvents names series userMax userMin".split(" "); return g
        }(); e.Axis = B; return e.Axis
    }); O(q, "parts/DateTimeAxis.js", [q["parts/Axis.js"], q["parts/Utilities.js"]],
        function (p, e) {
            var q = e.addEvent, B = e.getMagnitude, D = e.normalizeTickInterval, z = e.timeUnits, J = function () {
                function e(m) { this.axis = m } e.prototype.normalizeTimeTickInterval = function (m, e) {
                    var p = e || [["millisecond", [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], ["second", [1, 2, 5, 10, 15, 30]], ["minute", [1, 2, 5, 10, 15, 30]], ["hour", [1, 2, 3, 4, 6, 8, 12]], ["day", [1, 2]], ["week", [1, 2]], ["month", [1, 2, 3, 4, 6]], ["year", null]]; e = p[p.length - 1]; var A = z[e[0]], q = e[1], w; for (w = 0; w < p.length && !(e = p[w], A = z[e[0]], q = e[1], p[w + 1] && m <= (A * q[q.length - 1] + z[p[w +
                        1][0]]) / 2); w++); A === z.year && m < 5 * A && (q = [1, 2, 5]); m = D(m / A, q, "year" === e[0] ? Math.max(B(m / A), 1) : 1); return { unitRange: A, count: m, unitName: e[0] }
                }; return e
            }(); e = function () { function e() { } e.compose = function (m) { m.keepProps.push("dateTime"); m.prototype.getTimeTicks = function () { return this.chart.time.getTimeTicks.apply(this.chart.time, arguments) }; q(m, "init", function (m) { "datetime" !== m.userOptions.type ? this.dateTime = void 0 : this.dateTime || (this.dateTime = new J(this)) }) }; e.AdditionsClass = J; return e }(); e.compose(p); return e
        });
    O(q, "parts/LogarithmicAxis.js", [q["parts/Axis.js"], q["parts/Utilities.js"]], function (p, e) {
        var q = e.addEvent, B = e.getMagnitude, D = e.normalizeTickInterval, z = e.pick, J = function () {
            function e(m) { this.axis = m } e.prototype.getLogTickPositions = function (m, e, p, A) {
                var q = this.axis, w = q.len, r = q.options, u = []; A || (this.minorAutoInterval = void 0); if (.5 <= m) m = Math.round(m), u = q.getLinearTickPositions(m, e, p); else if (.08 <= m) {
                    r = Math.floor(e); var C, h; for (w = .3 < m ? [1, 2, 4] : .15 < m ? [1, 2, 4, 6, 8] : [1, 2, 3, 4, 5, 6, 7, 8, 9]; r < p + 1 && !h; r++) {
                        var f =
                            w.length; for (C = 0; C < f && !h; C++) { var d = this.log2lin(this.lin2log(r) * w[C]); d > e && (!A || t <= p) && "undefined" !== typeof t && u.push(t); t > p && (h = !0); var t = d }
                    }
                } else e = this.lin2log(e), p = this.lin2log(p), m = A ? q.getMinorTickInterval() : r.tickInterval, m = z("auto" === m ? null : m, this.minorAutoInterval, r.tickPixelInterval / (A ? 5 : 1) * (p - e) / ((A ? w / q.tickPositions.length : w) || 1)), m = D(m, void 0, B(m)), u = q.getLinearTickPositions(m, e, p).map(this.log2lin), A || (this.minorAutoInterval = m / 5); A || (q.tickInterval = m); return u
            }; e.prototype.lin2log = function (m) {
                return Math.pow(10,
                    m)
            }; e.prototype.log2lin = function (m) { return Math.log(m) / Math.LN10 }; return e
        }(); e = function () {
            function e() { } e.compose = function (m) {
                m.keepProps.push("logarithmic"); var e = m.prototype, p = J.prototype; e.log2lin = p.log2lin; e.lin2log = p.lin2log; q(m, "init", function (m) { var e = this.logarithmic; "logarithmic" !== m.userOptions.type ? this.logarithmic = void 0 : (e || (e = this.logarithmic = new J(this)), this.log2lin !== e.log2lin && (e.log2lin = this.log2lin.bind(this)), this.lin2log !== e.lin2log && (e.lin2log = this.lin2log.bind(this))) }); q(m,
                    "afterInit", function () { var m = this.logarithmic; m && (this.lin2val = function (e) { return m.lin2log(e) }, this.val2lin = function (e) { return m.log2lin(e) }) })
            }; return e
        }(); e.compose(p); return e
    }); O(q, "parts/PlotLineOrBand.js", [q["parts/Axis.js"], q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e, q) {
        var B = q.arrayMax, D = q.arrayMin, z = q.defined, J = q.destroyObjectProperties, G = q.erase, m = q.extend, H = q.merge, M = q.objectEach, A = q.pick, K = function () {
            function m(r, u) { this.axis = r; u && (this.options = u, this.id = u.id) } m.prototype.render =
                function () {
                    e.fireEvent(this, "render"); var r = this, u = r.axis, m = u.horiz, h = u.logarithmic, f = r.options, d = f.label, t = r.label, l = f.to, c = f.from, a = f.value, x = z(c) && z(l), v = z(a), E = r.svgElem, F = !E, k = [], y = f.color, I = A(f.zIndex, 0), w = f.events; k = { "class": "highcharts-plot-" + (x ? "band " : "line ") + (f.className || "") }; var g = {}, b = u.chart.renderer, n = x ? "bands" : "lines"; h && (c = h.log2lin(c), l = h.log2lin(l), a = h.log2lin(a)); u.chart.styledMode || (v ? (k.stroke = y || "#999999", k["stroke-width"] = A(f.width, 1), f.dashStyle && (k.dashstyle = f.dashStyle)) :
                        x && (k.fill = y || "#e6ebf5", f.borderWidth && (k.stroke = f.borderColor, k["stroke-width"] = f.borderWidth))); g.zIndex = I; n += "-" + I; (h = u.plotLinesAndBandsGroups[n]) || (u.plotLinesAndBandsGroups[n] = h = b.g("plot-" + n).attr(g).add()); F && (r.svgElem = E = b.path().attr(k).add(h)); if (v) k = u.getPlotLinePath({ value: a, lineWidth: E.strokeWidth(), acrossPanes: f.acrossPanes }); else if (x) k = u.getPlotBandPath(c, l, f); else return; !r.eventsAdded && w && (M(w, function (a, b) { E.on(b, function (a) { w[b].apply(r, [a]) }) }), r.eventsAdded = !0); (F || !E.d) && k &&
                            k.length ? E.attr({ d: k }) : E && (k ? (E.show(!0), E.animate({ d: k })) : E.d && (E.hide(), t && (r.label = t = t.destroy()))); d && (z(d.text) || z(d.formatter)) && k && k.length && 0 < u.width && 0 < u.height && !k.isFlat ? (d = H({ align: m && x && "center", x: m ? !x && 4 : 10, verticalAlign: !m && x && "middle", y: m ? x ? 16 : 10 : x ? 6 : -4, rotation: m && !x && 90 }, d), this.renderLabel(d, k, x, I)) : t && t.hide(); return r
                }; m.prototype.renderLabel = function (r, u, m, h) {
                    var f = this.label, d = this.axis.chart.renderer; f || (f = {
                        align: r.textAlign || r.align, rotation: r.rotation, "class": "highcharts-plot-" +
                            (m ? "band" : "line") + "-label " + (r.className || "")
                    }, f.zIndex = h, h = this.getLabelText(r), this.label = f = d.text(h, 0, 0, r.useHTML).attr(f).add(), this.axis.chart.styledMode || f.css(r.style)); d = u.xBounds || [u[0][1], u[1][1], m ? u[2][1] : u[0][1]]; u = u.yBounds || [u[0][2], u[1][2], m ? u[2][2] : u[0][2]]; m = D(d); h = D(u); f.align(r, !1, { x: m, y: h, width: B(d) - m, height: B(u) - h }); f.show(!0)
                }; m.prototype.getLabelText = function (r) { return z(r.formatter) ? r.formatter.call(this) : r.text }; m.prototype.destroy = function () {
                    G(this.axis.plotLinesAndBands,
                        this); delete this.axis; J(this)
                }; return m
        }(); m(p.prototype, {
            getPlotBandPath: function (m, r) {
                var u = this.getPlotLinePath({ value: r, force: !0, acrossPanes: this.options.acrossPanes }), e = this.getPlotLinePath({ value: m, force: !0, acrossPanes: this.options.acrossPanes }), h = [], f = this.horiz, d = 1; m = m < this.min && r < this.min || m > this.max && r > this.max; if (e && u) {
                    if (m) { var t = e.toString() === u.toString(); d = 0 } for (m = 0; m < e.length; m += 2) {
                        r = e[m]; var l = e[m + 1], c = u[m], a = u[m + 1]; "M" !== r[0] && "L" !== r[0] || "M" !== l[0] && "L" !== l[0] || "M" !== c[0] && "L" !==
                            c[0] || "M" !== a[0] && "L" !== a[0] || (f && c[1] === r[1] ? (c[1] += d, a[1] += d) : f || c[2] !== r[2] || (c[2] += d, a[2] += d), h.push(["M", r[1], r[2]], ["L", l[1], l[2]], ["L", a[1], a[2]], ["L", c[1], c[2]], ["Z"])); h.isFlat = t
                    }
                } return h
            }, addPlotBand: function (m) { return this.addPlotBandOrLine(m, "plotBands") }, addPlotLine: function (m) { return this.addPlotBandOrLine(m, "plotLines") }, addPlotBandOrLine: function (m, r) {
                var u = (new K(this, m)).render(), e = this.userOptions; if (u) {
                    if (r) { var h = e[r] || []; h.push(m); e[r] = h } this.plotLinesAndBands.push(u); this._addedPlotLB =
                        !0
                } return u
            }, removePlotBandOrLine: function (m) { for (var r = this.plotLinesAndBands, u = this.options, e = this.userOptions, h = r.length; h--;)r[h].id === m && r[h].destroy();[u.plotLines || [], e.plotLines || [], u.plotBands || [], e.plotBands || []].forEach(function (f) { for (h = f.length; h--;)(f[h] || {}).id === m && G(f, f[h]) }) }, removePlotBand: function (m) { this.removePlotBandOrLine(m) }, removePlotLine: function (m) { this.removePlotBandOrLine(m) }
        }); e.PlotLineOrBand = K; return e.PlotLineOrBand
    }); O(q, "parts/Tooltip.js", [q["parts/Globals.js"],
    q["parts/Utilities.js"]], function (p, e) {
        var q = p.doc, B = e.clamp, D = e.css, z = e.defined, J = e.discardElement, G = e.extend, m = e.fireEvent, H = e.format, M = e.isNumber, A = e.isString, K = e.merge, w = e.pick, r = e.splat, u = e.syncTimeout, C = e.timeUnits; ""; var h = function () {
            function f(d, f) { this.container = void 0; this.crosshairs = []; this.distance = 0; this.isHidden = !0; this.isSticky = !1; this.now = {}; this.options = {}; this.outside = !1; this.chart = d; this.init(d, f) } f.prototype.applyFilter = function () {
                var d = this.chart; d.renderer.definition({
                    tagName: "filter",
                    id: "drop-shadow-" + d.index, opacity: .5, children: [{ tagName: "feGaussianBlur", "in": "SourceAlpha", stdDeviation: 1 }, { tagName: "feOffset", dx: 1, dy: 1 }, { tagName: "feComponentTransfer", children: [{ tagName: "feFuncA", type: "linear", slope: .3 }] }, { tagName: "feMerge", children: [{ tagName: "feMergeNode" }, { tagName: "feMergeNode", "in": "SourceGraphic" }] }]
                }); d.renderer.definition({ tagName: "style", textContent: ".highcharts-tooltip-" + d.index + "{filter:url(#drop-shadow-" + d.index + ")}" })
            }; f.prototype.bodyFormatter = function (d) {
                return d.map(function (d) {
                    var l =
                        d.series.tooltipOptions; return (l[(d.point.formatPrefix || "point") + "Formatter"] || d.point.tooltipFormatter).call(d.point, l[(d.point.formatPrefix || "point") + "Format"] || "")
                })
            }; f.prototype.cleanSplit = function (d) { this.chart.series.forEach(function (f) { var l = f && f.tt; l && (!l.isActive || d ? f.tt = l.destroy() : l.isActive = !1) }) }; f.prototype.defaultFormatter = function (d) {
                var f = this.points || r(this); var l = [d.tooltipFooterHeaderFormatter(f[0])]; l = l.concat(d.bodyFormatter(f)); l.push(d.tooltipFooterHeaderFormatter(f[0], !0));
                return l
            }; f.prototype.destroy = function () { this.label && (this.label = this.label.destroy()); this.split && this.tt && (this.cleanSplit(this.chart, !0), this.tt = this.tt.destroy()); this.renderer && (this.renderer = this.renderer.destroy(), J(this.container)); e.clearTimeout(this.hideTimer); e.clearTimeout(this.tooltipTimeout) }; f.prototype.getAnchor = function (d, f) {
                var l = this.chart, c = l.pointer, a = l.inverted, x = l.plotTop, v = l.plotLeft, h = 0, t = 0, k, y; d = r(d); this.followPointer && f ? ("undefined" === typeof f.chartX && (f = c.normalize(f)),
                    d = [f.chartX - v, f.chartY - x]) : d[0].tooltipPos ? d = d[0].tooltipPos : (d.forEach(function (c) { k = c.series.yAxis; y = c.series.xAxis; h += c.plotX + (!a && y ? y.left - v : 0); t += (c.plotLow ? (c.plotLow + c.plotHigh) / 2 : c.plotY) + (!a && k ? k.top - x : 0) }), h /= d.length, t /= d.length, d = [a ? l.plotWidth - t : h, this.shared && !a && 1 < d.length && f ? f.chartY - x : a ? l.plotHeight - h : t]); return d.map(Math.round)
            }; f.prototype.getDateFormat = function (d, f, l, c) {
                var a = this.chart.time, x = a.dateFormat("%m-%d %H:%M:%S.%L", f), v = { millisecond: 15, second: 12, minute: 9, hour: 6, day: 3 },
                h = "millisecond"; for (t in C) { if (d === C.week && +a.dateFormat("%w", f) === l && "00:00:00.000" === x.substr(6)) { var t = "week"; break } if (C[t] > d) { t = h; break } if (v[t] && x.substr(v[t]) !== "01-01 00:00:00.000".substr(v[t])) break; "week" !== t && (h = t) } if (t) var k = a.resolveDTLFormat(c[t]).main; return k
            }; f.prototype.getLabel = function () {
                var d, f, l = this, c = this.chart.renderer, a = this.chart.styledMode, x = this.options, v = "tooltip" + (z(x.className) ? " " + x.className : ""), h = (null === (d = x.style) || void 0 === d ? void 0 : d.pointerEvents) || (!this.followPointer &&
                    x.stickOnContact ? "auto" : "none"), F; d = function () { l.inContact = !0 }; var k = function () { var a = l.chart.hoverSeries; l.inContact = !1; if (a && a.onMouseOut) a.onMouseOut() }; if (!this.label) {
                    this.outside && (this.container = F = p.doc.createElement("div"), F.className = "highcharts-tooltip-container", D(F, { position: "absolute", top: "1px", pointerEvents: h, zIndex: 3 }), p.doc.body.appendChild(F), this.renderer = c = new p.Renderer(F, 0, 0, null === (f = this.chart.options.chart) || void 0 === f ? void 0 : f.style, void 0, void 0, c.styledMode)); this.split ?
                        this.label = c.g(v) : (this.label = c.label("", 0, 0, x.shape || "callout", null, null, x.useHTML, null, v).attr({ padding: x.padding, r: x.borderRadius }), a || this.label.attr({ fill: x.backgroundColor, "stroke-width": x.borderWidth }).css(x.style).css({ pointerEvents: h }).shadow(x.shadow)); a && (this.applyFilter(), this.label.addClass("highcharts-tooltip-" + this.chart.index)); if (l.outside && !l.split) {
                            var y = this.label, I = y.xSetter, u = y.ySetter; y.xSetter = function (a) { I.call(y, l.distance); F.style.left = a + "px" }; y.ySetter = function (a) {
                                u.call(y,
                                    l.distance); F.style.top = a + "px"
                            }
                        } this.label.on("mouseenter", d).on("mouseleave", k).attr({ zIndex: 8 }).add()
                    } return this.label
            }; f.prototype.getPosition = function (d, f, l) {
                var c = this.chart, a = this.distance, x = {}, v = c.inverted && l.h || 0, h, t = this.outside, k = t ? q.documentElement.clientWidth - 2 * a : c.chartWidth, y = t ? Math.max(q.body.scrollHeight, q.documentElement.scrollHeight, q.body.offsetHeight, q.documentElement.offsetHeight, q.documentElement.clientHeight) : c.chartHeight, I = c.pointer.getChartPosition(), u = c.containerScaling,
                g = function (a) { return u ? a * u.scaleX : a }, b = function (a) { return u ? a * u.scaleY : a }, n = function (n) { var v = "x" === n; return [n, v ? k : y, v ? d : f].concat(t ? [v ? g(d) : b(f), v ? I.left - a + g(l.plotX + c.plotLeft) : I.top - a + b(l.plotY + c.plotTop), 0, v ? k : y] : [v ? d : f, v ? l.plotX + c.plotLeft : l.plotY + c.plotTop, v ? c.plotLeft : c.plotTop, v ? c.plotLeft + c.plotWidth : c.plotTop + c.plotHeight]) }, L = n("y"), r = n("x"), m = !this.followPointer && w(l.ttBelow, !c.inverted === !!l.negative), e = function (c, n, k, d, l, f, h) {
                    var y = "y" === c ? b(a) : g(a), t = (k - d) / 2, F = d < l - a, E = l + a + d < n, I = l - y -
                        k + t; l = l + y - t; if (m && E) x[c] = l; else if (!m && F) x[c] = I; else if (F) x[c] = Math.min(h - d, 0 > I - v ? I : I - v); else if (E) x[c] = Math.max(f, l + v + k > n ? l : l + v); else return !1
                }, C = function (b, g, c, n, k) { var d; k < a || k > g - a ? d = !1 : x[b] = k < c / 2 ? 1 : k > g - n / 2 ? g - n - 2 : k - c / 2; return d }, p = function (a) { var b = L; L = r; r = b; h = a }, A = function () { !1 !== e.apply(0, L) ? !1 !== C.apply(0, r) || h || (p(!0), A()) : h ? x.x = x.y = 0 : (p(!0), A()) }; (c.inverted || 1 < this.len) && p(); A(); return x
            }; f.prototype.getXDateFormat = function (d, f, l) {
                f = f.dateTimeLabelFormats; var c = l && l.closestPointRange; return (c ?
                    this.getDateFormat(c, d.x, l.options.startOfWeek, f) : f.day) || f.year
            }; f.prototype.hide = function (d) { var f = this; e.clearTimeout(this.hideTimer); d = w(d, this.options.hideDelay, 500); this.isHidden || (this.hideTimer = u(function () { f.getLabel().fadeOut(d ? void 0 : d); f.isHidden = !0 }, d)) }; f.prototype.init = function (d, f) {
            this.chart = d; this.options = f; this.crosshairs = []; this.now = { x: 0, y: 0 }; this.isHidden = !0; this.split = f.split && !d.inverted && !d.polar; this.shared = f.shared || this.split; this.outside = w(f.outside, !(!d.scrollablePixelsX &&
                !d.scrollablePixelsY))
            }; f.prototype.isStickyOnContact = function () { return !(this.followPointer || !this.options.stickOnContact || !this.inContact) }; f.prototype.move = function (d, f, l, c) {
                var a = this, x = a.now, v = !1 !== a.options.animation && !a.isHidden && (1 < Math.abs(d - x.x) || 1 < Math.abs(f - x.y)), h = a.followPointer || 1 < a.len; G(x, { x: v ? (2 * x.x + d) / 3 : d, y: v ? (x.y + f) / 2 : f, anchorX: h ? void 0 : v ? (2 * x.anchorX + l) / 3 : l, anchorY: h ? void 0 : v ? (x.anchorY + c) / 2 : c }); a.getLabel().attr(x); a.drawTracker(); v && (e.clearTimeout(this.tooltipTimeout), this.tooltipTimeout =
                    setTimeout(function () { a && a.move(d, f, l, c) }, 32))
            }; f.prototype.refresh = function (d, f) {
                var l = this.chart, c = this.options, a = d, h = {}, v = [], t = c.formatter || this.defaultFormatter; h = this.shared; var F = l.styledMode; if (c.enabled) {
                    e.clearTimeout(this.hideTimer); this.followPointer = r(a)[0].series.tooltipOptions.followPointer; var k = this.getAnchor(a, f); f = k[0]; var y = k[1]; !h || a.series && a.series.noSharedTooltip ? h = a.getLabelConfig() : (l.pointer.applyInactiveState(a), a.forEach(function (a) { a.setState("hover"); v.push(a.getLabelConfig()) }),
                        h = { x: a[0].category, y: a[0].y }, h.points = v, a = a[0]); this.len = v.length; l = t.call(h, this); t = a.series; this.distance = w(t.tooltipOptions.distance, 16); !1 === l ? this.hide() : (this.split ? this.renderSplit(l, r(d)) : (d = this.getLabel(), c.style.width && !F || d.css({ width: this.chart.spacingBox.width + "px" }), d.attr({ text: l && l.join ? l.join("") : l }), d.removeClass(/highcharts-color-[\d]+/g).addClass("highcharts-color-" + w(a.colorIndex, t.colorIndex)), F || d.attr({ stroke: c.borderColor || a.color || t.color || "#666666" }), this.updatePosition({
                            plotX: f,
                            plotY: y, negative: a.negative, ttBelow: a.ttBelow, h: k[2] || 0
                        })), this.isHidden && this.label && this.label.attr({ opacity: 1 }).show(), this.isHidden = !1); m(this, "refresh")
                }
            }; f.prototype.renderSplit = function (d, f) {
                function l(a, b, g, c, n) { void 0 === n && (n = !0); g ? (b = q ? 0 : z, a = B(a - c / 2, e.left, e.right - c)) : (b -= H, a = n ? a - c - L : a + L, a = B(a, n ? a : e.left, e.right)); return { x: a, y: b } } var c = this, a = c.chart, h = c.chart, v = h.plotHeight, t = h.plotLeft, F = h.plotTop, k = h.pointer, y = h.renderer, I = h.scrollablePixelsY, u = void 0 === I ? 0 : I; I = h.scrollingContainer; I =
                    void 0 === I ? { scrollLeft: 0, scrollTop: 0 } : I; var g = I.scrollLeft, b = I.scrollTop, n = h.styledMode, L = c.distance, r = c.options, m = c.options.positioner, e = { left: g, right: g + h.chartWidth, top: b, bottom: b + h.chartHeight }, C = c.getLabel(), q = !(!a.xAxis[0] || !a.xAxis[0].opposite), H = F + b, K = 0, z = v - u; A(d) && (d = [!1, d]); d = d.slice(0, f.length + 1).reduce(function (a, g, k) {
                        if (!1 !== g && "" !== g) {
                            k = f[k - 1] || { isHeader: !0, plotX: f[0].plotX, plotY: v, series: {} }; var d = k.isHeader, h = d ? c : k.series, x = h.tt, E = k.isHeader; var I = k.series; var N = "highcharts-color-" +
                                w(k.colorIndex, I.colorIndex, "none"); x || (x = { padding: r.padding, r: r.borderRadius }, n || (x.fill = r.backgroundColor, x["stroke-width"] = r.borderWidth), x = y.label("", 0, 0, r[E ? "headerShape" : "shape"] || "callout", void 0, void 0, r.useHTML).addClass((E ? "highcharts-tooltip-header " : "") + "highcharts-tooltip-box " + N).attr(x).add(C)); x.isActive = !0; x.attr({ text: g }); n || x.css(r.style).shadow(r.shadow).attr({ stroke: r.borderColor || k.color || I.color || "#333333" }); g = h.tt = x; E = g.getBBox(); h = E.width + g.strokeWidth(); d && (K = E.height, z +=
                                    K, q && (H -= K)); I = k.plotX; I = void 0 === I ? 0 : I; N = k.plotY; N = void 0 === N ? 0 : N; var P = k.series; if (k.isHeader) { I = t + I; var p = F + v / 2 } else x = P.xAxis, P = P.yAxis, I = x.pos + B(I, -L, x.len + L), P.pos + N >= b + F && P.pos + N <= b + F + v - u && (p = P.pos + N); I = B(I, e.left - L, e.right + L); "number" === typeof p ? (E = E.height + 1, N = m ? m.call(c, h, E, k) : l(I, p, d, h), a.push({ align: m ? 0 : void 0, anchorX: I, anchorY: p, boxWidth: h, point: k, rank: w(N.rank, d ? 1 : 0), size: E, target: N.y, tt: g, x: N.x })) : g.isActive = !1
                        } return a
                    }, []); !m && d.some(function (a) { return a.x < e.left }) && (d = d.map(function (a) {
                        var b =
                            l(a.anchorX, a.anchorY, a.point.isHeader, a.boxWidth, !1); return G(a, { target: b.y, x: b.x })
                    })); c.cleanSplit(); p.distribute(d, z); d.forEach(function (a) { var b = a.pos; a.tt.attr({ visibility: "undefined" === typeof b ? "hidden" : "inherit", x: a.x, y: b + H, anchorX: a.anchorX, anchorY: a.anchorY }) }); d = c.container; a = c.renderer; c.outside && d && a && (h = C.getBBox(), a.setSize(h.width + h.x, h.height + h.y, !1), k = k.getChartPosition(), d.style.left = k.left + "px", d.style.top = k.top + "px")
            }; f.prototype.drawTracker = function () {
                if (this.followPointer || !this.options.stickOnContact) this.tracker &&
                    this.tracker.destroy(); else {
                        var d = this.chart, f = this.label, l = d.hoverPoint; if (f && l) {
                            var c = { x: 0, y: 0, width: 0, height: 0 }; l = this.getAnchor(l); var a = f.getBBox(); l[0] += d.plotLeft - f.translateX; l[1] += d.plotTop - f.translateY; c.x = Math.min(0, l[0]); c.y = Math.min(0, l[1]); c.width = 0 > l[0] ? Math.max(Math.abs(l[0]), a.width - l[0]) : Math.max(Math.abs(l[0]), a.width); c.height = 0 > l[1] ? Math.max(Math.abs(l[1]), a.height - Math.abs(l[1])) : Math.max(Math.abs(l[1]), a.height); this.tracker ? this.tracker.attr(c) : (this.tracker = f.renderer.rect(c).addClass("highcharts-tracker").add(f),
                                d.styledMode || this.tracker.attr({ fill: "rgba(0,0,0,0)" }))
                        }
                }
            }; f.prototype.styledModeFormat = function (d) { return d.replace('style="font-size: 10px"', 'class="highcharts-header"').replace(/style="color:{(point|series)\.color}"/g, 'class="highcharts-color-{$1.colorIndex}"') }; f.prototype.tooltipFooterHeaderFormatter = function (d, f) {
                var l = f ? "footer" : "header", c = d.series, a = c.tooltipOptions, h = a.xDateFormat, v = c.xAxis, t = v && "datetime" === v.options.type && M(d.key), F = a[l + "Format"]; f = { isFooter: f, labelConfig: d }; m(this, "headerFormatter",
                    f, function (k) { t && !h && (h = this.getXDateFormat(d, a, v)); t && h && (d.point && d.point.tooltipDateKeys || ["key"]).forEach(function (a) { F = F.replace("{point." + a + "}", "{point." + a + ":" + h + "}") }); c.chart.styledMode && (F = this.styledModeFormat(F)); k.text = H(F, { point: d, series: c }, this.chart) }); return f.text
            }; f.prototype.update = function (d) { this.destroy(); K(!0, this.chart.options.tooltip.userOptions, d); this.init(this.chart, K(!0, this.options, d)) }; f.prototype.updatePosition = function (d) {
                var f = this.chart, l = f.pointer, c = this.getLabel(),
                a = d.plotX + f.plotLeft, h = d.plotY + f.plotTop; l = l.getChartPosition(); d = (this.options.positioner || this.getPosition).call(this, c.width, c.height, d); if (this.outside) { var v = (this.options.borderWidth || 0) + 2 * this.distance; this.renderer.setSize(c.width + v, c.height + v, !1); if (f = f.containerScaling) D(this.container, { transform: "scale(" + f.scaleX + ", " + f.scaleY + ")" }), a *= f.scaleX, h *= f.scaleY; a += l.left - d.x; h += l.top - d.y } this.move(Math.round(d.x), Math.round(d.y || 0), a, h)
            }; return f
        }(); p.Tooltip = h; return p.Tooltip
    }); O(q, "parts/Pointer.js",
        [q["parts/Color.js"], q["parts/Globals.js"], q["parts/Tooltip.js"], q["parts/Utilities.js"]], function (p, e, q, B) {
            var D = p.parse, z = e.charts, J = e.noop, G = B.addEvent, m = B.attr, H = B.css, M = B.defined, A = B.extend, K = B.find, w = B.fireEvent, r = B.isNumber, u = B.isObject, C = B.objectEach, h = B.offset, f = B.pick, d = B.splat; ""; p = function () {
                function t(d, c) { this.lastValidTouch = {}; this.pinchDown = []; this.runChartClick = !1; this.chart = d; this.hasDragged = !1; this.options = c; this.unbindContainerMouseLeave = function () { }; this.init(d, c) } t.prototype.applyInactiveState =
                    function (d) { var c = [], a; (d || []).forEach(function (d) { a = d.series; c.push(a); a.linkedParent && c.push(a.linkedParent); a.linkedSeries && (c = c.concat(a.linkedSeries)); a.navigatorSeries && c.push(a.navigatorSeries) }); this.chart.series.forEach(function (a) { -1 === c.indexOf(a) ? a.setState("inactive", !0) : a.options.inactiveOtherPoints && a.setAllPointsToState("inactive") }) }; t.prototype.destroy = function () {
                        var d = this; "undefined" !== typeof d.unDocMouseMove && d.unDocMouseMove(); this.unbindContainerMouseLeave(); e.chartCount || (e.unbindDocumentMouseUp &&
                            (e.unbindDocumentMouseUp = e.unbindDocumentMouseUp()), e.unbindDocumentTouchEnd && (e.unbindDocumentTouchEnd = e.unbindDocumentTouchEnd())); clearInterval(d.tooltipTimeout); C(d, function (c, a) { d[a] = void 0 })
                    }; t.prototype.drag = function (d) {
                        var c = this.chart, a = c.options.chart, l = d.chartX, f = d.chartY, h = this.zoomHor, t = this.zoomVert, k = c.plotLeft, y = c.plotTop, I = c.plotWidth, r = c.plotHeight, g = this.selectionMarker, b = this.mouseDownX || 0, n = this.mouseDownY || 0, L = u(a.panning) ? a.panning && a.panning.enabled : a.panning, m = a.panKey && d[a.panKey +
                            "Key"]; if (!g || !g.touch) if (l < k ? l = k : l > k + I && (l = k + I), f < y ? f = y : f > y + r && (f = y + r), this.hasDragged = Math.sqrt(Math.pow(b - l, 2) + Math.pow(n - f, 2)), 10 < this.hasDragged) {
                                var e = c.isInsidePlot(b - k, n - y); c.hasCartesianSeries && (this.zoomX || this.zoomY) && e && !m && !g && (this.selectionMarker = g = c.renderer.rect(k, y, h ? 1 : I, t ? 1 : r, 0).attr({ "class": "highcharts-selection-marker", zIndex: 7 }).add(), c.styledMode || g.attr({ fill: a.selectionMarkerFill || D("#335cad").setOpacity(.25).get() })); g && h && (l -= b, g.attr({ width: Math.abs(l), x: (0 < l ? 0 : l) + b }));
                                g && t && (l = f - n, g.attr({ height: Math.abs(l), y: (0 < l ? 0 : l) + n })); e && !g && L && c.pan(d, a.panning)
                            }
                    }; t.prototype.dragStart = function (d) { var c = this.chart; c.mouseIsDown = d.type; c.cancelClick = !1; c.mouseDownX = this.mouseDownX = d.chartX; c.mouseDownY = this.mouseDownY = d.chartY }; t.prototype.drop = function (d) {
                        var c = this, a = this.chart, l = this.hasPinched; if (this.selectionMarker) {
                            var f = { originalEvent: d, xAxis: [], yAxis: [] }, h = this.selectionMarker, t = h.attr ? h.attr("x") : h.x, k = h.attr ? h.attr("y") : h.y, y = h.attr ? h.attr("width") : h.width, I = h.attr ?
                                h.attr("height") : h.height, u; if (this.hasDragged || l) a.axes.forEach(function (a) { if (a.zoomEnabled && M(a.min) && (l || c[{ xAxis: "zoomX", yAxis: "zoomY" }[a.coll]]) && r(t) && r(k)) { var b = a.horiz, g = "touchend" === d.type ? a.minPixelPadding : 0, h = a.toValue((b ? t : k) + g); b = a.toValue((b ? t + y : k + I) - g); f[a.coll].push({ axis: a, min: Math.min(h, b), max: Math.max(h, b) }); u = !0 } }), u && w(a, "selection", f, function (g) { a.zoom(A(g, l ? { animation: !1 } : null)) }); r(a.index) && (this.selectionMarker = this.selectionMarker.destroy()); l && this.scaleGroups()
                        } a && r(a.index) &&
                            (H(a.container, { cursor: a._cursor }), a.cancelClick = 10 < this.hasDragged, a.mouseIsDown = this.hasDragged = this.hasPinched = !1, this.pinchDown = [])
                    }; t.prototype.findNearestKDPoint = function (d, c, a) {
                        var l = this.chart, f = l.hoverPoint; l = l.tooltip; if (f && l && l.isStickyOnContact()) return f; var h; d.forEach(function (d) {
                            var k = !(d.noSharedTooltip && c) && 0 > d.options.findNearestPointBy.indexOf("y"); d = d.searchPoint(a, k); if ((k = u(d, !0)) && !(k = !u(h, !0))) {
                                k = h.distX - d.distX; var l = h.dist - d.dist, f = (d.series.group && d.series.group.zIndex) -
                                    (h.series.group && h.series.group.zIndex); k = 0 < (0 !== k && c ? k : 0 !== l ? l : 0 !== f ? f : h.series.index > d.series.index ? -1 : 1)
                            } k && (h = d)
                        }); return h
                    }; t.prototype.getChartCoordinatesFromPoint = function (d, c) { var a = d.series, l = a.xAxis; a = a.yAxis; var h = f(d.clientX, d.plotX), t = d.shapeArgs; if (l && a) return c ? { chartX: l.len + l.pos - h, chartY: a.len + a.pos - d.plotY } : { chartX: h + l.pos, chartY: d.plotY + a.pos }; if (t && t.x && t.y) return { chartX: t.x, chartY: t.y } }; t.prototype.getChartPosition = function () { return this.chartPosition || (this.chartPosition = h(this.chart.container)) };
                t.prototype.getCoordinates = function (d) { var c = { xAxis: [], yAxis: [] }; this.chart.axes.forEach(function (a) { c[a.isXAxis ? "xAxis" : "yAxis"].push({ axis: a, value: a.toValue(d[a.horiz ? "chartX" : "chartY"]) }) }); return c }; t.prototype.getHoverData = function (d, c, a, h, v, t) {
                    var l, k = []; h = !(!h || !d); var y = c && !c.stickyTracking, x = { chartX: t ? t.chartX : void 0, chartY: t ? t.chartY : void 0, shared: v }; w(this, "beforeGetHoverData", x); y = y ? [c] : a.filter(function (a) {
                        return x.filter ? x.filter(a) : a.visible && !(!v && a.directTouch) && f(a.options.enableMouseTracking,
                            !0) && a.stickyTracking
                    }); c = (l = h || !t ? d : this.findNearestKDPoint(y, v, t)) && l.series; l && (v && !c.noSharedTooltip ? (y = a.filter(function (a) { return x.filter ? x.filter(a) : a.visible && !(!v && a.directTouch) && f(a.options.enableMouseTracking, !0) && !a.noSharedTooltip }), y.forEach(function (a) { var g = K(a.points, function (a) { return a.x === l.x && !a.isNull }); u(g) && (a.chart.isBoosting && (g = a.getPoint(g)), k.push(g)) })) : k.push(l)); x = { hoverPoint: l }; w(this, "afterGetHoverData", x); return { hoverPoint: x.hoverPoint, hoverSeries: c, hoverPoints: k }
                };
                t.prototype.getPointFromEvent = function (d) { d = d.target; for (var c; d && !c;)c = d.point, d = d.parentNode; return c }; t.prototype.onTrackerMouseOut = function (d) { d = d.relatedTarget || d.toElement; var c = this.chart.hoverSeries; this.isDirectTouch = !1; if (!(!c || !d || c.stickyTracking || this.inClass(d, "highcharts-tooltip") || this.inClass(d, "highcharts-series-" + c.index) && this.inClass(d, "highcharts-tracker"))) c.onMouseOut() }; t.prototype.inClass = function (d, c) {
                    for (var a; d;) {
                        if (a = m(d, "class")) {
                            if (-1 !== a.indexOf(c)) return !0; if (-1 !==
                                a.indexOf("highcharts-container")) return !1
                        } d = d.parentNode
                    }
                }; t.prototype.init = function (d, c) { this.options = c; this.chart = d; this.runChartClick = c.chart.events && !!c.chart.events.click; this.pinchDown = []; this.lastValidTouch = {}; q && (d.tooltip = new q(d, c.tooltip), this.followTouchMove = f(c.tooltip.followTouchMove, !0)); this.setDOMEvents() }; t.prototype.normalize = function (d, c) {
                    var a = d.touches, l = a ? a.length ? a.item(0) : f(a.changedTouches, d.changedTouches)[0] : d; c || (c = this.getChartPosition()); a = l.pageX - c.left; c = l.pageY -
                        c.top; if (l = this.chart.containerScaling) a /= l.scaleX, c /= l.scaleY; return A(d, { chartX: Math.round(a), chartY: Math.round(c) })
                }; t.prototype.onContainerClick = function (d) { var c = this.chart, a = c.hoverPoint; d = this.normalize(d); var f = c.plotLeft, l = c.plotTop; c.cancelClick || (a && this.inClass(d.target, "highcharts-tracker") ? (w(a.series, "click", A(d, { point: a })), c.hoverPoint && a.firePointEvent("click", d)) : (A(d, this.getCoordinates(d)), c.isInsidePlot(d.chartX - f, d.chartY - l) && w(c, "click", d))) }; t.prototype.onContainerMouseDown =
                    function (d) { d = this.normalize(d); if (e.isFirefox && 0 !== d.button) this.onContainerMouseMove(d); if ("undefined" === typeof d.button || 1 === ((d.buttons || d.button) & 1)) this.zoomOption(d), this.dragStart(d) }; t.prototype.onContainerMouseLeave = function (d) { var c = z[f(e.hoverChartIndex, -1)], a = this.chart.tooltip; d = this.normalize(d); c && (d.relatedTarget || d.toElement) && (c.pointer.reset(), c.pointer.chartPosition = void 0); a && !a.isHidden && this.reset() }; t.prototype.onContainerMouseMove = function (d) {
                        var c = this.chart; d = this.normalize(d);
                        this.setHoverChartIndex(); d.preventDefault || (d.returnValue = !1); "mousedown" === c.mouseIsDown && this.drag(d); c.openMenu || !this.inClass(d.target, "highcharts-tracker") && !c.isInsidePlot(d.chartX - c.plotLeft, d.chartY - c.plotTop) || this.runPointActions(d)
                    }; t.prototype.onDocumentTouchEnd = function (d) { z[e.hoverChartIndex] && z[e.hoverChartIndex].pointer.drop(d) }; t.prototype.onContainerTouchMove = function (d) { this.touch(d) }; t.prototype.onContainerTouchStart = function (d) { this.zoomOption(d); this.touch(d, !0) }; t.prototype.onDocumentMouseMove =
                        function (d) { var c = this.chart, a = this.chartPosition; d = this.normalize(d, a); var f = c.tooltip; !a || f && f.isStickyOnContact() || c.isInsidePlot(d.chartX - c.plotLeft, d.chartY - c.plotTop) || this.inClass(d.target, "highcharts-tracker") || this.reset() }; t.prototype.onDocumentMouseUp = function (d) { var c = z[f(e.hoverChartIndex, -1)]; c && c.pointer.drop(d) }; t.prototype.pinch = function (d) {
                            var c = this, a = c.chart, l = c.pinchDown, h = d.touches || [], t = h.length, F = c.lastValidTouch, k = c.hasZoom, y = c.selectionMarker, u = {}, r = 1 === t && (c.inClass(d.target,
                                "highcharts-tracker") && a.runTrackerClick || c.runChartClick), g = {}; 1 < t && (c.initiated = !0); k && c.initiated && !r && d.preventDefault();[].map.call(h, function (a) { return c.normalize(a) }); "touchstart" === d.type ? ([].forEach.call(h, function (a, g) { l[g] = { chartX: a.chartX, chartY: a.chartY } }), F.x = [l[0].chartX, l[1] && l[1].chartX], F.y = [l[0].chartY, l[1] && l[1].chartY], a.axes.forEach(function (b) {
                                    if (b.zoomEnabled) {
                                        var g = a.bounds[b.horiz ? "h" : "v"], c = b.minPixelPadding, k = b.toPixels(Math.min(f(b.options.min, b.dataMin), b.dataMin)),
                                        d = b.toPixels(Math.max(f(b.options.max, b.dataMax), b.dataMax)), l = Math.max(k, d); g.min = Math.min(b.pos, Math.min(k, d) - c); g.max = Math.max(b.pos + b.len, l + c)
                                    }
                                }), c.res = !0) : c.followTouchMove && 1 === t ? this.runPointActions(c.normalize(d)) : l.length && (y || (c.selectionMarker = y = A({ destroy: J, touch: !0 }, a.plotBox)), c.pinchTranslate(l, h, u, y, g, F), c.hasPinched = k, c.scaleGroups(u, g), c.res && (c.res = !1, this.reset(!1, 0)))
                        }; t.prototype.pinchTranslate = function (d, c, a, f, h, t) {
                        this.zoomHor && this.pinchTranslateDirection(!0, d, c, a, f, h, t);
                            this.zoomVert && this.pinchTranslateDirection(!1, d, c, a, f, h, t)
                        }; t.prototype.pinchTranslateDirection = function (d, c, a, f, h, t, F, k) {
                            var l = this.chart, v = d ? "x" : "y", x = d ? "X" : "Y", g = "chart" + x, b = d ? "width" : "height", n = l["plot" + (d ? "Left" : "Top")], u, r, m = k || 1, E = l.inverted, e = l.bounds[d ? "h" : "v"], C = 1 === c.length, w = c[0][g], p = a[0][g], A = !C && c[1][g], q = !C && a[1][g]; a = function () { "number" === typeof q && 20 < Math.abs(w - A) && (m = k || Math.abs(p - q) / Math.abs(w - A)); r = (n - p) / m + w; u = l["plot" + (d ? "Width" : "Height")] / m }; a(); c = r; if (c < e.min) {
                                c = e.min; var H =
                                    !0
                            } else c + u > e.max && (c = e.max - u, H = !0); H ? (p -= .8 * (p - F[v][0]), "number" === typeof q && (q -= .8 * (q - F[v][1])), a()) : F[v] = [p, q]; E || (t[v] = r - n, t[b] = u); t = E ? 1 / m : m; h[b] = u; h[v] = c; f[E ? d ? "scaleY" : "scaleX" : "scale" + x] = m; f["translate" + x] = t * n + (p - t * w)
                        }; t.prototype.reset = function (f, c) {
                            var a = this.chart, l = a.hoverSeries, h = a.hoverPoint, t = a.hoverPoints, F = a.tooltip, k = F && F.shared ? t : h; f && k && d(k).forEach(function (a) { a.series.isCartesian && "undefined" === typeof a.plotX && (f = !1) }); if (f) F && k && d(k).length && (F.refresh(k), F.shared && t ? t.forEach(function (a) {
                                a.setState(a.state,
                                    !0); a.series.isCartesian && (a.series.xAxis.crosshair && a.series.xAxis.drawCrosshair(null, a), a.series.yAxis.crosshair && a.series.yAxis.drawCrosshair(null, a))
                            }) : h && (h.setState(h.state, !0), a.axes.forEach(function (a) { a.crosshair && h.series[a.coll] === a && a.drawCrosshair(null, h) }))); else {
                                if (h) h.onMouseOut(); t && t.forEach(function (a) { a.setState() }); if (l) l.onMouseOut(); F && F.hide(c); this.unDocMouseMove && (this.unDocMouseMove = this.unDocMouseMove()); a.axes.forEach(function (a) { a.hideCrosshair() }); this.hoverX = a.hoverPoints =
                                    a.hoverPoint = null
                            }
                        }; t.prototype.runPointActions = function (d, c) {
                            var a = this.chart, l = a.tooltip && a.tooltip.options.enabled ? a.tooltip : void 0, h = l ? l.shared : !1, t = c || a.hoverPoint, F = t && t.series || a.hoverSeries; F = this.getHoverData(t, F, a.series, (!d || "touchmove" !== d.type) && (!!c || F && F.directTouch && this.isDirectTouch), h, d); t = F.hoverPoint; var k = F.hoverPoints; c = (F = F.hoverSeries) && F.tooltipOptions.followPointer; h = h && F && !F.noSharedTooltip; if (t && (t !== a.hoverPoint || l && l.isHidden)) {
                                (a.hoverPoints || []).forEach(function (a) {
                                -1 ===
                                    k.indexOf(a) && a.setState()
                                }); if (a.hoverSeries !== F) F.onMouseOver(); this.applyInactiveState(k); (k || []).forEach(function (a) { a.setState("hover") }); a.hoverPoint && a.hoverPoint.firePointEvent("mouseOut"); if (!t.series) return; a.hoverPoints = k; a.hoverPoint = t; t.firePointEvent("mouseOver"); l && l.refresh(h ? k : t, d)
                            } else c && l && !l.isHidden && (t = l.getAnchor([{}], d), l.updatePosition({ plotX: t[0], plotY: t[1] })); this.unDocMouseMove || (this.unDocMouseMove = G(a.container.ownerDocument, "mousemove", function (a) {
                                var c = z[e.hoverChartIndex];
                                if (c) c.pointer.onDocumentMouseMove(a)
                            })); a.axes.forEach(function (c) { var l = f((c.crosshair || {}).snap, !0), h; l && ((h = a.hoverPoint) && h.series[c.coll] === c || (h = K(k, function (a) { return a.series[c.coll] === c }))); h || !l ? c.drawCrosshair(d, h) : c.hideCrosshair() })
                        }; t.prototype.scaleGroups = function (d, c) {
                            var a = this.chart, f; a.series.forEach(function (l) {
                                f = d || l.getPlotBox(); l.xAxis && l.xAxis.zoomEnabled && l.group && (l.group.attr(f), l.markerGroup && (l.markerGroup.attr(f), l.markerGroup.clip(c ? a.clipRect : null)), l.dataLabelsGroup &&
                                    l.dataLabelsGroup.attr(f))
                            }); a.clipRect.attr(c || a.clipBox)
                        }; t.prototype.setDOMEvents = function () {
                            var d = this.chart.container, c = d.ownerDocument; d.onmousedown = this.onContainerMouseDown.bind(this); d.onmousemove = this.onContainerMouseMove.bind(this); d.onclick = this.onContainerClick.bind(this); this.unbindContainerMouseLeave = G(d, "mouseleave", this.onContainerMouseLeave.bind(this)); e.unbindDocumentMouseUp || (e.unbindDocumentMouseUp = G(c, "mouseup", this.onDocumentMouseUp.bind(this))); e.hasTouch && (G(d, "touchstart",
                                this.onContainerTouchStart.bind(this)), G(d, "touchmove", this.onContainerTouchMove.bind(this)), e.unbindDocumentTouchEnd || (e.unbindDocumentTouchEnd = G(c, "touchend", this.onDocumentTouchEnd.bind(this))))
                        }; t.prototype.setHoverChartIndex = function () { var d = this.chart, c = e.charts[f(e.hoverChartIndex, -1)]; if (c && c !== d) c.pointer.onContainerMouseLeave({ relatedTarget: !0 }); c && c.mouseIsDown || (e.hoverChartIndex = d.index) }; t.prototype.touch = function (d, c) {
                            var a = this.chart, l; this.setHoverChartIndex(); if (1 === d.touches.length) if (d =
                                this.normalize(d), (l = a.isInsidePlot(d.chartX - a.plotLeft, d.chartY - a.plotTop)) && !a.openMenu) { c && this.runPointActions(d); if ("touchmove" === d.type) { c = this.pinchDown; var h = c[0] ? 4 <= Math.sqrt(Math.pow(c[0].chartX - d.chartX, 2) + Math.pow(c[0].chartY - d.chartY, 2)) : !1 } f(h, !0) && this.pinch(d) } else c && this.reset(); else 2 === d.touches.length && this.pinch(d)
                        }; t.prototype.zoomOption = function (d) {
                            var c = this.chart, a = c.options.chart, l = a.zoomType || ""; c = c.inverted; /touch/.test(d.type) && (l = f(a.pinchType, l)); this.zoomX = d = /x/.test(l);
                            this.zoomY = l = /y/.test(l); this.zoomHor = d && !c || l && c; this.zoomVert = l && !c || d && c; this.hasZoom = d || l
                        }; return t
            }(); return e.Pointer = p
        }); O(q, "parts/MSPointer.js", [q["parts/Globals.js"], q["parts/Pointer.js"], q["parts/Utilities.js"]], function (p, e, q) {
            function B() { var u = []; u.item = function (u) { return this[u] }; A(w, function (r) { u.push({ pageX: r.pageX, pageY: r.pageY, target: r.target }) }); return u } function D(u, r, h, f) {
            "touch" !== u.pointerType && u.pointerType !== u.MSPOINTER_TYPE_TOUCH || !J[p.hoverChartIndex] || (f(u), f = J[p.hoverChartIndex].pointer,
                f[r]({ type: h, target: u.currentTarget, preventDefault: m, touches: B() }))
            } var z = this && this.__extends || function () { var u = function (r, h) { u = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (f, d) { f.__proto__ = d } || function (f, d) { for (var h in d) d.hasOwnProperty(h) && (f[h] = d[h]) }; return u(r, h) }; return function (r, h) { function f() { this.constructor = r } u(r, h); r.prototype = null === h ? Object.create(h) : (f.prototype = h.prototype, new f) } }(), J = p.charts, G = p.doc, m = p.noop, H = q.addEvent, M = q.css, A = q.objectEach, K = q.removeEvent,
                w = {}, r = !!p.win.PointerEvent; return function (u) {
                    function m() { return null !== u && u.apply(this, arguments) || this } z(m, u); m.prototype.batchMSEvents = function (h) { h(this.chart.container, r ? "pointerdown" : "MSPointerDown", this.onContainerPointerDown); h(this.chart.container, r ? "pointermove" : "MSPointerMove", this.onContainerPointerMove); h(G, r ? "pointerup" : "MSPointerUp", this.onDocumentPointerUp) }; m.prototype.destroy = function () { this.batchMSEvents(K); u.prototype.destroy.call(this) }; m.prototype.init = function (h, f) {
                        u.prototype.init.call(this,
                            h, f); this.hasZoom && M(h.container, { "-ms-touch-action": "none", "touch-action": "none" })
                    }; m.prototype.onContainerPointerDown = function (h) { D(h, "onContainerTouchStart", "touchstart", function (f) { w[f.pointerId] = { pageX: f.pageX, pageY: f.pageY, target: f.currentTarget } }) }; m.prototype.onContainerPointerMove = function (h) { D(h, "onContainerTouchMove", "touchmove", function (f) { w[f.pointerId] = { pageX: f.pageX, pageY: f.pageY }; w[f.pointerId].target || (w[f.pointerId].target = f.currentTarget) }) }; m.prototype.onDocumentPointerUp = function (h) {
                        D(h,
                            "onDocumentTouchEnd", "touchend", function (f) { delete w[f.pointerId] })
                    }; m.prototype.setDOMEvents = function () { u.prototype.setDOMEvents.call(this); (this.hasZoom || this.followTouchMove) && this.batchMSEvents(H) }; return m
                }(e)
        }); O(q, "parts/Legend.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
            var q = e.addEvent, B = e.animObject, D = e.css, z = e.defined, J = e.discardElement, G = e.find, m = e.fireEvent, H = e.format, M = e.isNumber, A = e.merge, K = e.pick, w = e.relativeLength, r = e.setAnimation, u = e.stableSort, C = e.syncTimeout;
            e = e.wrap; var h = p.isFirefox, f = p.marginNames, d = p.win, t = function () {
                function d(c, a) {
                this.allItems = []; this.contentGroup = this.box = void 0; this.display = !1; this.group = void 0; this.offsetWidth = this.maxLegendWidth = this.maxItemWidth = this.legendWidth = this.legendHeight = this.lastLineHeight = this.lastItemY = this.itemY = this.itemX = this.itemMarginTop = this.itemMarginBottom = this.itemHeight = this.initialItemY = 0; this.options = {}; this.padding = 0; this.pages = []; this.proximate = !1; this.scrollGroup = void 0; this.widthOption = this.totalItemWidth =
                    this.titleHeight = this.symbolWidth = this.symbolHeight = 0; this.chart = c; this.init(c, a)
                } d.prototype.init = function (c, a) { this.chart = c; this.setOptions(a); a.enabled && (this.render(), q(this.chart, "endResize", function () { this.legend.positionCheckboxes() }), this.proximate ? this.unchartrender = q(this.chart, "render", function () { this.legend.proximatePositions(); this.legend.positionItems() }) : this.unchartrender && this.unchartrender()) }; d.prototype.setOptions = function (c) {
                    var a = K(c.padding, 8); this.options = c; this.chart.styledMode ||
                        (this.itemStyle = c.itemStyle, this.itemHiddenStyle = A(this.itemStyle, c.itemHiddenStyle)); this.itemMarginTop = c.itemMarginTop || 0; this.itemMarginBottom = c.itemMarginBottom || 0; this.padding = a; this.initialItemY = a - 5; this.symbolWidth = K(c.symbolWidth, 16); this.pages = []; this.proximate = "proximate" === c.layout && !this.chart.inverted; this.baseline = void 0
                }; d.prototype.update = function (c, a) { var d = this.chart; this.setOptions(A(!0, this.options, c)); this.destroy(); d.isDirtyLegend = d.isDirtyBox = !0; K(a, !0) && d.redraw(); m(this, "afterUpdate") };
                d.prototype.colorizeItem = function (c, a) { c.legendGroup[a ? "removeClass" : "addClass"]("highcharts-legend-item-hidden"); if (!this.chart.styledMode) { var d = this.options, f = c.legendItem, h = c.legendLine, l = c.legendSymbol, k = this.itemHiddenStyle.color; d = a ? d.itemStyle.color : k; var t = a ? c.color || k : k, u = c.options && c.options.marker, r = { fill: t }; f && f.css({ fill: d, color: d }); h && h.attr({ stroke: t }); l && (u && l.isMarker && (r = c.pointAttribs(), a || (r.stroke = r.fill = k)), l.attr(r)) } m(this, "afterColorizeItem", { item: c, visible: a }) }; d.prototype.positionItems =
                    function () { this.allItems.forEach(this.positionItem, this); this.chart.isResizing || this.positionCheckboxes() }; d.prototype.positionItem = function (c) { var a = this, d = this.options, f = d.symbolPadding, l = !d.rtl, h = c._legendItemPos; d = h[0]; h = h[1]; var k = c.checkbox, t = c.legendGroup; t && t.element && (f = { translateX: l ? d : this.legendWidth - d - 2 * f - 4, translateY: h }, l = function () { m(a, "afterPositionItem", { item: c }) }, z(t.translateY) ? t.animate(f, { complete: l }) : (t.attr(f), l())); k && (k.x = d, k.y = h) }; d.prototype.destroyItem = function (c) {
                        var a =
                            c.checkbox;["legendItem", "legendLine", "legendSymbol", "legendGroup"].forEach(function (a) { c[a] && (c[a] = c[a].destroy()) }); a && J(c.checkbox)
                    }; d.prototype.destroy = function () { function c(a) { this[a] && (this[a] = this[a].destroy()) } this.getAllItems().forEach(function (a) { ["legendItem", "legendGroup"].forEach(c, a) }); "clipRect up down pager nav box title group".split(" ").forEach(c, this); this.display = null }; d.prototype.positionCheckboxes = function () {
                        var c = this.group && this.group.alignAttr, a = this.clipHeight || this.legendHeight,
                        d = this.titleHeight; if (c) { var f = c.translateY; this.allItems.forEach(function (h) { var l = h.checkbox; if (l) { var k = f + d + l.y + (this.scrollOffset || 0) + 3; D(l, { left: c.translateX + h.checkboxOffset + l.x - 20 + "px", top: k + "px", display: this.proximate || k > f - 6 && k < f + a - 6 ? "" : "none" }) } }, this) }
                    }; d.prototype.renderTitle = function () {
                        var c = this.options, a = this.padding, d = c.title, f = 0; d.text && (this.title || (this.title = this.chart.renderer.label(d.text, a - 3, a - 4, null, null, null, c.useHTML, null, "legend-title").attr({ zIndex: 1 }), this.chart.styledMode ||
                            this.title.css(d.style), this.title.add(this.group)), d.width || this.title.css({ width: this.maxLegendWidth + "px" }), c = this.title.getBBox(), f = c.height, this.offsetWidth = c.width, this.contentGroup.attr({ translateY: f })); this.titleHeight = f
                    }; d.prototype.setText = function (c) { var a = this.options; c.legendItem.attr({ text: a.labelFormat ? H(a.labelFormat, c, this.chart) : a.labelFormatter.call(c) }) }; d.prototype.renderItem = function (c) {
                        var a = this.chart, d = a.renderer, f = this.options, l = this.symbolWidth, h = f.symbolPadding, k = this.itemStyle,
                        t = this.itemHiddenStyle, r = "horizontal" === f.layout ? K(f.itemDistance, 20) : 0, u = !f.rtl, g = c.legendItem, b = !c.series, n = !b && c.series.drawLegendSymbol ? c.series : c, m = n.options; m = this.createCheckboxForItem && m && m.showCheckbox; r = l + h + r + (m ? 20 : 0); var e = f.useHTML, w = c.options.className; g || (c.legendGroup = d.g("legend-item").addClass("highcharts-" + n.type + "-series highcharts-color-" + c.colorIndex + (w ? " " + w : "") + (b ? " highcharts-series-" + c.index : "")).attr({ zIndex: 1 }).add(this.scrollGroup), c.legendItem = g = d.text("", u ? l + h : -h, this.baseline ||
                            0, e), a.styledMode || g.css(A(c.visible ? k : t)), g.attr({ align: u ? "left" : "right", zIndex: 2 }).add(c.legendGroup), this.baseline || (this.fontMetrics = d.fontMetrics(a.styledMode ? 12 : k.fontSize, g), this.baseline = this.fontMetrics.f + 3 + this.itemMarginTop, g.attr("y", this.baseline)), this.symbolHeight = f.symbolHeight || this.fontMetrics.f, n.drawLegendSymbol(this, c), this.setItemEvents && this.setItemEvents(c, g, e)); m && !c.checkbox && this.createCheckboxForItem && this.createCheckboxForItem(c); this.colorizeItem(c, c.visible); !a.styledMode &&
                                k.width || g.css({ width: (f.itemWidth || this.widthOption || a.spacingBox.width) - r + "px" }); this.setText(c); a = g.getBBox(); c.itemWidth = c.checkboxOffset = f.itemWidth || c.legendItemWidth || a.width + r; this.maxItemWidth = Math.max(this.maxItemWidth, c.itemWidth); this.totalItemWidth += c.itemWidth; this.itemHeight = c.itemHeight = Math.round(c.legendItemHeight || a.height || this.symbolHeight)
                    }; d.prototype.layoutItem = function (c) {
                        var a = this.options, d = this.padding, f = "horizontal" === a.layout, l = c.itemHeight, h = this.itemMarginBottom, k =
                            this.itemMarginTop, t = f ? K(a.itemDistance, 20) : 0, r = this.maxLegendWidth; a = a.alignColumns && this.totalItemWidth > r ? this.maxItemWidth : c.itemWidth; f && this.itemX - d + a > r && (this.itemX = d, this.lastLineHeight && (this.itemY += k + this.lastLineHeight + h), this.lastLineHeight = 0); this.lastItemY = k + this.itemY + h; this.lastLineHeight = Math.max(l, this.lastLineHeight); c._legendItemPos = [this.itemX, this.itemY]; f ? this.itemX += a : (this.itemY += k + l + h, this.lastLineHeight = l); this.offsetWidth = this.widthOption || Math.max((f ? this.itemX - d - (c.checkbox ?
                                0 : t) : a) + d, this.offsetWidth)
                    }; d.prototype.getAllItems = function () { var c = []; this.chart.series.forEach(function (a) { var d = a && a.options; a && K(d.showInLegend, z(d.linkedTo) ? !1 : void 0, !0) && (c = c.concat(a.legendItems || ("point" === d.legendType ? a.data : a))) }); m(this, "afterGetAllItems", { allItems: c }); return c }; d.prototype.getAlignment = function () { var c = this.options; return this.proximate ? c.align.charAt(0) + "tv" : c.floating ? "" : c.align.charAt(0) + c.verticalAlign.charAt(0) + c.layout.charAt(0) }; d.prototype.adjustMargins = function (c,
                        a) { var d = this.chart, l = this.options, h = this.getAlignment(); h && [/(lth|ct|rth)/, /(rtv|rm|rbv)/, /(rbh|cb|lbh)/, /(lbv|lm|ltv)/].forEach(function (t, k) { t.test(h) && !z(c[k]) && (d[f[k]] = Math.max(d[f[k]], d.legend[(k + 1) % 2 ? "legendHeight" : "legendWidth"] + [1, -1, -1, 1][k] * l[k % 2 ? "x" : "y"] + K(l.margin, 12) + a[k] + (d.titleOffset[k] || 0))) }) }; d.prototype.proximatePositions = function () {
                            var c = this.chart, a = [], d = "left" === this.options.align; this.allItems.forEach(function (f) {
                                var l = d; if (f.yAxis && f.points) {
                                    f.xAxis.options.reversed && (l =
                                        !l); var h = G(l ? f.points : f.points.slice(0).reverse(), function (a) { return M(a.plotY) }); l = this.itemMarginTop + f.legendItem.getBBox().height + this.itemMarginBottom; var k = f.yAxis.top - c.plotTop; f.visible ? (h = h ? h.plotY : f.yAxis.height, h += k - .3 * l) : h = k + f.yAxis.height; a.push({ target: h, size: l, item: f })
                                }
                            }, this); p.distribute(a, c.plotHeight); a.forEach(function (a) { a.item._legendItemPos[1] = c.plotTop - c.spacing[0] + a.pos })
                        }; d.prototype.render = function () {
                            var c = this.chart, a = c.renderer, d = this.group, f = this.box, l = this.options, h =
                                this.padding; this.itemX = h; this.itemY = this.initialItemY; this.lastItemY = this.offsetWidth = 0; this.widthOption = w(l.width, c.spacingBox.width - h); var k = c.spacingBox.width - 2 * h - l.x; -1 < ["rm", "lm"].indexOf(this.getAlignment().substring(0, 2)) && (k /= 2); this.maxLegendWidth = this.widthOption || k; d || (this.group = d = a.g("legend").attr({ zIndex: 7 }).add(), this.contentGroup = a.g().attr({ zIndex: 1 }).add(d), this.scrollGroup = a.g().add(this.contentGroup)); this.renderTitle(); var t = this.getAllItems(); u(t, function (a, g) {
                                    return (a.options &&
                                        a.options.legendIndex || 0) - (g.options && g.options.legendIndex || 0)
                                }); l.reversed && t.reverse(); this.allItems = t; this.display = k = !!t.length; this.itemHeight = this.totalItemWidth = this.maxItemWidth = this.lastLineHeight = 0; t.forEach(this.renderItem, this); t.forEach(this.layoutItem, this); t = (this.widthOption || this.offsetWidth) + h; var r = this.lastItemY + this.lastLineHeight + this.titleHeight; r = this.handleOverflow(r); r += h; f || (this.box = f = a.rect().addClass("highcharts-legend-box").attr({ r: l.borderRadius }).add(d), f.isNew = !0);
                            c.styledMode || f.attr({ stroke: l.borderColor, "stroke-width": l.borderWidth || 0, fill: l.backgroundColor || "none" }).shadow(l.shadow); 0 < t && 0 < r && (f[f.isNew ? "attr" : "animate"](f.crisp.call({}, { x: 0, y: 0, width: t, height: r }, f.strokeWidth())), f.isNew = !1); f[k ? "show" : "hide"](); c.styledMode && "none" === d.getStyle("display") && (t = r = 0); this.legendWidth = t; this.legendHeight = r; k && this.align(); this.proximate || this.positionItems(); m(this, "afterRender")
                        }; d.prototype.align = function (c) {
                        void 0 === c && (c = this.chart.spacingBox); var a =
                            this.chart, d = this.options, f = c.y; /(lth|ct|rth)/.test(this.getAlignment()) && 0 < a.titleOffset[0] ? f += a.titleOffset[0] : /(lbh|cb|rbh)/.test(this.getAlignment()) && 0 < a.titleOffset[2] && (f -= a.titleOffset[2]); f !== c.y && (c = A(c, { y: f })); this.group.align(A(d, { width: this.legendWidth, height: this.legendHeight, verticalAlign: this.proximate ? "top" : d.verticalAlign }), !0, c)
                        }; d.prototype.handleOverflow = function (c) {
                            var a = this, d = this.chart, f = d.renderer, l = this.options, h = l.y, k = this.padding; h = d.spacingBox.height + ("top" === l.verticalAlign ?
                                -h : h) - k; var t = l.maxHeight, r, u = this.clipRect, g = l.navigation, b = K(g.animation, !0), n = g.arrowSize || 12, m = this.nav, e = this.pages, w, C = this.allItems, p = function (b) { "number" === typeof b ? u.attr({ height: b }) : u && (a.clipRect = u.destroy(), a.contentGroup.clip()); a.contentGroup.div && (a.contentGroup.div.style.clip = b ? "rect(" + k + "px,9999px," + (k + b) + "px,0)" : "auto") }, A = function (b) { a[b] = f.circle(0, 0, 1.3 * n).translate(n / 2, n / 2).add(m); d.styledMode || a[b].attr("fill", "rgba(0,0,0,0.0001)"); return a[b] }; "horizontal" !== l.layout || "middle" ===
                                    l.verticalAlign || l.floating || (h /= 2); t && (h = Math.min(h, t)); e.length = 0; c > h && !1 !== g.enabled ? (this.clipHeight = r = Math.max(h - 20 - this.titleHeight - k, 0), this.currentPage = K(this.currentPage, 1), this.fullHeight = c, C.forEach(function (a, b) { var g = a._legendItemPos[1], c = Math.round(a.legendItem.getBBox().height), d = e.length; if (!d || g - e[d - 1] > r && (w || g) !== e[d - 1]) e.push(w || g), d++; a.pageIx = d - 1; w && (C[b - 1].pageIx = d - 1); b === C.length - 1 && g + c - e[d - 1] > r && g !== w && (e.push(g), a.pageIx = d); g !== w && (w = g) }), u || (u = a.clipRect = f.clipRect(0, k, 9999,
                                        0), a.contentGroup.clip(u)), p(r), m || (this.nav = m = f.g().attr({ zIndex: 1 }).add(this.group), this.up = f.symbol("triangle", 0, 0, n, n).add(m), A("upTracker").on("click", function () { a.scroll(-1, b) }), this.pager = f.text("", 15, 10).addClass("highcharts-legend-navigation"), d.styledMode || this.pager.css(g.style), this.pager.add(m), this.down = f.symbol("triangle-down", 0, 0, n, n).add(m), A("downTracker").on("click", function () { a.scroll(1, b) })), a.scroll(0), c = h) : m && (p(), this.nav = m.destroy(), this.scrollGroup.attr({ translateY: 1 }), this.clipHeight =
                                            0); return c
                        }; d.prototype.scroll = function (c, a) {
                            var d = this, f = this.chart, l = this.pages, h = l.length, k = this.currentPage + c; c = this.clipHeight; var t = this.options.navigation, u = this.pager, e = this.padding; k > h && (k = h); 0 < k && ("undefined" !== typeof a && r(a, f), this.nav.attr({ translateX: e, translateY: c + this.padding + 7 + this.titleHeight, visibility: "visible" }), [this.up, this.upTracker].forEach(function (a) { a.attr({ "class": 1 === k ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active" }) }), u.attr({ text: k + "/" + h }), [this.down,
                            this.downTracker].forEach(function (a) { a.attr({ x: 18 + this.pager.getBBox().width, "class": k === h ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active" }) }, this), f.styledMode || (this.up.attr({ fill: 1 === k ? t.inactiveColor : t.activeColor }), this.upTracker.css({ cursor: 1 === k ? "default" : "pointer" }), this.down.attr({ fill: k === h ? t.inactiveColor : t.activeColor }), this.downTracker.css({ cursor: k === h ? "default" : "pointer" })), this.scrollOffset = -l[k - 1] + this.initialItemY, this.scrollGroup.animate({ translateY: this.scrollOffset }),
                                this.currentPage = k, this.positionCheckboxes(), a = B(K(a, f.renderer.globalAnimation, !0)), C(function () { m(d, "afterScroll", { currentPage: k }) }, a.duration || 0))
                        }; return d
            }(); (/Trident\/7\.0/.test(d.navigator && d.navigator.userAgent) || h) && e(t.prototype, "positionItem", function (d, c) { var a = this, f = function () { c._legendItemPos && d.call(a, c) }; f(); a.bubbleLegend || setTimeout(f) }); p.Legend = t; return p.Legend
        }); O(q, "parts/Chart.js", [q["parts/Axis.js"], q["parts/Globals.js"], q["parts/Legend.js"], q["parts/MSPointer.js"], q["parts/Options.js"],
        q["parts/Pointer.js"], q["parts/Time.js"], q["parts/Utilities.js"]], function (p, e, q, B, D, z, J, G) {
            var m = e.charts, H = e.doc, M = e.seriesTypes, A = e.win, K = D.defaultOptions, w = G.addEvent, r = G.animate, u = G.animObject, C = G.attr, h = G.createElement, f = G.css, d = G.defined, t = G.discardElement, l = G.erase, c = G.error, a = G.extend, x = G.find, v = G.fireEvent, E = G.getStyle, F = G.isArray, k = G.isFunction, y = G.isNumber, I = G.isObject, P = G.isString, g = G.merge, b = G.numberFormat, n = G.objectEach, L = G.pick, N = G.pInt, S = G.relativeLength, aa = G.removeEvent, Z = G.setAnimation,
            ba = G.splat, Y = G.syncTimeout, ca = G.uniqueKey, da = e.marginNames, T = function () {
                function D(a, b, g) {
                this.yAxis = this.xAxis = this.userOptions = this.titleOffset = this.time = this.symbolCounter = this.spacingBox = this.spacing = this.series = this.renderTo = this.renderer = this.pointer = this.pointCount = this.plotWidth = this.plotTop = this.plotLeft = this.plotHeight = this.plotBox = this.options = this.numberFormatter = this.margin = this.legend = this.labelCollectors = this.isResizing = this.index = this.container = this.colorCounter = this.clipBox = this.chartWidth =
                    this.chartHeight = this.bounds = this.axisOffset = this.axes = void 0; this.getArgs(a, b, g)
                } D.prototype.getArgs = function (a, b, g) { P(a) || a.nodeName ? (this.renderTo = a, this.init(b, g)) : this.init(a, b) }; D.prototype.init = function (a, c) {
                    var d, f = a.series, h = a.plotOptions || {}; v(this, "init", { args: arguments }, function () {
                    a.series = null; d = g(K, a); var l = d.chart || {}; n(d.plotOptions, function (a, b) { I(a) && (a.tooltip = h[b] && g(h[b].tooltip) || void 0) }); d.tooltip.userOptions = a.chart && a.chart.forExport && a.tooltip.userOptions || a.tooltip; d.series =
                        a.series = f; this.userOptions = a; var t = l.events; this.margin = []; this.spacing = []; this.bounds = { h: {}, v: {} }; this.labelCollectors = []; this.callback = c; this.isResizing = 0; this.options = d; this.axes = []; this.series = []; this.time = a.time && Object.keys(a.time).length ? new J(a.time) : e.time; this.numberFormatter = l.numberFormatter || b; this.styledMode = l.styledMode; this.hasCartesianSeries = l.showAxes; var y = this; y.index = m.length; m.push(y); e.chartCount++; t && n(t, function (a, b) { k(a) && w(y, b, a) }); y.xAxis = []; y.yAxis = []; y.pointCount = y.colorCounter =
                            y.symbolCounter = 0; v(y, "afterInit"); y.firstRender()
                    })
                }; D.prototype.initSeries = function (a) { var b = this.options.chart; b = a.type || b.type || b.defaultSeriesType; var g = M[b]; g || c(17, !0, this, { missingModuleFor: b }); b = new g; b.init(this, a); return b }; D.prototype.setSeriesData = function () { this.getSeriesOrderByLinks().forEach(function (a) { a.points || a.data || !a.enabledDataSorting || a.setData(a.options.data, !1) }) }; D.prototype.getSeriesOrderByLinks = function () {
                    return this.series.concat().sort(function (a, b) {
                        return a.linkedSeries.length ||
                            b.linkedSeries.length ? b.linkedSeries.length - a.linkedSeries.length : 0
                    })
                }; D.prototype.orderSeries = function (a) { var b = this.series; for (a = a || 0; a < b.length; a++)b[a] && (b[a].index = a, b[a].name = b[a].getName()) }; D.prototype.isInsidePlot = function (a, b, g) { var c = g ? b : a; a = g ? a : b; c = { x: c, y: a, isInsidePlot: 0 <= c && c <= this.plotWidth && 0 <= a && a <= this.plotHeight }; v(this, "afterIsInsidePlot", c); return c.isInsidePlot }; D.prototype.redraw = function (b) {
                    v(this, "beforeRedraw"); var g = this, c = g.axes, d = g.series, n = g.pointer, k = g.legend, f = g.userOptions.legend,
                        h = g.isDirtyLegend, l = g.hasCartesianSeries, t = g.isDirtyBox, r = g.renderer, u = r.isHidden(), x = []; g.setResponsive && g.setResponsive(!1); Z(g.hasRendered ? b : !1, g); u && g.temporaryDisplay(); g.layOutTitles(); for (b = d.length; b--;) { var m = d[b]; if (m.options.stacking) { var F = !0; if (m.isDirty) { var I = !0; break } } } if (I) for (b = d.length; b--;)m = d[b], m.options.stacking && (m.isDirty = !0); d.forEach(function (a) {
                        a.isDirty && ("point" === a.options.legendType ? (a.updateTotals && a.updateTotals(), h = !0) : f && (f.labelFormatter || f.labelFormat) && (h = !0));
                            a.isDirtyData && v(a, "updatedData")
                        }); h && k && k.options.enabled && (k.render(), g.isDirtyLegend = !1); F && g.getStacks(); l && c.forEach(function (a) { g.isResizing && y(a.min) || (a.updateNames(), a.setScale()) }); g.getMargins(); l && (c.forEach(function (a) { a.isDirty && (t = !0) }), c.forEach(function (b) { var g = b.min + "," + b.max; b.extKey !== g && (b.extKey = g, x.push(function () { v(b, "afterSetExtremes", a(b.eventArgs, b.getExtremes())); delete b.eventArgs })); (t || F) && b.redraw() })); t && g.drawChartBox(); v(g, "predraw"); d.forEach(function (a) {
                        (t ||
                            a.isDirty) && a.visible && a.redraw(); a.isDirtyData = !1
                        }); n && n.reset(!0); r.draw(); v(g, "redraw"); v(g, "render"); u && g.temporaryDisplay(!0); x.forEach(function (a) { a.call() })
                }; D.prototype.get = function (a) { function b(b) { return b.id === a || b.options && b.options.id === a } var g = this.series, c; var d = x(this.axes, b) || x(this.series, b); for (c = 0; !d && c < g.length; c++)d = x(g[c].points || [], b); return d }; D.prototype.getAxes = function () {
                    var a = this, b = this.options, g = b.xAxis = ba(b.xAxis || {}); b = b.yAxis = ba(b.yAxis || {}); v(this, "getAxes"); g.forEach(function (a,
                        b) { a.index = b; a.isX = !0 }); b.forEach(function (a, b) { a.index = b }); g.concat(b).forEach(function (b) { new p(a, b) }); v(this, "afterGetAxes")
                }; D.prototype.getSelectedPoints = function () { var a = []; this.series.forEach(function (b) { a = a.concat(b.getPointsCollection().filter(function (a) { return L(a.selectedStaging, a.selected) })) }); return a }; D.prototype.getSelectedSeries = function () { return this.series.filter(function (a) { return a.selected }) }; D.prototype.setTitle = function (a, b, g) {
                    this.applyDescription("title", a); this.applyDescription("subtitle",
                        b); this.applyDescription("caption", void 0); this.layOutTitles(g)
                }; D.prototype.applyDescription = function (a, b) {
                    var c = this, d = "title" === a ? { color: "#333333", fontSize: this.options.isStock ? "16px" : "18px" } : { color: "#666666" }; d = this.options[a] = g(!this.styledMode && { style: d }, this.options[a], b); var n = this[a]; n && b && (this[a] = n = n.destroy()); d && !n && (n = this.renderer.text(d.text, 0, 0, d.useHTML).attr({ align: d.align, "class": "highcharts-" + a, zIndex: d.zIndex || 4 }).add(), n.update = function (b) {
                        c[{
                            title: "setTitle", subtitle: "setSubtitle",
                            caption: "setCaption"
                        }[a]](b)
                    }, this.styledMode || n.css(d.style), this[a] = n)
                }; D.prototype.layOutTitles = function (b) {
                    var g = [0, 0, 0], c = this.renderer, d = this.spacingBox;["title", "subtitle", "caption"].forEach(function (b) {
                        var n = this[b], k = this.options[b], f = k.verticalAlign || "top"; b = "title" === b ? -3 : "top" === f ? g[0] + 2 : 0; if (n) {
                            if (!this.styledMode) var h = k.style.fontSize; h = c.fontMetrics(h, n).b; n.css({ width: (k.width || d.width + (k.widthAdjust || 0)) + "px" }); var l = Math.round(n.getBBox(k.useHTML).height); n.align(a({
                                y: "bottom" ===
                                    f ? h : b + h, height: l
                            }, k), !1, "spacingBox"); k.floating || ("top" === f ? g[0] = Math.ceil(g[0] + l) : "bottom" === f && (g[2] = Math.ceil(g[2] + l)))
                        }
                    }, this); g[0] && "top" === (this.options.title.verticalAlign || "top") && (g[0] += this.options.title.margin); g[2] && "bottom" === this.options.caption.verticalAlign && (g[2] += this.options.caption.margin); var n = !this.titleOffset || this.titleOffset.join(",") !== g.join(","); this.titleOffset = g; v(this, "afterLayOutTitles"); !this.isDirtyBox && n && (this.isDirtyBox = this.isDirtyLegend = n, this.hasRendered &&
                        L(b, !0) && this.isDirtyBox && this.redraw())
                }; D.prototype.getChartSize = function () { var a = this.options.chart, b = a.width; a = a.height; var g = this.renderTo; d(b) || (this.containerWidth = E(g, "width")); d(a) || (this.containerHeight = E(g, "height")); this.chartWidth = Math.max(0, b || this.containerWidth || 600); this.chartHeight = Math.max(0, S(a, this.chartWidth) || (1 < this.containerHeight ? this.containerHeight : 400)) }; D.prototype.temporaryDisplay = function (a) {
                    var b = this.renderTo; if (a) for (; b && b.style;)b.hcOrigStyle && (f(b, b.hcOrigStyle),
                        delete b.hcOrigStyle), b.hcOrigDetached && (H.body.removeChild(b), b.hcOrigDetached = !1), b = b.parentNode; else for (; b && b.style;) {
                            H.body.contains(b) || b.parentNode || (b.hcOrigDetached = !0, H.body.appendChild(b)); if ("none" === E(b, "display", !1) || b.hcOricDetached) b.hcOrigStyle = { display: b.style.display, height: b.style.height, overflow: b.style.overflow }, a = { display: "block", overflow: "hidden" }, b !== this.renderTo && (a.height = 0), f(b, a), b.offsetWidth || b.style.setProperty("display", "block", "important"); b = b.parentNode; if (b ===
                                H.body) break
                        }
                }; D.prototype.setClassName = function (a) { this.container.className = "highcharts-container " + (a || "") }; D.prototype.getContainer = function () {
                    var b = this.options, g = b.chart; var d = this.renderTo; var n = ca(), k, l; d || (this.renderTo = d = g.renderTo); P(d) && (this.renderTo = d = H.getElementById(d)); d || c(13, !0, this); var t = N(C(d, "data-highcharts-chart")); y(t) && m[t] && m[t].hasRendered && m[t].destroy(); C(d, "data-highcharts-chart", this.index); d.innerHTML = ""; g.skipClone || d.offsetWidth || this.temporaryDisplay(); this.getChartSize();
                    t = this.chartWidth; var r = this.chartHeight; f(d, { overflow: "hidden" }); this.styledMode || (k = a({ position: "relative", overflow: "hidden", width: t + "px", height: r + "px", textAlign: "left", lineHeight: "normal", zIndex: 0, "-webkit-tap-highlight-color": "rgba(0,0,0,0)", userSelect: "none" }, g.style)); this.container = d = h("div", { id: n }, k, d); this._cursor = d.style.cursor; this.renderer = new (e[g.renderer] || e.Renderer)(d, t, r, null, g.forExport, b.exporting && b.exporting.allowHTML, this.styledMode); Z(void 0, this); this.setClassName(g.className);
                    if (this.styledMode) for (l in b.defs) this.renderer.definition(b.defs[l]); else this.renderer.setStyle(g.style); this.renderer.chartIndex = this.index; v(this, "afterGetContainer")
                }; D.prototype.getMargins = function (a) {
                    var b = this.spacing, g = this.margin, c = this.titleOffset; this.resetMargins(); c[0] && !d(g[0]) && (this.plotTop = Math.max(this.plotTop, c[0] + b[0])); c[2] && !d(g[2]) && (this.marginBottom = Math.max(this.marginBottom, c[2] + b[2])); this.legend && this.legend.display && this.legend.adjustMargins(g, b); v(this, "getMargins");
                    a || this.getAxisMargins()
                }; D.prototype.getAxisMargins = function () { var a = this, b = a.axisOffset = [0, 0, 0, 0], g = a.colorAxis, c = a.margin, n = function (a) { a.forEach(function (a) { a.visible && a.getOffset() }) }; a.hasCartesianSeries ? n(a.axes) : g && g.length && n(g); da.forEach(function (g, n) { d(c[n]) || (a[g] += b[n]) }); a.setChartSize() }; D.prototype.reflow = function (a) {
                    var b = this, g = b.options.chart, c = b.renderTo, n = d(g.width) && d(g.height), k = g.width || E(c, "width"); g = g.height || E(c, "height"); c = a ? a.target : A; if (!n && !b.isPrinting && k && g && (c ===
                        A || c === H)) { if (k !== b.containerWidth || g !== b.containerHeight) G.clearTimeout(b.reflowTimeout), b.reflowTimeout = Y(function () { b.container && b.setSize(void 0, void 0, !1) }, a ? 100 : 0); b.containerWidth = k; b.containerHeight = g }
                }; D.prototype.setReflow = function (a) { var b = this; !1 === a || this.unbindReflow ? !1 === a && this.unbindReflow && (this.unbindReflow = this.unbindReflow()) : (this.unbindReflow = w(A, "resize", function (a) { b.options && b.reflow(a) }), w(this, "destroy", this.unbindReflow)) }; D.prototype.setSize = function (a, b, g) {
                    var c = this,
                    d = c.renderer; c.isResizing += 1; Z(g, c); g = d.globalAnimation; c.oldChartHeight = c.chartHeight; c.oldChartWidth = c.chartWidth; "undefined" !== typeof a && (c.options.chart.width = a); "undefined" !== typeof b && (c.options.chart.height = b); c.getChartSize(); c.styledMode || (g ? r : f)(c.container, { width: c.chartWidth + "px", height: c.chartHeight + "px" }, g); c.setChartSize(!0); d.setSize(c.chartWidth, c.chartHeight, g); c.axes.forEach(function (a) { a.isDirty = !0; a.setScale() }); c.isDirtyLegend = !0; c.isDirtyBox = !0; c.layOutTitles(); c.getMargins();
                    c.redraw(g); c.oldChartHeight = null; v(c, "resize"); Y(function () { c && v(c, "endResize", null, function () { --c.isResizing }) }, u(g).duration || 0)
                }; D.prototype.setChartSize = function (a) {
                    var b = this.inverted, g = this.renderer, c = this.chartWidth, d = this.chartHeight, n = this.options.chart, k = this.spacing, f = this.clipOffset, h, l, t, y; this.plotLeft = h = Math.round(this.plotLeft); this.plotTop = l = Math.round(this.plotTop); this.plotWidth = t = Math.max(0, Math.round(c - h - this.marginRight)); this.plotHeight = y = Math.max(0, Math.round(d - l - this.marginBottom));
                    this.plotSizeX = b ? y : t; this.plotSizeY = b ? t : y; this.plotBorderWidth = n.plotBorderWidth || 0; this.spacingBox = g.spacingBox = { x: k[3], y: k[0], width: c - k[3] - k[1], height: d - k[0] - k[2] }; this.plotBox = g.plotBox = { x: h, y: l, width: t, height: y }; c = 2 * Math.floor(this.plotBorderWidth / 2); b = Math.ceil(Math.max(c, f[3]) / 2); g = Math.ceil(Math.max(c, f[0]) / 2); this.clipBox = { x: b, y: g, width: Math.floor(this.plotSizeX - Math.max(c, f[1]) / 2 - b), height: Math.max(0, Math.floor(this.plotSizeY - Math.max(c, f[2]) / 2 - g)) }; a || this.axes.forEach(function (a) {
                        a.setAxisSize();
                        a.setAxisTranslation()
                    }); v(this, "afterSetChartSize", { skipAxes: a })
                }; D.prototype.resetMargins = function () { v(this, "resetMargins"); var a = this, b = a.options.chart;["margin", "spacing"].forEach(function (g) { var c = b[g], d = I(c) ? c : [c, c, c, c];["Top", "Right", "Bottom", "Left"].forEach(function (c, n) { a[g][n] = L(b[g + c], d[n]) }) }); da.forEach(function (b, g) { a[b] = L(a.margin[g], a.spacing[g]) }); a.axisOffset = [0, 0, 0, 0]; a.clipOffset = [0, 0, 0, 0] }; D.prototype.drawChartBox = function () {
                    var a = this.options.chart, b = this.renderer, g = this.chartWidth,
                    c = this.chartHeight, d = this.chartBackground, n = this.plotBackground, k = this.plotBorder, f = this.styledMode, h = this.plotBGImage, l = a.backgroundColor, t = a.plotBackgroundColor, y = a.plotBackgroundImage, r, u = this.plotLeft, m = this.plotTop, x = this.plotWidth, F = this.plotHeight, I = this.plotBox, L = this.clipRect, e = this.clipBox, E = "animate"; d || (this.chartBackground = d = b.rect().addClass("highcharts-background").add(), E = "attr"); if (f) var w = r = d.strokeWidth(); else {
                        w = a.borderWidth || 0; r = w + (a.shadow ? 8 : 0); l = { fill: l || "none" }; if (w || d["stroke-width"]) l.stroke =
                            a.borderColor, l["stroke-width"] = w; d.attr(l).shadow(a.shadow)
                    } d[E]({ x: r / 2, y: r / 2, width: g - r - w % 2, height: c - r - w % 2, r: a.borderRadius }); E = "animate"; n || (E = "attr", this.plotBackground = n = b.rect().addClass("highcharts-plot-background").add()); n[E](I); f || (n.attr({ fill: t || "none" }).shadow(a.plotShadow), y && (h ? (y !== h.attr("href") && h.attr("href", y), h.animate(I)) : this.plotBGImage = b.image(y, u, m, x, F).add())); L ? L.animate({ width: e.width, height: e.height }) : this.clipRect = b.clipRect(e); E = "animate"; k || (E = "attr", this.plotBorder =
                        k = b.rect().addClass("highcharts-plot-border").attr({ zIndex: 1 }).add()); f || k.attr({ stroke: a.plotBorderColor, "stroke-width": a.plotBorderWidth || 0, fill: "none" }); k[E](k.crisp({ x: u, y: m, width: x, height: F }, -k.strokeWidth())); this.isDirtyBox = !1; v(this, "afterDrawChartBox")
                }; D.prototype.propFromSeries = function () {
                    var a = this, b = a.options.chart, g, c = a.options.series, d, n;["inverted", "angular", "polar"].forEach(function (k) {
                        g = M[b.type || b.defaultSeriesType]; n = b[k] || g && g.prototype[k]; for (d = c && c.length; !n && d--;)(g = M[c[d].type]) &&
                            g.prototype[k] && (n = !0); a[k] = n
                    })
                }; D.prototype.linkSeries = function () { var a = this, b = a.series; b.forEach(function (a) { a.linkedSeries.length = 0 }); b.forEach(function (b) { var g = b.options.linkedTo; P(g) && (g = ":previous" === g ? a.series[b.index - 1] : a.get(g)) && g.linkedParent !== b && (g.linkedSeries.push(b), b.linkedParent = g, g.enabledDataSorting && b.setDataSortingOptions(), b.visible = L(b.options.visible, g.options.visible, b.visible)) }); v(this, "afterLinkSeries") }; D.prototype.renderSeries = function () {
                    this.series.forEach(function (a) {
                        a.translate();
                        a.render()
                    })
                }; D.prototype.renderLabels = function () { var b = this, g = b.options.labels; g.items && g.items.forEach(function (c) { var d = a(g.style, c.style), n = N(d.left) + b.plotLeft, k = N(d.top) + b.plotTop + 12; delete d.left; delete d.top; b.renderer.text(c.html, n, k).attr({ zIndex: 2 }).css(d).add() }) }; D.prototype.render = function () {
                    var a = this.axes, b = this.colorAxis, g = this.renderer, c = this.options, d = 0, n = function (a) { a.forEach(function (a) { a.visible && a.render() }) }; this.setTitle(); this.legend = new q(this, c.legend); this.getStacks &&
                        this.getStacks(); this.getMargins(!0); this.setChartSize(); c = this.plotWidth; a.some(function (a) { if (a.horiz && a.visible && a.options.labels.enabled && a.series.length) return d = 21, !0 }); var k = this.plotHeight = Math.max(this.plotHeight - d, 0); a.forEach(function (a) { a.setScale() }); this.getAxisMargins(); var f = 1.1 < c / this.plotWidth; var h = 1.05 < k / this.plotHeight; if (f || h) a.forEach(function (a) { (a.horiz && f || !a.horiz && h) && a.setTickInterval(!0) }), this.getMargins(); this.drawChartBox(); this.hasCartesianSeries ? n(a) : b && b.length &&
                            n(b); this.seriesGroup || (this.seriesGroup = g.g("series-group").attr({ zIndex: 3 }).add()); this.renderSeries(); this.renderLabels(); this.addCredits(); this.setResponsive && this.setResponsive(); this.updateContainerScaling(); this.hasRendered = !0
                }; D.prototype.addCredits = function (a) {
                    var b = this, c = g(!0, this.options.credits, a); c.enabled && !this.credits && (this.credits = this.renderer.text(c.text + (this.mapCredits || ""), 0, 0).addClass("highcharts-credits").on("click", function () { c.href && (A.location.href = c.href) }).attr({
                        align: c.position.align,
                        zIndex: 8
                    }), b.styledMode || this.credits.css(c.style), this.credits.add().align(c.position), this.credits.update = function (a) { b.credits = b.credits.destroy(); b.addCredits(a) })
                }; D.prototype.updateContainerScaling = function () { var a = this.container; if (2 < a.offsetWidth && 2 < a.offsetHeight && a.getBoundingClientRect) { var b = a.getBoundingClientRect(), g = b.width / a.offsetWidth; a = b.height / a.offsetHeight; 1 !== g || 1 !== a ? this.containerScaling = { scaleX: g, scaleY: a } : delete this.containerScaling } }; D.prototype.destroy = function () {
                    var a =
                        this, b = a.axes, g = a.series, c = a.container, d, k = c && c.parentNode; v(a, "destroy"); a.renderer.forExport ? l(m, a) : m[a.index] = void 0; e.chartCount--; a.renderTo.removeAttribute("data-highcharts-chart"); aa(a); for (d = b.length; d--;)b[d] = b[d].destroy(); this.scroller && this.scroller.destroy && this.scroller.destroy(); for (d = g.length; d--;)g[d] = g[d].destroy(); "title subtitle chartBackground plotBackground plotBGImage plotBorder seriesGroup clipRect credits pointer rangeSelector legend resetZoomButton tooltip renderer".split(" ").forEach(function (b) {
                            var g =
                                a[b]; g && g.destroy && (a[b] = g.destroy())
                        }); c && (c.innerHTML = "", aa(c), k && t(c)); n(a, function (b, g) { delete a[g] })
                }; D.prototype.firstRender = function () {
                    var a = this, b = a.options; if (!a.isReadyToRender || a.isReadyToRender()) {
                        a.getContainer(); a.resetMargins(); a.setChartSize(); a.propFromSeries(); a.getAxes(); (F(b.series) ? b.series : []).forEach(function (b) { a.initSeries(b) }); a.linkSeries(); a.setSeriesData(); v(a, "beforeRender"); z && (a.pointer = e.hasTouch || !A.PointerEvent && !A.MSPointerEvent ? new z(a, b) : new B(a, b)); a.render();
                        if (!a.renderer.imgCount && !a.hasLoaded) a.onload(); a.temporaryDisplay(!0)
                    }
                }; D.prototype.onload = function () { this.callbacks.concat([this.callback]).forEach(function (a) { a && "undefined" !== typeof this.index && a.apply(this, [this]) }, this); v(this, "load"); v(this, "render"); d(this.index) && this.setReflow(this.options.chart.reflow); this.hasLoaded = !0 }; return D
            }(); T.prototype.callbacks = []; e.chart = function (a, b, g) { return new T(a, b, g) }; return e.Chart = T
        }); O(q, "parts/ScrollablePlotArea.js", [q["parts/Chart.js"], q["parts/Globals.js"],
        q["parts/Utilities.js"]], function (p, e, q) {
            var B = q.addEvent, D = q.createElement, z = q.pick, J = q.stop; ""; B(p, "afterSetChartSize", function (p) {
                var m = this.options.chart.scrollablePlotArea, q = m && m.minWidth; m = m && m.minHeight; if (!this.renderer.forExport) {
                    if (q) { if (this.scrollablePixelsX = q = Math.max(0, q - this.chartWidth)) { this.plotWidth += q; this.inverted ? (this.clipBox.height += q, this.plotBox.height += q) : (this.clipBox.width += q, this.plotBox.width += q); var G = { 1: { name: "right", value: q } } } } else m && (this.scrollablePixelsY = q = Math.max(0,
                        m - this.chartHeight)) && (this.plotHeight += q, this.inverted ? (this.clipBox.width += q, this.plotBox.width += q) : (this.clipBox.height += q, this.plotBox.height += q), G = { 2: { name: "bottom", value: q } }); G && !p.skipAxes && this.axes.forEach(function (m) { G[m.side] ? m.getPlotLinePath = function () { var p = G[m.side].name, w = this[p]; this[p] = w - G[m.side].value; var r = e.Axis.prototype.getPlotLinePath.apply(this, arguments); this[p] = w; return r } : (m.setAxisSize(), m.setAxisTranslation()) })
                }
            }); B(p, "render", function () {
            this.scrollablePixelsX || this.scrollablePixelsY ?
                (this.setUpScrolling && this.setUpScrolling(), this.applyFixed()) : this.fixedDiv && this.applyFixed()
            }); p.prototype.setUpScrolling = function () {
                var e = this, m = { WebkitOverflowScrolling: "touch", overflowX: "hidden", overflowY: "hidden" }; this.scrollablePixelsX && (m.overflowX = "auto"); this.scrollablePixelsY && (m.overflowY = "auto"); this.scrollingContainer = D("div", { className: "highcharts-scrolling" }, m, this.renderTo); B(this.scrollingContainer, "scroll", function () { e.pointer && delete e.pointer.chartPosition }); this.innerContainer =
                    D("div", { className: "highcharts-inner-container" }, null, this.scrollingContainer); this.innerContainer.appendChild(this.container); this.setUpScrolling = null
            }; p.prototype.moveFixedElements = function () {
                var e = this.container, m = this.fixedRenderer, p = ".highcharts-contextbutton .highcharts-credits .highcharts-legend .highcharts-legend-checkbox .highcharts-navigator-series .highcharts-navigator-xaxis .highcharts-navigator-yaxis .highcharts-navigator .highcharts-reset-zoom .highcharts-scrollbar .highcharts-subtitle .highcharts-title".split(" "),
                q; this.scrollablePixelsX && !this.inverted ? q = ".highcharts-yaxis" : this.scrollablePixelsX && this.inverted ? q = ".highcharts-xaxis" : this.scrollablePixelsY && !this.inverted ? q = ".highcharts-xaxis" : this.scrollablePixelsY && this.inverted && (q = ".highcharts-yaxis"); p.push(q, q + "-labels"); p.forEach(function (p) { [].forEach.call(e.querySelectorAll(p), function (e) { (e.namespaceURI === m.SVG_NS ? m.box : m.box.parentNode).appendChild(e); e.style.pointerEvents = "auto" }) })
            }; p.prototype.applyFixed = function () {
                var p, m, q = !this.fixedDiv, M =
                    this.options.chart.scrollablePlotArea; q ? (this.fixedDiv = D("div", { className: "highcharts-fixed" }, { position: "absolute", overflow: "hidden", pointerEvents: "none", zIndex: 2 }, null, !0), this.renderTo.insertBefore(this.fixedDiv, this.renderTo.firstChild), this.renderTo.style.overflow = "visible", this.fixedRenderer = m = new e.Renderer(this.fixedDiv, this.chartWidth, this.chartHeight, null === (p = this.options.chart) || void 0 === p ? void 0 : p.style), this.scrollableMask = m.path().attr({
                        fill: this.options.chart.backgroundColor || "#fff",
                        "fill-opacity": z(M.opacity, .85), zIndex: -1
                    }).addClass("highcharts-scrollable-mask").add(), this.moveFixedElements(), B(this, "afterShowResetZoom", this.moveFixedElements), B(this, "afterLayOutTitles", this.moveFixedElements)) : this.fixedRenderer.setSize(this.chartWidth, this.chartHeight); p = this.chartWidth + (this.scrollablePixelsX || 0); m = this.chartHeight + (this.scrollablePixelsY || 0); J(this.container); this.container.style.width = p + "px"; this.container.style.height = m + "px"; this.renderer.boxWrapper.attr({
                        width: p, height: m,
                        viewBox: [0, 0, p, m].join(" ")
                    }); this.chartBackground.attr({ width: p, height: m }); this.scrollingContainer.style.height = this.chartHeight + "px"; q && (M.scrollPositionX && (this.scrollingContainer.scrollLeft = this.scrollablePixelsX * M.scrollPositionX), M.scrollPositionY && (this.scrollingContainer.scrollTop = this.scrollablePixelsY * M.scrollPositionY)); m = this.axisOffset; q = this.plotTop - m[0] - 1; M = this.plotLeft - m[3] - 1; p = this.plotTop + this.plotHeight + m[2] + 1; m = this.plotLeft + this.plotWidth + m[1] + 1; var A = this.plotLeft + this.plotWidth -
                        (this.scrollablePixelsX || 0), K = this.plotTop + this.plotHeight - (this.scrollablePixelsY || 0); q = this.scrollablePixelsX ? [["M", 0, q], ["L", this.plotLeft - 1, q], ["L", this.plotLeft - 1, p], ["L", 0, p], ["Z"], ["M", A, q], ["L", this.chartWidth, q], ["L", this.chartWidth, p], ["L", A, p], ["Z"]] : this.scrollablePixelsY ? [["M", M, 0], ["L", M, this.plotTop - 1], ["L", m, this.plotTop - 1], ["L", m, 0], ["Z"], ["M", M, K], ["L", M, this.chartHeight], ["L", m, this.chartHeight], ["L", m, K], ["Z"]] : [["M", 0, 0]]; "adjustHeight" !== this.redrawTrigger && this.scrollableMask.attr({ d: q })
            }
        });
    O(q, "parts/StackingAxis.js", [q["parts/Utilities.js"]], function (p) {
        var e = p.addEvent, q = p.destroyObjectProperties, B = p.fireEvent, D = p.objectEach, z = p.pick, J = function () {
            function e(m) { this.oldStacks = {}; this.stacks = {}; this.stacksTouched = 0; this.axis = m } e.prototype.buildStacks = function () {
                var m = this.axis, e = m.series, p = z(m.options.reversedStacks, !0), q = e.length, K; if (!m.isXAxis) {
                this.usePercentage = !1; for (K = q; K--;) { var w = e[p ? K : q - K - 1]; w.setStackedPoints(); w.setGroupedPoints() } for (K = 0; K < q; K++)e[K].modifyStacks(); B(m,
                    "afterBuildStacks")
                }
            }; e.prototype.cleanStacks = function () { if (!this.axis.isXAxis) { if (this.oldStacks) var m = this.stacks = this.oldStacks; D(m, function (m) { D(m, function (m) { m.cumulative = m.total }) }) } }; e.prototype.resetStacks = function () { var m = this, e = m.stacks; m.axis.isXAxis || D(e, function (e) { D(e, function (p, q) { p.touched < m.stacksTouched ? (p.destroy(), delete e[q]) : (p.total = null, p.cumulative = null) }) }) }; e.prototype.renderStackTotals = function () {
                var m = this.axis.chart, e = m.renderer, p = this.stacks, q = this.stackTotalGroup = this.stackTotalGroup ||
                    e.g("stack-labels").attr({ visibility: "visible", zIndex: 6 }).add(); q.translate(m.plotLeft, m.plotTop); D(p, function (m) { D(m, function (m) { m.render(q) }) })
            }; return e
        }(); return function () { function p() { } p.compose = function (m) { e(m, "init", p.onInit); e(m, "destroy", p.onDestroy) }; p.onDestroy = function () { var m = this.stacking; if (m) { var e = m.stacks; D(e, function (m, p) { q(m); e[p] = null }); m && m.stackTotalGroup && m.stackTotalGroup.destroy() } }; p.onInit = function () { this.stacking || (this.stacking = new J(this)) }; return p }()
    }); O(q, "mixins/legend-symbol.js",
        [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
            var q = e.merge, B = e.pick; p.LegendSymbolMixin = {
                drawRectangle: function (e, p) { var q = e.symbolHeight, z = e.options.squareSymbol; p.legendSymbol = this.chart.renderer.rect(z ? (e.symbolWidth - q) / 2 : 0, e.baseline - q + 1, z ? q : e.symbolWidth, q, B(e.options.symbolRadius, q / 2)).addClass("highcharts-point").attr({ zIndex: 3 }).add(p.legendGroup) }, drawLineMarker: function (e) {
                    var p = this.options, J = p.marker, G = e.symbolWidth, m = e.symbolHeight, H = m / 2, D = this.chart.renderer, A = this.legendGroup;
                    e = e.baseline - Math.round(.3 * e.fontMetrics.b); var K = {}; this.chart.styledMode || (K = { "stroke-width": p.lineWidth || 0 }, p.dashStyle && (K.dashstyle = p.dashStyle)); this.legendLine = D.path(["M", 0, e, "L", G, e]).addClass("highcharts-graph").attr(K).add(A); J && !1 !== J.enabled && G && (p = Math.min(B(J.radius, H), H), 0 === this.symbol.indexOf("url") && (J = q(J, { width: m, height: m }), p = 0), this.legendSymbol = J = D.symbol(this.symbol, G / 2 - p, e - p, 2 * p, 2 * p, J).addClass("highcharts-point").add(A), J.isMarker = !0)
                }
            }; return p.LegendSymbolMixin
        }); O(q, "parts/Point.js",
            [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
                var q = e.animObject, B = e.defined, D = e.erase, z = e.extend, J = e.fireEvent, G = e.format, m = e.getNestedProperty, H = e.isArray, M = e.isNumber, A = e.isObject, K = e.syncTimeout, w = e.pick, r = e.removeEvent, u = e.uniqueKey; ""; e = function () {
                    function e() { this.colorIndex = this.category = void 0; this.formatPrefix = "point"; this.id = void 0; this.isNull = !1; this.percentage = this.options = this.name = void 0; this.selected = !1; this.total = this.series = void 0; this.visible = !0; this.x = void 0 } e.prototype.animateBeforeDestroy =
                        function () { var h = this, f = { x: h.startXPos, opacity: 0 }, d, t = h.getGraphicalProps(); t.singular.forEach(function (l) { d = "dataLabel" === l; h[l] = h[l].animate(d ? { x: h[l].startXPos, y: h[l].startYPos, opacity: 0 } : f) }); t.plural.forEach(function (d) { h[d].forEach(function (c) { c.element && c.animate(z({ x: h.startXPos }, c.startYPos ? { x: c.startXPos, y: c.startYPos } : {})) }) }) }; e.prototype.applyOptions = function (h, f) {
                            var d = this.series, t = d.options.pointValKey || d.pointValKey; h = e.prototype.optionsToObject.call(this, h); z(this, h); this.options =
                                this.options ? z(this.options, h) : h; h.group && delete this.group; h.dataLabels && delete this.dataLabels; t && (this.y = e.prototype.getNestedProperty.call(this, t)); this.formatPrefix = (this.isNull = w(this.isValid && !this.isValid(), null === this.x || !M(this.y))) ? "null" : "point"; this.selected && (this.state = "select"); "name" in this && "undefined" === typeof f && d.xAxis && d.xAxis.hasNames && (this.x = d.xAxis.nameToX(this)); "undefined" === typeof this.x && d && (this.x = "undefined" === typeof f ? d.autoIncrement(this) : f); return this
                        }; e.prototype.destroy =
                            function () { function h() { if (f.graphic || f.dataLabel || f.dataLabels) r(f), f.destroyElements(); for (a in f) f[a] = null } var f = this, d = f.series, t = d.chart; d = d.options.dataSorting; var l = t.hoverPoints, c = q(f.series.chart.renderer.globalAnimation), a; f.legendItem && t.legend.destroyItem(f); l && (f.setState(), D(l, f), l.length || (t.hoverPoints = null)); if (f === t.hoverPoint) f.onMouseOut(); d && d.enabled ? (this.animateBeforeDestroy(), K(h, c.duration)) : h(); t.pointCount-- }; e.prototype.destroyElements = function (h) {
                                var f = this; h = f.getGraphicalProps(h);
                                h.singular.forEach(function (d) { f[d] = f[d].destroy() }); h.plural.forEach(function (d) { f[d].forEach(function (d) { d.element && d.destroy() }); delete f[d] })
                            }; e.prototype.firePointEvent = function (h, f, d) { var t = this, l = this.series.options; (l.point.events[h] || t.options && t.options.events && t.options.events[h]) && t.importEvents(); "click" === h && l.allowPointSelect && (d = function (c) { t.select && t.select(null, c.ctrlKey || c.metaKey || c.shiftKey) }); J(t, h, f, d) }; e.prototype.getClassName = function () {
                                return "highcharts-point" + (this.selected ?
                                    " highcharts-point-select" : "") + (this.negative ? " highcharts-negative" : "") + (this.isNull ? " highcharts-null-point" : "") + ("undefined" !== typeof this.colorIndex ? " highcharts-color-" + this.colorIndex : "") + (this.options.className ? " " + this.options.className : "") + (this.zone && this.zone.className ? " " + this.zone.className.replace("highcharts-negative", "") : "")
                            }; e.prototype.getGraphicalProps = function (h) {
                                var f = this, d = [], t, l = { singular: [], plural: [] }; h = h || { graphic: 1, dataLabel: 1 }; h.graphic && d.push("graphic", "shadowGroup");
                                h.dataLabel && d.push("dataLabel", "dataLabelUpper", "connector"); for (t = d.length; t--;) { var c = d[t]; f[c] && l.singular.push(c) } ["dataLabel", "connector"].forEach(function (a) { var c = a + "s"; h[a] && f[c] && l.plural.push(c) }); return l
                            }; e.prototype.getLabelConfig = function () { return { x: this.category, y: this.y, color: this.color, colorIndex: this.colorIndex, key: this.name || this.category, series: this.series, point: this, percentage: this.percentage, total: this.total || this.stackTotal } }; e.prototype.getNestedProperty = function (h) {
                                if (h) return 0 ===
                                    h.indexOf("custom.") ? m(h, this.options) : this[h]
                            }; e.prototype.getZone = function () { var h = this.series, f = h.zones; h = h.zoneAxis || "y"; var d = 0, t; for (t = f[d]; this[h] >= t.value;)t = f[++d]; this.nonZonedColor || (this.nonZonedColor = this.color); this.color = t && t.color && !this.options.color ? t.color : this.nonZonedColor; return t }; e.prototype.hasNewShapeType = function () { return (this.graphic && (this.graphic.symbolName || this.graphic.element.nodeName)) !== this.shapeType }; e.prototype.init = function (h, f, d) {
                            this.series = h; this.applyOptions(f,
                                d); this.id = B(this.id) ? this.id : u(); this.resolveColor(); h.chart.pointCount++; J(this, "afterInit"); return this
                            }; e.prototype.optionsToObject = function (h) {
                                var f = {}, d = this.series, t = d.options.keys, l = t || d.pointArrayMap || ["y"], c = l.length, a = 0, r = 0; if (M(h) || null === h) f[l[0]] = h; else if (H(h)) for (!t && h.length > c && (d = typeof h[0], "string" === d ? f.name = h[0] : "number" === d && (f.x = h[0]), a++); r < c;)t && "undefined" === typeof h[a] || (0 < l[r].indexOf(".") ? e.prototype.setNestedProperty(f, h[a], l[r]) : f[l[r]] = h[a]), a++, r++; else "object" ===
                                    typeof h && (f = h, h.dataLabels && (d._hasPointLabels = !0), h.marker && (d._hasPointMarkers = !0)); return f
                            }; e.prototype.resolveColor = function () {
                                var h = this.series; var f = h.chart.options.chart.colorCount; var d = h.chart.styledMode; delete this.nonZonedColor; d || this.options.color || (this.color = h.color); h.options.colorByPoint ? (d || (f = h.options.colors || h.chart.options.colors, this.color = this.color || f[h.colorCounter], f = f.length), d = h.colorCounter, h.colorCounter++, h.colorCounter === f && (h.colorCounter = 0)) : d = h.colorIndex; this.colorIndex =
                                    w(this.colorIndex, d)
                            }; e.prototype.setNestedProperty = function (h, f, d) { d.split(".").reduce(function (d, h, c, a) { d[h] = a.length - 1 === c ? f : A(d[h], !0) ? d[h] : {}; return d[h] }, h); return h }; e.prototype.tooltipFormatter = function (h) {
                                var f = this.series, d = f.tooltipOptions, t = w(d.valueDecimals, ""), l = d.valuePrefix || "", c = d.valueSuffix || ""; f.chart.styledMode && (h = f.chart.tooltip.styledModeFormat(h)); (f.pointArrayMap || ["y"]).forEach(function (a) {
                                    a = "{point." + a; if (l || c) h = h.replace(RegExp(a + "}", "g"), l + a + "}" + c); h = h.replace(RegExp(a +
                                        "}", "g"), a + ":,." + t + "f}")
                                }); return G(h, { point: this, series: this.series }, f.chart)
                            }; return e
                }(); return p.Point = e
            }); O(q, "parts/Series.js", [q["parts/Globals.js"], q["mixins/legend-symbol.js"], q["parts/Options.js"], q["parts/Point.js"], q["parts/SVGElement.js"], q["parts/Utilities.js"]], function (p, e, q, B, D, z) {
                var J = q.defaultOptions, G = z.addEvent, m = z.animObject, H = z.arrayMax, M = z.arrayMin, A = z.clamp, K = z.correctFloat, w = z.defined, r = z.erase, u = z.error, C = z.extend, h = z.find, f = z.fireEvent, d = z.getNestedProperty, t = z.isArray,
                l = z.isFunction, c = z.isNumber, a = z.isString, x = z.merge, v = z.objectEach, E = z.pick, F = z.removeEvent; q = z.seriesType; var k = z.splat, y = z.syncTimeout; ""; var I = p.seriesTypes, P = p.win; p.Series = q("line", null, {
                    lineWidth: 2, allowPointSelect: !1, crisp: !0, showCheckbox: !1, animation: { duration: 1E3 }, events: {}, marker: {
                        enabledThreshold: 2, lineColor: "#ffffff", lineWidth: 0, radius: 4, states: {
                            normal: { animation: !0 }, hover: { animation: { duration: 50 }, enabled: !0, radiusPlus: 2, lineWidthPlus: 1 }, select: {
                                fillColor: "#cccccc", lineColor: "#000000",
                                lineWidth: 2
                            }
                        }
                    }, point: { events: {} }, dataLabels: { align: "center", formatter: function () { var a = this.series.chart.numberFormatter; return "number" !== typeof this.y ? "" : a(this.y, -1) }, padding: 5, style: { fontSize: "11px", fontWeight: "bold", color: "contrast", textOutline: "1px contrast" }, verticalAlign: "bottom", x: 0, y: 0 }, cropThreshold: 300, opacity: 1, pointRange: 0, softThreshold: !0, states: {
                        normal: { animation: !0 }, hover: { animation: { duration: 50 }, lineWidthPlus: 1, marker: {}, halo: { size: 10, opacity: .25 } }, select: { animation: { duration: 0 } },
                        inactive: { animation: { duration: 50 }, opacity: .2 }
                    }, stickyTracking: !0, turboThreshold: 1E3, findNearestPointBy: "x"
                }, {
                    axisTypes: ["xAxis", "yAxis"], coll: "series", colorCounter: 0, cropShoulder: 1, directTouch: !1, eventsToUnbind: [], isCartesian: !0, parallelArrays: ["x", "y"], pointClass: B, requireSorting: !0, sorted: !0, init: function (a, b) {
                        f(this, "init", { options: b }); var g = this, c = a.series, d; this.eventOptions = this.eventOptions || {}; g.chart = a; g.options = b = g.setOptions(b); g.linkedSeries = []; g.bindAxes(); C(g, {
                            name: b.name, state: "", visible: !1 !==
                                b.visible, selected: !0 === b.selected
                        }); var k = b.events; v(k, function (a, b) { l(a) && g.eventOptions[b] !== a && (l(g.eventOptions[b]) && F(g, b, g.eventOptions[b]), g.eventOptions[b] = a, G(g, b, a)) }); if (k && k.click || b.point && b.point.events && b.point.events.click || b.allowPointSelect) a.runTrackerClick = !0; g.getColor(); g.getSymbol(); g.parallelArrays.forEach(function (a) { g[a + "Data"] || (g[a + "Data"] = []) }); g.isCartesian && (a.hasCartesianSeries = !0); c.length && (d = c[c.length - 1]); g._i = E(d && d._i, -1) + 1; g.opacity = g.options.opacity; a.orderSeries(this.insert(c));
                        b.dataSorting && b.dataSorting.enabled ? g.setDataSortingOptions() : g.points || g.data || g.setData(b.data, !1); f(this, "afterInit")
                    }, is: function (a) { return I[a] && this instanceof I[a] }, insert: function (a) { var b = this.options.index, g; if (c(b)) { for (g = a.length; g--;)if (b >= E(a[g].options.index, a[g]._i)) { a.splice(g + 1, 0, this); break } -1 === g && a.unshift(this); g += 1 } else a.push(this); return E(g, a.length - 1) }, bindAxes: function () {
                        var a = this, b = a.options, c = a.chart, d; f(this, "bindAxes", null, function () {
                            (a.axisTypes || []).forEach(function (g) {
                                c[g].forEach(function (c) {
                                    d =
                                    c.options; if (b[g] === d.index || "undefined" !== typeof b[g] && b[g] === d.id || "undefined" === typeof b[g] && 0 === d.index) a.insert(c.series), a[g] = c, c.isDirty = !0
                                }); a[g] || a.optionalAxis === g || u(18, !0, c)
                            })
                        }); f(this, "afterBindAxes")
                    }, updateParallelArrays: function (a, b) { var g = a.series, d = arguments, k = c(b) ? function (c) { var d = "y" === c && g.toYData ? g.toYData(a) : a[c]; g[c + "Data"][b] = d } : function (a) { Array.prototype[b].apply(g[a + "Data"], Array.prototype.slice.call(d, 2)) }; g.parallelArrays.forEach(k) }, hasData: function () {
                        return this.visible &&
                            "undefined" !== typeof this.dataMax && "undefined" !== typeof this.dataMin || this.visible && this.yData && 0 < this.yData.length
                    }, autoIncrement: function () {
                        var a = this.options, b = this.xIncrement, c, d = a.pointIntervalUnit, k = this.chart.time; b = E(b, a.pointStart, 0); this.pointInterval = c = E(this.pointInterval, a.pointInterval, 1); d && (a = new k.Date(b), "day" === d ? k.set("Date", a, k.get("Date", a) + c) : "month" === d ? k.set("Month", a, k.get("Month", a) + c) : "year" === d && k.set("FullYear", a, k.get("FullYear", a) + c), c = a.getTime() - b); this.xIncrement =
                            b + c; return b
                    }, setDataSortingOptions: function () { var a = this.options; C(this, { requireSorting: !1, sorted: !1, enabledDataSorting: !0, allowDG: !1 }); w(a.pointRange) || (a.pointRange = 1) }, setOptions: function (a) {
                        var b = this.chart, g = b.options, c = g.plotOptions, d = b.userOptions || {}; a = x(a); b = b.styledMode; var k = { plotOptions: c, userOptions: a }; f(this, "setOptions", k); var h = k.plotOptions[this.type], l = d.plotOptions || {}; this.userOptions = k.userOptions; d = x(h, c.series, d.plotOptions && d.plotOptions[this.type], a); this.tooltipOptions =
                            x(J.tooltip, J.plotOptions.series && J.plotOptions.series.tooltip, J.plotOptions[this.type].tooltip, g.tooltip.userOptions, c.series && c.series.tooltip, c[this.type].tooltip, a.tooltip); this.stickyTracking = E(a.stickyTracking, l[this.type] && l[this.type].stickyTracking, l.series && l.series.stickyTracking, this.tooltipOptions.shared && !this.noSharedTooltip ? !0 : d.stickyTracking); null === h.marker && delete d.marker; this.zoneAxis = d.zoneAxis; g = this.zones = (d.zones || []).slice(); !d.negativeColor && !d.negativeFillColor || d.zones ||
                                (c = { value: d[this.zoneAxis + "Threshold"] || d.threshold || 0, className: "highcharts-negative" }, b || (c.color = d.negativeColor, c.fillColor = d.negativeFillColor), g.push(c)); g.length && w(g[g.length - 1].value) && g.push(b ? {} : { color: this.color, fillColor: this.fillColor }); f(this, "afterSetOptions", { options: d }); return d
                    }, getName: function () { return E(this.options.name, "Series " + (this.index + 1)) }, getCyclic: function (a, b, c) {
                        var g = this.chart, d = this.userOptions, k = a + "Index", n = a + "Counter", f = c ? c.length : E(g.options.chart[a + "Count"],
                            g[a + "Count"]); if (!b) { var h = E(d[k], d["_" + k]); w(h) || (g.series.length || (g[n] = 0), d["_" + k] = h = g[n] % f, g[n] += 1); c && (b = c[h]) } "undefined" !== typeof h && (this[k] = h); this[a] = b
                    }, getColor: function () { this.chart.styledMode ? this.getCyclic("color") : this.options.colorByPoint ? this.options.color = null : this.getCyclic("color", this.options.color || J.plotOptions[this.type].color, this.chart.options.colors) }, getPointsCollection: function () { return (this.hasGroupedData ? this.points : this.data) || [] }, getSymbol: function () {
                        this.getCyclic("symbol",
                            this.options.marker.symbol, this.chart.options.symbols)
                    }, findPointIndex: function (a, b) {
                        var g = a.id, d = a.x, k = this.points, f, l = this.options.dataSorting; if (g) var t = this.chart.get(g); else if (this.linkedParent || this.enabledDataSorting) { var v = l && l.matchByName ? "name" : "index"; t = h(k, function (b) { return !b.touched && b[v] === a[v] }); if (!t) return } if (t) { var y = t && t.index; "undefined" !== typeof y && (f = !0) } "undefined" === typeof y && c(d) && (y = this.xData.indexOf(d, b)); -1 !== y && "undefined" !== typeof y && this.cropped && (y = y >= this.cropStart ?
                            y - this.cropStart : y); !f && k[y] && k[y].touched && (y = void 0); return y
                    }, drawLegendSymbol: e.drawLineMarker, updateData: function (a, b) {
                        var g = this.options, d = g.dataSorting, k = this.points, f = [], h, l, t, y = this.requireSorting, v = a.length === k.length, r = !0; this.xIncrement = null; a.forEach(function (a, b) {
                            var n = w(a) && this.pointClass.prototype.optionsToObject.call({ series: this }, a) || {}; var l = n.x; if (n.id || c(l)) {
                                if (l = this.findPointIndex(n, t), -1 === l || "undefined" === typeof l ? f.push(a) : k[l] && a !== g.data[l] ? (k[l].update(a, !1, null, !1),
                                    k[l].touched = !0, y && (t = l + 1)) : k[l] && (k[l].touched = !0), !v || b !== l || d && d.enabled || this.hasDerivedData) h = !0
                            } else f.push(a)
                        }, this); if (h) for (a = k.length; a--;)(l = k[a]) && !l.touched && l.remove && l.remove(!1, b); else !v || d && d.enabled ? r = !1 : (a.forEach(function (a, b) { k[b].update && a !== k[b].y && k[b].update(a, !1, null, !1) }), f.length = 0); k.forEach(function (a) { a && (a.touched = !1) }); if (!r) return !1; f.forEach(function (a) { this.addPoint(a, !1, null, null, !1) }, this); null === this.xIncrement && this.xData && this.xData.length && (this.xIncrement =
                            H(this.xData), this.autoIncrement()); return !0
                    }, setData: function (g, b, d, k) {
                        var n = this, f = n.points, h = f && f.length || 0, l, y = n.options, v = n.chart, r = y.dataSorting, m = null, e = n.xAxis; m = y.turboThreshold; var x = this.xData, F = this.yData, I = (l = n.pointArrayMap) && l.length, L = y.keys, w = 0, C = 1, p; g = g || []; l = g.length; b = E(b, !0); r && r.enabled && (g = this.sortData(g)); !1 !== k && l && h && !n.cropped && !n.hasGroupedData && n.visible && !n.isSeriesBoosting && (p = this.updateData(g, d)); if (!p) {
                        n.xIncrement = null; n.colorCounter = 0; this.parallelArrays.forEach(function (a) {
                            n[a +
                            "Data"].length = 0
                        }); if (m && l > m) if (m = n.getFirstValidPoint(g), c(m)) for (d = 0; d < l; d++)x[d] = this.autoIncrement(), F[d] = g[d]; else if (t(m)) if (I) for (d = 0; d < l; d++)k = g[d], x[d] = k[0], F[d] = k.slice(1, I + 1); else for (L && (w = L.indexOf("x"), C = L.indexOf("y"), w = 0 <= w ? w : 0, C = 0 <= C ? C : 1), d = 0; d < l; d++)k = g[d], x[d] = k[w], F[d] = k[C]; else u(12, !1, v); else for (d = 0; d < l; d++)"undefined" !== typeof g[d] && (k = { series: n }, n.pointClass.prototype.applyOptions.apply(k, [g[d]]), n.updateParallelArrays(k, d)); F && a(F[0]) && u(14, !0, v); n.data = []; n.options.data =
                            n.userOptions.data = g; for (d = h; d--;)f[d] && f[d].destroy && f[d].destroy(); e && (e.minRange = e.userMinRange); n.isDirty = v.isDirtyBox = !0; n.isDirtyData = !!f; d = !1
                        } "point" === y.legendType && (this.processData(), this.generatePoints()); b && v.redraw(d)
                    }, sortData: function (a) {
                        var b = this, g = b.options.dataSorting.sortKey || "y", c = function (a, b) { return w(b) && a.pointClass.prototype.optionsToObject.call({ series: a }, b) || {} }; a.forEach(function (g, d) { a[d] = c(b, g); a[d].index = d }, this); a.concat().sort(function (a, b) {
                            a = d(g, a); b = d(g, b); return b <
                                a ? -1 : b > a ? 1 : 0
                        }).forEach(function (a, b) { a.x = b }, this); b.linkedSeries && b.linkedSeries.forEach(function (b) { var g = b.options, d = g.data; g.dataSorting && g.dataSorting.enabled || !d || (d.forEach(function (g, k) { d[k] = c(b, g); a[k] && (d[k].x = a[k].x, d[k].index = k) }), b.setData(d, !1)) }); return a
                    }, getProcessedData: function (a) {
                        var b = this.xData, g = this.yData, c = b.length; var d = 0; var k = this.xAxis, f = this.options; var l = f.cropThreshold; var h = a || this.getExtremesFromAll || f.getExtremesFromAll, t = this.isCartesian; a = k && k.val2lin; f = !(!k || !k.logarithmic);
                        var y = this.requireSorting; if (k) { k = k.getExtremes(); var v = k.min; var r = k.max } if (t && this.sorted && !h && (!l || c > l || this.forceCrop)) if (b[c - 1] < v || b[0] > r) b = [], g = []; else if (this.yData && (b[0] < v || b[c - 1] > r)) { d = this.cropData(this.xData, this.yData, v, r); b = d.xData; g = d.yData; d = d.start; var m = !0 } for (l = b.length || 1; --l;)if (c = f ? a(b[l]) - a(b[l - 1]) : b[l] - b[l - 1], 0 < c && ("undefined" === typeof e || c < e)) var e = c; else 0 > c && y && (u(15, !1, this.chart), y = !1); return { xData: b, yData: g, cropped: m, cropStart: d, closestPointRange: e }
                    }, processData: function (a) {
                        var b =
                            this.xAxis; if (this.isCartesian && !this.isDirty && !b.isDirty && !this.yAxis.isDirty && !a) return !1; a = this.getProcessedData(); this.cropped = a.cropped; this.cropStart = a.cropStart; this.processedXData = a.xData; this.processedYData = a.yData; this.closestPointRange = this.basePointRange = a.closestPointRange
                    }, cropData: function (a, b, c, d, k) {
                        var g = a.length, n = 0, f = g, l; k = E(k, this.cropShoulder); for (l = 0; l < g; l++)if (a[l] >= c) { n = Math.max(0, l - k); break } for (c = l; c < g; c++)if (a[c] > d) { f = c + k; break } return {
                            xData: a.slice(n, f), yData: b.slice(n, f),
                            start: n, end: f
                        }
                    }, generatePoints: function () {
                        var a = this.options, b = a.data, c = this.data, d, l = this.processedXData, h = this.processedYData, t = this.pointClass, y = l.length, v = this.cropStart || 0, r = this.hasGroupedData; a = a.keys; var m = [], e; c || r || (c = [], c.length = b.length, c = this.data = c); a && r && (this.options.keys = !1); for (e = 0; e < y; e++) {
                            var u = v + e; if (r) { var x = (new t).init(this, [l[e]].concat(k(h[e]))); x.dataGroup = this.groupMap[e]; x.dataGroup.options && (x.options = x.dataGroup.options, C(x, x.dataGroup.options), delete x.dataLabels) } else (x =
                                c[u]) || "undefined" === typeof b[u] || (c[u] = x = (new t).init(this, b[u], l[e])); x && (x.index = u, m[e] = x)
                        } this.options.keys = a; if (c && (y !== (d = c.length) || r)) for (e = 0; e < d; e++)e !== v || r || (e += y), c[e] && (c[e].destroyElements(), c[e].plotX = void 0); this.data = c; this.points = m; f(this, "afterGeneratePoints")
                    }, getXExtremes: function (a) { return { min: M(a), max: H(a) } }, getExtremes: function (a, b) {
                        var g = this.xAxis, d = this.yAxis, k = this.processedXData || this.xData, l = [], h = 0, y = 0; var v = 0; var r = this.requireSorting ? this.cropShoulder : 0, e = d ? d.positiveValuesOnly :
                            !1, m; a = a || this.stackedYData || this.processedYData || []; d = a.length; g && (v = g.getExtremes(), y = v.min, v = v.max); for (m = 0; m < d; m++) { var u = k[m]; var x = a[m]; var F = (c(x) || t(x)) && (x.length || 0 < x || !e); u = b || this.getExtremesFromAll || this.options.getExtremesFromAll || this.cropped || !g || (k[m + r] || u) >= y && (k[m - r] || u) <= v; if (F && u) if (F = x.length) for (; F--;)c(x[F]) && (l[h++] = x[F]); else l[h++] = x } a = { dataMin: M(l), dataMax: H(l) }; f(this, "afterGetExtremes", { dataExtremes: a }); return a
                    }, applyExtremes: function () {
                        var a = this.getExtremes(); this.dataMin =
                            a.dataMin; this.dataMax = a.dataMax; return a
                    }, getFirstValidPoint: function (a) { for (var b = null, g = a.length, c = 0; null === b && c < g;)b = a[c], c++; return b }, translate: function () {
                    this.processedXData || this.processData(); this.generatePoints(); var a = this.options, b = a.stacking, d = this.xAxis, k = d.categories, l = this.enabledDataSorting, h = this.yAxis, y = this.points, v = y.length, r = !!this.modifyValue, m, e = this.pointPlacementToXValue(), u = !!e, x = a.threshold, F = a.startFromThreshold ? x : 0, I, C = this.zoneAxis || "y", p = Number.MAX_VALUE; for (m = 0; m <
                        v; m++) {
                            var q = y[m], P = q.x, z = q.y, G = q.low, B = b && h.stacking && h.stacking.stacks[(this.negStacks && z < (F ? 0 : x) ? "-" : "") + this.stackKey]; h.positiveValuesOnly && null !== z && 0 >= z && (q.isNull = !0); q.plotX = I = K(A(d.translate(P, 0, 0, 0, 1, e, "flags" === this.type), -1E5, 1E5)); if (b && this.visible && B && B[P]) { var H = this.getStackIndicator(H, P, this.index); if (!q.isNull) { var J = B[P]; var D = J.points[H.key] } } t(D) && (G = D[0], z = D[1], G === F && H.key === B[P].base && (G = E(c(x) && x, h.min)), h.positiveValuesOnly && 0 >= G && (G = null), q.total = q.stackTotal = J.total,
                                q.percentage = J.total && q.y / J.total * 100, q.stackY = z, this.irregularWidths || J.setOffset(this.pointXOffset || 0, this.barW || 0)); q.yBottom = w(G) ? A(h.translate(G, 0, 1, 0, 1), -1E5, 1E5) : null; r && (z = this.modifyValue(z, q)); q.plotY = "number" === typeof z && Infinity !== z ? A(h.translate(z, 0, 1, 0, 1), -1E5, 1E5) : void 0; q.isInside = this.isPointInside(q); q.clientX = u ? K(d.translate(P, 0, 0, 0, 1, e)) : I; q.negative = q[C] < (a[C + "Threshold"] || x || 0); q.category = k && "undefined" !== typeof k[q.x] ? k[q.x] : q.x; if (!q.isNull && !1 !== q.visible) {
                                "undefined" !== typeof M &&
                                    (p = Math.min(p, Math.abs(I - M))); var M = I
                                } q.zone = this.zones.length && q.getZone(); !q.graphic && this.group && l && (q.isNew = !0)
                    } this.closestPointRangePx = p; f(this, "afterTranslate")
                    }, getValidPoints: function (a, b, c) { var g = this.chart; return (a || this.points || []).filter(function (a) { return b && !g.isInsidePlot(a.plotX, a.plotY, g.inverted) ? !1 : !1 !== a.visible && (c || !a.isNull) }) }, getClipBox: function (a, b) {
                        var g = this.options, c = this.chart, d = c.inverted, k = this.xAxis, f = k && this.yAxis, l = c.options.chart.scrollablePlotArea || {}; a && !1 ===
                            g.clip && f ? a = d ? { y: -c.chartWidth + f.len + f.pos, height: c.chartWidth, width: c.chartHeight, x: -c.chartHeight + k.len + k.pos } : { y: -f.pos, height: c.chartHeight, width: c.chartWidth, x: -k.pos } : (a = this.clipBox || c.clipBox, b && (a.width = c.plotSizeX, a.x = (c.scrollablePixelsX || 0) * (l.scrollPositionX || 0))); return b ? { width: a.width, x: a.x } : a
                    }, setClip: function (a) {
                        var b = this.chart, g = this.options, c = b.renderer, d = b.inverted, k = this.clipBox, f = this.getClipBox(a), l = this.sharedClipKey || ["_sharedClip", a && a.duration, a && a.easing, f.height, g.xAxis,
                            g.yAxis].join(), h = b[l], t = b[l + "m"]; a && (f.width = 0, d && (f.x = b.plotHeight + (!1 !== g.clip ? 0 : b.plotTop))); h ? b.hasLoaded || h.attr(f) : (a && (b[l + "m"] = t = c.clipRect(d ? b.plotSizeX + 99 : -99, d ? -b.plotLeft : -b.plotTop, 99, d ? b.chartWidth : b.chartHeight)), b[l] = h = c.clipRect(f), h.count = { length: 0 }); a && !h.count[this.index] && (h.count[this.index] = !0, h.count.length += 1); if (!1 !== g.clip || a) this.group.clip(a || k ? h : b.clipRect), this.markerGroup.clip(t), this.sharedClipKey = l; a || (h.count[this.index] && (delete h.count[this.index], --h.count.length),
                                0 === h.count.length && l && b[l] && (k || (b[l] = b[l].destroy()), b[l + "m"] && (b[l + "m"] = b[l + "m"].destroy())))
                    }, animate: function (a) { var b = this.chart, g = m(this.options.animation); if (!b.hasRendered) if (a) this.setClip(g); else { var c = this.sharedClipKey; a = b[c]; var d = this.getClipBox(g, !0); a && a.animate(d, g); b[c + "m"] && b[c + "m"].animate({ width: d.width + 99, x: d.x - (b.inverted ? 0 : 99) }, g) } }, afterAnimate: function () { this.setClip(); f(this, "afterAnimate"); this.finishedAnimating = !0 }, drawPoints: function () {
                        var a = this.points, b = this.chart,
                        c, d, k = this.options.marker, f = this[this.specialGroup] || this.markerGroup, l = this.xAxis, h = E(k.enabled, !l || l.isRadial ? !0 : null, this.closestPointRangePx >= k.enabledThreshold * k.radius); if (!1 !== k.enabled || this._hasPointMarkers) for (c = 0; c < a.length; c++) {
                            var t = a[c]; var y = (d = t.graphic) ? "animate" : "attr"; var v = t.marker || {}; var r = !!t.marker; if ((h && "undefined" === typeof v.enabled || v.enabled) && !t.isNull && !1 !== t.visible) {
                                var m = E(v.symbol, this.symbol); var e = this.markerAttribs(t, t.selected && "select"); this.enabledDataSorting &&
                                    (t.startXPos = l.reversed ? -e.width : l.width); var u = !1 !== t.isInside; d ? d[u ? "show" : "hide"](u).animate(e) : u && (0 < e.width || t.hasImage) && (t.graphic = d = b.renderer.symbol(m, e.x, e.y, e.width, e.height, r ? v : k).add(f), this.enabledDataSorting && b.hasRendered && (d.attr({ x: t.startXPos }), y = "animate")); d && "animate" === y && d[u ? "show" : "hide"](u).animate(e); if (d && !b.styledMode) d[y](this.pointAttribs(t, t.selected && "select")); d && d.addClass(t.getClassName(), !0)
                            } else d && (t.graphic = d.destroy())
                        }
                    }, markerAttribs: function (a, b) {
                        var c = this.options,
                        g = c.marker, d = a.marker || {}, k = d.symbol || g.symbol, f = E(d.radius, g.radius); b && (g = g.states[b], b = d.states && d.states[b], f = E(b && b.radius, g && g.radius, f + (g && g.radiusPlus || 0))); a.hasImage = k && 0 === k.indexOf("url"); a.hasImage && (f = 0); a = { x: c.crisp ? Math.floor(a.plotX) - f : a.plotX - f, y: a.plotY - f }; f && (a.width = a.height = 2 * f); return a
                    }, pointAttribs: function (a, b) {
                        var c = this.options.marker, g = a && a.options, d = g && g.marker || {}, k = this.color, f = g && g.color, l = a && a.color; g = E(d.lineWidth, c.lineWidth); var h = a && a.zone && a.zone.color; a = 1; k =
                            f || h || l || k; f = d.fillColor || c.fillColor || k; k = d.lineColor || c.lineColor || k; b = b || "normal"; c = c.states[b]; b = d.states && d.states[b] || {}; g = E(b.lineWidth, c.lineWidth, g + E(b.lineWidthPlus, c.lineWidthPlus, 0)); f = b.fillColor || c.fillColor || f; k = b.lineColor || c.lineColor || k; a = E(b.opacity, c.opacity, a); return { stroke: k, "stroke-width": g, fill: f, opacity: a }
                    }, destroy: function (a) {
                        var b = this, c = b.chart, g = /AppleWebKit\/533/.test(P.navigator.userAgent), d, k, l = b.data || [], h, t; f(b, "destroy"); this.removeEvents(a); (b.axisTypes || []).forEach(function (a) {
                        (t =
                            b[a]) && t.series && (r(t.series, b), t.isDirty = t.forceRedraw = !0)
                        }); b.legendItem && b.chart.legend.destroyItem(b); for (k = l.length; k--;)(h = l[k]) && h.destroy && h.destroy(); b.points = null; z.clearTimeout(b.animationTimeout); v(b, function (a, b) { a instanceof D && !a.survive && (d = g && "group" === b ? "hide" : "destroy", a[d]()) }); c.hoverSeries === b && (c.hoverSeries = null); r(c.series, b); c.orderSeries(); v(b, function (c, g) { a && "hcEvents" === g || delete b[g] })
                    }, getGraphPath: function (a, b, c) {
                        var d = this, g = d.options, k = g.step, n, f = [], l = [], h; a = a ||
                            d.points; (n = a.reversed) && a.reverse(); (k = { right: 1, center: 2 }[k] || k && 3) && n && (k = 4 - k); a = this.getValidPoints(a, !1, !(g.connectNulls && !b && !c)); a.forEach(function (n, t) {
                                var y = n.plotX, v = n.plotY, r = a[t - 1]; (n.leftCliff || r && r.rightCliff) && !c && (h = !0); n.isNull && !w(b) && 0 < t ? h = !g.connectNulls : n.isNull && !b ? h = !0 : (0 === t || h ? t = [["M", n.plotX, n.plotY]] : d.getPointSpline ? t = [d.getPointSpline(a, n, t)] : k ? (t = 1 === k ? [["L", r.plotX, v]] : 2 === k ? [["L", (r.plotX + y) / 2, r.plotY], ["L", (r.plotX + y) / 2, v]] : [["L", y, r.plotY]], t.push(["L", y, v])) : t = [["L",
                                    y, v]], l.push(n.x), k && (l.push(n.x), 2 === k && l.push(n.x)), f.push.apply(f, t), h = !1)
                            }); f.xMap = l; return d.graphPath = f
                    }, drawGraph: function () {
                        var a = this, b = this.options, c = (this.gappedPath || this.getGraphPath).call(this), d = this.chart.styledMode, k = [["graph", "highcharts-graph"]]; d || k[0].push(b.lineColor || this.color || "#cccccc", b.dashStyle); k = a.getZonesGraphs(k); k.forEach(function (g, k) {
                            var n = g[0], f = a[n], l = f ? "animate" : "attr"; f ? (f.endX = a.preventGraphAnimation ? null : c.xMap, f.animate({ d: c })) : c.length && (a[n] = f = a.chart.renderer.path(c).addClass(g[1]).attr({ zIndex: 1 }).add(a.group));
                            f && !d && (n = { stroke: g[2], "stroke-width": b.lineWidth, fill: a.fillGraph && a.color || "none" }, g[3] ? n.dashstyle = g[3] : "square" !== b.linecap && (n["stroke-linecap"] = n["stroke-linejoin"] = "round"), f[l](n).shadow(2 > k && b.shadow)); f && (f.startX = c.xMap, f.isArea = c.isArea)
                        })
                    }, getZonesGraphs: function (a) {
                        this.zones.forEach(function (b, c) { c = ["zone-graph-" + c, "highcharts-graph highcharts-zone-graph-" + c + " " + (b.className || "")]; this.chart.styledMode || c.push(b.color || this.color, b.dashStyle || this.options.dashStyle); a.push(c) }, this);
                        return a
                    }, applyZones: function () {
                        var a = this, b = this.chart, c = b.renderer, d = this.zones, k, f, l = this.clips || [], h, t = this.graph, y = this.area, v = Math.max(b.chartWidth, b.chartHeight), r = this[(this.zoneAxis || "y") + "Axis"], e = b.inverted, m, u, x, F = !1, I, w; if (d.length && (t || y) && r && "undefined" !== typeof r.min) {
                            var C = r.reversed; var p = r.horiz; t && !this.showLine && t.hide(); y && y.hide(); var q = r.getExtremes(); d.forEach(function (d, g) {
                                k = C ? p ? b.plotWidth : 0 : p ? 0 : r.toPixels(q.min) || 0; k = A(E(f, k), 0, v); f = A(Math.round(r.toPixels(E(d.value, q.max),
                                    !0) || 0), 0, v); F && (k = f = r.toPixels(q.max)); m = Math.abs(k - f); u = Math.min(k, f); x = Math.max(k, f); r.isXAxis ? (h = { x: e ? x : u, y: 0, width: m, height: v }, p || (h.x = b.plotHeight - h.x)) : (h = { x: 0, y: e ? x : u, width: v, height: m }, p && (h.y = b.plotWidth - h.y)); e && c.isVML && (h = r.isXAxis ? { x: 0, y: C ? u : x, height: h.width, width: b.chartWidth } : { x: h.y - b.plotLeft - b.spacingBox.x, y: 0, width: h.height, height: b.chartHeight }); l[g] ? l[g].animate(h) : l[g] = c.clipRect(h); I = a["zone-area-" + g]; w = a["zone-graph-" + g]; t && w && w.clip(l[g]); y && I && I.clip(l[g]); F = d.value > q.max;
                                a.resetZones && 0 === f && (f = void 0)
                            }); this.clips = l
                        } else a.visible && (t && t.show(!0), y && y.show(!0))
                    }, invertGroups: function (a) { function b() { ["group", "markerGroup"].forEach(function (b) { c[b] && (d.renderer.isVML && c[b].attr({ width: c.yAxis.len, height: c.xAxis.len }), c[b].width = c.yAxis.len, c[b].height = c.xAxis.len, c[b].invert(c.isRadialSeries ? !1 : a)) }) } var c = this, d = c.chart; c.xAxis && (c.eventsToUnbind.push(G(d, "resize", b)), b(), c.invertGroups = b) }, plotGroup: function (a, b, c, d, k) {
                        var g = this[a], f = !g; c = {
                            visibility: c, zIndex: d ||
                                .1
                        }; "undefined" === typeof this.opacity || this.chart.styledMode || (c.opacity = this.opacity); f && (this[a] = g = this.chart.renderer.g().add(k)); g.addClass("highcharts-" + b + " highcharts-series-" + this.index + " highcharts-" + this.type + "-series " + (w(this.colorIndex) ? "highcharts-color-" + this.colorIndex + " " : "") + (this.options.className || "") + (g.hasClass("highcharts-tracker") ? " highcharts-tracker" : ""), !0); g.attr(c)[f ? "attr" : "animate"](this.getPlotBox()); return g
                    }, getPlotBox: function () {
                        var a = this.chart, b = this.xAxis, c = this.yAxis;
                        a.inverted && (b = c, c = this.xAxis); return { translateX: b ? b.left : a.plotLeft, translateY: c ? c.top : a.plotTop, scaleX: 1, scaleY: 1 }
                    }, removeEvents: function (a) { a ? this.eventsToUnbind.length && (this.eventsToUnbind.forEach(function (a) { a() }), this.eventsToUnbind.length = 0) : F(this) }, render: function () {
                        var a = this, b = a.chart, c = a.options, d = !a.finishedAnimating && b.renderer.isSVG && m(c.animation).duration, k = a.visible ? "inherit" : "hidden", l = c.zIndex, h = a.hasRendered, t = b.seriesGroup, v = b.inverted; f(this, "render"); var r = a.plotGroup("group",
                            "series", k, l, t); a.markerGroup = a.plotGroup("markerGroup", "markers", k, l, t); d && a.animate && a.animate(!0); r.inverted = a.isCartesian || a.invertable ? v : !1; a.drawGraph && (a.drawGraph(), a.applyZones()); a.visible && a.drawPoints(); a.drawDataLabels && a.drawDataLabels(); a.redrawPoints && a.redrawPoints(); a.drawTracker && !1 !== a.options.enableMouseTracking && a.drawTracker(); a.invertGroups(v); !1 === c.clip || a.sharedClipKey || h || r.clip(b.clipRect); d && a.animate && a.animate(); h || (a.animationTimeout = y(function () { a.afterAnimate() },
                                d || 0)); a.isDirty = !1; a.hasRendered = !0; f(a, "afterRender")
                    }, redraw: function () { var a = this.chart, b = this.isDirty || this.isDirtyData, c = this.group, d = this.xAxis, k = this.yAxis; c && (a.inverted && c.attr({ width: a.plotWidth, height: a.plotHeight }), c.animate({ translateX: E(d && d.left, a.plotLeft), translateY: E(k && k.top, a.plotTop) })); this.translate(); this.render(); b && delete this.kdTree }, kdAxisArray: ["clientX", "plotY"], searchPoint: function (a, b) {
                        var c = this.xAxis, d = this.yAxis, g = this.chart.inverted; return this.searchKDTree({
                            clientX: g ?
                                c.len - a.chartY + c.pos : a.chartX - c.pos, plotY: g ? d.len - a.chartX + d.pos : a.chartY - d.pos
                        }, b, a)
                    }, buildKDTree: function (a) {
                        function b(a, d, g) { var k; if (k = a && a.length) { var f = c.kdAxisArray[d % g]; a.sort(function (a, b) { return a[f] - b[f] }); k = Math.floor(k / 2); return { point: a[k], left: b(a.slice(0, k), d + 1, g), right: b(a.slice(k + 1), d + 1, g) } } } this.buildingKdTree = !0; var c = this, d = -1 < c.options.findNearestPointBy.indexOf("y") ? 2 : 1; delete c.kdTree; y(function () { c.kdTree = b(c.getValidPoints(null, !c.directTouch), d, d); c.buildingKdTree = !1 }, c.options.kdNow ||
                            a && "touchstart" === a.type ? 0 : 1)
                    }, searchKDTree: function (a, b, c) {
                        function d(a, b, c, l) { var h = b.point, t = g.kdAxisArray[c % l], y = h; var v = w(a[k]) && w(h[k]) ? Math.pow(a[k] - h[k], 2) : null; var r = w(a[f]) && w(h[f]) ? Math.pow(a[f] - h[f], 2) : null; r = (v || 0) + (r || 0); h.dist = w(r) ? Math.sqrt(r) : Number.MAX_VALUE; h.distX = w(v) ? Math.sqrt(v) : Number.MAX_VALUE; t = a[t] - h[t]; r = 0 > t ? "left" : "right"; v = 0 > t ? "right" : "left"; b[r] && (r = d(a, b[r], c + 1, l), y = r[n] < y[n] ? r : h); b[v] && Math.sqrt(t * t) < y[n] && (a = d(a, b[v], c + 1, l), y = a[n] < y[n] ? a : y); return y } var g = this, k =
                            this.kdAxisArray[0], f = this.kdAxisArray[1], n = b ? "distX" : "dist"; b = -1 < g.options.findNearestPointBy.indexOf("y") ? 2 : 1; this.kdTree || this.buildingKdTree || this.buildKDTree(c); if (this.kdTree) return d(a, this.kdTree, b, b)
                    }, pointPlacementToXValue: function () { var a = this.options, b = a.pointRange, d = this.xAxis; a = a.pointPlacement; "between" === a && (a = d.reversed ? -.5 : .5); return c(a) ? a * E(b, d.pointRange) : 0 }, isPointInside: function (a) {
                        return "undefined" !== typeof a.plotY && "undefined" !== typeof a.plotX && 0 <= a.plotY && a.plotY <= this.yAxis.len &&
                            0 <= a.plotX && a.plotX <= this.xAxis.len
                    }
                }); ""
            }); O(q, "parts/Stacking.js", [q["parts/Axis.js"], q["parts/Chart.js"], q["parts/Globals.js"], q["parts/StackingAxis.js"], q["parts/Utilities.js"]], function (p, e, q, B, D) {
                var z = D.correctFloat, J = D.defined, G = D.destroyObjectProperties, m = D.format, H = D.isNumber, M = D.pick; ""; var A = q.Series, K = function () {
                    function e(r, e, m, h, f) {
                        var d = r.chart.inverted; this.axis = r; this.isNegative = m; this.options = e = e || {}; this.x = h; this.total = null; this.points = {}; this.hasValidPoints = !1; this.stack = f; this.rightCliff =
                            this.leftCliff = 0; this.alignOptions = { align: e.align || (d ? m ? "left" : "right" : "center"), verticalAlign: e.verticalAlign || (d ? "middle" : m ? "bottom" : "top"), y: e.y, x: e.x }; this.textAlign = e.textAlign || (d ? m ? "right" : "left" : "center")
                    } e.prototype.destroy = function () { G(this, this.axis) }; e.prototype.render = function (r) {
                        var e = this.axis.chart, w = this.options, h = w.format; h = h ? m(h, this, e) : w.formatter.call(this); this.label ? this.label.attr({ text: h, visibility: "hidden" }) : (this.label = e.renderer.label(h, null, null, w.shape, null, null, w.useHTML,
                            !1, "stack-labels"), h = { r: w.borderRadius || 0, text: h, rotation: w.rotation, padding: M(w.padding, 5), visibility: "hidden" }, e.styledMode || (h.fill = w.backgroundColor, h.stroke = w.borderColor, h["stroke-width"] = w.borderWidth, this.label.css(w.style)), this.label.attr(h), this.label.added || this.label.add(r)); this.label.labelrank = e.plotHeight
                    }; e.prototype.setOffset = function (r, e, m, h, f) {
                        var d = this.axis, t = d.chart; h = d.translate(d.stacking.usePercentage ? 100 : h ? h : this.total, 0, 0, 0, 1); m = d.translate(m ? m : 0); m = J(h) && Math.abs(h - m);
                        r = M(f, t.xAxis[0].translate(this.x)) + r; d = J(h) && this.getStackBox(t, this, r, h, e, m, d); e = this.label; m = this.isNegative; r = "justify" === M(this.options.overflow, "justify"); var l = this.textAlign; e && d && (f = e.getBBox(), h = e.padding, l = "left" === l ? t.inverted ? -h : h : "right" === l ? f.width : t.inverted && "center" === l ? f.width / 2 : t.inverted ? m ? f.width + h : -h : f.width / 2, m = t.inverted ? f.height / 2 : m ? -h : f.height, this.alignOptions.x = M(this.options.x, 0), this.alignOptions.y = M(this.options.y, 0), d.x -= l, d.y -= m, e.align(this.alignOptions, null, d),
                            t.isInsidePlot(e.alignAttr.x + l - this.alignOptions.x, e.alignAttr.y + m - this.alignOptions.y) ? e.show() : (e.alignAttr.y = -9999, r = !1), r && A.prototype.justifyDataLabel.call(this.axis, e, this.alignOptions, e.alignAttr, f, d), e.attr({ x: e.alignAttr.x, y: e.alignAttr.y }), M(!r && this.options.crop, !0) && ((t = H(e.x) && H(e.y) && t.isInsidePlot(e.x - h + e.width, e.y) && t.isInsidePlot(e.x + h, e.y)) || e.hide()))
                    }; e.prototype.getStackBox = function (e, m, w, h, f, d, t) {
                        var l = m.axis.reversed, c = e.inverted, a = t.height + t.pos - (c ? e.plotLeft : e.plotTop); m =
                            m.isNegative && !l || !m.isNegative && l; return { x: c ? m ? h - t.right : h - d + t.pos - e.plotLeft : w + e.xAxis[0].transB - e.plotLeft, y: c ? t.height - w - f : m ? a - h - d : a - h, width: c ? d : f, height: c ? f : d }
                    }; return e
                }(); e.prototype.getStacks = function () {
                    var e = this, r = e.inverted; e.yAxis.forEach(function (e) { e.stacking && e.stacking.stacks && e.hasVisibleSeries && (e.stacking.oldStacks = e.stacking.stacks) }); e.series.forEach(function (m) {
                        var u = m.xAxis && m.xAxis.options || {}; !m.options.stacking || !0 !== m.visible && !1 !== e.options.chart.ignoreHiddenSeries || (m.stackKey =
                            [m.type, M(m.options.stack, ""), r ? u.top : u.left, r ? u.height : u.width].join())
                    })
                }; B.compose(p); A.prototype.setGroupedPoints = function () { this.options.centerInCategory && (this.is("column") || this.is("columnrange")) && !this.options.stacking && 1 < this.chart.series.length && A.prototype.setStackedPoints.call(this, "group") }; A.prototype.setStackedPoints = function (e) {
                    var r = e || this.options.stacking; if (r && (!0 === this.visible || !1 === this.chart.options.chart.ignoreHiddenSeries)) {
                        var m = this.processedXData, w = this.processedYData,
                        h = [], f = w.length, d = this.options, t = d.threshold, l = M(d.startFromThreshold && t, 0); d = d.stack; e = e ? this.type + "," + r : this.stackKey; var c = "-" + e, a = this.negStacks, x = this.yAxis, v = x.stacking.stacks, E = x.stacking.oldStacks, F, k; x.stacking.stacksTouched += 1; for (k = 0; k < f; k++) {
                            var y = m[k]; var I = w[k]; var p = this.getStackIndicator(p, y, this.index); var g = p.key; var b = (F = a && I < (l ? 0 : t)) ? c : e; v[b] || (v[b] = {}); v[b][y] || (E[b] && E[b][y] ? (v[b][y] = E[b][y], v[b][y].total = null) : v[b][y] = new K(x, x.options.stackLabels, F, y, d)); b = v[b][y]; null !== I ?
                                (b.points[g] = b.points[this.index] = [M(b.cumulative, l)], J(b.cumulative) || (b.base = g), b.touched = x.stacking.stacksTouched, 0 < p.index && !1 === this.singleStacks && (b.points[g][0] = b.points[this.index + "," + y + ",0"][0])) : b.points[g] = b.points[this.index] = null; "percent" === r ? (F = F ? e : c, a && v[F] && v[F][y] ? (F = v[F][y], b.total = F.total = Math.max(F.total, b.total) + Math.abs(I) || 0) : b.total = z(b.total + (Math.abs(I) || 0))) : "group" === r ? null !== I && (b.total = (b.total || 0) + 1) : b.total = z(b.total + (I || 0)); b.cumulative = "group" === r ? (b.total || 1) - 1 :
                                    M(b.cumulative, l) + (I || 0); null !== I && (b.points[g].push(b.cumulative), h[k] = b.cumulative, b.hasValidPoints = !0)
                        } "percent" === r && (x.stacking.usePercentage = !0); "group" !== r && (this.stackedYData = h); x.stacking.oldStacks = {}
                    }
                }; A.prototype.modifyStacks = function () {
                    var e = this, r = e.stackKey, m = e.yAxis.stacking.stacks, p = e.processedXData, h, f = e.options.stacking; e[f + "Stacker"] && [r, "-" + r].forEach(function (d) {
                        for (var t = p.length, l, c; t--;)if (l = p[t], h = e.getStackIndicator(h, l, e.index, d), c = (l = m[d] && m[d][l]) && l.points[h.key]) e[f +
                            "Stacker"](c, l, t)
                    })
                }; A.prototype.percentStacker = function (e, m, u) { m = m.total ? 100 / m.total : 0; e[0] = z(e[0] * m); e[1] = z(e[1] * m); this.stackedYData[u] = e[1] }; A.prototype.getStackIndicator = function (e, m, u, p) { !J(e) || e.x !== m || p && e.key !== p ? e = { x: m, index: 0, key: p } : e.index++; e.key = [u, m, e.index].join(); return e }; q.StackItem = K; return q.StackItem
            }); O(q, "parts/Dynamics.js", [q["parts/Axis.js"], q["parts/Chart.js"], q["parts/Globals.js"], q["parts/Options.js"], q["parts/Point.js"], q["parts/Time.js"], q["parts/Utilities.js"]], function (p,
                e, q, B, D, z, J) {
                    var G = B.time, m = J.addEvent, H = J.animate, M = J.createElement, A = J.css, K = J.defined, w = J.erase, r = J.error, u = J.extend, C = J.fireEvent, h = J.isArray, f = J.isNumber, d = J.isObject, t = J.isString, l = J.merge, c = J.objectEach, a = J.pick, x = J.relativeLength, v = J.setAnimation, E = J.splat; B = q.Series; var F = q.seriesTypes; q.cleanRecursively = function (a, f) { var k = {}; c(a, function (c, g) { if (d(a[g], !0) && !a.nodeType && f[g]) c = q.cleanRecursively(a[g], f[g]), Object.keys(c).length && (k[g] = c); else if (d(a[g]) || a[g] !== f[g]) k[g] = a[g] }); return k };
                u(e.prototype, {
                    addSeries: function (c, d, f) { var k, g = this; c && (d = a(d, !0), C(g, "addSeries", { options: c }, function () { k = g.initSeries(c); g.isDirtyLegend = !0; g.linkSeries(); k.enabledDataSorting && k.setData(c.data, !1); C(g, "afterAddSeries", { series: k }); d && g.redraw(f) })); return k }, addAxis: function (a, c, d, f) { return this.createAxis(c ? "xAxis" : "yAxis", { axis: a, redraw: d, animation: f }) }, addColorAxis: function (a, c, d) { return this.createAxis("colorAxis", { axis: a, redraw: c, animation: d }) }, createAxis: function (c, d) {
                        var k = this.options,
                        f = "colorAxis" === c, g = d.redraw, b = d.animation; d = l(d.axis, { index: this[c].length, isX: "xAxis" === c }); var n = f ? new q.ColorAxis(this, d) : new p(this, d); k[c] = E(k[c] || {}); k[c].push(d); f && (this.isDirtyLegend = !0, this.axes.forEach(function (a) { a.series = [] }), this.series.forEach(function (a) { a.bindAxes(); a.isDirtyData = !0 })); a(g, !0) && this.redraw(b); return n
                    }, showLoading: function (c) {
                        var d = this, k = d.options, f = d.loadingDiv, g = k.loading, b = function () {
                            f && A(f, {
                                left: d.plotLeft + "px", top: d.plotTop + "px", width: d.plotWidth + "px", height: d.plotHeight +
                                    "px"
                            })
                        }; f || (d.loadingDiv = f = M("div", { className: "highcharts-loading highcharts-loading-hidden" }, null, d.container), d.loadingSpan = M("span", { className: "highcharts-loading-inner" }, null, f), m(d, "redraw", b)); f.className = "highcharts-loading"; d.loadingSpan.innerHTML = a(c, k.lang.loading, ""); d.styledMode || (A(f, u(g.style, { zIndex: 10 })), A(d.loadingSpan, g.labelStyle), d.loadingShown || (A(f, { opacity: 0, display: "" }), H(f, { opacity: g.style.opacity || .5 }, { duration: g.showDuration || 0 }))); d.loadingShown = !0; b()
                    }, hideLoading: function () {
                        var a =
                            this.options, c = this.loadingDiv; c && (c.className = "highcharts-loading highcharts-loading-hidden", this.styledMode || H(c, { opacity: 0 }, { duration: a.loading.hideDuration || 100, complete: function () { A(c, { display: "none" }) } })); this.loadingShown = !1
                    }, propsRequireDirtyBox: "backgroundColor borderColor borderWidth borderRadius plotBackgroundColor plotBackgroundImage plotBorderColor plotBorderWidth plotShadow shadow".split(" "), propsRequireReflow: "margin marginTop marginRight marginBottom marginLeft spacing spacingTop spacingRight spacingBottom spacingLeft".split(" "),
                    propsRequireUpdateSeries: "chart.inverted chart.polar chart.ignoreHiddenSeries chart.type colors plotOptions time tooltip".split(" "), collectionsWithUpdate: ["xAxis", "yAxis", "zAxis", "series"], update: function (d, h, e, v) {
                        var g = this, b = { credits: "addCredits", title: "setTitle", subtitle: "setSubtitle", caption: "setCaption" }, k, y, m, r = d.isResponsiveOptions, u = []; C(g, "update", { options: d }); r || g.setResponsive(!1, !0); d = q.cleanRecursively(d, g.options); l(!0, g.userOptions, d); if (k = d.chart) {
                            l(!0, g.options.chart, k); "className" in
                                k && g.setClassName(k.className); "reflow" in k && g.setReflow(k.reflow); if ("inverted" in k || "polar" in k || "type" in k) { g.propFromSeries(); var F = !0 } "alignTicks" in k && (F = !0); c(k, function (a, b) { -1 !== g.propsRequireUpdateSeries.indexOf("chart." + b) && (y = !0); -1 !== g.propsRequireDirtyBox.indexOf(b) && (g.isDirtyBox = !0); -1 !== g.propsRequireReflow.indexOf(b) && (r ? g.isDirtyBox = !0 : m = !0) }); !g.styledMode && "style" in k && g.renderer.setStyle(k.style)
                        } !g.styledMode && d.colors && (this.options.colors = d.colors); d.plotOptions && l(!0, this.options.plotOptions,
                            d.plotOptions); d.time && this.time === G && (this.time = new z(d.time)); c(d, function (a, c) { if (g[c] && "function" === typeof g[c].update) g[c].update(a, !1); else if ("function" === typeof g[b[c]]) g[b[c]](a); "chart" !== c && -1 !== g.propsRequireUpdateSeries.indexOf(c) && (y = !0) }); this.collectionsWithUpdate.forEach(function (b) {
                                if (d[b]) {
                                    if ("series" === b) { var c = []; g[b].forEach(function (b, d) { b.options.isInternal || c.push(a(b.options.index, d)) }) } E(d[b]).forEach(function (a, d) {
                                        var k = K(a.id), f; k && (f = g.get(a.id)); f || (f = g[b][c ? c[d] : d]) &&
                                            k && K(f.options.id) && (f = void 0); f && f.coll === b && (f.update(a, !1), e && (f.touched = !0)); !f && e && g.collectionsWithInit[b] && (g.collectionsWithInit[b][0].apply(g, [a].concat(g.collectionsWithInit[b][1] || []).concat([!1])).touched = !0)
                                    }); e && g[b].forEach(function (a) { a.touched || a.options.isInternal ? delete a.touched : u.push(a) })
                                }
                            }); u.forEach(function (a) { a.remove && a.remove(!1) }); F && g.axes.forEach(function (a) { a.update({}, !1) }); y && g.getSeriesOrderByLinks().forEach(function (a) { a.chart && a.update({}, !1) }, this); d.loading &&
                                l(!0, g.options.loading, d.loading); F = k && k.width; k = k && k.height; t(k) && (k = x(k, F || g.chartWidth)); m || f(F) && F !== g.chartWidth || f(k) && k !== g.chartHeight ? g.setSize(F, k, v) : a(h, !0) && g.redraw(v); C(g, "afterUpdate", { options: d, redraw: h, animation: v })
                    }, setSubtitle: function (a, c) { this.applyDescription("subtitle", a); this.layOutTitles(c) }, setCaption: function (a, c) { this.applyDescription("caption", a); this.layOutTitles(c) }
                }); e.prototype.collectionsWithInit = {
                    xAxis: [e.prototype.addAxis, [!0]], yAxis: [e.prototype.addAxis, [!1]],
                    series: [e.prototype.addSeries]
                }; u(D.prototype, {
                    update: function (c, f, l, h) {
                        function g() {
                            b.applyOptions(c); var g = t && b.hasDummyGraphic; g = null === b.y ? !g : g; t && g && (b.graphic = t.destroy(), delete b.hasDummyGraphic); d(c, !0) && (t && t.element && c && c.marker && "undefined" !== typeof c.marker.symbol && (b.graphic = t.destroy()), c && c.dataLabels && b.dataLabel && (b.dataLabel = b.dataLabel.destroy()), b.connector && (b.connector = b.connector.destroy())); e = b.index; k.updateParallelArrays(b, e); m.data[e] = d(m.data[e], !0) || d(c, !0) ? b.options :
                                a(c, m.data[e]); k.isDirty = k.isDirtyData = !0; !k.fixedBox && k.hasCartesianSeries && (v.isDirtyBox = !0); "point" === m.legendType && (v.isDirtyLegend = !0); f && v.redraw(l)
                        } var b = this, k = b.series, t = b.graphic, e, v = k.chart, m = k.options; f = a(f, !0); !1 === h ? g() : b.firePointEvent("update", { options: c }, g)
                    }, remove: function (a, c) { this.series.removePoint(this.series.data.indexOf(this), a, c) }
                }); u(B.prototype, {
                    addPoint: function (c, d, f, l, g) {
                        var b = this.options, k = this.data, h = this.chart, t = this.xAxis; t = t && t.hasNames && t.names; var e = b.data, v =
                            this.xData, m; d = a(d, !0); var y = { series: this }; this.pointClass.prototype.applyOptions.apply(y, [c]); var r = y.x; var x = v.length; if (this.requireSorting && r < v[x - 1]) for (m = !0; x && v[x - 1] > r;)x--; this.updateParallelArrays(y, "splice", x, 0, 0); this.updateParallelArrays(y, x); t && y.name && (t[r] = y.name); e.splice(x, 0, c); m && (this.data.splice(x, 0, null), this.processData()); "point" === b.legendType && this.generatePoints(); f && (k[0] && k[0].remove ? k[0].remove(!1) : (k.shift(), this.updateParallelArrays(y, "shift"), e.shift())); !1 !== g && C(this,
                                "addPoint", { point: y }); this.isDirtyData = this.isDirty = !0; d && h.redraw(l)
                    }, removePoint: function (c, d, f) { var k = this, g = k.data, b = g[c], n = k.points, l = k.chart, h = function () { n && n.length === g.length && n.splice(c, 1); g.splice(c, 1); k.options.data.splice(c, 1); k.updateParallelArrays(b || { series: k }, "splice", c, 1); b && b.destroy(); k.isDirty = !0; k.isDirtyData = !0; d && l.redraw() }; v(f, l); d = a(d, !0); b ? b.firePointEvent("remove", null, h) : h() }, remove: function (c, d, f, l) {
                        function g() {
                            b.destroy(l); b.remove = null; k.isDirtyLegend = k.isDirtyBox =
                                !0; k.linkSeries(); a(c, !0) && k.redraw(d)
                        } var b = this, k = b.chart; !1 !== f ? C(b, "remove", null, g) : g()
                    }, update: function (c, d) {
                        c = q.cleanRecursively(c, this.userOptions); C(this, "update", { options: c }); var k = this, f = k.chart, g = k.userOptions, b = k.initialType || k.type, n = c.type || g.type || f.options.chart.type, h = !(this.hasDerivedData || c.dataGrouping || n && n !== this.type || "undefined" !== typeof c.pointStart || c.pointInterval || c.pointIntervalUnit || c.keys), t = F[b].prototype, e, v = ["eventOptions", "navigatorSeries", "baseSeries"], m = k.finishedAnimating &&
                            { animation: !1 }, y = {}; h && (v.push("data", "isDirtyData", "points", "processedXData", "processedYData", "xIncrement", "cropped", "_hasPointMarkers", "_hasPointLabels", "mapMap", "mapData", "minY", "maxY", "minX", "maxX"), !1 !== c.visible && v.push("area", "graph"), k.parallelArrays.forEach(function (a) { v.push(a + "Data") }), c.data && (c.dataSorting && u(k.options.dataSorting, c.dataSorting), this.setData(c.data, !1))); c = l(g, m, { index: "undefined" === typeof g.index ? k.index : g.index, pointStart: a(g.pointStart, k.xData[0]) }, !h && { data: k.options.data },
                                c); h && c.data && (c.data = k.options.data); v = ["group", "markerGroup", "dataLabelsGroup", "transformGroup"].concat(v); v.forEach(function (a) { v[a] = k[a]; delete k[a] }); k.remove(!1, null, !1, !0); for (e in t) k[e] = void 0; F[n || b] ? u(k, F[n || b].prototype) : r(17, !0, f, { missingModuleFor: n || b }); v.forEach(function (a) { k[a] = v[a] }); k.init(f, c); if (h && this.points) {
                                    var x = k.options; !1 === x.visible ? (y.graphic = 1, y.dataLabel = 1) : k._hasPointLabels || (c = x.marker, g = x.dataLabels, c && (!1 === c.enabled || "symbol" in c) && (y.graphic = 1), g && !1 === g.enabled &&
                                        (y.dataLabel = 1)); this.points.forEach(function (a) { a && a.series && (a.resolveColor(), Object.keys(y).length && a.destroyElements(y), !1 === x.showInLegend && a.legendItem && f.legend.destroyItem(a)) }, this)
                                } k.initialType = b; f.linkSeries(); C(this, "afterUpdate"); a(d, !0) && f.redraw(h ? void 0 : !1)
                    }, setName: function (a) { this.name = this.options.name = this.userOptions.name = a; this.chart.isDirtyLegend = !0 }
                }); u(p.prototype, {
                    update: function (d, f) {
                        var k = this.chart, h = d && d.events || {}; d = l(this.userOptions, d); k.options[this.coll].indexOf &&
                            (k.options[this.coll][k.options[this.coll].indexOf(this.userOptions)] = d); c(k.options[this.coll].events, function (a, b) { "undefined" === typeof h[b] && (h[b] = void 0) }); this.destroy(!0); this.init(k, u(d, { events: h })); k.isDirtyBox = !0; a(f, !0) && k.redraw()
                    }, remove: function (c) {
                        for (var d = this.chart, k = this.coll, f = this.series, g = f.length; g--;)f[g] && f[g].remove(!1); w(d.axes, this); w(d[k], this); h(d.options[k]) ? d.options[k].splice(this.options.index, 1) : delete d.options[k]; d[k].forEach(function (a, c) {
                            a.options.index = a.userOptions.index =
                                c
                        }); this.destroy(); d.isDirtyBox = !0; a(c, !0) && d.redraw()
                    }, setTitle: function (a, c) { this.update({ title: a }, c) }, setCategories: function (a, c) { this.update({ categories: a }, c) }
                })
            }); O(q, "parts/AreaSeries.js", [q["parts/Globals.js"], q["parts/Color.js"], q["mixins/legend-symbol.js"], q["parts/Utilities.js"]], function (p, e, q, B) {
                var D = e.parse, z = B.objectEach, J = B.pick; e = B.seriesType; var G = p.Series; e("area", "line", { softThreshold: !1, threshold: 0 }, {
                    singleStacks: !1, getStackPoints: function (e) {
                        var m = [], p = [], q = this.xAxis, K = this.yAxis,
                        w = K.stacking.stacks[this.stackKey], r = {}, u = this.index, C = K.series, h = C.length, f = J(K.options.reversedStacks, !0) ? 1 : -1, d; e = e || this.points; if (this.options.stacking) {
                            for (d = 0; d < e.length; d++)e[d].leftNull = e[d].rightNull = void 0, r[e[d].x] = e[d]; z(w, function (d, c) { null !== d.total && p.push(c) }); p.sort(function (d, c) { return d - c }); var t = C.map(function (d) { return d.visible }); p.forEach(function (l, c) {
                                var a = 0, e, v; if (r[l] && !r[l].isNull) m.push(r[l]), [-1, 1].forEach(function (a) {
                                    var m = 1 === a ? "rightNull" : "leftNull", k = 0, y = w[p[c + a]];
                                    if (y) for (d = u; 0 <= d && d < h;)e = y.points[d], e || (d === u ? r[l][m] = !0 : t[d] && (v = w[l].points[d]) && (k -= v[1] - v[0])), d += f; r[l][1 === a ? "rightCliff" : "leftCliff"] = k
                                }); else { for (d = u; 0 <= d && d < h;) { if (e = w[l].points[d]) { a = e[1]; break } d += f } a = K.translate(a, 0, 1, 0, 1); m.push({ isNull: !0, plotX: q.translate(l, 0, 0, 0, 1), x: l, plotY: a, yBottom: a }) }
                            })
                        } return m
                    }, getGraphPath: function (e) {
                        var m = G.prototype.getGraphPath, p = this.options, q = p.stacking, z = this.yAxis, w, r = [], u = [], C = this.index, h = z.stacking.stacks[this.stackKey], f = p.threshold, d = Math.round(z.getThreshold(p.threshold));
                        p = J(p.connectNulls, "percent" === q); var t = function (a, l, t) { var v = e[a]; a = q && h[v.x].points[C]; var k = v[t + "Null"] || 0; t = v[t + "Cliff"] || 0; v = !0; if (t || k) { var m = (k ? a[0] : a[1]) + t; var x = a[0] + t; v = !!k } else !q && e[l] && e[l].isNull && (m = x = f); "undefined" !== typeof m && (u.push({ plotX: c, plotY: null === m ? d : z.getThreshold(m), isNull: v, isCliff: !0 }), r.push({ plotX: c, plotY: null === x ? d : z.getThreshold(x), doCurve: !1 })) }; e = e || this.points; q && (e = this.getStackPoints(e)); for (w = 0; w < e.length; w++) {
                            q || (e[w].leftCliff = e[w].rightCliff = e[w].leftNull =
                                e[w].rightNull = void 0); var l = e[w].isNull; var c = J(e[w].rectPlotX, e[w].plotX); var a = J(e[w].yBottom, d); if (!l || p) p || t(w, w - 1, "left"), l && !q && p || (u.push(e[w]), r.push({ x: w, plotX: c, plotY: a })), p || t(w, w + 1, "right")
                        } w = m.call(this, u, !0, !0); r.reversed = !0; l = m.call(this, r, !0, !0); (a = l[0]) && "M" === a[0] && (l[0] = ["L", a[1], a[2]]); l = w.concat(l); m = m.call(this, u, !1, p); l.xMap = w.xMap; this.areaPath = l; return m
                    }, drawGraph: function () {
                    this.areaPath = []; G.prototype.drawGraph.apply(this); var e = this, p = this.areaPath, q = this.options, A = [["area",
                        "highcharts-area", this.color, q.fillColor]]; this.zones.forEach(function (m, p) { A.push(["zone-area-" + p, "highcharts-area highcharts-zone-area-" + p + " " + m.className, m.color || e.color, m.fillColor || q.fillColor]) }); A.forEach(function (m) {
                            var w = m[0], r = e[w], u = r ? "animate" : "attr", C = {}; r ? (r.endX = e.preventGraphAnimation ? null : p.xMap, r.animate({ d: p })) : (C.zIndex = 0, r = e[w] = e.chart.renderer.path(p).addClass(m[1]).add(e.group), r.isArea = !0); e.chart.styledMode || (C.fill = J(m[3], D(m[2]).setOpacity(J(q.fillOpacity, .75)).get()));
                            r[u](C); r.startX = p.xMap; r.shiftUnit = q.step ? 2 : 1
                        })
                    }, drawLegendSymbol: q.drawRectangle
                }); ""
            }); O(q, "parts/SplineSeries.js", [q["parts/Utilities.js"]], function (p) {
                var e = p.pick; p = p.seriesType; p("spline", "line", {}, {
                    getPointSpline: function (p, q, D) {
                        var z = q.plotX || 0, B = q.plotY || 0, G = p[D - 1]; D = p[D + 1]; if (G && !G.isNull && !1 !== G.doCurve && !q.isCliff && D && !D.isNull && !1 !== D.doCurve && !q.isCliff) {
                            p = G.plotY || 0; var m = D.plotX || 0; D = D.plotY || 0; var H = 0; var M = (1.5 * z + (G.plotX || 0)) / 2.5; var A = (1.5 * B + p) / 2.5; m = (1.5 * z + m) / 2.5; var K = (1.5 *
                                B + D) / 2.5; m !== M && (H = (K - A) * (m - z) / (m - M) + B - K); A += H; K += H; A > p && A > B ? (A = Math.max(p, B), K = 2 * B - A) : A < p && A < B && (A = Math.min(p, B), K = 2 * B - A); K > D && K > B ? (K = Math.max(D, B), A = 2 * B - K) : K < D && K < B && (K = Math.min(D, B), A = 2 * B - K); q.rightContX = m; q.rightContY = K
                        } q = ["C", e(G.rightContX, G.plotX, 0), e(G.rightContY, G.plotY, 0), e(M, z, 0), e(A, B, 0), z, B]; G.rightContX = G.rightContY = void 0; return q
                    }
                }); ""
            }); O(q, "parts/AreaSplineSeries.js", [q["parts/Globals.js"], q["mixins/legend-symbol.js"], q["parts/Options.js"], q["parts/Utilities.js"]], function (p, e, q,
                B) { B = B.seriesType; p = p.seriesTypes.area.prototype; B("areaspline", "spline", q.defaultOptions.plotOptions.area, { getStackPoints: p.getStackPoints, getGraphPath: p.getGraphPath, drawGraph: p.drawGraph, drawLegendSymbol: e.drawRectangle }); "" }); O(q, "parts/ColumnSeries.js", [q["parts/Globals.js"], q["parts/Color.js"], q["mixins/legend-symbol.js"], q["parts/Utilities.js"]], function (p, e, q, B) {
                    ""; var D = e.parse, z = B.animObject, J = B.clamp, G = B.defined, m = B.extend, H = B.isNumber, M = B.merge, A = B.pick; e = B.seriesType; var K = p.Series; e("column",
                        "line", { borderRadius: 0, centerInCategory: !1, groupPadding: .2, marker: null, pointPadding: .1, minPointLength: 0, cropThreshold: 50, pointRange: null, states: { hover: { halo: !1, brightness: .1 }, select: { color: "#cccccc", borderColor: "#000000" } }, dataLabels: { align: void 0, verticalAlign: void 0, y: void 0 }, softThreshold: !1, startFromThreshold: !0, stickyTracking: !1, tooltip: { distance: 6 }, threshold: 0, borderColor: "#ffffff" }, {
                            cropShoulder: 0, directTouch: !0, trackerGroups: ["group", "dataLabelsGroup"], negStacks: !0, init: function () {
                                K.prototype.init.apply(this,
                                    arguments); var e = this, m = e.chart; m.hasRendered && m.series.forEach(function (m) { m.type === e.type && (m.isDirty = !0) })
                            }, getColumnMetrics: function () {
                                var e = this, m = e.options, u = e.xAxis, p = e.yAxis, h = u.options.reversedStacks; h = u.reversed && !h || !u.reversed && h; var f, d = {}, t = 0; !1 === m.grouping ? t = 1 : e.chart.series.forEach(function (a) {
                                    var c = a.yAxis, h = a.options; if (a.type === e.type && (a.visible || !e.chart.options.chart.ignoreHiddenSeries) && p.len === c.len && p.pos === c.pos) {
                                        if (h.stacking && "group" !== h.stacking) {
                                            f = a.stackKey; "undefined" ===
                                                typeof d[f] && (d[f] = t++); var l = d[f]
                                        } else !1 !== h.grouping && (l = t++); a.columnIndex = l
                                    }
                                }); var l = Math.min(Math.abs(u.transA) * (u.ordinal && u.ordinal.slope || m.pointRange || u.closestPointRange || u.tickInterval || 1), u.len), c = l * m.groupPadding, a = (l - 2 * c) / (t || 1); m = Math.min(m.maxPointWidth || u.len, A(m.pointWidth, a * (1 - 2 * m.pointPadding))); e.columnMetrics = { width: m, offset: (a - m) / 2 + (c + ((e.columnIndex || 0) + (h ? 1 : 0)) * a - l / 2) * (h ? -1 : 1), paddedWidth: a, columnCount: t }; return e.columnMetrics
                            }, crispCol: function (e, m, u, p) {
                                var h = this.chart,
                                f = this.borderWidth, d = -(f % 2 ? .5 : 0); f = f % 2 ? .5 : 1; h.inverted && h.renderer.isVML && (f += 1); this.options.crisp && (u = Math.round(e + u) + d, e = Math.round(e) + d, u -= e); p = Math.round(m + p) + f; d = .5 >= Math.abs(m) && .5 < p; m = Math.round(m) + f; p -= m; d && p && (--m, p += 1); return { x: e, y: m, width: u, height: p }
                            }, adjustForMissingColumns: function (e, m, u, q) {
                                var h = this, f = this.options.stacking; if (!u.isNull && 1 < q.columnCount) {
                                    var d = 0, t = 0; Highcharts.objectEach(this.yAxis.stacking && this.yAxis.stacking.stacks, function (l) {
                                        if ("number" === typeof u.x && (l = l[u.x.toString()])) {
                                            var c =
                                                l.points[h.index], a = l.total; f ? (c && (d = t), l.hasValidPoints && t++) : p.isArray(c) && (d = c[1], t = a || 0)
                                        }
                                    }); e = (u.plotX || 0) + ((t - 1) * q.paddedWidth + m) / 2 - m - d * q.paddedWidth
                                } return e
                            }, translate: function () {
                                var e = this, m = e.chart, u = e.options, p = e.dense = 2 > e.closestPointRange * e.xAxis.transA; p = e.borderWidth = A(u.borderWidth, p ? 0 : 1); var h = e.xAxis, f = e.yAxis, d = u.threshold, t = e.translatedThreshold = f.getThreshold(d), l = A(u.minPointLength, 5), c = e.getColumnMetrics(), a = c.width, x = e.barW = Math.max(a, 1 + 2 * p), v = e.pointXOffset = c.offset, q = e.dataMin,
                                    F = e.dataMax; m.inverted && (t -= .5); u.pointPadding && (x = Math.ceil(x)); K.prototype.translate.apply(e); e.points.forEach(function (k) {
                                        var y = A(k.yBottom, t), r = 999 + Math.abs(y), p = a, g = k.plotX || 0; r = J(k.plotY, -r, f.len + r); var b = g + v, n = x, E = Math.min(r, y), w = Math.max(r, y) - E; if (l && Math.abs(w) < l) { w = l; var C = !f.reversed && !k.negative || f.reversed && k.negative; H(d) && H(F) && k.y === d && F <= d && (f.min || 0) < d && q !== F && (C = !C); E = Math.abs(E - t) > l ? y - l : t - (C ? l : 0) } G(k.options.pointWidth) && (p = n = Math.ceil(k.options.pointWidth), b -= Math.round((p - a) /
                                            2)); u.centerInCategory && (b = e.adjustForMissingColumns(b, p, k, c)); k.barX = b; k.pointWidth = p; k.tooltipPos = m.inverted ? [f.len + f.pos - m.plotLeft - r, h.len + h.pos - m.plotTop - (g || 0) - v - n / 2, w] : [b + n / 2, r + f.pos - m.plotTop, w]; k.shapeType = e.pointClass.prototype.shapeType || "rect"; k.shapeArgs = e.crispCol.apply(e, k.isNull ? [b, t, n, 0] : [b, E, n, w])
                                    })
                            }, getSymbol: p.noop, drawLegendSymbol: q.drawRectangle, drawGraph: function () { this.group[this.dense ? "addClass" : "removeClass"]("highcharts-dense-data") }, pointAttribs: function (e, m) {
                                var r = this.options,
                                p = this.pointAttrToOptions || {}; var h = p.stroke || "borderColor"; var f = p["stroke-width"] || "borderWidth", d = e && e.color || this.color, t = e && e[h] || r[h] || this.color || d, l = e && e[f] || r[f] || this[f] || 0; p = e && e.options.dashStyle || r.dashStyle; var c = A(e && e.opacity, r.opacity, 1); if (e && this.zones.length) { var a = e.getZone(); d = e.options.color || a && (a.color || e.nonZonedColor) || this.color; a && (t = a.borderColor || t, p = a.dashStyle || p, l = a.borderWidth || l) } m && e && (e = M(r.states[m], e.options.states && e.options.states[m] || {}), m = e.brightness, d =
                                    e.color || "undefined" !== typeof m && D(d).brighten(e.brightness).get() || d, t = e[h] || t, l = e[f] || l, p = e.dashStyle || p, c = A(e.opacity, c)); h = { fill: d, stroke: t, "stroke-width": l, opacity: c }; p && (h.dashstyle = p); return h
                            }, drawPoints: function () {
                                var e = this, m = this.chart, u = e.options, p = m.renderer, h = u.animationLimit || 250, f; e.points.forEach(function (d) {
                                    var t = d.graphic, l = !!t, c = t && m.pointCount < h ? "animate" : "attr"; if (H(d.plotY) && null !== d.y) {
                                        f = d.shapeArgs; t && d.hasNewShapeType() && (t = t.destroy()); e.enabledDataSorting && (d.startXPos =
                                            e.xAxis.reversed ? -(f ? f.width : 0) : e.xAxis.width); t || (d.graphic = t = p[d.shapeType](f).add(d.group || e.group)) && e.enabledDataSorting && m.hasRendered && m.pointCount < h && (t.attr({ x: d.startXPos }), l = !0, c = "animate"); if (t && l) t[c](M(f)); if (u.borderRadius) t[c]({ r: u.borderRadius }); m.styledMode || t[c](e.pointAttribs(d, d.selected && "select")).shadow(!1 !== d.allowShadow && u.shadow, null, u.stacking && !u.borderRadius); t.addClass(d.getClassName(), !0)
                                    } else t && (d.graphic = t.destroy())
                                })
                            }, animate: function (e) {
                                var r = this, u = this.yAxis,
                                p = r.options, h = this.chart.inverted, f = {}, d = h ? "translateX" : "translateY"; if (e) f.scaleY = .001, e = J(u.toPixels(p.threshold), u.pos, u.pos + u.len), h ? f.translateX = e - u.len : f.translateY = e, r.clipBox && r.setClip(), r.group.attr(f); else { var t = r.group.attr(d); r.group.animate({ scaleY: 1 }, m(z(r.options.animation), { step: function (h, c) { r.group && (f[d] = t + c.pos * (u.pos - t), r.group.attr(f)) } })) }
                            }, remove: function () {
                                var e = this, m = e.chart; m.hasRendered && m.series.forEach(function (m) { m.type === e.type && (m.isDirty = !0) }); K.prototype.remove.apply(e,
                                    arguments)
                            }
                    }); ""
                }); O(q, "parts/BarSeries.js", [q["parts/Utilities.js"]], function (p) { p = p.seriesType; p("bar", "column", null, { inverted: !0 }); "" }); O(q, "parts/ScatterSeries.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
                    var q = e.addEvent; e = e.seriesType; var B = p.Series; e("scatter", "line", { lineWidth: 0, findNearestPointBy: "xy", jitter: { x: 0, y: 0 }, marker: { enabled: !0 }, tooltip: { headerFormat: '<span style="color:{point.color}">\u25cf</span> <span style="font-size: 10px"> {series.name}</span><br/>', pointFormat: "x: <b>{point.x}</b><br/>y: <b>{point.y}</b><br/>" } },
                        {
                            sorted: !1, requireSorting: !1, noSharedTooltip: !0, trackerGroups: ["group", "markerGroup", "dataLabelsGroup"], takeOrdinalPosition: !1, drawGraph: function () { this.options.lineWidth && B.prototype.drawGraph.call(this) }, applyJitter: function () {
                                var e = this, p = this.options.jitter, q = this.points.length; p && this.points.forEach(function (z, m) {
                                    ["x", "y"].forEach(function (B, G) {
                                        var A = "plot" + B.toUpperCase(); if (p[B] && !z.isNull) {
                                            var K = e[B + "Axis"]; var w = p[B] * K.transA; if (K && !K.isLog) {
                                                var r = Math.max(0, z[A] - w); K = Math.min(K.len, z[A] +
                                                    w); G = 1E4 * Math.sin(m + G * q); z[A] = r + (K - r) * (G - Math.floor(G)); "x" === B && (z.clientX = z.plotX)
                                            }
                                        }
                                    })
                                })
                            }
                        }); q(B, "afterTranslate", function () { this.applyJitter && this.applyJitter() }); ""
                }); O(q, "mixins/centered-series.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
                    var q = e.isNumber, B = e.pick, D = e.relativeLength, z = p.deg2rad; p.CenteredSeriesMixin = {
                        getCenter: function () {
                            var e = this.options, q = this.chart, m = 2 * (e.slicedOffset || 0), z = q.plotWidth - 2 * m, M = q.plotHeight - 2 * m, A = e.center, K = Math.min(z, M), w = e.size, r = e.innerSize ||
                                0; "string" === typeof w && (w = parseFloat(w)); "string" === typeof r && (r = parseFloat(r)); e = [B(A[0], "50%"), B(A[1], "50%"), B(w && 0 > w ? void 0 : e.size, "100%"), B(r && 0 > r ? void 0 : e.innerSize || 0, "0%")]; !q.angular || this instanceof p.Series || (e[3] = 0); for (A = 0; 4 > A; ++A)w = e[A], q = 2 > A || 2 === A && /%$/.test(w), e[A] = D(w, [z, M, K, e[2]][A]) + (q ? m : 0); e[3] > e[2] && (e[3] = e[2]); return e
                        }, getStartAndEndRadians: function (e, p) { e = q(e) ? e : 0; p = q(p) && p > e && 360 > p - e ? p : e + 360; return { start: z * (e + -90), end: z * (p + -90) } }
                    }
                }); O(q, "parts/PieSeries.js", [q["parts/Globals.js"],
                q["mixins/legend-symbol.js"], q["parts/Point.js"], q["parts/Utilities.js"]], function (p, e, q, B) {
                    var D = B.addEvent, z = B.clamp, J = B.defined, G = B.fireEvent, m = B.isNumber, H = B.merge, M = B.pick, A = B.relativeLength, K = B.seriesType, w = B.setAnimation; B = p.CenteredSeriesMixin; var r = B.getStartAndEndRadians, u = p.noop, C = p.Series; K("pie", "line", {
                        center: [null, null], clip: !1, colorByPoint: !0, dataLabels: {
                            allowOverlap: !0, connectorPadding: 5, connectorShape: "fixedOffset", crookDistance: "70%", distance: 30, enabled: !0, formatter: function () {
                                return this.point.isNull ?
                                    void 0 : this.point.name
                            }, softConnector: !0, x: 0
                        }, fillColor: void 0, ignoreHiddenPoint: !0, inactiveOtherPoints: !0, legendType: "point", marker: null, size: null, showInLegend: !1, slicedOffset: 10, stickyTracking: !1, tooltip: { followPointer: !0 }, borderColor: "#ffffff", borderWidth: 1, lineWidth: void 0, states: { hover: { brightness: .1 } }
                    }, {
                        isCartesian: !1, requireSorting: !1, directTouch: !0, noSharedTooltip: !0, trackerGroups: ["group", "dataLabelsGroup"], axisTypes: [], pointAttribs: p.seriesTypes.column.prototype.pointAttribs, animate: function (h) {
                            var f =
                                this, d = f.points, e = f.startAngleRad; h || d.forEach(function (d) { var c = d.graphic, a = d.shapeArgs; c && a && (c.attr({ r: M(d.startR, f.center && f.center[3] / 2), start: e, end: e }), c.animate({ r: a.r, start: a.start, end: a.end }, f.options.animation)) })
                        }, hasData: function () { return !!this.processedXData.length }, updateTotals: function () {
                            var h, f = 0, d = this.points, e = d.length, l = this.options.ignoreHiddenPoint; for (h = 0; h < e; h++) { var c = d[h]; f += l && !c.visible ? 0 : c.isNull ? 0 : c.y } this.total = f; for (h = 0; h < e; h++)c = d[h], c.percentage = 0 < f && (c.visible || !l) ?
                                c.y / f * 100 : 0, c.total = f
                        }, generatePoints: function () { C.prototype.generatePoints.call(this); this.updateTotals() }, getX: function (h, f, d) { var e = this.center, l = this.radii ? this.radii[d.index] : e[2] / 2; h = Math.asin(z((h - e[1]) / (l + d.labelDistance), -1, 1)); return e[0] + (f ? -1 : 1) * Math.cos(h) * (l + d.labelDistance) + (0 < d.labelDistance ? (f ? -1 : 1) * this.options.dataLabels.padding : 0) }, translate: function (h) {
                            this.generatePoints(); var f = 0, d = this.options, e = d.slicedOffset, l = e + (d.borderWidth || 0), c = r(d.startAngle, d.endAngle), a = this.startAngleRad =
                                c.start; c = (this.endAngleRad = c.end) - a; var m = this.points, v = d.dataLabels.distance; d = d.ignoreHiddenPoint; var u, F = m.length; h || (this.center = h = this.getCenter()); for (u = 0; u < F; u++) {
                                    var k = m[u]; var y = a + f * c; if (!d || k.visible) f += k.percentage / 100; var p = a + f * c; k.shapeType = "arc"; k.shapeArgs = { x: h[0], y: h[1], r: h[2] / 2, innerR: h[3] / 2, start: Math.round(1E3 * y) / 1E3, end: Math.round(1E3 * p) / 1E3 }; k.labelDistance = M(k.options.dataLabels && k.options.dataLabels.distance, v); k.labelDistance = A(k.labelDistance, k.shapeArgs.r); this.maxLabelDistance =
                                        Math.max(this.maxLabelDistance || 0, k.labelDistance); p = (p + y) / 2; p > 1.5 * Math.PI ? p -= 2 * Math.PI : p < -Math.PI / 2 && (p += 2 * Math.PI); k.slicedTranslation = { translateX: Math.round(Math.cos(p) * e), translateY: Math.round(Math.sin(p) * e) }; var q = Math.cos(p) * h[2] / 2; var g = Math.sin(p) * h[2] / 2; k.tooltipPos = [h[0] + .7 * q, h[1] + .7 * g]; k.half = p < -Math.PI / 2 || p > Math.PI / 2 ? 1 : 0; k.angle = p; y = Math.min(l, k.labelDistance / 5); k.labelPosition = {
                                            natural: { x: h[0] + q + Math.cos(p) * k.labelDistance, y: h[1] + g + Math.sin(p) * k.labelDistance }, "final": {}, alignment: 0 >
                                                k.labelDistance ? "center" : k.half ? "right" : "left", connectorPosition: { breakAt: { x: h[0] + q + Math.cos(p) * y, y: h[1] + g + Math.sin(p) * y }, touchingSliceAt: { x: h[0] + q, y: h[1] + g } }
                                        }
                                } G(this, "afterTranslate")
                        }, drawEmpty: function () {
                            var h = this.startAngleRad, f = this.endAngleRad, d = this.options; if (0 === this.total) {
                                var e = this.center[0]; var l = this.center[1]; this.graph || (this.graph = this.chart.renderer.arc(e, l, this.center[1] / 2, 0, h, f).addClass("highcharts-empty-series").add(this.group)); this.graph.attr({
                                    d: Highcharts.SVGRenderer.prototype.symbols.arc(e,
                                        l, this.center[2] / 2, 0, { start: h, end: f, innerR: this.center[3] / 2 })
                                }); this.chart.styledMode || this.graph.attr({ "stroke-width": d.borderWidth, fill: d.fillColor || "none", stroke: d.color || "#cccccc" })
                            } else this.graph && (this.graph = this.graph.destroy())
                        }, redrawPoints: function () {
                            var h = this, f = h.chart, d = f.renderer, e, l, c, a, m = h.options.shadow; this.drawEmpty(); !m || h.shadowGroup || f.styledMode || (h.shadowGroup = d.g("shadow").attr({ zIndex: -1 }).add(h.group)); h.points.forEach(function (t) {
                                var v = {}; l = t.graphic; if (!t.isNull && l) {
                                    a =
                                    t.shapeArgs; e = t.getTranslate(); if (!f.styledMode) { var r = t.shadowGroup; m && !r && (r = t.shadowGroup = d.g("shadow").add(h.shadowGroup)); r && r.attr(e); c = h.pointAttribs(t, t.selected && "select") } t.delayedRendering ? (l.setRadialReference(h.center).attr(a).attr(e), f.styledMode || l.attr(c).attr({ "stroke-linejoin": "round" }).shadow(m, r), t.delayedRendering = !1) : (l.setRadialReference(h.center), f.styledMode || H(!0, v, c), H(!0, v, a, e), l.animate(v)); l.attr({ visibility: t.visible ? "inherit" : "hidden" }); l.addClass(t.getClassName())
                                } else l &&
                                    (t.graphic = l.destroy())
                            })
                        }, drawPoints: function () { var h = this.chart.renderer; this.points.forEach(function (f) { f.graphic && f.hasNewShapeType() && (f.graphic = f.graphic.destroy()); f.graphic || (f.graphic = h[f.shapeType](f.shapeArgs).add(f.series.group), f.delayedRendering = !0) }) }, searchPoint: u, sortByAngle: function (h, f) { h.sort(function (d, h) { return "undefined" !== typeof d.angle && (h.angle - d.angle) * f }) }, drawLegendSymbol: e.drawRectangle, getCenter: B.getCenter, getSymbol: u, drawGraph: null
                    }, {
                        init: function () {
                            q.prototype.init.apply(this,
                                arguments); var h = this; h.name = M(h.name, "Slice"); var f = function (d) { h.slice("select" === d.type) }; D(h, "select", f); D(h, "unselect", f); return h
                        }, isValid: function () { return m(this.y) && 0 <= this.y }, setVisible: function (h, f) {
                            var d = this, e = d.series, l = e.chart, c = e.options.ignoreHiddenPoint; f = M(f, c); h !== d.visible && (d.visible = d.options.visible = h = "undefined" === typeof h ? !d.visible : h, e.options.data[e.data.indexOf(d)] = d.options, ["graphic", "dataLabel", "connector", "shadowGroup"].forEach(function (a) { if (d[a]) d[a][h ? "show" : "hide"](!0) }),
                                d.legendItem && l.legend.colorizeItem(d, h), h || "hover" !== d.state || d.setState(""), c && (e.isDirty = !0), f && l.redraw())
                        }, slice: function (h, f, d) { var e = this.series; w(d, e.chart); M(f, !0); this.sliced = this.options.sliced = J(h) ? h : !this.sliced; e.options.data[e.data.indexOf(this)] = this.options; this.graphic && this.graphic.animate(this.getTranslate()); this.shadowGroup && this.shadowGroup.animate(this.getTranslate()) }, getTranslate: function () { return this.sliced ? this.slicedTranslation : { translateX: 0, translateY: 0 } }, haloPath: function (h) {
                            var f =
                                this.shapeArgs; return this.sliced || !this.visible ? [] : this.series.chart.renderer.symbols.arc(f.x, f.y, f.r + h, f.r + h, { innerR: f.r - 1, start: f.start, end: f.end })
                        }, connectorShapes: {
                            fixedOffset: function (h, f, d) { var e = f.breakAt; f = f.touchingSliceAt; return [["M", h.x, h.y], d.softConnector ? ["C", h.x + ("left" === h.alignment ? -5 : 5), h.y, 2 * e.x - f.x, 2 * e.y - f.y, e.x, e.y] : ["L", e.x, e.y], ["L", f.x, f.y]] }, straight: function (h, f) { f = f.touchingSliceAt; return [["M", h.x, h.y], ["L", f.x, f.y]] }, crookedLine: function (h, f, d) {
                                f = f.touchingSliceAt; var e =
                                    this.series, l = e.center[0], c = e.chart.plotWidth, a = e.chart.plotLeft; e = h.alignment; var m = this.shapeArgs.r; d = A(d.crookDistance, 1); c = "left" === e ? l + m + (c + a - l - m) * (1 - d) : a + (l - m) * d; d = ["L", c, h.y]; l = !0; if ("left" === e ? c > h.x || c < f.x : c < h.x || c > f.x) l = !1; h = [["M", h.x, h.y]]; l && h.push(d); h.push(["L", f.x, f.y]); return h
                            }
                        }, getConnectorPath: function () {
                            var h = this.labelPosition, f = this.series.options.dataLabels, d = f.connectorShape, e = this.connectorShapes; e[d] && (d = e[d]); return d.call(this, { x: h.final.x, y: h.final.y, alignment: h.alignment },
                                h.connectorPosition, f)
                        }
                    }); ""
                }); O(q, "parts/DataLabels.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
                    var q = p.noop, B = p.seriesTypes, D = e.animObject, z = e.arrayMax, J = e.clamp, G = e.defined, m = e.extend, H = e.fireEvent, M = e.format, A = e.isArray, K = e.merge, w = e.objectEach, r = e.pick, u = e.relativeLength, C = e.splat, h = e.stableSort, f = p.Series; p.distribute = function (d, f, l) {
                        function c(a, c) { return a.target - c.target } var a, e = !0, t = d, m = []; var u = 0; var k = t.reducedLen || f; for (a = d.length; a--;)u += d[a].size; if (u > k) {
                            h(d, function (a,
                                c) { return (c.rank || 0) - (a.rank || 0) }); for (u = a = 0; u <= k;)u += d[a].size, a++; m = d.splice(a - 1, d.length)
                        } h(d, c); for (d = d.map(function (a) { return { size: a.size, targets: [a.target], align: r(a.align, .5) } }); e;) {
                            for (a = d.length; a--;)e = d[a], u = (Math.min.apply(0, e.targets) + Math.max.apply(0, e.targets)) / 2, e.pos = J(u - e.size * e.align, 0, f - e.size); a = d.length; for (e = !1; a--;)0 < a && d[a - 1].pos + d[a - 1].size > d[a].pos && (d[a - 1].size += d[a].size, d[a - 1].targets = d[a - 1].targets.concat(d[a].targets), d[a - 1].align = .5, d[a - 1].pos + d[a - 1].size > f && (d[a - 1].pos =
                                f - d[a - 1].size), d.splice(a, 1), e = !0)
                        } t.push.apply(t, m); a = 0; d.some(function (c) { var d = 0; if (c.targets.some(function () { t[a].pos = c.pos + d; if ("undefined" !== typeof l && Math.abs(t[a].pos - t[a].target) > l) return t.slice(0, a + 1).forEach(function (a) { delete a.pos }), t.reducedLen = (t.reducedLen || f) - .1 * f, t.reducedLen > .1 * f && p.distribute(t, f, l), !0; d += t[a].size; a++ })) return !0 }); h(t, c)
                    }; f.prototype.drawDataLabels = function () {
                        function d(a, c) {
                            var b = c.filter; return b ? (c = b.operator, a = a[b.property], b = b.value, ">" === c && a > b || "<" ===
                                c && a < b || ">=" === c && a >= b || "<=" === c && a <= b || "==" === c && a == b || "===" === c && a === b ? !0 : !1) : !0
                        } function f(a, c) { var b = [], d; if (A(a) && !A(c)) b = a.map(function (a) { return K(a, c) }); else if (A(c) && !A(a)) b = c.map(function (b) { return K(a, b) }); else if (A(a) || A(c)) for (d = Math.max(a.length, c.length); d--;)b[d] = K(a[d], c[d]); else b = K(a, c); return b } var h = this, c = h.chart, a = h.options, e = a.dataLabels, m = h.points, u, F = h.hasRendered || 0, k = D(a.animation).duration, y = Math.min(k, 200), p = !c.renderer.forExport && r(e.defer, 0 < y), q = c.renderer; e = f(f(c.options.plotOptions &&
                            c.options.plotOptions.series && c.options.plotOptions.series.dataLabels, c.options.plotOptions && c.options.plotOptions[h.type] && c.options.plotOptions[h.type].dataLabels), e); H(this, "drawDataLabels"); if (A(e) || e.enabled || h._hasPointLabels) {
                                var g = h.plotGroup("dataLabelsGroup", "data-labels", p && !F ? "hidden" : "inherit", e.zIndex || 6); p && (g.attr({ opacity: +F }), F || setTimeout(function () { var b = h.dataLabelsGroup; b && (h.visible && g.show(!0), b[a.animation ? "animate" : "attr"]({ opacity: 1 }, { duration: y })) }, k - y)); m.forEach(function (b) {
                                    u =
                                    C(f(e, b.dlOptions || b.options && b.options.dataLabels)); u.forEach(function (k, f) {
                                        var l = k.enabled && (!b.isNull || b.dataLabelOnNull) && d(b, k), n = b.dataLabels ? b.dataLabels[f] : b.dataLabel, e = b.connectors ? b.connectors[f] : b.connector, t = r(k.distance, b.labelDistance), m = !n; if (l) {
                                            var v = b.getLabelConfig(); var y = r(k[b.formatPrefix + "Format"], k.format); v = G(y) ? M(y, v, c) : (k[b.formatPrefix + "Formatter"] || k.formatter).call(v, k); y = k.style; var x = k.rotation; c.styledMode || (y.color = r(k.color, y.color, h.color, "#000000"), "contrast" ===
                                                y.color ? (b.contrastColor = q.getContrast(b.color || h.color), y.color = !G(t) && k.inside || 0 > t || a.stacking ? b.contrastColor : "#000000") : delete b.contrastColor, a.cursor && (y.cursor = a.cursor)); var u = { r: k.borderRadius || 0, rotation: x, padding: k.padding, zIndex: 1 }; c.styledMode || (u.fill = k.backgroundColor, u.stroke = k.borderColor, u["stroke-width"] = k.borderWidth); w(u, function (a, b) { "undefined" === typeof a && delete u[b] })
                                        } !n || l && G(v) ? l && G(v) && (n ? u.text = v : (b.dataLabels = b.dataLabels || [], n = b.dataLabels[f] = x ? q.text(v, 0, -9999, k.useHTML).addClass("highcharts-data-label") :
                                            q.label(v, 0, -9999, k.shape, null, null, k.useHTML, null, "data-label"), f || (b.dataLabel = n), n.addClass(" highcharts-data-label-color-" + b.colorIndex + " " + (k.className || "") + (k.useHTML ? " highcharts-tracker" : ""))), n.options = k, n.attr(u), c.styledMode || n.css(y).shadow(k.shadow), n.added || n.add(g), k.textPath && !k.useHTML && (n.setTextPath(b.getDataLabelPath && b.getDataLabelPath(n) || b.graphic, k.textPath), b.dataLabelPath && !k.textPath.enabled && (b.dataLabelPath = b.dataLabelPath.destroy())), h.alignDataLabel(b, n, k, null, m)) : (b.dataLabel =
                                                b.dataLabel && b.dataLabel.destroy(), b.dataLabels && (1 === b.dataLabels.length ? delete b.dataLabels : delete b.dataLabels[f]), f || delete b.dataLabel, e && (b.connector = b.connector.destroy(), b.connectors && (1 === b.connectors.length ? delete b.connectors : delete b.connectors[f])))
                                    })
                                })
                            } H(this, "afterDrawDataLabels")
                    }; f.prototype.alignDataLabel = function (d, f, h, c, a) {
                        var l = this, e = this.chart, t = this.isCartesian && e.inverted, u = this.enabledDataSorting, k = r(d.dlBox && d.dlBox.centerX, d.plotX, -9999), y = r(d.plotY, -9999), p = f.getBBox(),
                        q = h.rotation, g = h.align, b = e.isInsidePlot(k, Math.round(y), t), n = "justify" === r(h.overflow, u ? "none" : "justify"), C = this.visible && !1 !== d.visible && (d.series.forceDL || u && !n || b || h.inside && c && e.isInsidePlot(k, t ? c.x + 1 : c.y + c.height - 1, t)); var w = function (c) { u && l.xAxis && !n && l.setDataLabelStartPos(d, f, a, b, c) }; if (C) {
                            var A = e.renderer.fontMetrics(e.styledMode ? void 0 : h.style.fontSize, f).b; c = m({ x: t ? this.yAxis.len - y : k, y: Math.round(t ? this.xAxis.len - k : y), width: 0, height: 0 }, c); m(h, { width: p.width, height: p.height }); q ? (n = !1, k =
                                e.renderer.rotCorr(A, q), k = { x: c.x + (h.x || 0) + c.width / 2 + k.x, y: c.y + (h.y || 0) + { top: 0, middle: .5, bottom: 1 }[h.verticalAlign] * c.height }, w(k), f[a ? "attr" : "animate"](k).attr({ align: g }), w = (q + 720) % 360, w = 180 < w && 360 > w, "left" === g ? k.y -= w ? p.height : 0 : "center" === g ? (k.x -= p.width / 2, k.y -= p.height / 2) : "right" === g && (k.x -= p.width, k.y -= w ? 0 : p.height), f.placed = !0, f.alignAttr = k) : (w(c), f.align(h, null, c), k = f.alignAttr); n && 0 <= c.height ? this.justifyDataLabel(f, h, k, p, c, a) : r(h.crop, !0) && (C = e.isInsidePlot(k.x, k.y) && e.isInsidePlot(k.x + p.width,
                                    k.y + p.height)); if (h.shape && !q) f[a ? "attr" : "animate"]({ anchorX: t ? e.plotWidth - d.plotY : d.plotX, anchorY: t ? e.plotHeight - d.plotX : d.plotY })
                        } a && u && (f.placed = !1); C || u && !n || (f.hide(!0), f.placed = !1)
                    }; f.prototype.setDataLabelStartPos = function (d, f, h, c, a) {
                        var l = this.chart, e = l.inverted, t = this.xAxis, m = t.reversed, k = e ? f.height / 2 : f.width / 2; d = (d = d.pointWidth) ? d / 2 : 0; t = e ? a.x : m ? -k - d : t.width - k + d; a = e ? m ? this.yAxis.height - k + d : -k - d : a.y; f.startXPos = t; f.startYPos = a; c ? "hidden" === f.visibility && (f.show(), f.attr({ opacity: 0 }).animate({ opacity: 1 })) :
                            f.attr({ opacity: 1 }).animate({ opacity: 0 }, void 0, f.hide); l.hasRendered && (h && f.attr({ x: f.startXPos, y: f.startYPos }), f.placed = !0)
                    }; f.prototype.justifyDataLabel = function (d, f, h, c, a, e) {
                        var l = this.chart, t = f.align, m = f.verticalAlign, k = d.box ? 0 : d.padding || 0, y = f.x; y = void 0 === y ? 0 : y; var r = f.y; var u = void 0 === r ? 0 : r; r = h.x + k; if (0 > r) { "right" === t && 0 <= y ? (f.align = "left", f.inside = !0) : y -= r; var g = !0 } r = h.x + c.width - k; r > l.plotWidth && ("left" === t && 0 >= y ? (f.align = "right", f.inside = !0) : y += l.plotWidth - r, g = !0); r = h.y + k; 0 > r && ("bottom" ===
                            m && 0 <= u ? (f.verticalAlign = "top", f.inside = !0) : u -= r, g = !0); r = h.y + c.height - k; r > l.plotHeight && ("top" === m && 0 >= u ? (f.verticalAlign = "bottom", f.inside = !0) : u += l.plotHeight - r, g = !0); g && (f.x = y, f.y = u, d.placed = !e, d.align(f, void 0, a)); return g
                    }; B.pie && (B.pie.prototype.dataLabelPositioners = {
                        radialDistributionY: function (d) { return d.top + d.distributeBox.pos }, radialDistributionX: function (d, f, h, c) { return d.getX(h < f.top + 2 || h > f.bottom - 2 ? c : h, f.half, f) }, justify: function (d, f, h) { return h[0] + (d.half ? -1 : 1) * (f + d.labelDistance) },
                        alignToPlotEdges: function (d, f, h, c) { d = d.getBBox().width; return f ? d + c : h - d - c }, alignToConnectors: function (d, f, h, c) { var a = 0, l; d.forEach(function (c) { l = c.dataLabel.getBBox().width; l > a && (a = l) }); return f ? a + c : h - a - c }
                    }, B.pie.prototype.drawDataLabels = function () {
                        var d = this, h = d.data, l, c = d.chart, a = d.options.dataLabels || {}, e = a.connectorPadding, m, u = c.plotWidth, F = c.plotHeight, k = c.plotLeft, y = Math.round(c.chartWidth / 3), q, w = d.center, g = w[2] / 2, b = w[1], n, C, A, B, D = [[], []], H, J, M, O, R = [0, 0, 0, 0], T = d.dataLabelPositioners, W; d.visible &&
                            (a.enabled || d._hasPointLabels) && (h.forEach(function (a) { a.dataLabel && a.visible && a.dataLabel.shortened && (a.dataLabel.attr({ width: "auto" }).css({ width: "auto", textOverflow: "clip" }), a.dataLabel.shortened = !1) }), f.prototype.drawDataLabels.apply(d), h.forEach(function (b) {
                            b.dataLabel && (b.visible ? (D[b.half].push(b), b.dataLabel._pos = null, !G(a.style.width) && !G(b.options.dataLabels && b.options.dataLabels.style && b.options.dataLabels.style.width) && b.dataLabel.getBBox().width > y && (b.dataLabel.css({
                                width: Math.round(.7 *
                                    y) + "px"
                            }), b.dataLabel.shortened = !0)) : (b.dataLabel = b.dataLabel.destroy(), b.dataLabels && 1 === b.dataLabels.length && delete b.dataLabels))
                            }), D.forEach(function (f, h) {
                                var t = f.length, m = [], v; if (t) {
                                    d.sortByAngle(f, h - .5); if (0 < d.maxLabelDistance) {
                                        var y = Math.max(0, b - g - d.maxLabelDistance); var x = Math.min(b + g + d.maxLabelDistance, c.plotHeight); f.forEach(function (a) {
                                        0 < a.labelDistance && a.dataLabel && (a.top = Math.max(0, b - g - a.labelDistance), a.bottom = Math.min(b + g + a.labelDistance, c.plotHeight), v = a.dataLabel.getBBox().height ||
                                            21, a.distributeBox = { target: a.labelPosition.natural.y - a.top + v / 2, size: v, rank: a.y }, m.push(a.distributeBox))
                                        }); y = x + v - y; p.distribute(m, y, y / 5)
                                    } for (O = 0; O < t; O++) {
                                        l = f[O]; A = l.labelPosition; n = l.dataLabel; M = !1 === l.visible ? "hidden" : "inherit"; J = y = A.natural.y; m && G(l.distributeBox) && ("undefined" === typeof l.distributeBox.pos ? M = "hidden" : (B = l.distributeBox.size, J = T.radialDistributionY(l))); delete l.positionIndex; if (a.justify) H = T.justify(l, g, w); else switch (a.alignTo) {
                                            case "connectors": H = T.alignToConnectors(f, h, u, k);
                                                break; case "plotEdges": H = T.alignToPlotEdges(n, h, u, k); break; default: H = T.radialDistributionX(d, l, J, y)
                                        }n._attr = { visibility: M, align: A.alignment }; W = l.options.dataLabels || {}; n._pos = { x: H + r(W.x, a.x) + ({ left: e, right: -e }[A.alignment] || 0), y: J + r(W.y, a.y) - 10 }; A.final.x = H; A.final.y = J; r(a.crop, !0) && (C = n.getBBox().width, y = null, H - C < e && 1 === h ? (y = Math.round(C - H + e), R[3] = Math.max(y, R[3])) : H + C > u - e && 0 === h && (y = Math.round(H + C - u + e), R[1] = Math.max(y, R[1])), 0 > J - B / 2 ? R[0] = Math.max(Math.round(-J + B / 2), R[0]) : J + B / 2 > F && (R[2] = Math.max(Math.round(J +
                                            B / 2 - F), R[2])), n.sideOverflow = y)
                                    }
                                }
                            }), 0 === z(R) || this.verifyDataLabelOverflow(R)) && (this.placeDataLabels(), this.points.forEach(function (b) {
                                W = K(a, b.options.dataLabels); if (m = r(W.connectorWidth, 1)) {
                                    var g; q = b.connector; if ((n = b.dataLabel) && n._pos && b.visible && 0 < b.labelDistance) {
                                        M = n._attr.visibility; if (g = !q) b.connector = q = c.renderer.path().addClass("highcharts-data-label-connector  highcharts-color-" + b.colorIndex + (b.className ? " " + b.className : "")).add(d.dataLabelsGroup), c.styledMode || q.attr({
                                            "stroke-width": m,
                                            stroke: W.connectorColor || b.color || "#666666"
                                        }); q[g ? "attr" : "animate"]({ d: b.getConnectorPath() }); q.attr("visibility", M)
                                    } else q && (b.connector = q.destroy())
                                }
                            }))
                    }, B.pie.prototype.placeDataLabels = function () {
                        this.points.forEach(function (d) {
                            var f = d.dataLabel, h; f && d.visible && ((h = f._pos) ? (f.sideOverflow && (f._attr.width = Math.max(f.getBBox().width - f.sideOverflow, 0), f.css({ width: f._attr.width + "px", textOverflow: (this.options.dataLabels.style || {}).textOverflow || "ellipsis" }), f.shortened = !0), f.attr(f._attr), f[f.moved ?
                                "animate" : "attr"](h), f.moved = !0) : f && f.attr({ y: -9999 })); delete d.distributeBox
                        }, this)
                    }, B.pie.prototype.alignDataLabel = q, B.pie.prototype.verifyDataLabelOverflow = function (d) {
                        var f = this.center, h = this.options, c = h.center, a = h.minSize || 80, e = null !== h.size; if (!e) {
                            if (null !== c[0]) var m = Math.max(f[2] - Math.max(d[1], d[3]), a); else m = Math.max(f[2] - d[1] - d[3], a), f[0] += (d[3] - d[1]) / 2; null !== c[1] ? m = J(m, a, f[2] - Math.max(d[0], d[2])) : (m = J(m, a, f[2] - d[0] - d[2]), f[1] += (d[0] - d[2]) / 2); m < f[2] ? (f[2] = m, f[3] = Math.min(u(h.innerSize ||
                                0, m), m), this.translate(f), this.drawDataLabels && this.drawDataLabels()) : e = !0
                        } return e
                    }); B.column && (B.column.prototype.alignDataLabel = function (d, h, l, c, a) {
                        var e = this.chart.inverted, m = d.series, t = d.dlBox || d.shapeArgs, u = r(d.below, d.plotY > r(this.translatedThreshold, m.yAxis.len)), k = r(l.inside, !!this.options.stacking); t && (c = K(t), 0 > c.y && (c.height += c.y, c.y = 0), t = c.y + c.height - m.yAxis.len, 0 < t && t < c.height && (c.height -= t), e && (c = { x: m.yAxis.len - c.y - c.height, y: m.xAxis.len - c.x - c.width, width: c.height, height: c.width }), k ||
                            (e ? (c.x += u ? 0 : c.width, c.width = 0) : (c.y += u ? c.height : 0, c.height = 0))); l.align = r(l.align, !e || k ? "center" : u ? "right" : "left"); l.verticalAlign = r(l.verticalAlign, e || k ? "middle" : u ? "top" : "bottom"); f.prototype.alignDataLabel.call(this, d, h, l, c, a); l.inside && d.contrastColor && h.css({ color: d.contrastColor })
                    })
                }); O(q, "modules/overlapping-datalabels.src.js", [q["parts/Chart.js"], q["parts/Utilities.js"]], function (p, e) {
                    var q = e.addEvent, B = e.fireEvent, D = e.isArray, z = e.isNumber, J = e.objectEach, G = e.pick; q(p, "render", function () {
                        var e =
                            []; (this.labelCollectors || []).forEach(function (m) { e = e.concat(m()) }); (this.yAxis || []).forEach(function (m) { m.stacking && m.options.stackLabels && !m.options.stackLabels.allowOverlap && J(m.stacking.stacks, function (m) { J(m, function (m) { e.push(m.label) }) }) }); (this.series || []).forEach(function (m) {
                                var p = m.options.dataLabels; m.visible && (!1 !== p.enabled || m._hasPointLabels) && (m.nodes || m.points).forEach(function (m) {
                                m.visible && (D(m.dataLabels) ? m.dataLabels : m.dataLabel ? [m.dataLabel] : []).forEach(function (p) {
                                    var q = p.options;
                                    p.labelrank = G(q.labelrank, m.labelrank, m.shapeArgs && m.shapeArgs.height); q.allowOverlap || e.push(p)
                                })
                                })
                            }); this.hideOverlappingLabels(e)
                    }); p.prototype.hideOverlappingLabels = function (e) {
                        var m = this, p = e.length, q = m.renderer, K, w, r, u = !1; var C = function (d) {
                            var f, h = d.box ? 0 : d.padding || 0, c = f = 0, a; if (d && (!d.alignAttr || d.placed)) {
                                var e = d.alignAttr || { x: d.attr("x"), y: d.attr("y") }; var m = d.parentGroup; d.width || (f = d.getBBox(), d.width = f.width, d.height = f.height, f = q.fontMetrics(null, d.element).h); var r = d.width - 2 * h; (a = {
                                    left: "0",
                                    center: "0.5", right: "1"
                                }[d.alignValue]) ? c = +a * r : z(d.x) && Math.round(d.x) !== d.translateX && (c = d.x - d.translateX); return { x: e.x + (m.translateX || 0) + h - c, y: e.y + (m.translateY || 0) + h - f, width: d.width - 2 * h, height: d.height - 2 * h }
                            }
                        }; for (w = 0; w < p; w++)if (K = e[w]) K.oldOpacity = K.opacity, K.newOpacity = 1, K.absoluteBox = C(K); e.sort(function (d, f) { return (f.labelrank || 0) - (d.labelrank || 0) }); for (w = 0; w < p; w++) {
                            var h = (C = e[w]) && C.absoluteBox; for (K = w + 1; K < p; ++K) {
                                var f = (r = e[K]) && r.absoluteBox; !h || !f || C === r || 0 === C.newOpacity || 0 === r.newOpacity ||
                                    f.x > h.x + h.width || f.x + f.width < h.x || f.y > h.y + h.height || f.y + f.height < h.y || ((C.labelrank < r.labelrank ? C : r).newOpacity = 0)
                            }
                        } e.forEach(function (d) {
                            if (d) {
                                var f = d.newOpacity; d.oldOpacity !== f && (d.alignAttr && d.placed ? (d[f ? "removeClass" : "addClass"]("highcharts-data-label-hidden"), u = !0, d.alignAttr.opacity = f, d[d.isOld ? "animate" : "attr"](d.alignAttr, null, function () { m.styledMode || d.css({ pointerEvents: f ? "auto" : "none" }); d.visibility = f ? "inherit" : "hidden"; d.placed = !!f }), B(m, "afterHideOverlappingLabel")) : d.attr({ opacity: f }));
                                d.isOld = !0
                            }
                        }); u && B(m, "afterHideAllOverlappingLabels")
                    }
                }); O(q, "parts/Interaction.js", [q["parts/Chart.js"], q["parts/Globals.js"], q["parts/Legend.js"], q["parts/Options.js"], q["parts/Point.js"], q["parts/Utilities.js"]], function (p, e, q, B, D, z) {
                    var J = B.defaultOptions, G = z.addEvent, m = z.createElement, H = z.css, M = z.defined, A = z.extend, K = z.fireEvent, w = z.isArray, r = z.isFunction, u = z.isNumber, C = z.isObject, h = z.merge, f = z.objectEach, d = z.pick, t = e.hasTouch; B = e.Series; z = e.seriesTypes; var l = e.svg; var c = e.TrackerMixin = {
                        drawTrackerPoint: function () {
                            var a =
                                this, c = a.chart, d = c.pointer, f = function (a) { var c = d.getPointFromEvent(a); "undefined" !== typeof c && (d.isDirectTouch = !0, c.onMouseOver(a)) }, h; a.points.forEach(function (a) { h = w(a.dataLabels) ? a.dataLabels : a.dataLabel ? [a.dataLabel] : []; a.graphic && (a.graphic.element.point = a); h.forEach(function (c) { c.div ? c.div.point = a : c.element.point = a }) }); a._hasTracking || (a.trackerGroups.forEach(function (k) {
                                    if (a[k]) {
                                        a[k].addClass("highcharts-tracker").on("mouseover", f).on("mouseout", function (a) { d.onTrackerMouseOut(a) }); if (t) a[k].on("touchstart",
                                            f); !c.styledMode && a.options.cursor && a[k].css(H).css({ cursor: a.options.cursor })
                                    }
                                }), a._hasTracking = !0); K(this, "afterDrawTracker")
                        }, drawTrackerGraph: function () {
                            var a = this, c = a.options, d = c.trackByArea, f = [].concat(d ? a.areaPath : a.graphPath), h = a.chart, k = h.pointer, e = h.renderer, m = h.options.tooltip.snap, r = a.tracker, g = function (b) { if (h.hoverSeries !== a) a.onMouseOver() }, b = "rgba(192,192,192," + (l ? .0001 : .002) + ")"; r ? r.attr({ d: f }) : a.graph && (a.tracker = e.path(f).attr({ visibility: a.visible ? "visible" : "hidden", zIndex: 2 }).addClass(d ?
                                "highcharts-tracker-area" : "highcharts-tracker-line").add(a.group), h.styledMode || a.tracker.attr({ "stroke-linecap": "round", "stroke-linejoin": "round", stroke: b, fill: d ? b : "none", "stroke-width": a.graph.strokeWidth() + (d ? 0 : 2 * m) }), [a.tracker, a.markerGroup].forEach(function (a) { a.addClass("highcharts-tracker").on("mouseover", g).on("mouseout", function (a) { k.onTrackerMouseOut(a) }); c.cursor && !h.styledMode && a.css({ cursor: c.cursor }); if (t) a.on("touchstart", g) })); K(this, "afterDrawTracker")
                        }
                    }; z.column && (z.column.prototype.drawTracker =
                        c.drawTrackerPoint); z.pie && (z.pie.prototype.drawTracker = c.drawTrackerPoint); z.scatter && (z.scatter.prototype.drawTracker = c.drawTrackerPoint); A(q.prototype, {
                            setItemEvents: function (a, c, d) {
                                var f = this, e = f.chart.renderer.boxWrapper, k = a instanceof D, l = "highcharts-legend-" + (k ? "point" : "series") + "-active", m = f.chart.styledMode; (d ? [c, a.legendSymbol] : [a.legendGroup]).forEach(function (d) {
                                    if (d) d.on("mouseover", function () {
                                    a.visible && f.allItems.forEach(function (c) { a !== c && c.setState("inactive", !k) }); a.setState("hover");
                                        a.visible && e.addClass(l); m || c.css(f.options.itemHoverStyle)
                                    }).on("mouseout", function () { f.chart.styledMode || c.css(h(a.visible ? f.itemStyle : f.itemHiddenStyle)); f.allItems.forEach(function (c) { a !== c && c.setState("", !k) }); e.removeClass(l); a.setState() }).on("click", function (c) {
                                        var b = function () { a.setVisible && a.setVisible(); f.allItems.forEach(function (b) { a !== b && b.setState(a.visible ? "inactive" : "", !k) }) }; e.removeClass(l); c = { browserEvent: c }; a.firePointEvent ? a.firePointEvent("legendItemClick", c, b) : K(a, "legendItemClick",
                                            c, b)
                                    })
                                })
                            }, createCheckboxForItem: function (a) { a.checkbox = m("input", { type: "checkbox", className: "highcharts-legend-checkbox", checked: a.selected, defaultChecked: a.selected }, this.options.itemCheckboxStyle, this.chart.container); G(a.checkbox, "click", function (c) { K(a.series || a, "checkboxClick", { checked: c.target.checked, item: a }, function () { a.select() }) }) }
                        }); A(p.prototype, {
                            showResetZoom: function () {
                                function a() { c.zoomOut() } var c = this, d = J.lang, f = c.options.chart.resetZoomButton, h = f.theme, k = h.states, e = "chart" === f.relativeTo ||
                                    "spaceBox" === f.relativeTo ? null : "plotBox"; K(this, "beforeShowResetZoom", null, function () { c.resetZoomButton = c.renderer.button(d.resetZoom, null, null, a, h, k && k.hover).attr({ align: f.position.align, title: d.resetZoomTitle }).addClass("highcharts-reset-zoom").add().align(f.position, !1, e) }); K(this, "afterShowResetZoom")
                            }, zoomOut: function () { K(this, "selection", { resetSelection: !0 }, this.zoom) }, zoom: function (a) {
                                var c = this, f, h = c.pointer, e = !1, k = c.inverted ? h.mouseDownX : h.mouseDownY; !a || a.resetSelection ? (c.axes.forEach(function (a) {
                                    f =
                                    a.zoom()
                                }), h.initiated = !1) : a.xAxis.concat(a.yAxis).forEach(function (a) { var d = a.axis, g = c.inverted ? d.left : d.top, b = c.inverted ? g + d.width : g + d.height, n = d.isXAxis, l = !1; if (!n && k >= g && k <= b || n || !M(k)) l = !0; h[n ? "zoomX" : "zoomY"] && l && (f = d.zoom(a.min, a.max), d.displayBtn && (e = !0)) }); var l = c.resetZoomButton; e && !l ? c.showResetZoom() : !e && C(l) && (c.resetZoomButton = l.destroy()); f && c.redraw(d(c.options.chart.animation, a && a.animation, 100 > c.pointCount))
                            }, pan: function (a, c) {
                                var d = this, f = d.hoverPoints, h = d.options.chart, k = d.options.mapNavigation &&
                                    d.options.mapNavigation.enabled, l; c = "object" === typeof c ? c : { enabled: c, type: "x" }; h && h.panning && (h.panning = c); var m = c.type; K(this, "pan", { originalEvent: a }, function () {
                                        f && f.forEach(function (a) { a.setState() }); var c = [1]; "xy" === m ? c = [1, 0] : "y" === m && (c = [0]); c.forEach(function (c) {
                                            var b = d[c ? "xAxis" : "yAxis"][0], g = b.horiz, f = a[g ? "chartX" : "chartY"]; g = g ? "mouseDownX" : "mouseDownY"; var h = d[g], t = (b.pointRange || 0) / 2, r = b.reversed && !d.inverted || !b.reversed && d.inverted ? -1 : 1, v = b.getExtremes(), y = b.toValue(h - f, !0) + t * r; r = b.toValue(h +
                                                b.len - f, !0) - t * r; var p = r < y; h = p ? r : y; y = p ? y : r; var q = b.hasVerticalPanning(), x = b.panningState; b.series.forEach(function (a) { if (q && !c && (!x || x.isDirty)) { var b = a.getProcessedData(!0); a = a.getExtremes(b.yData, !0); x || (x = { startMin: Number.MAX_VALUE, startMax: -Number.MAX_VALUE }); u(a.dataMin) && u(a.dataMax) && (x.startMin = Math.min(a.dataMin, x.startMin), x.startMax = Math.max(a.dataMax, x.startMax)) } }); r = Math.min(e.pick(null === x || void 0 === x ? void 0 : x.startMin, v.dataMin), t ? v.min : b.toValue(b.toPixels(v.min) - b.minPixelPadding));
                                            t = Math.max(e.pick(null === x || void 0 === x ? void 0 : x.startMax, v.dataMax), t ? v.max : b.toValue(b.toPixels(v.max) + b.minPixelPadding)); b.panningState = x; b.isOrdinal || (p = r - h, 0 < p && (y += p, h = r), p = y - t, 0 < p && (y = t, h -= p), b.series.length && h !== v.min && y !== v.max && h >= r && y <= t && (b.setExtremes(h, y, !1, !1, { trigger: "pan" }), d.resetZoomButton || k || h === r || y === t || !m.match("y") || (d.showResetZoom(), b.displayBtn = !1), l = !0), d[g] = f)
                                        }); l && d.redraw(!1); H(d.container, { cursor: "move" })
                                    })
                            }
                        }); A(D.prototype, {
                            select: function (a, c) {
                                var f = this, h = f.series,
                                e = h.chart; this.selectedStaging = a = d(a, !f.selected); f.firePointEvent(a ? "select" : "unselect", { accumulate: c }, function () { f.selected = f.options.selected = a; h.options.data[h.data.indexOf(f)] = f.options; f.setState(a && "select"); c || e.getSelectedPoints().forEach(function (a) { var c = a.series; a.selected && a !== f && (a.selected = a.options.selected = !1, c.options.data[c.data.indexOf(a)] = a.options, a.setState(e.hoverPoints && c.options.inactiveOtherPoints ? "inactive" : ""), a.firePointEvent("unselect")) }) }); delete this.selectedStaging
                            },
                            onMouseOver: function (a) { var c = this.series.chart, d = c.pointer; a = a ? d.normalize(a) : d.getChartCoordinatesFromPoint(this, c.inverted); d.runPointActions(a, this) }, onMouseOut: function () { var a = this.series.chart; this.firePointEvent("mouseOut"); this.series.options.inactiveOtherPoints || (a.hoverPoints || []).forEach(function (a) { a.setState() }); a.hoverPoints = a.hoverPoint = null }, importEvents: function () {
                                if (!this.hasImportedEvents) {
                                    var a = this, c = h(a.series.options.point, a.options).events; a.events = c; f(c, function (c, d) {
                                    r(c) &&
                                        G(a, d, c)
                                    }); this.hasImportedEvents = !0
                                }
                            }, setState: function (a, c) {
                                var f = this.series, h = this.state, e = f.options.states[a || "normal"] || {}, k = J.plotOptions[f.type].marker && f.options.marker, l = k && !1 === k.enabled, m = k && k.states && k.states[a || "normal"] || {}, t = !1 === m.enabled, g = f.stateMarkerGraphic, b = this.marker || {}, n = f.chart, r = f.halo, u, p = k && f.markerAttribs; a = a || ""; if (!(a === this.state && !c || this.selected && "select" !== a || !1 === e.enabled || a && (t || l && !1 === m.enabled) || a && b.states && b.states[a] && !1 === b.states[a].enabled)) {
                                this.state =
                                    a; p && (u = f.markerAttribs(this, a)); if (this.graphic) {
                                        h && this.graphic.removeClass("highcharts-point-" + h); a && this.graphic.addClass("highcharts-point-" + a); if (!n.styledMode) { var q = f.pointAttribs(this, a); var x = d(n.options.chart.animation, e.animation); f.options.inactiveOtherPoints && q.opacity && ((this.dataLabels || []).forEach(function (a) { a && a.animate({ opacity: q.opacity }, x) }), this.connector && this.connector.animate({ opacity: q.opacity }, x)); this.graphic.animate(q, x) } u && this.graphic.animate(u, d(n.options.chart.animation,
                                            m.animation, k.animation)); g && g.hide()
                                    } else { if (a && m) { h = b.symbol || f.symbol; g && g.currentSymbol !== h && (g = g.destroy()); if (u) if (g) g[c ? "animate" : "attr"]({ x: u.x, y: u.y }); else h && (f.stateMarkerGraphic = g = n.renderer.symbol(h, u.x, u.y, u.width, u.height).add(f.markerGroup), g.currentSymbol = h); !n.styledMode && g && g.attr(f.pointAttribs(this, a)) } g && (g[a && this.isInside ? "show" : "hide"](), g.element.point = this) } a = e.halo; e = (g = this.graphic || g) && g.visibility || "inherit"; a && a.size && g && "hidden" !== e && !this.isCluster ? (r || (f.halo = r =
                                        n.renderer.path().add(g.parentGroup)), r.show()[c ? "animate" : "attr"]({ d: this.haloPath(a.size) }), r.attr({ "class": "highcharts-halo highcharts-color-" + d(this.colorIndex, f.colorIndex) + (this.className ? " " + this.className : ""), visibility: e, zIndex: -1 }), r.point = this, n.styledMode || r.attr(A({ fill: this.color || f.color, "fill-opacity": a.opacity }, a.attributes))) : r && r.point && r.point.haloPath && r.animate({ d: r.point.haloPath(0) }, null, r.hide); K(this, "afterSetState")
                                }
                            }, haloPath: function (a) {
                                return this.series.chart.renderer.symbols.circle(Math.floor(this.plotX) -
                                    a, this.plotY - a, 2 * a, 2 * a)
                            }
                        }); A(B.prototype, {
                            onMouseOver: function () { var a = this.chart, c = a.hoverSeries; a.pointer.setHoverChartIndex(); if (c && c !== this) c.onMouseOut(); this.options.events.mouseOver && K(this, "mouseOver"); this.setState("hover"); a.hoverSeries = this }, onMouseOut: function () {
                                var a = this.options, c = this.chart, d = c.tooltip, f = c.hoverPoint; c.hoverSeries = null; if (f) f.onMouseOut(); this && a.events.mouseOut && K(this, "mouseOut"); !d || this.stickyTracking || d.shared && !this.noSharedTooltip || d.hide(); c.series.forEach(function (a) {
                                    a.setState("",
                                        !0)
                                })
                            }, setState: function (a, c) {
                                var f = this, h = f.options, e = f.graph, k = h.inactiveOtherPoints, l = h.states, m = h.lineWidth, t = h.opacity, g = d(l[a || "normal"] && l[a || "normal"].animation, f.chart.options.chart.animation); h = 0; a = a || ""; if (f.state !== a && ([f.group, f.markerGroup, f.dataLabelsGroup].forEach(function (b) { b && (f.state && b.removeClass("highcharts-series-" + f.state), a && b.addClass("highcharts-series-" + a)) }), f.state = a, !f.chart.styledMode)) {
                                    if (l[a] && !1 === l[a].enabled) return; a && (m = l[a].lineWidth || m + (l[a].lineWidthPlus ||
                                        0), t = d(l[a].opacity, t)); if (e && !e.dashstyle) for (l = { "stroke-width": m }, e.animate(l, g); f["zone-graph-" + h];)f["zone-graph-" + h].attr(l), h += 1; k || [f.group, f.markerGroup, f.dataLabelsGroup, f.labelBySeries].forEach(function (a) { a && a.animate({ opacity: t }, g) })
                                } c && k && f.points && f.setAllPointsToState(a)
                            }, setAllPointsToState: function (a) { this.points.forEach(function (c) { c.setState && c.setState(a) }) }, setVisible: function (a, c) {
                                var d = this, f = d.chart, h = d.legendItem, k = f.options.chart.ignoreHiddenSeries, e = d.visible; var l = (d.visible =
                                    a = d.options.visible = d.userOptions.visible = "undefined" === typeof a ? !e : a) ? "show" : "hide";["group", "dataLabelsGroup", "markerGroup", "tracker", "tt"].forEach(function (a) { if (d[a]) d[a][l]() }); if (f.hoverSeries === d || (f.hoverPoint && f.hoverPoint.series) === d) d.onMouseOut(); h && f.legend.colorizeItem(d, a); d.isDirty = !0; d.options.stacking && f.series.forEach(function (a) { a.options.stacking && a.visible && (a.isDirty = !0) }); d.linkedSeries.forEach(function (c) { c.setVisible(a, !1) }); k && (f.isDirtyBox = !0); K(d, l); !1 !== c && f.redraw()
                            },
                            show: function () { this.setVisible(!0) }, hide: function () { this.setVisible(!1) }, select: function (a) { this.selected = a = this.options.selected = "undefined" === typeof a ? !this.selected : a; this.checkbox && (this.checkbox.checked = a); K(this, a ? "select" : "unselect") }, drawTracker: c.drawTrackerGraph
                        })
                }); O(q, "parts/Responsive.js", [q["parts/Chart.js"], q["parts/Utilities.js"]], function (p, e) {
                    var q = e.find, B = e.isArray, D = e.isObject, z = e.merge, J = e.objectEach, G = e.pick, m = e.splat, H = e.uniqueKey; p.prototype.setResponsive = function (e, m) {
                        var p =
                            this.options.responsive, w = [], r = this.currentResponsive; !m && p && p.rules && p.rules.forEach(function (e) { "undefined" === typeof e._id && (e._id = H()); this.matchResponsiveRule(e, w) }, this); m = z.apply(0, w.map(function (e) { return q(p.rules, function (m) { return m._id === e }).chartOptions })); m.isResponsiveOptions = !0; w = w.toString() || void 0; w !== (r && r.ruleIds) && (r && this.update(r.undoOptions, e, !0), w ? (r = this.currentOptions(m), r.isResponsiveOptions = !0, this.currentResponsive = { ruleIds: w, mergedOptions: m, undoOptions: r }, this.update(m,
                                e, !0)) : this.currentResponsive = void 0)
                    }; p.prototype.matchResponsiveRule = function (e, m) { var p = e.condition; (p.callback || function () { return this.chartWidth <= G(p.maxWidth, Number.MAX_VALUE) && this.chartHeight <= G(p.maxHeight, Number.MAX_VALUE) && this.chartWidth >= G(p.minWidth, 0) && this.chartHeight >= G(p.minHeight, 0) }).call(this) && m.push(e._id) }; p.prototype.currentOptions = function (e) {
                        function p(e, u, w, h) {
                            var f; J(e, function (d, e) {
                                if (!h && -1 < q.collectionsWithUpdate.indexOf(e)) for (d = m(d), w[e] = [], f = 0; f < Math.max(d.length,
                                    u[e].length); f++)u[e][f] && (void 0 === d[f] ? w[e][f] = u[e][f] : (w[e][f] = {}, p(d[f], u[e][f], w[e][f], h + 1))); else D(d) ? (w[e] = B(d) ? [] : {}, p(d, u[e] || {}, w[e], h + 1)) : w[e] = "undefined" === typeof u[e] ? null : u[e]
                            })
                        } var q = this, w = {}; p(e, this.options, w, 0); return w
                    }
                }); O(q, "masters/highcharts.src.js", [q["parts/Globals.js"]], function (p) { return p }); O(q, "parts/NavigatorAxis.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
                    var q = p.isTouchDevice, B = e.addEvent, D = e.correctFloat, z = e.defined, J = e.isNumber, G = e.pick, m =
                        function () { function e(e) { this.axis = e } e.prototype.destroy = function () { this.axis = void 0 }; e.prototype.toFixedRange = function (e, m, p, q) { var r = this.axis, u = r.chart; u = u && u.fixedRange; var w = (r.pointRange || 0) / 2; e = G(p, r.translate(e, !0, !r.horiz)); m = G(q, r.translate(m, !0, !r.horiz)); r = u && (m - e) / u; z(p) || (e = D(e + w)); z(q) || (m = D(m - w)); .7 < r && 1.3 > r && (q ? e = m - u : m = e + u); J(e) && J(m) || (e = m = void 0); return { min: e, max: m } }; return e }(); return function () {
                            function e() { } e.compose = function (e) {
                                e.keepProps.push("navigatorAxis"); B(e, "init", function () {
                                this.navigatorAxis ||
                                    (this.navigatorAxis = new m(this))
                                }); B(e, "zoom", function (e) { var m = this.chart.options, p = m.navigator, r = this.navigatorAxis, u = m.chart.pinchType, C = m.rangeSelector; m = m.chart.zoomType; this.isXAxis && (p && p.enabled || C && C.enabled) && ("y" === m ? e.zoomed = !1 : (!q && "xy" === m || q && "xy" === u) && this.options.range && (p = r.previousZoom, z(e.newMin) ? r.previousZoom = [this.min, this.max] : p && (e.newMin = p[0], e.newMax = p[1], r.previousZoom = void 0))); "undefined" !== typeof e.zoomed && e.preventDefault() })
                            }; e.AdditionsClass = m; return e
                        }()
                }); O(q,
                    "parts/ScrollbarAxis.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
                        var q = e.addEvent, B = e.defined, D = e.pick; return function () {
                            function e() { } e.compose = function (e, z) {
                                q(e, "afterInit", function () {
                                    var e = this; e.options && e.options.scrollbar && e.options.scrollbar.enabled && (e.options.scrollbar.vertical = !e.horiz, e.options.startOnTick = e.options.endOnTick = !1, e.scrollbar = new z(e.chart.renderer, e.options.scrollbar, e.chart), q(e.scrollbar, "changed", function (m) {
                                        var q = D(e.options && e.options.min, e.min),
                                        A = D(e.options && e.options.max, e.max), z = B(e.dataMin) ? Math.min(q, e.min, e.dataMin) : q, w = (B(e.dataMax) ? Math.max(A, e.max, e.dataMax) : A) - z; B(q) && B(A) && (e.horiz && !e.reversed || !e.horiz && e.reversed ? (q = z + w * this.to, z += w * this.from) : (q = z + w * (1 - this.from), z += w * (1 - this.to)), D(this.options.liveRedraw, p.svg && !p.isTouchDevice && !this.chart.isBoosting) || "mouseup" === m.DOMType || !B(m.DOMType) ? e.setExtremes(z, q, !0, "mousemove" !== m.DOMType, m) : this.setRange(this.from, this.to))
                                    }))
                                }); q(e, "afterRender", function () {
                                    var e = Math.min(D(this.options.min,
                                        this.min), this.min, D(this.dataMin, this.min)), p = Math.max(D(this.options.max, this.max), this.max, D(this.dataMax, this.max)), q = this.scrollbar, A = this.axisTitleMargin + (this.titleOffset || 0), z = this.chart.scrollbarsOffsets, w = this.options.margin || 0; q && (this.horiz ? (this.opposite || (z[1] += A), q.position(this.left, this.top + this.height + 2 + z[1] - (this.opposite ? w : 0), this.width, this.height), this.opposite || (z[1] += w), A = 1) : (this.opposite && (z[0] += A), q.position(this.left + this.width + 2 + z[0] - (this.opposite ? 0 : w), this.top, this.width,
                                            this.height), this.opposite && (z[0] += w), A = 0), z[A] += q.size + q.options.margin, isNaN(e) || isNaN(p) || !B(this.min) || !B(this.max) || this.min === this.max ? q.setRange(0, 1) : (z = (this.min - e) / (p - e), e = (this.max - e) / (p - e), this.horiz && !this.reversed || !this.horiz && this.reversed ? q.setRange(z, e) : q.setRange(1 - e, 1 - z)))
                                }); q(e, "afterGetOffset", function () { var e = this.horiz ? 2 : 1, p = this.scrollbar; p && (this.chart.scrollbarsOffsets = [0, 0], this.chart.axisOffset[e] += p.size + p.options.margin) })
                            }; return e
                        }()
                    }); O(q, "parts/Scrollbar.js", [q["parts/Axis.js"],
                    q["parts/Globals.js"], q["parts/ScrollbarAxis.js"], q["parts/Utilities.js"], q["parts/Options.js"]], function (p, e, q, B, D) {
                        var z = B.addEvent, J = B.correctFloat, G = B.defined, m = B.destroyObjectProperties, H = B.fireEvent, M = B.merge, A = B.pick, K = B.removeEvent; B = D.defaultOptions; var w = e.hasTouch, r = e.isTouchDevice, u = e.swapXY = function (e, h) { h && e.forEach(function (f) { for (var d = f.length, h, e = 0; e < d; e += 2)h = f[e + 1], "number" === typeof h && (f[e + 1] = f[e + 2], f[e + 2] = h) }); return e }; D = function () {
                            function e(e, f, d) {
                            this._events = []; this.from =
                                this.chartY = this.chartX = 0; this.scrollbar = this.group = void 0; this.scrollbarButtons = []; this.scrollbarGroup = void 0; this.scrollbarLeft = 0; this.scrollbarRifles = void 0; this.scrollbarStrokeWidth = 1; this.to = this.size = this.scrollbarTop = 0; this.track = void 0; this.trackBorderWidth = 1; this.userOptions = {}; this.y = this.x = 0; this.chart = d; this.options = f; this.renderer = d.renderer; this.init(e, f, d)
                            } e.prototype.addEvents = function () {
                                var e = this.options.inverted ? [1, 0] : [0, 1], f = this.scrollbarButtons, d = this.scrollbarGroup.element,
                                m = this.track.element, l = this.mouseDownHandler.bind(this), c = this.mouseMoveHandler.bind(this), a = this.mouseUpHandler.bind(this); e = [[f[e[0]].element, "click", this.buttonToMinClick.bind(this)], [f[e[1]].element, "click", this.buttonToMaxClick.bind(this)], [m, "click", this.trackClick.bind(this)], [d, "mousedown", l], [d.ownerDocument, "mousemove", c], [d.ownerDocument, "mouseup", a]]; w && e.push([d, "touchstart", l], [d.ownerDocument, "touchmove", c], [d.ownerDocument, "touchend", a]); e.forEach(function (a) { z.apply(null, a) }); this._events =
                                    e
                            }; e.prototype.buttonToMaxClick = function (e) { var f = (this.to - this.from) * A(this.options.step, .2); this.updatePosition(this.from + f, this.to + f); H(this, "changed", { from: this.from, to: this.to, trigger: "scrollbar", DOMEvent: e }) }; e.prototype.buttonToMinClick = function (e) { var f = J(this.to - this.from) * A(this.options.step, .2); this.updatePosition(J(this.from - f), J(this.to - f)); H(this, "changed", { from: this.from, to: this.to, trigger: "scrollbar", DOMEvent: e }) }; e.prototype.cursorToScrollbarPosition = function (e) {
                                var f = this.options;
                                f = f.minWidth > this.calculatedWidth ? f.minWidth : 0; return { chartX: (e.chartX - this.x - this.xOffset) / (this.barWidth - f), chartY: (e.chartY - this.y - this.yOffset) / (this.barWidth - f) }
                            }; e.prototype.destroy = function () { var e = this.chart.scroller; this.removeEvents();["track", "scrollbarRifles", "scrollbar", "scrollbarGroup", "group"].forEach(function (f) { this[f] && this[f].destroy && (this[f] = this[f].destroy()) }, this); e && this === e.scrollbar && (e.scrollbar = null, m(e.scrollbarButtons)) }; e.prototype.drawScrollbarButton = function (e) {
                                var f =
                                    this.renderer, d = this.scrollbarButtons, h = this.options, l = this.size; var c = f.g().add(this.group); d.push(c); c = f.rect().addClass("highcharts-scrollbar-button").add(c); this.chart.styledMode || c.attr({ stroke: h.buttonBorderColor, "stroke-width": h.buttonBorderWidth, fill: h.buttonBackgroundColor }); c.attr(c.crisp({ x: -.5, y: -.5, width: l + 1, height: l + 1, r: h.buttonBorderRadius }, c.strokeWidth())); c = f.path(u([["M", l / 2 + (e ? -1 : 1), l / 2 - 3], ["L", l / 2 + (e ? -1 : 1), l / 2 + 3], ["L", l / 2 + (e ? 2 : -2), l / 2]], h.vertical)).addClass("highcharts-scrollbar-arrow").add(d[e]);
                                this.chart.styledMode || c.attr({ fill: h.buttonArrowColor })
                            }; e.prototype.init = function (h, f, d) { this.scrollbarButtons = []; this.renderer = h; this.userOptions = f; this.options = M(e.defaultOptions, f); this.chart = d; this.size = A(this.options.size, this.options.height); f.enabled && (this.render(), this.addEvents()) }; e.prototype.mouseDownHandler = function (e) {
                                e = this.chart.pointer.normalize(e); e = this.cursorToScrollbarPosition(e); this.chartX = e.chartX; this.chartY = e.chartY; this.initPositions = [this.from, this.to]; this.grabbedCenter =
                                    !0
                            }; e.prototype.mouseMoveHandler = function (e) { var f = this.chart.pointer.normalize(e), d = this.options.vertical ? "chartY" : "chartX", h = this.initPositions || []; !this.grabbedCenter || e.touches && 0 === e.touches[0][d] || (f = this.cursorToScrollbarPosition(f)[d], d = this[d], d = f - d, this.hasDragged = !0, this.updatePosition(h[0] + d, h[1] + d), this.hasDragged && H(this, "changed", { from: this.from, to: this.to, trigger: "scrollbar", DOMType: e.type, DOMEvent: e })) }; e.prototype.mouseUpHandler = function (e) {
                            this.hasDragged && H(this, "changed", {
                                from: this.from,
                                to: this.to, trigger: "scrollbar", DOMType: e.type, DOMEvent: e
                            }); this.grabbedCenter = this.hasDragged = this.chartX = this.chartY = null
                            }; e.prototype.position = function (e, f, d, m) {
                                var h = this.options.vertical, c = 0, a = this.rendered ? "animate" : "attr"; this.x = e; this.y = f + this.trackBorderWidth; this.width = d; this.xOffset = this.height = m; this.yOffset = c; h ? (this.width = this.yOffset = d = c = this.size, this.xOffset = f = 0, this.barWidth = m - 2 * d, this.x = e += this.options.margin) : (this.height = this.xOffset = m = f = this.size, this.barWidth = d - 2 * m, this.y += this.options.margin);
                                this.group[a]({ translateX: e, translateY: this.y }); this.track[a]({ width: d, height: m }); this.scrollbarButtons[1][a]({ translateX: h ? 0 : d - f, translateY: h ? m - c : 0 })
                            }; e.prototype.removeEvents = function () { this._events.forEach(function (e) { K.apply(null, e) }); this._events.length = 0 }; e.prototype.render = function () {
                                var e = this.renderer, f = this.options, d = this.size, m = this.chart.styledMode, l; this.group = l = e.g("scrollbar").attr({ zIndex: f.zIndex, translateY: -99999 }).add(); this.track = e.rect().addClass("highcharts-scrollbar-track").attr({
                                    x: 0,
                                    r: f.trackBorderRadius || 0, height: d, width: d
                                }).add(l); m || this.track.attr({ fill: f.trackBackgroundColor, stroke: f.trackBorderColor, "stroke-width": f.trackBorderWidth }); this.trackBorderWidth = this.track.strokeWidth(); this.track.attr({ y: -this.trackBorderWidth % 2 / 2 }); this.scrollbarGroup = e.g().add(l); this.scrollbar = e.rect().addClass("highcharts-scrollbar-thumb").attr({ height: d, width: d, r: f.barBorderRadius || 0 }).add(this.scrollbarGroup); this.scrollbarRifles = e.path(u([["M", -3, d / 4], ["L", -3, 2 * d / 3], ["M", 0, d / 4], ["L",
                                    0, 2 * d / 3], ["M", 3, d / 4], ["L", 3, 2 * d / 3]], f.vertical)).addClass("highcharts-scrollbar-rifles").add(this.scrollbarGroup); m || (this.scrollbar.attr({ fill: f.barBackgroundColor, stroke: f.barBorderColor, "stroke-width": f.barBorderWidth }), this.scrollbarRifles.attr({ stroke: f.rifleColor, "stroke-width": 1 })); this.scrollbarStrokeWidth = this.scrollbar.strokeWidth(); this.scrollbarGroup.translate(-this.scrollbarStrokeWidth % 2 / 2, -this.scrollbarStrokeWidth % 2 / 2); this.drawScrollbarButton(0); this.drawScrollbarButton(1)
                            }; e.prototype.setRange =
                                function (e, f) {
                                    var d = this.options, h = d.vertical, l = d.minWidth, c = this.barWidth, a, m = !this.rendered || this.hasDragged || this.chart.navigator && this.chart.navigator.hasDragged ? "attr" : "animate"; if (G(c)) {
                                        e = Math.max(e, 0); var r = Math.ceil(c * e); this.calculatedWidth = a = J(c * Math.min(f, 1) - r); a < l && (r = (c - l + a) * e, a = l); l = Math.floor(r + this.xOffset + this.yOffset); c = a / 2 - .5; this.from = e; this.to = f; h ? (this.scrollbarGroup[m]({ translateY: l }), this.scrollbar[m]({ height: a }), this.scrollbarRifles[m]({ translateY: c }), this.scrollbarTop = l,
                                            this.scrollbarLeft = 0) : (this.scrollbarGroup[m]({ translateX: l }), this.scrollbar[m]({ width: a }), this.scrollbarRifles[m]({ translateX: c }), this.scrollbarLeft = l, this.scrollbarTop = 0); 12 >= a ? this.scrollbarRifles.hide() : this.scrollbarRifles.show(!0); !1 === d.showFull && (0 >= e && 1 <= f ? this.group.hide() : this.group.show()); this.rendered = !0
                                    }
                                }; e.prototype.trackClick = function (e) {
                                    var f = this.chart.pointer.normalize(e), d = this.to - this.from, h = this.y + this.scrollbarTop, l = this.x + this.scrollbarLeft; this.options.vertical && f.chartY >
                                        h || !this.options.vertical && f.chartX > l ? this.updatePosition(this.from + d, this.to + d) : this.updatePosition(this.from - d, this.to - d); H(this, "changed", { from: this.from, to: this.to, trigger: "scrollbar", DOMEvent: e })
                                }; e.prototype.update = function (e) { this.destroy(); this.init(this.chart.renderer, M(!0, this.options, e), this.chart) }; e.prototype.updatePosition = function (e, f) { 1 < f && (e = J(1 - J(f - e)), f = 1); 0 > e && (f = J(f - e), e = 0); this.from = e; this.to = f }; e.defaultOptions = {
                                    height: r ? 20 : 14, barBorderRadius: 0, buttonBorderRadius: 0, liveRedraw: void 0,
                                    margin: 10, minWidth: 6, step: .2, zIndex: 3, barBackgroundColor: "#cccccc", barBorderWidth: 1, barBorderColor: "#cccccc", buttonArrowColor: "#333333", buttonBackgroundColor: "#e6e6e6", buttonBorderColor: "#cccccc", buttonBorderWidth: 1, rifleColor: "#333333", trackBackgroundColor: "#f2f2f2", trackBorderColor: "#f2f2f2", trackBorderWidth: 1
                                }; return e
                        }(); e.Scrollbar || (B.scrollbar = M(!0, D.defaultOptions, B.scrollbar), e.Scrollbar = D, q.compose(p, D)); return e.Scrollbar
                    }); O(q, "parts/Navigator.js", [q["parts/Axis.js"], q["parts/Chart.js"],
                    q["parts/Color.js"], q["parts/Globals.js"], q["parts/NavigatorAxis.js"], q["parts/Options.js"], q["parts/Scrollbar.js"], q["parts/Utilities.js"]], function (p, e, q, B, D, z, J, G) {
                        q = q.parse; var m = z.defaultOptions, H = G.addEvent, M = G.clamp, A = G.correctFloat, K = G.defined, w = G.destroyObjectProperties, r = G.erase, u = G.extend, C = G.find, h = G.isArray, f = G.isNumber, d = G.merge, t = G.pick, l = G.removeEvent, c = G.splat, a = B.hasTouch, x = B.isTouchDevice; z = B.Series; var v = function (a) {
                            for (var c = [], d = 1; d < arguments.length; d++)c[d - 1] = arguments[d]; c =
                                [].filter.call(c, f); if (c.length) return Math[a].apply(0, c)
                        }; G = "undefined" === typeof B.seriesTypes.areaspline ? "line" : "areaspline"; u(m, {
                            navigator: {
                                height: 40, margin: 25, maskInside: !0, handles: { width: 7, height: 15, symbols: ["navigator-handle", "navigator-handle"], enabled: !0, lineWidth: 1, backgroundColor: "#f2f2f2", borderColor: "#999999" }, maskFill: q("#6685c2").setOpacity(.3).get(), outlineColor: "#cccccc", outlineWidth: 1, series: {
                                    type: G, fillOpacity: .05, lineWidth: 1, compare: null, dataGrouping: {
                                        approximation: "average", enabled: !0,
                                        groupPixelWidth: 2, smoothed: !0, units: [["millisecond", [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], ["second", [1, 2, 5, 10, 15, 30]], ["minute", [1, 2, 5, 10, 15, 30]], ["hour", [1, 2, 3, 4, 6, 8, 12]], ["day", [1, 2, 3, 4]], ["week", [1, 2, 3]], ["month", [1, 3, 6]], ["year", null]]
                                    }, dataLabels: { enabled: !1, zIndex: 2 }, id: "highcharts-navigator-series", className: "highcharts-navigator-series", lineColor: null, marker: { enabled: !1 }, threshold: null
                                }, xAxis: {
                                    overscroll: 0, className: "highcharts-navigator-xaxis", tickLength: 0, lineWidth: 0, gridLineColor: "#e6e6e6",
                                    gridLineWidth: 1, tickPixelInterval: 200, labels: { align: "left", style: { color: "#999999" }, x: 3, y: -4 }, crosshair: !1
                                }, yAxis: { className: "highcharts-navigator-yaxis", gridLineWidth: 0, startOnTick: !1, endOnTick: !1, minPadding: .1, maxPadding: .1, labels: { enabled: !1 }, crosshair: !1, title: { text: null }, tickLength: 0, tickWidth: 0 }
                            }
                        }); B.Renderer.prototype.symbols["navigator-handle"] = function (a, c, d, f, e) {
                            a = (e && e.width || 0) / 2; c = Math.round(a / 3) + .5; e = e && e.height || 0; return [["M", -a - 1, .5], ["L", a, .5], ["L", a, e + .5], ["L", -a - 1, e + .5], ["L", -a -
                                1, .5], ["M", -c, 4], ["L", -c, e - 3], ["M", c - 1, 4], ["L", c - 1, e - 3]]
                        }; var E = function () {
                            function e(a) { this.zoomedMin = this.zoomedMax = this.yAxis = this.xAxis = this.top = this.size = this.shades = this.rendered = this.range = this.outlineHeight = this.outline = this.opposite = this.navigatorSize = this.navigatorSeries = this.navigatorOptions = this.navigatorGroup = this.navigatorEnabled = this.left = this.height = this.handles = this.chart = this.baseSeries = void 0; this.init(a) } e.prototype.drawHandle = function (a, c, d, f) {
                                var g = this.navigatorOptions.handles.height;
                                this.handles[c][f](d ? { translateX: Math.round(this.left + this.height / 2), translateY: Math.round(this.top + parseInt(a, 10) + .5 - g) } : { translateX: Math.round(this.left + parseInt(a, 10)), translateY: Math.round(this.top + this.height / 2 - g / 2 - 1) })
                            }; e.prototype.drawOutline = function (a, c, d, f) {
                                var g = this.navigatorOptions.maskInside, b = this.outline.strokeWidth(), k = b / 2, e = b % 2 / 2; b = this.outlineHeight; var h = this.scrollbarHeight || 0, l = this.size, m = this.left - h, t = this.top; d ? (m -= k, d = t + c + e, c = t + a + e, e = [["M", m + b, t - h - e], ["L", m + b, d], ["L", m, d],
                                ["L", m, c], ["L", m + b, c], ["L", m + b, t + l + h]], g && e.push(["M", m + b, d - k], ["L", m + b, c + k])) : (a += m + h - e, c += m + h - e, t += k, e = [["M", m, t], ["L", a, t], ["L", a, t + b], ["L", c, t + b], ["L", c, t], ["L", m + l + 2 * h, t]], g && e.push(["M", a - k, t], ["L", c + k, t])); this.outline[f]({ d: e })
                            }; e.prototype.drawMasks = function (a, c, d, f) {
                                var g = this.left, b = this.top, k = this.height; if (d) { var e = [g, g, g]; var h = [b, b + a, b + c]; var l = [k, k, k]; var m = [a, c - a, this.size - c] } else e = [g, g + a, g + c], h = [b, b, b], l = [a, c - a, this.size - c], m = [k, k, k]; this.shades.forEach(function (a, b) {
                                    a[f]({
                                        x: e[b],
                                        y: h[b], width: l[b], height: m[b]
                                    })
                                })
                            }; e.prototype.renderElements = function () {
                                var a = this, c = a.navigatorOptions, d = c.maskInside, f = a.chart, g = f.renderer, b, e = { cursor: f.inverted ? "ns-resize" : "ew-resize" }; a.navigatorGroup = b = g.g("navigator").attr({ zIndex: 8, visibility: "hidden" }).add();[!d, d, !d].forEach(function (d, k) { a.shades[k] = g.rect().addClass("highcharts-navigator-mask" + (1 === k ? "-inside" : "-outside")).add(b); f.styledMode || a.shades[k].attr({ fill: d ? c.maskFill : "rgba(0,0,0,0)" }).css(1 === k && e) }); a.outline = g.path().addClass("highcharts-navigator-outline").add(b);
                                f.styledMode || a.outline.attr({ "stroke-width": c.outlineWidth, stroke: c.outlineColor }); c.handles.enabled && [0, 1].forEach(function (d) { c.handles.inverted = f.inverted; a.handles[d] = g.symbol(c.handles.symbols[d], -c.handles.width / 2 - 1, 0, c.handles.width, c.handles.height, c.handles); a.handles[d].attr({ zIndex: 7 - d }).addClass("highcharts-navigator-handle highcharts-navigator-handle-" + ["left", "right"][d]).add(b); if (!f.styledMode) { var k = c.handles; a.handles[d].attr({ fill: k.backgroundColor, stroke: k.borderColor, "stroke-width": k.lineWidth }).css(e) } })
                            };
                            e.prototype.update = function (a) { (this.series || []).forEach(function (a) { a.baseSeries && delete a.baseSeries.navigatorSeries }); this.destroy(); d(!0, this.chart.options.navigator, this.options, a); this.init(this.chart) }; e.prototype.render = function (a, c, d, e) {
                                var g = this.chart, b = this.scrollbarHeight, k, h = this.xAxis, l = h.pointRange || 0; var m = h.navigatorAxis.fake ? g.xAxis[0] : h; var r = this.navigatorEnabled, p, u = this.rendered; var y = g.inverted; var v = g.xAxis[0].minRange, q = g.xAxis[0].options.maxRange; if (!this.hasDragged || K(d)) {
                                    a =
                                    A(a - l / 2); c = A(c + l / 2); if (!f(a) || !f(c)) if (u) d = 0, e = t(h.width, m.width); else return; this.left = t(h.left, g.plotLeft + b + (y ? g.plotWidth : 0)); this.size = p = k = t(h.len, (y ? g.plotHeight : g.plotWidth) - 2 * b); g = y ? b : k + 2 * b; d = t(d, h.toPixels(a, !0)); e = t(e, h.toPixels(c, !0)); f(d) && Infinity !== Math.abs(d) || (d = 0, e = g); a = h.toValue(d, !0); c = h.toValue(e, !0); var x = Math.abs(A(c - a)); x < v ? this.grabbedLeft ? d = h.toPixels(c - v - l, !0) : this.grabbedRight && (e = h.toPixels(a + v + l, !0)) : K(q) && A(x - l) > q && (this.grabbedLeft ? d = h.toPixels(c - q - l, !0) : this.grabbedRight &&
                                        (e = h.toPixels(a + q + l, !0))); this.zoomedMax = M(Math.max(d, e), 0, p); this.zoomedMin = M(this.fixedWidth ? this.zoomedMax - this.fixedWidth : Math.min(d, e), 0, p); this.range = this.zoomedMax - this.zoomedMin; p = Math.round(this.zoomedMax); d = Math.round(this.zoomedMin); r && (this.navigatorGroup.attr({ visibility: "visible" }), u = u && !this.hasDragged ? "animate" : "attr", this.drawMasks(d, p, y, u), this.drawOutline(d, p, y, u), this.navigatorOptions.handles.enabled && (this.drawHandle(d, 0, y, u), this.drawHandle(p, 1, y, u))); this.scrollbar && (y ? (y = this.top -
                                            b, m = this.left - b + (r || !m.opposite ? 0 : (m.titleOffset || 0) + m.axisTitleMargin), b = k + 2 * b) : (y = this.top + (r ? this.height : -b), m = this.left - b), this.scrollbar.position(m, y, g, b), this.scrollbar.setRange(this.zoomedMin / (k || 1), this.zoomedMax / (k || 1))); this.rendered = !0
                                }
                            }; e.prototype.addMouseEvents = function () {
                                var c = this, d = c.chart, f = d.container, e = [], g, b; c.mouseMoveHandler = g = function (a) { c.onMouseMove(a) }; c.mouseUpHandler = b = function (a) { c.onMouseUp(a) }; e = c.getPartsEvents("mousedown"); e.push(H(d.renderTo, "mousemove", g), H(f.ownerDocument,
                                    "mouseup", b)); a && (e.push(H(d.renderTo, "touchmove", g), H(f.ownerDocument, "touchend", b)), e.concat(c.getPartsEvents("touchstart"))); c.eventsToUnbind = e; c.series && c.series[0] && e.push(H(c.series[0].xAxis, "foundExtremes", function () { d.navigator.modifyNavigatorAxisExtremes() }))
                            }; e.prototype.getPartsEvents = function (a) { var c = this, d = [];["shades", "handles"].forEach(function (f) { c[f].forEach(function (g, b) { d.push(H(g.element, a, function (a) { c[f + "Mousedown"](a, b) })) }) }); return d }; e.prototype.shadesMousedown = function (a,
                                c) {
                                    a = this.chart.pointer.normalize(a); var d = this.chart, f = this.xAxis, g = this.zoomedMin, b = this.left, k = this.size, e = this.range, h = a.chartX; d.inverted && (h = a.chartY, b = this.top); if (1 === c) this.grabbedCenter = h, this.fixedWidth = e, this.dragOffset = h - g; else {
                                        a = h - b - e / 2; if (0 === c) a = Math.max(0, a); else if (2 === c && a + e >= k) if (a = k - e, this.reversedExtremes) { a -= e; var l = this.getUnionExtremes().dataMin } else var m = this.getUnionExtremes().dataMax; a !== g && (this.fixedWidth = e, c = f.navigatorAxis.toFixedRange(a, a + e, l, m), K(c.min) && d.xAxis[0].setExtremes(Math.min(c.min,
                                            c.max), Math.max(c.min, c.max), !0, null, { trigger: "navigator" }))
                                    }
                            }; e.prototype.handlesMousedown = function (a, c) { this.chart.pointer.normalize(a); a = this.chart; var d = a.xAxis[0], f = this.reversedExtremes; 0 === c ? (this.grabbedLeft = !0, this.otherHandlePos = this.zoomedMax, this.fixedExtreme = f ? d.min : d.max) : (this.grabbedRight = !0, this.otherHandlePos = this.zoomedMin, this.fixedExtreme = f ? d.max : d.min); a.fixedRange = null }; e.prototype.onMouseMove = function (a) {
                                var c = this, d = c.chart, f = c.left, g = c.navigatorSize, b = c.range, k = c.dragOffset,
                                e = d.inverted; a.touches && 0 === a.touches[0].pageX || (a = d.pointer.normalize(a), d = a.chartX, e && (f = c.top, d = a.chartY), c.grabbedLeft ? (c.hasDragged = !0, c.render(0, 0, d - f, c.otherHandlePos)) : c.grabbedRight ? (c.hasDragged = !0, c.render(0, 0, c.otherHandlePos, d - f)) : c.grabbedCenter && (c.hasDragged = !0, d < k ? d = k : d > g + k - b && (d = g + k - b), c.render(0, 0, d - k, d - k + b)), c.hasDragged && c.scrollbar && t(c.scrollbar.options.liveRedraw, B.svg && !x && !this.chart.isBoosting) && (a.DOMType = a.type, setTimeout(function () { c.onMouseUp(a) }, 0)))
                            }; e.prototype.onMouseUp =
                                function (a) {
                                    var c = this.chart, d = this.xAxis, f = this.scrollbar, g = a.DOMEvent || a, b = c.inverted, k = this.rendered && !this.hasDragged ? "animate" : "attr", e = Math.round(this.zoomedMax), h = Math.round(this.zoomedMin); if (this.hasDragged && (!f || !f.hasDragged) || "scrollbar" === a.trigger) {
                                        f = this.getUnionExtremes(); if (this.zoomedMin === this.otherHandlePos) var l = this.fixedExtreme; else if (this.zoomedMax === this.otherHandlePos) var m = this.fixedExtreme; this.zoomedMax === this.size && (m = this.reversedExtremes ? f.dataMin : f.dataMax); 0 === this.zoomedMin &&
                                            (l = this.reversedExtremes ? f.dataMax : f.dataMin); d = d.navigatorAxis.toFixedRange(this.zoomedMin, this.zoomedMax, l, m); K(d.min) && c.xAxis[0].setExtremes(Math.min(d.min, d.max), Math.max(d.min, d.max), !0, this.hasDragged ? !1 : null, { trigger: "navigator", triggerOp: "navigator-drag", DOMEvent: g })
                                    } "mousemove" !== a.DOMType && "touchmove" !== a.DOMType && (this.grabbedLeft = this.grabbedRight = this.grabbedCenter = this.fixedWidth = this.fixedExtreme = this.otherHandlePos = this.hasDragged = this.dragOffset = null); this.navigatorEnabled && (this.shades &&
                                        this.drawMasks(h, e, b, k), this.outline && this.drawOutline(h, e, b, k), this.navigatorOptions.handles.enabled && Object.keys(this.handles).length === this.handles.length && (this.drawHandle(h, 0, b, k), this.drawHandle(e, 1, b, k)))
                                }; e.prototype.removeEvents = function () { this.eventsToUnbind && (this.eventsToUnbind.forEach(function (a) { a() }), this.eventsToUnbind = void 0); this.removeBaseSeriesEvents() }; e.prototype.removeBaseSeriesEvents = function () {
                                    var a = this.baseSeries || []; this.navigatorEnabled && a[0] && (!1 !== this.navigatorOptions.adaptToUpdatedData &&
                                        a.forEach(function (a) { l(a, "updatedData", this.updatedDataHandler) }, this), a[0].xAxis && l(a[0].xAxis, "foundExtremes", this.modifyBaseAxisExtremes))
                                }; e.prototype.init = function (a) {
                                    var c = a.options, f = c.navigator, e = f.enabled, g = c.scrollbar, b = g.enabled; c = e ? f.height : 0; var k = b ? g.height : 0; this.handles = []; this.shades = []; this.chart = a; this.setBaseSeries(); this.height = c; this.scrollbarHeight = k; this.scrollbarEnabled = b; this.navigatorEnabled = e; this.navigatorOptions = f; this.scrollbarOptions = g; this.outlineHeight = c + k; this.opposite =
                                        t(f.opposite, !(e || !a.inverted)); var h = this; e = h.baseSeries; g = a.xAxis.length; b = a.yAxis.length; var l = e && e[0] && e[0].xAxis || a.xAxis[0] || { options: {} }; a.isDirtyBox = !0; h.navigatorEnabled ? (h.xAxis = new p(a, d({ breaks: l.options.breaks, ordinal: l.options.ordinal }, f.xAxis, { id: "navigator-x-axis", yAxis: "navigator-y-axis", isX: !0, type: "datetime", index: g, isInternal: !0, offset: 0, keepOrdinalPadding: !0, startOnTick: !1, endOnTick: !1, minPadding: 0, maxPadding: 0, zoomEnabled: !1 }, a.inverted ? { offsets: [k, 0, -k, 0], width: c } : {
                                            offsets: [0,
                                                -k, 0, k], height: c
                                        })), h.yAxis = new p(a, d(f.yAxis, { id: "navigator-y-axis", alignTicks: !1, offset: 0, index: b, isInternal: !0, zoomEnabled: !1 }, a.inverted ? { width: c } : { height: c })), e || f.series.data ? h.updateNavigatorSeries(!1) : 0 === a.series.length && (h.unbindRedraw = H(a, "beforeRedraw", function () { 0 < a.series.length && !h.series && (h.setBaseSeries(), h.unbindRedraw()) })), h.reversedExtremes = a.inverted && !h.xAxis.reversed || !a.inverted && h.xAxis.reversed, h.renderElements(), h.addMouseEvents()) : (h.xAxis = {
                                            chart: a, navigatorAxis: { fake: !0 },
                                            translate: function (b, c) { var d = a.xAxis[0], g = d.getExtremes(), f = d.len - 2 * k, e = v("min", d.options.min, g.dataMin); d = v("max", d.options.max, g.dataMax) - e; return c ? b * d / f + e : f * (b - e) / d }, toPixels: function (a) { return this.translate(a) }, toValue: function (a) { return this.translate(a, !0) }
                                        }, h.xAxis.navigatorAxis.axis = h.xAxis, h.xAxis.navigatorAxis.toFixedRange = D.AdditionsClass.prototype.toFixedRange.bind(h.xAxis.navigatorAxis)); a.options.scrollbar.enabled && (a.scrollbar = h.scrollbar = new J(a.renderer, d(a.options.scrollbar, {
                                            margin: h.navigatorEnabled ?
                                                0 : 10, vertical: a.inverted
                                        }), a), H(h.scrollbar, "changed", function (b) { var c = h.size, d = c * this.to; c *= this.from; h.hasDragged = h.scrollbar.hasDragged; h.render(0, 0, c, d); (a.options.scrollbar.liveRedraw || "mousemove" !== b.DOMType && "touchmove" !== b.DOMType) && setTimeout(function () { h.onMouseUp(b) }) })); h.addBaseSeriesEvents(); h.addChartEvents()
                                }; e.prototype.getUnionExtremes = function (a) {
                                    var c = this.chart.xAxis[0], d = this.xAxis, f = d.options, g = c.options, b; a && null === c.dataMin || (b = {
                                        dataMin: t(f && f.min, v("min", g.min, c.dataMin,
                                            d.dataMin, d.min)), dataMax: t(f && f.max, v("max", g.max, c.dataMax, d.dataMax, d.max))
                                    }); return b
                                }; e.prototype.setBaseSeries = function (a, c) {
                                    var d = this.chart, f = this.baseSeries = []; a = a || d.options && d.options.navigator.baseSeries || (d.series.length ? C(d.series, function (a) { return !a.options.isInternal }).index : 0); (d.series || []).forEach(function (c, b) { c.options.isInternal || !c.options.showInNavigator && (b !== a && c.options.id !== a || !1 === c.options.showInNavigator) || f.push(c) }); this.xAxis && !this.xAxis.navigatorAxis.fake && this.updateNavigatorSeries(!0,
                                        c)
                                }; e.prototype.updateNavigatorSeries = function (a, f) {
                                    var e = this, k = e.chart, g = e.baseSeries, b, n, r = e.navigatorOptions.series, p, v = { enableMouseTracking: !1, index: null, linkedTo: null, group: "nav", padXAxis: !1, xAxis: "navigator-x-axis", yAxis: "navigator-y-axis", showInLegend: !1, stacking: void 0, isInternal: !0, states: { inactive: { opacity: 1 } } }, q = e.series = (e.series || []).filter(function (a) {
                                        var b = a.baseSeries; return 0 > g.indexOf(b) ? (b && (l(b, "updatedData", e.updatedDataHandler), delete b.navigatorSeries), a.chart && a.destroy(),
                                            !1) : !0
                                    }); g && g.length && g.forEach(function (a) {
                                        var c = a.navigatorSeries, l = u({ color: a.color, visible: a.visible }, h(r) ? m.navigator.series : r); c && !1 === e.navigatorOptions.adaptToUpdatedData || (v.name = "Navigator " + g.length, b = a.options || {}, p = b.navigatorOptions || {}, n = d(b, v, l, p), n.pointRange = t(l.pointRange, p.pointRange, m.plotOptions[n.type || "line"].pointRange), l = p.data || l.data, e.hasNavigatorData = e.hasNavigatorData || !!l, n.data = l || b.data && b.data.slice(0), c && c.options ? c.update(n, f) : (a.navigatorSeries = k.initSeries(n),
                                            a.navigatorSeries.baseSeries = a, q.push(a.navigatorSeries)))
                                    }); if (r.data && (!g || !g.length) || h(r)) e.hasNavigatorData = !1, r = c(r), r.forEach(function (a, b) { v.name = "Navigator " + (q.length + 1); n = d(m.navigator.series, { color: k.series[b] && !k.series[b].options.isInternal && k.series[b].color || k.options.colors[b] || k.options.colors[0] }, v, a); n.data = a.data; n.data && (e.hasNavigatorData = !0, q.push(k.initSeries(n))) }); a && this.addBaseSeriesEvents()
                                }; e.prototype.addBaseSeriesEvents = function () {
                                    var a = this, c = a.baseSeries || []; c[0] &&
                                        c[0].xAxis && H(c[0].xAxis, "foundExtremes", this.modifyBaseAxisExtremes); c.forEach(function (c) {
                                            H(c, "show", function () { this.navigatorSeries && this.navigatorSeries.setVisible(!0, !1) }); H(c, "hide", function () { this.navigatorSeries && this.navigatorSeries.setVisible(!1, !1) }); !1 !== this.navigatorOptions.adaptToUpdatedData && c.xAxis && H(c, "updatedData", this.updatedDataHandler); H(c, "remove", function () {
                                            this.navigatorSeries && (r(a.series, this.navigatorSeries), K(this.navigatorSeries.options) && this.navigatorSeries.remove(!1),
                                                delete this.navigatorSeries)
                                            })
                                        }, this)
                                }; e.prototype.getBaseSeriesMin = function (a) { return this.baseSeries.reduce(function (a, c) { return Math.min(a, c.xData ? c.xData[0] : a) }, a) }; e.prototype.modifyNavigatorAxisExtremes = function () { var a = this.xAxis, c; "undefined" !== typeof a.getExtremes && (!(c = this.getUnionExtremes(!0)) || c.dataMin === a.min && c.dataMax === a.max || (a.min = c.dataMin, a.max = c.dataMax)) }; e.prototype.modifyBaseAxisExtremes = function () {
                                    var a = this.chart.navigator, c = this.getExtremes(), d = c.dataMin, e = c.dataMax; c =
                                        c.max - c.min; var g = a.stickToMin, b = a.stickToMax, h = t(this.options.overscroll, 0), l = a.series && a.series[0], m = !!this.setExtremes; if (!this.eventArgs || "rangeSelectorButton" !== this.eventArgs.trigger) { if (g) { var r = d; var p = r + c } b && (p = e + h, g || (r = Math.max(d, p - c, a.getBaseSeriesMin(l && l.xData ? l.xData[0] : -Number.MAX_VALUE)))); m && (g || b) && f(r) && (this.min = this.userMin = r, this.max = this.userMax = p) } a.stickToMin = a.stickToMax = null
                                }; e.prototype.updatedDataHandler = function () {
                                    var a = this.chart.navigator, c = this.navigatorSeries, d =
                                        a.getBaseSeriesMin(this.xData[0]); a.stickToMax = a.reversedExtremes ? 0 === Math.round(a.zoomedMin) : Math.round(a.zoomedMax) >= Math.round(a.size); a.stickToMin = f(this.xAxis.min) && this.xAxis.min <= d && (!this.chart.fixedRange || !a.stickToMax); c && !a.hasNavigatorData && (c.options.pointStart = this.xData[0], c.setData(this.options.data, !1, null, !1))
                                }; e.prototype.addChartEvents = function () {
                                this.eventsToUnbind || (this.eventsToUnbind = []); this.eventsToUnbind.push(H(this.chart, "redraw", function () {
                                    var a = this.navigator, c = a && (a.baseSeries &&
                                        a.baseSeries[0] && a.baseSeries[0].xAxis || this.xAxis[0]); c && a.render(c.min, c.max)
                                }), H(this.chart, "getMargins", function () { var a = this.navigator, c = a.opposite ? "plotTop" : "marginBottom"; this.inverted && (c = a.opposite ? "marginRight" : "plotLeft"); this[c] = (this[c] || 0) + (a.navigatorEnabled || !this.inverted ? a.outlineHeight : 0) + a.navigatorOptions.margin }))
                                }; e.prototype.destroy = function () {
                                    this.removeEvents(); this.xAxis && (r(this.chart.xAxis, this.xAxis), r(this.chart.axes, this.xAxis)); this.yAxis && (r(this.chart.yAxis, this.yAxis),
                                        r(this.chart.axes, this.yAxis)); (this.series || []).forEach(function (a) { a.destroy && a.destroy() }); "series xAxis yAxis shades outline scrollbarTrack scrollbarRifles scrollbarGroup scrollbar navigatorGroup rendered".split(" ").forEach(function (a) { this[a] && this[a].destroy && this[a].destroy(); this[a] = null }, this);[this.handles].forEach(function (a) { w(a) }, this)
                                }; return e
                        }(); B.Navigator || (B.Navigator = E, D.compose(p), H(e, "beforeShowResetZoom", function () {
                            var a = this.options, c = a.navigator, d = a.rangeSelector; if ((c &&
                                c.enabled || d && d.enabled) && (!x && "x" === a.chart.zoomType || x && "x" === a.chart.pinchType)) return !1
                        }), H(e, "beforeRender", function () { var a = this.options; if (a.navigator.enabled || a.scrollbar.enabled) this.scroller = this.navigator = new E(this) }), H(e, "afterSetChartSize", function () {
                            var a = this.legend, c = this.navigator; if (c) {
                                var d = a && a.options; var f = c.xAxis; var e = c.yAxis; var g = c.scrollbarHeight; this.inverted ? (c.left = c.opposite ? this.chartWidth - g - c.height : this.spacing[3] + g, c.top = this.plotTop + g) : (c.left = this.plotLeft + g,
                                    c.top = c.navigatorOptions.top || this.chartHeight - c.height - g - this.spacing[2] - (this.rangeSelector && this.extraBottomMargin ? this.rangeSelector.getHeight() : 0) - (d && "bottom" === d.verticalAlign && "proximate" !== d.layout && d.enabled && !d.floating ? a.legendHeight + t(d.margin, 10) : 0) - (this.titleOffset ? this.titleOffset[2] : 0)); f && e && (this.inverted ? f.options.left = e.options.left = c.left : f.options.top = e.options.top = c.top, f.setAxisSize(), e.setAxisSize())
                            }
                        }), H(e, "update", function (a) {
                            var c = a.options.navigator || {}, f = a.options.scrollbar ||
                                {}; this.navigator || this.scroller || !c.enabled && !f.enabled || (d(!0, this.options.navigator, c), d(!0, this.options.scrollbar, f), delete a.options.navigator, delete a.options.scrollbar)
                        }), H(e, "afterUpdate", function (a) { this.navigator || this.scroller || !this.options.navigator.enabled && !this.options.scrollbar.enabled || (this.scroller = this.navigator = new E(this), t(a.redraw, !0) && this.redraw(a.animation)) }), H(e, "afterAddSeries", function () { this.navigator && this.navigator.setBaseSeries(null, !1) }), H(z, "afterUpdate", function () {
                            this.chart.navigator &&
                            !this.options.isInternal && this.chart.navigator.setBaseSeries(null, !1)
                        }), e.prototype.callbacks.push(function (a) { var c = a.navigator; c && a.xAxis[0] && (a = a.xAxis[0].getExtremes(), c.render(a.min, a.max)) })); B.Navigator = E; return B.Navigator
                    }); O(q, "parts/OrdinalAxis.js", [q["parts/Axis.js"], q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e, q) {
                        var B = q.addEvent, D = q.css, z = q.defined, J = q.pick, G = q.timeUnits; q = e.Chart; var m = e.Series, H; (function (m) {
                            var p = function () {
                                function m(e) { this.index = {}; this.axis = e } m.prototype.beforeSetTickPositions =
                                    function () {
                                        var e = this.axis, m = e.ordinal, p = [], q, h = !1, f = e.getExtremes(), d = f.min, t = f.max, l, c = e.isXAxis && !!e.options.breaks; f = e.options.ordinal; var a = Number.MAX_VALUE, x = e.chart.options.chart.ignoreHiddenSeries, v; if (f || c) {
                                            e.series.forEach(function (d, f) {
                                                q = []; if (!(x && !1 === d.visible || !1 === d.takeOrdinalPosition && !c) && (p = p.concat(d.processedXData), E = p.length, p.sort(function (a, c) { return a - c }), a = Math.min(a, J(d.closestPointRange, a)), E)) {
                                                    for (f = 0; f < E - 1;)p[f] !== p[f + 1] && q.push(p[f + 1]), f++; q[0] !== p[0] && q.unshift(p[0]);
                                                    p = q
                                                } d.isSeriesBoosting && (v = !0)
                                            }); v && (p.length = 0); var E = p.length; if (2 < E) { var F = p[1] - p[0]; for (l = E - 1; l-- && !h;)p[l + 1] - p[l] !== F && (h = !0); !e.options.keepOrdinalPadding && (p[0] - d > F || t - p[p.length - 1] > F) && (h = !0) } else e.options.overscroll && (2 === E ? a = p[1] - p[0] : 1 === E ? (a = e.options.overscroll, p = [p[0], p[0] + a]) : a = m.overscrollPointsRange); h ? (e.options.overscroll && (m.overscrollPointsRange = a, p = p.concat(m.getOverscrollPositions())), m.positions = p, F = e.ordinal2lin(Math.max(d, p[0]), !0), l = Math.max(e.ordinal2lin(Math.min(t, p[p.length -
                                                1]), !0), 1), m.slope = t = (t - d) / (l - F), m.offset = d - F * t) : (m.overscrollPointsRange = J(e.closestPointRange, m.overscrollPointsRange), m.positions = e.ordinal.slope = m.offset = void 0)
                                        } e.isOrdinal = f && h; m.groupIntervalFactor = null
                                    }; m.prototype.getExtendedPositions = function () {
                                        var m = this, r = m.axis, p = r.constructor.prototype, q = r.chart, h = r.series[0].currentDataGrouping, f = m.index, d = h ? h.count + h.unitName : "raw", t = r.options.overscroll, l = r.getExtremes(), c; f || (f = m.index = {}); if (!f[d]) {
                                            var a = {
                                                series: [], chart: q, getExtremes: function () {
                                                    return {
                                                        min: l.dataMin,
                                                        max: l.dataMax + t
                                                    }
                                                }, options: { ordinal: !0 }, ordinal: {}, ordinal2lin: p.ordinal2lin, val2lin: p.val2lin
                                            }; a.ordinal.axis = a; r.series.forEach(function (d) { c = { xAxis: a, xData: d.xData.slice(), chart: q, destroyGroupedData: e.noop, getProcessedData: e.Series.prototype.getProcessedData }; c.xData = c.xData.concat(m.getOverscrollPositions()); c.options = { dataGrouping: h ? { enabled: !0, forced: !0, approximation: "open", units: [[h.unitName, [h.count]]] } : { enabled: !1 } }; d.processData.apply(c); a.series.push(c) }); r.ordinal.beforeSetTickPositions.apply({ axis: a });
                                            f[d] = a.ordinal.positions
                                        } return f[d]
                                    }; m.prototype.getGroupIntervalFactor = function (e, m, p) { p = p.processedXData; var r = p.length, h = []; var f = this.groupIntervalFactor; if (!f) { for (f = 0; f < r - 1; f++)h[f] = p[f + 1] - p[f]; h.sort(function (d, f) { return d - f }); h = h[Math.floor(r / 2)]; e = Math.max(e, p[0]); m = Math.min(m, p[r - 1]); this.groupIntervalFactor = f = r * h / (m - e) } return f }; m.prototype.getOverscrollPositions = function () {
                                        var e = this.axis, m = e.options.overscroll, p = this.overscrollPointsRange, q = [], h = e.dataMax; if (z(p)) for (q.push(h); h <= e.dataMax +
                                            m;)h += p, q.push(h); return q
                                    }; m.prototype.postProcessTickInterval = function (e) { var m = this.axis, p = this.slope; return p ? m.options.breaks ? m.closestPointRange || e : e / (p / m.closestPointRange) : e }; return m
                            }(); m.Composition = p; m.compose = function (e, p, r) {
                                e.keepProps.push("ordinal"); var q = e.prototype; e.prototype.getTimeTicks = function (e, h, f, d, m, l, c) {
                                void 0 === m && (m = []); void 0 === l && (l = 0); var a = 0, t, r, p = {}, q = [], k = -Number.MAX_VALUE, u = this.options.tickPixelInterval, w = this.chart.time, C = []; if (!this.options.ordinal && !this.options.breaks ||
                                    !m || 3 > m.length || "undefined" === typeof h) return w.getTimeTicks.apply(w, arguments); var g = m.length; for (t = 0; t < g; t++) { var b = t && m[t - 1] > f; m[t] < h && (a = t); if (t === g - 1 || m[t + 1] - m[t] > 5 * l || b) { if (m[t] > k) { for (r = w.getTimeTicks(e, m[a], m[t], d); r.length && r[0] <= k;)r.shift(); r.length && (k = r[r.length - 1]); C.push(q.length); q = q.concat(r) } a = t + 1 } if (b) break } r = r.info; if (c && r.unitRange <= G.hour) {
                                        t = q.length - 1; for (a = 1; a < t; a++)if (w.dateFormat("%d", q[a]) !== w.dateFormat("%d", q[a - 1])) { p[q[a]] = "day"; var n = !0 } n && (p[q[0]] = "day"); r.higherRanks =
                                            p
                                    } r.segmentStarts = C; q.info = r; if (c && z(u)) { a = C = q.length; n = []; var A; for (w = []; a--;)t = this.translate(q[a]), A && (w[a] = A - t), n[a] = A = t; w.sort(); w = w[Math.floor(w.length / 2)]; w < .6 * u && (w = null); a = q[C - 1] > f ? C - 1 : C; for (A = void 0; a--;)t = n[a], C = Math.abs(A - t), A && C < .8 * u && (null === w || C < .8 * w) ? (p[q[a]] && !p[q[a + 1]] ? (C = a + 1, A = t) : C = a, q.splice(C, 1)) : A = t } return q
                                }; q.lin2val = function (e, h) {
                                    var f = this.ordinal, d = f.positions; if (d) {
                                        var m = f.slope, l = f.offset; f = d.length - 1; if (h) if (0 > e) e = d[0]; else if (e > f) e = d[f]; else {
                                            f = Math.floor(e); var c = e -
                                                f
                                        } else for (; f--;)if (h = m * f + l, e >= h) { m = m * (f + 1) + l; c = (e - h) / (m - h); break } return "undefined" !== typeof c && "undefined" !== typeof d[f] ? d[f] + (c ? c * (d[f + 1] - d[f]) : 0) : e
                                    } return e
                                }; q.val2lin = function (e, h) { var f = this.ordinal, d = f.positions; if (d) { var m = d.length, l; for (l = m; l--;)if (d[l] === e) { var c = l; break } for (l = m - 1; l--;)if (e > d[l] || 0 === l) { e = (e - d[l]) / (d[l + 1] - d[l]); c = l + e; break } h = h ? c : f.slope * (c || 0) + f.offset } else h = e; return h }; q.ordinal2lin = q.val2lin; B(e, "afterInit", function () { this.ordinal || (this.ordinal = new m.Composition(this)) });
                                B(e, "foundExtremes", function () { this.isXAxis && z(this.options.overscroll) && this.max === this.dataMax && (!this.chart.mouseIsDown || this.isInternal) && (!this.eventArgs || this.eventArgs && "navigator" !== this.eventArgs.trigger) && (this.max += this.options.overscroll, !this.isInternal && z(this.userMin) && (this.min += this.options.overscroll)) }); B(e, "afterSetScale", function () { this.horiz && !this.isDirty && (this.isDirty = this.isOrdinal && this.chart.navigator && !this.chart.navigator.adaptToUpdatedData) }); B(e, "initialAxisTranslation",
                                    function () { this.ordinal && (this.ordinal.beforeSetTickPositions(), this.tickInterval = this.ordinal.postProcessTickInterval(this.tickInterval)) }); B(p, "pan", function (e) {
                                        var h = this.xAxis[0], f = h.options.overscroll, d = e.originalEvent.chartX, m = this.options.chart && this.options.chart.panning, l = !1; if (m && "y" !== m.type && h.options.ordinal && h.series.length) {
                                            var c = this.mouseDownX, a = h.getExtremes(), r = a.dataMax, p = a.min, q = a.max, u = this.hoverPoints, k = h.closestPointRange || h.ordinal && h.ordinal.overscrollPointsRange; c = (c - d) /
                                                (h.translationSlope * (h.ordinal.slope || k)); var y = { ordinal: { positions: h.ordinal.getExtendedPositions() } }; k = h.lin2val; var w = h.val2lin; if (!y.ordinal.positions) l = !0; else if (1 < Math.abs(c)) {
                                                    u && u.forEach(function (a) { a.setState() }); if (0 > c) { u = y; var C = h.ordinal.positions ? h : y } else u = h.ordinal.positions ? h : y, C = y; y = C.ordinal.positions; r > y[y.length - 1] && y.push(r); this.fixedRange = q - p; c = h.navigatorAxis.toFixedRange(null, null, k.apply(u, [w.apply(u, [p, !0]) + c, !0]), k.apply(C, [w.apply(C, [q, !0]) + c, !0])); c.min >= Math.min(a.dataMin,
                                                        p) && c.max <= Math.max(r, q) + f && h.setExtremes(c.min, c.max, !0, !1, { trigger: "pan" }); this.mouseDownX = d; D(this.container, { cursor: "move" })
                                                }
                                        } else l = !0; l || m && /y/.test(m.type) ? f && (h.max = h.dataMax + f) : e.preventDefault()
                                    }); B(r, "updatedData", function () { var e = this.xAxis; e && e.options.ordinal && delete e.ordinal.index })
                            }
                        })(H || (H = {})); H.compose(p, q, m); return H
                    }); O(q, "modules/broken-axis.src.js", [q["parts/Axis.js"], q["parts/Globals.js"], q["parts/Utilities.js"], q["parts/Stacking.js"]], function (p, e, q, B) {
                        var D = q.addEvent, z =
                            q.find, J = q.fireEvent, G = q.isArray, m = q.isNumber, H = q.pick, M = e.Series, A = function () {
                                function e(e) { this.hasBreaks = !1; this.axis = e } e.isInBreak = function (e, m) { var r = e.repeat || Infinity, p = e.from, h = e.to - e.from; m = m >= p ? (m - p) % r : r - (p - m) % r; return e.inclusive ? m <= h : m < h && 0 !== m }; e.lin2Val = function (m) { var r = this.brokenAxis; r = r && r.breakArray; if (!r) return m; var p; for (p = 0; p < r.length; p++) { var q = r[p]; if (q.from >= m) break; else q.to < m ? m += q.len : e.isInBreak(q, m) && (m += q.len) } return m }; e.val2Lin = function (m) {
                                    var r = this.brokenAxis; r =
                                        r && r.breakArray; if (!r) return m; var p = m, q; for (q = 0; q < r.length; q++) { var h = r[q]; if (h.to <= m) p -= h.len; else if (h.from >= m) break; else if (e.isInBreak(h, m)) { p -= m - h.from; break } } return p
                                }; e.prototype.findBreakAt = function (e, m) { return z(m, function (m) { return m.from < e && e < m.to }) }; e.prototype.isInAnyBreak = function (m, r) { var p = this.axis, q = p.options.breaks, h = q && q.length, f; if (h) { for (; h--;)if (e.isInBreak(q[h], m)) { var d = !0; f || (f = H(q[h].showPoints, !p.isXAxis)) } var t = d && r ? d && !f : d } return t }; e.prototype.setBreaks = function (m,
                                    r) {
                                        var q = this, w = q.axis, h = G(m) && !!m.length; w.isDirty = q.hasBreaks !== h; q.hasBreaks = h; w.options.breaks = w.userOptions.breaks = m; w.forceRedraw = !0; w.series.forEach(function (f) { f.isDirty = !0 }); h || w.val2lin !== e.val2Lin || (delete w.val2lin, delete w.lin2val); h && (w.userOptions.ordinal = !1, w.lin2val = e.lin2Val, w.val2lin = e.val2Lin, w.setExtremes = function (f, d, e, h, c) {
                                            if (q.hasBreaks) { for (var a, l = this.options.breaks; a = q.findBreakAt(f, l);)f = a.to; for (; a = q.findBreakAt(d, l);)d = a.from; d < f && (d = f) } p.prototype.setExtremes.call(this,
                                                f, d, e, h, c)
                                        }, w.setAxisTranslation = function (f) {
                                            p.prototype.setAxisTranslation.call(this, f); q.unitLength = null; if (q.hasBreaks) {
                                                f = w.options.breaks || []; var d = [], h = [], l = 0, c, a = w.userMin || w.min, m = w.userMax || w.max, r = H(w.pointRangePadding, 0), u; f.forEach(function (d) { c = d.repeat || Infinity; e.isInBreak(d, a) && (a += d.to % c - a % c); e.isInBreak(d, m) && (m -= m % c - d.from % c) }); f.forEach(function (f) {
                                                    k = f.from; for (c = f.repeat || Infinity; k - c > a;)k -= c; for (; k < a;)k += c; for (u = k; u < m; u += c)d.push({ value: u, move: "in" }), d.push({
                                                        value: u + (f.to - f.from),
                                                        move: "out", size: f.breakSize
                                                    })
                                                }); d.sort(function (a, c) { return a.value === c.value ? ("in" === a.move ? 0 : 1) - ("in" === c.move ? 0 : 1) : a.value - c.value }); var F = 0; var k = a; d.forEach(function (a) { F += "in" === a.move ? 1 : -1; 1 === F && "in" === a.move && (k = a.value); 0 === F && (h.push({ from: k, to: a.value, len: a.value - k - (a.size || 0) }), l += a.value - k - (a.size || 0)) }); w.breakArray = q.breakArray = h; q.unitLength = m - a - l + r; J(w, "afterBreaks"); w.staticScale ? w.transA = w.staticScale : q.unitLength && (w.transA *= (m - w.min + r) / q.unitLength); r && (w.minPixelPadding = w.transA *
                                                    w.minPointOffset); w.min = a; w.max = m
                                            }
                                        }); H(r, !0) && w.chart.redraw()
                                }; return e
                            }(); e = function () {
                                function e() { } e.compose = function (e, r) {
                                    e.keepProps.push("brokenAxis"); var p = M.prototype; p.drawBreaks = function (e, h) {
                                        var f = this, d = f.points, t, l, c, a; if (e && e.brokenAxis && e.brokenAxis.hasBreaks) {
                                            var r = e.brokenAxis; h.forEach(function (h) {
                                                t = r && r.breakArray || []; l = e.isXAxis ? e.min : H(f.options.threshold, e.min); d.forEach(function (d) {
                                                    a = H(d["stack" + h.toUpperCase()], d[h]); t.forEach(function (f) {
                                                        if (m(l) && m(a)) {
                                                            c = !1; if (l < f.from &&
                                                                a > f.to || l > f.from && a < f.from) c = "pointBreak"; else if (l < f.from && a > f.from && a < f.to || l > f.from && a > f.to && a < f.from) c = "pointInBreak"; c && J(e, c, { point: d, brk: f })
                                                        }
                                                    })
                                                })
                                            })
                                        }
                                    }; p.gappedPath = function () {
                                        var e = this.currentDataGrouping, h = e && e.gapSize; e = this.options.gapSize; var f = this.points.slice(), d = f.length - 1, m = this.yAxis, l; if (e && 0 < d) for ("value" !== this.options.gapUnit && (e *= this.basePointRange), h && h > e && h >= this.basePointRange && (e = h), l = void 0; d--;)l && !1 !== l.visible || (l = f[d + 1]), h = f[d], !1 !== l.visible && !1 !== h.visible && (l.x -
                                            h.x > e && (l = (h.x + l.x) / 2, f.splice(d + 1, 0, { isNull: !0, x: l }), m.stacking && this.options.stacking && (l = m.stacking.stacks[this.stackKey][l] = new B(m, m.options.stackLabels, !1, l, this.stack), l.total = 0)), l = h); return this.getGraphPath(f)
                                    }; D(e, "init", function () { this.brokenAxis || (this.brokenAxis = new A(this)) }); D(e, "afterInit", function () { "undefined" !== typeof this.brokenAxis && this.brokenAxis.setBreaks(this.options.breaks, !1) }); D(e, "afterSetTickPositions", function () {
                                        var e = this.brokenAxis; if (e && e.hasBreaks) {
                                            var h = this.tickPositions,
                                            f = this.tickPositions.info, d = [], m; for (m = 0; m < h.length; m++)e.isInAnyBreak(h[m]) || d.push(h[m]); this.tickPositions = d; this.tickPositions.info = f
                                        }
                                    }); D(e, "afterSetOptions", function () { this.brokenAxis && this.brokenAxis.hasBreaks && (this.options.ordinal = !1) }); D(r, "afterGeneratePoints", function () {
                                        var e = this.options.connectNulls, h = this.points, f = this.xAxis, d = this.yAxis; if (this.isDirty) for (var m = h.length; m--;) {
                                            var l = h[m], c = !(null === l.y && !1 === e) && (f && f.brokenAxis && f.brokenAxis.isInAnyBreak(l.x, !0) || d && d.brokenAxis &&
                                                d.brokenAxis.isInAnyBreak(l.y, !0)); l.visible = c ? !1 : !1 !== l.options.visible
                                        }
                                    }); D(r, "afterRender", function () { this.drawBreaks(this.xAxis, ["x"]); this.drawBreaks(this.yAxis, H(this.pointArrayMap, ["y"])) })
                                }; return e
                            }(); e.compose(p, M); return e
                    }); O(q, "masters/modules/broken-axis.src.js", [], function () { }); O(q, "parts/DataGrouping.js", [q["parts/DateTimeAxis.js"], q["parts/Globals.js"], q["parts/Options.js"], q["parts/Point.js"], q["parts/Tooltip.js"], q["parts/Utilities.js"]], function (p, e, q, B, D, z) {
                        ""; var J = z.addEvent,
                            G = z.arrayMax, m = z.arrayMin, H = z.correctFloat, M = z.defined, A = z.error, K = z.extend, w = z.format, r = z.isNumber, u = z.merge, C = z.pick, h = e.Axis; z = e.Series; var f = e.approximations = {
                                sum: function (a) { var c = a.length; if (!c && a.hasNulls) var d = null; else if (c) for (d = 0; c--;)d += a[c]; return d }, average: function (a) { var c = a.length; a = f.sum(a); r(a) && c && (a = H(a / c)); return a }, averages: function () { var a = [];[].forEach.call(arguments, function (c) { a.push(f.average(c)) }); return "undefined" === typeof a[0] ? void 0 : a }, open: function (a) {
                                    return a.length ?
                                        a[0] : a.hasNulls ? null : void 0
                                }, high: function (a) { return a.length ? G(a) : a.hasNulls ? null : void 0 }, low: function (a) { return a.length ? m(a) : a.hasNulls ? null : void 0 }, close: function (a) { return a.length ? a[a.length - 1] : a.hasNulls ? null : void 0 }, ohlc: function (a, c, d, e) { a = f.open(a); c = f.high(c); d = f.low(d); e = f.close(e); if (r(a) || r(c) || r(d) || r(e)) return [a, c, d, e] }, range: function (a, c) { a = f.low(a); c = f.high(c); if (r(a) || r(c)) return [a, c]; if (null === a && null === c) return null }
                            }, d = function (a, c, d, e) {
                                var k = this, g = k.data, b = k.options && k.options.data,
                                h = [], l = [], m = [], p = a.length, t = !!c, q = [], v = k.pointArrayMap, x = v && v.length, y = ["x"].concat(v || ["y"]), w = 0, F = 0, E; e = "function" === typeof e ? e : f[e] ? f[e] : f[k.getDGApproximation && k.getDGApproximation() || "average"]; x ? v.forEach(function () { q.push([]) }) : q.push([]); var A = x || 1; for (E = 0; E <= p && !(a[E] >= d[0]); E++); for (E; E <= p; E++) {
                                    for (; "undefined" !== typeof d[w + 1] && a[E] >= d[w + 1] || E === p;) {
                                        var z = d[w]; k.dataGroupInfo = { start: k.cropStart + F, length: q[0].length }; var B = e.apply(k, q); k.pointClass && !M(k.dataGroupInfo.options) && (k.dataGroupInfo.options =
                                            u(k.pointClass.prototype.optionsToObject.call({ series: k }, k.options.data[k.cropStart + F])), y.forEach(function (a) { delete k.dataGroupInfo.options[a] })); "undefined" !== typeof B && (h.push(z), l.push(B), m.push(k.dataGroupInfo)); F = E; for (z = 0; z < A; z++)q[z].length = 0, q[z].hasNulls = !1; w += 1; if (E === p) break
                                    } if (E === p) break; if (v) for (z = k.cropStart + E, B = g && g[z] || k.pointClass.prototype.applyOptions.apply({ series: k }, [b[z]]), z = 0; z < x; z++) { var C = B[v[z]]; r(C) ? q[z].push(C) : null === C && (q[z].hasNulls = !0) } else z = t ? c[E] : null, r(z) ? q[0].push(z) :
                                        null === z && (q[0].hasNulls = !0)
                                } return { groupedXData: h, groupedYData: l, groupMap: m }
                            }, t = { approximations: f, groupData: d }, l = z.prototype, c = l.processData, a = l.generatePoints, x = {
                                groupPixelWidth: 2, dateTimeLabelFormats: {
                                    millisecond: ["%A, %b %e, %H:%M:%S.%L", "%A, %b %e, %H:%M:%S.%L", "-%H:%M:%S.%L"], second: ["%A, %b %e, %H:%M:%S", "%A, %b %e, %H:%M:%S", "-%H:%M:%S"], minute: ["%A, %b %e, %H:%M", "%A, %b %e, %H:%M", "-%H:%M"], hour: ["%A, %b %e, %H:%M", "%A, %b %e, %H:%M", "-%H:%M"], day: ["%A, %b %e, %Y", "%A, %b %e", "-%A, %b %e, %Y"],
                                    week: ["Week from %A, %b %e, %Y", "%A, %b %e", "-%A, %b %e, %Y"], month: ["%B %Y", "%B", "-%B %Y"], year: ["%Y", "%Y", "-%Y"]
                                }
                            }, v = { line: {}, spline: {}, area: {}, areaspline: {}, arearange: {}, column: { groupPixelWidth: 10 }, columnrange: { groupPixelWidth: 10 }, candlestick: { groupPixelWidth: 10 }, ohlc: { groupPixelWidth: 5 } }, E = e.defaultDataGroupingUnits = [["millisecond", [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], ["second", [1, 2, 5, 10, 15, 30]], ["minute", [1, 2, 5, 10, 15, 30]], ["hour", [1, 2, 3, 4, 6, 8, 12]], ["day", [1]], ["week", [1]], ["month", [1, 3, 6]], ["year",
                                null]]; l.getDGApproximation = function () { return this.is("arearange") ? "range" : this.is("ohlc") ? "ohlc" : this.is("column") ? "sum" : "average" }; l.groupData = d; l.processData = function () {
                                    var a = this.chart, d = this.options.dataGrouping, f = !1 !== this.allowDG && d && C(d.enabled, a.options.isStock), e = this.visible || !a.options.chart.ignoreHiddenSeries, h, g = this.currentDataGrouping, b = !1; this.forceCrop = f; this.groupPixelWidth = null; this.hasProcessed = !0; f && !this.requireSorting && (this.requireSorting = b = !0); f = !1 === c.apply(this, arguments) ||
                                        !f; b && (this.requireSorting = !1); if (!f) {
                                            this.destroyGroupedData(); f = d.groupAll ? this.xData : this.processedXData; var m = d.groupAll ? this.yData : this.processedYData, r = a.plotSizeX; a = this.xAxis; var t = a.options.ordinal, q = this.groupPixelWidth = a.getGroupPixelWidth && a.getGroupPixelWidth(); if (q) {
                                            this.isDirty = h = !0; this.points = null; b = a.getExtremes(); var v = b.min; b = b.max; t = t && a.ordinal && a.ordinal.getGroupIntervalFactor(v, b, this) || 1; q = q * (b - v) / r * t; r = a.getTimeTicks(p.AdditionsClass.prototype.normalizeTimeTickInterval(q,
                                                d.units || E), Math.min(v, f[0]), Math.max(b, f[f.length - 1]), a.options.startOfWeek, f, this.closestPointRange); m = l.groupData.apply(this, [f, m, r, d.approximation]); f = m.groupedXData; t = m.groupedYData; var u = 0; if (d.smoothed && f.length) { var x = f.length - 1; for (f[x] = Math.min(f[x], b); x-- && 0 < x;)f[x] += q / 2; f[0] = Math.max(f[0], v) } for (x = 1; x < r.length; x++)r.info.segmentStarts && -1 !== r.info.segmentStarts.indexOf(x) || (u = Math.max(r[x] - r[x - 1], u)); v = r.info; v.gapSize = u; this.closestPointRange = r.info.totalRange; this.groupMap = m.groupMap;
                                                if (M(f[0]) && f[0] < a.min && e) { if (!M(a.options.min) && a.min <= a.dataMin || a.min === a.dataMin) a.min = Math.min(f[0], a.min); a.dataMin = Math.min(f[0], a.dataMin) } d.groupAll && (d = this.cropData(f, t, a.min, a.max, 1), f = d.xData, t = d.yData); this.processedXData = f; this.processedYData = t
                                            } else this.groupMap = null; this.hasGroupedData = h; this.currentDataGrouping = v; this.preventGraphAnimation = (g && g.totalRange) !== (v && v.totalRange)
                                        }
                                }; l.destroyGroupedData = function () {
                                this.groupedData && (this.groupedData.forEach(function (a, c) {
                                    a && (this.groupedData[c] =
                                        a.destroy ? a.destroy() : null)
                                }, this), this.groupedData.length = 0)
                                }; l.generatePoints = function () { a.apply(this); this.destroyGroupedData(); this.groupedData = this.hasGroupedData ? this.points : null }; J(B, "update", function () { if (this.dataGroup) return A(24, !1, this.series.chart), !1 }); J(D, "headerFormatter", function (a) {
                                    var c = this.chart, d = c.time, f = a.labelConfig, e = f.series, g = e.tooltipOptions, b = e.options.dataGrouping, h = g.xDateFormat, l = e.xAxis, m = g[(a.isFooter ? "footer" : "header") + "Format"]; if (l && "datetime" === l.options.type &&
                                        b && r(f.key)) { var p = e.currentDataGrouping; b = b.dateTimeLabelFormats || x.dateTimeLabelFormats; if (p) if (g = b[p.unitName], 1 === p.count) h = g[0]; else { h = g[1]; var t = g[2] } else !h && b && (h = this.getXDateFormat(f, g, l)); h = d.dateFormat(h, f.key); t && (h += d.dateFormat(t, f.key + p.totalRange - 1)); e.chart.styledMode && (m = this.styledModeFormat(m)); a.text = w(m, { point: K(f.point, { key: h }), series: e }, c); a.preventDefault() }
                                }); J(z, "destroy", l.destroyGroupedData); J(z, "afterSetOptions", function (a) {
                                    a = a.options; var c = this.type, d = this.chart.options.plotOptions,
                                        f = q.defaultOptions.plotOptions[c].dataGrouping, e = this.useCommonDataGrouping && x; if (v[c] || e) f || (f = u(x, v[c])), a.dataGrouping = u(e, f, d.series && d.series.dataGrouping, d[c].dataGrouping, this.userOptions.dataGrouping)
                                }); J(h, "afterSetScale", function () { this.series.forEach(function (a) { a.hasProcessed = !1 }) }); h.prototype.getGroupPixelWidth = function () {
                                    var a = this.series, c = a.length, d, f = 0, e = !1, g; for (d = c; d--;)(g = a[d].options.dataGrouping) && (f = Math.max(f, C(g.groupPixelWidth, x.groupPixelWidth))); for (d = c; d--;)(g = a[d].options.dataGrouping) &&
                                        a[d].hasProcessed && (c = (a[d].processedXData || a[d].data).length, a[d].groupPixelWidth || c > this.chart.plotSizeX / f || c && g.forced) && (e = !0); return e ? f : 0
                                }; h.prototype.setDataGrouping = function (a, c) { var d; c = C(c, !0); a || (a = { forced: !1, units: null }); if (this instanceof h) for (d = this.series.length; d--;)this.series[d].update({ dataGrouping: a }, !1); else this.chart.options.series.forEach(function (c) { c.dataGrouping = a }, !1); this.ordinal && (this.ordinal.slope = void 0); c && this.chart.redraw() }; e.dataGrouping = t; ""; return t
                    }); O(q,
                        "parts/OHLCSeries.js", [q["parts/Globals.js"], q["parts/Point.js"], q["parts/Utilities.js"]], function (p, e, q) {
                            q = q.seriesType; var B = p.seriesTypes; q("ohlc", "column", { lineWidth: 1, tooltip: { pointFormat: '<span style="color:{point.color}">\u25cf</span> <b> {series.name}</b><br/>Open: {point.open}<br/>High: {point.high}<br/>Low: {point.low}<br/>Close: {point.close}<br/>' }, threshold: null, states: { hover: { lineWidth: 3 } }, stickyTracking: !0 }, {
                                directTouch: !1, pointArrayMap: ["open", "high", "low", "close"], toYData: function (e) {
                                    return [e.open,
                                    e.high, e.low, e.close]
                                }, pointValKey: "close", pointAttrToOptions: { stroke: "color", "stroke-width": "lineWidth" }, init: function () { B.column.prototype.init.apply(this, arguments); this.options.stacking = void 0 }, pointAttribs: function (e, p) { p = B.column.prototype.pointAttribs.call(this, e, p); var q = this.options; delete p.fill; !e.options.color && q.upColor && e.open < e.close && (p.stroke = q.upColor); return p }, translate: function () {
                                    var e = this, p = e.yAxis, q = !!e.modifyValue, G = ["plotOpen", "plotHigh", "plotLow", "plotClose", "yBottom"];
                                    B.column.prototype.translate.apply(e); e.points.forEach(function (m) { [m.open, m.high, m.low, m.close, m.low].forEach(function (z, B) { null !== z && (q && (z = e.modifyValue(z)), m[G[B]] = p.toPixels(z, !0)) }); m.tooltipPos[1] = m.plotHigh + p.pos - e.chart.plotTop })
                                }, drawPoints: function () {
                                    var e = this, p = e.chart, q = function (e, m, p) { var q = e[0]; e = e[1]; "number" === typeof q[2] && (q[2] = Math.max(p + m, q[2])); "number" === typeof e[2] && (e[2] = Math.min(p - m, e[2])) }; e.points.forEach(function (z) {
                                        var m = z.graphic, B = !m; if ("undefined" !== typeof z.plotY) {
                                            m ||
                                            (z.graphic = m = p.renderer.path().add(e.group)); p.styledMode || m.attr(e.pointAttribs(z, z.selected && "select")); var G = m.strokeWidth(); var A = G % 2 / 2; var D = Math.round(z.plotX) - A; var w = Math.round(z.shapeArgs.width / 2); var r = [["M", D, Math.round(z.yBottom)], ["L", D, Math.round(z.plotHigh)]]; if (null !== z.open) { var u = Math.round(z.plotOpen) + A; r.push(["M", D, u], ["L", D - w, u]); q(r, G / 2, u) } null !== z.close && (u = Math.round(z.plotClose) + A, r.push(["M", D, u], ["L", D + w, u]), q(r, G / 2, u)); m[B ? "attr" : "animate"]({ d: r }).addClass(z.getClassName(),
                                                !0)
                                        }
                                    })
                                }, animate: null
                            }, { getClassName: function () { return e.prototype.getClassName.call(this) + (this.open < this.close ? " highcharts-point-up" : " highcharts-point-down") } }); ""
                        }); O(q, "parts/CandlestickSeries.js", [q["parts/Globals.js"], q["parts/Options.js"], q["parts/Utilities.js"]], function (p, e, q) {
                            e = e.defaultOptions; var B = q.merge; q = q.seriesType; var D = p.seriesTypes; q("candlestick", "ohlc", B(e.plotOptions.column, {
                                states: { hover: { lineWidth: 2 } }, tooltip: e.plotOptions.ohlc.tooltip, threshold: null, lineColor: "#000000",
                                lineWidth: 1, upColor: "#ffffff", stickyTracking: !0
                            }), {
                                pointAttribs: function (e, p) { var q = D.column.prototype.pointAttribs.call(this, e, p), m = this.options, z = e.open < e.close, B = m.lineColor || this.color; q["stroke-width"] = m.lineWidth; q.fill = e.options.color || (z ? m.upColor || this.color : this.color); q.stroke = e.options.lineColor || (z ? m.upLineColor || B : B); p && (e = m.states[p], q.fill = e.color || q.fill, q.stroke = e.lineColor || q.stroke, q["stroke-width"] = e.lineWidth || q["stroke-width"]); return q }, drawPoints: function () {
                                    var e = this, p = e.chart,
                                    q = e.yAxis.reversed; e.points.forEach(function (m) {
                                        var z = m.graphic, B = !z; if ("undefined" !== typeof m.plotY) {
                                            z || (m.graphic = z = p.renderer.path().add(e.group)); e.chart.styledMode || z.attr(e.pointAttribs(m, m.selected && "select")).shadow(e.options.shadow); var A = z.strokeWidth() % 2 / 2; var D = Math.round(m.plotX) - A; var w = m.plotOpen; var r = m.plotClose; var u = Math.min(w, r); w = Math.max(w, r); var C = Math.round(m.shapeArgs.width / 2); r = q ? w !== m.yBottom : Math.round(u) !== Math.round(m.plotHigh); var h = q ? Math.round(u) !== Math.round(m.plotHigh) :
                                                w !== m.yBottom; u = Math.round(u) + A; w = Math.round(w) + A; A = []; A.push(["M", D - C, w], ["L", D - C, u], ["L", D + C, u], ["L", D + C, w], ["Z"], ["M", D, u], ["L", D, r ? Math.round(q ? m.yBottom : m.plotHigh) : u], ["M", D, w], ["L", D, h ? Math.round(q ? m.plotHigh : m.yBottom) : w]); z[B ? "attr" : "animate"]({ d: A }).addClass(m.getClassName(), !0)
                                        }
                                    })
                                }
                            }); ""
                        }); O(q, "mixins/on-series.js", [q["parts/Globals.js"], q["parts/Utilities.js"]], function (p, e) {
                            var q = e.defined, B = e.stableSort, D = p.seriesTypes; return {
                                getPlotBox: function () {
                                    return p.Series.prototype.getPlotBox.call(this.options.onSeries &&
                                        this.chart.get(this.options.onSeries) || this)
                                }, translate: function () {
                                    D.column.prototype.translate.apply(this); var e = this, p = e.options, G = e.chart, m = e.points, H = m.length - 1, M, A = p.onSeries; A = A && G.get(A); p = p.onKey || "y"; var K = A && A.options.step, w = A && A.points, r = w && w.length, u = G.inverted, C = e.xAxis, h = e.yAxis, f = 0, d; if (A && A.visible && r) {
                                        f = (A.pointXOffset || 0) + (A.barW || 0) / 2; G = A.currentDataGrouping; var t = w[r - 1].x + (G ? G.totalRange : 0); B(m, function (a, c) { return a.x - c.x }); for (p = "plot" + p[0].toUpperCase() + p.substr(1); r-- && m[H];) {
                                            var l =
                                                w[r]; G = m[H]; G.y = l.y; if (l.x <= G.x && "undefined" !== typeof l[p]) { if (G.x <= t && (G.plotY = l[p], l.x < G.x && !K && (d = w[r + 1]) && "undefined" !== typeof d[p])) { var c = (G.x - l.x) / (d.x - l.x); G.plotY += c * (d[p] - l[p]); G.y += c * (d.y - l.y) } H--; r++; if (0 > H) break }
                                        }
                                    } m.forEach(function (a, c) {
                                    a.plotX += f; if ("undefined" === typeof a.plotY || u) 0 <= a.plotX && a.plotX <= C.len ? u ? (a.plotY = C.translate(a.x, 0, 1, 0, 1), a.plotX = q(a.y) ? h.translate(a.y, 0, 0, 0, 1) : 0) : a.plotY = (C.opposite ? 0 : e.yAxis.len) + C.offset : a.shapeArgs = {}; if ((M = m[c - 1]) && M.plotX === a.plotX) {
                                    "undefined" ===
                                        typeof M.stackIndex && (M.stackIndex = 0); var d = M.stackIndex + 1
                                    } a.stackIndex = d
                                    }); this.onSeries = A
                                }
                            }
                        }); O(q, "parts/FlagsSeries.js", [q["parts/Globals.js"], q["parts/SVGElement.js"], q["parts/SVGRenderer.js"], q["parts/Utilities.js"], q["mixins/on-series.js"]], function (p, e, q, B, D) {
                            function z(e) {
                            h[e + "pin"] = function (d, f, l, c, a) {
                                var m = a && a.anchorX; a = a && a.anchorY; "circle" === e && c > l && (d -= Math.round((c - l) / 2), l = c); var p = h[e](d, f, l, c); if (m && a) {
                                    var r = m; "circle" === e ? r = d + l / 2 : (d = p[0], l = p[1], "M" === d[0] && "L" === l[0] && (r = (d[1] + l[1]) /
                                        2)); p.push(["M", r, f > a ? f : f + c], ["L", m, a]); p = p.concat(h.circle(m - 1, a - 1, 2, 2))
                                } return p
                            }
                            } var J = B.addEvent, G = B.defined, m = B.isNumber, H = B.merge, M = B.objectEach, A = B.seriesType, K = B.wrap; B = p.noop; var w = p.Renderer, r = p.Series, u = p.TrackerMixin, C = p.VMLRenderer, h = q.prototype.symbols; A("flags", "column", {
                                pointRange: 0, allowOverlapX: !1, shape: "flag", stackDistance: 12, textAlign: "center", tooltip: { pointFormat: "{point.text}<br/>" }, threshold: null, y: -30, fillColor: "#ffffff", lineWidth: 1, states: { hover: { lineColor: "#000000", fillColor: "#ccd6eb" } },
                                style: { fontSize: "11px", fontWeight: "bold" }
                            }, {
                                sorted: !1, noSharedTooltip: !0, allowDG: !1, takeOrdinalPosition: !1, trackerGroups: ["markerGroup"], forceCrop: !0, init: r.prototype.init, pointAttribs: function (e, d) { var f = this.options, h = e && e.color || this.color, c = f.lineColor, a = e && e.lineWidth; e = e && e.fillColor || f.fillColor; d && (e = f.states[d].fillColor, c = f.states[d].lineColor, a = f.states[d].lineWidth); return { fill: e || h, stroke: c || h, "stroke-width": a || f.lineWidth || 0 } }, translate: D.translate, getPlotBox: D.getPlotBox, drawPoints: function () {
                                    var f =
                                        this.points, d = this.chart, h = d.renderer, l = d.inverted, c = this.options, a = c.y, m, r = this.yAxis, q = {}, u = []; for (m = f.length; m--;) {
                                            var k = f[m]; var y = (l ? k.plotY : k.plotX) > this.xAxis.len; var w = k.plotX; var z = k.stackIndex; var g = k.options.shape || c.shape; var b = k.plotY; "undefined" !== typeof b && (b = k.plotY + a - ("undefined" !== typeof z && z * c.stackDistance)); k.anchorX = z ? void 0 : k.plotX; var n = z ? void 0 : k.plotY; var A = "flag" !== g; z = k.graphic; "undefined" !== typeof b && 0 <= w && !y ? (z || (z = k.graphic = h.label("", null, null, g, null, null, c.useHTML),
                                                d.styledMode || z.attr(this.pointAttribs(k)).css(H(c.style, k.style)), z.attr({ align: A ? "center" : "left", width: c.width, height: c.height, "text-align": c.textAlign }).addClass("highcharts-point").add(this.markerGroup), k.graphic.div && (k.graphic.div.point = k), d.styledMode || z.shadow(c.shadow), z.isNew = !0), 0 < w && (w -= z.strokeWidth() % 2), g = { y: b, anchorY: n }, c.allowOverlapX && (g.x = w, g.anchorX = k.anchorX), z.attr({ text: k.options.title || c.title || "A" })[z.isNew ? "attr" : "animate"](g), c.allowOverlapX || (q[k.plotX] ? q[k.plotX].size =
                                                    Math.max(q[k.plotX].size, z.width) : q[k.plotX] = { align: A ? .5 : 0, size: z.width, target: w, anchorX: w }), k.tooltipPos = [w, b + r.pos - d.plotTop]) : z && (k.graphic = z.destroy())
                                        } c.allowOverlapX || (M(q, function (a) { a.plotX = a.anchorX; u.push(a) }), p.distribute(u, l ? r.len : this.xAxis.len, 100), f.forEach(function (a) { var b = a.graphic && q[a.plotX]; b && (a.graphic[a.graphic.isNew ? "attr" : "animate"]({ x: b.pos + b.align * b.size, anchorX: a.anchorX }), G(b.pos) ? a.graphic.isNew = !1 : (a.graphic.attr({ x: -9999, anchorX: -9999 }), a.graphic.isNew = !0)) })); c.useHTML &&
                                            K(this.markerGroup, "on", function (a) { return e.prototype.on.apply(a.apply(this, [].slice.call(arguments, 1)), [].slice.call(arguments, 1)) })
                                }, drawTracker: function () { var e = this.points; u.drawTrackerPoint.apply(this); e.forEach(function (d) { var f = d.graphic; f && J(f.element, "mouseover", function () { 0 < d.stackIndex && !d.raised && (d._y = f.y, f.attr({ y: d._y - 8 }), d.raised = !0); e.forEach(function (e) { e !== d && e.raised && e.graphic && (e.graphic.attr({ y: e._y }), e.raised = !1) }) }) }) }, animate: function (e) { e && this.setClip() }, setClip: function () {
                                    r.prototype.setClip.apply(this,
                                        arguments); !1 !== this.options.clip && this.sharedClipKey && this.markerGroup.clip(this.chart[this.sharedClipKey])
                                }, buildKDTree: B, invertGroups: B
                            }, { isValid: function () { return m(this.y) || "undefined" === typeof this.y } }); h.flag = function (e, d, m, l, c) { var a = c && c.anchorX || e; c = c && c.anchorY || d; var f = h.circle(a - 1, c - 1, 2, 2); f.push(["M", a, c], ["L", e, d + l], ["L", e, d], ["L", e + m, d], ["L", e + m, d + l], ["L", e, d + l], ["Z"]); return f }; z("circle"); z("square"); w === C && ["circlepin", "flag", "squarepin"].forEach(function (e) {
                                C.prototype.symbols[e] =
                                h[e]
                            }); ""
                        }); O(q, "parts/RangeSelector.js", [q["parts/Axis.js"], q["parts/Chart.js"], q["parts/Globals.js"], q["parts/Options.js"], q["parts/SVGElement.js"], q["parts/Utilities.js"]], function (p, e, q, B, D, z) {
                            var J = B.defaultOptions, G = z.addEvent, m = z.createElement, H = z.css, M = z.defined, A = z.destroyObjectProperties, K = z.discardElement, w = z.extend, r = z.fireEvent, u = z.isNumber, C = z.merge, h = z.objectEach, f = z.pick, d = z.pInt, t = z.splat; w(J, {
                                rangeSelector: {
                                    verticalAlign: "top", buttonTheme: { width: 28, height: 18, padding: 2, zIndex: 7 },
                                    floating: !1, x: 0, y: 0, height: void 0, inputPosition: { align: "right", x: 0, y: 0 }, buttonPosition: { align: "left", x: 0, y: 0 }, labelStyle: { color: "#666666" }
                                }
                            }); J.lang = C(J.lang, { rangeSelectorZoom: "Zoom", rangeSelectorFrom: "From", rangeSelectorTo: "To" }); var l = function () {
                                function c(a) { this.buttons = void 0; this.buttonOptions = c.prototype.defaultButtons; this.options = void 0; this.chart = a; this.init(a) } c.prototype.clickButton = function (a, c) {
                                    var d = this.chart, e = this.buttonOptions[a], h = d.xAxis[0], k = d.scroller && d.scroller.getUnionExtremes() ||
                                        h || {}, l = k.dataMin, m = k.dataMax, q = h && Math.round(Math.min(h.max, f(m, h.max))), g = e.type; k = e._range; var b, n = e.dataGrouping; if (null !== l && null !== m) {
                                        d.fixedRange = k; n && (this.forcedDataGrouping = !0, p.prototype.setDataGrouping.call(h || { chart: this.chart }, n, !1), this.frozenStates = e.preserveDataGrouping); if ("month" === g || "year" === g) if (h) { g = { range: e, max: q, chart: d, dataMin: l, dataMax: m }; var r = h.minFromRange.call(g); u(g.newMax) && (q = g.newMax) } else k = e; else if (k) r = Math.max(q - k, l), q = Math.min(r + k, m); else if ("ytd" === g) if (h) "undefined" ===
                                            typeof m && (l = Number.MAX_VALUE, m = Number.MIN_VALUE, d.series.forEach(function (a) { a = a.xData; l = Math.min(a[0], l); m = Math.max(a[a.length - 1], m) }), c = !1), q = this.getYTDExtremes(m, l, d.time.useUTC), r = b = q.min, q = q.max; else { this.deferredYTDClick = a; return } else "all" === g && h && (r = l, q = m); r += e._offsetMin; q += e._offsetMax; this.setSelected(a); if (h) h.setExtremes(r, q, f(c, 1), null, { trigger: "rangeSelectorButton", rangeSelectorButton: e }); else {
                                                var x = t(d.options.xAxis)[0]; var w = x.range; x.range = k; var z = x.min; x.min = b; G(d, "load", function () {
                                                x.range =
                                                    w; x.min = z
                                                })
                                            }
                                        }
                                }; c.prototype.setSelected = function (a) { this.selected = this.options.selected = a }; c.prototype.init = function (a) {
                                    var c = this, d = a.options.rangeSelector, e = d.buttons || c.defaultButtons.slice(), f = d.selected, h = function () { var a = c.minInput, d = c.maxInput; a && a.blur && r(a, "blur"); d && d.blur && r(d, "blur") }; c.chart = a; c.options = d; c.buttons = []; c.buttonOptions = e; this.unMouseDown = G(a.container, "mousedown", h); this.unResize = G(a, "resize", h); e.forEach(c.computeButtonRange); "undefined" !== typeof f && e[f] && this.clickButton(f,
                                        !1); G(a, "load", function () { a.xAxis && a.xAxis[0] && G(a.xAxis[0], "setExtremes", function (d) { this.max - this.min !== a.fixedRange && "rangeSelectorButton" !== d.trigger && "updatedData" !== d.trigger && c.forcedDataGrouping && !c.frozenStates && this.setDataGrouping(!1, !1) }) })
                                }; c.prototype.updateButtonStates = function () {
                                    var a = this, c = this.chart, d = c.xAxis[0], e = Math.round(d.max - d.min), f = !d.hasVisibleSeries, h = c.scroller && c.scroller.getUnionExtremes() || d, l = h.dataMin, m = h.dataMax; c = a.getYTDExtremes(m, l, c.time.useUTC); var p = c.min,
                                        g = c.max, b = a.selected, n = u(b), q = a.options.allButtonsEnabled, r = a.buttons; a.buttonOptions.forEach(function (c, h) {
                                            var k = c._range, t = c.type, v = c.count || 1, u = r[h], x = 0, y = c._offsetMax - c._offsetMin; c = h === b; var w = k > m - l, E = k < d.minRange, z = !1, A = !1; k = k === e; ("month" === t || "year" === t) && e + 36E5 >= 864E5 * { month: 28, year: 365 }[t] * v - y && e - 36E5 <= 864E5 * { month: 31, year: 366 }[t] * v + y ? k = !0 : "ytd" === t ? (k = g - p + y === e, z = !c) : "all" === t && (k = d.max - d.min >= m - l, A = !c && n && k); t = !q && (w || E || A || f); v = c && k || k && !n && !z || c && a.frozenStates; t ? x = 3 : v && (n = !0, x = 2); u.state !==
                                                x && (u.setState(x), 0 === x && b === h && a.setSelected(null))
                                        })
                                }; c.prototype.computeButtonRange = function (a) { var c = a.type, d = a.count || 1, e = { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5 }; if (e[c]) a._range = e[c] * d; else if ("month" === c || "year" === c) a._range = 864E5 * { month: 30, year: 365 }[c] * d; a._offsetMin = f(a.offsetMin, 0); a._offsetMax = f(a.offsetMax, 0); a._range += a._offsetMax - a._offsetMin }; c.prototype.setInputValue = function (a, c) {
                                    var d = this.chart.options.rangeSelector, e = this.chart.time, f = this[a + "Input"];
                                    M(c) && (f.previousValue = f.HCTime, f.HCTime = c); f.value = e.dateFormat(d.inputEditDateFormat || "%Y-%m-%d", f.HCTime); this[a + "DateBox"].attr({ text: e.dateFormat(d.inputDateFormat || "%b %e, %Y", f.HCTime) })
                                }; c.prototype.showInput = function (a) { var c = this.inputGroup, d = this[a + "DateBox"]; H(this[a + "Input"], { left: c.translateX + d.x + "px", top: c.translateY + "px", width: d.width - 2 + "px", height: d.height - 2 + "px", border: "2px solid silver" }) }; c.prototype.hideInput = function (a) {
                                    H(this[a + "Input"], { border: 0, width: "1px", height: "1px" });
                                    this.setInputValue(a)
                                }; c.prototype.drawInput = function (a) {
                                    function c() {
                                        var a = g.value, b = (l.inputDateParser || Date.parse)(a), c = f.xAxis[0], h = f.scroller && f.scroller.xAxis ? f.scroller.xAxis : c, k = h.dataMin; h = h.dataMax; b !== g.previousValue && (g.previousValue = b, u(b) || (b = a.split("-"), b = Date.UTC(d(b[0]), d(b[1]) - 1, d(b[2]))), u(b) && (f.time.useUTC || (b += 6E4 * (new Date).getTimezoneOffset()), r ? b > e.maxInput.HCTime ? b = void 0 : b < k && (b = k) : b < e.minInput.HCTime ? b = void 0 : b > h && (b = h), "undefined" !== typeof b && c.setExtremes(r ? b : c.min,
                                            r ? c.max : b, void 0, void 0, { trigger: "rangeSelectorInput" })))
                                    } var e = this, f = e.chart, h = f.renderer.style || {}, k = f.renderer, l = f.options.rangeSelector, p = e.div, r = "min" === a, g, b, n = this.inputGroup; this[a + "Label"] = b = k.label(J.lang[r ? "rangeSelectorFrom" : "rangeSelectorTo"], this.inputGroup.offset).addClass("highcharts-range-label").attr({ padding: 2 }).add(n); n.offset += b.width + 5; this[a + "DateBox"] = k = k.label("", n.offset).addClass("highcharts-range-input").attr({
                                        padding: 2, width: l.inputBoxWidth || 90, height: l.inputBoxHeight ||
                                            17, "text-align": "center"
                                    }).on("click", function () { e.showInput(a); e[a + "Input"].focus() }); f.styledMode || k.attr({ stroke: l.inputBoxBorderColor || "#cccccc", "stroke-width": 1 }); k.add(n); n.offset += k.width + (r ? 10 : 0); this[a + "Input"] = g = m("input", { name: a, className: "highcharts-range-selector", type: "text" }, { top: f.plotTop + "px" }, p); f.styledMode || (b.css(C(h, l.labelStyle)), k.css(C({ color: "#333333" }, h, l.inputStyle)), H(g, w({
                                        position: "absolute", border: 0, width: "1px", height: "1px", padding: 0, textAlign: "center", fontSize: h.fontSize,
                                        fontFamily: h.fontFamily, top: "-9999em"
                                    }, l.inputStyle))); g.onfocus = function () { e.showInput(a) }; g.onblur = function () { g === q.doc.activeElement && c(); e.hideInput(a); g.blur() }; g.onchange = c; g.onkeypress = function (a) { 13 === a.keyCode && c() }
                                }; c.prototype.getPosition = function () { var a = this.chart, c = a.options.rangeSelector; a = "top" === c.verticalAlign ? a.plotTop - a.axisOffset[0] : 0; return { buttonTop: a + c.buttonPosition.y, inputTop: a + c.inputPosition.y - 10 } }; c.prototype.getYTDExtremes = function (a, c, d) {
                                    var e = this.chart.time, f = new e.Date(a),
                                    h = e.get("FullYear", f); d = d ? e.Date.UTC(h, 0, 1) : +new e.Date(h, 0, 1); c = Math.max(c || 0, d); f = f.getTime(); return { max: Math.min(a || f, f), min: c }
                                }; c.prototype.render = function (a, c) {
                                    var d = this, e = d.chart, h = e.renderer, k = e.container, l = e.options, p = l.exporting && !1 !== l.exporting.enabled && l.navigation && l.navigation.buttonOptions, r = J.lang, g = d.div, b = l.rangeSelector, n = f(l.chart.style && l.chart.style.zIndex, 0) + 1; l = b.floating; var q = d.buttons; g = d.inputGroup; var t = b.buttonTheme, u = b.buttonPosition, x = b.inputPosition, w = b.inputEnabled,
                                        z = t && t.states, A = e.plotLeft, B = d.buttonGroup, C, D = d.options.verticalAlign, G = e.legend, H = G && G.options, K = u.y, M = x.y, O = e.hasLoaded, R = O ? "animate" : "attr", Q = 0, U = 0; if (!1 !== b.enabled) {
                                        d.rendered || (d.group = C = h.g("range-selector-group").attr({ zIndex: 7 }).add(), d.buttonGroup = B = h.g("range-selector-buttons").add(C), d.zoomText = h.text(r.rangeSelectorZoom, 0, 15).add(B), e.styledMode || (d.zoomText.css(b.labelStyle), t["stroke-width"] = f(t["stroke-width"], 0)), d.buttonOptions.forEach(function (a, b) {
                                        q[b] = h.button(a.text, 0, 0, function (c) {
                                            var e =
                                                a.events && a.events.click, f; e && (f = e.call(a, c)); !1 !== f && d.clickButton(b); d.isActive = !0
                                        }, t, z && z.hover, z && z.select, z && z.disabled).attr({ "text-align": "center" }).add(B)
                                        }), !1 !== w && (d.div = g = m("div", null, { position: "relative", height: 0, zIndex: n }), k.parentNode.insertBefore(g, k), d.inputGroup = g = h.g("input-group").add(C), g.offset = 0, d.drawInput("min"), d.drawInput("max"))); d.zoomText[R]({ x: f(A + u.x, A) }); var X = f(A + u.x, A) + d.zoomText.getBBox().width + 5; d.buttonOptions.forEach(function (a, c) {
                                        q[c][R]({ x: X }); X += q[c].width +
                                            f(b.buttonSpacing, 5)
                                        }); A = e.plotLeft - e.spacing[3]; d.updateButtonStates(); p && this.titleCollision(e) && "top" === D && "right" === u.align && u.y + B.getBBox().height - 12 < (p.y || 0) + p.height && (Q = -40); k = u.x - e.spacing[3]; "right" === u.align ? k += Q - A : "center" === u.align && (k -= A / 2); B.align({ y: u.y, width: B.getBBox().width, align: u.align, x: k }, !0, e.spacingBox); d.group.placed = O; d.buttonGroup.placed = O; !1 !== w && (Q = p && this.titleCollision(e) && "top" === D && "right" === x.align && x.y - g.getBBox().height - 12 < (p.y || 0) + p.height + e.spacing[0] ? -40 : 0,
                                            "left" === x.align ? k = A : "right" === x.align && (k = -Math.max(e.axisOffset[1], -Q)), g.align({ y: x.y, width: g.getBBox().width, align: x.align, x: x.x + k - 2 }, !0, e.spacingBox), p = g.alignAttr.translateX + g.alignOptions.x - Q + g.getBBox().x + 2, k = g.alignOptions.width, r = B.alignAttr.translateX + B.getBBox().x, A = B.getBBox().width + 20, (x.align === u.align || r + A > p && p + k > r && K < M + g.getBBox().height) && g.attr({ translateX: g.alignAttr.translateX + (e.axisOffset[1] >= -Q ? 0 : -Q), translateY: g.alignAttr.translateY + B.getBBox().height + 10 }), d.setInputValue("min",
                                                a), d.setInputValue("max", c), d.inputGroup.placed = O); d.group.align({ verticalAlign: D }, !0, e.spacingBox); a = d.group.getBBox().height + 20; c = d.group.alignAttr.translateY; "bottom" === D && (G = H && "bottom" === H.verticalAlign && H.enabled && !H.floating ? G.legendHeight + f(H.margin, 10) : 0, a = a + G - 20, U = c - a - (l ? 0 : b.y) - (e.titleOffset ? e.titleOffset[2] : 0) - 10); if ("top" === D) l && (U = 0), e.titleOffset && e.titleOffset[0] && (U = e.titleOffset[0]), U += e.margin[0] - e.spacing[0] || 0; else if ("middle" === D) if (M === K) U = 0 > M ? c + void 0 : c; else if (M || K) U = 0 > M ||
                                                    0 > K ? U - Math.min(M, K) : c - a + NaN; d.group.translate(b.x, b.y + Math.floor(U)); !1 !== w && (d.minInput.style.marginTop = d.group.translateY + "px", d.maxInput.style.marginTop = d.group.translateY + "px"); d.rendered = !0
                                        }
                                }; c.prototype.getHeight = function () { var a = this.options, c = this.group, d = a.y, e = a.buttonPosition.y, f = a.inputPosition.y; if (a.height) return a.height; a = c ? c.getBBox(!0).height + 13 + d : 0; c = Math.min(f, e); if (0 > f && 0 > e || 0 < f && 0 < e) a += Math.abs(c); return a }; c.prototype.titleCollision = function (a) {
                                    return !(a.options.title.text ||
                                        a.options.subtitle.text)
                                }; c.prototype.update = function (a) { var c = this.chart; C(!0, c.options.rangeSelector, a); this.destroy(); this.init(c); c.rangeSelector.render() }; c.prototype.destroy = function () { var a = this, d = a.minInput, e = a.maxInput; a.unMouseDown(); a.unResize(); A(a.buttons); d && (d.onfocus = d.onblur = d.onchange = null); e && (e.onfocus = e.onblur = e.onchange = null); h(a, function (d, e) { d && "chart" !== e && (d instanceof D ? d.destroy() : d instanceof window.HTMLElement && K(d)); d !== c.prototype[e] && (a[e] = null) }, this) }; return c
                            }();
                            l.prototype.defaultButtons = [{ type: "month", count: 1, text: "1m" }, { type: "month", count: 3, text: "3m" }, { type: "month", count: 6, text: "6m" }, { type: "ytd", text: "YTD" }, { type: "year", count: 1, text: "1y" }, { type: "all", text: "All" }]; p.prototype.minFromRange = function () {
                                var c = this.range, a = c.type, d = this.max, e = this.chart.time, h = function (c, d) { var f = "year" === a ? "FullYear" : "Month", b = new e.Date(c), h = e.get(f, b); e.set(f, b, h + d); h === e.get(f, b) && e.set("Date", b, 0); return b.getTime() - c }; if (u(c)) { var l = d - c; var k = c } else l = d + h(d, -c.count),
                                    this.chart && (this.chart.fixedRange = d - l); var m = f(this.dataMin, Number.MIN_VALUE); u(l) || (l = m); l <= m && (l = m, "undefined" === typeof k && (k = h(l, c.count)), this.newMax = Math.min(l + k, this.dataMax)); u(d) || (l = void 0); return l
                            }; q.RangeSelector || (G(e, "afterGetContainer", function () { this.options.rangeSelector.enabled && (this.rangeSelector = new l(this)) }), G(e, "beforeRender", function () {
                                var c = this.axes, a = this.rangeSelector; a && (u(a.deferredYTDClick) && (a.clickButton(a.deferredYTDClick), delete a.deferredYTDClick), c.forEach(function (a) {
                                    a.updateNames();
                                    a.setScale()
                                }), this.getAxisMargins(), a.render(), c = a.options.verticalAlign, a.options.floating || ("bottom" === c ? this.extraBottomMargin = !0 : "middle" !== c && (this.extraTopMargin = !0)))
                            }), G(e, "update", function (c) {
                                var a = c.options.rangeSelector; c = this.rangeSelector; var d = this.extraBottomMargin, e = this.extraTopMargin; a && a.enabled && !M(c) && (this.options.rangeSelector.enabled = !0, this.rangeSelector = new l(this)); this.extraTopMargin = this.extraBottomMargin = !1; c && (c.render(), a = a && a.verticalAlign || c.options && c.options.verticalAlign,
                                    c.options.floating || ("bottom" === a ? this.extraBottomMargin = !0 : "middle" !== a && (this.extraTopMargin = !0)), this.extraBottomMargin !== d || this.extraTopMargin !== e) && (this.isDirtyBox = !0)
                            }), G(e, "render", function () { var c = this.rangeSelector; c && !c.options.floating && (c.render(), c = c.options.verticalAlign, "bottom" === c ? this.extraBottomMargin = !0 : "middle" !== c && (this.extraTopMargin = !0)) }), G(e, "getMargins", function () {
                                var c = this.rangeSelector; c && (c = c.getHeight(), this.extraTopMargin && (this.plotTop += c), this.extraBottomMargin &&
                                    (this.marginBottom += c))
                            }), e.prototype.callbacks.push(function (c) {
                                function a() { d = c.xAxis[0].getExtremes(); f = c.legend; k = null === e || void 0 === e ? void 0 : e.options.verticalAlign; u(d.min) && e.render(d.min, d.max); e && f.display && "top" === k && k === f.options.verticalAlign && (h = C(c.spacingBox), h.y = "vertical" === f.options.layout ? c.plotTop : h.y + e.getHeight(), f.group.placed = !1, f.align(h)) } var d, e = c.rangeSelector, f, h, k; if (e) { var l = G(c.xAxis[0], "afterSetExtremes", function (a) { e.render(a.min, a.max) }); var m = G(c, "redraw", a); a() } G(c,
                                    "destroy", function () { e && (m(), l()) })
                            }), q.RangeSelector = l); return q.RangeSelector
                        }); O(q, "parts/StockChart.js", [q["parts/Axis.js"], q["parts/Chart.js"], q["parts/Globals.js"], q["parts/Point.js"], q["parts/SVGRenderer.js"], q["parts/Utilities.js"]], function (p, e, q, B, D, z) {
                            var J = z.addEvent, G = z.arrayMax, m = z.arrayMin, H = z.clamp, M = z.defined, A = z.extend, K = z.find, w = z.format, r = z.getOptions, u = z.isNumber, C = z.isString, h = z.merge, f = z.pick, d = z.splat; z = q.Series; var t = z.prototype, l = t.init, c = t.processData, a = B.prototype.tooltipFormatter;
                            q.StockChart = q.stockChart = function (a, c, l) {
                                var m = C(a) || a.nodeName, k = arguments[m ? 1 : 0], p = k, q = k.series, t = r(), g, b = f(k.navigator && k.navigator.enabled, t.navigator.enabled, !0); k.xAxis = d(k.xAxis || {}).map(function (a, c) { return h({ minPadding: 0, maxPadding: 0, overscroll: 0, ordinal: !0, title: { text: null }, labels: { overflow: "justify" }, showLastLabel: !0 }, t.xAxis, t.xAxis && t.xAxis[c], a, { type: "datetime", categories: null }, b ? { startOnTick: !1, endOnTick: !1 } : null) }); k.yAxis = d(k.yAxis || {}).map(function (a, b) {
                                    g = f(a.opposite, !0); return h({
                                        labels: { y: -2 },
                                        opposite: g, showLastLabel: !(!a.categories && "category" !== a.type), title: { text: null }
                                    }, t.yAxis, t.yAxis && t.yAxis[b], a)
                                }); k.series = null; k = h({ chart: { panning: { enabled: !0, type: "x" }, pinchType: "x" }, navigator: { enabled: b }, scrollbar: { enabled: f(t.scrollbar.enabled, !0) }, rangeSelector: { enabled: f(t.rangeSelector.enabled, !0) }, title: { text: null }, tooltip: { split: f(t.tooltip.split, !0), crosshairs: !0 }, legend: { enabled: !1 } }, k, { isStock: !0 }); k.series = p.series = q; return m ? new e(a, k, l) : new e(k, c)
                            }; J(z, "setOptions", function (a) {
                                var c;
                                this.chart.options.isStock && (this.is("column") || this.is("columnrange") ? c = { borderWidth: 0, shadow: !1 } : this.is("scatter") || this.is("sma") || (c = { marker: { enabled: !1, radius: 2 } }), c && (a.plotOptions[this.type] = h(a.plotOptions[this.type], c)))
                            }); J(p, "autoLabelAlign", function (a) {
                                var c = this.chart, d = this.options; c = c._labelPanes = c._labelPanes || {}; var e = this.options.labels; this.chart.options.isStock && "yAxis" === this.coll && (d = d.top + "," + d.height, !c[d] && e.enabled && (15 === e.x && (e.x = 0), "undefined" === typeof e.align && (e.align =
                                    "right"), c[d] = this, a.align = "right", a.preventDefault()))
                            }); J(p, "destroy", function () { var a = this.chart, c = this.options && this.options.top + "," + this.options.height; c && a._labelPanes && a._labelPanes[c] === this && delete a._labelPanes[c] }); J(p, "getPlotLinePath", function (a) {
                                function c(a) { var b = "xAxis" === a ? "yAxis" : "xAxis"; a = d.options[b]; return u(a) ? [h[b][a]] : C(a) ? [h.get(a)] : e.map(function (a) { return a[b] }) } var d = this, e = this.isLinked && !this.series ? this.linkedParent.series : this.series, h = d.chart, l = h.renderer, m = d.left,
                                    p = d.top, g, b, n, q, r = [], t = [], x = a.translatedValue, w = a.value, z = a.force; if (h.options.isStock && !1 !== a.acrossPanes && "xAxis" === d.coll || "yAxis" === d.coll) {
                                        a.preventDefault(); t = c(d.coll); var A = d.isXAxis ? h.yAxis : h.xAxis; A.forEach(function (a) { if (M(a.options.id) ? -1 === a.options.id.indexOf("navigator") : 1) { var b = a.isXAxis ? "yAxis" : "xAxis"; b = M(a.options[b]) ? h[b][a.options[b]] : h[b][0]; d === b && t.push(a) } }); var B = t.length ? [] : [d.isXAxis ? h.yAxis[0] : h.xAxis[0]]; t.forEach(function (a) {
                                        -1 !== B.indexOf(a) || K(B, function (b) {
                                            return b.pos ===
                                                a.pos && b.len === a.len
                                        }) || B.push(a)
                                        }); var D = f(x, d.translate(w, null, null, a.old)); u(D) && (d.horiz ? B.forEach(function (a) { var c; b = a.pos; q = b + a.len; g = n = Math.round(D + d.transB); "pass" !== z && (g < m || g > m + d.width) && (z ? g = n = H(g, m, m + d.width) : c = !0); c || r.push(["M", g, b], ["L", n, q]) }) : B.forEach(function (a) { var c; g = a.pos; n = g + a.len; b = q = Math.round(p + d.height - D); "pass" !== z && (b < p || b > p + d.height) && (z ? b = q = H(b, p, p + d.height) : c = !0); c || r.push(["M", g, b], ["L", n, q]) })); a.path = 0 < r.length ? l.crispPolyLine(r, a.lineWidth || 1) : null
                                    }
                            }); D.prototype.crispPolyLine =
                                function (a, c) { for (var d = 0; d < a.length; d += 2) { var e = a[d], f = a[d + 1]; e[1] === f[1] && (e[1] = f[1] = Math.round(e[1]) - c % 2 / 2); e[2] === f[2] && (e[2] = f[2] = Math.round(e[2]) + c % 2 / 2) } return a }; J(p, "afterHideCrosshair", function () { this.crossLabel && (this.crossLabel = this.crossLabel.hide()) }); J(p, "afterDrawCrosshair", function (a) {
                                    var c, d; if (M(this.crosshair.label) && this.crosshair.label.enabled && this.cross) {
                                        var e = this.chart, h = this.logarithmic, l = this.options.crosshair.label, m = this.horiz, p = this.opposite, g = this.left, b = this.top, n =
                                            this.crossLabel, q = l.format, r = "", t = "inside" === this.options.tickPosition, x = !1 !== this.crosshair.snap, z = 0, B = a.e || this.cross && this.cross.e, C = a.point; a = this.min; var D = this.max; h && (a = h.lin2log(a), D = h.lin2log(D)); h = m ? "center" : p ? "right" === this.labelAlign ? "right" : "left" : "left" === this.labelAlign ? "left" : "center"; n || (n = this.crossLabel = e.renderer.label(null, null, null, l.shape || "callout").addClass("highcharts-crosshair-label" + (this.series[0] && " highcharts-color-" + this.series[0].colorIndex)).attr({
                                                align: l.align ||
                                                    h, padding: f(l.padding, 8), r: f(l.borderRadius, 3), zIndex: 2
                                            }).add(this.labelGroup), e.styledMode || n.attr({ fill: l.backgroundColor || this.series[0] && this.series[0].color || "#666666", stroke: l.borderColor || "", "stroke-width": l.borderWidth || 0 }).css(A({ color: "#ffffff", fontWeight: "normal", fontSize: "11px", textAlign: "center" }, l.style))); m ? (h = x ? C.plotX + g : B.chartX, b += p ? 0 : this.height) : (h = p ? this.width + g : 0, b = x ? C.plotY + b : B.chartY); q || l.formatter || (this.dateTime && (r = "%b %d, %Y"), q = "{value" + (r ? ":" + r : "") + "}"); r = x ? C[this.isXAxis ?
                                                "x" : "y"] : this.toValue(m ? B.chartX : B.chartY); n.attr({ text: q ? w(q, { value: r }, e) : l.formatter.call(this, r), x: h, y: b, visibility: r < a || r > D ? "hidden" : "visible" }); l = n.getBBox(); if (u(n.y)) if (m) { if (t && !p || !t && p) b = n.y - l.height } else b = n.y - l.height / 2; m ? (c = g - l.x, d = g + this.width - l.x) : (c = "left" === this.labelAlign ? g : 0, d = "right" === this.labelAlign ? g + this.width : e.chartWidth); n.translateX < c && (z = c - n.translateX); n.translateX + l.width >= d && (z = -(n.translateX + l.width - d)); n.attr({
                                                    x: h + z, y: b, anchorX: m ? h : this.opposite ? 0 : e.chartWidth, anchorY: m ?
                                                        this.opposite ? e.chartHeight : 0 : b + l.height / 2
                                                })
                                    }
                                }); t.init = function () { l.apply(this, arguments); this.setCompare(this.options.compare) }; t.setCompare = function (a) { this.modifyValue = "value" === a || "percent" === a ? function (c, d) { var e = this.compareValue; return "undefined" !== typeof c && "undefined" !== typeof e ? (c = "value" === a ? c - e : c / e * 100 - (100 === this.options.compareBase ? 0 : 100), d && (d.change = c), c) : 0 } : null; this.userOptions.compare = a; this.chart.hasRendered && (this.isDirty = !0) }; t.processData = function (a) {
                                    var d, e = -1, f = !0 === this.options.compareStart ?
                                        0 : 1; c.apply(this, arguments); if (this.xAxis && this.processedYData) { var h = this.processedXData; var l = this.processedYData; var m = l.length; this.pointArrayMap && (e = this.pointArrayMap.indexOf(this.options.pointValKey || this.pointValKey || "y")); for (d = 0; d < m - f; d++) { var p = l[d] && -1 < e ? l[d][e] : l[d]; if (u(p) && h[d + f] >= this.xAxis.min && 0 !== p) { this.compareValue = p; break } } }
                                }; J(z, "afterGetExtremes", function (a) {
                                    a = a.dataExtremes; if (this.modifyValue && a) {
                                        var c = [this.modifyValue(a.dataMin), this.modifyValue(a.dataMax)]; a.dataMin = m(c);
                                        a.dataMax = G(c)
                                    }
                                }); p.prototype.setCompare = function (a, c) { this.isXAxis || (this.series.forEach(function (c) { c.setCompare(a) }), f(c, !0) && this.chart.redraw()) }; B.prototype.tooltipFormatter = function (c) { var d = this.series.chart.numberFormatter; c = c.replace("{point.change}", (0 < this.change ? "+" : "") + d(this.change, f(this.series.tooltipOptions.changeDecimals, 2))); return a.apply(this, [c]) }; J(z, "render", function () {
                                    var a = this.chart; if (!(a.is3d && a.is3d() || a.polar) && this.xAxis && !this.xAxis.isRadial) {
                                        var c = this.yAxis.len;
                                        if (this.xAxis.axisLine) { var d = a.plotTop + a.plotHeight - this.yAxis.pos - this.yAxis.len, e = Math.floor(this.xAxis.axisLine.strokeWidth() / 2); 0 <= d && (c -= Math.max(e - d, 0)) } !this.clipBox && this.animate ? (this.clipBox = h(a.clipBox), this.clipBox.width = this.xAxis.len, this.clipBox.height = c) : a[this.sharedClipKey] && (a[this.sharedClipKey].animate({ width: this.xAxis.len, height: c }), a[this.sharedClipKey + "m"] && a[this.sharedClipKey + "m"].animate({ width: this.xAxis.len }))
                                    }
                                }); J(e, "update", function (a) {
                                    a = a.options; "scrollbar" in a &&
                                        this.navigator && (h(!0, this.options.scrollbar, a.scrollbar), this.navigator.update({}, !1), delete a.scrollbar)
                                })
                        }); O(q, "masters/modules/stock.src.js", [], function () { }); O(q, "masters/highstock.src.js", [q["masters/highcharts.src.js"]], function (p) { p.product = "Highstock"; return p }); q["masters/highstock.src.js"]._modules = q; return q["masters/highstock.src.js"]
});
//# sourceMappingURL=highstock.js.map