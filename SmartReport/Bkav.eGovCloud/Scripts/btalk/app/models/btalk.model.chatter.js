/**
 * Goi tin lay danh sach chatter gan nhat
 *
 * <body xmlns:stream="http://etherx.jabber.org/streams" xmlns="http://jabber.org/protocol/httpbind" xmlns:xmpp="urn:xmpp:xbosh" ack="740318" xmpp:version="1.0" host="cuongnt.bmail.vn" from="bmail.vn" secure="true">
 * 	<iq xmlns="jabber:client" type="result" to="cuongnt@bmail.vn/jwchat817" id="jwchat_history">
 * 		<list xmlns="urn:xmpp:archive">
 * 			<chat with="84984822685@bmail.vn" start="2015-11-10T08:09:52+0000" />
 * 			<chat with="tienbv@bmail.vn" start="2015-11-13T02:11:24+0000" />
 * 			<chat with="chinhtq@bmail.vn" start="2015-11-13T02:12:06+0000" />
 * 			<chat with="dambv@bmail.vn" start="2015-11-13T02:12:14+0000" />
 * 			<chat with="dungha@bmail.vn" start="2015-11-13T02:12:21+0000" />
 * 			<set xmlns="http://jabber.org/protocol/rsm">
 * 				<first index="0">0</first>
 * 				<last>4</last>
 * 				<count>32</count>
 * 			</set>
 * 		</list>
 * 	</iq>
 * </body>
 */
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function () {
    'use strict';

    btalk.model = btalk.model || {};

    if (btalk.model.chatter === true) {
        return;
    }

    btalk.model.chatter = true;

    // Chatter
    var Chatter = Backbone.Model.extend({
        // Luu toan bo object msg, dung khi gui xac nhan da xem tin
        lastmsg: null,
        messageCaches: null, // Danh sach cache lich su chat da tai truoc do
        defaults: {
            jid: "", 			// chat.with
            fulljid: "",		// jid + '/' + resource
            account: "", 		// chat.with.split('@')[0]
            type: "chat",		// chat, groupchat

            // Tin nhan cuoi cung
            lasttimestamp: null,// Thoi diem nhan tin nhan cuoi
            lastmessage: "",	// Noi dung tin nhan cuoi
            lastmessageid: null, // Id tin nhan cuoi
            isme: false,		// Neu tin nhan cuoi la tin nhan cua minh gui di. Su dung de hien thi them: "Toi: lastmessage...."

            // Tin nhan chua doc (tin moi, tin offfline)
            unread: false,		// Trang thai co tin nhan moi chua xem
            unreadcount: 0,		// So luong tin nhan moi chua xem

            // True khi duoc click chon tren giao dien
            isselected: false,
            fullname: '',		// account || fullname

            // True neu sau khi chon chatter (isselected == true),
            // va nguoi dung go noi dung chat lan dau tien.
            // Su dung de move chatter nay len top sau lan chat dau tien
            isAtTop: false,

            // Trang thai online
            online: false,		// true neu online, false nguoc lai

            rosterUser: null,	// Tro ve doi tuong RosterUser tuong ung
            roster: null,		// Tro ve doi tuong Roster: btalk.ROSTER

            // true:  khong count chatter nay khi lay danh sach chatter tiep theo tren server neu la true.
            //        true khi chatter nay duoc tao ra do tim kiem chatter.
            // false: nguoc lai.
            //		  Duoc set ve false khi hoac la co tin nhan phat sinh tren chatter nay
            //        hoac la khi cuong lay danh sach chatter tiep theo tren server ma co chua chatter nay
            isResultOfSearch: false,

            lastTypedText: "",      // Noi dung chat cuoi cung ma chua gui di
            archiveIsLoaded: false,

            // Added DamBV - 14/02/2017 - Bổ sung biến điểu khiển trạng thái kích thước.
            statusHeightChatter: "default",

            //Tin nhan cuoi cung da duoc xem hay chua, mac dinh la chua xem - false
            isSeenLastMessage: false,

            // Bien danh dau trang thai co cho phep hien thong bao hay khong.
            // false: cho phep hien, true la okie
            muteNotification: false,

            // Added DamBV  Data share file
            collectionShareImage: [],
            collectionShareFile: [],

            statusIdle: false,
        },  

        read: function (value) {
            if (value && typeof value === 'boolean') {
                this.set({ unread: !value });

                if (!value == true) {
                    this.set({ unreadcount: this.get('unreadcount') + 1 });
                } else {
                    this.set({ unreadcount: 0 });
                }

            } else {
                return !this.get('unread');
            }
        },

        unread: function (value) {
            if (value && typeof value === 'boolean') {
                this.set({ unread: value });
                if (value == true) {
                    this.set({ unreadcount: this.get('unreadcount') + 1 });
                } else {
                    this.set({ unreadcount: 0 });
                }
            } else {
                return this.get('unread');
            }
        },

        // Ham nay chi co get, khong cho set
        unreadcount: function () {
            return this.get('unreadcount');
        },

        lastmessage: function (msg) {
            if (msg) {
                msg = msg.length <= 100 ? msg : msg.substr(0, 100);
                this.set({ lastmessage: msg });
            } else {
                return this.get('lastmessage');
            }
        },

        lasttimestamp: function (time) {
            if (time) {
                this.set({ lasttimestamp: time });
            } else {
                return this.get('lasttimestamp');
            }
        },

        isAtTop: function (top) {
            if (top) {
                this.set({ isAtTop: top });
            } else {
                return this.get('isAtTop');
            }
        }
    });

    // List of Collections
    // -------------------
    var ChatterList = Backbone.Collection.extend({
        model: Chatter,
        first: 0,
        index: 0,
        last: 0,
        count: 0,
        comparator: 'lasttimestamp',

        read: function () {
            return this.where({ unread: false });
        },

        unread: function () {
            return this.where({ unread: true });
        },

        counts: function () {
            // Chi dem so luong chatter thuc su lay theo thu tu tu server tra ve,
            // giup viec truy van lay lich su chat dung thuc hien dung
            return this.where({ isResultOfSearch: false }).length;
        },

        selectedTop: function () {
            if (this.length > 0) {
                this.models[this.length - 1].set({ isselected: true });
            }
        }
    });

    var ChatterNext = Backbone.Model.extend({
        defaults: {
            textloading: "Đang tải...",
            // 0: an, 1: Click de tai them, 2: Dang tai...
            state: 2
        },
        startLoading: function () {
            this.set({ state: 2, textloading: "Đang tải..." });
        },

        stopLoading: function () {
            // Bo thong bao nay de chuyen sang tinh luon tai danh sach message cao hon chieu cao trinh duyet
            this.set({ state: 1, textloading: "" }); // Click để tải thêm
        },

        hidden: function () {
            this.set({ state: 0, textloading: "" });            
        }
    });

    // Chatter
    // -------
    var ChatterSearch = Backbone.Model.extend({
        defaults: {
            jid: "", 			// chat.with
            label: "", 			// chat.with.split('@')[0]
            account: "",
            online: false,		// true neu online, false nguoc lai
            messagecount: 0,	// tong tin nhan da trao doi de set muc uu tien len dau khi tim kiem
            chatter: null,		// tro lai chatter goc neu co
            rosterUser: null,
            type: "",
            key: ""
        }
    });

    // List of Collections
    // -------------------
    var ChatterSearchList = Backbone.Collection.extend({
        model: ChatterSearch,
        comparator: 'messagecount'
    });

    // ChatterInfo
    // -------
    var ChatterInfo = Backbone.Model.extend({
        // model: AttachmentList
        attachments: null,
        defaults: {
            account: "[Chưa có thông tin]",
            mobile: "[Chưa có thông tin]",
            jobtitles: "[Chưa có thông tin]",
            departments: "[Chưa có thông tin]",
            online: false,
            statusmessage: "[Chưa có thông tin]",

            // Added DAMBV - 18/11/2016- Thêm trường members, danh sach thanh vien trong nhom.
            members: [],
            type: "" // "chat" hoac "groupchat"
        }
    });

    /* DamBV: Danh sach cac thanh vien trong nhom  */
    var MemberCollection = Backbone.Collection.extend({
        model: ChatterInfo,
    });

    /* DamBV: Danh sach cac thanh vien duoc goi y de them vao nhom */
    var FriendCollection = Backbone.Collection.extend({
        model: ChatterSearch,
    });

    var _chatters = new ChatterList;
    var _chatterNext = new ChatterNext;
    var _chatterSearch = new ChatterSearchList;

    // Export to btalk.model api
    btalk.model = $.extend(btalk.model, {
        Chatter: Chatter,
        ChatterSearch: ChatterSearch,
        ChatterInfo: ChatterInfo,
        MemberCollection: MemberCollection,
        FriendCollection: FriendCollection,

        //ChatterList: ChatterList,
        //ChatterNext: ChatterNext,
        //ChatterSearchList: ChatterSearchList
    });

    btalk = $.extend(btalk, {
        CHATTERS: _chatters,
        CHATTERNEXT: _chatterNext,
        CHATTERSEARCH: _chatterSearch,
    });
})();