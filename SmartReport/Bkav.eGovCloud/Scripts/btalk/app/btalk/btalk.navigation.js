    // Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/
// Yeu cau: Xu ly sao cho dam bao se alert canh bao khi nguoi dung dong tab chat hien tai. Khi f5, ctrl f5... thi bo qua.

(function (global, factory) {
    'use strict';

    if ( typeof define === 'function' && define.amd ) {
        // AMD. Register as an anonymous module.
        define(['jquery', './btalk', 'require'], factory);
    } else {
        // Browser globals
        factory(jQuery, btalk, require);
    }
}(typeof window !== "undefined" ? window : this, function ($, btalk, require) {
	'use strict';

if ( btalk.navigation ) {
	return;
}

btalk.navigation = {
	_validNavigation: false,
	isReady: false,
	beforeunloads: [],
	unloads: [],
	message: "",
	init: function (_beforeunload, _unload, _message) {
		if ( typeof _beforeunload === 'function' ) {
			var check1 = false;
			for (var i = 0; i < this.beforeunloads.length; i++ ) {
				if (this.beforeunloads[i] == _beforeunload) {
					check1 = true;
					break;
				}
			}
			if ( check1 == false ) {
				this.beforeunloads.push(_beforeunload);
			}
		}
		if ( typeof _unload === 'function' ) {
			var check2 = false;
			for (var i = 0; i < this.unloads.length; i++ ) {
				if (this.unloads[i] != _unload) {
					check2 = true;
					break;
				}
			}
			if ( check2 == false ) {
				this.unloads.push(_unload);
			}
		}

		this.message = _message || "Warning!!!";

		// Chi gan cac su kien ben duoi 1 lan
		if (this.isReady == true) return;
		this.isReady = true;

		window.onbeforeunload = function () {
			if ( !this.validNavigation() ) {
				return this.message;
			}
			for (var i = 0; i < this.beforeunloads.length; i++ ) {
				this.beforeunloads[i]();
			}
			this.validNavigation(false);
		}.bind(this);
		window.onunload = function () {
			for (var i = 0; i < this.unloads.length; i++ ) {
				this.unloads[i]();
			}
			this.validNavigation(false);
		}.bind(this);

		// Attach the event keypress to exclude the F5 refresh
		$(document).bind('keydown', function (e) {
			if (e.keyCode == 116) {
				this.validNavigation(true);
			}
		}.bind(this));
	},
	_isresetting: false,
	reset: function () {
		if (this._isresetting == true) {
			return;
		}

		this._isresetting = true;
		setTimeout(function() {
				this._validNavigation = false;
				this._isresetting = false;
				console.log("btalk.navigation.reset();");
		}.bind(this), 1000);
	},
	validNavigation: function (value) {
		if (typeof value === 'boolean') {
			this._validNavigation = value;
			// Dinh ki reset bien danh dau trang thai
			if (value == true) {
				this.reset();
			}
		} else {
			return this._validNavigation;
		}
	}
};
}));