
requirejs.config({
    // Tienbv: thay đổi chút về cách đánh phiên bản
    //     - file main.js luôn luôn tải về bản mới nhất.
    //     - các file khác phụ thuộc vào phiên bản được đánh này, khi sửa file js thì đánh lại phiên bản cho nó là dc. Như thế ko phải lúc nào f5 cũng load về cái mới nhất
    // Todos: về lâu dài 456sẽ tính cho từng file, file nào thay đổi thì đánh version lại cho file đó thôi
    urlArgs: new Date().getTime()
});

/// <summary>Khai báo namespace chung cho toàn hệ thống</summary>
(function (egov) {
    "use strict";
    // Phiên bản dùng nội bộ công ty
    // Todo: cần chuyển config này trả về từ server
    egov.isPrivateVersion = false;

    // Danh sách các view
    egov.views = {
        // Danh sách các view dùng chung: tree, contextmenu, toolbar, dialog, message
        base: {},
    };

    //Lưu trữ các option của tree hiện tại
    egov.currentTree = {
        document: {}
    };

    // Danh sách các thiết lập cá nhân, thiết lập hệ thống ứng với user hiện tại.
    egov.setting = {};

    // Helper
    egov.helper = {
        loading: '<img class="loading" src="../Content/bkav.egov/images/ajax-loader.gif" alt="loading"/>',

        destroyClickEvent: function (e) {
            if (e) {
                if (e.preventDefault) {
                    e.preventDefault();
                }
                else {
                    // fix cho ie 8
                    e.returnValue = false;
                }
                e.stopPropagation();
            }
        },

        hideAllContext: function () {
            /// <summary>An cac contextmenu</summary>
            if (egov.views.home && egov.views.home.documents) {
                egov.views.home.documents.hideAllContextmenu();
            }
        }
    };

    // Danh sách các thư viện bổ trợ: ultilities, grid,...
    egov.utils = egov.utils || {};

    egov.isMobile = false;

    window.Aloha = window.Aloha || {};
})(window.egov = window.egov || {})

require(["config"], function () {
    if (navigator.isMobile) {
        require(
            ['vendors', 'egovCore', 'resource'],
            function () {
                egov.isMobile = true;
                initMobile();
            });
    }
    else {
        require(
            ['vendors', 'egovCore', 'contextMenuView', 'templateList', 'dialog'],
            function () {
                init();
            });
    }
});

