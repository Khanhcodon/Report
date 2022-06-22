
define([egov.template.chat.messageGroup, egov.template.chat.messageItem
            , egov.template.chat.messageFileItem, egov.template.chat.messageImgItem
            , egov.template.chat.config],
    function (GroupTemplate, MessageTemplate, FileItemTemplate, ImgItemTemplate, ConfigTemplate) {

        var _typingWaitTimeout = 4000;
        var _PAGESIZE = 30;
        var _TIMEFORSTARTCONVERSION = 15 * 60 * 1000; // 15 PHÚT: khoảng thời gian tính bắt đầu một cuộc hội thoại mới

        var MessageView = Backbone.View.extend({
            $message: null,
            $composeContent: null,
            $container: $('#chatDetail'),
            $layoutContent: null,
            $seen: null,
            chatterGroupClass: '.chattergroup',

            isComposing: false,
            _isShowTyping: false,
            messageWith: 150,

            sendings: [],

            images: [],

            options: {
                baseDatetimeQuery: '2015-01-01T00:00:00Z',
                maxMessagesInCache: 50
            },

            events: {
                'click': '_setSeenAll',
                'click .removePaste': '_removePaste'
            },

            initialize: function (options) {
                this.members = [];

                // Danh sách file dính kèm
                this.attachments = [];
                this.messages = [];

                this.isScrollAtBottom = true;

                this.jid = this.model.get('jid');
                this.isGroup = this.model.get('isGroup');
                this.type = this.model.get('type');
                this.render();
            },

            render: function () {
                this._initLayout(function () {
                    this._fixSize();
                    this.registerHandlers();

                    this._renderEmoticons();
                    this._loadMessages();
                    this._setSeenAll();

                }.bind(this));
            },

            //#region Layout

            _initLayout: function (success) {
                var that = this;
                require([egov.template.chat.messageView], function (chatDetailTemplate) {
                    that.$el.html($.tmpl(chatDetailTemplate, {
                        name: that.model.get('fullname'),
                        jid: that.jid,
                        avatar: that.model.get('avatar'),
                        isgroup: that.isGroup,
                    }));

                    that.$message = that.$(".chat-message");
                    that.$composeContent = that.$('.compose-content');
                    that.$layoutContent = that.$('.chat-body');

                    that.$seen = that.$('.chat-seen');
                    that.$seen.find('.user-seen').text(that.model.get('username'));

                    that._bindTypingAvatar([that.model.get('avatar')]);
                    success();
                });
            },

            _fixSize: function () {
                var headerH = this.$('.mdl-layout__header-row').height();
                this.messageWith = 186;

                this.imageSize = {
                    oneCol: this.messageWith,
                    twoCol: this.messageWith / 2,
                    threeCol: (this.messageWith - 2) / 3
                };

                this._scrollLoadMore();
                this._scrollToLastPosition();
            },

            _bindTypingAvatar: function (avatars) {
                var that = this;
                _.each(avatars, function (avatar) {
                    that.$('.chat-typing img').attr('src', avatar);
                });
            },

            _scrollLoadMore: function () {
                this.$layoutContent.scrollLoadMore('prev', this._loadPreviosMessage.bind(this));
            },

            _renderEmoticons: function () {
                this.$("#emoticon").append(btalk.emoticon.render());

                this.$("#emoticon li").bind("click", function (e) {
                    var emoticon_id = parseInt($(e.currentTarget).attr('data-value'));
                    var _symbol = btalk.emoticon.getSymbol(emoticon_id);

                    this.$composeContent.focus();
                    // Luu y luon them khoang trang khi chen emoticon
                    btalk.insertTextAtCursor(" " + btalk.htmlDec(_symbol) + " ");
                }.bind(this));
            },

            //#endregion

            registerHandlers: function () {
                btalk.cm.registerHandler('handle_iq_retrieve_result', this._handleArchiveMessages.bind(this));

                btalk.cm.registerHandler('message', this._handleMessage.bind(this));
                btalk.cm.registerHandler('message_error', this._handleMessageError.bind(this));

                btalk.cm.registerHandler('_handle_status_message', this._handleStatusMessage.bind(this));

                egov.pubsub.subscribe("window.resize", this._fixSize, this);
                egov.pubsub.subscribe("window.paste", this._handlePaste.bind(this));

                egov.pubsub.subscribe("chat.connected", this._sendFailMessages.bind(this));

                // Compose
                this.$('.btnSendMessage').click(this._sendMessage.bind(this));
                this.$composeContent.on('keydown', this._sendComposingAndMessage.bind(this));

                // Toolbar
                this.$(".insertAttachment").change(this._insertAttachment.bind(this));
                this.$(".insertPhoto").change(this._insertPhotos.bind(this));
            },

            //#region Load history messages

            _loadMessages: function () {
                this.hasCached = this.messages && this.messages.length > 0;
                if (!this.hasCached) {
                    this._loadPreviosMessage();
                    return;
                }
            },

            _loadPreviosMessage: function () {
                var loadedMessageCount = this.messages ? this.messages.length : 0;

                btalk.cm.getNextPageOfMessages(this.jid, loadedMessageCount, _PAGESIZE, this.type);
            },

            _handleArchiveMessages: function (resultMessages) {
                var that = this;

                if (!this._isChatWithMe(!resultMessages.chat ? "" : resultMessages.chat.with)) return;

                var messages = this._parseArchiveMessages(resultMessages);

                this._saveToCache(messages);
                this._bindMessage(messages, function () {
                    this._keepScrollAtLastPosition();
                }.bind(this));
            },

            _bindMessage: function (parsedMessages, success) {
                var leng = parsedMessages.length;
                var that = this;
                if (leng <= 0) return;

                this._saveScrollPosition();

                var prepend = true;
                parsedMessages = parsedMessages.reverse();
                _.each(parsedMessages, function (message, idx) {
                    that._printMessage(message, prepend);
                    if (idx === leng - 1) {
                        success();
                    }
                });
            },

            //#endregion

            //#region Handle chatting Message

            _handleMessage: function (oMsg) {
                /*
                 * Handle realtime message
                 */

                if (!oMsg)
                    return;

                if (!this._isChatWithMe(!oMsg ? "" : oMsg.from)) return;

                if (oMsg.processType === "composing") {
                    return this.jid !== oMsg.chatterJid || this._showTyping(oMsg);
                }

                if (oMsg.changeinfo) {
                    this.handleRenameGroupMessage(oMsg);
                    return;
                }

                if (this.jid !== oMsg.chatterJid) {
                    return;
                }

                this._hideTyping();

                oMsg.processType !== 'received' && this._saveToCache([oMsg]);

                //if (!this._isFocussing()) {
                //    return;
                //}

                this._printMessage(oMsg);

                if (oMsg.isError) {
                    this._handleMessageError(oMsg);
                }

                if (oMsg.processType !== btalk.CHATTYPE.CHAT && oMsg.processType !== 'file') {
                    return;
                }
            },

            _handleMessageError: function (message) {
                var oldMsg = this._getMessage(message.id);
                if (!oldMsg) return;

                var type = oldMsg.processType;
                if (type === "file" || type === "image") {
                    oldMsg.isError = true;
                    return;
                }

                var exist = _.find(this.sendings, function (m) { return m.id == oldMsg.id; });
                if (exist) return;

                this.sendings.push(oldMsg);

                this.sendings = _.sortBy(this.sendings, "clienttime").reverse();
            },

            _handleStatusMessage: function (statusMsg) {

                if (!this._isChatWithMe(!statusMsg ? "" : statusMsg.from)) return;

                this._saveScrollPosition();
                statusMsg = this._parseRealtimeMessage(statusMsg);
                this._updateMessageStatus(statusMsg);
                return;
            },

            _handleFileMessage: function (fileMsg) {

            },

            _showTyping: function () {
                if (this._isShowTyping) {
                    return;
                }
                var that = this;

                this._isShowTyping = true;
                this._saveScrollPosition();

                this._typingTimeout = setTimeout(function () {
                    that._hideTyping();
                }, _typingWaitTimeout);

                return this.$('.chat-typing').show() && this._keepScrollAtLastPosition();
            },

            _hideTyping: function () {
                this._isShowTyping = false;
                this.$('.chat-typing').hide();
            },

            _showSeen: function () {
                this.$seen.show();
            },

            _hideSeen: function () {
                this.$seen.hide();
            },

            _setSeenAll: function () {
                this.model.set('unread', 0);
            },

            //#endregion

            //#region Render Message

            _printMessage: function (msg, prepend) {
                var that = this;
                prepend = prepend || false;

                this._hideSeen();

                if (msg.processType == "percentage" || msg.processType == "received") {
                    return;
                }

                if (msg.isConfigMsg) {
                    return this._showConfigMessage(msg, prepend);
                }

                if (that._getMessageElement(msg.id).length > 0) return;

                var isme = msg.senderJid.toLowerCase() == btalk.ROSTER.currentJid;
                var user = btalk.ROSTER.getUserByJID(msg.from);
                var _message = {
                    isme: isme,
                    message: btalk.emoticon.process(msg.body),
                    append: true,
                    // Neu la tin do minh gui di thi moi check trang thai/
                    // [TODO] Check them truong hop tu minh gui cho chinh minh
                    status: msg.status,
                    id: msg.id,
                    // Neu dang forcus thi khong can hien style chua doc
                    unread: btalk.WINDOWFOCUS === true && !fromCacheOrArchive ? false : msg.unread,
                    // Test preview file
                    imageColumn: msg.imageCount <= 3 ? msg.imageCount : 3,
                    imageCount: msg.imageCount || 0,
                    otherCount: msg.otherCount || 0,
                    senderJid: msg.senderJid,
                    receiverJid: msg.receiverJid,
                    account: msg.senderJid.split('@')[0],
                    isConfigMsg: msg.isConfigMsg,
                    avatar: user.get('avatar'),
                    timestamp: msg.servertime,
                    attachment: msg.attachment,
                    contentmsg_quote: null,
                    sendermsg_quote: null,
                    timemsg_quote: null,
                    messageTime: new Date(parseInt(msg.clienttime || msg.servertime)).format("HH:mm dd/MM/yyyy")
                };

                _message.viewedBy = btalk.VIEWED_CACHE_LIST.getViewedListByMsgId(msg.chatterJid, msg.id);

                if (msg.quote) {
                    _message.contentmsg_quote = btalk.emoticon.process(msg.quote.contentmsg);
                    var senderName = msg.quote.sendermsg ? msg.quote.sendermsg.split('@')[0] : "";
                    var senderUser = _.find(egov.setting.allUsers, function (u) { return u.username === senderName; });

                    _message.sendermsg_quote = !senderUser ? senderName : senderUser.fullname;
                    //var _date = new Date(parseInt(msg.quote.timemsg));
                    //_message.timemsg_quote = btalk.getCoolTime2(_date);
                }

                if (msg.type === btalk.CHATTYPE.GROUPCHAT) {
                    _message.isMsgGroup = true;
                }

                if (msg.processType === 'file') {
                    this._showFileMessage(_message, prepend);
                    return;
                }

                if (msg.processType === 'image') {
                    this._showImageMessage(_message, prepend);
                    return;
                }

                that._showChatMessage(_message, prepend);
                return;
            },

            _showChatMessage: function (message, prepend) {
                var that = this, $chatterMessage;

                $chatterMessage = that._getMessageGroupElement(message, prepend);
                var container = $chatterMessage.find(".chatter-messages");

                prepend ? container.prepend($.tmpl(MessageTemplate, message)) : container.append($.tmpl(MessageTemplate, message));

                container.find('.chat-content').off('click').on('click', this._toggleMessageTime.bind(this));
            },

            _showFileMessage: function (message, prepend) {
                var that = this, $chatterMessage;

                if (this._getMessageElement(message.id).length === 1) return;

                $chatterMessage = that._getMessageGroupElement(message, prepend);
                var container = $chatterMessage.find(".chatter-messages");
                prepend ? container.prepend($.tmpl(FileItemTemplate, message)) : container.append($.tmpl(FileItemTemplate, message));

                $chatterMessage.find(".btn-link").off('click').on("click", function (e) {
                    var target = $(e.target).closest('.btn-link');
                    var fileInfo = target.parents(".item-message__data");
                    var url = fileInfo.attr('url');
                    var name = fileInfo.attr('file-name');
                    var extension = fileInfo.attr('extension');
                    var fileName = String.format("{0}.{1}", name, extension);
                    target.is('.openFile') ? openAttachment(url, fileName) : egov.fileExtension.downloadUri(url, fileName);
                    e.preventDefault();
                });
            },

            _showImageMessage: function (message, prepend) {
                var that = this, $chatterMessage;
                if (this._getMessageElement(message.id).length === 1) return;

                $chatterMessage = that._getMessageGroupElement(message, prepend);

                that._parsePreviewImages(message.attachment, function (image) {
                    message.previewImages = image;
                    _.each(message.previewImages, function (a) {
                        that.images.push({
                            id: a.id,
                            name: a.name,
                            url: a.url,
                            w: a.width,
                            h: a.height,
                            isLandscape: a.isLandscape
                        });
                    });

                    message.isMulti = message.attachment.length > 1;

                    var container = $chatterMessage.find(".chatter-messages");
                    prepend ? container.prepend($.tmpl(ImgItemTemplate, message)) : container.append($.tmpl(ImgItemTemplate, message));

                    $chatterMessage.find(".chat-image").off("click").on("click", that._openPreview.bind(that));
                });
            },

            _showMessageError: function (message) {
                var messageElement = this._getMessageElement(message.id);
                if (!messageElement) return;
                messageElement.find(".chat-content-wrap").append("<span style='color:red;'>Gui loi!</span>")
            },

            _getMessageElement: function (id) {
                return this.$('[data-id="' + id + '"]');
            },

            _getMessageGroupElement: function (message, prepend) {
                var that = this;
                var $chatterMessage = prepend ? that.$(".chat-message > div").first() : that.$(".chat-message > div").last();
                var lastSender = $chatterMessage.attr('senderid');

                if ($chatterMessage.is('.chat-config') || lastSender !== message.senderJid) {
                    var newGroup = $.tmpl(GroupTemplate, {
                        isme: message.isme,
                        avatar: message.avatar,
                        senderid: message.senderJid
                    });

                    prepend ? that.$message.prepend(newGroup) : that.$message.append(newGroup);

                    $chatterMessage = prepend ? that.$message.find(that.chatterGroupClass).first() : that.$message.find(that.chatterGroupClass).last();
                    message.isFirsrMessage = true;
                } else {
                    message.isFirsrMessage = false;
                }

                message.width = that.messageWith;

                return $chatterMessage;
            },

            _showConfigMessage: function (message, prepend) {
                prepend ? this.$message.prepend($.tmpl(ConfigTemplate, message)) : this.$message.append($.tmpl(ConfigTemplate, message));
            },

            _updateMessageStatus: function (msg) {

                var oldMsg = this._getMessage(msg.id);
                if (!oldMsg) {
                    return;
                }

                msg.processType == "received" && this._updateStateReceiveMsg(oldMsg, msg);

                return;
            },

            _updateStateReceiveMsg: function (oldMsg, msg) {
                if (oldMsg.status === 'viewed') {
                    return;
                }

                oldMsg.status = msg.status;
                var statusElement = this.$message.find("[idmsg='" + msg.id + "']");
                if (statusElement.length > 0) {
                    var icon = this._getStatusIcon(msg.status);
                    statusElement.html(icon);
                }

                if (msg.status === 'server') {
                    this._sendNotifications(msg);
                }

                if (msg.status === "viewed") {
                    this._showSeen();
                }

                this._keepScrollAtLastPosition();
            },

            _getStatusIcon: function (status) {
                switch (status) {
                    case 'client':
                        return '<i class="material-icons">panorama_fish_eye</i>';
                    case 'server':
                        return '<i class="material-icons">check_circle_outline</i>';
                    case 'success':
                        return '<i class="material-icons">check_circle</i>';
                    default:
                        return '';
                }
            },

            //#endregion

            //#region Parse Response Messages

            _parseArchiveMessages: function (responseMessages) {
                var result = [], to, from, chatwith, startTime;

                // Neu goi tin khong phai la goi tin chua noi dung tin nhan thi dung lai.
                if (!responseMessages.chat) {
                    return result;
                }

                chatwith = btalk.cutResource(responseMessages.chat.with);
                startTime = new Date(responseMessages.chat.start);

                if (responseMessages.chat.to) {
                    to = $.isArray(responseMessages.chat.to) ? responseMessages.chat.to : [responseMessages.chat.to];
                    result = _.union(result, this._parseMessage(to, startTime, chatwith, false));
                }

                if (responseMessages.chat.from) {
                    from = $.isArray(responseMessages.chat.from) ? responseMessages.chat.from : [responseMessages.chat.from];
                    result = _.union(result, this._parseMessage(from, startTime, chatwith, true));
                }

                result = _.sortBy(result, 'clienttime');
                return result;
            },

            _parseMessage: function (messages, start, chatwith, isFrom) {
                var result = [], that = this, fileCount = 0, imgCount = 0;

                _.each(messages, function (msg) {
                    if (!msg || typeof msg == "string") {
                        return;
                    }

                    if (!msg.servertime) {
                        var secs = typeof msg.secs != 'number' ? parseInt(msg.secs) : msg.secs;
                        msg.servertime = start.setSeconds(start.getSeconds() + secs);
                    }

                    if (isFrom) {
                        msg.from = msg.from || chatwith;
                        msg.to = btalk.ROSTER.currentJid;
                    } else {
                        msg.from = btalk.ROSTER.currentJid;
                        msg.to = chatwith;
                    }

                    msg.chatterJid = btalk.cutResource(chatwith);

                    // Encode html truoc khi hien thi len giao dien
                    msg.body = btalk.text(msg.body);

                    // Xac dinh kich ban xu ly tiep theo cua goi tin message
                    msg.processType = that._checkProcessTypeOfMessage(msg);

                    if (msg.attachment) {
                        msg.attachment = $.isArray(msg.attachment) ? msg.attachment : [msg.attachment];

                        var receiverFileAcc = msg.receiverJid;
                        _.each(msg.attachment, function (a) {
                            var senderTenantId = a.tenantid || "";

                            a.extension = a.name.indexOf(".") > 0 ? _.last(a.name.split(".")) : _.last(msg.body.split("."));
                            a.name = egov.fileExtension.getFileName(a.name);
                            a.size = egov.fileExtension.getSizeText(a.size);
                            a.url = btalk.fm.geturl(senderTenantId, receiverFileAcc, a.object, String.format("{0}.{1}", a.name, a.extension), a.sentDate, 101);
                        });
                    }

                    if (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.from.indexOf(btalk.cm.options.xmpp.conference) > 0) {
                        // <message from="groupname@conference.bkav.com/dungha" to="cuongnt@bkav.com/Ubuntu-14.04".../>
                        var msgChatter = btalk.getResource(msg.from);
                        if (msgChatter == "") {
                            return;
                        }

                        msg.senderJid = msgChatter + '@' + btalk.cm.options.xmpp.domain;
                        msg.receiverJid = btalk.cutResource(msg.from);
                    } else {
                        // hoac neu la tin nhan do chinh minh gui toi nhom tu client khac (tin nhan groupchat carbons):
                        // <message from="cuongnt@bkav.com/Ubuntu-14.04" to="groupname@conference.bkav.com/cuongnt".../>
                        // hoac la tin nhan 1-1 thong thuong
                        // Nguoi gui thuc su
                        msg.senderJid = btalk.cutResource(msg.from);
                        // Nguoi nhan thuc su
                        msg.receiverJid = btalk.cutResource(msg.to);
                    }

                    that._parseConfigMessage(msg);

                    result.push(msg);
                });

                return result;
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

            //#endregion

            //#region Events

            _sendMessage: function (e) {
                if (this.$composeContent.text().trim() === '' && this.pastedContent == null) {
                    return;
                }

                this._sendStartConversation();

                this.pastedContent && this._sendPasted();

                var message = this.$composeContent.html();
                message !== '' && this._sendText(message);

                this.$composeContent.empty();
                this.$composeContent.focus();
                this.$('.send-files').show();

                e.preventDefault();
                return false;
            },

            _updateRoster: function (message) {
                var user = this.model;
                var lastTime = new Date();
                user.set('lastMessage', message);
                user.set('lastMessageTime', lastTime);
                user.set('lastTimeString', btalk.getCoolTime(lastTime));

                egov.pubsub.publish('hisroties.update', user);
            },

            _sendStartConversation: function () {
                // Gửi gói tin config hiển thị thời gian bắt đầu cuộc hội thoại.
                var dateSend = new Date();
                if (dateSend - this.lastMessageTime < _TIMEFORSTARTCONVERSION) return;

                // Gửi tin nhắn thời gian bắt đầu cuộc hội thoại mới
                var configs = [{ "key": "edit_by", "value": btalk.ROSTER.currentJid },
                                { "key": "start_conversion", "value": dateSend.getTime() }];

                btalk.cm.sendConfigs(configs, this.jid, this.type, this._printAndSaveMessage.bind(this));
            },

            _sendSeenMessage: function (message) {
                message.read = true;
                var msg = $.extend({}, message); // clone message gốc

                if (this.isGroup) {
                    msg.from = this.jid;
                }

                btalk.cm.sendReceived(msg, 'viewed');
            },

            _sendComposingAndMessage: function (e) {
                // Gui message bao trang thai dang go chi khi dang go phim ki tu, phim so
                // 4 giay moi gui 1 goi tin composing di 1 lan
                if (this.isComposing === false) {
                    this.isComposing = true;
                    btalk.cm.sendComposing(this.jid, this.type);

                    // Dat timeout, de sau 4s neu su kien soan tin nhan van phat sinh thi moi tiep tuc
                    // gui message composing
                    setTimeout(function () {
                        this.isComposing = false;
                    }.bind(this), _typingWaitTimeout);
                }

                var comment = this.$composeContent.text().trim();
                comment === "" ? this.$('.send-files').show() : this.$('.send-files').hide();

                if (e.keyCode === KeyCode.enter) {
                    this._sendMessage(e);
                }
            },

            _printAndSaveMessage: function (message) {
                message = this._parseRealtimeMessage(message);
                message.unread = false;
                message.status = 'client';
                message.imageCount = 0;
                message.otherCount = 0;

                this._saveToCache([message]);

                if (!message.isSent) {
                    this._handleMessageError(message);
                }

                this._printMessage(message);
            },

            _saveToCache: function (messages) {
                this.messages = _.union(this.messages, messages);
                this.model.set('messages', this.messages);
                var lastTime = parseFloat(_.max(_.pluck(this.messages, 'clienttime')));
                this.lastMessageTime = new Date(lastTime);
            },

            _sendNotifications: function (statusMessage) {
                if (typeof window.addChatNotify !== 'function') {
                    return;
                }

                if (statusMessage.processType !== 'received' || statusMessage.status !== 'server') {
                    return;
                }

                var message = this._getMessage(statusMessage.id);
                var userReceived = btalk.ROSTER.getUserByJID(statusMessage.chatterJid);
                var userSend = btalk.ROSTER.getUserByJID(message.from);

                var notify = {
                    title: userSend.fullname,
                    originalContent: message.body,
                    content: message.body,
                    extMessage: message.body,

                    date: new Date(),
                    avatar: userSend.get('avatar'),
                    userName: userSend.get('username'),
                    fullName: userSend.get('fullname'),
                    type: 0,
                    isRead: false,
                    isMe: true,

                    sender: message.chatterJid,

                    //chat
                    chatterJid: this.jid,
                    messageId: message.id,
                    userId: userReceived.get('userid')
                };

                window.addChatNotify(notify);
            },

            _toggleMessageTime: function (e) {
                $(e.target).closest('.item-message').find(".message-time").slideToggle();
                e.preventDefault();
            },

            _handlePaste: function (data) {
                if (!this._isFocussing() || data == null) return;

                var content = data.data;
                var isImage = data.isImage;

                if (content) {
                    var canvas = this.$('#previewCanvas')[0];
                    var ctx = canvas.getContext('2d');

                    var img = new Image();

                    img.onload = function () {
                        ctx.drawImage(img, 0, 0);
                    };

                    var URLObj = window.URL || window.webkitURL;

                    img.src = URLObj.createObjectURL(content);

                    this.$(".pastePreview").show();
                    this.$composeContent.text('');
                    this.pastedContent = content;
                }
            },

            _removePaste: function () {
                this.pastedContent = null;

                var pasteContent = this.$(".pastePreview");
                pasteContent.slideDown().hide();

                var canvas = this.$('#previewCanvas')[0];
                var context = canvas.getContext('2d');
                context.clearRect(0, 0, canvas.width, canvas.height);
            },

            //#endregion

            //#region Send Text

            _sendText: function (message) {
                message = btalk.text(message);

                this._saveScrollPosition();

                btalk.cm.sendMessage(message, this.jid, null, this._printAndSaveMessage.bind(this));
                this._scrollToLastPosition();

                this._updateRoster(message);
            },

            _sendPasted: function () {
                if (!this.pastedContent) return;

                var that = this;
                var messageid = new Date().getTime().toString();
                var attachments = this._parseAttachments([this.pastedContent], messageid);
                var total = _.keys(attachments).length;
                var idx = 0;

                _.each(attachments, function (a) {
                    that._getImageDimension(a.file, function (dimension) {
                        dimension && (a.dimension = dimension);
                        idx++;

                        if (idx === total) {
                            that._removePaste();
                            return that._uploadFiles(messageid, attachments, total, true);
                        }
                    });
                });
            },

            //#endregion

            //#region Send Files

            _insertAttachment: function (e) {
                var files = e.target.files;
                var attachments = this._parseAttachments(files);
                var total = _.keys(attachments).length;
                // Gui message bao danh sach file se trao doi
                var messageid = new Date().getTime().toString();

                this.$(".insertAttachment").val(null);

                return this._uploadFiles(messageid, attachments, total, false);
            },

            _parseAttachments: function (files, messageid) {
                var sentDate = new Date();
                var that = this, fileIdx = 1;

                if (files.length > 5) {
                    return;
                }

                var hasNotAllowSize = _.max(files, function (f) { return f.size; }) > maxAllowSize;
                if (hasNotAllowSize) {

                    return;
                }

                var maxAllowSize = egov.config.FILESERVER.MAX_SIZE * 1000000;

                var result = {};
                _.each(files, function (f) {
                    fileid = String.format("file_{0}_{1}", messageid, fileIdx);

                    result[fileid] = {
                        fileServerType: btalk.fm.fileServerType,
                        id: fileid,
                        lastModified: f.lastModified,
                        lastModifiedDate: f.lastModifiedDate,
                        messageid: messageid,
                        name: f.name,
                        object: sentDate.getTime() + fileIdx + '',
                        sentDate: sentDate,
                        size: f.size,
                        tenantid: btalk.fm.gettenantid(),
                        'type': f.type,
                        file: f,
                        append: true,
                    };

                    fileIdx++;
                });

                return result;
            },

            _uploadFiles: function (msgId, attachments, totalfiles, isPhoto) {
                var fileids = [];

                var message = btalk.messageFactory.sendAttachmentMessage(msgId, btalk.ROSTER.currentJid, this.jid, this.type, attachments);
                this._printAndSaveMessage(message.JSON());

                for (var f in attachments) {
                    btalk.fm.upload(attachments[f], attachments[f].from, attachments[f].to,
                        this.sendPercentage.bind(this, f, attachments, totalfiles, fileids, message),
                        this.sendAttachmentError.bind(this, msgId, attachments, totalfiles, fileids));
                }
            },

            sendPercentage: function (fileid, attachments, totalfiles, fileids, message, response, header) {
                // prop: "file_1533268025024_1" 
                // response: percent {percentage: 100, state: "Finalizing."}
                // response: uploaded [{FileName: "Wr5DBk9JPU5fYN82EQfka5dVkuXfavloaC2N", RootName: "904-STTTT-TT-BCXB.PDF", CreatedDate: "8/3/2018 12:00:00 AM"}]

                if (!response) return;

                var attachmentElement = $("#" + fileid);
                if (attachmentElement.length === 0) return;

                var attachment = attachments[fileid];
                if (!attachment) return;

                if (response.percentage) {
                    attachmentElement.find("#p1 .progressbar").width(response.percentage + "%");
                    return;
                }

                var uploadCompleted = btalk.fm.fileServerType === "egov" && JSON.parse(response)[0].FileName;
                if (uploadCompleted) {
                    fileids.push(attachment.id);
                    // 101: trang thai gui thanh cong.
                    // Vi 100: la trang thai progress upload ket thuc thoi chu khong dam bao upload thanh cong.
                    attachment.percentage = 101;

                    btalk.fm.fileServerType != "egov" && (attachment.tenantid = btalk.fm.gettenantid());

                    attachment.uploadCompleted = true;
                    attachment.url = btalk.fm.geturl(attachment.tenantid, message.receiverJid, message.object, String.format("{0}.{1}", attachment.name, attachment.extension), attachment.sentDate, 101);
                }

                var isImage = attachment.type.startWith('image');
                uploadCompleted && (isImage ? attachmentElement.css('backgroundImage', String.format('url({0})', attachment.url)) : attachmentElement.find("#p1").remove());

                if (totalfiles == fileids.length) {
                    btalk.cm.sendAttachments(message);
                }
            },

            sendAttachmentError: function (fileid, attachments, totalfiles, fileids, error) {
                var attachmentElement = this._getMessageElement(fileid);
                if (attachmentElement.length === 0) return;

                attachmentElement.find("#p1").remove();
                attachmentElement.find(".item-message__data").append("<div style='color:red;'>" + (error && error.message) + "</div>");
                return;
            },

            //#endregion

            //#region Send Photos

            _insertPhotos: function (e) {
                var files = e.target.files;
                var that = this;

                // Gui message bao danh sach file se trao doi
                var messageid = new Date().getTime().toString();
                var attachments = this._parseAttachments(files, messageid);
                var total = _.keys(attachments).length;
                var idx = 0;

                this.$(".insertPhoto").val(null);

                _.each(attachments, function (a) {
                    that._getImageDimension(a.file, function (dimension) {
                        dimension && (a.dimension = dimension);
                        idx++;

                        if (idx === total) {
                            return that._uploadFiles(messageid, attachments, total, true);
                        }
                    });
                });
            },

            _openPreview: function (e) {
                var previewEl = $('<div>').addClass('imagePreview');
                var fileId = $(e.target).closest('.chat-image').attr('id');

                var imagePreview = $("#imagePreview");
                imagePreview.find("ul").html($.tmpl('<li><img src="${url}" alt="Picture 1"></li>', this.images));
                var idx = _.indexOf(_.pluck(this.images, 'id'), fileId);
                var viewer = new Viewer(document.getElementById('imagePreview'), {
                    hidden: function () {
                        viewer.destroy();
                    }
                });

                viewer.view(idx);

                e.preventDefault();
            },

            _parsePreviewImages: function (files, success) {
                var idx = 0;
                var total = files.length, file;
                var result = [], that = this;

                var parseSuccess = function (files) {
                    switch (total) {
                        case 1:
                            file = files[0];
                            var w = Math.min(file.width, file.isLandscape === 'true' ? that.imageSize.oneCol : 123);
                            result.push({
                                width: w,
                                height: Math.floor(w * file.height) / file.width,
                                url: file.url,
                                id: file.id
                            });
                            break;
                        case 2:
                            result.push(_.map(files, function (f) {
                                return {
                                    width: that.imageSize.twoCol,
                                    height: that.imageSize.twoCol,
                                    url: f.url,
                                    id: f.id
                                };
                            }));
                            break;
                        case 3:
                            var landscapes = _.where(files, { isLandscape: "true" });
                            if (landscapes.length === 1) {
                                var f = landscapes[0];
                                var w = Math.min(f.width, f.isLandscape === 'true' ? that.imageSize.oneCol : 123);
                                result.push([{
                                    width: w,
                                    height: Math.floor(w * f.height) / f.width,
                                    url: f.url,
                                    id: f.id,
                                    colspan: 2
                                }]);

                                result.push(_.map(_.where(files, { isLandscape: "false" }), function (f) {
                                    return {
                                        width: that.imageSize.twoCol,
                                        height: that.imageSize.twoCol,
                                        url: f.url,
                                        id: f.id
                                    };
                                }));

                                break;
                            }

                            result.push(_.map(files, function (f) {
                                return {
                                    width: that.imageSize.threeCol,
                                    height: that.imageSize.threeCol,
                                    url: f.url,
                                    id: f.id
                                };
                            }));

                            break;
                        default:
                            var chunkFiles = _.chunk(files, total === 4 ? 2 : 3);
                            _.each(chunkFiles, function (chunk) {
                                result.push(_.map(chunk, function (f) {
                                    return {
                                        width: total === 4 ? that.imageSize.twoCol : that.imageSize.threeCol,
                                        height: total === 4 ? that.imageSize.twoCol : that.imageSize.threeCol,
                                        url: f.url,
                                        id: f.id
                                    };
                                }));
                            });
                            break;
                    };

                    success(result);
                };

                _.each(files, function (a) {
                    that._getImageDimension(a.url, function (dimension) {
                        dimension && (a.dimension = dimension);
                        a.width = dimension.width;
                        a.height = dimension.height;
                        a.isLandscape = dimension.isLandscape;
                        idx++;

                        if (idx === total) {
                            parseSuccess(files);
                        }
                    });
                });
            },

            //#endregion

            _sendFailMessages: function () {
                if (!this.sendings || this.sendings.length === 0) return;

                while (this.sendings.length > 0) {
                    var message = this.sendings.pop();
                    setTimeout(btalk.cm.sendMessage(message, this.jid, message.id, this._printAndSaveMessage.bind(this)), 1000);
                }
            },

            //#region Private Methods

            _isChatWithMe: function (chatWith) {
                return String.isNullOrEmpty(chatWith) ? false : this.jid.equals(chatWith, true);
            },

            _showChatInfo: function (e) {

                e.preventDefault();
            },

            _backToChat: function () {

            },

            _saveScrollPosition: function () {
                this.lastScrollPosition = this.$layoutContent.get(0).scrollHeight;
                this.isScrollAtBottom = this.$layoutContent && this.$layoutContent.length > 0 &&
                                            (this.$layoutContent.scrollTop() + this.$layoutContent[0].clientHeight) >= this.lastScrollPosition - 20;
            },

            _keepScrollAtLastPosition: function () {
                this.isScrollAtBottom ? this.$layoutContent.scrollToBottom() : this._scrollToLastPosition();
            },

            _scrollToLastPosition: function () {
                var scrollDiff = this.$layoutContent.get(0).scrollHeight - this.lastScrollPosition;
                this.$layoutContent.get(0).scrollTop += scrollDiff;
            },

            _getMessage: function (id) {
                return _.find(this.messages, function (m) {
                    return m.id === id && (m.processType === "chat" || m.processType === "file" || m.processType === 'image');
                });
            },

            _getLastmessageStr: function (body) {
                if (!body) {
                    return "";
                }
                return body.length <= 100 ? body : body.substr(0, 100);
            },

            _getImageDimension: function (file, success) {
                var _URL = window.URL || window.webkitURL, result = {}, img;
                if (file) {
                    img = new Image();
                    img.onload = function () {
                        result.width = this.width;
                        result.height = this.height;
                        result.isLandscape = this.width / this.height > 1.2;
                        success(result);
                    };
                    img.src = typeof file === 'string' ? file : _URL.createObjectURL(file);
                    return;
                }

                success(null);
            },

            _isFocussing: function () {
                // Todo: cần xem lại cách này.
                // Gọi từ DOM như này tiện, nhưng lâu dài khó maintain.
                // Cần truyền thêm Tab từ chat-dock vào đây

                return this.$el.parents('.chat-tab').is(".onfocused");
            }

            //#endregion
        });

        function openAttachment(url, name) {
            Plugin && Plugin.openFileUri(url, name);
        }

        return MessageView;
    });