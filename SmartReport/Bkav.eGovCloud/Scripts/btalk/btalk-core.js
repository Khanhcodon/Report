// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function (global, undefined) {
    if (global.btalk) return;

    var
		version = "1.0.0",
		author = "Bkav coporation";

    // My library code...
    function noscript(str) {
        if (typeof (str) == 'undefined' || !str || str == '')
            return;

        return str.replace(/<script.*?>.*?<\/script>/igm, '');
    };

    function text(str) {
        if (typeof (str) == 'undefined' || !str || str == '')
            return;

        var result = this.noscript(str);
        var div = document.createElement("div");
        div.innerHTML = result.replace(/(<br\/>|<br>|<\/div>|<\/p>)/igm, '\n').replace(/(<div>|<p>)/igm, '');
        result = div.textContent || div.innerText || "";
        return this.htmlEnc(result);
    };

    function getArgs() {
        var passedArgs = new Array();
        var search = self.location.href;
        search = search.split('?');
        if (search.length > 1) {
            var argList = search[1];
            argList = argList.split('&');
            for (var i = 0; i < argList.length; i++) {
                var newArg = argList[i];
                newArg = argList[i].split('=');
                passedArgs[decodeURIComponent(newArg[0])] = decodeURIComponent(newArg[1]);
            }
        }
        return passedArgs;
    };

    function cutResource(aJID) { // removes resource from a given jid
        if (typeof (aJID) == 'undefined' || !aJID)
            return;
        var retval = aJID;
        if (retval.indexOf("/") != -1)
            retval = retval.substring(0, retval.indexOf("/"));
        return retval;
    };

    function getResource(aJID) { // removes resource from a given jid
        if (typeof (aJID) == 'undefined' || !aJID)
            return;
        var retval = aJID;
        var resource = '';
        if (retval.indexOf("/") != -1)
            resource = retval.substring(retval.indexOf("/") + 1, retval.length);
        return resource;
    };

    function msgEscape(msg) {
        if (typeof (msg) == 'undefined' || !msg || msg == '')
            return;

        msg = msg.replace(/%/g, "%25"); // must be done first

        msg = msg.replace(/\n/g, "%0A");
        msg = msg.replace(/\r/g, "%0D");
        msg = msg.replace(/ /g, "%20");
        msg = msg.replace(/\"/g, "%22");
        msg = msg.replace(/#/g, "%23");
        msg = msg.replace(/\$/g, "%24");
        msg = msg.replace(/&/g, "%26");
        msg = msg.replace(/\(/g, "%28");
        msg = msg.replace(/\)/g, "%29");
        msg = msg.replace(/\+/g, "%2B");
        msg = msg.replace(/,/g, "%2C");
        msg = msg.replace(/\//g, "%2F");
        msg = msg.replace(/\:/g, "%3A");
        msg = msg.replace(/\;/g, "%3B");
        msg = msg.replace(/</g, "%3C");
        msg = msg.replace(/=/g, "%3D");
        msg = msg.replace(/>/g, "%3E");
        msg = msg.replace(/@/g, "%40");

        return msg;
    };

    // fucking IE is too stupid for window names
    function makeWindowName(wName) {
        wName = wName.replace(/@/, "at");
        wName = wName.replace(/\./g, "dot");
        wName = wName.replace(/\//g, "slash");
        wName = wName.replace(/&/g, "amp");
        wName = wName.replace(/\'/g, "tick");
        wName = wName.replace(/=/g, "equals");
        wName = wName.replace(/#/g, "pound");
        wName = wName.replace(/:/g, "colon");
        wName = wName.replace(/%/g, "percent");
        wName = wName.replace(/-/g, "dash");
        wName = wName.replace(/ /g, "blank");
        wName = wName.replace(/\*/g, "asterix");
        return wName;
    };

    function htmlEnc(str) {
        if (!str)
            return '';

        str = str.replace(/&/g, "&amp;");
        str = str.replace(/</g, "&lt;");
        str = str.replace(/>/g, "&gt;");
        str = str.replace(/\"/g, "&quot;");
        return str;
    };

    function htmlDec(str) {
        if (!str)
            return '';

        str = str.replace(/&amp;/g, "&");
        str = str.replace(/&lt;/g, "<");
        str = str.replace(/&gt;/g, ">");
        str = str.replace(/&quot;/g, "\"");
        return str;
    };

    function msgFormat(msg) { // replaces emoticons and urls in a message
        if (!msg)
            return null;

        msg = this.htmlEnc(msg);

        if (typeof (emoticons) != 'undefined') {
            for (var i in emoticons) {
                if (!emoticons.hasOwnProperty(i))
                    continue;
                var iq = i.replace(/\\/g, '');
                var emo = new Image();
                emo.src = emoticonpath + emoticons[i];
                if (emo.width > 0 && emo.height > 0)
                    msg = msg.replace(eval("/\(\\s\|\^\)" + i + "\(\\s|\$\)/g"), "$1<img src=\"" + emo.src + "\" width='" + emo.width + "' height='" + emo.height + "' alt=\"" + iq + "\" title=\"" + iq + "\">$2");
                else
                    msg = msg.replace(eval("/\(\\s\|\^\)" + i + "\(\\s|\$\)/g"), "$1<img src=\"" + emo.src + "\" alt=\"" + iq + "\" title=\"" + iq + "\">$2");
            }
        }

        // replace http://<url>
        msg = msg.replace(/(\s|^)(https?:\/\/\S+)/gi, "$1<a href=\"$2\" target=\"_blank\">$2</a>");

        // replace ftp://<url>
        msg = msg.replace(/(\s|^)(ftp:\/\/\S+)/gi, "$1<a href=\"$2\" target=\"_blank\">$2</a>");

        // replace mail-links
        msg = msg.replace(/(\s|^)(\w+\@\S+\.\S+)/g, "$1<a href=\"mailto:$2\">$2</a>");

        // replace *<pattern>*
        msg = msg.replace(/(\s|^)\*([^\*\r\n]+)\*/g, "$1<b>\$2\</b>");

        // replace _bla_
        msg = msg.replace(/(\s|^)\_([^\*\r\n]+)\_/g, "$1<u>$2</u>");

        msg = msg.replace(/\n/g, "<br>");

        return msg;
    };

    /* isValidJID
    * checks whether jid is valid
    */
    var prohibited = ['"', ' ', '&', '\'', '/', ':', '<', '>', '@']; // invalid chars
    function isValidJID(jid) {
        var nodeprep = jid.substring(0, jid.lastIndexOf('@')); // node name (string before the @)

        for (var i = 0; i < prohibited.length; i++) {
            if (nodeprep.indexOf(prohibited[i]) != -1) {
                alert("Invalid JID\n'" + prohibited[i] + "' not allowed in JID.\nChoose another one!");
                return false;
            }
        }
        return true;
    };

    /* jab2date
    * converts from jabber timestamps to javascript date objects
    * VD: btalk.utils.jab2date("2015-11-28T02:34:34Z"); -> date object
    */
    function jab2date(ts) {
        var date = new Date(Date.UTC(ts.substr(0, 4), ts.substr(5, 2) - 1, ts.substr(8, 2), ts.substr(11, 2), ts.substr(14, 2), ts.substr(17, 2)));
        if (ts.substr(ts.length - 6, 1) != 'Z') { // there's an offset
            var offset = new Date();
            offset.setTime(0);
            offset.setUTCHours(ts.substr(ts.length - 5, 2));
            offset.setUTCMinutes(ts.substr(ts.length - 2, 2));
            if (ts.substr(ts.length - 6, 1) == '+')
                date.setTime(date.getTime() - offset.getTime());
            else if (ts.substr(ts.length - 6, 1) == '-')
                date.setTime(date.getTime() + offset.getTime());
        }
        return date;
    };

    /* hrTime - human readable Time
    * takes a timestamp in the form of 2004-08-13T12:07:04±02:00 as argument
    * and converts it to some sort of humane readable format
    * btalk.utils.jab2date("2015-11-28T02:34:34Z"); -> "11/28/2015, 9:34:34 AM"
    */
    function hrTime(ts) {
        return this.jab2date(ts).toLocaleString();
    };

    /* jabberDate
    * somewhat opposit to hrTime (see above)
    * expects a javascript Date object as parameter and returns a jabber
    * date string conforming to JEP-0082
    * VD: btalk.utils.jabberDate(new Date()); --> "2015-11-28T02:43:34Z"
    */
    function jabberDate(date) {
        if (!date.getUTCFullYear)
            return;

        var jDate = date.getUTCFullYear() + "-";
        jDate += (((date.getUTCMonth() + 1) < 10) ? "0" : "") + (date.getUTCMonth() + 1) + "-";
        jDate += ((date.getUTCDate() < 10) ? "0" : "") + date.getUTCDate() + "T";

        jDate += ((date.getUTCHours() < 10) ? "0" : "") + date.getUTCHours() + ":";
        jDate += ((date.getUTCMinutes() < 10) ? "0" : "") + date.getUTCMinutes() + ":";
        jDate += ((date.getUTCSeconds() < 10) ? "0" : "") + date.getUTCSeconds() + "Z";

        return jDate;
    };

    // Xu ly paste vao div contenteditable tai vi tri con tro/boi chon hien tai
    function insertTextAtCursor(text) {
        var sel, range;
        if (window.getSelection) {
            sel = window.getSelection();
            if (sel.getRangeAt && sel.rangeCount) {
                range = sel.getRangeAt(0);
                range.deleteContents();
                var node = document.createTextNode(text);
                range.insertNode(node);
                // Xu ly chuyen caret ve cuoi doan text vua chen
                range.setStartAfter(node);
                // Fix: for chrome to forcus at end of insert text
                sel.removeAllRanges();
                sel.addRange(range);
            }
        } else if (document.selection && document.selection.createRange) {
            document.selection.createRange().text = text;
        }
    };

    function saveSelection() {
        var rangeOfMsgBox = null;
        if (window.getSelection) {
            var sel = window.getSelection();
            if (sel.getRangeAt && sel.rangeCount) {
                rangeOfMsgBox = sel.getRangeAt(0);
            }
        } else if (document.selection && document.selection.createRange) {
            rangeOfMsgBox = document.selection.createRange();
        }
        return rangeOfMsgBox;
    };

    function restoreSelection(range) {
        if (!range) {
            return;
        }
        if (window.getSelection) {
            var sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
        } else if (document.selection && range.select) {
            range.select();
        }
    };

    function clearSelection() {
        if (window.getSelection) {
            if (window.getSelection().empty) {  // Chrome
                window.getSelection().empty();
            } else if (window.getSelection().removeAllRanges) {  // Firefox
                window.getSelection().removeAllRanges();
            }
        } else if (document.selection) {  // IE?
            document.selection.empty();
        }
    };

    function diffDays(from, to) {
        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
        var oneHour = 60 * 60 * 1000; // minutes*seconds*milliseconds
        return Math.round(Math.abs((from.getTime() - to.getTime()) / (oneHour)));
    };

    // DamBV 


    // [TODO] xem lai cach xu ly culture tren eGov sang day

    // Update DamBV - 10/03/2017:  Hien thi tren chatter
    function getCoolTime(date) {
        var days = ['Chủ nhật', 'T2', 'T3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7'];
        //var days = ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'];

        var now = new Date();
        //var diff = this.diffDays(date, now);
        var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
        var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
        if (date.getDate() === now.getDate()) {
            return hours + ':' + minutes;
        } if (date.getDate() === now.getDate() - 1) {
            return 'Hôm qua';
        } else if (date.getWeek() === now.getWeek()) {
            return days[date.getDay()];
        } else {
            if (date.getFullYear() == now.getFullYear()) {
                return date.getDate() + '/' + (date.getMonth() + 1);
            } else {
                return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
            }
        }
    };

    function getCoolTime2(date) {
        var days = ['Chủ nhật', 'Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7'];
        //var days = ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'];
        var now = new Date();
        var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
        var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
        if (date.getDate() === now.getDate()) {
            return hours + ':' + minutes;
        } else if (date.getDate() === now.getDate() - 1) {
            return "Hôm qua, " + hours + ':' + minutes;
        } else if (date.getWeek() === now.getWeek()) {
            return days[date.getDay()] + ', ' + hours + ':' + minutes;
        } else {
            return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear() + ', ' + hours + ':' + minutes;
        }
    };

    function getCoolDay(date) {
        var days = ['Chủ nhật', 'Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7'];
        //var days = ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'];
        var now = new Date();
        var diff = this.diffDays(date, now);

        if (diff == 0) {
            return 'Hôm nay';
        } else if (diff == 1) {
            return "Hôm qua";
        } else if (date.getWeek() == now.getWeek()) {
            return days[date.getDay()];
        } else if (date.getWeek() < now.getWeek()) {
            return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
        } else {
            // Thoi gian truyen vao sai
        }
    };

    function b64EncodeUnicode(str) {
        return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g, function (match, p1) {
            return String.fromCharCode('0x' + p1);
        }));
    };

    function hex2string(hexx) {
        var hex = hexx.toString();//force conversion
        var str = '';
        for (var i = 0; i < hex.length; i += 2)
            str += String.fromCharCode(parseInt(hex.substr(i, 2), 16));
        return str;
    };

    function getQueryVariable(variable) {
        var query = window.location.search.substring(1);
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == variable) {
                return pair[1];
            }
        }
    };

    function isAlphanumericKeycode(keyCode) {
        if (// 0-9
            (keyCode >= 48 && keyCode <= 57) ||
            // a-z
            (keyCode >= 65 && keyCode <= 90) ||
            // 0-9 numlock
            (keyCode >= 96 && keyCode <= 105)) {
            return true;
        }
        return false;
    }

    // Added DamBV - 14/02/2017 : Viet phan tra ve tuan thu bao nhieu cua ngay.
    Date.prototype.getWeek = function () {
        var onejan = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((((this - onejan) / 86400000) + onejan.getDay() + 1) / 7);
    }

    // Added DambV - 01/03/2017 : Doi tuong Quote
    // user : dambv@bkav,time: 1234533, contentMsg : "huhu"
    function QuoteObject(user, time, contentMsg) {
        this._user = user;
        this._contentMsg = contentMsg;
        this._time = time;
        this._seletor = $('#input_replymsg');
        var timestamp = btalk.getCoolTime2(new Date(this._time));

        var that = this;

        this.showQuote = function () {
            var html = "<table><tr><td><i id='content_msg_reply' class='contentreply'>" + that._contentMsg + "</i></td></tr>"
                           + "<tr><td id='sender_msg' data-time = " + that._time + "  data-sender=" + that._user + " >" + that._user + ", " + timestamp + "</td></tr></table>";
            that._seletor.html(html);
        };

        this.hiddeQuote = function () {
            that._seletor.html('');
        };
    }

    // Public API of btalk
    var btalk = {
        // Library info
        version: version,
        author: author,

        // new btalk.roster.Roster(); from btalk.roster.js
        // Doi tuong quan ly toan bo danh sach roster (ban be) cua user dang dang nhap
        ROSTER: null,

        // Public API
        // Tra ve chuoi da duoc loai bo cac the va noi dung trong no: <script></script>
        noscript: noscript,
        // Tra ve chuoi da duoc ma hoa cac ki tu dac biet
        htmlEnc: htmlEnc,
        htmlDec: htmlDec,
        // Tra ve chuoi sau khi da: noscript + htmlEnc
        text: text,
        getArgs: getArgs,
        cutResource: cutResource,
        getResource: getResource,
        msgEscape: msgEscape,
        makeWindowName: makeWindowName,
        msgFormat: msgFormat,
        isValidJID: isValidJID,
        jab2date: jab2date,
        hrTime: hrTime,
        jabberDate: jabberDate,
        insertTextAtCursor: insertTextAtCursor,
        saveSelection: saveSelection,
        restoreSelection: restoreSelection,
        clearSelection: clearSelection,
        diffDays: diffDays,
        getCoolTime: getCoolTime,
        getCoolTime2: getCoolTime2,
        getCoolDay: getCoolDay,
        b64EncodeUnicode: b64EncodeUnicode,
        hex2string: hex2string,
        getQueryVariable: getQueryVariable,
        isAlphanumericKeycode: isAlphanumericKeycode,
        QuoteObject: QuoteObject,
        temporaryQuote: null,

        listSeenCache: [],

        VIEWED_CACHE_LIST: {},

        // Object view dai dien toan bo giao dien chat. TODO: Tam thoi de o day
        APPVIEW: null,
        // Trang thai co dang forcus vao cua so chat hay khong.
        // Su dung de bao tin nhan moi len tabbar va de danh dau tin chua doc.
        WINDOWFOCUS: false,
        FILETYPE: {
            IMAGE: "image",
            FILE: 'file'
        },
        CHATTYPE: {
            CHAT: "chat",
            GROUPCHAT: "groupchat"
        },
        CONSTANT: {
            DEFAULT_ROOM_NAME: "New group chat"
        },

        // truong trong model chatter xem co phai lam moi lai viec tai file.
        IS_REFRESH_SHARE_FILE: "isRefreshShareFile",

        // Bien danh dau gia tri view anh khong.
        STATUS_PREVIEWIMAGE: false,

        SEARCH: {
            // Biến danh dau xem tim kiếm chưa.
            IS_STATUS_SEARCH: false,

            // Biến kieu search theo ngay hay theo text.
            TYPE_SEARCH: null,

            // Neu top load msg thi se ko scroll bottom nhu hien tai.
            TOP_LOAD_MSG: false,

            // gia tri kiem tra xem co the next /previous ket qua tim kiem nua khong.
            CAN_NEXT_RESULT: true,
            CAN_PREVIOUS_RESULT: true,

            // index ban dau cua cac ket qua tim kiem trong cuoc hoi thoai.
            INDEX_RESULT: 0,
        },

        getSelectionHtml: function () {
            this.CAN_QUOTE = true;
            var html = "";
            if (typeof window.getSelection != "undefined") {
                var sel = window.getSelection();
                if (sel.rangeCount) {
                    var container = document.createElement("div");
                    for (var i = 0, len = sel.rangeCount; i < len; ++i) {
                        container.appendChild(sel.getRangeAt(i).cloneContents());
                    }

                    // Trong TH co 2 tin nhan cua 2 ng thi khong tao quote.
                    var messages = $(container).find('.chat-detail-row-message');
                    for (var i = 0; i < messages.length; i++) {
                        for (var j = i + 1; j < messages.length; j++) {
                            if ($(messages[i]).attr('data-account') != $(messages[j]).attr('data-account')) {
                                this.CAN_QUOTE = false;
                                break;
                            }
                        }
                    }

                    // Lay text
                    var children = $(container).find('.chat-detail-row-message .enableSelectText');
                    if (children && children.length > 1) {
                        var htmlText = "";
                        for (var i = 0 ; i < children.length ; i++) {
                            htmlText = htmlText + children[i].innerHTML + "\n";
                        }
                        html = htmlText;
                    } else {
                        html = container.innerHTML;
                    }
                }
            } else if (typeof document.selection != "undefined") {
                if (document.selection.type == "Text") {
                    html = document.selection.createRange().htmlText;
                }
            }
            return html;
        },

        // kiem tra xem có the tao quote khong.
        CAN_QUOTE: true,

        /*
         * So thanh vien toi da co the gui tin nhan hoac tao nhom 
         * khi thuc hien gui tin hoac tao phong ben cay phong ban
         */
        MAX_MEMBERS: 15,
        isMobile: false
    };

    global.btalk = btalk;
}(typeof window !== "undefined" ? window : this));
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.alert) {
        return;
    }

    btalk.alert = {
        html: '<div class="alert" style="display:none;"> ' +
                '<strong>{title}</strong> {message} ' +
              '</div>',
        instance: null,
        _count: 15,
        _lastcount: 15,
        $count: null,
        callback: null,
        title: "",
        message: "",
        timer: null,
        options: {
            target: "body",
            callbackAfterShow: function () { }
        },

        init: function (options) {
            this.options = $.extend(true, {}, this.options, options);
        },

        startCountdown: function () {
            if (!this.$count) return;

            this._count = this._count || 15;

            if ((this._count - 1) >= 0) {
                this._count = this._count - 1;
                this.$count.text(this._count);
                if (this._count > 0) {
                    this.timer = window.setTimeout(btalk.alert.startCountdown.bind(this), 1000);
                } else {
                    this.callback(this.title, this.message);
                    this.clear();
                }
            }
        },

        make: function (title, message, callback, _type) {
            if (this.instance) {
                this.clear();
            }

            if (!message) {
                return;
            }
            this.title = title || "";
            this.message = message;

            var html = this.html.replace("{title}", this.title).replace("{message}", this.message);
            this.instance = $(html).addClass("alert-" + _type);

            if (typeof callback === 'function') {
                this.callback = callback;
                this.instance.find("a.reconnect").bind("click", function () {
                    this.callback(this.title, this.message);
                    this.clear();
                }.bind(this));
            }

            if (_type === 'success') {
                window.setTimeout(function () {
                    this.clear();
                }.bind(this), 5000);
            }

            this.$count = this.instance.find("a.count");
            if (this.$count && parseInt(this.$count.text()) > 0) {
                this._count = parseInt(this.$count.text()) > 300 ? 300 : parseInt(this.$count.text());
            }
        },

        show: function () {
            if (this.instance) {
                $(this.options.target).append(this.instance);
                this.instance.show();
                this.startCountdown();
                this.options.callbackAfterShow();
            }
        },

        info: function (title, message, callback) {
            /*
            <div class="alert alert-info">
                <strong>Info!</strong> Indicates a neutral informative change or action.
            </div>
            */
            title = title || "";
            this.make(title, message, callback, "info");
            this.show();
        },

        warning: function (title, message, callback) {
            /*
            <div class="alert alert-warning">
                <strong>Warning!</strong> Indicates a warning that might need attention.
            </div>
            */
            title = title || "";
            this.make(title, message, callback, "warning");
            this.show();
        },

        error: function (title, message, callback) {
            /*
            <div class="alert alert-error">
                <strong>Warning!</strong> Indicates a warning that might need attention.
            </div>
            */
            title = title || "";
            this.make(title, message, callback, "error");
            this.show();
        },

        success: function (title, message, callback) {
            /*
            <div class="alert alert-success">
                <strong>Success!</strong> Indicates a successful or positive action.
            </div>
            */
            title = title || "";
            this.make(title, message, callback, "success");
            this.show();
        },

        danger: function () {
            /*
            <div class="alert alert-danger">
                <strong>Danger!</strong> Indicates a dangerous or potentially negative action.
            </div>
            */
        },

        clear: function () {
            window.clearTimeout(this.timer);
            this.callback = null;
            $('div.alert').remove();
            this.instance = null;
        }
    };
})(window.jQuery, window.btalk = window.btalk || {});
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.reconnection) {
        return;
    }

    btalk.reconnection = {
        _count: 3,
        timer: null,
        callback: null,
        connection: function (callback, type) {
            if (type === 'success') {
                this.clear();
            }
            if (typeof callback === 'function') {
                this.callback = callback;
            }
        },

        startCountdown: function () {
            this._count = this._count || 3;
            if ((this._count - 1) >= 0) {
                this._count = this._count - 1;
                if (this._count > 0) {
                    this.timer = window.setTimeout(btalk.reconnection.startCountdown.bind(this), 1000);
                } else {
                    if (typeof this.callback === 'function') {                        
                        this.callback();                        
                    }
                    this.clear();
                }
            }
        },

        reconnectionError: function (callback) {
            this.connection(callback, "error");
            this.startCountdown();

        },

        reconnectionSuccess: function (callback) {
            this.connection(callback, "success");
            this.startCountdown();
        },

        clear: function () {
            window.clearTimeout(this.timer);
            this.callback = null;
        }
    };
})(window.jQuery, window.btalk);
(function ($, btalk) {
    /*
        Đọc thêm các thông tin về gói tin xmpp ở đây
        https://xmpp.org/rfcs/rfc3921.html
    */

    if (typeof window.JSJaCMessage === "undefined") {
        throw "Chưa tải thư viện jsjac.js";
    }

    var NS_RECEIVED = "urn:xmpp:receipts";

    var MessageBuilder = {
        _message: null,
        init: function (id) {
            this._message = new window.JSJaCMessage(id);

            this._message.JSON = function () {
                return $.xml2json(this.doc);
            }

            return this;
        },

        setId: function (jid) {
            this._message.setID(jid);
            return this;
        },

        setType: function (type) {
            this._message.setType(type);
            return this;
        },

        setFrom: function (from) {
            this._message.setFrom(from);
            return this;
        },

        setTo: function (to) {
            this._message.setTo(to);
            return this;
        },

        setBody: function (body) {
            this._message.setBody(body);
            return this;
        },

        setAttr: function (name, value) {
            this._message.getNode().setAttribute(name, value);
            return this;
        },

        appendChild: function (name, attributes, chilren) {
            if (String.isNullOrEmpty(name)) {
                return this;
            }

            var childNode = this._message.getDoc().createElement(name);
            if (attributes && typeof attributes === 'object') {
                _.each(_.keys(attributes), function (name) {
                    childNode.setAttribute(name, attributes[name]);
                });
            }

            if (chilren && typeof chilren === 'object') {

            }

            this._message.getNode().appendChild(childNode);
            return this;
        },

        get: function () {
            this.setAttr('clienttime', new Date().getTime().toString())
            return this._message;
        }
    };

    var IQMessageBuilder = {
        _iqmessage: null,
        _newElement: null,

        init: function (id, type, to) {
            this._iqmessage = new window.JSJaCIQ();
            this.setIq(id, type, to);

            this._iqmessage.JSON = function () {
                return $.xml2json(this.doc);
            }

            return this;
        },

        setIq: function (id, type, to) {
            type = type || 'get';
            to = to || null;

            this._iqmessage.setIQ(to, type, id);
            return this;
        },

        appendChild: function (name, attributes, value) {
            this._newElement = this._iqmessage.buildNode(name, attributes, value);
            this._iqmessage.getNode().appendChild(this._newElement);

            return this;
        },

        appendNodeChild: function (parentName, name, attributes, value) {
            if (String.isNullOrEmpty(parentName)) return this;

            var parentNode = this._iqmessage.getNode().getElementsByTagName(parentName).item(0);
            if (!parentName) return this;

            this._newElement = this._iqmessage.buildNode(name, attributes, value);
            parentNode.appendChild(this._newElement);

            return this;
        },

        setQuery: function (query) {
            this._iqmessage.setQuery(query);
            return this;
        },

        setType: function (type) {
            this._iqmessage.setType(type);
            return this;
        },

        get: function () {

            return this._iqmessage;
        }
    };

    btalk.messageFactory = {
        _messageBase: function (id, from, to, type) {
            var result = MessageBuilder.init(id);
            result.setId(id)
                .setFrom(from)
                .setTo(to)
                .setType(type);

            return result;
        },

        _iqMessage: function (id, type, to) {
            return IQMessageBuilder.init(id, type, to);
        },

        //#region Chat Message Request

        sendMessage: function (id, from, to, type, body, quote) {
            /*
             * Tạo gói tin gửi tin nhắn đi, bao gồm các tin nhắn chat thông thường
                <message xmlns="jabber:client" id="1534740038052" from="bkav.tienbv@sotttt.tayninh.gov.vn" to="bkav.cuongnt@sotttt.tayninh.gov.vn" type="chat" clienttime="1534740038052">
                    <body>i</body>
                    <quote></quote>
                </message>
             */
            var message = new this._messageBase(id, from, to, type);
            message.setBody(body);

            if (quote && typeof quote === 'object') {
                message.appendChild('quote', quote);
            }

            return message.get();
        },

        sendStatusMessage: function (id, from, to, type, status, secs) {
            /*
             * Tạo gói tin gửi trạng thái gửi nhận tin nhắn

             */

            var message = new this._messageBase(id, from, to, type);
            message.appendChild("received", { xmlns: NS_RECEIVED, id: id, status: status, secs: secs });

            return message.get();
        },

        sendComposingMessage: function (id, from, to, type) {
            /*
             * Tạo gói tin gửi trạng thái đang gõ.
             <message xmlns="jabber:client" id="1534740667603" from="bkav.tienbv@sotttt.tayninh.gov.vn" to="bkav.dambv@sotttt.tayninh.gov.vn" type="chat" clienttime="1534740667603">
                <composing xmlns="http://jabber.org/protocol/chatstates" jid="bkav.tienbv@sotttt.tayninh.gov.vn"/>
             </message>
             */

            var message = new this._messageBase(id, from, to, type);
            message.appendChild('composing', { 'xmlns': 'http://jabber.org/protocol/chatstates', 'jid': from });

            return message.get();
        },

        sendConfigMessage: function (id, from, to, type, configs) {
            /*
             * Gói tin gửi trạng thái chung của room chat: thay đổi biệt hiệu, thời điểm bắt đầu một cuộc hội thoại, thay đổi tên nhóm, ...
                <message xmlns="jabber:client" id="1534740304930" from="bkav.tienbv@sotttt.tayninh.gov.vn" to="bkav.cuongnt@sotttt.tayninh.gov.vn" type="chat" clienttime="1534740304931">
                    <body>Room Config</body>
                    <config xmlns="" key="edit_by" value="bkav.tienbv@sotttt.tayninh.gov.vn"/>
                    <config xmlns="" key="start_conversion" value="1534740304930"/>
                </message>
             */

            if (!configs) return null;

            var message = new this._messageBase(id, from, to, type);
            message.setBody("Room Config");

            for (var i in configs) {
                var config = {};
                config[configs[i].key] = configs[i].value;
                message.appendChild('config', config);
            }

            return message.get();
        },

        sendAttachmentMessage: function (id, from, to, type, attachments) {
            /*
             * Tạo gói tin gửi file đính kèm
             */
            var msgBody = "";
            var fileType = "file";

            if (!attachments) return;

            var message = new this._messageBase(id, from, to, type);

            for (var i in attachments) {
                msgBody = msgBody + attachments[i].name;

                if (i < _.keys(attachments).length - 1) {
                    msgBody = msgBody + this.options.file.seperatekey;
                }
            }
            message.setBody(msgBody);

            if (attachments[_.keys(attachments)[0]].type.indexOf('image') > -1) {
                fileType = "image";
            }
            message.setAttr('msgContentType', fileType);

            _.each(attachments, function (file) {
                // attachment
                var attrs = {
                    'id': file.id, 'name': file.name, 'object': file.object, 'type': file.type, 'percentage': 101,
                    'messageid': file.messageid, 'sentDate': file.sentDate, 'fileServerType': file.fileServerType
                };

                file.size && parseInt(file.size) && (attrs['size'] = file.size);
                file.lastModified && (attrs['lastModified'] = file.lastModified);
                file.lastModifiedDate && (attrs['lastModifiedDate'] = file.lastModifiedDate);
                file.tenantid && (attrs['tenantid'] = file.tenantid);
                file.dimension && (attrs = _.extend(attrs, file.dimension));

                message.appendChild('attachment', attrs);
            });

            return message.get();
        },

        //#endregion

        //#region Roster, history, online Request

        getRosterIqRequest: function () {
            // <iq xmlns="jabber:client" type="get" id="roster_1"><query xmlns="jabber:iq:roster"/></iq>

            var id = 'roster_1', type = 'get';
            var result = new this._iqMessage(id, type);
            result.setQuery('jabber:iq:roster');

            return result.get();
        },

        getOnlineIqRequest: function () {
            // <iq xmlns="jabber:client" type="set" id="enable_auto_send_presence1532505128399"><mobile xmlns="http://btalk.vn/protocol/btalk_mobile#v2" enable="false"/></iq>

            var id = 'enable_auto_send_presence' + new Date().getTime().toString();
            var type = 'set';
            var result = new this._iqMessage(id, type);

            var xmlns = 'http://btalk.vn/protocol/btalk_mobile#v2';
            result.appendChild('mobile', { enable: 'false', 'xmlns': xmlns });

            return result.get();
        },

        getHistoryIqRequest: function (start, skip, take) {
            // <iq xmlns="jabber:client" type="get" id="jwchat_history"><list xmlns="urn:xmpp:archive" start="2015-01-01T00:00:00Z"><set xmlns="http://jabber.org/protocol/rsm"><max xmlns="">15</max><after xmlns="">15</max></set></list></iq>

            var id = 'jwchat_history', type = 'get';
            var result = new this._iqMessage(id, type);
            var xmlns = 'urn:xmpp:archive';
            var nodeName = 'list';
            skip = skip || 0;
            take = take || 10;

            result.appendChild(nodeName, { 'start': start, 'xmlns': xmlns })
                    .appendNodeChild(nodeName, 'set', { xmlns: 'http://jabber.org/protocol/rsm' })
                    .appendNodeChild('set', 'max', null, take)
                    .appendNodeChild('set', 'after', null, skip);

            return result.get();
        },

        GetMembersIqRequest: function (groupId) {
            // <iq id='member3' type='get' to='dambv1479888134772@conference.bkav.com'> <query xmlns='http://jabber.org/protocol/muc#admin'><item affiliation='owner'/></query></iq>

            var id = "get_members" + new Date().getTime().toString();
            var type = 'get';
            var to = btalk.cutResource(groupId);

            var result = new this._iqMessage(id, type, to);
            result.appendChild('query', { xmlns: "http://jabber.org/protocol/muc#admin" })
                    .appendNodeChild('query', 'item', { affiliation: 'owner' });

            return result.get();
        },

        GetMessageHistoriesIqRequest: function (jid, type, start, skip, take) {
            /*
                Build và trả về gói tin lấy danh sách tin nhắn có phân trang
                <iq xmlns="jabber:client" type="get" id="jwchat_get_history">
                    <retrieve xmlns="urn:xmpp:archive" start="2015-01-01T00:00:00Z" with="bkav.dambv@sotttt.tayninh.gov.vn">
                        <set xmlns="http://jabber.org/protocol/rsm">
                            <max xmlns="">30</max>
                            <after xmlns="">60</after>
                        </set>
                    </retrieve>
                </iq>
            */
            var id = 'jwchat_get_history';
            var xmlns = 'urn:xmpp:archive';
            skip = skip || 0;
            take = take || 30;

            var result = new this._iqMessage(id, 'get');
            var nodeName = type === btalk.CHATTYPE.GROUPCHAT ? 'groupchat' : 'retrieve';

            result.appendChild(nodeName, { xmlns: 'urn:xmpp:archive', start: start, 'with': jid })
                    .appendNodeChild(nodeName, 'set', { xmlns: 'http://jabber.org/protocol/rsm' })
                    .appendNodeChild('set', 'max', null, take)
                    .appendNodeChild('set', 'after', null, (skip - 1) + ''); // -1 do index bắt đầu từ 0

            return result.get();
        },

        //#endregion
    };
})
($, window.btalk = window.btalk || {});
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.auth) {
        return;
    }

    btalk.auth = {
        options: {
            xmppTokenKey: "bkavAuthen",
            keystoneTokenKey: "keystoneAuth",
            loginPage: "index.html",
            authorizedPage: "jwchat.html",
            // For read xmpp token
            rXmppToken: { domain: '', path: '/' }, //{ path:'/' },
            // For read keystone token
            rKeystoneToken: { domain: '', path: '/' }, //{ path:'/'},
            // For write xmpp token
            wXmppToken: { expires: 7, domain: '', path: '/' }, //{ expires: 7, path: '/' },
            // For write keystone token
            wKeystoneToken: { expires: 7, domain: '', path: '/' },//{ expires: 7, path: '/' },
            // Domain of user is using when logining to xmpp server
            domain: "",
            FILE_ACTIVE: false,
            remember: false
        },

        keystoneAuth: false,
        xmppAuth: false,

        init: function (options) {
            this.options = $.extend(true, {}, this.options, options);
        },

        // trang thai danh dau da khoi tao hay chua
        isReadyCm: false,
        _initCm: function (debugJid) {
            if (this.isReadyCm == true) {
                return;
            }

            this.isReadyCm = true;
            btalk.cm._init(this.getJID() || debugJid, btalk.config.CM);
        },

        // Dam bao chi goi lay ban be, lich su chat... 1 lan duy nhat, ngay ca khi mat ket noi va reconnect lai.
        isReadyCmRoster: false,
        _initCmRoster: function (success) {
            if (this.xmppAuth != true) {
                return;
            }
            if (this.isReadyCmRoster == true) {
                if (typeof success === 'function') {
                    success();
                }
                return;
            }
            this.isReadyCmRoster = true;

            // Phao goi ham nay o day ma khong goi trong btalk.connectionManager.js duoc
            // de dam bao chi goi khi login lan dau. Cac lan reconnect do may ket noi thi khong can goi.
            // btalk.cm.getRoster();

            if (typeof success === 'function') {
                success();
            }
        },

        /**
        * Dang nhap
        *
        * Note: kich ban hien tai dang chap nhan co the login file server khong thanh cong
        */
        firstLogin: true,
        login: function (username, password, remember, error) {
            this.options.remember = remember || false;

            var _debugJid = (username.indexOf("@") == -1) ? username + "@" + this.options.domain : username;
            // Goi sau khi gan _jid
            this._initCm(_debugJid);

            error = error || function () { };

            var successKeystone = function () {
                this.keystoneAuth = true;
            };

            /*
             * CuongNT - 24/12/2015: Them de handle goi tin <success ...
             * de lay token sau khi login thanh cong ban username/pass
             */
            var handleTokenXmpp = function (success) {
                // Fix lỗi trên ie8 không có textContent
                var token = success.textContent || success.text || success.innerText;
                if (token) {
                    // TODO: Cho nay dung ham b64decode trong jsjac.... can xem dung ham khac
                    // save xmpp token to cookie
                    this.saveXmppToken(b64decode(token));
                } else {
                    error("Server khong tra ve token, hoac loi khi ghi token ra cookie.");
                }
            };

            // Xay ra sau handleTokenXmpp.
            var successXmpp = function () {
                var token = this.getXmppToken();
                if (token && token.length > 0) {
                    this.xmppAuth = true;
                    // Lam nhiem vu duy nhat la chuyen vao giao dien hoi thoai
                    window.location.href = this.options.authorizedPage;
                } else {
                    error("Server khong tra ve token, hoac loi khi ghi token ra cookie.");
                }
            };

            // Neu la lan dau thi gan hanlde
            if (this.firstLogin == true) {
                this.firstLogin = false;

                btalk.cm.login(username, password, successXmpp.bind(this), error, handleTokenXmpp.bind(this));
            }
            else {
                btalk.cm.login(username, password);
            }
        },

        firstReconnect: true,
        firstFileReconnect: true,
        reconnect: function (success, error) {
            this._initCm();

            error = error || function () { };
            success = success || function () { };

            if (this.hasXmppToken()) {
                // callback, goi khi reconnect thanh cong
                var successXmpp = function () {
                    this.xmppAuth = true;
                    this._initCmRoster(success);

                    /*
                    TamDN - 30/6/2017 - Khởi tạo cache danh sách đã xem
                    */
                    btalk.VIEWED_CACHE_LIST = new btalk.cache.CacheManager();

                    // neu co kich hoat tinh nang gui file
                    if (this.options.FILE_ACTIVE === true) {
                        var fmOps = {};
                        // Neu la lan dau thi gan hanlde
                        if (this.firstFileReconnect == true) {
                            this.firstFileReconnect = false;
                        }
                        else {
                            // login server file
                            //fmOps = $.extend({}, btalk.config.FILESERVER, { tenant: this.getJID().split("@")[0] });
                            //btalk.fm._init(fmOps);
                            //btalk.fm.reconnect(this.getXmppToken(), successKeystone.bind(this));
                        }
                    }
                };

                // callback, goi khi Loi khi reconnect
                var errorXmpp = function (e) {
                    // Tra xu ly loi cho giao dien
                    error(e);
                    // Xong van dam bao chac chan da logout neu la loi "Authorization failed"
                    switch (e.getAttribute('code')) {
                        case '401':
                            this.logout();
                            return false;
                            break;
                    }
                };

                var successKeystone = function () {
                    this.keystoneAuth = true;

                    // Gan lai token cho keystone theo token eGov
                    // TODO: De tam de test
                    window.JSTACK.Keystone.params.token = this.getXmppToken();
                };

                // Neu la lan dau thi gan hanlde
                if (this.firstReconnect == true) {
                    this.firstReconnect = false;
                    // Thuc hien reconnect                   
                    btalk.cm.reconnect(this.getXmppToken(), successXmpp.bind(this), errorXmpp.bind(this));
                }
                    // Nguoc lai thi khong
                else {
                    btalk.cm.reconnect(this.getXmppToken());
                }
            } else {
                this.logout();
                return false;
            }
        },

        logout: function () {
            this.xmppAuth = false;
            return;
            btalk.cm.logout();
            // neu co kich hoat tinh nang gui file
            if (this.options.FILE_ACTIVE === true) {
                btalk.fm.logout();
            }
            this._removeToken();
            // TODO: Kiem tra de khong redirect lap cho nay
            if (this.options.loginPage) {
                window.location.href = this.options.loginPage;
            }
        },

        available: function () {
            btalk.cm.changeStatus("available");
        },

        unavailable: function () {
            btalk.cm.changeStatus('offline');
        },

        hasXmppToken: function () {
            var token = this.getXmppToken();
            return token && token.length > 0;
        },

        getExpiresOfXmppToken: function () {
            return this.options.remember ? this.options.wXmppToken : { expires: null };
        },

        saveXmppToken: function (token) {
            $.cookie(this.options.xmppTokenKey, token, this.getExpiresOfXmppToken());
        },

        getXmppToken: function () {
            return $.cookie(this.options.xmppTokenKey);
        },

        getExpiresOfKeystoneToken: function () {
            return this.options.remember ? this.options.wKeystoneToken : { expires: null };
        },

        saveKeystoneToken: function (token) {
            $.cookie(this.options.keystoneTokenKey, token, this.getExpiresOfKeystoneToken());
        },

        getKeystoneToken: function () {
            return $.cookie(this.options.keystoneTokenKey);
        },

        _removeToken: function () {
            //$.cookie(this.options.xmppTokenKey, "", this.options.rXmppToken);
            //$.cookie(this.options.keystoneTokenKey, "", this.options.rKeystoneToken);
        },

        /** Tra ve account@domain cua tai khoan dang nhap hien tai (khong gom resource) */
        getJID: function () {
            var token = this.getXmppToken();
            if (token && token.length > 0) {
                return this.findKey(token, 'user').toLowerCase();
            } else {
                return "";
            }
        },

        findKey: function (token, key) {
            var tokenArr = btalk.hex2string(token.split('_')[2]).split(';');
            for (var i = 0; i < tokenArr.length; i++) {
                if (tokenArr[i].split('=')[0] == key &&
                    tokenArr[i].split(':').length == 2) {
                    return tokenArr[i].split(':')[1];
                }
            }
            return "";
        },

        /** Tra ve account@domain/resource cua tai khoan dang nhap hien tai */
        getFullJID: function () {
            return this.getJID + '/' + this.getResource();
        },

        /** Tra ve resource cua tai khoan dang nhap hien tai */
        getResource: function () {
            return btalk.cm.getResource();
        }
    };
})(window.jQuery, window.btalk = window.btalk || {});
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.cm) return;

    btalk.cm = {

        //#region config

        JSJACCON: null,
        DEBUG: null,
        disco: null,
        // JID cac dich vu server ho tro:
        // JID cua dich vu luu tru tin nhan (message archive)
        // ARCHIVE_SERVICE_JID: undefined,
        // JID cua dich vu chat nhom, vi du: conference.bmail.vn
        MUC_SERVICE_JID: undefined,
        options: {
            debug: {
                // Custome debug
                Debugger: null,
                // turn debugging on
                active: false,
                // if true only DEBUGJID gets the debugger
                useDebugJid: false,
                // which user get's debug messages
                debugJid: "admin@btalk.vn",
                // debug-level 0..4 (4 = very noisy)
                debugLvl: 2
            },

            xmpp: {
                connect_port: 5222,
                connect_host: "192.168.140.133",
                connect_secure: true,
                domain: "",
                conference: "",
                backendType: "binding",
                defaultResource: "btalk",
                httpbase: "/bosh/",
                baseDatetimeQuery: '2015-01-01T00:00:00Z',
                chatterNextCounts: 15,
                messageNextCounts: 40
            },

            cookie: {
                expiredDay: 7,
                // br: btalk resource
                resourceKey: "br"
            },

            client: {
                version: ""
            },

            file: {
                seperatekey: "!@!"
            },

            loginPage: "index.html"
        },

        // saved states
        savedStates: {
            onlstat: '',
            onlmsg: '',
            onlprio: '8',
            openedDepartments: []
        },

        // saved preferences
        savedPrefs: {
            timerval: 30 * 1000, // 30s
            autoPopup: true,
            autoPopupAway: false,
            playSounds: true,
            forcusWindows: true,
            timestamps: false,
            enablelog: false
        },

        onlineStatus: {
            "available": "online",
            "chat": "free for chat",
            "away": "away",
            "xa": "not available",
            "dnd": "do not disturb",
            "invisible": "invisible",
            "unavailable": "offline"
        },

        // Trang thai da goi ham logout() chua
        logoutCalled: false,
        _events: [],
        isReady: false,

        //#endregion

        // Duoc goi trong btalk.auth.js
        _init: function (debugJid, options) {
            if (this.isReady) { return; }
            this.isReady = true;

            this.options = $.extend(true, {}, this.options, options);

            /* initialise debugger */
            if (!this.DEBUG || typeof (this.DEBUG) == 'undefined' || !this.DEBUG.start) {
                if (this.options.debug.active && (!this.options.debug.useDebugJid || this.options.debug.debugJid == debugJid)) {
                    if (typeof (this.options.debug.Debugger) != 'undefined') {
                        this.DEBUG = new this.options.debug.Debugger(this.options.debug.debugLvl, 'Btalk ' + debugJid);
                        this.DEBUG.start();
                    } else {
                        this.DEBUG = new window.JSJaCConsoleLogger(this.options.debug.debugLvl);
                    }
                } else {
                    this.DEBUG = {
                        log: function () {
                        }
                    };
                }
            }

            /* create new connection */
            var oArg = {
                oDbg: this.DEBUG,
                httpbase: new URL(egov.connections.ChatLink).origin + '/bosh/', // this.options.xmpp.httpbase,
                timerval: this.savedPrefs.timerval
            };

            if (this.options.xmpp.backendType == 'binding')
                this.JSJACCON = new window.JSJaCHttpBindingConnection(oArg);
            else
                this.JSJACCON = new window.JSJaCHttpPollingConnection(oArg);

            this.JSJACCON.registerHandler('presence', this.handlePresence.bind(this));

            this.JSJACCON.registerHandler('ondisconnect', this.handleDisconnect.bind(this));
            this.JSJACCON.registerHandler('onconnect', this.handleConnected.bind(this));
            this.JSJACCON.registerHandler('onerror', this.handleConError.bind(this));
            this.JSJACCON.registerHandler('ontoken', this.handleToken.bind(this));

            this.JSJACCON.registerHandler('message', this.handleReceiveMessage.bind(this));
        },

        //#region CONNECT TO SERVER & LOGIN - BAT DAU QUA TRINH KET NOI SERVER & DANG NHAP *

        handleConError: function (e) {
            /*
             * Xu ly tat cac cac loi trong qua trinh ket noi toi server chat
             */

            this._handleEvent('onerror', e);
            switch (e.getAttribute('code')) {
                case '401':
                    // Authorization Failed
                    if (!this.JSJACCON.connected()) {
                        this._handleEvent('con_error_authorization_failed');
                    }
                    break;
                case '402':
                    // Invalid SID: qua thoi gian toi da giu ket noi BOSH giua client, server
                    this._handleEvent('con_error_invalid_sid_error');
                    break;
                case '409':
                    // Registration failed!\n\nPlease choose a different username!
                    this._handleEvent('con_error_conflict_name');
                    break;
                case '503':
                    // Service unavailable
                    this._handleEvent('con_error_xmpp_server_unavailable');
                    break;
                case '500':
                    // Internal Server error
                    if (!this.JSJACCON.connected() && !this.logoutCalled && this.savedStates.onlstat != 'offline') {
                        this._handleEvent('con_error_internal_server_error');
                    }
                    break;
                default:
                    // this shouldn't happen :)
                    this._handleEvent('con_error_default');
                    break;
            }
        },

        handleDisconnect: function () {
            /*
             * Xu ly mat ket noi toi server XMPP
             */

            if (this.logoutCalled || this.savedStates.onlstat == 'offline')
                return;
            this._handleEvent('ondisconnect');
        },

        handleConnected: function (iq) {
            /** Xu ly khi tao ket noi toi server XMPP thanh cong, tuc:
             * - Xay ra sau handleToken neu co kich hoat auth bang token
             * - Xay ra khi bind resource thanh cong len server
             */
            // btalk.auth cung handle su kien nay va lam cac viec sau:
            // 1. Khoi tao btalk.ROSTER
            // 2. Khoi tao btalk.ROSTER.currentuser
            // 4. Khoi tao AppView.js tren mainApp (main.js)

            this._handleEvent('onconnect');
            this.DEBUG.log("Connected", 0);
        },

        handleToken: function (token) {
            // Xay ra truoc handleConnected, va sau khi auth thanh cong bang type="Token"

            this._handleEvent('ontoken', token);
        },

        login: function (username, password, success, error, token) {
            // username, pass null or "" then return
            if (!username || !password) {
                return;
            }

            if (success && typeof success == 'function') {
                this.registerHandler('onconnect', success);
                // CuongNT - 24/12/2015: Tu them de lay token sau khi login bang username/pass
                this.registerHandler('ontoken', token);
            }

            if (error && typeof error == 'function') {
                this.registerHandler('onerror', error);
            }

            // TODO: resource sinh random theo tieu chi nao do va luu vao cookie
            var userDomain = this.options.xmpp.domain;
            if (username.indexOf("@") != -1) {
                var params = username.split("@");
                username = params[0];
                userDomain = params[1];
            }
            var oArg = {
                domain: userDomain,
                username: username,
                pass: password,
                resource: this.getResource(),
                register: false,

                version: this.options.client.version,
                allow_plain: true,
                allow_token: false
            };

            if (this.options.xmpp.backendType == 'binding') {
                oArg.port = this.options.xmpp.connect_port;
                oArg.host = this.options.xmpp.connect_host;
                oArg.secure = this.options.xmpp.connect_secure;
            }
            this.JSJACCON.connect(oArg);
        },

        reconnect: function (token, success, error) {
            if (success && typeof success == 'function') {
                this.registerHandler('onconnect', success);
            }

            if (error && typeof error == 'function') {
                this.registerHandler('onerror', error);
            }

            // [TODO] resource lay tu cookie.
            var oArg = {
                domain: this.options.xmpp.domain,
                token: token,
                resource: this.getResource(),
                register: false,

                version: this.options.client.version,
                allow_plain: false,
                allow_token: true
            };

            if (this.options.xmpp.backendType == 'binding') {
                oArg.port = this.options.xmpp.connect_port;
                //oArg.host = this.options.xmpp.connect_host;
                oArg.secure = this.options.xmpp.connect_secure;
            }

            this.JSJACCON.connect(oArg);
        },

        //TODO can sua de co the mo 2 tab tren 1 trinh duyet khong bi conflict
        getResource: function () {
            var resource = $.cookie(this.options.cookie.resourceKey);
            if (!resource) {
                resource = btalk.browser.OSName + "_" + btalk.browser.browserName
                    + "_" + btalk.browser.fullVersion + Date.now();
                $.cookie(this.options.cookie.resourceKey, resource, this.options.cookie.expiredDay);
            }
            return resource || this.options.xmpp.defaultResource;
        },

        /** Ket thuc qua trinh login */
        _handle_logined: function () {
            this._handleEvent('_handle_logined');
        },

        /**
         * @states: trang thai su dung cuoi cung
         * @prefs: thiet lap cau hinh cuoi cung
         */
        logout: function (states, prefs) {
            this.logoutCalled = true;

            if (!this.JSJACCON.connected())
                return;

            /* save state */
            var iq = new window.JSJaCIQ();
            iq.setIQ(null, 'set');
            var query = iq.setQuery('jabber:iq:private');
            var aNode = query.appendChild(iq.buildNode('jwchat', {
                'xmlns': 'jwchat:state'
            }));
            if (states) {
                // save presence
                if (states.onlstat && states.onlstat != 'offline')
                    aNode.appendChild(iq.buildNode('presence', states.onlstat));

                // save status message
                if (states.onlmsg && states.onlmsg != '')
                    aNode.appendChild(iq.buildNode('onlmsg', states.onlmsg));

                // save department tree state
                if (states.openedDepartments && states.openedDepartments.length > 0) {
                    var _openeddepartments = '';
                    for (var i in states.openedDepartments)
                        _openeddepartments += i + ",";
                    if (_openeddepartments != '')
                        aNode.appendChild(iq.buildNode('openedDepartments', _openeddepartments));
                }
            }
            this.DEBUG.log(iq.xml(), 2);
            this.JSJACCON.send(iq);
            var aPresence = new window.JSJaCPresence();
            aPresence.setType('unavailable');
            this.JSJACCON.send(aPresence);
            this.JSJACCON.disconnect();
        },

        //#endregion

        //#region Register and Unregister Events

        registerHandler: function (event) {
            event = event.toLowerCase(); // don't be case-sensitive here
            var eArg = {
                handler: arguments[arguments.length - 1],
                childName: '*',
                childNS: '*',
                type: '*'
            };
            if (arguments.length > 2)
                eArg.childName = arguments[1];
            if (arguments.length > 3)
                eArg.childNS = arguments[2];
            if (arguments.length > 4)
                eArg.type = arguments[3];

            // CuongNT - 12/1/2016: Khong cho dang ki 1 ham 2 lan
            if (this._events[event]) {
                var arr = this._events[event];
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].handler == eArg.handler) {
                        return;
                    }
                }
            }

            if (!this._events[event])
                this._events[event] = [eArg];
            else
                this._events[event] = this._events[event].concat(eArg);

            // sort events in order how specific they match criterias thus using
            // wildcard patterns puts them back in queue when it comes to
            // bubbling the event
            this._events[event] =
            this._events[event].sort(function (a, b) {
                var aRank = 0;
                var bRank = 0;

                if (a.type == '*')
                    aRank++;
                if (a.childNS == '*')
                    aRank++;
                if (a.childName == '*')
                    aRank++;
                if (b.type == '*')
                    bRank++;
                if (b.childNS == '*')
                    bRank++;
                if (b.childName == '*')
                    bRank++;

                if (aRank > bRank)
                    return 1;

                if (aRank < bRank)
                    return -1;

                return 0;
            });
            this.DEBUG.log("registered handler for event '" + event + "'", 2);

            return this;
        },

        unregisterHandler: function (event, handler) {
            event = event.toLowerCase(); // don't be case-sensitive here

            if (!this._events[event])
                return this;

            var arr = this._events[event], res = [];
            if (typeof handler === "undefined") this._events[event] = res;
            else {
                for (var i = 0; i < arr.length; i++)
                    if (arr[i].handler != handler)
                        res.push(arr[i]);

                if (arr.length != res.length) {
                    this._events[event] = res;
                    this.DEBUG.log("unregistered handler for event '" + event + "'", 2);
                }
            }

            return this;
        },

        _handleEvent: function (event, arg) {
            event = event.toLowerCase(); // don't be case-sensitive here
            this.DEBUG.log("incoming event '" + event + "'", 3);
            if (!this._events[event])
                return;
            this.DEBUG.log("handling event '" + event + "'", 2);

            for (var i = 0; i < this._events[event].length; i++) {
                var aEvent = this._events[event][i];
                if (typeof aEvent.handler == 'function') {
                    if (arg) {
                        if (arg.pType) { // it's a packet
                            if ((!arg.getNode().hasChildNodes() && aEvent.childName != '*') ||
                                (arg.getNode().hasChildNodes() &&
                                !arg.getChild(aEvent.childName, aEvent.childNS))) {
                                continue;
                            }
                            if (aEvent.type != '*' && arg.getType() != aEvent.type) {
                                continue;
                            }
                            this.DEBUG.log(aEvent.childName + "/" + aEvent.childNS + "/" + aEvent.type + " => match for handler " + aEvent.handler, 3);
                        }
                        if (aEvent.handler(arg)) {
                            // handled!
                            break;
                        }
                    } else if (aEvent.handler()) {
                        // handled!
                        break;
                    }
                }
            }
        },

        //#endregion

        //#region Handle Danh sách bạn bè, danh sách online, histories

        getRoster: function () {
            // Todo: Dang duoc goi tu btalk.auth, cần gọi từ Roster

            var iq = btalk.messageFactory.getRosterIqRequest();
            this.JSJACCON.send(iq, this.getRosterResult.bind(this));
        },

        getRosterResult: function (iq) {
            if (!iq || iq.getType() != 'result') {
                if (iq)
                    this.DEBUG.log("Error fetching roster:\n" + iq.xml(), 1);
                else
                    this.DEBUG.log("Error fetching roster", 1);

                return;
            }

            this._handleEvent('_handle_iq_roster_result', this._parseResultToJson(iq)[0]);
        },

        getOnline: function () {
            var iq = btalk.messageFactory.getOnlineIqRequest();
            this.JSJACCON.send(iq, this.getOnlineResult.bind(this));
        },

        getOnlineResult: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            this._handle_iq_online_result(iq);

            // Đưa vào đây do load form home-desktop sau các sự kiện này
            // Todo: cần xem lại cơ chế khi login thẳng.
            this._handle_logined();
        },

        _handle_iq_online_result: function (iq) {
            var iqJson = this._parseResultToJson(iq)[0];

            if (!iqJson || !iqJson.presences || !iqJson.presences.presence) return;

            var onlineJids = _.pluck(iqJson.presences.presence, 'from');

            btalk.ROSTER.updateOnlines(onlineJids);
            this._handleEvent('_handle_iq_online_result', onlineJids);
        },

        getHistories: function (skip, take) {
            var start = this.options.xmpp.baseDatetimeQuery;

            var iq = btalk.messageFactory.getHistoryIqRequest(start, skip, take);
            this.JSJACCON.send(iq, this.getHistoriesResult.bind(this));
        },

        getHistoriesResult: function (iq) {
            if (!iq || iq.getType() != 'result') {
                return;
            }

            var result = this._parseResultToJson(iq)[0];
            var histories = _.isArray(result.list.chat) ? result.list.chat : [result.list.chat];
            this._handleEvent('_handle_iq_list_result', histories || []);
        },

        //#endregion

        //#region Handle Group: get member, add member, remove member, rename

        getMembers: function (groupId) {
            var iq = btalk.messageFactory.GetMembersIqRequest(groupId);
            this.JSJACCON.send(iq, this._getMembersResult.bind(this));
        },

        _getMembersResult: function (iq) {
            var response = this._parseResultToJson(iq);
            var result = {};

            if (!response || !response.query || !response.query.item) return;

            result.groupId = response.from;
            result.members = _.pluck(response.query.item, 'jid');
            this._handleEvent('_handle_getMembersGroup', result);
        },

        _handle_getMembersGroup: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('_handle_getMembersGroup', iqJson);
        },

        _handle_addMembersGroup: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('_handle_addMembersGroup', iqJson);
        },

        _handle_reanameGroup: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('_handle_reanameGroup', iqJson);
        },

        _handle_removeMemberGroup: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('_handle_removeMemberGroup', iqJson);
        },

        //#endregion

        //#region Handle Chat Messages

        // Sends
        sendMessage: function (message, to, id, callback) {
            // Kiểm tra có phải là tin nhắn trả lời hay không?
            var _quote = null, from, msgId, message, type;
            if (typeof message === "object") {
                _quote = message;
                message = _quote.body;
            }
            if (message === '') return;

            from = btalk.ROSTER.currentJid;
            to = btalk.cutResource(to);

            // Id khác null trong trường hợp gửi lại tin nhắn gửi chưa thành công (trạng thái client).
            msgId = id || new Date().getTime().toString();
            type = to.split("@")[1] == this.options.xmpp.conference ? btalk.CHATTYPE.GROUPCHAT : btalk.CHATTYPE.CHAT;

            message = btalk.messageFactory.sendMessage(msgId, from, to, type, message, _quote);

            var isSent = this.JSJACCON.send(message);

            var msgJson = $.xml2json(message.doc);
            if (_.isFunction(callback)) {
                msgJson.status = 'client';
                msgJson.unread = false;
                msgJson.isSent = isSent;
                msgJson.chatterJid = btalk.cutResource(to);
                callback(msgJson);
            }
        },

        sendReceived: function (msgJson, status) {
            // Gửi trạng thái tin nhắn đã nhận.
            var message;
            var from = msgJson.to; // gửi trạng thái ngược lại về người gửi.
            var to = btalk.cutResource(msgJson.from); // gửi trạng thái ngược lại về người gửi.

            // Khong xac nhan neu nguoi gui la chinh minh.
            // Xay ra khi online tren nhieu client, voi goi tin carbon
            if (msgJson.carbons == true || to == btalk.ROSTER.currentJid) {
                return;
            }

            message = btalk.messageFactory.sendStatusMessage(msgJson.id, from, to, msgJson.type, status, msgJson.secs);
            this.JSJACCON.send(message);
        },

        sendConfigs: function (configs, to, chatType, callback) {
            if (!configs || Object.keys(configs).length <= 0) {
                return;
            }

            var msgId = new Date().getTime().toString();
            var from = btalk.ROSTER.currentJid;
            to = btalk.cutResource(to);

            var message = btalk.messageFactory.sendConfigMessage(msgId, from, to, chatType, configs);
            this.JSJACCON.send(message);

            if (typeof callback === 'function') {
                var msgJson = message.JSON();
                msgJson.status = 'client';
                msgJson.unread = false;
                msgJson.chatterJid = btalk.cutResource(to);
                callback(msgJson);
            }
        },

        sendAttachments: function (message, callback) {
            var that = this;
            this.JSJACCON.send(message);

            if (typeof callback === 'function') {
                var msgJson = message.JSON();
                msgJson.status = 'client';
                callback(msgJson);
            }
        },

        sendComposing: function (to, type) {
            /**
             * CuongNT - 6/4/2016
             * Kich ban bao typing:
             * - Bat dau typing khi:
             *   + Nhan duoc composing cua chatter HIEN TAI.
             *     Tu dong dung typing neu sau n giay khong nhan goi composing tiep theo.
             * - Dung typing khi:
             *   + Nhan duoc tin chat toi cua chatter HIEN TAI
             *   + Khi click chuyen sang chatter khac
             * Kich ban gui message composing:
             * - Gui khi bat dau go trong textbox chat. Sau do lien tuc update thoi diem go cuoi.
             * - Dinh ki sau N giay kiem tra lai thoi diem cuoi, neu qua M giay so voi hien tai thi khong gui tiep composing.
             *   Nguoc lai thi tiep tuc gui composing de bao trang thai dang typing.
             *   TamDN - 10/12/2016
             *   Them tham so type phan biet chat 1-1 hay chat nhom
             */

            var from, to, message, msgId;
            var msgId = new Date().getTime().toString();

            from = btalk.ROSTER.currentJid;
            to = btalk.cutResource(to);

            message = btalk.messageFactory.sendComposingMessage(msgId, from, to, type);
            this.JSJACCON.send(message);
        },

        // Receives
        handleReceiveMessage: function (oMsg) {
            /*
                Xử lý gói tin đến: gói tin chát, gói tin trạng thái, gói tin invite
            */
            var messageData = $.xml2json(oMsg.doc);

            // Gói tin lỗi
            if (oMsg.getType() === 'error') {
                return this._handleEvent('message_error', messageData);
            }

            // Gói tin mời vào nhóm
            var invitation = oMsg.getChild('x', 'http://jabber.org/protocol/muc#user');
            if (invitation) {
                return this._handleInviteToGroup(messageData, invitation);
            }

            this._handleChatMessage(oMsg, messageData);
        },

        _handleChatMessage: function (oMsg, msgJson) {
            var i, leng, offline;
            var from = btalk.cutResource(oMsg.getFrom());
            var type = oMsg.getType();

            // Tính thời điểm nhận gói tin offline
            var xNode = oMsg.getNode().getElementsByTagName('x');
            for (i = 0, leng = xNode.length; i < leng; i++) {
                if (xNode.item(i).getAttribute('xmlns') === 'jabber:x:delay') {
                    offline = xNode.item(i);
                    break;
                }
            }

            if (offline) {
                var stamp = offline.getAttribute('stamp');
                oMsg.jwcTimestamp = new Date(Date.UTC(stamp.substring(0, 4), stamp.substring(4, 6) - 1, stamp.substring(6, 8), stamp.substring(9, 11), stamp.substring(12, 14), stamp.substring(15, 17)));
            } else {
                oMsg.jwcTimestamp = new Date();
            }

            if (msgJson.received) {
                return this._handleStatusMessage(msgJson);
            }

            if (msgJson.filename) {
                return this._handleFileMessage(msgJson);
            }

            if (msgJson.sent && msgJson.sent.forwarded && msgJson.sent.forwarded.message) {
                // Xep-0280: Message Carbons: http://xmpp.org/extensions/xep-0280.html
                // Kiem tra message carbons, tuc message duoc gui di boi client khac cung online tai khoan hien tai
                // va duoc forward ve client hien tai de dong bo

                msgJson = msgJson.sent.forwarded.message;
                msgJson.carbons = true;
            }

            msgJson = this._parseRealtimeMessage(msgJson);

            msgJson.isContentMessage && this._handle_roster_message(msgJson);

            this._handleEvent('message', msgJson);

            // Chi xac nhan neu la goi message chat 1-1.
            // Mesasge groupchat chi can xac nhan da xem, khong can xac nhan da toi.
            msgJson.body && msgJson.carbons != true && type === btalk.CHATTYPE.CHAT
                && this.sendReceived(msgJson, 'success');
        },

        _handle_roster_message: function (msgJson) {
            this._handleEvent('_handle_update_roster', msgJson);
        },

        _handleStatusMessage: function (msgJson) {
            this._handleEvent('_handle_status_message', msgJson);
        },

        _handleFileMessage: function (msgJson) {
            this._handleEvent('_handle_file_message', msgJson);
        },

        handleMessageConfig: function (oMsg) {
            var msgJson = $.xml2json(oMsg.doc);
            this._handleEvent('_handle_oMsg_configMemberGroup', msgJson);
        },

        _handleInviteToGroup: function (msgJson, invatation) {
            // Todo: xử lý xong ko thấy dùng để làm gì?

            var from, reason, pass;

            var aInvite = invatation.getElementsByTagName('invite').item(0);

            from = aInvite.getAttribute('from');
            if (aInvite.firstChild && aInvite.firstChild.nodeName == 'reason' && aInvite.firstChild.firstChild) {
                reason = aInvite.firstChild.firstChild.nodeValue;
            }
            if (invatation.getElementsByTagName('password').item(0)) {
                pass = invatation.getElementsByTagName('password').item(0).firstChild.nodeValue;
            }

            // Them group chat jid vao danh sach ban be neu chua co
            var user = btalk.ROSTER.getUserByJID(btalk.cutResource(from));

            // users not in roster (yet)
            if (!user) {
                user = btalk.ROSTER.addUser(btalk.cutResource(from));
            }

            user.iwArr = user.iwArr || [];

            return this._handleEvent('group_invite', msgJson);
        },

        _parseRealtimeMessage: function (message) {
            /*
                 * body: "99999"
                   clienttime: "1530000347185"
                    from: "tungnt@sonongnghiep.tayninh.gov.vn"
                    id: "1530000347185"
                    to: "lytt@sonongnghiep.tayninh.gov.vn"
                    type: "chat"
                 */
            var that = this;
            message.body = btalk.text(message.body);

            // 1. 	Neu la message gui di, duoc forward tu client khac dang dang nhap cung tai khoan
            message.chatterJid = message.carbons === true ? message.to : message.from;
            message.chatterJid = btalk.cutResource(message.chatterJid);

            // 2. 	Neu la message cap nhat trang thai gui/nhan
            if (message.received) {
                message.status = message.received.status || 'viewed';
            }

            // 3. 	Luon la tin chua doc do moi nhan tu server. Chi khi ve len giao dien moi xet trang thai tin da doc hay chua.
            message.unread = true; // message.carbons === true ? false : true;
            message.processType = this._checkProcessTypeOfMessage(message);
            if (!message.secs) {
                var start = new Date(this.options.baseDatetimeQuery);
                var secs = ((new Date()).getTime() - start.getTime()) / 1000;
                message.secs = secs;
            }

            message.senderJid = btalk.cutResource(message.from);
            // Nguoi nhan thuc su
            message.receiverJid = btalk.cutResource(message.to);

            message.attachment && !_.isArray(message.attachment) && (message.attachment = [message.attachment]);

            var receiverFileAcc = message.receiverJid;
            _.each(message.attachment, function (a) {
                var senderTenantId = a.tenantid || "";
                a.extension = _.last(a.name.split('.'));
                a.name = egov.fileExtension.getFileName(a.name);
                a.size = egov.fileExtension.getSizeText(a.size);
                a.url = btalk.fm.geturl(senderTenantId, receiverFileAcc, a.object, String.format("{0}.{1}", a.name, a.extension), a.sentDate, 101);
            });

            this._parseConfigMessage(message);

            message.isContentMessage = this.isContentMessage(message.processType);

            return message;
        },

        _checkProcessTypeOfMessage: function (msg) {
            if (msg.received) {
                return "received";
            }

            if (msg.msgContentType === 'file') {
                return "file";
            }

            if (msg.msgContentType === 'image') {
                return 'image';
            }

            if (msg.config != undefined) { // msg.type == btalk.CHATTYPE.GROUPCHAT && 
                return "roomconfig";
            }

            var attachment = msg.attachment ? (_.isArray(msg.attachment) ? msg.attachment : [msg.attachment]) : null;
            if ((msg.type == btalk.CHATTYPE.CHAT && msg.body && !attachment) || (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.body && !attachment)) {
                return btalk.CHATTYPE.CHAT;
            }

            if ((msg.type == btalk.CHATTYPE.CHAT || msg.type == btalk.CHATTYPE.GROUPCHAT) && attachment
                    && attachment[0].type && attachment[0].type.startWith('image')) {
                return 'image';
            }

            if ((msg.type == btalk.CHATTYPE.CHAT || msg.type == btalk.CHATTYPE.GROUPCHAT) && attachment) {
                return "file";
            }

            if ((msg.type == btalk.CHATTYPE.CHAT && msg.percentage != undefined) || (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.percentage)) {
                return "percentage";
            }

            if (((msg.type == btalk.CHATTYPE.CHAT || msg.type == btalk.CHATTYPE.GROUPCHAT) && msg.composing != undefined)) {
                return "composing";
            }

            console.log("WARNING: MESSAGE NAY KHONG DUOC NHAN DIEN");
            return "";
        },

        _parseConfigMessage: function (msg) {
            var isConfigMsg;
            if (!msg.config || msg.processType !== 'roomconfig') {
                return;
            }

            var editBy;
            var roomName;
            var newMembers;
            var removeMember;
            var timeStartConversion;
            var bodyMsgSuggestchatBmail;

            msg.config.body_msg_suggest_chatBmail && (bodyMsgSuggestchatBmail = msg.config.body_msg_suggest_chatBmail);

            _.each(msg.config, function (item) {
                item.edit_by && (editBy = item.edit_by);
                item.roomconfig_roomname && (roomName = item.roomconfig_roomname);
                item.new_members && (newMembers = item.new_members);
                item.remove_member && (removeMember = item.remove_member);
                item.start_conversion && (timeStartConversion = item.start_conversion);
            });

            msg.isConfigMsg = true;

            if (timeStartConversion) {
                msg.config = { value: new Date(parseInt(timeStartConversion)).format('HH:mm dd/MM/yyyy') };
                return;
            }

            var userEdit = btalk.ROSTER.getUserByJID(editBy);
            if (!userEdit) return;

            var config = {
                userEdit: userEdit.fullname,
                icon: '',
                reason: '',
                value: ''
            };

            if (roomName) {
                config.icon = 'border_color';
                config.reason = ' đã đổi tên nhóm thành ';
                config.value = roomName;
            } else if (newMembers) {
                config.icon = 'group_add';
                config.reason = ' đã thêm ';
                config.value = newMembers;
            } else if (timeStartConversion) {
                config.icon = '';
                config.reason = '';
                config.value = new Date(timeStartConversion).format('HH:mm dd/MM/YYYY');
            } else {
                config.icon = 'block';
                config.reason = ' đã xoá ';
                config.value = removeMember;
            }

            msg.config = config;
        },

        //#endregion

        //#region Load Messages

        getNextPageOfMessages: function (jid, skip, take, type) {
            var start = this.options.xmpp.baseDatetimeQuery;
            var message = btalk.messageFactory.GetMessageHistoriesIqRequest(jid, type, start, skip, take);

            this.JSJACCON.send(message, this.handle_iq_retrieve_result.bind(this));
        },

        handle_iq_retrieve_result: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('handle_iq_retrieve_result', iqJson);
        },

        //#endregion

        //#region Roster User, Groups

        addRoster: function (aJid) {
            var iq = new window.JSJaCIQ();
            iq.setType('set');
            var query = iq.setQuery('jabber:iq:roster');
            var item = query.appendChild(iq.getDoc().createElement('item'));
            item.setAttribute('jid', aJid);
            item.setAttribute('name', aJid);
            item.setAttribute('subscription', 'both');
            this.JSJACCON.send(iq, this.addRosterResult.bind(this));

            // Them nhom hoi thoai vao danh sach lien he
            var user = btalk.ROSTER.getUserByJID(aJid);
            if (!user) {
                // Neu la add groupchat
                if (aJid.substring(aJid.indexOf('@') + 1) == this.options.xmpp.conference) {
                    user = btalk.ROSTER.addUser(aJid, aJid.substring(0, aJid.indexOf('@')), '', ["Chat Rooms"]);
                    user.status = 'available';
                    user.roster = new window.GroupchatRoster();
                    user.roster.nick = aJid.substring(0, aJid.indexOf('@')); // remember my nickname
                }
                    // Neu la add roster
                else {
                    user = btalk.ROSTER.addUser(aJid);
                }
            }
        },

        addRosterResult: function () {
        },

        addRosters: function (aJids) {
            if (!aJids || aJids.length <= 0) {
                return;
            }

            var iq = new window.JSJaCIQ();
            iq.setType('set');
            iq.setID("add_rosters_" + (new Date()).getTime());
            var query = iq.setQuery('jabber:iq:roster');

            for (var i = 0; i < aJids.length; i++) {
                var item = query.appendChild(iq.getDoc().createElement('item'));
                item.setAttribute('jid', aJids[i]);
                item.setAttribute('name', aJids[i].split('@')[0]);
                item.setAttribute('subscription', 'both');
                if (((i + 1) % 200) == 0) {
                    this.JSJACCON.send(iq, this.addRosterResult.bind(this));
                    iq = new window.JSJaCIQ();
                    iq.setType('set');
                    iq.setID("add_rosters_" + (new Date()).getTime());
                    query = iq.setQuery('jabber:iq:roster');
                }
            }
            if ((aJids.length) % 200 != 0) {
                this.JSJACCON.send(iq, this.addRosterResult.bind(this));
            }

            //// Them nhom hoi thoai vao danh sach lien he
            //for (var i = 0; i < aJids.length; i++) {
            //    // Them nhom hoi thoai vao danh sach lien he
            //    var user = btalk.ROSTER.getUserByJID(aJids[i]);
            //    if (!user) {
            //        // Neu la add groupchat
            //        if (aJids[i].substring(aJids[i].indexOf('@') + 1) == this.options.xmpp.conference) {
            //            user = btalk.ROSTER.addUser(aJids[i], aJids[i].substring(0, aJids[i].indexOf('@')), '', ["Chat Rooms"]);
            //            user.status = 'available';
            //            user.roster = new window.GroupchatRoster();
            //            user.roster.nick = aJids[i].substring(0, aJids[i].indexOf('@')); // remember my nickname
            //        }
            //            // Neu la add roster
            //        else {
            //            user = btalk.ROSTER.addUser(aJids[i]);
            //        }
            //    }
            //}
        },

        addRostersResult: function () {
        },

        removeRoster: function (aJid) {
            // get fulljid
            var fulljid = btalk.ROSTER.getUserByJID(aJid).fulljid;

            var iq = new window.JSJaCIQ();
            iq.setType('set');
            var query = iq.setQuery('jabber:iq:roster');
            var item = query.appendChild(iq.getDoc().createElement('item'));
            item.setAttribute('jid', fulljid);
            item.setAttribute('subscription', 'remove');
            this.JSJACCON.send(iq);
        },

        createGroup: function (groupjid, members) {
            /*
            <presence to="cuongnt-bkav1518@conference.bmail.vn">
            </presence>
            <x xmlns="http://jabber.org/protocol/muc"/>
            */
            if (!members || members.length <= 0) {
                return;
            }
            /*
            // Khong can vi mac dinh nguoi tao nhom da la thanh vien nhom
            var hasCurrentUser = false;
            for ( var i = 0; i < members.length; i++ ) {
                if (btalk.cutResource(members[i]) == btalk.ROSTER.currentJid) {
                    hasCurrentUser = true;
                    break;
                }
            }
            if ( hasCurrentUser != true ) {
                members.push(btalk.ROSTER.currentJid);
            }*/

            groupjid = btalk.cutResource(groupjid);
            // !! Dam bao nickname trong nhom chat luon la account that do btalk serrver quy uoc vay !!
            var groupfulljid = groupjid + "/" + btalk.ROSTER.currentuser.name;
            var oPresence = new window.JSJaCPresence();
            // Bat buoc khi tao nhom thi phai co resource la chinh nick cua nguoi tao nhom
            oPresence.setTo(groupfulljid);
            //oPresence.getNode().appendChild(oPresence.getDoc().createElement('x', "http://jabber.org/protocol/muc"))

            this.JSJACCON.send(oPresence, this.createGroupResult.bind(this, groupjid, members));
        },

        createGroupResult: function (groupjid, members) {
            // Add thanh vien nhom
            this.addGroupMembers(groupjid, members);

            // Khoi tao RosterUser gan voi nhom chat
            this.addRoster(groupjid);
        },

        addGroupMembers: function (groupjid, members) {
            // groupjid, members
            groupjid = btalk.cutResource(groupjid);
            var iq = new window.JSJaCIQ();
            iq.setType('set');
            iq.setTo(groupjid);
            iq.setID("add_member");
            var query = iq.setQuery('http://jabber.org/protocol/muc#admin');

            for (var i in members) {
                var item = query.appendChild(iq.getDoc().createElement('item'));
                item.setAttribute('jid', members[i]);
                item.setAttribute('affiliation', 'owner');
            }
            this.JSJACCON.send(iq);
        },

        removeGroupMember: function (groupjid, memberjid) {
            /*
            <iq type="set" to="cuongnt-1547@muc.bmail.vn" id="ab0ea">
                <query xmlns="http://jabber.org/protocol/muc#admin">
                    <item affiliation="none" jid="cuongnt@bmail.vn"/>
                </query>
            </iq>
            */
            groupjid = btalk.cutResource(groupjid);

            var iq = new window.JSJaCIQ();
            iq.setType('set');
            iq.setTo(groupjid);
            iq.setID("remove_member");
            var query = iq.setQuery('http://jabber.org/protocol/muc#admin');
            var item = query.appendChild(iq.getDoc().createElement('item'));
            item.setAttribute('jid', memberjid);
            item.setAttribute('affiliation', 'none');
            this.JSJACCON.send(iq);
        },

        joinGroup: function (groupjid) {
            /*
            <message to="bkav#group#84984822685@conference.bmail.vn" id="1432011755798" xmlns="jabber:client" type="groupchat">
                <body>btalk-join-to-room</body>
            </message>
            */
            var oMsg = new window.JSJaCMessage();
            oMsg.setTo(btalk.cutResource(groupjid));
            oMsg.setID("join_group");
            oMsg.setXMLNS("jabber:client");
            oMsg.setType(btalk.CHATTYPE.GROUPCHAT);
            oMsg.setBody("btalk-join-to-room");
            this.JSJACCON.send(oMsg);
        },

        existGroup: function (groupjid) {
            /*
            <message to="84984822685@bmail.vn" id="1432011755798" xmlns="jabber:client" type="groupchat">
                <body>btalk-out-of-room</body>
            </message>
            */
            var oMsg = new window.JSJaCMessage();
            oMsg.setTo(btalk.cutResource(groupjid));
            oMsg.setID("exist_group");
            oMsg.setXMLNS("jabber:client");
            oMsg.setType(btalk.CHATTYPE.GROUPCHAT);
            oMsg.setBody("btalk-out-of-room");
            this.JSJACCON.send(oMsg);

            this.removeGroupMember();
        },

        renameGroup: function (groupjid, newname) {
            /*
             <iq from='dambv2@bmail.vn/chat'
               id='create2'
               to='dambv21479884877221@conference.bmail.vn'
               type='set'>
              <query xmlns='http://jabber.org/protocol/muc#owner'>
                <x xmlns='jabber:x:data' type='submit'>
                    <field var='FORM_TYPE'>
                       <value>http://jabber.org/protocol/muc#roomconfig</value>
                    </field>
                    <field var='muc#roomconfig_roomname'>
                      <value>new name</value>
                    </field>
               </x>
              </query>
            </iq>

             */

            var sentDate = new Date();
            var msgId = sentDate.getTime().toString();
            var iq = new window.JSJaCIQ();
            iq.setID("rename_group" + msgId);
            iq.setType('set');
            iq.setTo(btalk.cutResource(groupjid));

            var query = iq.setQuery('http://jabber.org/protocol/muc#owner');
            //var x = query.appendChild(iq.getDoc().createElement('x'));
            //x.setAttribute('type', 'submit');
            //x.setAttribute("xmlns", "jabber:x:data");

            var x = iq.buildNode('x', {
                'xmlns': 'jabber:x:data',
                'type': 'submit'
            });
            query.appendChild(x);

            var field_1 = x.appendChild(iq.getDoc().createElement('field'));
            field_1.setAttribute('var', 'FORM_TYPE');
            var value_1 = field_1.appendChild(iq.getDoc().createElement('value'));
            value_1.textContent = "http://jabber.org/protocol/muc#roomconfig";

            var field_2 = x.appendChild(iq.getDoc().createElement('field'));
            field_2.setAttribute('var', 'muc#roomconfig_roomname');
            var value_2 = field_2.appendChild(iq.getDoc().createElement('value'));
            value_2.textContent = newname;
            this.JSJACCON.send(iq);
        },

        //#endregion

        //#region Presence

        changeStatus: function (val, away, prio) {
            this.savedStates.onlstat = val;
            if (away)
                this.savedStates.onlmsg = away;

            if (prio && !isNaN(prio))
                this.savedStates.onlprio = prio;

            if (!this.JSJACCON.connected() && val != 'offline') {
                this._init();
                return;
            }

            var aPresence = new window.JSJaCPresence();

            switch (val) {
                case "unavailable":
                    val = "invisible";
                    aPresence.setType('invisible');
                    break;
                case "offline":
                    val = "unavailable";
                    aPresence.setType('unavailable');
                    this.JSJACCON.send(aPresence);
                    this.JSJACCON.disconnect();
                    return;
                    break;
                case "available":
                    val = 'available';
                    // needed for led in status bar
                    if (away)
                        aPresence.setStatus(away);
                    if (prio && !isNaN(prio))
                        aPresence.setPriority(prio);
                    else
                        aPresence.setPriority(this.savedStates.onlprio);
                    break;
                case "chat":
                    if (prio && !isNaN(prio))
                        aPresence.setPriority(prio);
                    else
                        aPresence.setPriority(this.savedStates.onlprio);
                default:
                    if (away)
                        aPresence.setStatus(away);

                    aPresence.setType(val);
                    if (prio && !isNaN(prio))
                        aPresence.setPriority(prio);
                    else
                        aPresence.setPriority('0');

                    aPresence.setShow(val);
            }

            this.JSJACCON.send(aPresence);

            /*
                * Send presence to chatrooms
                */
            if (btalk.ROSTER && this.savedStates.onlstat != 'invisible') {
                this.sendPresence2Groupchats(btalk.ROSTER.getGroupChats(), this.savedStates.onlstat, this.savedStates.onlmsg);
            }
        },

        sendPresence2Groupchats: function (gc, val, away) {
            var aPresence;
            for (var i = 0; i < gc.length; i++) {
                aPresence = new window.JSJaCPresence();
                aPresence.setTo(gc[i]);
                if (away && away != '')
                    aPresence.setStatus(away);
                if (val != 'available')
                    aPresence.setShow(val);
                this.JSJACCON.send(aPresence);
            }
        },

        // Su dung khi can invisible voi rieng 1 ai do
        sendCustomPresence: function (aJid, presence, msg) {
            var oPresence = new window.JSJaCPresence();
            oPresence.setTo(aJid);
            if (btalk.ROSTER.getUserByJID(aJid).roster)
                oPresence.setXMLNS();

            switch (presence) {
                case 'offline':
                    oPresence.setType('unavailable');
                case 'unavailable':
                    oPresence.setType('unavailable');
                    presence = "invisible";
                default:
                    if (presence != 'available')
                        oPresence.setShow(presence);
            }

            if (typeof (msg) != 'undefined' && msg != '') oPresence.setStatus(msg);
            this.DEBUG.log(oPresence.xml(), 2);
            this.JSJACCON.send(oPresence);
        },

        sendPresenceIDLE: function (valueIdle) {
            var aPresence = new window.JSJaCPresence();
            aPresence.setStatus('available');
            if (valueIdle) {
                // co online nhung ko lam gi.
                aPresence.setShow('away');
            } else {
                // hien tai dang online va ko idle
                aPresence.setShow('chat');
            }
            aPresence.setPriority(1);
            this.JSJACCON.send(aPresence);
        },

        handlePresence: function (presence) {
            /*
                Xử lý gói tin báo trạng thái của người dùng
            */

            var from = btalk.cutResource(presence.getFrom());
            var type = presence.getType();
            var show = presence.getShow();
            var status = presence.getStatus();

            var user = btalk.ROSTER.getUserByJID(from);
            var actor;

            if (!user) return;

            /** PRESENCE FOR GROUPCHAT */
            // handle presence for MUC
            var x = presence.getChild('x', 'http://jabber.org/protocol/muc#user');

            if (x) {
                var ofrom = presence.getFrom().substring(presence.getFrom().indexOf('/') + 1);

                this.DEBUG.log("jabber.from:" + presence.getFrom() + ", ofrom:" + ofrom, 3);

                var ouser = user.roster.getUserByJID(presence.getFrom());

                // no user? create one!
                if (!ouser) {
                    ouser = new GroupchatRosterUser(presence.getFrom(), ofrom);
                }

                var item = x.getElementsByTagName('item').item(0);
                ouser.affiliation = item.getAttribute('affiliation');
                ouser.role = item.getAttribute('role');
                ouser.nick = item.getAttribute('nick');
                ouser.realjid = item.getAttribute('jid');
                if (item.getElementsByTagName('reason').item(0)) {
                    ouser.reason = item.getElementsByTagName('reason').item(0).firstChild.nodeValue;
                }
                if (actor = item.getElementsByTagName('actor').item(0)) {
                    if (actor.getAttribute('jid') != null) {
                        ouser.actor = actor.getAttribute('jid');
                    } else if (item.getElementsByTagName('actor').item(0).firstChild != null) {
                        ouser.actor = item.getElementsByTagName('actor').item(0).firstChild.nodeValue;
                    }
                }
                if (ouser.role != '') {
                    ouser.add2Group(ouser.role + 's');
                    if (ouser.name == btalk.htmlEnc(user.roster.nick)) {
                        user.roster.me = ouser;
                        // store this reference
                        if (user.chatW.updateMe) {
                            user.chatW.updateMe();
                        }
                    }
                }

                this.DEBUG.log("ouser.jid: " + ouser.jid + ", ouser.fulljid:" + ouser.fulljid + ", ouser.name:" + ouser.name + ", user.roster.nick:" + user.roster.nick, 3);

                var nickChanged = false;
                if (x.getElementsByTagName('status').item(0)) {
                    var code = x.getElementsByTagName('status').item(0).getAttribute('code');
                    switch (code) {
                        case '201':
                            // room created -> Tu dong tao room voi config mac dinh
                            var iq = new window.JSJaCIQ();
                            iq.setType('set');
                            iq.setTo(user.jid);
                            var query = iq.setQuery('http://jabber.org/protocol/muc#owner');
                            query.appendChild(iq.buildNode('x', {
                                'xmlns': NS_XDATA,
                                'type': 'submit'
                            }));
                            this.JSJACCON.send(iq);
                            break;
                        case '303':
                            /* Khong xu ly truong hop nay */
                            break;
                        case '301':
                            /* Khong xu ly truong hop nay */
                            break;
                        case '307':
                            /* Khong xu ly truong hop nay */
                            break;
                    }
                }

                this.DEBUG.log("<" + ouser.name + "> affiliation:" + ouser.affiliation + ", role:" + ouser.role, 3);

                if (!user.roster.getUserByJID(presence.getFrom()) && !nickChanged) {
                    // add user vao danh sach thanh vien nhom chat
                    user.roster.addUser(ouser);

                    // show join message
                    var oMsg = new window.JSJaCMessage();
                    oMsg.setFrom(user.jid);
                    oMsg.setBody("" + ouser.name + " has become available");

                    // [TODO] co the thuc te khong dung truong hop nay ma dua tren goi message truc tiep nhu btalk
                    var msgJson = $.xml2json(oMsg.doc);
                    // [TODO] Luu tin chua doc vao cache tai day neu chuyen co che cache tren appview xuong roster
                    this._handleEvent('group_member_join', msgJson);
                } else if (presence.getType() == 'unavailable' && !nickChanged) {
                    // show part message
                    var oMsg = new window.JSJaCMessage();
                    oMsg.setFrom(user.jid);
                    var body = "" + ouser.name + " has left";
                    if (presence.getStatus())
                        body += ": " + presence.getStatus();
                    oMsg.setBody(body);

                    // [TODO] co the thuc te khong dung truong hop nay ma dua tren goi message truc tiep nhu btalk
                    var msgJson = $.xml2json(oMsg.doc);
                    // [TODO] Luu tin chua doc vao cache tai day neu chuyen co che cache tren appview xuong roster
                    this._handleEvent('group_member_left', msgJson);
                }

                user = ouser;
            }

            /** PRESENCE FOR USER */
            var online = undefined;
            // AVAILABLE
            if (show) {
                if (user.get('status') == 'unavailable') {
                    online = true;
                }
                // fix broken pressenc status
                if (show != btalk.CHATTYPE.CHAT && show != 'away' && show != 'xa' && show != 'dnd')
                    show = 'available';
                user.set('status', show);
            } else if (type) {
                // UNAVAILABLE hoac ....
                if (type == 'unsubscribe') {
                    user.subscription = 'from';
                    user.set('status', 'stalker');
                } else if (user.get('status') != 'stalker') {
                    user.set('status', 'unavailable');
                }
                online = false;
            } else {
                // user was offline before
                online = true;
                user.set('status', 'available');
            }

            // show away message
            if (status) {
                user.statusMsg = status;
            } else {
                user.statusMsg = null;
            }
            if ((show == 'chat' || show == 'away') && status == 'available') {
                // Fix the hien idle
                var presenceJson = $.xml2json(presence.doc);
                this._handleEvent('statusIDLE', presenceJson);
            } else if (online == true) {
                var presenceJson = $.xml2json(presence.doc);
                this._handleEvent('online', presenceJson);
            } else if (online == false) {
                var presenceJson = $.xml2json(presence.doc);
                this._handleEvent('offline', presenceJson);
            }

            if (type == 'changeStatus') {
                this._handleEvent('changeStatus', { from: from, status: status });
            }
        },

        //#endregion

        //#region Private

        isContentMessage: function (type) {
            var result = type === btalk.CHATTYPE.CHAT || type === 'file' || type === btalk.CHATTYPE.GROUPCHAT;
            return result;
        },

        bind: function (fn, obj, optArg) {
            return function (arg) {
                return fn.apply(obj, [arg, optArg]);
            };
        },

        _parseResultToJson: function (iq) {
            var result = $.xml2json(iq.doc);
            result = $.isArray(result) ? result : [result];
            return result;
        }

        //#endregion
    };
})(window.jQuery, window.btalk);
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk, _) {
    'use strict';

    if (btalk.egov) {
        return;
    }

    btalk.egov = {
        options: {
            url: "",
            userDeptUrl: "",
            allUsersUrl: "",
            allDeptUrl: "",
            allJobTitlesUrl: "",
            allPositionsUrl: "",

            // Cache json lay tu eGov
            users: egov.setting.allUsers,
            depts: egov.setting.allDeps,
            userDeptPoses: egov.setting.allUserDeps,
            jobtitles: egov.setting.allJobtitles,
            poses: egov.setting.allPositions,
            acs: []
        },

        init: function (options) {
            this.options = $.extend(true, {}, this.options, options);
        },

        getUserDeptTree: function (options) {
            if (this.options.users.length > 0
                && this.options.depts.length > 0
                && this.options.userDeptPoses.length > 0
                && this.options.jobtitles.length > 0
                && this.options.poses.length > 0) {
                if (typeof this.options.success == 'function') {
                    this.options.success({
                        users: this.options.users,
                        depts: this.options.depts,
                        userDeptPoses: this.options.userDeptPoses,
                        jobtitles: this.options.jobtitles,
                        poses: this.options.poses
                    });
                }
                return;
            }

            this.options = $.extend({}, this.options, options);
            // TODO: Sửa về dùng when.then.then... https://datgs.wordpress.com/2011/07/19/ma-thuat-voi-jquery-defferreds/
            //$.when(
            //    this.getAllUsers.bind(this),
            //    this.getAllDept.bind(this),
            //    this.getAllUserDeptPosition.bind(this),
            //    this.getAllJobTitles.bind(this),
            //    this.getAllPositions.bind(this))
            //.then(function () {
            //    var newDept = [];
            //    for (var i = 0; i < this.depts.length; i++) {
            //        newDept.push({
            //            'id': this.depts[i].value,
            //            'parentid': this.depts[i].parentid, // ? "#" : depts[i].parentid,
            //            'text': this.depts[i].data.toString(),
            //            'idext': this.depts[i].idext,
            //            'icon': "",
            //            'order': this.depts[i].order,
            //            'level': this.depts[i].level,
            //            'label': this.depts[i].label,
            //            'state': {
            //                "opened": false
            //            },
            //            'a_attr': {
            //                'rel': "dept",
            //                'idext': this.depts[i].idext,
            //                'label': this.depts[i].label
            //            },
            //            'li_attr': {
            //                'id': this.depts[i].value
            //            },
            //            'children': ['_']
            //        });
            //    }
            //    if (typeof this.options.success == 'function') {
            //        this.options.success({
            //            users: this.users,
            //            depts: newDept,
            //            userDeptPoses: this.userDeptPoses,
            //            jobtitles: this.jobtitles,
            //            poses: this.poses
            //        });
            //    }
            //}.bind(this));

            this.getAllUsers({
                success: function (users) {
                    this.getAllDept({
                        success: function (depts) {
                            this.getAllUserDeptPosition({
                                success: function (userDeptPoses) {
                                    this.getAllJobTitles({
                                        success: function (jobtitles) {
                                            this.getAllPositions({
                                                success: function (poses) {
                                                    var newDept = [];
                                                    for (var i = 0; i < depts.length; i++) {
                                                        newDept.push({
                                                            'id': depts[i].value,
                                                            'parentid': depts[i].parentid, // ? "#" : depts[i].parentid,
                                                            'text': depts[i].data.toString(),
                                                            'idext': depts[i].idext,
                                                            'icon': "",
                                                            'order': depts[i].order,
                                                            'level': depts[i].level,
                                                            'label': depts[i].label,
                                                            'state': {
                                                                "opened": false
                                                            },
                                                            'a_attr': {
                                                                'rel': "dept",
                                                                'idext': depts[i].idext,
                                                                'label': depts[i].label
                                                            },
                                                            'li_attr': {
                                                                'id': depts[i].value
                                                            },
                                                            'children': ['_']
                                                        });
                                                    }
                                                    if (typeof this.options.success == 'function') {
                                                        this.options.success({
                                                            users: users,
                                                            depts: newDept,
                                                            userDeptPoses: userDeptPoses,
                                                            jobtitles: jobtitles,
                                                            poses: poses
                                                        });
                                                    }
                                                    /*
                                                     * TamDN - 23/12/2016
                                                     * Sap xep lai roster theo phong ban
                                                     */
                                                    btalk.ROSTER.sort(btalk.ROSTER.users);
                                                }.bind(this)
                                            });
                                        }.bind(this)
                                    });
                                }.bind(this)
                            });
                        }.bind(this)
                    });
                }.bind(this)
            });
        },

        getAllUsers: function (callback) {
            if (this.options.users.length > 0) {
                return this.options.users;
            }

            if (this.options.allUsersUr == "") {
                console.log("Chua cau hinh url lay toan bo user egov!");
                return [];
            }

            $.ajax({
                url: this.options.allUsersUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.users = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getAllDept: function (callback) {
            if (this.options.depts.length > 0) {
                return this.options.depts;
            }

            $.ajax({
                url: this.options.allDeptUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.depts = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getAllUserDeptPosition: function (callback) {
            if (this.options.userDeptPoses.length > 0) {
                return this.options.userDeptPoses;
            }
            if (this.options.userDeptUrl == "") {
                console.log("Chua cau hinh url lay danh sach user thuoc phong ban egov!");
                return [];
            }
            $.ajax({
                url: this.options.userDeptUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.userDeptPoses = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getAllJobTitles: function (callback) {
            if (this.options.jobtitles.length > 0) {
                return this.options.jobtitles;
            }
            if (this.options.allJobTitlesUrl == "") {
                console.log("Chua cau hinh url lay toan bo chuc vu egov!");
                return [];
            }
            $.ajax({
                url: this.options.allJobTitlesUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.jobtitles = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getAllPositions: function (callback) {
            if (this.options.poses.length > 0) {
                return this.options.poses;
            }
            if (this.options.allPositionsUrl == "") {
                console.log("Chua cau hinh url lay toan bo chuc danh egov!");
                return [];
            }
            $.ajax({
                url: this.options.allPositionsUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.poses = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getACSInfo: function (callback) {
            if (this.options.acs.length > 0) {
                return this.options.acs;
            }
            if (this.options.acsUrl == "") {
                console.log("Chua cau hinh url lay thong tin nghi phep tu acs!");
                return [];
            }

            var d = new Date().toISOString();
            $.ajax({
                type: "POST",
                url: this.options.acsUrl,
                contentType: "application/x-www-form-urlencoded",
                data: { day: d },
                dataType: "xml",
                success: function (data) {
                    // array [{FinishDate: "1/18/2016 11:45:00 PM",
                    // Reason: "",
                    // RegionName: "",
                    // RegulationId: "101",
                    // RegulationName: "Không phải quẹt thẻ",
                    // StartDate: "1/18/2016 8:00:00 AM",
                    // UserName: "Andy"}, ...]
                    var result = [];
                    try {
                        result = JSON.parse($(data.children[0]).text());
                    } catch (err) {
                        console.log("btalk.egov.getACSInfo: loi parse thong tin nghi phep!")
                        result = [];
                    }

                    btalk.egov.options.acs = result;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(result);
                    }
                }.bind(this),
                error: function (e) {
                    console.log("btalk.egov.getACSInfo: loi service lay thong tin nghi phep!")
                },
                async: false
            });
            return btalk.egov.options.acs;
        }
    };
})(window.jQuery, window.btalk = window.btalk || {}, window._);
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.browser) {
        return;
    }

    btalk.browser = {
        nVer: navigator.appVersion,
        nAgt: navigator.userAgent,
        browserName: navigator.appName,
        fullVersion: '' + parseFloat(navigator.appVersion),
        majorVersion: parseInt(navigator.appVersion, 10),
        nameOffset: undefined,
        verOffset: undefined,
        ix: undefined,
        OSName: "Unknown OS",

        init: function () {
            // In Opera, the true version is after "Opera" or after "Version"
            if ((this.verOffset = this.nAgt.indexOf("Opera")) != -1) {
                this.browserName = "Opera";
                this.fullVersion = this.nAgt.substring(this.verOffset + 6);
                if ((this.verOffset = this.nAgt.indexOf("Version")) != -1)
                    this.fullVersion = this.nAgt.substring(this.verOffset + 8);
            }
                // In MSIE, the true version is after "MSIE" in userAgent
            else if ((this.verOffset = this.nAgt.indexOf("MSIE")) != -1) {
                this.browserName = "Microsoft Internet Explorer";
                this.fullVersion = this.nAgt.substring(this.verOffset + 5);
            }
                // In Chrome, the true version is after "Chrome"
            else if ((this.verOffset = this.nAgt.indexOf("Chrome")) != -1) {
                this.browserName = "Chrome";
                this.fullVersion = this.nAgt.substring(this.verOffset + 7);
            }
                // In Safari, the true version is after "Safari" or after "Version"
            else if ((this.verOffset = this.nAgt.indexOf("Safari")) != -1) {
                this.browserName = "Safari";
                this.fullVersion = this.nAgt.substring(this.verOffset + 7);
                if ((this.verOffset = this.nAgt.indexOf("Version")) != -1)
                    this.fullVersion = this.nAgt.substring(this.verOffset + 8);
            }
                // In Firefox, the true version is after "Firefox"
            else if ((this.verOffset = this.nAgt.indexOf("Firefox")) != -1) {
                this.browserName = "Firefox";
                this.fullVersion = this.nAgt.substring(this.verOffset + 8);
            }
                // In most other browsers, "name/version" is at the end of userAgent
            else if ((this.nameOffset = this.nAgt.lastIndexOf(' ') + 1) <
                (this.verOffset = this.nAgt.lastIndexOf('/'))) {
                this.browserName = this.nAgt.substring(this.nameOffset, this.verOffset);
                this.fullVersion = this.nAgt.substring(this.verOffset + 1);
                if (this.browserName.toLowerCase() == this.browserName.toUpperCase()) {
                    this.browserName = navigator.appName;
                }
            }
            // trim the this.fullVersion string at semicolon/space if present
            if ((this.ix = this.fullVersion.indexOf(";")) != -1)
                this.fullVersion = this.fullVersion.substring(0, this.ix);
            if ((this.ix = this.fullVersion.indexOf(" ")) != -1)
                this.fullVersion = this.fullVersion.substring(0, this.ix);

            this.majorVersion = parseInt('' + this.fullVersion, 10);
            if (isNaN(this.majorVersion)) {
                this.fullVersion = '' + parseFloat(navigator.appVersion);
                this.majorVersion = parseInt(navigator.appVersion, 10);
            }

            if (navigator.appVersion.indexOf("Win") != -1) this.OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) this.OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) this.OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) this.OSName = "Linux";
        }
    };

    btalk.browser.init();
})(window.jQuery, window.btalk);
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.cache) {
        return;
    }

    // Viet du lieu cache ra Storage
    function writeCacheStorage(namekey, valuekey) {
        if (typeof (Storage) !== "undefined") {
            localStorage.setItem(namekey, valuekey);
        }
    }

    // Doc du lieu cache tu Storage
    function readCacheStorage(namekey) {
        var valuekey = null;
        if (typeof (Storage) !== "undefined") {
            valuekey = localStorage.getItem(namekey);
        }
        return valuekey;
    }

    // Added DamBV - 23/02/2017 : Doi tuong setting notification.
    function SettingNotifyWithChatter(idChatter, value) {
        this.account = idChatter;
        this.value = value;
        var that = this;

        this.canShow = function () {
            if (that.value == "active" || that.value == null || that.value == "") return true;
            else if (that.value == "noactive") return false;
            else {
                var valueTimeNoteRunNotify = parseInt(that.value);
                var timeNow = new Date().valueOf();
                if (timeNow < valueTimeNoteRunNotify) {
                    return false;
                }
                return true;
            }
        };

        this.delete = function () {
            if (that.value == "active" || that.value == null || that.value == "") {
                return true;
            }
            else {
                var valueTimeNoteRunNotify = parseInt(that.value);
                var timeNow = new Date().valueOf();
                if (timeNow > valueTimeNoteRunNotify) {
                    return true;
                }
                return false;
            }
        }
    }

    /* TamDN - 30/6/2017
    ViewedCache - Lưu thông tin về cache đã xem của 1 chatter, tương ứng
    với một record trong storage
    */
    function ViewedCache(chatterId, strValue) {
        this.chatterId = chatterId;
        this.viewedCache = JSON.parse(strValue);

        if(!this.viewedCache || this.viewedCache[0])
            this.viewedCache = {};

        //Lấy danh sách jids đã xem 1 message dựa vào msgId
        this.getViewedJidsByMsgId = function(msgId) {
            var result = [];
            var keys = Object.keys(this.viewedCache);
            for(var i = 0; i<keys.length; i++) {
                if(this.viewedCache[keys[i]].msgId == msgId)
                    result.push(keys[i]);
            }
            return result;
        }

        //Lưu thông tin vào cache
        this.saveToCache = function(viewedByJid, msgId, secs) {
            //Nếu msgId của message đã xem lớn hơn đang lưu
            //hoặc chưa có thông tin trong cache thì mới cập nhật

            /*if(!this.viewedCache[viewedByJid] || !this.viewedCache[viewedByJid].secs
                    || this.viewedCache[viewedByJid].secs == "undefined" || (this.viewedCache[viewedByJid].secs < secs)) {*/
                var newCacheInfo = {'msgId': msgId, 'secs': secs};
                this.viewedCache[viewedByJid] = newCacheInfo;
                btalk.cache.writeCacheStorage(this.chatterId, JSON.stringify(this.viewedCache));

        }
    }

    /* TamDN - 30/6/2017
    Quản lý cache đã xem trong storage
    */
    function CacheManager() {
        var viewedCacheList = {};

        //Load cache từ storage
        function loadCacheByChatterId(chatterId) {
            var strValue = btalk.cache.readCacheStorage(chatterId);
            var newCache = new btalk.cache.ViewedCache(chatterId, strValue);
            viewedCacheList[chatterId] = newCache;
        }

        //Lấy cache đã xem dựa vào chatterId, nếu chưa load thì load sau đó trả về
        function getViewedCacheByChatterId(chatterId) {
            if(!viewedCacheList[chatterId])
                loadCacheByChatterId(chatterId);
            return viewedCacheList[chatterId];
        }

        //Lấy danh sách các tài khoản đã xem 1 message trong 1 chatter
        this.getViewedListByMsgId = function(chatterId, msgId) {
            var cache = getViewedCacheByChatterId(chatterId);
            return cache.getViewedJidsByMsgId(msgId);
        }

        //Lưu trạng thái đã xem của 1 tài khoản đối với 1 message trong 1 chatter
        this.updateAndSaveToCache = function(chatterId, viewedByJid, msgId, secs) {
            //Nếu là tin của chính mình thì không xử lý
            if(btalk && btalk.auth && typeof btalk.auth.getJID == "function" &&
                    btalk.auth.getJID() == viewedByJid)
                return;

            var cache = getViewedCacheByChatterId(chatterId);
            var oldMsgId = null;
            if(cache.viewedCache[viewedByJid])
                oldMsgId = cache.viewedCache[viewedByJid].msgId;
            cache.saveToCache(viewedByJid, msgId, secs);
            if(btalk.APPVIEW && btalk.APPVIEW.CURRENTCHATTER &&
                    typeof btalk.APPVIEW.CURRENTCHATTER.get == "function" &&
                    btalk.APPVIEW.CURRENTCHATTER.get("jid") == chatterId) {
                btalk.CONVERSATIONDAYS.updateModelsViewed(oldMsgId, msgId, viewedByJid);
            }
            //Cập nhật cache, set lại status các tin khác trong chatter
            btalk.APPVIEW.viewedAllMessage(chatterId);
        }
    }

    btalk.cache = {
        // Cac ham va doi tuong
        ViewedCache: ViewedCache,
        CacheManager: CacheManager,

        SettingNotifyWithChatter: SettingNotifyWithChatter,
        // Cac ham
        writeCacheStorage: writeCacheStorage,
        readCacheStorage: readCacheStorage,

        // Cac bien cau hinh
        statusSizeChatter: "default",
        heightDefaultChatter: 40,
        heightTabHistory: null,

        Name_OpenChatterInfo: "OpenChatterInfo",
        Value_OpenChatterInfo: false,

        Name_SoundMessage: "soundMessage",
        Key_SoundMessage: "active",

        Name_SettingNotification: "SettingNotification",
        ManagerSettingNotification: [],
    }
})(window.jQuery, window.btalk);
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function () {
    'use strict';

    if (btalk.emoticon) {
        return;
    }

    btalk.emoticon = {
        HTML: "",

        EMOTICONS_URL: "/Scripts/btalk/emoticons/",

        EMOTICONS_COUNT: 88,
        _location: window.location.origin,

        EMOTICONS_ID: [21, 20, 6, 18, 42, 48, 50, 50, 51, 53,
                    55, 56, 58, 63, 67, 69, 77, 34, 5, 23,
                    60, 1, 2, 3, 4, 5, 7, 7, 8, 8,
                    9, 10, 10, 11, 12, 13, 14, 14, 15, 16,
                    17, 19, 20, 21, 22, 23, 24, 25, 26, 28,
                    28, 28, 31, 30, 31, 32, 33, 49, 49, 49,
                    35, 36, 37, 38, 39, 40, 41, 43, 44, 44,
                    45, 46, 49, 49, 50, 52, 54, 57, 59, 60,
                    61, 75, 64, 65, 66, 68, 70, 71, 76, 78,
                    79, 47, 72, 73, 74, 75, 1, 1, 2, 3,
                    4, 8, 8, 10, 10, 11, 11, 13, 14, 14, 15,
                    22, 26, 27, 9, 65, 62, 80, 80, 81,
                    81, 82, 82, 83, 83, 84, 84, 85, 85, 86,
                    87, 87, 88, 88],

        // CuongNT - 13/1/2016: Them vao de fix loi scoll bottom khong dung. Fix cung dung chieu cao anh truoc se fix loi nay.
        EMOTICONS_HEIGHT: [
                    20, 20, 20, 20, 18, 25, 25, 20, 20, 19,
                    19, 20, 20, 18, 20, 20, 20, 23, 19, 18,
                    20, 18, 20, 20, 24, 20, 18, 23, 22, 26,
                    20, 20, 24, 18, 20, 35, 18, 25, 23, 20,
                    20, 28, 20, 24, 22, 20, 28, 26, 20, 48,
                    28, 23, 26, 20, 25, 18, 20, 26, 28, 25,
                    25, 20, 30, 18, 20, 18, 23, 25, 23, 20,
                    25, 21, 36, 18, 29, 24, 18, 20, 31, 18,
                    21, 21, 18, 20, 31, 24, 20, 20, 20
        ],

        BACKGROUND_POSITION: [
            // Bo sung phan comment css ben duoi len day
        ],

        EMOTICONS: [
                    //21, 		20, 		6, 				18, 			42, 		48, 		50, 		50, 		51, 		53,
                    ":-))", ":-((", "&gt;:D&lt;", "#:-S", ":-SS", "&lt;):)", "3:-O", "3:-o", ":(|)", "@};-",
                    //55, 		56, 		58, 			63, 			67, 		69, 		77, 		34, 		5, 			23,
                    "**==", "(~~)", "*-:)", "[-O&lt;", ":)&gt;-", "\\:D/", "^:)^", "&lt;@:)", ";;-)", "/:-)",
                    //60, 		1, 			2, 				3, 				4, 			5, 			7, 			7, 			8, 			8,
                    "=:-)", "(-:", ":-(", ";-)", ":-D", ";;)", ":-\\", ":-/", ":-x", ":-X",
                    //9, 		10, 		10, 			11, 			12, 		13, 		14, 		14, 		15, 		16,
                    ":\"&gt;", ":-p", ":-P", ":-*", "=((", ":-O", "x-(", "X-(", ":-&gt;", "B-)",
                    //17, 		19, 		20, 			21, 			22, 		23, 		24, 		25, 		26, 		28,
                    ":-S", "&gt;:)", ":((", ":))", ":-|", "/:)", "=))", "O:)", ":-B", "I-|",
                    //28, 		28, 		31, 			30, 			31, 		32, 		33, 		49, 		49, 		49,
                    "|-)", "I-|", "8-|", "L-)", "8-|", ":-$", "[-(", ":o)", ":0)", ":O)",
                    //35, 		36, 		37, 			38, 			39, 		40, 		41, 		43, 		44, 		44,
                    "8-}", "&lt;:P", "(:|", "=P~", ":-?", "#-o", "=D&gt;", "@-)", ":^O", ":^o",
                    //45, 		46, 		49, 			49, 			50, 		52, 		54, 		57, 		59, 		60,
                    ":-w", ":-&lt;", ":O)", ":@)", "3:O", "~:&gt;", "%%-", "~O)", "8-X", "=:)",
                    //61, 		75, 		64, 			65, 			66, 		68, 		70, 		71, 		76, 		78,
                    "&gt;-)", ":-L", "$-)", ':-"', "b-(", "[-X", "&gt;:/", ";))", ":-@", ":-j",
                    //79, 		47,			72 , 			73 , 			74 ,		75, 		1, 			1, 			2, 			3,
                    "(*)", "&gt;:P", "o-+", "o=&gt;", "o-&gt;", ":-l", "(:", ":)", ":(", ";)",
                    //4, 		8, 			8, 				10, 			10, 		11, 		11, 		13, 		14, 		14,
                    ":D", ":x", ":X", ":P", ":p", "=*", ":*", ":O", "x(", ")X",
                    //15,		22, 		26, 			27, 			9, 			65,			62,			80,			80,			81
                    ":&gt;", ":|", ":B", "=;", ":&quot;&gt;", ":-&quot;", ":-||", "\\o/", "\\O/", "o^^o",
                    //81,		82,			82,				83,				83,			84,			84,			85,			85,			86
                    "O^^O", "o^^", "O^^", "o&gt;:o", "O&gt;:O", ".|.:d", ".|.:D", ":(o-o", ":(O-O", "&gt;:)(:&lt;",
                    //87,		87, 		88, 			88
                    "*\\o/*", "*\\O/*", "/o\\", "/O\\"],

        EMOTICONS_REG: [
                    ":-\\)\\)", ":-\\(\\(", "&gt;:D&lt;", "#:-S", ":-SS", "&lt;\\):\\)", "3:-O", "3:-o", ":\\(\\|\\)", "@};-",
                    "\\*\\*==", "\\(~~\\)", "\\*-:\\)", "\\[-O&lt;", ":\\)&gt;-", "\\\\:D/", "\\^:\\)\\^", "&lt;@:\\)", ";;-\\)",
                    "/:-\\)", "=:-\\)", "\\(-:", ":-\\(", ";-\\)", ":-D", ";;\\)", ":-\\\\", ":-/",
                    ":-x", ":-X", ":\"&gt;", ":-p", ":-P", ":-\\*", "=\\(\\(", ":-O", "x-\\(",
                    "X-\\(", ":-&gt;", "B-\\)", ":-S", "&gt;:\\)", ":\\(\\(", ":\\)\\)", ":-\\|", "/:\\)",
                    "=\\)\\)", "O:\\)", ":-B", "I-\\|", "\\|-\\)", "I-\\|", "8-\\|", "L-\\)", "8-\\|",
                    ":-\\$", "\\[-\\(", ":o\\)", ":0\\)", ":O\\)", "8-}", "&lt;:P", "\\(:\\|", "=P~",
                    ":-\\?", "#-o", "=D&gt;", "@-\\)", ":\\^O", ":\\^o", ":-w", ":-&lt;", ":O\\)",
                    ":@\\)", "3:O", "~:&gt;", "%%-", "~O\\)", "8-X", "=:\\)", "&gt;-\\)", ":-L",
                    "\\$-\\)", ':-"', "b-\\(", "\\[-X", "&gt;:/", ";\\)\\)", ":-@", ":-j", "\\(\\*\\)",
                    "&gt;:P", "o-\\+", "o=&gt;", "o-&gt;", ":-l", "\\(:", ":\\)", ":\\(", ";\\)",
                    ":D", ":x", ":X", ":P", ":p", "=\\*", ":\\*", ":O", "x\\(",
                    "\\)X", ":&gt;", ":\\|", ":B", "=;", ":&quot;&gt;", ":-&quot;", ":-\\|\\|", "\\\\o/",
                    "\\\\O/", "o\\^\\^o", "O\\^\\^O", "o\\^\\^", "O\\^\\^", "o&gt;:o", "O&gt;:O", "\\.\\|\\.:d", "\\.\\|\\.:D",
                    ":\\(o-o", ":\\(O-O", "&gt;:\\)\\(:&lt;", "\\*\\\\o/\\*", "\\*\\\\O/\\*", "/o\\\\", "/O\\\\"],

        init: function () {
            for (var i = 1; i <= this.EMOTICONS_COUNT; i++) {
                var emojiEl = String.format('<li data-value="{0}" class="emoticon icon{0}" title="{2}"><img src="{1}.gif" alt=""/></li>', i, this.EMOTICONS_URL + i, this.getSymbol(i));
                this.HTML += emojiEl;
                // this.HTML = this.HTML + "<li data-value='" + i + "' class='emoticon icon" + i + "' style='background: url(" + this.EMOTICONS_URL + i + ".gif) no-repeat center center' title='" + this.getSymbol(i) + "'></li>";
            }
        },

        getId: function (_symbol) {
            var _index = this._getIndexBySymbol(_symbol);
            return _index == this.EMOTICONS.length ? "" : this.EMOTICONS_ID[_index];
        },

        getSymbol: function (emoticon_id) {
            var _index = this._getIndexById(emoticon_id);
            return _index == this.EMOTICONS_ID.length ? "" : this.EMOTICONS[_index];
        },

        _getIndexById: function (emoticon_id) {
            var _index = -1;
            for (var i = 0; i < this.EMOTICONS_ID.length; i++) {
                if (this.EMOTICONS_ID[i] == emoticon_id) {
                    _index = i;
                    break;
                }
            }
            return _index;
        },

        _getIndexBySymbol: function (_symbol) {
            var _index = 0;
            for (var i = 0; i < this.EMOTICONS.length; i++) {
                if (this.EMOTICONS[i] == _symbol) {
                    _index = i;
                    break;
                }
            }
            return _index;
        },

        render: function () {
            return this.HTML;
        },

        /* Chay thu mat 56ms */
        process: function (str) {
            if (!str) return;
            var startTime = new Date();

            for (var i = 0; i < this.EMOTICONS_REG.length; i++) {
                // Xu ly chinh xac cac dau cach ' '
                // dung \\s thay vi ' ' xong se lam mat dau ngat dong cua html khi copy
                var regex1 = new RegExp('^' + this.EMOTICONS_REG[i] + ' ', "gim");
                var regex2 = new RegExp(' ' + this.EMOTICONS_REG[i] + ' ', "gim");
                var regex3 = new RegExp('^' + this.EMOTICONS_REG[i] + '$', "gim");
                var regex4 = new RegExp(' ' + this.EMOTICONS_REG[i] + '$', "gim");

                // Dau cach phia sau img
                while (regex1.test(str)) {
                    str = str.replace(regex1,
                        '<img title="' + this.EMOTICONS[i] + '" class="emoticon icon' + this.EMOTICONS_ID[i] + '" style="height: ' + this.EMOTICONS_HEIGHT[this.EMOTICONS_ID[i] - 1] + ';" src="' + this.EMOTICONS_URL + this.EMOTICONS_ID[i] + '.gif"/> ');
                }
                // Dau cach 2 phia img
                while (regex2.test(str)) {
                    str = str.replace(regex2,
                        ' <img title="' + this.EMOTICONS[i] + '" class="emoticon icon' + this.EMOTICONS_ID[i] + '" style="height: ' + this.EMOTICONS_HEIGHT[this.EMOTICONS_ID[i] - 1] + ';" src="' + this.EMOTICONS_URL + this.EMOTICONS_ID[i] + '.gif"/> ');
                }
                // Khong dau cach img
                while (regex3.test(str)) {
                    str = str.replace(regex3,
                        '<img title="' + this.EMOTICONS[i] + '" class="emoticon icon' + this.EMOTICONS_ID[i] + '" style="height: ' + this.EMOTICONS_HEIGHT[this.EMOTICONS_ID[i] - 1] + ';" src="' + this.EMOTICONS_URL + this.EMOTICONS_ID[i] + '.gif"/>');
                }
                // Dau cach phia truoc img
                while (regex4.test(str)) {
                    str = str.replace(regex4,
                        ' <img title="' + this.EMOTICONS[i] + '" class="emoticon icon' + this.EMOTICONS_ID[i] + '" style="height: ' + this.EMOTICONS_HEIGHT[this.EMOTICONS_ID[i] - 1] + ';" src="' + this.EMOTICONS_URL + this.EMOTICONS_ID[i] + '.gif"/>');
                }
            }
            var endTime = new Date();
            //console.log("btalk.emoticon.process: " + (endTime.getTime() - startTime.getTime()));
            return str;
        },

        /* Chay thu mat 930ms */
        process2: function (str) {
            var startTime = new Date();
            if (!str) return;

            for (var i = 0; i < this.EMOTICONS.length; i++) {
                var emoticon = this.EMOTICONS[i];
                var emoticonLength = emoticon.length;

                if (str == emoticon) {
                    str = '<img title="' + emoticon + '" class="emoticon icon' + this.EMOTICONS_ID[i] + '" style="height: ' + this.EMOTICONS_HEIGHT[this.EMOTICONS_ID[i] - 1] + ';" src="./emoticons/' + this.EMOTICONS_ID[i] + '.gif"/>';
                    break;
                }

                if (str.startsWith(emoticon + ' ')) {
                    str = '<img title="' + emoticon + '" class="emoticon icon' + this.EMOTICONS_ID[i] + '" style="height: ' + this.EMOTICONS_HEIGHT[this.EMOTICONS_ID[i] - 1] + ';" src="./emoticons/' + this.EMOTICONS_ID[i] + '.gif"/>'
                        + str.slice(emoticonLength);
                }
                if (str.endsWith(' ' + emoticon)) {
                    str = str.slice(0, str.length - emoticonLength) +
                        '<img title="' + emoticon + '" class="emoticon icon' + this.EMOTICONS_ID[i] + '" style="height: ' + this.EMOTICONS_HEIGHT[this.EMOTICONS_ID[i] - 1] + ';" src="./emoticons/' + this.EMOTICONS_ID[i] + '.gif"/>';
                }
            }

            var outstr = '',
                start = 0;

            var emoticonArr = this.EMOTICONS;
            do {
                var minEnd = -1;
                var removeArr = [];
                var minIndex = -1;
                var minEmoticon = "";
                for (var j = 0; j < emoticonArr.length; j++) {
                    var emoticon = emoticonArr[j];
                    var endTmp = str.indexOf(' ' + emoticon + ' ', start);
                    if (endTmp == -1) {
                        //removeArr.push(emoticon);
                    } else if (minEnd == -1 || minEnd > endTmp) {
                        minEnd = endTmp;
                        minIndex = j;
                        minEmoticon = emoticon;
                    }
                }
                if (minEnd == -1 || minIndex == -1 || minEmoticon == "") {
                    break;
                }

                /*for (var j = 0; j < removeArr.length; j++) {
                    var index = emoticonArr.indexOf(5);
                    if (index > -1) {
                        emoticonArr.splice(index, 1);
                    }
                }*/

                outstr += str.slice(start, minEnd);
                start = minEnd;

                outstr += ' <img title="' + minEmoticon + '" class="emoticon icon' + this.EMOTICONS_ID[minIndex] + '" style="height: ' + this.EMOTICONS_HEIGHT[this.EMOTICONS_ID[minIndex] - 1] + ';" src="' + this.EMOTICONS_URL + this.EMOTICONS_ID[minIndex] + '.gif"/> ';
                start = start + minEmoticon.length + 1;
            } while (minEnd > -1)
            str = outstr + str.slice(start);

            var endTime = new Date();
            console.log("btalk.emoticon.process2: " + (endTime.getTime() - startTime.getTime()));
            return str;
        }
    };

    btalk.emoticon.init();
})();
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function () {
    'use strict';

    if (btalk.autolink) {
        return btalk.autolink;
    }

    btalk.autolink = {
        process: function (str) {
            if (!str) {
                return;
            }
            var link = str.autoLink();
            if (link) return link;
        }
    }
})();
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function () {
    'use strict';

    if (btalk.fm) {
        return;
    }

    btalk.fm = {
        serverURL: window._FILEURL,
        fileServerType: "egov",
        options: {
            keystoneUrl: '',
            keystoneUrlAdmin: "",
            tenant: "",
            debug: false,
            defaultRegion: "regionOne"
        },

        isReady: false,
        containers: new Array(),

        // Duoc goi trong btalk.auth.js
        _init: function (options) {
            if (this.isReady) { return; }
            this.isReady = true;

            this.options = $.extend(true, {}, this.options, options);
            JSTACK.Keystone.init(this.options.keystoneUrl, this.options.keystoneUrlAdmin);
        },

        log: function (result) {
            if (this.options.debug) {
                console.log(JSON.stringify(result, null, 4));
            }
        },

        /** Dang nhap */
        login: function (username, password, callback) {
            JSTACK.Keystone.authenticate(username, password, undefined, this.options.tenant, callback, this.log.bind(this));
        },

        /** Ket noi lai bang token */
        reconnect: function (token, callback) {
            JSTACK.Keystone.authenticate(undefined, undefined, token, this.options.tenant, callback, this.log.bind(this));
        },

        /** Thoat */
        logout: function () {
        },

        /**
        * Gui file
        *
        * @param attachment : file gui
        * @param from : jid cua nguoi gui file (khong kem resource)
        * @param to : jid cua nguoi nhan file (khong kem resource)
        */
        upload: function (attachment, from, to, callback, error) {
            var fileData = attachment;
            var formData = new FormData();
            var result;
            var dateStr = this.getEgovDate(fileData.sentDate);

            formData.append('file', fileData.file);
            formData.append('tenantid', fileData.tenantid);
            $.ajax({
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();

                    xhr.upload.onprogress = function (e) {
                        if (e.lengthComputable) {
                            var percentLoaded = Math.round((e.loaded / e.total) * 100);
                            callback({ percentage: percentLoaded, state: percentLoaded == 100 ? 'Finalizing.' : 'Uploading.' });
                        }
                    };

                    return xhr;
                },

                success: function (data, textStatus, xhr) {
                    if (xhr.readyState === 4) {
                        //flag = true;
                        switch (xhr.status) {
                            // In case of successful response it calls the `callbackOK` function.
                            case 100:
                            case 200:
                            case 201:
                            case 202:
                            case 203:
                            case 204:
                            case 205:
                            case 206:
                            case 207:
                                result = undefined;
                                // CuongNT - 27/1/2016: Sua de chi parse json neu tra ve dang json
                                if (xhr.responseText !== undefined && xhr.responseText !== '' &&
                                        xhr.getResponseHeader('Content-Type') == "application/json") {
                                    result = JSON.parse(xhr.responseText);
                                } else {
                                    result = xhr.responseText;
                                }

                                callback(result, xhr.getAllResponseHeaders()); //, xhr.getResponseHeader('x-subject-token') => Tra ve cai nay lam gi nhi?
                                break;

                                // In case of error it sends an error message to `callbackError`.
                            case 401:
                                if (skip_token) {
                                    error({ message: xhr.status + " Error", body: xhr.responseText });
                                } else {
                                    checkToken(function () {
                                        error({ message: xhr.status + " Error", body: xhr.responseText });
                                    });
                                }
                            default:
                                error({ message: xhr.status + " Error", body: xhr.responseText });
                        }
                    }
                },

                type: 'post',
                url: this.serverURL + '/FileUploads.ashx?fileName='
                    + fileData.tenantid + "&date=" + dateStr,
                data: formData,

                processData: false,
                contentType: false,

                error: function () {
                    error({ message: "File gửi lỗi.", body: "" });
                }
            });
        },

        /** Tai file */
        download: function (filename, from, to, callback) {
        },

        // [TODO] dang xay ra truong hop result == undefined khi duoc goi ve danh sach message. Can tinh xu ly cho nay.
        geturl: function (tenantid, container, object, fileName, sentDate, percentage) {
            var dateStr = this.getEgovDate(new Date(sentDate));
            var url = this.serverURL + "/FileDownload.ashx?fileId=" + tenantid + "&fileName=" + encodeURI(fileName) + "&date=" + dateStr;
            url = encodeURI(url);
            return url;
        },
        
        gettenantid: function () {
            return this.generateFileId();
        },

        generateFileId: function () {
            var text = "";
            var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (var i = 0; i < 36; i++)
                text += possible.charAt(Math.floor(Math.random() * possible.length));

            return text;
        },

        getEgovDate: function (date) {
            var dateStr;
            dateStr = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
            return dateStr;
        },

        // Added DamBV - 24/02/2017 : Kiem tra xem file co ton tai hay khong.
        is_url_exist: function (urlToFile) {
            var xhr = new XMLHttpRequest();
            xhr.open('HEAD', urlToFile, false);
            xhr.send();

            if (xhr.status == "404") {
                return false;
            } else {
                return true;
            }
        },

        // Added DamBV 04/07/2017
        getFileFromData: function (toAndFrom) {
            var newSharedFiles = [];
            var newSharedImages = [];
            for (var k = 0; k < toAndFrom.length; k++) {
                if (toAndFrom[k]) {
                    var msg = toAndFrom[k];
                    var receiverFileAcc = msg.chatterJid;
                    if (msg.attachment) {

                        // tin nhan nhieu file
                        if (msg.attachment.length > 0) {
                            for (var i = 0; i < msg.attachment.length; i++) {
                                var url = this.getUrlFile(msg.attachment[i], receiverFileAcc);
                                msg.attachment[i].url = url;
                                if (msg.attachment[i].type.indexOf('image') > -1) {
                                    newSharedImages.push(msg.attachment[i]);
                                } else {
                                    newSharedFiles.push(msg.attachment[i]);
                                }
                            }
                        } else {
                            // tin nhan 1 file
                            var url = this.getUrlFile(msg.attachment, receiverFileAcc);
                            msg.attachment.url = url;
                            if (msg.attachment.type.indexOf('image') > -1) {
                                newSharedImages.push(msg.attachment);
                            } else {
                                newSharedFiles.push(msg.attachment);
                            }
                        }
                    }
                }
            }

            return { file: newSharedFiles, image: newSharedImages };
        },

        getUrlFile: function (attachment, receiverfileacc) {
            var senderTenantId, ojbect, name, sentdate, percentage, type;
            senderTenantId = attachment.tenantid;
            ojbect = attachment.object;
            name = attachment.name;
            sentdate = attachment.sentDate;
            percentage = attachment.percentage;
            type = attachment.type;
            var _url = btalk.fm.geturl(senderTenantId, receiverfileacc, ojbect, name, sentdate, percentage);
            return _url;
        },

        synsShareFileLocal: function (oldList, newList) {
            if (oldList && oldList != null) {
                for (var i = 0; i < newList.length; i++) {
                    var isAdd = true;
                    for (var j = 0; j < oldList.length; j++) {
                        if (oldList[j].id === newList[i].id) {
                            isAdd = false;
                            break;
                        }
                    }
                    if (isAdd) {
                        oldList.push(newList[i]);
                    }
                }
            } else {
                oldList = newList;
            }

            // Sort  theo time
            for (var i = 0; i < oldList.length; i++) {
                for (var j = i + 1; j < oldList.length; j++) {
                    if (parseInt(oldList[i].object) < parseInt(oldList[j].object)) {
                        var tmp = oldList[i];
                        oldList[i] = oldList[j];
                        oldList[j] = tmp;
                    }
                }
            }
            return oldList;
        },

    };
})();