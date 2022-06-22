var MXI_DEBUG = !0;
(function (b, c) {
    function h(l, f) { for (var d, g = [], a = 0; a < l.length; ++a) { if (!(d = k[l[a]])) a: { d = b; for (var q = l[a].split(/[.\/]/), m = 0; m < q.length; ++m) { if (!d[q[m]]) { d = void 0; break a } d = d[q[m]] } } if (!d) throw "module definition dependecy not found: " + l[a]; g.push(d) } f.apply(null, g) } function e(b, f, d) {
        if ("string" !== typeof b) throw "invalid module definition, module id must be defined and be a string"; if (f === c) throw "invalid module definition, dependencies must be specified"; if (d === c) throw "invalid module definition, definition function must be specified"; h(f,
        function () { k[b] = d.apply(null, arguments) })
    } var k = {}; e("moxie/core/utils/Basic", [], function () {
        var b = function (a) { return void 0 === a ? "undefined" : null === a ? "null" : a.nodeType ? "node" : {}.toString.call(a).match(/\s([a-z|A-Z]+)/)[1].toLowerCase() }, f = function (a) { d(arguments, function (q, m) { 0 < m && d(q, function (d, m) { void 0 !== d && (b(a[m]) === b(d) && ~g(b(d), ["array", "object"]) ? f(a[m], d) : a[m] = d) }) }); return a }, d = function (a, d) {
            var g, f; if (a) if ("number" === b(a.length)) for (f = 0, g = a.length; f < g && !1 !== d(a[f], f) ; f++); else if ("object" ===
            b(a)) for (g in a) if (a.hasOwnProperty(g) && !1 === d(a[g], g)) break
        }, g = function (a, d) { if (d) { if (Array.prototype.indexOf) return Array.prototype.indexOf.call(d, a); for (var g = 0, b = d.length; g < b; g++) if (d[g] === a) return g } return -1 }; return {
            guid: function () { var a = 0; return function (d) { var g = (new Date).getTime().toString(32), b; for (b = 0; 5 > b; b++) g += Math.floor(65535 * Math.random()).toString(32); return (d || "o_") + g + (a++).toString(32) } }(), typeOf: b, extend: f, each: d, isEmptyObj: function (a) {
                var d; if (!a || "object" !== b(a)) return !0; for (d in a) return !1;
                return !0
            }, inSeries: function (a, d) { function g(c) { if ("function" === b(a[c])) a[c](function (a) { ++c < f && !a ? g(c) : d(a) }) } var f = a.length; "function" !== b(d) && (d = function () { }); a && a.length || d(); g(0) }, inParallel: function (a, g) { var b = 0, f = a.length, l = Array(f); d(a, function (a, d) { a(function (a) { if (a) return g(a); var c = [].slice.call(arguments); c.shift(); l[d] = c; b++; b === f && (l.unshift(null), g.apply(this, l)) }) }) }, inArray: g, arrayDiff: function (a, d) {
                var f = []; "array" !== b(a) && (a = [a]); "array" !== b(d) && (d = [d]); for (var c in a) -1 === g(a[c],
                d) && f.push(a[c]); return f.length ? f : !1
            }, arrayIntersect: function (a, b) { var f = []; d(a, function (a) { -1 !== g(a, b) && f.push(a) }); return f.length ? f : null }, toArray: function (a) { var d, g = []; for (d = 0; d < a.length; d++) g[d] = a[d]; return g }, trim: function (a) { return a ? String.prototype.trim ? String.prototype.trim.call(a) : a.toString().replace(/^\s*/, "").replace(/\s*$/, "") : a }, sprintf: function (a) { var d = [].slice.call(arguments, 1); return a.replace(/%[a-z]/g, function () { var a = d.shift(); return "undefined" !== b(a) ? a : "" }) }, parseSizeStr: function (a) {
                if ("string" !==
                typeof a) return a; var d = { t: 1099511627776, g: 1073741824, m: 1048576, k: 1024 }, g; a = /^([0-9\.]+)([tmgk]?)$/.exec(a.toLowerCase().replace(/[^0-9\.tmkg]/g, "")); g = a[2]; a = +a[1]; d.hasOwnProperty(g) && (a *= d[g]); return Math.floor(a)
            }
        }
    }); e("moxie/core/utils/Env", ["moxie/core/utils/Basic"], function (b) {
        var f = function (a) {
            var d = { has: function (a, d) { return -1 !== d.toLowerCase().indexOf(a.toLowerCase()) }, lowerize: function (a) { return a.toLowerCase() } }, g = function () {
                for (var d, g = 0, b, f, l, c, m, u = arguments; g < u.length; g += 2) {
                    m = u[g]; var w =
                    u[g + 1]; if ("undefined" === typeof d) for (l in d = {}, w) b = w[l], "object" === typeof b ? d[b[0]] = a : d[b] = a; for (b = f = 0; b < m.length; b++) if (c = m[b].exec(this.getUA())) { for (l = 0; l < w.length; l++) m = c[++f], b = w[l], "object" === typeof b && 0 < b.length ? 2 == b.length ? d[b[0]] = "function" == typeof b[1] ? b[1].call(this, m) : b[1] : 3 == b.length ? d[b[0]] = "function" !== typeof b[1] || b[1].exec && b[1].test ? m ? m.replace(b[1], b[2]) : a : m ? b[1].call(this, m, b[2]) : a : 4 == b.length && (d[b[0]] = m ? b[3].call(this, m.replace(b[1], b[2])) : a) : d[b] = m ? m : a; break } if (c) break
                } return d
            },
            b = function (b, g) { for (var f in g) if ("object" === typeof g[f] && 0 < g[f].length) for (var l = 0; l < g[f].length; l++) { if (d.has(g[f][l], b)) return "?" === f ? a : f } else if (d.has(g[f], b)) return "?" === f ? a : f; return b }, f = { ME: "4.90", "NT 3.11": "NT3.51", "NT 4.0": "NT4.0", 2E3: "NT 5.0", XP: ["NT 5.1", "NT 5.2"], Vista: "NT 6.0", 7: "NT 6.1", 8: "NT 6.2", "8.1": "NT 6.3", RT: "ARM" }, l = [[/(opera\smini)\/([\w\.-]+)/i, /(opera\s[mobiletab]+).+version\/([\w\.-]+)/i, /(opera).+version\/([\w\.]+)/i, /(opera)[\/\s]+([\w\.]+)/i], ["name", "version"], [/\s(opr)\/([\w\.]+)/i],
            [["name", "Opera"], "version"], [/(kindle)\/([\w\.]+)/i, /(lunascape|maxthon|netfront|jasmine|blazer)[\/\s]?([\w\.]+)*/i, /(avant\s|iemobile|slim|baidu)(?:browser)?[\/\s]?([\w\.]*)/i, /(?:ms|\()(ie)\s([\w\.]+)/i, /(rekonq)\/([\w\.]+)*/i, /(chromium|flock|rockmelt|midori|epiphany|silk|skyfire|ovibrowser|bolt|iron|vivaldi)\/([\w\.-]+)/i], ["name", "version"], [/(trident).+rv[:\s]([\w\.]+).+like\sgecko/i], [["name", "IE"], "version"], [/(edge)\/((\d+)?[\w\.]+)/i], ["name", "version"], [/(yabrowser)\/([\w\.]+)/i], [["name",
            "Yandex"], "version"], [/(comodo_dragon)\/([\w\.]+)/i], [["name", /_/g, " "], "version"], [/(chrome|omniweb|arora|[tizenoka]{5}\s?browser)\/v?([\w\.]+)/i, /(uc\s?browser|qqbrowser)[\/\s]?([\w\.]+)/i], ["name", "version"], [/(dolfin)\/([\w\.]+)/i], [["name", "Dolphin"], "version"], [/((?:android.+)crmo|crios)\/([\w\.]+)/i], [["name", "Chrome"], "version"], [/XiaoMi\/MiuiBrowser\/([\w\.]+)/i], ["version", ["name", "MIUI Browser"]], [/android.+version\/([\w\.]+)\s+(?:mobile\s?safari|safari)/i], ["version", ["name", "Android Browser"]],
            [/FBAV\/([\w\.]+);/i], ["version", ["name", "Facebook"]], [/version\/([\w\.]+).+?mobile\/\w+\s(safari)/i], ["version", ["name", "Mobile Safari"]], [/version\/([\w\.]+).+?(mobile\s?safari|safari)/i], ["version", "name"], [/webkit.+?(mobile\s?safari|safari)(\/[\w\.]+)/i], ["name", ["version", b, { "1.0": "/8", "1.2": "/1", "1.3": "/3", "2.0": "/412", "2.0.2": "/416", "2.0.3": "/417", "2.0.4": "/419", "?": "/" }]], [/(konqueror)\/([\w\.]+)/i, /(webkit|khtml)\/([\w\.]+)/i], ["name", "version"], [/(navigator|netscape)\/([\w\.-]+)/i], [["name",
            "Netscape"], "version"], [/(swiftfox)/i, /(icedragon|iceweasel|camino|chimera|fennec|maemo\sbrowser|minimo|conkeror)[\/\s]?([\w\.\+]+)/i, /(firefox|seamonkey|k-meleon|icecat|iceape|firebird|phoenix)\/([\w\.-]+)/i, /(mozilla)\/([\w\.]+).+rv\:.+gecko\/\d+/i, /(polaris|lynx|dillo|icab|doris|amaya|w3m|netsurf)[\/\s]?([\w\.]+)/i, /(links)\s\(([\w\.]+)/i, /(gobrowser)\/?([\w\.]+)*/i, /(ice\s?browser)\/v?([\w\._]+)/i, /(mosaic)[\/\s]([\w\.]+)/i], ["name", "version"]], c = [[/windows.+\sedge\/([\w\.]+)/i], ["version", ["name",
            "EdgeHTML"]], [/(presto)\/([\w\.]+)/i, /(webkit|trident|netfront|netsurf|amaya|lynx|w3m)\/([\w\.]+)/i, /(khtml|tasman|links)[\/\s]\(?([\w\.]+)/i, /(icab)[\/\s]([23]\.[\d\.]+)/i], ["name", "version"], [/rv\:([\w\.]+).*(gecko)/i], ["version", "name"]], e = [[/microsoft\s(windows)\s(vista|xp)/i], ["name", "version"], [/(windows)\snt\s6\.2;\s(arm)/i, /(windows\sphone(?:\sos)*|windows\smobile|windows)[\s\/]?([ntce\d\.\s]+\w)/i], ["name", ["version", b, f]], [/(win(?=3|9|n)|win\s9x\s)([nt\d\.]+)/i], [["name", "Windows"], ["version",
            b, f]], [/\((bb)(10);/i], [["name", "BlackBerry"], "version"], [/(blackberry)\w*\/?([\w\.]+)*/i, /(tizen)[\/\s]([\w\.]+)/i, /(android|webos|palm\os|qnx|bada|rim\stablet\sos|meego|contiki)[\/\s-]?([\w\.]+)*/i, /linux;.+(sailfish);/i], ["name", "version"], [/(symbian\s?os|symbos|s60(?=;))[\/\s-]?([\w\.]+)*/i], [["name", "Symbian"], "version"], [/\((series40);/i], ["name"], [/mozilla.+\(mobile;.+gecko.+firefox/i], [["name", "Firefox OS"], "version"], [/(nintendo|playstation)\s([wids3portablevu]+)/i, /(mint)[\/\s\(]?(\w+)*/i,
            /(mageia|vectorlinux)[;\s]/i, /(joli|[kxln]?ubuntu|debian|[open]*suse|gentoo|arch|slackware|fedora|mandriva|centos|pclinuxos|redhat|zenwalk|linpus)[\/\s-]?([\w\.-]+)*/i, /(hurd|linux)\s?([\w\.]+)*/i, /(gnu)\s?([\w\.]+)*/i], ["name", "version"], [/(cros)\s[\w]+\s([\w\.]+\w)/i], [["name", "Chromium OS"], "version"], [/(sunos)\s?([\w\.]+\d)*/i], [["name", "Solaris"], "version"], [/\s([frentopc-]{0,4}bsd|dragonfly)\s?([\w\.]+)*/i], ["name", "version"], [/(ip[honead]+)(?:.*os\s*([\w]+)*\slike\smac|;\sopera)/i], [["name",
            "iOS"], ["version", /_/g, "."]], [/(mac\sos\sx)\s?([\w\s\.]+\w)*/i, /(macintosh|mac(?=_powerpc)\s)/i], [["name", "Mac OS"], ["version", /_/g, "."]], [/((?:open)?solaris)[\/\s-]?([\w\.]+)*/i, /(haiku)\s(\w+)/i, /(aix)\s((\d)(?=\.|\)|\s)[\w\.]*)*/i, /(plan\s9|minix|beos|os\/2|amigaos|morphos|risc\sos|openvms)/i, /(unix)\s?([\w\.]+)*/i], ["name", "version"]]; return function (a) {
                var d = a || (window && window.navigator && window.navigator.userAgent ? window.navigator.userAgent : ""); this.getBrowser = function () {
                    return g.apply(this,
                    l)
                }; this.getEngine = function () { return g.apply(this, c) }; this.getOS = function () { return g.apply(this, e) }; this.getResult = function () { return { ua: this.getUA(), browser: this.getBrowser(), engine: this.getEngine(), os: this.getOS() } }; this.getUA = function () { return d }; this.setUA = function (a) { d = a; return this }; this.setUA(d)
            }
        }(), d = function () {
            var d = {
                define_property: !1, create_canvas: function () { var a = document.createElement("canvas"); return !(!a.getContext || !a.getContext("2d")) }(), return_response_type: function (a) {
                    try {
                        if (-1 !==
                        b.inArray(a, ["", "text", "document"])) return !0; if (window.XMLHttpRequest) { var d = new XMLHttpRequest; d.open("get", "/"); if ("responseType" in d) return d.responseType = a, d.responseType !== a ? !1 : !0 }
                    } catch (g) { } return !1
                }, use_data_uri: function () { var a = new Image; a.onload = function () { d.use_data_uri = 1 === a.width && 1 === a.height }; setTimeout(function () { a.src = "data:image/gif;base64,R0lGODlhAQABAIAAAP8AAAAAACH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==" }, 1); return !1 }(), use_data_uri_over32kb: function () {
                    return d.use_data_uri && ("IE" !==
                    a.browser || 9 <= a.version)
                }, use_data_uri_of: function (a) { return d.use_data_uri && 33E3 > a || d.use_data_uri_over32kb() }, use_fileinput: function () { if (navigator.userAgent.match(/(Android (1.0|1.1|1.5|1.6|2.0|2.1))|(Windows Phone (OS 7|8.0))|(XBLWP)|(ZuneWP)|(w(eb)?OSBrowser)|(webOS)|(Kindle\/(1.0|2.0|2.5|3.0))/)) return !1; var a = document.createElement("input"); a.setAttribute("type", "file"); return !a.disabled }
            }; return function (a) {
                var g = [].slice.call(arguments); g.shift(); return "function" === b.typeOf(d[a]) ? d[a].apply(this,
                g) : !!d[a]
            }
        }(), g = (new f).getResult(), a = {
            can: d, uaParser: f, browser: g.browser.name, version: g.browser.version, os: g.os.name, osVersion: g.os.version, verComp: function (a, d, g) {
                var b = 0, f = 0, l = 0, c = { dev: -6, alpha: -5, a: -5, beta: -4, b: -4, RC: -3, rc: -3, "#": -2, p: 1, pl: 1 }, b = function (a) { a = ("" + a).replace(/[_\-+]/g, "."); a = a.replace(/([^.\d]+)/g, ".$1.").replace(/\.{2,}/g, "."); return a.length ? a.split(".") : [-8] }, e = function (a) { return a ? isNaN(a) ? c[a] || -7 : parseInt(a, 10) : 0 }; a = b(a); d = b(d); f = Math.max(a.length, d.length); for (b = 0; b < f; b++) if (a[b] !=
                d[b]) if (a[b] = e(a[b]), d[b] = e(d[b]), a[b] < d[b]) { l = -1; break } else if (a[b] > d[b]) { l = 1; break } if (!g) return l; switch (g) { case ">": case "gt": return 0 < l; case ">=": case "ge": return 0 <= l; case "<=": case "le": return 0 >= l; case "==": case "=": case "eq": return 0 === l; case "<>": case "!=": case "ne": return 0 !== l; case "": case "<": case "lt": return 0 > l; default: return null }
            }, swf_url: "../flash/Moxie.swf", xap_url: "../silverlight/Moxie.xap", global_event_dispatcher: "moxie.core.EventTarget.instance.dispatchEvent"
        }; a.OS = a.os; MXI_DEBUG &&
        (a.debug = { runtime: !0, events: !1 }, a.log = function () { var a = arguments[0]; "string" === b.typeOf(a) && (a = b.sprintf.apply(this, arguments)); if (window && window.console && window.console.log) window.console.log(a); else if (document) { var d = document.getElementById("moxie-console"); d || (d = document.createElement("pre"), d.id = "moxie-console", document.body.appendChild(d)); b.inArray(b.typeOf(a), ["object", "array"]); d.appendChild(document.createTextNode(a + "\n")) } }); return a
    }); e("moxie/core/I18n", ["moxie/core/utils/Basic"], function (b) {
        var f =
        {}; return { addI18n: function (d) { return b.extend(f, d) }, translate: function (d) { return f[d] || d }, _: function (d) { return this.translate(d) }, sprintf: function (d) { var g = [].slice.call(arguments, 1); return d.replace(/%[a-z]/g, function () { var a = g.shift(); return "undefined" !== b.typeOf(a) ? a : "" }) } }
    }); e("moxie/core/utils/Mime", ["moxie/core/utils/Basic", "moxie/core/I18n"], function (b, f) {
        var d = {
            mimes: {}, extensions: {}, addMimeType: function (d) {
                d = d.split(/,/); var a, b, f; for (a = 0; a < d.length; a += 2) {
                    f = d[a + 1].split(/ /); for (b = 0; b < f.length; b++) this.mimes[f[b]] =
                    d[a]; this.extensions[d[a]] = f
                }
            }, extList2mimes: function (d, a) { var f, c, w, u, e = []; for (c = 0; c < d.length; c++) for (f = d[c].extensions.split(/\s*,\s*/), w = 0; w < f.length; w++) { if ("*" === f[w]) return []; (u = this.mimes[f[w]]) && -1 === b.inArray(u, e) && e.push(u); if (a && /^\w+$/.test(f[w])) e.push("." + f[w]); else if (!u) return [] } return e }, mimes2exts: function (d) {
                var a = this, f = []; b.each(d, function (d) {
                    if ("*" === d) return f = [], !1; var g = d.match(/^(\w+)\/(\*|\w+)$/); g && ("*" === g[2] ? b.each(a.extensions, function (d, b) {
                        (new RegExp("^" + g[1] + "/")).test(b) &&
                        [].push.apply(f, a.extensions[b])
                    }) : a.extensions[d] && [].push.apply(f, a.extensions[d]))
                }); return f
            }, mimes2extList: function (d) { var a = [], c = []; "string" === b.typeOf(d) && (d = b.trim(d).split(/\s*,\s*/)); c = this.mimes2exts(d); a.push({ title: f.translate("Files"), extensions: c.length ? c.join(",") : "*" }); a.mimes = d; return a }, getFileExtension: function (d) { return (d = d && d.match(/\.([^.]+)$/)) ? d[1].toLowerCase() : "" }, getFileMime: function (d) { return this.mimes[this.getFileExtension(d)] || "" }
        }; d.addMimeType("application/msword,doc dot,application/pdf,pdf,application/pgp-signature,pgp,application/postscript,ps ai eps,application/rtf,rtf,application/vnd.ms-excel,xls xlb,application/vnd.ms-powerpoint,ppt pps pot,application/zip,zip,application/x-shockwave-flash,swf swfl,application/vnd.openxmlformats-officedocument.wordprocessingml.document,docx,application/vnd.openxmlformats-officedocument.wordprocessingml.template,dotx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,xlsx,application/vnd.openxmlformats-officedocument.presentationml.presentation,pptx,application/vnd.openxmlformats-officedocument.presentationml.template,potx,application/vnd.openxmlformats-officedocument.presentationml.slideshow,ppsx,application/x-javascript,js,application/json,json,audio/mpeg,mp3 mpga mpega mp2,audio/x-wav,wav,audio/x-m4a,m4a,audio/ogg,oga ogg,audio/aiff,aiff aif,audio/flac,flac,audio/aac,aac,audio/ac3,ac3,audio/x-ms-wma,wma,image/bmp,bmp,image/gif,gif,image/jpeg,jpg jpeg jpe,image/photoshop,psd,image/png,png,image/svg+xml,svg svgz,image/tiff,tiff tif,text/plain,asc txt text diff log,text/html,htm html xhtml,text/css,css,text/csv,csv,text/rtf,rtf,video/mpeg,mpeg mpg mpe m2v,video/quicktime,qt mov,video/mp4,mp4,video/x-m4v,m4v,video/x-flv,flv,video/x-ms-wmv,wmv,video/avi,avi,video/webm,webm,video/3gpp,3gpp 3gp,video/3gpp2,3g2,video/vnd.rn-realvideo,rv,video/ogg,ogv,video/x-matroska,mkv,application/vnd.oasis.opendocument.formula-template,otf,application/octet-stream,exe");
        return d
    }); e("moxie/core/utils/Dom", ["moxie/core/utils/Env"], function (b) {
        var f = function (d, b) { return d.className ? (new RegExp("(^|\\s+)" + b + "(\\s+|$)")).test(d.className) : !1 }; return {
            get: function (d) { return "string" !== typeof d ? d : document.getElementById(d) }, hasClass: f, addClass: function (d, b) { f(d, b) || (d.className = d.className ? d.className.replace(/\s+$/, "") + " " + b : b) }, removeClass: function (d, b) {
                d.className && (d.className = d.className.replace(new RegExp("(^|\\s+)" + b + "(\\s+|$)"), function (a, d, b) {
                    return " " === d && " " ===
                    b ? " " : ""
                }))
            }, getStyle: function (d, b) { if (d.currentStyle) return d.currentStyle[b]; if (window.getComputedStyle) return window.getComputedStyle(d, null)[b] }, getPos: function (d, f) {
                function a(a) { var d, b = 0; d = 0; a && (d = a.getBoundingClientRect(), a = "CSS1Compat" === u.compatMode ? u.documentElement : u.body, b = d.left + a.scrollLeft, d = d.top + a.scrollTop); return { x: b, y: d } } var c = 0, m = 0, w, u = document; f = f || u.body; if (d && d.getBoundingClientRect && "IE" === b.browser && (!u.documentMode || 8 > u.documentMode)) return c = a(d), m = a(f), {
                    x: c.x - m.x, y: c.y -
                    m.y
                }; for (w = d; w && w != f && w.nodeType;) c += w.offsetLeft || 0, m += w.offsetTop || 0, w = w.offsetParent; for (w = d.parentNode; w && w != f && w.nodeType;) c -= w.scrollLeft || 0, m -= w.scrollTop || 0, w = w.parentNode; return { x: c, y: m }
            }, getSize: function (d) { return { w: d.offsetWidth || d.clientWidth, h: d.offsetHeight || d.clientHeight } }
        }
    }); e("moxie/core/Exceptions", ["moxie/core/utils/Basic"], function (b) {
        function f(d, b) { for (var a in d) if (d[a] === b) return a; return null } return {
            RuntimeError: function () {
                function d(a) {
                    this.code = a; this.name = f(g, a); this.message =
                    this.name + ": RuntimeError " + this.code
                } var g = { NOT_INIT_ERR: 1, NOT_SUPPORTED_ERR: 9, JS_ERR: 4 }; b.extend(d, g); d.prototype = Error.prototype; return d
            }(), OperationNotAllowedException: function () { function d(d) { this.code = d; this.name = "OperationNotAllowedException" } b.extend(d, { NOT_ALLOWED_ERR: 1 }); d.prototype = Error.prototype; return d }(), ImageError: function () {
                function d(a) { this.code = a; this.name = f(g, a); this.message = this.name + ": ImageError " + this.code } var g = { WRONG_FORMAT: 1, MAX_RESOLUTION_ERR: 2, INVALID_META_ERR: 3 }; b.extend(d,
                g); d.prototype = Error.prototype; return d
            }(), FileException: function () { function d(a) { this.code = a; this.name = f(g, a); this.message = this.name + ": FileException " + this.code } var g = { NOT_FOUND_ERR: 1, SECURITY_ERR: 2, ABORT_ERR: 3, NOT_READABLE_ERR: 4, ENCODING_ERR: 5, NO_MODIFICATION_ALLOWED_ERR: 6, INVALID_STATE_ERR: 7, SYNTAX_ERR: 8 }; b.extend(d, g); d.prototype = Error.prototype; return d }(), DOMException: function () {
                function d(a) { this.code = a; this.name = f(g, a); this.message = this.name + ": DOMException " + this.code } var g = {
                    INDEX_SIZE_ERR: 1,
                    DOMSTRING_SIZE_ERR: 2, HIERARCHY_REQUEST_ERR: 3, WRONG_DOCUMENT_ERR: 4, INVALID_CHARACTER_ERR: 5, NO_DATA_ALLOWED_ERR: 6, NO_MODIFICATION_ALLOWED_ERR: 7, NOT_FOUND_ERR: 8, NOT_SUPPORTED_ERR: 9, INUSE_ATTRIBUTE_ERR: 10, INVALID_STATE_ERR: 11, SYNTAX_ERR: 12, INVALID_MODIFICATION_ERR: 13, NAMESPACE_ERR: 14, INVALID_ACCESS_ERR: 15, VALIDATION_ERR: 16, TYPE_MISMATCH_ERR: 17, SECURITY_ERR: 18, NETWORK_ERR: 19, ABORT_ERR: 20, URL_MISMATCH_ERR: 21, QUOTA_EXCEEDED_ERR: 22, TIMEOUT_ERR: 23, INVALID_NODE_TYPE_ERR: 24, DATA_CLONE_ERR: 25
                }; b.extend(d, g);
                d.prototype = Error.prototype; return d
            }(), EventException: function () { function d(d) { this.code = d; this.name = "EventException" } b.extend(d, { UNSPECIFIED_EVENT_TYPE_ERR: 0 }); d.prototype = Error.prototype; return d }()
        }
    }); e("moxie/core/EventTarget", ["moxie/core/utils/Env", "moxie/core/Exceptions", "moxie/core/utils/Basic"], function (b, f, d) {
        function g() {
            var a = {}; d.extend(this, {
                uid: null, init: function () { this.uid || (this.uid = d.guid("uid_")) }, addEventListener: function (b, f, g, c) {
                    var l = this, e; this.hasOwnProperty("uid") || (this.uid =
                    d.guid("uid_")); b = d.trim(b); /\s/.test(b) ? d.each(b.split(/\s+/), function (a) { l.addEventListener(a, f, g, c) }) : (b = b.toLowerCase(), g = parseInt(g, 10) || 0, e = a[this.uid] && a[this.uid][b] || [], e.push({ fn: f, priority: g, scope: c || this }), a[this.uid] || (a[this.uid] = {}), a[this.uid][b] = e)
                }, hasEventListener: function (d) { return (d = d ? a[this.uid] && a[this.uid][d] : a[this.uid]) ? d : !1 }, removeEventListener: function (b, f) {
                    b = b.toLowerCase(); var g = a[this.uid] && a[this.uid][b], c; if (g) {
                        if (f) for (c = g.length - 1; 0 <= c; c--) {
                            if (g[c].fn === f) {
                                g.splice(c,
                                1); break
                            }
                        } else g = []; g.length || (delete a[this.uid][b], d.isEmptyObj(a[this.uid]) && delete a[this.uid])
                    }
                }, removeAllEventListeners: function () { a[this.uid] && delete a[this.uid] }, dispatchEvent: function (g) {
                    var c, e, u, k = {}, h = !0; if ("string" !== d.typeOf(g)) if (e = g, "string" === d.typeOf(e.type)) g = e.type, void 0 !== e.total && void 0 !== e.loaded && (k.total = e.total, k.loaded = e.loaded), k.async = e.async || !1; else throw new f.EventException(f.EventException.UNSPECIFIED_EVENT_TYPE_ERR); -1 !== g.indexOf("::") ? function (a) { c = a[0]; g = a[1] }(g.split("::")) :
                    c = this.uid; g = g.toLowerCase(); if (e = a[c] && a[c][g]) { e.sort(function (a, d) { return d.priority - a.priority }); u = [].slice.call(arguments); u.shift(); k.type = g; u.unshift(k); MXI_DEBUG && b.debug.events && b.log("Event '%s' fired on %u", k.type, c); var r = []; d.each(e, function (a) { u[0].target = a.scope; k.async ? r.push(function (d) { setTimeout(function () { d(!1 === a.fn.apply(a.scope, u)) }, 1) }) : r.push(function (d) { d(!1 === a.fn.apply(a.scope, u)) }) }); r.length && d.inSeries(r, function (a) { h = !a }) } return h
                }, bind: function () {
                    this.addEventListener.apply(this,
                    arguments)
                }, unbind: function () { this.removeEventListener.apply(this, arguments) }, unbindAll: function () { this.removeAllEventListeners.apply(this, arguments) }, trigger: function () { return this.dispatchEvent.apply(this, arguments) }, handleEventProps: function (a) { var b = this; this.bind(a.join(" "), function (a) { var b = "on" + a.type.toLowerCase(); "function" === d.typeOf(this[b]) && this[b].apply(this, arguments) }); d.each(a, function (a) { a = "on" + a.toLowerCase(a); "undefined" === d.typeOf(b[a]) && (b[a] = null) }) }
            })
        } g.instance = new g; return g
    });
    e("moxie/runtime/Runtime", ["moxie/core/utils/Env", "moxie/core/utils/Basic", "moxie/core/utils/Dom", "moxie/core/EventTarget"], function (b, f, d, g) {
        function a(g, c, e, k, q) {
            var h = this, n, x = f.guid(c + "_"); q = q || "browser"; g = g || {}; m[x] = this; e = f.extend({
                access_binary: !1, access_image_binary: !1, display_media: !1, do_cors: !1, drag_and_drop: !1, filter_by_extension: !0, resize_image: !1, report_upload_progress: !1, return_response_headers: !1, return_response_type: !1, return_status_code: !0, send_custom_headers: !1, select_file: !1, select_folder: !1,
                select_multiple: !0, send_binary_string: !1, send_browser_cookies: !0, send_multipart: !0, slice_blob: !1, stream_upload: !1, summon_file_dialog: !1, upload_filesize: !0, use_http_method: !0
            }, e); g.preferred_caps && (q = a.getMode(k, g.preferred_caps, q)); MXI_DEBUG && b.debug.runtime && b.log("\tdefault mode: %s", q); n = function () {
                var a = {}; return {
                    exec: function (d, b, f, g) { if (n[b] && (a[d] || (a[d] = { context: this, instance: new n[b] }), a[d].instance[f])) return a[d].instance[f].apply(this, g) }, removeInstance: function (d) { delete a[d] }, removeAllInstances: function () {
                        var d =
                        this; f.each(a, function (a, b) { "function" === f.typeOf(a.instance.destroy) && a.instance.destroy.call(a.context); d.removeInstance(b) })
                    }
                }
            }(); f.extend(this, {
                initialized: !1, uid: x, type: c, mode: a.getMode(k, g.required_caps, q), shimid: x + "_container", clients: 0, options: g, can: function (d, b, g) { g = g || e; "string" === f.typeOf(d) && "undefined" === f.typeOf(b) && (d = a.parseCaps(d)); if ("object" === f.typeOf(d)) { for (var c in d) if (!this.can(c, d[c], g)) return !1; return !0 } return "function" === f.typeOf(g[d]) ? g[d].call(this, b) : b === g[d] }, getShimContainer: function () {
                    var a,
                    b = d.get(this.shimid); b || (a = this.options.container ? d.get(this.options.container) : document.body, b = document.createElement("div"), b.id = this.shimid, b.className = "moxie-shim moxie-shim-" + this.type, f.extend(b.style, { position: "absolute", top: "0px", left: "0px", width: "1px", height: "1px", overflow: "hidden" }), a.appendChild(b)); return b
                }, getShim: function () { return n }, shimExec: function (a, d) { var b = [].slice.call(arguments, 2); return h.getShim().exec.call(this, this.uid, a, d, b) }, exec: function (a, d) {
                    var b = [].slice.call(arguments,
                    2); return h[a] && h[a][d] ? h[a][d].apply(this, b) : h.shimExec.apply(this, arguments)
                }, destroy: function () { if (h) { var a = d.get(this.shimid); a && a.parentNode.removeChild(a); n && n.removeAllInstances(); this.unbindAll(); delete m[this.uid]; x = h = n = this.uid = null } }
            }); this.mode && g.required_caps && !this.can(g.required_caps) && (this.mode = !1)
        } var c = {}, m = {}; a.order = "html5,flash,silverlight,html4"; a.getRuntime = function (a) { return m[a] ? m[a] : !1 }; a.addConstructor = function (a, d) { d.prototype = g.instance; c[a] = d }; a.getConstructor = function (a) {
            return c[a] ||
            null
        }; a.getInfo = function (d) { var b = a.getRuntime(d); return b ? { uid: b.uid, type: b.type, mode: b.mode, can: function () { return b.can.apply(b, arguments) } } : null }; a.parseCaps = function (a) { var d = {}; if ("string" !== f.typeOf(a)) return a || {}; f.each(a.split(","), function (a) { d[a] = !0 }); return d }; a.can = function (d, b) { var g; g = a.getConstructor(d); var f; return g ? (g = new g({ required_caps: b }), f = g.mode, g.destroy(), !!f) : !1 }; a.thatCan = function (d, b) { var g = (b || a.order).split(/\s*,\s*/), f; for (f in g) if (a.can(g[f], d)) return g[f]; return null };
        a.getMode = function (a, d, g) { var c = null; "undefined" === f.typeOf(g) && (g = "browser"); if (d && !f.isEmptyObj(a)) { f.each(d, function (d, g) { if (a.hasOwnProperty(g)) { var m = a[g](d); "string" === typeof m && (m = [m]); if (!c) c = m; else if (!(c = f.arrayIntersect(c, m))) return MXI_DEBUG && b.debug.runtime && b.log("\t\t%c: %v (conflicting mode requested: %s)", g, d, m), c = !1 } MXI_DEBUG && b.debug.runtime && b.log("\t\t%c: %v (compatible modes: %s)", g, d, c) }); if (c) return -1 !== f.inArray(g, c) ? g : c[0]; if (!1 === c) return !1 } return g }; a.capTrue = function () { return !0 };
        a.capFalse = function () { return !1 }; a.capTest = function (a) { return function () { return !!a } }; return a
    }); e("moxie/runtime/RuntimeClient", ["moxie/core/utils/Env", "moxie/core/Exceptions", "moxie/core/utils/Basic", "moxie/runtime/Runtime"], function (b, f, d, g) {
        return function () {
            var a; d.extend(this, {
                connectRuntime: function (c) {
                    function m(d) {
                        var k, u; d.length ? (k = d.shift().toLowerCase(), (u = g.getConstructor(k)) ? (MXI_DEBUG && b.debug.runtime && (b.log("Trying runtime: %s", k), b.log(c)), a = new u(c), a.bind("Init", function () {
                            a.initialized =
                            !0; MXI_DEBUG && b.debug.runtime && b.log("Runtime '%s' initialized", a.type); setTimeout(function () { a.clients++; e.trigger("RuntimeInit", a) }, 1)
                        }), a.bind("Error", function () { MXI_DEBUG && b.debug.runtime && b.log("Runtime '%s' failed to initialize", a.type); a.destroy(); m(d) }), MXI_DEBUG && b.debug.runtime && b.log("\tselected mode: %s", a.mode), a.mode ? a.init() : a.trigger("Error")) : m(d)) : (e.trigger("RuntimeError", new f.RuntimeError(f.RuntimeError.NOT_INIT_ERR)), a = null)
                    } var e = this, k; "string" === d.typeOf(c) ? k = c : "string" ===
                    d.typeOf(c.ruid) && (k = c.ruid); if (k) { if (a = g.getRuntime(k)) return a.clients++, a; throw new f.RuntimeError(f.RuntimeError.NOT_INIT_ERR); } m((c.runtime_order || g.order).split(/\s*,\s*/))
                }, disconnectRuntime: function () { a && 0 >= --a.clients && a.destroy(); a = null }, getRuntime: function () { return a && a.uid ? a : a = null }, exec: function () { return a ? a.exec.apply(this, arguments) : null }
            })
        }
    }); e("moxie/file/FileInput", "moxie/core/utils/Basic moxie/core/utils/Env moxie/core/utils/Mime moxie/core/utils/Dom moxie/core/Exceptions moxie/core/EventTarget moxie/core/I18n moxie/runtime/Runtime moxie/runtime/RuntimeClient".split(" "),
    function (b, f, d, g, a, c, m, e, k) {
        function h(c) {
            MXI_DEBUG && f.log("Instantiating FileInput..."); var q = this, t; -1 !== b.inArray(b.typeOf(c), ["string", "node"]) && (c = { browse_button: c }); t = g.get(c.browse_button); if (!t) throw new a.DOMException(a.DOMException.NOT_FOUND_ERR); t = { accept: [{ title: m.translate("All Files"), extensions: "*" }], name: "file", multiple: !1, required_caps: !1, container: t.parentNode || document.body }; c = b.extend({}, t, c); "string" === typeof c.required_caps && (c.required_caps = e.parseCaps(c.required_caps)); "string" ===
            typeof c.accept && (c.accept = d.mimes2extList(c.accept)); t = g.get(c.container); t || (t = document.body); "static" === g.getStyle(t, "position") && (t.style.position = "relative"); t = t = null; k.call(q); b.extend(q, {
                uid: b.guid("uid_"), ruid: null, shimid: null, files: null, init: function () {
                    q.bind("RuntimeInit", function (a, d) {
                        q.ruid = d.uid; q.shimid = d.shimid; q.bind("Ready", function () { q.trigger("Refresh") }, 999); q.bind("Refresh", function () {
                            var a, f, m; f = g.get(c.browse_button); m = g.get(d.shimid); f && (a = g.getPos(f, g.get(c.container)), f =
                            g.getSize(f), m && b.extend(m.style, { top: a.y + "px", left: a.x + "px", width: f.w + "px", height: f.h + "px" }))
                        }); d.exec.call(q, "FileInput", "init", c)
                    }); q.connectRuntime(b.extend({}, c, { required_caps: { select_file: !0 } }))
                }, disable: function (a) { var d = this.getRuntime(); d && d.exec.call(this, "FileInput", "disable", "undefined" === b.typeOf(a) ? !0 : a) }, refresh: function () { q.trigger("Refresh") }, destroy: function () {
                    var a = this.getRuntime(); a && (a.exec.call(this, "FileInput", "destroy"), this.disconnectRuntime()); "array" === b.typeOf(this.files) &&
                    b.each(this.files, function (a) { a.destroy() }); this.files = null; this.unbindAll()
                }
            }); this.handleEventProps(p)
        } var p = "ready change cancel mouseenter mouseleave mousedown mouseup".split(" "); h.prototype = c.instance; return h
    }); e("moxie/core/utils/Encode", [], function () {
        var b = function (d) { return unescape(encodeURIComponent(d)) }, f = function (d) { return decodeURIComponent(escape(d)) }; return {
            utf8_encode: b, utf8_decode: f, atob: function (d, b) {
                if ("function" === typeof window.atob) return b ? f(window.atob(d)) : window.atob(d);
                var a, c, l, e, k, h = 0, p = 0; e = ""; var r = []; if (!d) return d; d += ""; do a = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(d.charAt(h++)), c = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(d.charAt(h++)), e = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(d.charAt(h++)), k = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".indexOf(d.charAt(h++)), l = a << 18 | c << 12 | e << 6 | k, a = l >> 16 & 255, c = l >> 8 & 255, l &= 255, 64 == e ? r[p++] = String.fromCharCode(a) :
                64 == k ? r[p++] = String.fromCharCode(a, c) : r[p++] = String.fromCharCode(a, c, l); while (h < d.length); e = r.join(""); return b ? f(e) : e
            }, btoa: function (d, g) {
                g && (d = b(d)); if ("function" === typeof window.btoa) return window.btoa(d); var a, f, c, e, k = 0, h = 0, p = "", p = []; if (!d) return d; do a = d.charCodeAt(k++), f = d.charCodeAt(k++), c = d.charCodeAt(k++), e = a << 16 | f << 8 | c, a = e >> 18 & 63, f = e >> 12 & 63, c = e >> 6 & 63, e &= 63, p[h++] = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(a) + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(f) +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(c) + "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(e); while (k < d.length); p = p.join(""); k = d.length % 3; return (k ? p.slice(0, k - 3) : p) + "===".slice(k || 3)
            }
        }
    }); e("moxie/file/Blob", ["moxie/core/utils/Basic", "moxie/core/utils/Encode", "moxie/runtime/RuntimeClient"], function (b, f, d) {
        function g(c, m) {
            function e(d, f, c) {
                var m = a[this.uid]; if ("string" !== b.typeOf(m) || !m.length) return null; f = new g(null, { type: c, size: f - d });
                f.detach(m.substr(d, f.size)); return f
            } d.call(this); c && this.connectRuntime(c); m ? "string" === b.typeOf(m) && (m = { data: m }) : m = {}; b.extend(this, {
                uid: m.uid || b.guid("uid_"), ruid: c, size: m.size || 0, type: m.type || "", slice: function (a, d, b) { return this.isDetached() ? e.apply(this, arguments) : this.getRuntime().exec.call(this, "Blob", "slice", this.getSource(), a, d, b) }, getSource: function () { return a[this.uid] ? a[this.uid] : null }, detach: function (d) {
                    this.ruid && (this.getRuntime().exec.call(this, "Blob", "destroy"), this.disconnectRuntime(),
                    this.ruid = null); d = d || ""; if ("data:" == d.substr(0, 5)) { var b = d.indexOf(";base64,"); this.type = d.substring(5, b); d = f.atob(d.substring(b + 8)) } this.size = d.length; a[this.uid] = d
                }, isDetached: function () { return !this.ruid && "string" === b.typeOf(a[this.uid]) }, destroy: function () { this.detach(); delete a[this.uid] }
            }); m.data ? this.detach(m.data) : a[this.uid] = m
        } var a = {}; return g
    }); e("moxie/file/File", ["moxie/core/utils/Basic", "moxie/core/utils/Mime", "moxie/file/Blob"], function (b, f, d) {
        function g(a, g) {
            g || (g = {}); d.apply(this,
            arguments); this.type || (this.type = f.getFileMime(g.name)); var c; g.name ? (c = g.name.replace(/\\/g, "/"), c = c.substr(c.lastIndexOf("/") + 1)) : this.type && (c = this.type.split("/")[0], c = b.guid(("" !== c ? c : "file") + "_"), f.extensions[this.type] && (c += "." + f.extensions[this.type][0])); b.extend(this, { name: c || b.guid("file_"), relativePath: "", lastModifiedDate: g.lastModifiedDate || (new Date).toLocaleString() })
        } g.prototype = d.prototype; return g
    }); e("moxie/file/FileDrop", "moxie/core/I18n moxie/core/utils/Dom moxie/core/Exceptions moxie/core/utils/Basic moxie/core/utils/Env moxie/file/File moxie/runtime/RuntimeClient moxie/core/EventTarget moxie/core/utils/Mime".split(" "),
    function (b, f, d, g, a, c, m, e, k) {
        function h(a) {
            var d = this, c; "string" === typeof a && (a = { drop_zone: a }); c = { accept: [{ title: b.translate("All Files"), extensions: "*" }], required_caps: { drag_and_drop: !0 } }; a = "object" === typeof a ? g.extend({}, c, a) : c; a.container = f.get(a.drop_zone) || document.body; "static" === f.getStyle(a.container, "position") && (a.container.style.position = "relative"); "string" === typeof a.accept && (a.accept = k.mimes2extList(a.accept)); m.call(d); g.extend(d, {
                uid: g.guid("uid_"), ruid: null, files: null, init: function () {
                    d.bind("RuntimeInit",
                    function (b, g) { d.ruid = g.uid; g.exec.call(d, "FileDrop", "init", a); d.dispatchEvent("ready") }); d.connectRuntime(a)
                }, destroy: function () { var a = this.getRuntime(); a && (a.exec.call(this, "FileDrop", "destroy"), this.disconnectRuntime()); this.files = null; this.unbindAll() }
            }); this.handleEventProps(p)
        } var p = ["ready", "dragenter", "dragleave", "drop", "error"]; h.prototype = e.instance; return h
    }); e("moxie/file/FileReader", "moxie/core/utils/Basic moxie/core/utils/Encode moxie/core/Exceptions moxie/core/EventTarget moxie/file/Blob moxie/runtime/RuntimeClient".split(" "),
    function (b, f, d, g, a, c) {
        function m() {
            function g(b, c) {
                this.trigger("loadstart"); if (this.readyState === m.LOADING) this.trigger("error", new d.DOMException(d.DOMException.INVALID_STATE_ERR)), this.trigger("loadend"); else if (c instanceof a) if (this.result = null, this.readyState = m.LOADING, c.isDetached()) { var l = c.getSource(); switch (b) { case "readAsText": case "readAsBinaryString": this.result = l; break; case "readAsDataURL": this.result = "data:" + c.type + ";base64," + f.btoa(l) } this.readyState = m.DONE; this.trigger("load"); this.trigger("loadend") } else this.connectRuntime(c.ruid),
                this.exec("FileReader", "read", b, c); else this.trigger("error", new d.DOMException(d.DOMException.NOT_FOUND_ERR)), this.trigger("loadend")
            } c.call(this); b.extend(this, {
                uid: b.guid("uid_"), readyState: m.EMPTY, result: null, error: null, readAsBinaryString: function (a) { g.call(this, "readAsBinaryString", a) }, readAsDataURL: function (a) { g.call(this, "readAsDataURL", a) }, readAsText: function (a) { g.call(this, "readAsText", a) }, abort: function () {
                    this.result = null; -1 === b.inArray(this.readyState, [m.EMPTY, m.DONE]) && (this.readyState ===
                    m.LOADING && (this.readyState = m.DONE), this.exec("FileReader", "abort"), this.trigger("abort"), this.trigger("loadend"))
                }, destroy: function () { this.abort(); this.exec("FileReader", "destroy"); this.disconnectRuntime(); this.unbindAll() }
            }); this.handleEventProps(e); this.bind("Error", function (a, d) { this.readyState = m.DONE; this.error = d }, 999); this.bind("Load", function (a) { this.readyState = m.DONE }, 999)
        } var e = "loadstart progress load abort error loadend".split(" "); m.EMPTY = 0; m.LOADING = 1; m.DONE = 2; m.prototype = g.instance;
        return m
    }); e("moxie/core/utils/Url", [], function () {
        var b = function (f, d) {
            for (var g = "source scheme authority userInfo user pass host port relative path directory file query fragment".split(" "), a = g.length, c = { http: 80, https: 443 }, m = {}, e = /^(?:([^:\/?#]+):)?(?:\/\/()(?:(?:()(?:([^:@\/]*):?([^:@\/]*))?@)?([^:\/?#]*)(?::(\d*))?))?()(?:(()(?:(?:[^?#\/]*\/)*)()(?:[^?#]*))(?:\\?([^#]*))?(?:#(.*))?)/.exec(f || "") ; a--;) e[a] && (m[g[a]] = e[a]); m.scheme || (d && "string" !== typeof d || (d = b(d || document.location.href)), m.scheme =
            d.scheme, m.host = d.host, m.port = d.port, g = "", /^[^\/]/.test(m.path) && (g = d.path, g = /\/[^\/]*\.[^\/]*$/.test(g) ? g.replace(/\/[^\/]+$/, "/") : g.replace(/\/?$/, "/")), m.path = g + (m.path || "")); m.port || (m.port = c[m.scheme] || 80); m.port = parseInt(m.port, 10); m.path || (m.path = "/"); delete m.source; return m
        }; return {
            parseUrl: b, resolveUrl: function (f) { f = "object" === typeof f ? f : b(f); return f.scheme + "://" + f.host + (f.port !== { http: 80, https: 443 }[f.scheme] ? ":" + f.port : "") + f.path + (f.query ? f.query : "") }, hasSameOrigin: function (f) {
                function d(d) {
                    return [d.scheme,
                    d.host, d.port].join("/")
                } "string" === typeof f && (f = b(f)); return d(b()) === d(f)
            }
        }
    }); e("moxie/runtime/RuntimeTarget", ["moxie/core/utils/Basic", "moxie/runtime/RuntimeClient", "moxie/core/EventTarget"], function (b, f, d) { function g() { this.uid = b.guid("uid_"); f.call(this); this.destroy = function () { this.disconnectRuntime(); this.unbindAll() } } g.prototype = d.instance; return g }); e("moxie/file/FileReaderSync", ["moxie/core/utils/Basic", "moxie/runtime/RuntimeClient", "moxie/core/utils/Encode"], function (b, f, d) {
        return function () {
            function g(a,
            b) { if (b.isDetached()) { var g = b.getSource(); switch (a) { case "readAsBinaryString": return g; case "readAsDataURL": return "data:" + b.type + ";base64," + d.btoa(g); case "readAsText": for (var f = "", c = 0, l = g.length; c < l; c++) f += String.fromCharCode(g[c]); return f } } else return g = this.connectRuntime(b.ruid).exec.call(this, "FileReaderSync", "read", a, b), this.disconnectRuntime(), g } f.call(this); b.extend(this, {
                uid: b.guid("uid_"), readAsBinaryString: function (a) { return g.call(this, "readAsBinaryString", a) }, readAsDataURL: function (a) {
                    return g.call(this,
                    "readAsDataURL", a)
                }, readAsText: function (a) { return g.call(this, "readAsText", a) }
            })
        }
    }); e("moxie/xhr/FormData", ["moxie/core/Exceptions", "moxie/core/utils/Basic", "moxie/file/Blob"], function (b, f, d) {
        return function () {
            var b, a = []; f.extend(this, {
                append: function (c, l) {
                    var e = this, k = f.typeOf(l); l instanceof d ? b = { name: c, value: l } : "array" === k ? (c += "[]", f.each(l, function (a) { e.append(c, a) })) : "object" === k ? f.each(l, function (a, d) { e.append(c + "[" + d + "]", a) }) : "null" === k || "undefined" === k || "number" === k && isNaN(l) ? e.append(c, "false") :
                    a.push({ name: c, value: l.toString() })
                }, hasBlob: function () { return !!this.getBlob() }, getBlob: function () { return b && b.value || null }, getBlobName: function () { return b && b.name || null }, each: function (d) { f.each(a, function (a) { d(a.value, a.name) }); b && d(b.value, b.name) }, destroy: function () { b = null; a = [] }
            })
        }
    }); e("moxie/xhr/XMLHttpRequest", "moxie/core/utils/Basic moxie/core/Exceptions moxie/core/EventTarget moxie/core/utils/Encode moxie/core/utils/Url moxie/runtime/Runtime moxie/runtime/RuntimeTarget moxie/file/Blob moxie/file/FileReaderSync moxie/xhr/FormData moxie/core/utils/Env moxie/core/utils/Mime".split(" "),
    function (b, f, d, g, a, c, e, k, h, t, p, r) {
        function v() { this.uid = b.guid("uid_") } function n() {
            function d(a, b) { if (A.hasOwnProperty(a)) { if (1 === arguments.length) return p.can("define_property") ? A[a] : u[a]; p.can("define_property") ? A[a] = b : u[a] = b } } function h(a) {
                function f() { H && (H.destroy(), H = null); k.dispatchEvent("loadend"); k = null } function g(c) {
                    H.bind("LoadStart", function (a) { d("readyState", n.LOADING); k.dispatchEvent("readystatechange"); k.dispatchEvent(a); N && k.upload.dispatchEvent(a) }); H.bind("Progress", function (a) {
                        d("readyState") !==
                        n.LOADING && (d("readyState", n.LOADING), k.dispatchEvent("readystatechange")); k.dispatchEvent(a)
                    }); H.bind("UploadProgress", function (a) { N && k.upload.dispatchEvent({ type: "progress", lengthComputable: !1, total: a.total, loaded: a.loaded }) }); H.bind("Load", function (a) {
                        d("readyState", n.DONE); d("status", Number(c.exec.call(H, "XMLHttpRequest", "getStatus") || 0)); d("statusText", x[d("status")] || ""); d("response", c.exec.call(H, "XMLHttpRequest", "getResponse", d("responseType"))); ~b.inArray(d("responseType"), ["text", ""]) ? d("responseText",
                        d("response")) : "document" === d("responseType") && d("responseXML", d("response")); S = c.exec.call(H, "XMLHttpRequest", "getAllResponseHeaders"); k.dispatchEvent("readystatechange"); 0 < d("status") ? (N && k.upload.dispatchEvent(a), k.dispatchEvent(a)) : (T = !0, k.dispatchEvent("error")); f()
                    }); H.bind("Abort", function (a) { k.dispatchEvent(a); f() }); H.bind("Error", function (a) { T = !0; d("readyState", n.DONE); k.dispatchEvent("readystatechange"); D = !0; k.dispatchEvent(a); f() }); c.exec.call(H, "XMLHttpRequest", "send", {
                        url: J, method: L,
                        async: E, user: I, password: P, headers: y, mimeType: O, encoding: M, responseType: k.responseType, withCredentials: k.withCredentials, options: K
                    }, a)
                } var k = this; (new Date).getTime(); H = new e; "string" === typeof K.required_caps && (K.required_caps = c.parseCaps(K.required_caps)); K.required_caps = b.extend({}, K.required_caps, { return_response_type: k.responseType }); a instanceof t && (K.required_caps.send_multipart = !0); b.isEmptyObj(y) || (K.required_caps.send_custom_headers = !0); U || (K.required_caps.do_cors = !0); K.ruid ? g(H.connectRuntime(K)) :
                (H.bind("RuntimeInit", function (a, d) { g(d) }), H.bind("RuntimeError", function (a, d) { k.dispatchEvent("RuntimeError", d) }), H.connectRuntime(K))
            } var u = this, A = { timeout: 0, readyState: n.UNSENT, withCredentials: !1, status: 0, statusText: "", responseType: "", responseXML: null, responseText: null, response: null }, E = !0, J, L, y = {}, I, P, M = null, O = null, Q = !1, F = !1, N = !1, D = !1, T = !1, U = !1, K = {}, H, S = "", R; b.extend(this, A, {
                uid: b.guid("uid_"), upload: new v, open: function (c, e, k, m, h) {
                    if (!c || !e) throw new f.DOMException(f.DOMException.SYNTAX_ERR); if (/[\u0100-\uffff]/.test(c) ||
                    g.utf8_encode(c) !== c) throw new f.DOMException(f.DOMException.SYNTAX_ERR); ~b.inArray(c.toUpperCase(), "CONNECT DELETE GET HEAD OPTIONS POST PUT TRACE TRACK".split(" ")) && (L = c.toUpperCase()); if (~b.inArray(L, ["CONNECT", "TRACE", "TRACK"])) throw new f.DOMException(f.DOMException.SECURITY_ERR); e = g.utf8_encode(e); c = a.parseUrl(e); U = a.hasSameOrigin(c); J = a.resolveUrl(e); if ((m || h) && !U) throw new f.DOMException(f.DOMException.INVALID_ACCESS_ERR); I = m || c.user; P = h || c.pass; E = k || !0; if (!1 === E && (d("timeout") || d("withCredentials") ||
                    "" !== d("responseType"))) throw new f.DOMException(f.DOMException.INVALID_ACCESS_ERR); Q = !E; F = !1; y = {}; d("responseText", ""); d("responseXML", null); d("response", null); d("status", 0); d("statusText", ""); d("readyState", n.OPENED); this.dispatchEvent("readystatechange")
                }, setRequestHeader: function (a, c) {
                    if (d("readyState") !== n.OPENED || F) throw new f.DOMException(f.DOMException.INVALID_STATE_ERR); if (/[\u0100-\uffff]/.test(a) || g.utf8_encode(a) !== a) throw new f.DOMException(f.DOMException.SYNTAX_ERR); a = b.trim(a).toLowerCase();
                    if (~b.inArray(a, "accept-charset accept-encoding access-control-request-headers access-control-request-method connection content-length cookie cookie2 content-transfer-encoding date expect host keep-alive origin referer te trailer transfer-encoding upgrade user-agent via".split(" ")) || /^(proxy\-|sec\-)/.test(a)) return !1; y[a] = y[a] ? y[a] + (", " + c) : c; return !0
                }, getAllResponseHeaders: function () { return S || "" }, getResponseHeader: function (a) {
                    a = a.toLowerCase(); return T || ~b.inArray(a, ["set-cookie", "set-cookie2"]) ?
                    null : S && "" !== S && (R || (R = {}, b.each(S.split(/\r\n/), function (a) { a = a.split(/:\s+/); 2 === a.length && (a[0] = b.trim(a[0]), R[a[0].toLowerCase()] = { header: a[0], value: b.trim(a[1]) }) })), R.hasOwnProperty(a)) ? R[a].header + ": " + R[a].value : null
                }, overrideMimeType: function (a) {
                    var c; if (~b.inArray(d("readyState"), [n.LOADING, n.DONE])) throw new f.DOMException(f.DOMException.INVALID_STATE_ERR); a = b.trim(a.toLowerCase()); /;/.test(a) && (c = a.match(/^([^;]+)(?:;\scharset\=)?(.*)$/)) && (a = c[1]); if (!r.mimes[a]) throw new f.DOMException(f.DOMException.SYNTAX_ERR);
                }, send: function (a, d) {
                    K = "string" === b.typeOf(d) ? { ruid: d } : d ? d : {}; if (this.readyState !== n.OPENED || F) throw new f.DOMException(f.DOMException.INVALID_STATE_ERR); if (a instanceof k) K.ruid = a.ruid, O = a.type || "application/octet-stream"; else if (a instanceof t) { if (a.hasBlob()) { var c = a.getBlob(); K.ruid = c.ruid; O = c.type || "application/octet-stream" } } else "string" === typeof a && (M = "UTF-8", O = "text/plain;charset=UTF-8", a = g.utf8_encode(a)); this.withCredentials || (this.withCredentials = K.required_caps && K.required_caps.send_browser_cookies &&
                    !U); N = !Q && this.upload.hasEventListener(); T = !1; D = !a; Q || (F = !0); h.call(this, a)
                }, abort: function () { T = !0; Q = !1; if (~b.inArray(d("readyState"), [n.UNSENT, n.OPENED, n.DONE])) d("readyState", n.UNSENT); else { d("readyState", n.DONE); F = !1; if (H) H.getRuntime().exec.call(H, "XMLHttpRequest", "abort", D); else throw new f.DOMException(f.DOMException.INVALID_STATE_ERR); D = !0 } }, destroy: function () { H && ("function" === b.typeOf(H.destroy) && H.destroy(), H = null); this.unbindAll(); this.upload && (this.upload.unbindAll(), this.upload = null) }
            });
            this.handleEventProps(z.concat(["readystatechange"])); this.upload.handleEventProps(z)
        } var x = {
            100: "Continue", 101: "Switching Protocols", 102: "Processing", 200: "OK", 201: "Created", 202: "Accepted", 203: "Non-Authoritative Information", 204: "No Content", 205: "Reset Content", 206: "Partial Content", 207: "Multi-Status", 226: "IM Used", 300: "Multiple Choices", 301: "Moved Permanently", 302: "Found", 303: "See Other", 304: "Not Modified", 305: "Use Proxy", 306: "Reserved", 307: "Temporary Redirect", 400: "Bad Request", 401: "Unauthorized",
            402: "Payment Required", 403: "Forbidden", 404: "Not Found", 405: "Method Not Allowed", 406: "Not Acceptable", 407: "Proxy Authentication Required", 408: "Request Timeout", 409: "Conflict", 410: "Gone", 411: "Length Required", 412: "Precondition Failed", 413: "Request Entity Too Large", 414: "Request-URI Too Long", 415: "Unsupported Media Type", 416: "Requested Range Not Satisfiable", 417: "Expectation Failed", 422: "Unprocessable Entity", 423: "Locked", 424: "Failed Dependency", 426: "Upgrade Required", 500: "Internal Server Error", 501: "Not Implemented",
            502: "Bad Gateway", 503: "Service Unavailable", 504: "Gateway Timeout", 505: "HTTP Version Not Supported", 506: "Variant Also Negotiates", 507: "Insufficient Storage", 510: "Not Extended"
        }; v.prototype = d.instance; var z = "loadstart progress abort error load timeout loadend".split(" "); n.UNSENT = 0; n.OPENED = 1; n.HEADERS_RECEIVED = 2; n.LOADING = 3; n.DONE = 4; n.prototype = d.instance; return n
    }); e("moxie/runtime/Transporter", ["moxie/core/utils/Basic", "moxie/core/utils/Encode", "moxie/runtime/RuntimeClient", "moxie/core/EventTarget"],
    function (b, c, d, g) {
        function a() {
            function g(d, c) { var f = this; h = c; f.bind("TransportingProgress", function (d) { r = d.loaded; r < p && -1 === b.inArray(f.state, [a.IDLE, a.DONE]) && e.call(f) }, 999); f.bind("TransportingComplete", function () { r = p; f.state = a.DONE; t = null; f.result = h.exec.call(f, "Transporter", "getAsBlob", d || "") }, 999); f.state = a.BUSY; f.trigger("TransportingStarted"); e.call(f) } function e() { var a; a = p - r; v > a && (v = a); a = c.btoa(t.substr(r, v)); h.exec.call(this, "Transporter", "receive", a, p) } var k, h, t, p, r, v; d.call(this); b.extend(this,
            {
                uid: b.guid("uid_"), state: a.IDLE, result: null, transport: function (a, d, c) { var f = this; c = b.extend({ chunk_size: 204798 }, c); if (k = c.chunk_size % 3) c.chunk_size += 3 - k; v = c.chunk_size; p = r = 0; t = this.result = null; t = a; p = a.length; if ("string" === b.typeOf(c) || c.ruid) g.call(f, d, this.connectRuntime(c)); else { var e = function (a, b) { f.unbind("RuntimeInit", e); g.call(f, d, b) }; this.bind("RuntimeInit", e); this.connectRuntime(c) } }, abort: function () {
                    this.state = a.IDLE; h && (h.exec.call(this, "Transporter", "clear"), this.trigger("TransportingAborted"));
                    p = r = 0; t = this.result = null
                }, destroy: function () { this.unbindAll(); h = null; this.disconnectRuntime(); p = r = 0; t = this.result = null }
            })
        } a.IDLE = 0; a.BUSY = 1; a.DONE = 2; a.prototype = g.instance; return a
    }); e("moxie/image/Image", "moxie/core/utils/Basic moxie/core/utils/Dom moxie/core/Exceptions moxie/file/FileReaderSync moxie/xhr/XMLHttpRequest moxie/runtime/Runtime moxie/runtime/RuntimeClient moxie/runtime/Transporter moxie/core/utils/Env moxie/core/EventTarget moxie/file/Blob moxie/file/File moxie/core/utils/Encode".split(" "),
    function (b, c, d, g, a, e, k, h, u, t, p, r, v) {
        function n() {
            function g(a) {
                var c = b.typeOf(a); try {
                    if (a instanceof n) { if (!a.size) throw new d.DOMException(d.DOMException.INVALID_STATE_ERR); t.apply(this, arguments) } else if (a instanceof p) { if (!~b.inArray(a.type, ["image/jpeg", "image/png"])) throw new d.ImageError(d.ImageError.WRONG_FORMAT); C.apply(this, arguments) } else if (-1 !== b.inArray(c, ["blob", "file"])) g.call(this, new r(null, a), arguments[1]); else if ("string" === c) "data:" === a.substr(0, 5) ? g.call(this, new p(null, { data: a }),
                    arguments[1]) : G.apply(this, arguments); else if ("node" === c && "img" === a.nodeName.toLowerCase()) g.call(this, a.src, arguments[1]); else throw new d.DOMException(d.DOMException.TYPE_MISMATCH_ERR);
                } catch (f) { this.trigger("error", f.code) }
            } function t(a, d) { var c = this.connectRuntime(a.ruid); this.ruid = c.uid; c.exec.call(this, "Image", "loadFromImage", a, "undefined" === b.typeOf(d) ? !0 : d) } function C(a, d) {
                function c(d) { f.ruid = d.uid; d.exec.call(f, "Image", "loadFromBlob", a) } var f = this; f.name = a.name || ""; a.isDetached() ? (this.bind("RuntimeInit",
                function (a, d) { c(d) }), d && "string" === typeof d.required_caps && (d.required_caps = e.parseCaps(d.required_caps)), this.connectRuntime(b.extend({ required_caps: { access_image_binary: !0, resize_image: !0 } }, d))) : c(this.connectRuntime(a.ruid))
            } function G(d, b) {
                var c = this, f; f = new a; f.open("get", d); f.responseType = "blob"; f.onprogress = function (a) { c.trigger(a) }; f.onload = function () { C.call(c, f.response, !0) }; f.onerror = function (a) { c.trigger(a) }; f.onloadend = function () { f.destroy() }; f.bind("RuntimeError", function (a, d) {
                    c.trigger("RuntimeError",
                    d)
                }); f.send(null, b)
            } k.call(this); b.extend(this, {
                uid: b.guid("uid_"), ruid: null, name: "", size: 0, width: 0, height: 0, type: "", meta: {}, clone: function () { this.load.apply(this, arguments) }, load: function () { g.apply(this, arguments) }, downsize: function (a, c, f, g) {
                    var e = { width: this.width, height: this.height, type: this.type || "image/jpeg", quality: 90, crop: !1, preserveHeaders: !0, resample: !1 }; a = "object" === typeof a ? b.extend(e, a) : b.extend(e, { width: a, height: c, crop: f, preserveHeaders: g }); try {
                        if (!this.size) throw new d.DOMException(d.DOMException.INVALID_STATE_ERR);
                        if (this.width > n.MAX_RESIZE_WIDTH || this.height > n.MAX_RESIZE_HEIGHT) throw new d.ImageError(d.ImageError.MAX_RESOLUTION_ERR); this.exec("Image", "downsize", a.width, a.height, a.crop, a.preserveHeaders)
                    } catch (k) { this.trigger("error", k.code) }
                }, crop: function (a, d, b) { this.downsize(a, d, !0, b) }, getAsCanvas: function () { if (!u.can("create_canvas")) throw new d.RuntimeError(d.RuntimeError.NOT_SUPPORTED_ERR); return this.connectRuntime(this.ruid).exec.call(this, "Image", "getAsCanvas") }, getAsBlob: function (a, b) {
                    if (!this.size) throw new d.DOMException(d.DOMException.INVALID_STATE_ERR);
                    return this.exec("Image", "getAsBlob", a || "image/jpeg", b || 90)
                }, getAsDataURL: function (a, b) { if (!this.size) throw new d.DOMException(d.DOMException.INVALID_STATE_ERR); return this.exec("Image", "getAsDataURL", a || "image/jpeg", b || 90) }, getAsBinaryString: function (a, d) { var b = this.getAsDataURL(a, d); return v.atob(b.substring(b.indexOf("base64,") + 7)) }, embed: function (a, g) {
                    function e(c, f) {
                        var g = this; if (u.can("create_canvas")) { var q = g.getAsCanvas(); if (q) { a.appendChild(q); q = null; g.destroy(); k.trigger("embedded"); return } } q =
                        g.getAsDataURL(c, f); if (!q) throw new d.ImageError(d.ImageError.WRONG_FORMAT); if (u.can("use_data_uri_of", q.length)) a.innerHTML = '<img src="' + q + '" width="' + g.width + '" height="' + g.height + '" />', g.destroy(), k.trigger("embedded"); else {
                            var t = new h; t.bind("TransportingComplete", function () {
                                m = k.connectRuntime(this.result.ruid); k.bind("Embedded", function () { b.extend(m.getShimContainer().style, { top: "0px", left: "0px", width: g.width + "px", height: g.height + "px" }); m = null }, 999); m.exec.call(k, "ImageView", "display", this.result.uid,
                                width, height); g.destroy()
                            }); t.transport(v.atob(q.substring(q.indexOf("base64,") + 7)), c, { required_caps: { display_media: !0 }, runtime_order: "flash,silverlight", container: a })
                        }
                    } var k = this, m; g = b.extend({ width: this.width, height: this.height, type: this.type || "image/jpeg", quality: 90 }, g || {}); try {
                        if (!(a = c.get(a))) throw new d.DOMException(d.DOMException.INVALID_NODE_TYPE_ERR); if (!this.size) throw new d.DOMException(d.DOMException.INVALID_STATE_ERR); var q = new n; q.bind("Resize", function () { e.call(this, g.type, g.quality) });
                        q.bind("Load", function () { q.downsize(g) }); this.meta.thumb && this.meta.thumb.width >= g.width && this.meta.thumb.height >= g.height ? q.load(this.meta.thumb.data) : q.clone(this, !1); return q
                    } catch (t) { this.trigger("error", t.code) }
                }, destroy: function () { this.ruid && (this.getRuntime().exec.call(this, "Image", "destroy"), this.disconnectRuntime()); this.unbindAll() }
            }); this.handleEventProps(x); this.bind("Load Resize", function () {
                var a = void 0, a = this.exec("Image", "getInfo"); this.size = a.size; this.width = a.width; this.height = a.height;
                this.type = a.type; this.meta = a.meta; "" === this.name && (this.name = a.name)
            }, 999)
        } var x = ["progress", "load", "error", "resize", "embedded"]; n.MAX_RESIZE_WIDTH = 8192; n.MAX_RESIZE_HEIGHT = 8192; n.prototype = t.instance; return n
    }); e("moxie/runtime/html5/Runtime", ["moxie/core/utils/Basic", "moxie/core/Exceptions", "moxie/runtime/Runtime", "moxie/core/utils/Env"], function (b, c, d, g) {
        var a = {}; d.addConstructor("html5", function (c, f, e) {
            var k = this, h = d.capTest, p = d.capTrue; e = b.extend({
                access_binary: h(window.FileReader || window.File &&
                window.File.getAsDataURL), access_image_binary: function () { return k.can("access_binary") && !!a.Image }, display_media: h(g.can("create_canvas") || g.can("use_data_uri_over32kb")), do_cors: h(window.XMLHttpRequest && "withCredentials" in new XMLHttpRequest), drag_and_drop: h(function () { var a = document.createElement("div"); return ("draggable" in a || "ondragstart" in a && "ondrop" in a) && ("IE" !== g.browser || g.verComp(g.version, 9, ">")) }()), filter_by_extension: h("Chrome" === g.browser && g.verComp(g.version, 28, ">=") || "IE" === g.browser &&
                g.verComp(g.version, 10, ">=") || "Safari" === g.browser && g.verComp(g.version, 7, ">=")), return_response_headers: p, return_response_type: function (a) { return "json" === a && window.JSON ? !0 : g.can("return_response_type", a) }, return_status_code: p, report_upload_progress: h(window.XMLHttpRequest && (new XMLHttpRequest).upload), resize_image: function () { return k.can("access_binary") && g.can("create_canvas") }, select_file: function () { return g.can("use_fileinput") && window.File }, select_folder: function () {
                    return k.can("select_file") &&
                    "Chrome" === g.browser && g.verComp(g.version, 21, ">=")
                }, select_multiple: function () { return k.can("select_file") && !("Safari" === g.browser && "Windows" === g.os) && !("iOS" === g.os && g.verComp(g.osVersion, "7.0.0", ">") && g.verComp(g.osVersion, "8.0.0", "<")) }, send_binary_string: h(window.XMLHttpRequest && ((new XMLHttpRequest).sendAsBinary || window.Uint8Array && window.ArrayBuffer)), send_custom_headers: h(window.XMLHttpRequest), send_multipart: function () {
                    return !!(window.XMLHttpRequest && (new XMLHttpRequest).upload && window.FormData) ||
                    k.can("send_binary_string")
                }, slice_blob: h(window.File && (File.prototype.mozSlice || File.prototype.webkitSlice || File.prototype.slice)), stream_upload: function () { return k.can("slice_blob") && k.can("send_multipart") }, summon_file_dialog: function () { return k.can("select_file") && ("Firefox" === g.browser && g.verComp(g.version, 4, ">=") || "Opera" === g.browser && g.verComp(g.version, 12, ">=") || "IE" === g.browser && g.verComp(g.version, 10, ">=") || !!~b.inArray(g.browser, ["Chrome", "Safari"])) }, upload_filesize: p
            }, e); d.call(this,
            c, f || "html5", e); b.extend(this, { init: function () { this.trigger("Init") }, destroy: function (a) { return function () { a.call(k); a = k = null } }(this.destroy) }); b.extend(this.getShim(), a)
        }); return a
    }); e("moxie/core/utils/Events", ["moxie/core/utils/Basic"], function (b) {
        function c() { this.returnValue = !1 } function d() { this.cancelBubble = !0 } var g = {}, a = "moxie_" + b.guid(), e = function (d, c, f) {
            var e; c = c.toLowerCase(); if (d[a] && g[d[a]] && g[d[a]][c]) {
                e = g[d[a]][c]; for (var k = e.length - 1; 0 <= k; k--) if (e[k].orig === f || e[k].key === f) if (d.removeEventListener ?
                d.removeEventListener(c, e[k].func, !1) : d.detachEvent && d.detachEvent("on" + c, e[k].func), e[k].orig = null, e[k].func = null, e.splice(k, 1), void 0 !== f) break; e.length || delete g[d[a]][c]; if (b.isEmptyObj(g[d[a]])) { delete g[d[a]]; try { delete d[a] } catch (h) { d[a] = void 0 } }
            }
        }; return {
            addEvent: function (e, k, h, q) {
                var p; k = k.toLowerCase(); e.addEventListener ? (p = h, e.addEventListener(k, p, !1)) : e.attachEvent && (p = function () { var a = window.event; a.target || (a.target = a.srcElement); a.preventDefault = c; a.stopPropagation = d; h(a) }, e.attachEvent("on" +
                k, p)); e[a] || (e[a] = b.guid()); g.hasOwnProperty(e[a]) || (g[e[a]] = {}); e = g[e[a]]; e.hasOwnProperty(k) || (e[k] = []); e[k].push({ func: p, orig: h, key: q })
            }, removeEvent: e, removeAllEvents: function (d, c) { d && d[a] && b.each(g[d[a]], function (a, b) { e(d, b, c) }) }
        }
    }); e("moxie/runtime/html5/file/FileInput", "moxie/runtime/html5/Runtime moxie/file/File moxie/core/utils/Basic moxie/core/utils/Dom moxie/core/utils/Events moxie/core/utils/Mime moxie/core/utils/Env".split(" "), function (b, c, d, g, a, e, k) {
        return b.FileInput = function () {
            var b;
            d.extend(this, {
                init: function (l) {
                    var h = this, p = h.getRuntime(), r, v, n; b = l; l = b.accept.mimes || e.extList2mimes(b.accept, p.can("filter_by_extension")); r = p.getShimContainer(); r.innerHTML = '<input id="' + p.uid + '" type="file" style="font-size:999px;opacity:0;"' + (b.multiple && p.can("select_multiple") ? "multiple" : "") + (b.directory && p.can("select_folder") ? "webkitdirectory directory" : "") + (l ? ' accept="' + l.join(",") + '"' : "") + " />"; l = g.get(p.uid); d.extend(l.style, { position: "absolute", top: 0, left: 0, width: "100%", height: "100%" });
                    v = g.get(b.browse_button); p.can("summon_file_dialog") && ("static" === g.getStyle(v, "position") && (v.style.position = "relative"), n = parseInt(g.getStyle(v, "z-index"), 10) || 1, v.style.zIndex = n, r.style.zIndex = n - 1, a.addEvent(v, "click", function (a) { var d = g.get(p.uid); d && !d.disabled && d.click(); a.preventDefault() }, h.uid)); r = p.can("summon_file_dialog") ? v : r; a.addEvent(r, "mouseover", function () { h.trigger("mouseenter") }, h.uid); a.addEvent(r, "mouseout", function () { h.trigger("mouseleave") }, h.uid); a.addEvent(r, "mousedown", function () { h.trigger("mousedown") },
                    h.uid); a.addEvent(g.get(b.container), "mouseup", function () { h.trigger("mouseup") }, h.uid); l.onchange = function z(a) { h.files = []; d.each(this.files, function (a) { var d = ""; if (b.directory && "." == a.name) return !0; a.webkitRelativePath && (d = "/" + a.webkitRelativePath.replace(/^\//, "")); a = new c(p.uid, a); a.relativePath = d; h.files.push(a) }); "IE" !== k.browser && "IEMobile" !== k.browser ? this.value = "" : (a = this.cloneNode(!0), this.parentNode.replaceChild(a, this), a.onchange = z); h.files.length && h.trigger("change") }; h.trigger({
                        type: "ready",
                        async: !0
                    }); r = null
                }, disable: function (a) { var d = this.getRuntime(); if (d = g.get(d.uid)) d.disabled = !!a }, destroy: function () { var d = this.getRuntime(), c = d.getShim(), d = d.getShimContainer(); a.removeAllEvents(d, this.uid); a.removeAllEvents(b && g.get(b.container), this.uid); a.removeAllEvents(b && g.get(b.browse_button), this.uid); d && (d.innerHTML = ""); c.removeInstance(this.uid); b = null }
            })
        }
    }); e("moxie/runtime/html5/file/Blob", ["moxie/runtime/html5/Runtime", "moxie/file/Blob"], function (b, c) {
        return b.Blob = function () {
            function d(d,
            a, b) { var c; if (window.File.prototype.slice) try { return d.slice(), d.slice(a, b) } catch (f) { return d.slice(a, b - a) } else return (c = window.File.prototype.webkitSlice || window.File.prototype.mozSlice) ? c.call(d, a, b) : null } this.slice = function () { return new c(this.getRuntime().uid, d.apply(this, arguments)) }
        }
    }); e("moxie/runtime/html5/file/FileDrop", "moxie/runtime/html5/Runtime moxie/file/File moxie/core/utils/Basic moxie/core/utils/Dom moxie/core/utils/Events moxie/core/utils/Mime".split(" "), function (b, c, d, g, a, e) {
        return b.FileDrop =
        function () {
            function b(a) { if (!a.dataTransfer || !a.dataTransfer.types) return !1; a = d.toArray(a.dataTransfer.types || []); return -1 !== d.inArray("Files", a) || -1 !== d.inArray("public.file-url", a) || -1 !== d.inArray("application/x-moz-file", a) } function k(a, b) { var g; x.length ? (g = e.getFileExtension(a.name), g = !g || -1 !== d.inArray(g, x)) : g = !0; g && (g = new c(B, a), g.relativePath = b || "", n.push(g)) } function l(a) { for (var b = [], c = 0; c < a.length; c++)[].push.apply(b, a[c].extensions.split(/\s*,\s*/)); return -1 === d.inArray("*", b) ? b : [] } function h(a,
            b) { var c = []; d.each(a, function (a) { var d = a.webkitGetAsEntry(); d && (d.isFile ? k(a.getAsFile(), d.fullPath) : c.push(d)) }); c.length ? p(c, b) : b() } function p(a, b) { var c = []; d.each(a, function (a) { c.push(function (d) { r(a, d) }) }); d.inSeries(c, function () { b() }) } function r(a, d) { a.isFile ? a.file(function (b) { k(b, a.fullPath); d() }, function () { d() }) : a.isDirectory ? v(a, d) : d() } function v(a, d) { function b(a) { f.readEntries(function (d) { d.length ? ([].push.apply(c, d), b(a)) : a() }, a) } var c = [], f = a.createReader(); b(function () { p(c, d) }) } var n =
            [], x = [], z, B; d.extend(this, {
                init: function (c) {
                    var f = this; z = c; B = f.ruid; x = l(z.accept); z.inIframe && (c = document.getElementById(z.editor_id).contentWindow.document, a.addEvent(c, "dragover", function (a) { b(a) && (a.preventDefault(), a.dataTransfer.dropEffect = "copy") }, f.uid), a.addEvent(c, "drop", function (a) {
                        b(a) && (a.preventDefault(), n = [], a.dataTransfer.items && a.dataTransfer.items[0].webkitGetAsEntry ? h(a.dataTransfer.items, function () { f.files = n; f.trigger("drop") }) : (d.each(a.dataTransfer.files, function (a) { k(a) }), f.files =
                        n, f.trigger("drop")))
                    }, f.uid), a.addEvent(c, "dragenter", function (a) { f.trigger("dragenter") }, f.uid), a.addEvent(c, "dragleave", function (a) { f.trigger("dragleave") }, f.uid)); c = z.container; a.addEvent(c, "dragover", function (a) { b(a) && (a.preventDefault(), a.dataTransfer.dropEffect = "copy") }, f.uid); a.addEvent(c, "drop", function (a) {
                        b(a) && (a.preventDefault(), n = [], a.dataTransfer.items && a.dataTransfer.items[0].webkitGetAsEntry ? h(a.dataTransfer.items, function () { f.files = n; f.trigger("drop") }) : (d.each(a.dataTransfer.files,
                        function (a) { k(a) }), f.files = n, f.trigger("drop")))
                    }, f.uid); a.addEvent(c, "dragenter", function (a) { f.trigger("dragenter") }, f.uid); a.addEvent(c, "dragleave", function (a) { f.trigger("dragleave") }, f.uid)
                }, destroy: function () { a.removeAllEvents(z && g.get(z.container), this.uid); B = n = x = z = null }
            })
        }
    }); e("moxie/runtime/html5/file/FileReader", ["moxie/runtime/html5/Runtime", "moxie/core/utils/Encode", "moxie/core/utils/Basic"], function (b, c, d) {
        return b.FileReader = function () {
            var b, a = !1; d.extend(this, {
                read: function (e, k) {
                    var l =
                    this; l.result = ""; b = new window.FileReader; b.addEventListener("progress", function (a) { l.trigger(a) }); b.addEventListener("load", function (d) { var e; a ? (e = b.result, e = c.atob(e.substring(e.indexOf("base64,") + 7))) : e = b.result; l.result = e; l.trigger(d) }); b.addEventListener("error", function (a) { l.trigger(a, b.error) }); b.addEventListener("loadend", function (a) { b = null; l.trigger(a) }); "function" === d.typeOf(b[e]) ? (a = !1, b[e](k.getSource())) : "readAsBinaryString" === e && (a = !0, b.readAsDataURL(k.getSource()))
                }, abort: function () {
                    b &&
                    b.abort()
                }, destroy: function () { b = null }
            })
        }
    }); e("moxie/runtime/html5/xhr/XMLHttpRequest", "moxie/runtime/html5/Runtime moxie/core/utils/Basic moxie/core/utils/Mime moxie/core/utils/Url moxie/file/File moxie/file/Blob moxie/xhr/FormData moxie/core/Exceptions moxie/core/utils/Env".split(" "), function (b, c, d, g, a, e, k, h, u) {
        return b.XMLHttpRequest = function () {
            function b(a, d) {
                var c = this, f, g; f = d.getBlob().getSource(); g = new window.FileReader; g.onload = function () {
                    d.append(d.getBlobName(), new e(null, { type: f.type, data: g.result }));
                    v.send.call(c, a, d)
                }; g.readAsBinaryString(f)
            } function l() { if (!window.XMLHttpRequest || "IE" === u.browser && u.verComp(u.version, 8, "<")) { var a; a: { for (var d = ["Msxml2.XMLHTTP.6.0", "Microsoft.XMLHTTP"], b = 0; b < d.length; b++) try { a = new ActiveXObject(d[b]); break a } catch (c) { } a = void 0 } return a } return new window.XMLHttpRequest } function r(a) {
                var d = "----moxieboundary" + (new Date).getTime(), b = ""; if (!this.getRuntime().can("send_binary_string")) throw new h.RuntimeError(h.RuntimeError.NOT_SUPPORTED_ERR); n.setRequestHeader("Content-Type",
                "multipart/form-data; boundary=" + d); a.each(function (a, c) { b = a instanceof e ? b + ("--" + d + '\r\nContent-Disposition: form-data; name="' + c + '"; filename="' + unescape(encodeURIComponent(a.name || "blob")) + '"\r\nContent-Type: ' + (a.type || "application/octet-stream") + "\r\n\r\n" + a.getSource() + "\r\n") : b + ("--" + d + '\r\nContent-Disposition: form-data; name="' + c + '"\r\n\r\n' + unescape(encodeURIComponent(a)) + "\r\n") }); return b += "--" + d + "--\r\n"
            } var v = this, n, x; c.extend(this, {
                send: function (a, d) {
                    var h = this, w = "Mozilla" === u.browser &&
                    u.verComp(u.version, 4, ">=") && u.verComp(u.version, 7, "<"), v = "Android Browser" === u.browser, E = !1; x = a.url.replace(/^.+?\/([\w\-\.]+)$/, "$1").toLowerCase(); n = l(); n.open(a.method, a.url, a.async, a.user, a.password); if (d instanceof e) d.isDetached() && (E = !0), d = d.getSource(); else if (d instanceof k) {
                        if (d.hasBlob()) if (d.getBlob().isDetached()) d = r.call(h, d), E = !0; else if ((w || v) && "blob" === c.typeOf(d.getBlob().getSource()) && window.FileReader) { b.call(h, a, d); return } if (d instanceof k) {
                            var J = new window.FormData; d.each(function (a,
                            d) { a instanceof e ? J.append(d, a.getSource()) : J.append(d, a) }); d = J
                        }
                    } n.upload ? (a.withCredentials && (n.withCredentials = !0), n.addEventListener("load", function (a) { h.trigger(a) }), n.addEventListener("error", function (a) { h.trigger(a) }), n.addEventListener("progress", function (a) { h.trigger(a) }), n.upload.addEventListener("progress", function (a) { h.trigger({ type: "UploadProgress", loaded: a.loaded, total: a.total }) })) : n.onreadystatechange = function () {
                        switch (n.readyState) {
                            case 3: var d, b; try {
                                g.hasSameOrigin(a.url) && (d = n.getResponseHeader("Content-Length") ||
                                0), n.responseText && (b = n.responseText.length)
                            } catch (c) { d = b = 0 } h.trigger({ type: "progress", lengthComputable: !!d, total: parseInt(d, 10), loaded: b }); break; case 4: n.onreadystatechange = function () { }, 0 === n.status ? h.trigger("error") : h.trigger("load")
                        }
                    }; c.isEmptyObj(a.headers) || c.each(a.headers, function (a, d) { n.setRequestHeader(d, a) }); "" !== a.responseType && "responseType" in n && ("json" !== a.responseType || u.can("return_response_type", "json") ? n.responseType = a.responseType : n.responseType = "text"); E ? n.sendAsBinary ? n.sendAsBinary(d) :
                    function () { for (var a = new Uint8Array(d.length), b = 0; b < d.length; b++) a[b] = d.charCodeAt(b) & 255; n.send(a.buffer) }() : n.send(d); h.trigger("loadstart")
                }, getStatus: function () { try { if (n) return n.status } catch (a) { } return 0 }, getResponse: function (b) {
                    var c = this.getRuntime(); try {
                        switch (b) {
                            case "blob": var f = new a(c.uid, n.response), g = n.getResponseHeader("Content-Disposition"); if (g) { var e = g.match(/filename=([\'\"'])([^\1]+)\1/); e && (x = e[2]) } f.name = x; f.type || (f.type = d.getFileMime(x)); return f; case "json": return u.can("return_response_type",
                            "json") ? n.response : 200 === n.status && window.JSON ? JSON.parse(n.responseText) : null; case "document": var k; var l = n.responseXML, h = n.responseText; "IE" === u.browser && h && l && !l.documentElement && /[^\/]+\/[^\+]+\+xml/.test(n.getResponseHeader("Content-Type")) && (l = new window.ActiveXObject("Microsoft.XMLDOM"), l.async = !1, l.validateOnParse = !1, l.loadXML(h)); k = l && ("IE" === u.browser && 0 !== l.parseError || !l.documentElement || "parsererror" === l.documentElement.tagName) ? null : l; return k; default: return "" !== n.responseText ? n.responseText :
                            null
                        }
                    } catch (m) { return null }
                }, getAllResponseHeaders: function () { try { return n.getAllResponseHeaders() } catch (a) { } return "" }, abort: function () { n && n.abort() }, destroy: function () { v = x = null }
            })
        }
    }); e("moxie/runtime/html5/utils/BinaryReader", ["moxie/core/utils/Basic"], function (b) {
        function c(a) { a instanceof ArrayBuffer ? d.apply(this, arguments) : g.apply(this, arguments) } function d(a) {
            var d = new DataView(a); b.extend(this, {
                readByteAt: function (a) { return d.getUint8(a) }, writeByteAt: function (a, b) { d.setUint8(a, b) }, SEGMENT: function (b,
                c, f) { switch (arguments.length) { case 2: return a.slice(b, b + c); case 1: return a.slice(b); case 3: if (null === f && (f = new ArrayBuffer), f instanceof ArrayBuffer) { var g = new Uint8Array(this.length() - c + f.byteLength); 0 < b && g.set(new Uint8Array(a.slice(0, b)), 0); g.set(new Uint8Array(f), b); g.set(new Uint8Array(a.slice(b + c)), b + f.byteLength); this.clear(); a = g.buffer; d = new DataView(a); break } default: return a } }, length: function () { return a ? a.byteLength : 0 }, clear: function () { d = a = null }
            })
        } function g(a) {
            function d(b, c, f) {
                f = 3 === arguments.length ?
                    f : a.length - c - 1; a = a.substr(0, c) + b + a.substr(f + c)
            } b.extend(this, { readByteAt: function (d) { return a.charCodeAt(d) }, writeByteAt: function (a, b) { d(String.fromCharCode(b), a, 1) }, SEGMENT: function (b, c, f) { switch (arguments.length) { case 1: return a.substr(b); case 2: return a.substr(b, c); case 3: d(null !== f ? f : "", b, c); break; default: return a } }, length: function () { return a ? a.length : 0 }, clear: function () { a = null } })
        } b.extend(c.prototype, {
            littleEndian: !1, read: function (a, d) {
                var b, c, f; if (a + d > this.length()) throw Error("You are trying to read outside the source boundaries.");
                c = this.littleEndian ? 0 : -8 * (d - 1); for (b = f = 0; f < d; f++) b |= this.readByteAt(a + f) << Math.abs(c + 8 * f); return b
            }, write: function (a, d, b) { var c, f; if (a > this.length()) throw Error("You are trying to write outside the source boundaries."); c = this.littleEndian ? 0 : -8 * (b - 1); for (f = 0; f < b; f++) this.writeByteAt(a + f, d >> Math.abs(c + 8 * f) & 255) }, BYTE: function (a) { return this.read(a, 1) }, SHORT: function (a) { return this.read(a, 2) }, LONG: function (a) { return this.read(a, 4) }, SLONG: function (a) {
                a = this.read(a, 4); return 2147483647 < a ? a - 4294967296 :
                a
            }, CHAR: function (a) { return String.fromCharCode(this.read(a, 1)) }, STRING: function (a, d) { return this.asArray("CHAR", a, d).join("") }, asArray: function (a, d, b) { for (var c = [], f = 0; f < b; f++) c[f] = this[a](d + f); return c }
        }); return c
    }); e("moxie/runtime/html5/image/JPEGHeaders", ["moxie/runtime/html5/utils/BinaryReader", "moxie/core/Exceptions"], function (b, c) {
        return function g(a) {
            var e = [], k, h, u = 0; a = new b(a); if (65496 !== a.SHORT(0)) throw a.clear(), new c.ImageError(c.ImageError.WRONG_FORMAT); for (k = 2; k <= a.length() ;) if (h = a.SHORT(k),
            65488 <= h && 65495 >= h) k += 2; else { if (65498 === h || 65497 === h) break; u = a.SHORT(k + 2) + 2; 65505 <= h && 65519 >= h && e.push({ hex: h, name: "APP" + (h & 15), start: k, length: u, segment: a.SEGMENT(k, u) }); k += u } a.clear(); return {
                headers: e, restore: function (a) { var c, f; f = new b(a); k = 65504 == f.SHORT(2) ? 4 + f.SHORT(4) : 2; c = 0; for (a = e.length; c < a; c++) f.SEGMENT(k, 0, e[c].segment), k += e[c].length; a = f.SEGMENT(); f.clear(); return a }, strip: function (a) {
                    var c, f; c = new g(a); f = c.headers; c.purge(); c = new b(a); for (a = f.length; a--;) c.SEGMENT(f[a].start, f[a].length,
                    ""); a = c.SEGMENT(); c.clear(); return a
                }, get: function (a) { for (var b = [], c = 0, f = e.length; c < f; c++) e[c].name === a.toUpperCase() && b.push(e[c].segment); return b }, set: function (a, b) { var c = [], f, g, k; "string" === typeof b ? c.push(b) : c = b; f = g = 0; for (k = e.length; f < k && !(e[f].name === a.toUpperCase() && (e[f].segment = c[g], e[f].length = c[g].length, g++), g >= c.length) ; f++); }, purge: function () { this.headers = e = [] }
            }
        }
    }); e("moxie/runtime/html5/image/ExifParser", ["moxie/core/utils/Basic", "moxie/runtime/html5/utils/BinaryReader", "moxie/core/Exceptions"],
    function (b, f, d) {
        function g(a) {
            function e(a, f) {
                var g, k, h, m, q, r, p; m = []; var w = {}, L = { 1: "BYTE", 7: "UNDEFINED", 2: "ASCII", 3: "SHORT", 4: "LONG", 5: "RATIONAL", 9: "SLONG", 10: "SRATIONAL" }, y = { BYTE: 1, UNDEFINED: 1, ASCII: 1, SHORT: 2, LONG: 4, RATIONAL: 8, SLONG: 4, SRATIONAL: 8 }; g = this.SHORT(a); for (k = 0; k < g; k++) if (p = a + 2 + 12 * k, h = f[this.SHORT(p)], h !== c) {
                    m = L[this.SHORT(p += 2)]; q = this.LONG(p += 2); r = y[m]; if (!r) throw new d.ImageError(d.ImageError.INVALID_META_ERR); p += 4; 4 < r * q && (p = this.LONG(p) + t.tiffHeader); if (p + r * q >= this.length()) throw new d.ImageError(d.ImageError.INVALID_META_ERR);
                    "ASCII" === m ? w[h] = b.trim(this.STRING(p, q).replace(/\0$/, "")) : (m = this.asArray(m, p, q), q = 1 == q ? m[0] : m, u.hasOwnProperty(h) && "object" != typeof q ? w[h] = u[h][q] : w[h] = q)
                } return w
            } var k, h, u, t, p, r; f.call(this, a); h = {
                tiff: { 274: "Orientation", 270: "ImageDescription", 271: "Make", 272: "Model", 305: "Software", 34665: "ExifIFDPointer", 34853: "GPSInfoIFDPointer" }, exif: {
                    36864: "ExifVersion", 40961: "ColorSpace", 40962: "PixelXDimension", 40963: "PixelYDimension", 36867: "DateTimeOriginal", 33434: "ExposureTime", 33437: "FNumber", 34855: "ISOSpeedRatings",
                    37377: "ShutterSpeedValue", 37378: "ApertureValue", 37383: "MeteringMode", 37384: "LightSource", 37385: "Flash", 37386: "FocalLength", 41986: "ExposureMode", 41987: "WhiteBalance", 41990: "SceneCaptureType", 41988: "DigitalZoomRatio", 41992: "Contrast", 41993: "Saturation", 41994: "Sharpness"
                }, gps: { 0: "GPSVersionID", 1: "GPSLatitudeRef", 2: "GPSLatitude", 3: "GPSLongitudeRef", 4: "GPSLongitude" }, thumb: { 513: "JPEGInterchangeFormat", 514: "JPEGInterchangeFormatLength" }
            }; u = {
                ColorSpace: { 1: "sRGB", 0: "Uncalibrated" }, MeteringMode: {
                    0: "Unknown",
                    1: "Average", 2: "CenterWeightedAverage", 3: "Spot", 4: "MultiSpot", 5: "Pattern", 6: "Partial", 255: "Other"
                }, LightSource: {
                    1: "Daylight", 2: "Fliorescent", 3: "Tungsten", 4: "Flash", 9: "Fine weather", 10: "Cloudy weather", 11: "Shade", 12: "Daylight fluorescent (D 5700 - 7100K)", 13: "Day white fluorescent (N 4600 -5400K)", 14: "Cool white fluorescent (W 3900 - 4500K)", 15: "White fluorescent (WW 3200 - 3700K)", 17: "Standard light A", 18: "Standard light B", 19: "Standard light C", 20: "D55", 21: "D65", 22: "D75", 23: "D50", 24: "ISO studio tungsten",
                    255: "Other"
                }, Flash: {
                    0: "Flash did not fire", 1: "Flash fired", 5: "Strobe return light not detected", 7: "Strobe return light detected", 9: "Flash fired, compulsory flash mode", 13: "Flash fired, compulsory flash mode, return light not detected", 15: "Flash fired, compulsory flash mode, return light detected", 16: "Flash did not fire, compulsory flash mode", 24: "Flash did not fire, auto mode", 25: "Flash fired, auto mode", 29: "Flash fired, auto mode, return light not detected", 31: "Flash fired, auto mode, return light detected",
                    32: "No flash function", 65: "Flash fired, red-eye reduction mode", 69: "Flash fired, red-eye reduction mode, return light not detected", 71: "Flash fired, red-eye reduction mode, return light detected", 73: "Flash fired, compulsory flash mode, red-eye reduction mode", 77: "Flash fired, compulsory flash mode, red-eye reduction mode, return light not detected", 79: "Flash fired, compulsory flash mode, red-eye reduction mode, return light detected", 89: "Flash fired, auto mode, red-eye reduction mode", 93: "Flash fired, auto mode, return light not detected, red-eye reduction mode",
                    95: "Flash fired, auto mode, return light detected, red-eye reduction mode"
                }, ExposureMode: { 0: "Auto exposure", 1: "Manual exposure", 2: "Auto bracket" }, WhiteBalance: { 0: "Auto white balance", 1: "Manual white balance" }, SceneCaptureType: { 0: "Standard", 1: "Landscape", 2: "Portrait", 3: "Night scene" }, Contrast: { 0: "Normal", 1: "Soft", 2: "Hard" }, Saturation: { 0: "Normal", 1: "Low saturation", 2: "High saturation" }, Sharpness: { 0: "Normal", 1: "Soft", 2: "Hard" }, GPSLatitudeRef: { N: "North latitude", S: "South latitude" }, GPSLongitudeRef: {
                    E: "East longitude",
                    W: "West longitude"
                }
            }; t = { tiffHeader: 10 }; p = t.tiffHeader; k = { clear: this.clear }; b.extend(this, {
                read: function () { try { return g.prototype.read.apply(this, arguments) } catch (a) { throw new d.ImageError(d.ImageError.INVALID_META_ERR); } }, write: function () { try { return g.prototype.write.apply(this, arguments) } catch (a) { throw new d.ImageError(d.ImageError.INVALID_META_ERR); } }, UNDEFINED: function () { return this.BYTE.apply(this, arguments) }, RATIONAL: function (a) { return this.LONG(a) / this.LONG(a + 4) }, SRATIONAL: function (a) {
                    return this.SLONG(a) /
                    this.SLONG(a + 4)
                }, ASCII: function (a) { return this.CHAR(a) }, TIFF: function () { return r || null }, EXIF: function () { var a = null; if (t.exifIFD) { try { a = e.call(this, t.exifIFD, h.exif) } catch (d) { return null } if (a.ExifVersion && "array" === b.typeOf(a.ExifVersion)) { for (var c = 0, f = ""; c < a.ExifVersion.length; c++) f += String.fromCharCode(a.ExifVersion[c]); a.ExifVersion = f } } return a }, GPS: function () {
                    var a = null; if (t.gpsIFD) {
                        try { a = e.call(this, t.gpsIFD, h.gps) } catch (d) { return null } a.GPSVersionID && "array" === b.typeOf(a.GPSVersionID) && (a.GPSVersionID =
                        a.GPSVersionID.join("."))
                    } return a
                }, thumb: function () { if (t.IFD1) try { var a = e.call(this, t.IFD1, h.thumb); if ("JPEGInterchangeFormat" in a) return this.SEGMENT(t.tiffHeader + a.JPEGInterchangeFormat, a.JPEGInterchangeFormatLength) } catch (d) { } return null }, setExif: function (a, d) {
                    if ("PixelXDimension" !== a && "PixelYDimension" !== a) return !1; var b; a: {
                        b = a; var c, f, g, e = 0; if ("string" === typeof b) for (f in c = h.exif, c) if (c[f] === b) { b = f; break } c = t.exifIFD; f = this.SHORT(c); for (var k = 0; k < f; k++) if (g = c + 12 * k + 2, this.SHORT(g) == b) {
                            e = g + 8;
                            break
                        } if (e) { try { this.write(e, d, 4) } catch (l) { b = !1; break a } b = !0 } else b = !1
                    } return b
                }, clear: function () { k.clear(); a = h = u = r = t = k = null }
            }); if (65505 !== this.SHORT(0) || "EXIF\x00" !== this.STRING(4, 5).toUpperCase()) throw new d.ImageError(d.ImageError.INVALID_META_ERR); this.littleEndian = 18761 == this.SHORT(p); if (42 !== this.SHORT(p += 2)) throw new d.ImageError(d.ImageError.INVALID_META_ERR); t.IFD0 = t.tiffHeader + this.LONG(p += 2); r = e.call(this, t.IFD0, h.tiff); "ExifIFDPointer" in r && (t.exifIFD = t.tiffHeader + r.ExifIFDPointer, delete r.ExifIFDPointer);
            "GPSInfoIFDPointer" in r && (t.gpsIFD = t.tiffHeader + r.GPSInfoIFDPointer, delete r.GPSInfoIFDPointer); b.isEmptyObj(r) && (r = null); if (p = this.LONG(t.IFD0 + 12 * this.SHORT(t.IFD0) + 2)) t.IFD1 = t.tiffHeader + p
        } g.prototype = f.prototype; return g
    }); e("moxie/runtime/html5/image/JPEG", ["moxie/core/utils/Basic", "moxie/core/Exceptions", "moxie/runtime/html5/image/JPEGHeaders", "moxie/runtime/html5/utils/BinaryReader", "moxie/runtime/html5/image/ExifParser"], function (b, c, d, g, a) {
        return function (e) {
            function k(a) {
                var b = 0, d; for (a ||
                (a = u) ; b <= a.length() ;) { d = a.SHORT(b += 2); if (65472 <= d && 65475 >= d) return b += 5, { height: a.SHORT(b), width: a.SHORT(b + 2) }; d = a.SHORT(b += 2); b += d - 2 } return null
            } function h() { var a = p.thumb(), b, d; return a && (b = new g(a), d = k(b), b.clear(), d) ? (d.data = a, d) : null } var u, t, p, r; u = new g(e); if (65496 !== u.SHORT(0)) throw new c.ImageError(c.ImageError.WRONG_FORMAT); t = new d(e); try { p = new a(t.get("app1")[0]) } catch (v) { } r = k.call(this); b.extend(this, {
                type: "image/jpeg", size: u.length(), width: r && r.width || 0, height: r && r.height || 0, setExif: function (a,
                d) { if (!p) return !1; "object" === b.typeOf(a) ? b.each(a, function (a, b) { p.setExif(b, a) }) : p.setExif(a, d); t.set("app1", p.SEGMENT()) }, writeHeaders: function () { return arguments.length ? t.restore(arguments[0]) : t.restore(e) }, stripHeaders: function (a) { return t.strip(a) }, purge: function () { p && t && u && (p.clear(), t.purge(), u.clear(), r = t = p = u = null) }
            }); p && (this.meta = { tiff: p.TIFF(), exif: p.EXIF(), gps: p.GPS(), thumb: h() })
        }
    }); e("moxie/runtime/html5/image/PNG", ["moxie/core/Exceptions", "moxie/core/utils/Basic", "moxie/runtime/html5/utils/BinaryReader"],
    function (b, c, d) {
        return function (g) {
            function a() { e && (e.clear(), g = k = e = null) } var e, k; e = new d(g); (function () { for (var a = 0, d = 0, c = [35152, 20039, 3338, 6666], d = 0; d < c.length; d++, a += 2) if (c[d] != e.SHORT(a)) throw new b.ImageError(b.ImageError.WRONG_FORMAT); })(); k = function () { var a = 8, b, d, c; b = e.LONG(a); d = e.STRING(a += 4, 4); c = a += 4; e.LONG(a + b); return "IHDR" == d ? { width: e.LONG(c), height: e.LONG(c + 4) } : null }.call(this); c.extend(this, { type: "image/png", size: e.length(), width: k.width, height: k.height, purge: function () { a.call(this) } });
            a.call(this)
        }
    }); e("moxie/runtime/html5/image/ImageInfo", ["moxie/core/utils/Basic", "moxie/core/Exceptions", "moxie/runtime/html5/image/JPEG", "moxie/runtime/html5/image/PNG"], function (b, c, d, g) {
        return function (a) {
            var e = [d, g], k; k = function () { for (var b = 0; b < e.length; b++) try { return new e[b](a) } catch (d) { } throw new c.ImageError(c.ImageError.WRONG_FORMAT); }(); b.extend(this, {
                type: "", size: 0, width: 0, height: 0, setExif: function () { }, writeHeaders: function (a) { return a }, stripHeaders: function (a) { return a }, purge: function () {
                    a =
                    null
                }
            }); b.extend(this, k); this.purge = function () { k.purge(); k = null }
        }
    }); e("moxie/runtime/html5/image/MegaPixel", [], function () {
        function b(c) { var d = c.naturalWidth; if (1048576 < d * c.naturalHeight) { var g = document.createElement("canvas"); g.width = g.height = 1; g = g.getContext("2d"); g.drawImage(c, -d + 1, 0); return 0 === g.getImageData(0, 0, 1, 1).data[3] } return !1 } return {
            isSubsampled: b, renderTo: function (c, d, g) {
                var a = c.naturalWidth, e = c.naturalHeight, k = g.width, h = g.height, u = g.x || 0; g = g.y || 0; d = d.getContext("2d"); b(c) && (a /= 2, e /=
                2); var t = document.createElement("canvas"); t.width = t.height = 1024; var p = t.getContext("2d"), r; r = e; var v = document.createElement("canvas"); v.width = 1; v.height = r; v = v.getContext("2d"); v.drawImage(c, 0, 0); for (var v = v.getImageData(0, 0, 1, r).data, n = 0, x = r, z = r; z > n;) 0 === v[4 * (z - 1) + 3] ? x = z : n = z, z = x + n >> 1; r = z / r; r = 0 === r ? 1 : r; for (v = 0; v < e;) {
                    n = v + 1024 > e ? e - v : 1024; for (x = 0; x < a;) z = x + 1024 > a ? a - x : 1024, p.clearRect(0, 0, 1024, 1024), p.drawImage(c, -x, -v), d.drawImage(t, 0, 0, z, n, x * k / a + u << 0, v * h / e / r + g << 0, Math.ceil(z * k / a), Math.ceil(n * h / e / r)),
                    x += 1024; v += 1024
                }
            }
        }
    }); e("moxie/runtime/html5/image/Image", "moxie/runtime/html5/Runtime moxie/core/utils/Basic moxie/core/Exceptions moxie/core/utils/Encode moxie/file/Blob moxie/file/File moxie/runtime/html5/image/ImageInfo moxie/runtime/html5/image/MegaPixel moxie/core/utils/Mime moxie/core/utils/Env".split(" "), function (b, c, d, g, a, e, k, h, u, t) {
        return b.Image = function () {
            function b() { if (!A && !C) throw new d.ImageError(d.DOMException.INVALID_STATE_ERR); return A || C } function l(a) {
                return g.atob(a.substring(a.indexOf("base64,") +
                7))
            } function v(a) { var b = this; C = new Image; C.onerror = function () { z.call(this); b.trigger("error", d.ImageError.WRONG_FORMAT) }; C.onload = function () { b.trigger("load") }; C.src = "data:" == a.substr(0, 5) ? a : "data:" + (J.type || "") + ";base64," + g.btoa(a) } function n(a, b) { var c = this, f; if (window.FileReader) f = new FileReader, f.onload = function () { b(this.result) }, f.onerror = function () { c.trigger("error", d.ImageError.WRONG_FORMAT) }, f.readAsDataURL(a); else return b(a.getAsDataURL()) } function x(a, d, g, e) {
                var k, l = 0, m = 0, q, r; y = e; r = this.meta &&
                this.meta.tiff && this.meta.tiff.Orientation || 1; -1 !== c.inArray(r, [5, 6, 7, 8]) && (q = a, a = d, d = q); q = b(); g ? (a = Math.min(a, q.width), d = Math.min(d, q.height), k = Math.max(a / q.width, d / q.height)) : k = Math.min(a / q.width, d / q.height); if (!(1 < k && !g && e)) {
                    A || (A = document.createElement("canvas")); e = Math.round(q.width * k); k = Math.round(q.height * k); g ? (A.width = a, A.height = d, e > a && (l = Math.round((e - a) / 2)), k > d && (m = Math.round((k - d) / 2))) : (A.width = e, A.height = k); if (!y) {
                        a = A.width; d = A.height; switch (r) {
                            case 5: case 6: case 7: case 8: A.width = d; A.height =
                            a; break; default: A.width = a, A.height = d
                        } g = A.getContext("2d"); switch (r) { case 2: g.translate(a, 0); g.scale(-1, 1); break; case 3: g.translate(a, d); g.rotate(Math.PI); break; case 4: g.translate(0, d); g.scale(1, -1); break; case 5: g.rotate(.5 * Math.PI); g.scale(1, -1); break; case 6: g.rotate(.5 * Math.PI); g.translate(0, -d); break; case 7: g.rotate(.5 * Math.PI); g.translate(a, -d); g.scale(-1, 1); break; case 8: g.rotate(-.5 * Math.PI), g.translate(-a, 0) }
                    } r = A; l = -l; m = -m; "iOS" === t.OS ? h.renderTo(q, r, { width: e, height: k, x: l, y: m }) : r.getContext("2d").drawImage(q,
                    l, m, e, k); this.width = A.width; this.height = A.height; L = !0
                } this.trigger("Resize")
            } function z() { G && (G.purge(), G = null); E = C = A = J = null; L = !1 } var B = this, C, G, A, E, J, L = !1, y = !0; c.extend(this, {
                loadFromBlob: function (a) { var b = this, c = b.getRuntime(), f = 1 < arguments.length ? arguments[1] : !0; if (!c.can("access_binary")) throw new d.RuntimeError(d.RuntimeError.NOT_SUPPORTED_ERR); J = a; a.isDetached() ? (E = a.getSource(), v.call(this, E)) : n.call(this, a.getSource(), function (a) { f && (E = l(a)); v.call(b, a) }) }, loadFromImage: function (a, b) {
                    this.meta =
                    a.meta; J = new e(null, { name: a.name, size: a.size, type: a.type }); v.call(this, b ? E = a.getAsBinaryString() : a.getAsDataURL())
                }, getInfo: function () { var d = this.getRuntime(); !G && E && d.can("access_image_binary") && (G = new k(E)); d = { width: b().width || 0, height: b().height || 0, type: J.type || u.getFileMime(J.name), size: E && E.length || J.size || 0, name: J.name || "", meta: G && G.meta || this.meta || {} }; !d.meta || !d.meta.thumb || d.meta.thumb.data instanceof a || (d.meta.thumb.data = new a(null, { type: "image/jpeg", data: d.meta.thumb.data })); return d },
                downsize: function () { x.apply(this, arguments) }, getAsCanvas: function () { A && (A.id = this.uid + "_canvas"); return A }, getAsBlob: function (a, b) { a !== this.type && x.call(this, this.width, this.height, !1); return new e(null, { name: J.name || "", type: a, data: B.getAsBinaryString.call(this, a, b) }) }, getAsDataURL: function (a, b) { var d = b || 90; if (!L) return C.src; if ("image/jpeg" !== a) return A.toDataURL("image/png"); try { return A.toDataURL("image/jpeg", d / 100) } catch (c) { return A.toDataURL("image/jpeg") } }, getAsBinaryString: function (a, b) {
                    if (!L) return E ||
                    (E = l(B.getAsDataURL(a, b))), E; if ("image/jpeg" !== a) E = l(B.getAsDataURL(a, b)); else { var d; b || (b = 90); try { d = A.toDataURL("image/jpeg", b / 100) } catch (c) { d = A.toDataURL("image/jpeg") } E = l(d); G && (E = G.stripHeaders(E), y && (G.meta && G.meta.exif && G.setExif({ PixelXDimension: this.width, PixelYDimension: this.height }), E = G.writeHeaders(E)), G.purge(), G = null) } L = !1; return E
                }, destroy: function () { B = null; z.call(this); this.getRuntime().getShim().removeInstance(this.uid) }
            })
        }
    }); e("moxie/runtime/flash/Runtime", ["moxie/core/utils/Basic",
    "moxie/core/utils/Env", "moxie/core/utils/Dom", "moxie/core/Exceptions", "moxie/runtime/Runtime"], function (b, c, d, g, a) {
        function e() { var a; try { a = navigator.plugins["Shockwave Flash"], a = a.description } catch (b) { try { a = (new ActiveXObject("ShockwaveFlash.ShockwaveFlash")).GetVariable("$version") } catch (d) { a = "0.0" } } a = a.match(/\d+/g); return parseFloat(a[0] + "." + a[1]) } function k(a) {
            var b = d.get(a); b && "OBJECT" == b.nodeName && ("IE" === c.browser ? (b.style.display = "none", function r() {
                if (4 == b.readyState) {
                    var c = d.get(a); if (c) {
                        for (var f in c) "function" ==
                        typeof c[f] && (c[f] = null); c.parentNode.removeChild(c)
                    }
                } else setTimeout(r, 10)
            }()) : b.parentNode.removeChild(b))
        } var h = {}; a.addConstructor("flash", function (u) {
            var t = this, p; u = b.extend({ swf_url: c.swf_url }, u); a.call(this, u, "flash", {
                access_binary: function (a) { return a && "browser" === t.mode }, access_image_binary: function (a) { return a && "browser" === t.mode }, display_media: a.capTrue, do_cors: a.capTrue, drag_and_drop: !1, report_upload_progress: function () { return "client" === t.mode }, resize_image: a.capTrue, return_response_headers: !1,
                return_response_type: function (a) { return "json" === a && window.JSON ? !0 : !b.arrayDiff(a, ["", "text", "document"]) || "browser" === t.mode }, return_status_code: function (a) { return "browser" === t.mode || !b.arrayDiff(a, [200, 404]) }, select_file: a.capTrue, select_multiple: a.capTrue, send_binary_string: function (a) { return a && "browser" === t.mode }, send_browser_cookies: function (a) { return a && "browser" === t.mode }, send_custom_headers: function (a) { return a && "browser" === t.mode }, send_multipart: a.capTrue, slice_blob: function (a) {
                    return a &&
                    "browser" === t.mode
                }, stream_upload: function (a) { return a && "browser" === t.mode }, summon_file_dialog: !1, upload_filesize: function (a) { return 2097152 >= b.parseSizeStr(a) || "client" === t.mode }, use_http_method: function (a) { return !b.arrayDiff(a, ["GET", "POST"]) }
            }, {
                access_binary: function (a) { return a ? "browser" : "client" }, access_image_binary: function (a) { return a ? "browser" : "client" }, report_upload_progress: function (a) { return a ? "browser" : "client" }, return_response_type: function (a) {
                    return b.arrayDiff(a, ["", "text", "json", "document"]) ?
                    "browser" : ["client", "browser"]
                }, return_status_code: function (a) { return b.arrayDiff(a, [200, 404]) ? "browser" : ["client", "browser"] }, send_binary_string: function (a) { return a ? "browser" : "client" }, send_browser_cookies: function (a) { return a ? "browser" : "client" }, send_custom_headers: function (a) { return a ? "browser" : "client" }, stream_upload: function (a) { return a ? "client" : "browser" }, upload_filesize: function (a) { return 2097152 <= b.parseSizeStr(a) ? "client" : "browser" }
            }, "client"); 10 > e() && (MXI_DEBUG && c.debug.runtime && c.log("\tFlash didn't meet minimal version requirement (10)."),
            this.mode = !1); b.extend(this, {
                getShim: function () { return d.get(this.uid) }, shimExec: function (a, b) { var d = [].slice.call(arguments, 2); return t.getShim().exec(this.uid, a, b, d) }, init: function () {
                    var a, d, e; e = this.getShimContainer(); b.extend(e.style, { position: "absolute", top: "-8px", left: "-8px", width: "9px", height: "9px", overflow: "hidden" }); a = '<object id="' + this.uid + '" type="application/x-shockwave-flash" data="' + u.swf_url + '" '; "IE" === c.browser && (a += 'classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" '); a += 'width="100%" height="100%" style="outline:0"><param name="movie" value="' +
                    u.swf_url + '" /><param name="flashvars" value="uid=' + escape(this.uid) + "&target=" + c.global_event_dispatcher + '" /><param name="wmode" value="transparent" /><param name="allowscriptaccess" value="always" /></object>'; "IE" === c.browser ? (d = document.createElement("div"), e.appendChild(d), d.outerHTML = a, d = e = null) : e.innerHTML = a; p = setTimeout(function () { t && !t.initialized && (t.trigger("Error", new g.RuntimeError(g.RuntimeError.NOT_INIT_ERR)), MXI_DEBUG && c.debug.runtime && c.log("\tFlash failed to initialize within a specified period of time (typically 5s).")) },
                    5E3)
                }, destroy: function (a) { return function () { k(t.uid); a.call(t); clearTimeout(p); u = p = a = t = null } }(this.destroy)
            }, h)
        }); return h
    }); e("moxie/runtime/flash/file/FileInput", ["moxie/runtime/flash/Runtime", "moxie/file/File", "moxie/core/utils/Basic"], function (b, c, d) {
        return b.FileInput = {
            init: function (b) {
                var a = this, e = this.getRuntime(); this.bind("Change", function () { var b = e.shimExec.call(a, "FileInput", "getFiles"); a.files = []; d.each(b, function (b) { a.files.push(new c(e.uid, b)) }) }, 999); this.getRuntime().shimExec.call(this,
                "FileInput", "init", { name: b.name, accept: b.accept, multiple: b.multiple }); this.trigger("ready")
            }
        }
    }); e("moxie/runtime/flash/file/Blob", ["moxie/runtime/flash/Runtime", "moxie/file/Blob"], function (b, c) { return b.Blob = { slice: function (b, g, a, e) { var k = this.getRuntime(); 0 > g ? g = Math.max(b.size + g, 0) : 0 < g && (g = Math.min(g, b.size)); 0 > a ? a = Math.max(b.size + a, 0) : 0 < a && (a = Math.min(a, b.size)); (b = k.shimExec.call(this, "Blob", "slice", g, a, e || "")) && (b = new c(k.uid, b)); return b } } }); e("moxie/runtime/flash/file/FileReader", ["moxie/runtime/flash/Runtime",
    "moxie/core/utils/Encode"], function (b, c) { function d(b, a) { switch (a) { case "readAsText": return c.atob(b, "utf8"); case "readAsBinaryString": return c.atob(b); case "readAsDataURL": return b } return null } return b.FileReader = { read: function (b, a) { var c = this; c.result = ""; "readAsDataURL" === b && (c.result = "data:" + (a.type || "") + ";base64,"); c.bind("Progress", function (a, f) { f && (c.result += d(f, b)) }, 999); return c.getRuntime().shimExec.call(this, "FileReader", "readAsBase64", a.uid) } } }); e("moxie/runtime/flash/file/FileReaderSync",
    ["moxie/runtime/flash/Runtime", "moxie/core/utils/Encode"], function (b, c) { function d(b, a) { switch (a) { case "readAsText": return c.atob(b, "utf8"); case "readAsBinaryString": return c.atob(b); case "readAsDataURL": return b } return null } return b.FileReaderSync = { read: function (b, a) { var c; c = this.getRuntime().shimExec.call(this, "FileReaderSync", "readAsBase64", a.uid); if (!c) return null; "readAsDataURL" === b && (c = "data:" + (a.type || "") + ";base64," + c); return d(c, b, a.type) } } }); e("moxie/runtime/flash/xhr/XMLHttpRequest", "moxie/runtime/flash/Runtime moxie/core/utils/Basic moxie/file/Blob moxie/file/File moxie/file/FileReaderSync moxie/xhr/FormData moxie/runtime/Transporter".split(" "),
    function (b, c, d, g, a, e, k) {
        return b.XMLHttpRequest = {
            send: function (a, b) {
                function g() { a.transport = n.mode; n.shimExec.call(v, "XMLHttpRequest", "send", a, b) } function h(a, d) { n.shimExec.call(v, "XMLHttpRequest", "appendBlob", a, d.uid); b = null; g() } function l(a, b) { var d = new k; d.bind("TransportingComplete", function () { b(this.result) }); d.transport(a.getSource(), a.type, { ruid: n.uid }) } var v = this, n = v.getRuntime(); c.isEmptyObj(a.headers) || c.each(a.headers, function (a, b) {
                    n.shimExec.call(v, "XMLHttpRequest", "setRequestHeader",
                    b, a.toString())
                }); if (b instanceof e) { var x; b.each(function (a, b) { a instanceof d ? x = b : n.shimExec.call(v, "XMLHttpRequest", "append", b, a) }); if (b.hasBlob()) { var z = b.getBlob(); z.isDetached() ? l(z, function (a) { z.destroy(); h(x, a) }) : h(x, z) } else b = null, g() } else b instanceof d ? b.isDetached() ? l(b, function (a) { b.destroy(); b = a.uid; g() }) : (b = b.uid, g()) : g()
            }, getResponse: function (b) {
                var d, e, k = this.getRuntime(); if (e = k.shimExec.call(this, "XMLHttpRequest", "getResponseAsBlob")) {
                    e = new g(k.uid, e); if ("blob" === b) return e; try {
                        d =
                        new a; if (~c.inArray(b, ["", "text"])) return d.readAsText(e); if ("json" === b && window.JSON) return JSON.parse(d.readAsText(e))
                    } finally { e.destroy() }
                } return null
            }, abort: function (a) { this.getRuntime().shimExec.call(this, "XMLHttpRequest", "abort"); this.dispatchEvent("readystatechange"); this.dispatchEvent("abort") }
        }
    }); e("moxie/runtime/flash/runtime/Transporter", ["moxie/runtime/flash/Runtime", "moxie/file/Blob"], function (b, c) {
        return b.Transporter = {
            getAsBlob: function (b) {
                var g = this.getRuntime(); return (b = g.shimExec.call(this,
                "Transporter", "getAsBlob", b)) ? new c(g.uid, b) : null
            }
        }
    }); e("moxie/runtime/flash/image/Image", ["moxie/runtime/flash/Runtime", "moxie/core/utils/Basic", "moxie/runtime/Transporter", "moxie/file/Blob", "moxie/file/FileReaderSync"], function (b, c, d, g, a) {
        return b.Image = {
            loadFromBlob: function (a) {
                function b(a) { f.shimExec.call(c, "Image", "loadFromBlob", a.uid); c = f = null } var c = this, f = c.getRuntime(); if (a.isDetached()) {
                    var g = new d; g.bind("TransportingComplete", function () { b(g.result.getSource()) }); g.transport(a.getSource(),
                    a.type, { ruid: f.uid })
                } else b(a.getSource())
            }, loadFromImage: function (a) { return this.getRuntime().shimExec.call(this, "Image", "loadFromImage", a.uid) }, getInfo: function () { var a = this.getRuntime(), b = a.shimExec.call(this, "Image", "getInfo"); !b.meta || !b.meta.thumb || b.meta.thumb.data instanceof g || (b.meta.thumb.data = new g(a.uid, b.meta.thumb.data)); return b }, getAsBlob: function (a, b) { var d = this.getRuntime(), c = d.shimExec.call(this, "Image", "getAsBlob", a, b); return c ? new g(d.uid, c) : null }, getAsDataURL: function () {
                var b =
                this.getRuntime().Image.getAsBlob.apply(this, arguments); return b ? (new a).readAsDataURL(b) : null
            }
        }
    }); e("moxie/runtime/silverlight/Runtime", ["moxie/core/utils/Basic", "moxie/core/utils/Env", "moxie/core/utils/Dom", "moxie/core/Exceptions", "moxie/runtime/Runtime"], function (b, c, d, g, a) {
        function e(a) {
            var b = !1, d = null, c, f, g, k, h, l = 0; try {
                try { d = new ActiveXObject("AgControl.AgControl"), d.IsVersionSupported(a) && (b = !0) } catch (m) {
                    var q = navigator.plugins["Silverlight Plug-In"]; if (q) {
                        c = q.description; "1.0.30226.2" === c &&
                        (c = "2.0.30226.2"); for (f = c.split(".") ; 3 < f.length;) f.pop(); for (; 4 > f.length;) f.push(0); for (g = a.split(".") ; 4 < g.length;) g.pop(); do k = parseInt(g[l], 10), h = parseInt(f[l], 10), l++; while (l < g.length && k === h); k <= h && !isNaN(k) && (b = !0)
                    }
                }
            } catch (m) { b = !1 } return b
        } var k = {}; a.addConstructor("silverlight", function (h) {
            var u = this, t; h = b.extend({ xap_url: c.xap_url }, h); a.call(this, h, "silverlight", {
                access_binary: a.capTrue, access_image_binary: a.capTrue, display_media: a.capTrue, do_cors: a.capTrue, drag_and_drop: !1, report_upload_progress: a.capTrue,
                resize_image: a.capTrue, return_response_headers: function (a) { return a && "client" === u.mode }, return_response_type: function (a) { return "json" !== a ? !0 : !!window.JSON }, return_status_code: function (a) { return "client" === u.mode || !b.arrayDiff(a, [200, 404]) }, select_file: a.capTrue, select_multiple: a.capTrue, send_binary_string: a.capTrue, send_browser_cookies: function (a) { return a && "browser" === u.mode }, send_custom_headers: function (a) { return a && "client" === u.mode }, send_multipart: a.capTrue, slice_blob: a.capTrue, stream_upload: !0,
                summon_file_dialog: !1, upload_filesize: a.capTrue, use_http_method: function (a) { return "client" === u.mode || !b.arrayDiff(a, ["GET", "POST"]) }
            }, {
                return_response_headers: function (a) { return a ? "client" : "browser" }, return_status_code: function (a) { return b.arrayDiff(a, [200, 404]) ? "client" : ["client", "browser"] }, send_browser_cookies: function (a) { return a ? "browser" : "client" }, send_custom_headers: function (a) { return a ? "client" : "browser" }, use_http_method: function (a) {
                    return b.arrayDiff(a, ["GET", "POST"]) ? "client" : ["client",
                    "browser"]
                }
            }); e("2.0.31005.0") && "Opera" !== c.browser || (MXI_DEBUG && c.debug.runtime && c.log("\tSilverlight is not installed or minimal version (2.0.31005.0) requirement not met (not likely)."), this.mode = !1); b.extend(this, {
                getShim: function () { return d.get(this.uid).content.Moxie }, shimExec: function (a, b) { var d = [].slice.call(arguments, 2); return u.getShim().exec(this.uid, a, b, d) }, init: function () {
                    this.getShimContainer().innerHTML = '<object id="' + this.uid + '" data="data:application/x-silverlight," type="application/x-silverlight-2" width="100%" height="100%" style="outline:none;"><param name="source" value="' +
                    h.xap_url + '"/><param name="background" value="Transparent"/><param name="windowless" value="true"/><param name="enablehtmlaccess" value="true"/><param name="initParams" value="uid=' + this.uid + ",target=" + c.global_event_dispatcher + '"/></object>'; t = setTimeout(function () { u && !u.initialized && (u.trigger("Error", new g.RuntimeError(g.RuntimeError.NOT_INIT_ERR)), MXI_DEBUG && c.debug.runtime && c.log("Silverlight failed to initialize within a specified period of time (5-10s).")) }, "Windows" !== c.OS ? 1E4 : 5E3)
                }, destroy: function (a) {
                    return function () {
                        a.call(u);
                        clearTimeout(t); h = t = a = u = null
                    }
                }(this.destroy)
            }, k)
        }); return k
    }); e("moxie/runtime/silverlight/file/FileInput", ["moxie/runtime/silverlight/Runtime", "moxie/file/File", "moxie/core/utils/Basic"], function (b, c, d) {
        return b.FileInput = {
            init: function (b) {
                var a = this, e = this.getRuntime(); this.bind("Change", function () { var b = e.shimExec.call(a, "FileInput", "getFiles"); a.files = []; d.each(b, function (b) { a.files.push(new c(e.uid, b)) }) }, 999); this.getRuntime().shimExec.call(this, "FileInput", "init", function (a) {
                    for (var b = "", d =
                    0; d < a.length; d++) b += ("" !== b ? "|" : "") + a[d].title + " | *." + a[d].extensions.replace(/,/g, ";*."); return b
                }(b.accept), b.name, b.multiple); this.trigger("ready")
            }
        }
    }); e("moxie/runtime/silverlight/file/Blob", ["moxie/runtime/silverlight/Runtime", "moxie/core/utils/Basic", "moxie/runtime/flash/file/Blob"], function (b, c, d) { return b.Blob = c.extend({}, d) }); e("moxie/runtime/silverlight/file/FileDrop", ["moxie/runtime/silverlight/Runtime", "moxie/core/utils/Dom", "moxie/core/utils/Events"], function (b, c, d) {
        return b.FileDrop =
        { init: function () { var b = this.getRuntime(), a; a = b.getShimContainer(); d.addEvent(a, "dragover", function (a) { a.preventDefault(); a.stopPropagation(); a.dataTransfer.dropEffect = "copy" }, this.uid); d.addEvent(a, "dragenter", function (a) { a.preventDefault(); c.get(b.uid).dragEnter(a) && a.stopPropagation() }, this.uid); d.addEvent(a, "drop", function (a) { a.preventDefault(); c.get(b.uid).dragDrop(a) && a.stopPropagation() }, this.uid); return b.shimExec.call(this, "FileDrop", "init") } }
    }); e("moxie/runtime/silverlight/file/FileReader",
    ["moxie/runtime/silverlight/Runtime", "moxie/core/utils/Basic", "moxie/runtime/flash/file/FileReader"], function (b, c, d) { return b.FileReader = c.extend({}, d) }); e("moxie/runtime/silverlight/file/FileReaderSync", ["moxie/runtime/silverlight/Runtime", "moxie/core/utils/Basic", "moxie/runtime/flash/file/FileReaderSync"], function (b, c, d) { return b.FileReaderSync = c.extend({}, d) }); e("moxie/runtime/silverlight/xhr/XMLHttpRequest", ["moxie/runtime/silverlight/Runtime", "moxie/core/utils/Basic", "moxie/runtime/flash/xhr/XMLHttpRequest"],
    function (b, c, d) { return b.XMLHttpRequest = c.extend({}, d) }); e("moxie/runtime/silverlight/runtime/Transporter", ["moxie/runtime/silverlight/Runtime", "moxie/core/utils/Basic", "moxie/runtime/flash/runtime/Transporter"], function (b, c, d) { return b.Transporter = c.extend({}, d) }); e("moxie/runtime/silverlight/image/Image", ["moxie/runtime/silverlight/Runtime", "moxie/core/utils/Basic", "moxie/file/Blob", "moxie/runtime/flash/image/Image"], function (b, c, d, g) {
        return b.Image = c.extend({}, g, {
            getInfo: function () {
                var a = this.getRuntime(),
                b = ["tiff", "exif", "gps", "thumb"], g = { meta: {} }, e = a.shimExec.call(this, "Image", "getInfo"); e.meta && (c.each(b, function (a) { var b = e.meta[a], d, c, f, k; if (b && b.keys) for (g.meta[a] = {}, c = 0, f = b.keys.length; c < f; c++) if (d = b.keys[c], k = b[d]) /^(\d|[1-9]\d+) $/.test(k) ? k = parseInt(k, 10) : /^\d*\.\d+$/.test(k) && (k = parseFloat(k)), g.meta[a][d] = k }), !g.meta || !g.meta.thumb || g.meta.thumb.data instanceof d || (g.meta.thumb.data = new d(a.uid, g.meta.thumb.data))); g.width = parseInt(e.width, 10); g.height = parseInt(e.height, 10); g.size = parseInt(e.size,
                10); g.type = e.type; g.name = e.name; return g
            }
        })
    }); e("moxie/runtime/html4/Runtime", ["moxie/core/utils/Basic", "moxie/core/Exceptions", "moxie/runtime/Runtime", "moxie/core/utils/Env"], function (b, c, d, g) {
        var a = {}; d.addConstructor("html4", function (c) {
            var f = this, e = d.capTest, k = d.capTrue; d.call(this, c, "html4", {
                access_binary: e(window.FileReader || window.File && File.getAsDataURL), access_image_binary: !1, display_media: e(a.Image && (g.can("create_canvas") || g.can("use_data_uri_over32kb"))), do_cors: !1, drag_and_drop: !1, filter_by_extension: e("Chrome" ===
                g.browser && g.verComp(g.version, 28, ">=") || "IE" === g.browser && g.verComp(g.version, 10, ">=") || "Safari" === g.browser && g.verComp(g.version, 7, ">=")), resize_image: function () { return a.Image && f.can("access_binary") && g.can("create_canvas") }, report_upload_progress: !1, return_response_headers: !1, return_response_type: function (a) { return "json" === a && window.JSON ? !0 : !!~b.inArray(a, ["text", "document", ""]) }, return_status_code: function (a) { return !b.arrayDiff(a, [200, 404]) }, select_file: function () { return g.can("use_fileinput") },
                select_multiple: !1, send_binary_string: !1, send_custom_headers: !1, send_multipart: !0, slice_blob: !1, stream_upload: function () { return f.can("select_file") }, summon_file_dialog: function () { return f.can("select_file") && ("Firefox" === g.browser && g.verComp(g.version, 4, ">=") || "Opera" === g.browser && g.verComp(g.version, 12, ">=") || "IE" === g.browser && g.verComp(g.version, 10, ">=") || !!~b.inArray(g.browser, ["Chrome", "Safari"])) }, upload_filesize: k, use_http_method: function (a) { return !b.arrayDiff(a, ["GET", "POST"]) }
            }); b.extend(this,
            { init: function () { this.trigger("Init") }, destroy: function (a) { return function () { a.call(f); a = f = null } }(this.destroy) }); b.extend(this.getShim(), a)
        }); return a
    }); e("moxie/runtime/html4/file/FileInput", "moxie/runtime/html4/Runtime moxie/file/File moxie/core/utils/Basic moxie/core/utils/Dom moxie/core/utils/Events moxie/core/utils/Mime moxie/core/utils/Env".split(" "), function (b, c, d, g, a, e, k) {
        return b.FileInput = function () {
            function b() {
                var e = this, q = e.getRuntime(), n, x, z, B, C; C = d.guid("uid_"); n = q.getShimContainer();
                h && (x = g.get(h + "_form")) && d.extend(x.style, { top: "100%" }); z = document.createElement("form"); z.setAttribute("id", C + "_form"); z.setAttribute("method", "post"); z.setAttribute("enctype", "multipart/form-data"); z.setAttribute("encoding", "multipart/form-data"); d.extend(z.style, { overflow: "hidden", position: "absolute", top: 0, left: 0, width: "100%", height: "100%" }); B = document.createElement("input"); B.setAttribute("id", C); B.setAttribute("type", "file"); B.setAttribute("name", p.name || "Filedata"); B.setAttribute("accept", l.join(","));
                d.extend(B.style, { fontSize: "999px", opacity: 0 }); z.appendChild(B); n.appendChild(z); d.extend(B.style, { position: "absolute", top: 0, left: 0, width: "100%", height: "100%" }); "IE" === k.browser && k.verComp(k.version, 10, "<") && d.extend(B.style, { filter: "progid:DXImageTransform.Microsoft.Alpha(opacity=0)" }); B.onchange = function () {
                    var a; if (this.value) {
                        if (this.files) { if (a = this.files[0], 0 === a.size) { z.parentNode.removeChild(z); return } } else a = { name: this.value }; a = new c(q.uid, a); this.onchange = function () { }; b.call(e); e.files = [a];
                        B.setAttribute("id", a.uid); z.setAttribute("id", a.uid + "_form"); e.trigger("change"); B = z = null
                    }
                }; q.can("summon_file_dialog") && (n = g.get(p.browse_button), a.removeEvent(n, "click", e.uid), a.addEvent(n, "click", function (a) { B && !B.disabled && B.click(); a.preventDefault() }, e.uid)); h = C; n = x = n = null
            } var h, l = [], p; d.extend(this, {
                init: function (d) {
                    var c = this, f = c.getRuntime(), k; p = d; l = d.accept.mimes || e.extList2mimes(d.accept, f.can("filter_by_extension")); k = f.getShimContainer(); (function () {
                        var b, e; b = g.get(d.browse_button);
                        f.can("summon_file_dialog") && ("static" === g.getStyle(b, "position") && (b.style.position = "relative"), e = parseInt(g.getStyle(b, "z-index"), 10) || 1, b.style.zIndex = e, k.style.zIndex = e - 1); b = f.can("summon_file_dialog") ? b : k; a.addEvent(b, "mouseover", function () { c.trigger("mouseenter") }, c.uid); a.addEvent(b, "mouseout", function () { c.trigger("mouseleave") }, c.uid); a.addEvent(b, "mousedown", function () { c.trigger("mousedown") }, c.uid); a.addEvent(g.get(d.container), "mouseup", function () { c.trigger("mouseup") }, c.uid); b = null
                    })();
                    b.call(this); k = null; c.trigger({ type: "ready", async: !0 })
                }, disable: function (a) { var b; if (b = g.get(h)) b.disabled = !!a }, destroy: function () { var b = this.getRuntime(), d = b.getShim(), b = b.getShimContainer(); a.removeAllEvents(b, this.uid); a.removeAllEvents(p && g.get(p.container), this.uid); a.removeAllEvents(p && g.get(p.browse_button), this.uid); b && (b.innerHTML = ""); d.removeInstance(this.uid); h = l = p = null }
            })
        }
    }); e("moxie/runtime/html4/file/FileReader", ["moxie/runtime/html4/Runtime", "moxie/runtime/html5/file/FileReader"], function (b,
    c) { return b.FileReader = c }); e("moxie/runtime/html4/xhr/XMLHttpRequest", "moxie/runtime/html4/Runtime moxie/core/utils/Basic moxie/core/utils/Dom moxie/core/utils/Url moxie/core/Exceptions moxie/core/utils/Events moxie/file/Blob moxie/xhr/FormData".split(" "), function (b, c, d, g, a, e, k, h) {
        return b.XMLHttpRequest = function () {
            function b(a) {
                var c = this, f, g, k, h = !1; if (r) {
                    f = r.id.replace(/_iframe$/, ""); if (f = d.get(f + "_form")) {
                        g = f.getElementsByTagName("input"); for (k = g.length; k--;) switch (g[k].getAttribute("type")) {
                            case "hidden": g[k].parentNode.removeChild(g[k]);
                                break; case "file": h = !0
                        } g = []; h || f.parentNode.removeChild(f); f = null
                    } setTimeout(function () { e.removeEvent(r, "load", c.uid); r.parentNode && r.parentNode.removeChild(r); var b = c.getRuntime().getShimContainer(); b.children.length || b.parentNode.removeChild(b); r = null; a() }, 1)
                }
            } var l, p, r; c.extend(this, {
                send: function (v, n) {
                    var x = this, z = x.getRuntime(), B, C, G, A; l = p = null; if (n instanceof h && n.hasBlob()) { if (A = n.getBlob(), B = A.uid, G = d.get(B), C = d.get(B + "_form"), !C) throw new a.DOMException(a.DOMException.NOT_FOUND_ERR); } else B =
                    c.guid("uid_"), C = document.createElement("form"), C.setAttribute("id", B + "_form"), C.setAttribute("method", v.method), C.setAttribute("enctype", "multipart/form-data"), C.setAttribute("encoding", "multipart/form-data"), z.getShimContainer().appendChild(C); C.setAttribute("target", B + "_iframe"); n instanceof h && n.each(function (a, b) { if (a instanceof k) G && G.setAttribute("name", b); else { var d = document.createElement("input"); c.extend(d, { type: "hidden", name: b, value: a }); G ? C.insertBefore(d, G) : C.appendChild(d) } }); C.setAttribute("action",
                    v.url); (function () {
                        var a = z.getShimContainer() || document.body, d = document.createElement("div"); d.innerHTML = '<iframe id="' + B + '_iframe" name="' + B + '_iframe" src="javascript:&quot;&quot;" style="display:none"></iframe>'; r = d.firstChild; a.appendChild(r); e.addEvent(r, "load", function () {
                            var a; try {
                                a = r.contentWindow.document || r.contentDocument || window.frames[r.id].document, /^4(0[0-9]|1[0-7]|2[2346])\s/.test(a.title) ? l = a.title.replace(/^(\d+).*$/, "$1") : (l = 200, p = c.trim(a.body.innerHTML), x.trigger({
                                    type: "progress",
                                    loaded: p.length, total: p.length
                                }), A && x.trigger({ type: "uploadprogress", loaded: A.size || 1025, total: A.size || 1025 }))
                            } catch (d) { if (g.hasSameOrigin(v.url)) l = 404; else { b.call(x, function () { x.trigger("error") }); return } } b.call(x, function () { x.trigger("load") })
                        }, x.uid)
                    })(); C.submit(); x.trigger("loadstart")
                }, getStatus: function () { return l }, getResponse: function (a) { if ("json" === a && "string" === c.typeOf(p) && window.JSON) try { return JSON.parse(p.replace(/^\s*<pre[^>]*>/, "").replace(/<\/pre>\s*$/, "")) } catch (b) { return null } return p },
                abort: function () { var a = this; r && r.contentWindow && (r.contentWindow.stop ? r.contentWindow.stop() : r.contentWindow.document.execCommand ? r.contentWindow.document.execCommand("Stop") : r.src = "about:blank"); b.call(this, function () { a.dispatchEvent("abort") }) }
            })
        }
    }); e("moxie/runtime/html4/image/Image", ["moxie/runtime/html4/Runtime", "moxie/runtime/html5/image/Image"], function (b, c) { return b.Image = c }); (function (e) {
        for (var f = 0; f < e.length; f++) {
            for (var d = b, g = e[f], a = g.split(/[.\/]/), h = 0; h < a.length - 1; ++h) d[a[h]] === c && (d[a[h]] =
            {}), d = d[a[h]]; d[a[a.length - 1]] = k[g]
        }
    })("moxie/core/utils/Basic moxie/core/utils/Env moxie/core/I18n moxie/core/utils/Mime moxie/core/utils/Dom moxie/core/Exceptions moxie/core/EventTarget moxie/runtime/Runtime moxie/runtime/RuntimeClient moxie/file/FileInput moxie/core/utils/Encode moxie/file/Blob moxie/file/File moxie/file/FileDrop moxie/file/FileReader moxie/core/utils/Url moxie/runtime/RuntimeTarget moxie/file/FileReaderSync moxie/xhr/FormData moxie/xhr/XMLHttpRequest moxie/runtime/Transporter moxie/image/Image moxie/core/utils/Events".split(" "))
})(this);
(function (b) { var c = {}, h = b.moxie.core.utils.Basic.inArray; (function k(b) { var f, d; for (f in b) d = typeof b[f], "object" !== d || ~h(f, ["Exceptions", "Env", "Mime"]) ? "function" === d && (c[f] = b[f]) : k(b[f]) })(b.moxie); c.Env = b.moxie.core.utils.Env; c.Mime = b.moxie.core.utils.Mime; c.Exceptions = b.moxie.core.Exceptions; b.mOxie = c; b.o || (b.o = c); return c })(this); function CssStyle(b) { this.nameTag = b } CssStyle.prototype.nameTag; CssStyle.prototype.closeTag = function () { return "</" + this.nameTag + ">" }; CssStyle.prototype.setTag = function (b) { this.nameTag = b }; CssStyle.prototype.makeStyle = function (b, c) { "undefined" == typeof this.nameTag && (this.nameTag = "div"); return "<" + this.nameTag + " style='" + b + "'>" + c };
CssStyle.prototype.makeStyleWithIdOrClass = function (b, c, h, e) { "undefined" == typeof this.nameTag && (this.nameTag = "div"); return "id" == b ? "<" + this.nameTag + " id='" + c + "' style='" + h + "'>" + e : "<" + this.nameTag + " class='" + c + "' style='" + h + "'>" + e }; CssStyle.prototype.makeStyleWithIdAndClass = function (b, c, h, e) { "undefined" == typeof this.nameTag && (this.nameTag = "div"); return "<" + this.nameTag + " class='" + b + "' id='" + c + "' style='" + h + "'>" + e };
CssStyle.prototype.insertSpaceChar = function (b) { output = ""; for (i = 0; i < b; i++) output += "&nbsp;"; return output }; (function (b, c, h) {
    function e(b) {
        function c(a, b, d) { var f = { chunks: "slice_blob", jpgresize: "send_binary_string", pngresize: "send_binary_string", progress: "report_upload_progress", multi_selection: "select_multiple", dragdrop: "drag_and_drop", drop_element: "drag_and_drop", headers: "send_custom_headers", urlstream_upload: "send_binary_string", canSendBinary: "send_binary", triggerDialog: "summon_file_dialog" }; f[a] ? e[f[a]] = b : d || (e[a] = b) } var a = b.required_features, e = {}; if ("string" === typeof a) f.each(a.split(/\s*,\s*/), function (a) {
            c(a,
            !0)
        }); else if ("object" === typeof a) f.each(a, function (a, b) { c(b, a) }); else if (!0 === a) { 0 < b.chunk_size && (e.slice_blob = !0); if (b.resize.enabled || !b.multipart) e.send_binary_string = !0; f.each(b, function (a, b) { c(b, !!a, !0) }) } return e
    } var k = b.setTimeout, l = {}, f = {
        VERSION: "2.1.8", STOPPED: 1, STARTED: 2, QUEUED: 1, UPLOADING: 2, FAILED: 4, DONE: 5, GENERIC_ERROR: -100, HTTP_ERROR: -200, IO_ERROR: -300, SECURITY_ERROR: -400, INIT_ERROR: -500, FILE_SIZE_ERROR: -600, FILE_EXTENSION_ERROR: -601, FILE_DUPLICATE_ERROR: -602, IMAGE_FORMAT_ERROR: -700,
        MEMORY_ERROR: -701, IMAGE_DIMENSIONS_ERROR: -702, mimeTypes: c.mimes, ua: c.ua, typeOf: c.typeOf, extend: c.extend, guid: c.guid, get: function (b) { var f = [], a; "array" !== c.typeOf(b) && (b = [b]); for (var e = b.length; e--;) (a = c.get(b[e])) && f.push(a); return f.length ? f : null }, each: c.each, getPos: c.getPos, getSize: c.getSize, xmlEncode: function (b) { var c = { "<": "lt", ">": "gt", "&": "amp", '"': "quot", "'": "#39" }, a = /[<>&\"\']/g; return b ? ("" + b).replace(a, function (a) { return c[a] ? "&" + c[a] + ";" : a }) : b }, toArray: c.toArray, inArray: c.inArray, addI18n: c.addI18n,
        translate: c.translate, isEmptyObj: c.isEmptyObj, hasClass: c.hasClass, addClass: c.addClass, removeClass: c.removeClass, getStyle: c.getStyle, addEvent: c.addEvent, removeEvent: c.removeEvent, removeAllEvents: c.removeAllEvents, cleanName: function (b) {
            var c, a; a = [/[\300-\306]/g, "A", /[\340-\346]/g, "a", /\307/g, "C", /\347/g, "c", /[\310-\313]/g, "E", /[\350-\353]/g, "e", /[\314-\317]/g, "I", /[\354-\357]/g, "i", /\321/g, "N", /\361/g, "n", /[\322-\330]/g, "O", /[\362-\370]/g, "o", /[\331-\334]/g, "U", /[\371-\374]/g, "u"]; for (c = 0; c < a.length; c +=
            2) b = b.replace(a[c], a[c + 1]); b = b.replace(/\s+/g, "_"); return b = b.replace(/[^a-z0-9_\-\.]+/gi, "")
        }, buildUrl: function (b, c) { var a = ""; f.each(c, function (b, d) { a += (a ? "&" : "") + encodeURIComponent(d) + "=" + encodeURIComponent(b) }); a && (b += (0 < b.indexOf("?") ? "&" : "?") + a); return b }, formatSize: function (b) {
            function c(a, b) { return Math.round(a * Math.pow(10, b)) / Math.pow(10, b) } if (b === h || /\D/.test(b)) return f.translate("N/A"); var a = Math.pow(1024, 4); return b > a ? c(b / a, 1) + " " + f.translate("tb") : b > (a /= 1024) ? c(b / a, 1) + " " + f.translate("gb") :
            b > (a /= 1024) ? c(b / a, 1) + " " + f.translate("mb") : 1024 < b ? Math.round(b / 1024) + " " + f.translate("kb") : b + " " + f.translate("b")
        }, parseSize: c.parseSizeStr, predictRuntime: function (b, g) { var a, e; a = new f.Uploader(b); e = c.Runtime.thatCan(a.getOption().required_features, g || b.runtimes); a.destroy(); return e }, addFileFilter: function (b, c) { l[b] = c }
    }; f.addFileFilter("mime_types", function (b, c, a) {
        b.length && !b.regexp.test(c.name) ? (this.trigger("Error", { code: f.FILE_EXTENSION_ERROR, message: f.translate("File extension error."), file: c }),
        a(!1)) : a(!0)
    }); f.addFileFilter("max_file_size", function (b, c, a) { b = f.parseSize(b); void 0 !== c.size && b && c.size > b ? (this.trigger("Error", { code: f.FILE_SIZE_ERROR, message: f.translate("File size error."), file: c }), a(!1)) : a(!0) }); f.addFileFilter("prevent_duplicates", function (b, c, a) { if (b) for (b = this.files.length; b--;) if (c.name === this.files[b].name && c.size === this.files[b].size) { this.trigger("Error", { code: f.FILE_DUPLICATE_ERROR, message: f.translate("Duplicate file error."), file: c }); a(!1); return } a(!0) }); f.Uploader =
    function (b) {
        function g() { var a, b = 0, d; if (this.state == f.STARTED) { for (d = 0; d < I.length; d++) a || I[d].status != f.QUEUED ? b++ : (a = I[d], this.trigger("BeforeUpload", a) && (a.status = f.UPLOADING, this.trigger("UploadFile", a))); b == I.length && (this.state !== f.STOPPED && (this.state = f.STOPPED, this.trigger("StateChanged")), this.trigger("UploadComplete", I)) } } function a(a) { a.percent = 0 < a.size ? Math.ceil(a.loaded / a.size * 100) : 100; q() } function q() {
            var a, b; F.reset(); for (a = 0; a < I.length; a++) b = I[a], b.size !== h ? (F.size += b.origSize, F.loaded +=
            b.loaded * b.origSize / b.size) : F.size = h, b.status == f.DONE ? F.uploaded++ : b.status == f.FAILED ? F.failed++ : F.queued++; F.size === h ? F.percent = 0 < I.length ? Math.ceil(F.uploaded / I.length * 100) : 0 : (F.bytesPerSec = Math.ceil(F.loaded / ((+new Date - Q || 1) / 1E3)), F.percent = 0 < F.size ? Math.ceil(F.loaded / F.size * 100) : 0)
        } function m() { var a = M[0] || O[0]; return a ? a.getRuntime().uid : !1 } function w(a, b) { if (a.ruid) { var d = c.Runtime.getInfo(a.ruid); if (d) return d.can(b) } return !1 } function u() {
            this.bind("FilesAdded FilesRemoved", function (a) {
                a.trigger("QueueChanged");
                a.refresh()
            }); this.bind("CancelUpload", B); this.bind("BeforeUpload", v); this.bind("UploadFile", n); this.bind("UploadProgress", x); this.bind("StateChanged", z); this.bind("QueueChanged", q); this.bind("Error", G); this.bind("FileUploaded", C); this.bind("Destroy", A)
        } function t(a, b) {
            var d = this, g = 0, e = [], k = { runtime_order: a.runtimes, required_caps: a.required_features, preferred_caps: P, swf_url: a.flash_swf_url, xap_url: a.silverlight_xap_url }; f.each(a.runtimes.split(/\s*,\s*/), function (b) { a[b] && (k[b] = a[b]) }); a.browse_button &&
            f.each(a.browse_button, function (b) {
                e.push(function (e) {
                    var h = new c.FileInput(f.extend({}, k, { accept: a.filters.mime_types, name: a.file_data_name, multiple: a.multi_selection, container: a.container, browse_button: b })); h.onready = function () { var a = c.Runtime.getInfo(this.ruid); c.extend(d.features, { chunks: a.can("slice_blob"), multipart: a.can("send_multipart"), multi_selection: a.can("select_multiple") }); g++; M.push(this); e() }; h.onchange = function () { d.addFile(this.files) }; h.bind("mouseenter mouseleave mousedown mouseup",
                    function (d) { N || (a.browse_button_hover && ("mouseenter" === d.type ? c.addClass(b, a.browse_button_hover) : "mouseleave" === d.type && c.removeClass(b, a.browse_button_hover)), a.browse_button_active && ("mousedown" === d.type ? c.addClass(b, a.browse_button_active) : "mouseup" === d.type && c.removeClass(b, a.browse_button_active))) }); h.bind("mousedown", function () { d.trigger("Browse") }); h.bind("error runtimeerror", function () { h = null; e() }); h.init()
                })
            }); a.drop_element && f.each(a.drop_element, function (a) {
                e.push(function (b) {
                    var e = new c.FileDrop(f.extend({},
                    k, { drop_zone: a, inIframe: E, editor_id: J })); e.onready = function () { var a = c.Runtime.getInfo(this.ruid); d.features.dragdrop = a.can("drag_and_drop"); g++; O.push(this); b() }; e.ondrop = function () { d.addFile(this.files) }; e.bind("error runtimeerror", function () { e = null; b() }); e.init()
                })
            }); c.inSeries(e, function () { "function" === typeof b && b(g) })
        } function p(a, b, d) {
            var f = new c.Image; try {
                f.onload = function () {
                    if (b.width > this.width && b.height > this.height && b.quality === h && b.preserve_headers && !b.crop) return this.destroy(), d(a); f.downsize(b.width,
                    b.height, b.crop, b.preserve_headers)
                }, f.onresize = function () { d(this.getAsBlob(a.type, b.quality)); this.destroy() }, f.onerror = function () { d(a) }, f.load(a)
            } catch (g) { d(a) }
        } function r(a, b, d) {
            function g(a, b, d) {
                var c = y[a]; switch (a) {
                    case "max_file_size": "max_file_size" === a && (y.max_file_size = y.filters.max_file_size = b); break; case "chunk_size": if (b = f.parseSize(b)) y[a] = b, y.send_file_name = !0; break; case "multipart": y[a] = b; b || (y.send_file_name = !0); break; case "unique_names": if (y[a] = b) y.send_file_name = !0; break; case "filters": "array" ===
                    f.typeOf(b) && (b = { mime_types: b }); d ? f.extend(y.filters, b) : y.filters = b; b.mime_types && (y.filters.mime_types.regexp = function (a) { var b = []; f.each(a, function (a) { f.each(a.extensions.split(/,/), function (a) { /^\s*\*\s*$/.test(a) ? b.push("\\.*") : b.push("\\." + a.replace(new RegExp("[" + "/^$.*+?|()[]{}\\".replace(/./g, "\\$&") + "]", "g"), "\\$&")) }) }); return new RegExp("(" + b.join("|") + ")$", "i") }(y.filters.mime_types)); break; case "resize": d ? f.extend(y.resize, b, { enabled: !0 }) : y.resize = b; break; case "prevent_duplicates": y.prevent_duplicates =
                    y.filters.prevent_duplicates = !!b; break; case "browse_button": case "drop_element": b = f.get(b); case "container": case "runtimes": case "multi_selection": case "flash_swf_url": case "silverlight_xap_url": y[a] = b; d || (h = !0); break; default: y[a] = b
                } d || k.trigger("OptionChanged", a, b, c)
            } var k = this, h = !1; "object" === typeof a ? f.each(a, function (a, b) { g(b, a, d) }) : g(a, b, d); d ? (y.required_features = e(f.extend({}, y)), P = e(f.extend({}, y, { required_features: !0 }))) : h && (k.trigger("Destroy"), t.call(k, y, function (a) {
                a ? (k.runtime = c.Runtime.getInfo(m()).type,
                k.trigger("Init", { runtime: k.runtime }), k.trigger("PostInit")) : k.trigger("Error", { code: f.INIT_ERROR, message: f.translate("Init error.") })
            }))
        } function v(a, b) { if (a.settings.unique_names) { var d = b.name.match(/\.([^.]+)$/), c = "part"; d && (c = d[1]); b.target_name = b.id + "." + c } } function n(a, b) {
            function d() { 0 < l-- ? k(g, 1E3) : (b.loaded = q, a.trigger("Error", { code: f.HTTP_ERROR, message: f.translate("HTTP Error."), file: b, response: D.responseText, status: D.status, responseHeaders: D.getAllResponseHeaders() })) } function g() {
                var p,
                r, u = {}, t; b.status === f.UPLOADING && a.state !== f.STOPPED && (a.settings.send_file_name && (u.name = b.target_name || b.name), h && m.chunks && n.size > h ? (t = Math.min(h, n.size - q), p = n.slice(q, q + t)) : (t = n.size, p = n), h && m.chunks && (a.settings.send_chunk_number ? (u.chunk = Math.ceil(q / h), u.chunks = Math.ceil(n.size / h)) : (u.offset = q, u.total = n.size)), D = new c.XMLHttpRequest, D.upload && (D.upload.onprogress = function (d) { b.loaded = Math.min(b.size, q + d.loaded); a.trigger("UploadProgress", b) }), D.onload = function () {
                    400 <= D.status ? d() : (l = a.settings.max_retries,
                    t < n.size ? (p.destroy(), q += t, b.loaded = Math.min(q, n.size), a.trigger("ChunkUploaded", b, { offset: b.loaded, total: n.size, response: D.responseText, status: D.status, responseHeaders: D.getAllResponseHeaders() }), "Android Browser" === c.Env.browser && a.trigger("UploadProgress", b)) : b.loaded = b.size, p = r = null, !q || q >= n.size ? (b.size != b.origSize && (n.destroy(), n = null), a.trigger("UploadProgress", b), b.status = f.DONE, a.trigger("FileUploaded", b, { response: D.responseText, status: D.status, responseHeaders: D.getAllResponseHeaders() })) :
                    k(g, 1))
                }, D.onerror = function () { d() }, D.onloadend = function () { this.destroy(); D = null }, a.settings.multipart && m.multipart ? (D.open("post", e, !0), f.each(a.settings.headers, function (a, b) { D.setRequestHeader(b, a) }), r = new c.FormData, f.each(f.extend(u, a.settings.multipart_params), function (a, b) { r.append(b, a) }), r.append(a.settings.file_data_name, p), D.send(r, { runtime_order: a.settings.runtimes, required_caps: a.settings.required_features, preferred_caps: P, swf_url: a.settings.flash_swf_url, xap_url: a.settings.silverlight_xap_url })) :
                (e = f.buildUrl(a.settings.url, f.extend(u, a.settings.multipart_params)), D.open("post", e, !0), D.setRequestHeader("Content-Type", "application/octet-stream"), f.each(a.settings.headers, function (a, b) { D.setRequestHeader(b, a) }), D.send(p, { runtime_order: a.settings.runtimes, required_caps: a.settings.required_features, preferred_caps: P, swf_url: a.settings.flash_swf_url, xap_url: a.settings.silverlight_xap_url })))
            } var e = a.settings.url, h = a.settings.chunk_size, l = a.settings.max_retries, m = a.features, q = 0, n; b.loaded && (q = b.loaded =
            h ? h * Math.floor(b.loaded / h) : 0); n = b.getSource(); a.settings.resize.enabled && w(n, "send_binary_string") && ~c.inArray(n.type, ["image/jpeg", "image/png"]) ? p.call(this, n, a.settings.resize, function (a) { n = a; b.size = a.size; g() }) : g()
        } function x(b, d) { a(d) } function z(a) { if (a.state == f.STARTED) Q = +new Date; else if (a.state == f.STOPPED) for (var b = a.files.length - 1; 0 <= b; b--) a.files[b].status == f.UPLOADING && (a.files[b].status = f.QUEUED, q()) } function B() { D && D.abort() } function C(a) { q(); k(function () { g.call(a) }, 1) } function G(b, d) {
            d.code ===
            f.INIT_ERROR ? b.destroy() : d.code === f.HTTP_ERROR && (d.file.status = f.FAILED, a(d.file), b.state == f.STARTED && (b.trigger("CancelUpload"), k(function () { g.call(b) }, 1)))
        } function A(a) { a.stop(); f.each(I, function (a) { a.destroy() }); I = []; M.length && (f.each(M, function (a) { a.destroy() }), M = []); O.length && (f.each(O, function (a) { a.destroy() }), O = []); P = {}; N = !1; Q = D = null; F.reset() } var E = b.inIframe || !1, J = b.editor_id || "", L = f.guid(), y, I = [], P = {}, M = [], O = [], Q, F, N = !1, D; y = {
            runtimes: c.Runtime.order, max_retries: 0, chunk_size: 0, multipart: !0,
            editor_id: "", inIframe: !1, multi_selection: !0, file_data_name: "file", flash_swf_url: "js/Moxie.swf", silverlight_xap_url: "js/Moxie.xap", filters: { mime_types: [], prevent_duplicates: !1, max_file_size: 0 }, resize: { enabled: !1, preserve_headers: !0, crop: !1 }, send_file_name: !0, send_chunk_number: !0
        }; r.call(this, b, null, !0); F = new f.QueueProgress; f.extend(this, {
            id: L, uid: L, state: f.STOPPED, features: {}, runtime: null, files: I, settings: y, total: F, init: function () {
                var a = this; "function" == typeof y.preinit ? y.preinit(a) : f.each(y.preinit,
                function (b, d) { a.bind(d, b) }); u.call(this); y.browse_button && y.url ? t.call(this, y, function (b) { "function" == typeof y.init ? y.init(a) : f.each(y.init, function (b, d) { a.bind(d, b) }); b ? (a.runtime = c.Runtime.getInfo(m()).type, a.trigger("Init", { runtime: a.runtime }), a.trigger("PostInit")) : a.trigger("Error", { code: f.INIT_ERROR, message: f.translate("Init error.") }) }) : this.trigger("Error", { code: f.INIT_ERROR, message: f.translate("Init error.") })
            }, setOption: function (a, b) { r.call(this, a, b, !this.runtime) }, getOption: function (a) {
                return a ?
                y[a] : y
            }, refresh: function () { M.length && f.each(M, function (a) { a.trigger("Refresh") }); this.trigger("Refresh") }, start: function () { this.state != f.STARTED && (this.state = f.STARTED, this.trigger("StateChanged"), g.call(this)) }, stop: function () { this.state != f.STOPPED && (this.state = f.STOPPED, this.trigger("StateChanged"), this.trigger("CancelUpload")) }, disableBrowse: function (a) { N = a !== h ? a : !0; M.length && f.each(M, function (a) { a.disable(N) }); this.trigger("DisableBrowse", N) }, getFile: function (a) {
                var b; for (b = I.length - 1; 0 <= b; b--) if (I[b].id ===
                a) return I[b]
            }, addFile: function (a, b) {
                function d(a, b) { var f = []; c.each(e.settings.filters, function (b, d) { l[d] && f.push(function (c) { l[d].call(e, b, a, function (a) { c(!a) }) }) }); c.inSeries(f, b) } function g(a) {
                    var l = c.typeOf(a); if (a instanceof c.File) { if (!a.ruid && !a.isDetached()) { if (!n) return !1; a.ruid = n; a.connectRuntime(n) } g(new f.File(a)) } else a instanceof c.Blob ? (g(a.getSource()), a.destroy()) : a instanceof f.File ? (b && (a.name = b), h.push(function (b) {
                        d(a, function (d) {
                            d || (I.push(a), q.push(a), e.trigger("FileFiltered",
                            a)); k(b, 1)
                        })
                    })) : -1 !== c.inArray(l, ["file", "blob"]) ? g(new c.File(null, a)) : "node" === l && "filelist" === c.typeOf(a.files) ? c.each(a.files, g) : "array" === l && (b = null, c.each(a, g))
                } var e = this, h = [], q = [], n; n = m(); g(a); h.length && c.inSeries(h, function () { q.length && e.trigger("FilesAdded", q) })
            }, removeFile: function (a) { a = "string" === typeof a ? a : a.id; for (var b = I.length - 1; 0 <= b; b--) if (I[b].id === a) return this.splice(b, 1)[0] }, splice: function (a, b) {
                var d = I.splice(a === h ? 0 : a, b === h ? I.length : b), c = !1; this.state == f.STARTED && (f.each(d,
                function (a) { if (a.status === f.UPLOADING) return c = !0, !1 }), c && this.stop()); this.trigger("FilesRemoved", d); f.each(d, function (a) { a.destroy() }); c && this.start(); return d
            }, dispatchEvent: function (a) { var b, d; a = a.toLowerCase(); if (b = this.hasEventListener(a)) { b.sort(function (a, b) { return b.priority - a.priority }); d = [].slice.call(arguments); d.shift(); d.unshift(this); for (var c = 0; c < b.length; c++) if (!1 === b[c].fn.apply(b[c].scope, d)) return !1 } return !0 }, bind: function (a, b, d, c) { f.Uploader.prototype.bind.call(this, a, b, c, d) },
            destroy: function () { this.trigger("Destroy"); y = F = null; this.unbindAll() }
        })
    }; f.Uploader.prototype = c.EventTarget.instance; f.File = function () {
        var b = {}; return function (g) {
            f.extend(this, {
                id: f.guid(), name: g.name || g.fileName, type: g.type || "", size: g.size || g.fileSize, origSize: g.size || g.fileSize, loaded: 0, percent: 0, status: f.QUEUED, lastModifiedDate: g.lastModifiedDate || (new Date).toLocaleString(), getNative: function () { var a = this.getSource().getSource(); return -1 !== c.inArray(c.typeOf(a), ["blob", "file"]) ? a : null }, getSource: function () {
                    return b[this.id] ?
                    b[this.id] : null
                }, destroy: function () { var a = this.getSource(); a && (a.destroy(), delete b[this.id]) }
            }); b[this.id] = g
        }
    }(); f.QueueProgress = function () { var b = this; b.size = 0; b.loaded = 0; b.uploaded = 0; b.failed = 0; b.queued = 0; b.percent = 0; b.bytesPerSec = 0; b.reset = function () { b.size = b.loaded = b.uploaded = b.failed = b.queued = b.percent = b.bytesPerSec = 0 } }; b.plupload = f
})(window, mOxie); AttachmentBox = function (b, c, h, e, k, l) { this._compose = b; AttachmentBox.ID++; this._id = "attachElement" + share.INDEX + AttachmentBox.ID; $("<span id='" + this._id + "'></span>").appendTo($(c)); $("#" + this._id).addClass("attachItem").css("font-size", "12px").css("padding", "2px 4px 2px 7px").css("padding-left", "4").css("display", "inline-block"); this._icon = h; this._filename = e; this._readonly = k; this._isFwd = l || !1 }; AttachmentBox.ID = 0; AttachmentBox.prototype.setPart = function (b) { this._part = b };
AttachmentBox.prototype.getPart = function () { return this._part }; AttachmentBox.prototype.getFileName = function () { return this._filename };
AttachmentBox.prototype.render = function () {
    var b = this, c = "a" + share.INDEX + AttachmentBox.ID, h = $('<a  href="" style="right:1px;position:absolute; top:1px;"></a>'); $(h).append("" + (this._readonly ? "" : '<span style="background-image:url(icon1/icon_close.png); height:16px; width:16px; text-indent: -9999px; overflow:hidden;display:block;margin-top:-1px">close</span>')); var e = $('<a href="#" id="' + c + '">' + this._filename + "</a>"); $("#" + this._id).addClass("attachmentbox").append(60 < this._filename.length ? this._filename.substring(0,
    57) + "..." : this._readonly ? e : this._filename).append(h); this._readonly ? ($(h).css("display", "block").click(function () { b._compose.downloadFile(b._part, b._filename) }), $("#" + c).attr("href", "https://" + window.location.host + "/service/home/~/" + this._filename + "?auth=co&loc=en_US&id=" + this._compose._mid + "&part=" + this._part + "&disp=a")) : $(h).css("display", "block").click(function (c) { c.preventDefault(); console.log("_this._isFwd :" + b._isFwd); b._compose.removeAttachFile(b._part, b._isFwd); $("#" + b._id).remove(); b._compose.updateHeight() })
}; jQuery.fn.selectSendOther = function (b, c, h) {
    var e = "ulsendother_" + c, k = this, l = $('<ul class="ulsendother" id="' + e + '"></ul>'); $("body").prepend(l); $(l).hide(); jQuery.each(b, function (b, d) {
        var c = $("#selectAccountAddress" + h).val(), a = ""; if (d.accountAddress == Session.getInstance().getUser()) { var e = "<img src='giaodien/BmailChat.png' style='width:16px;height:16px;float:left;margin-top:2px;margin-right:5px;' /> G\u1eedi b\u1eb1ng t\u00e0i kho\u1ea3n ch\u00ednh"; "" == c && (a = "selected") } else -1 != d.accountAddress.toLowerCase().indexOf("@yahoo") ?
        e = "<img src='giaodien/yahoo.png' style='width:16px;height:16px;float:left;margin-top:2px;margin-right:5px;'  /> G\u1eedi b\u1eb1ng t\u00e0i kho\u1ea3n Yahoo" : -1 != d.accountAddress.toLowerCase().indexOf("@gmail") && (e = "<img src='giaodien/gtalk.png' style='width:16px;height:16px;float:left;margin-top:2px;margin-right:5px;'  /> G\u1eedi b\u1eb1ng t\u00e0i kho\u1ea3n Gmail"); $(l).append('<li><a href="#"  title = "< ' + d.accountAddress + ' >" class="' + a + '" val="' + d.accountAddress + '" name="' + d.accountAddress +
        '">' + e + "</a></li>")
    }); $(k).bind("click", function (b) { b.preventDefault(); "none" != $(l).css("display") ? $(l).css("display", "none") : $(l).show(); $(l).css({ top: $(k).offset().top + $(k).height() + 4, left: $(k).offset().left - 80 }); $(this).blur(); return !1 }); $(l).find("a").click(function () { var b = $(this).attr("val"); $("#selectAccountAddress" + h).val(b); $("#selectAccountName" + h).val(b); $("#tb_composeMail" + h).click(); $(l).hide(); return !1 }); $(document).bind("click", function (b) {
        $("#" + e) && "none" != $("#" + e).css("display") &&
        $(b.target).attr("id") != c && $("#" + e).css("display", "none")
    })
};
function ComposeModel(b) {
    this.mailId = b.mailId || ""; this.rowId = b.rowId || ""; this.type = b.composeType; this.folderId = b.folderId || ""; this.folderPath = b.folderPath || ""; this.loadFromBpr = b.loadFromBpr || !1; this.selectFolder = this.isAttaching = !1; this.message = null; this.imagePart = []; this.imageMailId = []; this.imageName = []; this.imageInline = []; this.currentImg = null; this.updateData = new Event(this); this.insertAttach = new Event(this); this.attachFail = new Event(this); this.startSendMail = new Event(this); this.sendMailFail = new Event(this);
    this.sendMailComplete = new Event(this); this.insertImageFail = new Event(this); this.insertImageSuccess = new Event(this); this.uploadCompleteEvent = new Event(this)
} ComposeModel.prototype.idMailDraft = ""; ComposeModel.prototype.dontNeedHandleFaultResponse = !1; ComposeModel.prototype.idMailFwd = ""; ComposeModel.prototype.partMailFwd = []; ComposeModel.prototype.idnt = ""; ComposeModel.prototype.irt = ""; ComposeModel.prototype.midPart = []; ComposeModel.prototype.aidArray = []; ComposeModel.prototype.fileList = [];
ComposeModel.prototype.toApplet = ""; ComposeModel.prototype.ccApplet = ""; ComposeModel.prototype.bccApplet = ""; ComposeModel.prototype.beginPartAttach = ""; ComposeModel.prototype.attachNameApplet = ""; ComposeModel.prototype.subjectApplet = ""; ComposeModel.prototype.contentApplet = ""; ComposeModel.prototype.cidApplet = ""; ComposeModel.prototype.formatApplet = "text/html"; ComposeModel.prototype.oldContent = ""; ComposeModel.prototype.xhr = null;
ComposeModel.prototype.getTabName = function () { var b = newTabSoanTxt; "reply" == this.type ? b = replyButtonMsg : "replyall" == this.type || "postandreplyall" == this.type ? b = replyAllTabMsg : "forward" == this.type ? b = forwardButtonMsg : "readmail" == this.type && (b = readMailMsg); return b }; ComposeModel.prototype.getTabId = function () { var b = "composeMail"; "reply" == this.type ? b = "replyTab" : "replyall" == this.type ? b = "replyAllTab" : "forward" == this.type ? b = "forwardTab" : "readmail" == this.type && (b = "readMailTab"); return b };
ComposeModel.prototype.getMailId = function () { var b = this.mailId || ""; b || (b = dataTable.getRecordSet().getRecord(this.rowId).getData().id); return b };
ComposeModel.prototype.getMail = function () {
    if ("" != this.mailId) {
        var b = MailCache.getInstance().getMessageByRowId(this.rowId), c = this, h = function (b) {
            var e = dataTable.getRecordSet().getRecord(c.rowId); e && Util.markRead(e, !0); dataTable.setCacheReplyMail(b); c.message = new Message(b); c.folderId = c.message.getFolderId(); b = TreeController.getInstance().getNodeByProperty("labelElId", c.folderId); b || (b = mailbox.getInstance().getShareFolderId(c.folderId), c.folderId = b, b = TreeController.getInstance().getNodeByProperty("labelElId",
            c.folderId)); c.folderPath = getLocation(b); var f = getTo(), d = getCc(); b = getSubject(); var g = getFullName(), e = getContent(), a = getSendTime(), h = getFrom(); if ("reply" == c.type) { -1 == b.indexOf("Re: ") && (b = "Re: " + b); d && (f += d); var e = c.getContentReply(e), e = c.getStringReply(h, g, a, f, b) + "</div><br>" + e, e = c.insertSignature(e), m = { to: h, subject: b, content: e } } else if ("replyall" == c.type) {
                -1 == b.indexOf("Re: ") && (b = "Re: " + b); d && (f += d); e = c.getContentReply(e); e = c.getStringReply(h, g, a, f, b) + "</div><br>" + e; e = c.insertSignature(e); m = f.split(";");
                if (1 < m.length) { d = !1; for (f = 0; f < m.length; f++) m[f] == h && (d = !0), m[f].toUpperCase() == Session.getInstance().getUser().toUpperCase() && m.splice(f, 1); d || m.unshift(h); f = m.join(";") } m = { to: f, subject: b, content: e }
            } else if ("forward" == c.type) {
                -1 == b.indexOf("Fwd: ") && (b = "Fwd: " + b); d = getPart(); e = c.getContentReply(e); e = c.getStringReply(h, g, a, f, b) + "</div><br>" + e; e = c.insertSignature(e); if (0 < d.length) for (c.idMailFwd = getId(), console.log(c.idMailFwd), h = getFileName(), f = 0; f < d.length; f++) c.partMailFwd.push(d[f]), m = {
                    part: d[f],
                    index: f, fileName: h[f], isFwd: !0
                }, c.insertAttach.notify(m); m = { subject: b, content: e }
            } else if ("editasnew" == c.type) m = { subject: b, content: e }; else if ("readmail" == c.type) d = getPart(), e = c.getContentReply(e), m = { subject: b, content: e, to: f, fullName: g, from: h, part: d }; else if ("postfolder" == c.type || "postandreplyall" == c.type) -1 == b.indexOf("Re: ") && (b = "Re: " + b), e = c.getContentReply(e), e = c.getStringReply(h, g, a, f, b) + "</div><br>" + e, e = c.insertSignature(e), m = { subject: b, folderPath: c.folderPath, content: e }; c.updateData.notify(m)
        };
        if (b) b = b.get("Data"), h(b); else { var b = new XMLElement("GetMsgRequest"), e = b.addElement("m"); e.addAttribute("id", this.mailId); e.addAttribute("html", "1"); b = new Soap(Session.getInstance().getUser(), b, "urn:zimbraMail", !0, sessionId, seqNotify); b.isNeedAbort(!1); b.sendRequestCallback(h) }
    } else { if ("postfolder" == this.type || "postandreplyall" == this.type) h = TreeController.getInstance().getHighlightedNode(), this.folderId = h.labelElId, this.folderPath = getLocation(h); this.getDataMail() }
};
ComposeModel.prototype.getSignature = function () {
    var b = "";
    "FALSE" != currentUser.bmailUseSignature
    ? b = currentUser.signatureContent
    : null == currentUser.signatureContent
    && (b = "<a href='mailto:" + Session.getInstance().getUser() + "' >" + Session.getInstance().getUser() + "</a>");
    return b;
};
ComposeModel.prototype.getStringReply = function (b, c, h, e, k) { var l = sendFromHeaderMsg, f = sentAtHeaderMsg, d = sentToHeaderMsg, g = subjectHeaderMsg, a = "<div style='font-size: 14px'><br><span style='color: rgb(139, 0, 0);'><i> ----- " + oldContentMsg + " : -----</i></span>", a = a + (c ? l + (c + "</u>[" + b + "]</span>") : l + (b + "</u>[" + b + "]</span>")), a = a + (f + h), a = a + (d + e); return a += g + k };
ComposeModel.prototype.insertSignature = function (b) { var c = ""; "FALSE" != currentUser.bmailUseSignature ? "TRUE" == currentUser.addSignatureReply && (c = "top" == currentUser.signatureStyle ? "<br><div>" + currentUser.signatureContent + "</div>" + b : b + "<br><div>" + currentUser.signatureContent + "</div>") : null == currentUser.signatureContent && (c = "<a href='mailto:" + Session.getInstance().getUser() + "' >" + Session.getInstance().getUser() + "</a>", c = b + "<br><div>" + c + "</div>"); c || (c = b); return c };
ComposeModel.prototype.getContentReply = function (b) { -1 != b.indexOf("dfsrc") && (b = b.replace(/dfsrc/gi, "src")); var c = []; this.message && (c = this.message.getAttachInline()); if (0 < c.length) for (var h = 0; h < c.length; h++) { var e = c[h].path, k = c[h].ci; -1 != b.indexOf(k) && (b = b.replace(new RegExp(k, "g"), e)) } return b }; ComposeModel.prototype.getDataMail = function () { var b = this.getSignature(), b = "<br><div>" + b + "</div>"; "" != b && this.updateData.notify({ content: b, folderPath: this.folderPath }) };
ComposeModel.prototype.saveDraft = function (b) {
    var c = !1; b.notify && (c = !0); var h = []; b && b.attachId && (h = b.attachId); var e = b.isInsertImage || !1, k = b.multipart, l = b.emailTo, f = b.subject; 0 != currentUser.timeoutSession && window.clearTimeout(mailbox.getInstance().timeout); var d = ""; if (h) { var g = h.length; if (1 == g) var a = h[0].length, d = d + h[0].substring(1, a - 1); else for (var q = 0; q < g; q++) a = h[q].length, 0 == q && (h[q] = h[q].substring(1, a)), q == g - 1 && (h[q] = h[q].substring(0, a - 1)), h[q] += ",", d += h[q] } h = Session.getInstance().getIpServer();
    a = Session.getInstance().getUser(); q = Session.getInstance().getAuthen(); h = location.protocol + "//" + h + "/service/soap"; g = "<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'><soap:Header><context xmlns='urn:zimbra'>"; g += "<format type='js'/>"; g += "<account by='name'>" + a + "</account>"; g += "<authToken>" + q + "</authToken>"; g += "</context>"; g += "</soap:Header>"; g += "<soap:Body>"; g += "<SaveDraftRequest xmlns='urn:zimbraMail'>"; g = this.idMailDraft && "" != this.idMailDraft ? g + ("<m id='" + this.idMailDraft + "' idnt='" +
    this.idnt + "'>") : g + "<m>"; l && (g += l); g += "<su>" + f + "</su>"; k && (g += k); if (this.midPart && 0 < this.midPart.length) { g = d ? g + ("<attach aid='" + d + "'>") : g + "<attach>"; k = this.midPart.length; if (this.idMailDraft) for (l = 0; l < k; l++) g += "<mp mid='" + this.idMailDraft + "' part='" + this.midPart[l] + "' />"; g += "</attach>" } else d && (g += "<attach aid='" + d + "'></attach>"); this.irt && (g += "<irt>" + this.irt + "</irt>"); var g = g + "</m>", g = g + "</SaveDraftRequest>", g = g + "</soap:Body>", g = g + "</soap:Envelope>", m = getRequest(), w = this; m.onreadystatechange = function () {
        if (4 ==
        m.readyState && 200 == m.status) { var a = eval("(" + m.responseText + ")"); e ? w.handleInsertImage(w, a) : w.handleSaveDraft(w, a, c) } else 4 == m.readyState && 200 != m.status && (e ? new MessageBox("Th\u00f4ng b\u00e1o", "Hi\u1ec7n t\u1ea1i kh\u00f4ng \u0111\u00ednh k\u00e8m \u1ea3nh \u0111\u01b0\u1ee3c. B\u1ea1n vui l\u00f2ng th\u1eed l\u1ea1i") : b && b.attachId ? ($("#wprocess" + w.orderTab).css("display", "none"), new MessageBox("Th\u00f4ng b\u00e1o", "Hi\u1ec7n t\u1ea1i kh\u00f4ng \u0111\u00ednh k\u00e8m \u0111\u01b0\u1ee3c. B\u1ea1n vui l\u00f2ng th\u1eed l\u1ea1i")) :
        new MessageBox("Th\u00f4ng b\u00e1o", "Hi\u1ec7n t\u1ea1i kh\u00f4ng l\u01b0u nh\u00e1p \u0111\u01b0\u1ee3c. B\u1ea1n vui l\u00f2ng th\u1eed l\u1ea1i"))
    }; m.open("POST", h, !0, "SaveDraftRequest"); m.send(g)
};
ComposeModel.prototype.handleInsertImage = function (b, c) {
    if (c.Body.SaveDraftResponse && c.Body.SaveDraftResponse.m && c.Body.SaveDraftResponse.m[0]) {
        if (this.imageMailId.push(c.Body.SaveDraftResponse.m[0].id), c.Body.SaveDraftResponse.m[0].mp[0].mp) {
            for (var h = c.Body.SaveDraftResponse.m[0].mp[0].mp.length, e = 0; e < h; e++) if (c.Body.SaveDraftResponse.m[0].mp[0].mp[e].cd && "attachment" == c.Body.SaveDraftResponse.m[0].mp[0].mp[e].cd) {
                var k = c.Body.SaveDraftResponse.m[0].mp[0].mp[e].part; this.imagePart.push(k); var l =
                c.Body.SaveDraftResponse.m[0].mp[0].mp[e].filename; this.imageName.push(l)
            } this.insertImageSuccess.notify({ part: k, fileName: l, mailId: c.Body.SaveDraftResponse.m[0].id })
        }
    } else this.insertImageFail.notify()
};
ComposeModel.prototype.handleSaveDraft = function (b, c, h) {
    b._attachBoxes = []; var e = []; e.push(""); if (c.Body.SaveDraftResponse && c.Body.SaveDraftResponse.m && c.Body.SaveDraftResponse.m[0]) {
        this.uploadCompleteEvent.notify(); b.midPart = []; b.idMailDraft = c.Body.SaveDraftResponse.m[0].id; c.Body.SaveDraftResponse.m[0].idnt && (b.idnt = c.Body.SaveDraftResponse.m[0].idnt); c.Body.SaveDraftResponse.m[0].mid && (b.irt = c.Body.SaveDraftResponse.m[0].mid); "" != b.irt && (b.irt = b.irt.substring(1, b.irt.length - 1)); b._cidApplet = c.Body.SaveDraftResponse.m[0].id;
        if (c.Body.SaveDraftResponse.m[0].mp[0].mp) for (var k = c.Body.SaveDraftResponse.m[0].mp[0].mp.length, l = 0, f = 0; f < k; f++) if (c.Body.SaveDraftResponse.m[0].mp[0].mp[f].cd && "attachment" == c.Body.SaveDraftResponse.m[0].mp[0].mp[f].cd) {
            var d = c.Body.SaveDraftResponse.m[0].mp[0].mp[f].part; 0 == l && (b._beginPartAttach = c.Body.SaveDraftResponse.m[0].mp[0].mp[f].part); e.push(d); var g = c.Body.SaveDraftResponse.m[0].mp[0].mp[f].filename; b._attachNameApplet += g; 0 < f && f < k - 1 && (b._attachNameApplet += "/"); b.midPart.push(d); d = {
                part: d,
                index: f, fileName: g
            }; b.insertAttach.notify(d); l++; b.isAttaching = !1
        } h && (b = new ActionDoneMsg("resources/common/saveDraftMsg.png"), b.init(), b.show())
    } else this.uploadCompleteEvent.notify(), h || (d = { response: c }, b.isAttaching = !1, b.attachFail.notify(d))
};
ComposeModel.prototype.updateContactCache = function (b, c) { var h = b.length, e = ""; if (0 < h) for (var k = 0; k < h; k++) { console.log("1: " + b[k]); var l = b[k].replace(/^\s*/, "").replace(/\s*$/, ""); "" != l && " " != l && (console.log("2: " + b[k]), l = String(l).replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;"), -1 == l.indexOf("@") && (l = l + "@" + share.domain), e += '{"t":"' + c + '","a":"' + l + '"}', e += ",", currentUser.updateContact(b[k])) } return e };
ComposeModel.prototype.createAttachTag = function (b) { var c = ""; if (this.idMailDraft && 0 < b.length) for (var c = '"attach":{"mp":[', h = 0; h < b.length; h++) { var e = '{"mid":"' + this.idMailDraft + '","part":"' + b[h] + '"}', c = c + e; h < b.length - 1 && (c += ",") } if ("" != this.idMailFwd && 0 < this.partMailFwd.length) for (c = "" == c ? '"attach":{"mp":[' : c + ",", h = 0; h < this.partMailFwd.length; h++) e = '{"mid":"' + this.idMailFwd + '","part":"' + this.partMailFwd[h] + '"}', c += e, h < this.partMailFwd.length - 1 && (c += ","); "" != c && (c += "]}"); return c };
ComposeModel.prototype.createAttachInlineTag = function () {
    var b = "", c = []; this.message && (c = this.message.getAttachInline()); if (0 < c.length) for (var b = ",", h = c.length, e = 0; e < h; e++) { var k = "", k = c[e].fullCid, l = c[e].id, f = c[e].part, k = '{"ci":"' + k + '","attach":{"mp":[', k = k + ('{"mid":"' + l + '","part":"' + f + '"}'), k = k + "]}}", b = b + k; e < h - 1 && (b += ",") } if (0 < this.imageInline.length) for (b += ",", h = this.imageInline.length, e = 0; e < h; e++) k = this.imageInline[e].cId, l = this.imageInline[e].mailId, f = this.imageInline[e].part, k = '{"ci":"' + k + '","attach":{"mp":[',
    k += '{"mid":"' + l + '","part":"' + f + '"}', k += "]}}", b += k, e < h - 1 && (b += ","); return b
}; ComposeModel.prototype.sendInviteMsg = function (b) { };
ComposeModel.prototype.replaceImageInline = function (b) { if (0 < this.imagePart.length) for (var c = Session.getInstance().getAuthen(), h = Session.getInstance().getIpServer(), e = this.imagePart.length, k = 0; k < e; k++) { var l = this.imageMailId[k], f = this.imagePart[k], d = location.protocol + "//" + h + "/service/home/~/" + this.imageName[k] + "?authToken=" + c + "&id=" + l + "&part=" + f, d = d.replace(/&/g, "&amp;"); if (-1 != b.indexOf(d)) { var g = l + f; b = b.replace(d, "cid:" + g); this.imageInline.push({ cId: g, part: f, mailId: l }) } } return b };
ComposeModel.prototype.sendMsg = function (b) {
    var c = b.content,
        h = b.part,
        e = b.subject,
        e = String(e).replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;"),
        k = b.noCheckComposeType || !1;
    this.startSendMail.notify();
    var l = [];
    this.message && (l = this.message.getAttachInline());
    if (0 < l.length)
        for (var f = 0; f < l.length; f++) {
            var d = l[f].path, g = l[f].ci, d = d.replace(/&/g, "&amp;");
            -1 != c.indexOf(d) && (c = c.replaceAll(d, g))
        }
    c = this.removeBreaks(c);
    c = this.replaceImageInline(c);
    -1 != c.indexOf('"') && (c = c.replace(/"/gi, '\\"'));
    -1 != c.indexOf("\\") && (c = c.replace(/\\/gi, "\\"));
    l = "https://" + Session.getInstance().getIpServer() + "/service/soap";
    f = Session.getInstance().getUser(); d = Session.getInstance().getAuthen();
    "postfolder" != this.type
    && "postandreplyall" != this.type
    || k ? g = this.updateContactCache(b.toArr, "t") : (g = f.split("@")[1],
    g = this.locdau(this.folderPath) + "@" + g,
    g = '{"t":"t","a":"..' + g + '"},');
    var a = b.ccArr,
        q = this.updateContactCache(a, "c"),
        m = b.bccArr,
        w = this.updateContactCache(m, "b");
    if ("postfolder" == this.type || "postandreplyall" == this.type)
        if ("" != q || "" != w) {
            var u = m; 0 == a.length && (a = m, u = []);
            this.sendMsg({
                emailTo: b.emailTo,
                toArr: a,
                ccArr: [],
                bccArr: u,
                content: b.content,
                frDisplayName: b.fromDisplayName,
                frAddress: b.fromAddress,
                part: b.part,
                subject: b.subject,
                textPlain: b.textPlain,
                noCheckComposeType: !0
            })
        }
    var m = "",
        a = this.createAttachTag(h),
        u = this.createAttachInlineTag(),
        t = Session.getInstance().getUser(),
        p = currentUser.displayName;
    b.frDisplayName && (p = b.frDisplayName);
    b.frAddress && (t = b.frAddress);
    p ||
    (p = t); b = b.textPlain; -1 != b.indexOf('"')
    && (b = b.replace(/"/gi, '\\"'));
    -1 != b.indexOf("\\")
    && (b = b.replace(/\\/gi, "\\"));
    b = b.replace(/\n/gi, "\\r\\n");
    b = this.removeBreaks(b);
    -1 != e.indexOf('"')
    && (e = e.replace(/"/gi, '\\"'));
    -1 != e.indexOf("\\")
    && (e = e.replace(/\\/gi, "\\"));
    var r = "SendMsgRequest";
    "postfolder" != this.type
    && "postandreplyall" != this.type
    || k
    || (r = "SendMailHereRequest");
    var v = "";
    "postfolder" != this.type
    && "postandreplyall" != this.type
    || k
    || (v = '},"l":{"_content":"' + this.folderId + '"');
    "" != a && (a = "," + a);
    m += '{"Header":{"context":{"_jsns":"urn:zimbra","account":{"_content":"' +
    f + '","by":"name"},"authToken":"' + d + '"}},';
    m = this.idMailDraft
        && 0 < h.length
        ? m + ('"Body":{"'
        + r + '":{"_jsns":"urn:zimbraMail","m":{"id":"'
        + this.idMailDraft
        + '","e":[' + g + q + w + '{"t":"f","a":"' + t + '","p":"' + p + '"}],"su":{"_content":"' + e + '"}' + a + ',"mp":[{"ct":"multipart/alternative","mp":[{"ct":"text/plain","content":{"_content":"' + b + '"}},{"ct":"multipart/related","mp":[{"ct":"text/html","content":{"_content":"' + c + '"}}' + u + "]}]}]" + v + "}}}}") : m + ('"Body":{"' + r + '":{"_jsns":"urn:zimbraMail","m":{"e":[' + g + q + w + '{"t":"f","a":"' +
    t + '","p":"' + p + '"}],"su":{"_content":"' + e + '"}' + a + ',"mp":[{"ct":"multipart/alternative","mp":[{"ct":"text/plain","content":{"_content":"' + b + '"}},{"ct":"multipart/related","mp":[{"ct":"text/html","content":{"_content":"' + c + '"}}' + u + "]}]}]" + v + "}}}}"); this.xhr = getRequest(); var n = this; this.xhr.onreadystatechange = function () {
        if (4 == n.xhr.readyState && 200 == n.xhr.status) {
            var a = eval("(" + n.xhr.responseText + ")"); -1 != n.xhr.responseText.indexOf("service.AUTH_EXPIRED") && share.logout(); var b = !1; "postfolder" == n.type ||
            "postandreplyall" == n.type ? a.Body && a.Body.SendMailHereResponse && (b = !0) : a.Body && a.Body.SendMsgResponse && a.Body.SendMsgResponse.m && (b = !0); if (b) { if (n.sendMailComplete.notify(), 0 < h.length && "" != n.idMailDraft && n.deleteM(n.idMailDraft), 0 < n.imageMailId.length) for (a = 0; a < n.imageMailId.length; a++) n.deleteM(n.imageMailId[a]) } else -1 != n.xhr.responseText.indexOf("mail.SEND_ABORTED_ADDRESS_FAILURE") ? new MessageBox(errorDlgMsg, errorInvalidReciever) : new MessageBox(errorDlgMsg, sendErrMsg), n.sendMailFail.notify()
        } else 4 !=
        n.xhr.readyState || 200 == n.xhr.status || n.dontNeedHandleFaultResponse || (-1 != n.xhr.responseText.indexOf("mail.SEND_ABORTED_ADDRESS_FAILURE") ? new MessageBox(errorDlgMsg, errorInvalidReciever) : new MessageBox(errorDlgMsg, sendErrMsg), n.sendMailFail.notify())
    }; console.log(m); this.xhr.open("POST", l, !0, r); this.xhr.send(m)
};
ComposeModel.prototype.deleteM = function (b) {
    var c = Session.getInstance().getUser(), h = Session.getInstance().getAuthen(), e = Session.getInstance().getIpServer(), e = location.protocol + "//" + e + "/service/soap", k; k = "<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'><soap:Header><context xmlns='urn:zimbra'>"; k += "<format type='js'/>"; k += "<account by='name'>" + c + "</account>"; k += "<authToken >" + h + "</authToken>"; k += "</context>"; k += "</soap:Header>"; k += "<soap:Body>"; k += "<MsgActionRequest xmlns='urn:zimbraMail'>";
    k += "<action id='" + b + "' op='delete' />"; k += "</MsgActionRequest>"; k += "</soap:Body>"; k += "</soap:Envelope>"; b = getRequest(); b.onreadystatechange = function () { }; b.open("POST", e, !0, "MsgActionRequest"); b.send(k)
};
ComposeModel.prototype.locdau = function (b) {
    b = b.replace(/\u00e0|\u00e1|\u1ea1|\u1ea3|\u00e3|\u00e2|\u1ea7|\u1ea5|\u1ead|\u1ea9|\u1eab|\u0103|\u1eb1|\u1eaf|\u1eb7|\u1eb3|\u1eb5/gi, "a"); b = b.replace(/\u00e8|\u00e9|\u1eb9|\u1ebb|\u1ebd|\u00ea|\u1ec1|\u1ebf|\u1ec7|\u1ec3|\u1ec5/gi, "e"); b = b.replace(/\u00ec|\u00ed|\u1ecb|\u1ec9|\u0129/gi, "i"); b = b.replace(/\u00f2|\u00f3|\u1ecd|\u1ecf|\u00f5|\u00f4|\u1ed3|\u1ed1|\u1ed9|\u1ed5|\u1ed7|\u01a1|\u1edd|\u1edb|\u1ee3|\u1edf|\u1ee1/gi, "o"); b = b.replace(/\u00f9|\u00fa|\u1ee5|\u1ee7|\u0169|\u01b0|\u1eeb|\u1ee9|\u1ef1|\u1eed|\u1eef/gi,
    "u"); b = b.replace(/\u1ef3|\u00fd|\u1ef5|\u1ef7|\u1ef9/gi, "y"); b = b.replace(/\u0111/gi, "d"); b = b.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, " "); b = b.replace(/ + /g, ""); b = b.replace(/^\ +|\ +$/g, ""); return b = b.replace(/ /g, "")
}; ComposeModel.prototype.removeBreaks = function (b) { b = b.replace(/(\r\n|\n|\r)/gm, "<1br />"); b = b.replace(/<1br \/><1br \/>/gi, " "); b = b.replace(/\<1br \/>/gi, " "); b = b.replace(/\s+/g, " "); return b = b.replace(/<2br \/>/gi, "\n\n") }; function ComposeView(b, c) { this._model = b; this.table = c || dataTable; this.dontSetActiveTab = !1; this.sendMsg = new Event(this); this.saveDraftBtnClick = new Event(this); this.eSignatureBtnClick = new Event(this); this.ccBtnClick = new Event(this); this.bccBtnClick = new Event(this); this.createAutoComplete = new Event(this) } ComposeView.prototype.widthCompose = 0; ComposeView.prototype.heightCompose = 0; ComposeView.prototype.counter = 1; ComposeView.prototype.orderTab = 0; ComposeView.prototype.layout = null;
ComposeView.prototype.style = null; ComposeView.prototype.attachFail = !1; ComposeView.prototype.tab = null; ComposeView.prototype.toolbar = null; ComposeView.prototype.sending = !1; ComposeView.prototype.tblSelectAddress = null; ComposeView.prototype.tblAddress = null; ComposeView.prototype.layoutId = ""; ComposeView.prototype.toolbarId = "";
ComposeView.prototype.init = function () { this.orderTab = ++share.INDEX; this.style = new CssStyle("div"); this.layoutId = "layoutComposeMail" + this.orderTab; this.toolbarId = "inboxToolbarComposeMail" + this.orderTab; this.widthCompose = YAHOO.util.Dom.getClientWidth() - 200; this.heightCompose = YAHOO.util.Dom.getClientHeight() - 24; this.createTab(this._model.getTabId()) };
ComposeView.prototype.createTab = function (b) {
    var c = this._model.getTabName(); this.removeComposeTab(); if ("readmail" == this._model.type) c = '<div class="hoverTab"></div><div id="composeLabel' + this.orderTab + '"><span id="composeImage" style=""></span><span style="margin-top:5px;"><span style="position:absolute;left:35px;margin-top:6px;color: #6FAEEE;" class="labelSoan" id="labelSoan' + this.orderTab + '" >' + c + '</span><div id="closeTab' + this.orderTab + '" class="closeTab" name="' + this._model.getMailId() + '"><img id="closesubtab' +
    this.orderTab + '" class="closeSubTab" name="So\u1ea1n" src="icon1/Closeinactive.png"></div></span></div>'; else {
        var h = "color:#6FAEEE"; mailbox.getInstance().isEgov && (h = "color:black !important"); c = '<div class="hoverTab"></div><div class="sendingDiv" id="sendingTab' + this.orderTab + '" style="margin-top:7px;display:none;' + h + '">' + sendingTabMsg + '<div id="closeSendingTab' + this.orderTab + '" class="closeTab"><img id="closeSendingImg' + this.orderTab + '" class="closeSubTab" name="So\u1ea1n" src="icon1/Closeinactive.png"></div></div><div id="composeLabel' +
        this.orderTab + '"><span id="composeImage" style=""></span><span style="margin-top:5px;"><span style="position:absolute;left:35px;margin-top:6px;color: #6FAEEE;" class="labelSoan" id="labelSoan' + this.orderTab + '" >' + c + '</span><div id="closeTab' + this.orderTab + '" class="closeTab"><img id="closesubtab' + this.orderTab + '" class="closeSubTab" name="So\u1ea1n" src="icon1/Closeinactive.png"></div></span></div>'; NaN + this.orderTab + '" class="closeSubTab" name="So\u1ea1n" src="icon1/Closeinactive.png"></div></span></div>'
    } this.tab =
    new Tab(b, c, '<div class = "layoutComposeMail" id = "layoutComposeMail' + this.orderTab + '"></div>', !0); this.tab.init(); tabView.addTab(this.tab.tab); this.addComposeTab(); "readmail" == this._model.type ? this.createLayoutReadMail() : this.createLayout(); var e = this; YAHOO.util.Event.on("closeTab" + this.orderTab, "click", function () {
        if ("readmail" == e._model.type) {
            var b = e._model.mailId, c = share.tabReadMail.length; if (0 < c) {
                for (var f = [], d = 0; d < c; d++) share.tabReadMail[d].mailId != b && f.push({
                    mailId: share.tabReadMail[d].mailId,
                    tab: share.tabReadMail[d].tab
                }); share.tabReadMail = f
            } tabView.removeTab(e.tab.tab); tabView.set("activeIndex", 0)
        } else e._model.oldContent != e._editor.getContent() && "" != e._editor.getContent() && "<br><div><br><br></div>" != e._editor.getContent() && "<br>" != e._editor.getContent().toLowerCase() ? e.handleCfmSaveDraft() : (tabView.removeTab(e.tab.tab), tabView.set("activeIndex", 0))
    })
};
ComposeView.prototype.createLayout = function () {
    var b = this; this.style.setTag("div"); var c = this.style.makeStyleWithIdAndClass("headerCompose", "headerCompose" + this.orderTab, "height:100px;", "") + this.style.makeStyleWithIdOrClass("class", "tooBarCompose", "eateLayouttransparent;height:25px;margin-left:1px;margin-top: -10px;", "") + this.style.makeStyleWithIdOrClass("id", "inboxToolbarComposeMail" + this.orderTab, "background: transparent;", "") + '<div id="tb_sendother' + this.orderTab + '" class="sendother"></div></div></div><form name="composeForm" class="composeForm" id="composeForm' +
    this.orderTab + '" style="background: transparent;border:0px;margin-top:5px;"><div class="toFrom"><div class="buttonListDiv"><input align="center"  class="buttonListInput" value ="' + toInputMsg + '" name="' + this.orderTab + '" type="button" style="margin-top:0px;margin-left:6px; color: #565656" /></div><div class="spaceVer"></div><div class="textToFromDiv">'; "postfolder" == this._model.type || "postandreplyall" == this._model.type ? c += '<input  class="textToFromInput" readonly="readonly" name="textToFromInput" id="textToFromInput' +
    this.orderTab + '" type="text" />' : (c += '<input  class="textToFromInput" name="textToFromInput" id="textToFromInput' + this.orderTab + '" type="text"/>', c += '<div  id="autoComTo' + this.orderTab + '" class="yui-ac-container"></div>'); c += '</div><div style="right:5px;position:absolute;"><input type="button" value="CC" class="displayCC" id="cc-button' + this.orderTab + '" /><input type="button" value="BCC" class="displayBCC" id="bcc-button' + this.orderTab + '"/></div></div><div class="spaceHor2"></div><div class="fromCc" style="display:none" id="fromCc' +
    this.orderTab + '"><div class="buttonListDiv"><input align="center"  class="buttonListInput" name="' + this.orderTab + '"  type="button" value="' + ccInputMsg + '" style="margin-top:-2px;margin-left:5px; color: #565656"/></div><div class="spaceVer"></div><div class="textFromCcDiv"><input class="textFromCcInput"  name ="textFromCcInput"  id="textFromCcInput' + this.orderTab + '" type="text"/><div class="yui-ac-container" id="autoComCc' + this.orderTab + '"></div></div></div><div class="spaceHor3" id= "spaceHor3' + this.orderTab +
    '"></div><div class="fromBcc" id="fromBcc' + this.orderTab + '"><div class="buttonListDiv" ><input class="buttonListInput" name="' + this.orderTab + '"  type="button" value="BCC"  style="margin-top:-2px;margin-left:6px"/></div><div class="spaceVer"></div><div class="textFromBccDiv"><input   type="text"  name ="textFromBccInput" class="textFromBccInput" id="textFromBccInput' + this.orderTab + '"/><div id="autoComBcc' + this.orderTab + '" class="yui-ac-container"></div></div></div><div class="spaceHor4"></div><div class="fromSub"><div class="buttonListDiv" ><div align="right" class="sub" style="margin-top:8px;margin-left:14px; color: #565656">' +
    subjectInputMsg + '</div></div><div class="spaceVer"></div><div class="textFromSubDiv"><input type="text"  name = "textFromSubInput" class="textFromSubInput" id ="textFromSubInput' + this.orderTab + '"/>&nbsp;&nbsp;<input type="hidden" id ="selectAccountAddress' + this.orderTab + '" value=""/><input type="hidden" id ="selectAccountName' + this.orderTab + '" value=""/></div></div><div style="margin-left:63px;margin-top:9px;margin-bottom:5px;" id="progressAttach' + this.orderTab + '"></div><div class="yui-pb yui-pb-ltr" id="wprocess' +
    this.orderTab + '"  style="border-radius: 3px;display: none;width: 100px; height: 13px;border:1px solid #808080;margin-left:63px"><div class="yui-pb-bar yui-pb-anim" id="processUpload' + this.orderTab + '" style="width:0;"></div></div><div class="fileAttachDiv" id="fileAttachDiv' + this.orderTab + '"><div id="fileAttach' + this.orderTab + '" class="fileAttachCompose" style="padding-left:20px;margin-left: 45px; max-width: 859px; max-height: 55px; overflow-y: auto;overflow-x:hidden;background:url(/mail/css/giaodien/separator.png) no-repeat scroll 18px 0 transparent;margin-top:2px;"></div></div><div class="spaceHor6"></div></form></div><div class="bodyCompose" ><form name="bodyContent" ><div  id="contentHtml' +
    this.orderTab + '" name="contentHtml"        class="rte-editor"></div><div id="descriptionContainer"></div></form></div></div>'; this.layout = new YAHOO.widget.Layout(this.layoutId, { width: YAHOO.util.Dom.getClientWidth() - 240, height: YAHOO.util.Dom.getClientHeight() - 50, units: [{ position: "center", body: c, gutter: "0 0px px 0" }] }); this.layout.on("render", function () { b.initInterface(); b.initEditor(); b.createToolbar(); b.initAttach() }); this.layout.render(); this.createAutoComplete.notify()
};
ComposeView.prototype.updateTabLabel = function (b) { $("#labelSoan" + this.orderTab).html(b) };
ComposeView.prototype.createLayoutReadMail = function () {
    var b = '<div  align="center"  style="float:right;margin-top:9px;background:transparent;margin-right:20px;" class="Rcompose"><img src="giaodien/previous.png" height="14px" id="previousRead' + this.orderTab + '"/>' + this.style.insertSpaceChar(2) + '<img src="giaodien/separate.png" style="margin-right:7px;height:16px"><img src="giaodien/next.png" height="14px" id="nextRead' + this.orderTab + '"/></div><div class="headerCompose"  id="headerCompose' + this.orderTab +
    '"><div class="tooBarComposeReadMail readMailLayout" style="background: transparent;margin-left:1px;height:26px;"><div id="inboxToolbarComposeMail' + this.orderTab + '"  style="background: transparent;"></div></div><div id="hReadMail' + this.orderTab + "\" style=\"\" class=\"hReadMail\"><div class='divHeaderReadMail' style='padding-top : 5px'><span class='fromLabelReadMail'>" + senderTableMsg + ": </span><span id='fromLabelReadMailText" + this.orderTab + "' class='fromLabelReadMailText'></span><span class='toLabelReadMail'>" +
    toInputMsg + ": </span><a id='toMore" + this.orderTab + "' class='toMoreReadMail'></a><div id='frShowMore" + this.orderTab + "' class='frShowMoreReadMail'><div id='showMore" + this.orderTab + "' class='showMoreReadMail'></div><a id='closeMore" + this.orderTab + "' class='closeMoreReadMail'>&nbsp;</a></div><span id='toLabelReadMailText" + this.orderTab + "' class='toLabelReadMailText'></span></div><div class='hrHeaderReadMail'></div><div class='divHeaderReadMail1'><span class='subjectLabelReadMail'>" + subjectInputMsg + ": </span><span id='subjectLabelReadMailText" +
    this.orderTab + "' class='subjectLabelReadMailText'></span></div><div class=\"attach-div-readmail\" id=\"attach-div-readmail" + this.orderTab + '"><div class="attachFileReadMailLabel" id="attachFileReadMailLabel' + this.orderTab + '">' + fileAttachMsg + '</div><div class="attachFileReadMailIcon" id="attachFileReadMailIcon' + this.orderTab + '" ><img src="giaodien/paperclip1.png" /></div><div class="attachFileReadMailPanel" id="attachFileReadMailPanel' + this.orderTab + '"></div></div></div><iframe id="contentHtml' + this.orderTab +
    '"  frameborder="0" style="overflow:auto;width:100%;border-top:0px;border-left:1px;border-style:solid;border-color:rgb(175,173,174);background-color:#fff" class="divContentReadMail"></iframe></div>', b = b + ("<div id='loadingTabReadMail" + this.orderTab + "' style='position:absolute;top:150px;width:100%'><p align='center' ><img src='images/progress1.gif'></div>"), b = new YAHOO.widget.Layout(this.layoutId, {
        width: YAHOO.util.Dom.getClientWidth() - 240, height: YAHOO.util.Dom.getClientHeight() - 26, units: [{
            position: "center",
            body: b, gutter: "0 5px 0px 0"
        }]
    }), c = this; b.on("render", function () { c.createToolbarReadMail(); $("#contentHtml" + c.orderTab).height(YAHOO.util.Dom.getClientHeight() - 26 - 100 - 14) }); b.render(); $("#attach-div-readmail" + this.orderTab).click(function (b) { b.stopPropagation(); c.createAttachReadMailDialog() }); var h = this._model.rowId, b = this.table.getRecordSet().getRecord(h).getData().id; share.tabReadMail.push({ mailId: b, tab: this.tab.tab }); var b = this.table.getTrEl(h), e = h, k = this.table.getPreviousTrEl(b); k || (document.getElementById("previousRead" +
    this.orderTab).style.opacity = "0.4"); var l = this.table.getNextTrEl(b); l || (document.getElementById("nextRead" + this.orderTab).style.opacity = "0.4"); YAHOO.util.Event.on("previousRead" + this.orderTab, "click", function () {
        if (k) {
            if (e) { var b = c.table.getRecordSet().getRecord(e).getData().id, d = share.tabReadMail.length; if (0 < d) { for (var g = [], a = 0; a < d; a++) share.tabReadMail[a].mailId != b && g.push({ mailId: share.tabReadMail[a].mailId, tab: share.tabReadMail[a].tab }); share.tabReadMail = g } } h = k.id; $("#closeTab" + c.orderTab).click();
            c.table == dataTable ? c.table.handleRowDbClick(h) : c.table.rowDblclickEvent.fire()
        }
    }); YAHOO.util.Event.on("nextRead" + this.orderTab, "click", function () { if (l) { if (e) { var b = c.table.getRecordSet().getRecord(e).getData().id, d = share.tabReadMail.length; if (0 < d) { for (var g = [], a = 0; a < d; a++) share.tabReadMail[a].mailId != b && g.push({ mailId: share.tabReadMail[a].mailId, tab: share.tabReadMail[a].tab }); share.tabReadMail = g } } h = l.id; $("#closeTab" + c.orderTab).click(); c.table == dataTable ? c.table.handleRowDbClick(h) : c.table.rowDblclickEvent.fire() } });
    $(".buttonListInput").attr("tabIndex", -1)
}; ComposeView.prototype.resetInterface = function () { $("#fromLabelReadMailText" + this.orderTab).html(""); $("#subjectLabelReadMailText" + this.orderTab).html(""); $("#toLabelReadMailText" + this.orderTab).html("") };
ComposeView.prototype.createAttachReadMailDialog = function (b, c) {
    var h = new YAHOO.widget.Panel("attachPanelReadMail" + this.orderTab, { visible: !1, draggable: !1, close: !1, zIndex: 1E3 }); b = this._model.message.getDownloadAll(); b = "<a href='" + b + "'><img src ='" + getImgFromFileName(".all") + "' style ='margin-bottom: -3px; margin-right: 3px;'/>" + dowloadAllMsg + "</a>"; c = this._model.message.getFileArray(); h.setHeader(""); var e = document.createElement("div"), k = document.createElement("div"); k.innerHTML = b; YAHOO.util.Dom.setStyle(k,
    "padding", "2px"); $(k).hover(function () { $(this).css({ "background-color": "#EDF5FF" }); $(this).children().css("color", "black") }, function () { $(this).css("background-color", "#FFFFFF") }); $(k).click(function (b) { $(this).children() }); var l = this; $("#attachPanelReadMail" + this.orderTab + "_c").find("a").bind("click", function () { $("#attachPanelReadMail" + l.orderTab + "_c").remove() }); e.appendChild(k); for (var k = c.split("</div>"), f = 0; f < k.length - 1; f++) {
        var d = document.createElement("div"); d.innerHTML = k[f] + "</div>"; $(d).css("padding",
        "2px").css("margin-top", "2px"); $(d).hover(function () { $(this).css({ "background-color": "#EDF5FF" }); $(this).find("a").css("color", "black") }, function () { $(this).css("background-color", "#FFFFFF") }); e.appendChild(d)
    } h.setBody(e); h.setFooter(""); h.render("attachFileReadMailPanel" + this.orderTab); h.cfg.setProperty("minWidth", "160px"); h.show(); mailbox.getInstance().disableOutline(); l = this; YAHOO.util.Event.on(document, "click", function (b) {
        if (h) {
            b = YAHOO.util.Event.getTarget(b); var a = document.getElementById("attachFileReadMailIcon" +
            l.orderTab), d = document.getElementById("attachFileReadMailLabel" + l.orderTab), c = h.element; b == c || YAHOO.util.Dom.isAncestor(c, b) || b == a || YAHOO.util.Dom.isAncestor(a, b) || b == d || YAHOO.util.Dom.isAncestor(d, b) || $("#attachPanelReadMail" + l.orderTab + "_c").remove()
        }
    })
}; ComposeView.prototype.initEditor = function () { $(".bodyCompose").width(this.widthCompose - 40); this._editor = new BmailEditor("#contentHtml" + this.orderTab, null, this); this._editor.render(); this._editor.setHeight(this.heightCompose - 203 + 30) };
ComposeView.prototype.handleInsertImageSuccess = function (b) {
    var c = b.fileName, h = b.mailId, e = b.part, k = Session.getInstance().getAuthen(), l = Session.getInstance().getIpServer(); b = this._editor._editorId; c = location.protocol + "//" + l + "/service/home/~/" + c + "?authToken=" + k + "&id=" + h + "&part=" + e; $("#" + b).contents().find("#uploadingImage").attr("src", c); $("#" + b).contents().find("#uploadingImage").removeAttr("id"); "Microsoft Internet Explorer" == navigator.appName && $("#" + b).contents().find("#uploadingImage").css({
        width: "64px",
        height: "64px"
    })
}; ComposeView.prototype.handleInsertImageFail = function () { $("#" + this._editor._editorId).contents().find("#uploadingImage").remove() };
ComposeView.prototype.createToolbarReadMail = function () {
    this.toolbar = new YAHOO.widget.Toolbar(this.toolbarId, { buttons: [{ group: "", label: null, buttons: [{ id: "compose_tb_reply", type: "push", label: replyButtonMsg, value: "reply" }, { id: "compose_tb_replyall", type: "push", label: replyAllButtonMsg, value: "replyall" }, { id: "compose_tb_forward", type: "push", label: forwardButtonMsg, value: "forward" }] }] }); var b = this; this.toolbar.on("buttonClick", function (c) {
        if ("reply" == c.button.value) {
            c = {
                composeType: "reply", rowId: b._model.rowId,
                mailId: b._model.mailId
            }; c = new ComposeModel(c); var h = new ComposeView(c); c = new ComposeController(h, c); c.init()
        } else "replyall" == c.button.value ? (c = !1, 0 == b._model.folderPath.indexOf("inbox") && (c = !0), c = "2" == b._model.folderId || "3" == b._model.folderId || "4" == b._model.folderId || "5" == b._model.folderId || "6" == b._model.folderId || c ? { composeType: "replyall", rowId: b._model.rowId, mailId: b._model.mailId } : { composeType: "postandreplyall", rowId: b._model.rowId, mailId: b._model.mailId }, c = new ComposeModel(c), h = new ComposeView(c),
        c = new ComposeController(h, c), c.init()) : "forward" == c.button.value && (c = { composeType: "forward", rowId: b._model.rowId, mailId: b._model.mailId }, c = new ComposeModel(c), h = new ComposeView(c), c = new ComposeController(h, c), c.init())
    })
};
ComposeView.prototype.createToolbar = function () {
    var b = this; this.toolbar = "TRUE" == currentUser.useESignature ? new YAHOO.widget.Toolbar(this.toolbarId, { buttons: [{ group: "", label: null, buttons: [{ id: "tb_composeMail" + b.orderTab, type: "push", label: sendButtonMsg, value: "compose" }, { type: "separator" }, { id: "tb_saveDraft" + b.orderTab, type: "push", label: saveButtonMsg, value: "draft" }, { id: "tb_encode", type: "push", label: "M\u00e3 h\u00f3a", value: "encode" }, { id: "tb_ca", type: "push", label: "K\u00fd \u0111i\u1ec7n t\u1eed", value: "ca" }] }] }) :
    new YAHOO.widget.Toolbar(this.toolbarId, { buttons: [{ group: "", label: null, buttons: [{ id: "tb_composeMail" + b.orderTab, type: "push", label: sendButtonMsg, value: "compose" }, { type: "separator" }, { id: "tb_saveDraft" + b.orderTab, type: "push", label: saveButtonMsg, value: "draft" }] }] }); 1 < accountArray.length && ($("#tb_sendother" + this.orderTab).selectSendOther(accountArray, "tb_sendother" + this.orderTab, this.orderTab), $("#tb_sendother" + this.orderTab).css("display", "block")); $("#tb_composeMail" + this.orderTab).click(function () {
        if (!b.toolbar.getButtonById("tb_composeMail" +
        b.orderTab).get("disabled")) {
            var c = b.getEmailTo(); if ("" == c) new MessageBox("L\u1ed7i", noReceiverErrMsg), $("#textToFromInput" + b.orderTab).focus(); else {
                var h = function () {
                    var e = function (b) {
                        var f = b._editor.getContent(), d = b._editor.getPlainTextContent(), e = document.getElementById("textFromSubInput" + b.orderTab).value, a = b.getTo(), k = b.getCc(), h = b.getBcc(), w = "", u = ""; if (1 < accountArray.length) {
                            var t = document.getElementById("selectAccountAddress" + b.orderTab).value, p = document.getElementById("selectAccountName" + b.orderTab).value;
                            t != u && w != p && (w = p, u = t)
                        } -1 == c.indexOf("@") && (c = c + "@" + share.domain); t = b.getPartList(); b.sendMsg.notify({ emailTo: c, toArr: a, ccArr: k, bccArr: h, content: f, frDisplayName: w, frAddress: u, part: t, subject: e, textPlain: d })
                    }; 0 == mailbox.getInstance().timeToRetrieveMailSent ? e(b) : b.handleRetrieveMailSent(e)
                }; if ("" == document.getElementById("textFromSubInput" + b.orderTab).value) new ConfirmBox("Th\u00f4ng b\u00e1o", sendNoSubjectErrMsg, h); else {
                    var e = b._editor.getContent(); "<br>" == e || "" == e ? new ConfirmBox("Th\u00f4ng b\u00e1o",
                    sendNoContentErrMsg, h) : h()
                }
            }
        }
    }); $("#tb_saveDraft" + this.orderTab).click(function () { b.handleSaveDraft() }); $("#tb_ca").click(function () { b.eSignatureBtnClick.notify() })
}; ComposeView.prototype.initEventAbortSendMail = function () { var b = this; console.log("initEventAbortSendMail: "); YAHOO.util.Event.on("closeSendingTab" + this.orderTab, "click", function () { b.cfmCloseSedingTab() }) };
ComposeView.prototype.cfmCloseSedingTab = function () { var b = this; new ConfirmBox(messageBoxMsg, cfmCloseTabWhenSendingMsg, function () { tabView.removeTab(b.tab.tab); tabView.set("activeIndex", 0) }, function () { }, chooseButtonMsg, cancelButtonMsg) }; ComposeView.prototype.createDropToAttach = function () { };
ComposeView.prototype.handleSaveDraft = function () { var b = this.getEmailTo(), c = this._editor.getContent(), c = c.replace(/</gi, "&lt;"), c = c.replace(/>/gi, "&gt;"); -1 != c.indexOf("&nbsp") && (c = c.replace(/&nbsp;/gi, "&#160;")); var c = "<mp ct='text/html'><content>" + c + "</content></mp>", h = document.getElementById("textFromSubInput" + this.orderTab).value; this.saveDraftBtnClick.notify({ multipart: c, subject: h, emailTo: b, notify: !0 }) };
ComposeView.prototype.handleCfmSaveDraft = function () { var b = this; new ConfirmBox(messageBoxMsg, cfmSaveDraftMsg, function () { b.handleSaveDraft(); tabView.removeTab(b.tab.tab); tabView.set("activeIndex", 0) }, function () { tabView.removeTab(b.tab.tab); tabView.set("activeIndex", 0) }, chooseButtonMsg, cancelButtonMsg) };
ComposeView.prototype.handleRetrieveMailSent = function (b) {
    $("#tab .yui-nav").children("li[class='selected']").css("display", "none"); tabView.selectTab(0); mailbox.getInstance().composeArr.push({ composeView: this, callback: b }); var c = this; document.getElementById("retrieveMailSent") || ($("body").prepend('<div id = "retrieveMailSent"></div>'), $("#retrieveMailSent").css("display", "none")); if ("none" == $("#retrieveMailSent").css("display")) {
        $("#retrieveMailSent").css("display", "block"); var h = $("#layoutMain").width() /
        2; $("#retrieveMailSent").css("left", h + "px"); $("#retrieveMailSent").html('<span id ="retrieveMailSentBtn" style="color: red">L\u1ea5y l\u1ea1i </span>th\u01b0 trong <span id ="timeRetrieveMailSent">' + mailbox.getInstance().timeToRetrieveMailSent + "</span> gi\u00e2y."); mailbox.getInstance().timeOutRetrieveMailSent = window.setTimeout(function () { b(mailbox.getInstance().composeArr[0].composeView); mailbox.getInstance().composeArr = [] }, 1E3 * mailbox.getInstance().timeToRetrieveMailSent); $("#retrieveMailSentBtn").click(function () { c.retrieveMailSent() });
        this.countdown("timeRetrieveMailSent")
    }
};
ComposeView.prototype.retrieveMailSent = function () {
    clearTimeout(mailbox.getInstance().timeOutRetrieveMailSent); mailbox.getInstance().timeOutRetrieveMailSent = null; mailbox.getInstance().composeArr = []; $("#retrieveMailSent").css("display", "none"); var b = "composeLabel" + this.orderTab; $("#" + b).parents("li[id='" + this._model.getTabId() + "']").css("display", "inline"); $("#tab .yui-nav").children("li[class='selected']").removeClass("selected"); $("#" + b).parents("li[id='" + this._model.getTabId() + "']").addClass("selected");
    for (var b = $("#tab .yui-nav").children(), c = 0; c < b.length; c++) { var h = b[c]; if ($(h).attr("class") && "selected" == $(h).attr("class")) { tabView.selectTab(c); break } }
}; ComposeView.prototype.countdownInterval = function (b) { var c = parseInt($("#" + b).html()), c = c - 1; 0 > c && (c = 0); $("#" + b).html(c); 0 >= c && $("#" + b).parent().css("display", "none") };
ComposeView.prototype.countdown = function (b) { this.countdownTimeout && (clearInterval(this.countdownTimeout), this.countdownTimeout = null); var c = this; this.countdownTimeout = window.setInterval(function () { c.countdownInterval(b) }, 1E3) };
ComposeView.prototype.initInterface = function () {
    var b = this.widthCompose - 82 - 120 - 4; document.getElementById("btnBcc") && (document.getElementById("btnBcc").value = displayBccMsg); document.getElementById("btnTo") && (document.getElementById("btnTo").value = sendToMsg); document.getElementById("sub") && (document.getElementById("sub").innerHTML = subjectMsg); $(".composeMail").width(this.widthCompose); $(".spaceHor1").width(this.widthCompose); $(".toFrom").width(this.widthCompose); $(".tooBarCompose").width(this.widthCompose);
    $(".textToFromInput").width(b); $(".textToFromInput").css("padding-left", "5px"); $(".spaceHor2").width(this.widthCompose); $(".fromCc").width(this.widthCompose); $(".textFromCcDiv").width(b); $(".textFromCcInput").width(b); $(".textFromCcInput").css("padding-left", "5px"); $(".spaceHor3").width(this.widthCompose); $(".fromBcc").width(this.widthCompose); $(".textFromBccDiv").width(b); $(".textFromBccInput").width(b); $(".textFromBccInput").css("padding-left", "5px"); $(".fromSub").width(this.widthCompose); $(".spaceHor4").width(this.widthCompose);
    $(".textFromSubDiv").width(b); 1 >= accountArray.length ? ($(".textFromSubInput").width(b), $(".sub").css("margin-top", "2px")) : ($(".textFromSubInput").width(b), $(".sub").css("margin-top", "4px")); $(".textFromSubInput").css("padding-left", "5px"); $(".spaceHor5").width(this.widthCompose); $(".spaceHor6").width(this.widthCompose); $(".fileAttachCompose").width(b - 28); var c = this; $(".buttonListInput").click(function (b) {
        $(this).attr("value") != sendToMsg || "forward" != c._model.type && !c._model.selectFolder ? c.createDialogSelectAddress(c.orderTab) :
        c.createDialogSelectFolder()
    }); $("#cc-button" + this.orderTab).click(function () { c.ccBtnClick.notify() }); $("#bcc-button" + this.orderTab).click(function () { c.bccBtnClick.notify() }); this.handleTabKey(); document.getElementById("selectAccountAddress" + this.orderTab).value = Session.getInstance().getUser(); document.getElementById("selectAccountName" + this.orderTab).value = currentUser.displayName; $("#textFromSubInput" + this.orderTab).keyup(function () {
        var b = $(this).val(); "" != b ? (8 < b.length && (b = b.substring(0, 8), b += "..."),
        $("#labelSoan" + c.orderTab).html(b)) : $("#labelSoan" + c.orderTab).html(newTabSoanTxt)
    })
}; ComposeView.prototype.handleTabKey = function () { $(".buttonListInput").attr("tabIndex", -1); $(".displayBCC").attr("tabIndex", -1); $(".displayCC").attr("tabIndex", -1); $(".moxie-shim").attr("tabIndex", -1) };
ComposeView.prototype.updateData = function (b) {
    var c = this; if ("readmail" != this._model.type) {
        if (this._model.loadFromBpr) bkav_template.getInstance().getCookie(), $("#textToFromInput" + this.orderTab).attr("readonly", "readonly"), $("#textToFromInput" + this.orderTab).val(bkav_template.toTemplate()), $("#textFromSubInput" + this.orderTab).val(bkav_template.subjectTemplate()), this._editor.setContent(bkav_template.getInstance().contentTemplate()), mailbox.getInstance().isSupport && (mailbox.getInstance().isSupport = !1);
        else { this._editor.setNoFocusContent(!0); this._editor.setContent(b.content); var h = b.subject || ""; $("#textFromSubInput" + this.orderTab).val(h); if ("postfolder" == this._model.type || "postandreplyall" == this._model.type) $("#textToFromInput" + this.orderTab).val(b.folderPath); else { var e = b.to || ""; $("#textToFromInput" + this.orderTab).val(e) } } window.setTimeout(function () { var a = document.activeElement; console.log("curElement: " + a); a || ($(document).click(), c._editor.focus()) }, 400); window.setTimeout(function () {
            c._model.oldContent =
            c._editor.getContent()
        }, 500)
    } else {
        var h = b.subject || "", k = b.fullName || "", l = b.from || "", e = b.to || "", f = b.content || "", d = h; 8 < h.length && (d = h.substring(0, 8) + "..."); this.updateTabLabel(d); var e = e.replace(/;$/, ""), d = e.split(";"), g = ""; 2 < d.length && (g = e, e = d[0] + ";" + d[1]); 2 < d.length ? (document.getElementById("toLabelReadMailText" + this.orderTab).innerHTML = d[0] + "," + d[1], Util.renderMoreAddress("closeMore" + this.orderTab, "frShowMore" + this.orderTab, "toMore" + this.orderTab, g)) : (2 == d.length && $("#toLabelReadMailText" + this.orderTab).html(d[0] +
        "," + d[1]), 1 == d.length && $("#toLabelReadMailText" + this.orderTab).html(d[0])); k ? $("#fromLabelReadMailText" + this.orderTab).html(k + "&lt;" + l + "&gt;") : $("#fromLabelReadMailText" + this.orderTab).html(l); $("#subjectLabelReadMailText" + this.orderTab).html(h); $("#toLabelReadMailText" + this.orderTab).html(e); 0 < (b.part || []).length && ($("#attachFileReadMailLabel" + this.orderTab).css("display", "block"), $("#attachFileReadMailIcon" + this.orderTab).css("display", "block")); window.setTimeout(function () {
            $("#contentHtml" + c.orderTab).contents().find("body").html("<div id='contentReadMail_" +
            c.orderTab + "'>" + f + "</div>"); $("#loadingTabReadMail" + c.orderTab).remove(); $("#contentHtml" + c.orderTab).contents().find("body").css({ height: "97%", overflow: "auto", padding: "0", margin: "0", "padding-top": "10px" }); $("#contentHtml" + c.orderTab).contents().find("html").css({ height: "100%", overflow: "auto", padding: "0", margin: "0", "padding-left": "12px" }); $("#contentHtml" + c.orderTab).contents().find(".volumeInfo").css({ color: "#6c8594", "font-size": "12px", "margin-top": "20px", "font-family": "Tahoma" }); $("#contentHtml" +
            c.orderTab).contents().find("table").css({ "margin-left": "0" }); var a = $("#contentHtml" + c.orderTab).contents().get(0); $(a).click(function (a) { 0 < $("#frShowMore" + c.orderTab).length && $("#frShowMore" + c.orderTab).css("display", "none"); 0 < $("#attachPanelReadMail" + c.orderTab + "_c").length && $("#attachPanelReadMail" + c.orderTab + "_c").remove() })
        }, 100); Util.createContextMenu("contentHtml" + c.orderTab); window.setTimeout(function () { Util.decodeImageSrc("contentHtml" + c.orderTab) }, 500)
    }
};
ComposeView.prototype.removeComposeTab = function () { var b = getindexNewTab(txtSoan); tabView.removeTab(tabView.getTab(b)) };
ComposeView.prototype.handleStartSendMail = function () { 0 != currentUser.timeoutSession && window.clearTimeout(mailbox.getInstance().timeout); YAHOO.util.Dom.setStyle("sendingTab" + this.orderTab, "display", "block"); YAHOO.util.Dom.setStyle("composeLabel" + this.orderTab, "display", "none"); this.toolbar.getButtonById("tb_composeMail" + this.orderTab).set("disabled", !0); this.toolbar.getButtonById("tb_saveDraft" + this.orderTab).set("disabled", !0); this.initEventAbortSendMail() };
ComposeView.prototype.handleSendMailFail = function () {
    0 < mailbox.getInstance().timeToRetrieveMailSent && this.retrieveMailSent(); YAHOO.util.Dom.setStyle("sendingTab" + this.orderTab, "display", "none"); YAHOO.util.Dom.setStyle("composeLabel" + this.orderTab, "display", "block"); this.isSendingStatus = this.sending = !1; this.toolbar.getButtonById("tb_composeMail" + this.orderTab).set("disabled", !1); this.toolbar.getButtonById("tb_saveDraft" + this.orderTab).set("disabled", !1); document.getElementById("tb_attach" + this.orderTab).disabled =
    !1; mailbox.getInstance().loggingOut && new ConfirmBox(messageBoxMsg, "G\u1eedi mail l\u1ed7i. B\u1ea1n c\u00f3 mu\u1ed1n ti\u1ebfp t\u1ee5c tho\u00e1t kh\u00f4ng?", function () { mailbox.getInstance().handleLogout() }, function () { }, chooseButtonMsg, cancelButtonMsg)
}; ComposeView.prototype.removeTab = function () { tabView.removeTab(this.tab.tab); this.dontSetActiveTab || tabView.set("activeIndex", 0) };
ComposeView.prototype.handleSendMailComplete = function () { this.sending = !1; this.removeTab(); var b = new ActionDoneMsg("resources/common/sentMailMsg.png"); b.init(); b.show(); mailbox.getInstance().loggingOut && mailbox.getInstance().handleLogout() };
ComposeView.prototype.addComposeTab = function () { var b = new YAHOO.widget.Tab({ label: txtSoan, id: "composeMailTab" }); tabView.addTab(b); YAHOO.util.Event.on("composeMailTab", "click", function () { var b = new ComposeModel({ composeType: "compose" }), h = new ComposeView(b); (new ComposeController(h, b)).init() }) };
ComposeView.prototype.handleCcBcc = function (b) {
    "bcc-button" == b.id ? (this._headerHeight || (this._headerHeight = $("#headerCompose" + this.orderTab).height() - ($(".fileAttachDiv").height() || 0)), "none" == YAHOO.util.Dom.getStyle("fromBcc" + this.orderTab, "display") ? (YAHOO.util.Dom.setStyle("fromBcc" + this.orderTab, "display", "block"), this._headerHeight += 28, this._editor.setHeight(this.heightCompose - 230 + 44)) : (YAHOO.util.Dom.setStyle("fromBcc" + this.orderTab, "display", "none"), this._headerHeight -= 28, this._editor.setHeight(this.heightCompose -
    205 + 44))) : (this._headerHeight || (this._headerHeight = $("#headerCompose" + this.orderTab).height() - ($(".fileAttachDiv").height() || 0)), "none" == YAHOO.util.Dom.getStyle("fromCc" + this.orderTab, "display") ? (YAHOO.util.Dom.setStyle("fromCc" + this.orderTab, "display", "block"), this._headerHeight += 28, this._editor.setHeight(this.heightCompose - 230 + 44)) : (YAHOO.util.Dom.setStyle("fromCc" + this.orderTab, "display", "none"), this._headerHeight -= 28, this._editor.setHeight(this.heightCompose - 205 + 44))); $("#headerCompose" + this.orderTab).height(this._headerHeight +
    $(".fileAttachDiv").height() || 0)
}; ComposeView.prototype.handleUploadComplete = function () { $("#wprocess" + this.orderTab).css("display", "none") };
ComposeView.prototype.initAttach = function () {
    window.addEventListener("dragover", function (b) { b = b || event; b.preventDefault() }, !1); window.addEventListener("drop", function (b) { b = b || event; b.preventDefault() }, !1); var b = this, c = Session.getInstance().getIpServer(), h = "200,'null','", e = new plupload.Uploader({
        runtimes: "html5,flash", browse_button: "tb_attach" + b.orderTab, drop_element: "descriptionContainer", inIframe: !0, editor_id: b._editor._editorId, multi_selection: !0, url: location.protocol + "//" + c + "/service/upload?fmt=raw",
        flash_swf_url: "lib/plupload/Moxie.swf", dragdrop: !0, init: {
            PostInit: function () { }, FilesAdded: function (c, h) {
                b.attachFail = !1; for (var f = !0, d = "", g = !1, a = 0; a < h.length; a++) { var q = h[a].name.split(".").pop(); -1 != currentUser.extension.indexOf(q) ? (f = !1, d += h[a].name + "<br>", c.removeFile(h[a])) : g = !0 } f ? c.total.size > currentUser.maxFileSize && (g = !1, this.trigger("Error", {
                    code: "-600", message: "T\u1ed5ng dung l\u01b0\u1ee3ng t\u1ec7p \u0111\u00ednh k\u00e8m kh\u00f4ng \u0111\u01b0\u1ee3c v\u01b0\u1ee3t qu\u00e1 " + plupload.formatSize(currentUser.maxFileSize),
                    file: h[a]
                })) : this.trigger("Error", { code: "-601", message: d + errorTypeAttachment, file: h[a] }); g && (b._headerHeight || (b._headerHeight = $("#headerCompose" + b.orderTab).height() - ($("#fileAttach" + b.orderTab).height() || 0)), $("#headerCompose" + b.orderTab).height($("#headerCompose" + b.orderTab).height() + 15), b._model.isAttaching = !0, e.start())
            }, UploadComplete: function (b, c) { }, UploadProgress: function (c, h) {
                $("#wprocess" + b.orderTab).css("display", "block"); $("#processUpload" + b.orderTab).width(e.total.percent); $("#processUpload" +
                b.orderTab).html('<span style="height: 8px;font-size: 12; margin-left: 105px;">' + e.total.percent + "%</span>")
            }, FileUploaded: function (c, l, f) {
                b.attachFail = !1; l = f.response.split(","); 2 < l.length && (l = l[2].split("'"), h = "200,'null','" == h ? h + l[1] : h + ("," + l[1])); if (c.total.uploaded == e.files.length) {
                    h += "'"; h.split(","); if ("<pre></pre>" == h) pb1 && pb1.set("value", 2E3), new MessageBox("L\u1ed7i", attachErrMsg), $(".yui-editor-editable-container").height($(".yui-editor-editable-container").height() + 25), b.attachFail = !0,
                    b._isSendingStatus = !1; else {
                        c = []; l = h.split(","); f = l.length; if (2 < f) for (var d = 2; d < f; d++) c.push(l[d]); b._model._attachBoxes = []; l = b.getEmailTo(); f = b._editor.getContent(); f = f.replace(/</gi, "&lt;"); f = f.replace(/>/gi, "&gt;"); -1 != f.indexOf("&nbsp") && (f = f.replace(/&nbsp;/gi, "&#160;")); f = "<mp ct='text/html'><content>" + f + "</content></mp>"; if (document.getElementById("textFromSubInput") + b.orderTab) var g = document.getElementById("textFromSubInput" + b.orderTab).value; b.saveDraftBtnClick.notify({
                            attachId: c, multipart: f,
                            subject: g, emailTo: l
                        })
                    } h = "200,'null','"
                }
            }, Error: function (c, e) { b._model.isAttaching = !1; "-600" != e.code && "-601" != e.code || new MessageBox(errorDlgMsg, e.message) }
        }
    }); e.init({ editor_id: b._editor._editorId }); $(document).live()
};
ComposeView.prototype.handleAttachFail = function (b) {
    b = b.response; b.Body && b.Body.Fault && b.Body.Fault.Detail && b.Body.Fault.Detail.Error && "mail.MESSAGE_TOO_BIG" == b.Body.Fault.Detail.Error.Code ? (this.attachFail = !0, $(".yui-editor-editable-container").height($(".yui-editor-editable-container").height() + 25), new MessageBox("L\u1ed7i", attachSizeToBigErrMsg)) : (this.attachFail = !0, $(".yui-editor-editable-container").height($(".yui-editor-editable-container").height() + 25), -1 != response_data.indexOf("413,'null'") ?
    new MessageBox(errorDlgMsg, attachSizeToBigErrMsg) : new MessageBox(errorDlgMsg, attachErrMsg)); this.attachFail ? $("#headerCompose" + this.orderTab).height($("#headerCompose" + this.orderTab).height() - 15) : ($("#fileAttach" + this.orderTab).css("display", "block"), $("#headerCompose" + this.orderTab).height(this._headerHeight + $("#fileAttach" + this.orderTab).height() + 3))
};
ComposeView.prototype.updateHeight = function () { var b = $("#headerCompose" + this.orderTab).height(); $("#headerCompose" + this.orderTab).height(this._headerHeight + $("#fileAttach" + this.orderTab).height()); b = $("#headerCompose" + this.orderTab).height() - b; console.log("added : " + b); $(".yui-editor-editable-container").css("height", $(".yui-editor-editable-container").height() - b + "px !important") };
ComposeView.prototype.removeAttachFile = function (b, c) {
    for (var h = -1, e = 0; e < this._model._attachBoxes.length; e++) this._model._attachBoxes[e].getPart() == b && (h = e); 0 <= h && this._model._attachBoxes.splice(h, 1); h = -1; for (e = 0; e < this._model.midPart.length; e++) this._model.midPart[e] == b && (h = e); 0 <= h && this._model.midPart.splice(h, 1); h = -1; console.log("isFwd : " + c); if (c && 0 < this._model.partMailFwd.length) for (e = 0; e < this._model.partMailFwd.length; e++) this._model.partMailFwd[e] == b && (h = e); 0 <= h && this._model.partMailFwd.splice(h,
    1); console.log(h); console.log("this._model.partMailFwd.length: " + this._model.partMailFwd.length)
}; ComposeView.prototype.hasAttach = function () { return this._model._attachBoxes && 0 < this._model._attachBoxes.length }; ComposeView.prototype.getPartList = function () { var b = []; if (this._model._attachBoxes) for (var c = 0; c < this._model._attachBoxes.length; c++) b.push(this._model._attachBoxes[c].getPart()); return b };
ComposeView.prototype.getAttachFileNames = function () { var b = []; if (this._model._attachBoxes) for (var c = 0; c < this._model._attachBoxes.length; c++) b.push(this._model._attachBoxes[c].getFileName()); return b };
ComposeView.prototype.insertAttach = function (b) {
    this._model._attachBoxes && 0 == this._model._attachBoxes.length && $("#fileAttachDiv" + this.orderTab + " span[id^='attachElement']").remove(); var c = b.part, h = b.fileName; this.counter++; $("#fileAttachDiv" + this.orderTab).css("display", "block"); $("#headerCompose" + this.orderTab).css("height", "140px"); var e = !1; b.isFwd && (e = b.isFwd); if ("" != h) {
        if (document.getElementById("fileAttach" + this.orderTab)) var k = document.getElementById("fileAttach" + this.orderTab); b = new AttachmentBox(this,
        k, getImgFromFileName(h), h, !1, e); b.setPart(c); b.render(); this._model._attachBoxes || (this._model._attachBoxes = []); this._model._attachBoxes.push(b)
    } 3 < this._model._attachBoxes.length ? ($("#headerCompose" + this.orderTab).height("160px"), this._editor.setHeight(this.heightCompose - 203 - 30)) : 0 < this._model._attachBoxes.length ? (this._editor.setHeight(this.heightCompose - 203 - 10), $("#headerCompose" + this.orderTab).height("140px")) : this._editor.setHeight(this.heightCompose - 203 + 30)
};
ComposeView.prototype.getTo = function () { var b = document.getElementById("textToFromInput" + this.orderTab).value, c = 0, h = []; if ("" == b) c = 0; else if (-1 != b.indexOf(";")) for (var h = b.split(";"), c = h.length, e = 0; e < c; e++) { if (-1 != h[e].indexOf(",")) for (var k = h[e].split(","), c = c - 1 + k.length, h = removeItem(h, e), l = k.length, f = 0; f < l; f++) h.push(k[f]) } else -1 != b.indexOf(",") ? (h = b.split(","), c = h.length) : c = 1; k = []; if (0 != c) { if (0 < h.length ? k.push(h[0]) : k.push(b), 1 < c) for (e = 1; e < c; e++) 0 < h.length ? k.push(h[e]) : k.push(b) } else k.push(b); return k };
ComposeView.prototype.getCc = function () { var b = [], c = document.getElementById("textFromCcInput" + this.orderTab).value, h = 0, e = []; if ("" == c) h = 0; else if (-1 != c.indexOf(";")) for (var e = c.split(";"), h = e.length, k = 0; k < h; k++) { if (-1 != e[k].indexOf(",")) for (var l = e[k].split(","), h = h - 1 + l.length, e = removeItem(e, k), f = l.length, d = 0; d < f; d++) e.push(l[d]) } else -1 != c.indexOf(",") ? (e = c.split(","), h = e.length) : h = 1; if (0 != h) { if (0 < e.length ? b.push(e[0]) : b.push(c), 1 < h) for (k = 1; k < h; k++) 0 < e.length ? b.push(e[k]) : b.push(c) } else b.push(c); return b };
ComposeView.prototype.getBcc = function () {
    var b = [], c = document.getElementById("textFromBccInput" + this.orderTab).value, h = [], e = 0; if ("none" == document.getElementById("fromBcc" + this.orderTab).style.display) e = 0; else if ("" == c) e = 0; else if (-1 != c.indexOf(";")) for (var h = c.split(";"), e = h.length, k = 0; k < e; k++) { if (-1 != h[k].indexOf(",")) for (var l = h[k].split(","), e = e - 1 + l.length, h = removeItem(h, k), f = l.length, d = 0; d < f; d++) h.push(l[d]) } else -1 != c.indexOf(",") ? (h = c.split(","), e = h.length) : e = 1; if (0 != e) {
        if (0 < h.length ? b.push(h[0]) :
        b.push(c), 1 < e) for (k = 1; k < e; k++) 0 < h.length ? b.push(h[k]) : b.push(c)
    } else b.push(c); return b
};
ComposeView.prototype.getEmailTo = function () {
    var b = this.getTo(),
        c = this.getCc(),
        h = this.getBcc(),
        e = b.length;
    if (0 != e) {
        for (var k = "<e t='f' a='" + Session.getInstance().getUser()
            + "' p='"
            + currentUser.displayName + "' />",
            l = 0;
            l < e; l++) ""
                != b[l] && -1 != b[l].indexOf("@")
                && (k += "<e t='t' a='"
                + b[l] + "'/>"); if (0 < c.length) for (l = 0; l < c.length; l++) "" != c[l] && (k += "<e t='c' a='" + c[l] + "'/>"); if (0 < h.length) for (l = 0; l < h.length; l++) "" != h[l] && (k += "<e t='b' a='" + h[l] + "'/>"); return k
    } return ""
};
ComposeView.prototype.createDialogSelectFolder = function () {
    function b() {
        if ("" != document.getElementById("foldertoMove").value) { l._model.type = "postfolder"; l._model.selectFolder = !0; var b = document.getElementById("idSubFolder").value; l._model.folderId = b; b = TreeController.getInstance().getNodeByProperty("labelElId", b); l._model.folderPath = getLocation(b); $("#textToFromInput" + l.orderTab).val(l._model.folderPath); $("#textToFromInput" + l.orderTab).attr("disabled", "disabled"); this.cancel() } else "" == b && ($("#foldertoMove").focus(),
        new MessageBox(errorDlgMsg, noChoiceFolderToMoveMsg))
    } var c = document.body.appendChild(document.createElement("div")); c.setAttribute("id", "moveDialog"); var h = c.appendChild(document.createElement("div")); h.className = "hd"; h.appendChild(document.createTextNode("")); h = c.appendChild(document.createElement("div")); h.className = "bd"; var e = document.createElement("div"); e.setAttribute("id", "divFolder"); var k = document.createElement("label"); k.innerHTML = folderMsg + ": "; e.appendChild(k); k = document.createElement("input");
    k.setAttribute("type", "text"); k.setAttribute("size", "30"); k.setAttribute("id", "foldertoMove"); k.setAttribute("disabled", "disabled"); e.appendChild(k); k = document.createElement("input"); k.setAttribute("type", "hidden"); k.setAttribute("id", "idSubFolder"); e.appendChild(k); h.appendChild(e); e = h.appendChild(document.createElement("div")); e.setAttribute("id", "bodyMoveDialog"); e.style.overflow = "auto"; e.style.background = "none repeat scroll 0 0 #FFFFFF"; e.style.height = "300px"; e.style.width = "400px"; e.style.border =
    "1px solid #c0c0c0"; e.style.marginTop = "5px"; e.style.bgcolor = "#fdfcfa"; h = document.createElement("div"); h.id = TreeController.subTreeId + TreeView.INDEX; e.appendChild(h); TreeView.INDEX++; e = new TreeModel(TreeController.getInstance().getBmailFolderStructure()); e = new TreeView(e); e.createTree({ treeId: h.id, isSubTree: !0 }); TreeController.getInstance().changeExpandRootNode(e); e.tree.subscribe("labelClick", function (b) {
        var d = getSubfolderLabel(b.label); document.getElementById("foldertoMove").value = d; document.getElementById("idSubFolder").value =
        b.labelElId
    }); c.appendChild(document.createElement("div")).className = "ft"; var c = new YAHOO.widget.SimpleDialog(c, { fixedcenter: !0, modal: !0, constraintoviewport: !0, close: !1, width: "430px", visible: !1 }), l = this; c.cfg.queueProperty("buttons", [{ text: chooseButtonMsg, handler: b }, { text: closeButtonMsg, handler: function () { this.cancel() } }]); c.setHeader("<div class = 'rhd'></div><div class = 'rft'></div>" + headerDialogMoveMailMsg); c.render(document.body); c.bringToTop(); c.show(); $("#foldertoMove").focus(); $("#divFolder").css("margin-left",
    "3px"); c.focusEnter(b); mailbox.getInstance().disableOutline()
};
ComposeView.prototype.createDialogSelectAddress = function () {
    var b = this, c = new YAHOO.widget.SimpleDialog("CreateNewContactGroupDialog", { fixedcenter: !0, modal: !0, constraintoviewport: !0, draggable: !0, close: !1, visible: !1, height: "525px", width: "820px" }), h = [{
        text: chooseButtonMsg, handler: function () {
            var c = b._tblAddress.getRecordSet().getLength(); if (0 < c) {
                for (var e = [], f = [], d = [], g = 0; g < c; g++) {
                    var a = b._tblAddress.getRecordSet().getRecord(g), h = a.getData().type, a = a.getData().email; "To" == h ? e.push(a) : "Cc" == h ? f.push(a) :
                    "Bcc" == h && d.push(a)
                } if (0 < e.length) { c = $("#textToFromInput" + b.orderTab).val(); g = c.substr(c.length - 1); h = ""; for (a = 0; a < e.length; a++) h += e[a] + ";"; c = "" == c || ";" == g || "," == g ? c + h : c + ";" + h; $("#textToFromInput" + b.orderTab).val(c) } if (0 < f.length && ("block" != document.getElementById("fromCc" + b.orderTab).style.display && $("#cc-button" + b.orderTab).click(), "block" == document.getElementById("fromCc" + b.orderTab).style.display)) {
                    c = $("#textFromCcInput" + b.orderTab).val(); g = c.substr(c.length - 1); h = ""; for (a = 0; a < f.length; a++) h += f[a] +
                    ";"; c = "" == c || ";" == g || "," == g ? c + h : c + ";" + h; $("#textFromCcInput" + b.orderTab).val(c)
                } if (0 < d.length && ("block" != document.getElementById("fromBcc" + b.orderTab).style.display && $("#bcc-button" + b.orderTab).click(), "block" == document.getElementById("fromBcc" + b.orderTab).style.display)) { c = $("#textFromBccInput" + b.orderTab).val(); g = c.substr(c.length - 1); h = ""; for (a = 0; a < d.length; a++) h += d[a] + ";"; c = "" == c || ";" == g || "," == g ? c + h : c + ";" + h; $("#textFromBccInput" + b.orderTab).val(c) }
            } this.cancel()
        }
    }, { text: cancelButtonMsg, handler: function () { this.cancel() } }],
    e; e = "<div>" + ("<input type='text' id='searchAddressInput' /> <input type='button' value='" + searchButtonMsg + "' id='searchAddressBtn' />"); e += "<div  id='autoSearch' class='yui-ac-container'></div></div>"; e += "<div>"; e += "<table>"; e += "<tr>"; e += "<td>"; e += "<div id='tblSelectAddress' />"; e += "</td>"; e += "<td>"; e += "<div><input type='button' value='" + sendToMsg + "' id='toBtnSelectAddress' /></div>"; e += "<div><input type='button' value='" + ccInputMsg + "' id='ccBtnSelectAddress' /></div>"; e += "<div><input type='button' value='" +
    bccInputMsg + "' id='bccBtnSelectAddress' /></div>"; e += "<div><input type='button' value='" + removeButtonMsg + "' id='removeBtnSelectAddress' /></div>"; e += "</td>"; e += "<td>"; e += "<div id='tblAddress' />"; e += "</td>"; e += "</tr>"; e += "</table>"; e += "</div>"; c.cfg.queueProperty("buttons", h); c.setBody(e); c.setHeader(' <div class = "rhd"></div><div class = "rft"></div> ' + selectContactMsg); c.render(document.body); c.show(); this.createTblSelectAddress(); this.createTblAddress(); b = this; c = []; for (h = 0; h < currentUser.contact.length; h++) /^([\w-\.]+) @((\[[0-9]{1, 3}\.[0-9]{1, 3}\.[0-9]{1, 3}\.) |(([\w-]+\.) +)) ([a-zA-Z]{2, 4}|[0-9]{1, 3}) (\]?) $/.test(currentUser.contact[h].email) &&
    c.push({ email: currentUser.contact[h].email, name: currentUser.contact[h].fullname }); c = new YAHOO.util.LocalDataSource(c); c.responseSchema = { fields: ["email", "name"] }; c = new YAHOO.widget.AutoComplete("searchAddressInput", "autoSearch", c); c.prehighlightClassName = "yui-ac-prehighlight"; c.queryDelay = 0; c.doBeforeExpandContainer = function (c, e, f, d) {
        c = []; for (e = 0; e < d.length; e++) { f = d[e].name; var g = d[e].email; "" == f && (f = g); c.push({ email: g, name: f }) } d = b._tblSelectAddress.getRecordSet(); 0 < d.getLength() && (b._tblSelectAddress.set("MSG_EMPTY",
        ""), d = d.getLength(), b._tblSelectAddress.deleteRows(0, d)); b._tblSelectAddress.addRows(c, 0); return !1
    }; YAHOO.util.Event.on("searchAddressInput", "keyup", function (c) { "" == document.getElementById("searchAddressInput").value && b.createTblSelectAddress() }); YAHOO.util.Event.on("searchAddressInput", "keydown", function (c) {
        if (13 == c.keyCode) {
            c = $("#searchAddressInput").val(); var e = new XMLElement("SearchGalRequest"); e.addAttribute("type", "account"); e.addAttribute("limit", "500"); e.addAttribute("name", c); c = new Soap(readCookie("user"),
            e, "urn:zimbraAccount", !0); c.setSimple(!0); c.isNeedAbort(!1); c.sendRequestCallback(function (c) { var d = []; if (c.Body && c.Body.SearchGalResponse && c.Body.SearchGalResponse.cn) for (var e = c.Body.SearchGalResponse.cn.length, a = 0; a < e; a++) { var h = c.Body.SearchGalResponse.cn[a]._attrs.email, k = ""; c.Body.SearchGalResponse.cn[a]._attrs.fullName && (k = c.Body.SearchGalResponse.cn[a]._attrs.fullName); "" != k && d.push({ name: k, email: h }) } b.createTblSelectAddress(d) })
        }
    }); YAHOO.util.Event.on("searchAddressBtn", "click", function () {
        var c =
        $("#searchAddressInput").val(), e = new XMLElement("SearchGalRequest"); e.addAttribute("type", "account"); e.addAttribute("limit", "500"); e.addAttribute("name", c); c = new Soap(readCookie("user"), e, "urn:zimbraAccount", !0); c.setSimple(!0); c.isNeedAbort(!1); c.sendRequestCallback(function (c) {
            var d = []; if (c.Body && c.Body.SearchGalResponse && c.Body.SearchGalResponse.cn) for (var e = c.Body.SearchGalResponse.cn.length, a = 0; a < e; a++) {
                var h = c.Body.SearchGalResponse.cn[a]._attrs.email, k = ""; c.Body.SearchGalResponse.cn[a]._attrs.fullName &&
                (k = c.Body.SearchGalResponse.cn[a]._attrs.fullName); "" != k && d.push({ name: k, email: h })
            } b.createTblSelectAddress(d)
        })
    }); YAHOO.util.Event.on("toBtnSelectAddress", "click", function () {
        var c = b._tblSelectAddress.getSelectedRows(), e = c.length; if (0 < e) for (var f = 0; f < e; f++) {
            for (var d = c[f], g = b._tblSelectAddress.getRecordSet().getRecord(d).getData().name, d = b._tblSelectAddress.getRecordSet().getRecord(d).getData().email, a = 0, h = b._tblAddress.getRecordSet().getLength(), m = 0; m < h; m++) {
                var w = b._tblAddress.getRecordSet().getRecord(m),
                u = w.getData().type; if (w.getData().email == d && "To" == u) { a = 1; break }
            } 0 == a && (a = [], a.push({ type: "To", email: d, name: g }), b._tblAddress.addRow(a[0]))
        }
    }); YAHOO.util.Event.on("ccBtnSelectAddress", "click", function () {
        var c = b._tblSelectAddress.getSelectedRows(), e = c.length; if (0 < e) for (var f = 0; f < e; f++) {
            for (var d = c[f], g = b._tblSelectAddress.getRecordSet().getRecord(d).getData().name, d = b._tblSelectAddress.getRecordSet().getRecord(d).getData().email, a = 0, h = b._tblAddress.getRecordSet().getLength(), m = 0; m < h; m++) {
                var w = b._tblAddress.getRecordSet().getRecord(m),
                u = w.getData().type; if (w.getData().email == d && "Cc" == u) { a = 1; break }
            } 0 == a && (a = [], a.push({ type: "Cc", email: d, name: g }), b._tblAddress.addRow(a[0]))
        }
    }); YAHOO.util.Event.on("bccBtnSelectAddress", "click", function () {
        var c = b._tblSelectAddress.getSelectedRows(), e = c.length; if (0 < e) for (var f = 0; f < e; f++) {
            for (var d = c[f], g = b._tblSelectAddress.getRecordSet().getRecord(d).getData().name, d = b._tblSelectAddress.getRecordSet().getRecord(d).getData().email, a = 0, h = b._tblAddress.getRecordSet().getLength(), m = 0; m < h; m++) {
                var w = b._tblAddress.getRecordSet().getRecord(m),
                u = w.getData().type; if (w.getData().email == d && "Bcc" == u) { a = 1; break }
            } 0 == a && (a = [], a.push({ type: "Bcc", email: d, name: g }), b._tblAddress.addRow(a[0]))
        }
    }); YAHOO.util.Event.on("removeBtnSelectAddress", "click", function (c) { c = b._tblAddress.getSelectedRows().length; if (0 < c) for (var e = 0; e < c; e++) { var f = b._tblAddress.getSelectedRows()[e]; b._tblAddress.deleteRow(f) } })
};
ComposeView.prototype.createTblSelectAddress = function (b) {
    var c = this; YAHOO.util.Event.onDOMReady(function () {
        if (b) var h = new YAHOO.util.DataSource(b); else { for (var h = [], e = 0; e < currentUser.contact.length; e++) { var k = currentUser.contact[e].email, l = currentUser.contact[e].fullname; /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/.test(k) && ("" == l && (l = k), h.push({ name: l, email: k })) } h = new YAHOO.util.DataSource(h) } c._tblSelectAddress = new YAHOO.widget.DataTable("tblSelectAddress",
        [{ key: "name", label: "<div style='margin-top:2px'>T\u00ean", resizeable: !0, width: 100 }, { key: "email", label: "<div style='margin-top:2px'>Email", resizeable: !1, width: 150 }], h, { scrollable: !0, width: "350px", height: "350px", MSG_EMPTY: "" }); c._tblSelectAddress.subscribe("rowClickEvent", c._tblSelectAddress.onEventSelectRow); c._tblSelectAddress.subscribe("rowDblclickEvent", function (b) {
            var d = b.target.id; b = this.getRecordSet().getRecord(d).getData().name; for (var d = this.getRecordSet().getRecord(d).getData().email, e = 0,
            a = c._tblAddress.getRecordSet().getLength(), h = 0; h < a; h++) { var k = c._tblAddress.getRecordSet().getRecord(h), l = k.getData().type; if (k.getData().email == d && "To" == l) { e = 1; break } } 0 == e && (e = [], e.push({ type: "To", email: d, name: b }), c._tblAddress.addRow(e[0]))
        })
    })
};
ComposeView.prototype.createTblAddress = function () {
    var b = this; YAHOO.util.Event.onDOMReady(function () {
        var c = [{ key: "type", label: "<div style='margin-top:2px'>" + typeMsg, resizeable: !0, width: 30 }, { key: "name", label: "<div style='margin-top:2px'>" + nameMsg, resizeable: !0, width: 100 }, { key: "email", label: "<div style='margin-top:2px'>" + emailMsg, resizeable: !1, width: 150 }], h = new YAHOO.util.DataSource; b._tblAddress = new YAHOO.widget.DataTable("tblAddress", c, h, { scrollable: !0, width: "350px", height: "350px", MSG_EMPTY: "" }); b._tblAddress.subscribe("rowClickEvent",
        b._tblAddress.onEventSelectRow); b._tblAddress.subscribe("rowDblclickEvent", function (c) { b._tblAddress.deleteRow(c.target.id) })
    })
}; function ComposeController(b, c) {
    this.sendComposeInQueue(); this._view = b; this._model = c; var h = this; this._view.sendMsg.attach(function (b, c) { h._model.sendMsg(c) }); this._view.saveDraftBtnClick.attach(function (b, c) { h._model.saveDraft(c) }); this._view.ccBtnClick.attach(function (b, c) { c = { id: "cc-button" }; h._view.handleCcBcc(c) }); this._view.bccBtnClick.attach(function (b, c) { c = { id: "bcc-button" }; h._view.handleCcBcc(c) }); this._model.updateData.attach(function (b, c) {
        var l = ["js/mudim-minorfixed.js" + mailbox.getVersionWeb()];
        YAHOO.util.Get.script(l, { onFailure: function () { }, onSuccess: function () { mailbox.getInstance().viEnabled ? Mudim.method = 2 : Mudim.method = 0; h._view.updateData(c) } })
    }); this._model.insertAttach.attach(function (b, c) { h._view.insertAttach(c) }); this._model.attachFail.attach(function (b, c) { h._view.handleAttachFail(c) }); this._model.startSendMail.attach(function () { h._view.handleStartSendMail() }); this._model.sendMailFail.attach(function () { h._view.handleSendMailFail() }); this._model.sendMailComplete.attach(function () { h._view.handleSendMailComplete() });
    this._view.createAutoComplete.attach(function () { h.createAutoComplete() }); this._model.insertImageFail.attach(function () { h._view.handleInsertImageFail() }); this._model.insertImageSuccess.attach(function (b, c) { h._view.handleInsertImageSuccess(c) }); this._model.uploadCompleteEvent.attach(function (b, c) { h._view.handleUploadComplete() }); this.handleWindowClose()
} ComposeController.dontSetActiveTab = !1; ComposeController.prototype.abortSending = !1; ComposeController.prototype.useKyDienTu = !1;
ComposeController.prototype.countdownTimeout = null; ComposeController.prototype.init = function () { this._view.init(); this._model.getMail() };
ComposeController.prototype.createAutoComplete = function () {
    var b = this._view.orderTab; $(".textToFromInput").keypress(function (b) { if (32 == b.which || 37 == b.which || 38 == b.which || 39 == b.which || 40 == b.which) return !1 }); for (var c = [], h = 0; h < currentUser.contact.length; h++) { var e = currentUser.contact[h].email; e && (e = e.toLowerCase(), -1 == c.indexOf(e) && c.push(e)) } h = new YAHOO.util.LocalDataSource(c); h.responseSchema = { fields: ["contact"] }; h = new YAHOO.widget.AutoComplete("textToFromInput" + b, "autoComTo" + b, h); h.prehighlightClassName =
    "yui-ac-prehighlight"; h.delimChar = ";"; h.maxResultsDisplayed = 20; e = new YAHOO.util.LocalDataSource(c); e.responseSchema = { fields: ["contact"] }; e = new YAHOO.widget.AutoComplete("textFromCcInput" + b, "autoComCc" + b, e); e.prehighlightClassName = "yui-ac-prehighlight"; e.delimChar = ";"; e.maxResultsDisplayed = 20; c = new YAHOO.util.LocalDataSource(c); c.responseSchema = { fields: ["contact"] }; c = new YAHOO.widget.AutoComplete("textFromBccInput" + b, "autoComBcc" + b, c); c.prehighlightClassName = "yui-ac-prehighlight"; c.delimChar = ";"; c.maxResultsDisplayed =
    20; h.doBeforeExpandContainer = function (c, e, f, d) { c = document.getElementById("textToFromInput" + b); f = YAHOO.util.Dom.getX(c); f = f + 11 * c.value.length / 2 - 2; YAHOO.util.Dom.setX(e, f); return !0 }; e.doBeforeExpandContainer = function (c, e, f, d) { c = document.getElementById("textFromCcInput" + b); f = YAHOO.util.Dom.getX(c); f = f + 11 * c.value.length / 2 - 2; YAHOO.util.Dom.setX(e, f); return !0 }; c.doBeforeExpandContainer = function (c, e, f, d) {
        c = document.getElementById("textFromBccInput" + b); f = YAHOO.util.Dom.getX(c); f = f + 11 * c.value.length / 2 - 2; YAHOO.util.Dom.setX(e,
        f); return !0
    }
}; ComposeController.prototype.sendComposeInQueue = function () { if (0 < mailbox.getInstance().composeArr.length) { clearTimeout(mailbox.getInstance().timeOutRetrieveMailSent); mailbox.getInstance().timeOutRetrieveMailSent = null; $("#retrieveMailSent").css("display", "none"); for (var b = mailbox.getInstance().composeArr.length, c = 0; c < b; c++) { var h = mailbox.getInstance().composeArr[c].composeView, e = mailbox.getInstance().composeArr[c].callback; h.dontSetActiveTab = !0; e(h); mailbox.getInstance().composeArr.pop() } } };
ComposeController.prototype.handleWindowClose = function () { var b = this; window.onbeforeunload = function () { b.sendComposeInQueue() } };