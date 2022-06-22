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