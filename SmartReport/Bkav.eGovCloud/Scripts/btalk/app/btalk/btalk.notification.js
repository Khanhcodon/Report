// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/
/**
 * Kich ban notify:
 *
 *   Notify khi Online/offline:
 *   - Chi notify voi nhung user dang co trong danh sach "Hoi thoai" ma thay doi trang thai online/offline
 *   - Notify xong tu dong dong luon.
 *
 *   Notify khi co tin nhan moi:
 *
 *   - 2 kieu notify:
 * 		+ C1: Chi hien so luong tin moi (khong hien noi dung tin).
 * 		+ C2: Hien so luong tin moi + ca noi dung tin. Cac config phu:
 *
 * 	 - Cau hinh:
 * 		+ [V] C1: Khong notify noi dung tin moi, chi hien so luong tin moi.
 * 			* (o)Tu dong dong sau 15 giay.
 * 			* ( )Luon hien cho toi khi doc tin
 * 		+ [ ] C2: Cho phep notify noi dung tin moi va ca so luong tin moi.
 * 			* (o)Tu dong dong sau 15 giay
 * 			* ( )Luon hien cho toi khi doc tin
 * 			-----------------------------------------
 * 			* (o)Notify tat ca tin nhan moi
 * 			* ( )Chi notify 10 tin nhan moi dau tien
 *
 * 	 - Cach notify
 * 		+ C1:
 * 			+ Thong bao chung qua 1 notify duy nhat.
 * 		  	  Moi khi co them nguoi chat moi se dong notify cu di, hien notify moi.
 * 			  Tuy theo cau hinh, notify se tu dong dong sau 15s hoac luon hien thi.
 * 			+ Noi dung notify:
 * 				Title: Co N nguoi cho trao doi
 * 				Noi dung: CuongNT, TaiMV, TienBV... (account hoac fullname, cau hinh duoc).
 *
 * 	 	+ C2:
 * 			+ Moi tin nhan moi hien thi tren 1 notify.
 * 			  Ket thuc notify tin moi bang notify tong ket cua C1.
 * 			  Tuy theo cau hinh, notify se tu dong dong sau 15s hoac luon hien thi.
 * 			+ Noi dung notify:
 * 				Title: Account (N tin chua doc)
 * 				Noi dung: Noi dung chat...
 */

