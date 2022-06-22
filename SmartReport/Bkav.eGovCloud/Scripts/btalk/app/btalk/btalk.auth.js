// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.auth) {
        return;
    }

    btalk.auth = {
        options: {
            xmppTokenKey: "bkavAuthen",
            keystoneTokenKey: "keystoneAuth",
            loginPage: "index.html",
            authorizedPage: "jwchat.html",
            // For read xmpp token
            rXmppToken: { domain: '', path: '/' }, //{ path:'/' },
            // For read keystone token
            rKeystoneToken: { domain: '', path: '/' }, //{ path:'/'},
            // For write xmpp token
            wXmppToken: { expires: 7, domain: '', path: '/' }, //{ expires: 7, path: '/' },
            // For write keystone token
            wKeystoneToken: { expires: 7, domain: '', path: '/' },//{ expires: 7, path: '/' },
            // Domain of user is using when logining to xmpp server
            domain: "",
            FILE_ACTIVE: false,
            remember: false
        },

        keystoneAuth: false,
        xmppAuth: false,

        init: function (options) {
            this.options = $.extend(true, {}, this.options, options);
        },

        // trang thai danh dau da khoi tao hay chua
        isReadyCm: false,
        _initCm: function (debugJid) {
            if (this.isReadyCm == true) {
                return;
            }

            this.isReadyCm = true;
            btalk.cm._init(this.getJID() || debugJid, btalk.config.CM);
        },

        // Dam bao chi goi lay ban be, lich su chat... 1 lan duy nhat, ngay ca khi mat ket noi va reconnect lai.
        isReadyCmRoster: false,
        _initCmRoster: function (success) {
            if (this.xmppAuth != true) {
                return;
            }
            if (this.isReadyCmRoster == true) {
                if (typeof success === 'function') {
                    success();
                }
                return;
            }
            this.isReadyCmRoster = true;

            // Phao goi ham nay o day ma khong goi trong btalk.connectionManager.js duoc
            // de dam bao chi goi khi login lan dau. Cac lan reconnect do may ket noi thi khong can goi.
            // btalk.cm.getRoster();

            if (typeof success === 'function') {
                success();
            }
        },

        /**
        * Dang nhap
        *
        * Note: kich ban hien tai dang chap nhan co the login file server khong thanh cong
        */
        firstLogin: true,
        login: function (username, password, remember, error) {
            this.options.remember = remember || false;

            var _debugJid = (username.indexOf("@") == -1) ? username + "@" + this.options.domain : username;
            // Goi sau khi gan _jid
            this._initCm(_debugJid);

            error = error || function () { };

            var successKeystone = function () {
                this.keystoneAuth = true;
            };

            /*
             * CuongNT - 24/12/2015: Them de handle goi tin <success ...
             * de lay token sau khi login thanh cong ban username/pass
             */
            var handleTokenXmpp = function (success) {
                // Fix lỗi trên ie8 không có textContent
                var token = success.textContent || success.text || success.innerText;
                if (token) {
                    // TODO: Cho nay dung ham b64decode trong jsjac.... can xem dung ham khac
                    // save xmpp token to cookie
                    this.saveXmppToken(b64decode(token));
                } else {
                    error("Server khong tra ve token, hoac loi khi ghi token ra cookie.");
                }
            };

            // Xay ra sau handleTokenXmpp.
            var successXmpp = function () {
                var token = this.getXmppToken();
                if (token && token.length > 0) {
                    this.xmppAuth = true;
                    // Lam nhiem vu duy nhat la chuyen vao giao dien hoi thoai
                    window.location.href = this.options.authorizedPage;
                } else {
                    error("Server khong tra ve token, hoac loi khi ghi token ra cookie.");
                }
            };

            // Neu la lan dau thi gan hanlde
            if (this.firstLogin == true) {
                this.firstLogin = false;

                btalk.cm.login(username, password, successXmpp.bind(this), error, handleTokenXmpp.bind(this));
            }
            else {
                btalk.cm.login(username, password);
            }
        },

        firstReconnect: true,
        firstFileReconnect: true,
        reconnect: function (success, error) {
            this._initCm();

            error = error || function () { };
            success = success || function () { };

            if (this.hasXmppToken()) {
                // callback, goi khi reconnect thanh cong
                var successXmpp = function () {
                    this.xmppAuth = true;
                    this._initCmRoster(success);

                    /*
                    TamDN - 30/6/2017 - Khởi tạo cache danh sách đã xem
                    */
                    btalk.VIEWED_CACHE_LIST = new btalk.cache.CacheManager();

                    // neu co kich hoat tinh nang gui file
                    if (this.options.FILE_ACTIVE === true) {
                        var fmOps = {};
                        // Neu la lan dau thi gan hanlde
                        if (this.firstFileReconnect == true) {
                            this.firstFileReconnect = false;
                        }
                        else {
                            // login server file
                            //fmOps = $.extend({}, btalk.config.FILESERVER, { tenant: this.getJID().split("@")[0] });
                            //btalk.fm._init(fmOps);
                            //btalk.fm.reconnect(this.getXmppToken(), successKeystone.bind(this));
                        }
                    }
                };

                // callback, goi khi Loi khi reconnect
                var errorXmpp = function (e) {
                    // Tra xu ly loi cho giao dien
                    error(e);
                    // Xong van dam bao chac chan da logout neu la loi "Authorization failed"
                    switch (e.getAttribute('code')) {
                        case '401':
                            this.logout();
                            return false;
                            break;
                    }
                };

                var successKeystone = function () {
                    this.keystoneAuth = true;

                    // Gan lai token cho keystone theo token eGov
                    // TODO: De tam de test
                    window.JSTACK.Keystone.params.token = this.getXmppToken();
                };

                // Neu la lan dau thi gan hanlde
                if (this.firstReconnect == true) {
                    this.firstReconnect = false;
                    // Thuc hien reconnect                   
                    btalk.cm.reconnect(this.getXmppToken(), successXmpp.bind(this), errorXmpp.bind(this));
                }
                    // Nguoc lai thi khong
                else {
                    btalk.cm.reconnect(this.getXmppToken());
                }
            } else {
                this.logout();
                return false;
            }
        },

        logout: function () {
            this.xmppAuth = false;
            return;
            btalk.cm.logout();
            // neu co kich hoat tinh nang gui file
            if (this.options.FILE_ACTIVE === true) {
                btalk.fm.logout();
            }
            this._removeToken();
            // TODO: Kiem tra de khong redirect lap cho nay
            if (this.options.loginPage) {
                window.location.href = this.options.loginPage;
            }
        },

        available: function () {
            btalk.cm.changeStatus("available");
        },

        unavailable: function () {
            btalk.cm.changeStatus('offline');
        },

        hasXmppToken: function () {
            var token = this.getXmppToken();
            return token && token.length > 0;
        },

        getExpiresOfXmppToken: function () {
            return this.options.remember ? this.options.wXmppToken : { expires: null };
        },

        saveXmppToken: function (token) {
            $.cookie(this.options.xmppTokenKey, token, this.getExpiresOfXmppToken());
        },

        getXmppToken: function () {
            return $.cookie(this.options.xmppTokenKey);
        },

        getExpiresOfKeystoneToken: function () {
            return this.options.remember ? this.options.wKeystoneToken : { expires: null };
        },

        saveKeystoneToken: function (token) {
            $.cookie(this.options.keystoneTokenKey, token, this.getExpiresOfKeystoneToken());
        },

        getKeystoneToken: function () {
            return $.cookie(this.options.keystoneTokenKey);
        },

        _removeToken: function () {
            //$.cookie(this.options.xmppTokenKey, "", this.options.rXmppToken);
            //$.cookie(this.options.keystoneTokenKey, "", this.options.rKeystoneToken);
        },

        /** Tra ve account@domain cua tai khoan dang nhap hien tai (khong gom resource) */
        getJID: function () {
            var token = this.getXmppToken();
            if (token && token.length > 0) {
                return this.findKey(token, 'user').toLowerCase();
            } else {
                return "";
            }
        },

        findKey: function (token, key) {
            var tokenArr = btalk.hex2string(token.split('_')[2]).split(';');
            for (var i = 0; i < tokenArr.length; i++) {
                if (tokenArr[i].split('=')[0] == key &&
                    tokenArr[i].split(':').length == 2) {
                    return tokenArr[i].split(':')[1];
                }
            }
            return "";
        },

        /** Tra ve account@domain/resource cua tai khoan dang nhap hien tai */
        getFullJID: function () {
            return this.getJID + '/' + this.getResource();
        },

        /** Tra ve resource cua tai khoan dang nhap hien tai */
        getResource: function () {
            return btalk.cm.getResource();
        }
    };
})(window.jQuery, window.btalk = window.btalk || {});