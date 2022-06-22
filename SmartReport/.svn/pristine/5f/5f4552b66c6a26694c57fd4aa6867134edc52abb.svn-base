define([
    egov.template.tab.desktop
], function (tabTemplate) {
    "use strict";

    //#region Model

    var tabType = {
        document: 0,
        store: 1,
        search: 2,
        report: 4,
        print: 8,
        addImagePacket: 16
    };

    var _resource = egov.resources.file;
    var userSettings = egov.setting.user.userSetting;

    //#endregion

    //#region View

    /// <summary>Đối tượng View quản lý các tab trong hệ thống.</summary>
    var Tabs = Backbone.View.extend({
        //Thẻ chưa văn bản hồ sơ
        //Trên mobile-tablet thì chỉ mở 1 văn bản , hồ sơ
        //Trên giao diện dessktop thì cho phép mở nhiều văn bản cùng 1 lúc
        el: egov.isMobile ? "#document-detail" : "#ulTabs",

        firstModel: [],

        events: {
        },

        // Khởi tạo
        initialize: function (options) {
            //Nếu là mobile - tablet
            if (!egov.isMobile) {
                this.initDefault();
            }
            else if (egov.isMobile && options.model) {
                //De tam cai nay, khong hieu sao toan bat phai UlTabs
                this.$el = $("#document-detail");
                this.initMobile(options);
            }

            egov.views.home.tab = this;
        },

        //Khởi tao khi là giao diện bình thường
        initDefault: function () {
            this.model = new egov.models.TabList;

            this._eventChange();

            this.addHome();
            this.addSearch();

            this._getTabFomCache();

            this.$el.bindResources();
        },

        _eventChange: function () {
            var that = this;

            this.model.on('add', function (tabModel) {
                var tabView = new TabView({
                    model: tabModel,
                    tabs: that.model
                });

                that.$el.append(tabView.$el);

                // Nếu thêm tab từ cookie (tự động lưu) thì chỉ thêm title

                if (tabModel.get('hasLoadContent')) {
                    // tabView.$('a').tab('show');
                    tabView.active();
                }

                if (tabModel.get('isRoot') && tabModel.get('name') === 'home') {
                    that.rootTab = tabView;
                }

                egov.callback(tabModel.get("callback"), tabView);
            });

            // Trigger khi remove tab
            this.model.on('remove', function (tab, model, option) {
                // Nếu tab đóng đang được active thì active tab trước nó
                if (tab.view.isActivated()) {
                    var haveSearchTab = false;
                    var prevTab;              

                    if (!haveSearchTab) {
                        prevTab = model.at(0);
                    }

                    if (prevTab) {
                        prevTab.view.active();
                    } else {
                        $('#documentProcessing').show();
                    }
                }

                that.resizeTab();
            });
        },

        _getTabFomCache: function () {
            ///<summary>
            ///Lấy danh sách các tab được lưu ở cache
            ///</summary>
            if (userSettings.isSaveOpenTab) {
                // Bind lại danh sách các tab đã được lưu cookie
                var recentTab = []
                if (egov.locache.hasSupportLocalStorage) {
                    recentTab = egov.localStorage.getRecentTab(egov.setting.userName);
                } else {
                    recentTab = egov.cookie.getRecentTab(egov.setting.userName);
                }

                if (recentTab && recentTab.length > 0) {
                    for (var i = 0; i < recentTab.length; i++) {
                        var tab = recentTab[i];
                        tab.hasLoadContent = false;
                        this.model.add(tab);
                    }
                }
            }
        },

        addHome: function () {
            ///<summary>
            /// Thêm tab home
            ///</summary>
            this.model.add({
                title: "Báo cáo",
                name: 'home',
                icon: 'home.png',
                href: 'documentProcess',
                hasTooltip: false,
                hasCloseButton: false,
                isRoot: true
            });
        },

        addSearchHome: function () {
            ///<summary>
            /// Thêm tab home
            ///</summary>
            this.model.add({
                title: 'Tìm kiếm',
                name: 'search',
                icon: 'home.png',
                href: 'searchTab',
                hasTooltip: false,
                hasCloseButton: false,
                isRoot: true,
                hasLoadContent: false,
                type: tabType.search
            });
        },

        addReportBCTH: function () {
            ///<summary>
            /// Thêm tab báo cáo thống kê
            ///</summary>
            var tab = this.model.detect(function (tab) {
                return tab.get('name') === 'reportTH';
            });

            if (tab) {
                tab.view.active();
            }
            else {
                this.model.add({
                    name: 'reportTH',
                    title: egov.resources.tab.report.title,
                    icon: 'report.png',
                    href: 'http://smartnation.bkav.com/reportviewer/ReportGeneral?theme=shine&reportId=255',
                    hasTooltip: false,
                    hasCloseButton: false,
                    isRoot: true,
                    hasLoadContent: false,
                    type: tabType.report
                });
            }
        },

        addReport: function () {
            ///<summary>
            /// Thêm tab báo cáo thống kê
            ///</summary>
            var tab = this.model.detect(function (tab) {
                return tab.get('name') === 'report';
            });

            if (tab) {
                tab.view.active();
            }
            else {
                this.model.add({
                    name: 'report',
                    title: egov.resources.tab.report.title,
                    icon: 'report.png',
                    href: 'report',
                    url: '/ReportViewer/Index',
                    hasCloseButton: true,
                    type: tabType.report
                });
            }
        },

        addPrint: function () {
            ///<summary>
            /// Thêm tab in
            ///</summary>
            var tab = this.model.detect(function (tab) {
                return tab.get('name') === 'print';
            });
            if (tab) {
                tab.view.active();
            }
            else {
                this.model.add({
                    name: 'print',
                    title: egov.resources.tab.print.title,
                    icon: 'print.png',
                    href: 'printTab',
                    url: '/Print/ExpressPrint',
                    hasCloseButton: true,
                    type: tabType.print
                });
            }
        },

        addDocument: function (id, title, relationId, isPacket, attachment, contentHTML, callback, info, relationType) {
            //addDocument: function (id, title, relationId, categoryBusinessId) {
            /// <summary>
            /// Tab thêm mới hồ sơ, văn bản
            /// </summary>
            /// <param name="id">DoctypId</param>
            /// <param name="title">Tiêu đề tab</param>
            /// <param name="relationId">DocumentCopyId của văn bản gốc: sử dụng cho trường hợp trả lời văn bản.</param>
            var model = new egov.models.document({
                DocTypeId: id,
                //Compendium: "Việt DŨng",
                Attachments: attachment
            });

            if (info) {
                model = info;
            }

            var tabModel = new egov.models.TabModel({
                name: 'newDocument' + id,
                id: id,
                privateId: id,
                title: title ? title : egov.resources.tab.newDocument,
                icon: 'new-document.png',
                url: '/Document/Create/' + id,
                href: 'tabContentCreate_' + id,
                relationId: relationId,
                relationType: relationType,
                isCreateDocument: true,
                hasCloseButton: true,
                isPacket: isPacket,
                attachment: attachment,
                contentHTML: contentHTML,
                callback: callback,
                documentInfo: model
            });

            if (relationId) {
                tabModel.set('url', tabModel.get('url') + '&documentCopyRelationId=' + relationId);
            }
            var exists = _.filter(this.model.toJSON(), function (item) {
                return item.privateId === tabModel.get('privateId');
            });
            if (exists) {
                tabModel.set('href', tabModel.get('href') + '_' + exists.length);
                tabModel.set('id', tabModel.get('id') + '_' + exists.length);
            }
            this.model.add(tabModel);
            return tabModel;
        },

        duplicateDocument: function (copiedDocument, getNewInfoFromServer, callback) {

            var that = this,
                docCopyId,
                docTypeId,
                title,
                createTabFunc,
                tabModel,
                exists,
                tempDoc;

            docTypeId = copiedDocument.get("DocTypeId");
            title = copiedDocument.get("Compendium");
            docCopyId = copiedDocument.get("DocumentCopyId");

            createTabFunc = function () {
                tabModel = new egov.models.TabModel({
                    name: 'newDocument' + docTypeId,
                    id: docTypeId,
                    privateId: docTypeId,
                    title: title ? title : egov.resources.tab.newDocument,
                    icon: 'new-document.png',
                    url: '/Document/Create/' + docTypeId,
                    href: 'tabContentCreate_' + docTypeId,
                    isCreateDocument: true,
                    hasCloseButton: true,
                    callback: callback,
                    copiedDocument: tempDoc,
                    documentInfo: new egov.models.document({
                        DocTypeId: docTypeId,
                    })
                });
                exists = _.filter(that.model.toJSON(), function (item) {
                    return item.privateId === tabModel.get('privateId');
                });
                if (exists) {
                    tabModel.set('href', tabModel.get('href') + '_' + exists.length);
                    tabModel.set('id', tabModel.get('id') + '_' + exists.length);
                }
                that.model.add(tabModel);
            };

            if (!getNewInfoFromServer) {
                tempDoc = copiedDocument;
                createTabFunc();
                return;
            }

            egov.request.editNew({
                data: { id: docCopyId },
                success: function (result) {
                    if (result) {
                        if (result.error) {
                            egov.pubsub.publish(egov.events.status.error, result.error);
                            return;
                        }

                        tempDoc = new egov.models.document(result.document);
                        createTabFunc();
                    }
                },
                error: function (xhr) {
                    $(egov.views.home.tree.documentList).addClass('display');
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                }
            });

            //egov.dataManager.getDocumentEditInfo(docCopyId, {
            //    success: function (data) {
            //        if (data) {
            //            if (data.error) {
            //                that.tab.close();
            //                egov.pubsub.publish(egov.events.status.error, data.error);
            //                return;
            //            }
            //            tempDoc = new egov.models.document(data);
            //            createTabFunc();
            //        }
            //    }, error: function (xhr) {
            //        egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
            //    }, complete: function () {
            //        egov.pubsub.publish(egov.events.status.destroy);
            //    }
            //});

            //return tabModel;
        },

        addSearch: function (searchQuery, searchType) {
            var tab = this.model.detect(function (tab) {
                return tab.get('name') === 'search';
            });

            if (tab) {
                tab.view.close();
            }

            this.model.add({
                name: 'search',
                title: 'Tìm kiếm',
                href: 'searchTab',
                type: tabType.search,
                hasCloseButton: true,
                searchQuery: searchQuery,
                searchType: searchType
            });
        },

        addImagePacket: function () {
            var currentImage,
                tab;

            tab = this.model.detect(function (tab) {
                return tab.get('name') === 'addImagePacket';
            });

            if (tab) {
                tab.view.active();
            }

            if (egov.waitingImagePacket) {
                currentImage = _.find(egov.waitingImagePacket, function (item) {
                    return !item.opened;
                });
            }

            egov.hasImagePacket = currentImage != null;

            if (egov.hasImagePacket) {
                this.model.add({
                    name: 'addImagePacket',
                    title: 'Đính kèm ảnh theo lô',
                    href: 'imagePacket',
                    type: tabType.addImagePacket,
                    hasCloseButton: true,
                });
            }
        },

        openDocument: function (id, title, hasLoadContent, callback) {
            //<summary> Mở văn bản</summary>
            /// <param name="id" type="int">DocumentCopy id</param>
            /// <param name="title" type="String">Trích yếu</param>
            //if (userSettings.ViewDocInPopUp) {
            //    require(['egovPopup'], function () {
            //        var link = "/Document/EditNew/?id=" + id + "&isPopup=true";
            //        egov.popup.open(link, title);
            //    });
            //    return;
            //}

            var documentModel = new egov.models.document({
                DocumentCopyId: id,
                Compendium: title
            });

            this.openDocumentNew(documentModel);
        },

        openDocumentNew: function (documentInfo, callback) {
            /// <summary>
            /// Hàm mở văn bản từ danh sách văn bản, sử dụng những thông tin đã lấy trên danh sách
            /// </summary>
            /// <param name="document">document</param>
            /// <param name="callback">call back</param>

            var id = documentInfo.get('DocumentCopyId');
            var tab = this.model.detect(function (tab) {
                return tab.get('name') === 'document' + id;
            });

            if (tab) {
                tab.view.active();
                if (typeof callback === 'function') {
                    callback();
                }
                return;
            }

            var tabModel = {
                name: 'document' + id,
                id: id,
                privateId: id,
                title: documentInfo.get('Compendium') ? documentInfo.get('Compendium') : egov.resources.tab.newDocument,
                icon: 'document.png',
                hasCloseButton: true,
                url: '/Document/Edit/' + id,
                href: 'tabContentEdit_' + id,
                documentInfo: documentInfo,
                callback: function () {
                    if (typeof callback === 'function') {
                        callback();
                    }
                },
                hasLoadContent: true,
                checkSuccessed: true
            };
            this.model.add(tabModel);

            if (userSettings.isSaveOpenTab) {
                this.save(tabModel);
            }
        },

        openDocumentInfo: function (documentInfo, hasLoadContent, callback) {
            /// <summary>
            /// Hàm mở văn bản từ danh sách văn bản, sử dụng những thông tin đã lấy trên danh sách
            /// </summary>
            /// <param name="document">document</param>
            /// <param name="hasLoadContent">Có load nội dung khi mở văn bản hay không</param>
            /// <param name="callback">call back</param>
            if (userSettings.ViewDocInPopUp) {
                require(['egovPopup'], function () {
                    var link = "/Document/EditNew/?id=" + documentInfo.get('DocumentCopyId') + "&isPopup=true";
                    egov.popup.open(link, documentInfo.get('Compendium'));
                });
                return;
            }

            var id = documentInfo.get('DocumentCopyId');
            var tab = this.model.detect(function (tab) {
                return tab.get('name') === 'document' + id;
            });

            if (tab) {
                if (hasLoadContent) {
                    tab.view.active();
                }

                if (typeof callback === 'function') {
                    callback();
                }
                return;
            }
            var tabModel = {
                name: 'document' + id,
                id: id,
                privateId: id,
                title: documentInfo.get('Compendium'),
                icon: 'document.png',
                hasCloseButton: true,
                url: '/Document/Edit/' + id,
                href: 'tabContentEdit_' + id,
                documentInfo: documentInfo,
                callback: function () {
                    if (typeof callback === 'function') {
                        callback();
                    }
                },
                checkSuccessed: true,
                hasLoadContent: hasLoadContent
            };

            this.model.add(tabModel);

            if (userSettings.isSaveOpenTab) {
                this.save(tabModel);
            }
        },

        openMultiDocument: function (arrOptions, isOpenAll) {
            if (!arrOptions || arrOptions.length <= 0)
                return;

            isOpenAll = isOpenAll || false;
            var that = this;
            if (isOpenAll) {
                var option = arrOptions[0];
                this.openDocument(option.id, option.title, function () {
                    arrOptions = arrOptions.shift();
                    that.openMultiDocument(arrOptions, isOpenAll);
                    egov.callback(option.callback);
                });

                return;
            }

            for (var i = 0; i < arrOptions.length; i++) {
                var option = arrOptions[i];
                var tab = this.model.detect(function (tab) {
                    return tab.get('name') === 'document' + option.id;
                });

                if (!tab) {
                    var tabModel = {
                        name: 'document' + option.id,
                        id: option.id,
                        privateId: option.id,
                        title: option.title,
                        icon: 'document.png',
                        hasCloseButton: true,
                        url: '/Document/Edit/' + option.id,
                        href: 'tabContentEdit_' + option.id,
                        callback: function () {
                            egov.callback(option.callback);
                        },

                        hasLoadContent: (i === arrOptions.length - 1),
                    };
                    this.model.add(tabModel);
                    if (userSettings.isSaveOpenTab) {
                        this.save(tabModel);
                    }
                }
            }
        },

        openQuestion: function (question, callback) {
            // <summary> Mở hồ sơ đăng ký qua mạng </summary>
            /// <param name="question">question</param>
            if (egov.isMobile) {
                this.openQuestionMobile(question, callback);
            }
            var id = question.get("QuestionId");
            var title = question.get("Name");
            var tab = this.model.detect(function (tab) {
                return tab.get('name') === 'question' + id;
            });

            if (tab) {
                tab.view.active();
                return;
            }

            var tabModel = {
                name: 'question' + id,
                id: id,
                privateId: id,
                title: title,
                hasCloseButton: true,
                icon: 'document.png',
                href: 'tabQuestion_' + id,
                isQuestionMode: true,
                question: question,
                callback: function () {
                    egov.callback(callback);
                },
            };

            this.model.add(tabModel);
        },

        openDocumentOnline: function (id, title, hasLoadContent, callback, saveTab, isOnlineRegistration) {
            // <summary> Mở hồ sơ đăng ký qua mạng </summary>
            /// <param name="id">id</param>
            /// <param name="title" type="String">Trích yếu</param>
            var tab = this.model.detect(function (tab) {
                return tab.get('name') === 'document' + id;
            });
            if (tab) {
                tab.view.active();
                return;
            }

            var tabModel = {
                name: 'document' + id,
                id: id,
                privateId: id,
                title: title,
                hasCloseButton: true,
                icon: 'document.png',
                url: '/Document/Edit/' + id,
                href: 'tabContentOnlineEdit_' + id,
                isDocumentOnline: true,
                isOnlineRegistration: isOnlineRegistration,
                callback: function () {
                    egov.callback(callback);
                },
                hasLoadContent: hasLoadContent
            };
            this.model.add(tabModel);
            if (userSettings.isSaveOpenTab && saveTab) {
                this.save(tabModel);
            }
        },

        // Resize tab
        resizeTab: function () {
            var tabWidth = 95;
            var documentWidth = $(document).outerWidth();
            var count = this.model.length;
            var parentPading = 10; // padding của div tab.
            var marginBetweenTab = count - 1;
            var parentWidth = documentWidth - parentPading - marginBetweenTab;

            var paddingTab = 12;
            var newMaxWidth = Math.floor(parentWidth / count) - paddingTab;
            if (newMaxWidth > tabWidth) {
                newMaxWidth = tabWidth;
            }

            this.model.each(function (tab) {
                tab.view.setWidth(newMaxWidth);
            });
        },

        save: function (model) {
            ///<summary>
            /// Lưu tab để hiển thị vào lần sau
            ///<para name="model" type="object"> Đối tượng lưu tab</para>
            ///</summary>
            if (egov.locache.hasSupportLocalStorage) {
                egov.localStorage.addRecentTab(egov.setting.userName, model);
            } else {
                egov.cookie.addRecentTab(egov.setting.userName, model);
            }
        },

        ////======================== tablet- mobile

        initMobile: function (options) {
            if (options.question) {
                this.openQuestionMobile(options.question);
                return;
            } else {
                this.model = new egov.models.TabModel(options.model);
            }

            this.renderMobile();
        },

        //ren trên giao diện mobile- tablet
        renderMobile: function () {
            var _this = this;
            var id = _this.model.get('id');
            _this.$(".tabdocument").removeClass("display").hide();

            if (this.model.get("isCreate")) {
                _this.$(".addDocument").remove();
                require(['documentViewMobile'], function (DocumentMobileView) {
                    egov.mobile.showDetailPage();
                    var documentView = new DocumentMobileView({
                        id: id,
                        isCreate: true,
                        parent: _this,
                        tab: _this,
                        documentInfo: new egov.models.document({
                            DocTypeId: id,
                        })
                    });
                    documentView.$el.addClass("addDocument display");
                    _this.$el.append(documentView.$el);
                    egov.mobile.current.tab = documentView.$el;
                });
            }
            else {
                if (!this.hasRenderTab("#tabDocument_" + this.model.get("id"))) {
                    _this.$el.append("<div id= " + _this.model.get('href') + " class='display tabdocument'></div>");
                    require(['documentViewMobile'], function (DocumentMobileView) {
                        egov.mobile.showDetailPage();
                        var documentView = new DocumentMobileView({
                            id: id,
                            el: '#' + _this.model.get('href'),
                            parent: _this,
                            tab: _this,
                            documentInfo: _this.model.get("documentInfo")
                        });
                        documentView.$el.addClass("display");
                        _this.$el.append(documentView.$el);
                        egov.mobile.current.tab = documentView.$el;
                    });
                }
            }
        },

        openQuestionMobile: function (question, callback) {
            var questionId = question.get("QuestionId");
            if (!this.hasRenderTab("#tabQuestion_" + questionId)) {
                var that = this;
                require(['documentView'], function (DocumentView) {
                    var documentView = new DocumentView({
                        id: questionId,
                        el: '#' + that.model.href,
                        question: question,
                        callback: function () {
                            egov.callback(callback);
                        }
                    });
                    that.$el.append("<div id=" + that.model.href + " class='display tabdocument'>" + documentView.$el + "</div>");
                });
            }
        },

        hasRenderTab: function (selector) {
            /// <summary>
            /// selector check
            /// </summary>
            /// <param name="selector"></param>
            var $tab = $(selector);

            if (egov.mobile.isTablet && $tab.hasClass("display")) {
                $tab.closest(".dataDetail").addClass("display");
                return;
            }

            if (!egov.mobile.isTablet) {
                egov.commonFn.event.hideNavbar();
                $(".dataList").removeClass("display");
            }

            $(".dataDetail").addClass("display");

            this.$el.find(".display").removeClass("display");
            var hasBind = $tab.length > 0;
            if (hasBind) {
                $tab.siblings().hide();
                $tab.show().addClass("display");
                $tab.children(".document").addClass("display");
                egov.mobile.showDetailPage();
            }

            return hasBind;
        },

        removeDisplayingDocument_Mobile: function () {
            this.$el.children().remove();
        },

        //Xóa trên giao diện hiển thị văn bản hồ sơ
        removeDocument: function () {
            if (this.model) {
                var element = this.$el.find('#' + this.model.get('href'));
                if (element.length > 0) {
                    $(element).remove();
                }
            }
        },

        ///Đóng tab
        close: function () {
            var _this = this;
            setTimeout(function () {
                _this.$el.removeClass(_this.displayClass);
                //if (egov.mobile) {
                //    egov.mobile.hideDetailPage();
                //}
                _this.removeDocument();
            }, 400);
        },

        activeTab: function (tabIdx) {
            var tab = this.model.models[tabIdx]
            if (tab) {
                tab.view.active();
            }
        },

        getActiveTab: function () {
            var tabs = this.$el.children("li");
            for (var i = 0; i < tabs.length; i++) {
                if (tabs.eq(i).hasClass("active")) {
                    return tabs.eq(i);
                }
            }
        },

        ///<Summary>
        ///Mở văn bản ở notify
        ///</Summary>
        openDocumentInNotify: function () {
            var value = $.cookie("documentNotify");
            if (!value) {
                return;
            }

            var result = JSON.parse(value);
            var that = this;
            if (result) {
                this.openDocument(result.DocumentCopyId, result.Compendium, null, function () {
                    $.cookie('documentNotify', "", { expires: -1, domain: document.domain, path: "/", secure: true });
                });
            }
        },

        createDocInWaitingPacket: function (doc) {
            /// <summary>
            /// Lần lượt tạo mới những văn bản trong danh sách chờ khởi tạo theo lô
            /// </summary>
            /// <param name="doc">Văn bản được thêm file trong danh sách chờ xử lý theo lô, nếu không truyền sẽ tạo mới</param>
            var elem,
                that,
                firstDocContent,
                img,
                $img,
                $imgTemp,
                savedElem;

            that = this;

            //Không có danh sách chờ khởi tạo theo lô
            if (!egov || !egov.waitingPacket || egov.waitingPacket.length == 0) {
                return;
            }

            //Lấy file đầu tiên chưa xử lý
            elem = _.find(egov.waitingPacket, function (i) {
                return !i.opened
            });

            //Nếu tất cả đều xử lý rồi => return
            if (!elem) {
                savedElem = _.find(egov.waitingPacket, function (i) {
                    return i.saved;
                });

                if (!savedElem) {
                    egov.waitingPacket = undefined;
                    return;
                }
                that.openDocument(savedElem.documentCopyId, savedElem.title, true, function () {
                    savedElem.saved = false;
                });
                return;
            }

            //Set đã xử lý
            elem.opened = true;

            //Nếu không truyền document, tạo mới 1 văn bản
            if (!doc) {
                that.addDocument(elem.docTypeId, elem.title, undefined, true, elem.attachment);
                return;
            }

            //Thêm file vào văn bản hiện tại
            doc.attachments.model.add(elem.attachment);
            doc.attachmentIdInWaitingPacket = elem.attachment.get("Id");
            firstDocContent = doc.model.get("DocumentContents")[0];
            if (elem.attachment.get("Extension").toLowerCase() === '.pdf') {
                egov.request.createImagesFromBeginAndLastPdfPages({
                    data: { id: elem.attachment.get("Id") },
                    beforeSend: function () {
                        egov.pubsub.publish(egov.events.status.processing, "Đang xử lý...");
                    },
                    success: function (data) {
                        if (data) {
                            if (data.error) {
                                // egov.message.error(data.error);
                                egov.pubsub.publish(egov.events.status.error, data.error);
                            } else {
                                var images = data.images;
                                $imgTemp = "";
                                for (var i = 0; i < images.length; i++) {
                                    $img = '<div><p>' + '<img style="width:100%;" src="' + images[i] + '" />' + '</p></div>';
                                    $imgTemp = $imgTemp + $img;
                                }

                                if (data.documentInfo) {
                                    doc.model.set("Compendium", data.documentInfo.Compendium);
                                    doc.model.set("DocCode", data.documentInfo.DocCode);
                                    doc.model.set("Organization", data.documentInfo.Organization);
                                    doc.model.set("DatePublished", data.documentInfo.DatePublished);
                                    doc.reRenderInfo();
                                }

                                doc.model.get("DocumentContents")[0].Content = $imgTemp + firstDocContent.Content;
                                doc._renderForm();
                            }
                        }
                    },
                    error: function () {
                        // egov.message.error(_resource.errorDownload);
                        egov.pubsub.publish(egov.events.status.error, _resource.errorDownload);
                    },
                    complete: function () {
                        // egov.message.hide();
                        egov.pubsub.publish(egov.events.status.destroy);
                    }
                });
                return;
            }
        },

        closeTab: function (documentCopyIds) {
            ///<Summary>
            /// Đóng các tab văn bản theo id của văn bản
            ///<param name= 'documentCopyIds" type= "Array"> Danh sách id văn bản</param>
            ///</Summary>
            if ((!documentCopyIds
                || documentCopyIds.length <= 0)
                || this.model.length <= 1) {
                return;
            }

            var exist = [],
                leng = this.model.length,
                leng2 = documentCopyIds.length;
            for (var i = 0; i < leng; i++) {
                var item = this.model.at(i);
                if (item.get('isRoot')) {
                    continue;
                }

                for (var j = 0; j < leng2; j++) {
                    if (item.get("id") == documentCopyIds[j]) {
                        exist.push(item);
                        break;
                    }
                }
            }

            if (exist.length >= 1) {
                for (var i = 0; i < exist.length; i++) {
                    exist[i].view.close();
                }
            }
        },

        closeAll: function (success) {
            /// <summary>
            /// Đóng tất cả các tab
            /// </summary>
            var that = this;
            egov.message.show(egov.resources.tab.saveChange, egov.resources.common.alert, egov.message.messageButtons.YesNo, function () {
                var tabLength = that.model.length;
                var idx = 0;
                that.model.each(function (tab) {
                    idx++;
                    tab.view.close(false, true, null, function () {
                        if (idx === tabLength) {
                            egov.callback(success);
                        }
                    });
                });

            }, function () {
                egov.callback(success);
            });
        },

        hasChangeContent: function () {
            var result = false;
            this.model.each(function (tab) {
                if (!tab.view.document) return;
                tab.view.document.hasChange(function (hasChange) {
                    result = hasChange;
                    return;
                });
            });

            return result;
        }
    });

    /// <summary>Đối tượng View thể hiện 1 tab</summary>
    var TabView = Backbone.View.extend({
        // Dom tagname của tab => this.el = '<li></li>'
        tagName: 'li',

        // Class thể hiện trạng thái tab đang active
        activeClass: "active",

        container: '#tabContents',

        // Danh sách các sự kiện tác động lên tab
        events: {
            "click .close": "close3",    // đóng thẻ tab khi click chọn nút close
            "mousedown": "clickEvent",          // click events
            'click': "active"
        },

        // Hàm khởi tạo
        initialize: function (option) {
            this.tabs = option.tabs;
            this.documentInfo = option.model.get('documentInfo');
            this.copiedDocument = option.model.get('copiedDocument');
            if (option.model.get("isQuestionMode")) {
                this.question = option.model.get("question");
            }
            this.render();
            this.model.view = this;
            this.isLoadedContent = (this.model.get('isRoot') && this.model.get('name') === 'home') ? true : false;
            this.model.on('change:title', function (model, title) {
                this.$('a').text(title);
            }, this);

            return this;
        },

        // Render
        render: function () {
            this.$el.append($.tmpl(tabTemplate, this.model.toJSON()));

            if (this.model.get('hasTooltip')) {
                var title = splitTitle(this.model.get('title'));
                this.$el.attr('title', title);
                this.$el.addClass('qtooltip');
                this.$el.etip();
            }

            if ($('#' + this.model.get('href')).length > 0) {
                this.content = $('#' + this.model.get('href'));
            }
            else {
                this.content = $('<div>').addClass('tab-content full-height').attr('id', this.model.get('href'));
                $(this.container).append(this.content);
            }

            if (this.model.get('content') !== '' && this.model.get('content') !== undefined) {
                this.content.html(this.model.get('content'));
                this.isLoadedContent = true;
            }
        },

        // Active tab
        active: function (e) {
            if (e !== undefined && e.which == 2) {
                e.preventDefault();
                this.close();
            }

            if (!this.isActivated()) {
                this.$el.siblings().removeClass(this.activeClass);
                this.$el.addClass(this.activeClass);
                this.showContent();
                if (!this.model.get("isCreateDocument")) {
                    this.content.find("#wrapComment textarea").focus();
                }
            }
        },

        // Bind nội dung tab
        insertContent: function () {
            var type = this.model.get('type');
            //tab xử lý văn bản
            if (type === tabType.document) {
                this._insertContentDocument();
            }
                //Tab xử lý phần tìm kiếm
            else if (type === tabType.search) {
                this._insertContentSearch();
            }
                ///Tab xử lý phần báo cáo thống kê
            else if (type === tabType.report) {
                this._insertContentReport();
            }
                //Tab xử lý phân in ấn
            else if (type === tabType.print) { }

            else if (type === tabType.addImagePacket) {
                this._addImagePacket();
            }
        },

        _insertContentDocument: function () {
            var that = this;

            var isCreate = this.model.get('isCreateDocument');
            var isOnlineRegistration = this.model.get('isOnlineRegistration');
            var id = isCreate ? this.model.get('privateId') : this.model.get('id');
            var openDocumentCallback = function (DocumentView) {
                var documentView = new DocumentView({
                    id: id,
                    el: '#' + that.model.get('href'),
                    tab: that,
                    isCreate: isCreate,
                    question: that.question,
                    isOnlineRegistration: isOnlineRegistration,
                    documentInfo: that.documentInfo,
                    copiedDocument: that.copiedDocument,
                    callback: function () {
                        egov.callback(that.model.get('callback'));
                    }
                });

                that.document = documentView;
                that.isLoadedContent = true;
            };

            require(['documentView'], function (DocumentView) {
                openDocumentCallback(DocumentView);
            });
        },

        _insertContentReport: function () {
            var that = this;
            require(['reportViews'], function (ReportView) {
                var reportView = new ReportView({
                    el: '#' + that.model.get('href'),
                    tab: that
                });

                that.isLoadedContent = true;
            });
        },

        _insertContentSearch: function () {
            var that = this;
            egov.request.quickSearch({
                data: {
                    query: that.model.get("searchQuery"),
                    type: that.model.get("searchType"),
                    isUseCached: false
                },
                success: function (result) {
                    egov.pubsub.publish(egov.events.status.destroy);
                    if (result.TotalResult === 1) {
                        var document = result.Items[0];
                        that.$el.remove();
                        egov.views.home.tab.openDocument(document.DocumentCopyId, document.DocumentCompendium);
                        return;
                    }

                    require(['egovSearch'], function (EgovSearch) {
                        var egovSearch = new EgovSearch({
                            el: '#' + that.model.get('href'),
                            model: result,
                            searchQuery: that.model.get("searchQuery"),
                            searchType: that.model.get("searchType"),
                            isRelationDoc: false
                        });
                        that.isLoadedContent = true;
                    });
                },
                error: function (xhr) {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.search.error);
                }
            });
        },

        _addImagePacket: function () {
            var that = this;

            require(['insertImagePacket'], function (InsertImagePacket) {
                var insertImagePacket = new InsertImagePacket({
                    el: '#' + that.model.get('href'),
                    tab: that,
                });
                that.isLoadedContent = true;
            })
        },

        // Load lại nội dung tab
        reloadContent: function () {
            this.isLoadedContent = false;
            $('#' + this.model.get('href')).empty();
            this.showContent();
        },

        // Hiển thị content của tab
        showContent: function () {
            var that = this;
            this.content.siblings("div:visible").removeClass('active').hide();
            this.content.show();
            this.content.addClass('active');

            if (!this.isLoadedContent) {
                this.insertContent();
            }

            if (this.$el.is('[egovtab="print"]')) {
                this.reloadContent();
            }
        },

        // Xóa content của tab, dùng khi xóa tab
        removeContent: function () {
            var contentId = this.$el.find('a').attr('href');
            if ($(contentId).length > 0) {
                $(contentId).remove();
            }
        },

        // Trả về giá trị xác định trạng thái tab đang active hay không: true là active, false là không.
        isActivated: function () {
            return this.$el.hasClass(this.activeClass);
        },

        clickEvent: function (e) {
            if (!e) {
                return;
            }

            egov.helper.destroyClickEvent(e);
            if (e.which === 1) {
                this.active(e);
            } else {
                this.close2(true);
            }
        },

        display: function (isDisplay) {
            if (isDisplay) {
                this.$el.removeClass("hidden");
            } else {
                this.$el.addClass("hidden");
            }
        },

        errorTab: function () {
            this.$el.attr('title', egov.resources.document.transferError);
            this.$("a").css({
                "color": "red"
                //"text-decoration": "blink"
            });
        },

        // Close, remove tab
        close: function (isErrorDoc, noConfirm, useOldData, callback) {
            //Kiểm tra nếu có 1 tab hoặc là tab root thì không xóa
            var that = this;
            if (that.tabs.length === 1 || that.model.get('name') === 'home') {
                return;
            }

            if (isErrorDoc || !that.document) {
                that._remove(); return;
            }

            that.document.hasChange(function (hasChange) {
                if (!hasChange) {
                    that._remove(); return;
                }

                if (noConfirm) {
                    that.document.saveDocument(function () {
                        that._remove();
                        egov.callback(callback);
                        // egov.views.home.documents.reloadDocuments();
                    });
                    return;
                }

                egov.message.show(egov.resources.tab.saveChange, egov.resources.common.alert,
                    egov.message.messageButtons.YesNo, function () {
                        that.document.saveDocument(function () {
                            that._remove();
                            egov.callback(callback);
                        });
                    }, function () {
                        that._remove();
                        egov.callback(callback);
                    });

                return;
            });
        },

        close2: function (hasConfirm) {
            //Kiểm tra nếu có 1 tab hoặc là tab root thì không xóa
            var that = this;
            if (that.tabs.length === 1 || that.model.get('isRoot')) {
                return;
            }

            if (egov.mobile && egov.mobile.isTablet) {
                $(egov.views.home.tree.documentDetail).addClass('display');
                that.$el.append("<h3 class='detaildocumenterror'>" + egov.resources.document.notpermission + "</h3>");
                return;
            }

            if (!hasConfirm || !that.document) return that._remove();

            that.document.hasChange(function (hasChange) {
                if (!hasChange) {
                    return that._remove();
                }

                var title = that.document.isCreate ? egov.resources.tab.saveDraft : egov.resources.tab.saveChange;
                egov.message.show(title, egov.resources.common.alert, egov.message.messageButtons.YesNo,
                function () {
                    that.document.saveDocument(function () {
                        that._remove();
                    });
                }, function () {
                    that._remove();
                });

                return;
            });
        },

        close3: function () {
            this.close2(true)
        },

        setWidth: function (width) {
            this.$el.width(width);
        },

        _remove: function () {
            /// <summary>
            /// Xóa tab
            /// </summary>
            this.removeContent();
            this.$el.etip('destroy');
            this.$el.remove();
            if (userSettings.isSaveOpenTab) {
                if (egov.locache.hasSupportLocalStorage) {
                    egov.localStorage.deleteRecentTab(egov.setting.userName, this.model);
                } else {
                    egov.cookie.deleteRecentTab(egov.setting.userName, this.model);
                }
            }
            var that = this;
            if (this.document && this.document.isCreate && this.document.stores && this.document.stores.length > 0) {
                var codes = _.flatten(_.pluck(that.document.stores, "Codes"));
                if (codes.length > 0) {
                    egov.request.cancelCode({
                        data: {
                            inOutCodes: JSON.stringify(codes)
                        },
                        success: function () {
                        }
                    });
                }
            }
            this.tabs.remove(this.model);

            egov.views.home.tab.createDocInWaitingPacket();
            // egov.views.home.tab.addImagePacket();
        }
    });

    var titleLeng = 100;
    function splitTitle(title) {
        if (title.length <= titleLeng) {
            return title;
        }

        var char = title.charAt(titleLeng);
        var str = title.substring(0, titleLeng);
        if (char == " ") {
            return str;
        }

        var lastSpaceStr = titleLeng - str.lastIndexOf(" ");
        var str2 = title.substring(titleLeng, title.length);
        var firstSpaceStr2 = str2.indexOf(" ");

        if (firstSpaceStr2 < lastSpaceStr) {
            return title.substring(0, titleLeng + firstSpaceStr2) + '...';
        }
        else {
            return title.substring(0, titleLeng - lastSpaceStr) + '...';
        }
    };

    //#endregion

    return Tabs;
});