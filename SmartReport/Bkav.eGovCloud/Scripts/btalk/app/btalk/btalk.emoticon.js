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