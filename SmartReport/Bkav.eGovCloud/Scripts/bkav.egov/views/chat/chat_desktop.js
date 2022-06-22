
define(['chatConfig', 'btalkCore', 'chatRoster'], function () {
    "use strict";

    var FIRST_LOAD = true;

    var DesktopChat = Backbone.View.extend({
        isDisconnecting: false,
        disconnectTimeout: null,
        isActive: false,
        events: {
            "click .txtSearch": "focusSearch",
            "blur #txtSearch": "hideSearch",
            "keyup #txtSearch": "quickSearch",
        },

        initialize: function () {
            this.render();
            this._registerHandles();

            egov.chatDesktop = this;
        },

        render: function () {
            var that = this;

            this._init();
        },

        _init: function () {
            // Load thong tin config
            btalk.config = $.extend(true, {}, btalk.config, egov.config);

            // Bat dau qua trinh reconnect
            btalk.auth.init(btalk.config.AUTH);
            btalk.auth.reconnect(this._successReconnect.bind(this), this._errorReconect.bind(this));
        },

        _errorReconect: function (e) {
            this.isDisconnecting = true;
            if (!this.disconnectTimeout && e !== undefined && typeof e.getAttribute === "function") {
                // Chờ thêm 15s, nếu vẫn disconnect thì mới hiển thị thông báo,
                // Nếu hệ thống reconnect lại dc rồi thì hủy.
                this.disconnectTimeout = setTimeout(function () {
                    this.isDisconnecting && this._handleLossConnect();
                }.bind(this), 15 * 1000);
            }

            btalk.reconnection.reconnectionError(function () { btalk.auth.reconnect(); });
        },

        _successReconnect: function () {
            this.isDisconnecting = false;
            clearTimeout(this.disconnectTimeout);

            //roster
            if (FIRST_LOAD == true) {
                btalk.egov.init(btalk.config.WEBAPI);
                btalk.alert.init(btalk.config.ALERT);

                btalk.roster.init(btalk.config.CLIENT.CLIENT_TYPE);

                this.isActive = true;
                this._renderViews();
                FIRST_LOAD = false;
            } else {
                btalk.reconnection.reconnectionSuccess("");
                btalk.auth.available();
            }

            $(".chat-view").removeClass('disconnected');
            egov.pubsub.publish('chat.connected');
        },

        _handleLossConnect: function () {
            $(".chat-view").addClass('disconnected');
            egov.pubsub.publish('chat.disconnected');
        },

        _registerHandles: function () {
            egov.pubsub.subscribe('chatdesktop.active', this._active.bind(this));
            egov.pubsub.subscribe('chat.changestatus', this._changeStatus.bind(this));
        },

        _active: function (isActive) {
            // Kích hoạt hiển thị và dùng chat desktop.
            // Gọi khi chuyển các app ở menu chính.
            this.isActive = isActive;
            this._showDesktop();
            this.isActive ? this.layout.show("east") : this.layout.hide("east");

            egov.pubsub.publish('chatdock.active', this.isActive);
        },

        _changeStatus: function (status) {
            btalk.cm.changeStatus('changeStatus', status, 0);
        },

        _renderViews: function () {
            this._showDesktop();

            require(['charViewManager', 'ChatDock'], function (ChatHome, ChatDock) {
                btalk.APPVIEW = new ChatHome();
                new ChatDock();
            });
        },

        _showDesktop: function () {
            !this.layout && (this.layout = $('.site-center-container').layout({
                resizable: false,
                closable: true,
                spacing_closed: 0,
                spacing_open: 0,
                east__spacing_open: 0,
                east__size: 205,
                east__zIndex: 1,
                east__paneSelector: ".chat-view",
                center__paneSelector: ".site-content"
            }));
        }
    });

    return DesktopChat;
});