/*! jQuery v2.2.3 | (c) jQuery Foundation | jquery.org/license */
!function(n,t){"object"==typeof module&&"object"==typeof module.exports?module.exports=n.document?t(n,!0):function(n){if(!n.document)throw new Error("jQuery requires a window with a document");return t(n)}:t(n)}("undefined"!=typeof window?window:this,function(n,t){function ii(n){var t=!!n&&"length"in n&&n.length,r=i.type(n);return"function"===r||i.isWindow(n)?!1:"array"===r||0===t||"number"==typeof t&&t>0&&t-1 in n}function ri(n,t,r){if(i.isFunction(t))return i.grep(n,function(n,i){return!!t.call(n,i,n)!==r});if(t.nodeType)return i.grep(n,function(n){return n===t!==r});if("string"==typeof t){if(bf.test(t))return i.filter(t,n,r);t=i.filter(t,n)}return i.grep(n,function(n){return lt.call(t,n)>-1!==r})}function hr(n,t){while((n=n[t])&&1!==n.nodeType);return n}function kf(n){var t={};return i.each(n.match(h)||[],function(n,i){t[i]=!0}),t}function yt(){u.removeEventListener("DOMContentLoaded",yt);n.removeEventListener("load",yt);i.ready()}function et(){this.expando=i.expando+et.uid++}function lr(n,t,r){var u;if(void 0===r&&1===n.nodeType)if(u="data-"+t.replace(cr,"-$&").toLowerCase(),r=n.getAttribute(u),"string"==typeof r){try{r="true"===r?!0:"false"===r?!1:"null"===r?null:+r+""===r?+r:df.test(r)?i.parseJSON(r):r}catch(f){}e.set(n,t,r)}else r=void 0;return r}function vr(n,t,r,u){var h,e=1,l=20,c=u?function(){return u.cur()}:function(){return i.css(n,t,"")},s=c(),o=r&&r[3]||(i.cssNumber[t]?"":"px"),f=(i.cssNumber[t]||"px"!==o&&+s)&&ot.exec(i.css(n,t));if(f&&f[3]!==o){o=o||f[3];r=r||[];f=+s||1;do e=e||".5",f/=e,i.style(n,t,f+o);while(e!==(e=c()/s)&&1!==e&&--l)}return r&&(f=+f||+s||0,h=r[1]?f+(r[1]+1)*r[2]:+r[2],u&&(u.unit=o,u.start=f,u.end=h)),h}function o(n,t){var r="undefined"!=typeof n.getElementsByTagName?n.getElementsByTagName(t||"*"):"undefined"!=typeof n.querySelectorAll?n.querySelectorAll(t||"*"):[];return void 0===t||t&&i.nodeName(n,t)?i.merge([n],r):r}function ui(n,t){for(var i=0,u=n.length;u>i;i++)r.set(n[i],"globalEval",!t||r.get(t[i],"globalEval"))}function kr(n,t,r,u,f){for(var e,s,p,a,w,v,h=t.createDocumentFragment(),y=[],l=0,b=n.length;b>l;l++)if(e=n[l],e||0===e)if("object"===i.type(e))i.merge(y,e.nodeType?[e]:e);else if(br.test(e)){for(s=s||h.appendChild(t.createElement("div")),p=(pr.exec(e)||["",""])[1].toLowerCase(),a=c[p]||c._default,s.innerHTML=a[1]+i.htmlPrefilter(e)+a[2],v=a[0];v--;)s=s.lastChild;i.merge(y,s.childNodes);s=h.firstChild;s.textContent=""}else y.push(t.createTextNode(e));for(h.textContent="",l=0;e=y[l++];)if(u&&i.inArray(e,u)>-1)f&&f.push(e);else if(w=i.contains(e.ownerDocument,e),s=o(h.appendChild(e),"script"),w&&ui(s),r)for(v=0;e=s[v++];)wr.test(e.type||"")&&r.push(e);return h}function pt(){return!0}function nt(){return!1}function gr(){try{return u.activeElement}catch(n){}}function fi(n,t,r,u,f,e){var o,s;if("object"==typeof t){"string"!=typeof r&&(u=u||r,r=void 0);for(s in t)fi(n,s,r,u,t[s],e);return n}if(null==u&&null==f?(f=r,u=r=void 0):null==f&&("string"==typeof r?(f=u,u=void 0):(f=u,u=r,r=void 0)),f===!1)f=nt;else if(!f)return n;return 1===e&&(o=f,f=function(n){return i().off(n),o.apply(this,arguments)},f.guid=o.guid||(o.guid=i.guid++)),n.each(function(){i.event.add(this,t,f,u,r)})}function nu(n,t){return i.nodeName(n,"table")&&i.nodeName(11!==t.nodeType?t:t.firstChild,"tr")?n.getElementsByTagName("tbody")[0]||n.appendChild(n.ownerDocument.createElement("tbody")):n}function ee(n){return n.type=(null!==n.getAttribute("type"))+"/"+n.type,n}function oe(n){var t=ue.exec(n.type);return t?n.type=t[1]:n.removeAttribute("type"),n}function tu(n,t){var u,c,f,s,h,l,a,o;if(1===t.nodeType){if(r.hasData(n)&&(s=r.access(n),h=r.set(t,s),o=s.events)){delete h.handle;h.events={};for(f in o)for(u=0,c=o[f].length;c>u;u++)i.event.add(t,f,o[f][u])}e.hasData(n)&&(l=e.access(n),a=i.extend({},l),e.set(t,a))}}function se(n,t){var i=t.nodeName.toLowerCase();"input"===i&&yr.test(n.type)?t.checked=n.checked:"input"!==i&&"textarea"!==i||(t.defaultValue=n.defaultValue)}function b(n,t,u,e){t=gi.apply([],t);var l,p,c,a,s,w,h=0,v=n.length,d=v-1,y=t[0],k=i.isFunction(y);if(k||v>1&&"string"==typeof y&&!f.checkClone&&re.test(y))return n.each(function(i){var r=n.eq(i);k&&(t[0]=y.call(this,i,r.html()));b(r,t,u,e)});if(v&&(l=kr(t,n[0].ownerDocument,!1,n,e),p=l.firstChild,1===l.childNodes.length&&(l=p),p||e)){for(c=i.map(o(l,"script"),ee),a=c.length;v>h;h++)s=l,h!==d&&(s=i.clone(s,!0,!0),a&&i.merge(c,o(s,"script"))),u.call(n[h],s,h);if(a)for(w=c[c.length-1].ownerDocument,i.map(c,oe),h=0;a>h;h++)s=c[h],wr.test(s.type||"")&&!r.access(s,"globalEval")&&i.contains(w,s)&&(s.src?i._evalUrl&&i._evalUrl(s.src):i.globalEval(s.textContent.replace(fe,"")))}return n}function iu(n,t,r){for(var u,e=t?i.filter(t,n):n,f=0;null!=(u=e[f]);f++)r||1!==u.nodeType||i.cleanData(o(u)),u.parentNode&&(r&&i.contains(u.ownerDocument,u)&&ui(o(u,"script")),u.parentNode.removeChild(u));return n}function ru(n,t){var r=i(t.createElement(n)).appendTo(t.body),u=i.css(r[0],"display");return r.detach(),u}function oi(n){var r=u,t=ei[n];return t||(t=ru(n,r),"none"!==t&&t||(wt=(wt||i("<iframe frameborder='0' width='0' height='0'/>")).appendTo(r.documentElement),r=wt[0].contentDocument,r.write(),r.close(),t=ru(n,r),wt.detach()),ei[n]=t),t}function tt(n,t,r){var o,s,h,u,e=n.style;return r=r||bt(n),u=r?r.getPropertyValue(t)||r[t]:void 0,""!==u&&void 0!==u||i.contains(n.ownerDocument,n)||(u=i.style(n,t)),r&&!f.pixelMarginRight()&&si.test(u)&&uu.test(t)&&(o=e.width,s=e.minWidth,h=e.maxWidth,e.minWidth=e.maxWidth=e.width=u,u=r.width,e.width=o,e.minWidth=s,e.maxWidth=h),void 0!==u?u+"":u}function ci(n,t){return{get:function(){return n()?void delete this.get:(this.get=t).apply(this,arguments)}}}function su(n){if(n in ou)return n;for(var i=n[0].toUpperCase()+n.slice(1),t=eu.length;t--;)if(n=eu[t]+i,n in ou)return n}function hu(n,t,i){var r=ot.exec(t);return r?Math.max(0,r[2]-(i||0))+(r[3]||"px"):t}function cu(n,t,r,u,f){for(var e=r===(u?"border":"content")?4:"width"===t?1:0,o=0;4>e;e+=2)"margin"===r&&(o+=i.css(n,r+w[e],!0,f)),u?("content"===r&&(o-=i.css(n,"padding"+w[e],!0,f)),"margin"!==r&&(o-=i.css(n,"border"+w[e]+"Width",!0,f))):(o+=i.css(n,"padding"+w[e],!0,f),"padding"!==r&&(o+=i.css(n,"border"+w[e]+"Width",!0,f)));return o}function lu(t,r,e){var h=!0,o="width"===r?t.offsetWidth:t.offsetHeight,s=bt(t),c="border-box"===i.css(t,"boxSizing",!1,s);if(u.msFullscreenElement&&n.top!==n&&t.getClientRects().length&&(o=Math.round(100*t.getBoundingClientRect()[r])),0>=o||null==o){if(o=tt(t,r,s),(0>o||null==o)&&(o=t.style[r]),si.test(o))return o;h=c&&(f.boxSizingReliable()||o===t.style[r]);o=parseFloat(o)||0}return o+cu(t,r,e||(c?"border":"content"),h,s)+"px"}function au(n,t){for(var e,u,s,o=[],f=0,h=n.length;h>f;f++)u=n[f],u.style&&(o[f]=r.get(u,"olddisplay"),e=u.style.display,t?(o[f]||"none"!==e||(u.style.display=""),""===u.style.display&&st(u)&&(o[f]=r.access(u,"olddisplay",oi(u.nodeName)))):(s=st(u),"none"===e&&s||r.set(u,"olddisplay",s?e:i.css(u,"display"))));for(f=0;h>f;f++)u=n[f],u.style&&(t&&"none"!==u.style.display&&""!==u.style.display||(u.style.display=t?o[f]||"":"none"));return n}function s(n,t,i,r,u){return new s.prototype.init(n,t,i,r,u)}function pu(){return n.setTimeout(function(){it=void 0}),it=i.now()}function dt(n,t){var r,u=0,i={height:n};for(t=t?1:0;4>u;u+=2-t)r=w[u],i["margin"+r]=i["padding"+r]=n;return t&&(i.opacity=i.width=n),i}function wu(n,t,i){for(var u,f=(l.tweeners[t]||[]).concat(l.tweeners["*"]),r=0,e=f.length;e>r;r++)if(u=f[r].call(i,t,n))return u}function le(n,t,u){var f,a,p,v,o,w,h,b,l=this,y={},s=n.style,c=n.nodeType&&st(n),e=r.get(n,"fxshow");u.queue||(o=i._queueHooks(n,"fx"),null==o.unqueued&&(o.unqueued=0,w=o.empty.fire,o.empty.fire=function(){o.unqueued||w()}),o.unqueued++,l.always(function(){l.always(function(){o.unqueued--;i.queue(n,"fx").length||o.empty.fire()})}));1===n.nodeType&&("height"in t||"width"in t)&&(u.overflow=[s.overflow,s.overflowX,s.overflowY],h=i.css(n,"display"),b="none"===h?r.get(n,"olddisplay")||oi(n.nodeName):h,"inline"===b&&"none"===i.css(n,"float")&&(s.display="inline-block"));u.overflow&&(s.overflow="hidden",l.always(function(){s.overflow=u.overflow[0];s.overflowX=u.overflow[1];s.overflowY=u.overflow[2]}));for(f in t)if(a=t[f],vu.exec(a)){if(delete t[f],p=p||"toggle"===a,a===(c?"hide":"show")){if("show"!==a||!e||void 0===e[f])continue;c=!0}y[f]=e&&e[f]||i.style(n,f)}else h=void 0;if(i.isEmptyObject(y))"inline"===("none"===h?oi(n.nodeName):h)&&(s.display=h);else{e?"hidden"in e&&(c=e.hidden):e=r.access(n,"fxshow",{});p&&(e.hidden=!c);c?i(n).show():l.done(function(){i(n).hide()});l.done(function(){var t;r.remove(n,"fxshow");for(t in y)i.style(n,t,y[t])});for(f in y)v=wu(c?e[f]:0,f,l),f in e||(e[f]=v.start,c&&(v.end=v.start,v.start="width"===f||"height"===f?1:0))}}function ae(n,t){var r,f,e,u,o;for(r in n)if(f=i.camelCase(r),e=t[f],u=n[r],i.isArray(u)&&(e=u[1],u=n[r]=u[0]),r!==f&&(n[f]=u,delete n[r]),o=i.cssHooks[f],o&&"expand"in o){u=o.expand(u);delete n[f];for(r in u)r in n||(n[r]=u[r],t[r]=e)}else t[f]=e}function l(n,t,r){var e,o,s=0,a=l.prefilters.length,f=i.Deferred().always(function(){delete c.elem}),c=function(){if(o)return!1;for(var s=it||pu(),t=Math.max(0,u.startTime+u.duration-s),h=t/u.duration||0,i=1-h,r=0,e=u.tweens.length;e>r;r++)u.tweens[r].run(i);return f.notifyWith(n,[u,i,t]),1>i&&e?t:(f.resolveWith(n,[u]),!1)},u=f.promise({elem:n,props:i.extend({},t),opts:i.extend(!0,{specialEasing:{},easing:i.easing._default},r),originalProperties:t,originalOptions:r,startTime:it||pu(),duration:r.duration,tweens:[],createTween:function(t,r){var f=i.Tween(n,u.opts,t,r,u.opts.specialEasing[t]||u.opts.easing);return u.tweens.push(f),f},stop:function(t){var i=0,r=t?u.tweens.length:0;if(o)return this;for(o=!0;r>i;i++)u.tweens[i].run(1);return t?(f.notifyWith(n,[u,1,0]),f.resolveWith(n,[u,t])):f.rejectWith(n,[u,t]),this}}),h=u.props;for(ae(h,u.opts.specialEasing);a>s;s++)if(e=l.prefilters[s].call(u,n,h,u.opts))return i.isFunction(e.stop)&&(i._queueHooks(u.elem,u.opts.queue).stop=i.proxy(e.stop,e)),e;return i.map(h,wu,u),i.isFunction(u.opts.start)&&u.opts.start.call(n,u),i.fx.timer(i.extend(c,{elem:n,anim:u,queue:u.opts.queue})),u.progress(u.opts.progress).done(u.opts.done,u.opts.complete).fail(u.opts.fail).always(u.opts.always)}function k(n){return n.getAttribute&&n.getAttribute("class")||""}function ff(n){return function(t,r){"string"!=typeof t&&(r=t,t="*");var u,f=0,e=t.toLowerCase().match(h)||[];if(i.isFunction(r))while(u=e[f++])"+"===u[0]?(u=u.slice(1)||"*",(n[u]=n[u]||[]).unshift(r)):(n[u]=n[u]||[]).push(r)}}function ef(n,t,r,u){function e(s){var h;return f[s]=!0,i.each(n[s]||[],function(n,i){var s=i(t,r,u);return"string"!=typeof s||o||f[s]?o?!(h=s):void 0:(t.dataTypes.unshift(s),e(s),!1)}),h}var f={},o=n===yi;return e(t.dataTypes[0])||!f["*"]&&e("*")}function wi(n,t){var r,u,f=i.ajaxSettings.flatOptions||{};for(r in t)void 0!==t[r]&&((f[r]?n:u||(u={}))[r]=t[r]);return u&&i.extend(!0,n,u),n}function be(n,t,i){for(var e,u,f,o,s=n.contents,r=n.dataTypes;"*"===r[0];)r.shift(),void 0===e&&(e=n.mimeType||t.getResponseHeader("Content-Type"));if(e)for(u in s)if(s[u]&&s[u].test(e)){r.unshift(u);break}if(r[0]in i)f=r[0];else{for(u in i){if(!r[0]||n.converters[u+" "+r[0]]){f=u;break}o||(o=u)}f=f||o}if(f)return(f!==r[0]&&r.unshift(f),i[f])}function ke(n,t,i,r){var h,u,f,s,e,o={},c=n.dataTypes.slice();if(c[1])for(f in n.converters)o[f.toLowerCase()]=n.converters[f];for(u=c.shift();u;)if(n.responseFields[u]&&(i[n.responseFields[u]]=t),!e&&r&&n.dataFilter&&(t=n.dataFilter(t,n.dataType)),e=u,u=c.shift())if("*"===u)u=e;else if("*"!==e&&e!==u){if(f=o[e+" "+u]||o["* "+u],!f)for(h in o)if(s=h.split(" "),s[1]===u&&(f=o[e+" "+s[0]]||o["* "+s[0]])){f===!0?f=o[h]:o[h]!==!0&&(u=s[0],c.unshift(s[1]));break}if(f!==!0)if(f&&n.throws)t=f(t);else try{t=f(t)}catch(l){return{state:"parsererror",error:f?l:"No conversion from "+e+" to "+u}}}return{state:"success",data:t}}function bi(n,t,r,u){var f;if(i.isArray(t))i.each(t,function(t,i){r||ge.test(n)?u(n,i):bi(n+"["+("object"==typeof i&&null!=i?t:"")+"]",i,r,u)});else if(r||"object"!==i.type(t))u(n,t);else for(f in t)bi(n+"["+f+"]",t[f],r,u)}function hf(n){return i.isWindow(n)?n:9===n.nodeType&&n.defaultView}var y=[],u=n.document,v=y.slice,gi=y.concat,ti=y.push,lt=y.indexOf,at={},af=at.toString,ft=at.hasOwnProperty,f={},nr="2.2.3",i=function(n,t){return new i.fn.init(n,t)},vf=/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g,yf=/^-ms-/,pf=/-([\da-z])/gi,wf=function(n,t){return t.toUpperCase()},p,ur,fr,er,or,sr,h,vt,a,g,br,wt,ei,it,kt,vu,yu,bu,rt,ku,du,gt,gu,nf,li,sf,ut,ki,ni,di,cf,lf;i.fn=i.prototype={jquery:nr,constructor:i,selector:"",length:0,toArray:function(){return v.call(this)},get:function(n){return null!=n?0>n?this[n+this.length]:this[n]:v.call(this)},pushStack:function(n){var t=i.merge(this.constructor(),n);return t.prevObject=this,t.context=this.context,t},each:function(n){return i.each(this,n)},map:function(n){return this.pushStack(i.map(this,function(t,i){return n.call(t,i,t)}))},slice:function(){return this.pushStack(v.apply(this,arguments))},first:function(){return this.eq(0)},last:function(){return this.eq(-1)},eq:function(n){var i=this.length,t=+n+(0>n?i:0);return this.pushStack(t>=0&&i>t?[this[t]]:[])},end:function(){return this.prevObject||this.constructor()},push:ti,sort:y.sort,splice:y.splice};i.extend=i.fn.extend=function(){var e,f,r,t,o,s,n=arguments[0]||{},u=1,c=arguments.length,h=!1;for("boolean"==typeof n&&(h=n,n=arguments[u]||{},u++),"object"==typeof n||i.isFunction(n)||(n={}),u===c&&(n=this,u--);c>u;u++)if(null!=(e=arguments[u]))for(f in e)r=n[f],t=e[f],n!==t&&(h&&t&&(i.isPlainObject(t)||(o=i.isArray(t)))?(o?(o=!1,s=r&&i.isArray(r)?r:[]):s=r&&i.isPlainObject(r)?r:{},n[f]=i.extend(h,s,t)):void 0!==t&&(n[f]=t));return n};i.extend({expando:"jQuery"+(nr+Math.random()).replace(/\D/g,""),isReady:!0,error:function(n){throw new Error(n);},noop:function(){},isFunction:function(n){return"function"===i.type(n)},isArray:Array.isArray,isWindow:function(n){return null!=n&&n===n.window},isNumeric:function(n){var t=n&&n.toString();return!i.isArray(n)&&t-parseFloat(t)+1>=0},isPlainObject:function(n){var t;if("object"!==i.type(n)||n.nodeType||i.isWindow(n)||n.constructor&&!ft.call(n,"constructor")&&!ft.call(n.constructor.prototype||{},"isPrototypeOf"))return!1;for(t in n);return void 0===t||ft.call(n,t)},isEmptyObject:function(n){for(var t in n)return!1;return!0},type:function(n){return null==n?n+"":"object"==typeof n||"function"==typeof n?at[af.call(n)]||"object":typeof n},globalEval:function(n){var t,r=eval;n=i.trim(n);n&&(1===n.indexOf("use strict")?(t=u.createElement("script"),t.text=n,u.head.appendChild(t).parentNode.removeChild(t)):r(n))},camelCase:function(n){return n.replace(yf,"ms-").replace(pf,wf)},nodeName:function(n,t){return n.nodeName&&n.nodeName.toLowerCase()===t.toLowerCase()},each:function(n,t){var r,i=0;if(ii(n)){for(r=n.length;r>i;i++)if(t.call(n[i],i,n[i])===!1)break}else for(i in n)if(t.call(n[i],i,n[i])===!1)break;return n},trim:function(n){return null==n?"":(n+"").replace(vf,"")},makeArray:function(n,t){var r=t||[];return null!=n&&(ii(Object(n))?i.merge(r,"string"==typeof n?[n]:n):ti.call(r,n)),r},inArray:function(n,t,i){return null==t?-1:lt.call(t,n,i)},merge:function(n,t){for(var u=+t.length,i=0,r=n.length;u>i;i++)n[r++]=t[i];return n.length=r,n},grep:function(n,t,i){for(var u,f=[],r=0,e=n.length,o=!i;e>r;r++)u=!t(n[r],r),u!==o&&f.push(n[r]);return f},map:function(n,t,i){var e,u,r=0,f=[];if(ii(n))for(e=n.length;e>r;r++)u=t(n[r],r,i),null!=u&&f.push(u);else for(r in n)u=t(n[r],r,i),null!=u&&f.push(u);return gi.apply([],f)},guid:1,proxy:function(n,t){var u,f,r;return"string"==typeof t&&(u=n[t],t=n,n=u),i.isFunction(n)?(f=v.call(arguments,2),r=function(){return n.apply(t||this,f.concat(v.call(arguments)))},r.guid=n.guid=n.guid||i.guid++,r):void 0},now:Date.now,support:f});"function"==typeof Symbol&&(i.fn[Symbol.iterator]=y[Symbol.iterator]);i.each("Boolean Number String Function Array Date RegExp Object Error Symbol".split(" "),function(n,t){at["[object "+t+"]"]=t.toLowerCase()});p=function(n){function u(n,t,r,u){var l,w,a,s,nt,d,y,g,p=t&&t.ownerDocument,v=t?t.nodeType:9;if(r=r||[],"string"!=typeof n||!n||1!==v&&9!==v&&11!==v)return r;if(!u&&((t?t.ownerDocument||t:c)!==i&&b(t),t=t||i,h)){if(11!==v&&(d=sr.exec(n)))if(l=d[1]){if(9===v){if(!(a=t.getElementById(l)))return r;if(a.id===l)return r.push(a),r}else if(p&&(a=p.getElementById(l))&&et(t,a)&&a.id===l)return r.push(a),r}else{if(d[2])return k.apply(r,t.getElementsByTagName(n)),r;if((l=d[3])&&f.getElementsByClassName&&t.getElementsByClassName)return k.apply(r,t.getElementsByClassName(l)),r}if(f.qsa&&!lt[n+" "]&&(!o||!o.test(n))){if(1!==v)p=t,g=n;else if("object"!==t.nodeName.toLowerCase()){for((s=t.getAttribute("id"))?s=s.replace(hr,"\\$&"):t.setAttribute("id",s=e),y=ft(n),w=y.length,nt=yi.test(s)?"#"+s:"[id='"+s+"']";w--;)y[w]=nt+" "+yt(y[w]);g=y.join(",");p=gt.test(n)&&ii(t.parentNode)||t}if(g)try{return k.apply(r,p.querySelectorAll(g)),r}catch(tt){}finally{s===e&&t.removeAttribute("id")}}}return si(n.replace(at,"$1"),t,r,u)}function ni(){function n(r,u){return i.push(r+" ")>t.cacheLength&&delete n[i.shift()],n[r+" "]=u}var i=[];return n}function l(n){return n[e]=!0,n}function a(n){var t=i.createElement("div");try{return!!n(t)}catch(r){return!1}finally{t.parentNode&&t.parentNode.removeChild(t);t=null}}function ti(n,i){for(var r=n.split("|"),u=r.length;u--;)t.attrHandle[r[u]]=i}function wi(n,t){var i=t&&n,r=i&&1===n.nodeType&&1===t.nodeType&&(~t.sourceIndex||li)-(~n.sourceIndex||li);if(r)return r;if(i)while(i=i.nextSibling)if(i===t)return-1;return n?1:-1}function cr(n){return function(t){var i=t.nodeName.toLowerCase();return"input"===i&&t.type===n}}function lr(n){return function(t){var i=t.nodeName.toLowerCase();return("input"===i||"button"===i)&&t.type===n}}function it(n){return l(function(t){return t=+t,l(function(i,r){for(var u,f=n([],i.length,t),e=f.length;e--;)i[u=f[e]]&&(i[u]=!(r[u]=i[u]))})})}function ii(n){return n&&"undefined"!=typeof n.getElementsByTagName&&n}function bi(){}function yt(n){for(var t=0,r=n.length,i="";r>t;t++)i+=n[t].value;return i}function ri(n,t,i){var r=t.dir,u=i&&"parentNode"===r,f=ki++;return t.first?function(t,i,f){while(t=t[r])if(1===t.nodeType||u)return n(t,i,f)}:function(t,i,o){var s,h,c,l=[v,f];if(o){while(t=t[r])if((1===t.nodeType||u)&&n(t,i,o))return!0}else while(t=t[r])if(1===t.nodeType||u){if(c=t[e]||(t[e]={}),h=c[t.uniqueID]||(c[t.uniqueID]={}),(s=h[r])&&s[0]===v&&s[1]===f)return l[2]=s[2];if(h[r]=l,l[2]=n(t,i,o))return!0}}}function ui(n){return n.length>1?function(t,i,r){for(var u=n.length;u--;)if(!n[u](t,i,r))return!1;return!0}:n[0]}function ar(n,t,i){for(var r=0,f=t.length;f>r;r++)u(n,t[r],i);return i}function pt(n,t,i,r,u){for(var e,o=[],f=0,s=n.length,h=null!=t;s>f;f++)(e=n[f])&&(i&&!i(e,r,u)||(o.push(e),h&&t.push(f)));return o}function fi(n,t,i,r,u,f){return r&&!r[e]&&(r=fi(r)),u&&!u[e]&&(u=fi(u,f)),l(function(f,e,o,s){var l,c,a,p=[],y=[],w=e.length,b=f||ar(t||"*",o.nodeType?[o]:o,[]),v=!n||!f&&t?b:pt(b,p,n,o,s),h=i?u||(f?n:w||r)?[]:e:v;if(i&&i(v,h,o,s),r)for(l=pt(h,y),r(l,[],o,s),c=l.length;c--;)(a=l[c])&&(h[y[c]]=!(v[y[c]]=a));if(f){if(u||n){if(u){for(l=[],c=h.length;c--;)(a=h[c])&&l.push(v[c]=a);u(null,h=[],l,s)}for(c=h.length;c--;)(a=h[c])&&(l=u?nt(f,a):p[c])>-1&&(f[l]=!(e[l]=a))}}else h=pt(h===e?h.splice(w,h.length):h),u?u(null,e,h,s):k.apply(e,h)})}function ei(n){for(var o,u,r,s=n.length,h=t.relative[n[0].type],c=h||t.relative[" "],i=h?1:0,l=ri(function(n){return n===o},c,!0),a=ri(function(n){return nt(o,n)>-1},c,!0),f=[function(n,t,i){var r=!h&&(i||t!==ht)||((o=t).nodeType?l(n,t,i):a(n,t,i));return o=null,r}];s>i;i++)if(u=t.relative[n[i].type])f=[ri(ui(f),u)];else{if(u=t.filter[n[i].type].apply(null,n[i].matches),u[e]){for(r=++i;s>r;r++)if(t.relative[n[r].type])break;return fi(i>1&&ui(f),i>1&&yt(n.slice(0,i-1).concat({value:" "===n[i-2].type?"*":""})).replace(at,"$1"),u,r>i&&ei(n.slice(i,r)),s>r&&ei(n=n.slice(r)),s>r&&yt(n))}f.push(u)}return ui(f)}function vr(n,r){var f=r.length>0,e=n.length>0,o=function(o,s,c,l,a){var y,nt,d,g=0,p="0",tt=o&&[],w=[],it=ht,rt=o||e&&t.find.TAG("*",a),ut=v+=null==it?1:Math.random()||.1,ft=rt.length;for(a&&(ht=s===i||s||a);p!==ft&&null!=(y=rt[p]);p++){if(e&&y){for(nt=0,s||y.ownerDocument===i||(b(y),c=!h);d=n[nt++];)if(d(y,s||i,c)){l.push(y);break}a&&(v=ut)}f&&((y=!d&&y)&&g--,o&&tt.push(y))}if(g+=p,f&&p!==g){for(nt=0;d=r[nt++];)d(tt,w,s,c);if(o){if(g>0)while(p--)tt[p]||w[p]||(w[p]=gi.call(l));w=pt(w)}k.apply(l,w);a&&!o&&w.length>0&&g+r.length>1&&u.uniqueSort(l)}return a&&(v=ut,ht=it),tt};return f?l(o):o}var rt,f,t,st,oi,ft,wt,si,ht,w,ut,b,i,s,h,o,d,ct,et,e="sizzle"+1*new Date,c=n.document,v=0,ki=0,hi=ni(),ci=ni(),lt=ni(),bt=function(n,t){return n===t&&(ut=!0),0},li=-2147483648,di={}.hasOwnProperty,g=[],gi=g.pop,nr=g.push,k=g.push,ai=g.slice,nt=function(n,t){for(var i=0,r=n.length;r>i;i++)if(n[i]===t)return i;return-1},kt="checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped",r="[\\x20\\t\\r\\n\\f]",tt="(?:\\\\.|[\\w-]|[^\\x00-\\xa0])+",vi="\\["+r+"*("+tt+")(?:"+r+"*([*^$|!~]?=)"+r+"*(?:'((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\"|("+tt+"))|)"+r+"*\\]",dt=":("+tt+")(?:\\((('((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\")|((?:\\\\.|[^\\\\()[\\]]|"+vi+")*)|.*)\\)|)",tr=new RegExp(r+"+","g"),at=new RegExp("^"+r+"+|((?:^|[^\\\\])(?:\\\\.)*)"+r+"+$","g"),ir=new RegExp("^"+r+"*,"+r+"*"),rr=new RegExp("^"+r+"*([>+~]|"+r+")"+r+"*"),ur=new RegExp("="+r+"*([^\\]'\"]*?)"+r+"*\\]","g"),fr=new RegExp(dt),yi=new RegExp("^"+tt+"$"),vt={ID:new RegExp("^#("+tt+")"),CLASS:new RegExp("^\\.("+tt+")"),TAG:new RegExp("^("+tt+"|[*])"),ATTR:new RegExp("^"+vi),PSEUDO:new RegExp("^"+dt),CHILD:new RegExp("^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\("+r+"*(even|odd|(([+-]|)(\\d*)n|)"+r+"*(?:([+-]|)"+r+"*(\\d+)|))"+r+"*\\)|)","i"),bool:new RegExp("^(?:"+kt+")$","i"),needsContext:new RegExp("^"+r+"*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\("+r+"*((?:-\\d)?\\d*)"+r+"*\\)|)(?=[^-]|$)","i")},er=/^(?:input|select|textarea|button)$/i,or=/^h\d$/i,ot=/^[^{]+\{\s*\[native \w/,sr=/^(?:#([\w-]+)|(\w+)|\.([\w-]+))$/,gt=/[+~]/,hr=/'|\\/g,y=new RegExp("\\\\([\\da-f]{1,6}"+r+"?|("+r+")|.)","ig"),p=function(n,t,i){var r="0x"+t-65536;return r!==r||i?t:0>r?String.fromCharCode(r+65536):String.fromCharCode(r>>10|55296,1023&r|56320)},pi=function(){b()};try{k.apply(g=ai.call(c.childNodes),c.childNodes);g[c.childNodes.length].nodeType}catch(yr){k={apply:g.length?function(n,t){nr.apply(n,ai.call(t))}:function(n,t){for(var i=n.length,r=0;n[i++]=t[r++];);n.length=i-1}}}f=u.support={};oi=u.isXML=function(n){var t=n&&(n.ownerDocument||n).documentElement;return t?"HTML"!==t.nodeName:!1};b=u.setDocument=function(n){var v,u,l=n?n.ownerDocument||n:c;return l!==i&&9===l.nodeType&&l.documentElement?(i=l,s=i.documentElement,h=!oi(i),(u=i.defaultView)&&u.top!==u&&(u.addEventListener?u.addEventListener("unload",pi,!1):u.attachEvent&&u.attachEvent("onunload",pi)),f.attributes=a(function(n){return n.className="i",!n.getAttribute("className")}),f.getElementsByTagName=a(function(n){return n.appendChild(i.createComment("")),!n.getElementsByTagName("*").length}),f.getElementsByClassName=ot.test(i.getElementsByClassName),f.getById=a(function(n){return s.appendChild(n).id=e,!i.getElementsByName||!i.getElementsByName(e).length}),f.getById?(t.find.ID=function(n,t){if("undefined"!=typeof t.getElementById&&h){var i=t.getElementById(n);return i?[i]:[]}},t.filter.ID=function(n){var t=n.replace(y,p);return function(n){return n.getAttribute("id")===t}}):(delete t.find.ID,t.filter.ID=function(n){var t=n.replace(y,p);return function(n){var i="undefined"!=typeof n.getAttributeNode&&n.getAttributeNode("id");return i&&i.value===t}}),t.find.TAG=f.getElementsByTagName?function(n,t){return"undefined"!=typeof t.getElementsByTagName?t.getElementsByTagName(n):f.qsa?t.querySelectorAll(n):void 0}:function(n,t){var i,r=[],f=0,u=t.getElementsByTagName(n);if("*"===n){while(i=u[f++])1===i.nodeType&&r.push(i);return r}return u},t.find.CLASS=f.getElementsByClassName&&function(n,t){if("undefined"!=typeof t.getElementsByClassName&&h)return t.getElementsByClassName(n)},d=[],o=[],(f.qsa=ot.test(i.querySelectorAll))&&(a(function(n){s.appendChild(n).innerHTML="<a id='"+e+"'><\/a><select id='"+e+"-\r\\' msallowcapture=''><option selected=''><\/option><\/select>";n.querySelectorAll("[msallowcapture^='']").length&&o.push("[*^$]="+r+"*(?:''|\"\")");n.querySelectorAll("[selected]").length||o.push("\\["+r+"*(?:value|"+kt+")");n.querySelectorAll("[id~="+e+"-]").length||o.push("~=");n.querySelectorAll(":checked").length||o.push(":checked");n.querySelectorAll("a#"+e+"+*").length||o.push(".#.+[+~]")}),a(function(n){var t=i.createElement("input");t.setAttribute("type","hidden");n.appendChild(t).setAttribute("name","D");n.querySelectorAll("[name=d]").length&&o.push("name"+r+"*[*^$|!~]?=");n.querySelectorAll(":enabled").length||o.push(":enabled",":disabled");n.querySelectorAll("*,:x");o.push(",.*:")})),(f.matchesSelector=ot.test(ct=s.matches||s.webkitMatchesSelector||s.mozMatchesSelector||s.oMatchesSelector||s.msMatchesSelector))&&a(function(n){f.disconnectedMatch=ct.call(n,"div");ct.call(n,"[s!='']:x");d.push("!=",dt)}),o=o.length&&new RegExp(o.join("|")),d=d.length&&new RegExp(d.join("|")),v=ot.test(s.compareDocumentPosition),et=v||ot.test(s.contains)?function(n,t){var r=9===n.nodeType?n.documentElement:n,i=t&&t.parentNode;return n===i||!(!i||1!==i.nodeType||!(r.contains?r.contains(i):n.compareDocumentPosition&&16&n.compareDocumentPosition(i)))}:function(n,t){if(t)while(t=t.parentNode)if(t===n)return!0;return!1},bt=v?function(n,t){if(n===t)return ut=!0,0;var r=!n.compareDocumentPosition-!t.compareDocumentPosition;return r?r:(r=(n.ownerDocument||n)===(t.ownerDocument||t)?n.compareDocumentPosition(t):1,1&r||!f.sortDetached&&t.compareDocumentPosition(n)===r?n===i||n.ownerDocument===c&&et(c,n)?-1:t===i||t.ownerDocument===c&&et(c,t)?1:w?nt(w,n)-nt(w,t):0:4&r?-1:1)}:function(n,t){if(n===t)return ut=!0,0;var r,u=0,o=n.parentNode,s=t.parentNode,f=[n],e=[t];if(!o||!s)return n===i?-1:t===i?1:o?-1:s?1:w?nt(w,n)-nt(w,t):0;if(o===s)return wi(n,t);for(r=n;r=r.parentNode;)f.unshift(r);for(r=t;r=r.parentNode;)e.unshift(r);while(f[u]===e[u])u++;return u?wi(f[u],e[u]):f[u]===c?-1:e[u]===c?1:0},i):i};u.matches=function(n,t){return u(n,null,null,t)};u.matchesSelector=function(n,t){if((n.ownerDocument||n)!==i&&b(n),t=t.replace(ur,"='$1']"),f.matchesSelector&&h&&!lt[t+" "]&&(!d||!d.test(t))&&(!o||!o.test(t)))try{var r=ct.call(n,t);if(r||f.disconnectedMatch||n.document&&11!==n.document.nodeType)return r}catch(e){}return u(t,i,null,[n]).length>0};u.contains=function(n,t){return(n.ownerDocument||n)!==i&&b(n),et(n,t)};u.attr=function(n,r){(n.ownerDocument||n)!==i&&b(n);var e=t.attrHandle[r.toLowerCase()],u=e&&di.call(t.attrHandle,r.toLowerCase())?e(n,r,!h):void 0;return void 0!==u?u:f.attributes||!h?n.getAttribute(r):(u=n.getAttributeNode(r))&&u.specified?u.value:null};u.error=function(n){throw new Error("Syntax error, unrecognized expression: "+n);};u.uniqueSort=function(n){var r,u=[],t=0,i=0;if(ut=!f.detectDuplicates,w=!f.sortStable&&n.slice(0),n.sort(bt),ut){while(r=n[i++])r===n[i]&&(t=u.push(i));while(t--)n.splice(u[t],1)}return w=null,n};st=u.getText=function(n){var r,i="",u=0,t=n.nodeType;if(t){if(1===t||9===t||11===t){if("string"==typeof n.textContent)return n.textContent;for(n=n.firstChild;n;n=n.nextSibling)i+=st(n)}else if(3===t||4===t)return n.nodeValue}else while(r=n[u++])i+=st(r);return i};t=u.selectors={cacheLength:50,createPseudo:l,match:vt,attrHandle:{},find:{},relative:{">":{dir:"parentNode",first:!0}," ":{dir:"parentNode"},"+":{dir:"previousSibling",first:!0},"~":{dir:"previousSibling"}},preFilter:{ATTR:function(n){return n[1]=n[1].replace(y,p),n[3]=(n[3]||n[4]||n[5]||"").replace(y,p),"~="===n[2]&&(n[3]=" "+n[3]+" "),n.slice(0,4)},CHILD:function(n){return n[1]=n[1].toLowerCase(),"nth"===n[1].slice(0,3)?(n[3]||u.error(n[0]),n[4]=+(n[4]?n[5]+(n[6]||1):2*("even"===n[3]||"odd"===n[3])),n[5]=+(n[7]+n[8]||"odd"===n[3])):n[3]&&u.error(n[0]),n},PSEUDO:function(n){var i,t=!n[6]&&n[2];return vt.CHILD.test(n[0])?null:(n[3]?n[2]=n[4]||n[5]||"":t&&fr.test(t)&&(i=ft(t,!0))&&(i=t.indexOf(")",t.length-i)-t.length)&&(n[0]=n[0].slice(0,i),n[2]=t.slice(0,i)),n.slice(0,3))}},filter:{TAG:function(n){var t=n.replace(y,p).toLowerCase();return"*"===n?function(){return!0}:function(n){return n.nodeName&&n.nodeName.toLowerCase()===t}},CLASS:function(n){var t=hi[n+" "];return t||(t=new RegExp("(^|"+r+")"+n+"("+r+"|$)"))&&hi(n,function(n){return t.test("string"==typeof n.className&&n.className||"undefined"!=typeof n.getAttribute&&n.getAttribute("class")||"")})},ATTR:function(n,t,i){return function(r){var f=u.attr(r,n);return null==f?"!="===t:t?(f+="","="===t?f===i:"!="===t?f!==i:"^="===t?i&&0===f.indexOf(i):"*="===t?i&&f.indexOf(i)>-1:"$="===t?i&&f.slice(-i.length)===i:"~="===t?(" "+f.replace(tr," ")+" ").indexOf(i)>-1:"|="===t?f===i||f.slice(0,i.length+1)===i+"-":!1):!0}},CHILD:function(n,t,i,r,u){var s="nth"!==n.slice(0,3),o="last"!==n.slice(-4),f="of-type"===t;return 1===r&&0===u?function(n){return!!n.parentNode}:function(t,i,h){var p,w,y,c,a,b,k=s!==o?"nextSibling":"previousSibling",d=t.parentNode,nt=f&&t.nodeName.toLowerCase(),g=!h&&!f,l=!1;if(d){if(s){while(k){for(c=t;c=c[k];)if(f?c.nodeName.toLowerCase()===nt:1===c.nodeType)return!1;b=k="only"===n&&!b&&"nextSibling"}return!0}if(b=[o?d.firstChild:d.lastChild],o&&g){for(c=d,y=c[e]||(c[e]={}),w=y[c.uniqueID]||(y[c.uniqueID]={}),p=w[n]||[],a=p[0]===v&&p[1],l=a&&p[2],c=a&&d.childNodes[a];c=++a&&c&&c[k]||(l=a=0)||b.pop();)if(1===c.nodeType&&++l&&c===t){w[n]=[v,a,l];break}}else if(g&&(c=t,y=c[e]||(c[e]={}),w=y[c.uniqueID]||(y[c.uniqueID]={}),p=w[n]||[],a=p[0]===v&&p[1],l=a),l===!1)while(c=++a&&c&&c[k]||(l=a=0)||b.pop())if((f?c.nodeName.toLowerCase()===nt:1===c.nodeType)&&++l&&(g&&(y=c[e]||(c[e]={}),w=y[c.uniqueID]||(y[c.uniqueID]={}),w[n]=[v,l]),c===t))break;return l-=u,l===r||l%r==0&&l/r>=0}}},PSEUDO:function(n,i){var f,r=t.pseudos[n]||t.setFilters[n.toLowerCase()]||u.error("unsupported pseudo: "+n);return r[e]?r(i):r.length>1?(f=[n,n,"",i],t.setFilters.hasOwnProperty(n.toLowerCase())?l(function(n,t){for(var u,f=r(n,i),e=f.length;e--;)u=nt(n,f[e]),n[u]=!(t[u]=f[e])}):function(n){return r(n,0,f)}):r}},pseudos:{not:l(function(n){var t=[],r=[],i=wt(n.replace(at,"$1"));return i[e]?l(function(n,t,r,u){for(var e,o=i(n,null,u,[]),f=n.length;f--;)(e=o[f])&&(n[f]=!(t[f]=e))}):function(n,u,f){return t[0]=n,i(t,null,f,r),t[0]=null,!r.pop()}}),has:l(function(n){return function(t){return u(n,t).length>0}}),contains:l(function(n){return n=n.replace(y,p),function(t){return(t.textContent||t.innerText||st(t)).indexOf(n)>-1}}),lang:l(function(n){return yi.test(n||"")||u.error("unsupported lang: "+n),n=n.replace(y,p).toLowerCase(),function(t){var i;do if(i=h?t.lang:t.getAttribute("xml:lang")||t.getAttribute("lang"))return i=i.toLowerCase(),i===n||0===i.indexOf(n+"-");while((t=t.parentNode)&&1===t.nodeType);return!1}}),target:function(t){var i=n.location&&n.location.hash;return i&&i.slice(1)===t.id},root:function(n){return n===s},focus:function(n){return n===i.activeElement&&(!i.hasFocus||i.hasFocus())&&!!(n.type||n.href||~n.tabIndex)},enabled:function(n){return n.disabled===!1},disabled:function(n){return n.disabled===!0},checked:function(n){var t=n.nodeName.toLowerCase();return"input"===t&&!!n.checked||"option"===t&&!!n.selected},selected:function(n){return n.parentNode&&n.parentNode.selectedIndex,n.selected===!0},empty:function(n){for(n=n.firstChild;n;n=n.nextSibling)if(n.nodeType<6)return!1;return!0},parent:function(n){return!t.pseudos.empty(n)},header:function(n){return or.test(n.nodeName)},input:function(n){return er.test(n.nodeName)},button:function(n){var t=n.nodeName.toLowerCase();return"input"===t&&"button"===n.type||"button"===t},text:function(n){var t;return"input"===n.nodeName.toLowerCase()&&"text"===n.type&&(null==(t=n.getAttribute("type"))||"text"===t.toLowerCase())},first:it(function(){return[0]}),last:it(function(n,t){return[t-1]}),eq:it(function(n,t,i){return[0>i?i+t:i]}),even:it(function(n,t){for(var i=0;t>i;i+=2)n.push(i);return n}),odd:it(function(n,t){for(var i=1;t>i;i+=2)n.push(i);return n}),lt:it(function(n,t,i){for(var r=0>i?i+t:i;--r>=0;)n.push(r);return n}),gt:it(function(n,t,i){for(var r=0>i?i+t:i;++r<t;)n.push(r);return n})}};t.pseudos.nth=t.pseudos.eq;for(rt in{radio:!0,checkbox:!0,file:!0,password:!0,image:!0})t.pseudos[rt]=cr(rt);for(rt in{submit:!0,reset:!0})t.pseudos[rt]=lr(rt);return bi.prototype=t.filters=t.pseudos,t.setFilters=new bi,ft=u.tokenize=function(n,i){var e,f,s,o,r,h,c,l=ci[n+" "];if(l)return i?0:l.slice(0);for(r=n,h=[],c=t.preFilter;r;){(!e||(f=ir.exec(r)))&&(f&&(r=r.slice(f[0].length)||r),h.push(s=[]));e=!1;(f=rr.exec(r))&&(e=f.shift(),s.push({value:e,type:f[0].replace(at," ")}),r=r.slice(e.length));for(o in t.filter)(f=vt[o].exec(r))&&(!c[o]||(f=c[o](f)))&&(e=f.shift(),s.push({value:e,type:o,matches:f}),r=r.slice(e.length));if(!e)break}return i?r.length:r?u.error(n):ci(n,h).slice(0)},wt=u.compile=function(n,t){var r,u=[],f=[],i=lt[n+" "];if(!i){for(t||(t=ft(n)),r=t.length;r--;)i=ei(t[r]),i[e]?u.push(i):f.push(i);i=lt(n,vr(f,u));i.selector=n}return i},si=u.select=function(n,i,r,u){var s,e,o,a,v,l="function"==typeof n&&n,c=!u&&ft(n=l.selector||n);if(r=r||[],1===c.length){if(e=c[0]=c[0].slice(0),e.length>2&&"ID"===(o=e[0]).type&&f.getById&&9===i.nodeType&&h&&t.relative[e[1].type]){if(i=(t.find.ID(o.matches[0].replace(y,p),i)||[])[0],!i)return r;l&&(i=i.parentNode);n=n.slice(e.shift().value.length)}for(s=vt.needsContext.test(n)?0:e.length;s--;){if(o=e[s],t.relative[a=o.type])break;if((v=t.find[a])&&(u=v(o.matches[0].replace(y,p),gt.test(e[0].type)&&ii(i.parentNode)||i))){if(e.splice(s,1),n=u.length&&yt(e),!n)return k.apply(r,u),r;break}}}return(l||wt(n,c))(u,i,!h,r,!i||gt.test(n)&&ii(i.parentNode)||i),r},f.sortStable=e.split("").sort(bt).join("")===e,f.detectDuplicates=!!ut,b(),f.sortDetached=a(function(n){return 1&n.compareDocumentPosition(i.createElement("div"))}),a(function(n){return n.innerHTML="<a href='#'><\/a>","#"===n.firstChild.getAttribute("href")})||ti("type|href|height|width",function(n,t,i){if(!i)return n.getAttribute(t,"type"===t.toLowerCase()?1:2)}),f.attributes&&a(function(n){return n.innerHTML="<input/>",n.firstChild.setAttribute("value",""),""===n.firstChild.getAttribute("value")})||ti("value",function(n,t,i){if(!i&&"input"===n.nodeName.toLowerCase())return n.defaultValue}),a(function(n){return null==n.getAttribute("disabled")})||ti(kt,function(n,t,i){var r;if(!i)return n[t]===!0?t.toLowerCase():(r=n.getAttributeNode(t))&&r.specified?r.value:null}),u}(n);i.find=p;i.expr=p.selectors;i.expr[":"]=i.expr.pseudos;i.uniqueSort=i.unique=p.uniqueSort;i.text=p.getText;i.isXMLDoc=p.isXML;i.contains=p.contains;var d=function(n,t,r){for(var u=[],f=void 0!==r;(n=n[t])&&9!==n.nodeType;)if(1===n.nodeType){if(f&&i(n).is(r))break;u.push(n)}return u},tr=function(n,t){for(var i=[];n;n=n.nextSibling)1===n.nodeType&&n!==t&&i.push(n);return i},ir=i.expr.match.needsContext,rr=/^<([\w-]+)\s*\/?>(?:<\/\1>|)$/,bf=/^.[^:#\[\.,]*$/;i.filter=function(n,t,r){var u=t[0];return r&&(n=":not("+n+")"),1===t.length&&1===u.nodeType?i.find.matchesSelector(u,n)?[u]:[]:i.find.matches(n,i.grep(t,function(n){return 1===n.nodeType}))};i.fn.extend({find:function(n){var t,u=this.length,r=[],f=this;if("string"!=typeof n)return this.pushStack(i(n).filter(function(){for(t=0;u>t;t++)if(i.contains(f[t],this))return!0}));for(t=0;u>t;t++)i.find(n,f[t],r);return r=this.pushStack(u>1?i.unique(r):r),r.selector=this.selector?this.selector+" "+n:n,r},filter:function(n){return this.pushStack(ri(this,n||[],!1))},not:function(n){return this.pushStack(ri(this,n||[],!0))},is:function(n){return!!ri(this,"string"==typeof n&&ir.test(n)?i(n):n||[],!1).length}});fr=/^(?:\s*(<[\w\W]+>)[^>]*|#([\w-]*))$/;er=i.fn.init=function(n,t,r){var f,e;if(!n)return this;if(r=r||ur,"string"==typeof n){if(f="<"===n[0]&&">"===n[n.length-1]&&n.length>=3?[null,n,null]:fr.exec(n),!f||!f[1]&&t)return!t||t.jquery?(t||r).find(n):this.constructor(t).find(n);if(f[1]){if(t=t instanceof i?t[0]:t,i.merge(this,i.parseHTML(f[1],t&&t.nodeType?t.ownerDocument||t:u,!0)),rr.test(f[1])&&i.isPlainObject(t))for(f in t)i.isFunction(this[f])?this[f](t[f]):this.attr(f,t[f]);return this}return e=u.getElementById(f[2]),e&&e.parentNode&&(this.length=1,this[0]=e),this.context=u,this.selector=n,this}return n.nodeType?(this.context=this[0]=n,this.length=1,this):i.isFunction(n)?void 0!==r.ready?r.ready(n):n(i):(void 0!==n.selector&&(this.selector=n.selector,this.context=n.context),i.makeArray(n,this))};er.prototype=i.fn;ur=i(u);or=/^(?:parents|prev(?:Until|All))/;sr={children:!0,contents:!0,next:!0,prev:!0};i.fn.extend({has:function(n){var t=i(n,this),r=t.length;return this.filter(function(){for(var n=0;r>n;n++)if(i.contains(this,t[n]))return!0})},closest:function(n,t){for(var r,f=0,o=this.length,u=[],e=ir.test(n)||"string"!=typeof n?i(n,t||this.context):0;o>f;f++)for(r=this[f];r&&r!==t;r=r.parentNode)if(r.nodeType<11&&(e?e.index(r)>-1:1===r.nodeType&&i.find.matchesSelector(r,n))){u.push(r);break}return this.pushStack(u.length>1?i.uniqueSort(u):u)},index:function(n){return n?"string"==typeof n?lt.call(i(n),this[0]):lt.call(this,n.jquery?n[0]:n):this[0]&&this[0].parentNode?this.first().prevAll().length:-1},add:function(n,t){return this.pushStack(i.uniqueSort(i.merge(this.get(),i(n,t))))},addBack:function(n){return this.add(null==n?this.prevObject:this.prevObject.filter(n))}});i.each({parent:function(n){var t=n.parentNode;return t&&11!==t.nodeType?t:null},parents:function(n){return d(n,"parentNode")},parentsUntil:function(n,t,i){return d(n,"parentNode",i)},next:function(n){return hr(n,"nextSibling")},prev:function(n){return hr(n,"previousSibling")},nextAll:function(n){return d(n,"nextSibling")},prevAll:function(n){return d(n,"previousSibling")},nextUntil:function(n,t,i){return d(n,"nextSibling",i)},prevUntil:function(n,t,i){return d(n,"previousSibling",i)},siblings:function(n){return tr((n.parentNode||{}).firstChild,n)},children:function(n){return tr(n.firstChild)},contents:function(n){return n.contentDocument||i.merge([],n.childNodes)}},function(n,t){i.fn[n]=function(r,u){var f=i.map(this,t,r);return"Until"!==n.slice(-5)&&(u=r),u&&"string"==typeof u&&(f=i.filter(u,f)),this.length>1&&(sr[n]||i.uniqueSort(f),or.test(n)&&f.reverse()),this.pushStack(f)}});h=/\S+/g;i.Callbacks=function(n){n="string"==typeof n?kf(n):i.extend({},n);var o,r,h,f,t=[],e=[],u=-1,c=function(){for(f=n.once,h=o=!0;e.length;u=-1)for(r=e.shift();++u<t.length;)t[u].apply(r[0],r[1])===!1&&n.stopOnFalse&&(u=t.length,r=!1);n.memory||(r=!1);o=!1;f&&(t=r?[]:"")},s={add:function(){return t&&(r&&!o&&(u=t.length-1,e.push(r)),function f(r){i.each(r,function(r,u){i.isFunction(u)?n.unique&&s.has(u)||t.push(u):u&&u.length&&"string"!==i.type(u)&&f(u)})}(arguments),r&&!o&&c()),this},remove:function(){return i.each(arguments,function(n,r){for(var f;(f=i.inArray(r,t,f))>-1;)t.splice(f,1),u>=f&&u--}),this},has:function(n){return n?i.inArray(n,t)>-1:t.length>0},empty:function(){return t&&(t=[]),this},disable:function(){return f=e=[],t=r="",this},disabled:function(){return!t},lock:function(){return f=e=[],r||(t=r=""),this},locked:function(){return!!f},fireWith:function(n,t){return f||(t=t||[],t=[n,t.slice?t.slice():t],e.push(t),o||c()),this},fire:function(){return s.fireWith(this,arguments),this},fired:function(){return!!h}};return s};i.extend({Deferred:function(n){var u=[["resolve","done",i.Callbacks("once memory"),"resolved"],["reject","fail",i.Callbacks("once memory"),"rejected"],["notify","progress",i.Callbacks("memory")]],f="pending",r={state:function(){return f},always:function(){return t.done(arguments).fail(arguments),this},then:function(){var n=arguments;return i.Deferred(function(f){i.each(u,function(u,e){var o=i.isFunction(n[u])&&n[u];t[e[1]](function(){var n=o&&o.apply(this,arguments);n&&i.isFunction(n.promise)?n.promise().progress(f.notify).done(f.resolve).fail(f.reject):f[e[0]+"With"](this===r?f.promise():this,o?[n]:arguments)})});n=null}).promise()},promise:function(n){return null!=n?i.extend(n,r):r}},t={};return r.pipe=r.then,i.each(u,function(n,i){var e=i[2],o=i[3];r[i[1]]=e.add;o&&e.add(function(){f=o},u[1^n][2].disable,u[2][2].lock);t[i[0]]=function(){return t[i[0]+"With"](this===t?r:this,arguments),this};t[i[0]+"With"]=e.fireWith}),r.promise(t),n&&n.call(t,t),t},when:function(n){var t=0,u=v.call(arguments),r=u.length,e=1!==r||n&&i.isFunction(n.promise)?r:0,f=1===e?n:i.Deferred(),h=function(n,t,i){return function(r){t[n]=this;i[n]=arguments.length>1?v.call(arguments):r;i===o?f.notifyWith(t,i):--e||f.resolveWith(t,i)}},o,c,s;if(r>1)for(o=new Array(r),c=new Array(r),s=new Array(r);r>t;t++)u[t]&&i.isFunction(u[t].promise)?u[t].promise().progress(h(t,c,o)).done(h(t,s,u)).fail(f.reject):--e;return e||f.resolveWith(s,u),f.promise()}});i.fn.ready=function(n){return i.ready.promise().done(n),this};i.extend({isReady:!1,readyWait:1,holdReady:function(n){n?i.readyWait++:i.ready(!0)},ready:function(n){(n===!0?--i.readyWait:i.isReady)||(i.isReady=!0,n!==!0&&--i.readyWait>0||(vt.resolveWith(u,[i]),i.fn.triggerHandler&&(i(u).triggerHandler("ready"),i(u).off("ready"))))}});i.ready.promise=function(t){return vt||(vt=i.Deferred(),"complete"===u.readyState||"loading"!==u.readyState&&!u.documentElement.doScroll?n.setTimeout(i.ready):(u.addEventListener("DOMContentLoaded",yt),n.addEventListener("load",yt))),vt.promise(t)};i.ready.promise();a=function(n,t,r,u,f,e,o){var s=0,c=n.length,h=null==r;if("object"===i.type(r)){f=!0;for(s in r)a(n,t,s,r[s],!0,e,o)}else if(void 0!==u&&(f=!0,i.isFunction(u)||(o=!0),h&&(o?(t.call(n,u),t=null):(h=t,t=function(n,t,r){return h.call(i(n),r)})),t))for(;c>s;s++)t(n[s],r,o?u:u.call(n[s],s,t(n[s],r)));return f?n:h?t.call(n):c?t(n[0],r):e};g=function(n){return 1===n.nodeType||9===n.nodeType||!+n.nodeType};et.uid=1;et.prototype={register:function(n,t){var i=t||{};return n.nodeType?n[this.expando]=i:Object.defineProperty(n,this.expando,{value:i,writable:!0,configurable:!0}),n[this.expando]},cache:function(n){if(!g(n))return{};var t=n[this.expando];return t||(t={},g(n)&&(n.nodeType?n[this.expando]=t:Object.defineProperty(n,this.expando,{value:t,configurable:!0}))),t},set:function(n,t,i){var r,u=this.cache(n);if("string"==typeof t)u[t]=i;else for(r in t)u[r]=t[r];return u},get:function(n,t){return void 0===t?this.cache(n):n[this.expando]&&n[this.expando][t]},access:function(n,t,r){var u;return void 0===t||t&&"string"==typeof t&&void 0===r?(u=this.get(n,t),void 0!==u?u:this.get(n,i.camelCase(t))):(this.set(n,t,r),void 0!==r?r:t)},remove:function(n,t){var f,r,e,u=n[this.expando];if(void 0!==u){if(void 0===t)this.register(n);else for(i.isArray(t)?r=t.concat(t.map(i.camelCase)):(e=i.camelCase(t),(t in u)?r=[t,e]:(r=e,r=(r in u)?[r]:r.match(h)||[])),f=r.length;f--;)delete u[r[f]];(void 0===t||i.isEmptyObject(u))&&(n.nodeType?n[this.expando]=void 0:delete n[this.expando])}},hasData:function(n){var t=n[this.expando];return void 0!==t&&!i.isEmptyObject(t)}};var r=new et,e=new et,df=/^(?:\{[\w\W]*\}|\[[\w\W]*\])$/,cr=/[A-Z]/g;i.extend({hasData:function(n){return e.hasData(n)||r.hasData(n)},data:function(n,t,i){return e.access(n,t,i)},removeData:function(n,t){e.remove(n,t)},_data:function(n,t,i){return r.access(n,t,i)},_removeData:function(n,t){r.remove(n,t)}});i.fn.extend({data:function(n,t){var o,f,s,u=this[0],h=u&&u.attributes;if(void 0===n){if(this.length&&(s=e.get(u),1===u.nodeType&&!r.get(u,"hasDataAttrs"))){for(o=h.length;o--;)h[o]&&(f=h[o].name,0===f.indexOf("data-")&&(f=i.camelCase(f.slice(5)),lr(u,f,s[f])));r.set(u,"hasDataAttrs",!0)}return s}return"object"==typeof n?this.each(function(){e.set(this,n)}):a(this,function(t){var r,f;if(u&&void 0===t){if((r=e.get(u,n)||e.get(u,n.replace(cr,"-$&").toLowerCase()),void 0!==r)||(f=i.camelCase(n),r=e.get(u,f),void 0!==r)||(r=lr(u,f,void 0),void 0!==r))return r}else f=i.camelCase(n),this.each(function(){var i=e.get(this,f);e.set(this,f,t);n.indexOf("-")>-1&&void 0!==i&&e.set(this,n,t)})},null,t,arguments.length>1,null,!0)},removeData:function(n){return this.each(function(){e.remove(this,n)})}});i.extend({queue:function(n,t,u){var f;if(n)return(t=(t||"fx")+"queue",f=r.get(n,t),u&&(!f||i.isArray(u)?f=r.access(n,t,i.makeArray(u)):f.push(u)),f||[])},dequeue:function(n,t){t=t||"fx";var r=i.queue(n,t),e=r.length,u=r.shift(),f=i._queueHooks(n,t),o=function(){i.dequeue(n,t)};"inprogress"===u&&(u=r.shift(),e--);u&&("fx"===t&&r.unshift("inprogress"),delete f.stop,u.call(n,o,f));!e&&f&&f.empty.fire()},_queueHooks:function(n,t){var u=t+"queueHooks";return r.get(n,u)||r.access(n,u,{empty:i.Callbacks("once memory").add(function(){r.remove(n,[t+"queue",u])})})}});i.fn.extend({queue:function(n,t){var r=2;return"string"!=typeof n&&(t=n,n="fx",r--),arguments.length<r?i.queue(this[0],n):void 0===t?this:this.each(function(){var r=i.queue(this,n,t);i._queueHooks(this,n);"fx"===n&&"inprogress"!==r[0]&&i.dequeue(this,n)})},dequeue:function(n){return this.each(function(){i.dequeue(this,n)})},clearQueue:function(n){return this.queue(n||"fx",[])},promise:function(n,t){var u,e=1,o=i.Deferred(),f=this,s=this.length,h=function(){--e||o.resolveWith(f,[f])};for("string"!=typeof n&&(t=n,n=void 0),n=n||"fx";s--;)u=r.get(f[s],n+"queueHooks"),u&&u.empty&&(e++,u.empty.add(h));return h(),o.promise(t)}});var ar=/[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source,ot=new RegExp("^(?:([+-])=|)("+ar+")([a-z%]*)$","i"),w=["Top","Right","Bottom","Left"],st=function(n,t){return n=t||n,"none"===i.css(n,"display")||!i.contains(n.ownerDocument,n)};var yr=/^(?:checkbox|radio)$/i,pr=/<([\w:-]+)/,wr=/^$|\/(?:java|ecma)script/i,c={option:[1,"<select multiple='multiple'>","<\/select>"],thead:[1,"<table>","<\/table>"],col:[2,"<table><colgroup>","<\/colgroup><\/table>"],tr:[2,"<table><tbody>","<\/tbody><\/table>"],td:[3,"<table><tbody><tr>","<\/tr><\/tbody><\/table>"],_default:[0,"",""]};c.optgroup=c.option;c.tbody=c.tfoot=c.colgroup=c.caption=c.thead;c.th=c.td;br=/<|&#?\w+;/;!function(){var i=u.createDocumentFragment(),n=i.appendChild(u.createElement("div")),t=u.createElement("input");t.setAttribute("type","radio");t.setAttribute("checked","checked");t.setAttribute("name","t");n.appendChild(t);f.checkClone=n.cloneNode(!0).cloneNode(!0).lastChild.checked;n.innerHTML="<textarea>x<\/textarea>";f.noCloneChecked=!!n.cloneNode(!0).lastChild.defaultValue}();var gf=/^key/,ne=/^(?:mouse|pointer|contextmenu|drag|drop)|click/,dr=/^([^.]*)(?:\.(.+)|)/;i.event={global:{},add:function(n,t,u,f,e){var v,y,w,p,b,c,s,l,o,k,d,a=r.get(n);if(a)for(u.handler&&(v=u,u=v.handler,e=v.selector),u.guid||(u.guid=i.guid++),(p=a.events)||(p=a.events={}),(y=a.handle)||(y=a.handle=function(t){if("undefined"!=typeof i&&i.event.triggered!==t.type)return i.event.dispatch.apply(n,arguments)}),t=(t||"").match(h)||[""],b=t.length;b--;)w=dr.exec(t[b])||[],o=d=w[1],k=(w[2]||"").split(".").sort(),o&&(s=i.event.special[o]||{},o=(e?s.delegateType:s.bindType)||o,s=i.event.special[o]||{},c=i.extend({type:o,origType:d,data:f,handler:u,guid:u.guid,selector:e,needsContext:e&&i.expr.match.needsContext.test(e),namespace:k.join(".")},v),(l=p[o])||(l=p[o]=[],l.delegateCount=0,s.setup&&s.setup.call(n,f,k,y)!==!1||n.addEventListener&&n.addEventListener(o,y)),s.add&&(s.add.call(n,c),c.handler.guid||(c.handler.guid=u.guid)),e?l.splice(l.delegateCount++,0,c):l.push(c),i.event.global[o]=!0)},remove:function(n,t,u,f,e){var y,k,c,v,p,s,l,a,o,b,d,w=r.hasData(n)&&r.get(n);if(w&&(v=w.events)){for(t=(t||"").match(h)||[""],p=t.length;p--;)if(c=dr.exec(t[p])||[],o=d=c[1],b=(c[2]||"").split(".").sort(),o){for(l=i.event.special[o]||{},o=(f?l.delegateType:l.bindType)||o,a=v[o]||[],c=c[2]&&new RegExp("(^|\\.)"+b.join("\\.(?:.*\\.|)")+"(\\.|$)"),k=y=a.length;y--;)s=a[y],!e&&d!==s.origType||u&&u.guid!==s.guid||c&&!c.test(s.namespace)||f&&f!==s.selector&&("**"!==f||!s.selector)||(a.splice(y,1),s.selector&&a.delegateCount--,l.remove&&l.remove.call(n,s));k&&!a.length&&(l.teardown&&l.teardown.call(n,b,w.handle)!==!1||i.removeEvent(n,o,w.handle),delete v[o])}else for(o in v)i.event.remove(n,o+t[p],u,f,!0);i.isEmptyObject(v)&&r.remove(n,"handle events")}},dispatch:function(n){n=i.event.fix(n);var o,s,e,u,t,h=[],c=v.call(arguments),l=(r.get(this,"events")||{})[n.type]||[],f=i.event.special[n.type]||{};if(c[0]=n,n.delegateTarget=this,!f.preDispatch||f.preDispatch.call(this,n)!==!1){for(h=i.event.handlers.call(this,n,l),o=0;(u=h[o++])&&!n.isPropagationStopped();)for(n.currentTarget=u.elem,s=0;(t=u.handlers[s++])&&!n.isImmediatePropagationStopped();)n.rnamespace&&!n.rnamespace.test(t.namespace)||(n.handleObj=t,n.data=t.data,e=((i.event.special[t.origType]||{}).handle||t.handler).apply(u.elem,c),void 0!==e&&(n.result=e)===!1&&(n.preventDefault(),n.stopPropagation()));return f.postDispatch&&f.postDispatch.call(this,n),n.result}},handlers:function(n,t){var e,u,f,o,h=[],s=t.delegateCount,r=n.target;if(s&&r.nodeType&&("click"!==n.type||isNaN(n.button)||n.button<1))for(;r!==this;r=r.parentNode||this)if(1===r.nodeType&&(r.disabled!==!0||"click"!==n.type)){for(u=[],e=0;s>e;e++)o=t[e],f=o.selector+" ",void 0===u[f]&&(u[f]=o.needsContext?i(f,this).index(r)>-1:i.find(f,this,null,[r]).length),u[f]&&u.push(o);u.length&&h.push({elem:r,handlers:u})}return s<t.length&&h.push({elem:this,handlers:t.slice(s)}),h},props:"altKey bubbles cancelable ctrlKey currentTarget detail eventPhase metaKey relatedTarget shiftKey target timeStamp view which".split(" "),fixHooks:{},keyHooks:{props:"char charCode key keyCode".split(" "),filter:function(n,t){return null==n.which&&(n.which=null!=t.charCode?t.charCode:t.keyCode),n}},mouseHooks:{props:"button buttons clientX clientY offsetX offsetY pageX pageY screenX screenY toElement".split(" "),filter:function(n,t){var e,i,r,f=t.button;return null==n.pageX&&null!=t.clientX&&(e=n.target.ownerDocument||u,i=e.documentElement,r=e.body,n.pageX=t.clientX+(i&&i.scrollLeft||r&&r.scrollLeft||0)-(i&&i.clientLeft||r&&r.clientLeft||0),n.pageY=t.clientY+(i&&i.scrollTop||r&&r.scrollTop||0)-(i&&i.clientTop||r&&r.clientTop||0)),n.which||void 0===f||(n.which=1&f?1:2&f?3:4&f?2:0),n}},fix:function(n){if(n[i.expando])return n;var f,e,o,r=n.type,s=n,t=this.fixHooks[r];for(t||(this.fixHooks[r]=t=ne.test(r)?this.mouseHooks:gf.test(r)?this.keyHooks:{}),o=t.props?this.props.concat(t.props):this.props,n=new i.Event(s),f=o.length;f--;)e=o[f],n[e]=s[e];return n.target||(n.target=u),3===n.target.nodeType&&(n.target=n.target.parentNode),t.filter?t.filter(n,s):n},special:{load:{noBubble:!0},focus:{trigger:function(){if(this!==gr()&&this.focus)return(this.focus(),!1)},delegateType:"focusin"},blur:{trigger:function(){if(this===gr()&&this.blur)return(this.blur(),!1)},delegateType:"focusout"},click:{trigger:function(){if("checkbox"===this.type&&this.click&&i.nodeName(this,"input"))return(this.click(),!1)},_default:function(n){return i.nodeName(n.target,"a")}},beforeunload:{postDispatch:function(n){void 0!==n.result&&n.originalEvent&&(n.originalEvent.returnValue=n.result)}}}};i.removeEvent=function(n,t,i){n.removeEventListener&&n.removeEventListener(t,i)};i.Event=function(n,t){return this instanceof i.Event?(n&&n.type?(this.originalEvent=n,this.type=n.type,this.isDefaultPrevented=n.defaultPrevented||void 0===n.defaultPrevented&&n.returnValue===!1?pt:nt):this.type=n,t&&i.extend(this,t),this.timeStamp=n&&n.timeStamp||i.now(),void(this[i.expando]=!0)):new i.Event(n,t)};i.Event.prototype={constructor:i.Event,isDefaultPrevented:nt,isPropagationStopped:nt,isImmediatePropagationStopped:nt,preventDefault:function(){var n=this.originalEvent;this.isDefaultPrevented=pt;n&&n.preventDefault()},stopPropagation:function(){var n=this.originalEvent;this.isPropagationStopped=pt;n&&n.stopPropagation()},stopImmediatePropagation:function(){var n=this.originalEvent;this.isImmediatePropagationStopped=pt;n&&n.stopImmediatePropagation();this.stopPropagation()}};i.each({mouseenter:"mouseover",mouseleave:"mouseout",pointerenter:"pointerover",pointerleave:"pointerout"},function(n,t){i.event.special[n]={delegateType:t,bindType:t,handle:function(n){var u,f=this,r=n.relatedTarget,e=n.handleObj;return r&&(r===f||i.contains(f,r))||(n.type=e.origType,u=e.handler.apply(this,arguments),n.type=t),u}}});i.fn.extend({on:function(n,t,i,r){return fi(this,n,t,i,r)},one:function(n,t,i,r){return fi(this,n,t,i,r,1)},off:function(n,t,r){var u,f;if(n&&n.preventDefault&&n.handleObj)return u=n.handleObj,i(n.delegateTarget).off(u.namespace?u.origType+"."+u.namespace:u.origType,u.selector,u.handler),this;if("object"==typeof n){for(f in n)this.off(f,t,n[f]);return this}return t!==!1&&"function"!=typeof t||(r=t,t=void 0),r===!1&&(r=nt),this.each(function(){i.event.remove(this,n,r,t)})}});var te=/<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:-]+)[^>]*)\/>/gi,ie=/<script|<style|<link/i,re=/checked\s*(?:[^=]|=\s*.checked.)/i,ue=/^true\/(.*)/,fe=/^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g;i.extend({htmlPrefilter:function(n){return n.replace(te,"<$1><\/$2>")},clone:function(n,t,r){var u,c,s,e,h=n.cloneNode(!0),l=i.contains(n.ownerDocument,n);if(!(f.noCloneChecked||1!==n.nodeType&&11!==n.nodeType||i.isXMLDoc(n)))for(e=o(h),s=o(n),u=0,c=s.length;c>u;u++)se(s[u],e[u]);if(t)if(r)for(s=s||o(n),e=e||o(h),u=0,c=s.length;c>u;u++)tu(s[u],e[u]);else tu(n,h);return e=o(h,"script"),e.length>0&&ui(e,!l&&o(n,"script")),h},cleanData:function(n){for(var u,t,f,s=i.event.special,o=0;void 0!==(t=n[o]);o++)if(g(t)){if(u=t[r.expando]){if(u.events)for(f in u.events)s[f]?i.event.remove(t,f):i.removeEvent(t,f,u.handle);t[r.expando]=void 0}t[e.expando]&&(t[e.expando]=void 0)}}});i.fn.extend({domManip:b,detach:function(n){return iu(this,n,!0)},remove:function(n){return iu(this,n)},text:function(n){return a(this,function(n){return void 0===n?i.text(this):this.empty().each(function(){1!==this.nodeType&&11!==this.nodeType&&9!==this.nodeType||(this.textContent=n)})},null,n,arguments.length)},append:function(){return b(this,arguments,function(n){if(1===this.nodeType||11===this.nodeType||9===this.nodeType){var t=nu(this,n);t.appendChild(n)}})},prepend:function(){return b(this,arguments,function(n){if(1===this.nodeType||11===this.nodeType||9===this.nodeType){var t=nu(this,n);t.insertBefore(n,t.firstChild)}})},before:function(){return b(this,arguments,function(n){this.parentNode&&this.parentNode.insertBefore(n,this)})},after:function(){return b(this,arguments,function(n){this.parentNode&&this.parentNode.insertBefore(n,this.nextSibling)})},empty:function(){for(var n,t=0;null!=(n=this[t]);t++)1===n.nodeType&&(i.cleanData(o(n,!1)),n.textContent="");return this},clone:function(n,t){return n=null==n?!1:n,t=null==t?n:t,this.map(function(){return i.clone(this,n,t)})},html:function(n){return a(this,function(n){var t=this[0]||{},r=0,u=this.length;if(void 0===n&&1===t.nodeType)return t.innerHTML;if("string"==typeof n&&!ie.test(n)&&!c[(pr.exec(n)||["",""])[1].toLowerCase()]){n=i.htmlPrefilter(n);try{for(;u>r;r++)t=this[r]||{},1===t.nodeType&&(i.cleanData(o(t,!1)),t.innerHTML=n);t=0}catch(f){}}t&&this.empty().append(n)},null,n,arguments.length)},replaceWith:function(){var n=[];return b(this,arguments,function(t){var r=this.parentNode;i.inArray(this,n)<0&&(i.cleanData(o(this)),r&&r.replaceChild(t,this))},n)}});i.each({appendTo:"append",prependTo:"prepend",insertBefore:"before",insertAfter:"after",replaceAll:"replaceWith"},function(n,t){i.fn[n]=function(n){for(var u,f=[],e=i(n),o=e.length-1,r=0;o>=r;r++)u=r===o?this:this.clone(!0),i(e[r])[t](u),ti.apply(f,u.get());return this.pushStack(f)}});ei={HTML:"block",BODY:"block"};var uu=/^margin/,si=new RegExp("^("+ar+")(?!px)[a-z%]+$","i"),bt=function(t){var i=t.ownerDocument.defaultView;return i&&i.opener||(i=n),i.getComputedStyle(t)},hi=function(n,t,i,r){var f,u,e={};for(u in t)e[u]=n.style[u],n.style[u]=t[u];f=i.apply(n,r||[]);for(u in t)n.style[u]=e[u];return f},ht=u.documentElement;!function(){var s,e,h,c,r=u.createElement("div"),t=u.createElement("div");if(t.style){t.style.backgroundClip="content-box";t.cloneNode(!0).style.backgroundClip="";f.clearCloneStyle="content-box"===t.style.backgroundClip;r.style.cssText="border:0;width:8px;height:0;top:0;left:-9999px;padding:0;margin-top:1px;position:absolute";r.appendChild(t);function o(){t.style.cssText="-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;position:relative;display:block;margin:auto;border:1px;padding:1px;top:1%;width:50%";t.innerHTML="";ht.appendChild(r);var i=n.getComputedStyle(t);s="1%"!==i.top;c="2px"===i.marginLeft;e="4px"===i.width;t.style.marginRight="50%";h="4px"===i.marginRight;ht.removeChild(r)}i.extend(f,{pixelPosition:function(){return o(),s},boxSizingReliable:function(){return null==e&&o(),e},pixelMarginRight:function(){return null==e&&o(),h},reliableMarginLeft:function(){return null==e&&o(),c},reliableMarginRight:function(){var f,i=t.appendChild(u.createElement("div"));return i.style.cssText=t.style.cssText="-webkit-box-sizing:content-box;box-sizing:content-box;display:block;margin:0;border:0;padding:0",i.style.marginRight=i.style.width="0",t.style.width="1px",ht.appendChild(r),f=!parseFloat(n.getComputedStyle(i).marginRight),ht.removeChild(r),t.removeChild(i),f}})}}();var he=/^(none|table(?!-c[ea]).+)/,ce={position:"absolute",visibility:"hidden",display:"block"},fu={letterSpacing:"0",fontWeight:"400"},eu=["Webkit","O","Moz","ms"],ou=u.createElement("div").style;i.extend({cssHooks:{opacity:{get:function(n,t){if(t){var i=tt(n,"opacity");return""===i?"1":i}}}},cssNumber:{animationIterationCount:!0,columnCount:!0,fillOpacity:!0,flexGrow:!0,flexShrink:!0,fontWeight:!0,lineHeight:!0,opacity:!0,order:!0,orphans:!0,widows:!0,zIndex:!0,zoom:!0},cssProps:{float:"cssFloat"},style:function(n,t,r,u){if(n&&3!==n.nodeType&&8!==n.nodeType&&n.style){var e,h,o,s=i.camelCase(t),c=n.style;return t=i.cssProps[s]||(i.cssProps[s]=su(s)||s),o=i.cssHooks[t]||i.cssHooks[s],void 0===r?o&&"get"in o&&void 0!==(e=o.get(n,!1,u))?e:c[t]:(h=typeof r,"string"===h&&(e=ot.exec(r))&&e[1]&&(r=vr(n,t,e),h="number"),null!=r&&r===r&&("number"===h&&(r+=e&&e[3]||(i.cssNumber[s]?"":"px")),f.clearCloneStyle||""!==r||0!==t.indexOf("background")||(c[t]="inherit"),o&&"set"in o&&void 0===(r=o.set(n,r,u))||(c[t]=r)),void 0)}},css:function(n,t,r,u){var f,s,o,e=i.camelCase(t);return t=i.cssProps[e]||(i.cssProps[e]=su(e)||e),o=i.cssHooks[t]||i.cssHooks[e],o&&"get"in o&&(f=o.get(n,!0,r)),void 0===f&&(f=tt(n,t,u)),"normal"===f&&t in fu&&(f=fu[t]),""===r||r?(s=parseFloat(f),r===!0||isFinite(s)?s||0:f):f}});i.each(["height","width"],function(n,t){i.cssHooks[t]={get:function(n,r,u){if(r)return he.test(i.css(n,"display"))&&0===n.offsetWidth?hi(n,ce,function(){return lu(n,t,u)}):lu(n,t,u)},set:function(n,r,u){var f,e=u&&bt(n),o=u&&cu(n,t,u,"border-box"===i.css(n,"boxSizing",!1,e),e);return o&&(f=ot.exec(r))&&"px"!==(f[3]||"px")&&(n.style[t]=r,r=i.css(n,t)),hu(n,r,o)}}});i.cssHooks.marginLeft=ci(f.reliableMarginLeft,function(n,t){if(t)return(parseFloat(tt(n,"marginLeft"))||n.getBoundingClientRect().left-hi(n,{marginLeft:0},function(){return n.getBoundingClientRect().left}))+"px"});i.cssHooks.marginRight=ci(f.reliableMarginRight,function(n,t){if(t)return hi(n,{display:"inline-block"},tt,[n,"marginRight"])});i.each({margin:"",padding:"",border:"Width"},function(n,t){i.cssHooks[n+t]={expand:function(i){for(var r=0,f={},u="string"==typeof i?i.split(" "):[i];4>r;r++)f[n+w[r]+t]=u[r]||u[r-2]||u[0];return f}};uu.test(n)||(i.cssHooks[n+t].set=hu)});i.fn.extend({css:function(n,t){return a(this,function(n,t,r){var f,e,o={},u=0;if(i.isArray(t)){for(f=bt(n),e=t.length;e>u;u++)o[t[u]]=i.css(n,t[u],!1,f);return o}return void 0!==r?i.style(n,t,r):i.css(n,t)},n,t,arguments.length>1)},show:function(){return au(this,!0)},hide:function(){return au(this)},toggle:function(n){return"boolean"==typeof n?n?this.show():this.hide():this.each(function(){st(this)?i(this).show():i(this).hide()})}});i.Tween=s;s.prototype={constructor:s,init:function(n,t,r,u,f,e){this.elem=n;this.prop=r;this.easing=f||i.easing._default;this.options=t;this.start=this.now=this.cur();this.end=u;this.unit=e||(i.cssNumber[r]?"":"px")},cur:function(){var n=s.propHooks[this.prop];return n&&n.get?n.get(this):s.propHooks._default.get(this)},run:function(n){var t,r=s.propHooks[this.prop];return this.pos=this.options.duration?t=i.easing[this.easing](n,this.options.duration*n,0,1,this.options.duration):t=n,this.now=(this.end-this.start)*t+this.start,this.options.step&&this.options.step.call(this.elem,this.now,this),r&&r.set?r.set(this):s.propHooks._default.set(this),this}};s.prototype.init.prototype=s.prototype;s.propHooks={_default:{get:function(n){var t;return 1!==n.elem.nodeType||null!=n.elem[n.prop]&&null==n.elem.style[n.prop]?n.elem[n.prop]:(t=i.css(n.elem,n.prop,""),t&&"auto"!==t?t:0)},set:function(n){i.fx.step[n.prop]?i.fx.step[n.prop](n):1!==n.elem.nodeType||null==n.elem.style[i.cssProps[n.prop]]&&!i.cssHooks[n.prop]?n.elem[n.prop]=n.now:i.style(n.elem,n.prop,n.now+n.unit)}}};s.propHooks.scrollTop=s.propHooks.scrollLeft={set:function(n){n.elem.nodeType&&n.elem.parentNode&&(n.elem[n.prop]=n.now)}};i.easing={linear:function(n){return n},swing:function(n){return.5-Math.cos(n*Math.PI)/2},_default:"swing"};i.fx=s.prototype.init;i.fx.step={};vu=/^(?:toggle|show|hide)$/;yu=/queueHooks$/;i.Animation=i.extend(l,{tweeners:{"*":[function(n,t){var i=this.createTween(n,t);return vr(i.elem,n,ot.exec(t),i),i}]},tweener:function(n,t){i.isFunction(n)?(t=n,n=["*"]):n=n.match(h);for(var r,u=0,f=n.length;f>u;u++)r=n[u],l.tweeners[r]=l.tweeners[r]||[],l.tweeners[r].unshift(t)},prefilters:[le],prefilter:function(n,t){t?l.prefilters.unshift(n):l.prefilters.push(n)}});i.speed=function(n,t,r){var u=n&&"object"==typeof n?i.extend({},n):{complete:r||!r&&t||i.isFunction(n)&&n,duration:n,easing:r&&t||t&&!i.isFunction(t)&&t};return u.duration=i.fx.off?0:"number"==typeof u.duration?u.duration:u.duration in i.fx.speeds?i.fx.speeds[u.duration]:i.fx.speeds._default,null!=u.queue&&u.queue!==!0||(u.queue="fx"),u.old=u.complete,u.complete=function(){i.isFunction(u.old)&&u.old.call(this);u.queue&&i.dequeue(this,u.queue)},u};i.fn.extend({fadeTo:function(n,t,i,r){return this.filter(st).css("opacity",0).show().end().animate({opacity:t},n,i,r)},animate:function(n,t,u,f){var s=i.isEmptyObject(n),o=i.speed(t,u,f),e=function(){var t=l(this,i.extend({},n),o);(s||r.get(this,"finish"))&&t.stop(!0)};return e.finish=e,s||o.queue===!1?this.each(e):this.queue(o.queue,e)},stop:function(n,t,u){var f=function(n){var t=n.stop;delete n.stop;t(u)};return"string"!=typeof n&&(u=t,t=n,n=void 0),t&&n!==!1&&this.queue(n||"fx",[]),this.each(function(){var s=!0,t=null!=n&&n+"queueHooks",o=i.timers,e=r.get(this);if(t)e[t]&&e[t].stop&&f(e[t]);else for(t in e)e[t]&&e[t].stop&&yu.test(t)&&f(e[t]);for(t=o.length;t--;)o[t].elem!==this||null!=n&&o[t].queue!==n||(o[t].anim.stop(u),s=!1,o.splice(t,1));!s&&u||i.dequeue(this,n)})},finish:function(n){return n!==!1&&(n=n||"fx"),this.each(function(){var t,e=r.get(this),u=e[n+"queue"],o=e[n+"queueHooks"],f=i.timers,s=u?u.length:0;for(e.finish=!0,i.queue(this,n,[]),o&&o.stop&&o.stop.call(this,!0),t=f.length;t--;)f[t].elem===this&&f[t].queue===n&&(f[t].anim.stop(!0),f.splice(t,1));for(t=0;s>t;t++)u[t]&&u[t].finish&&u[t].finish.call(this);delete e.finish})}});i.each(["toggle","show","hide"],function(n,t){var r=i.fn[t];i.fn[t]=function(n,i,u){return null==n||"boolean"==typeof n?r.apply(this,arguments):this.animate(dt(t,!0),n,i,u)}});i.each({slideDown:dt("show"),slideUp:dt("hide"),slideToggle:dt("toggle"),fadeIn:{opacity:"show"},fadeOut:{opacity:"hide"},fadeToggle:{opacity:"toggle"}},function(n,t){i.fn[n]=function(n,i,r){return this.animate(t,n,i,r)}});i.timers=[];i.fx.tick=function(){var r,n=0,t=i.timers;for(it=i.now();n<t.length;n++)r=t[n],r()||t[n]!==r||t.splice(n--,1);t.length||i.fx.stop();it=void 0};i.fx.timer=function(n){i.timers.push(n);n()?i.fx.start():i.timers.pop()};i.fx.interval=13;i.fx.start=function(){kt||(kt=n.setInterval(i.fx.tick,i.fx.interval))};i.fx.stop=function(){n.clearInterval(kt);kt=null};i.fx.speeds={slow:600,fast:200,_default:400};i.fn.delay=function(t,r){return t=i.fx?i.fx.speeds[t]||t:t,r=r||"fx",this.queue(r,function(i,r){var u=n.setTimeout(i,t);r.stop=function(){n.clearTimeout(u)}})},function(){var n=u.createElement("input"),t=u.createElement("select"),i=t.appendChild(u.createElement("option"));n.type="checkbox";f.checkOn=""!==n.value;f.optSelected=i.selected;t.disabled=!0;f.optDisabled=!i.disabled;n=u.createElement("input");n.value="t";n.type="radio";f.radioValue="t"===n.value}();rt=i.expr.attrHandle;i.fn.extend({attr:function(n,t){return a(this,i.attr,n,t,arguments.length>1)},removeAttr:function(n){return this.each(function(){i.removeAttr(this,n)})}});i.extend({attr:function(n,t,r){var u,f,e=n.nodeType;if(3!==e&&8!==e&&2!==e)return"undefined"==typeof n.getAttribute?i.prop(n,t,r):(1===e&&i.isXMLDoc(n)||(t=t.toLowerCase(),f=i.attrHooks[t]||(i.expr.match.bool.test(t)?bu:void 0)),void 0!==r?null===r?void i.removeAttr(n,t):f&&"set"in f&&void 0!==(u=f.set(n,r,t))?u:(n.setAttribute(t,r+""),r):f&&"get"in f&&null!==(u=f.get(n,t))?u:(u=i.find.attr(n,t),null==u?void 0:u))},attrHooks:{type:{set:function(n,t){if(!f.radioValue&&"radio"===t&&i.nodeName(n,"input")){var r=n.value;return n.setAttribute("type",t),r&&(n.value=r),t}}}},removeAttr:function(n,t){var r,u,e=0,f=t&&t.match(h);if(f&&1===n.nodeType)while(r=f[e++])u=i.propFix[r]||r,i.expr.match.bool.test(r)&&(n[u]=!1),n.removeAttribute(r)}});bu={set:function(n,t,r){return t===!1?i.removeAttr(n,r):n.setAttribute(r,r),r}};i.each(i.expr.match.bool.source.match(/\w+/g),function(n,t){var r=rt[t]||i.find.attr;rt[t]=function(n,t,i){var u,f;return i||(f=rt[t],rt[t]=u,u=null!=r(n,t,i)?t.toLowerCase():null,rt[t]=f),u}});ku=/^(?:input|select|textarea|button)$/i;du=/^(?:a|area)$/i;i.fn.extend({prop:function(n,t){return a(this,i.prop,n,t,arguments.length>1)},removeProp:function(n){return this.each(function(){delete this[i.propFix[n]||n]})}});i.extend({prop:function(n,t,r){var f,u,e=n.nodeType;if(3!==e&&8!==e&&2!==e)return 1===e&&i.isXMLDoc(n)||(t=i.propFix[t]||t,u=i.propHooks[t]),void 0!==r?u&&"set"in u&&void 0!==(f=u.set(n,r,t))?f:n[t]=r:u&&"get"in u&&null!==(f=u.get(n,t))?f:n[t]},propHooks:{tabIndex:{get:function(n){var t=i.find.attr(n,"tabindex");return t?parseInt(t,10):ku.test(n.nodeName)||du.test(n.nodeName)&&n.href?0:-1}}},propFix:{"for":"htmlFor","class":"className"}});f.optSelected||(i.propHooks.selected={get:function(n){var t=n.parentNode;return t&&t.parentNode&&t.parentNode.selectedIndex,null},set:function(n){var t=n.parentNode;t&&(t.selectedIndex,t.parentNode&&t.parentNode.selectedIndex)}});i.each(["tabIndex","readOnly","maxLength","cellSpacing","cellPadding","rowSpan","colSpan","useMap","frameBorder","contentEditable"],function(){i.propFix[this.toLowerCase()]=this});gt=/[\t\r\n\f]/g;i.fn.extend({addClass:function(n){var o,t,r,u,f,s,e,c=0;if(i.isFunction(n))return this.each(function(t){i(this).addClass(n.call(this,t,k(this)))});if("string"==typeof n&&n)for(o=n.match(h)||[];t=this[c++];)if(u=k(t),r=1===t.nodeType&&(" "+u+" ").replace(gt," ")){for(s=0;f=o[s++];)r.indexOf(" "+f+" ")<0&&(r+=f+" ");e=i.trim(r);u!==e&&t.setAttribute("class",e)}return this},removeClass:function(n){var o,r,t,u,f,s,e,c=0;if(i.isFunction(n))return this.each(function(t){i(this).removeClass(n.call(this,t,k(this)))});if(!arguments.length)return this.attr("class","");if("string"==typeof n&&n)for(o=n.match(h)||[];r=this[c++];)if(u=k(r),t=1===r.nodeType&&(" "+u+" ").replace(gt," ")){for(s=0;f=o[s++];)while(t.indexOf(" "+f+" ")>-1)t=t.replace(" "+f+" "," ");e=i.trim(t);u!==e&&r.setAttribute("class",e)}return this},toggleClass:function(n,t){var u=typeof n;return"boolean"==typeof t&&"string"===u?t?this.addClass(n):this.removeClass(n):i.isFunction(n)?this.each(function(r){i(this).toggleClass(n.call(this,r,k(this),t),t)}):this.each(function(){var t,e,f,o;if("string"===u)for(e=0,f=i(this),o=n.match(h)||[];t=o[e++];)f.hasClass(t)?f.removeClass(t):f.addClass(t);else void 0!==n&&"boolean"!==u||(t=k(this),t&&r.set(this,"__className__",t),this.setAttribute&&this.setAttribute("class",t||n===!1?"":r.get(this,"__className__")||""))})},hasClass:function(n){for(var t,r=0,i=" "+n+" ";t=this[r++];)if(1===t.nodeType&&(" "+k(t)+" ").replace(gt," ").indexOf(i)>-1)return!0;return!1}});gu=/\r/g;nf=/[\x20\t\r\n\f]+/g;i.fn.extend({val:function(n){var t,r,f,u=this[0];return arguments.length?(f=i.isFunction(n),this.each(function(r){var u;1===this.nodeType&&(u=f?n.call(this,r,i(this).val()):n,null==u?u="":"number"==typeof u?u+="":i.isArray(u)&&(u=i.map(u,function(n){return null==n?"":n+""})),t=i.valHooks[this.type]||i.valHooks[this.nodeName.toLowerCase()],t&&"set"in t&&void 0!==t.set(this,u,"value")||(this.value=u))})):u?(t=i.valHooks[u.type]||i.valHooks[u.nodeName.toLowerCase()],t&&"get"in t&&void 0!==(r=t.get(u,"value"))?r:(r=u.value,"string"==typeof r?r.replace(gu,""):null==r?"":r)):void 0}});i.extend({valHooks:{option:{get:function(n){var t=i.find.attr(n,"value");return null!=t?t:i.trim(i.text(n)).replace(nf," ")}},select:{get:function(n){for(var o,t,s=n.options,r=n.selectedIndex,u="select-one"===n.type||0>r,h=u?null:[],c=u?r+1:s.length,e=0>r?c:u?r:0;c>e;e++)if(t=s[e],(t.selected||e===r)&&(f.optDisabled?!t.disabled:null===t.getAttribute("disabled"))&&(!t.parentNode.disabled||!i.nodeName(t.parentNode,"optgroup"))){if(o=i(t).val(),u)return o;h.push(o)}return h},set:function(n,t){for(var u,r,f=n.options,e=i.makeArray(t),o=f.length;o--;)r=f[o],(r.selected=i.inArray(i.valHooks.option.get(r),e)>-1)&&(u=!0);return u||(n.selectedIndex=-1),e}}}});i.each(["radio","checkbox"],function(){i.valHooks[this]={set:function(n,t){if(i.isArray(t))return n.checked=i.inArray(i(n).val(),t)>-1}};f.checkOn||(i.valHooks[this].get=function(n){return null===n.getAttribute("value")?"on":n.value})});li=/^(?:focusinfocus|focusoutblur)$/;i.extend(i.event,{trigger:function(t,f,e,o){var w,s,c,b,a,v,l,p=[e||u],h=ft.call(t,"type")?t.type:t,y=ft.call(t,"namespace")?t.namespace.split("."):[];if(s=c=e=e||u,3!==e.nodeType&&8!==e.nodeType&&!li.test(h+i.event.triggered)&&(h.indexOf(".")>-1&&(y=h.split("."),h=y.shift(),y.sort()),a=h.indexOf(":")<0&&"on"+h,t=t[i.expando]?t:new i.Event(h,"object"==typeof t&&t),t.isTrigger=o?2:3,t.namespace=y.join("."),t.rnamespace=t.namespace?new RegExp("(^|\\.)"+y.join("\\.(?:.*\\.|)")+"(\\.|$)"):null,t.result=void 0,t.target||(t.target=e),f=null==f?[t]:i.makeArray(f,[t]),l=i.event.special[h]||{},o||!l.trigger||l.trigger.apply(e,f)!==!1)){if(!o&&!l.noBubble&&!i.isWindow(e)){for(b=l.delegateType||h,li.test(b+h)||(s=s.parentNode);s;s=s.parentNode)p.push(s),c=s;c===(e.ownerDocument||u)&&p.push(c.defaultView||c.parentWindow||n)}for(w=0;(s=p[w++])&&!t.isPropagationStopped();)t.type=w>1?b:l.bindType||h,v=(r.get(s,"events")||{})[t.type]&&r.get(s,"handle"),v&&v.apply(s,f),v=a&&s[a],v&&v.apply&&g(s)&&(t.result=v.apply(s,f),t.result===!1&&t.preventDefault());return t.type=h,o||t.isDefaultPrevented()||l._default&&l._default.apply(p.pop(),f)!==!1||!g(e)||a&&i.isFunction(e[h])&&!i.isWindow(e)&&(c=e[a],c&&(e[a]=null),i.event.triggered=h,e[h](),i.event.triggered=void 0,c&&(e[a]=c)),t.result}},simulate:function(n,t,r){var u=i.extend(new i.Event,r,{type:n,isSimulated:!0});i.event.trigger(u,null,t);u.isDefaultPrevented()&&r.preventDefault()}});i.fn.extend({trigger:function(n,t){return this.each(function(){i.event.trigger(n,t,this)})},triggerHandler:function(n,t){var r=this[0];if(r)return i.event.trigger(n,t,r,!0)}});i.each("blur focus focusin focusout load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup error contextmenu".split(" "),function(n,t){i.fn[t]=function(n,i){return arguments.length>0?this.on(t,null,n,i):this.trigger(t)}});i.fn.extend({hover:function(n,t){return this.mouseenter(n).mouseleave(t||n)}});f.focusin="onfocusin"in n;f.focusin||i.each({focus:"focusin",blur:"focusout"},function(n,t){var u=function(n){i.event.simulate(t,n.target,i.event.fix(n))};i.event.special[t]={setup:function(){var i=this.ownerDocument||this,f=r.access(i,t);f||i.addEventListener(n,u,!0);r.access(i,t,(f||0)+1)},teardown:function(){var i=this.ownerDocument||this,f=r.access(i,t)-1;f?r.access(i,t,f):(i.removeEventListener(n,u,!0),r.remove(i,t))}}});var ct=n.location,ai=i.now(),vi=/\?/;i.parseJSON=function(n){return n?JSON.parse(n+""):null};i.parseXML=function(t){var r;if(!t||"string"!=typeof t)return null;try{r=(new n.DOMParser).parseFromString(t,"text/xml")}catch(u){r=void 0}return r&&!r.getElementsByTagName("parsererror").length||i.error("Invalid XML: "+t),r};var ve=/#.*$/,tf=/([?&])_=[^&]*/,ye=/^(.*?):[ \t]*([^\r\n]*)$/gm,pe=/^(?:GET|HEAD)$/,we=/^\/\//,rf={},yi={},uf="*/".concat("*"),pi=u.createElement("a");pi.href=ct.href;i.extend({active:0,lastModified:{},etag:{},ajaxSettings:{url:ct.href,type:"GET",isLocal:/^(?:about|app|app-storage|.+-extension|file|res|widget):$/.test(ct.protocol),global:!0,processData:!0,async:!0,contentType:"application/x-www-form-urlencoded; charset=UTF-8",accepts:{"*":uf,text:"text/plain",html:"text/html",xml:"application/xml, text/xml",json:"application/json, text/javascript"},contents:{xml:/\bxml\b/,html:/\bhtml/,json:/\bjson\b/},responseFields:{xml:"responseXML",text:"responseText",json:"responseJSON"},converters:{"* text":String,"text html":!0,"text json":i.parseJSON,"text xml":i.parseXML},flatOptions:{url:!0,context:!0}},ajaxSetup:function(n,t){return t?wi(wi(n,i.ajaxSettings),t):wi(i.ajaxSettings,n)},ajaxPrefilter:ff(rf),ajaxTransport:ff(yi),ajax:function(t,r){function b(t,r,u,h){var a,rt,it,p,b,l=r;2!==s&&(s=2,d&&n.clearTimeout(d),v=void 0,k=h||"",e.readyState=t>0?4:0,a=t>=200&&300>t||304===t,u&&(p=be(f,e,u)),p=ke(f,p,e,a),a?(f.ifModified&&(b=e.getResponseHeader("Last-Modified"),b&&(i.lastModified[o]=b),b=e.getResponseHeader("etag"),b&&(i.etag[o]=b)),204===t||"HEAD"===f.type?l="nocontent":304===t?l="notmodified":(l=p.state,rt=p.data,it=p.error,a=!it)):(it=l,!t&&l||(l="error",0>t&&(t=0))),e.status=t,e.statusText=(r||l)+"",a?nt.resolveWith(c,[rt,l,e]):nt.rejectWith(c,[e,l,it]),e.statusCode(w),w=void 0,y&&g.trigger(a?"ajaxSuccess":"ajaxError",[e,f,a?rt:it]),tt.fireWith(c,[e,l]),y&&(g.trigger("ajaxComplete",[e,f]),--i.active||i.event.trigger("ajaxStop")))}"object"==typeof t&&(r=t,t=void 0);r=r||{};var v,o,k,p,d,l,y,a,f=i.ajaxSetup({},r),c=f.context||f,g=f.context&&(c.nodeType||c.jquery)?i(c):i.event,nt=i.Deferred(),tt=i.Callbacks("once memory"),w=f.statusCode||{},it={},rt={},s=0,ut="canceled",e={readyState:0,getResponseHeader:function(n){var t;if(2===s){if(!p)for(p={};t=ye.exec(k);)p[t[1].toLowerCase()]=t[2];t=p[n.toLowerCase()]}return null==t?null:t},getAllResponseHeaders:function(){return 2===s?k:null},setRequestHeader:function(n,t){var i=n.toLowerCase();return s||(n=rt[i]=rt[i]||n,it[n]=t),this},overrideMimeType:function(n){return s||(f.mimeType=n),this},statusCode:function(n){var t;if(n)if(2>s)for(t in n)w[t]=[w[t],n[t]];else e.always(n[e.status]);return this},abort:function(n){var t=n||ut;return v&&v.abort(t),b(0,t),this}};if(nt.promise(e).complete=tt.add,e.success=e.done,e.error=e.fail,f.url=((t||f.url||ct.href)+"").replace(ve,"").replace(we,ct.protocol+"//"),f.type=r.method||r.type||f.method||f.type,f.dataTypes=i.trim(f.dataType||"*").toLowerCase().match(h)||[""],null==f.crossDomain){l=u.createElement("a");try{l.href=f.url;l.href=l.href;f.crossDomain=pi.protocol+"//"+pi.host!=l.protocol+"//"+l.host}catch(ft){f.crossDomain=!0}}if(f.data&&f.processData&&"string"!=typeof f.data&&(f.data=i.param(f.data,f.traditional)),ef(rf,f,r,e),2===s)return e;y=i.event&&f.global;y&&0==i.active++&&i.event.trigger("ajaxStart");f.type=f.type.toUpperCase();f.hasContent=!pe.test(f.type);o=f.url;f.hasContent||(f.data&&(o=f.url+=(vi.test(o)?"&":"?")+f.data,delete f.data),f.cache===!1&&(f.url=tf.test(o)?o.replace(tf,"$1_="+ai++):o+(vi.test(o)?"&":"?")+"_="+ai++));f.ifModified&&(i.lastModified[o]&&e.setRequestHeader("If-Modified-Since",i.lastModified[o]),i.etag[o]&&e.setRequestHeader("If-None-Match",i.etag[o]));(f.data&&f.hasContent&&f.contentType!==!1||r.contentType)&&e.setRequestHeader("Content-Type",f.contentType);e.setRequestHeader("Accept",f.dataTypes[0]&&f.accepts[f.dataTypes[0]]?f.accepts[f.dataTypes[0]]+("*"!==f.dataTypes[0]?", "+uf+"; q=0.01":""):f.accepts["*"]);for(a in f.headers)e.setRequestHeader(a,f.headers[a]);if(f.beforeSend&&(f.beforeSend.call(c,e,f)===!1||2===s))return e.abort();ut="abort";for(a in{success:1,error:1,complete:1})e[a](f[a]);if(v=ef(yi,f,r,e)){if(e.readyState=1,y&&g.trigger("ajaxSend",[e,f]),2===s)return e;f.async&&f.timeout>0&&(d=n.setTimeout(function(){e.abort("timeout")},f.timeout));try{s=1;v.send(it,b)}catch(ft){if(!(2>s))throw ft;b(-1,ft)}}else b(-1,"No Transport");return e},getJSON:function(n,t,r){return i.get(n,t,r,"json")},getScript:function(n,t){return i.get(n,void 0,t,"script")}});i.each(["get","post"],function(n,t){i[t]=function(n,r,u,f){return i.isFunction(r)&&(f=f||u,u=r,r=void 0),i.ajax(i.extend({url:n,type:t,dataType:f,data:r,success:u},i.isPlainObject(n)&&n))}});i._evalUrl=function(n){return i.ajax({url:n,type:"GET",dataType:"script",async:!1,global:!1,throws:!0})};i.fn.extend({wrapAll:function(n){var t;return i.isFunction(n)?this.each(function(t){i(this).wrapAll(n.call(this,t))}):(this[0]&&(t=i(n,this[0].ownerDocument).eq(0).clone(!0),this[0].parentNode&&t.insertBefore(this[0]),t.map(function(){for(var n=this;n.firstElementChild;)n=n.firstElementChild;return n}).append(this)),this)},wrapInner:function(n){return i.isFunction(n)?this.each(function(t){i(this).wrapInner(n.call(this,t))}):this.each(function(){var t=i(this),r=t.contents();r.length?r.wrapAll(n):t.append(n)})},wrap:function(n){var t=i.isFunction(n);return this.each(function(r){i(this).wrapAll(t?n.call(this,r):n)})},unwrap:function(){return this.parent().each(function(){i.nodeName(this,"body")||i(this).replaceWith(this.childNodes)}).end()}});i.expr.filters.hidden=function(n){return!i.expr.filters.visible(n)};i.expr.filters.visible=function(n){return n.offsetWidth>0||n.offsetHeight>0||n.getClientRects().length>0};var de=/%20/g,ge=/\[\]$/,of=/\r?\n/g,no=/^(?:submit|button|image|reset|file)$/i,to=/^(?:input|select|textarea|keygen)/i;return i.param=function(n,t){var r,u=[],f=function(n,t){t=i.isFunction(t)?t():null==t?"":t;u[u.length]=encodeURIComponent(n)+"="+encodeURIComponent(t)};if(void 0===t&&(t=i.ajaxSettings&&i.ajaxSettings.traditional),i.isArray(n)||n.jquery&&!i.isPlainObject(n))i.each(n,function(){f(this.name,this.value)});else for(r in n)bi(r,n[r],t,f);return u.join("&").replace(de,"+")},i.fn.extend({serialize:function(){return i.param(this.serializeArray())},serializeArray:function(){return this.map(function(){var n=i.prop(this,"elements");return n?i.makeArray(n):this}).filter(function(){var n=this.type;return this.name&&!i(this).is(":disabled")&&to.test(this.nodeName)&&!no.test(n)&&(this.checked||!yr.test(n))}).map(function(n,t){var r=i(this).val();return null==r?null:i.isArray(r)?i.map(r,function(n){return{name:t.name,value:n.replace(of,"\r\n")}}):{name:t.name,value:r.replace(of,"\r\n")}}).get()}}),i.ajaxSettings.xhr=function(){try{return new n.XMLHttpRequest}catch(t){}},sf={0:200,1223:204},ut=i.ajaxSettings.xhr(),f.cors=!!ut&&"withCredentials"in ut,f.ajax=ut=!!ut,i.ajaxTransport(function(t){var i,r;if(f.cors||ut&&!t.crossDomain)return{send:function(u,f){var o,e=t.xhr();if(e.open(t.type,t.url,t.async,t.username,t.password),t.xhrFields)for(o in t.xhrFields)e[o]=t.xhrFields[o];t.mimeType&&e.overrideMimeType&&e.overrideMimeType(t.mimeType);t.crossDomain||u["X-Requested-With"]||(u["X-Requested-With"]="XMLHttpRequest");for(o in u)e.setRequestHeader(o,u[o]);i=function(n){return function(){i&&(i=r=e.onload=e.onerror=e.onabort=e.onreadystatechange=null,"abort"===n?e.abort():"error"===n?"number"!=typeof e.status?f(0,"error"):f(e.status,e.statusText):f(sf[e.status]||e.status,e.statusText,"text"!==(e.responseType||"text")||"string"!=typeof e.responseText?{binary:e.response}:{text:e.responseText},e.getAllResponseHeaders()))}};e.onload=i();r=e.onerror=i("error");void 0!==e.onabort?e.onabort=r:e.onreadystatechange=function(){4===e.readyState&&n.setTimeout(function(){i&&r()})};i=i("abort");try{e.send(t.hasContent&&t.data||null)}catch(s){if(i)throw s;}},abort:function(){i&&i()}}}),i.ajaxSetup({accepts:{script:"text/javascript, application/javascript, application/ecmascript, application/x-ecmascript"},contents:{script:/\b(?:java|ecma)script\b/},converters:{"text script":function(n){return i.globalEval(n),n}}}),i.ajaxPrefilter("script",function(n){void 0===n.cache&&(n.cache=!1);n.crossDomain&&(n.type="GET")}),i.ajaxTransport("script",function(n){if(n.crossDomain){var r,t;return{send:function(f,e){r=i("<script>").prop({charset:n.scriptCharset,src:n.url}).on("load error",t=function(n){r.remove();t=null;n&&e("error"===n.type?404:200,n.type)});u.head.appendChild(r[0])},abort:function(){t&&t()}}}}),ki=[],ni=/(=)\?(?=&|$)|\?\?/,i.ajaxSetup({jsonp:"callback",jsonpCallback:function(){var n=ki.pop()||i.expando+"_"+ai++;return this[n]=!0,n}}),i.ajaxPrefilter("json jsonp",function(t,r,u){var f,e,o,s=t.jsonp!==!1&&(ni.test(t.url)?"url":"string"==typeof t.data&&0===(t.contentType||"").indexOf("application/x-www-form-urlencoded")&&ni.test(t.data)&&"data");if(s||"jsonp"===t.dataTypes[0])return(f=t.jsonpCallback=i.isFunction(t.jsonpCallback)?t.jsonpCallback():t.jsonpCallback,s?t[s]=t[s].replace(ni,"$1"+f):t.jsonp!==!1&&(t.url+=(vi.test(t.url)?"&":"?")+t.jsonp+"="+f),t.converters["script json"]=function(){return o||i.error(f+" was not called"),o[0]},t.dataTypes[0]="json",e=n[f],n[f]=function(){o=arguments},u.always(function(){void 0===e?i(n).removeProp(f):n[f]=e;t[f]&&(t.jsonpCallback=r.jsonpCallback,ki.push(f));o&&i.isFunction(e)&&e(o[0]);o=e=void 0}),"script")}),i.parseHTML=function(n,t,r){if(!n||"string"!=typeof n)return null;"boolean"==typeof t&&(r=t,t=!1);t=t||u;var f=rr.exec(n),e=!r&&[];return f?[t.createElement(f[1])]:(f=kr([n],t,e),e&&e.length&&i(e).remove(),i.merge([],f.childNodes))},di=i.fn.load,i.fn.load=function(n,t,r){if("string"!=typeof n&&di)return di.apply(this,arguments);var u,o,s,f=this,e=n.indexOf(" ");return e>-1&&(u=i.trim(n.slice(e)),n=n.slice(0,e)),i.isFunction(t)?(r=t,t=void 0):t&&"object"==typeof t&&(o="POST"),f.length>0&&i.ajax({url:n,type:o||"GET",dataType:"html",data:t}).done(function(n){s=arguments;f.html(u?i("<div>").append(i.parseHTML(n)).find(u):n)}).always(r&&function(n,t){f.each(function(){r.apply(this,s||[n.responseText,t,n])})}),this},i.each(["ajaxStart","ajaxStop","ajaxComplete","ajaxError","ajaxSuccess","ajaxSend"],function(n,t){i.fn[t]=function(n){return this.on(t,n)}}),i.expr.filters.animated=function(n){return i.grep(i.timers,function(t){return n===t.elem}).length},i.offset={setOffset:function(n,t,r){var e,o,s,h,u,c,v,l=i.css(n,"position"),a=i(n),f={};"static"===l&&(n.style.position="relative");u=a.offset();s=i.css(n,"top");c=i.css(n,"left");v=("absolute"===l||"fixed"===l)&&(s+c).indexOf("auto")>-1;v?(e=a.position(),h=e.top,o=e.left):(h=parseFloat(s)||0,o=parseFloat(c)||0);i.isFunction(t)&&(t=t.call(n,r,i.extend({},u)));null!=t.top&&(f.top=t.top-u.top+h);null!=t.left&&(f.left=t.left-u.left+o);"using"in t?t.using.call(n,f):a.css(f)}},i.fn.extend({offset:function(n){if(arguments.length)return void 0===n?this:this.each(function(t){i.offset.setOffset(this,n,t)});var t,f,r=this[0],u={top:0,left:0},e=r&&r.ownerDocument;if(e)return t=e.documentElement,i.contains(t,r)?(u=r.getBoundingClientRect(),f=hf(e),{top:u.top+f.pageYOffset-t.clientTop,left:u.left+f.pageXOffset-t.clientLeft}):u},position:function(){if(this[0]){var n,r,u=this[0],t={top:0,left:0};return"fixed"===i.css(u,"position")?r=u.getBoundingClientRect():(n=this.offsetParent(),r=this.offset(),i.nodeName(n[0],"html")||(t=n.offset()),t.top+=i.css(n[0],"borderTopWidth",!0),t.left+=i.css(n[0],"borderLeftWidth",!0)),{top:r.top-t.top-i.css(u,"marginTop",!0),left:r.left-t.left-i.css(u,"marginLeft",!0)}}},offsetParent:function(){return this.map(function(){for(var n=this.offsetParent;n&&"static"===i.css(n,"position");)n=n.offsetParent;return n||ht})}}),i.each({scrollLeft:"pageXOffset",scrollTop:"pageYOffset"},function(n,t){var r="pageYOffset"===t;i.fn[n]=function(i){return a(this,function(n,i,u){var f=hf(n);return void 0===u?f?f[t]:n[i]:void(f?f.scrollTo(r?f.pageXOffset:u,r?u:f.pageYOffset):n[i]=u)},n,i,arguments.length)}}),i.each(["top","left"],function(n,t){i.cssHooks[t]=ci(f.pixelPosition,function(n,r){if(r)return(r=tt(n,t),si.test(r)?i(n).position()[t]+"px":r)})}),i.each({Height:"height",Width:"width"},function(n,t){i.each({padding:"inner"+n,content:t,"":"outer"+n},function(r,u){i.fn[u]=function(u,f){var e=arguments.length&&(r||"boolean"!=typeof u),o=r||(u===!0||f===!0?"margin":"border");return a(this,function(t,r,u){var f;return i.isWindow(t)?t.document.documentElement["client"+n]:9===t.nodeType?(f=t.documentElement,Math.max(t.body["scroll"+n],f["scroll"+n],t.body["offset"+n],f["offset"+n],f["client"+n])):void 0===u?i.css(t,r,o):i.style(t,r,u,o)},t,e?u:void 0,e,null)}})}),i.fn.extend({bind:function(n,t,i){return this.on(n,null,t,i)},unbind:function(n,t){return this.off(n,null,t)},delegate:function(n,t,i,r){return this.on(t,n,i,r)},undelegate:function(n,t,i){return 1===arguments.length?this.off(n,"**"):this.off(t,n||"**",i)},size:function(){return this.length}}),i.fn.andSelf=i.fn.addBack,"function"==typeof define&&define.amd&&define("jquery",[],function(){return i}),cf=n.jQuery,lf=n.$,i.noConflict=function(t){return n.$===i&&(n.$=lf),t&&n.jQuery===i&&(n.jQuery=cf),i},t||(n.jQuery=n.$=i),i});
/*
* jQuery Mobile v1.4.5
* http://jquerymobile.com
*
* Copyright 2010, 2014 jQuery Foundation, Inc. and other contributors
* Released under the MIT license.
* http://jquery.org/license
*
*/

(function (root, doc, factory) {
    if (typeof define === "function" && define.amd) {
        // AMD. Register as an anonymous module.
        define(["jquery"], function ($) {
            factory($, root, doc);
            return $.mobile;
        });
    } else {
        // Browser globals
        factory(root.jQuery, root, doc);
    }
}(this, document, function (jQuery, window, document, undefined) {
    (function ($, undefined) {
        $.extend($.support, {
            orientation: "orientation" in window && "onorientationchange" in window
        });
    }(jQuery));


    // throttled resize event
    (function ($) {
        $.event.special.throttledresize = {
            setup: function () {
                $(this).bind("resize", handler);
            },
            teardown: function () {
                $(this).unbind("resize", handler);
            }
        };

        var throttle = 250,
			handler = function () {
			    curr = (new Date()).getTime();
			    diff = curr - lastCall;

			    if (diff >= throttle) {

			        lastCall = curr;
			        $(this).trigger("throttledresize");

			    } else {

			        if (heldCall) {
			            clearTimeout(heldCall);
			        }

			        // Promise a held call will still execute
			        heldCall = setTimeout(handler, throttle - diff);
			    }
			},
			lastCall = 0,
			heldCall,
			curr,
			diff;
    })(jQuery);


    (function ($, window) {
        var win = $(window),
            event_name = "orientationchange",
            get_orientation,
            last_orientation,
            initial_orientation_is_landscape,
            initial_orientation_is_default,
            portrait_map = { "0": true, "180": true },
            ww, wh, landscape_threshold;

        // It seems that some device/browser vendors use window.orientation values 0 and 180 to
        // denote the "default" orientation. For iOS devices, and most other smart-phones tested,
        // the default orientation is always "portrait", but in some Android and RIM based tablets,
        // the default orientation is "landscape". The following code attempts to use the window
        // dimensions to figure out what the current orientation is, and then makes adjustments
        // to the to the portrait_map if necessary, so that we can properly decode the
        // window.orientation value whenever get_orientation() is called.
        //
        // Note that we used to use a media query to figure out what the orientation the browser
        // thinks it is in:
        //
        //     initial_orientation_is_landscape = $.mobile.media("all and (orientation: landscape)");
        //
        // but there was an iPhone/iPod Touch bug beginning with iOS 4.2, up through iOS 5.1,
        // where the browser *ALWAYS* applied the landscape media query. This bug does not
        // happen on iPad.

        if ($.support.orientation) {

            // Check the window width and height to figure out what the current orientation
            // of the device is at this moment. Note that we've initialized the portrait map
            // values to 0 and 180, *AND* we purposely check for landscape so that if we guess
            // wrong, , we default to the assumption that portrait is the default orientation.
            // We use a threshold check below because on some platforms like iOS, the iPhone
            // form-factor can report a larger width than height if the user turns on the
            // developer console. The actual threshold value is somewhat arbitrary, we just
            // need to make sure it is large enough to exclude the developer console case.

            ww = window.innerWidth || win.width();
            wh = window.innerHeight || win.height();
            landscape_threshold = 50;

            initial_orientation_is_landscape = ww > wh && (ww - wh) > landscape_threshold;

            // Now check to see if the current window.orientation is 0 or 180.
            initial_orientation_is_default = portrait_map[window.orientation];

            // If the initial orientation is landscape, but window.orientation reports 0 or 180, *OR*
            // if the initial orientation is portrait, but window.orientation reports 90 or -90, we
            // need to flip our portrait_map values because landscape is the default orientation for
            // this device/browser.
            if ((initial_orientation_is_landscape && initial_orientation_is_default) || (!initial_orientation_is_landscape && !initial_orientation_is_default)) {
                portrait_map = { "-90": true, "90": true };
            }
        }

        $.event.special.orientationchange = $.extend({}, $.event.special.orientationchange, {
            setup: function () {
                // If the event is supported natively, return false so that jQuery
                // will bind to the event using DOM methods.
                if ($.support.orientation && !$.event.special.orientationchange.disabled) {
                    return false;
                }

                // Get the current orientation to avoid initial double-triggering.
                last_orientation = get_orientation();

                // Because the orientationchange event doesn't exist, simulate the
                // event by testing window dimensions on resize.
                win.bind("throttledresize", handler);
            },
            teardown: function () {
                // If the event is not supported natively, return false so that
                // jQuery will unbind the event using DOM methods.
                if ($.support.orientation && !$.event.special.orientationchange.disabled) {
                    return false;
                }

                // Because the orientationchange event doesn't exist, unbind the
                // resize event handler.
                win.unbind("throttledresize", handler);
            },
            add: function (handleObj) {
                // Save a reference to the bound event handler.
                var old_handler = handleObj.handler;

                handleObj.handler = function (event) {
                    // Modify event object, adding the .orientation property.
                    event.orientation = get_orientation();

                    // Call the originally-bound event handler and return its result.
                    return old_handler.apply(this, arguments);
                };
            }
        });

        // If the event is not supported natively, this handler will be bound to
        // the window resize event to simulate the orientationchange event.
        function handler() {
            // Get the current orientation.
            var orientation = get_orientation();

            if (orientation !== last_orientation) {
                // The orientation has changed, so trigger the orientationchange event.
                last_orientation = orientation;
                win.trigger(event_name);
            }
        }

        // Get the current page orientation. This method is exposed publicly, should it
        // be needed, as jQuery.event.special.orientationchange.orientation()
        $.event.special.orientationchange.orientation = get_orientation = function () {
            var isPortrait = true, elem = document.documentElement;

            // prefer window orientation to the calculation based on screensize as
            // the actual screen resize takes place before or after the orientation change event
            // has been fired depending on implementation (eg android 2.3 is before, iphone after).
            // More testing is required to determine if a more reliable method of determining the new screensize
            // is possible when orientationchange is fired. (eg, use media queries + element + opacity)
            if ($.support.orientation) {
                // if the window orientation registers as 0 or 180 degrees report
                // portrait, otherwise landscape
                isPortrait = portrait_map[window.orientation];
            } else {
                isPortrait = elem && elem.clientWidth / elem.clientHeight < 1.1;
            }

            return isPortrait ? "portrait" : "landscape";
        };

        $.fn[event_name] = function (fn) {
            return fn ? this.bind(event_name, fn) : this.trigger(event_name);
        };

        // jQuery < 1.8
        if ($.attrFn) {
            $.attrFn[event_name] = true;
        }

    }(jQuery, this));


    // This plugin is an experiment for abstracting away the touch and mouse
    // events so that developers don't have to worry about which method of input
    // the device their document is loaded on supports.
    //
    // The idea here is to allow the developer to register listeners for the
    // basic mouse events, such as mousedown, mousemove, mouseup, and click,
    // and the plugin will take care of registering the correct listeners
    // behind the scenes to invoke the listener at the fastest possible time
    // for that device, while still retaining the order of event firing in
    // the traditional mouse environment, should multiple handlers be registered
    // on the same element for different events.
    //
    // The current version exposes the following virtual events to jQuery bind methods:
    // "vmouseover vmousedown vmousemove vmouseup vclick vmouseout vmousecancel"

    (function ($, window, document, undefined) {

        var dataPropertyName = "virtualMouseBindings",
            touchTargetPropertyName = "virtualTouchID",
            virtualEventNames = "vmouseover vmousedown vmousemove vmouseup vclick vmouseout vmousecancel".split(" "),
            touchEventProps = "clientX clientY pageX pageY screenX screenY".split(" "),
            mouseHookProps = $.event.mouseHooks ? $.event.mouseHooks.props : [],
            mouseEventProps = $.event.props.concat(mouseHookProps),
            activeDocHandlers = {},
            resetTimerID = 0,
            startX = 0,
            startY = 0,
            didScroll = false,
            clickBlockList = [],
            blockMouseTriggers = false,
            blockTouchTriggers = false,
            eventCaptureSupported = "addEventListener" in document,
            $document = $(document),
            nextTouchID = 1,
            lastTouchID = 0, threshold,
            i;

        $.vmouse = {
            moveDistanceThreshold: 10,
            clickDistanceThreshold: 10,
            resetTimerDuration: 1500
        };

        function getNativeEvent(event) {

            while (event && typeof event.originalEvent !== "undefined") {
                event = event.originalEvent;
            }
            return event;
        }

        function createVirtualEvent(event, eventType) {

            var t = event.type,
                oe, props, ne, prop, ct, touch, i, j, len;

            event = $.Event(event);
            event.type = eventType;

            oe = event.originalEvent;
            props = $.event.props;

            // addresses separation of $.event.props in to $.event.mouseHook.props and Issue 3280
            // https://github.com/jquery/jquery-mobile/issues/3280
            if (t.search(/^(mouse|click)/) > -1) {
                props = mouseEventProps;
            }

            // copy original event properties over to the new event
            // this would happen if we could call $.event.fix instead of $.Event
            // but we don't have a way to force an event to be fixed multiple times
            if (oe) {
                for (i = props.length, prop; i;) {
                    prop = props[--i];
                    event[prop] = oe[prop];
                }
            }

            // make sure that if the mouse and click virtual events are generated
            // without a .which one is defined
            if (t.search(/mouse(down|up)|click/) > -1 && !event.which) {
                event.which = 1;
            }

            if (t.search(/^touch/) !== -1) {
                ne = getNativeEvent(oe);
                t = ne.touches;
                ct = ne.changedTouches;
                touch = (t && t.length) ? t[0] : ((ct && ct.length) ? ct[0] : undefined);

                if (touch) {
                    for (j = 0, len = touchEventProps.length; j < len; j++) {
                        prop = touchEventProps[j];
                        event[prop] = touch[prop];
                    }
                }
            }

            return event;
        }

        function getVirtualBindingFlags(element) {

            var flags = {},
                b, k;

            while (element) {

                b = $.data(element, dataPropertyName);

                for (k in b) {
                    if (b[k]) {
                        flags[k] = flags.hasVirtualBinding = true;
                    }
                }
                element = element.parentNode;
            }
            return flags;
        }

        function getClosestElementWithVirtualBinding(element, eventType) {
            var b;
            while (element) {

                b = $.data(element, dataPropertyName);

                if (b && (!eventType || b[eventType])) {
                    return element;
                }
                element = element.parentNode;
            }
            return null;
        }

        function enableTouchBindings() {
            blockTouchTriggers = false;
        }

        function disableTouchBindings() {
            blockTouchTriggers = true;
        }

        function enableMouseBindings() {
            lastTouchID = 0;
            clickBlockList.length = 0;
            blockMouseTriggers = false;

            // When mouse bindings are enabled, our
            // touch bindings are disabled.
            disableTouchBindings();
        }

        function disableMouseBindings() {
            // When mouse bindings are disabled, our
            // touch bindings are enabled.
            enableTouchBindings();
        }

        function startResetTimer() {
            clearResetTimer();
            resetTimerID = setTimeout(function () {
                resetTimerID = 0;
                enableMouseBindings();
            }, $.vmouse.resetTimerDuration);
        }

        function clearResetTimer() {
            if (resetTimerID) {
                clearTimeout(resetTimerID);
                resetTimerID = 0;
            }
        }

        function triggerVirtualEvent(eventType, event, flags) {
            var ve;

            if ((flags && flags[eventType]) ||
                        (!flags && getClosestElementWithVirtualBinding(event.target, eventType))) {

                ve = createVirtualEvent(event, eventType);

                $(event.target).trigger(ve);
            }

            return ve;
        }

        function mouseEventCallback(event) {
            var touchID = $.data(event.target, touchTargetPropertyName),
                ve;

            if (!blockMouseTriggers && (!lastTouchID || lastTouchID !== touchID)) {
                ve = triggerVirtualEvent("v" + event.type, event);
                if (ve) {
                    if (ve.isDefaultPrevented()) {
                        event.preventDefault();
                    }
                    if (ve.isPropagationStopped()) {
                        event.stopPropagation();
                    }
                    if (ve.isImmediatePropagationStopped()) {
                        event.stopImmediatePropagation();
                    }
                }
            }
        }

        function handleTouchStart(event) {

            var touches = getNativeEvent(event).touches,
                target, flags, t;

            if (touches && touches.length === 1) {

                target = event.target;
                flags = getVirtualBindingFlags(target);

                if (flags.hasVirtualBinding) {

                    lastTouchID = nextTouchID++;
                    $.data(target, touchTargetPropertyName, lastTouchID);

                    clearResetTimer();

                    disableMouseBindings();
                    didScroll = false;

                    t = getNativeEvent(event).touches[0];
                    startX = t.pageX;
                    startY = t.pageY;

                    triggerVirtualEvent("vmouseover", event, flags);
                    triggerVirtualEvent("vmousedown", event, flags);
                }
            }
        }

        function handleScroll(event) {
            if (blockTouchTriggers) {
                return;
            }

            if (!didScroll) {
                triggerVirtualEvent("vmousecancel", event, getVirtualBindingFlags(event.target));
            }

            didScroll = true;
            startResetTimer();
        }

        function handleTouchMove(event) {
            if (blockTouchTriggers) {
                return;
            }

            var t = getNativeEvent(event).touches[0],
                didCancel = didScroll,
                moveThreshold = $.vmouse.moveDistanceThreshold,
                flags = getVirtualBindingFlags(event.target);

            didScroll = didScroll ||
                (Math.abs(t.pageX - startX) > moveThreshold ||
                    Math.abs(t.pageY - startY) > moveThreshold);

            if (didScroll && !didCancel) {
                triggerVirtualEvent("vmousecancel", event, flags);
            }

            triggerVirtualEvent("vmousemove", event, flags);
            startResetTimer();
        }

        function handleTouchEnd(event) {
            if (blockTouchTriggers) {
                return;
            }

            disableTouchBindings();

            var flags = getVirtualBindingFlags(event.target),
                ve, t;
            triggerVirtualEvent("vmouseup", event, flags);

            if (!didScroll) {
                ve = triggerVirtualEvent("vclick", event, flags);
                if (ve && ve.isDefaultPrevented()) {
                    // The target of the mouse events that follow the touchend
                    // event don't necessarily match the target used during the
                    // touch. This means we need to rely on coordinates for blocking
                    // any click that is generated.
                    t = getNativeEvent(event).changedTouches[0];
                    clickBlockList.push({
                        touchID: lastTouchID,
                        x: t.clientX,
                        y: t.clientY
                    });

                    // Prevent any mouse events that follow from triggering
                    // virtual event notifications.
                    blockMouseTriggers = true;
                }
            }
            triggerVirtualEvent("vmouseout", event, flags);
            didScroll = false;

            startResetTimer();
        }

        function hasVirtualBindings(ele) {
            var bindings = $.data(ele, dataPropertyName),
                k;

            if (bindings) {
                for (k in bindings) {
                    if (bindings[k]) {
                        return true;
                    }
                }
            }
            return false;
        }

        function dummyMouseHandler() { }

        function getSpecialEventObject(eventType) {
            var realType = eventType.substr(1);

            return {
                setup: function (/* data, namespace */) {
                    // If this is the first virtual mouse binding for this element,
                    // add a bindings object to its data.

                    if (!hasVirtualBindings(this)) {
                        $.data(this, dataPropertyName, {});
                    }

                    // If setup is called, we know it is the first binding for this
                    // eventType, so initialize the count for the eventType to zero.
                    var bindings = $.data(this, dataPropertyName);
                    bindings[eventType] = true;

                    // If this is the first virtual mouse event for this type,
                    // register a global handler on the document.

                    activeDocHandlers[eventType] = (activeDocHandlers[eventType] || 0) + 1;

                    if (activeDocHandlers[eventType] === 1) {
                        $document.bind(realType, mouseEventCallback);
                    }

                    // Some browsers, like Opera Mini, won't dispatch mouse/click events
                    // for elements unless they actually have handlers registered on them.
                    // To get around this, we register dummy handlers on the elements.

                    $(this).bind(realType, dummyMouseHandler);

                    // For now, if event capture is not supported, we rely on mouse handlers.
                    if (eventCaptureSupported) {
                        // If this is the first virtual mouse binding for the document,
                        // register our touchstart handler on the document.

                        activeDocHandlers["touchstart"] = (activeDocHandlers["touchstart"] || 0) + 1;

                        if (activeDocHandlers["touchstart"] === 1) {
                            $document.bind("touchstart", handleTouchStart)
                                .bind("touchend", handleTouchEnd)

                                // On touch platforms, touching the screen and then dragging your finger
                                // causes the window content to scroll after some distance threshold is
                                // exceeded. On these platforms, a scroll prevents a click event from being
                                // dispatched, and on some platforms, even the touchend is suppressed. To
                                // mimic the suppression of the click event, we need to watch for a scroll
                                // event. Unfortunately, some platforms like iOS don't dispatch scroll
                                // events until *AFTER* the user lifts their finger (touchend). This means
                                // we need to watch both scroll and touchmove events to figure out whether
                                // or not a scroll happenens before the touchend event is fired.

                                .bind("touchmove", handleTouchMove)
                                .bind("scroll", handleScroll);
                        }
                    }
                },

                teardown: function (/* data, namespace */) {
                    // If this is the last virtual binding for this eventType,
                    // remove its global handler from the document.

                    --activeDocHandlers[eventType];

                    if (!activeDocHandlers[eventType]) {
                        $document.unbind(realType, mouseEventCallback);
                    }

                    if (eventCaptureSupported) {
                        // If this is the last virtual mouse binding in existence,
                        // remove our document touchstart listener.

                        --activeDocHandlers["touchstart"];

                        if (!activeDocHandlers["touchstart"]) {
                            $document.unbind("touchstart", handleTouchStart)
                                .unbind("touchmove", handleTouchMove)
                                .unbind("touchend", handleTouchEnd)
                                .unbind("scroll", handleScroll);
                        }
                    }

                    var $this = $(this),
                        bindings = $.data(this, dataPropertyName);

                    // teardown may be called when an element was
                    // removed from the DOM. If this is the case,
                    // jQuery core may have already stripped the element
                    // of any data bindings so we need to check it before
                    // using it.
                    if (bindings) {
                        bindings[eventType] = false;
                    }

                    // Unregister the dummy event handler.

                    $this.unbind(realType, dummyMouseHandler);

                    // If this is the last virtual mouse binding on the
                    // element, remove the binding data from the element.

                    if (!hasVirtualBindings(this)) {
                        $this.removeData(dataPropertyName);
                    }
                }
            };
        }

        // Expose our custom events to the jQuery bind/unbind mechanism.

        for (i = 0; i < virtualEventNames.length; i++) {
            $.event.special[virtualEventNames[i]] = getSpecialEventObject(virtualEventNames[i]);
        }

        // Add a capture click handler to block clicks.
        // Note that we require event capture support for this so if the device
        // doesn't support it, we punt for now and rely solely on mouse events.
        if (eventCaptureSupported) {
            document.addEventListener("click", function (e) {
                var cnt = clickBlockList.length,
                    target = e.target,
                    x, y, ele, i, o, touchID;

                if (cnt) {
                    x = e.clientX;
                    y = e.clientY;
                    threshold = $.vmouse.clickDistanceThreshold;

                    // The idea here is to run through the clickBlockList to see if
                    // the current click event is in the proximity of one of our
                    // vclick events that had preventDefault() called on it. If we find
                    // one, then we block the click.
                    //
                    // Why do we have to rely on proximity?
                    //
                    // Because the target of the touch event that triggered the vclick
                    // can be different from the target of the click event synthesized
                    // by the browser. The target of a mouse/click event that is synthesized
                    // from a touch event seems to be implementation specific. For example,
                    // some browsers will fire mouse/click events for a link that is near
                    // a touch event, even though the target of the touchstart/touchend event
                    // says the user touched outside the link. Also, it seems that with most
                    // browsers, the target of the mouse/click event is not calculated until the
                    // time it is dispatched, so if you replace an element that you touched
                    // with another element, the target of the mouse/click will be the new
                    // element underneath that point.
                    //
                    // Aside from proximity, we also check to see if the target and any
                    // of its ancestors were the ones that blocked a click. This is necessary
                    // because of the strange mouse/click target calculation done in the
                    // Android 2.1 browser, where if you click on an element, and there is a
                    // mouse/click handler on one of its ancestors, the target will be the
                    // innermost child of the touched element, even if that child is no where
                    // near the point of touch.

                    ele = target;

                    while (ele) {
                        for (i = 0; i < cnt; i++) {
                            o = clickBlockList[i];
                            touchID = 0;

                            if ((ele === target && Math.abs(o.x - x) < threshold && Math.abs(o.y - y) < threshold) ||
                                        $.data(ele, touchTargetPropertyName) === o.touchID) {
                                // XXX: We may want to consider removing matches from the block list
                                //      instead of waiting for the reset timer to fire.
                                e.preventDefault();
                                e.stopPropagation();
                                return;
                            }
                        }
                        ele = ele.parentNode;
                    }
                }
            }, true);
        }
    })(jQuery, window, document);

    (function ($) {
        $.mobile = {};
    }(jQuery));

    (function ($, undefined) {
        var support = {
            touch: "ontouchend" in document
        };

        $.mobile.support = $.mobile.support || {};
        $.extend($.support, support);
        $.extend($.mobile.support, support);
    }(jQuery));


    (function ($, window, undefined) {
        var $document = $(document),
            supportTouch = $.mobile.support.touch,
            scrollEvent = "touchmove scroll",
            touchStartEvent = supportTouch ? "touchstart" : "mousedown",
            touchStopEvent = supportTouch ? "touchend" : "mouseup",
            touchMoveEvent = supportTouch ? "touchmove" : "mousemove";

        // setup new event shortcuts
        $.each(("touchstart touchmove touchend " +
            "tap taphold " +
            "swipe swipeleft swiperight swipeup swipedown " +
            "scrollstart scrollstop").split(" "), function (i, name) {

                $.fn[name] = function (fn) {
                    return fn ? this.bind(name, fn) : this.trigger(name);
                };

                // jQuery < 1.8
                if ($.attrFn) {
                    $.attrFn[name] = true;
                }
            });

        function triggerCustomEvent(obj, eventType, event, bubble) {
            var originalType = event.type;
            event.type = eventType;
            if (bubble) {
                $.event.trigger(event, undefined, obj);
            } else {
                $.event.dispatch.call(obj, event);
            }
            event.type = originalType;
        }

        // also handles scrollstop
        $.event.special.scrollstart = {

            enabled: true,
            setup: function () {

                var thisObject = this,
                    $this = $(thisObject),
                    scrolling,
                    timer;

                function trigger(event, state) {
                    scrolling = state;
                    triggerCustomEvent(thisObject, scrolling ? "scrollstart" : "scrollstop", event);
                }

                // iPhone triggers scroll after a small delay; use touchmove instead
                $this.bind(scrollEvent, function (event) {

                    if (!$.event.special.scrollstart.enabled) {
                        return;
                    }

                    if (!scrolling) {
                        trigger(event, true);
                    }

                    clearTimeout(timer);
                    timer = setTimeout(function () {
                        trigger(event, false);
                    }, 50);
                });
            },
            teardown: function () {
                $(this).unbind(scrollEvent);
            }
        };

        // also handles taphold
        $.event.special.tap = {
            tapholdThreshold: 750,
            emitTapOnTaphold: true,
            setup: function () {
                var thisObject = this,
                    $this = $(thisObject),
                    isTaphold = false;

                $this.bind("vmousedown", function (event) {
                    isTaphold = false;
                    if (event.which && event.which !== 1) {
                        return false;
                    }

                    var origTarget = event.target,
                        timer;

                    function clearTapTimer() {
                        clearTimeout(timer);
                    }

                    function clearTapHandlers() {
                        clearTapTimer();

                        $this.unbind("vclick", clickHandler)
                            .unbind("vmouseup", clearTapTimer);
                        $document.unbind("vmousecancel", clearTapHandlers);
                    }

                    function clickHandler(event) {
                        clearTapHandlers();

                        // ONLY trigger a 'tap' event if the start target is
                        // the same as the stop target.
                        if (!isTaphold && origTarget === event.target) {
                            triggerCustomEvent(thisObject, "tap", event);
                        } else if (isTaphold) {
                            event.preventDefault();
                        }
                    }

                    $this.bind("vmouseup", clearTapTimer)
                        .bind("vclick", clickHandler);
                    $document.bind("vmousecancel", clearTapHandlers);

                    timer = setTimeout(function () {
                        if (!$.event.special.tap.emitTapOnTaphold) {
                            isTaphold = true;
                        }
                        triggerCustomEvent(thisObject, "taphold", $.Event("taphold", { target: origTarget }));
                    }, $.event.special.tap.tapholdThreshold);
                });
            },
            teardown: function () {
                $(this).unbind("vmousedown").unbind("vclick").unbind("vmouseup");
                $document.unbind("vmousecancel");
            }
        };

        // Also handles swipeleft, swiperight
        $.event.special.swipe = {

            // More than this horizontal displacement, and we will suppress scrolling.
            scrollSupressionThreshold: 30,

            // More time than this, and it isn't a swipe.
            durationThreshold: 1000,

            // Swipe horizontal displacement must be more than this.
            horizontalDistanceThreshold: 30,

            // Swipe vertical displacement must be less than this.
            verticalDistanceThreshold: 30,

            getLocation: function (event) {
                var winPageX = window.pageXOffset,
                    winPageY = window.pageYOffset,
                    x = event.clientX,
                    y = event.clientY;

                if (event.pageY === 0 && Math.floor(y) > Math.floor(event.pageY) ||
                    event.pageX === 0 && Math.floor(x) > Math.floor(event.pageX)) {

                    // iOS4 clientX/clientY have the value that should have been
                    // in pageX/pageY. While pageX/page/ have the value 0
                    x = x - winPageX;
                    y = y - winPageY;
                } else if (y < (event.pageY - winPageY) || x < (event.pageX - winPageX)) {

                    // Some Android browsers have totally bogus values for clientX/Y
                    // when scrolling/zooming a page. Detectable since clientX/clientY
                    // should never be smaller than pageX/pageY minus page scroll
                    x = event.pageX - winPageX;
                    y = event.pageY - winPageY;
                }

                return {
                    x: x,
                    y: y
                };
            },

            start: function (event) {
                var data = event.originalEvent.touches ?
                        event.originalEvent.touches[0] : event,
                    location = $.event.special.swipe.getLocation(data);
                return {
                    time: (new Date()).getTime(),
                    coords: [location.x, location.y],
                    origin: $(event.target)
                };
            },

            stop: function (event) {
                var data = event.originalEvent.touches ?
                        event.originalEvent.touches[0] : event,
                    location = $.event.special.swipe.getLocation(data);
                return {
                    time: (new Date()).getTime(),
                    coords: [location.x, location.y]
                };
            },

            handleSwipe: function (start, stop, thisObject, origTarget) {
                //TrinhNVd: Thêm sự kiện vuốt lên, xuống
                if (stop.time - start.time < $.event.special.swipe.durationThreshold) {
                    var direction = "";
                    if (Math.abs(start.coords[0] - stop.coords[0]) > $.event.special.swipe.horizontalDistanceThreshold &&
                        Math.abs(start.coords[1] - stop.coords[1]) < $.event.special.swipe.verticalDistanceThreshold) {
                        direction = start.coords[0] > stop.coords[0] ? "swipeleft" : "swiperight";
                    }
                    else if (Math.abs(start.coords[0] - stop.coords[0]) < $.event.special.swipe.horizontalDistanceThreshold &&
                        Math.abs(start.coords[1] - stop.coords[1]) > $.event.special.swipe.verticalDistanceThreshold) {
                        direction = start.coords[1] > stop.coords[1] ? "swipeup" : "swipedown";
                    }
                    triggerCustomEvent(thisObject, "swipe", $.Event("swipe", { target: origTarget, swipestart: start, swipestop: stop }), true);
                    triggerCustomEvent(thisObject, direction, $.Event(direction, { target: origTarget, swipestart: start, swipestop: stop }), true);
                    return true;
                }
                return false;
            },

            // This serves as a flag to ensure that at most one swipe event event is
            // in work at any given time
            eventInProgress: false,

            setup: function () {
                var events,
                    thisObject = this,
                    $this = $(thisObject),
                    context = {};

                // Retrieve the events data for this element and add the swipe context
                events = $.data(this, "mobile-events");
                if (!events) {
                    events = { length: 0 };
                    $.data(this, "mobile-events", events);
                }
                events.length++;
                events.swipe = context;

                context.start = function (event) {

                    // Bail if we're already working on a swipe event
                    if ($.event.special.swipe.eventInProgress) {
                        return;
                    }
                    $.event.special.swipe.eventInProgress = true;

                    var stop,
                        start = $.event.special.swipe.start(event),
                        origTarget = event.target,
                        emitted = false;

                    context.move = function (event) {
                        if (!start || event.isDefaultPrevented()) {
                            return;
                        }

                        stop = $.event.special.swipe.stop(event);
                        if (!emitted) {
                            emitted = $.event.special.swipe.handleSwipe(start, stop, thisObject, origTarget);
                            if (emitted) {

                                // Reset the context to make way for the next swipe event
                                $.event.special.swipe.eventInProgress = false;
                            }
                        }
                        // prevent scrolling
                        if (Math.abs(start.coords[0] - stop.coords[0]) > $.event.special.swipe.scrollSupressionThreshold) {
                            event.preventDefault();
                        }
                    };

                    context.stop = function () {
                        emitted = true;

                        // Reset the context to make way for the next swipe event
                        $.event.special.swipe.eventInProgress = false;
                        $document.off(touchMoveEvent, context.move);
                        context.move = null;
                    };

                    $document.on(touchMoveEvent, context.move)
                        .one(touchStopEvent, context.stop);
                };
                $this.on(touchStartEvent, context.start);
            },

            teardown: function () {
                var events, context;

                events = $.data(this, "mobile-events");
                if (events) {
                    context = events.swipe;
                    delete events.swipe;
                    events.length--;
                    if (events.length === 0) {
                        $.removeData(this, "mobile-events");
                    }
                }

                if (context) {
                    if (context.start) {
                        $(this).off(touchStartEvent, context.start);
                    }
                    if (context.move) {
                        $document.off(touchMoveEvent, context.move);
                    }
                    if (context.stop) {
                        $document.off(touchStopEvent, context.stop);
                    }
                }
            }
        };
        $.each({
            scrollstop: "scrollstart",
            taphold: "tap",
            swipeleft: "swipe.left",
            swiperight: "swipe.right",
            swipeup: "swipe.up",
            swipedown: "swipe.down"
        }, function (event, sourceEvent) {

            $.event.special[event] = {
                setup: function () {
                    $(this).bind(sourceEvent, $.noop);
                },
                teardown: function () {
                    $(this).unbind(sourceEvent);
                }
            };
        });

    })(jQuery, this);

    (function ($, undefined) {
        var props = {
            "animation": {},
            "transition": {}
        },
            testElement = document.createElement("a"),
            vendorPrefixes = ["", "webkit-", "moz-", "o-"];

        $.each(["animation", "transition"], function (i, test) {

            // Get correct name for test
            var testName = (i === 0) ? test + "-" + "name" : test;

            $.each(vendorPrefixes, function (j, prefix) {
                if (testElement.style[$.camelCase(prefix + testName)] !== undefined) {
                    props[test]["prefix"] = prefix;
                    return false;
                }
            });

            // Set event and duration names for later use
            props[test]["duration"] =
                $.camelCase(props[test]["prefix"] + test + "-" + "duration");
            props[test]["event"] =
                $.camelCase(props[test]["prefix"] + test + "-" + "end");

            // All lower case if not a vendor prop
            if (props[test]["prefix"] === "") {
                props[test]["event"] = props[test]["event"].toLowerCase();
            }
        });

        // If a valid prefix was found then the it is supported by the browser
        $.support.cssTransitions = (props["transition"]["prefix"] !== undefined);
        $.support.cssAnimations = (props["animation"]["prefix"] !== undefined);

        // Remove the testElement
        $(testElement).remove();

        // Animation complete callback
        $.fn.animationComplete = function (callback, type, fallbackTime) {
            var timer, duration,
                that = this,
                eventBinding = function () {

                    // Clear the timer so we don't call callback twice
                    clearTimeout(timer);
                    callback.apply(this, arguments);
                },
                animationType = (!type || type === "animation") ? "animation" : "transition";

            // Make sure selected type is supported by browser
            if (($.support.cssTransitions && animationType === "transition") ||
                ($.support.cssAnimations && animationType === "animation")) {

                // If a fallback time was not passed set one
                if (fallbackTime === undefined) {

                    // Make sure the was not bound to document before checking .css
                    if ($(this).context !== document) {

                        // Parse the durration since its in second multiple by 1000 for milliseconds
                        // Multiply by 3 to make sure we give the animation plenty of time.
                        duration = parseFloat(
                            $(this).css(props[animationType].duration)
                        ) * 3000;
                    }

                    // If we could not read a duration use the default
                    if (duration === 0 || duration === undefined || isNaN(duration)) {
                        duration = $.fn.animationComplete.defaultDuration;
                    }
                }

                // Sets up the fallback if event never comes
                timer = setTimeout(function () {
                    $(that).off(props[animationType].event, eventBinding);
                    callback.apply(that);
                }, duration);

                // Bind the event
                return $(this).one(props[animationType].event, eventBinding);
            } else {

                // CSS animation / transitions not supported
                // Defer execution for consistency between webkit/non webkit
                setTimeout($.proxy(callback, this), 0);
                return $(this);
            }
        };

        // Allow default callback to be configured on mobileInit
        $.fn.animationComplete.defaultDuration = 1000;
    })(jQuery);

    (function ($, window, undefined) {
        var nsNormalizeDict = {},
            oldFind = $.find,
            rbrace = /(?:\{[\s\S]*\}|\[[\s\S]*\])$/,
            jqmDataRE = /:jqmData\(([^)]*)\)/g;

        $.extend($.mobile, {

            // Namespace used framework-wide for data-attrs. Default is no namespace

            ns: "",

            // Retrieve an attribute from an element and perform some massaging of the value

            getAttribute: function (element, key) {
                var data;

                element = element.jquery ? element[0] : element;

                if (element && element.getAttribute) {
                    data = element.getAttribute("data-" + $.mobile.ns + key);
                }

                // Copied from core's src/data.js:dataAttr()
                // Convert from a string to a proper data type
                try {
                    data = data === "true" ? true :
                        data === "false" ? false :
                        data === "null" ? null :
                        // Only convert to a number if it doesn't change the string
                        +data + "" === data ? +data :
                        rbrace.test(data) ? JSON.parse(data) :
                        data;
                } catch (err) { }

                return data;
            },

            // Expose our cache for testing purposes.
            nsNormalizeDict: nsNormalizeDict,

            // Take a data attribute property, prepend the namespace
            // and then camel case the attribute string. Add the result
            // to our nsNormalizeDict so we don't have to do this again.
            nsNormalize: function (prop) {
                return nsNormalizeDict[prop] ||
                    (nsNormalizeDict[prop] = $.camelCase($.mobile.ns + prop));
            },

            // Find the closest javascript page element to gather settings data jsperf test
            // http://jsperf.com/single-complex-selector-vs-many-complex-selectors/edit
            // possibly naive, but it shows that the parsing overhead for *just* the page selector vs
            // the page and dialog selector is negligable. This could probably be speed up by
            // doing a similar parent node traversal to the one found in the inherited theme code above
            closestPageData: function ($target) {
                return $target
                    .closest(":jqmData(role='page'), :jqmData(role='dialog')")
                    .data("mobile-page");
            }

        });

        // Mobile version of data and removeData and hasData methods
        // ensures all data is set and retrieved using jQuery Mobile's data namespace
        $.fn.jqmData = function (prop, value) {
            var result;
            if (typeof prop !== "undefined") {
                if (prop) {
                    prop = $.mobile.nsNormalize(prop);
                }

                // undefined is permitted as an explicit input for the second param
                // in this case it returns the value and does not set it to undefined
                if (arguments.length < 2 || value === undefined) {
                    result = this.data(prop);
                } else {
                    result = this.data(prop, value);
                }
            }
            return result;
        };

        $.jqmData = function (elem, prop, value) {
            var result;
            if (typeof prop !== "undefined") {
                result = $.data(elem, prop ? $.mobile.nsNormalize(prop) : prop, value);
            }
            return result;
        };

        $.fn.jqmRemoveData = function (prop) {
            return this.removeData($.mobile.nsNormalize(prop));
        };

        $.jqmRemoveData = function (elem, prop) {
            return $.removeData(elem, $.mobile.nsNormalize(prop));
        };

        $.find = function (selector, context, ret, extra) {
            if (selector.indexOf(":jqmData") > -1) {
                selector = selector.replace(jqmDataRE, "[data-" + ($.mobile.ns || "") + "$1]");
            }

            return oldFind.call(this, selector, context, ret, extra);
        };

        $.extend($.find, oldFind);

    })(jQuery, this);

    (function ($, window, undefined) {
        $.extend($.mobile, {

            // Version of the jQuery Mobile Framework
            version: "1.4.5",

            // Deprecated and no longer used in 1.4 remove in 1.5
            // Define the url parameter used for referencing widget-generated sub-pages.
            // Translates to example.html&ui-page=subpageIdentifier
            // hash segment before &ui-page= is used to make Ajax request
            subPageUrlKey: "ui-page",

            hideUrlBar: true,

            // Keepnative Selector
            keepNative: ":jqmData(role='none'), :jqmData(role='nojs')",

            // Deprecated in 1.4 remove in 1.5
            // Class assigned to page currently in view, and during transitions
            activePageClass: "ui-page-active",

            // Deprecated in 1.4 remove in 1.5
            // Class used for "active" button state, from CSS framework
            activeBtnClass: "ui-btn-active",

            // Deprecated in 1.4 remove in 1.5
            // Class used for "focus" form element state, from CSS framework
            focusClass: "ui-focus",

            // Automatically handle clicks and form submissions through Ajax, when same-domain
            ajaxEnabled: true,

            // Automatically load and show pages based on location.hash
            hashListeningEnabled: true,

            // disable to prevent jquery from bothering with links
            linkBindingEnabled: true,

            // Set default page transition - 'none' for no transitions
            defaultPageTransition: "fade",

            // Set maximum window width for transitions to apply - 'false' for no limit
            maxTransitionWidth: false,

            // Minimum scroll distance that will be remembered when returning to a page
            // Deprecated remove in 1.5
            minScrollBack: 0,

            // Set default dialog transition - 'none' for no transitions
            defaultDialogTransition: "pop",

            // Error response message - appears when an Ajax page request fails
            pageLoadErrorMessage: "Error Loading Page",

            // For error messages, which theme does the box use?
            pageLoadErrorMessageTheme: "a",

            // replace calls to window.history.back with phonegaps navigation helper
            // where it is provided on the window object
            phonegapNavigationEnabled: false,

            //automatically initialize the DOM when it's ready
            autoInitializePage: true,

            pushStateEnabled: true,

            // allows users to opt in to ignoring content by marking a parent element as
            // data-ignored
            ignoreContentEnabled: false,

            buttonMarkup: {
                hoverDelay: 200
            },

            // disable the alteration of the dynamic base tag or links in the case
            // that a dynamic base tag isn't supported
            dynamicBaseEnabled: true,

            // default the property to remove dependency on assignment in init module
            pageContainer: $(),

            //enable cross-domain page support
            allowCrossDomainPages: false,

            dialogHashKey: "&ui-state=dialog"
        });
    })(jQuery, this);

    /*!
     * jQuery UI Core c0ab71056b936627e8a7821f03c044aec6280a40
     * http://jqueryui.com
     *
     * Copyright 2013 jQuery Foundation and other contributors
     * Released under the MIT license.
     * http://jquery.org/license
     *
     * http://api.jqueryui.com/category/ui-core/
     */
    (function ($, undefined) {

        var uuid = 0,
            runiqueId = /^ui-id-\d+$/;

        // $.ui might exist from components with no dependencies, e.g., $.ui.position
        $.ui = $.ui || {};

        $.extend($.ui, {
            version: "c0ab71056b936627e8a7821f03c044aec6280a40",

            keyCode: {
                BACKSPACE: 8,
                COMMA: 188,
                DELETE: 46,
                DOWN: 40,
                END: 35,
                ENTER: 13,
                ESCAPE: 27,
                HOME: 36,
                LEFT: 37,
                PAGE_DOWN: 34,
                PAGE_UP: 33,
                PERIOD: 190,
                RIGHT: 39,
                SPACE: 32,
                TAB: 9,
                UP: 38
            }
        });

        // plugins
        $.fn.extend({
            focus: (function (orig) {
                return function (delay, fn) {
                    return typeof delay === "number" ?
                        this.each(function () {
                            var elem = this;
                            setTimeout(function () {
                                $(elem).focus();
                                if (fn) {
                                    fn.call(elem);
                                }
                            }, delay);
                        }) :
                        orig.apply(this, arguments);
                };
            })($.fn.focus),

            scrollParent: function () {
                var scrollParent;
                if (($.ui.ie && (/(static|relative)/).test(this.css("position"))) || (/absolute/).test(this.css("position"))) {
                    scrollParent = this.parents().filter(function () {
                        return (/(relative|absolute|fixed)/).test($.css(this, "position")) && (/(auto|scroll)/).test($.css(this, "overflow") + $.css(this, "overflow-y") + $.css(this, "overflow-x"));
                    }).eq(0);
                } else {
                    scrollParent = this.parents().filter(function () {
                        return (/(auto|scroll)/).test($.css(this, "overflow") + $.css(this, "overflow-y") + $.css(this, "overflow-x"));
                    }).eq(0);
                }

                return (/fixed/).test(this.css("position")) || !scrollParent.length ? $(this[0].ownerDocument || document) : scrollParent;
            },

            uniqueId: function () {
                return this.each(function () {
                    if (!this.id) {
                        this.id = "ui-id-" + (++uuid);
                    }
                });
            },

            removeUniqueId: function () {
                return this.each(function () {
                    if (runiqueId.test(this.id)) {
                        $(this).removeAttr("id");
                    }
                });
            }
        });

        // selectors
        function focusable(element, isTabIndexNotNaN) {
            var map, mapName, img,
                nodeName = element.nodeName.toLowerCase();
            if ("area" === nodeName) {
                map = element.parentNode;
                mapName = map.name;
                if (!element.href || !mapName || map.nodeName.toLowerCase() !== "map") {
                    return false;
                }
                img = $("img[usemap=#" + mapName + "]")[0];
                return !!img && visible(img);
            }
            return (/input|select|textarea|button|object/.test(nodeName) ?
                !element.disabled :
                "a" === nodeName ?
                    element.href || isTabIndexNotNaN :
                    isTabIndexNotNaN) &&
                // the element and all of its ancestors must be visible
                visible(element);
        }

        function visible(element) {
            return $.expr.filters.visible(element) &&
                !$(element).parents().addBack().filter(function () {
                    return $.css(this, "visibility") === "hidden";
                }).length;
        }

        $.extend($.expr[":"], {
            data: $.expr.createPseudo ?
                $.expr.createPseudo(function (dataName) {
                    return function (elem) {
                        return !!$.data(elem, dataName);
                    };
                }) :
                // support: jQuery <1.8
                function (elem, i, match) {
                    return !!$.data(elem, match[3]);
                },

            focusable: function (element) {
                return focusable(element, !isNaN($.attr(element, "tabindex")));
            },

            tabbable: function (element) {
                var tabIndex = $.attr(element, "tabindex"),
                    isTabIndexNaN = isNaN(tabIndex);
                return (isTabIndexNaN || tabIndex >= 0) && focusable(element, !isTabIndexNaN);
            }
        });

        // support: jQuery <1.8
        if (!$("<a>").outerWidth(1).jquery) {
            $.each(["Width", "Height"], function (i, name) {
                var side = name === "Width" ? ["Left", "Right"] : ["Top", "Bottom"],
                    type = name.toLowerCase(),
                    orig = {
                        innerWidth: $.fn.innerWidth,
                        innerHeight: $.fn.innerHeight,
                        outerWidth: $.fn.outerWidth,
                        outerHeight: $.fn.outerHeight
                    };

                function reduce(elem, size, border, margin) {
                    $.each(side, function () {
                        size -= parseFloat($.css(elem, "padding" + this)) || 0;
                        if (border) {
                            size -= parseFloat($.css(elem, "border" + this + "Width")) || 0;
                        }
                        if (margin) {
                            size -= parseFloat($.css(elem, "margin" + this)) || 0;
                        }
                    });
                    return size;
                }

                $.fn["inner" + name] = function (size) {
                    if (size === undefined) {
                        return orig["inner" + name].call(this);
                    }

                    return this.each(function () {
                        $(this).css(type, reduce(this, size) + "px");
                    });
                };

                $.fn["outer" + name] = function (size, margin) {
                    if (typeof size !== "number") {
                        return orig["outer" + name].call(this, size);
                    }

                    return this.each(function () {
                        $(this).css(type, reduce(this, size, true, margin) + "px");
                    });
                };
            });
        }

        // support: jQuery <1.8
        if (!$.fn.addBack) {
            $.fn.addBack = function (selector) {
                return this.add(selector == null ?
                    this.prevObject : this.prevObject.filter(selector)
                );
            };
        }

        // support: jQuery 1.6.1, 1.6.2 (http://bugs.jquery.com/ticket/9413)
        if ($("<a>").data("a-b", "a").removeData("a-b").data("a-b")) {
            $.fn.removeData = (function (removeData) {
                return function (key) {
                    if (arguments.length) {
                        return removeData.call(this, $.camelCase(key));
                    } else {
                        return removeData.call(this);
                    }
                };
            })($.fn.removeData);
        }





        // deprecated
        $.ui.ie = !!/msie [\w.]+/.exec(navigator.userAgent.toLowerCase());

        $.support.selectstart = "onselectstart" in document.createElement("div");
        $.fn.extend({
            disableSelection: function () {
                return this.bind(($.support.selectstart ? "selectstart" : "mousedown") +
                    ".ui-disableSelection", function (event) {
                        event.preventDefault();
                    });
            },

            enableSelection: function () {
                return this.unbind(".ui-disableSelection");
            },

            zIndex: function (zIndex) {
                if (zIndex !== undefined) {
                    return this.css("zIndex", zIndex);
                }

                if (this.length) {
                    var elem = $(this[0]), position, value;
                    while (elem.length && elem[0] !== document) {
                        // Ignore z-index if position is set to a value where z-index is ignored by the browser
                        // This makes behavior of this function consistent across browsers
                        // WebKit always returns auto if the element is positioned
                        position = elem.css("position");
                        if (position === "absolute" || position === "relative" || position === "fixed") {
                            // IE returns 0 when zIndex is not specified
                            // other browsers return a string
                            // we ignore the case of nested elements with an explicit value of 0
                            // <div style="z-index: -10;"><div style="z-index: 0;"></div></div>
                            value = parseInt(elem.css("zIndex"), 10);
                            if (!isNaN(value) && value !== 0) {
                                return value;
                            }
                        }
                        elem = elem.parent();
                    }
                }

                return 0;
            }
        });

        // $.ui.plugin is deprecated. Use $.widget() extensions instead.
        $.ui.plugin = {
            add: function (module, option, set) {
                var i,
                    proto = $.ui[module].prototype;
                for (i in set) {
                    proto.plugins[i] = proto.plugins[i] || [];
                    proto.plugins[i].push([option, set[i]]);
                }
            },
            call: function (instance, name, args, allowDisconnected) {
                var i,
                    set = instance.plugins[name];

                if (!set) {
                    return;
                }

                if (!allowDisconnected && (!instance.element[0].parentNode || instance.element[0].parentNode.nodeType === 11)) {
                    return;
                }

                for (i = 0; i < set.length; i++) {
                    if (instance.options[set[i][0]]) {
                        set[i][1].apply(instance.element, args);
                    }
                }
            }
        };

    })(jQuery);

    (function ($, window, undefined) {

        // Subtract the height of external toolbars from the page height, if the page does not have
        // internal toolbars of the same type. We take care to use the widget options if we find a
        // widget instance and the element's data-attributes otherwise.
        var compensateToolbars = function (page, desiredHeight) {
            var pageParent = page.parent(),
                toolbarsAffectingHeight = [],

                // We use this function to filter fixed toolbars with option updatePagePadding set to
                // true (which is the default) from our height subtraction, because fixed toolbars with
                // option updatePagePadding set to true compensate for their presence by adding padding
                // to the active page. We want to avoid double-counting by also subtracting their
                // height from the desired page height.
                noPadders = function () {
                    var theElement = $(this),
                        widgetOptions = $.mobile.toolbar && theElement.data("mobile-toolbar") ?
                            theElement.toolbar("option") : {
                                position: theElement.attr("data-" + $.mobile.ns + "position"),
                                updatePagePadding: (theElement.attr("data-" + $.mobile.ns +
                                    "update-page-padding") !== false)
                            };

                    return !(widgetOptions.position === "fixed" &&
                        widgetOptions.updatePagePadding === true);
                },
                externalHeaders = pageParent.children(":jqmData(role='header')").filter(noPadders),
                internalHeaders = page.children(":jqmData(role='header')"),
                externalFooters = pageParent.children(":jqmData(role='footer')").filter(noPadders),
                internalFooters = page.children(":jqmData(role='footer')");

            // If we have no internal headers, but we do have external headers, then their height
            // reduces the page height
            if (internalHeaders.length === 0 && externalHeaders.length > 0) {
                toolbarsAffectingHeight = toolbarsAffectingHeight.concat(externalHeaders.toArray());
            }

            // If we have no internal footers, but we do have external footers, then their height
            // reduces the page height
            if (internalFooters.length === 0 && externalFooters.length > 0) {
                toolbarsAffectingHeight = toolbarsAffectingHeight.concat(externalFooters.toArray());
            }

            $.each(toolbarsAffectingHeight, function (index, value) {
                desiredHeight -= $(value).outerHeight();
            });

            // Height must be at least zero
            return Math.max(0, desiredHeight);
        };

        $.extend($.mobile, {
            // define the window and the document objects
            window: $(window),
            document: $(document),

            // TODO: Remove and use $.ui.keyCode directly
            keyCode: $.ui.keyCode,

            // Place to store various widget extensions
            behaviors: {},

            // Scroll page vertically: scroll to 0 to hide iOS address bar, or pass a Y value
            silentScroll: function (ypos) {
                if ($.type(ypos) !== "number") {
                    ypos = $.mobile.defaultHomeScroll;
                }

                // prevent scrollstart and scrollstop events
                $.event.special.scrollstart.enabled = false;

                setTimeout(function () {
                    window.scrollTo(0, ypos);
                    $.mobile.document.trigger("silentscroll", { x: 0, y: ypos });
                }, 20);

                setTimeout(function () {
                    $.event.special.scrollstart.enabled = true;
                }, 150);
            },

            getClosestBaseUrl: function (ele) {
                // Find the closest page and extract out its url.
                var url = $(ele).closest(".ui-page").jqmData("url"),
                    base = $.mobile.path.documentBase.hrefNoHash;

                if (!$.mobile.dynamicBaseEnabled || !url || !$.mobile.path.isPath(url)) {
                    url = base;
                }

                return $.mobile.path.makeUrlAbsolute(url, base);
            },
            removeActiveLinkClass: function (forceRemoval) {
                if (!!$.mobile.activeClickedLink &&
                    (!$.mobile.activeClickedLink.closest("." + $.mobile.activePageClass).length ||
                        forceRemoval)) {

                    $.mobile.activeClickedLink.removeClass($.mobile.activeBtnClass);
                }
                $.mobile.activeClickedLink = null;
            },

            // DEPRECATED in 1.4
            // Find the closest parent with a theme class on it. Note that
            // we are not using $.fn.closest() on purpose here because this
            // method gets called quite a bit and we need it to be as fast
            // as possible.
            getInheritedTheme: function (el, defaultTheme) {
                var e = el[0],
                    ltr = "",
                    re = /ui-(bar|body|overlay)-([a-z])\b/,
                    c, m;
                while (e) {
                    c = e.className || "";
                    if (c && (m = re.exec(c)) && (ltr = m[2])) {
                        // We found a parent with a theme class
                        // on it so bail from this loop.
                        break;
                    }

                    e = e.parentNode;
                }
                // Return the theme letter we found, if none, return the
                // specified default.
                return ltr || defaultTheme || "a";
            },

            enhanceable: function (elements) {
                return this.haveParents(elements, "enhance");
            },

            hijackable: function (elements) {
                return this.haveParents(elements, "ajax");
            },

            haveParents: function (elements, attr) {
                if (!$.mobile.ignoreContentEnabled) {
                    return elements;
                }

                var count = elements.length,
                    $newSet = $(),
                    e, $element, excluded,
                    i, c;

                for (i = 0; i < count; i++) {
                    $element = elements.eq(i);
                    excluded = false;
                    e = elements[i];

                    while (e) {
                        c = e.getAttribute ? e.getAttribute("data-" + $.mobile.ns + attr) : "";

                        if (c === "false") {
                            excluded = true;
                            break;
                        }

                        e = e.parentNode;
                    }

                    if (!excluded) {
                        $newSet = $newSet.add($element);
                    }
                }

                return $newSet;
            },

            getScreenHeight: function () {
                // Native innerHeight returns more accurate value for this across platforms,
                // jQuery version is here as a normalized fallback for platforms like Symbian
                return window.innerHeight || $.mobile.window.height();
            },

            //simply set the active page's minimum height to screen height, depending on orientation
            resetActivePageHeight: function (height) {
                var page = $("." + $.mobile.activePageClass),
                    pageHeight = page.height(),
                    pageOuterHeight = page.outerHeight(true);

                height = compensateToolbars(page,
                    (typeof height === "number") ? height : $.mobile.getScreenHeight());

                // Remove any previous min-height setting
                page.css("min-height", "");

                // Set the minimum height only if the height as determined by CSS is insufficient
                if (page.height() < height) {
                    page.css("min-height", height - (pageOuterHeight - pageHeight));
                }
            },

            loading: function () {
                // If this is the first call to this function, instantiate a loader widget
                var loader = this.loading._widget || $($.mobile.loader.prototype.defaultHtml).loader(),

                    // Call the appropriate method on the loader
                    returnValue = loader.loader.apply(loader, arguments);

                // Make sure the loader is retained for future calls to this function.
                this.loading._widget = loader;

                return returnValue;
            }
        });

        $.addDependents = function (elem, newDependents) {
            var $elem = $(elem),
                dependents = $elem.jqmData("dependents") || $();

            $elem.jqmData("dependents", $(dependents).add(newDependents));
        };

        // plugins
        $.fn.extend({
            removeWithDependents: function () {
                $.removeWithDependents(this);
            },

            // Enhance child elements
            enhanceWithin: function () {
                var index,
                    widgetElements = {},
                    keepNative = $.mobile.page.prototype.keepNativeSelector(),
                    that = this;

                // Add no js class to elements
                if ($.mobile.nojs) {
                    $.mobile.nojs(this);
                }

                // Bind links for ajax nav
                if ($.mobile.links) {
                    $.mobile.links(this);
                }

                // Degrade inputs for styleing
                if ($.mobile.degradeInputsWithin) {
                    $.mobile.degradeInputsWithin(this);
                }

                // Run buttonmarkup
                if ($.fn.buttonMarkup) {
                    this.find($.fn.buttonMarkup.initSelector).not(keepNative)
                    .jqmEnhanceable().buttonMarkup();
                }

                // Add classes for fieldContain
                if ($.fn.fieldcontain) {
                    this.find(":jqmData(role='fieldcontain')").not(keepNative)
                    .jqmEnhanceable().fieldcontain();
                }

                // Enhance widgets
                $.each($.mobile.widgets, function (name, constructor) {

                    // If initSelector not false find elements
                    if (constructor.initSelector) {

                        // Filter elements that should not be enhanced based on parents
                        var elements = $.mobile.enhanceable(that.find(constructor.initSelector));

                        // If any matching elements remain filter ones with keepNativeSelector
                        if (elements.length > 0) {

                            // $.mobile.page.prototype.keepNativeSelector is deprecated this is just for backcompat
                            // Switch to $.mobile.keepNative in 1.5 which is just a value not a function
                            elements = elements.not(keepNative);
                        }

                        // Enhance whatever is left
                        if (elements.length > 0) {
                            widgetElements[constructor.prototype.widgetName] = elements;
                        }
                    }
                });

                for (index in widgetElements) {
                    widgetElements[index][index]();
                }

                return this;
            },

            addDependents: function (newDependents) {
                $.addDependents(this, newDependents);
            },

            // note that this helper doesn't attempt to handle the callback
            // or setting of an html element's text, its only purpose is
            // to return the html encoded version of the text in all cases. (thus the name)
            getEncodedText: function () {
                return $("<a>").text(this.text()).html();
            },

            // fluent helper function for the mobile namespaced equivalent
            jqmEnhanceable: function () {
                return $.mobile.enhanceable(this);
            },

            jqmHijackable: function () {
                return $.mobile.hijackable(this);
            }
        });

        $.removeWithDependents = function (nativeElement) {
            var element = $(nativeElement);

            (element.jqmData("dependents") || $()).remove();
            element.remove();
        };
        $.addDependents = function (nativeElement, newDependents) {
            var element = $(nativeElement),
                dependents = element.jqmData("dependents") || $();

            element.jqmData("dependents", $(dependents).add(newDependents));
        };

        $.find.matches = function (expr, set) {
            return $.find(expr, null, null, set);
        };

        $.find.matchesSelector = function (node, expr) {
            return $.find(expr, null, null, [node]).length > 0;
        };

    })(jQuery, this);

    /*!
     * jQuery UI Widget c0ab71056b936627e8a7821f03c044aec6280a40
     * http://jqueryui.com
     *
     * Copyright 2013 jQuery Foundation and other contributors
     * Released under the MIT license.
     * http://jquery.org/license
     *
     * http://api.jqueryui.com/jQuery.widget/
     */
    (function ($, undefined) {

        var uuid = 0,
            slice = Array.prototype.slice,
            _cleanData = $.cleanData;
        $.cleanData = function (elems) {
            for (var i = 0, elem; (elem = elems[i]) != null; i++) {
                try {
                    $(elem).triggerHandler("remove");
                    // http://bugs.jquery.com/ticket/8235
                } catch (e) { }
            }
            _cleanData(elems);
        };

        $.widget = function (name, base, prototype) {
            var fullName, existingConstructor, constructor, basePrototype,
                // proxiedPrototype allows the provided prototype to remain unmodified
                // so that it can be used as a mixin for multiple widgets (#8876)
                proxiedPrototype = {},
                namespace = name.split(".")[0];

            name = name.split(".")[1];
            fullName = namespace + "-" + name;

            if (!prototype) {
                prototype = base;
                base = $.Widget;
            }

            // create selector for plugin
            $.expr[":"][fullName.toLowerCase()] = function (elem) {
                return !!$.data(elem, fullName);
            };

            $[namespace] = $[namespace] || {};
            existingConstructor = $[namespace][name];
            constructor = $[namespace][name] = function (options, element) {
                // allow instantiation without "new" keyword
                if (!this._createWidget) {
                    return new constructor(options, element);
                }

                // allow instantiation without initializing for simple inheritance
                // must use "new" keyword (the code above always passes args)
                if (arguments.length) {
                    this._createWidget(options, element);
                }
            };
            // extend with the existing constructor to carry over any static properties
            $.extend(constructor, existingConstructor, {
                version: prototype.version,
                // copy the object used to create the prototype in case we need to
                // redefine the widget later
                _proto: $.extend({}, prototype),
                // track widgets that inherit from this widget in case this widget is
                // redefined after a widget inherits from it
                _childConstructors: []
            });

            basePrototype = new base();
            // we need to make the options hash a property directly on the new instance
            // otherwise we'll modify the options hash on the prototype that we're
            // inheriting from
            basePrototype.options = $.widget.extend({}, basePrototype.options);
            $.each(prototype, function (prop, value) {
                if (!$.isFunction(value)) {
                    proxiedPrototype[prop] = value;
                    return;
                }
                proxiedPrototype[prop] = (function () {
                    var _super = function () {
                        return base.prototype[prop].apply(this, arguments);
                    },
                        _superApply = function (args) {
                            return base.prototype[prop].apply(this, args);
                        };
                    return function () {
                        var __super = this._super,
                            __superApply = this._superApply,
                            returnValue;

                        this._super = _super;
                        this._superApply = _superApply;

                        returnValue = value.apply(this, arguments);

                        this._super = __super;
                        this._superApply = __superApply;

                        return returnValue;
                    };
                })();
            });
            constructor.prototype = $.widget.extend(basePrototype, {
                // TODO: remove support for widgetEventPrefix
                // always use the name + a colon as the prefix, e.g., draggable:start
                // don't prefix for widgets that aren't DOM-based
                widgetEventPrefix: existingConstructor ? (basePrototype.widgetEventPrefix || name) : name
            }, proxiedPrototype, {
                constructor: constructor,
                namespace: namespace,
                widgetName: name,
                widgetFullName: fullName
            });

            // If this widget is being redefined then we need to find all widgets that
            // are inheriting from it and redefine all of them so that they inherit from
            // the new version of this widget. We're essentially trying to replace one
            // level in the prototype chain.
            if (existingConstructor) {
                $.each(existingConstructor._childConstructors, function (i, child) {
                    var childPrototype = child.prototype;

                    // redefine the child widget using the same prototype that was
                    // originally used, but inherit from the new version of the base
                    $.widget(childPrototype.namespace + "." + childPrototype.widgetName, constructor, child._proto);
                });
                // remove the list of existing child constructors from the old constructor
                // so the old child constructors can be garbage collected
                delete existingConstructor._childConstructors;
            } else {
                base._childConstructors.push(constructor);
            }

            $.widget.bridge(name, constructor);

            return constructor;
        };

        $.widget.extend = function (target) {
            var input = slice.call(arguments, 1),
                inputIndex = 0,
                inputLength = input.length,
                key,
                value;
            for (; inputIndex < inputLength; inputIndex++) {
                for (key in input[inputIndex]) {
                    value = input[inputIndex][key];
                    if (input[inputIndex].hasOwnProperty(key) && value !== undefined) {
                        // Clone objects
                        if ($.isPlainObject(value)) {
                            target[key] = $.isPlainObject(target[key]) ?
                                $.widget.extend({}, target[key], value) :
                                // Don't extend strings, arrays, etc. with objects
                                $.widget.extend({}, value);
                            // Copy everything else by reference
                        } else {
                            target[key] = value;
                        }
                    }
                }
            }
            return target;
        };

        $.widget.bridge = function (name, object) {
            var fullName = object.prototype.widgetFullName || name;
            $.fn[name] = function (options) {
                var isMethodCall = typeof options === "string",
                    args = slice.call(arguments, 1),
                    returnValue = this;

                // allow multiple hashes to be passed on init
                options = !isMethodCall && args.length ?
                    $.widget.extend.apply(null, [options].concat(args)) :
                    options;

                if (isMethodCall) {
                    this.each(function () {
                        var methodValue,
                            instance = $.data(this, fullName);
                        if (options === "instance") {
                            returnValue = instance;
                            return false;
                        }
                        if (!instance) {
                            return $.error("cannot call methods on " + name + " prior to initialization; " +
                                "attempted to call method '" + options + "'");
                        }
                        if (!$.isFunction(instance[options]) || options.charAt(0) === "_") {
                            return $.error("no such method '" + options + "' for " + name + " widget instance");
                        }
                        methodValue = instance[options].apply(instance, args);
                        if (methodValue !== instance && methodValue !== undefined) {
                            returnValue = methodValue && methodValue.jquery ?
                                returnValue.pushStack(methodValue.get()) :
                                methodValue;
                            return false;
                        }
                    });
                } else {
                    this.each(function () {
                        var instance = $.data(this, fullName);
                        if (instance) {
                            instance.option(options || {})._init();
                        } else {
                            $.data(this, fullName, new object(options, this));
                        }
                    });
                }

                return returnValue;
            };
        };

        $.Widget = function ( /* options, element */) { };
        $.Widget._childConstructors = [];

        $.Widget.prototype = {
            widgetName: "widget",
            widgetEventPrefix: "",
            defaultElement: "<div>",
            options: {
                disabled: false,

                // callbacks
                create: null
            },
            _createWidget: function (options, element) {
                element = $(element || this.defaultElement || this)[0];
                this.element = $(element);
                this.uuid = uuid++;
                this.eventNamespace = "." + this.widgetName + this.uuid;
                this.options = $.widget.extend({},
                    this.options,
                    this._getCreateOptions(),
                    options);

                this.bindings = $();
                this.hoverable = $();
                this.focusable = $();

                if (element !== this) {
                    $.data(element, this.widgetFullName, this);
                    this._on(true, this.element, {
                        remove: function (event) {
                            if (event.target === element) {
                                this.destroy();
                            }
                        }
                    });
                    this.document = $(element.style ?
                        // element within the document
                        element.ownerDocument :
                        // element is window or document
                        element.document || element);
                    this.window = $(this.document[0].defaultView || this.document[0].parentWindow);
                }

                this._create();
                this._trigger("create", null, this._getCreateEventData());
                this._init();
            },
            _getCreateOptions: $.noop,
            _getCreateEventData: $.noop,
            _create: $.noop,
            _init: $.noop,

            destroy: function () {
                this._destroy();
                // we can probably remove the unbind calls in 2.0
                // all event bindings should go through this._on()
                this.element
                    .unbind(this.eventNamespace)
                    .removeData(this.widgetFullName)
                    // support: jquery <1.6.3
                    // http://bugs.jquery.com/ticket/9413
                    .removeData($.camelCase(this.widgetFullName));
                this.widget()
                    .unbind(this.eventNamespace)
                    .removeAttr("aria-disabled")
                    .removeClass(
                        this.widgetFullName + "-disabled " +
                        "ui-state-disabled");

                // clean up events and states
                this.bindings.unbind(this.eventNamespace);
                this.hoverable.removeClass("ui-state-hover");
                this.focusable.removeClass("ui-state-focus");
            },
            _destroy: $.noop,

            widget: function () {
                return this.element;
            },

            option: function (key, value) {
                var options = key,
                    parts,
                    curOption,
                    i;

                if (arguments.length === 0) {
                    // don't return a reference to the internal hash
                    return $.widget.extend({}, this.options);
                }

                if (typeof key === "string") {
                    // handle nested keys, e.g., "foo.bar" => { foo: { bar: ___ } }
                    options = {};
                    parts = key.split(".");
                    key = parts.shift();
                    if (parts.length) {
                        curOption = options[key] = $.widget.extend({}, this.options[key]);
                        for (i = 0; i < parts.length - 1; i++) {
                            curOption[parts[i]] = curOption[parts[i]] || {};
                            curOption = curOption[parts[i]];
                        }
                        key = parts.pop();
                        if (value === undefined) {
                            return curOption[key] === undefined ? null : curOption[key];
                        }
                        curOption[key] = value;
                    } else {
                        if (value === undefined) {
                            return this.options[key] === undefined ? null : this.options[key];
                        }
                        options[key] = value;
                    }
                }

                this._setOptions(options);

                return this;
            },
            _setOptions: function (options) {
                var key;

                for (key in options) {
                    this._setOption(key, options[key]);
                }

                return this;
            },
            _setOption: function (key, value) {
                this.options[key] = value;

                if (key === "disabled") {
                    this.widget()
                        .toggleClass(this.widgetFullName + "-disabled", !!value);
                    this.hoverable.removeClass("ui-state-hover");
                    this.focusable.removeClass("ui-state-focus");
                }

                return this;
            },

            enable: function () {
                return this._setOptions({ disabled: false });
            },
            disable: function () {
                return this._setOptions({ disabled: true });
            },

            _on: function (suppressDisabledCheck, element, handlers) {
                var delegateElement,
                    instance = this;

                // no suppressDisabledCheck flag, shuffle arguments
                if (typeof suppressDisabledCheck !== "boolean") {
                    handlers = element;
                    element = suppressDisabledCheck;
                    suppressDisabledCheck = false;
                }

                // no element argument, shuffle and use this.element
                if (!handlers) {
                    handlers = element;
                    element = this.element;
                    delegateElement = this.widget();
                } else {
                    // accept selectors, DOM elements
                    element = delegateElement = $(element);
                    this.bindings = this.bindings.add(element);
                }

                $.each(handlers, function (event, handler) {
                    function handlerProxy() {
                        // allow widgets to customize the disabled handling
                        // - disabled as an array instead of boolean
                        // - disabled class as method for disabling individual parts
                        if (!suppressDisabledCheck &&
                                (instance.options.disabled === true ||
                                    $(this).hasClass("ui-state-disabled"))) {
                            return;
                        }
                        return (typeof handler === "string" ? instance[handler] : handler)
                            .apply(instance, arguments);
                    }

                    // copy the guid so direct unbinding works
                    if (typeof handler !== "string") {
                        handlerProxy.guid = handler.guid =
                            handler.guid || handlerProxy.guid || $.guid++;
                    }

                    var match = event.match(/^(\w+)\s*(.*)$/),
                        eventName = match[1] + instance.eventNamespace,
                        selector = match[2];
                    if (selector) {
                        delegateElement.delegate(selector, eventName, handlerProxy);
                    } else {
                        element.bind(eventName, handlerProxy);
                    }
                });
            },

            _off: function (element, eventName) {
                eventName = (eventName || "").split(" ").join(this.eventNamespace + " ") + this.eventNamespace;
                element.unbind(eventName).undelegate(eventName);
            },

            _delay: function (handler, delay) {
                function handlerProxy() {
                    return (typeof handler === "string" ? instance[handler] : handler)
                        .apply(instance, arguments);
                }
                var instance = this;
                return setTimeout(handlerProxy, delay || 0);
            },

            _hoverable: function (element) {
                this.hoverable = this.hoverable.add(element);
                this._on(element, {
                    mouseenter: function (event) {
                        $(event.currentTarget).addClass("ui-state-hover");
                    },
                    mouseleave: function (event) {
                        $(event.currentTarget).removeClass("ui-state-hover");
                    }
                });
            },

            _focusable: function (element) {
                this.focusable = this.focusable.add(element);
                this._on(element, {
                    focusin: function (event) {
                        $(event.currentTarget).addClass("ui-state-focus");
                    },
                    focusout: function (event) {
                        $(event.currentTarget).removeClass("ui-state-focus");
                    }
                });
            },

            _trigger: function (type, event, data) {
                var prop, orig,
                    callback = this.options[type];

                data = data || {};
                event = $.Event(event);
                event.type = (type === this.widgetEventPrefix ?
                    type :
                    this.widgetEventPrefix + type).toLowerCase();
                // the original event may come from any element
                // so we need to reset the target on the new event
                event.target = this.element[0];

                // copy original event properties over to the new event
                orig = event.originalEvent;
                if (orig) {
                    for (prop in orig) {
                        if (!(prop in event)) {
                            event[prop] = orig[prop];
                        }
                    }
                }

                this.element.trigger(event, data);
                return !($.isFunction(callback) &&
                    callback.apply(this.element[0], [event].concat(data)) === false ||
                    event.isDefaultPrevented());
            }
        };

        $.each({ show: "fadeIn", hide: "fadeOut" }, function (method, defaultEffect) {
            $.Widget.prototype["_" + method] = function (element, options, callback) {
                if (typeof options === "string") {
                    options = { effect: options };
                }
                var hasOptions,
                    effectName = !options ?
                    method :
                        options === true || typeof options === "number" ?
                    defaultEffect :
                            options.effect || defaultEffect;
                options = options || {};
                if (typeof options === "number") {
                    options = { duration: options };
                }
                hasOptions = !$.isEmptyObject(options);
                options.complete = callback;
                if (options.delay) {
                    element.delay(options.delay);
                }
                if (hasOptions && $.effects && $.effects.effect[effectName]) {
                    element[method](options);
                } else if (effectName !== method && element[effectName]) {
                    element[effectName](options.duration, options.easing, callback);
                } else {
                    element.queue(function (next) {
                        $(this)[method]();
                        if (callback) {
                            callback.call(element[0]);
                        }
                        next();
                    });
                }
            };
        });

    })(jQuery);

    (function ($, undefined) {

        var rcapitals = /[A-Z]/g,
            replaceFunction = function (c) {
                return "-" + c.toLowerCase();
            };

        $.extend($.Widget.prototype, {
            _getCreateOptions: function () {
                var option, value,
                    elem = this.element[0],
                    options = {};

                //
                if (!$.mobile.getAttribute(elem, "defaults")) {
                    for (option in this.options) {
                        value = $.mobile.getAttribute(elem, option.replace(rcapitals, replaceFunction));

                        if (value != null) {
                            options[option] = value;
                        }
                    }
                }

                return options;
            }
        });

        //TODO: Remove in 1.5 for backcompat only
        $.mobile.widget = $.Widget;

    })(jQuery);


    (function ($) {
        var meta = $("meta[name=viewport]"),
            initialContent = meta.attr("content"),
            disabledZoom = initialContent + ",maximum-scale=1, user-scalable=no",
            enabledZoom = initialContent + ",maximum-scale=10, user-scalable=yes",
            disabledInitially = /(user-scalable[\s]*=[\s]*no)|(maximum-scale[\s]*=[\s]*1)[$,\s]/.test(initialContent);

        $.mobile.zoom = $.extend({}, {
            enabled: !disabledInitially,
            locked: false,
            disable: function (lock) {
                if (!disabledInitially && !$.mobile.zoom.locked) {
                    meta.attr("content", disabledZoom);
                    $.mobile.zoom.enabled = false;
                    $.mobile.zoom.locked = lock || false;
                }
            },
            enable: function (unlock) {
                if (!disabledInitially && (!$.mobile.zoom.locked || unlock === true)) {
                    meta.attr("content", enabledZoom);
                    $.mobile.zoom.enabled = true;
                    $.mobile.zoom.locked = false;
                }
            },
            restore: function () {
                if (!disabledInitially) {
                    meta.attr("content", initialContent);
                    $.mobile.zoom.enabled = true;
                }
            }
        });

    }(jQuery));

    (function ($, window) {

        $.mobile.iosorientationfixEnabled = true;

        // This fix addresses an iOS bug, so return early if the UA claims it's something else.
        var ua = navigator.userAgent,
            zoom,
            evt, x, y, z, aig;
        if (!(/iPhone|iPad|iPod/.test(navigator.platform) && /OS [1-5]_[0-9_]* like Mac OS X/i.test(ua) && ua.indexOf("AppleWebKit") > -1)) {
            $.mobile.iosorientationfixEnabled = false;
            return;
        }

        zoom = $.mobile.zoom;

        function checkTilt(e) {
            evt = e.originalEvent;
            aig = evt.accelerationIncludingGravity;

            x = Math.abs(aig.x);
            y = Math.abs(aig.y);
            z = Math.abs(aig.z);

            // If portrait orientation and in one of the danger zones
            if (!window.orientation && (x > 7 || ((z > 6 && y < 8 || z < 8 && y > 6) && x > 5))) {
                if (zoom.enabled) {
                    zoom.disable();
                }
            } else if (!zoom.enabled) {
                zoom.enable();
            }
        }

        $.mobile.document.on("mobileinit", function () {
            if ($.mobile.iosorientationfixEnabled) {
                $.mobile.window
                    .bind("orientationchange.iosorientationfix", zoom.enable)
                    .bind("devicemotion.iosorientationfix", checkTilt);
            }
        });

    }(jQuery, this));


    (function ($, undefined) {
        var path, $base, dialogHashKey = "&ui-state=dialog";

        $.mobile.path = path = {
            uiStateKey: "&ui-state",

            // This scary looking regular expression parses an absolute URL or its relative
            // variants (protocol, site, document, query, and hash), into the various
            // components (protocol, host, path, query, fragment, etc that make up the
            // URL as well as some other commonly used sub-parts. When used with RegExp.exec()
            // or String.match, it parses the URL into a results array that looks like this:
            //
            //     [0]: http://jblas:password@mycompany.com:8080/mail/inbox?msg=1234&type=unread#msg-content
            //     [1]: http://jblas:password@mycompany.com:8080/mail/inbox?msg=1234&type=unread
            //     [2]: http://jblas:password@mycompany.com:8080/mail/inbox
            //     [3]: http://jblas:password@mycompany.com:8080
            //     [4]: http:
            //     [5]: //
            //     [6]: jblas:password@mycompany.com:8080
            //     [7]: jblas:password
            //     [8]: jblas
            //     [9]: password
            //    [10]: mycompany.com:8080
            //    [11]: mycompany.com
            //    [12]: 8080
            //    [13]: /mail/inbox
            //    [14]: /mail/
            //    [15]: inbox
            //    [16]: ?msg=1234&type=unread
            //    [17]: #msg-content
            //
            urlParseRE: /^\s*(((([^:\/#\?]+:)?(?:(\/\/)((?:(([^:@\/#\?]+)(?:\:([^:@\/#\?]+))?)@)?(([^:\/#\?\]\[]+|\[[^\/\]@#?]+\])(?:\:([0-9]+))?))?)?)?((\/?(?:[^\/\?#]+\/+)*)([^\?#]*)))?(\?[^#]+)?)(#.*)?/,

            // Abstraction to address xss (Issue #4787) by removing the authority in
            // browsers that auto-decode it. All references to location.href should be
            // replaced with a call to this method so that it can be dealt with properly here
            getLocation: function (url) {
                var parsedUrl = this.parseUrl(url || location.href),
					uri = url ? parsedUrl : location,

					// Make sure to parse the url or the location object for the hash because using
					// location.hash is autodecoded in firefox, the rest of the url should be from
					// the object (location unless we're testing) to avoid the inclusion of the
					// authority
					hash = parsedUrl.hash;

                // mimic the browser with an empty string when the hash is empty
                hash = hash === "#" ? "" : hash;

                return uri.protocol +
					parsedUrl.doubleSlash +
					uri.host +

					// The pathname must start with a slash if there's a protocol, because you
					// can't have a protocol followed by a relative path. Also, it's impossible to
					// calculate absolute URLs from relative ones if the absolute one doesn't have
					// a leading "/".
					((uri.protocol !== "" && uri.pathname.substring(0, 1) !== "/") ?
						"/" : "") +
					uri.pathname +
					uri.search +
					hash;
            },

            //return the original document url
            getDocumentUrl: function (asParsedObject) {
                return asParsedObject ? $.extend({}, path.documentUrl) : path.documentUrl.href;
            },

            parseLocation: function () {
                return this.parseUrl(this.getLocation());
            },

            //Parse a URL into a structure that allows easy access to
            //all of the URL components by name.
            parseUrl: function (url) {
                // If we're passed an object, we'll assume that it is
                // a parsed url object and just return it back to the caller.
                if ($.type(url) === "object") {
                    return url;
                }

                var matches = path.urlParseRE.exec(url || "") || [];

                // Create an object that allows the caller to access the sub-matches
                // by name. Note that IE returns an empty string instead of undefined,
                // like all other browsers do, so we normalize everything so its consistent
                // no matter what browser we're running on.
                return {
                    href: matches[0] || "",
                    hrefNoHash: matches[1] || "",
                    hrefNoSearch: matches[2] || "",
                    domain: matches[3] || "",
                    protocol: matches[4] || "",
                    doubleSlash: matches[5] || "",
                    authority: matches[6] || "",
                    username: matches[8] || "",
                    password: matches[9] || "",
                    host: matches[10] || "",
                    hostname: matches[11] || "",
                    port: matches[12] || "",
                    pathname: matches[13] || "",
                    directory: matches[14] || "",
                    filename: matches[15] || "",
                    search: matches[16] || "",
                    hash: matches[17] || ""
                };
            },

            //Turn relPath into an asbolute path. absPath is
            //an optional absolute path which describes what
            //relPath is relative to.
            makePathAbsolute: function (relPath, absPath) {
                var absStack,
					relStack,
					i, d;

                if (relPath && relPath.charAt(0) === "/") {
                    return relPath;
                }

                relPath = relPath || "";
                absPath = absPath ? absPath.replace(/^\/|(\/[^\/]*|[^\/]+)$/g, "") : "";

                absStack = absPath ? absPath.split("/") : [];
                relStack = relPath.split("/");

                for (i = 0; i < relStack.length; i++) {
                    d = relStack[i];
                    switch (d) {
                        case ".":
                            break;
                        case "..":
                            if (absStack.length) {
                                absStack.pop();
                            }
                            break;
                        default:
                            absStack.push(d);
                            break;
                    }
                }
                return "/" + absStack.join("/");
            },

            //Returns true if both urls have the same domain.
            isSameDomain: function (absUrl1, absUrl2) {
                return path.parseUrl(absUrl1).domain.toLowerCase() ===
					path.parseUrl(absUrl2).domain.toLowerCase();
            },

            //Returns true for any relative variant.
            isRelativeUrl: function (url) {
                // All relative Url variants have one thing in common, no protocol.
                return path.parseUrl(url).protocol === "";
            },

            //Returns true for an absolute url.
            isAbsoluteUrl: function (url) {
                return path.parseUrl(url).protocol !== "";
            },

            //Turn the specified realtive URL into an absolute one. This function
            //can handle all relative variants (protocol, site, document, query, fragment).
            makeUrlAbsolute: function (relUrl, absUrl) {
                if (!path.isRelativeUrl(relUrl)) {
                    return relUrl;
                }

                if (absUrl === undefined) {
                    absUrl = this.documentBase;
                }

                var relObj = path.parseUrl(relUrl),
					absObj = path.parseUrl(absUrl),
					protocol = relObj.protocol || absObj.protocol,
					doubleSlash = relObj.protocol ? relObj.doubleSlash : (relObj.doubleSlash || absObj.doubleSlash),
					authority = relObj.authority || absObj.authority,
					hasPath = relObj.pathname !== "",
					pathname = path.makePathAbsolute(relObj.pathname || absObj.filename, absObj.pathname),
					search = relObj.search || (!hasPath && absObj.search) || "",
					hash = relObj.hash;

                return protocol + doubleSlash + authority + pathname + search + hash;
            },

            //Add search (aka query) params to the specified url.
            addSearchParams: function (url, params) {
                var u = path.parseUrl(url),
					p = (typeof params === "object") ? $.param(params) : params,
					s = u.search || "?";
                return u.hrefNoSearch + s + (s.charAt(s.length - 1) !== "?" ? "&" : "") + p + (u.hash || "");
            },

            convertUrlToDataUrl: function (absUrl) {
                var result = absUrl,
					u = path.parseUrl(absUrl);

                if (path.isEmbeddedPage(u)) {
                    // For embedded pages, remove the dialog hash key as in getFilePath(),
                    // and remove otherwise the Data Url won't match the id of the embedded Page.
                    result = u.hash
						.split(dialogHashKey)[0]
						.replace(/^#/, "")
						.replace(/\?.*$/, "");
                } else if (path.isSameDomain(u, this.documentBase)) {
                    result = u.hrefNoHash.replace(this.documentBase.domain, "").split(dialogHashKey)[0];
                }

                return window.decodeURIComponent(result);
            },

            //get path from current hash, or from a file path
            get: function (newPath) {
                if (newPath === undefined) {
                    newPath = path.parseLocation().hash;
                }
                return path.stripHash(newPath).replace(/[^\/]*\.[^\/*]+$/, "");
            },

            //set location hash to path
            set: function (path) {
                location.hash = path;
            },

            //test if a given url (string) is a path
            //NOTE might be exceptionally naive
            isPath: function (url) {
                return (/\//).test(url);
            },

            //return a url path with the window's location protocol/hostname/pathname removed
            clean: function (url) {
                return url.replace(this.documentBase.domain, "");
            },

            //just return the url without an initial #
            stripHash: function (url) {
                return url.replace(/^#/, "");
            },

            stripQueryParams: function (url) {
                return url.replace(/\?.*$/, "");
            },

            //remove the preceding hash, any query params, and dialog notations
            cleanHash: function (hash) {
                return path.stripHash(hash.replace(/\?.*$/, "").replace(dialogHashKey, ""));
            },

            isHashValid: function (hash) {
                return (/^#[^#]+$/).test(hash);
            },

            //check whether a url is referencing the same domain, or an external domain or different protocol
            //could be mailto, etc
            isExternal: function (url) {
                var u = path.parseUrl(url);

                return !!(u.protocol &&
					(u.domain.toLowerCase() !== this.documentUrl.domain.toLowerCase()));
            },

            hasProtocol: function (url) {
                return (/^(:?\w+:)/).test(url);
            },

            isEmbeddedPage: function (url) {
                var u = path.parseUrl(url);

                //if the path is absolute, then we need to compare the url against
                //both the this.documentUrl and the documentBase. The main reason for this
                //is that links embedded within external documents will refer to the
                //application document, whereas links embedded within the application
                //document will be resolved against the document base.
                if (u.protocol !== "") {
                    return (!this.isPath(u.hash) && u.hash && (u.hrefNoHash === this.documentUrl.hrefNoHash || (this.documentBaseDiffers && u.hrefNoHash === this.documentBase.hrefNoHash)));
                }
                return (/^#/).test(u.href);
            },

            squash: function (url, resolutionUrl) {
                var href, cleanedUrl, search, stateIndex, docUrl,
					isPath = this.isPath(url),
					uri = this.parseUrl(url),
					preservedHash = uri.hash,
					uiState = "";

                // produce a url against which we can resolve the provided path
                if (!resolutionUrl) {
                    if (isPath) {
                        resolutionUrl = path.getLocation();
                    } else {
                        docUrl = path.getDocumentUrl(true);
                        if (path.isPath(docUrl.hash)) {
                            resolutionUrl = path.squash(docUrl.href);
                        } else {
                            resolutionUrl = docUrl.href;
                        }
                    }
                }

                // If the url is anything but a simple string, remove any preceding hash
                // eg #foo/bar -> foo/bar
                //    #foo -> #foo
                cleanedUrl = isPath ? path.stripHash(url) : url;

                // If the url is a full url with a hash check if the parsed hash is a path
                // if it is, strip the #, and use it otherwise continue without change
                cleanedUrl = path.isPath(uri.hash) ? path.stripHash(uri.hash) : cleanedUrl;

                // Split the UI State keys off the href
                stateIndex = cleanedUrl.indexOf(this.uiStateKey);

                // store the ui state keys for use
                if (stateIndex > -1) {
                    uiState = cleanedUrl.slice(stateIndex);
                    cleanedUrl = cleanedUrl.slice(0, stateIndex);
                }

                // make the cleanedUrl absolute relative to the resolution url
                href = path.makeUrlAbsolute(cleanedUrl, resolutionUrl);

                // grab the search from the resolved url since parsing from
                // the passed url may not yield the correct result
                search = this.parseUrl(href).search;

                // TODO all this crap is terrible, clean it up
                if (isPath) {
                    // reject the hash if it's a path or it's just a dialog key
                    if (path.isPath(preservedHash) || preservedHash.replace("#", "").indexOf(this.uiStateKey) === 0) {
                        preservedHash = "";
                    }

                    // Append the UI State keys where it exists and it's been removed
                    // from the url
                    if (uiState && preservedHash.indexOf(this.uiStateKey) === -1) {
                        preservedHash += uiState;
                    }

                    // make sure that pound is on the front of the hash
                    if (preservedHash.indexOf("#") === -1 && preservedHash !== "") {
                        preservedHash = "#" + preservedHash;
                    }

                    // reconstruct each of the pieces with the new search string and hash
                    href = path.parseUrl(href);
                    href = href.protocol + href.doubleSlash + href.host + href.pathname + search +
						preservedHash;
                } else {
                    href += href.indexOf("#") > -1 ? uiState : "#" + uiState;
                }

                return href;
            },

            isPreservableHash: function (hash) {
                return hash.replace("#", "").indexOf(this.uiStateKey) === 0;
            },

            // Escape weird characters in the hash if it is to be used as a selector
            hashToSelector: function (hash) {
                var hasHash = (hash.substring(0, 1) === "#");
                if (hasHash) {
                    hash = hash.substring(1);
                }
                return (hasHash ? "#" : "") + hash.replace(/([!"#$%&'()*+,./:;<=>?@[\]^`{|}~])/g, "\\$1");
            },

            // return the substring of a filepath before the dialogHashKey, for making a server
            // request
            getFilePath: function (path) {
                return path && path.split(dialogHashKey)[0];
            },

            // check if the specified url refers to the first page in the main
            // application document.
            isFirstPageUrl: function (url) {
                // We only deal with absolute paths.
                var u = path.parseUrl(path.makeUrlAbsolute(url, this.documentBase)),

					// Does the url have the same path as the document?
					samePath = u.hrefNoHash === this.documentUrl.hrefNoHash ||
						(this.documentBaseDiffers &&
							u.hrefNoHash === this.documentBase.hrefNoHash),

					// Get the first page element.
					fp = $.mobile.firstPage,

					// Get the id of the first page element if it has one.
					fpId = fp && fp[0] ? fp[0].id : undefined;

                // The url refers to the first page if the path matches the document and
                // it either has no hash value, or the hash is exactly equal to the id
                // of the first page element.
                return samePath &&
					(!u.hash ||
						u.hash === "#" ||
						(fpId && u.hash.replace(/^#/, "") === fpId));
            },

            // Some embedded browsers, like the web view in Phone Gap, allow
            // cross-domain XHR requests if the document doing the request was loaded
            // via the file:// protocol. This is usually to allow the application to
            // "phone home" and fetch app specific data. We normally let the browser
            // handle external/cross-domain urls, but if the allowCrossDomainPages
            // option is true, we will allow cross-domain http/https requests to go
            // through our page loading logic.
            isPermittedCrossDomainRequest: function (docUrl, reqUrl) {
                return $.mobile.allowCrossDomainPages &&
					(docUrl.protocol === "file:" || docUrl.protocol === "content:") &&
					reqUrl.search(/^https?:/) !== -1;
            }
        };

        path.documentUrl = path.parseLocation();

        $base = $("head").find("base");

        path.documentBase = $base.length ?
			path.parseUrl(path.makeUrlAbsolute($base.attr("href"), path.documentUrl.href)) :
			path.documentUrl;

        path.documentBaseDiffers = (path.documentUrl.hrefNoHash !== path.documentBase.hrefNoHash);

        //return the original document base url
        path.getDocumentBase = function (asParsedObject) {
            return asParsedObject ? $.extend({}, path.documentBase) : path.documentBase.href;
        };

        // DEPRECATED as of 1.4.0 - remove in 1.5.0
        $.extend($.mobile, {

            //return the original document url
            getDocumentUrl: path.getDocumentUrl,

            //return the original document base url
            getDocumentBase: path.getDocumentBase
        });
    })(jQuery);



    (function ($, undefined) {

        // existing base tag?
        var baseElement = $("head").children("base"),

        // base element management, defined depending on dynamic base tag support
        // TODO move to external widget
        base = {

            // define base element, for use in routing asset urls that are referenced
            // in Ajax-requested markup
            element: (baseElement.length ? baseElement :
                $("<base>", { href: $.mobile.path.documentBase.hrefNoHash }).prependTo($("head"))),

            linkSelector: "[src], link[href], a[rel='external'], :jqmData(ajax='false'), a[target]",

            // set the generated BASE element's href to a new page's base path
            set: function (href) {

                // we should do nothing if the user wants to manage their url base
                // manually
                if (!$.mobile.dynamicBaseEnabled) {
                    return;
                }

                // we should use the base tag if we can manipulate it dynamically
                if ($.support.dynamicBaseTag) {
                    base.element.attr("href",
                        $.mobile.path.makeUrlAbsolute(href, $.mobile.path.documentBase));
                }
            },

            rewrite: function (href, page) {
                var newPath = $.mobile.path.get(href);

                page.find(base.linkSelector).each(function (i, link) {
                    var thisAttr = $(link).is("[href]") ? "href" :
                        $(link).is("[src]") ? "src" : "action",
                    theLocation = $.mobile.path.parseLocation(),
                    thisUrl = $(link).attr(thisAttr);

                    // XXX_jblas: We need to fix this so that it removes the document
                    //            base URL, and then prepends with the new page URL.
                    // if full path exists and is same, chop it - helps IE out
                    thisUrl = thisUrl.replace(theLocation.protocol + theLocation.doubleSlash +
                        theLocation.host + theLocation.pathname, "");

                    if (!/^(\w+:|#|\/)/.test(thisUrl)) {
                        $(link).attr(thisAttr, newPath + thisUrl);
                    }
                });
            },

            // set the generated BASE element's href to a new page's base path
            reset: function (/* href */) {
                base.element.attr("href", $.mobile.path.documentBase.hrefNoSearch);
            }
        };

        $.mobile.base = base;

    })(jQuery);



    (function ($, undefined) {
        $.mobile.History = function (stack, index) {
            this.stack = stack || [];
            this.activeIndex = index || 0;
        };

        $.extend($.mobile.History.prototype, {
            getActive: function () {
                return this.stack[this.activeIndex];
            },

            getLast: function () {
                return this.stack[this.previousIndex];
            },

            getNext: function () {
                return this.stack[this.activeIndex + 1];
            },

            getPrev: function () {
                return this.stack[this.activeIndex - 1];
            },

            // addNew is used whenever a new page is added
            add: function (url, data) {
                data = data || {};

                //if there's forward history, wipe it
                if (this.getNext()) {
                    this.clearForward();
                }

                // if the hash is included in the data make sure the shape
                // is consistent for comparison
                if (data.hash && data.hash.indexOf("#") === -1) {
                    data.hash = "#" + data.hash;
                }

                data.url = url;
                this.stack.push(data);
                this.activeIndex = this.stack.length - 1;
            },

            //wipe urls ahead of active index
            clearForward: function () {
                this.stack = this.stack.slice(0, this.activeIndex + 1);
            },

            find: function (url, stack, earlyReturn) {
                stack = stack || this.stack;

                var entry, i, length = stack.length, index;

                for (i = 0; i < length; i++) {
                    entry = stack[i];

                    if (decodeURIComponent(url) === decodeURIComponent(entry.url) ||
                        decodeURIComponent(url) === decodeURIComponent(entry.hash)) {
                        index = i;

                        if (earlyReturn) {
                            return index;
                        }
                    }
                }

                return index;
            },

            closest: function (url) {
                var closest, a = this.activeIndex;

                // First, take the slice of the history stack before the current index and search
                // for a url match. If one is found, we'll avoid avoid looking through forward history
                // NOTE the preference for backward history movement is driven by the fact that
                //      most mobile browsers only have a dedicated back button, and users rarely use
                //      the forward button in desktop browser anyhow
                closest = this.find(url, this.stack.slice(0, a));

                // If nothing was found in backward history check forward. The `true`
                // value passed as the third parameter causes the find method to break
                // on the first match in the forward history slice. The starting index
                // of the slice must then be added to the result to get the element index
                // in the original history stack :( :(
                //
                // TODO this is hyper confusing and should be cleaned up (ugh so bad)
                if (closest === undefined) {
                    closest = this.find(url, this.stack.slice(a), true);
                    closest = closest === undefined ? closest : closest + a;
                }

                return closest;
            },

            direct: function (opts) {
                var newActiveIndex = this.closest(opts.url), a = this.activeIndex;

                // save new page index, null check to prevent falsey 0 result
                // record the previous index for reference
                if (newActiveIndex !== undefined) {
                    this.activeIndex = newActiveIndex;
                    this.previousIndex = a;
                }

                // invoke callbacks where appropriate
                //
                // TODO this is also convoluted and confusing
                if (newActiveIndex < a) {
                    (opts.present || opts.back || $.noop)(this.getActive(), "back");
                } else if (newActiveIndex > a) {
                    (opts.present || opts.forward || $.noop)(this.getActive(), "forward");
                } else if (newActiveIndex === undefined && opts.missing) {
                    opts.missing(this.getActive());
                }
            }
        });
    })(jQuery);



    (function ($) {
        // TODO move loader class down into the widget settings
        var loaderClass = "ui-loader", $html = $("html");

        $.widget("mobile.loader", {
            // NOTE if the global config settings are defined they will override these
            //      options
            options: {
                // the theme for the loading message
                theme: "a",

                // whether the text in the loading message is shown
                textVisible: false,

                // custom html for the inner content of the loading message
                html: "",

                // the text to be displayed when the popup is shown
                text: "loading"
            },

            defaultHtml: "<div class='" + loaderClass + "'>" +
                "<span class='ui-icon-loading'></span>" +
                "<h1></h1>" +
                "</div>",

            // For non-fixed supportin browsers. Position at y center (if scrollTop supported), above the activeBtn (if defined), or just 100px from top
            fakeFixLoader: function () {
                var activeBtn = $("." + $.mobile.activeBtnClass).first();

                this.element
                    .css({
                        top: $.support.scrollTop && this.window.scrollTop() + this.window.height() / 2 ||
                            activeBtn.length && activeBtn.offset().top || 100
                    });
            },

            // check position of loader to see if it appears to be "fixed" to center
            // if not, use abs positioning
            checkLoaderPosition: function () {
                var offset = this.element.offset(),
                    scrollTop = this.window.scrollTop(),
                    screenHeight = $.mobile.getScreenHeight();

                if (offset.top < scrollTop || (offset.top - scrollTop) > screenHeight) {
                    this.element.addClass("ui-loader-fakefix");
                    this.fakeFixLoader();
                    this.window
                        .unbind("scroll", this.checkLoaderPosition)
                        .bind("scroll", $.proxy(this.fakeFixLoader, this));
                }
            },

            resetHtml: function () {
                this.element.html($(this.defaultHtml).html());
            },

            // Turn on/off page loading message. Theme doubles as an object argument
            // with the following shape: { theme: '', text: '', html: '', textVisible: '' }
            // NOTE that the $.mobile.loading* settings and params past the first are deprecated
            // TODO sweet jesus we need to break some of this out
            show: function (theme, msgText, textonly) {
                var textVisible, message, loadSettings;

                this.resetHtml();

                // use the prototype options so that people can set them globally at
                // mobile init. Consistency, it's what's for dinner
                if ($.type(theme) === "object") {
                    loadSettings = $.extend({}, this.options, theme);

                    theme = loadSettings.theme;
                } else {
                    loadSettings = this.options;

                    // here we prefer the theme value passed as a string argument, then
                    // we prefer the global option because we can't use undefined default
                    // prototype options, then the prototype option
                    theme = theme || loadSettings.theme;
                }

                // set the message text, prefer the param, then the settings object
                // then loading message
                message = msgText || (loadSettings.text === false ? "" : loadSettings.text);

                // prepare the dom
                $html.addClass("ui-loading");

                textVisible = loadSettings.textVisible;

                // add the proper css given the options (theme, text, etc)
                // Force text visibility if the second argument was supplied, or
                // if the text was explicitly set in the object args
                this.element.attr("class", loaderClass +
                    " ui-corner-all ui-body-" + theme +
                    " ui-loader-" + (textVisible || msgText || theme.text ? "verbose" : "default") +
                    (loadSettings.textonly || textonly ? " ui-loader-textonly" : ""));

                // TODO verify that jquery.fn.html is ok to use in both cases here
                //      this might be overly defensive in preventing unknowing xss
                // if the html attribute is defined on the loading settings, use that
                // otherwise use the fallbacks from above
                if (loadSettings.html) {
                    this.element.html(loadSettings.html);
                } else {
                    this.element.find("h1").text(message);
                }

                // If the pagecontainer widget has been defined we may use the :mobile-pagecontainer
                // and attach to the element on which the pagecontainer widget has been defined. If not,
                // we attach to the body.
                this.element.appendTo($.mobile.pagecontainer ?
                    $(":mobile-pagecontainer") : $("body"));

                // check that the loader is visible
                this.checkLoaderPosition();

                // on scroll check the loader position
                this.window.bind("scroll", $.proxy(this.checkLoaderPosition, this));
            },

            hide: function () {
                $html.removeClass("ui-loading");

                if (this.options.text) {
                    this.element.removeClass("ui-loader-fakefix");
                }

                this.window.unbind("scroll", this.fakeFixLoader);
                this.window.unbind("scroll", this.checkLoaderPosition);
            }
        });

    })(jQuery, this);


    (function ($, undefined) {
        $.mobile.widgets = {};

        var originalWidget = $.widget,

            // Record the original, non-mobileinit-modified version of $.mobile.keepNative
            // so we can later determine whether someone has modified $.mobile.keepNative
            keepNativeFactoryDefault = $.mobile.keepNative;

        $.widget = (function (orig) {
            return function () {
                var constructor = orig.apply(this, arguments),
                    name = constructor.prototype.widgetName;

                constructor.initSelector = ((constructor.prototype.initSelector !== undefined) ?
                    constructor.prototype.initSelector : ":jqmData(role='" + name + "')");

                $.mobile.widgets[name] = constructor;

                return constructor;
            };
        })($.widget);

        // Make sure $.widget still has bridge and extend methods
        $.extend($.widget, originalWidget);

        // For backcompat remove in 1.5
        $.mobile.document.on("create", function (event) {
            $(event.target).enhanceWithin();
        });

        $.widget("mobile.page", {
            options: {
                theme: "a",
                domCache: false,

                // Deprecated in 1.4 remove in 1.5
                keepNativeDefault: $.mobile.keepNative,

                // Deprecated in 1.4 remove in 1.5
                contentTheme: null,
                enhanced: false
            },

            // DEPRECATED for > 1.4
            // TODO remove at 1.5
            _createWidget: function () {
                $.Widget.prototype._createWidget.apply(this, arguments);
                this._trigger("init");
            },

            _create: function () {
                // If false is returned by the callbacks do not create the page
                if (this._trigger("beforecreate") === false) {
                    return false;
                }

                if (!this.options.enhanced) {
                    this._enhance();
                }

                this._on(this.element, {
                    pagebeforehide: "removeContainerBackground",
                    pagebeforeshow: "_handlePageBeforeShow"
                });

                this.element.enhanceWithin();
                // Dialog widget is deprecated in 1.4 remove this in 1.5
                if ($.mobile.getAttribute(this.element[0], "role") === "dialog1" && $.mobile.dialog1) {
                    this.element.dialog();
                }
            },

            _enhance: function () {
                var attrPrefix = "data-" + $.mobile.ns,
                    self = this;

                if (this.options.role) {
                    this.element.attr("data-" + $.mobile.ns + "role", this.options.role);
                }

                this.element
                    .attr("tabindex", "0")
                    .addClass("ui-page ui-page-theme-" + this.options.theme);

                // Manipulation of content os Deprecated as of 1.4 remove in 1.5
                this.element.find("[" + attrPrefix + "role='content']").each(function () {
                    var $this = $(this),
                        theme = this.getAttribute(attrPrefix + "theme") || undefined;
                    self.options.contentTheme = theme || self.options.contentTheme || (self.options.dialog && self.options.theme) || (self.element.jqmData("role") === "dialog" && self.options.theme);
                    $this.addClass("ui-content");
                    if (self.options.contentTheme) {
                        $this.addClass("ui-body-" + (self.options.contentTheme));
                    }
                    // Add ARIA role
                    $this.attr("role", "main").addClass("ui-content");
                });
            },

            bindRemove: function (callback) {
                var page = this.element;

                // when dom caching is not enabled or the page is embedded bind to remove the page on hide
                if (!page.data("mobile-page").options.domCache &&
                    page.is(":jqmData(external-page='true')")) {

                    // TODO use _on - that is, sort out why it doesn't work in this case
                    page.bind("pagehide.remove", callback || function (e, data) {

                        //check if this is a same page transition and if so don't remove the page
                        if (!data.samePage) {
                            var $this = $(this),
                                prEvent = new $.Event("pageremove");

                            $this.trigger(prEvent);

                            if (!prEvent.isDefaultPrevented()) {
                                $this.removeWithDependents();
                            }
                        }
                    });
                }
            },

            _setOptions: function (o) {
                if (o.theme !== undefined) {
                    this.element.removeClass("ui-page-theme-" + this.options.theme).addClass("ui-page-theme-" + o.theme);
                }

                if (o.contentTheme !== undefined) {
                    this.element.find("[data-" + $.mobile.ns + "='content']").removeClass("ui-body-" + this.options.contentTheme)
                        .addClass("ui-body-" + o.contentTheme);
                }
            },

            _handlePageBeforeShow: function (/* e */) {
                this.setContainerBackground();
            },
            // Deprecated in 1.4 remove in 1.5
            removeContainerBackground: function () {
                this.element.closest(":mobile-pagecontainer").pagecontainer({ "theme": "none" });
            },
            // Deprecated in 1.4 remove in 1.5
            // set the page container background to the page theme
            setContainerBackground: function (theme) {
                this.element.parent().pagecontainer({ "theme": theme || this.options.theme });
            },
            // Deprecated in 1.4 remove in 1.5
            keepNativeSelector: function () {
                var options = this.options,
                    keepNative = $.trim(options.keepNative || ""),
                    globalValue = $.trim($.mobile.keepNative),
                    optionValue = $.trim(options.keepNativeDefault),

                    // Check if $.mobile.keepNative has changed from the factory default
                    newDefault = (keepNativeFactoryDefault === globalValue ?
                        "" : globalValue),

                    // If $.mobile.keepNative has not changed, use options.keepNativeDefault
                    oldDefault = (newDefault === "" ? optionValue : "");

                // Concatenate keepNative selectors from all sources where the value has
                // changed or, if nothing has changed, return the default
                return ((keepNative ? [keepNative] : [])
                    .concat(newDefault ? [newDefault] : [])
                    .concat(oldDefault ? [oldDefault] : [])
                    .join(", "));
            }
        });
    })(jQuery);


}));

jQuery.cookie=function(n,t,i){var f,r,e,o,u,s;if(typeof t!="undefined"){i=i||{};t===null&&(t="",i.expires=-1);f="";i.expires&&(typeof i.expires=="number"||i.expires.toUTCString)&&(typeof i.expires=="number"?(r=new Date,r.setTime(r.getTime()+i.expires*864e5)):r=i.expires,f="; expires="+r.toUTCString());var h=i.path?"; path="+i.path:"",c=i.domain?"; domain="+i.domain:"",l=i.secure?"; secure":"";document.cookie=[n,"=",encodeURIComponent(t),f,h,c,l].join("")}else{if(e=null,document.cookie&&document.cookie!="")for(o=document.cookie.split(";"),u=0;u<o.length;u++)if(s=jQuery.trim(o[u]),s.substring(0,n.length+1)==n+"="){e=decodeURIComponent(s.substring(n.length+1));break}return e}};


/*!
 * jQuery Templates Plugin 1.0.0pre
 * http://github.com/jquery/jquery-tmpl
 * Requires jQuery 1.4.2
 *
 * Copyright Software Freedom Conservancy, Inc.
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 */
(function( jQuery, undefined ){
	var oldManip = jQuery.fn.domManip, tmplItmAtt = "_tmplitem", htmlExpr = /^[^<]*(<[\w\W]+>)[^>]*$|\{\{\! /,
		newTmplItems = {}, wrappedItems = {}, appendToTmplItems, topTmplItem = { key: 0, data: {} }, itemKey = 0, cloneIndex = 0, stack = [];

	function newTmplItem( options, parentItem, fn, data ) {
		// Returns a template item data structure for a new rendered instance of a template (a 'template item').
		// The content field is a hierarchical array of strings and nested items (to be
		// removed and replaced by nodes field of dom elements, once inserted in DOM).
		var newItem = {
			data: data || (data === 0 || data === false) ? data : (parentItem ? parentItem.data : {}),
			_wrap: parentItem ? parentItem._wrap : null,
			tmpl: null,
			parent: parentItem || null,
			nodes: [],
			calls: tiCalls,
			nest: tiNest,
			wrap: tiWrap,
			html: tiHtml,
			update: tiUpdate
		};
		if ( options ) {
			jQuery.extend( newItem, options, { nodes: [], parent: parentItem });
		}
		if ( fn ) {
			// Build the hierarchical content to be used during insertion into DOM
			newItem.tmpl = fn;
			newItem._ctnt = newItem._ctnt || newItem.tmpl( jQuery, newItem );
			newItem.key = ++itemKey;
			// Keep track of new template item, until it is stored as jQuery Data on DOM element
			(stack.length ? wrappedItems : newTmplItems)[itemKey] = newItem;
		}
		return newItem;
	}

	// Override appendTo etc., in order to provide support for targeting multiple elements. (This code would disappear if integrated in jquery core).
	jQuery.each({
		appendTo: "append",
		prependTo: "prepend",
		insertBefore: "before",
		insertAfter: "after",
		replaceAll: "replaceWith"
	}, function( name, original ) {
		jQuery.fn[ name ] = function( selector ) {
			var ret = [], insert = jQuery( selector ), elems, i, l, tmplItems,
				parent = this.length === 1 && this[0].parentNode;

			appendToTmplItems = newTmplItems || {};
			if ( parent && parent.nodeType === 11 && parent.childNodes.length === 1 && insert.length === 1 ) {
				insert[ original ]( this[0] );
				ret = this;
			} else {
				for ( i = 0, l = insert.length; i < l; i++ ) {
					cloneIndex = i;
					elems = (i > 0 ? this.clone(true) : this).get();
					jQuery( insert[i] )[ original ]( elems );
					ret = ret.concat( elems );
				}
				cloneIndex = 0;
				ret = this.pushStack( ret, name, insert.selector );
			}
			tmplItems = appendToTmplItems;
			appendToTmplItems = null;
			jQuery.tmpl.complete(tmplItems);
			
			return ret;
		};
	});

	jQuery.fn.extend({
		// Use first wrapped element as template markup.
		// Return wrapped set of template items, obtained by rendering template against data.
		tmpl: function( data, options, parentItem ) {
			return jQuery.tmpl( this[0], data, options, parentItem );
		},

		// Find which rendered template item the first wrapped DOM element belongs to
		tmplItem: function() {
			return jQuery.tmplItem( this[0] );
		},

		// Consider the first wrapped element as a template declaration, and get the compiled template or store it as a named template.
		template: function( name ) {
			return jQuery.template( name, this[0] );
		},

		domManip: function( args, table, callback, options ) {
			if ( args[0] && jQuery.isArray( args[0] )) {
				var dmArgs = jQuery.makeArray( arguments ), elems = args[0], elemsLength = elems.length, i = 0, tmplItem;
				while ( i < elemsLength && !(tmplItem = jQuery.data( elems[i++], "tmplItem" ))) {}
				if ( tmplItem && cloneIndex ) {
					dmArgs[2] = function( fragClone ) {
						// Handler called by oldManip when rendered template has been inserted into DOM.
						jQuery.tmpl.afterManip( this, fragClone, callback );
					};
				}
				oldManip.apply( this, dmArgs );
			} else {
				oldManip.apply( this, arguments );
			}
			cloneIndex = 0;
			if ( !appendToTmplItems ) {
				jQuery.tmpl.complete( newTmplItems );
			}
			return this;
		}
	});

	jQuery.extend({
		// Return wrapped set of template items, obtained by rendering template against data.
	    tmpl: function (tmpl, data, options, parentItem) {
			var ret, topLevel = !parentItem;
			if ( topLevel ) {
				// This is a top-level tmpl call (not from a nested template using {{tmpl}})
				parentItem = topTmplItem;
				tmpl = jQuery.template[tmpl] || jQuery.template( null, tmpl );
				wrappedItems = {}; // Any wrapped items will be rebuilt, since this is top level
			} else if ( !tmpl ) {
				// The template item is already associated with DOM - this is a refresh.
				// Re-evaluate rendered template for the parentItem
				tmpl = parentItem.tmpl;
				newTmplItems[parentItem.key] = parentItem;
				parentItem.nodes = [];
				if ( parentItem.wrapped ) {
					updateWrapped( parentItem, parentItem.wrapped );
				}
				// Rebuild, without creating a new template item
				return jQuery( build( parentItem, null, parentItem.tmpl( jQuery, parentItem ) ));
			}
			if ( !tmpl ) {
				return []; // Could throw...
			}
			if ( typeof data === "function" ) {
				data = data.call( parentItem || {} );
			}
			if ( options && options.wrapped ) {
				updateWrapped( options, options.wrapped );
			}
			ret = jQuery.isArray( data ) ?
				jQuery.map( data, function( dataItem ) {
					return dataItem ? newTmplItem( options, parentItem, tmpl, dataItem ) : null;
				}) :
				[newTmplItem(options, parentItem, tmpl, data)];
			var html = jQuery(build(parentItem, null, ret));
			if (typeof $.fn.bindResources === "function") {
			    html = html.bindResources();
			}
			return topLevel ? html : ret;
		},

		// Return rendered template item for an element.
		tmplItem: function( elem ) {
			var tmplItem;
			if ( elem instanceof jQuery ) {
				elem = elem[0];
			}
			while ( elem && elem.nodeType === 1 && !(tmplItem = jQuery.data( elem, "tmplItem" )) && (elem = elem.parentNode) ) {}
			return tmplItem || topTmplItem;
		},

		// Set:
		// Use $.template( name, tmpl ) to cache a named template,
		// where tmpl is a template string, a script element or a jQuery instance wrapping a script element, etc.
		// Use $( "selector" ).template( name ) to provide access by name to a script block template declaration.

		// Get:
		// Use $.template( name ) to access a cached template.
		// Also $( selectorToScriptBlock ).template(), or $.template( null, templateString )
		// will return the compiled template, without adding a name reference.
		// If templateString includes at least one HTML tag, $.template( templateString ) is equivalent
		// to $.template( null, templateString )
		template: function( name, tmpl ) {
			if (tmpl) {
				// Compile template and associate with name
				if ( typeof tmpl === "string" ) {
					// This is an HTML string being passed directly in.
					tmpl = buildTmplFn( tmpl );
				} else if ( tmpl instanceof jQuery ) {
					tmpl = tmpl[0] || {};
				}
				if ( tmpl.nodeType ) {
					// If this is a template block, use cached copy, or generate tmpl function and cache.
					tmpl = jQuery.data( tmpl, "tmpl" ) || jQuery.data( tmpl, "tmpl", buildTmplFn( tmpl.innerHTML ));
					// Issue: In IE, if the container element is not a script block, the innerHTML will remove quotes from attribute values whenever the value does not include white space.
					// This means that foo="${x}" will not work if the value of x includes white space: foo="${x}" -> foo=value of x.
					// To correct this, include space in tag: foo="${ x }" -> foo="value of x"
				}
				return typeof name === "string" ? (jQuery.template[name] = tmpl) : tmpl;
			}
			// Return named compiled template
			return name ? (typeof name !== "string" ? jQuery.template( null, name ):
				(jQuery.template[name] ||
					// If not in map, and not containing at least on HTML tag, treat as a selector.
					// (If integrated with core, use quickExpr.exec)
					jQuery.template( null, htmlExpr.test( name ) ? name : jQuery( name )))) : null;
		},

		encode: function( text ) {
			// Do HTML encoding replacing < > & and ' and " by corresponding entities.
			return ("" + text).split("<").join("&lt;").split(">").join("&gt;").split('"').join("&#34;").split("'").join("&#39;");
		}
	});

	jQuery.extend( jQuery.tmpl, {
		tag: {
			"tmpl": {
				_default: { $2: "null" },
				open: "if($notnull_1){__=__.concat($item.nest($1,$2));}"
				// tmpl target parameter can be of type function, so use $1, not $1a (so not auto detection of functions)
				// This means that {{tmpl foo}} treats foo as a template (which IS a function).
				// Explicit parens can be used if foo is a function that returns a template: {{tmpl foo()}}.
			},
			"wrap": {
				_default: { $2: "null" },
				open: "$item.calls(__,$1,$2);__=[];",
				close: "call=$item.calls();__=call._.concat($item.wrap(call,__));"
			},
			"each": {
				_default: { $2: "$index, $value" },
				open: "if($notnull_1){$.each($1a,function($2){with(this){",
				close: "}});}"
			},
			"if": {
				open: "if(($notnull_1) && $1a){",
				close: "}"
			},
			"else": {
				_default: { $1: "true" },
				open: "}else if(($notnull_1) && $1a){"
			},
			"html": {
				// Unecoded expression evaluation.
				open: "if($notnull_1){__.push($1a);}"
			},
			"=": {
				// Encoded expression evaluation. Abbreviated form is ${}.
				_default: { $1: "$data" },
				open: "if($notnull_1){__.push($.encode($1a));}"
			},
			"!": {
				// Comment tag. Skipped by parser
				open: ""
			}
		},

		// This stub can be overridden, e.g. in jquery.tmplPlus for providing rendered events
		complete: function( items ) {
			newTmplItems = {};
		},

		// Call this from code which overrides domManip, or equivalent
		// Manage cloning/storing template items etc.
		afterManip: function afterManip( elem, fragClone, callback ) {
			// Provides cloned fragment ready for fixup prior to and after insertion into DOM
			var content = fragClone.nodeType === 11 ?
				jQuery.makeArray(fragClone.childNodes) :
				fragClone.nodeType === 1 ? [fragClone] : [];

			// Return fragment to original caller (e.g. append) for DOM insertion
			callback.call( elem, fragClone );

			// Fragment has been inserted:- Add inserted nodes to tmplItem data structure. Replace inserted element annotations by jQuery.data.
			storeTmplItems( content );
			cloneIndex++;
		}
	});

	//========================== Private helper functions, used by code above ==========================

	function build( tmplItem, nested, content ) {
		// Convert hierarchical content into flat string array
		// and finally return array of fragments ready for DOM insertion
		var frag, ret = content ? jQuery.map( content, function( item ) {
			return (typeof item === "string") ?
				// Insert template item annotations, to be converted to jQuery.data( "tmplItem" ) when elems are inserted into DOM.
				(tmplItem.key ? item.replace( /(<\w+)(?=[\s>])(?![^>]*_tmplitem)([^>]*)/g, "$1 " + tmplItmAtt + "=\"" + tmplItem.key + "\" $2" ) : item) :
				// This is a child template item. Build nested template.
				build( item, tmplItem, item._ctnt );
		}) :
		// If content is not defined, insert tmplItem directly. Not a template item. May be a string, or a string array, e.g. from {{html $item.html()}}.
		tmplItem;
		if ( nested ) {
			return ret;
		}

		// top-level template
		ret = ret.join("");

		// Support templates which have initial or final text nodes, or consist only of text
		// Also support HTML entities within the HTML markup.
		ret.replace( /^\s*([^<\s][^<]*)?(<[\w\W]+>)([^>]*[^>\s])?\s*$/, function( all, before, middle, after) {
			frag = jQuery( middle ).get();

			storeTmplItems( frag );
			if ( before ) {
				frag = unencode( before ).concat(frag);
			}
			if ( after ) {
				frag = frag.concat(unencode( after ));
			}
		});
		
		return frag ? frag : unencode( ret );
	}

	function unencode( text ) {
		// Use createElement, since createTextNode will not render HTML entities correctly
		var el = document.createElement( "div" );
		el.innerHTML = text;
		return jQuery.makeArray(el.childNodes);
	}

	// Generate a reusable function that will serve to render a template against data
	function buildTmplFn( markup ) {
		return new Function("jQuery","$item",
			// Use the variable __ to hold a string array while building the compiled template. (See https://github.com/jquery/jquery-tmpl/issues#issue/10).
			"var $=jQuery,call,__=[],$data=$item.data;" +

			// Introduce the data as local variables using with(){}
			"with($data){__.push('" +

			// Convert the template into pure JavaScript
			jQuery.trim(markup)
				.replace( /([\\'])/g, "\\$1" )
				.replace( /[\r\t\n]/g, " " )
				.replace( /\$\{([^\}]*)\}/g, "{{= $1}}" )
				.replace( /\{\{(\/?)(\w+|.)(?:\(((?:[^\}]|\}(?!\}))*?)?\))?(?:\s+(.*?)?)?(\(((?:[^\}]|\}(?!\}))*?)\))?\s*\}\}/g,
				function( all, slash, type, fnargs, target, parens, args ) {
					var tag = jQuery.tmpl.tag[ type ], def, expr, exprAutoFnDetect;
					if ( !tag ) {
						throw "Unknown template tag: " + type;
					}
					def = tag._default || [];
					if ( parens && !/\w$/.test(target)) {
						target += parens;
						parens = "";
					}
					if ( target ) {
						target = unescape( target );
						args = args ? ("," + unescape( args ) + ")") : (parens ? ")" : "");
						// Support for target being things like a.toLowerCase();
						// In that case don't call with template item as 'this' pointer. Just evaluate...
						expr = parens ? (target.indexOf(".") > -1 ? target + unescape( parens ) : ("(" + target + ").call($item" + args)) : target;
						exprAutoFnDetect = parens ? expr : "(typeof(" + target + ")==='function'?(" + target + ").call($item):(" + target + "))";
					} else {
						exprAutoFnDetect = expr = def.$1 || "null";
					}
					fnargs = unescape( fnargs );
					return "');" +
						tag[ slash ? "close" : "open" ]
							.split( "$notnull_1" ).join( target ? "typeof(" + target + ")!=='undefined' && (" + target + ")!=null" : "true" )
							.split( "$1a" ).join( exprAutoFnDetect )
							.split( "$1" ).join( expr )
							.split( "$2" ).join( fnargs || def.$2 || "" ) +
						"__.push('";
				}) +
			"');}return __;"
		);
	}
	function updateWrapped( options, wrapped ) {
		// Build the wrapped content.
		options._wrap = build( options, true,
			// Suport imperative scenario in which options.wrapped can be set to a selector or an HTML string.
			jQuery.isArray( wrapped ) ? wrapped : [htmlExpr.test( wrapped ) ? wrapped : jQuery( wrapped ).html()]
		).join("");
	}

	function unescape( args ) {
		return args ? args.replace( /\\'/g, "'").replace(/\\\\/g, "\\" ) : null;
	}
	function outerHtml( elem ) {
		var div = document.createElement("div");
		div.appendChild( elem.cloneNode(true) );
		return div.innerHTML;
	}

	// Store template items in jQuery.data(), ensuring a unique tmplItem data data structure for each rendered template instance.
	function storeTmplItems( content ) {
		var keySuffix = "_" + cloneIndex, elem, elems, newClonedItems = {}, i, l, m;
		for ( i = 0, l = content.length; i < l; i++ ) {
			if ( (elem = content[i]).nodeType !== 1 ) {
				continue;
			}
			elems = elem.getElementsByTagName("*");
			for ( m = elems.length - 1; m >= 0; m-- ) {
				processItemKey( elems[m] );
			}
			processItemKey( elem );
		}
		function processItemKey( el ) {
			var pntKey, pntNode = el, pntItem, tmplItem, key;
			// Ensure that each rendered template inserted into the DOM has its own template item,
			if ( (key = el.getAttribute( tmplItmAtt ))) {
				while ( pntNode.parentNode && (pntNode = pntNode.parentNode).nodeType === 1 && !(pntKey = pntNode.getAttribute( tmplItmAtt ))) { }
				if ( pntKey !== key ) {
					// The next ancestor with a _tmplitem expando is on a different key than this one.
					// So this is a top-level element within this template item
					// Set pntNode to the key of the parentNode, or to 0 if pntNode.parentNode is null, or pntNode is a fragment.
					pntNode = pntNode.parentNode ? (pntNode.nodeType === 11 ? 0 : (pntNode.getAttribute( tmplItmAtt ) || 0)) : 0;
					if ( !(tmplItem = newTmplItems[key]) ) {
						// The item is for wrapped content, and was copied from the temporary parent wrappedItem.
						tmplItem = wrappedItems[key];
						tmplItem = newTmplItem( tmplItem, newTmplItems[pntNode]||wrappedItems[pntNode] );
						tmplItem.key = ++itemKey;
						newTmplItems[itemKey] = tmplItem;
					}
					if ( cloneIndex ) {
						cloneTmplItem( key );
					}
				}
				el.removeAttribute( tmplItmAtt );
			} else if ( cloneIndex && (tmplItem = jQuery.data( el, "tmplItem" )) ) {
				// This was a rendered element, cloned during append or appendTo etc.
				// TmplItem stored in jQuery data has already been cloned in cloneCopyEvent. We must replace it with a fresh cloned tmplItem.
				cloneTmplItem( tmplItem.key );
				newTmplItems[tmplItem.key] = tmplItem;
				pntNode = jQuery.data( el.parentNode, "tmplItem" );
				pntNode = pntNode ? pntNode.key : 0;
			}
			if ( tmplItem ) {
				pntItem = tmplItem;
				// Find the template item of the parent element.
				// (Using !=, not !==, since pntItem.key is number, and pntNode may be a string)
				while ( pntItem && pntItem.key != pntNode ) {
					// Add this element as a top-level node for this rendered template item, as well as for any
					// ancestor items between this item and the item of its parent element
					pntItem.nodes.push( el );
					pntItem = pntItem.parent;
				}
				// Delete content built during rendering - reduce API surface area and memory use, and avoid exposing of stale data after rendering...
				delete tmplItem._ctnt;
				delete tmplItem._wrap;
				// Store template item as jQuery data on the element
				jQuery.data( el, "tmplItem", tmplItem );
			}
			function cloneTmplItem( key ) {
				key = key + keySuffix;
				tmplItem = newClonedItems[key] =
					(newClonedItems[key] || newTmplItem( tmplItem, newTmplItems[tmplItem.parent.key + keySuffix] || tmplItem.parent ));
			}
		}
	}

	//---- Helper functions for template item ----

	function tiCalls( content, tmpl, data, options ) {
		if ( !content ) {
			return stack.pop();
		}
		stack.push({ _: content, tmpl: tmpl, item:this, data: data, options: options });
	}

	function tiNest( tmpl, data, options ) {
		// nested template, using {{tmpl}} tag
		return jQuery.tmpl( jQuery.template( tmpl ), data, options, this );
	}

	function tiWrap( call, wrapped ) {
		// nested template, using {{wrap}} tag
		var options = call.options || {};
		options.wrapped = wrapped;
		// Apply the template, which may incorporate wrapped content,
		return jQuery.tmpl( jQuery.template( call.tmpl ), call.data, options, call.item );
	}

	function tiHtml( filter, textOnly ) {
		var wrapped = this._wrap;
		return jQuery.map(
			jQuery( jQuery.isArray( wrapped ) ? wrapped.join("") : wrapped ).filter( filter || "*" ),
			function(e) {
				return textOnly ?
					e.innerText || e.textContent :
					e.outerHTML || outerHtml(e);
			});
	}

	function tiUpdate() {
		var coll = this.nodes;
		jQuery.tmpl( null, null, null, this).insertBefore( coll[0] );
		jQuery( coll ).remove();
	}
})( jQuery );

(function(n,t){function v(n,t,r){var e=n.children(),o=!1,u,s,f;for(n.empty(),u=0,s=e.length;u<s;u++)if(f=e.eq(u),n.append(f),r&&n.append(r),i(n,t)){f.remove();o=!0;break}else r&&r.detach();return o}function o(t,r,u,f,e){var s=!1,h="table, thead, tbody, tfoot, tr, col, colgroup, object, embed, param, ol, ul, dl, blockquote, select, optgroup, option, textarea, script, style",c="script";return t.contents().detach().each(function(){var a=this,l=n(a);if(typeof a=="undefined"||a.nodeType==3&&n.trim(a.data).length==0)return!0;if(l.is(c))t.append(l);else{if(s)return!0;t.append(l);e&&t[t.is(h)?"after":"append"](e);i(u,f)&&(s=a.nodeType==3?y(l,r,u,f,e):o(l,r,u,f,e),s||(l.detach(),s=!0));s||e&&e.detach()}}),s}function y(t,u,e,o,c){var l=t[0],nt,k,d;if(!l)return!1;var y=h(l),tt=y.indexOf(" ")!==-1?" ":"　",p=o.wrap=="letter"?"":tt,a=y.split(p),g=-1,w=-1,b=0,v=a.length-1;for(o.fallbackToLetter&&b==0&&v==0&&(p="",a=y.split(p),v=a.length-1);b<=v&&!(b==0&&v==0);){if(nt=Math.floor((b+v)/2),nt==w)break;w=nt;f(l,a.slice(0,w+1).join(p)+o.ellipsis);i(e,o)?(v=w,o.fallbackToLetter&&b==0&&v==0&&(p="",a=a[0].split(p),g=-1,w=-1,b=0,v=a.length-1)):(g=w,b=w)}return g==-1||a.length==1&&a[0].length==0?(k=t.parent(),t.detach(),d=c&&c.closest(k).length?c.length:0,k.contents().length>d?l=r(k.contents().eq(-1-d),u):(l=r(k,u,!0),d||k.detach()),l&&(y=s(h(l),o),f(l,y),d&&c&&n(l).parent().append(c))):(y=s(a.slice(0,g+1).join(p),o),f(l,y)),!0}function i(n,t){return n.innerHeight()>t.maxHeight}function s(t,i){while(n.inArray(t.slice(-1),i.lastCharacter.remove)>-1)t=t.slice(0,-1);return n.inArray(t.slice(-1),i.lastCharacter.noEllipsis)<0&&(t+=i.ellipsis),t}function u(n){return{width:n.innerWidth(),height:n.innerHeight()}}function f(n,t){n.innerText?n.innerText=t:n.nodeValue?n.nodeValue=t:n.textContent&&(n.textContent=t)}function h(n){return n.innerText?n.innerText:n.nodeValue?n.nodeValue:n.textContent?n.textContent:""}function c(n){do n=n.previousSibling;while(n&&n.nodeType!==1&&n.nodeType!==3);return n}function r(t,i,u){var e=t&&t[0],f;if(e){if(!u){if(e.nodeType===3)return e;if(n.trim(t.text()))return r(t.contents().last(),i)}for(f=c(e);!f;){if(t=t.parent(),t.is(i)||!t.length)return!1;f=c(t[0])}if(f)return r(n(f),i)}return!1}function p(t,i){return t?typeof t=="string"?(t=n(t,i),t.length?t:!1):t.jquery?t:!1:!1}function w(n){for(var t,r=n.innerHeight(),u=["paddingTop","paddingBottom"],i=0,f=u.length;i<f;i++)t=parseInt(n.css(u[i]),10),isNaN(t)&&(t=0),r-=t;return r}var e,l,a;n.fn.dotdotdot||(n.fn.dotdotdot=function(t){var r;if(this.length==0)return n.fn.dotdotdot.debug('No element found for "'+this.selector+'".'),this;if(this.length>1)return this.each(function(){n(this).dotdotdot(t)});r=this;r.data("dotdotdot")&&r.trigger("destroy.dot");r.data("dotdotdot-style",r.attr("style")||"");r.css("word-wrap","break-word");r.css("white-space")==="nowrap"&&r.css("white-space","normal");r.bind_events=function(){return r.bind("update.dot",function(t,u){t.preventDefault();t.stopPropagation();f.maxHeight=typeof f.height=="number"?f.height:w(r);f.maxHeight+=f.tolerance;typeof u!="undefined"&&((typeof u=="string"||u instanceof HTMLElement)&&(u=n("<div />").append(u).contents()),u instanceof n&&(c=u));h=r.wrapInner('<div class="dotdotdot" />').children();h.contents().detach().end().append(c.clone(!0)).find("br").replaceWith("  <br />  ").end().css({height:"auto",width:"auto",border:"none",padding:0,margin:0});var e=!1,l=!1;return s.afterElement&&(e=s.afterElement.clone(!0),e.show(),s.afterElement.detach()),i(h,f)&&(l=f.wrap=="children"?v(h,f,e):o(h,r,h,f,e)),h.replaceWith(h.contents()),h=null,n.isFunction(f.callback)&&f.callback.call(r[0],l,c),s.isTruncated=l,l}).bind("isTruncated.dot",function(n,t){return n.preventDefault(),n.stopPropagation(),typeof t=="function"&&t.call(r[0],s.isTruncated),s.isTruncated}).bind("originalContent.dot",function(n,t){return n.preventDefault(),n.stopPropagation(),typeof t=="function"&&t.call(r[0],c),c}).bind("destroy.dot",function(n){n.preventDefault();n.stopPropagation();r.unwatch().unbind_events().contents().detach().end().append(c).attr("style",r.data("dotdotdot-style")||"").data("dotdotdot",!1)}),r};r.unbind_events=function(){return r.unbind(".dot"),r};r.watch=function(){if(r.unwatch(),f.watch=="window"){var t=n(window),i=t.width(),e=t.height();t.bind("resize.dot"+s.dotId,function(){i==t.width()&&e==t.height()&&f.windowResizeFix||(i=t.width(),e=t.height(),l&&clearInterval(l),l=setTimeout(function(){r.trigger("update.dot")},10))})}else a=u(r),l=setInterval(function(){var n=u(r);(a.width!=n.width||a.height!=n.height)&&(r.trigger("update.dot"),a=u(r))},0);return r};r.unwatch=function(){return n(window).unbind("resize.dot"+s.dotId),l&&clearInterval(l),r};var c=r.contents(),f=n.extend(!0,{},n.fn.dotdotdot.defaults,t),s={},a={},l=null,h=null;return f.lastCharacter.remove instanceof Array||(f.lastCharacter.remove=n.fn.dotdotdot.defaultArrays.lastCharacter.remove),f.lastCharacter.noEllipsis instanceof Array||(f.lastCharacter.noEllipsis=n.fn.dotdotdot.defaultArrays.lastCharacter.noEllipsis),s.afterElement=p(f.after,r),s.isTruncated=!1,s.dotId=e++,r.data("dotdotdot",!0).bind_events().trigger("update.dot"),f.watch&&r.watch(),r},n.fn.dotdotdot.defaults={ellipsis:"... ",wrap:"word",fallbackToLetter:!0,lastCharacter:{},tolerance:0,callback:null,after:null,height:null,watch:!1,windowResizeFix:!0},n.fn.dotdotdot.defaultArrays={lastCharacter:{remove:[" ","　",",",";",".","!","?"],noEllipsis:[]}},n.fn.dotdotdot.debug=function(){},e=1,l=n.fn.html,n.fn.html=function(i){return i!=t&&!n.isFunction(i)&&this.data("dotdotdot")?this.trigger("update",[i]):l.apply(this,arguments)},a=n.fn.text,n.fn.text=function(i){return i!=t&&!n.isFunction(i)&&this.data("dotdotdot")?(i=n("<div />").text(i).html(),this.trigger("update",[i])):a.apply(this,arguments)})})(jQuery);
//# sourceMappingURL=jquery.dotdotdot.min.js.map

//     Underscore.js 1.8.3
//     http://underscorejs.org
//     (c) 2009-2015 Jeremy Ashkenas, DocumentCloud and Investigative Reporters & Editors
//     Underscore may be freely distributed under the MIT license.

(function() {

  // Baseline setup
  // --------------

  // Establish the root object, `window` in the browser, or `exports` on the server.
  var root = this;

  // Save the previous value of the `_` variable.
  var previousUnderscore = root._;

  // Save bytes in the minified (but not gzipped) version:
  var ArrayProto = Array.prototype, ObjProto = Object.prototype, FuncProto = Function.prototype;

  // Create quick reference variables for speed access to core prototypes.
  var
    push             = ArrayProto.push,
    slice            = ArrayProto.slice,
    toString         = ObjProto.toString,
    hasOwnProperty   = ObjProto.hasOwnProperty;

  // All **ECMAScript 5** native function implementations that we hope to use
  // are declared here.
  var
    nativeIsArray      = Array.isArray,
    nativeKeys         = Object.keys,
    nativeBind         = FuncProto.bind,
    nativeCreate       = Object.create;

  // Naked function reference for surrogate-prototype-swapping.
  var Ctor = function(){};

  // Create a safe reference to the Underscore object for use below.
  var _ = function(obj) {
    if (obj instanceof _) return obj;
    if (!(this instanceof _)) return new _(obj);
    this._wrapped = obj;
  };

  // Export the Underscore object for **Node.js**, with
  // backwards-compatibility for the old `require()` API. If we're in
  // the browser, add `_` as a global object.
  if (typeof exports !== 'undefined') {
    if (typeof module !== 'undefined' && module.exports) {
      exports = module.exports = _;
    }
    exports._ = _;
  } else {
    root._ = _;
  }

  // Current version.
  _.VERSION = '1.8.3';

  // Internal function that returns an efficient (for current engines) version
  // of the passed-in callback, to be repeatedly applied in other Underscore
  // functions.
  var optimizeCb = function(func, context, argCount) {
    if (context === void 0) return func;
    switch (argCount == null ? 3 : argCount) {
      case 1: return function(value) {
        return func.call(context, value);
      };
      case 2: return function(value, other) {
        return func.call(context, value, other);
      };
      case 3: return function(value, index, collection) {
        return func.call(context, value, index, collection);
      };
      case 4: return function(accumulator, value, index, collection) {
        return func.call(context, accumulator, value, index, collection);
      };
    }
    return function() {
      return func.apply(context, arguments);
    };
  };

  // A mostly-internal function to generate callbacks that can be applied
  // to each element in a collection, returning the desired result — either
  // identity, an arbitrary callback, a property matcher, or a property accessor.
  var cb = function(value, context, argCount) {
    if (value == null) return _.identity;
    if (_.isFunction(value)) return optimizeCb(value, context, argCount);
    if (_.isObject(value)) return _.matcher(value);
    return _.property(value);
  };
  _.iteratee = function(value, context) {
    return cb(value, context, Infinity);
  };

  // An internal function for creating assigner functions.
  var createAssigner = function(keysFunc, undefinedOnly) {
    return function(obj) {
      var length = arguments.length;
      if (length < 2 || obj == null) return obj;
      for (var index = 1; index < length; index++) {
        var source = arguments[index],
            keys = keysFunc(source),
            l = keys.length;
        for (var i = 0; i < l; i++) {
          var key = keys[i];
          if (!undefinedOnly || obj[key] === void 0) obj[key] = source[key];
        }
      }
      return obj;
    };
  };

  // An internal function for creating a new object that inherits from another.
  var baseCreate = function(prototype) {
    if (!_.isObject(prototype)) return {};
    if (nativeCreate) return nativeCreate(prototype);
    Ctor.prototype = prototype;
    var result = new Ctor;
    Ctor.prototype = null;
    return result;
  };

  var property = function(key) {
    return function(obj) {
      return obj == null ? void 0 : obj[key];
    };
  };

  // Helper for collection methods to determine whether a collection
  // should be iterated as an array or as an object
  // Related: http://people.mozilla.org/~jorendorff/es6-draft.html#sec-tolength
  // Avoids a very nasty iOS 8 JIT bug on ARM-64. #2094
  var MAX_ARRAY_INDEX = Math.pow(2, 53) - 1;
  var getLength = property('length');
  var isArrayLike = function(collection) {
    var length = getLength(collection);
    return typeof length == 'number' && length >= 0 && length <= MAX_ARRAY_INDEX;
  };

  // Collection Functions
  // --------------------

  // The cornerstone, an `each` implementation, aka `forEach`.
  // Handles raw objects in addition to array-likes. Treats all
  // sparse array-likes as if they were dense.
  _.each = _.forEach = function(obj, iteratee, context) {
    iteratee = optimizeCb(iteratee, context);
    var i, length;
    if (isArrayLike(obj)) {
      for (i = 0, length = obj.length; i < length; i++) {
        iteratee(obj[i], i, obj);
      }
    } else {
      var keys = _.keys(obj);
      for (i = 0, length = keys.length; i < length; i++) {
        iteratee(obj[keys[i]], keys[i], obj);
      }
    }
    return obj;
  };

  // Return the results of applying the iteratee to each element.
  _.map = _.collect = function(obj, iteratee, context) {
    iteratee = cb(iteratee, context);
    var keys = !isArrayLike(obj) && _.keys(obj),
        length = (keys || obj).length,
        results = Array(length);
    for (var index = 0; index < length; index++) {
      var currentKey = keys ? keys[index] : index;
      results[index] = iteratee(obj[currentKey], currentKey, obj);
    }
    return results;
  };

  // Create a reducing function iterating left or right.
  function createReduce(dir) {
    // Optimized iterator function as using arguments.length
    // in the main function will deoptimize the, see #1991.
    function iterator(obj, iteratee, memo, keys, index, length) {
      for (; index >= 0 && index < length; index += dir) {
        var currentKey = keys ? keys[index] : index;
        memo = iteratee(memo, obj[currentKey], currentKey, obj);
      }
      return memo;
    }

    return function(obj, iteratee, memo, context) {
      iteratee = optimizeCb(iteratee, context, 4);
      var keys = !isArrayLike(obj) && _.keys(obj),
          length = (keys || obj).length,
          index = dir > 0 ? 0 : length - 1;
      // Determine the initial value if none is provided.
      if (arguments.length < 3) {
        memo = obj[keys ? keys[index] : index];
        index += dir;
      }
      return iterator(obj, iteratee, memo, keys, index, length);
    };
  }

  // **Reduce** builds up a single result from a list of values, aka `inject`,
  // or `foldl`.
  _.reduce = _.foldl = _.inject = createReduce(1);

  // The right-associative version of reduce, also known as `foldr`.
  _.reduceRight = _.foldr = createReduce(-1);

  // Return the first value which passes a truth test. Aliased as `detect`.
  _.find = _.detect = function(obj, predicate, context) {
    var key;
    if (isArrayLike(obj)) {
      key = _.findIndex(obj, predicate, context);
    } else {
      key = _.findKey(obj, predicate, context);
    }
    if (key !== void 0 && key !== -1) return obj[key];
  };

  // Return all the elements that pass a truth test.
  // Aliased as `select`.
  _.filter = _.select = function(obj, predicate, context) {
    var results = [];
    predicate = cb(predicate, context);
    _.each(obj, function(value, index, list) {
      if (predicate(value, index, list)) results.push(value);
    });
    return results;
  };

  // Return all the elements for which a truth test fails.
  _.reject = function(obj, predicate, context) {
    return _.filter(obj, _.negate(cb(predicate)), context);
  };

  // Determine whether all of the elements match a truth test.
  // Aliased as `all`.
  _.every = _.all = function(obj, predicate, context) {
    predicate = cb(predicate, context);
    var keys = !isArrayLike(obj) && _.keys(obj),
        length = (keys || obj).length;
    for (var index = 0; index < length; index++) {
      var currentKey = keys ? keys[index] : index;
      if (!predicate(obj[currentKey], currentKey, obj)) return false;
    }
    return true;
  };

  // Determine if at least one element in the object matches a truth test.
  // Aliased as `any`.
  _.some = _.any = function(obj, predicate, context) {
    predicate = cb(predicate, context);
    var keys = !isArrayLike(obj) && _.keys(obj),
        length = (keys || obj).length;
    for (var index = 0; index < length; index++) {
      var currentKey = keys ? keys[index] : index;
      if (predicate(obj[currentKey], currentKey, obj)) return true;
    }
    return false;
  };

  // Determine if the array or object contains a given item (using `===`).
  // Aliased as `includes` and `include`.
  _.contains = _.includes = _.include = function(obj, item, fromIndex, guard) {
    if (!isArrayLike(obj)) obj = _.values(obj);
    if (typeof fromIndex != 'number' || guard) fromIndex = 0;
    return _.indexOf(obj, item, fromIndex) >= 0;
  };

  // Invoke a method (with arguments) on every item in a collection.
  _.invoke = function(obj, method) {
    var args = slice.call(arguments, 2);
    var isFunc = _.isFunction(method);
    return _.map(obj, function(value) {
      var func = isFunc ? method : value[method];
      return func == null ? func : func.apply(value, args);
    });
  };

  // Convenience version of a common use case of `map`: fetching a property.
  _.pluck = function(obj, key) {
    return _.map(obj, _.property(key));
  };

  // Convenience version of a common use case of `filter`: selecting only objects
  // containing specific `key:value` pairs.
  _.where = function(obj, attrs) {
    return _.filter(obj, _.matcher(attrs));
  };

  // Convenience version of a common use case of `find`: getting the first object
  // containing specific `key:value` pairs.
  _.findWhere = function(obj, attrs) {
    return _.find(obj, _.matcher(attrs));
  };

  // Return the maximum element (or element-based computation).
  _.max = function(obj, iteratee, context) {
    var result = -Infinity, lastComputed = -Infinity,
        value, computed;
    if (iteratee == null && obj != null) {
      obj = isArrayLike(obj) ? obj : _.values(obj);
      for (var i = 0, length = obj.length; i < length; i++) {
        value = obj[i];
        if (value > result) {
          result = value;
        }
      }
    } else {
      iteratee = cb(iteratee, context);
      _.each(obj, function(value, index, list) {
        computed = iteratee(value, index, list);
        if (computed > lastComputed || computed === -Infinity && result === -Infinity) {
          result = value;
          lastComputed = computed;
        }
      });
    }
    return result;
  };

  // Return the minimum element (or element-based computation).
  _.min = function(obj, iteratee, context) {
    var result = Infinity, lastComputed = Infinity,
        value, computed;
    if (iteratee == null && obj != null) {
      obj = isArrayLike(obj) ? obj : _.values(obj);
      for (var i = 0, length = obj.length; i < length; i++) {
        value = obj[i];
        if (value < result) {
          result = value;
        }
      }
    } else {
      iteratee = cb(iteratee, context);
      _.each(obj, function(value, index, list) {
        computed = iteratee(value, index, list);
        if (computed < lastComputed || computed === Infinity && result === Infinity) {
          result = value;
          lastComputed = computed;
        }
      });
    }
    return result;
  };

  // Shuffle a collection, using the modern version of the
  // [Fisher-Yates shuffle](http://en.wikipedia.org/wiki/Fisher–Yates_shuffle).
  _.shuffle = function(obj) {
    var set = isArrayLike(obj) ? obj : _.values(obj);
    var length = set.length;
    var shuffled = Array(length);
    for (var index = 0, rand; index < length; index++) {
      rand = _.random(0, index);
      if (rand !== index) shuffled[index] = shuffled[rand];
      shuffled[rand] = set[index];
    }
    return shuffled;
  };

  // Sample **n** random values from a collection.
  // If **n** is not specified, returns a single random element.
  // The internal `guard` argument allows it to work with `map`.
  _.sample = function(obj, n, guard) {
    if (n == null || guard) {
      if (!isArrayLike(obj)) obj = _.values(obj);
      return obj[_.random(obj.length - 1)];
    }
    return _.shuffle(obj).slice(0, Math.max(0, n));
  };

  // Sort the object's values by a criterion produced by an iteratee.
  _.sortBy = function(obj, iteratee, context) {
    iteratee = cb(iteratee, context);
    return _.pluck(_.map(obj, function(value, index, list) {
      return {
        value: value,
        index: index,
        criteria: iteratee(value, index, list)
      };
    }).sort(function(left, right) {
      var a = left.criteria;
      var b = right.criteria;
      if (a !== b) {
        if (a > b || a === void 0) return 1;
        if (a < b || b === void 0) return -1;
      }
      return left.index - right.index;
    }), 'value');
  };

  // An internal function used for aggregate "group by" operations.
  var group = function(behavior) {
    return function(obj, iteratee, context) {
      var result = {};
      iteratee = cb(iteratee, context);
      _.each(obj, function(value, index) {
        var key = iteratee(value, index, obj);
        behavior(result, value, key);
      });
      return result;
    };
  };

  // Groups the object's values by a criterion. Pass either a string attribute
  // to group by, or a function that returns the criterion.
  _.groupBy = group(function(result, value, key) {
    if (_.has(result, key)) result[key].push(value); else result[key] = [value];
  });

  // Indexes the object's values by a criterion, similar to `groupBy`, but for
  // when you know that your index values will be unique.
  _.indexBy = group(function(result, value, key) {
    result[key] = value;
  });

  // Counts instances of an object that group by a certain criterion. Pass
  // either a string attribute to count by, or a function that returns the
  // criterion.
  _.countBy = group(function(result, value, key) {
    if (_.has(result, key)) result[key]++; else result[key] = 1;
  });

  // Safely create a real, live array from anything iterable.
  _.toArray = function(obj) {
    if (!obj) return [];
    if (_.isArray(obj)) return slice.call(obj);
    if (isArrayLike(obj)) return _.map(obj, _.identity);
    return _.values(obj);
  };

  // Return the number of elements in an object.
  _.size = function(obj) {
    if (obj == null) return 0;
    return isArrayLike(obj) ? obj.length : _.keys(obj).length;
  };

  // Split a collection into two arrays: one whose elements all satisfy the given
  // predicate, and one whose elements all do not satisfy the predicate.
  _.partition = function(obj, predicate, context) {
    predicate = cb(predicate, context);
    var pass = [], fail = [];
    _.each(obj, function(value, key, obj) {
      (predicate(value, key, obj) ? pass : fail).push(value);
    });
    return [pass, fail];
  };

  // Array Functions
  // ---------------

  // Get the first element of an array. Passing **n** will return the first N
  // values in the array. Aliased as `head` and `take`. The **guard** check
  // allows it to work with `_.map`.
  _.first = _.head = _.take = function(array, n, guard) {
    if (array == null) return void 0;
    if (n == null || guard) return array[0];
    return _.initial(array, array.length - n);
  };

  // Returns everything but the last entry of the array. Especially useful on
  // the arguments object. Passing **n** will return all the values in
  // the array, excluding the last N.
  _.initial = function(array, n, guard) {
    return slice.call(array, 0, Math.max(0, array.length - (n == null || guard ? 1 : n)));
  };

  // Get the last element of an array. Passing **n** will return the last N
  // values in the array.
  _.last = function(array, n, guard) {
    if (array == null) return void 0;
    if (n == null || guard) return array[array.length - 1];
    return _.rest(array, Math.max(0, array.length - n));
  };

  // Returns everything but the first entry of the array. Aliased as `tail` and `drop`.
  // Especially useful on the arguments object. Passing an **n** will return
  // the rest N values in the array.
  _.rest = _.tail = _.drop = function(array, n, guard) {
    return slice.call(array, n == null || guard ? 1 : n);
  };

  // Trim out all falsy values from an array.
  _.compact = function(array) {
    return _.filter(array, _.identity);
  };

  // Internal implementation of a recursive `flatten` function.
  var flatten = function(input, shallow, strict, startIndex) {
    var output = [], idx = 0;
    for (var i = startIndex || 0, length = getLength(input); i < length; i++) {
      var value = input[i];
      if (isArrayLike(value) && (_.isArray(value) || _.isArguments(value))) {
        //flatten current level of array or arguments object
        if (!shallow) value = flatten(value, shallow, strict);
        var j = 0, len = value.length;
        output.length += len;
        while (j < len) {
          output[idx++] = value[j++];
        }
      } else if (!strict) {
        output[idx++] = value;
      }
    }
    return output;
  };

  // Flatten out an array, either recursively (by default), or just one level.
  _.flatten = function(array, shallow) {
    return flatten(array, shallow, false);
  };

  // Return a version of the array that does not contain the specified value(s).
  _.without = function(array) {
    return _.difference(array, slice.call(arguments, 1));
  };

  // Produce a duplicate-free version of the array. If the array has already
  // been sorted, you have the option of using a faster algorithm.
  // Aliased as `unique`.
  _.uniq = _.unique = function(array, isSorted, iteratee, context) {
    if (!_.isBoolean(isSorted)) {
      context = iteratee;
      iteratee = isSorted;
      isSorted = false;
    }
    if (iteratee != null) iteratee = cb(iteratee, context);
    var result = [];
    var seen = [];
    for (var i = 0, length = getLength(array); i < length; i++) {
      var value = array[i],
          computed = iteratee ? iteratee(value, i, array) : value;
      if (isSorted) {
        if (!i || seen !== computed) result.push(value);
        seen = computed;
      } else if (iteratee) {
        if (!_.contains(seen, computed)) {
          seen.push(computed);
          result.push(value);
        }
      } else if (!_.contains(result, value)) {
        result.push(value);
      }
    }
    return result;
  };

  // Produce an array that contains the union: each distinct element from all of
  // the passed-in arrays.
  _.union = function() {
    return _.uniq(flatten(arguments, true, true));
  };

  // Produce an array that contains every item shared between all the
  // passed-in arrays.
  _.intersection = function(array) {
    var result = [];
    var argsLength = arguments.length;
    for (var i = 0, length = getLength(array); i < length; i++) {
      var item = array[i];
      if (_.contains(result, item)) continue;
      for (var j = 1; j < argsLength; j++) {
        if (!_.contains(arguments[j], item)) break;
      }
      if (j === argsLength) result.push(item);
    }
    return result;
  };

  // Take the difference between one array and a number of other arrays.
  // Only the elements present in just the first array will remain.
  _.difference = function(array) {
    var rest = flatten(arguments, true, true, 1);
    return _.filter(array, function(value){
      return !_.contains(rest, value);
    });
  };

  // Zip together multiple lists into a single array -- elements that share
  // an index go together.
  _.zip = function() {
    return _.unzip(arguments);
  };

  // Complement of _.zip. Unzip accepts an array of arrays and groups
  // each array's elements on shared indices
  _.unzip = function(array) {
    var length = array && _.max(array, getLength).length || 0;
    var result = Array(length);

    for (var index = 0; index < length; index++) {
      result[index] = _.pluck(array, index);
    }
    return result;
  };

  // Converts lists into objects. Pass either a single array of `[key, value]`
  // pairs, or two parallel arrays of the same length -- one of keys, and one of
  // the corresponding values.
  _.object = function(list, values) {
    var result = {};
    for (var i = 0, length = getLength(list); i < length; i++) {
      if (values) {
        result[list[i]] = values[i];
      } else {
        result[list[i][0]] = list[i][1];
      }
    }
    return result;
  };

  // Generator function to create the findIndex and findLastIndex functions
  function createPredicateIndexFinder(dir) {
    return function(array, predicate, context) {
      predicate = cb(predicate, context);
      var length = getLength(array);
      var index = dir > 0 ? 0 : length - 1;
      for (; index >= 0 && index < length; index += dir) {
        if (predicate(array[index], index, array)) return index;
      }
      return -1;
    };
  }

  // Returns the first index on an array-like that passes a predicate test
  _.findIndex = createPredicateIndexFinder(1);
  _.findLastIndex = createPredicateIndexFinder(-1);

  // Use a comparator function to figure out the smallest index at which
  // an object should be inserted so as to maintain order. Uses binary search.
  _.sortedIndex = function(array, obj, iteratee, context) {
    iteratee = cb(iteratee, context, 1);
    var value = iteratee(obj);
    var low = 0, high = getLength(array);
    while (low < high) {
      var mid = Math.floor((low + high) / 2);
      if (iteratee(array[mid]) < value) low = mid + 1; else high = mid;
    }
    return low;
  };

  // Generator function to create the indexOf and lastIndexOf functions
  function createIndexFinder(dir, predicateFind, sortedIndex) {
    return function(array, item, idx) {
      var i = 0, length = getLength(array);
      if (typeof idx == 'number') {
        if (dir > 0) {
            i = idx >= 0 ? idx : Math.max(idx + length, i);
        } else {
            length = idx >= 0 ? Math.min(idx + 1, length) : idx + length + 1;
        }
      } else if (sortedIndex && idx && length) {
        idx = sortedIndex(array, item);
        return array[idx] === item ? idx : -1;
      }
      if (item !== item) {
        idx = predicateFind(slice.call(array, i, length), _.isNaN);
        return idx >= 0 ? idx + i : -1;
      }
      for (idx = dir > 0 ? i : length - 1; idx >= 0 && idx < length; idx += dir) {
        if (array[idx] === item) return idx;
      }
      return -1;
    };
  }

  // Return the position of the first occurrence of an item in an array,
  // or -1 if the item is not included in the array.
  // If the array is large and already in sort order, pass `true`
  // for **isSorted** to use binary search.
  _.indexOf = createIndexFinder(1, _.findIndex, _.sortedIndex);
  _.lastIndexOf = createIndexFinder(-1, _.findLastIndex);

  // Generate an integer Array containing an arithmetic progression. A port of
  // the native Python `range()` function. See
  // [the Python documentation](http://docs.python.org/library/functions.html#range).
  _.range = function(start, stop, step) {
    if (stop == null) {
      stop = start || 0;
      start = 0;
    }
    step = step || 1;

    var length = Math.max(Math.ceil((stop - start) / step), 0);
    var range = Array(length);

    for (var idx = 0; idx < length; idx++, start += step) {
      range[idx] = start;
    }

    return range;
  };

  // Function (ahem) Functions
  // ------------------

  // Determines whether to execute a function as a constructor
  // or a normal function with the provided arguments
  var executeBound = function(sourceFunc, boundFunc, context, callingContext, args) {
    if (!(callingContext instanceof boundFunc)) return sourceFunc.apply(context, args);
    var self = baseCreate(sourceFunc.prototype);
    var result = sourceFunc.apply(self, args);
    if (_.isObject(result)) return result;
    return self;
  };

  // Create a function bound to a given object (assigning `this`, and arguments,
  // optionally). Delegates to **ECMAScript 5**'s native `Function.bind` if
  // available.
  _.bind = function(func, context) {
    if (nativeBind && func.bind === nativeBind) return nativeBind.apply(func, slice.call(arguments, 1));
    if (!_.isFunction(func)) throw new TypeError('Bind must be called on a function');
    var args = slice.call(arguments, 2);
    var bound = function() {
      return executeBound(func, bound, context, this, args.concat(slice.call(arguments)));
    };
    return bound;
  };

  // Partially apply a function by creating a version that has had some of its
  // arguments pre-filled, without changing its dynamic `this` context. _ acts
  // as a placeholder, allowing any combination of arguments to be pre-filled.
  _.partial = function(func) {
    var boundArgs = slice.call(arguments, 1);
    var bound = function() {
      var position = 0, length = boundArgs.length;
      var args = Array(length);
      for (var i = 0; i < length; i++) {
        args[i] = boundArgs[i] === _ ? arguments[position++] : boundArgs[i];
      }
      while (position < arguments.length) args.push(arguments[position++]);
      return executeBound(func, bound, this, this, args);
    };
    return bound;
  };

  // Bind a number of an object's methods to that object. Remaining arguments
  // are the method names to be bound. Useful for ensuring that all callbacks
  // defined on an object belong to it.
  _.bindAll = function(obj) {
    var i, length = arguments.length, key;
    if (length <= 1) throw new Error('bindAll must be passed function names');
    for (i = 1; i < length; i++) {
      key = arguments[i];
      obj[key] = _.bind(obj[key], obj);
    }
    return obj;
  };

  // Memoize an expensive function by storing its results.
  _.memoize = function(func, hasher) {
    var memoize = function(key) {
      var cache = memoize.cache;
      var address = '' + (hasher ? hasher.apply(this, arguments) : key);
      if (!_.has(cache, address)) cache[address] = func.apply(this, arguments);
      return cache[address];
    };
    memoize.cache = {};
    return memoize;
  };

  // Delays a function for the given number of milliseconds, and then calls
  // it with the arguments supplied.
  _.delay = function(func, wait) {
    var args = slice.call(arguments, 2);
    return setTimeout(function(){
      return func.apply(null, args);
    }, wait);
  };

  // Defers a function, scheduling it to run after the current call stack has
  // cleared.
  _.defer = _.partial(_.delay, _, 1);

  // Returns a function, that, when invoked, will only be triggered at most once
  // during a given window of time. Normally, the throttled function will run
  // as much as it can, without ever going more than once per `wait` duration;
  // but if you'd like to disable the execution on the leading edge, pass
  // `{leading: false}`. To disable execution on the trailing edge, ditto.
  _.throttle = function(func, wait, options) {
    var context, args, result;
    var timeout = null;
    var previous = 0;
    if (!options) options = {};
    var later = function() {
      previous = options.leading === false ? 0 : _.now();
      timeout = null;
      result = func.apply(context, args);
      if (!timeout) context = args = null;
    };
    return function() {
      var now = _.now();
      if (!previous && options.leading === false) previous = now;
      var remaining = wait - (now - previous);
      context = this;
      args = arguments;
      if (remaining <= 0 || remaining > wait) {
        if (timeout) {
          clearTimeout(timeout);
          timeout = null;
        }
        previous = now;
        result = func.apply(context, args);
        if (!timeout) context = args = null;
      } else if (!timeout && options.trailing !== false) {
        timeout = setTimeout(later, remaining);
      }
      return result;
    };
  };

  // Returns a function, that, as long as it continues to be invoked, will not
  // be triggered. The function will be called after it stops being called for
  // N milliseconds. If `immediate` is passed, trigger the function on the
  // leading edge, instead of the trailing.
  _.debounce = function(func, wait, immediate) {
    var timeout, args, context, timestamp, result;

    var later = function() {
      var last = _.now() - timestamp;

      if (last < wait && last >= 0) {
        timeout = setTimeout(later, wait - last);
      } else {
        timeout = null;
        if (!immediate) {
          result = func.apply(context, args);
          if (!timeout) context = args = null;
        }
      }
    };

    return function() {
      context = this;
      args = arguments;
      timestamp = _.now();
      var callNow = immediate && !timeout;
      if (!timeout) timeout = setTimeout(later, wait);
      if (callNow) {
        result = func.apply(context, args);
        context = args = null;
      }

      return result;
    };
  };

  // Returns the first function passed as an argument to the second,
  // allowing you to adjust arguments, run code before and after, and
  // conditionally execute the original function.
  _.wrap = function(func, wrapper) {
    return _.partial(wrapper, func);
  };

  // Returns a negated version of the passed-in predicate.
  _.negate = function(predicate) {
    return function() {
      return !predicate.apply(this, arguments);
    };
  };

  // Returns a function that is the composition of a list of functions, each
  // consuming the return value of the function that follows.
  _.compose = function() {
    var args = arguments;
    var start = args.length - 1;
    return function() {
      var i = start;
      var result = args[start].apply(this, arguments);
      while (i--) result = args[i].call(this, result);
      return result;
    };
  };

  // Returns a function that will only be executed on and after the Nth call.
  _.after = function(times, func) {
    return function() {
      if (--times < 1) {
        return func.apply(this, arguments);
      }
    };
  };

  // Returns a function that will only be executed up to (but not including) the Nth call.
  _.before = function(times, func) {
    var memo;
    return function() {
      if (--times > 0) {
        memo = func.apply(this, arguments);
      }
      if (times <= 1) func = null;
      return memo;
    };
  };

  // Returns a function that will be executed at most one time, no matter how
  // often you call it. Useful for lazy initialization.
  _.once = _.partial(_.before, 2);

  // Object Functions
  // ----------------

  // Keys in IE < 9 that won't be iterated by `for key in ...` and thus missed.
  var hasEnumBug = !{toString: null}.propertyIsEnumerable('toString');
  var nonEnumerableProps = ['valueOf', 'isPrototypeOf', 'toString',
                      'propertyIsEnumerable', 'hasOwnProperty', 'toLocaleString'];

  function collectNonEnumProps(obj, keys) {
    var nonEnumIdx = nonEnumerableProps.length;
    var constructor = obj.constructor;
    var proto = (_.isFunction(constructor) && constructor.prototype) || ObjProto;

    // Constructor is a special case.
    var prop = 'constructor';
    if (_.has(obj, prop) && !_.contains(keys, prop)) keys.push(prop);

    while (nonEnumIdx--) {
      prop = nonEnumerableProps[nonEnumIdx];
      if (prop in obj && obj[prop] !== proto[prop] && !_.contains(keys, prop)) {
        keys.push(prop);
      }
    }
  }

  // Retrieve the names of an object's own properties.
  // Delegates to **ECMAScript 5**'s native `Object.keys`
  _.keys = function(obj) {
    if (!_.isObject(obj)) return [];
    if (nativeKeys) return nativeKeys(obj);
    var keys = [];
    for (var key in obj) if (_.has(obj, key)) keys.push(key);
    // Ahem, IE < 9.
    if (hasEnumBug) collectNonEnumProps(obj, keys);
    return keys;
  };

  // Retrieve all the property names of an object.
  _.allKeys = function(obj) {
    if (!_.isObject(obj)) return [];
    var keys = [];
    for (var key in obj) keys.push(key);
    // Ahem, IE < 9.
    if (hasEnumBug) collectNonEnumProps(obj, keys);
    return keys;
  };

  // Retrieve the values of an object's properties.
  _.values = function(obj) {
    var keys = _.keys(obj);
    var length = keys.length;
    var values = Array(length);
    for (var i = 0; i < length; i++) {
      values[i] = obj[keys[i]];
    }
    return values;
  };

  // Returns the results of applying the iteratee to each element of the object
  // In contrast to _.map it returns an object
  _.mapObject = function(obj, iteratee, context) {
    iteratee = cb(iteratee, context);
    var keys =  _.keys(obj),
          length = keys.length,
          results = {},
          currentKey;
      for (var index = 0; index < length; index++) {
        currentKey = keys[index];
        results[currentKey] = iteratee(obj[currentKey], currentKey, obj);
      }
      return results;
  };

  // Convert an object into a list of `[key, value]` pairs.
  _.pairs = function(obj) {
    var keys = _.keys(obj);
    var length = keys.length;
    var pairs = Array(length);
    for (var i = 0; i < length; i++) {
      pairs[i] = [keys[i], obj[keys[i]]];
    }
    return pairs;
  };

  // Invert the keys and values of an object. The values must be serializable.
  _.invert = function(obj) {
    var result = {};
    var keys = _.keys(obj);
    for (var i = 0, length = keys.length; i < length; i++) {
      result[obj[keys[i]]] = keys[i];
    }
    return result;
  };

  // Return a sorted list of the function names available on the object.
  // Aliased as `methods`
  _.functions = _.methods = function(obj) {
    var names = [];
    for (var key in obj) {
      if (_.isFunction(obj[key])) names.push(key);
    }
    return names.sort();
  };

  // Extend a given object with all the properties in passed-in object(s).
  _.extend = createAssigner(_.allKeys);

  // Assigns a given object with all the own properties in the passed-in object(s)
  // (https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Object/assign)
  _.extendOwn = _.assign = createAssigner(_.keys);

  // Returns the first key on an object that passes a predicate test
  _.findKey = function(obj, predicate, context) {
    predicate = cb(predicate, context);
    var keys = _.keys(obj), key;
    for (var i = 0, length = keys.length; i < length; i++) {
      key = keys[i];
      if (predicate(obj[key], key, obj)) return key;
    }
  };

  // Return a copy of the object only containing the whitelisted properties.
  _.pick = function(object, oiteratee, context) {
    var result = {}, obj = object, iteratee, keys;
    if (obj == null) return result;
    if (_.isFunction(oiteratee)) {
      keys = _.allKeys(obj);
      iteratee = optimizeCb(oiteratee, context);
    } else {
      keys = flatten(arguments, false, false, 1);
      iteratee = function(value, key, obj) { return key in obj; };
      obj = Object(obj);
    }
    for (var i = 0, length = keys.length; i < length; i++) {
      var key = keys[i];
      var value = obj[key];
      if (iteratee(value, key, obj)) result[key] = value;
    }
    return result;
  };

   // Return a copy of the object without the blacklisted properties.
  _.omit = function(obj, iteratee, context) {
    if (_.isFunction(iteratee)) {
      iteratee = _.negate(iteratee);
    } else {
      var keys = _.map(flatten(arguments, false, false, 1), String);
      iteratee = function(value, key) {
        return !_.contains(keys, key);
      };
    }
    return _.pick(obj, iteratee, context);
  };

  // Fill in a given object with default properties.
  _.defaults = createAssigner(_.allKeys, true);

  // Creates an object that inherits from the given prototype object.
  // If additional properties are provided then they will be added to the
  // created object.
  _.create = function(prototype, props) {
    var result = baseCreate(prototype);
    if (props) _.extendOwn(result, props);
    return result;
  };

  // Create a (shallow-cloned) duplicate of an object.
  _.clone = function(obj) {
    if (!_.isObject(obj)) return obj;
    return _.isArray(obj) ? obj.slice() : _.extend({}, obj);
  };

  // Invokes interceptor with the obj, and then returns obj.
  // The primary purpose of this method is to "tap into" a method chain, in
  // order to perform operations on intermediate results within the chain.
  _.tap = function(obj, interceptor) {
    interceptor(obj);
    return obj;
  };

  // Returns whether an object has a given set of `key:value` pairs.
  _.isMatch = function(object, attrs) {
    var keys = _.keys(attrs), length = keys.length;
    if (object == null) return !length;
    var obj = Object(object);
    for (var i = 0; i < length; i++) {
      var key = keys[i];
      if (attrs[key] !== obj[key] || !(key in obj)) return false;
    }
    return true;
  };


  // Internal recursive comparison function for `isEqual`.
  var eq = function(a, b, aStack, bStack) {
    // Identical objects are equal. `0 === -0`, but they aren't identical.
    // See the [Harmony `egal` proposal](http://wiki.ecmascript.org/doku.php?id=harmony:egal).
    if (a === b) return a !== 0 || 1 / a === 1 / b;
    // A strict comparison is necessary because `null == undefined`.
    if (a == null || b == null) return a === b;
    // Unwrap any wrapped objects.
    if (a instanceof _) a = a._wrapped;
    if (b instanceof _) b = b._wrapped;
    // Compare `[[Class]]` names.
    var className = toString.call(a);
    if (className !== toString.call(b)) return false;
    switch (className) {
      // Strings, numbers, regular expressions, dates, and booleans are compared by value.
      case '[object RegExp]':
      // RegExps are coerced to strings for comparison (Note: '' + /a/i === '/a/i')
      case '[object String]':
        // Primitives and their corresponding object wrappers are equivalent; thus, `"5"` is
        // equivalent to `new String("5")`.
        return '' + a === '' + b;
      case '[object Number]':
        // `NaN`s are equivalent, but non-reflexive.
        // Object(NaN) is equivalent to NaN
        if (+a !== +a) return +b !== +b;
        // An `egal` comparison is performed for other numeric values.
        return +a === 0 ? 1 / +a === 1 / b : +a === +b;
      case '[object Date]':
      case '[object Boolean]':
        // Coerce dates and booleans to numeric primitive values. Dates are compared by their
        // millisecond representations. Note that invalid dates with millisecond representations
        // of `NaN` are not equivalent.
        return +a === +b;
    }

    var areArrays = className === '[object Array]';
    if (!areArrays) {
      if (typeof a != 'object' || typeof b != 'object') return false;

      // Objects with different constructors are not equivalent, but `Object`s or `Array`s
      // from different frames are.
      var aCtor = a.constructor, bCtor = b.constructor;
      if (aCtor !== bCtor && !(_.isFunction(aCtor) && aCtor instanceof aCtor &&
                               _.isFunction(bCtor) && bCtor instanceof bCtor)
                          && ('constructor' in a && 'constructor' in b)) {
        return false;
      }
    }
    // Assume equality for cyclic structures. The algorithm for detecting cyclic
    // structures is adapted from ES 5.1 section 15.12.3, abstract operation `JO`.

    // Initializing stack of traversed objects.
    // It's done here since we only need them for objects and arrays comparison.
    aStack = aStack || [];
    bStack = bStack || [];
    var length = aStack.length;
    while (length--) {
      // Linear search. Performance is inversely proportional to the number of
      // unique nested structures.
      if (aStack[length] === a) return bStack[length] === b;
    }

    // Add the first object to the stack of traversed objects.
    aStack.push(a);
    bStack.push(b);

    // Recursively compare objects and arrays.
    if (areArrays) {
      // Compare array lengths to determine if a deep comparison is necessary.
      length = a.length;
      if (length !== b.length) return false;
      // Deep compare the contents, ignoring non-numeric properties.
      while (length--) {
        if (!eq(a[length], b[length], aStack, bStack)) return false;
      }
    } else {
      // Deep compare objects.
      var keys = _.keys(a), key;
      length = keys.length;
      // Ensure that both objects contain the same number of properties before comparing deep equality.
      if (_.keys(b).length !== length) return false;
      while (length--) {
        // Deep compare each member
        key = keys[length];
        if (!(_.has(b, key) && eq(a[key], b[key], aStack, bStack))) return false;
      }
    }
    // Remove the first object from the stack of traversed objects.
    aStack.pop();
    bStack.pop();
    return true;
  };

  // Perform a deep comparison to check if two objects are equal.
  _.isEqual = function(a, b) {
    return eq(a, b);
  };

  // Is a given array, string, or object empty?
  // An "empty" object has no enumerable own-properties.
  _.isEmpty = function(obj) {
    if (obj == null) return true;
    if (isArrayLike(obj) && (_.isArray(obj) || _.isString(obj) || _.isArguments(obj))) return obj.length === 0;
    return _.keys(obj).length === 0;
  };

  // Is a given value a DOM element?
  _.isElement = function(obj) {
    return !!(obj && obj.nodeType === 1);
  };

  // Is a given value an array?
  // Delegates to ECMA5's native Array.isArray
  _.isArray = nativeIsArray || function(obj) {
    return toString.call(obj) === '[object Array]';
  };

  // Is a given variable an object?
  _.isObject = function(obj) {
    var type = typeof obj;
    return type === 'function' || type === 'object' && !!obj;
  };

  // Add some isType methods: isArguments, isFunction, isString, isNumber, isDate, isRegExp, isError.
  _.each(['Arguments', 'Function', 'String', 'Number', 'Date', 'RegExp', 'Error'], function(name) {
    _['is' + name] = function(obj) {
      return toString.call(obj) === '[object ' + name + ']';
    };
  });

  // Define a fallback version of the method in browsers (ahem, IE < 9), where
  // there isn't any inspectable "Arguments" type.
  if (!_.isArguments(arguments)) {
    _.isArguments = function(obj) {
      return _.has(obj, 'callee');
    };
  }

  // Optimize `isFunction` if appropriate. Work around some typeof bugs in old v8,
  // IE 11 (#1621), and in Safari 8 (#1929).
  if (typeof /./ != 'function' && typeof Int8Array != 'object') {
    _.isFunction = function(obj) {
      return typeof obj == 'function' || false;
    };
  }

  // Is a given object a finite number?
  _.isFinite = function(obj) {
    return isFinite(obj) && !isNaN(parseFloat(obj));
  };

  // Is the given value `NaN`? (NaN is the only number which does not equal itself).
  _.isNaN = function(obj) {
    return _.isNumber(obj) && obj !== +obj;
  };

  // Is a given value a boolean?
  _.isBoolean = function(obj) {
    return obj === true || obj === false || toString.call(obj) === '[object Boolean]';
  };

  // Is a given value equal to null?
  _.isNull = function(obj) {
    return obj === null;
  };

  // Is a given variable undefined?
  _.isUndefined = function(obj) {
    return obj === void 0;
  };

  // Shortcut function for checking if an object has a given property directly
  // on itself (in other words, not on a prototype).
  _.has = function(obj, key) {
    return obj != null && hasOwnProperty.call(obj, key);
  };

  // Utility Functions
  // -----------------

  // Run Underscore.js in *noConflict* mode, returning the `_` variable to its
  // previous owner. Returns a reference to the Underscore object.
  _.noConflict = function() {
    root._ = previousUnderscore;
    return this;
  };

  // Keep the identity function around for default iteratees.
  _.identity = function(value) {
    return value;
  };

  // Predicate-generating functions. Often useful outside of Underscore.
  _.constant = function(value) {
    return function() {
      return value;
    };
  };

  _.noop = function(){};

  _.property = property;

  // Generates a function for a given object that returns a given property.
  _.propertyOf = function(obj) {
    return obj == null ? function(){} : function(key) {
      return obj[key];
    };
  };

  // Returns a predicate for checking whether an object has a given set of
  // `key:value` pairs.
  _.matcher = _.matches = function(attrs) {
    attrs = _.extendOwn({}, attrs);
    return function(obj) {
      return _.isMatch(obj, attrs);
    };
  };

  // Run a function **n** times.
  _.times = function(n, iteratee, context) {
    var accum = Array(Math.max(0, n));
    iteratee = optimizeCb(iteratee, context, 1);
    for (var i = 0; i < n; i++) accum[i] = iteratee(i);
    return accum;
  };

  // Return a random integer between min and max (inclusive).
  _.random = function(min, max) {
    if (max == null) {
      max = min;
      min = 0;
    }
    return min + Math.floor(Math.random() * (max - min + 1));
  };

  // A (possibly faster) way to get the current timestamp as an integer.
  _.now = Date.now || function() {
    return new Date().getTime();
  };

   // List of HTML entities for escaping.
  var escapeMap = {
    '&': '&amp;',
    '<': '&lt;',
    '>': '&gt;',
    '"': '&quot;',
    "'": '&#x27;',
    '`': '&#x60;'
  };
  var unescapeMap = _.invert(escapeMap);

  // Functions for escaping and unescaping strings to/from HTML interpolation.
  var createEscaper = function(map) {
    var escaper = function(match) {
      return map[match];
    };
    // Regexes for identifying a key that needs to be escaped
    var source = '(?:' + _.keys(map).join('|') + ')';
    var testRegexp = RegExp(source);
    var replaceRegexp = RegExp(source, 'g');
    return function(string) {
      string = string == null ? '' : '' + string;
      return testRegexp.test(string) ? string.replace(replaceRegexp, escaper) : string;
    };
  };
  _.escape = createEscaper(escapeMap);
  _.unescape = createEscaper(unescapeMap);

  // If the value of the named `property` is a function then invoke it with the
  // `object` as context; otherwise, return it.
  _.result = function(object, property, fallback) {
    var value = object == null ? void 0 : object[property];
    if (value === void 0) {
      value = fallback;
    }
    return _.isFunction(value) ? value.call(object) : value;
  };

  // Generate a unique integer id (unique within the entire client session).
  // Useful for temporary DOM ids.
  var idCounter = 0;
  _.uniqueId = function(prefix) {
    var id = ++idCounter + '';
    return prefix ? prefix + id : id;
  };

  // By default, Underscore uses ERB-style template delimiters, change the
  // following template settings to use alternative delimiters.
  _.templateSettings = {
    evaluate    : /<%([\s\S]+?)%>/g,
    interpolate : /<%=([\s\S]+?)%>/g,
    escape      : /<%-([\s\S]+?)%>/g
  };

  // When customizing `templateSettings`, if you don't want to define an
  // interpolation, evaluation or escaping regex, we need one that is
  // guaranteed not to match.
  var noMatch = /(.)^/;

  // Certain characters need to be escaped so that they can be put into a
  // string literal.
  var escapes = {
    "'":      "'",
    '\\':     '\\',
    '\r':     'r',
    '\n':     'n',
    '\u2028': 'u2028',
    '\u2029': 'u2029'
  };

  var escaper = /\\|'|\r|\n|\u2028|\u2029/g;

  var escapeChar = function(match) {
    return '\\' + escapes[match];
  };

  // JavaScript micro-templating, similar to John Resig's implementation.
  // Underscore templating handles arbitrary delimiters, preserves whitespace,
  // and correctly escapes quotes within interpolated code.
  // NB: `oldSettings` only exists for backwards compatibility.
  _.template = function(text, settings, oldSettings) {
    if (!settings && oldSettings) settings = oldSettings;
    settings = _.defaults({}, settings, _.templateSettings);

    // Combine delimiters into one regular expression via alternation.
    var matcher = RegExp([
      (settings.escape || noMatch).source,
      (settings.interpolate || noMatch).source,
      (settings.evaluate || noMatch).source
    ].join('|') + '|$', 'g');

    // Compile the template source, escaping string literals appropriately.
    var index = 0;
    var source = "__p+='";
    text.replace(matcher, function(match, escape, interpolate, evaluate, offset) {
      source += text.slice(index, offset).replace(escaper, escapeChar);
      index = offset + match.length;

      if (escape) {
        source += "'+\n((__t=(" + escape + "))==null?'':_.escape(__t))+\n'";
      } else if (interpolate) {
        source += "'+\n((__t=(" + interpolate + "))==null?'':__t)+\n'";
      } else if (evaluate) {
        source += "';\n" + evaluate + "\n__p+='";
      }

      // Adobe VMs need the match returned to produce the correct offest.
      return match;
    });
    source += "';\n";

    // If a variable is not specified, place data values in local scope.
    if (!settings.variable) source = 'with(obj||{}){\n' + source + '}\n';

    source = "var __t,__p='',__j=Array.prototype.join," +
      "print=function(){__p+=__j.call(arguments,'');};\n" +
      source + 'return __p;\n';

    try {
      var render = new Function(settings.variable || 'obj', '_', source);
    } catch (e) {
      e.source = source;
      throw e;
    }

    var template = function(data) {
      return render.call(this, data, _);
    };

    // Provide the compiled source as a convenience for precompilation.
    var argument = settings.variable || 'obj';
    template.source = 'function(' + argument + '){\n' + source + '}';

    return template;
  };

  // Add a "chain" function. Start chaining a wrapped Underscore object.
  _.chain = function(obj) {
    var instance = _(obj);
    instance._chain = true;
    return instance;
  };

  // OOP
  // ---------------
  // If Underscore is called as a function, it returns a wrapped object that
  // can be used OO-style. This wrapper holds altered versions of all the
  // underscore functions. Wrapped objects may be chained.

  // Helper function to continue chaining intermediate results.
  var result = function(instance, obj) {
    return instance._chain ? _(obj).chain() : obj;
  };

  // Add your own custom functions to the Underscore object.
  _.mixin = function(obj) {
    _.each(_.functions(obj), function(name) {
      var func = _[name] = obj[name];
      _.prototype[name] = function() {
        var args = [this._wrapped];
        push.apply(args, arguments);
        return result(this, func.apply(_, args));
      };
    });
  };

  // Add all of the Underscore functions to the wrapper object.
  _.mixin(_);

  // Add all mutator Array functions to the wrapper.
  _.each(['pop', 'push', 'reverse', 'shift', 'sort', 'splice', 'unshift'], function(name) {
    var method = ArrayProto[name];
    _.prototype[name] = function() {
      var obj = this._wrapped;
      method.apply(obj, arguments);
      if ((name === 'shift' || name === 'splice') && obj.length === 0) delete obj[0];
      return result(this, obj);
    };
  });

  // Add all accessor Array functions to the wrapper.
  _.each(['concat', 'join', 'slice'], function(name) {
    var method = ArrayProto[name];
    _.prototype[name] = function() {
      return result(this, method.apply(this._wrapped, arguments));
    };
  });

  // Extracts the result from a wrapped and chained object.
  _.prototype.value = function() {
    return this._wrapped;
  };

  // Provide unwrapping proxy for some methods used in engine operations
  // such as arithmetic and JSON stringification.
  _.prototype.valueOf = _.prototype.toJSON = _.prototype.value;

  _.prototype.toString = function() {
    return '' + this._wrapped;
  };

  // AMD registration happens at the end for compatibility with AMD loaders
  // that may not enforce next-turn semantics on modules. Even though general
  // practice for AMD registration is to be anonymous, underscore registers
  // as a named module because, like jQuery, it is a base library that is
  // popular enough to be bundled in a third party lib, but not be part of
  // an AMD load request. Those cases could generate an error when an
  // anonymous define() is called outside of a loader request.
  if (typeof define === 'function' && define.amd) {
    define('underscore', [], function() {
      return _;
    });
  }
}.call(this));

//     Backbone.js 1.3.3

//     (c) 2010-2016 Jeremy Ashkenas, DocumentCloud and Investigative Reporters & Editors
//     Backbone may be freely distributed under the MIT license.
//     For all details and documentation:
//     http://backbonejs.org

(function (factory) {

    // Establish the root object, `window` (`self`) in the browser, or `global` on the server.
    // We use `self` instead of `window` for `WebWorker` support.
    var root = (typeof self == 'object' && self.self === self && self) ||
              (typeof global == 'object' && global.global === global && global);

    root.Backbone = factory(root, {}, root._, (root.jQuery || root.Zepto || root.ender || root.$));

    //// Set up Backbone appropriately for the environment. Start with AMD.
    //if (typeof define === 'function' && define.amd) {
    //    define(['underscore', 'jquery', 'exports'], function (_, $, exports) {
    //        // Export global even in AMD case in case this script is loaded with
    //        // others that may still expect a global Backbone.
    //        root.Backbone = factory(root, exports, _, $);
    //    });

    //    // Next for Node.js or CommonJS. jQuery may not be needed as a module.
    //} else if (typeof exports !== 'undefined') {
    //    var _ = require('underscore'), $;
    //    try { $ = require('jquery'); } catch (e) { }
    //    factory(root, exports, _, $);

    //    // Finally, as a browser global.
    //} else {
    //    root.Backbone = factory(root, {}, root._, (root.jQuery || root.Zepto || root.ender || root.$));
    //}

})(function (root, Backbone, _, $) {

    // Initial Setup
    // -------------

    // Save the previous value of the `Backbone` variable, so that it can be
    // restored later on, if `noConflict` is used.
    var previousBackbone = root.Backbone;

    // Create a local reference to a common array method we'll want to use later.
    var slice = Array.prototype.slice;

    // Current version of the library. Keep in sync with `package.json`.
    Backbone.VERSION = '1.3.3';

    // For Backbone's purposes, jQuery, Zepto, Ender, or My Library (kidding) owns
    // the `$` variable.
    Backbone.$ = $;

    // Runs Backbone.js in *noConflict* mode, returning the `Backbone` variable
    // to its previous owner. Returns a reference to this Backbone object.
    Backbone.noConflict = function () {
        root.Backbone = previousBackbone;
        return this;
    };

    // Turn on `emulateHTTP` to support legacy HTTP servers. Setting this option
    // will fake `"PATCH"`, `"PUT"` and `"DELETE"` requests via the `_method` parameter and
    // set a `X-Http-Method-Override` header.
    Backbone.emulateHTTP = false;

    // Turn on `emulateJSON` to support legacy servers that can't deal with direct
    // `application/json` requests ... this will encode the body as
    // `application/x-www-form-urlencoded` instead and will send the model in a
    // form param named `model`.
    Backbone.emulateJSON = false;

    // Proxy Backbone class methods to Underscore functions, wrapping the model's
    // `attributes` object or collection's `models` array behind the scenes.
    //
    // collection.filter(function(model) { return model.get('age') > 10 });
    // collection.each(this.addView);
    //
    // `Function#apply` can be slow so we use the method's arg count, if we know it.
    var addMethod = function (length, method, attribute) {
        switch (length) {
            case 1: return function () {
                return _[method](this[attribute]);
            };
            case 2: return function (value) {
                return _[method](this[attribute], value);
            };
            case 3: return function (iteratee, context) {
                return _[method](this[attribute], cb(iteratee, this), context);
            };
            case 4: return function (iteratee, defaultVal, context) {
                return _[method](this[attribute], cb(iteratee, this), defaultVal, context);
            };
            default: return function () {
                var args = slice.call(arguments);
                args.unshift(this[attribute]);
                return _[method].apply(_, args);
            };
        }
    };
    var addUnderscoreMethods = function (Class, methods, attribute) {
        _.each(methods, function (length, method) {
            if (_[method]) Class.prototype[method] = addMethod(length, method, attribute);
        });
    };

    // Support `collection.sortBy('attr')` and `collection.findWhere({id: 1})`.
    var cb = function (iteratee, instance) {
        if (_.isFunction(iteratee)) return iteratee;
        if (_.isObject(iteratee) && !instance._isModel(iteratee)) return modelMatcher(iteratee);
        if (_.isString(iteratee)) return function (model) { return model.get(iteratee); };
        return iteratee;
    };
    var modelMatcher = function (attrs) {
        var matcher = _.matches(attrs);
        return function (model) {
            return matcher(model.attributes);
        };
    };

    // Backbone.Events
    // ---------------

    // A module that can be mixed in to *any object* in order to provide it with
    // a custom event channel. You may bind a callback to an event with `on` or
    // remove with `off`; `trigger`-ing an event fires all callbacks in
    // succession.
    //
    //     var object = {};
    //     _.extend(object, Backbone.Events);
    //     object.on('expand', function(){ alert('expanded'); });
    //     object.trigger('expand');
    //
    var Events = Backbone.Events = {};

    // Regular expression used to split event strings.
    var eventSplitter = /\s+/;

    // Iterates over the standard `event, callback` (as well as the fancy multiple
    // space-separated events `"change blur", callback` and jQuery-style event
    // maps `{event: callback}`).
    var eventsApi = function (iteratee, events, name, callback, opts) {
        var i = 0, names;
        if (name && typeof name === 'object') {
            // Handle event maps.
            if (callback !== void 0 && 'context' in opts && opts.context === void 0) opts.context = callback;
            for (names = _.keys(name) ; i < names.length ; i++) {
                events = eventsApi(iteratee, events, names[i], name[names[i]], opts);
            }
        } else if (name && eventSplitter.test(name)) {
            // Handle space-separated event names by delegating them individually.
            for (names = name.split(eventSplitter) ; i < names.length; i++) {
                events = iteratee(events, names[i], callback, opts);
            }
        } else {
            // Finally, standard events.
            events = iteratee(events, name, callback, opts);
        }
        return events;
    };

    // Bind an event to a `callback` function. Passing `"all"` will bind
    // the callback to all events fired.
    Events.on = function (name, callback, context) {
        return internalOn(this, name, callback, context);
    };

    // Guard the `listening` argument from the public API.
    var internalOn = function (obj, name, callback, context, listening) {
        obj._events = eventsApi(onApi, obj._events || {}, name, callback, {
            context: context,
            ctx: obj,
            listening: listening
        });

        if (listening) {
            var listeners = obj._listeners || (obj._listeners = {});
            listeners[listening.id] = listening;
        }

        return obj;
    };

    // Inversion-of-control versions of `on`. Tell *this* object to listen to
    // an event in another object... keeping track of what it's listening to
    // for easier unbinding later.
    Events.listenTo = function (obj, name, callback) {
        if (!obj) return this;
        var id = obj._listenId || (obj._listenId = _.uniqueId('l'));
        var listeningTo = this._listeningTo || (this._listeningTo = {});
        var listening = listeningTo[id];

        // This object is not listening to any other events on `obj` yet.
        // Setup the necessary references to track the listening callbacks.
        if (!listening) {
            var thisId = this._listenId || (this._listenId = _.uniqueId('l'));
            listening = listeningTo[id] = { obj: obj, objId: id, id: thisId, listeningTo: listeningTo, count: 0 };
        }

        // Bind callbacks on obj, and keep track of them on listening.
        internalOn(obj, name, callback, this, listening);
        return this;
    };

    // The reducing API that adds a callback to the `events` object.
    var onApi = function (events, name, callback, options) {
        if (callback) {
            var handlers = events[name] || (events[name] = []);
            var context = options.context, ctx = options.ctx, listening = options.listening;
            if (listening) listening.count++;

            handlers.push({ callback: callback, context: context, ctx: context || ctx, listening: listening });
        }
        return events;
    };

    // Remove one or many callbacks. If `context` is null, removes all
    // callbacks with that function. If `callback` is null, removes all
    // callbacks for the event. If `name` is null, removes all bound
    // callbacks for all events.
    Events.off = function (name, callback, context) {
        if (!this._events) return this;
        this._events = eventsApi(offApi, this._events, name, callback, {
            context: context,
            listeners: this._listeners
        });
        return this;
    };

    // Tell this object to stop listening to either specific events ... or
    // to every object it's currently listening to.
    Events.stopListening = function (obj, name, callback) {
        var listeningTo = this._listeningTo;
        if (!listeningTo) return this;

        var ids = obj ? [obj._listenId] : _.keys(listeningTo);

        for (var i = 0; i < ids.length; i++) {
            var listening = listeningTo[ids[i]];

            // If listening doesn't exist, this object is not currently
            // listening to obj. Break out early.
            if (!listening) break;

            listening.obj.off(name, callback, this);
        }

        return this;
    };

    // The reducing API that removes a callback from the `events` object.
    var offApi = function (events, name, callback, options) {
        if (!events) return;

        var i = 0, listening;
        var context = options.context, listeners = options.listeners;

        // Delete all events listeners and "drop" events.
        if (!name && !callback && !context) {
            var ids = _.keys(listeners);
            for (; i < ids.length; i++) {
                listening = listeners[ids[i]];
                delete listeners[listening.id];
                delete listening.listeningTo[listening.objId];
            }
            return;
        }

        var names = name ? [name] : _.keys(events);
        for (; i < names.length; i++) {
            name = names[i];
            var handlers = events[name];

            // Bail out if there are no events stored.
            if (!handlers) break;

            // Replace events if there are any remaining.  Otherwise, clean up.
            var remaining = [];
            for (var j = 0; j < handlers.length; j++) {
                var handler = handlers[j];
                if (
                  callback && callback !== handler.callback &&
                    callback !== handler.callback._callback ||
                      context && context !== handler.context
                ) {
                    remaining.push(handler);
                } else {
                    listening = handler.listening;
                    if (listening && --listening.count === 0) {
                        delete listeners[listening.id];
                        delete listening.listeningTo[listening.objId];
                    }
                }
            }

            // Update tail event if the list has any events.  Otherwise, clean up.
            if (remaining.length) {
                events[name] = remaining;
            } else {
                delete events[name];
            }
        }
        return events;
    };

    // Bind an event to only be triggered a single time. After the first time
    // the callback is invoked, its listener will be removed. If multiple events
    // are passed in using the space-separated syntax, the handler will fire
    // once for each event, not once for a combination of all events.
    Events.once = function (name, callback, context) {
        // Map the event into a `{event: once}` object.
        var events = eventsApi(onceMap, {}, name, callback, _.bind(this.off, this));
        if (typeof name === 'string' && context == null) callback = void 0;
        return this.on(events, callback, context);
    };

    // Inversion-of-control versions of `once`.
    Events.listenToOnce = function (obj, name, callback) {
        // Map the event into a `{event: once}` object.
        var events = eventsApi(onceMap, {}, name, callback, _.bind(this.stopListening, this, obj));
        return this.listenTo(obj, events);
    };

    // Reduces the event callbacks into a map of `{event: onceWrapper}`.
    // `offer` unbinds the `onceWrapper` after it has been called.
    var onceMap = function (map, name, callback, offer) {
        if (callback) {
            var once = map[name] = _.once(function () {
                offer(name, once);
                callback.apply(this, arguments);
            });
            once._callback = callback;
        }
        return map;
    };

    // Trigger one or many events, firing all bound callbacks. Callbacks are
    // passed the same arguments as `trigger` is, apart from the event name
    // (unless you're listening on `"all"`, which will cause your callback to
    // receive the true name of the event as the first argument).
    Events.trigger = function (name) {
        if (!this._events) return this;

        var length = Math.max(0, arguments.length - 1);
        var args = Array(length);
        for (var i = 0; i < length; i++) args[i] = arguments[i + 1];

        eventsApi(triggerApi, this._events, name, void 0, args);
        return this;
    };

    // Handles triggering the appropriate event callbacks.
    var triggerApi = function (objEvents, name, callback, args) {
        if (objEvents) {
            var events = objEvents[name];
            var allEvents = objEvents.all;
            if (events && allEvents) allEvents = allEvents.slice();
            if (events) triggerEvents(events, args);
            if (allEvents) triggerEvents(allEvents, [name].concat(args));
        }
        return objEvents;
    };

    // A difficult-to-believe, but optimized internal dispatch function for
    // triggering events. Tries to keep the usual cases speedy (most internal
    // Backbone events have 3 arguments).
    var triggerEvents = function (events, args) {
        var ev, i = -1, l = events.length, a1 = args[0], a2 = args[1], a3 = args[2];
        switch (args.length) {
            case 0: while (++i < l) (ev = events[i]).callback.call(ev.ctx); return;
            case 1: while (++i < l) (ev = events[i]).callback.call(ev.ctx, a1); return;
            case 2: while (++i < l) (ev = events[i]).callback.call(ev.ctx, a1, a2); return;
            case 3: while (++i < l) (ev = events[i]).callback.call(ev.ctx, a1, a2, a3); return;
            default: while (++i < l) (ev = events[i]).callback.apply(ev.ctx, args); return;
        }
    };

    // Aliases for backwards compatibility.
    Events.bind = Events.on;
    Events.unbind = Events.off;

    // Allow the `Backbone` object to serve as a global event bus, for folks who
    // want global "pubsub" in a convenient place.
    _.extend(Backbone, Events);

    // Backbone.Model
    // --------------

    // Backbone **Models** are the basic data object in the framework --
    // frequently representing a row in a table in a database on your server.
    // A discrete chunk of data and a bunch of useful, related methods for
    // performing computations and transformations on that data.

    // Create a new model with the specified attributes. A client id (`cid`)
    // is automatically generated and assigned for you.
    var Model = Backbone.Model = function (attributes, options) {
        var attrs = attributes || {};
        options || (options = {});
        this.cid = _.uniqueId(this.cidPrefix);
        this.attributes = {};
        if (options.collection) this.collection = options.collection;
        if (options.parse) attrs = this.parse(attrs, options) || {};
        var defaults = _.result(this, 'defaults');
        attrs = _.defaults(_.extend({}, defaults, attrs), defaults);
        this.set(attrs, options);
        this.changed = {};
        this.initialize.apply(this, arguments);
    };

    // Attach all inheritable methods to the Model prototype.
    _.extend(Model.prototype, Events, {

        // A hash of attributes whose current and previous value differ.
        changed: null,

        // The value returned during the last failed validation.
        validationError: null,

        // The default name for the JSON `id` attribute is `"id"`. MongoDB and
        // CouchDB users may want to set this to `"_id"`.
        idAttribute: 'id',

        // The prefix is used to create the client id which is used to identify models locally.
        // You may want to override this if you're experiencing name clashes with model ids.
        cidPrefix: 'c',

        // Initialize is an empty function by default. Override it with your own
        // initialization logic.
        initialize: function () { },

        // Return a copy of the model's `attributes` object.
        toJSON: function (options) {
            return _.clone(this.attributes);
        },

        // Proxy `Backbone.sync` by default -- but override this if you need
        // custom syncing semantics for *this* particular model.
        sync: function () {
            return Backbone.sync.apply(this, arguments);
        },

        // Get the value of an attribute.
        get: function (attr) {
            return this.attributes[attr];
        },

        // Get the HTML-escaped value of an attribute.
        escape: function (attr) {
            return _.escape(this.get(attr));
        },

        // Returns `true` if the attribute contains a value that is not null
        // or undefined.
        has: function (attr) {
            return this.get(attr) != null;
        },

        // Special-cased proxy to underscore's `_.matches` method.
        matches: function (attrs) {
            return !!_.iteratee(attrs, this)(this.attributes);
        },

        // Set a hash of model attributes on the object, firing `"change"`. This is
        // the core primitive operation of a model, updating the data and notifying
        // anyone who needs to know about the change in state. The heart of the beast.
        set: function (key, val, options) {
            if (key == null) return this;

            // Handle both `"key", value` and `{key: value}` -style arguments.
            var attrs;
            if (typeof key === 'object') {
                attrs = key;
                options = val;
            } else {
                (attrs = {})[key] = val;
            }

            options || (options = {});

            // Run validation.
            if (!this._validate(attrs, options)) return false;

            // Extract attributes and options.
            var unset = options.unset;
            var silent = options.silent;
            var changes = [];
            var changing = this._changing;
            this._changing = true;

            if (!changing) {
                this._previousAttributes = _.clone(this.attributes);
                this.changed = {};
            }

            var current = this.attributes;
            var changed = this.changed;
            var prev = this._previousAttributes;

            // For each `set` attribute, update or delete the current value.
            for (var attr in attrs) {
                val = attrs[attr];
                if (!_.isEqual(current[attr], val)) changes.push(attr);
                if (!_.isEqual(prev[attr], val)) {
                    changed[attr] = val;
                } else {
                    delete changed[attr];
                }
                unset ? delete current[attr] : current[attr] = val;
            }

            // Update the `id`.
            if (this.idAttribute in attrs) this.id = this.get(this.idAttribute);

            // Trigger all relevant attribute changes.
            if (!silent) {
                if (changes.length) this._pending = options;
                for (var i = 0; i < changes.length; i++) {
                    this.trigger('change:' + changes[i], this, current[changes[i]], options);
                }
            }

            // You might be wondering why there's a `while` loop here. Changes can
            // be recursively nested within `"change"` events.
            if (changing) return this;
            if (!silent) {
                while (this._pending) {
                    options = this._pending;
                    this._pending = false;
                    this.trigger('change', this, options);
                }
            }
            this._pending = false;
            this._changing = false;
            return this;
        },

        // Remove an attribute from the model, firing `"change"`. `unset` is a noop
        // if the attribute doesn't exist.
        unset: function (attr, options) {
            return this.set(attr, void 0, _.extend({}, options, { unset: true }));
        },

        // Clear all attributes on the model, firing `"change"`.
        clear: function (options) {
            var attrs = {};
            for (var key in this.attributes) attrs[key] = void 0;
            return this.set(attrs, _.extend({}, options, { unset: true }));
        },

        // Determine if the model has changed since the last `"change"` event.
        // If you specify an attribute name, determine if that attribute has changed.
        hasChanged: function (attr) {
            if (attr == null) return !_.isEmpty(this.changed);
            return _.has(this.changed, attr);
        },

        // Return an object containing all the attributes that have changed, or
        // false if there are no changed attributes. Useful for determining what
        // parts of a view need to be updated and/or what attributes need to be
        // persisted to the server. Unset attributes will be set to undefined.
        // You can also pass an attributes object to diff against the model,
        // determining if there *would be* a change.
        changedAttributes: function (diff) {
            if (!diff) return this.hasChanged() ? _.clone(this.changed) : false;
            var old = this._changing ? this._previousAttributes : this.attributes;
            var changed = {};
            for (var attr in diff) {
                var val = diff[attr];
                if (_.isEqual(old[attr], val)) continue;
                changed[attr] = val;
            }
            return _.size(changed) ? changed : false;
        },

        // Get the previous value of an attribute, recorded at the time the last
        // `"change"` event was fired.
        previous: function (attr) {
            if (attr == null || !this._previousAttributes) return null;
            return this._previousAttributes[attr];
        },

        // Get all of the attributes of the model at the time of the previous
        // `"change"` event.
        previousAttributes: function () {
            return _.clone(this._previousAttributes);
        },

        // Fetch the model from the server, merging the response with the model's
        // local attributes. Any changed attributes will trigger a "change" event.
        fetch: function (options) {
            options = _.extend({ parse: true }, options);
            var model = this;
            var success = options.success;
            options.success = function (resp) {
                var serverAttrs = options.parse ? model.parse(resp, options) : resp;
                if (!model.set(serverAttrs, options)) return false;
                if (success) success.call(options.context, model, resp, options);
                model.trigger('sync', model, resp, options);
            };
            wrapError(this, options);
            return this.sync('read', this, options);
        },

        // Set a hash of model attributes, and sync the model to the server.
        // If the server returns an attributes hash that differs, the model's
        // state will be `set` again.
        save: function (key, val, options) {
            // Handle both `"key", value` and `{key: value}` -style arguments.
            var attrs;
            if (key == null || typeof key === 'object') {
                attrs = key;
                options = val;
            } else {
                (attrs = {})[key] = val;
            }

            options = _.extend({ validate: true, parse: true }, options);
            var wait = options.wait;

            // If we're not waiting and attributes exist, save acts as
            // `set(attr).save(null, opts)` with validation. Otherwise, check if
            // the model will be valid when the attributes, if any, are set.
            if (attrs && !wait) {
                if (!this.set(attrs, options)) return false;
            } else if (!this._validate(attrs, options)) {
                return false;
            }

            // After a successful server-side save, the client is (optionally)
            // updated with the server-side state.
            var model = this;
            var success = options.success;
            var attributes = this.attributes;
            options.success = function (resp) {
                // Ensure attributes are restored during synchronous saves.
                model.attributes = attributes;
                var serverAttrs = options.parse ? model.parse(resp, options) : resp;
                if (wait) serverAttrs = _.extend({}, attrs, serverAttrs);
                if (serverAttrs && !model.set(serverAttrs, options)) return false;
                if (success) success.call(options.context, model, resp, options);
                model.trigger('sync', model, resp, options);
            };
            wrapError(this, options);

            // Set temporary attributes if `{wait: true}` to properly find new ids.
            if (attrs && wait) this.attributes = _.extend({}, attributes, attrs);

            var method = this.isNew() ? 'create' : (options.patch ? 'patch' : 'update');
            if (method === 'patch' && !options.attrs) options.attrs = attrs;
            var xhr = this.sync(method, this, options);

            // Restore attributes.
            this.attributes = attributes;

            return xhr;
        },

        // Destroy this model on the server if it was already persisted.
        // Optimistically removes the model from its collection, if it has one.
        // If `wait: true` is passed, waits for the server to respond before removal.
        destroy: function (options) {
            options = options ? _.clone(options) : {};
            var model = this;
            var success = options.success;
            var wait = options.wait;

            var destroy = function () {
                model.stopListening();
                model.trigger('destroy', model, model.collection, options);
            };

            options.success = function (resp) {
                if (wait) destroy();
                if (success) success.call(options.context, model, resp, options);
                if (!model.isNew()) model.trigger('sync', model, resp, options);
            };

            var xhr = false;
            if (this.isNew()) {
                _.defer(options.success);
            } else {
                wrapError(this, options);
                xhr = this.sync('delete', this, options);
            }
            if (!wait) destroy();
            return xhr;
        },

        // Default URL for the model's representation on the server -- if you're
        // using Backbone's restful methods, override this to change the endpoint
        // that will be called.
        url: function () {
            var base =
              _.result(this, 'urlRoot') ||
              _.result(this.collection, 'url') ||
              urlError();
            if (this.isNew()) return base;
            var id = this.get(this.idAttribute);
            return base.replace(/[^\/]$/, '$&/') + encodeURIComponent(id);
        },

        // **parse** converts a response into the hash of attributes to be `set` on
        // the model. The default implementation is just to pass the response along.
        parse: function (resp, options) {
            return resp;
        },

        // Create a new model with identical attributes to this one.
        clone: function () {
            return new this.constructor(this.attributes);
        },

        // A model is new if it has never been saved to the server, and lacks an id.
        isNew: function () {
            return !this.has(this.idAttribute);
        },

        // Check if the model is currently in a valid state.
        isValid: function (options) {
            return this._validate({}, _.extend({}, options, { validate: true }));
        },

        // Run validation against the next complete set of model attributes,
        // returning `true` if all is well. Otherwise, fire an `"invalid"` event.
        _validate: function (attrs, options) {
            if (!options.validate || !this.validate) return true;
            attrs = _.extend({}, this.attributes, attrs);
            var error = this.validationError = this.validate(attrs, options) || null;
            if (!error) return true;
            this.trigger('invalid', this, error, _.extend(options, { validationError: error }));
            return false;
        }

    });

    // Underscore methods that we want to implement on the Model, mapped to the
    // number of arguments they take.
    var modelMethods = {
        keys: 1, values: 1, pairs: 1, invert: 1, pick: 0,
        omit: 0, chain: 1, isEmpty: 1
    };

    // Mix in each Underscore method as a proxy to `Model#attributes`.
    addUnderscoreMethods(Model, modelMethods, 'attributes');

    // Backbone.Collection
    // -------------------

    // If models tend to represent a single row of data, a Backbone Collection is
    // more analogous to a table full of data ... or a small slice or page of that
    // table, or a collection of rows that belong together for a particular reason
    // -- all of the messages in this particular folder, all of the documents
    // belonging to this particular author, and so on. Collections maintain
    // indexes of their models, both in order, and for lookup by `id`.

    // Create a new **Collection**, perhaps to contain a specific type of `model`.
    // If a `comparator` is specified, the Collection will maintain
    // its models in sort order, as they're added and removed.
    var Collection = Backbone.Collection = function (models, options) {
        options || (options = {});
        if (options.model) this.model = options.model;
        if (options.comparator !== void 0) this.comparator = options.comparator;
        this._reset();
        this.initialize.apply(this, arguments);
        if (models) this.reset(models, _.extend({ silent: true }, options));
    };

    // Default options for `Collection#set`.
    var setOptions = { add: true, remove: true, merge: true };
    var addOptions = { add: true, remove: false };

    // Splices `insert` into `array` at index `at`.
    var splice = function (array, insert, at) {
        at = Math.min(Math.max(at, 0), array.length);
        var tail = Array(array.length - at);
        var length = insert.length;
        var i;
        for (i = 0; i < tail.length; i++) tail[i] = array[i + at];
        for (i = 0; i < length; i++) array[i + at] = insert[i];
        for (i = 0; i < tail.length; i++) array[i + length + at] = tail[i];
    };

    // Define the Collection's inheritable methods.
    _.extend(Collection.prototype, Events, {

        // The default model for a collection is just a **Backbone.Model**.
        // This should be overridden in most cases.
        model: Model,

        // Initialize is an empty function by default. Override it with your own
        // initialization logic.
        initialize: function () { },

        // The JSON representation of a Collection is an array of the
        // models' attributes.
        toJSON: function (options) {
            return this.map(function (model) { return model.toJSON(options); });
        },

        // Proxy `Backbone.sync` by default.
        sync: function () {
            return Backbone.sync.apply(this, arguments);
        },

        // Add a model, or list of models to the set. `models` may be Backbone
        // Models or raw JavaScript objects to be converted to Models, or any
        // combination of the two.
        add: function (models, options) {
            return this.set(models, _.extend({ merge: false }, options, addOptions));
        },

        // Remove a model, or a list of models from the set.
        remove: function (models, options) {
            options = _.extend({}, options);
            var singular = !_.isArray(models);
            models = singular ? [models] : models.slice();
            var removed = this._removeModels(models, options);
            if (!options.silent && removed.length) {
                options.changes = { added: [], merged: [], removed: removed };
                this.trigger('update', this, options);
            }
            return singular ? removed[0] : removed;
        },

        // Update a collection by `set`-ing a new list of models, adding new ones,
        // removing models that are no longer present, and merging models that
        // already exist in the collection, as necessary. Similar to **Model#set**,
        // the core operation for updating the data contained by the collection.
        set: function (models, options) {
            if (models == null) return;

            options = _.extend({}, setOptions, options);
            if (options.parse && !this._isModel(models)) {
                models = this.parse(models, options) || [];
            }

            var singular = !_.isArray(models);
            models = singular ? [models] : models.slice();

            var at = options.at;
            if (at != null) at = +at;
            if (at > this.length) at = this.length;
            if (at < 0) at += this.length + 1;

            var set = [];
            var toAdd = [];
            var toMerge = [];
            var toRemove = [];
            var modelMap = {};

            var add = options.add;
            var merge = options.merge;
            var remove = options.remove;

            var sort = false;
            var sortable = this.comparator && at == null && options.sort !== false;
            var sortAttr = _.isString(this.comparator) ? this.comparator : null;

            // Turn bare objects into model references, and prevent invalid models
            // from being added.
            var model, i;
            for (i = 0; i < models.length; i++) {
                model = models[i];

                // If a duplicate is found, prevent it from being added and
                // optionally merge it into the existing model.
                var existing = this.get(model);
                if (existing) {
                    if (merge && model !== existing) {
                        var attrs = this._isModel(model) ? model.attributes : model;
                        if (options.parse) attrs = existing.parse(attrs, options);
                        existing.set(attrs, options);
                        toMerge.push(existing);
                        if (sortable && !sort) sort = existing.hasChanged(sortAttr);
                    }
                    if (!modelMap[existing.cid]) {
                        modelMap[existing.cid] = true;
                        set.push(existing);
                    }
                    models[i] = existing;

                    // If this is a new, valid model, push it to the `toAdd` list.
                } else if (add) {
                    model = models[i] = this._prepareModel(model, options);
                    if (model) {
                        toAdd.push(model);
                        this._addReference(model, options);
                        modelMap[model.cid] = true;
                        set.push(model);
                    }
                }
            }

            // Remove stale models.
            if (remove) {
                for (i = 0; i < this.length; i++) {
                    model = this.models[i];
                    if (!modelMap[model.cid]) toRemove.push(model);
                }
                if (toRemove.length) this._removeModels(toRemove, options);
            }

            // See if sorting is needed, update `length` and splice in new models.
            var orderChanged = false;
            var replace = !sortable && add && remove;
            if (set.length && replace) {
                orderChanged = this.length !== set.length || _.some(this.models, function (m, index) {
                    return m !== set[index];
                });
                this.models.length = 0;
                splice(this.models, set, 0);
                this.length = this.models.length;
            } else if (toAdd.length) {
                if (sortable) sort = true;
                splice(this.models, toAdd, at == null ? this.length : at);
                this.length = this.models.length;
            }

            // Silently sort the collection if appropriate.
            if (sort) this.sort({ silent: true });

            // Unless silenced, it's time to fire all appropriate add/sort/update events.
            if (!options.silent) {
                for (i = 0; i < toAdd.length; i++) {
                    if (at != null) options.index = at + i;
                    model = toAdd[i];
                    model.trigger('add', model, this, options);
                }
                if (sort || orderChanged) this.trigger('sort', this, options);
                if (toAdd.length || toRemove.length || toMerge.length) {
                    options.changes = {
                        added: toAdd,
                        removed: toRemove,
                        merged: toMerge
                    };
                    this.trigger('update', this, options);
                }
            }

            // Return the added (or merged) model (or models).
            return singular ? models[0] : models;
        },

        // When you have more items than you want to add or remove individually,
        // you can reset the entire set with a new list of models, without firing
        // any granular `add` or `remove` events. Fires `reset` when finished.
        // Useful for bulk operations and optimizations.
        reset: function (models, options) {
            options = options ? _.clone(options) : {};
            for (var i = 0; i < this.models.length; i++) {
                this._removeReference(this.models[i], options);
            }
            options.previousModels = this.models;
            this._reset();
            models = this.add(models, _.extend({ silent: true }, options));
            if (!options.silent) this.trigger('reset', this, options);
            return models;
        },

        // Add a model to the end of the collection.
        push: function (model, options) {
            return this.add(model, _.extend({ at: this.length }, options));
        },

        // Remove a model from the end of the collection.
        pop: function (options) {
            var model = this.at(this.length - 1);
            return this.remove(model, options);
        },

        // Add a model to the beginning of the collection.
        unshift: function (model, options) {
            return this.add(model, _.extend({ at: 0 }, options));
        },

        // Remove a model from the beginning of the collection.
        shift: function (options) {
            var model = this.at(0);
            return this.remove(model, options);
        },

        // Slice out a sub-array of models from the collection.
        slice: function () {
            return slice.apply(this.models, arguments);
        },

        // Get a model from the set by id, cid, model object with id or cid
        // properties, or an attributes object that is transformed through modelId.
        get: function (obj) {
            if (obj == null) return void 0;
            return this._byId[obj] ||
              this._byId[this.modelId(obj.attributes || obj)] ||
              obj.cid && this._byId[obj.cid];
        },

        // Returns `true` if the model is in the collection.
        has: function (obj) {
            return this.get(obj) != null;
        },

        // Get the model at the given index.
        at: function (index) {
            if (index < 0) index += this.length;
            return this.models[index];
        },

        // Return models with matching attributes. Useful for simple cases of
        // `filter`.
        where: function (attrs, first) {
            return this[first ? 'find' : 'filter'](attrs);
        },

        // Return the first model with matching attributes. Useful for simple cases
        // of `find`.
        findWhere: function (attrs) {
            return this.where(attrs, true);
        },

        // Force the collection to re-sort itself. You don't need to call this under
        // normal circumstances, as the set will maintain sort order as each item
        // is added.
        sort: function (options) {
            var comparator = this.comparator;
            if (!comparator) throw new Error('Cannot sort a set without a comparator');
            options || (options = {});

            var length = comparator.length;
            if (_.isFunction(comparator)) comparator = _.bind(comparator, this);

            // Run sort based on type of `comparator`.
            if (length === 1 || _.isString(comparator)) {
                this.models = this.sortBy(comparator);
            } else {
                this.models.sort(comparator);
            }
            if (!options.silent) this.trigger('sort', this, options);
            return this;
        },

        // Pluck an attribute from each model in the collection.
        pluck: function (attr) {
            return this.map(attr + '');
        },

        // Fetch the default set of models for this collection, resetting the
        // collection when they arrive. If `reset: true` is passed, the response
        // data will be passed through the `reset` method instead of `set`.
        fetch: function (options) {
            options = _.extend({ parse: true }, options);
            var success = options.success;
            var collection = this;
            options.success = function (resp) {
                var method = options.reset ? 'reset' : 'set';
                collection[method](resp, options);
                if (success) success.call(options.context, collection, resp, options);
                collection.trigger('sync', collection, resp, options);
            };
            wrapError(this, options);
            return this.sync('read', this, options);
        },

        // Create a new instance of a model in this collection. Add the model to the
        // collection immediately, unless `wait: true` is passed, in which case we
        // wait for the server to agree.
        create: function (model, options) {
            options = options ? _.clone(options) : {};
            var wait = options.wait;
            model = this._prepareModel(model, options);
            if (!model) return false;
            if (!wait) this.add(model, options);
            var collection = this;
            var success = options.success;
            options.success = function (m, resp, callbackOpts) {
                if (wait) collection.add(m, callbackOpts);
                if (success) success.call(callbackOpts.context, m, resp, callbackOpts);
            };
            model.save(null, options);
            return model;
        },

        // **parse** converts a response into a list of models to be added to the
        // collection. The default implementation is just to pass it through.
        parse: function (resp, options) {
            return resp;
        },

        // Create a new collection with an identical list of models as this one.
        clone: function () {
            return new this.constructor(this.models, {
                model: this.model,
                comparator: this.comparator
            });
        },

        // Define how to uniquely identify models in the collection.
        modelId: function (attrs) {
            return attrs[this.model.prototype.idAttribute || 'id'];
        },

        // Private method to reset all internal state. Called when the collection
        // is first initialized or reset.
        _reset: function () {
            this.length = 0;
            this.models = [];
            this._byId = {};
        },

        // Prepare a hash of attributes (or other model) to be added to this
        // collection.
        _prepareModel: function (attrs, options) {
            if (this._isModel(attrs)) {
                if (!attrs.collection) attrs.collection = this;
                return attrs;
            }
            options = options ? _.clone(options) : {};
            options.collection = this;
            var model = new this.model(attrs, options);
            if (!model.validationError) return model;
            this.trigger('invalid', this, model.validationError, options);
            return false;
        },

        // Internal method called by both remove and set.
        _removeModels: function (models, options) {
            var removed = [];
            for (var i = 0; i < models.length; i++) {
                var model = this.get(models[i]);
                if (!model) continue;

                var index = this.indexOf(model);
                this.models.splice(index, 1);
                this.length--;

                // Remove references before triggering 'remove' event to prevent an
                // infinite loop. #3693
                delete this._byId[model.cid];
                var id = this.modelId(model.attributes);
                if (id != null) delete this._byId[id];

                if (!options.silent) {
                    options.index = index;
                    model.trigger('remove', model, this, options);
                }

                removed.push(model);
                this._removeReference(model, options);
            }
            return removed;
        },

        // Method for checking whether an object should be considered a model for
        // the purposes of adding to the collection.
        _isModel: function (model) {
            return model instanceof Model;
        },

        // Internal method to create a model's ties to a collection.
        _addReference: function (model, options) {
            this._byId[model.cid] = model;
            var id = this.modelId(model.attributes);
            if (id != null) this._byId[id] = model;
            model.on('all', this._onModelEvent, this);
        },

        // Internal method to sever a model's ties to a collection.
        _removeReference: function (model, options) {
            delete this._byId[model.cid];
            var id = this.modelId(model.attributes);
            if (id != null) delete this._byId[id];
            if (this === model.collection) delete model.collection;
            model.off('all', this._onModelEvent, this);
        },

        // Internal method called every time a model in the set fires an event.
        // Sets need to update their indexes when models change ids. All other
        // events simply proxy through. "add" and "remove" events that originate
        // in other collections are ignored.
        _onModelEvent: function (event, model, collection, options) {
            if (model) {
                if ((event === 'add' || event === 'remove') && collection !== this) return;
                if (event === 'destroy') this.remove(model, options);
                if (event === 'change') {
                    var prevId = this.modelId(model.previousAttributes());
                    var id = this.modelId(model.attributes);
                    if (prevId !== id) {
                        if (prevId != null) delete this._byId[prevId];
                        if (id != null) this._byId[id] = model;
                    }
                }
            }
            this.trigger.apply(this, arguments);
        }

    });

    // Underscore methods that we want to implement on the Collection.
    // 90% of the core usefulness of Backbone Collections is actually implemented
    // right here:
    var collectionMethods = {
        forEach: 3, each: 3, map: 3, collect: 3, reduce: 0,
        foldl: 0, inject: 0, reduceRight: 0, foldr: 0, find: 3, detect: 3, filter: 3,
        select: 3, reject: 3, every: 3, all: 3, some: 3, any: 3, include: 3, includes: 3,
        contains: 3, invoke: 0, max: 3, min: 3, toArray: 1, size: 1, first: 3,
        head: 3, take: 3, initial: 3, rest: 3, tail: 3, drop: 3, last: 3,
        without: 0, difference: 0, indexOf: 3, shuffle: 1, lastIndexOf: 3,
        isEmpty: 1, chain: 1, sample: 3, partition: 3, groupBy: 3, countBy: 3,
        sortBy: 3, indexBy: 3, findIndex: 3, findLastIndex: 3
    };

    // Mix in each Underscore method as a proxy to `Collection#models`.
    addUnderscoreMethods(Collection, collectionMethods, 'models');

    // Backbone.View
    // -------------

    // Backbone Views are almost more convention than they are actual code. A View
    // is simply a JavaScript object that represents a logical chunk of UI in the
    // DOM. This might be a single item, an entire list, a sidebar or panel, or
    // even the surrounding frame which wraps your whole app. Defining a chunk of
    // UI as a **View** allows you to define your DOM events declaratively, without
    // having to worry about render order ... and makes it easy for the view to
    // react to specific changes in the state of your models.

    // Creating a Backbone.View creates its initial element outside of the DOM,
    // if an existing element is not provided...
    var View = Backbone.View = function (options) {
        this.cid = _.uniqueId('view');
        _.extend(this, _.pick(options, viewOptions));
        this._ensureElement();
        this.initialize.apply(this, arguments);
    };

    // Cached regex to split keys for `delegate`.
    var delegateEventSplitter = /^(\S+)\s*(.*)$/;

    // List of view options to be set as properties.
    var viewOptions = ['model', 'collection', 'el', 'id', 'attributes', 'className', 'tagName', 'events'];

    // Set up all inheritable **Backbone.View** properties and methods.
    _.extend(View.prototype, Events, {

        // The default `tagName` of a View's element is `"div"`.
        tagName: 'div',

        // jQuery delegate for element lookup, scoped to DOM elements within the
        // current view. This should be preferred to global lookups where possible.
        $: function (selector) {
            return this.$el.find(selector);
        },

        // Initialize is an empty function by default. Override it with your own
        // initialization logic.
        initialize: function () { },

        // **render** is the core function that your view should override, in order
        // to populate its element (`this.el`), with the appropriate HTML. The
        // convention is for **render** to always return `this`.
        render: function () {
            return this;
        },

        // Remove this view by taking the element out of the DOM, and removing any
        // applicable Backbone.Events listeners.
        remove: function () {
            this._removeElement();
            this.stopListening();
            return this;
        },

        // Remove this view's element from the document and all event listeners
        // attached to it. Exposed for subclasses using an alternative DOM
        // manipulation API.
        _removeElement: function () {
            this.$el.remove();
        },

        // Change the view's element (`this.el` property) and re-delegate the
        // view's events on the new element.
        setElement: function (element) {
            this.undelegateEvents();
            this._setElement(element);
            this.delegateEvents();
            return this;
        },

        // Creates the `this.el` and `this.$el` references for this view using the
        // given `el`. `el` can be a CSS selector or an HTML string, a jQuery
        // context or an element. Subclasses can override this to utilize an
        // alternative DOM manipulation API and are only required to set the
        // `this.el` property.
        _setElement: function (el) {
            this.$el = el instanceof Backbone.$ ? el : Backbone.$(el);
            this.el = this.$el[0];
        },

        // Set callbacks, where `this.events` is a hash of
        //
        // *{"event selector": "callback"}*
        //
        //     {
        //       'mousedown .title':  'edit',
        //       'click .button':     'save',
        //       'click .open':       function(e) { ... }
        //     }
        //
        // pairs. Callbacks will be bound to the view, with `this` set properly.
        // Uses event delegation for efficiency.
        // Omitting the selector binds the event to `this.el`.
        delegateEvents: function (events) {
            events || (events = _.result(this, 'events'));
            if (!events) return this;
            this.undelegateEvents();
            for (var key in events) {
                var method = events[key];
                if (!_.isFunction(method)) method = this[method];
                if (!method) continue;
                var match = key.match(delegateEventSplitter);
                this.delegate(match[1], match[2], _.bind(method, this));
            }
            return this;
        },

        // Add a single event listener to the view's element (or a child element
        // using `selector`). This only works for delegate-able events: not `focus`,
        // `blur`, and not `change`, `submit`, and `reset` in Internet Explorer.
        delegate: function (eventName, selector, listener) {
            this.$el.on(eventName + '.delegateEvents' + this.cid, selector, listener);
            return this;
        },

        // Clears all callbacks previously bound to the view by `delegateEvents`.
        // You usually don't need to use this, but may wish to if you have multiple
        // Backbone views attached to the same DOM element.
        undelegateEvents: function () {
            if (this.$el) this.$el.off('.delegateEvents' + this.cid);
            return this;
        },

        // A finer-grained `undelegateEvents` for removing a single delegated event.
        // `selector` and `listener` are both optional.
        undelegate: function (eventName, selector, listener) {
            this.$el.off(eventName + '.delegateEvents' + this.cid, selector, listener);
            return this;
        },

        // Produces a DOM element to be assigned to your view. Exposed for
        // subclasses using an alternative DOM manipulation API.
        _createElement: function (tagName) {
            return document.createElement(tagName);
        },

        // Ensure that the View has a DOM element to render into.
        // If `this.el` is a string, pass it through `$()`, take the first
        // matching element, and re-assign it to `el`. Otherwise, create
        // an element from the `id`, `className` and `tagName` properties.
        _ensureElement: function () {
            if (!this.el) {
                var attrs = _.extend({}, _.result(this, 'attributes'));
                if (this.id) attrs.id = _.result(this, 'id');
                if (this.className) attrs['class'] = _.result(this, 'className');
                this.setElement(this._createElement(_.result(this, 'tagName')));
                this._setAttributes(attrs);
            } else {
                this.setElement(_.result(this, 'el'));
            }
        },

        // Set attributes from a hash on this view's element.  Exposed for
        // subclasses using an alternative DOM manipulation API.
        _setAttributes: function (attributes) {
            this.$el.attr(attributes);
        }

    });

    // Backbone.sync
    // -------------

    // Override this function to change the manner in which Backbone persists
    // models to the server. You will be passed the type of request, and the
    // model in question. By default, makes a RESTful Ajax request
    // to the model's `url()`. Some possible customizations could be:
    //
    // * Use `setTimeout` to batch rapid-fire updates into a single request.
    // * Send up the models as XML instead of JSON.
    // * Persist models via WebSockets instead of Ajax.
    //
    // Turn on `Backbone.emulateHTTP` in order to send `PUT` and `DELETE` requests
    // as `POST`, with a `_method` parameter containing the true HTTP method,
    // as well as all requests with the body as `application/x-www-form-urlencoded`
    // instead of `application/json` with the model in a param named `model`.
    // Useful when interfacing with server-side languages like **PHP** that make
    // it difficult to read the body of `PUT` requests.
    Backbone.sync = function (method, model, options) {
        var type = methodMap[method];

        // Default options, unless specified.
        _.defaults(options || (options = {}), {
            emulateHTTP: Backbone.emulateHTTP,
            emulateJSON: Backbone.emulateJSON
        });

        // Default JSON-request options.
        var params = { type: type, dataType: 'json' };

        // Ensure that we have a URL.
        if (!options.url) {
            params.url = _.result(model, 'url') || urlError();
        }

        // Ensure that we have the appropriate request data.
        if (options.data == null && model && (method === 'create' || method === 'update' || method === 'patch')) {
            params.contentType = 'application/json';
            params.data = JSON.stringify(options.attrs || model.toJSON(options));
        }

        // For older servers, emulate JSON by encoding the request into an HTML-form.
        if (options.emulateJSON) {
            params.contentType = 'application/x-www-form-urlencoded';
            params.data = params.data ? { model: params.data } : {};
        }

        // For older servers, emulate HTTP by mimicking the HTTP method with `_method`
        // And an `X-HTTP-Method-Override` header.
        if (options.emulateHTTP && (type === 'PUT' || type === 'DELETE' || type === 'PATCH')) {
            params.type = 'POST';
            if (options.emulateJSON) params.data._method = type;
            var beforeSend = options.beforeSend;
            options.beforeSend = function (xhr) {
                xhr.setRequestHeader('X-HTTP-Method-Override', type);
                if (beforeSend) return beforeSend.apply(this, arguments);
            };
        }

        // Don't process data on a non-GET request.
        if (params.type !== 'GET' && !options.emulateJSON) {
            params.processData = false;
        }

        // Pass along `textStatus` and `errorThrown` from jQuery.
        var error = options.error;
        options.error = function (xhr, textStatus, errorThrown) {
            options.textStatus = textStatus;
            options.errorThrown = errorThrown;
            if (error) error.call(options.context, xhr, textStatus, errorThrown);
        };

        // Make the request, allowing the user to override any Ajax options.
        var xhr = options.xhr = Backbone.ajax(_.extend(params, options));
        model.trigger('request', model, xhr, options);
        return xhr;
    };

    // Map from CRUD to HTTP for our default `Backbone.sync` implementation.
    var methodMap = {
        'create': 'POST',
        'update': 'PUT',
        'patch': 'PATCH',
        'delete': 'DELETE',
        'read': 'GET'
    };

    // Set the default implementation of `Backbone.ajax` to proxy through to `$`.
    // Override this if you'd like to use a different library.
    Backbone.ajax = function () {
        return Backbone.$.ajax.apply(Backbone.$, arguments);
    };

    // Backbone.Router
    // ---------------

    // Routers map faux-URLs to actions, and fire events when routes are
    // matched. Creating a new one sets its `routes` hash, if not set statically.
    var Router = Backbone.Router = function (options) {
        options || (options = {});
        if (options.routes) this.routes = options.routes;
        this._bindRoutes();
        this.initialize.apply(this, arguments);
    };

    // Cached regular expressions for matching named param parts and splatted
    // parts of route strings.
    var optionalParam = /\((.*?)\)/g;
    var namedParam = /(\(\?)?:\w+/g;
    var splatParam = /\*\w+/g;
    var escapeRegExp = /[\-{}\[\]+?.,\\\^$|#\s]/g;

    // Set up all inheritable **Backbone.Router** properties and methods.
    _.extend(Router.prototype, Events, {

        // Initialize is an empty function by default. Override it with your own
        // initialization logic.
        initialize: function () { },

        // Manually bind a single named route to a callback. For example:
        //
        //     this.route('search/:query/p:num', 'search', function(query, num) {
        //       ...
        //     });
        //
        route: function (route, name, callback) {
            if (!_.isRegExp(route)) route = this._routeToRegExp(route);
            if (_.isFunction(name)) {
                callback = name;
                name = '';
            }
            if (!callback) callback = this[name];
            var router = this;
            Backbone.history.route(route, function (fragment) {
                var args = router._extractParameters(route, fragment);
                if (router.execute(callback, args, name) !== false) {
                    router.trigger.apply(router, ['route:' + name].concat(args));
                    router.trigger('route', name, args);
                    Backbone.history.trigger('route', router, name, args);
                }
            });
            return this;
        },

        // Execute a route handler with the provided parameters.  This is an
        // excellent place to do pre-route setup or post-route cleanup.
        execute: function (callback, args, name) {
            if (callback) callback.apply(this, args);
        },

        // Simple proxy to `Backbone.history` to save a fragment into the history.
        navigate: function (fragment, options) {
            Backbone.history.navigate(fragment, options);
            return this;
        },

        // Bind all defined routes to `Backbone.history`. We have to reverse the
        // order of the routes here to support behavior where the most general
        // routes can be defined at the bottom of the route map.
        _bindRoutes: function () {
            if (!this.routes) return;
            this.routes = _.result(this, 'routes');
            var route, routes = _.keys(this.routes);
            while ((route = routes.pop()) != null) {
                this.route(route, this.routes[route]);
            }
        },

        // Convert a route string into a regular expression, suitable for matching
        // against the current location hash.
        _routeToRegExp: function (route) {
            route = route.replace(escapeRegExp, '\\$&')
                         .replace(optionalParam, '(?:$1)?')
                         .replace(namedParam, function (match, optional) {
                             return optional ? match : '([^/?]+)';
                         })
                         .replace(splatParam, '([^?]*?)');
            return new RegExp('^' + route + '(?:\\?([\\s\\S]*))?$');
        },

        // Given a route, and a URL fragment that it matches, return the array of
        // extracted decoded parameters. Empty or unmatched parameters will be
        // treated as `null` to normalize cross-browser behavior.
        _extractParameters: function (route, fragment) {
            var params = route.exec(fragment).slice(1);
            return _.map(params, function (param, i) {
                // Don't decode the search params.
                if (i === params.length - 1) return param || null;
                return param ? decodeURIComponent(param) : null;
            });
        }

    });

    // Backbone.History
    // ----------------

    // Handles cross-browser history management, based on either
    // [pushState](http://diveintohtml5.info/history.html) and real URLs, or
    // [onhashchange](https://developer.mozilla.org/en-US/docs/DOM/window.onhashchange)
    // and URL fragments. If the browser supports neither (old IE, natch),
    // falls back to polling.
    var History = Backbone.History = function () {
        this.handlers = [];
        this.checkUrl = _.bind(this.checkUrl, this);

        // Ensure that `History` can be used outside of the browser.
        if (typeof window !== 'undefined') {
            this.location = window.location;
            this.history = window.history;
        }
    };

    // Cached regex for stripping a leading hash/slash and trailing space.
    var routeStripper = /^[#\/]|\s+$/g;

    // Cached regex for stripping leading and trailing slashes.
    var rootStripper = /^\/+|\/+$/g;

    // Cached regex for stripping urls of hash.
    var pathStripper = /#.*$/;

    // Has the history handling already been started?
    History.started = false;

    // Set up all inheritable **Backbone.History** properties and methods.
    _.extend(History.prototype, Events, {

        // The default interval to poll for hash changes, if necessary, is
        // twenty times a second.
        interval: 50,

        // Are we at the app root?
        atRoot: function () {
            var path = this.location.pathname.replace(/[^\/]$/, '$&/');
            return path === this.root && !this.getSearch();
        },

        // Does the pathname match the root?
        matchRoot: function () {
            var path = this.decodeFragment(this.location.pathname);
            var rootPath = path.slice(0, this.root.length - 1) + '/';
            return rootPath === this.root;
        },

        // Unicode characters in `location.pathname` are percent encoded so they're
        // decoded for comparison. `%25` should not be decoded since it may be part
        // of an encoded parameter.
        decodeFragment: function (fragment) {
            return decodeURI(fragment.replace(/%25/g, '%2525'));
        },

        // In IE6, the hash fragment and search params are incorrect if the
        // fragment contains `?`.
        getSearch: function () {
            var match = this.location.href.replace(/#.*/, '').match(/\?.+/);
            return match ? match[0] : '';
        },

        // Gets the true hash value. Cannot use location.hash directly due to bug
        // in Firefox where location.hash will always be decoded.
        getHash: function (window) {
            var match = (window || this).location.href.match(/#(.*)$/);
            return match ? match[1] : '';
        },

        // Get the pathname and search params, without the root.
        getPath: function () {
            var path = this.decodeFragment(
              this.location.pathname + this.getSearch()
            ).slice(this.root.length - 1);
            return path.charAt(0) === '/' ? path.slice(1) : path;
        },

        // Get the cross-browser normalized URL fragment from the path or hash.
        getFragment: function (fragment) {
            if (fragment == null) {
                if (this._usePushState || !this._wantsHashChange) {
                    fragment = this.getPath();
                } else {
                    fragment = this.getHash();
                }
            }
            return fragment.replace(routeStripper, '');
        },

        // Start the hash change handling, returning `true` if the current URL matches
        // an existing route, and `false` otherwise.
        start: function (options) {
            if (History.started) throw new Error('Backbone.history has already been started');
            History.started = true;

            // Figure out the initial configuration. Do we need an iframe?
            // Is pushState desired ... is it available?
            this.options = _.extend({ root: '/' }, this.options, options);
            this.root = this.options.root;
            this._wantsHashChange = this.options.hashChange !== false;
            this._hasHashChange = 'onhashchange' in window && (document.documentMode === void 0 || document.documentMode > 7);
            this._useHashChange = this._wantsHashChange && this._hasHashChange;
            this._wantsPushState = !!this.options.pushState;
            this._hasPushState = !!(this.history && this.history.pushState);
            this._usePushState = this._wantsPushState && this._hasPushState;
            this.fragment = this.getFragment();

            // Normalize root to always include a leading and trailing slash.
            this.root = ('/' + this.root + '/').replace(rootStripper, '/');

            // Transition from hashChange to pushState or vice versa if both are
            // requested.
            if (this._wantsHashChange && this._wantsPushState) {

                // If we've started off with a route from a `pushState`-enabled
                // browser, but we're currently in a browser that doesn't support it...
                if (!this._hasPushState && !this.atRoot()) {
                    var rootPath = this.root.slice(0, -1) || '/';
                    this.location.replace(rootPath + '#' + this.getPath());
                    // Return immediately as browser will do redirect to new url
                    return true;

                    // Or if we've started out with a hash-based route, but we're currently
                    // in a browser where it could be `pushState`-based instead...
                } else if (this._hasPushState && this.atRoot()) {
                    this.navigate(this.getHash(), { replace: true });
                }

            }

            // Proxy an iframe to handle location events if the browser doesn't
            // support the `hashchange` event, HTML5 history, or the user wants
            // `hashChange` but not `pushState`.
            if (!this._hasHashChange && this._wantsHashChange && !this._usePushState) {
                this.iframe = document.createElement('iframe');
                this.iframe.src = 'javascript:0';
                this.iframe.style.display = 'none';
                this.iframe.tabIndex = -1;
                var body = document.body;
                // Using `appendChild` will throw on IE < 9 if the document is not ready.
                var iWindow = body.insertBefore(this.iframe, body.firstChild).contentWindow;
                iWindow.document.open();
                iWindow.document.close();
                iWindow.location.hash = '#' + this.fragment;
            }

            // Add a cross-platform `addEventListener` shim for older browsers.
            var addEventListener = window.addEventListener || function (eventName, listener) {
                return attachEvent('on' + eventName, listener);
            };

            // Depending on whether we're using pushState or hashes, and whether
            // 'onhashchange' is supported, determine how we check the URL state.
            if (this._usePushState) {
                addEventListener('popstate', this.checkUrl, false);
            } else if (this._useHashChange && !this.iframe) {
                addEventListener('hashchange', this.checkUrl, false);
            } else if (this._wantsHashChange) {
                this._checkUrlInterval = setInterval(this.checkUrl, this.interval);
            }

            if (!this.options.silent) return this.loadUrl();
        },

        // Disable Backbone.history, perhaps temporarily. Not useful in a real app,
        // but possibly useful for unit testing Routers.
        stop: function () {
            // Add a cross-platform `removeEventListener` shim for older browsers.
            var removeEventListener = window.removeEventListener || function (eventName, listener) {
                return detachEvent('on' + eventName, listener);
            };

            // Remove window listeners.
            if (this._usePushState) {
                removeEventListener('popstate', this.checkUrl, false);
            } else if (this._useHashChange && !this.iframe) {
                removeEventListener('hashchange', this.checkUrl, false);
            }

            // Clean up the iframe if necessary.
            if (this.iframe) {
                document.body.removeChild(this.iframe);
                this.iframe = null;
            }

            // Some environments will throw when clearing an undefined interval.
            if (this._checkUrlInterval) clearInterval(this._checkUrlInterval);
            History.started = false;
        },

        // Add a route to be tested when the fragment changes. Routes added later
        // may override previous routes.
        route: function (route, callback) {
            this.handlers.unshift({ route: route, callback: callback });
        },

        // Checks the current URL to see if it has changed, and if it has,
        // calls `loadUrl`, normalizing across the hidden iframe.
        checkUrl: function (e) {
            var current = this.getFragment();

            // If the user pressed the back button, the iframe's hash will have
            // changed and we should use that for comparison.
            if (current === this.fragment && this.iframe) {
                current = this.getHash(this.iframe.contentWindow);
            }

            if (current === this.fragment) return false;
            if (this.iframe) this.navigate(current);
            this.loadUrl();
        },

        // Attempt to load the current URL fragment. If a route succeeds with a
        // match, returns `true`. If no defined routes matches the fragment,
        // returns `false`.
        loadUrl: function (fragment) {
            // If the root doesn't match, no routes can match either.
            if (!this.matchRoot()) return false;
            fragment = this.fragment = this.getFragment(fragment);
            return _.some(this.handlers, function (handler) {
                if (handler.route.test(fragment)) {
                    handler.callback(fragment);
                    return true;
                }
            });
        },

        // Save a fragment into the hash history, or replace the URL state if the
        // 'replace' option is passed. You are responsible for properly URL-encoding
        // the fragment in advance.
        //
        // The options object can contain `trigger: true` if you wish to have the
        // route callback be fired (not usually desirable), or `replace: true`, if
        // you wish to modify the current URL without adding an entry to the history.
        navigate: function (fragment, options) {
            if (!History.started) return false;
            if (!options || options === true) options = { trigger: !!options };

            // Normalize the fragment.
            fragment = this.getFragment(fragment || '');

            // Don't include a trailing slash on the root.
            var rootPath = this.root;
            if (fragment === '' || fragment.charAt(0) === '?') {
                rootPath = rootPath.slice(0, -1) || '/';
            }
            var url = rootPath + fragment;

            // Strip the hash and decode for matching.
            fragment = this.decodeFragment(fragment.replace(pathStripper, ''));

            if (this.fragment === fragment) return;
            this.fragment = fragment;

            // If pushState is available, we use it to set the fragment as a real URL.
            if (this._usePushState) {
                this.history[options.replace ? 'replaceState' : 'pushState']({}, document.title, url);

                // If hash changes haven't been explicitly disabled, update the hash
                // fragment to store history.
            } else if (this._wantsHashChange) {
                this._updateHash(this.location, fragment, options.replace);
                if (this.iframe && fragment !== this.getHash(this.iframe.contentWindow)) {
                    var iWindow = this.iframe.contentWindow;

                    // Opening and closing the iframe tricks IE7 and earlier to push a
                    // history entry on hash-tag change.  When replace is true, we don't
                    // want this.
                    if (!options.replace) {
                        iWindow.document.open();
                        iWindow.document.close();
                    }

                    this._updateHash(iWindow.location, fragment, options.replace);
                }

                // If you've told us that you explicitly don't want fallback hashchange-
                // based history, then `navigate` becomes a page refresh.
            } else {
                return this.location.assign(url);
            }
            if (options.trigger) return this.loadUrl(fragment);
        },

        // Update the hash location, either replacing the current entry, or adding
        // a new one to the browser history.
        _updateHash: function (location, fragment, replace) {
            if (replace) {
                var href = location.href.replace(/(javascript:|#).*$/, '');
                location.replace(href + '#' + fragment);
            } else {
                // Some browsers require that `hash` contains a leading #.
                location.hash = '#' + fragment;
            }
        }

    });

    // Create the default Backbone.history.
    Backbone.history = new History;

    // Helpers
    // -------

    // Helper function to correctly set up the prototype chain for subclasses.
    // Similar to `goog.inherits`, but uses a hash of prototype properties and
    // class properties to be extended.
    var extend = function (protoProps, staticProps) {
        var parent = this;
        var child;

        // The constructor function for the new subclass is either defined by you
        // (the "constructor" property in your `extend` definition), or defaulted
        // by us to simply call the parent constructor.
        if (protoProps && _.has(protoProps, 'constructor')) {
            child = protoProps.constructor;
        } else {
            child = function () { return parent.apply(this, arguments); };
        }

        // Add static properties to the constructor function, if supplied.
        _.extend(child, parent, staticProps);

        // Set the prototype chain to inherit from `parent`, without calling
        // `parent`'s constructor function and add the prototype properties.
        child.prototype = _.create(parent.prototype, protoProps);
        child.prototype.constructor = child;

        // Set a convenience property in case the parent's prototype is needed
        // later.
        child.__super__ = parent.prototype;

        return child;
    };

    // Set up inheritance for the model, collection, router, view and history.
    Model.extend = Collection.extend = Router.extend = View.extend = History.extend = extend;

    // Throw an error when a URL is needed, and none is supplied.
    var urlError = function () {
        throw new Error('A "url" property or function must be specified');
    };

    // Wrap an optional error callback with a fallback error event.
    var wrapError = function (model, options) {
        var error = options.error;
        options.error = function (resp) {
            if (error) error.call(options.context, model, resp, options);
            model.trigger('error', model, resp, options);
        };
    };

    return Backbone;
});

(function() {

  // nb. This is for IE10 and lower _only_.
  var supportCustomEvent = window.CustomEvent;
  if (!supportCustomEvent || typeof supportCustomEvent === 'object') {
    supportCustomEvent = function CustomEvent(event, x) {
      x = x || {};
      var ev = document.createEvent('CustomEvent');
      ev.initCustomEvent(event, !!x.bubbles, !!x.cancelable, x.detail || null);
      return ev;
    };
    supportCustomEvent.prototype = window.Event.prototype;
  }

  /**
   * @param {Element} el to check for stacking context
   * @return {boolean} whether this el or its parents creates a stacking context
   */
  function createsStackingContext(el) {
    while (el && el !== document.body) {
      var s = window.getComputedStyle(el);
      var invalid = function(k, ok) {
        return !(s[k] === undefined || s[k] === ok);
      }
      if (s.opacity < 1 ||
          invalid('zIndex', 'auto') ||
          invalid('transform', 'none') ||
          invalid('mixBlendMode', 'normal') ||
          invalid('filter', 'none') ||
          invalid('perspective', 'none') ||
          s['isolation'] === 'isolate' ||
          s.position === 'fixed' ||
          s.webkitOverflowScrolling === 'touch') {
        return true;
      }
      el = el.parentElement;
    }
    return false;
  }

  /**
   * Finds the nearest <dialog> from the passed element.
   *
   * @param {Element} el to search from
   * @return {HTMLDialogElement} dialog found
   */
  function findNearestDialog(el) {
    while (el) {
      if (el.localName === 'dialog') {
        return /** @type {HTMLDialogElement} */ (el);
      }
      el = el.parentElement;
    }
    return null;
  }

  /**
   * Blur the specified element, as long as it's not the HTML body element.
   * This works around an IE9/10 bug - blurring the body causes Windows to
   * blur the whole application.
   *
   * @param {Element} el to blur
   */
  function safeBlur(el) {
    if (el && el.blur && el !== document.body) {
      el.blur();
    }
  }

  /**
   * @param {!NodeList} nodeList to search
   * @param {Node} node to find
   * @return {boolean} whether node is inside nodeList
   */
  function inNodeList(nodeList, node) {
    for (var i = 0; i < nodeList.length; ++i) {
      if (nodeList[i] === node) {
        return true;
      }
    }
    return false;
  }

  /**
   * @param {HTMLFormElement} el to check
   * @return {boolean} whether this form has method="dialog"
   */
  function isFormMethodDialog(el) {
    if (!el || !el.hasAttribute('method')) {
      return false;
    }
    return el.getAttribute('method').toLowerCase() === 'dialog';
  }

  /**
   * @param {!HTMLDialogElement} dialog to upgrade
   * @constructor
   */
  function dialogPolyfillInfo(dialog) {
    this.dialog_ = dialog;
    this.replacedStyleTop_ = false;
    this.openAsModal_ = false;

    // Set a11y role. Browsers that support dialog implicitly know this already.
    if (!dialog.hasAttribute('role')) {
      dialog.setAttribute('role', 'dialog');
    }

    dialog.show = this.show.bind(this);
    dialog.showModal = this.showModal.bind(this);
    dialog.close = this.close.bind(this);

    if (!('returnValue' in dialog)) {
      dialog.returnValue = '';
    }

    if ('MutationObserver' in window) {
      var mo = new MutationObserver(this.maybeHideModal.bind(this));
      mo.observe(dialog, {attributes: true, attributeFilter: ['open']});
    } else {
      // IE10 and below support. Note that DOMNodeRemoved etc fire _before_ removal. They also
      // seem to fire even if the element was removed as part of a parent removal. Use the removed
      // events to force downgrade (useful if removed/immediately added).
      var removed = false;
      var cb = function() {
        removed ? this.downgradeModal() : this.maybeHideModal();
        removed = false;
      }.bind(this);
      var timeout;
      var delayModel = function(ev) {
        if (ev.target !== dialog) { return; }  // not for a child element
        var cand = 'DOMNodeRemoved';
        removed |= (ev.type.substr(0, cand.length) === cand);
        window.clearTimeout(timeout);
        timeout = window.setTimeout(cb, 0);
      };
      ['DOMAttrModified', 'DOMNodeRemoved', 'DOMNodeRemovedFromDocument'].forEach(function(name) {
        dialog.addEventListener(name, delayModel);
      });
    }
    // Note that the DOM is observed inside DialogManager while any dialog
    // is being displayed as a modal, to catch modal removal from the DOM.

    Object.defineProperty(dialog, 'open', {
      set: this.setOpen.bind(this),
      get: dialog.hasAttribute.bind(dialog, 'open')
    });

    this.backdrop_ = document.createElement('div');
    this.backdrop_.className = 'backdrop';
    this.backdrop_.addEventListener('click', this.backdropClick_.bind(this));
  }

  dialogPolyfillInfo.prototype = {

    get dialog() {
      return this.dialog_;
    },

    /**
     * Maybe remove this dialog from the modal top layer. This is called when
     * a modal dialog may no longer be tenable, e.g., when the dialog is no
     * longer open or is no longer part of the DOM.
     */
    maybeHideModal: function() {
      if (this.dialog_.hasAttribute('open') && document.body.contains(this.dialog_)) { return; }
      this.downgradeModal();
    },

    /**
     * Remove this dialog from the modal top layer, leaving it as a non-modal.
     */
    downgradeModal: function() {
      if (!this.openAsModal_) { return; }
      this.openAsModal_ = false;
      this.dialog_.style.zIndex = '';

      // This won't match the native <dialog> exactly because if the user set top on a centered
      // polyfill dialog, that top gets thrown away when the dialog is closed. Not sure it's
      // possible to polyfill this perfectly.
      if (this.replacedStyleTop_) {
        this.dialog_.style.top = '';
        this.replacedStyleTop_ = false;
      }

      // Clear the backdrop and remove from the manager.
      this.backdrop_.parentNode && this.backdrop_.parentNode.removeChild(this.backdrop_);
      dialogPolyfill.dm.removeDialog(this);
    },

    /**
     * @param {boolean} value whether to open or close this dialog
     */
    setOpen: function(value) {
      if (value) {
        this.dialog_.hasAttribute('open') || this.dialog_.setAttribute('open', '');
      } else {
        this.dialog_.removeAttribute('open');
        this.maybeHideModal();  // nb. redundant with MutationObserver
      }
    },

    /**
     * Handles clicks on the fake .backdrop element, redirecting them as if
     * they were on the dialog itself.
     *
     * @param {!Event} e to redirect
     */
    backdropClick_: function(e) {
      if (!this.dialog_.hasAttribute('tabindex')) {
        // Clicking on the backdrop should move the implicit cursor, even if dialog cannot be
        // focused. Create a fake thing to focus on. If the backdrop was _before_ the dialog, this
        // would not be needed - clicks would move the implicit cursor there.
        var fake = document.createElement('div');
        this.dialog_.insertBefore(fake, this.dialog_.firstChild);
        fake.tabIndex = -1;
        fake.focus();
        this.dialog_.removeChild(fake);
      } else {
        this.dialog_.focus();
      }

      var redirectedEvent = document.createEvent('MouseEvents');
      redirectedEvent.initMouseEvent(e.type, e.bubbles, e.cancelable, window,
          e.detail, e.screenX, e.screenY, e.clientX, e.clientY, e.ctrlKey,
          e.altKey, e.shiftKey, e.metaKey, e.button, e.relatedTarget);
      this.dialog_.dispatchEvent(redirectedEvent);
      e.stopPropagation();
    },

    /**
     * Focuses on the first focusable element within the dialog. This will always blur the current
     * focus, even if nothing within the dialog is found.
     */
    focus_: function() {
      // Find element with `autofocus` attribute, or fall back to the first form/tabindex control.
      var target = this.dialog_.querySelector('[autofocus]:not([disabled])');
      if (!target && this.dialog_.tabIndex >= 0) {
        target = this.dialog_;
      }
      if (!target) {
        // Note that this is 'any focusable area'. This list is probably not exhaustive, but the
        // alternative involves stepping through and trying to focus everything.
        var opts = ['button', 'input', 'keygen', 'select', 'textarea'];
        var query = opts.map(function(el) {
          return el + ':not([disabled])';
        });
        // TODO(samthor): tabindex values that are not numeric are not focusable.
        query.push('[tabindex]:not([disabled]):not([tabindex=""])');  // tabindex != "", not disabled
        target = this.dialog_.querySelector(query.join(', '));
      }
      safeBlur(document.activeElement);
      target && target.focus();
    },

    /**
     * Sets the zIndex for the backdrop and dialog.
     *
     * @param {number} dialogZ
     * @param {number} backdropZ
     */
    updateZIndex: function(dialogZ, backdropZ) {
      if (dialogZ < backdropZ) {
        throw new Error('dialogZ should never be < backdropZ');
      }
      this.dialog_.style.zIndex = dialogZ;
      this.backdrop_.style.zIndex = backdropZ;
    },

    /**
     * Shows the dialog. If the dialog is already open, this does nothing.
     */
    show: function() {
      if (!this.dialog_.open) {
        this.setOpen(true);
        this.focus_();
      }
    },

    /**
     * Show this dialog modally.
     */
    showModal: function() {
      if (this.dialog_.hasAttribute('open')) {
        throw new Error('Failed to execute \'showModal\' on dialog: The element is already open, and therefore cannot be opened modally.');
      }
      if (!document.body.contains(this.dialog_)) {
        throw new Error('Failed to execute \'showModal\' on dialog: The element is not in a Document.');
      }
      if (!dialogPolyfill.dm.pushDialog(this)) {
        throw new Error('Failed to execute \'showModal\' on dialog: There are too many open modal dialogs.');
      }

      if (createsStackingContext(this.dialog_.parentElement)) {
        console.warn('A dialog is being shown inside a stacking context. ' +
            'This may cause it to be unusable. For more information, see this link: ' +
            'https://github.com/GoogleChrome/dialog-polyfill/#stacking-context');
      }

      this.setOpen(true);
      this.openAsModal_ = true;

      // Optionally center vertically, relative to the current viewport.
      if (dialogPolyfill.needsCentering(this.dialog_)) {
        dialogPolyfill.reposition(this.dialog_);
        this.replacedStyleTop_ = true;
      } else {
        this.replacedStyleTop_ = false;
      }

      // Insert backdrop.
      this.dialog_.parentNode.insertBefore(this.backdrop_, this.dialog_.nextSibling);

      // Focus on whatever inside the dialog.
      this.focus_();
    },

    /**
     * Closes this HTMLDialogElement. This is optional vs clearing the open
     * attribute, however this fires a 'close' event.
     *
     * @param {string=} opt_returnValue to use as the returnValue
     */
    close: function(opt_returnValue) {
      if (!this.dialog_.hasAttribute('open')) {
        throw new Error('Failed to execute \'close\' on dialog: The element does not have an \'open\' attribute, and therefore cannot be closed.');
      }
      this.setOpen(false);

      // Leave returnValue untouched in case it was set directly on the element
      if (opt_returnValue !== undefined) {
        this.dialog_.returnValue = opt_returnValue;
      }

      // Triggering "close" event for any attached listeners on the <dialog>.
      var closeEvent = new supportCustomEvent('close', {
        bubbles: false,
        cancelable: false
      });
      this.dialog_.dispatchEvent(closeEvent);
    }

  };

  var dialogPolyfill = {};

  dialogPolyfill.reposition = function(element) {
    var scrollTop = document.body.scrollTop || document.documentElement.scrollTop;
    var topValue = scrollTop + (window.innerHeight - element.offsetHeight) / 2;
    element.style.top = Math.max(scrollTop, topValue) + 'px';
  };

  dialogPolyfill.isInlinePositionSetByStylesheet = function(element) {
    for (var i = 0; i < document.styleSheets.length; ++i) {
      var styleSheet = document.styleSheets[i];
      var cssRules = null;
      // Some browsers throw on cssRules.
      try {
        cssRules = styleSheet.cssRules;
      } catch (e) {}
      if (!cssRules) { continue; }
      for (var j = 0; j < cssRules.length; ++j) {
        var rule = cssRules[j];
        var selectedNodes = null;
        // Ignore errors on invalid selector texts.
        try {
          selectedNodes = document.querySelectorAll(rule.selectorText);
        } catch(e) {}
        if (!selectedNodes || !inNodeList(selectedNodes, element)) {
          continue;
        }
        var cssTop = rule.style.getPropertyValue('top');
        var cssBottom = rule.style.getPropertyValue('bottom');
        if ((cssTop && cssTop !== 'auto') || (cssBottom && cssBottom !== 'auto')) {
          return true;
        }
      }
    }
    return false;
  };

  dialogPolyfill.needsCentering = function(dialog) {
    var computedStyle = window.getComputedStyle(dialog);
    if (computedStyle.position !== 'absolute') {
      return false;
    }

    // We must determine whether the top/bottom specified value is non-auto.  In
    // WebKit/Blink, checking computedStyle.top == 'auto' is sufficient, but
    // Firefox returns the used value. So we do this crazy thing instead: check
    // the inline style and then go through CSS rules.
    if ((dialog.style.top !== 'auto' && dialog.style.top !== '') ||
        (dialog.style.bottom !== 'auto' && dialog.style.bottom !== '')) {
      return false;
    }
    return !dialogPolyfill.isInlinePositionSetByStylesheet(dialog);
  };

  /**
   * @param {!Element} element to force upgrade
   */
  dialogPolyfill.forceRegisterDialog = function(element) {
    if (window.HTMLDialogElement || element.showModal) {
      console.warn('This browser already supports <dialog>, the polyfill ' +
          'may not work correctly', element);
    }
    if (element.localName !== 'dialog') {
      throw new Error('Failed to register dialog: The element is not a dialog.');
    }
    new dialogPolyfillInfo(/** @type {!HTMLDialogElement} */ (element));
  };

  /**
   * @param {!Element} element to upgrade, if necessary
   */
  dialogPolyfill.registerDialog = function(element) {
    if (!element.showModal) {
      dialogPolyfill.forceRegisterDialog(element);
    }
  };

  /**
   * @constructor
   */
  dialogPolyfill.DialogManager = function() {
    /** @type {!Array<!dialogPolyfillInfo>} */
    this.pendingDialogStack = [];

    var checkDOM = this.checkDOM_.bind(this);

    // The overlay is used to simulate how a modal dialog blocks the document.
    // The blocking dialog is positioned on top of the overlay, and the rest of
    // the dialogs on the pending dialog stack are positioned below it. In the
    // actual implementation, the modal dialog stacking is controlled by the
    // top layer, where z-index has no effect.
    this.overlay = document.createElement('div');
    this.overlay.className = '_dialog_overlay';
    this.overlay.addEventListener('click', function(e) {
      this.forwardTab_ = undefined;
      e.stopPropagation();
      checkDOM([]);  // sanity-check DOM
    }.bind(this));

    this.handleKey_ = this.handleKey_.bind(this);
    this.handleFocus_ = this.handleFocus_.bind(this);

    this.zIndexLow_ = 100000;
    this.zIndexHigh_ = 100000 + 150;

    this.forwardTab_ = undefined;

    if ('MutationObserver' in window) {
      this.mo_ = new MutationObserver(function(records) {
        var removed = [];
        records.forEach(function(rec) {
          for (var i = 0, c; c = rec.removedNodes[i]; ++i) {
            if (!(c instanceof Element)) {
              continue;
            } else if (c.localName === 'dialog') {
              removed.push(c);
            }
            removed = removed.concat(c.querySelectorAll('dialog'));
          }
        });
        removed.length && checkDOM(removed);
      });
    }
  };

  /**
   * Called on the first modal dialog being shown. Adds the overlay and related
   * handlers.
   */
  dialogPolyfill.DialogManager.prototype.blockDocument = function() {
    document.documentElement.addEventListener('focus', this.handleFocus_, true);
    document.addEventListener('keydown', this.handleKey_);
    this.mo_ && this.mo_.observe(document, {childList: true, subtree: true});
  };

  /**
   * Called on the first modal dialog being removed, i.e., when no more modal
   * dialogs are visible.
   */
  dialogPolyfill.DialogManager.prototype.unblockDocument = function() {
    document.documentElement.removeEventListener('focus', this.handleFocus_, true);
    document.removeEventListener('keydown', this.handleKey_);
    this.mo_ && this.mo_.disconnect();
  };

  /**
   * Updates the stacking of all known dialogs.
   */
  dialogPolyfill.DialogManager.prototype.updateStacking = function() {
    var zIndex = this.zIndexHigh_;

    for (var i = 0, dpi; dpi = this.pendingDialogStack[i]; ++i) {
      dpi.updateZIndex(--zIndex, --zIndex);
      if (i === 0) {
        this.overlay.style.zIndex = --zIndex;
      }
    }

    // Make the overlay a sibling of the dialog itself.
    var last = this.pendingDialogStack[0];
    if (last) {
      var p = last.dialog.parentNode || document.body;
      p.appendChild(this.overlay);
    } else if (this.overlay.parentNode) {
      this.overlay.parentNode.removeChild(this.overlay);
    }
  };

  /**
   * @param {Element} candidate to check if contained or is the top-most modal dialog
   * @return {boolean} whether candidate is contained in top dialog
   */
  dialogPolyfill.DialogManager.prototype.containedByTopDialog_ = function(candidate) {
    while (candidate = findNearestDialog(candidate)) {
      for (var i = 0, dpi; dpi = this.pendingDialogStack[i]; ++i) {
        if (dpi.dialog === candidate) {
          return i === 0;  // only valid if top-most
        }
      }
      candidate = candidate.parentElement;
    }
    return false;
  };

  dialogPolyfill.DialogManager.prototype.handleFocus_ = function(event) {
    if (this.containedByTopDialog_(event.target)) { return; }

    event.preventDefault();
    event.stopPropagation();
    safeBlur(/** @type {Element} */ (event.target));

    if (this.forwardTab_ === undefined) { return; }  // move focus only from a tab key

    var dpi = this.pendingDialogStack[0];
    var dialog = dpi.dialog;
    var position = dialog.compareDocumentPosition(event.target);
    if (position & Node.DOCUMENT_POSITION_PRECEDING) {
      if (this.forwardTab_) {  // forward
        dpi.focus_();
      } else {  // backwards
        document.documentElement.focus();
      }
    } else {
      // TODO: Focus after the dialog, is ignored.
    }

    return false;
  };

  dialogPolyfill.DialogManager.prototype.handleKey_ = function(event) {
    this.forwardTab_ = undefined;
    if (event.keyCode === 27) {
      event.preventDefault();
      event.stopPropagation();
      var cancelEvent = new supportCustomEvent('cancel', {
        bubbles: false,
        cancelable: true
      });
      var dpi = this.pendingDialogStack[0];
      if (dpi && dpi.dialog.dispatchEvent(cancelEvent)) {
        dpi.dialog.close();
      }
    } else if (event.keyCode === 9) {
      this.forwardTab_ = !event.shiftKey;
    }
  };

  /**
   * Finds and downgrades any known modal dialogs that are no longer displayed. Dialogs that are
   * removed and immediately readded don't stay modal, they become normal.
   *
   * @param {!Array<!HTMLDialogElement>} removed that have definitely been removed
   */
  dialogPolyfill.DialogManager.prototype.checkDOM_ = function(removed) {
    // This operates on a clone because it may cause it to change. Each change also calls
    // updateStacking, which only actually needs to happen once. But who removes many modal dialogs
    // at a time?!
    var clone = this.pendingDialogStack.slice();
    clone.forEach(function(dpi) {
      if (removed.indexOf(dpi.dialog) !== -1) {
        dpi.downgradeModal();
      } else {
        dpi.maybeHideModal();
      }
    });
  };

  /**
   * @param {!dialogPolyfillInfo} dpi
   * @return {boolean} whether the dialog was allowed
   */
  dialogPolyfill.DialogManager.prototype.pushDialog = function(dpi) {
    var allowed = (this.zIndexHigh_ - this.zIndexLow_) / 2 - 1;
    if (this.pendingDialogStack.length >= allowed) {
      return false;
    }
    if (this.pendingDialogStack.unshift(dpi) === 1) {
      this.blockDocument();
    }
    this.updateStacking();
    return true;
  };

  /**
   * @param {!dialogPolyfillInfo} dpi
   */
  dialogPolyfill.DialogManager.prototype.removeDialog = function(dpi) {
    var index = this.pendingDialogStack.indexOf(dpi);
    if (index === -1) { return; }

    this.pendingDialogStack.splice(index, 1);
    if (this.pendingDialogStack.length === 0) {
      this.unblockDocument();
    }
    this.updateStacking();
  };

  dialogPolyfill.dm = new dialogPolyfill.DialogManager();
  dialogPolyfill.formSubmitter = null;
  dialogPolyfill.useValue = null;

  /**
   * Installs global handlers, such as click listers and native method overrides. These are needed
   * even if a no dialog is registered, as they deal with <form method="dialog">.
   */
  if (window.HTMLDialogElement === undefined) {

    /**
     * If HTMLFormElement translates method="DIALOG" into 'get', then replace the descriptor with
     * one that returns the correct value.
     */
    var testForm = document.createElement('form');
    testForm.setAttribute('method', 'dialog');
    if (testForm.method !== 'dialog') {
      var methodDescriptor = Object.getOwnPropertyDescriptor(HTMLFormElement.prototype, 'method');
      if (methodDescriptor) {
        // nb. Some older iOS and older PhantomJS fail to return the descriptor. Don't do anything
        // and don't bother to update the element.
        var realGet = methodDescriptor.get;
        methodDescriptor.get = function() {
          if (isFormMethodDialog(this)) {
            return 'dialog';
          }
          return realGet.call(this);
        };
        var realSet = methodDescriptor.set;
        methodDescriptor.set = function(v) {
          if (typeof v === 'string' && v.toLowerCase() === 'dialog') {
            return this.setAttribute('method', v);
          }
          return realSet.call(this, v);
        };
        Object.defineProperty(HTMLFormElement.prototype, 'method', methodDescriptor);
      }
    }

    /**
     * Global 'click' handler, to capture the <input type="submit"> or <button> element which has
     * submitted a <form method="dialog">. Needed as Safari and others don't report this inside
     * document.activeElement.
     */
    document.addEventListener('click', function(ev) {
      dialogPolyfill.formSubmitter = null;
      dialogPolyfill.useValue = null;
      if (ev.defaultPrevented) { return; }  // e.g. a submit which prevents default submission

      var target = /** @type {Element} */ (ev.target);
      if (!target || !isFormMethodDialog(target.form)) { return; }

      var valid = (target.type === 'submit' && ['button', 'input'].indexOf(target.localName) > -1);
      if (!valid) {
        if (!(target.localName === 'input' && target.type === 'image')) { return; }
        // this is a <input type="image">, which can submit forms
        dialogPolyfill.useValue = ev.offsetX + ',' + ev.offsetY;
      }

      var dialog = findNearestDialog(target);
      if (!dialog) { return; }

      dialogPolyfill.formSubmitter = target;
    }, false);

    /**
     * Replace the native HTMLFormElement.submit() method, as it won't fire the
     * submit event and give us a chance to respond.
     */
    var nativeFormSubmit = HTMLFormElement.prototype.submit;
    function replacementFormSubmit() {
      if (!isFormMethodDialog(this)) {
        return nativeFormSubmit.call(this);
      }
      var dialog = findNearestDialog(this);
      dialog && dialog.close();
    }
    HTMLFormElement.prototype.submit = replacementFormSubmit;

    /**
     * Global form 'dialog' method handler. Closes a dialog correctly on submit
     * and possibly sets its return value.
     */
    document.addEventListener('submit', function(ev) {
      var form = /** @type {HTMLFormElement} */ (ev.target);
      if (!isFormMethodDialog(form)) { return; }
      ev.preventDefault();

      var dialog = findNearestDialog(form);
      if (!dialog) { return; }

      // Forms can only be submitted via .submit() or a click (?), but anyway: sanity-check that
      // the submitter is correct before using its value as .returnValue.
      var s = dialogPolyfill.formSubmitter;
      if (s && s.form === form) {
        dialog.close(dialogPolyfill.useValue || s.value);
      } else {
        dialog.close();
      }
      dialogPolyfill.formSubmitter = null;
    }, true);
  }

  dialogPolyfill['forceRegisterDialog'] = dialogPolyfill.forceRegisterDialog;
  dialogPolyfill['registerDialog'] = dialogPolyfill.registerDialog;

  if (typeof define === 'function' && 'amd' in define) {
    // AMD support
    define(function() { return dialogPolyfill; });
  } else if (typeof module === 'object' && typeof module['exports'] === 'object') {
    // CommonJS support
    module['exports'] = dialogPolyfill;
  } else {
    // all others
    window['dialogPolyfill'] = dialogPolyfill;
  }
})();

/*
 * jQuery UI Widget 1.10.1+amd
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2013 jQuery Foundation and other contributors
 * Released under the MIT license.
 * http://jquery.org/license
 *
 * http://api.jqueryui.com/jQuery.widget/
 */

(function (factory) {
    factory(jQuery);
    //if (typeof define === "function" && define.amd) {
    //    // Register as an anonymous AMD module:
    //    define(["jquery"], factory);
    //} else {
    //    // Browser globals:
    //    factory(jQuery);
    //}
}(function( $, undefined ) {

var uuid = 0,
	slice = Array.prototype.slice,
	_cleanData = $.cleanData;
$.cleanData = function( elems ) {
	for ( var i = 0, elem; (elem = elems[i]) != null; i++ ) {
		try {
			$( elem ).triggerHandler( "remove" );
		// http://bugs.jquery.com/ticket/8235
		} catch( e ) {}
	}
	_cleanData( elems );
};

$.widget = function( name, base, prototype ) {
	var fullName, existingConstructor, constructor, basePrototype,
		// proxiedPrototype allows the provided prototype to remain unmodified
		// so that it can be used as a mixin for multiple widgets (#8876)
		proxiedPrototype = {},
		namespace = name.split( "." )[ 0 ];

	name = name.split( "." )[ 1 ];
	fullName = namespace + "-" + name;

	if ( !prototype ) {
		prototype = base;
		base = $.Widget;
	}

	// create selector for plugin
	$.expr[ ":" ][ fullName.toLowerCase() ] = function( elem ) {
		return !!$.data( elem, fullName );
	};

	$[ namespace ] = $[ namespace ] || {};
	existingConstructor = $[ namespace ][ name ];
	constructor = $[ namespace ][ name ] = function( options, element ) {
		// allow instantiation without "new" keyword
		if ( !this._createWidget ) {
			return new constructor( options, element );
		}

		// allow instantiation without initializing for simple inheritance
		// must use "new" keyword (the code above always passes args)
		if ( arguments.length ) {
			this._createWidget( options, element );
		}
	};
	// extend with the existing constructor to carry over any static properties
	$.extend( constructor, existingConstructor, {
		version: prototype.version,
		// copy the object used to create the prototype in case we need to
		// redefine the widget later
		_proto: $.extend( {}, prototype ),
		// track widgets that inherit from this widget in case this widget is
		// redefined after a widget inherits from it
		_childConstructors: []
	});

	basePrototype = new base();
	// we need to make the options hash a property directly on the new instance
	// otherwise we'll modify the options hash on the prototype that we're
	// inheriting from
	basePrototype.options = $.widget.extend( {}, basePrototype.options );
	$.each( prototype, function( prop, value ) {
		if ( !$.isFunction( value ) ) {
			proxiedPrototype[ prop ] = value;
			return;
		}
		proxiedPrototype[ prop ] = (function() {
			var _super = function() {
					return base.prototype[ prop ].apply( this, arguments );
				},
				_superApply = function( args ) {
					return base.prototype[ prop ].apply( this, args );
				};
			return function() {
				var __super = this._super,
					__superApply = this._superApply,
					returnValue;

				this._super = _super;
				this._superApply = _superApply;

				returnValue = value.apply( this, arguments );

				this._super = __super;
				this._superApply = __superApply;

				return returnValue;
			};
		})();
	});
	constructor.prototype = $.widget.extend( basePrototype, {
		// TODO: remove support for widgetEventPrefix
		// always use the name + a colon as the prefix, e.g., draggable:start
		// don't prefix for widgets that aren't DOM-based
		widgetEventPrefix: existingConstructor ? basePrototype.widgetEventPrefix : name
	}, proxiedPrototype, {
		constructor: constructor,
		namespace: namespace,
		widgetName: name,
		widgetFullName: fullName
	});

	// If this widget is being redefined then we need to find all widgets that
	// are inheriting from it and redefine all of them so that they inherit from
	// the new version of this widget. We're essentially trying to replace one
	// level in the prototype chain.
	if ( existingConstructor ) {
		$.each( existingConstructor._childConstructors, function( i, child ) {
			var childPrototype = child.prototype;

			// redefine the child widget using the same prototype that was
			// originally used, but inherit from the new version of the base
			$.widget( childPrototype.namespace + "." + childPrototype.widgetName, constructor, child._proto );
		});
		// remove the list of existing child constructors from the old constructor
		// so the old child constructors can be garbage collected
		delete existingConstructor._childConstructors;
	} else {
		base._childConstructors.push( constructor );
	}

	$.widget.bridge( name, constructor );
};

$.widget.extend = function( target ) {
	var input = slice.call( arguments, 1 ),
		inputIndex = 0,
		inputLength = input.length,
		key,
		value;
	for ( ; inputIndex < inputLength; inputIndex++ ) {
		for ( key in input[ inputIndex ] ) {
			value = input[ inputIndex ][ key ];
			if ( input[ inputIndex ].hasOwnProperty( key ) && value !== undefined ) {
				// Clone objects
				if ( $.isPlainObject( value ) ) {
					target[ key ] = $.isPlainObject( target[ key ] ) ?
						$.widget.extend( {}, target[ key ], value ) :
						// Don't extend strings, arrays, etc. with objects
						$.widget.extend( {}, value );
				// Copy everything else by reference
				} else {
					target[ key ] = value;
				}
			}
		}
	}
	return target;
};

$.widget.bridge = function( name, object ) {
	var fullName = object.prototype.widgetFullName || name;
	$.fn[ name ] = function( options ) {
		var isMethodCall = typeof options === "string",
			args = slice.call( arguments, 1 ),
			returnValue = this;

		// allow multiple hashes to be passed on init
		options = !isMethodCall && args.length ?
			$.widget.extend.apply( null, [ options ].concat(args) ) :
			options;

		if ( isMethodCall ) {
			this.each(function() {
				var methodValue,
					instance = $.data( this, fullName );
				if ( !instance ) {
					return $.error( "cannot call methods on " + name + " prior to initialization; " +
						"attempted to call method '" + options + "'" );
				}
				if ( !$.isFunction( instance[options] ) || options.charAt( 0 ) === "_" ) {
					return $.error( "no such method '" + options + "' for " + name + " widget instance" );
				}
				methodValue = instance[ options ].apply( instance, args );
				if ( methodValue !== instance && methodValue !== undefined ) {
					returnValue = methodValue && methodValue.jquery ?
						returnValue.pushStack( methodValue.get() ) :
						methodValue;
					return false;
				}
			});
		} else {
			this.each(function() {
				var instance = $.data( this, fullName );
				if ( instance ) {
					instance.option( options || {} )._init();
				} else {
					$.data( this, fullName, new object( options, this ) );
				}
			});
		}

		return returnValue;
	};
};

$.Widget = function( /* options, element */ ) {};
$.Widget._childConstructors = [];

$.Widget.prototype = {
	widgetName: "widget",
	widgetEventPrefix: "",
	defaultElement: "<div>",
	options: {
		disabled: false,

		// callbacks
		create: null
	},
	_createWidget: function( options, element ) {
		element = $( element || this.defaultElement || this )[ 0 ];
		this.element = $( element );
		this.uuid = uuid++;
		this.eventNamespace = "." + this.widgetName + this.uuid;
		if (!$.widget.extend) {
		    $.widget.extend = function (target) {
		        var input = slice.call(arguments, 1),
                    inputIndex = 0,
                    inputLength = input.length,
                    key,
                    value;
		        for (; inputIndex < inputLength; inputIndex++) {
		            for (key in input[inputIndex]) {
		                value = input[inputIndex][key];
		                if (input[inputIndex].hasOwnProperty(key) && value !== undefined) {
		                    // Clone objects
		                    if ($.isPlainObject(value)) {
		                        target[key] = $.isPlainObject(target[key]) ?
                                    $.widget.extend({}, target[key], value) :
                                    // Don't extend strings, arrays, etc. with objects
                                    $.widget.extend({}, value);
		                        // Copy everything else by reference
		                    } else {
		                        target[key] = value;
		                    }
		                }
		            }
		        }
		        return target;
		    };
		}
		this.options = $.widget.extend( {},
			this.options,
			this._getCreateOptions(),
			options );

		this.bindings = $();
		this.hoverable = $();
		this.focusable = $();

		if ( element !== this ) {
		    $.data(element, this.widgetFullName, this);
			this._on( true, this.element, {
				remove: function( event ) {
					if ( event.target === element ) {
						this.destroy();
					}
				}
			});
			this.document = $( element.style ?
				// element within the document
				element.ownerDocument :
				// element is window or document
				element.document || element );
			this.window = $( this.document[0].defaultView || this.document[0].parentWindow );
		}

		this._create();
		this._trigger( "create", null, this._getCreateEventData() );
		this._init();
	},
	_getCreateOptions: $.noop,
	_getCreateEventData: $.noop,
	_create: $.noop,
	_init: $.noop,

	destroy: function () {
	    if (this._destroy) {
	        this._destroy();
	    }
		// we can probably remove the unbind calls in 2.0
		// all event bindings should go through this._on()
		this.element
			.unbind( this.eventNamespace )
			// 1.9 BC for #7810
			// TODO remove dual storage
			.removeData( this.widgetName )
			.removeData( this.widgetFullName )
			// support: jquery <1.6.3
			// http://bugs.jquery.com/ticket/9413
			.removeData( $.camelCase( this.widgetFullName ) );
		this.widget()
			.unbind( this.eventNamespace )
			.removeAttr( "aria-disabled" )
			.removeClass(
				this.widgetFullName + "-disabled " +
				"ui-state-disabled" );

		// clean up events and states
		this.bindings.unbind( this.eventNamespace );
		this.hoverable.removeClass( "ui-state-hover" );
		this.focusable.removeClass( "ui-state-focus" );
	},
	_destroy: $.noop,

	widget: function() {
		return this.element;
	},

	option: function( key, value ) {
		var options = key,
			parts,
			curOption,
			i;

		if ( arguments.length === 0 ) {
			// don't return a reference to the internal hash
			return $.widget.extend( {}, this.options );
		}

		if ( typeof key === "string" ) {
			// handle nested keys, e.g., "foo.bar" => { foo: { bar: ___ } }
			options = {};
			parts = key.split( "." );
			key = parts.shift();
			if ( parts.length ) {
				curOption = options[ key ] = $.widget.extend( {}, this.options[ key ] );
				for ( i = 0; i < parts.length - 1; i++ ) {
					curOption[ parts[ i ] ] = curOption[ parts[ i ] ] || {};
					curOption = curOption[ parts[ i ] ];
				}
				key = parts.pop();
				if ( value === undefined ) {
					return curOption[ key ] === undefined ? null : curOption[ key ];
				}
				curOption[ key ] = value;
			} else {
				if ( value === undefined ) {
					return this.options[ key ] === undefined ? null : this.options[ key ];
				}
				options[ key ] = value;
			}
		}

		this._setOptions( options );

		return this;
	},
	_setOptions: function( options ) {
		var key;

		for ( key in options ) {
			this._setOption( key, options[ key ] );
		}

		return this;
	},
	_setOption: function( key, value ) {
		this.options[ key ] = value;

		if ( key === "disabled" ) {
		    this.widget()
				[value ? "addClass" : "removeClass"](
					this.widgetBaseClass + "-disabled" + " " +
					"ui-state-disabled")
				.attr("aria-disabled", value);
		}

		return this;
	},

	enable: function() {
		return this._setOption( "disabled", false );
	},
	disable: function() {
		return this._setOption( "disabled", true );
	},

	_on: function( suppressDisabledCheck, element, handlers ) {
		var delegateElement,
			instance = this;

		// no suppressDisabledCheck flag, shuffle arguments
		if ( typeof suppressDisabledCheck !== "boolean" ) {
			handlers = element;
			element = suppressDisabledCheck;
			suppressDisabledCheck = false;
		}

		// no element argument, shuffle and use this.element
		if ( !handlers ) {
			handlers = element;
			element = this.element;
			delegateElement = this.widget();
		} else {
			// accept selectors, DOM elements
			element = delegateElement = $( element );
			this.bindings = this.bindings.add( element );
		}

		$.each( handlers, function( event, handler ) {
			function handlerProxy() {
				// allow widgets to customize the disabled handling
				// - disabled as an array instead of boolean
				// - disabled class as method for disabling individual parts
				if ( !suppressDisabledCheck &&
						( instance.options.disabled === true ||
							$( this ).hasClass( "ui-state-disabled" ) ) ) {
					return;
				}
				return ( typeof handler === "string" ? instance[ handler ] : handler )
					.apply( instance, arguments );
			}

			// copy the guid so direct unbinding works
			if ( typeof handler !== "string" ) {
				handlerProxy.guid = handler.guid =
					handler.guid || handlerProxy.guid || $.guid++;
			}

			var match = event.match( /^(\w+)\s*(.*)$/ ),
				eventName = match[1] + instance.eventNamespace,
				selector = match[2];
			if ( selector ) {
				delegateElement.delegate( selector, eventName, handlerProxy );
			} else {
				element.bind( eventName, handlerProxy );
			}
		});
	},

	_off: function( element, eventName ) {
		eventName = (eventName || "").split( " " ).join( this.eventNamespace + " " ) + this.eventNamespace;
		element.unbind( eventName ).undelegate( eventName );
	},

	_delay: function( handler, delay ) {
		function handlerProxy() {
			return ( typeof handler === "string" ? instance[ handler ] : handler )
				.apply( instance, arguments );
		}
		var instance = this;
		return setTimeout( handlerProxy, delay || 0 );
	},

	_hoverable: function( element ) {
		this.hoverable = this.hoverable.add( element );
		this._on( element, {
			mouseenter: function( event ) {
				$( event.currentTarget ).addClass( "ui-state-hover" );
			},
			mouseleave: function( event ) {
				$( event.currentTarget ).removeClass( "ui-state-hover" );
			}
		});
	},

	_focusable: function( element ) {
		this.focusable = this.focusable.add( element );
		this._on( element, {
			focusin: function( event ) {
				$( event.currentTarget ).addClass( "ui-state-focus" );
			},
			focusout: function( event ) {
				$( event.currentTarget ).removeClass( "ui-state-focus" );
			}
		});
	},

	_trigger: function( type, event, data ) {
		var prop, orig,
			callback = this.options[ type ];

		data = data || {};
		event = $.Event( event );
		event.type = ( type === this.widgetEventPrefix ?
			type :
			this.widgetEventPrefix + type ).toLowerCase();
		// the original event may come from any element
		// so we need to reset the target on the new event
		event.target = this.element[ 0 ];

		// copy original event properties over to the new event
		orig = event.originalEvent;
		if ( orig ) {
			for ( prop in orig ) {
				if ( !( prop in event ) ) {
					event[ prop ] = orig[ prop ];
				}
			}
		}

		this.element.trigger( event, data );
		return !( $.isFunction( callback ) &&
			callback.apply( this.element[0], [ event ].concat( data ) ) === false ||
			event.isDefaultPrevented() );
	}
};

$.each( { show: "fadeIn", hide: "fadeOut" }, function( method, defaultEffect ) {
	$.Widget.prototype[ "_" + method ] = function( element, options, callback ) {
		if ( typeof options === "string" ) {
			options = { effect: options };
		}
		var hasOptions,
			effectName = !options ?
				method :
				options === true || typeof options === "number" ?
					defaultEffect :
					options.effect || defaultEffect;
		options = options || {};
		if ( typeof options === "number" ) {
			options = { duration: options };
		}
		hasOptions = !$.isEmptyObject( options );
		options.complete = callback;
		if ( options.delay ) {
			element.delay( options.delay );
		}
		if ( hasOptions && $.effects && $.effects.effect[ effectName ] ) {
			element[ method ]( options );
		} else if ( effectName !== method && element[ effectName ] ) {
			element[ effectName ]( options.duration, options.easing, callback );
		} else {
			element.queue(function( next ) {
				$( this )[ method ]();
				if ( callback ) {
					callback.call( element[ 0 ] );
				}
				next();
			});
		}
	};
});

}));

/*
 * jQuery File Upload Plugin 5.26
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global define, window, document, File, Blob, FormData, location */

(function (factory) {
    'use strict';
    factory(window.jQuery);
    //if (typeof define === 'function' && define.amd) {
    //    // Register as an anonymous AMD module:
    //    define([
    //        'jquery',
    //        'jquery.ui.widget'
    //    ], factory);
    //} else {
    //    // Browser globals:
    //    factory(window.jQuery);
    //}
}(function ($) {
    'use strict';

    // The FileReader API is not actually used, but works as feature detection,
    // as e.g. Safari supports XHR file uploads via the FormData API,
    // but not non-multipart XHR file uploads:
    $.support.xhrFileUpload = !!(window.XMLHttpRequestUpload && window.FileReader);
    $.support.xhrFormDataFileUpload = !!window.FormData;

    // The fileupload widget listens for change events on file input fields defined
    // via fileInput setting and paste or drop events of the given dropZone.
    // In addition to the default jQuery Widget methods, the fileupload widget
    // exposes the "add" and "send" methods, to add or directly send files using
    // the fileupload API.
    // By default, files added via file input selection, paste, drag & drop or
    // "add" method are uploaded immediately, but it is possible to override
    // the "add" callback option to queue file uploads.
    $.widget('blueimp.fileupload', {

        options: {
            // The drop target element(s), by the default the complete document.
            // Set to null to disable drag & drop support:
            dropZone: $(document),
            // The paste target element(s), by the default the complete document.
            // Set to null to disable paste support:
            pasteZone: $(document),
            // The file input field(s), that are listened to for change events.
            // If undefined, it is set to the file input fields inside
            // of the widget element on plugin initialization.
            // Set to null to disable the change listener.
            fileInput: undefined,
            // By default, the file input field is replaced with a clone after
            // each input field change event. This is required for iframe transport
            // queues and allows change events to be fired for the same file
            // selection, but can be disabled by setting the following option to false:
            replaceFileInput: true,
            // The parameter name for the file form data (the request argument name).
            // If undefined or empty, the name property of the file input field is
            // used, or "files[]" if the file input name property is also empty,
            // can be a string or an array of strings:
            paramName: undefined,
            // By default, each file of a selection is uploaded using an individual
            // request for XHR type uploads. Set to false to upload file
            // selections in one request each:
            singleFileUploads: true,
            // To limit the number of files uploaded with one XHR request,
            // set the following option to an integer greater than 0:
            limitMultiFileUploads: undefined,
            // Set the following option to true to issue all file upload requests
            // in a sequential order:
            sequentialUploads: false,
            // To limit the number of concurrent uploads,
            // set the following option to an integer greater than 0:
            limitConcurrentUploads: undefined,
            // Set the following option to true to force iframe transport uploads:
            forceIframeTransport: false,
            // Set the following option to the location of a redirect url on the
            // origin server, for cross-domain iframe transport uploads:
            redirect: undefined,
            // The parameter name for the redirect url, sent as part of the form
            // data and set to 'redirect' if this option is empty:
            redirectParamName: undefined,
            // Set the following option to the location of a postMessage window,
            // to enable postMessage transport uploads:
            postMessage: undefined,
            // By default, XHR file uploads are sent as multipart/form-data.
            // The iframe transport is always using multipart/form-data.
            // Set to false to enable non-multipart XHR uploads:
            multipart: true,
            // To upload large files in smaller chunks, set the following option
            // to a preferred maximum chunk size. If set to 0, null or undefined,
            // or the browser does not support the required Blob API, files will
            // be uploaded as a whole.
            maxChunkSize: undefined,
            // When a non-multipart upload or a chunked multipart upload has been
            // aborted, this option can be used to resume the upload by setting
            // it to the size of the already uploaded bytes. This option is most
            // useful when modifying the options object inside of the "add" or
            // "send" callbacks, as the options are cloned for each file upload.
            uploadedBytes: undefined,
            // By default, failed (abort or error) file uploads are removed from the
            // global progress calculation. Set the following option to false to
            // prevent recalculating the global progress data:
            recalculateProgress: true,
            // Interval in milliseconds to calculate and trigger progress events:
            progressInterval: 100,
            // Interval in milliseconds to calculate progress bitrate:
            bitrateInterval: 500,
            // By default, uploads are started automatically when adding files:
            autoUpload: true,

            // Additional form data to be sent along with the file uploads can be set
            // using this option, which accepts an array of objects with name and
            // value properties, a function returning such an array, a FormData
            // object (for XHR file uploads), or a simple object.
            // The form of the first fileInput is given as parameter to the function:
            formData: function (form) {
                return form.serializeArray();
            },

            // The add callback is invoked as soon as files are added to the fileupload
            // widget (via file input selection, drag & drop, paste or add API call).
            // If the singleFileUploads option is enabled, this callback will be
            // called once for each file in the selection for XHR file uplaods, else
            // once for each file selection.
            // The upload starts when the submit method is invoked on the data parameter.
            // The data object contains a files property holding the added files
            // and allows to override plugin options as well as define ajax settings.
            // Listeners for this callback can also be bound the following way:
            // .bind('fileuploadadd', func);
            // data.submit() returns a Promise object and allows to attach additional
            // handlers using jQuery's Deferred callbacks:
            // data.submit().done(func).fail(func).always(func);
            add: function (e, data) {
                if (data.autoUpload || (data.autoUpload !== false &&
                        ($(this).data('blueimp-fileupload') ||
                        $(this).data('fileupload')).options.autoUpload)) {
                    data.submit();
                }
            },

            // Other callbacks:

            // Callback for the submit event of each file upload:
            // submit: function (e, data) {}, // .bind('fileuploadsubmit', func);

            // Callback for the start of each file upload request:
            // send: function (e, data) {}, // .bind('fileuploadsend', func);

            // Callback for successful uploads:
            // done: function (e, data) {}, // .bind('fileuploaddone', func);

            // Callback for failed (abort or error) uploads:
            // fail: function (e, data) {}, // .bind('fileuploadfail', func);

            // Callback for completed (success, abort or error) requests:
            // always: function (e, data) {}, // .bind('fileuploadalways', func);

            // Callback for upload progress events:
            // progress: function (e, data) {}, // .bind('fileuploadprogress', func);

            // Callback for global upload progress events:
            // progressall: function (e, data) {}, // .bind('fileuploadprogressall', func);

            // Callback for uploads start, equivalent to the global ajaxStart event:
            // start: function (e) {}, // .bind('fileuploadstart', func);

            // Callback for uploads stop, equivalent to the global ajaxStop event:
            // stop: function (e) {}, // .bind('fileuploadstop', func);

            // Callback for change events of the fileInput(s):
            // change: function (e, data) {}, // .bind('fileuploadchange', func);

            // Callback for paste events to the pasteZone(s):
            // paste: function (e, data) {}, // .bind('fileuploadpaste', func);

            // Callback for drop events of the dropZone(s):
            // drop: function (e, data) {}, // .bind('fileuploaddrop', func);

            // Callback for dragover events of the dropZone(s):
            // dragover: function (e) {}, // .bind('fileuploaddragover', func);

            // Callback for the start of each chunk upload request:
            // chunksend: function (e, data) {}, // .bind('fileuploadchunksend', func);

            // Callback for successful chunk uploads:
            // chunkdone: function (e, data) {}, // .bind('fileuploadchunkdone', func);

            // Callback for failed (abort or error) chunk uploads:
            // chunkfail: function (e, data) {}, // .bind('fileuploadchunkfail', func);

            // Callback for completed (success, abort or error) chunk upload requests:
            // chunkalways: function (e, data) {}, // .bind('fileuploadchunkalways', func);

            // The plugin options are used as settings object for the ajax calls.
            // The following are jQuery ajax settings required for the file uploads:
            processData: false,
            contentType: false,
            cache: false
        },

        // A list of options that require a refresh after assigning a new value:
        _refreshOptionsList: [
            'fileInput',
            'dropZone',
            'pasteZone',
            'multipart',
            'forceIframeTransport'
        ],

        _BitrateTimer: function () {
            this.timestamp = +(new Date());
            this.loaded = 0;
            this.bitrate = 0;
            this.getBitrate = function (now, loaded, interval) {
                var timeDiff = now - this.timestamp;
                if (!this.bitrate || !interval || timeDiff > interval) {
                    this.bitrate = (loaded - this.loaded) * (1000 / timeDiff) * 8;
                    this.loaded = loaded;
                    this.timestamp = now;
                }
                return this.bitrate;
            };
        },

        _isXHRUpload: function (options) {
            return !options.forceIframeTransport &&
                ((!options.multipart && $.support.xhrFileUpload) ||
                $.support.xhrFormDataFileUpload);
        },

        _getFormData: function (options) {
            var formData;
            if (typeof options.formData === 'function') {
                return options.formData(options.form);
            }
            if ($.isArray(options.formData)) {
                return options.formData;
            }
            if (options.formData) {
                formData = [];
                $.each(options.formData, function (name, value) {
                    formData.push({name: name, value: value});
                });
                return formData;
            }
            return [];
        },

        _getTotal: function (files) {
            var total = 0;
            $.each(files, function (index, file) {
                total += file.size || 1;
            });
            return total;
        },

        _initProgressObject: function (obj) {
            obj._progress = {
                loaded: 0,
                total: 0,
                bitrate: 0
            };
        },

        _onProgress: function (e, data) {
            if (e.lengthComputable) {
                var now = +(new Date()),
                    loaded;
                if (data._time && data.progressInterval &&
                        (now - data._time < data.progressInterval) &&
                        e.loaded !== e.total) {
                    return;
                }
                data._time = now;
                loaded = Math.floor(
                    e.loaded / e.total * (data.chunkSize || data._progress.total)
                ) + (data.uploadedBytes || 0);
                // Add the difference from the previously loaded state
                // to the global loaded counter:
                this._progress.loaded += (loaded - data._progress.loaded);
                this._progress.bitrate = this._bitrateTimer.getBitrate(
                    now,
                    this._progress.loaded,
                    data.bitrateInterval
                );
                data._progress.loaded = data.loaded = loaded;
                data._progress.bitrate = data.bitrate = data._bitrateTimer.getBitrate(
                    now,
                    loaded,
                    data.bitrateInterval
                );
                // Trigger a custom progress event with a total data property set
                // to the file size(s) of the current upload and a loaded data
                // property calculated accordingly:
                this._trigger('progress', e, data);
                // Trigger a global progress event for all current file uploads,
                // including ajax calls queued for sequential file uploads:
                this._trigger('progressall', e, this._progress);
            }
        },

        _initProgressListener: function (options) {
            var that = this,
                xhr = options.xhr ? options.xhr() : $.ajaxSettings.xhr();
            // Accesss to the native XHR object is required to add event listeners
            // for the upload progress event:
            if (xhr.upload) {
                $(xhr.upload).bind('progress', function (e) {
                    var oe = e.originalEvent;
                    // Make sure the progress event properties get copied over:
                    e.lengthComputable = oe.lengthComputable;
                    e.loaded = oe.loaded;
                    e.total = oe.total;
                    that._onProgress(e, options);
                });
                options.xhr = function () {
                    return xhr;
                };
            }
        },

        _initXHRData: function (options) {
            var formData,
                file = options.files[0],
                // Ignore non-multipart setting if not supported:
                multipart = options.multipart || !$.support.xhrFileUpload,
                paramName = options.paramName[0];
            options.headers = options.headers || {};
            if (options.contentRange) {
                options.headers['Content-Range'] = options.contentRange;
            }
            if (!multipart) {
                options.headers['Content-Disposition'] = 'attachment; filename="' +
                    encodeURI(file.name) + '"';
                options.contentType = file.type;
                options.data = options.blob || file;
            } else if ($.support.xhrFormDataFileUpload) {
                if (options.postMessage) {
                    // window.postMessage does not allow sending FormData
                    // objects, so we just add the File/Blob objects to
                    // the formData array and let the postMessage window
                    // create the FormData object out of this array:
                    formData = this._getFormData(options);
                    if (options.blob) {
                        formData.push({
                            name: paramName,
                            value: options.blob
                        });
                    } else {
                        $.each(options.files, function (index, file) {
                            formData.push({
                                name: options.paramName[index] || paramName,
                                value: file
                            });
                        });
                    }
                } else {
                    if (options.formData instanceof FormData) {
                        formData = options.formData;
                    } else {
                        formData = new FormData();
                        $.each(this._getFormData(options), function (index, field) {
                            formData.append(field.name, field.value);
                        });
                    }
                    if (options.blob) {
                        options.headers['Content-Disposition'] = 'attachment; filename="' +
                            encodeURI(file.name) + '"';
                        formData.append(paramName, options.blob, file.name);
                    } else {
                        $.each(options.files, function (index, file) {
                            // Files are also Blob instances, but some browsers
                            // (Firefox 3.6) support the File API but not Blobs.
                            // This check allows the tests to run with
                            // dummy objects:
                            if ((window.Blob && file instanceof Blob) ||
                                    (window.File && file instanceof File)) {
                                formData.append(
                                    options.paramName[index] || paramName,
                                    file,
                                    file.name
                                );
                            }
                        });
                    }
                }
                options.data = formData;
            }
            // Blob reference is not needed anymore, free memory:
            options.blob = null;
        },

        _initIframeSettings: function (options) {
            // Setting the dataType to iframe enables the iframe transport:
            options.dataType = 'iframe ' + (options.dataType || '');
            // The iframe transport accepts a serialized array as form data:
            options.formData = this._getFormData(options);
            // Add redirect url to form data on cross-domain uploads:
            if (options.redirect && $('<a></a>').prop('href', options.url)
                    .prop('host') !== location.host) {
                options.formData.push({
                    name: options.redirectParamName || 'redirect',
                    value: options.redirect
                });
            }
        },

        _initDataSettings: function (options) {
            if (this._isXHRUpload(options)) {
                if (!this._chunkedUpload(options, true)) {
                    if (!options.data) {
                        this._initXHRData(options);
                    }
                    this._initProgressListener(options);
                }
                if (options.postMessage) {
                    // Setting the dataType to postmessage enables the
                    // postMessage transport:
                    options.dataType = 'postmessage ' + (options.dataType || '');
                }
            } else {
                this._initIframeSettings(options, 'iframe');
            }
        },

        _getParamName: function (options) {
            var fileInput = $(options.fileInput),
                paramName = options.paramName;
            if (!paramName) {
                paramName = [];
                fileInput.each(function () {
                    var input = $(this),
                        name = input.prop('name') || 'files[]',
                        i = (input.prop('files') || [1]).length;
                    while (i) {
                        paramName.push(name);
                        i -= 1;
                    }
                });
                if (!paramName.length) {
                    paramName = [fileInput.prop('name') || 'files[]'];
                }
            } else if (!$.isArray(paramName)) {
                paramName = [paramName];
            }
            return paramName;
        },

        _initFormSettings: function (options) {
            // Retrieve missing options from the input field and the
            // associated form, if available:
            if (!options.form || !options.form.length) {
                options.form = $(options.fileInput.prop('form'));
                // If the given file input doesn't have an associated form,
                // use the default widget file input's form:
                if (!options.form.length) {
                    options.form = $(this.options.fileInput.prop('form'));
                }
            }
            options.paramName = this._getParamName(options);
            if (!options.url) {
                options.url = options.form.prop('action') || location.href;
            }
            // The HTTP request method must be "POST" or "PUT":
            options.type = (options.type || options.form.prop('method') || '')
                .toUpperCase();
            if (options.type !== 'POST' && options.type !== 'PUT' &&
                    options.type !== 'PATCH') {
                options.type = 'POST';
            }
            if (!options.formAcceptCharset) {
                options.formAcceptCharset = options.form.attr('accept-charset');
            }
        },

        _getAJAXSettings: function (data) {
            var options = $.extend({}, this.options, data);
            this._initFormSettings(options);
            this._initDataSettings(options);
            return options;
        },

        // jQuery 1.6 doesn't provide .state(),
        // while jQuery 1.8+ removed .isRejected() and .isResolved():
        _getDeferredState: function (deferred) {
            if (deferred.state) {
                return deferred.state();
            }
            if (deferred.isResolved()) {
                return 'resolved';
            }
            if (deferred.isRejected()) {
                return 'rejected';
            }
            return 'pending';
        },

        // Maps jqXHR callbacks to the equivalent
        // methods of the given Promise object:
        _enhancePromise: function (promise) {
            promise.success = promise.done;
            promise.error = promise.fail;
            promise.complete = promise.always;
            return promise;
        },

        // Creates and returns a Promise object enhanced with
        // the jqXHR methods abort, success, error and complete:
        _getXHRPromise: function (resolveOrReject, context, args) {
            var dfd = $.Deferred(),
                promise = dfd.promise();
            context = context || this.options.context || promise;
            if (resolveOrReject === true) {
                dfd.resolveWith(context, args);
            } else if (resolveOrReject === false) {
                dfd.rejectWith(context, args);
            }
            promise.abort = dfd.promise;
            return this._enhancePromise(promise);
        },

        // Adds convenience methods to the callback arguments:
        _addConvenienceMethods: function (e, data) {
            var that = this;
            data.submit = function () {
                if (this.state() !== 'pending') {
                    data.jqXHR = this.jqXHR =
                        (that._trigger('submit', e, this) !== false) &&
                        that._onSend(e, this);
                }
                return this.jqXHR || that._getXHRPromise();
            };
            data.abort = function () {
                if (this.jqXHR) {
                    return this.jqXHR.abort();
                }
                return this._getXHRPromise();
            };
            data.state = function () {
                if (this.jqXHR) {
                    return that._getDeferredState(this.jqXHR);
                }
            };
            data.progress = function () {
                return this._progress;
            };
        },

        // Parses the Range header from the server response
        // and returns the uploaded bytes:
        _getUploadedBytes: function (jqXHR) {
            var range = jqXHR.getResponseHeader('Range'),
                parts = range && range.split('-'),
                upperBytesPos = parts && parts.length > 1 &&
                    parseInt(parts[1], 10);
            return upperBytesPos && upperBytesPos + 1;
        },

        // Uploads a file in multiple, sequential requests
        // by splitting the file up in multiple blob chunks.
        // If the second parameter is true, only tests if the file
        // should be uploaded in chunks, but does not invoke any
        // upload requests:
        _chunkedUpload: function (options, testOnly) {
            var that = this,
                file = options.files[0],
                fs = file.size,
                ub = options.uploadedBytes = options.uploadedBytes || 0,
                mcs = options.maxChunkSize || fs,
                slice = file.slice || file.webkitSlice || file.mozSlice,
                dfd = $.Deferred(),
                promise = dfd.promise(),
                jqXHR,
                upload;
            if (!(this._isXHRUpload(options) && slice && (ub || mcs < fs)) ||
                    options.data) {
                return false;
            }
            if (testOnly) {
                return true;
            }
            if (ub >= fs) {
                file.error = 'Uploaded bytes exceed file size';
                return this._getXHRPromise(
                    false,
                    options.context,
                    [null, 'error', file.error]
                );
            }
            // The chunk upload method:
            upload = function () {
                // Clone the options object for each chunk upload:
                var o = $.extend({}, options),
                    currentLoaded = o._progress.loaded;
                o.blob = slice.call(
                    file,
                    ub,
                    ub + mcs,
                    file.type
                );
                // Store the current chunk size, as the blob itself
                // will be dereferenced after data processing:
                o.chunkSize = o.blob.size;
                // Expose the chunk bytes position range:
                o.contentRange = 'bytes ' + ub + '-' +
                    (ub + o.chunkSize - 1) + '/' + fs;
                // Process the upload data (the blob and potential form data):
                that._initXHRData(o);
                // Add progress listeners for this chunk upload:
                that._initProgressListener(o);
                jqXHR = ((that._trigger('chunksend', null, o) !== false && $.ajax(o)) ||
                        that._getXHRPromise(false, o.context))
                    .done(function (result, textStatus, jqXHR) {
                        ub = that._getUploadedBytes(jqXHR) ||
                            (ub + o.chunkSize);
                        // Create a progress event if no final progress event
                        // with loaded equaling total has been triggered
                        // for this chunk:
                        if (o._progress.loaded === currentLoaded) {
                            that._onProgress($.Event('progress', {
                                lengthComputable: true,
                                loaded: ub - o.uploadedBytes,
                                total: ub - o.uploadedBytes
                            }), o);
                        }
                        options.uploadedBytes = o.uploadedBytes = ub;
                        o.result = result;
                        o.textStatus = textStatus;
                        o.jqXHR = jqXHR;
                        that._trigger('chunkdone', null, o);
                        that._trigger('chunkalways', null, o);
                        if (ub < fs) {
                            // File upload not yet complete,
                            // continue with the next chunk:
                            upload();
                        } else {
                            dfd.resolveWith(
                                o.context,
                                [result, textStatus, jqXHR]
                            );
                        }
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        o.jqXHR = jqXHR;
                        o.textStatus = textStatus;
                        o.errorThrown = errorThrown;
                        that._trigger('chunkfail', null, o);
                        that._trigger('chunkalways', null, o);
                        dfd.rejectWith(
                            o.context,
                            [jqXHR, textStatus, errorThrown]
                        );
                    });
            };
            this._enhancePromise(promise);
            promise.abort = function () {
                return jqXHR.abort();
            };
            upload();
            return promise;
        },

        _beforeSend: function (e, data) {
            if (this._active === 0) {
                // the start callback is triggered when an upload starts
                // and no other uploads are currently running,
                // equivalent to the global ajaxStart event:
                this._trigger('start');
                // Set timer for global bitrate progress calculation:
                this._bitrateTimer = new this._BitrateTimer();
                // Reset the global progress values:
                this._progress.loaded = this._progress.total = 0;
                this._progress.bitrate = 0;
            }
            if (!data._progress) {
                data._progress = {};
            }
            data._progress.loaded = data.loaded = data.uploadedBytes || 0;
            data._progress.total = data.total = this._getTotal(data.files) || 1;
            data._progress.bitrate = data.bitrate = 0;
            this._active += 1;
            // Initialize the global progress values:
            this._progress.loaded += data.loaded;
            this._progress.total += data.total;
        },

        _onDone: function (result, textStatus, jqXHR, options) {
            var total = options._progress.total;
            if (options._progress.loaded < total) {
                // Create a progress event if no final progress event
                // with loaded equaling total has been triggered:
                this._onProgress($.Event('progress', {
                    lengthComputable: true,
                    loaded: total,
                    total: total
                }), options);
            }
            options.result = result;
            options.textStatus = textStatus;
            options.jqXHR = jqXHR;
            this._trigger('done', null, options);
        },

        _onFail: function (jqXHR, textStatus, errorThrown, options) {
            options.jqXHR = jqXHR;
            options.textStatus = textStatus;
            options.errorThrown = errorThrown;
            this._trigger('fail', null, options);
            if (options.recalculateProgress) {
                // Remove the failed (error or abort) file upload from
                // the global progress calculation:
                this._progress.loaded -= options._progress.loaded;
                this._progress.total -= options._progress.total;
            }
        },

        _onAlways: function (jqXHRorResult, textStatus, jqXHRorError, options) {
            // jqXHRorResult, textStatus and jqXHRorError are added to the
            // options object via done and fail callbacks
            this._active -= 1;
            this._trigger('always', null, options);
            if (this._active === 0) {
                // The stop callback is triggered when all uploads have
                // been completed, equivalent to the global ajaxStop event:
                this._trigger('stop');
            }
        },

        _onSend: function (e, data) {
            if (!data.submit) {
                this._addConvenienceMethods(e, data);
            }
            var that = this,
                jqXHR,
                aborted,
                slot,
                pipe,
                options = that._getAJAXSettings(data),
                send = function () {
                    that._sending += 1;
                    // Set timer for bitrate progress calculation:
                    options._bitrateTimer = new that._BitrateTimer();
                    jqXHR = jqXHR || (
                        ((aborted || that._trigger('send', e, options) === false) &&
                        that._getXHRPromise(false, options.context, aborted)) ||
                        that._chunkedUpload(options) || $.ajax(options)
                    ).done(function (result, textStatus, jqXHR) {
                        that._onDone(result, textStatus, jqXHR, options);
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        that._onFail(jqXHR, textStatus, errorThrown, options);
                    }).always(function (jqXHRorResult, textStatus, jqXHRorError) {
                        that._sending -= 1;
                        that._onAlways(
                            jqXHRorResult,
                            textStatus,
                            jqXHRorError,
                            options
                        );
                        if (options.limitConcurrentUploads &&
                                options.limitConcurrentUploads > that._sending) {
                            // Start the next queued upload,
                            // that has not been aborted:
                            var nextSlot = that._slots.shift();
                            while (nextSlot) {
                                if (that._getDeferredState(nextSlot) === 'pending') {
                                    nextSlot.resolve();
                                    break;
                                }
                                nextSlot = that._slots.shift();
                            }
                        }
                    });
                    return jqXHR;
                };
            this._beforeSend(e, options);
            if (this.options.sequentialUploads ||
                    (this.options.limitConcurrentUploads &&
                    this.options.limitConcurrentUploads <= this._sending)) {
                if (this.options.limitConcurrentUploads > 1) {
                    slot = $.Deferred();
                    this._slots.push(slot);
                    pipe = slot.pipe(send);
                } else {
                    pipe = (this._sequence = this._sequence.pipe(send, send));
                }
                // Return the piped Promise object, enhanced with an abort method,
                // which is delegated to the jqXHR object of the current upload,
                // and jqXHR callbacks mapped to the equivalent Promise methods:
                pipe.abort = function () {
                    aborted = [undefined, 'abort', 'abort'];
                    if (!jqXHR) {
                        if (slot) {
                            slot.rejectWith(options.context, aborted);
                        }
                        return send();
                    }
                    return jqXHR.abort();
                };
                return this._enhancePromise(pipe);
            }
            return send();
        },

        _onAdd: function (e, data) {
            var that = this,
                result = true,
                options = $.extend({}, this.options, data),
                limit = options.limitMultiFileUploads,
                paramName = this._getParamName(options),
                paramNameSet,
                paramNameSlice,
                fileSet,
                i;
            if (!(options.singleFileUploads || limit) ||
                    !this._isXHRUpload(options)) {
                fileSet = [data.files];
                paramNameSet = [paramName];
            } else if (!options.singleFileUploads && limit) {
                fileSet = [];
                paramNameSet = [];
                for (i = 0; i < data.files.length; i += limit) {
                    fileSet.push(data.files.slice(i, i + limit));
                    paramNameSlice = paramName.slice(i, i + limit);
                    if (!paramNameSlice.length) {
                        paramNameSlice = paramName;
                    }
                    paramNameSet.push(paramNameSlice);
                }
            } else {
                paramNameSet = paramName;
            }
            data.originalFiles = data.files;
            $.each(fileSet || data.files, function (index, element) {
                var newData = $.extend({}, data);
                newData.files = fileSet ? element : [element];
                newData.paramName = paramNameSet[index];
                that._initProgressObject(newData);
                that._addConvenienceMethods(e, newData);
                result = that._trigger('add', e, newData);
                return result;
            });
            return result;
        },

        _replaceFileInput: function (input) {
            var inputClone = input.clone(true);
            $('<form></form>').append(inputClone)[0].reset();
            // Detaching allows to insert the fileInput on another form
            // without loosing the file input value:
            input.after(inputClone).detach();
            // Avoid memory leaks with the detached file input:
            $.cleanData(input.unbind('remove'));
            // Replace the original file input element in the fileInput
            // elements set with the clone, which has been copied including
            // event handlers:
            this.options.fileInput = this.options.fileInput.map(function (i, el) {
                if (el === input[0]) {
                    return inputClone[0];
                }
                return el;
            });
            // If the widget has been initialized on the file input itself,
            // override this.element with the file input clone:
            if (input[0] === this.element[0]) {
                this.element = inputClone;
            }
        },

        _handleFileTreeEntry: function (entry, path) {
            var that = this,
                dfd = $.Deferred(),
                errorHandler = function (e) {
                    if (e && !e.entry) {
                        e.entry = entry;
                    }
                    // Since $.when returns immediately if one
                    // Deferred is rejected, we use resolve instead.
                    // This allows valid files and invalid items
                    // to be returned together in one set:
                    dfd.resolve([e]);
                },
                dirReader;
            path = path || '';
            if (entry.isFile) {
                if (entry._file) {
                    // Workaround for Chrome bug #149735
                    entry._file.relativePath = path;
                    dfd.resolve(entry._file);
                } else {
                    entry.file(function (file) {
                        file.relativePath = path;
                        dfd.resolve(file);
                    }, errorHandler);
                }
            } else if (entry.isDirectory) {
                dirReader = entry.createReader();
                dirReader.readEntries(function (entries) {
                    that._handleFileTreeEntries(
                        entries,
                        path + entry.name + '/'
                    ).done(function (files) {
                        dfd.resolve(files);
                    }).fail(errorHandler);
                }, errorHandler);
            } else {
                // Return an empy list for file system items
                // other than files or directories:
                dfd.resolve([]);
            }
            return dfd.promise();
        },

        _handleFileTreeEntries: function (entries, path) {
            var that = this;
            return $.when.apply(
                $,
                $.map(entries, function (entry) {
                    return that._handleFileTreeEntry(entry, path);
                })
            ).pipe(function () {
                return Array.prototype.concat.apply(
                    [],
                    arguments
                );
            });
        },

        _getDroppedFiles: function (dataTransfer) {
            dataTransfer = dataTransfer || {};
            var items = dataTransfer.items;
            if (items && items.length && (items[0].webkitGetAsEntry ||
                    items[0].getAsEntry)) {
                return this._handleFileTreeEntries(
                    $.map(items, function (item) {
                        var entry;
                        if (item.webkitGetAsEntry) {
                            entry = item.webkitGetAsEntry();
                            if (entry) {
                                // Workaround for Chrome bug #149735:
                                entry._file = item.getAsFile();
                            }
                            return entry;
                        }
                        return item.getAsEntry();
                    })
                );
            }
            return $.Deferred().resolve(
                $.makeArray(dataTransfer.files)
            ).promise();
        },

        _getSingleFileInputFiles: function (fileInput) {
            fileInput = $(fileInput);
            var entries = fileInput.prop('webkitEntries') ||
                    fileInput.prop('entries'),
                files,
                value;
            if (entries && entries.length) {
                return this._handleFileTreeEntries(entries);
            }
            files = $.makeArray(fileInput.prop('files'));
            if (!files.length) {
                value = fileInput.prop('value');
                if (!value) {
                    return $.Deferred().resolve([]).promise();
                }
                // If the files property is not available, the browser does not
                // support the File API and we add a pseudo File object with
                // the input value as name with path information removed:
                files = [{name: value.replace(/^.*\\/, '')}];
            } else if (files[0].name === undefined && files[0].fileName) {
                // File normalization for Safari 4 and Firefox 3:
                $.each(files, function (index, file) {
                    file.name = file.fileName;
                    file.size = file.fileSize;
                });
            }
            return $.Deferred().resolve(files).promise();
        },

        _getFileInputFiles: function (fileInput) {
            if (!(fileInput instanceof $) || fileInput.length === 1) {
                return this._getSingleFileInputFiles(fileInput);
            }
            return $.when.apply(
                $,
                $.map(fileInput, this._getSingleFileInputFiles)
            ).pipe(function () {
                return Array.prototype.concat.apply(
                    [],
                    arguments
                );
            });
        },

        _onChange: function (e) {
            var that = this,
                data = {
                    fileInput: $(e.target),
                    form: $(e.target.form)
                };
            this._getFileInputFiles(data.fileInput).always(function (files) {
                data.files = files;
                if (that.options.replaceFileInput) {
                    that._replaceFileInput(data.fileInput);
                }
                if (that._trigger('change', e, data) !== false) {
                    that._onAdd(e, data);
                }
            });
        },

        _onPaste: function (e) {
            var cbd = e.originalEvent.clipboardData,
                items = (cbd && cbd.items) || [],
                data = {files: []};
            $.each(items, function (index, item) {
                var file = item.getAsFile && item.getAsFile();
                if (file) {
                    data.files.push(file);
                }
            });
            if (this._trigger('paste', e, data) === false ||
                    this._onAdd(e, data) === false) {
                return false;
            }
        },

        _onDrop: function (e) {
            var that = this,
                dataTransfer = e.dataTransfer = e.originalEvent.dataTransfer,
                data = {};
            if (dataTransfer && dataTransfer.files && dataTransfer.files.length) {
                e.preventDefault();
            }
            this._getDroppedFiles(dataTransfer).always(function (files) {
                data.files = files;
                if (that._trigger('drop', e, data) !== false) {
                    that._onAdd(e, data);
                }
            });
        },

        _onDragOver: function (e) {
            var dataTransfer = e.dataTransfer = e.originalEvent.dataTransfer;
            if (this._trigger('dragover', e) === false) {
                return false;
            }
            if (dataTransfer && $.inArray('Files', dataTransfer.types) !== -1) {
                dataTransfer.dropEffect = 'copy';
                e.preventDefault();
            }
        },

        _initEventHandlers: function () {
            if (this._isXHRUpload(this.options)) {
                this._on(this.options.dropZone, {
                    dragover: this._onDragOver,
                    drop: this._onDrop
                });
                this._on(this.options.pasteZone, {
                    paste: this._onPaste
                });
            }
            this._on(this.options.fileInput, {
                change: this._onChange
            });
        },

        _destroyEventHandlers: function () {
            this._off(this.options.dropZone, 'dragover drop');
            this._off(this.options.pasteZone, 'paste');
            this._off(this.options.fileInput, 'change');
        },

        _setOption: function (key, value) {
            var refresh = $.inArray(key, this._refreshOptionsList) !== -1;
            if (refresh) {
                this._destroyEventHandlers();
            }
            this._super(key, value);
            if (refresh) {
                this._initSpecialOptions();
                this._initEventHandlers();
            }
        },

        _initSpecialOptions: function () {
            var options = this.options;
            if (options.fileInput === undefined) {
                options.fileInput = this.element.is('input[type="file"]') ?
                        this.element : this.element.find('input[type="file"]');
            } else if (!(options.fileInput instanceof $)) {
                options.fileInput = $(options.fileInput);
            }
            if (!(options.dropZone instanceof $)) {
                options.dropZone = $(options.dropZone);
            }
            if (!(options.pasteZone instanceof $)) {
                options.pasteZone = $(options.pasteZone);
            }
        },

        _create: function () {
            var options = this.options;
            // Initialize options set via HTML5 data-attributes:
            $.extend(options, $(this.element[0].cloneNode(false)).data());
            this._initSpecialOptions();
            this._slots = [];
            this._sequence = this._getXHRPromise(true);
            this._sending = this._active = 0;
            this._initProgressObject(this);
            this._initEventHandlers();
        },

        // This method is exposed to the widget API and allows to query
        // the widget upload progress.
        // It returns an object with loaded, total and bitrate properties
        // for the running uploads:
        progress: function () {
            return this._progress;
        },

        // This method is exposed to the widget API and allows adding files
        // using the fileupload API. The data parameter accepts an object which
        // must have a files property and can contain additional options:
        // .fileupload('add', {files: filesList});
        add: function (data) {
            var that = this;
            if (!data || this.options.disabled) {
                return;
            }
            if (data.fileInput && !data.files) {
                this._getFileInputFiles(data.fileInput).always(function (files) {
                    data.files = files;
                    that._onAdd(null, data);
                });
            } else {
                data.files = $.makeArray(data.files);
                this._onAdd(null, data);
            }
        },

        // This method is exposed to the widget API and allows sending files
        // using the fileupload API. The data parameter accepts an object which
        // must have a files or fileInput property and can contain additional options:
        // .fileupload('send', {files: filesList});
        // The method returns a Promise object for the file upload call.
        send: function (data) {
            if (data && !this.options.disabled) {
                if (data.fileInput && !data.files) {
                    var that = this,
                        dfd = $.Deferred(),
                        promise = dfd.promise(),
                        jqXHR,
                        aborted;
                    promise.abort = function () {
                        aborted = true;
                        if (jqXHR) {
                            return jqXHR.abort();
                        }
                        dfd.reject(null, 'abort', 'abort');
                        return promise;
                    };
                    this._getFileInputFiles(data.fileInput).always(
                        function (files) {
                            if (aborted) {
                                return;
                            }
                            data.files = files;
                            jqXHR = that._onSend(null, data).then(
                                function (result, textStatus, jqXHR) {
                                    dfd.resolve(result, textStatus, jqXHR);
                                },
                                function (jqXHR, textStatus, errorThrown) {
                                    dfd.reject(jqXHR, textStatus, errorThrown);
                                }
                            );
                        }
                    );
                    return this._enhancePromise(promise);
                }
                data.files = $.makeArray(data.files);
                if (data.files.length) {
                    return this._onSend(null, data);
                }
            }
            return this._getXHRPromise(false, data && data.context);
        }

    });

}));

function getColorCode(n){return n?(n.toUpperCase().charCodeAt(0)+randomInt)%10:randomInt%10}function getDefaultAvatar(){egov.mobile&&(title=egov.mobile.avatarTheme);switch(title){case"troll":return String.format(getResource("egov.resources.avatar.troll"),Math.floor(Math.random()*6)+1);case"icon":return String.format(getResource("egov.resources.avatar.icon"),Math.floor(Math.random()*2)+1);case"alphabet":return String.format(getResource("egov.resources.avatar.alphabet"),account[0].toLowerCase());default:return getResource("egov.resources.avatar.noData")}}function getUserAvatar(n,t){return n?String.format(egov.resources.avatar.path,t):getDefaultAvatar()}function getErrorAvatar(){return getResource("egov.resources.avatar.errorUrl")}jQuery.fn.bindResources=function(n,isNotRemoveDataRes){isNotRemoveDataRes=!0;var t=this.find("*[data-res]"),i=this.find("*[data-restitle]"),r=this.find("*[data-respholder]");return(t.length>0||i.length>0||r.length>0)&&($.each(t,function(i,ele){var res=$(ele).attr("data-res"),eleVal,texts;try{eval(res)?(isNotRemoveDataRes||$(ele).removeAttr("data-res"),$(ele).prop("tagName").toLowerCase()==="input"?(eleVal=$(ele).val().trim(),$(ele).val(eval(res))):(eleVal=$(ele).text().trim(),$(ele).text(eval(res)))):console.log(res)}catch(e){texts=JSON.stringify(res).split(".");texts=texts[texts.length-2]+"."+texts[texts.length-1];isNotRemoveDataRes||$(ele).removeAttr("data-res");$(ele).prop("tagName").toLowerCase()==="input"?$(ele).val(texts):$(ele).text(texts);console.log(res)}}),$.each(i,function(i,ele){var res=$(ele).attr("data-restitle"),texts;try{eval(res)&&(isNotRemoveDataRes||$(ele).removeAttr("data-restitle"),$(ele).attr("title",eval(res)))}catch(e){texts=JSON.stringify(res).split(".");texts=texts[texts.length-2]+"."+texts[texts.length-1];isNotRemoveDataRes||$(ele).removeAttr("data-restitle");$(ele).attr("title",texts);console.log(res)}}),$.each(r,function(i,ele){var res=$(ele).attr("data-respholder"),texts;try{eval(res)?(isNotRemoveDataRes||$(ele).removeAttr("data-respholder"),$(ele).attr("placeholder",eval(res))):console.log(res)}catch(e){texts=JSON.stringify(res).split(".");texts=texts[texts.length-2]+"."+texts[texts.length-1];isNotRemoveDataRes||$(ele).removeAttr("data-respholder");$(ele).attr("placeholder",texts);console.log(res)}})),typeof n=="function"&&n(),this};window.getResource=function(resourceKey){try{return eval(resourceKey)}catch(e){return resourceKey}};var extend=function(n,t){for(var i in t)t[i]&&t[i].constructor&&t[i].constructor===Object?(n[i]=n[i]||{},arguments.callee(n[i],t[i])):n[i]=t[i];return n},randomInt=Math.floor(Math.random()*10);

(function (window, egov, bmail) {
    var extend = function (destination, source) {
        /// <summary>
        /// Hàm extend để thêm resource
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        for (var property in source) {
            if (source[property] && source[property].constructor &&
             source[property].constructor === Object) {
                destination[property] = destination[property] || {};
                arguments.callee(destination[property], source[property]);
            } else {
                destination[property] = source[property];
            }
        }
        return destination;
    };
    //#region Version 1.0 - Đã dịch không thêm resource mới vào đây

    egov.resources = {
        document: {
            Compendium: "Trích yếu",
            Comment: "Ý kiến xử lý",
            DocType: "Loại văn bản",
            Category: "Hình thức",
            InOutPlace: "Đơn vị",
            DateAppointed: "Thời hạn XL",
            Organization: "Cơ quan gửi",
            DocCode: "Số/ký hiệu *",
            DocCode2: "Số hiệu *",
            DateArrived: "Ngày đến",
            DateResponse: "Hạn hồi báo",
            DatePublished: "Ngày ban hành",
            Docfield: "Lĩnh vực",
            StoreId: "Sổ văn bản",
            InOutCode: "Số đến",
            TotalPage: "Số trang",
            ChooseTotalPage: "Chọn số trang",
            DocField: "Lĩnh vực",
            Keyword: "Từ khóa",
            SendType: "Hình thức gửi",
            DocCode1: "Mã hồ sơ",
            CitizenName: "Tên công dân",
            Address: "Địa chỉ",
            Phone: "Số điện thoại",
            DocPapers: "Giấy tờ thu",
            IdentityCard: "Số CMT",
            Email: "Thư điện tử",
            Commune: "Xã phường",
            AttachmentList: "File đính kèm",
            RelationList: "Văn bản liên quan",
            BusinessLicense: "Giấy phép đăng ký",
            cbDetail: "Hiển thị chi tiết văn bản đến",
            AllComment: "Nội dung xử lý",
            titleContent: "Nội dung văn bản",
            Urgent: {
                name: "Độ khẩn",
                normal: "Thường",
                fast: "Khẩn",
                important: "Hỏa tốc"
            },
            SecurityId: {
                name: "Độ mật",
                normal: "Thường",
                high: "Mật",
                important: "Tối mật",
                highest: "Tuyệt mật",
            },
            CompendiumTitle: "Nhập trích yếu.",
            NoComment: "Chưa cho ý kiến",
            DisplayForm: "Hiển thị biểu mẫu",
            StorePrivate: "Hồ sơ cá nhân",
            StoreShare: "Hồ sơ chia sẻ",
            Note: "Ghi chú",
            nextPage: "Trang tiếp",
            prePage: "Trang trước",
            currentPage: "Trang 1",
            btnFinish: "Kết thúc",
            viewIconTraKetQua: "Trả kết quả",
            viewIconTiepNhanBoSung: "Tiếp nhận bổ sung",
            viewIconHuyVanBan: "Hủy",
            viewIconLuu: "Lưu sổ",
            viewIconGuiykien: "Gửi ý kiến",
            viewIconThongbao: "Thông báo",
            viewIconXinykien: "Xin ý kiến",
            viewIconYeuCauBoSung: "Yêu cầu bổ sung",
            viewIconGiaHanXuLy: "Gia hạn",
            no: "Từ chối",
            yes: "Đồng ý",
            btnInsertRelation: "Văn bản liên quan...",
            btnInsertAttachment: "Tệp đính kèm",
            btnInsertScan: "Tệp scan...",
            btnPaper: "Giấy phép...",
            btnInsertAnticipate: "Dự kiến chuyển...",
            btnTransfer: "Chuyển văn bản/hồ sơ",
            btnEdit: "Sửa nội dung văn bản/hồ sơ",
            btnInsertFile: "Đính kèm",
            btnApproverYes: "Đông ý phê duyệt",
            btnApproverNo: "Từ chối phê duyệt",
            btnDestroy: "Hủy văn bản/hồ sơ",
            viewIconKetthuc: "",
            btnFinishtt: "Kết thúc",
            btnAnswer: "Trả lời",
            btnChangeDoctype: "Phân loại",
            concurrency: "Vnd",
            UserComment: "Người xử lý",
            filename: "Tên tệp",
            filesize: "Kích thước",
            fileversion: "Phiên bản",
            lastUpdateFile: "Cập nhật cuối",
            FinalComment: "Ý kiến giải quyết",
            backtolist: "Quay lại danh sách",
            'delete': "Xóa",
            MainProcess: "Xử lý chính:",
            CoProcess: "Đồng xử lý:",
            sendTo: "Chuyển tới",
            thongbao: "Thông báo:",
            xinykien: "Xin ý kiến:",
            view: "Xem",
            download: "Tải về",
            PersonInfo: "Tên CD/DN:",
            toolbar: {
                noaction: "Không có hướng chuyển tiếp theo.",
                transferByDk: "Chuyển theo dự kiến",
                transferUserDk: "Chuyển người nhận dự kiến",
                controlName: {
                    transferDoc: {
                        name: "Chuyển",
                        message: {
                            error: "Có lỗi xảy ra khi tải danh sách hướng chuyển"
                        },
                        item: {
                            cancel: {
                                name: "Không tìm thấy hướng chuyển tiếp theo"
                            },
                            transferplan: {
                                name: "Chuyển theo dự kiến"
                            },
                            newtransferplan: {
                                name: "Chuyển người nhận dự kiến"
                            }
                        }
                    },
                    edit: {
                        name: "Sửa"
                    },
                    insert: {
                        name: "Chèn",
                        message: {
                            error: "Có lỗi xảy ra"
                        }
                    },
                    reload: {
                        name: "Tải lại"
                    },
                    approverYes: {
                        name: "Đồng ý"
                    },
                    approverNo: {
                        name: "Từ chối"
                    },
                    remove: {
                        name: "Hủy"
                    },
                    tiepNhanBoSung: {
                        name: "Yêu cầu bổ sung"
                    },
                    'return': {
                        name: "Trả kết quả"
                    },
                    finish: {
                        name: "Kết thúc"
                    },
                    traloi: {
                        name: "Trả lời",
                        hoso: "Hồ sơ mới",
                        document: "Văn bản mới",
                        message: {
                            error: "Có lỗi xảy ra khi tải danh sách phân loại"
                        }
                    },
                    phanloai: {
                        name: "Phân loại",
                        callBackTitle: "Chọn loại văn bản/hồ sơ",
                        message: {
                            error: "Có lỗi xảy ra khi tải danh sách phân loại"
                        }
                    },
                    print: {
                        name: "In",
                        message: {
                            error: "Có lỗi xảy ra khi tải danh sách in!."
                        }
                    },
                    giahan: {
                        name: "In"
                    },
                    xinykien: {
                        name: "Xin ý kiến"
                    },
                    thongbao: {
                        name: "Thông báo"
                    },
                    guiykien: {
                        name: "Gửi ý kiến"
                    },
                    savePrivateStore: {
                        name: "Lưu sổ"
                    },
                    others: {
                        name: "Khác"
                    }
                }
            },
            content: {
                version: "Xem phiên bản của {0} cập nhật lúc {1}"
            },
            relation: {
                titleDialog: "Thêm văn bản liên quan",
                confirmRelationTitle: "Xác nhận gửi kèm văn bản liên quan",
                ignoreConfirm: "Luôn gửi, không hiển thị lại thông báo này lần sau.",
                contextmenu: {
                    open: "Mở văn bản",
                    'delete': "Xóa văn bản"
                },
                documentNotExist: "Văn bản không tồn tại!"
            },
            attachment: {
                uploading: "Đang tải tệp lên",
                uploadSuccess: "Tải tệp lên thành công!.",
                uploadError: "Có lỗi xảy ra khi tải tệp lên",
                fileChanged: "<strong>Tệp {0} có sự thay đổi</strong><br/>Bạn có muốn lưu lại khô…",
                errorDownload: "Có lỗi xảy ra khi tải tệp.",
                openFile: "Mở",
                deleteFile: "Xóa",
                restoreFile: "Phục hồi tệp đã xóa",
                download: "Tải về",
                removed: "(Đã loại bỏ)",
                using: "Đang sử dụng",
                version: "Phiên bản {0} cập nhật lúc {1}",
                closeProgramBeforeSave: "Bạn phải đóng các chương trình đang mở tệp đính kèm trước khi lưu.",
                fileIsRemoved: "Tệp đã bị xóa",
                existFile: "Trùng tên tập đính kèm",
                replaceOrNo: "Bạn có muốn lưu phiên bản mới cho tệp <span style='color: #7A3807;'…",
                'new': "mới",
                notEqualName: "Tập tải lên không khớp với tập tin hiện tại",
                confirmToUploadWithOtherName: "Tập tin <span style='color: #7A3807;'>'{0}'</span> tải lên không ph…"
            },
            transfer: {
                transferButton: "Chuyển",
                dialogTitle: "Bàn giao văn bản",
                noUser: "Chưa chọn người nhận xử lý",
                transferSuccess: "Chuyển văn bản thành công.",
                transferError: "Có lỗi trong quá trình bàn giao.",
                noUserByAction: "Hướng chuyển không có người nhận",
                sendAll: "Tất cả mọi người",
                answerSuccess: "Trả lời thành công.",
                answerFail: "Có lỗi trong quá trình trả lời ý kiến.",
                showDgTitle: "Hiển thị giao diện chọn cán bộ khác",
                noXlc: "Chưa chọn cán bộ xử lý",
                userList: "Danh sách nhận văn bản",
                quicktransfer: "Chuyển nhanh",
                detail: "Chi tiết",
                'extends': "Nâng cao"
            },
            publishment: {
                dialogTitle: "Phát hành văn bản",
                privateDialogTitle: "Lưu sổ phát hành nội bộ",
                publishButton: "Lưu và Phát hành",
                noAddressSelected: "Bạn chưa chọn đơn vị nhận văn bản.",
                success: "Phát hành văn bản thành công.",
                error: "Có lỗi xảy ra khi phát hành. Vui lòng thử lại.",
                addpublishment: "Thêm dự kiến phát hành"
            },
            ChangeDoctype: {
                hasChangeDateAppoint: "Văn bản/hồ sơ đã được phân loại theo loại hồ sơ mới.</br>Bạn có muố…",
                success: "Văn bản/hồ sơ đã được chuyển sang loại văn bản {0}."
            },
            sendComment: {
                dialogButton: "Gửi ý kiến",
                dialogTitle: "Nhập ý kiến",
                enterComment: "Bạn chưa nhập ý kiến xử lý",
                sendFail: "Có lỗi xảy khi cho ý kiến, vui lòng thử lại.",
                sendSuccess: "Gửi ý kiến thành công",
                requireMessage: "Bạn chưa nhập ý kiến!"
            },
            announcement: {
                dialogTitle: "Thông báo",
                announcementButton: "Gửi thông báo",
                sendSuccess: "Gửi thông báo thành công.",
                sendFail: "Gửi thông báo lỗi, vui lòng thử lại.",
                noReceiver: "Bạn chưa chọn người nhận thông báo."
            },
            consult: {
                dialogTitle: "Xin ý kiến",
                consultButton: "Gửi xin ý kiến",
                sendSuccess: "Gửi xin ý kiến thành công.",
                sendFail: "Gửi xin ý kiến lỗi, vui lòng thử lại.",
                noReceiver: "Bạn chưa chọn người nhận xin ý kiến.",
                noComment: "Bạn chưa nhập ý kiến xử lý."
            },
            finish: {
                error: "Không kết thúc được văn bản, vui lòng thử lại.",
                success: "Kết thúc văn bản thành công",
                processing: "Đang xử lý"
            },
            docStore: {
                dialogTitle: "Lưu sổ cá nhân",
                createNew: "Tạo mới",
                saveButton: "Lưu",
                notSaveButton: "Không lưu",
                noChooseStore: "Bạn chưa chọn Sổ cá nhân",
                processing: "Đang lưu",
                success: "Lưu thành công",
                error: "Có lỗi xảy ra khi lưu, vui lòng thử lại"
            },
            hsmc: {
                documentResult: "Kết quả xử lý: ",
                noResult: "Chưa duyệt",
                resultOk: "Đã duyệt",
                resultDeny: "Không duyệt",
                removeResult: "Hủy"
            },
            supplementary: {
                title: "Yêu cầu bổ sung",
                requiredTitle: "Thông tin bổ sung",
                paper: "Giấy tờ bổ sung",
                fee: "Lệ phí bổ sung",
                noAdditional: "Dân không tới bổ sung",
                addPaper: "Thêm giấy tờ",
                addFee: "Thêm lệ phí",
                newDateAppointed: "Tính lại ngày hẹn trả",
                addDay: "Số ngày ",
                dateAppointed: "Hẹn trả: ",
                supplementType: {
                    renew: "Tính lại từ đầu",
                    'continue': "Tiếp tục tính",
                    add: "Cộng thêm ngày"
                },
                success: "Đã bổ sung",
                updateAndPrintButton: "Cập nhật và In biên nhận",
                name: "Yêu cầu bổ sung",
                undel: "Khôi phục",
                del: "Hủy",
                receivedRequire: "Yêu cầu bổ sung lần ",
                printTemplate: "In biên nhận",
                print: "In biên nhận",
                comment: "Ý kiến bổ sung",
                receivedTitle: "Tiếp nhận bổ sung",
                unsuccess: "Chưa bổ sung",
                cancelReveiced: "Hủy bổ sung",
                noComment: "Chú ý: xóa trắng ô nhập liệu này để hủy yêu cầu bổ sung của chính m…",
                removeRequired: "Hủy yêu cầu bổ sung này",
                add: "Thêm yêu cầu bổ sung",
                defaultComment: "Yêu cầu ông/bà bổ sung các loại giấy tờ còn thiếu",
                sendSuplementRequire: "Gửi yêu cầu bổ sung",
                expireDate: "Hạn bổ sung (ngày)"
            },
            print: {
                text: "In",
                quickPrint: "In nhanh",
                success: "In thành công ở: ",
                error: "Có lỗi xảy ra khi in",
                inDoc: "Khổ dọc",
                inNgang: "Khổ ngang",
                printer: "Chọn máy in",
                copies: "Số bản in",
                landscape: "Bố cục",
                isNotCreated: "Phiếu tiếp nhận chỉ in được khi tiếp nhận văn bản",
                processing: "Đang in"
            },
            renewals: {
                renewalsButton: "Gia hạn",
                renewalsAndPrintButton: "Gia hạn và In phiếu",
                dialogTitle: "Gia hạn xử lý",
                renewalsReason: "Lý do gia hạn",
                newDateAppoint: "Hạn xử lý mới",
                printTemplate: "Mẫu in",
                noPrintTemplate: "Không có mẫu in gia hạn",
                renewalsType: "Chọn hình thức gia hạn",
                renewalsStaffOverdue: "Gia hạn xử lý cá nhân",
                renewalsDocOverdue: "Gia hạn hẹn trả hồ sơ",
                error: "Gia hạn không thành công, vui lòng thử lại!"
            },
            updateLastResult: {
                ok: "Duyệt",
                deny: "Không duyệt",
                comment: "Ý kiến xử lý:",
                dialogTitle: "Cập nhật kết quả xử lý"
            },
            returnResult: {
                dialogTitle: "Trả kết quả",
                updateAndPrintButton: "In và trả kết quả",
                updateButton: "Trả kết quả",
                needToUpdateSupplementary: "Hồ sơ đang có yêu cầu bổ sung, Bạn cần cập nhật kết quả bổ sung trư…",
                needToUpdateLastResult: "Hồ sơ chưa có kết quả xử lý cuối cùng, Bạn cần cập nhật kết quả xử …",
                resultOk: "Đồng ý",
                resultDeny: "Từ chối",
                result: "Kết quả xử lý: ",
                personGive: "Thông tin công dân nhận kết quả",
                finish: " Kết thúc xử lý hồ sơ",
                printTemplate: "Mẫu in",
                finishAfterReturn: "Kết thúc xử lý hồ sơ sau khi trả kết quả."
            },
            documentOnline: {
                acceptSuccess: "Tiếp nhận văn bản đăng ký qua mạng thành công",
                checkCitizenInfo: "Kiểm tra thông tin công dân",
                noData: "Không có dữ liệu liên quan",
                getExistDocumentError: "Có lỗi trong quá trình tìm kiếm dữ liệu",
                checkRefDocument: "Xem hồ sơ liên quan",
                insertRelations: "Thêm văn bản liên quan",
                insertRejectComment: "Nhập lý do từ chối tiếp nhận hồ sơ"
            },
            confirmDestroy: "Bạn có chắc muốn hủy văn bản này?",
            xlcLabel: "Xử lý chính: ",
            dxlLabel: "Đồng gửi: ",
            xykLabel: "Xin ý kiến: ",
            gsLabel: "Giám sát: ",
            thongbaoLabel: "Thông báo: ",
            errorLoadPrivateStore: "Có lỗi xảy ra khi tải danh sách hồ sơ cá nhân",
            saveSuccess: "Lưu hồ sơ thành công",
            ignoreConfirmRelation: "Không hỏi lại",
            ignoreConfirmRelationWarning: "Có thể chỉnh lại config này bằng cách vào Thiết lập->Cấu hình khác-…",
            checkAll: "Chọn tất cả",
            displayAllComment: "Xem các ý kiến khác...",
            displayOnly3Coment: "Ẩn bớt ý kiến xử lý",
            addAnticipate: "Thêm dự kiến",
            require: "Yêu cầu",
            hasSpellError: "Phát hiện lỗi chính tả. Chọn \"Có\" nếu muốn tiếp tục, chọn \"Không…",
            errorSpell: {
                add: "Thêm vào thư viện chính tả",
                addSuccess: "Thêm thành công",
                addError: "Có lỗi xảy ra"
            },
            notpermission: "Bạn không có quyền xem văn bản này!",
            openError: "Không mở được văn bản liên quan",
            configError: "Cấu hình không đúng, vui lòng thử lại",
            saveViolateSuccess: "Ghi nhận CBLC thành công",
            table: {
                stt: "STT",
                creater: "Người soạn thảo",
                datecreate: "Ngày tạo",
                exprisedate: "Ngày hết hạn",
                lastcomment: "Ý kiến xử lý cuối",
                docCode: "Số ký hiệu",
                dateRecieved: "Ngày nhận",
                idCard: "Số CMND",
                citizenName: "Tên cơ quan/doanh nghiệp",
                Phone: "Điện thoại",
                Email: "Email",
                address: "Địa chỉ",
                relationDocsNumber: "{0} văn bản liên quan"
            },
            transferError: "Công văn có lỗi khi chuyển",
            addtime: {
                numberonly: "Thời gian gia hạn phải là số."
            },
            report: {
                exprort: "Xuất ra file",
                group: "Nhóm"
            },
            anticipate: {
                name: "Dự kiến chuyển",
                receive: "Hướng nhận",
                choosereceive: "Chọn hướng nhận",
                receiver: "Người nhận",
                choosereceiver: "Chọn người nhận",
                anticipate: "Hướng dự kiến",
                chooseanticipate: "Chọn hướng dự kiến"
            },
            PlaceLabel: "Nơi nhận",
            PublishMail: "Email nhận",
            OfficeName: "Tên cơ quan",
            PlaceInOffice: "Nơi nhận trong đơn vị",
            Approvers: "Người ký",
            DocInPage: "S.bản / s.trang",
            InPlace: "Nơi lưu bản gốc",
            publishReceive: "Danh sách nhận văn bản",
            UserNameReturned: "Người trả kết quả",
            DateReturned: "Ngày kết thúc xử lý",
            DateSuccess: "Ngày ký duyệt",
            DateReceived: "Ngày tiếp nhận",
            contextmenu: {
                copyText: "Copy",
                selectAll: "Chọn tất cả"
            },
            documentInfo: "Thông tin hồ sơ",
            citizenInfo: "Thông tin công dân",
            noCitizenIdCardNumber: "Chưa có CMT",
            noCitizenFullName: "Chưa có họ tên",
            noCitizenEmail: "Chưa có Email",
            addcomment: "Thêm ý kiến",
            Content: "Nội dung",
            Original: "Nguồn đơn",
            hasauthentication: "Thẩm quyền",
            iscomplain: "Phân loại đơn",
            publicResult: {
                titleDialog: "Cập nhật kết quả giải quyết khiếu nại, tố cáo",
                updateButton: "Xác nhận",
                finish: "Kết thúc xử lý hồ sơ",
                finalResult: "Kết quả giải quyết",
                dateAppoint: "Ngày hẹn tiếp",
                isAllowPublic: "Cho phép công bố kết quả",
                hasresult: "Đã cập nhật kết quả xử lý lần cuối ngày:"
            },
            dateCreated: "Ngày tiếp nhận",
            changeDateCreated: "Thay đổi ngày tiếp nhận",
            changeDateCreatedDialog: {
                title: "Thay đổi ngày tiếp nhận",
                submitBtn: "Thay đổi",
                closeBtn: "Bỏ qua",
                delayReason: "Lý do thay đổi",
                dateCreated: "Ngày tiếp nhận",
                applyAll: "Lưu tại cho lần tiếp nhận tiếp theo(khi nhấn hướng chuyển Tiếp nhận…"
            },
            docPaper: {
                receivedCount: "giấy tờ",
                viewPapers: "xem"
            },
            docFee: {
                viewFees: "xem",
                title: "Lệ phí"
            },
            changeWorkflowTypesDialog: {
                title: "Thay đổi loại hồ sơ",
                submitBtn: "Thay đổi",
                closeBtn: "Bỏ qua",
                stt: "STT",
                name: "Loại hồ sơ",
                expireProcess: "Hạn giữ",
                day: "Ngày",
                select: "Chọn"
            },
            documentOnlineStatus: {
                label: "Trạng thái",
                choDuyet: "Chờ duyệt",
                dangXuLy: "Đang xử lý",
                choBoSung: "Chờ bổ sung",
                choThanhToan: "Chờ thanh toán",
                choTraKetQua: "Chờ trả kết quả",
                daTraKetQua: "Đã trả kết quả",
                biTuChoi: "Bị từ chối"
            },
            delayReason: "Lý do muộn",
            deleteDelayReason: "Xóa",
            approver: {
                label: "Ký duyệt",
                accept: "Đồng ý",
                denied: "Từ chối",
                delApprover: "Xóa"
            },
            lienthong: {
                dialogTitle: "Liên thông văn bản",
                sendButton: "Gửi liên thông",
                noAddressChoised: "Chưa chọn cơ quan nhận",
                sendSuccess: "Gửi văn bản liên thông thành công",
                sendFail: "Gửi văn bản liên thông lỗi, vui lòng thử lại sau."
            },
            changeWorkflowType: "Thay đổi hạn xử lý theo loại",
            addCommonComment: "Thêm ý kiến thường dùng",
            selectCommonComment: "Chọn từ mẫu",
            create: {
                unpin: "Bỏ gắn",
                pin: "Gắn lên trên đầu"
            },
            hasReceived: "Hồ sơ đã tiếp nhận",
            isWaitting: "Hồ sơ đang đăng ký",
            appoint: {
                titleDialog: "Tạo lịch tiếp công dân",
                updateButton: "Cập nhật",
                dateAppoint: "Ngày hẹn tiếp",
                appointExist: "Đã có lịch hẹn",
                remind: "Ghi chú/Nhắc nhở",
                createAppointSuccess: "Đặt lịch thành công",
                updateAppointSuccess: "Đặt lại lịch thành công",
                number: "Lần gặp thứ"
            },
            validateEmail: "Mail không đúng định dạng.",
            validatePhone: "Số điện thoại không đúng định dạng.",
            notExist: "Văn bản/hồ sơ không tồn tại. Vui lòng xem lại.",
            deleteDocPaperError: "Có lỗi xảy ra khi xóa giấy tờ của hồ sơ, vui lòng thử lại."
        },
        documentQuickView: {
            belowDocumentSum: "Tóm tắt thông tin văn bản",
            Comment: "Ý kiến xử lý:",
            timeComment: "lúc",
            Category: "Loại văn bản:",
            Docfield: "Lĩnh vực:",
            DocCode: "Số kí hiệu:",
            Result: "Kết quả xử lý",
            LastUserComment: "Người xử lý cuối:",
            Place: "Nơi nhận văn bản:",
            Sign: "Người ký:",
            TotalPage: "Số trang:",
            noDocumentSelected: "Chọn văn bản để hiển thị thông tin tóm tắt ở đây."
        },
        transfer: {
            ChoseOtherUser: "Chọn cán bộ nhận khác",
            MainProcessUser: "Nhận bản chính",
            MainProcessUserComment: "(xử lý chính)",
            CoProcessUser: "Nhận bản sao",
            CoProcessUserComment: "(phối hợp xử lý)",
            AnnouceUser: "Nhận thông báo",
            AnnouceUserComment: "(để xem)",
            GiamsatUser: "Giám sát",
            QuickSearchAccount: "Tìm nhanh tài khoản của hướng chuyển",
            AnnouncementPlace: "Nơi nhận thông báo",
            PrivateAnoun: "Nhận thông báo",
            ConsultContent: "Nội dung xin ý kiến",
            ConsultUser: "Người xin ý kiến",
            MainProcess: "Xử lý chính",
            CoProcess: "Đồng xử lý",
            dgUserLabel: "(Chọn cá nhân, đơn vị nhận bản sao)",
            dgUser: "Cá nhân, đơn vị nhận bản sao",
            dgJobtitleLabel: "(Chọn chức vụ và phòng ban nhận bản sao)",
            dgJobtitle: "Chức vụ",
            dgDeptJob: "Phòng ban",
            dgUserGiamsat: "(Chọn cán bộ giám sát)",
            allJobs: "Tất cả chức vụ",
            sameDept: "Cùng đơn vị",
            isDg1: "Thông báo",
            isDg2: "Đồng gửi",
            searchDgLabel: "Cá nhân nhận bản sao",
            allJobTitlesForDept: "Tất cả các chức danh",
            jobtitlesDeptPopup: "Chức danh thuộc phòng ban(đơn vị)",
            jobtitleForAll: "Cấp nhận bản sao(để biết)",
            allJobTitles: "Tất cả các chức danh",
            IsThongbao: "Thông báo|",
            IsDxl: "Đồng xử lý |",
            IsAttachYk: "Gửi kèm ý kiến giải quyết",
            TransferDocument: "Bàn giao văn bản",
            userList: "Danh sách nhận văn bản",
            transferButton: "Chuyển",
            dialogTitle: "Bàn giao văn bản",
            noUser: "Chưa chọn người nhận xử lý",
            transferSuccess: "Chuyển văn bản thành công.",
            transferError: "Có lỗi trong quá trình bàn giao.",
            noUserByAction: "Hướng chuyển không có người nhận",
            sendAll: "Tất cả mọi người",
            answerSuccess: "Trả lời thành công.",
            answerFail: "Có lỗi trong quá trình trả lời ý kiến.",
            showDgTitle: "Hiển thị giao diện chọn cán bộ khác",
            noXlc: "Chưa chọn cán bộ xử lý",
            hsmsNoXlc: "Trên hồ sơ một cửa phải có người xử lý chính. Vui lòng xem lại",
            HasNoneDocument: "Bạn chưa chọn văn bản!",
            messageNoBtn: "Không",
            messageCancelBtn: "Bỏ qua",
            messageOkBtn: "Đồng ý",
            dgUserLabelM: "Cá nhân, đơn vị nhận bản sao",
            dgJobtitleLabelM: "Chức vụ nhận bản sao",
            dgDeptLabelM: "Phòng ban nhận bản sao",
            dgUserGiamsatM: "Cán bộ giám sát"
        },
        attachment: {
            view: "Xem",
            open: "Sửa",
            del: "Xóa",
            download: "Tải về",
            notPermision: "Bạn không có quyền thực hiện thao tác này",
            downloadAll: "Tải tất cả"
        },
        storePrivate: {
            attachmentName: "Tài liệu:",
            descStorePrivateAttachment: "Mô tả:",
            storePrivateName: "Tên hồ sơ:",
            storePrivateNameWarning: "Nhập tên hồ sơ",
            userJoined: "Người tham gia:",
            delJoined: "Xóa",
            descStorePrivate: "Ghi chú:"
        },
        relation: {
            open: "Mở",
            del: "Xóa",
            view: "Xem chi tiết"
        },
        toolbar: {
            XMLAttachment: "Đính kèm file XML",
            codeManager: "Quản lý mã",
            DuKienPhatHanh: "Dự kiến phát hành",
            transferBtn: "Chuyển",
            editBtn: "Sửa",
            attachBtn: "Đính kèm",
            relation: "Văn bản liên quan...",
            attachment: "Tệp đính kèm",
            scan: "Tệp scan...",
            packet: "Xử lý theo lô",
            imagePacket: "Chèn ảnh theo lô",
            paper: "Giấy phép...",
            DuKienChuyen: "Dự kiến chuyển...",
            reloadBtn: "Tải lại",
            allow: "Đồng ý",
            deny: "Từ chối",
            destroy: "Hủy",
            TiepNhanBoSung: "Tiếp nhận bổ sung",
            TraKetQua: "Trả kết quả",
            CapNhatKetQua: "Cập nhật kết quả",
            finish: "Kết thúc",
            reply: "Trả lời",
            PhanLoai: "Phân loại",
            print: "In",
            other: "Khác",
            GiaHan: "Gia hạn",
            YeuCauBoSung: "Yêu cầu bổ sung",
            XinYKien: "Xin ý kiến...",
            btnAnnouncement: "Thông báo...",
            btnSendAnswer: "Gửi ý kiến...",
            btnSaveStore: "Lưu sổ..",
            sendMail: "Gửi mail",
            sendSms: "Gửi tin nhắn",
            accept: "Tiếp nhận",
            reject: "Từ chối",
            additionalRequirements: "Yêu cầu bổ sung",
            checkCitizenInfo: "Kiểm tra thông tin công dân",
            addnewtemplate: "Thêm mẫu mới",
            btnSaveDraft: "Lưu nháp",
            btnSave: "Cập nhật",
            confirmTransferOrProcess: "Xác nhận bàn giao",
            btnEditDocInfo: "Sửa thông tin",
            appoint: "Hẹn tiếp",
            pdfPacket: "Chèn pdf theo lô",
            btnUndoFinish: "Lấy lại văn bản",
            btnRePublish: "Phát hành tiếp"
        },
        main: {
            gtv: "Kiểu gõ",
            notifications: "Thông báo",
            news: "Tin điều hành",
            newEmail: "Soạn thư",
            config: "Thiết lập",
            reply: "Gửi phản hồi",
            smallSize: "Xem cỡ nhỏ",
            mediumSize: "Xem cỡ vừa",
            largeSize: "Xem cỡ lớn",
            underPreview: "Xem trước bên dưới",
            rightPreview: "Xem trước bên phải",
            hidePreview: "Ẩn xem trước",
            reload: "Khởi động lại",
            logout: "Đăng xuất",
            searchDocument: "Tìm kiếm thông tin hồ sơ, văn bản, tệp đính kèm",
            searchFile: "Tìm kiếm file đính kèm",
            reloadMessage: "Một số thiết lập yêu câu phải reload lại hệ thống. Bạn có muốn relo…",
            closeBtn: "Đóng",
            submitBtn: "Cập nhật",
            titleMessage: "Thông báo!",
            closeAll: "Đóng tất cả lại",
            report: "Báo cáo thống kê",
            contacts: "Sổ liên lạc",
            calendar: "Lịch",
            chat: "Chat",
            documents: "Xử lý văn bản",
            bmail: "Tin điều hành",
            placeholderSearch: "Tìm kiếm thông tin hồ sơ, văn bản, tệp đính kèm",
            links: "Liên kết",
            administrator: "Quản trị hệ thống",
            messageNoBtn: "Không",
            emptyMailNotifications: "Bạn không có thông báo mail nào",
            openAllMail: "Mở tất cả mail nhận được",
            emptyChatNotifications: "Bạn không có tin nhắn nào",
            openAllChat: "Mở tất cả tin nhắn nhận được",
            emptyDocumentNotifications: "Bạn không có thông báo văn bản nào!",
            openAllDocument: "Mở tất cả văn bản được thông báo",
            haveNewDocument: "Bạn có văn bản mới",
            haveNewMail: "Bạn có thư mới",
            haveNewChat: "Bạn có tin nhắn mới",
            downloaddesktopversion: "Tải bản desktop",
            conversion: "Hội thoại",
            notJqueryAlert: "Chưa có file jquery. Vui lòng tải thêm file jquery!",
            lblDocument: "Văn bản",
            lblNewConversion: "Hội thoại",
            lblNewWorkTime: "Tạo lịch",
            lblNewMail: "Soạn thư",
            searchMail: "Tìm kiếm mail",
            youHave: "Bạn có",
            unreadDocuments: "văn bản chưa xem",
            installPlugin: {
                message: "Bạn cần cài đặt eGov Plugin để sử dụng đầy đủ các chức năng của eGo…",
                link: "Tải Plugin.",
                reDownload: "Tải lại."
            }
        },
        index: {
            storePrivate: "Hồ sơ công việc",
            plugin: "Ứng dụng",
            reportNode: "Báo cáo thống kê",
            printNode: "In nhanh",
            reload: "Đồng bộ"
        },
        setting: {
            title: "Thiết lập cá nhân",
            ProfileConfig: "Thông tin cá nhân",
            EnterCode: "Nhập mã xác thực",
            Changepassword: "Đổi mật khẩu",
            UserSetting: "Cấu hình phím tắt",
            GeneralSettings: "Cấu hình khác",
            NotifySettings: "Trung tâm thông báo",
            SignatureSetting: "Cấu hình chữ ký",
            btnUpdateSetting: "Cập nhật",
            btnCloseSetting: "Đóng",
            AuthorizesSetting: "Cấu hình ủy quyền",
            notify: {
                documentNotify: "Thông báo văn bản",
                BmailNotifyType: "Thông báo thư điện tử",
                chat: "Thông báo hội thoại",
                mail: {
                    MailFolderNotify: "Danh sách các thư mục được nhận thông báo",
                },
                mobileconfig: "Cấu hình cho app eGov",
            },
            signature: {
                titleCreate: "Thêm mới chữ ký",
                titleEdit: "Cập nhật chữ ký",
                configPossition: "Cấu hình vị trí đặt chữ ký",
                configOther: "Cấu hình khác",
                deleteMessage: "Bạn có chắc muốn xóa cấu hình này",
                labelCreate: "Thêm mới",
                table: {
                    header: {
                        stt: "STT",
                        configNameSignature: "Tên cấu hình",
                        wordsNeedFind: "Từ cần tìm",
                        findTypes: "Loại tìm kiếm",
                        signTypes: "Loại ký",
                        position: "Vị trí",
                        edit: "Sửa",
                        'delete': "Xóa"
                    },
                    body: {
                        findTypeBottomToTop: "Dưới lên",
                        findTypeTopToBottom: "Trên xuống",
                        imageSignature: "Chữ ký ảnh",
                        textSignature: "Chữ ký dạng ký tự",
                        leftPosition: "Bên trái",
                        abovePosition: "Bên trên",
                        rightPosition: "Bên phải",
                        belowPosition: "Bên dưới",
                        noData: "Không có dữ liệu"
                    }
                }
            },
            authorize: {
                titleCreate: "Thêm mới người nhận ủy quyền",
                titleEdit: "Cập nhật chữ ký",
                labelCreate: "Thêm mới",
                titleDialogDelete: "Thông báo!",
                confirmDelete: "Bạn có chắc muốn xóa cấu hình này",
                btnSubmitDelete: "Đồng ý",
                btnCancelDelete: "Hủy",
                table: {
                    header: {
                        stt: "STT",
                        nameDocType: "Tên loại hồ sơ",
                        userReceive: "Người nhận ủy quyền",
                        startDate: "Ngày bắt đầu",
                        endDate: "Ngày hết hạn",
                        state: "Trạng thái",
                        edit: "Sửa",
                        'delete': "Xóa"
                    },
                    body: {
                        noData: "Không có dữ liệu"
                    }
                }
            },
            general: {
                page: "Phần trang",
                scrollLoadData: "Cuộn chuột để tải dữ liệu",
                pagingLoadData: "Phân trang tải dữ liệu",
                showDetailDocument: "Hiển thị chi tiết văn bản",
                showQuickView: "Hiển thị tóm tắt văn bản",
                finishdocument: "Thiết lập chung xử lý hồ sơ, văn bản",
                setting: "Cấu hình Page trang chủ",
                nofifysetting: "Cấu hình notify",
                displayAccount: "Hiển thị tên người dùng",
                loadpagescroll: "Cuộn trang",
                loadpagesize: "Phân trang",
                language: "Ngôn ngữ: ",
                useVietNameseTyping: "Sử dụng bộ gõ Tiếng Việt",
                isFullQuickView: "Thiết lập chế độ xem tóm tắt văn bản/hồ sơ",
                IsSaveOpenTab: "Cho phép mở lại hồ sơ, văn bản khi load lại trang",
                HasHideLuuSo: "Cho phép bỏ qua lưu sổ khi kết thúc văn bản",
                DisplayPopupTransferTheoLo: "Hiển thị popup cho ý kiến khi bàn giao văn bản theo lô",
                ViewDocInPopUp: "Xem văn bản ở cửa sổ mới",
                IgnoreConfirmRelation: "Luôn gửi tất cả văn bản đính kèm",
                HasPopupChat: "Chat bằng cửa sổ popup (dạng phóng to).",
                DocumentNotifyType: "Thông báo văn bản",
                QuickView: "Vị trí hiển thị tóm tắt văn bản",
                MudimMethod: "Kiểu gõ",
                FontSize: "Cấu hình hiển thị cỡ chữ",
                DefaultPageSizeHome: "Số bản ghi trên 1 trang mặc định",
                ListPageSizeHome: "Danh sách phân trang",
                PrinterName: "Máy in",
                Language: "Ngôn ngữ",
                TypeChucVuChucDanh: "Hiển thị theo chức vụ hoặc chức danh trong phát hành văn bản"
            },
            profile: {
                avatar: "Ảnh đại diện",
                choseAvatar: "Chọn",
                male: "Nam",
                female: "Nữ",
                lastname: "Họ và tên đệm *",
                firstname: "Tên *",
                gender: "Giới tính *",
                phone: "Số điện thoại",
                fax: "Fax",
                address: "Địa chỉ",
                entercode: "Nhập mã"
            },
            login: {
                account: "Tài khoản:",
                password: "Mật khẩu:",
                keepingLogin: "Duy trì đăng nhập!",
                loginType: "Hình thức đăng nhập",
                forgetPassword: "Quên mật khẩu",
                choseServicer: "Hãy chọn 1 nhà cung cấp dịch vụ OpenID:",
                loading: "Đang xử lý...",
                btnLogin: "Đăng nhập",
                title: "ĐĂNG NHẬP",
                username: "Tên đăng nhập",
                capslockison: "Capslock đang bật",
                entercaptcha: "Đăng nhập sai quá số lần cho phép, hãy chứng minh bạn không phải ro…"
            },
            usersetting: {
                document: "Văn bản, hồ sơ",
                shortkey: "Phím tắt",
                documentdefaultname: "Tên văn bản, hồ sơ mặc định",
                supportkey: "Phím hỗ trợ",
                fnname: "Tên chức năng",
                generalconfig: "Cấu hình chung",
                selectdocument: "Chọn văn bản, hồ sơ"
            },
            sendemailto: "Gửi email kiểm tra tới ",
            sendemailsuccess: "thành công!",
            sendemailfailure: "không thành công!",
            smtpsetting: "Cấu hình máy chủ SMTP",
            othersetting: "Cấu hình khác",
            location: {
                addlocation: "Thêm vị trí",
                editlocation: "Sửa vị trí",
                confirmdeletefilelocation: "Bạn chắc chắn muốn xóa vị trí lưu file này chứ?",
                canotdelete: "",
                listfilelocation: "Danh sác các nơi lưu file",
                nodata: "Chưa có cấu hình vị trí lưu file"
            },
            passwordpolicy: {
                checkpassword: "Kiểm tra cú pháp mật khẩu",
                lookaccount: "Khóa tài khoản",
                passworddeadtime: "Hết hạn mật khẩu",
                passwordchangehistory: "Lịch sử thay đổi mật khẩu",
                defaultpassword: "Mật khẩu mặc định",
                captchatext: "Sử dụng chữ: (Ví dụ: 'MADQES', 'JOMCOC', ...)",
                captchamath: "Sử dụng biểu thức toán học: (Ví dụ: '4 + 5 =', '64 - 12 =', ...)",
                captchanote: "(bỏ chọn sẽ dùng thời gian khóa)"
            },
            mail: {
                active: "Kích hoạt chế độ gửi mail"
            },
            changepassword: {
                currentpassword: "Mật khẩu hiện tại *",
                newpassword: "Mật khẩu mới *",
                confirmpassword: "Xác nhận mật khẩu"
            },
            kntc: {
                enable: "Kích hoạt"
            }
        },
        scan: {
            rotateLeft: "Quay trái",
            rotateRight: "Quay phải",
            zoomIn: "Phóng to",
            zoomOut: "Thu nhỏ",
            setActualSize: "Ảnh gốc",
            crop: "Cắt ảnh",
            setBrightnessUp: "Tăng độ sáng",
            setBrightnessDown: "Giảm độ sáng",
            setContrastUp: "Tăng độ tương phản",
            setContrastDown: "Giảm độ tương phản",
            addToContent: "Đưa vào nội dung",
            pagePosition: "Trang: 0/0",
            preImage: "Ảnh trước",
            nextImage: "Ảnh sau",
            removeImageScan: "Xóa",
            removeAllImageScan: "Xóa tất cả",
            listScannerLabel: "Chọn máy scan:",
            reloadListScanner: "Làm mới danh sách máy scan",
            pixeltype: "Kiểu màu",
            pixeltype2: "Màu",
            pixeltype0: "Màu xám",
            pixeltype1: "Màu đen trắng",
            resolution: "Độ phân giải",
            resolution75: "75 dpi",
            resolution100: "100 dpi",
            resolution150: "150 dpi",
            resolution200: "200 dpi",
            resolution300: "300 dpi",
            duplex: "Quét 2 mặt",
            showui: "Dùng giao diện của máy scan",
            filename: "Tên tệp",
            imageformatLabel: "Lưu tệp dạng",
            imageformatJPEG: "JPEG",
            imageformatPNG: "PNG",
            imageformatGIF: "GIF",
            imageformatTIFF: "TIFF",
            imageformatBMP: "BMP",
            imageformatPDF: "PDF",
            imageformatDOC: "DOC",
            acquire: "Quét ảnh",
            refresh: "Làm mới danh sách máy scan",
            SaveFileAs: "Lưu file dưới dạng",
            selectScanMachine: "Chọn máy Scan",
            FileName: "Tên file",
            removeAllImageScantt: "Xóa toàn bộ"
        },
        tab: {
            close: "Đóng tab",
            home: {
                title: "Văn bản"
            },
            report: {
                title: "Báo cáo thống kê"
            },
            print: {
                title: "In nhanh"
            },
            search: {
                title: "Tìm kiếm"
            },
            setting: {
                title: "Thiết lập"
            },
            saveDraft: "Bạn có muốn lưu nháp lại văn bản này?",
            saveChange: "Văn bản có thay đổi, bạn có muốn lưu lại không?",
            newDocument: "Văn bản mới"
        },
        search: {
            compendium: "Trích yếu",
            doccode: "Số ký hiệu",
            inoutcode: "Số đến",
            content: "Nội dung",
            category: "Thể loại vb",
            keyword: "Từ khóa",
            urgent: "Độ khẩn",
            storeprivate: "Hồ sơ cá nhân",
            store: "Sổ văn bản",
            categorybusiness: {
                name: "Nghiệp vụ",
                all: "Tất cả",
                in: "Văn bản đến",
                out: "Văn bản đi",
                one: "Hồ sơ một cửa"
            },
            InOutPlace: "Đơn vị xử lý",
            OrganizationCreate: "C/Q ban hành",
            DocField: "Lĩnh vực",
            UserSuccess: "Người ký",
            UserCreate: "Người khởi tạo",
            CurrentUser: "Người giữ",
            CurrentDepartment: "Phòng ban giữ",
            FromDateStr: "Ngày tạo",
            ToDateStr: "Đến ngày",
            FromPubDateStr: "Ngày ban hành",
            showsearch: "Tìm kiếm nâng cao",
            createdate: "Ngày khởi tạo",
            createdate1: "Ngày tạo",
            status: "Trạng thái",
            status1: "Đang dự thảo",
            status2: "Đang xử lý",
            status4: "Đã kết thúc",
            status8: "Đã hủy",
            status16: "Dừng xử lý",
            search: "Tìm kiếm",
            searchnew: "Tìm kiếm mới",
            order: "STT",
            searchnotfound: "Không tìm thấy kết quả phù hợp",
            view: "Xem",
            download: "Tải về",
            DidYouMean: "Có phải bạn muốn tìm",
            all: "Tất cả",
            doccodePh: "Nhập số ký hiệu",
            inoutcodePh: "Nhập số đến",
            contentPh: "Nhập nội dung",
            keywordPh: "Nhập từ khóa",
            error: "Có lỗi xảy ra khi tìm kiếm. Vui lòng liên hệ quản trị mạng.",
            noresult: "Không tìm thấy kết quả",
            Compendiumph: "Nhập trích yếu văn bản",
            openattachmentfile: "Mở file đính kèm",
            downloadattachmentfile: "Tải file đính kèm"
        },
        common: {
            processing: "Đang xử lý...",
            loading: "Đang tải...",
            error: "Có lỗi xảy ra",
            searching: "Đang tìm kiếm",
            closeButton: "Đóng",
            addButton: "Thêm",
            editButton: "Sửa",
            updateButton: "Cập nhật",
            cancelButton: "Bỏ qua",
            deleleButton: "Xóa",
            confirmButton: "Xác nhận",
            alert: "Thông báo",
            transfering: "Đang chuyển",
            currencyUnit: "Vnd",
            save: "Lưu",
            messageYesBtn: "Có",
            messageNoBtn: "Không",
            messageCancelBtn: "Bỏ qua",
            messageOkBtn: "Đồng ý",
            errorMessage: "Có lỗi xảy ra, vui lòng thử lại hoặc báo cho quản trị",
            saveBtn: "Lưu",
            cancelBtn: "Bỏ qua",
            view: "Xem",
            updating: "Đang cập nhật ...",
            showPreviewPrint: "Hiển thị xem trước khi In"
        },
        file: {
            lenghtIsNotAllow: "File tải lên quá dung lượng quy định.",
            typeIsNotAllow: "File không đúng định dạng quy định.",
            errorUpload: "Có lỗi xảy ra khi tải tệp lên.",
            errorDownload: "Có lỗi xảy ra khi tải tệp xuống.",
            maxLength: "Dung lượng tối đa: ",
            notAcceptFileTypes: "Loại tệp này không cho phép tải lên"
        },
        home: {
            syncDataError: "Có lỗi khi đồng bộ danh sách văn bản",
            documentPreView: {
                tooltip: {
                    open: "Hiển thị tóm tăt văn bản/hồ sơ",
                    close: "Ẩn tóm tăt văn bản/hồ sơ"
                },
                control: {
                    close: "X",
                    open: "open"
                }
            },
            docType: {
                message: {
                    error: {
                        loading: "Không tải được danh sách loại văn bản!"
                    }
                }
            }
        },
        treeDocument: {
            message: {
                confirm: {},
                success: {},
                error: {
                    syncData: "Lỗi khi đồng bộ dữ liệu!"
                }
            }
        },
        treeStore: {
            nameStorePrivateRoot: "Hồ sơ cá nhân",
            nameStoreShareRoot: "Hồ sơ chia sẻ",
            title: {
                createStore: "Tạo sổ hồ sơ",
                detailSotore: "Xem chi tiết",
                addStorePrivateAttachment: "Thêm tài liệu"
            },
            message: {
                confirm: {
                    openStore: "Bạn có chắc muốn mở hồ sơ này không?",
                    closeStore: "Bạn có chắc muốn đóng hồ sơ này không?",
                    deleteStore: "Bạn có chắc muốn xóa hồ sơ này không?"
                },
                success: {
                    openStore: "Mở hồ sơ thành công!",
                    closeStore: "Đóng hồ sơ thành công!",
                    deleteStore: "Xóa hồ sơ thành công!"
                },
                error: {
                    createStore: "Có lỗi trong quá trình tạo mới sổ hồ sơ",
                    updateStore: "Có lỗi trong quá trình cập nhật sổ hồ sơ",
                    selectStore: " Có lỗi xảy ra khi lấy dữ liệu",
                    openStore: "Có lỗi khi mở hồ sơ!",
                    closeStore: "Có lỗi khi đóng hồ sơ!",
                    deleteStore: "Có lỗi khi xóa hồ sơ!"
                }
            },
            contextmenu: {
                createStore: "Tạo mới hồ sơ",
                updateStore: "Cập nhật hồ sơ",
                deleteStore: "Xóa hồ sơ",
                openStore: "Mở hồ sơ",
                closeStore: "Đóng hồ sơ",
                addStorePrivateAttachment: "Thêm tài liệu",
                messageCloseStore: "Bạn có chắc muộn đóng hồ sơ này?.",
                messageOpenStore: "Bạn có chắc muốn mở hồ sơ này?."
            }
        },
        documents: {
            title: {
                documentImportant: "Bỏ gắn quan trọng văn bản này",
                documentNotImportant: "Gắn quan trọng cho văn bản này",
                vanBanDongXuLy: "Văn bản đồng xử lý",
                vanBanSapHetHan: "Văn bản sắp hết hạn (còn 1 ngày)",
                vanBanKhanHoacQuaHanXuLy: "Văn bản khẩn hoặc quá hạn xử lý",
                vanBanQuaHanHoiBao: "Văn bản quá hạn hồi báo",
                vanBanHoaToc: "Văn bản hỏa tốc",
                vanBanThuong: "Văn bản bình thường",
                documentDetail: "Chi tiết văn bản/hồ sơ"
            },
            toolbar: {
                controlName: {
                    all: "Xem tất cả",
                    day: "ngày",
                    page: "Trang",
                    dateAppointed: "Ngày hết hạn",
                    docTypeName: "Loại hồ sơ",
                    documentImportant: "Xem văn bản quan trọng",
                    documentUnread: "Xem văn bản chưa đọc",
                    refresh: "Tải lại",
                    dateReceived: "Ngày nhận",
                    sortBy: "Sắp xếp theo",
                    setting: "Cài đặt danh sách",
                    preview: "Xem trước",
                    menu: "Menu"
                }
            },
            contextmenu: {
                name: {
                    xemvanban: "Xem văn bản...",
                    suavanban: "Sửa văn bản...",
                    guiykien: "Gửi ý kiến...",
                    xinykien: "Xin ý kiến...",
                    bangiao: "Bàn giao...",
                    thongbao: "Thông báo...",
                    laylaivanban: "Lấy lại văn bản",
                    xacnhanbangiao: "Xác nhận bàn giao...",
                    xacnhanxuly: "Xác nhận xử lý...",
                    yeucaubosung: "Yêu cầu bổ sung...",
                    tiepnhanbosung: "Tiếp nhận bổ sung...",
                    kyduyet: "Ký duyệt...",
                    ketthucxuly: "Kết thúc xử lý",
                    huyvanban: "Hủy văn bản",
                    capnhatketquaxulycuoi: "Cập nhật kết quả xử lý cuối...",
                    inphieutrinh: "In phiếu trình lãnh đạo...",
                    intomtat: "In tóm tắt",
                    capnhattiendo: "Cập nhật tiến độ...",
                    xoakhoiduthao: "Xóa văn bản dự thảo",
                    contextheodoi: "Fix contextmenu theo dõi",
                    dungxuly: "Dừng xử lý...",
                    giahanxuly: "Gia hạn xử lý...",
                    chitietvanban: "Chi tiết văn bản/hồ sơ",
                    danhdaudadoc: "Đánh dấu đã đọc",
                    danhdauchuadoc: "Đánh dấu chưa đọc",
                    movanban: "Mở văn bản",
                    exportToExcell: "Xuất danh sách ra tệp Excell",
                    exportToWord: "Xuất danh sách ra tệp Word",
                    removefromstoreprivate: "Xóa khỏi hồ sơ",
                },
                printTransferHistory: "In lịch sử bàn giao",

                reOpenDocument: {
                    text: "Mở lại hồ sơ",
                    success: "Mở lại thành công",
                    error: "Có lỗi xảy ra"
                },
                duplicateDocument: "Sao chép"
            },
            page: {
                text: "Trang",
                document: "văn bản"
            },
            message: {
                error: {
                    quickView: "Lỗi khi lấy thông tin văn bản!",
                    documentNotExist: "Văn bản không tồn tại!.",
                    documentNotSelectDelete: "Chưa chọn văn bản để xóa!.",
                    pagging: "Có lỗi trong quá trình chuyển sang trang mới",
                    loadNewerDocuments: "Có lỗi trong qua trình tải dữ liệu!",
                    getDocumentDetail: "Văn bản không tồn tại"
                }
            },
            noDocumentCopyItem: "Thật tuyệt vời! Bạn không có văn bản nào trong mục này.",
            notFound: "Danh sách văn bản hiện tại không có kết quả phù hợp. Nhấn Enter để …",
            documentNumberDayOverdue: "-{0} ngày",
            validDocuments: "Còn {0} ngày",
            validDocumentsInToday: "Hôm nay",
            validDocumentsInTodayMorning: "Sáng nay",
            validDocumentsInTodayAfternoon: "Chiều nay",
            validDocumentsTomorrow: "Ngày mai",
            validDocumentsAfterTomorrow: "Ngày kia",
            documentNumberWeekOverdue: "-{0} tuần",
            documentNumberMonthOverdue: "-{0} thg",
            documentNumberYearOverdue: "-{0} năm",
            unlimitedTime: "Vô hạn",
            multiselect: "{0}",
            print: {
                success: "In thành công",
                error: "Có lỗi xảy ra"
            },
            transfer: {
                notSelectedDocument: "Bạn chưa chọn văn bản nào.",
                confirmTitle: "Cho ý kiến trước khi bàn giao.",
                confirmCheckName: "Không hiển thị lại lần sau.",
                primaryButtonName: "Tiếp tục",
                addTemplateButtonName: "Chọn từ mẫu",
                noAction: "Không có hướng chuyển."
            }
        },
        templateComment: {
            titleDialog: "Mẫu ý kiến thường dùng",
            btnAddTemplateComment: "Thêm mẫu",
            btnSelected: "Chọn",
            table: {
                header: {
                    content: "Nội dung",
                    edit: "Sửa",
                    'delete': "Xóa"
                }
            },
            addDialog: {
                title: "Thêm mẫu ý kiến thường dùng",
                btnCreate: "Tạo mới",
                errorMessage: "Bạn chưa mẫu nhập ý kiến!"
            },
            editDialog: {
                title: "Cập nhật mẫu ý kiến thường dùng",
                btnEdit: "Cập nhật",
                errorMessage: "Bạn chưa mẫu nhập ý kiến!"
            },
            contextmenu: {
                selected: "Chọn",
                edit: "Sửa/Xem thông tin",
                'delete': "Xóa"
            }
        },
        requiredSupplementary: {
            addRequiredTitle: "Thêm mẫu yêu cầu bổ sung",
            noRequired: "Không có mẫu"
        },
        tree: {
            displayUnRead: "Có {0} văn bản chưa đọc",
            displayUnReadOnAll: "{0} chưa đọc / tổng số {1} văn bản",
            displayAll: "Có tất cả {0} văn bản",
            question: {
                label: "Hỏi đáp",
                general: "Hỏi đáp chung",
                document: "Câu hỏi hồ sơ",
                QuestionName: "Tên câu hỏi",
                citizenname: "Tên công dân",
                date: "Ngày hỏi",
                doccode: "Mã hồ sơ"
            }
        },
        searching: {
            result: "Kết quả tìm kiếm"
        },
        time: {
            date: "Ngày",
            _date: "ngày",
            minute: "Phút",
            _minute: "phút",
            mon: "Thứ 2",
            tue: "Thứ 3",
            wed: "Thứ 4",
            thi: "Thứ 5",
            fri: "Thứ 6",
            sat: "Thứ 7",
            sun: "Chủ nhật",
            morning: "Buổi sáng",
            affternoon: "Buổi chiều",
            timenotcheck: "Không kiểm tra được thời gian",
            checkdate: "Kiểm tra ngày",
            caculateextendtime: "Tính lịch nghỉ bù",
            nodata: "Không có ngày nghỉ nào",
            repeat: "Lặp lại",
            repeatbyyear: "Lặp theo năm",
            freeday: "Tên ngày nghỉ",
            AL: "Ngày âm",
            DL: "Ngày dương",
            day: "Thứ",
            listofrestday: "Danh sách ngày nghỉ năm",
            weekworktime: "Thời gian làm việc trong tuần",
            state: "Trạng thái",
            nghibu: "Nghỉ bù",
            nghile: "Nghỉ lễ",
            tatca: "Tất cả",
            worktime: "Giờ hành chính",
            listoffsetday: "Danh sách ngày làm bù trong năm",
            yesterday: "H.qua",
            minbefore: "{0} phút trước",
            justnow: "Vừa xong"
        },
        enumResource: {
            actionlevel: {
                levelone: "Mức độ 1",
                leveltwo: "Mức độ 2",
                levelthree: "Mức độ 3",
                levelfour: "Mức độ 5"
            },
            activitylogtype: {
                dangnhap: "Đăng nhập",
                dangxuat: "Đăng xuất",
                bangiao: "Bàn giao văn bản",
                thongbao: "Thông báo văn bản",
                huyvanban: "Hủy văn bản",
                ketthucvanban: "Kết thúc văn bản",
                phanloai: "Phân loại văn bản",
                phathanh: "Phát hành văn bản",
                kyduyet: "Ký duyệt văn bản",
                xinykien: "Xin ý kiến",
                guiykien: "Gửi ý kiến",
                tiepnhan: "Tiếp nhận",
                xingiahan: "Xin gia hạn",
                chuyenykiendonggop: "Chuyển ý kiến đóng góp"
            },
            categorybusinesstypes: {
                vbden: "Văn bản đến",
                vbdi: "Văn bản đi",
                hsmc: "Hồ sơ một cửa"
            },
            dailyprocesstimeforsearch: {
                allday: "Cả ngày",
                thirtyminutes: "30 phút trước",
                onehour: "1 tiếng trước",
                twohour: "2 tiếng trước",
                am: "Buổi sáng",
                pm: "Buổi chiều"
            },
            datetimereport: {
                trongngay: "Trong ngày",
                trongtuan: "Trong tuần",
                tuantruoc: "Tuần trước",
                trongthang: "Trong tháng",
                thangtruoc: "Tháng trước",
                quy1: "Quý 1",
                quy2: "Quý 2",
                quy3: "Quý 3",
                quy4: "Quý 4",
                trongnam: "Trong năm",
                namtruoc: "Năm trước",
                tatca: "Tất cả",
                tuychon: "Tùy chọn"
            },
            displaytreetype: {
                none: "Không hiển thị",
                unread: "Văn bản chưa đọc",
                unreadonall: "Chưa đọc / Tất cả",
                all: "Tất cả"
            },
            documentprocesstype: {
                tiepnhan: "Tiếp nhận",
                bangiao: "Bàn giao",
                kyduyet: "Ký duyệt",
                traketqua: "Trả kết quả",
                tiepnhanbosung: "Tiếp nhận bổ sung",
                giahan: "Gia hạn"
            },
            documenttype: {
                thongbao: "Thông báo",
                congvan: "Công văn"
            },
            egovjobenum: {
                indextimerelapsed: "IndexTimerElapsed",
                checkservices: "Kiểm tra những service không hoạt động",
                getdocumentsfromedoctool: "Kiểm tra xem có văn bản mới tới không",
                notifydocunread: "Notify những văn bản chưa đọc",
                notifydocinprocesses: "Notify những văn bản chờ xử lý",
                checkchangedfile: "Kiểm tra file bị thay đổi",
                addindex: "Đánh index tìm kiếm"
            },
            feetype: {
                indextimerelapsed: "Tiếp nhận",
                thuongbosung: "Thường bổ sung",
                tracongdan: "Trả công dân"
            },
            leveltype: {
                sobannganh: "Sở, Ban ngành",
                quanhuyen: "Quận, Huyện",
                phuongxa: "Xã, Phường"
            },
            licensestatus: {
                capmoi: "Cấp mới",
                capdoi: "Cấp đổi, bổ sung",
                thuhoi: "Thu hổi"
            },
            option: {
                documentonlinereg: "Đăng ký trực tuyến đã có tài khoản",
                documentonlineregnoaccount: "Đăng ký trực tuyến mà chưa có tài khoản",
                acceptdoconline: "Chấp nhận khi đăng ký trực tuyến",
                implementdoconline: "Yêu cầu bổ sung khi đăng ký trực tuyến",
                rejectdoconline: "Từ chối khi đăng ký trực tuyến"
            },
            papertype: {
                tiepnhan: "Tiếp nhận",
                thuongbosung: "Thường bổ sung",
                tracongdan: "Trả công dân"
            },
            permissiontypes: {
                ktao: "Khởi tạo văn bản",
                xly: "Xử lý văn bản"
            },
            processfilterexpression: {
                groupby: "Nhóm theo",
                equal: "Bằng",
                custom: "Khác"
            },
            scheduletype: {
                hangphut: "Hàng phút",
                hanggio: "Hàng giờ",
                hangngay: "Hàng ngày",
                hangtuan: "Hàng tuần",
                hangthang: "Hàng tháng"
            },
            searchtype: {
                document: "Tìm kiếm văn bản",
                file: "Tìm kiếm trong file"
            },
            securitytype: {
                thuong: "Thường",
                mat: "Mật",
                toimat: "Tối mật"
            },
            sendtype: {
                buudien: "Bưu điện",
                email: "Email",
                fax: "Fax",
                traotay: "Trao tay"
            },
            servicestatus: {
                running: "Đang chạy",
                stoped: "Đang dừng",
                paused: "Đang tạm dừng",
                accessdenied: "Không có quyền truy cập service",
                notfound: "Service chưa được cài đặt trên hệ thống"
            },
            specialkeyenum: {
                nguoidangnhap: "Người in phiếu",
                ngaythanghientai: "Ngày tháng hiện tại",
                meetingtitle: "Tiêu đề cuộc họp",
                meetingresource: "Địa điểm họp",
                meetingdate: "Thời điểm họp",
                meetingtodate: "Thời điểm kết thúc",
                meetingcreator: "Người tạo cuộc họp",
                meetingbody: "Nội dung cuộc họp",
                meetingusers: "Người tham gia cuộc họp",
                meetinglastupdate: "Người cập nhật cuộc họp"
            },
            supplementtype: {
                reset: "Tính lại thời gian",
                'continue': "Tiếp tục xử lý",
                fixeddays: "Thêm ngày cố định"
            },
            templatetype: {
                phieuin: "Phiếu in",
                email: "Thư điện tử",
                sms: "Tin nhắn sms"
            },
            timerjobtype: {
                warning: "Cảnh báo",
                searchindex: "Đánh chỉ mục tìm kiếm",
                deletetempfile: "Xóa bỏ các file tạm"
            },
            urgent: {
                thuong: "Thường",
                khan: "Khẩn",
                hoatoc: "Hỏa tốc"
            },
            quickview: {
                hide: "Không hiển thị",
                below: "Ở bên dưới",
                right: "Ở bên phải"
            },
            fontsize: {
                nho: "Chữ nhỏ",
                vua: "Chữ vừa",
                lon: "Chữ lớn"
            },
            notify: {
                documentnotifytype: {
                    hide: "Không hiển thị",
                    shownotifyinprocess: "Chỉ hiển thị thông báo văn bản chờ xử lý",
                    all: "Hiển thị tất cả thông báo văn bản có liên quan"
                },
                bmailnotifytype: {
                    hide: "Không hiển thị",
                    inbox: "Chỉ hiển thị thư cá nhân",
                    option: "Hiển thị trên các thư mục đã xem",
                    all: "Hiển thị tất cả thư nhận được"
                },
            }
        },
        documentNotifications: "Thông báo văn bản",
        emptyDocumentNotifications: "Bạn không có thông báo văn bản nào!",
        openAllDocument: "Mở tất cả văn bản được thông báo",
        downloaddesktopversion: "Tải bản desktop",
        gtv: "Kiểu gõ",
        notifications: "Thông báo",
        news: "Tin điều hành",
        newEmail: "Soạn thư",
        config: "Thiết lập",
        reply: "Gửi phản hồi",
        smallSize: "Xem cỡ nhỏ",
        mediumSize: "Xem cỡ vừa",
        largeSize: "Xem cỡ lớn",
        underPreview: "Xem trước bên dưới",
        rightPreview: "Xem trước bên phải",
        hidePreview: "Ẩn xem trước",
        reload: "Khởi động lại",
        logout: "Đăng xuất",
        searchDocument: "Tìm kiếm văn bản",
        searchFile: "Tìm kiếm file đính kèm",
        reloadMessage: "Một số thiết lập yêu câu phải reload lại hệ thống. Bạn có muốn relo…",
        closeBtn: "Đóng",
        submitBtn: "Cập nhật",
        titleMessage: "Thông báo!",
        closeAll: "Đóng tất cả lại",
        reportL: "Thống kê",
        contacts: "Sổ liên lạc",
        calendar: "Lịch",
        statictisLabel: "Giám sát, Thống kê",
        cbcl: "Chaas",
        reportLabel: "Báo cáo",
        chat: "Chat",
        documentslabel: "Xử lý công văn",
        placeholderSearch: "Tìm kiếm thông tin hồ sơ, văn bản, tệp đính kèm",
        administrator: "Quản trị hệ thống",
        links: "Liên kết",
        conversion: "Hội thoại",
        messageNoBtn: "Không",
        mailNotifications: "Thông báo mail",
        emptyMailNotifications: "Bạn không có thông báo mail nào",
        openAllMail: "Mở tất cả mail nhận được",
        chatNotifications: "Thông báo chat",
        emptyChatNotifications: "Bạn không có tin nhắn nào",
        openAllChat: "Mở tất cả tin nhắn nhận được",
        bmail: "Tin điều hành",
        notJqueryAlert: "Chưa có file jquery. Vui lòng tải thêm file jquery!",
        lblDocument: "Văn bản",
        lblNewConversion: "Hội thoại",
        lblNewWorkTime: "Tạo lịch",
        lblNewMail: "Soạn thư",
        searchMail: "Tìm kiếm mail",
        youHave: "Bạn có",
        unreadDocuments: "văn bản chưa xem",
        'delete': "Xóa",
        activityLog: {
            questionDelete: "Bạn có muốn xóa các nhật ký này không?",
            notChoice: "'Bạn chưa chọn nhật ký muốn xóa'"
        },
        level: {
            nodata: "Không có cấp hành chính nào"
        },
        license: {
            AddLicense: "Đăng ký",
            RegisterLicense: "Đăng ký bản quyền",
            customername: "",
            Phone: "Số điện thoại",
            Email: "Email",
            ToDate: "Ngày hết hạn",
            TotalUser: "Số tài khoản",
            key: "Mã kích hoạt"
        },
        log: {
            logNotSelect: "Bạn chưa chọn nhật ký muốn xóa",
            deleteSelection: "Xóa nhật ký được chọn",
            detail: "Chi tiết nhật ký"
        },
        notify: {
            noform: "Chưa có mẫu nào",
            nouse: "Không sử dụng",
            urgent: {
                name: "Độ khẩn",
                level1: "Bình thường",
                level2: "Khẩn",
                level3: "Thượng khẩn"
            },
            hasRead: "Được duyệt",
            alerttime: " phút (Đặt = 0 để gửi sms ngay khi có cuộc họp mới)",
            noJquery: "Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử d…",
            noUnderscore: "Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước …",
            openall: "Mở tất cả",
            closeall: "Đóng tất cả",
            nomailnotify: "Bạn không có thông báo mail nào.",
            nodocumentnotify: "Bạn không có thông báo văn bản nào",
            nochatnotify: "Bạn không có tin nhắn nào",
            valuemodelnotnull: "Giá trị model không được để null"
        },
        office: {
            nooffice: "Không có cơ quan nào"
        },
        paper: {
            nopaper: "Không có giấy tờ nào",
            other: "Khác",
            list: "Danh sách giấy tờ",
            action: "Nghiệp vụ",
            docfield: "Lĩnh vực",
            doctype: "Loại hồ sơ",
            addpaper: "Thêm mới giấy tờ",
            updatepaper: "Cập nhật giấy tờ"
        },
        people: {
            nopeople: "Không có người dùng nào",
            peoplesearch: "Tìm kiếm tài khoản "
        },
        position: {
            sorterror: "Có lỗi khi sắp xếp mức ưu tiên của chức vụ.",
            npposition: "Không có chức vụ nào",
            orderedsort: "Cho phép sắp xếp thứ tự bằng cách kéo thả"
        },
        printer: {
            addprinter: "Thêm mới máy in",
            editprinter: "Thiết lập cho máy in",
            nodata: "Không có máy in nào",
            notconnect: "Không kiểm tra kết nối được tới máy in!",
            nameisrequired: "Tên máy in không được để trống!"
        },
        processfunction: {
            selectcolor: "Chọn màu",
            user: "Người dùng",
            position: "Chức vụ",
            position1: "Phòng ban/Chức vụ",
            all: "Tất cả",
            failure: "Bạn chưa nhập đầy đủ thông tin trong phần cấu hình danh sách",
            setupforfilterlist: "Cấu hình danh sách bộ lọc cho node",
            setupforsamelist: "Cấu hình danh sách tương ứng với node",
            enternote: "(Dùng phím enter để xuống dòng (nếu ở dòng cuối cùng thì thêm dòng …",
            divrole: "Phân quyền",
            addnodefilter: "Thêm bộ lọc mới cho node",
            parameterlisthaverequirement: "Bạn chưa nhập đầy đủ thông tin trong phần danh sách tham số",
            docfieldnote1: " Nếu tham số có cột giá trị là DocFieldId thì hệ thống sẽ ",
            docfieldnote2: "ngầm hiểu là cây văn bản đó sẽ lọc theo lĩnh vực và loại hồ sơ",
            columnname: "Tên cột giá trị",
            paraname: "Tên tham số",
            updatenodetype: "Cập nhật loại node",
            document: "Hồ sơ công việc",
            addNode: "Thêm node mới",
            copyNode: "Copy node này",
            paste: "Dán node",
            'delete': "Xóa node này",
            confirmdeletenode: "Xóa 1 node sẽ xóa cả node con, bạn chắc chắn muốn xóa chứ?",
            deletenodesuccessfull: "Xóa node thành công!",
            nodata: "Không có loại node nào",
            nofilter: "Không có bộ lọc nào",
            nogroup: "Không có nhóm nào",
            alldocument: "---Tất cả loại văn bản, hồ sơ---",
            someinfoisrequired: "Bạn chưa nhập đầy đủ thông tin trong phần cấu hình danh sách",
            parent: "Cha",
            normal: "Bình thường",
            field: "Trường dữ liệu",
            type: "Kiểu",
            displayname: "Tên hiển thị",
            filterByOverdueDate: "Lọc theo hạn xử lý:",
            display: "Hiển thị",
            defaultdocumentsortconfig: "Cấu hình sắp xếp văn bản mặc định",
            entertobreakpage: "(Dùng phím enter để xuống dòng (nếu ở dòng cuối cùng thì thêm dòng …",
            configlistbynode: "Cấu hình danh sách tương ứng với node"
        },
        question: {
            nodata: "Không có câu hỏi nào",
            name: "Tên câu hỏi",
            content: "Nội dung câu hỏi",
            citizenname: "Tên công dân",
            email: "Email",
            phone: "Số điện thoại",
            uptohome: "Cho lên trang hành chính công",
            quickanswer: "Trả lời nhanh",
            answer: "Câu trả lời",
            btnAnswer: "Trả lời",
            transfer: "Chuyển cán bộ trả lời",
            date: "Thời gian hỏi",
            Compendium: "Trích yếu hồ sơ",
            reject: "Từ chối trả lời",
            rejectClause: "Lý do từ chối",
            rejectHolder: "Nhập lý do từ chối trả lời",
            rejectConfirm: "Xác nhận",
            searchUser: "Tìm tài khoản cán bộ",
            chooseuser: "Chọn cán bộ xử lý",
            forwarding: "Đang chuyển câu hỏi",
            forwardsuccess: "Chuyển câu hỏi thành công",
            btnShowDocumentDetail: "Xem chi tiết",
            listquestiontitle: "Danh sách câu hỏi",
            detail: "Chi tiết câu hỏi",
            showadvancededit: "Nâng cao",
            commentList: "Ý kiến cán bộ xử lý",
            compendium: "Trích yếu hồ sơ",
            transferComment: "Nhập ý kiến chuyển",
            defaultTransferComment: "Trả lời hộ nhe cưng",
            holding: "đang giữ câu hỏi",
            noquestion: "Không có câu hỏi nào"
        },
        report: {
            fileuploadrpt: "Chỉ cho phép tải lên tệp *.rpt",
            showguidewhenwritingsqlquery: "Hiển thị hướng dẫn soạn sql",
            showdatabygroup: "Cấu hình xem dữ liệu theo nhóm",
            showgroupinreporttree: "Cấu hình hiển thị nhóm lên cây báo cáo",
            _setting: "[cấu hình]",
            statisticSetting: "Cấu hình thống kê",
            permissionreadreport: "Phân quyền được xem báo cáo",
            privatesetting: "Cấu hình riêng",
            settingreport: "Sử dụng cấu hình báo cáo",
            nodata: "Không có nhóm báo cáo nào",
            deletesuccessfull: "Xóa báo cáo thành công",
            confirmdeletereport: "Bạn có chắc muốn xóa báo cáo này cùng với tất cả các báo cáo con củ…",
            config: "Cấu hình báo cáo",
            configsetup: "[cấu hình]",
            showguide: "Bật/tắt hướng dẫn",
            exprort: "Xuất ra file",
            group: "Nhóm",
            print: "In",
            view: "Xem",
            totalDocuments: "Tổng số văn bản:",
            totalDocumentNotProcessed: "Số văn bản chưa được xử lý:",
            totalDocumentProcessed: "Số văn bản đã được xử lý:",
            totalDocumentOverdue: "Số văn bản quá hạn:"
        },
        resource: {
            nodata: "Không có resource nào",
            choosefileimport: "Chọn tệp import"
        },
        role: {
            nodata: "Chưa có người dùng nào",
            isallow: "Cho phép",
            rolename: "Tên quyền",
            nodatagroup: "Không có nhóm người dùng nào"
        },
        scopearea: {
            nodata: "Không có ScopeArea nào"
        },
        shared: {
            productname: "Bkav eGov",
            systemtree: "Cây hệ thống",
            home: "Trang chủ",
            admincustomer: "Quản trị khách hàng"
        },
        store: {
            pts: "(Phụ trách sổ)",
            nouser: "Chưa có người dùng nào",
            tempforstore: "Danh sách mẫu cho sổ",
            alltempname: "Tất cả tên mẫu",
            notemp: "Không tồn tại mẫu nào!",
            _all: "[Tất cả]",
            nodocumentstore: "Không có sổ hồ sơ nào",
            choosecategory: "Chọn nghiệp vụ",
            addstoreviewer: "Thêm người xem sổ",
            addDocFields: "Thêm lĩnh vực",
            docField: "Lĩnh vực"
        },
        template: {
            nodata: "Không có mẫu nào",
            key: "Key dùng chung",
            systemerror: "Hệ thống lỗi, không thay đổi được trạng thái mẫu phiếu",
            printorder: "Phiếu in",
            per1: "Tiếp nhận",
            per2: "Bàn giao",
            per4: "Ký duyệt",
            per8: "Trả kết quả",
            per16: "Tiếp nhận bổ sung",
            per32: "Gia hạn",
            specialkey: "Key từ cuộc họp",
            keyfromform: "Key từ biểu mẫu",
            questionkey: "Key hỏi đáp",
            documentOnlineKey: "Key đăng ký qua mạng",
            dbkey: "Key lấy từ CSDL",
            commonKey: "Các key dung chung"
        },
        templatekey: {
            onoffguide: "Bật/tắt hướng dẫn",
            showguide: "Hiển thị hướng dẫn khi soạn mã key, sql, template",
            ex: "Vd:",
            needparameter: "Câu truy vấn phải có tham số",
            parametercanuseinquery: "Các tham số có thể sử dụng trong câu truy vấn",
            keyformat: "Định dạng key được phép",
            speccharacter: "{[a-zA-Z0-9_]+}",
            keyformat2: "bao gồm các chữ cái (hoa, thường), chữ số và dấu gạch chân (_).",
            getvalueintempdoc: "Lấy giá trị trong biểu mẫu nào của hồ sơ.",
            currentuserid: "Id người đăng nhập hiện tại.",
            doctype: "Loại giấy tờ.",
            costtype: "Loại lệ phí.",
            additiondoc: "Danh sách các giấy tờ bổ sung.",
            formatofresulequery: "Kết quả câu truy vấn được lấy theo dạng",
            fieldname: "ten_truong",
            sqlresult: "là kết quả của câu sql dạng",
            dataprocessfunctions: "Các hàm xử lý kiểu dữ liệu",
            exdataconvertfunctions: "Tất cả các hàm convert dữ liệu:",
            stringprocessingfuntions: "Tất cả các hàm xử lý chuỗi:",
            datefunctions: "Tất cả các hàm xử lý ngày tháng:",
            stringformats: "Tất cả các định dạng chuỗi:",
            viewdetail: "Xem chi tiết tại:",
            selectdocument: "Chọn loại hồ sơ",
            selecttemplate: "Chọn biểu mẫu",
            selectdocfield: "Chọn lĩnh vực",
            keycode: "Mã key",
            nokey: "Không có key nào"
        },
        transfertype: {
            nodata: "Không có hình thức chuyển nào"
        },
        user: {
            username: "Tên đăng nhập",
            fullname: "Họ và tên",
            phone: "Số điện thoại",
            usernameexist: "Tên đăng nhập đã tồn tại",
            notindepartment: "Chưa thuộc phòng ban nào",
            rolename: "Tên quyền",
            groupname: "Tên nhóm",
            isadministrator: "Là quản trị",
            ismaindepartment: "Là phòng ban chính",
            position: "Chức danh",
            position1: "Chức vụ",
            departmentname: "Tên phòng",
            nodata: "Không có người dùng nào",
            all: "---Tất cả---",
            active: "Hoạt động",
            unactive: "Không hoạt động",
            confirmtoresetpassword: "Bạn có chắc chắn muốn reset mật khẩu cho tài khoản này không?",
            resetpasswordsuccess: "Reset mật khẩu thành công!",
            selectusertoimport: "Bạn phải chọn người dùng để import",
            importusersuccessfull: "Import người dùng thành công",
            defaultPasswordRest: "Mật khẩu reset mặc định",
            clearToRandomData: "Bỏ trống để tạo mật khẩu ngẫu nhiên"
        },
        ward: {
            city: "Tỉnh/Thành phố:",
            district: "Quận/huyện:",
            nodata: "Không có xã/phường nào",
            updatedata: "Cập nhật xã/phường",
            datalist: "Danh sách xã/phường"
        },
        welcome: {},
        buttons: {
            select: "Chọn",
            selectAll: "Chọn tất",
            edit: "Sửa",
            'delete': "Xóa",
            orderedsort: "Lưu thứ tự sắp xếp",
            orderedSortHint: "Cho phép sắp xếp thứ tự bằng cách kéo thả",
            orderedsave: "Lưu thứ tự",
            addfilter: "Thêm bộ lọc mới",
            addparameter: "Thêm tham số",
            save: "Lưu",
            confirm: "Xác nhận",
            back: "Quay lại danh sách",
            config: "Cấu hình",
            close: "Đóng",
            deleteAll: "Xóa toàn bộ",
            deleteall: "Xóa hết",
            search: "Tìm kiếm",
            agree: "Đồng ý",
            ignore: "Bỏ qua",
            add: "Tạo mới",
            view: "Xem",
            copy: "Sao chép",
            rerunAll: "Chạy lại toàn bộ",
            testDVC: "Test dịch vụ công"
        },
        tableheader: {
            stt: "STT",
            'function': "Chức năng",
            description: "Mô tả",
            form: "Mẫu",
            edit: "Sửa",
            select: "Chọn",
            'delete': "Xóa",
            type: "Kiểu",
            formname: "Tên mẫu",
            filtername: "Tên bộ lọc",
            columnname: "Tên cột",
            justify: "Căn lề",
            displayname: "Tên hiển thị",
            width: "Chiều rộng",
            allowsort: "Cho phép sắp xếp",
            sortcolumn: "Cột sắp xếp",
            sort: "Sắp xếp",
            value: "Giá trị",
            name: "Name",
            domain: "Domain",
            ip: "ip",
            zone: "Vùng",
            isallow: "Cho phép",
            addordelete: "Thêm/Bỏ",
            sortcolumnname: "Tên cột sắp xếp",
            sorttype: "Kiểu sắp xếp(tăng hoặc giảm)",
            order: "Thứ tự",
            isallowsort: "Cho phép sắp xếp"
        },
        commonlabel: {
            list: "Danh sách",
            select: "Chọn",
            is: "Là",
            or: "Hoặc...",
            addcolumn: "Thêm cột",
            add: "Thêm",
            addnew: "Thêm mới",
            cancel: "Hủy bỏ",
            note: "*Lưu ý:",
            time: {
                date: "Ngày",
                _date: "ngày",
                minute: "Phút",
                _minute: "phút",
                mon: "Thứ 2",
                tue: "Thứ 3",
                wed: "Thứ 4",
                thi: "Thứ 5",
                fri: "Thứ 6",
                sat: "Thứ 7",
                sun: "Chủ nhật",
                morning: "Buổi sáng",
                affternoon: "Buổi chiều"
            },
            contact: "Liên hệ",
            errorpage: "Sorry, an error occurred while processing your request.",
            all: "Tất cả",
            email: "Email",
            sms: "SMS",
            printcard: "Phiếu in",
            vnconcurency: "vnd",
            reject: "Bỏ",
            yes: "Có",
            no: "Không",
            search: "Tìm",
            allow: "Cho phép",
            notallow: "Không cho phép",
            'delete': "Xóa",
            other: "Khác",
            haveerrortryagain: "Có lỗi xảy ra, vui lòng thử lại!",
            deincrease: "Giảm dần"
        },
        sitemap: {
            config: "Cấu hình",
            general: "Thiết lập hệ thống",
            processfunction: "Cây văn bản",
            resource: "Tài nguyên",
            activitylog: "Nhật ký hành động",
            egovjob: "Quản lý tiến trình của hệ thống ",
            config4: "Biểu mẫu",
            template: "Quản lý mẫu phiếu",
            templatekey: "Quản lý key",
            formgroup: "Quản lý nhóm biểu mẫu",
            form: "Quản lý biểu mẫu",
            embryonicform: "Quản lý mẫu phôi",
            config5: "Báo cáo",
            report: "Báo cáo động",
            notify: "Thông báo",
            config2: "Danh mục",
            categorybusiness: "Nghiệp vụ",
            docfield: "Lĩnh vực",
            doctype: "Loại văn bản",
            configworkflow: "Quy trình",
            category: "Hình thức văn bản",
            increase: "Nhảy số",
            code: "Bảng mã",
            store: "Sổ hồ sơ",
            catalog: "Danh mục tùy chọn",
            keyword: "Từ khóa",
            transfertype: "Hình thức gửi",
            time: "Thời gian làm việc",
            address: "Địa chỉ",
            config3: "Cơ cấu tổ chức",
            imfomation: "Cơ quan",
            department: "Phòng ban",
            jobtitles: "Chức danh",
            position: "Chức vụ",
            user: "Cán bộ",
            role: "Vai trò và quyền hạn",
            authorize: "Ủy quyền",
            client: "Client",
            people: "Người dùng",
            scopearea: "Vùng truy cập",
            config6: "eGovOnline",
            office: "Cơ quan",
            law: "Văn bản quy phạm",
            guide: "Hướng dẫn",
            question: "Câu hỏi",
            onlinetemplate: "Biểu mẫu hành chính",
            treeGroup: "Nhóm cây văn bản",
            permissionSetting: "Cấu hình quyền trên node",
            config7: "Sao lưu",
            docColumnSetting: "Cấu hình hiển thị danh sách",
            reportgroup: "Nhóm báo cáo",
            paper: "Giấy tờ",
            fee: "Lệ phí",
            requiredSupplementary: "Mẫu yêu cầu bổ sung",
            sharefolder: "Địa chỉ lưu trữ file sao lưu",
            backuprestoreconfig: "Sao lưu cơ sở dữ liệu",
            backuprestorehistory: "Lịch sử sao lưu cơ sở dữ liệu",
            backuprestorefileconfig: "Sao lưu thư mục",
            backupRestoreManager: "Quản lý tệp tin sao lưu",
            printer: "Quản lý máy in",
            interfaceConfig: "Giao diện"
        },
        docfield: {
            store: "Sổ hồ sơ",
            nodocumentstore: "Không có sổ hồ sơ nào"
        },
        crystalreport: {
            copyfromstatisticform: "Sao chép từ mẫu thống kê",
            copyfromreportform: "Sao chép từ mẫu báo cáo",
            reconfig: "Config lại từ đầu",
            save: "Lưu lại"
        },
        doctype: {
            othernodes: "Các nốt khác",
            addcontrol: "Thêm control",
            downloadworkflowerror: "Cõ lỗi khi tải quy trình",
            newworkflow: "Thêm quy trình mới",
            workflownameisrequired: "Bạn phải nhập tên quy trình",
            pasteworkflow: "Dán quy trình",
            usethisworkflow: "Dùng quy trình này",
            comfirmtocancel1: "Bạn có chắc muốn ",
            comfirmtocancel2: "bỏ ",
            comfirmtocancel3: "dùng quy trình này không?",
            update: "Cập nhật danh mục",
            editTemplateWorkflow: "Cấu hình giao diện",
            updatenode: "Câp nhật node",
            interfaceconfig: "Cấu hình giao diện",
            editthisworkflow: "Sửa quy trình này",
            copythisworkflow: "Copy quy trình này",
            deletethisworkflow: "Xóa quy trình này",
            confirmtodeltethisworkflow: "Bạn có chắc muốn xóa quy trình này không?",
            notyouthisworkflow: "Bỏ dùng quy trình này",
            workflowname: "Tên quy trình",
            exprisedate: "Hạn xử lý",
            date: "Ngày",
            addtemplateform: "Thêm mẫu phôi",
            noformdata: "Không có mẫu nào",
            doctypename: "Tên hồ sơ: ",
            docfield: "Lĩnh vực: ",
            status: "Trạng thái:",
            active: "Kích hoạt",
            notactive: "Không kích hoạt",
            noconfiguration: "Chưa có cấu hình",
            confirmtodeletereceivenode: "Bạn có đồng ý xóa node đến này không?",
            objectype: "Kiểu đối tượng",
            value: "Giá trị",
            'delete': "Xóa",
            receivepostlist: "Danh sách nhận thông báo",
            removeReceiveList: "Bạn có đồng ý xóa danh sách thông báo này không?",
            of: "Của",
            allpeople: "Tất cả mọi người",
            level: "Cấp",
            user: "Người sử dụng",
            alljobtitle: "Tất cả chức danh",
            alljobtitleof: "Tất cả chức danh của ",
            alljobtitleof1: "Tất cả ",
            alljobtitleof2: " của ",
            jobtitledepartment: "Chức danh-phòng ban",
            alluserof: "Tất cả user của ",
            fieldisrequired: "Bạn phải nhập giá trị",
            embryonicformname: "Tên mẫu phôi:",
            embryonicformlist: "Danh sách các mẫu phôi:",
            addnewform: "Thêm mẫu mới"
        },
        form: {
            updateform: "Cập nhật loại hồ sơ, văn bản",
            formname: "Tên mẫu",
            description: "Mô tả",
            tempkey: "Mẫu phôi",
            status: "Trạng thái",
            config: "Cấu hình",
            status1: " Đang sử dụng",
            status2: " Không sử dụng",
            status3: " Mẫu lưu tạm",
            configformtitle: "Title",
            addtitle: "Thêm Tiêu đề (nhãn)",
            showgrid: "Hiển thị đường kẻ",
            shownumber: "Hiển thị số",
            addrow: "Thêm dòng",
            chooseextendfield: "- - - Chọn trường mở rộng - - - ",
            choosedocumenttype: "- - - Chọn loại văn bản - - - ",
            title: "Cấu hình mẫu cho loại hồ sơ",
            brand: "Ô nhập liệu (nhãn)",
            references: "Quan hệ",
            account: "Tên đăng nhập",
            currentusername: "Tên người đăng nhập hiện tại",
            currentdepartment: "Tên đơn vị/bộ phận/phòng ban",
            docfieldname: "Tên lĩnh vực",
            doctypename: "Tên loại hồ sơ",
            doccode: "Mã hồ sơ",
            receivedate: "Ngày tiếp nhận",
            appointdate: "Ngày hẹn trả",
            template: "Mẫu:",
            insertspecialvalue: "Chèn các giá trị đặc biệt:",
            formgroup: "Nhóm mẫu:",
            formtype: "Loại mẫu:",
            searchText: "Từ khóa",
            submit: "Tìm kiếm"
        },
        guide: {
            nodata: "Không có bản hướng dẫn nào"
        },
        increase: {},
        infomation: {
            chageinfo: "Thay đổi thông tin"
        },
        jobtitles: {
            nodata: "Không có chức danh nào",
            dragordroptosort: "Cho phép sắp xếp thứ tự bằng cách kéo thả",
            sorterror: "Có lỗi khi sắp xếp mức ưu tiên của chức danh."
        },
        editor: {
            deletecol: "Xóa cột",
            deleterow: "Xóa dòng",
            insertabove: "Chèn trên",
            insertbelow: "Chèn dưới",
            insertleft: "Chèn trái",
            insertright: "Chèn phải",
            merge: "Hợp nhất",
            splitcolumn: "Chia cột",
            splitrow: "Chia dòng",
            update: "Cập nhật",
            all: "[Tất cả]"
        },
        law: {
            choosedocument: "Chọn văn bản",
            lawnumbercode: "Số kí hiệu",
            subContent: "Tóm tắt"
        },
        code: {
            choosedepartment: "Chọn phòng ban",
            name: "Nhảy số",
            config1: "Lấy ngày hiện tại, nếu nhỏ hơn 10 thì thêm số 0 đằng trước",
            config2: "Lấy ngày hiện tại",
            config3: "Lấy tháng hiện tại, nếu nhỏ hơn 10 thì thêm số 0 đằng trước",
            config4: "Lấy tháng hiện tại",
            config5: "Lấy năm hiện tại",
            config6: "Lấy 2 số cuối của năm hiện tại"
        },
        deparment: {
            choosejobtitle: "Chọn chức danh",
            chooseposition: "Chọn chức vụ",
            listuser: "Danh sách cán bộ thuộc phòng ban",
            nouser: "Chưa có người dùng nào",
            nodata: "Chưa có phòng ban",
            addsubdeparment: "Thêm mới phòng ban trực thuộc",
            deparmentinfo: "Thông tin phòng ban",
            deparmentname: "Tên phòng ban",
            updateinfo: "Cập nhật thông tin phòng ban",
            adduser: "Thêm người dùng vào phòng/ban",
            fullname: "Họ tên",
            isadmin: "Quản trị",
            jobtitle: "Chức danh",
            position: "Chức vụ",
            list: "Danh sách phòng ban",
            isprimary: "Phòng chính"
        },
        bkavmessagebox: {
            useshowtoreplacealert: "Sử dụng eGovMessage.show(message, title) để thay thế cho messageBox…",
            useshowtoreplaceconfirm: "Sử dụng eGovMessage.show(message, title, messageButtons.OkCancel) đ…",
            usenotificationtoreplacetemp: "Sử dụng eGovMessage.notification() để thay thế cho messageTemp().",
            closebutton: "X",
            yes: "Đồng ý",
            no: "Từ bỏ",
            ok: "Xác nhận",
            cancel: "Hủy bỏ",
            notify: "Thông báo"
        },
        workflow: {
            user: "Cán bộ",
            position: "Vị trí",
            relation: "Quan hệ",
            belowoffice: "Đơn vị cấp dưới",
            underoffice: "Đơn vị trực thuộc",
            currentoffice: "Đơn vị hiện tại",
            sameoffice: "Cùng đơn vị",
            peernode: "Node ngang hàng",
            sameparentnode: "Cùng Node cha",
            underuser: "Cấp dưới",
            overuser: "Cấp trên",
            addnotifyfouser: "Thêm user nhận thông báo",
            nodename: "Tên node",
            choosedepartment: "Chọn phòng ban",
            listuser: "Danh sách user",
            workflowTypes: "Loại con",
            addWorkflowType: "Thêm"
        },
        formtemplate: {
            columnwidth: "Chiều rộng cột",
            brandwidth: "Chiều rộng nhãn",
            height: "Chiều cao",
            disable: "Vô hiệu hóa",
            verifydata: "Có kiểm tra dữ liệu",
            compendium: "Trích yếu",
            comment: "Ý kiến xử lý",
            doctype: "Loại văn bản",
            category: "Hình thức",
            inoutplace: "Đơn vị",
            dateappointed: "Thời hạn xử lý",
            organization: "Cơ quan gửi",
            doccode: "Số/ký hiệu *",
            doccode2: "Số hiệu *",
            datearrived: "Ngày đến",
            dateresponse: "Hồi báo",
            datepublished: "Ngày ban hành",
            store: "Sổ hồ sơ",
            storeid: "Sổ văn bản",
            inoutcode: "Số đến đi",
            totalPage: "Số trang",
            choosetotalpage: "Chọn số trang",
            docfield: "Lĩnh vực",
            keyword: "Từ khóa",
            sendtype: "Hình thức gửi",
            doccode1: "Mã hồ sơ",
            citizenname: "Tên công dân",
            address: "Địa chỉ",
            phone: "Số điện thoại",
            docpapers: "Giấy tờ",
            identitycard: "Số CMT",
            email: "Thư điện tử",
            commune: "Xã phường",
            attachmentlist: "File đính kèm",
            relationlist: "Văn bản liên quan",
            cbdetail: "Hiển thị chi tiết văn bản đến",
            allcomment: "Nội dung xử lý",
            titlecontent: "Nội dung văn bản",
            urgent: {
                name: "Độ khẩn",
                normal: "Thường",
                fast: "Khẩn",
                important: "Hỏa tốc"
            },
            securityid: {
                name: "Độ mật",
                normal: "Thường",
                high: "Mật",
                important: "Tối mật",
                highest: "Tuyệt mật",

            },
            compendiumtitle: "Nhập trích yếu.",
            nocomment: "Chưa cho ý kiến",
            displayform: "Hiển thị biểu mẫu",
            storeprivate: "Hồ sơ cá nhân",
            storeshare: "Hồ sơ chia sẻ",
            nextpage: "Trang tiếp",
            prepage: "Trang trước",
            currentpage: "Trang",
            print: "In",
            btnfinish: "Kết thúc",
            viewicontraketqua: "Trả kết quả",
            viewicontiepnhanbosung: "Tiếp nhận bổ sung",
            viewiconhuyvanban: "Hủy",
            viewiconluu: "Lưu sổ",
            viewiconguiykien: "Gửi ý kiến",
            viewiconthongbao: "Thông báo",
            viewiconxinykien: "Xin ý kiến",
            viewiconyeucaubosung: "Yêu cầu bổ sung",
            viewicongiahanxuly: "Gia hạn",
            no: "Từ chối",
            yes: "Đồng ý",
            btninsertrelation: "Văn bản liên quan...",
            btninsertattachment: "Tệp đính kèm",
            btninsertscan: "Tệp scan...",
            btnpaper: "Giấy phép...",
            btninsertanticipate: "Dự kiến chuyển...",
            btntransfer: "Chuyển văn bản/hồ sơ",
            btnedit: "Sửa nội dung văn bản/hồ sơ",
            btninsertfile: "Đính kèm",
            btnapproveryes: "Đông ý phê duyệt",
            btnapproverno: "Từ chối phê duyệt",
            btndestroy: "Hủy văn bản/hồ sơ",
            viewiconketthuc: "",
            btnfinishtt: "Kết thúc",
            btnanswer: "Trả lời",
            btnchangedoctype: "Phân loại",
            concurrency: "Vnd",
            usercomment: "Người xử lý",
            filename: "Tên tệp",
            filesize: "Kích thước",
            fileversion: "Phiên bản",
            lastupdatefile: "Cập nhật cuối",
            finalcomment: "Ý kiến giải quyết",
            backtolist: "Quay lại danh sách",
            'delete': "Xóa",
            mainprocess: "Xử lý chính:",
            coprocess: "Đồng xử lý:",
            sendto: "Chuyển tới",
            thongbao: "Thông báo:",
            xinykien: "Xin ý kiến:",
            view: "Xem",
            download: "Tải về",
            placelabel: "Nơi nhận",
            officename: "Tên cơ quan",
            placeinoffice: "Nơi nhận trong đơn vị",
            approvers: "Người ký",
            docinpage: "S.bản / s.trang",
            inplace: "Nơi lưu bản gốc",
            publishreceive: "Danh sách nhận văn bản",
            createdate: "Ngày khởi tạo",
            createdate1: "Ngày tạo",
            panelselectorrequire: "Bạn phải truyền tham số panelSelector",
            publicoffice: "Cơ quan ban hành",
            insertanticipate: "Dự kiến chuyển",
            commoncomment: "Ý kiến thường dùng",
            receivedays: "Số ngày thụ lý",
            requirereport: "Yêu cầu hồi báo",
            fees: "Lệ phí",
            documentrelation: "Văn bản liên quan",
            attachment: "Tệp đính kèm",
            dateOverdue: "Hạn xử lý",
            content: "Nội dung",
            hasauthentication: {
                name: "Thẩm quyền",
                'true': "Thuộc thẩm quyền giải quyết",
                'false': "Không thuộc thẩm quyền giải quyết"
            },
            original: {
                name: "Nguồn đơn",
                direct: "Trực tiếp",
                topdown: "Từ trên chuyển xuống",
                other: "Từ nơi khác chuyển đến"
            },
            iscomplain: {
                name: "Phân loại đơn",
                'true': "Khiếu nại",
                'false': "Tố cáo"
            },
            dateCreated: "Ngày tiếp nhận",
            delayReason: "Lý do muộn",
            note: "Ghi chú khác",
            typeReturn: "Nơi trả hồ sơ"
        },
        catalog: {
            addbewobject: "Thêm đối tượng"
        },
        menu: "Danh mục",
        processdoc: "Văn bản chờ xử lý",
        userConfig: {
            saveSuccess: "Lưu thiết lập thành công",
            saveError: "Lưu thiết lập không thành công"
        },
        imagePacket: "Đính kèm ảnh theo lô",
        plugin: {
            noplugin: "Bạn chưa cài đặt plugin",
            pluginrequire: "Bạn cần tải về và cài đặt plugin này để sử dụng chức năng mở tệp đí…",
            needrestartbrowser: "Nếu bạn vẫn thấy thông báo này sau khi cài đặt plugin, hãy khởi độn…",
            downloadtosetup: "Tải về và cài đặt",
            waitforsetup: "Đang chờ cài đặt plugin..."
        },
        avatar: {
            nodata: "../../../AvatarProfile/noavatar.jpg",
            errorUrl: "../../../AvatarProfile/noavatar.jpg",
            icon: "../../../AvatarProfile/icon/i ({0}).png",
            troll: "../../../AvatarProfile/troll/t ({0}).png",
            alphabet: "../../../AvatarProfile/alphabet/{0}.png",
            path: "https://danhba.bkav.com/avatars/{0}.bmp"
        },
        mobile: {
            main: {
                documentSearch: "Nhập số ký hiệu, trích yếu,...",
                mailSearch: "Nhập từ khóa tìm kiếm",
                logout: "Đăng xuất",
                exit: "Thoát",
                reload: "Khởi động lại",
                config: "Thiết lập",
                createPersonMail: "Gửi mail cá nhân",
                createBoxMail: "Gửi tin vào đây",
                folderempty: "Làm rỗng mục tin",
                foldermarkread: "Đánh dấu tất cả là đã đọc",
                clearNotifies: "Xóa tất cả thông báo",
                nonotify: "Bạn không có thông báo nào"
            },
            usersetting: {
                loadavatar: "Tải ảnh đại diện",
                fullscreen: "Toàn màn hình",
                fullscreennode: "(khi cuộn trang)",
                appstart: "Ứng dụng khởi động",
                fontsize: "Cỡ chữ",
                fontsizeType: {
                    small: "Nhỏ",
                    normal: "Vừa",
                    large: "Lớn"
                },
                pagesizenode: "(chỉ cho mail)",
                pagesize: "Phân trang",
                language: "Ngôn ngữ",
                languagenode: "(cần khởi động lại)",
                fontfamily: "Font chữ",
                systemfont: "Mặc định",
                savelastapp: "Chạy sau cùng",
                appType: {
                    documents: "Văn bản",
                    bmail: "Thư",
                    chat: "Trò truyện",
                    calendar: "Lịch",
                    contacts: "Danh bạ"
                },
                notify: {
                    config: "Thông báo",
                    noNotify: "Tắt thông báo",
                    oneNotify: "Chỉ hiện thông báo cuối cùng",
                    allNotify: "Hiện tất cả thông báo"
                }
            },
            notify: {
                mailNotify: "Bạn có {0} thư mới.",
                chatNotify: "Bạn có {0} tin nhắn mới.",
                documentNotify: "Bạn có {0} văn bản thông báo.",
                calendarNotify: "Bạn có {0} lịch làm việc chưa xem.",
                documentNotFound: "Văn bản thông báo không tồn tại"
            },
            connectionError: "Máy chủ mail không thể kết nối"
        },
        kntc: "Khiếu nại tố cáo",
        syncDocType: "Đồng bộ công văn liên thông"
    };

    //egov.resources.transfer = extend(egov.resources.transfer, {
    //    dgUserLabelM: "Cá nhân, đơn vị nhận bản sao",
    //    dgJobtitleLabelM: "Chức vụ nhận bản sao",
    //    dgDeptLabelM: "Phòng ban nhận bản sao",
    //    dgUserGiamsatM: "Cán bộ giám sát",
    //});
    //#region bmail
    bmail.resources = {
        mailbox: {
            inbox: "Hộp thư đến",
            sent: "Thư đã gửi",
            drafts: "Thư nháp",
            junk: "Thư rác",
            trash: "Thư đã xóa"
        },
        toolbar: {
            'delete': "Xóa",
            deletePer: "Xóa vĩnh viễn",
            reply: "Trả lời",
            replyall: "Trả lời tất cả",
            forward: "Chuyển tiếp",
            edit: "Sửa",
            spam: "Đánh dấu là spam",
            unspam: "Bỏ đánh dấu là spam",
            read: "Đánh dấu là đã đọc",
            unread: "Đánh dấu là chưa đọc",
            restore: "Khôi phục thư đã xóa"
        },
        button: {
            ok: "Xác nhận",
            cancel: "Hủy bỏ",
            'delete': "Xóa",
            send: "Gửi",
            close: "Đóng"
        },
        notify: {
            success: "Thành công",
            error: "Lỗi"
        },
        common: {
            processing: "Đang tải",
            label: {
                time: "Thời gian"
            },
            time: {
                today: "Hôm nay",
                yesterday: "Hôm qua",
                date: "Ngày",
                _date: "ngày",
                minute: "Phút",
                _minute: "phút",
                mon: "Thứ 2",
                tue: "Thứ 3",
                wed: "Thứ 4",
                thi: "Thứ 5",
                fri: "Thứ 6",
                sat: "Thứ 7",
                sun: "Chủ nhật"
            },
            table: {
                stt: "STT",
                _stt: "Số thứ tự"
            },
            searchresult: "Kết quả tìm kiếm"
        },
        main: {
            newmail: "Thêm mới thư",
            sendto: "Gửi tới",
            cc: "Cc",
            bcc: "Bcc",
            subject: "Chủ đề",
            originmessage: "----- Thông điệp gốc : ----- ",
            send: "Gửi",
            close: "Đóng",
            confirmSave: "Lưu",
            notSave: "Không lưu",
            mailContentChange: "Nội dung thư thay đổi",
            savedraft: "Bạn có muốn lưu nháp thư đang soạn?",
            savedraftsuccess: "Lưu nháp thành công",
            savedrafterror: "Lưu nháp không thành công, vui lòng thử lại sau."
        },
        detail: {
            time: "Thời gian",
            sendto: "Gửi tới",
            sendfrom: "Gửi từ",
            subject: "Chủ đề",
            nosubject: "Không có chủ đề",
            file: {
                toolarge: "Không đính kèm được file có kích thước quá lớn",
                tryagain: "Lỗi không đính kèm được file. Mời bạn thử lại."
            },
            fieldsrequire: "Nhiều thông tin không được để trống.",
            uploaderror: "Lỗi tải tập tin lên, vui lòng thử lại.",
            confirmdelete: "Bạn có muốn xóa thư này?",
            content: "Nội dung thư",
            messageorigin: " ----- Thông điệp gốc: ----- ",
            downnloadAll: "Tải xuống tất cả",
            attachList: "Danh sách tệp đính kèm"
        },
        savedraft: {
            success: "Lưu nháp thành công",
            error: "Lưu nháp thất bại, vui lòng thử lại sau."
        },
        sendmail: {
            success: "Đã gửi",
            sendding: "Đang gửi",
            error: "Không thể gửi tin, vui lòng thử lại sau."
        },
        error: {
            hasError: "Có lỗi xảy ra, vui lòng thử lại sau.",
            connectionError: "Kết nối đến máy chủ thất bại, vui lòng thử lại sau."
        }
    }
    //#endregion
})
(window, window.egov = window.egov || {}, window.bmail = window.bmail || {});

(function (egov, Offline) {

    "use strict";

    var RequestManager = function () {
        /// <summary>
        /// Đối tượng quản lý các ajax http request từ client lên server.
        /// </summary>
        var check;

        this.dataDefaults = {
            type: 'GET',
            async: true,
            traditional: false,
            //global: false
        }

        // Kiểm tra có url trùng nhau
        // Khi thêm mới 1 query nếu trùng sẽ bị báo ngay.
        check = _.uniq(_.map(_queries, function (q) {
            return q !== undefined && q.url.trim();
        }));

        if (check.length !== _queries.length) {
            console.log("Lặp url, kiểm tra lại để đảm bảo không bị lặp: ");
        }

        // Kiểm tra tên trùng nhau
        check = _.uniq(_.map(_queries, function (q) {
            return q !== undefined && q.name.trim();
        }));

        if (check.length !== _queries.length) {
            console.log("Lặp tên query, kiểm tra lại để đảm bảo không bị lặp");
        }

        this.model = _queries;

        this.setDefaultAjaxSetting();
        this.init();
    }

    RequestManager.prototype.setDefaultAjaxSetting = function () {
        /// <summary>
        /// Xử lý các thiết lập chung cho mỗi ajax http request
        /// </summary>

        // Xử lý lỗi mặc định
        //$(document).ajaxError(function (e, jqXHR) {
        //    //if (jqXHR.status === 200) {

        //    //    // Xử lý các mã lỗi ở đây
        //    //    // window.location.replace(egov.getRelativeEndpointUrl('/Error.html'));
        //    //    egov.log(jqXHR);
        //    //}
        //});
    }

    RequestManager.prototype.init = function () {
        /// <summary>
        /// Render các function theo cấu hình
        /// </summary>
        /// <returns type="this"></returns>
        var that = this, name;

        //chưa danh sách ajax property khi run ajax call dữ liêu => để có thể phương thức ajax
        this.aborts = {};

        // Render các function theo tên các query trong danh sách
        _.each(this.model, function (query) {
            name = query['name'];
            that[name] = function (ajaxOption) {
                /// <summary>
                /// Tự động tạo hàm theo tên của query.
                /// </summary>
                /// <param name="ajaxOption" type="object">jQuery ajax option</param>

                // closure: query
                var processName = query.name + JSON.stringify(ajaxOption.data) + "processing";
                that._exeQuery(query, ajaxOption);
                //if (!that[processName]) {
                //    that._exeQuery(query, ajaxOption);
                //} else {
                //    egov.log("Gọi lặp chức năng " + query.name);
                //}
            }
        });

        return this;
    }

    RequestManager.prototype._exeQuery = function (query, ajaxOption) {
        /// <summary>Private: hàm thực thi một query</summary>
        /// <param name="query" type="QueryModel">Query cần thực thi</param>
        /// <param name="ajaxOption" type="object">Ajax option</param>
        var type,
            callerOptions,
            defaultOptions,
            tokenId,
            processName,
            beforeSendOption,
            completeOption,
            successOption,
            that = this;

        processName = query.name + JSON.stringify(ajaxOption.data) + "processing";
        defaultOptions = $.extend({}, that.dataDefaults);

        if (query['async'] !== undefined) {
            defaultOptions.async = query['async'];
        }
        defaultOptions.async = true;

        if (query['traditional'] !== undefined) {
            defaultOptions.dataType = 'json';
            defaultOptions.traditional = query['traditional'];
        }


        //HopCV:
        //todo: Cần xem lại chỗ này vì tocken có phân biệt hoa thường mà thăng url thì không=> nếu url mà nhập linh tinh=> có token sẽ lỗi
        // note: Khi nhập url cần chú ý
        if (query['hasToken'] !== undefined) {
            tokenId = '#' + query['url'].replace(/\//g, '');
            ajaxOption.data['__RequestVerificationToken'] = $("input[name='__RequestVerificationToken']", tokenId).val();
        }

        beforeSendOption = ajaxOption.beforeSend;
        ajaxOption.beforeSend = function () {
            that[processName] = true;
            if (typeof beforeSendOption === 'function')
                beforeSendOption();
        };

        successOption = ajaxOption.success;
        ajaxOption.success = function (result) {
            delete that[processName];
            if (typeof successOption === 'function')
                successOption(result);
        };

        completeOption = ajaxOption.complete;
        ajaxOption.complete = function (xhr) {
            delete that[processName];
            if (typeof completeOption === 'function')
                completeOption();
        };

        ajaxOption.data = ajaxOption.data || {};
        callerOptions = $.extend({}, defaultOptions, query, ajaxOption);

        //Overide lại XmlhttpRequest để lấy trạng thái kết nối của request đấy được trả về để đánh trạng thái kết nối
        if (Offline && !egov.isMobile) {
            Offline.options.checks = {
                xhr: callerOptions
            };
        }

        this.aborts[query['name']] = $.ajax(callerOptions);
    }

    //#region Private Fields

    // Danh sách tất cả các query từ client lên server
    var _queries = [

        //#region Common

        // Lấy danh sách tất cả user trong hệ thống
        { name: 'getAllUsers', url: '/Common/GetAllUsers' },

        // Lấy danh sách tất cả category trong hệ thống
        { name: 'getCategories', url: '/Common/GetCategories' },

        // Lấy danh sách tất cả department theo user trong hệ thống
        { name: 'getDepartmentsByUser', url: '/Common/GetDepartmentsByUser' },

        // Lấy danh sách tất cả department trong hệ thống
        { name: 'getAllDepartment', url: '/Common/GetAllDepartment' },

        // Lấy danh sách tất cả jobtitle trong hệ thống
        { name: 'getAllJobTitlies', url: '/Common/getAllJobTitlies' },

        { name: 'getAllUserDepartmentJobTitlesPosition', url: '/Common/GetAllUserDepartmentJobTitlesPosition' },

        // Lấy ra danh sách tất cả các chức vụ trong hệ thống
        { name: 'getAllPosition', url: '/Common/GetAllPosition' },

        { name: 'getAllAddress', url: '/Common/GetAllAddress' },

        // Lấy danh sách tất cả user trong hệ thống
        { name: 'getKeywords', url: '/Common/GetKeywords' },

        // Lấy danh sách tất cả user trong hệ thống
        { name: 'getDocField', url: '/Common/GetDocField' },

        { name: 'getSendTypes', url: '/Common/GetSendTypes' },

        { name: 'getDeptAndUsers', url: '/Common/GetDeptAndUsers' },

        // Lấy danh sách bảng mã.
        { name: 'GetCodes', url: '/Common/GetCodes', type: 'Get' },

        // Lấy danh sách cơ quan ban hành của đơn vị hiện tại.
        { name: 'getOrganizations', url: '/Common/GetOrganizations', type: 'Get' },

        //#endregion

        //#region Văn bản

        //Sửa văn bản
        { name: 'editNew', url: '/Document/EditNew/', type: 'Get' },

        // Lấy nội dung form
        { name: 'getFormContent', url: '/Document/GetFormContent' },

        // Lấy nội dung form
        { name: 'getFormUrl', url: '/Document/GetFormUrl' },

        // Xác nhận bàn giao
        { name: 'confirmTransfer', url: '/Document/ConfirmTransfer', type: 'Post', hasToken: true },

        // Xác nhận xử lý
        { name: 'confirmProcess', url: '/Document/ConfirmProcess', type: 'Post', hasToken: true },

        // Lấy danh sách văn bản liên quan
        { name: 'getDocumentRelations', url: '/document/GetDocumentRelations', type: 'Get' },

        // Gia hạn xử lý hồ sơ
        { name: 'renewals', url: '/Document/Renewals', type: 'Post' },

        // Lấy quyền thao tác xử lý văn bản hồ sơ
        { name: 'getDocumentPermission', url: '/Document/GetDocumentPermission', traditional: true, async: true },

        // Sửa văn bản
        { name: 'getDocumentInfoForEdit', url: '/Document/GetDocumentDetail' }, // url: '/Document/GetDocumentInfoForEdit'

        // Mở văn bản mobile
        { name: 'getDocumentInfoForMobile', url: '/Document/getDocumentInfoForMobile' },

        // Tạo văn bản
        { name: 'getDocumentInfoForCreate', url: '/Document/GetDocumentInfoForCreate' },

        // Lấy lại văn bản ở các node
        { name: 'getContextItemForUndoTransfering', url: '/Document/GetContextItemForUndoTransfering/' },

        // Xóa văn bản/hồ sơ
        { name: 'removeDocument', url: '/Document/RemoveDocument', type: 'Post', hasToken: true, traditional: true, },

        // Lấy lại văn bản
        { name: 'undoTransfering', url: '/Document/UndoTransfering', type: 'Post', hasToken: true },

        // Lấy những người nhận văn bản để undo lại
        { name: 'getUsersForUndoTransfering', url: '/Document/GetUsersForUndoTransfering' },

        // Set trang thái đọc văn bản
        { name: 'setDocumentViewed', url: '/Document/SetViewed/', type: 'Post' },

        // Sửa hồ sơ đăng ký trực tuyến
        { name: 'editDocumentOnline', url: '/Document/EditDocumentOnline/' },

        // Sửa nội dung hồ sơ
        { name: 'editDocumentContent', url: '/Document/EditContent/', type: 'Post' },

        // Trả về các phiên bản của nội dung văn bản
        { name: 'getDocumentContentVersion', url: '/Document/getDocumentContentVersion', type: 'Get' },

        // Lay y kien thuong dung cua nguoi dung
        { name: 'getCommonComments', url: '/Document/GetCommonComments/' },

        // Lay y kien thuong dung cua nguoi dung
        { name: 'getTemplateComments', url: '/Document/GetTemplateComments/' },

        { name: 'createTemplateComments', url: '/Document/CreateTemplateComments/', type: 'Post' },

        //Cap nhat mẫu template ý kiến mà người dùng đã soạn mẫu trước
        { name: 'updateTemplateComments', url: '/Document/UpdateTemplateComments', type: 'post' },

        //Tao moi mẫu template ý kiến mà người dùng đã soạn mẫu trước
        { name: 'deleteTemplateComments', url: '/Document/DeleteTemplateComments', type: 'post' },

        // Tim kiem van ban
        { name: 'searchDocuments', url: '/Document/SearchDocuments', type: 'Post', traditional: true },

        // Thiết lập văn bản quan trong hay không quan trọng
        { name: 'setDocumentImportant', url: '/Document/SetDocumentImportant', type: 'Post' },

        // Lấy thông tin chi tiết văn bản hồ sơ
        { name: 'getDocumentDetail', url: '/Document/GetDocumentDetail', type: 'Get' },

        // Xem trước thông tin văn bản hồ sơ
        { name: 'quickViewDocument', url: '/Home/QuickViewDocument', type: 'Get' },

        // Lấy file cấu hình form văn bản
        { name: 'getDocumentTemplate', url: '/Document/GetDocumentTemplate', type: 'Get' },

        // Cập nhật kết quả xử lý cuối cùng
        { name: 'updateLastResult', url: '/Document/UpdateLastResult', type: 'Post' },

        // Tạo ảnh từ file PDf, trả về url ảnh
        { name: 'createImagesFromBeginAndLastPdfPages', url: '/Document/CreateImagesFromBeginAndLastPdfPages', type: 'Get' },

        { name: 'GetDocumentInfoFromScan', url: '/Parallel/GetDocumentInfoFromScan', type: 'Get', traditional: true },

         // trả về url ảnh
        { name: 'getImageTemp', url: '/Document/GetImageTemp', type: 'Get' },

        // Hủy số đã cấp
        { name: 'cancelCode', url: '/Document/CancelCode', type: 'Get' },

        // Lấy danh sách số ký hiêu, mã hồ sơ
        { name: 'GetDocCodes', url: '/Document/GetDocCodes', type: 'Get' },

        // Lấy danh sách số đến đi
        { name: 'GetInOutCode', url: '/Document/GetInOutCode', type: 'Get' },

        // Lấy trạng thái phát hành
        { name: 'GetIsTransferPublish', url: '/Document/GetIsTransferPublish', type: 'Get' },

        // Xóa doc paper
        { name: 'deleteDocPaper', url: '/Document/DeleteDocPaper', type: 'Post' },

        // Xóa doc fee
        { name: 'deleteDocFee', url: '/Document/DeleteDocFee', type: 'Post' },

        // Lấy loại hồ sơ, văn bản
        { name: 'getDoctype', url: '/Doctype/GetDocType' },

        //Lay loai van ban
        { name: 'getDocTypes', url: '/Doctype/GetDocTypes' },

        // Trả về giấy tờ và lệ phí của doctype
        { name: 'getDoctypePaperAndFees', url: '/Doctype/GetPaperAndFees' },

        // Cập nhật giấy tờ và lệ phí của loại hồ sơ
        { name: 'updateDoctypePaperAndFees', url: '/Doctype/UpdatePaperAndFees', type: 'Post' },

        // Xóa doctype paper
        { name: 'deleteDoctypePaper', url: '/Document/DeleteDoctypePaper', type: 'Post' },

        // Xóa doctype paper
        { name: 'deleteDoctypeFee', url: '/Document/DeleteDoctypeFee', type: 'Post' },

        // Kiểm tra số ký hiệu đã được dùng
        { name: 'checkDocCodeIsUsed', url: '/Document/CheckDocCodeIsUsed' },

        //#endregion

        //#region Đính kèm

        // Tải về tệp đính kèm mới tải lên.
        { name: 'downloadAttachmentTemp', url: '/Attachment/DownloadAttachmentTempBase64', type: 'Get' },

        // Tải về tệp đính kèm có sẵn
        { name: 'downloadAttachment', url: '/Attachment/DownloadAttachmentBase64', type: 'Get' },

        // Tải về tệp đính kèm để ký
        { name: 'downloadAttachmentForSignBase64', url: '/Attachment/DownloadAttachmentForSignBase64', type: 'Get', traditional: true },

        //Upload file scan
        { name: 'uploadTempScan', url: '/Attachment/UploadTempScan', type: 'Post' },

        //#endregion

        //#region Workflow

        // Lấy danh sách các hướng chuyển với văn bản đang edit
        { name: 'getDocumentEditAction', url: '/Workflow/GetActionsEdit', type: 'Get' },

        // Lấy danh sách các hướng chuyển với văn bản tạo mới
        { name: 'getDocumentCreateAction', url: '/Workflow/GetActionsCreate', type: 'Get' },

        // Lấy danh sách người nhận theo hướng chuyển
        { name: 'getUserByAction', url: '/Workflow/GetUserByAction', type: 'Get' },

        // Lấy các hướng chuyển của người dùng theo duwj kieen
        { name: 'getActionsTransferPlan', url: '/Workflow/GetActionsTransferPlan', type: 'Get' },

         // Lấy danh sách người nhận theo hướng chuyển theo lo
        { name: 'getUserByActionTheoLo', url: '/Workflow/GetUserByActionTheoLo', type: 'post', traditional: true },

        // Lấy danh sách hướng chuyển theo lo
        { name: 'getActionTheoLoVanBan', url: '/Workflow/GetActionTheoLoVanBan', type: 'post', traditional: true, async: true },

        //#endregion

        //#region Xử lý văn bản

        // Chuyển văn bản
        { name: 'transfer', url: '/Transfer/Transfer', type: 'Post', traditional: true, hasToken: true },

        // Chuyen theo lo
        { name: 'transferTheoLo', url: '/Transfer/TransferTheoLo', type: 'Post', traditional: true, hasToken: true },

        // Chuyển văn bản
        { name: 'lightTransfer', url: '/Transfer/LightTransfer', type: 'Post' },

        // Chuyển ý kiến đóng góp: cho văn bản xin ý kiến, đồng xử lý.
        { name: 'TransferYKienDongGop', url: '/Transfer/TransferYKienDongGop', type: 'Post', traditional: true, hasToken: true },

        // Tiếp nhận hồ sơ.
        { name: 'TransferTiepNhan', url: '/Transfer/TransferTiepNhan', type: 'Post', traditional: true, hasToken: true },

        // Thông báo
        { name: 'TransferAnnouncement', url: '/Transfer/TransferThongBao', type: 'Post', traditional: true, hasToken: true },

        // Xin ý kiến
        { name: 'TransferConsult', url: '/Transfer/TransferXinYKienToolbar', type: 'Post', traditional: true, hasToken: true },

        // Xin ý kiến trên contextmenu trang chính
        { name: 'TransferConsultContext', url: '/Transfer/TransferXinYKienContext', type: 'Post', traditional: true, hasToken: true },

        // Lưu sổ và phát hành văn bản
        { name: 'publish', url: '/Transfer/TransferPublish', type: 'Post', traditional: true, hasToken: true },

        // Lưu sổ và phát hành nội bộ
        { name: 'privatePublish', url: '/Transfer/TransferPrivatePublish', type: 'Post', traditional: true, hasToken: true },

        // Lưu sổ và phát hành nội bộ theo lô
        { name: 'privatePublishTheoLo', url: '/Transfer/TransferPrivatePublishTheoLo', type: 'Post', traditional: true, },

        // Lưu sổ và phát hành văn bản theo lô
        { name: 'publishTheoLo', url: '/Transfer/TransferPublishTheoLo', type: 'Post', traditional: true, hasToken: true },

        // Dự kiến phát hành
        { name: 'publishmentPlan', url: '/Transfer/PublishmentPlan', type: 'Post' },

        // Cập nhật văn bản
        { name: 'saveDoc', url: '/Transfer/SaveDoc', type: 'Post', hasToken: true, traditional: true },

        // Lưu văn bản dự thảo
        { name: 'saveDocDraft', url: '/Transfer/SaveDocDraft', type: 'Post', hasToken: true },

        // Lưu văn bản dự thảo
        { name: 'transferLienThong', url: '/Transfer/transferLienThong', type: 'Post', hasToken: true, traditional: true },

        // Gửi liên thông lại
        { name: 'resendLienThong', url: '/Transfer/ResendLienThong', type: 'Post', hasToken: true, traditional: true },

        // Gửi ý kiến
        { name: 'sendComment', url: '/Document/SendComment', type: 'Post', hasToken: true },

        // Kết thúc xử lý văn bản
        { name: 'finish', url: '/Finish/UpdateFinish', type: 'Post', hasToken: true },

         // Lấy lại văn bản đã kết thúc
        { name: 'undoFinish', url: '/Finish/UndoFinish', type: 'Post' },

        // Phát hành tiếp
        { name: 'rePublish', url: '/Transfer/RePublish', type: 'Post' },

        // Ký duyệt
        { name: 'approverSend', url: '/Approver/Send', type: 'Post', hasToken: true },

        { name: 'deleteApprover', url: '/Approver/deleteApprover', type: 'Post', hasToken: true },

        { name: 'deleteResult', url: '/Document/DeleteResult', type: 'Post', hasToken: true },

        //#endregion

        //#region hỏi đáp

        { name: 'getNodeQuestion', url: '/Question/GetNode', type: 'Get' },

        { name: 'getsQuestion', url: '/Question/GetQuestions', type: 'Get' },

        { name: 'answerQuestion', url: '/Question/Answer', type: 'POST' },

        { name: 'rejectQuestion', url: '/Question/Reject', type: 'POST' },

        { name: 'forwardQuestion', url: '/Question/ForwardQuestion', type: 'POST' },

        { name: 'rejectAnswer', url: '/Question/RejectAnswer', type: 'POST' },

        { name: 'getForwardList', url: '/Question/GetForwardList', type: 'Get' },

        { name: 'getsHolderList', url: '/Question/GetsHolderList', type: 'Get' },

        //#endregion

        //#region Hồ sơ cá nhân

        // Lấy danh sách Sổ văn bản
        { name: 'GetStores', url: '/Common/GetStores', type: 'Get' },

        // Lấy danh sách hồ sơ cá nhân, hồ sơ chia sẻ
        { name: 'getStorePrivate', url: '/StorePrivate/Gets', type: 'Get' },

        // Tạo mới hồ sơ cá nhân,hồ sơ chia sẻ
        { name: 'createStorePrivate', url: '/StorePrivate/Create', type: 'Post', hasToken: true, traditional: true },

        // Cập nhật hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'updateStorePrivate', url: '/StorePrivate/Update', type: 'Post', hasToken: true, traditional: true },

        { name: 'anycStoreShare', url: '/StorePrivate/AnycStoreShare', type: 'Get' },

        // Mở hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'openStorePrivate', url: '/StorePrivate/Open', type: 'Post', hasToken: true },

        // Đóng hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'closeStorePrivate', url: '/StorePrivate/Close', type: 'Post', hasToken: true },

        // Xóa hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'deleteStorePrivate', url: '/StorePrivate/Delete', type: 'Post', hasToken: true },

        // Lấy danh sách văn bản hồ sơ trong hồ sơ cá nhân, hồ sơ chia sẻ
        { name: 'getStorePrivateDocuments', url: '/StorePrivate/GetDocuments', type: 'Post' },

        // Xóa văn bản ra khỏi hồ sơ
        { name: 'removeStorePrivateDocument', url: '/StorePrivate/RemoveDocuments', type: 'Post', traditional: true },

        //Lấy danh sách người dùng trong hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'getUserJoined', url: '/StorePrivate/GetUserJoined', type: 'Get' },

        //Thêm văn bản vào hồ sơ cá nhân/chia sẻ
        { name: 'SaveDocumentToStorePrivate', url: '/Document/SaveDocumentToStorePrivate', type: 'Post', hasToken: true },

        // Mở file từ hồ sơ cá nhân, hồ sơ chia sẻ
        { name: 'storePrivateOpenFile', url: '/StorePrivate/DownloadAttachmentBase64', type: 'Get' },

        // Xoá file trong hồ sơ
        { name: 'storePrivateRemoveFile', url: '/StorePrivate/RemoveAttachment', type: 'post', hasToken: true },

        // Loại hồ sơ khỏi hồ sơ thông báo
        { name: 'removeDocumentAnnouncement', url: '/Document/RemoveDocumentAnnouncement', type: 'post' },

        //#endregion

        //#region Danh sách văn bản

        // Lấy danh sách văn bản theo kho
        { name: 'getDocumentStore', url: '/Home/GetDocumentStore' },

        //#endregion

        //#region Cây văn bản

        //Lấy danh sách cây văn bản
        { name: 'getDocumentTree', url: '/Home/GetFunctionByParentId', type: 'get' },

        //Lấy danh sách cây văn bản có hướng chuyển theo lô
        { name: 'getDocumentTreeHasTransferTheoLo', url: '/Home/GetFunctionHasTransferTheoLoByParentId', type: 'get' },

        // Lấy danh sách cây văn bản
        { name: 'syncDocumentStore', url: '/Home/SyncDocumentStore' },

        // Lấy danh sách các kho văn bản
        { name: 'getFunctionGroups', url: '/Home/GetFunctionGroups' },

        //#endregion

        //#region Home

        // Lấy các cấu hình của người dùng
        { name: 'getCommonConfigs', url: '/Home/GetCommonConfigs', type: 'Get' },

        // Lấy các cấu hình bỏ lưu sổ
        { name: 'hasHideSaveStore', url: '/Home/HasHideSaveStore', type: 'Post' },

        // Lấy ngày hết hạn
        { name: 'getDateAppointed', url: '/Document/GetDateAppointed', type: 'Post' },

        // Thiết lập các config của người dùng
        { name: 'setUserConfig', url: '/Account/SetUserConfig/', type: 'Post' },

        // Thiết lập các config của người dùng
        { name: 'setPopUpSize', url: '/Account/setPopUpSize/', type: 'Post' },

        { name: 'filterCitizen', url: '/Document/FilterCitizen/', type: 'Get' },

        // Trang in
        { name: 'print', url: '/Print/Index', type: 'Get' },

        { name: 'previewPrint', url: '/Print/PreviewPrint', type: 'Get' },

        // Trả về danh sách các phiếu in của hồ sơ 
        { name: 'getPrints', url: '/Print/GetPrints', type: 'Get' },

        // Trả về danh sách các mẫu phiếu in theo nghiệp vụ
        { name: 'getPrintTemplates', url: '/Print/GetPrintTemplates', type: 'Get' },

        // Trả về danh sách các mẫu phiếu in theo danh sách hồ sơ
        { name: 'getPrintByDocCopys', url: '/Print/GetPrintByDocCopys', type: 'Get' },

        // In phiếu biên nhận
        { name: 'quickPrint', url: '/Print/QuickPrint', type: 'Get' },

        { name: 'getPrinters', url: '/Print/GetActivePrinters', type: 'Get' },

        { name: 'printTransferHistory', url: '/Print/PrintTransferHistory', type: 'Get' },

        // Trả về dữ liệu cho form trả kết quả
        { name: 'getReturnResult', url: '/Return/GetReturnResult', type: 'Get' },

        // Trả về dữ liệu cho form trả kết quả
        { name: 'updateReturn', url: '/Return/UpdateReturn', type: 'Post' },

        // Form yêu cầu bổ sung mới
        { name: 'createSupplementary', url: '/Supplementary/CreateSupplementary', type: 'Get' },

        // Tiếp nhận bổ sung
        { name: 'receiveSupplementary', url: '/Supplementary/GetDetails', type: 'Get' },

        // Tiếp nhận bổ sung - Posts
        { name: 'supplementaryReceive', url: '/Supplementary/Receive', type: 'Post' },

        // Trả về ngày hẹn trả mới khi tiếp nhận bổ sung
        { name: 'getNewDateAppointed', url: '/Supplementary/GetDateAppointed', type: 'Get' },

        // Tạo yêu cầu bổ sung hồ sơ
        { name: 'sendRequiredSupplementary', url: '/Supplementary/SendRequire', type: 'Post', hasToken: true },

        // Hủy yêu cầu bổ sung
        { name: 'cancelReceiveSupplementary', url: '/Supplementary/CancelReceive', type: 'Post', hasToken: true },

        // Tiếp tục xử lý
        { name: 'continueProcess', url: '/Supplementary/continueProcess', type: 'Post' },

        //Tìm kiếm nhanh
        { name: 'quickSearch', url: '/Search/QuickSearch', type: 'Get', traditional: true },

        { name: 'getMailTemplates', url: '/Document/GetMailTemplates', type: 'Get', },

        { name: 'getSmsTemplates', url: '/Document/GetSmsTemplates', type: 'Get', },

        { name: 'sendMailToPeople', url: '/Document/SendMailToPeople', type: 'Post', },

        { name: 'sendSmsToPeople', url: '/Document/SendSmsToPeople', type: 'Post', },

        { name: 'getVersionValue', url: '/Home/GetVersionValue', type: 'Get' },


        //#endregion

        //#region Khác - kiểm tra lại nếu không dùng thì bỏ

        // Lấy ra tổng số các văn bản hồ sơ chưa đọc
        { name: 'getTotalDocumentUnreadMultiFunction', url: '/Parallel/GetTotalDocumentUnreadMultiFunction', type: 'Post' },

        { name: 'getTotalDocumentUnread', url: '/Home/GetTotalDocumentUnread', type: 'Post' },

        // Lấy danh sách hồ sơ, văn bản
        { name: 'getDocuments', url: '/home/GetDocuments', type: 'Post' },

        // Lấy toàn bộ danh sách văn bản của node hiện tại
        { name: 'getAllDocument', url: '/Home/GetAllDocument', type: 'Post' },

        // Lấy danh sách hồ sơ văn bản theo phân trang
        { name: 'getDocumentPaging', url: '/Home/GetDocumentPaging', type: 'Post' },

        // Lấy danh sách hồ sơ, văn bản mới được thêm vào node và những văn bản xóa khỏi node
        { name: 'getLastestDocuments', url: '/Home/GetLastestDocuments', type: 'Post', traditional: true },

        //Tìm kiếm nâng cao
        { name: 'searchAdvance', url: '/Search/SearchAdvance', type: 'Get', traditional: true },

        //lấy form tìm kiếm nâng cao
        { name: 'getSearchAdvanceForm', url: '/Search/GetSearchAdvanceForm', type: 'Get', },

        //lấy form tìm kiếm nâng cao
        { name: 'getDiffVersionTrees', url: '/Home/DiffVersionTree', type: 'Get', },

        //#endregion

        //#region Đăng ký qua mạng

        //tiep nhan
        { name: 'acceptOnline', url: '/DocumentOnline/AcceptOnline', type: 'Post' },

        //tu choi
        { name: 'rejectOnline', url: '/DocumentOnline/RejectOnline', type: 'Post' },

        //tu choi
        { name: 'onlineSupplementary', url: '/DocumentOnline/OnlineSupplementary', type: 'Post' },

        { name: 'additionalRequirements', url: '/DocumentOnline/AdditionalRequirements', type: 'Post' },

        //Tong so van ban dang ky qua mang
        { name: 'getTotalOnlineRegistration', url: '/DocumentOnline/GetTotalOnlineRegistration', type: 'Get' },

         //Danh sach van ban dang ky qua mang
        { name: 'getDocumentOnlineRegistration', url: '/DocumentOnline/GetDocumentOnlineRegistration', type: 'Get' },

        //Tong so van ban dang ky qua mang bị hủy bỏ
        { name: 'getTotalOnlineCancel', url: '/DocumentOnline/GetTotalOnlineCancel', type: 'Get' },

         //Danh sach van ban dang ky qua mang bị hủy bỏ
        { name: 'getDocumentOnlineCancel', url: '/DocumentOnline/GetDocumentOnlineCancel', type: 'Get' },

        //chi tiet hos dang ky qua mang
        { name: 'getDocumentDetailOnlineRegistration', url: '/DocumentOnline/GetDocumentDetailOnlineRegistration', type: 'Get' },

        //Kiểm tra hồ sơ công dân đang đăng ký trực tuyến
        { name: 'checkDocumentOnline', url: '/DocumentOnline/CheckDocumentOnline/', type: 'Get' },

        //Kiểm tra danh sách hồ sơ đang có của công dân trên hệ thống
        { name: 'checkDocument', url: '/DocumentOnline/CheckDocument/', type: 'Get' },

        //Mở lại công văn kết thúc nhầm
        { name: 'reOpenDocument', url: '/Document/ReOpenDocument', type: 'Get' },

        //Xuat danh sach ra file
        { name: 'exportToFile', url: '/Home/ExportToFile', type: 'Post' },

          //Lấy mẫu phôi của mail, sms
        { name: 'editTemplate', url: '/Document/EditTemplate', type: 'Get' },

        //#endregion

        //#region Mobile

        // Lấy tổng số văn bản thông báo
        { name: 'notificationsCount', url: '/Mobile/GetNotificationsCount', type: 'Get' },

        // Thiết lập các config của người dùng cho Mobile
        { name: 'setMobileUserConfig', url: '/Account/SetMobileUserConfig/', type: 'Post', hasToken: true },

        //#endregion

        //Lấy Xóa tệp đính kèm
        { name: 'removeAttachment', url: '/Attachment/RemoveAttachment', type: 'Post' },

        // lấy tổng sô câu hỏi chung
        { name: 'getTotalGeneralQuestion', url: '/Question/GetTotal?isGetGeneral=true', type: 'Get' },

        // lấy tổng sô câu hỏi theo hồ sơ
        { name: 'getTotalDocumentQuestion', url: '/Question/GetTotal?isGetGeneral=false', type: 'Get' },

         //#region Giấy phép doanh nghiệp
        { name: 'getBusinessLicense', url: '/BusinessLicense/BusinessLicenses', type: 'Get' },

         //#region Giấy phép doanh nghiệp
        { name: 'removeBusinessLicense', url: '/BusinessLicense/RemoveLicenses', type: 'Post', traditional: true },

           //#region Giấy phép doanh nghiệp
        { name: 'createCitizen', url: '/BusinessLicense/CreateCitizen', type: 'Post', traditional: true },

            //#region Giấy phép doanh nghiệp
        { name: 'createLicense', url: '/BusinessLicense/CreateLicense', type: 'Post', traditional: true },
        //#endregion

        //#region Mobile

        // Lấy tổng số văn bản thông báo
      { name: 'createVote', url: '/Referendum/Vote', type: 'Post', traditional: true },
      { name: 'updateVote', url: '/Referendum/VoteUpdate', type: 'Post', traditional: true },
      { name: 'deleteVote', url: '/Referendum/DeleteVote', type: 'Post', traditional: true },
      // Lấy tổng số các vote cảu người dùng hiện tại
      { name: 'getVotes', url: '/Referendum/GetVotes', type: 'Get' },
      // Lấy tổng số các vote cảu người dùng hiện tại
      { name: 'getVoteDetail', url: '/Referendum/GetVoteDetail', type: 'Get' },
       // DeleteVote
      { name: 'createCommentDiff', url: '/Referendum/CreateCommentDiff', type: 'Post', traditional: true },
        // 
      { name: 'checkVote', url: '/Referendum/CheckVote', type: 'Post', traditional: true },

      // Gui
      { name: 'checkVoteResult', url: '/Referendum/CheckVoteResult', type: 'Post', traditional: true },
        // 
      { name: 'uncheckVote', url: '/Referendum/UncheckVote', type: 'Post', traditional: true },
      { name: 'getUserInfos', url: '/Referendum/GetUserInfos', type: 'get' },
      { name: 'getVoteDetailReload', url: '/Referendum/GetVoteDetailReload', type: 'get' },


        //#endregion
        //#endregion

    ];

    //#endregion

    egov.request = new RequestManager();
})
(this.egov = this.egov || {}, window.Offline);
// bt.util.date.js

/*
    Author: TienBV
    DateCreated: 25/06/2015
    Version: 1.0

    Description:

    - Các đơn vị thời gian:
        year: "year",
        quarter: "quarter",
        month: "month",
        week: "week",
        date: "date",
        day: "day",
        hour: "hours",
        minute: "minutes",
        second: "seconds",
        miniSecond: "miniSecond"

    - Các định dạng format
        yy: "yy",                   // Năm: 96, 97, ... 14, 15
        yyyy: "yyyy",               // Năm đầy đủ: 1996, 1997, ... 2014, 2015
        q: "q",                     // Quý: 1, 2, 3, 4
        M: "M",                     // Tháng: 1, 2, ... 12
        MM: "MM",                   // Tháng đầy đủ: 01, 02, ... 12
        MMM: "MMM",                 // Tháng: Th1, Th2, ... Th12
        MMMM: "MMMM",               // Tháng: Tháng một, Tháng hai, ... Tháng mười hai
        w: "w",                     // Tuần: 1, 2, ... 52
        ww: "ww",                   // Tuần: 01, 02, ... 52
        d: "d",                     // Ngày trong tháng: 1, 2, ... 31
        dd: "dd",                   // Ngày trong tháng: 01, 02, ... 31
        ddd: "ddd",                 // Ngày trong tuần: Thứ 2, thứ 3, ... 
        h: "h",                     // Giờ (12): 1, 2, ... 12    
        hh: "hh",                   // Giờ (12): 01, 02, ... 12
        H: "H",                     // Giờ (24): 1, 2, ... 23
        HH: "HH",                   // Giờ (24): 01, 02, ... 23
        a: "a",                     // Am/pm: am/pm
        A: "A",                     // Am/pm: AM/PM
        m: "m",                     // Phút: 1, 2, ... 59
        mm: "mm",                   // Phút: 01, 02, ... 59
        s: "s",                     // Giây: 1, 2, ... 59
        ss: "ss",                   // Giây: 01, 02, ... 59
        sss: "sss",                 // Tích tắc: 1, 2, ... 999
        z: "z",                     // Timezone: EST CST ... MST PST

    - Các method cho kiểu dữ liệu Date

        + parse(value, format): Chuyển đổi string sang datetime theo format chỉ định. Mặc định hệ thống parse theo ISODate format.
            Date.parse("2015-07-15T16:20:04.021Z")  = Wed Jul 15 2015 09:20:04 GMT-0700 (Pacific Daylight Time)
            Date.parse("2015-07-15", "yyyy-MM-dd)  = Wed Jul 15 2015 09:20:04 GMT-0700 (Pacific Daylight Time)
            
            Notes: sẽ có trong phiên bản 1.1

        + daysInMonth(month, year): Trả về số ngày trong tháng.
            Date.daysInMonth(7, 2015)  = 31

        + compare(date1, date2): Trả về kết quả so sánh giữa 2 đối tượng ngày tháng.
            Notes: sẽ có trong phiên bản 1.1

        + max([date1, date2, ...]): Trả về đối tượng ngày tháng lớn nhất.
            Notes: sẽ có trong phiên bản 1.1

        + min([date1, date2, ...]): Trả về đối tượng ngày tháng nhỏ nhất.
            Notes: sẽ có trong phiên bản 1.1

    - Các method cho đối tượng dữ liệu String

        + format(partern): Convert ngày tháng theo chuỗi theo định dạng yêu cầu và trả về kết quả; partern mặc định hh:mm dd/MM/yyyy (Vn_vi)
            new Date().format("dd/MM/yy") = "15/07/15"
          
        + subtract(value, unit): Trừ đi một khoảng thời gian theo đơn vị thời gian (ở trên) của đối tượng ban đầu và trả về đối tượng mới.
            new Date().subtract(1, "month").format("dd/MM/yy") = "15/06/15"

        + add(value, unit): Trừ đi một khoảng thời gian theo đơn vị thời gian (ở trên) của đối tượng ban đầu và trả về đối tượng mới.
            new Date().add(1, "month").format("dd/MM/yy") = "15/06/15"

        + month(value): Lấy hoặc thiết lập tháng của đối tượng thời gian hiện tại: 1, 2, ... 12
            new Date().month() = 07
            new Date().month(12).format("dd/MM/yy") = "15/12/15";

        + year(value): Tương tự hàm month ở trên.
            new Date().year() = 2015
            new Date().year(2017).format("dd/MM/yy") = "15/07/17";

        + date(value): Lấy hoặc thiết lập ngày trong tháng của đối tượng thời gian hiện tại: 1, 2, ... 31
            new Date().date() = 15
            new Date().date(24).format("dd/MM/yy") = "24/07/17";

        + day(): Trả về thứ tự ngày trong tuần của đối tượng thời gian hiện tại tính từ chủ nhật: 0, 6
            new Date().day() = 03

        + weekOfYear():  Trả về thứ tự tuần trong năm của đối tượng thời gian hiện tại: 1, 2, ... 52
            new Date().weekOfYear() = 29

        + dayOfYear(): Trả về thứ tự ngày trong năm của đối tượng thời gian hiện tại: 1, 2, ... 365 (366)
            new Date().dayOfYear() = 196

        + hours(value): Lấy hoặc thiết lập giờ của đối tượng thời gian hiện tại: 1, 2, ... 23
            var d = new Date(); d.setHours(15); d.format("hh:mm")    = "15:33"

        + minutes(value): Tương tự hours

        + seconds(value): Tương tự hours

        + miniSeconds(value): Tương tự hours

        + quarter(): Trả về quý của đối tượng thời gian hiện tại
            new Date().quarter() = 3

        + endOf(): Thiết lập đối tượng thời gian hiện tại thành thời điểm cuối cùng theo đơn vị thời gian truyền vào và trả về ngày mới tương ứng.
            new Date().endOf("year").format("dd/MM/yyyy")   = "31/12/2015"          ( Ngày cuối cùng trong năm )
            new Date().endOf("month").format("dd/MM/yyyy")  = "31/07/2015"          ( Ngày cuối cùng trong tháng )
            new Date().endOf("day").format()                = "23:59 15/07/2015"    ( Thời điểm cuối cùng trong ngày)
            ...

        + startOf(): Tương tự endOf
            new Date().startOf("year").format("dd/MM/yyyy")   = "01/01/2015"          ( Ngày đầu tiên trong năm )
            new Date().startOf("month").format("dd/MM/yyyy")  = "01/07/2015"          ( Ngày đầu tiên trong tháng )
            new Date().startOf("day").format()                = "00:00 15/07/2015"    ( Thời điểm đầu tiên trong ngày)
            ...

        + isLeapYear(): Trả về giá trị xác định năm hiện tại có phải là năm nhuận không; true = năm nhuận; false = không.
            new Date().subtract(1, "year").isLeapYear() = true;
*/


bt_util_date_resource = {
    vi: {
        months: ["tháng 1", "tháng 2", "tháng 3", "tháng 4", "tháng 5", "tháng 6", "tháng 7", "tháng 8", "tháng 9", "tháng 10", "tháng 11", "tháng 12"],
        shortMonths: ["Th1", "Th2", "Th3", "Th4", "Th5", "Th6", "Th7", "Th8", "Th9", "Th10", "Th11", "Th12"],
        weeks: ["chủ nhật", "thứ hai", "thứ ba", "thứ tư", "thứ năm", "thứ sáu", "thứ bảy"],
        shortWeeks: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
        times: {
            future: '%s tới',
            ago: '%s trước',
            s: 'vài giây',
            m: 'một phút',
            mm: '%d phút',
            h: 'một giờ',
            hh: '%d giờ',
            d: 'một ngày',
            dd: '%d ngày',
            M: 'một tháng',
            MM: '%d tháng',
            y: 'một năm',
            yy: '%d năm'
        },
        calendar: {
            sameDay: 'Hôm nay',
            nextDay: 'Ngày mai',
            lastDay: 'Hôm qua',
            sameWeek: 'Tuần này',
            nextWeek: 'Tuần tới',
            lastWeek: 'Tuần trước',
            sameMonth: 'Tháng này',
            lastMonth: 'Tháng trước'
        }
    }
};

(function (window) {
    "use strict";

    var _date, _prototype, _unitType, _formatObject, _formatTokens, _defaultFormat, _resource, _defaultFarseFormat;
    _date = Date;
    _prototype = _date.prototype;
    _unitType = {
        year: "year",
        quarter: "quarter",
        month: "month",
        week: "week",
        date: "date",
        day: "day",
        hour: "hours",
        minute: "minutes",
        second: "seconds",
        miniSecond: "miniSecond"
    };
    _formatTokens = /(\[[^\[]*\])|(\\)?(MM?M?M?|dd?d?|ww?|q|yy?yy?|a|A|hh?|HH?|mm?|ss?s?|z|.)/g;
    _defaultFormat = "hh:mm dd/MM/yyyy";
    _defaultFarseFormat = "yyyy-MM-ddTHH:mm:ss";
    _formatObject = {
        yy: "yy",                   // Năm: 96, 97, ... 14, 15
        yyyy: "yyyy",               // Năm: 1996, 1997, ... 2014, 2015
        q: "q",                     // Quý: 1, 2, 3, 4
        M: "M",                     // Tháng: 1, 2, ... 12
        MM: "MM",                   // Tháng: 01, 02, ... 12
        MMM: "MMM",                 // Tháng: Th1, Th2, ... Th12
        MMMM: "MMMM",               // Tháng: Tháng một, Tháng hai, ... Tháng mười hai
        w: "w",                     // Tuần: 1, 2, ... 52
        ww: "ww",                   // Tuần: 01, 02, ... 52
        d: "d",                     // Ngày trong tháng: 1, 2, ... 31
        dd: "dd",                   // Ngày trong tháng: 01, 02, ... 31
        ddd: "ddd",                 // Ngày trong tuần: Thứ 2, thứ 3, ... 
        h: "h",                     // Giờ (12): 1, 2, ... 12    
        hh: "hh",                   // Giờ (12): 01, 02, ... 12
        H: "H",                     // Giờ (24): 1, 2, ... 23
        HH: "HH",                   // Giờ (24): 01, 02, ... 23
        a: "a",                     // Am/pm: am/pm
        A: "A",                     // Am/pm: AM/PM
        m: "m",                     // Phút: 1, 2, ... 59
        mm: "mm",                   // Phút: 01, 02, ... 59
        s: "s",                     // Giây: 1, 2, ... 59
        ss: "ss",                   // Giây: 01, 02, ... 59
        sss: "sss",                 // Tích tắc: 1, 2, ... 999
        z: "z",                     // Timezone: EST CST ... MST PST 
    };
    _resource = bt_util_date_resource["vi"];

    //#region Prototype Methods

    _prototype.format = function Date$format(partern) {
        /// <summary>
        /// Convert ngày tháng thành chuỗi theo định dạng yêu cầu
        /// </summary>
        /// <param name="partern">Định dạng chuỗi cần xuất ra.</param>
        var formats, leng, match, i, formatResult;
        var result = "";

        partern = partern || _defaultFormat;
        formats = partern.match(_formatTokens);
        leng = formats.length;

        for (i = 0; i < leng; i++) {
            match = formats[i].toString();
            match = match.replace("[", "").replace("]", "");
            result += isFormatting(match) ? this._getFormattingValue(match) : match;
        }

        return result;
    }

    _prototype.subtract = function Date$subtract(value, unit) {
        /// <summary>
        /// Trừ đi một khoảng thời gian và trả về khoảng thời gian mới từ thời gian ban đầu.
        /// </summary>
        /// <param name="value">Giá trị trừ</param>
        /// <param name="unit">Trường thời gian cần trừ</param>
        switch (unit) {
            case _unitType.year:
                this.year(this.year() - value);
                break;
            case _unitType.month:
                this.month(this.month() - value);
                break;
            case _unitType.date:
                this.date(this.date() - value);
                return this;
            case _unitType.hour:
                this.hours(this.hours() - value);
                break;
            case _unitType.minute:
                this.minutes(this.minutes() - value);
                break;
            case _unitType.second:
                this.seconds(this.seconds() - value);
                break;
            case _unitType.miniSecond:
                this.miniSeconds(this.miniSeconds() - value);
                break;
            default:
                break;
        }

        return this;
    }

    _prototype.add = function Date$add(value, unit) {
        /// <summary>
        /// Thêm một khoảng thời gian vào thời gian ban đầu và trả về thời gian mới
        /// </summary>
        /// <param name="value">Khoảng thời gian cần thêm</param>
        /// <param name="unit">Trường thời gian cần thêm</param>
        switch (unit) {
            case _unitType.year:
                this.year(this.year() + value);
                break;
            case _unitType.month:
                this.month(this.month() + value);
                break;
            case _unitType.date:
                this.date(this.date() + value);
                return this;
            case _unitType.hour:
                this.hours(this.hours() + value);
                break;
            case _unitType.minute:
                this.minutes(this.minutes() + value);
                break;
            case _unitType.second:
                this.seconds(this.seconds() + value);
                break;
            case _unitType.miniSecond:
                this.miniSeconds(this.miniSeconds() + value);
                break;
            default:
                break;
        }

        return this;
    }

    _prototype.month = function Date$month(value) {
        /// <summary>
        /// Lấy hoặc thiết lập tháng của ngày này: 1,2 ... 12
        /// </summary>

        if (value == 2 && this.date() > 28) {
            this.date(28);
        }

        if (hasValue(value)) {
            this.setMonth(value - 1);
            return this;
        }

        return this.getMonth() + 1;
    }

    _prototype.year = function Date$year(value) {
        /// <summary>
        /// Lấy hoặc thiết lập năm của ngày này
        /// </summary>
        if (hasValue(value)) {
            this.setFullYear(value);
            return this;
        }

        return this.getFullYear();
    }

    _prototype.date = function Date$date(value) {
        /// <summary>
        /// Lấy hoặc thiết lập ngày trong tháng của ngày này
        /// </summary>
        /// <param name="value" type="int">Ngày cần set, gán null để lấy giá trị.</param>

        if (hasValue(value)) {
            this.setDate(value);
            return this;
        }

        return this.getDate();
    }

    _prototype.day = function Date$day() {
        /// <summary>
        /// Trả về ngày trong tuần của ngày này
        /// </summary>

        return this.getDay();
    }

    _prototype.weekOfYear = function Date$week() {
        /// <summary>
        /// Trả về tuần trong năm của ngày này
        /// </summary>
        var firstDay = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((((this - firstDay) / 86400000) + firstDay.day() - 1) / 7);
    }

    _prototype.dayOfYear = function Date$dayOfYear() {
        /// <summary>
        /// Trả về ngày trong năm của ngày này, ví dụ ngày thứ 234 của 365
        /// </summary>
        var firstDay = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((this - firstDay) / 86400000);
    }

    _prototype.hours = function Date$hours(value) {
        /// <summary>
        /// Lấy hoặc thiết lập giờ của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setHours(value);
            return this;
        }

        return this.getHours();
    }

    _prototype.minutes = function Date$minutes(value) {
        /// <summary>
        /// Lấy hoặc thiết lập phút của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setMinutes(value);
            return this;
        }

        return this.getMinutes();
    }

    _prototype.seconds = function Date$seconds(value) {
        /// <summary>
        /// Lấy hoặc thiết lập giây của ngày này
        /// </summary>
        if (hasValue(value)) {
            this.setSeconds(value);
            return this;
        }

        return this.getSeconds();
    }

    _prototype.miniSeconds = function Date$miniSeconds(value) {
        /// <summary>
        /// Lấy hoặc thiết lập mini giây của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setMilliseconds(value);
            return this;
        }

        return this.getMilliseconds();
    }

    _prototype.quarter = function Date$quarter() {
        /// <summary>
        /// Trả về quý của ngày này
        /// </summary>
        return Math.floor(((this.month() - 1) / 3) + 1);
    }

    _prototype.endOf = function Date$endOf(unit) {
        /// <summary>
        /// Thiết lập ngày hiện tại thành thời điểm cuối cùng của đơn vị truyền vào và trả về ngày mới tương ứng.
        /// </summary>
        /// <param name="unit">
        /// Đơn vị:
        /// - "year": thiết lập ngày hiện tại về ngày cuối cùng trong năm với ngày hiện tại.
        /// - "quarter": thiết lập ngày hiện tại về ngày cuối cùng của quý với ngày hiện tại
        /// - "month": thiết lập ngày hiện tại về ngày cuối cùng trong tháng với ngày hiện tại.
        /// - "week": thiết lập ngày hiện tại về ngày cuối cùng trong tuần với ngày hiện tại.
        /// - "day": thiết lập ngày hiện tại về thời điểm 23:59:59 của ngày hiện tại.
        /// - "hour": thiết lập ngày hiện tại về thời điểm 59:59 của giờ hiện tại.
        /// - "minute": thiết lập ngày hiện tại về thời điểm 59s của phút hiện tại.
        /// - "second": thiết lập ngày hiện tại về thời điểm 999ms của giây hiện tại.
        /// </param>
        if (_unitType[unit] === undefined) {
            throw "Đơn vị truyền vào không hợp lệ.";
        }

        var year, quarter, month, days, date;
        year = this.year();
        quarter = this.quarter();
        month = this.month();
        date = this.date();

        switch (unit) {
            case _unitType.year:
                this.month(12).date(31).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.quarter:
                month = quarter * 3;
                days = _date.daysInMonth(month, year);
                this.month(month).date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.month:
                days = _date.daysInMonth(month, year);
                this.date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.week:
                days = date - this.day() + 6;
                this.date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.day:
                this.hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.hour:
                this.minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.minute:
                this.seconds(59).miniSeconds(999);
                break;
            case _unitType.second:
                this.miniSeconds(999);
                break;
        }

        return this;
    }

    _prototype.startOf = function Date$startOf(unit) {
        /// <summary>
        /// Thiết lập ngày hiện tại thành thời điểm đầu tiên của đơn vị truyền vào và trả về ngày mới tương ứng.
        /// </summary>
        /// <param name="unit">
        /// Đơn vị:
        /// - "year": thiết lập ngày hiện tại về ngày đầu tiên trong năm với ngày hiện tại.
        /// - "quarter": thiết lập ngày hiện tại về ngày đầu tiên của quý với ngày hiện tại
        /// - "month": thiết lập ngày hiện tại về ngày đầu tiên trong tháng với ngày hiện tại.
        /// - "week": thiết lập ngày hiện tại về ngày đầu tiên trong tuần với ngày hiện tại.
        /// - "day": thiết lập ngày hiện tại về thời điểm 12:00:00 am của ngày hiện tại.
        /// - "hour": thiết lập ngày hiện tại về thời điểm 00:00 của giờ hiện tại.
        /// - "minute": thiết lập ngày hiện tại về thời điểm 00s của phút hiện tại.
        /// - "second": thiết lập ngày hiện tại về thời điểm 000ms của giây hiện tại.
        /// </param>
        if (_unitType[unit] === undefined) {
            throw "Đơn vị truyền vào không hợp lệ.";
        }

        var quarter, month, days, date;
        quarter = this.quarter();
        date = this.date();

        switch (unit) {
            case _unitType.year:
                this.month(1).date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.quarter:
                month = quarter * 3 - 2;
                this.month(month).date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.month:
                this.date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.week:
                days = date - this.day();
                this.date(days).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.day:
                this.hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.hour:
                this.minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.minute:
                this.seconds(0).miniSeconds(0);
                break;
            case _unitType.second:
                this.miniSeconds(0);
                break;
        }

        return this;
    }

    _prototype.isLeapYear = function Date$isLeapYear() {
        /// <summary>
        /// Trả về giá trị xác định năm hiện tại có phải là năm nhuận không.
        /// </summary>
        return (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;
    }

    _prototype._getFormattingValue = function Date$_getFormattingValue(partern) {
        var result;
        switch (partern) {
            case _formatObject.yy:
                result = this.year().toString().substr(2, 2);
                break;
            case _formatObject.yyyy:
                result = this.year();
                break;
            case _formatObject.q:
                result = this.quarter();
                break;
            case _formatObject.M:
                result = this.month();
                break;
            case _formatObject.MM:
                result = toDigit(this.month());
                break;
            case _formatObject.MMM:
                result = _resource.shortMonths[this.month() - 1];
                break;
            case _formatObject.MMMM:
                result = _resource.months[this.month() - 1];
                break;
            case _formatObject.d:
                result = this.date();
                break;
            case _formatObject.dd:
                result = toDigit(this.date());
                break;
            case _formatObject.ddd:
                result = _resource.weeks[this.day()];
                break;
            case _formatObject.h:
                result = to12Hour(this.hours());
                break;
            case _formatObject.hh:
                result = toDigit(to12Hour(this.hours()));
                break;
            case _formatObject.H:
                result = this.hours();
                break;
            case _formatObject.HH:
                result = toDigit(this.hours());
                break;
            case _formatObject.m:
                result = this.minutes();
                break;
            case _formatObject.mm:
                result = toDigit(this.minutes());
                break;
            case _formatObject.s:
                result = this.seconds();
                break;
            case _formatObject.ss:
                result = toDigit(this.seconds());
                break;
            case _formatObject.S:
                result = this.miniSeconds();
                break;
            case _formatObject.Z:
                result = this.year();
                break;
            default:
                result = "";
                break;
        }
        return result;
    }

    _prototype.relativeDate = function () {
        /// <summary>
        /// Time ago
        /// </summary>

        var seconds = Math.floor((new Date() - this) / 1000);
        var interval = Math.floor(seconds / 31536000);

        if (interval > 1) {
            return interval + " năm trước";
        }
        interval = Math.floor(seconds / 2592000);
        if (interval > 1) {
            return interval + " tháng trước";
        }
        interval = Math.floor(seconds / 604800);
        if (interval > 1) {
            return interval + " tuần trước";
        }
        interval = Math.floor(seconds / 86400);
        if (interval > 1) {
            return interval + " ngày trước";
        }
        interval = Math.floor(seconds / 3600);
        if (interval > 1) {
            return interval + " giờ trước";
        }
        interval = Math.floor(seconds / 60);
        if (interval > 1) {
            return interval + " phút trước";
        }
        return "Vừa mới";
    }

    _prototype.toServerString = function Date$toServerString() {
        //2017-10-25T11:04:23
        return this.format(_defaultFarseFormat);
    }

    _prototype.getVNDay = function () {
        var day = this.getDay();
        return bt_util_date_resource.vi.weeks[day];
    }

    _prototype.timeInWord = function () {
        /// <summary>
        ///
        /// </summary>

        var date = this; var distanceMillis;
        var now = Date.now();

        var today = now.format("yyyyMMwwdd");
        var lastDay = now.subtract(1, _unitType.date).format("yyyyMMwwdd");
        var week = now.format("yyyyMMww");
        var lastWeek = now.subtract(1, _unitType.week).format("yyyyMMww");
        var month = now.format("yyyyMM");
        var lastMonth = now.subtract(1, _unitType.month).format("yyyyMM");

        var year = now.format("yyyy");

        var dateFormat = date.format("yyyyMMwwddhh");
        if (dateFormat.indexOf(today) === 0) {
            return _resource.calendar.sameDay;
        }
        if (dateFormat.indexOf(lastDay) === 0) {
            return _resource.calendar.lastDay;
        }

        if (dateFormat.indexOf(week) === 0) {
            return _resource.calendar.sameWeek;
        }
        if (dateFormat.indexOf(lastWeek) === 0) {
            return _resource.calendar.lastWeek;
        }

        if (dateFormat.indexOf(month) === 0) {
            return _resource.calendar.sameMonth;
        }
        if (dateFormat.indexOf(lastMonth) === 0) {
            return _resource.calendar.lastMonth;
        }

        if (dateFormat.indexOf(year) === 0)
            return date.format("[Tháng] MM");

        return date.format("[Tháng] MM - yyyy");
    }

    _prototype.isToday = function () {
        var today = Date.now().format("yyyyMMdd");
        var date = this.format("yyyyMMdd");
        return date === today;
    }

    //#endregion

    //#region Statis Methods

    _date.parse = function Date$parse(value, format) {
        /// <summary>
        /// Chuyển đổi string thành định dạng ngày tháng theo format chỉ định
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        format = format || _defaultFarseFormat;

        if (!value)
            return null;

        if (Globalize) {
            return Globalize.parseDate(value, format)
        }

        return null;
    }

    _date.daysInMonth = function Date$daysInMonth(month, year) {
        /// <summary>
        /// Trả về số ngày trong tháng
        /// </summary>
        /// <param name="month">Tháng</param>
        /// <param name="year">Năm</param>
        return new Date(year, month, 0).date();
    }

    _date.compare = function Date$compare(date1, date2) {
        /// <summary>
        /// So sánh 2 ngày tháng và trả về kết quả so sánh nhỏ hơn, bằng, lớn hơn.
        /// </summary>
        /// <param name="date1">Ngày 1</param>
        /// <param name="date2">Ngày 2</param>
        /// <returns type="int">1-ngày 1 lớn hơn ngày 2; 0-ngày 1 bằng ngày 2; -1-ngày 1 nhỏ hơn ngày 2</returns>
    }

    _date.max = function () {
        /// <summary>
        /// Trả về ngày lớn nhất của kiểu dữ liệu Date
        /// </summary>
    }

    _date.min = function () {
        /// <summary>
        /// Trả về ngày nhỏ nhất của kiểu dữ liệu Date
        /// </summary>
    }

    _date.now = function () {
        return new Date;
    }

    //#endregion

    //#region Private Methods

    var hasValue = function (value) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị phải là null hay không.
        /// </summary>
        /// <param name="value">Giá trị</param>
        return value !== undefined && value !== null;
    };

    var _isObject = function (obj) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị có phải là một object không
        /// </summary>
        /// <param name="obj"></param>
        return typeof obj === "object";
    };

    var _isNumber = function (val) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị hiện tải có phải là 1 number hay không.
        /// </summary>
        /// <param name="val"></param>

    };

    var toDigit = function (number) {
        return number > 9 ? number : ("0" + number);
    };

    var to12Hour = function (value) {
        return value > 12 ? (value - 12) : value;
    };

    var isFormatting = function (value) {
        return _formatObject[value] !== undefined;
    };

    //#endregion
})
    (this);
// bt.util.string.js

/*
    Author: TienBV
    DateCreated: 25/06/2015
    Version: 1.0

    Description:

    - Các method cho kiểu dữ liệu String

        + format:               String.format("Tôi là {0}", "TienBV")   => return: "Tôi là TienBV"

        + isNullOrEmpty:        String.isNullOrEmpty("")                => return: true

        + isNullOrWhiteSpace:   String.isNullOrWhiteSpace("   ")        => return: true

        + isString:             String.isString("TienBV")               => return: true

    - Các method cho đối tượng dữ liệu String

        + trim, trimStart, trimEnd: cắt các ký tự trống ở 2 đầu chuỗi, hoặc đầu chuỗi, hoặc cuối chuỗi
            "   Bkav vô đối   ".trim()        = "Bkav vô đối"; 
            "   Bkav vô đối   ".trimStart()   = "Bkav vô đối   "; 
            "   Bkav vô đối   ".trimEnd()     = "   Bkav vô đối";

        + startWith(prefix, ignoreCase): Trả về giá trị xác định chuỗi cần kiểm tra có phải nằm ở đầu chuỗi hiện tại không.
            "Bkav vô đối".startWith("Bkav")         = true;  
            "Bkav vô đối".startWith("bKaV", true)   = true;

        + endWith(prefix, ignoreCase)      => tương tự startWith

        + equals(value, ignoreCase): Trả về giá trị xác định chuỗi cung cấp có bằng với chuỗi hiện tại không.
            "Bkav vô đối".equals("Bkav vô đối")         = true; 
            "Bkav vô đối".equals("Bkav Vô Đối", true)   = true

        + remove(startIndex, count): Trả về chuỗi sau khi đã xóa những ký tự ở các vị trí truyền vào.
            "Bkav vô đối".remove(4)             = "Bkav";
            "Bkav vô đối".remove(0, 5)          = "vô đối";

        + replaceAll(searchValue, replaceValue): Trả về chuỗi sau khi thay thế các chuỗi cũ được chỉ định bằng chuỗi mới.
            "Bkav vô đối".replaceAll("v", "i")  = "Bkai iô đối";

        + contains(value): Trả về giá trị xác định chuỗi cần kiểm tra có thuộc chuỗi hiện tại không.
            "Bkav vô đối".contains("vô")        = true;

        + toCharArray():  Trả về một mảng tất cả các ký tự trong chuỗi
            "Bkav".toCharArray()        = ["B", "k", "a", "v"]

        + reverse(): đảo chuỗi
            "Bkav".reverse()            = "vakB";

        + removeVietnamChars(): loại bỏ dấu tiếng việt
            "Bkav vô đối".removeVietnamChars()      = "Bkav vo doi";

*/

(function () { var t = String, n = t.prototype, i = ["aáàảãạăắằẳẵặâấầẩẫậ", "AÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ", "dđ", "DĐ", "eéèẻẽẹêếềểễệ", "EÉÈẺẼẸÊẾỀỂỄỆ", "iíìỉĩị", "IÍÌỈĨỊ", "oóòỏõọơớờởỡợôốồổỗộ", "OÓÒỎÕỌƠỚỜỞỠỢÔỐỒỔỖỘ", "uưứừửữựúùủũụ", "UƯỨỪỬỮỰÚÙỦŨỤ", "yýỳỷỹỵ", "YÝỲỶỸỴ"]; n.trim = function () { return this.replace(/^\s+|\s+$/g, "") }; n.trimStart = function () { return this.replace(/^\s+/, "") }; n.trimEnd = function () { return this.replace(/\s+$/, "") }; n.startWith = function (n, t) { if (!String.isString(n)) throw "Giá trị truyền vào không hợp lệ"; return t = t || !1, n.equals(this.substr(0, n.length), t) }; n.endWith = function (n, t) { if (!String.isString(n)) throw "Giá trị truyền vào không hợp lệ"; return t = t || !1, n.equals(this.substr(this.length - n.length), t) }; n.equals = function (n, t) { if (!String.isString(n)) throw "Giá trị truyền vào không hợp lệ"; return t = t || !1, t ? this.toLowerCase() === n.toLowerCase() : this.toString() === n }; n.remove = function (n, t) { var i; if (typeof n != "number") throw "Start Index phải là một chữ số"; return i = this.slice(0, n), t != undefined && typeof t == "number" && (i += this.slice(n + t)), i }; n.replaceAll = function (n, t) { if (!String.isString(n) || !String.isString(t)) throw "Các tham số truyền vào phải là string"; var i = new RegExp(n.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&"), "g"); return this.replace(i, t) }; n.contains = function (n) { if (!String.isString(n)) throw "Tham số truyền vào phải là chuỗi"; return this.indexOf(n) > -1 }; n.toCharArray = function () { return this.split("") }; n.reverse = function () { return this.split("").reverse().join("") }; n.forEach = function (n) { if (typeof n == "function") for (var i = this.length, t = 0; t < i; t++) n(this.charAt(t), t) }; n.removeVietnamChars = function () { var t = [], r = this, n; return r.forEach(function (r, u) { for (t[u] = r, n = 0; n < i.length; n++) if (i[n].contains(r)) { t[u] = i[n][0]; break } }), t.join("") }; t.format = function () { return String._toFormattedString(!1, arguments) }; t.isNullOrEmpty = function (n) { return n === null || n === undefined || n === "" }; t.isNUllOrWhiteSpace = function (n) { return n = n.trim(), String.isNullOrEmpty(n) }; t.isString = function (n) { return typeof n == "string" }; t._toFormattedString = function (n, t) { for (var o, u, c, r, e = "", f = t[0], i = 0; ;) { if (o = f.indexOf("{", i), u = f.indexOf("}", i), o < 0 && u < 0) { e += f.slice(i); break } if (u > 0 && (u < o || o < 0)) { if (f.charAt(u + 1) !== "}") throw new Error("format stringFormatBraceMismatch"); e += f.slice(i, u + 1); i = u + 2; continue } if (e += f.slice(i, o), i = o + 1, f.charAt(i) === "{") { e += "{"; i++; continue } if (u < 0) throw new Error("format stringFormatBraceMismatch"); var s = f.substring(i, u), h = s.indexOf(":"), l = parseInt(h < 0 ? s : s.substring(0, h), 10) + 1; if (isNaN(l)) throw new Error("format stringFormatInvalid"); c = h < 0 ? "" : s.substring(h + 1); r = t[l]; (typeof r == "undefined" || r === null) && (r = ""); e += r.toFormattedString ? r.toFormattedString(c) : n && r.localeFormat ? r.localeFormat(c) : r.format ? r.format(c) : r.toString(); i = u + 1 } return e } })(this);


(function (egov, $, _) {
    var regexUtcDate = /\d{4}-[0-1]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-6]\d$/;

    egov.commonFn = {
        event: {
            changeTitleForMobile: function (app, title) {
                $("#header-title span[data-app='" + app + "']").text(title);
            },

            showNavbar: function () {
                $("#main-page").removeClass("hidelayoutnav");
            },

            hideNavbar: function () {
                $("#main-page").addClass("hidelayoutnav");
            },

            //Logout
            logout: function () {
                var cookies = document.cookie.split(";");
                for (var i = 0; i < cookies.length; i++) {
                    var eqPos = cookies[i].indexOf("=");
                    var name = eqPos > -1 ? cookies[i].substr(0, eqPos) : cookies[i];
                    name = name.trim();
                    $.cookie(name, "", { domain: document.domain, path: "/", expires: -1, secure: true });
                    $.cookie(name, "", { expires: -1, secure: true });
                }
                if (helper.isTool) {
                    if (typeof window.external.CB_Logout === 'function') {
                        window.external.CB_Logout();
                    }
                    $.cookie("isLogin", "", { expires: -1, secure: true });
                }
                window.document.location.reload();
            },
        },
        util: {
            getCustomTime: function (date, isGetOverdue) {
                /// <summary>
                /// Lấy ngày quá hạn xử lý văn bản cho Mobile
                /// </summary>
                /// <param name="date">Ngày truyền vào</param>
                /// <param name="isGetOverdue">true nếu lấy cả hạn tổng, dùng cho Mobile</param>
                if (date == null || date == '') {
                    return "";
                }

                if (regexUtcDate.test(date) || (date instanceof Date)) {
                    if (regexUtcDate.test(date)) {
                        var array = date.split(/[^0-9]/);
                        date = new Date(array[0], array[1] - 1, array[2], array[3], array[4], array[5])
                    } else {
                        date = new Date(date);
                    }
                }
                else {
                    date = new Date(date);
                }

                var dateNow = new Date();
                var diff = ((dateNow.getTime() - date.getTime()) / 1000);

                //Nếu lấy thời hạn xử lý thì đảo ngược lại
                if (isGetOverdue) {
                    diff = diff * -1;
                }

                var day_diff = Math.floor(diff / 86400);

                if (day_diff < 0) {
                    day_diff = Math.abs(day_diff);
                    if (day_diff > 365) {
                        return String.format(egov.resources.documents.documentNumberYearOverdue, Math.round(day_diff / 365));
                    }
                    else if (day_diff > 30) {
                        return String.format(egov.resources.documents.documentNumberMonthOverdue, Math.round(day_diff / 30));
                    }
                    else if (day_diff > 6) {
                        return String.format(egov.resources.documents.documentNumberWeekOverdue, Math.round(day_diff / 7));
                    }

                    return String.format(egov.resources.documents.documentNumberDayOverdue, day_diff); //  'QH ' + Math.abs(day_diff) + ' ngày';
                }
                else if (day_diff === 0 && date.getDate() === dateNow.getDate()) {
                    if (diff < 120) {
                        return String.format(egov.resources.time.minbefore, 1);
                    }
                    else if (diff < 3600) {
                        return String.format(egov.resources.time.minbefore, Math.floor(diff / 60));
                    }
                    else {
                        return date.format("HH:mm");
                    }
                }
                else if (dateNow.getDate() - date.getDate() === 1 && dateNow.getMonth() == date.getMonth()) {
                    return egov.resources.time.yesterday; // + ", " + date.format("HH:mm")
                }
                else if (date.weekOfYear() === dateNow.weekOfYear()) {
                    return date.getVNDay();
                }
                else if (date.getFullYear() === dateNow.getFullYear()) {
                    return date.format("dd/M");
                }

                return date.format("dd/MM/yy");
            },

            getCommonTime: function (date) {
                /// <summary>
                /// Lấy ngày xử lý thông thường
                /// </summary>
                /// <param name="date">Ngày truyền vào</param>
                if (date == null || date == '') {
                    return egov.resources.documents.unlimitedTime;
                }

                if (regexUtcDate.test(date) || (date instanceof Date)) {
                    if (regexUtcDate.test(date)) {
                        var array = date.split(/[^0-9]/);
                        date = new Date(array[0], array[1] - 1, array[2], array[3], array[4], array[5])
                    } else {
                        date = new Date(date);
                    }
                }
                else {
                    date = new Date(date);
                }

                var dateNow = new Date();
                var diff = ((dateNow.getTime() - date.getTime()) / 1000);

                var day_diff = Math.floor(diff / 86400);
                if (day_diff < 0) {
                    //Trường hợp này thời gian của server chạy nhanh hơn thời gian hiện tại của client
                    return egov.resources.time.justnow;
                }
                else if (day_diff === 0 && date.getDate() === dateNow.getDate()) {
                    if (diff < 120) {
                        return String.format(egov.resources.time.minbefore, 1);
                    }
                    else if (diff < 3600) {
                        return String.format(egov.resources.time.minbefore, Math.floor(diff / 60));
                    }
                    else {
                        return date.format("HH:mm");
                    }
                }
                else if (dateNow.getDate() - date.getDate() === 1 && dateNow.getMonth() == date.getMonth()) {
                    return egov.resources.time.yesterday; //  + ", " + date.format("HH:mm")
                }
                else if (date.weekOfYear() === dateNow.weekOfYear()) {
                    return date.getVNDay();
                }
                else if (date.getFullYear() === dateNow.getFullYear()) {
                    return date.format("dd/MM");
                }
                return date.format("dd/M/yy");
            },

            getDetailDate: function (date, format) {
                if (date == null || date == '') {
                    return egov.resources.documents.unlimitedTime;
                }
                if (regexUtcDate.test(date) || (date instanceof Date)) {
                    if (regexUtcDate.test(date)) {
                        var array = date.split(/[^0-9]/);
                        date = new Date(array[0], array[1] - 1, array[2], array[3], array[4], array[5])
                    } else {
                        date = new Date(date);
                    }
                }
                else {
                    date = new Date(date);
                }
                if (!format) {
                    return date.format("dd/MM/yyyy");
                }
                return date.format(format);
            }
        },

    }
})
(egov = window.egov || {}, $, _);


(function () {

    var Dialog = {
        _confirmEl: "#confirm",
        _tranferEl: ".tranfer-dialog",

        confirm: function (options) {
            var title = options.title;
            var message = options.message;
            var confirm = options.confirm;
            var cancel = options.cancel;
            var $el = $(this._confirmEl);

            var el = document.querySelector(this._confirmEl);

            document.querySelector(".confirm-dialog .mdl-dialog__content").textContent = message;
            document.querySelector(".confirm-dialog .mdl-dialog__title").textContent = title;

            document.querySelector(".confirm-dialog .btnConfirm,.confirm-dialog .btnCancel").removeEventListener("touch", null);

            document.querySelector(".confirm-dialog .btnConfirm").addEventListener("touch", function () {
                el.style["display"] = "none";
                el.close();

                if (typeof confirm === "function")
                    confirm();
                return;
            });

            document.querySelector(".confirm-dialog .btnCancel").addEventListener("touch", function () {
                el.style["display"] = "none";
                el.close();

                if (typeof cancel === "function")
                    cancel();

                return;
            });

            el.style["display"] = "block";
            if (!el.showModal) {
                dialogPolyfill.registerDialog(el);
            }

            el.showModal();
        },

        transfer: function (options) {

        }
    };

    egov.dialog = Dialog;

})();
(function(n){n.template={documentList:{DocumentItem:"text!templates/document_list_item.html"},document:{mobileInfo:"text!templates/document-info.html",mobileCreate:"text!templates/document-create.html",commentMobile:"text!templates/document-comment.html",attachment:"text!templates/document-attachment.html"},bmail:{folderItem:"text!templates/bmail-folder-item.html",listItem:"text!templates/bmail-list-item.html",detail:"text!templates/bmail-detail.html",toolbar:"text!templates/bmail-toolbar.html",attachmentItem:"text!templates/bmail-attachment-item.html",createOrReply:"text!templates/bmail-createorreply.html"},transfer:{transferMobile:"text!templates/transfer-mobile.html",transferItemMobile:"text!templates/transferItem-mobile.html",transferExtendMobile:"text!templates/transferExtend-mobile.html"},calendar:{detail:"text!templates/calendar-detail.html",item:"text!templates/calendar-list-item.html"},toolbar:{mobile:"text!templates/document/toolbar-tablet-mobile.html"},notifyItem:"text!templates/notification-list-item.html"}})(this.egov=this.egov||{});
(function (egov) {

    egov.enum = {

        categoryBusiness: {
            vbden: 1,
            vbdi: 2,
            hsmc: 4,
            kntc: 8,
        },

        urgents: {
            thuong: 1,
            khan: 2,
            hoatoc: 3
        },

        securityLevel: {
            thuong: 1,
            mat: 2,
            toimat: 3
        },

        transferType: {
            xulychinh: 1,
            dongxuly: 2,
            thongbao: 3,
            xyk: 4,
            giamsat: 5
        },

        documentTransferType: {
            taoMoiThongThuong: 1,
            banGiaoThongThuong: 2,
            banGiaoKhiTraLoi: 4,
            banGiaoKhiPhanLoai: 8
        },

        documentListSize: {
            small: 0,
            medium: 1,
            large: 2
        },

        documentViewType: {
            'default': 0,
            preView: 1,     //HIển thị văn bản hồ sơ ở khung preview (Người dung thiết lập hiển  thị toàn bộ thông tin văn bản hồ sơ ở khung xem trước văn bản)
            dialog: 2        //Hiển thị trên văn bản hồ sơ khi hiện dialog khi click 'Chi tiết văn bản ' ở contextmenu
        },

        quickViewType: {
            hide: 0,   ///Không hiển thị tóm tắt văn bản
            right: 1,   //Hiển thị tóm tắt văn bản bên phải
            below: 2   //Hiển thị tóm tắt văn bản bên dưới
        },

        documentOriginal: {
            egov: 0,
            egovOnline: 1,
            other: 2
        },

        fontSizeType: {
            nho: 0,  //Chữ nhỏ
            vua: 1,  //Chữ vừa
            lon: 2   //Chữ lớn
        },

        searchType: {
            document: 1, //Tìm văn bản.
            file: 2       //Tìm trong file.
        },

        processFilterType: {
            group: 1,
            equal: 2,
            custom: 3
        },

        documentStatus: {
            DuThao: 1,
            DangXuLy: 2,
            KetThuc: 4,
            LoaiBo: 8,
            DungXuLy: 16
        },

        permission: {
            khoitaovanban: 1,
            xemvanban: 2,
            dinhkem: 4,
            suavanban: 8,
            guiykien: 16,
            bangiao: 32,
            thongbao: 64,
            xinykien: 128,
            phanloai: 256,
            traloivanban: 512,
            laylaivanban: 1024,
            xacnhanbangiao: 2048,
            xacnhanxuly: 4096,
            yeucaubosung: 8192,
            tiepnhanbosung: 16384,
            kyduyet: 32768,
            traketqua: 65536,
            giahanxuly: 131072,
            dungxuly: 262144,
            ketthucxuly: 524288,
            huyvanban: 1048576,
            luuhosocanhan: 2097152,
            luuso: 4194304,
            phathanh: 8388608,
            capnhatketquaxulycuoi: 16777216,
            luuvanban: 33554432,
            traloiykien: 67108864,
            capphep: 134217728,
            doihanxulykhiphanloai: 268435456,
            molaivanban: 536870912,
            danhlaisoden: 1073741824,
            xoavanbankhoihoso: 2147483648
        },

        actionSpecial: {
            thongThuong: { name: 'ThongThuong', value: 0 },
            luuSoVaPhatHanhNoiBo: { name: 'LuuSoVaPhatHanhNoiBo', value: 1 },
            luuSoNoiBo: { name: 'LuuSoNoiBo', value: 2 },
            luuSoVaPhatHanhRaNgoai: { name: 'LuuSoVaPhatHanhRaNgoai', value: 3 },
            chuyenNguoiKhoiTao: { name: 'ChuyenNguoiKhoiTao', value: 4 },
            chuyenYKienDongGopVbDxl: { name: 'ChuyenYKienDongGopVbDxl', value: 5 },
            tiepTucXuLy: { name: 'TiepTucXuLy', value: 6 },
            chuyenNguoiCoQuyenDongGopYKien: { name: 'ChuyenNguoiCoQuyenDongGopYKien', value: 7 },
            tiepNhanHoSo: { name: 'TiepNhanHoSo', value: 8 },
            tiepNhanHoSoVaTiepTuc: { name: 'TiepNhanHoSoVaTiepTuc', value: 9 },
            capNhatKetQuaDungXuLy: { name: 'CapNhatKetQuaDungXuLy', value: 10 },
            chuyenYKienDongGopVbXinYKien: { name: 'ChuyenYKienDongGopVbXinYKien', value: 11 },
            chuyenNguoiGui: { name: 'ChuyenNguoiGui', value: 12 },
            tiepNhanBoSung: { name: 'TiepNhanBoSung', value: 13 },
            lienThong: { name: 'LienThong', value: 14 }
        },

        commentType: {
            Common: 1,
            Consulted: 2,
            Contribution: 3,
            Supplementary: 4,
            Signed: 5,
            Success: 6,
            Finished: 7
        },

        formType: {
            html: 1,
            dynamic: 2,
            fromUrl: 3
        },

        language: {
            VietNam: 1,
            Laos: 2
        },

        defaultToolbar: {
            Create: 1,
            Edit: 2,
            InsertImagePacket: 3
        },

        commonTemplate: {
            InBienNhanBanGiao: 1
        }
    };

    //#region HSMC

    egov.enum.feeType = {
        TiepNhan: 1,
        ThuongBosung: 2,
        TraCongDan: 3
    };

    egov.enum.paperType = {
        TiepNhan: 1,
        ThuongBosung: 2,
        TraCongDan: 3
    };

    egov.enum.supplementaryType = {
        renew: 1,
        "continue": 2,
        add: 3
    };

    egov.enum.printProcessType = {
        TiepNhan: 1,

        BanGiao: 2,

        KyDuyet: 4,

        TraKetQua: 8,

        TiepNhanBoSung: 16,

        GiaHan: 32
    };

    //#endregion


})(this.egov = this.egov || {});
(function (egov) {

    egov.models = egov.models || {};
    egov.viewModels = egov.viewModels || {};

    //#region Document

    egov.models.document = Backbone.Model.extend({
        defaults: {
            Address: '',
            CategoryBusinessId: 0,
            CategoryId: 0,
            CitizenInfo: null,
            CitizenName: null,
            Color: 0,
            Comment: "",
            Comments: {},
            Compendium: "",
            Compendium2: "",
            DateAppointed: "",
            DateArrived: null,
            DateCreated: new Date(),
            DateFinished: null,
            DateModified: "",
            DateOfIssueCode: null,
            DatePublished: null,
            DateReceived: "",
            DateReceivedFormat: "",
            DateResponse: null,
            DateResponsed: null,
            DateResponsedOverdue: null,
            DateResult: "",
            DateReturned: null,
            DateSuccess: null,
            DocCode: null,
            DocFieldId: null,
            DocFieldIds: null,
            DocType: {},
            DocTypeId: "",
            DocTypePermission: 0,
            DocumentCopyId: 0,
            DocumentId: "00000000-0000-0000-0000-000000000000",
            Email: null,
            IdentityCard: null,
            InOutCode: null,
            InOutPlace: "",
            IsAcknowledged: 0,
            IsConverted: 0,
            IsDocumentImportant: 0,
            IsGettingOut: 0,
            IsReturned: null,
            IsSuccess: null,
            IsSupplemented: null,
            IsViewed: 0,
            Keyword: null,
            LastComment: "",
            LastUserIdComment: 0,
            Organization: null,
            Original: 1,
            Phone: null,
            ProcessedMinutes: null,
            ResultStatus: null,
            ReturnNote: null,
            SecurityId: null,
            SendTypeId: null,
            Status: 0,
            StoreId: null,
            SuccessNote: null,
            TotalPage: null,
            UrgentId: 1,
            UserCurrentId: 0,
            UserCreatedId: 0,
            UserCurrentFirstName: "",
            UserCurrentFullName: "",
            UserReturnedId: null,
            UserSuccessId: null,
            Selected: false,
            DocumentCopyType: 0,
            DocCopyStatus: 0,
            DateOverdue: '',
            NumberDayOverdue: '0',    //hạn giữ
            NumberDayAppointed: '0',  //Hạn tổng
            IsFile: 0,                 //Có phải là file hay không(dùng trong sổ hồ sơ):Mặc định là 0(không phải file)
            WorkflowId: 0,            //Quy trình của văn bản
            NodeCurrentId: 0,          //Node hiện tại của văn bản trên quy trình
            DocCopyDateModified: null,
            WorkflowTypes: "",
            WorkflowTypeId: "",
            WorkflowTypeName: "",
            Note: ""
        },

        initialize: function () {
            this.set('id', this.get('DocumentCopyId'));
        }
    });

    egov.models.documentList = Backbone.Collection.extend({
        model: egov.models.document
    });

    //#endregion

    //#region Document Permission

    egov.models.documentPermission = Backbone.Model.extend({
        defaults: { value: 0 }
    });

    egov.models.documentPermissionList = Backbone.Collection.extend({
        model: egov.models.documentPermission
    });

    //#endregion

    //#region Documents Toolbar

    egov.models.toolbar = Backbone.Model.extend({
        defaults: {
            text: '',
            className: '',
            disable: false,
            icon: '',
            dataUrl: '',
            shortKey: '',
            data: null,
            callback: null,
            dropdownWidth: 90,
            dropdownHeight: 0,
            position: {
                at: 'right bottom',
                my: 'right top'
            },
            contentId: '',
            isDatePicker: false,
            isDropdownMenu: false,
            hasShortKey: false,
            showSelected: false,
            defaultSelectedText: 'Tất cả'
        }
    });

    egov.models.toolbarList = Backbone.Collection.extend({
        model: egov.models.toolbar
    });

    //#endregion

    //#region Context Menu

    egov.models.contextMenu = Backbone.Model.extend({
        defaults: {
            selector: null,
            trigger: 'right',   // 'left'
            dataUrl: '',        // Url lấy dữ liệu (nếu có)
            param: '',          // Param cho url
            data: null,         // Danh sách các item là thể hiện của collection egov.models.ContextMenuItemModel
            callback: null,     // Hàm thực thi khi select trước thi thực thi hàm callback trong data
            style: {},
            position: {},
            isDatePicker: false, // Đặt true nếu muốn thể hiện nội dung xổ ra là datatimepicker
            // Hiển thị loading trước rồi bind dữ liệu sau
            // ví dụ: var context = selector.contextmenu({isShowLoading: true});
            // context.model.set('data', data);
            // context.render();
            isShowLoading: false,
            key: null
        },

        initialize: function () {
            var style = this.get('style');
            if (style.height === 0 || style.height === undefined) {
                style.height = 'auto';
            }

            this.set('style', $.extend({}, {
                display: 'none'
            }, style));

            this.set('position', $.extend({}, {
                at: 'right bottom',
                my: 'right top'
            }, this.get('position')));
        }
    });

    egov.models.contextMenuItem = Backbone.Model.extend({
        defaults: {
            text: '',
            value: '',
            callback: '',
            icon: '',
            selected: false
        },

        initialize: function () {
            var rootIconFolder = '/Content/Images/Toolbar/';
            if (this.get('icon') !== '') {
                this.set('icon', rootIconFolder + this.get('icon'));
            }
        }
    });

    egov.models.contextMenuList = Backbone.Collection.extend({
        model: egov.models.contextMenuItem
    });

    //#endregion

    //#region Tree

    var showTotalInTreeType = {
        none: 0,         //Không hiển thị
        unread: 1,       //Văn bản chưa đọc
        unreadOnAll: 2,  //Chưa đọc / Tất cả
        all: 3           //Tất cả
    };

    egov.models.TreeModel = Backbone.Model.extend({
        defaults: {
            functionId: 0,
            parentId: 0,
            name: "",
            params: "",
            paramId: 0,
            icon: "",
            state: "closed",
            order: 0,
            totalDocumentUnread: 0,
            totalDocument: 0,
            children: [],
            url: "",
            pagingUrl: "",
            isLoadChildren: false,
            showTotalInTreeType: showTotalInTreeType.unread,
            defaultSort: null,
            hasUyQuyen: false,
            userUyQuyen: null,
            isOpen: false,
            isSelected: false,
            hasTransferTheoLo: false,
            isOnlineRegistration: false,
            treeGroupId: null,
            treeGroupOrder: 0,
            hasExportFile: false
        },

        initialize: function () {
            this.set('id', this.get('functionId'));
            var url = '/Home/GetDocuments/' + this.get('functionId');
            var pagingUrl = '/Home/GetDocumentPaging/' + this.get('functionId');
            if (typeof this.get('params') === 'object') {
                this.set('params', JSON.stringify(this.get('params')));
            }
            if (this.get('params') !== '') {
                url += '?params=' + this.get('params');
                pagingUrl += '?params=' + this.get('params');
            }
            this.set('url', url);
            this.set('pagingUrl', pagingUrl);
            this.set('isLoadChildren', false);

            //  this.set("children", new egov.models.TreeList(this.get("children") ? this.get("children") : []));

            if (typeof this.get('defaultSort') === 'object') {
                this.set('defaultSort', JSON.stringify(this.get('defaultSort')));
            }

            if (typeof this.get('userUyQuyen') === 'string') {
                this.set('userUyQuyen', JSON.parse(this.get('userUyQuyen')));
            }
        }
    });

    egov.models.TreeList = Backbone.Collection.extend({
        model: egov.models.TreeModel
    });

    egov.models.StorePrivateModel = Backbone.Model.extend({
        defaults: {
            level: 0,
            parentId: 0,
            status: 0,
            storePrivateId: 0,
            storePrivateName: "",
            name: "",
            children: [],
            state: "closed",
            url: "",
            pagingUrl: "",
            root: false,
            isStoreShared: false,
            descStorePrivate: "",
            userIdJoined: [],
            deptExtJoined: []
        },

        initialize: function () {
            var pagingUrl = '/StorePrivate/GetDocuments/' + this.get('storePrivateId');
            this.set('id', this.get('storePrivateId'));
            this.set('pagingUrl', pagingUrl);

            this.set("children", new egov.models.StorePrivateList(this.get("children") ? this.get("children") : []));
        }
    });

    egov.models.StorePrivateList = Backbone.Collection.extend({
        model: egov.models.StorePrivateModel
    });

    //#endregion

    //#region Question

    egov.models.question = Backbone.Model.extend({
        defaults: {
            QuestionId: 0,
            Date: "",
            AskPeople: "",
            Detail: "",
            Name: "",
            Email: "",
            Phone: "",
            DocCode: "",
            QuestionType: 0,
            IsGeneralQuestion: true,
            UserComments: [],
            AnswerHolder: null,
            isMe: false,
            Compendium: "",
            DocumentHolderName: "",
            DocumentHolderAccount: "",
            DocumentHolderFullAccount: "",
        },

        initialize: function () {
        }
    });

    egov.models.questionList = Backbone.Collection.extend({
        model: egov.models.question
    });

    egov.models.QuestionTreeModel = Backbone.Model.extend({
        defaults: {
            level: 0,
            status: 0,
            name: "",
            children: [],
            state: "closed",
            root: false,
            isGeneral: false
        },

        initialize: function () {
            this.set("children", new egov.models.QuestionTreeList(this.get("children") ? this.get("children") : []));
        }
    });

    egov.models.QuestionTreeList = Backbone.Collection.extend({
        model: egov.models.QuestionTreeModel
    });

    //#endregion

    //#region Tabs

    egov.models.TabModel = Backbone.Model.extend({
        defaults: {
            id: 0,
            name: '',
            title: '',
            href: '',
            hasTooltip: true,
            hasCloseButton: false,
            isRoot: false,
            privateId: 0,
            //  isCookie: false,
            attributes: {},
            isCreateDocument: false,
            type: 0,
            cateBusId: 0,
            hasLoadContent: true
        }
    });

    egov.models.TabList = Backbone.Collection.extend({
        model: egov.models.TabModel
    });

    //#endregion

    //#region Transfer

    egov.models.action = Backbone.Model.extend({
        defaults: {
            currentNodeId: 0,
            id: "",
            isAllow: true,
            isAllowSign: false,
            isSpecial: false,
            name: "",
            nextNodeId: 0,
            priority: 0,
            userIdNext: 0,
            workflowId: 0
        }
    });

    egov.models.userAction = Backbone.Model.extend({
        defaults: {
            id: 0,
            value: 0,
            label: '',
            fullname: '',
            department: '',
            username: '',
            position: '',
            isMainProcess: false,
            isCoProcess: false
        },

        initialize: function () {
            this.set('id', this.get('value'));
        }
    });

    egov.models.actionUserList = Backbone.Collection.extend({
        model: egov.models.userAction
    });

    //#endregion

    //#region Attachment

    egov.models.attachment = Backbone.Model.extend({
        defaults: {
            Id: 0,
            Name: '',
            Extension: '',
            Size: 0,
            Versions: [],
            fileData: undefined,
            isRemoved: false,
            isNew: false,
            isMofified: false,
            isOpen: false,
            icon: ''
        },

        initialize: function () {
            var extension,
                icon;

            extension = this.get('Extension');
            if (extension.indexOf('.') !== 0) {
                extension = "." + extension;
            }

            switch (extension) {
                case '.doc':
                case '.docx':
                    icon = 'icon-file-word';
                    break;
                case '.xls':
                case '.xlsx':
                    icon = 'icon-file-excel';
                    break;
                case '.pdf':
                    icon = 'icon-file-pdf';
                    break;
                case '.txt':
                    icon = 'icon-text';
                    break;
                case '.zip':
                case '.rar':
                case '.7z':
                    icon = 'icon-file-zip';
                    break;
                case '.ppt':
                case '.pptx':
                    icon = 'icon-file-powerpoint';
                    break;
                case '.html':
                    icon = 'icon-chrome';
                    break;
                case '.jpg':
                case '.jpeg':
                case '.bmp':
                case '.png':
                case '.ico':
                case '.gif':
                    icon = 'icon-image2';
                    break;
                default:
                    icon = 'icon-file4';
                    break;
            }

            this.set('icon', icon);
        }
    });

    egov.models.attachmentList = Backbone.Collection.extend({
        model: egov.models.attachment
    });

    //#endregion

    //#region Relation

    egov.models.relation = Backbone.Model.extend({
        defaults: {
            id: 0,
            RelationCopyId: 0,
            RelationId: '',
            RelationType: 0,
            IsAddNext: false,
            Compendium: '',
            CitizenName: '',
            DocCode: '',
            DateCreated: '',
            CategoryName: '',
            IsRemoved: false,
            IsNew: false
        },

        initialize: function () {
            // Thiết lập id để tránh gán trùng relation id
            this.set('id', this.get('RelationCopyId'));
        }
    });

    egov.models.relationList = Backbone.Collection.extend({
        model: egov.models.relation
    });

    //#endregion

    //#region QuickView

    egov.models.quickView = Backbone.Model.extend({
        defaults: {
            id: 0,
            type: 1,
            compendium: null,
            lastComment: null,
            category: null,
            department: null,
            dateCreate: null,
            lastUser: null,
            docField: null,
            urgent: null,
            docCode: null,
            totalPage: null,

            ///online
            docType: null,
            dateReceived: null,
            DateReceivedFormat: "",
            dateAppoint: null,
            personInfo: null,
            email: null,
            phone: null,
            address: null,
        },
        initialize: function () {

        }
    });

    egov.models.quickViewList = Backbone.Collection.extend({

        model: egov.models.quickView,

        initialize: function () {
        }
    });

    //#endregion

    //#region Publish

    egov.models.address = Backbone.Model.extend({
        defaults: {
            IsShow: false,
            ParentId: null
        },
        initialize: function () {
            // Set các trường cho autocomplete
            this.set('id', this.get('AddressId'));
            this.set('value', this.get('AddressId'));
            this.set('label', this.get('Name'));
            this.set('isSelected', false);
        }
    });

    egov.models.addressCollection = Backbone.Collection.extend({
        model: egov.models.address
    });

    egov.models.publish = Backbone.Model.extend({
        defaults: {
            StoreId: 0,
            CodeId: 0,
            Code: '',
            DatePublished: new Date(),
            DateResponse: null,
            TotalPage: 1,
            SecurityId: 1,
            TotalCopy: 1,
            Approvers: '',
            KeyWordId: 0,
            InPlace: 0,
            IsCustomCode: false,
            Address: []
        }
    });

    //#endregion

    //#region Supplementary

    egov.models.supplementary = Backbone.Model.extend({
        SupplementaryId: 0,
        Comment: "",
        DateSend: "",
        IsDeleted: false,
        UserSendId: 0,
        UserSendName: ""
    });

    egov.models.supplementaryList = Backbone.Collection.extend({
        model: egov.models.supplementary
    });

    //#endregion

    //#region Search

    egov.models.search = Backbone.Model.extend({
        defaults: {
            Compendium: '',
            CategoryId: null,
            KeyWord: '',
            Content: '',
            DocCode: '',
            InOutCode: '',
            UrgentId: null,
            CategoryBusinessId: null,
            StorePrivateId: null,
            CurrentUserId: null,
            InOutPlace: '',
            FromDateStr: '',
            ToDateStr: '',
            BeforeDate: '',
            AfterDate: '',
            OrganizationCreate: '',
            DocFieldId: null,
            UserSuccessId: null
        }
    });

    egov.models.searchResultItem = Backbone.Model.extend({
        defaults: {
            Address: '',
            CategoryName: '',
            CitizenName: '',
            Compendium: '',
            DateAppointed: '',
            DateArrived: '',
            DateCreated: '',
            DocCode: '',
            DocumentCopyId: 0,
            DocumentId: '',
            InOutCode: 0,
            LastUserComment: '',
            UserSuccess: '',
            DateReceived: '',
            IsSelected: false
        }
    });

    egov.models.searchResult = Backbone.Collection.extend({
        model: egov.models.searchResultItem
    });

    //#endregion

    //#region Modal

    egov.models.modal = Backbone.Model.extend({
        defaults: {
            title: '',  // Tiêu đề modal
            keyboard: false, // Ẩn modal khi nhân esc
            resizable: false, // cho phép thay đổi kích thước
            draggable: false, // cho phép kéo thả vị trí
            animation: true, // hiển thị dạng fadein
            remote: '', // url nội dung modal - dùng để load nội dung modal sau theo url
            content: '', // nội dung modal - html có sẵn
            height: 'auto', // chiều cao
            width: 'auto', // chiều rộng,
            ignoreText: '',
            buttons: [], // các nút chức năng
            hide: null,   // callback sau khi ẩn modal
            close: null,   // callback trước khi ẩn modal 
            loaded: null,   // callback sau khi load xong nội dung = remote url.
            backdrop: "static",
            confirm: null
        }
    });

    //#endregion

    //#region Document Permission

    egov.models.totalNotifications = Backbone.Model.extend({
        defaults: {
            totalNotify: 0,
            total: 0
        }
    });

    egov.models.notification = Backbone.Model.extend({
        defaults: {
            NotificationId: 0,
            NotificationType: 0,
            Title: "",
            Content: "",
            SenderAvatar: "",
            SenderUserName: "",
            SenderFullName: "",
            Date: "",
            DateFormat: "",
            ReceiveDate: "",
            ViewdDate: "",
            IsViewed: false,

            //egov
            DocumentCopyId: 0,

            //mail
            MailId: 0,
            folderId: 0,

            //chat
            ChatId: "",
            chatterJid: "",
            messageId: ""
        }
    });

    egov.models.notificationList = Backbone.Collection.extend({
        model: egov.models.notification
    });

    egov.models.layoutNotify = Backbone.Model.extend({
        defaults: {
            total: 0,
            unreadTotal: 0,
            model: []
        }
    });

    //#endregion

    //#region Gán ra model cho các view. Mỗi view chỉ sử dụng một model tương ứng.

    // Cây văn bản
    egov.viewModels.tree = new egov.models.TreeList();

    // Danh sách văn bản
    egov.viewModels.documentList = new egov.models.documentList();

    // Danh sách tab
    egov.viewModels.tabList = new egov.models.TabList();

    // Danh sách người nhận trên form bàn giao
    egov.viewModels.actionUserList = new egov.models.actionUserList();

    //#endregion

})
(this.egov = this.egov || {});


(function (egov, $) {
    /**
    * PubSub như là một hệ thống EventEmitter.
    * Các Widget đăng ký các sự kiện publish và sự kiện được sử dụng chung cho tất cả các subscriber khác.
    * 
    * Notes: sử dụng egov.events.js để quản lý tên các event được đăng ký.
    */
    egov.pubsub = (function () {
        var queue = [],
            that = {};

        that.publish = function (eventName, data, position) {
            /// <summary>
            /// Thực thi các hàm callback được liên kết với eventName
            /// </summary>
            /// <param name="eventName">Tên event cần thực thi</param>
            /// <param name="data">Dữ liệu truyền cho hàm callback</param>
            var context, intervalId, idx = 0;
            if (queue[eventName]) {
                intervalId = setInterval(function () {
                    if (queue[eventName][idx]) {
                        try {
                            context = queue[eventName][idx].context || this;
                            queue[eventName][idx].callback.call(context, data, position);
                        } catch (e) {
                            // log the message for developers
                            egov.log('Có lỗi xảy ra khi thực thi một trong những hàm callback cho sự kiện "' + eventName + '"');
                            egov.log('Lỗi đó là: "' + e + '"');
                        }

                        idx += 1;
                    } else {
                        clearInterval(intervalId);
                    }
                }, 0);
            }
        };

        that.subscribe = function (eventName, callback, context) {
            /// <summary>
            /// Đăng ký một sự kiện. Cá Sự kiện đăng ký tiếp theo sẽ luôn được thêm vào (chứ không overwrite).
            /// Để hủy bỏ đăng ký một sự kiện, sử dụng hàm unsubscribe.
            /// </summary>
            /// <param name="eventName">Tên sự kiện đăng ký, nên sử dụng dấu . để phân biệt các event</param>
            /// <param name="callback">Hàm thực thi.</param>
            /// <param name="context">Context để thực thi hàm callback</param>
            if (!queue[eventName]) {
                queue[eventName] = [];
            }
            queue[eventName].push({
                callback: callback,
                context: context
            });
        };

        that.unsubscribe = function (eventName, callback, context) {
            /// <summary>
            /// Hủy bỏ đăng ký sự kiện.
            /// </summary>
            /// <param name="eventName">Tên sự kiện</param>
            /// <param name="callback">Hàm callback sau khi hủy bỏ. Sử dụng để chắc chắn rằng sự kiện đã được hủy bỏ.</param>
            /// <param name="context">Context thực thi hàm callback.</param>
            if (queue[eventName]) {
                queue[eventName].pop({
                    callback: callback,
                    context: context
                });
            }
        };

        return that;
    }());

}(this.egov = this.egov || {}, jQuery));

