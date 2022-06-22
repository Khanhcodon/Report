/*! jQuery v2.2.3 | (c) jQuery Foundation | jquery.org/license */
!function(n,t){"object"==typeof module&&"object"==typeof module.exports?module.exports=n.document?t(n,!0):function(n){if(!n.document)throw new Error("jQuery requires a window with a document");return t(n)}:t(n)}("undefined"!=typeof window?window:this,function(n,t){function ii(n){var t=!!n&&"length"in n&&n.length,r=i.type(n);return"function"===r||i.isWindow(n)?!1:"array"===r||0===t||"number"==typeof t&&t>0&&t-1 in n}function ri(n,t,r){if(i.isFunction(t))return i.grep(n,function(n,i){return!!t.call(n,i,n)!==r});if(t.nodeType)return i.grep(n,function(n){return n===t!==r});if("string"==typeof t){if(bf.test(t))return i.filter(t,n,r);t=i.filter(t,n)}return i.grep(n,function(n){return lt.call(t,n)>-1!==r})}function hr(n,t){while((n=n[t])&&1!==n.nodeType);return n}function kf(n){var t={};return i.each(n.match(h)||[],function(n,i){t[i]=!0}),t}function yt(){u.removeEventListener("DOMContentLoaded",yt);n.removeEventListener("load",yt);i.ready()}function et(){this.expando=i.expando+et.uid++}function lr(n,t,r){var u;if(void 0===r&&1===n.nodeType)if(u="data-"+t.replace(cr,"-$&").toLowerCase(),r=n.getAttribute(u),"string"==typeof r){try{r="true"===r?!0:"false"===r?!1:"null"===r?null:+r+""===r?+r:df.test(r)?i.parseJSON(r):r}catch(f){}e.set(n,t,r)}else r=void 0;return r}function vr(n,t,r,u){var h,e=1,l=20,c=u?function(){return u.cur()}:function(){return i.css(n,t,"")},s=c(),o=r&&r[3]||(i.cssNumber[t]?"":"px"),f=(i.cssNumber[t]||"px"!==o&&+s)&&ot.exec(i.css(n,t));if(f&&f[3]!==o){o=o||f[3];r=r||[];f=+s||1;do e=e||".5",f/=e,i.style(n,t,f+o);while(e!==(e=c()/s)&&1!==e&&--l)}return r&&(f=+f||+s||0,h=r[1]?f+(r[1]+1)*r[2]:+r[2],u&&(u.unit=o,u.start=f,u.end=h)),h}function o(n,t){var r="undefined"!=typeof n.getElementsByTagName?n.getElementsByTagName(t||"*"):"undefined"!=typeof n.querySelectorAll?n.querySelectorAll(t||"*"):[];return void 0===t||t&&i.nodeName(n,t)?i.merge([n],r):r}function ui(n,t){for(var i=0,u=n.length;u>i;i++)r.set(n[i],"globalEval",!t||r.get(t[i],"globalEval"))}function kr(n,t,r,u,f){for(var e,s,p,a,w,v,h=t.createDocumentFragment(),y=[],l=0,b=n.length;b>l;l++)if(e=n[l],e||0===e)if("object"===i.type(e))i.merge(y,e.nodeType?[e]:e);else if(br.test(e)){for(s=s||h.appendChild(t.createElement("div")),p=(pr.exec(e)||["",""])[1].toLowerCase(),a=c[p]||c._default,s.innerHTML=a[1]+i.htmlPrefilter(e)+a[2],v=a[0];v--;)s=s.lastChild;i.merge(y,s.childNodes);s=h.firstChild;s.textContent=""}else y.push(t.createTextNode(e));for(h.textContent="",l=0;e=y[l++];)if(u&&i.inArray(e,u)>-1)f&&f.push(e);else if(w=i.contains(e.ownerDocument,e),s=o(h.appendChild(e),"script"),w&&ui(s),r)for(v=0;e=s[v++];)wr.test(e.type||"")&&r.push(e);return h}function pt(){return!0}function nt(){return!1}function gr(){try{return u.activeElement}catch(n){}}function fi(n,t,r,u,f,e){var o,s;if("object"==typeof t){"string"!=typeof r&&(u=u||r,r=void 0);for(s in t)fi(n,s,r,u,t[s],e);return n}if(null==u&&null==f?(f=r,u=r=void 0):null==f&&("string"==typeof r?(f=u,u=void 0):(f=u,u=r,r=void 0)),f===!1)f=nt;else if(!f)return n;return 1===e&&(o=f,f=function(n){return i().off(n),o.apply(this,arguments)},f.guid=o.guid||(o.guid=i.guid++)),n.each(function(){i.event.add(this,t,f,u,r)})}function nu(n,t){return i.nodeName(n,"table")&&i.nodeName(11!==t.nodeType?t:t.firstChild,"tr")?n.getElementsByTagName("tbody")[0]||n.appendChild(n.ownerDocument.createElement("tbody")):n}function ee(n){return n.type=(null!==n.getAttribute("type"))+"/"+n.type,n}function oe(n){var t=ue.exec(n.type);return t?n.type=t[1]:n.removeAttribute("type"),n}function tu(n,t){var u,c,f,s,h,l,a,o;if(1===t.nodeType){if(r.hasData(n)&&(s=r.access(n),h=r.set(t,s),o=s.events)){delete h.handle;h.events={};for(f in o)for(u=0,c=o[f].length;c>u;u++)i.event.add(t,f,o[f][u])}e.hasData(n)&&(l=e.access(n),a=i.extend({},l),e.set(t,a))}}function se(n,t){var i=t.nodeName.toLowerCase();"input"===i&&yr.test(n.type)?t.checked=n.checked:"input"!==i&&"textarea"!==i||(t.defaultValue=n.defaultValue)}function b(n,t,u,e){t=gi.apply([],t);var l,p,c,a,s,w,h=0,v=n.length,d=v-1,y=t[0],k=i.isFunction(y);if(k||v>1&&"string"==typeof y&&!f.checkClone&&re.test(y))return n.each(function(i){var r=n.eq(i);k&&(t[0]=y.call(this,i,r.html()));b(r,t,u,e)});if(v&&(l=kr(t,n[0].ownerDocument,!1,n,e),p=l.firstChild,1===l.childNodes.length&&(l=p),p||e)){for(c=i.map(o(l,"script"),ee),a=c.length;v>h;h++)s=l,h!==d&&(s=i.clone(s,!0,!0),a&&i.merge(c,o(s,"script"))),u.call(n[h],s,h);if(a)for(w=c[c.length-1].ownerDocument,i.map(c,oe),h=0;a>h;h++)s=c[h],wr.test(s.type||"")&&!r.access(s,"globalEval")&&i.contains(w,s)&&(s.src?i._evalUrl&&i._evalUrl(s.src):i.globalEval(s.textContent.replace(fe,"")))}return n}function iu(n,t,r){for(var u,e=t?i.filter(t,n):n,f=0;null!=(u=e[f]);f++)r||1!==u.nodeType||i.cleanData(o(u)),u.parentNode&&(r&&i.contains(u.ownerDocument,u)&&ui(o(u,"script")),u.parentNode.removeChild(u));return n}function ru(n,t){var r=i(t.createElement(n)).appendTo(t.body),u=i.css(r[0],"display");return r.detach(),u}function oi(n){var r=u,t=ei[n];return t||(t=ru(n,r),"none"!==t&&t||(wt=(wt||i("<iframe frameborder='0' width='0' height='0'/>")).appendTo(r.documentElement),r=wt[0].contentDocument,r.write(),r.close(),t=ru(n,r),wt.detach()),ei[n]=t),t}function tt(n,t,r){var o,s,h,u,e=n.style;return r=r||bt(n),u=r?r.getPropertyValue(t)||r[t]:void 0,""!==u&&void 0!==u||i.contains(n.ownerDocument,n)||(u=i.style(n,t)),r&&!f.pixelMarginRight()&&si.test(u)&&uu.test(t)&&(o=e.width,s=e.minWidth,h=e.maxWidth,e.minWidth=e.maxWidth=e.width=u,u=r.width,e.width=o,e.minWidth=s,e.maxWidth=h),void 0!==u?u+"":u}function ci(n,t){return{get:function(){return n()?void delete this.get:(this.get=t).apply(this,arguments)}}}function su(n){if(n in ou)return n;for(var i=n[0].toUpperCase()+n.slice(1),t=eu.length;t--;)if(n=eu[t]+i,n in ou)return n}function hu(n,t,i){var r=ot.exec(t);return r?Math.max(0,r[2]-(i||0))+(r[3]||"px"):t}function cu(n,t,r,u,f){for(var e=r===(u?"border":"content")?4:"width"===t?1:0,o=0;4>e;e+=2)"margin"===r&&(o+=i.css(n,r+w[e],!0,f)),u?("content"===r&&(o-=i.css(n,"padding"+w[e],!0,f)),"margin"!==r&&(o-=i.css(n,"border"+w[e]+"Width",!0,f))):(o+=i.css(n,"padding"+w[e],!0,f),"padding"!==r&&(o+=i.css(n,"border"+w[e]+"Width",!0,f)));return o}function lu(t,r,e){var h=!0,o="width"===r?t.offsetWidth:t.offsetHeight,s=bt(t),c="border-box"===i.css(t,"boxSizing",!1,s);if(u.msFullscreenElement&&n.top!==n&&t.getClientRects().length&&(o=Math.round(100*t.getBoundingClientRect()[r])),0>=o||null==o){if(o=tt(t,r,s),(0>o||null==o)&&(o=t.style[r]),si.test(o))return o;h=c&&(f.boxSizingReliable()||o===t.style[r]);o=parseFloat(o)||0}return o+cu(t,r,e||(c?"border":"content"),h,s)+"px"}function au(n,t){for(var e,u,s,o=[],f=0,h=n.length;h>f;f++)u=n[f],u.style&&(o[f]=r.get(u,"olddisplay"),e=u.style.display,t?(o[f]||"none"!==e||(u.style.display=""),""===u.style.display&&st(u)&&(o[f]=r.access(u,"olddisplay",oi(u.nodeName)))):(s=st(u),"none"===e&&s||r.set(u,"olddisplay",s?e:i.css(u,"display"))));for(f=0;h>f;f++)u=n[f],u.style&&(t&&"none"!==u.style.display&&""!==u.style.display||(u.style.display=t?o[f]||"":"none"));return n}function s(n,t,i,r,u){return new s.prototype.init(n,t,i,r,u)}function pu(){return n.setTimeout(function(){it=void 0}),it=i.now()}function dt(n,t){var r,u=0,i={height:n};for(t=t?1:0;4>u;u+=2-t)r=w[u],i["margin"+r]=i["padding"+r]=n;return t&&(i.opacity=i.width=n),i}function wu(n,t,i){for(var u,f=(l.tweeners[t]||[]).concat(l.tweeners["*"]),r=0,e=f.length;e>r;r++)if(u=f[r].call(i,t,n))return u}function le(n,t,u){var f,a,p,v,o,w,h,b,l=this,y={},s=n.style,c=n.nodeType&&st(n),e=r.get(n,"fxshow");u.queue||(o=i._queueHooks(n,"fx"),null==o.unqueued&&(o.unqueued=0,w=o.empty.fire,o.empty.fire=function(){o.unqueued||w()}),o.unqueued++,l.always(function(){l.always(function(){o.unqueued--;i.queue(n,"fx").length||o.empty.fire()})}));1===n.nodeType&&("height"in t||"width"in t)&&(u.overflow=[s.overflow,s.overflowX,s.overflowY],h=i.css(n,"display"),b="none"===h?r.get(n,"olddisplay")||oi(n.nodeName):h,"inline"===b&&"none"===i.css(n,"float")&&(s.display="inline-block"));u.overflow&&(s.overflow="hidden",l.always(function(){s.overflow=u.overflow[0];s.overflowX=u.overflow[1];s.overflowY=u.overflow[2]}));for(f in t)if(a=t[f],vu.exec(a)){if(delete t[f],p=p||"toggle"===a,a===(c?"hide":"show")){if("show"!==a||!e||void 0===e[f])continue;c=!0}y[f]=e&&e[f]||i.style(n,f)}else h=void 0;if(i.isEmptyObject(y))"inline"===("none"===h?oi(n.nodeName):h)&&(s.display=h);else{e?"hidden"in e&&(c=e.hidden):e=r.access(n,"fxshow",{});p&&(e.hidden=!c);c?i(n).show():l.done(function(){i(n).hide()});l.done(function(){var t;r.remove(n,"fxshow");for(t in y)i.style(n,t,y[t])});for(f in y)v=wu(c?e[f]:0,f,l),f in e||(e[f]=v.start,c&&(v.end=v.start,v.start="width"===f||"height"===f?1:0))}}function ae(n,t){var r,f,e,u,o;for(r in n)if(f=i.camelCase(r),e=t[f],u=n[r],i.isArray(u)&&(e=u[1],u=n[r]=u[0]),r!==f&&(n[f]=u,delete n[r]),o=i.cssHooks[f],o&&"expand"in o){u=o.expand(u);delete n[f];for(r in u)r in n||(n[r]=u[r],t[r]=e)}else t[f]=e}function l(n,t,r){var e,o,s=0,a=l.prefilters.length,f=i.Deferred().always(function(){delete c.elem}),c=function(){if(o)return!1;for(var s=it||pu(),t=Math.max(0,u.startTime+u.duration-s),h=t/u.duration||0,i=1-h,r=0,e=u.tweens.length;e>r;r++)u.tweens[r].run(i);return f.notifyWith(n,[u,i,t]),1>i&&e?t:(f.resolveWith(n,[u]),!1)},u=f.promise({elem:n,props:i.extend({},t),opts:i.extend(!0,{specialEasing:{},easing:i.easing._default},r),originalProperties:t,originalOptions:r,startTime:it||pu(),duration:r.duration,tweens:[],createTween:function(t,r){var f=i.Tween(n,u.opts,t,r,u.opts.specialEasing[t]||u.opts.easing);return u.tweens.push(f),f},stop:function(t){var i=0,r=t?u.tweens.length:0;if(o)return this;for(o=!0;r>i;i++)u.tweens[i].run(1);return t?(f.notifyWith(n,[u,1,0]),f.resolveWith(n,[u,t])):f.rejectWith(n,[u,t]),this}}),h=u.props;for(ae(h,u.opts.specialEasing);a>s;s++)if(e=l.prefilters[s].call(u,n,h,u.opts))return i.isFunction(e.stop)&&(i._queueHooks(u.elem,u.opts.queue).stop=i.proxy(e.stop,e)),e;return i.map(h,wu,u),i.isFunction(u.opts.start)&&u.opts.start.call(n,u),i.fx.timer(i.extend(c,{elem:n,anim:u,queue:u.opts.queue})),u.progress(u.opts.progress).done(u.opts.done,u.opts.complete).fail(u.opts.fail).always(u.opts.always)}function k(n){return n.getAttribute&&n.getAttribute("class")||""}function ff(n){return function(t,r){"string"!=typeof t&&(r=t,t="*");var u,f=0,e=t.toLowerCase().match(h)||[];if(i.isFunction(r))while(u=e[f++])"+"===u[0]?(u=u.slice(1)||"*",(n[u]=n[u]||[]).unshift(r)):(n[u]=n[u]||[]).push(r)}}function ef(n,t,r,u){function e(s){var h;return f[s]=!0,i.each(n[s]||[],function(n,i){var s=i(t,r,u);return"string"!=typeof s||o||f[s]?o?!(h=s):void 0:(t.dataTypes.unshift(s),e(s),!1)}),h}var f={},o=n===yi;return e(t.dataTypes[0])||!f["*"]&&e("*")}function wi(n,t){var r,u,f=i.ajaxSettings.flatOptions||{};for(r in t)void 0!==t[r]&&((f[r]?n:u||(u={}))[r]=t[r]);return u&&i.extend(!0,n,u),n}function be(n,t,i){for(var e,u,f,o,s=n.contents,r=n.dataTypes;"*"===r[0];)r.shift(),void 0===e&&(e=n.mimeType||t.getResponseHeader("Content-Type"));if(e)for(u in s)if(s[u]&&s[u].test(e)){r.unshift(u);break}if(r[0]in i)f=r[0];else{for(u in i){if(!r[0]||n.converters[u+" "+r[0]]){f=u;break}o||(o=u)}f=f||o}if(f)return(f!==r[0]&&r.unshift(f),i[f])}function ke(n,t,i,r){var h,u,f,s,e,o={},c=n.dataTypes.slice();if(c[1])for(f in n.converters)o[f.toLowerCase()]=n.converters[f];for(u=c.shift();u;)if(n.responseFields[u]&&(i[n.responseFields[u]]=t),!e&&r&&n.dataFilter&&(t=n.dataFilter(t,n.dataType)),e=u,u=c.shift())if("*"===u)u=e;else if("*"!==e&&e!==u){if(f=o[e+" "+u]||o["* "+u],!f)for(h in o)if(s=h.split(" "),s[1]===u&&(f=o[e+" "+s[0]]||o["* "+s[0]])){f===!0?f=o[h]:o[h]!==!0&&(u=s[0],c.unshift(s[1]));break}if(f!==!0)if(f&&n.throws)t=f(t);else try{t=f(t)}catch(l){return{state:"parsererror",error:f?l:"No conversion from "+e+" to "+u}}}return{state:"success",data:t}}function bi(n,t,r,u){var f;if(i.isArray(t))i.each(t,function(t,i){r||ge.test(n)?u(n,i):bi(n+"["+("object"==typeof i&&null!=i?t:"")+"]",i,r,u)});else if(r||"object"!==i.type(t))u(n,t);else for(f in t)bi(n+"["+f+"]",t[f],r,u)}function hf(n){return i.isWindow(n)?n:9===n.nodeType&&n.defaultView}var y=[],u=n.document,v=y.slice,gi=y.concat,ti=y.push,lt=y.indexOf,at={},af=at.toString,ft=at.hasOwnProperty,f={},nr="2.2.3",i=function(n,t){return new i.fn.init(n,t)},vf=/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g,yf=/^-ms-/,pf=/-([\da-z])/gi,wf=function(n,t){return t.toUpperCase()},p,ur,fr,er,or,sr,h,vt,a,g,br,wt,ei,it,kt,vu,yu,bu,rt,ku,du,gt,gu,nf,li,sf,ut,ki,ni,di,cf,lf;i.fn=i.prototype={jquery:nr,constructor:i,selector:"",length:0,toArray:function(){return v.call(this)},get:function(n){return null!=n?0>n?this[n+this.length]:this[n]:v.call(this)},pushStack:function(n){var t=i.merge(this.constructor(),n);return t.prevObject=this,t.context=this.context,t},each:function(n){return i.each(this,n)},map:function(n){return this.pushStack(i.map(this,function(t,i){return n.call(t,i,t)}))},slice:function(){return this.pushStack(v.apply(this,arguments))},first:function(){return this.eq(0)},last:function(){return this.eq(-1)},eq:function(n){var i=this.length,t=+n+(0>n?i:0);return this.pushStack(t>=0&&i>t?[this[t]]:[])},end:function(){return this.prevObject||this.constructor()},push:ti,sort:y.sort,splice:y.splice};i.extend=i.fn.extend=function(){var e,f,r,t,o,s,n=arguments[0]||{},u=1,c=arguments.length,h=!1;for("boolean"==typeof n&&(h=n,n=arguments[u]||{},u++),"object"==typeof n||i.isFunction(n)||(n={}),u===c&&(n=this,u--);c>u;u++)if(null!=(e=arguments[u]))for(f in e)r=n[f],t=e[f],n!==t&&(h&&t&&(i.isPlainObject(t)||(o=i.isArray(t)))?(o?(o=!1,s=r&&i.isArray(r)?r:[]):s=r&&i.isPlainObject(r)?r:{},n[f]=i.extend(h,s,t)):void 0!==t&&(n[f]=t));return n};i.extend({expando:"jQuery"+(nr+Math.random()).replace(/\D/g,""),isReady:!0,error:function(n){throw new Error(n);},noop:function(){},isFunction:function(n){return"function"===i.type(n)},isArray:Array.isArray,isWindow:function(n){return null!=n&&n===n.window},isNumeric:function(n){var t=n&&n.toString();return!i.isArray(n)&&t-parseFloat(t)+1>=0},isPlainObject:function(n){var t;if("object"!==i.type(n)||n.nodeType||i.isWindow(n)||n.constructor&&!ft.call(n,"constructor")&&!ft.call(n.constructor.prototype||{},"isPrototypeOf"))return!1;for(t in n);return void 0===t||ft.call(n,t)},isEmptyObject:function(n){for(var t in n)return!1;return!0},type:function(n){return null==n?n+"":"object"==typeof n||"function"==typeof n?at[af.call(n)]||"object":typeof n},globalEval:function(n){var t,r=eval;n=i.trim(n);n&&(1===n.indexOf("use strict")?(t=u.createElement("script"),t.text=n,u.head.appendChild(t).parentNode.removeChild(t)):r(n))},camelCase:function(n){return n.replace(yf,"ms-").replace(pf,wf)},nodeName:function(n,t){return n.nodeName&&n.nodeName.toLowerCase()===t.toLowerCase()},each:function(n,t){var r,i=0;if(ii(n)){for(r=n.length;r>i;i++)if(t.call(n[i],i,n[i])===!1)break}else for(i in n)if(t.call(n[i],i,n[i])===!1)break;return n},trim:function(n){return null==n?"":(n+"").replace(vf,"")},makeArray:function(n,t){var r=t||[];return null!=n&&(ii(Object(n))?i.merge(r,"string"==typeof n?[n]:n):ti.call(r,n)),r},inArray:function(n,t,i){return null==t?-1:lt.call(t,n,i)},merge:function(n,t){for(var u=+t.length,i=0,r=n.length;u>i;i++)n[r++]=t[i];return n.length=r,n},grep:function(n,t,i){for(var u,f=[],r=0,e=n.length,o=!i;e>r;r++)u=!t(n[r],r),u!==o&&f.push(n[r]);return f},map:function(n,t,i){var e,u,r=0,f=[];if(ii(n))for(e=n.length;e>r;r++)u=t(n[r],r,i),null!=u&&f.push(u);else for(r in n)u=t(n[r],r,i),null!=u&&f.push(u);return gi.apply([],f)},guid:1,proxy:function(n,t){var u,f,r;return"string"==typeof t&&(u=n[t],t=n,n=u),i.isFunction(n)?(f=v.call(arguments,2),r=function(){return n.apply(t||this,f.concat(v.call(arguments)))},r.guid=n.guid=n.guid||i.guid++,r):void 0},now:Date.now,support:f});"function"==typeof Symbol&&(i.fn[Symbol.iterator]=y[Symbol.iterator]);i.each("Boolean Number String Function Array Date RegExp Object Error Symbol".split(" "),function(n,t){at["[object "+t+"]"]=t.toLowerCase()});p=function(n){function u(n,t,r,u){var l,w,a,s,nt,d,y,g,p=t&&t.ownerDocument,v=t?t.nodeType:9;if(r=r||[],"string"!=typeof n||!n||1!==v&&9!==v&&11!==v)return r;if(!u&&((t?t.ownerDocument||t:c)!==i&&b(t),t=t||i,h)){if(11!==v&&(d=sr.exec(n)))if(l=d[1]){if(9===v){if(!(a=t.getElementById(l)))return r;if(a.id===l)return r.push(a),r}else if(p&&(a=p.getElementById(l))&&et(t,a)&&a.id===l)return r.push(a),r}else{if(d[2])return k.apply(r,t.getElementsByTagName(n)),r;if((l=d[3])&&f.getElementsByClassName&&t.getElementsByClassName)return k.apply(r,t.getElementsByClassName(l)),r}if(f.qsa&&!lt[n+" "]&&(!o||!o.test(n))){if(1!==v)p=t,g=n;else if("object"!==t.nodeName.toLowerCase()){for((s=t.getAttribute("id"))?s=s.replace(hr,"\\$&"):t.setAttribute("id",s=e),y=ft(n),w=y.length,nt=yi.test(s)?"#"+s:"[id='"+s+"']";w--;)y[w]=nt+" "+yt(y[w]);g=y.join(",");p=gt.test(n)&&ii(t.parentNode)||t}if(g)try{return k.apply(r,p.querySelectorAll(g)),r}catch(tt){}finally{s===e&&t.removeAttribute("id")}}}return si(n.replace(at,"$1"),t,r,u)}function ni(){function n(r,u){return i.push(r+" ")>t.cacheLength&&delete n[i.shift()],n[r+" "]=u}var i=[];return n}function l(n){return n[e]=!0,n}function a(n){var t=i.createElement("div");try{return!!n(t)}catch(r){return!1}finally{t.parentNode&&t.parentNode.removeChild(t);t=null}}function ti(n,i){for(var r=n.split("|"),u=r.length;u--;)t.attrHandle[r[u]]=i}function wi(n,t){var i=t&&n,r=i&&1===n.nodeType&&1===t.nodeType&&(~t.sourceIndex||li)-(~n.sourceIndex||li);if(r)return r;if(i)while(i=i.nextSibling)if(i===t)return-1;return n?1:-1}function cr(n){return function(t){var i=t.nodeName.toLowerCase();return"input"===i&&t.type===n}}function lr(n){return function(t){var i=t.nodeName.toLowerCase();return("input"===i||"button"===i)&&t.type===n}}function it(n){return l(function(t){return t=+t,l(function(i,r){for(var u,f=n([],i.length,t),e=f.length;e--;)i[u=f[e]]&&(i[u]=!(r[u]=i[u]))})})}function ii(n){return n&&"undefined"!=typeof n.getElementsByTagName&&n}function bi(){}function yt(n){for(var t=0,r=n.length,i="";r>t;t++)i+=n[t].value;return i}function ri(n,t,i){var r=t.dir,u=i&&"parentNode"===r,f=ki++;return t.first?function(t,i,f){while(t=t[r])if(1===t.nodeType||u)return n(t,i,f)}:function(t,i,o){var s,h,c,l=[v,f];if(o){while(t=t[r])if((1===t.nodeType||u)&&n(t,i,o))return!0}else while(t=t[r])if(1===t.nodeType||u){if(c=t[e]||(t[e]={}),h=c[t.uniqueID]||(c[t.uniqueID]={}),(s=h[r])&&s[0]===v&&s[1]===f)return l[2]=s[2];if(h[r]=l,l[2]=n(t,i,o))return!0}}}function ui(n){return n.length>1?function(t,i,r){for(var u=n.length;u--;)if(!n[u](t,i,r))return!1;return!0}:n[0]}function ar(n,t,i){for(var r=0,f=t.length;f>r;r++)u(n,t[r],i);return i}function pt(n,t,i,r,u){for(var e,o=[],f=0,s=n.length,h=null!=t;s>f;f++)(e=n[f])&&(i&&!i(e,r,u)||(o.push(e),h&&t.push(f)));return o}function fi(n,t,i,r,u,f){return r&&!r[e]&&(r=fi(r)),u&&!u[e]&&(u=fi(u,f)),l(function(f,e,o,s){var l,c,a,p=[],y=[],w=e.length,b=f||ar(t||"*",o.nodeType?[o]:o,[]),v=!n||!f&&t?b:pt(b,p,n,o,s),h=i?u||(f?n:w||r)?[]:e:v;if(i&&i(v,h,o,s),r)for(l=pt(h,y),r(l,[],o,s),c=l.length;c--;)(a=l[c])&&(h[y[c]]=!(v[y[c]]=a));if(f){if(u||n){if(u){for(l=[],c=h.length;c--;)(a=h[c])&&l.push(v[c]=a);u(null,h=[],l,s)}for(c=h.length;c--;)(a=h[c])&&(l=u?nt(f,a):p[c])>-1&&(f[l]=!(e[l]=a))}}else h=pt(h===e?h.splice(w,h.length):h),u?u(null,e,h,s):k.apply(e,h)})}function ei(n){for(var o,u,r,s=n.length,h=t.relative[n[0].type],c=h||t.relative[" "],i=h?1:0,l=ri(function(n){return n===o},c,!0),a=ri(function(n){return nt(o,n)>-1},c,!0),f=[function(n,t,i){var r=!h&&(i||t!==ht)||((o=t).nodeType?l(n,t,i):a(n,t,i));return o=null,r}];s>i;i++)if(u=t.relative[n[i].type])f=[ri(ui(f),u)];else{if(u=t.filter[n[i].type].apply(null,n[i].matches),u[e]){for(r=++i;s>r;r++)if(t.relative[n[r].type])break;return fi(i>1&&ui(f),i>1&&yt(n.slice(0,i-1).concat({value:" "===n[i-2].type?"*":""})).replace(at,"$1"),u,r>i&&ei(n.slice(i,r)),s>r&&ei(n=n.slice(r)),s>r&&yt(n))}f.push(u)}return ui(f)}function vr(n,r){var f=r.length>0,e=n.length>0,o=function(o,s,c,l,a){var y,nt,d,g=0,p="0",tt=o&&[],w=[],it=ht,rt=o||e&&t.find.TAG("*",a),ut=v+=null==it?1:Math.random()||.1,ft=rt.length;for(a&&(ht=s===i||s||a);p!==ft&&null!=(y=rt[p]);p++){if(e&&y){for(nt=0,s||y.ownerDocument===i||(b(y),c=!h);d=n[nt++];)if(d(y,s||i,c)){l.push(y);break}a&&(v=ut)}f&&((y=!d&&y)&&g--,o&&tt.push(y))}if(g+=p,f&&p!==g){for(nt=0;d=r[nt++];)d(tt,w,s,c);if(o){if(g>0)while(p--)tt[p]||w[p]||(w[p]=gi.call(l));w=pt(w)}k.apply(l,w);a&&!o&&w.length>0&&g+r.length>1&&u.uniqueSort(l)}return a&&(v=ut,ht=it),tt};return f?l(o):o}var rt,f,t,st,oi,ft,wt,si,ht,w,ut,b,i,s,h,o,d,ct,et,e="sizzle"+1*new Date,c=n.document,v=0,ki=0,hi=ni(),ci=ni(),lt=ni(),bt=function(n,t){return n===t&&(ut=!0),0},li=-2147483648,di={}.hasOwnProperty,g=[],gi=g.pop,nr=g.push,k=g.push,ai=g.slice,nt=function(n,t){for(var i=0,r=n.length;r>i;i++)if(n[i]===t)return i;return-1},kt="checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped",r="[\\x20\\t\\r\\n\\f]",tt="(?:\\\\.|[\\w-]|[^\\x00-\\xa0])+",vi="\\["+r+"*("+tt+")(?:"+r+"*([*^$|!~]?=)"+r+"*(?:'((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\"|("+tt+"))|)"+r+"*\\]",dt=":("+tt+")(?:\\((('((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\")|((?:\\\\.|[^\\\\()[\\]]|"+vi+")*)|.*)\\)|)",tr=new RegExp(r+"+","g"),at=new RegExp("^"+r+"+|((?:^|[^\\\\])(?:\\\\.)*)"+r+"+$","g"),ir=new RegExp("^"+r+"*,"+r+"*"),rr=new RegExp("^"+r+"*([>+~]|"+r+")"+r+"*"),ur=new RegExp("="+r+"*([^\\]'\"]*?)"+r+"*\\]","g"),fr=new RegExp(dt),yi=new RegExp("^"+tt+"$"),vt={ID:new RegExp("^#("+tt+")"),CLASS:new RegExp("^\\.("+tt+")"),TAG:new RegExp("^("+tt+"|[*])"),ATTR:new RegExp("^"+vi),PSEUDO:new RegExp("^"+dt),CHILD:new RegExp("^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\("+r+"*(even|odd|(([+-]|)(\\d*)n|)"+r+"*(?:([+-]|)"+r+"*(\\d+)|))"+r+"*\\)|)","i"),bool:new RegExp("^(?:"+kt+")$","i"),needsContext:new RegExp("^"+r+"*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\("+r+"*((?:-\\d)?\\d*)"+r+"*\\)|)(?=[^-]|$)","i")},er=/^(?:input|select|textarea|button)$/i,or=/^h\d$/i,ot=/^[^{]+\{\s*\[native \w/,sr=/^(?:#([\w-]+)|(\w+)|\.([\w-]+))$/,gt=/[+~]/,hr=/'|\\/g,y=new RegExp("\\\\([\\da-f]{1,6}"+r+"?|("+r+")|.)","ig"),p=function(n,t,i){var r="0x"+t-65536;return r!==r||i?t:0>r?String.fromCharCode(r+65536):String.fromCharCode(r>>10|55296,1023&r|56320)},pi=function(){b()};try{k.apply(g=ai.call(c.childNodes),c.childNodes);g[c.childNodes.length].nodeType}catch(yr){k={apply:g.length?function(n,t){nr.apply(n,ai.call(t))}:function(n,t){for(var i=n.length,r=0;n[i++]=t[r++];);n.length=i-1}}}f=u.support={};oi=u.isXML=function(n){var t=n&&(n.ownerDocument||n).documentElement;return t?"HTML"!==t.nodeName:!1};b=u.setDocument=function(n){var v,u,l=n?n.ownerDocument||n:c;return l!==i&&9===l.nodeType&&l.documentElement?(i=l,s=i.documentElement,h=!oi(i),(u=i.defaultView)&&u.top!==u&&(u.addEventListener?u.addEventListener("unload",pi,!1):u.attachEvent&&u.attachEvent("onunload",pi)),f.attributes=a(function(n){return n.className="i",!n.getAttribute("className")}),f.getElementsByTagName=a(function(n){return n.appendChild(i.createComment("")),!n.getElementsByTagName("*").length}),f.getElementsByClassName=ot.test(i.getElementsByClassName),f.getById=a(function(n){return s.appendChild(n).id=e,!i.getElementsByName||!i.getElementsByName(e).length}),f.getById?(t.find.ID=function(n,t){if("undefined"!=typeof t.getElementById&&h){var i=t.getElementById(n);return i?[i]:[]}},t.filter.ID=function(n){var t=n.replace(y,p);return function(n){return n.getAttribute("id")===t}}):(delete t.find.ID,t.filter.ID=function(n){var t=n.replace(y,p);return function(n){var i="undefined"!=typeof n.getAttributeNode&&n.getAttributeNode("id");return i&&i.value===t}}),t.find.TAG=f.getElementsByTagName?function(n,t){return"undefined"!=typeof t.getElementsByTagName?t.getElementsByTagName(n):f.qsa?t.querySelectorAll(n):void 0}:function(n,t){var i,r=[],f=0,u=t.getElementsByTagName(n);if("*"===n){while(i=u[f++])1===i.nodeType&&r.push(i);return r}return u},t.find.CLASS=f.getElementsByClassName&&function(n,t){if("undefined"!=typeof t.getElementsByClassName&&h)return t.getElementsByClassName(n)},d=[],o=[],(f.qsa=ot.test(i.querySelectorAll))&&(a(function(n){s.appendChild(n).innerHTML="<a id='"+e+"'><\/a><select id='"+e+"-\r\\' msallowcapture=''><option selected=''><\/option><\/select>";n.querySelectorAll("[msallowcapture^='']").length&&o.push("[*^$]="+r+"*(?:''|\"\")");n.querySelectorAll("[selected]").length||o.push("\\["+r+"*(?:value|"+kt+")");n.querySelectorAll("[id~="+e+"-]").length||o.push("~=");n.querySelectorAll(":checked").length||o.push(":checked");n.querySelectorAll("a#"+e+"+*").length||o.push(".#.+[+~]")}),a(function(n){var t=i.createElement("input");t.setAttribute("type","hidden");n.appendChild(t).setAttribute("name","D");n.querySelectorAll("[name=d]").length&&o.push("name"+r+"*[*^$|!~]?=");n.querySelectorAll(":enabled").length||o.push(":enabled",":disabled");n.querySelectorAll("*,:x");o.push(",.*:")})),(f.matchesSelector=ot.test(ct=s.matches||s.webkitMatchesSelector||s.mozMatchesSelector||s.oMatchesSelector||s.msMatchesSelector))&&a(function(n){f.disconnectedMatch=ct.call(n,"div");ct.call(n,"[s!='']:x");d.push("!=",dt)}),o=o.length&&new RegExp(o.join("|")),d=d.length&&new RegExp(d.join("|")),v=ot.test(s.compareDocumentPosition),et=v||ot.test(s.contains)?function(n,t){var r=9===n.nodeType?n.documentElement:n,i=t&&t.parentNode;return n===i||!(!i||1!==i.nodeType||!(r.contains?r.contains(i):n.compareDocumentPosition&&16&n.compareDocumentPosition(i)))}:function(n,t){if(t)while(t=t.parentNode)if(t===n)return!0;return!1},bt=v?function(n,t){if(n===t)return ut=!0,0;var r=!n.compareDocumentPosition-!t.compareDocumentPosition;return r?r:(r=(n.ownerDocument||n)===(t.ownerDocument||t)?n.compareDocumentPosition(t):1,1&r||!f.sortDetached&&t.compareDocumentPosition(n)===r?n===i||n.ownerDocument===c&&et(c,n)?-1:t===i||t.ownerDocument===c&&et(c,t)?1:w?nt(w,n)-nt(w,t):0:4&r?-1:1)}:function(n,t){if(n===t)return ut=!0,0;var r,u=0,o=n.parentNode,s=t.parentNode,f=[n],e=[t];if(!o||!s)return n===i?-1:t===i?1:o?-1:s?1:w?nt(w,n)-nt(w,t):0;if(o===s)return wi(n,t);for(r=n;r=r.parentNode;)f.unshift(r);for(r=t;r=r.parentNode;)e.unshift(r);while(f[u]===e[u])u++;return u?wi(f[u],e[u]):f[u]===c?-1:e[u]===c?1:0},i):i};u.matches=function(n,t){return u(n,null,null,t)};u.matchesSelector=function(n,t){if((n.ownerDocument||n)!==i&&b(n),t=t.replace(ur,"='$1']"),f.matchesSelector&&h&&!lt[t+" "]&&(!d||!d.test(t))&&(!o||!o.test(t)))try{var r=ct.call(n,t);if(r||f.disconnectedMatch||n.document&&11!==n.document.nodeType)return r}catch(e){}return u(t,i,null,[n]).length>0};u.contains=function(n,t){return(n.ownerDocument||n)!==i&&b(n),et(n,t)};u.attr=function(n,r){(n.ownerDocument||n)!==i&&b(n);var e=t.attrHandle[r.toLowerCase()],u=e&&di.call(t.attrHandle,r.toLowerCase())?e(n,r,!h):void 0;return void 0!==u?u:f.attributes||!h?n.getAttribute(r):(u=n.getAttributeNode(r))&&u.specified?u.value:null};u.error=function(n){throw new Error("Syntax error, unrecognized expression: "+n);};u.uniqueSort=function(n){var r,u=[],t=0,i=0;if(ut=!f.detectDuplicates,w=!f.sortStable&&n.slice(0),n.sort(bt),ut){while(r=n[i++])r===n[i]&&(t=u.push(i));while(t--)n.splice(u[t],1)}return w=null,n};st=u.getText=function(n){var r,i="",u=0,t=n.nodeType;if(t){if(1===t||9===t||11===t){if("string"==typeof n.textContent)return n.textContent;for(n=n.firstChild;n;n=n.nextSibling)i+=st(n)}else if(3===t||4===t)return n.nodeValue}else while(r=n[u++])i+=st(r);return i};t=u.selectors={cacheLength:50,createPseudo:l,match:vt,attrHandle:{},find:{},relative:{">":{dir:"parentNode",first:!0}," ":{dir:"parentNode"},"+":{dir:"previousSibling",first:!0},"~":{dir:"previousSibling"}},preFilter:{ATTR:function(n){return n[1]=n[1].replace(y,p),n[3]=(n[3]||n[4]||n[5]||"").replace(y,p),"~="===n[2]&&(n[3]=" "+n[3]+" "),n.slice(0,4)},CHILD:function(n){return n[1]=n[1].toLowerCase(),"nth"===n[1].slice(0,3)?(n[3]||u.error(n[0]),n[4]=+(n[4]?n[5]+(n[6]||1):2*("even"===n[3]||"odd"===n[3])),n[5]=+(n[7]+n[8]||"odd"===n[3])):n[3]&&u.error(n[0]),n},PSEUDO:function(n){var i,t=!n[6]&&n[2];return vt.CHILD.test(n[0])?null:(n[3]?n[2]=n[4]||n[5]||"":t&&fr.test(t)&&(i=ft(t,!0))&&(i=t.indexOf(")",t.length-i)-t.length)&&(n[0]=n[0].slice(0,i),n[2]=t.slice(0,i)),n.slice(0,3))}},filter:{TAG:function(n){var t=n.replace(y,p).toLowerCase();return"*"===n?function(){return!0}:function(n){return n.nodeName&&n.nodeName.toLowerCase()===t}},CLASS:function(n){var t=hi[n+" "];return t||(t=new RegExp("(^|"+r+")"+n+"("+r+"|$)"))&&hi(n,function(n){return t.test("string"==typeof n.className&&n.className||"undefined"!=typeof n.getAttribute&&n.getAttribute("class")||"")})},ATTR:function(n,t,i){return function(r){var f=u.attr(r,n);return null==f?"!="===t:t?(f+="","="===t?f===i:"!="===t?f!==i:"^="===t?i&&0===f.indexOf(i):"*="===t?i&&f.indexOf(i)>-1:"$="===t?i&&f.slice(-i.length)===i:"~="===t?(" "+f.replace(tr," ")+" ").indexOf(i)>-1:"|="===t?f===i||f.slice(0,i.length+1)===i+"-":!1):!0}},CHILD:function(n,t,i,r,u){var s="nth"!==n.slice(0,3),o="last"!==n.slice(-4),f="of-type"===t;return 1===r&&0===u?function(n){return!!n.parentNode}:function(t,i,h){var p,w,y,c,a,b,k=s!==o?"nextSibling":"previousSibling",d=t.parentNode,nt=f&&t.nodeName.toLowerCase(),g=!h&&!f,l=!1;if(d){if(s){while(k){for(c=t;c=c[k];)if(f?c.nodeName.toLowerCase()===nt:1===c.nodeType)return!1;b=k="only"===n&&!b&&"nextSibling"}return!0}if(b=[o?d.firstChild:d.lastChild],o&&g){for(c=d,y=c[e]||(c[e]={}),w=y[c.uniqueID]||(y[c.uniqueID]={}),p=w[n]||[],a=p[0]===v&&p[1],l=a&&p[2],c=a&&d.childNodes[a];c=++a&&c&&c[k]||(l=a=0)||b.pop();)if(1===c.nodeType&&++l&&c===t){w[n]=[v,a,l];break}}else if(g&&(c=t,y=c[e]||(c[e]={}),w=y[c.uniqueID]||(y[c.uniqueID]={}),p=w[n]||[],a=p[0]===v&&p[1],l=a),l===!1)while(c=++a&&c&&c[k]||(l=a=0)||b.pop())if((f?c.nodeName.toLowerCase()===nt:1===c.nodeType)&&++l&&(g&&(y=c[e]||(c[e]={}),w=y[c.uniqueID]||(y[c.uniqueID]={}),w[n]=[v,l]),c===t))break;return l-=u,l===r||l%r==0&&l/r>=0}}},PSEUDO:function(n,i){var f,r=t.pseudos[n]||t.setFilters[n.toLowerCase()]||u.error("unsupported pseudo: "+n);return r[e]?r(i):r.length>1?(f=[n,n,"",i],t.setFilters.hasOwnProperty(n.toLowerCase())?l(function(n,t){for(var u,f=r(n,i),e=f.length;e--;)u=nt(n,f[e]),n[u]=!(t[u]=f[e])}):function(n){return r(n,0,f)}):r}},pseudos:{not:l(function(n){var t=[],r=[],i=wt(n.replace(at,"$1"));return i[e]?l(function(n,t,r,u){for(var e,o=i(n,null,u,[]),f=n.length;f--;)(e=o[f])&&(n[f]=!(t[f]=e))}):function(n,u,f){return t[0]=n,i(t,null,f,r),t[0]=null,!r.pop()}}),has:l(function(n){return function(t){return u(n,t).length>0}}),contains:l(function(n){return n=n.replace(y,p),function(t){return(t.textContent||t.innerText||st(t)).indexOf(n)>-1}}),lang:l(function(n){return yi.test(n||"")||u.error("unsupported lang: "+n),n=n.replace(y,p).toLowerCase(),function(t){var i;do if(i=h?t.lang:t.getAttribute("xml:lang")||t.getAttribute("lang"))return i=i.toLowerCase(),i===n||0===i.indexOf(n+"-");while((t=t.parentNode)&&1===t.nodeType);return!1}}),target:function(t){var i=n.location&&n.location.hash;return i&&i.slice(1)===t.id},root:function(n){return n===s},focus:function(n){return n===i.activeElement&&(!i.hasFocus||i.hasFocus())&&!!(n.type||n.href||~n.tabIndex)},enabled:function(n){return n.disabled===!1},disabled:function(n){return n.disabled===!0},checked:function(n){var t=n.nodeName.toLowerCase();return"input"===t&&!!n.checked||"option"===t&&!!n.selected},selected:function(n){return n.parentNode&&n.parentNode.selectedIndex,n.selected===!0},empty:function(n){for(n=n.firstChild;n;n=n.nextSibling)if(n.nodeType<6)return!1;return!0},parent:function(n){return!t.pseudos.empty(n)},header:function(n){return or.test(n.nodeName)},input:function(n){return er.test(n.nodeName)},button:function(n){var t=n.nodeName.toLowerCase();return"input"===t&&"button"===n.type||"button"===t},text:function(n){var t;return"input"===n.nodeName.toLowerCase()&&"text"===n.type&&(null==(t=n.getAttribute("type"))||"text"===t.toLowerCase())},first:it(function(){return[0]}),last:it(function(n,t){return[t-1]}),eq:it(function(n,t,i){return[0>i?i+t:i]}),even:it(function(n,t){for(var i=0;t>i;i+=2)n.push(i);return n}),odd:it(function(n,t){for(var i=1;t>i;i+=2)n.push(i);return n}),lt:it(function(n,t,i){for(var r=0>i?i+t:i;--r>=0;)n.push(r);return n}),gt:it(function(n,t,i){for(var r=0>i?i+t:i;++r<t;)n.push(r);return n})}};t.pseudos.nth=t.pseudos.eq;for(rt in{radio:!0,checkbox:!0,file:!0,password:!0,image:!0})t.pseudos[rt]=cr(rt);for(rt in{submit:!0,reset:!0})t.pseudos[rt]=lr(rt);return bi.prototype=t.filters=t.pseudos,t.setFilters=new bi,ft=u.tokenize=function(n,i){var e,f,s,o,r,h,c,l=ci[n+" "];if(l)return i?0:l.slice(0);for(r=n,h=[],c=t.preFilter;r;){(!e||(f=ir.exec(r)))&&(f&&(r=r.slice(f[0].length)||r),h.push(s=[]));e=!1;(f=rr.exec(r))&&(e=f.shift(),s.push({value:e,type:f[0].replace(at," ")}),r=r.slice(e.length));for(o in t.filter)(f=vt[o].exec(r))&&(!c[o]||(f=c[o](f)))&&(e=f.shift(),s.push({value:e,type:o,matches:f}),r=r.slice(e.length));if(!e)break}return i?r.length:r?u.error(n):ci(n,h).slice(0)},wt=u.compile=function(n,t){var r,u=[],f=[],i=lt[n+" "];if(!i){for(t||(t=ft(n)),r=t.length;r--;)i=ei(t[r]),i[e]?u.push(i):f.push(i);i=lt(n,vr(f,u));i.selector=n}return i},si=u.select=function(n,i,r,u){var s,e,o,a,v,l="function"==typeof n&&n,c=!u&&ft(n=l.selector||n);if(r=r||[],1===c.length){if(e=c[0]=c[0].slice(0),e.length>2&&"ID"===(o=e[0]).type&&f.getById&&9===i.nodeType&&h&&t.relative[e[1].type]){if(i=(t.find.ID(o.matches[0].replace(y,p),i)||[])[0],!i)return r;l&&(i=i.parentNode);n=n.slice(e.shift().value.length)}for(s=vt.needsContext.test(n)?0:e.length;s--;){if(o=e[s],t.relative[a=o.type])break;if((v=t.find[a])&&(u=v(o.matches[0].replace(y,p),gt.test(e[0].type)&&ii(i.parentNode)||i))){if(e.splice(s,1),n=u.length&&yt(e),!n)return k.apply(r,u),r;break}}}return(l||wt(n,c))(u,i,!h,r,!i||gt.test(n)&&ii(i.parentNode)||i),r},f.sortStable=e.split("").sort(bt).join("")===e,f.detectDuplicates=!!ut,b(),f.sortDetached=a(function(n){return 1&n.compareDocumentPosition(i.createElement("div"))}),a(function(n){return n.innerHTML="<a href='#'><\/a>","#"===n.firstChild.getAttribute("href")})||ti("type|href|height|width",function(n,t,i){if(!i)return n.getAttribute(t,"type"===t.toLowerCase()?1:2)}),f.attributes&&a(function(n){return n.innerHTML="<input/>",n.firstChild.setAttribute("value",""),""===n.firstChild.getAttribute("value")})||ti("value",function(n,t,i){if(!i&&"input"===n.nodeName.toLowerCase())return n.defaultValue}),a(function(n){return null==n.getAttribute("disabled")})||ti(kt,function(n,t,i){var r;if(!i)return n[t]===!0?t.toLowerCase():(r=n.getAttributeNode(t))&&r.specified?r.value:null}),u}(n);i.find=p;i.expr=p.selectors;i.expr[":"]=i.expr.pseudos;i.uniqueSort=i.unique=p.uniqueSort;i.text=p.getText;i.isXMLDoc=p.isXML;i.contains=p.contains;var d=function(n,t,r){for(var u=[],f=void 0!==r;(n=n[t])&&9!==n.nodeType;)if(1===n.nodeType){if(f&&i(n).is(r))break;u.push(n)}return u},tr=function(n,t){for(var i=[];n;n=n.nextSibling)1===n.nodeType&&n!==t&&i.push(n);return i},ir=i.expr.match.needsContext,rr=/^<([\w-]+)\s*\/?>(?:<\/\1>|)$/,bf=/^.[^:#\[\.,]*$/;i.filter=function(n,t,r){var u=t[0];return r&&(n=":not("+n+")"),1===t.length&&1===u.nodeType?i.find.matchesSelector(u,n)?[u]:[]:i.find.matches(n,i.grep(t,function(n){return 1===n.nodeType}))};i.fn.extend({find:function(n){var t,u=this.length,r=[],f=this;if("string"!=typeof n)return this.pushStack(i(n).filter(function(){for(t=0;u>t;t++)if(i.contains(f[t],this))return!0}));for(t=0;u>t;t++)i.find(n,f[t],r);return r=this.pushStack(u>1?i.unique(r):r),r.selector=this.selector?this.selector+" "+n:n,r},filter:function(n){return this.pushStack(ri(this,n||[],!1))},not:function(n){return this.pushStack(ri(this,n||[],!0))},is:function(n){return!!ri(this,"string"==typeof n&&ir.test(n)?i(n):n||[],!1).length}});fr=/^(?:\s*(<[\w\W]+>)[^>]*|#([\w-]*))$/;er=i.fn.init=function(n,t,r){var f,e;if(!n)return this;if(r=r||ur,"string"==typeof n){if(f="<"===n[0]&&">"===n[n.length-1]&&n.length>=3?[null,n,null]:fr.exec(n),!f||!f[1]&&t)return!t||t.jquery?(t||r).find(n):this.constructor(t).find(n);if(f[1]){if(t=t instanceof i?t[0]:t,i.merge(this,i.parseHTML(f[1],t&&t.nodeType?t.ownerDocument||t:u,!0)),rr.test(f[1])&&i.isPlainObject(t))for(f in t)i.isFunction(this[f])?this[f](t[f]):this.attr(f,t[f]);return this}return e=u.getElementById(f[2]),e&&e.parentNode&&(this.length=1,this[0]=e),this.context=u,this.selector=n,this}return n.nodeType?(this.context=this[0]=n,this.length=1,this):i.isFunction(n)?void 0!==r.ready?r.ready(n):n(i):(void 0!==n.selector&&(this.selector=n.selector,this.context=n.context),i.makeArray(n,this))};er.prototype=i.fn;ur=i(u);or=/^(?:parents|prev(?:Until|All))/;sr={children:!0,contents:!0,next:!0,prev:!0};i.fn.extend({has:function(n){var t=i(n,this),r=t.length;return this.filter(function(){for(var n=0;r>n;n++)if(i.contains(this,t[n]))return!0})},closest:function(n,t){for(var r,f=0,o=this.length,u=[],e=ir.test(n)||"string"!=typeof n?i(n,t||this.context):0;o>f;f++)for(r=this[f];r&&r!==t;r=r.parentNode)if(r.nodeType<11&&(e?e.index(r)>-1:1===r.nodeType&&i.find.matchesSelector(r,n))){u.push(r);break}return this.pushStack(u.length>1?i.uniqueSort(u):u)},index:function(n){return n?"string"==typeof n?lt.call(i(n),this[0]):lt.call(this,n.jquery?n[0]:n):this[0]&&this[0].parentNode?this.first().prevAll().length:-1},add:function(n,t){return this.pushStack(i.uniqueSort(i.merge(this.get(),i(n,t))))},addBack:function(n){return this.add(null==n?this.prevObject:this.prevObject.filter(n))}});i.each({parent:function(n){var t=n.parentNode;return t&&11!==t.nodeType?t:null},parents:function(n){return d(n,"parentNode")},parentsUntil:function(n,t,i){return d(n,"parentNode",i)},next:function(n){return hr(n,"nextSibling")},prev:function(n){return hr(n,"previousSibling")},nextAll:function(n){return d(n,"nextSibling")},prevAll:function(n){return d(n,"previousSibling")},nextUntil:function(n,t,i){return d(n,"nextSibling",i)},prevUntil:function(n,t,i){return d(n,"previousSibling",i)},siblings:function(n){return tr((n.parentNode||{}).firstChild,n)},children:function(n){return tr(n.firstChild)},contents:function(n){return n.contentDocument||i.merge([],n.childNodes)}},function(n,t){i.fn[n]=function(r,u){var f=i.map(this,t,r);return"Until"!==n.slice(-5)&&(u=r),u&&"string"==typeof u&&(f=i.filter(u,f)),this.length>1&&(sr[n]||i.uniqueSort(f),or.test(n)&&f.reverse()),this.pushStack(f)}});h=/\S+/g;i.Callbacks=function(n){n="string"==typeof n?kf(n):i.extend({},n);var o,r,h,f,t=[],e=[],u=-1,c=function(){for(f=n.once,h=o=!0;e.length;u=-1)for(r=e.shift();++u<t.length;)t[u].apply(r[0],r[1])===!1&&n.stopOnFalse&&(u=t.length,r=!1);n.memory||(r=!1);o=!1;f&&(t=r?[]:"")},s={add:function(){return t&&(r&&!o&&(u=t.length-1,e.push(r)),function f(r){i.each(r,function(r,u){i.isFunction(u)?n.unique&&s.has(u)||t.push(u):u&&u.length&&"string"!==i.type(u)&&f(u)})}(arguments),r&&!o&&c()),this},remove:function(){return i.each(arguments,function(n,r){for(var f;(f=i.inArray(r,t,f))>-1;)t.splice(f,1),u>=f&&u--}),this},has:function(n){return n?i.inArray(n,t)>-1:t.length>0},empty:function(){return t&&(t=[]),this},disable:function(){return f=e=[],t=r="",this},disabled:function(){return!t},lock:function(){return f=e=[],r||(t=r=""),this},locked:function(){return!!f},fireWith:function(n,t){return f||(t=t||[],t=[n,t.slice?t.slice():t],e.push(t),o||c()),this},fire:function(){return s.fireWith(this,arguments),this},fired:function(){return!!h}};return s};i.extend({Deferred:function(n){var u=[["resolve","done",i.Callbacks("once memory"),"resolved"],["reject","fail",i.Callbacks("once memory"),"rejected"],["notify","progress",i.Callbacks("memory")]],f="pending",r={state:function(){return f},always:function(){return t.done(arguments).fail(arguments),this},then:function(){var n=arguments;return i.Deferred(function(f){i.each(u,function(u,e){var o=i.isFunction(n[u])&&n[u];t[e[1]](function(){var n=o&&o.apply(this,arguments);n&&i.isFunction(n.promise)?n.promise().progress(f.notify).done(f.resolve).fail(f.reject):f[e[0]+"With"](this===r?f.promise():this,o?[n]:arguments)})});n=null}).promise()},promise:function(n){return null!=n?i.extend(n,r):r}},t={};return r.pipe=r.then,i.each(u,function(n,i){var e=i[2],o=i[3];r[i[1]]=e.add;o&&e.add(function(){f=o},u[1^n][2].disable,u[2][2].lock);t[i[0]]=function(){return t[i[0]+"With"](this===t?r:this,arguments),this};t[i[0]+"With"]=e.fireWith}),r.promise(t),n&&n.call(t,t),t},when:function(n){var t=0,u=v.call(arguments),r=u.length,e=1!==r||n&&i.isFunction(n.promise)?r:0,f=1===e?n:i.Deferred(),h=function(n,t,i){return function(r){t[n]=this;i[n]=arguments.length>1?v.call(arguments):r;i===o?f.notifyWith(t,i):--e||f.resolveWith(t,i)}},o,c,s;if(r>1)for(o=new Array(r),c=new Array(r),s=new Array(r);r>t;t++)u[t]&&i.isFunction(u[t].promise)?u[t].promise().progress(h(t,c,o)).done(h(t,s,u)).fail(f.reject):--e;return e||f.resolveWith(s,u),f.promise()}});i.fn.ready=function(n){return i.ready.promise().done(n),this};i.extend({isReady:!1,readyWait:1,holdReady:function(n){n?i.readyWait++:i.ready(!0)},ready:function(n){(n===!0?--i.readyWait:i.isReady)||(i.isReady=!0,n!==!0&&--i.readyWait>0||(vt.resolveWith(u,[i]),i.fn.triggerHandler&&(i(u).triggerHandler("ready"),i(u).off("ready"))))}});i.ready.promise=function(t){return vt||(vt=i.Deferred(),"complete"===u.readyState||"loading"!==u.readyState&&!u.documentElement.doScroll?n.setTimeout(i.ready):(u.addEventListener("DOMContentLoaded",yt),n.addEventListener("load",yt))),vt.promise(t)};i.ready.promise();a=function(n,t,r,u,f,e,o){var s=0,c=n.length,h=null==r;if("object"===i.type(r)){f=!0;for(s in r)a(n,t,s,r[s],!0,e,o)}else if(void 0!==u&&(f=!0,i.isFunction(u)||(o=!0),h&&(o?(t.call(n,u),t=null):(h=t,t=function(n,t,r){return h.call(i(n),r)})),t))for(;c>s;s++)t(n[s],r,o?u:u.call(n[s],s,t(n[s],r)));return f?n:h?t.call(n):c?t(n[0],r):e};g=function(n){return 1===n.nodeType||9===n.nodeType||!+n.nodeType};et.uid=1;et.prototype={register:function(n,t){var i=t||{};return n.nodeType?n[this.expando]=i:Object.defineProperty(n,this.expando,{value:i,writable:!0,configurable:!0}),n[this.expando]},cache:function(n){if(!g(n))return{};var t=n[this.expando];return t||(t={},g(n)&&(n.nodeType?n[this.expando]=t:Object.defineProperty(n,this.expando,{value:t,configurable:!0}))),t},set:function(n,t,i){var r,u=this.cache(n);if("string"==typeof t)u[t]=i;else for(r in t)u[r]=t[r];return u},get:function(n,t){return void 0===t?this.cache(n):n[this.expando]&&n[this.expando][t]},access:function(n,t,r){var u;return void 0===t||t&&"string"==typeof t&&void 0===r?(u=this.get(n,t),void 0!==u?u:this.get(n,i.camelCase(t))):(this.set(n,t,r),void 0!==r?r:t)},remove:function(n,t){var f,r,e,u=n[this.expando];if(void 0!==u){if(void 0===t)this.register(n);else for(i.isArray(t)?r=t.concat(t.map(i.camelCase)):(e=i.camelCase(t),(t in u)?r=[t,e]:(r=e,r=(r in u)?[r]:r.match(h)||[])),f=r.length;f--;)delete u[r[f]];(void 0===t||i.isEmptyObject(u))&&(n.nodeType?n[this.expando]=void 0:delete n[this.expando])}},hasData:function(n){var t=n[this.expando];return void 0!==t&&!i.isEmptyObject(t)}};var r=new et,e=new et,df=/^(?:\{[\w\W]*\}|\[[\w\W]*\])$/,cr=/[A-Z]/g;i.extend({hasData:function(n){return e.hasData(n)||r.hasData(n)},data:function(n,t,i){return e.access(n,t,i)},removeData:function(n,t){e.remove(n,t)},_data:function(n,t,i){return r.access(n,t,i)},_removeData:function(n,t){r.remove(n,t)}});i.fn.extend({data:function(n,t){var o,f,s,u=this[0],h=u&&u.attributes;if(void 0===n){if(this.length&&(s=e.get(u),1===u.nodeType&&!r.get(u,"hasDataAttrs"))){for(o=h.length;o--;)h[o]&&(f=h[o].name,0===f.indexOf("data-")&&(f=i.camelCase(f.slice(5)),lr(u,f,s[f])));r.set(u,"hasDataAttrs",!0)}return s}return"object"==typeof n?this.each(function(){e.set(this,n)}):a(this,function(t){var r,f;if(u&&void 0===t){if((r=e.get(u,n)||e.get(u,n.replace(cr,"-$&").toLowerCase()),void 0!==r)||(f=i.camelCase(n),r=e.get(u,f),void 0!==r)||(r=lr(u,f,void 0),void 0!==r))return r}else f=i.camelCase(n),this.each(function(){var i=e.get(this,f);e.set(this,f,t);n.indexOf("-")>-1&&void 0!==i&&e.set(this,n,t)})},null,t,arguments.length>1,null,!0)},removeData:function(n){return this.each(function(){e.remove(this,n)})}});i.extend({queue:function(n,t,u){var f;if(n)return(t=(t||"fx")+"queue",f=r.get(n,t),u&&(!f||i.isArray(u)?f=r.access(n,t,i.makeArray(u)):f.push(u)),f||[])},dequeue:function(n,t){t=t||"fx";var r=i.queue(n,t),e=r.length,u=r.shift(),f=i._queueHooks(n,t),o=function(){i.dequeue(n,t)};"inprogress"===u&&(u=r.shift(),e--);u&&("fx"===t&&r.unshift("inprogress"),delete f.stop,u.call(n,o,f));!e&&f&&f.empty.fire()},_queueHooks:function(n,t){var u=t+"queueHooks";return r.get(n,u)||r.access(n,u,{empty:i.Callbacks("once memory").add(function(){r.remove(n,[t+"queue",u])})})}});i.fn.extend({queue:function(n,t){var r=2;return"string"!=typeof n&&(t=n,n="fx",r--),arguments.length<r?i.queue(this[0],n):void 0===t?this:this.each(function(){var r=i.queue(this,n,t);i._queueHooks(this,n);"fx"===n&&"inprogress"!==r[0]&&i.dequeue(this,n)})},dequeue:function(n){return this.each(function(){i.dequeue(this,n)})},clearQueue:function(n){return this.queue(n||"fx",[])},promise:function(n,t){var u,e=1,o=i.Deferred(),f=this,s=this.length,h=function(){--e||o.resolveWith(f,[f])};for("string"!=typeof n&&(t=n,n=void 0),n=n||"fx";s--;)u=r.get(f[s],n+"queueHooks"),u&&u.empty&&(e++,u.empty.add(h));return h(),o.promise(t)}});var ar=/[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source,ot=new RegExp("^(?:([+-])=|)("+ar+")([a-z%]*)$","i"),w=["Top","Right","Bottom","Left"],st=function(n,t){return n=t||n,"none"===i.css(n,"display")||!i.contains(n.ownerDocument,n)};var yr=/^(?:checkbox|radio)$/i,pr=/<([\w:-]+)/,wr=/^$|\/(?:java|ecma)script/i,c={option:[1,"<select multiple='multiple'>","<\/select>"],thead:[1,"<table>","<\/table>"],col:[2,"<table><colgroup>","<\/colgroup><\/table>"],tr:[2,"<table><tbody>","<\/tbody><\/table>"],td:[3,"<table><tbody><tr>","<\/tr><\/tbody><\/table>"],_default:[0,"",""]};c.optgroup=c.option;c.tbody=c.tfoot=c.colgroup=c.caption=c.thead;c.th=c.td;br=/<|&#?\w+;/;!function(){var i=u.createDocumentFragment(),n=i.appendChild(u.createElement("div")),t=u.createElement("input");t.setAttribute("type","radio");t.setAttribute("checked","checked");t.setAttribute("name","t");n.appendChild(t);f.checkClone=n.cloneNode(!0).cloneNode(!0).lastChild.checked;n.innerHTML="<textarea>x<\/textarea>";f.noCloneChecked=!!n.cloneNode(!0).lastChild.defaultValue}();var gf=/^key/,ne=/^(?:mouse|pointer|contextmenu|drag|drop)|click/,dr=/^([^.]*)(?:\.(.+)|)/;i.event={global:{},add:function(n,t,u,f,e){var v,y,w,p,b,c,s,l,o,k,d,a=r.get(n);if(a)for(u.handler&&(v=u,u=v.handler,e=v.selector),u.guid||(u.guid=i.guid++),(p=a.events)||(p=a.events={}),(y=a.handle)||(y=a.handle=function(t){if("undefined"!=typeof i&&i.event.triggered!==t.type)return i.event.dispatch.apply(n,arguments)}),t=(t||"").match(h)||[""],b=t.length;b--;)w=dr.exec(t[b])||[],o=d=w[1],k=(w[2]||"").split(".").sort(),o&&(s=i.event.special[o]||{},o=(e?s.delegateType:s.bindType)||o,s=i.event.special[o]||{},c=i.extend({type:o,origType:d,data:f,handler:u,guid:u.guid,selector:e,needsContext:e&&i.expr.match.needsContext.test(e),namespace:k.join(".")},v),(l=p[o])||(l=p[o]=[],l.delegateCount=0,s.setup&&s.setup.call(n,f,k,y)!==!1||n.addEventListener&&n.addEventListener(o,y)),s.add&&(s.add.call(n,c),c.handler.guid||(c.handler.guid=u.guid)),e?l.splice(l.delegateCount++,0,c):l.push(c),i.event.global[o]=!0)},remove:function(n,t,u,f,e){var y,k,c,v,p,s,l,a,o,b,d,w=r.hasData(n)&&r.get(n);if(w&&(v=w.events)){for(t=(t||"").match(h)||[""],p=t.length;p--;)if(c=dr.exec(t[p])||[],o=d=c[1],b=(c[2]||"").split(".").sort(),o){for(l=i.event.special[o]||{},o=(f?l.delegateType:l.bindType)||o,a=v[o]||[],c=c[2]&&new RegExp("(^|\\.)"+b.join("\\.(?:.*\\.|)")+"(\\.|$)"),k=y=a.length;y--;)s=a[y],!e&&d!==s.origType||u&&u.guid!==s.guid||c&&!c.test(s.namespace)||f&&f!==s.selector&&("**"!==f||!s.selector)||(a.splice(y,1),s.selector&&a.delegateCount--,l.remove&&l.remove.call(n,s));k&&!a.length&&(l.teardown&&l.teardown.call(n,b,w.handle)!==!1||i.removeEvent(n,o,w.handle),delete v[o])}else for(o in v)i.event.remove(n,o+t[p],u,f,!0);i.isEmptyObject(v)&&r.remove(n,"handle events")}},dispatch:function(n){n=i.event.fix(n);var o,s,e,u,t,h=[],c=v.call(arguments),l=(r.get(this,"events")||{})[n.type]||[],f=i.event.special[n.type]||{};if(c[0]=n,n.delegateTarget=this,!f.preDispatch||f.preDispatch.call(this,n)!==!1){for(h=i.event.handlers.call(this,n,l),o=0;(u=h[o++])&&!n.isPropagationStopped();)for(n.currentTarget=u.elem,s=0;(t=u.handlers[s++])&&!n.isImmediatePropagationStopped();)n.rnamespace&&!n.rnamespace.test(t.namespace)||(n.handleObj=t,n.data=t.data,e=((i.event.special[t.origType]||{}).handle||t.handler).apply(u.elem,c),void 0!==e&&(n.result=e)===!1&&(n.preventDefault(),n.stopPropagation()));return f.postDispatch&&f.postDispatch.call(this,n),n.result}},handlers:function(n,t){var e,u,f,o,h=[],s=t.delegateCount,r=n.target;if(s&&r.nodeType&&("click"!==n.type||isNaN(n.button)||n.button<1))for(;r!==this;r=r.parentNode||this)if(1===r.nodeType&&(r.disabled!==!0||"click"!==n.type)){for(u=[],e=0;s>e;e++)o=t[e],f=o.selector+" ",void 0===u[f]&&(u[f]=o.needsContext?i(f,this).index(r)>-1:i.find(f,this,null,[r]).length),u[f]&&u.push(o);u.length&&h.push({elem:r,handlers:u})}return s<t.length&&h.push({elem:this,handlers:t.slice(s)}),h},props:"altKey bubbles cancelable ctrlKey currentTarget detail eventPhase metaKey relatedTarget shiftKey target timeStamp view which".split(" "),fixHooks:{},keyHooks:{props:"char charCode key keyCode".split(" "),filter:function(n,t){return null==n.which&&(n.which=null!=t.charCode?t.charCode:t.keyCode),n}},mouseHooks:{props:"button buttons clientX clientY offsetX offsetY pageX pageY screenX screenY toElement".split(" "),filter:function(n,t){var e,i,r,f=t.button;return null==n.pageX&&null!=t.clientX&&(e=n.target.ownerDocument||u,i=e.documentElement,r=e.body,n.pageX=t.clientX+(i&&i.scrollLeft||r&&r.scrollLeft||0)-(i&&i.clientLeft||r&&r.clientLeft||0),n.pageY=t.clientY+(i&&i.scrollTop||r&&r.scrollTop||0)-(i&&i.clientTop||r&&r.clientTop||0)),n.which||void 0===f||(n.which=1&f?1:2&f?3:4&f?2:0),n}},fix:function(n){if(n[i.expando])return n;var f,e,o,r=n.type,s=n,t=this.fixHooks[r];for(t||(this.fixHooks[r]=t=ne.test(r)?this.mouseHooks:gf.test(r)?this.keyHooks:{}),o=t.props?this.props.concat(t.props):this.props,n=new i.Event(s),f=o.length;f--;)e=o[f],n[e]=s[e];return n.target||(n.target=u),3===n.target.nodeType&&(n.target=n.target.parentNode),t.filter?t.filter(n,s):n},special:{load:{noBubble:!0},focus:{trigger:function(){if(this!==gr()&&this.focus)return(this.focus(),!1)},delegateType:"focusin"},blur:{trigger:function(){if(this===gr()&&this.blur)return(this.blur(),!1)},delegateType:"focusout"},click:{trigger:function(){if("checkbox"===this.type&&this.click&&i.nodeName(this,"input"))return(this.click(),!1)},_default:function(n){return i.nodeName(n.target,"a")}},beforeunload:{postDispatch:function(n){void 0!==n.result&&n.originalEvent&&(n.originalEvent.returnValue=n.result)}}}};i.removeEvent=function(n,t,i){n.removeEventListener&&n.removeEventListener(t,i)};i.Event=function(n,t){return this instanceof i.Event?(n&&n.type?(this.originalEvent=n,this.type=n.type,this.isDefaultPrevented=n.defaultPrevented||void 0===n.defaultPrevented&&n.returnValue===!1?pt:nt):this.type=n,t&&i.extend(this,t),this.timeStamp=n&&n.timeStamp||i.now(),void(this[i.expando]=!0)):new i.Event(n,t)};i.Event.prototype={constructor:i.Event,isDefaultPrevented:nt,isPropagationStopped:nt,isImmediatePropagationStopped:nt,preventDefault:function(){var n=this.originalEvent;this.isDefaultPrevented=pt;n&&n.preventDefault()},stopPropagation:function(){var n=this.originalEvent;this.isPropagationStopped=pt;n&&n.stopPropagation()},stopImmediatePropagation:function(){var n=this.originalEvent;this.isImmediatePropagationStopped=pt;n&&n.stopImmediatePropagation();this.stopPropagation()}};i.each({mouseenter:"mouseover",mouseleave:"mouseout",pointerenter:"pointerover",pointerleave:"pointerout"},function(n,t){i.event.special[n]={delegateType:t,bindType:t,handle:function(n){var u,f=this,r=n.relatedTarget,e=n.handleObj;return r&&(r===f||i.contains(f,r))||(n.type=e.origType,u=e.handler.apply(this,arguments),n.type=t),u}}});i.fn.extend({on:function(n,t,i,r){return fi(this,n,t,i,r)},one:function(n,t,i,r){return fi(this,n,t,i,r,1)},off:function(n,t,r){var u,f;if(n&&n.preventDefault&&n.handleObj)return u=n.handleObj,i(n.delegateTarget).off(u.namespace?u.origType+"."+u.namespace:u.origType,u.selector,u.handler),this;if("object"==typeof n){for(f in n)this.off(f,t,n[f]);return this}return t!==!1&&"function"!=typeof t||(r=t,t=void 0),r===!1&&(r=nt),this.each(function(){i.event.remove(this,n,r,t)})}});var te=/<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:-]+)[^>]*)\/>/gi,ie=/<script|<style|<link/i,re=/checked\s*(?:[^=]|=\s*.checked.)/i,ue=/^true\/(.*)/,fe=/^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g;i.extend({htmlPrefilter:function(n){return n.replace(te,"<$1><\/$2>")},clone:function(n,t,r){var u,c,s,e,h=n.cloneNode(!0),l=i.contains(n.ownerDocument,n);if(!(f.noCloneChecked||1!==n.nodeType&&11!==n.nodeType||i.isXMLDoc(n)))for(e=o(h),s=o(n),u=0,c=s.length;c>u;u++)se(s[u],e[u]);if(t)if(r)for(s=s||o(n),e=e||o(h),u=0,c=s.length;c>u;u++)tu(s[u],e[u]);else tu(n,h);return e=o(h,"script"),e.length>0&&ui(e,!l&&o(n,"script")),h},cleanData:function(n){for(var u,t,f,s=i.event.special,o=0;void 0!==(t=n[o]);o++)if(g(t)){if(u=t[r.expando]){if(u.events)for(f in u.events)s[f]?i.event.remove(t,f):i.removeEvent(t,f,u.handle);t[r.expando]=void 0}t[e.expando]&&(t[e.expando]=void 0)}}});i.fn.extend({domManip:b,detach:function(n){return iu(this,n,!0)},remove:function(n){return iu(this,n)},text:function(n){return a(this,function(n){return void 0===n?i.text(this):this.empty().each(function(){1!==this.nodeType&&11!==this.nodeType&&9!==this.nodeType||(this.textContent=n)})},null,n,arguments.length)},append:function(){return b(this,arguments,function(n){if(1===this.nodeType||11===this.nodeType||9===this.nodeType){var t=nu(this,n);t.appendChild(n)}})},prepend:function(){return b(this,arguments,function(n){if(1===this.nodeType||11===this.nodeType||9===this.nodeType){var t=nu(this,n);t.insertBefore(n,t.firstChild)}})},before:function(){return b(this,arguments,function(n){this.parentNode&&this.parentNode.insertBefore(n,this)})},after:function(){return b(this,arguments,function(n){this.parentNode&&this.parentNode.insertBefore(n,this.nextSibling)})},empty:function(){for(var n,t=0;null!=(n=this[t]);t++)1===n.nodeType&&(i.cleanData(o(n,!1)),n.textContent="");return this},clone:function(n,t){return n=null==n?!1:n,t=null==t?n:t,this.map(function(){return i.clone(this,n,t)})},html:function(n){return a(this,function(n){var t=this[0]||{},r=0,u=this.length;if(void 0===n&&1===t.nodeType)return t.innerHTML;if("string"==typeof n&&!ie.test(n)&&!c[(pr.exec(n)||["",""])[1].toLowerCase()]){n=i.htmlPrefilter(n);try{for(;u>r;r++)t=this[r]||{},1===t.nodeType&&(i.cleanData(o(t,!1)),t.innerHTML=n);t=0}catch(f){}}t&&this.empty().append(n)},null,n,arguments.length)},replaceWith:function(){var n=[];return b(this,arguments,function(t){var r=this.parentNode;i.inArray(this,n)<0&&(i.cleanData(o(this)),r&&r.replaceChild(t,this))},n)}});i.each({appendTo:"append",prependTo:"prepend",insertBefore:"before",insertAfter:"after",replaceAll:"replaceWith"},function(n,t){i.fn[n]=function(n){for(var u,f=[],e=i(n),o=e.length-1,r=0;o>=r;r++)u=r===o?this:this.clone(!0),i(e[r])[t](u),ti.apply(f,u.get());return this.pushStack(f)}});ei={HTML:"block",BODY:"block"};var uu=/^margin/,si=new RegExp("^("+ar+")(?!px)[a-z%]+$","i"),bt=function(t){var i=t.ownerDocument.defaultView;return i&&i.opener||(i=n),i.getComputedStyle(t)},hi=function(n,t,i,r){var f,u,e={};for(u in t)e[u]=n.style[u],n.style[u]=t[u];f=i.apply(n,r||[]);for(u in t)n.style[u]=e[u];return f},ht=u.documentElement;!function(){var s,e,h,c,r=u.createElement("div"),t=u.createElement("div");if(t.style){t.style.backgroundClip="content-box";t.cloneNode(!0).style.backgroundClip="";f.clearCloneStyle="content-box"===t.style.backgroundClip;r.style.cssText="border:0;width:8px;height:0;top:0;left:-9999px;padding:0;margin-top:1px;position:absolute";r.appendChild(t);function o(){t.style.cssText="-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;position:relative;display:block;margin:auto;border:1px;padding:1px;top:1%;width:50%";t.innerHTML="";ht.appendChild(r);var i=n.getComputedStyle(t);s="1%"!==i.top;c="2px"===i.marginLeft;e="4px"===i.width;t.style.marginRight="50%";h="4px"===i.marginRight;ht.removeChild(r)}i.extend(f,{pixelPosition:function(){return o(),s},boxSizingReliable:function(){return null==e&&o(),e},pixelMarginRight:function(){return null==e&&o(),h},reliableMarginLeft:function(){return null==e&&o(),c},reliableMarginRight:function(){var f,i=t.appendChild(u.createElement("div"));return i.style.cssText=t.style.cssText="-webkit-box-sizing:content-box;box-sizing:content-box;display:block;margin:0;border:0;padding:0",i.style.marginRight=i.style.width="0",t.style.width="1px",ht.appendChild(r),f=!parseFloat(n.getComputedStyle(i).marginRight),ht.removeChild(r),t.removeChild(i),f}})}}();var he=/^(none|table(?!-c[ea]).+)/,ce={position:"absolute",visibility:"hidden",display:"block"},fu={letterSpacing:"0",fontWeight:"400"},eu=["Webkit","O","Moz","ms"],ou=u.createElement("div").style;i.extend({cssHooks:{opacity:{get:function(n,t){if(t){var i=tt(n,"opacity");return""===i?"1":i}}}},cssNumber:{animationIterationCount:!0,columnCount:!0,fillOpacity:!0,flexGrow:!0,flexShrink:!0,fontWeight:!0,lineHeight:!0,opacity:!0,order:!0,orphans:!0,widows:!0,zIndex:!0,zoom:!0},cssProps:{float:"cssFloat"},style:function(n,t,r,u){if(n&&3!==n.nodeType&&8!==n.nodeType&&n.style){var e,h,o,s=i.camelCase(t),c=n.style;return t=i.cssProps[s]||(i.cssProps[s]=su(s)||s),o=i.cssHooks[t]||i.cssHooks[s],void 0===r?o&&"get"in o&&void 0!==(e=o.get(n,!1,u))?e:c[t]:(h=typeof r,"string"===h&&(e=ot.exec(r))&&e[1]&&(r=vr(n,t,e),h="number"),null!=r&&r===r&&("number"===h&&(r+=e&&e[3]||(i.cssNumber[s]?"":"px")),f.clearCloneStyle||""!==r||0!==t.indexOf("background")||(c[t]="inherit"),o&&"set"in o&&void 0===(r=o.set(n,r,u))||(c[t]=r)),void 0)}},css:function(n,t,r,u){var f,s,o,e=i.camelCase(t);return t=i.cssProps[e]||(i.cssProps[e]=su(e)||e),o=i.cssHooks[t]||i.cssHooks[e],o&&"get"in o&&(f=o.get(n,!0,r)),void 0===f&&(f=tt(n,t,u)),"normal"===f&&t in fu&&(f=fu[t]),""===r||r?(s=parseFloat(f),r===!0||isFinite(s)?s||0:f):f}});i.each(["height","width"],function(n,t){i.cssHooks[t]={get:function(n,r,u){if(r)return he.test(i.css(n,"display"))&&0===n.offsetWidth?hi(n,ce,function(){return lu(n,t,u)}):lu(n,t,u)},set:function(n,r,u){var f,e=u&&bt(n),o=u&&cu(n,t,u,"border-box"===i.css(n,"boxSizing",!1,e),e);return o&&(f=ot.exec(r))&&"px"!==(f[3]||"px")&&(n.style[t]=r,r=i.css(n,t)),hu(n,r,o)}}});i.cssHooks.marginLeft=ci(f.reliableMarginLeft,function(n,t){if(t)return(parseFloat(tt(n,"marginLeft"))||n.getBoundingClientRect().left-hi(n,{marginLeft:0},function(){return n.getBoundingClientRect().left}))+"px"});i.cssHooks.marginRight=ci(f.reliableMarginRight,function(n,t){if(t)return hi(n,{display:"inline-block"},tt,[n,"marginRight"])});i.each({margin:"",padding:"",border:"Width"},function(n,t){i.cssHooks[n+t]={expand:function(i){for(var r=0,f={},u="string"==typeof i?i.split(" "):[i];4>r;r++)f[n+w[r]+t]=u[r]||u[r-2]||u[0];return f}};uu.test(n)||(i.cssHooks[n+t].set=hu)});i.fn.extend({css:function(n,t){return a(this,function(n,t,r){var f,e,o={},u=0;if(i.isArray(t)){for(f=bt(n),e=t.length;e>u;u++)o[t[u]]=i.css(n,t[u],!1,f);return o}return void 0!==r?i.style(n,t,r):i.css(n,t)},n,t,arguments.length>1)},show:function(){return au(this,!0)},hide:function(){return au(this)},toggle:function(n){return"boolean"==typeof n?n?this.show():this.hide():this.each(function(){st(this)?i(this).show():i(this).hide()})}});i.Tween=s;s.prototype={constructor:s,init:function(n,t,r,u,f,e){this.elem=n;this.prop=r;this.easing=f||i.easing._default;this.options=t;this.start=this.now=this.cur();this.end=u;this.unit=e||(i.cssNumber[r]?"":"px")},cur:function(){var n=s.propHooks[this.prop];return n&&n.get?n.get(this):s.propHooks._default.get(this)},run:function(n){var t,r=s.propHooks[this.prop];return this.pos=this.options.duration?t=i.easing[this.easing](n,this.options.duration*n,0,1,this.options.duration):t=n,this.now=(this.end-this.start)*t+this.start,this.options.step&&this.options.step.call(this.elem,this.now,this),r&&r.set?r.set(this):s.propHooks._default.set(this),this}};s.prototype.init.prototype=s.prototype;s.propHooks={_default:{get:function(n){var t;return 1!==n.elem.nodeType||null!=n.elem[n.prop]&&null==n.elem.style[n.prop]?n.elem[n.prop]:(t=i.css(n.elem,n.prop,""),t&&"auto"!==t?t:0)},set:function(n){i.fx.step[n.prop]?i.fx.step[n.prop](n):1!==n.elem.nodeType||null==n.elem.style[i.cssProps[n.prop]]&&!i.cssHooks[n.prop]?n.elem[n.prop]=n.now:i.style(n.elem,n.prop,n.now+n.unit)}}};s.propHooks.scrollTop=s.propHooks.scrollLeft={set:function(n){n.elem.nodeType&&n.elem.parentNode&&(n.elem[n.prop]=n.now)}};i.easing={linear:function(n){return n},swing:function(n){return.5-Math.cos(n*Math.PI)/2},_default:"swing"};i.fx=s.prototype.init;i.fx.step={};vu=/^(?:toggle|show|hide)$/;yu=/queueHooks$/;i.Animation=i.extend(l,{tweeners:{"*":[function(n,t){var i=this.createTween(n,t);return vr(i.elem,n,ot.exec(t),i),i}]},tweener:function(n,t){i.isFunction(n)?(t=n,n=["*"]):n=n.match(h);for(var r,u=0,f=n.length;f>u;u++)r=n[u],l.tweeners[r]=l.tweeners[r]||[],l.tweeners[r].unshift(t)},prefilters:[le],prefilter:function(n,t){t?l.prefilters.unshift(n):l.prefilters.push(n)}});i.speed=function(n,t,r){var u=n&&"object"==typeof n?i.extend({},n):{complete:r||!r&&t||i.isFunction(n)&&n,duration:n,easing:r&&t||t&&!i.isFunction(t)&&t};return u.duration=i.fx.off?0:"number"==typeof u.duration?u.duration:u.duration in i.fx.speeds?i.fx.speeds[u.duration]:i.fx.speeds._default,null!=u.queue&&u.queue!==!0||(u.queue="fx"),u.old=u.complete,u.complete=function(){i.isFunction(u.old)&&u.old.call(this);u.queue&&i.dequeue(this,u.queue)},u};i.fn.extend({fadeTo:function(n,t,i,r){return this.filter(st).css("opacity",0).show().end().animate({opacity:t},n,i,r)},animate:function(n,t,u,f){var s=i.isEmptyObject(n),o=i.speed(t,u,f),e=function(){var t=l(this,i.extend({},n),o);(s||r.get(this,"finish"))&&t.stop(!0)};return e.finish=e,s||o.queue===!1?this.each(e):this.queue(o.queue,e)},stop:function(n,t,u){var f=function(n){var t=n.stop;delete n.stop;t(u)};return"string"!=typeof n&&(u=t,t=n,n=void 0),t&&n!==!1&&this.queue(n||"fx",[]),this.each(function(){var s=!0,t=null!=n&&n+"queueHooks",o=i.timers,e=r.get(this);if(t)e[t]&&e[t].stop&&f(e[t]);else for(t in e)e[t]&&e[t].stop&&yu.test(t)&&f(e[t]);for(t=o.length;t--;)o[t].elem!==this||null!=n&&o[t].queue!==n||(o[t].anim.stop(u),s=!1,o.splice(t,1));!s&&u||i.dequeue(this,n)})},finish:function(n){return n!==!1&&(n=n||"fx"),this.each(function(){var t,e=r.get(this),u=e[n+"queue"],o=e[n+"queueHooks"],f=i.timers,s=u?u.length:0;for(e.finish=!0,i.queue(this,n,[]),o&&o.stop&&o.stop.call(this,!0),t=f.length;t--;)f[t].elem===this&&f[t].queue===n&&(f[t].anim.stop(!0),f.splice(t,1));for(t=0;s>t;t++)u[t]&&u[t].finish&&u[t].finish.call(this);delete e.finish})}});i.each(["toggle","show","hide"],function(n,t){var r=i.fn[t];i.fn[t]=function(n,i,u){return null==n||"boolean"==typeof n?r.apply(this,arguments):this.animate(dt(t,!0),n,i,u)}});i.each({slideDown:dt("show"),slideUp:dt("hide"),slideToggle:dt("toggle"),fadeIn:{opacity:"show"},fadeOut:{opacity:"hide"},fadeToggle:{opacity:"toggle"}},function(n,t){i.fn[n]=function(n,i,r){return this.animate(t,n,i,r)}});i.timers=[];i.fx.tick=function(){var r,n=0,t=i.timers;for(it=i.now();n<t.length;n++)r=t[n],r()||t[n]!==r||t.splice(n--,1);t.length||i.fx.stop();it=void 0};i.fx.timer=function(n){i.timers.push(n);n()?i.fx.start():i.timers.pop()};i.fx.interval=13;i.fx.start=function(){kt||(kt=n.setInterval(i.fx.tick,i.fx.interval))};i.fx.stop=function(){n.clearInterval(kt);kt=null};i.fx.speeds={slow:600,fast:200,_default:400};i.fn.delay=function(t,r){return t=i.fx?i.fx.speeds[t]||t:t,r=r||"fx",this.queue(r,function(i,r){var u=n.setTimeout(i,t);r.stop=function(){n.clearTimeout(u)}})},function(){var n=u.createElement("input"),t=u.createElement("select"),i=t.appendChild(u.createElement("option"));n.type="checkbox";f.checkOn=""!==n.value;f.optSelected=i.selected;t.disabled=!0;f.optDisabled=!i.disabled;n=u.createElement("input");n.value="t";n.type="radio";f.radioValue="t"===n.value}();rt=i.expr.attrHandle;i.fn.extend({attr:function(n,t){return a(this,i.attr,n,t,arguments.length>1)},removeAttr:function(n){return this.each(function(){i.removeAttr(this,n)})}});i.extend({attr:function(n,t,r){var u,f,e=n.nodeType;if(3!==e&&8!==e&&2!==e)return"undefined"==typeof n.getAttribute?i.prop(n,t,r):(1===e&&i.isXMLDoc(n)||(t=t.toLowerCase(),f=i.attrHooks[t]||(i.expr.match.bool.test(t)?bu:void 0)),void 0!==r?null===r?void i.removeAttr(n,t):f&&"set"in f&&void 0!==(u=f.set(n,r,t))?u:(n.setAttribute(t,r+""),r):f&&"get"in f&&null!==(u=f.get(n,t))?u:(u=i.find.attr(n,t),null==u?void 0:u))},attrHooks:{type:{set:function(n,t){if(!f.radioValue&&"radio"===t&&i.nodeName(n,"input")){var r=n.value;return n.setAttribute("type",t),r&&(n.value=r),t}}}},removeAttr:function(n,t){var r,u,e=0,f=t&&t.match(h);if(f&&1===n.nodeType)while(r=f[e++])u=i.propFix[r]||r,i.expr.match.bool.test(r)&&(n[u]=!1),n.removeAttribute(r)}});bu={set:function(n,t,r){return t===!1?i.removeAttr(n,r):n.setAttribute(r,r),r}};i.each(i.expr.match.bool.source.match(/\w+/g),function(n,t){var r=rt[t]||i.find.attr;rt[t]=function(n,t,i){var u,f;return i||(f=rt[t],rt[t]=u,u=null!=r(n,t,i)?t.toLowerCase():null,rt[t]=f),u}});ku=/^(?:input|select|textarea|button)$/i;du=/^(?:a|area)$/i;i.fn.extend({prop:function(n,t){return a(this,i.prop,n,t,arguments.length>1)},removeProp:function(n){return this.each(function(){delete this[i.propFix[n]||n]})}});i.extend({prop:function(n,t,r){var f,u,e=n.nodeType;if(3!==e&&8!==e&&2!==e)return 1===e&&i.isXMLDoc(n)||(t=i.propFix[t]||t,u=i.propHooks[t]),void 0!==r?u&&"set"in u&&void 0!==(f=u.set(n,r,t))?f:n[t]=r:u&&"get"in u&&null!==(f=u.get(n,t))?f:n[t]},propHooks:{tabIndex:{get:function(n){var t=i.find.attr(n,"tabindex");return t?parseInt(t,10):ku.test(n.nodeName)||du.test(n.nodeName)&&n.href?0:-1}}},propFix:{"for":"htmlFor","class":"className"}});f.optSelected||(i.propHooks.selected={get:function(n){var t=n.parentNode;return t&&t.parentNode&&t.parentNode.selectedIndex,null},set:function(n){var t=n.parentNode;t&&(t.selectedIndex,t.parentNode&&t.parentNode.selectedIndex)}});i.each(["tabIndex","readOnly","maxLength","cellSpacing","cellPadding","rowSpan","colSpan","useMap","frameBorder","contentEditable"],function(){i.propFix[this.toLowerCase()]=this});gt=/[\t\r\n\f]/g;i.fn.extend({addClass:function(n){var o,t,r,u,f,s,e,c=0;if(i.isFunction(n))return this.each(function(t){i(this).addClass(n.call(this,t,k(this)))});if("string"==typeof n&&n)for(o=n.match(h)||[];t=this[c++];)if(u=k(t),r=1===t.nodeType&&(" "+u+" ").replace(gt," ")){for(s=0;f=o[s++];)r.indexOf(" "+f+" ")<0&&(r+=f+" ");e=i.trim(r);u!==e&&t.setAttribute("class",e)}return this},removeClass:function(n){var o,r,t,u,f,s,e,c=0;if(i.isFunction(n))return this.each(function(t){i(this).removeClass(n.call(this,t,k(this)))});if(!arguments.length)return this.attr("class","");if("string"==typeof n&&n)for(o=n.match(h)||[];r=this[c++];)if(u=k(r),t=1===r.nodeType&&(" "+u+" ").replace(gt," ")){for(s=0;f=o[s++];)while(t.indexOf(" "+f+" ")>-1)t=t.replace(" "+f+" "," ");e=i.trim(t);u!==e&&r.setAttribute("class",e)}return this},toggleClass:function(n,t){var u=typeof n;return"boolean"==typeof t&&"string"===u?t?this.addClass(n):this.removeClass(n):i.isFunction(n)?this.each(function(r){i(this).toggleClass(n.call(this,r,k(this),t),t)}):this.each(function(){var t,e,f,o;if("string"===u)for(e=0,f=i(this),o=n.match(h)||[];t=o[e++];)f.hasClass(t)?f.removeClass(t):f.addClass(t);else void 0!==n&&"boolean"!==u||(t=k(this),t&&r.set(this,"__className__",t),this.setAttribute&&this.setAttribute("class",t||n===!1?"":r.get(this,"__className__")||""))})},hasClass:function(n){for(var t,r=0,i=" "+n+" ";t=this[r++];)if(1===t.nodeType&&(" "+k(t)+" ").replace(gt," ").indexOf(i)>-1)return!0;return!1}});gu=/\r/g;nf=/[\x20\t\r\n\f]+/g;i.fn.extend({val:function(n){var t,r,f,u=this[0];return arguments.length?(f=i.isFunction(n),this.each(function(r){var u;1===this.nodeType&&(u=f?n.call(this,r,i(this).val()):n,null==u?u="":"number"==typeof u?u+="":i.isArray(u)&&(u=i.map(u,function(n){return null==n?"":n+""})),t=i.valHooks[this.type]||i.valHooks[this.nodeName.toLowerCase()],t&&"set"in t&&void 0!==t.set(this,u,"value")||(this.value=u))})):u?(t=i.valHooks[u.type]||i.valHooks[u.nodeName.toLowerCase()],t&&"get"in t&&void 0!==(r=t.get(u,"value"))?r:(r=u.value,"string"==typeof r?r.replace(gu,""):null==r?"":r)):void 0}});i.extend({valHooks:{option:{get:function(n){var t=i.find.attr(n,"value");return null!=t?t:i.trim(i.text(n)).replace(nf," ")}},select:{get:function(n){for(var o,t,s=n.options,r=n.selectedIndex,u="select-one"===n.type||0>r,h=u?null:[],c=u?r+1:s.length,e=0>r?c:u?r:0;c>e;e++)if(t=s[e],(t.selected||e===r)&&(f.optDisabled?!t.disabled:null===t.getAttribute("disabled"))&&(!t.parentNode.disabled||!i.nodeName(t.parentNode,"optgroup"))){if(o=i(t).val(),u)return o;h.push(o)}return h},set:function(n,t){for(var u,r,f=n.options,e=i.makeArray(t),o=f.length;o--;)r=f[o],(r.selected=i.inArray(i.valHooks.option.get(r),e)>-1)&&(u=!0);return u||(n.selectedIndex=-1),e}}}});i.each(["radio","checkbox"],function(){i.valHooks[this]={set:function(n,t){if(i.isArray(t))return n.checked=i.inArray(i(n).val(),t)>-1}};f.checkOn||(i.valHooks[this].get=function(n){return null===n.getAttribute("value")?"on":n.value})});li=/^(?:focusinfocus|focusoutblur)$/;i.extend(i.event,{trigger:function(t,f,e,o){var w,s,c,b,a,v,l,p=[e||u],h=ft.call(t,"type")?t.type:t,y=ft.call(t,"namespace")?t.namespace.split("."):[];if(s=c=e=e||u,3!==e.nodeType&&8!==e.nodeType&&!li.test(h+i.event.triggered)&&(h.indexOf(".")>-1&&(y=h.split("."),h=y.shift(),y.sort()),a=h.indexOf(":")<0&&"on"+h,t=t[i.expando]?t:new i.Event(h,"object"==typeof t&&t),t.isTrigger=o?2:3,t.namespace=y.join("."),t.rnamespace=t.namespace?new RegExp("(^|\\.)"+y.join("\\.(?:.*\\.|)")+"(\\.|$)"):null,t.result=void 0,t.target||(t.target=e),f=null==f?[t]:i.makeArray(f,[t]),l=i.event.special[h]||{},o||!l.trigger||l.trigger.apply(e,f)!==!1)){if(!o&&!l.noBubble&&!i.isWindow(e)){for(b=l.delegateType||h,li.test(b+h)||(s=s.parentNode);s;s=s.parentNode)p.push(s),c=s;c===(e.ownerDocument||u)&&p.push(c.defaultView||c.parentWindow||n)}for(w=0;(s=p[w++])&&!t.isPropagationStopped();)t.type=w>1?b:l.bindType||h,v=(r.get(s,"events")||{})[t.type]&&r.get(s,"handle"),v&&v.apply(s,f),v=a&&s[a],v&&v.apply&&g(s)&&(t.result=v.apply(s,f),t.result===!1&&t.preventDefault());return t.type=h,o||t.isDefaultPrevented()||l._default&&l._default.apply(p.pop(),f)!==!1||!g(e)||a&&i.isFunction(e[h])&&!i.isWindow(e)&&(c=e[a],c&&(e[a]=null),i.event.triggered=h,e[h](),i.event.triggered=void 0,c&&(e[a]=c)),t.result}},simulate:function(n,t,r){var u=i.extend(new i.Event,r,{type:n,isSimulated:!0});i.event.trigger(u,null,t);u.isDefaultPrevented()&&r.preventDefault()}});i.fn.extend({trigger:function(n,t){return this.each(function(){i.event.trigger(n,t,this)})},triggerHandler:function(n,t){var r=this[0];if(r)return i.event.trigger(n,t,r,!0)}});i.each("blur focus focusin focusout load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup error contextmenu".split(" "),function(n,t){i.fn[t]=function(n,i){return arguments.length>0?this.on(t,null,n,i):this.trigger(t)}});i.fn.extend({hover:function(n,t){return this.mouseenter(n).mouseleave(t||n)}});f.focusin="onfocusin"in n;f.focusin||i.each({focus:"focusin",blur:"focusout"},function(n,t){var u=function(n){i.event.simulate(t,n.target,i.event.fix(n))};i.event.special[t]={setup:function(){var i=this.ownerDocument||this,f=r.access(i,t);f||i.addEventListener(n,u,!0);r.access(i,t,(f||0)+1)},teardown:function(){var i=this.ownerDocument||this,f=r.access(i,t)-1;f?r.access(i,t,f):(i.removeEventListener(n,u,!0),r.remove(i,t))}}});var ct=n.location,ai=i.now(),vi=/\?/;i.parseJSON=function(n){return n?JSON.parse(n+""):null};i.parseXML=function(t){var r;if(!t||"string"!=typeof t)return null;try{r=(new n.DOMParser).parseFromString(t,"text/xml")}catch(u){r=void 0}return r&&!r.getElementsByTagName("parsererror").length||i.error("Invalid XML: "+t),r};var ve=/#.*$/,tf=/([?&])_=[^&]*/,ye=/^(.*?):[ \t]*([^\r\n]*)$/gm,pe=/^(?:GET|HEAD)$/,we=/^\/\//,rf={},yi={},uf="*/".concat("*"),pi=u.createElement("a");pi.href=ct.href;i.extend({active:0,lastModified:{},etag:{},ajaxSettings:{url:ct.href,type:"GET",isLocal:/^(?:about|app|app-storage|.+-extension|file|res|widget):$/.test(ct.protocol),global:!0,processData:!0,async:!0,contentType:"application/x-www-form-urlencoded; charset=UTF-8",accepts:{"*":uf,text:"text/plain",html:"text/html",xml:"application/xml, text/xml",json:"application/json, text/javascript"},contents:{xml:/\bxml\b/,html:/\bhtml/,json:/\bjson\b/},responseFields:{xml:"responseXML",text:"responseText",json:"responseJSON"},converters:{"* text":String,"text html":!0,"text json":i.parseJSON,"text xml":i.parseXML},flatOptions:{url:!0,context:!0}},ajaxSetup:function(n,t){return t?wi(wi(n,i.ajaxSettings),t):wi(i.ajaxSettings,n)},ajaxPrefilter:ff(rf),ajaxTransport:ff(yi),ajax:function(t,r){function b(t,r,u,h){var a,rt,it,p,b,l=r;2!==s&&(s=2,d&&n.clearTimeout(d),v=void 0,k=h||"",e.readyState=t>0?4:0,a=t>=200&&300>t||304===t,u&&(p=be(f,e,u)),p=ke(f,p,e,a),a?(f.ifModified&&(b=e.getResponseHeader("Last-Modified"),b&&(i.lastModified[o]=b),b=e.getResponseHeader("etag"),b&&(i.etag[o]=b)),204===t||"HEAD"===f.type?l="nocontent":304===t?l="notmodified":(l=p.state,rt=p.data,it=p.error,a=!it)):(it=l,!t&&l||(l="error",0>t&&(t=0))),e.status=t,e.statusText=(r||l)+"",a?nt.resolveWith(c,[rt,l,e]):nt.rejectWith(c,[e,l,it]),e.statusCode(w),w=void 0,y&&g.trigger(a?"ajaxSuccess":"ajaxError",[e,f,a?rt:it]),tt.fireWith(c,[e,l]),y&&(g.trigger("ajaxComplete",[e,f]),--i.active||i.event.trigger("ajaxStop")))}"object"==typeof t&&(r=t,t=void 0);r=r||{};var v,o,k,p,d,l,y,a,f=i.ajaxSetup({},r),c=f.context||f,g=f.context&&(c.nodeType||c.jquery)?i(c):i.event,nt=i.Deferred(),tt=i.Callbacks("once memory"),w=f.statusCode||{},it={},rt={},s=0,ut="canceled",e={readyState:0,getResponseHeader:function(n){var t;if(2===s){if(!p)for(p={};t=ye.exec(k);)p[t[1].toLowerCase()]=t[2];t=p[n.toLowerCase()]}return null==t?null:t},getAllResponseHeaders:function(){return 2===s?k:null},setRequestHeader:function(n,t){var i=n.toLowerCase();return s||(n=rt[i]=rt[i]||n,it[n]=t),this},overrideMimeType:function(n){return s||(f.mimeType=n),this},statusCode:function(n){var t;if(n)if(2>s)for(t in n)w[t]=[w[t],n[t]];else e.always(n[e.status]);return this},abort:function(n){var t=n||ut;return v&&v.abort(t),b(0,t),this}};if(nt.promise(e).complete=tt.add,e.success=e.done,e.error=e.fail,f.url=((t||f.url||ct.href)+"").replace(ve,"").replace(we,ct.protocol+"//"),f.type=r.method||r.type||f.method||f.type,f.dataTypes=i.trim(f.dataType||"*").toLowerCase().match(h)||[""],null==f.crossDomain){l=u.createElement("a");try{l.href=f.url;l.href=l.href;f.crossDomain=pi.protocol+"//"+pi.host!=l.protocol+"//"+l.host}catch(ft){f.crossDomain=!0}}if(f.data&&f.processData&&"string"!=typeof f.data&&(f.data=i.param(f.data,f.traditional)),ef(rf,f,r,e),2===s)return e;y=i.event&&f.global;y&&0==i.active++&&i.event.trigger("ajaxStart");f.type=f.type.toUpperCase();f.hasContent=!pe.test(f.type);o=f.url;f.hasContent||(f.data&&(o=f.url+=(vi.test(o)?"&":"?")+f.data,delete f.data),f.cache===!1&&(f.url=tf.test(o)?o.replace(tf,"$1_="+ai++):o+(vi.test(o)?"&":"?")+"_="+ai++));f.ifModified&&(i.lastModified[o]&&e.setRequestHeader("If-Modified-Since",i.lastModified[o]),i.etag[o]&&e.setRequestHeader("If-None-Match",i.etag[o]));(f.data&&f.hasContent&&f.contentType!==!1||r.contentType)&&e.setRequestHeader("Content-Type",f.contentType);e.setRequestHeader("Accept",f.dataTypes[0]&&f.accepts[f.dataTypes[0]]?f.accepts[f.dataTypes[0]]+("*"!==f.dataTypes[0]?", "+uf+"; q=0.01":""):f.accepts["*"]);for(a in f.headers)e.setRequestHeader(a,f.headers[a]);if(f.beforeSend&&(f.beforeSend.call(c,e,f)===!1||2===s))return e.abort();ut="abort";for(a in{success:1,error:1,complete:1})e[a](f[a]);if(v=ef(yi,f,r,e)){if(e.readyState=1,y&&g.trigger("ajaxSend",[e,f]),2===s)return e;f.async&&f.timeout>0&&(d=n.setTimeout(function(){e.abort("timeout")},f.timeout));try{s=1;v.send(it,b)}catch(ft){if(!(2>s))throw ft;b(-1,ft)}}else b(-1,"No Transport");return e},getJSON:function(n,t,r){return i.get(n,t,r,"json")},getScript:function(n,t){return i.get(n,void 0,t,"script")}});i.each(["get","post"],function(n,t){i[t]=function(n,r,u,f){return i.isFunction(r)&&(f=f||u,u=r,r=void 0),i.ajax(i.extend({url:n,type:t,dataType:f,data:r,success:u},i.isPlainObject(n)&&n))}});i._evalUrl=function(n){return i.ajax({url:n,type:"GET",dataType:"script",async:!1,global:!1,throws:!0})};i.fn.extend({wrapAll:function(n){var t;return i.isFunction(n)?this.each(function(t){i(this).wrapAll(n.call(this,t))}):(this[0]&&(t=i(n,this[0].ownerDocument).eq(0).clone(!0),this[0].parentNode&&t.insertBefore(this[0]),t.map(function(){for(var n=this;n.firstElementChild;)n=n.firstElementChild;return n}).append(this)),this)},wrapInner:function(n){return i.isFunction(n)?this.each(function(t){i(this).wrapInner(n.call(this,t))}):this.each(function(){var t=i(this),r=t.contents();r.length?r.wrapAll(n):t.append(n)})},wrap:function(n){var t=i.isFunction(n);return this.each(function(r){i(this).wrapAll(t?n.call(this,r):n)})},unwrap:function(){return this.parent().each(function(){i.nodeName(this,"body")||i(this).replaceWith(this.childNodes)}).end()}});i.expr.filters.hidden=function(n){return!i.expr.filters.visible(n)};i.expr.filters.visible=function(n){return n.offsetWidth>0||n.offsetHeight>0||n.getClientRects().length>0};var de=/%20/g,ge=/\[\]$/,of=/\r?\n/g,no=/^(?:submit|button|image|reset|file)$/i,to=/^(?:input|select|textarea|keygen)/i;return i.param=function(n,t){var r,u=[],f=function(n,t){t=i.isFunction(t)?t():null==t?"":t;u[u.length]=encodeURIComponent(n)+"="+encodeURIComponent(t)};if(void 0===t&&(t=i.ajaxSettings&&i.ajaxSettings.traditional),i.isArray(n)||n.jquery&&!i.isPlainObject(n))i.each(n,function(){f(this.name,this.value)});else for(r in n)bi(r,n[r],t,f);return u.join("&").replace(de,"+")},i.fn.extend({serialize:function(){return i.param(this.serializeArray())},serializeArray:function(){return this.map(function(){var n=i.prop(this,"elements");return n?i.makeArray(n):this}).filter(function(){var n=this.type;return this.name&&!i(this).is(":disabled")&&to.test(this.nodeName)&&!no.test(n)&&(this.checked||!yr.test(n))}).map(function(n,t){var r=i(this).val();return null==r?null:i.isArray(r)?i.map(r,function(n){return{name:t.name,value:n.replace(of,"\r\n")}}):{name:t.name,value:r.replace(of,"\r\n")}}).get()}}),i.ajaxSettings.xhr=function(){try{return new n.XMLHttpRequest}catch(t){}},sf={0:200,1223:204},ut=i.ajaxSettings.xhr(),f.cors=!!ut&&"withCredentials"in ut,f.ajax=ut=!!ut,i.ajaxTransport(function(t){var i,r;if(f.cors||ut&&!t.crossDomain)return{send:function(u,f){var o,e=t.xhr();if(e.open(t.type,t.url,t.async,t.username,t.password),t.xhrFields)for(o in t.xhrFields)e[o]=t.xhrFields[o];t.mimeType&&e.overrideMimeType&&e.overrideMimeType(t.mimeType);t.crossDomain||u["X-Requested-With"]||(u["X-Requested-With"]="XMLHttpRequest");for(o in u)e.setRequestHeader(o,u[o]);i=function(n){return function(){i&&(i=r=e.onload=e.onerror=e.onabort=e.onreadystatechange=null,"abort"===n?e.abort():"error"===n?"number"!=typeof e.status?f(0,"error"):f(e.status,e.statusText):f(sf[e.status]||e.status,e.statusText,"text"!==(e.responseType||"text")||"string"!=typeof e.responseText?{binary:e.response}:{text:e.responseText},e.getAllResponseHeaders()))}};e.onload=i();r=e.onerror=i("error");void 0!==e.onabort?e.onabort=r:e.onreadystatechange=function(){4===e.readyState&&n.setTimeout(function(){i&&r()})};i=i("abort");try{e.send(t.hasContent&&t.data||null)}catch(s){if(i)throw s;}},abort:function(){i&&i()}}}),i.ajaxSetup({accepts:{script:"text/javascript, application/javascript, application/ecmascript, application/x-ecmascript"},contents:{script:/\b(?:java|ecma)script\b/},converters:{"text script":function(n){return i.globalEval(n),n}}}),i.ajaxPrefilter("script",function(n){void 0===n.cache&&(n.cache=!1);n.crossDomain&&(n.type="GET")}),i.ajaxTransport("script",function(n){if(n.crossDomain){var r,t;return{send:function(f,e){r=i("<script>").prop({charset:n.scriptCharset,src:n.url}).on("load error",t=function(n){r.remove();t=null;n&&e("error"===n.type?404:200,n.type)});u.head.appendChild(r[0])},abort:function(){t&&t()}}}}),ki=[],ni=/(=)\?(?=&|$)|\?\?/,i.ajaxSetup({jsonp:"callback",jsonpCallback:function(){var n=ki.pop()||i.expando+"_"+ai++;return this[n]=!0,n}}),i.ajaxPrefilter("json jsonp",function(t,r,u){var f,e,o,s=t.jsonp!==!1&&(ni.test(t.url)?"url":"string"==typeof t.data&&0===(t.contentType||"").indexOf("application/x-www-form-urlencoded")&&ni.test(t.data)&&"data");if(s||"jsonp"===t.dataTypes[0])return(f=t.jsonpCallback=i.isFunction(t.jsonpCallback)?t.jsonpCallback():t.jsonpCallback,s?t[s]=t[s].replace(ni,"$1"+f):t.jsonp!==!1&&(t.url+=(vi.test(t.url)?"&":"?")+t.jsonp+"="+f),t.converters["script json"]=function(){return o||i.error(f+" was not called"),o[0]},t.dataTypes[0]="json",e=n[f],n[f]=function(){o=arguments},u.always(function(){void 0===e?i(n).removeProp(f):n[f]=e;t[f]&&(t.jsonpCallback=r.jsonpCallback,ki.push(f));o&&i.isFunction(e)&&e(o[0]);o=e=void 0}),"script")}),i.parseHTML=function(n,t,r){if(!n||"string"!=typeof n)return null;"boolean"==typeof t&&(r=t,t=!1);t=t||u;var f=rr.exec(n),e=!r&&[];return f?[t.createElement(f[1])]:(f=kr([n],t,e),e&&e.length&&i(e).remove(),i.merge([],f.childNodes))},di=i.fn.load,i.fn.load=function(n,t,r){if("string"!=typeof n&&di)return di.apply(this,arguments);var u,o,s,f=this,e=n.indexOf(" ");return e>-1&&(u=i.trim(n.slice(e)),n=n.slice(0,e)),i.isFunction(t)?(r=t,t=void 0):t&&"object"==typeof t&&(o="POST"),f.length>0&&i.ajax({url:n,type:o||"GET",dataType:"html",data:t}).done(function(n){s=arguments;f.html(u?i("<div>").append(i.parseHTML(n)).find(u):n)}).always(r&&function(n,t){f.each(function(){r.apply(this,s||[n.responseText,t,n])})}),this},i.each(["ajaxStart","ajaxStop","ajaxComplete","ajaxError","ajaxSuccess","ajaxSend"],function(n,t){i.fn[t]=function(n){return this.on(t,n)}}),i.expr.filters.animated=function(n){return i.grep(i.timers,function(t){return n===t.elem}).length},i.offset={setOffset:function(n,t,r){var e,o,s,h,u,c,v,l=i.css(n,"position"),a=i(n),f={};"static"===l&&(n.style.position="relative");u=a.offset();s=i.css(n,"top");c=i.css(n,"left");v=("absolute"===l||"fixed"===l)&&(s+c).indexOf("auto")>-1;v?(e=a.position(),h=e.top,o=e.left):(h=parseFloat(s)||0,o=parseFloat(c)||0);i.isFunction(t)&&(t=t.call(n,r,i.extend({},u)));null!=t.top&&(f.top=t.top-u.top+h);null!=t.left&&(f.left=t.left-u.left+o);"using"in t?t.using.call(n,f):a.css(f)}},i.fn.extend({offset:function(n){if(arguments.length)return void 0===n?this:this.each(function(t){i.offset.setOffset(this,n,t)});var t,f,r=this[0],u={top:0,left:0},e=r&&r.ownerDocument;if(e)return t=e.documentElement,i.contains(t,r)?(u=r.getBoundingClientRect(),f=hf(e),{top:u.top+f.pageYOffset-t.clientTop,left:u.left+f.pageXOffset-t.clientLeft}):u},position:function(){if(this[0]){var n,r,u=this[0],t={top:0,left:0};return"fixed"===i.css(u,"position")?r=u.getBoundingClientRect():(n=this.offsetParent(),r=this.offset(),i.nodeName(n[0],"html")||(t=n.offset()),t.top+=i.css(n[0],"borderTopWidth",!0),t.left+=i.css(n[0],"borderLeftWidth",!0)),{top:r.top-t.top-i.css(u,"marginTop",!0),left:r.left-t.left-i.css(u,"marginLeft",!0)}}},offsetParent:function(){return this.map(function(){for(var n=this.offsetParent;n&&"static"===i.css(n,"position");)n=n.offsetParent;return n||ht})}}),i.each({scrollLeft:"pageXOffset",scrollTop:"pageYOffset"},function(n,t){var r="pageYOffset"===t;i.fn[n]=function(i){return a(this,function(n,i,u){var f=hf(n);return void 0===u?f?f[t]:n[i]:void(f?f.scrollTo(r?f.pageXOffset:u,r?u:f.pageYOffset):n[i]=u)},n,i,arguments.length)}}),i.each(["top","left"],function(n,t){i.cssHooks[t]=ci(f.pixelPosition,function(n,r){if(r)return(r=tt(n,t),si.test(r)?i(n).position()[t]+"px":r)})}),i.each({Height:"height",Width:"width"},function(n,t){i.each({padding:"inner"+n,content:t,"":"outer"+n},function(r,u){i.fn[u]=function(u,f){var e=arguments.length&&(r||"boolean"!=typeof u),o=r||(u===!0||f===!0?"margin":"border");return a(this,function(t,r,u){var f;return i.isWindow(t)?t.document.documentElement["client"+n]:9===t.nodeType?(f=t.documentElement,Math.max(t.body["scroll"+n],f["scroll"+n],t.body["offset"+n],f["offset"+n],f["client"+n])):void 0===u?i.css(t,r,o):i.style(t,r,u,o)},t,e?u:void 0,e,null)}})}),i.fn.extend({bind:function(n,t,i){return this.on(n,null,t,i)},unbind:function(n,t){return this.off(n,null,t)},delegate:function(n,t,i,r){return this.on(t,n,i,r)},undelegate:function(n,t,i){return 1===arguments.length?this.off(n,"**"):this.off(t,n||"**",i)},size:function(){return this.length}}),i.fn.andSelf=i.fn.addBack,"function"==typeof define&&define.amd&&define("jquery",[],function(){return i}),cf=n.jQuery,lf=n.$,i.noConflict=function(t){return n.$===i&&(n.$=lf),t&&n.jQuery===i&&(n.jQuery=cf),i},t||(n.jQuery=n.$=i),i});
(function ($, undefined) { $.ui = $.ui || {}; if ($.ui.version) { return } $.extend($.ui, { version: "1.8.22", keyCode: { ALT: 18, BACKSPACE: 8, CAPS_LOCK: 20, COMMA: 188, COMMAND: 91, COMMAND_LEFT: 91, COMMAND_RIGHT: 93, CONTROL: 17, DELETE: 46, DOWN: 40, END: 35, ENTER: 13, ESCAPE: 27, HOME: 36, INSERT: 45, LEFT: 37, MENU: 93, NUMPAD_ADD: 107, NUMPAD_DECIMAL: 110, NUMPAD_DIVIDE: 111, NUMPAD_ENTER: 108, NUMPAD_MULTIPLY: 106, NUMPAD_SUBTRACT: 109, PAGE_DOWN: 34, PAGE_UP: 33, PERIOD: 190, RIGHT: 39, SHIFT: 16, SPACE: 32, TAB: 9, UP: 38, WINDOWS: 91 } }); $.fn.extend({ propAttr: $.fn.prop || $.fn.attr, _focus: $.fn.focus, focus: function (delay, fn) { return typeof delay === "number" ? this.each(function () { var elem = this; setTimeout(function () { $(elem).focus(); if (fn) { fn.call(elem) } }, delay) }) : this._focus.apply(this, arguments) }, scrollParent: function () { var scrollParent; if (($.browser.msie && (/(static|relative)/).test(this.css('position'))) || (/absolute/).test(this.css('position'))) { scrollParent = this.parents().filter(function () { return (/(relative|absolute|fixed)/).test($.curCSS(this, 'position', 1)) && (/(auto|scroll)/).test($.curCSS(this, 'overflow', 1) + $.curCSS(this, 'overflow-y', 1) + $.curCSS(this, 'overflow-x', 1)) }).eq(0) } else { scrollParent = this.parents().filter(function () { return (/(auto|scroll)/).test($.curCSS(this, 'overflow', 1) + $.curCSS(this, 'overflow-y', 1) + $.curCSS(this, 'overflow-x', 1)) }).eq(0) } return (/fixed/).test(this.css('position')) || !scrollParent.length ? $(document) : scrollParent }, zIndex: function (zIndex) { if (zIndex !== undefined) { return this.css("zIndex", zIndex) } if (this.length) { var elem = $(this[0]), position, value; while (elem.length && elem[0] !== document) { position = elem.css("position"); if (position === "absolute" || position === "relative" || position === "fixed") { value = parseInt(elem.css("zIndex"), 10); if (!isNaN(value) && value !== 0) { return value } } elem = elem.parent() } } return 0 }, disableSelection: function () { return this.bind(($.support.selectstart ? "selectstart" : "mousedown") + ".ui-disableSelection", function (event) { event.preventDefault() }) }, enableSelection: function () { return this.unbind(".ui-disableSelection") } }); if (!$("<a>").outerWidth(1).jquery) { $.each(["Width", "Height"], function (i, name) { var side = name === "Width" ? ["Left", "Right"] : ["Top", "Bottom"], type = name.toLowerCase(), orig = { innerWidth: $.fn.innerWidth, innerHeight: $.fn.innerHeight, outerWidth: $.fn.outerWidth, outerHeight: $.fn.outerHeight }; function reduce(elem, size, border, margin) { $.each(side, function () { size -= parseFloat($.curCSS(elem, "padding" + this, true)) || 0; if (border) { size -= parseFloat($.curCSS(elem, "border" + this + "Width", true)) || 0 } if (margin) { size -= parseFloat($.curCSS(elem, "margin" + this, true)) || 0 } }); return size } $.fn["inner" + name] = function (size) { if (size === undefined) { return orig["inner" + name].call(this) } return this.each(function () { $(this).css(type, reduce(this, size) + "px") }) }; $.fn["outer" + name] = function (size, margin) { if (typeof size !== "number") { return orig["outer" + name].call(this, size) } return this.each(function () { $(this).css(type, reduce(this, size, true, margin) + "px") }) } }) } function focusable(element, isTabIndexNotNaN) { var nodeName = element.nodeName.toLowerCase(); if ("area" === nodeName) { var map = element.parentNode, mapName = map.name, img; if (!element.href || !mapName || map.nodeName.toLowerCase() !== "map") { return false } img = $("img[usemap=#" + mapName + "]")[0]; return !!img && visible(img) } return (/input|select|textarea|button|object/.test(nodeName) ? !element.disabled : "a" == nodeName ? element.href || isTabIndexNotNaN : isTabIndexNotNaN) && visible(element) } function visible(element) { return !$(element).parents().andSelf().filter(function () { return $.curCSS(this, "visibility") === "hidden" || $.expr.filters.hidden(this) }).length } $.extend($.expr[":"], { data: $.expr.createPseudo ? $.expr.createPseudo(function (dataName) { return function (elem) { return !!$.data(elem, dataName) } }) : function (elem, i, match) { return !!$.data(elem, match[3]) }, focusable: function (element) { return focusable(element, !isNaN($.attr(element, "tabindex"))) }, tabbable: function (element) { var tabIndex = $.attr(element, "tabindex"), isTabIndexNaN = isNaN(tabIndex); return (isTabIndexNaN || tabIndex >= 0) && focusable(element, !isTabIndexNaN) } }); $(function () { var body = document.body, div = body.appendChild(div = document.createElement("div")); div.offsetHeight; $.extend(div.style, { minHeight: "100px", height: "auto", padding: 0, borderWidth: 0 }); $.support.minHeight = div.offsetHeight === 100; $.support.selectstart = "onselectstart" in div; body.removeChild(div).style.display = "none" }); if (!$.curCSS) { $.curCSS = $.css } $.extend($.ui, { plugin: { add: function (module, option, set) { var proto = $.ui[module].prototype; for (var i in set) { proto.plugins[i] = proto.plugins[i] || []; proto.plugins[i].push([option, set[i]]) } }, call: function (instance, name, args) { var set = instance.plugins[name]; if (!set || !instance.element[0].parentNode) { return } for (var i = 0; i < set.length; i++) { if (instance.options[set[i][0]]) { set[i][1].apply(instance.element, args) } } } }, contains: function (a, b) { return document.compareDocumentPosition ? a.compareDocumentPosition(b) & 16 : a !== b && a.contains(b) }, hasScroll: function (el, a) { if ($(el).css("overflow") === "hidden") { return false } var scroll = (a && a === "left") ? "scrollLeft" : "scrollTop", has = false; if (el[scroll] > 0) { return true } el[scroll] = 1; has = (el[scroll] > 0); el[scroll] = 0; return has }, isOverAxis: function (x, reference, size) { return (x > reference) && (x < (reference + size)) }, isOver: function (y, x, top, left, height, width) { return $.ui.isOverAxis(y, top, height) && $.ui.isOverAxis(x, left, width) } }) })(jQuery); (function ($, undefined) { if ($.cleanData) { var _cleanData = $.cleanData; $.cleanData = function (elems) { for (var i = 0, elem; (elem = elems[i]) != null; i++) { try { $(elem).triggerHandler("remove") } catch (e) { } } _cleanData(elems) } } else { var _remove = $.fn.remove; $.fn.remove = function (selector, keepData) { return this.each(function () { if (!keepData) { if (!selector || $.filter(selector, [this]).length) { $("*", this).add([this]).each(function () { try { $(this).triggerHandler("remove") } catch (e) { } }) } } return _remove.call($(this), selector, keepData) }) } } $.widget = function (name, base, prototype) { var namespace = name.split(".")[0], fullName; name = name.split(".")[1]; fullName = namespace + "-" + name; if (!prototype) { prototype = base; base = $.Widget } $.expr[":"][fullName] = function (elem) { return !!$.data(elem, name) }; $[namespace] = $[namespace] || {}; $[namespace][name] = function (options, element) { if (arguments.length) { this._createWidget(options, element) } }; var basePrototype = new base(); basePrototype.options = $.extend(true, {}, basePrototype.options); $[namespace][name].prototype = $.extend(true, basePrototype, { namespace: namespace, widgetName: name, widgetEventPrefix: $[namespace][name].prototype.widgetEventPrefix || name, widgetBaseClass: fullName }, prototype); $.widget.bridge(name, $[namespace][name]) }; $.widget.bridge = function (name, object) { $.fn[name] = function (options) { var isMethodCall = typeof options === "string", args = Array.prototype.slice.call(arguments, 1), returnValue = this; options = !isMethodCall && args.length ? $.extend.apply(null, [true, options].concat(args)) : options; if (isMethodCall && options.charAt(0) === "_") { return returnValue } if (isMethodCall) { this.each(function () { var instance = $.data(this, name), methodValue = instance && $.isFunction(instance[options]) ? instance[options].apply(instance, args) : instance; if (methodValue !== instance && methodValue !== undefined) { returnValue = methodValue; return false } }) } else { this.each(function () { var instance = $.data(this, name); if (instance) { instance.option(options || {})._init() } else { $.data(this, name, new object(options, this)) } }) } return returnValue } }; $.Widget = function (options, element) { if (arguments.length) { this._createWidget(options, element) } }; $.Widget.prototype = { widgetName: "widget", widgetEventPrefix: "", options: { disabled: false }, _createWidget: function (options, element) { $.data(element, this.widgetName, this); this.element = $(element); this.options = $.extend(true, {}, this.options, this._getCreateOptions(), options); var self = this; this.element.bind("remove." + this.widgetName, function () { self.destroy() }); this._create(); this._trigger("create"); this._init() }, _getCreateOptions: function () { return $.metadata && $.metadata.get(this.element[0])[this.widgetName] }, _create: function () { }, _init: function () { }, destroy: function () { this.element.unbind("." + this.widgetName).removeData(this.widgetName); this.widget().unbind("." + this.widgetName).removeAttr("aria-disabled").removeClass(this.widgetBaseClass + "-disabled " + "ui-state-disabled") }, widget: function () { return this.element }, option: function (key, value) { var options = key; if (arguments.length === 0) { return $.extend({}, this.options) } if (typeof key === "string") { if (value === undefined) { return this.options[key] } options = {}; options[key] = value } this._setOptions(options); return this }, _setOptions: function (options) { var self = this; $.each(options, function (key, value) { self._setOption(key, value) }); return this }, _setOption: function (key, value) { this.options[key] = value; if (key === "disabled") { this.widget()[value ? "addClass" : "removeClass"](this.widgetBaseClass + "-disabled" + " " + "ui-state-disabled").attr("aria-disabled", value) } return this }, enable: function () { return this._setOption("disabled", false) }, disable: function () { return this._setOption("disabled", true) }, _trigger: function (type, event, data) { var prop, orig, callback = this.options[type]; data = data || {}; event = $.Event(event); event.type = (type === this.widgetEventPrefix ? type : this.widgetEventPrefix + type).toLowerCase(); event.target = this.element[0]; orig = event.originalEvent; if (orig) { for (prop in orig) { if (!(prop in event)) { event[prop] = orig[prop] } } } this.element.trigger(event, data); return !($.isFunction(callback) && callback.call(this.element[0], event, data) === false || event.isDefaultPrevented()) } } })(jQuery); (function ($, undefined) { var mouseHandled = false; $(document).mouseup(function (e) { mouseHandled = false }); $.widget("ui.mouse", { options: { cancel: ':input,option', distance: 1, delay: 0 }, _mouseInit: function () { var self = this; this.element.bind('mousedown.' + this.widgetName, function (event) { return self._mouseDown(event) }).bind('click.' + this.widgetName, function (event) { if (true === $.data(event.target, self.widgetName + '.preventClickEvent')) { $.removeData(event.target, self.widgetName + '.preventClickEvent'); event.stopImmediatePropagation(); return false } }); this.started = false }, _mouseDestroy: function () { this.element.unbind('.' + this.widgetName); $(document).unbind('mousemove.' + this.widgetName, this._mouseMoveDelegate).unbind('mouseup.' + this.widgetName, this._mouseUpDelegate) }, _mouseDown: function (event) { if (mouseHandled) { return }; (this._mouseStarted && this._mouseUp(event)); this._mouseDownEvent = event; var self = this, btnIsLeft = (event.which == 1), elIsCancel = (typeof this.options.cancel == "string" && event.target.nodeName ? $(event.target).closest(this.options.cancel).length : false); if (!btnIsLeft || elIsCancel || !this._mouseCapture(event)) { return true } this.mouseDelayMet = !this.options.delay; if (!this.mouseDelayMet) { this._mouseDelayTimer = setTimeout(function () { self.mouseDelayMet = true }, this.options.delay) } if (this._mouseDistanceMet(event) && this._mouseDelayMet(event)) { this._mouseStarted = (this._mouseStart(event) !== false); if (!this._mouseStarted) { event.preventDefault(); return true } } if (true === $.data(event.target, this.widgetName + '.preventClickEvent')) { $.removeData(event.target, this.widgetName + '.preventClickEvent') } this._mouseMoveDelegate = function (event) { return self._mouseMove(event) }; this._mouseUpDelegate = function (event) { return self._mouseUp(event) }; $(document).bind('mousemove.' + this.widgetName, this._mouseMoveDelegate).bind('mouseup.' + this.widgetName, this._mouseUpDelegate); event.preventDefault(); mouseHandled = true; return true }, _mouseMove: function (event) { if ($.browser.msie && !(document.documentMode >= 9) && !event.button) { return this._mouseUp(event) } if (this._mouseStarted) { this._mouseDrag(event); return event.preventDefault() } if (this._mouseDistanceMet(event) && this._mouseDelayMet(event)) { this._mouseStarted = (this._mouseStart(this._mouseDownEvent, event) !== false); (this._mouseStarted ? this._mouseDrag(event) : this._mouseUp(event)) } return !this._mouseStarted }, _mouseUp: function (event) { $(document).unbind('mousemove.' + this.widgetName, this._mouseMoveDelegate).unbind('mouseup.' + this.widgetName, this._mouseUpDelegate); if (this._mouseStarted) { this._mouseStarted = false; if (event.target == this._mouseDownEvent.target) { $.data(event.target, this.widgetName + '.preventClickEvent', true) } this._mouseStop(event) } return false }, _mouseDistanceMet: function (event) { return (Math.max(Math.abs(this._mouseDownEvent.pageX - event.pageX), Math.abs(this._mouseDownEvent.pageY - event.pageY)) >= this.options.distance) }, _mouseDelayMet: function (event) { return this.mouseDelayMet }, _mouseStart: function (event) { }, _mouseDrag: function (event) { }, _mouseStop: function (event) { }, _mouseCapture: function (event) { return true } }) })(jQuery); (function ($, undefined) { $.widget("ui.draggable", $.ui.mouse, { widgetEventPrefix: "drag", options: { addClasses: true, appendTo: "parent", axis: false, connectToSortable: false, containment: false, cursor: "auto", cursorAt: false, grid: false, handle: false, helper: "original", iframeFix: false, opacity: false, refreshPositions: false, revert: false, revertDuration: 500, scope: "default", scroll: true, scrollSensitivity: 20, scrollSpeed: 20, snap: false, snapMode: "both", snapTolerance: 20, stack: false, zIndex: false }, _create: function () { if (this.options.helper == 'original' && !(/^(?:r|a|f)/).test(this.element.css("position"))) this.element[0].style.position = 'relative'; (this.options.addClasses && this.element.addClass("ui-draggable")); (this.options.disabled && this.element.addClass("ui-draggable-disabled")); this._mouseInit() }, destroy: function () { if (!this.element.data('draggable')) return; this.element.removeData("draggable").unbind(".draggable").removeClass("ui-draggable" + " ui-draggable-dragging" + " ui-draggable-disabled"); this._mouseDestroy(); return this }, _mouseCapture: function (event) { var o = this.options; if (this.helper || o.disabled || $(event.target).is('.ui-resizable-handle')) return false; this.handle = this._getHandle(event); if (!this.handle) return false; if (o.iframeFix) { $(o.iframeFix === true ? "iframe" : o.iframeFix).each(function () { $('<div class="ui-draggable-iframeFix" style="background: #fff;"></div>').css({ width: this.offsetWidth + "px", height: this.offsetHeight + "px", position: "absolute", opacity: "0.001", zIndex: 1000 }).css($(this).offset()).appendTo("body") }) } return true }, _mouseStart: function (event) { var o = this.options; this.helper = this._createHelper(event); this.helper.addClass("ui-draggable-dragging"); this._cacheHelperProportions(); if ($.ui.ddmanager) $.ui.ddmanager.current = this; this._cacheMargins(); this.cssPosition = this.helper.css("position"); this.scrollParent = this.helper.scrollParent(); this.offset = this.positionAbs = this.element.offset(); this.offset = { top: this.offset.top - this.margins.top, left: this.offset.left - this.margins.left }; $.extend(this.offset, { click: { left: event.pageX - this.offset.left, top: event.pageY - this.offset.top }, parent: this._getParentOffset(), relative: this._getRelativeOffset() }); this.originalPosition = this.position = this._generatePosition(event); this.originalPageX = event.pageX; this.originalPageY = event.pageY; (o.cursorAt && this._adjustOffsetFromHelper(o.cursorAt)); if (o.containment) this._setContainment(); if (this._trigger("start", event) === false) { this._clear(); return false } this._cacheHelperProportions(); if ($.ui.ddmanager && !o.dropBehaviour) $.ui.ddmanager.prepareOffsets(this, event); this._mouseDrag(event, true); if ($.ui.ddmanager) $.ui.ddmanager.dragStart(this, event); return true }, _mouseDrag: function (event, noPropagation) { this.position = this._generatePosition(event); this.positionAbs = this._convertPositionTo("absolute"); if (!noPropagation) { var ui = this._uiHash(); if (this._trigger('drag', event, ui) === false) { this._mouseUp({}); return false } this.position = ui.position } if (!this.options.axis || this.options.axis != "y") this.helper[0].style.left = this.position.left + 'px'; if (!this.options.axis || this.options.axis != "x") this.helper[0].style.top = this.position.top + 'px'; if ($.ui.ddmanager) $.ui.ddmanager.drag(this, event); return false }, _mouseStop: function (event) { var dropped = false; if ($.ui.ddmanager && !this.options.dropBehaviour) dropped = $.ui.ddmanager.drop(this, event); if (this.dropped) { dropped = this.dropped; this.dropped = false } var element = this.element[0], elementInDom = false; while (element && (element = element.parentNode)) { if (element == document) { elementInDom = true } } if (!elementInDom && this.options.helper === "original") return false; if ((this.options.revert == "invalid" && !dropped) || (this.options.revert == "valid" && dropped) || this.options.revert === true || ($.isFunction(this.options.revert) && this.options.revert.call(this.element, dropped))) { var self = this; $(this.helper).animate(this.originalPosition, parseInt(this.options.revertDuration, 10), function () { if (self._trigger("stop", event) !== false) { self._clear() } }) } else { if (this._trigger("stop", event) !== false) { this._clear() } } return false }, _mouseUp: function (event) { if (this.options.iframeFix === true) { $("div.ui-draggable-iframeFix").each(function () { this.parentNode.removeChild(this) }) } if ($.ui.ddmanager) $.ui.ddmanager.dragStop(this, event); return $.ui.mouse.prototype._mouseUp.call(this, event) }, cancel: function () { if (this.helper.is(".ui-draggable-dragging")) { this._mouseUp({}) } else { this._clear() } return this }, _getHandle: function (event) { var handle = !this.options.handle || !$(this.options.handle, this.element).length ? true : false; $(this.options.handle, this.element).find("*").andSelf().each(function () { if (this == event.target) handle = true }); return handle }, _createHelper: function (event) { var o = this.options; var helper = $.isFunction(o.helper) ? $(o.helper.apply(this.element[0], [event])) : (o.helper == 'clone' ? this.element.clone().removeAttr('id') : this.element); if (!helper.parents('body').length) helper.appendTo((o.appendTo == 'parent' ? this.element[0].parentNode : o.appendTo)); if (helper[0] != this.element[0] && !(/(fixed|absolute)/).test(helper.css("position"))) helper.css("position", "absolute"); return helper }, _adjustOffsetFromHelper: function (obj) { if (typeof obj == 'string') { obj = obj.split(' ') } if ($.isArray(obj)) { obj = { left: +obj[0], top: +obj[1] || 0 } } if ('left' in obj) { this.offset.click.left = obj.left + this.margins.left } if ('right' in obj) { this.offset.click.left = this.helperProportions.width - obj.right + this.margins.left } if ('top' in obj) { this.offset.click.top = obj.top + this.margins.top } if ('bottom' in obj) { this.offset.click.top = this.helperProportions.height - obj.bottom + this.margins.top } }, _getParentOffset: function () { this.offsetParent = this.helper.offsetParent(); var po = this.offsetParent.offset(); if (this.cssPosition == 'absolute' && this.scrollParent[0] != document && $.ui.contains(this.scrollParent[0], this.offsetParent[0])) { po.left += this.scrollParent.scrollLeft(); po.top += this.scrollParent.scrollTop() } if ((this.offsetParent[0] == document.body) || (this.offsetParent[0].tagName && this.offsetParent[0].tagName.toLowerCase() == 'html' && $.browser.msie)) po = { top: 0, left: 0 }; return { top: po.top + (parseInt(this.offsetParent.css("borderTopWidth"), 10) || 0), left: po.left + (parseInt(this.offsetParent.css("borderLeftWidth"), 10) || 0) } }, _getRelativeOffset: function () { if (this.cssPosition == "relative") { var p = this.element.position(); return { top: p.top - (parseInt(this.helper.css("top"), 10) || 0) + this.scrollParent.scrollTop(), left: p.left - (parseInt(this.helper.css("left"), 10) || 0) + this.scrollParent.scrollLeft() } } else { return { top: 0, left: 0 } } }, _cacheMargins: function () { this.margins = { left: (parseInt(this.element.css("marginLeft"), 10) || 0), top: (parseInt(this.element.css("marginTop"), 10) || 0), right: (parseInt(this.element.css("marginRight"), 10) || 0), bottom: (parseInt(this.element.css("marginBottom"), 10) || 0) } }, _cacheHelperProportions: function () { this.helperProportions = { width: this.helper.outerWidth(), height: this.helper.outerHeight() } }, _setContainment: function () { var o = this.options; if (o.containment == 'parent') o.containment = this.helper[0].parentNode; if (o.containment == 'document' || o.containment == 'window') this.containment = [o.containment == 'document' ? 0 : $(window).scrollLeft() - this.offset.relative.left - this.offset.parent.left, o.containment == 'document' ? 0 : $(window).scrollTop() - this.offset.relative.top - this.offset.parent.top, (o.containment == 'document' ? 0 : $(window).scrollLeft()) + $(o.containment == 'document' ? document : window).width() - this.helperProportions.width - this.margins.left, (o.containment == 'document' ? 0 : $(window).scrollTop()) + ($(o.containment == 'document' ? document : window).height() || document.body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top]; if (!(/^(document|window|parent)$/).test(o.containment) && o.containment.constructor != Array) { var c = $(o.containment); var ce = c[0]; if (!ce) return; var co = c.offset(); var over = ($(ce).css("overflow") != 'hidden'); this.containment = [(parseInt($(ce).css("borderLeftWidth"), 10) || 0) + (parseInt($(ce).css("paddingLeft"), 10) || 0), (parseInt($(ce).css("borderTopWidth"), 10) || 0) + (parseInt($(ce).css("paddingTop"), 10) || 0), (over ? Math.max(ce.scrollWidth, ce.offsetWidth) : ce.offsetWidth) - (parseInt($(ce).css("borderLeftWidth"), 10) || 0) - (parseInt($(ce).css("paddingRight"), 10) || 0) - this.helperProportions.width - this.margins.left - this.margins.right, (over ? Math.max(ce.scrollHeight, ce.offsetHeight) : ce.offsetHeight) - (parseInt($(ce).css("borderTopWidth"), 10) || 0) - (parseInt($(ce).css("paddingBottom"), 10) || 0) - this.helperProportions.height - this.margins.top - this.margins.bottom]; this.relative_container = c } else if (o.containment.constructor == Array) { this.containment = o.containment } }, _convertPositionTo: function (d, pos) { if (!pos) pos = this.position; var mod = d == "absolute" ? 1 : -1; var o = this.options, scroll = this.cssPosition == 'absolute' && !(this.scrollParent[0] != document && $.ui.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent, scrollIsRootNode = (/(html|body)/i).test(scroll[0].tagName); return { top: (pos.top + this.offset.relative.top * mod + this.offset.parent.top * mod - ($.browser.safari && $.browser.version < 526 && this.cssPosition == 'fixed' ? 0 : (this.cssPosition == 'fixed' ? -this.scrollParent.scrollTop() : (scrollIsRootNode ? 0 : scroll.scrollTop())) * mod)), left: (pos.left + this.offset.relative.left * mod + this.offset.parent.left * mod - ($.browser.safari && $.browser.version < 526 && this.cssPosition == 'fixed' ? 0 : (this.cssPosition == 'fixed' ? -this.scrollParent.scrollLeft() : scrollIsRootNode ? 0 : scroll.scrollLeft()) * mod)) } }, _generatePosition: function (event) { var o = this.options, scroll = this.cssPosition == 'absolute' && !(this.scrollParent[0] != document && $.ui.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent, scrollIsRootNode = (/(html|body)/i).test(scroll[0].tagName); var pageX = event.pageX; var pageY = event.pageY; if (this.originalPosition) { var containment; if (this.containment) { if (this.relative_container) { var co = this.relative_container.offset(); containment = [this.containment[0] + co.left, this.containment[1] + co.top, this.containment[2] + co.left, this.containment[3] + co.top] } else { containment = this.containment } if (event.pageX - this.offset.click.left < containment[0]) pageX = containment[0] + this.offset.click.left; if (event.pageY - this.offset.click.top < containment[1]) pageY = containment[1] + this.offset.click.top; if (event.pageX - this.offset.click.left > containment[2]) pageX = containment[2] + this.offset.click.left; if (event.pageY - this.offset.click.top > containment[3]) pageY = containment[3] + this.offset.click.top } if (o.grid) { var top = o.grid[1] ? this.originalPageY + Math.round((pageY - this.originalPageY) / o.grid[1]) * o.grid[1] : this.originalPageY; pageY = containment ? (!(top - this.offset.click.top < containment[1] || top - this.offset.click.top > containment[3]) ? top : (!(top - this.offset.click.top < containment[1]) ? top - o.grid[1] : top + o.grid[1])) : top; var left = o.grid[0] ? this.originalPageX + Math.round((pageX - this.originalPageX) / o.grid[0]) * o.grid[0] : this.originalPageX; pageX = containment ? (!(left - this.offset.click.left < containment[0] || left - this.offset.click.left > containment[2]) ? left : (!(left - this.offset.click.left < containment[0]) ? left - o.grid[0] : left + o.grid[0])) : left } } return { top: (pageY - this.offset.click.top - this.offset.relative.top - this.offset.parent.top + ($.browser.safari && $.browser.version < 526 && this.cssPosition == 'fixed' ? 0 : (this.cssPosition == 'fixed' ? -this.scrollParent.scrollTop() : (scrollIsRootNode ? 0 : scroll.scrollTop())))), left: (pageX - this.offset.click.left - this.offset.relative.left - this.offset.parent.left + ($.browser.safari && $.browser.version < 526 && this.cssPosition == 'fixed' ? 0 : (this.cssPosition == 'fixed' ? -this.scrollParent.scrollLeft() : scrollIsRootNode ? 0 : scroll.scrollLeft()))) } }, _clear: function () { this.helper.removeClass("ui-draggable-dragging"); if (this.helper[0] != this.element[0] && !this.cancelHelperRemoval) this.helper.remove(); this.helper = null; this.cancelHelperRemoval = false }, _trigger: function (type, event, ui) { ui = ui || this._uiHash(); $.ui.plugin.call(this, type, [event, ui]); if (type == "drag") this.positionAbs = this._convertPositionTo("absolute"); return $.Widget.prototype._trigger.call(this, type, event, ui) }, plugins: {}, _uiHash: function (event) { return { helper: this.helper, position: this.position, originalPosition: this.originalPosition, offset: this.positionAbs } } }); $.extend($.ui.draggable, { version: "1.8.22" }); $.ui.plugin.add("draggable", "connectToSortable", { start: function (event, ui) { var inst = $(this).data("draggable"), o = inst.options, uiSortable = $.extend({}, ui, { item: inst.element }); inst.sortables = []; $(o.connectToSortable).each(function () { var sortable = $.data(this, 'sortable'); if (sortable && !sortable.options.disabled) { inst.sortables.push({ instance: sortable, shouldRevert: sortable.options.revert }); sortable.refreshPositions(); sortable._trigger("activate", event, uiSortable) } }) }, stop: function (event, ui) { var inst = $(this).data("draggable"), uiSortable = $.extend({}, ui, { item: inst.element }); $.each(inst.sortables, function () { if (this.instance.isOver) { this.instance.isOver = 0; inst.cancelHelperRemoval = true; this.instance.cancelHelperRemoval = false; if (this.shouldRevert) this.instance.options.revert = true; this.instance._mouseStop(event); this.instance.options.helper = this.instance.options._helper; if (inst.options.helper == 'original') this.instance.currentItem.css({ top: 'auto', left: 'auto' }) } else { this.instance.cancelHelperRemoval = false; this.instance._trigger("deactivate", event, uiSortable) } }) }, drag: function (event, ui) { var inst = $(this).data("draggable"), self = this; var checkPos = function (o) { var dyClick = this.offset.click.top, dxClick = this.offset.click.left; var helperTop = this.positionAbs.top, helperLeft = this.positionAbs.left; var itemHeight = o.height, itemWidth = o.width; var itemTop = o.top, itemLeft = o.left; return $.ui.isOver(helperTop + dyClick, helperLeft + dxClick, itemTop, itemLeft, itemHeight, itemWidth) }; $.each(inst.sortables, function (i) { this.instance.positionAbs = inst.positionAbs; this.instance.helperProportions = inst.helperProportions; this.instance.offset.click = inst.offset.click; if (this.instance._intersectsWith(this.instance.containerCache)) { if (!this.instance.isOver) { this.instance.isOver = 1; this.instance.currentItem = $(self).clone().removeAttr('id').appendTo(this.instance.element).data("sortable-item", true); this.instance.options._helper = this.instance.options.helper; this.instance.options.helper = function () { return ui.helper[0] }; event.target = this.instance.currentItem[0]; this.instance._mouseCapture(event, true); this.instance._mouseStart(event, true, true); this.instance.offset.click.top = inst.offset.click.top; this.instance.offset.click.left = inst.offset.click.left; this.instance.offset.parent.left -= inst.offset.parent.left - this.instance.offset.parent.left; this.instance.offset.parent.top -= inst.offset.parent.top - this.instance.offset.parent.top; inst._trigger("toSortable", event); inst.dropped = this.instance.element; inst.currentItem = inst.element; this.instance.fromOutside = inst } if (this.instance.currentItem) this.instance._mouseDrag(event) } else { if (this.instance.isOver) { this.instance.isOver = 0; this.instance.cancelHelperRemoval = true; this.instance.options.revert = false; this.instance._trigger('out', event, this.instance._uiHash(this.instance)); this.instance._mouseStop(event, true); this.instance.options.helper = this.instance.options._helper; this.instance.currentItem.remove(); if (this.instance.placeholder) this.instance.placeholder.remove(); inst._trigger("fromSortable", event); inst.dropped = false } } }) } }); $.ui.plugin.add("draggable", "cursor", { start: function (event, ui) { var t = $('body'), o = $(this).data('draggable').options; if (t.css("cursor")) o._cursor = t.css("cursor"); t.css("cursor", o.cursor) }, stop: function (event, ui) { var o = $(this).data('draggable').options; if (o._cursor) $('body').css("cursor", o._cursor) } }); $.ui.plugin.add("draggable", "opacity", { start: function (event, ui) { var t = $(ui.helper), o = $(this).data('draggable').options; if (t.css("opacity")) o._opacity = t.css("opacity"); t.css('opacity', o.opacity) }, stop: function (event, ui) { var o = $(this).data('draggable').options; if (o._opacity) $(ui.helper).css('opacity', o._opacity) } }); $.ui.plugin.add("draggable", "scroll", { start: function (event, ui) { var i = $(this).data("draggable"); if (i.scrollParent[0] != document && i.scrollParent[0].tagName != 'HTML') i.overflowOffset = i.scrollParent.offset() }, drag: function (event, ui) { var i = $(this).data("draggable"), o = i.options, scrolled = false; if (i.scrollParent[0] != document && i.scrollParent[0].tagName != 'HTML') { if (!o.axis || o.axis != 'x') { if ((i.overflowOffset.top + i.scrollParent[0].offsetHeight) - event.pageY < o.scrollSensitivity) i.scrollParent[0].scrollTop = scrolled = i.scrollParent[0].scrollTop + o.scrollSpeed; else if (event.pageY - i.overflowOffset.top < o.scrollSensitivity) i.scrollParent[0].scrollTop = scrolled = i.scrollParent[0].scrollTop - o.scrollSpeed } if (!o.axis || o.axis != 'y') { if ((i.overflowOffset.left + i.scrollParent[0].offsetWidth) - event.pageX < o.scrollSensitivity) i.scrollParent[0].scrollLeft = scrolled = i.scrollParent[0].scrollLeft + o.scrollSpeed; else if (event.pageX - i.overflowOffset.left < o.scrollSensitivity) i.scrollParent[0].scrollLeft = scrolled = i.scrollParent[0].scrollLeft - o.scrollSpeed } } else { if (!o.axis || o.axis != 'x') { if (event.pageY - $(document).scrollTop() < o.scrollSensitivity) scrolled = $(document).scrollTop($(document).scrollTop() - o.scrollSpeed); else if ($(window).height() - (event.pageY - $(document).scrollTop()) < o.scrollSensitivity) scrolled = $(document).scrollTop($(document).scrollTop() + o.scrollSpeed) } if (!o.axis || o.axis != 'y') { if (event.pageX - $(document).scrollLeft() < o.scrollSensitivity) scrolled = $(document).scrollLeft($(document).scrollLeft() - o.scrollSpeed); else if ($(window).width() - (event.pageX - $(document).scrollLeft()) < o.scrollSensitivity) scrolled = $(document).scrollLeft($(document).scrollLeft() + o.scrollSpeed) } } if (scrolled !== false && $.ui.ddmanager && !o.dropBehaviour) $.ui.ddmanager.prepareOffsets(i, event) } }); $.ui.plugin.add("draggable", "snap", { start: function (event, ui) { var i = $(this).data("draggable"), o = i.options; i.snapElements = []; $(o.snap.constructor != String ? (o.snap.items || ':data(draggable)') : o.snap).each(function () { var $t = $(this); var $o = $t.offset(); if (this != i.element[0]) i.snapElements.push({ item: this, width: $t.outerWidth(), height: $t.outerHeight(), top: $o.top, left: $o.left }) }) }, drag: function (event, ui) { var inst = $(this).data("draggable"), o = inst.options; var d = o.snapTolerance; var x1 = ui.offset.left, x2 = x1 + inst.helperProportions.width, y1 = ui.offset.top, y2 = y1 + inst.helperProportions.height; for (var i = inst.snapElements.length - 1; i >= 0; i--) { var l = inst.snapElements[i].left, r = l + inst.snapElements[i].width, t = inst.snapElements[i].top, b = t + inst.snapElements[i].height; if (!((l - d < x1 && x1 < r + d && t - d < y1 && y1 < b + d) || (l - d < x1 && x1 < r + d && t - d < y2 && y2 < b + d) || (l - d < x2 && x2 < r + d && t - d < y1 && y1 < b + d) || (l - d < x2 && x2 < r + d && t - d < y2 && y2 < b + d))) { if (inst.snapElements[i].snapping) (inst.options.snap.release && inst.options.snap.release.call(inst.element, event, $.extend(inst._uiHash(), { snapItem: inst.snapElements[i].item }))); inst.snapElements[i].snapping = false; continue } if (o.snapMode != 'inner') { var ts = Math.abs(t - y2) <= d; var bs = Math.abs(b - y1) <= d; var ls = Math.abs(l - x2) <= d; var rs = Math.abs(r - x1) <= d; if (ts) ui.position.top = inst._convertPositionTo("relative", { top: t - inst.helperProportions.height, left: 0 }).top - inst.margins.top; if (bs) ui.position.top = inst._convertPositionTo("relative", { top: b, left: 0 }).top - inst.margins.top; if (ls) ui.position.left = inst._convertPositionTo("relative", { top: 0, left: l - inst.helperProportions.width }).left - inst.margins.left; if (rs) ui.position.left = inst._convertPositionTo("relative", { top: 0, left: r }).left - inst.margins.left } var first = (ts || bs || ls || rs); if (o.snapMode != 'outer') { var ts = Math.abs(t - y1) <= d; var bs = Math.abs(b - y2) <= d; var ls = Math.abs(l - x1) <= d; var rs = Math.abs(r - x2) <= d; if (ts) ui.position.top = inst._convertPositionTo("relative", { top: t, left: 0 }).top - inst.margins.top; if (bs) ui.position.top = inst._convertPositionTo("relative", { top: b - inst.helperProportions.height, left: 0 }).top - inst.margins.top; if (ls) ui.position.left = inst._convertPositionTo("relative", { top: 0, left: l }).left - inst.margins.left; if (rs) ui.position.left = inst._convertPositionTo("relative", { top: 0, left: r - inst.helperProportions.width }).left - inst.margins.left } if (!inst.snapElements[i].snapping && (ts || bs || ls || rs || first)) (inst.options.snap.snap && inst.options.snap.snap.call(inst.element, event, $.extend(inst._uiHash(), { snapItem: inst.snapElements[i].item }))); inst.snapElements[i].snapping = (ts || bs || ls || rs || first) } } }); $.ui.plugin.add("draggable", "stack", { start: function (event, ui) { var o = $(this).data("draggable").options; var group = $.makeArray($(o.stack)).sort(function (a, b) { return (parseInt($(a).css("zIndex"), 10) || 0) - (parseInt($(b).css("zIndex"), 10) || 0) }); if (!group.length) { return } var min = parseInt(group[0].style.zIndex) || 0; $(group).each(function (i) { this.style.zIndex = min + i }); this[0].style.zIndex = min + group.length } }); $.ui.plugin.add("draggable", "zIndex", { start: function (event, ui) { var t = $(ui.helper), o = $(this).data("draggable").options; if (t.css("zIndex")) o._zIndex = t.css("zIndex"); t.css('zIndex', o.zIndex) }, stop: function (event, ui) { var o = $(this).data("draggable").options; if (o._zIndex) $(ui.helper).css('zIndex', o._zIndex) } }) })(jQuery); (function ($, undefined) { $.widget("ui.droppable", { widgetEventPrefix: "drop", options: { accept: '*', activeClass: false, addClasses: true, greedy: false, hoverClass: false, scope: 'default', tolerance: 'intersect' }, _create: function () { var o = this.options, accept = o.accept; this.isover = 0; this.isout = 1; this.accept = $.isFunction(accept) ? accept : function (d) { return d.is(accept) }; this.proportions = { width: this.element[0].offsetWidth, height: this.element[0].offsetHeight }; $.ui.ddmanager.droppables[o.scope] = $.ui.ddmanager.droppables[o.scope] || []; $.ui.ddmanager.droppables[o.scope].push(this); (o.addClasses && this.element.addClass("ui-droppable")) }, destroy: function () { var drop = $.ui.ddmanager.droppables[this.options.scope]; for (var i = 0; i < drop.length; i++) if (drop[i] == this) drop.splice(i, 1); this.element.removeClass("ui-droppable ui-droppable-disabled").removeData("droppable").unbind(".droppable"); return this }, _setOption: function (key, value) { if (key == 'accept') { this.accept = $.isFunction(value) ? value : function (d) { return d.is(value) } } $.Widget.prototype._setOption.apply(this, arguments) }, _activate: function (event) { var draggable = $.ui.ddmanager.current; if (this.options.activeClass) this.element.addClass(this.options.activeClass); (draggable && this._trigger('activate', event, this.ui(draggable))) }, _deactivate: function (event) { var draggable = $.ui.ddmanager.current; if (this.options.activeClass) this.element.removeClass(this.options.activeClass); (draggable && this._trigger('deactivate', event, this.ui(draggable))) }, _over: function (event) { var draggable = $.ui.ddmanager.current; if (!draggable || (draggable.currentItem || draggable.element)[0] == this.element[0]) return; if (this.accept.call(this.element[0], (draggable.currentItem || draggable.element))) { if (this.options.hoverClass) this.element.addClass(this.options.hoverClass); this._trigger('over', event, this.ui(draggable)) } }, _out: function (event) { var draggable = $.ui.ddmanager.current; if (!draggable || (draggable.currentItem || draggable.element)[0] == this.element[0]) return; if (this.accept.call(this.element[0], (draggable.currentItem || draggable.element))) { if (this.options.hoverClass) this.element.removeClass(this.options.hoverClass); this._trigger('out', event, this.ui(draggable)) } }, _drop: function (event, custom) { var draggable = custom || $.ui.ddmanager.current; if (!draggable || (draggable.currentItem || draggable.element)[0] == this.element[0]) return false; var childrenIntersection = false; this.element.find(":data(droppable)").not(".ui-draggable-dragging").each(function () { var inst = $.data(this, 'droppable'); if (inst.options.greedy && !inst.options.disabled && inst.options.scope == draggable.options.scope && inst.accept.call(inst.element[0], (draggable.currentItem || draggable.element)) && $.ui.intersect(draggable, $.extend(inst, { offset: inst.element.offset() }), inst.options.tolerance)) { childrenIntersection = true; return false } }); if (childrenIntersection) return false; if (this.accept.call(this.element[0], (draggable.currentItem || draggable.element))) { if (this.options.activeClass) this.element.removeClass(this.options.activeClass); if (this.options.hoverClass) this.element.removeClass(this.options.hoverClass); this._trigger('drop', event, this.ui(draggable)); return this.element } return false }, ui: function (c) { return { draggable: (c.currentItem || c.element), helper: c.helper, position: c.position, offset: c.positionAbs } } }); $.extend($.ui.droppable, { version: "1.8.22" }); $.ui.intersect = function (draggable, droppable, toleranceMode) { if (!droppable.offset) return false; var x1 = (draggable.positionAbs || draggable.position.absolute).left, x2 = x1 + draggable.helperProportions.width, y1 = (draggable.positionAbs || draggable.position.absolute).top, y2 = y1 + draggable.helperProportions.height; var l = droppable.offset.left, r = l + droppable.proportions.width, t = droppable.offset.top, b = t + droppable.proportions.height; switch (toleranceMode) { case 'fit': return (l <= x1 && x2 <= r && t <= y1 && y2 <= b); break; case 'intersect': return (l < x1 + (draggable.helperProportions.width / 2) && x2 - (draggable.helperProportions.width / 2) < r && t < y1 + (draggable.helperProportions.height / 2) && y2 - (draggable.helperProportions.height / 2) < b); break; case 'pointer': var draggableLeft = ((draggable.positionAbs || draggable.position.absolute).left + (draggable.clickOffset || draggable.offset.click).left), draggableTop = ((draggable.positionAbs || draggable.position.absolute).top + (draggable.clickOffset || draggable.offset.click).top), isOver = $.ui.isOver(draggableTop, draggableLeft, t, l, droppable.proportions.height, droppable.proportions.width); return isOver; break; case 'touch': return ((y1 >= t && y1 <= b) || (y2 >= t && y2 <= b) || (y1 < t && y2 > b)) && ((x1 >= l && x1 <= r) || (x2 >= l && x2 <= r) || (x1 < l && x2 > r)); break; default: return false; break } }; $.ui.ddmanager = { current: null, droppables: { 'default': [] }, prepareOffsets: function (t, event) { var m = $.ui.ddmanager.droppables[t.options.scope] || []; var type = event ? event.type : null; var list = (t.currentItem || t.element).find(":data(droppable)").andSelf(); droppablesLoop: for (var i = 0; i < m.length; i++) { if (m[i].options.disabled || (t && !m[i].accept.call(m[i].element[0], (t.currentItem || t.element)))) continue; for (var j = 0; j < list.length; j++) { if (list[j] == m[i].element[0]) { m[i].proportions.height = 0; continue droppablesLoop } }; m[i].visible = m[i].element.css("display") != "none"; if (!m[i].visible) continue; if (type == "mousedown") m[i]._activate.call(m[i], event); m[i].offset = m[i].element.offset(); m[i].proportions = { width: m[i].element[0].offsetWidth, height: m[i].element[0].offsetHeight } } }, drop: function (draggable, event) { var dropped = false; $.each($.ui.ddmanager.droppables[draggable.options.scope] || [], function () { if (!this.options) return; if (!this.options.disabled && this.visible && $.ui.intersect(draggable, this, this.options.tolerance)) dropped = this._drop.call(this, event) || dropped; if (!this.options.disabled && this.visible && this.accept.call(this.element[0], (draggable.currentItem || draggable.element))) { this.isout = 1; this.isover = 0; this._deactivate.call(this, event) } }); return dropped }, dragStart: function (draggable, event) { draggable.element.parents(":not(body,html)").bind("scroll.droppable", function () { if (!draggable.options.refreshPositions) $.ui.ddmanager.prepareOffsets(draggable, event) }) }, drag: function (draggable, event) { if (draggable.options.refreshPositions) $.ui.ddmanager.prepareOffsets(draggable, event); $.each($.ui.ddmanager.droppables[draggable.options.scope] || [], function () { if (this.options.disabled || this.greedyChild || !this.visible) return; var intersects = $.ui.intersect(draggable, this, this.options.tolerance); var c = !intersects && this.isover == 1 ? 'isout' : (intersects && this.isover == 0 ? 'isover' : null); if (!c) return; var parentInstance; if (this.options.greedy) { var parent = this.element.parents(':data(droppable):eq(0)'); if (parent.length) { parentInstance = $.data(parent[0], 'droppable'); parentInstance.greedyChild = (c == 'isover' ? 1 : 0) } } if (parentInstance && c == 'isover') { parentInstance['isover'] = 0; parentInstance['isout'] = 1; parentInstance._out.call(parentInstance, event) } this[c] = 1; this[c == 'isout' ? 'isover' : 'isout'] = 0; this[c == "isover" ? "_over" : "_out"].call(this, event); if (parentInstance && c == 'isout') { parentInstance['isout'] = 0; parentInstance['isover'] = 1; parentInstance._over.call(parentInstance, event) } }) }, dragStop: function (draggable, event) { draggable.element.parents(":not(body,html)").unbind("scroll.droppable"); if (!draggable.options.refreshPositions) $.ui.ddmanager.prepareOffsets(draggable, event) } } })(jQuery); (function ($, undefined) { $.widget("ui.resizable", $.ui.mouse, { widgetEventPrefix: "resize", options: { alsoResize: false, animate: false, animateDuration: "slow", animateEasing: "swing", aspectRatio: false, autoHide: false, containment: false, ghost: false, grid: false, handles: "e,s,se", helper: false, maxHeight: null, maxWidth: null, minHeight: 10, minWidth: 10, zIndex: 1000 }, _create: function () { var self = this, o = this.options; this.element.addClass("ui-resizable"); $.extend(this, { _aspectRatio: !!(o.aspectRatio), aspectRatio: o.aspectRatio, originalElement: this.element, _proportionallyResizeElements: [], _helper: o.helper || o.ghost || o.animate ? o.helper || 'ui-resizable-helper' : null }); if (this.element[0].nodeName.match(/canvas|textarea|input|select|button|img/i)) { this.element.wrap($('<div class="ui-wrapper" style="overflow: hidden;"></div>').css({ position: this.element.css('position'), width: this.element.outerWidth(), height: this.element.outerHeight(), top: this.element.css('top'), left: this.element.css('left') })); this.element = this.element.parent().data("resizable", this.element.data('resizable')); this.elementIsWrapper = true; this.element.css({ marginLeft: this.originalElement.css("marginLeft"), marginTop: this.originalElement.css("marginTop"), marginRight: this.originalElement.css("marginRight"), marginBottom: this.originalElement.css("marginBottom") }); this.originalElement.css({ marginLeft: 0, marginTop: 0, marginRight: 0, marginBottom: 0 }); this.originalResizeStyle = this.originalElement.css('resize'); this.originalElement.css('resize', 'none'); this._proportionallyResizeElements.push(this.originalElement.css({ position: 'static', zoom: 1, display: 'block' })); this.originalElement.css({ margin: this.originalElement.css('margin') }); this._proportionallyResize() } this.handles = o.handles || (!$('.ui-resizable-handle', this.element).length ? "e,s,se" : { n: '.ui-resizable-n', e: '.ui-resizable-e', s: '.ui-resizable-s', w: '.ui-resizable-w', se: '.ui-resizable-se', sw: '.ui-resizable-sw', ne: '.ui-resizable-ne', nw: '.ui-resizable-nw' }); if (this.handles.constructor == String) { if (this.handles == 'all') this.handles = 'n,e,s,w,se,sw,ne,nw'; var n = this.handles.split(","); this.handles = {}; for (var i = 0; i < n.length; i++) { var handle = $.trim(n[i]), hname = 'ui-resizable-' + handle; var axis = $('<div class="ui-resizable-handle ' + hname + '"></div>'); axis.css({ zIndex: o.zIndex }); if ('se' == handle) { axis.addClass('ui-icon ui-icon-gripsmall-diagonal-se') }; this.handles[handle] = '.ui-resizable-' + handle; this.element.append(axis) } } this._renderAxis = function (target) { target = target || this.element; for (var i in this.handles) { if (this.handles[i].constructor == String) this.handles[i] = $(this.handles[i], this.element).show(); if (this.elementIsWrapper && this.originalElement[0].nodeName.match(/textarea|input|select|button/i)) { var axis = $(this.handles[i], this.element), padWrapper = 0; padWrapper = /sw|ne|nw|se|n|s/.test(i) ? axis.outerHeight() : axis.outerWidth(); var padPos = ['padding', /ne|nw|n/.test(i) ? 'Top' : /se|sw|s/.test(i) ? 'Bottom' : /^e$/.test(i) ? 'Right' : 'Left'].join(""); target.css(padPos, padWrapper); this._proportionallyResize() } if (!$(this.handles[i]).length) continue } }; this._renderAxis(this.element); this._handles = $('.ui-resizable-handle', this.element).disableSelection(); this._handles.mouseover(function () { if (!self.resizing) { if (this.className) var axis = this.className.match(/ui-resizable-(se|sw|ne|nw|n|e|s|w)/i); self.axis = axis && axis[1] ? axis[1] : 'se' } }); if (o.autoHide) { this._handles.hide(); $(this.element).addClass("ui-resizable-autohide").hover(function () { if (o.disabled) return; $(this).removeClass("ui-resizable-autohide"); self._handles.show() }, function () { if (o.disabled) return; if (!self.resizing) { $(this).addClass("ui-resizable-autohide"); self._handles.hide() } }) } this._mouseInit() }, destroy: function () { this._mouseDestroy(); var _destroy = function (exp) { $(exp).removeClass("ui-resizable ui-resizable-disabled ui-resizable-resizing").removeData("resizable").unbind(".resizable").find('.ui-resizable-handle').remove() }; if (this.elementIsWrapper) { _destroy(this.element); var wrapper = this.element; wrapper.after(this.originalElement.css({ position: wrapper.css('position'), width: wrapper.outerWidth(), height: wrapper.outerHeight(), top: wrapper.css('top'), left: wrapper.css('left') })).remove() } this.originalElement.css('resize', this.originalResizeStyle); _destroy(this.originalElement); return this }, _mouseCapture: function (event) { var handle = false; for (var i in this.handles) { if ($(this.handles[i])[0] == event.target) { handle = true } } return !this.options.disabled && handle }, _mouseStart: function (event) { var o = this.options, iniPos = this.element.position(), el = this.element; this.resizing = true; this.documentScroll = { top: $(document).scrollTop(), left: $(document).scrollLeft() }; if (el.is('.ui-draggable') || (/absolute/).test(el.css('position'))) { el.css({ position: 'absolute', top: iniPos.top, left: iniPos.left }) } this._renderProxy(); var curleft = num(this.helper.css('left')), curtop = num(this.helper.css('top')); if (o.containment) { curleft += $(o.containment).scrollLeft() || 0; curtop += $(o.containment).scrollTop() || 0 } this.offset = this.helper.offset(); this.position = { left: curleft, top: curtop }; this.size = this._helper ? { width: el.outerWidth(), height: el.outerHeight() } : { width: el.width(), height: el.height() }; this.originalSize = this._helper ? { width: el.outerWidth(), height: el.outerHeight() } : { width: el.width(), height: el.height() }; this.originalPosition = { left: curleft, top: curtop }; this.sizeDiff = { width: el.outerWidth() - el.width(), height: el.outerHeight() - el.height() }; this.originalMousePosition = { left: event.pageX, top: event.pageY }; this.aspectRatio = (typeof o.aspectRatio == 'number') ? o.aspectRatio : ((this.originalSize.width / this.originalSize.height) || 1); var cursor = $('.ui-resizable-' + this.axis).css('cursor'); $('body').css('cursor', cursor == 'auto' ? this.axis + '-resize' : cursor); el.addClass("ui-resizable-resizing"); this._propagate("start", event); return true }, _mouseDrag: function (event) { var el = this.helper, o = this.options, props = {}, self = this, smp = this.originalMousePosition, a = this.axis; var dx = (event.pageX - smp.left) || 0, dy = (event.pageY - smp.top) || 0; var trigger = this._change[a]; if (!trigger) return false; var data = trigger.apply(this, [event, dx, dy]), ie6 = $.browser.msie && $.browser.version < 7, csdif = this.sizeDiff; this._updateVirtualBoundaries(event.shiftKey); if (this._aspectRatio || event.shiftKey) data = this._updateRatio(data, event); data = this._respectSize(data, event); this._propagate("resize", event); el.css({ top: this.position.top + "px", left: this.position.left + "px", width: this.size.width + "px", height: this.size.height + "px" }); if (!this._helper && this._proportionallyResizeElements.length) this._proportionallyResize(); this._updateCache(data); this._trigger('resize', event, this.ui()); return false }, _mouseStop: function (event) { this.resizing = false; var o = this.options, self = this; if (this._helper) { var pr = this._proportionallyResizeElements, ista = pr.length && (/textarea/i).test(pr[0].nodeName), soffseth = ista && $.ui.hasScroll(pr[0], 'left') ? 0 : self.sizeDiff.height, soffsetw = ista ? 0 : self.sizeDiff.width; var s = { width: (self.helper.width() - soffsetw), height: (self.helper.height() - soffseth) }, left = (parseInt(self.element.css('left'), 10) + (self.position.left - self.originalPosition.left)) || null, top = (parseInt(self.element.css('top'), 10) + (self.position.top - self.originalPosition.top)) || null; if (!o.animate) this.element.css($.extend(s, { top: top, left: left })); self.helper.height(self.size.height); self.helper.width(self.size.width); if (this._helper && !o.animate) this._proportionallyResize() } $('body').css('cursor', 'auto'); this.element.removeClass("ui-resizable-resizing"); this._propagate("stop", event); if (this._helper) this.helper.remove(); return false }, _updateVirtualBoundaries: function (forceAspectRatio) { var o = this.options, pMinWidth, pMaxWidth, pMinHeight, pMaxHeight, b; b = { minWidth: isNumber(o.minWidth) ? o.minWidth : 0, maxWidth: isNumber(o.maxWidth) ? o.maxWidth : Infinity, minHeight: isNumber(o.minHeight) ? o.minHeight : 0, maxHeight: isNumber(o.maxHeight) ? o.maxHeight : Infinity }; if (this._aspectRatio || forceAspectRatio) { pMinWidth = b.minHeight * this.aspectRatio; pMinHeight = b.minWidth / this.aspectRatio; pMaxWidth = b.maxHeight * this.aspectRatio; pMaxHeight = b.maxWidth / this.aspectRatio; if (pMinWidth > b.minWidth) b.minWidth = pMinWidth; if (pMinHeight > b.minHeight) b.minHeight = pMinHeight; if (pMaxWidth < b.maxWidth) b.maxWidth = pMaxWidth; if (pMaxHeight < b.maxHeight) b.maxHeight = pMaxHeight } this._vBoundaries = b }, _updateCache: function (data) { var o = this.options; this.offset = this.helper.offset(); if (isNumber(data.left)) this.position.left = data.left; if (isNumber(data.top)) this.position.top = data.top; if (isNumber(data.height)) this.size.height = data.height; if (isNumber(data.width)) this.size.width = data.width }, _updateRatio: function (data, event) { var o = this.options, cpos = this.position, csize = this.size, a = this.axis; if (isNumber(data.height)) data.width = (data.height * this.aspectRatio); else if (isNumber(data.width)) data.height = (data.width / this.aspectRatio); if (a == 'sw') { data.left = cpos.left + (csize.width - data.width); data.top = null } if (a == 'nw') { data.top = cpos.top + (csize.height - data.height); data.left = cpos.left + (csize.width - data.width) } return data }, _respectSize: function (data, event) { var el = this.helper, o = this._vBoundaries, pRatio = this._aspectRatio || event.shiftKey, a = this.axis, ismaxw = isNumber(data.width) && o.maxWidth && (o.maxWidth < data.width), ismaxh = isNumber(data.height) && o.maxHeight && (o.maxHeight < data.height), isminw = isNumber(data.width) && o.minWidth && (o.minWidth > data.width), isminh = isNumber(data.height) && o.minHeight && (o.minHeight > data.height); if (isminw) data.width = o.minWidth; if (isminh) data.height = o.minHeight; if (ismaxw) data.width = o.maxWidth; if (ismaxh) data.height = o.maxHeight; var dw = this.originalPosition.left + this.originalSize.width, dh = this.position.top + this.size.height; var cw = /sw|nw|w/.test(a), ch = /nw|ne|n/.test(a); if (isminw && cw) data.left = dw - o.minWidth; if (ismaxw && cw) data.left = dw - o.maxWidth; if (isminh && ch) data.top = dh - o.minHeight; if (ismaxh && ch) data.top = dh - o.maxHeight; var isNotwh = !data.width && !data.height; if (isNotwh && !data.left && data.top) data.top = null; else if (isNotwh && !data.top && data.left) data.left = null; return data }, _proportionallyResize: function () { var o = this.options; if (!this._proportionallyResizeElements.length) return; var element = this.helper || this.element; for (var i = 0; i < this._proportionallyResizeElements.length; i++) { var prel = this._proportionallyResizeElements[i]; if (!this.borderDif) { var b = [prel.css('borderTopWidth'), prel.css('borderRightWidth'), prel.css('borderBottomWidth'), prel.css('borderLeftWidth')], p = [prel.css('paddingTop'), prel.css('paddingRight'), prel.css('paddingBottom'), prel.css('paddingLeft')]; this.borderDif = $.map(b, function (v, i) { var border = parseInt(v, 10) || 0, padding = parseInt(p[i], 10) || 0; return border + padding }) } if ($.browser.msie && !(!($(element).is(':hidden') || $(element).parents(':hidden').length))) continue; prel.css({ height: (element.height() - this.borderDif[0] - this.borderDif[2]) || 0, width: (element.width() - this.borderDif[1] - this.borderDif[3]) || 0 }) } }, _renderProxy: function () { var el = this.element, o = this.options; this.elementOffset = el.offset(); if (this._helper) { this.helper = this.helper || $('<div style="overflow:hidden;"></div>'); var ie6 = $.browser.msie && $.browser.version < 7, ie6offset = (ie6 ? 1 : 0), pxyoffset = (ie6 ? 2 : -1); this.helper.addClass(this._helper).css({ width: this.element.outerWidth() + pxyoffset, height: this.element.outerHeight() + pxyoffset, position: 'absolute', left: this.elementOffset.left - ie6offset + 'px', top: this.elementOffset.top - ie6offset + 'px', zIndex: ++o.zIndex }); this.helper.appendTo("body").disableSelection() } else { this.helper = this.element } }, _change: { e: function (event, dx, dy) { return { width: this.originalSize.width + dx } }, w: function (event, dx, dy) { var o = this.options, cs = this.originalSize, sp = this.originalPosition; return { left: sp.left + dx, width: cs.width - dx } }, n: function (event, dx, dy) { var o = this.options, cs = this.originalSize, sp = this.originalPosition; return { top: sp.top + dy, height: cs.height - dy } }, s: function (event, dx, dy) { return { height: this.originalSize.height + dy } }, se: function (event, dx, dy) { return $.extend(this._change.s.apply(this, arguments), this._change.e.apply(this, [event, dx, dy])) }, sw: function (event, dx, dy) { return $.extend(this._change.s.apply(this, arguments), this._change.w.apply(this, [event, dx, dy])) }, ne: function (event, dx, dy) { return $.extend(this._change.n.apply(this, arguments), this._change.e.apply(this, [event, dx, dy])) }, nw: function (event, dx, dy) { return $.extend(this._change.n.apply(this, arguments), this._change.w.apply(this, [event, dx, dy])) } }, _propagate: function (n, event) { $.ui.plugin.call(this, n, [event, this.ui()]); (n != "resize" && this._trigger(n, event, this.ui())) }, plugins: {}, ui: function () { return { originalElement: this.originalElement, element: this.element, helper: this.helper, position: this.position, size: this.size, originalSize: this.originalSize, originalPosition: this.originalPosition } } }); $.extend($.ui.resizable, { version: "1.8.22" }); $.ui.plugin.add("resizable", "alsoResize", { start: function (event, ui) { var self = $(this).data("resizable"), o = self.options; var _store = function (exp) { $(exp).each(function () { var el = $(this); el.data("resizable-alsoresize", { width: parseInt(el.width(), 10), height: parseInt(el.height(), 10), left: parseInt(el.css('left'), 10), top: parseInt(el.css('top'), 10) }) }) }; if (typeof (o.alsoResize) == 'object' && !o.alsoResize.parentNode) { if (o.alsoResize.length) { o.alsoResize = o.alsoResize[0]; _store(o.alsoResize) } else { $.each(o.alsoResize, function (exp) { _store(exp) }) } } else { _store(o.alsoResize) } }, resize: function (event, ui) { var self = $(this).data("resizable"), o = self.options, os = self.originalSize, op = self.originalPosition; var delta = { height: (self.size.height - os.height) || 0, width: (self.size.width - os.width) || 0, top: (self.position.top - op.top) || 0, left: (self.position.left - op.left) || 0 }, _alsoResize = function (exp, c) { $(exp).each(function () { var el = $(this), start = $(this).data("resizable-alsoresize"), style = {}, css = c && c.length ? c : el.parents(ui.originalElement[0]).length ? ['width', 'height'] : ['width', 'height', 'top', 'left']; $.each(css, function (i, prop) { var sum = (start[prop] || 0) + (delta[prop] || 0); if (sum && sum >= 0) style[prop] = sum || null }); el.css(style) }) }; if (typeof (o.alsoResize) == 'object' && !o.alsoResize.nodeType) { $.each(o.alsoResize, function (exp, c) { _alsoResize(exp, c) }) } else { _alsoResize(o.alsoResize) } }, stop: function (event, ui) { $(this).removeData("resizable-alsoresize") } }); $.ui.plugin.add("resizable", "animate", { stop: function (event, ui) { var self = $(this).data("resizable"), o = self.options; var pr = self._proportionallyResizeElements, ista = pr.length && (/textarea/i).test(pr[0].nodeName), soffseth = ista && $.ui.hasScroll(pr[0], 'left') ? 0 : self.sizeDiff.height, soffsetw = ista ? 0 : self.sizeDiff.width; var style = { width: (self.size.width - soffsetw), height: (self.size.height - soffseth) }, left = (parseInt(self.element.css('left'), 10) + (self.position.left - self.originalPosition.left)) || null, top = (parseInt(self.element.css('top'), 10) + (self.position.top - self.originalPosition.top)) || null; self.element.animate($.extend(style, top && left ? { top: top, left: left } : {}), { duration: o.animateDuration, easing: o.animateEasing, step: function () { var data = { width: parseInt(self.element.css('width'), 10), height: parseInt(self.element.css('height'), 10), top: parseInt(self.element.css('top'), 10), left: parseInt(self.element.css('left'), 10) }; if (pr && pr.length) $(pr[0]).css({ width: data.width, height: data.height }); self._updateCache(data); self._propagate("resize", event) } }) } }); $.ui.plugin.add("resizable", "containment", { start: function (event, ui) { var self = $(this).data("resizable"), o = self.options, el = self.element; var oc = o.containment, ce = (oc instanceof $) ? oc.get(0) : (/parent/.test(oc)) ? el.parent().get(0) : oc; if (!ce) return; self.containerElement = $(ce); if (/document/.test(oc) || oc == document) { self.containerOffset = { left: 0, top: 0 }; self.containerPosition = { left: 0, top: 0 }; self.parentData = { element: $(document), left: 0, top: 0, width: $(document).width(), height: $(document).height() || document.body.parentNode.scrollHeight } } else { var element = $(ce), p = []; $(["Top", "Right", "Left", "Bottom"]).each(function (i, name) { p[i] = num(element.css("padding" + name)) }); self.containerOffset = element.offset(); self.containerPosition = element.position(); self.containerSize = { height: (element.innerHeight() - p[3]), width: (element.innerWidth() - p[1]) }; var co = self.containerOffset, ch = self.containerSize.height, cw = self.containerSize.width, width = ($.ui.hasScroll(ce, "left") ? ce.scrollWidth : cw), height = ($.ui.hasScroll(ce) ? ce.scrollHeight : ch); self.parentData = { element: ce, left: co.left, top: co.top, width: width, height: height } } }, resize: function (event, ui) { var self = $(this).data("resizable"), o = self.options, ps = self.containerSize, co = self.containerOffset, cs = self.size, cp = self.position, pRatio = self._aspectRatio || event.shiftKey, cop = { top: 0, left: 0 }, ce = self.containerElement; if (ce[0] != document && (/static/).test(ce.css('position'))) cop = co; if (cp.left < (self._helper ? co.left : 0)) { self.size.width = self.size.width + (self._helper ? (self.position.left - co.left) : (self.position.left - cop.left)); if (pRatio) self.size.height = self.size.width / self.aspectRatio; self.position.left = o.helper ? co.left : 0 } if (cp.top < (self._helper ? co.top : 0)) { self.size.height = self.size.height + (self._helper ? (self.position.top - co.top) : self.position.top); if (pRatio) self.size.width = self.size.height * self.aspectRatio; self.position.top = self._helper ? co.top : 0 } self.offset.left = self.parentData.left + self.position.left; self.offset.top = self.parentData.top + self.position.top; var woset = Math.abs((self._helper ? self.offset.left - cop.left : (self.offset.left - cop.left)) + self.sizeDiff.width), hoset = Math.abs((self._helper ? self.offset.top - cop.top : (self.offset.top - co.top)) + self.sizeDiff.height); var isParent = self.containerElement.get(0) == self.element.parent().get(0), isOffsetRelative = /relative|absolute/.test(self.containerElement.css('position')); if (isParent && isOffsetRelative) woset -= self.parentData.left; if (woset + self.size.width >= self.parentData.width) { self.size.width = self.parentData.width - woset; if (pRatio) self.size.height = self.size.width / self.aspectRatio } if (hoset + self.size.height >= self.parentData.height) { self.size.height = self.parentData.height - hoset; if (pRatio) self.size.width = self.size.height * self.aspectRatio } }, stop: function (event, ui) { var self = $(this).data("resizable"), o = self.options, cp = self.position, co = self.containerOffset, cop = self.containerPosition, ce = self.containerElement; var helper = $(self.helper), ho = helper.offset(), w = helper.outerWidth() - self.sizeDiff.width, h = helper.outerHeight() - self.sizeDiff.height; if (self._helper && !o.animate && (/relative/).test(ce.css('position'))) $(this).css({ left: ho.left - cop.left - co.left, width: w, height: h }); if (self._helper && !o.animate && (/static/).test(ce.css('position'))) $(this).css({ left: ho.left - cop.left - co.left, width: w, height: h }) } }); $.ui.plugin.add("resizable", "ghost", { start: function (event, ui) { var self = $(this).data("resizable"), o = self.options, cs = self.size; self.ghost = self.originalElement.clone(); self.ghost.css({ opacity: .25, display: 'block', position: 'relative', height: cs.height, width: cs.width, margin: 0, left: 0, top: 0 }).addClass('ui-resizable-ghost').addClass(typeof o.ghost == 'string' ? o.ghost : ''); self.ghost.appendTo(self.helper) }, resize: function (event, ui) { var self = $(this).data("resizable"), o = self.options; if (self.ghost) self.ghost.css({ position: 'relative', height: self.size.height, width: self.size.width }) }, stop: function (event, ui) { var self = $(this).data("resizable"), o = self.options; if (self.ghost && self.helper) self.helper.get(0).removeChild(self.ghost.get(0)) } }); $.ui.plugin.add("resizable", "grid", { resize: function (event, ui) { var self = $(this).data("resizable"), o = self.options, cs = self.size, os = self.originalSize, op = self.originalPosition, a = self.axis, ratio = o._aspectRatio || event.shiftKey; o.grid = typeof o.grid == "number" ? [o.grid, o.grid] : o.grid; var ox = Math.round((cs.width - os.width) / (o.grid[0] || 1)) * (o.grid[0] || 1), oy = Math.round((cs.height - os.height) / (o.grid[1] || 1)) * (o.grid[1] || 1); if (/^(se|s|e)$/.test(a)) { self.size.width = os.width + ox; self.size.height = os.height + oy } else if (/^(ne)$/.test(a)) { self.size.width = os.width + ox; self.size.height = os.height + oy; self.position.top = op.top - oy } else if (/^(sw)$/.test(a)) { self.size.width = os.width + ox; self.size.height = os.height + oy; self.position.left = op.left - ox } else { self.size.width = os.width + ox; self.size.height = os.height + oy; self.position.top = op.top - oy; self.position.left = op.left - ox } } }); var num = function (v) { return parseInt(v, 10) || 0 }; var isNumber = function (value) { return !isNaN(parseInt(value, 10)) } })(jQuery); (function ($, undefined) { $.widget("ui.selectable", $.ui.mouse, { options: { appendTo: 'body', autoRefresh: true, distance: 0, filter: '*', tolerance: 'touch' }, _create: function () { var self = this; this.element.addClass("ui-selectable"); this.dragged = false; var selectees; this.refresh = function () { selectees = $(self.options.filter, self.element[0]); selectees.addClass("ui-selectee"); selectees.each(function () { var $this = $(this); var pos = $this.offset(); $.data(this, "selectable-item", { element: this, $element: $this, left: pos.left, top: pos.top, right: pos.left + $this.outerWidth(), bottom: pos.top + $this.outerHeight(), startselected: false, selected: $this.hasClass('ui-selected'), selecting: $this.hasClass('ui-selecting'), unselecting: $this.hasClass('ui-unselecting') }) }) }; this.refresh(); this.selectees = selectees.addClass("ui-selectee"); this._mouseInit(); this.helper = $("<div class='ui-selectable-helper'></div>") }, destroy: function () { this.selectees.removeClass("ui-selectee").removeData("selectable-item"); this.element.removeClass("ui-selectable ui-selectable-disabled").removeData("selectable").unbind(".selectable"); this._mouseDestroy(); return this }, _mouseStart: function (event) { var self = this; this.opos = [event.pageX, event.pageY]; if (this.options.disabled) return; var options = this.options; this.selectees = $(options.filter, this.element[0]); this._trigger("start", event); $(options.appendTo).append(this.helper); this.helper.css({ "left": event.clientX, "top": event.clientY, "width": 0, "height": 0 }); if (options.autoRefresh) { this.refresh() } this.selectees.filter('.ui-selected').each(function () { var selectee = $.data(this, "selectable-item"); selectee.startselected = true; if (!event.metaKey && !event.ctrlKey) { selectee.$element.removeClass('ui-selected'); selectee.selected = false; selectee.$element.addClass('ui-unselecting'); selectee.unselecting = true; self._trigger("unselecting", event, { unselecting: selectee.element }) } }); $(event.target).parents().andSelf().each(function () { var selectee = $.data(this, "selectable-item"); if (selectee) { var doSelect = (!event.metaKey && !event.ctrlKey) || !selectee.$element.hasClass('ui-selected'); selectee.$element.removeClass(doSelect ? "ui-unselecting" : "ui-selected").addClass(doSelect ? "ui-selecting" : "ui-unselecting"); selectee.unselecting = !doSelect; selectee.selecting = doSelect; selectee.selected = doSelect; if (doSelect) { self._trigger("selecting", event, { selecting: selectee.element }) } else { self._trigger("unselecting", event, { unselecting: selectee.element }) } return false } }) }, _mouseDrag: function (event) { var self = this; this.dragged = true; if (this.options.disabled) return; var options = this.options; var x1 = this.opos[0], y1 = this.opos[1], x2 = event.pageX, y2 = event.pageY; if (x1 > x2) { var tmp = x2; x2 = x1; x1 = tmp } if (y1 > y2) { var tmp = y2; y2 = y1; y1 = tmp } this.helper.css({ left: x1, top: y1, width: x2 - x1, height: y2 - y1 }); this.selectees.each(function () { var selectee = $.data(this, "selectable-item"); if (!selectee || selectee.element == self.element[0]) return; var hit = false; if (options.tolerance == 'touch') { hit = (!(selectee.left > x2 || selectee.right < x1 || selectee.top > y2 || selectee.bottom < y1)) } else if (options.tolerance == 'fit') { hit = (selectee.left > x1 && selectee.right < x2 && selectee.top > y1 && selectee.bottom < y2) } if (hit) { if (selectee.selected) { selectee.$element.removeClass('ui-selected'); selectee.selected = false } if (selectee.unselecting) { selectee.$element.removeClass('ui-unselecting'); selectee.unselecting = false } if (!selectee.selecting) { selectee.$element.addClass('ui-selecting'); selectee.selecting = true; self._trigger("selecting", event, { selecting: selectee.element }) } } else { if (selectee.selecting) { if ((event.metaKey || event.ctrlKey) && selectee.startselected) { selectee.$element.removeClass('ui-selecting'); selectee.selecting = false; selectee.$element.addClass('ui-selected'); selectee.selected = true } else { selectee.$element.removeClass('ui-selecting'); selectee.selecting = false; if (selectee.startselected) { selectee.$element.addClass('ui-unselecting'); selectee.unselecting = true } self._trigger("unselecting", event, { unselecting: selectee.element }) } } if (selectee.selected) { if (!event.metaKey && !event.ctrlKey && !selectee.startselected) { selectee.$element.removeClass('ui-selected'); selectee.selected = false; selectee.$element.addClass('ui-unselecting'); selectee.unselecting = true; self._trigger("unselecting", event, { unselecting: selectee.element }) } } } }); return false }, _mouseStop: function (event) { var self = this; this.dragged = false; var options = this.options; $('.ui-unselecting', this.element[0]).each(function () { var selectee = $.data(this, "selectable-item"); selectee.$element.removeClass('ui-unselecting'); selectee.unselecting = false; selectee.startselected = false; self._trigger("unselected", event, { unselected: selectee.element }) }); $('.ui-selecting', this.element[0]).each(function () { var selectee = $.data(this, "selectable-item"); selectee.$element.removeClass('ui-selecting').addClass('ui-selected'); selectee.selecting = false; selectee.selected = true; selectee.startselected = true; self._trigger("selected", event, { selected: selectee.element }) }); this._trigger("stop", event); this.helper.remove(); return false } }); $.extend($.ui.selectable, { version: "1.8.22" }) })(jQuery); (function ($, undefined) { $.widget("ui.sortable", $.ui.mouse, { widgetEventPrefix: "sort", ready: false, options: { appendTo: "parent", axis: false, connectWith: false, containment: false, cursor: 'auto', cursorAt: false, dropOnEmpty: true, forcePlaceholderSize: false, forceHelperSize: false, grid: false, handle: false, helper: "original", items: '> *', opacity: false, placeholder: false, revert: false, scroll: true, scrollSensitivity: 20, scrollSpeed: 20, scope: "default", tolerance: "intersect", zIndex: 1000 }, _create: function () { var o = this.options; this.containerCache = {}; this.element.addClass("ui-sortable"); this.refresh(); this.floating = this.items.length ? o.axis === 'x' || (/left|right/).test(this.items[0].item.css('float')) || (/inline|table-cell/).test(this.items[0].item.css('display')) : false; this.offset = this.element.offset(); this._mouseInit(); this.ready = true }, destroy: function () { $.Widget.prototype.destroy.call(this); this.element.removeClass("ui-sortable ui-sortable-disabled"); this._mouseDestroy(); for (var i = this.items.length - 1; i >= 0; i--) this.items[i].item.removeData(this.widgetName + "-item"); return this }, _setOption: function (key, value) { if (key === "disabled") { this.options[key] = value; this.widget()[value ? "addClass" : "removeClass"]("ui-sortable-disabled") } else { $.Widget.prototype._setOption.apply(this, arguments) } }, _mouseCapture: function (event, overrideHandle) { var that = this; if (this.reverting) { return false } if (this.options.disabled || this.options.type == 'static') return false; this._refreshItems(event); var currentItem = null, self = this, nodes = $(event.target).parents().each(function () { if ($.data(this, that.widgetName + '-item') == self) { currentItem = $(this); return false } }); if ($.data(event.target, that.widgetName + '-item') == self) currentItem = $(event.target); if (!currentItem) return false; if (this.options.handle && !overrideHandle) { var validHandle = false; $(this.options.handle, currentItem).find("*").andSelf().each(function () { if (this == event.target) validHandle = true }); if (!validHandle) return false } this.currentItem = currentItem; this._removeCurrentsFromItems(); return true }, _mouseStart: function (event, overrideHandle, noActivation) { var o = this.options, self = this; this.currentContainer = this; this.refreshPositions(); this.helper = this._createHelper(event); this._cacheHelperProportions(); this._cacheMargins(); this.scrollParent = this.helper.scrollParent(); this.offset = this.currentItem.offset(); this.offset = { top: this.offset.top - this.margins.top, left: this.offset.left - this.margins.left }; $.extend(this.offset, { click: { left: event.pageX - this.offset.left, top: event.pageY - this.offset.top }, parent: this._getParentOffset(), relative: this._getRelativeOffset() }); this.helper.css("position", "absolute"); this.cssPosition = this.helper.css("position"); this.originalPosition = this._generatePosition(event); this.originalPageX = event.pageX; this.originalPageY = event.pageY; (o.cursorAt && this._adjustOffsetFromHelper(o.cursorAt)); this.domPosition = { prev: this.currentItem.prev()[0], parent: this.currentItem.parent()[0] }; if (this.helper[0] != this.currentItem[0]) { this.currentItem.hide() } this._createPlaceholder(); if (o.containment) this._setContainment(); if (o.cursor) { if ($('body').css("cursor")) this._storedCursor = $('body').css("cursor"); $('body').css("cursor", o.cursor) } if (o.opacity) { if (this.helper.css("opacity")) this._storedOpacity = this.helper.css("opacity"); this.helper.css("opacity", o.opacity) } if (o.zIndex) { if (this.helper.css("zIndex")) this._storedZIndex = this.helper.css("zIndex"); this.helper.css("zIndex", o.zIndex) } if (this.scrollParent[0] != document && this.scrollParent[0].tagName != 'HTML') this.overflowOffset = this.scrollParent.offset(); this._trigger("start", event, this._uiHash()); if (!this._preserveHelperProportions) this._cacheHelperProportions(); if (!noActivation) { for (var i = this.containers.length - 1; i >= 0; i--) { this.containers[i]._trigger("activate", event, self._uiHash(this)) } } if ($.ui.ddmanager) $.ui.ddmanager.current = this; if ($.ui.ddmanager && !o.dropBehaviour) $.ui.ddmanager.prepareOffsets(this, event); this.dragging = true; this.helper.addClass("ui-sortable-helper"); this._mouseDrag(event); return true }, _mouseDrag: function (event) { this.position = this._generatePosition(event); this.positionAbs = this._convertPositionTo("absolute"); if (!this.lastPositionAbs) { this.lastPositionAbs = this.positionAbs } if (this.options.scroll) { var o = this.options, scrolled = false; if (this.scrollParent[0] != document && this.scrollParent[0].tagName != 'HTML') { if ((this.overflowOffset.top + this.scrollParent[0].offsetHeight) - event.pageY < o.scrollSensitivity) this.scrollParent[0].scrollTop = scrolled = this.scrollParent[0].scrollTop + o.scrollSpeed; else if (event.pageY - this.overflowOffset.top < o.scrollSensitivity) this.scrollParent[0].scrollTop = scrolled = this.scrollParent[0].scrollTop - o.scrollSpeed; if ((this.overflowOffset.left + this.scrollParent[0].offsetWidth) - event.pageX < o.scrollSensitivity) this.scrollParent[0].scrollLeft = scrolled = this.scrollParent[0].scrollLeft + o.scrollSpeed; else if (event.pageX - this.overflowOffset.left < o.scrollSensitivity) this.scrollParent[0].scrollLeft = scrolled = this.scrollParent[0].scrollLeft - o.scrollSpeed } else { if (event.pageY - $(document).scrollTop() < o.scrollSensitivity) scrolled = $(document).scrollTop($(document).scrollTop() - o.scrollSpeed); else if ($(window).height() - (event.pageY - $(document).scrollTop()) < o.scrollSensitivity) scrolled = $(document).scrollTop($(document).scrollTop() + o.scrollSpeed); if (event.pageX - $(document).scrollLeft() < o.scrollSensitivity) scrolled = $(document).scrollLeft($(document).scrollLeft() - o.scrollSpeed); else if ($(window).width() - (event.pageX - $(document).scrollLeft()) < o.scrollSensitivity) scrolled = $(document).scrollLeft($(document).scrollLeft() + o.scrollSpeed) } if (scrolled !== false && $.ui.ddmanager && !o.dropBehaviour) $.ui.ddmanager.prepareOffsets(this, event) } this.positionAbs = this._convertPositionTo("absolute"); if (!this.options.axis || this.options.axis != "y") this.helper[0].style.left = this.position.left + 'px'; if (!this.options.axis || this.options.axis != "x") this.helper[0].style.top = this.position.top + 'px'; for (var i = this.items.length - 1; i >= 0; i--) { var item = this.items[i], itemElement = item.item[0], intersection = this._intersectsWithPointer(item); if (!intersection) continue; if (itemElement != this.currentItem[0] && this.placeholder[intersection == 1 ? "next" : "prev"]()[0] != itemElement && !$.ui.contains(this.placeholder[0], itemElement) && (this.options.type == 'semi-dynamic' ? !$.ui.contains(this.element[0], itemElement) : true)) { this.direction = intersection == 1 ? "down" : "up"; if (this.options.tolerance == "pointer" || this._intersectsWithSides(item)) { this._rearrange(event, item) } else { break } this._trigger("change", event, this._uiHash()); break } } this._contactContainers(event); if ($.ui.ddmanager) $.ui.ddmanager.drag(this, event); this._trigger('sort', event, this._uiHash()); this.lastPositionAbs = this.positionAbs; return false }, _mouseStop: function (event, noPropagation) { if (!event) return; if ($.ui.ddmanager && !this.options.dropBehaviour) $.ui.ddmanager.drop(this, event); if (this.options.revert) { var self = this; var cur = self.placeholder.offset(); self.reverting = true; $(this.helper).animate({ left: cur.left - this.offset.parent.left - self.margins.left + (this.offsetParent[0] == document.body ? 0 : this.offsetParent[0].scrollLeft), top: cur.top - this.offset.parent.top - self.margins.top + (this.offsetParent[0] == document.body ? 0 : this.offsetParent[0].scrollTop) }, parseInt(this.options.revert, 10) || 500, function () { self._clear(event) }) } else { this._clear(event, noPropagation) } return false }, cancel: function () { var self = this; if (this.dragging) { this._mouseUp({ target: null }); if (this.options.helper == "original") this.currentItem.css(this._storedCSS).removeClass("ui-sortable-helper"); else this.currentItem.show(); for (var i = this.containers.length - 1; i >= 0; i--) { this.containers[i]._trigger("deactivate", null, self._uiHash(this)); if (this.containers[i].containerCache.over) { this.containers[i]._trigger("out", null, self._uiHash(this)); this.containers[i].containerCache.over = 0 } } } if (this.placeholder) { if (this.placeholder[0].parentNode) this.placeholder[0].parentNode.removeChild(this.placeholder[0]); if (this.options.helper != "original" && this.helper && this.helper[0].parentNode) this.helper.remove(); $.extend(this, { helper: null, dragging: false, reverting: false, _noFinalSort: null }); if (this.domPosition.prev) { $(this.domPosition.prev).after(this.currentItem) } else { $(this.domPosition.parent).prepend(this.currentItem) } } return this }, serialize: function (o) { var items = this._getItemsAsjQuery(o && o.connected); var str = []; o = o || {}; $(items).each(function () { var res = ($(o.item || this).attr(o.attribute || 'id') || '').match(o.expression || (/(.+)[-=_](.+)/)); if (res) str.push((o.key || res[1] + '[]') + '=' + (o.key && o.expression ? res[1] : res[2])) }); if (!str.length && o.key) { str.push(o.key + '=') } return str.join('&') }, toArray: function (o) { var items = this._getItemsAsjQuery(o && o.connected); var ret = []; o = o || {}; items.each(function () { ret.push($(o.item || this).attr(o.attribute || 'id') || '') }); return ret }, _intersectsWith: function (item) { var x1 = this.positionAbs.left, x2 = x1 + this.helperProportions.width, y1 = this.positionAbs.top, y2 = y1 + this.helperProportions.height; var l = item.left, r = l + item.width, t = item.top, b = t + item.height; var dyClick = this.offset.click.top, dxClick = this.offset.click.left; var isOverElement = (y1 + dyClick) > t && (y1 + dyClick) < b && (x1 + dxClick) > l && (x1 + dxClick) < r; if (this.options.tolerance == "pointer" || this.options.forcePointerForContainers || (this.options.tolerance != "pointer" && this.helperProportions[this.floating ? 'width' : 'height'] > item[this.floating ? 'width' : 'height'])) { return isOverElement } else { return (l < x1 + (this.helperProportions.width / 2) && x2 - (this.helperProportions.width / 2) < r && t < y1 + (this.helperProportions.height / 2) && y2 - (this.helperProportions.height / 2) < b); } }, _intersectsWithPointer: function (item) { var isOverElementHeight = (this.options.axis === 'x') || $.ui.isOverAxis(this.positionAbs.top + this.offset.click.top, item.top, item.height), isOverElementWidth = (this.options.axis === 'y') || $.ui.isOverAxis(this.positionAbs.left + this.offset.click.left, item.left, item.width), isOverElement = isOverElementHeight && isOverElementWidth, verticalDirection = this._getDragVerticalDirection(), horizontalDirection = this._getDragHorizontalDirection(); if (!isOverElement) return false; return this.floating ? (((horizontalDirection && horizontalDirection == "right") || verticalDirection == "down") ? 2 : 1) : (verticalDirection && (verticalDirection == "down" ? 2 : 1)) }, _intersectsWithSides: function (item) { var isOverBottomHalf = $.ui.isOverAxis(this.positionAbs.top + this.offset.click.top, item.top + (item.height / 2), item.height), isOverRightHalf = $.ui.isOverAxis(this.positionAbs.left + this.offset.click.left, item.left + (item.width / 2), item.width), verticalDirection = this._getDragVerticalDirection(), horizontalDirection = this._getDragHorizontalDirection(); if (this.floating && horizontalDirection) { return ((horizontalDirection == "right" && isOverRightHalf) || (horizontalDirection == "left" && !isOverRightHalf)) } else { return verticalDirection && ((verticalDirection == "down" && isOverBottomHalf) || (verticalDirection == "up" && !isOverBottomHalf)) } }, _getDragVerticalDirection: function () { var delta = this.positionAbs.top - this.lastPositionAbs.top; return delta != 0 && (delta > 0 ? "down" : "up") }, _getDragHorizontalDirection: function () { var delta = this.positionAbs.left - this.lastPositionAbs.left; return delta != 0 && (delta > 0 ? "right" : "left") }, refresh: function (event) { this._refreshItems(event); this.refreshPositions(); return this }, _connectWith: function () { var options = this.options; return options.connectWith.constructor == String ? [options.connectWith] : options.connectWith }, _getItemsAsjQuery: function (connected) { var self = this; var items = []; var queries = []; var connectWith = this._connectWith(); if (connectWith && connected) { for (var i = connectWith.length - 1; i >= 0; i--) { var cur = $(connectWith[i]); for (var j = cur.length - 1; j >= 0; j--) { var inst = $.data(cur[j], this.widgetName); if (inst && inst != this && !inst.options.disabled) { queries.push([$.isFunction(inst.options.items) ? inst.options.items.call(inst.element) : $(inst.options.items, inst.element).not(".ui-sortable-helper").not('.ui-sortable-placeholder'), inst]) } } } } queries.push([$.isFunction(this.options.items) ? this.options.items.call(this.element, null, { options: this.options, item: this.currentItem }) : $(this.options.items, this.element).not(".ui-sortable-helper").not('.ui-sortable-placeholder'), this]); for (var i = queries.length - 1; i >= 0; i--) { queries[i][0].each(function () { items.push(this) }) }; return $(items) }, _removeCurrentsFromItems: function () { var list = this.currentItem.find(":data(" + this.widgetName + "-item)"); for (var i = 0; i < this.items.length; i++) { for (var j = 0; j < list.length; j++) { if (list[j] == this.items[i].item[0]) this.items.splice(i, 1) } } }, _refreshItems: function (event) { this.items = []; this.containers = [this]; var items = this.items; var self = this; var queries = [[$.isFunction(this.options.items) ? this.options.items.call(this.element[0], event, { item: this.currentItem }) : $(this.options.items, this.element), this]]; var connectWith = this._connectWith(); if (connectWith && this.ready) { for (var i = connectWith.length - 1; i >= 0; i--) { var cur = $(connectWith[i]); for (var j = cur.length - 1; j >= 0; j--) { var inst = $.data(cur[j], this.widgetName); if (inst && inst != this && !inst.options.disabled) { queries.push([$.isFunction(inst.options.items) ? inst.options.items.call(inst.element[0], event, { item: this.currentItem }) : $(inst.options.items, inst.element), inst]); this.containers.push(inst) } } } } for (var i = queries.length - 1; i >= 0; i--) { var targetData = queries[i][1]; var _queries = queries[i][0]; for (var j = 0, queriesLength = _queries.length; j < queriesLength; j++) { var item = $(_queries[j]); item.data(this.widgetName + '-item', targetData); items.push({ item: item, instance: targetData, width: 0, height: 0, left: 0, top: 0 }) } } }, refreshPositions: function (fast) { if (this.offsetParent && this.helper) { this.offset.parent = this._getParentOffset() } for (var i = this.items.length - 1; i >= 0; i--) { var item = this.items[i]; if (item.instance != this.currentContainer && this.currentContainer && item.item[0] != this.currentItem[0]) continue; var t = this.options.toleranceElement ? $(this.options.toleranceElement, item.item) : item.item; if (!fast) { item.width = t.outerWidth(); item.height = t.outerHeight() } var p = t.offset(); item.left = p.left; item.top = p.top }; if (this.options.custom && this.options.custom.refreshContainers) { this.options.custom.refreshContainers.call(this) } else { for (var i = this.containers.length - 1; i >= 0; i--) { var p = this.containers[i].element.offset(); this.containers[i].containerCache.left = p.left; this.containers[i].containerCache.top = p.top; this.containers[i].containerCache.width = this.containers[i].element.outerWidth(); this.containers[i].containerCache.height = this.containers[i].element.outerHeight() } } return this }, _createPlaceholder: function (that) { var self = that || this, o = self.options; if (!o.placeholder || o.placeholder.constructor == String) { var className = o.placeholder; o.placeholder = { element: function () { var el = $(document.createElement(self.currentItem[0].nodeName)).addClass(className || self.currentItem[0].className + " ui-sortable-placeholder").removeClass("ui-sortable-helper")[0]; if (!className) el.style.visibility = "hidden"; return el }, update: function (container, p) { if (className && !o.forcePlaceholderSize) return; if (!p.height()) { p.height(self.currentItem.innerHeight() - parseInt(self.currentItem.css('paddingTop') || 0, 10) - parseInt(self.currentItem.css('paddingBottom') || 0, 10)) }; if (!p.width()) { p.width(self.currentItem.innerWidth() - parseInt(self.currentItem.css('paddingLeft') || 0, 10) - parseInt(self.currentItem.css('paddingRight') || 0, 10)) } } } } self.placeholder = $(o.placeholder.element.call(self.element, self.currentItem)); self.currentItem.after(self.placeholder); o.placeholder.update(self, self.placeholder) }, _contactContainers: function (event) { var innermostContainer = null, innermostIndex = null; for (var i = this.containers.length - 1; i >= 0; i--) { if ($.ui.contains(this.currentItem[0], this.containers[i].element[0])) continue; if (this._intersectsWith(this.containers[i].containerCache)) { if (innermostContainer && $.ui.contains(this.containers[i].element[0], innermostContainer.element[0])) continue; innermostContainer = this.containers[i]; innermostIndex = i } else { if (this.containers[i].containerCache.over) { this.containers[i]._trigger("out", event, this._uiHash(this)); this.containers[i].containerCache.over = 0 } } } if (!innermostContainer) return; if (this.containers.length === 1) { this.containers[innermostIndex]._trigger("over", event, this._uiHash(this)); this.containers[innermostIndex].containerCache.over = 1 } else if (this.currentContainer != this.containers[innermostIndex]) { var dist = 10000; var itemWithLeastDistance = null; var base = this.positionAbs[this.containers[innermostIndex].floating ? 'left' : 'top']; for (var j = this.items.length - 1; j >= 0; j--) { if (!$.ui.contains(this.containers[innermostIndex].element[0], this.items[j].item[0])) continue; var cur = this.containers[innermostIndex].floating ? this.items[j].item.offset().left : this.items[j].item.offset().top; if (Math.abs(cur - base) < dist) { dist = Math.abs(cur - base); itemWithLeastDistance = this.items[j]; this.direction = (cur - base > 0) ? 'down' : 'up' } } if (!itemWithLeastDistance && !this.options.dropOnEmpty) return; this.currentContainer = this.containers[innermostIndex]; itemWithLeastDistance ? this._rearrange(event, itemWithLeastDistance, null, true) : this._rearrange(event, null, this.containers[innermostIndex].element, true); this._trigger("change", event, this._uiHash()); this.containers[innermostIndex]._trigger("change", event, this._uiHash(this)); this.options.placeholder.update(this.currentContainer, this.placeholder); this.containers[innermostIndex]._trigger("over", event, this._uiHash(this)); this.containers[innermostIndex].containerCache.over = 1 } }, _createHelper: function (event) { var o = this.options; var helper = $.isFunction(o.helper) ? $(o.helper.apply(this.element[0], [event, this.currentItem])) : (o.helper == 'clone' ? this.currentItem.clone() : this.currentItem); if (!helper.parents('body').length) $(o.appendTo != 'parent' ? o.appendTo : this.currentItem[0].parentNode)[0].appendChild(helper[0]); if (helper[0] == this.currentItem[0]) this._storedCSS = { width: this.currentItem[0].style.width, height: this.currentItem[0].style.height, position: this.currentItem.css("position"), top: this.currentItem.css("top"), left: this.currentItem.css("left") }; if (helper[0].style.width == '' || o.forceHelperSize) helper.width(this.currentItem.width()); if (helper[0].style.height == '' || o.forceHelperSize) helper.height(this.currentItem.height()); return helper }, _adjustOffsetFromHelper: function (obj) { if (typeof obj == 'string') { obj = obj.split(' ') } if ($.isArray(obj)) { obj = { left: +obj[0], top: +obj[1] || 0 } } if ('left' in obj) { this.offset.click.left = obj.left + this.margins.left } if ('right' in obj) { this.offset.click.left = this.helperProportions.width - obj.right + this.margins.left } if ('top' in obj) { this.offset.click.top = obj.top + this.margins.top } if ('bottom' in obj) { this.offset.click.top = this.helperProportions.height - obj.bottom + this.margins.top } }, _getParentOffset: function () { this.offsetParent = this.helper.offsetParent(); var po = this.offsetParent.offset(); if (this.cssPosition == 'absolute' && this.scrollParent[0] != document && $.ui.contains(this.scrollParent[0], this.offsetParent[0])) { po.left += this.scrollParent.scrollLeft(); po.top += this.scrollParent.scrollTop() } if ((this.offsetParent[0] == document.body) || (this.offsetParent[0].tagName && this.offsetParent[0].tagName.toLowerCase() == 'html' && $.browser.msie)) po = { top: 0, left: 0 }; return { top: po.top + (parseInt(this.offsetParent.css("borderTopWidth"), 10) || 0), left: po.left + (parseInt(this.offsetParent.css("borderLeftWidth"), 10) || 0) } }, _getRelativeOffset: function () { if (this.cssPosition == "relative") { var p = this.currentItem.position(); return { top: p.top - (parseInt(this.helper.css("top"), 10) || 0) + this.scrollParent.scrollTop(), left: p.left - (parseInt(this.helper.css("left"), 10) || 0) + this.scrollParent.scrollLeft() } } else { return { top: 0, left: 0 } } }, _cacheMargins: function () { this.margins = { left: (parseInt(this.currentItem.css("marginLeft"), 10) || 0), top: (parseInt(this.currentItem.css("marginTop"), 10) || 0) } }, _cacheHelperProportions: function () { this.helperProportions = { width: this.helper.outerWidth(), height: this.helper.outerHeight() } }, _setContainment: function () { var o = this.options; if (o.containment == 'parent') o.containment = this.helper[0].parentNode; if (o.containment == 'document' || o.containment == 'window') this.containment = [0 - this.offset.relative.left - this.offset.parent.left, 0 - this.offset.relative.top - this.offset.parent.top, $(o.containment == 'document' ? document : window).width() - this.helperProportions.width - this.margins.left, ($(o.containment == 'document' ? document : window).height() || document.body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top]; if (!(/^(document|window|parent)$/).test(o.containment)) { var ce = $(o.containment)[0]; var co = $(o.containment).offset(); var over = ($(ce).css("overflow") != 'hidden'); this.containment = [co.left + (parseInt($(ce).css("borderLeftWidth"), 10) || 0) + (parseInt($(ce).css("paddingLeft"), 10) || 0) - this.margins.left, co.top + (parseInt($(ce).css("borderTopWidth"), 10) || 0) + (parseInt($(ce).css("paddingTop"), 10) || 0) - this.margins.top, co.left + (over ? Math.max(ce.scrollWidth, ce.offsetWidth) : ce.offsetWidth) - (parseInt($(ce).css("borderLeftWidth"), 10) || 0) - (parseInt($(ce).css("paddingRight"), 10) || 0) - this.helperProportions.width - this.margins.left, co.top + (over ? Math.max(ce.scrollHeight, ce.offsetHeight) : ce.offsetHeight) - (parseInt($(ce).css("borderTopWidth"), 10) || 0) - (parseInt($(ce).css("paddingBottom"), 10) || 0) - this.helperProportions.height - this.margins.top] } }, _convertPositionTo: function (d, pos) { if (!pos) pos = this.position; var mod = d == "absolute" ? 1 : -1; var o = this.options, scroll = this.cssPosition == 'absolute' && !(this.scrollParent[0] != document && $.ui.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent, scrollIsRootNode = (/(html|body)/i).test(scroll[0].tagName); return { top: (pos.top + this.offset.relative.top * mod + this.offset.parent.top * mod - ($.browser.safari && this.cssPosition == 'fixed' ? 0 : (this.cssPosition == 'fixed' ? -this.scrollParent.scrollTop() : (scrollIsRootNode ? 0 : scroll.scrollTop())) * mod)), left: (pos.left + this.offset.relative.left * mod + this.offset.parent.left * mod - ($.browser.safari && this.cssPosition == 'fixed' ? 0 : (this.cssPosition == 'fixed' ? -this.scrollParent.scrollLeft() : scrollIsRootNode ? 0 : scroll.scrollLeft()) * mod)) } }, _generatePosition: function (event) { var o = this.options, scroll = this.cssPosition == 'absolute' && !(this.scrollParent[0] != document && $.ui.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent, scrollIsRootNode = (/(html|body)/i).test(scroll[0].tagName); if (this.cssPosition == 'relative' && !(this.scrollParent[0] != document && this.scrollParent[0] != this.offsetParent[0])) { this.offset.relative = this._getRelativeOffset() } var pageX = event.pageX; var pageY = event.pageY; if (this.originalPosition) { if (this.containment) { if (event.pageX - this.offset.click.left < this.containment[0]) pageX = this.containment[0] + this.offset.click.left; if (event.pageY - this.offset.click.top < this.containment[1]) pageY = this.containment[1] + this.offset.click.top; if (event.pageX - this.offset.click.left > this.containment[2]) pageX = this.containment[2] + this.offset.click.left; if (event.pageY - this.offset.click.top > this.containment[3]) pageY = this.containment[3] + this.offset.click.top } if (o.grid) { var top = this.originalPageY + Math.round((pageY - this.originalPageY) / o.grid[1]) * o.grid[1]; pageY = this.containment ? (!(top - this.offset.click.top < this.containment[1] || top - this.offset.click.top > this.containment[3]) ? top : (!(top - this.offset.click.top < this.containment[1]) ? top - o.grid[1] : top + o.grid[1])) : top; var left = this.originalPageX + Math.round((pageX - this.originalPageX) / o.grid[0]) * o.grid[0]; pageX = this.containment ? (!(left - this.offset.click.left < this.containment[0] || left - this.offset.click.left > this.containment[2]) ? left : (!(left - this.offset.click.left < this.containment[0]) ? left - o.grid[0] : left + o.grid[0])) : left } } return { top: (pageY - this.offset.click.top - this.offset.relative.top - this.offset.parent.top + ($.browser.safari && this.cssPosition == 'fixed' ? 0 : (this.cssPosition == 'fixed' ? -this.scrollParent.scrollTop() : (scrollIsRootNode ? 0 : scroll.scrollTop())))), left: (pageX - this.offset.click.left - this.offset.relative.left - this.offset.parent.left + ($.browser.safari && this.cssPosition == 'fixed' ? 0 : (this.cssPosition == 'fixed' ? -this.scrollParent.scrollLeft() : scrollIsRootNode ? 0 : scroll.scrollLeft()))) } }, _rearrange: function (event, i, a, hardRefresh) { a ? a[0].appendChild(this.placeholder[0]) : i.item[0].parentNode.insertBefore(this.placeholder[0], (this.direction == 'down' ? i.item[0] : i.item[0].nextSibling)); this.counter = this.counter ? ++this.counter : 1; var self = this, counter = this.counter; window.setTimeout(function () { if (counter == self.counter) self.refreshPositions(!hardRefresh) }, 0) }, _clear: function (event, noPropagation) { this.reverting = false; var delayedTriggers = [], self = this; if (!this._noFinalSort && this.currentItem.parent().length) this.placeholder.before(this.currentItem); this._noFinalSort = null; if (this.helper[0] == this.currentItem[0]) { for (var i in this._storedCSS) { if (this._storedCSS[i] == 'auto' || this._storedCSS[i] == 'static') this._storedCSS[i] = '' } this.currentItem.css(this._storedCSS).removeClass("ui-sortable-helper") } else { this.currentItem.show() } if (this.fromOutside && !noPropagation) delayedTriggers.push(function (event) { this._trigger("receive", event, this._uiHash(this.fromOutside)) }); if ((this.fromOutside || this.domPosition.prev != this.currentItem.prev().not(".ui-sortable-helper")[0] || this.domPosition.parent != this.currentItem.parent()[0]) && !noPropagation) delayedTriggers.push(function (event) { this._trigger("update", event, this._uiHash()) }); if (!$.ui.contains(this.element[0], this.currentItem[0])) { if (!noPropagation) delayedTriggers.push(function (event) { this._trigger("remove", event, this._uiHash()) }); for (var i = this.containers.length - 1; i >= 0; i--) { if ($.ui.contains(this.containers[i].element[0], this.currentItem[0]) && !noPropagation) { delayedTriggers.push((function (c) { return function (event) { c._trigger("receive", event, this._uiHash(this)) } }).call(this, this.containers[i])); delayedTriggers.push((function (c) { return function (event) { c._trigger("update", event, this._uiHash(this)) } }).call(this, this.containers[i])) } } }; for (var i = this.containers.length - 1; i >= 0; i--) { if (!noPropagation) delayedTriggers.push((function (c) { return function (event) { c._trigger("deactivate", event, this._uiHash(this)) } }).call(this, this.containers[i])); if (this.containers[i].containerCache.over) { delayedTriggers.push((function (c) { return function (event) { c._trigger("out", event, this._uiHash(this)) } }).call(this, this.containers[i])); this.containers[i].containerCache.over = 0 } } if (this._storedCursor) $('body').css("cursor", this._storedCursor); if (this._storedOpacity) this.helper.css("opacity", this._storedOpacity); if (this._storedZIndex) this.helper.css("zIndex", this._storedZIndex == 'auto' ? '' : this._storedZIndex); this.dragging = false; if (this.cancelHelperRemoval) { if (!noPropagation) { this._trigger("beforeStop", event, this._uiHash()); for (var i = 0; i < delayedTriggers.length; i++) { delayedTriggers[i].call(this, event) }; this._trigger("stop", event, this._uiHash()) } this.fromOutside = false; return false } if (!noPropagation) this._trigger("beforeStop", event, this._uiHash()); this.placeholder[0].parentNode.removeChild(this.placeholder[0]); if (this.helper[0] != this.currentItem[0]) this.helper.remove(); this.helper = null; if (!noPropagation) { for (var i = 0; i < delayedTriggers.length; i++) { delayedTriggers[i].call(this, event) }; this._trigger("stop", event, this._uiHash()) } this.fromOutside = false; return true }, _trigger: function () { if ($.Widget.prototype._trigger.apply(this, arguments) === false) { this.cancel() } }, _uiHash: function (inst) { var self = inst || this; return { helper: self.helper, placeholder: self.placeholder || $([]), position: self.position, originalPosition: self.originalPosition, offset: self.positionAbs, item: self.currentItem, sender: inst ? inst.element : null } } }); $.extend($.ui.sortable, { version: "1.8.22" }) })(jQuery); jQuery.effects || (function ($, undefined) { $.effects = {}; $.each(['backgroundColor', 'borderBottomColor', 'borderLeftColor', 'borderRightColor', 'borderTopColor', 'borderColor', 'color', 'outlineColor'], function (i, attr) { $.fx.step[attr] = function (fx) { if (!fx.colorInit) { fx.start = getColor(fx.elem, attr); fx.end = getRGB(fx.end); fx.colorInit = true } fx.elem.style[attr] = 'rgb(' + Math.max(Math.min(parseInt((fx.pos * (fx.end[0] - fx.start[0])) + fx.start[0], 10), 255), 0) + ',' + Math.max(Math.min(parseInt((fx.pos * (fx.end[1] - fx.start[1])) + fx.start[1], 10), 255), 0) + ',' + Math.max(Math.min(parseInt((fx.pos * (fx.end[2] - fx.start[2])) + fx.start[2], 10), 255), 0) + ')' } }); function getRGB(color) { var result; if (color && color.constructor == Array && color.length == 3) return color; if (result = /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/.exec(color)) return [parseInt(result[1], 10), parseInt(result[2], 10), parseInt(result[3], 10)]; if (result = /rgb\(\s*([0-9]+(?:\.[0-9]+)?)\%\s*,\s*([0-9]+(?:\.[0-9]+)?)\%\s*,\s*([0-9]+(?:\.[0-9]+)?)\%\s*\)/.exec(color)) return [parseFloat(result[1]) * 2.55, parseFloat(result[2]) * 2.55, parseFloat(result[3]) * 2.55]; if (result = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(color)) return [parseInt(result[1], 16), parseInt(result[2], 16), parseInt(result[3], 16)]; if (result = /#([a-fA-F0-9])([a-fA-F0-9])([a-fA-F0-9])/.exec(color)) return [parseInt(result[1] + result[1], 16), parseInt(result[2] + result[2], 16), parseInt(result[3] + result[3], 16)]; if (result = /rgba\(0, 0, 0, 0\)/.exec(color)) return colors['transparent']; return colors[$.trim(color).toLowerCase()] } function getColor(elem, attr) { var color; do { color = ($.curCSS || $.css)(elem, attr); if (color != '' && color != 'transparent' || $.nodeName(elem, "body")) break; attr = "backgroundColor" } while (elem = elem.parentNode); return getRGB(color) }; var colors = { aqua: [0, 255, 255], azure: [240, 255, 255], beige: [245, 245, 220], black: [0, 0, 0], blue: [0, 0, 255], brown: [165, 42, 42], cyan: [0, 255, 255], darkblue: [0, 0, 139], darkcyan: [0, 139, 139], darkgrey: [169, 169, 169], darkgreen: [0, 100, 0], darkkhaki: [189, 183, 107], darkmagenta: [139, 0, 139], darkolivegreen: [85, 107, 47], darkorange: [255, 140, 0], darkorchid: [153, 50, 204], darkred: [139, 0, 0], darksalmon: [233, 150, 122], darkviolet: [148, 0, 211], fuchsia: [255, 0, 255], gold: [255, 215, 0], green: [0, 128, 0], indigo: [75, 0, 130], khaki: [240, 230, 140], lightblue: [173, 216, 230], lightcyan: [224, 255, 255], lightgreen: [144, 238, 144], lightgrey: [211, 211, 211], lightpink: [255, 182, 193], lightyellow: [255, 255, 224], lime: [0, 255, 0], magenta: [255, 0, 255], maroon: [128, 0, 0], navy: [0, 0, 128], olive: [128, 128, 0], orange: [255, 165, 0], pink: [255, 192, 203], purple: [128, 0, 128], violet: [128, 0, 128], red: [255, 0, 0], silver: [192, 192, 192], white: [255, 255, 255], yellow: [255, 255, 0], transparent: [255, 255, 255] }; var classAnimationActions = ['add', 'remove', 'toggle'], shorthandStyles = { border: 1, borderBottom: 1, borderColor: 1, borderLeft: 1, borderRight: 1, borderTop: 1, borderWidth: 1, margin: 1, padding: 1 }; function getElementStyles() { var style = document.defaultView ? document.defaultView.getComputedStyle(this, null) : this.currentStyle, newStyle = {}, key, camelCase; if (style && style.length && style[0] && style[style[0]]) { var len = style.length; while (len--) { key = style[len]; if (typeof style[key] == 'string') { camelCase = key.replace(/\-(\w)/g, function (all, letter) { return letter.toUpperCase() }); newStyle[camelCase] = style[key] } } } else { for (key in style) { if (typeof style[key] === 'string') { newStyle[key] = style[key] } } } return newStyle } function filterStyles(styles) { var name, value; for (name in styles) { value = styles[name]; if (value == null || $.isFunction(value) || name in shorthandStyles || (/scrollbar/).test(name) || (!(/color/i).test(name) && isNaN(parseFloat(value)))) { delete styles[name] } } return styles } function styleDifference(oldStyle, newStyle) { var diff = { _: 0 }, name; for (name in newStyle) { if (oldStyle[name] != newStyle[name]) { diff[name] = newStyle[name] } } return diff } $.effects.animateClass = function (value, duration, easing, callback) { if ($.isFunction(easing)) { callback = easing; easing = null } return this.queue(function () { var that = $(this), originalStyleAttr = that.attr('style') || ' ', originalStyle = filterStyles(getElementStyles.call(this)), newStyle, className = that.attr('class') || ""; $.each(classAnimationActions, function (i, action) { if (value[action]) { that[action + 'Class'](value[action]) } }); newStyle = filterStyles(getElementStyles.call(this)); that.attr('class', className); that.animate(styleDifference(originalStyle, newStyle), { queue: false, duration: duration, easing: easing, complete: function () { $.each(classAnimationActions, function (i, action) { if (value[action]) { that[action + 'Class'](value[action]) } }); if (typeof that.attr('style') == 'object') { that.attr('style').cssText = ''; that.attr('style').cssText = originalStyleAttr } else { that.attr('style', originalStyleAttr) } if (callback) { callback.apply(this, arguments) } $.dequeue(this) } }) }) }; $.fn.extend({ _addClass: $.fn.addClass, addClass: function (classNames, speed, easing, callback) { return speed ? $.effects.animateClass.apply(this, [{ add: classNames }, speed, easing, callback]) : this._addClass(classNames) }, _removeClass: $.fn.removeClass, removeClass: function (classNames, speed, easing, callback) { return speed ? $.effects.animateClass.apply(this, [{ remove: classNames }, speed, easing, callback]) : this._removeClass(classNames) }, _toggleClass: $.fn.toggleClass, toggleClass: function (classNames, force, speed, easing, callback) { if (typeof force == "boolean" || force === undefined) { if (!speed) { return this._toggleClass(classNames, force) } else { return $.effects.animateClass.apply(this, [(force ? { add: classNames } : { remove: classNames }), speed, easing, callback]) } } else { return $.effects.animateClass.apply(this, [{ toggle: classNames }, force, speed, easing]) } }, switchClass: function (remove, add, speed, easing, callback) { return $.effects.animateClass.apply(this, [{ add: add, remove: remove }, speed, easing, callback]) } }); $.extend($.effects, { version: "1.8.22", save: function (element, set) { for (var i = 0; i < set.length; i++) { if (set[i] !== null) element.data("ec.storage." + set[i], element[0].style[set[i]]) } }, restore: function (element, set) { for (var i = 0; i < set.length; i++) { if (set[i] !== null) element.css(set[i], element.data("ec.storage." + set[i])) } }, setMode: function (el, mode) { if (mode == 'toggle') mode = el.is(':hidden') ? 'show' : 'hide'; return mode }, getBaseline: function (origin, original) { var y, x; switch (origin[0]) { case 'top': y = 0; break; case 'middle': y = 0.5; break; case 'bottom': y = 1; break; default: y = origin[0] / original.height }; switch (origin[1]) { case 'left': x = 0; break; case 'center': x = 0.5; break; case 'right': x = 1; break; default: x = origin[1] / original.width }; return { x: x, y: y } }, createWrapper: function (element) { if (element.parent().is('.ui-effects-wrapper')) { return element.parent() } var props = { width: element.outerWidth(true), height: element.outerHeight(true), 'float': element.css('float') }, wrapper = $('<div></div>').addClass('ui-effects-wrapper').css({ fontSize: '100%', background: 'transparent', border: 'none', margin: 0, padding: 0 }), active = document.activeElement; try { active.id } catch (e) { active = document.body } element.wrap(wrapper); if (element[0] === active || $.contains(element[0], active)) { $(active).focus() } wrapper = element.parent(); if (element.css('position') == 'static') { wrapper.css({ position: 'relative' }); element.css({ position: 'relative' }) } else { $.extend(props, { position: element.css('position'), zIndex: element.css('z-index') }); $.each(['top', 'left', 'bottom', 'right'], function (i, pos) { props[pos] = element.css(pos); if (isNaN(parseInt(props[pos], 10))) { props[pos] = 'auto' } }); element.css({ position: 'relative', top: 0, left: 0, right: 'auto', bottom: 'auto' }) } return wrapper.css(props).show() }, removeWrapper: function (element) { var parent, active = document.activeElement; if (element.parent().is('.ui-effects-wrapper')) { parent = element.parent().replaceWith(element); if (element[0] === active || $.contains(element[0], active)) { $(active).focus() } return parent } return element }, setTransition: function (element, list, factor, value) { value = value || {}; $.each(list, function (i, x) { var unit = element.cssUnit(x); if (unit[0] > 0) value[x] = unit[0] * factor + unit[1] }); return value } }); function _normalizeArguments(effect, options, speed, callback) { if (typeof effect == 'object') { callback = options; speed = null; options = effect; effect = options.effect } if ($.isFunction(options)) { callback = options; speed = null; options = {} } if (typeof options == 'number' || $.fx.speeds[options]) { callback = speed; speed = options; options = {} } if ($.isFunction(speed)) { callback = speed; speed = null } options = options || {}; speed = speed || options.duration; speed = $.fx.off ? 0 : typeof speed == 'number' ? speed : speed in $.fx.speeds ? $.fx.speeds[speed] : $.fx.speeds._default; callback = callback || options.complete; return [effect, options, speed, callback] } function standardSpeed(speed) { if (!speed || typeof speed === "number" || $.fx.speeds[speed]) { return true } if (typeof speed === "string" && !$.effects[speed]) { return true } return false } $.fn.extend({ effect: function (effect, options, speed, callback) { var args = _normalizeArguments.apply(this, arguments), args2 = { options: args[1], duration: args[2], callback: args[3] }, mode = args2.options.mode, effectMethod = $.effects[effect]; if ($.fx.off || !effectMethod) { if (mode) { return this[mode](args2.duration, args2.callback) } else { return this.each(function () { if (args2.callback) { args2.callback.call(this) } }) } } return effectMethod.call(this, args2) }, _show: $.fn.show, show: function (speed) { if (standardSpeed(speed)) { return this._show.apply(this, arguments) } else { var args = _normalizeArguments.apply(this, arguments); args[1].mode = 'show'; return this.effect.apply(this, args) } }, _hide: $.fn.hide, hide: function (speed) { if (standardSpeed(speed)) { return this._hide.apply(this, arguments) } else { var args = _normalizeArguments.apply(this, arguments); args[1].mode = 'hide'; return this.effect.apply(this, args) } }, __toggle: $.fn.toggle, toggle: function (speed) { if (standardSpeed(speed) || typeof speed === "boolean" || $.isFunction(speed)) { return this.__toggle.apply(this, arguments) } else { var args = _normalizeArguments.apply(this, arguments); args[1].mode = 'toggle'; return this.effect.apply(this, args) } }, cssUnit: function (key) { var style = this.css(key), val = []; $.each(['em', 'px', '%', 'pt'], function (i, unit) { if (style.indexOf(unit) > 0) val = [parseFloat(style), unit] }); return val } }); $.easing.jswing = $.easing.swing; $.extend($.easing, { def: 'easeOutQuad', swing: function (x, t, b, c, d) { return $.easing[$.easing.def](x, t, b, c, d) }, easeInQuad: function (x, t, b, c, d) { return c * (t /= d) * t + b }, easeOutQuad: function (x, t, b, c, d) { return -c * (t /= d) * (t - 2) + b }, easeInOutQuad: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return c / 2 * t * t + b; return -c / 2 * ((--t) * (t - 2) - 1) + b }, easeInCubic: function (x, t, b, c, d) { return c * (t /= d) * t * t + b }, easeOutCubic: function (x, t, b, c, d) { return c * ((t = t / d - 1) * t * t + 1) + b }, easeInOutCubic: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return c / 2 * t * t * t + b; return c / 2 * ((t -= 2) * t * t + 2) + b }, easeInQuart: function (x, t, b, c, d) { return c * (t /= d) * t * t * t + b }, easeOutQuart: function (x, t, b, c, d) { return -c * ((t = t / d - 1) * t * t * t - 1) + b }, easeInOutQuart: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b; return -c / 2 * ((t -= 2) * t * t * t - 2) + b }, easeInQuint: function (x, t, b, c, d) { return c * (t /= d) * t * t * t * t + b }, easeOutQuint: function (x, t, b, c, d) { return c * ((t = t / d - 1) * t * t * t * t + 1) + b }, easeInOutQuint: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b; return c / 2 * ((t -= 2) * t * t * t * t + 2) + b }, easeInSine: function (x, t, b, c, d) { return -c * Math.cos(t / d * (Math.PI / 2)) + c + b }, easeOutSine: function (x, t, b, c, d) { return c * Math.sin(t / d * (Math.PI / 2)) + b }, easeInOutSine: function (x, t, b, c, d) { return -c / 2 * (Math.cos(Math.PI * t / d) - 1) + b }, easeInExpo: function (x, t, b, c, d) { return (t == 0) ? b : c * Math.pow(2, 10 * (t / d - 1)) + b }, easeOutExpo: function (x, t, b, c, d) { return (t == d) ? b + c : c * (-Math.pow(2, -10 * t / d) + 1) + b }, easeInOutExpo: function (x, t, b, c, d) { if (t == 0) return b; if (t == d) return b + c; if ((t /= d / 2) < 1) return c / 2 * Math.pow(2, 10 * (t - 1)) + b; return c / 2 * (-Math.pow(2, -10 * --t) + 2) + b }, easeInCirc: function (x, t, b, c, d) { return -c * (Math.sqrt(1 - (t /= d) * t) - 1) + b }, easeOutCirc: function (x, t, b, c, d) { return c * Math.sqrt(1 - (t = t / d - 1) * t) + b }, easeInOutCirc: function (x, t, b, c, d) { if ((t /= d / 2) < 1) return -c / 2 * (Math.sqrt(1 - t * t) - 1) + b; return c / 2 * (Math.sqrt(1 - (t -= 2) * t) + 1) + b }, easeInElastic: function (x, t, b, c, d) { var s = 1.70158; var p = 0; var a = c; if (t == 0) return b; if ((t /= d) == 1) return b + c; if (!p) p = d * .3; if (a < Math.abs(c)) { a = c; var s = p / 4 } else var s = p / (2 * Math.PI) * Math.asin(c / a); return -(a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b }, easeOutElastic: function (x, t, b, c, d) { var s = 1.70158; var p = 0; var a = c; if (t == 0) return b; if ((t /= d) == 1) return b + c; if (!p) p = d * .3; if (a < Math.abs(c)) { a = c; var s = p / 4 } else var s = p / (2 * Math.PI) * Math.asin(c / a); return a * Math.pow(2, -10 * t) * Math.sin((t * d - s) * (2 * Math.PI) / p) + c + b }, easeInOutElastic: function (x, t, b, c, d) { var s = 1.70158; var p = 0; var a = c; if (t == 0) return b; if ((t /= d / 2) == 2) return b + c; if (!p) p = d * (.3 * 1.5); if (a < Math.abs(c)) { a = c; var s = p / 4 } else var s = p / (2 * Math.PI) * Math.asin(c / a); if (t < 1) return -.5 * (a * Math.pow(2, 10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p)) + b; return a * Math.pow(2, -10 * (t -= 1)) * Math.sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b }, easeInBack: function (x, t, b, c, d, s) { if (s == undefined) s = 1.70158; return c * (t /= d) * t * ((s + 1) * t - s) + b }, easeOutBack: function (x, t, b, c, d, s) { if (s == undefined) s = 1.70158; return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b }, easeInOutBack: function (x, t, b, c, d, s) { if (s == undefined) s = 1.70158; if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b; return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b }, easeInBounce: function (x, t, b, c, d) { return c - $.easing.easeOutBounce(x, d - t, 0, c, d) + b }, easeOutBounce: function (x, t, b, c, d) { if ((t /= d) < (1 / 2.75)) { return c * (7.5625 * t * t) + b } else if (t < (2 / 2.75)) { return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b } else if (t < (2.5 / 2.75)) { return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b } else { return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b } }, easeInOutBounce: function (x, t, b, c, d) { if (t < d / 2) return $.easing.easeInBounce(x, t * 2, 0, c, d) * .5 + b; return $.easing.easeOutBounce(x, t * 2 - d, 0, c, d) * .5 + c * .5 + b } }) })(jQuery); (function ($, undefined) { $.effects.blind = function (o) { return this.queue(function () { var el = $(this), props = ['position', 'top', 'bottom', 'left', 'right']; var mode = $.effects.setMode(el, o.options.mode || 'hide'); var direction = o.options.direction || 'vertical'; $.effects.save(el, props); el.show(); var wrapper = $.effects.createWrapper(el).css({ overflow: 'hidden' }); var ref = (direction == 'vertical') ? 'height' : 'width'; var distance = (direction == 'vertical') ? wrapper.height() : wrapper.width(); if (mode == 'show') wrapper.css(ref, 0); var animation = {}; animation[ref] = mode == 'show' ? distance : 0; wrapper.animate(animation, o.duration, o.options.easing, function () { if (mode == 'hide') el.hide(); $.effects.restore(el, props); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(el[0], arguments); el.dequeue() }) }) } })(jQuery); (function ($, undefined) { $.effects.bounce = function (o) { return this.queue(function () { var el = $(this), props = ['position', 'top', 'bottom', 'left', 'right']; var mode = $.effects.setMode(el, o.options.mode || 'effect'); var direction = o.options.direction || 'up'; var distance = o.options.distance || 20; var times = o.options.times || 5; var speed = o.duration || 250; if (/show|hide/.test(mode)) props.push('opacity'); $.effects.save(el, props); el.show(); $.effects.createWrapper(el); var ref = (direction == 'up' || direction == 'down') ? 'top' : 'left'; var motion = (direction == 'up' || direction == 'left') ? 'pos' : 'neg'; var distance = o.options.distance || (ref == 'top' ? el.outerHeight(true) / 3 : el.outerWidth(true) / 3); if (mode == 'show') el.css('opacity', 0).css(ref, motion == 'pos' ? -distance : distance); if (mode == 'hide') distance = distance / (times * 2); if (mode != 'hide') times--; if (mode == 'show') { var animation = { opacity: 1 }; animation[ref] = (motion == 'pos' ? '+=' : '-=') + distance; el.animate(animation, speed / 2, o.options.easing); distance = distance / 2; times-- }; for (var i = 0; i < times; i++) { var animation1 = {}, animation2 = {}; animation1[ref] = (motion == 'pos' ? '-=' : '+=') + distance; animation2[ref] = (motion == 'pos' ? '+=' : '-=') + distance; el.animate(animation1, speed / 2, o.options.easing).animate(animation2, speed / 2, o.options.easing); distance = (mode == 'hide') ? distance * 2 : distance / 2 }; if (mode == 'hide') { var animation = { opacity: 0 }; animation[ref] = (motion == 'pos' ? '-=' : '+=') + distance; el.animate(animation, speed / 2, o.options.easing, function () { el.hide(); $.effects.restore(el, props); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(this, arguments) }) } else { var animation1 = {}, animation2 = {}; animation1[ref] = (motion == 'pos' ? '-=' : '+=') + distance; animation2[ref] = (motion == 'pos' ? '+=' : '-=') + distance; el.animate(animation1, speed / 2, o.options.easing).animate(animation2, speed / 2, o.options.easing, function () { $.effects.restore(el, props); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(this, arguments) }) }; el.queue('fx', function () { el.dequeue() }); el.dequeue() }) } })(jQuery); (function ($, undefined) { $.effects.clip = function (o) { return this.queue(function () { var el = $(this), props = ['position', 'top', 'bottom', 'left', 'right', 'height', 'width']; var mode = $.effects.setMode(el, o.options.mode || 'hide'); var direction = o.options.direction || 'vertical'; $.effects.save(el, props); el.show(); var wrapper = $.effects.createWrapper(el).css({ overflow: 'hidden' }); var animate = el[0].tagName == 'IMG' ? wrapper : el; var ref = { size: (direction == 'vertical') ? 'height' : 'width', position: (direction == 'vertical') ? 'top' : 'left' }; var distance = (direction == 'vertical') ? animate.height() : animate.width(); if (mode == 'show') { animate.css(ref.size, 0); animate.css(ref.position, distance / 2) } var animation = {}; animation[ref.size] = mode == 'show' ? distance : 0; animation[ref.position] = mode == 'show' ? 0 : distance / 2; animate.animate(animation, { queue: false, duration: o.duration, easing: o.options.easing, complete: function () { if (mode == 'hide') el.hide(); $.effects.restore(el, props); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(el[0], arguments); el.dequeue() } }) }) } })(jQuery); (function ($, undefined) { $.effects.drop = function (o) { return this.queue(function () { var el = $(this), props = ['position', 'top', 'bottom', 'left', 'right', 'opacity']; var mode = $.effects.setMode(el, o.options.mode || 'hide'); var direction = o.options.direction || 'left'; $.effects.save(el, props); el.show(); $.effects.createWrapper(el); var ref = (direction == 'up' || direction == 'down') ? 'top' : 'left'; var motion = (direction == 'up' || direction == 'left') ? 'pos' : 'neg'; var distance = o.options.distance || (ref == 'top' ? el.outerHeight(true) / 2 : el.outerWidth(true) / 2); if (mode == 'show') el.css('opacity', 0).css(ref, motion == 'pos' ? -distance : distance); var animation = { opacity: mode == 'show' ? 1 : 0 }; animation[ref] = (mode == 'show' ? (motion == 'pos' ? '+=' : '-=') : (motion == 'pos' ? '-=' : '+=')) + distance; el.animate(animation, { queue: false, duration: o.duration, easing: o.options.easing, complete: function () { if (mode == 'hide') el.hide(); $.effects.restore(el, props); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(this, arguments); el.dequeue() } }) }) } })(jQuery); (function ($, undefined) { $.effects.explode = function (o) { return this.queue(function () { var rows = o.options.pieces ? Math.round(Math.sqrt(o.options.pieces)) : 3; var cells = o.options.pieces ? Math.round(Math.sqrt(o.options.pieces)) : 3; o.options.mode = o.options.mode == 'toggle' ? ($(this).is(':visible') ? 'hide' : 'show') : o.options.mode; var el = $(this).show().css('visibility', 'hidden'); var offset = el.offset(); offset.top -= parseInt(el.css("marginTop"), 10) || 0; offset.left -= parseInt(el.css("marginLeft"), 10) || 0; var width = el.outerWidth(true); var height = el.outerHeight(true); for (var i = 0; i < rows; i++) { for (var j = 0; j < cells; j++) { el.clone().appendTo('body').wrap('<div></div>').css({ position: 'absolute', visibility: 'visible', left: -j * (width / cells), top: -i * (height / rows) }).parent().addClass('ui-effects-explode').css({ position: 'absolute', overflow: 'hidden', width: width / cells, height: height / rows, left: offset.left + j * (width / cells) + (o.options.mode == 'show' ? (j - Math.floor(cells / 2)) * (width / cells) : 0), top: offset.top + i * (height / rows) + (o.options.mode == 'show' ? (i - Math.floor(rows / 2)) * (height / rows) : 0), opacity: o.options.mode == 'show' ? 0 : 1 }).animate({ left: offset.left + j * (width / cells) + (o.options.mode == 'show' ? 0 : (j - Math.floor(cells / 2)) * (width / cells)), top: offset.top + i * (height / rows) + (o.options.mode == 'show' ? 0 : (i - Math.floor(rows / 2)) * (height / rows)), opacity: o.options.mode == 'show' ? 1 : 0 }, o.duration || 500) } } setTimeout(function () { o.options.mode == 'show' ? el.css({ visibility: 'visible' }) : el.css({ visibility: 'visible' }).hide(); if (o.callback) o.callback.apply(el[0]); el.dequeue(); $('div.ui-effects-explode').remove() }, o.duration || 500) }) } })(jQuery); (function ($, undefined) { $.effects.fade = function (o) { return this.queue(function () { var elem = $(this), mode = $.effects.setMode(elem, o.options.mode || 'hide'); elem.animate({ opacity: mode }, { queue: false, duration: o.duration, easing: o.options.easing, complete: function () { (o.callback && o.callback.apply(this, arguments)); elem.dequeue() } }) }) } })(jQuery); (function ($, undefined) { $.effects.fold = function (o) { return this.queue(function () { var el = $(this), props = ['position', 'top', 'bottom', 'left', 'right']; var mode = $.effects.setMode(el, o.options.mode || 'hide'); var size = o.options.size || 15; var horizFirst = !(!o.options.horizFirst); var duration = o.duration ? o.duration / 2 : $.fx.speeds._default / 2; $.effects.save(el, props); el.show(); var wrapper = $.effects.createWrapper(el).css({ overflow: 'hidden' }); var widthFirst = ((mode == 'show') != horizFirst); var ref = widthFirst ? ['width', 'height'] : ['height', 'width']; var distance = widthFirst ? [wrapper.width(), wrapper.height()] : [wrapper.height(), wrapper.width()]; var percent = /([0-9]+)%/.exec(size); if (percent) size = parseInt(percent[1], 10) / 100 * distance[mode == 'hide' ? 0 : 1]; if (mode == 'show') wrapper.css(horizFirst ? { height: 0, width: size } : { height: size, width: 0 }); var animation1 = {}, animation2 = {}; animation1[ref[0]] = mode == 'show' ? distance[0] : size; animation2[ref[1]] = mode == 'show' ? distance[1] : 0; wrapper.animate(animation1, duration, o.options.easing).animate(animation2, duration, o.options.easing, function () { if (mode == 'hide') el.hide(); $.effects.restore(el, props); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(el[0], arguments); el.dequeue() }) }) } })(jQuery); (function ($, undefined) { $.effects.highlight = function (o) { return this.queue(function () { var elem = $(this), props = ['backgroundImage', 'backgroundColor', 'opacity'], mode = $.effects.setMode(elem, o.options.mode || 'show'), animation = { backgroundColor: elem.css('backgroundColor') }; if (mode == 'hide') { animation.opacity = 0 } $.effects.save(elem, props); elem.show().css({ backgroundImage: 'none', backgroundColor: o.options.color || '#ffff99' }).animate(animation, { queue: false, duration: o.duration, easing: o.options.easing, complete: function () { (mode == 'hide' && elem.hide()); $.effects.restore(elem, props); (mode == 'show' && !$.support.opacity && this.style.removeAttribute('filter')); (o.callback && o.callback.apply(this, arguments)); elem.dequeue() } }) }) } })(jQuery); (function ($, undefined) { $.effects.pulsate = function (o) { return this.queue(function () { var elem = $(this), mode = $.effects.setMode(elem, o.options.mode || 'show'), times = ((o.options.times || 5) * 2) - 1, duration = o.duration ? o.duration / 2 : $.fx.speeds._default / 2, isVisible = elem.is(':visible'), animateTo = 0; if (!isVisible) { elem.css('opacity', 0).show(); animateTo = 1 } if ((mode == 'hide' && isVisible) || (mode == 'show' && !isVisible)) { times-- } for (var i = 0; i < times; i++) { elem.animate({ opacity: animateTo }, duration, o.options.easing); animateTo = (animateTo + 1) % 2 } elem.animate({ opacity: animateTo }, duration, o.options.easing, function () { if (animateTo == 0) { elem.hide() } (o.callback && o.callback.apply(this, arguments)) }); elem.queue('fx', function () { elem.dequeue() }).dequeue() }) } })(jQuery); (function ($, undefined) { $.effects.puff = function (o) { return this.queue(function () { var elem = $(this), mode = $.effects.setMode(elem, o.options.mode || 'hide'), percent = parseInt(o.options.percent, 10) || 150, factor = percent / 100, original = { height: elem.height(), width: elem.width() }; $.extend(o.options, { fade: true, mode: mode, percent: mode == 'hide' ? percent : 100, from: mode == 'hide' ? original : { height: original.height * factor, width: original.width * factor } }); elem.effect('scale', o.options, o.duration, o.callback); elem.dequeue() }) }; $.effects.scale = function (o) { return this.queue(function () { var el = $(this); var options = $.extend(true, {}, o.options); var mode = $.effects.setMode(el, o.options.mode || 'effect'); var percent = parseInt(o.options.percent, 10) || (parseInt(o.options.percent, 10) == 0 ? 0 : (mode == 'hide' ? 0 : 100)); var direction = o.options.direction || 'both'; var origin = o.options.origin; if (mode != 'effect') { options.origin = origin || ['middle', 'center']; options.restore = true } var original = { height: el.height(), width: el.width() }; el.from = o.options.from || (mode == 'show' ? { height: 0, width: 0 } : original); var factor = { y: direction != 'horizontal' ? (percent / 100) : 1, x: direction != 'vertical' ? (percent / 100) : 1 }; el.to = { height: original.height * factor.y, width: original.width * factor.x }; if (o.options.fade) { if (mode == 'show') { el.from.opacity = 0; el.to.opacity = 1 }; if (mode == 'hide') { el.from.opacity = 1; el.to.opacity = 0 } }; options.from = el.from; options.to = el.to; options.mode = mode; el.effect('size', options, o.duration, o.callback); el.dequeue() }) }; $.effects.size = function (o) { return this.queue(function () { var el = $(this), props = ['position', 'top', 'bottom', 'left', 'right', 'width', 'height', 'overflow', 'opacity']; var props1 = ['position', 'top', 'bottom', 'left', 'right', 'overflow', 'opacity']; var props2 = ['width', 'height', 'overflow']; var cProps = ['fontSize']; var vProps = ['borderTopWidth', 'borderBottomWidth', 'paddingTop', 'paddingBottom']; var hProps = ['borderLeftWidth', 'borderRightWidth', 'paddingLeft', 'paddingRight']; var mode = $.effects.setMode(el, o.options.mode || 'effect'); var restore = o.options.restore || false; var scale = o.options.scale || 'both'; var origin = o.options.origin; var original = { height: el.height(), width: el.width() }; el.from = o.options.from || original; el.to = o.options.to || original; if (origin) { var baseline = $.effects.getBaseline(origin, original); el.from.top = (original.height - el.from.height) * baseline.y; el.from.left = (original.width - el.from.width) * baseline.x; el.to.top = (original.height - el.to.height) * baseline.y; el.to.left = (original.width - el.to.width) * baseline.x }; var factor = { from: { y: el.from.height / original.height, x: el.from.width / original.width }, to: { y: el.to.height / original.height, x: el.to.width / original.width } }; if (scale == 'box' || scale == 'both') { if (factor.from.y != factor.to.y) { props = props.concat(vProps); el.from = $.effects.setTransition(el, vProps, factor.from.y, el.from); el.to = $.effects.setTransition(el, vProps, factor.to.y, el.to) }; if (factor.from.x != factor.to.x) { props = props.concat(hProps); el.from = $.effects.setTransition(el, hProps, factor.from.x, el.from); el.to = $.effects.setTransition(el, hProps, factor.to.x, el.to) } }; if (scale == 'content' || scale == 'both') { if (factor.from.y != factor.to.y) { props = props.concat(cProps); el.from = $.effects.setTransition(el, cProps, factor.from.y, el.from); el.to = $.effects.setTransition(el, cProps, factor.to.y, el.to) } }; $.effects.save(el, restore ? props : props1); el.show(); $.effects.createWrapper(el); el.css('overflow', 'hidden').css(el.from); if (scale == 'content' || scale == 'both') { vProps = vProps.concat(['marginTop', 'marginBottom']).concat(cProps); hProps = hProps.concat(['marginLeft', 'marginRight']); props2 = props.concat(vProps).concat(hProps); el.find("*[width]").each(function () { var child = $(this); if (restore) $.effects.save(child, props2); var c_original = { height: child.height(), width: child.width() }; child.from = { height: c_original.height * factor.from.y, width: c_original.width * factor.from.x }; child.to = { height: c_original.height * factor.to.y, width: c_original.width * factor.to.x }; if (factor.from.y != factor.to.y) { child.from = $.effects.setTransition(child, vProps, factor.from.y, child.from); child.to = $.effects.setTransition(child, vProps, factor.to.y, child.to) }; if (factor.from.x != factor.to.x) { child.from = $.effects.setTransition(child, hProps, factor.from.x, child.from); child.to = $.effects.setTransition(child, hProps, factor.to.x, child.to) }; child.css(child.from); child.animate(child.to, o.duration, o.options.easing, function () { if (restore) $.effects.restore(child, props2) }) }) }; el.animate(el.to, { queue: false, duration: o.duration, easing: o.options.easing, complete: function () { if (el.to.opacity === 0) { el.css('opacity', el.from.opacity) } if (mode == 'hide') el.hide(); $.effects.restore(el, restore ? props : props1); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(this, arguments); el.dequeue() } }) }) } })(jQuery); (function ($, undefined) { $.effects.shake = function (o) { return this.queue(function () { var el = $(this), props = ['position', 'top', 'bottom', 'left', 'right']; var mode = $.effects.setMode(el, o.options.mode || 'effect'); var direction = o.options.direction || 'left'; var distance = o.options.distance || 20; var times = o.options.times || 3; var speed = o.duration || o.options.duration || 140; $.effects.save(el, props); el.show(); $.effects.createWrapper(el); var ref = (direction == 'up' || direction == 'down') ? 'top' : 'left'; var motion = (direction == 'up' || direction == 'left') ? 'pos' : 'neg'; var animation = {}, animation1 = {}, animation2 = {}; animation[ref] = (motion == 'pos' ? '-=' : '+=') + distance; animation1[ref] = (motion == 'pos' ? '+=' : '-=') + distance * 2; animation2[ref] = (motion == 'pos' ? '-=' : '+=') + distance * 2; el.animate(animation, speed, o.options.easing); for (var i = 1; i < times; i++) { el.animate(animation1, speed, o.options.easing).animate(animation2, speed, o.options.easing) }; el.animate(animation1, speed, o.options.easing).animate(animation, speed / 2, o.options.easing, function () { $.effects.restore(el, props); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(this, arguments) }); el.queue('fx', function () { el.dequeue() }); el.dequeue() }) } })(jQuery); (function ($, undefined) { $.effects.slide = function (o) { return this.queue(function () { var el = $(this), props = ['position', 'top', 'bottom', 'left', 'right']; var mode = $.effects.setMode(el, o.options.mode || 'show'); var direction = o.options.direction || 'left'; $.effects.save(el, props); el.show(); $.effects.createWrapper(el).css({ overflow: 'hidden' }); var ref = (direction == 'up' || direction == 'down') ? 'top' : 'left'; var motion = (direction == 'up' || direction == 'left') ? 'pos' : 'neg'; var distance = o.options.distance || (ref == 'top' ? el.outerHeight(true) : el.outerWidth(true)); if (mode == 'show') el.css(ref, motion == 'pos' ? (isNaN(distance) ? "-" + distance : -distance) : distance); var animation = {}; animation[ref] = (mode == 'show' ? (motion == 'pos' ? '+=' : '-=') : (motion == 'pos' ? '-=' : '+=')) + distance; el.animate(animation, { queue: false, duration: o.duration, easing: o.options.easing, complete: function () { if (mode == 'hide') el.hide(); $.effects.restore(el, props); $.effects.removeWrapper(el); if (o.callback) o.callback.apply(this, arguments); el.dequeue() } }) }) } })(jQuery); (function ($, undefined) { $.effects.transfer = function (o) { return this.queue(function () { var elem = $(this), target = $(o.options.to), endPosition = target.offset(), animation = { top: endPosition.top, left: endPosition.left, height: target.innerHeight(), width: target.innerWidth() }, startPosition = elem.offset(), transfer = $('<div class="ui-effects-transfer"></div>').appendTo(document.body).addClass(o.options.className).css({ top: startPosition.top, left: startPosition.left, height: elem.innerHeight(), width: elem.innerWidth(), position: 'absolute' }).animate(animation, o.duration, o.options.easing, function () { transfer.remove(); (o.callback && o.callback.apply(elem[0], arguments)); elem.dequeue() }) }) } })(jQuery); (function ($, undefined) { $.widget("ui.accordion", { options: { active: 0, animated: "slide", autoHeight: true, clearStyle: false, collapsible: false, event: "click", fillSpace: false, header: "> li > :first-child,> :not(li):even", icons: { header: "ui-icon-triangle-1-e", headerSelected: "ui-icon-triangle-1-s" }, navigation: false, navigationFilter: function () { return this.href.toLowerCase() === location.href.toLowerCase() } }, _create: function () { var self = this, options = self.options; self.running = 0; self.element.addClass("ui-accordion ui-widget ui-helper-reset").children("li").addClass("ui-accordion-li-fix"); self.headers = self.element.find(options.header).addClass("ui-accordion-header ui-helper-reset ui-state-default ui-corner-all").bind("mouseenter.accordion", function () { if (options.disabled) { return } $(this).addClass("ui-state-hover") }).bind("mouseleave.accordion", function () { if (options.disabled) { return } $(this).removeClass("ui-state-hover") }).bind("focus.accordion", function () { if (options.disabled) { return } $(this).addClass("ui-state-focus") }).bind("blur.accordion", function () { if (options.disabled) { return } $(this).removeClass("ui-state-focus") }); self.headers.next().addClass("ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom"); if (options.navigation) { var current = self.element.find("a").filter(options.navigationFilter).eq(0); if (current.length) { var header = current.closest(".ui-accordion-header"); if (header.length) { self.active = header } else { self.active = current.closest(".ui-accordion-content").prev() } } } self.active = self._findActive(self.active || options.active).addClass("ui-state-default ui-state-active").toggleClass("ui-corner-all").toggleClass("ui-corner-top"); self.active.next().addClass("ui-accordion-content-active"); self._createIcons(); self.resize(); self.element.attr("role", "tablist"); self.headers.attr("role", "tab").bind("keydown.accordion", function (event) { return self._keydown(event) }).next().attr("role", "tabpanel"); self.headers.not(self.active || "").attr({ "aria-expanded": "false", "aria-selected": "false", tabIndex: -1 }).next().hide(); if (!self.active.length) { self.headers.eq(0).attr("tabIndex", 0) } else { self.active.attr({ "aria-expanded": "true", "aria-selected": "true", tabIndex: 0 }) } if (!$.browser.safari) { self.headers.find("a").attr("tabIndex", -1) } if (options.event) { self.headers.bind(options.event.split(" ").join(".accordion ") + ".accordion", function (event) { self._clickHandler.call(self, event, this); event.preventDefault() }) } }, _createIcons: function () { var options = this.options; if (options.icons) { $("<span></span>").addClass("ui-icon " + options.icons.header).prependTo(this.headers); this.active.children(".ui-icon").toggleClass(options.icons.header).toggleClass(options.icons.headerSelected); this.element.addClass("ui-accordion-icons") } }, _destroyIcons: function () { this.headers.children(".ui-icon").remove(); this.element.removeClass("ui-accordion-icons") }, destroy: function () { var options = this.options; this.element.removeClass("ui-accordion ui-widget ui-helper-reset").removeAttr("role"); this.headers.unbind(".accordion").removeClass("ui-accordion-header ui-accordion-disabled ui-helper-reset ui-state-default ui-corner-all ui-state-active ui-state-disabled ui-corner-top").removeAttr("role").removeAttr("aria-expanded").removeAttr("aria-selected").removeAttr("tabIndex"); this.headers.find("a").removeAttr("tabIndex"); this._destroyIcons(); var contents = this.headers.next().css("display", "").removeAttr("role").removeClass("ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content ui-accordion-content-active ui-accordion-disabled ui-state-disabled"); if (options.autoHeight || options.fillHeight) { contents.css("height", "") } return $.Widget.prototype.destroy.call(this) }, _setOption: function (key, value) { $.Widget.prototype._setOption.apply(this, arguments); if (key == "active") { this.activate(value) } if (key == "icons") { this._destroyIcons(); if (value) { this._createIcons() } } if (key == "disabled") { this.headers.add(this.headers.next())[value ? "addClass" : "removeClass"]("ui-accordion-disabled ui-state-disabled") } }, _keydown: function (event) { if (this.options.disabled || event.altKey || event.ctrlKey) { return } var keyCode = $.ui.keyCode, length = this.headers.length, currentIndex = this.headers.index(event.target), toFocus = false; switch (event.keyCode) { case keyCode.RIGHT: case keyCode.DOWN: toFocus = this.headers[(currentIndex + 1) % length]; break; case keyCode.LEFT: case keyCode.UP: toFocus = this.headers[(currentIndex - 1 + length) % length]; break; case keyCode.SPACE: case keyCode.ENTER: this._clickHandler({ target: event.target }, event.target); event.preventDefault() } if (toFocus) { $(event.target).attr("tabIndex", -1); $(toFocus).attr("tabIndex", 0); toFocus.focus(); return false } return true }, resize: function () { var options = this.options, maxHeight; if (options.fillSpace) { if ($.browser.msie) { var defOverflow = this.element.parent().css("overflow"); this.element.parent().css("overflow", "hidden") } maxHeight = this.element.parent().height(); if ($.browser.msie) { this.element.parent().css("overflow", defOverflow) } this.headers.each(function () { maxHeight -= $(this).outerHeight(true) }); this.headers.next().each(function () { $(this).height(Math.max(0, maxHeight - $(this).innerHeight() + $(this).height())) }).css("overflow", "auto") } else if (options.autoHeight) { maxHeight = 0; this.headers.next().each(function () { maxHeight = Math.max(maxHeight, $(this).height("").height()) }).height(maxHeight) } return this }, activate: function (index) { this.options.active = index; var active = this._findActive(index)[0]; this._clickHandler({ target: active }, active); return this }, _findActive: function (selector) { return selector ? typeof selector === "number" ? this.headers.filter(":eq(" + selector + ")") : this.headers.not(this.headers.not(selector)) : selector === false ? $([]) : this.headers.filter(":eq(0)") }, _clickHandler: function (event, target) { var options = this.options; if (options.disabled) { return } if (!event.target) { if (!options.collapsible) { return } this.active.removeClass("ui-state-active ui-corner-top").addClass("ui-state-default ui-corner-all").children(".ui-icon").removeClass(options.icons.headerSelected).addClass(options.icons.header); this.active.next().addClass("ui-accordion-content-active"); var toHide = this.active.next(), data = { options: options, newHeader: $([]), oldHeader: options.active, newContent: $([]), oldContent: toHide }, toShow = (this.active = $([])); this._toggle(toShow, toHide, data); return } var clicked = $(event.currentTarget || target), clickedIsActive = clicked[0] === this.active[0]; options.active = options.collapsible && clickedIsActive ? false : this.headers.index(clicked); if (this.running || (!options.collapsible && clickedIsActive)) { return } var active = this.active, toShow = clicked.next(), toHide = this.active.next(), data = { options: options, newHeader: clickedIsActive && options.collapsible ? $([]) : clicked, oldHeader: this.active, newContent: clickedIsActive && options.collapsible ? $([]) : toShow, oldContent: toHide }, down = this.headers.index(this.active[0]) > this.headers.index(clicked[0]); this.active = clickedIsActive ? $([]) : clicked; this._toggle(toShow, toHide, data, clickedIsActive, down); active.removeClass("ui-state-active ui-corner-top").addClass("ui-state-default ui-corner-all").children(".ui-icon").removeClass(options.icons.headerSelected).addClass(options.icons.header); if (!clickedIsActive) { clicked.removeClass("ui-state-default ui-corner-all").addClass("ui-state-active ui-corner-top").children(".ui-icon").removeClass(options.icons.header).addClass(options.icons.headerSelected); clicked.next().addClass("ui-accordion-content-active") } return }, _toggle: function (toShow, toHide, data, clickedIsActive, down) { var self = this, options = self.options; self.toShow = toShow; self.toHide = toHide; self.data = data; var complete = function () { if (!self) { return } return self._completed.apply(self, arguments) }; self._trigger("changestart", null, self.data); self.running = toHide.size() === 0 ? toShow.size() : toHide.size(); if (options.animated) { var animOptions = {}; if (options.collapsible && clickedIsActive) { animOptions = { toShow: $([]), toHide: toHide, complete: complete, down: down, autoHeight: options.autoHeight || options.fillSpace } } else { animOptions = { toShow: toShow, toHide: toHide, complete: complete, down: down, autoHeight: options.autoHeight || options.fillSpace } } if (!options.proxied) { options.proxied = options.animated } if (!options.proxiedDuration) { options.proxiedDuration = options.duration } options.animated = $.isFunction(options.proxied) ? options.proxied(animOptions) : options.proxied; options.duration = $.isFunction(options.proxiedDuration) ? options.proxiedDuration(animOptions) : options.proxiedDuration; var animations = $.ui.accordion.animations, duration = options.duration, easing = options.animated; if (easing && !animations[easing] && !$.easing[easing]) { easing = "slide" } if (!animations[easing]) { animations[easing] = function (options) { this.slide(options, { easing: easing, duration: duration || 700 }) } } animations[easing](animOptions) } else { if (options.collapsible && clickedIsActive) { toShow.toggle() } else { toHide.hide(); toShow.show() } complete(true) } toHide.prev().attr({ "aria-expanded": "false", "aria-selected": "false", tabIndex: -1 }).blur(); toShow.prev().attr({ "aria-expanded": "true", "aria-selected": "true", tabIndex: 0 }).focus() }, _completed: function (cancel) { this.running = cancel ? 0 : --this.running; if (this.running) { return } if (this.options.clearStyle) { this.toShow.add(this.toHide).css({ height: "", overflow: "" }) } this.toHide.removeClass("ui-accordion-content-active"); if (this.toHide.length) { this.toHide.parent()[0].className = this.toHide.parent()[0].className } this._trigger("change", null, this.data) } }); $.extend($.ui.accordion, { version: "1.8.22", animations: { slide: function (options, additions) { options = $.extend({ easing: "swing", duration: 300 }, options, additions); if (!options.toHide.size()) { options.toShow.animate({ height: "show", paddingTop: "show", paddingBottom: "show" }, options); return } if (!options.toShow.size()) { options.toHide.animate({ height: "hide", paddingTop: "hide", paddingBottom: "hide" }, options); return } var overflow = options.toShow.css("overflow"), percentDone = 0, showProps = {}, hideProps = {}, fxAttrs = ["height", "paddingTop", "paddingBottom"], originalWidth; var s = options.toShow; originalWidth = s[0].style.width; s.width(s.parent().width() - parseFloat(s.css("paddingLeft")) - parseFloat(s.css("paddingRight")) - (parseFloat(s.css("borderLeftWidth")) || 0) - (parseFloat(s.css("borderRightWidth")) || 0)); $.each(fxAttrs, function (i, prop) { hideProps[prop] = "hide"; var parts = ("" + $.css(options.toShow[0], prop)).match(/^([\d+-.]+)(.*)$/); showProps[prop] = { value: parts[1], unit: parts[2] || "px" } }); options.toShow.css({ height: 0, overflow: "hidden" }).show(); options.toHide.filter(":hidden").each(options.complete).end().filter(":visible").animate(hideProps, { step: function (now, settings) { if (settings.prop == "height") { percentDone = (settings.end - settings.start === 0) ? 0 : (settings.now - settings.start) / (settings.end - settings.start) } options.toShow[0].style[settings.prop] = (percentDone * showProps[settings.prop].value) + showProps[settings.prop].unit }, duration: options.duration, easing: options.easing, complete: function () { if (!options.autoHeight) { options.toShow.css("height", "") } options.toShow.css({ width: originalWidth, overflow: overflow }); options.complete() } }) }, bounceslide: function (options) { this.slide(options, { easing: options.down ? "easeOutBounce" : "swing", duration: options.down ? 1000 : 200 }) } } }) })(jQuery); (function ($, undefined) { var requestIndex = 0; $.widget("ui.autocomplete", { options: { appendTo: "body", autoFocus: false, delay: 300, minLength: 1, position: { my: "left top", at: "left bottom", collision: "none" }, source: null }, pending: 0, _create: function () { var self = this, doc = this.element[0].ownerDocument, suppressKeyPress; this.isMultiLine = this.element.is("textarea"); this.element.addClass("ui-autocomplete-input").attr("autocomplete", "off").attr({ role: "textbox", "aria-autocomplete": "list", "aria-haspopup": "true" }).bind("keydown.autocomplete", function (event) { if (self.options.disabled || self.element.propAttr("readOnly")) { return } suppressKeyPress = false; var keyCode = $.ui.keyCode; switch (event.keyCode) { case keyCode.PAGE_UP: self._move("previousPage", event); break; case keyCode.PAGE_DOWN: self._move("nextPage", event); break; case keyCode.UP: self._keyEvent("previous", event); break; case keyCode.DOWN: self._keyEvent("next", event); break; case keyCode.ENTER: case keyCode.NUMPAD_ENTER: if (self.menu.active) { suppressKeyPress = true; event.preventDefault() } case keyCode.TAB: if (!self.menu.active) { return } self.menu.select(event); break; case keyCode.ESCAPE: self.element.val(self.term); self.close(event); break; default: clearTimeout(self.searching); self.searching = setTimeout(function () { if (self.term != self.element.val()) { self.selectedItem = null; self.search(null, event) } }, self.options.delay); break } }).bind("keypress.autocomplete", function (event) { if (suppressKeyPress) { suppressKeyPress = false; event.preventDefault() } }).bind("focus.autocomplete", function () { if (self.options.disabled) { return } self.selectedItem = null; self.previous = self.element.val() }).bind("blur.autocomplete", function (event) { if (self.options.disabled) { return } clearTimeout(self.searching); self.closing = setTimeout(function () { self.close(event); self._change(event) }, 150) }); this._initSource(); this.menu = $("<ul></ul>").addClass("ui-autocomplete").appendTo($(this.options.appendTo || "body", doc)[0]).mousedown(function (event) { var menuElement = self.menu.element[0]; if (!$(event.target).closest(".ui-menu-item").length) { setTimeout(function () { $(document).one('mousedown', function (event) { if (event.target !== self.element[0] && event.target !== menuElement && !$.ui.contains(menuElement, event.target)) { self.close() } }) }, 1) } setTimeout(function () { clearTimeout(self.closing) }, 13) }).menu({ focus: function (event, ui) { var item = ui.item.data("item.autocomplete"); if (false !== self._trigger("focus", event, { item: item })) { if (/^key/.test(event.originalEvent.type)) { self.element.val(item.value) } } }, selected: function (event, ui) { var item = ui.item.data("item.autocomplete"), previous = self.previous; if (self.element[0] !== doc.activeElement) { self.element.focus(); self.previous = previous; setTimeout(function () { self.previous = previous; self.selectedItem = item }, 1) } if (false !== self._trigger("select", event, { item: item })) { self.element.val(item.value) } self.term = self.element.val(); self.close(event); self.selectedItem = item }, blur: function (event, ui) { if (self.menu.element.is(":visible") && (self.element.val() !== self.term)) { self.element.val(self.term) } } }).zIndex(this.element.zIndex() + 1).css({ top: 0, left: 0 }).hide().data("menu"); if ($.fn.bgiframe) { this.menu.element.bgiframe() } self.beforeunloadHandler = function () { self.element.removeAttr("autocomplete") }; $(window).bind("beforeunload", self.beforeunloadHandler) }, destroy: function () { this.element.removeClass("ui-autocomplete-input").removeAttr("autocomplete").removeAttr("role").removeAttr("aria-autocomplete").removeAttr("aria-haspopup"); this.menu.element.remove(); $(window).unbind("beforeunload", this.beforeunloadHandler); $.Widget.prototype.destroy.call(this) }, _setOption: function (key, value) { $.Widget.prototype._setOption.apply(this, arguments); if (key === "source") { this._initSource() } if (key === "appendTo") { this.menu.element.appendTo($(value || "body", this.element[0].ownerDocument)[0]) } if (key === "disabled" && value && this.xhr) { this.xhr.abort() } }, _initSource: function () { var self = this, array, url; if ($.isArray(this.options.source)) { array = this.options.source; this.source = function (request, response) { response($.ui.autocomplete.filter(array, request.term)) } } else if (typeof this.options.source === "string") { url = this.options.source; this.source = function (request, response) { if (self.xhr) { self.xhr.abort() } self.xhr = $.ajax({ url: url, data: request, dataType: "json", success: function (data, status) { response(data) }, error: function () { response([]) } }) } } else { this.source = this.options.source } }, search: function (value, event) { value = value != null ? value : this.element.val(); this.term = this.element.val(); if (value.length < this.options.minLength) { return this.close(event) } clearTimeout(this.closing); if (this._trigger("search", event) === false) { return } return this._search(value) }, _search: function (value) { this.pending++; this.element.addClass("ui-autocomplete-loading"); this.source({ term: value }, this._response()) }, _response: function () { var that = this, index = ++requestIndex; return function (content) { if (index === requestIndex) { that.__response(content) } that.pending--; if (!that.pending) { that.element.removeClass("ui-autocomplete-loading") } } }, __response: function (content) { if (!this.options.disabled && content && content.length) { content = this._normalize(content); this._suggest(content); this._trigger("open") } else { this.close() } }, close: function (event) { clearTimeout(this.closing); if (this.menu.element.is(":visible")) { this.menu.element.hide(); this.menu.deactivate(); this._trigger("close", event) } }, _change: function (event) { if (this.previous !== this.element.val()) { this._trigger("change", event, { item: this.selectedItem }) } }, _normalize: function (items) { if (items.length && items[0].label && items[0].value) { return items } return $.map(items, function (item) { if (typeof item === "string") { return { label: item, value: item } } return $.extend({ label: item.label || item.value, value: item.value || item.label }, item) }) }, _suggest: function (items) { var ul = this.menu.element.empty().zIndex(this.element.zIndex() + 1); this._renderMenu(ul, items); this.menu.deactivate(); this.menu.refresh(); ul.show(); this._resizeMenu(); ul.position($.extend({ of: this.element }, this.options.position)); if (this.options.autoFocus) { this.menu.next(new $.Event("mouseover")) } }, _resizeMenu: function () { var ul = this.menu.element; ul.outerWidth(Math.max(ul.width("").outerWidth() + 1, this.element.outerWidth())) }, _renderMenu: function (ul, items) { var self = this; $.each(items, function (index, item) { self._renderItem(ul, item) }) }, _renderItem: function (ul, item) { return $("<li></li>").data("item.autocomplete", item).append($("<a></a>").text(item.label)).appendTo(ul) }, _move: function (direction, event) { if (!this.menu.element.is(":visible")) { this.search(null, event); return } if (this.menu.first() && /^previous/.test(direction) || this.menu.last() && /^next/.test(direction)) { this.element.val(this.term); this.menu.deactivate(); return } this.menu[direction](event) }, widget: function () { return this.menu.element }, _keyEvent: function (keyEvent, event) { if (!this.isMultiLine || this.menu.element.is(":visible")) { this._move(keyEvent, event); event.preventDefault() } } }); $.extend($.ui.autocomplete, { escapeRegex: function (value) { return value.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&") }, filter: function (array, term) { var matcher = new RegExp($.ui.autocomplete.escapeRegex(term), "i"); return $.grep(array, function (value) { return matcher.test(value.label || value.value || value) }) } }) }(jQuery)); (function ($) { $.widget("ui.menu", { _create: function () { var self = this; this.element.addClass("ui-menu ui-widget ui-widget-content ui-corner-all").attr({ role: "listbox", "aria-activedescendant": "ui-active-menuitem" }).click(function (event) { if (!$(event.target).closest(".ui-menu-item a").length) { return } event.preventDefault(); self.select(event) }); this.refresh() }, refresh: function () { var self = this; var items = this.element.children("li:not(.ui-menu-item):has(a)").addClass("ui-menu-item").attr("role", "menuitem"); items.children("a").addClass("ui-corner-all").attr("tabindex", -1).mouseenter(function (event) { self.activate(event, $(this).parent()) }).mouseleave(function () { self.deactivate() }) }, activate: function (event, item) { this.deactivate(); if (this.hasScroll()) { var offset = item.offset().top - this.element.offset().top, scroll = this.element.scrollTop(), elementHeight = this.element.height(); if (offset < 0) { this.element.scrollTop(scroll + offset) } else if (offset >= elementHeight) { this.element.scrollTop(scroll + offset - elementHeight + item.height()) } } this.active = item.eq(0).children("a").addClass("ui-state-hover").attr("id", "ui-active-menuitem").end(); this._trigger("focus", event, { item: item }) }, deactivate: function () { if (!this.active) { return } this.active.children("a").removeClass("ui-state-hover").removeAttr("id"); this._trigger("blur"); this.active = null }, next: function (event) { this.move("next", ".ui-menu-item:first", event) }, previous: function (event) { this.move("prev", ".ui-menu-item:last", event) }, first: function () { return this.active && !this.active.prevAll(".ui-menu-item").length }, last: function () { return this.active && !this.active.nextAll(".ui-menu-item").length }, move: function (direction, edge, event) { if (!this.active) { this.activate(event, this.element.children(edge)); return } var next = this.active[direction + "All"](".ui-menu-item").eq(0); if (next.length) { this.activate(event, next) } else { this.activate(event, this.element.children(edge)) } }, nextPage: function (event) { if (this.hasScroll()) { if (!this.active || this.last()) { this.activate(event, this.element.children(".ui-menu-item:first")); return } var base = this.active.offset().top, height = this.element.height(), result = this.element.children(".ui-menu-item").filter(function () { var close = $(this).offset().top - base - height + $(this).height(); return close < 10 && close > -10 }); if (!result.length) { result = this.element.children(".ui-menu-item:last") } this.activate(event, result) } else { this.activate(event, this.element.children(".ui-menu-item").filter(!this.active || this.last() ? ":first" : ":last")) } }, previousPage: function (event) { if (this.hasScroll()) { if (!this.active || this.first()) { this.activate(event, this.element.children(".ui-menu-item:last")); return } var base = this.active.offset().top, height = this.element.height(), result = this.element.children(".ui-menu-item").filter(function () { var close = $(this).offset().top - base + height - $(this).height(); return close < 10 && close > -10 }); if (!result.length) { result = this.element.children(".ui-menu-item:first") } this.activate(event, result) } else { this.activate(event, this.element.children(".ui-menu-item").filter(!this.active || this.first() ? ":last" : ":first")) } }, hasScroll: function () { return this.element.height() < this.element[$.fn.prop ? "prop" : "attr"]("scrollHeight") }, select: function (event) { this._trigger("selected", event, { item: this.active }) } }) }(jQuery)); (function ($, undefined) { var lastActive, startXPos, startYPos, clickDragged, baseClasses = "ui-button ui-widget ui-state-default ui-corner-all", stateClasses = "ui-state-hover ui-state-active ", typeClasses = "ui-button-icons-only ui-button-icon-only ui-button-text-icons ui-button-text-icon-primary ui-button-text-icon-secondary ui-button-text-only", formResetHandler = function () { var buttons = $(this).find(":ui-button"); setTimeout(function () { buttons.button("refresh") }, 1) }, radioGroup = function (radio) { var name = radio.name, form = radio.form, radios = $([]); if (name) { if (form) { radios = $(form).find("[name='" + name + "']") } else { radios = $("[name='" + name + "']", radio.ownerDocument).filter(function () { return !this.form }) } } return radios }; $.widget("ui.button", { options: { disabled: null, text: true, label: null, icons: { primary: null, secondary: null } }, _create: function () { this.element.closest("form").unbind("reset.button").bind("reset.button", formResetHandler); if (typeof this.options.disabled !== "boolean") { this.options.disabled = !!this.element.propAttr("disabled") } else { this.element.propAttr("disabled", this.options.disabled) } this._determineButtonType(); this.hasTitle = !!this.buttonElement.attr("title"); var self = this, options = this.options, toggleButton = this.type === "checkbox" || this.type === "radio", hoverClass = "ui-state-hover" + (!toggleButton ? " ui-state-active" : ""), focusClass = "ui-state-focus"; if (options.label === null) { options.label = this.buttonElement.html() } this.buttonElement.addClass(baseClasses).attr("role", "button").bind("mouseenter.button", function () { if (options.disabled) { return } $(this).addClass("ui-state-hover"); if (this === lastActive) { $(this).addClass("ui-state-active") } }).bind("mouseleave.button", function () { if (options.disabled) { return } $(this).removeClass(hoverClass) }).bind("click.button", function (event) { if (options.disabled) { event.preventDefault(); event.stopImmediatePropagation() } }); this.element.bind("focus.button", function () { self.buttonElement.addClass(focusClass) }).bind("blur.button", function () { self.buttonElement.removeClass(focusClass) }); if (toggleButton) { this.element.bind("change.button", function () { if (clickDragged) { return } self.refresh() }); this.buttonElement.bind("mousedown.button", function (event) { if (options.disabled) { return } clickDragged = false; startXPos = event.pageX; startYPos = event.pageY }).bind("mouseup.button", function (event) { if (options.disabled) { return } if (startXPos !== event.pageX || startYPos !== event.pageY) { clickDragged = true } }) } if (this.type === "checkbox") { this.buttonElement.bind("click.button", function () { if (options.disabled || clickDragged) { return false } $(this).toggleClass("ui-state-active"); self.buttonElement.attr("aria-pressed", self.element[0].checked) }) } else if (this.type === "radio") { this.buttonElement.bind("click.button", function () { if (options.disabled || clickDragged) { return false } $(this).addClass("ui-state-active"); self.buttonElement.attr("aria-pressed", "true"); var radio = self.element[0]; radioGroup(radio).not(radio).map(function () { return $(this).button("widget")[0] }).removeClass("ui-state-active").attr("aria-pressed", "false") }) } else { this.buttonElement.bind("mousedown.button", function () { if (options.disabled) { return false } $(this).addClass("ui-state-active"); lastActive = this; $(document).one("mouseup", function () { lastActive = null }) }).bind("mouseup.button", function () { if (options.disabled) { return false } $(this).removeClass("ui-state-active") }).bind("keydown.button", function (event) { if (options.disabled) { return false } if (event.keyCode == $.ui.keyCode.SPACE || event.keyCode == $.ui.keyCode.ENTER) { $(this).addClass("ui-state-active") } }).bind("keyup.button", function () { $(this).removeClass("ui-state-active") }); if (this.buttonElement.is("a")) { this.buttonElement.keyup(function (event) { if (event.keyCode === $.ui.keyCode.SPACE) { $(this).click() } }) } } this._setOption("disabled", options.disabled); this._resetButton() }, _determineButtonType: function () { if (this.element.is(":checkbox")) { this.type = "checkbox" } else if (this.element.is(":radio")) { this.type = "radio" } else if (this.element.is("input")) { this.type = "input" } else { this.type = "button" } if (this.type === "checkbox" || this.type === "radio") { var ancestor = this.element.parents().filter(":last"), labelSelector = "label[for='" + this.element.attr("id") + "']"; this.buttonElement = ancestor.find(labelSelector); if (!this.buttonElement.length) { ancestor = ancestor.length ? ancestor.siblings() : this.element.siblings(); this.buttonElement = ancestor.filter(labelSelector); if (!this.buttonElement.length) { this.buttonElement = ancestor.find(labelSelector) } } this.element.addClass("ui-helper-hidden-accessible"); var checked = this.element.is(":checked"); if (checked) { this.buttonElement.addClass("ui-state-active") } this.buttonElement.attr("aria-pressed", checked) } else { this.buttonElement = this.element } }, widget: function () { return this.buttonElement }, destroy: function () { this.element.removeClass("ui-helper-hidden-accessible"); this.buttonElement.removeClass(baseClasses + " " + stateClasses + " " + typeClasses).removeAttr("role").removeAttr("aria-pressed").html(this.buttonElement.find(".ui-button-text").html()); if (!this.hasTitle) { this.buttonElement.removeAttr("title") } $.Widget.prototype.destroy.call(this) }, _setOption: function (key, value) { $.Widget.prototype._setOption.apply(this, arguments); if (key === "disabled") { if (value) { this.element.propAttr("disabled", true) } else { this.element.propAttr("disabled", false) } return } this._resetButton() }, refresh: function () { var isDisabled = this.element.is(":disabled"); if (isDisabled !== this.options.disabled) { this._setOption("disabled", isDisabled) } if (this.type === "radio") { radioGroup(this.element[0]).each(function () { if ($(this).is(":checked")) { $(this).button("widget").addClass("ui-state-active").attr("aria-pressed", "true") } else { $(this).button("widget").removeClass("ui-state-active").attr("aria-pressed", "false") } }) } else if (this.type === "checkbox") { if (this.element.is(":checked")) { this.buttonElement.addClass("ui-state-active").attr("aria-pressed", "true") } else { this.buttonElement.removeClass("ui-state-active").attr("aria-pressed", "false") } } }, _resetButton: function () { if (this.type === "input") { if (this.options.label) { this.element.val(this.options.label) } return } var buttonElement = this.buttonElement.removeClass(typeClasses), buttonText = $("<span></span>", this.element[0].ownerDocument).addClass("ui-button-text").html(this.options.label).appendTo(buttonElement.empty()).text(), icons = this.options.icons, multipleIcons = icons.primary && icons.secondary, buttonClasses = []; if (icons.primary || icons.secondary) { if (this.options.text) { buttonClasses.push("ui-button-text-icon" + (multipleIcons ? "s" : (icons.primary ? "-primary" : "-secondary"))) } if (icons.primary) { buttonElement.prepend("<span class='ui-button-icon-primary ui-icon " + icons.primary + "'></span>") } if (icons.secondary) { buttonElement.append("<span class='ui-button-icon-secondary ui-icon " + icons.secondary + "'></span>") } if (!this.options.text) { buttonClasses.push(multipleIcons ? "ui-button-icons-only" : "ui-button-icon-only"); if (!this.hasTitle) { buttonElement.attr("title", buttonText) } } } else { buttonClasses.push("ui-button-text-only") } buttonElement.addClass(buttonClasses.join(" ")) } }); $.widget("ui.buttonset", { options: { items: ":button, :submit, :reset, :checkbox, :radio, a, :data(button)" }, _create: function () { this.element.addClass("ui-buttonset") }, _init: function () { this.refresh() }, _setOption: function (key, value) { if (key === "disabled") { this.buttons.button("option", key, value) } $.Widget.prototype._setOption.apply(this, arguments) }, refresh: function () { var rtl = this.element.css("direction") === "rtl"; this.buttons = this.element.find(this.options.items).filter(":ui-button").button("refresh").end().not(":ui-button").button().end().map(function () { return $(this).button("widget")[0] }).removeClass("ui-corner-all ui-corner-left ui-corner-right").filter(":first").addClass(rtl ? "ui-corner-right" : "ui-corner-left").end().filter(":last").addClass(rtl ? "ui-corner-left" : "ui-corner-right").end().end() }, destroy: function () { this.element.removeClass("ui-buttonset"); this.buttons.map(function () { return $(this).button("widget")[0] }).removeClass("ui-corner-left ui-corner-right").end().button("destroy"); $.Widget.prototype.destroy.call(this) } }) }(jQuery)); (function ($, undefined) { $.extend($.ui, { datepicker: { version: "1.8.22" } }); var PROP_NAME = 'datepicker'; var dpuuid = new Date().getTime(); var instActive; function Datepicker() { this.debug = false; this._curInst = null; this._keyEvent = false; this._disabledInputs = []; this._datepickerShowing = false; this._inDialog = false; this._mainDivId = 'ui-datepicker-div'; this._inlineClass = 'ui-datepicker-inline'; this._appendClass = 'ui-datepicker-append'; this._triggerClass = 'ui-datepicker-trigger'; this._dialogClass = 'ui-datepicker-dialog'; this._disableClass = 'ui-datepicker-disabled'; this._unselectableClass = 'ui-datepicker-unselectable'; this._currentClass = 'ui-datepicker-current-day'; this._dayOverClass = 'ui-datepicker-days-cell-over'; this.regional = []; this.regional[''] = { closeText: 'Done', prevText: 'Prev', nextText: 'Next', currentText: 'Today', monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'], monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'], dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'], dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'], dayNamesMin: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'], weekHeader: 'Wk', dateFormat: 'mm/dd/yy', firstDay: 0, isRTL: false, showMonthAfterYear: false, yearSuffix: '' }; this._defaults = { showOn: 'focus', showAnim: 'fadeIn', showOptions: {}, defaultDate: null, appendText: '', buttonText: '...', buttonImage: '', buttonImageOnly: false, hideIfNoPrevNext: false, navigationAsDateFormat: false, gotoCurrent: false, changeMonth: false, changeYear: false, yearRange: 'c-10:c+10', showOtherMonths: false, selectOtherMonths: false, showWeek: false, calculateWeek: this.iso8601Week, shortYearCutoff: '+10', minDate: null, maxDate: null, duration: 'fast', beforeShowDay: null, beforeShow: null, onSelect: null, onChangeMonthYear: null, onClose: null, numberOfMonths: 1, showCurrentAtPos: 0, stepMonths: 1, stepBigMonths: 12, altField: '', altFormat: '', constrainInput: true, showButtonPanel: false, autoSize: false, disabled: false }; $.extend(this._defaults, this.regional['']); this.dpDiv = bindHover($('<div id="' + this._mainDivId + '" class="ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all"></div>')) } $.extend(Datepicker.prototype, { markerClassName: 'hasDatepicker', maxRows: 4, log: function () { if (this.debug) console.log.apply('', arguments) }, _widgetDatepicker: function () { return this.dpDiv }, setDefaults: function (settings) { extendRemove(this._defaults, settings || {}); return this }, _attachDatepicker: function (target, settings) { var inlineSettings = null; for (var attrName in this._defaults) { var attrValue = target.getAttribute('date:' + attrName); if (attrValue) { inlineSettings = inlineSettings || {}; try { inlineSettings[attrName] = eval(attrValue) } catch (err) { inlineSettings[attrName] = attrValue } } } var nodeName = target.nodeName.toLowerCase(); var inline = (nodeName == 'div' || nodeName == 'span'); if (!target.id) { this.uuid += 1; target.id = 'dp' + this.uuid } var inst = this._newInst($(target), inline); inst.settings = $.extend({}, settings || {}, inlineSettings || {}); if (nodeName == 'input') { this._connectDatepicker(target, inst) } else if (inline) { this._inlineDatepicker(target, inst) } }, _newInst: function (target, inline) { var id = target[0].id.replace(/([^A-Za-z0-9_-])/g, '\\\\$1'); return { id: id, input: target, selectedDay: 0, selectedMonth: 0, selectedYear: 0, drawMonth: 0, drawYear: 0, inline: inline, dpDiv: (!inline ? this.dpDiv : bindHover($('<div class="' + this._inlineClass + ' ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all"></div>'))) } }, _connectDatepicker: function (target, inst) { var input = $(target); inst.append = $([]); inst.trigger = $([]); if (input.hasClass(this.markerClassName)) return; this._attachments(input, inst); input.addClass(this.markerClassName).keydown(this._doKeyDown).keypress(this._doKeyPress).keyup(this._doKeyUp).bind("setData.datepicker", function (event, key, value) { inst.settings[key] = value }).bind("getData.datepicker", function (event, key) { return this._get(inst, key) }); this._autoSize(inst); $.data(target, PROP_NAME, inst); if (inst.settings.disabled) { this._disableDatepicker(target) } }, _attachments: function (input, inst) { var appendText = this._get(inst, 'appendText'); var isRTL = this._get(inst, 'isRTL'); if (inst.append) inst.append.remove(); if (appendText) { inst.append = $('<span class="' + this._appendClass + '">' + appendText + '</span>'); input[isRTL ? 'before' : 'after'](inst.append) } input.unbind('focus', this._showDatepicker); if (inst.trigger) inst.trigger.remove(); var showOn = this._get(inst, 'showOn'); if (showOn == 'focus' || showOn == 'both') input.focus(this._showDatepicker); if (showOn == 'button' || showOn == 'both') { var buttonText = this._get(inst, 'buttonText'); var buttonImage = this._get(inst, 'buttonImage'); inst.trigger = $(this._get(inst, 'buttonImageOnly') ? $('<img/>').addClass(this._triggerClass).attr({ src: buttonImage, alt: buttonText, title: buttonText }) : $('<button type="button"></button>').addClass(this._triggerClass).html(buttonImage == '' ? buttonText : $('<img/>').attr({ src: buttonImage, alt: buttonText, title: buttonText }))); input[isRTL ? 'before' : 'after'](inst.trigger); inst.trigger.click(function () { if ($.datepicker._datepickerShowing && $.datepicker._lastInput == input[0]) $.datepicker._hideDatepicker(); else if ($.datepicker._datepickerShowing && $.datepicker._lastInput != input[0]) { $.datepicker._hideDatepicker(); $.datepicker._showDatepicker(input[0]) } else $.datepicker._showDatepicker(input[0]); return false }) } }, _autoSize: function (inst) { if (this._get(inst, 'autoSize') && !inst.inline) { var date = new Date(2009, 12 - 1, 20); var dateFormat = this._get(inst, 'dateFormat'); if (dateFormat.match(/[DM]/)) { var findMax = function (names) { var max = 0; var maxI = 0; for (var i = 0; i < names.length; i++) { if (names[i].length > max) { max = names[i].length; maxI = i } } return maxI }; date.setMonth(findMax(this._get(inst, (dateFormat.match(/MM/) ? 'monthNames' : 'monthNamesShort')))); date.setDate(findMax(this._get(inst, (dateFormat.match(/DD/) ? 'dayNames' : 'dayNamesShort'))) + 20 - date.getDay()) } inst.input.attr('size', this._formatDate(inst, date).length) } }, _inlineDatepicker: function (target, inst) { var divSpan = $(target); if (divSpan.hasClass(this.markerClassName)) return; divSpan.addClass(this.markerClassName).append(inst.dpDiv).bind("setData.datepicker", function (event, key, value) { inst.settings[key] = value }).bind("getData.datepicker", function (event, key) { return this._get(inst, key) }); $.data(target, PROP_NAME, inst); this._setDate(inst, this._getDefaultDate(inst), true); this._updateDatepicker(inst); this._updateAlternate(inst); if (inst.settings.disabled) { this._disableDatepicker(target) } inst.dpDiv.css("display", "block") }, _dialogDatepicker: function (input, date, onSelect, settings, pos) { var inst = this._dialogInst; if (!inst) { this.uuid += 1; var id = 'dp' + this.uuid; this._dialogInput = $('<input type="text" id="' + id + '" style="position: absolute; top: -100px; width: 0px;"/>'); this._dialogInput.keydown(this._doKeyDown); $('body').append(this._dialogInput); inst = this._dialogInst = this._newInst(this._dialogInput, false); inst.settings = {}; $.data(this._dialogInput[0], PROP_NAME, inst) } extendRemove(inst.settings, settings || {}); date = (date && date.constructor == Date ? this._formatDate(inst, date) : date); this._dialogInput.val(date); this._pos = (pos ? (pos.length ? pos : [pos.pageX, pos.pageY]) : null); if (!this._pos) { var browserWidth = document.documentElement.clientWidth; var browserHeight = document.documentElement.clientHeight; var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft; var scrollY = document.documentElement.scrollTop || document.body.scrollTop; this._pos = [(browserWidth / 2) - 100 + scrollX, (browserHeight / 2) - 150 + scrollY] } this._dialogInput.css('left', (this._pos[0] + 20) + 'px').css('top', this._pos[1] + 'px'); inst.settings.onSelect = onSelect; this._inDialog = true; this.dpDiv.addClass(this._dialogClass); this._showDatepicker(this._dialogInput[0]); if ($.blockUI) $.blockUI(this.dpDiv); $.data(this._dialogInput[0], PROP_NAME, inst); return this }, _destroyDatepicker: function (target) { var $target = $(target); var inst = $.data(target, PROP_NAME); if (!$target.hasClass(this.markerClassName)) { return } var nodeName = target.nodeName.toLowerCase(); $.removeData(target, PROP_NAME); if (nodeName == 'input') { inst.append.remove(); inst.trigger.remove(); $target.removeClass(this.markerClassName).unbind('focus', this._showDatepicker).unbind('keydown', this._doKeyDown).unbind('keypress', this._doKeyPress).unbind('keyup', this._doKeyUp) } else if (nodeName == 'div' || nodeName == 'span') $target.removeClass(this.markerClassName).empty() }, _enableDatepicker: function (target) { var $target = $(target); var inst = $.data(target, PROP_NAME); if (!$target.hasClass(this.markerClassName)) { return } var nodeName = target.nodeName.toLowerCase(); if (nodeName == 'input') { target.disabled = false; inst.trigger.filter('button').each(function () { this.disabled = false }).end().filter('img').css({ opacity: '1.0', cursor: '' }) } else if (nodeName == 'div' || nodeName == 'span') { var inline = $target.children('.' + this._inlineClass); inline.children().removeClass('ui-state-disabled'); inline.find("select.ui-datepicker-month, select.ui-datepicker-year").removeAttr("disabled") } this._disabledInputs = $.map(this._disabledInputs, function (value) { return (value == target ? null : value) }) }, _disableDatepicker: function (target) { var $target = $(target); var inst = $.data(target, PROP_NAME); if (!$target.hasClass(this.markerClassName)) { return } var nodeName = target.nodeName.toLowerCase(); if (nodeName == 'input') { target.disabled = true; inst.trigger.filter('button').each(function () { this.disabled = true }).end().filter('img').css({ opacity: '0.5', cursor: 'default' }) } else if (nodeName == 'div' || nodeName == 'span') { var inline = $target.children('.' + this._inlineClass); inline.children().addClass('ui-state-disabled'); inline.find("select.ui-datepicker-month, select.ui-datepicker-year").attr("disabled", "disabled") } this._disabledInputs = $.map(this._disabledInputs, function (value) { return (value == target ? null : value) }); this._disabledInputs[this._disabledInputs.length] = target }, _isDisabledDatepicker: function (target) { if (!target) { return false } for (var i = 0; i < this._disabledInputs.length; i++) { if (this._disabledInputs[i] == target) return true } return false }, _getInst: function (target) { try { return $.data(target, PROP_NAME) } catch (err) { throw 'Missing instance data for this datepicker' } }, _optionDatepicker: function (target, name, value) { var inst = this._getInst(target); if (arguments.length == 2 && typeof name == 'string') { return (name == 'defaults' ? $.extend({}, $.datepicker._defaults) : (inst ? (name == 'all' ? $.extend({}, inst.settings) : this._get(inst, name)) : null)) } var settings = name || {}; if (typeof name == 'string') { settings = {}; settings[name] = value } if (inst) { if (this._curInst == inst) { this._hideDatepicker() } var date = this._getDateDatepicker(target, true); var minDate = this._getMinMaxDate(inst, 'min'); var maxDate = this._getMinMaxDate(inst, 'max'); extendRemove(inst.settings, settings); if (minDate !== null && settings['dateFormat'] !== undefined && settings['minDate'] === undefined) inst.settings.minDate = this._formatDate(inst, minDate); if (maxDate !== null && settings['dateFormat'] !== undefined && settings['maxDate'] === undefined) inst.settings.maxDate = this._formatDate(inst, maxDate); this._attachments($(target), inst); this._autoSize(inst); this._setDate(inst, date); this._updateAlternate(inst); this._updateDatepicker(inst) } }, _changeDatepicker: function (target, name, value) { this._optionDatepicker(target, name, value) }, _refreshDatepicker: function (target) { var inst = this._getInst(target); if (inst) { this._updateDatepicker(inst) } }, _setDateDatepicker: function (target, date) { var inst = this._getInst(target); if (inst) { this._setDate(inst, date); this._updateDatepicker(inst); this._updateAlternate(inst) } }, _getDateDatepicker: function (target, noDefault) { var inst = this._getInst(target); if (inst && !inst.inline) this._setDateFromField(inst, noDefault); return (inst ? this._getDate(inst) : null) }, _doKeyDown: function (event) { var inst = $.datepicker._getInst(event.target); var handled = true; var isRTL = inst.dpDiv.is('.ui-datepicker-rtl'); inst._keyEvent = true; if ($.datepicker._datepickerShowing) switch (event.keyCode) { case 9: $.datepicker._hideDatepicker(); handled = false; break; case 13: var sel = $('td.' + $.datepicker._dayOverClass + ':not(.' + $.datepicker._currentClass + ')', inst.dpDiv); if (sel[0]) $.datepicker._selectDay(event.target, inst.selectedMonth, inst.selectedYear, sel[0]); var onSelect = $.datepicker._get(inst, 'onSelect'); if (onSelect) { var dateStr = $.datepicker._formatDate(inst); onSelect.apply((inst.input ? inst.input[0] : null), [dateStr, inst]) } else $.datepicker._hideDatepicker(); return false; break; case 27: $.datepicker._hideDatepicker(); break; case 33: $.datepicker._adjustDate(event.target, (event.ctrlKey ? -$.datepicker._get(inst, 'stepBigMonths') : -$.datepicker._get(inst, 'stepMonths')), 'M'); break; case 34: $.datepicker._adjustDate(event.target, (event.ctrlKey ? +$.datepicker._get(inst, 'stepBigMonths') : +$.datepicker._get(inst, 'stepMonths')), 'M'); break; case 35: if (event.ctrlKey || event.metaKey) $.datepicker._clearDate(event.target); handled = event.ctrlKey || event.metaKey; break; case 36: if (event.ctrlKey || event.metaKey) $.datepicker._gotoToday(event.target); handled = event.ctrlKey || event.metaKey; break; case 37: if (event.ctrlKey || event.metaKey) $.datepicker._adjustDate(event.target, (isRTL ? +1 : -1), 'D'); handled = event.ctrlKey || event.metaKey; if (event.originalEvent.altKey) $.datepicker._adjustDate(event.target, (event.ctrlKey ? -$.datepicker._get(inst, 'stepBigMonths') : -$.datepicker._get(inst, 'stepMonths')), 'M'); break; case 38: if (event.ctrlKey || event.metaKey) $.datepicker._adjustDate(event.target, -7, 'D'); handled = event.ctrlKey || event.metaKey; break; case 39: if (event.ctrlKey || event.metaKey) $.datepicker._adjustDate(event.target, (isRTL ? -1 : +1), 'D'); handled = event.ctrlKey || event.metaKey; if (event.originalEvent.altKey) $.datepicker._adjustDate(event.target, (event.ctrlKey ? +$.datepicker._get(inst, 'stepBigMonths') : +$.datepicker._get(inst, 'stepMonths')), 'M'); break; case 40: if (event.ctrlKey || event.metaKey) $.datepicker._adjustDate(event.target, +7, 'D'); handled = event.ctrlKey || event.metaKey; break; default: handled = false } else if (event.keyCode == 36 && event.ctrlKey) $.datepicker._showDatepicker(this); else { handled = false } if (handled) { event.preventDefault(); event.stopPropagation() } }, _doKeyPress: function (event) { var inst = $.datepicker._getInst(event.target); if ($.datepicker._get(inst, 'constrainInput')) { var chars = $.datepicker._possibleChars($.datepicker._get(inst, 'dateFormat')); var chr = String.fromCharCode(event.charCode == undefined ? event.keyCode : event.charCode); return event.ctrlKey || event.metaKey || (chr < ' ' || !chars || chars.indexOf(chr) > -1) } }, _doKeyUp: function (event) { var inst = $.datepicker._getInst(event.target); if (inst.input.val() != inst.lastVal) { try { var date = $.datepicker.parseDate($.datepicker._get(inst, 'dateFormat'), (inst.input ? inst.input.val() : null), $.datepicker._getFormatConfig(inst)); if (date) { $.datepicker._setDateFromField(inst); $.datepicker._updateAlternate(inst); $.datepicker._updateDatepicker(inst) } } catch (err) { $.datepicker.log(err) } } return true }, _showDatepicker: function (input) { input = input.target || input; if (input.nodeName.toLowerCase() != 'input') input = $('input', input.parentNode)[0]; if ($.datepicker._isDisabledDatepicker(input) || $.datepicker._lastInput == input) return; var inst = $.datepicker._getInst(input); if ($.datepicker._curInst && $.datepicker._curInst != inst) { $.datepicker._curInst.dpDiv.stop(true, true); if (inst && $.datepicker._datepickerShowing) { $.datepicker._hideDatepicker($.datepicker._curInst.input[0]) } } var beforeShow = $.datepicker._get(inst, 'beforeShow'); var beforeShowSettings = beforeShow ? beforeShow.apply(input, [input, inst]) : {}; if (beforeShowSettings === false) { return } extendRemove(inst.settings, beforeShowSettings); inst.lastVal = null; $.datepicker._lastInput = input; $.datepicker._setDateFromField(inst); if ($.datepicker._inDialog) input.value = ''; if (!$.datepicker._pos) { $.datepicker._pos = $.datepicker._findPos(input); $.datepicker._pos[1] += input.offsetHeight } var isFixed = false; $(input).parents().each(function () { isFixed |= $(this).css('position') == 'fixed'; return !isFixed }); if (isFixed && $.browser.opera) { $.datepicker._pos[0] -= document.documentElement.scrollLeft; $.datepicker._pos[1] -= document.documentElement.scrollTop } var offset = { left: $.datepicker._pos[0], top: $.datepicker._pos[1] }; $.datepicker._pos = null; inst.dpDiv.empty(); inst.dpDiv.css({ position: 'absolute', display: 'block', top: '-1000px' }); $.datepicker._updateDatepicker(inst); offset = $.datepicker._checkOffset(inst, offset, isFixed); inst.dpDiv.css({ position: ($.datepicker._inDialog && $.blockUI ? 'static' : (isFixed ? 'fixed' : 'absolute')), display: 'none', left: offset.left + 'px', top: offset.top + 'px' }); if (!inst.inline) { var showAnim = $.datepicker._get(inst, 'showAnim'); var duration = $.datepicker._get(inst, 'duration'); var postProcess = function () { var cover = inst.dpDiv.find('iframe.ui-datepicker-cover'); if (!!cover.length) { var borders = $.datepicker._getBorders(inst.dpDiv); cover.css({ left: -borders[0], top: -borders[1], width: inst.dpDiv.outerWidth(), height: inst.dpDiv.outerHeight() }) } }; inst.dpDiv.zIndex($(input).zIndex() + 1); $.datepicker._datepickerShowing = true; if ($.effects && $.effects[showAnim]) inst.dpDiv.show(showAnim, $.datepicker._get(inst, 'showOptions'), duration, postProcess); else inst.dpDiv[showAnim || 'show']((showAnim ? duration : null), postProcess); if (!showAnim || !duration) postProcess(); if (inst.input.is(':visible') && !inst.input.is(':disabled')) inst.input.focus(); $.datepicker._curInst = inst } }, _updateDatepicker: function (inst) { var self = this; self.maxRows = 4; var borders = $.datepicker._getBorders(inst.dpDiv); instActive = inst; inst.dpDiv.empty().append(this._generateHTML(inst)); this._attachHandlers(inst); var cover = inst.dpDiv.find('iframe.ui-datepicker-cover'); if (!!cover.length) { cover.css({ left: -borders[0], top: -borders[1], width: inst.dpDiv.outerWidth(), height: inst.dpDiv.outerHeight() }) } inst.dpDiv.find('.' + this._dayOverClass + ' a').mouseover(); var numMonths = this._getNumberOfMonths(inst); var cols = numMonths[1]; var width = 17; inst.dpDiv.removeClass('ui-datepicker-multi-2 ui-datepicker-multi-3 ui-datepicker-multi-4').width(''); if (cols > 1) inst.dpDiv.addClass('ui-datepicker-multi-' + cols).css('width', (width * cols) + 'em'); inst.dpDiv[(numMonths[0] != 1 || numMonths[1] != 1 ? 'add' : 'remove') + 'Class']('ui-datepicker-multi'); inst.dpDiv[(this._get(inst, 'isRTL') ? 'add' : 'remove') + 'Class']('ui-datepicker-rtl'); if (inst == $.datepicker._curInst && $.datepicker._datepickerShowing && inst.input && inst.input.is(':visible') && !inst.input.is(':disabled') && inst.input[0] != document.activeElement) inst.input.focus(); if (inst.yearshtml) { var origyearshtml = inst.yearshtml; setTimeout(function () { if (origyearshtml === inst.yearshtml && inst.yearshtml) { inst.dpDiv.find('select.ui-datepicker-year:first').replaceWith(inst.yearshtml) } origyearshtml = inst.yearshtml = null }, 0) } }, _getBorders: function (elem) { var convert = function (value) { return { thin: 1, medium: 2, thick: 3 }[value] || value }; return [parseFloat(convert(elem.css('border-left-width'))), parseFloat(convert(elem.css('border-top-width')))] }, _checkOffset: function (inst, offset, isFixed) { var dpWidth = inst.dpDiv.outerWidth(); var dpHeight = inst.dpDiv.outerHeight(); var inputWidth = inst.input ? inst.input.outerWidth() : 0; var inputHeight = inst.input ? inst.input.outerHeight() : 0; var viewWidth = document.documentElement.clientWidth + (isFixed ? 0 : $(document).scrollLeft()); var viewHeight = document.documentElement.clientHeight + (isFixed ? 0 : $(document).scrollTop()); offset.left -= (this._get(inst, 'isRTL') ? (dpWidth - inputWidth) : 0); offset.left -= (isFixed && offset.left == inst.input.offset().left) ? $(document).scrollLeft() : 0; offset.top -= (isFixed && offset.top == (inst.input.offset().top + inputHeight)) ? $(document).scrollTop() : 0; offset.left -= Math.min(offset.left, (offset.left + dpWidth > viewWidth && viewWidth > dpWidth) ? Math.abs(offset.left + dpWidth - viewWidth) : 0); offset.top -= Math.min(offset.top, (offset.top + dpHeight > viewHeight && viewHeight > dpHeight) ? Math.abs(dpHeight + inputHeight) : 0); return offset }, _findPos: function (obj) { var inst = this._getInst(obj); var isRTL = this._get(inst, 'isRTL'); while (obj && (obj.type == 'hidden' || obj.nodeType != 1 || $.expr.filters.hidden(obj))) { obj = obj[isRTL ? 'previousSibling' : 'nextSibling'] } var position = $(obj).offset(); return [position.left, position.top] }, _hideDatepicker: function (input) { var inst = this._curInst; if (!inst || (input && inst != $.data(input, PROP_NAME))) return; if (this._datepickerShowing) { var showAnim = this._get(inst, 'showAnim'); var duration = this._get(inst, 'duration'); var postProcess = function () { $.datepicker._tidyDialog(inst) }; if ($.effects && $.effects[showAnim]) inst.dpDiv.hide(showAnim, $.datepicker._get(inst, 'showOptions'), duration, postProcess); else inst.dpDiv[(showAnim == 'slideDown' ? 'slideUp' : (showAnim == 'fadeIn' ? 'fadeOut' : 'hide'))]((showAnim ? duration : null), postProcess); if (!showAnim) postProcess(); this._datepickerShowing = false; var onClose = this._get(inst, 'onClose'); if (onClose) onClose.apply((inst.input ? inst.input[0] : null), [(inst.input ? inst.input.val() : ''), inst]); this._lastInput = null; if (this._inDialog) { this._dialogInput.css({ position: 'absolute', left: '0', top: '-100px' }); if ($.blockUI) { $.unblockUI(); $('body').append(this.dpDiv) } } this._inDialog = false } }, _tidyDialog: function (inst) { inst.dpDiv.removeClass(this._dialogClass).unbind('.ui-datepicker-calendar') }, _checkExternalClick: function (event) { if (!$.datepicker._curInst) return; var $target = $(event.target), inst = $.datepicker._getInst($target[0]); if ((($target[0].id != $.datepicker._mainDivId && $target.parents('#' + $.datepicker._mainDivId).length == 0 && !$target.hasClass($.datepicker.markerClassName) && !$target.closest("." + $.datepicker._triggerClass).length && $.datepicker._datepickerShowing && !($.datepicker._inDialog && $.blockUI))) || ($target.hasClass($.datepicker.markerClassName) && $.datepicker._curInst != inst)) $.datepicker._hideDatepicker() }, _adjustDate: function (id, offset, period) { var target = $(id); var inst = this._getInst(target[0]); if (this._isDisabledDatepicker(target[0])) { return } this._adjustInstDate(inst, offset + (period == 'M' ? this._get(inst, 'showCurrentAtPos') : 0), period); this._updateDatepicker(inst) }, _gotoToday: function (id) { var target = $(id); var inst = this._getInst(target[0]); if (this._get(inst, 'gotoCurrent') && inst.currentDay) { inst.selectedDay = inst.currentDay; inst.drawMonth = inst.selectedMonth = inst.currentMonth; inst.drawYear = inst.selectedYear = inst.currentYear } else { var date = new Date(); inst.selectedDay = date.getDate(); inst.drawMonth = inst.selectedMonth = date.getMonth(); inst.drawYear = inst.selectedYear = date.getFullYear() } this._notifyChange(inst); this._adjustDate(target) }, _selectMonthYear: function (id, select, period) { var target = $(id); var inst = this._getInst(target[0]); inst['selected' + (period == 'M' ? 'Month' : 'Year')] = inst['draw' + (period == 'M' ? 'Month' : 'Year')] = parseInt(select.options[select.selectedIndex].value, 10); this._notifyChange(inst); this._adjustDate(target) }, _selectDay: function (id, month, year, td) { var target = $(id); if ($(td).hasClass(this._unselectableClass) || this._isDisabledDatepicker(target[0])) { return } var inst = this._getInst(target[0]); inst.selectedDay = inst.currentDay = $('a', td).html(); inst.selectedMonth = inst.currentMonth = month; inst.selectedYear = inst.currentYear = year; this._selectDate(id, this._formatDate(inst, inst.currentDay, inst.currentMonth, inst.currentYear)) }, _clearDate: function (id) { var target = $(id); var inst = this._getInst(target[0]); this._selectDate(target, '') }, _selectDate: function (id, dateStr) { var target = $(id); var inst = this._getInst(target[0]); dateStr = (dateStr != null ? dateStr : this._formatDate(inst)); if (inst.input) inst.input.val(dateStr); this._updateAlternate(inst); var onSelect = this._get(inst, 'onSelect'); if (onSelect) onSelect.apply((inst.input ? inst.input[0] : null), [dateStr, inst]); else if (inst.input) inst.input.trigger('change'); if (inst.inline) this._updateDatepicker(inst); else { this._hideDatepicker(); this._lastInput = inst.input[0]; if (typeof (inst.input[0]) != 'object') inst.input.focus(); this._lastInput = null } }, _updateAlternate: function (inst) { var altField = this._get(inst, 'altField'); if (altField) { var altFormat = this._get(inst, 'altFormat') || this._get(inst, 'dateFormat'); var date = this._getDate(inst); var dateStr = this.formatDate(altFormat, date, this._getFormatConfig(inst)); $(altField).each(function () { $(this).val(dateStr) }) } }, noWeekends: function (date) { var day = date.getDay(); return [(day > 0 && day < 6), ''] }, iso8601Week: function (date) { var checkDate = new Date(date.getTime()); checkDate.setDate(checkDate.getDate() + 4 - (checkDate.getDay() || 7)); var time = checkDate.getTime(); checkDate.setMonth(0); checkDate.setDate(1); return Math.floor(Math.round((time - checkDate) / 86400000) / 7) + 1 }, parseDate: function (format, value, settings) { if (format == null || value == null) throw 'Invalid arguments'; value = (typeof value == 'object' ? value.toString() : value + ''); if (value == '') return null; var shortYearCutoff = (settings ? settings.shortYearCutoff : null) || this._defaults.shortYearCutoff; shortYearCutoff = (typeof shortYearCutoff != 'string' ? shortYearCutoff : new Date().getFullYear() % 100 + parseInt(shortYearCutoff, 10)); var dayNamesShort = (settings ? settings.dayNamesShort : null) || this._defaults.dayNamesShort; var dayNames = (settings ? settings.dayNames : null) || this._defaults.dayNames; var monthNamesShort = (settings ? settings.monthNamesShort : null) || this._defaults.monthNamesShort; var monthNames = (settings ? settings.monthNames : null) || this._defaults.monthNames; var year = -1; var month = -1; var day = -1; var doy = -1; var literal = false; var lookAhead = function (match) { var matches = (iFormat + 1 < format.length && format.charAt(iFormat + 1) == match); if (matches) iFormat++; return matches }; var getNumber = function (match) { var isDoubled = lookAhead(match); var size = (match == '@' ? 14 : (match == '!' ? 20 : (match == 'y' && isDoubled ? 4 : (match == 'o' ? 3 : 2)))); var digits = new RegExp('^\\d{1,' + size + '}'); var num = value.substring(iValue).match(digits); if (!num) throw 'Missing number at position ' + iValue; iValue += num[0].length; return parseInt(num[0], 10) }; var getName = function (match, shortNames, longNames) { var names = $.map(lookAhead(match) ? longNames : shortNames, function (v, k) { return [[k, v]] }).sort(function (a, b) { return -(a[1].length - b[1].length) }); var index = -1; $.each(names, function (i, pair) { var name = pair[1]; if (value.substr(iValue, name.length).toLowerCase() == name.toLowerCase()) { index = pair[0]; iValue += name.length; return false } }); if (index != -1) return index + 1; else throw 'Unknown name at position ' + iValue }; var checkLiteral = function () { if (value.charAt(iValue) != format.charAt(iFormat)) throw 'Unexpected literal at position ' + iValue; iValue++ }; var iValue = 0; for (var iFormat = 0; iFormat < format.length; iFormat++) { if (literal) if (format.charAt(iFormat) == "'" && !lookAhead("'")) literal = false; else checkLiteral(); else switch (format.charAt(iFormat)) { case 'd': day = getNumber('d'); break; case 'D': getName('D', dayNamesShort, dayNames); break; case 'o': doy = getNumber('o'); break; case 'm': month = getNumber('m'); break; case 'M': month = getName('M', monthNamesShort, monthNames); break; case 'y': year = getNumber('y'); break; case '@': var date = new Date(getNumber('@')); year = date.getFullYear(); month = date.getMonth() + 1; day = date.getDate(); break; case '!': var date = new Date((getNumber('!') - this._ticksTo1970) / 10000); year = date.getFullYear(); month = date.getMonth() + 1; day = date.getDate(); break; case "'": if (lookAhead("'")) checkLiteral(); else literal = true; break; default: checkLiteral() } } if (iValue < value.length) { throw "Extra/unparsed characters found in date: " + value.substring(iValue) } if (year == -1) year = new Date().getFullYear(); else if (year < 100) year += new Date().getFullYear() - new Date().getFullYear() % 100 + (year <= shortYearCutoff ? 0 : -100); if (doy > -1) { month = 1; day = doy; do { var dim = this._getDaysInMonth(year, month - 1); if (day <= dim) break; month++; day -= dim } while (true) } var date = this._daylightSavingAdjust(new Date(year, month - 1, day)); if (date.getFullYear() != year || date.getMonth() + 1 != month || date.getDate() != day) throw 'Invalid date'; return date }, ATOM: 'yy-mm-dd', COOKIE: 'D, dd M yy', ISO_8601: 'yy-mm-dd', RFC_822: 'D, d M y', RFC_850: 'DD, dd-M-y', RFC_1036: 'D, d M y', RFC_1123: 'D, d M yy', RFC_2822: 'D, d M yy', RSS: 'D, d M y', TICKS: '!', TIMESTAMP: '@', W3C: 'yy-mm-dd', _ticksTo1970: (((1970 - 1) * 365 + Math.floor(1970 / 4) - Math.floor(1970 / 100) + Math.floor(1970 / 400)) * 24 * 60 * 60 * 10000000), formatDate: function (format, date, settings) { if (!date) return ''; var dayNamesShort = (settings ? settings.dayNamesShort : null) || this._defaults.dayNamesShort; var dayNames = (settings ? settings.dayNames : null) || this._defaults.dayNames; var monthNamesShort = (settings ? settings.monthNamesShort : null) || this._defaults.monthNamesShort; var monthNames = (settings ? settings.monthNames : null) || this._defaults.monthNames; var lookAhead = function (match) { var matches = (iFormat + 1 < format.length && format.charAt(iFormat + 1) == match); if (matches) iFormat++; return matches }; var formatNumber = function (match, value, len) { var num = '' + value; if (lookAhead(match)) while (num.length < len) num = '0' + num; return num }; var formatName = function (match, value, shortNames, longNames) { return (lookAhead(match) ? longNames[value] : shortNames[value]) }; var output = ''; var literal = false; if (date) for (var iFormat = 0; iFormat < format.length; iFormat++) { if (literal) if (format.charAt(iFormat) == "'" && !lookAhead("'")) literal = false; else output += format.charAt(iFormat); else switch (format.charAt(iFormat)) { case 'd': output += formatNumber('d', date.getDate(), 2); break; case 'D': output += formatName('D', date.getDay(), dayNamesShort, dayNames); break; case 'o': output += formatNumber('o', Math.round((new Date(date.getFullYear(), date.getMonth(), date.getDate()).getTime() - new Date(date.getFullYear(), 0, 0).getTime()) / 86400000), 3); break; case 'm': output += formatNumber('m', date.getMonth() + 1, 2); break; case 'M': output += formatName('M', date.getMonth(), monthNamesShort, monthNames); break; case 'y': output += (lookAhead('y') ? date.getFullYear() : (date.getYear() % 100 < 10 ? '0' : '') + date.getYear() % 100); break; case '@': output += date.getTime(); break; case '!': output += date.getTime() * 10000 + this._ticksTo1970; break; case "'": if (lookAhead("'")) output += "'"; else literal = true; break; default: output += format.charAt(iFormat) } } return output }, _possibleChars: function (format) { var chars = ''; var literal = false; var lookAhead = function (match) { var matches = (iFormat + 1 < format.length && format.charAt(iFormat + 1) == match); if (matches) iFormat++; return matches }; for (var iFormat = 0; iFormat < format.length; iFormat++) if (literal) if (format.charAt(iFormat) == "'" && !lookAhead("'")) literal = false; else chars += format.charAt(iFormat); else switch (format.charAt(iFormat)) { case 'd': case 'm': case 'y': case '@': chars += '0123456789'; break; case 'D': case 'M': return null; case "'": if (lookAhead("'")) chars += "'"; else literal = true; break; default: chars += format.charAt(iFormat) } return chars }, _get: function (inst, name) { return inst.settings[name] !== undefined ? inst.settings[name] : this._defaults[name] }, _setDateFromField: function (inst, noDefault) { if (inst.input.val() == inst.lastVal) { return } var dateFormat = this._get(inst, 'dateFormat'); var dates = inst.lastVal = inst.input ? inst.input.val() : null; var date, defaultDate; date = defaultDate = this._getDefaultDate(inst); var settings = this._getFormatConfig(inst); try { date = this.parseDate(dateFormat, dates, settings) || defaultDate } catch (event) { this.log(event); dates = (noDefault ? '' : dates) } inst.selectedDay = date.getDate(); inst.drawMonth = inst.selectedMonth = date.getMonth(); inst.drawYear = inst.selectedYear = date.getFullYear(); inst.currentDay = (dates ? date.getDate() : 0); inst.currentMonth = (dates ? date.getMonth() : 0); inst.currentYear = (dates ? date.getFullYear() : 0); this._adjustInstDate(inst) }, _getDefaultDate: function (inst) { return this._restrictMinMax(inst, this._determineDate(inst, this._get(inst, 'defaultDate'), new Date())) }, _determineDate: function (inst, date, defaultDate) { var offsetNumeric = function (offset) { var date = new Date(); date.setDate(date.getDate() + offset); return date }; var offsetString = function (offset) { try { return $.datepicker.parseDate($.datepicker._get(inst, 'dateFormat'), offset, $.datepicker._getFormatConfig(inst)) } catch (e) { } var date = (offset.toLowerCase().match(/^c/) ? $.datepicker._getDate(inst) : null) || new Date(); var year = date.getFullYear(); var month = date.getMonth(); var day = date.getDate(); var pattern = /([+-]?[0-9]+)\s*(d|D|w|W|m|M|y|Y)?/g; var matches = pattern.exec(offset); while (matches) { switch (matches[2] || 'd') { case 'd': case 'D': day += parseInt(matches[1], 10); break; case 'w': case 'W': day += parseInt(matches[1], 10) * 7; break; case 'm': case 'M': month += parseInt(matches[1], 10); day = Math.min(day, $.datepicker._getDaysInMonth(year, month)); break; case 'y': case 'Y': year += parseInt(matches[1], 10); day = Math.min(day, $.datepicker._getDaysInMonth(year, month)); break } matches = pattern.exec(offset) } return new Date(year, month, day) }; var newDate = (date == null || date === '' ? defaultDate : (typeof date == 'string' ? offsetString(date) : (typeof date == 'number' ? (isNaN(date) ? defaultDate : offsetNumeric(date)) : new Date(date.getTime())))); newDate = (newDate && newDate.toString() == 'Invalid Date' ? defaultDate : newDate); if (newDate) { newDate.setHours(0); newDate.setMinutes(0); newDate.setSeconds(0); newDate.setMilliseconds(0) } return this._daylightSavingAdjust(newDate) }, _daylightSavingAdjust: function (date) { if (!date) return null; date.setHours(date.getHours() > 12 ? date.getHours() + 2 : 0); return date }, _setDate: function (inst, date, noChange) { var clear = !date; var origMonth = inst.selectedMonth; var origYear = inst.selectedYear; var newDate = this._restrictMinMax(inst, this._determineDate(inst, date, new Date())); inst.selectedDay = inst.currentDay = newDate.getDate(); inst.drawMonth = inst.selectedMonth = inst.currentMonth = newDate.getMonth(); inst.drawYear = inst.selectedYear = inst.currentYear = newDate.getFullYear(); if ((origMonth != inst.selectedMonth || origYear != inst.selectedYear) && !noChange) this._notifyChange(inst); this._adjustInstDate(inst); if (inst.input) { inst.input.val(clear ? '' : this._formatDate(inst)) } }, _getDate: function (inst) { var startDate = (!inst.currentYear || (inst.input && inst.input.val() == '') ? null : this._daylightSavingAdjust(new Date(inst.currentYear, inst.currentMonth, inst.currentDay))); return startDate }, _attachHandlers: function (inst) { var stepMonths = this._get(inst, 'stepMonths'); var id = '#' + inst.id; inst.dpDiv.find('[data-handler]').map(function () { var handler = { prev: function () { window['DP_jQuery_' + dpuuid].datepicker._adjustDate(id, -stepMonths, 'M') }, next: function () { window['DP_jQuery_' + dpuuid].datepicker._adjustDate(id, +stepMonths, 'M') }, hide: function () { window['DP_jQuery_' + dpuuid].datepicker._hideDatepicker() }, today: function () { window['DP_jQuery_' + dpuuid].datepicker._gotoToday(id) }, selectDay: function () { window['DP_jQuery_' + dpuuid].datepicker._selectDay(id, +this.getAttribute('data-month'), +this.getAttribute('data-year'), this); return false }, selectMonth: function () { window['DP_jQuery_' + dpuuid].datepicker._selectMonthYear(id, this, 'M'); return false }, selectYear: function () { window['DP_jQuery_' + dpuuid].datepicker._selectMonthYear(id, this, 'Y'); return false } }; $(this).bind(this.getAttribute('data-event'), handler[this.getAttribute('data-handler')]) }) }, _generateHTML: function (inst) { var today = new Date(); today = this._daylightSavingAdjust(new Date(today.getFullYear(), today.getMonth(), today.getDate())); var isRTL = this._get(inst, 'isRTL'); var showButtonPanel = this._get(inst, 'showButtonPanel'); var hideIfNoPrevNext = this._get(inst, 'hideIfNoPrevNext'); var navigationAsDateFormat = this._get(inst, 'navigationAsDateFormat'); var numMonths = this._getNumberOfMonths(inst); var showCurrentAtPos = this._get(inst, 'showCurrentAtPos'); var stepMonths = this._get(inst, 'stepMonths'); var isMultiMonth = (numMonths[0] != 1 || numMonths[1] != 1); var currentDate = this._daylightSavingAdjust((!inst.currentDay ? new Date(9999, 9, 9) : new Date(inst.currentYear, inst.currentMonth, inst.currentDay))); var minDate = this._getMinMaxDate(inst, 'min'); var maxDate = this._getMinMaxDate(inst, 'max'); var drawMonth = inst.drawMonth - showCurrentAtPos; var drawYear = inst.drawYear; if (drawMonth < 0) { drawMonth += 12; drawYear-- } if (maxDate) { var maxDraw = this._daylightSavingAdjust(new Date(maxDate.getFullYear(), maxDate.getMonth() - (numMonths[0] * numMonths[1]) + 1, maxDate.getDate())); maxDraw = (minDate && maxDraw < minDate ? minDate : maxDraw); while (this._daylightSavingAdjust(new Date(drawYear, drawMonth, 1)) > maxDraw) { drawMonth--; if (drawMonth < 0) { drawMonth = 11; drawYear-- } } } inst.drawMonth = drawMonth; inst.drawYear = drawYear; var prevText = this._get(inst, 'prevText'); prevText = (!navigationAsDateFormat ? prevText : this.formatDate(prevText, this._daylightSavingAdjust(new Date(drawYear, drawMonth - stepMonths, 1)), this._getFormatConfig(inst))); var prev = (this._canAdjustMonth(inst, -1, drawYear, drawMonth) ? '<a class="ui-datepicker-prev ui-corner-all" data-handler="prev" data-event="click"' + ' title="' + prevText + '"><span class="ui-icon ui-icon-circle-triangle-' + (isRTL ? 'e' : 'w') + '">' + prevText + '</span></a>' : (hideIfNoPrevNext ? '' : '<a class="ui-datepicker-prev ui-corner-all ui-state-disabled" title="' + prevText + '"><span class="ui-icon ui-icon-circle-triangle-' + (isRTL ? 'e' : 'w') + '">' + prevText + '</span></a>')); var nextText = this._get(inst, 'nextText'); nextText = (!navigationAsDateFormat ? nextText : this.formatDate(nextText, this._daylightSavingAdjust(new Date(drawYear, drawMonth + stepMonths, 1)), this._getFormatConfig(inst))); var next = (this._canAdjustMonth(inst, +1, drawYear, drawMonth) ? '<a class="ui-datepicker-next ui-corner-all" data-handler="next" data-event="click"' + ' title="' + nextText + '"><span class="ui-icon ui-icon-circle-triangle-' + (isRTL ? 'w' : 'e') + '">' + nextText + '</span></a>' : (hideIfNoPrevNext ? '' : '<a class="ui-datepicker-next ui-corner-all ui-state-disabled" title="' + nextText + '"><span class="ui-icon ui-icon-circle-triangle-' + (isRTL ? 'w' : 'e') + '">' + nextText + '</span></a>')); var currentText = this._get(inst, 'currentText'); var gotoDate = (this._get(inst, 'gotoCurrent') && inst.currentDay ? currentDate : today); currentText = (!navigationAsDateFormat ? currentText : this.formatDate(currentText, gotoDate, this._getFormatConfig(inst))); var controls = (!inst.inline ? '<button type="button" class="ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all" data-handler="hide" data-event="click">' + this._get(inst, 'closeText') + '</button>' : ''); var buttonPanel = (showButtonPanel) ? '<div class="ui-datepicker-buttonpane ui-widget-content">' + (isRTL ? controls : '') + (this._isInRange(inst, gotoDate) ? '<button type="button" class="ui-datepicker-current ui-state-default ui-priority-secondary ui-corner-all" data-handler="today" data-event="click"' + '>' + currentText + '</button>' : '') + (isRTL ? '' : controls) + '</div>' : ''; var firstDay = parseInt(this._get(inst, 'firstDay'), 10); firstDay = (isNaN(firstDay) ? 0 : firstDay); var showWeek = this._get(inst, 'showWeek'); var dayNames = this._get(inst, 'dayNames'); var dayNamesShort = this._get(inst, 'dayNamesShort'); var dayNamesMin = this._get(inst, 'dayNamesMin'); var monthNames = this._get(inst, 'monthNames'); var monthNamesShort = this._get(inst, 'monthNamesShort'); var beforeShowDay = this._get(inst, 'beforeShowDay'); var showOtherMonths = this._get(inst, 'showOtherMonths'); var selectOtherMonths = this._get(inst, 'selectOtherMonths'); var calculateWeek = this._get(inst, 'calculateWeek') || this.iso8601Week; var defaultDate = this._getDefaultDate(inst); var html = ''; for (var row = 0; row < numMonths[0]; row++) { var group = ''; this.maxRows = 4; for (var col = 0; col < numMonths[1]; col++) { var selectedDate = this._daylightSavingAdjust(new Date(drawYear, drawMonth, inst.selectedDay)); var cornerClass = ' ui-corner-all'; var calender = ''; if (isMultiMonth) { calender += '<div class="ui-datepicker-group'; if (numMonths[1] > 1) switch (col) { case 0: calender += ' ui-datepicker-group-first'; cornerClass = ' ui-corner-' + (isRTL ? 'right' : 'left'); break; case numMonths[1] - 1: calender += ' ui-datepicker-group-last'; cornerClass = ' ui-corner-' + (isRTL ? 'left' : 'right'); break; default: calender += ' ui-datepicker-group-middle'; cornerClass = ''; break } calender += '">' } calender += '<div class="ui-datepicker-header ui-widget-header ui-helper-clearfix' + cornerClass + '">' + (/all|left/.test(cornerClass) && row == 0 ? (isRTL ? next : prev) : '') + (/all|right/.test(cornerClass) && row == 0 ? (isRTL ? prev : next) : '') + this._generateMonthYearHeader(inst, drawMonth, drawYear, minDate, maxDate, row > 0 || col > 0, monthNames, monthNamesShort) + '</div><table class="ui-datepicker-calendar"><thead>' + '<tr>'; var thead = (showWeek ? '<th class="ui-datepicker-week-col">' + this._get(inst, 'weekHeader') + '</th>' : ''); for (var dow = 0; dow < 7; dow++) { var day = (dow + firstDay) % 7; thead += '<th' + ((dow + firstDay + 6) % 7 >= 5 ? ' class="ui-datepicker-week-end"' : '') + '>' + '<span title="' + dayNames[day] + '">' + dayNamesMin[day] + '</span></th>' } calender += thead + '</tr></thead><tbody>'; var daysInMonth = this._getDaysInMonth(drawYear, drawMonth); if (drawYear == inst.selectedYear && drawMonth == inst.selectedMonth) inst.selectedDay = Math.min(inst.selectedDay, daysInMonth); var leadDays = (this._getFirstDayOfMonth(drawYear, drawMonth) - firstDay + 7) % 7; var curRows = Math.ceil((leadDays + daysInMonth) / 7); var numRows = (isMultiMonth ? this.maxRows > curRows ? this.maxRows : curRows : curRows); this.maxRows = numRows; var printDate = this._daylightSavingAdjust(new Date(drawYear, drawMonth, 1 - leadDays)); for (var dRow = 0; dRow < numRows; dRow++) { calender += '<tr>'; var tbody = (!showWeek ? '' : '<td class="ui-datepicker-week-col">' + this._get(inst, 'calculateWeek')(printDate) + '</td>'); for (var dow = 0; dow < 7; dow++) { var daySettings = (beforeShowDay ? beforeShowDay.apply((inst.input ? inst.input[0] : null), [printDate]) : [true, '']); var otherMonth = (printDate.getMonth() != drawMonth); var unselectable = (otherMonth && !selectOtherMonths) || !daySettings[0] || (minDate && printDate < minDate) || (maxDate && printDate > maxDate); tbody += '<td class="' + ((dow + firstDay + 6) % 7 >= 5 ? ' ui-datepicker-week-end' : '') + (otherMonth ? ' ui-datepicker-other-month' : '') + ((printDate.getTime() == selectedDate.getTime() && drawMonth == inst.selectedMonth && inst._keyEvent) || (defaultDate.getTime() == printDate.getTime() && defaultDate.getTime() == selectedDate.getTime()) ? ' ' + this._dayOverClass : '') + (unselectable ? ' ' + this._unselectableClass + ' ui-state-disabled' : '') + (otherMonth && !showOtherMonths ? '' : ' ' + daySettings[1] + (printDate.getTime() == currentDate.getTime() ? ' ' + this._currentClass : '') + (printDate.getTime() == today.getTime() ? ' ui-datepicker-today' : '')) + '"' + ((!otherMonth || showOtherMonths) && daySettings[2] ? ' title="' + daySettings[2] + '"' : '') + (unselectable ? '' : ' data-handler="selectDay" data-event="click" data-month="' + printDate.getMonth() + '" data-year="' + printDate.getFullYear() + '"') + '>' + (otherMonth && !showOtherMonths ? '&#xa0;' : (unselectable ? '<span class="ui-state-default">' + printDate.getDate() + '</span>' : '<a class="ui-state-default' + (printDate.getTime() == today.getTime() ? ' ui-state-highlight' : '') + (printDate.getTime() == currentDate.getTime() ? ' ui-state-active' : '') + (otherMonth ? ' ui-priority-secondary' : '') + '" href="#">' + printDate.getDate() + '</a>')) + '</td>'; printDate.setDate(printDate.getDate() + 1); printDate = this._daylightSavingAdjust(printDate) } calender += tbody + '</tr>' } drawMonth++; if (drawMonth > 11) { drawMonth = 0; drawYear++ } calender += '</tbody></table>' + (isMultiMonth ? '</div>' + ((numMonths[0] > 0 && col == numMonths[1] - 1) ? '<div class="ui-datepicker-row-break"></div>' : '') : ''); group += calender } html += group } html += buttonPanel + ($.browser.msie && parseInt($.browser.version, 10) < 7 && !inst.inline ? '<iframe src="javascript:false;" class="ui-datepicker-cover" frameborder="0"></iframe>' : ''); inst._keyEvent = false; return html }, _generateMonthYearHeader: function (inst, drawMonth, drawYear, minDate, maxDate, secondary, monthNames, monthNamesShort) { var changeMonth = this._get(inst, 'changeMonth'); var changeYear = this._get(inst, 'changeYear'); var showMonthAfterYear = this._get(inst, 'showMonthAfterYear'); var html = '<div class="ui-datepicker-title">'; var monthHtml = ''; if (secondary || !changeMonth) monthHtml += '<span class="ui-datepicker-month">' + monthNames[drawMonth] + '</span>'; else { var inMinYear = (minDate && minDate.getFullYear() == drawYear); var inMaxYear = (maxDate && maxDate.getFullYear() == drawYear); monthHtml += '<select class="ui-datepicker-month" data-handler="selectMonth" data-event="change">'; for (var month = 0; month < 12; month++) { if ((!inMinYear || month >= minDate.getMonth()) && (!inMaxYear || month <= maxDate.getMonth())) monthHtml += '<option value="' + month + '"' + (month == drawMonth ? ' selected="selected"' : '') + '>' + monthNamesShort[month] + '</option>' } monthHtml += '</select>' } if (!showMonthAfterYear) html += monthHtml + (secondary || !(changeMonth && changeYear) ? '&#xa0;' : ''); if (!inst.yearshtml) { inst.yearshtml = ''; if (secondary || !changeYear) html += '<span class="ui-datepicker-year">' + drawYear + '</span>'; else { var years = this._get(inst, 'yearRange').split(':'); var thisYear = new Date().getFullYear(); var determineYear = function (value) { var year = (value.match(/c[+-].*/) ? drawYear + parseInt(value.substring(1), 10) : (value.match(/[+-].*/) ? thisYear + parseInt(value, 10) : parseInt(value, 10))); return (isNaN(year) ? thisYear : year) }; var year = determineYear(years[0]); var endYear = Math.max(year, determineYear(years[1] || '')); year = (minDate ? Math.max(year, minDate.getFullYear()) : year); endYear = (maxDate ? Math.min(endYear, maxDate.getFullYear()) : endYear); inst.yearshtml += '<select class="ui-datepicker-year" data-handler="selectYear" data-event="change">'; for (; year <= endYear; year++) { inst.yearshtml += '<option value="' + year + '"' + (year == drawYear ? ' selected="selected"' : '') + '>' + year + '</option>' } inst.yearshtml += '</select>'; html += inst.yearshtml; inst.yearshtml = null } } html += this._get(inst, 'yearSuffix'); if (showMonthAfterYear) html += (secondary || !(changeMonth && changeYear) ? '&#xa0;' : '') + monthHtml; html += '</div>'; return html }, _adjustInstDate: function (inst, offset, period) { var year = inst.drawYear + (period == 'Y' ? offset : 0); var month = inst.drawMonth + (period == 'M' ? offset : 0); var day = Math.min(inst.selectedDay, this._getDaysInMonth(year, month)) + (period == 'D' ? offset : 0); var date = this._restrictMinMax(inst, this._daylightSavingAdjust(new Date(year, month, day))); inst.selectedDay = date.getDate(); inst.drawMonth = inst.selectedMonth = date.getMonth(); inst.drawYear = inst.selectedYear = date.getFullYear(); if (period == 'M' || period == 'Y') this._notifyChange(inst) }, _restrictMinMax: function (inst, date) { var minDate = this._getMinMaxDate(inst, 'min'); var maxDate = this._getMinMaxDate(inst, 'max'); var newDate = (minDate && date < minDate ? minDate : date); newDate = (maxDate && newDate > maxDate ? maxDate : newDate); return newDate }, _notifyChange: function (inst) { var onChange = this._get(inst, 'onChangeMonthYear'); if (onChange) onChange.apply((inst.input ? inst.input[0] : null), [inst.selectedYear, inst.selectedMonth + 1, inst]) }, _getNumberOfMonths: function (inst) { var numMonths = this._get(inst, 'numberOfMonths'); return (numMonths == null ? [1, 1] : (typeof numMonths == 'number' ? [1, numMonths] : numMonths)) }, _getMinMaxDate: function (inst, minMax) { return this._determineDate(inst, this._get(inst, minMax + 'Date'), null) }, _getDaysInMonth: function (year, month) { return 32 - this._daylightSavingAdjust(new Date(year, month, 32)).getDate() }, _getFirstDayOfMonth: function (year, month) { return new Date(year, month, 1).getDay() }, _canAdjustMonth: function (inst, offset, curYear, curMonth) { var numMonths = this._getNumberOfMonths(inst); var date = this._daylightSavingAdjust(new Date(curYear, curMonth + (offset < 0 ? offset : numMonths[0] * numMonths[1]), 1)); if (offset < 0) date.setDate(this._getDaysInMonth(date.getFullYear(), date.getMonth())); return this._isInRange(inst, date) }, _isInRange: function (inst, date) { var minDate = this._getMinMaxDate(inst, 'min'); var maxDate = this._getMinMaxDate(inst, 'max'); return ((!minDate || date.getTime() >= minDate.getTime()) && (!maxDate || date.getTime() <= maxDate.getTime())) }, _getFormatConfig: function (inst) { var shortYearCutoff = this._get(inst, 'shortYearCutoff'); shortYearCutoff = (typeof shortYearCutoff != 'string' ? shortYearCutoff : new Date().getFullYear() % 100 + parseInt(shortYearCutoff, 10)); return { shortYearCutoff: shortYearCutoff, dayNamesShort: this._get(inst, 'dayNamesShort'), dayNames: this._get(inst, 'dayNames'), monthNamesShort: this._get(inst, 'monthNamesShort'), monthNames: this._get(inst, 'monthNames') } }, _formatDate: function (inst, day, month, year) { if (!day) { inst.currentDay = inst.selectedDay; inst.currentMonth = inst.selectedMonth; inst.currentYear = inst.selectedYear } var date = (day ? (typeof day == 'object' ? day : this._daylightSavingAdjust(new Date(year, month, day))) : this._daylightSavingAdjust(new Date(inst.currentYear, inst.currentMonth, inst.currentDay))); return this.formatDate(this._get(inst, 'dateFormat'), date, this._getFormatConfig(inst)) } }); function bindHover(dpDiv) { var selector = 'button, .ui-datepicker-prev, .ui-datepicker-next, .ui-datepicker-calendar td a'; return dpDiv.bind('mouseout', function (event) { var elem = $(event.target).closest(selector); if (!elem.length) { return } elem.removeClass("ui-state-hover ui-datepicker-prev-hover ui-datepicker-next-hover") }).bind('mouseover', function (event) { var elem = $(event.target).closest(selector); if ($.datepicker._isDisabledDatepicker(instActive.inline ? dpDiv.parent()[0] : instActive.input[0]) || !elem.length) { return } elem.parents('.ui-datepicker-calendar').find('a').removeClass('ui-state-hover'); elem.addClass('ui-state-hover'); if (elem.hasClass('ui-datepicker-prev')) elem.addClass('ui-datepicker-prev-hover'); if (elem.hasClass('ui-datepicker-next')) elem.addClass('ui-datepicker-next-hover') }) } function extendRemove(target, props) { $.extend(target, props); for (var name in props) if (props[name] == null || props[name] == undefined) target[name] = props[name]; return target }; function isArray(a) { return (a && (($.browser.safari && typeof a == 'object' && a.length) || (a.constructor && a.constructor.toString().match(/\Array\(\)/)))) }; $.fn.datepicker = function (options) { if (!this.length) { return this } if (!$.datepicker.initialized) { $(document).mousedown($.datepicker._checkExternalClick).find('body').append($.datepicker.dpDiv); $.datepicker.initialized = true } var otherArgs = Array.prototype.slice.call(arguments, 1); if (typeof options == 'string' && (options == 'isDisabled' || options == 'getDate' || options == 'widget')) return $.datepicker['_' + options + 'Datepicker'].apply($.datepicker, [this[0]].concat(otherArgs)); if (options == 'option' && arguments.length == 2 && typeof arguments[1] == 'string') return $.datepicker['_' + options + 'Datepicker'].apply($.datepicker, [this[0]].concat(otherArgs)); return this.each(function () { typeof options == 'string' ? $.datepicker['_' + options + 'Datepicker'].apply($.datepicker, [this].concat(otherArgs)) : $.datepicker._attachDatepicker(this, options) }) }; $.datepicker = new Datepicker(); $.datepicker.initialized = false; $.datepicker.uuid = new Date().getTime(); $.datepicker.version = "1.8.22"; window['DP_jQuery_' + dpuuid] = $ })(jQuery); (function ($, undefined) { var uiDialogClasses = 'ui-dialog ' + 'ui-widget ' + 'ui-widget-content ' + 'ui-corner-all ', sizeRelatedOptions = { buttons: true, height: true, maxHeight: true, maxWidth: true, minHeight: true, minWidth: true, width: true }, resizableRelatedOptions = { maxHeight: true, maxWidth: true, minHeight: true, minWidth: true }, attrFn = $.attrFn || { val: true, css: true, html: true, text: true, data: true, width: true, height: true, offset: true, click: true }; $.widget("ui.dialog", { options: { autoOpen: true, buttons: {}, closeOnEscape: true, closeText: 'close', dialogClass: '', draggable: true, hide: null, height: 'auto', maxHeight: false, maxWidth: false, minHeight: 150, minWidth: 150, modal: false, position: { my: 'center', at: 'center', collision: 'fit', using: function (pos) { var topOffset = $(this).css(pos).offset().top; if (topOffset < 0) { $(this).css('top', pos.top - topOffset) } } }, resizable: true, show: null, stack: true, title: '', width: 300, zIndex: 1000 }, _create: function () { this.originalTitle = this.element.attr('title'); if (typeof this.originalTitle !== "string") { this.originalTitle = "" } this.options.title = this.options.title || this.originalTitle; var self = this, options = self.options, title = options.title || '&#160;', titleId = $.ui.dialog.getTitleId(self.element), uiDialog = (self.uiDialog = $('<div></div>')).appendTo(document.body).hide().addClass(uiDialogClasses + options.dialogClass).css({ zIndex: options.zIndex }).attr('tabIndex', -1).css('outline', 0).keydown(function (event) { if (options.closeOnEscape && !event.isDefaultPrevented() && event.keyCode && event.keyCode === $.ui.keyCode.ESCAPE) { self.close(event); event.preventDefault() } }).attr({ role: 'dialog', 'aria-labelledby': titleId }).mousedown(function (event) { self.moveToTop(false, event) }), uiDialogContent = self.element.show().removeAttr('title').addClass('ui-dialog-content ' + 'ui-widget-content').appendTo(uiDialog), uiDialogTitlebar = (self.uiDialogTitlebar = $('<div></div>')).addClass('ui-dialog-titlebar ' + 'ui-widget-header ' + 'ui-corner-all ' + 'ui-helper-clearfix').prependTo(uiDialog), uiDialogTitlebarClose = $('<a href="#"></a>').addClass('ui-dialog-titlebar-close ' + 'ui-corner-all').attr('role', 'button').hover(function () { uiDialogTitlebarClose.addClass('ui-state-hover') }, function () { uiDialogTitlebarClose.removeClass('ui-state-hover') }).focus(function () { uiDialogTitlebarClose.addClass('ui-state-focus') }).blur(function () { uiDialogTitlebarClose.removeClass('ui-state-focus') }).click(function (event) { self.close(event); return false }).appendTo(uiDialogTitlebar), uiDialogTitlebarCloseText = (self.uiDialogTitlebarCloseText = $('<span></span>')).addClass('ui-icon ' + 'ui-icon-closethick').text(options.closeText).appendTo(uiDialogTitlebarClose), uiDialogTitle = $('<span></span>').addClass('ui-dialog-title').attr('id', titleId).html(title).prependTo(uiDialogTitlebar); if ($.isFunction(options.beforeclose) && !$.isFunction(options.beforeClose)) { options.beforeClose = options.beforeclose } uiDialogTitlebar.find("*").add(uiDialogTitlebar).disableSelection(); if (options.draggable && $.fn.draggable) { self._makeDraggable() } if (options.resizable && $.fn.resizable) { self._makeResizable() } self._createButtons(options.buttons); self._isOpen = false; if ($.fn.bgiframe) { uiDialog.bgiframe() } }, _init: function () { if (this.options.autoOpen) { this.open() } }, destroy: function () { var self = this; if (self.overlay) { self.overlay.destroy() } self.uiDialog.hide(); self.element.unbind('.dialog').removeData('dialog').removeClass('ui-dialog-content ui-widget-content').hide().appendTo('body'); self.uiDialog.remove(); if (self.originalTitle) { self.element.attr('title', self.originalTitle) } return self }, widget: function () { return this.uiDialog }, close: function (event) { var self = this, maxZ, thisZ; if (false === self._trigger('beforeClose', event)) { return } if (self.overlay) { self.overlay.destroy() } self.uiDialog.unbind('keypress.ui-dialog'); self._isOpen = false; if (self.options.hide) { self.uiDialog.hide(self.options.hide, function () { self._trigger('close', event) }) } else { self.uiDialog.hide(); self._trigger('close', event) } $.ui.dialog.overlay.resize(); if (self.options.modal) { maxZ = 0; $('.ui-dialog').each(function () { if (this !== self.uiDialog[0]) { thisZ = $(this).css('z-index'); if (!isNaN(thisZ)) { maxZ = Math.max(maxZ, thisZ) } } }); $.ui.dialog.maxZ = maxZ } return self }, isOpen: function () { return this._isOpen }, moveToTop: function (force, event) { var self = this, options = self.options, saveScroll; if ((options.modal && !force) || (!options.stack && !options.modal)) { return self._trigger('focus', event) } if (options.zIndex > $.ui.dialog.maxZ) { $.ui.dialog.maxZ = options.zIndex } if (self.overlay) { $.ui.dialog.maxZ += 1; self.overlay.$el.css('z-index', $.ui.dialog.overlay.maxZ = $.ui.dialog.maxZ) } saveScroll = { scrollTop: self.element.scrollTop(), scrollLeft: self.element.scrollLeft() }; $.ui.dialog.maxZ += 1; self.uiDialog.css('z-index', $.ui.dialog.maxZ); self.element.attr(saveScroll); self._trigger('focus', event); return self }, open: function () { if (this._isOpen) { return } var self = this, options = self.options, uiDialog = self.uiDialog; self.overlay = options.modal ? new $.ui.dialog.overlay(self) : null; self._size(); self._position(options.position); uiDialog.show(options.show); self.moveToTop(true); if (options.modal) { uiDialog.bind("keydown.ui-dialog", function (event) { if (event.keyCode !== $.ui.keyCode.TAB) { return } var tabbables = $(':tabbable', this), first = tabbables.filter(':first'), last = tabbables.filter(':last'); if (event.target === last[0] && !event.shiftKey) { first.focus(1); return false } else if (event.target === first[0] && event.shiftKey) { last.focus(1); return false } }) } $(self.element.find(':tabbable').get().concat(uiDialog.find('.ui-dialog-buttonpane :tabbable').get().concat(uiDialog.get()))).eq(0).focus(); self._isOpen = true; self._trigger('open'); return self }, _createButtons: function (buttons) { var self = this, hasButtons = false, uiDialogButtonPane = $('<div></div>').addClass('ui-dialog-buttonpane ' + 'ui-widget-content ' + 'ui-helper-clearfix'), uiButtonSet = $("<div></div>").addClass("ui-dialog-buttonset").appendTo(uiDialogButtonPane); self.uiDialog.find('.ui-dialog-buttonpane').remove(); if (typeof buttons === 'object' && buttons !== null) { $.each(buttons, function () { return !(hasButtons = true) }) } if (hasButtons) { $.each(buttons, function (name, props) { props = $.isFunction(props) ? { click: props, text: name } : props; var button = $('<button type="button"></button>').click(function () { props.click.apply(self.element[0], arguments) }).appendTo(uiButtonSet); $.each(props, function (key, value) { if (key === "click") { return } if (key in attrFn) { button[key](value) } else { button.attr(key, value) } }); if ($.fn.button) { button.button() } }); uiDialogButtonPane.appendTo(self.uiDialog) } }, _makeDraggable: function () { var self = this, options = self.options, doc = $(document), heightBeforeDrag; function filteredUi(ui) { return { position: ui.position, offset: ui.offset } } self.uiDialog.draggable({ cancel: '.ui-dialog-content, .ui-dialog-titlebar-close', handle: '.ui-dialog-titlebar', containment: 'document', start: function (event, ui) { heightBeforeDrag = options.height === "auto" ? "auto" : $(this).height(); $(this).height($(this).height()).addClass("ui-dialog-dragging"); self._trigger('dragStart', event, filteredUi(ui)) }, drag: function (event, ui) { self._trigger('drag', event, filteredUi(ui)) }, stop: function (event, ui) { options.position = [ui.position.left - doc.scrollLeft(), ui.position.top - doc.scrollTop()]; $(this).removeClass("ui-dialog-dragging").height(heightBeforeDrag); self._trigger('dragStop', event, filteredUi(ui)); $.ui.dialog.overlay.resize() } }) }, _makeResizable: function (handles) { handles = (handles === undefined ? this.options.resizable : handles); var self = this, options = self.options, position = self.uiDialog.css('position'), resizeHandles = (typeof handles === 'string' ? handles : 'n,e,s,w,se,sw,ne,nw'); function filteredUi(ui) { return { originalPosition: ui.originalPosition, originalSize: ui.originalSize, position: ui.position, size: ui.size } } self.uiDialog.resizable({ cancel: '.ui-dialog-content', containment: 'document', alsoResize: self.element, maxWidth: options.maxWidth, maxHeight: options.maxHeight, minWidth: options.minWidth, minHeight: self._minHeight(), handles: resizeHandles, start: function (event, ui) { $(this).addClass("ui-dialog-resizing"); self._trigger('resizeStart', event, filteredUi(ui)) }, resize: function (event, ui) { self._trigger('resize', event, filteredUi(ui)) }, stop: function (event, ui) { $(this).removeClass("ui-dialog-resizing"); options.height = $(this).height(); options.width = $(this).width(); self._trigger('resizeStop', event, filteredUi(ui)); $.ui.dialog.overlay.resize() } }).css('position', position).find('.ui-resizable-se').addClass('ui-icon ui-icon-grip-diagonal-se') }, _minHeight: function () { var options = this.options; if (options.height === 'auto') { return options.minHeight } else { return Math.min(options.minHeight, options.height) } }, _position: function (position) { var myAt = [], offset = [0, 0], isVisible; if (position) { if (typeof position === 'string' || (typeof position === 'object' && '0' in position)) { myAt = position.split ? position.split(' ') : [position[0], position[1]]; if (myAt.length === 1) { myAt[1] = myAt[0] } $.each(['left', 'top'], function (i, offsetPosition) { if (+myAt[i] === myAt[i]) { offset[i] = myAt[i]; myAt[i] = offsetPosition } }); position = { my: myAt.join(" "), at: myAt.join(" "), offset: offset.join(" ") } } position = $.extend({}, $.ui.dialog.prototype.options.position, position) } else { position = $.ui.dialog.prototype.options.position } isVisible = this.uiDialog.is(':visible'); if (!isVisible) { this.uiDialog.show() } this.uiDialog.css({ top: 0, left: 0 }).position($.extend({ of: window }, position)); if (!isVisible) { this.uiDialog.hide() } }, _setOptions: function (options) { var self = this, resizableOptions = {}, resize = false; $.each(options, function (key, value) { self._setOption(key, value); if (key in sizeRelatedOptions) { resize = true } if (key in resizableRelatedOptions) { resizableOptions[key] = value } }); if (resize) { this._size() } if (this.uiDialog.is(":data(resizable)")) { this.uiDialog.resizable("option", resizableOptions) } }, _setOption: function (key, value) { var self = this, uiDialog = self.uiDialog; switch (key) { case "beforeclose": key = "beforeClose"; break; case "buttons": self._createButtons(value); break; case "closeText": self.uiDialogTitlebarCloseText.text("" + value); break; case "dialogClass": uiDialog.removeClass(self.options.dialogClass).addClass(uiDialogClasses + value); break; case "disabled": if (value) { uiDialog.addClass('ui-dialog-disabled') } else { uiDialog.removeClass('ui-dialog-disabled') } break; case "draggable": var isDraggable = uiDialog.is(":data(draggable)"); if (isDraggable && !value) { uiDialog.draggable("destroy") } if (!isDraggable && value) { self._makeDraggable() } break; case "position": self._position(value); break; case "resizable": var isResizable = uiDialog.is(":data(resizable)"); if (isResizable && !value) { uiDialog.resizable('destroy') } if (isResizable && typeof value === 'string') { uiDialog.resizable('option', 'handles', value) } if (!isResizable && value !== false) { self._makeResizable(value) } break; case "title": $(".ui-dialog-title", self.uiDialogTitlebar).html("" + (value || '&#160;')); break } $.Widget.prototype._setOption.apply(self, arguments) }, _size: function () { var options = this.options, nonContentHeight, minContentHeight, isVisible = this.uiDialog.is(":visible"); this.element.show().css({ width: 'auto', minHeight: 0, height: 0 }); if (options.minWidth > options.width) { options.width = options.minWidth } nonContentHeight = this.uiDialog.css({ height: 'auto', width: options.width }).height(); minContentHeight = Math.max(0, options.minHeight - nonContentHeight); if (options.height === "auto") { if ($.support.minHeight) { this.element.css({ minHeight: minContentHeight, height: "auto" }) } else { this.uiDialog.show(); var autoHeight = this.element.css("height", "auto").height(); if (!isVisible) { this.uiDialog.hide() } this.element.height(Math.max(autoHeight, minContentHeight)) } } else { this.element.height(Math.max(options.height - nonContentHeight, 0)) } if (this.uiDialog.is(':data(resizable)')) { this.uiDialog.resizable('option', 'minHeight', this._minHeight()) } } }); $.extend($.ui.dialog, { version: "1.8.22", uuid: 0, maxZ: 0, getTitleId: function ($el) { var id = $el.attr('id'); if (!id) { this.uuid += 1; id = this.uuid } return 'ui-dialog-title-' + id }, overlay: function (dialog) { this.$el = $.ui.dialog.overlay.create(dialog) } }); $.extend($.ui.dialog.overlay, { instances: [], oldInstances: [], maxZ: 0, events: $.map('focus,mousedown,mouseup,keydown,keypress,click'.split(','), function (event) { return event + '.dialog-overlay' }).join(' '), create: function (dialog) { if (this.instances.length === 0) { setTimeout(function () { if ($.ui.dialog.overlay.instances.length) { $(document).bind($.ui.dialog.overlay.events, function (event) { if ($(event.target).zIndex() < $.ui.dialog.overlay.maxZ) { return false } }) } }, 1); $(document).bind('keydown.dialog-overlay', function (event) { if (dialog.options.closeOnEscape && !event.isDefaultPrevented() && event.keyCode && event.keyCode === $.ui.keyCode.ESCAPE) { dialog.close(event); event.preventDefault() } }); $(window).bind('resize.dialog-overlay', $.ui.dialog.overlay.resize) } var $el = (this.oldInstances.pop() || $('<div></div>').addClass('ui-widget-overlay')).appendTo(document.body).css({ width: this.width(), height: this.height() }); if ($.fn.bgiframe) { $el.bgiframe() } this.instances.push($el); return $el }, destroy: function ($el) { var indexOf = $.inArray($el, this.instances); if (indexOf != -1) { this.oldInstances.push(this.instances.splice(indexOf, 1)[0]) } if (this.instances.length === 0) { $([document, window]).unbind('.dialog-overlay') } $el.remove(); var maxZ = 0; $.each(this.instances, function () { maxZ = Math.max(maxZ, this.css('z-index')) }); this.maxZ = maxZ }, height: function () { var scrollHeight, offsetHeight; if ($.browser.msie && $.browser.version < 7) { scrollHeight = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight); offsetHeight = Math.max(document.documentElement.offsetHeight, document.body.offsetHeight); if (scrollHeight < offsetHeight) { return $(window).height() + 'px' } else { return scrollHeight + 'px' } } else { return $(document).height() + 'px' } }, width: function () { var scrollWidth, offsetWidth; if ($.browser.msie) { scrollWidth = Math.max(document.documentElement.scrollWidth, document.body.scrollWidth); offsetWidth = Math.max(document.documentElement.offsetWidth, document.body.offsetWidth); if (scrollWidth < offsetWidth) { return $(window).width() + 'px' } else { return scrollWidth + 'px' } } else { return $(document).width() + 'px' } }, resize: function () { var $overlays = $([]); $.each($.ui.dialog.overlay.instances, function () { $overlays = $overlays.add(this) }); $overlays.css({ width: 0, height: 0 }).css({ width: $.ui.dialog.overlay.width(), height: $.ui.dialog.overlay.height() }) } }); $.extend($.ui.dialog.overlay.prototype, { destroy: function () { $.ui.dialog.overlay.destroy(this.$el) } }) }(jQuery)); (function ($, undefined) { $.ui = $.ui || {}; var horizontalPositions = /left|center|right/, verticalPositions = /top|center|bottom/, center = "center", support = {}, _position = $.fn.position, _offset = $.fn.offset; $.fn.position = function (options) { if (!options || !options.of) { return _position.apply(this, arguments) } options = $.extend({}, options); var target = $(options.of), targetElem = target[0], collision = (options.collision || "flip").split(" "), offset = options.offset ? options.offset.split(" ") : [0, 0], targetWidth, targetHeight, basePosition; if (targetElem.nodeType === 9) { targetWidth = target.width(); targetHeight = target.height(); basePosition = { top: 0, left: 0 } } else if (targetElem.setTimeout) { targetWidth = target.width(); targetHeight = target.height(); basePosition = { top: target.scrollTop(), left: target.scrollLeft() } } else if (targetElem.preventDefault) { options.at = "left top"; targetWidth = targetHeight = 0; basePosition = { top: options.of.pageY, left: options.of.pageX } } else { targetWidth = target.outerWidth(); targetHeight = target.outerHeight(); basePosition = target.offset() } $.each(["my", "at"], function () { var pos = (options[this] || "").split(" "); if (pos.length === 1) { pos = horizontalPositions.test(pos[0]) ? pos.concat([center]) : verticalPositions.test(pos[0]) ? [center].concat(pos) : [center, center] } pos[0] = horizontalPositions.test(pos[0]) ? pos[0] : center; pos[1] = verticalPositions.test(pos[1]) ? pos[1] : center; options[this] = pos }); if (collision.length === 1) { collision[1] = collision[0] } offset[0] = parseInt(offset[0], 10) || 0; if (offset.length === 1) { offset[1] = offset[0] } offset[1] = parseInt(offset[1], 10) || 0; if (options.at[0] === "right") { basePosition.left += targetWidth } else if (options.at[0] === center) { basePosition.left += targetWidth / 2 } if (options.at[1] === "bottom") { basePosition.top += targetHeight } else if (options.at[1] === center) { basePosition.top += targetHeight / 2 } basePosition.left += offset[0]; basePosition.top += offset[1]; return this.each(function () { var elem = $(this), elemWidth = elem.outerWidth(), elemHeight = elem.outerHeight(), marginLeft = parseInt($.curCSS(this, "marginLeft", true)) || 0, marginTop = parseInt($.curCSS(this, "marginTop", true)) || 0, collisionWidth = elemWidth + marginLeft + (parseInt($.curCSS(this, "marginRight", true)) || 0), collisionHeight = elemHeight + marginTop + (parseInt($.curCSS(this, "marginBottom", true)) || 0), position = $.extend({}, basePosition), collisionPosition; if (options.my[0] === "right") { position.left -= elemWidth } else if (options.my[0] === center) { position.left -= elemWidth / 2 } if (options.my[1] === "bottom") { position.top -= elemHeight } else if (options.my[1] === center) { position.top -= elemHeight / 2 } if (!support.fractions) { position.left = Math.round(position.left); position.top = Math.round(position.top) } collisionPosition = { left: position.left - marginLeft, top: position.top - marginTop }; $.each(["left", "top"], function (i, dir) { if ($.ui.position[collision[i]]) { $.ui.position[collision[i]][dir](position, { targetWidth: targetWidth, targetHeight: targetHeight, elemWidth: elemWidth, elemHeight: elemHeight, collisionPosition: collisionPosition, collisionWidth: collisionWidth, collisionHeight: collisionHeight, offset: offset, my: options.my, at: options.at }) } }); if ($.fn.bgiframe) { elem.bgiframe() } var pos = $.extend(position, { using: options.using }); elem.css({ "top": pos.top, "left": pos.left }) }) }; $.ui.position = { fit: { left: function (position, data) { var win = $(window), over = data.collisionPosition.left + data.collisionWidth - win.width() - win.scrollLeft(); position.left = over > 0 ? position.left - over : Math.max(position.left - data.collisionPosition.left, position.left) }, top: function (position, data) { var win = $(window), over = data.collisionPosition.top + data.collisionHeight - win.height() - win.scrollTop(); position.top = over > 0 ? position.top - over : Math.max(position.top - data.collisionPosition.top, position.top) } }, flip: { left: function (position, data) { if (data.at[0] === center) { return } var win = $(window), over = data.collisionPosition.left + data.collisionWidth - win.width() - win.scrollLeft(), myOffset = data.my[0] === "left" ? -data.elemWidth : data.my[0] === "right" ? data.elemWidth : 0, atOffset = data.at[0] === "left" ? data.targetWidth : -data.targetWidth, offset = -2 * data.offset[0]; position.left += data.collisionPosition.left < 0 ? myOffset + atOffset + offset : over > 0 ? myOffset + atOffset + offset : 0 }, top: function (position, data) { if (data.at[1] === center) { return } var win = $(window), over = data.collisionPosition.top + data.collisionHeight - win.height() - win.scrollTop(), myOffset = data.my[1] === "top" ? -data.elemHeight : data.my[1] === "bottom" ? data.elemHeight : 0, atOffset = data.at[1] === "top" ? data.targetHeight : -data.targetHeight, offset = -2 * data.offset[1]; position.top += data.collisionPosition.top < 0 ? myOffset + atOffset + offset : over > 0 ? myOffset + atOffset + offset : 0 } } }; if (!$.offset.setOffset) { $.offset.setOffset = function (elem, options) { if (/static/.test($.curCSS(elem, "position"))) { elem.style.position = "relative" } var curElem = $(elem), curOffset = curElem.offset(), curTop = parseInt($.curCSS(elem, "top", true), 10) || 0, curLeft = parseInt($.curCSS(elem, "left", true), 10) || 0, props = { top: (options.top - curOffset.top) + curTop, left: (options.left - curOffset.left) + curLeft }; if ('using' in options) { options.using.call(elem, props) } else { curElem.css(props) } }; $.fn.offset = function (options) { var elem = this[0]; if (!elem || !elem.ownerDocument) { return null } if (options) { if ($.isFunction(options)) { return this.each(function (i) { $(this).offset(options.call(this, i, $(this).offset())) }) } return this.each(function () { $.offset.setOffset(this, options) }) } return _offset.call(this) } } (function () { var body = document.getElementsByTagName("body")[0], div = document.createElement("div"), testElement, testElementParent, testElementStyle, offset, offsetTotal; testElement = document.createElement(body ? "div" : "body"); testElementStyle = { visibility: "hidden", width: 0, height: 0, border: 0, margin: 0, background: "none" }; if (body) { $.extend(testElementStyle, { position: "absolute", left: "-1000px", top: "-1000px" }) } for (var i in testElementStyle) { testElement.style[i] = testElementStyle[i] } testElement.appendChild(div); testElementParent = body || document.documentElement; testElementParent.insertBefore(testElement, testElementParent.firstChild); div.style.cssText = "position: absolute; left: 10.7432222px; top: 10.432325px; height: 30px; width: 201px;"; offset = $(div).offset(function (_, offset) { return offset }).offset(); testElement.innerHTML = ""; testElementParent.removeChild(testElement); offsetTotal = offset.top + offset.left + (body ? 2000 : 0); support.fractions = offsetTotal > 21 && offsetTotal < 22 })() }(jQuery)); (function ($, undefined) { $.widget("ui.progressbar", { options: { value: 0, max: 100 }, min: 0, _create: function () { this.element.addClass("ui-progressbar ui-widget ui-widget-content ui-corner-all").attr({ role: "progressbar", "aria-valuemin": this.min, "aria-valuemax": this.options.max, "aria-valuenow": this._value() }); this.valueDiv = $("<div class='ui-progressbar-value ui-widget-header ui-corner-left'></div>").appendTo(this.element); this.oldValue = this._value(); this._refreshValue() }, destroy: function () { this.element.removeClass("ui-progressbar ui-widget ui-widget-content ui-corner-all").removeAttr("role").removeAttr("aria-valuemin").removeAttr("aria-valuemax").removeAttr("aria-valuenow"); this.valueDiv.remove(); $.Widget.prototype.destroy.apply(this, arguments) }, value: function (newValue) { if (newValue === undefined) { return this._value() } this._setOption("value", newValue); return this }, _setOption: function (key, value) { if (key === "value") { this.options.value = value; this._refreshValue(); if (this._value() === this.options.max) { this._trigger("complete") } } $.Widget.prototype._setOption.apply(this, arguments) }, _value: function () { var val = this.options.value; if (typeof val !== "number") { val = 0 } return Math.min(this.options.max, Math.max(this.min, val)) }, _percentage: function () { return 100 * this._value() / this.options.max }, _refreshValue: function () { var value = this.value(); var percentage = this._percentage(); if (this.oldValue !== value) { this.oldValue = value; this._trigger("change") } this.valueDiv.toggle(value > this.min).toggleClass("ui-corner-right", value === this.options.max).width(percentage.toFixed(0) + "%"); this.element.attr("aria-valuenow", value) } }); $.extend($.ui.progressbar, { version: "1.8.22" }) })(jQuery); (function ($, undefined) { var numPages = 5; $.widget("ui.slider", $.ui.mouse, { widgetEventPrefix: "slide", options: { animate: false, distance: 0, max: 100, min: 0, orientation: "horizontal", range: false, step: 1, value: 0, values: null }, _create: function () { var self = this, o = this.options, existingHandles = this.element.find(".ui-slider-handle").addClass("ui-state-default ui-corner-all"), handle = "<a class='ui-slider-handle ui-state-default ui-corner-all' href='#'></a>", handleCount = (o.values && o.values.length) || 1, handles = []; this._keySliding = false; this._mouseSliding = false; this._animateOff = true; this._handleIndex = null; this._detectOrientation(); this._mouseInit(); this.element.addClass("ui-slider" + " ui-slider-" + this.orientation + " ui-widget" + " ui-widget-content" + " ui-corner-all" + (o.disabled ? " ui-slider-disabled ui-disabled" : "")); this.range = $([]); if (o.range) { if (o.range === true) { if (!o.values) { o.values = [this._valueMin(), this._valueMin()] } if (o.values.length && o.values.length !== 2) { o.values = [o.values[0], o.values[0]] } } this.range = $("<div></div>").appendTo(this.element).addClass("ui-slider-range" + " ui-widget-header" + ((o.range === "min" || o.range === "max") ? " ui-slider-range-" + o.range : "")) } for (var i = existingHandles.length; i < handleCount; i += 1) { handles.push(handle) } this.handles = existingHandles.add($(handles.join("")).appendTo(self.element)); this.handle = this.handles.eq(0); this.handles.add(this.range).filter("a").click(function (event) { event.preventDefault() }).hover(function () { if (!o.disabled) { $(this).addClass("ui-state-hover") } }, function () { $(this).removeClass("ui-state-hover") }).focus(function () { if (!o.disabled) { $(".ui-slider .ui-state-focus").removeClass("ui-state-focus"); $(this).addClass("ui-state-focus") } else { $(this).blur() } }).blur(function () { $(this).removeClass("ui-state-focus") }); this.handles.each(function (i) { $(this).data("index.ui-slider-handle", i) }); this.handles.keydown(function (event) { var index = $(this).data("index.ui-slider-handle"), allowed, curVal, newVal, step; if (self.options.disabled) { return } switch (event.keyCode) { case $.ui.keyCode.HOME: case $.ui.keyCode.END: case $.ui.keyCode.PAGE_UP: case $.ui.keyCode.PAGE_DOWN: case $.ui.keyCode.UP: case $.ui.keyCode.RIGHT: case $.ui.keyCode.DOWN: case $.ui.keyCode.LEFT: event.preventDefault(); if (!self._keySliding) { self._keySliding = true; $(this).addClass("ui-state-active"); allowed = self._start(event, index); if (allowed === false) { return } } break } step = self.options.step; if (self.options.values && self.options.values.length) { curVal = newVal = self.values(index) } else { curVal = newVal = self.value() } switch (event.keyCode) { case $.ui.keyCode.HOME: newVal = self._valueMin(); break; case $.ui.keyCode.END: newVal = self._valueMax(); break; case $.ui.keyCode.PAGE_UP: newVal = self._trimAlignValue(curVal + ((self._valueMax() - self._valueMin()) / numPages)); break; case $.ui.keyCode.PAGE_DOWN: newVal = self._trimAlignValue(curVal - ((self._valueMax() - self._valueMin()) / numPages)); break; case $.ui.keyCode.UP: case $.ui.keyCode.RIGHT: if (curVal === self._valueMax()) { return } newVal = self._trimAlignValue(curVal + step); break; case $.ui.keyCode.DOWN: case $.ui.keyCode.LEFT: if (curVal === self._valueMin()) { return } newVal = self._trimAlignValue(curVal - step); break } self._slide(event, index, newVal) }).keyup(function (event) { var index = $(this).data("index.ui-slider-handle"); if (self._keySliding) { self._keySliding = false; self._stop(event, index); self._change(event, index); $(this).removeClass("ui-state-active") } }); this._refreshValue(); this._animateOff = false }, destroy: function () { this.handles.remove(); this.range.remove(); this.element.removeClass("ui-slider" + " ui-slider-horizontal" + " ui-slider-vertical" + " ui-slider-disabled" + " ui-widget" + " ui-widget-content" + " ui-corner-all").removeData("slider").unbind(".slider"); this._mouseDestroy(); return this }, _mouseCapture: function (event) { var o = this.options, position, normValue, distance, closestHandle, self, index, allowed, offset, mouseOverHandle; if (o.disabled) { return false } this.elementSize = { width: this.element.outerWidth(), height: this.element.outerHeight() }; this.elementOffset = this.element.offset(); position = { x: event.pageX, y: event.pageY }; normValue = this._normValueFromMouse(position); distance = this._valueMax() - this._valueMin() + 1; self = this; this.handles.each(function (i) { var thisDistance = Math.abs(normValue - self.values(i)); if (distance > thisDistance) { distance = thisDistance; closestHandle = $(this); index = i } }); if (o.range === true && this.values(1) === o.min) { index += 1; closestHandle = $(this.handles[index]) } allowed = this._start(event, index); if (allowed === false) { return false } this._mouseSliding = true; self._handleIndex = index; closestHandle.addClass("ui-state-active").focus(); offset = closestHandle.offset(); mouseOverHandle = !$(event.target).parents().andSelf().is(".ui-slider-handle"); this._clickOffset = mouseOverHandle ? { left: 0, top: 0 } : { left: event.pageX - offset.left - (closestHandle.width() / 2), top: event.pageY - offset.top - (closestHandle.height() / 2) - (parseInt(closestHandle.css("borderTopWidth"), 10) || 0) - (parseInt(closestHandle.css("borderBottomWidth"), 10) || 0) + (parseInt(closestHandle.css("marginTop"), 10) || 0) }; if (!this.handles.hasClass("ui-state-hover")) { this._slide(event, index, normValue) } this._animateOff = true; return true }, _mouseStart: function (event) { return true }, _mouseDrag: function (event) { var position = { x: event.pageX, y: event.pageY }, normValue = this._normValueFromMouse(position); this._slide(event, this._handleIndex, normValue); return false }, _mouseStop: function (event) { this.handles.removeClass("ui-state-active"); this._mouseSliding = false; this._stop(event, this._handleIndex); this._change(event, this._handleIndex); this._handleIndex = null; this._clickOffset = null; this._animateOff = false; return false }, _detectOrientation: function () { this.orientation = (this.options.orientation === "vertical") ? "vertical" : "horizontal" }, _normValueFromMouse: function (position) { var pixelTotal, pixelMouse, percentMouse, valueTotal, valueMouse; if (this.orientation === "horizontal") { pixelTotal = this.elementSize.width; pixelMouse = position.x - this.elementOffset.left - (this._clickOffset ? this._clickOffset.left : 0) } else { pixelTotal = this.elementSize.height; pixelMouse = position.y - this.elementOffset.top - (this._clickOffset ? this._clickOffset.top : 0) } percentMouse = (pixelMouse / pixelTotal); if (percentMouse > 1) { percentMouse = 1 } if (percentMouse < 0) { percentMouse = 0 } if (this.orientation === "vertical") { percentMouse = 1 - percentMouse } valueTotal = this._valueMax() - this._valueMin(); valueMouse = this._valueMin() + percentMouse * valueTotal; return this._trimAlignValue(valueMouse) }, _start: function (event, index) { var uiHash = { handle: this.handles[index], value: this.value() }; if (this.options.values && this.options.values.length) { uiHash.value = this.values(index); uiHash.values = this.values() } return this._trigger("start", event, uiHash) }, _slide: function (event, index, newVal) { var otherVal, newValues, allowed; if (this.options.values && this.options.values.length) { otherVal = this.values(index ? 0 : 1); if ((this.options.values.length === 2 && this.options.range === true) && ((index === 0 && newVal > otherVal) || (index === 1 && newVal < otherVal))) { newVal = otherVal } if (newVal !== this.values(index)) { newValues = this.values(); newValues[index] = newVal; allowed = this._trigger("slide", event, { handle: this.handles[index], value: newVal, values: newValues }); otherVal = this.values(index ? 0 : 1); if (allowed !== false) { this.values(index, newVal, true) } } } else { if (newVal !== this.value()) { allowed = this._trigger("slide", event, { handle: this.handles[index], value: newVal }); if (allowed !== false) { this.value(newVal) } } } }, _stop: function (event, index) { var uiHash = { handle: this.handles[index], value: this.value() }; if (this.options.values && this.options.values.length) { uiHash.value = this.values(index); uiHash.values = this.values() } this._trigger("stop", event, uiHash) }, _change: function (event, index) { if (!this._keySliding && !this._mouseSliding) { var uiHash = { handle: this.handles[index], value: this.value() }; if (this.options.values && this.options.values.length) { uiHash.value = this.values(index); uiHash.values = this.values() } this._trigger("change", event, uiHash) } }, value: function (newValue) { if (arguments.length) { this.options.value = this._trimAlignValue(newValue); this._refreshValue(); this._change(null, 0); return } return this._value() }, values: function (index, newValue) { var vals, newValues, i; if (arguments.length > 1) { this.options.values[index] = this._trimAlignValue(newValue); this._refreshValue(); this._change(null, index); return } if (arguments.length) { if ($.isArray(arguments[0])) { vals = this.options.values; newValues = arguments[0]; for (i = 0; i < vals.length; i += 1) { vals[i] = this._trimAlignValue(newValues[i]); this._change(null, i) } this._refreshValue() } else { if (this.options.values && this.options.values.length) { return this._values(index) } else { return this.value() } } } else { return this._values() } }, _setOption: function (key, value) { var i, valsLength = 0; if ($.isArray(this.options.values)) { valsLength = this.options.values.length } $.Widget.prototype._setOption.apply(this, arguments); switch (key) { case "disabled": if (value) { this.handles.filter(".ui-state-focus").blur(); this.handles.removeClass("ui-state-hover"); this.handles.propAttr("disabled", true); this.element.addClass("ui-disabled") } else { this.handles.propAttr("disabled", false); this.element.removeClass("ui-disabled") } break; case "orientation": this._detectOrientation(); this.element.removeClass("ui-slider-horizontal ui-slider-vertical").addClass("ui-slider-" + this.orientation); this._refreshValue(); break; case "value": this._animateOff = true; this._refreshValue(); this._change(null, 0); this._animateOff = false; break; case "values": this._animateOff = true; this._refreshValue(); for (i = 0; i < valsLength; i += 1) { this._change(null, i) } this._animateOff = false; break } }, _value: function () { var val = this.options.value; val = this._trimAlignValue(val); return val }, _values: function (index) { var val, vals, i; if (arguments.length) { val = this.options.values[index]; val = this._trimAlignValue(val); return val } else { vals = this.options.values.slice(); for (i = 0; i < vals.length; i += 1) { vals[i] = this._trimAlignValue(vals[i]) } return vals } }, _trimAlignValue: function (val) { if (val <= this._valueMin()) { return this._valueMin() } if (val >= this._valueMax()) { return this._valueMax() } var step = (this.options.step > 0) ? this.options.step : 1, valModStep = (val - this._valueMin()) % step, alignValue = val - valModStep; if (Math.abs(valModStep) * 2 >= step) { alignValue += (valModStep > 0) ? step : (-step) } return parseFloat(alignValue.toFixed(5)) }, _valueMin: function () { return this.options.min }, _valueMax: function () { return this.options.max }, _refreshValue: function () { var oRange = this.options.range, o = this.options, self = this, animate = (!this._animateOff) ? o.animate : false, valPercent, _set = {}, lastValPercent, value, valueMin, valueMax; if (this.options.values && this.options.values.length) { this.handles.each(function (i, j) { valPercent = (self.values(i) - self._valueMin()) / (self._valueMax() - self._valueMin()) * 100; _set[self.orientation === "horizontal" ? "left" : "bottom"] = valPercent + "%"; $(this).stop(1, 1)[animate ? "animate" : "css"](_set, o.animate); if (self.options.range === true) { if (self.orientation === "horizontal") { if (i === 0) { self.range.stop(1, 1)[animate ? "animate" : "css"]({ left: valPercent + "%" }, o.animate) } if (i === 1) { self.range[animate ? "animate" : "css"]({ width: (valPercent - lastValPercent) + "%" }, { queue: false, duration: o.animate }) } } else { if (i === 0) { self.range.stop(1, 1)[animate ? "animate" : "css"]({ bottom: (valPercent) + "%" }, o.animate) } if (i === 1) { self.range[animate ? "animate" : "css"]({ height: (valPercent - lastValPercent) + "%" }, { queue: false, duration: o.animate }) } } } lastValPercent = valPercent }) } else { value = this.value(); valueMin = this._valueMin(); valueMax = this._valueMax(); valPercent = (valueMax !== valueMin) ? (value - valueMin) / (valueMax - valueMin) * 100 : 0; _set[self.orientation === "horizontal" ? "left" : "bottom"] = valPercent + "%"; this.handle.stop(1, 1)[animate ? "animate" : "css"](_set, o.animate); if (oRange === "min" && this.orientation === "horizontal") { this.range.stop(1, 1)[animate ? "animate" : "css"]({ width: valPercent + "%" }, o.animate) } if (oRange === "max" && this.orientation === "horizontal") { this.range[animate ? "animate" : "css"]({ width: (100 - valPercent) + "%" }, { queue: false, duration: o.animate }) } if (oRange === "min" && this.orientation === "vertical") { this.range.stop(1, 1)[animate ? "animate" : "css"]({ height: valPercent + "%" }, o.animate) } if (oRange === "max" && this.orientation === "vertical") { this.range[animate ? "animate" : "css"]({ height: (100 - valPercent) + "%" }, { queue: false, duration: o.animate }) } } } }); $.extend($.ui.slider, { version: "1.8.22" }) }(jQuery)); (function ($, undefined) { var tabId = 0, listId = 0; function getNextTabId() { return ++tabId } function getNextListId() { return ++listId } $.widget("ui.tabs", { options: { add: null, ajaxOptions: null, cache: false, cookie: null, collapsible: false, disable: null, disabled: [], enable: null, event: "click", fx: null, idPrefix: "ui-tabs-", load: null, panelTemplate: "<div></div>", remove: null, select: null, show: null, spinner: "<em>Loading&#8230;</em>", tabTemplate: "<li><a href='#{href}'><span>#{label}</span></a></li>" }, _create: function () { this._tabify(true) }, _setOption: function (key, value) { if (key == "selected") { if (this.options.collapsible && value == this.options.selected) { return } this.select(value) } else { this.options[key] = value; this._tabify() } }, _tabId: function (a) { return a.title && a.title.replace(/\s/g, "_").replace(/[^\w\u00c0-\uFFFF-]/g, "") || this.options.idPrefix + getNextTabId() }, _sanitizeSelector: function (hash) { return hash.replace(/:/g, "\\:") }, _cookie: function () { var cookie = this.cookie || (this.cookie = this.options.cookie.name || "ui-tabs-" + getNextListId()); return $.cookie.apply(null, [cookie].concat($.makeArray(arguments))) }, _ui: function (tab, panel) { return { tab: tab, panel: panel, index: this.anchors.index(tab) } }, _cleanup: function () { this.lis.filter(".ui-state-processing").removeClass("ui-state-processing").find("span:data(label.tabs)").each(function () { var el = $(this); el.html(el.data("label.tabs")).removeData("label.tabs") }) }, _tabify: function (init) { var self = this, o = this.options, fragmentId = /^#.+/; this.list = this.element.find("ol,ul").eq(0); this.lis = $(" > li:has(a[href])", this.list); this.anchors = this.lis.map(function () { return $("a", this)[0] }); this.panels = $([]); this.anchors.each(function (i, a) { var href = $(a).attr("href"); var hrefBase = href.split("#")[0], baseEl; if (hrefBase && (hrefBase === location.toString().split("#")[0] || (baseEl = $("base")[0]) && hrefBase === baseEl.href)) { href = a.hash; a.href = href } if (fragmentId.test(href)) { self.panels = self.panels.add(self.element.find(self._sanitizeSelector(href))) } else if (href && href !== "#") { $.data(a, "href.tabs", href); $.data(a, "load.tabs", href.replace(/#.*$/, "")); var id = self._tabId(a); a.href = "#" + id; var $panel = self.element.find("#" + id); if (!$panel.length) { $panel = $(o.panelTemplate).attr("id", id).addClass("ui-tabs-panel ui-widget-content ui-corner-bottom").insertAfter(self.panels[i - 1] || self.list); $panel.data("destroy.tabs", true) } self.panels = self.panels.add($panel) } else { o.disabled.push(i) } }); if (init) { this.element.addClass("ui-tabs ui-widget ui-widget-content ui-corner-all"); this.list.addClass("ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all"); this.lis.addClass("ui-state-default ui-corner-top"); this.panels.addClass("ui-tabs-panel ui-widget-content ui-corner-bottom"); if (o.selected === undefined) { if (location.hash) { this.anchors.each(function (i, a) { if (a.hash == location.hash) { o.selected = i; return false } }) } if (typeof o.selected !== "number" && o.cookie) { o.selected = parseInt(self._cookie(), 10) } if (typeof o.selected !== "number" && this.lis.filter(".ui-tabs-selected").length) { o.selected = this.lis.index(this.lis.filter(".ui-tabs-selected")) } o.selected = o.selected || (this.lis.length ? 0 : -1) } else if (o.selected === null) { o.selected = -1 } o.selected = ((o.selected >= 0 && this.anchors[o.selected]) || o.selected < 0) ? o.selected : 0; o.disabled = $.unique(o.disabled.concat($.map(this.lis.filter(".ui-state-disabled"), function (n, i) { return self.lis.index(n) }))).sort(); if ($.inArray(o.selected, o.disabled) != -1) { o.disabled.splice($.inArray(o.selected, o.disabled), 1) } this.panels.addClass("ui-tabs-hide"); this.lis.removeClass("ui-tabs-selected ui-state-active"); if (o.selected >= 0 && this.anchors.length) { self.element.find(self._sanitizeSelector(self.anchors[o.selected].hash)).removeClass("ui-tabs-hide"); this.lis.eq(o.selected).addClass("ui-tabs-selected ui-state-active"); self.element.queue("tabs", function () { self._trigger("show", null, self._ui(self.anchors[o.selected], self.element.find(self._sanitizeSelector(self.anchors[o.selected].hash))[0])) }); this.load(o.selected) } $(window).bind("unload", function () { self.lis.add(self.anchors).unbind(".tabs"); self.lis = self.anchors = self.panels = null }) } else { o.selected = this.lis.index(this.lis.filter(".ui-tabs-selected")) } this.element[o.collapsible ? "addClass" : "removeClass"]("ui-tabs-collapsible"); if (o.cookie) { this._cookie(o.selected, o.cookie) } for (var i = 0, li; (li = this.lis[i]) ; i++) { $(li)[$.inArray(i, o.disabled) != -1 && !$(li).hasClass("ui-tabs-selected") ? "addClass" : "removeClass"]("ui-state-disabled") } if (o.cache === false) { this.anchors.removeData("cache.tabs") } this.lis.add(this.anchors).unbind(".tabs"); if (o.event !== "mouseover") { var addState = function (state, el) { if (el.is(":not(.ui-state-disabled)")) { el.addClass("ui-state-" + state) } }; var removeState = function (state, el) { el.removeClass("ui-state-" + state) }; this.lis.bind("mouseover.tabs", function () { addState("hover", $(this)) }); this.lis.bind("mouseout.tabs", function () { removeState("hover", $(this)) }); this.anchors.bind("focus.tabs", function () { addState("focus", $(this).closest("li")) }); this.anchors.bind("blur.tabs", function () { removeState("focus", $(this).closest("li")) }) } var hideFx, showFx; if (o.fx) { if ($.isArray(o.fx)) { hideFx = o.fx[0]; showFx = o.fx[1] } else { hideFx = showFx = o.fx } } function resetStyle($el, fx) { $el.css("display", ""); if (!$.support.opacity && fx.opacity) { $el[0].style.removeAttribute("filter") } } var showTab = showFx ? function (clicked, $show) { $(clicked).closest("li").addClass("ui-tabs-selected ui-state-active"); $show.hide().removeClass("ui-tabs-hide").animate(showFx, showFx.duration || "normal", function () { resetStyle($show, showFx); self._trigger("show", null, self._ui(clicked, $show[0])) }) } : function (clicked, $show) { $(clicked).closest("li").addClass("ui-tabs-selected ui-state-active"); $show.removeClass("ui-tabs-hide"); self._trigger("show", null, self._ui(clicked, $show[0])) }; var hideTab = hideFx ? function (clicked, $hide) { $hide.animate(hideFx, hideFx.duration || "normal", function () { self.lis.removeClass("ui-tabs-selected ui-state-active"); $hide.addClass("ui-tabs-hide"); resetStyle($hide, hideFx); self.element.dequeue("tabs") }) } : function (clicked, $hide, $show) { self.lis.removeClass("ui-tabs-selected ui-state-active"); $hide.addClass("ui-tabs-hide"); self.element.dequeue("tabs") }; this.anchors.bind(o.event + ".tabs", function () { var el = this, $li = $(el).closest("li"), $hide = self.panels.filter(":not(.ui-tabs-hide)"), $show = self.element.find(self._sanitizeSelector(el.hash)); if (($li.hasClass("ui-tabs-selected") && !o.collapsible) || $li.hasClass("ui-state-disabled") || $li.hasClass("ui-state-processing") || self.panels.filter(":animated").length || self._trigger("select", null, self._ui(this, $show[0])) === false) { this.blur(); return false } o.selected = self.anchors.index(this); self.abort(); if (o.collapsible) { if ($li.hasClass("ui-tabs-selected")) { o.selected = -1; if (o.cookie) { self._cookie(o.selected, o.cookie) } self.element.queue("tabs", function () { hideTab(el, $hide) }).dequeue("tabs"); this.blur(); return false } else if (!$hide.length) { if (o.cookie) { self._cookie(o.selected, o.cookie) } self.element.queue("tabs", function () { showTab(el, $show) }); self.load(self.anchors.index(this)); this.blur(); return false } } if (o.cookie) { self._cookie(o.selected, o.cookie) } if ($show.length) { if ($hide.length) { self.element.queue("tabs", function () { hideTab(el, $hide) }) } self.element.queue("tabs", function () { showTab(el, $show) }); self.load(self.anchors.index(this)) } else { throw "jQuery UI Tabs: Mismatching fragment identifier." } if ($.browser.msie) { this.blur() } }); this.anchors.bind("click.tabs", function () { return false }) }, _getIndex: function (index) { if (typeof index == "string") { index = this.anchors.index(this.anchors.filter("[href$='" + index + "']")) } return index }, destroy: function () { var o = this.options; this.abort(); this.element.unbind(".tabs").removeClass("ui-tabs ui-widget ui-widget-content ui-corner-all ui-tabs-collapsible").removeData("tabs"); this.list.removeClass("ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all"); this.anchors.each(function () { var href = $.data(this, "href.tabs"); if (href) { this.href = href } var $this = $(this).unbind(".tabs"); $.each(["href", "load", "cache"], function (i, prefix) { $this.removeData(prefix + ".tabs") }) }); this.lis.unbind(".tabs").add(this.panels).each(function () { if ($.data(this, "destroy.tabs")) { $(this).remove() } else { $(this).removeClass(["ui-state-default", "ui-corner-top", "ui-tabs-selected", "ui-state-active", "ui-state-hover", "ui-state-focus", "ui-state-disabled", "ui-tabs-panel", "ui-widget-content", "ui-corner-bottom", "ui-tabs-hide"].join(" ")) } }); if (o.cookie) { this._cookie(null, o.cookie) } return this }, add: function (url, label, index) { if (index === undefined) { index = this.anchors.length } var self = this, o = this.options, $li = $(o.tabTemplate.replace(/#\{href\}/g, url).replace(/#\{label\}/g, label)), id = !url.indexOf("#") ? url.replace("#", "") : this._tabId($("a", $li)[0]); $li.addClass("ui-state-default ui-corner-top").data("destroy.tabs", true); var $panel = self.element.find("#" + id); if (!$panel.length) { $panel = $(o.panelTemplate).attr("id", id).data("destroy.tabs", true) } $panel.addClass("ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"); if (index >= this.lis.length) { $li.appendTo(this.list); $panel.appendTo(this.list[0].parentNode) } else { $li.insertBefore(this.lis[index]); $panel.insertBefore(this.panels[index]) } o.disabled = $.map(o.disabled, function (n, i) { return n >= index ? ++n : n }); this._tabify(); if (this.anchors.length == 1) { o.selected = 0; $li.addClass("ui-tabs-selected ui-state-active"); $panel.removeClass("ui-tabs-hide"); this.element.queue("tabs", function () { self._trigger("show", null, self._ui(self.anchors[0], self.panels[0])) }); this.load(0) } this._trigger("add", null, this._ui(this.anchors[index], this.panels[index])); return this }, remove: function (index) { index = this._getIndex(index); var o = this.options, $li = this.lis.eq(index).remove(), $panel = this.panels.eq(index).remove(); if ($li.hasClass("ui-tabs-selected") && this.anchors.length > 1) { this.select(index + (index + 1 < this.anchors.length ? 1 : -1)) } o.disabled = $.map($.grep(o.disabled, function (n, i) { return n != index }), function (n, i) { return n >= index ? --n : n }); this._tabify(); this._trigger("remove", null, this._ui($li.find("a")[0], $panel[0])); return this }, enable: function (index) { index = this._getIndex(index); var o = this.options; if ($.inArray(index, o.disabled) == -1) { return } this.lis.eq(index).removeClass("ui-state-disabled"); o.disabled = $.grep(o.disabled, function (n, i) { return n != index }); this._trigger("enable", null, this._ui(this.anchors[index], this.panels[index])); return this }, disable: function (index) { index = this._getIndex(index); var self = this, o = this.options; if (index != o.selected) { this.lis.eq(index).addClass("ui-state-disabled"); o.disabled.push(index); o.disabled.sort(); this._trigger("disable", null, this._ui(this.anchors[index], this.panels[index])) } return this }, select: function (index) { index = this._getIndex(index); if (index == -1) { if (this.options.collapsible && this.options.selected != -1) { index = this.options.selected } else { return this } } this.anchors.eq(index).trigger(this.options.event + ".tabs"); return this }, load: function (index) { index = this._getIndex(index); var self = this, o = this.options, a = this.anchors.eq(index)[0], url = $.data(a, "load.tabs"); this.abort(); if (!url || this.element.queue("tabs").length !== 0 && $.data(a, "cache.tabs")) { this.element.dequeue("tabs"); return } this.lis.eq(index).addClass("ui-state-processing"); if (o.spinner) { var span = $("span", a); span.data("label.tabs", span.html()).html(o.spinner) } this.xhr = $.ajax($.extend({}, o.ajaxOptions, { url: url, success: function (r, s) { self.element.find(self._sanitizeSelector(a.hash)).html(r); self._cleanup(); if (o.cache) { $.data(a, "cache.tabs", true) } self._trigger("load", null, self._ui(self.anchors[index], self.panels[index])); try { o.ajaxOptions.success(r, s) } catch (e) { } }, error: function (xhr, s, e) { self._cleanup(); self._trigger("load", null, self._ui(self.anchors[index], self.panels[index])); try { o.ajaxOptions.error(xhr, s, index, a) } catch (e) { } } })); self.element.dequeue("tabs"); return this }, abort: function () { this.element.queue([]); this.panels.stop(false, true); this.element.queue("tabs", this.element.queue("tabs").splice(-2, 2)); if (this.xhr) { this.xhr.abort(); delete this.xhr } this._cleanup(); return this }, url: function (index, url) { this.anchors.eq(index).removeData("cache.tabs").data("load.tabs", url); return this }, length: function () { return this.anchors.length } }); $.extend($.ui.tabs, { version: "1.8.22" }); $.extend($.ui.tabs.prototype, { rotation: null, rotate: function (ms, continuing) { var self = this, o = this.options; var rotate = self._rotate || (self._rotate = function (e) { clearTimeout(self.rotation); self.rotation = setTimeout(function () { var t = o.selected; self.select(++t < self.anchors.length ? t : 0) }, ms); if (e) { e.stopPropagation() } }); var stop = self._unrotate || (self._unrotate = !continuing ? function (e) { if (e.clientX) { self.rotate(null) } } : function (e) { rotate() }); if (ms) { this.element.bind("tabsshow", rotate); this.anchors.bind(o.event + ".tabs", stop); rotate() } else { clearTimeout(self.rotation); this.element.unbind("tabsshow", rotate); this.anchors.unbind(o.event + ".tabs", stop); delete this._rotate; delete this._unrotate } return this } }) })(jQuery);
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

// Limit scope pollution from any deprecated API
(function () {

    var matched, browser;

    // Use of jQuery.browser is frowned upon.
    // More details: http://api.jquery.com/jQuery.browser
    // jQuery.uaMatch maintained for back-compat
    jQuery.uaMatch = function (ua) {
        ua = ua.toLowerCase();

        var match = /(chrome)[ \/]([\w.]+)/.exec(ua) ||
            /(webkit)[ \/]([\w.]+)/.exec(ua) ||
            /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(ua) ||
            /(msie) ([\w.]+)/.exec(ua) ||
            ua.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(ua) ||
            [];

        return {
            browser: match[1] || "",
            version: match[2] || "0"
        };
    };

    matched = jQuery.uaMatch(navigator.userAgent);
    browser = {};

    if (matched.browser) {
        browser[matched.browser] = true;
        browser.version = matched.version;
    }

    // Chrome is Webkit, but Webkit is also Safari.
    if (browser.chrome) {
        browser.webkit = true;
    } else if (browser.webkit) {
        browser.safari = true;
    }

    jQuery.browser = browser;

    jQuery.sub = function () {
        function jQuerySub(selector, context) {
            return new jQuerySub.fn.init(selector, context);
        }
        jQuery.extend(true, jQuerySub, this);
        jQuerySub.superclass = this;
        jQuerySub.fn = jQuerySub.prototype = this();
        jQuerySub.fn.constructor = jQuerySub;
        jQuerySub.sub = this.sub;
        jQuerySub.fn.init = function init(selector, context) {
            if (context && context instanceof jQuery && !(context instanceof jQuerySub)) {
                context = jQuerySub(context);
            }

            return jQuery.fn.init.call(this, selector, context, rootjQuerySub);
        };
        jQuerySub.fn.init.prototype = jQuerySub.fn;
        var rootjQuerySub = jQuerySub(document);
        return jQuerySub;
    };

})();
/**
 * @preserve
 * jquery.layout 1.4.4
 * $Date: 2014-11-29 08:00:00 (Sat, 29 November 2014) $
 * $Rev: 1.0404 $
 *
 * Copyright (c) 2014 Kevin Dalman (http://jquery-dev.com)
 * Based on work by Fabrizio Balliano (http://www.fabrizioballiano.net)
 *
 * Dual licensed under the GPL (http://www.gnu.org/licenses/gpl.html)
 * and MIT (http://www.opensource.org/licenses/mit-license.php) licenses.
 *
 * SEE: http://layout.jquery-dev.com/LICENSE.txt
 *
 * Changelog: http://layout.jquery-dev.com/changelog.cfm
 *
 * Docs: http://layout.jquery-dev.com/documentation.html
 * Tips: http://layout.jquery-dev.com/tips.html
 * Help: http://groups.google.com/group/jquery-ui-layout
 */

/* JavaDoc Info: http://code.google.com/closure/compiler/docs/js-for-compiler.html
 * {!Object}	non-nullable type (never NULL)
 * {?string}	nullable type (sometimes NULL) - default for {Object}
 * {number=}	optional parameter
 * {*}			ALL types
 */
/*	TODO for jQ 2.x 
 *	check $.fn.disableSelection - this is in jQuery UI 1.9.x
 */

// NOTE: For best readability, view with a fixed-width font and tabs equal to 4-chars

; (function ($) {

    // alias Math methods - used a lot!
    var min = Math.min
    , max = Math.max
    , round = Math.floor

    , isStr = function (v) { return $.type(v) === "string"; }

        /**
         * @param {!Object}			Instance
         * @param {Array.<string>}	a_fn
         */
    , runPluginCallbacks = function (Instance, a_fn) {
        if ($.isArray(a_fn))
            for (var i = 0, c = a_fn.length; i < c; i++) {
                var fn = a_fn[i];
                try {
                    if (isStr(fn)) // 'name' of a function
                        fn = eval(fn);
                    if ($.isFunction(fn))
                        g(fn)(Instance);
                } catch (ex) { }
            }
        function g(f) { return f; }; // compiler hack
    }
    ;

    /*
     *	GENERIC $.layout METHODS - used by all layouts
     */
    $.layout = {

        version: "1.4.4"
    , revision: 1.0404 // eg: ver 1.4.4 = rev 1.0404 - major(n+).minor(nn)+patch(nn+)

        // $.layout.browser REPLACES $.browser
    , browser: {} // set below

        // *PREDEFINED* EFFECTS & DEFAULTS 
        // MUST list effect here - OR MUST set an fxSettings option (can be an empty hash: {})
    , effects: {

        //	Pane Open/Close Animations
        slide: {
            all: { duration: "fast" } // eg: duration: 1000, easing: "easeOutBounce"
		, north: { direction: "up" }
		, south: { direction: "down" }
		, east: { direction: "right" }
		, west: { direction: "left" }
        }
        , drop: {
            all: { duration: "slow" }
            , north: { direction: "up" }
            , south: { direction: "down" }
            , east: { direction: "right" }
            , west: { direction: "left" }
        }
        , scale: {
            all: { duration: "fast" }
        }
        //	these are not recommended, but can be used
        , blind: {}
        , clip: {}
        , explode: {}
        , fade: {}
        , fold: {}
        , puff: {}

        //	Pane Resize Animations
        , size: {
            all: { easing: "swing" }
        }
    }

        // INTERNAL CONFIG DATA - DO NOT CHANGE THIS!
    , config: {
        optionRootKeys: "effects,panes,north,south,west,east,center".split(",")
        , allPanes: "north,south,west,east,center".split(",")
        , borderPanes: "north,south,west,east".split(",")
        , oppositeEdge: {
            north: "south"
            , south: "north"
            , east: "west"
            , west: "east"
        }
        //	offscreen data
        , offscreenCSS: { left: "-99999px", right: "auto" } // used by hide/close if useOffscreenClose=true
        , offscreenReset: "offscreenReset" // key used for data
        //	CSS used in multiple places
        , hidden: { visibility: "hidden" }
        , visible: { visibility: "visible" }
        //	layout element settings
        , resizers: {
            cssReq: {
                position: "absolute"
			, padding: 0
			, margin: 0
			, fontSize: "1px"
			, textAlign: "left"	// to counter-act "center" alignment!
			, overflow: "hidden" // prevent toggler-button from overflowing
                //	SEE $.layout.defaults.zIndexes.resizer_normal
            }
            , cssDemo: { // DEMO CSS - applied if: options.PANE.applyDemoStyles=true
                background: "#DDD"
                , border: "none"
            }
        }
        , togglers: {
            cssReq: {
                position: "absolute"
			, display: "block"
			, padding: 0
			, margin: 0
			, overflow: "hidden"
			, textAlign: "center"
			, fontSize: "1px"
			, cursor: "pointer"
			, zIndex: 1
            }
            , cssDemo: { // DEMO CSS - applied if: options.PANE.applyDemoStyles=true
                background: "#AAA"
            }
        }
        , content: {
            cssReq: {
                position: "relative" /* contain floated or positioned elements */
            }
            , cssDemo: { // DEMO CSS - applied if: options.PANE.applyDemoStyles=true
                overflow: "auto"
                , padding: "10px"
            }
            , cssDemoPane: { // DEMO CSS - REMOVE scrolling from 'pane' when it has a content-div
                overflow: "hidden"
                , padding: 0
            }
        }
        , panes: { // defaults for ALL panes - overridden by 'per-pane settings' below
            cssReq: {
                position: "absolute"
			, margin: 0
                //	$.layout.defaults.zIndexes.pane_normal
            }
            , cssDemo: { // DEMO CSS - applied if: options.PANE.applyDemoStyles=true
                padding: "10px"
                , background: "#FFF"
                , border: "1px solid #BBB"
                , overflow: "auto"
            }
        }
        , north: {
            side: "top"
            , sizeType: "Height"
            , dir: "horz"
            , cssReq: {
                top: 0
                , bottom: "auto"
                , left: 0
                , right: 0
                , width: "auto"
                //	height: 	DYNAMIC
            }
        }
        , south: {
            side: "bottom"
            , sizeType: "Height"
            , dir: "horz"
            , cssReq: {
                top: "auto"
                , bottom: 0
                , left: 0
                , right: 0
                , width: "auto"
                //	height: 	DYNAMIC
            }
        }
        , east: {
            side: "right"
            , sizeType: "Width"
            , dir: "vert"
            , cssReq: {
                left: "auto"
                , right: 0
                , top: "auto" // DYNAMIC
                , bottom: "auto" // DYNAMIC
                , height: "auto"
                //	width: 		DYNAMIC
            }
        }
        , west: {
            side: "left"
            , sizeType: "Width"
            , dir: "vert"
            , cssReq: {
                left: 0
                , right: "auto"
                , top: "auto" // DYNAMIC
                , bottom: "auto" // DYNAMIC
                , height: "auto"
                //	width: 		DYNAMIC
            }
        }
        , center: {
            dir: "center"
            , cssReq: {
                left: "auto" // DYNAMIC
                , right: "auto" // DYNAMIC
                , top: "auto" // DYNAMIC
                , bottom: "auto" // DYNAMIC
                , height: "auto"
                , width: "auto"
            }
        }
    }

        // CALLBACK FUNCTION NAMESPACE - used to store reusable callback functions
    , callbacks: {}

    , getParentPaneElem: function (el) {
        // must pass either a container or pane element
        var $el = $(el)
		, layout = $el.data("layout") || $el.data("parentLayout");
        if (layout) {
            var $cont = layout.container;
            // see if this container is directly-nested inside an outer-pane
            if ($cont.data("layoutPane")) return $cont;
            var $pane = $cont.closest("." + $.layout.defaults.panes.paneClass);
            // if a pane was found, return it
            if ($pane.data("layoutPane")) return $pane;
        }
        return null;
    }

    , getParentPaneInstance: function (el) {
        // must pass either a container or pane element
        var $pane = $.layout.getParentPaneElem(el);
        return $pane ? $pane.data("layoutPane") : null;
    }

    , getParentLayoutInstance: function (el) {
        // must pass either a container or pane element
        var $pane = $.layout.getParentPaneElem(el);
        return $pane ? $pane.data("parentLayout") : null;
    }

    , getEventObject: function (evt) {
        return typeof evt === "object" && evt.stopPropagation ? evt : null;
    }
    , parsePaneName: function (evt_or_pane) {
        var evt = $.layout.getEventObject(evt_or_pane)
		, pane = evt_or_pane;
        if (evt) {
            // ALWAYS stop propagation of events triggered in Layout!
            evt.stopPropagation();
            pane = $(this).data("layoutEdge");
        }
        if (pane && !/^(west|east|north|south|center)$/.test(pane)) {
            $.layout.msg('LAYOUT ERROR - Invalid pane-name: "' + pane + '"');
            pane = "error";
        }
        return pane;
    }


        // LAYOUT-PLUGIN REGISTRATION
        // more plugins can added beyond this default list
    , plugins: {
        draggable: !!$.fn.draggable // resizing
        , effects: {
            core: !!$.effects		// animimations (specific effects tested by initOptions)
            , slide: $.effects && ($.effects.slide || ($.effects.effect && $.effects.effect.slide)) // default effect
        }
    }

        //	arrays of plugin or other methods to be triggered for events in *each layout* - will be passed 'Instance'
    , onCreate: []	// runs when layout is just starting to be created - right after options are set
    , onLoad: []	// runs after layout container and global events init, but before initPanes is called
    , onReady: []	// runs after initialization *completes* - ie, after initPanes completes successfully
    , onDestroy: []	// runs after layout is destroyed
    , onUnload: []	// runs after layout is destroyed OR when page unloads
    , afterOpen: []	// runs after setAsOpen() completes
    , afterClose: []	// runs after setAsClosed() completes

        /*
         *	GENERIC UTILITY METHODS
         */

        // calculate and return the scrollbar width, as an integer
    , scrollbarWidth: function () { return window.scrollbarWidth || $.layout.getScrollbarSize('width'); }
    , scrollbarHeight: function () { return window.scrollbarHeight || $.layout.getScrollbarSize('height'); }
    , getScrollbarSize: function (dim) {
        var $c = $('<div style="position: absolute; top: -10000px; left: -10000px; width: 100px; height: 100px; border: 0; overflow: scroll;"></div>').appendTo("body")
		, d = { width: $c.outerWidth - $c[0].clientWidth, height: 100 - $c[0].clientHeight };
        $c.remove();
        window.scrollbarWidth = d.width;
        window.scrollbarHeight = d.height;
        return dim.match(/^(width|height)$/) ? d[dim] : d;
    }


    , disableTextSelection: function () {
        var $d = $(document)
		, s = 'textSelectionDisabled'
		, x = 'textSelectionInitialized'
        ;
        if ($.fn.disableSelection) {
            if (!$d.data(x)) // document hasn't been initialized yet
                $d.on('mouseup', $.layout.enableTextSelection).data(x, true);
            if (!$d.data(s))
                $d.disableSelection().data(s, true);
        }
    }
    , enableTextSelection: function () {
        var $d = $(document)
		, s = 'textSelectionDisabled';
        if ($.fn.enableSelection && $d.data(s))
            $d.enableSelection().data(s, false);
    }


        /**
         * Returns hash container 'display' and 'visibility'
         *
         * @see	$.swap() - swaps CSS, runs callback, resets CSS
         * @param  {!Object}		$E				jQuery element
         * @param  {boolean=}	[force=false]	Run even if display != none
         * @return {!Object}						Returns current style props, if applicable
         */
    , showInvisibly: function ($E, force) {
        if ($E && $E.length && (force || $E.css("display") === "none")) { // only if not *already hidden*
            var s = $E[0].style
				// save ONLY the 'style' props because that is what we must restore
			, CSS = { display: s.display || '', visibility: s.visibility || '' };
            // show element 'invisibly' so can be measured
            $E.css({ display: "block", visibility: "hidden" });
            return CSS;
        }
        return {};
    }

        /**
         * Returns data for setting size of an element (container or a pane).
         *
         * @see  _create(), onWindowResize() for container, plus others for pane
         * @return JSON  Returns a hash of all dimensions: top, bottom, left, right, outerWidth, innerHeight, etc
         */
    , getElementDimensions: function ($E, inset) {
        var
		//	dimensions hash - start with current data IF passed
			d = { css: {}, inset: {} }
		, x = d.css			// CSS hash
		, i = { bottom: 0 }	// TEMP insets (bottom = complier hack)
		, N = $.layout.cssNum
		, R = Math.round
		, off = $E.offset()
		, b, p, ei			// TEMP border, padding
        ;
        d.offsetLeft = off.left;
        d.offsetTop = off.top;

        if (!inset) inset = {}; // simplify logic below

        $.each("Left,Right,Top,Bottom".split(","), function (idx, e) { // e = edge
            b = x["border" + e] = $.layout.borderWidth($E, e);
            p = x["padding" + e] = $.layout.cssNum($E, "padding" + e);
            ei = e.toLowerCase();
            d.inset[ei] = inset[ei] >= 0 ? inset[ei] : p; // any missing insetX value = paddingX
            i[ei] = d.inset[ei] + b; // total offset of content from outer side
        });

        x.width = R($E.width());
        x.height = R($E.height());
        x.top = N($E, "top", true);
        x.bottom = N($E, "bottom", true);
        x.left = N($E, "left", true);
        x.right = N($E, "right", true);

        d.outerWidth = R($E.outerWidth());
        d.outerHeight = R($E.outerHeight());
        // calc the TRUE inner-dimensions, even in quirks-mode!
        d.innerWidth = max(0, d.outerWidth - i.left - i.right);
        d.innerHeight = max(0, d.outerHeight - i.top - i.bottom);
        // layoutWidth/Height is used in calcs for manual resizing
        // layoutW/H only differs from innerW/H when in quirks-mode - then is like outerW/H
        d.layoutWidth = R($E.innerWidth());
        d.layoutHeight = R($E.innerHeight());

        //if ($E.prop('tagName') === 'BODY') { debugData( d, $E.prop('tagName') ); } // DEBUG

        //d.visible	= $E.is(":visible");// && x.width > 0 && x.height > 0;

        return d;
    }

    , getElementStyles: function ($E, list) {
        var
			CSS = {}
		, style = $E[0].style
		, props = list.split(",")
		, sides = "Top,Bottom,Left,Right".split(",")
		, attrs = "Color,Style,Width".split(",")
		, p, s, a, i, j, k
        ;
        for (i = 0; i < props.length; i++) {
            p = props[i];
            if (p.match(/(border|padding|margin)$/))
                for (j = 0; j < 4; j++) {
                    s = sides[j];
                    if (p === "border")
                        for (k = 0; k < 3; k++) {
                            a = attrs[k];
                            CSS[p + s + a] = style[p + s + a];
                        }
                    else
                        CSS[p + s] = style[p + s];
                }
            else
                CSS[p] = style[p];
        };
        return CSS
    }

        /**
         * Return the innerWidth for the current browser/doctype
         *
         * @see  initPanes(), sizeMidPanes(), initHandles(), sizeHandles()
         * @param  {Array.<Object>}	$E  Must pass a jQuery object - first element is processed
         * @param  {number=}			outerWidth (optional) Can pass a width, allowing calculations BEFORE element is resized
         * @return {number}			Returns the innerWidth of the elem by subtracting padding and borders
         */
    , cssWidth: function ($E, outerWidth) {
        // a 'calculated' outerHeight can be passed so borders and/or padding are removed if needed
        if (outerWidth <= 0) return 0;

        var lb = $.layout.browser
		, bs = !lb.boxModel ? "border-box" : lb.boxSizing ? $E.css("boxSizing") : "content-box"
		, b = $.layout.borderWidth
		, n = $.layout.cssNum
		, W = outerWidth
        ;
        // strip border and/or padding from outerWidth to get CSS Width
        if (bs !== "border-box")
            W -= (b($E, "Left") + b($E, "Right"));
        if (bs === "content-box")
            W -= (n($E, "paddingLeft") + n($E, "paddingRight"));
        return max(0, W);
    }

        /**
         * Return the innerHeight for the current browser/doctype
         *
         * @see  initPanes(), sizeMidPanes(), initHandles(), sizeHandles()
         * @param  {Array.<Object>}	$E  Must pass a jQuery object - first element is processed
         * @param  {number=}			outerHeight  (optional) Can pass a width, allowing calculations BEFORE element is resized
         * @return {number}			Returns the innerHeight of the elem by subtracting padding and borders
         */
    , cssHeight: function ($E, outerHeight) {
        // a 'calculated' outerHeight can be passed so borders and/or padding are removed if needed
        if (outerHeight <= 0) return 0;

        var lb = $.layout.browser
		, bs = !lb.boxModel ? "border-box" : lb.boxSizing ? $E.css("boxSizing") : "content-box"
		, b = $.layout.borderWidth
		, n = $.layout.cssNum
		, H = outerHeight
        ;
        // strip border and/or padding from outerHeight to get CSS Height
        if (bs !== "border-box")
            H -= (b($E, "Top") + b($E, "Bottom"));
        if (bs === "content-box")
            H -= (n($E, "paddingTop") + n($E, "paddingBottom"));
        return max(0, H);
    }

        /**
         * Returns the 'current CSS numeric value' for a CSS property - 0 if property does not exist
         *
         * @see  Called by many methods
         * @param {Array.<Object>}	$E					Must pass a jQuery object - first element is processed
         * @param {string}			prop				The name of the CSS property, eg: top, width, etc.
         * @param {boolean=}			[allowAuto=false]	true = return 'auto' if that is value; false = return 0
         * @return {(string|number)}						Usually used to get an integer value for position (top, left) or size (height, width)
         */
    , cssNum: function ($E, prop, allowAuto) {
        if (!$E.jquery) $E = $($E);
        var CSS = $.layout.showInvisibly($E)
		, p = $.css($E[0], prop, true)
		, v = allowAuto && p == "auto" ? p : Math.round(parseFloat(p) || 0);
        $E.css(CSS); // RESET
        return v;
    }

    , borderWidth: function (el, side) {
        if (el.jquery) el = el[0];
        var b = "border" + side.substr(0, 1).toUpperCase() + side.substr(1); // left => Left
        return $.css(el, b + "Style", true) === "none" ? 0 : Math.round(parseFloat($.css(el, b + "Width", true)) || 0);
    }

        /**
         * Mouse-tracking utility - FUTURE REFERENCE
         *
         * init: if (!window.mouse) {
         *			window.mouse = { x: 0, y: 0 };
         *			$(document).mousemove( $.layout.trackMouse );
         *		}
         *
         * @param {Object}		evt
         *
    ,	trackMouse: function (evt) {
            window.mouse = { x: evt.clientX, y: evt.clientY };
        }
        */

        /**
         * SUBROUTINE for preventPrematureSlideClose option
         *
         * @param {Object}		evt
         * @param {Object=}		el
         */
    , isMouseOverElem: function (evt, el) {
        var
			$E = $(el || this)
		, d = $E.offset()
		, T = d.top
		, L = d.left
		, R = L + $E.outerWidth()
		, B = T + $E.outerHeight()
		, x = evt.pageX	// evt.clientX ?
		, y = evt.pageY	// evt.clientY ?
        ;
        // if X & Y are < 0, probably means is over an open SELECT
        return ($.layout.browser.msie && x < 0 && y < 0) || ((x >= L && x <= R) && (y >= T && y <= B));
    }

        /**
         * Message/Logging Utility
         *
         * @example $.layout.msg("My message");				// log text
         * @example $.layout.msg("My message", true);		// alert text
         * @example $.layout.msg({ foo: "bar" }, "Title");	// log hash-data, with custom title
         * @example $.layout.msg({ foo: "bar" }, true, "Title", { sort: false }); -OR-
         * @example $.layout.msg({ foo: "bar" }, "Title", { sort: false, display: true }); // alert hash-data
         *
         * @param {(Object|string)}			info			String message OR Hash/Array
         * @param {(Boolean|string|Object)=}	[popup=false]	True means alert-box - can be skipped
         * @param {(Object|string)=}			[debugTitle=""]	Title for Hash data - can be skipped
         * @param {Object=}					[debugOpts]		Extra options for debug output
         */
    , msg: function (info, popup, debugTitle, debugOpts) {
        if ($.isPlainObject(info) && window.debugData) {
            if (typeof popup === "string") {
                debugOpts = debugTitle;
                debugTitle = popup;
            }
            else if (typeof debugTitle === "object") {
                debugOpts = debugTitle;
                debugTitle = null;
            }
            var t = debugTitle || "log( <object> )"
			, o = $.extend({ sort: false, returnHTML: false, display: false }, debugOpts);
            if (popup === true || o.display)
                debugData(info, t, o);
            else if (window.console)
                console.log(debugData(info, t, o));
        }
        else if (window.console)
            console.log(info);
        else if (popup)
            alert(info);
        else {
            var id = "#layoutLogger"
			, $l = $(id);
            if (!$l.length)
                $l = createLog();
            $l.children("ul").append('<li style="padding: 4px 10px; margin: 0; border-top: 1px solid #CCC;">' + info.replace(/\</g, "&lt;").replace(/\>/g, "&gt;") + '</li>');
        }

        function createLog() {
            var pos = $.support.fixedPosition ? 'fixed' : 'absolute'
			, $e = $('<div id="layoutLogger" style="position: ' + pos + '; top: 5px; z-index: 999999; max-width: 25%; overflow: hidden; border: 1px solid #000; border-radius: 5px; background: #FBFBFB; box-shadow: 0 2px 10px rgba(0,0,0,0.3);">'
				+ '<div style="font-size: 13px; font-weight: bold; padding: 5px 10px; background: #F6F6F6; border-radius: 5px 5px 0 0; cursor: move;">'
				+ '<span style="float: right; padding-left: 7px; cursor: pointer;" title="Remove Console" onclick="$(this).closest(\'#layoutLogger\').remove()">X</span>Layout console.log</div>'
				+ '<ul style="font-size: 13px; font-weight: none; list-style: none; margin: 0; padding: 0 0 2px;"></ul>'
				+ '</div>'
				).appendTo("body");
            $e.css('left', $(window).width() - $e.outerWidth() - 5)
            if ($.ui.draggable) $e.draggable({ handle: ':first-child' });
            return $e;
        };
    }

    };


    /*
     *	$.layout.browser REPLACES removed $.browser, with extra data
     *	Parsing code here adapted from jQuery 1.8 $.browse
     */
    (function () {
        var u = navigator.userAgent.toLowerCase()
        , m = /(chrome)[ \/]([\w.]+)/.exec(u)
            || /(webkit)[ \/]([\w.]+)/.exec(u)
            || /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(u)
            || /(msie) ([\w.]+)/.exec(u)
            || u.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(u)
            || []
        , b = m[1] || ""
        , v = m[2] || 0
        , ie = b === "msie"
        , cm = document.compatMode
        , $s = $.support
        , bs = $s.boxSizing !== undefined ? $s.boxSizing : $s.boxSizingReliable
        , bm = !ie || !cm || cm === "CSS1Compat" || $s.boxModel || false
        , lb = $.layout.browser = {
            version: v
            , safari: b === "webkit"	// webkit (NOT chrome) = safari
            , webkit: b === "chrome"	// chrome = webkit
            , msie: ie
            , isIE6: ie && v == 6
            // ONLY IE reverts to old box-model - Note that compatMode was deprecated as of IE8
            , boxModel: bm
            , boxSizing: !!(typeof bs === "function" ? bs() : bs)
        };
        ;
        if (b) lb[b] = true; // set CURRENT browser
        /*	OLD versions of jQuery only set $.support.boxModel after page is loaded
         *	so if this is IE, use support.boxModel to test for quirks-mode (ONLY IE changes boxModel) */
        if (!bm && !cm) $(function () { lb.boxModel = $s.boxModel; });
    })();


    // DEFAULT OPTIONS
    $.layout.defaults = {
        /*
         *	LAYOUT & LAYOUT-CONTAINER OPTIONS
         *	- none of these options are applicable to individual panes
         */
        name: ""			// Not required, but useful for buttons and used for the state-cookie
    , containerClass: "ui-layout-container" // layout-container element
    , inset: null		// custom container-inset values (override padding)
    , scrollToBookmarkOnLoad: true		// after creating a layout, scroll to bookmark in URL (.../page.htm#myBookmark)
    , resizeWithWindow: true		// bind thisLayout.resizeAll() to the window.resize event
    , resizeWithWindowDelay: 200			// delay calling resizeAll because makes window resizing very jerky
    , resizeWithWindowMaxDelay: 0			// 0 = none - force resize every XX ms while window is being resized
    , maskPanesEarly: false		// true = create pane-masks on resizer.mouseDown instead of waiting for resizer.dragstart
    , onresizeall_start: null		// CALLBACK when resizeAll() STARTS	- NOT pane-specific
    , onresizeall_end: null		// CALLBACK when resizeAll() ENDS	- NOT pane-specific
    , onload_start: null		// CALLBACK when Layout inits - after options initialized, but before elements
    , onload_end: null		// CALLBACK when Layout inits - after EVERYTHING has been initialized
    , onunload_start: null		// CALLBACK when Layout is destroyed OR onWindowUnload
    , onunload_end: null		// CALLBACK when Layout is destroyed OR onWindowUnload
    , initPanes: true		// false = DO NOT initialize the panes onLoad - will init later
    , showErrorMessages: true		// enables fatal error messages to warn developers of common errors
    , showDebugMessages: false		// display console-and-alert debug msgs - IF this Layout version _has_ debugging code!
        //	Changing this zIndex value will cause other zIndex values to automatically change
    , zIndex: null		// the PANE zIndex - resizers and masks will be +1
        //	DO NOT CHANGE the zIndex values below unless you clearly understand their relationships
    , zIndexes: {								// set _default_ z-index values here...
        pane_normal: 0			// normal z-index for panes
        , content_mask: 1			// applied to overlays used to mask content INSIDE panes during resizing
        , resizer_normal: 2			// normal z-index for resizer-bars
        , pane_sliding: 100			// applied to *BOTH* the pane and its resizer when a pane is 'slid open'
        , pane_animate: 1000		// applied to the pane when being animated - not applied to the resizer
        , resizer_drag: 10000		// applied to the CLONED resizer-bar when being 'dragged'
    }
    , errors: {
        pane: "pane"		// description of "layout pane element" - used only in error messages
        , selector: "selector"	// description of "jQuery-selector" - used only in error messages
        , addButtonError: "Error Adding Button\nInvalid "
        , containerMissing: "UI Layout Initialization Error\nThe specified layout-container does not exist."
        , centerPaneMissing: "UI Layout Initialization Error\nThe center-pane element does not exist.\nThe center-pane is a required element."
        , noContainerHeight: "UI Layout Initialization Warning\nThe layout-container \"CONTAINER\" has no height.\nTherefore the layout is 0-height and hence 'invisible'!"
        , callbackError: "UI Layout Callback Error\nThe EVENT callback is not a valid function."
    }
        /*
         *	PANE DEFAULT SETTINGS
         *	- settings under the 'panes' key become the default settings for *all panes*
         *	- ALL pane-options can also be set specifically for each panes, which will override these 'default values'
         */
    , panes: { // default options for 'all panes' - will be overridden by 'per-pane settings'
        applyDemoStyles: false		// NOTE: renamed from applyDefaultStyles for clarity
        , closable: true		// pane can open & close
        , resizable: true		// when open, pane can be resized 
        , slidable: true		// when closed, pane can 'slide open' over other panes - closes on mouse-out
        , initClosed: false		// true = init pane as 'closed'
        , initHidden: false 		// true = init pane as 'hidden' - no resizer-bar/spacing
        //	SELECTORS
        //,	paneSelector:			""			// MUST be pane-specific - jQuery selector for pane
        , contentSelector: ".ui-layout-content" // INNER div/element to auto-size so only it scrolls, not the entire pane!
        , contentIgnoreSelector: ".ui-layout-ignore"	// element(s) to 'ignore' when measuring 'content'
        , findNestedContent: false		// true = $P.find(contentSelector), false = $P.children(contentSelector)
        //	GENERIC ROOT-CLASSES - for auto-generated classNames
        , paneClass: "ui-layout-pane"	// Layout Pane
        , resizerClass: "ui-layout-resizer"	// Resizer Bar
        , togglerClass: "ui-layout-toggler"	// Toggler Button
        , buttonClass: "ui-layout-button"	// CUSTOM Buttons	- eg: '[ui-layout-button]-toggle/-open/-close/-pin'
        //	ELEMENT SIZE & SPACING
        //,	size:					100			// MUST be pane-specific -initial size of pane
        , minSize: 0			// when manually resizing a pane
        , maxSize: 0			// ditto, 0 = no limit
        , spacing_open: 6			// space between pane and adjacent panes - when pane is 'open'
        , spacing_closed: 6			// ditto - when pane is 'closed'
        , togglerLength_open: 50			// Length = WIDTH of toggler button on north/south sides - HEIGHT on east/west sides
        , togglerLength_closed: 50			// 100% OR -1 means 'full height/width of resizer bar' - 0 means 'hidden'
        , togglerAlign_open: "center"	// top/left, bottom/right, center, OR...
        , togglerAlign_closed: "center"	// 1 => nn = offset from top/left, -1 => -nn == offset from bottom/right
        , togglerContent_open: ""			// text or HTML to put INSIDE the toggler
        , togglerContent_closed: ""			// ditto
        //	RESIZING OPTIONS
        , resizerDblClickToggle: true		// 
        , autoResize: true		// IF size is 'auto' or a percentage, then recalc 'pixel size' whenever the layout resizes
        , autoReopen: true		// IF a pane was auto-closed due to noRoom, reopen it when there is room? False = leave it closed
        , resizerDragOpacity: 1			// option for ui.draggable
        //,	resizerCursor:			""			// MUST be pane-specific - cursor when over resizer-bar
        , maskContents: false		// true = add DIV-mask over-or-inside this pane so can 'drag' over IFRAMES
        , maskObjects: false		// true = add IFRAME-mask over-or-inside this pane to cover objects/applets - content-mask will overlay this mask
        , maskZindex: null		// will override zIndexes.content_mask if specified - not applicable to iframe-panes
        , resizingGrid: false		// grid size that the resizers will snap-to during resizing, eg: [20,20]
        , livePaneResizing: false		// true = LIVE Resizing as resizer is dragged
        , liveContentResizing: false		// true = re-measure header/footer heights as resizer is dragged
        , liveResizingTolerance: 1			// how many px change before pane resizes, to control performance
        //	SLIDING OPTIONS
        , sliderCursor: "pointer"	// cursor when resizer-bar will trigger 'sliding'
        , slideTrigger_open: "click"		// click, dblclick, mouseenter
        , slideTrigger_close: "mouseleave"// click, mouseleave
        , slideDelay_open: 300			// applies only for mouseenter event - 0 = instant open
        , slideDelay_close: 300			// applies only for mouseleave event (300ms is the minimum!)
        , hideTogglerOnSlide: false		// when pane is slid-open, should the toggler show?
        , preventQuickSlideClose: $.layout.browser.webkit // Chrome triggers slideClosed as it is opening
        , preventPrematureSlideClose: false	// handle incorrect mouseleave trigger, like when over a SELECT-list in IE
        //	PANE-SPECIFIC TIPS & MESSAGES
        , tips: {
            Open: "Open"		// eg: "Open Pane"
            , Close: "Close"
            , Resize: "Resize"
            , Slide: "Slide Open"
            , Pin: "Pin"
            , Unpin: "Un-Pin"
            , noRoomToOpen: "Not enough room to show this panel."	// alert if user tries to open a pane that cannot
            , minSizeWarning: "Panel has reached its minimum size"	// displays in browser statusbar
            , maxSizeWarning: "Panel has reached its maximum size"	// ditto
        }
        //	HOT-KEYS & MISC
        , showOverflowOnHover: false		// will bind allowOverflow() utility to pane.onMouseOver
        , enableCursorHotkey: true		// enabled 'cursor' hotkeys
        //,	customHotkey:			""			// MUST be pane-specific - EITHER a charCode OR a character
        , customHotkeyModifier: "SHIFT"		// either 'SHIFT', 'CTRL' or 'CTRL+SHIFT' - NOT 'ALT'
        //	PANE ANIMATION
        //	NOTE: fxSss_open, fxSss_close & fxSss_size options (eg: fxName_open) are auto-generated if not passed
        , fxName: "slide" 	// ('none' or blank), slide, drop, scale -- only relevant to 'open' & 'close', NOT 'size'
        , fxSpeed: null		// slow, normal, fast, 200, nnn - if passed, will OVERRIDE fxSettings.duration
        , fxSettings: {}			// can be passed, eg: { easing: "easeOutBounce", duration: 1500 }
        , fxOpacityFix: true		// tries to fix opacity in IE to restore anti-aliasing after animation
        , animatePaneSizing: false		// true = animate resizing after dragging resizer-bar OR sizePane() is called
        /*  NOTE: Action-specific FX options are auto-generated from the options above if not specifically set:
            fxName_open:			"slide"		// 'Open' pane animation
            fnName_close:			"slide"		// 'Close' pane animation
            fxName_size:			"slide"		// 'Size' pane animation - when animatePaneSizing = true
            fxSpeed_open:			null
            fxSpeed_close:			null
            fxSpeed_size:			null
            fxSettings_open:		{}
            fxSettings_close:		{}
            fxSettings_size:		{}
        */
        //	CHILD/NESTED LAYOUTS
        , children: null		// Layout-options for nested/child layout - even {} is valid as options
        , containerSelector: ''			// if child is NOT 'directly nested', a selector to find it/them (can have more than one child layout!)
        , initChildren: true		// true = child layout will be created as soon as _this_ layout completes initialization
        , destroyChildren: true		// true = destroy child-layout if this pane is destroyed
        , resizeChildren: true		// true = trigger child-layout.resizeAll() when this pane is resized
        //	EVENT TRIGGERING
        , triggerEventsOnLoad: false		// true = trigger onopen OR onclose callbacks when layout initializes
        , triggerEventsDuringLiveResize: true	// true = trigger onresize callback REPEATEDLY if livePaneResizing==true
        //	PANE CALLBACKS
        , onshow_start: null		// CALLBACK when pane STARTS to Show	- BEFORE onopen/onhide_start
        , onshow_end: null		// CALLBACK when pane ENDS being Shown	- AFTER  onopen/onhide_end
        , onhide_start: null		// CALLBACK when pane STARTS to Close	- BEFORE onclose_start
        , onhide_end: null		// CALLBACK when pane ENDS being Closed	- AFTER  onclose_end
        , onopen_start: null		// CALLBACK when pane STARTS to Open
        , onopen_end: null		// CALLBACK when pane ENDS being Opened
        , onclose_start: null		// CALLBACK when pane STARTS to Close
        , onclose_end: null		// CALLBACK when pane ENDS being Closed
        , onresize_start: null		// CALLBACK when pane STARTS being Resized ***FOR ANY REASON***
        , onresize_end: null		// CALLBACK when pane ENDS being Resized ***FOR ANY REASON***
        , onsizecontent_start: null		// CALLBACK when sizing of content-element STARTS
        , onsizecontent_end: null		// CALLBACK when sizing of content-element ENDS
        , onswap_start: null		// CALLBACK when pane STARTS to Swap
        , onswap_end: null		// CALLBACK when pane ENDS being Swapped
        , ondrag_start: null		// CALLBACK when pane STARTS being ***MANUALLY*** Resized
        , ondrag_end: null		// CALLBACK when pane ENDS being ***MANUALLY*** Resized
    }
        /*
         *	PANE-SPECIFIC SETTINGS
         *	- options listed below MUST be specified per-pane - they CANNOT be set under 'panes'
         *	- all options under the 'panes' key can also be set specifically for any pane
         *	- most options under the 'panes' key apply only to 'border-panes' - NOT the the center-pane
         */
    , north: {
        paneSelector: ".ui-layout-north"
        , size: "auto"		// eg: "auto", "30%", .30, 200
        , resizerCursor: "n-resize"	// custom = url(myCursor.cur)
        , customHotkey: ""			// EITHER a charCode (43) OR a character ("o")
    }
    , south: {
        paneSelector: ".ui-layout-south"
        , size: "auto"
        , resizerCursor: "s-resize"
        , customHotkey: ""
    }
    , east: {
        paneSelector: ".ui-layout-east"
        , size: 200
        , resizerCursor: "e-resize"
        , customHotkey: ""
    }
    , west: {
        paneSelector: ".ui-layout-west"
        , size: 200
        , resizerCursor: "w-resize"
        , customHotkey: ""
    }
    , center: {
        paneSelector: ".ui-layout-center"
        , minWidth: 0
        , minHeight: 0
    }
    };

    $.layout.optionsMap = {
        // layout/global options - NOT pane-options
        layout: ("name,instanceKey,stateManagement,effects,inset,zIndexes,errors,"
        + "zIndex,scrollToBookmarkOnLoad,showErrorMessages,maskPanesEarly,"
        + "outset,resizeWithWindow,resizeWithWindowDelay,resizeWithWindowMaxDelay,"
        + "onresizeall,onresizeall_start,onresizeall_end,onload,onload_start,onload_end,onunload,onunload_start,onunload_end").split(",")
        //	borderPanes: [ ALL options that are NOT specified as 'layout' ]
        // default.panes options that apply to the center-pane (most options apply _only_ to border-panes)
    , center: ("paneClass,contentSelector,contentIgnoreSelector,findNestedContent,applyDemoStyles,triggerEventsOnLoad,"
        + "showOverflowOnHover,maskContents,maskObjects,liveContentResizing,"
        + "containerSelector,children,initChildren,resizeChildren,destroyChildren,"
        + "onresize,onresize_start,onresize_end,onsizecontent,onsizecontent_start,onsizecontent_end").split(",")
        // options that MUST be specifically set 'per-pane' - CANNOT set in the panes (defaults) key
    , noDefault: ("paneSelector,resizerCursor,customHotkey").split(",")
    };

    /**
     * Processes options passed in converts flat-format data into subkey (JSON) format
     * In flat-format, subkeys are _currently_ separated with 2 underscores, like north__optName
     * Plugins may also call this method so they can transform their own data
     *
     * @param  {!Object}	hash			Data/options passed by user - may be a single level or nested levels
     * @param  {boolean=}	[addKeys=false]	Should the primary layout.options keys be added if they do not exist?
     * @return {Object}						Returns hash of minWidth & minHeight
     */
    $.layout.transformData = function (hash, addKeys) {
        var json = addKeys ? { panes: {}, center: {} } : {} // init return object
        , branch, optKey, keys, key, val, i, c;

        if (typeof hash !== "object") return json; // no options passed

        // convert all 'flat-keys' to 'sub-key' format
        for (optKey in hash) {
            branch = json;
            val = hash[optKey];
            keys = optKey.split("__"); // eg: west__size or north__fxSettings__duration
            c = keys.length - 1;
            // convert underscore-delimited to subkeys
            for (i = 0; i <= c; i++) {
                key = keys[i];
                if (i === c) {	// last key = value
                    if ($.isPlainObject(val))
                        branch[key] = $.layout.transformData(val); // RECURSE
                    else
                        branch[key] = val;
                }
                else {
                    if (!branch[key])
                        branch[key] = {}; // create the subkey
                    // recurse to sub-key for next loop - if not done
                    branch = branch[key];
                }
            }
        }
        return json;
    };

    // INTERNAL CONFIG DATA - DO NOT CHANGE THIS!
    $.layout.backwardCompatibility = {
        // data used by renameOldOptions()
        map: {
            //	OLD Option Name:			NEW Option Name
            applyDefaultStyles: "applyDemoStyles"
            //	CHILD/NESTED LAYOUTS
        , childOptions: "children"
        , initChildLayout: "initChildren"
        , destroyChildLayout: "destroyChildren"
        , resizeChildLayout: "resizeChildren"
        , resizeNestedLayout: "resizeChildren"
            //	MISC Options
        , resizeWhileDragging: "livePaneResizing"
        , resizeContentWhileDragging: "liveContentResizing"
        , triggerEventsWhileDragging: "triggerEventsDuringLiveResize"
        , maskIframesOnResize: "maskContents"
            //	STATE MANAGEMENT
        , useStateCookie: "stateManagement.enabled"
        , "cookie.autoLoad": "stateManagement.autoLoad"
        , "cookie.autoSave": "stateManagement.autoSave"
        , "cookie.keys": "stateManagement.stateKeys"
        , "cookie.name": "stateManagement.cookie.name"
        , "cookie.domain": "stateManagement.cookie.domain"
        , "cookie.path": "stateManagement.cookie.path"
        , "cookie.expires": "stateManagement.cookie.expires"
        , "cookie.secure": "stateManagement.cookie.secure"
            //	OLD Language options
        , noRoomToOpenTip: "tips.noRoomToOpen"
        , togglerTip_open: "tips.Close"	// open   = Close
        , togglerTip_closed: "tips.Open"		// closed = Open
        , resizerTip: "tips.Resize"
        , sliderTip: "tips.Slide"
        }

        /**
        * @param {Object}	opts
        */
    , renameOptions: function (opts) {
        var map = $.layout.backwardCompatibility.map
		, oldData, newData, value
        ;
        for (var itemPath in map) {
            oldData = getBranch(itemPath);
            value = oldData.branch[oldData.key];
            if (value !== undefined) {
                newData = getBranch(map[itemPath], true);
                newData.branch[newData.key] = value;
                delete oldData.branch[oldData.key];
            }
        }

        /**
		 * @param {string}	path
		 * @param {boolean=}	[create=false]	Create path if does not exist
		 */
        function getBranch(path, create) {
            var a = path.split(".") // split keys into array
			, c = a.length - 1
			, D = { branch: opts, key: a[c] } // init branch at top & set key (last item)
			, i = 0, k, undef;
            for (; i < c; i++) { // skip the last key (data)
                k = a[i];
                if (D.branch[k] == undefined) { // child-key does not exist
                    if (create) {
                        D.branch = D.branch[k] = {}; // create child-branch
                    }
                    else // can't go any farther
                        D.branch = {}; // branch is undefined
                }
                else
                    D.branch = D.branch[k]; // get child-branch
            }
            return D;
        };
    }

        /**
        * @param {Object}	opts
        */
    , renameAllOptions: function (opts) {
        var ren = $.layout.backwardCompatibility.renameOptions;
        // rename root (layout) options
        ren(opts);
        // rename 'defaults' to 'panes'
        if (opts.defaults) {
            if (typeof opts.panes !== "object")
                opts.panes = {};
            $.extend(true, opts.panes, opts.defaults);
            delete opts.defaults;
        }
        // rename options in the the options.panes key
        if (opts.panes) ren(opts.panes);
        // rename options inside *each pane key*, eg: options.west
        $.each($.layout.config.allPanes, function (i, pane) {
            if (opts[pane]) ren(opts[pane]);
        });
        return opts;
    }
    };




    /*	============================================================
     *	BEGIN WIDGET: $( selector ).layout( {options} );
     *	============================================================
     */
    $.fn.layout = function (opts) {
        var

        // local aliases to global data
        browser = $.layout.browser
    , _c = $.layout.config

        // local aliases to utlity methods
    , cssW = $.layout.cssWidth
    , cssH = $.layout.cssHeight
    , elDims = $.layout.getElementDimensions
    , styles = $.layout.getElementStyles
    , evtObj = $.layout.getEventObject
    , evtPane = $.layout.parsePaneName

    /**
     * options - populated by initOptions()
     */
    , options = $.extend(true, {}, $.layout.defaults)
    , effects = options.effects = $.extend(true, {}, $.layout.effects)

    /**
     * layout-state object
     */
    , state = {
        // generate unique ID to use for event.namespace so can unbind only events added by 'this layout'
        id: "layout" + $.now()	// code uses alias: sID
        , initialized: false
        , paneResizing: false
        , panesSliding: {}
        , container: { 	// list all keys referenced in code to avoid compiler error msgs
            innerWidth: 0
            , innerHeight: 0
            , outerWidth: 0
            , outerHeight: 0
            , layoutWidth: 0
            , layoutHeight: 0
        }
        , north: { childIdx: 0 }
        , south: { childIdx: 0 }
        , east: { childIdx: 0 }
        , west: { childIdx: 0 }
        , center: { childIdx: 0 }
    }

    /**
     * parent/child-layout pointers
     */
    //,	hasParentLayout	= false	- exists ONLY inside Instance so can be set externally
    , children = {
        north: null
        , south: null
        , east: null
        , west: null
        , center: null
    }

    /*
     * ###########################
     *  INTERNAL HELPER FUNCTIONS
     * ###########################
     */

        /**
         * Manages all internal timers
         */
    , timer = {
        data: {}
        , set: function (s, fn, ms) { timer.clear(s); timer.data[s] = setTimeout(fn, ms); }
        , clear: function (s) { var t = timer.data; if (t[s]) { clearTimeout(t[s]); delete t[s]; } }
    }

        /**
         * Alert or console.log a message - IF option is enabled.
         *
         * @param {(string|!Object)}	msg				Message (or debug-data) to display
         * @param {boolean=}			[popup=false]	True by default, means 'alert', false means use console.log
         * @param {boolean=}			[debug=false]	True means is a widget debugging message
         */
    , _log = function (msg, popup, debug) {
        var o = options;
        if ((o.showErrorMessages && !debug) || (debug && o.showDebugMessages))
            $.layout.msg(o.name + ' / ' + msg, (popup !== false));
        return false;
    }

        /**
         * Executes a Callback function after a trigger event, like resize, open or close
         *
         * @param {string}				evtName					Name of the layout callback, eg "onresize_start"
         * @param {(string|boolean)=}	[pane=""]				This is passed only so we can pass the 'pane object' to the callback
         * @param {(string|boolean)=}	[skipBoundEvents=false]	True = do not run events bound to the elements - only the callbacks set in options
         */
    , _runCallbacks = function (evtName, pane, skipBoundEvents) {
        var hasPane = pane && isStr(pane)
		, s = hasPane ? state[pane] : state
		, o = hasPane ? options[pane] : options
		, lName = options.name
			// names like onopen and onopen_end separate are interchangeable in options...
		, lng = evtName + (evtName.match(/_/) ? "" : "_end")
		, shrt = lng.match(/_end$/) ? lng.substr(0, lng.length - 4) : ""
		, fn = o[lng] || o[shrt]
		, retVal = "NC" // NC = No Callback
		, args = []
		, $P = hasPane ? $Ps[pane] : 0
        ;
        if (hasPane && !$P) // a pane is specified, but does not exist!
            return retVal;
        if (!hasPane && $.type(pane) === "boolean") {
            skipBoundEvents = pane; // allow pane param to be skipped for Layout callback
            pane = "";
        }

        // first trigger the callback set in the options
        if (fn) {
            try {
                // convert function name (string) to function object
                if (isStr(fn)) {
                    if (fn.match(/,/)) {
                        // function name cannot contain a comma, 
                        // so must be a function name AND a parameter to pass
                        args = fn.split(",")
						, fn = eval(args[0]);
                    }
                    else // just the name of an external function?
                        fn = eval(fn);
                }
                // execute the callback, if exists
                if ($.isFunction(fn)) {
                    if (args.length)
                        retVal = g(fn)(args[1]); // pass the argument parsed from 'list'
                    else if (hasPane)
                        // pass data: pane-name, pane-element, pane-state, pane-options, and layout-name
                        retVal = g(fn)(pane, $Ps[pane], s, o, lName);
                    else // must be a layout/container callback - pass suitable info
                        retVal = g(fn)(Instance, s, o, lName);
                }
            }
            catch (ex) {
                _log(options.errors.callbackError.replace(/EVENT/, $.trim((pane || "") + " " + lng)), false);
                if ($.type(ex) === "string" && string.length)
                    _log("Exception:  " + ex, false);
            }
        }

        // trigger additional events bound directly to the pane
        if (!skipBoundEvents && retVal !== false) {
            if (hasPane) { // PANE events can be bound to each pane-elements
                o = options[pane];
                s = state[pane];
                $P.triggerHandler("layoutpane" + lng, [pane, $P, s, o, lName]);
                if (shrt)
                    $P.triggerHandler("layoutpane" + shrt, [pane, $P, s, o, lName]);
            }
            else { // LAYOUT events can be bound to the container-element
                $N.triggerHandler("layout" + lng, [Instance, s, o, lName]);
                if (shrt)
                    $N.triggerHandler("layout" + shrt, [Instance, s, o, lName]);
            }
        }

        // ALWAYS resizeChildren after an onresize_end event - even during initialization
        // IGNORE onsizecontent_end event because causes child-layouts to resize TWICE
        if (hasPane && evtName === "onresize_end") // BAD: || evtName === "onsizecontent_end"
            resizeChildren(pane + "", true); // compiler hack -force string

        return retVal;

        function g(f) { return f; }; // compiler hack
    }


        /**
         * cure iframe display issues in IE & other browsers
         */
    , _fixIframe = function (pane) {
        if (browser.mozilla) return; // skip FireFox - it auto-refreshes iframes onShow
        var $P = $Ps[pane];
        // if the 'pane' is an iframe, do it
        if (state[pane].tagName === "IFRAME")
            $P.css(_c.hidden).css(_c.visible);
        else // ditto for any iframes INSIDE the pane
            $P.find('IFRAME').css(_c.hidden).css(_c.visible);
    }

        /**
         * @param  {string}		pane		Can accept ONLY a 'pane' (east, west, etc)
         * @param  {number=}		outerSize	(optional) Can pass a width, allowing calculations BEFORE element is resized
         * @return {number}		Returns the innerHeight/Width of el by subtracting padding and borders
         */
    , cssSize = function (pane, outerSize) {
        var fn = _c[pane].dir == "horz" ? cssH : cssW;
        return fn($Ps[pane], outerSize);
    }

        /**
         * @param  {string}		pane		Can accept ONLY a 'pane' (east, west, etc)
         * @return {Object}		Returns hash of minWidth & minHeight
         */
    , cssMinDims = function (pane) {
        // minWidth/Height means CSS width/height = 1px
        var $P = $Ps[pane]
		, dir = _c[pane].dir
		, d = {
		    minWidth: 1001 - cssW($P, 1000)
			, minHeight: 1001 - cssH($P, 1000)
		}
        ;
        if (dir === "horz") d.minSize = d.minHeight;
        if (dir === "vert") d.minSize = d.minWidth;
        return d;
    }

        // TODO: see if these methods can be made more useful...
        // TODO: *maybe* return cssW/H from these so caller can use this info

        /**
         * @param {(string|!Object)}		el
         * @param {number=}				outerWidth
         * @param {boolean=}				[autoHide=false]
         */
    , setOuterWidth = function (el, outerWidth, autoHide) {
        var $E = el, w;
        if (isStr(el)) $E = $Ps[el]; // west
        else if (!el.jquery) $E = $(el);
        w = cssW($E, outerWidth);
        $E.css({ width: w });
        if (w > 0) {
            if (autoHide && $E.data('autoHidden') && $E.innerHeight() > 0) {
                $E.show().data('autoHidden', false);
                if (!browser.mozilla) // FireFox refreshes iframes - IE does not
                    // make hidden, then visible to 'refresh' display after animation
                    $E.css(_c.hidden).css(_c.visible);
            }
        }
        else if (autoHide && !$E.data('autoHidden'))
            $E.hide().data('autoHidden', true);
    }

        /**
         * @param {(string|!Object)}		el
         * @param {number=}				outerHeight
         * @param {boolean=}				[autoHide=false]
         */
    , setOuterHeight = function (el, outerHeight, autoHide) {
        var $E = el, h;
        if (isStr(el)) $E = $Ps[el]; // west
        else if (!el.jquery) $E = $(el);
        h = cssH($E, outerHeight);
        $E.css({ height: h, visibility: "visible" }); // may have been 'hidden' by sizeContent
        if (h > 0 && $E.innerWidth() > 0) {
            if (autoHide && $E.data('autoHidden')) {
                $E.show().data('autoHidden', false);
                if (!browser.mozilla) // FireFox refreshes iframes - IE does not
                    $E.css(_c.hidden).css(_c.visible);
            }
        }
        else if (autoHide && !$E.data('autoHidden'))
            $E.hide().data('autoHidden', true);
    }


        /**
         * Converts any 'size' params to a pixel/integer size, if not already
         * If 'auto' or a decimal/percentage is passed as 'size', a pixel-size is calculated
         *
        /**
         * @param  {string}				pane
         * @param  {(string|number)=}	size
         * @param  {string=}				[dir]
         * @return {number}
         */
    , _parseSize = function (pane, size, dir) {
        if (!dir) dir = _c[pane].dir;

        if (isStr(size) && size.match(/%/))
            size = (size === '100%') ? -1 : parseInt(size, 10) / 100; // convert % to decimal

        if (size === 0)
            return 0;
        else if (size >= 1)
            return parseInt(size, 10);

        var o = options, avail = 0;
        if (dir == "horz") // north or south or center.minHeight
            avail = sC.innerHeight - ($Ps.north ? o.north.spacing_open : 0) - ($Ps.south ? o.south.spacing_open : 0);
        else if (dir == "vert") // east or west or center.minWidth
            avail = sC.innerWidth - ($Ps.west ? o.west.spacing_open : 0) - ($Ps.east ? o.east.spacing_open : 0);

        if (size === -1) // -1 == 100%
            return avail;
        else if (size > 0) // percentage, eg: .25
            return round(avail * size);
        else if (pane == "center")
            return 0;
        else { // size < 0 || size=='auto' || size==Missing || size==Invalid
            // auto-size the pane
            var dim = (dir === "horz" ? "height" : "width")
			, $P = $Ps[pane]
			, $C = dim === 'height' ? $Cs[pane] : false
			, vis = $.layout.showInvisibly($P) // show pane invisibly if hidden
			, szP = $P.css(dim) // SAVE current pane size
			, szC = $C ? $C.css(dim) : 0 // SAVE current content size
            ;
            $P.css(dim, "auto");
            if ($C) $C.css(dim, "auto");
            size = (dim === "height") ? $P.outerHeight() : $P.outerWidth(); // MEASURE
            $P.css(dim, szP).css(vis); // RESET size & visibility
            if ($C) $C.css(dim, szC);
            return size;
        }
    }

        /**
         * Calculates current 'size' (outer-width or outer-height) of a border-pane - optionally with 'pane-spacing' added
         *
         * @param  {(string|!Object)}	pane
         * @param  {boolean=}			[inclSpace=false]
         * @return {number}				Returns EITHER Width for east/west panes OR Height for north/south panes
         */
    , getPaneSize = function (pane, inclSpace) {
        var
			$P = $Ps[pane]
		, o = options[pane]
		, s = state[pane]
		, oSp = (inclSpace ? o.spacing_open : 0)
		, cSp = (inclSpace ? o.spacing_closed : 0)
        ;
        if (!$P || s.isHidden)
            return 0;
        else if (s.isClosed || (s.isSliding && inclSpace))
            return cSp;
        else if (_c[pane].dir === "horz")
            return $P.outerHeight() + oSp;
        else // dir === "vert"
            return $P.outerWidth() + oSp;
    }

        /**
         * Calculate min/max pane dimensions and limits for resizing
         *
         * @param  {string}		pane
         * @param  {boolean=}	[slide=false]
         */
    , setSizeLimits = function (pane, slide) {
        if (!isInitialized()) return;
        var
			o = options[pane]
		, s = state[pane]
		, c = _c[pane]
		, dir = c.dir
		, type = c.sizeType.toLowerCase()
		, isSliding = (slide != undefined ? slide : s.isSliding) // only open() passes 'slide' param
		, $P = $Ps[pane]
		, paneSpacing = o.spacing_open
		//	measure the pane on the *opposite side* from this pane
		, altPane = _c.oppositeEdge[pane]
		, altS = state[altPane]
		, $altP = $Ps[altPane]
		, altPaneSize = (!$altP || altS.isVisible === false || altS.isSliding ? 0 : (dir == "horz" ? $altP.outerHeight() : $altP.outerWidth()))
		, altPaneSpacing = ((!$altP || altS.isHidden ? 0 : options[altPane][altS.isClosed !== false ? "spacing_closed" : "spacing_open"]) || 0)
		//	limitSize prevents this pane from 'overlapping' opposite pane
		, containerSize = (dir == "horz" ? sC.innerHeight : sC.innerWidth)
		, minCenterDims = cssMinDims("center")
		, minCenterSize = dir == "horz" ? max(options.center.minHeight, minCenterDims.minHeight) : max(options.center.minWidth, minCenterDims.minWidth)
		//	if pane is 'sliding', then ignore center and alt-pane sizes - because 'overlays' them
		, limitSize = (containerSize - paneSpacing - (isSliding ? 0 : (_parseSize("center", minCenterSize, dir) + altPaneSize + altPaneSpacing)))
		, minSize = s.minSize = max(_parseSize(pane, o.minSize), cssMinDims(pane).minSize)
		, maxSize = s.maxSize = min((o.maxSize ? _parseSize(pane, o.maxSize) : 100000), limitSize)
		, r = s.resizerPosition = {} // used to set resizing limits
		, top = sC.inset.top
		, left = sC.inset.left
		, W = sC.innerWidth
		, H = sC.innerHeight
		, rW = o.spacing_open // subtract resizer-width to get top/left position for south/east
        ;
        switch (pane) {
            case "north": r.min = top + minSize;
                r.max = top + maxSize;
                break;
            case "west": r.min = left + minSize;
                r.max = left + maxSize;
                break;
            case "south": r.min = top + H - maxSize - rW;
                r.max = top + H - minSize - rW;
                break;
            case "east": r.min = left + W - maxSize - rW;
                r.max = left + W - minSize - rW;
                break;
        };
    }

        /**
         * Returns data for setting the size/position of center pane. Also used to set Height for east/west panes
         *
         * @return JSON  Returns a hash of all dimensions: top, bottom, left, right, (outer) width and (outer) height
         */
    , calcNewCenterPaneDims = function () {
        var d = {
            top: getPaneSize("north", true) // true = include 'spacing' value for pane
		, bottom: getPaneSize("south", true)
		, left: getPaneSize("west", true)
		, right: getPaneSize("east", true)
		, width: 0
		, height: 0
        };

        // NOTE: sC = state.container
        // calc center-pane outer dimensions
        d.width = sC.innerWidth - d.left - d.right;  // outerWidth
        d.height = sC.innerHeight - d.bottom - d.top; // outerHeight
        // add the 'container border/padding' to get final positions relative to the container
        d.top += sC.inset.top;
        d.bottom += sC.inset.bottom;
        d.left += sC.inset.left;
        d.right += sC.inset.right;

        return d;
    }


        /**
         * @param {!Object}		el
         * @param {boolean=}		[allStates=false]
         */
    , getHoverClasses = function (el, allStates) {
        var
			$El = $(el)
		, type = $El.data("layoutRole")
		, pane = $El.data("layoutEdge")
		, o = options[pane]
		, root = o[type + "Class"]
		, _pane = "-" + pane // eg: "-west"
		, _open = "-open"
		, _closed = "-closed"
		, _slide = "-sliding"
		, _hover = "-hover " // NOTE the trailing space
		, _state = $El.hasClass(root + _closed) ? _closed : _open
		, _alt = _state === _closed ? _open : _closed
		, classes = (root + _hover) + (root + _pane + _hover) + (root + _state + _hover) + (root + _pane + _state + _hover)
        ;
        if (allStates) // when 'removing' classes, also remove alternate-state classes
            classes += (root + _alt + _hover) + (root + _pane + _alt + _hover);

        if (type == "resizer" && $El.hasClass(root + _slide))
            classes += (root + _slide + _hover) + (root + _pane + _slide + _hover);

        return $.trim(classes);
    }
    , addHover = function (evt, el) {
        var $E = $(el || this);
        if (evt && $E.data("layoutRole") === "toggler")
            evt.stopPropagation(); // prevent triggering 'slide' on Resizer-bar
        $E.addClass(getHoverClasses($E));
    }
    , removeHover = function (evt, el) {
        var $E = $(el || this);
        $E.removeClass(getHoverClasses($E, true));
    }

    , onResizerEnter = function (evt) { // ALSO called by toggler.mouseenter
        var pane = $(this).data("layoutEdge")
		, s = state[pane]
		, $d = $(document)
        ;
        // ignore closed-panes and mouse moving back & forth over resizer!
        // also ignore if ANY pane is currently resizing
        if (s.isResizing || state.paneResizing) return;

        if (options.maskPanesEarly)
            showMasks(pane, { resizing: true });
    }
    , onResizerLeave = function (evt, el) {
        var e = el || this // el is only passed when called by the timer
		, pane = $(e).data("layoutEdge")
		, name = pane + "ResizerLeave"
		, $d = $(document)
        ;
        timer.clear(pane + "_openSlider"); // cancel slideOpen timer, if set
        timer.clear(name); // cancel enableSelection timer - may re/set below
        // this method calls itself on a timer because it needs to allow
        // enough time for dragging to kick-in and set the isResizing flag
        // dragging has a 100ms delay set, so this delay must be >100
        if (!el) // 1st call - mouseleave event
            timer.set(name, function () { onResizerLeave(evt, e); }, 200);
            // if user is resizing, dragStop will reset everything, so skip it here
        else if (options.maskPanesEarly && !state.paneResizing) // 2nd call - by timer
            hideMasks();
    }

    /*
     * ###########################
     *   INITIALIZATION METHODS
     * ###########################
     */

        /**
         * Initialize the layout - called automatically whenever an instance of layout is created
         *
         * @see  none - triggered onInit
         * @return  mixed	true = fully initialized | false = panes not initialized (yet) | 'cancel' = abort
         */
    , _create = function () {
        // initialize config/options
        initOptions();
        var o = options
		, s = state;

        // TEMP state so isInitialized returns true during init process
        s.creatingLayout = true;

        // init plugins for this layout, if there are any (eg: stateManagement)
        runPluginCallbacks(Instance, $.layout.onCreate);

        // options & state have been initialized, so now run beforeLoad callback
        // onload will CANCEL layout creation if it returns false
        if (false === _runCallbacks("onload_start"))
            return 'cancel';

        // initialize the container element
        _initContainer();

        // bind hotkey function - keyDown - if required
        initHotkeys();

        // bind window.onunload
        $(window).bind("unload." + sID, unload);

        // init plugins for this layout, if there are any (eg: customButtons)
        runPluginCallbacks(Instance, $.layout.onLoad);

        // if layout elements are hidden, then layout WILL NOT complete initialization!
        // initLayoutElements will set initialized=true and run the onload callback IF successful
        if (o.initPanes) _initLayoutElements();

        delete s.creatingLayout;

        return state.initialized;
    }

        /**
         * Initialize the layout IF not already
         *
         * @see  All methods in Instance run this test
         * @return  boolean	true = layoutElements have been initialized | false = panes are not initialized (yet)
         */
    , isInitialized = function () {
        if (state.initialized || state.creatingLayout) return true;	// already initialized
        else return _initLayoutElements();	// try to init panes NOW
    }

        /**
         * Initialize the layout - called automatically whenever an instance of layout is created
         *
         * @see  _create() & isInitialized
         * @param {boolean=}		[retry=false]	// indicates this is a 2nd try
         * @return  An object pointer to the instance created
         */
    , _initLayoutElements = function (retry) {
        // initialize config/options
        var o = options;
        // CANNOT init panes inside a hidden container!
        if (!$N.is(":visible")) {
            // handle Chrome bug where popup window 'has no height'
            // if layout is BODY element, try again in 50ms
            // SEE: http://layout.jquery-dev.com/samples/test_popup_window.html
            if (!retry && browser.webkit && $N[0].tagName === "BODY")
                setTimeout(function () { _initLayoutElements(true); }, 50);
            return false;
        }

        // a center pane is required, so make sure it exists
        if (!getPane("center").length) {
            return _log(o.errors.centerPaneMissing);
        }

        // TEMP state so isInitialized returns true during init process
        state.creatingLayout = true;

        // update Container dims
        $.extend(sC, elDims($N, o.inset)); // passing inset means DO NOT include insetX values

        // initialize all layout elements
        initPanes();	// size & position panes - calls initHandles() - which calls initResizable()

        if (o.scrollToBookmarkOnLoad) {
            var l = self.location;
            if (l.hash) l.replace(l.hash); // scrollTo Bookmark
        }

        // check to see if this layout 'nested' inside a pane
        if (Instance.hasParentLayout)
            o.resizeWithWindow = false;
            // bind resizeAll() for 'this layout instance' to window.resize event
        else if (o.resizeWithWindow)
            $(window).bind("resize." + sID, windowResize);

        delete state.creatingLayout;
        state.initialized = true;

        // init plugins for this layout, if there are any
        runPluginCallbacks(Instance, $.layout.onReady);

        // now run the onload callback, if exists
        _runCallbacks("onload_end");

        return true; // elements initialized successfully
    }

        /**
         * Initialize nested layouts for a specific pane - can optionally pass layout-options
         *
         * @param {(string|Object)}	evt_or_pane	The pane being opened, ie: north, south, east, or west
         * @param {Object=}			[opts]		Layout-options - if passed, will OVERRRIDE options[pane].children
         * @return  An object pointer to the layout instance created - or null
         */
    , createChildren = function (evt_or_pane, opts) {
        var pane = evtPane.call(this, evt_or_pane)
		, $P = $Ps[pane]
        ;
        if (!$P) return;
        var $C = $Cs[pane]
		, s = state[pane]
		, o = options[pane]
		, sm = options.stateManagement || {}
		, cos = opts ? (o.children = opts) : o.children
        ;
        if ($.isPlainObject(cos))
            cos = [cos]; // convert a hash to a 1-elem array
        else if (!cos || !$.isArray(cos))
            return;

        $.each(cos, function (idx, co) {
            if (!$.isPlainObject(co)) return;

            // determine which element is supposed to be the 'child container'
            // if pane has a 'containerSelector' OR a 'content-div', use those instead of the pane
            var $containers = co.containerSelector ? $P.find(co.containerSelector) : ($C || $P);

            $containers.each(function () {
                var $cont = $(this)
				, child = $cont.data("layout") //	see if a child-layout ALREADY exists on this element
                ;
                // if no layout exists, but children are set, try to create the layout now
                if (!child) {
                    // TODO: see about moving this to the stateManagement plugin, as a method
                    // set a unique child-instance key for this layout, if not already set
                    setInstanceKey({ container: $cont, options: co }, s);
                    // If THIS layout has a hash in stateManagement.autoLoad,
                    // then see if it also contains state-data for this child-layout
                    // If so, copy the stateData to child.options.stateManagement.autoLoad
                    if (sm.includeChildren && state.stateData[pane]) {
                        //	THIS layout's state was cached when its state was loaded
                        var paneChildren = state.stateData[pane].children || {}
						, childState = paneChildren[co.instanceKey]
						, co_sm = co.stateManagement || (co.stateManagement = { autoLoad: true })
                        ;
                        // COPY the stateData into the autoLoad key
                        if (co_sm.autoLoad === true && childState) {
                            co_sm.autoSave = false; // disable autoSave because saving handled by parent-layout
                            co_sm.includeChildren = true;  // cascade option - FOR NOW
                            co_sm.autoLoad = $.extend(true, {}, childState); // COPY the state-hash
                        }
                    }

                    // create the layout
                    child = $cont.layout(co);

                    // if successful, update data
                    if (child) {
                        // add the child and update all layout-pointers
                        // MAY have already been done by child-layout calling parent.refreshChildren()
                        refreshChildren(pane, child);
                    }
                }
            });
        });
    }

    , setInstanceKey = function (child, parentPaneState) {
        // create a named key for use in state and instance branches
        var $c = child.container
		, o = child.options
		, sm = o.stateManagement
		, key = o.instanceKey || $c.data("layoutInstanceKey")
        ;
        if (!key) key = (sm && sm.cookie ? sm.cookie.name : '') || o.name; // look for a name/key
        if (!key) key = "layout" + (++parentPaneState.childIdx);	// if no name/key found, generate one
        else key = key.replace(/[^\w-]/gi, '_').replace(/_{2,}/g, '_');	 // ensure is valid as a hash key
        o.instanceKey = key;
        $c.data("layoutInstanceKey", key); // useful if layout is destroyed and then recreated
        return key;
    }

        /**
         * @param {string}		pane		The pane being opened, ie: north, south, east, or west
         * @param {Object=}		newChild	New child-layout Instance to add to this pane
         */
    , refreshChildren = function (pane, newChild) {
        var $P = $Ps[pane]
		, pC = children[pane]
		, s = state[pane]
		, o
        ;
        // check for destroy()ed layouts and update the child pointers & arrays
        if ($.isPlainObject(pC)) {
            $.each(pC, function (key, child) {
                if (child.destroyed) delete pC[key]
            });
            // if no more children, remove the children hash
            if ($.isEmptyObject(pC))
                pC = children[pane] = null; // clear children hash
        }

        // see if there is a directly-nested layout inside this pane
        // if there is, then there can be only ONE child-layout, so check that...
        if (!newChild && !pC) {
            newChild = $P.data("layout");
        }

        // if a newChild instance was passed, add it to children[pane]
        if (newChild) {
            // update child.state
            newChild.hasParentLayout = true; // set parent-flag in child
            // instanceKey is a key-name used in both state and children
            o = newChild.options;
            // set a unique child-instance key for this layout, if not already set
            setInstanceKey(newChild, s);
            // add pointer to pane.children hash
            if (!pC) pC = children[pane] = {}; // create an empty children hash
            pC[o.instanceKey] = newChild.container.data("layout"); // add childLayout instance
        }

        // ALWAYS refresh the pane.children alias, even if null
        Instance[pane].children = children[pane];

        // if newChild was NOT passed - see if there is a child layout NOW
        if (!newChild) {
            createChildren(pane); // MAY create a child and re-call this method
        }
    }

    , windowResize = function () {
        var o = options
		, delay = Number(o.resizeWithWindowDelay);
        if (delay < 10) delay = 100; // MUST have a delay!
        // resizing uses a delay-loop because the resize event fires repeatly - except in FF, but delay anyway
        timer.clear("winResize"); // if already running
        timer.set("winResize", function () {
            timer.clear("winResize");
            timer.clear("winResizeRepeater");
            var dims = elDims($N, o.inset);
            // only trigger resizeAll() if container has changed size
            if (dims.innerWidth !== sC.innerWidth || dims.innerHeight !== sC.innerHeight)
                resizeAll();
        }, delay);
        // ALSO set fixed-delay timer, if not already running
        if (!timer.data["winResizeRepeater"]) setWindowResizeRepeater();
    }

    , setWindowResizeRepeater = function () {
        var delay = Number(options.resizeWithWindowMaxDelay);
        if (delay > 0)
            timer.set("winResizeRepeater", function () { setWindowResizeRepeater(); resizeAll(); }, delay);
    }

    , unload = function () {
        var o = options;

        _runCallbacks("onunload_start");

        // trigger plugin callabacks for this layout (eg: stateManagement)
        runPluginCallbacks(Instance, $.layout.onUnload);

        _runCallbacks("onunload_end");
    }

        /**
         * Validate and initialize container CSS and events
         *
         * @see  _create()
         */
    , _initContainer = function () {
        var
			N = $N[0]
		, $H = $("html")
		, tag = sC.tagName = N.tagName
		, id = sC.id = N.id
		, cls = sC.className = N.className
		, o = options
		, name = o.name
		, props = "position,margin,padding,border"
		, css = "layoutCSS"
		, CSS = {}
		, hid = "hidden" // used A LOT!
		//	see if this container is a 'pane' inside an outer-layout
		, parent = $N.data("parentLayout")	// parent-layout Instance
		, pane = $N.data("layoutEdge")		// pane-name in parent-layout
		, isChild = parent && pane
		, num = $.layout.cssNum
		, $parent, n
        ;
        // sC = state.container
        sC.selector = $N.selector.split(".slice")[0];
        sC.ref = (o.name ? o.name + ' layout / ' : '') + tag + (id ? "#" + id : cls ? '.[' + cls + ']' : ''); // used in messages
        sC.isBody = (tag === "BODY");

        // try to find a parent-layout
        if (!isChild && !sC.isBody) {
            $parent = $N.closest("." + $.layout.defaults.panes.paneClass);
            parent = $parent.data("parentLayout");
            pane = $parent.data("layoutEdge");
            isChild = parent && pane;
        }

        $N.data({
            layout: Instance
			, layoutContainer: sID // FLAG to indicate this is a layout-container - contains unique internal ID
        })
			.addClass(o.containerClass)
        ;
        var layoutMethods = {
            destroy: ''
		, initPanes: ''
		, resizeAll: 'resizeAll'
		, resize: 'resizeAll'
        };
        // loop hash and bind all methods - include layoutID namespacing
        for (name in layoutMethods) {
            $N.bind("layout" + name.toLowerCase() + "." + sID, Instance[layoutMethods[name] || name]);
        }

        // if this container is another layout's 'pane', then set child/parent pointers
        if (isChild) {
            // update parent flag
            Instance.hasParentLayout = true;
            // set pointers to THIS child-layout (Instance) in parent-layout
            parent.refreshChildren(pane, Instance);
        }

        // SAVE original container CSS for use in destroy()
        if (!$N.data(css)) {
            // handle props like overflow different for BODY & HTML - has 'system default' values
            if (sC.isBody) {
                // SAVE <BODY> CSS
                $N.data(css, $.extend(styles($N, props), {
                    height: $N.css("height")
				, overflow: $N.css("overflow")
				, overflowX: $N.css("overflowX")
				, overflowY: $N.css("overflowY")
                }));
                // ALSO SAVE <HTML> CSS
                $H.data(css, $.extend(styles($H, 'padding'), {
                    height: "auto" // FF would return a fixed px-size!
				, overflow: $H.css("overflow")
				, overflowX: $H.css("overflowX")
				, overflowY: $H.css("overflowY")
                }));
            }
            else // handle props normally for non-body elements
                $N.data(css, styles($N, props + ",top,bottom,left,right,width,height,overflow,overflowX,overflowY"));
        }

        try {
            // common container CSS
            CSS = {
                overflow: hid
			, overflowX: hid
			, overflowY: hid
            };
            $N.css(CSS);

            if (o.inset && !$.isPlainObject(o.inset)) {
                // can specify a single number for equal outset all-around
                n = parseInt(o.inset, 10) || 0
                o.inset = {
                    top: n
				, bottom: n
				, left: n
				, right: n
                };
            }

            // format html & body if this is a full page layout
            if (sC.isBody) {
                // if HTML has padding, use this as an outer-spacing around BODY
                if (!o.outset) {
                    // use padding from parent-elem (HTML) as outset
                    o.outset = {
                        top: num($H, "paddingTop")
					, bottom: num($H, "paddingBottom")
					, left: num($H, "paddingLeft")
					, right: num($H, "paddingRight")
                    };
                }
                else if (!$.isPlainObject(o.outset)) {
                    // can specify a single number for equal outset all-around
                    n = parseInt(o.outset, 10) || 0
                    o.outset = {
                        top: n
					, bottom: n
					, left: n
					, right: n
                    };
                }
                // HTML
                $H.css(CSS).css({
                    height: "100%"
				, border: "none"	// no border or padding allowed when using height = 100%
				, padding: 0		// ditto
				, margin: 0
                });
                // BODY
                if (browser.isIE6) {
                    // IE6 CANNOT use the trick of setting absolute positioning on all 4 sides - must have 'height'
                    $N.css({
                        width: "100%"
					, height: "100%"
					, border: "none"	// no border or padding allowed when using height = 100%
					, padding: 0		// ditto
					, margin: 0
					, position: "relative"
                    });
                    // convert body padding to an inset option - the border cannot be measured in IE6!
                    if (!o.inset) o.inset = elDims($N).inset;
                }
                else { // use absolute positioning for BODY to allow borders & padding without overflow
                    $N.css({
                        width: "auto"
					, height: "auto"
					, margin: 0
					, position: "absolute"	// allows for border and padding on BODY
                    });
                    // apply edge-positioning created above
                    $N.css(o.outset);
                }
                // set current layout-container dimensions
                $.extend(sC, elDims($N, o.inset)); // passing inset means DO NOT include insetX values
            }
            else {
                // container MUST have 'position'
                var p = $N.css("position");
                if (!p || !p.match(/(fixed|absolute|relative)/))
                    $N.css("position", "relative");

                // set current layout-container dimensions
                if ($N.is(":visible")) {
                    $.extend(sC, elDims($N, o.inset)); // passing inset means DO NOT change insetX (padding) values
                    if (sC.innerHeight < 1) // container has no 'height' - warn developer
                        _log(o.errors.noContainerHeight.replace(/CONTAINER/, sC.ref));
                }
            }

            // if container has min-width/height, then enable scrollbar(s)
            if (num($N, "minWidth")) $N.parent().css("overflowX", "auto");
            if (num($N, "minHeight")) $N.parent().css("overflowY", "auto");

        } catch (ex) { }
    }

        /**
         * Bind layout hotkeys - if options enabled
         *
         * @see  _create() and addPane()
         * @param {string=}	[panes=""]	The edge(s) to process
         */
    , initHotkeys = function (panes) {
        panes = panes ? panes.split(",") : _c.borderPanes;
        // bind keyDown to capture hotkeys, if option enabled for ANY pane
        $.each(panes, function (i, pane) {
            var o = options[pane];
            if (o.enableCursorHotkey || o.customHotkey) {
                $(document).bind("keydown." + sID, keyDown); // only need to bind this ONCE
                return false; // BREAK - binding was done
            }
        });
    }

        /**
         * Build final OPTIONS data
         *
         * @see  _create()
         */
    , initOptions = function () {
        var data, d, pane, key, val, i, c, o;

        // reprocess user's layout-options to have correct options sub-key structure
        opts = $.layout.transformData(opts, true); // panes = default subkey

        // auto-rename old options for backward compatibility
        opts = $.layout.backwardCompatibility.renameAllOptions(opts);

        // if user-options has 'panes' key (pane-defaults), clean it...
        if (!$.isEmptyObject(opts.panes)) {
            // REMOVE any pane-defaults that MUST be set per-pane
            data = $.layout.optionsMap.noDefault;
            for (i = 0, c = data.length; i < c; i++) {
                key = data[i];
                delete opts.panes[key]; // OK if does not exist
            }
            // REMOVE any layout-options specified under opts.panes
            data = $.layout.optionsMap.layout;
            for (i = 0, c = data.length; i < c; i++) {
                key = data[i];
                delete opts.panes[key]; // OK if does not exist
            }
        }

        // MOVE any NON-layout-options from opts-root to opts.panes
        data = $.layout.optionsMap.layout;
        var rootKeys = $.layout.config.optionRootKeys;
        for (key in opts) {
            val = opts[key];
            if ($.inArray(key, rootKeys) < 0 && $.inArray(key, data) < 0) {
                if (!opts.panes[key])
                    opts.panes[key] = $.isPlainObject(val) ? $.extend(true, {}, val) : val;
                delete opts[key]
            }
        }

        // START by updating ALL options from opts
        $.extend(true, options, opts);

        // CREATE final options (and config) for EACH pane
        $.each(_c.allPanes, function (i, pane) {

            // apply 'pane-defaults' to CONFIG.[PANE]
            _c[pane] = $.extend(true, {}, _c.panes, _c[pane]);

            d = options.panes;
            o = options[pane];

            // center-pane uses SOME keys in defaults.panes branch
            if (pane === 'center') {
                // ONLY copy keys from opts.panes listed in: $.layout.optionsMap.center
                data = $.layout.optionsMap.center;		// list of 'center-pane keys'
                for (i = 0, c = data.length; i < c; i++) {	// loop the list...
                    key = data[i];
                    // only need to use pane-default if pane-specific value not set
                    if (!opts.center[key] && (opts.panes[key] || !o[key]))
                        o[key] = d[key]; // pane-default
                }
            }
            else {
                // border-panes use ALL keys in defaults.panes branch
                o = options[pane] = $.extend(true, {}, d, o); // re-apply pane-specific opts AFTER pane-defaults
                createFxOptions(pane);
                // ensure all border-pane-specific base-classes exist
                if (!o.resizerClass) o.resizerClass = "ui-layout-resizer";
                if (!o.togglerClass) o.togglerClass = "ui-layout-toggler";
            }
            // ensure we have base pane-class (ALL panes)
            if (!o.paneClass) o.paneClass = "ui-layout-pane";
        });

        // update options.zIndexes if a zIndex-option specified
        var zo = opts.zIndex
		, z = options.zIndexes;
        if (zo > 0) {
            z.pane_normal = zo;
            z.content_mask = max(zo + 1, z.content_mask);	// MIN = +1
            z.resizer_normal = max(zo + 2, z.resizer_normal);	// MIN = +2
        }

        // DELETE 'panes' key now that we are done - values were copied to EACH pane
        delete options.panes;


        function createFxOptions(pane) {
            var o = options[pane]
			, d = options.panes;
            // ensure fxSettings key to avoid errors
            if (!o.fxSettings) o.fxSettings = {};
            if (!d.fxSettings) d.fxSettings = {};

            $.each(["_open", "_close", "_size"], function (i, n) {
                var
					sName = "fxName" + n
				, sSpeed = "fxSpeed" + n
				, sSettings = "fxSettings" + n
					// recalculate fxName according to specificity rules
				, fxName = o[sName] =
						o[sName]	// options.west.fxName_open
					|| d[sName]	// options.panes.fxName_open
					|| o.fxName	// options.west.fxName
					|| d.fxName	// options.panes.fxName
					|| "none"		// MEANS $.layout.defaults.panes.fxName == "" || false || null || 0
				, fxExists = $.effects && ($.effects[fxName] || ($.effects.effect && $.effects.effect[fxName]))
                ;
                // validate fxName to ensure is valid effect - MUST have effect-config data in options.effects
                if (fxName === "none" || !options.effects[fxName] || !fxExists)
                    fxName = o[sName] = "none"; // effect not loaded OR unrecognized fxName

                // set vars for effects subkeys to simplify logic
                var fx = options.effects[fxName] || {}	// effects.slide
				, fx_all = fx.all || null				// effects.slide.all
				, fx_pane = fx[pane] || null				// effects.slide.west
                ;
                // create fxSpeed[_open|_close|_size]
                o[sSpeed] =
					o[sSpeed]				// options.west.fxSpeed_open
				|| d[sSpeed]				// options.west.fxSpeed_open
				|| o.fxSpeed				// options.west.fxSpeed
				|| d.fxSpeed				// options.panes.fxSpeed
				|| null					// DEFAULT - let fxSetting.duration control speed
                ;
                // create fxSettings[_open|_close|_size]
                o[sSettings] = $.extend(
					true
				, {}
				, fx_all					// effects.slide.all
				, fx_pane					// effects.slide.west
				, d.fxSettings			// options.panes.fxSettings
				, o.fxSettings			// options.west.fxSettings
				, d[sSettings]			// options.panes.fxSettings_open
				, o[sSettings]			// options.west.fxSettings_open
				);
            });

            // DONE creating action-specific-settings for this pane,
            // so DELETE generic options - are no longer meaningful
            delete o.fxName;
            delete o.fxSpeed;
            delete o.fxSettings;
        }
    }

        /**
         * Initialize module objects, styling, size and position for all panes
         *
         * @see  _initElements()
         * @param {string}	pane		The pane to process
         */
    , getPane = function (pane) {
        var sel = options[pane].paneSelector
        if (sel.substr(0, 1) === "#") // ID selector
            // NOTE: elements selected 'by ID' DO NOT have to be 'children'
            return $N.find(sel).eq(0);
        else { // class or other selector
            var $P = $N.children(sel).eq(0);
            // look for the pane nested inside a 'form' element
            return $P.length ? $P : $N.children("form:first").children(sel).eq(0);
        }
    }

        /**
         * @param {Object=}		evt
         */
    , initPanes = function (evt) {
        // stopPropagation if called by trigger("layoutinitpanes") - use evtPane utility 
        evtPane(evt);

        // NOTE: do north & south FIRST so we can measure their height - do center LAST
        $.each(_c.allPanes, function (idx, pane) {
            addPane(pane, true);
        });

        // init the pane-handles NOW in case we have to hide or close the pane below
        initHandles();

        // now that all panes have been initialized and initially-sized,
        // make sure there is really enough space available for each pane
        $.each(_c.borderPanes, function (i, pane) {
            if ($Ps[pane] && state[pane].isVisible) { // pane is OPEN
                setSizeLimits(pane);
                makePaneFit(pane); // pane may be Closed, Hidden or Resized by makePaneFit()
            }
        });
        // size center-pane AGAIN in case we 'closed' a border-pane in loop above
        sizeMidPanes("center");

        //	Chrome/Webkit sometimes fires callbacks BEFORE it completes resizing!
        //	Before RC30.3, there was a 10ms delay here, but that caused layout 
        //	to load asynchrously, which is BAD, so try skipping delay for now

        // process pane contents and callbacks, and init/resize child-layout if exists
        $.each(_c.allPanes, function (idx, pane) {
            afterInitPane(pane);
        });
    }

        /**
         * Add a pane to the layout - subroutine of initPanes()
         *
         * @see  initPanes()
         * @param {string}	pane			The pane to process
         * @param {boolean=}	[force=false]	Size content after init
         */
    , addPane = function (pane, force) {
        if (!force && !isInitialized()) return;
        var
			o = options[pane]
		, s = state[pane]
		, c = _c[pane]
		, dir = c.dir
		, fx = s.fx
		, spacing = o.spacing_open || 0
		, isCenter = (pane === "center")
		, CSS = {}
		, $P = $Ps[pane]
		, size, minSize, maxSize, child
        ;
        // if pane-pointer already exists, remove the old one first
        if ($P)
            removePane(pane, false, true, false);
        else
            $Cs[pane] = false; // init

        $P = $Ps[pane] = getPane(pane);
        if (!$P.length) {
            $Ps[pane] = false; // logic
            return;
        }

        // SAVE original Pane CSS
        if (!$P.data("layoutCSS")) {
            var props = "position,top,left,bottom,right,width,height,overflow,zIndex,display,backgroundColor,padding,margin,border";
            $P.data("layoutCSS", styles($P, props));
        }

        // create alias for pane data in Instance - initHandles will add more
        Instance[pane] = {
            name: pane
		, pane: $Ps[pane]
		, content: $Cs[pane]
		, options: options[pane]
		, state: state[pane]
		, children: children[pane]
        };

        // add classes, attributes & events
        $P.data({
            parentLayout: Instance		// pointer to Layout Instance
			, layoutPane: Instance[pane]	// NEW pointer to pane-alias-object
			, layoutEdge: pane
			, layoutRole: "pane"
        })
			.css(c.cssReq).css("zIndex", options.zIndexes.pane_normal)
			.css(o.applyDemoStyles ? c.cssDemo : {}) // demo styles
			.addClass(o.paneClass + " " + o.paneClass + "-" + pane) // default = "ui-layout-pane ui-layout-pane-west" - may be a dupe of 'paneSelector'
			.bind("mouseenter." + sID, addHover)
			.bind("mouseleave." + sID, removeHover)
        ;
        var paneMethods = {
            hide: ''
			, show: ''
			, toggle: ''
			, close: ''
			, open: ''
			, slideOpen: ''
			, slideClose: ''
			, slideToggle: ''
			, size: 'sizePane'
			, sizePane: 'sizePane'
			, sizeContent: ''
			, sizeHandles: ''
			, enableClosable: ''
			, disableClosable: ''
			, enableSlideable: ''
			, disableSlideable: ''
			, enableResizable: ''
			, disableResizable: ''
			, swapPanes: 'swapPanes'
			, swap: 'swapPanes'
			, move: 'swapPanes'
			, removePane: 'removePane'
			, remove: 'removePane'
			, createChildren: ''
			, resizeChildren: ''
			, resizeAll: 'resizeAll'
			, resizeLayout: 'resizeAll'
        }
		, name;
        // loop hash and bind all methods - include layoutID namespacing
        for (name in paneMethods) {
            $P.bind("layoutpane" + name.toLowerCase() + "." + sID, Instance[paneMethods[name] || name]);
        }

        // see if this pane has a 'scrolling-content element'
        initContent(pane, false); // false = do NOT sizeContent() - called later

        if (!isCenter) {
            // call _parseSize AFTER applying pane classes & styles - but before making visible (if hidden)
            // if o.size is auto or not valid, then MEASURE the pane and use that as its 'size'
            size = s.size = _parseSize(pane, o.size);
            minSize = _parseSize(pane, o.minSize) || 1;
            maxSize = _parseSize(pane, o.maxSize) || 100000;
            if (size > 0) size = max(min(size, maxSize), minSize);
            s.autoResize = o.autoResize; // used with percentage sizes

            // state for border-panes
            s.isClosed = false; // true = pane is closed
            s.isSliding = false; // true = pane is currently open by 'sliding' over adjacent panes
            s.isResizing = false; // true = pane is in process of being resized
            s.isHidden = false; // true = pane is hidden - no spacing, resizer or toggler is visible!

            // array for 'pin buttons' whose classNames are auto-updated on pane-open/-close
            if (!s.pins) s.pins = [];
        }
        //	states common to ALL panes
        s.tagName = $P[0].tagName;
        s.edge = pane;		// useful if pane is (or about to be) 'swapped' - easy find out where it is (or is going)
        s.noRoom = false;	// true = pane 'automatically' hidden due to insufficient room - will unhide automatically
        s.isVisible = true;		// false = pane is invisible - closed OR hidden - simplify logic

        // init pane positioning
        setPanePosition(pane);

        // if pane is not visible, 
        if (dir === "horz") // north or south pane
            CSS.height = cssH($P, size);
        else if (dir === "vert") // east or west pane
            CSS.width = cssW($P, size);
        //else if (isCenter) {}

        $P.css(CSS); // apply size -- top, bottom & height will be set by sizeMidPanes
        if (dir != "horz") sizeMidPanes(pane, true); // true = skipCallback

        // if manually adding a pane AFTER layout initialization, then...
        if (state.initialized) {
            initHandles(pane);
            initHotkeys(pane);
        }

        // close or hide the pane if specified in settings
        if (o.initClosed && o.closable && !o.initHidden)
            close(pane, true, true); // true, true = force, noAnimation
        else if (o.initHidden || o.initClosed)
            hide(pane); // will be completely invisible - no resizer or spacing
        else if (!s.noRoom)
            // make the pane visible - in case was initially hidden
            $P.css("display", "block");
        // ELSE setAsOpen() - called later by initHandles()

        // RESET visibility now - pane will appear IF display:block
        $P.css("visibility", "visible");

        // check option for auto-handling of pop-ups & drop-downs
        if (o.showOverflowOnHover)
            $P.hover(allowOverflow, resetOverflow);

        // if manually adding a pane AFTER layout initialization, then...
        if (state.initialized) {
            afterInitPane(pane);
        }
    }

    , afterInitPane = function (pane) {
        var $P = $Ps[pane]
		, s = state[pane]
		, o = options[pane]
        ;
        if (!$P) return;

        // see if there is a directly-nested layout inside this pane
        if ($P.data("layout"))
            refreshChildren(pane, $P.data("layout"));

        // process pane contents and callbacks, and init/resize child-layout if exists
        if (s.isVisible) { // pane is OPEN
            if (state.initialized) // this pane was added AFTER layout was created
                resizeAll(); // will also sizeContent
            else
                sizeContent(pane);

            if (o.triggerEventsOnLoad)
                _runCallbacks("onresize_end", pane);
            else // automatic if onresize called, otherwise call it specifically
                // resize child - IF inner-layout already exists (created before this layout)
                resizeChildren(pane, true); // a previously existing childLayout
        }

        // init childLayouts - even if pane is not visible
        if (o.initChildren && o.children)
            createChildren(pane);
    }

        /**
         * @param {string=}	panes		The pane(s) to process
         */
    , setPanePosition = function (panes) {
        panes = panes ? panes.split(",") : _c.borderPanes;

        // create toggler DIVs for each pane, and set object pointers for them, eg: $R.north = north toggler DIV
        $.each(panes, function (i, pane) {
            var $P = $Ps[pane]
			, $R = $Rs[pane]
			, o = options[pane]
			, s = state[pane]
			, side = _c[pane].side
			, CSS = {}
            ;
            if (!$P) return; // pane does not exist - skip

            // set css-position to account for container borders & padding
            switch (pane) {
                case "north": CSS.top = sC.inset.top;
                    CSS.left = sC.inset.left;
                    CSS.right = sC.inset.right;
                    break;
                case "south": CSS.bottom = sC.inset.bottom;
                    CSS.left = sC.inset.left;
                    CSS.right = sC.inset.right;
                    break;
                case "west": CSS.left = sC.inset.left; // top, bottom & height set by sizeMidPanes()
                    break;
                case "east": CSS.right = sC.inset.right; // ditto
                    break;
                case "center":	// top, left, width & height set by sizeMidPanes()
            }
            // apply position
            $P.css(CSS);

            // update resizer position
            if ($R && s.isClosed)
                $R.css(side, sC.inset[side]);
            else if ($R && !s.isHidden)
                $R.css(side, sC.inset[side] + getPaneSize(pane));
        });
    }

        /**
         * Initialize module objects, styling, size and position for all resize bars and toggler buttons
         *
         * @see  _create()
         * @param {string=}	[panes=""]	The edge(s) to process
         */
    , initHandles = function (panes) {
        panes = panes ? panes.split(",") : _c.borderPanes;

        // create toggler DIVs for each pane, and set object pointers for them, eg: $R.north = north toggler DIV
        $.each(panes, function (i, pane) {
            var $P = $Ps[pane];
            $Rs[pane] = false; // INIT
            $Ts[pane] = false;
            if (!$P) return; // pane does not exist - skip

            var o = options[pane]
			, s = state[pane]
			, c = _c[pane]
			, paneId = o.paneSelector.substr(0, 1) === "#" ? o.paneSelector.substr(1) : ""
			, rClass = o.resizerClass
			, tClass = o.togglerClass
			, spacing = (s.isVisible ? o.spacing_open : o.spacing_closed)
			, _pane = "-" + pane // used for classNames
			, _state = (s.isVisible ? "-open" : "-closed") // used for classNames
			, I = Instance[pane]
				// INIT RESIZER BAR
			, $R = I.resizer = $Rs[pane] = $("<div></div>")
				// INIT TOGGLER BUTTON
			, $T = I.toggler = (o.closable ? $Ts[pane] = $("<div></div>") : false)
            ;

            //if (s.isVisible && o.resizable) ... handled by initResizable
            if (!s.isVisible && o.slidable)
                $R.attr("title", o.tips.Slide).css("cursor", o.sliderCursor);

            $R	// if paneSelector is an ID, then create a matching ID for the resizer, eg: "#paneLeft" => "paneLeft-resizer"
				.attr("id", paneId ? paneId + "-resizer" : "")
				.data({
				    parentLayout: Instance
				, layoutPane: Instance[pane]	// NEW pointer to pane-alias-object
				, layoutEdge: pane
				, layoutRole: "resizer"
				})
				.css(_c.resizers.cssReq).css("zIndex", options.zIndexes.resizer_normal)
				.css(o.applyDemoStyles ? _c.resizers.cssDemo : {}) // add demo styles
				.addClass(rClass + " " + rClass + _pane)
				.hover(addHover, removeHover) // ALWAYS add hover-classes, even if resizing is not enabled - handle with CSS instead
				.hover(onResizerEnter, onResizerLeave) // ALWAYS NEED resizer.mouseleave to balance toggler.mouseenter
				.mousedown($.layout.disableTextSelection)	// prevent text-selection OUTSIDE resizer
				.mouseup($.layout.enableTextSelection)		// not really necessary, but just in case
				.appendTo($N) // append DIV to container
            ;
            if ($.fn.disableSelection)
                $R.disableSelection(); // prevent text-selection INSIDE resizer
            if (o.resizerDblClickToggle)
                $R.bind("dblclick." + sID, toggle);

            if ($T) {
                $T	// if paneSelector is an ID, then create a matching ID for the resizer, eg: "#paneLeft" => "#paneLeft-toggler"
					.attr("id", paneId ? paneId + "-toggler" : "")
					.data({
					    parentLayout: Instance
					, layoutPane: Instance[pane]	// NEW pointer to pane-alias-object
					, layoutEdge: pane
					, layoutRole: "toggler"
					})
					.css(_c.togglers.cssReq) // add base/required styles
					.css(o.applyDemoStyles ? _c.togglers.cssDemo : {}) // add demo styles
					.addClass(tClass + " " + tClass + _pane)
					.hover(addHover, removeHover) // ALWAYS add hover-classes, even if toggling is not enabled - handle with CSS instead
					.bind("mouseenter", onResizerEnter) // NEED toggler.mouseenter because mouseenter MAY NOT fire on resizer
					.appendTo($R) // append SPAN to resizer DIV
                ;
                // ADD INNER-SPANS TO TOGGLER
                if (o.togglerContent_open) // ui-layout-open
                    $("<span>" + o.togglerContent_open + "</span>")
						.data({
						    layoutEdge: pane
						, layoutRole: "togglerContent"
						})
						.data("layoutRole", "togglerContent")
						.data("layoutEdge", pane)
						.addClass("content content-open")
						.css("display", "none")
						.appendTo($T)
                //.hover( addHover, removeHover ) // use ui-layout-toggler-west-hover .content-open instead!
                ;
                if (o.togglerContent_closed) // ui-layout-closed
                    $("<span>" + o.togglerContent_closed + "</span>")
						.data({
						    layoutEdge: pane
						, layoutRole: "togglerContent"
						})
						.addClass("content content-closed")
						.css("display", "none")
						.appendTo($T)
                //.hover( addHover, removeHover ) // use ui-layout-toggler-west-hover .content-closed instead!
                ;
                // ADD TOGGLER.click/.hover
                enableClosable(pane);
            }

            // add Draggable events
            initResizable(pane);

            // ADD CLASSNAMES & SLIDE-BINDINGS - eg: class="resizer resizer-west resizer-open"
            if (s.isVisible)
                setAsOpen(pane);	// onOpen will be called, but NOT onResize
            else {
                setAsClosed(pane);	// onClose will be called
                bindStartSlidingEvents(pane, true); // will enable events IF option is set
            }

        });

        // SET ALL HANDLE DIMENSIONS
        sizeHandles();
    }


        /**
         * Initialize scrolling ui-layout-content div - if exists
         *
         * @see  initPane() - or externally after an Ajax injection
         * @param {string}	pane			The pane to process
         * @param {boolean=}	[resize=true]	Size content after init
         */
    , initContent = function (pane, resize) {
        if (!isInitialized()) return;
        var
			o = options[pane]
		, sel = o.contentSelector
		, I = Instance[pane]
		, $P = $Ps[pane]
		, $C
        ;
        if (sel) $C = I.content = $Cs[pane] = (o.findNestedContent)
			? $P.find(sel).eq(0) // match 1-element only
			: $P.children(sel).eq(0)
        ;
        if ($C && $C.length) {
            $C.data("layoutRole", "content");
            // SAVE original Content CSS
            if (!$C.data("layoutCSS"))
                $C.data("layoutCSS", styles($C, "height"));
            $C.css(_c.content.cssReq);
            if (o.applyDemoStyles) {
                $C.css(_c.content.cssDemo); // add padding & overflow: auto to content-div
                $P.css(_c.content.cssDemoPane); // REMOVE padding/scrolling from pane
            }
            // ensure no vertical scrollbar on pane - will mess up measurements
            if ($P.css("overflowX").match(/(scroll|auto)/)) {
                $P.css("overflow", "hidden");
            }
            state[pane].content = {}; // init content state
            if (resize !== false) sizeContent(pane);
            // sizeContent() is called AFTER init of all elements
        }
        else
            I.content = $Cs[pane] = false;
    }


        /**
         * Add resize-bars to all panes that specify it in options
         * -dependancy: $.fn.resizable - will skip if not found
         *
         * @see  _create()
         * @param {string=}	[panes=""]	The edge(s) to process
         */
    , initResizable = function (panes) {
        var draggingAvailable = $.layout.plugins.draggable
		, side // set in start()
        ;
        panes = panes ? panes.split(",") : _c.borderPanes;

        $.each(panes, function (idx, pane) {
            var o = options[pane];
            if (!draggingAvailable || !$Ps[pane] || !o.resizable) {
                o.resizable = false;
                return true; // skip to next
            }

            var s = state[pane]
			, z = options.zIndexes
			, c = _c[pane]
			, side = c.dir == "horz" ? "top" : "left"
			, $P = $Ps[pane]
			, $R = $Rs[pane]
			, base = o.resizerClass
			, lastPos = 0 // used when live-resizing
			, r, live // set in start because may change
			//	'drag' classes are applied to the ORIGINAL resizer-bar while dragging is in process
			, resizerClass = base + "-drag"				// resizer-drag
			, resizerPaneClass = base + "-" + pane + "-drag"		// resizer-north-drag
			//	'helper' class is applied to the CLONED resizer-bar while it is being dragged
			, helperClass = base + "-dragging"			// resizer-dragging
			, helperPaneClass = base + "-" + pane + "-dragging" // resizer-north-dragging
			, helperLimitClass = base + "-dragging-limit"	// resizer-drag
			, helperPaneLimitClass = base + "-" + pane + "-dragging-limit"	// resizer-north-drag
			, helperClassesSet = false 					// logic var
            ;

            if (!s.isClosed)
                $R.attr("title", o.tips.Resize)
				  .css("cursor", o.resizerCursor); // n-resize, s-resize, etc

            $R.draggable({
                containment: $N[0] // limit resizing to layout container
			, axis: (c.dir == "horz" ? "y" : "x") // limit resizing to horz or vert axis
			, delay: 0
			, distance: 1
			, grid: o.resizingGrid
                //	basic format for helper - style it using class: .ui-draggable-dragging
			, helper: "clone"
			, opacity: o.resizerDragOpacity
			, addClasses: false // avoid ui-state-disabled class when disabled
                //,	iframeFix:		o.draggableIframeFix // TODO: consider using when bug is fixed
			, zIndex: z.resizer_drag

			, start: function (e, ui) {
			    // REFRESH options & state pointers in case we used swapPanes
			    o = options[pane];
			    s = state[pane];
			    // re-read options
			    live = o.livePaneResizing;

			    // ondrag_start callback - will CANCEL hide if returns false
			    // TODO: dragging CANNOT be cancelled like this, so see if there is a way?
			    if (false === _runCallbacks("ondrag_start", pane)) return false;

			    s.isResizing = true; // prevent pane from closing while resizing
			    state.paneResizing = pane; // easy to see if ANY pane is resizing
			    timer.clear(pane + "_closeSlider"); // just in case already triggered

			    // SET RESIZER LIMITS - used in drag()
			    setSizeLimits(pane); // update pane/resizer state
			    r = s.resizerPosition;
			    lastPos = ui.position[side]

			    $R.addClass(resizerClass + " " + resizerPaneClass); // add drag classes
			    helperClassesSet = false; // reset logic var - see drag()

			    // MASK PANES CONTAINING IFRAMES, APPLETS OR OTHER TROUBLESOME ELEMENTS
			    showMasks(pane, { resizing: true });
			}

			, drag: function (e, ui) {
			    if (!helperClassesSet) { // can only add classes after clone has been added to the DOM
			        //$(".ui-draggable-dragging")
			        ui.helper
                        .addClass(helperClass + " " + helperPaneClass) // add helper classes
                        .css({ right: "auto", bottom: "auto" })	// fix dir="rtl" issue
                        .children().css("visibility", "hidden")	// hide toggler inside dragged resizer-bar
			        ;
			        helperClassesSet = true;
			        // draggable bug!? RE-SET zIndex to prevent E/W resize-bar showing through N/S pane!
			        if (s.isSliding) $Ps[pane].css("zIndex", z.pane_sliding);
			    }
			    // CONTAIN RESIZER-BAR TO RESIZING LIMITS
			    var limit = 0;
			    if (ui.position[side] < r.min) {
			        ui.position[side] = r.min;
			        limit = -1;
			    }
			    else if (ui.position[side] > r.max) {
			        ui.position[side] = r.max;
			        limit = 1;
			    }
			    // ADD/REMOVE dragging-limit CLASS
			    if (limit) {
			        ui.helper.addClass(helperLimitClass + " " + helperPaneLimitClass); // at dragging-limit
			        window.defaultStatus = (limit > 0 && pane.match(/(north|west)/)) || (limit < 0 && pane.match(/(south|east)/)) ? o.tips.maxSizeWarning : o.tips.minSizeWarning;
			    }
			    else {
			        ui.helper.removeClass(helperLimitClass + " " + helperPaneLimitClass); // not at dragging-limit
			        window.defaultStatus = "";
			    }
			    // DYNAMICALLY RESIZE PANES IF OPTION ENABLED
			    // won't trigger unless resizer has actually moved!
			    if (live && Math.abs(ui.position[side] - lastPos) >= o.liveResizingTolerance) {
			        lastPos = ui.position[side];
			        resizePanes(e, ui, pane)
			    }
			}

			, stop: function (e, ui) {
			    $('body').enableSelection(); // RE-ENABLE TEXT SELECTION
			    window.defaultStatus = ""; // clear 'resizing limit' message from statusbar
			    $R.removeClass(resizerClass + " " + resizerPaneClass); // remove drag classes from Resizer
			    s.isResizing = false;
			    state.paneResizing = false; // easy to see if ANY pane is resizing
			    resizePanes(e, ui, pane, true); // true = resizingDone
			}

            });
        });

        /**
		 * resizePanes
		 *
		 * Sub-routine called from stop() - and drag() if livePaneResizing
		 *
		 * @param {!Object}		evt
		 * @param {!Object}		ui
		 * @param {string}		pane
		 * @param {boolean=}		[resizingDone=false]
		 */
        var resizePanes = function (evt, ui, pane, resizingDone) {
            var dragPos = ui.position
			, c = _c[pane]
			, o = options[pane]
			, s = state[pane]
			, resizerPos
            ;
            switch (pane) {
                case "north": resizerPos = dragPos.top; break;
                case "west": resizerPos = dragPos.left; break;
                case "south": resizerPos = sC.layoutHeight - dragPos.top - o.spacing_open; break;
                case "east": resizerPos = sC.layoutWidth - dragPos.left - o.spacing_open; break;
            };
            // remove container margin from resizer position to get the pane size
            var newSize = resizerPos - sC.inset[c.side];

            // Disable OR Resize Mask(s) created in drag.start
            if (!resizingDone) {
                // ensure we meet liveResizingTolerance criteria
                if (Math.abs(newSize - s.size) < o.liveResizingTolerance)
                    return; // SKIP resize this time
                // resize the pane
                manualSizePane(pane, newSize, false, true); // true = noAnimation
                sizeMasks(); // resize all visible masks
            }
            else { // resizingDone
                // ondrag_end callback
                if (false !== _runCallbacks("ondrag_end", pane))
                    manualSizePane(pane, newSize, false, true); // true = noAnimation
                hideMasks(true); // true = force hiding all masks even if one is 'sliding'
                if (s.isSliding) // RE-SHOW 'object-masks' so objects won't show through sliding pane
                    showMasks(pane, { resizing: true });
            }
        };
    }

        /**
         *	sizeMask
         *
         *	Needed to overlay a DIV over an IFRAME-pane because mask CANNOT be *inside* the pane
         *	Called when mask created, and during livePaneResizing
         */
    , sizeMask = function () {
        var $M = $(this)
		, pane = $M.data("layoutMask") // eg: "west"
		, s = state[pane]
        ;
        // only masks over an IFRAME-pane need manual resizing
        if (s.tagName == "IFRAME" && s.isVisible) // no need to mask closed/hidden panes
            $M.css({
                top: s.offsetTop
			, left: s.offsetLeft
			, width: s.outerWidth
			, height: s.outerHeight
            });
        /* ALT Method...
		var $P = $Ps[pane];
		$M.css( $P.position() ).css({ width: $P[0].offsetWidth, height: $P[0].offsetHeight });
		*/
    }
    , sizeMasks = function () {
        $Ms.each(sizeMask); // resize all 'visible' masks
    }

        /**
         * @param {string}	pane		The pane being resized, animated or isSliding
         * @param {Object=}	[args]		(optional) Options: which masks to apply, and to which panes
         */
    , showMasks = function (pane, args) {
        var c = _c[pane]
		, panes = ["center"]
		, z = options.zIndexes
		, a = $.extend({
		    objectsOnly: false
					, animation: false
					, resizing: true
					, sliding: state[pane].isSliding
		}, args)
		, o, s
        ;
        if (a.resizing)
            panes.push(pane);
        if (a.sliding)
            panes.push(_c.oppositeEdge[pane]); // ADD the oppositeEdge-pane

        if (c.dir === "horz") {
            panes.push("west");
            panes.push("east");
        }

        $.each(panes, function (i, p) {
            s = state[p];
            o = options[p];
            if (s.isVisible && (o.maskObjects || (!a.objectsOnly && o.maskContents))) {
                getMasks(p).each(function () {
                    sizeMask.call(this);
                    this.style.zIndex = s.isSliding ? z.pane_sliding + 1 : z.pane_normal + 1
                    this.style.display = "block";
                });
            }
        });
    }

        /**
         * @param {boolean=}	force		Hide masks even if a pane is sliding
         */
    , hideMasks = function (force) {
        // ensure no pane is resizing - could be a timing issue
        if (force || !state.paneResizing) {
            $Ms.hide(); // hide ALL masks
        }
            // if ANY pane is sliding, then DO NOT remove masks from panes with maskObjects enabled
        else if (!force && !$.isEmptyObject(state.panesSliding)) {
            var i = $Ms.length - 1
			, p, $M;
            for (; i >= 0; i--) {
                $M = $Ms.eq(i);
                p = $M.data("layoutMask");
                if (!options[p].maskObjects) {
                    $M.hide();
                }
            }
        }
    }

        /**
         * @param {string}	pane
         */
    , getMasks = function (pane) {
        var $Masks = $([])
		, $M, i = 0, c = $Ms.length
        ;
        for (; i < c; i++) {
            $M = $Ms.eq(i);
            if ($M.data("layoutMask") === pane)
                $Masks = $Masks.add($M);
        }
        if ($Masks.length)
            return $Masks;
        else
            return createMasks(pane);
    }

        /**
         * createMasks
         *
         * Generates both DIV (ALWAYS used) and IFRAME (optional) elements as masks
         * An IFRAME mask is created *under* the DIV when maskObjects=true, because a DIV cannot mask an applet
         *
         * @param {string}	pane
         */
    , createMasks = function (pane) {
        var
			$P = $Ps[pane]
		, s = state[pane]
		, o = options[pane]
		, z = options.zIndexes
		, isIframe, el, $M, css, i
        ;
        if (!o.maskContents && !o.maskObjects) return $([]);
        // if o.maskObjects=true, then loop TWICE to create BOTH kinds of mask, else only create a DIV
        for (i = 0; i < (o.maskObjects ? 2 : 1) ; i++) {
            isIframe = o.maskObjects && i == 0;
            el = document.createElement(isIframe ? "iframe" : "div");
            $M = $(el).data("layoutMask", pane); // add data to relate mask to pane
            el.className = "ui-layout-mask ui-layout-mask-" + pane; // for user styling
            css = el.style;
            // Both DIVs and IFRAMES
            css.background = "#FFF";
            css.position = "absolute";
            css.display = "block";
            if (isIframe) { // IFRAME-only props
                el.src = "about:blank";
                el.frameborder = 0;
                css.border = 0;
                css.opacity = 0;
                css.filter = "Alpha(Opacity='0')";
                //el.allowTransparency = true; - for IE, but breaks masking ability!
            }
            else { // DIV-only props
                css.opacity = 0.001;
                css.filter = "Alpha(Opacity='1')";
            }
            // if pane IS an IFRAME, then must mask the pane itself
            if (s.tagName == "IFRAME") {
                // NOTE sizing done by a subroutine so can be called during live-resizing
                css.zIndex = z.pane_normal + 1; // 1-higher than pane
                $N.append(el); // append to LAYOUT CONTAINER
            }
                // otherwise put masks *inside the pane* to mask its contents
            else {
                $M.addClass("ui-layout-mask-inside-pane");
                css.zIndex = o.maskZindex || z.content_mask; // usually 1, but customizable
                css.top = 0;
                css.left = 0;
                css.width = "100%";
                css.height = "100%";
                $P.append(el); // append INSIDE pane element
            }
            // add Mask to cached array so can be resized & reused
            $Ms = $Ms.add(el);
        }
        return $Ms;
    }


        /**
         * Destroy this layout and reset all elements
         *
         * @param {boolean=}	[destroyChildren=false]		Destory Child-Layouts first?
         */
    , destroy = function (evt_or_destroyChildren, destroyChildren) {
        // UNBIND layout events and remove global object
        $(window).unbind("." + sID);		// resize & unload
        $(document).unbind("." + sID);	// keyDown (hotkeys)

        if (typeof evt_or_destroyChildren === "object")
            // stopPropagation if called by trigger("layoutdestroy") - use evtPane utility 
            evtPane(evt_or_destroyChildren);
        else // no event, so transfer 1st param to destroyChildren param
            destroyChildren = evt_or_destroyChildren;

        // need to look for parent layout BEFORE we remove the container data, else skips a level
        //var parentPane = Instance.hasParentLayout ? $.layout.getParentPaneInstance( $N ) : null;

        // reset layout-container
        $N.clearQueue()
			.removeData("layout")
			.removeData("layoutContainer")
			.removeClass(options.containerClass)
			.unbind("." + sID) // remove ALL Layout events
        ;

        // remove all mask elements that have been created
        $Ms.remove();

        // loop all panes to remove layout classes, attributes and bindings
        $.each(_c.allPanes, function (i, pane) {
            removePane(pane, false, true, destroyChildren); // true = skipResize
        });

        // do NOT reset container CSS if is a 'pane' (or 'content') in an outer-layout - ie, THIS layout is 'nested'
        var css = "layoutCSS";
        if ($N.data(css) && !$N.data("layoutRole")) // RESET CSS
            $N.css($N.data(css)).removeData(css);

        // for full-page layouts, also reset the <HTML> CSS
        if (sC.tagName === "BODY" && ($N = $("html")).data(css)) // RESET <HTML> CSS
            $N.css($N.data(css)).removeData(css);

        // trigger plugins for this layout, if there are any
        runPluginCallbacks(Instance, $.layout.onDestroy);

        // trigger state-management and onunload callback
        unload();

        // clear the Instance of everything except for container & options (so could recreate)
        // RE-CREATE: myLayout = myLayout.container.layout( myLayout.options );
        for (var n in Instance)
            if (!n.match(/^(container|options)$/)) delete Instance[n];
        // add a 'destroyed' flag to make it easy to check
        Instance.destroyed = true;

        // if this is a child layout, CLEAR the child-pointer in the parent
        /* for now the pointer REMAINS, but with only container, options and destroyed keys
		if (parentPane) {
			var layout	= parentPane.pane.data("parentLayout")
			,	key		= layout.options.instanceKey || 'error';
			// THIS SYNTAX MAY BE WRONG!
			parentPane.children[key] = layout.children[ parentPane.name ].children[key] = null;
		}
		*/

        return Instance; // for coding convenience
    }

        /**
         * Remove a pane from the layout - subroutine of destroy()
         *
         * @see  destroy()
         * @param {(string|Object)}	evt_or_pane			The pane to process
         * @param {boolean=}			[remove=false]		Remove the DOM element?
         * @param {boolean=}			[skipResize=false]	Skip calling resizeAll()?
         * @param {boolean=}			[destroyChild=true]	Destroy Child-layouts? If not passed, obeys options setting
         */
    , removePane = function (evt_or_pane, remove, skipResize, destroyChild) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, $P = $Ps[pane]
		, $C = $Cs[pane]
		, $R = $Rs[pane]
		, $T = $Ts[pane]
        ;
        // NOTE: elements can still exist even after remove()
        //		so check for missing data(), which is cleared by removed()
        if ($P && $.isEmptyObject($P.data())) $P = false;
        if ($C && $.isEmptyObject($C.data())) $C = false;
        if ($R && $.isEmptyObject($R.data())) $R = false;
        if ($T && $.isEmptyObject($T.data())) $T = false;

        if ($P) $P.stop(true, true);

        var o = options[pane]
		, s = state[pane]
		, d = "layout"
		, css = "layoutCSS"
		, pC = children[pane]
		, hasChildren = $.isPlainObject(pC) && !$.isEmptyObject(pC)
		, destroy = destroyChild !== undefined ? destroyChild : o.destroyChildren
        ;
        // FIRST destroy the child-layout(s)
        if (hasChildren && destroy) {
            $.each(pC, function (key, child) {
                if (!child.destroyed)
                    child.destroy(true);// tell child-layout to destroy ALL its child-layouts too
                if (child.destroyed)	// destroy was successful
                    delete pC[key];
            });
            // if no more children, remove the children hash
            if ($.isEmptyObject(pC)) {
                pC = children[pane] = null; // clear children hash
                hasChildren = false;
            }
        }

        // Note: can't 'remove' a pane element with non-destroyed children
        if ($P && remove && !hasChildren)
            $P.remove(); // remove the pane-element and everything inside it
        else if ($P && $P[0]) {
            //	create list of ALL pane-classes that need to be removed
            var root = o.paneClass // default="ui-layout-pane"
			, pRoot = root + "-" + pane // eg: "ui-layout-pane-west"
			, _open = "-open"
			, _sliding = "-sliding"
			, _closed = "-closed"
			, classes = [root, root + _open, root + _closed, root + _sliding,		// generic classes
							pRoot, pRoot + _open, pRoot + _closed, pRoot + _sliding]	// pane-specific classes
            ;
            $.merge(classes, getHoverClasses($P, true)); // ADD hover-classes
            // remove all Layout classes from pane-element
            $P.removeClass(classes.join(" ")) // remove ALL pane-classes
				.removeData("parentLayout")
				.removeData("layoutPane")
				.removeData("layoutRole")
				.removeData("layoutEdge")
				.removeData("autoHidden")	// in case set
				.unbind("." + sID) // remove ALL Layout events
            // TODO: remove these extra unbind commands when jQuery is fixed
            //.unbind("mouseenter"+ sID)
            //.unbind("mouseleave"+ sID)
            ;
            // do NOT reset CSS if this pane/content is STILL the container of a nested layout!
            // the nested layout will reset its 'container' CSS when/if it is destroyed
            if (hasChildren && $C) {
                // a content-div may not have a specific width, so give it one to contain the Layout
                $C.width($C.width());
                $.each(pC, function (key, child) {
                    child.resizeAll(); // resize the Layout
                });
            }
            else if ($C)
                $C.css($C.data(css)).removeData(css).removeData("layoutRole");
            // remove pane AFTER content in case there was a nested layout
            if (!$P.data(d))
                $P.css($P.data(css)).removeData(css);
        }

        // REMOVE pane resizer and toggler elements
        if ($T) $T.remove();
        if ($R) $R.remove();

        // CLEAR all pointers and state data
        Instance[pane] = $Ps[pane] = $Cs[pane] = $Rs[pane] = $Ts[pane] = false;
        s = { removed: true };

        if (!skipResize)
            resizeAll();
    }


    /*
     * ###########################
     *	   ACTION METHODS
     * ###########################
     */

        /**
         * @param {string}	pane
         */
    , _hidePane = function (pane) {
        var $P = $Ps[pane]
		, o = options[pane]
		, s = $P[0].style
        ;
        if (o.useOffscreenClose) {
            if (!$P.data(_c.offscreenReset))
                $P.data(_c.offscreenReset, { left: s.left, right: s.right });
            $P.css(_c.offscreenCSS);
        }
        else
            $P.hide().removeData(_c.offscreenReset);
    }

        /**
         * @param {string}	pane
         */
    , _showPane = function (pane) {
        var $P = $Ps[pane]
		, o = options[pane]
		, off = _c.offscreenCSS
		, old = $P.data(_c.offscreenReset)
		, s = $P[0].style
        ;
        $P.show() // ALWAYS show, just in case
			.removeData(_c.offscreenReset);
        if (o.useOffscreenClose && old) {
            if (s.left == off.left)
                s.left = old.left;
            if (s.right == off.right)
                s.right = old.right;
        }
    }


        /**
         * Completely 'hides' a pane, including its spacing - as if it does not exist
         * The pane is not actually 'removed' from the source, so can use 'show' to un-hide it
         *
         * @param {(string|Object)}	evt_or_pane			The pane being hidden, ie: north, south, east, or west
         * @param {boolean=}			[noAnimation=false]	
         */
    , hide = function (evt_or_pane, noAnimation) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, o = options[pane]
		, s = state[pane]
		, $P = $Ps[pane]
		, $R = $Rs[pane]
        ;
        if (pane === "center" || !$P || s.isHidden) return; // pane does not exist OR is already hidden

        // onhide_start callback - will CANCEL hide if returns false
        if (state.initialized && false === _runCallbacks("onhide_start", pane)) return;

        s.isSliding = false; // just in case
        delete state.panesSliding[pane];

        // now hide the elements
        if ($R) $R.hide(); // hide resizer-bar
        if (!state.initialized || s.isClosed) {
            s.isClosed = true; // to trigger open-animation on show()
            s.isHidden = true;
            s.isVisible = false;
            if (!state.initialized)
                _hidePane(pane); // no animation when loading page
            sizeMidPanes(_c[pane].dir === "horz" ? "" : "center");
            if (state.initialized || o.triggerEventsOnLoad)
                _runCallbacks("onhide_end", pane);
        }
        else {
            s.isHiding = true; // used by onclose
            close(pane, false, noAnimation); // adjust all panes to fit
        }
    }

        /**
         * Show a hidden pane - show as 'closed' by default unless openPane = true
         *
         * @param {(string|Object)}	evt_or_pane			The pane being opened, ie: north, south, east, or west
         * @param {boolean=}			[openPane=false]
         * @param {boolean=}			[noAnimation=false]
         * @param {boolean=}			[noAlert=false]
         */
    , show = function (evt_or_pane, openPane, noAnimation, noAlert) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, o = options[pane]
		, s = state[pane]
		, $P = $Ps[pane]
		, $R = $Rs[pane]
        ;
        if (pane === "center" || !$P || !s.isHidden) return; // pane does not exist OR is not hidden

        // onshow_start callback - will CANCEL show if returns false
        if (false === _runCallbacks("onshow_start", pane)) return;

        s.isShowing = true; // used by onopen/onclose
        //s.isHidden  = false; - will be set by open/close - if not cancelled
        s.isSliding = false; // just in case
        delete state.panesSliding[pane];

        // now show the elements
        //if ($R) $R.show(); - will be shown by open/close
        if (openPane === false)
            close(pane, true); // true = force
        else
            open(pane, false, noAnimation, noAlert); // adjust all panes to fit
    }


        /**
         * Toggles a pane open/closed by calling either open or close
         *
         * @param {(string|Object)}	evt_or_pane		The pane being toggled, ie: north, south, east, or west
         * @param {boolean=}			[slide=false]
         */
    , toggle = function (evt_or_pane, slide) {
        if (!isInitialized()) return;
        var evt = evtObj(evt_or_pane)
		, pane = evtPane.call(this, evt_or_pane)
		, s = state[pane]
        ;
        if (evt) // called from to $R.dblclick OR triggerPaneEvent
            evt.stopImmediatePropagation();
        if (s.isHidden)
            show(pane); // will call 'open' after unhiding it
        else if (s.isClosed)
            open(pane, !!slide);
        else
            close(pane);
    }


        /**
         * Utility method used during init or other auto-processes
         *
         * @param {string}	pane   The pane being closed
         * @param {boolean=}	[setHandles=false]
         */
    , _closePane = function (pane, setHandles) {
        var
			$P = $Ps[pane]
		, s = state[pane]
        ;
        _hidePane(pane);
        s.isClosed = true;
        s.isVisible = false;
        if (setHandles) setAsClosed(pane);
    }

        /**
         * Close the specified pane (animation optional), and resize all other panes as needed
         *
         * @param {(string|Object)}	evt_or_pane			The pane being closed, ie: north, south, east, or west
         * @param {boolean=}			[force=false]
         * @param {boolean=}			[noAnimation=false]
         * @param {boolean=}			[skipCallback=false]
         */
    , close = function (evt_or_pane, force, noAnimation, skipCallback) {
        var pane = evtPane.call(this, evt_or_pane);
        if (pane === "center") return; // validate
        // if pane has been initialized, but NOT the complete layout, close pane instantly
        if (!state.initialized && $Ps[pane]) {
            _closePane(pane, true); // INIT pane as closed
            return;
        }
        if (!isInitialized()) return;

        var
			$P = $Ps[pane]
		, $R = $Rs[pane]
		, $T = $Ts[pane]
		, o = options[pane]
		, s = state[pane]
		, c = _c[pane]
		, doFX, isShowing, isHiding, wasSliding;

        // QUEUE in case another action/animation is in progress
        $N.queue(function (queueNext) {

            if (!$P
			|| (!o.closable && !s.isShowing && !s.isHiding)	// invalid request // (!o.resizable && !o.closable) ???
			|| (!force && s.isClosed && !s.isShowing)			// already closed
			) return queueNext();

            // onclose_start callback - will CANCEL hide if returns false
            // SKIP if just 'showing' a hidden pane as 'closed'
            var abort = !s.isShowing && false === _runCallbacks("onclose_start", pane);

            // transfer logic vars to temp vars
            isShowing = s.isShowing;
            isHiding = s.isHiding;
            wasSliding = s.isSliding;
            // now clear the logic vars (REQUIRED before aborting)
            delete s.isShowing;
            delete s.isHiding;

            if (abort) return queueNext();

            doFX = !noAnimation && !s.isClosed && (o.fxName_close != "none");
            s.isMoving = true;
            s.isClosed = true;
            s.isVisible = false;
            // update isHidden BEFORE sizing panes
            if (isHiding) s.isHidden = true;
            else if (isShowing) s.isHidden = false;

            if (s.isSliding) // pane is being closed, so UNBIND trigger events
                bindStopSlidingEvents(pane, false); // will set isSliding=false
            else // resize panes adjacent to this one
                sizeMidPanes(_c[pane].dir === "horz" ? "" : "center", false); // false = NOT skipCallback

            // if this pane has a resizer bar, move it NOW - before animation
            setAsClosed(pane);

            // CLOSE THE PANE
            if (doFX) { // animate the close
                lockPaneForFX(pane, true);	// need to set left/top so animation will work
                $P.hide(o.fxName_close, o.fxSettings_close, o.fxSpeed_close, function () {
                    lockPaneForFX(pane, false); // undo
                    if (s.isClosed) close_2();
                    queueNext();
                });
            }
            else { // hide the pane without animation
                _hidePane(pane);
                close_2();
                queueNext();
            };
        });

        // SUBROUTINE
        function close_2() {
            s.isMoving = false;
            bindStartSlidingEvents(pane, true); // will enable if o.slidable = true

            // if opposite-pane was autoClosed, see if it can be autoOpened now
            var altPane = _c.oppositeEdge[pane];
            if (state[altPane].noRoom) {
                setSizeLimits(altPane);
                makePaneFit(altPane);
            }

            if (!skipCallback && (state.initialized || o.triggerEventsOnLoad)) {
                // onclose callback - UNLESS just 'showing' a hidden pane as 'closed'
                if (!isShowing) _runCallbacks("onclose_end", pane);
                // onhide OR onshow callback
                if (isShowing) _runCallbacks("onshow_end", pane);
                if (isHiding) _runCallbacks("onhide_end", pane);
            }
        }
    }

        /**
         * @param {string}	pane	The pane just closed, ie: north, south, east, or west
         */
    , setAsClosed = function (pane) {
        if (!$Rs[pane]) return; // handles not initialized yet!
        var
			$P = $Ps[pane]
		, $R = $Rs[pane]
		, $T = $Ts[pane]
		, o = options[pane]
		, s = state[pane]
		, side = _c[pane].side
		, rClass = o.resizerClass
		, tClass = o.togglerClass
		, _pane = "-" + pane // used for classNames
		, _open = "-open"
		, _sliding = "-sliding"
		, _closed = "-closed"
        ;
        $R
			.css(side, sC.inset[side]) // move the resizer
			.removeClass(rClass + _open + " " + rClass + _pane + _open)
			.removeClass(rClass + _sliding + " " + rClass + _pane + _sliding)
			.addClass(rClass + _closed + " " + rClass + _pane + _closed)
        ;
        // handle already-hidden panes in case called by swap() or a similar method 
        if (s.isHidden) $R.hide(); // hide resizer-bar 

        // DISABLE 'resizing' when closed - do this BEFORE bindStartSlidingEvents?
        if (o.resizable && $.layout.plugins.draggable)
            $R
				.draggable("disable")
				.removeClass("ui-state-disabled") // do NOT apply disabled styling - not suitable here
				.css("cursor", "default")
				.attr("title", "")
        ;

        // if pane has a toggler button, adjust that too
        if ($T) {
            $T
				.removeClass(tClass + _open + " " + tClass + _pane + _open)
				.addClass(tClass + _closed + " " + tClass + _pane + _closed)
				.attr("title", o.tips.Open) // may be blank
            ;
            // toggler-content - if exists
            $T.children(".content-open").hide();
            $T.children(".content-closed").css("display", "block");
        }

        // sync any 'pin buttons'
        syncPinBtns(pane, false);

        if (state.initialized) {
            // resize 'length' and position togglers for adjacent panes
            sizeHandles();
        }
    }

        /**
         * Open the specified pane (animation optional), and resize all other panes as needed
         *
         * @param {(string|Object)}	evt_or_pane			The pane being opened, ie: north, south, east, or west
         * @param {boolean=}			[slide=false]
         * @param {boolean=}			[noAnimation=false]
         * @param {boolean=}			[noAlert=false]
         */
    , open = function (evt_or_pane, slide, noAnimation, noAlert) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, $P = $Ps[pane]
		, $R = $Rs[pane]
		, $T = $Ts[pane]
		, o = options[pane]
		, s = state[pane]
		, c = _c[pane]
		, doFX, isShowing
        ;
        if (pane === "center") return; // validate
        // QUEUE in case another action/animation is in progress
        $N.queue(function (queueNext) {

            if (!$P
			|| (!o.resizable && !o.closable && !s.isShowing)	// invalid request
			|| (s.isVisible && !s.isSliding)					// already open
			) return queueNext();

            // pane can ALSO be unhidden by just calling show(), so handle this scenario
            if (s.isHidden && !s.isShowing) {
                queueNext(); // call before show() because it needs the queue free
                show(pane, true);
                return;
            }

            if (s.autoResize && s.size != o.size) // resize pane to original size set in options
                sizePane(pane, o.size, true, true, true); // true=skipCallback/noAnimation/forceResize
            else
                // make sure there is enough space available to open the pane
                setSizeLimits(pane, slide);

            // onopen_start callback - will CANCEL open if returns false
            var cbReturn = _runCallbacks("onopen_start", pane);

            if (cbReturn === "abort")
                return queueNext();

            // update pane-state again in case options were changed in onopen_start
            if (cbReturn !== "NC") // NC = "No Callback"
                setSizeLimits(pane, slide);

            if (s.minSize > s.maxSize) { // INSUFFICIENT ROOM FOR PANE TO OPEN!
                syncPinBtns(pane, false); // make sure pin-buttons are reset
                if (!noAlert && o.tips.noRoomToOpen) {
                    if (window.console) {
                        console.log(o.tips.noRoomToOpen);
                    }
                    else {
                        alert(o.tips.noRoomToOpen);
                    }
                }
                return queueNext(); // ABORT
            }

            if (slide) // START Sliding - will set isSliding=true
                bindStopSlidingEvents(pane, true); // BIND trigger events to close sliding-pane
            else if (s.isSliding) // PIN PANE (stop sliding) - open pane 'normally' instead
                bindStopSlidingEvents(pane, false); // UNBIND trigger events - will set isSliding=false
            else if (o.slidable)
                bindStartSlidingEvents(pane, false); // UNBIND trigger events

            s.noRoom = false; // will be reset by makePaneFit if 'noRoom'
            makePaneFit(pane);

            // transfer logic var to temp var
            isShowing = s.isShowing;
            // now clear the logic var
            delete s.isShowing;

            doFX = !noAnimation && s.isClosed && (o.fxName_open != "none");
            s.isMoving = true;
            s.isVisible = true;
            s.isClosed = false;
            // update isHidden BEFORE sizing panes - WHY??? Old?
            if (isShowing) s.isHidden = false;

            if (doFX) { // ANIMATE
                // mask adjacent panes with objects
                lockPaneForFX(pane, true);	// need to set left/top so animation will work
                $P.show(o.fxName_open, o.fxSettings_open, o.fxSpeed_open, function () {
                    lockPaneForFX(pane, false); // undo
                    if (s.isVisible) open_2(); // continue
                    queueNext();
                });
            }
            else { // no animation
                _showPane(pane);// just show pane and...
                open_2();		// continue
                queueNext();
            };
        });

        // SUBROUTINE
        function open_2() {
            s.isMoving = false;

            // cure iframe display issues
            _fixIframe(pane);

            // NOTE: if isSliding, then other panes are NOT 'resized'
            if (!s.isSliding) { // resize all panes adjacent to this one
                sizeMidPanes(_c[pane].dir == "vert" ? "center" : "", false); // false = NOT skipCallback
            }

            // set classes, position handles and execute callbacks...
            setAsOpen(pane);
        };

    }

        /**
         * @param {string}	pane		The pane just opened, ie: north, south, east, or west
         * @param {boolean=}	[skipCallback=false]
         */
    , setAsOpen = function (pane, skipCallback) {
        var
			$P = $Ps[pane]
		, $R = $Rs[pane]
		, $T = $Ts[pane]
		, o = options[pane]
		, s = state[pane]
		, side = _c[pane].side
		, rClass = o.resizerClass
		, tClass = o.togglerClass
		, _pane = "-" + pane // used for classNames
		, _open = "-open"
		, _closed = "-closed"
		, _sliding = "-sliding"
        ;
        $R
			.css(side, sC.inset[side] + getPaneSize(pane)) // move the resizer
			.removeClass(rClass + _closed + " " + rClass + _pane + _closed)
			.addClass(rClass + _open + " " + rClass + _pane + _open)
        ;
        if (s.isSliding)
            $R.addClass(rClass + _sliding + " " + rClass + _pane + _sliding)
        else // in case 'was sliding'
            $R.removeClass(rClass + _sliding + " " + rClass + _pane + _sliding)

        removeHover(0, $R); // remove hover classes
        if (o.resizable && $.layout.plugins.draggable)
            $R.draggable("enable")
				.css("cursor", o.resizerCursor)
				.attr("title", o.tips.Resize);
        else if (!s.isSliding)
            $R.css("cursor", "default"); // n-resize, s-resize, etc

        // if pane also has a toggler button, adjust that too
        if ($T) {
            $T.removeClass(tClass + _closed + " " + tClass + _pane + _closed)
				.addClass(tClass + _open + " " + tClass + _pane + _open)
				.attr("title", o.tips.Close); // may be blank
            removeHover(0, $T); // remove hover classes
            // toggler-content - if exists
            $T.children(".content-closed").hide();
            $T.children(".content-open").css("display", "block");
        }

        // sync any 'pin buttons'
        syncPinBtns(pane, !s.isSliding);

        // update pane-state dimensions - BEFORE resizing content
        $.extend(s, elDims($P));

        if (state.initialized) {
            // resize resizer & toggler sizes for all panes
            sizeHandles();
            // resize content every time pane opens - to be sure
            sizeContent(pane, true); // true = remeasure headers/footers, even if 'pane.isMoving'
        }

        if (!skipCallback && (state.initialized || o.triggerEventsOnLoad) && $P.is(":visible")) {
            // onopen callback
            _runCallbacks("onopen_end", pane);
            // onshow callback - TODO: should this be here?
            if (s.isShowing) _runCallbacks("onshow_end", pane);

            // ALSO call onresize because layout-size *may* have changed while pane was closed
            if (state.initialized)
                _runCallbacks("onresize_end", pane);
        }

        // TODO: Somehow sizePane("north") is being called after this point???
    }


        /**
         * slideOpen / slideClose / slideToggle
         *
         * Pass-though methods for sliding
         */
    , slideOpen = function (evt_or_pane) {
        if (!isInitialized()) return;
        var evt = evtObj(evt_or_pane)
		, pane = evtPane.call(this, evt_or_pane)
		, s = state[pane]
		, delay = options[pane].slideDelay_open
        ;
        if (pane === "center") return; // validate
        // prevent event from triggering on NEW resizer binding created below
        if (evt) evt.stopImmediatePropagation();

        if (s.isClosed && evt && evt.type === "mouseenter" && delay > 0)
            // trigger = mouseenter - use a delay
            timer.set(pane + "_openSlider", open_NOW, delay);
        else
            open_NOW(); // will unbind events if is already open

        /**
		 * SUBROUTINE for timed open
		 */
        function open_NOW() {
            if (!s.isClosed) // skip if no longer closed!
                bindStopSlidingEvents(pane, true); // BIND trigger events to close sliding-pane
            else if (!s.isMoving)
                open(pane, true); // true = slide - open() will handle binding
        };
    }

    , slideClose = function (evt_or_pane) {
        if (!isInitialized()) return;
        var evt = evtObj(evt_or_pane)
		, pane = evtPane.call(this, evt_or_pane)
		, o = options[pane]
		, s = state[pane]
		, delay = s.isMoving ? 1000 : 300 // MINIMUM delay - option may override
        ;
        if (pane === "center") return; // validate
        if (s.isClosed || s.isResizing)
            return; // skip if already closed OR in process of resizing
        else if (o.slideTrigger_close === "click")
            close_NOW(); // close immediately onClick
        else if (o.preventQuickSlideClose && s.isMoving)
            return; // handle Chrome quick-close on slide-open
        else if (o.preventPrematureSlideClose && evt && $.layout.isMouseOverElem(evt, $Ps[pane]))
            return; // handle incorrect mouseleave trigger, like when over a SELECT-list in IE
        else if (evt) // trigger = mouseleave - use a delay
            // 1 sec delay if 'opening', else .3 sec
            timer.set(pane + "_closeSlider", close_NOW, max(o.slideDelay_close, delay));
        else // called programically
            close_NOW();

        /**
		 * SUBROUTINE for timed close
		 */
        function close_NOW() {
            if (s.isClosed) // skip 'close' if already closed!
                bindStopSlidingEvents(pane, false); // UNBIND trigger events - TODO: is this needed here?
            else if (!s.isMoving)
                close(pane); // close will handle unbinding
        };
    }

        /**
         * @param {(string|Object)}	evt_or_pane		The pane being opened, ie: north, south, east, or west
         */
    , slideToggle = function (evt_or_pane) {
        var pane = evtPane.call(this, evt_or_pane);
        toggle(pane, true);
    }


        /**
         * Must set left/top on East/South panes so animation will work properly
         *
         * @param {string}	pane	The pane to lock, 'east' or 'south' - any other is ignored!
         * @param {boolean}	doLock  true = set left/top, false = remove
         */
    , lockPaneForFX = function (pane, doLock) {
        var $P = $Ps[pane]
		, s = state[pane]
		, o = options[pane]
		, z = options.zIndexes
        ;
        if (doLock) {
            showMasks(pane, { animation: true, objectsOnly: true });
            $P.css({ zIndex: z.pane_animate }); // overlay all elements during animation
            if (pane == "south")
                $P.css({ top: sC.inset.top + sC.innerHeight - $P.outerHeight() });
            else if (pane == "east")
                $P.css({ left: sC.inset.left + sC.innerWidth - $P.outerWidth() });
        }
        else { // animation DONE - RESET CSS
            hideMasks();
            $P.css({ zIndex: (s.isSliding ? z.pane_sliding : z.pane_normal) });
            if (pane == "south")
                $P.css({ top: "auto" });
                // if pane is positioned 'off-screen', then DO NOT screw with it!
            else if (pane == "east" && !$P.css("left").match(/\-99999/))
                $P.css({ left: "auto" });
            // fix anti-aliasing in IE - only needed for animations that change opacity
            if (browser.msie && o.fxOpacityFix && o.fxName_open != "slide" && $P.css("filter") && $P.css("opacity") == 1)
                $P[0].style.removeAttribute('filter');
        }
    }


        /**
         * Toggle sliding functionality of a specific pane on/off by adding removing 'slide open' trigger
         *
         * @see  open(), close()
         * @param {string}	pane	The pane to enable/disable, 'north', 'south', etc.
         * @param {boolean}	enable	Enable or Disable sliding?
         */
    , bindStartSlidingEvents = function (pane, enable) {
        var o = options[pane]
		, $P = $Ps[pane]
		, $R = $Rs[pane]
		, evtName = o.slideTrigger_open.toLowerCase()
        ;
        if (!$R || (enable && !o.slidable)) return;

        // make sure we have a valid event
        if (evtName.match(/mouseover/))
            evtName = o.slideTrigger_open = "mouseenter";
        else if (!evtName.match(/(click|dblclick|mouseenter)/))
            evtName = o.slideTrigger_open = "click";

        // must remove double-click-toggle when using dblclick-slide
        if (o.resizerDblClickToggle && evtName.match(/click/)) {
            $R[enable ? "unbind" : "bind"]('dblclick.' + sID, toggle)
        }

        $R
			// add or remove event
			[enable ? "bind" : "unbind"](evtName + '.' + sID, slideOpen)
			// set the appropriate cursor & title/tip
			.css("cursor", enable ? o.sliderCursor : "default")
			.attr("title", enable ? o.tips.Slide : "")
        ;
    }

        /**
         * Add or remove 'mouseleave' events to 'slide close' when pane is 'sliding' open or closed
         * Also increases zIndex when pane is sliding open
         * See bindStartSlidingEvents for code to control 'slide open'
         *
         * @see  slideOpen(), slideClose()
         * @param {string}	pane	The pane to process, 'north', 'south', etc.
         * @param {boolean}	enable	Enable or Disable events?
         */
    , bindStopSlidingEvents = function (pane, enable) {
        var o = options[pane]
		, s = state[pane]
		, c = _c[pane]
		, z = options.zIndexes
		, evtName = o.slideTrigger_close.toLowerCase()
		, action = (enable ? "bind" : "unbind")
		, $P = $Ps[pane]
		, $R = $Rs[pane]
        ;
        timer.clear(pane + "_closeSlider"); // just in case

        if (enable) {
            s.isSliding = true;
            state.panesSliding[pane] = true;
            // remove 'slideOpen' event from resizer
            // ALSO will raise the zIndex of the pane & resizer
            bindStartSlidingEvents(pane, false);
        }
        else {
            s.isSliding = false;
            delete state.panesSliding[pane];
        }

        // RE/SET zIndex - increases when pane is sliding-open, resets to normal when not
        $P.css("zIndex", enable ? z.pane_sliding : z.pane_normal);
        $R.css("zIndex", enable ? z.pane_sliding + 2 : z.resizer_normal); // NOTE: mask = pane_sliding+1

        // make sure we have a valid event
        if (!evtName.match(/(click|mouseleave)/))
            evtName = o.slideTrigger_close = "mouseleave"; // also catches 'mouseout'

        // add/remove slide triggers
        $R[action](evtName, slideClose); // base event on resize
        // need extra events for mouseleave
        if (evtName === "mouseleave") {
            // also close on pane.mouseleave
            $P[action]("mouseleave." + sID, slideClose);
            // cancel timer when mouse moves between 'pane' and 'resizer'
            $R[action]("mouseenter." + sID, cancelMouseOut);
            $P[action]("mouseenter." + sID, cancelMouseOut);
        }

        if (!enable)
            timer.clear(pane + "_closeSlider");
        else if (evtName === "click" && !o.resizable) {
            // IF pane is not resizable (which already has a cursor and tip) 
            // then set the a cursor & title/tip on resizer when sliding
            $R.css("cursor", enable ? o.sliderCursor : "default");
            $R.attr("title", enable ? o.tips.Close : ""); // use Toggler-tip, eg: "Close Pane"
        }

        // SUBROUTINE for mouseleave timer clearing
        function cancelMouseOut(evt) {
            timer.clear(pane + "_closeSlider");
            evt.stopPropagation();
        }
    }


        /**
         * Hides/closes a pane if there is insufficient room - reverses this when there is room again
         * MUST have already called setSizeLimits() before calling this method
         *
         * @param {string}	pane					The pane being resized
         * @param {boolean=}	[isOpening=false]		Called from onOpen?
         * @param {boolean=}	[skipCallback=false]	Should the onresize callback be run?
         * @param {boolean=}	[force=false]
         */
    , makePaneFit = function (pane, isOpening, skipCallback, force) {
        var o = options[pane]
		, s = state[pane]
		, c = _c[pane]
		, $P = $Ps[pane]
		, $R = $Rs[pane]
		, isSidePane = c.dir === "vert"
		, hasRoom = false
        ;
        // special handling for center & east/west panes
        if (pane === "center" || (isSidePane && s.noVerticalRoom)) {
            // see if there is enough room to display the pane
            // ERROR: hasRoom = s.minHeight <= s.maxHeight && (isSidePane || s.minWidth <= s.maxWidth);
            hasRoom = (s.maxHeight >= 0);
            if (hasRoom && s.noRoom) { // previously hidden due to noRoom, so show now
                _showPane(pane);
                if ($R) $R.show();
                s.isVisible = true;
                s.noRoom = false;
                if (isSidePane) s.noVerticalRoom = false;
                _fixIframe(pane);
            }
            else if (!hasRoom && !s.noRoom) { // not currently hidden, so hide now
                _hidePane(pane);
                if ($R) $R.hide();
                s.isVisible = false;
                s.noRoom = true;
            }
        }

        // see if there is enough room to fit the border-pane
        if (pane === "center") {
            // ignore center in this block
        }
        else if (s.minSize <= s.maxSize) { // pane CAN fit
            hasRoom = true;
            if (s.size > s.maxSize) // pane is too big - shrink it
                sizePane(pane, s.maxSize, skipCallback, true, force); // true = noAnimation
            else if (s.size < s.minSize) // pane is too small - enlarge it
                sizePane(pane, s.minSize, skipCallback, true, force); // true = noAnimation
                // need s.isVisible because new pseudoClose method keeps pane visible, but off-screen
            else if ($R && s.isVisible && $P.is(":visible")) {
                // make sure resizer-bar is positioned correctly
                // handles situation where nested layout was 'hidden' when initialized
                var pos = s.size + sC.inset[c.side];
                if ($.layout.cssNum($R, c.side) != pos) $R.css(c.side, pos);
            }

            // if was previously hidden due to noRoom, then RESET because NOW there is room
            if (s.noRoom) {
                // s.noRoom state will be set by open or show
                if (s.wasOpen && o.closable) {
                    if (o.autoReopen)
                        open(pane, false, true, true); // true = noAnimation, true = noAlert
                    else // leave the pane closed, so just update state
                        s.noRoom = false;
                }
                else
                    show(pane, s.wasOpen, true, true); // true = noAnimation, true = noAlert
            }
        }
        else { // !hasRoom - pane CANNOT fit
            if (!s.noRoom) { // pane not set as noRoom yet, so hide or close it now...
                s.noRoom = true; // update state
                s.wasOpen = !s.isClosed && !s.isSliding;
                if (s.isClosed) { } // SKIP
                else if (o.closable) // 'close' if possible
                    close(pane, true, true); // true = force, true = noAnimation
                else // 'hide' pane if cannot just be closed
                    hide(pane, true); // true = noAnimation
            }
        }
    }


        /**
         * manualSizePane is an exposed flow-through method allowing extra code when pane is 'manually resized'
         *
         * @param {(string|Object)}	evt_or_pane				The pane being resized
         * @param {number}			size					The *desired* new size for this pane - will be validated
         * @param {boolean=}			[skipCallback=false]	Should the onresize callback be run?
         * @param {boolean=}			[noAnimation=false]
         * @param {boolean=}			[force=false]			Force resizing even if does not seem necessary
         */
    , manualSizePane = function (evt_or_pane, size, skipCallback, noAnimation, force) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, o = options[pane]
		, s = state[pane]
		//	if resizing callbacks have been delayed and resizing is now DONE, force resizing to complete...
		, forceResize = force || (o.livePaneResizing && !s.isResizing)
        ;
        if (pane === "center") return; // validate
        // ANY call to manualSizePane disables autoResize - ie, percentage sizing
        s.autoResize = false;
        // flow-through...
        sizePane(pane, size, skipCallback, noAnimation, forceResize); // will animate resize if option enabled
    }

        /**
         * sizePane is called only by internal methods whenever a pane needs to be resized
         *
         * @param {(string|Object)}	evt_or_pane				The pane being resized
         * @param {number}			size					The *desired* new size for this pane - will be validated
         * @param {boolean=}			[skipCallback=false]	Should the onresize callback be run?
         * @param {boolean=}			[noAnimation=false]
         * @param {boolean=}			[force=false]			Force resizing even if does not seem necessary
         */
    , sizePane = function (evt_or_pane, size, skipCallback, noAnimation, force) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane) // probably NEVER called from event?
		, o = options[pane]
		, s = state[pane]
		, $P = $Ps[pane]
		, $R = $Rs[pane]
		, side = _c[pane].side
		, dimName = _c[pane].sizeType.toLowerCase()
		, skipResizeWhileDragging = s.isResizing && !o.triggerEventsDuringLiveResize
		, doFX = noAnimation !== true && o.animatePaneSizing
		, oldSize, newSize
        ;
        if (pane === "center") return; // validate
        // QUEUE in case another action/animation is in progress
        $N.queue(function (queueNext) {
            // calculate 'current' min/max sizes
            setSizeLimits(pane); // update pane-state
            oldSize = s.size;
            size = _parseSize(pane, size); // handle percentages & auto
            size = max(size, _parseSize(pane, o.minSize));
            size = min(size, s.maxSize);
            if (size < s.minSize) { // not enough room for pane!
                queueNext(); // call before makePaneFit() because it needs the queue free
                makePaneFit(pane, false, skipCallback);	// will hide or close pane
                return;
            }

            // IF newSize is same as oldSize, then nothing to do - abort
            if (!force && size === oldSize)
                return queueNext();

            s.newSize = size;

            // onresize_start callback CANNOT cancel resizing because this would break the layout!
            if (!skipCallback && state.initialized && s.isVisible)
                _runCallbacks("onresize_start", pane);

            // resize the pane, and make sure its visible
            newSize = cssSize(pane, size);

            if (doFX && $P.is(":visible")) { // ANIMATE
                var fx = $.layout.effects.size[pane] || $.layout.effects.size.all
				, easing = o.fxSettings_size.easing || fx.easing
				, z = options.zIndexes
				, props = {};
                props[dimName] = newSize + 'px';
                s.isMoving = true;
                // overlay all elements during animation
                $P.css({ zIndex: z.pane_animate })
				  .show().animate(props, o.fxSpeed_size, easing, function () {
				      // reset zIndex after animation
				      $P.css({ zIndex: (s.isSliding ? z.pane_sliding : z.pane_normal) });
				      s.isMoving = false;
				      delete s.newSize;
				      sizePane_2(); // continue
				      queueNext();
				  });
            }
            else { // no animation
                $P.css(dimName, newSize);	// resize pane
                delete s.newSize;
                // if pane is visible, then 
                if ($P.is(":visible"))
                    sizePane_2(); // continue
                else {
                    // pane is NOT VISIBLE, so just update state data...
                    // when pane is *next opened*, it will have the new size
                    s.size = size;				// update state.size
                    //$.extend(s, elDims($P));	// update state dimensions - CANNOT do this when not visible!				}
                }
                queueNext();
            };

        });

        // SUBROUTINE
        function sizePane_2() {
            /*	Panes are sometimes not sized precisely in some browsers!?
			 *	This code will resize the pane up to 3 times to nudge the pane to the correct size
			 */
            var actual = dimName === 'width' ? $P.outerWidth() : $P.outerHeight()
			, tries = [{
			    pane: pane
						, count: 1
						, target: size
						, actual: actual
						, correct: (size === actual)
						, attempt: size
						, cssSize: newSize
			}]
			, lastTry = tries[0]
			, thisTry = {}
			, msg = 'Inaccurate size after resizing the ' + pane + '-pane.'
            ;
            while (!lastTry.correct) {
                thisTry = { pane: pane, count: lastTry.count + 1, target: size };

                if (lastTry.actual > size)
                    thisTry.attempt = max(0, lastTry.attempt - (lastTry.actual - size));
                else // lastTry.actual < size
                    thisTry.attempt = max(0, lastTry.attempt + (size - lastTry.actual));

                thisTry.cssSize = cssSize(pane, thisTry.attempt);
                $P.css(dimName, thisTry.cssSize);

                thisTry.actual = dimName == 'width' ? $P.outerWidth() : $P.outerHeight();
                thisTry.correct = (size === thisTry.actual);

                // log attempts and alert the user of this *non-fatal error* (if showDebugMessages)
                if (tries.length === 1) {
                    _log(msg, false, true);
                    _log(lastTry, false, true);
                }
                _log(thisTry, false, true);
                // after 4 tries, is as close as its gonna get!
                if (tries.length > 3) break;

                tries.push(thisTry);
                lastTry = tries[tries.length - 1];
            }
            // END TESTING CODE

            // update pane-state dimensions
            s.size = size;
            $.extend(s, elDims($P));

            if (s.isVisible && $P.is(":visible")) {
                // reposition the resizer-bar
                if ($R) $R.css(side, size + sC.inset[side]);
                // resize the content-div
                sizeContent(pane);
            }

            if (!skipCallback && !skipResizeWhileDragging && state.initialized && s.isVisible)
                _runCallbacks("onresize_end", pane);

            // resize all the adjacent panes, and adjust their toggler buttons
            // when skipCallback passed, it means the controlling method will handle 'other panes'
            if (!skipCallback) {
                // also no callback if live-resize is in progress and NOT triggerEventsDuringLiveResize
                if (!s.isSliding) sizeMidPanes(_c[pane].dir == "horz" ? "" : "center", skipResizeWhileDragging, force);
                sizeHandles();
            }

            // if opposite-pane was autoClosed, see if it can be autoOpened now
            var altPane = _c.oppositeEdge[pane];
            if (size < oldSize && state[altPane].noRoom) {
                setSizeLimits(altPane);
                makePaneFit(altPane, false, skipCallback);
            }

            // DEBUG - ALERT user/developer so they know there was a sizing problem
            if (tries.length > 1)
                _log(msg + '\nSee the Error Console for details.', true, true);
        }
    }

        /**
         * @see  initPanes(), sizePane(), 	resizeAll(), open(), close(), hide()
         * @param {(Array.<string>|string)}	panes					The pane(s) being resized, comma-delmited string
         * @param {boolean=}					[skipCallback=false]	Should the onresize callback be run?
         * @param {boolean=}					[force=false]
         */
    , sizeMidPanes = function (panes, skipCallback, force) {
        panes = (panes ? panes : "east,west,center").split(",");

        $.each(panes, function (i, pane) {
            if (!$Ps[pane]) return; // NO PANE - skip
            var
				o = options[pane]
			, s = state[pane]
			, $P = $Ps[pane]
			, $R = $Rs[pane]
			, isCenter = (pane == "center")
			, hasRoom = true
			, CSS = {}
			//	if pane is not visible, show it invisibly NOW rather than for *each call* in this script
			, visCSS = $.layout.showInvisibly($P)

			, newCenter = calcNewCenterPaneDims()
            ;

            // update pane-state dimensions
            $.extend(s, elDims($P));

            if (pane === "center") {
                if (!force && s.isVisible && newCenter.width === s.outerWidth && newCenter.height === s.outerHeight) {
                    $P.css(visCSS);
                    return true; // SKIP - pane already the correct size
                }
                // set state for makePaneFit() logic
                $.extend(s, cssMinDims(pane), {
                    maxWidth: newCenter.width
				, maxHeight: newCenter.height
                });
                CSS = newCenter;
                s.newWidth = CSS.width;
                s.newHeight = CSS.height;
                // convert OUTER width/height to CSS width/height 
                CSS.width = cssW($P, CSS.width);
                // NEW - allow pane to extend 'below' visible area rather than hide it
                CSS.height = cssH($P, CSS.height);
                hasRoom = CSS.width >= 0 && CSS.height >= 0; // height >= 0 = ALWAYS TRUE NOW

                // during layout init, try to shrink east/west panes to make room for center
                if (!state.initialized && o.minWidth > newCenter.width) {
                    var
						reqPx = o.minWidth - s.outerWidth
					, minE = options.east.minSize || 0
					, minW = options.west.minSize || 0
					, sizeE = state.east.size
					, sizeW = state.west.size
					, newE = sizeE
					, newW = sizeW
                    ;
                    if (reqPx > 0 && state.east.isVisible && sizeE > minE) {
                        newE = max(sizeE - minE, sizeE - reqPx);
                        reqPx -= sizeE - newE;
                    }
                    if (reqPx > 0 && state.west.isVisible && sizeW > minW) {
                        newW = max(sizeW - minW, sizeW - reqPx);
                        reqPx -= sizeW - newW;
                    }
                    // IF we found enough extra space, then resize the border panes as calculated
                    if (reqPx === 0) {
                        if (sizeE && sizeE != minE)
                            sizePane('east', newE, true, true, force); // true = skipCallback/noAnimation - initPanes will handle when done
                        if (sizeW && sizeW != minW)
                            sizePane('west', newW, true, true, force); // true = skipCallback/noAnimation
                        // now start over!
                        sizeMidPanes('center', skipCallback, force);
                        $P.css(visCSS);
                        return; // abort this loop
                    }
                }
            }
            else { // for east and west, set only the height, which is same as center height
                // set state.min/maxWidth/Height for makePaneFit() logic
                if (s.isVisible && !s.noVerticalRoom)
                    $.extend(s, elDims($P), cssMinDims(pane))
                if (!force && !s.noVerticalRoom && newCenter.height === s.outerHeight) {
                    $P.css(visCSS);
                    return true; // SKIP - pane already the correct size
                }
                // east/west have same top, bottom & height as center
                CSS.top = newCenter.top;
                CSS.bottom = newCenter.bottom;
                s.newSize = newCenter.height
                // NEW - allow pane to extend 'below' visible area rather than hide it
                CSS.height = cssH($P, newCenter.height);
                s.maxHeight = CSS.height;
                hasRoom = (s.maxHeight >= 0); // ALWAYS TRUE NOW
                if (!hasRoom) s.noVerticalRoom = true; // makePaneFit() logic
            }

            if (hasRoom) {
                // resizeAll passes skipCallback because it triggers callbacks after ALL panes are resized
                if (!skipCallback && state.initialized)
                    _runCallbacks("onresize_start", pane);

                $P.css(CSS); // apply the CSS to pane
                if (pane !== "center")
                    sizeHandles(pane); // also update resizer length
                if (s.noRoom && !s.isClosed && !s.isHidden)
                    makePaneFit(pane); // will re-open/show auto-closed/hidden pane
                if (s.isVisible) {
                    $.extend(s, elDims($P)); // update pane dimensions
                    if (state.initialized) sizeContent(pane); // also resize the contents, if exists
                }
            }
            else if (!s.noRoom && s.isVisible) // no room for pane
                makePaneFit(pane); // will hide or close pane

            // reset visibility, if necessary
            $P.css(visCSS);

            delete s.newSize;
            delete s.newWidth;
            delete s.newHeight;

            if (!s.isVisible)
                return true; // DONE - next pane

            /*
			 * Extra CSS for IE6 or IE7 in Quirks-mode - add 'width' to NORTH/SOUTH panes
			 * Normally these panes have only 'left' & 'right' positions so pane auto-sizes
			 * ALSO required when pane is an IFRAME because will NOT default to 'full width'
			 *	TODO: Can I use width:100% for a north/south iframe?
			 *	TODO: Sounds like a job for $P.outerWidth( sC.innerWidth ) SETTER METHOD
			 */
            if (pane === "center") { // finished processing midPanes
                var fix = browser.isIE6 || !browser.boxModel;
                if ($Ps.north && (fix || state.north.tagName == "IFRAME"))
                    $Ps.north.css("width", cssW($Ps.north, sC.innerWidth));
                if ($Ps.south && (fix || state.south.tagName == "IFRAME"))
                    $Ps.south.css("width", cssW($Ps.south, sC.innerWidth));
            }

            // resizeAll passes skipCallback because it triggers callbacks after ALL panes are resized
            if (!skipCallback && state.initialized)
                _runCallbacks("onresize_end", pane);
        });
    }


        /**
         * @see  window.onresize(), callbacks or custom code
         * @param {(Object|boolean)=}	evt_or_refresh	If 'true', then also reset pane-positioning
         */
    , resizeAll = function (evt_or_refresh) {
        var oldW = sC.innerWidth
		, oldH = sC.innerHeight
        ;
        // stopPropagation if called by trigger("layoutdestroy") - use evtPane utility 
        evtPane(evt_or_refresh);

        // cannot size layout when 'container' is hidden or collapsed
        if (!$N.is(":visible")) return;

        if (!state.initialized) {
            _initLayoutElements();
            return; // no need to resize since we just initialized!
        }

        if (evt_or_refresh === true && $.isPlainObject(options.outset)) {
            // update container CSS in case outset option has changed
            $N.css(options.outset);
        }
        // UPDATE container dimensions
        $.extend(sC, elDims($N, options.inset));
        if (!sC.outerHeight) return;

        // if 'true' passed, refresh pane & handle positioning too
        if (evt_or_refresh === true) {
            setPanePosition();
        }

        // onresizeall_start will CANCEL resizing if returns false
        // state.container has already been set, so user can access this info for calcuations
        if (false === _runCallbacks("onresizeall_start")) return false;

        var	// see if container is now 'smaller' than before
			shrunkH = (sC.innerHeight < oldH)
		, shrunkW = (sC.innerWidth < oldW)
		, $P, o, s
        ;
        // NOTE special order for sizing: S-N-E-W
        $.each(["south", "north", "east", "west"], function (i, pane) {
            if (!$Ps[pane]) return; // no pane - SKIP
            o = options[pane];
            s = state[pane];
            if (s.autoResize && s.size != o.size) // resize pane to original size set in options
                sizePane(pane, o.size, true, true, true); // true=skipCallback/noAnimation/forceResize
            else {
                setSizeLimits(pane);
                makePaneFit(pane, false, true, true); // true=skipCallback/forceResize
            }
        });

        sizeMidPanes("", true, true); // true=skipCallback/forceResize
        sizeHandles(); // reposition the toggler elements

        // trigger all individual pane callbacks AFTER layout has finished resizing
        $.each(_c.allPanes, function (i, pane) {
            $P = $Ps[pane];
            if (!$P) return; // SKIP
            if (state[pane].isVisible) // undefined for non-existent panes
                _runCallbacks("onresize_end", pane); // callback - if exists
        });

        _runCallbacks("onresizeall_end");
        //_triggerLayoutEvent(pane, 'resizeall');
    }

        /**
         * Whenever a pane resizes or opens that has a nested layout, trigger resizeAll
         *
         * @param {(string|Object)}	evt_or_pane		The pane just resized or opened
         */
    , resizeChildren = function (evt_or_pane, skipRefresh) {
        var pane = evtPane.call(this, evt_or_pane);

        if (!options[pane].resizeChildren) return;

        // ensure the pane-children are up-to-date
        if (!skipRefresh) refreshChildren(pane);
        var pC = children[pane];
        if ($.isPlainObject(pC)) {
            // resize one or more children
            $.each(pC, function (key, child) {
                if (!child.destroyed) child.resizeAll();
            });
        }
    }

        /**
         * IF pane has a content-div, then resize all elements inside pane to fit pane-height
         *
         * @param {(string|Object)}	evt_or_panes		The pane(s) being resized
         * @param {boolean=}			[remeasure=false]	Should the content (header/footer) be remeasured?
         */
    , sizeContent = function (evt_or_panes, remeasure) {
        if (!isInitialized()) return;

        var panes = evtPane.call(this, evt_or_panes);
        panes = panes ? panes.split(",") : _c.allPanes;

        $.each(panes, function (idx, pane) {
            var
				$P = $Ps[pane]
			, $C = $Cs[pane]
			, o = options[pane]
			, s = state[pane]
			, m = s.content // m = measurements
            ;
            if (!$P || !$C || !$P.is(":visible")) return true; // NOT VISIBLE - skip

            // if content-element was REMOVED, update OR remove the pointer
            if (!$C.length) {
                initContent(pane, false);	// false = do NOT sizeContent() - already there!
                if (!$C) return;			// no replacement element found - pointer have been removed
            }

            // onsizecontent_start will CANCEL resizing if returns false
            if (false === _runCallbacks("onsizecontent_start", pane)) return;

            // skip re-measuring offsets if live-resizing
            if ((!s.isMoving && !s.isResizing) || o.liveContentResizing || remeasure || m.top == undefined) {
                _measure();
                // if any footers are below pane-bottom, they may not measure correctly,
                // so allow pane overflow and re-measure
                if (m.hiddenFooters > 0 && $P.css("overflow") === "hidden") {
                    $P.css("overflow", "visible");
                    _measure(); // remeasure while overflowing
                    $P.css("overflow", "hidden");
                }
            }
            // NOTE: spaceAbove/Below *includes* the pane paddingTop/Bottom, but not pane.borders
            var newH = s.innerHeight - (m.spaceAbove - s.css.paddingTop) - (m.spaceBelow - s.css.paddingBottom);

            if (!$C.is(":visible") || m.height != newH) {
                // size the Content element to fit new pane-size - will autoHide if not enough room
                setOuterHeight($C, newH, true); // true=autoHide
                m.height = newH; // save new height
            };

            if (state.initialized)
                _runCallbacks("onsizecontent_end", pane);

            function _below($E) {
                return max(s.css.paddingBottom, (parseInt($E.css("marginBottom"), 10) || 0));
            };

            function _measure() {
                var
					ignore = options[pane].contentIgnoreSelector
				, $Fs = $C.nextAll().not(".ui-layout-mask").not(ignore || ":lt(0)") // not :lt(0) = ALL
				, $Fs_vis = $Fs.filter(':visible')
				, $F = $Fs_vis.filter(':last')
                ;
                m = {
                    top: $C[0].offsetTop
				, height: $C.outerHeight()
				, numFooters: $Fs.length
				, hiddenFooters: $Fs.length - $Fs_vis.length
				, spaceBelow: 0 // correct if no content footer ($E)
                }
                m.spaceAbove = m.top; // just for state - not used in calc
                m.bottom = m.top + m.height;
                if ($F.length)
                    //spaceBelow = (LastFooter.top + LastFooter.height) [footerBottom] - Content.bottom + max(LastFooter.marginBottom, pane.paddingBotom)
                    m.spaceBelow = ($F[0].offsetTop + $F.outerHeight()) - m.bottom + _below($F);
                else // no footer - check marginBottom on Content element itself
                    m.spaceBelow = _below($C);
            };
        });
    }


        /**
         * Called every time a pane is opened, closed, or resized to slide the togglers to 'center' and adjust their length if necessary
         *
         * @see  initHandles(), open(), close(), resizeAll()
         * @param {(string|Object)=}		evt_or_panes	The pane(s) being resized
         */
    , sizeHandles = function (evt_or_panes) {
        var panes = evtPane.call(this, evt_or_panes)
        panes = panes ? panes.split(",") : _c.borderPanes;

        $.each(panes, function (i, pane) {
            var
				o = options[pane]
			, s = state[pane]
			, $P = $Ps[pane]
			, $R = $Rs[pane]
			, $T = $Ts[pane]
			, $TC
            ;
            if (!$P || !$R) return;

            var
				dir = _c[pane].dir
			, _state = (s.isClosed ? "_closed" : "_open")
			, spacing = o["spacing" + _state]
			, togAlign = o["togglerAlign" + _state]
			, togLen = o["togglerLength" + _state]
			, paneLen
			, left
			, offset
			, CSS = {}
            ;

            if (spacing === 0) {
                $R.hide();
                return;
            }
            else if (!s.noRoom && !s.isHidden) // skip if resizer was hidden for any reason
                $R.show(); // in case was previously hidden

            // Resizer Bar is ALWAYS same width/height of pane it is attached to
            if (dir === "horz") { // north/south
                //paneLen = $P.outerWidth(); // s.outerWidth || 
                paneLen = sC.innerWidth; // handle offscreen-panes
                s.resizerLength = paneLen;
                left = $.layout.cssNum($P, "left")
                $R.css({
                    width: cssW($R, paneLen) // account for borders & padding
				, height: cssH($R, spacing) // ditto
				, left: left > -9999 ? left : sC.inset.left // handle offscreen-panes
                });
            }
            else { // east/west
                paneLen = $P.outerHeight(); // s.outerHeight || 
                s.resizerLength = paneLen;
                $R.css({
                    height: cssH($R, paneLen) // account for borders & padding
				, width: cssW($R, spacing) // ditto
				, top: sC.inset.top + getPaneSize("north", true) // TODO: what if no North pane?
                    //,	top:	$.layout.cssNum($Ps["center"], "top")
                });
            }

            // remove hover classes
            removeHover(o, $R);

            if ($T) {
                if (togLen === 0 || (s.isSliding && o.hideTogglerOnSlide)) {
                    $T.hide(); // always HIDE the toggler when 'sliding'
                    return;
                }
                else
                    $T.show(); // in case was previously hidden

                if (!(togLen > 0) || togLen === "100%" || togLen > paneLen) {
                    togLen = paneLen;
                    offset = 0;
                }
                else { // calculate 'offset' based on options.PANE.togglerAlign_open/closed
                    if (isStr(togAlign)) {
                        switch (togAlign) {
                            case "top":
                            case "left": offset = 0;
                                break;
                            case "bottom":
                            case "right": offset = paneLen - togLen;
                                break;
                            case "middle":
                            case "center":
                            default: offset = round((paneLen - togLen) / 2); // 'default' catches typos
                        }
                    }
                    else { // togAlign = number
                        var x = parseInt(togAlign, 10); //
                        if (togAlign >= 0) offset = x;
                        else offset = paneLen - togLen + x; // NOTE: x is negative!
                    }
                }

                if (dir === "horz") { // north/south
                    var width = cssW($T, togLen);
                    $T.css({
                        width: width  // account for borders & padding
					, height: cssH($T, spacing) // ditto
					, left: offset // TODO: VERIFY that toggler  positions correctly for ALL values
					, top: 0
                    });
                    // CENTER the toggler content SPAN
                    $T.children(".content").each(function () {
                        $TC = $(this);
                        $TC.css("marginLeft", round((width - $TC.outerWidth()) / 2)); // could be negative
                    });
                }
                else { // east/west
                    var height = cssH($T, togLen);
                    $T.css({
                        height: height // account for borders & padding
					, width: cssW($T, spacing) // ditto
					, top: offset // POSITION the toggler
					, left: 0
                    });
                    // CENTER the toggler content SPAN
                    $T.children(".content").each(function () {
                        $TC = $(this);
                        $TC.css("marginTop", round((height - $TC.outerHeight()) / 2)); // could be negative
                    });
                }

                // remove ALL hover classes
                removeHover(0, $T);
            }

            // DONE measuring and sizing this resizer/toggler, so can be 'hidden' now
            if (!state.initialized && (o.initHidden || s.isHidden)) {
                $R.hide();
                if ($T) $T.hide();
            }
        });
    }


        /**
         * @param {(string|Object)}	evt_or_pane
         */
    , enableClosable = function (evt_or_pane) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, $T = $Ts[pane]
		, o = options[pane]
        ;
        if (!$T) return;
        o.closable = true;
        $T.bind("click." + sID, function (evt) { evt.stopPropagation(); toggle(pane); })
			.css("visibility", "visible")
			.css("cursor", "pointer")
			.attr("title", state[pane].isClosed ? o.tips.Open : o.tips.Close) // may be blank
			.show();
    }
        /**
         * @param {(string|Object)}	evt_or_pane
         * @param {boolean=}			[hide=false]
         */
    , disableClosable = function (evt_or_pane, hide) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, $T = $Ts[pane]
        ;
        if (!$T) return;
        options[pane].closable = false;
        // is closable is disable, then pane MUST be open!
        if (state[pane].isClosed) open(pane, false, true);
        $T.unbind("." + sID)
			.css("visibility", hide ? "hidden" : "visible") // instead of hide(), which creates logic issues
			.css("cursor", "default")
			.attr("title", "");
    }


        /**
         * @param {(string|Object)}	evt_or_pane
         */
    , enableSlidable = function (evt_or_pane) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, $R = $Rs[pane]
        ;
        if (!$R || !$R.data('draggable')) return;
        options[pane].slidable = true;
        if (state[pane].isClosed)
            bindStartSlidingEvents(pane, true);
    }
        /**
         * @param {(string|Object)}	evt_or_pane
         */
    , disableSlidable = function (evt_or_pane) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, $R = $Rs[pane]
        ;
        if (!$R) return;
        options[pane].slidable = false;
        if (state[pane].isSliding)
            close(pane, false, true);
        else {
            bindStartSlidingEvents(pane, false);
            $R.css("cursor", "default")
				.attr("title", "");
            removeHover(null, $R[0]); // in case currently hovered
        }
    }


        /**
         * @param {(string|Object)}	evt_or_pane
         */
    , enableResizable = function (evt_or_pane) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, $R = $Rs[pane]
		, o = options[pane]
        ;
        if (!$R || !$R.data('draggable')) return;
        o.resizable = true;
        $R.draggable("enable");
        if (!state[pane].isClosed)
            $R.css("cursor", o.resizerCursor)
			 	.attr("title", o.tips.Resize);
    }
        /**
         * @param {(string|Object)}	evt_or_pane
         */
    , disableResizable = function (evt_or_pane) {
        if (!isInitialized()) return;
        var pane = evtPane.call(this, evt_or_pane)
		, $R = $Rs[pane]
        ;
        if (!$R || !$R.data('draggable')) return;
        options[pane].resizable = false;
        $R.draggable("disable")
			.css("cursor", "default")
			.attr("title", "");
        removeHover(null, $R[0]); // in case currently hovered
    }


        /**
         * Move a pane from source-side (eg, west) to target-side (eg, east)
         * If pane exists on target-side, move that to source-side, ie, 'swap' the panes
         *
         * @param {(string|Object)}	evt_or_pane1	The pane/edge being swapped
         * @param {string}			pane2			ditto
         */
    , swapPanes = function (evt_or_pane1, pane2) {
        if (!isInitialized()) return;
        var pane1 = evtPane.call(this, evt_or_pane1);
        // change state.edge NOW so callbacks can know where pane is headed...
        state[pane1].edge = pane2;
        state[pane2].edge = pane1;
        // run these even if NOT state.initialized
        if (false === _runCallbacks("onswap_start", pane1)
		 || false === _runCallbacks("onswap_start", pane2)
		) {
            state[pane1].edge = pane1; // reset
            state[pane2].edge = pane2;
            return;
        }

        var
			oPane1 = copy(pane1)
		, oPane2 = copy(pane2)
		, sizes = {}
        ;
        sizes[pane1] = oPane1 ? oPane1.state.size : 0;
        sizes[pane2] = oPane2 ? oPane2.state.size : 0;

        // clear pointers & state
        $Ps[pane1] = false;
        $Ps[pane2] = false;
        state[pane1] = {};
        state[pane2] = {};

        // ALWAYS remove the resizer & toggler elements
        if ($Ts[pane1]) $Ts[pane1].remove();
        if ($Ts[pane2]) $Ts[pane2].remove();
        if ($Rs[pane1]) $Rs[pane1].remove();
        if ($Rs[pane2]) $Rs[pane2].remove();
        $Rs[pane1] = $Rs[pane2] = $Ts[pane1] = $Ts[pane2] = false;

        // transfer element pointers and data to NEW Layout keys
        move(oPane1, pane2);
        move(oPane2, pane1);

        // cleanup objects
        oPane1 = oPane2 = sizes = null;

        // make panes 'visible' again
        if ($Ps[pane1]) $Ps[pane1].css(_c.visible);
        if ($Ps[pane2]) $Ps[pane2].css(_c.visible);

        // fix any size discrepancies caused by swap
        resizeAll();

        // run these even if NOT state.initialized
        _runCallbacks("onswap_end", pane1);
        _runCallbacks("onswap_end", pane2);

        return;

        function copy(n) { // n = pane
            var
				$P = $Ps[n]
			, $C = $Cs[n]
            ;
            return !$P ? false : {
                pane: n
			, P: $P ? $P[0] : false
			, C: $C ? $C[0] : false
			, state: $.extend(true, {}, state[n])
			, options: $.extend(true, {}, options[n])
            }
        };

        function move(oPane, pane) {
            if (!oPane) return;
            var
				P = oPane.P
			, C = oPane.C
			, oldPane = oPane.pane
			, c = _c[pane]
			//	save pane-options that should be retained
			, s = $.extend(true, {}, state[pane])
			, o = options[pane]
			//	RETAIN side-specific FX Settings - more below
			, fx = { resizerCursor: o.resizerCursor }
			, re, size, pos
            ;
            $.each("fxName,fxSpeed,fxSettings".split(","), function (i, k) {
                fx[k + "_open"] = o[k + "_open"];
                fx[k + "_close"] = o[k + "_close"];
                fx[k + "_size"] = o[k + "_size"];
            });

            // update object pointers and attributes
            $Ps[pane] = $(P)
				.data({
				    layoutPane: Instance[pane]	// NEW pointer to pane-alias-object
				, layoutEdge: pane
				})
				.css(_c.hidden)
				.css(c.cssReq)
            ;
            $Cs[pane] = C ? $(C) : false;

            // set options and state
            options[pane] = $.extend(true, {}, oPane.options, fx);
            state[pane] = $.extend(true, {}, oPane.state);

            // change classNames on the pane, eg: ui-layout-pane-east ==> ui-layout-pane-west
            re = new RegExp(o.paneClass + "-" + oldPane, "g");
            P.className = P.className.replace(re, o.paneClass + "-" + pane);

            // ALWAYS regenerate the resizer & toggler elements
            initHandles(pane); // create the required resizer & toggler

            // if moving to different orientation, then keep 'target' pane size
            if (c.dir != _c[oldPane].dir) {
                size = sizes[pane] || 0;
                setSizeLimits(pane); // update pane-state
                size = max(size, state[pane].minSize);
                // use manualSizePane to disable autoResize - not useful after panes are swapped
                manualSizePane(pane, size, true, true); // true/true = skipCallback/noAnimation
            }
            else // move the resizer here
                $Rs[pane].css(c.side, sC.inset[c.side] + (state[pane].isVisible ? getPaneSize(pane) : 0));


            // ADD CLASSNAMES & SLIDE-BINDINGS
            if (oPane.state.isVisible && !s.isVisible)
                setAsOpen(pane, true); // true = skipCallback
            else {
                setAsClosed(pane);
                bindStartSlidingEvents(pane, true); // will enable events IF option is set
            }

            // DESTROY the object
            oPane = null;
        };
    }


        /**
         * INTERNAL method to sync pin-buttons when pane is opened or closed
         * Unpinned means the pane is 'sliding' - ie, over-top of the adjacent panes
         *
         * @see  open(), setAsOpen(), setAsClosed()
         * @param {string}	pane   These are the params returned to callbacks by layout()
         * @param {boolean}	doPin  True means set the pin 'down', False means 'up'
         */
    , syncPinBtns = function (pane, doPin) {
        if ($.layout.plugins.buttons)
            $.each(state[pane].pins, function (i, selector) {
                $.layout.buttons.setPinState(Instance, $(selector), pane, doPin);
            });
    }

        ;	// END var DECLARATIONS

        /**
         * Capture keys when enableCursorHotkey - toggle pane if hotkey pressed
         *
         * @see  document.keydown()
         */
        function keyDown(evt) {
            if (!evt) return true;
            var code = evt.keyCode;
            if (code < 33) return true; // ignore special keys: ENTER, TAB, etc

            var
                PANE = {
                    38: "north" // Up Cursor	- $.ui.keyCode.UP
                , 40: "south" // Down Cursor	- $.ui.keyCode.DOWN
                , 37: "west"  // Left Cursor	- $.ui.keyCode.LEFT
                , 39: "east"  // Right Cursor	- $.ui.keyCode.RIGHT
                }
            , ALT = evt.altKey // no worky!
            , SHIFT = evt.shiftKey
            , CTRL = evt.ctrlKey
            , CURSOR = (CTRL && code >= 37 && code <= 40)
            , o, k, m, pane
            ;

            if (CURSOR && options[PANE[code]].enableCursorHotkey) // valid cursor-hotkey
                pane = PANE[code];
            else if (CTRL || SHIFT) // check to see if this matches a custom-hotkey
                $.each(_c.borderPanes, function (i, p) { // loop each pane to check its hotkey
                    o = options[p];
                    k = o.customHotkey;
                    m = o.customHotkeyModifier; // if missing or invalid, treated as "CTRL+SHIFT"
                    if ((SHIFT && m == "SHIFT") || (CTRL && m == "CTRL") || (CTRL && SHIFT)) { // Modifier matches
                        if (k && code === (isNaN(k) || k <= 9 ? k.toUpperCase().charCodeAt(0) : k)) { // Key matches
                            pane = p;
                            return false; // BREAK
                        }
                    }
                });

            // validate pane
            if (!pane || !$Ps[pane] || !options[pane].closable || state[pane].isHidden)
                return true;

            toggle(pane);

            evt.stopPropagation();
            evt.returnValue = false; // CANCEL key
            return false;
        };


        /*
         * ######################################
         *	UTILITY METHODS
         *	called externally or by initButtons
         * ######################################
         */

        /**
         * Change/reset a pane overflow setting & zIndex to allow popups/drop-downs to work
         *
         * @param {Object=}   [el]	(optional) Can also be 'bound' to a click, mouseOver, or other event
         */
        function allowOverflow(el) {
            if (!isInitialized()) return;
            if (this && this.tagName) el = this; // BOUND to element
            var $P;
            if (isStr(el))
                $P = $Ps[el];
            else if ($(el).data("layoutRole"))
                $P = $(el);
            else
                $(el).parents().each(function () {
                    if ($(this).data("layoutRole")) {
                        $P = $(this);
                        return false; // BREAK
                    }
                });
            if (!$P || !$P.length) return; // INVALID

            var
                pane = $P.data("layoutEdge")
            , s = state[pane]
            ;

            // if pane is already raised, then reset it before doing it again!
            // this would happen if allowOverflow is attached to BOTH the pane and an element 
            if (s.cssSaved)
                resetOverflow(pane); // reset previous CSS before continuing

            // if pane is raised by sliding or resizing, or its closed, then abort
            if (s.isSliding || s.isResizing || s.isClosed) {
                s.cssSaved = false;
                return;
            }

            var
                newCSS = { zIndex: (options.zIndexes.resizer_normal + 1) }
            , curCSS = {}
            , of = $P.css("overflow")
            , ofX = $P.css("overflowX")
            , ofY = $P.css("overflowY")
            ;
            // determine which, if any, overflow settings need to be changed
            if (of != "visible") {
                curCSS.overflow = of;
                newCSS.overflow = "visible";
            }
            if (ofX && !ofX.match(/(visible|auto)/)) {
                curCSS.overflowX = ofX;
                newCSS.overflowX = "visible";
            }
            if (ofY && !ofY.match(/(visible|auto)/)) {
                curCSS.overflowY = ofX;
                newCSS.overflowY = "visible";
            }

            // save the current overflow settings - even if blank!
            s.cssSaved = curCSS;

            // apply new CSS to raise zIndex and, if necessary, make overflow 'visible'
            $P.css(newCSS);

            // make sure the zIndex of all other panes is normal
            $.each(_c.allPanes, function (i, p) {
                if (p != pane) resetOverflow(p);
            });

        };
        /**
         * @param {Object=}   [el]	(optional) Can also be 'bound' to a click, mouseOver, or other event
         */
        function resetOverflow(el) {
            if (!isInitialized()) return;
            if (this && this.tagName) el = this; // BOUND to element
            var $P;
            if (isStr(el))
                $P = $Ps[el];
            else if ($(el).data("layoutRole"))
                $P = $(el);
            else
                $(el).parents().each(function () {
                    if ($(this).data("layoutRole")) {
                        $P = $(this);
                        return false; // BREAK
                    }
                });
            if (!$P || !$P.length) return; // INVALID

            var
                pane = $P.data("layoutEdge")
            , s = state[pane]
            , CSS = s.cssSaved || {}
            ;
            // reset the zIndex
            if (!s.isSliding && !s.isResizing)
                $P.css("zIndex", options.zIndexes.pane_normal);

            // reset Overflow - if necessary
            $P.css(CSS);

            // clear var
            s.cssSaved = false;
        };

        /*
         * #####################
         * CREATE/RETURN LAYOUT
         * #####################
         */

        // validate that container exists
        var $N = $(this).eq(0); // FIRST matching Container element
        if (!$N.length) {
            return _log(options.errors.containerMissing);
        };

        // Users retrieve Instance of a layout with: $N.layout() OR $N.data("layout")
        // return the Instance-pointer if layout has already been initialized
        if ($N.data("layoutContainer") && $N.data("layout"))
            return $N.data("layout"); // cached pointer

        // init global vars
        var
            $Ps = {}	// Panes x5		- set in initPanes()
        , $Cs = {}	// Content x5	- set in initPanes()
        , $Rs = {}	// Resizers x4	- set in initHandles()
        , $Ts = {}	// Togglers x4	- set in initHandles()
        , $Ms = $([])	// Masks - up to 2 masks per pane (IFRAME + DIV)
        //	aliases for code brevity
        , sC = state.container // alias for easy access to 'container dimensions'
        , sID = state.id // alias for unique layout ID/namespace - eg: "layout435"
        ;

        // create Instance object to expose data & option Properties, and primary action Methods
        var Instance = {
            //	layout data
            options: options			// property - options hash
        , state: state			// property - dimensions hash
            //	object pointers
        , container: $N				// property - object pointers for layout container
        , panes: $Ps				// property - object pointers for ALL Panes: panes.north, panes.center
        , contents: $Cs				// property - object pointers for ALL Content: contents.north, contents.center
        , resizers: $Rs				// property - object pointers for ALL Resizers, eg: resizers.north
        , togglers: $Ts				// property - object pointers for ALL Togglers, eg: togglers.north
            //	border-pane open/close
        , hide: hide			// method - ditto
        , show: show			// method - ditto
        , toggle: toggle			// method - pass a 'pane' ("north", "west", etc)
        , open: open			// method - ditto
        , close: close			// method - ditto
        , slideOpen: slideOpen		// method - ditto
        , slideClose: slideClose		// method - ditto
        , slideToggle: slideToggle		// method - ditto
            //	pane actions
        , setSizeLimits: setSizeLimits	// method - pass a 'pane' - update state min/max data
        , _sizePane: sizePane		// method -intended for user by plugins only!
        , sizePane: manualSizePane	// method - pass a 'pane' AND an 'outer-size' in pixels or percent, or 'auto'
        , sizeContent: sizeContent		// method - pass a 'pane'
        , swapPanes: swapPanes		// method - pass TWO 'panes' - will swap them
        , showMasks: showMasks		// method - pass a 'pane' OR list of panes - default = all panes with mask option set
        , hideMasks: hideMasks		// method - ditto'
            //	pane element methods
        , initContent: initContent		// method - ditto
        , addPane: addPane			// method - pass a 'pane'
        , removePane: removePane		// method - pass a 'pane' to remove from layout, add 'true' to delete the pane-elem
        , createChildren: createChildren	// method - pass a 'pane' and (optional) layout-options (OVERRIDES options[pane].children
        , refreshChildren: refreshChildren	// method - pass a 'pane' and a layout-instance
            //	special pane option setting
        , enableClosable: enableClosable	// method - pass a 'pane'
        , disableClosable: disableClosable	// method - ditto
        , enableSlidable: enableSlidable	// method - ditto
        , disableSlidable: disableSlidable	// method - ditto
        , enableResizable: enableResizable	// method - ditto
        , disableResizable: disableResizable// method - ditto
            //	utility methods for panes
        , allowOverflow: allowOverflow	// utility - pass calling element (this)
        , resetOverflow: resetOverflow	// utility - ditto
            //	layout control
        , destroy: destroy			// method - no parameters
        , initPanes: isInitialized	// method - no parameters
        , resizeAll: resizeAll		// method - no parameters
            //	callback triggering
        , runCallbacks: _runCallbacks	// method - pass evtName & pane (if a pane-event), eg: trigger("onopen", "west")
            //	alias collections of options, state and children - created in addPane and extended elsewhere
        , hasParentLayout: false			// set by initContainer()
        , children: children		// pointers to child-layouts, eg: Instance.children.west.layoutName
        , north: false			// alias group: { name: pane, pane: $Ps[pane], options: options[pane], state: state[pane], children: children[pane] }
        , south: false			// ditto
        , west: false			// ditto
        , east: false			// ditto
        , center: false			// ditto
        };

        // create the border layout NOW
        if (_create() === 'cancel') // onload_start callback returned false to CANCEL layout creation
            return null;
        else // true OR false -- if layout-elements did NOT init (hidden or do not exist), can auto-init later
            return Instance; // return the Instance object

    }


})(jQuery);




/**
 * jquery.layout.state 1.2
 * $Date: 2014-08-30 08:00:00 (Sat, 30 Aug 2014) $
 *
 * Copyright (c) 2014 
 *   Kevin Dalman (http://allpro.net)
 *
 * Dual licensed under the GPL (http://www.gnu.org/licenses/gpl.html)
 * and MIT (http://www.opensource.org/licenses/mit-license.php) licenses.
 *
 * @requires: UI Layout 1.4.0 or higher
 * @requires: $.ui.cookie (above)
 *
 * @see: http://groups.google.com/group/jquery-ui-layout
 */
; (function ($) {

    if (!$.layout) return;


    /**
     *	UI COOKIE UTILITY
     *
     *	A $.cookie OR $.ui.cookie namespace *should be standard*, but until then...
     *	This creates $.ui.cookie so Layout does not need the cookie.jquery.js plugin
     *	NOTE: This utility is REQUIRED by the layout.state plugin
     *
     *	Cookie methods in Layout are created as part of State Management 
     */
    if (!$.ui) $.ui = {};
    $.ui.cookie = {

        // cookieEnabled is not in DOM specs, but DOES works in all browsers,including IE6
        acceptsCookies: !!navigator.cookieEnabled

    , read: function (name) {
        var
			c = document.cookie
		, cs = c ? c.split(';') : []
		, pair, data, i
        ;
        for (i = 0; pair = cs[i]; i++) {
            data = $.trim(pair).split('='); // name=value => [ name, value ]
            if (data[0] == name) // found the layout cookie
                return decodeURIComponent(data[1]);
        }
        return null;
    }

    , write: function (name, val, cookieOpts) {
        var params = ""
		, date = ""
		, clear = false
		, o = cookieOpts || {}
		, x = o.expires || null
		, t = $.type(x)
        ;
        if (t === "date")
            date = x;
        else if (t === "string" && x > 0) {
            x = parseInt(x, 10);
            t = "number";
        }
        if (t === "number") {
            date = new Date();
            if (x > 0)
                date.setDate(date.getDate() + x);
            else {
                date.setFullYear(1970);
                clear = true;
            }
        }
        if (date) params += ";expires=" + date.toUTCString();
        if (o.path) params += ";path=" + o.path;
        if (o.domain) params += ";domain=" + o.domain;
        if (o.secure) params += ";secure";
        document.cookie = name + "=" + (clear ? "" : encodeURIComponent(val)) + params; // write or clear cookie
    }

    , clear: function (name) {
        $.ui.cookie.write(name, "", { expires: -1 });
    }

    };
    // if cookie.jquery.js is not loaded, create an alias to replicate it
    // this may be useful to other plugins or code dependent on that plugin
    if (!$.cookie) $.cookie = function (k, v, o) {
        var C = $.ui.cookie;
        if (v === null)
            C.clear(k);
        else if (v === undefined)
            return C.read(k);
        else
            C.write(k, v, o);
    };



    /**
     *	State-management options stored in options.stateManagement, which includes a .cookie hash
     *	Default options saves ALL KEYS for ALL PANES, ie: pane.size, pane.isClosed, pane.isHidden
     *
     *	// STATE/COOKIE OPTIONS
     *	@example $(el).layout({
                    stateManagement: {
                        enabled:	true
                    ,	stateKeys:	"east.size,west.size,east.isClosed,west.isClosed"
                    ,	cookie:		{ name: "appLayout", path: "/" }
                    }
                })
     *	@example $(el).layout({ stateManagement__enabled: true }) // enable auto-state-management using cookies
     *	@example $(el).layout({ stateManagement__cookie: { name: "appLayout", path: "/" } })
     *	@example $(el).layout({ stateManagement__cookie__name: "appLayout", stateManagement__cookie__path: "/" })
     *
     *	// STATE/COOKIE METHODS
     *	@example myLayout.saveCookie( "west.isClosed,north.size,south.isHidden", {expires: 7} );
     *	@example myLayout.loadCookie();
     *	@example myLayout.deleteCookie();
     *	@example var JSON = myLayout.readState();	// CURRENT Layout State
     *	@example var JSON = myLayout.readCookie();	// SAVED Layout State (from cookie)
     *	@example var JSON = myLayout.state.stateData;	// LAST LOADED Layout State (cookie saved in layout.state hash)
     *
     *	CUSTOM STATE-MANAGEMENT (eg, saved in a database)
     *	@example var JSON = myLayout.readState( "west.isClosed,north.size,south.isHidden" );
     *	@example myLayout.loadState( JSON );
     */

    // tell Layout that the state plugin is available
    $.layout.plugins.stateManagement = true;

    //	Add State-Management options to layout.defaults
    $.layout.defaults.stateManagement = {
        enabled: false	// true = enable state-management, even if not using cookies
    , autoSave: true	// Save a state-cookie when page exits?
    , autoLoad: true	// Load the state-cookie when Layout inits?
    , animateLoad: true	// animate panes when loading state into an active layout
    , includeChildren: true	// recurse into child layouts to include their state as well
        // List state-data to save - must be pane-specific
    , stateKeys: "north.size,south.size,east.size,west.size," +
                    "north.isClosed,south.isClosed,east.isClosed,west.isClosed," +
                    "north.isHidden,south.isHidden,east.isHidden,west.isHidden"
    , cookie: {
        name: ""	// If not specified, will use Layout.name, else just "Layout"
        , domain: ""	// blank = current domain
        , path: ""	// blank = current page, "/" = entire website
        , expires: ""	// 'days' to keep cookie - leave blank for 'session cookie'
        , secure: false
    }
    };

    // Set stateManagement as a 'layout-option', NOT a 'pane-option'
    $.layout.optionsMap.layout.push("stateManagement");
    // Update config so layout does not move options into the pane-default branch (panes)
    $.layout.config.optionRootKeys.push("stateManagement");

    /*
     *	State Management methods
     */
    $.layout.state = {

        /**
         * Get the current layout state and save it to a cookie
         *
         * myLayout.saveCookie( keys, cookieOpts )
         *
         * @param {Object}			inst
         * @param {(string|Array)=}	keys
         * @param {Object=}			cookieOpts
         */
        saveCookie: function (inst, keys, cookieOpts) {
            var o = inst.options
            , sm = o.stateManagement
            , oC = $.extend(true, {}, sm.cookie, cookieOpts || null)
            , data = inst.state.stateData = inst.readState(keys || sm.stateKeys) // read current panes-state
            ;
            $.ui.cookie.write(oC.name || o.name || "Layout", $.layout.state.encodeJSON(data), oC);
            return $.extend(true, {}, data); // return COPY of state.stateData data
        }

        /**
         * Remove the state cookie
         *
         * @param {Object}	inst
         */
    , deleteCookie: function (inst) {
        var o = inst.options;
        $.ui.cookie.clear(o.stateManagement.cookie.name || o.name || "Layout");
    }

        /**
         * Read & return data from the cookie - as JSON
         *
         * @param {Object}	inst
         */
    , readCookie: function (inst) {
        var o = inst.options;
        var c = $.ui.cookie.read(o.stateManagement.cookie.name || o.name || "Layout");
        // convert cookie string back to a hash and return it
        return c ? $.layout.state.decodeJSON(c) : {};
    }

        /**
         * Get data from the cookie and USE IT to loadState
         *
         * @param {Object}	inst
         */
    , loadCookie: function (inst) {
        var c = $.layout.state.readCookie(inst); // READ the cookie
        if (c && !$.isEmptyObject(c)) {
            inst.state.stateData = $.extend(true, {}, c); // SET state.stateData
            inst.loadState(c); // LOAD the retrieved state
        }
        return c;
    }

        /**
         * Update layout options from the cookie, if one exists
         *
         * @param {Object}		inst
         * @param {Object=}		stateData
         * @param {boolean=}	animate
         */
    , loadState: function (inst, data, opts) {
        if (!$.isPlainObject(data) || $.isEmptyObject(data)) return;

        // normalize data & cache in the state object
        data = inst.state.stateData = $.layout.transformData(data); // panes = default subkey

        // add missing/default state-restore options
        var smo = inst.options.stateManagement;
        opts = $.extend({
            animateLoad: false //smo.animateLoad
		, includeChildren: smo.includeChildren
        }, opts);

        if (!inst.state.initialized) {
            /*
			 *	layout NOT initialized, so just update its options
			 */
            // MUST remove pane.children keys before applying to options
            // use a copy so we don't remove keys from original data
            var o = $.extend(true, {}, data);
            //delete o.center; // center has no state-data - only children
            $.each($.layout.config.allPanes, function (idx, pane) {
                if (o[pane]) delete o[pane].children;
            });
            // update CURRENT layout-options with saved state data
            $.extend(true, inst.options, o);
        }
        else {
            /*
			 *	layout already initialized, so modify layout's configuration
			 */
            var noAnimate = !opts.animateLoad
			, o, c, h, state, open
            ;
            $.each($.layout.config.borderPanes, function (idx, pane) {
                o = data[pane];
                if (!$.isPlainObject(o)) return; // no key, skip pane

                s = o.size;
                c = o.initClosed;
                h = o.initHidden;
                ar = o.autoResize
                state = inst.state[pane];
                open = state.isVisible;

                // reset autoResize
                if (ar)
                    state.autoResize = ar;
                // resize BEFORE opening
                if (!open)
                    inst._sizePane(pane, s, false, false, false); // false=skipCallback/noAnimation/forceResize
                // open/close as necessary - DO NOT CHANGE THIS ORDER!
                if (h === true) inst.hide(pane, noAnimate);
                else if (c === true) inst.close(pane, false, noAnimate);
                else if (c === false) inst.open(pane, false, noAnimate);
                else if (h === false) inst.show(pane, false, noAnimate);
                // resize AFTER any other actions
                if (open)
                    inst._sizePane(pane, s, false, false, noAnimate); // animate resize if option passed
            });

            /*
			 *	RECURSE INTO CHILD-LAYOUTS
			 */
            if (opts.includeChildren) {
                var paneStateChildren, childState;
                $.each(inst.children, function (pane, paneChildren) {
                    paneStateChildren = data[pane] ? data[pane].children : 0;
                    if (paneStateChildren && paneChildren) {
                        $.each(paneChildren, function (stateKey, child) {
                            childState = paneStateChildren[stateKey];
                            if (child && childState)
                                child.loadState(childState);
                        });
                    }
                });
            }
        }
    }

        /**
         * Get the *current layout state* and return it as a hash
         *
         * @param {Object=}		inst	// Layout instance to get state for
         * @param {object=}		[opts]	// State-Managements override options
         */
    , readState: function (inst, opts) {
        // backward compatility
        if ($.type(opts) === 'string') opts = { keys: opts };
        if (!opts) opts = {};
        var sm = inst.options.stateManagement
		, ic = opts.includeChildren
		, recurse = ic !== undefined ? ic : sm.includeChildren
		, keys = opts.stateKeys || sm.stateKeys
		, alt = { isClosed: 'initClosed', isHidden: 'initHidden' }
		, state = inst.state
		, panes = $.layout.config.allPanes
		, data = {}
		, pair, pane, key, val
		, ps, pC, child, array, count, branch
        ;
        if ($.isArray(keys)) keys = keys.join(",");
        // convert keys to an array and change delimiters from '__' to '.'
        keys = keys.replace(/__/g, ".").split(',');
        // loop keys and create a data hash
        for (var i = 0, n = keys.length; i < n; i++) {
            pair = keys[i].split(".");
            pane = pair[0];
            key = pair[1];
            if ($.inArray(pane, panes) < 0) continue; // bad pane!
            val = state[pane][key];
            if (val == undefined) continue;
            if (key == "isClosed" && state[pane]["isSliding"])
                val = true; // if sliding, then *really* isClosed
            (data[pane] || (data[pane] = {}))[alt[key] ? alt[key] : key] = val;
        }

        // recurse into the child-layouts for each pane
        if (recurse) {
            $.each(panes, function (idx, pane) {
                pC = inst.children[pane];
                ps = state.stateData[pane];
                if ($.isPlainObject(pC) && !$.isEmptyObject(pC)) {
                    // ensure a key exists for this 'pane', eg: branch = data.center
                    branch = data[pane] || (data[pane] = {});
                    if (!branch.children) branch.children = {};
                    $.each(pC, function (key, child) {
                        // ONLY read state from an initialize layout
                        if (child.state.initialized)
                            branch.children[key] = $.layout.state.readState(child);
                            // if we have PREVIOUS (onLoad) state for this child-layout, KEEP IT!
                        else if (ps && ps.children && ps.children[key]) {
                            branch.children[key] = $.extend(true, {}, ps.children[key]);
                        }
                    });
                }
            });
        }

        return data;
    }

        /**
         *	Stringify a JSON hash so can save in a cookie or db-field
         */
    , encodeJSON: function (json) {
        var local = window.JSON || {};
        return (local.stringify || stringify)(json);

        function stringify(h) {
            var D = [], i = 0, k, v, t // k = key, v = value
			, a = $.isArray(h)
            ;
            for (k in h) {
                v = h[k];
                t = typeof v;
                if (t == 'string')		// STRING - add quotes
                    v = '"' + v + '"';
                else if (t == 'object')	// SUB-KEY - recurse into it
                    v = parse(v);
                D[i++] = (!a ? '"' + k + '":' : '') + v;
            }
            return (a ? '[' : '{') + D.join(',') + (a ? ']' : '}');
        };
    }

        /**
         *	Convert stringified JSON back to a hash object
         *	@see		$.parseJSON(), adding in jQuery 1.4.1
         */
    , decodeJSON: function (str) {
        try { return $.parseJSON ? $.parseJSON(str) : window["eval"]("(" + str + ")") || {}; }
        catch (e) { return {}; }
    }


    , _create: function (inst) {
        var s = $.layout.state
		, o = inst.options
		, sm = o.stateManagement
        ;
        //	ADD State-Management plugin methods to inst
        $.extend(inst, {
            //	readCookie - update options from cookie - returns hash of cookie data
            readCookie: function () { return s.readCookie(inst); }
            //	deleteCookie
       , deleteCookie: function () { s.deleteCookie(inst); }
            //	saveCookie - optionally pass keys-list and cookie-options (hash)
       , saveCookie: function (keys, cookieOpts) { return s.saveCookie(inst, keys, cookieOpts); }
            //	loadCookie - readCookie and use to loadState() - returns hash of cookie data
       , loadCookie: function () { return s.loadCookie(inst); }
            //	loadState - pass a hash of state to use to update options
       , loadState: function (stateData, opts) { s.loadState(inst, stateData, opts); }
            //	readState - returns hash of current layout-state
       , readState: function (keys) { return s.readState(inst, keys); }
            //	add JSON utility methods too...
       , encodeJSON: s.encodeJSON
       , decodeJSON: s.decodeJSON
        });

        // init state.stateData key, even if plugin is initially disabled
        inst.state.stateData = {};

        // autoLoad MUST BE one of: data-array, data-hash, callback-function, or TRUE
        if (!sm.autoLoad) return;

        //	When state-data exists in the autoLoad key USE IT,
        //	even if stateManagement.enabled == false
        if ($.isPlainObject(sm.autoLoad)) {
            if (!$.isEmptyObject(sm.autoLoad)) {
                inst.loadState(sm.autoLoad);
            }
        }
        else if (sm.enabled) {
            // update the options from cookie or callback
            // if options is a function, call it to get stateData
            if ($.isFunction(sm.autoLoad)) {
                var d = {};
                try {
                    d = sm.autoLoad(inst, inst.state, inst.options, inst.options.name || ''); // try to get data from fn
                } catch (e) { }
                if (d && $.isPlainObject(d) && !$.isEmptyObject(d))
                    inst.loadState(d);
            }
            else // any other truthy value will trigger loadCookie
                inst.loadCookie();
        }
    }

    , _unload: function (inst) {
        var sm = inst.options.stateManagement;
        if (sm.enabled && sm.autoSave) {
            // if options is a function, call it to save the stateData
            if ($.isFunction(sm.autoSave)) {
                try {
                    sm.autoSave(inst, inst.state, inst.options, inst.options.name || ''); // try to get data from fn
                } catch (e) { }
            }
            else // any truthy value will trigger saveCookie
                inst.saveCookie();
        }
    }

    };

    // add state initialization method to Layout's onCreate array of functions
    $.layout.onCreate.push($.layout.state._create);
    $.layout.onUnload.push($.layout.state._unload);

})(jQuery);



/**
 * @preserve jquery.layout.buttons 1.0
 * $Date: 2011-07-16 08:00:00 (Sat, 16 July 2011) $
 *
 * Copyright (c) 2011 
 *   Kevin Dalman (http://allpro.net)
 *
 * Dual licensed under the GPL (http://www.gnu.org/licenses/gpl.html)
 * and MIT (http://www.opensource.org/licenses/mit-license.php) licenses.
 *
 * @dependancies: UI Layout 1.3.0.rc30.1 or higher
 *
 * @support: http://groups.google.com/group/jquery-ui-layout
 *
 * Docs: [ to come ]
 * Tips: [ to come ]
 */
; (function ($) {

    if (!$.layout) return;


    // tell Layout that the state plugin is available
    $.layout.plugins.buttons = true;

    //	Add State-Management options to layout.defaults
    $.layout.defaults.autoBindCustomButtons = false;
    // Set stateManagement as a layout-option, NOT a pane-option
    $.layout.optionsMap.layout.push("autoBindCustomButtons");

    /*
     *	Button methods
     */
    $.layout.buttons = {
        // set data used by multiple methods below
        config: {
            borderPanes: "north,south,west,east"
        }

        /**
        * Searches for .ui-layout-button-xxx elements and auto-binds them as layout-buttons
        *
        * @see  _create()
        */
    , init: function (inst) {
        var pre = "ui-layout-button-"
		, layout = inst.options.name || ""
		, name;
        $.each("toggle,open,close,pin,toggle-slide,open-slide".split(","), function (i, action) {
            $.each($.layout.buttons.config.borderPanes.split(","), function (ii, pane) {
                $("." + pre + action + "-" + pane).each(function () {
                    // if button was previously 'bound', data.layoutName was set, but is blank if layout has no 'name'
                    name = $(this).data("layoutName") || $(this).attr("layoutName");
                    if (name == undefined || name === layout)
                        inst.bindButton(this, action, pane);
                });
            });
        });
    }

        /**
        * Helper function to validate params received by addButton utilities
        *
        * Two classes are added to the element, based on the buttonClass...
        * The type of button is appended to create the 2nd className:
        *  - ui-layout-button-pin
        *  - ui-layout-pane-button-toggle
        *  - ui-layout-pane-button-open
        *  - ui-layout-pane-button-close
        *
        * @param  {(string|!Object)}	selector	jQuery selector (or element) for button, eg: ".ui-layout-north .toggle-button"
        * @param  {string}   			pane 		Name of the pane the button is for: 'north', 'south', etc.
        * @return {Array.<Object>}		If both params valid, the element matching 'selector' in a jQuery wrapper - otherwise returns null
        */
    , get: function (inst, selector, pane, action) {
        var $E = $(selector)
		, o = inst.options
        //,	err	= o.showErrorMessages
        ;
        if ($E.length && $.layout.buttons.config.borderPanes.indexOf(pane) >= 0) {
            var btn = o[pane].buttonClass + "-" + action;
            $E.addClass(btn + " " + btn + "-" + pane)
				.data("layoutName", o.name); // add layout identifier - even if blank!
        }
        return $E;
    }


        /**
        * NEW syntax for binding layout-buttons - will eventually replace addToggle, addOpen, etc.
        *
        * @param {(string|!Object)}	sel		jQuery selector (or element) for button, eg: ".ui-layout-north .toggle-button"
        * @param {string}			action
        * @param {string}			pane
        */
    , bind: function (inst, sel, action, pane) {
        var _ = $.layout.buttons;
        switch (action.toLowerCase()) {
            case "toggle": _.addToggle(inst, sel, pane); break;
            case "open": _.addOpen(inst, sel, pane); break;
            case "close": _.addClose(inst, sel, pane); break;
            case "pin": _.addPin(inst, sel, pane); break;
            case "toggle-slide": _.addToggle(inst, sel, pane, true); break;
            case "open-slide": _.addOpen(inst, sel, pane, true); break;
        }
        return inst;
    }

        /**
        * Add a custom Toggler button for a pane
        *
        * @param {(string|!Object)}	selector	jQuery selector (or element) for button, eg: ".ui-layout-north .toggle-button"
        * @param {string}  			pane 		Name of the pane the button is for: 'north', 'south', etc.
        * @param {boolean=}			slide 		true = slide-open, false = pin-open
        */
    , addToggle: function (inst, selector, pane, slide) {
        $.layout.buttons.get(inst, selector, pane, "toggle")
			.click(function (evt) {
			    inst.toggle(pane, !!slide);
			    evt.stopPropagation();
			});
        return inst;
    }

        /**
        * Add a custom Open button for a pane
        *
        * @param {(string|!Object)}	selector	jQuery selector (or element) for button, eg: ".ui-layout-north .toggle-button"
        * @param {string}			pane 		Name of the pane the button is for: 'north', 'south', etc.
        * @param {boolean=}			slide 		true = slide-open, false = pin-open
        */
    , addOpen: function (inst, selector, pane, slide) {
        $.layout.buttons.get(inst, selector, pane, "open")
			.attr("title", inst.options[pane].tips.Open)
			.click(function (evt) {
			    inst.open(pane, !!slide);
			    evt.stopPropagation();
			});
        return inst;
    }

        /**
        * Add a custom Close button for a pane
        *
        * @param {(string|!Object)}	selector	jQuery selector (or element) for button, eg: ".ui-layout-north .toggle-button"
        * @param {string}   		pane 		Name of the pane the button is for: 'north', 'south', etc.
        */
    , addClose: function (inst, selector, pane) {
        $.layout.buttons.get(inst, selector, pane, "close")
			.attr("title", inst.options[pane].tips.Close)
			.click(function (evt) {
			    inst.close(pane);
			    evt.stopPropagation();
			});
        return inst;
    }

        /**
        * Add a custom Pin button for a pane
        *
        * Four classes are added to the element, based on the paneClass for the associated pane...
        * Assuming the default paneClass and the pin is 'up', these classes are added for a west-pane pin:
        *  - ui-layout-pane-pin
        *  - ui-layout-pane-west-pin
        *  - ui-layout-pane-pin-up
        *  - ui-layout-pane-west-pin-up
        *
        * @param {(string|!Object)}	selector	jQuery selector (or element) for button, eg: ".ui-layout-north .toggle-button"
        * @param {string}   		pane 		Name of the pane the pin is for: 'north', 'south', etc.
        */
    , addPin: function (inst, selector, pane) {
        var $E = $.layout.buttons.get(inst, selector, pane, "pin");
        if ($E.length) {
            var s = inst.state[pane];
            $E.click(function (evt) {
                $.layout.buttons.setPinState(inst, $(this), pane, (s.isSliding || s.isClosed));
                if (s.isSliding || s.isClosed) inst.open(pane); // change from sliding to open
                else inst.close(pane); // slide-closed
                evt.stopPropagation();
            });
            // add up/down pin attributes and classes
            $.layout.buttons.setPinState(inst, $E, pane, (!s.isClosed && !s.isSliding));
            // add this pin to the pane data so we can 'sync it' automatically
            // PANE.pins key is an array so we can store multiple pins for each pane
            s.pins.push(selector); // just save the selector string
        }
        return inst;
    }

        /**
        * Change the class of the pin button to make it look 'up' or 'down'
        *
        * @see  addPin(), syncPins()
        * @param {Array.<Object>}	$Pin	The pin-span element in a jQuery wrapper
        * @param {string}	pane	These are the params returned to callbacks by layout()
        * @param {boolean}	doPin	true = set the pin 'down', false = set it 'up'
        */
    , setPinState: function (inst, $Pin, pane, doPin) {
        var updown = $Pin.attr("pin");
        if (updown && doPin === (updown == "down")) return; // already in correct state
        var
			po = inst.options[pane]
		, lang = po.tips
		, pin = po.buttonClass + "-pin"
		, side = pin + "-" + pane
		, UP = pin + "-up " + side + "-up"
		, DN = pin + "-down " + side + "-down"
        ;
        $Pin
			.attr("pin", doPin ? "down" : "up") // logic
			.attr("title", doPin ? lang.Unpin : lang.Pin)
			.removeClass(doPin ? UP : DN)
			.addClass(doPin ? DN : UP)
        ;
    }

        /**
        * INTERNAL function to sync 'pin buttons' when pane is opened or closed
        * Unpinned means the pane is 'sliding' - ie, over-top of the adjacent panes
        *
        * @see  open(), close()
        * @param {string}	pane   These are the params returned to callbacks by layout()
        * @param {boolean}	doPin  True means set the pin 'down', False means 'up'
        */
    , syncPinBtns: function (inst, pane, doPin) {
        // REAL METHOD IS _INSIDE_ LAYOUT - THIS IS HERE JUST FOR REFERENCE
        $.each(state[pane].pins, function (i, selector) {
            $.layout.buttons.setPinState(inst, $(selector), pane, doPin);
        });
    }


    , _load: function (inst) {
        //	ADD Button methods to Layout Instance
        $.extend(inst, {
            bindButton: function (selector, action, pane) { return $.layout.buttons.bind(inst, selector, action, pane); }
            //	DEPRECATED METHODS...
		, addToggleBtn: function (selector, pane, slide) { return $.layout.buttons.addToggle(inst, selector, pane, slide); }
		, addOpenBtn: function (selector, pane, slide) { return $.layout.buttons.addOpen(inst, selector, pane, slide); }
		, addCloseBtn: function (selector, pane) { return $.layout.buttons.addClose(inst, selector, pane); }
		, addPinBtn: function (selector, pane) { return $.layout.buttons.addPin(inst, selector, pane); }
        });

        // init state array to hold pin-buttons
        for (var i = 0; i < 4; i++) {
            var pane = $.layout.buttons.config.borderPanes[i];
            inst.state[pane].pins = [];
        }

        // auto-init buttons onLoad if option is enabled
        if (inst.options.autoBindCustomButtons)
            $.layout.buttons.init(inst);
    }

    , _unload: function (inst) {
        // TODO: unbind all buttons???
    }

    };

    // add initialization method to Layout's onLoad array of functions
    $.layout.onLoad.push($.layout.buttons._load);
    //$.layout.onUnload.push( $.layout.buttons._unload );

})(jQuery);




/**
 * jquery.layout.browserZoom 1.0
 * $Date: 2011-12-29 08:00:00 (Thu, 29 Dec 2011) $
 *
 * Copyright (c) 2012 
 *   Kevin Dalman (http://allpro.net)
 *
 * Dual licensed under the GPL (http://www.gnu.org/licenses/gpl.html)
 * and MIT (http://www.opensource.org/licenses/mit-license.php) licenses.
 *
 * @requires: UI Layout 1.3.0.rc30.1 or higher
 *
 * @see: http://groups.google.com/group/jquery-ui-layout
 *
 * TODO: Extend logic to handle other problematic zooming in browsers
 * TODO: Add hotkey/mousewheel bindings to _instantly_ respond to these zoom event
 */
(function ($) {

    // tell Layout that the plugin is available
    $.layout.plugins.browserZoom = true;

    $.layout.defaults.browserZoomCheckInterval = 1000;
    $.layout.optionsMap.layout.push("browserZoomCheckInterval");

    /*
     *	browserZoom methods
     */
    $.layout.browserZoom = {

        _init: function (inst) {
            // abort if browser does not need this check
            if ($.layout.browserZoom.ratio() !== false)
                $.layout.browserZoom._setTimer(inst);
        }

    , _setTimer: function (inst) {
        // abort if layout destroyed or browser does not need this check
        if (inst.destroyed) return;
        var o = inst.options
		, s = inst.state
		//	don't need check if inst has parentLayout, but check occassionally in case parent destroyed!
		//	MINIMUM 100ms interval, for performance
		, ms = inst.hasParentLayout ? 5000 : Math.max(o.browserZoomCheckInterval, 100)
        ;
        // set the timer
        setTimeout(function () {
            if (inst.destroyed || !o.resizeWithWindow) return;
            var d = $.layout.browserZoom.ratio();
            if (d !== s.browserZoom) {
                s.browserZoom = d;
                inst.resizeAll();
            }
            // set a NEW timeout
            $.layout.browserZoom._setTimer(inst);
        }
		, ms);
    }

    , ratio: function () {
        var w = window
		, s = screen
		, d = document
		, dE = d.documentElement || d.body
		, b = $.layout.browser
		, v = b.version
		, r, sW, cW
        ;
        // we can ignore all browsers that fire window.resize event onZoom
        if (!b.msie || v > 8)
            return false; // don't need to track zoom
        if (s.deviceXDPI && s.systemXDPI) // syntax compiler hack
            return calc(s.deviceXDPI, s.systemXDPI);
        // everything below is just for future reference!
        if (b.webkit && (r = d.body.getBoundingClientRect))
            return calc((r.left - r.right), d.body.offsetWidth);
        if (b.webkit && (sW = w.outerWidth))
            return calc(sW, w.innerWidth);
        if ((sW = s.width) && (cW = dE.clientWidth))
            return calc(sW, cW);
        return false; // no match, so cannot - or don't need to - track zoom

        function calc(x, y) { return (parseInt(x, 10) / parseInt(y, 10) * 100).toFixed(); }
    }

    };
    // add initialization method to Layout's onLoad array of functions
    $.layout.onReady.push($.layout.browserZoom._init);


})(jQuery);




/**
 *	UI Layout Plugin: Slide-Offscreen Animation
 *
 *	Prevent panes from being 'hidden' so that an iframes/objects 
 *	does not reload/refresh when pane 'opens' again.
 *	This plug-in adds a new animation called "slideOffscreen".
 *	It is identical to the normal "slide" effect, but avoids hiding the element
 *
 *	Requires Layout 1.3.0.RC30.1 or later for Close offscreen
 *	Requires Layout 1.3.0.RC30.5 or later for Hide, initClosed & initHidden offscreen
 *
 *	Version:	1.1 - 2012-11-18
 *	Author:		Kevin Dalman (kevin@jquery-dev.com)
 *	@preserve	jquery.layout.slideOffscreen-1.1.js
 */
; (function ($) {

    // Add a new "slideOffscreen" effect
    if ($.effects) {

        // add an option so initClosed and initHidden will work
        $.layout.defaults.panes.useOffscreenClose = false; // user must enable when needed
        /* set the new animation as the default for all panes
        $.layout.defaults.panes.fxName = "slideOffscreen";
        */

        if ($.layout.plugins)
            $.layout.plugins.effects.slideOffscreen = true;

        // dupe 'slide' effect defaults as new effect defaults
        $.layout.effects.slideOffscreen = $.extend(true, {}, $.layout.effects.slide);

        // add new effect to jQuery UI
        $.effects.slideOffscreen = function (o) {
            return this.queue(function () {

                var fx = $.effects
                , opt = o.options
                , $el = $(this)
                , pane = $el.data('layoutEdge')
                , state = $el.data('parentLayout').state
                , dist = state[pane].size
                , s = this.style
                , props = ['top', 'bottom', 'left', 'right']
                    // Set options
                , mode = fx.setMode($el, opt.mode || 'show') // Set Mode
                , show = (mode == 'show')
                , dir = opt.direction || 'left' // Default Direction
                , ref = (dir == 'up' || dir == 'down') ? 'top' : 'left'
                , pos = (dir == 'up' || dir == 'left')
                , offscrn = $.layout.config.offscreenCSS || {}
                , keyLR = $.layout.config.offscreenReset
                , keyTB = 'offscreenResetTop' // only used internally
                , animation = {}
                ;
                // Animation settings
                animation[ref] = (show ? (pos ? '+=' : '-=') : (pos ? '-=' : '+=')) + dist;

                if (show) { // show() animation, so save top/bottom but retain left/right set when 'hidden'
                    $el.data(keyTB, { top: s.top, bottom: s.bottom });

                    // set the top or left offset in preparation for animation
                    // Note: ALL animations work by shifting the top or left edges
                    if (pos) { // top (north) or left (west)
                        $el.css(ref, isNaN(dist) ? "-" + dist : -dist); // Shift outside the left/top edge
                    }
                    else { // bottom (south) or right (east) - shift all the way across container
                        if (dir === 'right')
                            $el.css({ left: state.container.layoutWidth, right: 'auto' });
                        else // dir === bottom
                            $el.css({ top: state.container.layoutHeight, bottom: 'auto' });
                    }
                    // restore the left/right setting if is a top/bottom animation
                    if (ref === 'top')
                        $el.css($el.data(keyLR) || {});
                }
                else { // hide() animation, so save ALL CSS
                    $el.data(keyTB, { top: s.top, bottom: s.bottom });
                    $el.data(keyLR, { left: s.left, right: s.right });
                }

                // Animate
                $el.show().animate(animation, {
                    queue: false, duration: o.duration, easing: opt.easing, complete: function () {
                        // Restore top/bottom
                        if ($el.data(keyTB))
                            $el.css($el.data(keyTB)).removeData(keyTB);
                        if (show) // Restore left/right too
                            $el.css($el.data(keyLR) || {}).removeData(keyLR);
                        else // Move the pane off-screen (left: -99999, right: 'auto')
                            $el.css(offscrn);

                        if (o.callback) o.callback.apply(this, arguments); // Callback
                        $el.dequeue();
                    }
                });

            });
        };

    }

})(jQuery);

/* qTip2 v2.2.0 tips modal viewport svg imagemap ie6 | qtip2.com | Licensed MIT, GPL | Thu Nov 21 2013 20:34:59 */
(function(t,e,i){(function(t){"use strict";"function"==typeof define&&define.amd?define(["jquery"],t):jQuery&&!jQuery.fn.qtip&&t(jQuery)})(function(s){"use strict";function o(t,e,i,o){this.id=i,this.target=t,this.tooltip=E,this.elements={target:t},this._id=X+"-"+i,this.timers={img:{}},this.options=e,this.plugins={},this.cache={event:{},target:s(),disabled:k,attr:o,onTooltip:k,lastClass:""},this.rendered=this.destroyed=this.disabled=this.waiting=this.hiddenDuringWait=this.positioning=this.triggering=k}function n(t){return t===E||"object"!==s.type(t)}function r(t){return!(s.isFunction(t)||t&&t.attr||t.length||"object"===s.type(t)&&(t.jquery||t.then))}function a(t){var e,i,o,a;return n(t)?k:(n(t.metadata)&&(t.metadata={type:t.metadata}),"content"in t&&(e=t.content,n(e)||e.jquery||e.done?e=t.content={text:i=r(e)?k:e}:i=e.text,"ajax"in e&&(o=e.ajax,a=o&&o.once!==k,delete e.ajax,e.text=function(t,e){var n=i||s(this).attr(e.options.content.attr)||"Loading...",r=s.ajax(s.extend({},o,{context:e})).then(o.success,E,o.error).then(function(t){return t&&a&&e.set("content.text",t),t},function(t,i,s){e.destroyed||0===t.status||e.set("content.text",i+": "+s)});return a?n:(e.set("content.text",n),r)}),"title"in e&&(n(e.title)||(e.button=e.title.button,e.title=e.title.text),r(e.title||k)&&(e.title=k))),"position"in t&&n(t.position)&&(t.position={my:t.position,at:t.position}),"show"in t&&n(t.show)&&(t.show=t.show.jquery?{target:t.show}:t.show===W?{ready:W}:{event:t.show}),"hide"in t&&n(t.hide)&&(t.hide=t.hide.jquery?{target:t.hide}:{event:t.hide}),"style"in t&&n(t.style)&&(t.style={classes:t.style}),s.each(R,function(){this.sanitize&&this.sanitize(t)}),t)}function h(t,e){for(var i,s=0,o=t,n=e.split(".");o=o[n[s++]];)n.length>s&&(i=o);return[i||t,n.pop()]}function l(t,e){var i,s,o;for(i in this.checks)for(s in this.checks[i])(o=RegExp(s,"i").exec(t))&&(e.push(o),("builtin"===i||this.plugins[i])&&this.checks[i][s].apply(this.plugins[i]||this,e))}function c(t){return G.concat("").join(t?"-"+t+" ":" ")}function d(i){return i&&{type:i.type,pageX:i.pageX,pageY:i.pageY,target:i.target,relatedTarget:i.relatedTarget,scrollX:i.scrollX||t.pageXOffset||e.body.scrollLeft||e.documentElement.scrollLeft,scrollY:i.scrollY||t.pageYOffset||e.body.scrollTop||e.documentElement.scrollTop}||{}}function p(t,e){return e>0?setTimeout(s.proxy(t,this),e):(t.call(this),i)}function u(t){return this.tooltip.hasClass(ee)?k:(clearTimeout(this.timers.show),clearTimeout(this.timers.hide),this.timers.show=p.call(this,function(){this.toggle(W,t)},this.options.show.delay),i)}function f(t){if(this.tooltip.hasClass(ee))return k;var e=s(t.relatedTarget),i=e.closest(U)[0]===this.tooltip[0],o=e[0]===this.options.show.target[0];if(clearTimeout(this.timers.show),clearTimeout(this.timers.hide),this!==e[0]&&"mouse"===this.options.position.target&&i||this.options.hide.fixed&&/mouse(out|leave|move)/.test(t.type)&&(i||o))try{t.preventDefault(),t.stopImmediatePropagation()}catch(n){}else this.timers.hide=p.call(this,function(){this.toggle(k,t)},this.options.hide.delay,this)}function g(t){return this.tooltip.hasClass(ee)||!this.options.hide.inactive?k:(clearTimeout(this.timers.inactive),this.timers.inactive=p.call(this,function(){this.hide(t)},this.options.hide.inactive),i)}function m(t){this.rendered&&this.tooltip[0].offsetWidth>0&&this.reposition(t)}function v(t,i,o){s(e.body).delegate(t,(i.split?i:i.join(he+" "))+he,function(){var t=T.api[s.attr(this,H)];t&&!t.disabled&&o.apply(t,arguments)})}function y(t,i,n){var r,h,l,c,d,p=s(e.body),u=t[0]===e?p:t,f=t.metadata?t.metadata(n.metadata):E,g="html5"===n.metadata.type&&f?f[n.metadata.name]:E,m=t.data(n.metadata.name||"qtipopts");try{m="string"==typeof m?s.parseJSON(m):m}catch(v){}if(c=s.extend(W,{},T.defaults,n,"object"==typeof m?a(m):E,a(g||f)),h=c.position,c.id=i,"boolean"==typeof c.content.text){if(l=t.attr(c.content.attr),c.content.attr===k||!l)return k;c.content.text=l}if(h.container.length||(h.container=p),h.target===k&&(h.target=u),c.show.target===k&&(c.show.target=u),c.show.solo===W&&(c.show.solo=h.container.closest("body")),c.hide.target===k&&(c.hide.target=u),c.position.viewport===W&&(c.position.viewport=h.container),h.container=h.container.eq(0),h.at=new z(h.at,W),h.my=new z(h.my),t.data(X))if(c.overwrite)t.qtip("destroy",!0);else if(c.overwrite===k)return k;return t.attr(Y,i),c.suppress&&(d=t.attr("title"))&&t.removeAttr("title").attr(se,d).attr("title",""),r=new o(t,c,i,!!l),t.data(X,r),t.one("remove.qtip-"+i+" removeqtip.qtip-"+i,function(){var t;(t=s(this).data(X))&&t.destroy(!0)}),r}function b(t){return t.charAt(0).toUpperCase()+t.slice(1)}function w(t,e){var s,o,n=e.charAt(0).toUpperCase()+e.slice(1),r=(e+" "+be.join(n+" ")+n).split(" "),a=0;if(ye[e])return t.css(ye[e]);for(;s=r[a++];)if((o=t.css(s))!==i)return ye[e]=s,o}function _(t,e){return Math.ceil(parseFloat(w(t,e)))}function x(t,e){this._ns="tip",this.options=e,this.offset=e.offset,this.size=[e.width,e.height],this.init(this.qtip=t)}function q(t,e){this.options=e,this._ns="-modal",this.init(this.qtip=t)}function C(t){this._ns="ie6",this.init(this.qtip=t)}var T,j,z,M,I,W=!0,k=!1,E=null,S="x",L="y",A="width",B="height",D="top",F="left",O="bottom",P="right",N="center",$="flipinvert",V="shift",R={},X="qtip",Y="data-hasqtip",H="data-qtip-id",G=["ui-widget","ui-tooltip"],U="."+X,Q="click dblclick mousedown mouseup mousemove mouseleave mouseenter".split(" "),J=X+"-fixed",K=X+"-default",Z=X+"-focus",te=X+"-hover",ee=X+"-disabled",ie="_replacedByqTip",se="oldtitle",oe={ie:function(){for(var t=3,i=e.createElement("div");(i.innerHTML="<!--[if gt IE "+ ++t+"]><i></i><![endif]-->")&&i.getElementsByTagName("i")[0];);return t>4?t:0/0}(),iOS:parseFloat((""+(/CPU.*OS ([0-9_]{1,5})|(CPU like).*AppleWebKit.*Mobile/i.exec(navigator.userAgent)||[0,""])[1]).replace("undefined","3_2").replace("_",".").replace("_",""))||k};j=o.prototype,j._when=function(t){return s.when.apply(s,t)},j.render=function(t){if(this.rendered||this.destroyed)return this;var e,i=this,o=this.options,n=this.cache,r=this.elements,a=o.content.text,h=o.content.title,l=o.content.button,c=o.position,d=("."+this._id+" ",[]);return s.attr(this.target[0],"aria-describedby",this._id),this.tooltip=r.tooltip=e=s("<div/>",{id:this._id,"class":[X,K,o.style.classes,X+"-pos-"+o.position.my.abbrev()].join(" "),width:o.style.width||"",height:o.style.height||"",tracking:"mouse"===c.target&&c.adjust.mouse,role:"alert","aria-live":"polite","aria-atomic":k,"aria-describedby":this._id+"-content","aria-hidden":W}).toggleClass(ee,this.disabled).attr(H,this.id).data(X,this).appendTo(c.container).append(r.content=s("<div />",{"class":X+"-content",id:this._id+"-content","aria-atomic":W})),this.rendered=-1,this.positioning=W,h&&(this._createTitle(),s.isFunction(h)||d.push(this._updateTitle(h,k))),l&&this._createButton(),s.isFunction(a)||d.push(this._updateContent(a,k)),this.rendered=W,this._setWidget(),s.each(R,function(t){var e;"render"===this.initialize&&(e=this(i))&&(i.plugins[t]=e)}),this._unassignEvents(),this._assignEvents(),this._when(d).then(function(){i._trigger("render"),i.positioning=k,i.hiddenDuringWait||!o.show.ready&&!t||i.toggle(W,n.event,k),i.hiddenDuringWait=k}),T.api[this.id]=this,this},j.destroy=function(t){function e(){if(!this.destroyed){this.destroyed=W;var t=this.target,e=t.attr(se);this.rendered&&this.tooltip.stop(1,0).find("*").remove().end().remove(),s.each(this.plugins,function(){this.destroy&&this.destroy()}),clearTimeout(this.timers.show),clearTimeout(this.timers.hide),this._unassignEvents(),t.removeData(X).removeAttr(H).removeAttr(Y).removeAttr("aria-describedby"),this.options.suppress&&e&&t.attr("title",e).removeAttr(se),this._unbind(t),this.options=this.elements=this.cache=this.timers=this.plugins=this.mouse=E,delete T.api[this.id]}}return this.destroyed?this.target:(t===W&&"hide"!==this.triggering||!this.rendered?e.call(this):(this.tooltip.one("tooltiphidden",s.proxy(e,this)),!this.triggering&&this.hide()),this.target)},M=j.checks={builtin:{"^id$":function(t,e,i,o){var n=i===W?T.nextid:i,r=X+"-"+n;n!==k&&n.length>0&&!s("#"+r).length?(this._id=r,this.rendered&&(this.tooltip[0].id=this._id,this.elements.content[0].id=this._id+"-content",this.elements.title[0].id=this._id+"-title")):t[e]=o},"^prerender":function(t,e,i){i&&!this.rendered&&this.render(this.options.show.ready)},"^content.text$":function(t,e,i){this._updateContent(i)},"^content.attr$":function(t,e,i,s){this.options.content.text===this.target.attr(s)&&this._updateContent(this.target.attr(i))},"^content.title$":function(t,e,s){return s?(s&&!this.elements.title&&this._createTitle(),this._updateTitle(s),i):this._removeTitle()},"^content.button$":function(t,e,i){this._updateButton(i)},"^content.title.(text|button)$":function(t,e,i){this.set("content."+e,i)},"^position.(my|at)$":function(t,e,i){"string"==typeof i&&(t[e]=new z(i,"at"===e))},"^position.container$":function(t,e,i){this.rendered&&this.tooltip.appendTo(i)},"^show.ready$":function(t,e,i){i&&(!this.rendered&&this.render(W)||this.toggle(W))},"^style.classes$":function(t,e,i,s){this.rendered&&this.tooltip.removeClass(s).addClass(i)},"^style.(width|height)":function(t,e,i){this.rendered&&this.tooltip.css(e,i)},"^style.widget|content.title":function(){this.rendered&&this._setWidget()},"^style.def":function(t,e,i){this.rendered&&this.tooltip.toggleClass(K,!!i)},"^events.(render|show|move|hide|focus|blur)$":function(t,e,i){this.rendered&&this.tooltip[(s.isFunction(i)?"":"un")+"bind"]("tooltip"+e,i)},"^(show|hide|position).(event|target|fixed|inactive|leave|distance|viewport|adjust)":function(){if(this.rendered){var t=this.options.position;this.tooltip.attr("tracking","mouse"===t.target&&t.adjust.mouse),this._unassignEvents(),this._assignEvents()}}}},j.get=function(t){if(this.destroyed)return this;var e=h(this.options,t.toLowerCase()),i=e[0][e[1]];return i.precedance?i.string():i};var ne=/^position\.(my|at|adjust|target|container|viewport)|style|content|show\.ready/i,re=/^prerender|show\.ready/i;j.set=function(t,e){if(this.destroyed)return this;var o,n=this.rendered,r=k,c=this.options;return this.checks,"string"==typeof t?(o=t,t={},t[o]=e):t=s.extend({},t),s.each(t,function(e,o){if(n&&re.test(e))return delete t[e],i;var a,l=h(c,e.toLowerCase());a=l[0][l[1]],l[0][l[1]]=o&&o.nodeType?s(o):o,r=ne.test(e)||r,t[e]=[l[0],l[1],o,a]}),a(c),this.positioning=W,s.each(t,s.proxy(l,this)),this.positioning=k,this.rendered&&this.tooltip[0].offsetWidth>0&&r&&this.reposition("mouse"===c.position.target?E:this.cache.event),this},j._update=function(t,e){var i=this,o=this.cache;return this.rendered&&t?(s.isFunction(t)&&(t=t.call(this.elements.target,o.event,this)||""),s.isFunction(t.then)?(o.waiting=W,t.then(function(t){return o.waiting=k,i._update(t,e)},E,function(t){return i._update(t,e)})):t===k||!t&&""!==t?k:(t.jquery&&t.length>0?e.empty().append(t.css({display:"block",visibility:"visible"})):e.html(t),this._waitForContent(e).then(function(t){t.images&&t.images.length&&i.rendered&&i.tooltip[0].offsetWidth>0&&i.reposition(o.event,!t.length)}))):k},j._waitForContent=function(t){var e=this.cache;return e.waiting=W,(s.fn.imagesLoaded?t.imagesLoaded():s.Deferred().resolve([])).done(function(){e.waiting=k}).promise()},j._updateContent=function(t,e){this._update(t,this.elements.content,e)},j._updateTitle=function(t,e){this._update(t,this.elements.title,e)===k&&this._removeTitle(k)},j._createTitle=function(){var t=this.elements,e=this._id+"-title";t.titlebar&&this._removeTitle(),t.titlebar=s("<div />",{"class":X+"-titlebar "+(this.options.style.widget?c("header"):"")}).append(t.title=s("<div />",{id:e,"class":X+"-title","aria-atomic":W})).insertBefore(t.content).delegate(".qtip-close","mousedown keydown mouseup keyup mouseout",function(t){s(this).toggleClass("ui-state-active ui-state-focus","down"===t.type.substr(-4))}).delegate(".qtip-close","mouseover mouseout",function(t){s(this).toggleClass("ui-state-hover","mouseover"===t.type)}),this.options.content.button&&this._createButton()},j._removeTitle=function(t){var e=this.elements;e.title&&(e.titlebar.remove(),e.titlebar=e.title=e.button=E,t!==k&&this.reposition())},j.reposition=function(i,o){if(!this.rendered||this.positioning||this.destroyed)return this;this.positioning=W;var n,r,a=this.cache,h=this.tooltip,l=this.options.position,c=l.target,d=l.my,p=l.at,u=l.viewport,f=l.container,g=l.adjust,m=g.method.split(" "),v=h.outerWidth(k),y=h.outerHeight(k),b=0,w=0,_=h.css("position"),x={left:0,top:0},q=h[0].offsetWidth>0,C=i&&"scroll"===i.type,T=s(t),j=f[0].ownerDocument,z=this.mouse;if(s.isArray(c)&&2===c.length)p={x:F,y:D},x={left:c[0],top:c[1]};else if("mouse"===c)p={x:F,y:D},!z||!z.pageX||!g.mouse&&i&&i.pageX?i&&i.pageX||((!g.mouse||this.options.show.distance)&&a.origin&&a.origin.pageX?i=a.origin:(!i||i&&("resize"===i.type||"scroll"===i.type))&&(i=a.event)):i=z,"static"!==_&&(x=f.offset()),j.body.offsetWidth!==(t.innerWidth||j.documentElement.clientWidth)&&(r=s(e.body).offset()),x={left:i.pageX-x.left+(r&&r.left||0),top:i.pageY-x.top+(r&&r.top||0)},g.mouse&&C&&z&&(x.left-=(z.scrollX||0)-T.scrollLeft(),x.top-=(z.scrollY||0)-T.scrollTop());else{if("event"===c?i&&i.target&&"scroll"!==i.type&&"resize"!==i.type?a.target=s(i.target):i.target||(a.target=this.elements.target):"event"!==c&&(a.target=s(c.jquery?c:this.elements.target)),c=a.target,c=s(c).eq(0),0===c.length)return this;c[0]===e||c[0]===t?(b=oe.iOS?t.innerWidth:c.width(),w=oe.iOS?t.innerHeight:c.height(),c[0]===t&&(x={top:(u||c).scrollTop(),left:(u||c).scrollLeft()})):R.imagemap&&c.is("area")?n=R.imagemap(this,c,p,R.viewport?m:k):R.svg&&c&&c[0].ownerSVGElement?n=R.svg(this,c,p,R.viewport?m:k):(b=c.outerWidth(k),w=c.outerHeight(k),x=c.offset()),n&&(b=n.width,w=n.height,r=n.offset,x=n.position),x=this.reposition.offset(c,x,f),(oe.iOS>3.1&&4.1>oe.iOS||oe.iOS>=4.3&&4.33>oe.iOS||!oe.iOS&&"fixed"===_)&&(x.left-=T.scrollLeft(),x.top-=T.scrollTop()),(!n||n&&n.adjustable!==k)&&(x.left+=p.x===P?b:p.x===N?b/2:0,x.top+=p.y===O?w:p.y===N?w/2:0)}return x.left+=g.x+(d.x===P?-v:d.x===N?-v/2:0),x.top+=g.y+(d.y===O?-y:d.y===N?-y/2:0),R.viewport?(x.adjusted=R.viewport(this,x,l,b,w,v,y),r&&x.adjusted.left&&(x.left+=r.left),r&&x.adjusted.top&&(x.top+=r.top)):x.adjusted={left:0,top:0},this._trigger("move",[x,u.elem||u],i)?(delete x.adjusted,o===k||!q||isNaN(x.left)||isNaN(x.top)||"mouse"===c||!s.isFunction(l.effect)?h.css(x):s.isFunction(l.effect)&&(l.effect.call(h,this,s.extend({},x)),h.queue(function(t){s(this).css({opacity:"",height:""}),oe.ie&&this.style.removeAttribute("filter"),t()})),this.positioning=k,this):this},j.reposition.offset=function(t,i,o){function n(t,e){i.left+=e*t.scrollLeft(),i.top+=e*t.scrollTop()}if(!o[0])return i;var r,a,h,l,c=s(t[0].ownerDocument),d=!!oe.ie&&"CSS1Compat"!==e.compatMode,p=o[0];do"static"!==(a=s.css(p,"position"))&&("fixed"===a?(h=p.getBoundingClientRect(),n(c,-1)):(h=s(p).position(),h.left+=parseFloat(s.css(p,"borderLeftWidth"))||0,h.top+=parseFloat(s.css(p,"borderTopWidth"))||0),i.left-=h.left+(parseFloat(s.css(p,"marginLeft"))||0),i.top-=h.top+(parseFloat(s.css(p,"marginTop"))||0),r||"hidden"===(l=s.css(p,"overflow"))||"visible"===l||(r=s(p)));while(p=p.offsetParent);return r&&(r[0]!==c[0]||d)&&n(r,1),i};var ae=(z=j.reposition.Corner=function(t,e){t=(""+t).replace(/([A-Z])/," $1").replace(/middle/gi,N).toLowerCase(),this.x=(t.match(/left|right/i)||t.match(/center/)||["inherit"])[0].toLowerCase(),this.y=(t.match(/top|bottom|center/i)||["inherit"])[0].toLowerCase(),this.forceY=!!e;var i=t.charAt(0);this.precedance="t"===i||"b"===i?L:S}).prototype;ae.invert=function(t,e){this[t]=this[t]===F?P:this[t]===P?F:e||this[t]},ae.string=function(){var t=this.x,e=this.y;return t===e?t:this.precedance===L||this.forceY&&"center"!==e?e+" "+t:t+" "+e},ae.abbrev=function(){var t=this.string().split(" ");return t[0].charAt(0)+(t[1]&&t[1].charAt(0)||"")},ae.clone=function(){return new z(this.string(),this.forceY)},j.toggle=function(t,i){var o=this.cache,n=this.options,r=this.tooltip;if(i){if(/over|enter/.test(i.type)&&/out|leave/.test(o.event.type)&&n.show.target.add(i.target).length===n.show.target.length&&r.has(i.relatedTarget).length)return this;o.event=d(i)}if(this.waiting&&!t&&(this.hiddenDuringWait=W),!this.rendered)return t?this.render(1):this;if(this.destroyed||this.disabled)return this;var a,h,l,c=t?"show":"hide",p=this.options[c],u=(this.options[t?"hide":"show"],this.options.position),f=this.options.content,g=this.tooltip.css("width"),m=this.tooltip.is(":visible"),v=t||1===p.target.length,y=!i||2>p.target.length||o.target[0]===i.target;return(typeof t).search("boolean|number")&&(t=!m),a=!r.is(":animated")&&m===t&&y,h=a?E:!!this._trigger(c,[90]),this.destroyed?this:(h!==k&&t&&this.focus(i),!h||a?this:(s.attr(r[0],"aria-hidden",!t),t?(o.origin=d(this.mouse),s.isFunction(f.text)&&this._updateContent(f.text,k),s.isFunction(f.title)&&this._updateTitle(f.title,k),!I&&"mouse"===u.target&&u.adjust.mouse&&(s(e).bind("mousemove."+X,this._storeMouse),I=W),g||r.css("width",r.outerWidth(k)),this.reposition(i,arguments[2]),g||r.css("width",""),p.solo&&("string"==typeof p.solo?s(p.solo):s(U,p.solo)).not(r).not(p.target).qtip("hide",s.Event("tooltipsolo"))):(clearTimeout(this.timers.show),delete o.origin,I&&!s(U+'[tracking="true"]:visible',p.solo).not(r).length&&(s(e).unbind("mousemove."+X),I=k),this.blur(i)),l=s.proxy(function(){t?(oe.ie&&r[0].style.removeAttribute("filter"),r.css("overflow",""),"string"==typeof p.autofocus&&s(this.options.show.autofocus,r).focus(),this.options.show.target.trigger("qtip-"+this.id+"-inactive")):r.css({display:"",visibility:"",opacity:"",left:"",top:""}),this._trigger(t?"visible":"hidden")},this),p.effect===k||v===k?(r[c](),l()):s.isFunction(p.effect)?(r.stop(1,1),p.effect.call(r,this),r.queue("fx",function(t){l(),t()})):r.fadeTo(90,t?1:0,l),t&&p.target.trigger("qtip-"+this.id+"-inactive"),this))},j.show=function(t){return this.toggle(W,t)},j.hide=function(t){return this.toggle(k,t)},j.focus=function(t){if(!this.rendered||this.destroyed)return this;var e=s(U),i=this.tooltip,o=parseInt(i[0].style.zIndex,10),n=T.zindex+e.length;return i.hasClass(Z)||this._trigger("focus",[n],t)&&(o!==n&&(e.each(function(){this.style.zIndex>o&&(this.style.zIndex=this.style.zIndex-1)}),e.filter("."+Z).qtip("blur",t)),i.addClass(Z)[0].style.zIndex=n),this},j.blur=function(t){return!this.rendered||this.destroyed?this:(this.tooltip.removeClass(Z),this._trigger("blur",[this.tooltip.css("zIndex")],t),this)},j.disable=function(t){return this.destroyed?this:("toggle"===t?t=!(this.rendered?this.tooltip.hasClass(ee):this.disabled):"boolean"!=typeof t&&(t=W),this.rendered&&this.tooltip.toggleClass(ee,t).attr("aria-disabled",t),this.disabled=!!t,this)},j.enable=function(){return this.disable(k)},j._createButton=function(){var t=this,e=this.elements,i=e.tooltip,o=this.options.content.button,n="string"==typeof o,r=n?o:"Close tooltip";e.button&&e.button.remove(),e.button=o.jquery?o:s("<a />",{"class":"qtip-close "+(this.options.style.widget?"":X+"-icon"),title:r,"aria-label":r}).prepend(s("<span />",{"class":"ui-icon ui-icon-close",html:"&times;"})),e.button.appendTo(e.titlebar||i).attr("role","button").click(function(e){return i.hasClass(ee)||t.hide(e),k})},j._updateButton=function(t){if(!this.rendered)return k;var e=this.elements.button;t?this._createButton():e.remove()},j._setWidget=function(){var t=this.options.style.widget,e=this.elements,i=e.tooltip,s=i.hasClass(ee);i.removeClass(ee),ee=t?"ui-state-disabled":"qtip-disabled",i.toggleClass(ee,s),i.toggleClass("ui-helper-reset "+c(),t).toggleClass(K,this.options.style.def&&!t),e.content&&e.content.toggleClass(c("content"),t),e.titlebar&&e.titlebar.toggleClass(c("header"),t),e.button&&e.button.toggleClass(X+"-icon",!t)},j._storeMouse=function(t){(this.mouse=d(t)).type="mousemove"},j._bind=function(t,e,i,o,n){var r="."+this._id+(o?"-"+o:"");e.length&&s(t).bind((e.split?e:e.join(r+" "))+r,s.proxy(i,n||this))},j._unbind=function(t,e){s(t).unbind("."+this._id+(e?"-"+e:""))};var he="."+X;s(function(){v(U,["mouseenter","mouseleave"],function(t){var e="mouseenter"===t.type,i=s(t.currentTarget),o=s(t.relatedTarget||t.target),n=this.options;e?(this.focus(t),i.hasClass(J)&&!i.hasClass(ee)&&clearTimeout(this.timers.hide)):"mouse"===n.position.target&&n.hide.event&&n.show.target&&!o.closest(n.show.target[0]).length&&this.hide(t),i.toggleClass(te,e)}),v("["+H+"]",Q,g)}),j._trigger=function(t,e,i){var o=s.Event("tooltip"+t);return o.originalEvent=i&&s.extend({},i)||this.cache.event||E,this.triggering=t,this.tooltip.trigger(o,[this].concat(e||[])),this.triggering=k,!o.isDefaultPrevented()},j._bindEvents=function(t,e,o,n,r,a){if(n.add(o).length===n.length){var h=[];e=s.map(e,function(e){var o=s.inArray(e,t);return o>-1?(h.push(t.splice(o,1)[0]),i):e}),h.length&&this._bind(o,h,function(t){var e=this.rendered?this.tooltip[0].offsetWidth>0:!1;(e?a:r).call(this,t)})}this._bind(o,t,r),this._bind(n,e,a)},j._assignInitialEvents=function(t){function e(t){return this.disabled||this.destroyed?k:(this.cache.event=d(t),this.cache.target=t?s(t.target):[i],clearTimeout(this.timers.show),this.timers.show=p.call(this,function(){this.render("object"==typeof t||o.show.ready)},o.show.delay),i)}var o=this.options,n=o.show.target,r=o.hide.target,a=o.show.event?s.trim(""+o.show.event).split(" "):[],h=o.hide.event?s.trim(""+o.hide.event).split(" "):[];/mouse(over|enter)/i.test(o.show.event)&&!/mouse(out|leave)/i.test(o.hide.event)&&h.push("mouseleave"),this._bind(n,"mousemove",function(t){this._storeMouse(t),this.cache.onTarget=W}),this._bindEvents(a,h,n,r,e,function(){clearTimeout(this.timers.show)}),(o.show.ready||o.prerender)&&e.call(this,t)},j._assignEvents=function(){var i=this,o=this.options,n=o.position,r=this.tooltip,a=o.show.target,h=o.hide.target,l=n.container,c=n.viewport,d=s(e),p=(s(e.body),s(t)),v=o.show.event?s.trim(""+o.show.event).split(" "):[],y=o.hide.event?s.trim(""+o.hide.event).split(" "):[];s.each(o.events,function(t,e){i._bind(r,"toggle"===t?["tooltipshow","tooltiphide"]:["tooltip"+t],e,null,r)}),/mouse(out|leave)/i.test(o.hide.event)&&"window"===o.hide.leave&&this._bind(d,["mouseout","blur"],function(t){/select|option/.test(t.target.nodeName)||t.relatedTarget||this.hide(t)}),o.hide.fixed?h=h.add(r.addClass(J)):/mouse(over|enter)/i.test(o.show.event)&&this._bind(h,"mouseleave",function(){clearTimeout(this.timers.show)}),(""+o.hide.event).indexOf("unfocus")>-1&&this._bind(l.closest("html"),["mousedown","touchstart"],function(t){var e=s(t.target),i=this.rendered&&!this.tooltip.hasClass(ee)&&this.tooltip[0].offsetWidth>0,o=e.parents(U).filter(this.tooltip[0]).length>0;e[0]===this.target[0]||e[0]===this.tooltip[0]||o||this.target.has(e[0]).length||!i||this.hide(t)}),"number"==typeof o.hide.inactive&&(this._bind(a,"qtip-"+this.id+"-inactive",g),this._bind(h.add(r),T.inactiveEvents,g,"-inactive")),this._bindEvents(v,y,a,h,u,f),this._bind(a.add(r),"mousemove",function(t){if("number"==typeof o.hide.distance){var e=this.cache.origin||{},i=this.options.hide.distance,s=Math.abs;(s(t.pageX-e.pageX)>=i||s(t.pageY-e.pageY)>=i)&&this.hide(t)}this._storeMouse(t)}),"mouse"===n.target&&n.adjust.mouse&&(o.hide.event&&this._bind(a,["mouseenter","mouseleave"],function(t){this.cache.onTarget="mouseenter"===t.type}),this._bind(d,"mousemove",function(t){this.rendered&&this.cache.onTarget&&!this.tooltip.hasClass(ee)&&this.tooltip[0].offsetWidth>0&&this.reposition(t)})),(n.adjust.resize||c.length)&&this._bind(s.event.special.resize?c:p,"resize",m),n.adjust.scroll&&this._bind(p.add(n.container),"scroll",m)},j._unassignEvents=function(){var i=[this.options.show.target[0],this.options.hide.target[0],this.rendered&&this.tooltip[0],this.options.position.container[0],this.options.position.viewport[0],this.options.position.container.closest("html")[0],t,e];this._unbind(s([]).pushStack(s.grep(i,function(t){return"object"==typeof t})))},T=s.fn.qtip=function(t,e,o){var n=(""+t).toLowerCase(),r=E,h=s.makeArray(arguments).slice(1),l=h[h.length-1],c=this[0]?s.data(this[0],X):E;return!arguments.length&&c||"api"===n?c:"string"==typeof t?(this.each(function(){var t=s.data(this,X);if(!t)return W;if(l&&l.timeStamp&&(t.cache.event=l),!e||"option"!==n&&"options"!==n)t[n]&&t[n].apply(t,h);else{if(o===i&&!s.isPlainObject(e))return r=t.get(e),k;t.set(e,o)}}),r!==E?r:this):"object"!=typeof t&&arguments.length?i:(c=a(s.extend(W,{},t)),this.each(function(t){var e,o;return o=s.isArray(c.id)?c.id[t]:c.id,o=!o||o===k||1>o.length||T.api[o]?T.nextid++:o,e=y(s(this),o,c),e===k?W:(T.api[o]=e,s.each(R,function(){"initialize"===this.initialize&&this(e)}),e._assignInitialEvents(l),i)}))},s.qtip=o,T.api={},s.each({attr:function(t,e){if(this.length){var i=this[0],o="title",n=s.data(i,"qtip");if(t===o&&n&&"object"==typeof n&&n.options.suppress)return 2>arguments.length?s.attr(i,se):(n&&n.options.content.attr===o&&n.cache.attr&&n.set("content.text",e),this.attr(se,e))}return s.fn["attr"+ie].apply(this,arguments)},clone:function(t){var e=(s([]),s.fn["clone"+ie].apply(this,arguments));return t||e.filter("["+se+"]").attr("title",function(){return s.attr(this,se)}).removeAttr(se),e}},function(t,e){if(!e||s.fn[t+ie])return W;var i=s.fn[t+ie]=s.fn[t];s.fn[t]=function(){return e.apply(this,arguments)||i.apply(this,arguments)}}),s.ui||(s["cleanData"+ie]=s.cleanData,s.cleanData=function(t){for(var e,i=0;(e=s(t[i])).length;i++)if(e.attr(Y))try{e.triggerHandler("removeqtip")}catch(o){}s["cleanData"+ie].apply(this,arguments)}),T.version="2.2.0",T.nextid=0,T.inactiveEvents=Q,T.zindex=15e3,T.defaults={prerender:k,id:k,overwrite:W,suppress:W,content:{text:W,attr:"title",title:k,button:k},position:{my:"top left",at:"bottom right",target:k,container:k,viewport:k,adjust:{x:0,y:0,mouse:W,scroll:W,resize:W,method:"flipinvert flipinvert"},effect:function(t,e){s(this).animate(e,{duration:200,queue:k})}},show:{target:k,event:"mouseenter",effect:W,delay:90,solo:k,ready:k,autofocus:k},hide:{target:k,event:"mouseleave",effect:W,delay:0,fixed:k,inactive:k,leave:"window",distance:k},style:{classes:"",widget:k,width:k,height:k,def:W},events:{render:E,move:E,show:E,hide:E,toggle:E,visible:E,hidden:E,focus:E,blur:E}};var le,ce="margin",de="border",pe="color",ue="background-color",fe="transparent",ge=" !important",me=!!e.createElement("canvas").getContext,ve=/rgba?\(0, 0, 0(, 0)?\)|transparent|#123456/i,ye={},be=["Webkit","O","Moz","ms"];if(me)var we=t.devicePixelRatio||1,_e=function(){var t=e.createElement("canvas").getContext("2d");return t.backingStorePixelRatio||t.webkitBackingStorePixelRatio||t.mozBackingStorePixelRatio||t.msBackingStorePixelRatio||t.oBackingStorePixelRatio||1}(),xe=we/_e;else var qe=function(t,e,i){return"<qtipvml:"+t+' xmlns="urn:schemas-microsoft.com:vml" class="qtip-vml" '+(e||"")+' style="behavior: url(#default#VML); '+(i||"")+'" />'};s.extend(x.prototype,{init:function(t){var e,i;i=this.element=t.elements.tip=s("<div />",{"class":X+"-tip"}).prependTo(t.tooltip),me?(e=s("<canvas />").appendTo(this.element)[0].getContext("2d"),e.lineJoin="miter",e.miterLimit=1e5,e.save()):(e=qe("shape",'coordorigin="0,0"',"position:absolute;"),this.element.html(e+e),t._bind(s("*",i).add(i),["click","mousedown"],function(t){t.stopPropagation()},this._ns)),t._bind(t.tooltip,"tooltipmove",this.reposition,this._ns,this),this.create()},_swapDimensions:function(){this.size[0]=this.options.height,this.size[1]=this.options.width},_resetDimensions:function(){this.size[0]=this.options.width,this.size[1]=this.options.height},_useTitle:function(t){var e=this.qtip.elements.titlebar;return e&&(t.y===D||t.y===N&&this.element.position().top+this.size[1]/2+this.options.offset<e.outerHeight(W))},_parseCorner:function(t){var e=this.qtip.options.position.my;return t===k||e===k?t=k:t===W?t=new z(e.string()):t.string||(t=new z(t),t.fixed=W),t},_parseWidth:function(t,e,i){var s=this.qtip.elements,o=de+b(e)+"Width";return(i?_(i,o):_(s.content,o)||_(this._useTitle(t)&&s.titlebar||s.content,o)||_(s.tooltip,o))||0},_parseRadius:function(t){var e=this.qtip.elements,i=de+b(t.y)+b(t.x)+"Radius";return 9>oe.ie?0:_(this._useTitle(t)&&e.titlebar||e.content,i)||_(e.tooltip,i)||0},_invalidColour:function(t,e,i){var s=t.css(e);return!s||i&&s===t.css(i)||ve.test(s)?k:s},_parseColours:function(t){var e=this.qtip.elements,i=this.element.css("cssText",""),o=de+b(t[t.precedance])+b(pe),n=this._useTitle(t)&&e.titlebar||e.content,r=this._invalidColour,a=[];return a[0]=r(i,ue)||r(n,ue)||r(e.content,ue)||r(e.tooltip,ue)||i.css(ue),a[1]=r(i,o,pe)||r(n,o,pe)||r(e.content,o,pe)||r(e.tooltip,o,pe)||e.tooltip.css(o),s("*",i).add(i).css("cssText",ue+":"+fe+ge+";"+de+":0"+ge+";"),a},_calculateSize:function(t){var e,i,s,o=t.precedance===L,n=this.options.width,r=this.options.height,a="c"===t.abbrev(),h=(o?n:r)*(a?.5:1),l=Math.pow,c=Math.round,d=Math.sqrt(l(h,2)+l(r,2)),p=[this.border/h*d,this.border/r*d];return p[2]=Math.sqrt(l(p[0],2)-l(this.border,2)),p[3]=Math.sqrt(l(p[1],2)-l(this.border,2)),e=d+p[2]+p[3]+(a?0:p[0]),i=e/d,s=[c(i*n),c(i*r)],o?s:s.reverse()},_calculateTip:function(t,e,i){i=i||1,e=e||this.size;var s=e[0]*i,o=e[1]*i,n=Math.ceil(s/2),r=Math.ceil(o/2),a={br:[0,0,s,o,s,0],bl:[0,0,s,0,0,o],tr:[0,o,s,0,s,o],tl:[0,0,0,o,s,o],tc:[0,o,n,0,s,o],bc:[0,0,s,0,n,o],rc:[0,0,s,r,0,o],lc:[s,0,s,o,0,r]};return a.lt=a.br,a.rt=a.bl,a.lb=a.tr,a.rb=a.tl,a[t.abbrev()]},_drawCoords:function(t,e){t.beginPath(),t.moveTo(e[0],e[1]),t.lineTo(e[2],e[3]),t.lineTo(e[4],e[5]),t.closePath()},create:function(){var t=this.corner=(me||oe.ie)&&this._parseCorner(this.options.corner);return(this.enabled=!!this.corner&&"c"!==this.corner.abbrev())&&(this.qtip.cache.corner=t.clone(),this.update()),this.element.toggle(this.enabled),this.corner},update:function(e,i){if(!this.enabled)return this;var o,n,r,a,h,l,c,d,p=this.qtip.elements,u=this.element,f=u.children(),g=this.options,m=this.size,v=g.mimic,y=Math.round;e||(e=this.qtip.cache.corner||this.corner),v===k?v=e:(v=new z(v),v.precedance=e.precedance,"inherit"===v.x?v.x=e.x:"inherit"===v.y?v.y=e.y:v.x===v.y&&(v[e.precedance]=e[e.precedance])),n=v.precedance,e.precedance===S?this._swapDimensions():this._resetDimensions(),o=this.color=this._parseColours(e),o[1]!==fe?(d=this.border=this._parseWidth(e,e[e.precedance]),g.border&&1>d&&!ve.test(o[1])&&(o[0]=o[1]),this.border=d=g.border!==W?g.border:d):this.border=d=0,c=this.size=this._calculateSize(e),u.css({width:c[0],height:c[1],lineHeight:c[1]+"px"}),l=e.precedance===L?[y(v.x===F?d:v.x===P?c[0]-m[0]-d:(c[0]-m[0])/2),y(v.y===D?c[1]-m[1]:0)]:[y(v.x===F?c[0]-m[0]:0),y(v.y===D?d:v.y===O?c[1]-m[1]-d:(c[1]-m[1])/2)],me?(r=f[0].getContext("2d"),r.restore(),r.save(),r.clearRect(0,0,6e3,6e3),a=this._calculateTip(v,m,xe),h=this._calculateTip(v,this.size,xe),f.attr(A,c[0]*xe).attr(B,c[1]*xe),f.css(A,c[0]).css(B,c[1]),this._drawCoords(r,h),r.fillStyle=o[1],r.fill(),r.translate(l[0]*xe,l[1]*xe),this._drawCoords(r,a),r.fillStyle=o[0],r.fill()):(a=this._calculateTip(v),a="m"+a[0]+","+a[1]+" l"+a[2]+","+a[3]+" "+a[4]+","+a[5]+" xe",l[2]=d&&/^(r|b)/i.test(e.string())?8===oe.ie?2:1:0,f.css({coordsize:c[0]+d+" "+(c[1]+d),antialias:""+(v.string().indexOf(N)>-1),left:l[0]-l[2]*Number(n===S),top:l[1]-l[2]*Number(n===L),width:c[0]+d,height:c[1]+d}).each(function(t){var e=s(this);e[e.prop?"prop":"attr"]({coordsize:c[0]+d+" "+(c[1]+d),path:a,fillcolor:o[0],filled:!!t,stroked:!t}).toggle(!(!d&&!t)),!t&&e.html(qe("stroke",'weight="'+2*d+'px" color="'+o[1]+'" miterlimit="1000" joinstyle="miter"'))})),t.opera&&setTimeout(function(){p.tip.css({display:"inline-block",visibility:"visible"})},1),i!==k&&this.calculate(e,c)},calculate:function(t,e){if(!this.enabled)return k;var i,o,n=this,r=this.qtip.elements,a=this.element,h=this.options.offset,l=(r.tooltip.hasClass("ui-widget"),{});return t=t||this.corner,i=t.precedance,e=e||this._calculateSize(t),o=[t.x,t.y],i===S&&o.reverse(),s.each(o,function(s,o){var a,c,d;o===N?(a=i===L?F:D,l[a]="50%",l[ce+"-"+a]=-Math.round(e[i===L?0:1]/2)+h):(a=n._parseWidth(t,o,r.tooltip),c=n._parseWidth(t,o,r.content),d=n._parseRadius(t),l[o]=Math.max(-n.border,s?c:h+(d>a?d:-a)))
}),l[t[i]]-=e[i===S?0:1],a.css({margin:"",top:"",bottom:"",left:"",right:""}).css(l),l},reposition:function(t,e,s){function o(t,e,i,s,o){t===V&&l.precedance===e&&c[s]&&l[i]!==N?l.precedance=l.precedance===S?L:S:t!==V&&c[s]&&(l[e]=l[e]===N?c[s]>0?s:o:l[e]===s?o:s)}function n(t,e,o){l[t]===N?g[ce+"-"+e]=f[t]=r[ce+"-"+e]-c[e]:(a=r[o]!==i?[c[e],-r[e]]:[-c[e],r[e]],(f[t]=Math.max(a[0],a[1]))>a[0]&&(s[e]-=c[e],f[e]=k),g[r[o]!==i?o:e]=f[t])}if(this.enabled){var r,a,h=e.cache,l=this.corner.clone(),c=s.adjusted,d=e.options.position.adjust.method.split(" "),p=d[0],u=d[1]||d[0],f={left:k,top:k,x:0,y:0},g={};this.corner.fixed!==W&&(o(p,S,L,F,P),o(u,L,S,D,O),l.string()===h.corner.string()||h.cornerTop===c.top&&h.cornerLeft===c.left||this.update(l,k)),r=this.calculate(l),r.right!==i&&(r.left=-r.right),r.bottom!==i&&(r.top=-r.bottom),r.user=this.offset,(f.left=p===V&&!!c.left)&&n(S,F,P),(f.top=u===V&&!!c.top)&&n(L,D,O),this.element.css(g).toggle(!(f.x&&f.y||l.x===N&&f.y||l.y===N&&f.x)),s.left-=r.left.charAt?r.user:p!==V||f.top||!f.left&&!f.top?r.left+this.border:0,s.top-=r.top.charAt?r.user:u!==V||f.left||!f.left&&!f.top?r.top+this.border:0,h.cornerLeft=c.left,h.cornerTop=c.top,h.corner=l.clone()}},destroy:function(){this.qtip._unbind(this.qtip.tooltip,this._ns),this.qtip.elements.tip&&this.qtip.elements.tip.find("*").remove().end().remove()}}),le=R.tip=function(t){return new x(t,t.options.style.tip)},le.initialize="render",le.sanitize=function(t){if(t.style&&"tip"in t.style){var e=t.style.tip;"object"!=typeof e&&(e=t.style.tip={corner:e}),/string|boolean/i.test(typeof e.corner)||(e.corner=W)}},M.tip={"^position.my|style.tip.(corner|mimic|border)$":function(){this.create(),this.qtip.reposition()},"^style.tip.(height|width)$":function(t){this.size=[t.width,t.height],this.update(),this.qtip.reposition()},"^content.title|style.(classes|widget)$":function(){this.update()}},s.extend(W,T.defaults,{style:{tip:{corner:W,mimic:k,width:6,height:6,border:W,offset:0}}});var Ce,Te,je="qtip-modal",ze="."+je;Te=function(){function t(t){if(s.expr[":"].focusable)return s.expr[":"].focusable;var e,i,o,n=!isNaN(s.attr(t,"tabindex")),r=t.nodeName&&t.nodeName.toLowerCase();return"area"===r?(e=t.parentNode,i=e.name,t.href&&i&&"map"===e.nodeName.toLowerCase()?(o=s("img[usemap=#"+i+"]")[0],!!o&&o.is(":visible")):!1):/input|select|textarea|button|object/.test(r)?!t.disabled:"a"===r?t.href||n:n}function i(t){1>c.length&&t.length?t.not("body").blur():c.first().focus()}function o(t){if(h.is(":visible")){var e,o=s(t.target),a=n.tooltip,l=o.closest(U);e=1>l.length?k:parseInt(l[0].style.zIndex,10)>parseInt(a[0].style.zIndex,10),e||o.closest(U)[0]===a[0]||i(o),r=t.target===c[c.length-1]}}var n,r,a,h,l=this,c={};s.extend(l,{init:function(){return h=l.elem=s("<div />",{id:"qtip-overlay",html:"<div></div>",mousedown:function(){return k}}).hide(),s(e.body).bind("focusin"+ze,o),s(e).bind("keydown"+ze,function(t){n&&n.options.show.modal.escape&&27===t.keyCode&&n.hide(t)}),h.bind("click"+ze,function(t){n&&n.options.show.modal.blur&&n.hide(t)}),l},update:function(e){n=e,c=e.options.show.modal.stealfocus!==k?e.tooltip.find("*").filter(function(){return t(this)}):[]},toggle:function(t,o,r){var c=(s(e.body),t.tooltip),d=t.options.show.modal,p=d.effect,u=o?"show":"hide",f=h.is(":visible"),g=s(ze).filter(":visible:not(:animated)").not(c);return l.update(t),o&&d.stealfocus!==k&&i(s(":focus")),h.toggleClass("blurs",d.blur),o&&h.appendTo(e.body),h.is(":animated")&&f===o&&a!==k||!o&&g.length?l:(h.stop(W,k),s.isFunction(p)?p.call(h,o):p===k?h[u]():h.fadeTo(parseInt(r,10)||90,o?1:0,function(){o||h.hide()}),o||h.queue(function(t){h.css({left:"",top:""}),s(ze).length||h.detach(),t()}),a=o,n.destroyed&&(n=E),l)}}),l.init()},Te=new Te,s.extend(q.prototype,{init:function(t){var e=t.tooltip;return this.options.on?(t.elements.overlay=Te.elem,e.addClass(je).css("z-index",T.modal_zindex+s(ze).length),t._bind(e,["tooltipshow","tooltiphide"],function(t,i,o){var n=t.originalEvent;if(t.target===e[0])if(n&&"tooltiphide"===t.type&&/mouse(leave|enter)/.test(n.type)&&s(n.relatedTarget).closest(Te.elem[0]).length)try{t.preventDefault()}catch(r){}else(!n||n&&"tooltipsolo"!==n.type)&&this.toggle(t,"tooltipshow"===t.type,o)},this._ns,this),t._bind(e,"tooltipfocus",function(t,i){if(!t.isDefaultPrevented()&&t.target===e[0]){var o=s(ze),n=T.modal_zindex+o.length,r=parseInt(e[0].style.zIndex,10);Te.elem[0].style.zIndex=n-1,o.each(function(){this.style.zIndex>r&&(this.style.zIndex-=1)}),o.filter("."+Z).qtip("blur",t.originalEvent),e.addClass(Z)[0].style.zIndex=n,Te.update(i);try{t.preventDefault()}catch(a){}}},this._ns,this),t._bind(e,"tooltiphide",function(t){t.target===e[0]&&s(ze).filter(":visible").not(e).last().qtip("focus",t)},this._ns,this),i):this},toggle:function(t,e,s){return t&&t.isDefaultPrevented()?this:(Te.toggle(this.qtip,!!e,s),i)},destroy:function(){this.qtip.tooltip.removeClass(je),this.qtip._unbind(this.qtip.tooltip,this._ns),Te.toggle(this.qtip,k),delete this.qtip.elements.overlay}}),Ce=R.modal=function(t){return new q(t,t.options.show.modal)},Ce.sanitize=function(t){t.show&&("object"!=typeof t.show.modal?t.show.modal={on:!!t.show.modal}:t.show.modal.on===i&&(t.show.modal.on=W))},T.modal_zindex=T.zindex-200,Ce.initialize="render",M.modal={"^show.modal.(on|blur)$":function(){this.destroy(),this.init(),this.qtip.elems.overlay.toggle(this.qtip.tooltip[0].offsetWidth>0)}},s.extend(W,T.defaults,{show:{modal:{on:k,effect:W,blur:W,stealfocus:W,escape:W}}}),R.viewport=function(i,s,o,n,r,a,h){function l(t,e,i,o,n,r,a,h,l){var c=s[n],p=_[t],b=x[t],w=i===V,q=p===n?l:p===r?-l:-l/2,C=b===n?h:b===r?-h:-h/2,T=v[n]+y[n]-(f?0:u[n]),j=T-c,z=c+l-(a===A?g:m)-T,M=q-(_.precedance===t||p===_[e]?C:0)-(b===N?h/2:0);return w?(M=(p===n?1:-1)*q,s[n]+=j>0?j:z>0?-z:0,s[n]=Math.max(-u[n]+y[n],c-M,Math.min(Math.max(-u[n]+y[n]+(a===A?g:m),c+M),s[n],"center"===p?c-q:1e9))):(o*=i===$?2:0,j>0&&(p!==n||z>0)?(s[n]-=M+o,d.invert(t,n)):z>0&&(p!==r||j>0)&&(s[n]-=(p===N?-M:M)+o,d.invert(t,r)),v>s[n]&&-s[n]>z&&(s[n]=c,d=_.clone())),s[n]-c}var c,d,p,u,f,g,m,v,y,b=o.target,w=i.elements.tooltip,_=o.my,x=o.at,q=o.adjust,C=q.method.split(" "),T=C[0],j=C[1]||C[0],z=o.viewport,M=o.container,I=i.cache,W={left:0,top:0};return z.jquery&&b[0]!==t&&b[0]!==e.body&&"none"!==q.method?(u=M.offset()||W,f="static"===M.css("position"),c="fixed"===w.css("position"),g=z[0]===t?z.width():z.outerWidth(k),m=z[0]===t?z.height():z.outerHeight(k),v={left:c?0:z.scrollLeft(),top:c?0:z.scrollTop()},y=z.offset()||W,("shift"!==T||"shift"!==j)&&(d=_.clone()),W={left:"none"!==T?l(S,L,T,q.x,F,P,A,n,a):0,top:"none"!==j?l(L,S,j,q.y,D,O,B,r,h):0},d&&I.lastClass!==(p=X+"-pos-"+d.abbrev())&&w.removeClass(i.cache.lastClass).addClass(i.cache.lastClass=p),W):W},R.polys={polygon:function(t,e){var i,s,o,n={width:0,height:0,position:{top:1e10,right:0,bottom:0,left:1e10},adjustable:k},r=0,a=[],h=1,l=1,c=0,d=0;for(r=t.length;r--;)i=[parseInt(t[--r],10),parseInt(t[r+1],10)],i[0]>n.position.right&&(n.position.right=i[0]),i[0]<n.position.left&&(n.position.left=i[0]),i[1]>n.position.bottom&&(n.position.bottom=i[1]),i[1]<n.position.top&&(n.position.top=i[1]),a.push(i);if(s=n.width=Math.abs(n.position.right-n.position.left),o=n.height=Math.abs(n.position.bottom-n.position.top),"c"===e.abbrev())n.position={left:n.position.left+n.width/2,top:n.position.top+n.height/2};else{for(;s>0&&o>0&&h>0&&l>0;)for(s=Math.floor(s/2),o=Math.floor(o/2),e.x===F?h=s:e.x===P?h=n.width-s:h+=Math.floor(s/2),e.y===D?l=o:e.y===O?l=n.height-o:l+=Math.floor(o/2),r=a.length;r--&&!(2>a.length);)c=a[r][0]-n.position.left,d=a[r][1]-n.position.top,(e.x===F&&c>=h||e.x===P&&h>=c||e.x===N&&(h>c||c>n.width-h)||e.y===D&&d>=l||e.y===O&&l>=d||e.y===N&&(l>d||d>n.height-l))&&a.splice(r,1);n.position={left:a[0][0],top:a[0][1]}}return n},rect:function(t,e,i,s){return{width:Math.abs(i-t),height:Math.abs(s-e),position:{left:Math.min(t,i),top:Math.min(e,s)}}},_angles:{tc:1.5,tr:7/4,tl:5/4,bc:.5,br:.25,bl:.75,rc:2,lc:1,c:0},ellipse:function(t,e,i,s,o){var n=R.polys._angles[o.abbrev()],r=0===n?0:i*Math.cos(n*Math.PI),a=s*Math.sin(n*Math.PI);return{width:2*i-Math.abs(r),height:2*s-Math.abs(a),position:{left:t+r,top:e+a},adjustable:k}},circle:function(t,e,i,s){return R.polys.ellipse(t,e,i,i,s)}},R.svg=function(t,i,o){for(var n,r,a,h,l,c,d,p,u,f,g,m=s(e),v=i[0],y=s(v.ownerSVGElement),b=1,w=1,_=!0;!v.getBBox;)v=v.parentNode;if(!v.getBBox||!v.parentNode)return k;n=y.attr("width")||y.width()||parseInt(y.css("width"),10),r=y.attr("height")||y.height()||parseInt(y.css("height"),10);var x=(parseInt(i.css("stroke-width"),10)||0)/2;switch(x&&(b+=x/n,w+=x/r),v.nodeName){case"ellipse":case"circle":f=R.polys.ellipse(v.cx.baseVal.value,v.cy.baseVal.value,(v.rx||v.r).baseVal.value+x,(v.ry||v.r).baseVal.value+x,o);break;case"line":case"polygon":case"polyline":for(u=v.points||[{x:v.x1.baseVal.value,y:v.y1.baseVal.value},{x:v.x2.baseVal.value,y:v.y2.baseVal.value}],f=[],p=-1,c=u.numberOfItems||u.length;c>++p;)d=u.getItem?u.getItem(p):u[p],f.push.apply(f,[d.x,d.y]);f=R.polys.polygon(f,o);break;default:f=v.getBoundingClientRect(),f={width:f.width,height:f.height,position:{left:f.left,top:f.top}},_=!1}return g=f.position,y=y[0],_&&(y.createSVGPoint&&(a=v.getScreenCTM(),u=y.createSVGPoint(),u.x=g.left,u.y=g.top,h=u.matrixTransform(a),g.left=h.x,g.top=h.y),y.viewBox&&(l=y.viewBox.baseVal)&&l.width&&l.height&&(b*=n/l.width,w*=r/l.height)),g.left+=m.scrollLeft(),g.top+=m.scrollTop(),f},R.imagemap=function(t,e,i){e.jquery||(e=s(e));var o,n,r,a,h,l=e.attr("shape").toLowerCase().replace("poly","polygon"),c=s('img[usemap="#'+e.parent("map").attr("name")+'"]'),d=s.trim(e.attr("coords")),p=d.replace(/,$/,"").split(",");if(!c.length)return k;if("polygon"===l)a=R.polys.polygon(p,i);else{if(!R.polys[l])return k;for(r=-1,h=p.length,n=[];h>++r;)n.push(parseInt(p[r],10));a=R.polys[l].apply(this,n.concat(i))}return o=c.offset(),o.left+=Math.ceil((c.outerWidth(k)-c.width())/2),o.top+=Math.ceil((c.outerHeight(k)-c.height())/2),a.position.left+=o.left,a.position.top+=o.top,a};var Me,Ie='<iframe class="qtip-bgiframe" frameborder="0" tabindex="-1" src="javascript:\'\';"  style="display:block; position:absolute; z-index:-1; filter:alpha(opacity=0); -ms-filter:"progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";"></iframe>';s.extend(C.prototype,{_scroll:function(){var e=this.qtip.elements.overlay;e&&(e[0].style.top=s(t).scrollTop()+"px")},init:function(i){var o=i.tooltip;1>s("select, object").length&&(this.bgiframe=i.elements.bgiframe=s(Ie).appendTo(o),i._bind(o,"tooltipmove",this.adjustBGIFrame,this._ns,this)),this.redrawContainer=s("<div/>",{id:X+"-rcontainer"}).appendTo(e.body),i.elements.overlay&&i.elements.overlay.addClass("qtipmodal-ie6fix")&&(i._bind(t,["scroll","resize"],this._scroll,this._ns,this),i._bind(o,["tooltipshow"],this._scroll,this._ns,this)),this.redraw()},adjustBGIFrame:function(){var t,e,i=this.qtip.tooltip,s={height:i.outerHeight(k),width:i.outerWidth(k)},o=this.qtip.plugins.tip,n=this.qtip.elements.tip;e=parseInt(i.css("borderLeftWidth"),10)||0,e={left:-e,top:-e},o&&n&&(t="x"===o.corner.precedance?[A,F]:[B,D],e[t[1]]-=n[t[0]]()),this.bgiframe.css(e).css(s)},redraw:function(){if(1>this.qtip.rendered||this.drawing)return this;var t,e,i,s,o=this.qtip.tooltip,n=this.qtip.options.style,r=this.qtip.options.position.container;return this.qtip.drawing=1,n.height&&o.css(B,n.height),n.width?o.css(A,n.width):(o.css(A,"").appendTo(this.redrawContainer),e=o.width(),1>e%2&&(e+=1),i=o.css("maxWidth")||"",s=o.css("minWidth")||"",t=(i+s).indexOf("%")>-1?r.width()/100:0,i=(i.indexOf("%")>-1?t:1)*parseInt(i,10)||e,s=(s.indexOf("%")>-1?t:1)*parseInt(s,10)||0,e=i+s?Math.min(Math.max(e,s),i):e,o.css(A,Math.round(e)).appendTo(r)),this.drawing=0,this},destroy:function(){this.bgiframe&&this.bgiframe.remove(),this.qtip._unbind([t,this.qtip.tooltip],this._ns)}}),Me=R.ie6=function(t){return 6===oe.ie?new C(t):k},Me.initialize="render",M.ie6={"^content|style$":function(){this.redraw()}}})})(window,document);

/*
 ### jQuery XML to JSON Plugin v1.3 - 2013-02-18 ###
 * http://www.fyneworks.com/ - diego@fyneworks.com
	* Licensed under http://en.wikipedia.org/wiki/MIT_License
 ###
 Website: http://www.fyneworks.com/jquery/xml-to-json/
*//*
 # INSPIRED BY: http://www.terracoder.com/
           AND: http://www.thomasfrank.se/xml_to_json.html
											AND: http://www.kawa.net/works/js/xml/objtree-e.html
*//*
 This simple script converts XML (document of code) into a JSON object. It is the combination of 2
 'xml to json' great parsers (see below) which allows for both 'simple' and 'extended' parsing modes.
*/
// Avoid collisions
; if (window.jQuery) (function ($) {
  // Add function to jQuery namespace
  $.extend({
    // converts xml documents and xml text to json object
    xml2json: function (xml, extended) {
      if (!xml) return {}; // quick fail

      //### PARSER LIBRARY
      // Core function
      function parseXML(node, simple) {
        if (!node) return null;
        var txt = '', obj = null, att = null;
        var nt = node.nodeType, nn = jsVar(node.localName || node.nodeName);
        var nv = node.text || node.nodeValue || '';
        /*DBG*/ //if(window.console) console.log(['x2j',nn,nt,nv.length+' bytes']);
        if (node.childNodes) {
          if (node.childNodes.length > 0) {
            /*DBG*/ //if(window.console) console.log(['x2j',nn,'CHILDREN',node.childNodes]);
            $.each(node.childNodes, function (n, cn) {
              var cnt = cn.nodeType, cnn = jsVar(cn.localName || cn.nodeName);
              var cnv = cn.text || cn.nodeValue || '';
              /*DBG*/ //if(window.console) console.log(['x2j',nn,'node>a',cnn,cnt,cnv]);
              if (cnt == 8) {
                /*DBG*/ //if(window.console) console.log(['x2j',nn,'node>b',cnn,'COMMENT (ignore)']);
                return; // ignore comment node
              }
              else if (cnt == 3 || cnt == 4 || !cnn) {
                // ignore white-space in between tags
                if (cnv.match(/^\s+$/)) {
                  /*DBG*/ //if(window.console) console.log(['x2j',nn,'node>c',cnn,'WHITE-SPACE (ignore)']);
                  return;
                };
                /*DBG*/ //if(window.console) console.log(['x2j',nn,'node>d',cnn,'TEXT']);
                txt += cnv.replace(/^\s+/, '').replace(/\s+$/, '');
                // make sure we ditch trailing spaces from markup
              }
              else {
                /*DBG*/ //if(window.console) console.log(['x2j',nn,'node>e',cnn,'OBJECT']);
                obj = obj || {};
                if (obj[cnn]) {
                  /*DBG*/ //if(window.console) console.log(['x2j',nn,'node>f',cnn,'ARRAY']);

                  // http://forum.jquery.com/topic/jquery-jquery-xml2json-problems-when-siblings-of-the-same-tagname-only-have-a-textnode-as-a-child
                  if (!obj[cnn].length) obj[cnn] = myArr(obj[cnn]);
                  obj[cnn] = myArr(obj[cnn]);

                  obj[cnn][obj[cnn].length] = parseXML(cn, true/* simple */);
                  obj[cnn].length = obj[cnn].length;
                }
                else {
                  /*DBG*/ //if(window.console) console.log(['x2j',nn,'node>g',cnn,'dig deeper...']);
                  obj[cnn] = parseXML(cn);
                };
              };
            });
          };//node.childNodes.length>0
        };//node.childNodes
        if (node.attributes) {
          if (node.attributes.length > 0) {
            /*DBG*/ //if(window.console) console.log(['x2j',nn,'ATTRIBUTES',node.attributes])
            att = {}; obj = obj || {};
            $.each(node.attributes, function (a, at) {
              var atn = jsVar(at.name), atv = at.value;
              att[atn] = atv;
              if (obj[atn]) {
                /*DBG*/ //if(window.console) console.log(['x2j',nn,'attr>',atn,'ARRAY']);

                // http://forum.jquery.com/topic/jquery-jquery-xml2json-problems-when-siblings-of-the-same-tagname-only-have-a-textnode-as-a-child
                //if(!obj[atn].length) obj[atn] = myArr(obj[atn]);//[ obj[ atn ] ];
                obj[cnn] = myArr(obj[cnn]);

                obj[atn][obj[atn].length] = atv;
                obj[atn].length = obj[atn].length;
              }
              else {
                /*DBG*/ //if(window.console) console.log(['x2j',nn,'attr>',atn,'TEXT']);
                obj[atn] = atv;
              };
            });
            //obj['attributes'] = att;
          };//node.attributes.length>0
        };//node.attributes
        if (obj) {
          obj = $.extend((txt != '' ? new String(txt) : {}),/* {text:txt},*/ obj || {}/*, att || {}*/);
          //txt = (obj.text) ? (typeof(obj.text)=='object' ? obj.text : [obj.text || '']).concat([txt]) : txt;
          txt = (obj.text) ? ([obj.text || '']).concat([txt]) : txt;
          if (txt) obj.text = txt;
          txt = '';
        };
        var out = obj || txt;
        //console.log([extended, simple, out]);
        if (extended) {
          if (txt) out = {};//new String(out);
          txt = out.text || txt || '';
          if (txt) out.text = txt;
          if (!simple) out = myArr(out);
        };
        return out;
      };// parseXML
      // Core Function End
      // Utility functions
      var jsVar = function (s) { return String(s || '').replace(/-/g, "_"); };

      // NEW isNum function: 01/09/2010
      // Thanks to Emile Grau, GigaTecnologies S.L., www.gigatransfer.com, www.mygigamail.com
      function isNum(s) {
        // based on utility function isNum from xml2json plugin (http://www.fyneworks.com/ - diego@fyneworks.com)
        // few bugs corrected from original function :
        // - syntax error : regexp.test(string) instead of string.test(reg)
        // - regexp modified to accept  comma as decimal mark (latin syntax : 25,24 )
        // - regexp modified to reject if no number before decimal mark  : ".7" is not accepted
        // - string is "trimmed", allowing to accept space at the beginning and end of string
        var regexp = /^((-)?([0-9]+)(([\.\,]{0,1})([0-9]+))?$)/
        return (typeof s == "number") || regexp.test(String((s && typeof s == "string") ? jQuery.trim(s) : ''));
      };
      // OLD isNum function: (for reference only)
      //var isNum = function(s){ return (typeof s == "number") || String((s && typeof s == "string") ? s : '').test(/^((-)?([0-9]*)((\.{0,1})([0-9]+))?$)/); };

      var myArr = function (o) {
        // http://forum.jquery.com/topic/jquery-jquery-xml2json-problems-when-siblings-of-the-same-tagname-only-have-a-textnode-as-a-child
        //if(!o.length) o = [ o ]; o.length=o.length;
        if (!$.isArray(o)) o = [o]; o.length = o.length;

        // here is where you can attach additional functionality, such as searching and sorting...
        return o;
      };
      // Utility functions End
      //### PARSER LIBRARY END

      // Convert plain text to xml
      if (typeof xml == 'string') xml = $.text2xml(xml);

      // Quick fail if not xml (or if this is a node)
      if (!xml.nodeType) return;
      if (xml.nodeType == 3 || xml.nodeType == 4) return xml.nodeValue;

      // Find xml root node
      var root = (xml.nodeType == 9) ? xml.documentElement : xml;

      // Convert xml to json
      var out = parseXML(root, true /* simple */);

      // Clean-up memory
      xml = null; root = null;

      // Send output
      return out;
    },

    // Convert text to XML DOM
    text2xml: function (str) {
      // NOTE: I'd like to use jQuery for this, but jQuery makes all tags uppercase
      //return $(xml)[0];

      /* prior to jquery 1.9 */
      /*
      var out;
      try{
       var xml = ((!$.support.opacity && !$.support.style))?new ActiveXObject("Microsoft.XMLDOM"):new DOMParser();
       xml.async = false;
      }catch(e){ throw new Error("XML Parser could not be instantiated") };
      try{
       if((!$.support.opacity && !$.support.style)) out = (xml.loadXML(str))?xml:false;
       else out = xml.parseFromString(str, "text/xml");
      }catch(e){ throw new Error("Error parsing XML string") };
      return out;
      */

      /* jquery 1.9+ */
      return $.parseXML(str);
    }
  }); // extend $
})(jQuery);
//     Underscore.js 1.9.1
//     http://underscorejs.org
//     (c) 2009-2018 Jeremy Ashkenas, DocumentCloud and Investigative Reporters & Editors
//     Underscore may be freely distributed under the MIT license.
!function(){var n="object"==typeof self&&self.self===self&&self||"object"==typeof global&&global.global===global&&global||this||{},r=n._,e=Array.prototype,o=Object.prototype,s="undefined"!=typeof Symbol?Symbol.prototype:null,u=e.push,c=e.slice,p=o.toString,i=o.hasOwnProperty,t=Array.isArray,a=Object.keys,l=Object.create,f=function(){},h=function(n){return n instanceof h?n:this instanceof h?void(this._wrapped=n):new h(n)};"undefined"==typeof exports||exports.nodeType?n._=h:("undefined"!=typeof module&&!module.nodeType&&module.exports&&(exports=module.exports=h),exports._=h),h.VERSION="1.9.1";var v,y=function(u,i,n){if(void 0===i)return u;switch(null==n?3:n){case 1:return function(n){return u.call(i,n)};case 3:return function(n,r,t){return u.call(i,n,r,t)};case 4:return function(n,r,t,e){return u.call(i,n,r,t,e)}}return function(){return u.apply(i,arguments)}},d=function(n,r,t){return h.iteratee!==v?h.iteratee(n,r):null==n?h.identity:h.isFunction(n)?y(n,r,t):h.isObject(n)&&!h.isArray(n)?h.matcher(n):h.property(n)};h.iteratee=v=function(n,r){return d(n,r,1/0)};var g=function(u,i){return i=null==i?u.length-1:+i,function(){for(var n=Math.max(arguments.length-i,0),r=Array(n),t=0;t<n;t++)r[t]=arguments[t+i];switch(i){case 0:return u.call(this,r);case 1:return u.call(this,arguments[0],r);case 2:return u.call(this,arguments[0],arguments[1],r)}var e=Array(i+1);for(t=0;t<i;t++)e[t]=arguments[t];return e[i]=r,u.apply(this,e)}},m=function(n){if(!h.isObject(n))return{};if(l)return l(n);f.prototype=n;var r=new f;return f.prototype=null,r},b=function(r){return function(n){return null==n?void 0:n[r]}},j=function(n,r){return null!=n&&i.call(n,r)},x=function(n,r){for(var t=r.length,e=0;e<t;e++){if(null==n)return;n=n[r[e]]}return t?n:void 0},_=Math.pow(2,53)-1,A=b("length"),w=function(n){var r=A(n);return"number"==typeof r&&0<=r&&r<=_};h.each=h.forEach=function(n,r,t){var e,u;if(r=y(r,t),w(n))for(e=0,u=n.length;e<u;e++)r(n[e],e,n);else{var i=h.keys(n);for(e=0,u=i.length;e<u;e++)r(n[i[e]],i[e],n)}return n},h.map=h.collect=function(n,r,t){r=d(r,t);for(var e=!w(n)&&h.keys(n),u=(e||n).length,i=Array(u),o=0;o<u;o++){var a=e?e[o]:o;i[o]=r(n[a],a,n)}return i};var O=function(c){return function(n,r,t,e){var u=3<=arguments.length;return function(n,r,t,e){var u=!w(n)&&h.keys(n),i=(u||n).length,o=0<c?0:i-1;for(e||(t=n[u?u[o]:o],o+=c);0<=o&&o<i;o+=c){var a=u?u[o]:o;t=r(t,n[a],a,n)}return t}(n,y(r,e,4),t,u)}};h.reduce=h.foldl=h.inject=O(1),h.reduceRight=h.foldr=O(-1),h.find=h.detect=function(n,r,t){var e=(w(n)?h.findIndex:h.findKey)(n,r,t);if(void 0!==e&&-1!==e)return n[e]},h.filter=h.select=function(n,e,r){var u=[];return e=d(e,r),h.each(n,function(n,r,t){e(n,r,t)&&u.push(n)}),u},h.reject=function(n,r,t){return h.filter(n,h.negate(d(r)),t)},h.every=h.all=function(n,r,t){r=d(r,t);for(var e=!w(n)&&h.keys(n),u=(e||n).length,i=0;i<u;i++){var o=e?e[i]:i;if(!r(n[o],o,n))return!1}return!0},h.some=h.any=function(n,r,t){r=d(r,t);for(var e=!w(n)&&h.keys(n),u=(e||n).length,i=0;i<u;i++){var o=e?e[i]:i;if(r(n[o],o,n))return!0}return!1},h.contains=h.includes=h.include=function(n,r,t,e){return w(n)||(n=h.values(n)),("number"!=typeof t||e)&&(t=0),0<=h.indexOf(n,r,t)},h.invoke=g(function(n,t,e){var u,i;return h.isFunction(t)?i=t:h.isArray(t)&&(u=t.slice(0,-1),t=t[t.length-1]),h.map(n,function(n){var r=i;if(!r){if(u&&u.length&&(n=x(n,u)),null==n)return;r=n[t]}return null==r?r:r.apply(n,e)})}),h.pluck=function(n,r){return h.map(n,h.property(r))},h.where=function(n,r){return h.filter(n,h.matcher(r))},h.findWhere=function(n,r){return h.find(n,h.matcher(r))},h.max=function(n,e,r){var t,u,i=-1/0,o=-1/0;if(null==e||"number"==typeof e&&"object"!=typeof n[0]&&null!=n)for(var a=0,c=(n=w(n)?n:h.values(n)).length;a<c;a++)null!=(t=n[a])&&i<t&&(i=t);else e=d(e,r),h.each(n,function(n,r,t){u=e(n,r,t),(o<u||u===-1/0&&i===-1/0)&&(i=n,o=u)});return i},h.min=function(n,e,r){var t,u,i=1/0,o=1/0;if(null==e||"number"==typeof e&&"object"!=typeof n[0]&&null!=n)for(var a=0,c=(n=w(n)?n:h.values(n)).length;a<c;a++)null!=(t=n[a])&&t<i&&(i=t);else e=d(e,r),h.each(n,function(n,r,t){((u=e(n,r,t))<o||u===1/0&&i===1/0)&&(i=n,o=u)});return i},h.shuffle=function(n){return h.sample(n,1/0)},h.sample=function(n,r,t){if(null==r||t)return w(n)||(n=h.values(n)),n[h.random(n.length-1)];var e=w(n)?h.clone(n):h.values(n),u=A(e);r=Math.max(Math.min(r,u),0);for(var i=u-1,o=0;o<r;o++){var a=h.random(o,i),c=e[o];e[o]=e[a],e[a]=c}return e.slice(0,r)},h.sortBy=function(n,e,r){var u=0;return e=d(e,r),h.pluck(h.map(n,function(n,r,t){return{value:n,index:u++,criteria:e(n,r,t)}}).sort(function(n,r){var t=n.criteria,e=r.criteria;if(t!==e){if(e<t||void 0===t)return 1;if(t<e||void 0===e)return-1}return n.index-r.index}),"value")};var k=function(o,r){return function(e,u,n){var i=r?[[],[]]:{};return u=d(u,n),h.each(e,function(n,r){var t=u(n,r,e);o(i,n,t)}),i}};h.groupBy=k(function(n,r,t){j(n,t)?n[t].push(r):n[t]=[r]}),h.indexBy=k(function(n,r,t){n[t]=r}),h.countBy=k(function(n,r,t){j(n,t)?n[t]++:n[t]=1});var S=/[^\ud800-\udfff]|[\ud800-\udbff][\udc00-\udfff]|[\ud800-\udfff]/g;h.toArray=function(n){return n?h.isArray(n)?c.call(n):h.isString(n)?n.match(S):w(n)?h.map(n,h.identity):h.values(n):[]},h.size=function(n){return null==n?0:w(n)?n.length:h.keys(n).length},h.partition=k(function(n,r,t){n[t?0:1].push(r)},!0),h.first=h.head=h.take=function(n,r,t){return null==n||n.length<1?null==r?void 0:[]:null==r||t?n[0]:h.initial(n,n.length-r)},h.initial=function(n,r,t){return c.call(n,0,Math.max(0,n.length-(null==r||t?1:r)))},h.last=function(n,r,t){return null==n||n.length<1?null==r?void 0:[]:null==r||t?n[n.length-1]:h.rest(n,Math.max(0,n.length-r))},h.rest=h.tail=h.drop=function(n,r,t){return c.call(n,null==r||t?1:r)},h.compact=function(n){return h.filter(n,Boolean)};var M=function(n,r,t,e){for(var u=(e=e||[]).length,i=0,o=A(n);i<o;i++){var a=n[i];if(w(a)&&(h.isArray(a)||h.isArguments(a)))if(r)for(var c=0,l=a.length;c<l;)e[u++]=a[c++];else M(a,r,t,e),u=e.length;else t||(e[u++]=a)}return e};h.flatten=function(n,r){return M(n,r,!1)},h.without=g(function(n,r){return h.difference(n,r)}),h.uniq=h.unique=function(n,r,t,e){h.isBoolean(r)||(e=t,t=r,r=!1),null!=t&&(t=d(t,e));for(var u=[],i=[],o=0,a=A(n);o<a;o++){var c=n[o],l=t?t(c,o,n):c;r&&!t?(o&&i===l||u.push(c),i=l):t?h.contains(i,l)||(i.push(l),u.push(c)):h.contains(u,c)||u.push(c)}return u},h.union=g(function(n){return h.uniq(M(n,!0,!0))}),h.intersection=function(n){for(var r=[],t=arguments.length,e=0,u=A(n);e<u;e++){var i=n[e];if(!h.contains(r,i)){var o;for(o=1;o<t&&h.contains(arguments[o],i);o++);o===t&&r.push(i)}}return r},h.difference=g(function(n,r){return r=M(r,!0,!0),h.filter(n,function(n){return!h.contains(r,n)})}),h.unzip=function(n){for(var r=n&&h.max(n,A).length||0,t=Array(r),e=0;e<r;e++)t[e]=h.pluck(n,e);return t},h.zip=g(h.unzip),h.object=function(n,r){for(var t={},e=0,u=A(n);e<u;e++)r?t[n[e]]=r[e]:t[n[e][0]]=n[e][1];return t};var F=function(i){return function(n,r,t){r=d(r,t);for(var e=A(n),u=0<i?0:e-1;0<=u&&u<e;u+=i)if(r(n[u],u,n))return u;return-1}};h.findIndex=F(1),h.findLastIndex=F(-1),h.sortedIndex=function(n,r,t,e){for(var u=(t=d(t,e,1))(r),i=0,o=A(n);i<o;){var a=Math.floor((i+o)/2);t(n[a])<u?i=a+1:o=a}return i};var E=function(i,o,a){return function(n,r,t){var e=0,u=A(n);if("number"==typeof t)0<i?e=0<=t?t:Math.max(t+u,e):u=0<=t?Math.min(t+1,u):t+u+1;else if(a&&t&&u)return n[t=a(n,r)]===r?t:-1;if(r!=r)return 0<=(t=o(c.call(n,e,u),h.isNaN))?t+e:-1;for(t=0<i?e:u-1;0<=t&&t<u;t+=i)if(n[t]===r)return t;return-1}};h.indexOf=E(1,h.findIndex,h.sortedIndex),h.lastIndexOf=E(-1,h.findLastIndex),h.range=function(n,r,t){null==r&&(r=n||0,n=0),t||(t=r<n?-1:1);for(var e=Math.max(Math.ceil((r-n)/t),0),u=Array(e),i=0;i<e;i++,n+=t)u[i]=n;return u},h.chunk=function(n,r){if(null==r||r<1)return[];for(var t=[],e=0,u=n.length;e<u;)t.push(c.call(n,e,e+=r));return t};var N=function(n,r,t,e,u){if(!(e instanceof r))return n.apply(t,u);var i=m(n.prototype),o=n.apply(i,u);return h.isObject(o)?o:i};h.bind=g(function(r,t,e){if(!h.isFunction(r))throw new TypeError("Bind must be called on a function");var u=g(function(n){return N(r,u,t,this,e.concat(n))});return u}),h.partial=g(function(u,i){var o=h.partial.placeholder,a=function(){for(var n=0,r=i.length,t=Array(r),e=0;e<r;e++)t[e]=i[e]===o?arguments[n++]:i[e];for(;n<arguments.length;)t.push(arguments[n++]);return N(u,a,this,this,t)};return a}),(h.partial.placeholder=h).bindAll=g(function(n,r){var t=(r=M(r,!1,!1)).length;if(t<1)throw new Error("bindAll must be passed function names");for(;t--;){var e=r[t];n[e]=h.bind(n[e],n)}}),h.memoize=function(e,u){var i=function(n){var r=i.cache,t=""+(u?u.apply(this,arguments):n);return j(r,t)||(r[t]=e.apply(this,arguments)),r[t]};return i.cache={},i},h.delay=g(function(n,r,t){return setTimeout(function(){return n.apply(null,t)},r)}),h.defer=h.partial(h.delay,h,1),h.throttle=function(t,e,u){var i,o,a,c,l=0;u||(u={});var f=function(){l=!1===u.leading?0:h.now(),i=null,c=t.apply(o,a),i||(o=a=null)},n=function(){var n=h.now();l||!1!==u.leading||(l=n);var r=e-(n-l);return o=this,a=arguments,r<=0||e<r?(i&&(clearTimeout(i),i=null),l=n,c=t.apply(o,a),i||(o=a=null)):i||!1===u.trailing||(i=setTimeout(f,r)),c};return n.cancel=function(){clearTimeout(i),l=0,i=o=a=null},n},h.debounce=function(t,e,u){var i,o,a=function(n,r){i=null,r&&(o=t.apply(n,r))},n=g(function(n){if(i&&clearTimeout(i),u){var r=!i;i=setTimeout(a,e),r&&(o=t.apply(this,n))}else i=h.delay(a,e,this,n);return o});return n.cancel=function(){clearTimeout(i),i=null},n},h.wrap=function(n,r){return h.partial(r,n)},h.negate=function(n){return function(){return!n.apply(this,arguments)}},h.compose=function(){var t=arguments,e=t.length-1;return function(){for(var n=e,r=t[e].apply(this,arguments);n--;)r=t[n].call(this,r);return r}},h.after=function(n,r){return function(){if(--n<1)return r.apply(this,arguments)}},h.before=function(n,r){var t;return function(){return 0<--n&&(t=r.apply(this,arguments)),n<=1&&(r=null),t}},h.once=h.partial(h.before,2),h.restArguments=g;var I=!{toString:null}.propertyIsEnumerable("toString"),T=["valueOf","isPrototypeOf","toString","propertyIsEnumerable","hasOwnProperty","toLocaleString"],B=function(n,r){var t=T.length,e=n.constructor,u=h.isFunction(e)&&e.prototype||o,i="constructor";for(j(n,i)&&!h.contains(r,i)&&r.push(i);t--;)(i=T[t])in n&&n[i]!==u[i]&&!h.contains(r,i)&&r.push(i)};h.keys=function(n){if(!h.isObject(n))return[];if(a)return a(n);var r=[];for(var t in n)j(n,t)&&r.push(t);return I&&B(n,r),r},h.allKeys=function(n){if(!h.isObject(n))return[];var r=[];for(var t in n)r.push(t);return I&&B(n,r),r},h.values=function(n){for(var r=h.keys(n),t=r.length,e=Array(t),u=0;u<t;u++)e[u]=n[r[u]];return e},h.mapObject=function(n,r,t){r=d(r,t);for(var e=h.keys(n),u=e.length,i={},o=0;o<u;o++){var a=e[o];i[a]=r(n[a],a,n)}return i},h.pairs=function(n){for(var r=h.keys(n),t=r.length,e=Array(t),u=0;u<t;u++)e[u]=[r[u],n[r[u]]];return e},h.invert=function(n){for(var r={},t=h.keys(n),e=0,u=t.length;e<u;e++)r[n[t[e]]]=t[e];return r},h.functions=h.methods=function(n){var r=[];for(var t in n)h.isFunction(n[t])&&r.push(t);return r.sort()};var R=function(c,l){return function(n){var r=arguments.length;if(l&&(n=Object(n)),r<2||null==n)return n;for(var t=1;t<r;t++)for(var e=arguments[t],u=c(e),i=u.length,o=0;o<i;o++){var a=u[o];l&&void 0!==n[a]||(n[a]=e[a])}return n}};h.extend=R(h.allKeys),h.extendOwn=h.assign=R(h.keys),h.findKey=function(n,r,t){r=d(r,t);for(var e,u=h.keys(n),i=0,o=u.length;i<o;i++)if(r(n[e=u[i]],e,n))return e};var q,K,z=function(n,r,t){return r in t};h.pick=g(function(n,r){var t={},e=r[0];if(null==n)return t;h.isFunction(e)?(1<r.length&&(e=y(e,r[1])),r=h.allKeys(n)):(e=z,r=M(r,!1,!1),n=Object(n));for(var u=0,i=r.length;u<i;u++){var o=r[u],a=n[o];e(a,o,n)&&(t[o]=a)}return t}),h.omit=g(function(n,t){var r,e=t[0];return h.isFunction(e)?(e=h.negate(e),1<t.length&&(r=t[1])):(t=h.map(M(t,!1,!1),String),e=function(n,r){return!h.contains(t,r)}),h.pick(n,e,r)}),h.defaults=R(h.allKeys,!0),h.create=function(n,r){var t=m(n);return r&&h.extendOwn(t,r),t},h.clone=function(n){return h.isObject(n)?h.isArray(n)?n.slice():h.extend({},n):n},h.tap=function(n,r){return r(n),n},h.isMatch=function(n,r){var t=h.keys(r),e=t.length;if(null==n)return!e;for(var u=Object(n),i=0;i<e;i++){var o=t[i];if(r[o]!==u[o]||!(o in u))return!1}return!0},q=function(n,r,t,e){if(n===r)return 0!==n||1/n==1/r;if(null==n||null==r)return!1;if(n!=n)return r!=r;var u=typeof n;return("function"===u||"object"===u||"object"==typeof r)&&K(n,r,t,e)},K=function(n,r,t,e){n instanceof h&&(n=n._wrapped),r instanceof h&&(r=r._wrapped);var u=p.call(n);if(u!==p.call(r))return!1;switch(u){case"[object RegExp]":case"[object String]":return""+n==""+r;case"[object Number]":return+n!=+n?+r!=+r:0==+n?1/+n==1/r:+n==+r;case"[object Date]":case"[object Boolean]":return+n==+r;case"[object Symbol]":return s.valueOf.call(n)===s.valueOf.call(r)}var i="[object Array]"===u;if(!i){if("object"!=typeof n||"object"!=typeof r)return!1;var o=n.constructor,a=r.constructor;if(o!==a&&!(h.isFunction(o)&&o instanceof o&&h.isFunction(a)&&a instanceof a)&&"constructor"in n&&"constructor"in r)return!1}e=e||[];for(var c=(t=t||[]).length;c--;)if(t[c]===n)return e[c]===r;if(t.push(n),e.push(r),i){if((c=n.length)!==r.length)return!1;for(;c--;)if(!q(n[c],r[c],t,e))return!1}else{var l,f=h.keys(n);if(c=f.length,h.keys(r).length!==c)return!1;for(;c--;)if(l=f[c],!j(r,l)||!q(n[l],r[l],t,e))return!1}return t.pop(),e.pop(),!0},h.isEqual=function(n,r){return q(n,r)},h.isEmpty=function(n){return null==n||(w(n)&&(h.isArray(n)||h.isString(n)||h.isArguments(n))?0===n.length:0===h.keys(n).length)},h.isElement=function(n){return!(!n||1!==n.nodeType)},h.isArray=t||function(n){return"[object Array]"===p.call(n)},h.isObject=function(n){var r=typeof n;return"function"===r||"object"===r&&!!n},h.each(["Arguments","Function","String","Number","Date","RegExp","Error","Symbol","Map","WeakMap","Set","WeakSet"],function(r){h["is"+r]=function(n){return p.call(n)==="[object "+r+"]"}}),h.isArguments(arguments)||(h.isArguments=function(n){return j(n,"callee")});var D=n.document&&n.document.childNodes;"function"!=typeof/./&&"object"!=typeof Int8Array&&"function"!=typeof D&&(h.isFunction=function(n){return"function"==typeof n||!1}),h.isFinite=function(n){return!h.isSymbol(n)&&isFinite(n)&&!isNaN(parseFloat(n))},h.isNaN=function(n){return h.isNumber(n)&&isNaN(n)},h.isBoolean=function(n){return!0===n||!1===n||"[object Boolean]"===p.call(n)},h.isNull=function(n){return null===n},h.isUndefined=function(n){return void 0===n},h.has=function(n,r){if(!h.isArray(r))return j(n,r);for(var t=r.length,e=0;e<t;e++){var u=r[e];if(null==n||!i.call(n,u))return!1;n=n[u]}return!!t},h.noConflict=function(){return n._=r,this},h.identity=function(n){return n},h.constant=function(n){return function(){return n}},h.noop=function(){},h.property=function(r){return h.isArray(r)?function(n){return x(n,r)}:b(r)},h.propertyOf=function(r){return null==r?function(){}:function(n){return h.isArray(n)?x(r,n):r[n]}},h.matcher=h.matches=function(r){return r=h.extendOwn({},r),function(n){return h.isMatch(n,r)}},h.times=function(n,r,t){var e=Array(Math.max(0,n));r=y(r,t,1);for(var u=0;u<n;u++)e[u]=r(u);return e},h.random=function(n,r){return null==r&&(r=n,n=0),n+Math.floor(Math.random()*(r-n+1))},h.now=Date.now||function(){return(new Date).getTime()};var L={"&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#x27;","`":"&#x60;"},P=h.invert(L),W=function(r){var t=function(n){return r[n]},n="(?:"+h.keys(r).join("|")+")",e=RegExp(n),u=RegExp(n,"g");return function(n){return n=null==n?"":""+n,e.test(n)?n.replace(u,t):n}};h.escape=W(L),h.unescape=W(P),h.result=function(n,r,t){h.isArray(r)||(r=[r]);var e=r.length;if(!e)return h.isFunction(t)?t.call(n):t;for(var u=0;u<e;u++){var i=null==n?void 0:n[r[u]];void 0===i&&(i=t,u=e),n=h.isFunction(i)?i.call(n):i}return n};var C=0;h.uniqueId=function(n){var r=++C+"";return n?n+r:r},h.templateSettings={evaluate:/<%([\s\S]+?)%>/g,interpolate:/<%=([\s\S]+?)%>/g,escape:/<%-([\s\S]+?)%>/g};var J=/(.)^/,U={"'":"'","\\":"\\","\r":"r","\n":"n","\u2028":"u2028","\u2029":"u2029"},V=/\\|'|\r|\n|\u2028|\u2029/g,$=function(n){return"\\"+U[n]};h.template=function(i,n,r){!n&&r&&(n=r),n=h.defaults({},n,h.templateSettings);var t,e=RegExp([(n.escape||J).source,(n.interpolate||J).source,(n.evaluate||J).source].join("|")+"|$","g"),o=0,a="__p+='";i.replace(e,function(n,r,t,e,u){return a+=i.slice(o,u).replace(V,$),o=u+n.length,r?a+="'+\n((__t=("+r+"))==null?'':_.escape(__t))+\n'":t?a+="'+\n((__t=("+t+"))==null?'':__t)+\n'":e&&(a+="';\n"+e+"\n__p+='"),n}),a+="';\n",n.variable||(a="with(obj||{}){\n"+a+"}\n"),a="var __t,__p='',__j=Array.prototype.join,"+"print=function(){__p+=__j.call(arguments,'');};\n"+a+"return __p;\n";try{t=new Function(n.variable||"obj","_",a)}catch(n){throw n.source=a,n}var u=function(n){return t.call(this,n,h)},c=n.variable||"obj";return u.source="function("+c+"){\n"+a+"}",u},h.chain=function(n){var r=h(n);return r._chain=!0,r};var G=function(n,r){return n._chain?h(r).chain():r};h.mixin=function(t){return h.each(h.functions(t),function(n){var r=h[n]=t[n];h.prototype[n]=function(){var n=[this._wrapped];return u.apply(n,arguments),G(this,r.apply(h,n))}}),h},h.mixin(h),h.each(["pop","push","reverse","shift","sort","splice","unshift"],function(r){var t=e[r];h.prototype[r]=function(){var n=this._wrapped;return t.apply(n,arguments),"shift"!==r&&"splice"!==r||0!==n.length||delete n[0],G(this,n)}}),h.each(["concat","join","slice"],function(n){var r=e[n];h.prototype[n]=function(){return G(this,r.apply(this._wrapped,arguments))}}),h.prototype.value=function(){return this._wrapped},h.prototype.valueOf=h.prototype.toJSON=h.prototype.value,h.prototype.toString=function(){return String(this._wrapped)},"function"==typeof define&&define.amd&&define("underscore",[],function(){return h})}();
(function(n){var t=typeof self=="object"&&self.self===self&&self||typeof global=="object"&&global.global===global&&global;t.Backbone=n(t,{},t._,t.jQuery||t.Zepto||t.ender||t.$)})(function(n,t,i,r){var ot=n.Backbone,y=Array.prototype.slice,h,k,d,c,g,l,o,nt,it,ut,et,v,s;t.VERSION="1.3.3";t.$=r;t.noConflict=function(){return n.Backbone=ot,this};t.emulateHTTP=!1;t.emulateJSON=!1;var st=function(n,t,r){switch(n){case 1:return function(){return i[t](this[r])};case 2:return function(n){return i[t](this[r],n)};case 3:return function(n,u){return i[t](this[r],w(n,this),u)};case 4:return function(n,u,f){return i[t](this[r],w(n,this),u,f)};default:return function(){var n=y.call(arguments);return n.unshift(this[r]),i[t].apply(i,n)}}},p=function(n,t,r){i.each(t,function(t,u){i[u]&&(n.prototype[u]=st(t,u,r))})},w=function(n,t){return i.isFunction(n)?n:i.isObject(n)&&!t._isModel(n)?ht(n):i.isString(n)?function(t){return t.get(n)}:n},ht=function(n){var t=i.matches(n);return function(n){return t(n.attributes)}},u=t.Events={},b=/\s+/,e=function(n,t,r,u,f){var o=0,s;if(r&&typeof r=="object")for(u!==void 0&&("context"in f)&&f.context===void 0&&(f.context=u),s=i.keys(r);o<s.length;o++)t=e(n,t,s[o],r[s[o]],f);else if(r&&b.test(r))for(s=r.split(b);o<s.length;o++)t=n(t,s[o],u,f);else t=n(t,r,u,f);return t};u.on=function(n,t,i){return h(this,n,t,i)};h=function(n,t,i,r,u){if(n._events=e(k,n._events||{},t,i,{context:r,ctx:n,listening:u}),u){var f=n._listeners||(n._listeners={});f[u.id]=u}return n};u.listenTo=function(n,t,r){var o;if(!n)return this;var u=n._listenId||(n._listenId=i.uniqueId("l")),f=this._listeningTo||(this._listeningTo={}),e=f[u];return e||(o=this._listenId||(this._listenId=i.uniqueId("l")),e=f[u]={obj:n,objId:u,id:o,listeningTo:f,count:0}),h(n,t,r,this,e),this};k=function(n,t,i,r){if(i){var e=n[t]||(n[t]=[]),f=r.context,o=r.ctx,u=r.listening;u&&u.count++;e.push({callback:i,context:f,ctx:f||o,listening:u})}return n};u.off=function(n,t,i){return this._events?(this._events=e(d,this._events,n,t,{context:i,listeners:this._listeners}),this):this};u.stopListening=function(n,t,r){var f=this._listeningTo,e,u,o;if(!f)return this;for(e=n?[n._listenId]:i.keys(f),u=0;u<e.length;u++){if(o=f[e[u]],!o)break;o.obj.off(t,r,this)}return this};d=function(n,t,r,u){var v,y,h,c,l,o;if(n){var e=0,f,a=u.context,s=u.listeners;if(!t&&!r&&!a){for(v=i.keys(s);e<v.length;e++)f=s[v[e]],delete s[f.id],delete f.listeningTo[f.objId];return}for(y=t?[t]:i.keys(n);e<y.length;e++){if(t=y[e],h=n[t],!h)break;for(c=[],l=0;l<h.length;l++)o=h[l],r&&r!==o.callback&&r!==o.callback._callback||a&&a!==o.context?c.push(o):(f=o.listening,f&&--f.count==0&&(delete s[f.id],delete f.listeningTo[f.objId]));c.length?n[t]=c:delete n[t]}return n}};u.once=function(n,t,r){var u=e(c,{},n,t,i.bind(this.off,this));typeof n=="string"&&r==null&&(t=void 0);return this.on(u,t,r)};u.listenToOnce=function(n,t,r){var u=e(c,{},t,r,i.bind(this.stopListening,this,n));return this.listenTo(n,u)};c=function(n,t,r,u){if(r){var f=n[t]=i.once(function(){u(t,f);r.apply(this,arguments)});f._callback=r}return n};u.trigger=function(n){var i,r,t;if(!this._events)return this;for(i=Math.max(0,arguments.length-1),r=Array(i),t=0;t<i;t++)r[t]=arguments[t+1];return e(g,this._events,n,void 0,r),this};g=function(n,t,i,r){if(n){var f=n[t],u=n.all;f&&u&&(u=u.slice());f&&l(f,r);u&&l(u,[t].concat(r))}return n};l=function(n,t){var i,r=-1,u=n.length,f=t[0],e=t[1],o=t[2];switch(t.length){case 0:while(++r<u)(i=n[r]).callback.call(i.ctx);return;case 1:while(++r<u)(i=n[r]).callback.call(i.ctx,f);return;case 2:while(++r<u)(i=n[r]).callback.call(i.ctx,f,e);return;case 3:while(++r<u)(i=n[r]).callback.call(i.ctx,f,e,o);return;default:while(++r<u)(i=n[r]).callback.apply(i.ctx,t);return}};u.bind=u.on;u.unbind=u.off;i.extend(t,u);o=t.Model=function(n,t){var r=n||{},u;t||(t={});this.cid=i.uniqueId(this.cidPrefix);this.attributes={};t.collection&&(this.collection=t.collection);t.parse&&(r=this.parse(r,t)||{});u=i.result(this,"defaults");r=i.defaults(i.extend({},u,r),u);this.set(r,t);this.changed={};this.initialize.apply(this,arguments)};i.extend(o.prototype,u,{changed:null,validationError:null,idAttribute:"id",cidPrefix:"c",initialize:function(){},toJSON:function(){return i.clone(this.attributes)},sync:function(){return t.sync.apply(this,arguments)},get:function(n){return this.attributes[n]},escape:function(n){return i.escape(this.get(n))},has:function(n){return this.get(n)!=null},matches:function(n){return!!i.iteratee(n,this)(this.attributes)},set:function(n,t,r){var f,u,o;if(n==null)return this;if(typeof n=="object"?(f=n,r=t):(f={})[n]=t,r||(r={}),!this._validate(f,r))return!1;var a=r.unset,h=r.silent,e=[],c=this._changing;this._changing=!0;c||(this._previousAttributes=i.clone(this.attributes),this.changed={});var s=this.attributes,l=this.changed,v=this._previousAttributes;for(u in f)t=f[u],i.isEqual(s[u],t)||e.push(u),i.isEqual(v[u],t)?delete l[u]:l[u]=t,a?delete s[u]:s[u]=t;if(this.idAttribute in f&&(this.id=this.get(this.idAttribute)),!h)for(e.length&&(this._pending=r),o=0;o<e.length;o++)this.trigger("change:"+e[o],this,s[e[o]],r);if(c)return this;if(!h)while(this._pending)r=this._pending,this._pending=!1,this.trigger("change",this,r);return this._pending=!1,this._changing=!1,this},unset:function(n,t){return this.set(n,void 0,i.extend({},t,{unset:!0}))},clear:function(n){var t={};for(var r in this.attributes)t[r]=void 0;return this.set(t,i.extend({},n,{unset:!0}))},hasChanged:function(n){return n==null?!i.isEmpty(this.changed):i.has(this.changed,n)},changedAttributes:function(n){var f,t,r,u;if(!n)return this.hasChanged()?i.clone(this.changed):!1;f=this._changing?this._previousAttributes:this.attributes;t={};for(r in n)(u=n[r],i.isEqual(f[r],u))||(t[r]=u);return i.size(t)?t:!1},previous:function(n){return n==null||!this._previousAttributes?null:this._previousAttributes[n]},previousAttributes:function(){return i.clone(this._previousAttributes)},fetch:function(n){n=i.extend({parse:!0},n);var t=this,r=n.success;return n.success=function(i){var u=n.parse?t.parse(i,n):i;if(!t.set(u,n))return!1;r&&r.call(n.context,t,i,n);t.trigger("sync",t,i,n)},s(this,n),this.sync("read",this,n)},save:function(n,t,r){var u,e,h,l;if(n==null||typeof n=="object"?(u=n,r=t):(u={})[n]=t,r=i.extend({validate:!0,parse:!0},r),e=r.wait,u&&!e){if(!this.set(u,r))return!1}else if(!this._validate(u,r))return!1;var f=this,c=r.success,o=this.attributes;return r.success=function(n){f.attributes=o;var t=r.parse?f.parse(n,r):n;if(e&&(t=i.extend({},u,t)),t&&!f.set(t,r))return!1;c&&c.call(r.context,f,n,r);f.trigger("sync",f,n,r)},s(this,r),u&&e&&(this.attributes=i.extend({},o,u)),h=this.isNew()?"create":r.patch?"patch":"update",h!=="patch"||r.attrs||(r.attrs=u),l=this.sync(h,this,r),this.attributes=o,l},destroy:function(n){var r;n=n?i.clone(n):{};var t=this,u=n.success,f=n.wait,e=function(){t.stopListening();t.trigger("destroy",t,t.collection,n)};return n.success=function(i){f&&e();u&&u.call(n.context,t,i,n);t.isNew()||t.trigger("sync",t,i,n)},r=!1,this.isNew()?i.defer(n.success):(s(this,n),r=this.sync("delete",this,n)),f||e(),r},url:function(){var n=i.result(this,"urlRoot")||i.result(this.collection,"url")||v(),t;return this.isNew()?n:(t=this.get(this.idAttribute),n.replace(/[^\/]$/,"$&/")+encodeURIComponent(t))},parse:function(n){return n},clone:function(){return new this.constructor(this.attributes)},isNew:function(){return!this.has(this.idAttribute)},isValid:function(n){return this._validate({},i.extend({},n,{validate:!0}))},_validate:function(n,t){if(!t.validate||!this.validate)return!0;n=i.extend({},this.attributes,n);var r=this.validationError=this.validate(n,t)||null;return r?(this.trigger("invalid",this,r,i.extend(t,{validationError:r})),!1):!0}});nt={keys:1,values:1,pairs:1,invert:1,pick:0,omit:0,chain:1,isEmpty:1};p(o,nt,"attributes");var a=t.Collection=function(n,t){t||(t={});t.model&&(this.model=t.model);t.comparator!==void 0&&(this.comparator=t.comparator);this._reset();this.initialize.apply(this,arguments);n&&this.reset(n,i.extend({silent:!0},t))},ct={add:!0,remove:!0,merge:!0},lt={add:!0,remove:!1},tt=function(n,t,i){i=Math.min(Math.max(i,0),n.length);for(var u=Array(n.length-i),f=t.length,r=0;r<u.length;r++)u[r]=n[r+i];for(r=0;r<f;r++)n[r+i]=t[r];for(r=0;r<u.length;r++)n[r+f+i]=u[r]};i.extend(a.prototype,u,{model:o,initialize:function(){},toJSON:function(n){return this.map(function(t){return t.toJSON(n)})},sync:function(){return t.sync.apply(this,arguments)},add:function(n,t){return this.set(n,i.extend({merge:!1},t,lt))},remove:function(n,t){var u,r;return t=i.extend({},t),u=!i.isArray(n),n=u?[n]:n.slice(),r=this._removeModels(n,t),!t.silent&&r.length&&(t.changes={added:[],merged:[],removed:r},this.trigger("update",this,t)),u?r[0]:r},set:function(n,t){var v,f,e,a,w,d;if(n!=null){t=i.extend({},ct,t);t.parse&&!this._isModel(n)&&(n=this.parse(n,t)||[]);v=!i.isArray(n);n=v?[n]:n.slice();f=t.at;f!=null&&(f=+f);f>this.length&&(f=this.length);f<0&&(f+=this.length+1);for(var s=[],o=[],y=[],h=[],l={},b=t.add,g=t.merge,k=t.remove,c=!1,p=this.comparator&&f==null&&t.sort!==!1,nt=i.isString(this.comparator)?this.comparator:null,r,u=0;u<n.length;u++)r=n[u],e=this.get(r),e?(g&&r!==e&&(a=this._isModel(r)?r.attributes:r,t.parse&&(a=e.parse(a,t)),e.set(a,t),y.push(e),p&&!c&&(c=e.hasChanged(nt))),l[e.cid]||(l[e.cid]=!0,s.push(e)),n[u]=e):b&&(r=n[u]=this._prepareModel(r,t),r&&(o.push(r),this._addReference(r,t),l[r.cid]=!0,s.push(r)));if(k){for(u=0;u<this.length;u++)r=this.models[u],l[r.cid]||h.push(r);h.length&&this._removeModels(h,t)}if(w=!1,d=!p&&b&&k,s.length&&d?(w=this.length!==s.length||i.some(this.models,function(n,t){return n!==s[t]}),this.models.length=0,tt(this.models,s,0),this.length=this.models.length):o.length&&(p&&(c=!0),tt(this.models,o,f==null?this.length:f),this.length=this.models.length),c&&this.sort({silent:!0}),!t.silent){for(u=0;u<o.length;u++)f!=null&&(t.index=f+u),r=o[u],r.trigger("add",r,this,t);(c||w)&&this.trigger("sort",this,t);(o.length||h.length||y.length)&&(t.changes={added:o,removed:h,merged:y},this.trigger("update",this,t))}return v?n[0]:n}},reset:function(n,t){t=t?i.clone(t):{};for(var r=0;r<this.models.length;r++)this._removeReference(this.models[r],t);return t.previousModels=this.models,this._reset(),n=this.add(n,i.extend({silent:!0},t)),t.silent||this.trigger("reset",this,t),n},push:function(n,t){return this.add(n,i.extend({at:this.length},t))},pop:function(n){var t=this.at(this.length-1);return this.remove(t,n)},unshift:function(n,t){return this.add(n,i.extend({at:0},t))},shift:function(n){var t=this.at(0);return this.remove(t,n)},slice:function(){return y.apply(this.models,arguments)},get:function(n){if(n!=null)return this._byId[n]||this._byId[this.modelId(n.attributes||n)]||n.cid&&this._byId[n.cid]},has:function(n){return this.get(n)!=null},at:function(n){return n<0&&(n+=this.length),this.models[n]},where:function(n,t){return this[t?"find":"filter"](n)},findWhere:function(n){return this.where(n,!0)},sort:function(n){var t=this.comparator,r;if(!t)throw new Error("Cannot sort a set without a comparator");return n||(n={}),r=t.length,i.isFunction(t)&&(t=i.bind(t,this)),r===1||i.isString(t)?this.models=this.sortBy(t):this.models.sort(t),n.silent||this.trigger("sort",this,n),this},pluck:function(n){return this.map(n+"")},fetch:function(n){n=i.extend({parse:!0},n);var r=n.success,t=this;return n.success=function(i){var u=n.reset?"reset":"set";t[u](i,n);r&&r.call(n.context,t,i,n);t.trigger("sync",t,i,n)},s(this,n),this.sync("read",this,n)},create:function(n,t){var r,f,u;return(t=t?i.clone(t):{},r=t.wait,n=this._prepareModel(n,t),!n)?!1:(r||this.add(n,t),f=this,u=t.success,t.success=function(n,t,i){r&&f.add(n,i);u&&u.call(i.context,n,t,i)},n.save(null,t),n)},parse:function(n){return n},clone:function(){return new this.constructor(this.models,{model:this.model,comparator:this.comparator})},modelId:function(n){return n[this.model.prototype.idAttribute||"id"]},_reset:function(){this.length=0;this.models=[];this._byId={}},_prepareModel:function(n,t){if(this._isModel(n))return n.collection||(n.collection=this),n;t=t?i.clone(t):{};t.collection=this;var r=new this.model(n,t);return r.validationError?(this.trigger("invalid",this,r.validationError,t),!1):r},_removeModels:function(n,t){for(var i,u,f,e=[],r=0;r<n.length;r++)(i=this.get(n[r]),i)&&(u=this.indexOf(i),this.models.splice(u,1),this.length--,delete this._byId[i.cid],f=this.modelId(i.attributes),f!=null&&delete this._byId[f],t.silent||(t.index=u,i.trigger("remove",i,this,t)),e.push(i),this._removeReference(i,t));return e},_isModel:function(n){return n instanceof o},_addReference:function(n){this._byId[n.cid]=n;var t=this.modelId(n.attributes);t!=null&&(this._byId[t]=n);n.on("all",this._onModelEvent,this)},_removeReference:function(n){delete this._byId[n.cid];var t=this.modelId(n.attributes);t!=null&&delete this._byId[t];this===n.collection&&delete n.collection;n.off("all",this._onModelEvent,this)},_onModelEvent:function(n,t,i,r){if(t){if((n==="add"||n==="remove")&&i!==this)return;if(n==="destroy"&&this.remove(t,r),n==="change"){var u=this.modelId(t.previousAttributes()),f=this.modelId(t.attributes);u!==f&&(u!=null&&delete this._byId[u],f!=null&&(this._byId[f]=t))}}this.trigger.apply(this,arguments)}});it={forEach:3,each:3,map:3,collect:3,reduce:0,foldl:0,inject:0,reduceRight:0,foldr:0,find:3,detect:3,filter:3,select:3,reject:3,every:3,all:3,some:3,any:3,include:3,includes:3,contains:3,invoke:0,max:3,min:3,toArray:1,size:1,first:3,head:3,take:3,initial:3,rest:3,tail:3,drop:3,last:3,without:0,difference:0,indexOf:3,shuffle:1,lastIndexOf:3,isEmpty:1,chain:1,sample:3,partition:3,groupBy:3,countBy:3,sortBy:3,indexBy:3,findIndex:3,findLastIndex:3};p(a,it,"models");var rt=t.View=function(n){this.cid=i.uniqueId("view");i.extend(this,i.pick(n,vt));this._ensureElement();this.initialize.apply(this,arguments)},at=/^(\S+)\s*(.*)$/,vt=["model","collection","el","id","attributes","className","tagName","events"];i.extend(rt.prototype,u,{tagName:"div",$:function(n){return this.$el.find(n)},initialize:function(){},render:function(){return this},remove:function(){return this._removeElement(),this.stopListening(),this},_removeElement:function(){this.$el.remove()},setElement:function(n){return this.undelegateEvents(),this._setElement(n),this.delegateEvents(),this},_setElement:function(n){this.$el=n instanceof t.$?n:t.$(n);this.el=this.$el[0]},delegateEvents:function(n){var r,t,u;if(n||(n=i.result(this,"events")),!n)return this;this.undelegateEvents();for(r in n)(t=n[r],i.isFunction(t)||(t=this[t]),t)&&(u=r.match(at),this.delegate(u[1],u[2],i.bind(t,this)));return this},delegate:function(n,t,i){this.$el.on(n+".delegateEvents"+this.cid,t,i);return this},undelegateEvents:function(){return this.$el&&this.$el.off(".delegateEvents"+this.cid),this},undelegate:function(n,t,i){return this.$el.off(n+".delegateEvents"+this.cid,t,i),this},_createElement:function(n){return document.createElement(n)},_ensureElement:function(){if(this.el)this.setElement(i.result(this,"el"));else{var n=i.extend({},i.result(this,"attributes"));this.id&&(n.id=i.result(this,"id"));this.className&&(n["class"]=i.result(this,"className"));this.setElement(this._createElement(i.result(this,"tagName")));this._setAttributes(n)}},_setAttributes:function(n){this.$el.attr(n)}});t.sync=function(n,r,u){var e=ut[n],f,o,s,h;return i.defaults(u||(u={}),{emulateHTTP:t.emulateHTTP,emulateJSON:t.emulateJSON}),f={type:e,dataType:"json"},u.url||(f.url=i.result(r,"url")||v()),u.data==null&&r&&(n==="create"||n==="update"||n==="patch")&&(f.contentType="application/json",f.data=JSON.stringify(u.attrs||r.toJSON(u))),u.emulateJSON&&(f.contentType="application/x-www-form-urlencoded",f.data=f.data?{model:f.data}:{}),u.emulateHTTP&&(e==="PUT"||e==="DELETE"||e==="PATCH")&&(f.type="POST",u.emulateJSON&&(f.data._method=e),o=u.beforeSend,u.beforeSend=function(n){return n.setRequestHeader("X-HTTP-Method-Override",e),o?o.apply(this,arguments):void 0}),f.type==="GET"||u.emulateJSON||(f.processData=!1),s=u.error,u.error=function(n,t,i){u.textStatus=t;u.errorThrown=i;s&&s.call(u.context,n,t,i)},h=u.xhr=t.ajax(i.extend(f,u)),r.trigger("request",r,h,u),h};ut={create:"POST",update:"PUT",patch:"PATCH","delete":"DELETE",read:"GET"};t.ajax=function(){return t.$.ajax.apply(t.$,arguments)};var ft=t.Router=function(n){n||(n={});n.routes&&(this.routes=n.routes);this._bindRoutes();this.initialize.apply(this,arguments)},yt=/\((.*?)\)/g,pt=/(\(\?)?:\w+/g,wt=/\*\w+/g,bt=/[\-{}\[\]+?.,\\\^$|#\s]/g;i.extend(ft.prototype,u,{initialize:function(){},route:function(n,r,u){i.isRegExp(n)||(n=this._routeToRegExp(n));i.isFunction(r)&&(u=r,r="");u||(u=this[r]);var f=this;return t.history.route(n,function(i){var e=f._extractParameters(n,i);f.execute(u,e,r)!==!1&&(f.trigger.apply(f,["route:"+r].concat(e)),f.trigger("route",r,e),t.history.trigger("route",f,r,e))}),this},execute:function(n,t){n&&n.apply(this,t)},navigate:function(n,i){return t.history.navigate(n,i),this},_bindRoutes:function(){if(this.routes){this.routes=i.result(this,"routes");for(var n,t=i.keys(this.routes);(n=t.pop())!=null;)this.route(n,this.routes[n])}},_routeToRegExp:function(n){return n=n.replace(bt,"\\$&").replace(yt,"(?:$1)?").replace(pt,function(n,t){return t?n:"([^/?]+)"}).replace(wt,"([^?]*?)"),new RegExp("^"+n+"(?:\\?([\\s\\S]*))?$")},_extractParameters:function(n,t){var r=n.exec(t).slice(1);return i.map(r,function(n,t){return t===r.length-1?n||null:n?decodeURIComponent(n):null})}});var f=t.History=function(){this.handlers=[];this.checkUrl=i.bind(this.checkUrl,this);typeof window!="undefined"&&(this.location=window.location,this.history=window.history)},kt=/^[#\/]|\s+$/g,dt=/^\/+|\/+$/g,gt=/#.*$/;return f.started=!1,i.extend(f.prototype,u,{interval:50,atRoot:function(){var n=this.location.pathname.replace(/[^\/]$/,"$&/");return n===this.root&&!this.getSearch()},matchRoot:function(){var n=this.decodeFragment(this.location.pathname),t=n.slice(0,this.root.length-1)+"/";return t===this.root},decodeFragment:function(n){return decodeURI(n.replace(/%25/g,"%2525"))},getSearch:function(){var n=this.location.href.replace(/#.*/,"").match(/\?.+/);return n?n[0]:""},getHash:function(n){var t=(n||this).location.href.match(/#(.*)$/);return t?t[1]:""},getPath:function(){var n=this.decodeFragment(this.location.pathname+this.getSearch()).slice(this.root.length-1);return n.charAt(0)==="/"?n.slice(1):n},getFragment:function(n){return n==null&&(n=this._usePushState||!this._wantsHashChange?this.getPath():this.getHash()),n.replace(kt,"")},start:function(n){var e,r,t,u;if(f.started)throw new Error("Backbone.history has already been started");if(f.started=!0,this.options=i.extend({root:"/"},this.options,n),this.root=this.options.root,this._wantsHashChange=this.options.hashChange!==!1,this._hasHashChange="onhashchange"in window&&(document.documentMode===void 0||document.documentMode>7),this._useHashChange=this._wantsHashChange&&this._hasHashChange,this._wantsPushState=!!this.options.pushState,this._hasPushState=!!(this.history&&this.history.pushState),this._usePushState=this._wantsPushState&&this._hasPushState,this.fragment=this.getFragment(),this.root=("/"+this.root+"/").replace(dt,"/"),this._wantsHashChange&&this._wantsPushState)if(this._hasPushState||this.atRoot())this._hasPushState&&this.atRoot()&&this.navigate(this.getHash(),{replace:!0});else return e=this.root.slice(0,-1)||"/",this.location.replace(e+"#"+this.getPath()),!0;return this._hasHashChange||!this._wantsHashChange||this._usePushState||(this.iframe=document.createElement("iframe"),this.iframe.src="javascript:0",this.iframe.style.display="none",this.iframe.tabIndex=-1,r=document.body,t=r.insertBefore(this.iframe,r.firstChild).contentWindow,t.document.open(),t.document.close(),t.location.hash="#"+this.fragment),u=window.addEventListener||function(n,t){return attachEvent("on"+n,t)},this._usePushState?u("popstate",this.checkUrl,!1):this._useHashChange&&!this.iframe?u("hashchange",this.checkUrl,!1):this._wantsHashChange&&(this._checkUrlInterval=setInterval(this.checkUrl,this.interval)),this.options.silent?void 0:this.loadUrl()},stop:function(){var n=window.removeEventListener||function(n,t){return detachEvent("on"+n,t)};this._usePushState?n("popstate",this.checkUrl,!1):this._useHashChange&&!this.iframe&&n("hashchange",this.checkUrl,!1);this.iframe&&(document.body.removeChild(this.iframe),this.iframe=null);this._checkUrlInterval&&clearInterval(this._checkUrlInterval);f.started=!1},route:function(n,t){this.handlers.unshift({route:n,callback:t})},checkUrl:function(){var n=this.getFragment();if(n===this.fragment&&this.iframe&&(n=this.getHash(this.iframe.contentWindow)),n===this.fragment)return!1;this.iframe&&this.navigate(n);this.loadUrl()},loadUrl:function(n){return this.matchRoot()?(n=this.fragment=this.getFragment(n),i.some(this.handlers,function(t){if(t.route.test(n))return t.callback(n),!0})):!1},navigate:function(n,t){var i,u,r;if(!f.started)return!1;if(t&&t!==!0||(t={trigger:!!t}),n=this.getFragment(n||""),i=this.root,(n===""||n.charAt(0)==="?")&&(i=i.slice(0,-1)||"/"),u=i+n,n=this.decodeFragment(n.replace(gt,"")),this.fragment!==n){if(this.fragment=n,this._usePushState)this.history[t.replace?"replaceState":"pushState"]({},document.title,u);else if(this._wantsHashChange)this._updateHash(this.location,n,t.replace),this.iframe&&n!==this.getHash(this.iframe.contentWindow)&&(r=this.iframe.contentWindow,t.replace||(r.document.open(),r.document.close()),this._updateHash(r.location,n,t.replace));else return this.location.assign(u);if(t.trigger)return this.loadUrl(n)}},_updateHash:function(n,t,i){if(i){var r=n.href.replace(/(javascript:|#).*$/,"");n.replace(r+"#"+t)}else n.hash="#"+t}}),t.history=new f,et=function(n,t){var u=this,r;return r=n&&i.has(n,"constructor")?n.constructor:function(){return u.apply(this,arguments)},i.extend(r,u,t),r.prototype=i.create(u.prototype,n),r.prototype.constructor=r,r.__super__=u.prototype,r},o.extend=a.extend=ft.extend=rt.extend=f.extend=et,v=function(){throw new Error('A "url" property or function must be specified');},s=function(n,t){var i=t.error;t.error=function(r){i&&i.call(t.context,n,r,t);n.trigger("error",n,r,t)}},t});
/*!
 * Bootstrap v3.2.0 (http://getbootstrap.com)
 * Copyright 2011-2014 Twitter, Inc.
 * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
 */

/*!
 * Generated using the Bootstrap Customizer (http://getbootstrap.com/customize/?id=f3d51fcade6d2dee81b9)
 * Config saved to config.json and https://gist.github.com/f3d51fcade6d2dee81b9
 */
if("undefined"==typeof jQuery)throw new Error("Bootstrap's JavaScript requires jQuery");+function(t){"use strict";function e(e){return this.each(function(){var i=t(this),o=i.data("bs.alert");o||i.data("bs.alert",o=new s(this)),"string"==typeof e&&o[e].call(i)})}var i='[data-dismiss="alert"]',s=function(e){t(e).on("click",i,this.close)};s.VERSION="3.2.0",s.prototype.close=function(e){function i(){n.detach().trigger("closed.bs.alert").remove()}var s=t(this),o=s.attr("data-target");o||(o=s.attr("href"),o=o&&o.replace(/.*(?=#[^\s]*$)/,""));var n=t(o);e&&e.preventDefault(),n.length||(n=s.hasClass("alert")?s:s.parent()),n.trigger(e=t.Event("close.bs.alert")),e.isDefaultPrevented()||(n.removeClass("in"),t.support.transition&&n.hasClass("fade")?n.one("bsTransitionEnd",i).emulateTransitionEnd(150):i())};var o=t.fn.alert;t.fn.alert=e,t.fn.alert.Constructor=s,t.fn.alert.noConflict=function(){return t.fn.alert=o,this},t(document).on("click.bs.alert.data-api",i,s.prototype.close)}(jQuery),+function(t){"use strict";function e(e){return this.each(function(){var s=t(this),o=s.data("bs.button"),n="object"==typeof e&&e;o||s.data("bs.button",o=new i(this,n)),"toggle"==e?o.toggle():e&&o.setState(e)})}var i=function(e,s){this.$element=t(e),this.options=t.extend({},i.DEFAULTS,s),this.isLoading=!1};i.VERSION="3.2.0",i.DEFAULTS={loadingText:"loading..."},i.prototype.setState=function(e){var i="disabled",s=this.$element,o=s.is("input")?"val":"html",n=s.data();e+="Text",null==n.resetText&&s.data("resetText",s[o]()),s[o](null==n[e]?this.options[e]:n[e]),setTimeout(t.proxy(function(){"loadingText"==e?(this.isLoading=!0,s.addClass(i).attr(i,i)):this.isLoading&&(this.isLoading=!1,s.removeClass(i).removeAttr(i))},this),0)},i.prototype.toggle=function(){var t=!0,e=this.$element.closest('[data-toggle="buttons"]');if(e.length){var i=this.$element.find("input");"radio"==i.prop("type")&&(i.prop("checked")&&this.$element.hasClass("active")?t=!1:e.find(".active").removeClass("active")),t&&i.prop("checked",!this.$element.hasClass("active")).trigger("change")}t&&this.$element.toggleClass("active")};var s=t.fn.button;t.fn.button=e,t.fn.button.Constructor=i,t.fn.button.noConflict=function(){return t.fn.button=s,this},t(document).on("click.bs.button.data-api",'[data-toggle^="button"]',function(i){var s=t(i.target);s.hasClass("btn")||(s=s.closest(".btn")),e.call(s,"toggle"),i.preventDefault()})}(jQuery),+function(t){"use strict";function e(e){return this.each(function(){var s=t(this),o=s.data("bs.carousel"),n=t.extend({},i.DEFAULTS,s.data(),"object"==typeof e&&e),a="string"==typeof e?e:n.slide;o||s.data("bs.carousel",o=new i(this,n)),"number"==typeof e?o.to(e):a?o[a]():n.interval&&o.pause().cycle()})}var i=function(e,i){this.$element=t(e).on("keydown.bs.carousel",t.proxy(this.keydown,this)),this.$indicators=this.$element.find(".carousel-indicators"),this.options=i,this.paused=this.sliding=this.interval=this.$active=this.$items=null,"hover"==this.options.pause&&this.$element.on("mouseenter.bs.carousel",t.proxy(this.pause,this)).on("mouseleave.bs.carousel",t.proxy(this.cycle,this))};i.VERSION="3.2.0",i.DEFAULTS={interval:5e3,pause:"hover",wrap:!0},i.prototype.keydown=function(t){switch(t.which){case 37:this.prev();break;case 39:this.next();break;default:return}t.preventDefault()},i.prototype.cycle=function(e){return e||(this.paused=!1),this.interval&&clearInterval(this.interval),this.options.interval&&!this.paused&&(this.interval=setInterval(t.proxy(this.next,this),this.options.interval)),this},i.prototype.getItemIndex=function(t){return this.$items=t.parent().children(".item"),this.$items.index(t||this.$active)},i.prototype.to=function(e){var i=this,s=this.getItemIndex(this.$active=this.$element.find(".item.active"));return e>this.$items.length-1||0>e?void 0:this.sliding?this.$element.one("slid.bs.carousel",function(){i.to(e)}):s==e?this.pause().cycle():this.slide(e>s?"next":"prev",t(this.$items[e]))},i.prototype.pause=function(e){return e||(this.paused=!0),this.$element.find(".next, .prev").length&&t.support.transition&&(this.$element.trigger(t.support.transition.end),this.cycle(!0)),this.interval=clearInterval(this.interval),this},i.prototype.next=function(){return this.sliding?void 0:this.slide("next")},i.prototype.prev=function(){return this.sliding?void 0:this.slide("prev")},i.prototype.slide=function(e,i){var s=this.$element.find(".item.active"),o=i||s[e](),n=this.interval,a="next"==e?"left":"right",r="next"==e?"first":"last",l=this;if(!o.length){if(!this.options.wrap)return;o=this.$element.find(".item")[r]()}if(o.hasClass("active"))return this.sliding=!1;var h=o[0],d=t.Event("slide.bs.carousel",{relatedTarget:h,direction:a});if(this.$element.trigger(d),!d.isDefaultPrevented()){if(this.sliding=!0,n&&this.pause(),this.$indicators.length){this.$indicators.find(".active").removeClass("active");var c=t(this.$indicators.children()[this.getItemIndex(o)]);c&&c.addClass("active")}var p=t.Event("slid.bs.carousel",{relatedTarget:h,direction:a});return t.support.transition&&this.$element.hasClass("slide")?(o.addClass(e),o[0].offsetWidth,s.addClass(a),o.addClass(a),s.one("bsTransitionEnd",function(){o.removeClass([e,a].join(" ")).addClass("active"),s.removeClass(["active",a].join(" ")),l.sliding=!1,setTimeout(function(){l.$element.trigger(p)},0)}).emulateTransitionEnd(1e3*s.css("transition-duration").slice(0,-1))):(s.removeClass("active"),o.addClass("active"),this.sliding=!1,this.$element.trigger(p)),n&&this.cycle(),this}};var s=t.fn.carousel;t.fn.carousel=e,t.fn.carousel.Constructor=i,t.fn.carousel.noConflict=function(){return t.fn.carousel=s,this},t(document).on("click.bs.carousel.data-api","[data-slide], [data-slide-to]",function(i){var s,o=t(this),n=t(o.attr("data-target")||(s=o.attr("href"))&&s.replace(/.*(?=#[^\s]+$)/,""));if(n.hasClass("carousel")){var a=t.extend({},n.data(),o.data()),r=o.attr("data-slide-to");r&&(a.interval=!1),e.call(n,a),r&&n.data("bs.carousel").to(r),i.preventDefault()}}),t(window).on("load",function(){t('[data-ride="carousel"]').each(function(){var i=t(this);e.call(i,i.data())})})}(jQuery),+function(t){"use strict";function e(e){e&&3===e.which||(t(o).remove(),t(n).each(function(){var s=i(t(this)),o={relatedTarget:this};s.hasClass("open")&&(s.trigger(e=t.Event("hide.bs.dropdown",o)),e.isDefaultPrevented()||s.removeClass("open").trigger("hidden.bs.dropdown",o))}))}function i(e){var i=e.attr("data-target");i||(i=e.attr("href"),i=i&&/#[A-Za-z]/.test(i)&&i.replace(/.*(?=#[^\s]*$)/,""));var s=i&&t(i);return s&&s.length?s:e.parent()}function s(e){return this.each(function(){var i=t(this),s=i.data("bs.dropdown");s||i.data("bs.dropdown",s=new a(this)),"string"==typeof e&&s[e].call(i)})}var o=".dropdown-backdrop",n='[data-toggle="dropdown"]',a=function(e){t(e).on("click.bs.dropdown",this.toggle)};a.VERSION="3.2.0",a.prototype.toggle=function(s){var o=t(this);if(!o.is(".disabled, :disabled")){var n=i(o),a=n.hasClass("open");if(e(),!a){"ontouchstart"in document.documentElement&&!n.closest(".navbar-nav").length&&t('<div class="dropdown-backdrop"/>').insertAfter(t(this)).on("click",e);var r={relatedTarget:this};if(n.trigger(s=t.Event("show.bs.dropdown",r)),s.isDefaultPrevented())return;o.trigger("focus"),n.toggleClass("open").trigger("shown.bs.dropdown",r)}return!1}},a.prototype.keydown=function(e){if(/(38|40|27)/.test(e.keyCode)){var s=t(this);if(e.preventDefault(),e.stopPropagation(),!s.is(".disabled, :disabled")){var o=i(s),a=o.hasClass("open");if(!a||a&&27==e.keyCode)return 27==e.which&&o.find(n).trigger("focus"),s.trigger("click");var r=" li:not(.divider):visible a",l=o.find('[role="menu"]'+r+', [role="listbox"]'+r);if(l.length){var h=l.index(l.filter(":focus"));38==e.keyCode&&h>0&&h--,40==e.keyCode&&h<l.length-1&&h++,~h||(h=0),l.eq(h).trigger("focus")}}}};var r=t.fn.dropdown;t.fn.dropdown=s,t.fn.dropdown.Constructor=a,t.fn.dropdown.noConflict=function(){return t.fn.dropdown=r,this},t(document).on("click.bs.dropdown.data-api",e).on("click.bs.dropdown.data-api",".dropdown form",function(t){t.stopPropagation()}).on("click.bs.dropdown.data-api",n,a.prototype.toggle).on("keydown.bs.dropdown.data-api",n+', [role="menu"], [role="listbox"]',a.prototype.keydown)}(jQuery),+function(t){"use strict";function e(e,s){return this.each(function(){var o=t(this),n=o.data("bs.modal"),a=t.extend({},i.DEFAULTS,o.data(),"object"==typeof e&&e);n||o.data("bs.modal",n=new i(this,a)),"string"==typeof e?n[e](s):a.show&&n.show(s)})}var i=function(e,i){this.options=i,this.$body=t(document.body),this.$element=t(e),this.$backdrop=this.isShown=null,this.scrollbarWidth=0,this.options.remote&&this.$element.find(".modal-content").load(this.options.remote,t.proxy(function(){this.$element.trigger("loaded.bs.modal")},this))};i.VERSION="3.2.0",i.DEFAULTS={backdrop:!0,keyboard:!0,show:!0},i.prototype.toggle=function(t){return this.isShown?this.hide():this.show(t)},i.prototype.show=function(e){var i=this,s=t.Event("show.bs.modal",{relatedTarget:e});this.$element.trigger(s),this.isShown||s.isDefaultPrevented()||(this.isShown=!0,this.checkScrollbar(),this.$body.addClass("modal-open"),this.setScrollbar(),this.escape(),this.$element.on("click.dismiss.bs.modal",'[data-dismiss="modal"]',t.proxy(this.hide,this)),this.backdrop(function(){var s=t.support.transition&&i.$element.hasClass("fade");i.$element.parent().length||i.$element.appendTo(i.$body),i.$element.show().scrollTop(0),s&&i.$element[0].offsetWidth,i.$element.addClass("in").attr("aria-hidden",!1),i.enforceFocus();var o=t.Event("shown.bs.modal",{relatedTarget:e});s?i.$element.find(".modal-dialog").one("bsTransitionEnd",function(){i.$element.trigger("focus").trigger(o)}).emulateTransitionEnd(300):i.$element.trigger("focus").trigger(o)}))},i.prototype.hide=function(e){e&&e.preventDefault(),e=t.Event("hide.bs.modal"),this.$element.trigger(e),this.isShown&&!e.isDefaultPrevented()&&(this.isShown=!1,this.$body.removeClass("modal-open"),this.resetScrollbar(),this.escape(),t(document).off("focusin.bs.modal"),this.$element.removeClass("in").attr("aria-hidden",!0).off("click.dismiss.bs.modal"),t.support.transition&&this.$element.hasClass("fade")?this.$element.one("bsTransitionEnd",t.proxy(this.hideModal,this)).emulateTransitionEnd(300):this.hideModal())},i.prototype.enforceFocus=function(){t(document).off("focusin.bs.modal").on("focusin.bs.modal",t.proxy(function(t){this.$element[0]===t.target||this.$element.has(t.target).length||this.$element.trigger("focus")},this))},i.prototype.escape=function(){this.isShown&&this.options.keyboard?this.$element.on("keyup.dismiss.bs.modal",t.proxy(function(t){27==t.which&&this.hide()},this)):this.isShown||this.$element.off("keyup.dismiss.bs.modal")},i.prototype.hideModal=function(){var t=this;this.$element.hide(),this.backdrop(function(){t.$element.trigger("hidden.bs.modal")})},i.prototype.removeBackdrop=function(){this.$backdrop&&this.$backdrop.remove(),this.$backdrop=null},i.prototype.backdrop=function(e){var i=this,s=this.$element.hasClass("fade")?"fade":"";if(this.isShown&&this.options.backdrop){var o=t.support.transition&&s;if(this.$backdrop=t('<div class="modal-backdrop '+s+'" />').appendTo(this.$body),this.$element.on("click.dismiss.bs.modal",t.proxy(function(t){t.target===t.currentTarget&&("static"==this.options.backdrop?this.$element[0].focus.call(this.$element[0]):this.hide.call(this))},this)),o&&this.$backdrop[0].offsetWidth,this.$backdrop.addClass("in"),!e)return;o?this.$backdrop.one("bsTransitionEnd",e).emulateTransitionEnd(150):e()}else if(!this.isShown&&this.$backdrop){this.$backdrop.removeClass("in");var n=function(){i.removeBackdrop(),e&&e()};t.support.transition&&this.$element.hasClass("fade")?this.$backdrop.one("bsTransitionEnd",n).emulateTransitionEnd(150):n()}else e&&e()},i.prototype.checkScrollbar=function(){document.body.clientWidth>=window.innerWidth||(this.scrollbarWidth=this.scrollbarWidth||this.measureScrollbar())},i.prototype.setScrollbar=function(){var t=parseInt(this.$body.css("padding-right")||0,10);this.scrollbarWidth&&this.$body.css("padding-right",t+this.scrollbarWidth)},i.prototype.resetScrollbar=function(){this.$body.css("padding-right","")},i.prototype.measureScrollbar=function(){var t=document.createElement("div");t.className="modal-scrollbar-measure",this.$body.append(t);var e=t.offsetWidth-t.clientWidth;return this.$body[0].removeChild(t),e};var s=t.fn.modal;t.fn.modal=e,t.fn.modal.Constructor=i,t.fn.modal.noConflict=function(){return t.fn.modal=s,this},t(document).on("click.bs.modal.data-api",'[data-toggle="modal"]',function(i){var s=t(this),o=s.attr("href"),n=t(s.attr("data-target")||o&&o.replace(/.*(?=#[^\s]+$)/,"")),a=n.data("bs.modal")?"toggle":t.extend({remote:!/#/.test(o)&&o},n.data(),s.data());s.is("a")&&i.preventDefault(),n.one("show.bs.modal",function(t){t.isDefaultPrevented()||n.one("hidden.bs.modal",function(){s.is(":visible")&&s.trigger("focus")})}),e.call(n,a,this)})}(jQuery),+function(t){"use strict";function e(e){return this.each(function(){var s=t(this),o=s.data("bs.tooltip"),n="object"==typeof e&&e;(o||"destroy"!=e)&&(o||s.data("bs.tooltip",o=new i(this,n)),"string"==typeof e&&o[e]())})}var i=function(t,e){this.type=this.options=this.enabled=this.timeout=this.hoverState=this.$element=null,this.init("tooltip",t,e)};i.VERSION="3.2.0",i.DEFAULTS={animation:!0,placement:"top",selector:!1,template:'<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>',trigger:"hover focus",title:"",delay:0,html:!1,container:!1,viewport:{selector:"body",padding:0}},i.prototype.init=function(e,i,s){this.enabled=!0,this.type=e,this.$element=t(i),this.options=this.getOptions(s),this.$viewport=this.options.viewport&&t(this.options.viewport.selector||this.options.viewport);for(var o=this.options.trigger.split(" "),n=o.length;n--;){var a=o[n];if("click"==a)this.$element.on("click."+this.type,this.options.selector,t.proxy(this.toggle,this));else if("manual"!=a){var r="hover"==a?"mouseenter":"focusin",l="hover"==a?"mouseleave":"focusout";this.$element.on(r+"."+this.type,this.options.selector,t.proxy(this.enter,this)),this.$element.on(l+"."+this.type,this.options.selector,t.proxy(this.leave,this))}}this.options.selector?this._options=t.extend({},this.options,{trigger:"manual",selector:""}):this.fixTitle()},i.prototype.getDefaults=function(){return i.DEFAULTS},i.prototype.getOptions=function(e){return e=t.extend({},this.getDefaults(),this.$element.data(),e),e.delay&&"number"==typeof e.delay&&(e.delay={show:e.delay,hide:e.delay}),e},i.prototype.getDelegateOptions=function(){var e={},i=this.getDefaults();return this._options&&t.each(this._options,function(t,s){i[t]!=s&&(e[t]=s)}),e},i.prototype.enter=function(e){var i=e instanceof this.constructor?e:t(e.currentTarget).data("bs."+this.type);return i||(i=new this.constructor(e.currentTarget,this.getDelegateOptions()),t(e.currentTarget).data("bs."+this.type,i)),clearTimeout(i.timeout),i.hoverState="in",i.options.delay&&i.options.delay.show?void(i.timeout=setTimeout(function(){"in"==i.hoverState&&i.show()},i.options.delay.show)):i.show()},i.prototype.leave=function(e){var i=e instanceof this.constructor?e:t(e.currentTarget).data("bs."+this.type);return i||(i=new this.constructor(e.currentTarget,this.getDelegateOptions()),t(e.currentTarget).data("bs."+this.type,i)),clearTimeout(i.timeout),i.hoverState="out",i.options.delay&&i.options.delay.hide?void(i.timeout=setTimeout(function(){"out"==i.hoverState&&i.hide()},i.options.delay.hide)):i.hide()},i.prototype.show=function(){var e=t.Event("show.bs."+this.type);if(this.hasContent()&&this.enabled){this.$element.trigger(e);var i=t.contains(document.documentElement,this.$element[0]);if(e.isDefaultPrevented()||!i)return;var s=this,o=this.tip(),n=this.getUID(this.type);this.setContent(),o.attr("id",n),this.$element.attr("aria-describedby",n),this.options.animation&&o.addClass("fade");var a="function"==typeof this.options.placement?this.options.placement.call(this,o[0],this.$element[0]):this.options.placement,r=/\s?auto?\s?/i,l=r.test(a);l&&(a=a.replace(r,"")||"top"),o.detach().css({top:0,left:0,display:"block"}).addClass(a).data("bs."+this.type,this),this.options.container?o.appendTo(this.options.container):o.insertAfter(this.$element);var h=this.getPosition(),d=o[0].offsetWidth,c=o[0].offsetHeight;if(l){var p=a,f=this.$element.parent(),u=this.getPosition(f);a="bottom"==a&&h.top+h.height+c-u.scroll>u.height?"top":"top"==a&&h.top-u.scroll-c<0?"bottom":"right"==a&&h.right+d>u.width?"left":"left"==a&&h.left-d<u.left?"right":a,o.removeClass(p).addClass(a)}var g=this.getCalculatedOffset(a,h,d,c);this.applyPlacement(g,a);var m=function(){s.$element.trigger("shown.bs."+s.type),s.hoverState=null};t.support.transition&&this.$tip.hasClass("fade")?o.one("bsTransitionEnd",m).emulateTransitionEnd(150):m()}},i.prototype.applyPlacement=function(e,i){var s=this.tip(),o=s[0].offsetWidth,n=s[0].offsetHeight,a=parseInt(s.css("margin-top"),10),r=parseInt(s.css("margin-left"),10);isNaN(a)&&(a=0),isNaN(r)&&(r=0),e.top=e.top+a,e.left=e.left+r,t.offset.setOffset(s[0],t.extend({using:function(t){s.css({top:Math.round(t.top),left:Math.round(t.left)})}},e),0),s.addClass("in");var l=s[0].offsetWidth,h=s[0].offsetHeight;"top"==i&&h!=n&&(e.top=e.top+n-h);var d=this.getViewportAdjustedDelta(i,e,l,h);d.left?e.left+=d.left:e.top+=d.top;var c=d.left?2*d.left-o+l:2*d.top-n+h,p=d.left?"left":"top",f=d.left?"offsetWidth":"offsetHeight";s.offset(e),this.replaceArrow(c,s[0][f],p)},i.prototype.replaceArrow=function(t,e,i){this.arrow().css(i,t?50*(1-t/e)+"%":"")},i.prototype.setContent=function(){var t=this.tip(),e=this.getTitle();t.find(".tooltip-inner")[this.options.html?"html":"text"](e),t.removeClass("fade in top bottom left right")},i.prototype.hide=function(){function e(){"in"!=i.hoverState&&s.detach(),i.$element.trigger("hidden.bs."+i.type)}var i=this,s=this.tip(),o=t.Event("hide.bs."+this.type);return this.$element.removeAttr("aria-describedby"),this.$element.trigger(o),o.isDefaultPrevented()?void 0:(s.removeClass("in"),t.support.transition&&this.$tip.hasClass("fade")?s.one("bsTransitionEnd",e).emulateTransitionEnd(150):e(),this.hoverState=null,this)},i.prototype.fixTitle=function(){var t=this.$element;(t.attr("title")||"string"!=typeof t.attr("data-original-title"))&&t.attr("data-original-title",t.attr("title")||"").attr("title","")},i.prototype.hasContent=function(){return this.getTitle()},i.prototype.getPosition=function(e){e=e||this.$element;var i=e[0],s="BODY"==i.tagName;return t.extend({},"function"==typeof i.getBoundingClientRect?i.getBoundingClientRect():null,{scroll:s?document.documentElement.scrollTop||document.body.scrollTop:e.scrollTop(),width:s?t(window).width():e.outerWidth(),height:s?t(window).height():e.outerHeight()},s?{top:0,left:0}:e.offset())},i.prototype.getCalculatedOffset=function(t,e,i,s){return"bottom"==t?{top:e.top+e.height,left:e.left+e.width/2-i/2}:"top"==t?{top:e.top-s,left:e.left+e.width/2-i/2}:"left"==t?{top:e.top+e.height/2-s/2,left:e.left-i}:{top:e.top+e.height/2-s/2,left:e.left+e.width}},i.prototype.getViewportAdjustedDelta=function(t,e,i,s){var o={top:0,left:0};if(!this.$viewport)return o;var n=this.options.viewport&&this.options.viewport.padding||0,a=this.getPosition(this.$viewport);if(/right|left/.test(t)){var r=e.top-n-a.scroll,l=e.top+n-a.scroll+s;r<a.top?o.top=a.top-r:l>a.top+a.height&&(o.top=a.top+a.height-l)}else{var h=e.left-n,d=e.left+n+i;h<a.left?o.left=a.left-h:d>a.width&&(o.left=a.left+a.width-d)}return o},i.prototype.getTitle=function(){var t,e=this.$element,i=this.options;return t=e.attr("data-original-title")||("function"==typeof i.title?i.title.call(e[0]):i.title)},i.prototype.getUID=function(t){do t+=~~(1e6*Math.random());while(document.getElementById(t));return t},i.prototype.tip=function(){return this.$tip=this.$tip||t(this.options.template)},i.prototype.arrow=function(){return this.$arrow=this.$arrow||this.tip().find(".tooltip-arrow")},i.prototype.validate=function(){this.$element[0].parentNode||(this.hide(),this.$element=null,this.options=null)},i.prototype.enable=function(){this.enabled=!0},i.prototype.disable=function(){this.enabled=!1},i.prototype.toggleEnabled=function(){this.enabled=!this.enabled},i.prototype.toggle=function(e){var i=this;e&&(i=t(e.currentTarget).data("bs."+this.type),i||(i=new this.constructor(e.currentTarget,this.getDelegateOptions()),t(e.currentTarget).data("bs."+this.type,i))),i.tip().hasClass("in")?i.leave(i):i.enter(i)},i.prototype.destroy=function(){clearTimeout(this.timeout),this.hide().$element.off("."+this.type).removeData("bs."+this.type)};var s=t.fn.tooltip;t.fn.tooltip=e,t.fn.tooltip.Constructor=i,t.fn.tooltip.noConflict=function(){return t.fn.tooltip=s,this}}(jQuery),+function(t){"use strict";function e(e){return this.each(function(){var s=t(this),o=s.data("bs.tab");o||s.data("bs.tab",o=new i(this)),"string"==typeof e&&o[e]()})}var i=function(e){this.element=t(e)};i.VERSION="3.2.0",i.prototype.show=function(){var e=this.element,i=e.closest("ul:not(.dropdown-menu)"),s=e.data("target");if(s||(s=e.attr("href"),s=s&&s.replace(/.*(?=#[^\s]*$)/,"")),!e.parent("li").hasClass("active")){var o=i.find(".active:last a")[0],n=t.Event("show.bs.tab",{relatedTarget:o});if(e.trigger(n),!n.isDefaultPrevented()){var a=t(s);this.activate(e.closest("li"),i),this.activate(a,a.parent(),function(){e.trigger({type:"shown.bs.tab",relatedTarget:o})})}}},i.prototype.activate=function(e,i,s){function o(){n.removeClass("active").find("> .dropdown-menu > .active").removeClass("active"),e.addClass("active"),a?(e[0].offsetWidth,e.addClass("in")):e.removeClass("fade"),e.parent(".dropdown-menu")&&e.closest("li.dropdown").addClass("active"),s&&s()}var n=i.find("> .active"),a=s&&t.support.transition&&n.hasClass("fade");a?n.one("bsTransitionEnd",o).emulateTransitionEnd(150):o(),n.removeClass("in")};var s=t.fn.tab;t.fn.tab=e,t.fn.tab.Constructor=i,t.fn.tab.noConflict=function(){return t.fn.tab=s,this},t(document).on("click.bs.tab.data-api",'[data-toggle="tab"], [data-toggle="pill"]',function(i){i.preventDefault(),e.call(t(this),"show")})}(jQuery),+function(t){"use strict";function e(e){return this.each(function(){var s=t(this),o=s.data("bs.affix"),n="object"==typeof e&&e;o||s.data("bs.affix",o=new i(this,n)),"string"==typeof e&&o[e]()})}var i=function(e,s){this.options=t.extend({},i.DEFAULTS,s),this.$target=t(this.options.target).on("scroll.bs.affix.data-api",t.proxy(this.checkPosition,this)).on("click.bs.affix.data-api",t.proxy(this.checkPositionWithEventLoop,this)),this.$element=t(e),this.affixed=this.unpin=this.pinnedOffset=null,this.checkPosition()};i.VERSION="3.2.0",i.RESET="affix affix-top affix-bottom",i.DEFAULTS={offset:0,target:window},i.prototype.getPinnedOffset=function(){if(this.pinnedOffset)return this.pinnedOffset;this.$element.removeClass(i.RESET).addClass("affix");var t=this.$target.scrollTop(),e=this.$element.offset();return this.pinnedOffset=e.top-t},i.prototype.checkPositionWithEventLoop=function(){setTimeout(t.proxy(this.checkPosition,this),1)},i.prototype.checkPosition=function(){if(this.$element.is(":visible")){var e=t(document).height(),s=this.$target.scrollTop(),o=this.$element.offset(),n=this.options.offset,a=n.top,r=n.bottom;"object"!=typeof n&&(r=a=n),"function"==typeof a&&(a=n.top(this.$element)),"function"==typeof r&&(r=n.bottom(this.$element));var l=null!=this.unpin&&s+this.unpin<=o.top?!1:null!=r&&o.top+this.$element.height()>=e-r?"bottom":null!=a&&a>=s?"top":!1;if(this.affixed!==l){null!=this.unpin&&this.$element.css("top","");var h="affix"+(l?"-"+l:""),d=t.Event(h+".bs.affix");this.$element.trigger(d),d.isDefaultPrevented()||(this.affixed=l,this.unpin="bottom"==l?this.getPinnedOffset():null,this.$element.removeClass(i.RESET).addClass(h).trigger(t.Event(h.replace("affix","affixed"))),"bottom"==l&&this.$element.offset({top:e-this.$element.height()-r}))}}};var s=t.fn.affix;t.fn.affix=e,t.fn.affix.Constructor=i,t.fn.affix.noConflict=function(){return t.fn.affix=s,this},t(window).on("load",function(){t('[data-spy="affix"]').each(function(){var i=t(this),s=i.data();s.offset=s.offset||{},s.offsetBottom&&(s.offset.bottom=s.offsetBottom),s.offsetTop&&(s.offset.top=s.offsetTop),e.call(i,s)})})}(jQuery),+function(t){"use strict";function e(e){return this.each(function(){var s=t(this),o=s.data("bs.collapse"),n=t.extend({},i.DEFAULTS,s.data(),"object"==typeof e&&e);!o&&n.toggle&&"show"==e&&(e=!e),o||s.data("bs.collapse",o=new i(this,n)),"string"==typeof e&&o[e]()})}var i=function(e,s){this.$element=t(e),this.options=t.extend({},i.DEFAULTS,s),this.transitioning=null,this.options.parent&&(this.$parent=t(this.options.parent)),this.options.toggle&&this.toggle()};i.VERSION="3.2.0",i.DEFAULTS={toggle:!0},i.prototype.dimension=function(){var t=this.$element.hasClass("width");return t?"width":"height"},i.prototype.show=function(){if(!this.transitioning&&!this.$element.hasClass("in")){var i=t.Event("show.bs.collapse");if(this.$element.trigger(i),!i.isDefaultPrevented()){var s=this.$parent&&this.$parent.find("> .panel > .in");if(s&&s.length){var o=s.data("bs.collapse");if(o&&o.transitioning)return;e.call(s,"hide"),o||s.data("bs.collapse",null)}var n=this.dimension();this.$element.removeClass("collapse").addClass("collapsing")[n](0),this.transitioning=1;var a=function(){this.$element.removeClass("collapsing").addClass("collapse in")[n](""),this.transitioning=0,this.$element.trigger("shown.bs.collapse")};if(!t.support.transition)return a.call(this);var r=t.camelCase(["scroll",n].join("-"));this.$element.one("bsTransitionEnd",t.proxy(a,this)).emulateTransitionEnd(350)[n](this.$element[0][r])}}},i.prototype.hide=function(){if(!this.transitioning&&this.$element.hasClass("in")){var e=t.Event("hide.bs.collapse");if(this.$element.trigger(e),!e.isDefaultPrevented()){var i=this.dimension();this.$element[i](this.$element[i]())[0].offsetHeight,this.$element.addClass("collapsing").removeClass("collapse").removeClass("in"),this.transitioning=1;var s=function(){this.transitioning=0,this.$element.trigger("hidden.bs.collapse").removeClass("collapsing").addClass("collapse")};return t.support.transition?void this.$element[i](0).one("bsTransitionEnd",t.proxy(s,this)).emulateTransitionEnd(350):s.call(this)}}},i.prototype.toggle=function(){this[this.$element.hasClass("in")?"hide":"show"]()};var s=t.fn.collapse;t.fn.collapse=e,t.fn.collapse.Constructor=i,t.fn.collapse.noConflict=function(){return t.fn.collapse=s,this},t(document).on("click.bs.collapse.data-api",'[data-toggle="collapse"]',function(i){var s,o=t(this),n=o.attr("data-target")||i.preventDefault()||(s=o.attr("href"))&&s.replace(/.*(?=#[^\s]+$)/,""),a=t(n),r=a.data("bs.collapse"),l=r?"toggle":o.data(),h=o.attr("data-parent"),d=h&&t(h);r&&r.transitioning||(d&&d.find('[data-toggle="collapse"][data-parent="'+h+'"]').not(o).addClass("collapsed"),o[a.hasClass("in")?"addClass":"removeClass"]("collapsed")),e.call(a,l)})}(jQuery),+function(t){"use strict";function e(i,s){var o=t.proxy(this.process,this);this.$body=t("body"),this.$scrollElement=t(t(i).is("body")?window:i),this.options=t.extend({},e.DEFAULTS,s),this.selector=(this.options.target||"")+" .nav li > a",this.offsets=[],this.targets=[],this.activeTarget=null,this.scrollHeight=0,this.$scrollElement.on("scroll.bs.scrollspy",o),this.refresh(),this.process()}function i(i){return this.each(function(){var s=t(this),o=s.data("bs.scrollspy"),n="object"==typeof i&&i;o||s.data("bs.scrollspy",o=new e(this,n)),"string"==typeof i&&o[i]()})}e.VERSION="3.2.0",e.DEFAULTS={offset:10},e.prototype.getScrollHeight=function(){return this.$scrollElement[0].scrollHeight||Math.max(this.$body[0].scrollHeight,document.documentElement.scrollHeight)},e.prototype.refresh=function(){var e="offset",i=0;t.isWindow(this.$scrollElement[0])||(e="position",i=this.$scrollElement.scrollTop()),this.offsets=[],this.targets=[],this.scrollHeight=this.getScrollHeight();var s=this;this.$body.find(this.selector).map(function(){var s=t(this),o=s.data("target")||s.attr("href"),n=/^#./.test(o)&&t(o);return n&&n.length&&n.is(":visible")&&[[n[e]().top+i,o]]||null}).sort(function(t,e){return t[0]-e[0]}).each(function(){s.offsets.push(this[0]),s.targets.push(this[1])})},e.prototype.process=function(){var t,e=this.$scrollElement.scrollTop()+this.options.offset,i=this.getScrollHeight(),s=this.options.offset+i-this.$scrollElement.height(),o=this.offsets,n=this.targets,a=this.activeTarget;if(this.scrollHeight!=i&&this.refresh(),e>=s)return a!=(t=n[n.length-1])&&this.activate(t);if(a&&e<=o[0])return a!=(t=n[0])&&this.activate(t);for(t=o.length;t--;)a!=n[t]&&e>=o[t]&&(!o[t+1]||e<=o[t+1])&&this.activate(n[t])},e.prototype.activate=function(e){this.activeTarget=e,t(this.selector).parentsUntil(this.options.target,".active").removeClass("active");var i=this.selector+'[data-target="'+e+'"],'+this.selector+'[href="'+e+'"]',s=t(i).parents("li").addClass("active");s.parent(".dropdown-menu").length&&(s=s.closest("li.dropdown").addClass("active")),s.trigger("activate.bs.scrollspy")};var s=t.fn.scrollspy;t.fn.scrollspy=i,t.fn.scrollspy.Constructor=e,t.fn.scrollspy.noConflict=function(){return t.fn.scrollspy=s,this},t(window).on("load.bs.scrollspy.data-api",function(){t('[data-spy="scroll"]').each(function(){var e=t(this);i.call(e,e.data())})})}(jQuery),+function(t){"use strict";function e(){var t=document.createElement("bootstrap"),e={WebkitTransition:"webkitTransitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd otransitionend",transition:"transitionend"};for(var i in e)if(void 0!==t.style[i])return{end:e[i]};return!1}t.fn.emulateTransitionEnd=function(e){var i=!1,s=this;t(this).one("bsTransitionEnd",function(){i=!0});var o=function(){i||t(s).trigger(t.support.transition.end)};return setTimeout(o,e),this},t(function(){t.support.transition=e(),t.support.transition&&(t.event.special.bsTransitionEnd={bindType:t.support.transition.end,delegateType:t.support.transition.end,handle:function(e){return t(e.target).is(this)?e.handleObj.handler.apply(this,arguments):void 0}})})}(jQuery);
/*! offline-js 0.7.14 */
(function () {
    var Offline, checkXHR, defaultOptions, extendNative, grab, handlers, init;
    extendNative = function (to, from) {
        var e, key, results, val;
        results = [];
        for (key in from.prototype) try {
            val = from.prototype[key], null == to[key] && "function" != typeof val ? results.push(to[key] = val) : results.push(void 0);
        } catch (_error) {
            e = _error;
        }
        return results;
    },

    Offline = {},

    Offline.options = window.Offline ? window.Offline.options || {} : {},

    defaultOptions = {
        checks: {
            xhr: {
                url: function () {
                    return "/Home/CheckConnection";
                },
                timeout: 5e3,
                type: "GET"
            },
            image: {
                url: function () {
                    return "/favicon.ico?_=" + new Date().getTime();
                }
            },
            active: "xhr"
        },
        checkOnLoad: !1,
        interceptRequests: !0,
        reconnect: !0,
        deDupBody: !1
    },

    grab = function (obj, key) {
        var cur, i, j, len, part, parts;
        for (cur = obj, parts = key.split("."), i = j = 0, len = parts.length; len > j && (part = parts[i],
        cur = cur[part], "object" == typeof cur) ; i = ++j);
        return i === parts.length - 1 ? cur : void 0;
    },

    Offline.getOption = function (key) {
        var ref, val;
        return val = null != (ref = grab(Offline.options, key)) ? ref : grab(defaultOptions, key),
        "function" == typeof val ? val() : val;
    },

    "function" == typeof window.addEventListener && window.addEventListener("online", function () {
        return setTimeout(Offline.confirmUp, 100);
    }, !1),

    "function" == typeof window.addEventListener && window.addEventListener("offline", function () {
        return Offline.confirmDown();
    }, !1),

    Offline.state = "up",

    Offline.markUp = function () {
        return Offline.trigger("confirmed-up"), "up" !== Offline.state ? (Offline.state = "up",
        Offline.trigger("up")) : void 0;
    },

    Offline.markDown = function () {
        return Offline.trigger("confirmed-down"), "down" !== Offline.state ? (Offline.state = "down",
        Offline.trigger("down")) : void 0;
    },

    handlers = {},

    Offline.on = function (event, handler, ctx) {
        var e, events, j, len, results;
        if (events = event.split(" "), events.length > 1) {
            for (results = [], j = 0, len = events.length; len > j; j++) e = events[j], results.push(Offline.on(e, handler, ctx));
            return results;
        }
        return null == handlers[event] && (handlers[event] = []), handlers[event].push([ctx, handler]);
    },

    Offline.off = function (event, handler) {
        var _handler, ctx, i, ref, results;
        if (null != handlers[event]) {
            if (handler) {
                for (i = 0, results = []; i < handlers[event].length;) ref = handlers[event][i],
                ctx = ref[0], _handler = ref[1], _handler === handler ? results.push(handlers[event].splice(i, 1)) : results.push(i++);
                return results;
            }
            return handlers[event] = [];
        }
    },

    Offline.trigger = function (event) {
        var ctx, handler, j, len, ref, ref1, results;
        if (null != handlers[event]) {
            for (ref = handlers[event], results = [], j = 0, len = ref.length; len > j; j++) ref1 = ref[j],
            ctx = ref1[0], handler = ref1[1], results.push(handler.call(ctx));
            return results;
        }
    },

    checkXHR = function (xhr, onUp, onDown) {
        var _onerror, _onload, _onreadystatechange, _ontimeout, checkStatus;
        return checkStatus = function () {
            return xhr.status && xhr.status < 12e3 ? onUp() : onDown();
        }, null === xhr.onprogress ? (_onerror = xhr.onerror, xhr.onerror = function () {
            return onDown(), "function" == typeof _onerror ? _onerror.apply(null, arguments) : void 0;
        }, _ontimeout = xhr.ontimeout, xhr.ontimeout = function () {
            return onDown(), "function" == typeof _ontimeout ? _ontimeout.apply(null, arguments) : void 0;
        }, _onload = xhr.onload, xhr.onload = function () {
            return checkStatus(), "function" == typeof _onload ? _onload.apply(null, arguments) : void 0;
        }) : (_onreadystatechange = xhr.onreadystatechange, xhr.onreadystatechange = function () {
            return 4 === xhr.readyState ? checkStatus() : 0 === xhr.readyState && onDown(), "function" == typeof _onreadystatechange ? _onreadystatechange.apply(null, arguments) : void 0;
        });
    },

    Offline.checks = {},

    Offline.checks.xhr = function () {
        var e, xhr;
        xhr = new XMLHttpRequest(),
        xhr.offline = !1,
        xhr.open(Offline.getOption("checks.xhr.type"),
         Offline.getOption("checks.xhr.url"), !0),
        null != xhr.timeout && (xhr.timeout = Offline.getOption("checks.xhr.timeout")),
        checkXHR(xhr, Offline.markUp, Offline.markDown);
        try {
            xhr.send();
        } catch (_error) {
            e = _error,
            Offline.markDown();
        }
        return xhr;
    },

    Offline.checks.image = function () {
        var img;
        return img = document.createElement("img"), img.onerror = Offline.markDown, img.onload = Offline.markUp,
        void (img.src = Offline.getOption("checks.image.url"));
    },

    Offline.checks.down = Offline.markDown, Offline.checks.up = Offline.markUp, Offline.check = function () {
        return Offline.trigger("checking"), Offline.checks[Offline.getOption("checks.active")]();
    },

    Offline.confirmUp = Offline.confirmDown = Offline.check,

    Offline.onXHR = function (cb) {
        var _XDomainRequest, _XMLHttpRequest, monitorXHR;
        return monitorXHR = function (req, flags) {
            var _open;
            return _open = req.open, req.open = function (type, url, async, user, password) {
                return cb({
                    type: type,
                    url: url,
                    async: async,
                    flags: flags,
                    user: user,
                    password: password,
                    xhr: req
                }), _open.apply(req, arguments);
            };
        }, _XMLHttpRequest = window.XMLHttpRequest, window.XMLHttpRequest = function (flags) {
            var _overrideMimeType, _setRequestHeader, req;
            return req = new _XMLHttpRequest(flags), monitorXHR(req, flags), _setRequestHeader = req.setRequestHeader,
            req.headers = {}, req.setRequestHeader = function (name, value) {
                return req.headers[name] = value, _setRequestHeader.call(req, name, value);
            }, _overrideMimeType = req.overrideMimeType, req.overrideMimeType = function (type) {
                return req.mimeType = type, _overrideMimeType.call(req, type);
            }, req;
        }, extendNative(window.XMLHttpRequest, _XMLHttpRequest), null != window.XDomainRequest ? (_XDomainRequest = window.XDomainRequest,
        window.XDomainRequest = function () {
            var req;
            return req = new _XDomainRequest(), monitorXHR(req), req;
        }, extendNative(window.XDomainRequest, _XDomainRequest)) : void 0;
    },

    init = function () {
        return Offline.getOption("interceptRequests") && Offline.onXHR(function (arg) {
            var xhr;
            return xhr = arg.xhr, xhr.offline !== !1 ? checkXHR(xhr, Offline.markUp, Offline.confirmDown) : void 0;
        }), Offline.getOption("checkOnLoad") ? Offline.check() : void 0;
    },

    setTimeout(init, 0),

    window.Offline = Offline;
}).call(this), function () {
    var down, next, nope, rc, reset, retryIntv, tick, tryNow, up;

    if (!window.Offline) throw new Error("Offline Reconnect brought in without offline.js");

    rc = Offline.reconnect = {},

    retryIntv = null,

    reset = function () {
        var ref;
        return null != rc.state && "inactive" !== rc.state && Offline.trigger("reconnect:stopped"),
        rc.state = "inactive", rc.remaining = rc.delay = null != (ref = Offline.getOption("reconnect.initialDelay")) ? ref : 15;
    },

    next = function () {
        var delay, ref;
        return delay = null != (ref = Offline.getOption("reconnect.delay")) ? ref : Math.min(Math.ceil(1.5 * rc.delay), 3600),
        rc.remaining = rc.delay = delay;
    },

    tick = function () {
        return "connecting" !== rc.state ? (rc.remaining -= 1, Offline.trigger("reconnect:tick"),
        0 === rc.remaining ? tryNow() : void 0) : void 0;
    },

    tryNow = function () {
        return "waiting" === rc.state ? (Offline.trigger("reconnect:connecting"), rc.state = "connecting",
        Offline.check()) : void 0;
    },

    down = function () {
        return Offline.getOption("reconnect") ? (reset(), rc.state = "waiting", Offline.trigger("reconnect:started"),
        retryIntv = setInterval(tick, 1e3)) : void 0;
    },

    up = function () {
        return "inactive" === rc.state && Offline.trigger("reconnect:success"),
            null != retryIntv && clearInterval(retryIntv), reset();
    },

    nope = function () {
        return Offline.getOption("reconnect") && "connecting" === rc.state ? (Offline.trigger("reconnect:failure"),
        rc.state = "waiting", next()) : void 0;
    },

    rc.tryNow = tryNow,
    reset(),

    Offline.on("down", down),

    Offline.on("confirmed-down", nope),

    Offline.on("up", up);

}.call(this), function () {
    var clear, flush, held, holdRequest, makeRequest, waitingOnConfirm;
    if (!window.Offline) throw new Error("Requests module brought in without offline.js");

    held = [],
    waitingOnConfirm = !1,
    holdRequest = function (req) {
        return Offline.getOption("requests") !== !1 ? (Offline.trigger("requests:capture"),
        "down" !== Offline.state && (waitingOnConfirm = !0), held.push(req)) : void 0;
    },

    makeRequest = function (arg) {
        var body, name, password, ref, type, url, user, val, xhr;
        if (xhr = arg.xhr, url = arg.url, type = arg.type, user = arg.user, password = arg.password,
        body = arg.body, Offline.getOption("requests") !== !1) {
            xhr.abort(), xhr.open(type, url, !0, user, password), ref = xhr.headers;
            for (name in ref) val = ref[name], xhr.setRequestHeader(name, val);
            return xhr.mimeType && xhr.overrideMimeType(xhr.mimeType), xhr.send(body);
        }
    },

    clear = function () {
        return held = [];
    },

    flush = function () {
        var body, i, key, len, request, requests, url;
        if (Offline.getOption("requests") !== !1) {
            for (Offline.trigger("requests:flush"), requests = {}, i = 0, len = held.length; len > i; i++) request = held[i],
            url = request.url.replace(/(\?|&)_=[0-9]+/, function (match, char) {
                return "?" === char ? char : "";
            }), Offline.getOption("deDupBody") ? (body = request.body, body = "[object Object]" === body.toString() ? JSON.stringify(body) : body.toString(),
            requests[request.type.toUpperCase() + " - " + url + " - " + body] = request) : requests[request.type.toUpperCase() + " - " + url] = request;
            for (key in requests) request = requests[key], makeRequest(request);
            return clear();
        }
    },

    setTimeout(function () {
        return Offline.getOption("requests") !== !1 ? (Offline.on("confirmed-up", function () {
            return waitingOnConfirm ? (waitingOnConfirm = !1, clear()) : void 0;
        }), Offline.on("up", flush), Offline.on("down", function () {
            return waitingOnConfirm = !1;
        }), Offline.onXHR(function (request) {
            var _onreadystatechange, _send, async, hold, xhr;
            return xhr = request.xhr, async = request.async, xhr.offline !== !1 && (hold = function () {
                return holdRequest(request);
            }, _send = xhr.send, xhr.send = function (body) {
                return request.body = body, _send.apply(xhr, arguments);
            }, async) ? null === xhr.onprogress ? (xhr.addEventListener("error", hold, !1),
            xhr.addEventListener("timeout", hold, !1)) : (_onreadystatechange = xhr.onreadystatechange,
            xhr.onreadystatechange = function () {
                return 0 === xhr.readyState ? hold() : 4 === xhr.readyState && (0 === xhr.status || xhr.status >= 12e3) && hold(),
                "function" == typeof _onreadystatechange ? _onreadystatechange.apply(null, arguments) : void 0;
            }) : void 0;
        }), Offline.requests = {
            flush: flush,
            clear: clear
        }) : void 0;
    }, 0);

}.call(this), function () {
    var base, i, len, ref, state;
    if (!Offline) throw new Error("Offline simulate brought in without offline.js");

    for (ref = ["up", "down"], i = 0, len = ref.length; len > i; i++)
        state = ref[i],
    (document.querySelector("script[data-simulate='" + state + "']") || ("undefined" != typeof localStorage && null !== localStorage ? localStorage.OFFLINE_SIMULATE : void 0) === state) && (null == Offline.options && (Offline.options = {}),
    null == (base = Offline.options).checks && (base.checks = {}), Offline.options.checks.active = state);

}.call(this), function () {
    var RETRY_TEMPLATE, TEMPLATE, _onreadystatechange, addClass, content, createFromHTML, el, flashClass, flashTimeouts, init, removeClass, render, roundTime;
    if (!window.Offline) throw new Error("Offline UI brought in without offline.js");
    TEMPLATE = '<div class="offline-ui"><div class="offline-ui-content"></div></div>',
    RETRY_TEMPLATE = '<a href class="offline-ui-retry"></a>', createFromHTML = function (html) {
        var el;
        return el = document.createElement("div"), el.innerHTML = html, el.children[0];
    },

    el = content = null,

    addClass = function (name) {
        return removeClass(name), el.className += " " + name;
    },

    removeClass = function (name) {
        return el.className = el.className.replace(new RegExp("(^| )" + name.split(" ").join("|") + "( |$)", "gi"), " ");
    },

    flashTimeouts = {},

    flashClass = function (name, time) {
        return addClass(name), null != flashTimeouts[name] && clearTimeout(flashTimeouts[name]),
        flashTimeouts[name] = setTimeout(function () {
            return removeClass(name), delete flashTimeouts[name];
        }, 1e3 * time);
    },

    roundTime = function (sec) {
        var mult, unit, units, val;
        units = {
            day: 86400,
            hour: 3600,
            minute: 60,
            second: 1
        };
        for (unit in units)
            if (mult = units[unit], sec >= mult)
                return val = Math.floor(sec / mult),
        [val, unit];
        return ["now", ""];
    },

    render = function () {
        var button, handler;
        return el = createFromHTML(TEMPLATE), document.body.appendChild(el), null != Offline.reconnect && Offline.getOption("reconnect") && (el.appendChild(createFromHTML(RETRY_TEMPLATE)),
        button = el.querySelector(".offline-ui-retry"), handler = function (e) {
            return e.preventDefault(), Offline.reconnect.tryNow();
        }, null != button.addEventListener ? button.addEventListener("click", handler, !1) : button.attachEvent("click", handler)),
        addClass("offline-ui-" + Offline.state), content = el.querySelector(".offline-ui-content");
    },

    init = function () {
        return render(),
            Offline.on("up", function () {
                return removeClass("offline-ui-down"), addClass("offline-ui-up"),
                    flashClass("offline-ui-up-2s", 2),
                    flashClass("offline-ui-up-5s", 5);
            }),

            Offline.on("down", function () {
                return removeClass("offline-ui-up"),
                    addClass("offline-ui-down"),
                    flashClass("offline-ui-down-2s", 2),
                    flashClass("offline-ui-down-5s", 5);
            }),

            Offline.on("reconnect:connecting", function () {
                return addClass("offline-ui-connecting"), removeClass("offline-ui-waiting");
            }),

            Offline.on("reconnect:tick", function () {
                var ref, time, unit;
                return addClass("offline-ui-waiting"),
                    removeClass("offline-ui-connecting"),
                    ref = roundTime(Offline.reconnect.remaining),
                    time = ref[0],
                    unit = ref[1],
                    content.setAttribute("data-retry-in-value", time),
                    content.setAttribute("data-retry-in-unit", unit);

            }), Offline.on("reconnect:stopped", function () {
                return removeClass("offline-ui-connecting offline-ui-waiting"),
                    content.setAttribute("data-retry-in-value", null),
                    content.setAttribute("data-retry-in-unit", null);
            }),

            Offline.on("reconnect:failure", function () {
                return flashClass("offline-ui-reconnect-failed-2s", 2),
                    flashClass("offline-ui-reconnect-failed-5s", 5);
            }),

            Offline.on("reconnect:success", function () {
                return flashClass("offline-ui-reconnect-succeeded-2s", 2),
                    flashClass("offline-ui-reconnect-succeeded-5s", 5);
            });
    },

    "complete" === document.readyState
            ? init()
            : null != document.addEventListener
                ? document.addEventListener("DOMContentLoaded", init, !1) : (_onreadystatechange = document.onreadystatechange,
                document.onreadystatechange = function () {
                    return "complete" === document.readyState && init(),
                        "function" == typeof _onreadystatechange ? _onreadystatechange.apply(null, arguments) : void 0;
                });
}.call(this);

/* NUGET: BEGIN LICENSE TEXT
 *
 * Microsoft grants you the right to use these script files for the sole
 * purpose of either: (i) interacting through your browser with the Microsoft
 * website or online service, subject to the applicable licensing or use
 * terms; or (ii) using the files as included with a Microsoft product subject
 * to that product's license terms. Microsoft reserves all other rights to the
 * files not expressly granted by Microsoft, whether by implication, estoppel
 * or otherwise. Insofar as a script file is dual licensed under GPL,
 * Microsoft neither took the code under GPL nor distributes it thereunder but
 * under the terms set out in this paragraph. All notices and licenses
 * below are for informational purposes only.
 *
 * NUGET: END LICENSE TEXT */
/*
** Unobtrusive Ajax support library for jQuery
** Copyright (C) Microsoft Corporation. All rights reserved.
*/
(function(a){var b="unobtrusiveAjaxClick",d="unobtrusiveAjaxClickTarget",h="unobtrusiveValidation";function c(d,b){var a=window,c=(d||"").split(".");while(a&&c.length)a=a[c.shift()];if(typeof a==="function")return a;b.push(d);return Function.constructor.apply(null,b)}function e(a){return a==="GET"||a==="POST"}function g(b,a){!e(a)&&b.setRequestHeader("X-HTTP-Method-Override",a)}function i(c,b,e){var d;if(e.indexOf("application/x-javascript")!==-1)return;d=(c.getAttribute("data-ajax-mode")||"").toUpperCase();a(c.getAttribute("data-ajax-update")).each(function(f,c){var e;switch(d){case"BEFORE":e=c.firstChild;a("<div />").html(b).contents().each(function(){c.insertBefore(this,e)});break;case"AFTER":a("<div />").html(b).contents().each(function(){c.appendChild(this)});break;case"REPLACE-WITH":a(c).replaceWith(b);break;default:a(c).html(b)}})}function f(b,d){var j,k,f,h;j=b.getAttribute("data-ajax-confirm");if(j&&!window.confirm(j))return;k=a(b.getAttribute("data-ajax-loading"));h=parseInt(b.getAttribute("data-ajax-loading-duration"),10)||0;a.extend(d,{type:b.getAttribute("data-ajax-method")||undefined,url:b.getAttribute("data-ajax-url")||undefined,cache:!!b.getAttribute("data-ajax-cache"),beforeSend:function(d){var a;g(d,f);a=c(b.getAttribute("data-ajax-begin"),["xhr"]).apply(b,arguments);a!==false&&k.show(h);return a},complete:function(){k.hide(h);c(b.getAttribute("data-ajax-complete"),["xhr","status"]).apply(b,arguments)},success:function(a,e,d){i(b,a,d.getResponseHeader("Content-Type")||"text/html");c(b.getAttribute("data-ajax-success"),["data","status","xhr"]).apply(b,arguments)},error:function(){c(b.getAttribute("data-ajax-failure"),["xhr","status","error"]).apply(b,arguments)}});d.data.push({name:"X-Requested-With",value:"XMLHttpRequest"});f=d.type.toUpperCase();if(!e(f)){d.type="POST";d.data.push({name:"X-HTTP-Method-Override",value:f})}a.ajax(d)}function j(c){var b=a(c).data(h);return!b||!b.validate||b.validate()}a(document).on("click","a[data-ajax=true]",function(a){a.preventDefault();f(this,{url:this.href,type:"GET",data:[]})});a(document).on("click","form[data-ajax=true] input[type=image]",function(c){var g=c.target.name,e=a(c.target),f=a(e.parents("form")[0]),d=e.offset();f.data(b,[{name:g+".x",value:Math.round(c.pageX-d.left)},{name:g+".y",value:Math.round(c.pageY-d.top)}]);setTimeout(function(){f.removeData(b)},0)});a(document).on("click","form[data-ajax=true] :submit",function(e){var g=e.currentTarget.name,f=a(e.target),c=a(f.parents("form")[0]);c.data(b,g?[{name:g,value:e.currentTarget.value}]:[]);c.data(d,f);setTimeout(function(){c.removeData(b);c.removeData(d)},0)});a(document).on("submit","form[data-ajax=true]",function(h){var e=a(this).data(b)||[],c=a(this).data(d),g=c&&c.hasClass("cancel");h.preventDefault();if(!g&&!j(this))return;f(this,{url:this.action,type:this.method||"GET",data:e.concat(a(this).serializeArray())})})})(jQuery);
/* Modernizr 2.8.3 (Custom Build) | MIT & BSD
 * Build: http://modernizr.com/download/#-fontface-backgroundsize-borderimage-borderradius-boxshadow-flexbox-flexboxlegacy-hsla-multiplebgs-opacity-rgba-textshadow-cssanimations-csscolumns-generatedcontent-cssgradients-cssreflections-csstransforms-csstransforms3d-csstransitions-applicationcache-canvas-canvastext-draganddrop-hashchange-history-audio-video-indexeddb-input-inputtypes-localstorage-postmessage-sessionstorage-websockets-websqldatabase-webworkers-shiv-cssclasses-teststyles-testprop-testallprops-hasevent-prefixes-domprefixes-load
 */
;window.Modernizr=function(a,b,c){function B(a){j.cssText=a}function C(a,b){return B(n.join(a+";")+(b||""))}function D(a,b){return typeof a===b}function E(a,b){return!!~(""+a).indexOf(b)}function F(a,b){for(var d in a){var e=a[d];if(!E(e,"-")&&j[e]!==c)return b=="pfx"?e:!0}return!1}function G(a,b,d){for(var e in a){var f=b[a[e]];if(f!==c)return d===!1?a[e]:D(f,"function")?f.bind(d||b):f}return!1}function H(a,b,c){var d=a.charAt(0).toUpperCase()+a.slice(1),e=(a+" "+p.join(d+" ")+d).split(" ");return D(b,"string")||D(b,"undefined")?F(e,b):(e=(a+" "+q.join(d+" ")+d).split(" "),G(e,b,c))}function I(){e.input=function(c){for(var d=0,e=c.length;d<e;d++)t[c[d]]=c[d]in k;return t.list&&(t.list=!!b.createElement("datalist")&&!!a.HTMLDataListElement),t}("autocomplete autofocus list placeholder max min multiple pattern required step".split(" ")),e.inputtypes=function(a){for(var d=0,e,f,h,i=a.length;d<i;d++)k.setAttribute("type",f=a[d]),e=k.type!=="text",e&&(k.value=l,k.style.cssText="position:absolute;visibility:hidden;",/^range$/.test(f)&&k.style.WebkitAppearance!==c?(g.appendChild(k),h=b.defaultView,e=h.getComputedStyle&&h.getComputedStyle(k,null).WebkitAppearance!=="textfield"&&k.offsetHeight!==0,g.removeChild(k)):/^(search|tel)$/.test(f)||(/^(url|email)$/.test(f)?e=k.checkValidity&&k.checkValidity()===!1:e=k.value!=l)),s[a[d]]=!!e;return s}("search tel url email datetime date month week time datetime-local number range color".split(" "))}var d="2.8.3",e={},f=!0,g=b.documentElement,h="modernizr",i=b.createElement(h),j=i.style,k=b.createElement("input"),l=":)",m={}.toString,n=" -webkit- -moz- -o- -ms- ".split(" "),o="Webkit Moz O ms",p=o.split(" "),q=o.toLowerCase().split(" "),r={},s={},t={},u=[],v=u.slice,w,x=function(a,c,d,e){var f,i,j,k,l=b.createElement("div"),m=b.body,n=m||b.createElement("body");if(parseInt(d,10))while(d--)j=b.createElement("div"),j.id=e?e[d]:h+(d+1),l.appendChild(j);return f=["&#173;",'<style id="s',h,'">',a,"</style>"].join(""),l.id=h,(m?l:n).innerHTML+=f,n.appendChild(l),m||(n.style.background="",n.style.overflow="hidden",k=g.style.overflow,g.style.overflow="hidden",g.appendChild(n)),i=c(l,a),m?l.parentNode.removeChild(l):(n.parentNode.removeChild(n),g.style.overflow=k),!!i},y=function(){function d(d,e){e=e||b.createElement(a[d]||"div"),d="on"+d;var f=d in e;return f||(e.setAttribute||(e=b.createElement("div")),e.setAttribute&&e.removeAttribute&&(e.setAttribute(d,""),f=D(e[d],"function"),D(e[d],"undefined")||(e[d]=c),e.removeAttribute(d))),e=null,f}var a={select:"input",change:"input",submit:"form",reset:"form",error:"img",load:"img",abort:"img"};return d}(),z={}.hasOwnProperty,A;!D(z,"undefined")&&!D(z.call,"undefined")?A=function(a,b){return z.call(a,b)}:A=function(a,b){return b in a&&D(a.constructor.prototype[b],"undefined")},Function.prototype.bind||(Function.prototype.bind=function(b){var c=this;if(typeof c!="function")throw new TypeError;var d=v.call(arguments,1),e=function(){if(this instanceof e){var a=function(){};a.prototype=c.prototype;var f=new a,g=c.apply(f,d.concat(v.call(arguments)));return Object(g)===g?g:f}return c.apply(b,d.concat(v.call(arguments)))};return e}),r.flexbox=function(){return H("flexWrap")},r.flexboxlegacy=function(){return H("boxDirection")},r.canvas=function(){var a=b.createElement("canvas");return!!a.getContext&&!!a.getContext("2d")},r.canvastext=function(){return!!e.canvas&&!!D(b.createElement("canvas").getContext("2d").fillText,"function")},r.postmessage=function(){return!!a.postMessage},r.websqldatabase=function(){return!!a.openDatabase},r.indexedDB=function(){return!!H("indexedDB",a)},r.hashchange=function(){return y("hashchange",a)&&(b.documentMode===c||b.documentMode>7)},r.history=function(){return!!a.history&&!!history.pushState},r.draganddrop=function(){var a=b.createElement("div");return"draggable"in a||"ondragstart"in a&&"ondrop"in a},r.websockets=function(){return"WebSocket"in a||"MozWebSocket"in a},r.rgba=function(){return B("background-color:rgba(150,255,150,.5)"),E(j.backgroundColor,"rgba")},r.hsla=function(){return B("background-color:hsla(120,40%,100%,.5)"),E(j.backgroundColor,"rgba")||E(j.backgroundColor,"hsla")},r.multiplebgs=function(){return B("background:url(https://),url(https://),red url(https://)"),/(url\s*\(.*?){3}/.test(j.background)},r.backgroundsize=function(){return H("backgroundSize")},r.borderimage=function(){return H("borderImage")},r.borderradius=function(){return H("borderRadius")},r.boxshadow=function(){return H("boxShadow")},r.textshadow=function(){return b.createElement("div").style.textShadow===""},r.opacity=function(){return C("opacity:.55"),/^0.55$/.test(j.opacity)},r.cssanimations=function(){return H("animationName")},r.csscolumns=function(){return H("columnCount")},r.cssgradients=function(){var a="background-image:",b="gradient(linear,left top,right bottom,from(#9f9),to(white));",c="linear-gradient(left top,#9f9, white);";return B((a+"-webkit- ".split(" ").join(b+a)+n.join(c+a)).slice(0,-a.length)),E(j.backgroundImage,"gradient")},r.cssreflections=function(){return H("boxReflect")},r.csstransforms=function(){return!!H("transform")},r.csstransforms3d=function(){var a=!!H("perspective");return a&&"webkitPerspective"in g.style&&x("@media (transform-3d),(-webkit-transform-3d){#modernizr{left:9px;position:absolute;height:3px;}}",function(b,c){a=b.offsetLeft===9&&b.offsetHeight===3}),a},r.csstransitions=function(){return H("transition")},r.fontface=function(){var a;return x('@font-face {font-family:"font";src:url("https://")}',function(c,d){var e=b.getElementById("smodernizr"),f=e.sheet||e.styleSheet,g=f?f.cssRules&&f.cssRules[0]?f.cssRules[0].cssText:f.cssText||"":"";a=/src/i.test(g)&&g.indexOf(d.split(" ")[0])===0}),a},r.generatedcontent=function(){var a;return x(["#",h,"{font:0/0 a}#",h,':after{content:"',l,'";visibility:hidden;font:3px/1 a}'].join(""),function(b){a=b.offsetHeight>=3}),a},r.video=function(){var a=b.createElement("video"),c=!1;try{if(c=!!a.canPlayType)c=new Boolean(c),c.ogg=a.canPlayType('video/ogg; codecs="theora"').replace(/^no$/,""),c.h264=a.canPlayType('video/mp4; codecs="avc1.42E01E"').replace(/^no$/,""),c.webm=a.canPlayType('video/webm; codecs="vp8, vorbis"').replace(/^no$/,"")}catch(d){}return c},r.audio=function(){var a=b.createElement("audio"),c=!1;try{if(c=!!a.canPlayType)c=new Boolean(c),c.ogg=a.canPlayType('audio/ogg; codecs="vorbis"').replace(/^no$/,""),c.mp3=a.canPlayType("audio/mpeg;").replace(/^no$/,""),c.wav=a.canPlayType('audio/wav; codecs="1"').replace(/^no$/,""),c.m4a=(a.canPlayType("audio/x-m4a;")||a.canPlayType("audio/aac;")).replace(/^no$/,"")}catch(d){}return c},r.localstorage=function(){try{return localStorage.setItem(h,h),localStorage.removeItem(h),!0}catch(a){return!1}},r.sessionstorage=function(){try{return sessionStorage.setItem(h,h),sessionStorage.removeItem(h),!0}catch(a){return!1}},r.webworkers=function(){return!!a.Worker},r.applicationcache=function(){return!!a.applicationCache};for(var J in r)A(r,J)&&(w=J.toLowerCase(),e[w]=r[J](),u.push((e[w]?"":"no-")+w));return e.input||I(),e.addTest=function(a,b){if(typeof a=="object")for(var d in a)A(a,d)&&e.addTest(d,a[d]);else{a=a.toLowerCase();if(e[a]!==c)return e;b=typeof b=="function"?b():b,typeof f!="undefined"&&f&&(g.className+=" "+(b?"":"no-")+a),e[a]=b}return e},B(""),i=k=null,function(a,b){function l(a,b){var c=a.createElement("p"),d=a.getElementsByTagName("head")[0]||a.documentElement;return c.innerHTML="x<style>"+b+"</style>",d.insertBefore(c.lastChild,d.firstChild)}function m(){var a=s.elements;return typeof a=="string"?a.split(" "):a}function n(a){var b=j[a[h]];return b||(b={},i++,a[h]=i,j[i]=b),b}function o(a,c,d){c||(c=b);if(k)return c.createElement(a);d||(d=n(c));var g;return d.cache[a]?g=d.cache[a].cloneNode():f.test(a)?g=(d.cache[a]=d.createElem(a)).cloneNode():g=d.createElem(a),g.canHaveChildren&&!e.test(a)&&!g.tagUrn?d.frag.appendChild(g):g}function p(a,c){a||(a=b);if(k)return a.createDocumentFragment();c=c||n(a);var d=c.frag.cloneNode(),e=0,f=m(),g=f.length;for(;e<g;e++)d.createElement(f[e]);return d}function q(a,b){b.cache||(b.cache={},b.createElem=a.createElement,b.createFrag=a.createDocumentFragment,b.frag=b.createFrag()),a.createElement=function(c){return s.shivMethods?o(c,a,b):b.createElem(c)},a.createDocumentFragment=Function("h,f","return function(){var n=f.cloneNode(),c=n.createElement;h.shivMethods&&("+m().join().replace(/[\w\-]+/g,function(a){return b.createElem(a),b.frag.createElement(a),'c("'+a+'")'})+");return n}")(s,b.frag)}function r(a){a||(a=b);var c=n(a);return s.shivCSS&&!g&&!c.hasCSS&&(c.hasCSS=!!l(a,"article,aside,dialog,figcaption,figure,footer,header,hgroup,main,nav,section{display:block}mark{background:#FF0;color:#000}template{display:none}")),k||q(a,c),a}var c="3.7.0",d=a.html5||{},e=/^<|^(?:button|map|select|textarea|object|iframe|option|optgroup)$/i,f=/^(?:a|b|code|div|fieldset|h1|h2|h3|h4|h5|h6|i|label|li|ol|p|q|span|strong|style|table|tbody|td|th|tr|ul)$/i,g,h="_html5shiv",i=0,j={},k;(function(){try{var a=b.createElement("a");a.innerHTML="<xyz></xyz>",g="hidden"in a,k=a.childNodes.length==1||function(){b.createElement("a");var a=b.createDocumentFragment();return typeof a.cloneNode=="undefined"||typeof a.createDocumentFragment=="undefined"||typeof a.createElement=="undefined"}()}catch(c){g=!0,k=!0}})();var s={elements:d.elements||"abbr article aside audio bdi canvas data datalist details dialog figcaption figure footer header hgroup main mark meter nav output progress section summary template time video",version:c,shivCSS:d.shivCSS!==!1,supportsUnknownElements:k,shivMethods:d.shivMethods!==!1,type:"default",shivDocument:r,createElement:o,createDocumentFragment:p};a.html5=s,r(b)}(this,b),e._version=d,e._prefixes=n,e._domPrefixes=q,e._cssomPrefixes=p,e.hasEvent=y,e.testProp=function(a){return F([a])},e.testAllProps=H,e.testStyles=x,g.className=g.className.replace(/(^|\s)no-js(\s|$)/,"$1$2")+(f?" js "+u.join(" "):""),e}(this,this.document),function(a,b,c){function d(a){return"[object Function]"==o.call(a)}function e(a){return"string"==typeof a}function f(){}function g(a){return!a||"loaded"==a||"complete"==a||"uninitialized"==a}function h(){var a=p.shift();q=1,a?a.t?m(function(){("c"==a.t?B.injectCss:B.injectJs)(a.s,0,a.a,a.x,a.e,1)},0):(a(),h()):q=0}function i(a,c,d,e,f,i,j){function k(b){if(!o&&g(l.readyState)&&(u.r=o=1,!q&&h(),l.onload=l.onreadystatechange=null,b)){"img"!=a&&m(function(){t.removeChild(l)},50);for(var d in y[c])y[c].hasOwnProperty(d)&&y[c][d].onload()}}var j=j||B.errorTimeout,l=b.createElement(a),o=0,r=0,u={t:d,s:c,e:f,a:i,x:j};1===y[c]&&(r=1,y[c]=[]),"object"==a?l.data=c:(l.src=c,l.type=a),l.width=l.height="0",l.onerror=l.onload=l.onreadystatechange=function(){k.call(this,r)},p.splice(e,0,u),"img"!=a&&(r||2===y[c]?(t.insertBefore(l,s?null:n),m(k,j)):y[c].push(l))}function j(a,b,c,d,f){return q=0,b=b||"j",e(a)?i("c"==b?v:u,a,b,this.i++,c,d,f):(p.splice(this.i++,0,a),1==p.length&&h()),this}function k(){var a=B;return a.loader={load:j,i:0},a}var l=b.documentElement,m=a.setTimeout,n=b.getElementsByTagName("script")[0],o={}.toString,p=[],q=0,r="MozAppearance"in l.style,s=r&&!!b.createRange().compareNode,t=s?l:n.parentNode,l=a.opera&&"[object Opera]"==o.call(a.opera),l=!!b.attachEvent&&!l,u=r?"object":l?"script":"img",v=l?"script":u,w=Array.isArray||function(a){return"[object Array]"==o.call(a)},x=[],y={},z={timeout:function(a,b){return b.length&&(a.timeout=b[0]),a}},A,B;B=function(a){function b(a){var a=a.split("!"),b=x.length,c=a.pop(),d=a.length,c={url:c,origUrl:c,prefixes:a},e,f,g;for(f=0;f<d;f++)g=a[f].split("="),(e=z[g.shift()])&&(c=e(c,g));for(f=0;f<b;f++)c=x[f](c);return c}function g(a,e,f,g,h){var i=b(a),j=i.autoCallback;i.url.split(".").pop().split("?").shift(),i.bypass||(e&&(e=d(e)?e:e[a]||e[g]||e[a.split("/").pop().split("?")[0]]),i.instead?i.instead(a,e,f,g,h):(y[i.url]?i.noexec=!0:y[i.url]=1,f.load(i.url,i.forceCSS||!i.forceJS&&"css"==i.url.split(".").pop().split("?").shift()?"c":c,i.noexec,i.attrs,i.timeout),(d(e)||d(j))&&f.load(function(){k(),e&&e(i.origUrl,h,g),j&&j(i.origUrl,h,g),y[i.url]=2})))}function h(a,b){function c(a,c){if(a){if(e(a))c||(j=function(){var a=[].slice.call(arguments);k.apply(this,a),l()}),g(a,j,b,0,h);else if(Object(a)===a)for(n in m=function(){var b=0,c;for(c in a)a.hasOwnProperty(c)&&b++;return b}(),a)a.hasOwnProperty(n)&&(!c&&!--m&&(d(j)?j=function(){var a=[].slice.call(arguments);k.apply(this,a),l()}:j[n]=function(a){return function(){var b=[].slice.call(arguments);a&&a.apply(this,b),l()}}(k[n])),g(a[n],j,b,n,h))}else!c&&l()}var h=!!a.test,i=a.load||a.both,j=a.callback||f,k=j,l=a.complete||f,m,n;c(h?a.yep:a.nope,!!i),i&&c(i)}var i,j,l=this.yepnope.loader;if(e(a))g(a,0,l,0);else if(w(a))for(i=0;i<a.length;i++)j=a[i],e(j)?g(j,0,l,0):w(j)?B(j):Object(j)===j&&h(j,l);else Object(a)===a&&h(a,l)},B.addPrefix=function(a,b){z[a]=b},B.addFilter=function(a){x.push(a)},B.errorTimeout=1e4,null==b.readyState&&b.addEventListener&&(b.readyState="loading",b.addEventListener("DOMContentLoaded",A=function(){b.removeEventListener("DOMContentLoaded",A,0),b.readyState="complete"},0)),a.yepnope=k(),a.yepnope.executeStack=h,a.yepnope.injectJs=function(a,c,d,e,i,j){var k=b.createElement("script"),l,o,e=e||B.errorTimeout;k.src=a;for(o in d)k.setAttribute(o,d[o]);c=j?h:c||f,k.onreadystatechange=k.onload=function(){!l&&g(k.readyState)&&(l=1,c(),k.onload=k.onreadystatechange=null)},m(function(){l||(l=1,c(1))},e),i?k.onload():n.parentNode.insertBefore(k,n)},a.yepnope.injectCss=function(a,c,d,e,g,i){var e=b.createElement("link"),j,c=i?h:c||f;e.href=a,e.rel="stylesheet",e.type="text/css";for(j in d)e.setAttribute(j,d[j]);g||(n.parentNode.insertBefore(e,n),m(c,0))}}(this,document),Modernizr.load=function(){yepnope.apply(window,[].slice.call(arguments,0))};

(function (global) {
    'use strict';
    // existing version for noConflict()
    var _hashBase64 = global.hashBase64;

    // if node.js, we use Buffer
    var buffer;
    if (typeof module !== 'undefined' && module.exports) {
        buffer = require('buffer').Buffer;
    }
    // constants
    var b64chars
        = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/';
    var b64tab = function (bin) {
        var t = {};
        for (var i = 0, l = bin.length; i < l; i++) t[bin.charAt(i)] = i;
        return t;
    }(b64chars);

    var fromCharCode = String.fromCharCode;
    // encoder stuff
    var cb_utob = function (c) {
        if (c.length < 2) {
            var cc = c.charCodeAt(0);
            return cc < 0x80 ? c
                : cc < 0x800 ? (fromCharCode(0xc0 | (cc >>> 6))
                                + fromCharCode(0x80 | (cc & 0x3f)))
                : (fromCharCode(0xe0 | ((cc >>> 12) & 0x0f))
                   + fromCharCode(0x80 | ((cc >>> 6) & 0x3f))
                   + fromCharCode(0x80 | (cc & 0x3f)));
        } else {
            var cc = 0x10000
                + (c.charCodeAt(0) - 0xD800) * 0x400
                + (c.charCodeAt(1) - 0xDC00);
            return (fromCharCode(0xf0 | ((cc >>> 18) & 0x07))
                    + fromCharCode(0x80 | ((cc >>> 12) & 0x3f))
                    + fromCharCode(0x80 | ((cc >>> 6) & 0x3f))
                    + fromCharCode(0x80 | (cc & 0x3f)));
        }
    };

    var re_utob = /[\uD800-\uDBFF][\uDC00-\uDFFFF]|[^\x00-\x7F]/g;
    var utob = function (u) {
        return u.replace(re_utob, cb_utob);
    };

    var cb_encode = function (ccc) {
        var padlen = [0, 2, 1][ccc.length % 3],
        ord = ccc.charCodeAt(0) << 16
            | ((ccc.length > 1 ? ccc.charCodeAt(1) : 0) << 8)
            | ((ccc.length > 2 ? ccc.charCodeAt(2) : 0)),
        chars = [
            b64chars.charAt(ord >>> 18),
            b64chars.charAt((ord >>> 12) & 63),
            padlen >= 2 ? '=' : b64chars.charAt((ord >>> 6) & 63),
            padlen >= 1 ? '=' : b64chars.charAt(ord & 63)
        ];
        return chars.join('');
    };

    var btoa = global.btoa
                ? function (b) { return global.btoa(b); }
                : function (b) { return b.replace(/[\s\S]{1,3}/g, cb_encode); };

    var _encode = buffer
                ? function (u) { return (new buffer(u)).toString('base64') }
                : function (u) { return btoa(utob(u)) };

    var encode = function (u, urisafe) {
        return !urisafe
            ? _encode(u)
            : _encode(u).replace(/[+\/]/g, function (m0) {
                return m0 == '+' ? '-' : '_';
            }).replace(/=/g, '');
    };
    var encodeURI = function (u) { return encode(u, true) };
    // decoder stuff
    var re_btou = new RegExp([
        '[\xC0-\xDF][\x80-\xBF]',
        '[\xE0-\xEF][\x80-\xBF]{2}',
        '[\xF0-\xF7][\x80-\xBF]{3}'
    ].join('|'), 'g');

    var cb_btou = function (cccc) {
        switch (cccc.length) {
            case 4:
                var cp = ((0x07 & cccc.charCodeAt(0)) << 18)
                    | ((0x3f & cccc.charCodeAt(1)) << 12)
                    | ((0x3f & cccc.charCodeAt(2)) << 6)
                    | (0x3f & cccc.charCodeAt(3)),
                offset = cp - 0x10000;
                return (fromCharCode((offset >>> 10) + 0xD800)
                        + fromCharCode((offset & 0x3FF) + 0xDC00));
            case 3:
                return fromCharCode(
                    ((0x0f & cccc.charCodeAt(0)) << 12)
                        | ((0x3f & cccc.charCodeAt(1)) << 6)
                        | (0x3f & cccc.charCodeAt(2))
                );
            default:
                return fromCharCode(
                    ((0x1f & cccc.charCodeAt(0)) << 6)
                        | (0x3f & cccc.charCodeAt(1))
                );
        }
    };
    var btou = function (b) {
        return b.replace(re_btou, cb_btou);
    };
    var cb_decode = function (cccc) {
        var len = cccc.length,
        padlen = len % 4,
        n = (len > 0 ? b64tab[cccc.charAt(0)] << 18 : 0)
            | (len > 1 ? b64tab[cccc.charAt(1)] << 12 : 0)
            | (len > 2 ? b64tab[cccc.charAt(2)] << 6 : 0)
            | (len > 3 ? b64tab[cccc.charAt(3)] : 0),
        chars = [
            fromCharCode(n >>> 16),
            fromCharCode((n >>> 8) & 0xff),
            fromCharCode(n & 0xff)
        ];
        chars.length -= [0, 0, 2, 1][padlen];
        return chars.join('');
    };
    var atob = global.atob ? function (a) {
        return global.atob(a);
    } : function (a) {
        return a.replace(/[\s\S]{1,4}/g, cb_decode);
    };
    var _decode = buffer
        ? function (a) { return (new buffer(a, 'base64')).toString() }
    : function (a) { return btou(atob(a)) };
    var decode = function (a) {
        return _decode(
            a.replace(/[-_]/g, function (m0) { return m0 == '-' ? '+' : '/' })
                .replace(/[^A-Za-z0-9\+\/]/g, '')
        );
    };
    var noConflict = function () {
        var hashBase64 = global.hashBase64;
        global.hashBase64 = _hashBase64;
        return hashBase64;
    };
    // export Base64
    global.hashBase64 = {
        //atob: atob,
        //btoa: btoa,
        fromBase64: decode,
        toBase64: encode,
        //utob: utob,
        encode: encode,
        encodeURI: encodeURI,
        //btou: btou,
        decode: decode,
        //noConflict: noConflict
    };
    // if ES5 is available, make Base64.extendString() available
    if (typeof Object.defineProperty === 'function') {
        var noEnum = function (v) {
            return { value: v, enumerable: false, writable: true, configurable: true };
        };
        global.hashBase64.extendString = function () {
            Object.defineProperty(
                String.prototype, 'fromBase64', noEnum(function () {
                    return decode(this)
                }));
            Object.defineProperty(
                String.prototype, 'toBase64', noEnum(function (urisafe) {
                    return encode(this, urisafe)
                }));
            Object.defineProperty(
                String.prototype, 'toBase64URI', noEnum(function () {
                    return encode(this, true)
                }));
        };
    }
    // that's it!
})(this);

if (this['Meteor']) {
    hashBase64 = global.hashBase64; // for normal export in Meteor.js
}

/*!
 * Globalize
 *
 * http://github.com/jquery/globalize
 *
 * Copyright Software Freedom Conservancy, Inc.
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 */
(function(n,t){var i,g,nt,tt,it,r,h,v,c,rt,y,f,u,p,e,l,w,b,ut,k,o,a,d,s;i=function(n){return new i.prototype.init(n)};typeof require!="undefined"&&typeof exports!="undefined"&&typeof module!="undefined"?module.exports=i:n.Globalize=i;i.cultures={};i.prototype={constructor:i,init:function(n){return this.cultures=i.cultures,this.cultureSelector=n,this}};i.prototype.init.prototype=i.prototype;i.cultures["default"]={name:"en",englishName:"English",nativeName:"English",isRTL:!1,language:"en",numberFormat:{pattern:["-n"],decimals:2,",":",",".":".",groupSizes:[3],"+":"+","-":"-",NaN:"NaN",negativeInfinity:"-Infinity",positiveInfinity:"Infinity",percent:{pattern:["-n %","n %"],decimals:2,groupSizes:[3],",":",",".":".",symbol:"%"},currency:{pattern:["($n)","$n"],decimals:2,groupSizes:[3],",":",",".":".",symbol:"$"}},calendars:{standard:{name:"Gregorian_USEnglish","/":"/",":":":",firstDay:0,days:{names:["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"],namesAbbr:["Sun","Mon","Tue","Wed","Thu","Fri","Sat"],namesShort:["Su","Mo","Tu","We","Th","Fr","Sa"]},months:{names:["January","February","March","April","May","June","July","August","September","October","November","December",""],namesAbbr:["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec",""]},AM:["AM","am","AM"],PM:["PM","pm","PM"],eras:[{name:"A.D.",start:null,offset:0}],twoDigitYearMax:2029,patterns:{d:"M/d/yyyy",D:"dddd, MMMM dd, yyyy",g:"M/d/yyyy h:mm tt",G:"M/d/yyyy h:mm:ss tt",t:"h:mm tt",T:"h:mm:ss tt",f:"dddd, MMMM dd, yyyy h:mm tt",F:"dddd, MMMM dd, yyyy h:mm:ss tt",M:"MMMM dd",Y:"yyyy MMMM",S:"yyyy'-'MM'-'dd'T'HH':'mm':'ss"}}},messages:{}};i.cultures["default"].calendar=i.cultures["default"].calendars.standard;i.cultures.en=i.cultures["default"];i.cultureSelector="en";g=/^0x[a-f0-9]+$/i;nt=/^[+\-]?infinity$/i;tt=/^[+\-]?\d*\.?\d*(e[+\-]?\d+)?$/;it=/^\s+|\s+$/g;r=function(n,t){if(n.indexOf)return n.indexOf(t);for(var i=0,r=n.length;i<r;i++)if(n[i]===t)return i;return-1};h=function(n,t){return n.substr(n.length-t.length)===t};v=function(){var e,u,r,i,o,s,n=arguments[0]||{},f=1,l=arguments.length,h=!1;for(typeof n=="boolean"&&(h=n,n=arguments[1]||{},f=2),typeof n=="object"||rt(n)||(n={});f<l;f++)if((e=arguments[f])!=null)for(u in e)(r=n[u],i=e[u],n!==i)&&(h&&i&&(y(i)||(o=c(i)))?(o?(o=!1,s=r&&c(r)?r:[]):s=r&&y(r)?r:{},n[u]=v(h,s,i)):i!==t&&(n[u]=i));return n};c=Array.isArray||function(n){return Object.prototype.toString.call(n)==="[object Array]"};rt=function(n){return Object.prototype.toString.call(n)==="[object Function]"};y=function(n){return Object.prototype.toString.call(n)==="[object Object]"};f=function(n,t){return n.indexOf(t)===0};u=function(n){return(n+"").replace(it,"")};p=function(n){return isNaN(n)?NaN:Math[n<0?"ceil":"floor"](n)};e=function(n,t,i){for(var r=n.length;r<t;r+=1)n=i?"0"+n:n+"0";return n};l=function(n,t){for(var u,f=0,i=!1,r=0,e=n.length;r<e;r++){u=n.charAt(r);switch(u){case"'":i?t.push("'"):f++;i=!1;break;case"\\":i&&t.push("\\");i=!i;break;default:t.push(u);i=!1}}return f};w=function(n,t){t=t||"F";var i,u=n.patterns,r=t.length;if(r===1){if(i=u[t],!i)throw"Invalid date format string '"+t+"'.";t=i}else r===2&&t.charAt(0)==="%"&&(t=t.charAt(1));return t};b=function(n,t,i){function e(n,t){var i,r=n+"";return t>1&&r.length<t?(i=st[t-2]+r,i.substr(i.length-t,t)):r}function ct(){return c||ut?c:(c=ht.test(t),ut=!0,c)}function it(n,t){if(v)return v[t];switch(t){case 0:return n.getFullYear();case 1:return n.getMonth();case 2:return n.getDate();default:throw"Invalid part value "+t;}}var u=i.calendar,d=u.convert,r,g,rt,nt,tt,p,f,ot,h;if(!t||!t.length||t==="i")return i&&i.name.length?d?r=b(n,u.patterns.F,i):(g=new Date(n.getTime()),rt=o(n,u.eras),g.setFullYear(a(n,u,rt)),r=g.toLocaleString()):r=n.toString(),r;nt=u.eras;tt=t==="s";t=w(u,t);r=[];var s,st=["0","00","000"],c,ut,ht=/([^d]|^)(d|dd)([^d]|$)/g,ft=0,et=k(),v;for(!tt&&d&&(v=d.fromGregorian(n));;){var lt=et.lastIndex,y=et.exec(t),at=t.slice(lt,y?y.index:t.length);if(ft+=l(at,r),!y)break;if(ft%2){r.push(y[0]);continue}p=y[0];f=p.length;switch(p){case"ddd":case"dddd":ot=f===3?u.days.namesAbbr:u.days.names;r.push(ot[n.getDay()]);break;case"d":case"dd":c=!0;r.push(e(it(n,2),f));break;case"MMM":case"MMMM":h=it(n,1);r.push(u.monthsGenitive&&ct()?u.monthsGenitive[f===3?"namesAbbr":"names"][h]:u.months[f===3?"namesAbbr":"names"][h]);break;case"M":case"MM":r.push(e(it(n,1)+1,f));break;case"y":case"yy":case"yyyy":h=v?v[0]:a(n,u,o(n,nt),tt);f<4&&(h=h%100);r.push(e(h,f));break;case"h":case"hh":s=n.getHours()%12;s===0&&(s=12);r.push(e(s,f));break;case"H":case"HH":r.push(e(n.getHours(),f));break;case"m":case"mm":r.push(e(n.getMinutes(),f));break;case"s":case"ss":r.push(e(n.getSeconds(),f));break;case"t":case"tt":h=n.getHours()<12?u.AM?u.AM[0]:" ":u.PM?u.PM[0]:" ";r.push(f===1?h.charAt(0):h);break;case"f":case"ff":case"fff":r.push(e(n.getMilliseconds(),3).substr(0,f));break;case"z":case"zz":s=n.getTimezoneOffset()/60;r.push((s<=0?"+":"-")+e(Math.floor(Math.abs(s)),f));break;case"zzz":s=n.getTimezoneOffset()/60;r.push((s<=0?"+":"-")+e(Math.floor(Math.abs(s)),2)+":"+e(Math.abs(n.getTimezoneOffset()%60),2));break;case"g":case"gg":u.eras&&r.push(u.eras[o(n,nt)].name);break;case"/":r.push(u["/"]);break;default:throw"Invalid date format pattern '"+p+"'.";}}return r.join("")},function(){var n;n=function(n,t,i){var l=i.groupSizes,c=l[0],a=1,p=Math.pow(10,t),v=Math.round(n*p)/p;isFinite(v)||(v=n);n=v;var r=n+"",u="",o=r.split(/e/i),f=o.length>1?parseInt(o[1],10):0;r=o[0];o=r.split(".");r=o[0];u=o.length>1?o[1]:"";f>0?(u=e(u,f,!1),r+=u.slice(0,f),u=u.substr(f)):f<0&&(f=-f,r=e(r,f+1,!0),u=r.slice(-f,r.length)+u,r=r.slice(0,-f));u=t>0?i["."]+(u.length>t?u.slice(0,t):e(u,t)):"";for(var s=r.length-1,y=i[","],h="";s>=0;){if(c===0||c>s)return r.slice(0,s+1)+(h.length?y+h+u:u);h=r.slice(s-c+1,s+1)+(h.length?y+h:"");s-=c;a<l.length&&(c=l[a],a++)}return r.slice(0,s+1)+y+h+u};ut=function(t,i,r){var a,f,v,o,y,l;if(!isFinite(t))return t===Infinity?r.numberFormat.positiveInfinity:t===-Infinity?r.numberFormat.negativeInfinity:r.numberFormat.NaN;if(!i||i==="i")return r.name.length?t.toLocaleString():t.toString();i=i||"D";var s=r.numberFormat,u=Math.abs(t),h=-1,c;i.length>1&&(h=parseInt(i.slice(1),10));a=i.charAt(0).toUpperCase();switch(a){case"D":c="n";u=p(u);h!==-1&&(u=e(""+u,h,!0));t<0&&(u="-"+u);break;case"N":f=s;case"C":f=f||s.currency;case"P":f=f||s.percent;c=t<0?f.pattern[0]:f.pattern[1]||"n";h===-1&&(h=f.decimals);u=n(u*(a==="P"?100:1),h,f);break;default:throw"Bad number format specifier: "+a;}for(v=/n|\$|-|%/g,o="";;){if(y=v.lastIndex,l=v.exec(c),o+=c.slice(y,l?l.index:c.length),!l)break;switch(l[0]){case"n":o+=u;break;case"$":o+=s.currency.symbol;break;case"-":/[1-9]/.test(u)&&(o+=s["-"]);break;case"%":o+=s.percent.symbol}}return o}}();k=function(){return/\/|dddd|ddd|dd|d|MMMM|MMM|MM|M|yyyy|yy|y|hh|h|HH|H|mm|m|ss|s|tt|t|fff|ff|f|zzz|zz|z|gg|g/g};o=function(n,t){var r,u,i,f;if(!t)return 0;for(u=n.getTime(),i=0,f=t.length;i<f;i++)if(r=t[i].start,r===null||u>=r)return i;return 0};a=function(n,t,i,r){var u=n.getFullYear();return!r&&t.eras&&(u-=t.eras[i].offset),u},function(){var e,s,h,c,n,i,t;e=function(n,t){if(t<100){var r=new Date,f=o(r),u=a(r,n,f),i=n.twoDigitYearMax;i=typeof i=="string"?(new Date).getFullYear()%100+parseInt(i,10):i;t+=u-u%100;t>i&&(t-=100)}return t};s=function(n,u,f){var e,s=n.days,o=n._upperDays;return o||(n._upperDays=o=[t(s.names),t(s.namesAbbr),t(s.namesShort)]),u=i(u),f?(e=r(o[1],u),e===-1&&(e=r(o[2],u))):e=r(o[0],u),e};h=function(n,u,f){var h=n.months,c=n.monthsGenitive||n.months,e=n._upperMonths,s=n._upperMonthsGen,o;return e||(n._upperMonths=e=[t(h.names),t(h.namesAbbr)],n._upperMonthsGen=s=[t(c.names),t(c.namesAbbr)]),u=i(u),o=r(f?e[1]:e[0],u),o<0&&(o=r(f?s[1]:s[0],u)),o};c=function(n,t){var f=n._parseRegExp,o,y,e,p,i,b,d;if(f){if(o=f[t],o)return o}else n._parseRegExp=f={};for(var s=w(n,t).replace(/([\^\$\.\*\+\?\|\[\]\(\)\{\}])/g,"\\\\$1"),r=["^"],c=[],h=0,a=0,v=k(),u;(u=v.exec(s))!==null;){if(y=s.slice(h,u.index),h=v.lastIndex,a+=l(y,r),a%2){r.push(u[0]);continue}e=u[0];p=e.length;switch(e){case"dddd":case"ddd":case"MMMM":case"MMM":case"gg":case"g":i="(\\D+)";break;case"tt":case"t":i="(\\D*)";break;case"yyyy":case"fff":case"ff":case"f":i="(\\d{"+p+"})";break;case"dd":case"d":case"MM":case"M":case"yy":case"y":case"HH":case"H":case"hh":case"h":case"mm":case"m":case"ss":case"s":i="(\\d\\d?)";break;case"zzz":i="([+-]?\\d\\d?:\\d{2})";break;case"zz":case"z":i="([+-]?\\d\\d?)";break;case"/":i="(\\/)";break;default:throw"Invalid date format pattern '"+e+"'.";}i&&r.push(i);c.push(u[0])}return l(s.slice(h),r),r.push("$"),b=r.join("").replace(/\s+/g,"\\s+"),d={regExp:b,groups:c},f[t]=d};n=function(n,t,i){return n<t||n>i};i=function(n){return n.split(" ").join(" ").toUpperCase()};t=function(n){for(var r=[],t=0,u=n.length;t<u;t++)r[t]=i(n[t]);return r};d=function(t,i,r){var d,wt,l,ft,et,g,nt,kt,a,dt,tt,at;t=u(t);var o=r.calendar,vt=c(o,i),yt=new RegExp(vt.regExp).exec(t);if(yt===null)return null;var pt=vt.groups,ot=null,w=null,p=null,b=null,it=null,y=0,k,st=0,ht=0,ct=0,rt=null,lt=!1;for(d=0,wt=pt.length;d<wt;d++)if(l=yt[d+1],l){var bt=pt[d],ut=bt.length,v=parseInt(l,10);switch(bt){case"dd":case"d":if(b=v,n(b,1,31))return null;break;case"MMM":case"MMMM":if(p=h(o,l,ut===3),n(p,0,11))return null;break;case"M":case"MM":if(p=v-1,n(p,0,11))return null;break;case"y":case"yy":case"yyyy":if(w=ut<4?e(o,v):v,n(w,0,9999))return null;break;case"h":case"hh":if(y=v,y===12&&(y=0),n(y,0,11))return null;break;case"H":case"HH":if(y=v,n(y,0,23))return null;break;case"m":case"mm":if(st=v,n(st,0,59))return null;break;case"s":case"ss":if(ht=v,n(ht,0,59))return null;break;case"tt":case"t":if(lt=o.PM&&(l===o.PM[0]||l===o.PM[1]||l===o.PM[2]),!lt&&(!o.AM||l!==o.AM[0]&&l!==o.AM[1]&&l!==o.AM[2]))return null;break;case"f":case"ff":case"fff":if(ct=v*Math.pow(10,3-ut),n(ct,0,999))return null;break;case"ddd":case"dddd":if(it=s(o,l,ut===3),n(it,0,6))return null;break;case"zzz":if((ft=l.split(/:/),ft.length!==2)||(k=parseInt(ft[0],10),n(k,-12,13))||(et=parseInt(ft[1],10),n(et,0,59)))return null;rt=k*60+(f(l,"-")?-et:et);break;case"z":case"zz":if(k=v,n(k,-12,13))return null;rt=k*60;break;case"g":case"gg":if(g=l,!g||!o.eras)return null;for(g=u(g.toLowerCase()),nt=0,kt=o.eras.length;nt<kt;nt++)if(g===o.eras[nt].name.toLowerCase()){ot=nt;break}if(ot===null)return null}}if(a=new Date,tt=o.convert,dt=tt?tt.fromGregorian(a)[0]:a.getFullYear(),w===null?w=dt:o.eras&&(w+=o.eras[ot||0].offset),p===null&&(p=0),b===null&&(b=1),tt){if(a=tt.toGregorian(w,p,b),a===null)return null}else if((a.setFullYear(w,p,b),a.getDate()!==b)||it!==null&&a.getDay()!==it)return null;return lt&&y<12&&(y+=12),a.setHours(y,st,ht,ct),rt!==null&&(at=a.getMinutes()-(rt+a.getTimezoneOffset()),a.setHours(a.getHours()+parseInt(at/60,10),at%60)),a}}();s=function(n,t,i){var r=t["-"],u=t["+"],e;switch(i){case"n -":r=" "+r;u=" "+u;case"n-":h(n,r)?e=["-",n.substr(0,n.length-r.length)]:h(n,u)&&(e=["+",n.substr(0,n.length-u.length)]);break;case"- n":r+=" ";u+=" ";case"-n":f(n,r)?e=["-",n.substr(r.length)]:f(n,u)&&(e=["+",n.substr(u.length)]);break;case"(n)":f(n,"(")&&h(n,")")&&(e=["-",n.substr(1,n.length-2)])}return e||["",n]};i.prototype.findClosestCulture=function(n){return i.findClosestCulture.call(this,n)};i.prototype.format=function(n,t,r){return i.format.call(this,n,t,r)};i.prototype.localize=function(n,t){return i.localize.call(this,n,t)};i.prototype.parseInt=function(n,t,r){return i.parseInt.call(this,n,t,r)};i.prototype.parseFloat=function(n,t,r){return i.parseFloat.call(this,n,t,r)};i.prototype.culture=function(n){return i.culture.call(this,n)};i.addCultureInfo=function(n,t,i){var r={},u=!1;typeof n!="string"?(i=n,n=this.culture().name,r=this.cultures[n]):typeof t!="string"?(i=t,u=this.cultures[n]==null,r=this.cultures[n]||this.cultures["default"]):(u=!0,r=this.cultures[t]);this.cultures[n]=v(!0,{},r,i);u&&(this.cultures[n].calendar=this.cultures[n].calendars.standard)};i.findClosestCulture=function(n){var r,f,h,l,y,a;if(!n)return this.findClosestCulture(this.cultureSelector)||this.cultures["default"];if(typeof n=="string"&&(n=n.split(",")),c(n)){for(var i,o=this.cultures,v=n,s=v.length,e=[],t=0;t<s;t++)n=u(v[t]),h=n.split(";"),i=u(h[0]),h.length===1?f=1:(n=u(h[1]),n.indexOf("q=")===0?(n=n.substr(2),f=parseFloat(n),f=isNaN(f)?0:f):f=1),e.push({lang:i,pri:f});for(e.sort(function(n,t){return n.pri<t.pri?1:n.pri>t.pri?-1:0}),t=0;t<s;t++)if(i=e[t].lang,r=o[i],r)return r;for(t=0;t<s;t++){i=e[t].lang;do{if(l=i.lastIndexOf("-"),l===-1)break;if(i=i.substr(0,l),r=o[i],r)return r}while(1)}for(t=0;t<s;t++){i=e[t].lang;for(y in o)if(a=o[y],a.language==i)return a}}else if(typeof n=="object")return n;return r||null};i.format=function(n,t,i){var r=this.findClosestCulture(i);return n instanceof Date?n=b(n,t,r):typeof n=="number"&&(n=ut(n,t,r)),n};i.localize=function(n,t){return this.findClosestCulture(t).messages[n]||this.cultures["default"].messages[n]};i.parseDate=function(n,t,i){var r,o,f,u,s,e;if(i=this.findClosestCulture(i),t){if(typeof t=="string"&&(t=[t]),t.length)for(u=0,s=t.length;u<s;u++)if(e=t[u],e&&(r=d(n,e,i),r))break}else{f=i.calendar.patterns;for(o in f)if(r=d(n,f[o],i),r)break}return r||null};i.parseInt=function(n,t,r){return p(i.parseFloat(n,t,r))};i.parseFloat=function(n,t,i){var y,c,l,h,p,k,w,b,d,a,it;typeof t!="number"&&(i=t,t=10);var f=this.findClosestCulture(i),v=NaN,r=f.numberFormat;if(n.indexOf(f.numberFormat.currency.symbol)>-1&&(n=n.replace(f.numberFormat.currency.symbol,""),n=n.replace(f.numberFormat.currency["."],f.numberFormat["."])),n.indexOf(f.numberFormat.percent.symbol)>-1&&(n=n.replace(f.numberFormat.percent.symbol,"")),n=n.replace(/ /g,""),nt.test(n))v=parseFloat(n);else if(!t&&g.test(n))v=parseInt(n,16);else{var u=s(n,r,r.pattern[0]),e=u[0],o=u[1];e===""&&r.pattern[0]!=="(n)"&&(u=s(n,r,"(n)"),e=u[0],o=u[1]);e===""&&r.pattern[0]!=="-n"&&(u=s(n,r,"-n"),e=u[0],o=u[1]);e=e||"+";l=o.indexOf("e");l<0&&(l=o.indexOf("E"));l<0?(c=o,y=null):(c=o.substr(0,l),y=o.substr(l+1));k=r["."];w=c.indexOf(k);w<0?(h=c,p=null):(h=c.substr(0,w),p=c.substr(w+k.length));b=r[","];h=h.split(b).join("");d=b.replace(/\u00A0/g," ");b!==d&&(h=h.split(d).join(""));a=e+h;p!==null&&(a+="."+p);y!==null&&(it=s(y,r,"-n"),a+="e"+(it[0]||"+")+it[1]);tt.test(a)&&(v=parseFloat(a))}return v};i.culture=function(n){return typeof n!="undefined"&&(this.cultureSelector=n),this.findClosestCulture(n)||this.cultures["default"]}})(this);
//# sourceMappingURL=globalize.min.js.map

/*
 * Globalize Culture vi-VN
 *
 * http://github.com/jquery/globalize
 *
 * Copyright Software Freedom Conservancy, Inc.
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 *
 * This file was generated by the Globalize Culture Generator
 * Translation: bugs found in this file need to be fixed in the generator
 */

(function( window, undefined ) {

var Globalize;

if ( typeof require !== "undefined" &&
	typeof exports !== "undefined" &&
	typeof module !== "undefined" ) {
	// Assume CommonJS
	Globalize = require( "globalize" );
} else {
	// Global variable
	Globalize = window.Globalize;
}

Globalize.addCultureInfo( "vi-VN", "default", {
	name: "vi-VN",
	englishName: "Vietnamese (Vietnam)",
	nativeName: "Tiếng Việt (Việt Nam)",
	language: "vi",
	numberFormat: {
		",": ".",
		".": ",",
		percent: {
			",": ".",
			".": ","
		},
		currency: {
			pattern: ["-n $","n $"],
			",": ".",
			".": ",",
			symbol: "₫"
		}
	},
	calendars: {
		standard: {
			firstDay: 1,
			days: {
				names: ["Chủ Nhật","Thứ Hai","Thứ Ba","Thứ Tư","Thứ Năm","Thứ Sáu","Thứ Bảy"],
				namesAbbr: ["CN","Hai","Ba","Tư","Năm","Sáu","Bảy"],
				namesShort: ["C","H","B","T","N","S","B"]
			},
			months: {
				names: ["Tháng Giêng","Tháng Hai","Tháng Ba","Tháng Tư","Tháng Năm","Tháng Sáu","Tháng Bảy","Tháng Tám","Tháng Chín","Tháng Mười","Tháng Mười Một","Tháng Mười Hai",""],
				namesAbbr: ["Thg1","Thg2","Thg3","Thg4","Thg5","Thg6","Thg7","Thg8","Thg9","Thg10","Thg11","Thg12",""]
			},
			AM: ["SA","sa","SA"],
			PM: ["CH","ch","CH"],
			patterns: {
				d: "dd/MM/yyyy",
				D: "dd MMMM yyyy",
				g: "dd/MM/yyyy h:mm tt",
				G: "dd/MM/yyyy h:mm:ss tt",
				f: "dd MMMM yyyy h:mm tt",
				F: "dd MMMM yyyy h:mm:ss tt",
				M: "dd MMMM",
				Y: "MMMM yyyy"
			}
		}
	}
});

}( this ));

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

(function(n){"use strict";n(window.jQuery)})(function(n){"use strict";n.support.xhrFileUpload=!!(window.XMLHttpRequestUpload&&window.FileReader);n.support.xhrFormDataFileUpload=!!window.FormData;n.widget("blueimp.fileupload",{options:{dropZone:n(document),pasteZone:n(document),fileInput:undefined,replaceFileInput:!0,paramName:undefined,singleFileUploads:!0,limitMultiFileUploads:undefined,sequentialUploads:!1,limitConcurrentUploads:undefined,forceIframeTransport:!1,redirect:undefined,redirectParamName:undefined,postMessage:undefined,multipart:!0,maxChunkSize:undefined,uploadedBytes:undefined,recalculateProgress:!0,progressInterval:100,bitrateInterval:500,autoUpload:!0,formData:function(n){return n.serializeArray()},add:function(t,i){(i.autoUpload||i.autoUpload!==!1&&(n(this).data("blueimp-fileupload")||n(this).data("fileupload")).options.autoUpload)&&i.submit()},processData:!1,contentType:!1,cache:!1},_refreshOptionsList:["fileInput","dropZone","pasteZone","multipart","forceIframeTransport"],_BitrateTimer:function(){this.timestamp=+new Date;this.loaded=0;this.bitrate=0;this.getBitrate=function(n,t,i){var r=n-this.timestamp;return(!this.bitrate||!i||r>i)&&(this.bitrate=(t-this.loaded)*(1e3/r)*8,this.loaded=t,this.timestamp=n),this.bitrate}},_isXHRUpload:function(t){return!t.forceIframeTransport&&(!t.multipart&&n.support.xhrFileUpload||n.support.xhrFormDataFileUpload)},_getFormData:function(t){var i;return typeof t.formData=="function"?t.formData(t.form):n.isArray(t.formData)?t.formData:t.formData?(i=[],n.each(t.formData,function(n,t){i.push({name:n,value:t})}),i):[]},_getTotal:function(t){var i=0;return n.each(t,function(n,t){i+=t.size||1}),i},_initProgressObject:function(n){n._progress={loaded:0,total:0,bitrate:0}},_onProgress:function(n,t){if(n.lengthComputable){var i=+new Date,r;if(t._time&&t.progressInterval&&i-t._time<t.progressInterval&&n.loaded!==n.total)return;t._time=i;r=Math.floor(n.loaded/n.total*(t.chunkSize||t._progress.total))+(t.uploadedBytes||0);this._progress.loaded+=r-t._progress.loaded;this._progress.bitrate=this._bitrateTimer.getBitrate(i,this._progress.loaded,t.bitrateInterval);t._progress.loaded=t.loaded=r;t._progress.bitrate=t.bitrate=t._bitrateTimer.getBitrate(i,r,t.bitrateInterval);this._trigger("progress",n,t);this._trigger("progressall",n,this._progress)}},_initProgressListener:function(t){var r=this,i=t.xhr?t.xhr():n.ajaxSettings.xhr();i.upload&&(n(i.upload).bind("progress",function(n){var i=n.originalEvent;n.lengthComputable=i.lengthComputable;n.loaded=i.loaded;n.total=i.total;r._onProgress(n,t)}),t.xhr=function(){return i})},_initXHRData:function(t){var i,r=t.files[0],f=t.multipart||!n.support.xhrFileUpload,u=t.paramName[0];t.headers=t.headers||{};t.contentRange&&(t.headers["Content-Range"]=t.contentRange);f?n.support.xhrFormDataFileUpload&&(t.postMessage?(i=this._getFormData(t),t.blob?i.push({name:u,value:t.blob}):n.each(t.files,function(n,r){i.push({name:t.paramName[n]||u,value:r})})):(t.formData instanceof FormData?i=t.formData:(i=new FormData,n.each(this._getFormData(t),function(n,t){i.append(t.name,t.value)})),t.blob?(t.headers["Content-Disposition"]='attachment; filename="'+encodeURI(r.name)+'"',i.append(u,t.blob,r.name)):n.each(t.files,function(n,r){(window.Blob&&r instanceof Blob||window.File&&r instanceof File)&&i.append(t.paramName[n]||u,r,r.name)})),t.data=i):(t.headers["Content-Disposition"]='attachment; filename="'+encodeURI(r.name)+'"',t.contentType=r.type,t.data=t.blob||r);t.blob=null},_initIframeSettings:function(t){t.dataType="iframe "+(t.dataType||"");t.formData=this._getFormData(t);t.redirect&&n("<a><\/a>").prop("href",t.url).prop("host")!==location.host&&t.formData.push({name:t.redirectParamName||"redirect",value:t.redirect})},_initDataSettings:function(n){this._isXHRUpload(n)?(this._chunkedUpload(n,!0)||(n.data||this._initXHRData(n),this._initProgressListener(n)),n.postMessage&&(n.dataType="postmessage "+(n.dataType||""))):this._initIframeSettings(n,"iframe")},_getParamName:function(t){var r=n(t.fileInput),i=t.paramName;return i?n.isArray(i)||(i=[i]):(i=[],r.each(function(){for(var t=n(this),u=t.prop("name")||"files[]",r=(t.prop("files")||[1]).length;r;)i.push(u),r-=1}),i.length||(i=[r.prop("name")||"files[]"])),i},_initFormSettings:function(t){t.form&&t.form.length||(t.form=n(t.fileInput.prop("form")),t.form.length||(t.form=n(this.options.fileInput.prop("form"))));t.paramName=this._getParamName(t);t.url||(t.url=t.form.prop("action")||location.href);t.type=(t.type||t.form.prop("method")||"").toUpperCase();t.type!=="POST"&&t.type!=="PUT"&&t.type!=="PATCH"&&(t.type="POST");t.formAcceptCharset||(t.formAcceptCharset=t.form.attr("accept-charset"))},_getAJAXSettings:function(t){var i=n.extend({},this.options,t);return this._initFormSettings(i),this._initDataSettings(i),i},_getDeferredState:function(n){return n.state?n.state():n.isResolved()?"resolved":n.isRejected()?"rejected":"pending"},_enhancePromise:function(n){return n.success=n.done,n.error=n.fail,n.complete=n.always,n},_getXHRPromise:function(t,i,r){var u=n.Deferred(),f=u.promise();return i=i||this.options.context||f,t===!0?u.resolveWith(i,r):t===!1&&u.rejectWith(i,r),f.abort=u.promise,this._enhancePromise(f)},_addConvenienceMethods:function(n,t){var i=this;t.submit=function(){return this.state()!=="pending"&&(t.jqXHR=this.jqXHR=i._trigger("submit",n,this)!==!1&&i._onSend(n,this)),this.jqXHR||i._getXHRPromise()};t.abort=function(){return this.jqXHR?this.jqXHR.abort():this._getXHRPromise()};t.state=function(){if(this.jqXHR)return i._getDeferredState(this.jqXHR)};t.progress=function(){return this._progress}},_getUploadedBytes:function(n){var i=n.getResponseHeader("Range"),t=i&&i.split("-"),r=t&&t.length>1&&parseInt(t[1],10);return r&&r+1},_chunkedUpload:function(t,i){var u=this,f=t.files[0],e=f.size,r=t.uploadedBytes=t.uploadedBytes||0,c=t.maxChunkSize||e,l=f.slice||f.webkitSlice||f.mozSlice,o=n.Deferred(),s=o.promise(),a,h;return!(this._isXHRUpload(t)&&l&&(r||c<e))||t.data?!1:i?!0:r>=e?(f.error="Uploaded bytes exceed file size",this._getXHRPromise(!1,t.context,[null,"error",f.error])):(h=function(){var i=n.extend({},t),s=i._progress.loaded;i.blob=l.call(f,r,r+c,f.type);i.chunkSize=i.blob.size;i.contentRange="bytes "+r+"-"+(r+i.chunkSize-1)+"/"+e;u._initXHRData(i);u._initProgressListener(i);a=(u._trigger("chunksend",null,i)!==!1&&n.ajax(i)||u._getXHRPromise(!1,i.context)).done(function(f,c,l){r=u._getUploadedBytes(l)||r+i.chunkSize;i._progress.loaded===s&&u._onProgress(n.Event("progress",{lengthComputable:!0,loaded:r-i.uploadedBytes,total:r-i.uploadedBytes}),i);t.uploadedBytes=i.uploadedBytes=r;i.result=f;i.textStatus=c;i.jqXHR=l;u._trigger("chunkdone",null,i);u._trigger("chunkalways",null,i);r<e?h():o.resolveWith(i.context,[f,c,l])}).fail(function(n,t,r){i.jqXHR=n;i.textStatus=t;i.errorThrown=r;u._trigger("chunkfail",null,i);u._trigger("chunkalways",null,i);o.rejectWith(i.context,[n,t,r])})},this._enhancePromise(s),s.abort=function(){return a.abort()},h(),s)},_beforeSend:function(n,t){this._active===0&&(this._trigger("start"),this._bitrateTimer=new this._BitrateTimer,this._progress.loaded=this._progress.total=0,this._progress.bitrate=0);t._progress||(t._progress={});t._progress.loaded=t.loaded=t.uploadedBytes||0;t._progress.total=t.total=this._getTotal(t.files)||1;t._progress.bitrate=t.bitrate=0;this._active+=1;this._progress.loaded+=t.loaded;this._progress.total+=t.total},_onDone:function(t,i,r,u){var f=u._progress.total;u._progress.loaded<f&&this._onProgress(n.Event("progress",{lengthComputable:!0,loaded:f,total:f}),u);u.result=t;u.textStatus=i;u.jqXHR=r;this._trigger("done",null,u)},_onFail:function(n,t,i,r){r.jqXHR=n;r.textStatus=t;r.errorThrown=i;this._trigger("fail",null,r);r.recalculateProgress&&(this._progress.loaded-=r._progress.loaded,this._progress.total-=r._progress.total)},_onAlways:function(n,t,i,r){this._active-=1;this._trigger("always",null,r);this._active===0&&this._trigger("stop")},_onSend:function(t,i){i.submit||this._addConvenienceMethods(t,i);var r=this,f,s,e,h,u=r._getAJAXSettings(i),o=function(){return r._sending+=1,u._bitrateTimer=new r._BitrateTimer,f=f||((s||r._trigger("send",t,u)===!1)&&r._getXHRPromise(!1,u.context,s)||r._chunkedUpload(u)||n.ajax(u)).done(function(n,t,i){r._onDone(n,t,i,u)}).fail(function(n,t,i){r._onFail(n,t,i,u)}).always(function(n,t,i){if(r._sending-=1,r._onAlways(n,t,i,u),u.limitConcurrentUploads&&u.limitConcurrentUploads>r._sending)for(var f=r._slots.shift();f;){if(r._getDeferredState(f)==="pending"){f.resolve();break}f=r._slots.shift()}})};return(this._beforeSend(t,u),this.options.sequentialUploads||this.options.limitConcurrentUploads&&this.options.limitConcurrentUploads<=this._sending)?(this.options.limitConcurrentUploads>1?(e=n.Deferred(),this._slots.push(e),h=e.pipe(o)):h=this._sequence=this._sequence.pipe(o,o),h.abort=function(){return(s=[undefined,"abort","abort"],!f)?(e&&e.rejectWith(u.context,s),o()):f.abort()},this._enhancePromise(h)):o()},_onAdd:function(t,i){var c=this,l=!0,u=n.extend({},this.options,i),f=u.limitMultiFileUploads,s=this._getParamName(u),e,h,o,r;if((u.singleFileUploads||f)&&this._isXHRUpload(u))if(!u.singleFileUploads&&f)for(o=[],e=[],r=0;r<i.files.length;r+=f)o.push(i.files.slice(r,r+f)),h=s.slice(r,r+f),h.length||(h=s),e.push(h);else e=s;else o=[i.files],e=[s];return i.originalFiles=i.files,n.each(o||i.files,function(r,u){var f=n.extend({},i);return f.files=o?u:[u],f.paramName=e[r],c._initProgressObject(f),c._addConvenienceMethods(t,f),l=c._trigger("add",t,f)}),l},_replaceFileInput:function(t){var i=t.clone(!0);n("<form><\/form>").append(i)[0].reset();t.after(i).detach();n.cleanData(t.unbind("remove"));this.options.fileInput=this.options.fileInput.map(function(n,r){return r===t[0]?i[0]:r});t[0]===this.element[0]&&(this.element=i)},_handleFileTreeEntry:function(t,i){var e=this,r=n.Deferred(),u=function(n){n&&!n.entry&&(n.entry=t);r.resolve([n])},f;return i=i||"",t.isFile?t._file?(t._file.relativePath=i,r.resolve(t._file)):t.file(function(n){n.relativePath=i;r.resolve(n)},u):t.isDirectory?(f=t.createReader(),f.readEntries(function(n){e._handleFileTreeEntries(n,i+t.name+"/").done(function(n){r.resolve(n)}).fail(u)},u)):r.resolve([]),r.promise()},_handleFileTreeEntries:function(t,i){var r=this;return n.when.apply(n,n.map(t,function(n){return r._handleFileTreeEntry(n,i)})).pipe(function(){return Array.prototype.concat.apply([],arguments)})},_getDroppedFiles:function(t){t=t||{};var i=t.items;return i&&i.length&&(i[0].webkitGetAsEntry||i[0].getAsEntry)?this._handleFileTreeEntries(n.map(i,function(n){var t;return n.webkitGetAsEntry?(t=n.webkitGetAsEntry(),t&&(t._file=n.getAsFile()),t):n.getAsEntry()})):n.Deferred().resolve(n.makeArray(t.files)).promise()},_getSingleFileInputFiles:function(t){t=n(t);var r=t.prop("webkitEntries")||t.prop("entries"),i,u;if(r&&r.length)return this._handleFileTreeEntries(r);if(i=n.makeArray(t.prop("files")),i.length)i[0].name===undefined&&i[0].fileName&&n.each(i,function(n,t){t.name=t.fileName;t.size=t.fileSize});else{if(u=t.prop("value"),!u)return n.Deferred().resolve([]).promise();i=[{name:u.replace(/^.*\\/,"")}]}return n.Deferred().resolve(i).promise()},_getFileInputFiles:function(t){return!(t instanceof n)||t.length===1?this._getSingleFileInputFiles(t):n.when.apply(n,n.map(t,this._getSingleFileInputFiles)).pipe(function(){return Array.prototype.concat.apply([],arguments)})},_onChange:function(t){var r=this,i={fileInput:n(t.target),form:n(t.target.form)};this._getFileInputFiles(i.fileInput).always(function(n){i.files=n;r.options.replaceFileInput&&r._replaceFileInput(i.fileInput);r._trigger("change",t,i)!==!1&&r._onAdd(t,i)})},_onPaste:function(t){var r=t.originalEvent.clipboardData,u=r&&r.items||[],i={files:[]};return n.each(u,function(n,t){var r=t.getAsFile&&t.getAsFile();r&&i.files.push(r)}),this._trigger("paste",t,i)===!1||this._onAdd(t,i)===!1?!1:void 0},_onDrop:function(n){var r=this,t=n.dataTransfer=n.originalEvent.dataTransfer,i={};t&&t.files&&t.files.length&&n.preventDefault();this._getDroppedFiles(t).always(function(t){i.files=t;r._trigger("drop",n,i)!==!1&&r._onAdd(n,i)})},_onDragOver:function(t){var i=t.dataTransfer=t.originalEvent.dataTransfer;if(this._trigger("dragover",t)===!1)return!1;i&&n.inArray("Files",i.types)!==-1&&(i.dropEffect="copy",t.preventDefault())},_initEventHandlers:function(){this._isXHRUpload(this.options)&&(this._on(this.options.dropZone,{dragover:this._onDragOver,drop:this._onDrop}),this._on(this.options.pasteZone,{paste:this._onPaste}));this._on(this.options.fileInput,{change:this._onChange})},_destroyEventHandlers:function(){this._off(this.options.dropZone,"dragover drop");this._off(this.options.pasteZone,"paste");this._off(this.options.fileInput,"change")},_setOption:function(t,i){var r=n.inArray(t,this._refreshOptionsList)!==-1;r&&this._destroyEventHandlers();this._super(t,i);r&&(this._initSpecialOptions(),this._initEventHandlers())},_initSpecialOptions:function(){var t=this.options;t.fileInput===undefined?t.fileInput=this.element.is('input[type="file"]')?this.element:this.element.find('input[type="file"]'):t.fileInput instanceof n||(t.fileInput=n(t.fileInput));t.dropZone instanceof n||(t.dropZone=n(t.dropZone));t.pasteZone instanceof n||(t.pasteZone=n(t.pasteZone))},_create:function(){var t=this.options;n.extend(t,n(this.element[0].cloneNode(!1)).data());this._initSpecialOptions();this._slots=[];this._sequence=this._getXHRPromise(!0);this._sending=this._active=0;this._initProgressObject(this);this._initEventHandlers()},progress:function(){return this._progress},add:function(t){var i=this;t&&!this.options.disabled&&(t.fileInput&&!t.files?this._getFileInputFiles(t.fileInput).always(function(n){t.files=n;i._onAdd(null,t)}):(t.files=n.makeArray(t.files),this._onAdd(null,t)))},send:function(t){if(t&&!this.options.disabled){if(t.fileInput&&!t.files){var e=this,i=n.Deferred(),r=i.promise(),u,f;return r.abort=function(){return(f=!0,u)?u.abort():(i.reject(null,"abort","abort"),r)},this._getFileInputFiles(t.fileInput).always(function(n){f||(t.files=n,u=e._onSend(null,t).then(function(n,t,r){i.resolve(n,t,r)},function(n,t,r){i.reject(n,t,r)}))}),this._enhancePromise(r)}if(t.files=n.makeArray(t.files),t.files.length)return this._onSend(null,t)}return this._getXHRPromise(!1,t&&t.context)}})});
// Lấy keycode
(function (window) {
    var exports = {};
    var codes = exports.codes = {
        'backspace': 8,
        'tab': 9,
        'enter': 13,
        'shift': 16,
        'ctrl': 17,
        'alt': 18,
        'pause/break': 19,
        'caps lock': 20,
        'esc': 27,
        'space': 32,
        'page up': 33,
        'page down': 34,
        'end': 35,
        'home': 36,
        'left': 37,
        'up': 38,
        'right': 39,
        'down': 40,
        'insert': 45,
        'delete': 46,
        'command': 91,
        'left command': 91,
        'right command': 93,
        'numpad *': 106,
        'numpad +': 107,
        'numpad -': 109,
        'numpad .': 110,
        'numpad /': 111,
        'num lock': 144,
        'scroll lock': 145,
        'my computer': 182,
        'my calculator': 183,
        ';': 186,
        '=': 187,
        ',': 188,
        '-': 189,
        '.': 190,
        '/': 191,
        '`': 192,
        '[': 219,
        '\\': 220,
        ']': 221,
        "'": 222
    }

    var aliases = exports.aliases = {
        'windows': 91,
        '⇧': 16,
        '⌥': 18,
        '⌃': 17,
        '⌘': 91,
        'ctl': 17,
        'control': 17,
        'option': 18,
        'pause': 19,
        'break': 19,
        'caps': 20,
        'return': 13,
        'escape': 27,
        'spc': 32,
        'pgup': 33,
        'pgdn': 34,
        'ins': 45,
        'del': 46,
        'cmd': 91
    }

    // kí tự viết thường
    for (i = 97; i < 123; i++) codes[String.fromCharCode(i)] = i - 32

    // số
    for (var i = 48; i < 58; i++) codes[i - 48] = i

    // phím chức năng
    for (i = 1; i < 13; i++) codes['f' + i] = i + 111

    // phím trên bàn phím số
    for (i = 0; i < 10; i++) codes['numpad ' + i] = i + 96

    var names = exports.names = {} 

    
    for (i in codes) names[codes[i]] = i

    
    for (var alias in aliases) {
        codes[alias] = aliases[alias]
    }

    window.KeyCode = exports.codes;
    window.KeyCode.names = exports.names;

    /*
     * sử dụng
     * KeyCode.a = 65
     * tìm kiếm
     * KeyCode.names[65] = 'a'
     */

})(window);


function getColorCode(n){return n?(n.toUpperCase().charCodeAt(0)+randomInt)%10:randomInt%10}function getDefaultAvatar(){egov.mobile&&(title=egov.mobile.avatarTheme);switch(title){case"troll":return String.format(getResource("egov.resources.avatar.troll"),Math.floor(Math.random()*6)+1);case"icon":return String.format(getResource("egov.resources.avatar.icon"),Math.floor(Math.random()*2)+1);case"alphabet":return String.format(getResource("egov.resources.avatar.alphabet"),account[0].toLowerCase());default:return getResource("egov.resources.avatar.noData")}}function getUserAvatar(n,t){return n?String.format(egov.resources.avatar.path,t):getDefaultAvatar()}function getErrorAvatar(){return getResource("egov.resources.avatar.errorUrl")}jQuery.fn.bindResources=function(n,isNotRemoveDataRes){isNotRemoveDataRes=!0;var t=this.find("*[data-res]"),i=this.find("*[data-restitle]"),r=this.find("*[data-respholder]");return(t.length>0||i.length>0||r.length>0)&&($.each(t,function(i,ele){var res=$(ele).attr("data-res"),eleVal,texts;try{eval(res)?(isNotRemoveDataRes||$(ele).removeAttr("data-res"),$(ele).prop("tagName").toLowerCase()==="input"?(eleVal=$(ele).val().trim(),$(ele).val(eval(res))):(eleVal=$(ele).text().trim(),$(ele).text(eval(res)))):console.log(res)}catch(e){texts=JSON.stringify(res).split(".");texts=texts[texts.length-2]+"."+texts[texts.length-1];isNotRemoveDataRes||$(ele).removeAttr("data-res");$(ele).prop("tagName").toLowerCase()==="input"?$(ele).val(texts):$(ele).text(texts);console.log(res)}}),$.each(i,function(i,ele){var res=$(ele).attr("data-restitle"),texts;try{eval(res)&&(isNotRemoveDataRes||$(ele).removeAttr("data-restitle"),$(ele).attr("title",eval(res)))}catch(e){texts=JSON.stringify(res).split(".");texts=texts[texts.length-2]+"."+texts[texts.length-1];isNotRemoveDataRes||$(ele).removeAttr("data-restitle");$(ele).attr("title",texts);console.log(res)}}),$.each(r,function(i,ele){var res=$(ele).attr("data-respholder"),texts;try{eval(res)?(isNotRemoveDataRes||$(ele).removeAttr("data-respholder"),$(ele).attr("placeholder",eval(res))):console.log(res)}catch(e){texts=JSON.stringify(res).split(".");texts=texts[texts.length-2]+"."+texts[texts.length-1];isNotRemoveDataRes||$(ele).removeAttr("data-respholder");$(ele).attr("placeholder",texts);console.log(res)}})),typeof n=="function"&&n(),this};window.getResource=function(resourceKey){try{return eval(resourceKey)}catch(e){return resourceKey}};var extend=function(n,t){for(var i in t)t[i]&&t[i].constructor&&t[i].constructor===Object?(n[i]=n[i]||{},arguments.callee(n[i],t[i])):n[i]=t[i];return n},randomInt=Math.floor(Math.random()*10);

(function () {

    var _LOADMORECLASS = 'sl-load-more';   

    var ScrollLoadMore = function (selector, options) {
        this.options = Object.assign({
            offset: 100,   // Vùng bao xác định scroll thuộc bottom hoặc top tính theo px
            fireDelay: 0,   // Thời gian delay trước khi gọi callback, đặt 0 để bỏ delay.
            direction: 'next' // hướng scroll to load.
        }, options || {});

        this.$container = typeof selector === 'string' ? document.querySelector(selector) : selector;

        this.callback = this.options.callback;

        if (!this.$container || this.$container.hasScrollLoadMore) {
            return;
        }

        this.$container.hasScrollLoadMore = true;

        this._init();
    };

    ScrollLoadMore.prototype._init = function () {
        this.containerBottom = this.$container.getBoundingClientRect().bottom;
        this.containerTop = this.$container.getBoundingClientRect().top;

        this.lastScrollTop = this.$container.scrollTop;
        this.$loadMoreElement = this._ensureLoadmoreElement();

        this.onScrolling = false;
        this.rafId = null;
        this.timer = null;

        this.onFiring = false; 			// Đang chạy callback

        this.$container.addEventListener('scroll', this._scrollHandler.bind(this));
    }

    ScrollLoadMore.prototype._scrollHandler = function () {
        this.timer && clearTimeout(this.timer);
        this.onScrolling = true;

        !this.rafId && this._startChecking();

        this.timer = window.setTimeout(function () {
            this.onScrolling = false;
            this._stopChecking();
        }.bind(this), 250);
    }

    ScrollLoadMore.prototype._scrollFire = function () {
        if (this._hasFire()) {
            this._stopChecking();
            this._callback();
            return;
        }

        this.onFiring = false;
        this.rafId = requestAnimationFrame(this._scrollFire.bind(this));
    }

    ScrollLoadMore.prototype._hasFire = function () {
        var bound = this.$loadMoreElement.getBoundingClientRect();
        return this.options.direction === 'next' ? (bound.bottom <= this.containerBottom + this.options.offset)
					: (bound.bottom >= this.containerTop - this.options.offset);
    }

    ScrollLoadMore.prototype._callback = function () {
        if (this.onFiring || typeof this.callback !== 'function') return;

        (this.onFiring = true) && setTimeout(function () { this.callback(); }.bind(this), this.options.fireDelay);
    }

    ScrollLoadMore.prototype._ensureLoadmoreElement = function () {
        var loadmoreElement = this.$container.getElementsByClassName(_LOADMORECLASS), result;
        if (loadmoreElement && loadmoreElement.length > 0) return loadmoreElement[0];

        result = document.createElement("div");
        result.classList.add(_LOADMORECLASS);
        result.style.height = "1px";
        result.style.width = "100%";
        result.style.background = "transparent";
        result.style.border = "none";

        this.options.direction === 'next' ? this.$container.append(result) : this.$container.prepend(result);
        return result;
    }

    ScrollLoadMore.prototype._startChecking = function () {
        this.onScrolling && (this.rafId = requestAnimationFrame(this._scrollFire.bind(this)));
    }

    ScrollLoadMore.prototype._stopChecking = function () {
        this.rafId && cancelAnimationFrame(this.rafId);
        this.rafId = null;
    }

    window.ScrollLoadMore = ScrollLoadMore;
})();
(function(n,t){function v(n,t,r){var e=n.children(),o=!1,u,s,f;for(n.empty(),u=0,s=e.length;u<s;u++)if(f=e.eq(u),n.append(f),r&&n.append(r),i(n,t)){f.remove();o=!0;break}else r&&r.detach();return o}function o(t,r,u,f,e){var s=!1,h="table, thead, tbody, tfoot, tr, col, colgroup, object, embed, param, ol, ul, dl, blockquote, select, optgroup, option, textarea, script, style",c="script";return t.contents().detach().each(function(){var a=this,l=n(a);if(typeof a=="undefined"||a.nodeType==3&&n.trim(a.data).length==0)return!0;if(l.is(c))t.append(l);else{if(s)return!0;t.append(l);e&&t[t.is(h)?"after":"append"](e);i(u,f)&&(s=a.nodeType==3?y(l,r,u,f,e):o(l,r,u,f,e),s||(l.detach(),s=!0));s||e&&e.detach()}}),s}function y(t,u,e,o,c){var l=t[0],nt,k,d;if(!l)return!1;var y=h(l),tt=y.indexOf(" ")!==-1?" ":"　",p=o.wrap=="letter"?"":tt,a=y.split(p),g=-1,w=-1,b=0,v=a.length-1;for(o.fallbackToLetter&&b==0&&v==0&&(p="",a=y.split(p),v=a.length-1);b<=v&&!(b==0&&v==0);){if(nt=Math.floor((b+v)/2),nt==w)break;w=nt;f(l,a.slice(0,w+1).join(p)+o.ellipsis);i(e,o)?(v=w,o.fallbackToLetter&&b==0&&v==0&&(p="",a=a[0].split(p),g=-1,w=-1,b=0,v=a.length-1)):(g=w,b=w)}return g==-1||a.length==1&&a[0].length==0?(k=t.parent(),t.detach(),d=c&&c.closest(k).length?c.length:0,k.contents().length>d?l=r(k.contents().eq(-1-d),u):(l=r(k,u,!0),d||k.detach()),l&&(y=s(h(l),o),f(l,y),d&&c&&n(l).parent().append(c))):(y=s(a.slice(0,g+1).join(p),o),f(l,y)),!0}function i(n,t){return n.innerHeight()>t.maxHeight}function s(t,i){while(n.inArray(t.slice(-1),i.lastCharacter.remove)>-1)t=t.slice(0,-1);return n.inArray(t.slice(-1),i.lastCharacter.noEllipsis)<0&&(t+=i.ellipsis),t}function u(n){return{width:n.innerWidth(),height:n.innerHeight()}}function f(n,t){n.innerText?n.innerText=t:n.nodeValue?n.nodeValue=t:n.textContent&&(n.textContent=t)}function h(n){return n.innerText?n.innerText:n.nodeValue?n.nodeValue:n.textContent?n.textContent:""}function c(n){do n=n.previousSibling;while(n&&n.nodeType!==1&&n.nodeType!==3);return n}function r(t,i,u){var e=t&&t[0],f;if(e){if(!u){if(e.nodeType===3)return e;if(n.trim(t.text()))return r(t.contents().last(),i)}for(f=c(e);!f;){if(t=t.parent(),t.is(i)||!t.length)return!1;f=c(t[0])}if(f)return r(n(f),i)}return!1}function p(t,i){return t?typeof t=="string"?(t=n(t,i),t.length?t:!1):t.jquery?t:!1:!1}function w(n){for(var t,r=n.innerHeight(),u=["paddingTop","paddingBottom"],i=0,f=u.length;i<f;i++)t=parseInt(n.css(u[i]),10),isNaN(t)&&(t=0),r-=t;return r}var e,l,a;n.fn.dotdotdot||(n.fn.dotdotdot=function(t){var r;if(this.length==0)return n.fn.dotdotdot.debug('No element found for "'+this.selector+'".'),this;if(this.length>1)return this.each(function(){n(this).dotdotdot(t)});r=this;r.data("dotdotdot")&&r.trigger("destroy.dot");r.data("dotdotdot-style",r.attr("style")||"");r.css("word-wrap","break-word");r.css("white-space")==="nowrap"&&r.css("white-space","normal");r.bind_events=function(){return r.bind("update.dot",function(t,u){t.preventDefault();t.stopPropagation();f.maxHeight=typeof f.height=="number"?f.height:w(r);f.maxHeight+=f.tolerance;typeof u!="undefined"&&((typeof u=="string"||u instanceof HTMLElement)&&(u=n("<div />").append(u).contents()),u instanceof n&&(c=u));h=r.wrapInner('<div class="dotdotdot" />').children();h.contents().detach().end().append(c.clone(!0)).find("br").replaceWith("  <br />  ").end().css({height:"auto",width:"auto",border:"none",padding:0,margin:0});var e=!1,l=!1;return s.afterElement&&(e=s.afterElement.clone(!0),e.show(),s.afterElement.detach()),i(h,f)&&(l=f.wrap=="children"?v(h,f,e):o(h,r,h,f,e)),h.replaceWith(h.contents()),h=null,n.isFunction(f.callback)&&f.callback.call(r[0],l,c),s.isTruncated=l,l}).bind("isTruncated.dot",function(n,t){return n.preventDefault(),n.stopPropagation(),typeof t=="function"&&t.call(r[0],s.isTruncated),s.isTruncated}).bind("originalContent.dot",function(n,t){return n.preventDefault(),n.stopPropagation(),typeof t=="function"&&t.call(r[0],c),c}).bind("destroy.dot",function(n){n.preventDefault();n.stopPropagation();r.unwatch().unbind_events().contents().detach().end().append(c).attr("style",r.data("dotdotdot-style")||"").data("dotdotdot",!1)}),r};r.unbind_events=function(){return r.unbind(".dot"),r};r.watch=function(){if(r.unwatch(),f.watch=="window"){var t=n(window),i=t.width(),e=t.height();t.bind("resize.dot"+s.dotId,function(){i==t.width()&&e==t.height()&&f.windowResizeFix||(i=t.width(),e=t.height(),l&&clearInterval(l),l=setTimeout(function(){r.trigger("update.dot")},10))})}else a=u(r),l=setInterval(function(){var n=u(r);(a.width!=n.width||a.height!=n.height)&&(r.trigger("update.dot"),a=u(r))},0);return r};r.unwatch=function(){return n(window).unbind("resize.dot"+s.dotId),l&&clearInterval(l),r};var c=r.contents(),f=n.extend(!0,{},n.fn.dotdotdot.defaults,t),s={},a={},l=null,h=null;return f.lastCharacter.remove instanceof Array||(f.lastCharacter.remove=n.fn.dotdotdot.defaultArrays.lastCharacter.remove),f.lastCharacter.noEllipsis instanceof Array||(f.lastCharacter.noEllipsis=n.fn.dotdotdot.defaultArrays.lastCharacter.noEllipsis),s.afterElement=p(f.after,r),s.isTruncated=!1,s.dotId=e++,r.data("dotdotdot",!0).bind_events().trigger("update.dot"),f.watch&&r.watch(),r},n.fn.dotdotdot.defaults={ellipsis:"... ",wrap:"word",fallbackToLetter:!0,lastCharacter:{},tolerance:0,callback:null,after:null,height:null,watch:!1,windowResizeFix:!0},n.fn.dotdotdot.defaultArrays={lastCharacter:{remove:[" ","　",",",";",".","!","?"],noEllipsis:[]}},n.fn.dotdotdot.debug=function(){},e=1,l=n.fn.html,n.fn.html=function(i){return i!=t&&!n.isFunction(i)&&this.data("dotdotdot")?this.trigger("update",[i]):l.apply(this,arguments)},a=n.fn.text,n.fn.text=function(i){return i!=t&&!n.isFunction(i)&&this.data("dotdotdot")?(i=n("<div />").text(i).html(),this.trigger("update",[i])):a.apply(this,arguments)})})(jQuery);
//# sourceMappingURL=jquery.dotdotdot.min.js.map