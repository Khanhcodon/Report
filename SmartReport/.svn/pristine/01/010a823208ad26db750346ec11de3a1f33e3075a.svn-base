// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.reconnection) {
        return;
    }

    btalk.reconnection = {
        _count: 3,
        timer: null,
        callback: null,
        connection: function (callback, type) {
            if (type === 'success') {
                this.clear();
            }
            if (typeof callback === 'function') {
                this.callback = callback;
            }
        },

        startCountdown: function () {
            this._count = this._count || 3;
            if ((this._count - 1) >= 0) {
                this._count = this._count - 1;
                if (this._count > 0) {
                    this.timer = window.setTimeout(btalk.reconnection.startCountdown.bind(this), 1000);
                } else {
                    if (typeof this.callback === 'function') {                        
                        this.callback();                        
                    }
                    this.clear();
                }
            }
        },

        reconnectionError: function (callback) {
            this.connection(callback, "error");
            this.startCountdown();

        },

        reconnectionSuccess: function (callback) {
            this.connection(callback, "success");
            this.startCountdown();
        },

        clear: function () {
            window.clearTimeout(this.timer);
            this.callback = null;
        }
    };
})(window.jQuery, window.btalk);