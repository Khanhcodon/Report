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