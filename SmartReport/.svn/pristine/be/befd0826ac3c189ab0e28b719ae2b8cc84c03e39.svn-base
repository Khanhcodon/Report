// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

define(['text!chatterT',
                'text!chattersearchT',
                'text!chatternextT',
                'text!chatterinfoT',
                'text!detailmembergroupchatT',
                'text!viewFriendT'
], function (chatterT, chattersearchT, chatternextT, chatterinfoT, detailmembergroupchatT, viewFriendT) {
    'use strict';

    btalk.view = btalk.view || {};
    if (btalk.view.chatter === true) {
        return;
    }

    var _parent;
    if (_parent && _parent.notify && typeof _parent.notify.requestPermission === 'function') {
        _parent.notify.requestPermission();
    }

    btalk.view.chatter = true;

    // Bien xu ly single, double click
    var delay = 300,
        clicks = 0,
        timer = null,
        firstclick = true;

    var ChatterView = Backbone.View.extend({
        tagName: "li",
        className: "list-group-item",
        $messageTxt: null,
        events: {
            'click': 'chatterClick',
            "click .bt-slt-command": "commandClick"
        },

        initialize: function () {
            // TODO: Tam thoi xu ly lay message theo id control tai day. Sau can sua cach lay khac cho de maintain hon.
            this.$messageTxt = $("#messageTxt");
            this.listenTo(this.model, 'change:isselected', this.changeSelected);
            this.listenTo(this.model, 'change:unread change:unreadcount', this.changeUnread);
            this.listenTo(this.model, 'change:lastmessage change:isme', this.changeLastmessage);
            this.listenTo(this.model, 'change:lasttimestamp', this.changeLasttimestamp);
            this.listenTo(this.model, 'change:isAtTop', this.changeIsAtTop);
            this.listenTo(this.model, 'change:online', this.changeOnline);
            this.listenTo(this.model, 'change:statusIdle', this.changeIdleStatus);
            this.listenTo(this.model, 'change:account', this.changeAccount);
            this.listenTo(this.model, 'destroy', this.remove);
            this.listenTo(this.model, 'change:fullname', this.changeFullnameGroup);
            this.listenTo(this.model, 'change:statusHeightChatter', this.changeHeightChatter);
            this.listenTo(this.model, 'change:isSeenLastMessage', this.changeStatusSeenChatter);
            this.listenTo(this.model, 'change:muteNotification', this.changeMuteNotification);
        },

        render: function () {
            this.$el.html($.tmpl(chatterT, this.model.toJSON()));

            var _lastmessage = btalk.emoticon.process(this.model.get('lastmessage'));
            this.$el.find('.lastmessage').html(_lastmessage);

            var _timestampText;
            var _lasttimestamp = this.model.get('lasttimestamp');

            if (typeof _lasttimestamp === "string") {
                var lastDate = new Date(parseInt(_lasttimestamp));
                _timestampText = btalk.getCoolTime(lastDate);
            } else {
                _timestampText = btalk.getCoolTime(_lasttimestamp);
            }
            this.$el.find('.chat-history-row-timestamp').text(_timestampText);

            if (this.model.get('type') == btalk.CHATTYPE.CHAT) {
                // user
                btalk.view.setAvatar([this.model.get("account")], this.$el.find("img.user"));
                if (this.model.get('isSeenLastMessage') == true) {
                    btalk.view.setAvatar([this.model.get("account")], this.$el.find(".avatarseen >img"));
                }
            } else {
                // groupchat
                this.$el.find("img.user").attr('src', './themes/default/images/group3.png');
            }

            var status = this.model.get('statusHeightChatter');
            if (status == "small") {
                this.$el.addClass('small-chatter');
            } else {
                this.$el.removeClass('small-chatter');
            }

            // Dung khi can moveToBottom
            this.model.view = this;
            return this;
        },

        changeSelected: function () {
            // Trang thai click chon
            if (this.model.get("isselected") == true) {
                // Selected chatter vua click
                this.$el.addClass("selected");

                // Danh dau luon la da doc
                this.model.read(true);

                // Doi current chatter
                this.shareEvents.trigger("changeCurrentChatter", this.model);

                // autoScroll toi vi tri selected
                var $scroll = $("#tab_history");
                $scroll.scrollVisible("ul:first-child>li.selected");
            } else {
                this.$el.removeClass("selected");
            }
            return this;
        },

        changeUnread: function () {
            // Neu vua thay doi trang thai thanh chua doc thi dua len top

            if (this.model.get('unread') && !this.model.previous('unread')) {
                this.shareEvents.trigger("moveChatterToTop", this.$el);
                this.$el.find('.chat-history-row').addClass('unread');
            } else if (!this.model.get('unread') && this.model.previous('unread')) {
                this.$el.find('.chat-history-row').removeClass('unread');
            }
            // So luong tin chua doc
            if (this.model.get('unreadcount') > 0) {
                this.$el.find('.unreadcount').text('(' + this.model.get('unreadcount') + ')');
            } else {
                this.$el.find('.unreadcount').text('');
            }
            return this;
        },

        changeLastmessage: function () {
            var _lastmessage = btalk.emoticon.process(this.model.get('lastmessage'));

            if (this.model.get('isme') == true && _lastmessage.indexOf("Bạn: ") < 0) {
                this.$el.find('.lastmessage').html("Bạn: " + _lastmessage);
            } else {
                this.$el.find('.lastmessage').html(_lastmessage);
            }
            return this;
        },

        changeLasttimestamp: function () {
            var timestampText = btalk.getCoolTime2(this.model.get('lasttimestamp'));
            this.$el.find('.chat-history-row-timestamp').text(timestampText);
            return this;
        },

        changeIsAtTop: function () {
            if (this.model.get('isAtTop') && !this.model.previous('isAtTop')) {
                this.shareEvents.trigger("moveChatterToTop", this.$el);
            }
        },

        changeOnline: function () {
            if (this.model.get('online') == true) {
                this.$el.find('.chat-history-row').addClass("online");
            } else {
                this.$el.find('.chat-history-row').removeClass("online");
            }
        },

        // Added DamBV 17/04/2018 : Thuc hien hien thi idle.
        changeIdleStatus: function () {
            if (this.model.get('statusIdle') == true) {
                this.$el.find('.chat-history-row').addClass("idle");
            } else {
                this.$el.find('.chat-history-row').removeClass("idle");
            }
        },

        /*
         * DamBV - 02/12/2016 - Thực hiện thay đổi tên nhóm chat.
         */
        changeFullnameGroup: function () {
            var name = this.model.get('fullname');
            this.$el.find('.chat-history-row-fullname').text(name);
        },

        /*
         * Click de xem chi tiet lich su hoi thoai
         */
        chatterClick: function (e) {
            // Neu la click thu n la tren doi tuong khac thi reset
            if (this.model.get('isselected') == true) {
                // Xu li rieng voi ban mobile.
                if (btalk.isMobile) {
                    btalk.APPVIEW.CURRENTCHATTER.set({ isselected: false });
                    clicks = 1;
                    firstclick = true;
                } else {
                    clicks++;
                    firstclick = false;
                }
            } else {
                clicks = 1;
                firstclick = true;
            }

            var chatterid = this.model.get('jid');

            // DamBV bo dung thong tin cho egov.
            if (_parent && typeof _parent.egovReadChat === 'function') {
                _parent.egovReadChat(chatterid);
            }

            // Single click
            if (clicks == 1) {
                clearTimeout(timer);
                timer = setTimeout(function () {
                    if (firstclick) {
                        this.userClicked();
                    }
                    clicks = 0; 		// after action performed, reset counter
                }.bind(this), delay);

                // Hien thi noi dung quote neu co.
                this.checkExistMessageReply();

                // kiem tra xem co can dowload cac file da chia se neu can.
                this.canDowloadFileInConversation(chatterid);
            }
        },

        userClicked: function () {
            this.selectUser();
        },

        // Added DamBV 02/07/2017 : hien thi phan quote neu co.
        checkExistMessageReply: function () {
            if (this.model.temporaryQuote && this.model.temporaryQuote != null) {
                this.model.temporaryQuote.showQuote();
            } else {
                $('#input_replymsg').html('');
            }
        },

        // Added DamBV : 2/07/2017: kiem tra xem co phai tai file ve khong.
        canDowloadFileInConversation: function (Idchatter) {
            var _isRefreshShareFile = this.model.get(btalk.IS_REFRESH_SHARE_FILE);
            if (_isRefreshShareFile && _isRefreshShareFile == true) {
                var type = this.model.get('type');

                btalk.cm.getMessageFileInConversation(Idchatter, btalk.FILETYPE.IMAGE, null, type);
                btalk.cm.getMessageFileInConversation(Idchatter, btalk.FILETYPE.FILE, null, type);
                this.model.set(btalk.IS_REFRESH_SHARE_FILE, null);
            }
        },

        /*
         * Xu ly style selected khi click xem chi tiet lich su chat
         */
        selectUser: function () {
            this.model.set({ isselected: true });
        },

        commandClick: function (e) {
            var command = $(e.currentTarget).attr('data-command');
            e.stopPropagation();
            switch (command) {
                case "delete":
                    // Thêm trường type chatter.
                    var that = this;
                    $('<div></div>').html('<div><h5>Bạn muốn xóa lịch sử hội thoại này?</h5></div>')
                      .dialog({
                          modal: true,
                          title: 'Xóa lịch sử',
                          zIndex: 10000,
                          autoOpen: true,
                          height: "auto",
                          width: 400,
                          resizable: false,
                          show: {
                              effect: "drop",
                          },
                          hide: {
                              effect: "drop",
                              duration: 500
                          },
                          buttons: {
                              "Đồng ý": function () {
                                  btalk.cm.removeCollection(that.model.get("jid"), that.model.get('type'));
                                  $(this).dialog("close");
                              },
                              "Hủy": function () {
                                  $(this).dialog("close");
                              }
                          },
                          close: function (event, ui) {
                              $(this).remove();
                          }
                      });
                    return false;
            }
        },

        /*
         * DamBV - 14/02/2017 : thay doi kich thuoc item trong danh sach cuoc hoi thoai.
         */
        changeHeightChatter: function () {
            var status = this.model.get('statusHeightChatter');
            if (status == "small") {
                this.$el.addClass('small-chatter');
            } else {
                this.$el.removeClass('small-chatter');
            }
        },

        /*
         * DamBV - 14/02/2017 : hien thi avatar trong item trong danh sach cuoc hoi thoai.
         */
        changeStatusSeenChatter: function () {
            if (this.model.get('isSeenLastMessage') == true) {
                this.$el.find(".avatarseen > img ").show();
                btalk.view.setAvatar([this.model.get("account")], this.$el.find(".avatarseen >img"));
            } else {
                this.$el.find(".avatarseen > img").hide();
            }
        },

        changeMuteNotification: function () {
            var _muteNotification = this.model.get('muteNotification');
            if (_muteNotification) {
                this.$el.find('.chat-turn-off-notification').show();
            }
            else if (!_muteNotification) {
                this.$el.find('.chat-turn-off-notification').hide();
            }
        }
    });

    var ChatterNextView = Backbone.View.extend({
        handTimeout: null,
        events: {
            'click #collection_next': 'loadNextPageOfChatters'
        },

        initialize: function () {
            this.listenTo(this.model, 'change', this.change);
        },

        render: function () {
            this.$el.html($.tmpl(chatternextT, this.model.toJSON()));
            this.handleTimeout();
            return this;
        },

        change: function () {
            this.$el.find('img')[0].style.visibility = this.model.get('state') == 2 ? null : 'hidden';
            this.$el.find('span')[0].innerHTML = this.model.get('textloading');
            this.handleTimeout();
            return this;
        },

        loadNextPageOfChatters: function (e) {
            // Chuyen trang thai sang loading
            this.model.startLoading();
            // Bat dau loading du lieu tu server
            // [TODO] Dua tren cache da luu de count ra danh sach chatter co tin moi chua doc de biet so luong lay mac dinh tu server ve.
            btalk.cm.getNextPageOfChatters(btalk.CHATTERS.counts(), btalk.CHATTERNEXTCOUNTS);
        },

        handleTimeout: function () {
            this.handTimeout = setTimeout(function () {
                this.setTimeoutLoading();
            }.bind(this), 6000);
        },

        setTimeoutLoading: function () {
            if (this.handTimeout) {
                var status = this.model.get('state');
                if (status === 2) {
                    btalk.CHATTERNEXT.hidden();
                }
                clearTimeout(this.handTimeout);
                this.handTimeout = null;
            }
        }
    });

    var ChatterSearchView = Backbone.View.extend({
        tagName: "li",
        className: "list-group-item",
        events: {
            'click': 'chatterSearchClick'
        },

        initialize: function () {
            this.listenTo(this.model, 'destroy', this.remove);
        },

        render: function () {
            this.$el.html($.tmpl(chattersearchT, this.model.toJSON()));

            this.$el.attr('data-field', 'chatter');

            if (this.model.get('type') == 'newgroupchat') {
                this.$el.find("span[data-field='key']").html('');

                var chatters = this.model.get('key');
                for (var i = 0; i < chatters.length; i++) {
                    var imgSrc = btalk.view.getAvatar([chatters[i].name.trim()]);

                    this.$el.find("span[data-field='key']")
                    .append("<div class='chat-history-row-avatar'><img height='32' class='img-circle search' src='" + imgSrc + "' title='" + chatters[i].name.trim() + "'/><div class='chat-history-status-account icon-bolt'></div></div>");
                }
            }

            if (this.model.get('type') == btalk.CHATTYPE.CHAT) {
                btalk.view.setAvatar([this.model.get("account")], this.$el.find("img.user"));
            } else {
                // groupchat
                this.$el.find("img.user")
                .attr('src', './themes/default/images/group3.png');
            }
            return this;
        },

        chatterSearchClick: function (e) {
            // Neu la click thu n la tren doi tuong khac thi reset
            if (this.model.get('isselected') == true) {
                clicks++;
                firstclick = false;
            } else {
                clicks = 1;
                firstclick = true;
            }

            // Single click
            if (clicks === 1) {
                clearTimeout(timer);
                timer = setTimeout(function () {
                    if (firstclick) {
                        this.userClicked();
                    }
                    clicks = 0; 		// after action performed, reset counter
                }.bind(this), delay);
            } else {
                clicks = 0;
            }
        },

        userClicked: function () {
            // Tim lai chatter gan voi ket qua tim kiem. neu co thi kich hoat selected
            if (this.model.get('chatter')) {
                this.model.get('chatter').set({ isselected: true });
                return;
            }

            // Thuc hien tao cuoc hoi thoai moi.
            if (this.model.get('type') == "newgroupchat") {
                this.createGroupConversation();
            } else {
                this.createUserConversation();
            }
        },

        // Xu li tao nhom chat khi tim kiem.
        createGroupConversation: function () {
            var rosterUser, newchatter;
            rosterUser = this.model.get('rosterUser');
            // Gui lenh tao nhom (gui presence)
            // TODO: Cho nay de tam vay de test nhanh
            var accounts = this.model.get('key');
            var members = [];
            accounts.forEach(function (acc) {
                members.push(acc.jid);
            }, this);

            // Xu li truong hop chat 1-1 va chat voi chinh minh.
            if (members.length < 3) {
                // TH: chat với chính mình
                if (members.length == 1) {
                    return false;
                } else {
                    // TH : chat voi nguoi khac thi thanh chat 1-1
                    var chatter = members[1];
                    chatter = chatter.substring(0, chatter.indexOf('@'));
                    btalk.APPVIEW._chatWithAccount(chatter);
                    return false;
                }
            }

            if (!rosterUser) {
                rosterUser = btalk.ROSTER.addUser(this.model.get("jid"));
                this.model.set("rosterUser", rosterUser);
            }

            // [TODO] xem chuyen viec nay ra thuc hien tren appView	            
            newchatter = new btalk.model.Chatter({
                jid: this.model.get('jid'), 				// chat.with
                account: this.model.get('account'), 		// chat.with.split('@')[0]
                type: btalk.CHATTYPE.GROUPCHAT,							// chat, groupchat
                // Tin nhan cuoi cung
                lasttimestamp: new Date(),	// start.setSeconds(start.getSeconds() + secs);
                lastmessage: '[Bạn bắt đầu chat với mọi người]',	// Noi dung tin nhan cuoi
                isme: false,								// Neu la tin nhan cua minh gui di
                // Tin nhan chua doc (tin moi, tin offfline)
                unread: false,								// Trang thai co tin nhan moi chua xem
                unreadcount: 0,								// So luong tin nhan moi chua xem
                // true khi duoc click chon tren giao dien
                isselected: false,
                fullname: 'New group chat',								// account || fullname
                rosterUser: this.model.get('rosterUser'),	// Tro ve doi tuong RosterUser tuong ung
                roster: btalk.ROSTER,						// Tro ve doi tuong Roster: btalk.ROSTER
                isResultOfSearch: true,
                statusHeightChatter: btalk.config.statuschatter,
            });

            // DamBV - them thanh vien cho roter trong truong hop la nhom chat
            rosterUser.members = members;

            // Gan lai chatter cho rosterUser
            rosterUser.chatterM = newchatter;

            btalk.CHATTERS.add(newchatter);
            newchatter.set({ isselected: true, unread: true });
            btalk.cm.createGroup(this.model.get('jid'), members);
        },

        // Tao nhom chat voi user khi tim kiem.
        createUserConversation: function () {
            var rosterUser = this.model.get('rosterUser');
            /*
             * Added by TamDN
             */
            var fullname = null;
            if (rosterUser.type == btalk.CHATTYPE.GROUPCHAT) {
                fullname = rosterUser.fullname;
                if (rosterUser.chatterM)
                    fullname = rosterUser.chatterM.get("fullname");
                if (fullname == null || fullname == "")
                    fullname = rosterUser.fullname;
            }

            // Added DamBV - khi them cuoc hoi thoai moi trong ban mobile
            if (btalk.isMobile && rosterUser.type === btalk.CHATTYPE.CHAT) {
                fullname = rosterUser.fullname;
            }

            var newchatter = new btalk.model.Chatter({
                jid: this.model.get('jid'), 				// chat.with
                account: this.model.get('account'), 		// chat.with.split('@')[0]
                type: rosterUser.type,								// chat, groupchat

                // Tin nhan cuoi cung
                lasttimestamp: new Date(),	// start.setSeconds(start.getSeconds() + secs);
                lastmessage: '[Bạn bắt đầu chat với ' + this.model.get('account') + ']',	// Noi dung tin nhan cuoi
                isme: false,								// Neu la tin nhan cua minh gui di

                // Tin nhan chua doc (tin moi, tin offfline)
                unread: false,		// Trang thai co tin nhan moi chua xem
                unreadcount: 0,		// So luong tin nhan moi chua xem

                // true khi duoc click chon tren giao dien
                isselected: false,
                fullname: fullname,		// account || fullname

                rosterUser: rosterUser,	// Tro ve doi tuong RosterUser tuong ung
                roster: btalk.ROSTER,		// Tro ve doi tuong Roster: btalk.ROSTER
                isResultOfSearch: true,
                online: rosterUser.status == 'available',
                statusHeightChatter: btalk.config.statuschatter,
            });
            btalk.CHATTERS.add(newchatter);
            newchatter.set({ isselected: true, unread: true });

            // Gan lai chatter cho rosterUser
            rosterUser.chatterM = newchatter;
        },
    });

    /* View quan li thanh vien trong nhom */
    var MemberGroupView = Backbone.View.extend({
        tagName: "li",
        template: detailmembergroupchatT,
        events: {
            'click div.chat-history-row-avatar': 'showDetail',
            'click .chat-history-row': 'chatWithSelecter',
            'click #deleteMember': 'deleteMember',
        },
        initialize: function () {
            this.listenTo(this.model, 'destroy', this.clear);
            this.listenTo(this.model, 'change', this.render);
        },

        render: function () {
            this.$el.html($.tmpl(this.template, this.model.toJSON()));
            this.$el = this.$el.children();
            this.setElement(this.$el);
            return this;
        },

        showDetail: function (e) {
            var this_li = $(e.target).parents('li');
            if (this_li.attr('id') === "add_menber") {
                alert('Add menber in group');
            } else {
                this.showProfile(this_li);
            }
        },

        deleteMember: function (e) {
            var jid = this.model.get('account');
            var accountDel = this.model.get('account');
            var group = btalk.APPVIEW.CURRENTCHATTER.get('jid');

            //Gui tin nhan thong bao
            var configs = [{ "key": "edit_by", "value": btalk.ROSTER.currentuser.jid },
                           { "key": "remove_member", "value": accountDel }];
            btalk.cm.sendConfigs(configs, group, btalk.CHATTYPE.GROUPCHAT,
            btalk.APPVIEW.printAndSaveMessage.bind(btalk.APPVIEW, btalk.APPVIEW.CURRENTCHATTER));

            //Thuc hien xoa tai khoan khoi nhom chat
            btalk.cm.removeGroupMember(group, accountDel);
            //btalk.cm.getGroupMembers(group);

            this.model.destroy();
            e.stopPropagation();
        },

        clear: function () {
            this.remove();
        },

        showProfile: function (li) {
            if (btalk.isMobile === false) {
                var child_detailProfile = li.find('div.detail-profile-member');
                child_detailProfile.slideToggle();
            }
        },

        chatWithSelecter: function (e) {
            if (btalk.isMobile === false) {
                var objectEvent = e.target;

                if (objectEvent.className.indexOf('img-circle user') < 0 &&
                    objectEvent.className.indexOf('button-option') < 0) {
                    var jid = this.model.get('account');
                    var accountCurrent = btalk.ROSTER.currentuser.jid;
                    if (jid == accountCurrent) {
                        return false;
                    }
                    var account = jid.substring(0, jid.indexOf('@'));
                    btalk.APPVIEW._chatWithAccount(account);
                    e.stopPropagation();
                }
            }
        }
    });


    /* change detail view chat group, addtional click for change chat group name */
    var ChatterInfoView = Backbone.View.extend({
        collectionMember: null,         // bien luu danh sach thanh vien trong nhom.	        
        template: chatterinfoT,         // bien luu mau html cho thong tin chatter.

        SHARE_IMAGE: "collectionShareImage",
        SHARE_FILES: "collectionShareFile",

        currentChatter: null,

        initialize: function () {
            this.currentChatter = btalk.APPVIEW.CURRENTCHATTER;

            this.collectionMember = new btalk.model.MemberCollection();

            this.collectionSuggestFriend = new btalk.model.FriendCollection();

            this.listenTo(this.collectionSuggestFriend, "add", this.showFriends);

            this.listenTo(this.collectionMember, "add", this.showViewMemberInGroup);

            /** TamDN - 19/7/2017
            * Gọi hàm vẽ lại số thành viên khi collectionMember được add/remove/reset
            */
            this.listenTo(this.collectionMember, "add", this.printNumberMemberGroup);
            this.listenTo(this.collectionMember, "remove", this.printNumberMemberGroup);
            this.listenTo(this.collectionMember, "reset", this.printNumberMemberGroup);

            this.listenTo(this.model, 'change:online', this.changeOnline);

            this.listenTo(this.model, 'change:fullname', this.startRenameGroup);

            this.registerhandler();

            this.eventExitStatusRenameGroup();
        },

        events: {
            'click #name-group': 'showCompomentRenameGroup',

            'keypress div.textarea-rename': 'finishRenameGroup',

            'click #done-rename-btn': 'sentRequestRenameGroup',

            'click #add-people': 'visibleFormAddMembers',

            'click #sharedimage img': 'previewSharedImage',

            'click #button_sysn': 'actionDowloadMoreFile',

            //menber
            'click #managerMemberGroup': 'displayLocationMemberGroup',

            // file
            'click #managerShareFile': 'displayLocationShareFile',
        },

        registerhandler: function () {
            // lay thong tin ban nguoi trong nhom
            btalk.cm.registerHandler('_handle_getMembersGroup', this._controlPackageResponseIq.bind(this));

            // them nhieu thanh vien vao nhom
            btalk.cm.registerHandler('_handle_addMembersGroup', this._controlPackageResponseIq.bind(this));

            // xoa 1 thanh vien
            btalk.cm.registerHandler('_handle_removeMemberGroup', this._controlPackageResponseIq.bind(this));

            btalk.cm.registerHandler('_handle_oMsg_configMemberGroup', this._controlPackageResponseMsg.bind(this));

            // DamBV 17/02/2017
            btalk.cm.registerHandler('handle_iq_retrieve_result_file', this._controlPackageResponseIq.bind(this));
        },

        _controlPackageResponseIq: function (iq) {
            var _idPackage = null;
            if (iq.doc) {
                _idPackage = iq.doc.children[0].id;
            }

            if (_idPackage == null) {
                return false;
            }

            // Lay thong tin thanh vien
            if (_idPackage.indexOf('get_member') != -1) {
                if (!btalk.ChatBmail) {
                    this.handleGetGroupMembers(iq);
                }
                return;
            }

            // Lay thong tin thay doi thanh vien
            if (_idPackage.indexOf('add_member') != -1 || _idPackage.indexOf('remove_member') != -1) {
                var typeIQ = iq.getType();
                if (typeIQ === "error") {
                    if (btalk.ChatBmail) {
                        btalk.ChatBmail.showNotifyDisconnect = true;
                        window.showNotifyDisconnect();
                    }
                } else if (typeIQ != "error") {
                    console.log("Thay doi thanh vien thanh cong");
                    btalk.cm.getGroupMembers(this.model.get('fulljid'));
                    if (btalk.ChatBmail) {
                        btalk.ChatBmail.renderAvatarCustomerService();
                    }
                }
                return;
            }

            // Lay thong tin cac file da chia se.
            if (_idPackage.indexOf("get_file_shared") != -1) {
                this._handleDataSharedFile(iq);
                return;
            }
        },

        /** Xử lý khi nhận được gói tin thêm/bớt thành viên trong nhóm*/
        _controlPackageResponseMsg: function (msg) {
            var _type = msg.type;
            var _from = msg.from;
            _from = _from.substring(0, _from.indexOf('/'));
            var _accountmodel = this.model.get('account');

            if (_type != btalk.CHATTYPE.GROUPCHAT) {
                return false;
            }
            var newMembers, removeMember;

            // Lấy thông tin thành viên được thêm/bớt
            if (msg.config && msg.config.forEach) {
                msg.config.forEach(function (item) {
                    if (item.new_members)
                        newMembers = item.new_members;
                    if (item.remove_member)
                        removeMember = item.remove_member;
                });
            }

            //Lấy thông tin từ cache chứa thông tin member của 1 groupchat (lưu khi chuyển qua lại giữa các chatter
            var _list = btalk.ROSTER.getUserByJID(_from).members;

            // Neu la them thanh vien vao nhom.
            if (newMembers) {
                var newListMember = newMembers.split(';');

                for (var i = 0; i < newListMember.length; i++) {
                    var newJid = newListMember[i].trim();
                    if ($.inArray(newJid, _list) == -1) {
                        //Nếu là chatter hiện tại thì mới cập nhật
                        if (_from == _accountmodel) {
                            //Nếu chính mình được thêm thì tải lại danh sách thành viên
                            if (btalk.auth.getJID() == newJid)
                                this.loadGroupMembers();
                            else {
                                var newMemberChatterInfoModel = this.initChatterInfoModelFromJid(newJid);
                                this.collectionMember.add(newMemberChatterInfoModel);
                            }
                        }
                        //Thêm vào để update vào cache
                        _list.push(newJid);
                    }
                }
            }

            // Xoa thanh vien trong nhom.
            if (removeMember) {
                // Xoa trong Collection.
                this.collectionMember.models.forEach(function (item) {
                    if (item.get('account') == removeMember) {
                        item.destroy();
                    }
                });
                var index = _list.indexOf(removeMember);
                if (index > -1) {
                    _list.splice(index, 1);
                }
            }
            //Cập nhật ngược lại vào cache
            btalk.ROSTER.getUserByJID(_from).members = _list;
        },

        render: function () {
            // DamBV - 17/11/2016 - kiem tra co phai la nhom chat khong?
            $('.btalk-textinput').show();
            var m_type = this.model.get("type");
            if (m_type === btalk.CHATTYPE.GROUPCHAT) {
                var _member = this.model.get('member');
                if (_member && _member.length > 0) {
                    this.showViewAllMemberInGroup(_member);
                }
                this.$el.html($.tmpl(this.template, this.model.toJSON()));
            } else {
                // chat 1-1
                this.$el.html($.tmpl(this.template, this.model.toJSON()));
                btalk.view.setAvatar([this.model.get('account')], this.$el.find("img[data-field='avatarurl']"));
            }

            this.$el.find('.body-member').hide();
            this.$el.find('.body-sharefile').hide();
            return this;
        },

        /*
         * Thay doi ten nhom
         */
        startRenameGroup: function () {
            this.$el.find('.btalk-body-profile .chat-history-row-body .chat-history-row-fullname')
                .text(this.model.get('fullname'));
        },

        showCompomentRenameGroup: function (e) {
            var nameGroup = $(this.$el.find('#name-group'));
            var nameModify = $(this.$el.find('#textarea-rename-id'));
            var buttonDone = $(this.$el.find('#done-rename-btn'));
            nameGroup.hide();
            nameModify.show();
            this.placeCaretAtEnd($('#textarea-rename-id').get(0));
            buttonDone.show();
        },

        // Gui goi tin thay doi ten nhom.
        sentRequestRenameGroup: function (event) {
            //Hoan thanh doi ten
            if (typeof event !== "undefined") {
                var buttonDone = $(this.$el.find('#done-rename-btn'));
                var nameGroup = $(this.$el.find('#name-group'));
                var nameModify = $(this.$el.find('#textarea-rename-id'));
                var oldName = nameGroup.text();
                var newName = nameModify.text();
                if (newName !== oldName && this.checkSpecialCharacter(newName)) {
                    var account = this.model.get('account');
                    btalk.cm.renameGroup(account, newName);
                    // Added TamDN
                    var configs = [{ "key": "edit_by", "value": btalk.ROSTER.currentuser.jid },
                                   { "key": "roomconfig_roomname", "value": newName }];

                    btalk.cm.sendConfigs(configs, this.model.get('account'), btalk.CHATTYPE.GROUPCHAT,
                            btalk.APPVIEW.printAndSaveMessage.bind(btalk.APPVIEW, this.currentChatter));
                    console.log('Đã gửi request rename group');
                    nameGroup.text(newName);
                }
                nameGroup.show();
                nameModify.hide();
                buttonDone.hide();
            }
        },

        // Kiem tra cac ki tu dac biet khi thay doi ten nhom.
        checkSpecialCharacter: function (str) {
            var rs = !/[~`!#$%\*+=\\[\]\\';,/{}|\\"<>\?]/g.test(str);
            if (rs == false) {
                btalk.APPVIEW.showNotifyFormText("Tên nhóm không được phép chứa kí tự đặc biệt!")
            }
            return rs;
        },

        // focus vao vi tri cuoi cung khi thay ten.
        placeCaretAtEnd: function (el) {
            el.focus();
            if (typeof window.getSelection != "undefined"
                    && typeof document.createRange != "undefined") {
                var range = document.createRange();
                range.selectNodeContents(el);
                range.collapse(false);
                var sel = window.getSelection();
                sel.removeAllRanges();
                sel.addRange(range);
            } else if (typeof document.body.createTextRange != "undefined") {
                var textRange = document.body.createTextRange();
                textRange.moveToElementText(el);
                textRange.collapse(false);
                textRange.select();
            }
        },

        finishRenameGroup: function (event) {
            if (event.which == 13) {
                this.sentRequestRenameGroup(event);
            }
        },

        // Khi dang doi ten nhom, chi can click bat ki vi tri nao cung ket thuc.
        eventExitStatusRenameGroup: function () {
            var that = this;
            $('body').click(function (e) {
                if (e.target.id !== "textarea-rename-id" && e.target.id !== "name-group") {
                    var nameGroup = $(that.$el.find('#name-group'));
                    var nameModify = $(that.$el.find('#textarea-rename-id'));
                    var buttonDone = $(that.$el.find('#done-rename-btn'));
                    nameModify.text(nameGroup.text());
                    nameGroup.show();
                    nameModify.hide();
                    buttonDone.hide();
                }
            });
        },

        displayLocationMemberGroup: function () {
            var valDisplay = this.$el.find('.body-member').css('display');
            this.$el.find('.body-member').slideToggle(1000);
            if (valDisplay == "none") {
                // an thi hien
                this.$el.find('.head-member .button-status').removeClass('active');
                this.$el.find('#show_member').addClass('active');
            } else {
                // dang hien thi an
                this.$el.find('.head-member .button-status').removeClass('active');
                this.$el.find('#hide_member').addClass('active');
            }
        },

        /** TamDN - 19/7/2017
        * Vẽ lại số lượng thành viên trong nhóm khi có thay đổi
        */
        printNumberMemberGroup: function () {
            if (this.collectionMember != null) {
                if (this.collectionMember.length > 0)
                    this.$el.find('#numbermembergroup').text(this.collectionMember.length + " thành viên");
                else
                    this.$el.find('#numbermembergroup').text("Bạn không còn là thành viên của nhóm này");
            }
        },

        // DamBV - 17/11/2016: La model thanh vien trong nhom.
        showViewMemberInGroup: function (member) {
            var _viewMember = new MemberGroupView({ model: member });
            $('.btalk-body-detail .btalk-body-detail-member li.item-add-people').after(_viewMember.render().el);
        },

        /** Xử lý gói tin lấy danh sách thành viên trong nhóm*/
        handleGetGroupMembers: function (iq) {
            var currentAccount = this.model.get('fulljid');
            var fromAccount;
            var m_attributes = iq.doc.children[0].attributes;

            for (var key in m_attributes) {
                if (m_attributes[key].name == 'from') {
                    fromAccount = m_attributes[key].value;
                }
            }

            // Xac nhan gui goi tin tra ve dung nhom gui len.
            if (currentAccount === fromAccount) {
                // Bien listNewMember luu thong tin thanh vien tu server gui ve.
                var groupMembers = [];
                if (iq.getQuery() && iq.getQuery().childNodes.length > 0) {
                    var inGroup = false;
                    for (var i = 0; i < iq.getQuery().childNodes.length; i++) {
                        var item = iq.getQuery().childNodes.item(i);
                        var itemJid = item.getAttribute('jid');
                        groupMembers.push(itemJid);
                    }
                }
                this.updateGroupMembers(groupMembers);
            }
        },

        /** TamDN - 20/7/2017
        * Tải thành viên trong nhóm, đọc trong cache trước,
        * nếu chưa có mới gửi request lên server
        */
        loadGroupMembers: function () {
            if (this.model.get('type') === btalk.CHATTYPE.GROUPCHAT) {
                var currentChatterJid = this.model.get('fulljid');

                /** Đoạn này để lại nếu dùng cache
                //Lấy từ cache
                var groupMembers = btalk.ROSTER.getUserByJID(currentChatterJid).members;
                if (!groupMembers || groupMembers.length < 1) {
                    btalk.cm.getGroupMembers(currentChatterJid);
                } else {
                    this.updateGroupMembers(groupMembers);
                }
                */

                //TODO Hiện tại chưa có nhu cầu cache nên request lên server
                btalk.cm.getGroupMembers(currentChatterJid);
            }
        },

        /** TamDN - 20/7/2017
        * Cập nhật thành viên trong group, add vào collectionMember
        */
        updateGroupMembers: function (groupMembers) {
            btalk.ROSTER.getUserByJID(this.model.get('fulljid')).members = groupMembers;
            var oldMembers = this.collectionMember.models.map(function (a) {
                return a.attributes.account;
            });
            for (var i = 0; i < groupMembers.length; i++) {
                var itemJid = groupMembers[i];
                //Nếu danh sách cũ chưa có thì mới thêm vào
                if ($.inArray(itemJid, oldMembers) == -1) {
                    var memberChatterInfoModel = this.initChatterInfoModelFromJid(itemJid);
                    this.collectionMember.add(memberChatterInfoModel);
                }
            }
            if ($.inArray(btalk.auth.getJID(), groupMembers) == -1) {
                $('.btalk-textinput').hide();
                this.printNumberMemberGroup();
            }
            else {
                $('.btalk-textinput').show();
            }
        },

        /** TamDN - 19/7/2017
        * Tạo một model ChatterInfo về thành viên trong nhóm từ jid
        */
        initChatterInfoModelFromJid: function (jid) {
            var user = btalk.ROSTER.getUserByJID(jid);
            if (!user)
                return null;

            var item = {
                account: jid,
                jobtitles: user instanceof btalk.roster.eGovUser ? user.getJobTitlesStr() : "",
                departments: user instanceof btalk.roster.eGovUser ? user.getDepartmentsStr() : "",
                online: user.status == "available" ? true : false,
                linkavatar: btalk.view.getAvatar([jid.split('@')[0]]),
            }
            return new btalk.model.ChatterInfo(item);
        },

        // Lam moi lai Collection quan li thanh vien trong nhom.
        resetCollectionMember: function () {
            if (this.collectionMember.length > 0) {
                var i;
                for (i = 0; i < this.collectionMember.length; i++) {
                    this.collectionMember.models[i].destroy();
                }
                this.$el.find('.btalk-body-detail-member ul')
                    .find('li.item-list')
                    .not('li.item-add-people')
                    .remove();
            }
            this.collectionMember.reset();
        },

        // Lấy thông tin từ input của dialog để lấy danh sách input ,sau đó đóng gói và gửi về server.	       

        visibleFormAddMembers: function () {
            var viewFormGroup = new btalk.view.ViewFormGroup();
            viewFormGroup.jidChatter = this.model.get('account');
            viewFormGroup.visibleForm();
        },

        /*
         * Quan li cac file chia se.
         */
        getUrlFile: function (attachment, receiverfileacc) {
            var senderTenantId, ojbect, name, sentdate, percentage, type;
            senderTenantId = attachment.tenantid;
            ojbect = attachment.object;
            name = attachment.name;
            sentdate = attachment.sentDate;
            percentage = attachment.percentage;
            type = attachment.type;
            var _url = btalk.fm.geturl(senderTenantId, receiverfileacc, ojbect, name, sentdate, percentage);
            return _url;
        },

        //  DamBV - 17/02/2017 : Xu li goi tin cac file (hình ảnh, tệp) đã chia sẻ.
        _handleDataSharedFile: function (packageMsg) {
            var _idPackage = packageMsg.doc.children[0].id;
            var ObjectAttachment = $.xml2json(packageMsg.doc);
            var toAndFrom = null,
                listShareFile = null,
                listShareImage = null;

            // Khoi tao cac doi tuong quan luu tru anh / file da chia se.
            listShareImage = this.currentChatter.get(this.SHARE_IMAGE) || [];
            listShareFile = this.currentChatter.get(this.SHARE_FILES) || [];

            /*
             * Trong truong hop cua khong co gia du lieu moi tra ve.
             * - Image: Neu trong trong trang thai xem preview thì chi them vao gia list anh ở duoi
             * Trong truong hop khong phai thi ve tiep vao phan the hien cac anh da chia se(Ap dung lan dau tien).
             * - File thi ve truc tiep vao khu vuc the hien cac file da chia se.(Ap dung lan dau tien).
             */

            toAndFrom = btalk.APPVIEW.getPackageMessageFromServer(ObjectAttachment);
            if (toAndFrom != null && toAndFrom.length > 0) {
                for (var i = 0; i < toAndFrom.length; i++) {
                    if (!toAndFrom[i]) {
                        toAndFrom.splice(i, 1);
                    }
                }
            } else if (toAndFrom == null) {
                if (_idPackage.indexOf('image') > -1) {
                    if (btalk.STATUS_PREVIEWIMAGE) {
                        var src = $('.previewimage .list-image .selected > img').attr('src');
                        btalk.view.previewSharedLocalImage(src);
                    } else {
                        this.showCollectionSharedFile(listShareImage, "image");
                    }
                } else {
                    this.showCollectionSharedFile(listShareFile, "file");
                }
                return;
            }

            // Get URL cua tung file.
            var newSharedFiles = [];
            var newSharedImages = [];
            for (var k = 0; k < toAndFrom.length; k++) {
                if (toAndFrom[k]) {
                    var msg = toAndFrom[k];
                    var receiverFileAcc = msg.chatterJid;
                    if (msg.attachment && msg.attachment.length > 0) {
                        for (var i = 0; i < msg.attachment.length; i++) {
                            var url = this.getUrlFile(msg.attachment[i], receiverFileAcc);
                            msg.attachment[i].url = url;
                            if (msg.attachment[i].type.indexOf('image') > -1) {
                                newSharedImages.push(msg.attachment[i]);
                            } else {
                                newSharedFiles.push(msg.attachment[i]);
                            }
                        }
                    } else {
                        var url = this.getUrlFile(msg.attachment, receiverFileAcc);
                        msg.attachment.url = url;
                        if (msg.attachment.type.indexOf('image') > -1) {
                            newSharedImages.push(msg.attachment);
                        } else {
                            newSharedFiles.push(msg.attachment);
                        }
                    }
                }
            }

            /*
             * Dong bo voi cache cua phan thu muc chia se file.
             * Nếu chatter khong co cache file da chia se thi se tao moi, neu co thì dong bo lai.
             */
            listShareImage = this.synsShareFileLocal(listShareImage, newSharedImages);
            listShareFile = this.synsShareFileLocal(listShareFile, newSharedFiles);

            // luu trang thai cache vao chatter.
            this.currentChatter.set(this.SHARE_IMAGE, listShareImage);
            this.currentChatter.set(this.SHARE_FILES, listShareFile);

            // Hien thi file/image da chia se.
            if (_idPackage.indexOf('image') > -1) {
                if (btalk.STATUS_PREVIEWIMAGE) {
                    var src = $('.previewimage .list-image .selected > img').attr('src');
                    btalk.view.previewSharedLocalImage(src);
                } else {
                    this.showCollectionSharedFile(listShareImage, "image");
                }
            } else {
                this.showCollectionSharedFile(listShareFile, "file");
            }
        },

        synsShareFileLocal: function (oldList, newList) {
            if (oldList && oldList != null) {
                for (var i = 0; i < newList.length; i++) {
                    var isAdd = true;
                    for (var j = 0; j < oldList.length; j++) {
                        if (oldList[j].id === newList[i].id) {
                            isAdd = false;
                            break;
                        }
                    }
                    if (isAdd) {
                        oldList.push(newList[i]);
                    }
                }
            } else {
                oldList = newList;
            }

            // Sort  theo time
            for (var i = 0; i < oldList.length; i++) {
                for (var j = i + 1; j < oldList.length; j++) {
                    if (parseInt(oldList[i].object) < parseInt(oldList[j].object)) {
                        var tmp = oldList[i];
                        oldList[i] = oldList[j];
                        oldList[j] = tmp;
                    }
                }
            }
            return oldList;
        },

        displayLocationShareFile: function () {
            var valDisplay = this.$el.find('.body-sharefile').css('display');
            this.$el.find('.body-sharefile').slideToggle(1000);
            if (valDisplay == "none") {
                // Hien thi cac vung da chia se.
                this.$el.find('.head-sharefile .button-status').removeClass('active');
                this.$el.find('#show_sharefile').addClass('active');

                /*
                 * Hien thi cac file da chia se : neu co thi hien thi ra, neu khong co thi tai ve.
                 */
                // Hinh anh
                var jidCurrrentChatter = this.currentChatter.get('jid');
                var typeCurrentChatter = this.model.get('type');

                var _listImage = this.currentChatter.get(this.SHARE_IMAGE);
                if (_listImage && _listImage.length > 0) {
                    this.showCollectionSharedFile(_listImage, "image");
                } else {
                    console.log("Tai ve lan 1 - image")
                    btalk.cm.getMessageFileInConversation(jidCurrrentChatter, btalk.FILETYPE.IMAGE, null, typeCurrentChatter);
                }

                // File
                var _listFile = this.currentChatter.get(this.SHARE_FILES);
                if (_listFile && _listFile.length > 0) {
                    this.showCollectionSharedFile(_listFile, "file");
                } else {
                    console.log("Tai ve lan 1 - file");
                    btalk.cm.getMessageFileInConversation(jidCurrrentChatter, btalk.FILETYPE.FILE, null, typeCurrentChatter);
                }
            } else {
                // An di cac file da chia se.
                this.$el.find('.head-sharefile .button-status').removeClass('active');
                this.$el.find('#hide_sharefile').addClass('active');
            }
        },

        showCollectionSharedFile: function (listFile, type) {
            var _imageShared = $('#sharedimage');
            var _fileShared = $('#sharedfile');

            if (type == "image") {
                var max = 8;
                if (_imageShared.find('li').length > 7) {
                    max = 9999999999999;
                }
                _imageShared.find('ul').html('');
                for (var k = 0; k < listFile.length && k < max; k++) {
                    this.showSharedFile(listFile[k], type);
                }
            }

            if (type == "file") {
                _fileShared.find('ul').html('');
                for (var k = 0; k < listFile.length; k++) {
                    this.showSharedFile(listFile[k], type);
                }
            }

            // DamBV - 10/04/2017 : kiem tra xem co the hien button tai them file chia se.
            if (_imageShared.find('li').length > 1) {
                _imageShared.find('#button_sysn').show();
            } else {
                _imageShared.find('#button_sysn').hide();
            }

            if (_fileShared.find('div').length > 1) {
                _fileShared.find('#button_sysn').show();
            } else {
                _fileShared.find('#button_sysn').hide();
            }
        },

        showSharedFile: function (attachment, type) {
            if (attachment.url == "") {
                return;
            }

            var managerImamge = $('#sharedimage > ul');
            var managerFile = $('#sharedfile > ul');
            if (type == 'image') {
                var _image = "<li class='item-image'><img src='" + attachment.url + "'/></li>";
                managerImamge.append(_image);
                return false;
            }

            if (type == 'file') {
                var modelAttachment = new btalk.model.Attachment({
                    name: attachment.name,
                    percentage: attachment.percentage,
                    url: attachment.url,
                    id: attachment.id,
                    statusText: "", 		// Uploading, Finalizing, Completed, Error
                    status: 0,				// 200, 201 => Completed; other => Error
                    from: attachment.from,
                    to: attachment.to,
                    type: attachment.type,
                    // Tanent id cua user dang nhap tren OpenStack Swift
                    tenantid: attachment.senderTenantId
                });
                var viewAttachment = new btalk.view.AttachmentView({ model: modelAttachment });
                managerFile.append(viewAttachment.render().el);
            }
        },

        previewSharedImage: function (e) {
            e.preventDefault();
            e.stopPropagation();
            var srcImage = e.target.src;
            btalk.view.previewSharedLocalImage(srcImage);
            $('#modalpreviewimage').show();
            this.downloadMoreSharedFile();
            btalk.STATUS_PREVIEWIMAGE = true;
            btalk.view.showSelectedImage(srcImage);
        },

        actionDowloadMoreFile: function () {
            var iconnormal = document.getElementById('status_normal');
            var iconloading = document.getElementById('status_loadding');
            iconloading.style.display = 'block';
            iconnormal.style.display = 'none';
            setTimeout(function () {
                iconloading.style.display = 'none';
                iconnormal.style.display = 'block';
            }, 1500);

            this.downloadMoreSharedFile();
        },

        /*
         * Ap trung trong 2 truong hop:
         * 1. Khi click xem anh tren tin nhan thi se tai ve.
         * 2. Khi click button tai them ve.
         */
        downloadMoreSharedFile: function () {
            var _listFile = null;
            var _typeFile = $('.manager-share-file li.active').attr('typeFile');

            this.currentChatter = btalk.APPVIEW.CURRENTCHATTER;
            if (_typeFile == "image") {
                _listFile = this.currentChatter.get(this.SHARE_IMAGE);
            } else {
                _listFile = this.currentChatter.get(this.SHARE_FILES);
            }
            var _jidCurrrentChatter = this.currentChatter.get('jid');
            var _typeCurrentChatter = this.currentChatter.get('type');
            // Neu listFile chua tai ve thi tai ve.
            if (_listFile.length < 1) {
                console.log("Tai them ve");
                btalk.cm.getMessageFileInConversation(_jidCurrrentChatter, _typeFile, null, _typeCurrentChatter);
            } else {
                console.log("Tai them ve");
                btalk.cm.getMessageFileInConversation(_jidCurrrentChatter, _typeFile, this.getIdLastMessage(_listFile), _typeCurrentChatter);
            }
        },

        getIdLastMessage: function (listFile) {
            var isDowloadMore = null;
            // Chon file de xac dinh moc lay tiep theo.
            var isDowloadMore = parseInt(listFile[listFile.length - 1].object);
            return isDowloadMore;
        },
    });

    function forcusTab(tabname) {
        $('li[role=presentation]').removeClass('active');
        $('.tab-pane').removeClass("active");

        $("li[data-target=" + tabname + "]").addClass('active');
        $('#' + tabname).addClass("active");
    }

    // VIEW TAI THEM CHATTER
    var CHATTERNEXTVIEW = new ChatterNextView({ model: btalk.CHATTERNEXT });

    // Export to btalk.view api
    btalk.view = $.extend(btalk.view, {
        ChatterView: ChatterView,
        ChatterSearchView: ChatterSearchView,
        ChatterInfoView: ChatterInfoView
        //ChatterNextView: ChatterNextView
    });

    btalk = $.extend(btalk, {
        // Object tai them chatter trong lich su
        CHATTERNEXTVIEW: CHATTERNEXTVIEW
    });
});
