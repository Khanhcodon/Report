
define([egov.template.history.listItem], function (historyItemTmp) {
    var HistoryList = Backbone.View.extend({
        el: '#chat-histories-list',

        $historyList: this.$("ul"),
        $groupList: $('#chat-groups .page-content ul'),

        isLoaded: false,
        skip: 0,
        isLoadAll: false,
        model: btalk.CHATTERS,

        initialize: function () {
            this.$historyList = this.$("ul");
            this.$historyList.empty();
            this.$groupList.empty();

            this._fixLayout();
            this.$el.scrollLoadMore('next', this._getHistories.bind(this));

            this.registerHandlers();
            this.render();
        },

        render: function () {
            this._getHistories();
        },

        _getHistories: function () {
            if (this.isLoadAll) return;

            btalk.cm.getHistories(this.skip);
        },

        _fixLayout: function () {
            this.$el.height(egov.screenSize.contentHWithoutFooter - 48); // 48 height cuar tab
        },

        registerHandlers: function () {
            // Handle xử lý danh sách histories
            btalk.cm.registerHandler('_handle_iq_list_result', this._handleHistories.bind(this));

            btalk.cm.registerHandler('_handle_getMembersGroup', this._handleGetMemberGroup.bind(this));

            egov.pubsub.subscribe('histories.moveToTop', this._moveToTop.bind(this));

            egov.pubsub.subscribe('hisroties.update', this._updateChatter.bind(this));
        },

        //#region Handler

        _handleHistories: function (histories) {
            var that = this, newHistory;
            if (!histories || histories.length === 0) {
                // Hiển thị thông báo chưa có lịch sử.
                // Hướng dẫn tạo chat mới nếu có.
                this.isLoadAll = true;
                return;
            }

            this.skip += histories.length;

            histories = _.sortBy(histories, 'timestamp');

            _.each(histories, function (history) {
                var history = this._parseHistory(history);
                history && this._addChatter(history);
            }.bind(this));
        },

        _handleGetMemberGroup: function (result) {
            btalk.ROSTER.addMembers(result.groupId, result.members);
        },

        //#endregion

        //#region Parse Histories

        _parseHistory: function (history) {
            var chatWith, isGroup, rosterUser, lastMessage, fromName, lastTime, isMe, newChatter, result;

            chatWith = btalk.cutResource(history.with);
            isGroup = chatWith.indexOf(btalk.config.CM.xmpp.conference) > 0;

            rosterUser = btalk.ROSTER.getUserByJID(chatWith);

            // Trường hợp null: người dùng không còn trong contact => ko xử lý.
            rosterUser = rosterUser || (isGroup ? btalk.ROSTER.addGroup(chatWith, history.fullname) : null);

            if (!rosterUser) return;

            // Lấy danh sách member của Group cho gói tin lấy history không trả về members
            // Todo: đưa phần này vào xử lý roster
            isGroup && this._getGroupMember(chatWith);

            isme = btalk.cutResource(history.from) === btalk.ROSTER.currentJid;
            fromName = history.from.split("@")[0];
            lastMessage = String.format("{0}{1}", isme ? "Bạn: " : (isGroup ? fromName + ": " : ""), this._getLastmessageStr(history.message));
            rosterUser.set('lastMessage', btalk.emoticon.process(lastMessage));

            lastTime = new Date(parseInt(history.timestamp));
            rosterUser.set('lastMessageTime', lastTime);
            rosterUser.set('lastTimeString', btalk.getCoolTime(lastTime));

            return rosterUser;
        },

        //#endregion

        //#region Bind Histories

        _addChatter: function (model) {

            var newChatter = new Chatter({
                model: model
            });

            this.$historyList.prepend(newChatter.el);

            if (model.get('isGroup')) {
                var group = new Chatter({
                    model: model
                });

                this.$groupList.prepend(group.el);
            }
        },

        _updateChatter: function (model) {
            var $chatter = this.$historyList.find('[data-id="' + model.get('jid') + '"]');
            if ($chatter.length === 0) {
                return this._addChatter(model);
            }

            this._moveToTop(model);
        },

        _moveToTop: function (model) {
            var $chatter = this.$historyList.find('[data-id="' + model.get('jid') + '"]');
            ($chatter.length > 0) && this.$historyList.prepend($chatter);

            if (model.get('isGroup')) {
                var $group = this.$groupList.find('[data-id="' + model.get('jid') + '"]');
                ($group.length > 0) && this.$groupList.prepend($group);
            }
        },

        //#endregion

        //#region Private Methods

        _getGroupMember: function (groupId) {
            btalk.cm.getMembers(groupId);
        },

        _getLastmessageStr: function (body) {
            body = body || "";
            return body.length <= 100 ? body : body.substr(0, 100);
        },

        //#endregion
    });

    var Chatter = Backbone.View.extend({
        tagName: "li",
        className: "mdl-list__item mdl-list__item--two-line chat-history-row",
        events: {
            'click': '_openMessage'
        },

        initialize: function () {
            this.render();
            this.listenTo(this.model, 'change:lastMessage', this._changeLastmessage);
            this.listenTo(this.model, 'change:lastMessageTime', this._changeLasttimestamp);
            this.listenTo(this.model, 'change:status', this._changeStatus);
            this.listenTo(this.model, 'change:fullname', this._changeFullnameGroup);
            this.listenTo(this.model, 'change:unread', this._changeUnread);
        },

        render: function () {
            var model = this.model.toJSON();
            this.$el.html($.tmpl(historyItemTmp, model));
            this.$el.attr('data-id', model.jid);
            this.model.get('unread') > 0 && this.$el.addClass('unread');

            this.$('.mdl-list__item-sub-title').dotdotdot();

            return this;
        },

        _changeLastmessage: function () {
            var _lastmessage = btalk.emoticon.process(this.model.get('lastMessage'));

            if (this.model.get('isme') == true && _lastmessage.indexOf("Bạn: ") < 0) {
                this.$el.find('.lastmessage').html("Bạn: " + _lastmessage);
            } else {
                this.$el.find('.lastmessage').html(_lastmessage);
            }

            return this;
        },

        _changeLasttimestamp: function () {
            var timestampText = this.model.get('lastTimeString');
            this.$el.find('.mdl-list__item-secondary-info').text(timestampText);
            return this;
        },

        _changeStatus: function () {
            this.model.get('online') == true
                        ? this.$el.find('.mdl-list__item-primary-content').addClass("online")
                        : this.$el.find('.mdl-list__item-primary-content').removeClass("online");
        },

        _changeFullnameGroup: function () {
            this.$el.find('.full-name').text(this.model.get('fullname'));
        },

        _changeUnread: function () {
            var unread = this.model.get('unread') || 0;
            this.$('.unread-count').text(unread) && (unread > 0 ? this.$el.addClass('unread') : this.$el.removeClass('unread'));
        },

        _openMessage: function (e) {
            egov.pubsub.publish('message.open', { chatterid: this.model.get('jid') });
        }
    });

    return HistoryList;
});