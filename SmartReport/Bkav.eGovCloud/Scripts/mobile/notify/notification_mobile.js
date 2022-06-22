/*
 * TienBV@bkav.com
 * 
 * Thư viện xử lý notification eGov
 * Bao gồm quản lý việc hiển thị notify trên header, notify browser, notify desktop.
 * 
 * === PUBLISH APIS ================
 * 
 * - window.registerNotificationApp(notifyConfig): đăng ký app mới vào hệ thống notification
                    notifyConfig = defaultConfig ở dưới

 * - window.pushNotifyFromClient(notifyModel): đẩy notify vào trung tâm thông báo, trung tâm thông báo tự động hiển thị lên header và hiển thị notify theo app.
 *                      notifyModel = theo NotifyModel ở dưới
 * 
 * 
 * === REMARKS =====================
 * 
 * Hiển thị desktop notification:
 * - Chỉ hiển thị các notificaion chưa đọc.
 * 
 * Các sự kiện click notify:
 * - Click Icon trên header: Cập nhật trạng thái tất cả  notify về đã xem, ẩn hết các notify đang hiển thị trên browser hoặc desktop.
 * - Click Notify trên Browser, Desktop: ẩn notify, Xóa notify trên danh sách, gọi hàm callback.
 * - Click notify trên danh sách: Ẩn notify desktop, Xóa notify trên danh sách,  gọi hàm callback.
 * - Click CloseButton notify trên Browser, Desktop: ẩn notify, các thứ khác giữ nguyên.
 * - Click CloseButton trên danh sách: Ẩn notify desktop, xóa notify khỏi danh sách.
 * - Click xóa tất cả: xóa tất cả notify trên danh sách, ẩn tất cả notify desktop tương ứng.
 * 
 * Update lên server:
 * - Khi xảy ra việc xóa notify khỏi danh sách.
 */

/*
 * Danh sách cần tối ưu:
 * - Xử lý lại mỗi lần cập nhật sẽ cập nhật tất cả danh sách từ server:
 *   Sửa hàm fetch: bỏ lastId đi, mỗi lần get là get toàn bộ danh sách.
 * - Xử lý hiển thị lại thời gian, hiện đang hiện số âm: utils/bt.util.date.js.
 * - Xử lý request remove về cập nhật trạng thái delete = true thay vì xóa hẳn.
 * - Mỗi lần click mở danh sách notifications ra cần cập nhật lại toàn bộ thời gian.
 * - Cuộn xem tiếp.
 * - Avatar lỗi (chưa có ảnh đại diện thì hiển thị avatar mặc định).
 * - Tham khảo facebook xem nó làm thế nào áp dụng vào.
 */

