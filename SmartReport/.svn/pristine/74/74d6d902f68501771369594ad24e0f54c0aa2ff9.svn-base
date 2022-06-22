(function () {
    'use strict';

    btalk.model = btalk.model || {};

    if (btalk.model.message === true) {
        return;
    }

    btalk.model.message = true;

    // CONVERSATIONDAY: Ngay co tin nhan
    var ConversationDay = Backbone.Model.extend({
        // members: la MemberList collection
        members: null,

        defaults: {
            'historyDay': ""
        },

        initialize: function () {
            this.members = new MemberList;
            this.members.on("reset", this.updateCounts);
        },

        updateCounts: function () {
        }
    });

    // CONVERSATIONDAYLIST: Danh sach cac ngay co tin nhan
    var ConversationDayList = Backbone.Collection.extend({
        model: ConversationDay,

        getDay: function (date) {
            var day = new Date();
            if (typeof date == 'object' && typeof date.getFullYear == 'function') {
                day = date;
            } else if (typeof date == 'string') {
                day = new Date(date);
            }
            var condition = day.getFullYear() + '/' + (day.getMonth() + 1) + '/' + day.getDate();
            return this.where({ historyDay: condition });
        },

        counts: function () {
            var count = 0;
            this.each(function (day) {
                day.members.each(function (user) {
                    count = count + user.messages.length;
                });
            });
            return count;
        },

        files: function () {
            var files = [];
            this.each(function (day) {
                day.members.each(function (user) {
                    user.messages.each(function (msg) {
                        if (msg.get('filename') && msg.get('filename').length > 0) {
                            files.push(msg);
                        }
                    });
                });
            });
            return files;
        },

        findMessage: function (id) {
            for (var i = 0; i < this.length; i++) {
                var day = this.models[i];
                for (var j = 0; j < day.members.length; j++) {
                    var messages = day.members.models[j].messages;
                    for (var k = 0; k < messages.length; k++) {
                        if (messages.models[k].get('id') == id) {
                            return messages.models[k];
                        }
                    }
                }
            }
        },

        readall: function () {
            for (var i = 0; i < this.length; i++) {
                var day = this.models[i];
                for (var j = 0; j < day.members.length; j++) {
                    var messages = day.members.models[j].messages;
                    for (var k = messages.length - 1; k >= 0; k--) {
                        if (messages.models[k].get('unread') == true) {
                            messages.models[k].set({ unread: false });
                        }
                    }
                }
            }
        },

        /*
            TamDN - 4/7/2017
            Cập nhật trạng thái các tin về ''
            Thực hiện khi cache báo trạng thái đã xem thay đổi
        */
        updateStatus: function(oldMsg, viewedMsgId) {
            var tmp = oldMsg;
            var viewedMsg = this.findMessage(viewedMsgId);
            while(tmp) {
                if (tmp.get('isme') == true && tmp.status != null) {
                    tmp.status('');
                    if(tmp.get('id') == viewedMsgId)
                        break;
                }
                tmp = tmp.newer;
            }
        },

        /*
            TamDN - 29/6/2017 - Sửa phần báo trạng thái đã xem
            Khi cache báo trạng thái đã xem thay đổi, cập nhật các model liên quan
        */
        updateModelsViewed: function(oldMsgId, msgId, byJid) {
            if(oldMsgId) {
                var oldMsg = this.findMessage(oldMsgId);
                if(oldMsg && typeof oldMsg.addViewedBy == "function") {
                    oldMsg.removeViewedBy(byJid);
                    this.updateStatus(oldMsg, msgId);
                }
            }
            if(msgId) {
                var viewedMsg = this.findMessage(msgId);
                if(viewedMsg && typeof viewedMsg.addViewedBy == "function")
                    viewedMsg.addViewedBy(byJid);
            }
        },
    });

    // MEMBER: Thanh vien
    // ------------------
    var Member = Backbone.Model.extend({
        // messages: la MessageList collection
        messages: null,

        defaults: {
            'account': "",
            'timestamp': "",
            // true khi la tin moi nhan.
            // Su dung de xac dinh cach ve len giao dien tu duoi len, phai nguoc voi tin lay tu lich su chat.
            append: false,
            online: false
        },

        initialize: function () {
            this.messages = new MessageList;
            this.messages.on("reset", this.updateCounts);
        },

        updateCounts: function () {
        }
    });

    // MEMBERLIST: Danh sach thanh vien
    var MemberList = Backbone.Collection.extend({
        model: Member
    });

    // MESSAGE: Tin nhan
    var Message = Backbone.Model.extend({
        /* Su dung de set trang thai viewed lan luot tren giao dien */
        // Tro ve message model cu hon lien sau message model hien tai`
        older: null,
        // Tro ve message model moi hon lien truoc message model hien tai
        newer: null,
        attachments: null,
        statusMap: {
            'client': 0,
            'server': 1,
            'success': 2,
            'viewed': 3,
            '': 4
        },
        defaults: {
            id: '',
            // Trang thai style cua tin tren giao dien
            // state: "one" | "first" | "last" | "". Trang thai phuc vu style giao dien.
            state: "",
            message: "",
            // Trang thai cua tin minh gui di
            // status: "client"-0 (chua len server) | "server"-1 (da len server) | "success"-2 (da toi dich) |
            //         "viewed"-3 (da xem, hien avatar nho cua user) | ""-4 (binh thuong)
            status: null,
            // private, su dung de so sanh giua cac trang thai
            _status: -1,
            // Trang thai tin minh nhan duoc: chua doc
            unread: false,
            // La tin nhan nhan duoc truc tiep khong qua lay lich su chat
            // Su dung xac dinh cach ve len giao dien va cach xu ly scroll
            append: false,

            // Gia tri co the: 1, 2, 3
            imageColumn: 0,
            imageCount: 0,
            otherCount: 0,
            receiverJid: "",
            senderJid: "",
            account: "",

            // Added DamBV-19/12/2016
            isMsgGroup: false,
            contentmsg_quote: null,
            sendermsg_quote: null,
            timemsg_quote: null,

            //TamDN - 29/6/2017 - Bo sung phan luu thong tin nhung nguoi da xem
            viewedBy: [],
        },

        addViewedBy: function(viewedByJid) {
            var viewedByList = _.clone(this.get('viewedBy'));
            viewedByList.push(viewedByJid);
            this.set({viewedBy: viewedByList});
        },

        removeViewedBy: function(viewedByJid) {
            var viewedByList = _.clone(this.get('viewedBy'));
            var index = viewedByList.indexOf(viewedByJid);
            viewedByList.splice(index, 1);
            this.set({viewedBy: viewedByList});
        },

        status: function (value) {
            if (value != null) {
                if (!this.statusMap[value]) return;

                if (this.get('_status') < this.statusMap[value]) {
                    this.set({ status: value, _status: this.statusMap[value] });
                } else {
                    console.log("WARNING: this.get('_status') < this.statusMap[value]: " + this.get('_status') < this.statusMap[value]);
                }
            } else {
                return this.get('status');
            }
        },

        findAttachment: function (id) {
            for (var i = 0; i < this.attachments.length; i++) {
                if (this.attachments.models[i].get('id') == id) {
                    return this.attachments.models[i];
                }
            }
        },

        initialize: function () {
            if (!arguments || arguments.length <= 0) {
                return;
            }
            var message = arguments[0];
            if (typeof message.imageCount === 'number' && typeof message.otherCount === 'number' &&
                    message.imageCount + message.otherCount > 0) {
                this.attachments = new AttachmentList;
                this.attachments.on("reset", this.updateCounts);
            }
        },

        updateCounts: function () {
        }       
    });

    // MESSAGELIST: Danh sach tin nhan
    var MessageList = Backbone.Collection.extend({
        model: Message
    });

    // ATTACHMENT: File gui kem
    var Attachment = Backbone.Model.extend({
        defaults: {
            name: "",
            type: "",
            // Quy uoc: -1: file gui loi, 101: file gui thanh cong. 0 -> 100: trang thai % dang gui.
            percentage: 100,
            url: "",
            id: "",
            statusText: "", 		// Uploading, Finalizing, Completed, Error
            status: 0,				// 200, 201 => Completed; other => Error
            from: "",
            to: "",
            // Tanent id cua user dang nhap tren OpenStack Swift
            tenantid: ""
        },
        percentage: function (value) {
            value = parseInt(value);
            // CuongNT - 14/04/2016: luon cho phep gan gia tri -1 vi day la trang thai bao gui loi
            if (value != null && (value == -1 || value > this.get("percentage"))) {
                this.set({ "percentage": value });
            } else {
                return this.get("percentage");
            }
        }
    });

    // Danh sach tin nhan
    var AttachmentList = Backbone.Collection.extend({
        model: Attachment
    });

    var MessageNext = Backbone.Model.extend({
        defaults: {
            textloading: "Đang tải...",
            /*
             * 0: an,
             * 1: Click de tai them.
             * 2: Dang tai... 
             */ 
            state: 2,
            currentChatter: null
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
        },

        currentChatter: function (value) {
            if (value instanceof Backbone.Model) {
                this.set({ 'currentChatter': value });
            } else {
                return this.get('currentChatter');
            }
        }
    });

    // Su dung de gan bang rosterUser.conversationDays
    var _conversationDays = new ConversationDayList;

    // Export to btalk.view api
    btalk.model = $.extend(btalk.model, {
        ConversationDay: ConversationDay,
        Member: Member,
        MemberList: MemberList,
        Message: Message,
        MessageList: MessageList,
        Attachment: Attachment,
        AttachmentList: AttachmentList,
        MessageNext: MessageNext
    });

    btalk = $.extend(btalk, {
        // User dang login hien tai
        CONVERSATIONDAYS: _conversationDays
    });

})();