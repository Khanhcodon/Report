define(['chatConfig', 'btalkCore', 'chatRoster'], function () {

    "use strict";

    var FIRST_LOAD = true;

    var MobileChat = Backbone.View.extend({
        el: "#chatFrame",
        events: {
            "click .txtSearch": "focusSearch",
            "blur #txtSearch": "hideSearch",
            "keyup #txtSearch": "quickSearch",
        },

        initialize: function () {
            //this.render();
        },

        render: function () {
            var that = this;

            // Load thong tin config
            btalk.config = $.extend(true, {}, btalk.config, egov.config);

            // Bat dau qua trinh reconnect
            btalk.auth.init(btalk.config.AUTH);
            btalk.auth.reconnect(this._successReconnect.bind(this), this._errorReconect.bind(this));
        },

        _errorReconect: function (e) {
            if (e !== undefined && typeof e.getAttribute === "function") {
                console.log(e.getAttribute('code') + ": Need reconnect!");
            }

            btalk.reconnection.reconnectionError(function () { btalk.auth.reconnect(); });
        },

        _successReconnect: function () {
            //roster
            if (FIRST_LOAD == true) {
                btalk.egov.init(btalk.config.WEBAPI);

                btalk.roster.init(btalk.config.CLIENT.CLIENT_TYPE);
                btalk.alert.init(btalk.config.ALERT);

                this.renderViews();

                FIRST_LOAD = false;
            } else {
                btalk.reconnection.reconnectionSuccess("");
                btalk.auth.available();
            }
        },

        renderViews: function () {
            require(['charViewManager'], function (ChatHome) {
                btalk.APPVIEW = new ChatHome();
            });
        }
    });

    return MobileChat;
});
