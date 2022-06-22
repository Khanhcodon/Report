// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function (global, factory) {
    'use strict';

    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['jquery', './btalk'], factory);
    } else {
        // Browser globals
        factory(jQuery, btalk);
    }
}(typeof window !== "undefined" ? window : this, function ($, btalk) {
    'use strict';

    // Plugin code goes here:

    if (btalk.audio) {
        return;
    }

    btalk.audio = {
        sounds: new Object(),

        init: function () {
            // Tam thoi bo am thanh di, hoan thien dua lai sau
            //return;
            this.sounds['message_recv'] = "./js/app/sounds/message.wav";
            this.sounds['message_queue'] = "./js/app/sounds/message.wav";
            this.sounds['chat_recv'] = "./js/app/sounds/message.wav";
            this.sounds['chat_queue'] = "./js/app/sounds/message.wav";
            this.sounds['online'] = "./js/app/sounds/notify.wav";
            this.sounds['offline'] = "./js/app/sounds/notify.wav";
            this.sounds['startup'] = "./js/app/sounds/notify.wav";
            this.sounds['connected'] = "./js/app/sounds/message.wav";
        },

        _alowPlaySound: true,
        _soundPlaying: false,

        soundLoaded: function () {
            this._soundPlaying = false;
        },

        active: function (value) {
            if (typeof value == 'boolean') {
                this._alowPlaySound = value;
            } else {
                return this._alowPlaySound;
            }
        },

        playSound: function (action) {
            if (btalk.cache.Key_SoundMessage == "active") {
                this._soundPlaying = true;
                var audio = new Audio(this.sounds[action]);
                audio.play();
                this._soundPlaying = false;
            }
        }
    };

    btalk.audio.init();
}));