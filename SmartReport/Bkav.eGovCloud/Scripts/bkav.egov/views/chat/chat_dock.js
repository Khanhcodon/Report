
define([egov.template.chat.chatTabItem], function (chatTabItem) {

    var ChatDock = Backbone.View.extend({
        el: "#ChatTabsPagelet",
        container: this.$('.chat-tabs-container'),
        collapser: this.$('.chat-collapsed'),
        collapseElements: this.$('#chatCollapseMenu'),
        contactMinimize: this.$('.contact-minimize '),
        wrapper: $('.chat-dock-wrapper'),

        tabs: [],

        events: {
            'click #chatCollapseMenu a': '_openFromCollapse',
            'click .contact-minimize .title-bar': '_toggleContact'
        },

        initialize: function () {
            this._caculatorDockSize();

            this.isActive = true;
            this.subscribe();
            this.render();
        },

        render: function () {

        },

        subscribe: function () {
            egov.pubsub.subscribe('chatdock.openTab', this._openChatTab.bind(this));
            egov.pubsub.subscribe('chatdock.onRemoveTab', this._removeTab.bind(this));
            egov.pubsub.subscribe('chatdock.changeUnread', this._changeUnread.bind(this));

            egov.pubsub.subscribe('chatdock.showContactMinize', this._showContactMinize.bind(this));
            egov.pubsub.subscribe('chatdock.hideContactMinize', this._hideContactMinize.bind(this));

            egov.pubsub.subscribe('chatdock.active', this._active.bind(this));

            egov.pubsub.subscribe('window.resize', this._windowResize.bind(this));
        },

        //#region Register Handle

        _openChatTab: function (model) {
            // Không tồn tại tài khoản người chat cùng.
            if (!model || !model.jid || String.isNullOrEmpty(model.jid)) return;

            model.title = model.title || model.jid;
            this._isOpened(model.jid) ? this._openChat(model.jid, model.hasRestoreWhenMinimize) : this._addNewChat(model);
        },

        _removeTab: function (jid) {
            this.tabs = _.reject(this.tabs, function (t) {
                return t.jid === jid;
            });

            this._showTabInCollapse();
        },

        _changeUnread: function (options) {
            var tab = this._getTab(options.id);
            if (!tab || !tab.view || !tab.view.isMinimizing) return;

            tab.view.setUnread(options.unread);
        },

        _showContactMinize: function () {
            this.contactMinimize.find(".chat-tab-container").width(205).show();
            this.contactMinimize.find(".chat-tab-flyout").show();
            this.wrapper.css('right', '0');
        },

        _hideContactMinize: function () {
            this.contactMinimize.find(".chat-tab-container").width(175).hide();
            this.contactMinimize.find(".chat-tab-flyout").hide();
            this.wrapper.css('right', '205px');
        },

        _windowResize: function () {
            this._caculatorDockSize();
        },

        _active: function (isActive) {
            this.isActive = isActive;
            this.isActive ? this.wrapper.show() : this.wrapper.hide();
        },

        //#endregion

        _openChat: function (jid, hasRestoreWhenMinimize) {
            var chatTab = this._getTab(jid);
            if (chatTab && chatTab.view && !chatTab.openning) {
                this._appendChat(chatTab.view);
                chatTab.openning = true;
            }

            if (hasRestoreWhenMinimize) {
                chatTab.view.restoreChat();
                this._focusChat(chatTab.view);
                return;
            }
        },

        _addNewChat: function (model) {
            var newTabView = new ChatTab(model);
            var newTab = {
                jid: model.jid,
                title: model.title,
                view: newTabView,
                openning: true
            };

            this._appendChat(newTabView);
            this.tabs.push(newTab);
            this._focusChat(newTabView);

            typeof model.success === 'function' && model.success(newTabView.contentElement);
        },

        _appendChat: function (chatTabView) {
            this._collapseTabWhenFull();
            this.collapser.after(chatTabView.$el);
            chatTabView.show();
        },

        _focusChat: function (newTab) {
            newTab.selected();
        },

        _isOpened: function (jid) {
            return this._getTab(jid) !== undefined;
        },

        _collapseTabWhenFull: function (jid) {
            var opening = _.where(this.tabs, { openning: true }).length;
            if (opening < this.chatSize.maxShow) return;

            var firstOpenning = this.container.find('.chat-tab').not('.hidden, .contact-minimize').last();
            var jid = firstOpenning.attr('data-id');

            var chatTab = this._getTab(jid);
            if (!chatTab) return;

            chatTab.view.hide();
            chatTab.openning = false;

            this._appendAndShowCollapser(chatTab);
        },

        _appendAndShowCollapser: function (chatTab) {
            this.collapseElements.append($.tmpl('<li data-id="${jid}"><a href="#">${title} <span class="pull-right">x</span></a></li>', chatTab));
            this.collapser.show();
        },

        _openFromCollapse: function (e) {
            var target = $(e.target).closest('li');
            var jid = target.attr("data-id");
            var chatTab = this._getTab(jid);

            if (chatTab) {
                this._openChat(chatTab.jid);
                this._removeFromCollapse(chatTab.jid);
            }
        },

        _showTabInCollapse: function () {
            var collapsing = _.find(this.tabs, function (t) {
                return t.openning === false;
            });

            if (collapsing) {
                this._openChat(collapsing.jid);
                this._removeFromCollapse(collapsing.jid);
            }
        },

        _removeFromCollapse: function (jid) {
            var collapsing = this.collapseElements.find("[data-id='" + jid + "']");
            collapsing.remove();

            (this.collapseElements.html().trim() === "") && this.collapser.hide();
        },

        _unFocusAll: function () {
            this.container.find('.onfocused').removeClass('onfocused');
        },

        _getTab: function (jid) {
            return String.isNullOrEmpty(jid) ? null : _.find(this.tabs, function (t) {
                return t.jid === jid;
            });
        },

        _caculatorDockSize: function () {
            /*
                Xử lý tính toán 1 dock theo kích thước viewport hiện tại mở được bao nhiêu chat tab cùng lúc
            */

            var w = $(window);
            var contactWidth = 215;
            this.chatSize = {
                dockWidth: w.width() - contactWidth,
                tab: 284,
                margin: 4,
                collapse: 180,
                maxShow: 0
            };

            this.chatSize.maxShow = Math.floor((this.chatSize.dockWidth - this.chatSize.collapse) / (this.chatSize.tab + this.chatSize.margin));
        },

        _toggleContact: function (e) {
            var $el = $(e.target).closest(".title-bar").parents(".contact-minimize");
            if ($el.is(".minimize")) {
                $el.find('.chat-tab-flyout').stop().animate({
                    height: 330,
                    width: 205
                }, 250, function () {
                    $el.removeClass('minimize');
                });

                $el.find('.chat-tab-container').stop().animate({
                    width: 205
                }, 250);

                return;
            }

            $el.find('.chat-tab-flyout').stop().animate({
                height: 26,
                width: 192
            }, 250, function () {
                $el.addClass('minimize');
            });

            $el.find('.chat-tab-container').stop().animate({
                width: 192
            }, 250);
        }
    });

    var ChatTab = Backbone.View.extend({
        className: 'chat-tab',
        events: {
            'click': 'selected',
            'click .btnClose': '_removeTab',
            'click .title-bar': 'toggleTab'
        },

        initialize: function (option) {
            this.model = option;
            return this.render();
        },

        render: function () {
            this.$el.html($.tmpl(chatTabItem, this.model));
            this.contentElement = this.$(".chat-tab-content");

            this.model.online && this.$('.online').show();
            this.$el.attr('data-id', this.model.jid);

            return this;
        },

        selected: function () {
            this.$el.addClass('onfocused');
            this.$el.siblings('.chat-tab').removeClass('onfocused');
        },

        show: function () {
            this.$el.removeClass('hidden');
        },

        hide: function () {
            this.$el.addClass('hidden');
        },

        setUnread: function (unread) {
            unread > 0 ? this.$el.addClass('unread') : this.$el.removeClass('unread');
            this.$('.total-unread').text(unread);

            this.blinkInterval = setInterval(function () {
                this.$el.toggleClass('blink');
            }.bind(this), 300);
        },

        _maximizeTab: function () {

        },

        _minimizeTab: function () {

        },

        _restoreTab: function () {

        },

        _removeTab: function () {
            this.$el.remove();
            egov.pubsub.publish('chatdock.onRemoveTab', this.model.jid);
        },

        toggleTab: function () {
            this.isMinimizing ? this.restoreChat() : this.minimizeChat();
        },

        minimizeChat: function () {
            this.isMinimizing = true;
            this.$('.chat-tab-flyout').stop().animate({
                height: 26,
                width: 192
            }, 250, function () {
                this.$el.addClass('minimize').removeClass('onfocused');
            }.bind(this));

            this.$('.chat-tab-container').stop().animate({
                width: 192
            }, 250);
        },

        restoreChat: function () {
            this.isMinimizing = false;
            this.$('.chat-tab-flyout').stop().animate({
                height: 330,
                width: 284
            }, 250, function () {
                clearInterval(this.blinkInterval);
                this.$el.removeClass('minimize').removeClass('unread').removeClass('blink');
            }.bind(this));

            this.$('.chat-tab-container').stop().animate({
                width: 284
            }, 250);
        }
    });

    return ChatDock;
});