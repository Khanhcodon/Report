define([
    'bmailmakeRequest',
    'bmailResponse',
],
function () {

    "use strict";
    var notifyName = {
        eGov: "eGovNotify",
        mail: "MailNotify",
        chat: "ChatNotify",
        calendar: "CalendarNotify",
    }
    var notifyType = {
        eGov: 1,     // Notify bên xử lý văn bản
        mail: 2,    // Notify bên bmail
        chat: 3,     // Notify của bên chat
        calendar: 4, // Notify bên lịch
    };

    var Notification = Backbone.View.extend({

        el: "#btnNotification",

        mdlMenuClass: 'mdl-menu mdl-menu--bottom-right mdl-js-menu',

        $notifyEl: $("#ddlNotification"),
        $notifyId: "notify_",
        $btnClear: null,
        unViewClass: "unview-notify",
        unReadLabel: egov.mobile.getTotalUnreadLabel,
        isPlayAudio: true, //Todo: Đưa vào cài đặt
        isOpen: false,
        notificationBrowser: null,
        currentNotify: null,
        listNotify: [],

        total: 0,

        template: "",
        mailNotifies: [],

        isFocusing: false,

        events: {
            "click": "show",
        },

        //#region General

        initialize: function () {
            egov.mobile.notification = this;
            this.$btnClear = egov.mobile.$btnClearNotify;

            this.documentModels = new egov.models.notificationList();
            this.bmailModels = new egov.models.notificationList();

            this.bmailList = new egov.models.layoutNotify();
            this.documentsList = new egov.models.layoutNotify();

            this.check();
            if (egov.mobile.isTablet) {
                this.$notifyEl.addClass(this.mdlMenuClass);
                this.$notifyEl.materialMenu();
            }
            this.initNotificationManager();
            this._eventOfProp();
        },

        initNotificationManager: function () {
            if (!egov.mobile.iseGovApp) {
                var that = this;
                this.notificationBrowser = window.Notification || window.mozNotification || window.webkitNotification;
                if (this.notificationBrowser && this.notificationBrowser.permission !== "granted") {
                    this.notificationBrowser.requestPermission(function (status) {
                        if (that.notificationBrowser.permission !== status) {
                            that.notificationBrowser.permission = status;
                        }
                    });
                }
            }
        },

        //#region Notify + Initdata

        notifyOuter: function (type, username, date, title, content, number, callback) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="type"></param>
            /// <param name="username"></param>
            /// <param name="date"></param>
            /// <param name="title"></param>
            /// <param name="content"></param>
            /// <param name="number"></param>
            var that = this;
            var avatarPath = String.format(egov.resources.avatar.path, username);
            if (egov.mobile.iseGovApp) {
                eGovApp.showNotify(type, title, content, avatarPath, number);
            }
            else {
                var notify = null, oneNotify = false;
                switch (parseInt(egov.mobile.notifyType)) {
                    case 1:
                        //Chỉ hiện notify cuối cùng
                        notify = this.currentNotify;
                        if (notify) {
                            notify.close();
                        }
                        oneNotify = true;
                        break;
                    case 2:
                        //Hiện toàn bộ notify
                        break;
                    default:
                        return;
                }
                notify = new this.notificationBrowser(title, {
                    body: content + "\r\n" + username + " - " + date,
                    icon: avatarPath
                });

                if (typeof callback == "function") {
                    notify.onclick = function () {
                        notify.close();
                        egov.callback(callback);
                    };
                }
                if (oneNotify) {
                    this.currentNotify = notify;
                }
                else {
                    this.listNotify.push(notify);
                }
            }
        },

        check: function () {
            this.checkMail();
            this.checkDocuments();
        },

        getFullNotifications: function (appName, callback) {
            var that = this;
            require([egov.template.mobile.notifyItem], function (template) {
                that.template = template;
                switch (appName) {
                    case appType.bmail:
                        that.getBmailNotification(callback);
                        return;
                    case appType.documents:
                        that.getDocumentNotification(callback);
                        return;
                    case appType.chat:
                        return;
                    case appType.calendar:
                        return;
                    case appType.contacts:
                        return;
                    default:
                        return;
                }
            });
        },

        _eventOfProp: function () {
            var that = this;

            //this.bmailList.on("change:unreadTotal", function () {
            //    that.updateBell();
            //});
            //this.documentsList.on("change:unreadTotal", function () {
            //    that.updateBell();
            //});
        },

        refreshTime: function () {
            var that = this;
            if (egov.mobile.currentApp == appType.documents) {
                _.each(that.documentModels.models, function (model) {
                    model.set("dateFormated", splitDateTime(model.get("date")));
                });
            }
            if (egov.mobile.currentApp == appType.bmail) {
                _.each(that.bmailModels.models, function (model) {
                    model.set("dateFormated", egov.commonFn.util.getCommonTime(model.get("date")));
                });
            }
        },

        //#endregion

        //#region Event

        show: function (e) {
            egov.helper.destroyClickEvent(e);
            if (e) {
                this.$el.toggleClass("active");
            }
            else {
                this.$el.addClass("active");
            }

            if (this.$el.hasClass("active")) {
                this.showNotification(e);
            }
            else {
                this.hideNotification(e);
            }
            this.updateBell();
        },

        showNotification: function () {
            this.isFocusing = true;
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

            if (egov.mobile.currentApp == appType.documents) {
                this.setDocumentNotified();
            }
            if (egov.mobile.currentApp == appType.bmail) {
                this.setBmailNotified();
            }
            egov.mobile.$mainPage.addClass("showNotify");
            this.showingNotification = true;
        },

        hideNotification: function (e) {
            if (!e) {
                return;
            }
            this.isFocusing = false;
            if (!egov.mobile.isTablet) {
                egov.mobile.$dataList.show();
                //$("#btnuptotop").show();
            }
            else {
                egov.mobile.hideModal();
            }
            this.$btnClear.hide();
            this[egov.mobile.currentApp + "List"].set("unreadTotal", 0);
            this.updateBell();
            this.$notifyEl.find("." + this.unViewClass).removeClass(this.unViewClass);
            this.$notifyEl.hide();
            egov.mobile.$mainPage.removeClass("showNotify");
            this.showingNotification = false;
        },

        clear: function (notifyId) {
        },

        clearNotifies: function () {
            var notifyIds = [];
            var $list = this.$notifyEl.children(".list-group-item");

            if (egov.mobile.currentApp == appType.documents) {
                $list.filter(function () {
                    notifyIds.push(parseInt($(this).attr("doccopyid")));
                });
                if (notifyIds.length > 0) {
                    removeAllNotify(notifyIds, function (result) {
                        if (result != true) {
                            console.log(result);
                        }
                    })
                }
                this.documentModels.reset();
                this.documentsList = new egov.models.layoutNotify();

            }
            else if (egov.mobile.currentApp == appType.bmail) {
                this.mailNotifies = [];
                this.bmailModels.reset();
                this.bmailList = new egov.models.layoutNotify();
            }

            $list.remove();
            this.show();
        },

        updateNumber: function (name, number, updateAll) {
            if (!name) {
                name = egov.mobile.currentApp;
            }

            var unreadTotal = 0, listModel = this[name + "List"];
            if (listModel) {
                unreadTotal = parseInt(listModel.get("unreadTotal"));
                if (updateAll) {
                    unreadTotal += number;
                }

                this.total = listModel.get("total") + number;

                listModel.set("total", this.total);
                listModel.set("unreadTotal", unreadTotal);
            }
            this.updateBell(name, unreadTotal, true);
        },

        updateBell: function (name, numberNotify, ignoreGet) {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="numberNotify"></param>
            /// <param name="ignoreGet"></param>
            if (!ignoreGet) {
                numberNotify = 0;
                if (!name) {
                    name = egov.mobile.currentApp;
                }
                var listModel = this[name + "List"];
                if (listModel) {
                    numberNotify = listModel.get("unreadTotal");
                    this.total = listModel.get("total");
                }
            }
            if (numberNotify < 0) {
                numberNotify = 0;
            }
            this.$el.attr("data-badge", numberNotify);
            var $noItem = this.$notifyEl.children(".no-notify");
            if (this.total > 0) {
                this.$(".material-icons").text("notifications");
                $noItem.hide();
            }
            else {
                this.$(".material-icons").text("notifications_none");
                $noItem.show();
            }
        },

        //#endregion

        //#endregion

        //#region Document

        checkDocuments: function () {
            var that = this;

            egov.request.notificationsCount({
                success: function (count) {
                    that.documentsList.set({
                        total: count,
                        unreadTotal: count,
                        enable: false,
                        model: []
                    });
                    that.docNotify(count);
                    //that.updateBell();
                },
                error: function (error) {
                    new Error(error);
                }
            });
        },

        docNotify: function (count) {
            $(".menulayout #documents").attr("data-badge", this.unReadLabel(count));
        },

        getDocumentNotification: function (callback) {
            var that = this;
            $.get("/Notification/GetsByType?type=1&lastId=1", function (results) {
                if (results) {
                    that.documentsList.set("total", results.length);
                    var idName = that.$notifyId + "documents_";
                    if (results.length > 0) {
                        for (var i = 0; i < results.length; i++) {
                            if (that.$notifyEl.find("#" + idName + results[i].id).length == 0) {
                                var notifyModel = new egov.models.notification(results[i]);

                                var item = new DocumentNotifyItem({
                                    parent: that,
                                    model: notifyModel,
                                    template: that.template,
                                    id: idName + results[i].id
                                });

                                item.$el.attr("data-app", "documents");
                                that.$notifyEl.append(item.$el);
                                that.documentModels.push(notifyModel);
                            }
                        }

                    }
                    egov.callback(callback);
                }

            });
        },

        notifyDocument: function (document) {
            var that = this;
            this.refreshTime();

            this.updateNumber("documents", 1, true);
            this.$el.buzz();

            var idName = that.$notifyId + "documents_";
            document.isNew = true;
            var notifyModel = new egov.models.notification(document);

            var item = new DocumentNotifyItem({
                parent: that,
                model: notifyModel,
                template: that.template,
                id: idName + document.id
            })
            item.$el.addClass(that.unViewClass);
            item.$el.attr("data-app", "documents");
            that.$notifyEl.prepend(item.$el);

            if (!egov.mobile.showingDetail && egov.mobile.currentApp == appType.documents) {
                egov.views.home.tree.reloadSelectedNode();
            }
            that.documentModels.push(notifyModel);
            this.notifyOuter(1,
                document.userName,
                document.date,
                document.title,
                document.content,
                this.documentsList.get("unreadTotal"), function () {
                    egov.mobile.changeApp("documents", function () {
                        item.select();
                        removeDataBadge(egov.mobile.$('.menu-items[data-ng-app="documents"]'));
                    });
                });
            //Nếu người dùng đang mở mục thông báo, cần đánh dấu luôn ở trạng thái đã xem
            if (this.isFocusing) {
                setNotify([document.docCopyId], function (result) {
                    if (result != true) {
                        console.log(result);
                    }
                });
            }

            if (egov.mobile.currentApp != appType.documents) {
                var currentDocNotify = parseInt($(".menulayout #documents").attr("data-badge"));
                this.docNotify(++currentDocNotify);
            }
        },

        setDocumentNotified: function () {
            var notifyIds = [];
            this.$notifyEl.children("." + this.unViewClass).filter(function () {
                notifyIds.push(parseInt($(this).attr("doccopyid")));
            });
            if (notifyIds.length > 0) {
                setNotify(notifyIds, function (result) {
                    if (result != true) {
                        console.log(result);
                    }
                })
            }
            if (egov.mobile.iseGovApp) {
                eGovApp.clearAllNotifications();
            }
        },

        openDocumentNotify: function (notifyId) {
            $.get("/Mobile/GetNotify", { notifyId: notifyId }, function (notify) {
                if (notify) {
                    require(['tabView'], function (TabView) {
                        var documentCopyId = notify.docCopyId;
                        var compendium = notify.title;
                        var tabDocument = new TabView({
                            model: {
                                id: documentCopyId,
                                name: compendium,
                                title: compendium,
                                href: 'notifyDocument_' + documentCopyId,
                            }
                        });
                        $(".documents-dataList #documentCopy_" + documentCopyId).removeClass("documentUnread");
                        //that.remove();
                    });
                }
                else {
                    egov.pubsub.publish(egov.events.status.warning, egov.resources.mobile.notify.documentNotFound);
                }
            })
        },

        //#endregion

        //#region Bmail

        checkMail: function () {
            var that = this;

            this.mailNotifies = bmail.locache.readAllNotify(notifyName.mail);

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

                var unReadNotifies = that.mailNotifies.filter(function (notify) {
                    return notify.isNew == true;
                });

                that.bmailList.set({
                    total: that.mailNotifies.length,
                    unreadTotal: unReadNotifies.length,
                    enable: false
                });
                that.mailNotify(unReadNotifies.length);
            })
        },

        mailNotify: function (count) {
            $(".menulayout #mail").attr("data-badge", this.unReadLabel(count));
        },

        getBmailNotification: function (callback) {
            /// <summary>
            /// Đọc cache để kiểm tra có bao nhiêu mail notify.
            /// </summary>
            /// <param name="callback"></param>

            var that = this;

            if (!this.mailHasRedered) {
                _.each(this.mailNotifies, function (mail) {
                    var idName = that.$notifyId + "mail_";

                    var notifyModel = that.toModel(mail);

                    var item = new MailNotifyItem({
                        parent: that,
                        model: notifyModel,
                        template: that.template,
                        id: idName + mail.id,
                        mail: mail
                    });

                    if (mail.isNew) {
                        item.$el.addClass(that.unViewClass);
                    }
                    item.$el.attr("data-app", "bmail");
                    that.$notifyEl.prepend(item.$el);
                    that.bmailModels.push(notifyModel);
                });

                this.mailHasRedered = true;
            }
            egov.callback(callback);
        },

        notifyMail: function (mails) {
            var that = this, number = 0;
            this.refreshTime();
            _.each(mails, function (mail) {
                mail.isNew = true;
                var notifyModel = that.toModel(mail);
                //Nếu là mail do mình gửi hoặc gửi vào các box không nằm trên cây thư mục hiện tại hoặc spam thì không hiển thị notify
                if (notifyModel.get("title").indexOf("[Spam]") == -1
                    && bmail.folderUtil.isSendToMyFolder(mail.l)
                    && notifyModel.get("userName").toLowerCase() != username.toLowerCase()) {
                    that.$el.buzz();
                    var idName = that.$notifyId + "mail_";
                    //Thời gian thông báo
                    mail.d = new Date();

                    bmail.locache.addNotify(notifyName.mail, mail);
                    that.mailNotifies.push(mail);
                    var item;
                    if (that.mailHasRedered) {
                        item = new MailNotifyItem({
                            parent: that,
                            model: notifyModel,
                            template: that.template,
                            id: idName + mail.id,
                            mail: mail
                        });

                        //Cái này cần xem lại.
                        //Lưu tất cả các notify vào cache trước, sau đó kiểm tra lại. 
                        //Nếu không có, thì là mới. Nếu có rồi thì bỏ unviewclass
                        item.$el.addClass(that.unViewClass);
                        item.$el.attr("data-app", "bmail");
                        that.$notifyEl.prepend(item.$el);
                    }

                    if (egov.mobile.currentApp != appType.bmail) {
                        var currentMailNotify = parseInt($(".menulayout #mail").attr("data-badge"));
                        that.mailNotify(++currentMailNotify);
                    }
                    that.bmailModels.push(notifyModel);
                    that.notifyOuter(2,
                        notifyModel.get("userName"),
                        notifyModel.get("dateString"),
                        notifyModel.get("title"),
                        notifyModel.get("content"),
                        that.bmailList.get("unreadTotal"), function () {
                            egov.mobile.changeApp("bmail", function () {
                                if (item) {
                                    item.select();
                                }
                                else {
                                    that.show();
                                }
                                removeDataBadge(egov.mobile.$('.menu-items[data-ng-app="bmail"]'));
                            });
                        });
                    number++;
                }
            });

            this.updateNumber("bmail", number, true);

            //Nếu người dùng đang mở mục thông báo, cần đánh dấu luôn ở trạng thái đã xem
            if (this.isFocusing) {
                that.setBmailNotified();
            }
            if (bmail.current && bmail.current.maillist
                && !egov.mobile.showingDetail && egov.mobile.currentApp == appType.bmail) {
                bmail.current.maillist.refresh();
            }
        },

        setBmailNotified: function () {
            /// <summary>
            /// Đặt trạng thái đã nhìn thấy cho tất cả notify của mail
            /// </summary>
            _.each(this.mailNotifies, function (item) {
                item.isNew = false;
            });

            bmail.locache.updateNotifies(notifyName.mail, this.mailNotifies);
        },

        toModel: function (mail) {
            var email = getEmail(mail);

            var userName = email.a;
            if (userName.indexOf("@") > 0) {
                userName = email.a.substr(0, email.a.indexOf("@"));
            }

            var notifyModel = new egov.models.notification({
                title: Util.getSubject(mail.su),
                content: mail.fr,
                date: mail.d,
                dateString: (new Date(mail.d)).format("HH:mm dd/MM/yyyy"),
                id: mail.id,
                docCopyId: 0,
                avatar: userName,
                userName: userName,
                fullName: userName,
                type: 0,
                isViewed: false,
                isNew: mail.isNew,
                hasAttach: mail.f == undefined ? false : mail.f.indexOf("a") != -1
            });

            return notifyModel;
        },

        //#endregion
    });

    //#region Document
    var DocumentNotifyItem = Backbone.View.extend({

        startX: 0,
        listWidth: 0,

        className: "list-group-item two-line",

        isNew: false,
        docCopyId: 0,

        events: {
            "click": "select",
            "touchstart": "touchstart",
            "touchmove": "touch",
            //"touchend": "touchend",
            //"swiperight": "removeright",
            //"swipeleft": "removeleft",
        },

        initialize: function (options) {
            this.parent = options.parent;
            this.template = options.template;
            this.isNew = this.model.get("isNew");
            this.docCopyId = this.model.get("docCopyId");
            this.username = this.model.get("userName");
            this.listWidth = egov.mobile.$ddlNotification.width();
            this.initData();
            this._eventOfProp();
            this.render();
        },

        initData: function () {
            this.model.set("avatar", egov.mobile.showUserAvatar ? String.format(egov.setting.avatarPath, this.model.get("avatar")) : getUserAvatar(this.model.get("userName")));
            this.model.set("dateFormated", splitDateTime(this.model.get("date")));
            this.model.set("alphabet", this.username[0]);
            this.model.set("alphabetCode", getColorCode(this.username[0]));
        },

        _eventOfProp: function () {
            var that = this;
            this.model.on("change:dateFormated", function (model, dateFormated) {
                that.$(".lblNotifyTime").text(dateFormated);
            })
        },

        render: function () {
            if (this.isNew) {
                this.$el.addClass(this.parent.unViewClass);
            }
            this.$el.attr("doccopyid", this.docCopyId);
            this.$el.append($.tmpl(this.template, this.model.toJSON()));
        },

        select: function () {
            var that = this;
            egov.mobile.$ddlNotification.hide();
            require(['tabView'], function (TabView) {
                var documentCopyId = that.docCopyId;
                var compendium = that.model.title;
                var tabDocument = new TabView({
                    model: {
                        id: documentCopyId,
                        name: compendium,
                        title: compendium,
                        href: 'notifyDocument_' + documentCopyId,
                    }
                });
                $(".documents-dataList #documentCopy_" + documentCopyId).removeClass("documentUnread");
                that.remove();
                egov.mobile.hideModal();
            });
        },

        touchstart: function (e) {
            this.startX = getPositionEventTouch(e).clientX;
        },

        touch: function (e) {
            var that = this;

            var eventWidth = getPositionEventTouch(e).clientX - this.startX;
            //this.$el.css({
            //    "left": eventWidth
            //})
            if (Math.abs(eventWidth) > this.listWidth / 3 && !this.removing) {
                this.removing = true;
                this.$el.fadeOut(300);
                window.setTimeout(function () {
                    that.remove();
                }, 200);
            }
        },

        touchend: function (e) {
            var that = this;
            var eventWidth = getPositionEventTouch(e).clientX - this.startX;
            this.$el.css({
                "left": eventWidth
            })
            if (Math.abs(eventWidth) <= this.listWidth / 3) {
                this.$el.animate({
                    "left": 0
                }, 300)
            }
        },

        removeleft: function (e) {
            this.remove();
        },

        removeright: function (e) {
            this.remove();
        },

        remove: function () {
            var that = this;
            that.parent.updateNumber("documents", -1, this.isNew);
            removeNotify(this.docCopyId, function (result) {
                if (result) {
                    that.$el.remove();
                }
            });
        },

    })

    //#endregion

    //#region Mail
    var MailNotifyItem = Backbone.View.extend({

        startX: 0,
        listWidth: 0,

        className: "list-group-item two-line",

        isNew: false,
        mailId: 0,
        username: "",

        events: {
            "click": "select",
            "touchstart": "touchstart",
            "touchmove": "touch",
        },

        initialize: function (options) {
            this.parent = options.parent;
            this.template = options.template;
            this.mail = options.mail;
            this.mailId = this.mail.id;
            this.isNew = this.model.get("isNew");
            this.username = this.model.get("userName");
            this.listWidth = egov.mobile.$ddlNotification.width();
            this.initData();
            this._eventOfProp();
            this.render();
        },

        initData: function () {
            this.model.set("avatar", egov.mobile.showUserAvatar ? String.format(egov.setting.avatarPath, this.model.get("avatar")) : getUserAvatar());
            this.model.set("alphabet", this.username[0]);
            this.model.set("alphabetCode", getColorCode(this.username[0]));
            this.model.set("dateFormated", egov.commonFn.util.getCommonTime(this.model.get("date")));
        },

        _eventOfProp: function () {
            var that = this;
            this.model.on("change:dateFormated", function (model, dateFormated) {
                that.$(".lblNotifyTime").text(dateFormated);
            })
        },

        render: function () {
            if (this.isNew) {
                this.$el.addClass(this.parent.unViewClass);
            }
            this.$el.attr("doccopyid", this.docCopyId);
            this.$el.append($.tmpl(this.template, this.model.toJSON()));
        },

        select: function () {
            var that = this;
            egov.mobile.$ddlNotification.hide();
            var mail = this.mail;

            require(["bmailViewDetail"], function (MailDetail) {
                var trimId = mail.id.replace(/:/gi, "");
                var email = getEmail(mail);
                that.bmailModel = new bmail.models.mail({
                    d: mail.d,
                    date: egov.commonFn.util.getCustomTime(mail.d),
                    detailDate: new Date(mail.d).format("HH:mm dd/MM/yyyy"),
                    location: mail.l,
                    conversationId: mail.cid,
                    id: mail.id,
                    trimId: trimId,
                    domId: "maillist_" + trimId,
                    mailDetailId: 'mailDetail_' + trimId,
                    sender: {
                        fulladdress: email.a,
                        address: email.a ? email.a.substr(0, email.a.indexOf("@")) : "",
                        name: email.d ? email.d : "",
                        fullname: email.p
                    },
                    subject: Util.getSubject(mail.su),
                });
                var mailDetail = new MailDetail({
                    model: that.bmailModel,
                    id: that.model.get("mailDetailId")
                });
                $("#mail-detail").children().hide();
                $("#mail-detail").append(mailDetail.$el);
                egov.mobile.showDetailPage();
                that.markAsRead();
                egov.mobile.hideModal();
            });
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

        remove: function () {
            var that = this;
            that.parent.updateNumber("bmail", -1, this.isNew);
            removeMailNotify(this.mailId, function () {
                that.$el.remove();
            })
            //Xóa cache
        },

        markAsRead: function () {
            /// <summary>
            /// Đánh dấu là đã đọc và xóa khỏi danh sách notify
            /// </summary>
            var that = this;
            bmail.makeRequest.msgAction("read", this.mailId, function () {
                if (bmail.current && bmail.current.folder) {
                    var toltalUnread = bmail.current.folder.model.get("totalUnread");
                    if (toltalUnread > 0) {
                        bmail.current.folder.model.set("totalUnread", toltalUnread - 1);
                    }
                }
            });

            $(".bmail-dataList #mail-list-" + that.mailId).removeClass("unread");
            this.remove();
        }

    })

    //#endregion

    function alertInfo(docId) {
        alert(docId);
    }

    function removeNotify(docCopyId, callback) {
        ///<summary>
        /// Xóa các notify liên quan đến văn bản
        ///</summary>
        ///<param name="docCopyId"> id của văn bản muốn xóa notify</param>
        ///<param name ="callback">Hàm gọi lại khi post ajax thành công</param>
        $.post("/Mobile/RemoveNotify", { docCopyId: docCopyId }, function (data) {
            egov.callback(callback(data));
        });
    }

    function removeMailNotify(mailId, callback) {
        /// <summary>
        /// Xóa thông báo mail
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="callback"></param>
        /// <returns type=""></returns>
        bmail.locache.removeNotify(notifyName.mail, mailId);
        egov.callback(callback());
    }

    function removeAllNotify(docCopyIds, callback) {
        ///<summary>
        /// Xóa tất cả thông báo
        ///</summary>
        ///<param name="docCopyId"> id của văn bản muốn xóa notify</param>
        ///<param name ="callback">Hàm gọi lại khi post ajax thành công</param>
        $.ajax({
            type: "POST",
            url: "/Mobile/RemoveAllNotify",
            traditional: true,
            data: { docCopyIds: docCopyIds },
            success: function (data) {
                egov.callback(callback(data));
            }
        });
    }

    function setNotify(docCopyIds, callback) {
        ///<summary>
        /// Đặt các thông báo đang hiển thị về trạng thái đã xem
        ///</summary>
        ///<param name="docCopyId"> id của văn bản muốn xóa notify</param>
        ///<param name ="callback">Hàm gọi lại khi post ajax thành công</param>
        $.ajax({
            type: "POST",
            url: "/Mobile/SetNotified",
            traditional: true,
            data: { docCopyIds: docCopyIds },
            success: function (data) {
                egov.callback(callback(data));
            }
        });
    }

    function splitDateTime(date) {
        var array = date.split(/[^0-9]/); //hh:mm:ss MM/dd/yyyy
        var dateFormatStr = "";
        if (egov.mobile.isIOS) {
            dateFormatStr = array[4] + "/" + array[3] + "/" + array[5] + " " + array[0] + ":" + array[1] + ":" + array[2];
        }
        else {
            dateFormatStr = array[0] + ":" + array[1] + ":" + array[2] + " " + array[4] + "/" + array[3] + "/" + array[5];
        }
        return egov.commonFn.util.getCommonTime(new Date(dateFormatStr));
    }

    function getPositionEventTouch(e) {
        return e.originalEvent.changedTouches[0];
    }

    function getPositiveNumber(number) {
        /// <summary>
        /// Trả về 0 nếu số âm
        /// </summary>
        /// <param name="number">Số bất kỳ</param>
        /// <returns type="int">0 nếu số âm</returns>
        if (number < 0) {
            number = 0;
        }
        return number;
    }

    function getEmail(mail) {
        /// <summary>
        /// Trả về địa chỉ email
        /// </summary>
        /// <param name="mail"></param>
        /// <returns type=""></returns>
        var email = {
            a: "unknown",
            d: "unknown",
            p: "unknown",
        };

        if (mail.e) {
            email = mail.e.filter(function (obj) {
                return obj.t == "f";
            })[0];
        }

        return email;
    }

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

    return new Notification();
})

function showDocumentNotify() {
    egov.mobile.changeApp(appType.documents, function () {
        egov.mobile.notification.show();
    });
}

function showMailNotify() {
    egov.mobile.changeApp(appType.bmail, function () {
        egov.mobile.notification.show();
    });
}