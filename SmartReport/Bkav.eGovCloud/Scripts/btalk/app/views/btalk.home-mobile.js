// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

define(function () {
    'use strict';

    btalk.view = btalk.view || {};

    /* Index View
    ---------------------------------------------------------------------- */

    var HomeView = Backbone.View.extend({
        el: "#chatFrame",

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

            this.registerHanders();

            this.initData();

            this.initEmoticons();

            this.$conversation = this.$(".conversation");
            this.$search = this.$(".search");
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
        },

        initData: function () {
            !btalk.roster.isLoaded && btalk.cm.getRoster();
        },

        initEmoticons: function () {
            $(".action-input-chat #btalk_emotion").append(btalk.emoticon.render());
            $(".message-button #btalk_emotion").append(btalk.emoticon.render());

            $(".btalk-emoticon li").bind("click", function (e) {
                var emoticon_id = parseInt($(e.currentTarget).attr('data-value'));
                var _symbol = btalk.emoticon.getSymbol(emoticon_id);
                // Neu textbox chat khong co noi dung chat => gui emoticon di luon
                if (!this.$messageTxt.text()) {
                    //this.sendMessage2(_symbol).bind(this);
                    this.sendMessage2(_symbol);
                }
                    // Nguoc lai thi chen vao vi tri contro hien tai
                else {
                    this.$messageTxt.focus();
                    // Luu y luon them khoang trang khi chen emoticon
                    btalk.insertTextAtCursor(" " + btalk.htmlDec(_symbol) + " ");
                }
            }.bind(this));
        },

        //#endregion

        //#region Handler Events

        handleRosters: function (rosters) {
            btalk.ROSTER.sync(rosters.query.item);

            // Xử lý hiển thị cây phòng ban, danh sách online, histories lần đầu.
            require(['chatDepartmentView', 'chatHistoryView', 'chatSearch'], function (chatDepartmentView, historyView, chatSearch) {
                new chatDepartmentView({
                    model: btalk.ROSTER
                });

                new historyView();

                new chatSearch();
            });
        },

        handleLogined: function () {
            /**
             * Xu ly khi qua trinh login hoan tat
             * Su dung de bao trang thai login thanh cong thi moi render giao dien
             */

            if (!btalk.ROSTER.currentuser) {
                // [TODO] bao loi va day ra trang login
                this.logout();
            }
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
            user.set('lastMessageTime', lastTime);
            user.set('lastTimeString', btalk.getCoolTime(lastTime));
            !msgJson.carbons && user.set('unread', user.get('unread') + 1);

            egov.pubsub.publish('hisroties.update', user);
        },

        _openMessage: function (data) {
            var chatterId = data.chatterid;
            var roster = btalk.ROSTER.getUserByJID(chatterId);
            if (roster == null) {
                return;
            }

            require(['messageView'], function (MessageView) {
                new MessageView({
                    model: roster
                });
            });
        },

        _showSearchPanel: function () {
            this.$('.mdl-layout__tab-bar-container').hide();
            this.$conversation.hide();
            this.$search.fadeIn();
        },

        _hideSearchPanel: function () {
            this.$search.hide();
            this.$('.mdl-layout__tab-bar-container').show();
            this.$conversation.fadeIn();
        },

        //#endregion

        logout: function () {
            btalk.auth.logout();
        }
    });

    return HomeView;
});