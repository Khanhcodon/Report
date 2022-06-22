/*
 Highcharts JS v8.1.2 (2020-06-16)

 3D features for Highcharts JS

 License: www.highcharts.com/license
*/
(function (b) { "object" === typeof module && module.exports ? (b["default"] = b, module.exports = b) : "function" === typeof define && define.amd ? define("highcharts/highcharts-3d", ["highcharts"], function (F) { b(F); b.Highcharts = F; return b }) : b("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (b) {
    function F(b, t, l, d) { b.hasOwnProperty(t) || (b[t] = d.apply(null, l)) } b = b ? b._modules : {}; F(b, "parts-3d/Math.js", [b["parts/Globals.js"], b["parts/Utilities.js"]], function (b, t) {
        var l = t.pick, d = b.deg2rad; b.perspective3D = function (d,
            n, q) { n = 0 < q && q < Number.POSITIVE_INFINITY ? q / (d.z + n.z + q) : 1; return { x: d.x * n, y: d.y * n } }; b.perspective = function (w, n, q, D) {
                var m = n.options.chart.options3d, u = l(D, q ? n.inverted : !1), g = { x: n.plotWidth / 2, y: n.plotHeight / 2, z: m.depth / 2, vd: l(m.depth, 1) * l(m.viewDistance, 0) }, p = n.scale3d || 1; D = d * m.beta * (u ? -1 : 1); m = d * m.alpha * (u ? -1 : 1); var t = Math.cos(m), y = Math.cos(-D), a = Math.sin(m), k = Math.sin(-D); q || (g.x += n.plotLeft, g.y += n.plotTop); return w.map(function (x) {
                    var c = (u ? x.y : x.x) - g.x; var h = (u ? x.x : x.y) - g.y; x = (x.z || 0) - g.z; c = {
                        x: y * c - k *
                            x, y: -a * k * c + t * h - y * a * x, z: t * k * c + a * h + t * y * x
                    }; h = b.perspective3D(c, g, g.vd); h.x = h.x * p + g.x; h.y = h.y * p + g.y; h.z = c.z * p + g.z; return { x: u ? h.y : h.x, y: u ? h.x : h.y, z: h.z }
                })
            }; b.pointCameraDistance = function (d, n) { var q = n.options.chart.options3d, b = n.plotWidth / 2; n = n.plotHeight / 2; q = l(q.depth, 1) * l(q.viewDistance, 0) + q.depth; return Math.sqrt(Math.pow(b - l(d.plotX, d.x), 2) + Math.pow(n - l(d.plotY, d.y), 2) + Math.pow(q - l(d.plotZ, d.z), 2)) }; b.shapeArea = function (d) {
                var n = 0, q; for (q = 0; q < d.length; q++) {
                    var b = (q + 1) % d.length; n += d[q].x * d[b].y - d[b].x *
                        d[q].y
                } return n / 2
            }; b.shapeArea3d = function (d, n, q) { return b.shapeArea(b.perspective(d, n, q)) }
    }); F(b, "parts-3d/SVGRenderer.js", [b["parts/Color.js"], b["parts/Globals.js"], b["parts/SVGElement.js"], b["parts/SVGRenderer.js"], b["parts/Utilities.js"]], function (b, t, l, d, w) {
        function n(a, k, e, K, c, I, h, r) {
            var f = [], v = I - c; return I > c && I - c > Math.PI / 2 + .0001 ? (f = f.concat(n(a, k, e, K, c, c + Math.PI / 2, h, r)), f = f.concat(n(a, k, e, K, c + Math.PI / 2, I, h, r))) : I < c && c - I > Math.PI / 2 + .0001 ? (f = f.concat(n(a, k, e, K, c, c - Math.PI / 2, h, r)), f = f.concat(n(a,
                k, e, K, c - Math.PI / 2, I, h, r))) : [["C", a + e * Math.cos(c) - e * J * v * Math.sin(c) + h, k + K * Math.sin(c) + K * J * v * Math.cos(c) + r, a + e * Math.cos(I) + e * J * v * Math.sin(I) + h, k + K * Math.sin(I) - K * J * v * Math.cos(I) + r, a + e * Math.cos(I) + h, k + K * Math.sin(I) + r]]
        } var q = b.parse, D = w.animObject, m = w.defined, u = w.extend, g = w.merge, p = w.objectEach, E = w.pick, y = Math.cos, a = Math.PI, k = Math.sin, x = t.charts, c = t.deg2rad, h = t.perspective; var J = 4 * (Math.sqrt(2) - 1) / 3 / (a / 2); d.prototype.toLinePath = function (a, c) {
            var e = []; a.forEach(function (a) { e.push(["L", a.x, a.y]) }); a.length &&
                (e[0][0] = "M", c && e.push(["Z"])); return e
        }; d.prototype.toLineSegments = function (a) { var f = [], e = !0; a.forEach(function (a) { f.push(e ? ["M", a.x, a.y] : ["L", a.x, a.y]); e = !e }); return f }; d.prototype.face3d = function (a) {
            var f = this, e = this.createElement("path"); e.vertexes = []; e.insidePlotArea = !1; e.enabled = !0; e.attr = function (a) {
                if ("object" === typeof a && (m(a.enabled) || m(a.vertexes) || m(a.insidePlotArea))) {
                this.enabled = E(a.enabled, this.enabled); this.vertexes = E(a.vertexes, this.vertexes); this.insidePlotArea = E(a.insidePlotArea,
                    this.insidePlotArea); delete a.enabled; delete a.vertexes; delete a.insidePlotArea; var e = h(this.vertexes, x[f.chartIndex], this.insidePlotArea), c = f.toLinePath(e, !0); e = t.shapeArea(e); e = this.enabled && 0 < e ? "visible" : "hidden"; a.d = c; a.visibility = e
                } return l.prototype.attr.apply(this, arguments)
            }; e.animate = function (a) {
                if ("object" === typeof a && (m(a.enabled) || m(a.vertexes) || m(a.insidePlotArea))) {
                this.enabled = E(a.enabled, this.enabled); this.vertexes = E(a.vertexes, this.vertexes); this.insidePlotArea = E(a.insidePlotArea,
                    this.insidePlotArea); delete a.enabled; delete a.vertexes; delete a.insidePlotArea; var e = h(this.vertexes, x[f.chartIndex], this.insidePlotArea), c = f.toLinePath(e, !0); e = t.shapeArea(e); e = this.enabled && 0 < e ? "visible" : "hidden"; a.d = c; this.attr("visibility", e)
                } return l.prototype.animate.apply(this, arguments)
            }; return e.attr(a)
        }; d.prototype.polyhedron = function (a) {
            var f = this, e = this.g(), c = e.destroy; this.styledMode || e.attr({ "stroke-linejoin": "round" }); e.faces = []; e.destroy = function () {
                for (var a = 0; a < e.faces.length; a++)e.faces[a].destroy();
                return c.call(this)
            }; e.attr = function (a, c, k, r) { if ("object" === typeof a && m(a.faces)) { for (; e.faces.length > a.faces.length;)e.faces.pop().destroy(); for (; e.faces.length < a.faces.length;)e.faces.push(f.face3d().add(e)); for (var h = 0; h < a.faces.length; h++)f.styledMode && delete a.faces[h].fill, e.faces[h].attr(a.faces[h], null, k, r); delete a.faces } return l.prototype.attr.apply(this, arguments) }; e.animate = function (a, c, k) {
                if (a && a.faces) {
                    for (; e.faces.length > a.faces.length;)e.faces.pop().destroy(); for (; e.faces.length < a.faces.length;)e.faces.push(f.face3d().add(e));
                    for (var r = 0; r < a.faces.length; r++)e.faces[r].animate(a.faces[r], c, k); delete a.faces
                } return l.prototype.animate.apply(this, arguments)
            }; return e.attr(a)
        }; b = {
            initArgs: function (a) { var f = this, e = f.renderer, c = e[f.pathType + "Path"](a), k = c.zIndexes; f.parts.forEach(function (a) { f[a] = e.path(c[a]).attr({ "class": "highcharts-3d-" + a, zIndex: k[a] || 0 }).add(f) }); f.attr({ "stroke-linejoin": "round", zIndex: k.group }); f.originalDestroy = f.destroy; f.destroy = f.destroyParts; f.forcedSides = c.forcedSides }, singleSetterForParts: function (a,
                c, e, k, h, d) { var f = {}; k = [null, null, k || "attr", h, d]; var r = e && e.zIndexes; e ? (r && r.group && this.attr({ zIndex: r.group }), p(e, function (c, v) { f[v] = {}; f[v][a] = c; r && (f[v].zIndex = e.zIndexes[v] || 0) }), k[1] = f) : (f[a] = c, k[0] = f); return this.processParts.apply(this, k) }, processParts: function (a, c, e, k, h) { var f = this; f.parts.forEach(function (d) { c && (a = E(c[d], !1)); if (!1 !== a) f[d][e](a, k, h) }); return f }, destroyParts: function () { this.processParts(null, null, "destroy"); return this.originalDestroy() }
        }; var M = g(b, {
            parts: ["front", "top",
                "side"], pathType: "cuboid", attr: function (a, c, e, k) { if ("string" === typeof a && "undefined" !== typeof c) { var f = a; a = {}; a[f] = c } return a.shapeArgs || m(a.x) ? this.singleSetterForParts("d", null, this.renderer[this.pathType + "Path"](a.shapeArgs || a)) : l.prototype.attr.call(this, a, void 0, e, k) }, animate: function (a, c, e) {
                    if (m(a.x) && m(a.y)) {
                        a = this.renderer[this.pathType + "Path"](a); var f = a.forcedSides; this.singleSetterForParts("d", null, a, "animate", c, e); this.attr({ zIndex: a.zIndexes.group }); f !== this.forcedSides && (this.forcedSides =
                            f, M.fillSetter.call(this, this.fill))
                    } else l.prototype.animate.call(this, a, c, e); return this
                }, fillSetter: function (a) { this.forcedSides = this.forcedSides || []; this.singleSetterForParts("fill", null, { front: a, top: q(a).brighten(0 <= this.forcedSides.indexOf("top") ? 0 : .1).get(), side: q(a).brighten(0 <= this.forcedSides.indexOf("side") ? 0 : -.1).get() }); this.color = this.fill = a; return this }
        }); d.prototype.elements3d = { base: b, cuboid: M }; d.prototype.element3d = function (a, c) {
            var e = this.g(); u(e, this.elements3d[a]); e.initArgs(c);
            return e
        }; d.prototype.cuboid = function (a) { return this.element3d("cuboid", a) }; d.prototype.cuboidPath = function (a) {
            function c(a) { return 0 === y && 1 < a && 6 > a ? { x: B[a].x, y: B[a].y + 10, z: B[a].z } : B[0].x === B[7].x && 4 <= a ? { x: B[a].x + 10, y: B[a].y, z: B[a].z } : 0 === G && 2 > a || 5 < a ? { x: B[a].x, y: B[a].y, z: B[a].z + 10 } : B[a] } function e(a) { return B[a] } var f = a.x, k = a.y, d = a.z || 0, y = a.height, r = a.width, G = a.depth, v = x[this.chartIndex], z = v.options.chart.options3d.alpha, H = 0, B = [{ x: f, y: k, z: d }, { x: f + r, y: k, z: d }, { x: f + r, y: k + y, z: d }, { x: f, y: k + y, z: d }, {
                x: f, y: k +
                    y, z: d + G
            }, { x: f + r, y: k + y, z: d + G }, { x: f + r, y: k, z: d + G }, { x: f, y: k, z: d + G }], N = []; B = h(B, v, a.insidePlotArea); var A = function (a, v, f) { var k = [[], -1], r = a.map(e), z = v.map(e); a = a.map(c); v = v.map(c); 0 > t.shapeArea(r) ? k = [r, 0] : 0 > t.shapeArea(z) ? k = [z, 1] : f && (N.push(f), k = 0 > t.shapeArea(a) ? [r, 0] : 0 > t.shapeArea(v) ? [z, 1] : [r, 0]); return k }; var C = A([3, 2, 1, 0], [7, 6, 5, 4], "front"); a = C[0]; var b = C[1]; C = A([1, 6, 7, 0], [4, 5, 2, 3], "top"); r = C[0]; var n = C[1]; C = A([1, 2, 5, 6], [0, 7, 4, 3], "side"); A = C[0]; C = C[1]; 1 === C ? H += 1E6 * (v.plotWidth - f) : C || (H += 1E6 * f); H += 10 *
                (!n || 0 <= z && 180 >= z || 360 > z && 357.5 < z ? v.plotHeight - k : 10 + k); 1 === b ? H += 100 * d : b || (H += 100 * (1E3 - d)); return { front: this.toLinePath(a, !0), top: this.toLinePath(r, !0), side: this.toLinePath(A, !0), zIndexes: { group: Math.round(H) }, forcedSides: N, isFront: b, isTop: n }
        }; d.prototype.arc3d = function (a) {
            function k(a) { var e = !1, k = {}, c; a = g(a); for (c in a) -1 !== h.indexOf(c) && (k[c] = a[c], delete a[c], e = !0); return e ? [k, a] : !1 } var e = this.g(), f = e.renderer, h = "x y r innerR start end depth".split(" "); a = g(a); a.alpha = (a.alpha || 0) * c; a.beta = (a.beta ||
                0) * c; e.top = f.path(); e.side1 = f.path(); e.side2 = f.path(); e.inn = f.path(); e.out = f.path(); e.onAdd = function () { var a = e.parentGroup, c = e.attr("class"); e.top.add(e);["out", "inn", "side1", "side2"].forEach(function (k) { e[k].attr({ "class": c + " highcharts-3d-side" }).add(a) }) };["addClass", "removeClass"].forEach(function (a) { e[a] = function () { var c = arguments;["top", "out", "inn", "side1", "side2"].forEach(function (k) { e[k][a].apply(e[k], c) }) } }); e.setPaths = function (a) {
                    var k = e.renderer.arc3dPath(a), c = 100 * k.zTop; e.attribs = a; e.top.attr({
                        d: k.top,
                        zIndex: k.zTop
                    }); e.inn.attr({ d: k.inn, zIndex: k.zInn }); e.out.attr({ d: k.out, zIndex: k.zOut }); e.side1.attr({ d: k.side1, zIndex: k.zSide1 }); e.side2.attr({ d: k.side2, zIndex: k.zSide2 }); e.zIndex = c; e.attr({ zIndex: c }); a.center && (e.top.setRadialReference(a.center), delete a.center)
                }; e.setPaths(a); e.fillSetter = function (a) { var e = q(a).brighten(-.1).get(); this.fill = a; this.side1.attr({ fill: e }); this.side2.attr({ fill: e }); this.inn.attr({ fill: e }); this.out.attr({ fill: e }); this.top.attr({ fill: a }); return this };["opacity", "translateX",
                    "translateY", "visibility"].forEach(function (a) { e[a + "Setter"] = function (a, k) { e[k] = a;["out", "inn", "side1", "side2", "top"].forEach(function (c) { e[c].attr(k, a) }) } }); e.attr = function (a) { var c; if ("object" === typeof a && (c = k(a))) { var f = c[0]; arguments[0] = c[1]; u(e.attribs, f); e.setPaths(e.attribs) } return l.prototype.attr.apply(e, arguments) }; e.animate = function (a, c, f) {
                        var r = this.attribs, v = "data-" + Math.random().toString(26).substring(2, 9); delete a.center; delete a.z; delete a.alpha; delete a.beta; var z = D(E(c, this.renderer.globalAnimation));
                        if (z.duration) { c = k(a); e[v] = 0; a[v] = 1; e[v + "Setter"] = t.noop; if (c) { var h = c[0]; z.step = function (a, e) { function c(a) { return r[a] + (E(h[a], r[a]) - r[a]) * e.pos } e.prop === v && e.elem.setPaths(g(r, { x: c("x"), y: c("y"), r: c("r"), innerR: c("innerR"), start: c("start"), end: c("end"), depth: c("depth") })) } } c = z } return l.prototype.animate.call(this, a, c, f)
                    }; e.destroy = function () { this.top.destroy(); this.out.destroy(); this.inn.destroy(); this.side1.destroy(); this.side2.destroy(); return l.prototype.destroy.call(this) }; e.hide = function () {
                        this.top.hide();
                        this.out.hide(); this.inn.hide(); this.side1.hide(); this.side2.hide()
                    }; e.show = function (a) { this.top.show(a); this.out.show(a); this.inn.show(a); this.side1.show(a); this.side2.show(a) }; return e
        }; d.prototype.arc3dPath = function (c) {
            function f(a) { a %= 2 * Math.PI; a > Math.PI && (a = 2 * Math.PI - a); return a } var e = c.x, h = c.y, d = c.start, x = c.end - .00001, b = c.r, r = c.innerR || 0, G = c.depth || 0, v = c.alpha, z = c.beta, H = Math.cos(d), B = Math.sin(d); c = Math.cos(x); var q = Math.sin(x), A = b * Math.cos(z); b *= Math.cos(v); var C = r * Math.cos(z), p = r * Math.cos(v);
            r = G * Math.sin(z); var m = G * Math.sin(v); G = [["M", e + A * H, h + b * B]]; G = G.concat(n(e, h, A, b, d, x, 0, 0)); G.push(["L", e + C * c, h + p * q]); G = G.concat(n(e, h, C, p, x, d, 0, 0)); G.push(["Z"]); var u = 0 < z ? Math.PI / 2 : 0; z = 0 < v ? 0 : Math.PI / 2; u = d > -u ? d : x > -u ? -u : d; var g = x < a - z ? x : d < a - z ? a - z : x, l = 2 * a - z; v = [["M", e + A * y(u), h + b * k(u)]]; v = v.concat(n(e, h, A, b, u, g, 0, 0)); x > l && d < l ? (v.push(["L", e + A * y(g) + r, h + b * k(g) + m]), v = v.concat(n(e, h, A, b, g, l, r, m)), v.push(["L", e + A * y(l), h + b * k(l)]), v = v.concat(n(e, h, A, b, l, x, 0, 0)), v.push(["L", e + A * y(x) + r, h + b * k(x) + m]), v = v.concat(n(e,
                h, A, b, x, l, r, m)), v.push(["L", e + A * y(l), h + b * k(l)]), v = v.concat(n(e, h, A, b, l, g, 0, 0))) : x > a - z && d < a - z && (v.push(["L", e + A * Math.cos(g) + r, h + b * Math.sin(g) + m]), v = v.concat(n(e, h, A, b, g, x, r, m)), v.push(["L", e + A * Math.cos(x), h + b * Math.sin(x)]), v = v.concat(n(e, h, A, b, x, g, 0, 0))); v.push(["L", e + A * Math.cos(g) + r, h + b * Math.sin(g) + m]); v = v.concat(n(e, h, A, b, g, u, r, m)); v.push(["Z"]); z = [["M", e + C * H, h + p * B]]; z = z.concat(n(e, h, C, p, d, x, 0, 0)); z.push(["L", e + C * Math.cos(x) + r, h + p * Math.sin(x) + m]); z = z.concat(n(e, h, C, p, x, d, r, m)); z.push(["Z"]); H = [["M",
                    e + A * H, h + b * B], ["L", e + A * H + r, h + b * B + m], ["L", e + C * H + r, h + p * B + m], ["L", e + C * H, h + p * B], ["Z"]]; e = [["M", e + A * c, h + b * q], ["L", e + A * c + r, h + b * q + m], ["L", e + C * c + r, h + p * q + m], ["L", e + C * c, h + p * q], ["Z"]]; q = Math.atan2(m, -r); h = Math.abs(x + q); c = Math.abs(d + q); d = Math.abs((d + x) / 2 + q); h = f(h); c = f(c); d = f(d); d *= 1E5; x = 1E5 * c; h *= 1E5; return { top: G, zTop: 1E5 * Math.PI + 1, out: v, zOut: Math.max(d, x, h), inn: z, zInn: Math.max(d, x, h), side1: H, zSide1: .99 * h, side2: e, zSide2: .99 * x }
        }
    }); F(b, "parts-3d/Tick3D.js", [b["parts/Utilities.js"]], function (b) {
        var t = b.addEvent, l =
            b.extend, d = b.wrap; return function () {
                function b() { } b.compose = function (n) { t(n, "afterGetLabelPosition", b.onAfterGetLabelPosition); d(n.prototype, "getMarkPath", b.wrapGetMarkPath) }; b.onAfterGetLabelPosition = function (d) { var b = this.axis.axis3D; b && l(d.pos, b.fix3dPosition(d.pos)) }; b.wrapGetMarkPath = function (d) {
                    var b = this.axis.axis3D, n = d.apply(this, [].slice.call(arguments, 1)); if (b) {
                        var m = n[0], u = n[1]; if ("M" === m[0] && "L" === u[0]) return b = [b.fix3dPosition({ x: m[1], y: m[2], z: 0 }), b.fix3dPosition({ x: u[1], y: u[2], z: 0 })],
                            this.axis.chart.renderer.toLineSegments(b)
                    } return n
                }; return b
            }()
    }); F(b, "parts-3d/Axis3D.js", [b["parts/Globals.js"], b["parts/Tick.js"], b["parts-3d/Tick3D.js"], b["parts/Utilities.js"]], function (b, t, l, d) {
        var w = d.addEvent, n = d.merge, q = d.pick, D = d.wrap, m = b.deg2rad, u = b.perspective, g = b.perspective3D, p = b.shapeArea, E = function () {
            function d(a) { this.axis = a } d.prototype.fix3dPosition = function (a, k) {
                var d = this.axis, c = d.chart; if ("colorAxis" === d.coll || !c.chart3d || !c.is3d()) return a; var h = m * c.options.chart.options3d.alpha,
                    b = m * c.options.chart.options3d.beta, y = q(k && d.options.title.position3d, d.options.labels.position3d); k = q(k && d.options.title.skew3d, d.options.labels.skew3d); var f = c.chart3d.frame3d, g = c.plotLeft, e = c.plotWidth + g, n = c.plotTop, l = c.plotHeight + n; c = !1; var t = 0, w = 0, r = { x: 0, y: 1, z: 0 }; a = d.axis3D.swapZ({ x: a.x, y: a.y, z: 0 }); if (d.isZAxis) if (d.opposite) { if (null === f.axes.z.top) return {}; w = a.y - n; a.x = f.axes.z.top.x; a.y = f.axes.z.top.y; g = f.axes.z.top.xDir; c = !f.top.frontFacing } else {
                        if (null === f.axes.z.bottom) return {}; w = a.y - l; a.x =
                            f.axes.z.bottom.x; a.y = f.axes.z.bottom.y; g = f.axes.z.bottom.xDir; c = !f.bottom.frontFacing
                    } else if (d.horiz) if (d.opposite) { if (null === f.axes.x.top) return {}; w = a.y - n; a.y = f.axes.x.top.y; a.z = f.axes.x.top.z; g = f.axes.x.top.xDir; c = !f.top.frontFacing } else { if (null === f.axes.x.bottom) return {}; w = a.y - l; a.y = f.axes.x.bottom.y; a.z = f.axes.x.bottom.z; g = f.axes.x.bottom.xDir; c = !f.bottom.frontFacing } else if (d.opposite) {
                        if (null === f.axes.y.right) return {}; t = a.x - e; a.x = f.axes.y.right.x; a.z = f.axes.y.right.z; g = f.axes.y.right.xDir;
                        g = { x: g.z, y: g.y, z: -g.x }
                    } else { if (null === f.axes.y.left) return {}; t = a.x - g; a.x = f.axes.y.left.x; a.z = f.axes.y.left.z; g = f.axes.y.left.xDir } "chart" !== y && ("flap" === y ? d.horiz ? (b = Math.sin(h), h = Math.cos(h), d.opposite && (b = -b), c && (b = -b), r = { x: g.z * b, y: h, z: -g.x * b }) : g = { x: Math.cos(b), y: 0, z: Math.sin(b) } : "ortho" === y ? d.horiz ? (r = Math.cos(h), y = Math.sin(b) * r, h = -Math.sin(h), b = -r * Math.cos(b), r = { x: g.y * b - g.z * h, y: g.z * y - g.x * b, z: g.x * h - g.y * y }, h = 1 / Math.sqrt(r.x * r.x + r.y * r.y + r.z * r.z), c && (h = -h), r = { x: h * r.x, y: h * r.y, z: h * r.z }) : g = {
                        x: Math.cos(b),
                        y: 0, z: Math.sin(b)
                    } : d.horiz ? r = { x: Math.sin(b) * Math.sin(h), y: Math.cos(h), z: -Math.cos(b) * Math.sin(h) } : g = { x: Math.cos(b), y: 0, z: Math.sin(b) }); a.x += t * g.x + w * r.x; a.y += t * g.y + w * r.y; a.z += t * g.z + w * r.z; c = u([a], d.chart)[0]; k && (0 > p(u([a, { x: a.x + g.x, y: a.y + g.y, z: a.z + g.z }, { x: a.x + r.x, y: a.y + r.y, z: a.z + r.z }], d.chart)) && (g = { x: -g.x, y: -g.y, z: -g.z }), a = u([{ x: a.x, y: a.y, z: a.z }, { x: a.x + g.x, y: a.y + g.y, z: a.z + g.z }, { x: a.x + r.x, y: a.y + r.y, z: a.z + r.z }], d.chart), c.matrix = [a[1].x - a[0].x, a[1].y - a[0].y, a[2].x - a[0].x, a[2].y - a[0].y, c.x, c.y], c.matrix[4] -=
                        c.x * c.matrix[0] + c.y * c.matrix[2], c.matrix[5] -= c.x * c.matrix[1] + c.y * c.matrix[3]); return c
            }; d.prototype.swapZ = function (a, k) { var d = this.axis; return d.isZAxis ? (k = k ? 0 : d.chart.plotLeft, { x: k + a.z, y: a.y, z: a.x - k }) : a }; return d
        }(); return function () {
            function d() { } d.compose = function (a) {
                n(!0, a.defaultOptions, d.defaultOptions); a.keepProps.push("axis3D"); w(a, "init", d.onInit); w(a, "afterSetOptions", d.onAfterSetOptions); w(a, "drawCrosshair", d.onDrawCrosshair); w(a, "destroy", d.onDestroy); a = a.prototype; D(a, "getLinePath", d.wrapGetLinePath);
                D(a, "getPlotBandPath", d.wrapGetPlotBandPath); D(a, "getPlotLinePath", d.wrapGetPlotLinePath); D(a, "getSlotWidth", d.wrapGetSlotWidth); D(a, "getTitlePosition", d.wrapGetTitlePosition); l.compose(t)
            }; d.onAfterSetOptions = function () { var a = this.chart, d = this.options; a.is3d && a.is3d() && "colorAxis" !== this.coll && (d.tickWidth = q(d.tickWidth, 0), d.gridLineWidth = q(d.gridLineWidth, 1)) }; d.onDestroy = function () { ["backFrame", "bottomFrame", "sideFrame"].forEach(function (a) { this[a] && (this[a] = this[a].destroy()) }, this) }; d.onDrawCrosshair =
                function (a) { this.chart.is3d() && "colorAxis" !== this.coll && a.point && (a.point.crosshairPos = this.isXAxis ? a.point.axisXpos : this.len - a.point.axisYpos) }; d.onInit = function () { this.axis3D || (this.axis3D = new E(this)) }; d.wrapGetLinePath = function (a) { return this.chart.is3d() && "colorAxis" !== this.coll ? [] : a.apply(this, [].slice.call(arguments, 1)) }; d.wrapGetPlotBandPath = function (a) {
                    if (!this.chart.is3d() || "colorAxis" === this.coll) return a.apply(this, [].slice.call(arguments, 1)); var d = arguments, b = d[2], c = []; d = this.getPlotLinePath({ value: d[1] });
                    b = this.getPlotLinePath({ value: b }); if (d && b) for (var h = 0; h < d.length; h += 2) { var g = d[h], p = d[h + 1], f = b[h], m = b[h + 1]; "M" === g[0] && "L" === p[0] && "M" === f[0] && "L" === m[0] && c.push(g, p, m, ["L", f[1], f[2]], ["Z"]) } return c
                }; d.wrapGetPlotLinePath = function (a) {
                    var d = this.axis3D, b = this.chart, c = a.apply(this, [].slice.call(arguments, 1)); if ("colorAxis" === this.coll || !b.chart3d || !b.is3d() || null === c) return c; var h = b.options.chart.options3d, g = this.isZAxis ? b.plotWidth : h.depth; h = b.chart3d.frame3d; var p = c[0], f = c[1]; c = []; "M" === p[0] && "L" ===
                        f[0] && (d = [d.swapZ({ x: p[1], y: p[2], z: 0 }), d.swapZ({ x: p[1], y: p[2], z: g }), d.swapZ({ x: f[1], y: f[2], z: 0 }), d.swapZ({ x: f[1], y: f[2], z: g })], this.horiz ? (this.isZAxis ? (h.left.visible && c.push(d[0], d[2]), h.right.visible && c.push(d[1], d[3])) : (h.front.visible && c.push(d[0], d[2]), h.back.visible && c.push(d[1], d[3])), h.top.visible && c.push(d[0], d[1]), h.bottom.visible && c.push(d[2], d[3])) : (h.front.visible && c.push(d[0], d[2]), h.back.visible && c.push(d[1], d[3]), h.left.visible && c.push(d[0], d[1]), h.right.visible && c.push(d[2], d[3])),
                            c = u(c, this.chart, !1)); return b.renderer.toLineSegments(c)
                }; d.wrapGetSlotWidth = function (a, d) {
                    var b = this.chart, c = this.ticks, h = this.gridGroup; if (this.categories && b.frameShapes && b.is3d() && h && d && d.label) {
                        h = h.element.childNodes[0].getBBox(); var k = b.frameShapes.left.getBBox(), p = b.options.chart.options3d; b = { x: b.plotWidth / 2, y: b.plotHeight / 2, z: p.depth / 2, vd: q(p.depth, 1) * q(p.viewDistance, 0) }; var f, m; p = d.pos; var e = c[p - 1]; c = c[p + 1]; 0 !== p && e && e.label.xy && (f = g({ x: e.label.xy.x, y: e.label.xy.y, z: null }, b, b.vd)); c && c.label.xy &&
                            (m = g({ x: c.label.xy.x, y: c.label.xy.y, z: null }, b, b.vd)); c = { x: d.label.xy.x, y: d.label.xy.y, z: null }; c = g(c, b, b.vd); return Math.abs(f ? c.x - f.x : m ? m.x - c.x : h.x - k.x)
                    } return a.apply(this, [].slice.call(arguments, 1))
                }; d.wrapGetTitlePosition = function (a) { var d = a.apply(this, [].slice.call(arguments, 1)); return this.axis3D ? this.axis3D.fix3dPosition(d, !0) : d }; d.defaultOptions = { labels: { position3d: "offset", skew3d: !1 }, title: { position3d: null, skew3d: null } }; return d
        }()
    }); F(b, "parts-3d/ZAxis.js", [b["parts/Axis.js"], b["parts/Utilities.js"]],
        function (b, t) {
            var l = this && this.__extends || function () { var d = function (b, p) { d = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b } || function (d, b) { for (var a in b) b.hasOwnProperty(a) && (d[a] = b[a]) }; return d(b, p) }; return function (b, p) { function g() { this.constructor = b } d(b, p); b.prototype = null === p ? Object.create(p) : (g.prototype = p.prototype, new g) } }(), d = t.addEvent, w = t.merge, n = t.pick, q = t.splat, D = function () {
                function b() { } b.compose = function (g) {
                    d(g, "afterGetAxes", b.onAfterGetAxes); g =
                        g.prototype; g.addZAxis = b.wrapAddZAxis; g.collectionsWithInit.zAxis = [g.addZAxis]; g.collectionsWithUpdate.push("zAxis")
                }; b.onAfterGetAxes = function () { var d = this, b = this.options; b = b.zAxis = q(b.zAxis || {}); d.is3d() && (d.zAxis = [], b.forEach(function (b, g) { b.index = g; b.isX = !0; d.addZAxis(b).setScale() })) }; b.wrapAddZAxis = function (d) { return new m(this, d) }; return b
            }(), m = function (d) {
                function b(b, g) { b = d.call(this, b, g) || this; b.isZAxis = !0; return b } l(b, d); b.prototype.getSeriesExtremes = function () {
                    var d = this, b = d.chart; d.hasVisibleSeries =
                        !1; d.dataMin = d.dataMax = d.ignoreMinPadding = d.ignoreMaxPadding = void 0; d.stacking && d.stacking.buildStacks(); d.series.forEach(function (g) { !g.visible && b.options.chart && b.options.chart.ignoreHiddenSeries || (d.hasVisibleSeries = !0, g = g.zData, g.length && (d.dataMin = Math.min(n(d.dataMin, g[0]), Math.min.apply(null, g)), d.dataMax = Math.max(n(d.dataMax, g[0]), Math.max.apply(null, g)))) })
                }; b.prototype.setAxisSize = function () {
                    var b = this.chart; d.prototype.setAxisSize.call(this); this.width = this.len = b.options.chart && b.options.chart.options3d &&
                        b.options.chart.options3d.depth || 0; this.right = b.chartWidth - this.width - this.left
                }; b.prototype.setOptions = function (b) { b = w({ offset: 0, lineWidth: 0 }, b); d.prototype.setOptions.call(this, b); this.coll = "zAxis" }; b.ZChartComposition = D; return b
            }(b); return m
        }); F(b, "parts-3d/Chart3D.js", [b["parts/Axis.js"], b["parts-3d/Axis3D.js"], b["parts/Chart.js"], b["parts/Globals.js"], b["parts/Options.js"], b["parts/Utilities.js"], b["parts-3d/ZAxis.js"]], function (b, t, l, d, w, n, q) {
            var D = w.defaultOptions, m = n.addEvent; w = n.Fx; var u =
                n.isArray, g = n.merge, p = n.pick, E = n.wrap, y = d.perspective, a; (function (a) {
                    function b(a) { this.is3d() && "scatter" === a.options.type && (a.options.type = "scatter3d") } function c() {
                        if (this.chart3d && this.is3d()) {
                            var a = this.renderer, b = this.options.chart.options3d, c = this.chart3d.get3dFrame(), e = this.plotLeft, h = this.plotLeft + this.plotWidth, f = this.plotTop, k = this.plotTop + this.plotHeight; b = b.depth; var g = e - (c.left.visible ? c.left.size : 0), m = h + (c.right.visible ? c.right.size : 0), n = f - (c.top.visible ? c.top.size : 0), q = k + (c.bottom.visible ?
                                c.bottom.size : 0), l = 0 - (c.front.visible ? c.front.size : 0), p = b + (c.back.visible ? c.back.size : 0), y = this.hasRendered ? "animate" : "attr"; this.chart3d.frame3d = c; this.frameShapes || (this.frameShapes = { bottom: a.polyhedron().add(), top: a.polyhedron().add(), left: a.polyhedron().add(), right: a.polyhedron().add(), back: a.polyhedron().add(), front: a.polyhedron().add() }); this.frameShapes.bottom[y]({
                                    "class": "highcharts-3d-frame highcharts-3d-frame-bottom", zIndex: c.bottom.frontFacing ? -1E3 : 1E3, faces: [{
                                        fill: d.color(c.bottom.color).brighten(.1).get(),
                                        vertexes: [{ x: g, y: q, z: l }, { x: m, y: q, z: l }, { x: m, y: q, z: p }, { x: g, y: q, z: p }], enabled: c.bottom.visible
                                    }, { fill: d.color(c.bottom.color).brighten(.1).get(), vertexes: [{ x: e, y: k, z: b }, { x: h, y: k, z: b }, { x: h, y: k, z: 0 }, { x: e, y: k, z: 0 }], enabled: c.bottom.visible }, { fill: d.color(c.bottom.color).brighten(-.1).get(), vertexes: [{ x: g, y: q, z: l }, { x: g, y: q, z: p }, { x: e, y: k, z: b }, { x: e, y: k, z: 0 }], enabled: c.bottom.visible && !c.left.visible }, {
                                        fill: d.color(c.bottom.color).brighten(-.1).get(), vertexes: [{ x: m, y: q, z: p }, { x: m, y: q, z: l }, { x: h, y: k, z: 0 }, {
                                            x: h,
                                            y: k, z: b
                                        }], enabled: c.bottom.visible && !c.right.visible
                                    }, { fill: d.color(c.bottom.color).get(), vertexes: [{ x: m, y: q, z: l }, { x: g, y: q, z: l }, { x: e, y: k, z: 0 }, { x: h, y: k, z: 0 }], enabled: c.bottom.visible && !c.front.visible }, { fill: d.color(c.bottom.color).get(), vertexes: [{ x: g, y: q, z: p }, { x: m, y: q, z: p }, { x: h, y: k, z: b }, { x: e, y: k, z: b }], enabled: c.bottom.visible && !c.back.visible }]
                                }); this.frameShapes.top[y]({
                                    "class": "highcharts-3d-frame highcharts-3d-frame-top", zIndex: c.top.frontFacing ? -1E3 : 1E3, faces: [{
                                        fill: d.color(c.top.color).brighten(.1).get(),
                                        vertexes: [{ x: g, y: n, z: p }, { x: m, y: n, z: p }, { x: m, y: n, z: l }, { x: g, y: n, z: l }], enabled: c.top.visible
                                    }, { fill: d.color(c.top.color).brighten(.1).get(), vertexes: [{ x: e, y: f, z: 0 }, { x: h, y: f, z: 0 }, { x: h, y: f, z: b }, { x: e, y: f, z: b }], enabled: c.top.visible }, { fill: d.color(c.top.color).brighten(-.1).get(), vertexes: [{ x: g, y: n, z: p }, { x: g, y: n, z: l }, { x: e, y: f, z: 0 }, { x: e, y: f, z: b }], enabled: c.top.visible && !c.left.visible }, {
                                        fill: d.color(c.top.color).brighten(-.1).get(), vertexes: [{ x: m, y: n, z: l }, { x: m, y: n, z: p }, { x: h, y: f, z: b }, { x: h, y: f, z: 0 }], enabled: c.top.visible &&
                                            !c.right.visible
                                    }, { fill: d.color(c.top.color).get(), vertexes: [{ x: g, y: n, z: l }, { x: m, y: n, z: l }, { x: h, y: f, z: 0 }, { x: e, y: f, z: 0 }], enabled: c.top.visible && !c.front.visible }, { fill: d.color(c.top.color).get(), vertexes: [{ x: m, y: n, z: p }, { x: g, y: n, z: p }, { x: e, y: f, z: b }, { x: h, y: f, z: b }], enabled: c.top.visible && !c.back.visible }]
                                }); this.frameShapes.left[y]({
                                    "class": "highcharts-3d-frame highcharts-3d-frame-left", zIndex: c.left.frontFacing ? -1E3 : 1E3, faces: [{
                                        fill: d.color(c.left.color).brighten(.1).get(), vertexes: [{ x: g, y: q, z: l }, {
                                            x: e,
                                            y: k, z: 0
                                        }, { x: e, y: k, z: b }, { x: g, y: q, z: p }], enabled: c.left.visible && !c.bottom.visible
                                    }, { fill: d.color(c.left.color).brighten(.1).get(), vertexes: [{ x: g, y: n, z: p }, { x: e, y: f, z: b }, { x: e, y: f, z: 0 }, { x: g, y: n, z: l }], enabled: c.left.visible && !c.top.visible }, { fill: d.color(c.left.color).brighten(-.1).get(), vertexes: [{ x: g, y: q, z: p }, { x: g, y: n, z: p }, { x: g, y: n, z: l }, { x: g, y: q, z: l }], enabled: c.left.visible }, { fill: d.color(c.left.color).brighten(-.1).get(), vertexes: [{ x: e, y: f, z: b }, { x: e, y: k, z: b }, { x: e, y: k, z: 0 }, { x: e, y: f, z: 0 }], enabled: c.left.visible },
                                    { fill: d.color(c.left.color).get(), vertexes: [{ x: g, y: q, z: l }, { x: g, y: n, z: l }, { x: e, y: f, z: 0 }, { x: e, y: k, z: 0 }], enabled: c.left.visible && !c.front.visible }, { fill: d.color(c.left.color).get(), vertexes: [{ x: g, y: n, z: p }, { x: g, y: q, z: p }, { x: e, y: k, z: b }, { x: e, y: f, z: b }], enabled: c.left.visible && !c.back.visible }]
                                }); this.frameShapes.right[y]({
                                    "class": "highcharts-3d-frame highcharts-3d-frame-right", zIndex: c.right.frontFacing ? -1E3 : 1E3, faces: [{
                                        fill: d.color(c.right.color).brighten(.1).get(), vertexes: [{ x: m, y: q, z: p }, { x: h, y: k, z: b }, {
                                            x: h,
                                            y: k, z: 0
                                        }, { x: m, y: q, z: l }], enabled: c.right.visible && !c.bottom.visible
                                    }, { fill: d.color(c.right.color).brighten(.1).get(), vertexes: [{ x: m, y: n, z: l }, { x: h, y: f, z: 0 }, { x: h, y: f, z: b }, { x: m, y: n, z: p }], enabled: c.right.visible && !c.top.visible }, { fill: d.color(c.right.color).brighten(-.1).get(), vertexes: [{ x: h, y: f, z: 0 }, { x: h, y: k, z: 0 }, { x: h, y: k, z: b }, { x: h, y: f, z: b }], enabled: c.right.visible }, { fill: d.color(c.right.color).brighten(-.1).get(), vertexes: [{ x: m, y: q, z: l }, { x: m, y: n, z: l }, { x: m, y: n, z: p }, { x: m, y: q, z: p }], enabled: c.right.visible },
                                    { fill: d.color(c.right.color).get(), vertexes: [{ x: m, y: n, z: l }, { x: m, y: q, z: l }, { x: h, y: k, z: 0 }, { x: h, y: f, z: 0 }], enabled: c.right.visible && !c.front.visible }, { fill: d.color(c.right.color).get(), vertexes: [{ x: m, y: q, z: p }, { x: m, y: n, z: p }, { x: h, y: f, z: b }, { x: h, y: k, z: b }], enabled: c.right.visible && !c.back.visible }]
                                }); this.frameShapes.back[y]({
                                    "class": "highcharts-3d-frame highcharts-3d-frame-back", zIndex: c.back.frontFacing ? -1E3 : 1E3, faces: [{
                                        fill: d.color(c.back.color).brighten(.1).get(), vertexes: [{ x: m, y: q, z: p }, { x: g, y: q, z: p }, {
                                            x: e,
                                            y: k, z: b
                                        }, { x: h, y: k, z: b }], enabled: c.back.visible && !c.bottom.visible
                                    }, { fill: d.color(c.back.color).brighten(.1).get(), vertexes: [{ x: g, y: n, z: p }, { x: m, y: n, z: p }, { x: h, y: f, z: b }, { x: e, y: f, z: b }], enabled: c.back.visible && !c.top.visible }, { fill: d.color(c.back.color).brighten(-.1).get(), vertexes: [{ x: g, y: q, z: p }, { x: g, y: n, z: p }, { x: e, y: f, z: b }, { x: e, y: k, z: b }], enabled: c.back.visible && !c.left.visible }, {
                                        fill: d.color(c.back.color).brighten(-.1).get(), vertexes: [{ x: m, y: n, z: p }, { x: m, y: q, z: p }, { x: h, y: k, z: b }, { x: h, y: f, z: b }], enabled: c.back.visible &&
                                            !c.right.visible
                                    }, { fill: d.color(c.back.color).get(), vertexes: [{ x: e, y: f, z: b }, { x: h, y: f, z: b }, { x: h, y: k, z: b }, { x: e, y: k, z: b }], enabled: c.back.visible }, { fill: d.color(c.back.color).get(), vertexes: [{ x: g, y: q, z: p }, { x: m, y: q, z: p }, { x: m, y: n, z: p }, { x: g, y: n, z: p }], enabled: c.back.visible }]
                                }); this.frameShapes.front[y]({
                                    "class": "highcharts-3d-frame highcharts-3d-frame-front", zIndex: c.front.frontFacing ? -1E3 : 1E3, faces: [{
                                        fill: d.color(c.front.color).brighten(.1).get(), vertexes: [{ x: g, y: q, z: l }, { x: m, y: q, z: l }, { x: h, y: k, z: 0 }, {
                                            x: e,
                                            y: k, z: 0
                                        }], enabled: c.front.visible && !c.bottom.visible
                                    }, { fill: d.color(c.front.color).brighten(.1).get(), vertexes: [{ x: m, y: n, z: l }, { x: g, y: n, z: l }, { x: e, y: f, z: 0 }, { x: h, y: f, z: 0 }], enabled: c.front.visible && !c.top.visible }, { fill: d.color(c.front.color).brighten(-.1).get(), vertexes: [{ x: g, y: n, z: l }, { x: g, y: q, z: l }, { x: e, y: k, z: 0 }, { x: e, y: f, z: 0 }], enabled: c.front.visible && !c.left.visible }, {
                                        fill: d.color(c.front.color).brighten(-.1).get(), vertexes: [{ x: m, y: q, z: l }, { x: m, y: n, z: l }, { x: h, y: f, z: 0 }, { x: h, y: k, z: 0 }], enabled: c.front.visible &&
                                            !c.right.visible
                                    }, { fill: d.color(c.front.color).get(), vertexes: [{ x: h, y: f, z: 0 }, { x: e, y: f, z: 0 }, { x: e, y: k, z: 0 }, { x: h, y: k, z: 0 }], enabled: c.front.visible }, { fill: d.color(c.front.color).get(), vertexes: [{ x: m, y: q, z: l }, { x: g, y: q, z: l }, { x: g, y: n, z: l }, { x: m, y: n, z: l }], enabled: c.front.visible }]
                                })
                        }
                    } function h() {
                    this.styledMode && (this.renderer.definition({ tagName: "style", textContent: ".highcharts-3d-top{filter: url(#highcharts-brighter)}\n.highcharts-3d-side{filter: url(#highcharts-darker)}\n" }), [{ name: "darker", slope: .6 },
                    { name: "brighter", slope: 1.4 }].forEach(function (a) { this.renderer.definition({ tagName: "filter", id: "highcharts-" + a.name, children: [{ tagName: "feComponentTransfer", children: [{ tagName: "feFuncR", type: "linear", slope: a.slope }, { tagName: "feFuncG", type: "linear", slope: a.slope }, { tagName: "feFuncB", type: "linear", slope: a.slope }] }] }) }, this))
                    } function k() { var a = this.options; this.is3d() && (a.series || []).forEach(function (c) { "scatter" === (c.type || a.chart.type || a.chart.defaultSeriesType) && (c.type = "scatter3d") }) } function n() {
                        var a =
                            this.options.chart.options3d; if (this.chart3d && this.is3d()) { a && (a.alpha = a.alpha % 360 + (0 <= a.alpha ? 0 : 360), a.beta = a.beta % 360 + (0 <= a.beta ? 0 : 360)); var c = this.inverted, b = this.clipBox, d = this.margin; b[c ? "y" : "x"] = -(d[3] || 0); b[c ? "x" : "y"] = -(d[0] || 0); b[c ? "height" : "width"] = this.chartWidth + (d[3] || 0) + (d[1] || 0); b[c ? "width" : "height"] = this.chartHeight + (d[0] || 0) + (d[2] || 0); this.scale3d = 1; !0 === a.fitToPlot && (this.scale3d = this.chart3d.getScale(a.depth)); this.chart3d.frame3d = this.chart3d.get3dFrame() }
                    } function f() {
                        this.is3d() &&
                        (this.isDirtyBox = !0)
                    } function q() { this.chart3d && this.is3d() && (this.chart3d.frame3d = this.chart3d.get3dFrame()) } function e() { this.chart3d || (this.chart3d = new L(this)) } function l(a) { return this.is3d() || a.apply(this, [].slice.call(arguments, 1)) } function w(a) { var c = this.series.length; if (this.is3d()) for (; c--;)a = this.series[c], a.translate(), a.render(); else a.call(this) } function t(a) { a.apply(this, [].slice.call(arguments, 1)); this.is3d() && (this.container.className += " highcharts-3d-chart") } var L = function () {
                        function a(a) {
                        this.frame3d =
                            void 0; this.chart = a
                        } a.prototype.get3dFrame = function () {
                            var a = this.chart, c = a.options.chart.options3d, b = c.frame, e = a.plotLeft, h = a.plotLeft + a.plotWidth, f = a.plotTop, k = a.plotTop + a.plotHeight, g = c.depth, m = function (c) { c = d.shapeArea3d(c, a); return .5 < c ? 1 : -.5 > c ? -1 : 0 }, n = m([{ x: e, y: k, z: g }, { x: h, y: k, z: g }, { x: h, y: k, z: 0 }, { x: e, y: k, z: 0 }]), q = m([{ x: e, y: f, z: 0 }, { x: h, y: f, z: 0 }, { x: h, y: f, z: g }, { x: e, y: f, z: g }]), l = m([{ x: e, y: f, z: 0 }, { x: e, y: f, z: g }, { x: e, y: k, z: g }, { x: e, y: k, z: 0 }]), r = m([{ x: h, y: f, z: g }, { x: h, y: f, z: 0 }, { x: h, y: k, z: 0 }, { x: h, y: k, z: g }]),
                            x = m([{ x: e, y: k, z: 0 }, { x: h, y: k, z: 0 }, { x: h, y: f, z: 0 }, { x: e, y: f, z: 0 }]); m = m([{ x: e, y: f, z: g }, { x: h, y: f, z: g }, { x: h, y: k, z: g }, { x: e, y: k, z: g }]); var u = !1, w = !1, t = !1, D = !1;[].concat(a.xAxis, a.yAxis, a.zAxis).forEach(function (a) { a && (a.horiz ? a.opposite ? w = !0 : u = !0 : a.opposite ? D = !0 : t = !0) }); var J = function (a, c, b) {
                                for (var d = ["size", "color", "visible"], e = {}, h = 0; h < d.length; h++)for (var f = d[h], k = 0; k < a.length; k++)if ("object" === typeof a[k]) { var g = a[k][f]; if ("undefined" !== typeof g && null !== g) { e[f] = g; break } } a = b; !0 === e.visible || !1 === e.visible ?
                                    a = e.visible : "auto" === e.visible && (a = 0 < c); return { size: p(e.size, 1), color: p(e.color, "none"), frontFacing: 0 < c, visible: a }
                            }; b = { axes: {}, bottom: J([b.bottom, b.top, b], n, u), top: J([b.top, b.bottom, b], q, w), left: J([b.left, b.right, b.side, b], l, t), right: J([b.right, b.left, b.side, b], r, D), back: J([b.back, b.front, b], m, !0), front: J([b.front, b.back, b], x, !1) }; "auto" === c.axisLabelPosition ? (r = function (a, c) { return a.visible !== c.visible || a.visible && c.visible && a.frontFacing !== c.frontFacing }, c = [], r(b.left, b.front) && c.push({
                                y: (f + k) /
                                    2, x: e, z: 0, xDir: { x: 1, y: 0, z: 0 }
                            }), r(b.left, b.back) && c.push({ y: (f + k) / 2, x: e, z: g, xDir: { x: 0, y: 0, z: -1 } }), r(b.right, b.front) && c.push({ y: (f + k) / 2, x: h, z: 0, xDir: { x: 0, y: 0, z: 1 } }), r(b.right, b.back) && c.push({ y: (f + k) / 2, x: h, z: g, xDir: { x: -1, y: 0, z: 0 } }), n = [], r(b.bottom, b.front) && n.push({ x: (e + h) / 2, y: k, z: 0, xDir: { x: 1, y: 0, z: 0 } }), r(b.bottom, b.back) && n.push({ x: (e + h) / 2, y: k, z: g, xDir: { x: -1, y: 0, z: 0 } }), q = [], r(b.top, b.front) && q.push({ x: (e + h) / 2, y: f, z: 0, xDir: { x: 1, y: 0, z: 0 } }), r(b.top, b.back) && q.push({ x: (e + h) / 2, y: f, z: g, xDir: { x: -1, y: 0, z: 0 } }),
                                l = [], r(b.bottom, b.left) && l.push({ z: (0 + g) / 2, y: k, x: e, xDir: { x: 0, y: 0, z: -1 } }), r(b.bottom, b.right) && l.push({ z: (0 + g) / 2, y: k, x: h, xDir: { x: 0, y: 0, z: 1 } }), k = [], r(b.top, b.left) && k.push({ z: (0 + g) / 2, y: f, x: e, xDir: { x: 0, y: 0, z: -1 } }), r(b.top, b.right) && k.push({ z: (0 + g) / 2, y: f, x: h, xDir: { x: 0, y: 0, z: 1 } }), e = function (c, b, d) { if (0 === c.length) return null; if (1 === c.length) return c[0]; for (var e = 0, h = y(c, a, !1), f = 1; f < h.length; f++)d * h[f][b] > d * h[e][b] ? e = f : d * h[f][b] === d * h[e][b] && h[f].z < h[e].z && (e = f); return c[e] }, b.axes = {
                                    y: {
                                        left: e(c, "x", -1),
                                        right: e(c, "x", 1)
                                    }, x: { top: e(q, "y", -1), bottom: e(n, "y", 1) }, z: { top: e(k, "y", -1), bottom: e(l, "y", 1) }
                                }) : b.axes = { y: { left: { x: e, z: 0, xDir: { x: 1, y: 0, z: 0 } }, right: { x: h, z: 0, xDir: { x: 0, y: 0, z: 1 } } }, x: { top: { y: f, z: 0, xDir: { x: 1, y: 0, z: 0 } }, bottom: { y: k, z: 0, xDir: { x: 1, y: 0, z: 0 } } }, z: { top: { x: t ? h : e, y: f, xDir: t ? { x: 0, y: 0, z: 1 } : { x: 0, y: 0, z: -1 } }, bottom: { x: t ? h : e, y: k, xDir: t ? { x: 0, y: 0, z: 1 } : { x: 0, y: 0, z: -1 } } } }; return b
                        }; a.prototype.getScale = function (a) {
                            var c = this.chart, b = c.plotLeft, d = c.plotWidth + b, e = c.plotTop, h = c.plotHeight + e, f = b + c.plotWidth / 2,
                            k = e + c.plotHeight / 2, g = Number.MAX_VALUE, m = -Number.MAX_VALUE, n = Number.MAX_VALUE, q = -Number.MAX_VALUE, l = 1; var p = [{ x: b, y: e, z: 0 }, { x: b, y: e, z: a }];[0, 1].forEach(function (a) { p.push({ x: d, y: p[a].y, z: p[a].z }) });[0, 1, 2, 3].forEach(function (a) { p.push({ x: p[a].x, y: h, z: p[a].z }) }); p = y(p, c, !1); p.forEach(function (a) { g = Math.min(g, a.x); m = Math.max(m, a.x); n = Math.min(n, a.y); q = Math.max(q, a.y) }); b > g && (l = Math.min(l, 1 - Math.abs((b + f) / (g + f)) % 1)); d < m && (l = Math.min(l, (d - f) / (m - f))); e > n && (l = 0 > n ? Math.min(l, (e + k) / (-n + e + k)) : Math.min(l, 1 -
                                (e + k) / (n + k) % 1)); h < q && (l = Math.min(l, Math.abs((h - k) / (q - k)))); return l
                        }; return a
                    }(); a.Composition = L; a.defaultOptions = { chart: { options3d: { enabled: !1, alpha: 0, beta: 0, depth: 100, fitToPlot: !0, viewDistance: 25, axisLabelPosition: null, frame: { visible: "default", size: 1, bottom: {}, top: {}, left: {}, right: {}, back: {}, front: {} } } } }; a.compose = function (p, y) {
                        var r = p.prototype; y = y.prototype; r.is3d = function () { return this.options.chart.options3d && this.options.chart.options3d.enabled }; r.propsRequireDirtyBox.push("chart.options3d");
                        r.propsRequireUpdateSeries.push("chart.options3d"); y.matrixSetter = function () { if (1 > this.pos && (u(this.start) || u(this.end))) { var a = this.start || [1, 0, 0, 1, 0, 0], c = this.end || [1, 0, 0, 1, 0, 0]; var b = []; for (var d = 0; 6 > d; d++)b.push(this.pos * c[d] + (1 - this.pos) * a[d]) } else b = this.end; this.elem.attr(this.prop, b, null, !0) }; g(!0, D, a.defaultOptions); m(p, "init", e); m(p, "addSeries", b); m(p, "afterDrawChartBox", c); m(p, "afterGetContainer", h); m(p, "afterInit", k); m(p, "afterSetChartSize", n); m(p, "beforeRedraw", f); m(p, "beforeRender",
                            q); E(d.Chart.prototype, "isInsidePlot", l); E(p, "renderSeries", w); E(p, "setClassName", t)
                    }
                })(a || (a = {})); a.compose(l, w); q.ZChartComposition.compose(l); t.compose(b); ""; return a
        }); F(b, "parts-3d/Series.js", [b["parts/Globals.js"], b["parts/Utilities.js"]], function (b, t) {
            var l = t.addEvent, d = t.pick, w = b.perspective; l(b.Series, "afterTranslate", function () { this.chart.is3d() && this.translate3dPoints() }); b.Series.prototype.translate3dPoints = function () {
                var b = this.chart, l = d(this.zAxis, b.options.zAxis[0]), t = [], m; for (m = 0; m <
                    this.data.length; m++) { var u = this.data[m]; if (l && l.translate) { var g = l.logarithmic && l.val2lin ? l.val2lin(u.z) : u.z; u.plotZ = l.translate(g); u.isInside = u.isInside ? g >= l.min && g <= l.max : !1 } else u.plotZ = 0; u.axisXpos = u.plotX; u.axisYpos = u.plotY; u.axisZpos = u.plotZ; t.push({ x: u.plotX, y: u.plotY, z: u.plotZ }) } b = w(t, b, !0); for (m = 0; m < this.data.length; m++)u = this.data[m], l = b[m], u.plotX = l.x, u.plotY = l.y, u.plotZ = l.z
            }
        }); F(b, "parts-3d/Column.js", [b["parts/Globals.js"], b["parts/Stacking.js"], b["parts/Utilities.js"]], function (b, t,
            l) {
                function d(b, a) { var d = b.series, g = {}, c, h = 1; d.forEach(function (b) { c = m(b.options.stack, a ? 0 : d.length - 1 - b.index); g[c] ? g[c].series.push(b) : (g[c] = { series: [b], position: h }, h++) }); g.totalStacks = h + 1; return g } function w(b) { var a = b.apply(this, [].slice.call(arguments, 1)); this.chart.is3d && this.chart.is3d() && (a.stroke = this.options.edgeColor || a.fill, a["stroke-width"] = m(this.options.edgeWidth, 1)); return a } function n(b, a, d) {
                    var k = this.chart.is3d && this.chart.is3d(); k && (this.options.inactiveOtherPoints = !0); b.call(this,
                        a, d); k && (this.options.inactiveOtherPoints = !1)
                } function q(b) { for (var a = [], d = 1; d < arguments.length; d++)a[d - 1] = arguments[d]; return this.series.chart.is3d() ? this.graphic && "g" !== this.graphic.element.nodeName : b.apply(this, a) } var D = l.addEvent, m = l.pick; l = l.wrap; var u = b.perspective, g = b.Series, p = b.seriesTypes, E = b.svg; l(p.column.prototype, "translate", function (b) { b.apply(this, [].slice.call(arguments, 1)); this.chart.is3d() && this.translate3dShapes() }); l(b.Series.prototype, "justifyDataLabel", function (b) {
                    return arguments[2].outside3dPlot ?
                        !1 : b.apply(this, [].slice.call(arguments, 1))
                }); p.column.prototype.translate3dPoints = function () { }; p.column.prototype.translate3dShapes = function () {
                    var b = this, a = b.chart, d = b.options, g = d.depth, c = (d.stacking ? d.stack || 0 : b.index) * (g + (d.groupZPadding || 1)), h = b.borderWidth % 2 ? .5 : 0, l; a.inverted && !b.yAxis.reversed && (h *= -1); !1 !== d.grouping && (c = 0); c += d.groupZPadding || 1; b.data.forEach(function (d) {
                    d.outside3dPlot = null; if (null !== d.y) {
                        var f = d.shapeArgs, k = d.tooltipPos, e;[["x", "width"], ["y", "height"]].forEach(function (a) {
                            e =
                            f[a[0]] - h; 0 > e && (f[a[1]] += f[a[0]] + h, f[a[0]] = -h, e = 0); e + f[a[1]] > b[a[0] + "Axis"].len && 0 !== f[a[1]] && (f[a[1]] = b[a[0] + "Axis"].len - f[a[0]]); if (0 !== f[a[1]] && (f[a[0]] >= b[a[0] + "Axis"].len || f[a[0]] + f[a[1]] <= h)) { for (var c in f) f[c] = 0; d.outside3dPlot = !0 }
                        }); "rect" === d.shapeType && (d.shapeType = "cuboid"); f.z = c; f.depth = g; f.insidePlotArea = !0; l = { x: f.x + f.width / 2, y: f.y, z: c + g / 2 }; a.inverted && (l.x = f.height, l.y = d.clientX); d.plot3d = u([l], a, !0, !1)[0]; k = u([{ x: k[0], y: k[1], z: c + g / 2 }], a, !0, !1)[0]; d.tooltipPos = [k.x, k.y]
                    }
                    }); b.z = c
                }; l(p.column.prototype,
                    "animate", function (b) {
                        if (this.chart.is3d()) { var a = arguments[1], d = this.yAxis, g = this, c = this.yAxis.reversed; E && (a ? g.data.forEach(function (a) { null !== a.y && (a.height = a.shapeArgs.height, a.shapey = a.shapeArgs.y, a.shapeArgs.height = 1, c || (a.shapeArgs.y = a.stackY ? a.plotY + d.translate(a.stackY) : a.plotY + (a.negative ? -a.height : a.height))) }) : (g.data.forEach(function (a) { null !== a.y && (a.shapeArgs.height = a.height, a.shapeArgs.y = a.shapey, a.graphic && a.graphic.animate(a.shapeArgs, g.options.animation)) }), this.drawDataLabels())) } else b.apply(this,
                            [].slice.call(arguments, 1))
                    }); l(p.column.prototype, "plotGroup", function (b, a, d, g, c, h) { "dataLabelsGroup" !== a && this.chart.is3d() && (this[a] && delete this[a], h && (this.chart.columnGroup || (this.chart.columnGroup = this.chart.renderer.g("columnGroup").add(h)), this[a] = this.chart.columnGroup, this.chart.columnGroup.attr(this.getPlotBox()), this[a].survive = !0, "group" === a || "markerGroup" === a)) && (arguments[3] = "visible"); return b.apply(this, Array.prototype.slice.call(arguments, 1)) }); l(p.column.prototype, "setVisible",
                        function (b, a) { var d = this, g; d.chart.is3d() && d.data.forEach(function (b) { g = (b.visible = b.options.visible = a = "undefined" === typeof a ? !m(d.visible, b.visible) : a) ? "visible" : "hidden"; d.options.data[d.data.indexOf(b)] = b.options; b.graphic && b.graphic.attr({ visibility: g }) }); b.apply(this, Array.prototype.slice.call(arguments, 1)) }); p.column.prototype.handle3dGrouping = !0; D(g, "afterInit", function () {
                            if (this.chart.is3d() && this.handle3dGrouping) {
                                var b = this.options, a = b.grouping, g = b.stacking, l = m(this.yAxis.options.reversedStacks,
                                    !0), c = 0; if ("undefined" === typeof a || a) { a = d(this.chart, g); c = b.stack || 0; for (g = 0; g < a[c].series.length && a[c].series[g] !== this; g++); c = 10 * (a.totalStacks - a[c].position) + (l ? g : -g); this.xAxis.reversed || (c = 10 * a.totalStacks - c) } b.depth = b.depth || 25; this.z = this.z || 0; b.zIndex = c
                            }
                        }); l(p.column.prototype, "pointAttribs", w); l(p.column.prototype, "setState", n); l(p.column.prototype.pointClass.prototype, "hasNewShapeType", q); p.columnrange && (l(p.columnrange.prototype, "pointAttribs", w), l(p.columnrange.prototype, "setState", n),
                            l(p.columnrange.prototype.pointClass.prototype, "hasNewShapeType", q), p.columnrange.prototype.plotGroup = p.column.prototype.plotGroup, p.columnrange.prototype.setVisible = p.column.prototype.setVisible); l(g.prototype, "alignDataLabel", function (b, a, d, g, c) {
                                var h = this.chart; g.outside3dPlot = a.outside3dPlot; if (h.is3d() && this.is("column")) {
                                    var k = this.options, l = m(g.inside, !!this.options.stacking), f = h.options.chart.options3d, n = a.pointWidth / 2 || 0; k = { x: c.x + n, y: c.y, z: this.z + k.depth / 2 }; h.inverted && (l && (c.width = 0, k.x +=
                                        a.shapeArgs.height / 2), 90 <= f.alpha && 270 >= f.alpha && (k.y += a.shapeArgs.width)); k = u([k], h, !0, !1)[0]; c.x = k.x - n; c.y = a.outside3dPlot ? -9E9 : k.y
                                } b.apply(this, [].slice.call(arguments, 1))
                            }); l(t.prototype, "getStackBox", function (b, a, d, g, c, h, l, m) {
                                var f = b.apply(this, [].slice.call(arguments, 1)); if (a.is3d() && d.base) {
                                    var k = +d.base.split(",")[0], e = a.series[k]; k = a.options.chart.options3d; e && e instanceof p.column && (e = { x: f.x + (a.inverted ? l : h / 2), y: f.y, z: e.options.depth / 2 }, a.inverted && (f.width = 0, 90 <= k.alpha && 270 >= k.alpha &&
                                        (e.y += h)), e = u([e], a, !0, !1)[0], f.x = e.x - h / 2, f.y = e.y)
                                } return f
                            })
        }); F(b, "parts-3d/Pie.js", [b["parts/Globals.js"], b["parts/Utilities.js"]], function (b, t) {
            var l = t.pick; t = t.wrap; var d = b.deg2rad, w = b.seriesTypes, n = b.svg; t(w.pie.prototype, "translate", function (b) {
                b.apply(this, [].slice.call(arguments, 1)); if (this.chart.is3d()) {
                    var l = this, m = l.options, n = m.depth || 0, g = l.chart.options.chart.options3d, p = g.alpha, q = g.beta, t = m.stacking ? (m.stack || 0) * n : l._i * n; t += n / 2; !1 !== m.grouping && (t = 0); l.data.forEach(function (a) {
                        var b =
                            a.shapeArgs; a.shapeType = "arc3d"; b.z = t; b.depth = .75 * n; b.alpha = p; b.beta = q; b.center = l.center; b = (b.end + b.start) / 2; a.slicedTranslation = { translateX: Math.round(Math.cos(b) * m.slicedOffset * Math.cos(p * d)), translateY: Math.round(Math.sin(b) * m.slicedOffset * Math.cos(p * d)) }
                    })
                }
            }); t(w.pie.prototype.pointClass.prototype, "haloPath", function (b) { var d = arguments; return this.series.chart.is3d() ? [] : b.call(this, d[1]) }); t(w.pie.prototype, "pointAttribs", function (b, d, m) {
                b = b.call(this, d, m); m = this.options; this.chart.is3d() && !this.chart.styledMode &&
                    (b.stroke = m.edgeColor || d.color || this.color, b["stroke-width"] = l(m.edgeWidth, 1)); return b
            }); t(w.pie.prototype, "drawDataLabels", function (b) {
                if (this.chart.is3d()) { var l = this.chart.options.chart.options3d; this.data.forEach(function (b) { var m = b.shapeArgs, g = m.r, n = (m.start + m.end) / 2; b = b.labelPosition; var q = b.connectorPosition, t = -g * (1 - Math.cos((m.alpha || l.alpha) * d)) * Math.sin(n), a = g * (Math.cos((m.beta || l.beta) * d) - 1) * Math.cos(n);[b.natural, q.breakAt, q.touchingSliceAt].forEach(function (b) { b.x += a; b.y += t }) }) } b.apply(this,
                    [].slice.call(arguments, 1))
            }); t(w.pie.prototype, "addPoint", function (b) { b.apply(this, [].slice.call(arguments, 1)); this.chart.is3d() && this.update(this.userOptions, !0) }); t(w.pie.prototype, "animate", function (b) {
                if (this.chart.is3d()) {
                    var d = arguments[1], m = this.options.animation, q = this.center, g = this.group, p = this.markerGroup; n && (!0 === m && (m = {}), d ? (g.oldtranslateX = l(g.oldtranslateX, g.translateX), g.oldtranslateY = l(g.oldtranslateY, g.translateY), d = { translateX: q[0], translateY: q[1], scaleX: .001, scaleY: .001 }, g.attr(d),
                        p && (p.attrSetters = g.attrSetters, p.attr(d))) : (d = { translateX: g.oldtranslateX, translateY: g.oldtranslateY, scaleX: 1, scaleY: 1 }, g.animate(d, m), p && p.animate(d, m)))
                } else b.apply(this, [].slice.call(arguments, 1))
            })
        }); F(b, "parts-3d/Scatter.js", [b["parts/Globals.js"], b["parts/Point.js"], b["parts/Utilities.js"]], function (b, t, l) {
            l = l.seriesType; var d = b.seriesTypes; l("scatter3d", "scatter", { tooltip: { pointFormat: "x: <b>{point.x}</b><br/>y: <b>{point.y}</b><br/>z: <b>{point.z}</b><br/>" } }, {
                pointAttribs: function (l) {
                    var n =
                        d.scatter.prototype.pointAttribs.apply(this, arguments); this.chart.is3d() && l && (n.zIndex = b.pointCameraDistance(l, this.chart)); return n
                }, axisTypes: ["xAxis", "yAxis", "zAxis"], pointArrayMap: ["x", "y", "z"], parallelArrays: ["x", "y", "z"], directTouch: !0
            }, { applyOptions: function () { t.prototype.applyOptions.apply(this, arguments); "undefined" === typeof this.z && (this.z = 0); return this } }); ""
        }); F(b, "parts-3d/VMLAxis3D.js", [b["parts/Utilities.js"]], function (b) {
            var t = b.addEvent, l = function () { return function (b) { this.axis = b } }();
            return function () {
                function b() { } b.compose = function (d) { d.keepProps.push("vml"); t(d, "init", b.onInit); t(d, "render", b.onRender) }; b.onInit = function () { this.vml || (this.vml = new l(this)) }; b.onRender = function () { var b = this.vml; b.sideFrame && (b.sideFrame.css({ zIndex: 0 }), b.sideFrame.front.attr({ fill: b.sideFrame.color })); b.bottomFrame && (b.bottomFrame.css({ zIndex: 1 }), b.bottomFrame.front.attr({ fill: b.bottomFrame.color })); b.backFrame && (b.backFrame.css({ zIndex: 0 }), b.backFrame.front.attr({ fill: b.backFrame.color })) };
                return b
            }()
        }); F(b, "parts-3d/VMLRenderer.js", [b["parts/Axis.js"], b["parts/Globals.js"], b["parts/SVGRenderer.js"], b["parts/Utilities.js"], b["parts-3d/VMLAxis3D.js"]], function (b, t, l, d, w) {
            d = d.setOptions; var n = t.VMLRenderer; n && (d({ animate: !1 }), n.prototype.face3d = l.prototype.face3d, n.prototype.polyhedron = l.prototype.polyhedron, n.prototype.elements3d = l.prototype.elements3d, n.prototype.element3d = l.prototype.element3d, n.prototype.cuboid = l.prototype.cuboid, n.prototype.cuboidPath = l.prototype.cuboidPath, n.prototype.toLinePath =
                l.prototype.toLinePath, n.prototype.toLineSegments = l.prototype.toLineSegments, n.prototype.arc3d = function (b) { b = l.prototype.arc3d.call(this, b); b.css({ zIndex: b.zIndex }); return b }, t.VMLRenderer.prototype.arc3dPath = l.prototype.arc3dPath, w.compose(b))
        }); F(b, "masters/highcharts-3d.src.js", [], function () { })
});
//# sourceMappingURL=highcharts-3d.js.map