define([egov.template.notifyItem], function (Template) {
    "use strict";


    var urlSettings = {
        getConfig: "/Notification/GetConfig",
        fetch: "/Notification/GetForMobile",
        getData: "/Notification/GetData",
        pulling: "/Notification/GetLastestMails",
        update: "/Notification/Update",
        removeAll: "/Notification/RemoveAll",
        remove: "/Notification/Remove",
        oldAll: "/Notification/OldAll",
        readAll: "/Notification/ReadAll",
        read: "/Notification/Read",
        setBmailFolder: '/Notification/SetMailFolderNotify'
    };

    /*
     * Notify trên web client
     * Nếu browser có hỗ trợ notification
     */
    var webClientNotify = function () {
        this._hasWebNotification = false;
        this._instance = null;
        this._notification = null;

        this._chromeNotificationTemplateType = {
            basic: "basic",     // Kiểu mặc định bao gồm title, message, icon, 2 nút bấm
            image: "image",     // Bao gồm kiểu basic và thêm preview 1 ảnh phía dưới
            list: "list",       // Bao gồm basic và hiển thị một danh sách notify thông qua danh sách items.
            progress: "progress"    // Hiển thị tiến độ xử lý: icon, title, message, progress, 2 nút bấm
        },
        this._chromeNotificationOptions = {
            type: "basic",      // templatetype mặc định
            iconUrl: "",        // link hình ảnh lớn của notify, có thể là: avatar người gửi, app icon, thumbnail image
            appIconMaskUrl: "", // Link icon của app
            title: "",          // Tiêu đề của thông báo
            message: "",        // Nội dung tin nhắn
            contextMessage: "",
            priority: 0,        // Độ ưu tiên từ -2 -> 2; -2 là thấp nhất; 0 là mặc định. Một số nền tảng không hỗ trợ -2, -1 do đó cần lưu ý khi sử dụng.
            eventTime: 0,       // Hẹn giờ hiển thị; ví dụ (Date.now() + n)
            buttons: [],
            imageUrl: "",       // Link preview một ảnh.
            items: [],          // Danh sách notify với templatetype là list: bao gồm các thông tin title và message
            // [{title: "", message: ""}, ...]
            progress: 0,        // Tiến độ xử lý từ 0 => 100 với kiểu templatetype là progress.
            isClickable: false, // Cho phép click vào body của notify
            requireInteraction: false   // Từ Chrome 50: chỉ ra rằng notify vẫn hiển thị trên màn hình đến khi người dùng click vào hoặc tắt nó đi. Mặc định là false
        },

        this.Instance = function () {
            if (!this._instance) {
                this._instance = new webClientNotify();
                this._checkBrowserNotification();
            }
            return this;
        };

        this.show = function (notify) {
            if (!this._hasWebNotification) {
                return;
            }
            var notify = this._create(notify);
            return notify;
        };

        this.showList = function (notifies) {

        },

        this._create = function (notify) {
            var title = notify.get("Title");
            var messsage = notify.get("Body");
            var icon = notify.get("Avatar");
            var notifyCallback = notify.Callback;
            var alternateContent = notify.get("alternateContent");

            if (alternateContent != undefined && alternateContent != "") {
                messsage = alternateContent;
            }

            var result = new this._notification(title, {
                body: messsage,
                icon: icon,
                silent: false,
                sound: '../Content/Sound/notify.wav',
                requireInteraction: false
            });

            result.onclick = function (e) {
                result.close();
                if (isFunction(notifyCallback)) {
                    notifyCallback(notify);
                }
            }

            return result;
        };

        this._checkBrowserNotification = function () {
            var that = this;

            this._notification = chrome.notifications || window.Notification || window.mozNotification || window.webkitNotification;

            if (!this._notification) {
                this._hasWebNotification = false;
            }

            var permission = this._notification.permission;

            if (permission == "default") {
                //Người dùng cho phép site hiển thị thông báo hay không
                this._notification.requestPermission(function (status) {
                    that._hasWebNotification = that.notificationManager.permission !== status;
                });
            } else {
                this._hasWebNotification = permission == "granted";
            }
        };
    };

    // #region Default Apps

    var defaultConfig = {
        "mail": {
            name: "bmail",                       // Tên app
            isActive: false,
            displayName: "Thông báo mail",
            el: ".div-bmail-notify",
            icon: '../Content/bkav.egov/icon egov/egov-11.png', // icon hiển thị lên header
            announIcon: '../Content/bkav.egov/icon egov/egov-16.png',  // Icon thông báo khi notify
            total: 0,                           // mặc định
            model: [],                          // mặc định
            callback: {                         // Xử lý khi click vào notify
                frameName: "",                  // Tên frame
                api: "readNewMail"              // Tên hàm gọi callback trong frame
            },
            isValidData: function (mailData) {
                var title = mailData.title,
                    folder = mailData.folderLocation,
                    result = false,
                    followFolders = this.followFolders;

                if (title.indexOf('[Spam]') >= 0) {
                    return result;
                }

                if (folder.indexOf("/") != 0) {
                    folder = "/" + folder;
                }

                if (_.indexOf(followFolders, folder) < 0) {
                    return result;
                }

                return true;
            }
        },
        "egov": {
            name: "documents",
            isActive: false,
            displayName: "Thông báo xử lý văn bản",
            el: ".div-eGov-notify",
            icon: '../Content/bkav.egov/icon egov/egov-01.png',
            announIcon: '../Content/bkav.egov/icon egov/egov-14.png',
            total: 0,
            model: [],
            callback: {
                frameName: "",
                api: "openNotify"
            }
        },
        "chat": {
            name: "chat",
            frameName: "",
            isActive: false,
            displayName: "Thông báo chat",
            el: ".div-chat-notify",
            icon: '../Content/bkav.egov/icon egov/egov-10.png',
            announIcon: '../Content/bkav.egov/icon egov/egov-10.png',
            total: 0,
            model: [],
            callback: {
                frameName: "",
                api: "openChat"
            }
        }
    };

    //#endregion

    // #region Model

    var NotifyModel = Backbone.Model.extend({
        defaults: {
            NotificationId: 0,
            UserId: 0,
            Title: "",      // Tiêu đề thông báo
            Body: "",       // Nội dung thông báo
            alternateContent: "", // Nội dung thay thế để hiển thị notification desktop khi mà nội dung chính là html
            Avatar: "",     // Ảnh đại diện
            DateCreated: "",    // Ngày tháng
            AppName: "",    // Tên app
            IsNew: true,    // Trạng thái mới
            IsReaded: false, // Trạng thái đã xem hay chưa
            OnlyAnnoun: false,  // Trạng thái xác định notify chỉ để thông báo, ko xử lý.
            IsCreatedByClient: false,  // Trạng thái xác định notify được thêm mới ở client
            JsonData: {}    // Dữ liệu notify: dùng để truyền vào hàm callback
        }
    });

    var NotifyList = Backbone.Collection.extend({
        model: NotifyModel
    });

    //#endregion

    var pageIndex = 1;
    var pageSize = 10;
    var isLoadmore = true;

    var EgovNotificationView = Backbone.View.extend({
        el: "#notificationFrame",
        notificationList: $("#notificationList"),
        notificationBell: $(".notifyBell"),
        //tiennvg them vao
        events: {
            'change #sl_type_notification': 'onChangeTypeNotify',
            'click .close-notify': '_remove',
            'click .notify-item': '_openNotify'
        },

        _isDesktop: false,

        _isInt: function (value) {
            var rex = /^-?[0-9]+$/;
            return rex.test(value);
        },

        // Danh sách các app được đăng ký hiển thị notify
        _registedApps: {},

        template: Template,
        model: new NotifyList,

        initialize: function (options) {
            var that = this;
            that.lastId = 0;

            this._getNotificationConfig(function (config) {
                that._registedApps = config;
                that.render();
            });
        },

        render: function () {
            // this.$el.attr('style', 'padding-bottom:' + $(".egov-footer").innerHeight() + 'px');

            this.fetch();

            this._pulling();

            this._bindEvents();

            this._registerApiToGlobal();

            this._registerTriggers();

            if (egov.openingNotify && egov.openingNotify.AppName) {
                var notifyModel = new NotifyModel(egov.openingNotify);
                var currentApp = this._getApp(notifyModel.get("AppName"));

                var interval = setInterval(function () {
                    if(btalk && btalk.ROSTER && typeof btalk.ROSTER.getByUsername === 'function'){
                        this._notifyCallback(currentApp, notifyModel);
                        clearInterval(interval);
                    }
                }.bind(this), 200);
            }
        },

        onChangeTypeNotify: function () {
            isLoadmore = true;
            this.fetch();

        },

        reset: function () {
            this.fetch();
        },

        //#region Initialize

        fetch: function () {
            var scopeItems;
            var that = this;

            var typeNotification = this.$('#sl_type_notification').val();

            if (!this._isInt(typeNotification)) {
                typeNotification = '0';
            }

            scopeItems = pageIndex * 10;  //muc dich khi interval auto reload notify or change typeNotification, van lay du so ban ghi da scroll truoc do

            $.ajax({
                url: urlSettings.fetch,
                beforeSend: function () {
                    egov.mobile.showProcessBar();
                },
                data: {
                    typeNotification: typeNotification,
                    pageSize: scopeItems
                },
                success: function (data) {
                    data = data.reverse();
                    success(data);
                },
                complete: function () {
                    egov.mobile.hideProcessBar();
                }
            });

            var success = function (data) {
                if (data.length > 0) {
                    that.lastId = _.last(data).NotificationId;

                    that.model.set(data);

                    that._bindNotificationItems();
                    that._changeNotifyTotal();
                } else {
                    that._showNoElementPage();
                    that.model.set([]);
                    return;
                }
            };
        },

        _pulling: function () {
            var that = this;

            // Chổ này đang để tạm vậy
            // Cần đưa vào service
            setInterval(function () {
                $.ajax({
                    url: urlSettings.pulling
                });
            }, 1 * 60 * 1000);

            // 1ph sẽ query lên server lấy notify mới một lần.
            setInterval(function () {
                that.fetch();
            }, 1 * 60 * 1000);
        },

        _getNotificationConfig: function (success) {
            var that = this;
            var config = egov.setting.user.notifyConfig;

            if (!config) {
                success(null);
                return;
            }

            var result = [];

            if (config.hasshowdocumentnotify) {
                var egovApp = defaultConfig.egov;
                egovApp.isActive = true;
                result[egovApp.name] = egovApp;
            }

            if (config.hasshowmailnotify) {
                var mailApp = defaultConfig.mail;
                mailApp.isActive = true;
                mailApp.followFolders = config.mailfoldernotify === undefined ? [] : config.mailfoldernotify.toLowerCase().split(',');
                result[mailApp.name] = mailApp;
            }

            if (config.hasshowchatnotify) {
                var chatApp = defaultConfig.chat;
                chatApp.isActive = true;
                result[chatApp.name] = chatApp;
            }

            that.RemoveRead = config.removeread;
            that.HasShowDesktop = config.hasshowdesktop;
            that.HasPlaySound = config.hasplaysound;

            success(result);
            return;
        },

        //#endregion

        //#region Notification List

        _bindNotificationItems: function () {
            var that = this, notifyItems = [];
            this._hideNoElementPage();

            var todayNofifies = [];
            var oldNotifies = [];
            this.model.each(function (n) {
                var date = new Date(n.get("DateCreated"));
                if (date.isToday()) {
                    todayNofifies.push(n.toJSON());
                } else {
                    oldNotifies.push(n.toJSON());
                }
            });

            if (todayNofifies.length > 0) {
                this.notificationList.find('div[data-group="new"] ul').html($.tmpl(this.template, todayNofifies.reverse()));
            } else {
                this.notificationList.find('div[data-group="new"] ul').html('<li class="mdl-list__item">Không có thông báo mới.</li>');
            }

            if (oldNotifies.length > 0) {
                this.notificationList.find('div[data-group="old"] ul').html($.tmpl(this.template, oldNotifies.reverse()));
            } else {
                this.notificationList.find('div[data-group="old"] ul').html('<li class="mdl-list__item">Không có thông báo nào.</li>');
            }

            this.$(".mdl-list__item-text-body").dotdotdot();
        },

        _changeNotifyTotal: function (currentApp) {
            var unread = this.model.where({ IsNew: true }).length;
            if (unread === 0) {
                this.notificationBell.removeAttr("data-badge");
                return;
            }

            this.notificationBell.attr("data-badge", unread > 9 ? "9+" : unread);
        },

        _removeNotifyElement: function (notifyModel) {
            var appName = notifyModel.get("AppName");
            var currentApp = this._getApp(appName);

            if (notifyModel.view) {
                notifyModel.view.close();
            }

            var element = this._findNotificationElement(notifyModel);
            if (element) {
                element.remove();
            }
        },

        _showNoElementPage: function (icon, message) {
            this.$(".no-element-content").removeClass("hidden");
            // this.$(".no-element-content .material-icons").text(icon);
            // this.$(".no-element-content .message-info").text(message);
            this.$(".page-content").addClass("hidden");
        },

        _hideNoElementPage: function () {
            this.$(".no-element-content").addClass("hidden");
            this.$(".page-content").removeClass("hidden");
        },

        //#endregion

        _registerApiToGlobal: function () {
            /*
             * Đăng ký các api ra global cho các ứng dụng khác sử dụng.
             */

            var that = this;

            /*
             * Hàm callback khi click vào notify ở các phần mềm ngoài như eGov Desktop
             */
            window.openNotifyFromExternal = function (data) {
                if (typeof data === "string") {
                    data = JSON.parse(data);
                }

                var notifyModel = that.model.detect(function (n) {
                    return n.get("NotificationId") == data.notificationId;
                });

                if (notifyModel.length == 0) {
                    return;
                }

                var currentApp = that._getApp(notifyModel.get("AppName"));

                that._openNotify(currentApp, notifyModel);
            }

            /*
             * API: Cho phép các app đăng ký thêm 1 loại notification app của mình.
             */
            window.registerNotificationApp = function (notifyConfig) {
                var appName = notifyConfig.AppName;
                var app = that._getApp(appName);
                if (app != undefined) {
                    throw "AppName đã có trong hệ thống";
                }

                that._registedApps[appName] = notifyConfig;
                that.render();
            };

            /*
             * API: cho phép các app đẩy 1 notify lên.
             * Cần gọi window.registerNotificationApp trước khi gọi hàm này.
             */
            window.pushNotifyFromClient = function (notifyModel) {
                var newNotifications = [];
                newNotifications.push(notifyModel);

                that._postToServer(newNotifications);

                if (notifyModel.IsReaded) {
                    // Hiển thị luôn xuống client

                    that._removeSimilarBeforeAdd(notifyModel);

                    if (typeof notifyModel !== "NotifyModel") {
                        notifyModel = new NotifyModel(notifyModel);
                    }
                    that.model.add(notifyModel);
                }
            };

            /*
             * API: cho phép nhận notify từ server qua các thư viện realtime: SignalR,...
             */
            window.pushNotifyFromServer = function (notifyModel) {

                that._removeSimilarBeforeAdd(notifyModel);

                if (typeof notifyModel !== "NotifyModel") {
                    notifyModel = new NotifyModel(notifyModel);
                }
                that.model.add(notifyModel);
            };

            /*
             * API: cho phép nhận notify từ bmail client
             */
            window.addBmailNotify = function (bmailData) {
                var mailApp = that._getApp("bmail");
                if (mailApp == undefined) {
                    return;
                }

                if (!mailApp.isValidData(bmailData)) {
                    return;
                }

                var mailData = {
                    NotificationType: 2,
                    Title: bmailData.fullName,
                    Content: bmailData.title,
                    SenderAvatar: bmailData.avatar,
                    SenderUserName: bmailData.userName,
                    SenderFullName: bmailData.fullName,
                    Date: bmailData.date,
                    ReceiveDate: new Date(),
                    MailId: bmailData.mailId,
                    FolderId: bmailData.folderId,
                    FolderLocation: bmailData.folderLocation
                };

                var notify = {
                    NotificationId: 0,
                    Title: parseMailTitle(bmailData.fullName, bmailData.folderLocation),
                    GroupId: bmailData.mailId,
                    Body: bmailData.title,
                    Avatar: bmailData.avatar,
                    DateCreated: bmailData.date,
                    IsNew: true,
                    AppName: "bmail",
                    JsonData: JSON.stringify(mailData)
                };

                var newNotifications = [];
                newNotifications.push(notify);

                that._postToServer(newNotifications);
            }

            /*
             * Sự kiện khi click đọc mail ở bmail, gọi ra hàm này để set notify đã đọc.
             */
            window.setMailRead = function (mailId) {
                var mailNotifyModel = that.model.detect(function (n) {
                    return n.get("GroupId") === mailId;
                });

                if (mailNotifyModel != undefined) {
                    mailNotifyModel.set("IsReaded", true);
                }

                // Xem xét có clear luôn những mail có cùng folder không?
                // Clear luôn thì có vẻ không đúng vì người ta chưa đọc.
                // Còn nếu click đọc thì sẽ nhảy vào hàm này để xét trạng thái luôn.
            }

            /*
             * API: Nhận chat notify từ chat client
             * Xử lý tạm: nhẽ ra chat phải tự registerNotificationApp và gọi pushNotifyFromClient mới đúng.
             */
            window.addChatNotify = function (chatData) {
                // chatdata.originalContent: nội dung chat gốc.
                // chatData.content: nội dung chat sau khi parse emoticon

                var notifyData = {
                    NotificationType: 3,
                    Title: chatData.title,
                    Content: chatData.content.split("\r\n")[0],
                    SenderAvatar: chatData.avatar,
                    SenderUserName: chatData.sender,
                    SenderFullName: chatData.title,
                    Date: chatData.date,
                    ReceiveDate: new Date(),

                    //chat
                    ChatId: chatData.messageId, //
                    ChatterJid: chatData.chatterJid,
                    messageId: chatData.messageId,
                    UserId: chatData.userId,
                    IsNewChat: true
                };

                //Reply message
                if (notifyData.Content.indexOf("&amp;message=") >= 0) {
                    notifyData.Content = notifyData.Content.split("&amp;message=")[1];
                }

                notifyData.Content = escape(notifyData.Content);

                var isRead = chatData.isRead | false;
                var isNew = !(chatData.isMe | isRead);
                var content = notifyData.Content;

                var notify = {
                    NotificationId: 0,
                    Title: notifyData.Title,
                    UserId: chatData.userId,
                    Body: content,
                    alternateContent: notifyData.originalContent,
                    Avatar: chatData.avatar,
                    DateCreated: chatData.date,
                    AppName: "chat",
                    GroupId: chatData.chatterJid,
                    IsViewed: false,
                    IsReaded: isRead == true ? true : false,
                    IsNew: true,
                    JsonData: JSON.stringify(notifyData)
                };

                pushNotifyFromClient(notify);
            }

            /*
             * Sự kiện khi click xem chat
             */
            window.eGovReadChat = function (chatterjId) {
                that.model.each(function (n) {
                    if (n.get("AppName") == "chat" && n.get("GroupId") == chatterjId) {
                        n.set("IsReaded", true);
                    }
                });
            }

            //method mobileApp goi mo notification tuong ung
            window.openNotifyToApp = function (notificationId, appName, jsonData) {
                var currentApp = that._getApp(appName);

                var notifyData = JSON.parse(jsonData);
                var notifyModel = new NotifyModel({
                    AppName: appName,
                    JsonData: jsonData,
                    GroupId: notifyData.GroupId,
                    MailId: notifyData.MailId
                });

                that._notifyCallback(currentApp, notifyModel);
            };

        },

        _registerTriggers: function () {
            var that = this;

            this.model.on("update", function (collection, changed) {
                // Added
                var addedNotifications = changed.changes.added;
                if (addedNotifications.length > 0) {
                    var hasShowDesktop = that.HasShowDesktop;

                    var notifyNews = _.filter(addedNotifications, function (n) {
                        return n.get("IsNew") && n.get("NotificationId") > that.lastId;
                    });

                    if (notifyNews.length > 2) {
                        var appName = notifyNews[0].get("AppName");
                        that._showAnnounMessage(appName, "Bạn có " + notifyNews.length + " thông báo mới.");
                        hasShowDesktop = false;
                    }

                    addedNotifications.forEach(function (nNotify) {
                        nNotify.set("DateCreated", parseServerDate(nNotify.get("DateCreated")).relativeDate());
                        var newId = nNotify.get("NotificationId");

                        if (that.lastId < newId) {
                            that.lastId = newId;
                        }

                        nNotify.set("Body", unescape(nNotify.get("Body")));
                    });
                }

                //// Removed
                var removedNotifications = changed.changes.removed;
                if (removedNotifications.length > 0) {
                    removedNotifications.forEach(function (nNotify) {
                        that._removeNotifyElement(nNotify);
                    });
                }
            });

            this.model.on("change:IsReaded", function (notification, changed) {
                if (that.RemoveRead) {
                    that.model.remove(notification);

                    $.ajax({
                        url: urlSettings.remove,
                        type: "Post",
                        data: { id: notification.get("NotificationId") }
                    });
                    return;
                }

                var element = that._findNotificationElement(notification);
                if (element.length > 0) {
                    element.removeClass("unview");
                }

                if (notification.view) {
                    notification.view.close();
                }

                if (notification.get("IsNew")) {
                    notification.set("IsNew", false);

                    $.ajax({
                        url: urlSettings.read,
                        type: "Post",
                        data: { id: notification.get("NotificationId") }
                    });
                }
            });

            this.model.on("change:IsNew", function (notification, changed) {
                var currentApp = that._getApp(notification.get("AppName"));
                that._changeNotifyTotal(currentApp);
            });
        },

        //#region Events

        _bindEvents: function () {
            var that = this;

            that.$(".btnCloseAll").click(function (e) {
                that._removeAll(e);
            });

            that.$(".btnReadAll").click(function (e) {
                that._readAll(e);
            });

            //TienNVg tam thoi comment lai, chua thay bind event + k thay dung debound dan den scroll nhanh co the sinh request lien tuc ma client chua kip xu ly
            //that.$("ul").scroll(function (e) {
            //    that._scrollToGetData(e);
            //});

            that.notificationBell.click(function () {
                that._setOlds();
            });

            //register event scroll element notification 
            that.$("#ct_scroll").scroll(_.debounce(function (e) { that.scroll_loadMore(e); }, 100));
        },

        _setOlds: function () {
            this.model.each(function (notify) {
                if (notify.get("IsNew") == true) {
                    notify.set("IsNew", false);
                }
            });

            this._changeNotifyTotal();

            $.ajax({
                url: urlSettings.oldAll,
                type: 'Post',
                data: {
                    appName: "",
                    lastId: this.lastId
                }
            });
        },

        _removeAll: function (e) {
            this.model.set([]);
            $.ajax({
                url: urlSettings.removeAll,
                type: 'Post',
                data: {
                    appName: "",
                    lastId: this.lastId
                },
                success: function (result) {
                    return result && this.$('.notify-item').remove();
                }
            });
        },

        _readAll: function (e) {
            this.model.each(function (notify) {
                if (notify.get("IsReaded") == false) {
                    notify.set("IsReaded", true);
                }
            });

            $.ajax({
                url: urlSettings.readAll,
                type: 'Post',
                data: {
                    appName: "",
                    lastId: this.lastId
                }
            });
        },

        _remove: function (e) {
            var target = $(e.target).closest("li");
            var notificationId = target.attr("data-id");

            var notifyModel = this.model.detect(function (n) {
                return n.get("NotificationId") == notificationId;
            });

            this.model.remove(notifyModel);
            var that = this;

            if (notificationId > 0) {
                $.ajax({
                    url: urlSettings.remove,
                    type: "Post",
                    data: { id: notificationId },
                    success: function (result) {
                        return result && that.$('.notify-item[data-id="' + notificationId + '"]').remove();
                    }
                });
            }
        },

        //#endregion

        //#region Hiển thị notify desktop

        /*
         * Hiển thị notify thông báo
         */
        _showAnnounMessage: function (appName, body) {
            //Todo: Xem có trường hợp thông báo không thuộc app nào thì thế nào?
            // Có thể chỉ cần truyền Icon chổ này cũng dc.
            var app = this._getApp(appName);

            var notify = new NotifyModel({
                NotificationId: 0,
                Title: "Thông báo",
                Body: body,
                AppName: appName,
                Avatar: app.AnnounIcon,
                OnlyAnnoun: true,
                JsonData: {}
            });

            this._showDesktop(notify);
        },

        /*
         * Xử lý hiển thị notify
         */
        _showDesktop: function (notify) {
            // Chỉ hiển thị notify desktop với notify mới
            if (notify.get("IsNew") == false) {
                return;
            }

            var that = this;
            if (this._egovTabIsActive()) {
                notify.view = this._showNotifyInBrowserTab(notify);
                return;
            }

            notify.Callback = function (notify) {
                that._openNotifyDesktop(notify);
            }

            notify.view = new webClientNotify().Instance().show(notify);
        },

        _showNotifyInBrowserTab: function (notify) {
            if (this.HasPlaySound) {
                playAudio();
            }

            // Xử lý hiển thị thông báo inbrowser ở góc ứng dụng.

        },

        //Xử lý mở notification trên app
        _openNotifyApp: function (notifyModel) {
            var that = this;
            var currentApp = that._getApp(notifyModel.get("AppName"));
            that._openNotify(currentApp, notifyModel);
        },

        //#endregion

        //#region Notification Click Callback

        /*
         * Sự kiện click vào notify
         */
        _openNotifyDesktop: function (notify) {
            if (notify == null) {
                return;
            }

            var currentApp = this._getApp(notify.get("AppName"));
            if (!currentApp || currentApp.isActive === false) {
                throw "App name không đúng.";
            }

            if (notify.get("OnlyAnnoun")) {
                this._activeTab();
                // this._showNotificationLists(currentApp);
                return;
            }

            var currentApp = this._getApp(notify.get("AppName"));
            if (!currentApp || currentApp.isActive === false) {
                throw "App name không đúng.";
            }

            this._openNotify(currentApp, notify);
        },

        _openNotify: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");
            var notificationId = target.attr("data-id");

            var currentApp = this._getApp(appName);
            var notifyModel = this.model.detect(function (n) {
                return n.get("NotificationId") == notificationId;
            });

            this._openNotifyInApp(currentApp, notifyModel);
        },

        _openNotifyInApp: function (currentApp, notifyModel) {
            if (!notifyModel) {
                return;
            }

            this._activeTab();
            notifyModel.set("IsReaded", true);

            this._notifyCallback(currentApp, notifyModel);
        },

        _notifyCallback: function (currentApp, notify) {
            var frameWindow, callback, frameLoadedCallback;

            if (!currentApp.callback || !currentApp.callback.api || currentApp.callback.api === "") {
                return;
            }

            var appName = notify.get('AppName');
            if (appName === 'chat') {
                var sender = btalk.ROSTER.getByUsername(JSON.parse(notify.get('JsonData')).SenderUserName);
                egov.pubsub.publish("message.open", { chatterid: sender.get('jid') });
                return;
            }

            if (appName === 'documents') {
                var data = JSON.parse(notify.get('JsonData'));
                egov.pubsub.publish("documents.open", data.DocumentCopyId);
                return;
            }

            if (appName === 'bmail') {
                egov.pubsub.publish("bmail.open", { chatterid: notify.get('MailId') });
                return;
            }
        },

        //#endregion

        //#region Update Server

        _postToServer: function (addeds) {
            if (addeds.length === 0) {
                return;
            }

            $.ajax({
                url: urlSettings.update,
                type: "POST",
                data: { addeds: JSON.stringify(addeds) },
                success: function (result) { },
                error: function () {

                }
            });
        },

        //#endregion

        //#region Hàm khác

        /*
         * Trả về giá trị xác định đang active eGov tab
         */
        _egovTabIsActive: function () {
            return false;
        },

        _activeTab: function () {
            window.focus();
        },

        _getApp: function (appName) {
            return this._registedApps[appName];
        },

        _findNotificationElement: function (notifyModel) {
            var element = this.notificationList.find("#" + notifyModel.get("NotificationId"));
            return element;
        },

        _removeSimilarBeforeAdd: function (nNotify) {
            var similars = this.model.select(function (n) {
                return n.get("GroupId") == nNotify.GroupId;
            });

            if (similars.length > 0) {
                this.model.remove(similars);
            }
        }

        //#endregion        
    });

    function isDesktopApp() {
        var result = false;
        try {
            result = typeof window.external.CB_ShowNotify == "function";
        } catch (e) {
            console.log(e);
        }

        return result;
    }
    function isFunction(func) {
        return typeof func === "function";
    }
    function playAudio(url) {
        try {
            var that = this;
            if (!url) {
                url = '../Content/Sound/notify.wav';
            }

            var audio = new Audio(url);
            audio.play();
        }
        catch (ex) { }
    }
    function parseServerDate(date) {
        if (typeof date === "string") {
            return new Date(date.replace(/T/i, ' '));
        }

        return new Date(date);
    }
    function parseMailTitle(title, folderPath) {
        if (folderPath == undefined || folderPath === "") {
            return title;
        }

        var folder = _.last(folderPath.split('/'));

        return title + " đã gửi vào " + parseMailFolderName(folder);
    }
    function parseMailFolderName(name) {
        name = name.toLowerCase();
        if (name === "inbox") {
            return "Hộp thư đến";
        }

        if (name === "sent") {
            return "Hộp thư đi";
        }

        if (name === "junk") {
            return "Thư đã xóa";
        }

        if (name === "drafts") {
            return "Thư rác";
        }

        return name;
    }

    return EgovNotificationView;
    // window.eGovNotification = new EgovNotificationView;
});