(function (global, factory) {
    'use strict';

    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['jquery', './btalk', 'require'], factory);
    } else {
        // Browser globals
        factory(jQuery, btalk, require);
    }
}(typeof window !== "undefined" ? window : this, function ($, btalk, require) {
    'use strict';

    if (btalk.ntf) {
        return;
    }

    var Notification = window.Notification || window.mozNotification || window.webkitNotification;

    // top.notify && typeof top.notify.requestPermission === 'function' => Dieu kien kiem tra thu vien notify cua eGov ton tai.
    var _parent = top && top.notify && typeof top.notify.requestPermission === 'function' ? top : parent;
    if (_parent && _parent.notify && typeof _parent.notify.requestPermission === 'function') {
        _parent.notify.requestPermission();
    }

    btalk.ntf = {
        instance: null,
        timeoutInstance: null,
        chatter: [],
        messagecount: 0,
        options: {
            /*
             * (o)Tu dong dong sau 15 giay 				=> autoClose = false, 15 giay => autoCloseAfter: 15000
             * ( )Luon hien cho toi khi doc tin			=> autoClose = true
             * --------------------------------------
             * (o)Notify tat ca tin nhan moi			=> notifyAll: true,
             * ( )Chi notify 10 tin nhan moi dau tien	=> notifyAll: false, => 10 tin nhan: notify10: 10
             */
            autoClose: true,
            autoCloseAfter: 15000,
            notifyAll: false,
            notify10: 10,

            timeout: 0,
            onclick: function (e) {
                // Something to do
                $(window).focus();
                btalk.ntf.clear();
            },
            onerror: function () {
                // Something to do
            },

            onclose: function () {
                // Something to do
            },

            getIcon: function (key) {
                return "";
            }
        },
        permission: false,
        requestPermission: function () {
            // Added DamBV 3/4/2018: Bo phan alert thong bao doi voi trinh duyet ko ho tro (chu yeu ap dung cho mobile).           
            if (Notification.permission === "granted") {
                // If it's okay let's create a notification
                //var notification = new Notification("Hi there!");
                this.permission = true;
            }
                // Otherwise, we need to ask the user for permission
            else if (Notification.permission !== "denied") {
                var that = this;
                Notification.requestPermission(function (permission) {
                    // If the user accepts, let's create a notification
                    if (permission === "granted") {
                        that.permission = true;
                    }
                });
            }
        },

        init: function (options) {
            this.options = $.extend(true, {}, this.options, options);
        },

        show: function (avatarChatter, idChatter, title, message, options) {
            if (!message) {
                return;
            }

            //TamDN - Khong notify nhung tin nhan cua chinh minh
            if (avatarChatter == btalk.ROSTER.currentuser.jid) {
                return
            }
            // Kiem tra quyen notify
            // Neu egov khong co quyen hien thi thong bao thi se khong co quyen
            debugger;
            if (!_parent && !typeof _parent.addChatNotify === 'function' && this.permission === true) {
                return;
            }
            this._show({
                idChatter: idChatter,
                avatarChatter: avatarChatter,
                title: title,
                message: message,
                options: options
            });
        },

        _show: function (_notify) {
            if (!_notify.message) {
                return;
            }

            var fullname = btalk.ROSTER.getUserByJID(_notify.idChatter).chatterM.get("fullname");
            var typeChatter = btalk.ROSTER.getUserByJID(_notify.idChatter).chatterM.get("type");

            if (this.chatter.indexOf(fullname) < 0) {
                this.chatter.push(fullname);
            }
            this.messagecount++;

            if (this.instance != null && typeof this.instance.close == 'function') {
                if (this.timeoutInstance) {
                    window.clearTimeout(this.timeoutInstance);
                }
                this.instance.close();
                this.instance = null;
            }

            var _title = _notify.title;
            var _message = "";
            if (_notify.message.length <= 300) {
                _message = _notify.message;
            } else {
                _message = _notify.message.substring(0, 300);
            }
            var extMessage = "";
            if (this.chatter.length == 1) {
                extMessage = "(" + this.messagecount + " tin chưa đọc)";
            } else if (this.chatter.length == 2) {
                extMessage = "(" + this.chatter[0].split("@")[0] + ", " + this.chatter[1].split("@")[0] + " - " + this.messagecount + " tin)";
            }
            else if (this.chatter.length > 2) {
                extMessage = "(" + this.chatter[this.chatter.length - 2].split("@")[0] + ", " + this.chatter[this.chatter.length - 3].split("@")[0] + "... - " + this.messagecount + " tin)";
            }

            var _isRead = _notify.options.isRead;
            var _isMe = _notify.options.isMe;

            var _contentEmotion = btalk.emoticon.process(_message);

            var _avartar = _notify.avatarChatter;
            var _sender = _notify.avatarChatter;

            if (typeChatter == btalk.CHATTYPE.GROUPCHAT) {
                _avartar = location.origin + '/themes/default/images/group3.png';
            } else {
                _avartar = this.options.getIcon(_avartar.split('@')[0]);
            }

            // TODO: ap dung pattern de tach rieng phan code lien quan toi eGov tai day
            if (_parent && typeof _parent.addChatNotify === 'function') {
                _parent.addChatNotify({
                    title: _title,
                    //originalContent: _message + "\r\n" + extMessage,
                    //content: btalk.emoticon.process(_message),
                    originalContent: _message,
                    content: _contentEmotion,
                    extMessage: extMessage,

                    date: new Date(),
                    avatar: _avartar,
                    userName: _notify.idChatter.split('@')[0],
                    fullName: _notify.idChatter,
                    type: 0,
                    isRead: _isRead,
                    isMe: _isMe,

                    sender: _sender,
                    //egov
                    docCopyId: 0,

                    //mail
                    mailId: 0,
                    folderId: 0,

                    //chat
                    chatterJid: _notify.idChatter,
                    messageId: (new Date()).getTime() + '',
                });
            } else {
                this.instance = new Notification(
                    _title, {
                        body: _message + "\r\n" + extMessage,
                        icon: this.options.getIcon(_notify.avatarChatter.split('@')[0])
                    }
                );
                this.instance.jid = _notify.idChatter;

                if (typeof this.options.onclick == 'function') {
                    this.instance.onclick = this.options.onclick;
                }
                if (typeof this.options.onerror == 'function') {
                    this.instance.onerror = this.options.onerror;
                }
                if (typeof this.options.onshow == 'function') {
                    this.instance.onshow = this.onshow;
                }
                if (typeof this.options.onclose == 'function') {
                    this.instance.onclose = this.options.onclose;
                }

                // Tu dong dong notify
                /*if (this.options.autoCloseAfter > 0) {
                    this.timeoutInstance = window.setTimeout(function () {
                        if (this.instance && typeof this.instance.close == 'function') {
                            this.instance.close();
                        }
                        this.instance = null;
                    }.bind(this), this.options.autoCloseAfter);
                }*/
            }
        },

        // Cai nay dung khi minh chat.
        showEgov: function (_notify) {
            if (!_notify.message) {
                return;
            }
            var fullname = btalk.ROSTER.getUserByJID(_notify.idChatter).chatterM.get("fullname");

            if (this.chatter.indexOf(fullname) < 0) {
                this.chatter.push(fullname);
            }
            this.messagecount++;

            if (this.instance != null && typeof this.instance.close == 'function') {
                if (this.timeoutInstance) {
                    window.clearTimeout(this.timeoutInstance);
                }
                this.instance.close();
                this.instance = null;
            }

            var _title = _notify.ntftitle;
            var _message = "";
            if (_notify.message.length <= 300) {
                _message = _notify.message;
            } else {
                _message = _notify.message.substring(0, 300);
            }

            var extMessage = "";
            if (this.chatter.length == 1) {
                extMessage = "(" + this.messagecount + " tin chưa đọc)";
            } else if (this.chatter.length == 2) {
                extMessage = "(" + this.chatter[0].split("@")[0] + ", " + this.chatter[1].split("@")[0] + " - " + this.messagecount + " tin)";
            }
            else if (this.chatter.length > 2) {
                extMessage = "(" + this.chatter[this.chatter.length - 2].split("@")[0] + ", " + this.chatter[this.chatter.length - 3].split("@")[0] + "... - " + this.messagecount + " tin)";
            }

            var _isRead = _notify.isRead;
            var _isMe = _notify.isMe;

            var _contentEmotion = btalk.emoticon.process(_message);

            var _avartar = _notify.avatarChatter;

            if (_notify.avatarChatter.indexOf("@") > -1) {
                _avartar = this.options.getIcon(_notify.avatarChatter.split('@')[0])
            }

            var _sender = btalk.auth.getJID();
            // TODO: ap dung pattern de tach rieng phan code lien quan toi eGov tai day
            if (_parent && typeof _parent.addChatNotify === 'function') {
                _parent.addChatNotify({
                    title: _title,
                    originalContent: _message,
                    content: _contentEmotion,
                    extMessage: extMessage,
                    date: new Date(),
                    avatar: _avartar,
                    userName: _notify.idChatter.split('@')[0],
                    fullName: _notify.idChatter,
                    type: 0,
                    isRead: _isRead,
                    isMe: _isMe,
                    //egov
                    docCopyId: 0,
                    sender: _sender,

                    //mail
                    mailId: 0,
                    folderId: 0,

                    //chat
                    chatterJid: _notify.idChatter,
                    messageId: (new Date()).getTime() + '',
                });
            }
        },

        // Dong het notify dang co va bo qua notify trong cache
        clear: function () {
            this.messagecount = 0;
            this.chatter = [];
            if (this.instance != null && typeof this.instance.close == 'function') {
                if (this.timeoutInstance) {
                    window.clearTimeout(this.timeoutInstance);
                }
                this.instance.close();
                this.instance = null;
            }
        }
    };

    btalk.ntf.requestPermission();
}));