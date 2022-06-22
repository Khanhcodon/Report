/**
 * Body.GetMsgResponse
 */

(function () {

    function MsgUtil(message, isGetFullInfo) {
        msg = message;
        if (isGetFullInfo) {
            this.su = this.getMsgSubject();
            this.e = this.getMsgSender();
            this.d = this.getMsgDate();
        }
        this.attach = [];
        this.getAllMsgPart();
        this.body = -1;
        this._msg = msg;
        this.inlineImages = [];
        this.getMsgContent(msg.m[0]);
        this.getImageInline(msg.m[0]);
    }

    var msg;
    /**
     * get image inline of message
     */
    MsgUtil.prototype.getImageInline = function (m) {
        if (m && m.mp) {
            var temp = m.mp;
            var l = temp.length;

            for (var i = 0; i < l; i++) {

                if ((temp[i].cd && (temp[i].cd == "attachment" || temp[i].cd == "inline")) || (!temp[i].cd && temp[i].filename) || temp[i].ci) {
                    if (temp[i].ct == "application/pkcs7-mime") {

                    } else {

                        if (temp[i].ci) {

                            var tempCi = temp[i].ci;
                            var tempCi = tempCi.substring(1, tempCi.length - 1);

                            var tempFilename = temp[i].filename;
                            var tempPart = temp[i].part;

                            if (this.body.indexOf(tempCi) != -1) {

                                this.inlineImages.push({
                                    "filename": tempFilename,
                                    "ci": tempCi,
                                    "part": tempPart,
                                    "s": temp[i].s
                                });
                            }
                        }
                    }
                }
                if (temp[i].mp)
                    this.getImageInline(temp[i]);
            }
        }
    };
    MsgUtil.prototype.getContentType = function () {
        return msg.m[0].mp[0].ct;
    };

    /**
     * Get the number of parts in this message
     */
    MsgUtil.prototype.getNumberOfPart = function () {
        return msg.m[0].mp.length;
    };

    /**
     * return the part of msg
     * NOTICE: with multipart/mixed, the return maybe an array
     * @return m-->mp[i]
     */
    MsgUtil.prototype.getMsgPart = function (index) {
        return msg.m[0].mp[index];
    };
    MsgUtil.prototype.getId = function () {
        return msg.m[0].id;
    };

    MsgUtil.prototype.getLocationId = function () {
        return msg.m[0].l;
    };

    var TEXT_PLAIN = "text/plain";
    var MULTIPART_MIXED = "multipart/mixed";
    var TEXT_HTML = "text/html";
    var MULTIPART_RELATED = "multipart/related";
    var MULTIPART_ALTERNATIVE = "multipart/alternative";

    /**
     * now we only support: text/plain and multipart/mixed contenttype
     */
    MsgUtil.prototype.getMsgContent = function (m) {
        /*if (this.getContentType() == TEXT_PLAIN || this.getContentType() == TEXT_HTML) {
         return this.getMsgPart(0).content;
         }
         if (this.getContentType() == MULTIPART_MIXED) {
         return this.getMsgPart(0).mp[0].mp[1].mp[1].content;
         }
         if (this.getContentType() == MULTIPART_RELATED) {
         return this.getMsgPart(0).mp[0].mp[0].content;
         }
         if (this.getContentType() == MULTIPART_ALTERNATIVE) {
         return this.getMsgPart(0).mp[1].content;
         } else {
         return this.getMsgPart(0).mp[0].content;
         }*/
        if (m && m.mp) {
            var temp = m.mp;
            var l = temp.length;
            var i = 0;
            for (i = 0; i < l; i++) {
                if (temp[i].body && (temp[i].ct == 'text/html' || temp[i].ct == 'text/plain')) {
                    this.body = temp[i].content;
                }
            }
            // lap lan 2
            if (this.body == -1) {
                var check = true;
                var i1 = 0;
                while (check) {
                    this.getMsgContent(temp[i1]);
                    //m.mp
                    if (this.body != -1) {
                        check = false;
                    } else {
                        i1++;
                        if (i1 == 5) {
                            break;
                        }
                    }
                }
            }
        }
    };

    MsgUtil.prototype.getMsgDate = function () {
        return new Date(msg.m[0].d);
    };

    /**
     *  display image inline
     */
    MsgUtil.prototype.displayImageInline = function () {
        //this.getImageInline();
        if (this.body.indexOf("dfsrc") != -1)
            this.body = this.body.replace(/dfsrc/gi, "src");

        if (this.inlineImages.length > 0) {
            for (var i = 0; i < this.inlineImages.length; i++) {
                var _filePart = "https://" + Util.readCookie("ipServer") + "/service/home/~/" + this.inlineImages[i].filename + "?authToken=co" + "&id=" + this.getMsgId() + "&part=" + this.inlineImages[i].part;
                var _ci = this.inlineImages[i].ci;
                _ci = "cid:" + _ci;
                if (this.body.indexOf(_ci) != -1) {
                    this.body = this.body.replace(new RegExp(_ci, 'g'), _filePart);
                }
            }
        }
    };

    MsgUtil.prototype.getMsgSubject = function () {
        return msg.m[0].su;
    };

    MsgUtil.prototype.getMsgSender = function () {
        if (msg && msg.m[0] && msg.m[0].e) {
            for (var i = 0; i < msg.m[0].e.length; i++) {
                if (msg.m[0].e[i].t == "f") {
                    return msg.m[0].e[i].a;
                }
            }
        }

        return "";
    };

    MsgUtil.prototype.getMsgTo = function () {
        var To = new Array();
        if (msg && msg.m[0] && msg.m[0].e) {
            for (var i = 0; i < msg.m[0].e.length; i++) {
                if (msg.m[0].e[i].t == "t") {
                    To.push(msg.m[0].e[i].a);
                }
            }
        }

        return To;
    };

    MsgUtil.prototype.getMsgId = function () {
        return msg.m[0].id;
    };

    MsgUtil.prototype.getMsgCc = function () {
        var Cc = new Array();
        if (msg && msg.m[0] && msg.m[0].e) {
            for (var i = 0; i < msg.m[0].e.length; i++) {
                if (msg.m[0].e[i].t == "c") {
                    Cc.push(msg.m[0].e[i].a);
                }
            }
        }

        return Cc;
    };

    MsgUtil.prototype.getMsgBcc = function () {
        var Bcc = new Array();
        if (msg && msg.m[0] && msg.m[0].e) {
            for (var i = 0; i < msg.m[0].e.length; i++) {
                if (msg.m[0].e[i].t == "b") {
                    Bcc.push(msg.m[0].e[i].a);
                }
            }
        }

        return Bcc;
    };

    MsgUtil.prototype.getMsgDisplayName = function () {
        for (var i = 0; i < msg.m[0].e.length; i++) {
            if (msg.m[0].e[i].t == "f") {
                return msg.m[0].e[i].p;
            }
        }
        return "";
    };

    MsgUtil.prototype.getAllMsgPart = function () {
        var mpart = msg.m[0].mp;
        for (var i = 0; i < mpart.length; i++) {
            if (!Util.isUndefined(mpart[i].mp)) {
                this._getAllMsgPart(mpart[i].mp);
            } else {
                if (!Util.isUndefined(mpart[i].ct)) {
                    var contentType = mpart[i].ct;
                    this._readMsgContent(contentType, mpart[i]);
                }
            }
        }
    };

    MsgUtil.prototype.contentTxt = "";
    MsgUtil.prototype.contentHtml = "";
    MsgUtil.prototype.inlineImages = new Array();

    MsgUtil.prototype._getAllMsgPart = function (mpart) {
        for (var i = 0; i < mpart.length; i++) {
            if (!Util.isUndefined(mpart[i].mp)) {
                this._getAllMsgPart(mpart[i].mp);
            } else {
                if (!Util.isUndefined(mpart[i].ct)) {
                    var contentType = mpart[i].ct;
                    this._readMsgContent(contentType, mpart[i]);
                }
            }
        }
    };

    MsgUtil.prototype._readMsgContent = function (contentType, mpart) {
        if (contentType == TEXT_PLAIN) {
            if (!Util.isUndefined(mpart.content)) {
                this.contentTxt = mpart.content;
            }
        } else if (contentType == TEXT_HTML) {
            if (!Util.isUndefined(mpart.content)) {
                this.contentHtml = mpart.content;
            }
        }

        if (Util.isUndefined(mpart.cd)) return;

        //to get inline image
        var fileName = mpart.filename.split('.');
        this.attach.push({
            "fileName": fileName[0],
            "extension": fileName[1],
            "part": mpart.part,
            "size": fileSizeInWord(mpart.s)
        });
        //if (!Util.isUndefined(mpart.ci)) {
        //    this.inlineImages.push({
        //        "fileName": fileName[0],
        //        "extension": fileName[1],
        //        "part": mpart.part,
        //        "size": fileSizeInWord(mpart.s)
        //    });
        //}
        //else {
        //    var fileName = mpart.filename.split('.');
        //    this.attach.push({
        //        "fileName": fileName[0],
        //        "extension": fileName[1],
        //        "part": mpart.part,
        //        "size": fileSizeInWord(mpart.s)
        //    });
        //}
    }

    function fileSizeInWord(filesize) {
        var oneKiloByte = 1024;
        var oneMegaByte = 1048576;
        var oneGigaByte = 1073741824;

        if (filesize >= oneGigaByte) {
            return Math.floor(filesize / oneGigaByte) + " GB";
        }
        if (filesize >= oneMegaByte) {
            return Math.floor(filesize / oneMegaByte) + " MB";
        }
        if (filesize >= oneKiloByte) {
            return Math.floor(filesize / oneKiloByte) + " KB";
        }
        return filesize + " bytes";
    }

    window.MsgUtil = MsgUtil;
})();