function init() {
    /// <summary>Tải Home View</summary>
    "use strict";

    _overrideBackboneToJsonFunction();

    egov.require = require;

    //Lớp quản lý các biến dùng chung
    egov.common = {};

    // Base view
    egov.views.base.dialog = new window.dialogAdapter();

    egov.localStorage = window.eGovLocalStorage;

    // Thư viện chung xử lý lưu và lấy cookie
    egov.cookie = egov.utils.cookie = eGovCookie;

    $('#egovStatus').status({
        subscribe: egov.pubsub.subscribe
    });

    // Thêm các fn tự định nghĩa cho jQuery
    regJqueryFn(window.ContextMenu);

    // Lớp quản lý các thông báo trên hệ thống.
    egov.message = window.eGovMessage = new eGovMessage;

    // Trước tiên cần tải về danh sách các thiết lập cho user hiện tại.
    egov.dataManager.getCommonConfigs({
        success: function (result) {
            egov.setting = result;

            egov.isMobile = false;

            egov.userId = egov.setting.user.userId;
            egov.userName = egov.setting.user.userName;
            egov.setting.userSetting = egov.setting.user.userSetting;
            egov.setting.signerConfig = egov.setting.user.signerConfig;

            egov.setting.publish.detectPdfChangeContent == undefined && (egov.setting.publish.detectPdfChangeContent = true);
            egov.setting.publish.allowThuHoiLt == undefined && (egov.setting.publish.allowThuHoiLt = false);

            egov.documentTemplates = ParseDocumentTemplate(result.documentTemplates);
            egov.doctypes = result.doctypes;
            egov.setting.allDoctypes = result.allDoctypes;
            egov.storePrivate = {
                storeShare: result.storeShare,
                storePrivate: result.storePrivate
            };

            egov.currentDepts = result.currentDepts;
            egov.allUsers = result.allUsers;
            egov.allDeps = result.allDeps;
            egov.allJobtitles = result.allJobtitles;
            egov.allPositions = result.allPositions;
            egov.allUserDeps = result.allUserDeps;

            //Lưu lại giá trị quickview vào cookie
            egov.cookie.setQuickView(egov.setting.user.userSetting.quickView);

            //Lưu lại trạng thái có hiển thị popup cho ý kiến khi chuyển theo lô
            egov.cookie.displayPopupTransferTheoLo(egov.setting.user.userSetting.displayPopupTransferTheoLo);

            // Danh sách các filetype được phép
            egov.setting.acceptFileTypes = new RegExp(result.fileUploadSettings.acceptFileTypes, 'i');

            $("#documentProcessing").layout({
                resizable: true,
                closable: false,
                north__spacing_open: 0,
                west__spacing_open: 1,
                west__size: 267,
                west__minSize: 235,
                west__maxSize: 500,
                west__initClosed: false,
                center__paneSelector: "#center",
                west__paneSelector: "#west",
            });

            initDoctypes(result.doctypes);

            initNotifications(result.user.notifyConfig);

            initChat(result);

            require(['homeView'], function (HomeView) {
                // Khởi tạo home view
                egov.views.home = new HomeView;

                $(".chat").removeClass("hidden");

                $(document.body).bindResources();

                //Kiểm tra plugin cài đặt chưa, nếu chưa thì hiển thị cài message yêu cầu cài đặt
                checkPlugin();

                // Đăng ký các api cho global.
                registerApisToGlobal();

                // Tai luon cac du lieuj ban dau
                egov.dataManager.getAllDept({});
                egov.dataManager.getAllUsers({});
                egov.dataManager.getAllJobtitle({});
                egov.dataManager.getAllUserDeptPosition({});
            });

            $(document.body).bindResources();
        }
    });

    setDefaults();
}

function initMobile() {
    require([
        'dialog',
        'commonFn',
        'dialogPolypill',
        'templateList'
    ],
        function (d, c, dialogPolypill) {
            "use strict";

            //loadMaterial();

            window.dialogPolypill = dialogPolypill;

            _overrideBackboneToJsonFunction();

            egov.require = require;

            //Lớp quản lý các biến dùng chung
            egov.common = {};

            // Base view
            egov.views.base.dialog = new window.dialogAdapter();

            egov.localStorage = window.eGovLocalStorage;

            // Thư viện chung xử lý lưu và lấy cookie
            egov.cookie = egov.utils.cookie = eGovCookie;

            $('#egovStatus').status({
                subscribe: egov.pubsub.subscribe
            });

            // Lớp quản lý các thông báo trên hệ thống.
            egov.message = window.eGovMessage = new eGovMessage;

            // Thêm các fn tự định nghĩa cho jQuery
            regJqueryFn();

            // Trước tiên cần tải về danh sách các thiết lập cho user hiện tại.
            egov.request.getCommonConfigs({
                success: function (result) {
                    egov.setting = result;
                    egov.isMobile = true;
                    // Set domain
                    document.domain = egov.setting.parentDomain;
                    egov.userId = egov.setting.userId;
                    egov.usernameEmailDomain = $.cookie("bkavUsername");
                    egov.setting.acceptFileTypes = new RegExp(result.acceptFileTypes, 'i');
                    require(["jsmobile"]);
                    $(document.body).bindResources();
                }
            });
        }
    );
}

