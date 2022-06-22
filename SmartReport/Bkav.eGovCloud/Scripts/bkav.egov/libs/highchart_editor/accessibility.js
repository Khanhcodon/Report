﻿/*
 Highcharts JS v8.1.2 (2020-06-16)

 Accessibility module

 (c) 2010-2019 Highsoft AS
 Author: Oystein Moseng

 License: www.highcharts.com/license
*/
(function (a) { "object" === typeof module && module.exports ? (a["default"] = a, module.exports = a) : "function" === typeof define && define.amd ? define("highcharts/modules/accessibility", ["highcharts"], function (r) { a(r); a.Highcharts = r; return a }) : a("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (a) {
    function r(a, g, q, n) { a.hasOwnProperty(g) || (a[g] = n.apply(null, q)) } a = a ? a._modules : {}; r(a, "modules/accessibility/utils/htmlUtilities.js", [a["parts/Utilities.js"], a["parts/Globals.js"]], function (a, g) {
        function l(a) {
            return a.replace(/&/g,
                "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&#x27;").replace(/\//g, "&#x2F;")
        } var n = a.merge, p = g.win, f = p.document; return {
            addClass: function (a, f) { a.classList ? a.classList.add(f) : 0 > a.className.indexOf(f) && (a.className += f) }, escapeStringForHTML: l, getElement: function (a) { return f.getElementById(a) }, getFakeMouseEvent: function (a) {
                if ("function" === typeof p.MouseEvent) return new p.MouseEvent(a); if (f.createEvent) {
                    var h = f.createEvent("MouseEvent"); if (h.initMouseEvent) return h.initMouseEvent(a,
                        !0, !0, p, "click" === a ? 1 : 0, 0, 0, 0, 0, !1, !1, !1, !1, 0, null), h
                } return { type: a }
            }, removeElement: function (a) { a && a.parentNode && a.parentNode.removeChild(a) }, reverseChildNodes: function (a) { for (var f = a.childNodes.length; f--;)a.appendChild(a.childNodes[f]) }, setElAttrs: function (a, f) { Object.keys(f).forEach(function (k) { var e = f[k]; null === e ? a.removeAttribute(k) : (e = l("" + e), a.setAttribute(k, e)) }) }, stripHTMLTagsFromString: function (a) { return "string" === typeof a ? a.replace(/<\/?[^>]+(>|$)/g, "") : a }, visuallyHideElement: function (a) {
                n(!0,
                    a.style, { position: "absolute", width: "1px", height: "1px", overflow: "hidden", whiteSpace: "nowrap", clip: "rect(1px, 1px, 1px, 1px)", marginTop: "-3px", "-ms-filter": "progid:DXImageTransform.Microsoft.Alpha(Opacity=1)", filter: "alpha(opacity=1)", opacity: "0.01" })
            }
        }
    }); r(a, "modules/accessibility/utils/chartUtilities.js", [a["modules/accessibility/utils/htmlUtilities.js"], a["parts/Utilities.js"]], function (a, g) {
        function l(e) { if (e.points && e.points.length && e.points[0].graphic) return e.points[0].graphic.element } function n(e) {
            var d =
                l(e); return d && d.parentNode || e.graph && e.graph.element || e.group && e.group.element
        } function p(e, d) { d.setAttribute("aria-hidden", !1); d !== e.renderTo && d.parentNode && (Array.prototype.forEach.call(d.parentNode.childNodes, function (b) { b.hasAttribute("aria-hidden") || b.setAttribute("aria-hidden", !0) }), p(e, d.parentNode)) } var f = a.stripHTMLTagsFromString, h = g.defined, x = g.find, k = g.fireEvent; return {
            getChartTitle: function (e) { return f(e.options.title.text || e.langFormat("accessibility.defaultChartTitle", { chart: e })) },
            getAxisDescription: function (e) { return f(e && (e.userOptions && e.userOptions.accessibility && e.userOptions.accessibility.description || e.axisTitle && e.axisTitle.textStr || e.options.id || e.categories && "categories" || e.dateTime && "Time" || "values")) }, getPointFromXY: function (e, d, b) { for (var m = e.length, c; m--;)if (c = x(e[m].points || [], function (c) { return c.x === d && c.y === b })) return c }, getSeriesFirstPointElement: l, getSeriesFromName: function (e, d) { return d ? (e.series || []).filter(function (b) { return b.name === d }) : e.series }, getSeriesA11yElement: n,
            unhideChartElementFromAT: p, hideSeriesFromAT: function (e) { (e = n(e)) && e.setAttribute("aria-hidden", !0) }, scrollToPoint: function (e) {
                var d = e.series.xAxis, b = e.series.yAxis, m = (null === d || void 0 === d ? 0 : d.scrollbar) ? d : b; if ((d = null === m || void 0 === m ? void 0 : m.scrollbar) && h(d.to) && h(d.from)) {
                    b = d.to - d.from; if (h(m.dataMin) && h(m.dataMax)) { var c = m.toPixels(m.dataMin), t = m.toPixels(m.dataMax); e = (m.toPixels(e["xAxis" === m.coll ? "x" : "y"] || 0) - c) / (t - c) } else e = 0; d.updatePosition(e - b / 2, e + b / 2); k(d, "changed", {
                        from: d.from, to: d.to,
                        trigger: "scrollbar", DOMEvent: null
                    })
                }
            }
        }
    }); r(a, "modules/accessibility/KeyboardNavigationHandler.js", [a["parts/Utilities.js"]], function (a) {
        function l(a, l) { this.chart = a; this.keyCodeMap = l.keyCodeMap || []; this.validate = l.validate; this.init = l.init; this.terminate = l.terminate; this.response = { success: 1, prev: 2, next: 3, noHandler: 4, fail: 5 } } var q = a.find; l.prototype = {
            run: function (a) {
                var l = a.which || a.keyCode, f = this.response.noHandler, h = q(this.keyCodeMap, function (a) { return -1 < a[0].indexOf(l) }); h ? f = h[1].call(this, l, a) :
                    9 === l && (f = this.response[a.shiftKey ? "prev" : "next"]); return f
            }
        }; return l
    }); r(a, "modules/accessibility/utils/EventProvider.js", [a["parts/Globals.js"], a["parts/Utilities.js"]], function (a, g) { var l = g.addEvent; g = g.extend; var n = function () { this.eventRemovers = [] }; g(n.prototype, { addEvent: function () { var g = l.apply(a, arguments); this.eventRemovers.push(g); return g }, removeAddedEvents: function () { this.eventRemovers.forEach(function (a) { a() }); this.eventRemovers = [] } }); return n }); r(a, "modules/accessibility/utils/DOMElementProvider.js",
        [a["parts/Globals.js"], a["parts/Utilities.js"], a["modules/accessibility/utils/htmlUtilities.js"]], function (a, g, q) { var l = a.win.document; a = g.extend; var p = q.removeElement; q = function () { this.elements = [] }; a(q.prototype, { createElement: function () { var a = l.createElement.apply(l, arguments); this.elements.push(a); return a }, destroyCreatedElements: function () { this.elements.forEach(function (a) { p(a) }); this.elements = [] } }); return q }); r(a, "modules/accessibility/AccessibilityComponent.js", [a["parts/Globals.js"], a["parts/Utilities.js"],
        a["modules/accessibility/utils/htmlUtilities.js"], a["modules/accessibility/utils/chartUtilities.js"], a["modules/accessibility/utils/EventProvider.js"], a["modules/accessibility/utils/DOMElementProvider.js"]], function (a, g, q, n, p, f) {
            function h() { } var l = a.win, k = l.document; a = g.extend; var e = g.fireEvent, d = g.merge, b = q.removeElement, m = q.getFakeMouseEvent, c = n.unhideChartElementFromAT; h.prototype = {
                initBase: function (b) {
                    this.chart = b; this.eventProvider = new p; this.domElementProvider = new f; this.keyCodes = {
                        left: 37,
                        right: 39, up: 38, down: 40, enter: 13, space: 32, esc: 27, tab: 9
                    }
                }, addEvent: function () { return this.eventProvider.addEvent.apply(this.eventProvider, arguments) }, createElement: function () { return this.domElementProvider.createElement.apply(this.domElementProvider, arguments) }, fireEventOnWrappedOrUnwrappedElement: function (b, c) { var d = c.type; k.createEvent && (b.dispatchEvent || b.fireEvent) ? b.dispatchEvent ? b.dispatchEvent(c) : b.fireEvent(d, c) : e(b, d, c) }, fakeClickEvent: function (b) {
                    if (b) {
                        var c = m("click"); this.fireEventOnWrappedOrUnwrappedElement(b,
                            c)
                    }
                }, addProxyGroup: function (b) { this.createOrUpdateProxyContainer(); var c = this.createElement("div"); Object.keys(b || {}).forEach(function (d) { null !== b[d] && c.setAttribute(d, b[d]) }); this.chart.a11yProxyContainer.appendChild(c); return c }, createOrUpdateProxyContainer: function () { var b = this.chart, c = b.renderer.box; b.a11yProxyContainer = b.a11yProxyContainer || this.createProxyContainerElement(); c.nextSibling !== b.a11yProxyContainer && b.container.insertBefore(b.a11yProxyContainer, c.nextSibling) }, createProxyContainerElement: function () {
                    var b =
                        k.createElement("div"); b.className = "highcharts-a11y-proxy-container"; return b
                }, createProxyButton: function (b, a, m, e, k) {
                    var t = b.element, w = this.createElement("button"), f = d({ "aria-label": t.getAttribute("aria-label") }, m); Object.keys(f).forEach(function (b) { null !== f[b] && w.setAttribute(b, f[b]) }); w.className = "highcharts-a11y-proxy-button"; k && this.addEvent(w, "click", k); this.setProxyButtonStyle(w); this.updateProxyButtonPosition(w, e || b); this.proxyMouseEventsForButton(t, w); a.appendChild(w); f["aria-hidden"] || c(this.chart,
                        w); return w
                }, getElementPosition: function (b) { var c = b.element; return (b = this.chart.renderTo) && c && c.getBoundingClientRect ? (c = c.getBoundingClientRect(), b = b.getBoundingClientRect(), { x: c.left - b.left, y: c.top - b.top, width: c.right - c.left, height: c.bottom - c.top }) : { x: 0, y: 0, width: 1, height: 1 } }, setProxyButtonStyle: function (b) {
                    d(!0, b.style, {
                        "border-width": 0, "background-color": "transparent", cursor: "pointer", outline: "none", opacity: .001, filter: "alpha(opacity=1)", "-ms-filter": "progid:DXImageTransform.Microsoft.Alpha(Opacity=1)",
                        zIndex: 999, overflow: "hidden", padding: 0, margin: 0, display: "block", position: "absolute"
                    })
                }, updateProxyButtonPosition: function (b, c) { c = this.getElementPosition(c); d(!0, b.style, { width: (c.width || 1) + "px", height: (c.height || 1) + "px", left: (c.x || 0) + "px", top: (c.y || 0) + "px" }) }, proxyMouseEventsForButton: function (b, c) {
                    var d = this; "click touchstart touchend touchcancel touchmove mouseover mouseenter mouseleave mouseout".split(" ").forEach(function (t) {
                        var a = 0 === t.indexOf("touch"); d.addEvent(c, t, function (c) {
                            var t = a ? d.cloneTouchEvent(c) :
                                d.cloneMouseEvent(c); b && d.fireEventOnWrappedOrUnwrappedElement(b, t); c.stopPropagation(); c.preventDefault()
                        })
                    })
                }, cloneMouseEvent: function (b) { if ("function" === typeof l.MouseEvent) return new l.MouseEvent(b.type, b); if (k.createEvent) { var c = k.createEvent("MouseEvent"); if (c.initMouseEvent) return c.initMouseEvent(b.type, b.bubbles, b.cancelable, b.view || l, b.detail, b.screenX, b.screenY, b.clientX, b.clientY, b.ctrlKey, b.altKey, b.shiftKey, b.metaKey, b.button, b.relatedTarget), c } return m(b.type) }, cloneTouchEvent: function (b) {
                    var c =
                        function (b) { for (var c = [], d = 0; d < b.length; ++d) { var a = b.item(d); a && c.push(a) } return c }; if ("function" === typeof l.TouchEvent) return c = new l.TouchEvent(b.type, { touches: c(b.touches), targetTouches: c(b.targetTouches), changedTouches: c(b.changedTouches), ctrlKey: b.ctrlKey, shiftKey: b.shiftKey, altKey: b.altKey, metaKey: b.metaKey, bubbles: b.bubbles, cancelable: b.cancelable, composed: b.composed, detail: b.detail, view: b.view }), b.defaultPrevented && c.preventDefault(), c; c = this.cloneMouseEvent(b); c.touches = b.touches; c.changedTouches =
                            b.changedTouches; c.targetTouches = b.targetTouches; return c
                }, destroyBase: function () { b(this.chart.a11yProxyContainer); this.domElementProvider.destroyCreatedElements(); this.eventProvider.removeAddedEvents() }
            }; a(h.prototype, { init: function () { }, getKeyboardNavigation: function () { }, onChartUpdate: function () { }, onChartRender: function () { }, destroy: function () { } }); return h
        }); r(a, "modules/accessibility/KeyboardNavigation.js", [a["parts/Globals.js"], a["parts/Utilities.js"], a["modules/accessibility/utils/htmlUtilities.js"],
        a["modules/accessibility/utils/EventProvider.js"]], function (a, g, q, n) {
            function l(d, b) { this.init(d, b) } var f = a.doc, h = a.win, x = g.addEvent, k = g.fireEvent, e = q.getElement; x(f, "keydown", function (d) { 27 === (d.which || d.keyCode) && a.charts && a.charts.forEach(function (b) { b && b.dismissPopupContent && b.dismissPopupContent() }) }); a.Chart.prototype.dismissPopupContent = function () { var d = this; k(this, "dismissPopupContent", {}, function () { d.tooltip && d.tooltip.hide(0); d.hideExportMenu() }) }; l.prototype = {
                init: function (d, b) {
                    var a = this,
                        c = this.eventProvider = new n; this.chart = d; this.components = b; this.modules = []; this.currentModuleIx = 0; this.update(); c.addEvent(d.renderTo, "keydown", function (b) { return a.onKeydown(b) }); c.addEvent(this.tabindexContainer, "focus", function (b) { return a.onFocus(b) }); c.addEvent(f, "mouseup", function () { return a.onMouseUp() }); c.addEvent(d.renderTo, "mousedown", function () { a.isClickingChart = !0 }); c.addEvent(d.renderTo, "mouseover", function () { a.pointerIsOverChart = !0 }); c.addEvent(d.renderTo, "mouseout", function () {
                            a.pointerIsOverChart =
                                !1
                        }); this.modules.length && this.modules[0].init(1)
                }, update: function (d) { var b = this.chart.options.accessibility; b = b && b.keyboardNavigation; var a = this.components; this.updateContainerTabindex(); b && b.enabled && d && d.length ? (this.modules = d.reduce(function (b, d) { d = a[d].getKeyboardNavigation(); return b.concat(d) }, []), this.updateExitAnchor()) : (this.modules = [], this.currentModuleIx = 0, this.removeExitAnchor()) }, onFocus: function (d) {
                    var b, a = this.chart; d = d.relatedTarget && a.container.contains(d.relatedTarget); this.isClickingChart ||
                        d || (null === (b = this.modules[0]) || void 0 === b ? void 0 : b.init(1))
                }, onMouseUp: function () { delete this.isClickingChart; if (!this.keyboardReset && !this.pointerIsOverChart) { var d = this.chart, b = this.modules && this.modules[this.currentModuleIx || 0]; b && b.terminate && b.terminate(); d.focusElement && d.focusElement.removeFocusBorder(); this.currentModuleIx = 0; this.keyboardReset = !0 } }, onKeydown: function (d) {
                    d = d || h.event; var b, a = this.modules && this.modules.length && this.modules[this.currentModuleIx]; this.keyboardReset = !1; if (a) {
                        var c =
                            a.run(d); c === a.response.success ? b = !0 : c === a.response.prev ? b = this.prev() : c === a.response.next && (b = this.next()); b && (d.preventDefault(), d.stopPropagation())
                    }
                }, prev: function () { return this.move(-1) }, next: function () { return this.move(1) }, move: function (d) {
                    var b = this.modules && this.modules[this.currentModuleIx]; b && b.terminate && b.terminate(d); this.chart.focusElement && this.chart.focusElement.removeFocusBorder(); this.currentModuleIx += d; if (b = this.modules && this.modules[this.currentModuleIx]) {
                        if (b.validate && !b.validate()) return this.move(d);
                        if (b.init) return b.init(d), !0
                    } this.currentModuleIx = 0; 0 < d ? (this.exiting = !0, this.exitAnchor.focus()) : this.tabindexContainer.focus(); return !1
                }, updateExitAnchor: function () { var d = e("highcharts-end-of-chart-marker-" + this.chart.index); this.removeExitAnchor(); d ? (this.makeElementAnExitAnchor(d), this.exitAnchor = d) : this.createExitAnchor() }, updateContainerTabindex: function () {
                    var d = this.chart.options.accessibility; d = d && d.keyboardNavigation; d = !(d && !1 === d.enabled); var b = this.chart, a = b.container; b.renderTo.hasAttribute("tabindex") &&
                        (a.removeAttribute("tabindex"), a = b.renderTo); this.tabindexContainer = a; var c = a.getAttribute("tabindex"); d && !c ? a.setAttribute("tabindex", "0") : d || b.container.removeAttribute("tabindex")
                }, makeElementAnExitAnchor: function (d) { var b = this.tabindexContainer.getAttribute("tabindex") || 0; d.setAttribute("class", "highcharts-exit-anchor"); d.setAttribute("tabindex", b); d.setAttribute("aria-hidden", !1); this.addExitAnchorEventsToEl(d) }, createExitAnchor: function () {
                    var d = this.chart, b = this.exitAnchor = f.createElement("div");
                    d.renderTo.appendChild(b); this.makeElementAnExitAnchor(b)
                }, removeExitAnchor: function () { this.exitAnchor && this.exitAnchor.parentNode && (this.exitAnchor.parentNode.removeChild(this.exitAnchor), delete this.exitAnchor) }, addExitAnchorEventsToEl: function (d) {
                    var b = this.chart, a = this; this.eventProvider.addEvent(d, "focus", function (c) {
                        c = c || h.event; c.relatedTarget && b.container.contains(c.relatedTarget) || a.exiting ? a.exiting = !1 : (b.renderTo.focus(), c.preventDefault(), a.modules && a.modules.length && (a.currentModuleIx =
                            a.modules.length - 1, (c = a.modules[a.currentModuleIx]) && c.validate && !c.validate() ? a.prev() : c && c.init(-1)))
                    })
                }, destroy: function () { this.removeExitAnchor(); this.eventProvider.removeAddedEvents(); this.chart.container.removeAttribute("tabindex") }
            }; return l
        }); r(a, "modules/accessibility/components/LegendComponent.js", [a["parts/Globals.js"], a["parts/Legend.js"], a["parts/Utilities.js"], a["modules/accessibility/AccessibilityComponent.js"], a["modules/accessibility/KeyboardNavigationHandler.js"], a["modules/accessibility/utils/htmlUtilities.js"]],
            function (a, g, q, n, p, f) {
                function h(b) { var c = b.legend && b.legend.allItems, d = b.options.legend.accessibility || {}; return !(!c || !c.length || b.colorAxis && b.colorAxis.length || !1 === d.enabled) } var l = q.addEvent, k = q.extend, e = q.find, d = q.fireEvent, b = f.stripHTMLTagsFromString, m = f.removeElement; a.Chart.prototype.highlightLegendItem = function (b) {
                    var c = this.legend.allItems, a = this.highlightedLegendItemIx; if (c[b]) {
                        c[a] && d(c[a].legendGroup.element, "mouseout"); a = this.legend; var e = a.allItems[b].pageIx, m = a.currentPage; "undefined" !==
                            typeof e && e + 1 !== m && a.scroll(1 + e - m); this.setFocusToElement(c[b].legendItem, c[b].a11yProxyElement); d(c[b].legendGroup.element, "mouseover"); return !0
                    } return !1
                }; l(g, "afterColorizeItem", function (b) { var c = b.item; this.chart.options.accessibility.enabled && c && c.a11yProxyElement && c.a11yProxyElement.setAttribute("aria-pressed", b.visible ? "false" : "true") }); a = function () { }; a.prototype = new n; k(a.prototype, {
                    init: function () {
                        var b = this; this.proxyElementsList = []; this.recreateProxies(); this.addEvent(g, "afterScroll", function () {
                            this.chart ===
                                b.chart && (b.updateProxiesPositions(), b.updateLegendItemProxyVisibility(), this.chart.highlightLegendItem(b.highlightedLegendItemIx))
                        }); this.addEvent(g, "afterPositionItem", function (c) { this.chart === b.chart && this.chart.renderer && b.updateProxyPositionForItem(c.item) })
                    }, updateLegendItemProxyVisibility: function () {
                        var b = this.chart.legend, a = b.currentPage || 1, d = b.clipHeight || 0; (b.allItems || []).forEach(function (c) {
                            var e = c.pageIx || 0, m = c._legendItemPos ? c._legendItemPos[1] : 0, t = c.legendItem ? Math.round(c.legendItem.getBBox().height) :
                                0; e = m + t - b.pages[e] > d || e !== a - 1; c.a11yProxyElement && (c.a11yProxyElement.style.visibility = e ? "hidden" : "visible")
                        })
                    }, onChartRender: function () { h(this.chart) ? this.updateProxiesPositions() : this.removeProxies() }, updateProxiesPositions: function () { for (var b = 0, a = this.proxyElementsList; b < a.length; b++) { var d = a[b]; this.updateProxyButtonPosition(d.element, d.posElement) } }, updateProxyPositionForItem: function (b) {
                        var c = e(this.proxyElementsList, function (c) { return c.item === b }); c && this.updateProxyButtonPosition(c.element,
                            c.posElement)
                    }, recreateProxies: function () { this.removeProxies(); h(this.chart) && (this.addLegendProxyGroup(), this.proxyLegendItems(), this.updateLegendItemProxyVisibility()) }, removeProxies: function () { m(this.legendProxyGroup); this.proxyElementsList = [] }, addLegendProxyGroup: function () { var b = this.chart.options.accessibility, a = this.chart.langFormat("accessibility.legend.legendLabel", {}); this.legendProxyGroup = this.addProxyGroup({ "aria-label": a, role: "all" === b.landmarkVerbosity ? "region" : null }) }, proxyLegendItems: function () {
                        var b =
                            this; (this.chart.legend && this.chart.legend.allItems || []).forEach(function (c) { c.legendItem && c.legendItem.element && b.proxyLegendItem(c) })
                    }, proxyLegendItem: function (c) {
                        if (c.legendItem && c.legendGroup) {
                            var a = this.chart.langFormat("accessibility.legend.legendItem", { chart: this.chart, itemName: b(c.name) }), d = c.legendGroup.div ? c.legendItem : c.legendGroup; c.a11yProxyElement = this.createProxyButton(c.legendItem, this.legendProxyGroup, { tabindex: -1, "aria-pressed": !c.visible, "aria-label": a }, d); this.proxyElementsList.push({
                                item: c,
                                element: c.a11yProxyElement, posElement: d
                            })
                        }
                    }, getKeyboardNavigation: function () { var b = this.keyCodes, a = this; return new p(this.chart, { keyCodeMap: [[[b.left, b.right, b.up, b.down], function (b) { return a.onKbdArrowKey(this, b) }], [[b.enter, b.space], function () { return a.onKbdClick(this) }]], validate: function () { return a.shouldHaveLegendNavigation() }, init: function (b) { return a.onKbdNavigationInit(b) } }) }, onKbdArrowKey: function (b, a) {
                        var c = this.keyCodes, d = b.response, e = this.chart, m = e.options.accessibility, f = e.legend.allItems.length;
                        a = a === c.left || a === c.up ? -1 : 1; return e.highlightLegendItem(this.highlightedLegendItemIx + a) ? (this.highlightedLegendItemIx += a, d.success) : 1 < f && m.keyboardNavigation.wrapAround ? (b.init(a), d.success) : d[0 < a ? "next" : "prev"]
                    }, onKbdClick: function (b) { var c = this.chart.legend.allItems[this.highlightedLegendItemIx]; c && c.a11yProxyElement && d(c.a11yProxyElement, "click"); return b.response.success }, shouldHaveLegendNavigation: function () {
                        var b = this.chart, a = b.colorAxis && b.colorAxis.length, d = (b.options.legend || {}).accessibility ||
                            {}; return !!(b.legend && b.legend.allItems && b.legend.display && !a && d.enabled && d.keyboardNavigation && d.keyboardNavigation.enabled)
                    }, onKbdNavigationInit: function (b) { var c = this.chart, a = c.legend.allItems.length - 1; b = 0 < b ? 0 : a; c.highlightLegendItem(b); this.highlightedLegendItemIx = b }
                }); return a
            }); r(a, "modules/accessibility/components/MenuComponent.js", [a["parts/Globals.js"], a["parts/Utilities.js"], a["modules/accessibility/AccessibilityComponent.js"], a["modules/accessibility/KeyboardNavigationHandler.js"], a["modules/accessibility/utils/chartUtilities.js"],
            a["modules/accessibility/utils/htmlUtilities.js"]], function (a, g, q, n, p, f) {
                function h(a) { return a.exportSVGElements && a.exportSVGElements[0] } g = g.extend; var l = p.unhideChartElementFromAT, k = f.removeElement, e = f.getFakeMouseEvent; a.Chart.prototype.showExportMenu = function () { var a = h(this); if (a && (a = a.element, a.onclick)) a.onclick(e("click")) }; a.Chart.prototype.hideExportMenu = function () {
                    var a = this.exportDivElements; a && this.exportContextMenu && (a.forEach(function (b) { if ("highcharts-menu-item" === b.className && b.onmouseout) b.onmouseout(e("mouseout")) }),
                        this.highlightedExportItemIx = 0, this.exportContextMenu.hideMenu(), this.container.focus())
                }; a.Chart.prototype.highlightExportItem = function (a) {
                    var b = this.exportDivElements && this.exportDivElements[a], d = this.exportDivElements && this.exportDivElements[this.highlightedExportItemIx]; if (b && "LI" === b.tagName && (!b.children || !b.children.length)) {
                        var c = !!(this.renderTo.getElementsByTagName("g")[0] || {}).focus; b.focus && c && b.focus(); if (d && d.onmouseout) d.onmouseout(e("mouseout")); if (b.onmouseover) b.onmouseover(e("mouseover"));
                        this.highlightedExportItemIx = a; return !0
                    } return !1
                }; a.Chart.prototype.highlightLastExportItem = function () { var a; if (this.exportDivElements) for (a = this.exportDivElements.length; a--;)if (this.highlightExportItem(a)) return !0; return !1 }; a = function () { }; a.prototype = new q; g(a.prototype, {
                    init: function () { var a = this.chart, b = this; this.addEvent(a, "exportMenuShown", function () { b.onMenuShown() }); this.addEvent(a, "exportMenuHidden", function () { b.onMenuHidden() }) }, onMenuHidden: function () {
                        var a = this.chart.exportContextMenu;
                        a && a.setAttribute("aria-hidden", "true"); this.isExportMenuShown = !1; this.setExportButtonExpandedState("false")
                    }, onMenuShown: function () { var a = this.chart, b = a.exportContextMenu; b && (this.addAccessibleContextMenuAttribs(), l(a, b)); this.isExportMenuShown = !0; this.setExportButtonExpandedState("true") }, setExportButtonExpandedState: function (a) { var b = this.exportButtonProxy; b && b.setAttribute("aria-expanded", a) }, onChartRender: function () {
                        var a = this.chart, b = a.options.accessibility; k(this.exportProxyGroup); var e = a.options.exporting,
                            c = h(a); e && !1 !== e.enabled && e.accessibility && e.accessibility.enabled && c && c.element && (this.exportProxyGroup = this.addProxyGroup("all" === b.landmarkVerbosity ? { "aria-label": a.langFormat("accessibility.exporting.exportRegionLabel", { chart: a }), role: "region" } : {}), b = h(this.chart), this.exportButtonProxy = this.createProxyButton(b, this.exportProxyGroup, { "aria-label": a.langFormat("accessibility.exporting.menuButtonLabel", { chart: a }), "aria-expanded": "false" }))
                    }, addAccessibleContextMenuAttribs: function () {
                        var a = this.chart,
                            b = a.exportDivElements; b && b.length && (b.forEach(function (b) { "LI" !== b.tagName || b.children && b.children.length ? b.setAttribute("aria-hidden", "true") : b.setAttribute("tabindex", -1) }), b = b[0].parentNode, b.removeAttribute("aria-hidden"), b.setAttribute("aria-label", a.langFormat("accessibility.exporting.chartMenuLabel", { chart: a })))
                    }, getKeyboardNavigation: function () {
                        var a = this.keyCodes, b = this.chart, e = this; return new n(b, {
                            keyCodeMap: [[[a.left, a.up], function () { return e.onKbdPrevious(this) }], [[a.right, a.down], function () { return e.onKbdNext(this) }],
                            [[a.enter, a.space], function () { return e.onKbdClick(this) }]], validate: function () { return b.exportChart && !1 !== b.options.exporting.enabled && !1 !== b.options.exporting.accessibility.enabled }, init: function () { var a = e.exportButtonProxy, d = b.exportingGroup; d && a && b.setFocusToElement(d, a) }, terminate: function () { b.hideExportMenu() }
                        })
                    }, onKbdPrevious: function (a) {
                        var b = this.chart, d = b.options.accessibility; a = a.response; for (var c = b.highlightedExportItemIx || 0; c--;)if (b.highlightExportItem(c)) return a.success; return d.keyboardNavigation.wrapAround ?
                            (b.highlightLastExportItem(), a.success) : a.prev
                    }, onKbdNext: function (a) { var b = this.chart, d = b.options.accessibility; a = a.response; for (var c = (b.highlightedExportItemIx || 0) + 1; c < b.exportDivElements.length; ++c)if (b.highlightExportItem(c)) return a.success; return d.keyboardNavigation.wrapAround ? (b.highlightExportItem(0), a.success) : a.next }, onKbdClick: function (a) {
                        var b = this.chart, d = b.exportDivElements[b.highlightedExportItemIx], c = h(b).element; this.isExportMenuShown ? this.fakeClickEvent(d) : (this.fakeClickEvent(c),
                            b.highlightExportItem(0)); return a.response.success
                    }
                }); return a
            }); r(a, "modules/accessibility/components/SeriesComponent/SeriesKeyboardNavigation.js", [a["parts/Chart.js"], a["parts/Globals.js"], a["parts/Point.js"], a["parts/Utilities.js"], a["modules/accessibility/KeyboardNavigationHandler.js"], a["modules/accessibility/utils/EventProvider.js"], a["modules/accessibility/utils/chartUtilities.js"]], function (a, g, q, n, p, f, h) {
                function l(b) {
                    var a = b.index, c = b.series.points, d = c.length; if (c[a] !== b) for (; d--;) {
                        if (c[d] ===
                            b) return d
                    } else return a
                } function k(b) { var a = b.chart.options.accessibility.keyboardNavigation.seriesNavigation, c = b.options.accessibility || {}, d = c.keyboardNavigation; return d && !1 === d.enabled || !1 === c.enabled || !1 === b.options.enableMouseTracking || !b.visible || a.pointNavigationEnabledThreshold && a.pointNavigationEnabledThreshold <= b.points.length } function e(b) { var a = b.series.chart.options.accessibility; return b.isNull && a.keyboardNavigation.seriesNavigation.skipNullPoints || !1 === b.visible || k(b.series) } function d(b,
                    a, d, e) { var m = Infinity, u = a.points.length, f = function (b) { return !(c(b.plotX) && c(b.plotY)) }; if (!f(b)) { for (; u--;) { var k = a.points[u]; if (!f(k) && (k = (b.plotX - k.plotX) * (b.plotX - k.plotX) * (d || 1) + (b.plotY - k.plotY) * (b.plotY - k.plotY) * (e || 1), k < m)) { m = k; var t = u } } return c(t) ? a.points[t] : void 0 } } function b(b) { var a = !1; delete b.highlightedPoint; return a = b.series.reduce(function (b, a) { return b || a.highlightFirstValidPoint() }, !1) } function m(b, a) { this.keyCodes = a; this.chart = b } var c = n.defined; n = n.extend; var t = h.getPointFromXY,
                        A = h.getSeriesFromName, z = h.scrollToPoint; g.Series.prototype.keyboardMoveVertical = !0;["column", "pie"].forEach(function (b) { g.seriesTypes[b] && (g.seriesTypes[b].prototype.keyboardMoveVertical = !1) }); q.prototype.highlight = function () { var b = this.series.chart; if (this.isNull) b.tooltip && b.tooltip.hide(0); else this.onMouseOver(); z(this); this.graphic && b.setFocusToElement(this.graphic); b.highlightedPoint = this; return this }; a.prototype.highlightAdjacentPoint = function (b) {
                            var a = this.series, c = this.highlightedPoint, d =
                                c && l(c) || 0, m = c && c.series.points, u = this.series && this.series[this.series.length - 1]; u = u && u.points && u.points[u.points.length - 1]; if (!a[0] || !a[0].points) return !1; if (c) { if (a = a[c.series.index + (b ? 1 : -1)], d = m[d + (b ? 1 : -1)], !d && a && (d = a.points[b ? 0 : a.points.length - 1]), !d) return !1 } else d = b ? a[0].points[0] : u; return e(d) ? (a = d.series, k(a) ? this.highlightedPoint = b ? a.points[a.points.length - 1] : a.points[0] : this.highlightedPoint = d, this.highlightAdjacentPoint(b)) : d.highlight()
                        }; g.Series.prototype.highlightFirstValidPoint = function () {
                            var b =
                                this.chart.highlightedPoint, a = (b && b.series) === this ? l(b) : 0; b = this.points; var c = b.length; if (b && c) { for (var d = a; d < c; ++d)if (!e(b[d])) return b[d].highlight(); for (; 0 <= a; --a)if (!e(b[a])) return b[a].highlight() } return !1
                        }; a.prototype.highlightAdjacentSeries = function (b) {
                            var a, c = this.highlightedPoint; var e = (a = this.series && this.series[this.series.length - 1]) && a.points && a.points[a.points.length - 1]; if (!this.highlightedPoint) return a = b ? this.series && this.series[0] : a, (e = b ? a && a.points && a.points[0] : e) ? e.highlight() : !1;
                            a = this.series[c.series.index + (b ? -1 : 1)]; if (!a) return !1; e = d(c, a, 4); if (!e) return !1; if (k(a)) return e.highlight(), b = this.highlightAdjacentSeries(b), b ? b : (c.highlight(), !1); e.highlight(); return e.series.highlightFirstValidPoint()
                        }; a.prototype.highlightAdjacentPointVertical = function (b) {
                            var a = this.highlightedPoint, d = Infinity, m; if (!c(a.plotX) || !c(a.plotY)) return !1; this.series.forEach(function (f) {
                                k(f) || f.points.forEach(function (k) {
                                    if (c(k.plotY) && c(k.plotX) && k !== a) {
                                        var u = k.plotY - a.plotY, t = Math.abs(k.plotX - a.plotX);
                                        t = Math.abs(u) * Math.abs(u) + t * t * 4; f.yAxis && f.yAxis.reversed && (u *= -1); !(0 >= u && b || 0 <= u && !b || 5 > t || e(k)) && t < d && (d = t, m = k)
                                    }
                                })
                            }); return m ? m.highlight() : !1
                        }; n(m.prototype, {
                            init: function () {
                                var a = this, c = this.chart, d = this.eventProvider = new f; d.addEvent(g.Series, "destroy", function () { return a.onSeriesDestroy(this) }); d.addEvent(c, "afterDrilldown", function () { b(this); this.focusElement && this.focusElement.removeFocusBorder() }); d.addEvent(c, "drilldown", function (b) {
                                    b = b.point; var c = b.series; a.lastDrilledDownPoint = {
                                        x: b.x,
                                        y: b.y, seriesName: c ? c.name : ""
                                    }
                                }); d.addEvent(c, "drillupall", function () { setTimeout(function () { a.onDrillupAll() }, 10) })
                            }, onDrillupAll: function () { var b = this.lastDrilledDownPoint, a = this.chart, d = b && A(a, b.seriesName), e; b && d && c(b.x) && c(b.y) && (e = t(d, b.x, b.y)); a.container && a.container.focus(); e && e.highlight && e.highlight(); a.focusElement && a.focusElement.removeFocusBorder() }, getKeyboardNavigationHandler: function () {
                                var b = this, a = this.keyCodes, c = this.chart, d = c.inverted; return new p(c, {
                                    keyCodeMap: [[d ? [a.up, a.down] :
                                        [a.left, a.right], function (a) { return b.onKbdSideways(this, a) }], [d ? [a.left, a.right] : [a.up, a.down], function (a) { return b.onKbdVertical(this, a) }], [[a.enter, a.space], function () { c.highlightedPoint && c.highlightedPoint.firePointEvent("click"); return this.response.success }]], init: function (a) { return b.onHandlerInit(this, a) }, terminate: function () { return b.onHandlerTerminate() }
                                })
                            }, onKbdSideways: function (b, a) { var c = this.keyCodes; return this.attemptHighlightAdjacentPoint(b, a === c.right || a === c.down) }, onKbdVertical: function (b,
                                a) { var c = this.chart, d = this.keyCodes; a = a === d.down || a === d.right; d = c.options.accessibility.keyboardNavigation.seriesNavigation; if (d.mode && "serialize" === d.mode) return this.attemptHighlightAdjacentPoint(b, a); c[c.highlightedPoint && c.highlightedPoint.series.keyboardMoveVertical ? "highlightAdjacentPointVertical" : "highlightAdjacentSeries"](a); return b.response.success }, onHandlerInit: function (a, c) {
                                    var d = this.chart; if (0 < c) b(d); else {
                                        c = d.series.length; for (var e; c-- && !(d.highlightedPoint = d.series[c].points[d.series[c].points.length -
                                            1], e = d.series[c].highlightFirstValidPoint()););
                                    } return a.response.success
                                }, onHandlerTerminate: function () { var b, a, c = this.chart, d = c.highlightedPoint; null === (b = c.tooltip) || void 0 === b ? void 0 : b.hide(0); null === (a = null === d || void 0 === d ? void 0 : d.onMouseOut) || void 0 === a ? void 0 : a.call(d); delete c.highlightedPoint }, attemptHighlightAdjacentPoint: function (b, a) {
                                    var c = this.chart, d = c.options.accessibility.keyboardNavigation.wrapAround; return c.highlightAdjacentPoint(a) ? b.response.success : d ? b.init(a ? 1 : -1) : b.response[a ?
                                        "next" : "prev"]
                                }, onSeriesDestroy: function (b) { var a = this.chart; a.highlightedPoint && a.highlightedPoint.series === b && (delete a.highlightedPoint, a.focusElement && a.focusElement.removeFocusBorder()) }, destroy: function () { this.eventProvider.removeAddedEvents() }
                        }); return m
            }); r(a, "modules/accessibility/components/AnnotationsA11y.js", [a["parts/Utilities.js"], a["modules/accessibility/utils/htmlUtilities.js"]], function (a, g) {
                function l(a) {
                    return (a.annotations || []).reduce(function (a, b) {
                        var d; !1 !== (null === (d = b.options) ||
                            void 0 === d ? void 0 : d.visible) && (a = a.concat(b.labels)); return a
                    }, [])
                } function n(a) { var d, b, e, c, k = null === (b = null === (d = a.options) || void 0 === d ? void 0 : d.accessibility) || void 0 === b ? void 0 : b.description; return k ? k : (null === (c = null === (e = a.graphic) || void 0 === e ? void 0 : e.text) || void 0 === c ? void 0 : c.textStr) || "" } function p(a) {
                    var d, b, e = null === (b = null === (d = a.options) || void 0 === d ? void 0 : d.accessibility) || void 0 === b ? void 0 : b.description; if (e) return e; d = a.chart; b = n(a); e = a.points.filter(function (b) { return !!b.graphic }).map(function (b) {
                        var a,
                            c; if (!(c = null === (a = null === b || void 0 === b ? void 0 : b.accessibility) || void 0 === a ? void 0 : a.valueDescription)) { var d, e; c = (null === (e = null === (d = null === b || void 0 === b ? void 0 : b.graphic) || void 0 === d ? void 0 : d.element) || void 0 === e ? void 0 : e.getAttribute("aria-label")) || "" } b = (null === b || void 0 === b ? void 0 : b.series.name) || ""; return (b ? b + ", " : "") + "data point " + c
                    }).filter(function (b) { return !!b }); var c = e.length; a = "accessibility.screenReaderSection.annotations.description" + (1 < c ? "MultiplePoints" : c ? "SinglePoint" : "NoPoints");
                    b = { annotationText: b, numPoints: c, annotationPoint: e[0], additionalAnnotationPoints: e.slice(1) }; return d.langFormat(a, b)
                } function f(a) { return l(a).map(function (a) { return (a = x(k(p(a)))) ? "<li>" + a + "</li>" : "" }) } var h = a.inArray, x = g.escapeStringForHTML, k = g.stripHTMLTagsFromString; return {
                    getAnnotationsInfoHTML: function (a) { var d = a.annotations; return d && d.length ? "<ul>" + f(a).join(" ") + "</ul>" : "" }, getAnnotationLabelDescription: p, getAnnotationListItems: f, getPointAnnotationTexts: function (a) {
                        var d = l(a.series.chart).filter(function (b) {
                            return -1 <
                                h(a, b.points)
                        }); return d.length ? d.map(function (b) { return "" + n(b) }) : []
                    }
                }
            }); r(a, "modules/accessibility/components/SeriesComponent/SeriesDescriber.js", [a["parts/Utilities.js"], a["modules/accessibility/components/AnnotationsA11y.js"], a["modules/accessibility/utils/htmlUtilities.js"], a["modules/accessibility/utils/chartUtilities.js"], a["parts/Tooltip.js"]], function (a, g, q, n, p) {
                function f(b) {
                    var a = b.index; return b.series && b.series.data && D(a) ? C(b.series.data, function (b) {
                        return !!(b && "undefined" !== typeof b.index &&
                            b.index > a && b.graphic && b.graphic.element)
                    }) || null : null
                } function h(b) { var a = b.chart.options.accessibility.series.pointDescriptionEnabledThreshold; return !!(!1 !== a && b.points && b.points.length >= a) } function l(b) { var a = b.options.accessibility || {}; return !h(b) && !a.exposeAsGroupOnly } function k(b) { var a = b.chart.options.accessibility.keyboardNavigation.seriesNavigation; return !(!b.points || !(b.points.length < a.pointNavigationEnabledThreshold || !1 === a.pointNavigationEnabledThreshold)) } function e(b, a) {
                    var c = b.series.chart,
                        d = c.options.accessibility.point || {}; b = b.series.tooltipOptions || {}; c = c.options.lang; return u(a) ? I(a, d.valueDecimals || b.valueDecimals || -1, c.decimalPoint, c.accessibility.thousandsSep || c.thousandsSep) : a
                } function d(b) { var a = (b.options.accessibility || {}).description; return a && b.chart.langFormat("accessibility.series.description", { description: a, series: b }) || "" } function b(b, a) { return b.chart.langFormat("accessibility.series." + a + "Description", { name: J(b[a]), series: b }) } function m(b) {
                    var a = b.series, c = a.chart,
                        d = c.options.accessibility.point || {}; if (a.xAxis && a.xAxis.dateTime) return a = p.prototype.getXDateFormat.call({ getDateFormat: p.prototype.getDateFormat, chart: c }, b, c.options.tooltip, a.xAxis), d = d.dateFormatter && d.dateFormatter(b) || d.dateFormat || a, c.time.dateFormat(d, b.x, void 0)
                } function c(b) { var a = m(b), c = (b.series.xAxis || {}).categories && D(b.category) && ("" + b.category).replace("<br/>", " "), d = b.id && 0 > b.id.indexOf("highcharts-"), e = "x, " + b.x; return b.name || a || c || (d ? b.id : e) } function t(b, a, c) {
                    var d = a || "", m = c ||
                        ""; return b.series.pointArrayMap.reduce(function (a, c) { a += a.length ? ", " : ""; var k = e(b, y(b[c], b.options[c])); return a + (c + ": " + d + k + m) }, "")
                } function A(b) { var a = b.series, c = a.chart.options.accessibility.point || {}, d = a.tooltipOptions || {}, m = c.valuePrefix || d.valuePrefix || ""; c = c.valueSuffix || d.valueSuffix || ""; d = e(b, b["undefined" !== typeof b.value ? "value" : "y"]); return b.isNull ? a.chart.langFormat("accessibility.series.nullPointValue", { point: b }) : a.pointArrayMap ? t(b, m, c) : m + d + c } function z(b) {
                    var a = b.series, d = a.chart,
                        e = d.options.accessibility.point.valueDescriptionFormat, m = (a = y(a.xAxis && a.xAxis.options.accessibility && a.xAxis.options.accessibility.enabled, !d.angular)) ? c(b) : ""; b = { point: b, index: D(b.index) ? b.index + 1 : "", xDescription: m, value: A(b), separator: a ? ", " : "" }; return v(e, b, d)
                } function w(b) {
                    var a = b.series, c = a.chart, d = z(b), e = b.options && b.options.accessibility && b.options.accessibility.description; e = e ? " " + e : ""; a = 1 < c.series.length && a.name ? " " + a.name + "." : ""; c = b.series.chart; var m = H(b), k = { point: b, annotations: m }; c = m.length ?
                        c.langFormat("accessibility.series.pointAnnotationsDescription", k) : ""; b.accessibility = b.accessibility || {}; b.accessibility.valueDescription = d; return d + e + a + (c ? " " + c : "")
                } function r(b) {
                    var a = l(b), c = k(b); (a || c) && b.points.forEach(function (b) {
                        var c; if (!(c = b.graphic && b.graphic.element) && (c = b.series && b.series.is("sunburst"), c = b.isNull && !c)) {
                            var d = b.series, e = f(b); d = (c = e && e.graphic) ? c.parentGroup : d.graph || d.group; e = e ? { x: y(b.plotX, e.plotX, 0), y: y(b.plotY, e.plotY, 0) } : { x: y(b.plotX, 0), y: y(b.plotY, 0) }; e = b.series.chart.renderer.rect(e.x,
                                e.y, 1, 1); e.attr({ "class": "highcharts-a11y-dummy-point", fill: "none", opacity: 0, "fill-opacity": 0, "stroke-opacity": 0 }); d && d.element ? (b.graphic = e, b.hasDummyGraphic = !0, e.add(d), d.element.insertBefore(e.element, c ? c.element : null), c = e.element) : c = void 0
                        } c && (c.setAttribute("tabindex", "-1"), c.style.outline = "0", a ? (e = b.series, d = e.chart.options.accessibility.point || {}, e = e.options.accessibility || {}, b = F(G(e.pointDescriptionFormatter && e.pointDescriptionFormatter(b) || d.descriptionFormatter && d.descriptionFormatter(b) ||
                            w(b))), c.setAttribute("role", "img"), c.setAttribute("aria-label", b)) : c.setAttribute("aria-hidden", !0))
                    })
                } function B(a) {
                    var c = a.chart, e = c.types || [], m = d(a), k = function (b) { return c[b] && 1 < c[b].length && a[b] }, f = b(a, "xAxis"), u = b(a, "yAxis"), t = { name: a.name || "", ix: a.index + 1, numSeries: c.series && c.series.length, numPoints: a.points && a.points.length, series: a }; e = 1 < e.length ? "Combination" : ""; return (c.langFormat("accessibility.series.summary." + a.type + e, t) || c.langFormat("accessibility.series.summary.default" + e, t)) + (m ?
                        " " + m : "") + (k("yAxis") ? " " + u : "") + (k("xAxis") ? " " + f : "")
                } var C = a.find, v = a.format, u = a.isNumber, I = a.numberFormat, y = a.pick, D = a.defined, H = g.getPointAnnotationTexts, F = q.escapeStringForHTML, K = q.reverseChildNodes, G = q.stripHTMLTagsFromString, J = n.getAxisDescription, L = n.getSeriesFirstPointElement, M = n.getSeriesA11yElement, N = n.unhideChartElementFromAT; return {
                    describeSeries: function (b) {
                        var a = b.chart, c = L(b), d = M(b), e = a.is3d && a.is3d(); if (d) {
                            d.lastChild !== c || e || K(d); r(b); N(a, d); e = b.chart; a = e.options.chart || {}; c = 1 < e.series.length;
                            e = e.options.accessibility.series.describeSingleSeries; var m = (b.options.accessibility || {}).exposeAsGroupOnly; a.options3d && a.options3d.enabled && c || !(c || e || m || h(b)) ? d.setAttribute("aria-label", "") : (a = b.chart.options.accessibility, c = a.landmarkVerbosity, (b.options.accessibility || {}).exposeAsGroupOnly ? d.setAttribute("role", "img") : "all" === c && d.setAttribute("role", "region"), d.setAttribute("tabindex", "-1"), d.style.outline = "0", d.setAttribute("aria-label", F(G(a.series.descriptionFormatter && a.series.descriptionFormatter(b) ||
                                B(b)))))
                        }
                    }, defaultPointDescriptionFormatter: w, defaultSeriesDescriptionFormatter: B, getPointA11yTimeDescription: m, getPointXDescription: c, getPointValue: A, getPointValueDescription: z
                }
            }); r(a, "modules/accessibility/utils/Announcer.js", [a["parts/Globals.js"], a["modules/accessibility/utils/DOMElementProvider.js"], a["modules/accessibility/utils/htmlUtilities.js"]], function (a, g, q) {
                var l = q.visuallyHideElement; q = function () {
                    function a(a, h) { this.chart = a; this.domElementProvider = new g; this.announceRegion = this.addAnnounceRegion(h) }
                    a.prototype.destroy = function () { this.domElementProvider.destroyCreatedElements() }; a.prototype.announce = function (a) { var f = this; this.announceRegion.innerHTML = a; this.clearAnnouncementRegionTimer && clearTimeout(this.clearAnnouncementRegionTimer); this.clearAnnouncementRegionTimer = setTimeout(function () { f.announceRegion.innerHTML = ""; delete f.clearAnnouncementRegionTimer }, 1E3) }; a.prototype.addAnnounceRegion = function (a) {
                        var f = this.chart.renderTo, g = this.domElementProvider.createElement("div"); g.setAttribute("aria-hidden",
                            !1); g.setAttribute("aria-live", a); l(g); f.insertBefore(g, f.firstChild); return g
                    }; return a
                }(); return a.Announcer = q
            }); r(a, "modules/accessibility/components/SeriesComponent/NewDataAnnouncer.js", [a["parts/Globals.js"], a["parts/Utilities.js"], a["modules/accessibility/utils/chartUtilities.js"], a["modules/accessibility/components/SeriesComponent/SeriesDescriber.js"], a["modules/accessibility/utils/Announcer.js"], a["modules/accessibility/utils/EventProvider.js"]], function (a, g, q, n, p, f) {
                function h(b) {
                    var a = b.series.data.filter(function (a) {
                        return b.x ===
                            a.x && b.y === a.y
                    }); return 1 === a.length ? a[0] : b
                } function l(b, a) { var c = (b || []).concat(a || []).reduce(function (b, a) { b[a.name + a.index] = a; return b }, {}); return Object.keys(c).map(function (b) { return c[b] }) } var k = g.extend, e = g.defined, d = q.getChartTitle, b = n.defaultPointDescriptionFormatter, m = n.defaultSeriesDescriptionFormatter; g = function (b) { this.chart = b }; k(g.prototype, {
                    init: function () {
                        var b = this.chart, a = b.options.accessibility.announceNewData.interruptUser ? "assertive" : "polite"; this.lastAnnouncementTime = 0; this.dirty =
                            { allSeries: {} }; this.eventProvider = new f; this.announcer = new p(b, a); this.addEventListeners()
                    }, destroy: function () { this.eventProvider.removeAddedEvents(); this.announcer.destroy() }, addEventListeners: function () {
                        var b = this, d = this.chart, e = this.eventProvider; e.addEvent(d, "afterDrilldown", function () { b.lastAnnouncementTime = 0 }); e.addEvent(a.Series, "updatedData", function () { b.onSeriesUpdatedData(this) }); e.addEvent(d, "afterAddSeries", function (a) { b.onSeriesAdded(a.series) }); e.addEvent(a.Series, "addPoint", function (a) { b.onPointAdded(a.point) });
                        e.addEvent(d, "redraw", function () { b.announceDirtyData() })
                    }, onSeriesUpdatedData: function (b) { var a = this.chart; b.chart === a && a.options.accessibility.announceNewData.enabled && (this.dirty.hasDirty = !0, this.dirty.allSeries[b.name + b.index] = b) }, onSeriesAdded: function (b) { this.chart.options.accessibility.announceNewData.enabled && (this.dirty.hasDirty = !0, this.dirty.allSeries[b.name + b.index] = b, this.dirty.newSeries = e(this.dirty.newSeries) ? void 0 : b) }, onPointAdded: function (b) {
                        var a = b.series.chart; this.chart === a && a.options.accessibility.announceNewData.enabled &&
                            (this.dirty.newPoint = e(this.dirty.newPoint) ? void 0 : b)
                    }, announceDirtyData: function () { var b = this; if (this.chart.options.accessibility.announceNewData && this.dirty.hasDirty) { var a = this.dirty.newPoint; a && (a = h(a)); this.queueAnnouncement(Object.keys(this.dirty.allSeries).map(function (a) { return b.dirty.allSeries[a] }), this.dirty.newSeries, a); this.dirty = { allSeries: {} } } }, queueAnnouncement: function (b, a, d) {
                        var c = this, e = this.chart.options.accessibility.announceNewData; if (e.enabled) {
                            var m = +new Date; e = Math.max(0,
                                e.minAnnounceInterval - (m - this.lastAnnouncementTime)); b = l(this.queuedAnnouncement && this.queuedAnnouncement.series, b); if (a = this.buildAnnouncementMessage(b, a, d)) this.queuedAnnouncement && clearTimeout(this.queuedAnnouncementTimer), this.queuedAnnouncement = { time: m, message: a, series: b }, this.queuedAnnouncementTimer = setTimeout(function () { c && c.announcer && (c.lastAnnouncementTime = +new Date, c.announcer.announce(c.queuedAnnouncement.message), delete c.queuedAnnouncement, delete c.queuedAnnouncementTimer) }, e)
                        }
                    }, buildAnnouncementMessage: function (c,
                        e, k) { var f = this.chart, h = f.options.accessibility.announceNewData; if (h.announcementFormatter && (c = h.announcementFormatter(c, e, k), !1 !== c)) return c.length ? c : null; c = a.charts && 1 < a.charts.length ? "Multiple" : "Single"; c = e ? "newSeriesAnnounce" + c : k ? "newPointAnnounce" + c : "newDataAnnounce"; h = d(f); return f.langFormat("accessibility.announceNewData." + c, { chartTitle: h, seriesDesc: e ? m(e) : null, pointDesc: k ? b(k) : null, point: k, series: e }) }
                }); return g
            }); r(a, "modules/accessibility/components/SeriesComponent/forcedMarkers.js", [a["parts/Globals.js"],
            a["parts/Utilities.js"]], function (a, g) {
                function l(a) { p(!0, a, { marker: { enabled: !0, states: { normal: { opacity: 0 } } } }) } var n = g.addEvent, p = g.merge; return function () {
                    n(a.Series, "render", function () {
                        var a = this.options, h = !1 !== (this.options.accessibility && this.options.accessibility.enabled); if (h = this.chart.options.accessibility.enabled && h) h = this.chart.options.accessibility, h = this.points.length < h.series.pointDescriptionEnabledThreshold || !1 === h.series.pointDescriptionEnabledThreshold; if (h) {
                            if (a.marker && !1 === a.marker.enabled &&
                                (this.a11yMarkersForced = !0, l(this.options)), this._hasPointMarkers && this.points && this.points.length) for (a = this.points.length; a--;) { h = this.points[a]; var g = h.options; delete h.hasForcedA11yMarker; g.marker && (g.marker.enabled ? (p(!0, g.marker, { states: { normal: { opacity: g.marker.states && g.marker.states.normal && g.marker.states.normal.opacity || 1 } } }), h.hasForcedA11yMarker = !1) : (l(g), h.hasForcedA11yMarker = !0)) }
                        } else this.a11yMarkersForced && (delete this.a11yMarkersForced, (a = this.resetA11yMarkerOptions) && p(!0, this.options,
                            { marker: { enabled: a.enabled, states: { normal: { opacity: a.states && a.states.normal && a.states.normal.opacity } } } }))
                    }); n(a.Series, "afterSetOptions", function (a) { this.resetA11yMarkerOptions = p(a.options.marker || {}, this.userOptions.marker || {}) }); n(a.Series, "afterRender", function () {
                        if (this.chart.styledMode) {
                            if (this.markerGroup) this.markerGroup[this.a11yMarkersForced ? "addClass" : "removeClass"]("highcharts-a11y-markers-hidden"); this._hasPointMarkers && this.points && this.points.length && this.points.forEach(function (a) {
                                a.graphic &&
                                    (a.graphic[a.hasForcedA11yMarker ? "addClass" : "removeClass"]("highcharts-a11y-marker-hidden"), a.graphic[!1 === a.hasForcedA11yMarker ? "addClass" : "removeClass"]("highcharts-a11y-marker-visible"))
                            })
                        }
                    })
                }
            }); r(a, "modules/accessibility/components/SeriesComponent/SeriesComponent.js", [a["parts/Globals.js"], a["parts/Utilities.js"], a["modules/accessibility/AccessibilityComponent.js"], a["modules/accessibility/components/SeriesComponent/SeriesKeyboardNavigation.js"], a["modules/accessibility/components/SeriesComponent/NewDataAnnouncer.js"],
            a["modules/accessibility/components/SeriesComponent/forcedMarkers.js"], a["modules/accessibility/utils/chartUtilities.js"], a["modules/accessibility/components/SeriesComponent/SeriesDescriber.js"], a["parts/Tooltip.js"]], function (a, g, q, n, p, f, h, x, k) {
                g = g.extend; var e = h.hideSeriesFromAT, d = x.describeSeries; a.SeriesAccessibilityDescriber = x; f(); a = function () { }; a.prototype = new q; g(a.prototype, {
                    init: function () {
                        this.newDataAnnouncer = new p(this.chart); this.newDataAnnouncer.init(); this.keyboardNavigation = new n(this.chart,
                            this.keyCodes); this.keyboardNavigation.init(); this.hideTooltipFromATWhenShown(); this.hideSeriesLabelsFromATWhenShown()
                    }, hideTooltipFromATWhenShown: function () { var b = this; this.addEvent(k, "refresh", function () { this.chart === b.chart && this.label && this.label.element && this.label.element.setAttribute("aria-hidden", !0) }) }, hideSeriesLabelsFromATWhenShown: function () { this.addEvent(this.chart, "afterDrawSeriesLabels", function () { this.series.forEach(function (b) { b.labelBySeries && b.labelBySeries.attr("aria-hidden", !0) }) }) },
                    onChartRender: function () { this.chart.series.forEach(function (b) { !1 !== (b.options.accessibility && b.options.accessibility.enabled) && b.visible ? d(b) : e(b) }) }, getKeyboardNavigation: function () { return this.keyboardNavigation.getKeyboardNavigationHandler() }, destroy: function () { this.newDataAnnouncer.destroy(); this.keyboardNavigation.destroy() }
                }); return a
            }); r(a, "modules/accessibility/components/ZoomComponent.js", [a["parts/Globals.js"], a["parts/Utilities.js"], a["modules/accessibility/AccessibilityComponent.js"],
            a["modules/accessibility/KeyboardNavigationHandler.js"], a["modules/accessibility/utils/chartUtilities.js"], a["modules/accessibility/utils/htmlUtilities.js"]], function (a, g, q, n, p, f) {
                var h = g.extend, l = g.pick, k = p.unhideChartElementFromAT, e = f.setElAttrs, d = f.removeElement; a.Axis.prototype.panStep = function (b, a) { var c = a || 3; a = this.getExtremes(); var d = (a.max - a.min) / c * b; c = a.max + d; d = a.min + d; var e = c - d; 0 > b && d < a.dataMin ? (d = a.dataMin, c = d + e) : 0 < b && c > a.dataMax && (c = a.dataMax, d = c - e); this.setExtremes(d, c) }; a = function () { };
                a.prototype = new q; h(a.prototype, {
                    init: function () { var b = this, a = this.chart;["afterShowResetZoom", "afterDrilldown", "drillupall"].forEach(function (c) { b.addEvent(a, c, function () { b.updateProxyOverlays() }) }) }, onChartUpdate: function () { var b = this.chart, a = this; b.mapNavButtons && b.mapNavButtons.forEach(function (c, d) { k(b, c.element); a.setMapNavButtonAttrs(c.element, "accessibility.zoom.mapZoom" + (d ? "Out" : "In")) }) }, setMapNavButtonAttrs: function (b, a) {
                        var c = this.chart; a = c.langFormat(a, { chart: c }); e(b, {
                            tabindex: -1, role: "button",
                            "aria-label": a
                        })
                    }, onChartRender: function () { this.updateProxyOverlays() }, updateProxyOverlays: function () {
                        var b = this.chart; d(this.drillUpProxyGroup); d(this.resetZoomProxyGroup); b.resetZoomButton && this.recreateProxyButtonAndGroup(b.resetZoomButton, "resetZoomProxyButton", "resetZoomProxyGroup", b.langFormat("accessibility.zoom.resetZoomButton", { chart: b })); b.drillUpButton && this.recreateProxyButtonAndGroup(b.drillUpButton, "drillUpProxyButton", "drillUpProxyGroup", b.langFormat("accessibility.drillUpButton", {
                            chart: b,
                            buttonText: b.getDrilldownBackText()
                        }))
                    }, recreateProxyButtonAndGroup: function (b, a, c, e) { d(this[c]); this[c] = this.addProxyGroup(); this[a] = this.createProxyButton(b, this[c], { "aria-label": e, tabindex: -1 }) }, getMapZoomNavigation: function () {
                        var b = this.keyCodes, a = this.chart, c = this; return new n(a, {
                            keyCodeMap: [[[b.up, b.down, b.left, b.right], function (b) { return c.onMapKbdArrow(this, b) }], [[b.tab], function (b, a) { return c.onMapKbdTab(this, a) }], [[b.space, b.enter], function () { return c.onMapKbdClick(this) }]], validate: function () {
                                return !!(a.mapZoom &&
                                    a.mapNavButtons && a.mapNavButtons.length)
                            }, init: function (b) { return c.onMapNavInit(b) }
                        })
                    }, onMapKbdArrow: function (b, a) { var c = this.keyCodes; this.chart[a === c.up || a === c.down ? "yAxis" : "xAxis"][0].panStep(a === c.left || a === c.up ? -1 : 1); return b.response.success }, onMapKbdTab: function (b, a) {
                        var c = this.chart; b = b.response; var d = (a = a.shiftKey) && !this.focusedMapNavButtonIx || !a && this.focusedMapNavButtonIx; c.mapNavButtons[this.focusedMapNavButtonIx].setState(0); if (d) return c.mapZoom(), b[a ? "prev" : "next"]; this.focusedMapNavButtonIx +=
                            a ? -1 : 1; a = c.mapNavButtons[this.focusedMapNavButtonIx]; c.setFocusToElement(a.box, a.element); a.setState(2); return b.success
                    }, onMapKbdClick: function (b) { this.fakeClickEvent(this.chart.mapNavButtons[this.focusedMapNavButtonIx].element); return b.response.success }, onMapNavInit: function (b) { var a = this.chart, c = a.mapNavButtons[0], d = a.mapNavButtons[1]; c = 0 < b ? c : d; a.setFocusToElement(c.box, c.element); c.setState(2); this.focusedMapNavButtonIx = 0 < b ? 0 : 1 }, simpleButtonNavigation: function (b, a, c) {
                        var d = this.keyCodes, e = this,
                            k = this.chart; return new n(k, { keyCodeMap: [[[d.tab, d.up, d.down, d.left, d.right], function (b, a) { return this.response[b === d.tab && a.shiftKey || b === d.left || b === d.up ? "prev" : "next"] }], [[d.space, d.enter], function () { var b = c(this, k); return l(b, this.response.success) }]], validate: function () { return k[b] && k[b].box && e[a] }, init: function () { k.setFocusToElement(k[b].box, e[a]) } })
                    }, getKeyboardNavigation: function () {
                        return [this.simpleButtonNavigation("resetZoomButton", "resetZoomProxyButton", function (b, a) { a.zoomOut() }), this.simpleButtonNavigation("drillUpButton",
                            "drillUpProxyButton", function (b, a) { a.drillUp(); return b.response.prev }), this.getMapZoomNavigation()]
                    }
                }); return a
            }); r(a, "modules/accessibility/components/RangeSelectorComponent.js", [a["parts/Globals.js"], a["parts/Utilities.js"], a["modules/accessibility/AccessibilityComponent.js"], a["modules/accessibility/KeyboardNavigationHandler.js"], a["modules/accessibility/utils/chartUtilities.js"], a["modules/accessibility/utils/htmlUtilities.js"]], function (a, g, q, n, p, f) {
                g = g.extend; var h = p.unhideChartElementFromAT,
                    l = f.setElAttrs; a.Chart.prototype.highlightRangeSelectorButton = function (a) { var e = this.rangeSelector.buttons, d = this.highlightedRangeSelectorItemIx; "undefined" !== typeof d && e[d] && e[d].setState(this.oldRangeSelectorItemState || 0); this.highlightedRangeSelectorItemIx = a; return e[a] ? (this.setFocusToElement(e[a].box, e[a].element), this.oldRangeSelectorItemState = e[a].state, e[a].setState(2), !0) : !1 }; a = function () { }; a.prototype = new q; g(a.prototype, {
                        onChartUpdate: function () {
                            var a = this.chart, e = this, d = a.rangeSelector;
                            d && (d.buttons && d.buttons.length && d.buttons.forEach(function (b) { h(a, b.element); e.setRangeButtonAttrs(b) }), d.maxInput && d.minInput && ["minInput", "maxInput"].forEach(function (b, k) { if (b = d[b]) h(a, b), e.setRangeInputAttrs(b, "accessibility.rangeSelector." + (k ? "max" : "min") + "InputLabel") }))
                        }, setRangeButtonAttrs: function (a) { var e = this.chart; e = e.langFormat("accessibility.rangeSelector.buttonText", { chart: e, buttonText: a.text && a.text.textStr }); l(a.element, { tabindex: -1, role: "button", "aria-label": e }) }, setRangeInputAttrs: function (a,
                            e) { var d = this.chart; l(a, { tabindex: -1, role: "textbox", "aria-label": d.langFormat(e, { chart: d }) }) }, getRangeSelectorButtonNavigation: function () {
                                var a = this.chart, e = this.keyCodes, d = this; return new n(a, {
                                    keyCodeMap: [[[e.left, e.right, e.up, e.down], function (a) { return d.onButtonNavKbdArrowKey(this, a) }], [[e.enter, e.space], function () { return d.onButtonNavKbdClick(this) }]], validate: function () { return a.rangeSelector && a.rangeSelector.buttons && a.rangeSelector.buttons.length }, init: function (b) {
                                        var d = a.rangeSelector.buttons.length -
                                            1; a.highlightRangeSelectorButton(0 < b ? 0 : d)
                                    }
                                })
                            }, onButtonNavKbdArrowKey: function (a, e) { var d = a.response, b = this.keyCodes, k = this.chart, c = k.options.accessibility.keyboardNavigation.wrapAround; e = e === b.left || e === b.up ? -1 : 1; return k.highlightRangeSelectorButton(k.highlightedRangeSelectorItemIx + e) ? d.success : c ? (a.init(e), d.success) : d[0 < e ? "next" : "prev"] }, onButtonNavKbdClick: function (a) {
                                a = a.response; var e = this.chart; 3 !== e.oldRangeSelectorItemState && this.fakeClickEvent(e.rangeSelector.buttons[e.highlightedRangeSelectorItemIx].element);
                                return a.success
                            }, getRangeSelectorInputNavigation: function () {
                                var a = this.chart, e = this.keyCodes, d = this; return new n(a, {
                                    keyCodeMap: [[[e.tab, e.up, e.down], function (a, k) { return d.onInputKbdMove(this, a === e.tab && k.shiftKey || a === e.up ? -1 : 1) }]], validate: function () { return a.rangeSelector && a.rangeSelector.inputGroup && "hidden" !== a.rangeSelector.inputGroup.element.getAttribute("visibility") && !1 !== a.options.rangeSelector.inputEnabled && a.rangeSelector.minInput && a.rangeSelector.maxInput }, init: function (a) { d.onInputNavInit(a) },
                                    terminate: function () { d.onInputNavTerminate() }
                                })
                            }, onInputKbdMove: function (a, e) { var d = this.chart; a = a.response; var b = d.highlightedInputRangeIx += e; if (1 < b || 0 > b) return a[0 < e ? "next" : "prev"]; d.rangeSelector[b ? "maxInput" : "minInput"].focus(); return a.success }, onInputNavInit: function (a) { var e = this.chart; a = 0 < a ? 0 : 1; e.highlightedInputRangeIx = a; e.rangeSelector[a ? "maxInput" : "minInput"].focus() }, onInputNavTerminate: function () { var a = this.chart.rangeSelector || {}; a.maxInput && a.hideInput("max"); a.minInput && a.hideInput("min") },
                        getKeyboardNavigation: function () { return [this.getRangeSelectorButtonNavigation(), this.getRangeSelectorInputNavigation()] }
                    }); return a
            }); r(a, "modules/accessibility/components/InfoRegionsComponent.js", [a["parts/Globals.js"], a["parts/Utilities.js"], a["modules/accessibility/AccessibilityComponent.js"], a["modules/accessibility/utils/Announcer.js"], a["modules/accessibility/components/AnnotationsA11y.js"], a["modules/accessibility/utils/chartUtilities.js"], a["modules/accessibility/utils/htmlUtilities.js"]],
                function (a, g, q, n, p, f, h) {
                    function l(a) { return a.replace(/&lt;(h[1-7]|p|div|ul|ol|li)&gt;/g, "<$1>").replace(/&lt;&#x2F;(h[1-7]|p|div|ul|ol|li|a|button)&gt;/g, "</$1>").replace(/&lt;(div|a|button) id=&quot;([a-zA-Z\-0-9#]*?)&quot;&gt;/g, '<$1 id="$2">') } var k = a.doc, e = g.extend, d = g.format, b = g.pick, m = p.getAnnotationsInfoHTML, c = f.unhideChartElementFromAT, t = f.getChartTitle, r = f.getAxisDescription, z = h.addClass, w = h.setElAttrs, E = h.escapeStringForHTML, B = h.stripHTMLTagsFromString, C = h.getElement, v = h.visuallyHideElement;
                    a.Chart.prototype.getTypeDescription = function (a) {
                        var b = a[0], c = this.series && this.series[0] || {}; c = { numSeries: this.series.length, numPoints: c.points && c.points.length, chart: this, mapTitle: c.mapTitle }; if (!b) return this.langFormat("accessibility.chartTypes.emptyChart", c); if ("map" === b) return c.mapTitle ? this.langFormat("accessibility.chartTypes.mapTypeDescription", c) : this.langFormat("accessibility.chartTypes.unknownMap", c); if (1 < this.types.length) return this.langFormat("accessibility.chartTypes.combinationChart",
                            c); a = a[0]; b = this.langFormat("accessibility.seriesTypeDescriptions." + a, c); var d = this.series && 2 > this.series.length ? "Single" : "Multiple"; return (this.langFormat("accessibility.chartTypes." + a + d, c) || this.langFormat("accessibility.chartTypes.default" + d, c)) + (b ? " " + b : "")
                    }; g = function () { }; g.prototype = new q; e(g.prototype, {
                        init: function () {
                            var a = this.chart, b = this; this.initRegionsDefinitions(); this.addEvent(a, "afterGetTable", function (a) { b.onDataTableCreated(a) }); this.addEvent(a, "afterViewData", function (a) {
                                b.dataTableDiv =
                                    a; setTimeout(function () { b.focusDataTable() }, 300)
                            }); this.announcer = new n(a, "assertive")
                        }, initRegionsDefinitions: function () {
                            var a = this; this.screenReaderSections = {
                                before: {
                                    element: null, buildContent: function (b) { var c = b.options.accessibility.screenReaderSection.beforeChartFormatter; return c ? c(b) : a.defaultBeforeChartFormatter(b) }, insertIntoDOM: function (a, b) { b.renderTo.insertBefore(a, b.renderTo.firstChild) }, afterInserted: function () {
                                        "undefined" !== typeof a.sonifyButtonId && a.initSonifyButton(a.sonifyButtonId);
                                        "undefined" !== typeof a.dataTableButtonId && a.initDataTableButton(a.dataTableButtonId)
                                    }
                                }, after: { element: null, buildContent: function (b) { var c = b.options.accessibility.screenReaderSection.afterChartFormatter; return c ? c(b) : a.defaultAfterChartFormatter() }, insertIntoDOM: function (a, b) { b.renderTo.insertBefore(a, b.container.nextSibling) } }
                            }
                        }, onChartRender: function () { var a = this; this.linkedDescriptionElement = this.getLinkedDescriptionElement(); this.setLinkedDescriptionAttrs(); Object.keys(this.screenReaderSections).forEach(function (b) { a.updateScreenReaderSection(b) }) },
                        getLinkedDescriptionElement: function () { var a = this.chart.options.accessibility.linkedDescription; if (a) { if ("string" !== typeof a) return a; a = d(a, this.chart); a = k.querySelectorAll(a); if (1 === a.length) return a[0] } }, setLinkedDescriptionAttrs: function () { var a = this.linkedDescriptionElement; a && (a.setAttribute("aria-hidden", "true"), z(a, "highcharts-linked-description")) }, updateScreenReaderSection: function (a) {
                            var b = this.chart, d = this.screenReaderSections[a], e = d.buildContent(b), f = d.element = d.element || this.createElement("div"),
                                k = f.firstChild || this.createElement("div"); this.setScreenReaderSectionAttribs(f, a); k.innerHTML = e; f.appendChild(k); d.insertIntoDOM(f, b); v(k); c(b, k); d.afterInserted && d.afterInserted()
                        }, setScreenReaderSectionAttribs: function (a, b) { var c = this.chart, d = c.langFormat("accessibility.screenReaderSection." + b + "RegionLabel", { chart: c }); w(a, { id: "highcharts-screen-reader-region-" + b + "-" + c.index, "aria-label": d }); a.style.position = "relative"; "all" === c.options.accessibility.landmarkVerbosity && d && a.setAttribute("role", "region") },
                        defaultBeforeChartFormatter: function () {
                            var b, c = this.chart, d = c.options.accessibility.screenReaderSection.beforeChartFormat, e = this.getAxesDescription(), f = c.sonify && (null === (b = c.options.sonification) || void 0 === b ? void 0 : b.enabled); b = "highcharts-a11y-sonify-data-btn-" + c.index; var k = "hc-linkto-highcharts-data-table-" + c.index, h = m(c), g = c.langFormat("accessibility.screenReaderSection.annotations.heading", { chart: c }); e = {
                                chartTitle: t(c), typeDescription: this.getTypeDescriptionText(), chartSubtitle: this.getSubtitleText(),
                                chartLongdesc: this.getLongdescText(), xAxisDescription: e.xAxis, yAxisDescription: e.yAxis, playAsSoundButton: f ? this.getSonifyButtonText(b) : "", viewTableButton: c.getCSV ? this.getDataTableButtonText(k) : "", annotationsTitle: h ? g : "", annotationsList: h
                            }; c = a.i18nFormat(d, e, c); this.dataTableButtonId = k; this.sonifyButtonId = b; return l(E(c)).replace(/<(\w+)[^>]*?>\s*<\/\1>/g, "")
                        }, defaultAfterChartFormatter: function () {
                            var b = this.chart, c = b.options.accessibility.screenReaderSection.afterChartFormat, d = { endOfChartMarker: this.getEndOfChartMarkerText() };
                            b = a.i18nFormat(c, d, b); return l(E(b)).replace(/<(\w+)[^>]*?>\s*<\/\1>/g, "")
                        }, getLinkedDescription: function () { var a = this.linkedDescriptionElement; return B(a && a.innerHTML || "") }, getLongdescText: function () { var a = this.chart.options, b = a.caption; b = b && b.text; var c = this.getLinkedDescription(); return a.accessibility.description || c || b || "" }, getTypeDescriptionText: function () { var a = this.chart; return a.types ? a.options.accessibility.typeDescription || a.getTypeDescription(a.types) : "" }, getDataTableButtonText: function (a) {
                            var b =
                                this.chart; b = b.langFormat("accessibility.table.viewAsDataTableButtonText", { chart: b, chartTitle: t(b) }); return '<a id="' + a + '">' + b + "</a>"
                        }, getSonifyButtonText: function (a) { var b, c = this.chart; if (!1 === (null === (b = c.options.sonification) || void 0 === b ? void 0 : b.enabled)) return ""; b = c.langFormat("accessibility.sonification.playAsSoundButtonText", { chart: c, chartTitle: t(c) }); return '<button id="' + a + '">' + b + "</button>" }, getSubtitleText: function () { var a = this.chart.options.subtitle; return B(a && a.text || "") }, getEndOfChartMarkerText: function () {
                            var a =
                                this.chart, b = a.langFormat("accessibility.screenReaderSection.endOfChartMarker", { chart: a }); return '<div id="highcharts-end-of-chart-marker-' + a.index + '">' + b + "</div>"
                        }, onDataTableCreated: function (a) { var b = this.chart; b.options.accessibility.enabled && (this.viewDataTableButton && this.viewDataTableButton.setAttribute("aria-expanded", "true"), a.html = a.html.replace("<table ", '<table tabindex="-1" summary="' + b.langFormat("accessibility.table.tableSummary", { chart: b }) + '"')) }, focusDataTable: function () {
                            var a = this.dataTableDiv;
                            (a = a && a.getElementsByTagName("table")[0]) && a.focus && a.focus()
                        }, initSonifyButton: function (a) {
                            var b = this, c = this.sonifyButton = C(a), d = this.chart, e = function (a) {
                                null === c || void 0 === c ? void 0 : c.setAttribute("aria-hidden", "true"); null === c || void 0 === c ? void 0 : c.setAttribute("aria-label", ""); a.preventDefault(); a.stopPropagation(); a = d.langFormat("accessibility.sonification.playAsSoundClickAnnouncement", { chart: d }); b.announcer.announce(a); setTimeout(function () {
                                    null === c || void 0 === c ? void 0 : c.removeAttribute("aria-hidden");
                                    null === c || void 0 === c ? void 0 : c.removeAttribute("aria-label"); d.sonify && d.sonify()
                                }, 1E3)
                            }; c && d && (w(c, { tabindex: "-1" }), c.onclick = function (a) { var b; ((null === (b = d.options.accessibility) || void 0 === b ? void 0 : b.screenReaderSection.onPlayAsSoundClick) || e).call(this, a, d) })
                        }, initDataTableButton: function (a) {
                            var b = this.viewDataTableButton = C(a), c = this.chart; a = a.replace("hc-linkto-", ""); b && (w(b, { role: "button", tabindex: "-1", "aria-expanded": !!C(a), href: "#" + a }), b.onclick = c.options.accessibility.screenReaderSection.onViewDataTableClick ||
                                function () { c.viewData() })
                        }, getAxesDescription: function () { var a = this.chart, c = function (c, d) { c = a[c]; return 1 < c.length || c[0] && b(c[0].options.accessibility && c[0].options.accessibility.enabled, d) }, d = !!a.types && 0 > a.types.indexOf("map"), e = !!a.hasCartesianSeries, f = c("xAxis", !a.angular && e && d); c = c("yAxis", e && d); d = {}; f && (d.xAxis = this.getAxisDescriptionText("xAxis")); c && (d.yAxis = this.getAxisDescriptionText("yAxis")); return d }, getAxisDescriptionText: function (a) {
                            var b = this, c = this.chart, d = c[a]; return c.langFormat("accessibility.axis." +
                                a + "Description" + (1 < d.length ? "Plural" : "Singular"), { chart: c, names: d.map(function (a) { return r(a) }), ranges: d.map(function (a) { return b.getAxisRangeDescription(a) }), numAxes: d.length })
                        }, getAxisRangeDescription: function (a) { var b = a.options || {}; return b.accessibility && "undefined" !== typeof b.accessibility.rangeDescription ? b.accessibility.rangeDescription : a.categories ? this.getCategoryAxisRangeDesc(a) : !a.dateTime || 0 !== a.min && 0 !== a.dataMin ? this.getAxisFromToDescription(a) : this.getAxisTimeLengthDesc(a) }, getCategoryAxisRangeDesc: function (a) {
                            var b =
                                this.chart; return a.dataMax && a.dataMin ? b.langFormat("accessibility.axis.rangeCategories", { chart: b, axis: a, numCategories: a.dataMax - a.dataMin + 1 }) : ""
                        }, getAxisTimeLengthDesc: function (a) {
                            var b = this.chart, c = {}, d = "Seconds"; c.Seconds = ((a.max || 0) - (a.min || 0)) / 1E3; c.Minutes = c.Seconds / 60; c.Hours = c.Minutes / 60; c.Days = c.Hours / 24;["Minutes", "Hours", "Days"].forEach(function (a) { 2 < c[a] && (d = a) }); var e = c[d].toFixed("Seconds" !== d && "Minutes" !== d ? 1 : 0); return b.langFormat("accessibility.axis.timeRange" + d, {
                                chart: b, axis: a, range: e.replace(".0",
                                    "")
                            })
                        }, getAxisFromToDescription: function (a) { var b = this.chart, c = b.options.accessibility.screenReaderSection.axisRangeDateFormat, d = function (d) { return a.dateTime ? b.time.dateFormat(c, a[d]) : a[d] }; return b.langFormat("accessibility.axis.rangeFromTo", { chart: b, axis: a, rangeFrom: d("min"), rangeTo: d("max") }) }, destroy: function () { var a; null === (a = this.announcer) || void 0 === a ? void 0 : a.destroy() }
                    }); return g
                }); r(a, "modules/accessibility/components/ContainerComponent.js", [a["parts/Globals.js"], a["parts/Utilities.js"],
                a["modules/accessibility/utils/htmlUtilities.js"], a["modules/accessibility/utils/chartUtilities.js"], a["modules/accessibility/AccessibilityComponent.js"]], function (a, g, q, n, p) {
                    var f = a.win.document; a = g.extend; var h = q.stripHTMLTagsFromString, l = n.unhideChartElementFromAT, k = n.getChartTitle; q = function () { }; q.prototype = new p; a(q.prototype, {
                        onChartUpdate: function () { this.handleSVGTitleElement(); this.setSVGContainerLabel(); this.setGraphicContainerAttrs(); this.setRenderToAttrs(); this.makeCreditsAccessible() },
                        handleSVGTitleElement: function () { var a = this.chart, d = "highcharts-title-" + a.index, b = h(a.langFormat("accessibility.svgContainerTitle", { chartTitle: k(a) })); if (b.length) { var m = this.svgTitleElement = this.svgTitleElement || f.createElementNS("http://www.w3.org/2000/svg", "title"); m.textContent = b; m.id = d; a.renderTo.insertBefore(m, a.renderTo.firstChild) } }, setSVGContainerLabel: function () {
                            var a = this.chart, d = h(a.langFormat("accessibility.svgContainerLabel", { chartTitle: k(a) })); a.renderer.box && d.length && a.renderer.box.setAttribute("aria-label",
                                d)
                        }, setGraphicContainerAttrs: function () { var a = this.chart, d = a.langFormat("accessibility.graphicContainerLabel", { chartTitle: k(a) }); d.length && a.container.setAttribute("aria-label", d) }, setRenderToAttrs: function () { var a = this.chart; "disabled" !== a.options.accessibility.landmarkVerbosity ? a.renderTo.setAttribute("role", "region") : a.renderTo.removeAttribute("role"); a.renderTo.setAttribute("aria-label", a.langFormat("accessibility.chartContainerLabel", { title: k(a), chart: a })) }, makeCreditsAccessible: function () {
                            var a =
                                this.chart, d = a.credits; d && (d.textStr && d.element.setAttribute("aria-label", h(a.langFormat("accessibility.credits", { creditsStr: d.textStr }))), l(a, d.element))
                        }, destroy: function () { this.chart.renderTo.setAttribute("aria-hidden", !0) }
                    }); return q
                }); r(a, "modules/accessibility/high-contrast-mode.js", [a["parts/Globals.js"]], function (a) {
                    var g = a.isMS, l = a.win, n = l.document; return {
                        isHighContrastModeActive: function () {
                            var a = /(Edg)/.test(l.navigator.userAgent); if (l.matchMedia && a) return l.matchMedia("(-ms-high-contrast: active)").matches;
                            if (g && l.getComputedStyle) { a = n.createElement("div"); a.style.backgroundImage = "url(data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==)"; n.body.appendChild(a); var f = (a.currentStyle || l.getComputedStyle(a)).backgroundImage; n.body.removeChild(a); return "none" === f } return !1
                        }, setHighContrastTheme: function (a) {
                            a.highContrastModeActive = !0; var f = a.options.accessibility.highContrastTheme; a.update(f, !1); a.series.forEach(function (a) {
                                var h = f.plotOptions[a.type] || {}; a.update({
                                    color: h.color || "windowText",
                                    colors: [h.color || "windowText"], borderColor: h.borderColor || "window"
                                }); a.points.forEach(function (a) { a.options && a.options.color && a.update({ color: h.color || "windowText", borderColor: h.borderColor || "window" }, !1) })
                            }); a.redraw()
                        }
                    }
                }); r(a, "modules/accessibility/high-contrast-theme.js", [], function () {
                    return {
                        chart: { backgroundColor: "window" }, title: { style: { color: "windowText" } }, subtitle: { style: { color: "windowText" } }, colorAxis: { minColor: "windowText", maxColor: "windowText", stops: [] }, colors: ["windowText"], xAxis: {
                            gridLineColor: "windowText",
                            labels: { style: { color: "windowText" } }, lineColor: "windowText", minorGridLineColor: "windowText", tickColor: "windowText", title: { style: { color: "windowText" } }
                        }, yAxis: { gridLineColor: "windowText", labels: { style: { color: "windowText" } }, lineColor: "windowText", minorGridLineColor: "windowText", tickColor: "windowText", title: { style: { color: "windowText" } } }, tooltip: { backgroundColor: "window", borderColor: "windowText", style: { color: "windowText" } }, plotOptions: {
                            series: {
                                lineColor: "windowText", fillColor: "window", borderColor: "windowText",
                                edgeColor: "windowText", borderWidth: 1, dataLabels: { connectorColor: "windowText", color: "windowText", style: { color: "windowText", textOutline: "none" } }, marker: { lineColor: "windowText", fillColor: "windowText" }
                            }, pie: { color: "window", colors: ["window"], borderColor: "windowText", borderWidth: 1 }, boxplot: { fillColor: "window" }, candlestick: { lineColor: "windowText", fillColor: "window" }, errorbar: { fillColor: "window" }
                        }, legend: {
                            backgroundColor: "window", itemStyle: { color: "windowText" }, itemHoverStyle: { color: "windowText" }, itemHiddenStyle: { color: "#555" },
                            title: { style: { color: "windowText" } }
                        }, credits: { style: { color: "windowText" } }, labels: { style: { color: "windowText" } }, drilldown: { activeAxisLabelStyle: { color: "windowText" }, activeDataLabelStyle: { color: "windowText" } }, navigation: { buttonOptions: { symbolStroke: "windowText", theme: { fill: "window" } } }, rangeSelector: {
                            buttonTheme: { fill: "window", stroke: "windowText", style: { color: "windowText" }, states: { hover: { fill: "window", stroke: "windowText", style: { color: "windowText" } }, select: { fill: "#444", stroke: "windowText", style: { color: "windowText" } } } },
                            inputBoxBorderColor: "windowText", inputStyle: { backgroundColor: "window", color: "windowText" }, labelStyle: { color: "windowText" }
                        }, navigator: { handles: { backgroundColor: "window", borderColor: "windowText" }, outlineColor: "windowText", maskFill: "transparent", series: { color: "windowText", lineColor: "windowText" }, xAxis: { gridLineColor: "windowText" } }, scrollbar: {
                            barBackgroundColor: "#444", barBorderColor: "windowText", buttonArrowColor: "windowText", buttonBackgroundColor: "window", buttonBorderColor: "windowText", rifleColor: "windowText",
                            trackBackgroundColor: "window", trackBorderColor: "windowText"
                        }
                    }
                }); r(a, "modules/accessibility/options/options.js", [], function () {
                    return {
                        accessibility: {
                            enabled: !0, screenReaderSection: {
                                beforeChartFormat: "<h5>{chartTitle}</h5><div>{typeDescription}</div><div>{chartSubtitle}</div><div>{chartLongdesc}</div><div>{playAsSoundButton}</div><div>{viewTableButton}</div><div>{xAxisDescription}</div><div>{yAxisDescription}</div><div>{annotationsTitle}{annotationsList}</div>", afterChartFormat: "{endOfChartMarker}",
                                axisRangeDateFormat: "%Y-%m-%d %H:%M:%S"
                            }, series: { describeSingleSeries: !1, pointDescriptionEnabledThreshold: 200 }, point: { valueDescriptionFormat: "{index}. {xDescription}{separator}{value}." }, landmarkVerbosity: "all", linkedDescription: '*[data-highcharts-chart="{index}"] + .highcharts-description', keyboardNavigation: {
                                enabled: !0, focusBorder: { enabled: !0, hideBrowserFocusOutline: !0, style: { color: "#335cad", lineWidth: 2, borderRadius: 3 }, margin: 2 }, order: ["series", "zoom", "rangeSelector", "legend", "chartMenu"], wrapAround: !0,
                                seriesNavigation: { skipNullPoints: !0, pointNavigationEnabledThreshold: !1 }
                            }, announceNewData: { enabled: !1, minAnnounceInterval: 5E3, interruptUser: !1 }
                        }, legend: { accessibility: { enabled: !0, keyboardNavigation: { enabled: !0 } } }, exporting: { accessibility: { enabled: !0 } }
                    }
                }); r(a, "modules/accessibility/options/langOptions.js", [], function () {
                    return {
                        accessibility: {
                            defaultChartTitle: "Chart", chartContainerLabel: "{title}. Highcharts interactive chart.", svgContainerLabel: "Interactive chart", drillUpButton: "{buttonText}", credits: "Chart credits: {creditsStr}",
                            thousandsSep: ",", svgContainerTitle: "", graphicContainerLabel: "", screenReaderSection: { beforeRegionLabel: "Chart screen reader information.", afterRegionLabel: "", annotations: { heading: "Chart annotations summary", descriptionSinglePoint: "{annotationText}. Related to {annotationPoint}", descriptionMultiplePoints: "{annotationText}. Related to {annotationPoint}{ Also related to, #each(additionalAnnotationPoints)}", descriptionNoPoints: "{annotationText}" }, endOfChartMarker: "End of interactive chart." }, sonification: {
                                playAsSoundButtonText: "Play as sound, {chartTitle}",
                                playAsSoundClickAnnouncement: "Play"
                            }, legend: { legendLabel: "Toggle series visibility", legendItem: "Toggle visibility of {itemName}" }, zoom: { mapZoomIn: "Zoom chart", mapZoomOut: "Zoom out chart", resetZoomButton: "Reset zoom" }, rangeSelector: { minInputLabel: "Select start date.", maxInputLabel: "Select end date.", buttonText: "Select range {buttonText}" }, table: { viewAsDataTableButtonText: "View as data table, {chartTitle}", tableSummary: "Table representation of chart." }, announceNewData: {
                                newDataAnnounce: "Updated data for chart {chartTitle}",
                                newSeriesAnnounceSingle: "New data series: {seriesDesc}", newPointAnnounceSingle: "New data point: {pointDesc}", newSeriesAnnounceMultiple: "New data series in chart {chartTitle}: {seriesDesc}", newPointAnnounceMultiple: "New data point in chart {chartTitle}: {pointDesc}"
                            }, seriesTypeDescriptions: {
                                boxplot: "Box plot charts are typically used to display groups of statistical data. Each data point in the chart can have up to 5 values: minimum, lower quartile, median, upper quartile, and maximum.", arearange: "Arearange charts are line charts displaying a range between a lower and higher value for each point.",
                                areasplinerange: "These charts are line charts displaying a range between a lower and higher value for each point.", bubble: "Bubble charts are scatter charts where each data point also has a size value.", columnrange: "Columnrange charts are column charts displaying a range between a lower and higher value for each point.", errorbar: "Errorbar series are used to display the variability of the data.", funnel: "Funnel charts are used to display reduction of data in stages.", pyramid: "Pyramid charts consist of a single pyramid with item heights corresponding to each point value.",
                                waterfall: "A waterfall chart is a column chart where each column contributes towards a total end value."
                            }, chartTypes: {
                                emptyChart: "Empty chart", mapTypeDescription: "Map of {mapTitle} with {numSeries} data series.", unknownMap: "Map of unspecified region with {numSeries} data series.", combinationChart: "Combination chart with {numSeries} data series.", defaultSingle: "Chart with {numPoints} data {#plural(numPoints, points, point)}.", defaultMultiple: "Chart with {numSeries} data series.", splineSingle: "Line chart with {numPoints} data {#plural(numPoints, points, point)}.",
                                splineMultiple: "Line chart with {numSeries} lines.", lineSingle: "Line chart with {numPoints} data {#plural(numPoints, points, point)}.", lineMultiple: "Line chart with {numSeries} lines.", columnSingle: "Bar chart with {numPoints} {#plural(numPoints, bars, bar)}.", columnMultiple: "Bar chart with {numSeries} data series.", barSingle: "Bar chart with {numPoints} {#plural(numPoints, bars, bar)}.", barMultiple: "Bar chart with {numSeries} data series.", pieSingle: "Pie chart with {numPoints} {#plural(numPoints, slices, slice)}.",
                                pieMultiple: "Pie chart with {numSeries} pies.", scatterSingle: "Scatter chart with {numPoints} {#plural(numPoints, points, point)}.", scatterMultiple: "Scatter chart with {numSeries} data series.", boxplotSingle: "Boxplot with {numPoints} {#plural(numPoints, boxes, box)}.", boxplotMultiple: "Boxplot with {numSeries} data series.", bubbleSingle: "Bubble chart with {numPoints} {#plural(numPoints, bubbles, bubble)}.", bubbleMultiple: "Bubble chart with {numSeries} data series."
                            }, axis: {
                                xAxisDescriptionSingular: "The chart has 1 X axis displaying {names[0]}. {ranges[0]}",
                                xAxisDescriptionPlural: "The chart has {numAxes} X axes displaying {#each(names, -1) }and {names[-1]}.", yAxisDescriptionSingular: "The chart has 1 Y axis displaying {names[0]}. {ranges[0]}", yAxisDescriptionPlural: "The chart has {numAxes} Y axes displaying {#each(names, -1) }and {names[-1]}.", timeRangeDays: "Range: {range} days.", timeRangeHours: "Range: {range} hours.", timeRangeMinutes: "Range: {range} minutes.", timeRangeSeconds: "Range: {range} seconds.", rangeFromTo: "Range: {rangeFrom} to {rangeTo}.", rangeCategories: "Range: {numCategories} categories."
                            },
                            exporting: { chartMenuLabel: "Chart menu", menuButtonLabel: "View chart menu", exportRegionLabel: "Chart menu" }, series: {
                                summary: {
                                    "default": "{name}, series {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", defaultCombination: "{name}, series {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", line: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", lineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.",
                                    spline: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", splineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.", column: "{name}, bar series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bars, bar)}.", columnCombination: "{name}, series {ix} of {numSeries}. Bar series with {numPoints} {#plural(numPoints, bars, bar)}.", bar: "{name}, bar series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bars, bar)}.",
                                    barCombination: "{name}, series {ix} of {numSeries}. Bar series with {numPoints} {#plural(numPoints, bars, bar)}.", pie: "{name}, pie {ix} of {numSeries} with {numPoints} {#plural(numPoints, slices, slice)}.", pieCombination: "{name}, series {ix} of {numSeries}. Pie with {numPoints} {#plural(numPoints, slices, slice)}.", scatter: "{name}, scatter plot {ix} of {numSeries} with {numPoints} {#plural(numPoints, points, point)}.", scatterCombination: "{name}, series {ix} of {numSeries}, scatter plot with {numPoints} {#plural(numPoints, points, point)}.",
                                    boxplot: "{name}, boxplot {ix} of {numSeries} with {numPoints} {#plural(numPoints, boxes, box)}.", boxplotCombination: "{name}, series {ix} of {numSeries}. Boxplot with {numPoints} {#plural(numPoints, boxes, box)}.", bubble: "{name}, bubble series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bubbles, bubble)}.", bubbleCombination: "{name}, series {ix} of {numSeries}. Bubble series with {numPoints} {#plural(numPoints, bubbles, bubble)}.", map: "{name}, map {ix} of {numSeries} with {numPoints} {#plural(numPoints, areas, area)}.",
                                    mapCombination: "{name}, series {ix} of {numSeries}. Map with {numPoints} {#plural(numPoints, areas, area)}.", mapline: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", maplineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.", mapbubble: "{name}, bubble series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bubbles, bubble)}.", mapbubbleCombination: "{name}, series {ix} of {numSeries}. Bubble series with {numPoints} {#plural(numPoints, bubbles, bubble)}."
                                },
                                description: "{description}", xAxisDescription: "X axis, {name}", yAxisDescription: "Y axis, {name}", nullPointValue: "No value", pointAnnotationsDescription: "{Annotation: #each(annotations). }"
                            }
                        }
                    }
                }); r(a, "modules/accessibility/options/deprecatedOptions.js", [a["parts/Utilities.js"]], function (a) {
                    function g(a, e, d) { for (var b, f = 0; f < e.length - 1; ++f)b = e[f], a = a[b] = x(a[b], {}); a[e[e.length - 1]] = d } function l(a, e, d, b) {
                        function f(a, b) { return b.reduce(function (a, b) { return a[b] }, a) } var c = f(a.options, e), k = f(a.options, d); Object.keys(b).forEach(function (f) {
                            var m,
                                l = c[f]; "undefined" !== typeof l && (g(k, b[f], l), h(32, !1, a, (m = {}, m[e.join(".") + "." + f] = d.join(".") + "." + b[f].join("."), m)))
                        })
                    } function n(a) { var e = a.options.chart || {}, d = a.options.accessibility || {};["description", "typeDescription"].forEach(function (b) { var f; e[b] && (d[b] = e[b], h(32, !1, a, (f = {}, f["chart." + b] = "use accessibility." + b, f))) }) } function p(a) { a.axes.forEach(function (e) { (e = e.options) && e.description && (e.accessibility = e.accessibility || {}, e.accessibility.description = e.description, h(32, !1, a, { "axis.description": "use axis.accessibility.description" })) }) }
                    function f(a) { var e = { description: ["accessibility", "description"], exposeElementToA11y: ["accessibility", "exposeAsGroupOnly"], pointDescriptionFormatter: ["accessibility", "pointDescriptionFormatter"], skipKeyboardNavigation: ["accessibility", "keyboardNavigation", "enabled"] }; a.series.forEach(function (d) { Object.keys(e).forEach(function (b) { var f, c = d.options[b]; "undefined" !== typeof c && (g(d.options, e[b], "skipKeyboardNavigation" === b ? !c : c), h(32, !1, a, (f = {}, f["series." + b] = "series." + e[b].join("."), f))) }) }) } var h =
                        a.error, x = a.pick; return function (a) {
                            n(a); p(a); a.series && f(a); l(a, ["accessibility"], ["accessibility"], {
                                pointDateFormat: ["point", "dateFormat"], pointDateFormatter: ["point", "dateFormatter"], pointDescriptionFormatter: ["point", "descriptionFormatter"], pointDescriptionThreshold: ["series", "pointDescriptionEnabledThreshold"], pointNavigationThreshold: ["keyboardNavigation", "seriesNavigation", "pointNavigationEnabledThreshold"], pointValueDecimals: ["point", "valueDecimals"], pointValuePrefix: ["point", "valuePrefix"],
                                pointValueSuffix: ["point", "valueSuffix"], screenReaderSectionFormatter: ["screenReaderSection", "beforeChartFormatter"], describeSingleSeries: ["series", "describeSingleSeries"], seriesDescriptionFormatter: ["series", "descriptionFormatter"], onTableAnchorClick: ["screenReaderSection", "onViewDataTableClick"], axisRangeDateFormat: ["screenReaderSection", "axisRangeDateFormat"]
                            }); l(a, ["accessibility", "keyboardNavigation"], ["accessibility", "keyboardNavigation", "seriesNavigation"], {
                                skipNullPoints: ["skipNullPoints"],
                                mode: ["mode"]
                            }); l(a, ["lang", "accessibility"], ["lang", "accessibility"], {
                                legendItem: ["legend", "legendItem"], legendLabel: ["legend", "legendLabel"], mapZoomIn: ["zoom", "mapZoomIn"], mapZoomOut: ["zoom", "mapZoomOut"], resetZoomButton: ["zoom", "resetZoomButton"], screenReaderRegionLabel: ["screenReaderSection", "beforeRegionLabel"], rangeSelectorButton: ["rangeSelector", "buttonText"], rangeSelectorMaxInput: ["rangeSelector", "maxInputLabel"], rangeSelectorMinInput: ["rangeSelector", "minInputLabel"], svgContainerEnd: ["screenReaderSection",
                                    "endOfChartMarker"], viewAsDataTable: ["table", "viewAsDataTableButtonText"], tableSummary: ["table", "tableSummary"]
                            })
                        }
                }); r(a, "modules/accessibility/a11y-i18n.js", [a["parts/Globals.js"], a["parts/Utilities.js"]], function (a, g) {
                    function l(a, h) {
                        var f = a.indexOf("#each("), k = a.indexOf("#plural("), e = a.indexOf("["), d = a.indexOf("]"); if (-1 < f) {
                            e = a.slice(f).indexOf(")") + f; var b = a.substring(0, f); k = a.substring(e + 1); e = a.substring(f + 6, e).split(","); f = Number(e[1]); a = ""; if (h = h[e[0]]) for (f = isNaN(f) ? h.length : f, f = 0 > f ? h.length +
                                f : Math.min(f, h.length), e = 0; e < f; ++e)a += b + h[e] + k; return a.length ? a : ""
                        } if (-1 < k) { b = a.slice(k).indexOf(")") + k; a = a.substring(k + 8, b).split(","); switch (Number(h[a[0]])) { case 0: a = p(a[4], a[1]); break; case 1: a = p(a[2], a[1]); break; case 2: a = p(a[3], a[1]); break; default: a = a[1] }a ? (h = a, h = h.trim && h.trim() || h.replace(/^\s+|\s+$/g, "")) : h = ""; return h } return -1 < e ? (k = a.substring(0, e), a = Number(a.substring(e + 1, d)), h = h[k], !isNaN(a) && h && (0 > a ? (b = h[h.length + a], "undefined" === typeof b && (b = h[0])) : (b = h[a], "undefined" === typeof b && (b =
                            h[h.length - 1]))), "undefined" !== typeof b ? b : "") : "{" + a + "}"
                    } var n = g.format, p = g.pick; a.i18nFormat = function (a, h, g) {
                        var f = function (a, b) { a = a.slice(b || 0); var c = a.indexOf("{"), d = a.indexOf("}"); if (-1 < c && d > c) return { statement: a.substring(c + 1, d), begin: b + c + 1, end: b + d } }, e = [], d = 0; do { var b = f(a, d); var m = a.substring(d, b && b.begin - 1); m.length && e.push({ value: m, type: "constant" }); b && e.push({ value: b.statement, type: "statement" }); d = b ? b.end + 1 : d + 1 } while (b); e.forEach(function (a) { "statement" === a.type && (a.value = l(a.value, h)) }); return n(e.reduce(function (a,
                            b) { return a + b.value }, ""), h, g)
                    }; a.Chart.prototype.langFormat = function (f, h) { f = f.split("."); for (var g = this.options.lang, k = 0; k < f.length; ++k)g = g && g[f[k]]; return "string" === typeof g ? a.i18nFormat(g, h, this) : "" }
                }); r(a, "modules/accessibility/focusBorder.js", [a["parts/Globals.js"], a["parts/SVGElement.js"], a["parts/SVGLabel.js"], a["parts/Utilities.js"]], function (a, g, q, n) {
                    function l(a) {
                        if (!a.focusBorderDestroyHook) {
                            var b = a.destroy; a.destroy = function () {
                                var c, d; null === (d = null === (c = a.focusBorder) || void 0 === c ? void 0 :
                                    c.destroy) || void 0 === d ? void 0 : d.call(c); return b.apply(a, arguments)
                            }; a.focusBorderDestroyHook = b
                        }
                    } function f(a) { for (var b = [], c = 1; c < arguments.length; c++)b[c - 1] = arguments[c]; a.focusBorderUpdateHooks || (a.focusBorderUpdateHooks = {}, d.forEach(function (c) { c += "Setter"; var d = a[c] || a._defaultSetter; a.focusBorderUpdateHooks[c] = d; a[c] = function () { var c = d.apply(a, arguments); a.addFocusBorder.apply(a, b); return c } })) } function h(a) {
                        a.focusBorderUpdateHooks && (Object.keys(a.focusBorderUpdateHooks).forEach(function (b) {
                            var c =
                                a.focusBorderUpdateHooks[b]; c === a._defaultSetter ? delete a[b] : a[b] = c
                        }), delete a.focusBorderUpdateHooks)
                    } var r = n.addEvent, k = n.extend, e = n.pick, d = "x y transform width height r d stroke-width".split(" "); k(g.prototype, {
                        addFocusBorder: function (b, d) {
                            this.focusBorder && this.removeFocusBorder(); var c = this.getBBox(), g = e(b, 3); c.x += this.translateX ? this.translateX : 0; c.y += this.translateY ? this.translateY : 0; var h = c.x - g, k = c.y - g, m = c.width + 2 * g, n = c.height + 2 * g, p = this instanceof q; if ("text" === this.element.nodeName || p) {
                                var r =
                                    !!this.rotation; if (p) var v = { x: r ? 1 : 0, y: 0 }; else h = v = 0, "middle" === this.attr("text-anchor") ? (v = a.isFirefox && this.rotation ? .25 : .5, h = a.isFirefox && !this.rotation ? .75 : .5) : this.rotation ? v = .25 : h = .75, v = { x: v, y: h }; h = +this.attr("x") - c.width * v.x - g; k = +this.attr("y") - c.height * v.y - g; p && r && (p = m, m = n, n = p, h = +this.attr("x") - c.height * v.x - g, k = +this.attr("y") - c.width * v.y - g)
                            } this.focusBorder = this.renderer.rect(h, k, m, n, parseInt((d && d.borderRadius || 0).toString(), 10)).addClass("highcharts-focus-border").attr({ zIndex: 99 }).add(this.parentGroup);
                            this.renderer.styledMode || this.focusBorder.attr({ stroke: d && d.stroke, "stroke-width": d && d.strokeWidth }); f(this, b, d); l(this)
                        }, removeFocusBorder: function () { h(this); this.focusBorderDestroyHook && (this.destroy = this.focusBorderDestroyHook, delete this.focusBorderDestroyHook); this.focusBorder && (this.focusBorder.destroy(), delete this.focusBorder) }
                    }); a.Chart.prototype.renderFocusBorder = function () {
                        var a = this.focusElement, d = this.options.accessibility.keyboardNavigation.focusBorder; a && (a.removeFocusBorder(), d.enabled &&
                            a.addFocusBorder(d.margin, { stroke: d.style.color, strokeWidth: d.style.lineWidth, borderRadius: d.style.borderRadius }))
                    }; a.Chart.prototype.setFocusToElement = function (a, d) { var b = this.options.accessibility.keyboardNavigation.focusBorder; (d = d || a.element) && d.focus && (d.hcEvents && d.hcEvents.focusin || r(d, "focusin", function () { }), d.focus(), b.hideBrowserFocusOutline && (d.style.outline = "none")); this.focusElement && this.focusElement.removeFocusBorder(); this.focusElement = a; this.renderFocusBorder() }
                }); r(a, "modules/accessibility/accessibility.js",
                    [a["modules/accessibility/utils/chartUtilities.js"], a["parts/Globals.js"], a["modules/accessibility/KeyboardNavigationHandler.js"], a["parts/Options.js"], a["parts/Point.js"], a["parts/Utilities.js"], a["modules/accessibility/AccessibilityComponent.js"], a["modules/accessibility/KeyboardNavigation.js"], a["modules/accessibility/components/LegendComponent.js"], a["modules/accessibility/components/MenuComponent.js"], a["modules/accessibility/components/SeriesComponent/SeriesComponent.js"], a["modules/accessibility/components/ZoomComponent.js"],
                    a["modules/accessibility/components/RangeSelectorComponent.js"], a["modules/accessibility/components/InfoRegionsComponent.js"], a["modules/accessibility/components/ContainerComponent.js"], a["modules/accessibility/high-contrast-mode.js"], a["modules/accessibility/high-contrast-theme.js"], a["modules/accessibility/options/options.js"], a["modules/accessibility/options/langOptions.js"], a["modules/accessibility/options/deprecatedOptions.js"]], function (a, g, q, n, p, f, h, r, k, e, d, b, m, c, t, A, z, w, E, B) {
                        function l(a) { this.init(a) }
                        var v = f.addEvent, u = f.extend, x = f.fireEvent, y = f.merge, D = g.win.document; y(!0, n.defaultOptions, w, { accessibility: { highContrastTheme: z }, lang: E }); g.A11yChartUtilities = a; g.KeyboardNavigationHandler = q; g.AccessibilityComponent = h; l.prototype = {
                            init: function (a) { this.chart = a; D.addEventListener && a.renderer.isSVG ? (B(a), this.initComponents(), this.keyboardNavigation = new r(a, this.components), this.update()) : a.renderTo.setAttribute("aria-hidden", !0) }, initComponents: function () {
                                var a = this.chart, f = a.options.accessibility;
                                this.components = { container: new t, infoRegions: new c, legend: new k, chartMenu: new e, rangeSelector: new m, series: new d, zoom: new b }; f.customComponents && u(this.components, f.customComponents); var g = this.components; this.getComponentOrder().forEach(function (b) { g[b].initBase(a); g[b].init() })
                            }, getComponentOrder: function () { if (!this.components) return []; if (!this.components.series) return Object.keys(this.components); var a = Object.keys(this.components).filter(function (a) { return "series" !== a }); return ["series"].concat(a) },
                            update: function () { var a = this.components, b = this.chart, c = b.options.accessibility; x(b, "beforeA11yUpdate"); b.types = this.getChartTypes(); this.getComponentOrder().forEach(function (c) { a[c].onChartUpdate(); x(b, "afterA11yComponentUpdate", { name: c, component: a[c] }) }); this.keyboardNavigation.update(c.keyboardNavigation.order); !b.highContrastModeActive && A.isHighContrastModeActive() && A.setHighContrastTheme(b); x(b, "afterA11yUpdate", { accessibility: this }) }, destroy: function () {
                                var a = this.chart || {}, b = this.components;
                                Object.keys(b).forEach(function (a) { b[a].destroy(); b[a].destroyBase() }); this.keyboardNavigation && this.keyboardNavigation.destroy(); a.renderTo && a.renderTo.setAttribute("aria-hidden", !0); a.focusElement && a.focusElement.removeFocusBorder()
                            }, getChartTypes: function () { var a = {}; this.chart.series.forEach(function (b) { a[b.type] = 1 }); return Object.keys(a) }
                        }; g.Chart.prototype.updateA11yEnabled = function () {
                            var a = this.accessibility, b = this.options.accessibility; b && b.enabled ? a ? a.update() : this.accessibility = new l(this) :
                                a ? (a.destroy && a.destroy(), delete this.accessibility) : this.renderTo.setAttribute("aria-hidden", !0)
                        }; v(g.Chart, "render", function (a) { this.a11yDirty && this.renderTo && (delete this.a11yDirty, this.updateA11yEnabled()); var b = this.accessibility; b && b.getComponentOrder().forEach(function (a) { b.components[a].onChartRender() }) }); v(g.Chart, "update", function (a) {
                            if (a = a.options.accessibility) a.customComponents && (this.options.accessibility.customComponents = a.customComponents, delete a.customComponents), y(!0, this.options.accessibility,
                                a), this.accessibility && this.accessibility.destroy && (this.accessibility.destroy(), delete this.accessibility); this.a11yDirty = !0
                        }); v(p, "update", function () { this.series.chart.accessibility && (this.series.chart.a11yDirty = !0) });["addSeries", "init"].forEach(function (a) { v(g.Chart, a, function () { this.a11yDirty = !0 }) });["update", "updatedData", "remove"].forEach(function (a) { v(g.Series, a, function () { this.chart.accessibility && (this.chart.a11yDirty = !0) }) });["afterDrilldown", "drillupall"].forEach(function (a) {
                            v(g.Chart,
                                a, function () { this.accessibility && this.accessibility.update() })
                        }); v(g.Chart, "destroy", function () { this.accessibility && this.accessibility.destroy() })
                    }); r(a, "masters/modules/accessibility.src.js", [], function () { })
});
//# sourceMappingURL=accessibility.js.map