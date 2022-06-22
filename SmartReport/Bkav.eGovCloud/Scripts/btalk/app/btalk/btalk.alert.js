// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.alert) {
        return;
    }

    btalk.alert = {
        html: '<div class="alert" style="display:none;"> ' +
                '<strong>{title}</strong> {message} ' +
              '</div>',
        instance: null,
        _count: 15,
        _lastcount: 15,
        $count: null,
        callback: null,
        title: "",
        message: "",
        timer: null,
        options: {
            target: "body",
            callbackAfterShow: function () { }
        },

        init: function (options) {
            this.options = $.extend(true, {}, this.options, options);
        },

        startCountdown: function () {
            if (!this.$count) return;

            this._count = this._count || 15;

            if ((this._count - 1) >= 0) {
                this._count = this._count - 1;
                this.$count.text(this._count);
                if (this._count > 0) {
                    this.timer = window.setTimeout(btalk.alert.startCountdown.bind(this), 1000);
                } else {
                    this.callback(this.title, this.message);
                    this.clear();
                }
            }
        },

        make: function (title, message, callback, _type) {
            if (this.instance) {
                this.clear();
            }

            if (!message) {
                return;
            }
            this.title = title || "";
            this.message = message;

            var html = this.html.replace("{title}", this.title).replace("{message}", this.message);
            this.instance = $(html).addClass("alert-" + _type);

            if (typeof callback === 'function') {
                this.callback = callback;
                this.instance.find("a.reconnect").bind("click", function () {
                    this.callback(this.title, this.message);
                    this.clear();
                }.bind(this));
            }

            if (_type === 'success') {
                window.setTimeout(function () {
                    this.clear();
                }.bind(this), 5000);
            }

            this.$count = this.instance.find("a.count");
            if (this.$count && parseInt(this.$count.text()) > 0) {
                this._count = parseInt(this.$count.text()) > 300 ? 300 : parseInt(this.$count.text());
            }
        },

        show: function () {
            if (this.instance) {
                $(this.options.target).append(this.instance);
                this.instance.show();
                this.startCountdown();
                this.options.callbackAfterShow();
            }
        },

        info: function (title, message, callback) {
            /*
            <div class="alert alert-info">
                <strong>Info!</strong> Indicates a neutral informative change or action.
            </div>
            */
            title = title || "";
            this.make(title, message, callback, "info");
            this.show();
        },

        warning: function (title, message, callback) {
            /*
            <div class="alert alert-warning">
                <strong>Warning!</strong> Indicates a warning that might need attention.
            </div>
            */
            title = title || "";
            this.make(title, message, callback, "warning");
            this.show();
        },

        error: function (title, message, callback) {
            /*
            <div class="alert alert-error">
                <strong>Warning!</strong> Indicates a warning that might need attention.
            </div>
            */
            title = title || "";
            this.make(title, message, callback, "error");
            this.show();
        },

        success: function (title, message, callback) {
            /*
            <div class="alert alert-success">
                <strong>Success!</strong> Indicates a successful or positive action.
            </div>
            */
            title = title || "";
            this.make(title, message, callback, "success");
            this.show();
        },

        danger: function () {
            /*
            <div class="alert alert-danger">
                <strong>Danger!</strong> Indicates a dangerous or potentially negative action.
            </div>
            */
        },

        clear: function () {
            window.clearTimeout(this.timer);
            this.callback = null;
            $('div.alert').remove();
            this.instance = null;
        }
    };
})(window.jQuery, window.btalk = window.btalk || {});