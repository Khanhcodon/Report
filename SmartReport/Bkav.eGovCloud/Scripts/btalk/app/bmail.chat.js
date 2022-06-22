define(['jquery', 'underscore', './btalk/btalk'], function ($, _, btalk) {
    function _createConversation(employeeCustomerServiceID) {
        if(!employeeCustomerServiceID)
            return;
        var members = [];
        members.push(employeeCustomerServiceID);

        var group_jid = btalk.cm.getGroupSupportJID();
        var rosterUser = btalk.ROSTER.addUser(group_jid);
        var newchatter = new btalk.model.Chatter({
            jid: group_jid,
            account: group_jid.split("@")[0],
            type: btalk.CHATTYPE.GROUPCHAT,
            lasttimestamp: new Date(),
            lastmessage: '',
            isme: false,
            unread: false,
            unreadcount: 0,
            isselected: false,
            fullname: 'Lịch sử hỗ trợ',
            rosterUser: rosterUser,
            roster: btalk.ROSTER,
            isResultOfSearch: true,
            statusHeightChatter: btalk.config.statuschatter,
        });
        rosterUser.members = members;
        rosterUser.chatterM = newchatter;

        btalk.CHATTERS.add(newchatter);
        newchatter.set({ isselected: true, unread: true });
        btalk.cm.createGroup(group_jid, members);
    }

    function _sendRequestSupport() {
        console.log("sendRequestSupport");
    }

    function _handlerControlResponseSupport(support_user) {
        if(!support_user)
            return;

        support_user.avatar = btalk.view.getAvatar([support_user.id.split('@')[0]]);
        btalk.ChatBmail.user = support_user;

        var dialogConnect = $("#dialog_connecting_service");
        if (dialogConnect.dialog("instance") && dialogConnect.dialog("isOpen")) {
            dialogConnect.dialog("close");
        }

        var employeebusy = $("#employee_busy");
        if (employeebusy.dialog("instance") && employeebusy.dialog("isOpen")) {
            employeebusy.dialog("close");
        }

        var dialogDisconnect = $("#dialog_disconnect");
        if (dialogDisconnect.dialog("instance") && dialogDisconnect.dialog("isOpen")) {
            dialogDisconnect.dialog("close");
        }

        btalk.ChatBmail.isFinish = false;
        btalk.ChatBmail.isCloseSent = false;

        //Tao ho tro
        btalk.ChatBmail.createConversation(btalk.ChatBmail.user.id);
    }

    function _renderAvatarCustomerService() {
        if(!btalk.ChatBmail.user)
            return;

        var userInfo = $('#user_info');
        var nameConversation = $('#name_conversation');

        if (btalk.ChatBmail.user.avatar) {
            userInfo.find('img').attr('src', btalk.ChatBmail.user.avatar);
        }
        if (btalk.ChatBmail.user.name) {
            userInfo.find('.fullname-user-chatBmail > h3').text(btalk.ChatBmail.user.name);
        }

        nameConversation.hide();
        userInfo.show();
        document.getElementById("btalk_request_support").style.display = "none";
        document.getElementById("btalk_textinput").style.display = "block";
    }

    function _hideNotifyConnectCustomerSupport() {
        var dialogConnect = $("#dialog_connecting_service");
        if (dialogConnect.dialog("instance") && dialogConnect.dialog("isOpen")) {
            dialogConnect.dialog("close");
        }
    }

    function _sendMessageFinishConversation() {
        var _date = btalk.getCoolTime2(new Date());
        var bodyMsg = "<h6>Phiên hỗ trợ đã kết thúc lúc " + _date + "</h6> ";
        var configs = [{ "key": "body_msg_suggest_chatBmail", "value": bodyMsg }];
        if (btalk.APPVIEW.CURRENTCHATTER && btalk.APPVIEW.CURRENTCHATTER.get('jid')) {
            btalk.cm.sendConfigs(configs, btalk.APPVIEW.CURRENTCHATTER.get('jid'), btalk.CHATTYPE.GROUPCHAT,
            btalk.APPVIEW.printAndSaveMessage.bind(btalk.APPVIEW, btalk.APPVIEW.CURRENTCHATTER));
        }
    }

    function _resetDisplayDefault() {
        var userInfo = $('#user_info');
        var nameConversation = $('#name_conversation');
        userInfo.find('img').attr('src', "");
        userInfo.find('.fullname-user-chatBmail > h3').text("");
        nameConversation.show();
        userInfo.hide();

        var btalk_request_support = document.getElementById("btalk_request_support")
        if (btalk_request_support) {
            btalk_request_support.style.display = "block";
        }

        var btalk_textinput = document.getElementById("btalk_textinput")
        if (btalk_textinput) {
            btalk_textinput.style.display = "none";
        }
    }

    // Khi KH tu dong muon ket thuc phine thi se hien Dialog va sau do cung ket thuc.
    function _showDialogBkavPro() {
        var _html = "<p>Phiên hỗ trợ trực tuyến đã kết thúc. Trong quá trình sử dụng nếu gặp bất kỳ vấn đề gì về virus hoặc bất thường trên máy tính,"
        + " Quý khách có thể mở phiên hỗ trợ mới hoặc liên hệ với Trung tâm CSKH Bkav Pro"
        + " qua số điện thoại <b>1900 56 12 96</b> hoặc email <b>BkavPro@bkav.com.vn</b> để được hỗ trợ kịp thời</p>"
        + " <div class='btn-dialog-bkavpro'><center><button onclick='btalk.ChatBmail.closeDialogBkavPro()'>OK<buton></center></div>";

        $('<div id = "dialog_finish_bkavpro"></div>').html(_html).dialog({
            title: "Bkav Pro Internet Security",
            modal: true,
            zIndex: 10000,
            autoOpen: true,
            height: "auto",
            width: 600,
            resizable: false,
            dialogClass: 'bkavpro-popup',
            show: {
                effect: "drop",
            },
            hide: {
                effect: "drop",
                duration: 500
            }
        });
    }

    // Ket thuc phien ho tro
    function _finishConversation(callback) {
        btalk.ChatBmail.resetDisplayDefault();
        btalk.ChatBmail.sendMessageFinishConversation();
        btalk.ChatBmail.isFinish = true;
        btalk.ChatBmail.isCloseSent = true;
    }

    function _handleMsgFinishConversation(msgJson) {
        // Nếu đúng là tin nhắn báo tắt phiên hỗ trợ thì mới xử lý
        if(msgJson && msgJson.config && msgJson.config.body_msg_suggest_chatBmail) {
            if(btalk.ChatBmail && typeof btalk.ChatBmail.resetDisplayDefault === 'function')
                btalk.ChatBmail.resetDisplayDefault();
            if(btalk.ChatBmail.isCloseSent) {
                //Nếu là đóng cửa sổ hỗ trợ
                btalk.ChatBmail.isCloseSent = false;
                if(parent && parent.tabView && (typeof parent.tabView.removeChatTab === 'function'))
                    parent.tabView.removeChatTab();
            }
        }
    }

    var ChatBmail = {
        showNotifyDisconnect: true,
        isFinish: true,

        isCloseSent: false, //Trang thai kiem tra da gui goi tin close ho tro hay chua

        createConversation: _createConversation,

        sendRequestSupport: _sendRequestSupport,

        handlerControlResponseSupport: _handlerControlResponseSupport,

        renderAvatarCustomerService: _renderAvatarCustomerService,

        sendMessageFinishConversation: _sendMessageFinishConversation,

        hideNotifyConnectCustomerSupport: _hideNotifyConnectCustomerSupport,

        resetDisplayDefault: _resetDisplayDefault,

        finishConversation: _finishConversation,

        showDialogBkavPro: _showDialogBkavPro,

        handleMsgFinishConversation: _handleMsgFinishConversation,

        user: {
            name: "Nguyễn Văn Ba",
            avatar: "https://danhba.bkav.com/avatars/tamdn.bmp",
            id: "cskh@bmailtest.vn",
            position: "Nhân viên hỗ trợ khách hàng"
        },

        closeDialogBkavPro: function () {
            $('#dialog_finish_bkavpro').dialog("close");
        }
    }

    btalk = $.extend(btalk, {
        ChatBmail: ChatBmail
    });
});