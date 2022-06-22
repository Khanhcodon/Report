// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function (btalk) {
    'use strict';

    btalk.view = btalk.view || {};
    if (btalk.view.index === true) {
        return;
    }

    btalk.view.index = true;

    //Neu dang vao tu egov thi focus = false, neu vao truc tiep chat thi focus = true
    btalk.WINDOWFOCUS = (top && top.notify && typeof top.notify.requestPermission === 'function') ? false : true;

    /* Global event
    ---------------------------------------------------------------------- */

    /**
     * WINDOW
     */
    $(window).resize(function () {
        if (btalk.APPVIEW) {
            btalk.APPVIEW.detectChatterNextState();
            btalk.APPVIEW.detectMessageNextState();

            // Edit DamBV
            if (btalk.cache.heightTabHistory != null) {
                var _height = $('#tab_history').height();
                if (_height > btalk.cache.heightTabHistory) {
                    btalk.CHATTERNEXTVIEW.loadNextPageOfChatters();
                }
            }
        }
        if (btalk.APPVIEW) {
            console.log('Reset IDLE');
            btalk.APPVIEW.resetIdle();
        }
    })
    .focus(function () {
        btalk.WINDOWFOCUS = true;
        if (btalk.APPVIEW) {
            // Xu ly bao da xem khi forcus tro lai cua so chat
            if (btalk.APPVIEW.CURRENTCHATTER
                && typeof btalk.APPVIEW.CURRENTCHATTER.unreadcount === 'function'
                && btalk.APPVIEW.CURRENTCHATTER.unreadcount() > 0) {
                btalk.APPVIEW.viewAllMessageOfCurrentChatter(btalk.APPVIEW.CURRENTCHATTER);
            }

            // Dong notification neu co
            btalk.APPVIEW.clearNotification();
        }
        if (btalk.APPVIEW) {
            console.log('Reset IDLE');
            btalk.APPVIEW.resetIdle();
        }
    })
    .blur(function () {
        btalk.WINDOWFOCUS = false;
        if (btalk.APPVIEW) {
            console.log('Reset IDLE');
            btalk.APPVIEW.resetIdle();
        }
    })
     .bind('mousemove', (function (e) {
         if (btalk.APPVIEW) {
             console.log('Reset IDLE');
             btalk.APPVIEW.resetIdle();
         }
     }))
     .bind('mousedown', (function (e) {
         if (btalk.APPVIEW) {
             console.log('Reset IDLE');
             btalk.APPVIEW.resetIdle();
         }
     }))
     .bind('keypress', (function (e) {
         if (btalk.APPVIEW) {
             console.log('Reset IDLE');
             btalk.APPVIEW.resetIdle();
         }
     }))
     .bind('DOMMouseScroll', (function (e) {
         if (btalk.APPVIEW) {
             console.log('Reset IDLE');
             btalk.APPVIEW.resetIdle();
         }
     }))
     .bind('mousewheel', (function (e) {
         if (btalk.APPVIEW) {
             console.log('Reset IDLE');
             btalk.APPVIEW.resetIdle();
         }
     }))
     .bind('touchmove', (function (e) {
         if (btalk.APPVIEW) {
             console.log('Reset IDLE');
             btalk.APPVIEW.resetIdle();
         }
     }))
     .bind('MSPointerMove', (function (e) {
         if (btalk.APPVIEW) {
             console.log('Reset IDLE');
             btalk.APPVIEW.resetIdle();
         }
     }));

    /**
     * DOCUMENT
     */

    var ctrlKey = 17, vKey = 86, cKey = 67, enterKey = 13, shiftKey = 16, fKey = 70,
        upKey = 38, downKey = 40, leftKey = 37, rightKey = 39, escKey = 27;
    var leftMouse = 1, middleMouse = 2, rightMouse = 3;

    /*
     * Su kien chuot, phim tren toan giao dien
     */
    $(document)
    .keydown(function (e) {
        var keycode = e.which;
        // Ctrl + F
        if (e.ctrlKey === true && keycode == fKey) {
            if (btalk.APPVIEW) {
                btalk.APPVIEW.$searchTxt.focus();
                // Bo qua hanh dong mac dinh cua trinh duyet (o day la mo cua so tim kiem)
                e.preventDefault();
            }
        }
        if (btalk.STATUS_PREVIEWIMAGE) {
            switch (keycode) {
                case escKey:
                    document.getElementById('modalpreviewimage').style.display = 'none';
                    btalk.STATUS_PREVIEWIMAGE = false;
                    break;
                case leftKey:
                    btalk.view.nextImage();
                    break;
                case rightKey:
                    btalk.view.previousImage();
                    break;
            }
        }
    })
    .mousedown(function (e) {
        var keycode = e.which;
        if (keycode == leftMouse) {
            // Phai clearSelection() tai day de co the
            // lap tuc boi chon noi dung chat duoc khi dang focus trong textbox chat
            btalk.clearSelection();
        }
    })
    .mouseup(function (e) {
        var keycode = e.which;
        if (keycode == leftMouse) {
            // left mouse
            // Bo qua khong xu ly forcus vao textbox chat khi:
            // 1. Click vao cac control nhap lieu (input, textarea, .btn)
            // 2. Khi click vao chinh textbox chat
            // 3. Khi dang boi chon text de copy
            // 4. Khi thay doi ten nhom chat
            // 5. Khi tim kiem trong trong cuoc hoi thoai.
            var range = btalk.saveSelection();
            var $target = $(e.target);
            var $parentTarget = $(e.target.parentElement);
            if (e.target.tagName.toLowerCase() == 'input' ||
                //DamBV - tranh focus khi doi ten nhom.
                    e.target.name == "optionSearchMsg" ||
                    e.target.id == 'img_searchMsg' || e.target.id == 'name-group' || e.target.id == 'textarea-rename-id' ||
                    e.target.tagName.toLowerCase() == 'textarea' ||
                    e.target.tagName.toLowerCase() == 'a' ||

                // CuongNT - 01/04/2016: Fix: loi focus tren firefox khong duoc neu click vao messageTxt rong. Bo sung dieu kien:  && $(e.target).html().length > 0
                    ($target.attr('id') == 'messageTxt' && $target.html().length > 0) ||
                // Cac nut bam tren giao dien, su dung class btn cua bootrap (upload file, emoticon)
                    $target.hasClass('btn') || $parentTarget.hasClass('btn') ||
                // Khi dang boi chon text de copy
                    (range && range.collapsed === false)) {
                return;
            }
            $("#messageTxt").focusAtEnd();
        }
    });


    /*
     * Detect trang thai Idle
     */
    btalk.ISIDLE = false;
    btalk.lastActive = new Date();
    btalk.TIMETOIDLE = 60000; //Config thoi gian idle, don vi la milisecond
    $(document).ready(function () {
        //Dinh ky 30s kiem tra lastActive de set trang thai Idle
        var idleInterval = setInterval(function () {
            if (!btalk.ISIDLE) {
                var current = new Date();
                if ((current - btalk.lastActive) > btalk.TIMETOIDLE) {
                    //set trang thai idle
                    btalk.ISIDLE = true;
                }
            }
        }, 30000);

        //Set lastActive when mousemove or keypress
        $(this).mousemove(function (e) {
            btalk.ISIDLE = false;
            btalk.lastActive = new Date();
        });
        $(this).keypress(function (e) {
            btalk.ISIDLE = false;
            btalk.lastActive = new Date();
        });
    });

    /* Index View
    ---------------------------------------------------------------------- */

    var IndexView = Backbone.View.extend({
        options: {
            baseDatetimeQuery: '2015-01-01T00:00:00Z',
            maxMessagesInCache: 50
        },

        // La lan dang nhap, su dung dau tien (khi chua co lich su hoi thoai nao)
        NEVER_USE: false,
        CURRENTCHATTER: {},
        MESSAGENEXT: {},
        MESSAGENEXTVIEW: {},

        // DamBV : profile cua chatter hien tai.
        CHATTER_INFO_MODEL: null,
        CurrentChatterViewInfo: null,

        // Cua so doi them nhom hoi thoai
        //dialog_groupname: null,

        el: "#chatFrame",

        // div chua $messagePanel va $messageTxt
        $middlePanel: $('#btalk_body_content'),

        // div chua danh sach tin nhan: conversationDay, member, message
        $messagePanel: $("#message_panel"),
        $messagePanelOuter: $("#chat_right_top_panel_id"),

        // Textbox go noi dung hoi thoai
        $messageTxt: $("#messageTxt"),
        $messageTxtOuter: $(".message-textbox"),

        // div chua danh sach chatter
        $chatterPanel: $("#chatter_panel"),
        $chatterPanelOuterOuter: $('#tab_history'),

        // ul chua ket qua tim kiem chatter
        $chatterSearchPanel: $("#tab_search ul"),

        // div message next: tai trang tin nhan tiep theo
        $messageNextPanel: $('#message_next'),

        // div chatter next: tai trang chatter tiep theo
        $chatterNextPanel: $('#chatter_next'),

        // div chua thong tin chi tiet ve chatter, nhom chat
        $chatterInfoPanel: $('#btalk_body_detail'),

        // Cay phong ban tren eGov
        $treeDepartmentPanel: $('#tree'),
        $treeOnlinePanel: $('#treeOnline'),

        // Textbox tim kiem
        $searchTxt: $("#searchTxt"),

        /*
         * TamDN - 26/12/2016
         * Chi cho phep tim kiem khi da tai xong danh sach chat gan day
         */
        enableSearch: function () {
            this.$searchTxt.removeAttr('disabled');
        },

        /* Event handler
        ---------------------------------------------------------------------- */

        sendMessageBtn_MouseDownEvent_Hanlder: function (e) {
            // Chua chon nguoi nhan tin
            if (!this.CURRENTCHATTER) {
                this.$messageTxt.focus();
                return;
            }
            this.sendMessage();
        },

        sendMessageBtn_MouseUpEvent_Hanlder: function () {
            // Xu ly tinh toan lai chieu cao cua textbox go tin nhan. Bat buoc dung keyup.
            this.$messageTxt.focus();
        },

        // Keypress: Su kien xay ra ki 1 ki tu duoc chen vao messageTxt.
        // Do do, khi bam cac phim ctrl, shift, alt...up/down, left/right, f1..f12...
        // tuc cac phim khong tao ra ki tu thi se khong khoi phat su kien nay
        messageTxt_KeypressEvent_Hanlder: function (e) {
            // CuongNT - 18/07/2016: Chuyển gửi trạng thái đang gõ từ keyup sang keypress.
            this.sendComposing();

            var keycode = e.which;
            // Enter + khong giu phim shift
            if (keycode == enterKey && !e.shiftKey) {
                // Chua chon nguoi nhan tin
                if (!this.CURRENTCHATTER) {
                    return;
                }
                this.sendMessage();
                // Dung xu ly su kien enter tai day. Fix loi bi xuong dong sau khi enter gui noi dung chat.
                e.preventDefault();
            }
        },

        moveCurrentChatter: function (keycode) {
            $("#tab_history").updown("li", 'selected', keycode);
            // Da xu ly delay 1 khoang thoi gian trong su kien click cua chatterView roi,
            // nen cho nay khong so bi xay ra viec tai lich su chat lien tuc
            $('#tab_history li.selected').trigger('click');
        },

        messageTxt_KeydownEvent_Hanlder: function (e) {
            var keycode = e.which;
            // Bo qua khong xu ly phim len, xuong de giu contro trong textbox seache luon co dinh.
            switch (keycode) {
                case upKey:// up
                case downKey:// down
                    // TODO: Phai bo sung dieu kien:
                    // + Dang o tab Hoi thoai
                    // + Co nguoi trong tab lich su chat
                    var tabHistory = $('li[data-target="tab_history"]');
                    if (e.ctrlKey && $('#tab_history ul.bt-chatter>li.selected').length == 1 &&
                            tabHistory.hasClass('active')) {
                        this.moveCurrentChatter(keycode)
                        return;
                    }
                    break;
            }
            // DamBV
            this.removeQuoteMessage(e);
        },

        /*
         * Xu ly su kien paste noi dung chat vao textbox chat
         * Update DamBV: 28/02/2017 : Xu li encode trong mot so truong hop.
         */
        messageTxt_PasteEvent_Hanlder: function (e) {
            // Ctrl + V --> paste noi dung vao textbox chat.
            // Right click --> Chon "paste".
            // https://www.w3.org/TR/clipboard-apis/#the-paste-action

            var pastedData = e.originalEvent.clipboardData.getData('text');
            var isquote = false;
            // Kiem tra xem co ton tai quote khong hay chi la dang text.
            if (btalk.temporaryQuote == null) {
                btalk.insertTextAtCursor(pastedData);
                return false;
            } else {
                // Xu li du lieu (emotion)
                var contentMsgQuote = this.replaceEmotionByText(btalk.temporaryQuote._contentMsg);

                // Kiem tra du lieu copy co trung voi du lieu de tao quote thi se xu li.
                if (contentMsgQuote == pastedData) {
                    btalk.temporaryQuote._contentMsg = btalk.emoticon.process(pastedData);
                    btalk.temporaryQuote.showQuote();
                    this.CURRENTCHATTER.temporaryQuote = btalk.temporaryQuote;
                    isquote = true;
                    btalk.temporaryQuote = null;
                }

                // Neu chi la dang text thi thoi.
                if (!isquote) {
                    btalk.insertTextAtCursor(pastedData);
                }
            }
            e.preventDefault();
        },

        /*
         * Su kien click nut "Xem chi tiet thong tin Chatter/Group chat"
         */
        chatterInfoBtn_ClickEvent_Hanlder: function () {
            if (this.$chatterInfoPanel.is(":visible")) {
                // Close Chatter Info
                this.$middlePanel.css('width', '100%');
                this.$chatterInfoPanel.hide();
                $('#chatterInfoBtn span').removeClass('selected-button');
                //  DamBV - luu trang thai
                btalk.cache.Value_OpenChatterInfo = false;
                btalk.cache.writeCacheStorage(btalk.cache.Name_OpenChatterInfo, btalk.cache.Value_OpenChatterInfo);
            } else {
                // Open Chatter Info
                this.$middlePanel.css('width', '');
                this.$chatterInfoPanel.show();

                $('#chatterInfoBtn span').addClass('selected-button');

                //  DamBV - luu trang thai trong cookies
                btalk.cache.Value_OpenChatterInfo = true;
                btalk.cache.writeCacheStorage(btalk.cache.Name_OpenChatterInfo, btalk.cache.Value_OpenChatterInfo);
            }
        },

        /*
         * Su kien click vung "Thong tin chi tiet Chatter/Group chat"
         */
        chatterInfoPanel_MousedownEvent_Hanlder: function (e) {
            // Danh sach tin nhan -> disable
            this.$messagePanel.removeClass('enableSelectText').addClass('disableSelectText');
            // Chi tiet user/group chat -> enable
            this.$chatterInfoPanel.removeClass('disableSelectText').addClass('enableSelectText');
        },

        /*
         * Enable select text trong panel danh sach tin nhan cua chatter
         */
        messagePanel_MousedownEvent_Hanlder: function () {
            // Danh sach tin nhan -> enable
            this.$messagePanel.removeClass('disableSelectText').addClass('enableSelectText');
            // Chi tiet user/group chat -> disable
            this.$chatterInfoPanel.removeClass('enableSelectText').addClass('disableSelectText');
        },

        /*
         * UPLOAD/DOWNLOAD FILE
         */
        inputFileBtn_ChangeEvent_Hanldler: function (e) {
            var files = e.target.files;
            this.sendFiles(files);
            //Set lai val de co the gui cung mot file do o lan sau
            $(".inputfile").val("");
            //e.defaulte
        },

        inputFileBtn_FocusEvent_Hanldler: function (e) { e.target.classList.add('has-focus'); },

        inputFileBtn_BlurEvent_Hanldler: function (e) { e.target.classList.remove('has-focus'); },

        /*
         * Menu chinh
         */
        menuBtn_ClickEvent_Handler: function (e) {
            if ($(e.currentTarget).find('span').hasClass("icon-configuration")) {
            } else if ($(e.currentTarget).find('span').hasClass("icon-about")) {
            } else if ($(e.currentTarget).find('span').hasClass("icon-accessibility")) {
            } else if ($(e.currentTarget).find('span').hasClass("icon-help")) {
            } else if ($(e.currentTarget).find('span').hasClass("icon-logout")) {
                this.logout();
            }
        },

        // ** searchTxt * Textbox Search: Tim kiem nguoi va nhom
        activeTabName: "",
        searchTxt_FocusEvent_Handler: function (e) {
            // Boi chon tu khoa tim kiem
            this.$searchTxt.select();
            // Luu trang thai tab dang active hien tai
            if ($('.btalk-tab .tab-pane.active').attr('id') != "tab_search") {
                this.activeTabName = $('.btalk-tab .tab-pane.active').attr('id');
            }

            // Forcus tab search
            forcusTab("tab_search");

            if (this.$searchTxt.val() === '') {
                this.$searchTxt.keyup();
            }
        },

        searchTxt_BlurEvent_Handler: function (e) {
            forcusTab(this.activeTabName);
        },

        searchTxt_KeydownEvent_Handler: function (e) {
            var keycode = e.which;

            /*
			TamDN - 22/12/2016
			Neu click phim Esc thi focus vao o chat
			*/
            if (keycode == 27) {
                $("#searchTxt").val("");
                this.$messageTxt.focusAtEnd();
            }

            $("#tab_search").updown("li", 'selected', keycode);

            switch (keycode) {
                case upKey:
                case downKey:
                    // Bo qua khong xu ly phim len, xuong de giu contro trong textbox seache luon co dinh.
                    return false;
                    break;
            }
        },

        searchTxt_KeyupEvent_Handler: function (e) {
            var keycode = e.which;
            switch (keycode) {
                case enterKey:
                    // Enter khi dang chon chatter => mo chat voi chatter nay
                    if ($('#tab_search li.selected').length > 0) {
                        // Clear textbox search chua tu khoa vua go
                        this.$searchTxt.val('');

                        forcusTab('tab_history');
                        // Dat lai active tab
                        this.activeTabName = 'tab_history';

                        $('#tab_search li.selected').trigger('click');
                        $('#messageTxt').focus();
                    }
                    break;

                    // Bo qua khong xu ly phim di chuyen
                case leftKey:// left
                case upKey:// up
                case rightKey:// right
                case downKey:// down
                    return;
                    break;
                default:
                    // Other key (dang go tim chatter) => Tim kiem chatter
                    // [TODO] xu ly ket qua seach nay sort theo tieu chi ai hay chat hon thi hien len dau
                    var key = e.currentTarget.value;
                    this.handleDefaultEventSearchUser(key);

                    break;
            }
            return;
        },

        handleDefaultEventSearchUser: function (key) {
            var rosters = btalk.ROSTER.search(key);

            btalk.CHATTERSEARCH.reset(null);

            $($("#tab_search ul")[0]).html('');

            for (var i = 0; i < rosters.length; i++) {
                var chatter = btalk.CHATTERS.findWhere({ jid: rosters[i].jid });
                // by TamDN - Neu la group thi moi hien thi fullname
                var fullname = null;
                if (rosters[i].type == btalk.CHATTYPE.GROUPCHAT) {
                    fullname = rosters[i].fullname;
                    if (!fullname || fullname == "")
                        fullname = rosters[i].chatterM.get("fullname");
                }
                var newchatter = new btalk.model.ChatterSearch({
                    label: "",
                    type: rosters[i].type,
                    jid: rosters[i].jid,
                    account: rosters[i].name,
                    fullname: fullname ? fullname : null,
                    online: rosters[i].status == 'available',
                    messagecount: 0,
                    chatter: chatter,
                    rosterUser: rosters[i],
                });

                btalk.CHATTERSEARCH.add(newchatter);
            }

            // Xu ly hien goi y tao nhom chat + tim kiem noi dung chat
            if (rosters.length <= 0) {
                var allInRosters = true;

                // Xu ly loai bo trung ten trong members, bo member rong... tai day
                var members = key.split(/[\s,;]+/);
                var newMembers = [];
                var memberStrs = "";
                var nameArr = [];
                var _nullMem = 0;
                for (var i = 0; i < members.length; i++) {
                    if (members[i].trim() == "") {
                        _nullMem = _nullMem + 1;

                        // Loai bo ki tu _ va ", " o cuoi
                        if (i == members.length - 1) {
                            if (memberStrs.lastIndexOf(", ") == memberStrs.length - ", ".length) {
                                memberStrs = memberStrs.substr(0, memberStrs.length - ", ".length);
                            }
                        }
                        continue;
                    }

                    // Neu thanh vien nay duoc them lap thi return
                    if (nameArr.indexOf(members[i]) != -1) {
                        continue;
                    }

                    var user = btalk.ROSTER.getUserByName(members[i]);
                    // TODO Xu ly loai bo user trung not o day
                    if (!user) {
                        allInRosters = false;
                        break;
                    }

                    nameArr.push(members[i]);
                    newMembers.push(user);
                    memberStrs = memberStrs + members[i].trim();
                    if (i < members.length - 1) {
                        memberStrs = memberStrs + ", ";
                    }
                }

                // Neu khong co currentuser thi them vao mac dinh lam thanh vien chat nhom
                if (nameArr.indexOf(btalk.ROSTER.currentuser.name) == -1) {
                    newMembers.unshift(btalk.ROSTER.currentuser);
                    memberStrs = btalk.ROSTER.currentuser.name + ", " + memberStrs;
                }

                members = newMembers;
                var group_jid = btalk.cm.getGroupJID();

                if (allInRosters === true) {
                    var newchatter2 = new btalk.model.ChatterSearch({
                        label: "Tạo nhóm (" + newMembers.length + " người): ",
                        type: "newgroupchat",
                        jid: group_jid,
                        account: group_jid.split("@")[0],
                        key: newMembers,
                        online: true,
                        messagecount: 0,
                        lastmessage: memberStrs,
                    });
                    btalk.CHATTERSEARCH.add(newchatter2);
                }

                var newchatter1 = new btalk.model.ChatterSearch({
                    label: "Tìm kiếm: ",
                    type: "search",
                    key: key,
                    online: true,
                    messagecount: 0,
                    lastmessage: key.trim(),
                });
                btalk.CHATTERSEARCH.add(newchatter1);
            }

            $('#tab_search li:first').addClass('selected');
        },

        events: {
            // Menu (Huong dan, ... Thoat)
            "click .dropdown-menu li": "menuBtn_ClickEvent_Handler",

            /** messageTxt - Textbox tin nhan */

            // Gui message
            "keypress #messageTxt": "messageTxt_KeypressEvent_Hanlder",

            // Auto resize middle panel           
            // Xu ly phim up/down di chuyen tren danh sach chatters
            "keydown #messageTxt": "messageTxt_KeydownEvent_Hanlder",

            // Paste: xu ly chi lay text
            "paste #messageTxt": "messageTxt_PasteEvent_Hanlder",

            /** sendMessageBtn - Nut gui tin nhan */
            // Gui message
            "mousedown #sendMessageBtn": "sendMessageBtn_MouseDownEvent_Hanlder",

            // Tinh lai chieu cao textbox go tin nhan. Bat buoc dung keyup.
            "mouseup #sendMessageBtn": "sendMessageBtn_MouseUpEvent_Hanlder",

            // Dong/mo panel xem thong tin chi tiet chatter
            "click #chatterInfoBtn": "chatterInfoBtn_ClickEvent_Hanlder",

            /** inputfile - Gui file */
            "change .inputfile": "inputFileBtn_ChangeEvent_Hanldler",
            "focus .inputfile": "inputFileBtn_FocusEvent_Hanldler",
            "blur .inputfile": "inputFileBtn_BlurEvent_Hanldler",

            /** searchTxt - Tim kiem, tao moi hoi thoai 1-1, nhom hoi thoai */
            "focus #searchTxt": "searchTxt_FocusEvent_Handler",
            "blur #searchTxt": "searchTxt_BlurEvent_Handler",
            "keydown #searchTxt": "searchTxt_KeydownEvent_Handler",
            "keyup #searchTxt": "searchTxt_KeyupEvent_Handler",
            "click #btnCreateGroup": "visibleFormCreateGroup",

            // TamDN - Them phan xu ly drag-drop file
            "dragenter #btalk_body_content": "dragenterToUploadFile",
            "dragleave #btalk_body_content": "dragleaveToUploadFile",
            "dragover #btalk_body_content": "dragoverToUploadFile",
            "drop #btalk_body_content": "dropToUploadFile",

            // DamBV - 12012017 - Them xu ly phần tìm kiếm trong cuộc hội thoại.
            "click #btn_searchMsg": "showViewSearchMsg",
            "click #close_search": "hiddenViewSearchMsg",
            "click #submit_search": "findMessageInConversation",
            "keypress #search_text": "findMessageInConversation",
            "click #next_result": "nextResultFindMessage",
            "click #previous_result": "previousResultFindMessage",
            "click #load_up_message": "loadUpMessages",
            "click #load_down_message": "loadDownMessages",

            // DamBV - 16/01/2017 - Xu li click vao avatar trong thread chat de bat dau chat voi nguoi moi.
            "click .btalk-userchat-content .chat-detail-row-avatar img": "selectAccountChat",

            // DamBV-17/01/2017 - Goi den notify của egov.
            "click #btalk_body_content": "callEgovNotify",

            // DamBV- 13/02/2017 - Xu li su kien kich setting
            "click #chatter_small": "setHeightChatterSmall",
            "click #chatter_default": "setHeightChatterDefault",

            // Xu li bat/tat am bao.
            "click #turn_off_sound": "setTurnOffSound",
            "click #turn_on_sound": "setTurnOnSound",

            // Lang nghe su kien an cac thong bao tooltip khi co tin chat moi den.
            "click #notify_newmessage_chatter": "hideTooltipNewMessageBelowChatter",
            "click #notify_newmessage_textput": "hideToolTipNewMessageOnTextInput",

            "click #back_content_chatter_mobile": "egovMobileBackToListHistory",

            // Bat su kien khi click vao tung tab, de luu cache.
            "click #tabs_option_id": "setCacheTabSelected",

            // Added DamBV 17/04/2018 : Xu li IDLE
        },

        /* Khoi tao giao dien  */
        initialize: function (options) {
            // Dang ki xu ly cac su kien
            this.registerHanders();

            // Khoi tao thu vien emoticon (:)), :V... )
            this.initEmoticons();

            // Khoi tao thu vien canh bao
            this.initNotification();

            this.initShareFunction();

            this.checkStatusOpenChatterInfo();

            // Load cache
            this.initCache();

            // Cai dat thoi gian hien thong bao khi co tin nhan toi doi voi tu mot cuoi hoi thoai.
            var that = this;
            window.onclick = function (event) {
                if (event.target.id == "btn_accept_setting_notify") {
                    that.setTimeActiveNotify();
                }
                if (event.target.id == "button_sysn" || event.target.id == "icon_text") {
                    btalk.view.ChatterInfoView.prototype.downloadMoreSharedFile();
                }
            }

            this.checkIdleInterval = setInterval(this.checkIdle, 10 * 1000);

            this.takeImageMobile();
        },

        // Load cache tu storage: cau hinh chung cho.
        initCache: function () {
            // Kich thuoc chattter
            var _statusChatter = btalk.cache.readCacheStorage("statuschatter");
            if (_statusChatter == null || _statusChatter == "default") {
                btalk.cache.statusSizeChatter = "default";
                btalk.cache.heightDefaultChatter = 40;
                var select_chatter_default = document.getElementById('select_chatter_default')
                if (select_chatter_default != null) {
                    select_chatter_default.style.display = 'block';
                }
                var select_chatter_small = document.getElementById('select_chatter_small')
                if (select_chatter_small != null) {
                    select_chatter_small.style.display = 'none';
                }
            } else {
                btalk.cache.statusSizeChatter = _statusChatter;
                btalk.cache.heightDefaultChatter = 35;
                var select_chatter_default = document.getElementById('select_chatter_default')
                if (select_chatter_default != null) {
                    select_chatter_default.style.display = 'none';
                }
                var select_chatter_small = document.getElementById('select_chatter_small')
                if (select_chatter_small != null) {
                    select_chatter_small.style.display = 'block';
                }
            }

            // Am bao tin nhan.
            var _statusSoundMsg = btalk.cache.readCacheStorage(btalk.cache.Name_SoundMessage);
            if (_statusSoundMsg == "active") {
                btalk.cache.Key_SoundMessage = "active";
                var select_on_sound = document.getElementById('select_on_sound');
                if (select_on_sound != null) {
                    select_on_sound.style.display = 'block';
                }
                var select_off_sound = document.getElementById('select_off_sound');
                if (select_off_sound != null) {
                    select_off_sound.style.display = 'none';
                }
            } else {
                btalk.cache.Key_SoundMessage = _statusSoundMsg;
                var select_on_sound = document.getElementById('select_on_sound');
                if (select_on_sound != null) {
                    select_on_sound.style.display = 'none';
                }
                var select_off_sound = document.getElementById('select_off_sound');
                if (select_off_sound != null) {
                    select_off_sound.style.display = 'block';
                }
            }

            var _statusTabSelected = btalk.cache.readCacheStorage("NameTabSelected");
            if (_statusTabSelected !== null) {
                forcusTab(_statusTabSelected);
            } else {
                forcusTab("tab_history");
            }

        },

        //  DamBV - 17/01/2017: Ban su kien notify cho egov
        callEgovNotify: function () {
            if (!btalk.ChatBmail && this && this.CURRENTCHATTER && this.CURRENTCHATTER.attributes) {
                var jid = this.CURRENTCHATTER.get('jid');
                if (typeof top.eGovReadChat === 'function') {
                    top.eGovReadChat(jid);
                }
            }
        },

        counterDragDrop: 0,
        /*
         * TamDN - Them ham keo tha file khi gui file
         */
        dropToUploadFile: function (e) {
            e.stopPropagation();
            e.preventDefault();
            this.counterDragDrop = 0;
            $("#btalk_body_content").removeClass("dragging");
            $("#upload_file_area").removeClass("dragging");
            var files = e.originalEvent.dataTransfer.files;
            this.sendFiles(files);
        },

        dragenterToUploadFile: function (e) {
            this.counterDragDrop += 1;
            e.stopPropagation();
            e.preventDefault();
            $("#btalk_body_content").addClass("dragging");
            $("#upload_file_area").addClass("dragging");
        },

        dragleaveToUploadFile: function (e) {
            this.counterDragDrop -= 1;
            if (this.counterDragDrop === 0) {
                $("#btalk_body_content").removeClass("dragging");
                $("#upload_file_area").removeClass("dragging");
            }
        },

        dragoverToUploadFile: function (e) {
            e.stopPropagation();
            e.preventDefault();
            $("#btalk_body_content").addClass("dragging");
            $("#upload_file_area").addClass("dragging");
        },

        isReady: false,
        scrollBottomOfMessageList: 0,

        registerHanders: function () {
            if (this.isReady) { return; }
            this.isReady = true;

            // Layout
            window.chatLayout = $('#btalk_body_content').layout({
                resizable: false,
                closable: false,
                spacing_closed: 0,
                spacing_open: 0,

                // Xử lý giữ nguyên trạng thái scroll nội dung chat khi resize giao diện
                onresize_start: function () {
                    // Lưu lại vị trí scroll trước khi resize.
                    this.scrollBottomOfMessageList = this.scrollBottom();
                }.bind(this),

                // Xử lý giữ nguyên trạng thái scroll nội dung chat khi resize giao diện
                onresize_end: function () {
                    // Set lại vị trí scroll sau khi resize xong
                    // Dung gia tri 5 de fix loi thinh thoan inBottom = 1|2.... chua ro do dau
                    if (this.scrollBottomOfMessageList <= 5) {
                        this.scrollBottom(0);
                    } else {
                        this.scrollBottom(this.scrollBottomOfMessageList);
                    }
                }.bind(this),

                //center__size: 178,
                center__paneSelector: ".btalk-userchat-content",

                north__size: .50,
                north__paneSelector: ".bt-sl-video-call",
                north__initHidden: true,

                south__size: 59,
                south__paneSelector: ".btalk-textinput",
                //south__onresize: "chatLayout.resizeAll",
            });

            // Resize lại giao diện khi textbox gõ nội dung resize
            this.$messageTxtOuter.on("mresize", function () {
                window.chatLayout.sizePane("south", this.$messageTxtOuter.innerHeight());
            }.bind(this));

            // Backbonejs event hander
            // ----------------------------------------------

            // Model danh sach user chat gan nhat
            this.listenTo(btalk.CHATTERS, 'add', this.addChatter.bind(this));

            // Model danh sach cac cuoc hoi thoai theo ngay
            this.listenTo(btalk.CONVERSATIONDAYS, 'add', this.addConversationDay.bind(this));
            this.listenTo(btalk.CONVERSATIONDAYS, 'remove', this.removeConversationDay.bind(this));
            this.listenTo(btalk.CONVERSATIONDAYS, 'reset', this.resetConversationDays.bind(this));

            // Model danh sach tim kiem chatter
            this.listenTo(btalk.CHATTERSEARCH, 'add', this.addChatterSearch.bind(this));

            // VIEW TAI THEM CHATTER
            this.$chatterNextPanel.append(btalk.CHATTERNEXTVIEW.render().el);

            // VIEW TAI THEM TIN NHAN
            this.MESSAGENEXT = new btalk.model.MessageNext();
            this.MESSAGENEXTVIEW = new btalk.view.MessageNextView({ model: this.MESSAGENEXT });
            this.$messageNextPanel.append(this.MESSAGENEXTVIEW.render().el);
            this.MESSAGENEXT.hidden();

            // SCROLL DANH SACH USER, TIN NHAN
            // DAMBV
            this.$messagePanelOuter.scroll(this.loadNextPageOfMessage.bind(this));
            this.$chatterPanelOuterOuter.scroll(this.loadNextPageOfChatter.bind(this));

            // ! Init event hanlder - Xay ra trong qua trinh dang nhap
            // -------------------------------------------------------

            // Tra ve danh sach ban be (danh sach chatter)
            btalk.cm.registerHandler('_handle_iq_roster_result', this.handleRosters.bind(this));

            // Tra ve danh sach online.
            btalk.cm.registerHandler('_handle_iq_mobile_result', this.handleUsersOnline.bind(this));

            // Tra ve danh sach chatter
            btalk.cm.registerHandler('_handle_iq_list_result', this.handleChatters.bind(this));

            // Xay ra khi toan bo qua trinh login thuc hien xong
            btalk.cm.registerHandler('_handle_logined', this.handleLogined.bind(this));

            // presence
            btalk.cm.registerHandler('online', this.handleOnline.bind(this));
            btalk.cm.registerHandler('offline', this.handleOffline.bind(this));
            btalk.cm.registerHandler('statusIDLE', this.handleStatusIDLE.bind(this));

            // message
            // Tra ve danh sach tin nhan tu lich su cua 1 chatter
            btalk.cm.registerHandler('handle_iq_retrieve_result', this.handleArchiveMessages.bind(this));

            // Tra ve tin nhan do nguoi khac gui toi
            btalk.cm.registerHandler('message', this.handleMessage.bind(this));

            // group chat
            btalk.cm.registerHandler('group_invite', this.handleGroupInvite.bind(this));
            btalk.cm.registerHandler('group_member_join', this.handleGroupMemberJoin.bind(this));
            btalk.cm.registerHandler('group_member_left', this.handleGroupMemberLeft.bind(this));

            // history/archive
            btalk.cm.registerHandler('_handle_iq_remove_result', this.handleArchiveRemove.bind(this));
            btalk.cm.registerHandler('_handle_ip_search_message_result', this.handleArchiveMessageSearch.bind(this));
            btalk.cm.registerHandler('_handle_ip_get_message_with_time', this.handleArchiveMessageWithTime.bind(this));

            // connection
            btalk.cm.registerHandler('ondisconnect', this.handleDisconnected.bind(this));
            btalk.cm.registerHandler('onconnect', this.handleConnected.bind(this));

            /*
             * Update DamBV -20/03/2017 : Lang nghe su kien khi scroll de go bo thong bao new messsage
             * trong khung chatter va khung hien thi tin chat
             */
            var _that = this;
            this.$chatterPanelOuterOuter.on('scroll', function () {
                _that.canHidenTooltipNewMessage();
            });

            this.$messagePanelOuter.on('scroll', function () {
                _that.canHidenToolTipNewMessageOnTextInput();
            });

            this.$messagePanelOuter.on('scroll', function () {
                _that.canShowAvatarWhenScrollHistory();
            });

            /*
             *  Trong truong hop trinh duyet vao FireFox thi khi khoi tao datepicker
             * do FireFox khong ho tro the input kieu date
             */
            $('#search_enddate').datepicker({ dateFormat: 'dd/mm/yy' });
            $('#search_startdate').datepicker({ dateFormat: 'dd/mm/yy' });
        },

        /**
         * Init emoticon
         */
        initEmoticons: function () {
            // Added DamBV
            $(".action-input-chat #btalk_emotion").append(btalk.emoticon.render());
            $(".message-button #btalk_emotion").append(btalk.emoticon.render());

            $(".btalk-emoticon li").bind("click", function (e) {
                var emoticon_id = parseInt($(e.currentTarget).attr('data-value'));
                var _symbol = btalk.emoticon.getSymbol(emoticon_id);
                // Neu textbox chat khong co noi dung chat => gui emoticon di luon
                if (!this.$messageTxt.text()) {
                    //this.sendMessage2(_symbol).bind(this);
                    this.sendMessage2(_symbol);
                }
                    // Nguoc lai thi chen vao vi tri contro hien tai
                else {
                    this.$messageTxt.focus();
                    // Luu y luon them khoang trang khi chen emoticon
                    btalk.insertTextAtCursor(" " + btalk.htmlDec(_symbol) + " ");
                }
            }.bind(this));
        },

        /**
         * Init notification
         */
        initNotification: function () {
            btalk.ntf.init({
                onclick: this.notificationClick.bind(this),
                getIcon: function (account) {
                    return btalk.view.getAvatar([account]);
                },
                closeAfter: 15000,
            });
        },

        /**
         * Init Share Function: between views
         */
        initShareFunction: function () {
            _.bindAll(this, "preventScrolling");
            this.shareEvents.bind("preventScrolling", this.preventScrolling);

            _.bindAll(this, "changeCurrentChatter");
            this.shareEvents.bind("changeCurrentChatter", this.changeCurrentChatter);

            _.bindAll(this, "moveChatterToTop");
            this.shareEvents.bind("moveChatterToTop", this.moveChatterToTop);
        },

        // Share Function:
        // Duoc goi tu: btalk.view.message.js
        scrollBottomOfMessagePanel: 0,
        preventScrolling: function () {
            this.$messagePanelOuter.scrollBottom(this.scrollBottomOfMessagePanel);
        },

        scrollBottom: function (position) {
            if (typeof position === 'number') {
                this.$messagePanelOuter.scrollBottom(position);
                this.scrollBottomOfMessagePanel = position;
            } else {
                return this.$messagePanelOuter.scrollBottom();
            }
        },

        saveScrollBottom: function () {
            this.scrollBottomOfMessagePanel = this.$messagePanelOuter.scrollBottom();
        },

        // Share Function:
        // Duoc goi tu btalk.view.chatter.js
        setMessageTxt: function (body) {
            this.$messageTxt.html(body);
            this.$messageTxt.focusAtEnd();
            this.$messageTxtOuter.scrollBottom(0);
        },

        // Share Function: Thay doi current chatter
        changeCurrentChatter: function (newCurrentChatter) {
            // Neu truoc khi click dang chon chatter khac
            if (this.CURRENTCHATTER instanceof Backbone.Model) {
                //this.CURRENTCHATTER.view.$el.removeClass("selected");
                var lastTypedText = this.$messageTxt.html().trim();

                // Reset isAtTop de lan selected sau ma chat lan dau se move len dau
                // Luu lai noi dung chat dang go chua gui di neu co
                this.CURRENTCHATTER.set({
                    isselected: false,
                    isAtTop: false,
                    lastTypedText: lastTypedText
                });
            }

            // ** NEW CURRENT CHATTER **
            this.CURRENTCHATTER = newCurrentChatter;

            // Gan lai current chatter cho MessageNext Model de thuc hien tai lich su chat
            this.MESSAGENEXT.currentChatter(newCurrentChatter);

            // Rebind lai danh sach lich su tin nhan cua chatter vua click chon
            this.resetConversationDaysView();

            // Restore lai noi dung chat dang go truoc do
            this.setMessageTxt(newCurrentChatter.get('lastTypedText'));

            // An typing cua chatter cu di neu co
            this.hideTyping();

            this.displayViewContentConversation();

            // Neu dang la trang thai hien thi ket qua tim kiem thi khi chuyen chatter se an bo ket qua day luon.
            if (btalk.SEARCH.IS_STATUS_SEARCH == true) {
                this.hiddenViewSearchMsg();
            }
        },

        // ! Connection
        handleDisconnected: function () {
            console.log("DamBV - view app - Disconnected");
        },

        handleConnected: function () {
            console.log("DamBV - view app - Connected");
        },

        // Tinh so luong chatter de dam bao luon full chieu cao.
        getNumberChatterHistoryByHeight: function () {
            var maxChatter = null;
            var _heightTabHistory = $('#tab_history').height();
            if (_heightTabHistory) {
                btalk.cache.heightTabHistory = _heightTabHistory;
                maxChatter = parseInt(_heightTabHistory / btalk.cache.heightDefaultChatter);
                return maxChatter;
            }
            return 0;
        },

        // Share Function: Thuc hien ham nay sau khi da set lai this.CURRENTCHATTER ve chatter moi
        resetConversationDaysView: function () {
            // DamBV - The hien cho muc tim kiem.
            this.turnOffComponentViewSearch();

            // CuongNT - 9/4/2016: Fix loi print nham lich su chat cua nguoi nay sang thread nguoi khac
            // Dung printMessage thread hoi thoai cua chatter truoc do neu co
            clearTimeout(this.printMessageTimeout);

            // Dung tien trinh danh dau trang thai da doc tin khi window focus neu co
            clearTimeout(this.conversationDaysReadAllTimeout);

            // reset panel danh sach tin nhan
            btalk.CONVERSATIONDAYS.reset(null);
            this.$messagePanel.html("");
            this.scrollBottom(0);

            // this.CURRENTCHATTER nay la chatter moi duoc click, Ve len giao dien tin tu cache neu co
            this.checkChatterCacheMessage();

            // Chat 1-1, group chat
            var typeCurrentChatter = this.CURRENTCHATTER.get("type");
            if (typeCurrentChatter == btalk.CHATTYPE.CHAT || typeCurrentChatter == btalk.CHATTYPE.GROUPCHAT) {
                this.$chatterInfoPanel.html("");
                var user = btalk.ROSTER.getUserByJID(this.CURRENTCHATTER.get('jid'));

                // CuongNT - 25/3/2016: Chi btalk.roster.eGovUser moi co thong tin ve chuc danh, phong ban
                var _jobtitles = user instanceof btalk.roster.eGovUser ? user.getJobTitlesStr() : "";
                var _depts = user instanceof btalk.roster.eGovUser ? user.getDepartmentsStr() : "";

                // CuongNT - 2/12/2016: moi them de luu loai roster luon de dung sau nay
                if (!user.type || user.type == null || user.type === "") {
                    user.type = this.CURRENTCHATTER.get('type');
                }

                var newModelChatterInfo = new btalk.model.ChatterInfo({
                    account: user.name,
                    fulljid: user.fulljid,
                    mobile: user.mobile,
                    jobtitles: _jobtitles,
                    departments: _depts,
                    online: user && user.status == 'available' ? true : false,
                    statusmessage: user.acs ? user.acs.fullreason : "[Trang thai...]",
                    fullname: user.name,
                    members: user.members,
                    type: user.type
                });

                //  DamBV - 30/12/2016 : Bổ sung thuộc tính fullname cho chatterInfo.
                if (user.chatterM) {
                    var namegroup = user.chatterM.get('fullname');
                    if (namegroup == "") {
                        namegroup = btalk.CONSTANT.DEFAULT_ROOM_NAME;
                    }
                    newModelChatterInfo.set('fullname', namegroup);
                    newModelChatterInfo.set('type', user.chatterM.get('type'));
                    namegroup = null;
                }

                this.resetInformationCurrentChatter(newModelChatterInfo, user);
            }
        },

        // Ve len giao dien tin tu cache neu co
        checkChatterCacheMessage: function () {
            if (this.CURRENTCHATTER.messageCaches) {
                this.MESSAGENEXT.startLoading();
                this.handleArchiveMessages(this.CURRENTCHATTER.messageCaches, true);

                // CuongNT - 24/8/2016: Kiem tra neu chatter chua tung tai lich su chat => Thuc hien tai
                if (this.CURRENTCHATTER.get("archiveIsLoaded") === false) {
                    this.CURRENTCHATTER.set("archiveIsLoaded", true);
                    this.MESSAGENEXTVIEW.loadNextPageOfMessages();
                }
            } else {
                // Nguoc lai, lay trang lich su tin nhan dau tien tu server
                this.CURRENTCHATTER.set("archiveIsLoaded", true);
                this.MESSAGENEXTVIEW.loadNextPageOfMessages();
            }
        },

        //  DamBV - 22/06/2017: Lam moi lai thong tin chi tiet cua chatter.
        resetInformationCurrentChatter: function (newModelChatterInfo, user) {
            if (this.CHATTER_INFO_MODEL) {
                this.CHATTER_INFO_MODEL = null;
                btalk.cm.unregisterHandler('_handle_getMembersGroup');
                btalk.cm.unregisterHandler('_handle_addMembersGroup');
                btalk.cm.unregisterHandler('_handle_removeMemberGroup');
                btalk.cm.unregisterHandler('_handle_oMsg_configMemberGroup');
            }

            this.CHATTER_INFO_MODEL = newModelChatterInfo;
            var chatterInfoView = new btalk.view.ChatterInfoView({ model: newModelChatterInfo });

            this.$chatterInfoPanel.html(chatterInfoView.render().el);

            //Vẽ lại thông tin thành viên trong nhóm
            chatterInfoView.loadGroupMembers();

            // Hien thi fullname chatter tren giao dien
            var chatter = btalk.ROSTER.getUserByJID(this.CURRENTCHATTER.get('jid'));
            var _fullname = "";
            if (chatter.chatterM && chatter.chatterM.get('type') == btalk.CHATTYPE.GROUPCHAT) {
                _fullname = chatter.chatterM.get('fullname');
            } else {
                _fullname = chatter.fullname;
            }
            $("span[data-field='chatterFullname']").text(_fullname);
            $("span[data-field='chatterAcsReason']").text(user.acs ? user.acs.fullreason : "");

            // Gan view infor de xu li cho mot so truong hop tren mobile.
            this.CurrentChatterViewInfo = chatterInfoView;
        },

        /* DEPARTMENT TREE - CAY PHONG BAN */
        _clickTreeNode: function (event, data) {
            var aE = $(event.target).closest("a");
            var isUser = $(event.target).attr('rel') === 'people';
            if (isUser != true) {
                return;
            }
            var key = aE.text().toLowerCase();
            this._chatWithAccount(key)
            // Đã bỏ phần tạo cuộc chat vs accoun khac thay bang _chatWithAccount.
        },

        /*
         *  DamBV - 14/12/2016 : chat tao cuoc hoi moi khi chat voi 1 account khac.
         * Input: account.
         */
        _chatWithAccount: function (jidAccount) {
            var _acount = jidAccount + "@" + btalk.config.CM.xmpp.domain;

            // Khong cho phep chat voi chinh minh.
            var _auth = btalk.auth.getJID();
            if (_acount === _auth) return;

            var rosterUser = btalk.ROSTER.getUserByJID(_acount);
            if (!rosterUser) {
                rosterUser = btalk.ROSTER.addUser(_acount);
            }
            forcusTab('tab_history');
            this.$messageTxt.focus();

            // [TODO] Can gop viec tim kiem + click cay de chat ve 1 ham duy nhat
            // [TODO] xu ly lap code tai cho nay cung voi trong chatter view
            var chatter = rosterUser.chatterM; //btalk.CHATTERS.findWhere({jid: jid});
            if (rosterUser.chatterM) {
                chatter.set({ isselected: true });
            } else {
                // [TODO] xem chuyen viec nay ra thuc hien tren appView
                // Xu ly sao cho khi chat noi dung dau tien thi tao chatter nhu binh thuong duocf
                var newchatter = new btalk.model.Chatter({
                    jid: rosterUser.jid, 			// chat.with
                    account: rosterUser.name, 		// chat.with.split('@')[0]
                    isme: false,					// Neu la tin nhan cua minh gui di
                    type: btalk.CHATTYPE.CHAT,					// chat, groupchat

                    // Tin nhan cuoi cung
                    lasttimestamp: new Date(),		// start.setSeconds(start.getSeconds() + secs);
                    lastmessage: '[Bạn bắt đầu chat với ' + jidAccount + ']',	// Noi dung tin nhan cuoi

                    // Tin nhan chua doc (tin moi, tin offfline)
                    unread: false,					// Trang thai co tin nhan moi chua xem
                    unreadcount: 0,					// So luong tin nhan moi chua xem

                    // true khi duoc click chon tren giao dien
                    isselected: false,
                    fullname: '',					// account || fullname

                    rosterUser: rosterUser,			// Tro ve doi tuong RosterUser tuong ung
                    roster: btalk.ROSTER,			// Tro ve doi tuong Roster: btalk.ROSTER
                    isResultOfSearch: true,
                    online: rosterUser.status == "available",

                    // Gia tri chieu cao cua chatter.
                    statusHeightChatter: btalk.cache.statusSizeChatter,
                });
                btalk.CHATTERS.add(newchatter);
                newchatter.set({ isselected: true, unread: true });
                // Gan lai chatter cho rosterUser
                rosterUser.chatterM = newchatter;
            }
        },

        _treeDeptIsReady: false,
        _treeOnlineIsReady: false,

        _reUpdateOnlineState: function () {
            // TODO: Tam thoi update tai day de dam bao 2 tree ve xong moi xet tiep toi tong so online => Can sua tong quat: kiem soat duoc khi da ve xong cay + da hoan tat qua trinh login thi se thuc hien 1 so viec nhu the nay
            if (this._treeDeptIsReady === true && this._treeOnlineIsReady === true) {
                this.updateOnlineCount();
                this.updateAllOnlineStatus();
            }
        },

        printUserDeptTree: function (iq_roster, response) {
            // [{"UserName":"YeuPT","StartDate":"1/18/2016 1:30:00 PM","FinishDate":"1/18/2016 11:45:00 PM",
            // "RegulationId":"101","RegulationName":"Không phải quẹt thẻ","Reason":"Nghỉ phép: Nghỉ sinh",
            // "RegionName":""}]
            // TODO: Chuyen thong tin cap nhat tu acs vao thuc hien sau cung sau khi login va tai cac thong tin khac thanh cong
            // Va thay doi thoi diep bao trang thai than thien hon
            var acs = [];
            if (btalk.config.CLIENT.ACS_ACTIVE === true) {
                acs = btalk.egov.getACSInfo();
            }

            // Danh sach user can add ban moi
            var newRosters = [];
            // Khoi gan danh sach ban be
            for (var i = 0; i < response.users.length; i++) {
                response.users[i].username = response.users[i].username.trim();
                // Handle loi eGov trong username thinh thoan co @, vd: username = admin@namdinh.gov.vn
                var jid = "";
                if (response.users[i].username.indexOf("@") > 0) {
                    jid = response.users[i].username.toLowerCase().split('@')[0] + '@' + btalk.config.CM.xmpp.domain;
                } else {
                    jid = response.users[i].username.toLowerCase() + '@' + btalk.config.CM.xmpp.domain;
                }

                var user = btalk.ROSTER.getUserByJID(jid);
                if (!user) {
                    user = btalk.ROSTER.addUser(jid);
                }
                user.userid = response.users[i].value;
                user.fullname = response.users[i].fullname;
                user.name == response.users[i].username;
                user.username = response.users[i].username;
                for (var j = 0; j < response.userDeptPoses.length; j++) {
                    if (response.userDeptPoses[j].userid == response.users[i].value) {
                        var _dept = _.find(response.depts, function (dept) {
                            return dept.id == response.userDeptPoses[j].departmentid;
                        });
                        var _jobtitle = _.find(response.jobtitles, function (jobtitle) {
                            return jobtitle.jobTitlesId == response.userDeptPoses[j].jobtitleid;
                        });
                        user.departments.push(
                            {
                                deptId: _dept ? _dept.id : undefined,
                                deptIdext: _dept ? _dept.idext : undefined,
                                deptName: _dept ? _dept.text : undefined,
                                deptFullName: _dept ? _dept.label : undefined,
                                jobTitlesId: _jobtitle ? _jobtitle.jobTitlesId : undefined,
                                jobTitlesName: _jobtitle ? _jobtitle.jobTitlesName : undefined,
                            });
                    }
                }

                // current user
                if (user.jid === btalk.auth.getJID()) {
                    btalk.ROSTER.currentuser = $.extend({}, btalk.ROSTER.currentuser, user);
                }

                // acs
                if (btalk.config.CLIENT.ACS_ACTIVE === true) {
                    var _userAcs = _.find(acs, function (_acs) {
                        return _acs.RegulationId == 6 && _acs.UserName.toLowerCase() == response.users[i].username.toLowerCase();
                    });
                    if (_userAcs) {
                        user.acs = {
                            startDate: _userAcs.StartDate,
                            finishDate: _userAcs.FinishDate,
                            regulationName: _userAcs.RegulationName, // Dropdownlist chon loai nghi phep tren phieu in
                            reason: _userAcs.Reason,
                            fullreason: "Nghi phep tu " + _userAcs.StartDate + " toi " + _userAcs.FinishDate + ". Loai nghi phep: " + _userAcs.RegulationName
                        };
                    }
                }

                // So sanh danh sach ban be tren server VS danh sach lay ben eGov
                // Thuc hien add ban moi neu co, remove ban cu neu khong co
                var item = _.find(iq_roster.query.item, function (_roster) {
                    return _roster.jid == user.jid;
                });
                // Neu user nay khong co tren danh sach ban be => them moi
                if (!item) {
                    newRosters.push(user.jid);
                }
            }
            // Thuc hien add ban cac user moi
            if (newRosters.length > 0) {
                btalk.cm.addRosters(newRosters);
            }

            // Ve cay phong ban. Ve sau khi khoi gan danh sach RosterUser
            this._bindJsTree(this.$treeDepartmentPanel, true, false,
                false, response.depts, response.users, response.userDeptPoses);

            this._bindJsTreeOnline(this.$treeOnlinePanel, true, false,
             false, response.depts, response.users, response.userDeptPoses);
        },

        // "themes", "json_data", "ui", "crrm"
        _plugins: [],
        _nodeAuth: null,
        _getChildrens: function (parentid, hasUser, arrDept, arrUsers, arrDeptUserJobtitles) {
            var children = _.filter(arrDept, function (num) {
                return num.parentid === parentid;
            });
            children = _.sortBy(children, function (dept) {
                return dept.text;
            });

            for (var i = 0; i < children.length; i++) {
                // Loai bo icon folder phia truoc phong ban
                children[i].icon = false;
            }

            var deptUsers = _.filter(arrDeptUserJobtitles, function (dept) {
                return dept.departmentid === parentid;
            });
            if (hasUser) {
                if (deptUsers.length > 0) {
                    var users = [];
                    for (var i = 0; i < deptUsers.length; i++) {
                        var userindept = _.find(arrUsers, function (user) {
                            return user.value === deptUsers[i].userid;
                        });
                        if (userindept) {
                            // Loai bo 1 user co nhieu chuc vu trong 1 phong ban thi chi hien thi 1
                            var check = _.filter(users, function (user) {
                                return user.li_attr.chatter.toLowerCase() === userindept.username.toLowerCase();
                            });
                            if (check.length <= 0) {
                                var imgSrc = btalk.view.getAvatar([userindept.username]);
                                var rosterUser = btalk.ROSTER.getUserByName(userindept.username);
                                var selected = {
                                    //'id': deptUsers[i].idext + '.' + userindept.value,
                                    'parentid': parentid,
                                    'text': userindept.username,
                                    'icon': imgSrc,
                                    "state": {
                                        opened: false
                                    },
                                    'a_attr': {
                                        // <a class='cuongnt online' => de tim va update trang thai online/offline thuan loi hon
                                        'class': rosterUser && rosterUser.status == 'unavailable' ? rosterUser.name + " offline" : rosterUser.name + " online",
                                        'rel': 'people',
                                        'label': userindept.fullname,
                                        'idext': deptUsers[i].idext,
                                    },
                                    'li_attr': {
                                        'id': deptUsers[i].idext + '.' + userindept.value,
                                        'class': 'chatter',
                                        'chatter': userindept.username
                                    }
                                };
                                users.push(selected);
                            }
                        }
                    }
                    users = _.sortBy(users, function (deptUser) {
                        return deptUser.text;
                    });
                }
            }
            if (users && users.length > 0) {
                children = children.concat(users);
            }

            //  DamBV - 24/04/2017 : Tìm node tuong ung voi user, doi voi treedept
            var that = this;
            children.forEach(function (item) {
                var user = btalk.ROSTER.getUserByJID(btalk.auth.getJID());
                var _depts = user instanceof btalk.roster.eGovUser ? user.getDepartmentsStr() : "";
                if (_depts.indexOf(item.label) > -1) {
                    that._nodeAuth = item;
                }
            });
            return children;
        },

        _appendChild: function ($parent, parentId, hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles) {
            var child = this._getChildrens(parentId, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
            if (child.length > 0) {
                for (var i = 0; i < child.length; i++) {
                    var position = 'last';
                    var node_id = this.$treeDepartmentPanel.jstree().create_node($('#' + $parent.id), child[i], position, function () { });
                }
            }

            //  DamBV - 24/04/2017 : mo phong ban tuong ung voi user dang dung, doi voi treedept
            for (var i = 0; i < child.length; i++) {
                var item = child[i];
                if (this._nodeAuth !== null && this._nodeAuth.label && this._nodeAuth.label.indexOf(item.label) > -1) {
                    this.$treeDepartmentPanel.jstree("open_node", $('#' + item.id));
                    break;
                }
            }
        },

        // Danh sach chatter can update trang thai online
        // La cac chatter thuoc cac phong ban dang opened
        activeChatters: [],
        _bindJsTree: function (divTree, hasUser, hasCheckbox,
            hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind) {
            var deptRoot = _.find(arrDept, function (node) {
                return node.parentid === 0;
            });

            if (hasCheckbox) {
                this._plugins.push("checkbox");
            }
            //if (hasDnD) {
            //    this._plugins.push("dnd");
            //}
            if (!deptRoot) {
                return;
            }
            this._plugins.push("contextmenu");

            var rootChildren = this._getChildrens(deptRoot.id, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);

            divTree.jstree({
                "core": {
                    "data": [{
                        'id': deptRoot.id,
                        'parentid': deptRoot.parentid,
                        'text': deptRoot.text,
                        'idext': deptRoot.idext,
                        'icon': false,//deptRoot.icon,
                        'order': deptRoot.order,
                        'level': deptRoot.level,
                        'label': deptRoot.label,
                        'state': {
                            "opened": true
                        },
                        'state_str': function () {
                            return this.state ? "open" : "close";
                        },
                        'a_attr': {
                            'rel': 'dept',
                            'idext': deptRoot.idext,
                            'label': deptRoot.label
                        },
                        'li_attr': {
                            'id': deptRoot.id
                        },
                        'children': rootChildren
                    }],

                    "check_callback": function (e, data) {
                        //console.log(data)
                    }
                },

                "crrm": hasDnD === false ? {} : {
                    "move": {
                        "check_move": function (m) {
                            var dept = _.find(arrDept, function (de) {
                                return de.value === parseInt(m.o.attr('id'));
                            });

                            if (!dept) return false;
                            if (dept.level != 1) return false;

                            var p = this.get_parent(m.o);
                            if (!p) return false;

                            p = p == -1 ? this.get_container() : p;
                            if (p === m.np) return true;

                            if (p[0] && m.np[0] && p[0] === m.np[0]) return true;

                            return false;
                        }
                    }
                },

                //"dnd": hasDnD === false ? {} : {
                //    "drop_target": false,
                //    "drag_target": false
                //},

                "contextmenu": {
                    "items": function ($node) {
                        if ($node.children.length == 0) return;
                        var tree = $("#tree").jstree(!0);
                        return {
                            "SendMessageMembers": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "Gửi tin nhắn đến thành viên",
                                "icon": "/themes/default/images/chat.png",
                                "action": function (obj) {
                                    var idtext = obj.reference[0].attributes.idext;
                                    var label = obj.reference[0].attributes.label;
                                    var listUser = btalk.ROSTER.getUserByDepartment(idtext);
                                    btalk.APPVIEW.sendMessageWithUsers(listUser, label);
                                    listUser = null;
                                    idtext = null;
                                    label = null;
                                }
                            },
                            "CreateGroupChat": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "Tạo nhóm chat",
                                "icon": "/themes/default/images/people.png",
                                "action": function (obj) {
                                    var idtext = obj.reference[0].attributes.idext;
                                    var label = obj.reference[0].attributes.label
                                    var listUser = btalk.ROSTER.getUserByDepartment(idtext);
                                    btalk.APPVIEW.createGroupChatWithUsers(listUser);
                                    listUser = null;
                                    idtext = null;
                                    label = null;
                                }
                            }
                        }
                    }
                },

                "plugins": this._plugins
            }).bind('loaded.jstree', function (e, dataLoad) {
                this._treeDeptIsReady = true;

                /** Event open node */
                divTree.bind("open_node.jstree", function (event, data) {
                    if (data.instance.get_children_dom(data.node).length == 1 &&
                        $(data.instance.get_children_dom(data.node)[0]).find('a').text() == "_") {
                        this.$treeDepartmentPanel.jstree().delete_node($(data.instance.get_children_dom(data.node)[0]));
                        this._appendChild(data.node, parseInt(data.node.id), hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles);
                    }
                }.bind(this));

                /** Event close node */
                divTree.bind("close_node.jstree", function (event, data) {
                    // Xu ly remove danh sach user trong node vua dong
                    this.$treeDepartmentPanel.jstree("remove", $(event.currentTarget).find('ul'));
                }.bind(this));

                /** Event double click node */
                divTree.bind("click.jstree", this._clickTreeNode.bind(this));

                //if (this._treeDeptIsReady === true && this._treeOnlineIsReady === true) {
                //    // TODO: Tam thoi update tai day de dam bao 2 tree ve xong moi xet tiep toi tong so online
                //    // => Can sua tong quat: kiem soat duoc khi da ve xong cay + da hoan tat qua trinh login thi se thuc hien 1 so viec nhu the nay
                //    this._reUpdateOnlineState();
                //}

                //  DamBV - 24/04/2017 : mo phong ban tuong ung voi user dang dung.
                if (this._nodeAuth && this._nodeAuth.id) {
                    divTree.jstree("open_node", $('#' + this._nodeAuth.id));
                }
            }.bind(this));
        },

        // CuongNT - 24/10/2016: Them cay truc tuyen theo yeu cau cua anh QuyNX
        /* ONLINE TREE - CAY TRUC TUYEN
        ---------------------------------------------------------------------- */
        _getAllUser: function (parentid, arrUsers) {
            console.log("getAllUser:" + arrUsers.length);
            var users = [];
            for (var i = 0; i < arrUsers.length; i++) {
                var user = arrUsers[i];
                var children = _.filter(users, function (num) {
                    return num.text === user.username;
                });
                if (children && children.length > 0) {
                    continue;
                }

                var imgSrc = btalk.view.getAvatar([user.username]);
                var rosterUser = btalk.ROSTER.getUserByName(user.username);
                //TamDN - Fix loi khi co tai khoan rong o cay phong ban tai tu egov
                if (rosterUser) {
                    var selected = {
                        //'id': deptUsers[i].idext + '.' + userindept.value,
                        'parentid': parentid,
                        'text': rosterUser.username,
                        'icon': imgSrc,
                        "state": {
                            opened: false
                        },
                        'a_attr': {
                            // <a class='cuongnt online' => de tim va update trang thai online/offline thuan loi hon
                            'class': rosterUser && rosterUser.status == 'unavailable' ? rosterUser.name + " offline" : rosterUser.name + " online",
                            'rel': 'people',
                            'label': rosterUser.fullname,
                        },
                        'li_attr': {
                            //'id': user.value,
                            'class': rosterUser && rosterUser.status == 'unavailable' ? "invisible chatter " + rosterUser.name : "chatter " + rosterUser.name,
                            //'class': 'chatter',
                            'chatter': rosterUser.name,
                        }
                    };
                    users.push(selected);
                }
            }
            return users;
        },

        _bindJsTreeOnline: function (divTree, hasUser, hasCheckbox,
            hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind) {
            var deptRoot = _.find(arrDept, function (node) {
                return node.parentid === 0;
            });

            if (hasCheckbox) {
                this._plugins.push("checkbox");
            }
            if (hasDnD) {
                this._plugins.push("dnd");
            }
            if (!deptRoot) {
                return;
            }
            var rootChildren = this._getAllUser(deptRoot.id, arrUsers);
            divTree.jstree({
                "core": {
                    "data": [{
                        'id': deptRoot.id,
                        'parentid': deptRoot.parentid,
                        'text': deptRoot.text,
                        'idext': deptRoot.idext,
                        'icon': false,//deptRoot.icon,
                        'order': deptRoot.order,
                        'level': deptRoot.level,
                        'label': deptRoot.label,
                        'state': {
                            "opened": true
                        },
                        'state_str': function () {
                            return this.state ? "open" : "close";
                        },
                        'a_attr': {
                            'rel': 'dept',
                            'idext': deptRoot.idext,
                            'label': deptRoot.label
                        },
                        'li_attr': {
                            'id': deptRoot.id
                        },
                        'children': rootChildren
                    }],

                    "check_callback": function (e, data) {
                        //console.log(data)
                    }
                },

                "crrm": hasDnD === false ? {} : {
                    "move": {
                        "check_move": function (m) {
                            var dept = _.find(arrDept, function (de) {
                                return de.value === parseInt(m.o.attr('id'));
                            });

                            if (!dept) return false;
                            if (dept.level != 1) return false;

                            var p = this.get_parent(m.o);
                            if (!p) return false;

                            p = p == -1 ? this.get_container() : p;
                            if (p === m.np) return true;

                            if (p[0] && m.np[0] && p[0] === m.np[0]) return true;

                            return false;
                        }
                    }
                },

                "dnd": hasDnD === false ? {} : {
                    "drop_target": false,
                    "drag_target": false
                },

                "plugins": this._plugins
            }).bind('loaded.jstree', function (e, dataLoad) {
                this._treeOnlineIsReady = true;
                /** Event double click node */
                divTree.bind("click.jstree", this._clickTreeNode.bind(this));

                divTree.bind("open_node.jstree", function (event, data) {
                    btalk.APPVIEW._reUpdateOnlineState();
                }.bind(this));

                //if (this._treeDeptIsReady === true && this._treeOnlineIsReady === true) {
                //    // TODO: Tam thoi update tai day de dam bao 2 tree ve xong moi xet tiep toi tong so online => Can sua tong quat: kiem soat duoc khi da ve xong cay + da hoan tat qua trinh login thi se thuc hien 1 so viec nhu the nay
                //    this._reUpdateOnlineState();
                //}
            }.bind(this));
        },

        /* MODEL EVENT - CAC SU KIEN TREN BACKBONEJS MODEL */
        detectChatterNextState: function () {
            // Xu ly phan hien thi dong bao tai them trong 2 truong hop:
            // - Khi noi dung khong du hien scroll
            // - Khi scroll xuong con cach bottom Npx

            // reset state truoc khi check scroll
            btalk.CHATTERNEXT.hidden();

            if (!this.$chatterPanelOuterOuter.scrollable()) {
                btalk.CHATTERNEXT.stopLoading();
            } else {
                btalk.CHATTERNEXT.hidden();
            }
        },

        detectMessageNextState: function () {
            // Xu ly phan hien thi dong bao tai them trong 2 truong hop:
            // - Khi noi dung khong du hien scroll
            // - Khi scroll xuong con cach bottom Npx

            // reset state truoc khi check scroll
            this.MESSAGENEXT.hidden();

            if (!this.$messagePanelOuter.scrollable()) {
                this.MESSAGENEXT.stopLoading();
            } else {
                this.MESSAGENEXT.hidden();
            }
        },

        //  Chatter List
        addChatter: function (roster) {
            var chatterView = new btalk.view.ChatterView({ model: roster });
            if (roster.get("isAtTop") === true) {
                this.$chatterPanel.prepend(chatterView.render().el);
            } else {
                this.$chatterPanel.append(chatterView.render().el);
            }
        },

        // ChatterSearch List

        addChatterSearch: function (roster) {
            var chatterSearchView = new btalk.view.ChatterSearchView({ model: roster });
            this.$chatterSearchPanel.append(chatterSearchView.render().el);
        },

        // Chi tiet lich su chat
        addConversationDay: function (day) {
            var conversationDay = new btalk.view.ConversationDayView({ model: day });
            if (day.get('append') === true) {
                this.$messagePanel.append(conversationDay.render().el);
            } else {
                this.$messagePanel.prepend(conversationDay.render().el);
                this.preventScrolling();
            }
        },

        removeConversationDay: function () {
            this.preventScrolling();
        },

        resetConversationDays: function (chatDays) {
            // reset mot so thuoc tinh trang thai
        },

        /* XMPP EVENT - CAC SU KIEN KHI GUI NHAN GOI TIN XMPP */

        // Xu ly ket qua lay danh sach ban be
        handleRosters: function (rosters) {
            // Chi ve cay phong ban neu dang su dung trong eGov
            if (btalk.config.CLIENT.CLIENT_TYPE === 'egov') {
                // TODO: Can co kich ban goi lai ham nay moi khi click vao tab Toan bo neu lan dau tien load chua thanh cong
                btalk.egov.getUserDeptTree({ success: this.printUserDeptTree.bind(this, rosters) });
            }
        },

        // DamBV: fix loi ve ve danh sach online
        handleUsersOnline: function (usersOnline) {
            console.log("Da nhan danh sach online cho lan dau");
            // Khi co ket qua danh sach online cua lan dau thi cap nhat tu dong ve danh sach online.
            if (!usersOnline || usersOnline.type != 'result') return;
            this._reUpdateOnlineState();
        },

        /**
         * Xu ly ket qua lay trang dau tien danh sach cuoc hoi thoai
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
        handleChatters: function (pageOfChatters) {
            // Neu khong con cuoc hoi thoai nao chua lay tu server ve
            if (!pageOfChatters.list.chat) {
                this.detectChatterNextState();
                // TODO: Day la lan dau tien su dung app nen can kich hoat kich ban huong dan su dung tim nguoi chat
                this.enableSearch();
                this.NEVER_USE = true;

                //  DamBV - 22/06/2017
                if (btalk.ChatBmail) {
                    this.$messagePanel.html("<center>Bạn không có lịch sử hỗ trợ</center>");
                }
                return;
            }
            // LUU Y: pageOfChatters khi parse bang thu vien $.xml2json co the la object hoac []
            // la object khi chi co 1 the, la [] khi co nhieu the.
            var chats = $.isArray(pageOfChatters.list.chat) ?
                                pageOfChatters.list.chat : [pageOfChatters.list.chat];

            for (var i = 0; i < chats.length; i++) {
                var jidWithOutResource = btalk.cutResource(chats[i].with);

                //TamDN - Them truong fullname
                var fullname = chats[i].fullname;

                //Kiem tra neu nhom chat la nhom ho tro thi hien thi ten ho tro thay vi ten nhom chat
                var supportGroupPart = btalk.cm.options.support.conferenceExtra + "@" + btalk.cm.options.xmpp.conference;

                if (chats[i].with && chats[i].with.indexOf(supportGroupPart) != -1)
                    fullname = chats[i].with.split("@")[0];

                var rosterUser = btalk.ROSTER.getUserByJID(jidWithOutResource);

                // Kiem tra la message toi tu chat nhom hay user
                // Neu la chat nhom
                var isMucMessage = jidWithOutResource.indexOf(btalk.config.CM.xmpp.conference) > 0;
                if (!rosterUser && isMucMessage) {
                    rosterUser = btalk.ROSTER.addUser(jidWithOutResource);
                    rosterUser.type = isMucMessage ? btalk.CHATTYPE.GROUPCHAT : btalk.CHATTYPE.CHAT;
                }

                if (!isMucMessage)
                    fullname = jidWithOutResource.split('@')[0];

                // Khong xu ly tin nhan cua chatter khong con trong danh ba
                if (!rosterUser) {
                    continue;
                }

                if (!rosterUser.chatterM) {
                    var _lastmessage = this.getLastmessageStr(chats[i].message);
                    var _fromSort = chats[i].from.split("@")[0];
                    var timestamp = new Date(parseInt(chats[i].timestamp));

                    var isme = btalk.cutResource(chats[i].from) == btalk.ROSTER.currentuser.jid;
                    if (isme) {
                        _lastmessage = "Bạn: " + _lastmessage;
                    }
                    else if (isMucMessage) {
                        _lastmessage = _fromSort + ": " + _lastmessage;
                    }

                    if (btalk.isMobile && rosterUser.type === btalk.CHATTYPE.CHAT) {
                        fullname = rosterUser.fullname;
                    }

                    var newchatter = new btalk.model.Chatter({
                        jid: jidWithOutResource, 						// chat.with
                        account: jidWithOutResource.split('@')[0], 		// chat.with.split('@')[0]
                        isme: isme,									// Neu la tin nhan cua minh gui di
                        type: isMucMessage ? btalk.CHATTYPE.GROUPCHAT : btalk.CHATTYPE.CHAT,									// chat, groupchat
                        // Tin nhan cuoi cung
                        lasttimestamp: timestamp,
                        lastmessage: _lastmessage,					// Noi dung tin nhan cuoi
                        // Tin nhan chua doc (tin moi, tin offfline)
                        unread: false,									// Trang thai co tin nhan moi chua xem
                        unreadcount: 0,									// So luong tin nhan moi chua xem
                        // true khi duoc click chon tren giao dien
                        isselected: false,
                        fullname: fullname,									// account || fullname
                        rosterUser: rosterUser,							// Tro ve doi tuong RosterUser tuong ung
                        roster: btalk.ROSTER,							// Tro ve doi tuong Roster: btalk.ROSTER
                        online: rosterUser.status == "available",
                        isSeenLastMessage: false,   //hack code
                        statusHeightChatter: btalk.cache.statusSizeChatter,
                    });
                    var _isSeenLastMessage = this.isSeenLastMessage(newchatter, chats[i]);
                    newchatter.set("isSeenLastMessage", _isSeenLastMessage);

                    btalk.CHATTERS.add(newchatter);
                    // Gan lai chatter cho rosterUser
                    rosterUser.chatterM = newchatter;
                } else {
                    if (rosterUser.chatterM.get('isResultOfSearch') === true) {
                        rosterUser.chatterM.set({ isResultOfSearch: false });
                    }
                    // Move chatter nay xuong cuoi cung danh sach hien tai de sap xep dung
                    // theo thu tu server tra ve
                    this.moveChatterToBottom(rosterUser.chatterM.view.$el);
                }
            }

            //  DamBV- 8/3/2017
            this.updateStatusNotificationChatter();

            this.detectChatterNextState();

            this.enableSearch();
        },

        isSeenLastMessage: function (chatter, lastmsg) {
            if (!chatter.get('isme') || chatter.get('type') == btalk.CHATTYPE.GROUPCHAT) {
                return false;
            }
            var viewedBy = btalk.VIEWED_CACHE_LIST.getViewedListByMsgId(chatter.get("jid"), lastmsg.id);
            if (viewedBy && viewedBy.length > 0)
                return true;
            return false;
        },

        /**
         * Xu ly khi qua trinh login hoan tat
         * Su dung de bao trang thai login thanh cong thi moi render giao dien
         */
        handleLogined: function (rosters) {
            // Render giao dien tai day dua tren danh sach ban be voi day du trang  thai truc tuyen va lich su chat cuoi cung neu co
            if (!btalk.ROSTER.currentuser) {
                // [TODO] bao loi va day ra trang login
                this.logout();
            }

            $('span[data-field="fullname"]').text(btalk.ROSTER.currentuser.fullname);
            // [TODO] Chay kich ban huong dan neu la lan dau tien su dung

            if (this.NEVER_USE === true) {
                forcusTab("tab_office");
            } else {
                if (!btalk.isMobile) {
                    btalk.CHATTERS.selectedTop();
                }

                this.$messageTxt.focus();
            }
        },

        // ! Presence
        /* TODO: Tam thoi bo sung them ham nay, va goi sau khi ve xong 2 tree
         * => Bi lap set trang thai online voi su kien handleonline khi dang nhap.
         */
        updateAllOnlineStatus: function () {
            var onlines = btalk.ROSTER.getAllOnline();
            console.log("DaMBV - size Roster length" + btalk.ROSTER.users.length);
            for (var i = 0; i < onlines.length; i++) {
                console.log("DaMBV - render Online " + onlines[i].name);
                this.updateOnlineStatus(onlines[i]);
            }
        },

        updateOnlineStatus: function (user) {
            // TODO: CuongNT - user.name.indexOf('@') > 0 Tam thoi cho vao de loai tru presence nhan tu chat nhom. Can fix chuan hon.
            if (!user || user.name.indexOf('@') > 0) {
                return;
            }
            if (btalk.config.CLIENT.CLIENT_TYPE == 'egov') {
                // Cap nhat trang thai online/offline phan phong ban
                var $tree_node = this.$treeDepartmentPanel.find("li [chatter='" + user.name + "']").children("a");
                if ($tree_node) {
                    if (user.status == 'unavailable') {
                        $tree_node.removeClass('online').addClass('offline');
                    } else {
                        $tree_node.removeClass('offline').addClass('online');
                    }
                }

                // Cap nhat trang thai online/offline phan phong ban cay online
                var $tree_node_online_li = this.$treeOnlinePanel.find("li [chatter='" + user.name + "']");
                if ($tree_node_online_li.length > 0) {
                    var $tree_node_online_a = $tree_node_online_li.children("a");
                    //console.log("INFO: Tim thay " + user.name + " tren treeOnline. Trang thai: " + user.status);
                    if (user.status === "unavailable") {
                        $tree_node_online_li.addClass('invisible');
                        $tree_node_online_a.removeClass('online').addClass('offline');
                    } else {
                        $tree_node_online_li.removeClass('invisible');
                        $tree_node_online_a.removeClass('offline').addClass('online');
                    }
                } else {
                    console.log("WARNING: Khong tim thay " + user.name + " tren treeOnline");
                }
            }

            // Cap nhat trang thai online/offline trong noi dung chat           
            var $member_node = $('#message_panel').find('div.' + user.name);
            if ($member_node) {
                if (user.status == 'unavailable') {
                    $member_node.removeClass('online').addClass('offline');
                } else {
                    $member_node.removeClass('offline').addClass('online');
                }
            }

            // Cap nhat trang thai online/offline phan thong tin chi tiet
            //  - DamBV - 06/12/2016 thay doi user.name = user.fulljid;
            var $chatter = this.$chatterInfoPanel.find('div[data-online="' + user.fulljid + '"]');
            if ($chatter) {
                if (user.status == 'unavailable') {
                    $chatter.removeClass('online').addClass('offline');
                } else {
                    $chatter.removeClass('offline').addClass('online');
                }
            }

            if (user.chatterM) {
                user.chatterM.set('online', user.status == 'available');
            }
        },

        updateOnlineCount: function () {
            var totalOnline = btalk.ROSTER.getTotalOnline();
            //console.log("INFO: Total online count " + totalOnline);
            var onlineCount = " - <span class='bt-color-blue'>(" + totalOnline + ")</span>";

            // Cap nhat tong so nguoi online
            var firstDept = $("#tree>ul:first-child>li:first-child>a");
            var onlineObject = firstDept.find(".bt-color-blue");
            if (onlineObject.length === 0) {
                firstDept.append(onlineCount);
            } else {
                onlineObject.text("(" + totalOnline + ")");
            }

            // Cap nhat tong so nguoi online
            var firstDept2 = $("#treeOnline>ul:first-child>li:first-child>a");
            var onlineObject2 = firstDept2.find(".bt-color-blue");
            if (onlineObject2.length === 0) {
                //var onlineCount2 = " - <span class='bt-color-blue'>(" + totalOnline + ")</span>";
                firstDept2.append(onlineCount);
            } else {
                onlineObject2.text("(" + totalOnline + ")");
            }
        },

        handleOnline: function (presence) {
            // [TODO] Xu ly online cho ca user va group chat

            // Kiem tra, dam bao 2 tree phong ban va tree online da sang sang.
            if (this._treeDeptIsReady === true && this._treeOnlineIsReady === true) {
                var chatterJid = btalk.cutResource(presence.from);
                var user = btalk.ROSTER.getUserByJID(chatterJid);
                this.updateOnlineStatus(user);
                this.updateOnlineCount();
            }
            // Cap nhat giao dien da duoc thuc hien trong btalk.roster.UserRoster.update();
            // Ham nay duoc goi tu dong trong btalk.connectionManager.js moi khi nhan presence.
            // TODO: Can xem lai kich ban cho nay de co logic de hieu hon. Cai nao thi thuc hien o tang duoi, cai nao thuc hien o tang giao dien.
        },

        handleOffline: function (presence) {
            // Kiem tra, dam bao 2 tree phong ban va tree online da sang sang.
            if (this._treeDeptIsReady === true && this._treeOnlineIsReady === true) {
                var chatterJid = btalk.cutResource(presence.from);
                var user = btalk.ROSTER.getUserByJID(chatterJid);
                this.updateOnlineStatus(user);
                this.updateOnlineCount();
            }
        },

        handleStatusIDLE: function (presence) {
            // Kiem tra, dam bao 2 tree phong ban va tree online da sang sang.
            if (this._treeDeptIsReady === true && this._treeOnlineIsReady === true) {
                var chatterJid = btalk.cutResource(presence.from);
                var user = btalk.ROSTER.getUserByJID(chatterJid);
                this.updateStatusIdle(user);
            }
        },

        updateStatusIdle: function (user) {
            if (!user || user.name.indexOf('@') > 0) {
                return;
            }
            var statusIdle = false;
            if (user.status == 'away' && user.statusMsg == 'available') statusIdle = true;
            if (user.status == 'chat' && user.statusMsg == 'available') statusIdle = false;

            if (btalk.config.CLIENT.CLIENT_TYPE == 'egov') {
                // Cap nhat trang thai idle phan phong ban
                var $tree_node = this.$treeDepartmentPanel.find("li [chatter='" + user.name + "']").children("a");
                if ($tree_node) {
                    if (statusIdle) {
                        $tree_node.addClass('idle');
                    } else {
                        $tree_node.removeClass('idle');
                    }
                }

                // Cap nhat trang thai idle phan phong ban cay online
                var $tree_node_online_li = this.$treeOnlinePanel.find("li [chatter='" + user.name + "']");
                if ($tree_node_online_li.length > 0) {
                    var $tree_node_online_a = $tree_node_online_li.children("a");
                    //console.log("INFO: Tim thay " + user.name + " tren treeOnline. Trang thai: " + user.status);
                    if (statusIdle) {
                        $tree_node_online_li.addClass('invisible');
                        $tree_node.addClass('idle');
                    } else {
                        $tree_node_online_li.removeClass('invisible');
                        $tree_node.removeClass('idle');
                    }

                } else {
                    console.log("WARNING: Khong tim thay " + user.name + " tren treeOnline");
                }
            }

            // Cap nhat trang thai idle trong noi dung chat           
            var $member_node = $('#message_panel').find('div.' + user.name);
            if ($member_node) {
                if (statusIdle) {
                    $member_node.addClass('idle');
                } else {
                    $member_node.removeClass('idle');
                }
            }

            // Cap nhat trang thai idle phan thong tin chi tiet
            var $chatter = this.$chatterInfoPanel.find('div[data-online="' + user.fulljid + '"]');
            if ($chatter) {
                if (statusIdle) {
                    $chatter.addClass('idle');
                } else {
                    $chatter.removeClass('idle');
                }
            }
            if (user.chatterM) {
                user.chatterM.set('statusIdle', statusIdle);
            }
        },

        /*
         * TamDN - Ham upload file goi khi drag & drop hoac chon file tu button gui file
         */
        sendFiles: function (files) {
            if (files.length > 5) {
                alert("Chỉ được phép gửi tối đa 5 file");
                return;
            }
            //Kich thuoc file toi da, doc tu config ra
            var MAX_SIZE = egov.config.FILESERVER.MAX_SIZE;

            var _currentChatter = this.CURRENTCHATTER;
            var sentDate = new Date();
            var attachments = {};

            // Gui message bao danh sach file se trao doi
            var messageid = sentDate.getTime().toString();

            for (var i = 0, f; f = files[i]; i++) {
                if (f.size > MAX_SIZE * 1000000) {
                    alert("Chỉ được phép gửi file có dung lượng tối đa là " + MAX_SIZE + "MB");
                    return;
                }
                var fileid = "file_" + messageid + "_" + i;
                attachments[fileid] = {
                    append: true,
                    id: fileid,
                    messageid: messageid,
                    // xmpp info
                    to: _currentChatter.get('jid'),
                    'from': btalk.ROSTER.currentuser.jid,
                    // file info
                    file: f,
                    lastModified: f.lastModified,
                    lastModifiedDate: f.lastModifiedDate,
                    size: f.size,
                    name: f.name,
                    // Ten tren url khi upload len
                    object: (new Date()).getTime() + i + '',
                    'type': f.type,
                    tenantid: btalk.fm.gettenantid(),
                    sentDate: new Date(),
                    fileServerType: btalk.fm.fileServerType
                };
            }
            var totalfiles = Object.keys(attachments).length;
            var fileids = [];
            btalk.cm.sendAttachments(attachments, _currentChatter.get('jid'), _currentChatter.get("type"), this.printAndSaveMessage.bind(this, _currentChatter));
            for (var f in attachments) {
                btalk.fm.upload(attachments[f], attachments[f].from, attachments[f].to,
                    this.sendPercentage.bind(this, f, attachments, totalfiles, fileids),
                    this.sendAttachmentError.bind(this, f, attachments, totalfiles, fileids));
            }
        },

        /*
         *  DamBV - 16/01/2017 - Xử lí click vào avatar trong chat nhóm để bắt đầu chat với người đó.
         */
        selectAccountChat: function (e) {
            var canSelect = true;
            if (this.CURRENTCHATTER && typeof this.CURRENTCHATTER.get === "function") {
                var currentJid = this.CURRENTCHATTER.get("jid");
                var supportGroupPart = btalk.cm.options.support.conferenceExtra + "@" + btalk.cm.options.xmpp.conference;
                if (currentJid.indexOf(supportGroupPart) != -1)
                    canSelect = false;
            }
            if (canSelect) {
                var account = $(e.target).attr('title');
                if (account != null) {
                    this._chatWithAccount(account);
                }
            }
        },

        // ! Message
        preProcessMessage: function (oMsg) {
            oMsg.body = btalk.text(oMsg.body);

            // 1. 	Neu la message gui di, duoc forward tu client khac dang dang nhap cung tai khoan
            oMsg.chatterJid = oMsg.carbons === true ? oMsg.to : oMsg.from;
            oMsg.chatterJid = btalk.cutResource(oMsg.chatterJid);

            // 2. 	Neu la message cap nhat trang thai gui/nhan
            if (oMsg.received) {
                oMsg.status = oMsg.received.status || 'viewed';
            }

            // 3. 	Luon la tin chua doc do moi nhan tu server. Chi khi ve len giao dien moi xet trang thai tin da doc hay chua.
            oMsg.unread = oMsg.carbons === true ? false : true;
            oMsg.processType = this._checkProcessTypeOfMessage(oMsg);
            if (!oMsg.secs) {
                var start = new Date(this.options.baseDatetimeQuery);
                var secs = ((new Date()).getTime() - start.getTime()) / 1000;
                oMsg.secs = secs;
            }
            return oMsg;
        },

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
         */
        handleTyping: null,

        showTyping: function (chatter) {
            if (this.handleTyping) {
                clearTimeout(this.handleTyping);
                this.handleTyping = null;
            }

            var authJID = btalk.auth.getJID();
            if (authJID.substring(0, authJID.indexOf('@')) != chatter) {
                $("#typing").text(chatter + ' đang soạn nội dung...').show();

                this.handleTyping = setTimeout(function () {
                    // disable typing
                    this.hideTyping();
                }.bind(this), 6000);
            }
        },

        hideTyping: function () {
            if (this.handleTyping) {
                $("#typing").hide();
                clearTimeout(this.handleTyping);
                this.handleTyping = null;
            }
        },

        /*
         * DamBV:  lam moi lai trang thai online khi nhan tin nhan cua tai khoan offline.
         */
        setStatusOnlineSender: function (oMsg) {
            /*
             * TamDN - 10/2/2017
             * Neu nhan duoc message cua tai khoan dang offline, set lai tai khoan do thanh online
             * Fix loi trang thai khi goi tin presence bi mat
             */
            if (!(oMsg.processType == "received" && oMsg.status == "server")) {
                var from = oMsg.from;
                if (oMsg.type == btalk.CHATTYPE.GROUPCHAT) {
                    from = btalk.getResource(from);
                    if (from.indexOf("@") < 0)
                        from = from + "@" + window._DOMAIN_OF_ACCOUNT;
                }
                else if (oMsg.type == btalk.CHATTYPE.CHAT) {
                    from = btalk.cutResource(from);
                }

                var user = btalk.ROSTER.getUserByJID(from);
                if (user) {
                    var isOffline = false;
                    if (user.status == "unavailable")
                        isOffline = true;
                    if (user.chatterM && !user.chatterM.get("online"))
                        isOffline = true;

                    //Neu tai khoan dang offline, cap nhat lai trang thai
                    if (isOffline) {
                        user.status = "available";
                        var presence = {
                            from: from,
                            priority: "8",
                            to: oMsg.to
                        }
                        this.handleOnline(presence);
                    }
                }
            }
        },

        /*
         * DamBV: Cap nhat tin nhan cuoi cung voi tuong ung voi chatter khi nhan duoc tin nhan.
         */
        modifyLastMessageChatter: function (chatter, oMsg, isMucMessage) {
            var _lastmessagestr = this.getLastmessageStr(oMsg.body);

            var isme = btalk.cutResource(oMsg.from).toLowerCase() == btalk.ROSTER.currentuser.jid ? true : false;

            if (isMucMessage) {
                isme = btalk.getResource(oMsg.from).toLowerCase() == btalk.ROSTER.currentuser.jid.split("@")[0];
                if (isme)
                    _lastmessagestr = "Bạn: " + _lastmessagestr;
                else
                    _lastmessagestr = btalk.getResource(oMsg.from).toLowerCase() + ": " + _lastmessagestr;
            }
            else {
                if (isme)
                    _lastmessagestr = "Bạn: " + _lastmessagestr;
            }
            chatter.set({ lasttimestamp: new Date(), lastmessage: _lastmessagestr, isme: isme });
            chatter.lastmsg = oMsg;
        },

        /*
         * DamBV : Chuẩn bị dữ liệu cho việc hiển thị thông báo khi có tin nhắn mới
         */
        preDataNotificationNewMessage: function (chatter, oMsg, rosterUser) {
            if (this.CURRENTCHATTER instanceof Backbone.Model
                   && this.CURRENTCHATTER.get("jid") == oMsg.chatterJid) {
                // TODO: Sua de unread duoc su dung dung cho 2 muc dich: hien thi va luu tru
                // Luu lai tin nhan cuoi cung (tin nhan, tin gui file) de xu ly khi gui goi tin xac nhan da xem
                if (btalk.WINDOWFOCUS != true && oMsg.unread === true) {
                    this.CURRENTCHATTER.unread(oMsg.unread);
                }
            } else {
                // [TODO] Hien tai bi update lien tuc khi moi login vao neu co tin offline -> xem xu ly chi hien 1 lan cuoi voi tin offline
                // Cap nhat trang thai unread + unreadcount cho chatter tuong ung
                chatter.unread(oMsg.unread);
            }

            // La tin nhan thuong.
            if (oMsg.carbons != true
                && chatter.get("unreadcount") > 0
                && !btalk.WINDOWFOCUS
                && oMsg.processType == btalk.CHATTYPE.CHAT) {
                // TODO: Sau gop titleAlert vao btalk.ntf
                // Canh bao tin nhan moi toi tren title
                $.titleAlert('(' + chatter.get("unreadcount") + ') ' + oMsg.from.split('@')[0], {
                    requireBlur: true,
                    stopOnFocus: true,
                    originalTitleInterval: 0,
                    duration: 0,
                    interval: 1000
                });

                // Notification
                //  DamBV - 22/02/2017 : kiếm tra xem có được phép hiện thông báo không.
                var idChatter = btalk.cutResource(oMsg.from);
                var _isNotification = this.canShowNotifyNewMessage(idChatter);
                if (_isNotification) {
                    var ntftitle = oMsg.from.split('@')[0];
                    var avatarChatter;
                    if (oMsg.type == btalk.CHATTYPE.GROUPCHAT) {
                        var fullname = rosterUser.chatterM.get('fullname');
                        ntftitle = fullname;
                        avatarChatter = btalk.getResource(oMsg.from);
                    } else {
                        avatarChatter = idChatter;
                    }
                    if (avatarChatter.indexOf("@") == -1)
                        avatarChatter += "@" + window._DOMAIN_OF_ACCOUNT;
                    var message = oMsg.body;

                    /*
                     *  DamBV - 03/05/2017 : Xác dinh gia tri isread, isme.
                     * isread: là true khi dang focus vào chat và ngược lại là flase.
                     * isme: true khi la noi dung tin chat cua minh va flase la khi khong phai tin chat cua minh.
                     */
                    var isRead = false;
                    if (this.$messageTxt.is(":focus")) {
                        isRead = true;
                    }
                    var isme = btalk.cutResource(oMsg.from).toLowerCase() == btalk.ROSTER.currentuser.jid ? true : false;
                    btalk.ntf.show(avatarChatter, idChatter, ntftitle, message, { isMe: isme, isRead: isRead });

                    /*
                     *   DamBV : 11/04/2017
                     * Cho xac dinh ro kich ban khi co am bao.
                     */
                    btalk.audio.playSound("message_recv");
                }
            }
        },

        /*
         *  DamBV - 22/03/2017 : thong bao bao thay doi ten nhom
         */
        handleRenameGroupMessage: function (oMsg) {
            if (oMsg && oMsg.changeinfo) {
                var fields = oMsg.x.field;
                var newname = null;
                var chatterJid = oMsg.chatterJid;

                for (var i in fields) {
                    var item = fields[i];
                    if (item.var === "muc#roomconfig_roomname") {
                        newname = item.value;
                    }
                }
                var that = this;
                btalk.CHATTERS.models.forEach(function (item) {
                    var jid = item.get('jid');
                    if (jid === chatterJid) {
                        item.set('fullname', newname);
                        if (that.CURRENTCHATTER.get('jid') === jid) {
                            that.CHATTER_INFO_MODEL.set('fullname', newname);
                        }
                    }
                });
            }
        },

        /*
         *  DamBV - 27/03/2017 : Cập nhật icon đã xem trong danh sách chatter cho chat 1-1
         */
        setStatusViewedMessage: function (chatterId) {
            var listChatterModel = btalk.CHATTERS.models;
            for (var i = 0; i < listChatterModel.length; i++) {
                //Chỉ cập nhật cho chat 1-1
                if (listChatterModel[i].get('jid') === chatterId &&
                         listChatterModel[i].get('type') === btalk.CHATTYPE.CHAT) {
                    btalk.CHATTERS.models[i].set('isSeenLastMessage', true);
                    break;
                }
            }
        },

        /*
         *  DamBV - 30/03/2017:
         * - Check xem tin nhan den, neu la dang file thi se gui thong tin lay file ve.
         */
        resfreshShareFileInConversation: function (typeFile, chatter) {
            if (chatter.get('type') == btalk.CHATTYPE.GROUPCHAT) {
                if (typeFile === btalk.FILETYPE.IMAGE) {
                    btalk.cm.getMessageFileInConversation(this.CURRENTCHATTER.get('jid'), btalk.FILETYPE.IMAGE, null, btalk.CHATTYPE.GROUPCHAT);
                }
                if (typeFile === btalk.FILETYPE.FILE) {
                    btalk.cm.getMessageFileInConversation(this.CURRENTCHATTER.get('jid'), btalk.FILETYPE.FILE, null, btalk.CHATTYPE.GROUPCHAT);
                }
            } else {
                if (typeFile === btalk.FILETYPE.IMAGE) {
                    btalk.cm.getMessageFileInConversation(this.CURRENTCHATTER.get('jid'), btalk.FILETYPE.IMAGE, null, btalk.CHATTYPE.CHAT);
                }
                if (typeFile === btalk.FILETYPE.FILE) {
                    btalk.cm.getMessageFileInConversation(this.CURRENTCHATTER.get('jid'), btalk.FILETYPE.FILE, null, btalk.CHATTYPE.CHAT);
                }
            }
        },

        handleMessage: function (oMsg) {
            // Tien xu ly thong tin tren goi message truoc khi dua vao quy trinh xu ly
            if (!oMsg)
                return;
            oMsg = this.preProcessMessage(oMsg);

            /*
             * TamDN - 10/2/2017
             * Neu nhan duoc message cua tai khoan dang offline, set lai tai khoan do thanh online
             * Fix loi trang thai khi goi tin presence bi mat
             */
            this.setStatusOnlineSender(oMsg);

            //Xử lý gói tin composing
            if (oMsg.processType == "composing") {
                this.handleComposingMessage(oMsg);
                return;
            }

            //Xử lý gói tin đổi tên nhóm
            if (oMsg.changeinfo) {
                this.handleRenameGroupMessage(oMsg);
                return;
            }

            // NEU LA MESSAGE NHAN TIN, GUI FILE THONG THUONG
            if (oMsg.processType == btalk.CHATTYPE.CHAT ||
                    oMsg.processType == "file" ||
                    oMsg.processType == "roomconfig") {
                this.handleCommonMessage(oMsg);
            }

            // VE TIN LEN GIAO DIEN VA LUU CACHE
            // Neu dang trong trang thai tim kiem thi se khong hien thi ra ma chi luu vao cache.
            if (this.CURRENTCHATTER instanceof Backbone.Model
                && typeof this.CURRENTCHATTER.get == "function"
                && this.CURRENTCHATTER.get("jid") == oMsg.chatterJid
                && !btalk.SEARCH.IS_STATUS_SEARCH) {
                this.printMessage(oMsg);
                this.hideTyping();
            }
            // Luu tin nhan thong thuong, trang thai gui file vao cache
            this.saveMessage(oMsg, false);
        },

        handleComposingMessage: function (oMsg) {
            if (this.CURRENTCHATTER && typeof this.CURRENTCHATTER.get == "function" &&
                    this.CURRENTCHATTER.get('jid') == oMsg.chatterJid) {
                this.showTyping(oMsg.composing.jid.split('@')[0]);
            }
        },

        //Xử lý các tin nhắn thông thường, tin gửi file, tin roomconfig
        handleCommonMessage: function (oMsg) {
            var rosterUser = btalk.ROSTER.getUserByJID(oMsg.chatterJid);
            var chatter = rosterUser.chatterM;  //btalk.CHATTERS.findWhere({jid: oMsg.chatterJid });
            var isMucMessage = oMsg.chatterJid.split("@")[1] == btalk.config.CM.xmpp.conference;

            // Neu trong danh sach chatters da tai chua co chatter nay
            if (!rosterUser.chatterM) {
                //TamDN - Lay ra fullname neu lan dau duoc add vao nhom
                var fullname = rosterUser.fullname;
                if (oMsg.config && oMsg.config.forEach)
                    oMsg.config.forEach(function (item) {
                        if (item.room_name)
                            fullname = item.room_name;
                    });

                var newchatter = new btalk.model.Chatter({
                    jid: oMsg.chatterJid, 			// chat.with
                    account: oMsg.chatterJid.split('@')[0], 		// chat.with.split('@')[0]
                    isme: false,		// Neu la tin nhan cua minh gui di
                    type: rosterUser.type,

                    lasttimestamp: oMsg.clienttime,
                    lastmessage: oMsg.body,	// Noi dung tin nhan cuoi
                    // Tin nhan chua doc (tin moi, tin offfline)
                    unread: oMsg.unread,		// Trang thai co tin nhan moi chua xem
                    unreadcount: 0,				// So luong tin nhan moi chua xem

                    // true khi duoc click chon tren giao dien
                    isselected: false,
                    fullname: fullname,		// account || fullname

                    rosterUser: rosterUser,	// Tro ve doi tuong RosterUser tuong ung
                    roster: btalk.ROSTER,		// Tro ve doi tuong Roster: btalk.ROSTER
                    isAtTop: true,
                    online: rosterUser.status == "available",
                    // CuongNT - 24/08/2016: Them thuoc tinh kiem tra trang thai da tai lich su chat chua
                    archiveIsLoaded: false,
                    statusHeightChatter: btalk.cache.statusSizeChatter,
                });

                btalk.CHATTERS.add(newchatter);

                // Gan lai chatter cho rosterUser
                rosterUser.chatterM = newchatter;
                chatter = btalk.CHATTERS.findWhere({ jid: oMsg.chatterJid });
            }

            // CAP NHAT TIN NHAN CUOI CUNG
            this.modifyLastMessageChatter(chatter, oMsg, isMucMessage);

            // VE TIN LEN GIAO DIEN HOAC LUU CACHE
            this.preDataNotificationNewMessage(chatter, oMsg, rosterUser);

            //  DamBV - 20/02/2017
            // TH1: Neu la tin nhan tuong ung voi chatter hien tai: chekc thong bao phan o phia duoi, dong bo voi phan file chia se.
            // TH2: Neu la tin nhan khong phai cua chatter hien tai: chekc thong tin o danh sach chatter, luu bien vao chatter de dong bo sau.
            var _currentChatterJid = "";
            if (this.CURRENTCHATTER && typeof this.CURRENTCHATTER.get == "function")
                _currentChatterJid = this.CURRENTCHATTER.get("jid");
            if (rosterUser.jid != _currentChatterJid) {
                // Kiem tra xem hien thi thong bao khi co tin nhan o chatter bi scroll an.
                var userId;
                if (oMsg.type == btalk.CHATTYPE.CHAT) {
                    //userId = btalk.ROSTER.getUserByJID(oMsg.from);
                    userId = oMsg.from;
                } else {
                    //userId = btalk.ROSTER.getUserByJID(oMsg.chatterJid);
                    userId = oMsg.chatterJid;
                }
                this.canShowTooltipNewMessageIBelowChatter(userId);

                // tim chatter de luu bien dong bo
                btalk.CHATTERS.models.forEach(function (item) {
                    if (oMsg.attachment) {
                        var jid = item.get('jid');
                        if (jid == rosterUser.jid) {
                            item.set(btalk.IS_REFRESH_SHARE_FILE, true);
                        }
                    }
                });
            } else {
                // Hien thi thong bao neu nhu co tin nhan den phia tren phan go noi dung chat.
                this.canShowToolTipNewMessageOnTextInput();
                // Neu co la tin nhan fie thi phai dong bo.
                if (oMsg.attachment) {
                    var typeFile = "file";
                    if (oMsg.attachment.type.indexOf('image') > -1) typeFile = "image";
                    this.resfreshShareFileInConversation(typeFile, this.CURRENTCHATTER);
                }
            }
        },

        //  DamBV - 10/07/2017 : Kiem tra chatter tuong ung voi tin nhan tra ve.
        checkCurrentChatterWithMsg: function (pageOfMessages) {
            // Neu tin nhan tra ve ma khong tuong ung voi chatter thi se thay doi chatter cho phu hop.
            // -> tranh ve lich su tin nhan khac khong tuong ung voi chatter.
            var _chatterWith = pageOfMessages.chat.with;
            if (_chatterWith && _chatterWith != this.CURRENTCHATTER.get('jid')) {
                var chatter = btalk.CHATTERS.where({ jid: _chatterWith });
                chatter[0].set({ isselected: true });
            }
        },

        // !Archive
        /** Xu ly ket qua trang dau tien lich su 1 cuoc hoi thoai
        *
        * {"historyDay":"26 thang 9", "historyOfDay": [
        *		{ "account":"cuongnt", "hasAvatar" : "1", "timestamp" : "20/11/2015 12:57", "rows" : [
        *					{"id" : "1", "total" : "1", "message":"abac lajsdflajdlsfk aldfka dslfkaf"}]
        *		},
        *		{ "account":"cuongnt", "hasAvatar" : "1", "timestamp" : "20/11/2015 12:57", "rows" : [
        *					{"id" : "1", "total" : "3", "message":"abac lajsdflajdlsfk aldfka dslfkaf"},
        *					{"id" : "2", "total" : "3", "message":"abac lajsdflajdlsfk aldfka dslfkaf"},
        *					{"id" : "3", "total" : "3", "message":"abac lajsdflajdlsfk aldfka dslfkaf"}]
        *		}]
        * }
        *
        *	<iq type="result" to="84984822685@bmail.vn/cuongnt" id="page1">
        *		<chat xmlns="urn:xmpp:archive" with="cuongnt@bmail.vn" start="1469-07-21T02:56:15+0000">
        *			<to secs="17238980328">
        *				<body>adfadsfadsf</body>
        *				<active xmlns="http://jabber.org/protocol/chatstates"/>
        *			</to>
        *			<to secs="17238980346">
        *				<body>asdfasdfasdf</body>
        *				<active xmlns="http://jabber.org/protocol/chatstates"/>
        *			</to>
        *			<set xmlns="http://jabber.org/protocol/rsm">
        *				<first index="0">0</first>
        *				<last>6</last>
        *				<count>7</count>
        *			</set>
        *		</chat>
        *	</iq>
        */
        handleArchiveMessages: function (pageOfMessages, fromCache) {
            //  DamBV - 10/07/2017 ,xac dinh lai chatter hien tai.
            this.checkCurrentChatterWithMsg(pageOfMessages);

            // Neu la bind len giao dien tu cache
            fromCache = fromCache || false;
            var toAndFrom = [];

            if (fromCache) {
                // Neu la bind tu cache thi se co fromAndTo
                if (pageOfMessages.chat.fromAndTo) {
                    pageOfMessages.chat.fromAndTo = $.isArray(pageOfMessages.chat.fromAndTo) ?
                                    pageOfMessages.chat.fromAndTo : [pageOfMessages.chat.fromAndTo];
                } else {
                    pageOfMessages.chat.fromAndTo = [];
                }
                toAndFrom = toAndFrom.concat(pageOfMessages.chat.fromAndTo);
            } else {
                // Save thong tin lich su tin nhan tren server, phuc vu truy van lay lan sau
                // Hien tai dang dung cach count message trong cache de truy van co the hoi ton hieu nang
                // Neu chuyen sang cache thong tin nay se giup thuc hien nhanh hon, xong hoi phuc tap
                // nen tam chua dung
                /*this._saveCacheInfo(btalk.cutResource(pageOfMessages.chat.with), {
                    index: pageOfMessages.chat.set.index,
                    first: pageOfMessages.chat.set.first,
                    last: pageOfMessages.chat.set.last,
                    count: pageOfMessages.chat.set.count
                });*/
                toAndFrom = this.getPackageMessageFromServer(pageOfMessages);

                if (toAndFrom.length > 0) {
                    // Kiem tra neu chatter nay duoc tao ra do khi tim kiem chatter de chat
                    if (this.CURRENTCHATTER.get('isResultOfSearch') === true) {
                        var start = new Date(pageOfMessages.chat.start);
                        var secs = typeof toAndFrom[0].secs != 'number' ? parseInt(toAndFrom[0].secs) : toAndFrom[0].secs;
                        start.setSeconds(start.getSeconds() + secs);

                        // cap nhat lai lasttimestamp, lastmessage
                        var _lastmessagestr = this.getLastmessageStr(toAndFrom[0].body);
                        var _isme = btalk.cutResource(toAndFrom[0].from) == btalk.ROSTER.currentuser.jid;
                        this.CURRENTCHATTER.set({
                            lasttimestamp: start,
                            lastmessage: _lastmessagestr,
                            isme: _isme
                        });
                    }
                }
            }

            // Neu khong co tin thi return luon
            if (toAndFrom.length <= 0) {
                this.detectMessageNextState();
                return;
            }

            // Luu lai vi tri scrollBottom hien tai truoc khi update noi dung chi tiet chat
            this.saveScrollBottom();

            // Xu ly do giao dien khi render du lieu
            var loopLength = toAndFrom.length;

            // Xu ly giao dien trong TH hien thi ket qua tim kiem trong cuoc thoai
            //-> Chỉ hiển thị tin và các tin lịch sử gần đấy.
            if (pageOfMessages.id == "searchMsgHistoryWithUser") {
                this.managerMsgFind = toAndFrom;
                btalk.cm.getMessageWithTime(toAndFrom[btalk.SEARCH.INDEX_RESULT], true);
                btalk.cm.getMessageWithTime(toAndFrom[btalk.SEARCH.INDEX_RESULT], false);

                var nextResultFindMsg = $('#next_result');

                if (this.managerMsgFind.length <= 1) {
                    btalk.SEARCH.CAN_NEXT_RESULT = false;
                    nextResultFindMsg.addClass('disactive');
                } else {
                    btalk.SEARCH.CAN_NEXT_RESULT = true;
                    nextResultFindMsg.removeClass('disactive');
                }
            } else {
                var _this = this;
                // Hiển thị theo lịch sử chat của cuộc hội thoại.
                (function loop(i) {
                    if (i == loopLength - 1) {
                        _this.detectMessageNextState();
                        _this.saveScrollBottom();
                    }

                    if (i < loopLength) {
                        _this.printMessage(toAndFrom[i], fromCache, !fromCache);

                        if (i % 5 == 0) {
                            _this.printMessageTimeout = setTimeout(function () {
                                _this.saveScrollBottom();
                                loop(++i);
                            }, 1);
                        } else {
                            loop(++i);
                        }
                    }
                }(0));

                if (fromCache != true) {
                    // LUU MESSAGE VAO CACHE NEU LA MESSAGE MOI HOAC MESSAGE TU ARCHIVE
                    // Luu y: can thuc hien buoc nay cuoi cung
                    //toAndFrom[i].unread = false; //btalk.WINDOWFOCUS === true ? false : (toAndFrom[i] && toAndFrom[i].unread ? toAndFrom[i].unread : false);
                    _this.saveMessage(toAndFrom, true);
                }
            }

            // luu cac tin nhan file vao phan muc da chia se.
            this.saveFileShareFromMessage(toAndFrom);
        },

        handleArchiveRemove: function (iq) {
            // Neu không có lỗi xảy ra
            if (!iq.error) {
                var chatter = btalk.CHATTERS.where({ jid: iq.with });
                _.invoke(chatter, 'destroy');
                // Xử lý selected lại người liên trước đó nếu có hoặc liên sau nếu có
                this.moveCurrentChatter(downKey);
            } else {
                // Thông báo xóa thread trao đổi lỗi tại đây
                btalk.alert.warning(null, "Xóa lịch sử không thành công. Vui lòng thử lại sau!");
            }
        },

        // Group chat ---------------------------------------------------------
        handleGroupInvite: function (invite) {
            if (document.forms[0].invite && document.forms[0].invite[0].checked) {
                this.acceptGroupInvite(invite.to, invite.from, invite.pass);
            }
        },

        acceptGroupInvite: function (to, nick, pass) {
            //this.openGroupchat(invite.to,JID.substring(0,JID.indexOf('@')),invite.pass);
            pass = pass || '';
            nick = nick || '';

            var user = btalk.ROSTER.getUserByJID(aJid);
            try {
                if (!user) {
                    user = btalk.ROSTER.addUser(aJid, aJid.substring(0, aJid.indexOf('@')), '', ["Chat Rooms"]);
                    user.type = btalk.CHATTYPE.GROUPCHAT;
                    btalk.ROSTER.print();
                }
            } catch (e) {
                DEBUG.log(e.message, 1);
            }
        },

        handleGroupMemberJoin: function (oMsg) {
            oMsg.body = btalk.text(oMsg.body);
            oMsg.unread = true;
            oMsg.chatterJid = btalk.cutResource(oMsg.from);
            if (this.CURRENTCHATTER instanceof Backbone.Model && this.CURRENTCHATTER.get("jid") == oMsg.chatterJid) {
                this.printMessage(oMsg);
            } else {
                // Luu tin nhan thong thuong, trang thai gui file vao cache
                this.saveMessage(oMsg, false);
                var chatter = btalk.CHATTERS.findWhere({ jid: oMsg.chatterJid });
                // Cap nhat trang thai unread + unreadcount cho chatter tuong ung
                chatter.unread(true);
                chatter.lastmsg = oMsg;
                chatter.lastmessage(oMsg.body);
                chatter.lastmessageid = oMsg.id;
            }
        },

        handleGroupMemberLeft: function (oMsg) {
            oMsg.body = btalk.text(oMsg.body);
            oMsg.unread = true;

            oMsg.chatterJid = btalk.cutResource(oMsg.from);
            if (this.CURRENTCHATTER instanceof Backbone.Model && this.CURRENTCHATTER.get("jid") == oMsg.chatterJid) {
                // chatter.read(true);
                this.printMessage(oMsg);
            } else {
                // Luu tin nhan thong thuong, trang thai gui file vao cache
                this.saveMessage(oMsg, false);

                var chatter = btalk.CHATTERS.findWhere({ jid: oMsg.chatterJid });
                // Cap nhat trang thai unread + unreadcount cho chatter tuong ung
                chatter.unread(true);
                chatter.lastmsg = oMsg;
                chatter.lastmessage(oMsg.body);
                chatter.lastmessageid = oMsg.id;
            }
        },

        /* GIAO DIEN */
        // call from: btalk.view.chatter --> chatterView --> changeUnread
        moveChatterToTop: function (el) {
            // Xu ly chat gan nhat nam tren dau tien
            this.$chatterPanel.prepend(el);
        },

        moveChatterToBottom: function (el) {
            // Xu ly chat gan nhat nam tren dau tien
            this.$chatterPanel.append(el);
        },

        // Dang scroll tai them message/chatter
        isLoadingMoreChatter: false,
        loadNextPageOfChatter: function (e) {
            // Lay doi tuong dang scrolling
            var current = $(e.currentTarget);

            // Dieu kien xac dinh phai co scroll thi moi khoi phat su kien
            var hasScroll = current.scrollable();//current.prop("scrollHeight") > current.innerHeight();

            // Neu scroll dang o bottom va khong phai dang tai lich su chat cu hon
            var inBottom = current.scrollBottom() <= 0 && !this.isLoadingMoreChatter;

            if (hasScroll && inBottom && btalk.CHATTERNEXT) {
                this.isLoadingMoreChatter = true;
                btalk.CHATTERNEXTVIEW.loadNextPageOfChatters();
            }
            if (this.isLoadingMoreChatter && current.scrollTop() != 0) {
                this.isLoadingMoreChatter = false;
            }
        },

        // ! Messages panel
        // Dang scroll tai them message
        isLoadingMoreMessage: false,
        loadNextPageOfMessage: function (e) {
            // Lay doi tuong dang scrolling
            var current = $(e.currentTarget);

            // Dieu kien xac dinh phai co scroll thi moi khoi phat su kien
            var hasScroll = current.scrollable();

            // Neu scroll dang o top va khong phai dang tai lich su chat cu hon
            var inTop = current.scrollTop() == 0 && !this.isLoadingMoreMessage;

            if (hasScroll && inTop && this.CURRENTCHATTER && !btalk.SEARCH.IS_STATUS_SEARCH) {
                this.isLoadingMoreMessage = true;
                this.MESSAGENEXTVIEW.loadNextPageOfMessages();
            }
            if (this.isLoadingMoreMessage && current.scrollTop() != 0) {
                this.isLoadingMoreMessage = false;
            }
        },

        conversationDaysReadAllTimeout: null,
        viewAllMessageOfCurrentChatter: function (chatter) {
            if (!chatter) return;

            // TODO: Xem can sua co cach lam tuong minh hon cho truong hop nay.
            // hien message cua groupchat thay vi from la groupchat@conference.bkav.com/nickname
            // thi da duoc xy ly doi from thanh nickname@bkav.com de tien hien thi len giao dien.
            // Do do khi gui xac nhanh da xem thi can xu ly lai cho nay.
            var msg1 = $.extend({}, chatter.lastmsg);

            if (chatter.get('type') == btalk.CHATTYPE.GROUPCHAT) {
                msg1.from = chatter.get('jid');
            }

            if (!btalk.ISIDLE)
                btalk.cm.sendReceived(msg1, 'viewed');

            // Cap nhat lai trang thai da xem trong chatter
            chatter.read(true);

            // TODO: Can sua de viec update cache nay thuc hien nhanh nhat (luu lai index cua message da doc lan cuoi)
            // Cap nhat trang thai da xem trong cache danh sach message
            this.readAllMessage(chatter.get('jid'));

            this.conversationDaysReadAllTimeout = setTimeout(function () {
                // TODO: Can sua de viec readall nay thuc hien nhanh nhat
                // Cap nhat trang thai da xem trong danh sach message
                btalk.CONVERSATIONDAYS.readall();
            }, 7000);
        },

        getLastmessageStr: function (body) {
            if (!body) {
                return "";
            }
            return body.length <= 100 ? body : body.substr(0, 100);
        },

        sendMessage: function () {
            var body = this.$messageTxt.html();
            // don't send empty message
            if (body === '') {
                return;
            }

            body = btalk.text(body);

            //  DamBV - 19/01/2017
            var contentMsgReply = $('#content_msg_reply').html();
            if (contentMsgReply) {
                contentMsgReply = this.replaceEmotionByText(contentMsgReply);
            }

            var objectSendermsg = $('#sender_msg');
            var senderMsg = objectSendermsg.attr('data-sender');
            var _time = objectSendermsg.attr('data-time');
            var paramquote = null;

            if (contentMsgReply && senderMsg && _time &&
                contentMsgReply != "" && senderMsg != "" && _time != "") {
                paramquote = {};
                paramquote.contentmsg = contentMsgReply;
                paramquote.sendermsg = senderMsg;
                paramquote.timemsg = _time;
                paramquote.body = body;
                $('#input_replymsg').html('');
            }
            this.CURRENTCHATTER.temporaryQuote = null;

            // Trong TH la tin tra loi thi tim gui luon object quote di.
            if (paramquote != null) {
                this.sendMessage2(paramquote);
            } else {
                this.sendMessage2(body);
            }
        },

        sendMessage2: function (parambody) {
            // Danh dau trang thai dung go khi vua gui tin di
            this.isComposing = false;
            var quote = null;
            var body;

            if (typeof parambody == "string") {
                body = btalk.text(parambody);
            } else {
                quote = parambody;
                body = btalk.text(parambody.body);
            }

            var _body = body.toLowerCase();
            // Ap dun cho Chat ho tro khach hang tren Bmail.
            if (btalk.ChatBmail && (_body === "closed chat" || _body === "close chat")) {
                btalk.ChatBmail.sendMessageFinishConversation();
                this.$messageTxt.empty();
                this.$messageTxt.focus();
                return;
            }

            if (!this.CURRENTCHATTER) {
                // Chua chon nguoi chat cung thi return
                return;
            }

            // Minh chu dong chat thi cuon scroll xuong duoi cung
            if (this.scrollBottom() != 0) {
                this.scrollBottom(0);
            }

            if (this.$chatterPanelOuterOuter.scrollTop() != 0) {
                this.$chatterPanelOuterOuter.scrollTop(0);
            }

            // Kiem tra neu chatter co isResultOfSearch === true thi set ve false
            if (this.CURRENTCHATTER.get('isResultOfSearch') === true) {
                this.CURRENTCHATTER.set({ isResultOfSearch: false });
            }

            // Cho nay da toi uu de chi move len top sau lan chat dau tien
            if (!this.CURRENTCHATTER.isAtTop()) {
                this.CURRENTCHATTER.isAtTop(true);
            }

            // noscript, only text, html encode
            if (!body) {
                return;
            }

            var _lastmessagestr = this.getLastmessageStr(body);

            this.CURRENTCHATTER.set({
                lastmessage: _lastmessagestr,
                lasttimestamp: new Date(),
                isme: true, isSeenLastMessage: false
            });

            if (quote != null) {
                btalk.cm.sendMessage(quote, this.CURRENTCHATTER.get('jid'), null,
                // .bind(param1, parame2): param1 la ngu canh, param2 la tham so dau tien (chatter) cua this.printAndSaveMessage
                this.printAndSaveMessage.bind(this, this.CURRENTCHATTER));
                this.$messageTxt.empty();

                this.showNotificationEgov(quote);
            } else {
                btalk.cm.sendMessage(body, this.CURRENTCHATTER.get('jid'), null,
                    // .bind(param1, parame2): param1 la ngu canh, param2 la tham so dau tien (chatter) cua this.printAndSaveMessage
                    this.printAndSaveMessage.bind(this, this.CURRENTCHATTER));
                this.$messageTxt.empty();
                this.showNotificationEgov(body);
            }

            // this.$messageTxt.empty();
            this.$messageTxt.focus();
            if (btalk.isMobile) {
                window.moveAction();
            }
            return false;
        },

        isComposing: false,
        sendComposing: function () {
            // Gui message bao trang thai dang go chi khi dang go phim ki tu, phim so
            // 4 giay moi gui 1 goi tin composing di 1 lan
            if (this.isComposing === false) {
                this.isComposing = true;
                btalk.cm.sendComposing(this.CURRENTCHATTER.get('jid'), this.CURRENTCHATTER.get('type'));
                // Dat timeout, de sau 4s neu su kien soan tin nhan van phat sinh thi moi tiep tuc
                // gui message composing
                setTimeout(function () {
                    this.isComposing = false;
                }.bind(this), 4000);
            }
        },

        sendPercentage: function (prop, attachments, totalfiles, fileids, response, header) {
            var chatter;
            if (attachments[prop].to == this.CURRENTCHATTER.get('jid')) {
                chatter = this.CURRENTCHATTER;
            }
            // Gui message bao trang thai upload tai day
            if (response && response.percentage) {
                // LUU Y: response.percentage = 100 khong am chi file upload thanh cong 100%
                // No chi am chi qua trinh upload ket thuc, con khong chac la thanh cong hay loi
                btalk.cm.sendPercentage({
                    percentage: response.percentage,
                    id: attachments[prop].id,
                    messageid: attachments[prop].messageid,
                    to: attachments[prop].to,
                    tenantid: btalk.fm.gettenantid()
                }, attachments[prop].to,
                // .bind(param1, parame2): param1 la ngu canh, param2 la tham so dau tien (chatter) cua this.printAndSaveMessage
                this.printAndSaveMessage.bind(this, chatter));
            } else {
                /*
                 * TamDN - Sua lai dieu kien kiem tra file da duoc gui thanh cong len server
                 * sau khi thay the jstack bang server upfile cua egov
                 */
                // CuongNT - 14/04/2016: Su kien nay moi dam bao file hien tai duoc server bao nhan thanh cong
                var uploadCompleted = false;
                if (btalk.fm.fileServerType === "egov") {
                    if (JSON.parse(response)[0].FileName)
                        uploadCompleted = true;
                }
                else {
                    if (fileids.indexOf(attachments[prop].id) < 0)
                        uploadCompleted = true;
                }
                if (uploadCompleted) {
                    fileids.push(attachments[prop].id);
                    // 101: trang thai gui thanh cong.
                    // Vi 100: la trang thai progress upload ket thuc thoi chu khong dam bao upload thanh cong.
                    attachments[prop].percentage = 101;
                    if (btalk.fm.fileServerType != "egov")
                        attachments[prop].tenantid = btalk.fm.gettenantid();
                    attachments[prop].uploadCompleted = true;

                    // CuongNT - 14/04/2016: Gui bo sung them goi bao trang thai 101% de bao hieu la gui file da thanh cong
                    btalk.cm.sendPercentage({
                        // 101: trang thai gui thanh cong.
                        // Vi 100: la trang thai progress upload ket thuc thoi chu khong dam bao upload thanh cong.
                        percentage: 101,
                        id: attachments[prop].id,
                        messageid: attachments[prop].messageid,
                        to: attachments[prop].to,
                        tenantid: btalk.fm.gettenantid()
                    }, attachments[prop].to,
                        // .bind(param1, parame2): param1 la ngu canh, param2 la tham so dau tien (chatter) cua this.printAndSaveMessage
                        this.printAndSaveMessage.bind(this, chatter));
                }

                if (totalfiles == fileids.length) {
                    btalk.cm.sendAttachments(attachments, attachments[prop].to, chatter.get("type"),
                    // .bind(param1, parame2): param1 la ngu canh, param2 la tham so dau tien (chatter) cua this.printAndSaveMessage
                    this.printAndSaveMessage.bind(this, chatter), true);

                    //  DamBV - 30/03/2017: neu gui tin nhan thanh cong thi cung tu dong tai dap tin moi ve.
                    var typeFile = "file";
                    if (attachments[prop].type.indexOf('image') > -1) {
                        typeFile = "image";
                    }
                    this.resfreshShareFileInConversation(typeFile, this.CURRENTCHATTER);
                }
            }
        },

        sendAttachmentError: function (prop, attachments, totalfiles, fileids, error) {
            var chatter;
            if (attachments[prop].to == this.CURRENTCHATTER.get('jid')) {
                chatter = this.CURRENTCHATTER;
            }

            if (fileids.indexOf(attachments[prop].id) < 0) {
                fileids.push(attachments[prop].id);
            }
            // -1: trang thai gui loi
            attachments[prop].percentage = -1;

            // CuongNT - 14/04/2016: Gui bo sung them goi bao trang thai -1% de bao hieu la gui file bi loi
            btalk.cm.sendPercentage({
                // -1: trang thai gui loi
                percentage: -1,
                id: attachments[prop].id,
                messageid: attachments[prop].messageid,
                to: attachments[prop].to,
                tenantid: btalk.fm.gettenantid(),
            }, attachments[prop].to,
                // .bind(param1, parame2): param1 la ngu canh, param2 la tham so dau tien (chatter) cua this.printAndSaveMessage
                this.printAndSaveMessage.bind(this, chatter));

            if (totalfiles == fileids.length) {
                btalk.cm.sendAttachments(attachments, attachments[prop].to, chatter.get("type"),
                // .bind(param1, parame2): param1 la ngu canh, param2 la tham so dau tien (chatter) cua this.printAndSaveMessage
                this.printAndSaveMessage.bind(this, chatter), true);
            }
        },

        _oldestChatday: null,
        _newestChatday: null,
        printMessageTimeout: null,

        // Cap nhat lai giao dien neu la progress send file.
        updateDisplayProcessSendFile: function (oldMsg, msg) {
            var attachment = oldMsg.findAttachment(msg.percentage.id);
            if (attachment) {
                attachment.percentage(msg.percentage.percentage);
            }
        },

        // Update trang thai nhan tin nhan.
        updateStateReceiveMsg: function (oldMsg, msg) {
            oldMsg.status(msg.status);
        },

        // Hien thi phan tram doi voi tin nhan file.
        displayPercentSendMessageFile: function (msg) {
            // Lay tin can upate lai giao dien
            var oldMsg = btalk.CONVERSATIONDAYS.findMessage(msg.id);
            if (oldMsg) {
                // 1.1.	Bao % gui file
                if (msg.processType == "percentage") {
                    this.updateDisplayProcessSendFile(oldMsg, msg);
                    return msg;
                }

                // 1.2.	Bao trang thai gui/nhan tin
                if (msg.processType == "received") {
                    this.updateStateReceiveMsg(oldMsg, msg);
                    return msg;
                }
            }
        },

        // DamBV - 06/07/2017 : xu li doi voi tin nhan config.
        checkMessageConfigInGroup: function (msg) {
            /*
             * TamDN - 12/12/2016
             * Neu la tin nhan roomconfig thi set lai content
             */
            var isConfigMsg = false;
            if (msg.processType == "roomconfig" && msg.config) {
                var editBy;
                var roomName;
                var newMembers;
                var removeMember;
                var bodyMsgSuggestchatBmail;

                if (msg.config.body_msg_suggest_chatBmail)
                    bodyMsgSuggestchatBmail = msg.config.body_msg_suggest_chatBmail;

                if (msg.config.forEach)
                    msg.config.forEach(function (item) {
                        if (item.edit_by)
                            editBy = item.edit_by;
                        if (item.roomconfig_roomname)
                            roomName = item.roomconfig_roomname;
                        if (item.new_members)
                            newMembers = item.new_members;
                        if (item.remove_member)
                            removeMember = item.remove_member;
                    });
                if (roomName) {
                    msg.body = "<b>" + editBy + "</b> đã đổi tên nhóm thành \"" + roomName + "\"";
                    isConfigMsg = true;
                }
                if (newMembers) {
                    msg.body = "<b>" + editBy + "</b> đã thêm " + newMembers + " vào nhóm";
                    isConfigMsg = true;
                }
                if (removeMember) {
                    msg.body = "<b>" + editBy + "</b> đã xoá " + removeMember + " khỏi nhóm";
                    isConfigMsg = true;
                }

                // Khi client KH nhan duoc tin nhan cu
                if (btalk.ChatBmail && bodyMsgSuggestchatBmail) {
                    msg.body = bodyMsgSuggestchatBmail;
                    isConfigMsg = true;
                    //btalk.ChatBmail.resetDisplayDefault();
                }
            }
            msg.isConfigMsg = isConfigMsg;
            return msg;
        },

        /**
         * Ve tin nhan len giao dien.
         *
         * Co 3 truong hop can ve tin len giao dien:
         * - Tin nhan online (Tin nhan nhan khi dang online).
         * - Tin nhan offline
         * - Tin nhan lay tu lich su tin nhan
         *
         * Co n truong hop can cap nhat giao dien:
         * - Tin bao trang thai gui nhan
         * - Tin bao % gui file
         */
        printMessage: function (msg, fromCache, fromArchive) {
            var inBottom = this.scrollBottom();     // Bien xac dinh vi tri bottom hien tai cua tin phan noi dung tin nhan.

            // Xac dinh user/member da gui message de hien thi len giao dien
            if (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.from.indexOf(btalk.cm.options.xmpp.conference) > 0) {
                // <message from="groupname@conference.bkav.com/dungha" to="cuongnt@bkav.com/Ubuntu-14.04".../>
                var msgChatter = btalk.getResource(msg.from);
                if (msgChatter == "") {
                    console.log("Message groupchat khong co nickname nguoi chat gui toi!");
                    return;
                }
                msg.senderJid = msgChatter + '@' + btalk.cm.options.xmpp.domain;
                // Nguoi nhan luon la group chat
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

            // Neu co thong tin gui file thi dam bao luon o dang array
            if (msg.attachment) {
                msg.attachment = $.isArray(msg.attachment) ? msg.attachment : [msg.attachment];
            }

            // Encode html truoc khi hien thi len giao dien
            msg.body = btalk.text(msg.body);

            // Xac dinh kich ban xu ly tiep theo cua goi tin message
            msg.processType = this._checkProcessTypeOfMessage(msg);

            // CAP NHAT GIAO DIEN: VOI MESSAGE BAO % GUI FILE && BAO TRANG THAI GUI/NHAN TIN
            if (msg.processType == "percentage" || msg.processType == "received") {
                this.displayPercentSendMessageFile(msg);
                return;
            }

            // VE MOI GIAO DIEN: LA MESSAGE THONG THUONG HOAC MESSAGE CHUA DANH SACH FILE DA GUI THANH CONG
            // Neu message khong phai tin nhan hoac bao danh sach file da gui thanh cong
            // hoac la message chat (type=chat|groupchat) ma body rong thi return
            if ((msg.processType != "file" && msg.processType != btalk.CHATTYPE.CHAT && msg.processType != "roomconfig") ||
                 (msg.processType == btalk.CHATTYPE.CHAT && (!msg.body || msg.body === ''))) {
                return;
            }

            // Xu li neu la tin nhan config, them truong isConfigMsg.
            msg = this.checkMessageConfigInGroup(msg);

            // DamBV: FIX TAM, neu khong co thoi gian server thi se lay thoi gian client.
            msg.servertime = msg.servertime || new Date(parseInt(msg.clienttime));

            // Tim ngay ma tin nhan thuoc vao
            fromCache = fromCache || false;
            fromArchive = fromArchive || false;
            var fromCacheOrArchive = fromCache || fromArchive;
            var chatDay = null;
            if (fromCacheOrArchive) {
                chatDay = btalk.CONVERSATIONDAYS.at(btalk.CONVERSATIONDAYS.length - 1);
            } else {
                chatDay = btalk.CONVERSATIONDAYS.at(0);
            }

            // Neu chat day khong ton tai
            // Hoac tin nhan vua nhan khong thuoc chatDay hien tai => tao chatDay moi            
            if (!chatDay || btalk.diffDays(chatDay.get('historyDay'), msg.servertime) != 0) {
                var _chatDay = null;
                if (fromCacheOrArchive) {
                    _chatDay = { historyDay: msg.servertime, append: false };
                    btalk.CONVERSATIONDAYS.push(_chatDay);
                    chatDay = btalk.CONVERSATIONDAYS.at(btalk.CONVERSATIONDAYS.length - 1);
                } else {
                    _chatDay = { historyDay: msg.servertime, append: true };
                    btalk.CONVERSATIONDAYS.unshift(_chatDay);
                    chatDay = btalk.CONVERSATIONDAYS.at(0);
                }
            }

            // Tim chatter (nguoi gui tin nhan) cuoi cung
            var _memberIndex = fromCacheOrArchive ? chatDay.members.length - 1 : 0;
            var lastChatter = chatDay.members.at(_memberIndex);
            var isme = msg.senderJid.toLowerCase() == btalk.ROSTER.currentuser.jid;

            if (!lastChatter || lastChatter.get('account') != msg.senderJid.split('@')[0]) {
                // La nguoi chat moi thi tao moi va them vao
                var online = false;
                if (!isme) {
                    var user = btalk.ROSTER.getUserByJID(msg.senderJid);
                    online = user && user.status == 'available' ? true : false;
                }
                var _member = {
                    account: msg.senderJid.split('@')[0],
                    timestamp: msg.servertime,
                    append: !fromCacheOrArchive,
                    online: online,
                    isme: isme,
                    isConfigMsg: msg.isConfigMsg
                };

                if (fromCacheOrArchive) {
                    chatDay.members.push(_member);
                } else {
                    chatDay.members.unshift(_member);
                }
                _memberIndex = fromCacheOrArchive ? chatDay.members.length - 1 : 0;
                lastChatter = chatDay.members.at(_memberIndex);
            }

            if (fromCacheOrArchive) {
                // Luon cap nhat de co duoc thoi gian dau tien minh bat dau chat
                // [TODO] xu ly toi uu hon: sao cho chi set khi la tin cuoi cung vua duoc lay hoac la tin cuoi truoc khi chuyen sang member khac
                lastChatter.set({ timestamp: msg.servertime });
            }

            // [TODO] Toi uu: moi khi gui message di ma check body co emoticon thi them attr danh dau,
            // giup viec xu ly emoticon.process chi thuc hien tren cac message nay se toi uu hon
            // Xu ly emoticon (chua html: <img nen phai xu ly sau "Loai bo HTML")
            var _body = btalk.emoticon.process(msg.body);

            //  DamBV - 16/12/2016 - xu li neu trong text co link.
            _body = btalk.autolink.process(_body);

            var _imgCount = 0;
            var _fileCount = 0;
            if ($.isArray(msg.attachment)) {
                for (var i = 0; i < msg.attachment.length; i++) {
                    if (msg.attachment[i].type.indexOf('image') >= 0 &&
                        // Khong phai file anh gui bi loi
                        msg.attachment[i].percentage != -1) {
                        _imgCount++;
                    } else {
                        _fileCount++;
                    }
                }
            }
            var _message = {
                isme: isme,
                message: _body,
                append: !fromCacheOrArchive,
                // Neu la tin do minh gui di thi moi check trang thai/
                // [TODO] Check them truong hop tu minh gui cho chinh minh
                status: msg.status,
                id: msg.id,
                // Neu dang forcus thi khong can hien style chua doc
                unread: btalk.WINDOWFOCUS === true && !fromCacheOrArchive ? false : msg.unread,
                // Test preview file
                imageColumn: _imgCount <= 3 ? _imgCount : 3,
                imageCount: _imgCount,
                otherCount: _fileCount,
                senderJid: msg.senderJid,
                receiverJid: msg.receiverJid,
                account: msg.senderJid.split('@')[0],
                isConfigMsg: msg.isConfigMsg,
                timestamp: msg.servertime,

                contentmsg_quote: null,
                sendermsg_quote: null,
                timemsg_quote: null,
            };

            /* TamDN - 30/6/2017 - Load danh sach thanh vien da xem tin nhan tu cache */
            _message.viewedBy = btalk.VIEWED_CACHE_LIST.getViewedListByMsgId(msg.chatterJid, msg.id);
            //  DamBV - 09/02/2017 - Bo sung phan quote.
            if (msg.quote) {
                _message.contentmsg_quote = btalk.emoticon.process(msg.quote.contentmsg);
                _message.sendermsg_quote = msg.quote.sendermsg;
                var _date = new Date(parseInt(msg.quote.timemsg));
                _message.timemsg_quote = btalk.getCoolTime2(_date);
            }

            //  DamBV-19/12/2016- them truong cho kiem tra cho tin nhan cua nhom
            if (msg.type === btalk.CHATTYPE.GROUPCHAT) {
                _message.isMsgGroup = true;
            }

            //  DamBV - 21/04/2017 : Xu li highlight
            if (btalk.SEARCH.IS_STATUS_SEARCH && btalk.SEARCH.TYPE_SEARCH == "textMsg" && this.valueSearch != null) {
                var hightlight = "<mark style='back'>" + this.valueSearch + "</mark>";
                _message.message = _message.message.replace(this.valueSearch, hightlight);
            }

            var currentMessage = null;
            /* Tao lien ket dong giua cac message */
            if (fromCacheOrArchive) {
                // [TODO]: Xu ly chen message theo thu tu thoi gian
                lastChatter.messages.push(_message);
                currentMessage = lastChatter.messages.models[lastChatter.messages.models.length - 1];
                // Neu 2 tin dang xet thuoc cung 1 member -> Tim message co index nho hon 1 don vi
                if (lastChatter.messages.models.length > 1) {
                    lastChatter.messages.models[lastChatter.messages.models.length - 1].newer = lastChatter.messages.models[lastChatter.messages.models.length - 2];
                    lastChatter.messages.models[lastChatter.messages.models.length - 2].older = lastChatter.messages.models[lastChatter.messages.models.length - 1];
                } else {
                    // Neu 2 tin dang xet thuoc 2 member lien tiep
                    // Neu 2 member trong cung 1 ngay -> tim member co index nho hon 1 don vi
                    var _newerMemberIndex = chatDay.members.length - 2;
                    if (_newerMemberIndex >= 0) {
                        lastChatter.messages.models[lastChatter.messages.models.length - 1].newer =
                            chatDay.members.models[_newerMemberIndex].messages.models[chatDay.members.models[_newerMemberIndex].messages.models.length - 1];

                        chatDay.members.models[_newerMemberIndex].messages.models[chatDay.members.models[_newerMemberIndex].messages.models.length - 1].older =
                            lastChatter.messages.models[lastChatter.messages.models.length - 1];
                    } else {
                        // Neu 2 member trong 2 ngay khac nhau -> tim day co index nho hon 1 don vi, hoac tim day lon hon 1 ngay
                        var _newerchatDay = _.find(btalk.CONVERSATIONDAYS.models, function (item) {
                            return btalk.diffDays(msg.servertime, item.get('historyDay')) == 1;
                        });
                        if (_newerchatDay) {
                            _newerMemberIndex = _newerchatDay.members.length - 1;
                            lastChatter.messages.models[lastChatter.messages.models.length - 1].newer =
                                _newerchatDay.members.models[_newerMemberIndex].messages.models[_newerchatDay.members.models[_newerMemberIndex].messages.models.length - 1];
                            _newerchatDay.members.models[_newerMemberIndex].messages.models[_newerchatDay.members.models[_newerMemberIndex].messages.models.length - 1].older =
                                lastChatter.messages.models[lastChatter.messages.models.length - 1];
                        }
                    }
                }
            } else {
                // [TODO]: Xu ly chen message theo thu tu thoi gian
                lastChatter.messages.unshift(_message);
                currentMessage = lastChatter.messages.models[0];
                // Neu 2 tin dang xet thuoc cung 1 member -> Tim message co index lon hon 1 don vi
                if (lastChatter.messages.models.length > 1) {
                    lastChatter.messages.models[0].older = lastChatter.messages.models[1];
                    lastChatter.messages.models[1].newer = lastChatter.messages.models[0];
                } else {
                    // Neu 2 tin dang xet thuoc 2 member lien tiep
                    var _olderMemberIndex = 1;
                    // Neu 2 member trong cung 1 ngay -> tim member co index nho lon 1 don vi
                    if (chatDay.members.length > 1) {
                        lastChatter.messages.models[0].older = chatDay.members.models[_olderMemberIndex].messages.models[0];
                        chatDay.members.models[_olderMemberIndex].messages.models[0].newer = lastChatter.messages.models[0];
                    } else {
                        // Neu 2 member trong 2 ngay khac nhau -> tim day co index lon hon 1 don vi, hoac tim day nho hon 1 ngay
                        var _olderchatDay = _.find(btalk.CONVERSATIONDAYS.models, function (item) {
                            return btalk.diffDays(msg.servertime, item.get('historyDay')) == 1;
                        });
                        if (_olderchatDay) {
                            _olderMemberIndex = 0;
                            lastChatter.messages.models[0].older = _olderchatDay.members.models[_olderMemberIndex].messages.models[0];
                            _olderchatDay.members.models[_olderMemberIndex].messages.models[0].newer = lastChatter.messages.models[0];
                        }
                    }
                }
            }

            // Chen danh sach file neu la tin nhan gui file
            if (msg.attachment && currentMessage.attachments) {
                //var senderFileAcc = msg.senderJid;
                var receiverFileAcc = msg.receiverJid;
                // Cac file loai image/*
                for (var k = 0; k < msg.attachment.length; k++) {
                    // currentMessage cho nay dang co the bi null
                    var senderTenantId = msg.attachment[k].tenantid || "";
                    currentMessage.attachments.add({
                        // file prop
                        name: msg.attachment[k].name,
                        type: msg.attachment[k].type,
                        // url: co the la undefined neu server file chua auth thanh cong
                        url: btalk.fm.geturl(senderTenantId, receiverFileAcc, msg.attachment[k].object, msg.attachment[k].name, msg.attachment[k].sentDate, msg.attachment[k].percentage),
                        // othr prop
                        id: msg.attachment[k].id,
                        percentage: parseInt(msg.attachment[k].percentage) || 0,
                        status: -1,
                        statusText: "Uploading",
                        from: msg.senderJid,
                        to: msg.receiverJid,
                        tenantid: senderTenantId
                    });
                }
            }

            // GUI XAC NHAN TIN DA XEM (NEU TIN DO NGUOI KHAC GUI TOI && CHUA XEM)
            // Co 4 nguon printMessage:
            // - Tu message luu tru tren server
            // - Tu minh gui di
            // - Tu message luu tru trong cache client => neu la tin chua doc => sendReceived
            // - Tu nguoi khac gui toi => mac dinh la tin chua doc => sendReceived
            // Cua so chat dang focus
            // La tin chua doc
            if (btalk.WINDOWFOCUS && msg.unread === true) {
                // Gui xac nhan da xem khi thoa man cac dieu kien tren
                // [TODO] xet dieu kien neu la tai tin tu cache thi chi gui 1 goi xac nhan da xem neu co nhieu tin chua doc
                var msg1 = $.extend({}, msg); // clone
                if (this.CURRENTCHATTER.get('type') == btalk.CHATTYPE.GROUPCHAT) {
                    msg1.from = this.CURRENTCHATTER.get('jid');
                }

                if (!btalk.ISIDLE) {
                    btalk.cm.sendReceived(msg1, 'viewed');
                }

                msg.unread = false;
                // CAP NHAT TRANG THAI UNREAD NEU LA MESSAGE TU CACHE
                // Day la vi tri duy nhat co the xet trang thai tin da doc hay chua
                if (fromCache === true) {
                    // Cap nhat trang thai read vao cache
                    this.readMessage(msg, this.CURRENTCHATTER.get("jid"));
                }
            }

            /*
             * CAP NHAT LAI SCROLL
             * Dung gia tri 5 de fix loi thinh thoan inBottom = 1|2.... chua ro do dau
             * Edit DamBV - 31/03/2017 : Xu li lai , de phu hop khi tim kiem.
             */
            if (!btalk.SEARCH.IS_STATUS_SEARCH && !fromCacheOrArchive && inBottom <= 10) {
                // Kich ban ban dau dung de scroll khi dang chat.
                this.$messagePanelOuter.scrollBottom(0);
            }
            else if (btalk.SEARCH.IS_STATUS_SEARCH && btalk.SEARCH.TOP_LOAD_MSG) {
                this.$messagePanelOuter.scrollBottom(btalk.SEARCH.SCROLL_BOTTOM);
            }
            else if (btalk.SEARCH.IS_STATUS_SEARCH && !btalk.SEARCH.TOP_LOAD_MSG) {
                this.$messagePanelOuter.scrollTop(btalk.SEARCH.SCROLL_TOP);
            }
            return msg;
        },

        /*
         LUU Y: chatter duoc truyen vao function nay bang param2 trong lenh .bind(param1, param2);
         CuongNT - 19/04/2016: them param chatter de fix loi, vd: dang gui file cho QuangLH thi click
         sang TaiMV xem => Trang thai gui file nay hien vao thread chat cua TaiMV.
        */
        printAndSaveMessage: function (chatter, msg, fromCache, fromArchive) {
            msg.unread = false;
            var saveMsg;
            if (!chatter) {
                saveMsg = msg;
            } else {
                saveMsg = this.printMessage(msg, fromCache, fromArchive);
            }
            // Neu la cac goi tin khong hop le, khong can ve len giao dien
            if (!saveMsg) {
                return;
            }
            // Xac dinh doi tuong cache se luu tin nay
            // TODO: Can sua lai cho nay, chi voi tin do minh chat di moi gan truc tiep vay. Nguoc lai thi can giu nguyen
            saveMsg.chatterJid = saveMsg.chatterJid || saveMsg.to;
            this.saveMessage(saveMsg, false);
        },

        // ! Notification panel
        getNotificationIcon: function (account) {
            return btalk.view.getAvatar([account]);
        },

        notificationClick: function (e) {
            this.clearNotification();

            // TODO: forcus ca vao chatter lien quan toi notify dang click
            var jid = e.target && e.target.jid ? e.target.jid : e.chatterJid;
            if (jid != this.CURRENTCHATTER.get('jid')) {
                $(window).focus();

                var chatter = btalk.CHATTERS.findWhere({ jid: jid });
                if (chatter) {
                    chatter.set({ isselected: true });
                } else {
                    console.log("Notification click: Khong tim thay chatter tuong ung.");
                }
            } else {
                // Phai goi sau de dam bao viec chuyen doi chatter selected da hoan thanh
                $(window).focus();
            }
            // TODO: Tam thoi de day, sau can xu ly tong quat hon
            if (btalk.config.CLIENT.CLIENT_TYPE == 'egov') {
                if (parent && parent.helper && parent.helper.displayApp && typeof parent.helper.displayApp === "function") {
                    parent.helper.displayApp('chat', function () { });
                }
            }
        },

        clearNotification: function () {
            btalk.ntf.clear();
        },

        /* MESSAGE CACHE - QUAN LY CACHE TIN NHAN
        ---------------------------------------------------------------------- */
        /**
         * Co 3 loai message:
         * - message chat thong thuong: <message type="chat" hoac <message type="groupchat"
         * 		+ chat 1-1: <message type="chat"
         * 		=> Dieu kien nhan biet: msg.type == "chat" && msg.attachment == null
         * 		=> @result: chat
         *
         * 		+ chat nhom: <message type="groupchat"
         * 		=> Dieu kien nhan biet: msg.type == "grouchat" && msg.attachment == null
         * 		=> @result: chat
         *
         * 		+ gui file 1-1: <message type="chat"..><attachment name="".../></message>
         * 		=> Dieu kien nhan biet: msg.type == "chat" && msg.attachment != null
         * 		=> @result: file
         *
         * 		+ gui file nhom: <message type="groupchat"..><attachment name="".../></message>
         * 		=> Dieu kien nhan biet: msg.type == "chat" && msg.attachment != null
         * 		=> @result: file
         *
         * - message chat bao trang thai gui/nhan: <message type="received"
         * => Dieu kien nhan biet: msg.type == "received"
         * => @result: received
         *
         * - message chat bao % gui file: <message type="file"
         * => Dieu kien nhan biet: msg.type == "file"
         * => @result: percentage
         *
         * @result: chat, file, percentage, received
         */
        _checkProcessTypeOfMessage: function (msg) {
            if (msg.received) {
                return "received";
            } else if ((msg.type == btalk.CHATTYPE.GROUPCHAT && msg.config != undefined)) {
                return "roomconfig";
            } else if ((msg.type == btalk.CHATTYPE.CHAT && msg.body && !msg.attachment) || (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.body && !msg.attachment)) {
                return btalk.CHATTYPE.CHAT;
            } else if ((msg.type == btalk.CHATTYPE.CHAT && msg.attachment != undefined) || (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.attachment)) {
                return "file";
            } else if ((msg.type == btalk.CHATTYPE.CHAT && msg.percentage != undefined) || (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.percentage)) {
                return "percentage";
            } else if (((msg.type == btalk.CHATTYPE.CHAT || msg.type == btalk.CHATTYPE.GROUPCHAT) && msg.composing != undefined)) {
                return "composing";
            } else {
                console.log("WARNING: MESSAGE NAY KHONG DUOC NHAN DIEN");
                //console.log(msg);
                return "";
            }
        },

        /*
         * Kiem tra cache chua khoi tao thi kho tao
         */
        _checkCache: function (cacher, toChatter) {
            if (!cacher.messageCaches) {
                cacher.messageCaches = {
                    type: 'result',
                    to: btalk.ROSTER.currentuser.jid,
                    id: (new Date()).getTime(),
                    chat: {
                        with: toChatter,
                        start: new Date(this.options.baseDatetimeQuery),
                        xmlns: "urn:xmpp:archive",
                        fromAndTo: [],
                        set: {
                            first: 0,
                            index: 0,
                            last: 0,
                            count: 0
                        }
                    }
                };
            }

            if (!cacher.messageCaches.chat) {
                cacher.messageCaches.chat = {
                    fromAndTo: [],
                    set: {
                        first: 0,
                        index: 0,
                        last: 0,
                        count: 0
                    }
                };
            }
        },

        _saveCacheInfo: function (toChatter, _set) {
            toChatter = btalk.cutResource(toChatter);
            var cacher = btalk.CHATTERS.findWhere({ jid: toChatter });
            if (!cacher) {
                return;
            }
            this._checkCache(cacher, toChatter);

            cacher.chat.set.first = _set.first;
            cacher.chat.set.index = _set.index;
            cacher.chat.set.last = _set.last;
            cacher.chat.set.count = _set.count;
        },

        // Cac nguon message can luu cache:
        // - Tu kho lich su tin nhan tren server
        // - Tu nguoi khac chat sang
        // - Tu minh chat di
        // Cache tin nhan de khong phai tai lai tu server
        // [TODO] Chuyen quan ly cache nay xuong duoi lop roster.js quan ly. Tang view khong quan ly viec nay.
        saveMessage: function (msg, fromCacheOrArchive) {
            var cacher, toChatter;
            if (fromCacheOrArchive === true) {
                var msgArr = $.isArray(msg) === true ? msg : [msg];
                toChatter = btalk.cutResource(msgArr[0].chatterJid);
                cacher = btalk.CHATTERS.findWhere({ jid: toChatter });
                if (!cacher) {
                    return;
                }
                // Kiem tra cache chua khoi tao thi kho tao
                this._checkCache(cacher, toChatter);
                if (cacher.messageCaches.chat.fromAndTo.length <= this.options.maxMessagesInCache) {
                    cacher.messageCaches.chat.fromAndTo = cacher.messageCaches.chat.fromAndTo.concat(msgArr);
                }
                return;
            }

            // Neu la message la groupchat va from la dang: groupname@conference.bkav.com/dungha
            // thi thuc hien chuyen from ve chinh chatter dang gui: dungha@bkav.com
            if (msg.type == btalk.CHATTYPE.GROUPCHAT && msg.from.indexOf(btalk.cm.options.xmpp.conference) > 0) {
                var msgChatter = btalk.getResource(msg.from);
                if (msgChatter == "") {
                    console.log("message groupchat khong co nickname nguoi chat gui toi!");
                    return;
                }
                msg.from = msgChatter + '@' + btalk.cm.options.xmpp.domain;// + '/' + msgChatter.split('@')[0];
            }

            toChatter = msg.chatterJid;
            cacher = btalk.CHATTERS.findWhere({ jid: toChatter });
            if (!cacher) {
                return;
            }

            // Kiem tra cache chua khoi tao thi kho tao
            this._checkCache(cacher, toChatter);

            // [TODO]: chi cache toi da voi 30 nguoi chat cuoi cung. Config duoc.
            if (!cacher.messageCaches.chat.fromAndTo) {
                cacher.messageCaches.chat.fromAndTo = [];
            }

            msg.processType = this._checkProcessTypeOfMessage(msg);

            //Dựa vào tin nhắn đến để cập nhật tin đã xem
            //Chỉ update cho các message dạng chat, roomconfig, hoặc tin gửi file thành công
            if (msg.processType == "chat" || msg.processType == "roomconfig" ||
                    (msg.processType == "percentage" && msg.percentage &&
                        msg.percentage.percentage && msg.percentage.percentage == "101")) {
                if (msg.senderJid != btalk.auth.getJID())
                    btalk.VIEWED_CACHE_LIST.updateAndSaveToCache(msg.chatterJid, msg.from, msg.id, msg.secs);
            }

            // 1.	CAP NHAT CACHE: VOI MESSAGE BAO % GUI FILE && BAO TRANG THAI GUI/NHAN TIN
            if (msg.processType == "percentage" || msg.processType == "received") {
                for (var i = 0; i < cacher.messageCaches.chat.fromAndTo.length; i++) {
                    // Neu tin nay co id da co trong cache => xet xem tin nay update gi va return luon
                    if (msg.processType == "percentage" &&
                            cacher.messageCaches.chat.fromAndTo[i].id == msg.id &&
                            cacher.messageCaches.chat.fromAndTo[i].clienttime <= msg.clienttime) {
                        // Neu la tin cap nhat trang thai % gui file => update percentage
                        // Cac message cap nhat trang thai % gui file co id giong nhau
                        if (cacher.messageCaches.chat.fromAndTo[i].percentage) {
                            for (var j = 0; j < cacher.messageCaches.chat.fromAndTo[i].percentage.length; j++) {
                                if (cacher.messageCaches.chat.fromAndTo[i].percentage[j].id == msg.percentage.id) {
                                    cacher.messageCaches.chat.fromAndTo[i].percentage[j].percentage = msg.percentage.percentage;
                                    return;
                                }
                            }
                        }
                        return;
                    }
                    if (msg.processType == "received" &&
                            cacher.messageCaches.chat.fromAndTo[i].id == msg.received.id &&
                            cacher.messageCaches.chat.fromAndTo[i].clienttime <= msg.clienttime) {
                        // Neu la tin cap nhat trang thai nhan tin => update status
                        msg.status = msg.received.status || 'viewed';
                        if (msg.status != 'viewed') {
                            cacher.messageCaches.chat.fromAndTo[i].status = msg.status;
                        } else {
                            //TamDN - 30/6/2017 - Lưu trạng thái đã xem vào trong cache
                            btalk.VIEWED_CACHE_LIST.updateAndSaveToCache(msg.chatterJid,
                                msg.from, msg.received.id, msg.received.secs);
                        }
                        return;
                    }
                }
                return;
            }

            // 2. 	THEM MOI VAO CACHE: LA MESSAGE THONG THUONG HOAC MESSAGE GUI FILE
            if (!msg.secs) {
                var start = new Date(cacher.messageCaches.chat.start);
                var secs = ((new Date()).getTime() - start.getTime()) / 1000;
                msg.secs = secs;
            }
            msg.unread = msg.unread || false;

            /*var start = new Date(cacher.messageCaches.chat.start);
            var secs = ( (new Date()).getTime() - start.getTime() ) / 1000;
            var newMsg = {
                secs: msg.secs || secs,
                body: msg.body,
                unread: msg.unread || false,
                from: btalk.cutResource(msg.from),
                id: msg.id,
                clienttime: msg.clienttime,
                status: msg.status,
                state: msg.state,
                type: msg.type,
                carbons: msg.carbons
            };
            if ( msg.attachment ) {
                msg.attachment = $.isArray(msg.attachment) ? msg.attachment : [msg.attachment];
                newMsg.attachment = [];
                for ( var i = 0; i < msg.attachment.length; i++ ) {
                    newMsg.attachment.push({
                        name: msg.attachment[i].name,
                        id: msg.attachment[i].id,
                        messageid: msg.attachment[i].messageid,
                        percentage: msg.attachment[i].percentage
                    });
                }
            }*/
            if (fromCacheOrArchive === true) {
                if (cacher.messageCaches.chat.fromAndTo.length <= this.options.maxMessagesInCache) {
                    cacher.messageCaches.chat.fromAndTo.push(msg);
                }
            } else {
                cacher.messageCaches.chat.fromAndTo.unshift(msg);
                if (cacher.messageCaches.chat.fromAndTo.length > this.options.maxMessagesInCache) {
                    // Xu ly remove tin cu nhat di neu qua cache size
                    var lastmsg = cacher.messageCaches.chat.fromAndTo[cacher.messageCaches.chat.fromAndTo.length - 1];
                    // [TODO] KHONG THUC HIEN XOA VOI CAC LOAI TIN SAU:
                    // 1. tin chua doc
                    // 2. tin cho xac nhan trang thai gui
                    // 3. tin bao trang thai dang gui file
                    if (lastmsg.unread != true) {
                        cacher.messageCaches.chat.fromAndTo.pop();
                    }
                }
            }
        },

        readMessage: function (msg, toChatter) {
            toChatter = btalk.cutResource(toChatter);
            var cacher = btalk.CHATTERS.findWhere({ jid: toChatter });
            if (!cacher) {
                return;
            }
            // Kiem tra cache chua khoi tao thi kho tao
            this._checkCache(cacher, toChatter);

            if (!cacher.messageCaches.chat.fromAndTo) {
                cacher.messageCaches.chat.fromAndTo = [];
            }
            for (var i = 0; i < cacher.messageCaches.chat.fromAndTo.length; i++) {
                if (cacher.messageCaches.chat.fromAndTo[i].id == msg.id &&
                            cacher.messageCaches.chat.fromAndTo[i].clienttime <= msg.clienttime) {
                    cacher.messageCaches.chat.fromAndTo[i].unread = false;
                    return;
                }
            }
        },

        /*
         * Cap nhat tat ca tin trong cache ve da doc.
         * Goi toi ham nay khi:
         * - Forcus lai cua so chat + giao dien tin nhan co tin chua doc
         */
        readAllMessage: function (toChatter) {
            toChatter = btalk.cutResource(toChatter);
            var cacher = btalk.CHATTERS.findWhere({ jid: toChatter });
            if (!cacher) {
                return;
            }
            // Kiem tra cache chua khoi tao thi kho tao
            this._checkCache(cacher, toChatter);

            if (!cacher.messageCaches.chat.fromAndTo) {
                cacher.messageCaches.chat.fromAndTo = [];
            }
            for (var i = 0; i < cacher.messageCaches.chat.fromAndTo.length; i++) {
                cacher.messageCaches.chat.fromAndTo[i].unread = false;
            }
        },

        /*
         * Cap nhat tat ca tin trong cache ve da xem.
         * Goi toi ham nay khi:
         * Nhan duoc goi tin bao da xem tu chatter (co phai la current chatter hay khong)
         */
        viewedAllMessage: function (toChatter) {
            toChatter = btalk.cutResource(toChatter);
            var cacher = btalk.CHATTERS.findWhere({ jid: toChatter });
            if (!cacher) {
                return;
            }
            // Kiem tra cache chua khoi tao thi khoi tao
            this._checkCache(cacher, toChatter);

            if (!cacher.messageCaches.chat.fromAndTo) {
                cacher.messageCaches.chat.fromAndTo = [];
            }

            var finish = false;
            for (var i = 0; i < cacher.messageCaches.chat.fromAndTo.length; i++) {
                cacher.messageCaches.chat.fromAndTo[i].status = '';
            }

            //Cập nhật icon đã xem ở danh sách chatter
            this.setStatusViewedMessage(toChatter);
        },

        /* Trả về danh sách tin nhắn chưa gửi thành công lên server */
        getClientStatusMessages: function (cacher) {
            //toChatter = btalk.cutResource(toChatter);
            //var cacher = btalk.CHATTERS.findWhere({ jid: toChatter });
            if (!cacher) {
                return;
            }
            // Kiem tra cache chua khoi tao thi kho tao
            this._checkCache(cacher);

            if (!cacher.messageCaches.chat.fromAndTo) {
                cacher.messageCaches.chat.fromAndTo = [];
            }
            var result = [];
            for (var i = cacher.messageCaches.chat.fromAndTo.length - 1; i >= 0; i--) {
                if (cacher.messageCaches.chat.fromAndTo[i].status === "client") {
                    result.push(cacher.messageCaches.chat.fromAndTo[i]);
                } else {
                    // TODO: break neu gap tin da toi server dau tien
                }
            }
            return result;
        },

        resendClientStatusMessagesToChatter: function (toChatter) {
            var resendMsgs = this.getClientStatusMessages(toChatter);
            for (var i = 0; i < resendMsgs.length; i++) {
                btalk.cm.sendMessage(resendMsgs[i].body, resendMsgs[i].to, resendMsgs[i].id);
            }
        },

        resendClientStatusMessages: function () {
            btalk.CHATTERS.each(function (toChatter) {
                this.resendClientStatusMessagesToChatter(toChatter);
            }.bind(this));
        },

        // 3. Chua ra soat not
        error_messages: new Array(),
        errorW: null,

        /* Luu lich su go noi dung chat */
        messageHistory: new Array(),
        historyIndex: 0,
        getHistory: function (key, message) {
            if ((key == "up") && (historyIndex > 0))
                historyIndex--;
            if ((key == "down") && (historyIndex < messageHistory.length))
                historyIndex++;
            if (historyIndex >= messageHistory.length) {
                if (historyIndex == messageHistory.length)
                    return '';
                return message;
            } else {
                return messageHistory[historyIndex];
            }
        },

        addtoHistory: function (message) {
            messageHistory.push(message);
            historyIndex = messageHistory.length;
        },

        logout: function () {
            btalk.auth.logout();
        },

        /*
         *  DamBV - 09/02/2017 : Xóa bỏ phần quote ở dưới mục text box, bang phim Backspace
         */
        removeQuoteMessage: function (e) {
            var textContent = $('#messageTxt').text();
            var KeyID = e.keyCode;
            var isRemoveQuote = false;
            // Khi bam phim backspace
            if (KeyID == 8) {
                // Neu khong co text trong phan soan tin nhan.
                if (textContent == "") {
                    isRemoveQuote = true;
                } else {
                    // Neu ton tai text trong phan soan tin nhan va vi tri con tro o cuoi cung.
                    var pos = window.getSelection();
                    if (pos.anchorOffset == 0) {
                        isRemoveQuote = true;
                    }
                }
            }
            if (isRemoveQuote) {
                $('#input_replymsg').html('');
                this.CURRENTCHATTER.temporaryQuote = null;
            }
        },

        /*
         *  DamBV - kiem tra storage de hien thi.
         */
        checkStatusOpenChatterInfo: function () {
            var isOpenChatterInfo = btalk.cache.readCacheStorage(btalk.cache.Name_OpenChatterInfo) === "true";
            if (btalk.isMobile === false) {
                if (isOpenChatterInfo != null && !isOpenChatterInfo) {
                    this.$middlePanel.css('width', '100%');
                    this.$chatterInfoPanel.hide();
                    $('#chatterInfoBtn span').removeClass('selected-button');
                } else {
                    this.$middlePanel.css('width', '');
                    this.$chatterInfoPanel.show();
                    $('#chatterInfoBtn span').addClass('selected-button');
                }
            }
        },

        /*
         *  DamBV - 14/02/2017: Dua chater ve loai nho.
         */
        setHeightChatterSmall: function (e) {
            btalk.CHATTERS.models.forEach(function (item) {
                item.set('statusHeightChatter', 'small');
            });

            btalk.cache.statusSizeChatter = "small";
            btalk.cache.heightDefaultChatter = 35;
            btalk.cache.writeCacheStorage('statuschatter', 'small');

            var select_chatter_default = document.getElementById('select_chatter_default')
            if (select_chatter_default != null) {
                select_chatter_default.style.display = 'none';
            }
            var select_chatter_small = document.getElementById('select_chatter_small')
            if (select_chatter_small != null) {
                select_chatter_small.style.display = 'block';
            }
            this.canGetChatter();
        },

        /*
         *  DamBV - 14/02/2017: Dua chater ve mac dinh.
         */
        setHeightChatterDefault: function (e) {
            btalk.CHATTERS.models.forEach(function (item) {
                item.set('statusHeightChatter', 'default');
            });

            btalk.cache.statusSizeChatter = "default";
            btalk.cache.heightDefaultChatter = 40;
            btalk.cache.writeCacheStorage('statuschatter', 'default');

            var select_chatter_default = document.getElementById('select_chatter_default')
            if (select_chatter_default != null) {
                select_chatter_default.style.display = 'block';
            }
            var select_chatter_small = document.getElementById('select_chatter_small')
            if (select_chatter_small != null) {
                select_chatter_small.style.display = 'none';
            }

            this.canGetChatter();
        },

        /*
         *  DamBV - 08/03/2017: Update trang thai cho notifi cho chatter
         */
        updateStatusNotificationChatter: function () {
            var _listSettedNotify = JSON.parse(btalk.cache.readCacheStorage(btalk.cache.Name_SettingNotification));
            if (_listSettedNotify == null) return;

            for (var j = 0; j < btalk.CHATTERS.models.length; j++) {
                var check = false;
                for (var i = 0; i < _listSettedNotify.length; i++) {
                    var _object = new btalk.cache.SettingNotifyWithChatter(_listSettedNotify[i].account, _listSettedNotify[i].value);

                    if (_object.account === btalk.CHATTERS.models[j].get('jid') && !_object.delete()) {
                        btalk.CHATTERS.models[j].set({ muteNotification: true });
                        check = true;
                        break;
                    }
                }

                if (!check) {
                    btalk.CHATTERS.models[j].set({ muteNotification: false });
                }
            }
        },

        /*
         * DamBV - 14/02/2017: Cài đặt thời gian cho phep hiển thị thông báo hay không.
         */
        setTimeActiveNotify: function () {
            // thoi gian time tieng theo phut.
            var selectInput = $('input[name="time_off_notify"]:checked');
            var valueTime = selectInput.val();
            selectInput.prop('checked', false);
            if (valueTime && valueTime != "notactive" && valueTime != "active" && valueTime != "" && valueTime != null) {
                var time = parseInt(valueTime);
                var today = new Date().valueOf();
                today = today + time * 60 * 1000;
                valueTime = today;
            }

            var currentChatter = this.CURRENTCHATTER.get('jid');
            var ObjectSetting = new btalk.cache.SettingNotifyWithChatter(this.CURRENTCHATTER.get('jid'), valueTime);

            // Doc lai danh sach da cai dat.
            var _listSettedNotify = JSON.parse(btalk.cache.readCacheStorage(btalk.cache.Name_SettingNotification));
            if (_listSettedNotify == null) {
                _listSettedNotify = [];
            }

            // update value setting notify
            var _isadd = false;
            for (var i = 0; i < _listSettedNotify.length; i++) {
                if (_listSettedNotify[i].account == ObjectSetting.account) {
                    _listSettedNotify[i].value = ObjectSetting.value;
                    _isadd = true;
                }
            }

            // trong truong hop khong update thi se them moi.
            if (_isadd == false) {
                _listSettedNotify[_listSettedNotify.length] = ObjectSetting;
            }

            // Update cho chatter de hien icon set notify
            for (var j = 0; j < btalk.CHATTERS.models.length; j++) {
                var check = false;
                for (var i = 0; i < _listSettedNotify.length; i++) {
                    var _object = new btalk.cache.SettingNotifyWithChatter(_listSettedNotify[i].account, _listSettedNotify[i].value);
                    if (_object.account === btalk.CHATTERS.models[j].get('jid') && !_object.delete()) {
                        btalk.CHATTERS.models[j].set({ muteNotification: true });
                        check = true;
                        break;
                    }
                }
                if (!check) {
                    btalk.CHATTERS.models[j].set({ muteNotification: false });
                }
            }

            // Xoa nhung truong hop khong can luu.
            for (var i = 0; i < _listSettedNotify.length; i++) {
                var item = new btalk.cache.SettingNotifyWithChatter(_listSettedNotify[i].account, _listSettedNotify[i].value);
                if (item.delete()) {
                    _listSettedNotify.splice(i, 1);
                }
            }

            //Truoc khi viet ra thi update.
            btalk.cache.ManagerSettingNotification = _listSettedNotify;
            btalk.cache.writeCacheStorage(btalk.cache.Name_SettingNotification, JSON.stringify(btalk.cache.ManagerSettingNotification));

            document.getElementById('modal_setting_notify').style.display = 'none';
        },

        /*
         * DamBV - 22/02/2017 : ham kiem tra xem co the cho phep hien thong bao hay khong.
         * Mac din la cho phep hien thi thong bao.
         */
        canShowNotifyNewMessage: function (jid) {
            var valueSettingNotification = btalk.cache.readCacheStorage(btalk.cache.Name_SettingNotification);
            if (!valueSettingNotification) return true;
            valueSettingNotification = JSON.parse(valueSettingNotification);
            var isShow = true;
            for (var i = 0; i < valueSettingNotification.length; i++) {
                var item = valueSettingNotification[i];
                if (item.account == jid) {
                    var setNofi = new btalk.cache.SettingNotifyWithChatter(item.account, item.value);
                    isShow = setNofi.canShow();
                    break;
                }
            }
            return isShow;
        },

        /*
         *  DamBV - 14/02/2017 : kiem tra xem có the lay xem chatter ve khong.
         */
        canGetChatter: function () {
            var numberchttermax = this.getMaxChatterCountsByHeight();
            var lenght = btalk.CHATTERS.models.length;
            if (numberchttermax > lenght) {
                btalk.CHATTERNEXTVIEW.loadNextPageOfChatters();
            }
        },

        /*
         * -------------------------------------------------------------------------------------- TIM KIEM LICH SU CHAT
         *  Update DamBV - 13/03/2017 : Thuc hien lai toan bo giao dien.
         */
        managerMsgFind: null,               // Bien luu gia tri cua ket qua tim kiem.
        managerMsgFindConversation: [],     // Luu gia tri cua cac tin nhan lien ke voi tin nhan ket qua,
        valueSearch: null,                  // Gia tri cua value search, gia tri tim kiem co the la text/date.

        /*
        * DamBV - 16/01/2017 - Hien thi ket qua tim kiem duoc.
        */
        handleArchiveMessageSearch: function (packageMsg) {
            // Doi voi goi tin co id la searchMsgHistoryWithUser thi moi tiep tuc xu li.
            if (packageMsg.id != "searchMsgHistoryWithUser") {
                return;
            }

            // An icon trang thai bao dan tim kiem.
            document.getElementById('icon_loading_search').style.display = 'none';

            // Kiem tra xem trong goi tin tra ve co so luong tin khong. neu khong co thi dung lai, khong xu li gi nua
            if (!packageMsg.chat.to && !packageMsg.chat.from) {
                this.showNotifyFormText("Không có kết quả tìm thấy trong cuộc hội thoại này!<br>Bạn có thể thử lại với nội dung khác.");
                // Hien nut tim kiem
                document.getElementById('submit_search').style.display = 'block';
                return;
            }

            clearTimeout(this.printMessageTimeout);
            // Dung tien trinh danh dau trang thai da doc tin khi window focus neu co
            clearTimeout(this.conversationDaysReadAllTimeout);

            // reset panel danh sach tin nhan
            this.scrollBottom(0);
            btalk.CONVERSATIONDAYS.reset(null);
            this.$messagePanel.html("");

            // Hien thi msg ra
            this.handleArchiveMessages(packageMsg, false);

            // Thuc hien cac buoc sau:

            // An bao message_next, tranh viec tai them ve.
            $('#message_next').hide();
            // An noi dung tim kiem de hien thi ket qua tim kiem.
            $('#body_search').hide();

            // Hien giao dien tai them o phia tren
            $('#load_up_message').show();

            // hien thi noi dung cua ket qua tim kiem
            $('#result_search').show();

            // Chinh giao dien tuong ung.
            if (this.managerMsgFind != null && this.managerMsgFind.length > 0) {
                if (btalk.SEARCH.TYPE_SEARCH === "textMsg") {
                    $('span[name ="number-result"]').text('1');
                    $('span[name ="total-result"]').text(" / " + this.managerMsgFind.length);
                    $('span[name ="text-msg"]').text("'" + this.valueSearch.substring(0, 30) + "'");
                    $('#load_down_message').show();
                } else {
                    $('#previous_result').addClass('disactive');
                    $('#next_result').addClass('disactive');
                    $('span[name ="number-result"]').text('');
                    $('span[name ="total-result"]').text('');
                    $('span[name ="text-msg"]').text(" " + this.valueSearch.substring(0, 30) + " có " + this.managerMsgFind.length + " tin nhắn");
                }
                btalk.SEARCH.IS_STATUS_SEARCH = true;
            }
        },

        /*
         * DamBV  : Xu li goi tin khi lay tin nhan them trong tim kiem.
         */
        handleArchiveMessageWithTime: function (pageOfMessages) {
            // Doi voi goi tin ma id khong co get_message_with_time thi se khong xu li.
            if (pageOfMessages.id.indexOf('get_message_with_time') < 0) {
                return;
            }

            // Reset lai toan bo nhung tin nhan duoc trong cuoc hoi thoai(tin tim thay va tin lien ke).
            if (this.managerMsgFindConversation == null) {
                this.managerMsgFindConversation = [];
            }

            // Convert goi tin thành mảng các tin nhắn đã tìm kiếm được.
            var _toAndFrom = this.getPackageMessageFromServer(pageOfMessages);

            // Trong truong hop tim kiem them thi se them vao cac tin tim kiem duoc.
            var listConversation = _.union(this.managerMsgFindConversation, _toAndFrom);

            // Sort theo thoi gian.
            listConversation = _.sortBy(listConversation, "servertime");
            listConversation.reverse();

            // Gan lai gia tri.
            this.managerMsgFindConversation = listConversation;

            // Loai bo cac tin trung nhau.
            for (var i = 0; i < this.managerMsgFindConversation.length; i++) {
                for (var j = i + 1; j < this.managerMsgFindConversation.length; j++) {
                    if (this.managerMsgFindConversation[i].id == this.managerMsgFindConversation[j].id) {
                        this.managerMsgFindConversation.splice(i, 1);
                    }
                }
            }

            // Neu so luong tin trong cuoc hoi thoai lon hon 5 thi moi duoc phep ve ra.
            if (this.managerMsgFindConversation.length > 5) {
                this.printMessageSearch();
            }
        },

        /*
         * DamBV Hien thi giao dien tim kiem.
         */
        showViewSearchMsg: function () {
            /*
             * - Ẩn hoặc hiển thị thanh tìm kiếm.
             * - Kiem tra lai trang thai thanh tim kiem de chinh lai chieu cho phan noi dung cuoc hoi thoai
             * va autofocus cho input tim kiem.
             */
            if (!btalk.SEARCH.IS_STATUS_SEARCH && btalk.SEARCH.IS_STATUS_SEARCH == true) {
                this.hiddenViewSearchMsg();
            }

            $('#body_search').show();
            $('#result_search').hide();
            var indexshow = btalk.SEARCH.INDEX_RESULT;
            $('span [name="index-result"]').text(indexshow++);   // reset lai cho phan tim kiem.
            document.getElementById('submit_search').style.display = 'block';

            // Khi bat dau se khong co lui
            $('#previous_result').addClass('disactive');
            btalk.SEARCH.CAN_PREVIOUS_RESULT = false;
            var bodySearch = $('#btalk_body_search');
            bodySearch.fadeToggle(700);
            $('#search_text').val('').focus();
            var img_searchMsg = $("#img_searchMsg");
            if (img_searchMsg.attr("class").indexOf("selected-button") < 0) {
                img_searchMsg.addClass("selected-button");
            } else {
                img_searchMsg.removeClass("selected-button");
            }
        },

        /*
         * DamBV An giao dien tim kiem.
         */
        hiddenViewSearchMsg: function () {
            $('#btalk_body_search').hide();
            if (btalk.SEARCH.IS_STATUS_SEARCH) {
                this.turnOffComponentViewSearch();
                this.resetConversationDaysView();
                $('#img_searchMsg').removeClass("selected-button");
            }
            this.managerMsgFind = null;
            this.managerMsgFindConversation = null;
            btalk.SEARCH.IS_STATUS_SEARCH = false;
            btalk.SEARCH.TYPE_SEARCH = null;
            btalk.SEARCH.TOP_LOAD_MSG = false;
            btalk.SEARCH.CAN_NEXT_RESULT = true;
            btalk.SEARCH.INDEX_RESULT = 0;
        },

        /*
         * Tat cac thanh phan cua search va khoi phuc trang thai xem tin nhu cu.
         */
        turnOffComponentViewSearch: function () {
            $('#load_down_message').hide();
            $('#load_up_message').hide();
            $('#message_next').show();
        },

        /*
         * DamBV Tim kiem da chat trong cuoc hoi thoai.
         */
        findMessageInConversation: function (e) {
            /*
             * - Chi thu hien khi go phim enter khi go phim va click nut tim kiem
             * - Thực hiện lấy txt rồi send messege tìm kiếm đi.
             */
            if (e.target.id == "submit_search" || (e.target.id == "search_text" && e.which == 13)) {
                var optionTxt = $('#option_search')[0].value;                   // Bien luu kieu tim kiem
                var jidCurrentChatter = this.CURRENTCHATTER.get('jid');         // Bien luu jid của chatter hien tai
                var typeChatter = this.CURRENTCHATTER.get('type');              // Bien luu kieu cua chatter hien tai.
                var value_input = "",                                           // Gia tri cua tim kiem theo text.
                    startDate,                                                  // Gia tri text cua ngay bat dau khi tim kiem.
                    endDate;                                                    // Gia tri text cua ngay ket thuc khi tim kiem.

                // Tim kiem theo dang chuoi
                if (optionTxt === "textMsg") {
                    value_input = $('#search_text').val();
                    if (value_input == "") {
                        this.showNotifyFormText("Từ khóa tìm kiếm bị trống!<br>Bạn vui lòng nhập đủ dữ liệu.");
                        return;
                    }
                    this.valueSearch = value_input.trim();
                }

                // Tim kiem theo dang ngay thang
                if (optionTxt === "dateMsg") {
                    // Lay du lieu tu picker date khi tim kiem theo ngay.
                    var _startDate = $('#search_startdate');
                    var _endDate = $('#search_enddate');

                    var textStartDate = _startDate.val();
                    var textEndDate = _endDate.val();
                    if (textEndDate == "" || textStartDate == "") {
                        this.showNotifyFormText("Dữ liệu ngày tháng bị trống!<br>Bạn vui lòng nhập đủ dữ liệu.");
                        return;
                    }

                    // Doi ngay ngay thang text -> Date.
                    var dateStartParts = textStartDate.split("/");
                    var dateStart = new Date(parseInt(dateStartParts[2]), parseInt(dateStartParts[1]) - 1, parseInt(dateStartParts[0]));

                    var dateEndParts = textEndDate.split("/");
                    var dateEnd = new Date(parseInt(dateEndParts[2]), parseInt(dateEndParts[1]) - 1, parseInt(dateEndParts[0]));

                    startDate = dateStart.valueOf();
                    endDate = dateEnd.valueOf();

                    if (endDate < startDate) {
                        this.showNotifyFormText("Ngày kết thúc phải lớn hơn ngày bắt đầu, vui lòng thực hiện lại !");
                        return;
                    }
                    this.valueSearch = _startDate[0].value + " " + _endDate[0].value;
                }
                btalk.SEARCH.TYPE_SEARCH = optionTxt;

                // An nut tim kiem
                document.getElementById('submit_search').style.display = 'none';

                // Lam hien icon trang thai tim kiem
                document.getElementById('icon_loading_search').style.display = 'block';

                // Gui tin tim kiem
                btalk.cm.searchMessageInConversation(optionTxt, jidCurrentChatter, typeChatter, value_input, startDate, endDate);
            }
        },

        /*
         * DamBV Click next chuyen den ket qua tim kiem tiep theo.
         */
        nextResultFindMessage: function () {
            if (btalk.SEARCH.IS_STATUS_SEARCH == true &&
                btalk.SEARCH.TYPE_SEARCH == "textMsg" &&
                btalk.SEARCH.CAN_NEXT_RESULT == true) {
                btalk.SEARCH.INDEX_RESULT++;
                var nowIndex = btalk.SEARCH.INDEX_RESULT;
                if (-1 < nowIndex && nowIndex < this.managerMsgFind.length) {
                    btalk.CONVERSATIONDAYS.reset(null);
                    this.$messagePanel.html("");
                    this.managerMsgFindConversation = [];

                    btalk.cm.getMessageWithTime(this.managerMsgFind[nowIndex], true);
                    btalk.cm.getMessageWithTime(this.managerMsgFind[nowIndex], false);
                    nowIndex = nowIndex + 1;
                    $('span[name="number-result"]').text("" + nowIndex);
                }

                if (btalk.SEARCH.INDEX_RESULT == this.managerMsgFind.length - 1) {
                    btalk.SEARCH.CAN_NEXT_RESULT = false;
                    $('#next_result').addClass('disactive');
                }

                $('#previous_result').removeClass('disactive');
                btalk.SEARCH.CAN_PREVIOUS_RESULT = true;
            }
        },

        /*
         * DamBV Click previous chuyen den ket quan tim kiem truoc do.
         */
        previousResultFindMessage: function () {
            if (btalk.SEARCH.IS_STATUS_SEARCH == true &&
                btalk.SEARCH.TYPE_SEARCH == "textMsg" &&
                btalk.SEARCH.CAN_PREVIOUS_RESULT == true) {
                btalk.SEARCH.INDEX_RESULT--;
                var index = btalk.SEARCH.INDEX_RESULT;

                if (-1 < index && index < this.managerMsgFind.length) {
                    btalk.CONVERSATIONDAYS.reset(null);
                    this.$messagePanel.html("");
                    this.managerMsgFindConversation = [];
                    btalk.cm.getMessageWithTime(this.managerMsgFind[index], true);
                    btalk.cm.getMessageWithTime(this.managerMsgFind[index], false);
                    index = index + 1;
                    $('span[name="number-result"]').text("" + index);
                }

                if (btalk.SEARCH.INDEX_RESULT == 0) {
                    btalk.SEARCH.CAN_PREVIOUS_RESULT = false;
                    $('#previous_result').addClass('disactive');
                }

                btalk.SEARCH.CAN_NEXT_RESULT = true;
                $('#next_result').removeClass('disactive');
            }
        },

        /*
         * DamBV: Hiển thị nội dung tin tìm kiếm cùng các tin liến trước, liền sau.
         */
        printMessageSearch: function () {
            this.managerMsgFindConversation = _.sortBy(this.managerMsgFindConversation, 'clienttime');//servertime
            this.managerMsgFindConversation = this.managerMsgFindConversation.reverse();
            var _this = this;

            btalk.CONVERSATIONDAYS.reset(null);
            this.$messagePanel.html("");
            this.managerMsgFindConversation.forEach(function (item) {
                if (item) {
                    _this.printMessage(item, false, true);
                }
            });
        },

        /*
         * DamBV - 17/03/2017: Tim them tin nhan trong tim kiem.
         * Tai them tin nhan.
         */
        loadUpMessages: function () {
            var lastUpMsg = this.managerMsgFindConversation[this.managerMsgFindConversation.length - 1];
            btalk.cm.getMessageWithTime(lastUpMsg, true);
            btalk.SEARCH.TOP_LOAD_MSG = true;
            btalk.SEARCH.SCROLL_BOTTOM = this.$messagePanelOuter.scrollBottom();
        },

        /*
         * DamBV - 17/03/2017: Tai them tin nhan trong tim kiem
         */
        loadDownMessages: function () {
            btalk.cm.getMessageWithTime(this.managerMsgFindConversation[0], false);
            btalk.SEARCH.TOP_LOAD_MSG = false;
            btalk.SEARCH.SCROLL_TOP = this.$messagePanelOuter.scrollTop();
        },
        // ------------------------------------------------------------------------------------------------ DUNG TIM KIEM LICH SU CHAT

        /*
         * DamBV - Thực hiện chuyển nội dung tin nhắn từ các gói chat.from, chat.to và thêm thuộc tính jid.
         */
        getPackageMessageFromServer: function (pageOfMessages) {
            var toAndFrom = [];

            // Neu goi tin khong phai la goi tin chua noi dung tin nhan thi dung lai.
            if (!pageOfMessages.chat) {
                return;
            }

            if (pageOfMessages.chat.to) {
                pageOfMessages.chat.to = $.isArray(pageOfMessages.chat.to) ?
                            pageOfMessages.chat.to : [pageOfMessages.chat.to];
            } else {
                pageOfMessages.chat.to = [];
            }

            if (pageOfMessages.chat.from) {
                pageOfMessages.chat.from = $.isArray(pageOfMessages.chat.from) ?
                                pageOfMessages.chat.from : [pageOfMessages.chat.from];
            } else {
                pageOfMessages.chat.from = [];
            }

            _.each(pageOfMessages.chat.to, function (_to) {
                if (!_to.servertime) {
                    var start = new Date(pageOfMessages.chat.start);
                    var secs = typeof _to.secs != 'number' ? parseInt(_to.secs) : _to.secs;
                    start.setSeconds(start.getSeconds() + secs);
                    _to.servertime = start;
                }
                //_to.type = "chat"; // TODO: bo sung groupchat sau nay
                _to.from = btalk.ROSTER.currentuser.jid;
                _to.to = pageOfMessages.chat.with;

                _to.chatterJid = btalk.cutResource(pageOfMessages.chat.with);
            }.bind(this));

            _.each(pageOfMessages.chat.from, function (_from) {
                if (typeof _from == "string") {
                    _from = pageOfMessages.chat.from;
                }

                if (!_from.servertime) {
                    var start = new Date(pageOfMessages.chat.start);
                    var secs = typeof _from.secs != 'number' ? parseInt(_from.secs) : _from.secs;
                    start.setSeconds(start.getSeconds() + secs);
                    _from.servertime = start;
                }

                //_from.type = "chat"; // TODO: bo sung groupchat sau nay
                _from.from = _from.from || pageOfMessages.chat.with;
                _from.to = btalk.ROSTER.currentuser.jid;
                _from.chatterJid = btalk.cutResource(pageOfMessages.chat.with);
            }.bind(this));

            toAndFrom = toAndFrom.concat(pageOfMessages.chat.from);
            toAndFrom = toAndFrom.concat(pageOfMessages.chat.to);

            // TamDN - 30/6/2017 - Loại bỏ msg rỗng và msg trùng nhau
            var tmp = [];
            for (var i = 0; i < toAndFrom.length; i++) {
                if (toAndFrom[i]) {
                    tmp.push(toAndFrom[i]);
                    for (var j = i + 1; j < toAndFrom.length; j++) {
                        if (toAndFrom[i].id == toAndFrom[j].id) {
                            tmp.splice(tmp.length - 1, 1);
                            break;
                        }
                    }
                }
            }
            toAndFrom = tmp;
            toAndFrom = _.sortBy(_.sortBy(toAndFrom, 'clienttime'), 'servertime');//servertime
            toAndFrom = toAndFrom.reverse();
            return toAndFrom;
        },

        /*
         * DamBV - 20/02/2017: Kiếm tra có hiện thông bao tin nhắn ở phần chatter.
         * Cho phép hiển thông báo khi chatter : không phải current chatter và trong vùng không nhìn thấy
         */
        canShowTooltipNewMessageIBelowChatter: function (jid) {
            var listChatter = $('.btalk-history-list .list-group .list-group-item');
            var scrollTop = $('#tab_history').scrollTop();
            var numberChatterCanShowNotify = Math.round(scrollTop / 50);
            var okie = false;
            for (var i = 0; i < numberChatterCanShowNotify; i++) {
                var _jid = listChatter[i].children[0].id;
                if (_jid == jid) {
                    okie = true;
                    break;
                }
            }
            if (okie == true) {
                $('#notify_newmessage_chatter').css('visibility', 'visible');
            }
        },

        /*
         *  DamBV - 20/02/2017: Ẩn đi thông báo có tin nhắn mới
         */
        hideTooltipNewMessageBelowChatter: function () {
            $('#notify_newmessage_chatter').css('visibility', 'hidden');
            $('#tab_history').scrollTop(0);
        },

        /*
         *  DamBV - 20/02/2017: Khi scroll gan ve 0 thi ẩn message.
         * Todo: chuẩn là đi qua chỗ tin nhăn chưa được.
         */
        canHidenTooltipNewMessage: function () {
            var scrollTop = $('#tab_history').scrollTop();
            var isShow = $('#notify_newmessage_chatter').css("visibility");
            if (scrollTop < 25 && isShow == "visible") {
                this.hideTooltipNewMessageBelowChatter();
            }
        },

        /*
         * DamBV - 22/02/2017 : Thay thế các img trong của emotion bằng text tương ứng
         */
        replaceEmotionByText: function (contentMessage) {
            var imgRegEx = /<img[^>]+src="?([^"\s]+)"?\s*>/g;
            var imgs = [];
            var m;
            while (m = imgRegEx.exec(contentMessage)) {
                imgs.push(m[0]);
            }
            for (var i = 0; i < imgs.length; i++) {
                var img = imgs[i];
                var title = $(img).attr("title");
                contentMessage = contentMessage.replace(img, title);
            }
            return contentMessage;
        },

        /*
         * DamBV - 20/02/2017 : Xu li hien avatar khi scroll tin nhan.
         */
        canShowAvatarWhenScrollHistory: function (e) {
            var spaceTextInput = $('.btalk-textinput').offset().top;
            var scrollBottom = $('#chat_right_top_panel_id').scrollBottom();
            var listMessageNotMe = $('div.chat-detail-row:not(div.isme)');

            if (listMessageNotMe.length < 1) return;

            for (var i = listMessageNotMe.length - 1; i >= 0; i--) {
                var _itemMessageNotMe = $(listMessageNotMe[i]);
                var _spaceTop = _itemMessageNotMe.offset().top;
                var _heightmessage = _itemMessageNotMe.height();

                var avt;
                if (_itemMessageNotMe[0].children[0].id.trim() == "avatar_axtra") {
                    avt = _itemMessageNotMe[0].children[0];
                }

                if (_spaceTop > 0 && _spaceTop + 40 < spaceTextInput && spaceTextInput < _spaceTop + _heightmessage - 40) {
                    var x = _heightmessage - (spaceTextInput - _spaceTop) + 10;
                    $(avt).css("display", "block").css("bottom", x);
                    break;
                } else {
                    $(avt).css("display", "none");
                }
            }
        },

        /*
         * DamBV - 28/02/2017 : kiểm tra có thông bao khi có tin nhăn mới ở phần TextInput.
         */
        canShowToolTipNewMessageOnTextInput: function () {
            var _scroll = this.scrollBottom();
            if (_scroll > 5) {
                $('#notify_newmessage_textput').css('display', 'block');
            }
        },

        /*
         * DamBV - 28/02/2017 : kiểm tra có an thong bao khi có tin nhăn mới ở phần TextInput.
         */
        canHidenToolTipNewMessageOnTextInput: function () {
            var _scroll = this.scrollBottom();
            var isShow = $('#notify_newmessage_textput').css("display");
            if (_scroll < 10 && isShow == "block") {
                this.hideToolTipNewMessageOnTextInput();
            }
        },

        /*
         * DamBV - 28/02/2017 :an thong bao khi có tin nhăn mới ở phần TextInput.
         */
        hideToolTipNewMessageOnTextInput: function () {
            $('#notify_newmessage_textput').css('display', 'none');
            this.scrollBottom(0);
        },

        /*
         * DamBV - 11/04/2017 : tat am bao tin nhan.
         */
        setTurnOffSound: function () {
            btalk.cache.Key_SoundMessage = "mute";
            btalk.cache.writeCacheStorage(btalk.cache.Name_SoundMessage, btalk.cache.Key_SoundMessage);
            document.getElementById('select_off_sound').style.display = 'block';
            document.getElementById('select_on_sound').style.display = 'none';
        },

        /*
         * DamBV - 11/04/2017 : bat am bao tin nhan.
         */
        setTurnOnSound: function () {
            btalk.cache.Key_SoundMessage = "active";
            btalk.cache.writeCacheStorage(btalk.cache.Name_SoundMessage, btalk.cache.Key_SoundMessage);
            document.getElementById('select_off_sound').style.display = 'none';
            document.getElementById('select_on_sound').style.display = 'block';
        },

        /*
         * DamBV - 03/05/2017 : Xu li notify cho egov:
         */
        showNotificationEgov: function (bodyMsg) {
            //avatarChatter, idChatter, title, message, options

            var idChatter = this.CURRENTCHATTER.get('jid');
            var ntftitle = idChatter.split('@')[0];
            var avatarChatter = idChatter;
            if (this.CURRENTCHATTER.get('type') == btalk.CHATTYPE.GROUPCHAT) {
                var fullname = btalk.ROSTER.getUserByJID(idChatter).chatterM.get("fullname");
                ntftitle = fullname;
                avatarChatter = location.origin + '/themes/default/images/group3.png';
            } else {
                if (avatarChatter.indexOf("@") == -1) {
                    avatarChatter += "@" + window._DOMAIN_OF_ACCOUNT;
                }
            }
            var message = bodyMsg.body || bodyMsg;

            /*
             *  DamBV - 03/05/2017 : Xác dinh gia tri isread, isme.
             * isread: là true khi dang focus vào chat và ngược lại là flase.
             * isme: true khi la noi dung tin chat cua minh va flase la khi khong phai tin chat cua minh.
             */
            var isRead = false;
            if (this.$messageTxt.is(":focus")) {
                isRead = true;
            }
            btalk.ntf.showEgov({
                avatarChatter: avatarChatter,
                idChatter: idChatter,
                ntftitle: ntftitle,
                message: message,
                isMe: true,
                isRead: isRead
            });
        },

        /*
         * DamBV: gui tin den nhieu nguoi trong phong ban
         * listUser: danh sach, lable: ten phong ban.
         */
        sendMessageWithUsers: function (listUser, lable) {
            if (listUser.length > btalk.MAX_MEMBERS) {
                this.showNotifyFormText("Bạn không thể thực hiện vì số lượng thành viên quá lớn!");
                return false;
            }

            if (listUser.length < 1) {
                this.showNotifyFormText("Không có thành viên nào!");
                return false;
            }

            var namegroup = lable.value.split("\\").pop()
            var modal = $("#messageSendUsersForm");

            modal.listUsers = null;
            modal.listUsers = listUser.map(function (a) { return a.fulljid; });
            modal.modal({
                cache: false,
                keyboard: false,
            }, "show");

            if (!namegroup) {
                namegroup = "Gửi tin nhắn tới nhóm";
            }
            modal.find(".modal-title").text(namegroup);
            var listUserName = listUser.map(function (a) { return a.name; });
            modal.find(".modal-body span").html("").html(listUserName.join(", "));
            modal.find(".modal-body textarea").val("");

            var _btnSendMsgUsers = $("#btnSendMsgUsers");
            _btnSendMsgUsers.on("click", function (e) {
                var content = modal.find(".modal-body textarea").val();
                for (var i = 0; i < modal.listUsers.length; i++) {
                    var user = modal.listUsers[i];
                    if (user != btalk.auth.getJID()) {
                        btalk.cm.sendMessage(content, user, null, null);
                    }
                }
                modal.modal("hide");
            });
            modal.on('hidden.bs.modal', function (e) {
                _btnSendMsgUsers.unbind('click');
            });
        },

        /*
         * DamBV: Tao nhom tu phong ban.
         */
        createGroupChatWithUsers: function (listUser) {
            if (listUser.length > btalk.MAX_MEMBERS) {
                this.showNotifyFormText("Bạn không thể thực hiện vì số lượng thành viên quá lớn!");
                return false;
            }

            if (listUser.length < 1) {
                this.showNotifyFormText("Không có thành viên nào!");
                return false;
            }

            var modal = $("#createGroupChatForm");
            modal.modal({
                cache: false,
                keyboard: false,
            }, "show");
            var listUserName = listUser.map(function (a) { return a.name; });
            modal.find(".modal-body textarea").html("").html(listUserName.join(", "));
            modal.listUsers = null;
            modal.listUsers = listUser.map(function (a) { return a.fulljid; });

            var _btnCreateGroupUsers = $("#btnCreateGroupUsers");
            _btnCreateGroupUsers.on("click", function (e) {
                var group_jid = btalk.cm.getGroupJID();
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
                    fullname: 'New Group Chat',
                    rosterUser: rosterUser,
                    roster: btalk.ROSTER,
                    isResultOfSearch: true,
                    statusHeightChatter: btalk.config.statuschatter,
                });
                var _listAccount = $('#creatGroupTree').val().split(',');
                var _listMail = [];
                _listAccount.map(function (item) {
                    var _mail = item + '@' + btalk.cm.options.xmpp.domain;
                    _mail = _mail.trim();
                    if (btalk.ROSTER.getUserByJID(_mail)) {
                        _listMail.push(_mail);
                    }
                });
                rosterUser.members = _listMail
                rosterUser.chatterM = newchatter;
                btalk.CHATTERS.add(newchatter);
                newchatter.set({ isselected: true, unread: true });
                btalk.cm.createGroup(group_jid, _listMail);
                modal.modal("hide");
            });
            modal.on('hidden.bs.modal', function (e) {
                _btnCreateGroupUsers.unbind('click');
            });
        },

        /*
         * DamBV: Hien thi cac thong bao chi dang tin nhan den nguoi dung.
         */
        showNotifyFormText: function (lable) {
            var notifyFormText = $("#notifyFormText");
            notifyFormText.modal("show");
            if (!lable && typeof lable != "string") {
                lable = "Xin chào !";
            }
            notifyFormText.find('.modal-body span').html(lable);
        },

        /*
         * Nhung tin nhan file da tai ve thi add luon vao muc quan li file chatter.
         */
        saveFileShareFromMessage: function (listFileMsg) {
            var dataFile = btalk.fm.getFileFromData(listFileMsg);
            var listShareImage = btalk.fm.synsShareFileLocal(this.CURRENTCHATTER.get(""), dataFile.image);
            var listShareFile = btalk.fm.synsShareFileLocal(this.CURRENTCHATTER.get(""), dataFile.file);

            // luu trang thai cache vao chatter.
            this.CURRENTCHATTER.set("collectionShareImage", listShareImage);
            this.CURRENTCHATTER.set("collectionShareFile", listShareFile);
        },

        /*
         * 07/08/2017: Xu li giao dien voi mobile, cho phep egov goi vao.
         *
         */
        displayViewContentConversation: function () {
            if (btalk.isMobile) {
                $('#btalk_left').animate({
                    left: -1200,
                }, function () {
                    $('#btalk_left').hide();
                    $('#btalk_right').show();
                });
                var chatter = btalk.ROSTER.getUserByJID(this.CURRENTCHATTER.get('jid'));
                var _fullname = "";
                if (chatter.chatterM && chatter.chatterM.get('type') == btalk.CHATTYPE.GROUPCHAT) {
                    _fullname = chatter.chatterM.get('fullname');
                } else {
                    _fullname = chatter.fullname;
                }
                top.mobileChat.openChat(_fullname);
                // can thay ra full name cua nguoi or ten nhom chat.
            }
        },

        egovMobileBackToListHistory: function () {
            $('#btalk_right').hide(10, function () {
                $('#btalk_left').animate({
                    left: 0,
                }).show();
            });

            btalk.CONVERSATIONDAYS.reset(null);
            this.$messagePanel.html("");
            this.scrollBottom(0);
        },

        egovMobileFocusSearch: function () {
            this.searchTxt_FocusEvent_Handler();
        },

        egovMobileNotFocusSearch: function () {
            this.searchTxt_BlurEvent_Handler();
        },

        // sua lai phan tim kiem, chi thuc hien tim lay text.
        egovMobileSearching: function (e, text) {
            var keycode = e.which;
            switch (keycode) {
                case enterKey:
                    // Enter khi dang chon chatter => mo chat voi chatter nay
                    if ($('#tab_search li.selected').length > 0) {
                        // Clear textbox search chua tu khoa vua go
                        this.$searchTxt.val('');

                        forcusTab('tab_history');
                        // Dat lai active tab
                        this.activeTabName = 'tab_history';

                        $('#tab_search li.selected').trigger('click');
                        $('#messageTxt').focus();
                    }
                        // [TODO] Nguoc lai => Tim kiem noi dung lich su chat
                    else {
                    }
                    break;

                    // Bo qua khong xu ly phim di chuyen
                case leftKey:// left
                case upKey:// up
                case rightKey:// right
                case downKey:// down
                    return;
                    break;

                default:
                    // Other key (dang go tim chatter) => Tim kiem chatter
                    // [TODO] xu ly ket qua seach nay sort theo tieu chi ai hay chat hon thi hien len dau                
                    this.handleDefaultEventSearchUser(text);

                    break;
            }
            return;
        },

        egovMobileShowAddMember: function () {
            if (this.CurrentChatterViewInfo) {
                this.CurrentChatterViewInfo.showDialogAddMembers();
            }
        },

        egovMobileShowMemberGroup: function () {
            if (this.CurrentChatterViewInfo) {
                var htmlMember = $('#body_member_group').html();
                $('#dialog_member_body').html(htmlMember);
                $('#dialogmembergroup').modal("show");
            }
        },

        egovMobileSelectChatter: function (accountJid) {
            if (btalk.isMobile) {
                this._chatWithAccount(accountJid);
                this.displayViewContentConversation();
            }
        },

        // get may anh or bo suu tap
        takeImageMobile: function (e) {
            var that = this;
            $('#take_image_camera').on('change', function (e) {
                var file = e.target.files;
                if (file) {
                    that.sendFiles(file);
                }
            });
        },

        setCacheTabSelected: function (e) {
            var parentTab = $(e.target).parents("li");
            var nameTabSelected = $(parentTab[0]).attr('data-target');
            btalk.cache.writeCacheStorage("NameTabSelected", nameTabSelected);
            console.log('Da viet tab selected storage');
        },

        visibleFormCreateGroup: function (e) {
            e.preventDefault();
            var viewFormGroup = new btalk.view.ViewFormGroup();
            viewFormGroup.visibleForm();
        },

        // XU LI IDLE 
        lastActivity: new Date().getTime(),
        idle: false,
        checkIdle: function () {
            // Xet dieu kien tro thanh Idle
            // fix tam 10s.
            console.log("Check IDLE");
            if (!btalk.APPVIEW.idle && btalk.APPVIEW.lastActivity + (20 * 1000) < new Date().getTime()) {
                btalk.APPVIEW.idle = true;
                // Gui tin Idle len server.               
                btalk.cm.sendPresenceIDLE(btalk.APPVIEW.idle);
            }
        },

        resetIdle: function () {
            clearInterval(btalk.APPVIEW.checkIdleInterval);
            this.lastActivity = new Date().getTime();
            // Xet dieu kien tro thanh Khong Idle           
            if (btalk.APPVIEW.idle) {
                btalk.APPVIEW.idle = false;
                // Gui tin Idle len server.
                btalk.cm.sendPresenceIDLE(btalk.APPVIEW.idle);
            }
            this.checkIdleInterval = setInterval(this.checkIdle, 10 * 1000);
        },
    });

    /*
     * TAB HOI THOAI, TOAN BO, YEU THICH
     */

    function forcusTab(tabname) {
        $('.btalk-left li[role=presentation]').removeClass('active');
        $('.btalk-left .tab-pane').removeClass("active");
        $("li[data-target=" + tabname + "]").addClass('active');
        $('#' + tabname).addClass("active");
    }

    $('li[role=presentation]').bind('click', function (e) {
        var target = $(e.currentTarget).attr('data-target');
        forcusTab(target);
    });

    return IndexView;

    //// Public IndexView
    //btalk.view = $.extend(btalk.view, {
    //    IndexView: IndexView
    //});
})(btalk);