// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

define(function () {
    'use strict';

    btalk.view = btalk.view || {};

    /* Index View
    ---------------------------------------------------------------------- */

    var HomeView = Backbone.View.extend({
        options: {
            baseDatetimeQuery: '2015-01-01T00:00:00Z',
            maxMessagesInCache: 50
        },

        // La lan dang nhap, su dung dau tien (khi chua co lich su hoi thoai nao)
        NEVER_USE: false,
        CURRENTCHATTER: {},
        isReady: false,

        events: {
            'blur #txtSearchChat': '_hideSearchPanel',
            'click .txtSearchChat': '_showSearchPanel',
        },

        initialize: function (options) {

            btalk.ROSTER = new btalk.BtalkRoster();

            this.render();

            this.registerHanders();

            this.initData();
        },

        render: function () {
            return this;
        },

        //#region Initialize methods

        registerHanders: function () {
            // Tra ve danh sach ban be (danh sach chatter)
            btalk.cm.registerHandler('_handle_iq_roster_result', this.handleRosters.bind(this));

            // Handle xử lý roster khi nhận tin nhắn đến
            btalk.cm.registerHandler('_handle_update_roster', this._handleRosterInMessage.bind(this));

            // Xay ra khi toan bo qua trinh login thuc hien xong
            btalk.cm.registerHandler('_handle_logined', this.handleLogined.bind(this));

            egov.pubsub.subscribe('message.open', this._openMessage.bind(this));


            window.addEventListener("paste", this._convertClipboard.bind(this), false);
        },

        initData: function () {
            !btalk.roster.isLoaded && btalk.cm.getRoster();
        },

        //#endregion

        //#region Handler Events

        handleRosters: function (rosters) {
            btalk.ROSTER.sync(rosters.query.item);

            // Xử lý hiển thị cây phòng ban, danh sách online, histories lần đầu.
            require(['chatContactView'], function (chatContactView) {
                new chatContactView({ model: btalk.ROSTER });
            });
        },

        handleLogined: function () {
            /**
             * Xu ly khi qua trinh login hoan tat
             * Su dung de bao trang thai login thanh cong thi moi render giao dien
             */

            if (!btalk.ROSTER.currentUser) {
                // [TODO] bao loi va day ra trang login
                this.logout();
            }

            btalk.auth.available();
        },

        _handleRosterInMessage: function (msgJson) {
            // Thêm người dùng vào danh sách bạn bè đã tải xuống (Roster) nếu chưa có.
            // Thêm vào danh sách History nếu chưa có
            // Đẩy Chatter trong history lên đầu danh sách

            var user = btalk.ROSTER.getUserByJID(btalk.cutResource(msgJson.carbons ? msgJson.to : msgJson.from));

            // Chỉ add thêm group, do người dùng đều được load sẵn từ eGov sang.
            user = user || (msgJson.type === btalk.CHATTYPE.GROUPCHAT ? btalk.ROSTER.addGroup(msgJson.from) : null);

            if (!user) return;

            this._updateHistory(user, msgJson);
        },

        _updateHistory: function (user, msgJson) {
            var lastTime = new Date(parseInt(msgJson.clienttime));
            user.set('lastMessage', msgJson.body);
            !msgJson.carbons && user.set('unread', user.get('unread') + 1);

            egov.pubsub.publish('chatdock.changeUnread', { id: user.get('jid'), unread: user.get('unread') });

            this._openMessage({ id: user.get('jid'), hasRestoreWhenMinimize: false });
        },

        _openMessage: function (options) {
            var chatterId = options.id;
            var roster = btalk.ROSTER.getUserByJID(chatterId);
            if (roster == null) {
                return;
            }

            var openTabSuccess = function (el) {
                require(['messageView'], function (MessageView) {
                    new MessageView({
                        model: roster,
                        el: el
                    });
                });
            };

            egov.pubsub.publish("chatdock.openTab", {
                jid: chatterId, title: roster.get('fullname'), online: roster.get('status') === 'available', success: openTabSuccess,
                hasRestoreWhenMinimize: options.hasRestoreWhenMinimize
            });
        },

        _showSearchPanel: function () {
        },

        _toggleChat: function () {
            ChatLayout && ChatLayout.toggle('east');
        },

        _hideSearchPanel: function () {
        },

        //#endregion

        logout: function () {
            btalk.auth.logout();
        },

        //#region Private Methods

        _convertClipboard: function (event, success) {
            if (event.clipboardData == false || event.clipboardData.items === undefined) {
                egov.pubsub.publish("window.paste", null);
                return;
            };

            var items = event.clipboardData.items;
            for (var i = 0; i < items.length; i++) {
                if (items[i].type.indexOf("image") == -1) continue;

                var blob = items[i].getAsFile();
                egov.pubsub.publish("window.paste", { data: blob, isImage: false });
                return;
            }
        },

        //#endregion
    });

    return HomeView;
});