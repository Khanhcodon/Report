var regexUtcDate = /\d{4}-[0-1]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-6]\d$/;

(function ($, _, Backbone) {
    "use strict";
    if (typeof $ === undefined) {
        throw "Chưa có thư viện jquery.";
    }

    if (typeof Backbone === undefined) {
        throw "Chưa có thư viện backbone.";
    }
    var appType = {
        documents: "documents",
        bmail: "bmail",
        chat: "chat",
        calendar: "calendar",
        contacts: "contacts"
    }

    var notifyCacheName = {
        eGov: "eGovNotifications",
        mail: "MailNotifications",
        chat: "ChatNotifications",
    };

    var notifyUrl = {
        gets: "/Notification/GetsByType?type={0}&lastId={1}", //Lấy toàn bộ notify
        setViewed: "/Notification/SetViewed?type=",
        hideNotify: "/Notification/HideNotify/",
        addNotify: "/Notification/AddNotify",
    };

    egov.notifyType = {
        eGov: 1,     // Notify bên xử lý văn bản
        mail: 2,    // Notify bên bmail
        chat: 3,     // Notify của bên chat
        calendar: 4, // Notify bên lịch
    };

    var notifyType = egov.notifyType;

    ///Đối tượng notify model
    var notifyModel = Backbone.Model.extend({
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

    ///Danh sách đối tượng notify model
    var notifyList = Backbone.Collection.extend({
        model: notifyModel
    });

    var NotifyView = Backbone.View.extend({

        el: "#btnNotification",
        $notifyEl: $("#ddlNotification"),
        currentNotify: null,
        $icon: null,
        $btnClear: null,
        notifyNumber: 0,
        total: 0,
        events: {
            "click": "openNotification"
        },

        initialize: function () {
            var that = this;
            this.$icon = this.$(".material-icons");
            this.$btnClear = egov.mobile.$btnClearNotify;

            if (egov.mobile.activeApps.indexOf("documents") >= 0) {
                this.eGovNotify = new notifyTypeView({
                    type: notifyType.eGov,
                    notifyUrl: notifyUrl,
                    oneOnly: false,
                    hasAlert: true,
                    useCache: true,
                    cacheName: notifyCacheName.eGov,
                    parent: that
                });
            }
            if (egov.mobile.activeApps.indexOf("mail") >= 0) {
                bmail.makeRequest.preAuthen(function () {
                    /// <summary>
                    /// Gửi lại AuthenRequest để nhận lại bkavAuthen mới
                    /// </summary>
                    bmail.makeRequest.noOption(function () {
                        /// <summary>
                        /// Tạo NoOpRequest, request này luôn ở trong trạng thái đợi,
                        /// bất kỳ thay đổi nào từ server sẽ được báo về. 
                        /// </summary>
                    });

                    that.MailNotify = new notifyTypeView({
                        type: notifyType.mail,
                        notifyUrl: notifyUrl,
                        oneOnly: false,
                        hasAlert: true,
                        useCache: true,
                        cacheName: notifyCacheName.mail,
                        parent: that
                    });
                })

            }

            if (egov.mobile.activeApps.indexOf("chat") >= 0) {
                this.ChatNotify = new notifyTypeView({
                    type: notifyType.chat,
                    notifyUrl: notifyUrl,
                    oneOnly: true,
                    hasAlert: true,
                    useCache: true,
                    cacheName: notifyCacheName.mail,
                    parent: that
                });
            }

            egov.mobile.notification = this;
        },

        callBack: function () {
            this.updateNotifyInMenu();
        },

        changeCurrentNotify: function () {
            switch (egov.mobile.currentApp) {
                case appType.documents:
                    this.currentNotify = this.eGovNotify;
                    break;
                case appType.bmail:
                    this.currentNotify = this.MailNotify;
                    break;
                case appType.chat:
                    break;
                case appType.calendar:
                    break;
                default:
            }
        },

        changeApp: function () {
            this.changeCurrentNotify();
            //this.$btnClear.hide();
            this.$notifyEl.hide();
            this.showingNotification = false;
            this.$el.removeClass("active");
            //egov.mobile.$mainPage.removeClass("showNotify");
            this.updateBell();
        },

        updateBell: function () {
            var currentNotify = this.currentNotify;
            if (currentNotify) {
                if (currentNotify.model) {
                    this.total = currentNotify.model.length;
                }
                this.notifyNumber = currentNotify.unreads;
            }
            this._updateBell();
        },

        updateNotifyInMenu: function () {
            if (this.eGovNotify) {
                $(".menulayout #documents").attr("data-badge", this.getUnreadLabel(this.eGovNotify.unreads));
            }
            if (this.MailNotify) {
                $(".menulayout #mail").attr("data-badge", this.getUnreadLabel(this.MailNotify.unreads));
            }
        },

        _updateBell: function () {
            if (this.total > 0) {
                this.$icon.text("notifications");
            }
            else {
                this.$icon.text("notifications_none");
            }
            this.$el.attr("data-badge", this.getUnreadLabel(this.notifyNumber));
        },

        getUnreadLabel: function (number) {
            if (number > 99) {
                number = "99+";
            } else if (number < 0) {
                //Hi hữu
                number = 0;
            }
            return number;
        },

        clearNotifies: function () {
            this.currentNotify.closeAll();
        },

        openNotification: function (e) {
            egov.helper.destroyClickEvent(e);
            if (this.currentNotify) {
                this.currentNotify.getNotificationsFromServer();
            }
            if (e) {
                this.$el.toggleClass("active");
            }

            if (this.$el.hasClass("active")) {
                this.showNotification(e);
            }
            else {
                this.hideNotification(e);
            }
            this.checkData();
        },

        showNotification: function (callback) {
            this.isFocusing = true;
            this.$el.addClass("active");
            if (!egov.mobile.isTablet) {
                //$("#btnuptotop").hide();
                egov.mobile.$dataList.hide();
            }
            else {
                egov.mobile.showModal();
            }
            this.$notifyEl.show();

            if (this.total > 0) {
                this.$btnClear.show();
            }
            else {
                this.$btnClear.hide();
            }
            egov.mobile.$mainPage.addClass("showNotify");
            this.showingNotification = true;
            if (egov.mobile.iseGovApp) {
                eGovApp.clearAllNotifications();
            }
            this.checkData();
            egov.callback(callback);
        },

        hideNotification: function (e) {
            if (!e) {
                return;
            }
            this.$el.removeClass("active");
            this.isFocusing = false;
            if (!egov.mobile.isTablet) {
                egov.mobile.$dataList.show();
                //$("#btnuptotop").show();
            }
            else {
                egov.mobile.hideModal();
            }
            this.$btnClear.hide();
            this.$notifyEl.hide();
            egov.mobile.$mainPage.removeClass("showNotify");
            this.showingNotification = false;
        },

        checkData: function () {
            this.currentNotify.open();
        }

    });

    var notifyTypeView = Backbone.View.extend({

        el: "#ddlNotification",

        hasAlert: false,//Cho phép có hiển thị thông báo dưới màn hình hay không
        hasAlertNotify: false, //Check có hiển thị notify trên khung dưới màn hình hay không - do user có đồng ý cho phép thông báo trên màn hình hay không
        useCache: true, //Cho phép sử dụng cache
        cacheName: "",
        oneOnly: false,

        notificationManager: undefined,
        currentNotify: undefined,
        listAlertNotify: [],
        unreads: 0,
        lastId: 0,

        template: "",

        notifyUrl: {
            gets: "",
            setViewed: "",
            hideNotify: "",
            addNotify: "",
        },

        events: {

        },

        initialize: function (options) {
            var that = this;
            this.parent = options.parent;
            this.type = options.type || notifyType.eGov;
            this.notifyUrl = options.notifyUrl;
            this.hasAlert = options.hasAlert || this.hasAlert;
            this.useCache = options.useCache || this.useCache;
            this.cacheName = options.cacheName || "notify_" + this.type;
            this.oneOnly = options.oneOnly || this.oneOnly;
            require([egov.template.mobile.notifyItem], function (template) {
                that.template = template;
                that.init();
            });
            this.initNotificationManager();
            this._bindEvent();
            return this;
        },

        init: function () {
            var notifications = this.getFromCache();
            this.model = new notifyList(notifications);
            this._eventProps();
            this.render(false);
            //this.checkEmptyData();
            this.getNotificationsFromServer();
        },

        getFromCache: function () {
            var result = [];
            if (this.useCache) {
                if (window.localStorage) {
                    var data = localStorage.getItem(this.cacheName);
                    if (data) {
                        try {
                            result = JSON.parse(hashBase64.decode(data));
                            if (result) {
                                result = result.filter(function (item) {
                                    return item.UserId == egov.userId;
                                });
                            }
                            if (result.length > 0) {
                                this.lastId = result[result.length - 1].NotificationId;
                            }
                        } catch (e) {

                        }
                    }
                }
            }
            return result;
        },

        getNotificationsFromServer: function () {
            /// <summary>
            /// Lấy ra thông báo
            /// </summary>
            var that = this;
            var notifications = [];
            return;
            $.ajax({
                url: String.format(this.notifyUrl.gets, this.type, this.lastId),
                type: "GET",
                dataType: "JSON",
                success: function (results) {
                    if (results.length > 0) {
                        console.log(results);
                    }
                    notifications = results;
                },
                error: function () {

                },
                complete: function () {
                    for (var i = 0; i < notifications.length; i++) {
                        that.addToModel(notifications[i]);
                        that.parent.callBack();
                    }
                }
            })
        },

        saveLocache: function () {
            if (this.useCache) {
                if (window.localStorage) {
                    localStorage.removeItem(this.cacheName);
                    this.model.models = this.model.models.sort(function (a, b) {
                        return a.get("NotificationId") > b.get("NotificationId");
                    })
                    var data = hashBase64.encode(JSON.stringify(this.model.models));
                    localStorage.setItem(this.cacheName, data);
                }
            }
            this.parent.updateBell();
            this.checkEmptyData();
        },

        open: function () {
            this.setViewAll();
            this.refreshNotifyTime();
            this.checkEmptyData();
            this.parent.updateBell();
            this._hideAlertNotifies();
        },

        _bindEvent: function () {
            var that = this;
            //Đóng toàn bộ thông báo đã hiển thị trước khi tải lại trang 
            $(window).bind('beforeunload', function () {
                that._hideAlertNotifies();
            });
            //this.$el.hover(function () {
            //    that.setViewAll();
            //    that.refreshNotifyTime();
            //    that.checkEmptyData();
            //});
        },

        _hideAlertNotifies: function () {
            /// <summary>
            /// Remove toàn bộ notify thông báo của trình duyệt
            /// </summary>

            if (this.listAlertNotify) {
                _.each(this.listAlertNotify, function (item) {
                    item.close();
                })
            }
            if (this.currentNotify) {
                this.currentNotify.close();
            }
        },

        _eventProps: function () {
            var that = this;
            this.model.on("add", function (newNotify) {
                that.bindItem(newNotify, true);
            });

            this.model.on("remove", function (model) {
                if (!model.get("IsViewed")) {
                    that.unreads--;
                }
                that.saveLocache();
                that.hideNotify(model.get("NotificationId"));
            });
        },

        render: function (hasAlert) {
            var that = this;
            if (this.model.length > 0) {
                $.each(this.model.models, function () {
                    that.bindItem(this, hasAlert);
                });
            }
        },

        bindItem: function (model, hasAlert) {
            var that = this, unread;
            var notifyItem = new notifyItemView({
                model: model,
                id: "notify_" + model.get("NotificationId"),
                type: this.type,
                parent: this
            });
            unread = !model.get("IsViewed");
            if (unread) {
                this.unreads++;
            }
            this.$el.prepend(notifyItem.$el);
            if (hasAlert && unread) {
                this.parent.$el.buzz();
                this.alertNotify(notifyItem.model, function () {
                    //if (!notifyItem.model.get("IsViewed")) {
                    //    that.unreads--;
                    //}
                    that.openNotifyById(notifyItem.model.get("NotificationId"));
                });
            }
        },

        renderEmptyItems: function () {
            this.$(".no-notify").show();
        },

        addToModel: function (model) {
            //console.log(model);
            var existNotify = this.model.find(function (item) {
                return item.get("NotificationId") == model.NotificationId;
            });
            if (existNotify) {
                return;
            }
            this.model.add(model);
            this.saveLocache();
        },

        initNotificationManager: function () {
            var that = this;
            if (this.hasAlert) {
                this.notificationManager = window.Notification || window.mozNotification || window.webkitNotification;
                if (this.notificationManager) {
                    var hasRequestedNotify = localStorage.getItem("hasRequestedNotify");
                    if (!hasRequestedNotify) {
                        if (this.notificationManager.permission !== "granted") {
                            this.notificationManager.requestPermission(function (status) {
                                localStorage.setItem("hasRequestedNotify", status);
                                that.hasAlertNotify = that.notificationManager.permission !== status;
                            });
                        } else {
                            that.hasAlertNotify = true;
                        }
                    } else {
                        that.hasAlertNotify = hasRequestedNotify;
                    }
                }
            }
        },

        setViewAll: function () {
            var that = this;
            if (this.unreads > 0) {
                $.ajax({
                    url: that.notifyUrl.setViewed + that.type,
                    type: "POST",
                    dataType: "JSON"
                });
                var unreadModels = this.model.models.filter(function (item) {
                    return item.get("IsViewed") == false;
                });
                for (var i = 0; i < unreadModels.length; i++) {
                    unreadModels[i].set("IsViewed", true);
                }
                this.saveLocache();
            }
            else {
                this.$(".unview-notify").removeClass("unview-notify");
            }
            this.unreads = 0;
        },

        refreshNotifyTime: function () {
            $.each(this.model.models, function () {
                this.set("DateFormat", new Date());
            });
        },

        checkEmptyData: function () {
            if (this.model.length > 0) {
                this.$(".no-notify").hide();
            }
            else {
                this.renderEmptyItems();
            }
        },

        hideNotify: function (notificationId) {
            $.ajax({
                url: this.notifyUrl.hideNotify + notificationId,
                type: "POST",
                dataType: "JSON"
            });
        },

        alertNotify: function (model, callback) {
            ///<summary>
            /// Tạo notify trên góc màn hình
            ///</summary>
            ///<param name="data">Đối tượng để tạo notify</param>
            if (!this.hasAlertNotify) {
                return;
            }
            if (model === undefined) {
                return;
            }

            var that = this;

            var notify = null;
            if (this.oneOnly) {
                //Chỉ hiện notify cuối cùng
                notify = this.currentNotify;
                if (notify) {
                    notify.close();
                }
            }
            var notifyTime = "";//getNotificationTime(model.get("ReceiveDate"));
            notify = new this.notificationManager(this._getTitleAlert(), {
                body: model.get("SenderFullName") + "\r\n" + model.get("Title") + "\r\n" + notifyTime,
                icon: model.get("SenderAvatar")
            });

            if (typeof callback == "function") {
                notify.onclick = function () {
                    notify.close();
                    callback();
                };
            }
            if (this.oneOnly) {
                this.currentNotify = notify;
            }
            else {
                this.listAlertNotify.push(notify);
            }
        },

        _getMessageNotifyEmpty: function () {
            var message = null;

            switch (this.type) {
                case notifyType.eGov:
                    // message = egov.resources.emptyDocumentNotifications;
                    message = egov.resources.notify.nodocumentnotify;
                    break;

                case notifyType.mail:
                    // message = egov.resources.emptyMailNotifications;
                    message = egov.resources.notify.nomailnotify;
                    break;

                case notifyType.eTask:
                    break;

                case notifyType.chat:
                    // message = egov.resources.emptyChatNotifications;
                    message = egov.resources.notify.nochatnotify;
                    break;

                default:
                    break;
            }

            return message;
        },

        _getStringOpenAll: function () {
            var str = null;

            switch (this.type) {
                case notifyType.eGov:
                    str = egov.resources.openAllDocument;
                    // str = "Mở tất cả văn bản được thông báo";
                    break;

                case notifyType.mail:
                    str = egov.resources.openAllMail;
                    // str = "Mở tất cả mail nhận được";
                    break;

                case notifyType.eTask:
                    break;

                case notifyType.chat:
                    str = egov.resources.openAllChat;
                    // str = "Mở tất cả tin nhắn nhận được";
                    break;

                default:
                    break;
            }

            return str;
        },

        _getTitleAlert: function () {
            var str = null;
            switch (this.type) {
                case notifyType.eGov:
                    str = egov.resources.main.haveNewDocument;
                    break;

                case notifyType.mail:
                    str = egov.resources.main.haveNewMail;
                    break;

                case notifyType.eTask:
                    break;

                case notifyType.chat:
                    str = egov.resources.main.haveNewChat;
                    break;

                default:
                    break;
            }

            return str;
        },

        _getPropKey: function () {
            ///</ummary>
            /// Lấy thuộc tính chính theo từng loại notify
            ///</summary>
            var propKeyname = null;

            switch (this.type) {
                case notifyType.eGov:
                    propKeyname = "DocumentCopyId";
                    break;
                case notifyType.mail:
                    propKeyname = "MailId";
                    break;
                case notifyType.chat:
                    propKeyname = "ChatId";
                    break;
                default:
                    break;
            }

            return propKeyname;
        },

        openNotifyById: function (id) {
            ///<summary>
            /// Mở notify theo id
            ///</summary>
            ///<param name="id"> <param>
            var models = this.model.select(function (item) {
                return item.get("NotificationId") == id;
            });

            if (models && models.length > 0) {
                var propName = this._getPropKey();
                var model = models[0];
                var value = model.get(propName);
                model.view.open();

                if ((this.arrAlertNotify && value) && this.arrAlertNotify[value]) {
                    this.arrAlertNotify[value].close();
                    delete this.arrAlertNotify[value];
                }
            }
        },

        removeModelById: function (id) {

            var notify = this.model.select(function (item) {
                return item.get("DocumentCopyId") == id;
            });
            if (notify && notify.length > 0) {
                notify = notify[0];
                notify.view._removeView();
            }
        },

        setAlert: function (value) {
            ///<summary>
            /// Thiết lập có hiển thị notify ở khung màn hình hay không
            ///</summary>
            ///<param name="value" type="bool">Giá trị cần thiết lập co hiển thị 1 thống báo ở khung màn hình hay không</param>
            this.hasAlertNotify = value;
        },

        openAll: function () {
            if (!this.model || this.model.length <= 0) {
                return;
            }

            this.model.at(0).view.open(this.model.length === 1);
            this.openAll();
        },

        closeAll: function () {
            if (!this.model || this.model.length <= 0) {
                return;
            }

            this.model.at(0).view.remove(true);
            this.closeAll();
        },

    });

    var notifyItemView = Backbone.View.extend({

        className: "list-group-item two-line",

        type: 0,
        events: {
            'click': 'open',
            //'click .close-notify': 'close',
            //"touchstart": "touchstart",
            //"touchmove": "touch",
        },

        initialize: function (option) {
            this.parent = option.parent;
            this.type = option.type;
            var username = this.model.get("SenderUserName").split("@")[0];
            var avatar = this.model.get("SenderAvatar");
            if (!avatar) {
                avatar = String.format(egov.resources.avatar.path, username);
                this.model.set("SenderAvatar", avatar);
            }
            this.model.set("alphabet", username[0]);
            this.model.set("alphabetCode", getColorCode(username[0]));
            this.render();
            this._eventProps();
            this.model.set("DateFormat", new Date());
            this.model.view = this;
            return this;
        },

        render: function () {
            this.setType();
            if (!this.model.get("IsViewed")) {
                this.$el.addClass("unview-notify");
            }
            this.$el.html(this._parseHtml(this.model));
        },

        setType: function () {
            var app = "";
            switch (this.type) {
                case notifyType.eGov:
                    app = appType.documents;
                    break;
                case notifyType.mail:
                    app = appType.bmail;
                    break;
                case notifyType.chat:
                    app = appType.chat;
                    break;
                default:
            }
            this.$el.attr("data-app", app);

        },

        _eventProps: function () {
            var that = this;
            this.model.on("change:DateFormat", function (model) {
                that.$(".notify-date").text(egov.commonFn.util.getCommonTime(that.model.get("ReceiveDate")))
            })
        },

        open: function () {
            ///<summary>
            /// Mở văn bản hoặc mail
            ///</summary> 
            window.focus();
            var type = this.type;
            this.remove();
            egov.mobile.$ddlNotification.hide();
            switch (type) {
                case notifyType.eGov:
                    this._openDocument(true);
                    break;
                case notifyType.mail:
                    //Mở mail
                    this._openMail();
                    break;
                case notifyType.chat:
                    this._openChat();
                    break;
                case notifyType.calendar:
                    break;
                default:
                    console.log("Notification type not exist!.");
                    break;
            }
        },

        touchstart: function (e) {
            this.startX = getPositionEventTouch(e).clientX;
        },

        touch: function (e) {
            var that = this;

            var eventWidth = getPositionEventTouch(e).clientX - this.startX;

            if (Math.abs(eventWidth) > this.listWidth / 3 && !this.removing) {
                this.removing = true;
                this.$el.fadeOut(300);
                window.setTimeout(function () {
                    that.remove();
                }, 200);
            }
        },

        close: function (e) {
            egov.helper.destroyEvent(e);
            this.remove();
        },

        _parseHtml: function (model) {
            ///<summary>
            /// Giao diện cuả từng model
            ///</summary>  
            if (!model)
                return null;

            if (model instanceof notifyModel) {
                model = model.toJSON();
            }
            return $.tmpl(this.parent.template, model);
        },

        _openDocument: function () {
            ///<summary>
            /// Mở văn bản
            ///</summary>
            openDocument(this.model.get("DocumentCopyId"));
        },

        _openMail: function () {
            ///<summary>
            /// Mở mail
            ///</summary>
            openMail(this.model.get("MailId"))
        },

        _openChat: function () {
            ///<summary>
            /// Mở chat
            ///</summary>
            var that = this;

        },

        _removeView: function () {
            ///<summary>
            /// Xóa trên giao diện và trong model
            ///</summary>
            this.$el.remove();
            this.parent.model.remove(this.model);
        },

        _closeAlertNotify: function () {
            if (this.parent && this.parent.arrAlertNotify) {
                var parent = this.parent;
                var propKey = parent._getPropKey();
                if (parent.arrAlertNotify[this.model.get(propKey)]) {
                    parent.arrAlertNotify[this.model.get(propKey)].close();
                    delete parent.arrAlertNotify[this.model.get(propKey)];
                }
            }
        },

        remove: function () {
            this._removeView();
            this._closeAlertNotify();
        }
    });

    $.fn["buzz"] = function (time, callback) {
        var $that = $(this);
        $that.find(".material-icons").text("notifications_active");
        $that.addClass("buzz");
        if (egov.mobile.notification.isPlayAudio) {
            var audio = new Audio('/Content/Sound/notification_alert.mp3');
            audio.play();
        }
        window.setTimeout(function () {
            $that.find(".material-icons").text("notifications");
            $that.removeClass("buzz");
        }, time || 820, egov.callback(callback))
    }

    return new NotifyView();

    // #endregion

})(window.jQuery, window._, window.Backbone, window.notify);

function openDocument(documentCopyId, callback) {
    egov.mobile.$ddlNotification.hide();
    require(['tabView'], function (TabView) {
        var tabDocument = new TabView({
            model: {
                id: documentCopyId,
                name: "",
                title: "",
                href: 'notifyDocument_' + documentCopyId,
            }
        });
        $(".documents-dataList #documentCopy_" + documentCopyId).removeClass("documentUnread");
        egov.mobile.hideModal();
        egov.callback(callback);
    });
}

function openMail(maiId) {
    egov.mobile.$ddlNotification.hide();
    require(["bmailViewDetail"], function (MailDetail) {
        var mailDetail = new MailDetail();
        mailDetail.openWithId(maiId);
    });
}

function showMobileNotify(callback) {
    var type = notifyOpen.isshowmail ? appType.bmail : appType.documents;
    egov.mobile.changeApp(type, function () {
        egov.mobile.notification.showNotification(callback);
    });
}