// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.cm) return;

    btalk.cm = {

        //#region config

        JSJACCON: null,
        DEBUG: null,
        disco: null,
        // JID cac dich vu server ho tro:
        // JID cua dich vu luu tru tin nhan (message archive)
        // ARCHIVE_SERVICE_JID: undefined,
        // JID cua dich vu chat nhom, vi du: conference.bmail.vn
        MUC_SERVICE_JID: undefined,
        options: {
            debug: {
                // Custome debug
                Debugger: null,
                // turn debugging on
                active: false,
                // if true only DEBUGJID gets the debugger
                useDebugJid: false,
                // which user get's debug messages
                debugJid: "admin@btalk.vn",
                // debug-level 0..4 (4 = very noisy)
                debugLvl: 2
            },

            xmpp: {
                connect_port: 5222,
                connect_host: "192.168.140.133",
                connect_secure: true,
                domain: "",
                conference: "",
                backendType: "binding",
                defaultResource: "btalk",
                httpbase: "/bosh/",
                baseDatetimeQuery: '2015-01-01T00:00:00Z',
                chatterNextCounts: 15,
                messageNextCounts: 40
            },

            cookie: {
                expiredDay: 7,
                // br: btalk resource
                resourceKey: "br"
            },

            client: {
                version: ""
            },

            file: {
                seperatekey: "!@!"
            },

            loginPage: "index.html"
        },

        // saved states
        savedStates: {
            onlstat: '',
            onlmsg: '',
            onlprio: '8',
            openedDepartments: []
        },

        // saved preferences
        savedPrefs: {
            timerval: 30 * 1000, // 30s
            autoPopup: true,
            autoPopupAway: false,
            playSounds: true,
            forcusWindows: true,
            timestamps: false,
            enablelog: false
        },

        onlineStatus: {
            "available": "online",
            "chat": "free for chat",
            "away": "away",
            "xa": "not available",
            "dnd": "do not disturb",
            "invisible": "invisible",
            "unavailable": "offline"
        },

        // Trang thai da goi ham logout() chua
        logoutCalled: false,
        _events: [],
        isReady: false,

        //#endregion

        // Duoc goi trong btalk.auth.js
        _init: function (debugJid, options) {
            if (this.isReady) { return; }
            this.isReady = true;

            this.options = $.extend(true, {}, this.options, options);

            /* initialise debugger */
            if (!this.DEBUG || typeof (this.DEBUG) == 'undefined' || !this.DEBUG.start) {
                if (this.options.debug.active && (!this.options.debug.useDebugJid || this.options.debug.debugJid == debugJid)) {
                    if (typeof (this.options.debug.Debugger) != 'undefined') {
                        this.DEBUG = new this.options.debug.Debugger(this.options.debug.debugLvl, 'Btalk ' + debugJid);
                        this.DEBUG.start();
                    } else {
                        this.DEBUG = new window.JSJaCConsoleLogger(this.options.debug.debugLvl);
                    }
                } else {
                    this.DEBUG = {
                        log: function () {
                        }
                    };
                }
            }

            /* create new connection */
            var oArg = {
                oDbg: this.DEBUG,
                httpbase: new URL(egov.connections.ChatLink).origin + '/bosh/', // this.options.xmpp.httpbase,
                timerval: this.savedPrefs.timerval
            };

            if (this.options.xmpp.backendType == 'binding')
                this.JSJACCON = new window.JSJaCHttpBindingConnection(oArg);
            else
                this.JSJACCON = new window.JSJaCHttpPollingConnection(oArg);

            this.JSJACCON.registerHandler('presence', this.handlePresence.bind(this));

            this.JSJACCON.registerHandler('ondisconnect', this.handleDisconnect.bind(this));
            this.JSJACCON.registerHandler('onconnect', this.handleConnected.bind(this));
            this.JSJACCON.registerHandler('onerror', this.handleConError.bind(this));
            this.JSJACCON.registerHandler('ontoken', this.handleToken.bind(this));

            this.JSJACCON.registerHandler('message', this.handleReceiveMessage.bind(this));
        },

        //#region CONNECT TO SERVER & LOGIN - BAT DAU QUA TRINH KET NOI SERVER & DANG NHAP *

        handleConError: function (e) {
            /*
             * Xu ly tat cac cac loi trong qua trinh ket noi toi server chat
             */

            this._handleEvent('onerror', e);
            switch (e.getAttribute('code')) {
                case '401':
                    // Authorization Failed
                    if (!this.JSJACCON.connected()) {
                        this._handleEvent('con_error_authorization_failed');
                    }
                    break;
                case '402':
                    // Invalid SID: qua thoi gian toi da giu ket noi BOSH giua client, server
                    this._handleEvent('con_error_invalid_sid_error');
                    break;
                case '409':
                    // Registration failed!\n\nPlease choose a different username!
                    this._handleEvent('con_error_conflict_name');
                    break;
                case '503':
                    // Service unavailable
                    this._handleEvent('con_error_xmpp_server_unavailable');
                    break;
                case '500':
                    // Internal Server error
                    if (!this.JSJACCON.connected() && !this.logoutCalled && this.savedStates.onlstat != 'offline') {
                        this._handleEvent('con_error_internal_server_error');
                    }
                    break;
                default:
                    // this shouldn't happen :)
                    this._handleEvent('con_error_default');
                    break;
            }
        },

        handleDisconnect: function () {
            /*
             * Xu ly mat ket noi toi server XMPP
             */

            if (this.logoutCalled || this.savedStates.onlstat == 'offline')
                return;
            this._handleEvent('ondisconnect');
        },

        handleConnected: function (iq) {
            /** Xu ly khi tao ket noi toi server XMPP thanh cong, tuc:
             * - Xay ra sau handleToken neu co kich hoat auth bang token
             * - Xay ra khi bind resource thanh cong len server
             */
            // btalk.auth cung handle su kien nay va lam cac viec sau:
            // 1. Khoi tao btalk.ROSTER
            // 2. Khoi tao btalk.ROSTER.currentuser
            // 4. Khoi tao AppView.js tren mainApp (main.js)

            this._handleEvent('onconnect');
            this.DEBUG.log("Connected", 0);
        },

        handleToken: function (token) {
            // Xay ra truoc handleConnected, va sau khi auth thanh cong bang type="Token"

            this._handleEvent('ontoken', token);
        },

        login: function (username, password, success, error, token) {
            // username, pass null or "" then return
            if (!username || !password) {
                return;
            }

            if (success && typeof success == 'function') {
                this.registerHandler('onconnect', success);
                // CuongNT - 24/12/2015: Tu them de lay token sau khi login bang username/pass
                this.registerHandler('ontoken', token);
            }

            if (error && typeof error == 'function') {
                this.registerHandler('onerror', error);
            }

            // TODO: resource sinh random theo tieu chi nao do va luu vao cookie
            var userDomain = this.options.xmpp.domain;
            if (username.indexOf("@") != -1) {
                var params = username.split("@");
                username = params[0];
                userDomain = params[1];
            }
            var oArg = {
                domain: userDomain,
                username: username,
                pass: password,
                resource: this.getResource(),
                register: false,

                version: this.options.client.version,
                allow_plain: true,
                allow_token: false
            };

            if (this.options.xmpp.backendType == 'binding') {
                oArg.port = this.options.xmpp.connect_port;
                oArg.host = this.options.xmpp.connect_host;
                oArg.secure = this.options.xmpp.connect_secure;
            }
            this.JSJACCON.connect(oArg);
        },

        reconnect: function (token, success, error) {
            if (success && typeof success == 'function') {
                this.registerHandler('onconnect', success);
            }

            if (error && typeof error == 'function') {
                this.registerHandler('onerror', error);
            }

            // [TODO] resource lay tu cookie.
            var oArg = {
                domain: this.options.xmpp.domain,
                token: token,
                resource: this.getResource(),
                register: false,

                version: this.options.client.version,
                allow_plain: false,
                allow_token: true
            };

            if (this.options.xmpp.backendType == 'binding') {
                oArg.port = this.options.xmpp.connect_port;
                //oArg.host = this.options.xmpp.connect_host;
                oArg.secure = this.options.xmpp.connect_secure;
            }

            this.JSJACCON.connect(oArg);
        },

        //TODO can sua de co the mo 2 tab tren 1 trinh duyet khong bi conflict
        getResource: function () {
            var resource = $.cookie(this.options.cookie.resourceKey);
            if (!resource) {
                resource = btalk.browser.OSName + "_" + btalk.browser.browserName
                    + "_" + btalk.browser.fullVersion + Date.now();
                $.cookie(this.options.cookie.resourceKey, resource, this.options.cookie.expiredDay);
            }
            return resource || this.options.xmpp.defaultResource;
        },

        /** Ket thuc qua trinh login */
        _handle_logined: function () {
            this._handleEvent('_handle_logined');
        },

        /**
         * @states: trang thai su dung cuoi cung
         * @prefs: thiet lap cau hinh cuoi cung
         */
        logout: function (states, prefs) {
            this.logoutCalled = true;

            if (!this.JSJACCON.connected())
                return;

            /* save state */
            var iq = new window.JSJaCIQ();
            iq.setIQ(null, 'set');
            var query = iq.setQuery('jabber:iq:private');
            var aNode = query.appendChild(iq.buildNode('jwchat', {
                'xmlns': 'jwchat:state'
            }));
            if (states) {
                // save presence
                if (states.onlstat && states.onlstat != 'offline')
                    aNode.appendChild(iq.buildNode('presence', states.onlstat));

                // save status message
                if (states.onlmsg && states.onlmsg != '')
                    aNode.appendChild(iq.buildNode('onlmsg', states.onlmsg));

                // save department tree state
                if (states.openedDepartments && states.openedDepartments.length > 0) {
                    var _openeddepartments = '';
                    for (var i in states.openedDepartments)
                        _openeddepartments += i + ",";
                    if (_openeddepartments != '')
                        aNode.appendChild(iq.buildNode('openedDepartments', _openeddepartments));
                }
            }
            this.DEBUG.log(iq.xml(), 2);
            this.JSJACCON.send(iq);
            var aPresence = new window.JSJaCPresence();
            aPresence.setType('unavailable');
            this.JSJACCON.send(aPresence);
            this.JSJACCON.disconnect();
        },

        //#endregion

        //#region Register and Unregister Events

        registerHandler: function (event) {
            event = event.toLowerCase(); // don't be case-sensitive here
            var eArg = {
                handler: arguments[arguments.length - 1],
                childName: '*',
                childNS: '*',
                type: '*'
            };
            if (arguments.length > 2)
                eArg.childName = arguments[1];
            if (arguments.length > 3)
                eArg.childNS = arguments[2];
            if (arguments.length > 4)
                eArg.type = arguments[3];

            // CuongNT - 12/1/2016: Khong cho dang ki 1 ham 2 lan
            if (this._events[event]) {
                var arr = this._events[event];
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].handler == eArg.handler) {
                        return;
                    }
                }
            }

            if (!this._events[event])
                this._events[event] = [eArg];
            else
                this._events[event] = this._events[event].concat(eArg);

            // sort events in order how specific they match criterias thus using
            // wildcard patterns puts them back in queue when it comes to
            // bubbling the event
            this._events[event] =
            this._events[event].sort(function (a, b) {
                var aRank = 0;
                var bRank = 0;

                if (a.type == '*')
                    aRank++;
                if (a.childNS == '*')
                    aRank++;
                if (a.childName == '*')
                    aRank++;
                if (b.type == '*')
                    bRank++;
                if (b.childNS == '*')
                    bRank++;
                if (b.childName == '*')
                    bRank++;

                if (aRank > bRank)
                    return 1;

                if (aRank < bRank)
                    return -1;

                return 0;
            });
            this.DEBUG.log("registered handler for event '" + event + "'", 2);

            return this;
        },

        unregisterHandler: function (event, handler) {
            event = event.toLowerCase(); // don't be case-sensitive here

            if (!this._events[event])
                return this;

            var arr = this._events[event], res = [];
            if (typeof handler === "undefined") this._events[event] = res;
            else {
                for (var i = 0; i < arr.length; i++)
                    if (arr[i].handler != handler)
                        res.push(arr[i]);

                if (arr.length != res.length) {
                    this._events[event] = res;
                    this.DEBUG.log("unregistered handler for event '" + event + "'", 2);
                }
            }

            return this;
        },

        _handleEvent: function (event, arg) {
            event = event.toLowerCase(); // don't be case-sensitive here
            this.DEBUG.log("incoming event '" + event + "'", 3);
            if (!this._events[event])
                return;
            this.DEBUG.log("handling event '" + event + "'", 2);

            for (var i = 0; i < this._events[event].length; i++) {
                var aEvent = this._events[event][i];
                if (typeof aEvent.handler == 'function') {
                    if (arg) {
                        if (arg.pType) { // it's a packet
                            if ((!arg.getNode().hasChildNodes() && aEvent.childName != '*') ||
                                (arg.getNode().hasChildNodes() &&
                                !arg.getChild(aEvent.childName, aEvent.childNS))) {
                                continue;
                            }
                            if (aEvent.type != '*' && arg.getType() != aEvent.type) {
                                continue;
                            }
                            this.DEBUG.log(aEvent.childName + "/" + aEvent.childNS + "/" + aEvent.type + " => match for handler " + aEvent.handler, 3);
                        }
                        if (aEvent.handler(arg)) {
                            // handled!
                            break;
                        }
                    } else if (aEvent.handler()) {
                        // handled!
                        break;
                    }
                }
            }
        },

        //#endregion

        //#region Handle Danh sách bạn bè, danh sách online, histories

        getRoster: function () {
            // Todo: Dang duoc goi tu btalk.auth, cần gọi từ Roster

            var iq = btalk.messageFactory.getRosterIqRequest();
            this.JSJACCON.send(iq, this.getRosterResult.bind(this));
        },

        getRosterResult: function (iq) {
            if (!iq || iq.getType() != 'result') {
                if (iq)
                    this.DEBUG.log("Error fetching roster:\n" + iq.xml(), 1);
                else
                    this.DEBUG.log("Error fetching roster", 1);

                return;
            }

            this._handleEvent('_handle_iq_roster_result', this._parseResultToJson(iq)[0]);
        },

        getOnline: function () {
            var iq = btalk.messageFactory.getOnlineIqRequest();
            this.JSJACCON.send(iq, this.getOnlineResult.bind(this));
        },

        getOnlineResult: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            this._handle_iq_online_result(iq);

            // Đưa vào đây do load form home-desktop sau các sự kiện này
            // Todo: cần xem lại cơ chế khi login thẳng.
            this._handle_logined();
        },

        _handle_iq_online_result: function (iq) {
            var iqJson = this._parseResultToJson(iq)[0];

            if (!iqJson || !iqJson.presences || !iqJson.presences.presence) return;

            var onlineJids = _.pluck(iqJson.presences.presence, 'from');

            btalk.ROSTER.updateOnlines(onlineJids);
            this._handleEvent('_handle_iq_online_result', onlineJids);
        },

        getHistories: function (skip, take) {
            var start = this.options.xmpp.baseDatetimeQuery;

            var iq = btalk.messageFactory.getHistoryIqRequest(start, skip, take);
            this.JSJACCON.send(iq, this.getHistoriesResult.bind(this));
        },

        getHistoriesResult: function (iq) {
            if (!iq || iq.getType() != 'result') {
                return;
            }

            var result = this._parseResultToJson(iq)[0];
            var histories = _.isArray(result.list.chat) ? result.list.chat : [result.list.chat];
            this._handleEvent('_handle_iq_list_result', histories || []);
        },

        //#endregion

        //#region Handle Group: get member, add member, remove member, rename

        getMembers: function (groupId) {
            var iq = btalk.messageFactory.GetMembersIqRequest(groupId);
            this.JSJACCON.send(iq, this._getMembersResult.bind(this));
        },

        _getMembersResult: function (iq) {
            var response = this._parseResultToJson(iq);
            var result = {};

            if (!response || !response.query || !response.query.item) return;

            result.groupId = response.from;
            result.members = _.pluck(response.query.item, 'jid');
            this._handleEvent('_handle_getMembersGroup', result);
        },

        _handle_getMembersGroup: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('_handle_getMembersGroup', iqJson);
        },

        _handle_addMembersGroup: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('_handle_addMembersGroup', iqJson);
        },

        _handle_reanameGroup: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('_handle_reanameGroup', iqJson);
        },

        _handle_removeMemberGroup: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('_handle_removeMemberGroup', iqJson);
        },

        //#endregion

        //#region Handle Chat Messages

        // Sends
        sendMessage: function (message, to, id, callback) {
            // Kiểm tra có phải là tin nhắn trả lời hay không?
            var _quote = null, from, msgId, message, type;
            if (typeof message === "object") {
                _quote = message;
                message = _quote.body;
            }
            if (message === '') return;

            from = btalk.ROSTER.currentJid;
            to = btalk.cutResource(to);

            // Id khác null trong trường hợp gửi lại tin nhắn gửi chưa thành công (trạng thái client).
            msgId = id || new Date().getTime().toString();
            type = to.split("@")[1] == this.options.xmpp.conference ? btalk.CHATTYPE.GROUPCHAT : btalk.CHATTYPE.CHAT;

            message = btalk.messageFactory.sendMessage(msgId, from, to, type, message, _quote);

            var isSent = this.JSJACCON.send(message);

            var msgJson = $.xml2json(message.doc);
            if (_.isFunction(callback)) {
                msgJson.status = 'client';
                msgJson.unread = false;
                msgJson.isSent = isSent;
                msgJson.chatterJid = btalk.cutResource(to);
                callback(msgJson);
            }
        },

        sendReceived: function (msgJson, status) {
            // Gửi trạng thái tin nhắn đã nhận.
            var message;
            var from = msgJson.to; // gửi trạng thái ngược lại về người gửi.
            var to = btalk.cutResource(msgJson.from); // gửi trạng thái ngược lại về người gửi.

            // Khong xac nhan neu nguoi gui la chinh minh.
            // Xay ra khi online tren nhieu client, voi goi tin carbon
            if (msgJson.carbons == true || to == btalk.ROSTER.currentJid) {
                return;
            }

            message = btalk.messageFactory.sendStatusMessage(msgJson.id, from, to, msgJson.type, status, msgJson.secs);
            this.JSJACCON.send(message);
        },

        sendConfigs: function (configs, to, chatType, callback) {
            if (!configs || Object.keys(configs).length <= 0) {
                return;
            }

            var msgId = new Date().getTime().toString();
            var from = btalk.ROSTER.currentJid;
            to = btalk.cutResource(to);

            var message = btalk.messageFactory.sendConfigMessage(msgId, from, to, chatType, configs);
            this.JSJACCON.send(message);

            if (typeof callback === 'function') {
                var msgJson = message.JSON();
                msgJson.status = 'client';
                msgJson.unread = false;
                msgJson.chatterJid = btalk.cutResource(to);
                callback(msgJson);
            }
        },

        sendAttachments: function (message, callback) {
            var that = this;
            this.JSJACCON.send(message);

            if (typeof callback === 'function') {
                var msgJson = message.JSON();
                msgJson.status = 'client';
                callback(msgJson);
            }
        },

        sendComposing: function (to, type) {
            /**
             * CuongNT - 6/4/2016
             * Kich ban bao typing:
             * - Bat dau typing khi:
             *   + Nhan duoc composing cua chatter HIEN TAI.
             *     Tu dong dung typing neu sau n giay khong nhan goi composing tiep theo.
             * - Dung typing khi:
             *   + Nhan duoc tin chat toi cua chatter HIEN TAI
             *   + Khi click chuyen sang chatter khac
             * Kich ban gui message composing:
             * - Gui khi bat dau go trong textbox chat. Sau do lien tuc update thoi diem go cuoi.
             * - Dinh ki sau N giay kiem tra lai thoi diem cuoi, neu qua M giay so voi hien tai thi khong gui tiep composing.
             *   Nguoc lai thi tiep tuc gui composing de bao trang thai dang typing.
             *   TamDN - 10/12/2016
             *   Them tham so type phan biet chat 1-1 hay chat nhom
             */

            var from, to, message, msgId;
            var msgId = new Date().getTime().toString();

            from = btalk.ROSTER.currentJid;
            to = btalk.cutResource(to);

            message = btalk.messageFactory.sendComposingMessage(msgId, from, to, type);
            this.JSJACCON.send(message);
        },

        // Receives
        handleReceiveMessage: function (oMsg) {
            /*
                Xử lý gói tin đến: gói tin chát, gói tin trạng thái, gói tin invite
            */
            var messageData = $.xml2json(oMsg.doc);

            // Gói tin lỗi
            if (oMsg.getType() === 'error') {
                return this._handleEvent('message_error', messageData);
            }

            // Gói tin mời vào nhóm
            var invitation = oMsg.getChild('x', 'http://jabber.org/protocol/muc#user');
            if (invitation) {
                return this._handleInviteToGroup(messageData, invitation);
            }

            this._handleChatMessage(oMsg, messageData);
        },

        _handleChatMessage: function (oMsg, msgJson) {
            var i, leng, offline;
            var from = btalk.cutResource(oMsg.getFrom());
            var type = oMsg.getType();

            // Tính thời điểm nhận gói tin offline
            var xNode = oMsg.getNode().getElementsByTagName('x');
            for (i = 0, leng = xNode.length; i < leng; i++) {
                if (xNode.item(i).getAttribute('xmlns') === 'jabber:x:delay') {
                    offline = xNode.item(i);
                    break;
                }
            }

            if (offline) {
                var stamp = offline.getAttribute('stamp');
                oMsg.jwcTimestamp = new Date(Date.UTC(stamp.substring(0, 4), stamp.substring(4, 6) - 1, stamp.substring(6, 8), stamp.substring(9, 11), stamp.substring(12, 14), stamp.substring(15, 17)));
            } else {
                oMsg.jwcTimestamp = new Date();
            }

            if (msgJson.received) {
                return this._handleStatusMessage(msgJson);
            }

            if (msgJson.filename) {
                return this._handleFileMessage(msgJson);
            }

            if (msgJson.sent && msgJson.sent.forwarded && msgJson.sent.forwarded.message) {
                // Xep-0280: Message Carbons: http://xmpp.org/extensions/xep-0280.html
                // Kiem tra message carbons, tuc message duoc gui di boi client khac cung online tai khoan hien tai
                // va duoc forward ve client hien tai de dong bo

                msgJson = msgJson.sent.forwarded.message;
                msgJson.carbons = true;
            }

            msgJson = this._parseRealtimeMessage(msgJson);

            msgJson.isContentMessage && this._handle_roster_message(msgJson);

            this._handleEvent('message', msgJson);

            // Chi xac nhan neu la goi message chat 1-1.
            // Mesasge groupchat chi can xac nhan da xem, khong can xac nhan da toi.
            msgJson.body && msgJson.carbons != true && type === btalk.CHATTYPE.CHAT
                && this.sendReceived(msgJson, 'success');
        },

        _handle_roster_message: function (msgJson) {
            this._handleEvent('_handle_update_roster', msgJson);
        },

        _handleStatusMessage: function (msgJson) {
            this._handleEvent('_handle_status_message', msgJson);
        },

        _handleFileMessage: function (msgJson) {
            this._handleEvent('_handle_file_message', msgJson);
        },

        handleMessageConfig: function (oMsg) {
            var msgJson = $.xml2json(oMsg.doc);
            this._handleEvent('_handle_oMsg_configMemberGroup', msgJson);
        },

        _handleInviteToGroup: function (msgJson, invatation) {
            // Todo: xử lý xong ko thấy dùng để làm gì?

            var from, reason, pass;

            var aInvite = invatation.getElementsByTagName('invite').item(0);

            from = aInvite.getAttribute('from');
            if (aInvite.firstChild && aInvite.firstChild.nodeName == 'reason' && aInvite.firstChild.firstChild) {
                reason = aInvite.firstChild.firstChild.nodeValue;
            }
            if (invatation.getElementsByTagName('password').item(0)) {
                pass = invatation.getElementsByTagName('password').item(0).firstChild.nodeValue;
            }

            // Them group chat jid vao danh sach ban be neu chua co
            var user = btalk.ROSTER.getUserByJID(btalk.cutResource(from));

            // users not in roster (yet)
            if (!user) {
                user = btalk.ROSTER.addUser(btalk.cutResource(from));
            }

            user.iwArr = user.iwArr || [];

            return this._handleEvent('group_invite', msgJson);
        },

        _parseRealtimeMessage: function (message) {
            /*
                 * body: "99999"
                   clienttime: "1530000347185"
                    from: "tungnt@sonongnghiep.tayninh.gov.vn"
                    id: "1530000347185"
                    to: "lytt@sonongnghiep.tayninh.gov.vn"
                    type: "chat"
                 */
            var that = this;
            message.body = btalk.text(message.body);

            // 1. 	Neu la message gui di, duoc forward tu client khac dang dang nhap cung tai khoan
            message.chatterJid = message.carbons === true ? message.to : message.from;
            message.chatterJid = btalk.cutResource(message.chatterJid);

            // 2. 	Neu la message cap nhat trang thai gui/nhan
            if (message.received) {
                message.status = message.received.status || 'viewed';
            }

            // 3. 	Luon la tin chua doc do moi nhan tu server. Chi khi ve len giao dien moi xet trang thai tin da doc hay chua.
            message.unread = true; // message.carbons === true ? false : true;
            message.processType = this._checkProcessTypeOfMessage(message);
            if (!message.secs) {
                var start = new Date(this.options.baseDatetimeQuery);
                var secs = ((new Date()).getTime() - start.getTime()) / 1000;
                message.secs = secs;
            }

            message.senderJid = btalk.cutResource(message.from);
            // Nguoi nhan thuc su
            message.receiverJid = btalk.cutResource(message.to);

            message.attachment && !_.isArray(message.attachment) && (message.attachment = [message.attachment]);

            var receiverFileAcc = message.receiverJid;
            _.each(message.attachment, function (a) {
                var senderTenantId = a.tenantid || "";
                a.extension = _.last(a.name.split('.'));
                a.name = egov.fileExtension.getFileName(a.name);
                a.size = egov.fileExtension.getSizeText(a.size);
                a.url = btalk.fm.geturl(senderTenantId, receiverFileAcc, a.object, String.format("{0}.{1}", a.name, a.extension), a.sentDate, 101);
            });

            this._parseConfigMessage(message);

            message.isContentMessage = this.isContentMessage(message.processType);

            return message;
        },

        _checkProcessTypeOfMessage: function (msg) {
            if (msg.received) {
                return "received";
            }

            if (msg.msgContentType === 'file') {
                return "file";
            }

            if (msg.msgContentType === 'image') {
                return 'image';
            }

            if (msg.config != undefined) { // msg.type == btalk.CHATTYPE.GROUPCHAT && 
                return "roomconfig";
            }

            var attachment = msg.attachment ? (_.isArray(msg.attachment) ? msg.attachment : [msg.attachment]) : null;
            if ((msg.type == btalk.CHATTYPE.CHAT && msg.body && !attachment) || (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.body && !attachment)) {
                return btalk.CHATTYPE.CHAT;
            }

            if ((msg.type == btalk.CHATTYPE.CHAT || msg.type == btalk.CHATTYPE.GROUPCHAT) && attachment
                    && attachment[0].type && attachment[0].type.startWith('image')) {
                return 'image';
            }

            if ((msg.type == btalk.CHATTYPE.CHAT || msg.type == btalk.CHATTYPE.GROUPCHAT) && attachment) {
                return "file";
            }

            if ((msg.type == btalk.CHATTYPE.CHAT && msg.percentage != undefined) || (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.percentage)) {
                return "percentage";
            }

            if (((msg.type == btalk.CHATTYPE.CHAT || msg.type == btalk.CHATTYPE.GROUPCHAT) && msg.composing != undefined)) {
                return "composing";
            }

            console.log("WARNING: MESSAGE NAY KHONG DUOC NHAN DIEN");
            return "";
        },

        _parseConfigMessage: function (msg) {
            var isConfigMsg;
            if (!msg.config || msg.processType !== 'roomconfig') {
                return;
            }

            var editBy;
            var roomName;
            var newMembers;
            var removeMember;
            var timeStartConversion;
            var bodyMsgSuggestchatBmail;

            msg.config.body_msg_suggest_chatBmail && (bodyMsgSuggestchatBmail = msg.config.body_msg_suggest_chatBmail);

            _.each(msg.config, function (item) {
                item.edit_by && (editBy = item.edit_by);
                item.roomconfig_roomname && (roomName = item.roomconfig_roomname);
                item.new_members && (newMembers = item.new_members);
                item.remove_member && (removeMember = item.remove_member);
                item.start_conversion && (timeStartConversion = item.start_conversion);
            });

            msg.isConfigMsg = true;

            if (timeStartConversion) {
                msg.config = { value: new Date(parseInt(timeStartConversion)).format('HH:mm dd/MM/yyyy') };
                return;
            }

            var userEdit = btalk.ROSTER.getUserByJID(editBy);
            if (!userEdit) return;

            var config = {
                userEdit: userEdit.fullname,
                icon: '',
                reason: '',
                value: ''
            };

            if (roomName) {
                config.icon = 'border_color';
                config.reason = ' đã đổi tên nhóm thành ';
                config.value = roomName;
            } else if (newMembers) {
                config.icon = 'group_add';
                config.reason = ' đã thêm ';
                config.value = newMembers;
            } else if (timeStartConversion) {
                config.icon = '';
                config.reason = '';
                config.value = new Date(timeStartConversion).format('HH:mm dd/MM/YYYY');
            } else {
                config.icon = 'block';
                config.reason = ' đã xoá ';
                config.value = removeMember;
            }

            msg.config = config;
        },

        //#endregion

        //#region Load Messages

        getNextPageOfMessages: function (jid, skip, take, type) {
            var start = this.options.xmpp.baseDatetimeQuery;
            var message = btalk.messageFactory.GetMessageHistoriesIqRequest(jid, type, start, skip, take);

            this.JSJACCON.send(message, this.handle_iq_retrieve_result.bind(this));
        },

        handle_iq_retrieve_result: function (iq) {
            if (!iq || iq.getType() != 'result')
                return;

            var iqJson = $.xml2json(iq.doc);
            this._handleEvent('handle_iq_retrieve_result', iqJson);
        },

        //#endregion

        //#region Roster User, Groups

        addRoster: function (aJid) {
            var iq = new window.JSJaCIQ();
            iq.setType('set');
            var query = iq.setQuery('jabber:iq:roster');
            var item = query.appendChild(iq.getDoc().createElement('item'));
            item.setAttribute('jid', aJid);
            item.setAttribute('name', aJid);
            item.setAttribute('subscription', 'both');
            this.JSJACCON.send(iq, this.addRosterResult.bind(this));

            // Them nhom hoi thoai vao danh sach lien he
            var user = btalk.ROSTER.getUserByJID(aJid);
            if (!user) {
                // Neu la add groupchat
                if (aJid.substring(aJid.indexOf('@') + 1) == this.options.xmpp.conference) {
                    user = btalk.ROSTER.addUser(aJid, aJid.substring(0, aJid.indexOf('@')), '', ["Chat Rooms"]);
                    user.status = 'available';
                    user.roster = new window.GroupchatRoster();
                    user.roster.nick = aJid.substring(0, aJid.indexOf('@')); // remember my nickname
                }
                    // Neu la add roster
                else {
                    user = btalk.ROSTER.addUser(aJid);
                }
            }
        },

        addRosterResult: function () {
        },

        addRosters: function (aJids) {
            if (!aJids || aJids.length <= 0) {
                return;
            }

            var iq = new window.JSJaCIQ();
            iq.setType('set');
            iq.setID("add_rosters_" + (new Date()).getTime());
            var query = iq.setQuery('jabber:iq:roster');

            for (var i = 0; i < aJids.length; i++) {
                var item = query.appendChild(iq.getDoc().createElement('item'));
                item.setAttribute('jid', aJids[i]);
                item.setAttribute('name', aJids[i].split('@')[0]);
                item.setAttribute('subscription', 'both');
                if (((i + 1) % 200) == 0) {
                    this.JSJACCON.send(iq, this.addRosterResult.bind(this));
                    iq = new window.JSJaCIQ();
                    iq.setType('set');
                    iq.setID("add_rosters_" + (new Date()).getTime());
                    query = iq.setQuery('jabber:iq:roster');
                }
            }
            if ((aJids.length) % 200 != 0) {
                this.JSJACCON.send(iq, this.addRosterResult.bind(this));
            }

            //// Them nhom hoi thoai vao danh sach lien he
            //for (var i = 0; i < aJids.length; i++) {
            //    // Them nhom hoi thoai vao danh sach lien he
            //    var user = btalk.ROSTER.getUserByJID(aJids[i]);
            //    if (!user) {
            //        // Neu la add groupchat
            //        if (aJids[i].substring(aJids[i].indexOf('@') + 1) == this.options.xmpp.conference) {
            //            user = btalk.ROSTER.addUser(aJids[i], aJids[i].substring(0, aJids[i].indexOf('@')), '', ["Chat Rooms"]);
            //            user.status = 'available';
            //            user.roster = new window.GroupchatRoster();
            //            user.roster.nick = aJids[i].substring(0, aJids[i].indexOf('@')); // remember my nickname
            //        }
            //            // Neu la add roster
            //        else {
            //            user = btalk.ROSTER.addUser(aJids[i]);
            //        }
            //    }
            //}
        },

        addRostersResult: function () {
        },

        removeRoster: function (aJid) {
            // get fulljid
            var fulljid = btalk.ROSTER.getUserByJID(aJid).fulljid;

            var iq = new window.JSJaCIQ();
            iq.setType('set');
            var query = iq.setQuery('jabber:iq:roster');
            var item = query.appendChild(iq.getDoc().createElement('item'));
            item.setAttribute('jid', fulljid);
            item.setAttribute('subscription', 'remove');
            this.JSJACCON.send(iq);
        },

        createGroup: function (groupjid, members) {
            /*
            <presence to="cuongnt-bkav1518@conference.bmail.vn">
            </presence>
            <x xmlns="http://jabber.org/protocol/muc"/>
            */
            if (!members || members.length <= 0) {
                return;
            }
            /*
            // Khong can vi mac dinh nguoi tao nhom da la thanh vien nhom
            var hasCurrentUser = false;
            for ( var i = 0; i < members.length; i++ ) {
                if (btalk.cutResource(members[i]) == btalk.ROSTER.currentJid) {
                    hasCurrentUser = true;
                    break;
                }
            }
            if ( hasCurrentUser != true ) {
                members.push(btalk.ROSTER.currentJid);
            }*/

            groupjid = btalk.cutResource(groupjid);
            // !! Dam bao nickname trong nhom chat luon la account that do btalk serrver quy uoc vay !!
            var groupfulljid = groupjid + "/" + btalk.ROSTER.currentuser.name;
            var oPresence = new window.JSJaCPresence();
            // Bat buoc khi tao nhom thi phai co resource la chinh nick cua nguoi tao nhom
            oPresence.setTo(groupfulljid);
            //oPresence.getNode().appendChild(oPresence.getDoc().createElement('x', "http://jabber.org/protocol/muc"))

            this.JSJACCON.send(oPresence, this.createGroupResult.bind(this, groupjid, members));
        },

        createGroupResult: function (groupjid, members) {
            // Add thanh vien nhom
            this.addGroupMembers(groupjid, members);

            // Khoi tao RosterUser gan voi nhom chat
            this.addRoster(groupjid);
        },

        addGroupMembers: function (groupjid, members) {
            // groupjid, members
            groupjid = btalk.cutResource(groupjid);
            var iq = new window.JSJaCIQ();
            iq.setType('set');
            iq.setTo(groupjid);
            iq.setID("add_member");
            var query = iq.setQuery('http://jabber.org/protocol/muc#admin');

            for (var i in members) {
                var item = query.appendChild(iq.getDoc().createElement('item'));
                item.setAttribute('jid', members[i]);
                item.setAttribute('affiliation', 'owner');
            }
            this.JSJACCON.send(iq);
        },

        removeGroupMember: function (groupjid, memberjid) {
            /*
            <iq type="set" to="cuongnt-1547@muc.bmail.vn" id="ab0ea">
                <query xmlns="http://jabber.org/protocol/muc#admin">
                    <item affiliation="none" jid="cuongnt@bmail.vn"/>
                </query>
            </iq>
            */
            groupjid = btalk.cutResource(groupjid);

            var iq = new window.JSJaCIQ();
            iq.setType('set');
            iq.setTo(groupjid);
            iq.setID("remove_member");
            var query = iq.setQuery('http://jabber.org/protocol/muc#admin');
            var item = query.appendChild(iq.getDoc().createElement('item'));
            item.setAttribute('jid', memberjid);
            item.setAttribute('affiliation', 'none');
            this.JSJACCON.send(iq);
        },

        joinGroup: function (groupjid) {
            /*
            <message to="bkav#group#84984822685@conference.bmail.vn" id="1432011755798" xmlns="jabber:client" type="groupchat">
                <body>btalk-join-to-room</body>
            </message>
            */
            var oMsg = new window.JSJaCMessage();
            oMsg.setTo(btalk.cutResource(groupjid));
            oMsg.setID("join_group");
            oMsg.setXMLNS("jabber:client");
            oMsg.setType(btalk.CHATTYPE.GROUPCHAT);
            oMsg.setBody("btalk-join-to-room");
            this.JSJACCON.send(oMsg);
        },

        existGroup: function (groupjid) {
            /*
            <message to="84984822685@bmail.vn" id="1432011755798" xmlns="jabber:client" type="groupchat">
                <body>btalk-out-of-room</body>
            </message>
            */
            var oMsg = new window.JSJaCMessage();
            oMsg.setTo(btalk.cutResource(groupjid));
            oMsg.setID("exist_group");
            oMsg.setXMLNS("jabber:client");
            oMsg.setType(btalk.CHATTYPE.GROUPCHAT);
            oMsg.setBody("btalk-out-of-room");
            this.JSJACCON.send(oMsg);

            this.removeGroupMember();
        },

        renameGroup: function (groupjid, newname) {
            /*
             <iq from='dambv2@bmail.vn/chat'
               id='create2'
               to='dambv21479884877221@conference.bmail.vn'
               type='set'>
              <query xmlns='http://jabber.org/protocol/muc#owner'>
                <x xmlns='jabber:x:data' type='submit'>
                    <field var='FORM_TYPE'>
                       <value>http://jabber.org/protocol/muc#roomconfig</value>
                    </field>
                    <field var='muc#roomconfig_roomname'>
                      <value>new name</value>
                    </field>
               </x>
              </query>
            </iq>

             */

            var sentDate = new Date();
            var msgId = sentDate.getTime().toString();
            var iq = new window.JSJaCIQ();
            iq.setID("rename_group" + msgId);
            iq.setType('set');
            iq.setTo(btalk.cutResource(groupjid));

            var query = iq.setQuery('http://jabber.org/protocol/muc#owner');
            //var x = query.appendChild(iq.getDoc().createElement('x'));
            //x.setAttribute('type', 'submit');
            //x.setAttribute("xmlns", "jabber:x:data");

            var x = iq.buildNode('x', {
                'xmlns': 'jabber:x:data',
                'type': 'submit'
            });
            query.appendChild(x);

            var field_1 = x.appendChild(iq.getDoc().createElement('field'));
            field_1.setAttribute('var', 'FORM_TYPE');
            var value_1 = field_1.appendChild(iq.getDoc().createElement('value'));
            value_1.textContent = "http://jabber.org/protocol/muc#roomconfig";

            var field_2 = x.appendChild(iq.getDoc().createElement('field'));
            field_2.setAttribute('var', 'muc#roomconfig_roomname');
            var value_2 = field_2.appendChild(iq.getDoc().createElement('value'));
            value_2.textContent = newname;
            this.JSJACCON.send(iq);
        },

        //#endregion

        //#region Presence

        changeStatus: function (val, away, prio) {
            this.savedStates.onlstat = val;
            if (away)
                this.savedStates.onlmsg = away;

            if (prio && !isNaN(prio))
                this.savedStates.onlprio = prio;

            if (!this.JSJACCON.connected() && val != 'offline') {
                this._init();
                return;
            }

            var aPresence = new window.JSJaCPresence();

            switch (val) {
                case "unavailable":
                    val = "invisible";
                    aPresence.setType('invisible');
                    break;
                case "offline":
                    val = "unavailable";
                    aPresence.setType('unavailable');
                    this.JSJACCON.send(aPresence);
                    this.JSJACCON.disconnect();
                    return;
                    break;
                case "available":
                    val = 'available';
                    // needed for led in status bar
                    if (away)
                        aPresence.setStatus(away);
                    if (prio && !isNaN(prio))
                        aPresence.setPriority(prio);
                    else
                        aPresence.setPriority(this.savedStates.onlprio);
                    break;
                case "chat":
                    if (prio && !isNaN(prio))
                        aPresence.setPriority(prio);
                    else
                        aPresence.setPriority(this.savedStates.onlprio);
                default:
                    if (away)
                        aPresence.setStatus(away);

                    aPresence.setType(val);
                    if (prio && !isNaN(prio))
                        aPresence.setPriority(prio);
                    else
                        aPresence.setPriority('0');

                    aPresence.setShow(val);
            }

            this.JSJACCON.send(aPresence);

            /*
                * Send presence to chatrooms
                */
            if (btalk.ROSTER && this.savedStates.onlstat != 'invisible') {
                this.sendPresence2Groupchats(btalk.ROSTER.getGroupChats(), this.savedStates.onlstat, this.savedStates.onlmsg);
            }
        },

        sendPresence2Groupchats: function (gc, val, away) {
            var aPresence;
            for (var i = 0; i < gc.length; i++) {
                aPresence = new window.JSJaCPresence();
                aPresence.setTo(gc[i]);
                if (away && away != '')
                    aPresence.setStatus(away);
                if (val != 'available')
                    aPresence.setShow(val);
                this.JSJACCON.send(aPresence);
            }
        },

        // Su dung khi can invisible voi rieng 1 ai do
        sendCustomPresence: function (aJid, presence, msg) {
            var oPresence = new window.JSJaCPresence();
            oPresence.setTo(aJid);
            if (btalk.ROSTER.getUserByJID(aJid).roster)
                oPresence.setXMLNS();

            switch (presence) {
                case 'offline':
                    oPresence.setType('unavailable');
                case 'unavailable':
                    oPresence.setType('unavailable');
                    presence = "invisible";
                default:
                    if (presence != 'available')
                        oPresence.setShow(presence);
            }

            if (typeof (msg) != 'undefined' && msg != '') oPresence.setStatus(msg);
            this.DEBUG.log(oPresence.xml(), 2);
            this.JSJACCON.send(oPresence);
        },

        sendPresenceIDLE: function (valueIdle) {
            var aPresence = new window.JSJaCPresence();
            aPresence.setStatus('available');
            if (valueIdle) {
                // co online nhung ko lam gi.
                aPresence.setShow('away');
            } else {
                // hien tai dang online va ko idle
                aPresence.setShow('chat');
            }
            aPresence.setPriority(1);
            this.JSJACCON.send(aPresence);
        },

        handlePresence: function (presence) {
            /*
                Xử lý gói tin báo trạng thái của người dùng
            */

            var from = btalk.cutResource(presence.getFrom());
            var type = presence.getType();
            var show = presence.getShow();
            var status = presence.getStatus();

            var user = btalk.ROSTER.getUserByJID(from);
            var actor;

            if (!user) return;

            /** PRESENCE FOR GROUPCHAT */
            // handle presence for MUC
            var x = presence.getChild('x', 'http://jabber.org/protocol/muc#user');

            if (x) {
                var ofrom = presence.getFrom().substring(presence.getFrom().indexOf('/') + 1);

                this.DEBUG.log("jabber.from:" + presence.getFrom() + ", ofrom:" + ofrom, 3);

                var ouser = user.roster.getUserByJID(presence.getFrom());

                // no user? create one!
                if (!ouser) {
                    ouser = new GroupchatRosterUser(presence.getFrom(), ofrom);
                }

                var item = x.getElementsByTagName('item').item(0);
                ouser.affiliation = item.getAttribute('affiliation');
                ouser.role = item.getAttribute('role');
                ouser.nick = item.getAttribute('nick');
                ouser.realjid = item.getAttribute('jid');
                if (item.getElementsByTagName('reason').item(0)) {
                    ouser.reason = item.getElementsByTagName('reason').item(0).firstChild.nodeValue;
                }
                if (actor = item.getElementsByTagName('actor').item(0)) {
                    if (actor.getAttribute('jid') != null) {
                        ouser.actor = actor.getAttribute('jid');
                    } else if (item.getElementsByTagName('actor').item(0).firstChild != null) {
                        ouser.actor = item.getElementsByTagName('actor').item(0).firstChild.nodeValue;
                    }
                }
                if (ouser.role != '') {
                    ouser.add2Group(ouser.role + 's');
                    if (ouser.name == btalk.htmlEnc(user.roster.nick)) {
                        user.roster.me = ouser;
                        // store this reference
                        if (user.chatW.updateMe) {
                            user.chatW.updateMe();
                        }
                    }
                }

                this.DEBUG.log("ouser.jid: " + ouser.jid + ", ouser.fulljid:" + ouser.fulljid + ", ouser.name:" + ouser.name + ", user.roster.nick:" + user.roster.nick, 3);

                var nickChanged = false;
                if (x.getElementsByTagName('status').item(0)) {
                    var code = x.getElementsByTagName('status').item(0).getAttribute('code');
                    switch (code) {
                        case '201':
                            // room created -> Tu dong tao room voi config mac dinh
                            var iq = new window.JSJaCIQ();
                            iq.setType('set');
                            iq.setTo(user.jid);
                            var query = iq.setQuery('http://jabber.org/protocol/muc#owner');
                            query.appendChild(iq.buildNode('x', {
                                'xmlns': NS_XDATA,
                                'type': 'submit'
                            }));
                            this.JSJACCON.send(iq);
                            break;
                        case '303':
                            /* Khong xu ly truong hop nay */
                            break;
                        case '301':
                            /* Khong xu ly truong hop nay */
                            break;
                        case '307':
                            /* Khong xu ly truong hop nay */
                            break;
                    }
                }

                this.DEBUG.log("<" + ouser.name + "> affiliation:" + ouser.affiliation + ", role:" + ouser.role, 3);

                if (!user.roster.getUserByJID(presence.getFrom()) && !nickChanged) {
                    // add user vao danh sach thanh vien nhom chat
                    user.roster.addUser(ouser);

                    // show join message
                    var oMsg = new window.JSJaCMessage();
                    oMsg.setFrom(user.jid);
                    oMsg.setBody("" + ouser.name + " has become available");

                    // [TODO] co the thuc te khong dung truong hop nay ma dua tren goi message truc tiep nhu btalk
                    var msgJson = $.xml2json(oMsg.doc);
                    // [TODO] Luu tin chua doc vao cache tai day neu chuyen co che cache tren appview xuong roster
                    this._handleEvent('group_member_join', msgJson);
                } else if (presence.getType() == 'unavailable' && !nickChanged) {
                    // show part message
                    var oMsg = new window.JSJaCMessage();
                    oMsg.setFrom(user.jid);
                    var body = "" + ouser.name + " has left";
                    if (presence.getStatus())
                        body += ": " + presence.getStatus();
                    oMsg.setBody(body);

                    // [TODO] co the thuc te khong dung truong hop nay ma dua tren goi message truc tiep nhu btalk
                    var msgJson = $.xml2json(oMsg.doc);
                    // [TODO] Luu tin chua doc vao cache tai day neu chuyen co che cache tren appview xuong roster
                    this._handleEvent('group_member_left', msgJson);
                }

                user = ouser;
            }

            /** PRESENCE FOR USER */
            var online = undefined;
            // AVAILABLE
            if (show) {
                if (user.get('status') == 'unavailable') {
                    online = true;
                }
                // fix broken pressenc status
                if (show != btalk.CHATTYPE.CHAT && show != 'away' && show != 'xa' && show != 'dnd')
                    show = 'available';
                user.set('status', show);
            } else if (type) {
                // UNAVAILABLE hoac ....
                if (type == 'unsubscribe') {
                    user.subscription = 'from';
                    user.set('status', 'stalker');
                } else if (user.get('status') != 'stalker') {
                    user.set('status', 'unavailable');
                }
                online = false;
            } else {
                // user was offline before
                online = true;
                user.set('status', 'available');
            }

            // show away message
            if (status) {
                user.statusMsg = status;
            } else {
                user.statusMsg = null;
            }
            if ((show == 'chat' || show == 'away') && status == 'available') {
                // Fix the hien idle
                var presenceJson = $.xml2json(presence.doc);
                this._handleEvent('statusIDLE', presenceJson);
            } else if (online == true) {
                var presenceJson = $.xml2json(presence.doc);
                this._handleEvent('online', presenceJson);
            } else if (online == false) {
                var presenceJson = $.xml2json(presence.doc);
                this._handleEvent('offline', presenceJson);
            }

            if (type == 'changeStatus') {
                this._handleEvent('changeStatus', { from: from, status: status });
            }
        },

        //#endregion

        //#region Private

        isContentMessage: function (type) {
            var result = type === btalk.CHATTYPE.CHAT || type === 'file' || type === btalk.CHATTYPE.GROUPCHAT;
            return result;
        },

        bind: function (fn, obj, optArg) {
            return function (arg) {
                return fn.apply(obj, [arg, optArg]);
            };
        },

        _parseResultToJson: function (iq) {
            var result = $.xml2json(iq.doc);
            result = $.isArray(result) ? result : [result];
            return result;
        }

        //#endregion
    };
})(window.jQuery, window.btalk);