function registerApisToGlobal() {
    /*
     * Đăng ký các api ra global.
     * 
     */

    // Hàm mở document khi click vào notify
    window.openNotify = function (notifyData) {
        if (typeof notifyData === "string") {
            notifyData = JSON.parse(notifyData);
        }

        //NotificationType=6 Là thông báo cho cuộc trưng cầu được tạo: 
        if (notifyData.NotificationType == 6) {
            var voteId = notifyData.DocumentCopyId;
            egov.request.getVoteDetail({
                data: { voteId: voteId },
                success: function (result) {
                    egov.require(['referendumVote'], function (referendumVote) {
                        result.IsView = true;
                        result.IsVote = true;
                        var model = new VoteModel(result);
                        var vote = new referendumVote({ model: model, view: 4 });
                        vote.render();
                    });
                },
                error: function (error) {

                }
            });
            return false;
        }

        var docCopyId = notifyData.DocumentCopyId;
        var compendium = notifyData.Content;
        if (!navigator.isMobile) {
            egov.views.home.tab.openDocument(docCopyId, compendium, true);
            return;
        }

        if (egov.views && egov.views.home && egov.views.home.tab) {
            egov.views.home.tab.removeDocument();
        }

        egov.require(['tabView'], function (TabView) {
            var tabDocument = new TabView({
                model: {
                    id: docCopyId,
                    name: compendium,
                    title: compendium,
                    href: 'tabDocument_' + docCopyId
                }
            });
        });
    };
}

function initDoctypes(doctypes) {
    window.top && window.top.egovMain && window.top.egovMain.initDoctype(doctypes);
}

function initChat(egovsetting) {
    window.top && window.top.egovMain && window.top.egovMain.initChat(egovsetting);
}

function initNotifications(config) {
    window.top && window.top.egovMain && window.top.egovMain.initNotifications(config);
}

function ParseDocumentTemplate(templates) {
    templates.vbden_min = { "layout": "<table id=\"tableLayoutCollapse\"><colgroup><col style=\"width: 50%;\"><col></colgroup><tbody><tr><td colspan=\"2\" rowspan=\"1\"></td></tr><tr><td colspan=\"1\" rowspan=\"1\"></td><td></td></tr><tr><td colspan=\"2\" rowspan=\"1\"></td></tr><tr><td colspan=\"2\" rowspan=\"1\"></td></tr></tbody></table>", "controls": { "organization": { "position": { "row": 0, "col": 0 }, "disable": false, "widthLabel": 150 }, "compendium": { "position": { "row": 2, "col": 0 }, "disable": false, "height": 40, "widthLabel": 150 }, "comment": { "position": { "row": 3, "col": 0 }, "disable": false, "height": 45, "widthLabel": 150 }, "docCode": { "position": { "row": 1, "col": 0 }, "disable": false, "widthLabel": 150 }, "inOutCode": { "position": { "row": 1, "col": 1 }, "disable": false, "widthLabel": 150 } } };
    templates.vbdi_min = { "layout": "<table id=\"tableLayoutCollapse\"><colgroup><col style=\"width: 50%;\"><col></colgroup><tbody><tr><td colspan=\"2\" rowspan=\"1\"></td></tr><tr><td rowspan=\"1\"></td><td rowspan=\"1\"></td></tr></tbody></table>", "controls": { "compendium": { "position": { "row": 0, "col": 0 }, "disable": false, "height": 45, "widthLabel": 150 }, "docCode": { "position": { "row": 1, "col": 1 }, "disable": false, "widthLabel": 150 }, "comment": { "position": { "row": 1, "col": 0 }, "disable": false, "height": 45, "widthLabel": 150 } } };
    templates.hsmc_min = {};
    return templates;
}

function _clearCache(success) {
    if (egov && egov.dataManager) {
        egov.dataManager.reset({
            success: function () {
                success();
            }
        });
    }
}

function _overrideBackboneToJsonFunction() {
    Backbone.Model.prototype.toJSON = function () {
        if (this._isSerializing) {
            return this.id || this.cid;
        }
        this._isSerializing = true;
        var json = _.clone(this.attributes);
        _.each(json, function (value, name) {
            value && _.isFunction(value.toJSON) && (json[name] = value.toJSON());
        });
        this._isSerializing = false;
        return json;
    }
}

