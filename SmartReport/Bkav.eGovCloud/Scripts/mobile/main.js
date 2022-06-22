
define(function () {
    "use strict";

    var appType = {
        documents: "documents",
        bmail: "bmail",
        chat: "chat",
        calendar: "calendar",
        contacts: "contacts",
        notifications: "notifications"
    };

    var Mobile = Backbone.View.extend({

        el: document,

        activeApps: ["documents", "mail", "chat", "calendar", "contact"],

        events: {
            // "orientationchange": "orientation",

            "click .logout": "logOut",
            'click #menu-logout': 'logOut',
            "click .reloadApp": "resetCache",
            "click .egov-appmenu > span": "initApp",

            "click #btnbacktolist": "hideDetailPage",
            "click #backDocumentPage": "backDetailPage",
            "click .mdl-layout__drawer-button": "showMainMenu",
            "click .mdl-layout__obfuscator": "_hideMainMenu",

            "click #btnShowSearch": "showSearch",
            "focus #txtSearch": "focusSearch",
            "blur #txtSearch": "hideSearch",
            "keyup #txtSearch": "quickSearch",
            'click #search-document-detail-back': 'hideDocumentDetail',
        },

        elements: {
            main: $('.egov'),
            mainPage: $("#main-page"),
            header: $(".egov-header"),
            headerTitle: $(".egov-header #header-title"),
            leftPanel: $(".egov-leftmenu"),
            mainContent: $('.egov-maincontent'),
            appMenu: $(".egov-appmenu"),
            tree: $(".treelist"),
            detailLayout: $(".detail-page"),
            searchLayout: $('.search-page'),
            infoLayout: $('.info-page'),
            dataList: $(".dataList"),
            ddlDoctypeList: $("#listNewDocument"),
            documentDetail: $('#search-document-detail'),
        },

        $egov: $(".egov"),

        displayClass: 'display',
        currentApp: "",
        listScrollTop: 0,
        isTablet: false,
        swipeEvent: {},
        touchPos: {},
        isLoading: false,
        iseGovApp: false,

        initialize: function () {
            var that = this;

            egov.mobile = this;
            egov.mobile.current = {};

            this.isTablet = window.innerWidth > 1024;
            this.hasRenderApp = [];

            this._getScreenSize();
            this._layout();

            $(".egov-appmenu > span").hide();
            _.each(this.activeApps, function (activeApp) {
                $(".egov-appmenu #" + activeApp).show();
            });

            this._getBasicConnectionData(function () {
                try {
                    $('#chat-content').append("<iframe class='full-wrap no-border' id='chat' src='" + egov.connections.ChatLink + "' />");
                }
                catch (ex) { };

                that.initApp();
            });

            // this._loginBmail();

            this._registerGlobalApi();

            // this.bindSpecialEvent();

            // Kiểm tra đang chạy trong app
            this._checkInApp();

            this.upgradeMaterial(".mdl-snackbar");

            $('#notif').click(function () {
                if ($('.mdl-layout__drawer-right').hasClass('active')) {
                    $('.mdl-layout__drawer-right').removeClass('active');
                }
                else {
                    $('.mdl-layout__drawer-right').addClass('active');
                }
            });

            $('.mdl-layout__obfuscator-right').click(function () {
                if ($('.mdl-layout__drawer-right').hasClass('active')) {
                    $('.mdl-layout__drawer-right').removeClass('active');
                }
                else {
                    $('.mdl-layout__drawer-right').addClass('active');
                }
            });
        },

        showProcessBar: function () {
            this.$("#processBar").show();
        },

        hideProcessBar: function () {
            this.$("#processBar").hide();
        },

        upgradeMaterial: function (elements) {
            if (typeof elements === "string") {
                elements = $(elements);
            }

            if (elements === undefined || elements.length === 0) {
                return;
            }

            elements.each(function (i, element) {
                componentHandler.upgradeElement(element);
            });
        },

        upgradeTimeago: function () {
            // Xử lý hiển thị ngày tháng
            $(".timeago").timeago();
        },

        _checkInApp: function () {
            if (/iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream) {
                this.isIOS = true;
            }

            try {
                if (eGovApp) {
                    this.iseGovApp = true;
                    this.$detailLayout.removeClass("trans trans-right");
                    this.$mainPage.addClass("egovapp");
                    $(".btnExit").show();
                }
            } catch (e) {

            }
        },

        _layout: function () {
            this.$(".app-view").height(egov.screenSize.h);
            this.$(".app-view > div").height(egov.screenSize.h);

            this.elements.mainContent.height(egov.screenSize.contentHWithoutFooter);
            this.elements.mainContent.find(".page-content").css('minHeight', this.elements.mainContent.height() - 30); // 16 padding

            this.elements.tree.height(egov.screenSize.h - 127);
            var contentM = egov.screenSize.contentMaxW > 930 ? 930 : egov.screenSize.contentMaxW;
            this.$('.mdl-layout__drawer').width(contentM);
        },

        _getScreenSize: function () {
            var w = $(window);
            egov.screenSize = {
                w: w.width(),
                h: w.height(),
                footerH: 0,
                contentMaxW: Math.round(w.width() * 0.7), // độ rộng tối đa nội dung trao đổi, comment, ...
                contentH: w.height() - this.elements.header.height(),
                contentHWithoutFooter: w.height() - this.elements.header.height() - 0
            };
        },

        initApp: function (e) {
            var isClickToApp = e != undefined;
            if (isClickToApp) {
                this.changeApp($(e.currentTarget).attr("data-ng-app"));
                return;
            }

            var that = this;
            var startApp = "documents";  // documents, bmail, chat, calendar
            this.changeApp(startApp, function () {
            });
        },

        changeApp: function (name, callback) {
            var that = this;
            this.currentApp = name;
            this.app = name;

            var $menuItem = $('.menu-items[data-ng-app = "' + name + '"]');
            var isActiving = $menuItem.hasClass('active');

            if (isActiving) {
                this._reloadAppContent(name);
                egov.callback(callback);
                return;
            }

            that._activeAppMenuAndContainer(name);
            that._renderApp(name, callback);
        },

        _renderApp: function (name, callback) {
            var that = this;
            if (this.hasRenderApp[name]) {
                return;
            }

            this.hasRenderApp[name] = true;
            switch (name) {
                case appType.bmail:
                    require(['bmailView'], function (BmailView) {
                        egov.bmailView = new BmailView;
                        egov.callback(callback);
                    });
                    return;
                case appType.documents:
                    require(['documentsView'], function (HomeView) {
                        egov.views.home = new HomeView;
                        that.getDoctypeList();
                        egov.callback(callback);
                    });
                    return;
                case appType.chat:
                    require(['chatViewMain'], function (MobileChat) {
                        new MobileChat;
                        egov.callback(callback);
                    });
                    return;
                case appType.calendar:
                    require(['calendarMobile'], function (Calendar) {
                        new Calendar;
                    });
                    return;
                case appType.notifications:
                    require(['notifyMobile'], function (Notification) {
                        window.eGovNotification = new Notification;
                    });
                    return;
                default:
                    return;
            }
        },

        reloadDocumentList: function () {
            egov.views.home && egov.views.home.tree && egov.views.home.tree.reloadSelectedNode();
        },

        showMenuDocument: function () {
            this.$('#egovMaincontent').addClass('hidden');
            this.$('.treelist').removeClass('hidden');
        },

        _reloadAppContent: function (appName) {
            var that = this;
            switch (appName) {
                case appType.bmail:

                    break;
                case appType.documents:
                    egov.documentMenuInMainView ? that.showMenuDocument() : that.reloadDocumentList();
                    break;
                case appType.chat:

                    break;
                case appType.notifications:
                    if (eGovNotification) {
                        eGovNotification.reset();
                    }
                    break;
                default:
                    break;
            }
        },

        _activeAppMenuAndContainer: function (name) {
            var $menuItem = $('.menu-items[data-ng-app = "' + name + '"]');
            $menuItem.siblings().removeClass("active");
            $menuItem.addClass('active');

            var container = $(".egov-app[data-app='" + name + "']");
            container.siblings().removeClass("display");
            container.addClass("display");

            var detail = $(".dataDetail > div[data-app='" + name + "']");
            detail.siblings().hide();
            detail.show();

            var leftMenu = $(".treelist div[data-app='" + name + "']");
            leftMenu.siblings().hide();
            leftMenu.show();

            container.prev(".egov-app").removeClass("trans-right").addClass("trans-left");
            container.next(".egov-app").removeClass("trans-left").addClass("trans-right");

            var $title = this.elements.headerTitle.find("span[data-app='" + name + "']");
            if ($title) {
                $title.show();
                $title.siblings().hide();
            }

            this.activeFullHeight(true);

            // componentHandler.upgradeDom()

            this.autoHideMainMemu();
        },

        setAppInfo: function () {
            if (this.iseGovApp) {
                eGovApp.setSessionId(bmail.mailSessionId);
                eGovApp.setMailToken(bmail.bmailNewAuthenCookie);
            }
        },

        _registerGlobalApi: function () {
            var that = this;

            window.readNewMail = function (mailData) {
                /*
                 * Hàm mở mail khi notify sử dụng cho Bmail. Lưu ý, ko đổi tên hàm này.
                 */
                var mailId, folderId;
                if (typeof mailData === "string") {
                    mailData = JSON.parse(mailData);
                }

                mailId = mailData.MailId;
                folderId = mailData.FolderId;

                that.changeApp("bmail");
            };

            window.openChat = function () {
                that.changeApp("chat");
            };

            $(window).on('resize', function () {
                this._getScreenSize();
                egov.pubsub.publish('window.resize');
            }.bind(this));
        },

        _loginBmail: function () {

        },

        //#region Events

        //#region Layout Event

        ipadShowMenu: function (e) {
            egov.helper.destroyClickEvent(e);
            this.$mainLayout.toggleClass("showingusermenu");
        },

        //#endregion

        logOut: function () {
            var cookies = document.cookie.split(";");
            for (var i = 0; i < cookies.length; i++) {
                var eqPos = cookies[i].indexOf("=");
                var name = eqPos > -1 ? cookies[i].substr(0, eqPos) : cookies[i];
                name = name.trim();
                $.cookie(name, "", { domain: document.domain, path: "/", expires: -1, secure: true });
                $.cookie(name, "", { expires: -1, secure: true });
            }

            if (this.iseGovApp) {
                eGovApp.logout();
            }
            window.location.href = '/account/logout';
        },

        resetCache: function () {
            window.location.reload(true);
        },

        //#endregion

        //#region show hide Detail Page, search page, main page

        _hideMainMenu: function (e) {
            if ($(".mdl-layout__container .mdl-layout__obfuscator").length > 1) {
                $(".mdl-layout__container .mdl-layout__obfuscator.is-visible").first().remove();
            }

            this.elements.mainPage.removeClass("showmenu");
        },

        autoHideMainMemu: function (e) {
            $("." + MaterialLayout.prototype.CssClasses_.OBFUSCATOR).removeClass(MaterialLayout.prototype.CssClasses_.IS_DRAWER_OPEN);
            $("." + MaterialLayout.prototype.CssClasses_.DRAWER).removeClass(MaterialLayout.prototype.CssClasses_.IS_DRAWER_OPEN);
            this._hideMainMenu(e);
        },

        hideMainMenu: function (e) {
            // egov.helper.destroyClickEvent(e);
            this.autoHideMainMemu(e);
            if (egov.mobile.iseGovApp) {
                // eGovApp.toggleMenu(false);
            }
        },

        showMainMenu: function (e) {
            var that = this;

            $(".mdl-layout__drawer").addClass("lg-drawer is-visible");
            switch (this.currentApp) {
                case appType.bmail:
                    if (!this.bmailLoaded) {
                        if ($("#folderlist .select").parents(".parentFolder").last().length > 0) {
                            if ($("#folderlist .select").parents(".parentFolder").length > 0) {
                                $(".treelist").scrollTop($("#folderlist .select").parents(".parentFolder").last().position().top);
                            }
                            this.bmailLoaded = true;
                        }
                    }
                    break;
                default:
                    if (!this.documentLoaded) {
                        if ($("#menu-document .active").length > 0) {
                            $(".treelist").scrollTop($("#menu-document .active").position().top);
                        }
                        this.documentLoaded = true;
                    }
                    break;
            }

            $(e.currentTarget).attr("data-badge", 0);
            this.elements.mainPage.addClass("showmenu");
            $(document).one("swipeleft", function () {
                that.hideMainMenu();
            });
            if (egov.mobile.iseGovApp) {
                eGovApp.toggleMenu(true);
            }
        },

        backDetailPage: function (e) {
            // egov.helper.destroyClickEvent(e);
            egov.mobile.current.document.$el.find(".commentMobilePage").removeClass("display");
            egov.mobile.current.document.$el.find(".document").addClass("display");
        },

        showDetailPage: function (appName) {
            var that = this;
            this.currentPage = this.showingDetail ? this.currentPage : this.$(".egov > div.display:not(.detail-page)");

            this.listScrollTop = $('body').scrollTop();

            this.showingDetail = true;
            $("#main-page, .search-page").removeClass(this.displayClass);

            this.elements.detailLayout.addClass(this.displayClass);
            var appDetail = this.elements.detailLayout.find("[data-app='" + appName + "']");
            appDetail.removeClass("hidden").show().siblings().addClass("hidden");
        },

        hideDetailPage: function () {
            this.showingDetail = false;
            this.elements.detailLayout.removeClass(this.displayClass);

            this.currentPage ? this.currentPage.addClass(this.displayClass) : $("#main-page").addClass(this.displayClass);
            this.currentPage = null;
        },

        showSearchPage: function (appName) {
            var that = this;
            this.searching = true;

            this.currentPage = this.$(".egov > div.display");

            this.listScrollTop = $('body').scrollTop();

            $("#main-page").removeClass(this.displayClass);

            if (!this.isTablet) {
                this.elements.mainPage.addClass("searching");
            }

            this.elements.searchLayout.addClass(this.displayClass);
            var appSearch = this.elements.searchLayout.find("[data-app='" + appName + "']");
            appSearch.removeClass("hidden").show().siblings().addClass("hidden");
        },

        hideSearchPage: function () {
            this.searching = false;
            this.elements.searchLayout.removeClass(this.displayClass);

            this.currentPage ? this.currentPage.addClass(this.displayClass) : $("#main-page").addClass(this.displayClass);
            this.currentPage = null;
        },

        showDocumentDetail: function (id) {
            
            var that = this;
            egov.request.quickViewDocument(
                {
                    data: { id: id },
                    success: function (data) {
                        if (!data) {
                            return;
                        }
                        else {

                            that.currentPage = that.$(".egov > div.display");
                            that.currentPage.length === 0 && (that.currentPage = $("#main-page"));

                            that.listScrollTop = $('body').scrollTop();

                            that.currentPage.removeClass(that.displayClass);
                            that.elements.documentDetail.addClass(that.displayClass);

                            require(['documentView'], function (DocumentView) {

                                //egov.mobile.showDetailPage("documents");
                                data.id = data.id;
                                var documentView = new DocumentView({
                                    id: id,
                                    parent: that,
                                    tab: that,
                                    documentInfo: data
                                });

                                that.elements.documentDetail.find('.documents-detail').html(documentView.$el);
                                that.elements.documentDetail.find('#search-document-detail-title').html(data.docType);
                                //egov.mobile.current.tab = documentView.$el;

                                if (egov.document._hasUpdateViewed(data)) {
                                    egov.document._setViewed(doc.DocumentCopyId, true);
                                }
                            });
                        }
                    },
                    error: function () {

                    }
                }
            );
        },


        hideDocumentDetail: function () {
            this.elements.documentDetail.removeClass(this.displayClass);

            this.elements.searchLayout.addClass(this.displayClass);

            this.currentPage = this.elements.searchLayout;

            this.elements.documentDetail.find('.documents-detail').html('');

            $('#documentFrame .documents-detail').find('#formContentmbsc').addClass('mbsc-form mbsc-no-touch mbsc-mobiscroll-dark mbsc-form-hb mbsc-mobiscroll mbsc-ltr');
        },

        showInfoPage: function () {
            var that = this;

            this.currentPage = this.$(".egov > div.display");
            this.currentPage.length === 0 && (this.currentPage = $("#main-page"));

            this.listScrollTop = $('body').scrollTop();

            this.currentPage.removeClass(this.displayClass);
            this.elements.infoLayout.addClass(this.displayClass);
        },

        hideInfoPage: function () {
            this.elements.infoLayout.removeClass(this.displayClass);
            this.elements.infoLayout.empty();
            this.currentPage ? this.currentPage.addClass(this.displayClass) : $("#main-page").addClass(this.displayClass);
            this.currentPage = null;
        },

        //#endregion

        //#region Download

        downloads: function (downloadUrl) {
            if (this.iseGovApp) {
                var date = new Date();
                var fullDate = date.format("dd/MM/yyyy");
                eGovApp.downloadFile(downloadUrl, "eGov-Attachments-" + fullDate + ".zip");
            }
            else {
                window.open(downloadUrl);
            }
        },

        download: function (downloadUrl, fileName) {// download ket hop vs open file
            this.iseGovApp ? eGovApp.downloadFile(downloadUrl, fileName) : window.open(downloadUrl);
        },

        onlyDownload: function (downloadUrl, fileName) { //chi download file
            this.iseGovApp ? eGovApp.onlyDownloadFile(downloadUrl, fileName) : window.open(downloadUrl);
        },

        //#endregion

        //#region Status

        showStatus: function (message, autoHide, actionText, actionHandler) {
            /*
             *  message: "",
                actionHandler: function (event) {},
                actionText: 'Undo',
                timeout: 10000
             */
            autoHide = autoHide || true;

            var timeout = autoHide ? 2750 : 100000;
            var notification = document.querySelector('.mdl-js-snackbar');
            var data;

            if (typeof message === "string") {
                data = {
                    message: message,
                    timeout: timeout,
                    actionText: actionText,
                    actionHandler: actionHandler
                };
            } else {
                data = message;
            }

            notification.MaterialSnackbar
                && notification.MaterialSnackbar.showSnackbar
                && notification.MaterialSnackbar.showSnackbar(data);
        },

        hideStatus: function () {
            var notification = document.querySelector('.mdl-js-snackbar');
            notification.MaterialSnackbar
                && notification.MaterialSnackbar.cleanup_
                && notification.MaterialSnackbar.cleanup_();
        },

        //#endregion

        //#region Private methods

        setNofifyFolder: function (folderPath) {
            //if (egov.setting.mailNotifyFolder.split(",").indexOf(folderPath) < 0) {
            //    try {
            //        $.ajax({
            //            url: '/Notification/SetMailFolderNotify',
            //            type: 'POST',
            //            data: { folderPath: folderPath }
            //        }).success(function () {
            //            egov.setting.mailNotifyFolder += "," + folderPath;
            //            console.log("add new mail folder to notify (" + folderPath + ")");
            //        });
            //    } catch (e) {
            //        console.error(e);
            //    }
            //}
        },

        showCreateDocumentMenu: function (e) {
            egov.helper.destroyClickEvent(e);
            var $icon = $(e.currentTarget).children(".material-icons");
            var statusText = $icon.text();
            if (statusText == "create") {
                $icon.text("close");
            }
            else {
                $icon.text("create");
            }
        },

        getDoctypeList: function () {
            var that = this;
            if (this.loadedDoctypeList) {
                return;
            }

            var results = egov.setting.doctypes;

            $("#btnNewDocument").removeClass("hidden");
            results = results.sort(function (a, b) {
                return a.Pinned == true;
            });
            _.each(results, function (item) {
                var doctypeItem = new DoctypeItem(item);
                that.elements.ddlDoctypeList.append(doctypeItem.$el);
            });
            that.loadedDoctypeList = true;

            componentHandler.upgradeDom();
        },

        showMailFolderOption: function () {
            var that = this;
            this.showModal();
            $(document).one("click", function (e) {
                if ($(e.target).closest("#ddlMailFolderMenu").length == 0) {
                    // egov.helper.destroyClickEvent(e);
                }

                that.hideModal();
            })
        },

        activeFullHeight: function (disable) {
            //var $el = $("#chatFrame");
            //if (!disable) {
            //    while ($el.parent().length > 0) {
            //        $el.parent().addClass("full-height");
            //        $el = $el.parent();
            //    }
            //}
            //else {
            //    while ($el.parent().length > 0) {
            //        $el.parent().removeClass("full-height");
            //        $el = $el.parent();
            //    }
            //}
        },

        changeAppTitle: function (app, title) {
            var $title = this.elements.headerTitle.find("span[data-app='" + app + "']");
            if ($title) {
                $title.text(title);
            }
        },

        //#endregion

        //#region Get Settings

        _getBasicConnectionData: function (complete) {
            $.ajax({
                url: '/Home/GetConnectionSettings',
                success: function (connections) {
                    egov.connections = connections;
                },
                error: function () {
                    console.warn("unknown mail address");
                },
                complete: function () {
                    egov.callback(complete);
                }
            });
        }

        //#endregion
    });


    var DoctypeItem = Backbone.View.extend({

        tagName: "li",
        className: "mdl-menu__item",

        categoryBussinessId: 0,

        initialize: function (options) {
            this.id = options.DocTypeId;
            this.name = options.DocTypeName;
            this.categoryBusinessId = options.CategoryBusinessId;

            this.render();
        },

        events: {
            "click": "createNewDocument"
        },

        render: function () {
            this.$el.css({ "max-width": "420px", "color": "white", "opacity": "1" })
            this.$el.html('<img class="mdl-left-img" src="../../Content/Images/home/list.png" alt=""><span>' + this.name + '</span>');
        },

        createNewDocument: function (e) {
            var that = this;
            require(['documentView'], function (DocumentView) {

                egov.mobile.showDetailPage("documents");

                var documentView = new DocumentView({
                    parent: that,
                    tab: that,
                    isCreate : true,
                    documentInfo: {DocTypeId : that.id}
                });

                $('.documents-dataDetail').html(documentView.$el);
                egov.mobile.current.tab = documentView.$el;

            });
        }
    });

    egov.createCookie = function (name, value, callback) {
        $.ajax({
            url: '/Account/CreateCookie',
            type: 'POST',
            data: { name: name, value: value }
        }).success(function () {
            egov.callback(callback);
        });
    }

    $.fn.doubleTap = function (doubleTapCallback) {
        return this.each(function () {
            var elm = this;
            var lastTap = 0;
            $(elm).bind('vmousedown', function (e) {
                var now = (new Date()).valueOf();
                var diff = (now - lastTap);
                lastTap = now;
                if (diff < 250) {
                    if ($.isFunction(doubleTapCallback)) {
                        doubleTapCallback.call(elm);
                    }
                }
            });
        });
    }

    $.fn.scrollToBottom = function () {
        var objDiv = this.get(0);
        objDiv.scrollTop = objDiv.scrollHeight;
    }

    $.fn.scrollLoadMore = function (direction, callback) {
        if (typeof callback !== 'function') {
            return;
        }

        direction = direction || 'next';

        ScrollLoadMore && new ScrollLoadMore(this.get(0), {
            offset: 70,
            direction: direction,
            callback: function () {
                typeof callback === 'function' && callback();
            }
        });
    }

    return new Mobile();
});
