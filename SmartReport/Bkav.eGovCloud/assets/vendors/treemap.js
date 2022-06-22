﻿/*
 Highcharts JS v8.0.0 (2019-12-10)

 (c) 2014-2019 Highsoft AS
 Authors: Jon Arild Nygard / Oystein Moseng

 License: www.highcharts.com/license
*/
(function (b) { "object" === typeof module && module.exports ? (b["default"] = b, module.exports = b) : "function" === typeof define && define.amd ? define("highcharts/modules/treemap", ["highcharts"], function (p) { b(p); b.Highcharts = p; return b }) : b("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (b) {
    function p(q, d, b, l) { q.hasOwnProperty(d) || (q[d] = l.apply(null, b)) } b = b ? b._modules : {}; p(b, "mixins/tree-series.js", [b["parts/Globals.js"], b["parts/Utilities.js"]], function (b, d) {
        var q = d.extend, l = d.isArray, y = d.isNumber, h =
            d.isObject, v = d.pick, p = b.merge; return {
                getColor: function (e, k) {
                    var d = k.index, q = k.mapOptionsToLevel, n = k.parentColor, l = k.parentColorIndex, h = k.series, r = k.colors, G = k.siblings, t = h.points, p = h.chart.options.chart, w; if (e) {
                        t = t[e.i]; e = q[e.level] || {}; if (q = t && e.colorByPoint) { var y = t.index % (r ? r.length : p.colorCount); var z = r && r[y] } if (!h.chart.styledMode) { r = t && t.options.color; p = e && e.color; if (w = n) w = (w = e && e.colorVariation) && "brightness" === w.key ? b.color(n).brighten(d / G * w.to).get() : n; w = v(r, p, z, w, h.color) } var C = v(t && t.options.colorIndex,
                            e && e.colorIndex, y, l, k.colorIndex)
                    } return { color: w, colorIndex: C }
                }, getLevelOptions: function (e) {
                    var k = null; if (h(e)) {
                        k = {}; var d = y(e.from) ? e.from : 1; var b = e.levels; var n = {}; var v = h(e.defaults) ? e.defaults : {}; l(b) && (n = b.reduce(function (k, e) { if (h(e) && y(e.level)) { var b = p({}, e); var l = "boolean" === typeof b.levelIsConstant ? b.levelIsConstant : v.levelIsConstant; delete b.levelIsConstant; delete b.level; e = e.level + (l ? 0 : d - 1); h(k[e]) ? q(k[e], b) : k[e] = b } return k }, {})); b = y(e.to) ? e.to : 1; for (e = 0; e <= b; e++)k[e] = p({}, v, h(n[e]) ? n[e] :
                            {})
                    } return k
                }, setTreeValues: function E(k, b) {
                    var d = b.before, l = b.idRoot, h = b.mapIdToNode[l], r = b.points[k.i], p = r && r.options || {}, t = 0, y = []; q(k, { levelDynamic: k.level - (("boolean" === typeof b.levelIsConstant ? b.levelIsConstant : 1) ? 0 : h.level), name: v(r && r.name, ""), visible: l === k.id || ("boolean" === typeof b.visible ? b.visible : !1) }); "function" === typeof d && (k = d(k, b)); k.children.forEach(function (d, l) { var h = q({}, b); q(h, { index: l, siblings: k.children.length, visible: k.visible }); d = E(d, h); y.push(d); d.visible && (t += d.val) }); k.visible =
                        0 < t || k.visible; d = v(p.value, t); q(k, { children: y, childrenTotal: t, isLeaf: k.visible && !t, val: d }); return k
                }, updateRootId: function (b) { if (h(b)) { var d = h(b.options) ? b.options : {}; d = v(b.rootNode, d.rootId, ""); h(b.userOptions) && (b.userOptions.rootId = d); b.rootNode = d } return d }
            }
    }); p(b, "mixins/draw-point.js", [], function () {
        var b = function (b) {
            var d = this, l = d.graphic, q = b.animatableAttribs, h = b.onComplete, p = b.css, A = b.renderer; if (d.shouldDraw()) l || (d.graphic = l = A[b.shapeType](b.shapeArgs).add(b.group)), l.css(p).attr(b.attribs).animate(q,
                b.isNew ? !1 : void 0, h); else if (l) { var e = function () { d.graphic = l = l.destroy(); "function" === typeof h && h() }; Object.keys(q).length ? l.animate(q, void 0, function () { e() }) : e() }
        }; return function (d) { (d.attribs = d.attribs || {})["class"] = this.getClassName(); b.call(this, d) }
    }); p(b, "modules/treemap.src.js", [b["parts/Globals.js"], b["mixins/tree-series.js"], b["mixins/draw-point.js"], b["parts/Utilities.js"]], function (b, d, p, l) {
        var q = l.correctFloat, h = l.defined, v = l.extend, A = l.isArray, e = l.isNumber, k = l.isObject, D = l.isString, E =
            l.objectEach, n = l.pick; l = b.seriesType; var F = b.seriesTypes, I = b.addEvent, r = b.merge, G = b.error, t = b.noop, C = b.fireEvent, w = d.getColor, K = d.getLevelOptions, z = b.Series, L = b.stableSort, J = b.Color, M = function (a, c, f) { f = f || this; E(a, function (b, g) { c.call(f, b, g, a) }) }, B = function (a, c, f) { f = f || this; a = c.call(f, a); !1 !== a && B(a, c, f) }, N = d.updateRootId; l("treemap", "scatter", {
                allowTraversingTree: !1, animationLimit: 250, showInLegend: !1, marker: !1, colorByPoint: !1, dataLabels: {
                    defer: !1, enabled: !0, formatter: function () {
                        var a = this && this.point ?
                            this.point : {}; return D(a.name) ? a.name : ""
                    }, inside: !0, verticalAlign: "middle"
                }, tooltip: { headerFormat: "", pointFormat: "<b>{point.name}</b>: {point.value}<br/>" }, ignoreHiddenPoint: !0, layoutAlgorithm: "sliceAndDice", layoutStartingDirection: "vertical", alternateStartingDirection: !1, levelIsConstant: !0, drillUpButton: { position: { align: "right", x: -10, y: 10 } }, traverseUpButton: { position: { align: "right", x: -10, y: 10 } }, borderColor: "#e6e6e6", borderWidth: 1, colorKey: "colorValue", opacity: .15, states: {
                    hover: {
                        borderColor: "#999999",
                        brightness: F.heatmap ? 0 : .1, halo: !1, opacity: .75, shadow: !1
                    }
                }
            }, {
                pointArrayMap: ["value"], directTouch: !0, optionalAxis: "colorAxis", getSymbol: t, parallelArrays: ["x", "y", "value", "colorValue"], colorKey: "colorValue", trackerGroups: ["group", "dataLabelsGroup"], getListOfParents: function (a, c) {
                    a = A(a) ? a : []; var f = A(c) ? c : []; c = a.reduce(function (a, c, f) { c = n(c.parent, ""); "undefined" === typeof a[c] && (a[c] = []); a[c].push(f); return a }, { "": [] }); M(c, function (a, c, b) {
                    "" !== c && -1 === f.indexOf(c) && (a.forEach(function (a) { b[""].push(a) }),
                        delete b[c])
                    }); return c
                }, getTree: function () { var a = this.data.map(function (a) { return a.id }); a = this.getListOfParents(this.data, a); this.nodeMap = []; return this.buildNode("", -1, 0, a, null) }, hasData: function () { return !!this.processedXData.length }, init: function (a, c) {
                    var f = b.colorMapSeriesMixin; f && (this.colorAttribs = f.colorAttribs); this.eventsToUnbind.push(I(this, "setOptions", function (a) {
                        a = a.userOptions; h(a.allowDrillToNode) && !h(a.allowTraversingTree) && (a.allowTraversingTree = a.allowDrillToNode, delete a.allowDrillToNode);
                        h(a.drillUpButton) && !h(a.traverseUpButton) && (a.traverseUpButton = a.drillUpButton, delete a.drillUpButton)
                    })); z.prototype.init.call(this, a, c); this.options.allowTraversingTree && this.eventsToUnbind.push(I(this, "click", this.onClickDrillToNode))
                }, buildNode: function (a, c, f, b, g) {
                    var x = this, m = [], u = x.points[c], d = 0, H; (b[a] || []).forEach(function (c) { H = x.buildNode(x.points[c].id, c, f + 1, b, a); d = Math.max(H.height + 1, d); m.push(H) }); c = { id: a, i: c, children: m, height: d, level: f, parent: g, visible: !1 }; x.nodeMap[c.id] = c; u && (u.node =
                        c); return c
                }, setTreeValues: function (a) {
                    var c = this, f = c.options, b = c.nodeMap[c.rootNode]; f = "boolean" === typeof f.levelIsConstant ? f.levelIsConstant : !0; var g = 0, x = [], m = c.points[a.i]; a.children.forEach(function (a) { a = c.setTreeValues(a); x.push(a); a.ignore || (g += a.val) }); L(x, function (a, c) { return a.sortIndex - c.sortIndex }); var d = n(m && m.options.value, g); m && (m.value = d); v(a, {
                        children: x, childrenTotal: g, ignore: !(n(m && m.visible, !0) && 0 < d), isLeaf: a.visible && !g, levelDynamic: a.level - (f ? 0 : b.level), name: n(m && m.name, ""), sortIndex: n(m &&
                            m.sortIndex, -d), val: d
                    }); return a
                }, calculateChildrenAreas: function (a, c) {
                    var f = this, b = f.options, g = f.mapOptionsToLevel[a.level + 1], x = n(f[g && g.layoutAlgorithm] && g.layoutAlgorithm, b.layoutAlgorithm), m = b.alternateStartingDirection, d = []; a = a.children.filter(function (a) { return !a.ignore }); g && g.layoutStartingDirection && (c.direction = "vertical" === g.layoutStartingDirection ? 0 : 1); d = f[x](c, a); a.forEach(function (a, b) {
                        b = d[b]; a.values = r(b, { val: a.childrenTotal, direction: m ? 1 - c.direction : c.direction }); a.pointValues = r(b,
                            { x: b.x / f.axisRatio, y: 100 - b.y - b.height, width: b.width / f.axisRatio }); a.children.length && f.calculateChildrenAreas(a, a.values)
                    })
                }, setPointValues: function () {
                    var a = this, c = a.xAxis, f = a.yAxis, b = a.chart.styledMode; a.points.forEach(function (g) {
                        var d = g.node, m = d.pointValues; d = d.visible; if (m && d) {
                            d = m.height; var u = m.width, e = m.x, k = m.y, l = b ? 0 : (a.pointAttribs(g)["stroke-width"] || 0) % 2 / 2; m = Math.round(c.toPixels(e, !0)) - l; u = Math.round(c.toPixels(e + u, !0)) - l; e = Math.round(f.toPixels(k, !0)) - l; d = Math.round(f.toPixels(k + d, !0)) -
                                l; g.shapeArgs = { x: Math.min(m, u), y: Math.min(e, d), width: Math.abs(u - m), height: Math.abs(d - e) }; g.plotX = g.shapeArgs.x + g.shapeArgs.width / 2; g.plotY = g.shapeArgs.y + g.shapeArgs.height / 2
                        } else delete g.plotX, delete g.plotY
                    })
                }, setColorRecursive: function (a, c, b, d, g) {
                    var f = this, m = f && f.chart; m = m && m.options && m.options.colors; if (a) {
                        var u = w(a, { colors: m, index: d, mapOptionsToLevel: f.mapOptionsToLevel, parentColor: c, parentColorIndex: b, series: f, siblings: g }); if (c = f.points[a.i]) c.color = u.color, c.colorIndex = u.colorIndex; (a.children ||
                            []).forEach(function (c, b) { f.setColorRecursive(c, u.color, u.colorIndex, b, a.children.length) })
                    }
                }, algorithmGroup: function (a, c, f, b) {
                this.height = a; this.width = c; this.plot = b; this.startDirection = this.direction = f; this.lH = this.nH = this.lW = this.nW = this.total = 0; this.elArr = []; this.lP = { total: 0, lH: 0, nH: 0, lW: 0, nW: 0, nR: 0, lR: 0, aspectRatio: function (a, c) { return Math.max(a / c, c / a) } }; this.addElement = function (a) {
                    this.lP.total = this.elArr[this.elArr.length - 1]; this.total += a; 0 === this.direction ? (this.lW = this.nW, this.lP.lH = this.lP.total /
                        this.lW, this.lP.lR = this.lP.aspectRatio(this.lW, this.lP.lH), this.nW = this.total / this.height, this.lP.nH = this.lP.total / this.nW, this.lP.nR = this.lP.aspectRatio(this.nW, this.lP.nH)) : (this.lH = this.nH, this.lP.lW = this.lP.total / this.lH, this.lP.lR = this.lP.aspectRatio(this.lP.lW, this.lH), this.nH = this.total / this.width, this.lP.nW = this.lP.total / this.nH, this.lP.nR = this.lP.aspectRatio(this.lP.nW, this.nH)); this.elArr.push(a)
                }; this.reset = function () { this.lW = this.nW = 0; this.elArr = []; this.total = 0 }
                }, algorithmCalcPoints: function (a,
                    c, b, d) { var f, u, m, e, l = b.lW, k = b.lH, h = b.plot, p = 0, n = b.elArr.length - 1; if (c) l = b.nW, k = b.nH; else var r = b.elArr[b.elArr.length - 1]; b.elArr.forEach(function (a) { if (c || p < n) 0 === b.direction ? (f = h.x, u = h.y, m = l, e = a / m) : (f = h.x, u = h.y, e = k, m = a / e), d.push({ x: f, y: u, width: m, height: q(e) }), 0 === b.direction ? h.y += e : h.x += m; p += 1 }); b.reset(); 0 === b.direction ? b.width -= l : b.height -= k; h.y = h.parent.y + (h.parent.height - b.height); h.x = h.parent.x + (h.parent.width - b.width); a && (b.direction = 1 - b.direction); c || b.addElement(r) }, algorithmLowAspectRatio: function (a,
                        c, b) { var f = [], g = this, d, e = { x: c.x, y: c.y, parent: c }, h = 0, l = b.length - 1, k = new this.algorithmGroup(c.height, c.width, c.direction, e); b.forEach(function (b) { d = b.val / c.val * c.height * c.width; k.addElement(d); k.lP.nR > k.lP.lR && g.algorithmCalcPoints(a, !1, k, f, e); h === l && g.algorithmCalcPoints(a, !0, k, f, e); h += 1 }); return f }, algorithmFill: function (a, c, b) {
                            var f = [], g, d = c.direction, e = c.x, k = c.y, h = c.width, l = c.height, p, n, r, q; b.forEach(function (b) {
                                g = b.val / c.val * c.height * c.width; p = e; n = k; 0 === d ? (q = l, r = g / q, h -= r, e += r) : (r = h, q = g / r, l -= q,
                                    k += q); f.push({ x: p, y: n, width: r, height: q }); a && (d = 1 - d)
                            }); return f
                        }, strip: function (a, c) { return this.algorithmLowAspectRatio(!1, a, c) }, squarified: function (a, c) { return this.algorithmLowAspectRatio(!0, a, c) }, sliceAndDice: function (a, c) { return this.algorithmFill(!0, a, c) }, stripes: function (a, c) { return this.algorithmFill(!1, a, c) }, translate: function () {
                            var a = this, c = a.options, b = N(a); z.prototype.translate.call(a); var d = a.tree = a.getTree(); var g = a.nodeMap[b]; a.renderTraverseUpButton(b); a.mapOptionsToLevel = K({
                                from: g.level +
                                    1, levels: c.levels, to: d.height, defaults: { levelIsConstant: a.options.levelIsConstant, colorByPoint: c.colorByPoint }
                            }); "" === b || g && g.children.length || (a.setRootNode("", !1), b = a.rootNode, g = a.nodeMap[b]); B(a.nodeMap[a.rootNode], function (b) { var c = !1, f = b.parent; b.visible = !0; if (f || "" === f) c = a.nodeMap[f]; return c }); B(a.nodeMap[a.rootNode].children, function (a) { var b = !1; a.forEach(function (a) { a.visible = !0; a.children.length && (b = (b || []).concat(a.children)) }); return b }); a.setTreeValues(d); a.axisRatio = a.xAxis.len / a.yAxis.len;
                            a.nodeMap[""].pointValues = b = { x: 0, y: 0, width: 100, height: 100 }; a.nodeMap[""].values = b = r(b, { width: b.width * a.axisRatio, direction: "vertical" === c.layoutStartingDirection ? 0 : 1, val: d.val }); a.calculateChildrenAreas(d, b); a.colorAxis || c.colorByPoint || a.setColorRecursive(a.tree); c.allowTraversingTree && (c = g.pointValues, a.xAxis.setExtremes(c.x, c.x + c.width, !1), a.yAxis.setExtremes(c.y, c.y + c.height, !1), a.xAxis.setScale(), a.yAxis.setScale()); a.setPointValues()
                        }, drawDataLabels: function () {
                            var a = this, b = a.mapOptionsToLevel,
                            f, d; a.points.filter(function (a) { return a.node.visible }).forEach(function (c) { d = b[c.node.level]; f = { style: {} }; c.node.isLeaf || (f.enabled = !1); d && d.dataLabels && (f = r(f, d.dataLabels), a._hasPointLabels = !0); c.shapeArgs && (f.style.width = c.shapeArgs.width, c.dataLabel && c.dataLabel.css({ width: c.shapeArgs.width + "px" })); c.dlOptions = r(f, c.options.dataLabels) }); z.prototype.drawDataLabels.call(this)
                        }, alignDataLabel: function (a, b, f) {
                            var c = f.style; !h(c.textOverflow) && b.text && b.getBBox().width > b.text.textWidth && b.css({
                                textOverflow: "ellipsis",
                                width: c.width += "px"
                            }); F.column.prototype.alignDataLabel.apply(this, arguments); a.dataLabel && a.dataLabel.attr({ zIndex: (a.node.zIndex || 0) + 1 })
                        }, pointAttribs: function (a, b) {
                            var c = k(this.mapOptionsToLevel) ? this.mapOptionsToLevel : {}, d = a && c[a.node.level] || {}; c = this.options; var g = b && c.states[b] || {}, e = a && a.getClassName() || ""; a = {
                                stroke: a && a.borderColor || d.borderColor || g.borderColor || c.borderColor, "stroke-width": n(a && a.borderWidth, d.borderWidth, g.borderWidth, c.borderWidth), dashstyle: a && a.borderDashStyle || d.borderDashStyle ||
                                    g.borderDashStyle || c.borderDashStyle, fill: a && a.color || this.color
                            }; -1 !== e.indexOf("highcharts-above-level") ? (a.fill = "none", a["stroke-width"] = 0) : -1 !== e.indexOf("highcharts-internal-node-interactive") ? (b = n(g.opacity, c.opacity), a.fill = J(a.fill).setOpacity(b).get(), a.cursor = "pointer") : -1 !== e.indexOf("highcharts-internal-node") ? a.fill = "none" : b && (a.fill = J(a.fill).brighten(g.brightness).get()); return a
                        }, drawPoints: function () {
                            var a = this, b = a.chart, d = b.renderer, e = b.styledMode, g = a.options, k = e ? {} : g.shadow, h = g.borderRadius,
                            l = b.pointCount < g.animationLimit, p = g.allowTraversingTree; a.points.forEach(function (b) {
                                var c = b.node.levelDynamic, f = {}, m = {}, q = {}, n = "level-group-" + c, u = !!b.graphic, t = l && u, w = b.shapeArgs; b.shouldDraw() && (h && (m.r = h), r(!0, t ? f : m, u ? w : {}, e ? {} : a.pointAttribs(b, b.selected && "select")), a.colorAttribs && e && v(q, a.colorAttribs(b)), a[n] || (a[n] = d.g(n).attr({ zIndex: 1E3 - c }).add(a.group), a[n].survive = !0)); b.draw({ animatableAttribs: f, attribs: m, css: q, group: a[n], renderer: d, shadow: k, shapeArgs: w, shapeType: "rect" }); p && b.graphic &&
                                    (b.drillId = g.interactByLeaf ? a.drillToByLeaf(b) : a.drillToByGroup(b))
                            })
                        }, onClickDrillToNode: function (a) { var b = (a = a.point) && a.drillId; D(b) && (a.setState(""), this.setRootNode(b, !0, { trigger: "click" })) }, drillToByGroup: function (a) { var b = !1; 1 !== a.node.level - this.nodeMap[this.rootNode].level || a.node.isLeaf || (b = a.id); return b }, drillToByLeaf: function (a) { var b = !1; if (a.node.parent !== this.rootNode && a.node.isLeaf) for (a = a.node; !b;)a = this.nodeMap[a.parent], a.parent === this.rootNode && (b = a.id); return b }, drillUp: function () {
                            var a =
                                this.nodeMap[this.rootNode]; a && D(a.parent) && this.setRootNode(a.parent, !0, { trigger: "traverseUpButton" })
                        }, drillToNode: function (a, b) { G("WARNING: treemap.drillToNode has been renamed to treemap.setRootNode, and will be removed in the next major version."); this.setRootNode(a, b) }, setRootNode: function (a, b, d) {
                            a = v({ newRootId: a, previousRootId: this.rootNode, redraw: n(b, !0), series: this }, d); C(this, "setRootNode", a, function (a) {
                                var b = a.series; b.idPreviousRoot = a.previousRootId; b.rootNode = a.newRootId; b.isDirty = !0; a.redraw &&
                                    b.chart.redraw()
                            })
                        }, renderTraverseUpButton: function (a) {
                            var b = this, d = b.options.traverseUpButton, e = n(d.text, b.nodeMap[a].name, "< Back"); if ("" === a) b.drillUpButton && (b.drillUpButton = b.drillUpButton.destroy()); else if (this.drillUpButton) this.drillUpButton.placed = !1, this.drillUpButton.attr({ text: e }).align(); else {
                                var g = (a = d.theme) && a.states; this.drillUpButton = this.chart.renderer.button(e, null, null, function () { b.drillUp() }, a, g && g.hover, g && g.select).addClass("highcharts-drillup-button").attr({
                                    align: d.position.align,
                                    zIndex: 7
                                }).add().align(d.position, !1, d.relativeTo || "plotBox")
                            }
                        }, buildKDTree: t, drawLegendSymbol: b.LegendSymbolMixin.drawRectangle, getExtremes: function () { z.prototype.getExtremes.call(this, this.colorValueData); this.valueMin = this.dataMin; this.valueMax = this.dataMax; z.prototype.getExtremes.call(this) }, getExtremesFromAll: !0, bindAxes: function () {
                            var a = { endOnTick: !1, gridLineWidth: 0, lineWidth: 0, min: 0, dataMin: 0, minPadding: 0, max: 100, dataMax: 100, maxPadding: 0, startOnTick: !1, title: null, tickPositions: [] }; z.prototype.bindAxes.call(this);
                            v(this.yAxis.options, a); v(this.xAxis.options, a)
                        }, setState: function (a) { this.options.inactiveOtherPoints = !0; z.prototype.setState.call(this, a, !1); this.options.inactiveOtherPoints = !1 }, utils: { recursive: B }
            }, {
                draw: p, setVisible: F.pie.prototype.pointClass.prototype.setVisible, getClassName: function () {
                    var a = b.Point.prototype.getClassName.call(this), c = this.series, d = c.options; this.node.level <= c.nodeMap[c.rootNode].level ? a += " highcharts-above-level" : this.node.isLeaf || n(d.interactByLeaf, !d.allowTraversingTree) ?
                        this.node.isLeaf || (a += " highcharts-internal-node") : a += " highcharts-internal-node-interactive"; return a
                }, isValid: function () { return this.id || e(this.value) }, setState: function (a) { b.Point.prototype.setState.call(this, a); this.graphic && this.graphic.attr({ zIndex: "hover" === a ? 1 : 0 }) }, shouldDraw: function () { return e(this.plotY) && null !== this.y }
            }); ""
    }); p(b, "masters/modules/treemap.src.js", [], function () { })
});
//# sourceMappingURL=treemap.js.map