function regJqueryFn(ContextMenu) {
    "use strict";

    // extend function 'etip' cho jQuery để set hiển thị tooltip
    jQuery.fn['etip'] = function (option) {
        if (egov.isMobile) {
            return;
        }
        if (!egov.utils.etip) {
            return;
        }
        if (this.length !== 0) {
            if (option === 'destroy') {
                egov.utils.etip.destroy(this);
            }
            else {
                egov.utils.etip.bind(this);
            }
        }
    };

    // extend function 'contextmenu' cho jQuery
    jQuery.fn['contextmenu'] = function (option, e) {
        var that = this,
            model,
            position;

        if (option instanceof egov.models.contextMenu) {
            model = option;
        }
        else {
            model = new egov.models.contextMenu(option);
        }
        model.set('selector', that);

        if (model.get('isShowLoading')) {
            that.contextobj = new ContextMenu({ model: model });
            return that.contextobj;
        }

        // set nếu đặt position.of = event thì hiển thị contextmenu ngay tại vị trí click chuột
        position = model.get('position');
        if (position.of === 'event') {
            position.of = e;
        }

        // Nếu đã có rồi thì hiển thị lên
        if (that.contextobj && model.get('hasCache')) {
            that.contextobj.render();
        }
        else {
            that.contextobj = new ContextMenu({ model: model });
            that.contextobj.render();
        }
        return that.contextobj;
    };

    // modal
    jQuery.fn['dialog'] = function (option) {
        var model;
        if (typeof option === 'string') {
            option = option.toLowerCase();
            if (this.modalView !== undefined) {
                if (option === 'hide') {
                    this.modalView.hide();
                }

                if (option === 'show') {
                    this.modalView.show();
                }

                if (option === 'destroy') {
                    this.modalView.destroy();
                    this.modalView = undefined;
                }
                if (option === 'showbuttons') {
                    this.modalView.showButtons();
                }

            }
            return;
        }

        if (!(option instanceof egov.models.modal)) {
            model = new egov.models.modal(option);
        }
        else {
            model = option;
        }
        model.set('content', this);

        if (this.modalView) {
            this.modalView.reRender(model);
            return;
        }

        // đưa vào các dialog 
        var help = $(this).attr("help-content-page")
        model.set("helpContent", help)
        this.modalView = new egov.views.modal({ model: model });

    };

    /// <summary>jquery function for table</summary>
    jQuery.fn['table'] = function (option) {
        new egov.utils.table({
            el: this,
            resizable: option.resizable
        })
    };

    /// <summary>
    /// Check all cho checkbox
    /// </summary>
    /// <param name="children">Các item con</param>
    jQuery.fn['checkAll'] = function (children) {
        var that = this,
            count,
            checkedAll,
            countCheck,
            countChecked,
            checked;
        if (this.is(':checkbox')) {
            count = children.length;
            //Check all items
            this.click(function () {
                checkedAll = this.checked;
                for (var i = 0; i < count; i++) {
                    children[i].checked = checkedAll;
                    $(children[i]).trigger('change');
                }
            });
            //Item check
            children.click(function () {
                countCheck = 0;
                countChecked = 0;
                children.each(function () {
                    countCheck++;
                    if (this.checked == true) {
                        countChecked++;
                    }
                });
                checked = countCheck == countChecked ? true : false;
                that.prop("checked", checked);
            });
        }
    };

    jQuery.fn['editor'] = function (height, callback) {
        if (this) {
            var that,
                bmailEditor,
                content,
                contentHeight;

            that = this;
            contentHeight = $(that).height();
            height = height && height > contentHeight ? height : contentHeight;
            content = $(that).clone();
            that.addClass("editting");
            require(['bmailEditor'], function () {
                bmailEditor = new BmailEditor(that);
                bmailEditor.render();
                bmailEditor.setHeight(height ? height : 300);
                bmailEditor.setNoFocusContent(true);
                bmailEditor.setContent(content, function () {
                    egov.callback(callback, bmailEditor);
                });
            })
        }
    }

    jQuery.fn['customDropdown'] = function (option) {
        /// <summary>
        /// Hàm hiển thị custom dropdown
        /// </summary>
        var contentId = this.attr('data-target');
        var target = $(contentId);
        var eventName = egov.isMobile ? "touchend" : "click";

        if (target.length === 0) return;

        if (typeof option == "string") {
            if (option == 'destroy') {
                target.remove();
            }
            return;
        }

        target.hide();
        target.css({ position: "absolute", top: 0, left: 0, right: 0, bottom: 0, zIndex: 1050, overflow: 'auto' });
        target.css(option.css);
        target.addClass('dropdown-menu custom-dropdown');

        var that = this;
        this.on(eventName, function (e) {
            $('.custom-dropdown').not(target).hide();
            if (typeof option.callback === "function") {
                option.callback();
            }
            var height = 0;
            if (!$(e.target).is(this) && that.find(e.target).length === 0) {
                return;
            }

            if (that.find(target).length > 0) {
                // trường hợp target là con của đối tượng hiện tại
                height = $(window.document).height() - that.offset().top - 100;
                that.css({ position: "relative" });
                if (height >= 300) {
                    target.css({
                        top: that.height() + 15,
                        left: that.width() - target.width() + 20  // hiển thị bottom - right
                    });
                } else {
                    height = 300;
                    target.css({
                        top: that.height() + 15 - height,
                        left: that.width() - target.width() + 20  // hiển thị bottom - right
                    });
                }
            }
            else {
                target.appendTo('body');
                var at = that.offset();
                height = $(window.document).height() - at.top - 100;

                if (height > 300) { // Nếu height đủ lớn thì hiển thị dropdown bình thường, còn nếu nhỏ quá thì hiển thị dropup
                    target.css({
                        top: at.top + that.height() + 15,
                        left: at.left + that.width() - target.width() + 20
                    });
                } else {
                    height = 300;
                    target.css({
                        top: at.top - height - 15,
                        left: at.left + that.width() - target.width() + 20
                    });
                }
            }

            if (option.css.height === 'auto') {
                target.height(height);
            }

            target.toggle();
            egov.helper.destroyClickEvent(e);
            e.stopImmediatePropagation();
        });
    };

    var currentSelector;
    jQuery.fn['dbclick'] = function (click_callback, dblclick_callback) {
        /// <summary>
        /// modify hàm double click mặc định
        /// </summary>
        /// <param name="click_callback">Gọi khi là click</param>
        /// <param name="dblclick_callback">Gọi khi là double click</param>
        var delay = 300,
            self;
        return this.each(function () {
            self = this;
            self.clicks = 0;
            jQuery(this).click(function (event) {
                self.clicks++;
                if (self.clicks > 1) {
                    self.clicks = 0;
                    dblclick_callback.call(self, event);
                }
                else {
                    window.setTimeout(function () {
                        if (self.clicks === 1) {
                            click_callback.call(self, event);
                        }

                        self.clicks = 0;
                    }, delay);
                }
            });
        });
    }

    //Không cho phép copy text
    jQuery.fn['disableTextSelect'] = function () {
        return this.each(function () {
            jQuery(this).css({
                "user-select": "none",
                "-khtml-user-select": "none",
                "-ms-user-select": "none",
                "-moz-user-select": "none",
                "-o-user-select": "none",
                "-webkit-user-select": "none"
            })
            .attr('unselectable', 'on')
            .bind("selectstart mousedown",
            function () {
                return false;
            });
        });
    };

    //Cho phép copy text
    jQuery.fn['enableTextSelect'] = function () {
        return this.each(function () {
            jQuery(this).css({
                "user-select": "text",
                "-khtml-user-select": "text",
                "-ms-user-select": "text",
                "-moz-user-select": "text",
                "-o-user-select": "text",
                "-webkit-user-select": "text",
                "cursor": "text"
            })
            .attr('unselectable', 'off')
                 //.unbind("selectstart.disableTextSelect mousedown.disableTextSelect");
            .unbind("selectstart mousedown");
        });
    };

    //Set cursor vào chữ cái cuối cùng trong textbox
    jQuery.fn['setCursorToTextEnd'] = function () {
        var $initialVal = this.val();
        this.val($initialVal);
    };

    jQuery.fn['setCursorPosition'] = function (pos) {
        if (this.setSelectionRange) {
            this.setSelectionRange(pos, pos);
        } else if (this.createTextRange) {
            var range = this.createTextRange();
            range.collapse(true);
            if (pos < 0) {
                pos = $(this).val().length + pos;
            }
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    }

    jQuery.fn['getCursorPosition'] = function () {
        var el = $(this).get(0),
            pos = 0,
            Sel,
            SelLength;
        if ('selectionStart' in el) {
            pos = el.selectionStart;
        } else if ('selection' in document) {
            el.focus();
            Sel = document.selection.createRange();
            SelLength = document.selection.createRange().text.length;
            Sel.moveStart('character', -el.value.length);
            pos = Sel.text.length - SelLength;
        }
        return pos;
    }

    jQuery.fn['focusAndSelectRange'] = function () {
        var length = this.val().length;
        this[0].setSelectionRange(0, length);

        this.focus();
    }
}

function checkPlugin(Plugin) {
    if (!egov.extension) {
        egov.extension = PluginFactory.getInstance();
    }

    egov.extension.isReady(function (isReady) {
        if (isReady) {
            if (egov.extension.hasAppendMode != null || egov.hasPluginAppendMode != null) {
                // 
            } else {
                setTimeout(function () {
                    egov.extension.hasAppendWriteMode(function (result) {
                        egov.extension.hasAppendMode = result;
                        egov.hasPluginAppendMode = result;
                    });

                }, 2000);
            }
            return;
        } else {
            egov.extension.isPluginExist(function (isInstalled) {
                showInstallPlugin();
            });
        }
    });
}

function showWarningMessage() {
    var showMessageElement;
    if ($("#warningMessage").length === 0) {
        var text = "Trình duyệt chưa cài đặt Plugin. <span style='color: blue'>Nhấn vào để tải về?</span>"
        showMessageElement = $('<div id="warningMessage" style="position: fixed;top: 10px; z-index: 100;left: 33%"><div class="alert alert-warning alert-dismissible" role="alert"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong class="icon-warning"></strong> <a id="installExtention" onclick="showInstallPlugin()">' + text + '</a></div><div>');
        showMessageElement.appendTo("body");
    }
}

function showInstallPlugin() {
    var installElement;
    if ($("#installPlugin").length === 0) {
        installElement = $("<div id='installPlugin'>");
        installElement.appendTo("body");
    }

    installElement = $("#installPlugin");
    if (installElement.find("iframe").length === 0) {
        installElement.append('<iframe src="/Install/extension" name="installPlugin" style="width: 1050px; height: 325px; border: none"></iframe>');
    }

    installElement.dialog({
        width: 1100,
        height: "auto",
        draggable: true,
        title: "Kích hoạt Bkav eGov Extension"
    });
}

function imageError(that) {
    $(that).attr("src", "/AvatarProfile/noavatar.jpg");
}

function ShowReferendum() {
    require(['config', 'vendors', 'egovCore', 'contextMenuView', 'templateList', 'dialog'], function () {
        if (navigator.isMobile) {
        }
        else {
            require(
                ['referendum'], function (returnView) {
                    var result = new returnView;
                });
        }
    });
}

function EditVote(voteId) {
    egov.request.getVoteDetail({
        data: { voteId: voteId },
        success: function (result) {
            require(['referendumCreate'], function (referendumCreate) {
                var create = new referendumCreate({ vote: result, isEdit: true });
            });
        },
        error: function (error) { }
    });
}

function DeleteVote(voteId) {
    egov.request.deleteVote({
        data: { voteId: voteId },
        success: function (result) { },
        error: function (error) { }
    });
}

function ShowVoteResult(voteId) {
    egov.request.getVoteDetail({
        data: { voteId: voteId },
        success: function (result) {
            require(['referendumVote'], function (referendumVote) {
                var model = new VoteModel(result);
                var vote = new referendumVote({ model: model, view: 4 });
                vote.render();
            });
        },
        error: function (error) { }
    });
}

function checkVersion(callback) {
    egov.DataAccess.version(function (data) {
        egov.request.getVersionValue({
            success: function (dataServer) {
                if (data == false) {
                    egov.DataAccess.setVersion(dataServer);
                } else {
                    if (data != dataServer) {
                        egov.dataManager.reset();
                        window.location.reload();
                    }
                }
            }
        });
    });
}

function setDefaults() {
    $.datepicker.setDefaults({
        dateFormat: "dd/mm/yy",
        gotoCurrent: true,
        changeMonth: true,
        changeYear: true
    });
    $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);
}

//process back app
function onBackPressed() {
    require(['jsmobile'], function (Jsmobile) {
        Jsmobile.$("#btnbacktolist").trigger("click");
        Jsmobile.autoHideMainMemu(false);
        Jsmobile._hideSearchClick();
    });
}