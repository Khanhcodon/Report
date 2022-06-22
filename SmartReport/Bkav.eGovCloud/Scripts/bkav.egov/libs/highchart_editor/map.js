/*
 Highmaps JS v8.1.2 (2020-06-16)

 Highmaps as a plugin for Highcharts or Highstock.

 (c) 2011-2019 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (a) { "object" === typeof module && module.exports ? (a["default"] = a, module.exports = a) : "function" === typeof define && define.amd ? define("highcharts/modules/map", ["highcharts"], function (z) { a(z); a.Highcharts = z; return a }) : a("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (a) {
    function z(a, r, k, n) { a.hasOwnProperty(r) || (a[r] = n.apply(null, k)) } a = a ? a._modules : {}; z(a, "parts-map/MapAxis.js", [a["parts/Axis.js"], a["parts/Utilities.js"]], function (a, r) {
        var k = r.addEvent, n = r.pick, c = function () {
            return function (c) {
            this.axis =
                c
            }
        }(); r = function () {
            function a() { } a.compose = function (a) {
                a.keepProps.push("mapAxis"); k(a, "init", function () { this.mapAxis || (this.mapAxis = new c(this)) }); k(a, "getSeriesExtremes", function () { if (this.mapAxis) { var c = []; this.isXAxis && (this.series.forEach(function (a, u) { a.useMapGeometry && (c[u] = a.xData, a.xData = []) }), this.mapAxis.seriesXData = c) } }); k(a, "afterGetSeriesExtremes", function () {
                    if (this.mapAxis) {
                        var c = this.mapAxis.seriesXData || [], a; if (this.isXAxis) {
                            var u = n(this.dataMin, Number.MAX_VALUE); var h = n(this.dataMax,
                                -Number.MAX_VALUE); this.series.forEach(function (f, x) { f.useMapGeometry && (u = Math.min(u, n(f.minX, u)), h = Math.max(h, n(f.maxX, h)), f.xData = c[x], a = !0) }); a && (this.dataMin = u, this.dataMax = h); this.mapAxis.seriesXData = void 0
                        }
                    }
                }); k(a, "afterSetAxisTranslation", function () {
                    if (this.mapAxis) {
                        var c = this.chart, a = c.plotWidth / c.plotHeight; c = c.xAxis[0]; var u; "yAxis" === this.coll && "undefined" !== typeof c.transA && this.series.forEach(function (c) { c.preserveAspectRatio && (u = !0) }); if (u && (this.transA = c.transA = Math.min(this.transA,
                            c.transA), a /= (c.max - c.min) / (this.max - this.min), a = 1 > a ? this : c, c = (a.max - a.min) * a.transA, a.mapAxis.pixelPadding = a.len - c, a.minPixelPadding = a.mapAxis.pixelPadding / 2, c = a.mapAxis.fixTo)) { c = c[1] - a.toValue(c[0], !0); c *= a.transA; if (Math.abs(c) > a.minPixelPadding || a.min === a.dataMin && a.max === a.dataMax) c = 0; a.minPixelPadding -= c }
                    }
                }); k(a, "render", function () { this.mapAxis && (this.mapAxis.fixTo = void 0) })
            }; return a
        }(); r.compose(a); return r
    }); z(a, "parts-map/ColorSeriesMixin.js", [a["parts/Globals.js"]], function (a) {
    a.colorPointMixin =
        { setVisible: function (a) { var k = this, n = a ? "show" : "hide"; k.visible = k.options.visible = !!a;["graphic", "dataLabel"].forEach(function (c) { if (k[c]) k[c][n]() }); this.series.buildKDTree() } }; a.colorSeriesMixin = {
            optionalAxis: "colorAxis", colorAxis: 0, translateColors: function () {
                var a = this, k = this.options.nullColor, n = this.colorAxis, c = this.colorKey; (this.data.length ? this.data : this.points).forEach(function (C) {
                    var w = C.getNestedProperty(c); (w = C.options.color || (C.isNull || null === C.value ? k : n && "undefined" !== typeof w ? n.toColor(w,
                        C) : C.color || a.color)) && C.color !== w && (C.color = w, "point" === a.options.legendType && C.legendItem && a.chart.legend.colorizeItem(C, C.visible))
                })
            }
        }
    }); z(a, "parts-map/ColorAxis.js", [a["parts/Axis.js"], a["parts/Chart.js"], a["parts/Color.js"], a["parts/Globals.js"], a["parts/Legend.js"], a["mixins/legend-symbol.js"], a["parts/Point.js"], a["parts/Utilities.js"]], function (a, r, k, n, c, C, D, A) {
        var w = this && this.__extends || function () {
            var b = function (e, d) {
                b = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (b, e) {
                b.__proto__ =
                    e
                } || function (b, e) { for (var d in e) e.hasOwnProperty(d) && (b[d] = e[d]) }; return b(e, d)
            }; return function (e, d) { function t() { this.constructor = e } b(e, d); e.prototype = null === d ? Object.create(d) : (t.prototype = d.prototype, new t) }
        }(), u = k.parse, h = n.noop; k = A.addEvent; var f = A.erase, x = A.extend, l = A.Fx, q = A.isNumber, p = A.merge, y = A.pick, g = A.splat; ""; var d = n.Series; A = n.colorPointMixin; x(d.prototype, n.colorSeriesMixin); x(D.prototype, A); r.prototype.collectionsWithUpdate.push("colorAxis"); r.prototype.collectionsWithInit.colorAxis =
            [r.prototype.addColorAxis]; var b = function (b) {
                function e(e, d) { var t = b.call(this, e, d) || this; t.beforePadding = !1; t.chart = void 0; t.coll = "colorAxis"; t.dataClasses = void 0; t.legendItem = void 0; t.legendItems = void 0; t.name = ""; t.options = void 0; t.stops = void 0; t.visible = !0; t.init(e, d); return t } w(e, b); e.buildOptions = function (b, e, d) {
                    b = b.options.legend || {}; var t = d.layout ? "vertical" !== d.layout : "vertical" !== b.layout; return p(e, { side: t ? 2 : 1, reversed: !t }, d, {
                        opposite: !t, showEmpty: !1, title: null, visible: b.enabled && (d ? !1 !==
                            d.visible : !0)
                    })
                }; e.prototype.init = function (d, t) { var v = e.buildOptions(d, e.defaultOptions, t); this.coll = "colorAxis"; b.prototype.init.call(this, d, v); t.dataClasses && this.initDataClasses(t); this.initStops(); this.horiz = !v.opposite; this.zoomEnabled = !1 }; e.prototype.initDataClasses = function (b) {
                    var e = this.chart, d, v = 0, g = e.options.chart.colorCount, m = this.options, f = b.dataClasses.length; this.dataClasses = d = []; this.legendItems = []; b.dataClasses.forEach(function (b, t) {
                        b = p(b); d.push(b); if (e.styledMode || !b.color) "category" ===
                            m.dataClassColor ? (e.styledMode || (t = e.options.colors, g = t.length, b.color = t[v]), b.colorIndex = v, v++, v === g && (v = 0)) : b.color = u(m.minColor).tweenTo(u(m.maxColor), 2 > f ? .5 : t / (f - 1))
                    })
                }; e.prototype.hasData = function () { return !!(this.tickPositions || []).length }; e.prototype.setTickPositions = function () { if (!this.dataClasses) return b.prototype.setTickPositions.call(this) }; e.prototype.initStops = function () {
                this.stops = this.options.stops || [[0, this.options.minColor], [1, this.options.maxColor]]; this.stops.forEach(function (b) {
                b.color =
                    u(b[1])
                })
                }; e.prototype.setOptions = function (e) { b.prototype.setOptions.call(this, e); this.options.crosshair = this.options.marker }; e.prototype.setAxisSize = function () { var b = this.legendSymbol, d = this.chart, g = d.options.legend || {}, m, f; b ? (this.left = g = b.attr("x"), this.top = m = b.attr("y"), this.width = f = b.attr("width"), this.height = b = b.attr("height"), this.right = d.chartWidth - g - f, this.bottom = d.chartHeight - m - b, this.len = this.horiz ? f : b, this.pos = this.horiz ? g : m) : this.len = (this.horiz ? g.symbolWidth : g.symbolHeight) || e.defaultLegendLength };
                e.prototype.normalizedValue = function (b) { this.logarithmic && (b = this.logarithmic.log2lin(b)); return 1 - (this.max - b) / (this.max - this.min || 1) }; e.prototype.toColor = function (b, e) {
                    var d = this.dataClasses, t = this.stops, g; if (d) for (g = d.length; g--;) { var m = d[g]; var v = m.from; t = m.to; if (("undefined" === typeof v || b >= v) && ("undefined" === typeof t || b <= t)) { var f = m.color; e && (e.dataClass = g, e.colorIndex = m.colorIndex); break } } else {
                        b = this.normalizedValue(b); for (g = t.length; g-- && !(b > t[g][0]);); v = t[g] || t[g + 1]; t = t[g + 1] || v; b = 1 - (t[0] -
                            b) / (t[0] - v[0] || 1); f = v.color.tweenTo(t.color, b)
                    } return f
                }; e.prototype.getOffset = function () { var e = this.legendGroup, d = this.chart.axisOffset[this.side]; e && (this.axisParent = e, b.prototype.getOffset.call(this), this.added || (this.added = !0, this.labelLeft = 0, this.labelRight = this.width), this.chart.axisOffset[this.side] = d) }; e.prototype.setLegendColor = function () { var b = this.reversed, e = b ? 1 : 0; b = b ? 0 : 1; e = this.horiz ? [e, 0, b, 0] : [0, b, 0, e]; this.legendColor = { linearGradient: { x1: e[0], y1: e[1], x2: e[2], y2: e[3] }, stops: this.stops } };
                e.prototype.drawLegendSymbol = function (b, d) { var t = b.padding, g = b.options, m = this.horiz, f = y(g.symbolWidth, m ? e.defaultLegendLength : 12), v = y(g.symbolHeight, m ? 12 : e.defaultLegendLength), c = y(g.labelPadding, m ? 16 : 30); g = y(g.itemDistance, 10); this.setLegendColor(); d.legendSymbol = this.chart.renderer.rect(0, b.baseline - 11, f, v).attr({ zIndex: 1 }).add(d.legendGroup); this.legendItemWidth = f + t + (m ? g : c); this.legendItemHeight = v + t + (m ? c : 0) }; e.prototype.setState = function (b) { this.series.forEach(function (e) { e.setState(b) }) }; e.prototype.setVisible =
                    function () { }; e.prototype.getSeriesExtremes = function () {
                        var b = this.series, e = b.length, g; this.dataMin = Infinity; for (this.dataMax = -Infinity; e--;) {
                            var m = b[e]; var f = m.colorKey = y(m.options.colorKey, m.colorKey, m.pointValKey, m.zoneAxis, "y"); var c = m.pointArrayMap; var a = m[f + "Min"] && m[f + "Max"]; if (m[f + "Data"]) var l = m[f + "Data"]; else if (c) { l = []; c = c.indexOf(f); var h = m.yData; if (0 <= c && h) for (g = 0; g < h.length; g++)l.push(y(h[g][c], h[g])) } else l = m.yData; a ? (m.minColorValue = m[f + "Min"], m.maxColorValue = m[f + "Max"]) : (l = d.prototype.getExtremes.call(m,
                                l), m.minColorValue = l.dataMin, m.maxColorValue = l.dataMax); "undefined" !== typeof m.minColorValue && (this.dataMin = Math.min(this.dataMin, m.minColorValue), this.dataMax = Math.max(this.dataMax, m.maxColorValue)); a || d.prototype.applyExtremes.call(m)
                        }
                    }; e.prototype.drawCrosshair = function (e, d) {
                        var m = d && d.plotX, g = d && d.plotY, t = this.pos, f = this.len; if (d) {
                            var c = this.toPixels(d.getNestedProperty(d.series.colorKey)); c < t ? c = t - 2 : c > t + f && (c = t + f + 2); d.plotX = c; d.plotY = this.len - c; b.prototype.drawCrosshair.call(this, e, d); d.plotX =
                                m; d.plotY = g; this.cross && !this.cross.addedToColorAxis && this.legendGroup && (this.cross.addClass("highcharts-coloraxis-marker").add(this.legendGroup), this.cross.addedToColorAxis = !0, !this.chart.styledMode && this.crosshair && this.cross.attr({ fill: this.crosshair.color }))
                        }
                    }; e.prototype.getPlotLinePath = function (e) {
                        var d = this.left, m = e.translatedValue, g = this.top; return q(m) ? this.horiz ? [["M", m - 4, g - 6], ["L", m + 4, g - 6], ["L", m, g], ["Z"]] : [["M", d, m], ["L", d - 6, m + 6], ["L", d - 6, m - 6], ["Z"]] : b.prototype.getPlotLinePath.call(this,
                            e)
                    }; e.prototype.update = function (d, m) { var g = this.chart, t = g.legend, f = e.buildOptions(g, {}, d); this.series.forEach(function (b) { b.isDirtyData = !0 }); (d.dataClasses && t.allItems || this.dataClasses) && this.destroyItems(); g.options[this.coll] = p(this.userOptions, f); b.prototype.update.call(this, f, m); this.legendItem && (this.setLegendColor(), t.colorizeItem(this, !0)) }; e.prototype.destroyItems = function () {
                        var b = this.chart; this.legendItem ? b.legend.destroyItem(this) : this.legendItems && this.legendItems.forEach(function (e) { b.legend.destroyItem(e) });
                        b.isDirtyLegend = !0
                    }; e.prototype.remove = function (e) { this.destroyItems(); b.prototype.remove.call(this, e) }; e.prototype.getDataClassLegendSymbols = function () {
                        var b = this, e = b.chart, d = b.legendItems, m = e.options.legend, g = m.valueDecimals, f = m.valueSuffix || "", c; d.length || b.dataClasses.forEach(function (m, t) {
                            var l = !0, a = m.from, v = m.to, q = e.numberFormatter; c = ""; "undefined" === typeof a ? c = "< " : "undefined" === typeof v && (c = "> "); "undefined" !== typeof a && (c += q(a, g) + f); "undefined" !== typeof a && "undefined" !== typeof v && (c += " - ");
                            "undefined" !== typeof v && (c += q(v, g) + f); d.push(x({ chart: e, name: c, options: {}, drawLegendSymbol: C.drawRectangle, visible: !0, setState: h, isDataClass: !0, setVisible: function () { l = b.visible = !l; b.series.forEach(function (b) { b.points.forEach(function (b) { b.dataClass === t && b.setVisible(l) }) }); e.legend.colorizeItem(this, l) } }, m))
                        }); return d
                    }; e.defaultLegendLength = 200; e.defaultOptions = {
                        lineWidth: 0, minPadding: 0, maxPadding: 0, gridLineWidth: 1, tickPixelInterval: 72, startOnTick: !0, endOnTick: !0, offset: 0, marker: {
                            animation: { duration: 50 },
                            width: .01, color: "#999999"
                        }, labels: { overflow: "justify", rotation: 0 }, minColor: "#e6ebf5", maxColor: "#003399", tickLength: 5, showInLegend: !0
                    }; e.keepProps = ["legendGroup", "legendItemHeight", "legendItemWidth", "legendItem", "legendSymbol"]; return e
            }(a); Array.prototype.push.apply(a.keepProps, b.keepProps); n.ColorAxis = b;["fill", "stroke"].forEach(function (b) { l.prototype[b + "Setter"] = function () { this.elem.attr(b, u(this.start).tweenTo(u(this.end), this.pos), null, !0) } }); k(r, "afterGetAxes", function () {
                var e = this, d = e.options;
                this.colorAxis = []; d.colorAxis && (d.colorAxis = g(d.colorAxis), d.colorAxis.forEach(function (d, m) { d.index = m; new b(e, d) }))
            }); k(d, "bindAxes", function () { var b = this.axisTypes; b ? -1 === b.indexOf("colorAxis") && b.push("colorAxis") : this.axisTypes = ["colorAxis"] }); k(c, "afterGetAllItems", function (b) {
                var e = [], d, g; (this.chart.colorAxis || []).forEach(function (g) {
                (d = g.options) && d.showInLegend && (d.dataClasses && d.visible ? e = e.concat(g.getDataClassLegendSymbols()) : d.visible && e.push(g), g.series.forEach(function (e) {
                    if (!e.options.showInLegend ||
                        d.dataClasses) "point" === e.options.legendType ? e.points.forEach(function (e) { f(b.allItems, e) }) : f(b.allItems, e)
                }))
                }); for (g = e.length; g--;)b.allItems.unshift(e[g])
            }); k(c, "afterColorizeItem", function (b) { b.visible && b.item.legendColor && b.item.legendSymbol.attr({ fill: b.item.legendColor }) }); k(c, "afterUpdate", function () { var b = this.chart.colorAxis; b && b.forEach(function (b, e, d) { b.update({}, d) }) }); k(d, "afterTranslate", function () { (this.chart.colorAxis && this.chart.colorAxis.length || this.colorAttribs) && this.translateColors() });
        return b
    }); z(a, "parts-map/ColorMapSeriesMixin.js", [a["parts/Globals.js"], a["parts/Point.js"], a["parts/Utilities.js"]], function (a, r, k) {
        var n = k.defined; k = a.noop; var c = a.seriesTypes; a.colorMapPointMixin = { dataLabelOnNull: !0, isValid: function () { return null !== this.value && Infinity !== this.value && -Infinity !== this.value }, setState: function (c) { r.prototype.setState.call(this, c); this.graphic && this.graphic.attr({ zIndex: "hover" === c ? 1 : 0 }) } }; a.colorMapSeriesMixin = {
            pointArrayMap: ["value"], axisTypes: ["xAxis", "yAxis",
                "colorAxis"], trackerGroups: ["group", "markerGroup", "dataLabelsGroup"], getSymbol: k, parallelArrays: ["x", "y", "value"], colorKey: "value", pointAttribs: c.column.prototype.pointAttribs, colorAttribs: function (c) { var a = {}; n(c.color) && (a[this.colorProp || "fill"] = c.color); return a }
        }
    }); z(a, "parts-map/MapNavigation.js", [a["parts/Chart.js"], a["parts/Globals.js"], a["parts/Utilities.js"]], function (a, r, k) {
        function n(f) { f && (f.preventDefault && f.preventDefault(), f.stopPropagation && f.stopPropagation(), f.cancelBubble = !0) }
        function c(f) { this.init(f) } var C = r.doc, w = k.addEvent, A = k.extend, B = k.merge, u = k.objectEach, h = k.pick; c.prototype.init = function (f) { this.chart = f; f.mapNavButtons = [] }; c.prototype.update = function (f) {
            var c = this.chart, a = c.options.mapNavigation, q, p, y, g, d, b = function (b) { this.handler.call(c, b); n(b) }, e = c.mapNavButtons; f && (a = c.options.mapNavigation = B(c.options.mapNavigation, f)); for (; e.length;)e.pop().destroy(); h(a.enableButtons, a.enabled) && !c.renderer.forExport && u(a.buttons, function (m, f) {
                q = B(a.buttonOptions, m); c.styledMode ||
                    (p = q.theme, p.style = B(q.theme.style, q.style), g = (y = p.states) && y.hover, d = y && y.select); m = c.renderer.button(q.text, 0, 0, b, p, g, d, 0, "zoomIn" === f ? "topbutton" : "bottombutton").addClass("highcharts-map-navigation highcharts-" + { zoomIn: "zoom-in", zoomOut: "zoom-out" }[f]).attr({ width: q.width, height: q.height, title: c.options.lang[f], padding: q.padding, zIndex: 5 }).add(); m.handler = q.onclick; w(m.element, "dblclick", n); e.push(m); var t = q, l = w(c, "load", function () {
                        m.align(A(t, { width: m.width, height: 2 * m.height }), null, t.alignTo);
                        l()
                    })
            }); this.updateEvents(a)
        }; c.prototype.updateEvents = function (c) {
            var f = this.chart; h(c.enableDoubleClickZoom, c.enabled) || c.enableDoubleClickZoomTo ? this.unbindDblClick = this.unbindDblClick || w(f.container, "dblclick", function (c) { f.pointer.onContainerDblClick(c) }) : this.unbindDblClick && (this.unbindDblClick = this.unbindDblClick()); h(c.enableMouseWheelZoom, c.enabled) ? this.unbindMouseWheel = this.unbindMouseWheel || w(f.container, "undefined" === typeof C.onmousewheel ? "DOMMouseScroll" : "mousewheel", function (c) {
                f.pointer.onContainerMouseWheel(c);
                n(c); return !1
            }) : this.unbindMouseWheel && (this.unbindMouseWheel = this.unbindMouseWheel())
        }; A(a.prototype, {
            fitToBox: function (c, a) { [["x", "width"], ["y", "height"]].forEach(function (f) { var l = f[0]; f = f[1]; c[l] + c[f] > a[l] + a[f] && (c[f] > a[f] ? (c[f] = a[f], c[l] = a[l]) : c[l] = a[l] + a[f] - c[f]); c[f] > a[f] && (c[f] = a[f]); c[l] < a[l] && (c[l] = a[l]) }); return c }, mapZoom: function (c, a, l, q, p) {
                var f = this.xAxis[0], g = f.max - f.min, d = h(a, f.min + g / 2), b = g * c; g = this.yAxis[0]; var e = g.max - g.min, m = h(l, g.min + e / 2); e *= c; d = this.fitToBox({
                    x: d - b * (q ? (q - f.pos) /
                        f.len : .5), y: m - e * (p ? (p - g.pos) / g.len : .5), width: b, height: e
                }, { x: f.dataMin, y: g.dataMin, width: f.dataMax - f.dataMin, height: g.dataMax - g.dataMin }); b = d.x <= f.dataMin && d.width >= f.dataMax - f.dataMin && d.y <= g.dataMin && d.height >= g.dataMax - g.dataMin; q && f.mapAxis && (f.mapAxis.fixTo = [q - f.pos, a]); p && g.mapAxis && (g.mapAxis.fixTo = [p - g.pos, l]); "undefined" === typeof c || b ? (f.setExtremes(void 0, void 0, !1), g.setExtremes(void 0, void 0, !1)) : (f.setExtremes(d.x, d.x + d.width, !1), g.setExtremes(d.y, d.y + d.height, !1)); this.redraw()
            }
        }); w(a,
            "beforeRender", function () { this.mapNavigation = new c(this); this.mapNavigation.update() }); r.MapNavigation = c
    }); z(a, "parts-map/MapPointer.js", [a["parts/Pointer.js"], a["parts/Utilities.js"]], function (a, r) {
        var k = r.extend, n = r.pick; r = r.wrap; k(a.prototype, {
            onContainerDblClick: function (c) {
                var a = this.chart; c = this.normalize(c); a.options.mapNavigation.enableDoubleClickZoomTo ? a.pointer.inClass(c.target, "highcharts-tracker") && a.hoverPoint && a.hoverPoint.zoomTo() : a.isInsidePlot(c.chartX - a.plotLeft, c.chartY - a.plotTop) &&
                    a.mapZoom(.5, a.xAxis[0].toValue(c.chartX), a.yAxis[0].toValue(c.chartY), c.chartX, c.chartY)
            }, onContainerMouseWheel: function (c) { var a = this.chart; c = this.normalize(c); var k = c.detail || -(c.wheelDelta / 120); a.isInsidePlot(c.chartX - a.plotLeft, c.chartY - a.plotTop) && a.mapZoom(Math.pow(a.options.mapNavigation.mouseWheelSensitivity, k), a.xAxis[0].toValue(c.chartX), a.yAxis[0].toValue(c.chartY), c.chartX, c.chartY) }
        }); r(a.prototype, "zoomOption", function (c) {
            var a = this.chart.options.mapNavigation; n(a.enableTouchZoom, a.enabled) &&
                (this.chart.options.chart.pinchType = "xy"); c.apply(this, [].slice.call(arguments, 1))
        }); r(a.prototype, "pinchTranslate", function (c, a, k, n, w, u, h) { c.call(this, a, k, n, w, u, h); "map" === this.chart.options.chart.type && this.hasZoom && (c = n.scaleX > n.scaleY, this.pinchTranslateDirection(!c, a, k, n, w, u, h, c ? n.scaleX : n.scaleY)) })
    }); z(a, "parts-map/MapSeries.js", [a["parts/Globals.js"], a["mixins/legend-symbol.js"], a["parts/Point.js"], a["parts/SVGRenderer.js"], a["parts/Utilities.js"]], function (a, r, k, n, c) {
        var w = c.extend, z = c.fireEvent,
        A = c.getNestedProperty, B = c.isArray, u = c.isNumber, h = c.merge, f = c.objectEach, x = c.pick, l = c.seriesType, q = c.splat, p = a.colorMapPointMixin, y = a.noop, g = a.Series, d = a.seriesTypes; l("map", "scatter", {
            animation: !1, dataLabels: { crop: !1, formatter: function () { return this.point.value }, inside: !0, overflow: !1, padding: 0, verticalAlign: "middle" }, marker: null, nullColor: "#f7f7f7", stickyTracking: !1, tooltip: { followPointer: !0, pointFormat: "{point.name}: {point.value}<br/>" }, turboThreshold: 0, allAreas: !0, borderColor: "#cccccc", borderWidth: 1,
            joinBy: "hc-key", states: { hover: { halo: null, brightness: .2 }, normal: { animation: !0 }, select: { color: "#cccccc" }, inactive: { opacity: 1 } }
        }, h(a.colorMapSeriesMixin, {
            type: "map", getExtremesFromAll: !0, useMapGeometry: !0, forceDL: !0, searchPoint: y, directTouch: !0, preserveAspectRatio: !0, pointArrayMap: ["value"], setOptions: function (b) { b = g.prototype.setOptions.call(this, b); var e = b.joinBy; null === e && (e = "_i"); e = this.joinBy = q(e); e[1] || (e[1] = e[0]); return b }, getBox: function (b) {
                var e = Number.MAX_VALUE, d = -e, c = e, g = -e, f = e, l = e, h = this.xAxis,
                p = this.yAxis, q; (b || []).forEach(function (b) {
                    if (b.path) {
                        "string" === typeof b.path ? b.path = a.splitPath(b.path) : "M" === b.path[0] && (b.path = n.prototype.pathToSegments(b.path)); var m = b.path || [], t = -e, h = e, p = -e, v = e, u = b.properties; b._foundBox || (m.forEach(function (b) { var e = b[b.length - 2]; b = b[b.length - 1]; "number" === typeof e && "number" === typeof b && (h = Math.min(h, e), t = Math.max(t, e), v = Math.min(v, b), p = Math.max(p, b)) }), b._midX = h + (t - h) * x(b.middleX, u && u["hc-middle-x"], .5), b._midY = v + (p - v) * x(b.middleY, u && u["hc-middle-y"], .5),
                            b._maxX = t, b._minX = h, b._maxY = p, b._minY = v, b.labelrank = x(b.labelrank, (t - h) * (p - v)), b._foundBox = !0); d = Math.max(d, b._maxX); c = Math.min(c, b._minX); g = Math.max(g, b._maxY); f = Math.min(f, b._minY); l = Math.min(b._maxX - b._minX, b._maxY - b._minY, l); q = !0
                    }
                }); q && (this.minY = Math.min(f, x(this.minY, e)), this.maxY = Math.max(g, x(this.maxY, -e)), this.minX = Math.min(c, x(this.minX, e)), this.maxX = Math.max(d, x(this.maxX, -e)), h && "undefined" === typeof h.options.minRange && (h.minRange = Math.min(5 * l, (this.maxX - this.minX) / 5, h.minRange || e)), p &&
                    "undefined" === typeof p.options.minRange && (p.minRange = Math.min(5 * l, (this.maxY - this.minY) / 5, p.minRange || e)))
            }, hasData: function () { return !!this.processedXData.length }, getExtremes: function () { var b = g.prototype.getExtremes.call(this, this.valueData), e = b.dataMin; b = b.dataMax; this.chart.hasRendered && this.isDirtyData && this.getBox(this.options.data); u(e) && (this.valueMin = e); u(b) && (this.valueMax = b); return { dataMin: this.minY, dataMax: this.maxY } }, translatePath: function (b) {
                var e = this.xAxis, d = this.yAxis, a = e.min, c = e.transA,
                g = e.minPixelPadding, f = d.min, l = d.transA, h = d.minPixelPadding, p = []; b && b.forEach(function (b) { "M" === b[0] ? p.push(["M", (b[1] - (a || 0)) * c + g, (b[2] - (f || 0)) * l + h]) : "L" === b[0] ? p.push(["L", (b[1] - (a || 0)) * c + g, (b[2] - (f || 0)) * l + h]) : "C" === b[0] ? p.push(["C", (b[1] - (a || 0)) * c + g, (b[2] - (f || 0)) * l + h, (b[3] - (a || 0)) * c + g, (b[4] - (f || 0)) * l + h, (b[5] - (a || 0)) * c + g, (b[6] - (f || 0)) * l + h]) : "Q" === b[0] ? p.push(["Q", (b[1] - (a || 0)) * c + g, (b[2] - (f || 0)) * l + h, (b[3] - (a || 0)) * c + g, (b[4] - (f || 0)) * l + h]) : "Z" === b[0] && p.push(["Z"]) }); return p
            }, setData: function (b, e, d,
                c) {
                    var m = this.options, l = this.chart.options.chart, p = l && l.map, q = m.mapData, v = this.joinBy, y = m.keys || this.pointArrayMap, n = [], x = {}, r = this.chart.mapTransforms; !q && p && (q = "string" === typeof p ? a.maps[p] : p); b && b.forEach(function (e, d) {
                        var a = 0; if (u(e)) b[d] = { value: e }; else if (B(e)) { b[d] = {}; !m.keys && e.length > y.length && "string" === typeof e[0] && (b[d]["hc-key"] = e[0], ++a); for (var c = 0; c < y.length; ++c, ++a)y[c] && "undefined" !== typeof e[a] && (0 < y[c].indexOf(".") ? k.prototype.setNestedProperty(b[d], e[a], y[c]) : b[d][y[c]] = e[a]) } v &&
                            "_i" === v[0] && (b[d]._i = d)
                    }); this.getBox(b); (this.chart.mapTransforms = r = l && l.mapTransforms || q && q["hc-transform"] || r) && f(r, function (b) { b.rotation && (b.cosAngle = Math.cos(b.rotation), b.sinAngle = Math.sin(b.rotation)) }); if (q) {
                    "FeatureCollection" === q.type && (this.mapTitle = q.title, q = a.geojson(q, this.type, this)); this.mapData = q; this.mapMap = {}; for (r = 0; r < q.length; r++)l = q[r], p = l.properties, l._i = r, v[0] && p && p[v[0]] && (l[v[0]] = p[v[0]]), x[l[v[0]]] = l; this.mapMap = x; if (b && v[1]) {
                        var w = v[1]; b.forEach(function (b) {
                            b = A(w, b);
                            x[b] && n.push(x[b])
                        })
                    } if (m.allAreas) { this.getBox(q); b = b || []; if (v[1]) { var C = v[1]; b.forEach(function (b) { n.push(A(C, b)) }) } n = "|" + n.map(function (b) { return b && b[v[0]] }).join("|") + "|"; q.forEach(function (e) { v[0] && -1 !== n.indexOf("|" + e[v[0]] + "|") || (b.push(h(e, { value: null })), c = !1) }) } else this.getBox(n)
                    } g.prototype.setData.call(this, b, e, d, c)
            }, drawGraph: y, drawDataLabels: y, doFullTranslate: function () { return this.isDirtyData || this.chart.isResizing || this.chart.renderer.isVML || !this.baseTrans }, translate: function () {
                var b =
                    this, e = b.xAxis, d = b.yAxis, a = b.doFullTranslate(); b.generatePoints(); b.data.forEach(function (c) { u(c._midX) && u(c._midY) && (c.plotX = e.toPixels(c._midX, !0), c.plotY = d.toPixels(c._midY, !0)); a && (c.shapeType = "path", c.shapeArgs = { d: b.translatePath(c.path) }) }); z(b, "afterTranslate")
            }, pointAttribs: function (b, e) {
                e = b.series.chart.styledMode ? this.colorAttribs(b) : d.column.prototype.pointAttribs.call(this, b, e); e["stroke-width"] = x(b.options[this.pointAttrToOptions && this.pointAttrToOptions["stroke-width"] || "borderWidth"],
                    "inherit"); return e
            }, drawPoints: function () {
                var b = this, e = b.xAxis, c = b.yAxis, a = b.group, g = b.chart, f = g.renderer, l = this.baseTrans; b.transformGroup || (b.transformGroup = f.g().attr({ scaleX: 1, scaleY: 1 }).add(a), b.transformGroup.survive = !0); if (b.doFullTranslate()) g.hasRendered && !g.styledMode && b.points.forEach(function (e) { e.shapeArgs && (e.shapeArgs.fill = b.pointAttribs(e, e.state).fill) }), b.group = b.transformGroup, d.column.prototype.drawPoints.apply(b), b.group = a, b.points.forEach(function (e) {
                    if (e.graphic) {
                        var d = "";
                        e.name && (d += "highcharts-name-" + e.name.replace(/ /g, "-").toLowerCase()); e.properties && e.properties["hc-key"] && (d += " highcharts-key-" + e.properties["hc-key"].toLowerCase()); d && e.graphic.addClass(d); g.styledMode && e.graphic.css(b.pointAttribs(e, e.selected && "select" || void 0))
                    }
                }), this.baseTrans = { originX: e.min - e.minPixelPadding / e.transA, originY: c.min - c.minPixelPadding / c.transA + (c.reversed ? 0 : c.len / c.transA), transAX: e.transA, transAY: c.transA }, this.transformGroup.animate({
                    translateX: 0, translateY: 0, scaleX: 1,
                    scaleY: 1
                }); else {
                    var h = e.transA / l.transAX; var p = c.transA / l.transAY; var q = e.toPixels(l.originX, !0); var u = c.toPixels(l.originY, !0); .99 < h && 1.01 > h && .99 < p && 1.01 > p && (p = h = 1, q = Math.round(q), u = Math.round(u)); var y = this.transformGroup; if (g.renderer.globalAnimation) {
                        var n = y.attr("translateX"); var k = y.attr("translateY"); var r = y.attr("scaleX"); var w = y.attr("scaleY"); y.attr({ animator: 0 }).animate({ animator: 1 }, {
                            step: function (b, e) {
                                y.attr({
                                    translateX: n + (q - n) * e.pos, translateY: k + (u - k) * e.pos, scaleX: r + (h - r) * e.pos, scaleY: w +
                                        (p - w) * e.pos
                                })
                            }
                        })
                    } else y.attr({ translateX: q, translateY: u, scaleX: h, scaleY: p })
                } g.styledMode || a.element.setAttribute("stroke-width", x(b.options[b.pointAttrToOptions && b.pointAttrToOptions["stroke-width"] || "borderWidth"], 1) / (h || 1)); this.drawMapDataLabels()
            }, drawMapDataLabels: function () { g.prototype.drawDataLabels.call(this); this.dataLabelsGroup && this.dataLabelsGroup.clip(this.chart.clipRect) }, render: function () {
                var b = this, e = g.prototype.render; b.chart.renderer.isVML && 3E3 < b.data.length ? setTimeout(function () { e.call(b) }) :
                    e.call(b)
            }, animate: function (b) { var e = this.options.animation, d = this.group, c = this.xAxis, a = this.yAxis, g = c.pos, f = a.pos; this.chart.renderer.isSVG && (!0 === e && (e = { duration: 1E3 }), b ? d.attr({ translateX: g + c.len / 2, translateY: f + a.len / 2, scaleX: .001, scaleY: .001 }) : d.animate({ translateX: g, translateY: f, scaleX: 1, scaleY: 1 }, e)) }, animateDrilldown: function (b) {
                var e = this.chart.plotBox, d = this.chart.drilldownLevels[this.chart.drilldownLevels.length - 1], c = d.bBox, a = this.chart.options.drilldown.animation; b || (b = Math.min(c.width /
                    e.width, c.height / e.height), d.shapeArgs = { scaleX: b, scaleY: b, translateX: c.x, translateY: c.y }, this.points.forEach(function (b) { b.graphic && b.graphic.attr(d.shapeArgs).animate({ scaleX: 1, scaleY: 1, translateX: 0, translateY: 0 }, a) }))
            }, drawLegendSymbol: r.drawRectangle, animateDrillupFrom: function (b) { d.column.prototype.animateDrillupFrom.call(this, b) }, animateDrillupTo: function (b) { d.column.prototype.animateDrillupTo.call(this, b) }
        }), w({
            applyOptions: function (b, e) {
                var d = this.series; b = k.prototype.applyOptions.call(this,
                    b, e); e = d.joinBy; d.mapData && d.mapMap && (e = k.prototype.getNestedProperty.call(b, e[1]), (e = "undefined" !== typeof e && d.mapMap[e]) ? (d.xyFromShape && (b.x = e._midX, b.y = e._midY), w(b, e)) : b.value = b.value || null); return b
            }, onMouseOver: function (b) { c.clearTimeout(this.colorInterval); if (null !== this.value || this.series.options.nullInteraction) k.prototype.onMouseOver.call(this, b); else this.series.onMouseOut(b) }, zoomTo: function () {
                var b = this.series; b.xAxis.setExtremes(this._minX, this._maxX, !1); b.yAxis.setExtremes(this._minY,
                    this._maxY, !1); b.chart.redraw()
            }
        }, p)); ""
    }); z(a, "parts-map/MapLineSeries.js", [a["parts/Globals.js"], a["parts/Utilities.js"]], function (a, r) { r = r.seriesType; var k = a.seriesTypes; r("mapline", "map", { lineWidth: 1, fillColor: "none" }, { type: "mapline", colorProp: "stroke", pointAttrToOptions: { stroke: "color", "stroke-width": "lineWidth" }, pointAttribs: function (a, c) { a = k.map.prototype.pointAttribs.call(this, a, c); a.fill = this.options.fillColor; return a }, drawLegendSymbol: k.line.prototype.drawLegendSymbol }); "" }); z(a, "parts-map/MapPointSeries.js",
        [a["parts/Globals.js"]], function (a) {
            var r = a.merge, k = a.Point, n = a.Series; a = a.seriesType; a("mappoint", "scatter", { dataLabels: { crop: !1, defer: !1, enabled: !0, formatter: function () { return this.point.name }, overflow: !1, style: { color: "#000000" } } }, { type: "mappoint", forceDL: !0, drawDataLabels: function () { n.prototype.drawDataLabels.call(this); this.dataLabelsGroup && this.dataLabelsGroup.clip(this.chart.clipRect) } }, {
                applyOptions: function (c, a) {
                    c = "undefined" !== typeof c.lat && "undefined" !== typeof c.lon ? r(c, this.series.chart.fromLatLonToPoint(c)) :
                        c; return k.prototype.applyOptions.call(this, c, a)
                }
            }); ""
        }); z(a, "parts-more/BubbleLegend.js", [a["parts/Chart.js"], a["parts/Color.js"], a["parts/Globals.js"], a["parts/Legend.js"], a["parts/Utilities.js"]], function (a, r, k, n, c) {
            var w = r.parse; r = c.addEvent; var z = c.arrayMax, A = c.arrayMin, B = c.isNumber, u = c.merge, h = c.objectEach, f = c.pick, x = c.setOptions, l = c.stableSort, q = c.wrap; ""; var p = k.Series, y = k.noop; x({
                legend: {
                    bubbleLegend: {
                        borderColor: void 0, borderWidth: 2, className: void 0, color: void 0, connectorClassName: void 0,
                        connectorColor: void 0, connectorDistance: 60, connectorWidth: 1, enabled: !1, labels: { className: void 0, allowOverlap: !1, format: "", formatter: void 0, align: "right", style: { fontSize: 10, color: void 0 }, x: 0, y: 0 }, maxSize: 60, minSize: 10, legendIndex: 0, ranges: { value: void 0, borderColor: void 0, color: void 0, connectorColor: void 0 }, sizeBy: "area", sizeByAbsoluteValue: !1, zIndex: 1, zThreshold: 0
                    }
                }
            }); x = function () {
                function a(d, b) {
                this.options = this.symbols = this.visible = this.ranges = this.movementX = this.maxLabel = this.legendSymbol = this.legendItemWidth =
                    this.legendItemHeight = this.legendItem = this.legendGroup = this.legend = this.fontMetrics = this.chart = void 0; this.setState = y; this.init(d, b)
                } a.prototype.init = function (d, b) { this.options = d; this.visible = !0; this.chart = b.chart; this.legend = b }; a.prototype.addToLegend = function (d) { d.splice(this.options.legendIndex, 0, this) }; a.prototype.drawLegendSymbol = function (d) {
                    var b = this.chart, e = this.options, a = f(d.options.itemDistance, 20), c = e.ranges; var g = e.connectorDistance; this.fontMetrics = b.renderer.fontMetrics(e.labels.style.fontSize.toString() +
                        "px"); c && c.length && B(c[0].value) ? (l(c, function (b, e) { return e.value - b.value }), this.ranges = c, this.setOptions(), this.render(), b = this.getMaxLabelSize(), c = this.ranges[0].radius, d = 2 * c, g = g - c + b.width, g = 0 < g ? g : 0, this.maxLabel = b, this.movementX = "left" === e.labels.align ? g : 0, this.legendItemWidth = d + g + a, this.legendItemHeight = d + this.fontMetrics.h / 2) : d.options.bubbleLegend.autoRanges = !0
                }; a.prototype.setOptions = function () {
                    var d = this.ranges, b = this.options, e = this.chart.series[b.seriesIndex], a = this.legend.baseline, c = {
                        "z-index": b.zIndex,
                        "stroke-width": b.borderWidth
                    }, g = { "z-index": b.zIndex, "stroke-width": b.connectorWidth }, l = this.getLabelStyles(), h = e.options.marker.fillOpacity, p = this.chart.styledMode; d.forEach(function (m, q) {
                        p || (c.stroke = f(m.borderColor, b.borderColor, e.color), c.fill = f(m.color, b.color, 1 !== h ? w(e.color).setOpacity(h).get("rgba") : e.color), g.stroke = f(m.connectorColor, b.connectorColor, e.color)); d[q].radius = this.getRangeRadius(m.value); d[q] = u(d[q], { center: d[0].radius - d[q].radius + a }); p || u(!0, d[q], {
                            bubbleStyle: u(!1, c), connectorStyle: u(!1,
                                g), labelStyle: l
                        })
                    }, this)
                }; a.prototype.getLabelStyles = function () { var d = this.options, b = {}, e = "left" === d.labels.align, a = this.legend.options.rtl; h(d.labels.style, function (e, d) { "color" !== d && "fontSize" !== d && "z-index" !== d && (b[d] = e) }); return u(!1, b, { "font-size": d.labels.style.fontSize, fill: f(d.labels.style.color, "#000000"), "z-index": d.zIndex, align: a || e ? "right" : "left" }) }; a.prototype.getRangeRadius = function (d) {
                    var b = this.options; return this.chart.series[this.options.seriesIndex].getRadius.call(this, b.ranges[b.ranges.length -
                        1].value, b.ranges[0].value, b.minSize, b.maxSize, d)
                }; a.prototype.render = function () { var d = this.chart.renderer, b = this.options.zThreshold; this.symbols || (this.symbols = { connectors: [], bubbleItems: [], labels: [] }); this.legendSymbol = d.g("bubble-legend"); this.legendItem = d.g("bubble-legend-item"); this.legendSymbol.translateX = 0; this.legendSymbol.translateY = 0; this.ranges.forEach(function (e) { e.value >= b && this.renderRange(e) }, this); this.legendSymbol.add(this.legendItem); this.legendItem.add(this.legendGroup); this.hideOverlappingLabels() };
                a.prototype.renderRange = function (d) {
                    var b = this.options, e = b.labels, a = this.chart.renderer, c = this.symbols, g = c.labels, f = d.center, l = Math.abs(d.radius), h = b.connectorDistance || 0, p = e.align, q = e.style.fontSize; h = this.legend.options.rtl || "left" === p ? -h : h; e = b.connectorWidth; var u = this.ranges[0].radius || 0, y = f - l - b.borderWidth / 2 + e / 2; q = q / 2 - (this.fontMetrics.h - q) / 2; var k = a.styledMode; "center" === p && (h = 0, b.connectorDistance = 0, d.labelStyle.align = "center"); p = y + b.labels.y; var n = u + h + b.labels.x; c.bubbleItems.push(a.circle(u,
                        f + ((y % 1 ? 1 : .5) - (e % 2 ? 0 : .5)), l).attr(k ? {} : d.bubbleStyle).addClass((k ? "highcharts-color-" + this.options.seriesIndex + " " : "") + "highcharts-bubble-legend-symbol " + (b.className || "")).add(this.legendSymbol)); c.connectors.push(a.path(a.crispLine([["M", u, y], ["L", u + h, y]], b.connectorWidth)).attr(k ? {} : d.connectorStyle).addClass((k ? "highcharts-color-" + this.options.seriesIndex + " " : "") + "highcharts-bubble-legend-connectors " + (b.connectorClassName || "")).add(this.legendSymbol)); d = a.text(this.formatLabel(d), n, p + q).attr(k ?
                            {} : d.labelStyle).addClass("highcharts-bubble-legend-labels " + (b.labels.className || "")).add(this.legendSymbol); g.push(d); d.placed = !0; d.alignAttr = { x: n, y: p + q }
                }; a.prototype.getMaxLabelSize = function () { var d, b; this.symbols.labels.forEach(function (e) { b = e.getBBox(!0); d = d ? b.width > d.width ? b : d : b }); return d || {} }; a.prototype.formatLabel = function (d) { var b = this.options, e = b.labels.formatter; b = b.labels.format; var a = this.chart.numberFormatter; return b ? c.format(b, d) : e ? e.call(d) : a(d.value, 1) }; a.prototype.hideOverlappingLabels =
                    function () { var d = this.chart, b = this.symbols; !this.options.labels.allowOverlap && b && (d.hideOverlappingLabels(b.labels), b.labels.forEach(function (e, d) { e.newOpacity ? e.newOpacity !== e.oldOpacity && b.connectors[d].show() : b.connectors[d].hide() })) }; a.prototype.getRanges = function () {
                        var d = this.legend.bubbleLegend, b = d.options.ranges, e, a = Number.MAX_VALUE, c = -Number.MAX_VALUE; d.chart.series.forEach(function (b) {
                        b.isBubble && !b.ignoreSeries && (e = b.zData.filter(B), e.length && (a = f(b.options.zMin, Math.min(a, Math.max(A(e),
                            !1 === b.options.displayNegative ? b.options.zThreshold : -Number.MAX_VALUE))), c = f(b.options.zMax, Math.max(c, z(e)))))
                        }); var g = a === c ? [{ value: c }] : [{ value: a }, { value: (a + c) / 2 }, { value: c, autoRanges: !0 }]; b.length && b[0].radius && g.reverse(); g.forEach(function (e, d) { b && b[d] && (g[d] = u(!1, b[d], e)) }); return g
                    }; a.prototype.predictBubbleSizes = function () {
                        var d = this.chart, b = this.fontMetrics, e = d.legend.options, a = "horizontal" === e.layout, c = a ? d.legend.lastLineHeight : 0, g = d.plotSizeX, f = d.plotSizeY, l = d.series[this.options.seriesIndex];
                        d = Math.ceil(l.minPxSize); var h = Math.ceil(l.maxPxSize); l = l.options.maxSize; var p = Math.min(f, g); if (e.floating || !/%$/.test(l)) b = h; else if (l = parseFloat(l), b = (p + c - b.h / 2) * l / 100 / (l / 100 + 1), a && f - b >= g || !a && g - b >= f) b = h; return [d, Math.ceil(b)]
                    }; a.prototype.updateRanges = function (d, b) { var e = this.legend.options.bubbleLegend; e.minSize = d; e.maxSize = b; e.ranges = this.getRanges() }; a.prototype.correctSizes = function () {
                        var d = this.legend, b = this.chart.series[this.options.seriesIndex]; 1 < Math.abs(Math.ceil(b.maxPxSize) - this.options.maxSize) &&
                            (this.updateRanges(this.options.minSize, b.maxPxSize), d.render())
                    }; return a
            }(); r(n, "afterGetAllItems", function (a) { var d = this.bubbleLegend, b = this.options, e = b.bubbleLegend, c = this.chart.getVisibleBubbleSeriesIndex(); d && d.ranges && d.ranges.length && (e.ranges.length && (e.autoRanges = !!e.ranges[0].autoRanges), this.destroyItem(d)); 0 <= c && b.enabled && e.enabled && (e.seriesIndex = c, this.bubbleLegend = new k.BubbleLegend(e, this), this.bubbleLegend.addToLegend(a.allItems)) }); a.prototype.getVisibleBubbleSeriesIndex = function () {
                for (var a =
                    this.series, d = 0; d < a.length;) { if (a[d] && a[d].isBubble && a[d].visible && a[d].zData.length) return d; d++ } return -1
            }; n.prototype.getLinesHeights = function () { var a = this.allItems, d = [], b = a.length, e, c = 0; for (e = 0; e < b; e++)if (a[e].legendItemHeight && (a[e].itemHeight = a[e].legendItemHeight), a[e] === a[b - 1] || a[e + 1] && a[e]._legendItemPos[1] !== a[e + 1]._legendItemPos[1]) { d.push({ height: 0 }); var f = d[d.length - 1]; for (c; c <= e; c++)a[c].itemHeight > f.height && (f.height = a[c].itemHeight); f.step = e } return d }; n.prototype.retranslateItems = function (a) {
                var d,
                b, e, c = this.options.rtl, f = 0; this.allItems.forEach(function (g, l) { d = g.legendGroup.translateX; b = g._legendItemPos[1]; if ((e = g.movementX) || c && g.ranges) e = c ? d - g.options.maxSize / 2 : d + e, g.legendGroup.attr({ translateX: e }); l > a[f].step && f++; g.legendGroup.attr({ translateY: Math.round(b + a[f].height / 2) }); g._legendItemPos[1] = b + a[f].height / 2 })
            }; r(p, "legendItemClick", function () {
                var a = this.chart, d = this.visible, b = this.chart.legend; b && b.bubbleLegend && (this.visible = !d, this.ignoreSeries = d, a = 0 <= a.getVisibleBubbleSeriesIndex(),
                    b.bubbleLegend.visible !== a && (b.update({ bubbleLegend: { enabled: a } }), b.bubbleLegend.visible = a), this.visible = d)
            }); q(a.prototype, "drawChartBox", function (a, d, b) {
                var e = this.legend, c = 0 <= this.getVisibleBubbleSeriesIndex(); if (e && e.options.enabled && e.bubbleLegend && e.options.bubbleLegend.autoRanges && c) {
                    var f = e.bubbleLegend.options; c = e.bubbleLegend.predictBubbleSizes(); e.bubbleLegend.updateRanges(c[0], c[1]); f.placed || (e.group.placed = !1, e.allItems.forEach(function (b) { b.legendGroup.translateY = null })); e.render();
                    this.getMargins(); this.axes.forEach(function (b) { b.visible && b.render(); f.placed || (b.setScale(), b.updateNames(), h(b.ticks, function (b) { b.isNew = !0; b.isNewLabel = !0 })) }); f.placed = !0; this.getMargins(); a.call(this, d, b); e.bubbleLegend.correctSizes(); e.retranslateItems(e.getLinesHeights())
                } else a.call(this, d, b), e && e.options.enabled && e.bubbleLegend && (e.render(), e.retranslateItems(e.getLinesHeights()))
            }); k.BubbleLegend = x; return k.BubbleLegend
        }); z(a, "parts-more/BubbleSeries.js", [a["parts/Globals.js"], a["parts/Color.js"],
        a["parts/Point.js"], a["parts/Utilities.js"]], function (a, r, k, n) {
            var c = r.parse, w = n.arrayMax, z = n.arrayMin, A = n.clamp, B = n.extend, u = n.isNumber, h = n.pick, f = n.pInt; r = n.seriesType; n = a.Axis; var x = a.noop, l = a.Series, q = a.seriesTypes; r("bubble", "scatter", {
                dataLabels: { formatter: function () { return this.point.z }, inside: !0, verticalAlign: "middle" }, animationLimit: 250, marker: { lineColor: null, lineWidth: 1, fillOpacity: .5, radius: null, states: { hover: { radiusPlus: 0 } }, symbol: "circle" }, minSize: 8, maxSize: "20%", softThreshold: !1, states: { hover: { halo: { size: 5 } } },
                tooltip: { pointFormat: "({point.x}, {point.y}), Size: {point.z}" }, turboThreshold: 0, zThreshold: 0, zoneAxis: "z"
            }, {
                pointArrayMap: ["y", "z"], parallelArrays: ["x", "y", "z"], trackerGroups: ["group", "dataLabelsGroup"], specialGroup: "group", bubblePadding: !0, zoneAxis: "z", directTouch: !0, isBubble: !0, pointAttribs: function (a, f) { var g = this.options.marker.fillOpacity; a = l.prototype.pointAttribs.call(this, a, f); 1 !== g && (a.fill = c(a.fill).setOpacity(g).get("rgba")); return a }, getRadii: function (a, c, f) {
                    var d = this.zData, b = this.yData,
                    e = f.minPxSize, g = f.maxPxSize, l = []; var h = 0; for (f = d.length; h < f; h++) { var p = d[h]; l.push(this.getRadius(a, c, e, g, p, b[h])) } this.radii = l
                }, getRadius: function (a, c, f, d, b, e) { var g = this.options, l = "width" !== g.sizeBy, h = g.zThreshold, p = c - a, q = .5; if (null === e || null === b) return null; if (u(b)) { g.sizeByAbsoluteValue && (b = Math.abs(b - h), p = Math.max(c - h, Math.abs(a - h)), a = 0); if (b < a) return f / 2 - 1; 0 < p && (q = (b - a) / p) } l && 0 <= q && (q = Math.sqrt(q)); return Math.ceil(f + q * (d - f)) / 2 }, animate: function (a) {
                !a && this.points.length < this.options.animationLimit &&
                    this.points.forEach(function (a) { var c = a.graphic; c && c.width && (this.hasRendered || c.attr({ x: a.plotX, y: a.plotY, width: 1, height: 1 }), c.animate(this.markerAttribs(a), this.options.animation)) }, this)
                }, hasData: function () { return !!this.processedXData.length }, translate: function () {
                    var a, c = this.data, f = this.radii; q.scatter.prototype.translate.call(this); for (a = c.length; a--;) {
                        var d = c[a]; var b = f ? f[a] : 0; u(b) && b >= this.minPxSize / 2 ? (d.marker = B(d.marker, { radius: b, width: 2 * b, height: 2 * b }), d.dlBox = {
                            x: d.plotX - b, y: d.plotY - b, width: 2 *
                                b, height: 2 * b
                        }) : d.shapeArgs = d.plotY = d.dlBox = void 0
                    }
                }, alignDataLabel: q.column.prototype.alignDataLabel, buildKDTree: x, applyZones: x
            }, { haloPath: function (a) { return k.prototype.haloPath.call(this, 0 === a ? 0 : (this.marker ? this.marker.radius || 0 : 0) + a) }, ttBelow: !1 }); n.prototype.beforePadding = function () {
                var a = this, c = this.len, g = this.chart, d = 0, b = c, e = this.isXAxis, l = e ? "xData" : "yData", q = this.min, k = {}, n = Math.min(g.plotWidth, g.plotHeight), x = Number.MAX_VALUE, r = -Number.MAX_VALUE, C = this.max - q, B = c / C, D = []; this.series.forEach(function (b) {
                    var d =
                        b.options; !b.bubblePadding || !b.visible && g.options.chart.ignoreHiddenSeries || (a.allowZoomOutside = !0, D.push(b), e && (["minSize", "maxSize"].forEach(function (b) { var a = d[b], e = /%$/.test(a); a = f(a); k[b] = e ? n * a / 100 : a }), b.minPxSize = k.minSize, b.maxPxSize = Math.max(k.maxSize, k.minSize), b = b.zData.filter(u), b.length && (x = h(d.zMin, A(z(b), !1 === d.displayNegative ? d.zThreshold : -Number.MAX_VALUE, x)), r = h(d.zMax, Math.max(r, w(b))))))
                }); D.forEach(function (c) {
                    var f = c[l], g = f.length; e && c.getRadii(x, r, c); if (0 < C) for (; g--;)if (u(f[g]) &&
                        a.dataMin <= f[g] && f[g] <= a.max) { var h = c.radii ? c.radii[g] : 0; d = Math.min((f[g] - q) * B - h, d); b = Math.max((f[g] - q) * B + h, b) }
                }); D.length && 0 < C && !this.logarithmic && (b -= c, B *= (c + Math.max(0, d) - Math.min(b, c)) / c, [["min", "userMin", d], ["max", "userMax", b]].forEach(function (b) { "undefined" === typeof h(a.options[b[0]], a[b[1]]) && (a[b[0]] += b[2] / B) }))
            }; ""
        }); z(a, "parts-map/MapBubbleSeries.js", [a["parts/Globals.js"], a["parts/Point.js"], a["parts/Utilities.js"]], function (a, r, k) {
            var n = k.merge; k = k.seriesType; var c = a.seriesTypes; c.bubble &&
                k("mapbubble", "bubble", { animationLimit: 500, tooltip: { pointFormat: "{point.name}: {point.z}" } }, { xyFromShape: !0, type: "mapbubble", pointArrayMap: ["z"], getMapData: c.map.prototype.getMapData, getBox: c.map.prototype.getBox, setData: c.map.prototype.setData, setOptions: c.map.prototype.setOptions }, {
                    applyOptions: function (a, k) {
                        return a && "undefined" !== typeof a.lat && "undefined" !== typeof a.lon ? r.prototype.applyOptions.call(this, n(a, this.series.chart.fromLatLonToPoint(a)), k) : c.map.prototype.pointClass.prototype.applyOptions.call(this,
                            a, k)
                    }, isValid: function () { return "number" === typeof this.z }, ttBelow: !1
                }); ""
        }); z(a, "parts-map/HeatmapSeries.js", [a["parts/Globals.js"], a["mixins/legend-symbol.js"], a["parts/SVGRenderer.js"], a["parts/Utilities.js"]], function (a, r, k, n) {
            var c = n.clamp, w = n.extend, z = n.fireEvent, A = n.isNumber, B = n.merge, u = n.pick; n = n.seriesType; ""; var h = a.colorMapPointMixin, f = a.Series, x = k.prototype.symbols; n("heatmap", "scatter", {
                animation: !1, borderWidth: 0, nullColor: "#f7f7f7", dataLabels: {
                    formatter: function () { return this.point.value },
                    inside: !0, verticalAlign: "middle", crop: !1, overflow: !1, padding: 0
                }, marker: { symbol: "rect", radius: 0, lineColor: void 0, states: { hover: { lineWidthPlus: 0 }, select: {} } }, clip: !0, pointRange: null, tooltip: { pointFormat: "{point.x}, {point.y}: {point.value}<br/>" }, states: { hover: { halo: !1, brightness: .2 } }
            }, B(a.colorMapSeriesMixin, {
                pointArrayMap: ["y", "value"], hasPointSpecificOptions: !0, getExtremesFromAll: !0, directTouch: !0, init: function () {
                    f.prototype.init.apply(this, arguments); var a = this.options; a.pointRange = u(a.pointRange,
                        a.colsize || 1); this.yAxis.axisPointRange = a.rowsize || 1; w(x, { ellipse: x.circle, rect: x.square })
                }, getSymbol: f.prototype.getSymbol, setClip: function (a) { var c = this.chart; f.prototype.setClip.apply(this, arguments); (!1 !== this.options.clip || a) && this.markerGroup.clip((a || this.clipBox) && this.sharedClipKey ? c[this.sharedClipKey] : c.clipRect) }, translate: function () {
                    var a = this.options, c = a.marker && a.marker.symbol || "", f = x[c] ? c : "rect"; a = this.options; var h = -1 !== ["circle", "square"].indexOf(f); this.generatePoints(); this.points.forEach(function (a) {
                        var d =
                            a.getCellAttributes(), b = { x: Math.min(d.x1, d.x2), y: Math.min(d.y1, d.y2), width: Math.max(Math.abs(d.x2 - d.x1), 0), height: Math.max(Math.abs(d.y2 - d.y1), 0) }; var e = a.hasImage = 0 === (a.marker && a.marker.symbol || c || "").indexOf("url"); if (h) { var g = Math.abs(b.width - b.height); b.x = Math.min(d.x1, d.x2) + (b.width < b.height ? 0 : g / 2); b.y = Math.min(d.y1, d.y2) + (b.width < b.height ? g / 2 : 0); b.width = b.height = Math.min(b.width, b.height) } g = {
                                plotX: (d.x1 + d.x2) / 2, plotY: (d.y1 + d.y2) / 2, clientX: (d.x1 + d.x2) / 2, shapeType: "path", shapeArgs: B(!0, b, {
                                    d: x[f](b.x,
                                        b.y, b.width, b.height)
                                })
                            }; e && (a.marker = { width: b.width, height: b.height }); w(a, g)
                    }); z(this, "afterTranslate")
                }, pointAttribs: function (c, h) {
                    var l = f.prototype.pointAttribs.call(this, c, h), q = this.options || {}, g = this.chart.options.plotOptions || {}, d = g.series || {}, b = g.heatmap || {}; g = q.borderColor || b.borderColor || d.borderColor; d = q.borderWidth || b.borderWidth || d.borderWidth || l["stroke-width"]; l.stroke = c && c.marker && c.marker.lineColor || q.marker && q.marker.lineColor || g || this.color; l["stroke-width"] = d; h && (c = B(q.states[h],
                        q.marker && q.marker.states[h], c.options.states && c.options.states[h] || {}), h = c.brightness, l.fill = c.color || a.color(l.fill).brighten(h || 0).get(), l.stroke = c.lineColor); return l
                }, markerAttribs: function (a, c) {
                    var f = a.marker || {}, h = this.options.marker || {}, g = a.shapeArgs || {}, d = {}; if (a.hasImage) return { x: a.plotX, y: a.plotY }; if (c) {
                        var b = h.states[c] || {}; var e = f.states && f.states[c] || {};[["width", "x"], ["height", "y"]].forEach(function (a) {
                        d[a[0]] = (e[a[0]] || b[a[0]] || g[a[0]]) + (e[a[0] + "Plus"] || b[a[0] + "Plus"] || 0); d[a[1]] =
                            g[a[1]] + (g[a[0]] - d[a[0]]) / 2
                        })
                    } return c ? d : g
                }, drawPoints: function () { var a = this; if ((this.options.marker || {}).enabled || this._hasPointMarkers) f.prototype.drawPoints.call(this), this.points.forEach(function (c) { c.graphic && c.graphic[a.chart.styledMode ? "css" : "animate"](a.colorAttribs(c)) }) }, hasData: function () { return !!this.processedXData.length }, getValidPoints: function (a, c) { return f.prototype.getValidPoints.call(this, a, c, !0) }, getBox: a.noop, drawLegendSymbol: r.drawRectangle, alignDataLabel: a.seriesTypes.column.prototype.alignDataLabel,
                getExtremes: function () { var a = f.prototype.getExtremes.call(this, this.valueData), c = a.dataMin; a = a.dataMax; A(c) && (this.valueMin = c); A(a) && (this.valueMax = a); return f.prototype.getExtremes.call(this) }
            }), B(h, {
                applyOptions: function (c, f) { c = a.Point.prototype.applyOptions.call(this, c, f); c.formatPrefix = c.isNull || null === c.value ? "null" : "point"; return c }, isValid: function () { return Infinity !== this.value && -Infinity !== this.value }, haloPath: function (a) {
                    if (!a) return []; var c = this.shapeArgs; return ["M", c.x - a, c.y - a, "L", c.x -
                        a, c.y + c.height + a, c.x + c.width + a, c.y + c.height + a, c.x + c.width + a, c.y - a, "Z"]
                }, getCellAttributes: function () {
                    var a = this.series, f = a.options, h = (f.colsize || 1) / 2, k = (f.rowsize || 1) / 2, g = a.xAxis, d = a.yAxis, b = this.options.marker || a.options.marker; a = a.pointPlacementToXValue(); var e = u(this.pointPadding, f.pointPadding, 0), m = {
                        x1: c(Math.round(g.len - (g.translate(this.x - h, !1, !0, !1, !0, -a) || 0)), -g.len, 2 * g.len), x2: c(Math.round(g.len - (g.translate(this.x + h, !1, !0, !1, !0, -a) || 0)), -g.len, 2 * g.len), y1: c(Math.round(d.translate(this.y -
                            k, !1, !0, !1, !0) || 0), -d.len, 2 * d.len), y2: c(Math.round(d.translate(this.y + k, !1, !0, !1, !0) || 0), -d.len, 2 * d.len)
                    };[["width", "x"], ["height", "y"]].forEach(function (a) { var c = a[0]; a = a[1]; var d = a + "1", f = a + "2", g = Math.abs(m[d] - m[f]), h = b && b.lineWidth || 0, l = Math.abs(m[d] + m[f]) / 2; b[c] && b[c] < g && (m[d] = l - b[c] / 2 - h / 2, m[f] = l + b[c] / 2 + h / 2); e && ("y" === a && (d = f, f = a + "1"), m[d] += e, m[f] -= e) }); return m
                }
            })); ""
        }); z(a, "parts-map/GeoJSON.js", [a["parts/Chart.js"], a["parts/Globals.js"], a["parts/Utilities.js"]], function (a, r, k) {
            function n(a,
                c) { var f, h = !1, l = a.x, q = a.y; a = 0; for (f = c.length - 1; a < c.length; f = a++) { var p = c[a][1] > q; var k = c[f][1] > q; p !== k && l < (c[f][0] - c[a][0]) * (q - c[a][1]) / (c[f][1] - c[a][1]) + c[a][0] && (h = !h) } return h } var c = r.win, w = k.error, z = k.extend, A = k.format, B = k.merge; k = k.wrap; ""; a.prototype.transformFromLatLon = function (a, h) {
                    var f, k = (null === (f = this.userOptions.chart) || void 0 === f ? void 0 : f.proj4) || c.proj4; if (!k) return w(21, !1, this), { x: 0, y: null }; a = k(h.crs, [a.lon, a.lat]); f = h.cosAngle || h.rotation && Math.cos(h.rotation); k = h.sinAngle || h.rotation &&
                        Math.sin(h.rotation); a = h.rotation ? [a[0] * f + a[1] * k, -a[0] * k + a[1] * f] : a; return { x: ((a[0] - (h.xoffset || 0)) * (h.scale || 1) + (h.xpan || 0)) * (h.jsonres || 1) + (h.jsonmarginX || 0), y: (((h.yoffset || 0) - a[1]) * (h.scale || 1) + (h.ypan || 0)) * (h.jsonres || 1) - (h.jsonmarginY || 0) }
                }; a.prototype.transformToLatLon = function (a, h) {
                    if ("undefined" === typeof c.proj4) w(21, !1, this); else {
                        a = {
                            x: ((a.x - (h.jsonmarginX || 0)) / (h.jsonres || 1) - (h.xpan || 0)) / (h.scale || 1) + (h.xoffset || 0), y: ((-a.y - (h.jsonmarginY || 0)) / (h.jsonres || 1) + (h.ypan || 0)) / (h.scale || 1) + (h.yoffset ||
                                0)
                        }; var f = h.cosAngle || h.rotation && Math.cos(h.rotation), k = h.sinAngle || h.rotation && Math.sin(h.rotation); h = c.proj4(h.crs, "WGS84", h.rotation ? { x: a.x * f + a.y * -k, y: a.x * k + a.y * f } : a); return { lat: h.y, lon: h.x }
                    }
                }; a.prototype.fromPointToLatLon = function (a) { var c = this.mapTransforms, f; if (c) { for (f in c) if (Object.hasOwnProperty.call(c, f) && c[f].hitZone && n({ x: a.x, y: -a.y }, c[f].hitZone.coordinates[0])) return this.transformToLatLon(a, c[f]); return this.transformToLatLon(a, c["default"]) } w(22, !1, this) }; a.prototype.fromLatLonToPoint =
                    function (a) { var c = this.mapTransforms, f; if (!c) return w(22, !1, this), { x: 0, y: null }; for (f in c) if (Object.hasOwnProperty.call(c, f) && c[f].hitZone) { var k = this.transformFromLatLon(a, c[f]); if (n({ x: k.x, y: -k.y }, c[f].hitZone.coordinates[0])) return k } return this.transformFromLatLon(a, c["default"]) }; r.geojson = function (a, c, f) {
                        var h = [], l = [], k = function (a) { a.forEach(function (a, c) { 0 === c ? l.push(["M", a[0], -a[1]]) : l.push(["L", a[0], -a[1]]) }) }; c = c || "map"; a.features.forEach(function (a) {
                            var f = a.geometry, g = f.type; f = f.coordinates;
                            a = a.properties; var d; l = []; "map" === c || "mapbubble" === c ? ("Polygon" === g ? (f.forEach(k), l.push(["Z"])) : "MultiPolygon" === g && (f.forEach(function (a) { a.forEach(k) }), l.push(["Z"])), l.length && (d = { path: l })) : "mapline" === c ? ("LineString" === g ? k(f) : "MultiLineString" === g && f.forEach(k), l.length && (d = { path: l })) : "mappoint" === c && "Point" === g && (d = { x: f[0], y: -f[1] }); d && h.push(z(d, { name: a.name || a.NAME, properties: a }))
                        }); f && a.copyrightShort && (f.chart.mapCredits = A(f.chart.options.credits.mapText, { geojson: a }), f.chart.mapCreditsFull =
                            A(f.chart.options.credits.mapTextFull, { geojson: a })); return h
                    }; k(a.prototype, "addCredits", function (a, c) { c = B(!0, this.options.credits, c); this.mapCredits && (c.href = null); a.call(this, c); this.credits && this.mapCreditsFull && this.credits.attr({ title: this.mapCreditsFull }) })
        }); z(a, "parts-map/Map.js", [a["parts/Chart.js"], a["parts/Globals.js"], a["parts/Options.js"], a["parts/SVGRenderer.js"], a["parts/Utilities.js"]], function (a, r, k, n, c) {
            function w(a, c, h, k, p, n, g, d) {
                return [["M", a + p, c], ["L", a + h - n, c], ["C", a + h - n / 2, c,
                    a + h, c + n / 2, a + h, c + n], ["L", a + h, c + k - g], ["C", a + h, c + k - g / 2, a + h - g / 2, c + k, a + h - g, c + k], ["L", a + d, c + k], ["C", a + d / 2, c + k, a, c + k - d / 2, a, c + k - d], ["L", a, c + p], ["C", a, c + p / 2, a + p / 2, c, a + p, c], ["Z"]]
            } k = k.defaultOptions; var z = c.extend, A = c.getOptions, B = c.merge, u = c.pick; c = r.Renderer; var h = r.VMLRenderer; z(k.lang, { zoomIn: "Zoom in", zoomOut: "Zoom out" }); k.mapNavigation = {
                buttonOptions: {
                    alignTo: "plotBox", align: "left", verticalAlign: "top", x: 0, width: 18, height: 18, padding: 5, style: { fontSize: "15px", fontWeight: "bold" }, theme: {
                        "stroke-width": 1,
                        "text-align": "center"
                    }
                }, buttons: { zoomIn: { onclick: function () { this.mapZoom(.5) }, text: "+", y: 0 }, zoomOut: { onclick: function () { this.mapZoom(2) }, text: "-", y: 28 } }, mouseWheelSensitivity: 1.1
            }; r.splitPath = function (a) { "string" === typeof a && (a = a.replace(/([A-Za-z])/g, " $1 ").replace(/^\s*/, "").replace(/\s*$/, ""), a = a.split(/[ ,;]+/).map(function (a) { return /[A-za-z]/.test(a) ? a : parseFloat(a) })); return n.prototype.pathToSegments(a) }; r.maps = {}; n.prototype.symbols.topbutton = function (a, c, h, k, p) {
                p = p && p.r || 0; return w(a - 1,
                    c - 1, h, k, p, p, 0, 0)
            }; n.prototype.symbols.bottombutton = function (a, c, h, k, p) { p = p && p.r || 0; return w(a - 1, c - 1, h, k, 0, 0, p, p) }; c === h && ["topbutton", "bottombutton"].forEach(function (a) { h.prototype.symbols[a] = n.prototype.symbols[a] }); r.Map = r.mapChart = function (c, h, k) {
                var f = "string" === typeof c || c.nodeName, l = arguments[f ? 1 : 0], n = l, g = { endOnTick: !1, visible: !1, minPadding: 0, maxPadding: 0, startOnTick: !1 }, d = A().credits; var b = l.series; l.series = null; l = B({
                    chart: { panning: { enabled: !0, type: "xy" }, type: "map" }, credits: {
                        mapText: u(d.mapText,
                            ' \u00a9 <a href="{geojson.copyrightUrl}">{geojson.copyrightShort}</a>'), mapTextFull: u(d.mapTextFull, "{geojson.copyright}")
                    }, tooltip: { followTouchMove: !1 }, xAxis: g, yAxis: B(g, { reversed: !0 })
                }, l, { chart: { inverted: !1, alignTicks: !1 } }); l.series = n.series = b; return f ? new a(c, l, k) : new a(l, h)
            }
        }); z(a, "masters/modules/map.src.js", [], function () { })
});
//# sourceMappingURL=map.js.map