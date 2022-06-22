

define([egov.template.chat.contactItem], function (contactTmp) {

    var ConntactList = Backbone.View.extend({
        el: '#chatFrame',
        wrapper: $(".chat-view"),
        filterDeptIdExt: undefined,

        events: {
            'click .list-group-item': '_openChatter',
            'focus #contactSearch': '_searchFocus',
            'blur #contactSearch': '_searchOut',
            'keyup #contactSearch': '_filterContact',
            'click #toggleChat': '_toggleChat',
            'click .total-online': '_filterOnline'
        },

        initialize: function (option) {
            this.showAll = true;
            this.registerHandlers();
            this.render();
        },

        render: function () {
            var that = this;
            require([egov.template.chat.contactList], function (chatDesktopTemplate) {
                that.$el.html(chatDesktopTemplate);
                that.$recents = that.$("#contact-recents .list-group");
                that._bindOthers();
                that._getOnlines();
                that._getHistories();
            });
        },

        //#region Handler

        registerHandlers: function () {
            this.model.on('change status', this._handleUsersOnline.bind(this));
            this.model.on('change unread', this._handleUnread.bind(this));

            btalk.cm.registerHandler('_handle_iq_list_result', this._handleHistories.bind(this));
            btalk.cm.registerHandler('changeStatus', this._handeStatus.bind(this));
        },

        _getHistories: function () {
            if (this.isLoadAll) return;

            btalk.cm.getHistories(this.skip);
        },

        _handleHistories: function (histories) {
            var that = this, newHistory;
            if (!histories || histories.length === 0 || histories[0] === undefined) {
                // Hiển thị thông báo chưa có lịch sử.
                // Hướng dẫn tạo chat mới nếu có.
                this.isLoadAll = true;
                this.$recents.html("Chưa có hội thoại gần đây.");
                return;
            }

            histories = _.sortBy(histories, 'timestamp');
            _.each(histories, this._parseHistory.bind(this));
            this._bindData();
        },

        _handeStatus: function (msg) {
            this.$("[data-id='" + msg.from + "'] .user-message").attr('title', msg.status).text(msg.status);
        },

        _getOnlines: function () {
            btalk.cm.getOnline();
            if (!this.getOnlineInterval) {
                this.getOnlineInterval = setInterval(this._getOnlines.bind(this), 60 * 1000); // 60s
            }
        },

        _handleUsersOnline: function (user) {
            var onlines = this.model.getOnlines().length;
            $(".count-online").text(onlines);

            user.get('status') === 'available' ? this.$("[data-id='" + user.get('jid') + "']").addClass('online') : this.$("[data-id='" + user.get('jid') + "']").removeClass('online');
        },

        _handleUnread: function (user) {
            var jid = user.get('jid'), unread = user.get('unread');
            var contactElement = this.$('.list-group-item[data-id="' + jid + '"]');
            contactElement.find('.total-unread').text(unread);
            unread === 0 ? contactElement.removeClass('unread') : contactElement.addClass('unread');
        },

        //#endregion

        //#region Bind Contacts

        _bindData: function () {
            this._bindOnlineCount();
            this._bindRecents();
            this._bindGroups();
        },

        _bindOnlineCount: function () {
            var onlines = this.model.getOnlines().length;
            $(".count-online").text(onlines);
        },

        _bindRecents: function () {
            this.$recents.empty();

            var recents = _.first(this.model.getChatRecents(), 6);
            _.each(recents, function (r) {
                this.$recents.append($.tmpl(contactTmp, r.toJSON()));
                this.$otherElement.find('[data-id="' + r.get('jid') + '"]').remove();
            }.bind(this));
        },

        _bindGroups: function () {
            this.$groupElement = this.$("#contact-groups .list-group");
            this.$groupElement.empty();
            var groups = _.first(this.model.getGroups(), 3);
            _.each(groups, function (g) {
                this.$groupElement.prepend($.tmpl(contactTmp, g.toJSON()));
            }.bind(this));
        },

        _bindOthers: function () {
            this.$otherElement = this.$('#contact-others .list-group');
            this.$otherElement.empty();

            var chats = _.sortBy(this.model.getChats(), function (u) {
                return u.get("username");
            });

            _.each(chats, function (c) {
                if (c.get('username').equals('admin', true)) return;
                this.$otherElement.append($.tmpl(contactTmp, c.toJSON()));
            }.bind(this));
        },

        //#endregion

        //#region Events

        _searchFocus: function () {
            this.$('#contactSearch').attr('placeholder', 'Nhập tên tài khoản');
        },

        _searchOut: function () {
            //this.$('#contactSearch').val('');
            //this.$('.list-group-item').show();
            //this.$('#contactSearch').attr('placeholder', 'Tìm kiếm');
        },

        _filterContact: function (e) {
            var searchTerm = this.$('#contactSearch').val();
            if (searchTerm === "") return this.$('.list-group-item').show();

            searchTerm = searchTerm.toLowerCase();
            this.$('.list-group-item').each(function (e) {
                $(this).attr('data-id').contains(searchTerm) ? $(this).show() : $(this).hide();
            });
        },

        _openChatter: function (e) {
            var target = $(e.target).closest(".list-group-item");
            var jid = target.attr('data-id');
            var user = this.model.getUserByJID(jid);

            user && egov.pubsub.publish("message.open", { id: jid, hasRestoreWhenMinimize: true });
        },

        _toggleChat: function () {
            this.minimizing ? this._showFullFrame() : this._showMinimize();
        },

        _showFullFrame: function () {
            this.minimizing = false;
            egov.pubsub.publish('chatdock.hideContactMinize');
            egov.chatDesktop.layout && egov.chatDesktop.layout.show('east');

            this.$el.removeClass("minimize");
            $(".chat-view").append(this.$el);
        },

        _showMinimize: function () {
            this.minimizing = true;
            egov.pubsub.publish('chatdock.showContactMinize');
            egov.chatDesktop.layout && egov.chatDesktop.layout.hide('east');
            this.$el.addClass("minimize");
            $("#contactViewMinize").append(this.$el);
        },

        _filterOnline: function () {
            if (this.showAll) {
                this.$('.list-group-item').hide();
                this.$('.list-group-item.online').show();
                this.showAll = false;
            } else {
                this.$('.list-group-item').show();
                this.showAll = true;
            }
        },

        //#endregion	

        _parseHistory: function (history) {
            var chatWith, isGroup, rosterUser, lastTime;

            if (!history) return;

            chatWith = btalk.cutResource(history.with);
            isGroup = chatWith.indexOf(btalk.config.CM.xmpp.conference) > 0;

            rosterUser = btalk.ROSTER.getUserByJID(chatWith);

            // Trường hợp null: người dùng không còn trong contact => ko xử lý.
            rosterUser = rosterUser || (isGroup ? btalk.ROSTER.addGroup(chatWith, history.fullname) : null);

            if (!rosterUser) return;

            lastTime = new Date(parseInt(history.timestamp));
            rosterUser.set('lastMessageTime', lastTime);
            rosterUser.set('lastTimeString', btalk.getCoolTime(lastTime));
            rosterUser.set('isRecent', true);
        },

    });

    return ConntactList;
});