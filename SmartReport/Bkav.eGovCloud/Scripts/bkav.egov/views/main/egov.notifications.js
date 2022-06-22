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

(function ($, _, Backbone, desktopNotify) {
    "use strict";


    var urlSettings = {
        getConfig: "/Notification/GetConfig",
        // fetch: "/Notification/GetNotSents",
        fetch: "/Parallel/Notification_GetNotSents",
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
                requireInteraction: true
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

    /*
     * Notify trên bản desktop
     * Nếu đang chạy app trên bản desktop
     */
    var desktopClientNotify = function () {
        this._isDesktop = false;

        this._instance = null;

        this.type = 3;

        this.Instance = function () {
            if (!this._instance) {
                this._instance = new desktopClientNotify();
                this._isDesktop = this.checkDesktopApp();
            }

            return this._instance;
        };

        this.show = function (notify) {
            var that = this;
            var title = notify.get("Title");
            var messsage = notify.get("Body");
            var icon = window.location.origin + "/logo.png";
            var notifyCallback = notify.Callback;
            var data = notify.get("JsonData");
            var senderName = data.SenderUserName;
            var alternateContent = notify.get("alternateContent");

            if (alternateContent != undefined && alternateContent != "") {
                messsage = alternateContent;
            }

            try {
                var jsonData = [
                {
                    "info": {
                        "title": title,
                        "content": messsage,
                        "account": "egov",
                        "avatar": icon,
                        "fullname": title,
                        "duration": that.type == 3 ? 5 : 0,
                        "type": that.type,
                    },
                    "callback": notifyCallback,
                    "callbackdata":
                    {
                        "notificationId": notify.get("NotificationId"),
                        "type": that.type
                    }
                }];

                window.external.CB_ShowNotify(JSON.stringify(jsonData));
            } catch (e) {
                this._isDesktop = false;
                console.log(e);
            }
        };

        this.checkDesktopApp = function () {
            return isDesktopApp();
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
            },
            isFocus: function () {
                return $(".menu-items .bmail").is(".active");
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
                frameName: "documents",
                api: "openNotify"
            },
            isFocus: function () {
                return $(".menu-items .egov").is(".active");
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
            },
            isFocus: function () {
                return $(".menu-items .chat").is(".active");
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

    //#region Templates

    var notifyTemplate = ' <a href="#" class="ripple-ef dropdown-toggle" data-toggle="dropdown">'
                        + '     <img src="${icon}" />'
                        + '     <span class="notify-count" data-count="0"></span>'
                        + '</a>'
                        + '<div class="dropdown-menu" role="menu">'
                        + '     <div class="headerrow text-right">'
                        + '         <a href="#" class="btnReadAll">Đánh dấu tất cả là đã đọc</a>'
                        + '         <a href="#" class="btnCloseAll">Xóa tất cả</a>'
                        + '     </div>'
                        + '     <ul class="notification list-group"></ul>'
                        + '     <div class="text-center loading-image"></div>'
                        + '</div>';

    var templateInBell = '<li id="${NotificationId}" app="${AppName}" class="list-group-item {{if IsReaded == false}} unview {{/if}}">'
                     + '  <a href="#" class="open-notify row">'
                     + '    <div class="col-md-3">'
                     + '        <img src="${Avatar}" class="avatar"/>'
                     + '    </div>'
                     + '    <div class="col-md-13">'
                     + '         <div>'
                     + '            <div class="notify-usersend">${Title}</div>'
                     + '            <div class="notify-content">{{html Body}}</div>'
                     + '            <div class="notify-date">${DateCreated}</div>'
                     + '         </div>'
                     + '    </div>'
                     + '    <span class="close-notify" title="Xóa thông báo">x</span>'
                     + '    </a></li>';

    var loadingTemplate = '<div id="progress" style="display:none">'
                          + '<h4>Loading...</h4>'
                          + '</div>';
    var pageIndex = 0;
    var pageSize = 10;

    //#endregion

    var EgovNotificationView = Backbone.View.extend({
        el: "#notificationCenter",

        events: {
            "click .notifyBell": "_setOlds"
        },

        _isDesktop: false,


        // Danh sách các app được đăng ký hiển thị notify
        _registedApps: {},

        template: notifyTemplate,
        model: new NotifyList,

        initialize: function (options) {
            var that = this;
            that.lastId = 0;
            that.settings = options.settings;
            that._getNotificationConfig(function (config) {
                that._registedApps = config;
                that._isDesktop = isDesktopApp();
            });
            that.render();
        },

        render: function () {
            var that = this;
            that.$(".notifyBell").remove();

            for (var appName in that._registedApps) {
                var app = that._registedApps[appName];
                app.$el = $(app.el);
                if (app.$el.length === 0) {
                    app.$el = $("<li>").addClass("pull-left hidden notifyBell").addClass(app.el).attr("app", appName);
                    that.$el.prepend(app.$el);
                }

                if (app.isActive) {
                    app.$el.removeClass("hidden");
                }

                app.$el.html($.tmpl(that.template, { icon: app.icon }))
                app.$count = app.$el.find(".notify-count");
                app.$notificationList = app.$el.find(".notification");
                app.model = new NotifyList;

                that._bindNotificationItems(app);
            };

            this.fetch();

            //this._pulling();//QuiBQ:Tam thoi comment ham nay thay chua hop ly

            this._bindEvents();
            this._registerApiToGlobal();
            this._registerTriggers();
        },

        //#region Initialize

        fetch: function () {
            var that = this;
            $.ajax({
                url: urlSettings.fetch,
                success: function (data) {
                    success(data);
                }
            });
            var success = function (data) {
                if (data && data.length > 0) {
                    that.lastId = _.last(data).NotificationId;
                    ////_.each(data, function (nNotify) {
                    ////    that._removeSimilarBeforeAdd(nNotify);
                    //});

                    that.model.add(data);
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
            var config = that.settings;
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

        _bindNotificationItems: function (currentApp) {
            var that = this, notifyItems = [];
            currentApp.$notificationList.empty();

            this._changeNotifyTotal(currentApp);

            this.model.each(function (n) {
                if (n.get("AppName") == currentApp.name) {
                    notifyItems.push(n.toJSON());
                }
            });

            notifyItems = notifyItems.reverse();
            currentApp.$notificationList.html(that._parseNotifyItems(notifyItems));
        },

        _showNotificationLists: function (currentApp) {
            currentApp.$el.click();
            currentApp.$el.addClass("open");
        },

        _parseNotifyItems: function (notifyItems) {
            var that = this;
            notifyItems.defaultAvatar = "/AvatarProfile/noavatar.jpg";
            var result = $.tmpl(templateInBell, notifyItems);

            result.click(function (e) {
                that._openNotifyInBell(e);
            });

            result.find(".close-notify").click(function (e) {
                that._remove(e);
            });
            result.find(".avatar").error(function (e) {
                that.imageError(this);
            });

            return result;
        },

        _bindListAndShowDesktop: function (newNotify, hasShowNotify) {
            var currentApp;
            currentApp = this._getApp(newNotify.get("AppName"));
            if (!currentApp) {
                throw "App name không đúng.";
            }

            this._bindNotificationItems(currentApp);
            if (hasShowNotify && newNotify.get("IsNew")) {
                this._showDesktop(newNotify);
            }
        },

        _changeNotifyTotal: function (currentApp) {
            // currentApp.total = currentApp.model.length;

            var unread = this.model.where({ IsNew: true, AppName: currentApp.name }).length;

            currentApp.$count.text(unread > 99 ? "99+" : unread);

            if (unread > 0) {
                currentApp.$count.show();
            } else {
                currentApp.$count.hide();
            }
        },

        _removeNotify: function (notifyModel) {
            var appName = notifyModel.get("AppName");
            var currentApp = this._getApp(appName);

            if (notifyModel.view) {
                notifyModel.view.close();
            }

            this._bindNotificationItems(currentApp);
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
                    DateCreated: new Date(),
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

                if (!chatData) return;

                var sender = chatData.sender;
                var receiver = chatData.chatterJid;
                if (!sender || !receiver) return;
                sender = sender.split('@')[0];
                receiver = receiver.split('@')[0];

                var userSend = _.find(egov.setting.allUsers, function (u) { return u.username === sender; });
                var userReceive = _.find(egov.setting.allUsers, function (u) { return u.username === receiver; });

                var notifyData = {
                    NotificationType: 3,
                    Title: userSend.label,
                    Content: chatData.content.split("\r\n")[0],
                    SenderAvatar: userSend.avatar,
                    SenderUserName: userSend.username,
                    SenderFullName: userSend.fullname,
                    Date: chatData.date,
                    ReceiveDate: new Date(),

                    //chat
                    ChatId: chatData.messageId, //
                    ChatterJid: chatData.chatterJid,
                    messageId: chatData.messageId,
                    IsNewChat: true
                };

                //Reply message
                if (notifyData.Content.indexOf("&amp;message=") >= 0) {
                    notifyData.Content = notifyData.Content.split("&amp;message=")[1];
                }

                notifyData.Content = escape(notifyData.Content);

                var isRead = false;
                var isNew = !isRead;
                var content = notifyData.Content;

                var notify = {
                    NotificationId: 0,
                    Title: notifyData.Title,
                    Body: content,
                    alternateContent: notifyData.originalContent,
                    Avatar: userSend.avatar,
                    DateCreated: chatData.date,
                    AppName: "chat",
                    GroupId: chatData.chatterJid,
                    IsViewed: false,
                    UserId: userReceive.value,
                    IsReaded: isRead,
                    IsNew: isNew,
                    JsonData: JSON.stringify(notifyData)
                };

                console.log(notify);

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

            // Dùng để kiểm tra tab eGov trên browser có đang được active không.
            if (/*@cc_on!@*/false) { // hack IE
                document.onfocusin = onFocus;
                document.onfocusout = onBlur;
            } else {
                window.onfocus = onFocus;
                window.onblur = onBlur;
            }
        },

        _registerTriggers: function () {
            var that = this;

            this.model.on("update", function (collection, changed) {
                // Added
                var addedNotifications = changed.changes.added;
                if (addedNotifications.length > 0) {
                    var hasShowDesktop = that.HasShowDesktop;

                    var notifyNews = _.filter(addedNotifications, function (n) {
                        return n.get("IsNew");
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

                        that._bindListAndShowDesktop(nNotify, hasShowDesktop);
                    });

                    // Kiểm tra nếu notify là documents thì reload lại cây văn bản
                    var hasDocument = _.find(addedNotifications, function (n) {
                        return n.get("AppName") === defaultConfig.egov.name;
                    });

                    if (hasDocument) {
                        that._reloadDocumentTree();
                    }
                }

                //// Removed
                var removedNotifications = changed.changes.removed;
                if (removedNotifications.length > 0) {
                    removedNotifications.forEach(function (nNotify) {
                        that._removeNotify(nNotify);
                        var nId = nNotify.get("NotificationId");
                        if (nId > 0) {
                            $.ajax({
                                url: urlSettings.remove,
                                type: "Post",
                                data: { id: nNotify.get("NotificationId") }
                            });
                        }
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

        _reloadDocumentTree: function () {
            var frameWindow;
            var documentsApp = this._getApp(defaultConfig.egov.name);
            if (!documentsApp || documentsApp.isActive === false) {
                throw "App name không đúng.";
            }

            var appFrame = window.document.getElementsByName(documentsApp.callback.frameName);
            if (appFrame == null || appFrame.length == 0) {
                return;
            }

            frameWindow = appFrame[0].contentWindow;
            if (frameWindow && frameWindow.egov && frameWindow.egov.pubsub) {
                var event = "tree.reload"; // egov.events.tree.reload;
                frameWindow.egov.pubsub.publish(event);
                frameWindow.egov.pubsub.publish("tree.reloadSelected");
            }
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
            that.$("ul").scroll(function (e) {
                that._scrollToGetData(e);
            });
        },

        _scrollToGetData: function (e) {
            var that = this;
            var element = $(e.target);
            if (element.scrollTop() + element.innerHeight() >= e.target.scrollHeight) {
                that.getData();
            }
        },

        getData: function () {
            var that = this;
            var load_img = $('<img/>').attr('src', '/Content/Images/ajax-loader-small.gif');
            $.ajax({
                url: urlSettings.getData,
                data: {
                    "pageIndex": pageIndex,
                    "pageSize": pageSize,
                },
                success: function (data) {
                    success(data);
                },
                beforeSend: function () {
                    that.$(".loading-image").append(load_img);
                },
                complete: function () {
                    load_img.remove();
                }
            });
            var success = function (data) {
                if (data.length > 0) {
                    that.model.add(data);
                    pageIndex++;
                }
            };
        },

        _setOlds: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");

            this.model.each(function (notify) {
                if (notify.get("AppName") === appName && notify.get("IsNew") == true) {
                    notify.set("IsNew", false);
                }
            });

            var currentApp = this._getApp(appName);
            this._changeNotifyTotal(currentApp);

            $.ajax({
                url: urlSettings.oldAll,
                type: 'Post',
                data: {
                    appName: appName,
                    lastId: this.lastId
                }
            });
        },

        _removeAll: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");
            var models = this.model.where({ AppName: appName });

            this.model.remove(models);

            $.ajax({
                url: urlSettings.removeAll,
                type: 'Post',
                data: {
                    appName: appName,
                    lastId: this.lastId
                }
            });
        },

        _readAll: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");

            this.model.each(function (notify) {
                if (notify.get("AppName") === appName && notify.get("IsReaded") == false) {
                    notify.set("IsReaded", true);
                }
            });

            $.ajax({
                url: urlSettings.readAll,
                type: 'Post',
                data: {
                    appName: appName,
                    lastId: this.lastId
                }
            });
        },

        _remove: function (e) {
            var target = $(e.target).closest("li");
            var notificationId = target.attr("id");
            var notifyModel = this.model.detect(function (n) {
                return n.get("NotificationId") == notificationId;
            });

            this.model.remove(notifyModel);

            $.ajax({
                url: urlSettings.remove,
                type: "Post",
                data: { id: notificationId }
            });
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
            if (this._isDesktop) {
                notify.Callback = "openNotifyFromExternal";
                notify.view = this._showNotifyInDesktop(notify);
                return;
            }

            if (this._egovTabIsActive(notify.get("AppName"))) {
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

        _showNotifyInDesktop: function (notify) {
            return new desktopClientNotify().show(notify);
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

        _openNotifyInBell: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");
            var notificationId = target.attr("id");

            var currentApp = this._getApp(appName);
            var notifyModel = this.model.detect(function (n) {
                return n.get("NotificationId") == notificationId;
            });

            this._openNotify(currentApp, notifyModel);
        },

        _openNotify: function (currentApp, notifyModel) {
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

            frameLoadedCallback = function (frameWindow) {
                if (frameWindow === undefined) {
                    return;
                }

                callback = frameWindow[currentApp.callback.api];
                if (isFunction(callback)) {
                    callback(notify.get("JsonData"));
                }
            };

            if (currentApp.callback.frameName == "") {
                frameWindow = window;
                frameLoadedCallback(frameWindow);
            } else {
                mainApps.openApp(currentApp.name, function () {
                    var appFrame = window.document.getElementsByName(currentApp.callback.frameName);
                    if (appFrame == null || appFrame.length == 0) {
                        return;
                    }

                    frameWindow = appFrame[0].contentWindow;

                    frameLoadedCallback(frameWindow);
                });
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
        _egovTabIsActive: function (appName) {
            var chromeTabIsFocus = window.document.isFocused === true;

            // Nếu không focus vào tab eGov thì hiển thị notify desktop
            if (!chromeTabIsFocus) {
                return false;
            }

            // nếu đang focus vào eGov mà không focus vào app, thì hiển thị notify déktop
            var currentApp = this._getApp(appName);
            if (currentApp && !currentApp.isFocus()) {
                return false;
            }

            // Không hiển thị notify desktop
            return true;
        },

        _activeTab: function () {
            window.focus();
        },

        _getApp: function (appName) {
            return this._registedApps[appName];
        },

        _findNotificationElement: function (notifyModel) {
            var appName = notifyModel.get("AppName");
            var notificationId = notifyModel.get("NotificationId");
            var currentApp = this._getApp(appName);
            var element = currentApp.$notificationList.find("#" + notificationId);

            return element;
        },

        _removeSimilarBeforeAdd: function (nNotify) {
            var similars = this.model.select(function (n) {
                return n.get("AppName") == nNotify.AppName &&
                        n.get("GroupId") == nNotify.GroupId;
            });

            if (similars.length > 0) {
                this.model.remove(similars);
            }
        },

        imageError: function (evt) {
            $(evt).attr("src", "/AvatarProfile/noavatar.jpg");
        }

        //#endregion

        //#region Test

        , test: function () {
            this.testInterval;
            var that = this;
            var groups = ["tienbv", "dambv", "tamdn", "dunghv", "dungnvl", "phucnhb", "cuongnt", "taimv"];
            var contents = ["Nghẹn ngào giây phút ta chấp nhận sống không cần nhau",
                            "Chẳng khác chi trái đất này làm sao tồn tại không có mặt trời",
                            "Chỉ biết lặng nhìn em quay lưng bước đi lòng anh thắt lại",
                            "Nghĩ đến mình sẽ không gặp lại.",
                            "Tình yêu đâu phải ai cũng may mắn tìm được nhau",
                            "Chẳng giống như chúng ta tìm được nhau rồi lại hoang phí duyên trời",
                            "Lắng Nghe Nước Mắt lyrics on ChiaSeNhac.vn",
                            "Tại sao phải rời xa nhau mãi mãi, biết đến khi nào chúng ta",
                            "Nhận ra chẳng thể quên được nhau."]

            this.testInterval = setInterval(function () {
                var groupId = _.sample(groups);
                var content = _.sample(contents);

                window.pushNotifyFromClient({
                    NotificationId: 0,
                    Title: groupId,
                    GroupId: groupId,
                    Body: content,
                    Avatar: "https://danhba.bkav.com/avatars/" + groupId + ".bmp",
                    DateCreated: new Date,
                    AppName: "chat",
                    IsNew: true,
                    JsonData: '{DocumentCopyId: 1778,Compendium: "Phạm Quang Ba (Phòng Khám Nha khoa Sài Gòn Colgate 2) "}'
                });
            }, 1000 * 5 * 1);
        },

        stopTest: function () {
            clearInterval(this.testInterval);
        }

        //#endregion
    });

    function onBlur() {
        window.document.isFocused = false;
    }
    function onFocus() {
        window.document.isFocused = true;
    }
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


    window.eGovNotification = EgovNotificationView;

})(window.jQuery, window._, window.Backbone, window.notify);