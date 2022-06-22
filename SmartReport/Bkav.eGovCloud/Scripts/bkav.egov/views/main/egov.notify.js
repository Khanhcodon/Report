// Regex kiểm tra chuỗi hiện tại có phải là chuỗi ngày tháng theo định dạng không
var regexUtcDate = /\d{4}-[0-1]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-6]\d$/;
var isDesktopApp = false;

try {
    isDesktopApp = typeof window.external.CB_ShowNotify == "function";
} catch (e) {
    console.log(e);
}

(function ($, _, Backbone, desktopNotify) {
    "use strict";
    if (typeof $ === undefined) {
        throw "Chưa có thư viện jquery.";
    }

    if (typeof Backbone === undefined) {
        throw "Chưa có thư viện backbone.";
    }

    egov.notifyType = {
        eGov: 1,     // Notify bên xử lý văn bản
        mail: 2,     // Notify bên bmail
        chat: 3,     // Notify của bên chat
        calendar: 4, // Notify bên lịch
    };

    var notifyType = egov.notifyType;
    var currentTitle = document.title;

    ///Đối tượng notify model
    var notifyModel = Backbone.Model.extend({
        defaults: {
            NotificationId: 0, // Id thông báo
            NotificationType: 0, // Loại thông báo (theo egov.notifyType)
            Title: "", //Tiêu đề 
            Content: "", // Nội dung 
            SenderAvatar: "", // Avatar người gửi
            SenderUserName: "", // Username người gửi
            SenderFullName: "", // Fullname người gửi
            Date: "", // Ngày nhận thông báo (ứng với người nhận)
            DateFormat: "", // Ngày nhận thông báo theo định dạng
            ReceiveDate: "", // Ngày tạo thông báo (ứng với người tạo)
            ViewdDate: "", // Ngày xem thông báo
            IsViewed: false, // Trạng thái đã xem hay chưa (theo ngày xem có null hay không)

            //egov
            DocumentCopyId: 0, // Id văn bản thông báo

            //mail
            MailId: 0, //Id mail thông báo
            FolderId: 0, //FolderId của mail thông báo

            //chat
            ChatId: "", //
            ChatterJid: "",
            messageId: ""
        }
    });

    ///Danh sách đối tượng notify model
    var notifyList = Backbone.Collection.extend({
        model: notifyModel
    });

    var notifyView = Backbone.View.extend({
        icon: '', //Icon thông báo
        hasAlert: false,//Cho phép có hiển thị thông báo dưới màn hình hay không
        hasAlertNotify: false, //Check có hiển thị notify trên khung dưới màn hình hay không - do user có đồng ý cho phép thông báo trên màn hình hay không
        useCache: true, //Cho phép sử dụng cache
        oneOnly: false, //Cho phép chỉ hiện 1 thông báo duy nhất, các thông báo trước sẽ tự mất đi

        notificationManager: undefined, //Quản lý các api thông báo 
        currentNotify: undefined, //Thông báo (góc dưới màn hình) hiện tại
        listAlertNotify: [], //Danh sách thông báo (góc dưới màn hình) hiện tại
        unreads: 0, // Số thông báo chưa đọc
        lastId: 0, // Id thông báo cuối cùng trong danh sách (để so sánh, lấy dữ liệu trên server)
        notifyUrl: {
            gets: "",
            setViewed: "",
            hideNotify: "",
            addNotify: "",
        },

        events: {
            "click .btnCloseAll": "closeAll"
        },

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options"></param>

            this.el = options.el;

            this.notifyUrl = options.notifyUrl;
            this.icon = options.icon || this.icon;
            this.hasAlert = options.hasAlert || this.hasAlert;
            this.useCache = options.useCache || this.useCache;
            this.oneOnly = options.oneOnly || this.oneOnly;

            this.type = options.type || notifyType.eGov;
            this.init();
            this.initNotificationManager();
            this._bindEvent();
            return this;
        },

        init: function () {
            /// <summary>
            /// Khởi tạo data
            /// </summary>

            //Ghép icon vào template rồi gán vào el
            this.$el.html($.tmpl(notifyTemplate, { icon: this.icon }));

            //Khởi tạo danh sách thông báo từ cache (localStorage)
            var notifications = this.getFromCache();
            this.model = new notifyList(notifications);

            this._eventProps();
            this.render(false);

            this.getNotificationsFromServer();
            if (this.type == 2) {
                this.getMailNotifications();
            }
        },

        getFromCache: function () {
            /// <summary>
            /// Lấy thông báo từ cache 
            /// </summary>

            var result = [];
            if (this.useCache) {
                if (window.localStorage) {
                    var data = localStorage.getItem(this.el);
                    if (data) {
                        try {
                            result = JSON.parse(hashBase64.decode(data));
                            if (result) {
                                result = result.filter(function (item) {
                                    return result.UserId == currentUserId;
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

        saveLocache: function () {
            if (this.useCache) {
                if (window.localStorage) {
                    localStorage.removeItem(this.el);
                    this.model.models = this.model.models.sort(function (a, b) {
                        return a.get("NotificationId") > b.get("NotificationId");
                    })
                    var data = hashBase64.encode(JSON.stringify(this.model.models));
                    localStorage.setItem(this.el, data);
                }
            }
        },

        _bindEvent: function () {
            var that = this;
            $(window).bind('beforeunload', function () {
                that._hideAlertNotifies();
            });
            this.$el.hover(function () {
                /// <summary>
                /// Khi di chuột lên icon thông báo để hiển thị danh sách thông báo
                /// </summary>
                that._hideAlertNotifies();

                /// Đánh dấu tất cả là đã đọc
                that.setViewAll();

                /// Kiểm tra xem danh sách thông báo có rỗng không
                if (that.checkNotEmptyData()) {
                    /// Đặt lại khoảng thời gian nhận được thông báo
                    that.refreshNotifyTime();
                }
            });
        },

        _hideAlertNotifies: function () {
            /// <summary>
            /// Remove toàn bộ notify góc dưới màn hình
            /// </summary>
            if (this.listAlertNotify) {
                _.each(this.listAlertNotify, function (item) {
                    item.close();
                })
            }
            //if (this.currentNotify) {
            //    this.currentNotify.close();
            //}
        },

        _eventProps: function () {
            /// <summary>
            /// Định nghĩa các sự kiện ứng với model
            /// </summary>

            var that = this;

            this.model.on("add", function (newNotify) {
                that.bindItem(newNotify, true);
                that._showNumberNotify();
            });

            this.model.on("remove", function (model) {
                if (!model.get("IsViewed")) {
                    that.unreads--;
                    that._showNumberNotify();
                }
                that.saveLocache();
                that.hideNotify(model.get("NotificationId"));
            });
        },

        getNotificationsFromServer: function () {
            /// <summary>
            /// Lấy ra thông báo từ server
            /// </summary>
            var that = this;
            var notifications = [];
            $.ajax({
                url: String.format(this.notifyUrl.gets, this.type, this.lastId),
                type: "GET",
                dataType: "JSON",
                success: function (results) {
                    notifications = results;
                },
                error: function () {

                },
                complete: function () {
                    for (var i = 0; i < notifications.length; i++) {
                        that.addToModel(notifications[i], true)
                    }
                }
            })
        },

        getMailNotifications: function () {
            /// <summary>
            /// Lấy ra thông báo từ server
            /// </summary>
            var that = this;
            var notifications = [];
            $.ajax({
                url: "/Notification/GetLastestMails",
                type: "GET",
                dataType: "JSON",
                success: function (results) {
                    notifications = results;
                },
                error: function () {

                },
                complete: function () {
                    for (var i = 0; i < notifications.length; i++) {
                        that.addToModel(notifications[i], true)
                    }
                }
            });
        },

        render: function (hasAlert) {
            /// <summary>
            /// Hiển thị thông báo lên giao diện 
            /// </summary>
            /// <param name="hasAlert">Có hiển thị thông báo hay không?</param>

            var that = this;
            if (this.model.length > 0) {
                $.each(this.model.models, function () {
                    that.bindItem(this, hasAlert);
                });
                this._showNumberNotify();
            }
        },

        bindItem: function (model, hasAlert) {
            /// <summary>
            /// Gán thông báo lên giao diện cho mỗi item
            /// </summary>
            /// <param name="model"></param>
            /// <param name="hasAlert">Có hiển thị thông báo hay không?</param>
            var that = this, unread;
            var notifyItem = new notifyItemView({
                model: model,
                id: "notify_" + model.get("NotificationId"),
                className: "notify_" + this.type,
                type: this.type,
                parent: this
            });

            //Nếu chưa xem thông báo thì tăng lượng chưa đọc lên 1
            unread = !model.get("IsViewed");
            if (unread) {
                this.unreads++;
            }

            var $notification = this.$(".notification");
            if (that.oneOnly) {
                var classUsername = "u_" + model.get("SenderUserName").split("@")[0];
                var $existEl = $notification.children("." + classUsername);
                if ($existEl.length > 0) {
                    $existEl.remove();
                }
                notifyItem.$el.addClass(classUsername);
                $notification.prepend(notifyItem.$el);
            } else {
                //Gán ngược, thông báo nào mới được đưa lên đầu
                $notification.prepend(notifyItem.$el);
            }

            //Những đối tượng chưa đọc nếu được thiết lập cho phép hiển thị thông báo khung dưới màn hình sẽ được hiển thị
            if (hasAlert && unread) {
                this.alertNotify(notifyItem.model, function () {
                    //if (!notifyItem.model.get("IsViewed")) {
                    //    that.unreads--;
                    //}
                    that.openNotifyById(notifyItem.model.get("NotificationId"));
                    that._chatSetViewChatter(notifyItem.model.get("ChatterJid"));
                });
            }
        },

        renderEmptyItems: function () {
            /// <summary>
            /// Hiển thị không có thông báo nào
            /// </summary>
            var htmlEmpty = '<li class="notify-empty"><b>' + this._getMessageNotifyEmpty() + '</b></li>';
            this.$('.notification').empty().append(htmlEmpty);
        },

        addToModel: function (model, isGetFromServer) {
            /// <summary>
            /// Thêm thông báo vào model
            /// </summary>
            /// <param name="model"></param>
            /// <param name="isGetFromServer"></param>
            var that = this;
            if (!isGetFromServer && model.NotificationType != 1) {
                //Nếu là notify mail, chat thì ghi lại thông báo trên db
                $.ajax({
                    url: "/Notification/AddNotify",
                    type: "POST",
                    data: JSON.stringify(model),
                    contentType: "application/json"
                });

                if (model.IsNewChat && that.oneOnly) {
                    var notifyChatter = this.model.find(function (item) {
                        return item.get("ChatterJid") == model.ChatterJid;
                    });

                    if (notifyChatter) {
                        if (notifyChatter.get("IsViewed")) {
                            this.unreads++;
                            this._showNumberNotify();
                            notifyChatter.set("ViewdDate", "");
                            notifyChatter.set("IsViewed", false);
                        }
                        if (model.Title && model.Title.length > 0) {
                            notifyChatter.set("Title", model.Title);
                            notifyChatter.set("SenderFullName", model.Title);
                        }

                        notifyChatter.set("Date", model.Date);
                        notifyChatter.set("SenderAvatar", model.SenderAvatar);
                        notifyChatter.set("SenderUserName", model.SenderUserName);
                        notifyChatter.set("ReceiveDate", model.Date);
                        notifyChatter.set("Content", model.Content);

                        //this.$(".notification").prepend(notifyChatter.view.$el);
                        //notifyChatter.view.$el.remove();

                        if (this.hasAlert) {
                            this.alertNotify(notifyChatter, function () {
                                mainApps.openApp("chat", function () {
                                    window.focus();
                                    if (currentApp.btalk && currentApp.btalk.APPVIEW) {
                                        currentApp.btalk.APPVIEW.notificationClick({ chatterJid: model.ChatterJid });
                                        that._chatSetViewChatter(model.ChatterJid);
                                    }
                                });
                            });
                            return;
                        }
                    }
                }
            }
            if (model.Content == "") {
                model.Content = model.Title;
            }
            if (that.type == 2 && model.FolderLocation == "inbox") {
                model.FolderLocation = "";
            }
            this.model.add(model);
            this.saveLocache();
        },

        initNotificationManager: function () {
            /// <summary>
            /// Khởi tạo NotificationManager
            /// </summary>
            var that = this;
            //Nếu cấu hình cho phép hiển thị thông báo phía dưới màn hình thì xét tiếp
            if (this.hasAlert) {
                this.notificationManager = window.Notification || window.mozNotification || window.webkitNotification;
                if (this.notificationManager) {
                    var permission = this.notificationManager.permission;
                    if (permission == "default") {
                        //Người dùng cho phép site hiển thị thông báo hay không
                        this.notificationManager.requestPermission(function (status) {
                            that.hasAlertNotify = that.notificationManager.permission !== status;
                        });
                    } else {
                        that.hasAlertNotify = permission == "granted";
                    }
                }
            }
        },

        _showNumberNotify: function () {
            /// <summary>
            /// Hiển thị số lượng thông báo chưa đọc trên cái chuông
            /// </summary>

            var numberNotify = this.unreads;
            if (numberNotify > 0) {
                this.$('.notify-count').text(numberNotify > 99 ? '99+' : numberNotify)
                    .addClass('notify-count-avaiable').attr('data-count', numberNotify);
            } else {
                this.$('.notify-count').text('').attr('data-count', 0);
            }
        },

        setViewAll: function () {
            /// <summary>
            /// Đánh dấu tất cả các thông báo đã đọc
            /// </summary>

            var that = this;
            //Khi hover lần đầu tiên, vẫn hiển thị các thông báo chưa đọc trên giao diện bình thường, 
            //đồng thời đánh dấu các thông báo này là đã đọc trên server
            if (this.unreads > 0) {
                //Lưu vào cache trước khi đánh dấu trên Server
                var unreadModels = this.model.models.filter(function (item) {
                    return item.get("IsViewed") == false;
                });
                for (var i = 0; i < unreadModels.length; i++) {
                    unreadModels[i].set("IsViewed", true);
                }
                this.saveLocache();
                $.ajax({
                    url: that.notifyUrl.setViewed + that.type,
                    type: "POST",
                    dataType: "JSON"
                });
            }
            else {
                //Khi hover các lần tiếp theo, bỏ trạng thái chưa đọc của thông báo đi
                this.$(".unview").removeClass("unview");
            }
            this.unreads = 0;
            this._showNumberNotify();
        },

        setCurrentChatterJId: function (chatterjId) {
            if (this.type == 3) {
                this.chatterjId = chatterjId;
                this._closeChatAlert(chatterjId);
                this._chatSetViewChatter(chatterjId);
            }
        },

        _chatSetViewChatter: function (chatterjId) {
            var currentChatter = this.model.find(function (item) {
                return item.get("ChatterJid") == chatterjId;
            });
            if (currentChatter && !currentChatter.get("IsViewed")) {
                var that = this;
                $.ajax({
                    url: "/Notification/SetChatView?chatterJId=" + chatterjId,
                    type: "POST",
                    dataType: "JSON",
                    success: function (data) {
                        if (data.success) {
                            currentChatter.view.$el.children("a").removeClass("unview");

                            that.unreads--;
                            that._showNumberNotify();
                        }
                    }
                });
            }
        },

        refreshNotifyTime: function () {
            /// <summary>
            /// Cập nhật lại thời gian thông báo
            /// </summary>

            $.each(this.model.models, function () {
                this.set("DateFormat", new Date());
            });
        },

        checkNotEmptyData: function () {
            /// <summary>
            /// Kiểm tra danh sách notify có rỗng hay không
            /// </summary>

            var result = false;
            result = this.model.length > 0;
            if (result) {
                this.$(".headerrow").removeClass("hidden");
                this.$(".notification .notify-empty").remove();
            }
            else {
                this.$(".headerrow").addClass("hidden");
                this.renderEmptyItems();
            }
            return result;
        },

        hideNotify: function (notificationId) {
            /// <summary>
            /// Ẩn notify khi mở thông báo
            /// </summary>
            /// <param name="notificationId"></param>
            if (notificationId != 0) {
                $.ajax({
                    url: this.notifyUrl.hideNotify + notificationId,
                    type: "POST",
                    dataType: "JSON"
                });
            }
        },

        alertNotify: function (model, callback) {
            ///<summary>
            /// Tạo notify trên góc màn hình
            ///</summary>
            ///<param name="data">Đối tượng để tạo notify</param>

            if (model === undefined) {
                return;
            }
            console.log(model);
            if (isDesktopApp) {
                //if (true||this.type != 3) {
                var that = this;
                var account = model.get("SenderUserName");
                account = account.split("@")[0];
                try {
                    var jsonData = [
                    {
                        "info": {
                            "title": model.get("Title"),
                            "content": model.get("Content"),
                            "account": account,
                            "avatar": model.get("SenderAvatar"),
                            "fullname": model.get("SenderFullName"),
                            "chaterJId": model.get("ChatterJid"),
                            "duration": that.type == 3 ? 5 : 0,
                            "type": that.type,
                        },
                        "callback": "callbackfunc",
                        "callbackdata":
                        {
                            "notifyId": model.get("NotificationId"),
                            "type": that.type
                        }
                    }];
                    //console.log(jsonData);
                    window.external.CB_ShowNotify(JSON.stringify(jsonData));
                } catch (e) {
                    isDesktopApp = false;
                    console.log(e);
                }
                //}
                return;
            }

            if (!this.hasAlertNotify) {
                return;
            }
            var that = this;

            var notify = null;
            var chatterJid = model.get("ChatterJid");
            if (this.oneOnly) {
                this._closeChatAlert(chatterJid);
            }
            var notifyTime = ""; //getNotificationTime(model.get("ReceiveDate"));
            var titleAlert = this._getTitleAlert();
            this._flashTitleBar(titleAlert);
            var body = model.get("SenderFullName") + "\r\n" + model.get("Content") + "\r\n" + notifyTime;
            var folderLocation = model.get("FolderLocation");
            if (that.type == 2) {
                body += "" + "\r\n" + folderLocation;
            }
            notify = new this.notificationManager(titleAlert, {
                body: model.get("SenderFullName") + "\r\n" + model.get("Content") + "\r\n" + notifyTime,
                icon: model.get("SenderAvatar")
            });

            //Kêu âm báo
            playAudio();
            notify.chatterjId = chatterJid;
            if (typeof callback == "function") {
                notify.onclick = function () {
                    notify.close();
                    callback();
                };
            }

            this.listAlertNotify.push(notify);
        },

        _closeChatAlert: function (chatterJId) {
            //Ẩn notify theo chatterJId
            if (!chatterJId) {
                return;
            }
            notify = this.listAlertNotify.filter(function (item) {
                return item.chatterjId = chatterJId;
            })[0];
            if (notify) {
                this.listAlertNotify.splice(this.listAlertNotify.indexOf(notify), 1);
                notify.close();
            }
        },

        _flashTitleBar: function (titleAlert) {
            /// <summary>
            /// Để thanh tiêu đề nhấp nháy (flash title)
            /// </summary>
            /// <param name="titleAlert"></param>

            //Đặt thời gian nhấp nháy 1 giây
            var that = this;
            this.flashTitleBarTimmer = window.setInterval(function () {
                document.title = document.title == currentTitle ? titleAlert : currentTitle;
                $(window.currentApp).on("click", function () {
                    clearInterval(that.flashTitleBarTimmer);
                    document.title = currentTitle;
                });
            }, 1000);

            //Tắt nhấp nháy khi trỏ chuột vào trang (không làm focus or click)
            //$(window).one("mouseover", function () {
            //    that.stopFlashTitleBar();
            //});
            
        },

        _getMessageNotifyEmpty: function () {
            /// <summary>
            /// Lấy thông điệp không có thông báo tương ứng với mỗi loại ứng dụng
            /// </summary>

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
            /// <summary>
            /// Lấy string mở tất cả ứng với mỗi ứng dụng
            /// </summary>

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
            /// <summary>
            /// Lấy tiêu đề thông báo tương ứng với app
            /// </summary>
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
            /// <summary>
            /// Xóa thông báo khi mở văn bản trên danh sách
            /// </summary>
            /// <param name="id"></param>
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
            /// <summary>
            /// 
            /// </summary>

            if (!this.model || this.model.length <= 0) {
                return;
            }

            this.model.at(0).view.open(this.model.length === 1);
            this.openAll();
        },

        closeAll: function () {
            /// <summary>
            /// 
            /// </summary>
            if (!this.model || this.model.length <= 0) {
                return;
            }
            var that = this;
            var $item = that.$(".notification").children().first();

            var hideItem = function (item) {
                if (item && item.length > 0) {
                    item.slideUp(50, function () {
                        hideItem(item.next());
                    })

                }
            }
            hideItem($item);
            $.ajax({
                url: "/Notification/HideAllNotify?type=" + this.type,
                type: "POST",
                success: function (result) {
                    if (result) {
                        that.model = new notifyList([]);
                        that.checkNotEmptyData();
                        that.saveLocache();
                    }
                }
            });
        }
    });

    var notifyItemView = Backbone.View.extend({
        tagName: 'li',
        type: 0,
        events: {
            'click .open-notify': 'open',
            'click .close-notify': 'close'
        },

        initialize: function (option) {
            this.parent = option.parent;
            this.type = option.type;
            var avatar = this.model.get("SenderAvatar");
            if (!avatar) {
                //Lấy avatar theo username và avatarPath
                var username = this.model.get("SenderUserName").split("@")[0];
                avatar = String.format(egov.resources.avatar.path, username);
                this.model.set("SenderAvatar", avatar);
            }
            this.render();
            this._eventProps();
            //Gán giá trị new Date() để handle thay đổi DateFormat bên dưới
            this.model.set("DateFormat", new Date());
            this.model.view = this;
            return this;
        },

        render: function () {
            this.$el.html(this._parseHtml(this.model));
        },

        _eventProps: function () {
            var that = this;
            this.model.on("change:DateFormat", function (model) {
                /// <summary>
                /// Hiển thị lại thời gian thông báo
                /// </summary>
                /// <param name="model"></param>
                that.$(".notify-date").text(getNotificationTime(that.model.get("ReceiveDate")));

            });
            this.model.on("change:Content", function (model) {
                /// <summary>
                /// Hiển thị lại nội dung
                /// </summary>
                /// <param name="model"></param>
                that.$(".lblContent").text(that.model.get("Content"));
            });
            this.model.on("change:IsViewed", function (model) {
                /// <summary>
                /// Hiển thị lại thời gian thông báo
                /// </summary>
                /// <param name="model"></param>
                if (!that.model.get("IsViewed")) {
                    that.$el.children("a").addClass("unview");
                }
            });
            this.model.on("change:SenderAvatar", function (model) {
                /// <summary>
                /// Hiển thị lại ảnh
                /// </summary>
                /// <param name="model"></param>
                that.$(".img-circle").attr("src", that.model.get("SenderAvatar"));
            });
            this.model.on("change:Title", function (model) {
                /// <summary>
                /// Hiển thị lại ảnh
                /// </summary>
                /// <param name="model"></param>
                that.$(".notify-usersend").text(that.model.get("Title"));
                that.$("a.open-notify").attr("title", that.model.get("Title") + " - " + that.model.get("SenderUserName"));
            });

        },

        open: function () {
            ///<summary>
            /// Mở văn bản hoặc mail
            ///</summary> 
            window.focus();
            var type = this.type;
            this.remove();
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

        close: function (e) {
            if (e) {
                if (e.preventDefault) {
                    e.preventDefault();
                }
                else {
                    // fix cho ie 8
                    e.returnValue = false;
                }
                e.stopPropagation();
            }
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

            model.defaultAvatar = "'../../AvatarProfile/noavatar.jpg'";
            return $.tmpl(templateInBell, model);
        },

        _openDocument: function (hasLoadContent) {
            ///<summary>
            /// Mở văn bản
            ///</summary>
            var that = this
                , docCopyId = this.model.get("DocumentCopyId");

            if (hasLoadContent == null || hasLoadContent === undefined) {
                hasLoadContent = true;
            }

            mainApps.openApp("documents", function () {
                try {
                    var egov = currentApp.egov;
                    //TrinhNVd - 5/5/15: Mở văn bản Notify trên Tablet
                    if (!navigator.isMobile) {
                        egov.views.home.tab.openDocument(docCopyId, that.model.get('title'), hasLoadContent);
                    }
                    else {
                        if (egov.views && egov.views.home && egov.views.home.tab) {
                            egov.views.home.tab.removeDocument();
                        }

                        egov.require(['tabView'], function (TabView) {
                            var tabDocument = new TabView({
                                model: {
                                    id: docCopyId,
                                    name: that.model.get('title'),
                                    title: that.model.get('title'),
                                    href: 'tabDocument_' + docCopyId
                                }
                            });
                        });
                    }
                }
                catch (ex) {
                    var data = {
                        DocumentCopyId: docCopyId,
                        title: that.model.get('title')
                    };

                    $.cookie('documentNotify', JSON.stringify(data),
                        {
                            expires: 1
                            , domain: window.document.domain
                            , path: "/"
                            , secure: true
                        }
                    );
                }
            });
        },

        _openMail: function () {
            ///<summary>
            /// Mở mail
            ///</summary>
            var that = this;
            mainApps.openApp("bmail", function () {
                currentApp.readNewMail(that.model.get("MailId"), that.model.get("FolderId"));
            });
        },

        _openChat: function () {
            ///<summary>
            /// Mở chat
            ///</summary>
            var that = this;
            var chatterJid = that.model.get("ChatterJid");
            this.parent.setCurrentChatterJId(chatterJid);

            mainApps.openApp("chat", function () {
                if (currentApp.btalk && currentApp.btalk.APPVIEW) {
                    currentApp.btalk.APPVIEW.notificationClick({ chatterJid: chatterJid });
                }
            });
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
            /// <summary>
            /// 
            /// </summary>

            //Nếu là chat thì giữ nguyên lịch sử
            if (this.type != 3) {
                this._removeView();
            }
            this._closeAlertNotify();
        }
    });

    //#region các template của notify

    var notifyTemplate = '<a href="#" class="ripple-ef dropdown-toggle" data-toggle="dropdown">'
                        + '<img src="${icon}" />'
                        + '<span class="notify-count" data-count="0"></span>'
                        + '</a>'
                        + '<div class="dropdown-menu" role="menu">'
                        + '<div class="headerrow text-right hidden"><a href="#" class="btnCloseAll">Đóng tất cả</a></div>'
                        + '<ul class="notification"></ul>'
                        + '</div>';

    var templateInBell = '<a href="#" class="open-notify{{if IsViewed == false}} unview{{/if}}" title="{{if SenderFullName != null}}${SenderFullName} -{{/if}} ${SenderUserName}">'
                     + '    <div class="col-xs-2 col-md-2 col-sm-3">'
                     + '         <img class="img-circle" src="${SenderAvatar}" onerror="this.src=${defaultAvatar}">'
                     + '    </div>'
                     + '    <div class="col-xs-14 col-md-14 col-sm-13 textalert">'
                     + '         <div class="col-xs-15 col-md-15 col-sm-15" style="padding:0px">'
                     + '            <div class="notify-usersend wraptext">${SenderFullName}</div>'
                     + '            <div class="notify-content wraptext lblContent">${Content}</div>'
                     + '            <div class="row"><div class="col-sm-8"><span class="notify-date">${DateFormat}</span></div>'
                     + '            <div class="col-sm-8"><div class="full-width text-right wraptext notify-location">${FolderLocation}</div></div></div>'
                     + '         </div>'
                     + '       <div class="close-notify col-xs-1 col-md-1 col-sm-1">X</div>'
                     + '    </div>'
                     + '</a>';

    //#region khởi tạo notify
    var notifyUrl = {
        gets: "/Notification/GetsByType?type={0}&lastId={1}", //Lấy toàn bộ notify
        setViewed: "/Notification/SetViewed?type=",
        hideNotify: "/Notification/HideNotify/",
        addNotify: "/Notification/AddNotify",
    };
    // #endregion
    window.bmailNotify = undefined, window.eGovNotify = undefined, window.chatNotify = undefined;
    $.ajax({
        url: "/Notification/GetConfig",
        type: "GET",
        success: function (result) {
            if (result) {
                var config = JSON.parse(result);
                if (config.DocumentActived) {
                    //Notify bên egov
                    window.eGovNotify = new notifyView({
                        el: ".div-eGov-notify",
                        type: notifyType.eGov,
                        notifyUrl: notifyUrl,
                        icon: '../Content/bkav.egov/icon egov/egov-01.png',
                        oneOnly: false,
                        hasAlert: true,
                        useCache: true
                    });
                }
                if (config.MailActived) {
                    //Notify bên mail
                    window.bmailNotify = new notifyView({
                        el: ".div-bmail-notify",
                        type: notifyType.mail,
                        notifyUrl: notifyUrl,
                        icon: '../Content/bkav.egov/icon egov/egov-11.png',
                        oneOnly: false,
                        hasAlert: true,
                        useCache: true
                    });
                }
                if (config.ChatActived) {
                    //Notify bên mail
                    window.chatNotify = new notifyView({
                        el: ".div-chat-notify",
                        type: notifyType.chat,
                        notifyUrl: notifyUrl,
                        icon: '../Content/bkav.egov/icon egov/egov-10.png',
                        oneOnly: true,
                        hasAlert: true,
                        useCache: true
                    });
                }
            }
        }
    });

})(window.jQuery, window._, window.Backbone, window.notify);

function getVNDay(date) {
    switch (date.getDay()) {
        case 1:
            return getResource("egov.resources.time.mon");
            break;
        case 2:
            return getResource("egov.resources.time.tue");
            break;
        case 3:
            return getResource("egov.resources.time.wed");
            break;
        case 4:
            return getResource("egov.resources.time.thi");
            break;
        case 5:
            return getResource("egov.resources.time.fri");
            break;
        case 6:
            return getResource("egov.resources.time.sat");
            break;
        default:
            return getResource("egov.resources.time.sun");
    }
}

function playAudio(url) {
    try {
        var _this = this;
        if (!_this.isPlayAudio) {
            if (!url) {
                url = '../Content/Sound/notify.wav';
            }

            var audio = new Audio(url);
            audio.play();

            _this.isPlayAudio = true;
            window.setTimeout(function () {
                _this.isPlayAudio = false;
            }, _this.timeShowSoundNotify);
        }
    }
    catch (ex) { }
}

function getNotificationTime(date) {
    /// <summary>
    /// Lấy ngày xử lý thông thường
    /// </summary>
    /// <param name="date">Ngày truyền vào</param>
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
        return egov.resources.time.yesterday + ", " + date.format("HH:mm");
    }
    else if (date.weekOfYear() === dateNow.weekOfYear()) {
        return date.format("HH:mm") + ", " + getVNDay(date);
    }
    else if (date.getFullYear() === dateNow.getFullYear()) {
        return date.format("HH:mm dd/MM");
    }
    return date.format("HH:mm dd/MM/yyyy");
}

function callbackfunc(data) {
    if (data) {
        data = JSON.parse(data);
        var notifyId = data.notifyId;
        var type = data.type;
        if (type == 1) {
            eGovNotify.openNotifyById(notifyId);
        } else if (type == 2) {
            bmailNotify.openNotifyById(notifyId);
        } if (type == 3) {
            chatNotify.openNotifyById(notifyId);
        }
    }
}

function getBase64Image(account) {
    if (!account) {
        window.external.CB_SetBase64Avatar({
            account: account,
            base64Image: ""
        });
    }
    var base64 = "";
    $.ajax({
        url: "/Notification/GetBase64FromImageUrl",
        type: "GET",
        data: { account: account },
        success: function (result) {
            base64 = result;
        },
        error: function () {

        },
        complete: function () {
            var data = {
                account: account,
                base64Image: base64
            }
            window.external.CB_SetBase64Avatar(data);
        }
    })
}