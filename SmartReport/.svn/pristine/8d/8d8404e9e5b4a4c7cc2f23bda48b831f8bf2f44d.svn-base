define([
    'contextMenuView',
    'documentContextmenu'
], function (ContextMenu, DocumentContextmenu) {
    "use strict";

    //#region Private Fields
    var regexUtcDate = /\d{4}-[0-1]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-6]\d$/;

    var storeColumnSettings =
    [
        { "ColumnName": "Compendium", "DisplayName": "Trích yếu", "SortName": "", "Width": "300", "EnableSort": true, "Order": 1 },
        { "ColumnName": "UserCurrentFullName", "DisplayName": "Người tạo", "SortName": null, "Width": "120", "EnableSort": false, "Order": 2 },
        { "ColumnName": "DateCreated", "DisplayName": "Ngày tạo", "SortName": "DateCreated", "Width": "80", "EnableSort": true, "Order": 3 },
        { "ColumnName": "DateReceived", "DisplayName": "Ngày nhận", "SortName": "DateReceived1", "Width": "80", "EnableSort": true, "Order": 4 },
        { "ColumnName": "DocCode", "DisplayName": "Số ký hiệu", "SortName": null, "Width": 100, "EnableSort": true, "Order": 5 },
        { "ColumnName": "InOutCode", "DisplayName": "Số đến đi", "SortName": null, "Width": 100, "EnableSort": true, "Order": 6 }
    ];


    var onlineRegistrationColumnSettings = [
        { "ColumnName": "STT", "DisplayName": getResource("egov.resources.document.table.stt"), "Width": "50", "EnableSort": false, "Order": 0 },
        { "ColumnName": "Compendium", "DisplayName": getResource("egov.resources.document.Compendium"), "SortName": "", "Width": "300", "EnableSort": true, "Order": 2 },
        { "ColumnName": "Email", "DisplayName": getResource("egov.resources.document.table.creater"), "SortName": null, "Width": 100, "EnableSort": true, "Order": 3 },
        { "ColumnName": "IdCard", "DisplayName": getResource("egov.resources.document.table.idCard"), "SortName": "", "Width": "100", "EnableSort": true, "Order": 4 },
        { "ColumnName": "DateReceivedFormat", "DisplayName": getResource("egov.resources.document.table.dateRecieved"), "SortName": "", "Width": "150", "EnableSort": true, "Order": 5 },
        { "ColumnName": "DocCode", "DisplayName": getResource("egov.resources.document.table.docCode"), "SortName": null, "Width": null, "EnableSort": true, "Order": 6 }
    ];

    var questionColumnSettings = [
        { "ColumnName": "STT", "DisplayName": getResource("egov.resources.document.table.stt"), "Width": "50", "EnableSort": false, "Order": 0 },
        { "ColumnName": "Date", "DisplayName": getResource("egov.resources.question.date"), "SortName": "", "Width": "150", "EnableSort": true, "Order": 2 },
        { "ColumnName": "Name", "DisplayName": getResource("egov.resources.question.name"), "SortName": "", "Width": null, "EnableSort": true, "Order": 3 },
        { "ColumnName": "AskPeople", "DisplayName": getResource("egov.resources.question.citizenname"), "SortName": null, "Width": 200, "EnableSort": true, "Order": 4 },
        { "ColumnName": "Compendium", "DisplayName": getResource("egov.resources.question.Compendium"), "SortName": null, "Width": 200, "EnableSort": true, "Order": 6 }
    ];

    var quickViewType = {
        egov: 1,
        egovOnline: 2
    }

    //Độ khẩn
    var urgents = egov.enum.urgents;

    //các kiểu sắp xếp
    var sortType = {
        asc: false,
        desc: true
    };


    var document;
    var _resource = egov.resources.documents;

    //#endregion

    //#region Document List View

    /// <summary>
    /// Đối tượng View thể hiện cho danh sách văn bản trong của một node trên cây văn bản
    /// </summary>
    var DocumentsView = Backbone.View.extend({
        tagName: egov.isMobile ? 'ul' : 'div',
        className: egov.isMobile ? 'mdl-list' : 'grid',

        // Div chứa danh sách document
        parent: $('#documentList'),

        //Giá trị mặc định xắp xếp theo khi chưa có cấu hình trên server
        defaultSorting: [{ columnName: 'DateReceived1', isDesc: sortType.desc, index: 0 }],

        lastSelected: null, //Hàng được chọn(model)

        isMultiSelecting: false,

        isQuestionTree: false,

        keyCode: {
            "enter": 13,
            "a": 65
        },

        documentListAdd: [], // chưa tạm danh sách văn bản khi tự động tải văn bản mới về  được thêm vào

        documentListRemove: [], // chưa tạm danh sách văn bản khi tự động tải văn bản mới về bị xóa

        tmpCallback: null,   ///Chứa tạm hàm callback khi tự động tải mới văn bản về

        events: {
            'click #checkAll': 'checkAll'
        },

        initialize: function (options) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="options">{node, tree}</param>
            var that = this,
                renderFunction;

            egov.views.home.documents = this;
            egov.setting.isScrollPaging = true; // Todo:Mặc định là scroll=> xem bỏ trong cấu hình đi
            // Gán danh sách văn bản cho node được chọn
            that.node = options.node;
            that.tree = options.tree;
            that.treeCallback = options.treeCallback;
            that.isStoreTree = options.isStoreTree;
            that.isQuestionTree = options.isQuestionTree;
            that.isOnlineRegistration = options.isOnlineRegistration;
            that.DocumentOnlineType = options.DocumentOnlineType
            that.hasExportFile = that.node.model.get('hasExportFile');
            that.isGeneralFAQTree = that.node.model.get("generalFAQTree") == true;
            that.docfieldIds = options.docfieldIds || null;
            that.storePrivate = egov.views.home.tree.storeTree;

            $(".is-group").each(function () {
                var $el = $(this);
                if ($el.parent().find("ul > li").length == 0) {
                    $el.hide();
                }
            })

            that.nodeId = that.isStoreTree ? that.node.model.get('storePrivateId')
                                            : that.node.model.get('functionId');

            if (!that.total) {
                that.total = parseInt(that.node.model.get('totalDocument'));
            }
            if (!that.unread) {
                that.unread = parseInt(that.node.model.get('totalDocumentUnread'));
            }

            that.data = {
                id: that.nodeId,
                paramsQuery: that.node.model.get('params')
            };

            ///lấy danh sách các cột sắp xếp mặc định
            if (that.node.model.get('defaultSort')) {
                that.defaultSorting = egov.toJSON(that.node.model.get('defaultSort'));
                that.defaultSorting = _.sortBy(that.defaultSorting, function (item) {
                    return item.index;
                });
            }
            that.sorting = that.defaultSorting;

            that._initData();

            that.bindView();

            if (that.isQuestionTree) {
                that.renderFunction = that._getQuestionList;
            }
            else if (that.isOnlineRegistration) {
                that.renderFunction = that._getDocumentOnlineRegistration;
            } else if (that.isStoreTree) {
                that.renderFunction = that._getDocumentStore;
            } else {
                that.renderFunction = that._getDocuments;
            }

            that._showProcessing();

            that.renderFunction(function (data) {
                if (that.isQuestionTree || that.isOnlineRegistration || that.isStoreTree) {
                    that.renderStore(); // Todo: cần đổi tên hàm
                    if (!that.isStoreTree) {
                        egov.callback(that.treeCallback, data);
                    }
                } else {
                    that.render();
                }

                that._hideProcessing();
            });

            return this;
        },

        render: function () {
            /// <summary>
            /// Page load
            /// </summary>
            if (egov.isMobile) {
                this._initForMobile();
                return;
            }

            this._initForDesktop();
        },

        _showProcessing: function () {
            if (egov.isMobile) {
                egov.mobile.showProcessBar();
                return;
            }
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
        },

        _hideProcessing: function () {
            if (egov.isMobile) {
                egov.mobile.hideProcessBar();
                return;
            }
            egov.pubsub.publish(egov.events.status.destroy);
        },

        _initData: function () {
            /// <summary>
            /// Khởi tạo dữ liệu mặc định cho danh sách văn bản
            /// </summary>

            this.$el.attr('id', "documentList" + this.nodeId);

            // Các tiêu chí lọc trên client
            this.isImportantFiltering = false;//Lọc theo văn bản quan trọng
            this.isUnreadFiltering = false;// Lọc theo văn bản chưa đọc
            this.dateFiltering = null;//Lọc theo khoảng thời gian
            this.overdueFiltering = null;//Lọc theo văn bản quá hạn
            this.doctypeFiltering = null;//Lọc theo loại văn bản
            this.allDataIsLoaded = false;//Check đã load hết dữ liệu từ server về
            this.pageCurrent = 1;
            this.sorting = this.defaultSorting;//Thiết lập để xắp xếp
            this.hasChanged = false;
        },

        _initForMobile: function () {
            /// <summary>
            /// Xử lý giao diện cho bản mobile
            /// </summary>
            var that = this;
            require(["tabView", "documentView", egov.template.document.mobileInfo]);
            that.$el.addClass('list-group list-document');

            that.renderDocumentsMobile(that.model, null, null, that.treeCallback);

            require(["toolbarView"], function (ToolbarView) {
                that.renderToolbar(ToolbarView);
            });
        },

        _initForDesktop: function () {
            /// <summary>
            /// Xử lý giao diện cho bản desktop
            /// </summary>
            var that = this;
            this.reloadColumnWidth();

            if (!this.columnSetting) {
                return;
            }

            require([egov.template.documentList.desktop], function (Template) {
                that.$el.html(Template);

                var command = function (isFirstSelected) {
                    that.renDocumentsHeader();
                    that.renderDocumentCommon(isFirstSelected);
                    that.enableResize();
                    that.layout();
                }

                if (that.isFirstload) {
                    command(true);
                    egov.callback(that.treeCallback);
                } else {
                    command(false);
                    that.loadNewerDocuments(that.treeCallback);
                }

                that.isFirstload = false;

                that._bindEventScroll();

                that.appendBodyEventKey();
            });
        },

        renderStore: function () {
            /// <summary>
            /// Xử lý giao diện cho bản desktop
            /// </summary>
            var that = this;
            if (egov.isMobile) {
                this.renderStoreMobile();
                return;
            }
            this.reloadColumnWidth();

            if (!this.columnSetting) {
                return;
            }

            require([egov.template.documentList.desktop], function (Template) {
                that.$el.html(Template);

                that.renDocumentsHeader();
                that.renderDocumentCommon(true);
                that.enableResize();
                that.layout();
                //  egov.callback(that.treeCallback);

                that._bindEventScroll();

                that.appendBodyEventKey();
            });
        },

        renderStoreMobile: function () {
            var that = this;
            if (this.isQuestionTree) {
                require(['questionMobile'], function (QuestionMobile) {
                    var questions = new QuestionMobile({
                        parent: that,
                        model: new egov.models.questionList(that.model.models)
                    })

                    that.$el.html(questions.$el);
                })

            }
        },

        bindView: function () {
            /// <summary>
            /// Hiển thị giao diện danh sách văn bản theo node
            /// </summary>

            // Ẩn các contextmenu
            this.hideAllContextmenu();

            // Ẩn các danh sách văn bản của các node khác.
            if (egov.isMobile) {
                this.bindViewMobile();
            }
            else {
                this.parent.find(".grid").hide();

                if (this.parent.has(this.$el).length > 0) {
                    this.$el.show();
                } else {
                    this.parent.append(this.$el);
                }
            }
        },

        bindViewMobile: function () {
            var tree = egov.views.home.tree;
            tree.$documentListId.children().hide();
            $(tree.nameGroupDocument).text(this.node.model.get('name'));
            this.parent.find(".list-document").not(this.$el).removeClass('display');
            if (this.parent.has(this.$el).length > 0) {
                this.$el.show();
            } else {
                this.parent.append(this.$el);
            }
        },

        layout: function () {
            /// <summary>
            /// Layout danh sách văn bản: chia header, grid, pager
            /// </summary>

            egov.views.home.documentLayout = this.$el.layout({
                resizable: true,
                closable: false,
                spacing_closed: 0,
                spacing_open: 0,
                north__spacing_open: 0,
                north__size: 32,
                north__paneSelector: '.grid-header',
                center__paneSelector: '.grid-content'
            });
        },

        renderDocuments: function (documents, hasEmpty, isFirstSelected, isAppendTop, removeDocIds, isAuto) {
            /// <summary>
            /// Render ra danh sách document (krenderQuickViewo có header)
            /// </summary>
            /// <param name="documents">Danh sách văn bản</param>
            /// <param name="hasEmpty">Giá trị xác định có clear danh sách đang có hay không</param>
            /// <param name="isFirstSelected">Có seleted hàng đầu tiên hay không</param>
            /// <param name="isAppendTop">True: Thêm view vào đầu danh sách; False: thêm vào cuối danh sách</param>
            /// <param name="removeDocIds">Danh sách id văn bản bị xóa</param>
            /// <param name="removeDocIds">Danh sách id văn bản bị xóa</param>
            var documentListElement = this.$('tbody'),
                  that = this,
                  idx = 0;
            hasEmpty = hasEmpty || false;

            if (hasEmpty) {
                this.removeAllSelected();
                documentListElement.empty();
            }

            this._hideDocuments(removeDocIds);

            if (!isAuto && (!documents || documents.length <= 0)) {
                var colspan = this.columnSetting.length;
                documentListElement
                    .empty()
                    .append("<tr><td colspan=" + colspan + ">" + egov.resources.documents.noDocumentCopyItem + "</td></tr>");

                return;
            }

            // Lấy ra danh sách các cột theo cấu hình (trừ những cột được thêm mặc định như Stt, Màu)
            var newcolumnSettings = _.filter(this.columnSetting, function (item) {
                return (item.ColumnName != "STT") && (item.ColumnName != "Checkmask") && (item.ColumnName != "Color");
            });

            //Hiển thị lên giao diện

            documents.forEach(function (document) {
                var documentItm = new DocumentItem({
                    parent: that,
                    model: document,
                    config: newcolumnSettings
                });

                if (idx === 0 && isFirstSelected) {
                    documentItm.selected();
                }

                if (isAppendTop) {
                    documentListElement.prepend(documentItm.$el);
                } else {
                    documentListElement.append(documentItm.$el);
                }
                idx = idx + 1;
            });

            this._hideProcessing();
        },

        _hideDocuments: function (removeDocIds) {
            ///<summary>
            /// Ẩn các văn bản trên giao diện đi
            ///</summary>
            if (removeDocIds && removeDocIds.length > 0) {
                var that = this;
                _.each(removeDocIds, function (docId) {
                    var _findEl = "tr#documentCopy" + docId;
                    that.$el.find('tbody').find(_findEl).fadeOut(600).remove();
                });
            }
        },

        renderDocumentsMobile: function (documents, hasReloadItem, indexRow, callback) {
            /// <summary>
            /// Render ra danh sách document cho Mobile
            /// </summary>
            /// <param name="documents">Danh sách văn bản</param>
            var that = this;
            // egov.mobile.hideNotify();
            this.removeAllSelected();

            var documentListElement = this.$el;
            egov.dataManager.getAllUsers({
                success: function (allUsers) {
                    documentListElement.empty();
                    if (!documents || documents.length <= 0) {
                        that.$el.append("<li class='two-line no-data'>" + egov.resources.documents.noDocumentCopyItem + "</li>");
                        return;
                    }

                    var selected, indexOfSelectedRow = indexRow || 0;

                    if (indexOfSelectedRow >= documents.length) {
                        indexOfSelectedRow = documents.length - 1;
                    }
                    else if (indexOfSelectedRow < 0) {
                        indexOfSelectedRow = 0;
                    }

                    that.allUsers = allUsers;
                    egov.currentUser = _.find(allUsers, function (user) {
                        return user.value === egov.userId;
                    });

                    documents.each(function (document, idx) {
                        var documentItm = new DocumentItem({
                            parent: that,
                            model: document
                        });

                        if (indexOfSelectedRow == idx) {
                            selected = documentItm
                        }

                        documentListElement.append(documentItm.$el);
                    });

                    selected.selected();

                    if (egov.mobile.isTablet) {
                        //var title = selected.model.get("Compendium");
                        //var documentCopyId = selected.model.get("DocumentCopyId");
                        //require(['tabView'], function (TabView) {
                        //    var tabDocument = new TabView({
                        //        model: {
                        //            id: documentCopyId,
                        //            name: title,
                        //            title: title,
                        //            href: 'tabDocument_' + documentCopyId
                        //        }
                        //    });
                        //});
                    }

                    egov.callback(callback);

                    // egov.materialReset();
                }
            });
        },

        renderSearchMobileDocuments: function (documents) {
            /// <summary>
            /// Render ra danh sách document tìm kiếm
            /// </summary>
            /// <param name="documents">Danh sách văn bản</param>
            var documentListElement,
                that = this,
                searchList = '#searchListDocument';

            $(searchList).remove();
            $("#documentList").append("<div id='searchListDocument' class='grid list-group list-document'></ul>");
            $(searchList).append("<div class='list-group-item'><b id='totalSearchResult'>" +
                egov.resources.searching.result + " (<span style='color: blue'>" +
                documents.length
                + "</span>)</b></li>");

            documentListElement = $(searchList);
            documents.each(function (document, idx) {
                var documentItm = new DocumentItem({
                    parent: that,
                    model: document
                });
                documentListElement.append(documentItm.$el);

                if (idx === 0) {
                    documentItm.selected();
                }
            });
        },

        mobileSearch: function (keySearch, isSearchAll) {
            /// <summary>
            /// Tìm kiếm trên Mobile
            /// </summary>
            /// <param name="keySearch">Giá trị muốn tìm kiếm</param>

            if (typeof this.searching !== 'undefined') {
                window.clearTimeout(this.searching);
            }

            var that = this;
            this.searching = window.setTimeout(function () {
                that.mobileSearchFilter(keySearch, isSearchAll);

                that._hideProcessing();
            }, 300);
        },

        mobileSearchFilter: function (keySearch, isGetAll) {
            /// <summary>
            ///
            /// </summary>
            /// <param name="keySearch"></param>
            var that = this;
            isGetAll = isGetAll == true;
            if (isGetAll) {
                var documents = [];
                this._showProcessing();

                $("#documentList .list-document").hide();
                // Lọc theo các tiêu chí
                egov.request.quickSearch({
                    data: { query: keySearch, isGetAll: true },
                    success: function (result) {
                        var items = result.Items;
                        for (var i = 0; i < items.length; i++) {
                            var item = {
                                Address: items[i].ExtendInfo.Address,
                                Compendium: items[i].DocumentCompendium,
                                DateCreated: items[i].ExtendInfo.DateCreate,
                                DateReceived: items[i].DateReceived,
                                DocCode: items[i].ExtendInfo.DocCode,
                                DocumentCopyId: items[i].DocumentCopyId,
                                DocumentId: items[i].DocumentId,
                                UserCurrentFullName: items[i].ExtendInfo.CurrentUsername,
                                Status: items[i].ExtendInfo.Status,
                                IsViewed: items[i].IsViewed,
                            };
                            documents.push(item);
                        }
                        //Cần thêm hiển thị tổng kết quả tìm kiếm
                        var model = new egov.models.documentList(documents);
                        that.renderSearchMobileDocuments(model);

                        that._hideProcessing();
                    }
                });
            }
            else {
                keySearch = egov.utilities.string.stripVietnameseChars(keySearch.toLowerCase());
                this.$el.children().hide();
                var compendiumContains = this.$el.find(".compendium").filter(function () {
                    return this.textContent.contains(keySearch);
                });
                if (compendiumContains.length > 0) {
                    _.each(compendiumContains, function (compendium) {
                        $(compendium).closest(".list-group-item").show();
                    })
                }
            }
        },

        renderDocumentCommon: function (isFirstSelected) {
            /// <summary>
            /// Bind danh sách văn bản mặc định
            /// </summary>

            this.isClientFilter = false;

            if (!this.model || this.model.length === 0) {
                var colspan = this.columnSetting.length;
                this.$el.find('tbody').empty()
                    .append("<tr><td colspan=" + colspan + ">" + egov.resources.documents.noDocumentCopyItem + "</td></tr>");

                this._noDocumentSelectedQuickview();

                egov.pubsub.publish(egov.events.status.destroy);

                return;
            }

            this.renderDocuments(this.model, false, isFirstSelected);
        },

        renderClientFilter: function (searchTerm) {
            /// <summary>
            /// Lọc danh sách văn bản dưới client
            /// </summary>
            /// <param name="value"></param>
            var that = this,
               leng = this.leng;
            // Lọc theo các tiêu chí

            var fnEmpty = function () {
                var colspan = that.columnSetting.length;
                that.$el.find('tbody').empty()
                    .append("<tr><td colspan=" + colspan + ">" + egov.resources.documents.noDocumentCopyItem + "</td></tr>");
                that._noDocumentSelectedQuickview();

                that._hideProcessing();
            };

            if (!this.model || this.model.length <= 0) {
                fnEmpty();
                return;
            }

            //Sắp xếp lại theo bộ lọc và tìm kiếm giá trị truyền vào
            this._sortBy();

            var documents = this.model.select(function (doc) {
                var result = true;

                if (searchTerm && searchTerm != '') {
                    var textToSearch = "";

                    if (doc.get('Compendium2')) {
                        textToSearch = doc.get('Compendium2');
                    }

                    if (doc.get('DocCode')) {
                        textToSearch += " " + doc.get('DocCode');
                    }

                    if (doc.get("InOutCode")) {
                        textToSearch += " " + doc.get('InOutCode');
                    }

                    if (doc.get("UserCurrentFullName")) {
                        textToSearch += " " + doc.get('UserCurrentFullName');
                    }

                    if (doc.get("UserCurrentFirstName")) {
                        textToSearch += " " + doc.get('UserCurrentFirstName');
                    }

                    result &= search(egov.utilities.string.stripVietnameseChars(textToSearch), searchTerm);
                }

                return result;
            });

            if (documents.length === 0) {
                fnEmpty();
                return;
            }

            this.isClientFilter = true;
            this.filterTerm = searchTerm;

            this.renderDocuments(new egov.models.documentList(documents), true, true);
            that._hideProcessing();
        },

        loadNewerDocuments: function (callback, isAuto) {
            /// <summary>
            /// Load danh sách các Document mới và các document đã bị remove của node hiện tại.
            /// </summary>
            var that = this;
            $('#toolbar-important, #toolbar-unread').removeClass('active');

            this.isImportantFiltering = false;
            this.isUnreadFiltering = false;
            this.pageCurrent = 1;
            (this.isClientFilter && this.filterTerm != '') ? this.renderClientFilter(this.filterTerm) : this._getLastestDocumentsInTree(callback, isAuto);
        },

        loadNewerDocumentsStore: function (callback) {
            /// <summary>
            /// Load danh sách các Document mới và các document đã bị remove của node hiện tại.
            /// </summary>
            var that = this;

            if (egov.isMobile) {
                if (this.model.models.length > 0) {
                    this.$el.children().first().tap()
                }
                return;
            }

            $('#toolbar-important, #toolbar-unread').removeClass('active');

            this.isImportantFiltering = false;
            this.isUnreadFiltering = false;
            this.pageCurrent = 1;

            //Load danh sách văn bản, file đính kèm bên sổ hồ sơ
            this._getDocumentStore(function () {
                that.renderDocuments(that.model, true, true);
            });
        },

        _getLastestDocumentsInTree: function (callback, isAuto) {
            ///<summary>
            ///Load những văn bản mới nhất trên cây văn bản
            ///</summary>
            var that = this;

            this._reloadDocumentTree();

            this._getLastestDocuments(function (hasAuto, addModels, removeDocIds, isAppendToTop, isFirstSelected, hasAutoGetNews) {
                that.hasChanged = false;
                var hasEmptyTable;
                if (hasAuto) {
                    hasEmptyTable = false;
                    that.renderDocuments(addModels, hasEmptyTable, isFirstSelected, isAppendToTop, removeDocIds, hasAuto);
                } else {
                    hasEmptyTable = true;
                    that.renderDocuments(that.model, hasEmptyTable, isFirstSelected);
                }
                egov.callback(callback, hasAutoGetNews);

                that._hideProcessing();
            }, isAuto);
        },

        _reloadDocumentTree: function () {
            egov.pubsub.publish(egov.events.tree.reload, this.node.parentNode ? this.node.parentNode.model.id : this.node.model.id);
        },

        getTotalDocuments: function () {
            /// <summary>
            /// Trả lại số lượng của documents
            /// </summary>
            return this.model.length || 0;
        },

        enableResize: function () {
            /// <summary>
            /// Resize các cột
            /// </summary>
            var that = this;
            this.$el.find('#tblListDocument').grid({
                isUsingCustomScroll: false,
                isResizeColumn: true,
                isFixHeightContent: true,
                isAddHoverRow: false,
                isUseCookie: true,
                isRenderPanelGrid: false,
                onresizefinish: function () {
                    var data = {};
                    var columnNoWidth = [];
                    var totalWidth = 0;
                    var tableHeader = that.$el.find('.grid-header th');
                    var tableHeaderCol = that.$el.find('.grid-header colgroup col');
                    var tableContent = that.$el.find('.grid-content table');

                    tableHeaderCol.each(function (idx, item) {
                        var column = tableHeader.eq(idx);
                        if ($(item).width() == 0) {
                            columnNoWidth.push(column.attr('data-column'));
                        } else {
                            totalWidth += $(item).width();
                        }
                        data[column.attr('data-column')] = $(item).width();
                    });

                    //Chiều rông mới của danhh sách văn bản
                    var newTableContentWidth = tableContent.width();

                    if (columnNoWidth.length > 0) {
                        var allWidth = newTableContentWidth - totalWidth;
                        var widthCol = allWidth / columnNoWidth.length;
                        for (var g = 0; g < columnNoWidth.length; g++) {
                            data[columnNoWidth[g]] = widthCol;
                        }
                    }

                    if (egov.locache.hasSupportLocalStorage) {
                        egov.localStorage.addDocumentHeaderWidth(that.nodeId, data);
                    } else {
                        egov.cookie.addDocumentHeaderWidth(that.nodeId, data);
                    }
                }
            });

            $('.document-preview').bind('mousedown, mousemove', function () {
                var value = that.$el.find('.grid-content').scrollLeft();
                that.$el.find('.grid-header-wrap').css({ "margin-left": -value });
            });
        },

        renderToolbar: function (ToolbarView) {
            /// <summary>
            /// Hiển thị toolbar của danh sách văn bản
            /// </summary>

            var documentsToolbar = new egov.models.toolbarList;
            if (egov.isMobile) {
                $('.documentlist-filter > li:not(.search, .user-option, .bell)').remove();
            } else {
                var that = this;
                ///Lọc văn bản chưa đọc
                documentsToolbar.add({
                    id: 'toolbar-unread',
                    text: _resource.toolbar.controlName.documentUnread,
                    className: egov.isMobile ? 'unread' : 'pull-right',
                    showSelected: true,
                    icon: 'icon-mail5',
                    isImageButton: true,
                    callback: function (selected) {
                        that.isUnreadFiltering = selected.$el.hasClass('active');
                        that.renderClientFilter();
                    }
                });

                ///Lọc văn bản quan trọng
                documentsToolbar.add({
                    id: 'toolbar-important',
                    text: _resource.toolbar.controlName.documentImportant,
                    className: egov.isMobile ? 'important' : 'pull-right',
                    showSelected: true,
                    icon: 'icon-flag',
                    isImageButton: true,
                    callback: function (selected) {
                        that.isImportantFiltering = selected.$el.hasClass('active');
                        that.renderClientFilter();
                    }
                });

                ///Refresh lại danh dách văn bản
                documentsToolbar.add({
                    text: _resource.toolbar.controlName.refresh,
                    className: egov.isMobile ? 'refresh ' : 'pull-right',
                    showSelected: true,
                    icon: egov.isMobile ? 'icon-cw' : 'icon-cw',
                    isImageButton: true,
                    callback: function (selected) {
                        that._showProcessing();

                        that.isImportantFiltering = false;
                        that.isUnreadFiltering = false;
                        that.pageCurrent = 1;
                        that.sorting = that.defaultSorting;

                        that._getLastestDocuments(function () {
                            that.renderClientFilter();
                            selected.$el.removeClass('active');
                            $('#toolbar-important,#toolbar-unread').removeClass('active');
                            that._hideProcessing();
                        });
                    }
                });

                // Cài đặt xem trước
                documentsToolbar.add({
                    id: 'toolbar-quickview',
                    text: _resource.toolbar.controlName.preview,
                    className: egov.isMobile ? 'xs-hidden' : 'pull-right',
                    icon: 'icon-info4',
                    isDropdownMenu: false,
                    selected: egov.enum.quickViewType.hide !== egov.setting.userSetting.quickView,
                    isImageButton: true,
                    callback: function (selected) {
                        var result = selected.$el.hasClass('active');
                        if (!result) {
                            egov.views.home.showPreview(egov.enum.quickViewType.hide);
                        } else {
                            var quickView = egov.locache.hasSupportLocalStorage
                                ? egov.localStorage.getQuickView()
                                : egov.cookie.getQuickView();

                            ///Nếu không tồn tại thì mặc định set cho hiển thị bên dưới
                            if (!quickView) {
                                quickView = egov.enum.quickViewType.below;
                                egov.views.home.showPreview(quickView);
                            } else {
                                quickView = parseInt(quickView);
                                if (isNaN(quickView) || egov.enum.quickViewType.hide === quickView) {
                                    quickView = egov.enum.quickViewType.below;
                                }
                                egov.views.home.showPreview(quickView);
                            }

                            that._reQuickView();
                        }

                        ///Lưu thiết lập về server
                        egov.request.setUserConfig({
                            data: {
                                QuickView: egov.setting.userSetting.quickView
                            }
                        });
                    }
                });

                // Cài đặt xem danh sách
                documentsToolbar.add({
                    text: _resource.toolbar.controlName.setting,
                    className: egov.isMobile ? 'xs-hidden' : 'pull-right',
                    icon: 'icon-cog',
                    isDropdownMenu: true,
                    isImageButton: true,
                    data: [
                        { text: 'Xem cỡ nhỏ', value: 0 },
                        { text: 'Xem cỡ vừa', value: 1 },
                        { text: 'Xem cỡ lớn', value: 2 },
                        { text: '---' },
                        { text: 'Xem trước bên phải', value: 4 },
                        { text: 'Xem trước bên dưới', value: 5 }
                    ],
                    callback: function (option, selected) {
                        if (option.value === egov.enum.fontSizeType.nho
                            || option.value === egov.enum.fontSizeType.vua
                            || option.value === egov.enum.fontSizeType.lon) {
                            selected.parents('.dropdown-menu').find('li[value="0"], li[value="1"], li[value="2"]').parent().removeClass('active');
                            egov.views.home.changeSize(option.value);

                            ///Lưu thiết lập về server
                            egov.request.setUserConfig({
                                data: {
                                    FontSize: option.value
                                }
                            });
                        } else if (option.value === 4 || option.value === 5) {
                            selected.parents('.dropdown-menu').find('li [value="4"], li [value="5"]').parent().removeClass('active');
                            var quickview = option.value === 4 ? egov.enum.quickViewType.right : egov.enum.quickViewType.below;
                            egov.views.home.showPreview(quickview);

                            that._reQuickView();
                            ///Lưu thiết lập về server
                            egov.request.setUserConfig({
                                data: {
                                    QuickView: quickview
                                },
                                success: function () {
                                    if (egov.locache.hasSupportLocalStorage) {
                                        egov.localStorage.setQuickView(quickview);
                                    } else {
                                        egov.cookie.setQuickView(quickview);
                                    }
                                }
                            });
                        }

                        selected.parent().addClass('active');
                        $('#toolbar-quickview').addClass('active');
                    }
                });

                if (this.isDoctypeFilter) {
                    var that = this;
                    if (that.model.length > 0) {
                        var checkDoctypeNameColumn = that.model.at(0).has('DocTypeName');
                        if (checkDoctypeNameColumn) {
                            documentsToolbar.add({
                                text: _resource.toolbar.controlName.docTypeName,
                                className: 'pull-left',
                                callback: function (option) {
                                    that.doctypeFiltering = option.value;
                                    that.renderClientFilter();
                                },
                                isDropdownMenu: true,
                                showSelected: true,
                                isImageButton: false,
                                data: function () {
                                    var doctypes = _.uniq(that.model.models, function (doc) {
                                        return doc.get('DocTypeId');
                                    });
                                    var result = [{
                                        text: _resource.toolbar.controlName.all, value: 0, selected: true
                                    }];
                                    doctypes.forEach(function (itm) {
                                        result.push({
                                            text: itm.get('DocTypeName'), value: itm.get('DocTypeId')
                                        })
                                    });

                                    return result;
                                }
                            });
                        }
                    }
                }

                if (this.isOverdueFilter) {
                    var textDay = _resource.toolbar.controlName.day;
                    documentsToolbar.add({
                        text: _resource.toolbar.controlName.dateAppointed,
                        className: 'pull-left',
                        callback: function (option) {
                            if (option.value !== 0) {
                                that._showProcessing();

                                egov.request.getDateAppointed({
                                    data: { days: option.value },
                                    success: function (result) {
                                        that.overdueFiltering = new Date(result.year, result.month - 1, result.day, 23, 59, 59); // -1 do kiểu date trong javascript tính tháng từ 0
                                        that.renderClientFilter();
                                    },
                                    complete: function () {
                                        that._hideProcessing();
                                    }
                                });
                            }
                            else {
                                that.overdueFiltering = null;
                                that.renderClientFilter();
                            }
                        },
                        isDropdownMenu: true,
                        isImageButton: false,
                        showSelected: true,
                        data: [
                            { text: _resource.toolbar.controlName.all, value: 0, selected: true },
                            { text: '1 ' + textDay, value: 1 },
                            { text: '2 ' + textDay, value: 2 },
                            { text: '3 ' + textDay, value: 3 },
                            { text: '4 ' + textDay, value: 4 },
                            { text: '5 ' + textDay, value: 5 },
                            { text: '6 ' + textDay, value: 6 },
                            { text: '7 ' + textDay, value: 7 },
                            { text: '10 ' + textDay, value: 8 },
                            { text: '15 ' + textDay, value: 15 },
                            { text: '20 ' + textDay, value: 20 },
                            { text: '30 ' + textDay, value: 30 },
                        ]
                    });
                }

                $('.documentlist-filter li:not(#quickSearch)').remove();
            }
            this.toolbar = new ToolbarView({
                el: '.documentlist-filter',
                model: documentsToolbar,
                documents: this
            });
        },

        renDocumentsHeader: function () {
            /// <summary>
            /// Hiển thị header cho danh sách văn bản
            /// </summary>

            renderGridHeader(this.columnSetting, this.$el);

            // Cột sắp xếp mặc định trong cấu hình
            // Todo: cần config cái này trong cấu hình
            var data = [], that = this;
            _.each(this.columnSetting, function (itm) {
                data.push({
                    text: itm.DisplayName,
                    value: (itm.SortName == undefined || itm.SortName == "") ? itm.ColumnName : itm.SortName,
                    isDesc: false,
                    enableSort: itm.EnableSort,
                    callback: function (option) {
                        if (!option.enableSort) {
                            return;
                        }

                        that.sorting = [{ columnName: option.value, isDesc: option.isDesc, index: 0 }];
                        that.renderClientFilter();

                        that._hideProcessing();
                    }
                })
            });

            this.gridHeader = new GridHeader({ model: new gridHeaderCollection(data), parent: that });
            this.$el.find('table thead').empty().append(this.gridHeader.$el);
        },

        setAllSelected: function () {
            /// <summary>
            /// Set selected tất cả các row của danh sách.
            /// </summary>

            if (this.model && this.model.length > 0) {
                this.model.each(function (doc) {
                    if (doc.get('Selected') == false) {
                        doc.set('Selected', true);
                    }
                });
            }
        },

        removeAllSelected: function () {
            /// <summary>
            /// Bỏ selected tất cả các row của danh sách.
            /// </summary>
            if (this.model && this.model.length > 0) {
                this.model.each(function (doc) {
                    if (doc.get('Selected') == true) {
                        doc.set('Selected', false);
                    }
                });
            }
        },

        reloadColumnWidth: function () {
            /// <summary>
            /// Load lại width đã dc lưu cache
            /// </summary>
            var columnCache = egov.locache.hasSupportLocalStorage
                ? egov.localStorage.getDocumentHeaderWidth(this.nodeId)
                : egov.cookie.getDocumentHeaderWidth(this.nodeId);

            if (columnCache && columnCache.length > 0) {
                var that = this;
                $.each(columnCache, function (key, value) {
                    var column = _.find(that.columnSetting, function (item) {
                        return item.ColumnName == key;
                    });

                    if (column) {
                        column.Width = value;
                    }
                });
            }
        },

        rebindUnreadForNode: function () {
            /// <summary>
            /// Đánh lại số văn bản chưa đọc của cây văn bản
            /// </summary>
            this.tree.reloadUnread(this.node, this.unread ? this.unread : 0, this.model ? this.model.length : 0);
        },

        clientQuickSearch: function (value) {
            ///<Summary>Tìm kiếm trên danh sách văn bản được cache lại dưới client</Summary>
            ///<param name="value" type="string">Giá trị muốn tìm kiếm</param>
            if (typeof this.searching !== 'undefined') {
                window.clearTimeout(this.searching);
            }

            if (value) {
                value = egov.utilities.string.stripVietnameseChars(value.toLowerCase());
            }

            var that = this;
            this.searching = window.setTimeout(function () {
                that.$el.find('tbody').empty();

                that.renderClientFilter(value);

                that._hideProcessing();
            }, 300);
        },

        contextmenu: function (e, target) {
            /// <summary>
            ///  Contextmenu vào văn vản khi selected
            /// </summary>

            this.hideAllContextmenu();
            var selectedModel = this.model.select(function (doc) {
                return doc.get('Selected') == true;
            });

            if (!selectedModel || selectedModel.length <= 0) {
                egov.pubsub.publish(egov.events.status.error, "Không lấy được contextmenu.");
                return;
            }

            var selectDoctypeIds = [],
                files = [];       //Check xem danh sách được chọn có phải là file hay không

            var selectedIds = _.map(selectedModel, function (m) {
                selectDoctypeIds.push(m.get('DocTypeId'));

                if (m.get('IsFile') == 1) {
                    files.push(m);
                }

                return m.get('DocumentCopyId');
            });

            //kiểm tra xem danh sách được chọn có item nào là file hay không
            if (files.length > 0) {
                //Nếu có tồn tại file thì check danh số lượng file với số lượng đucợ chọn
                if (selectedModel.length > files.length) {
                    egov.pubsub.publish(egov.events.status.error, "Danh sách chọn không cùng loai. Vui lòng xem lại.");
                    return;
                }

                this._contextmenuInFile(e, target, selectedIds);
                return;
            }

            this._contextmenuInDoc(e, target, selectedIds, selectedModel, selectDoctypeIds);
        },

        _contextmenuInDoc: function (e, target, selectedIds, selectedModel, selectDoctypeIds) {
            var that = this,
                loading,
                fnError,
                hasGetActionTheoLo,
                contextData,
                arrPermission,
                docpermiss,
                documentContextmenu,
                theoLoActions,
                docTypeId,
                documents,
                actionSpecials
            this.context = target.$el.contextmenu({
                isShowLoading: true,
                style: {
                    'min-width': '150px',
                    'width': 'auto'
                },
                position: {
                    my: 'left top',
                    of: e
                },
                trigger: 'right'
            });

            loading = $('<img class="loading" src="../Content/Images/ajax-loader.gif" width="20" height="20" style="position:fixed; z-index:100; top:' + (e.clientY - 10) + 'px; left: ' + (e.clientX - 10) + 'px;" />');
            loading.css({ 'position': 'absolute' }).appendTo('body');

            fnError = function () {
                loading.remove();
                that.hideAllContextmenu();
            };

            var hasGetActionTheoLo = this._isChoXuLyOrDuThao(selectedIds);

            egov.dataManager.getDocumentPermission(
                selectedIds,
                {
                    success: function (data) {
                        if (data.error) {
                            egov.pubsub.publish(egov.events.status.error, data.error);
                            fnError();
                            return;
                        }
                        contextData = new egov.models.contextMenuList;

                        arrPermission = _.uniq(data);
                        docpermiss = new egov.models.documentPermissionList;

                        arrPermission.forEach(function (item) {
                            docpermiss.add({
                                value: item
                            });
                        });

                        if (that.isStoreTree) {
                            docpermiss.add({
                                value: egov.enum.permission.xoavanbankhoihoso // thêm quyền xóa văn bản khỏi hồ sơ
                            });
                        }

                        documentContextmenu = new DocumentContextmenu({ collection: docpermiss, parent: that });
                        documentContextmenu.getContextmenu(selectedIds, selectedModel, target, function (items) {

                            var command = function (items, theoLoActions) {
                                if (items.length <= 0 && theoLoActions.length <= 0) {
                                    fnError();
                                    return;
                                }

                                if (items.length > 0) {
                                    items.forEach(function (item) {
                                        if (item === '---' || item == null || typeof item !== 'object') {
                                            contextData.add({
                                                name: '---',
                                            });
                                        } else {
                                            contextData.add({
                                                key: item.commandName,
                                                text: item.name,
                                                callback: item.callback,
                                                iconClass: item.iconClass
                                            });
                                        }
                                    });
                                }
                                // Quản lý nhiệm vụ
                                //contextData.add({
                                //    key: "bmm",
                                //    text: "Tạo nhiệm vụ",
                                //    iconClass: "icon-users3",
                                //    callback: function () {
                                //        that.showBmm();
                                //    }
                                //});

                                //Check danh sách action theo lô
                                if (theoLoActions.length > 0) {
                                    if (items.length > 0) {
                                        contextData.add({
                                            name: '---',
                                        });
                                    }

                                    docTypeId = selectDoctypeIds[0];
                                    // documents = [];
                                    actionSpecials = egov.enum.actionSpecial;
                                    var categoryIds = [];

                                    for (var i = 0; i < selectedModel.length; i++) {
                                        var modelJson = selectedModel[i].toJSON();
                                        if (modelJson.CategoryId) {
                                            categoryIds.push(modelJson.CategoryId);
                                        }
                                    }

                                    if (categoryIds.length > 0) {
                                        categoryIds = _.uniq(categoryIds);
                                    }

                                    contextData.add({
                                        key: actionSpecials.transferMultiple.name,
                                        text: "Duyệt và chuyển theo lô",
                                        iconClass: "icon-archive",
                                        callback: function () {
                                            that.transferMultiple(selectedIds, theoLoActions);
                                        }
                                    });

                                    theoLoActions.forEach(function (action, i) {
                                        var key = action.id;
                                        if (key === actionSpecials.luuSoVaPhatHanhNoiBo.name) {
                                            if (categoryIds.length === 1) {
                                                // Phát hành nội bộ
                                                contextData.add({
                                                    key: key,
                                                    text: action.name,
                                                    iconClass: "icon-archive",
                                                    callback: function () {
                                                        // Phát hành nội bộ
                                                        that.privatePublishTheoLo(selectedIds, docTypeId, categoryIds[0]);
                                                    }
                                                });
                                            }
                                        } else if (key === actionSpecials.luuSoVaPhatHanhRaNgoai.name) {
                                            // Phát hành ra ngoài
                                            if (categoryIds.length === 1) {
                                                contextData.add({
                                                    key: key,
                                                    text: action.name,
                                                    iconClass: "icon-archive",
                                                    callback: function () {
                                                        // Phát hành nội bộ
                                                        that.publishTheoLo(selectedIds, docTypeId, categoryIds[0]);
                                                    }
                                                });
                                            }
                                        } else {
                                            contextData.add({
                                                key: key,
                                                text: action.name,
                                                iconClass: "icon-forward4",
                                                callback: function () {
                                                    that.transferTheoLo(action, selectedIds);
                                                }
                                            });
                                        }

                                        if (i !== theoLoActions.length - 1) {
                                            // Hiển thị phân cách nếu cuối danh sách.
                                            var nextAction = theoLoActions[i + 1];
                                            if (action.priority !== nextAction.priority) {
                                                contextData.add({
                                                    name: '---',
                                                });
                                            }
                                        }
                                    });
                                }

                                contextData.add({
                                    name: '---',
                                });

                                contextData.add({
                                    key: "printTransferHistory",
                                    text: _resource.contextmenu.printTransferHistory,
                                    iconClass: "icon-printer",
                                    callback: function () {
                                        require(["print"], function (printView) {
                                            new printView({
                                                docCopyId: selectedIds,
                                                commonTemplate: egov.enum.commonTemplate.InBienNhanBanGiao
                                            });
                                        });
                                    }
                                });

                                if (selectedIds.length == 1) {
                                    contextData.add({
                                        key: "duplicateDocument",
                                        text: _resource.contextmenu.duplicateDocument,
                                        iconClass: "icon-copy",
                                        callback: function () {
                                            egov.views.home.tab.duplicateDocument(target.model, true);
                                        }
                                    });
                                }

                                if (contextData && contextData.length > 0) {
                                    loading.remove();
                                    that.context.model.set('data', contextData);
                                    that.context.render();
                                } else {
                                    fnError();
                                }
                            };

                            // chuyển văn bản theo lô
                            if (hasGetActionTheoLo) {
                                //Lấy danh sách các hướng chuyển
                                loading.css({ 'position': 'absolute' }).appendTo('body');
                                that._getActionTheoLoVanBan(selectedIds, function (theoLoActions) {
                                    loading.remove();
                                    command(items, theoLoActions);
                                }, function () {
                                    fnError();
                                });
                            } else {
                                command(items, []);
                            }
                        });

                        that._hideProcessing();
                    },
                    error: function (xhr) {
                        fnError();
                        that._hideProcessing();
                    }
                });
        },

        _contextmenuInFile: function (e, target, attachIds) {
            /// <summary>
            ///  Contextmenu vào file khi selected
            /// </summary>
            this.context = target.$el.contextmenu({
                isShowLoading: true,
                style: {
                    'min-width': '150px',
                    'width': 'auto'
                },
                position: {
                    my: 'left top',
                    of: e
                },
                trigger: 'right'
            });

            this.context.model.set('data', this._getContextFileItems(attachIds));
            this.context.render();
        },

        //<summery>Tạo context menu/summery>
        _getContextFileItems: function (attachIds) {
            /// <summary>
            ///  Danh sách các item trên context menu thao tác với file trên danh sách văn bản
            ///</summary>
            ///<param name="attachIds">Danh sách các file đính kèm cần thao tác</param>
            var _resource = egov.resources.document.attachment;
            var contextItems = new egov.models.contextMenuList();
            var that = this;

            // Cho phép mở file để sửa
            contextItems.add({
                text: _resource.openFile,
                value: 'openattach',
                iconClass: 'icon-eye3',
                callback: function () {
                    for (var i = 0; i < attachIds.length; i++) {
                        openAttachment(attachIds[i]);
                    }
                }
            });

            //Download file
            contextItems.add({
                text: _resource.download,
                value: 'download',
                iconClass: 'icon-download4',
                callback: function () {
                    for (var i = 0; i < attachIds.length; i++) {
                        downloadAttachment(attachIds[i]);
                    }
                }
            });

            contextItems.add({ name: '---' });

            //// Xóa file
            //contextItems.add({
            //    text: _resource.deleteFile,
            //    value: 'remove',
            //    iconClass: 'icon-trash',
            //    callback: function () {
            //        for (var i = 0; i < attachIds.length; i++) {
            //            var callback;
            //            if (i === attachIds.length - 1) {
            //                callback = function () {
            //                    egov.pubsub.publish(egov.events.status.success, "Xoá tệp thành công!");

            //                    if (egov.views
            //                         && egov.views.home.tree
            //                         && egov.views.home.tree.storeTree
            //                         && egov.views.home.tree.storeTree.storeModelSelected) {
            //                        egov.views.home.tree.storeTree.storeModelSelected.select();
            //                    }
            //                    egov.pubsub.publish(egov.events.status.destroy);
            //                };
            //            }

            //            removeAttachment(attachIds[i], callback);
            //        }
            //    }
            //});

            return contextItems;
        },

        removeDocumentsAndSetSelected: function (documents, callback) {
            /// <summary>
            /// Xóa danh sách văn bản trên client
            ///Note: DÙn hàm này sẽ lấy ra được utem sẽ được chọn kế tiếp khi các danh sách văn bản bị xóa trên giao diện
            /// </summary>
            ///<param name="documents" type="array">Mảng chứa văn bản copy</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thực thi thành công</param>
            var that = this;
            var command = function (itemSelected) {
                that.removeDocuments(documents, callback, true);
                if (itemSelected) {
                    itemSelected.set("Selected", true)
                }
            };
            that.selectedNextCurrent(function (itemSelected) {
                command(itemSelected);
            });
        },

        removeDocuments: function (documents, callback, isDeleteView) {
            /// <summary>
            /// Xóa danh sách văn bản trên client
            /// </summary>
            ///<param name="documents" type="array">Mảng chứa văn bản copy</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thực thi thành công</param>
            ///<param name="isDeleteView" type="boolean">Có xóa trên giao diện người dùng hay không</param>

            if (!documents || documents.length <= 0) {
                return;
            }

            if (!(documents instanceof Array)) {
                documents = documents.toArray();
            }

            if (isDeleteView) {
                var documentCopyIds = [];
                _.each(documents, function (doc) {
                    documentCopyIds.push(doc.get("DocumentCopyId"));
                });
                this._hideDocuments(documentCopyIds);
            }

            //Xóa văn bản khỏi model
            this.model.remove(documents);

            if (this.model.length === 0) {
                var colspan = this.columnSetting.length;
                this.$el.find('tbody')
                    .empty()
                    .append("<tr><td colspan=" + colspan + ">" + egov.resources.documents.noDocumentCopyItem + "</td></tr>");
            }

            //HopCV
            //todo: Bỏ phân trang
            //if (!egov.setting.isScrollPaging && this.paging) {
            //    var countRemove = documents.length;
            //    var total = this.total - countRemove;
            //    this.total = total >= 0 ? total : 0;

            //    total = parseInt(this.paging.model.get('totalDocument')) - countRemove;
            //    total = total > 0 ? total : 0;
            //    this.paging.model.set('totalDocument', total);
            //}

            //Lấy những văn bản chưa đọc
            var docUnread = _.filter(documents, function (item) {
                return item.get('IsViewed') == false;
            });

            if (!this.unread) {
                this.unread = parseInt(this.node.model.get('totalDocumentUnread'));
            }

            if (docUnread && docUnread.length > 0) {
                var unread = this.unread - docUnread.length;
                this.unread = unread > 0 ? unread : 0;
            }

            this.rebindUnreadForNode();

            this._updateDocumentsToCache(this.model.toJSON(), this.columnSetting, callback);
        },

        removeDocumentByIds: function (documentCopyIds, callback) {
            /// <summary>
            /// Xóa danh sách văn bản trên client
            /// </summary>
            ///<param name="documents" type="array">Mảng chứa văn bản copy</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thực thi thành công</param>

            if (!documentCopyIds || documentCopyIds.length <= 0) {
                return;
            }

            if (!this.model || this.model.length <= 0) {
                this.rebindUnreadForNode();
                return;
            }

            var documents = this.model.select(function (doc) {
                return _.contains(documentCopyIds, doc.get("DocumentCopyId"));
            });

            this._hideDocuments(documentCopyIds);

            this.removeDocuments(documents, callback);
        },

        removeDocumentByIdsAndSetSelected: function (documentCopyIds, callback) {
            /// <summary>
            /// Xóa danh sách văn bản trên client và selected phần tử kế tiếp
            /// </summary>
            ///<param name="documents" type="array">Mảng chứa văn bản copy</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thực thi thành công</param>

            if (!documentCopyIds || documentCopyIds.length <= 0) {
                return;
            }

            if (!this.model || this.model.length <= 0) {
                this.rebindUnreadForNode();
                return;
            }

            var that = this;
            var documents = this.model.select(function (doc) {
                return _.contains(documentCopyIds, doc.get("DocumentCopyId"));
            });

            var command = function (itemSelected) {
                that._hideDocuments(documentCopyIds);
                that.removeDocuments(documents, callback);
                if (itemSelected) {
                    itemSelected.set("Selected", true)
                }
            };

            this.selectedNextCurrent(function (itemSelected) {
                command(itemSelected);
            });
        },

        setViewed: function (documents, isViewed, callback) {
            /// <summary>
            /// Thiết lập trạng thái xem văn bản trên client
            /// Và update về server, nếu có lỗi trong quá trình update thì rollback lại trạng thái
            /// </summary>
            ///<param name="documentIds" type="array">Mảng chứa danh sách model văn bản</param>
            ///<param name="isViewed" type="bool">Trạng thái set cho văn bảns</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thực thi thành công</param>
            var that = this;
            this.setViewedInCache(documents, isViewed, function () {
                var outDocumentErrors = [];
                that._setViewed(documents, isViewed, outDocumentErrors, function () {
                    //Nếu có lỗi trong quá trình setviewed thì cập nhật lại những văn bản lỗi
                    if (outDocumentErrors && outDocumentErrors.length > 0) {
                        var leng = outDocumentErrors.length;
                        for (var i = 0; i < leng; i++) {
                            outDocumentErrors[i].set("IsViewed", !isViewed);
                            that.model.findWhere({ "DocumentCopyId": outDocumentErrors[i].get("DocumentCopyId") }).set("IsViewed", !isViewed);
                        }

                        if (!that.unread) {
                            that.unread = parseInt(that.node.model.get('totalDocumentUnread'));
                        }

                        if (isViewed == true) {
                            that.unread += leng;
                        } else {
                            that.unread -= leng;
                        }

                        that.rebindUnreadForNode();
                        that._updateDocumentsToCache(that.model.toJSON(), that.columnSetting, callback);
                    }
                    else {
                        egov.callback(callback);
                    }

                    that._hideProcessing();
                });
            });
        },

        setViewedInCache: function (documents, isViewed, callback) {
            /// <summary>
            /// Thiết lập trạng thái xem văn bản trên client
            /// </summary>
            ///<param name="documentIds" type="array">Mảng chứa danh sách model văn bản</param>
            ///<param name="isViewed" type="bool">Trạng thái set cho văn bảns</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thực thi thành công</param>
            if (!(documents instanceof Array) || documents.length <= 0) {
                return;
            }

            var leng = documents.length,
                that = this;
            for (var i = 0; i < leng; i++) {
                this.model.findWhere({ "DocumentCopyId": documents[i].get("DocumentCopyId") }).set("IsViewed", isViewed);
            }

            if (!this.unread) {
                this.unread = parseInt(this.node.model.get('totalDocumentUnread'));
            }

            if (isViewed == true) {
                if (this.unread >= leng) {
                    this.unread -= leng;
                } else {
                    this.unread = 0;
                }
            } else {
                this.unread += leng;
            }

            this.rebindUnreadForNode();
            this._updateDocumentsToCache(this.model.toJSON(), this.columnSetting, callback);
        },

        _setViewed: function (documents, isViewed, outDocumentErrors, callback) {
            /// <summary>
            /// Thay đổi trạng thái xem văn bản
            /// </summary>
            ///<param name="documents" type="Array">Danh sách văn bản kết thúc</param>
            ///<param name="isViewed" type="bool">Trạng thái xem văn bản</param>
            ///<param name="outDocumentErrors" type="Array">Danh sách văn bản khi kết thúc bị lỗi</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thành công</param>

            if (!documents || documents.length <= 0) {
                egov.callback(callback);
                return;
            }

            var that = this, document = documents[0];

            updateViewed(
                document.get('DocumentCopyId'),
                isViewed,
                function () {
                    documents.shift();
                    that._setViewed(documents, isViewed, outDocumentErrors, callback);
                },
                function () {
                    documents.shift();
                    outDocumentErrors.push(document);
                    that._setViewed(documents, isViewed, outDocumentErrors, callback);
                }
            );
        },

        finishs: function (documents, comment, callback, documentView) {
            /// <summary>
            /// Kết thúc xử lý văn bản
            /// </summary>
            ///<param name="documents" type="array">Mảng chứa id của văn bản copy</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thực thi thành công</param>
            if (!documents || !Array.isArray(documents) || documents.length <= 0) {
                return;
            }

            var that = this, outDocumentErrors = [];
            egov.pubsub.publish(egov.events.status.processing, egov.resources.document.finish.processing);

            // Xóa danh sách văn bản kết thúc khỏi danh văn bản
            that.selectedNextCurrent(function (itemSelected) {
                that.removeDocuments(documents);

                require(['docStoreView'], function (DocStore) {
                    if (!that.storePrivate) {
                        that.storePrivate = new DocStore;
                    }
                    var hasHideLuuSo = egov.setting.hasHideLuuSo;
                    if (hasHideLuuSo) {
                        that._finishs(documents, null, comment, outDocumentErrors, function () {
                            //Nếu có lỗi trong quá trình kết thúc văn bản
                            if (outDocumentErrors && outDocumentErrors.length > 0) {
                                that.model.add(outDocumentErrors);

                                that._updateDocumentsToCache(that.model.toJSON(), that.columnSetting, function () {
                                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.finish.error);
                                    that.renderClientFilter();
                                    egov.callback(callback);
                                });
                            }
                            else {
                                egov.pubsub.publish(egov.events.status.success, egov.resources.document.finish.success);
                                if (itemSelected) {
                                    itemSelected.set("Selected", true);
                                }
                                egov.callback(callback);
                            }

                            that._hideProcessing();
                        });
                        return false;
                    }
                    that.storePrivate.renderDialog(function (storeId) {
                        egov.pubsub.publish(egov.events.status.processing, egov.resources.document.finish.processing);
                        var finishFunc = function () {
                            that._finishs(documents, storeId, comment, outDocumentErrors, function () {
                                //Nếu có lỗi trong quá trình kết thúc văn bản
                                if (outDocumentErrors && outDocumentErrors.length > 0) {
                                    that.model.add(outDocumentErrors);

                                    that._updateDocumentsToCache(that.model.toJSON(), that.columnSetting, function () {
                                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.finish.error);
                                        that.renderClientFilter();
                                        egov.callback(callback);
                                    });
                                }
                                else {
                                    egov.pubsub.publish(egov.events.status.success, egov.resources.document.finish.success);
                                    if (itemSelected) {
                                        itemSelected.set("Selected", true);
                                    }
                                    egov.callback(callback);
                                }

                                that._hideProcessing();
                            });
                        };

                        if (documentView != undefined && documentView.transferType == egov.enum.documentTransferType.banGiaoKhiPhanLoai) {
                            documentView.updateDocument(function () {
                                finishFunc();
                            });
                        } else {
                            finishFunc();
                        }
                    });
                });

            });
        },

        _finishs: function (documents, storeId, comment, outDocumentErrors, callback) {
            /// <summary>
            /// Kết thúc văn bản
            /// Note: Hàm private ,chỉ dùng trong nội tại
            /// </summary>
            ///<param name="documents" type="Array">Danh sách văn bản kết thúc</param>
            ///<param name="outDocumentErrors" type="Array">Danh sách văn bản khi kết thúc bị lỗi</param>
            ///<param name="callback" type="function">Hàm gọi lại khi thành công</param>

            if (!documents || documents.length <= 0) {
                egov.callback(callback);
                return;
            }

            var that = this, document = documents[0];
            var documentCopyId = document.get('DocumentCopyId');
            var isThongBao = document.get('isThongBao') == 1 ? true : false;
            this.$el.find('#documentCopy' + documentCopyId).fadeOut(500).remove();

            finish(documentCopyId, storeId, comment, isThongBao,
                function () {
                    documents.shift();
                    that._finishs(documents, storeId, comment, outDocumentErrors, function () {
                        if (that.storePrivate) {
                            that.storePrivate.$el.dialog('hide');
                        }

                        egov.callback(callback);
                    });
                },
                function () {
                    documents.shift();
                    outDocumentErrors.push(document);
                    that._finishs(documents, storeId, outDocumentErrors, callback);
                }
            );


        },

        reQuickView: function (id) {
            /// <summary>
            /// Hiển thị lại xem tóm tắt văn bản
            /// </summary>
            var that = this, quickview;
            if (this.isOnlineRegistration) {
                var d = this.model.findWhere({ "Id": id });
                d && that._renQuickView(d.toJSON());
            } else {
                egov.request.quickViewDocument(
                    {
                        data: { id: id },
                        success: function (data) {
                            if (!data) {
                                return;
                            }

                            data.compendium = unescape(data.compendium);
                            that._renQuickView(data);
                        },
                        error: function () {
                            $(".document-preview").html(egov.resources.documents.message.error.quickView);
                        }
                    }
                );
            }
        },

        _reQuickViewQuestionTree: function (question) {
            var that = this;

            renderQuickView(egov.template.question.quickView, question.toJSON(), function () {
                $(".document-preview:visible").find(".btnQuickAnswer").click(function () {
                    var questionId = $(this).attr("questionId");
                    var $answerForm = $(this).closest(".formanswer");
                    var answer = $answerForm.find(".txtAnswer").val();
                    var isActive = $answerForm.find("#chkIsActive").is(":checked");
                    egov.request.answerQuestion({
                        data: {
                            questionId: questionId,
                            answer: answer,
                            isActive: isActive
                        },
                        success: function (result) {
                            $('tr#question-' + questionId).next().click();
                            $('tr#question-' + questionId).remove();
                        },
                        error: function (e) {
                        }
                    })
                });

                $(".document-preview:visible").find(".btnShowQuickAnswer").click(function () {
                    $(this).next().slideToggle();
                })
            });
        },

        _renQuickView: function (data) {
            /// <summary>
            /// Hiển thị xem tóm tắt văn bản
            /// </summary>
            var $el, template = getQuickViewTemplate();

            if (egov.enum.quickViewType.below === egov.setting.user.userSetting.quickView) {
                $el = '.document-preview-below';
            } else if (egov.enum.quickViewType.right === egov.setting.user.userSetting.quickView) {
                $el = '.document-preview-right';
            }

            if (!data || (!template || !$el)) {
                return;
            }

            if (this.isOnlineRegistration) {
                // Tóm tắt văn bản bên egov online
                renderQuickView(egov.template.documentList.quickViewOnlineRegistration, data);
                return;
            }

            if (data.type == quickViewType.egovOnline) {
                // Tóm tắt văn bản bên egov online
                renderQuickView(egov.template.documentList.quickViewDocumentOnline, data);
                return;
            }

            if (egov.setting.user.userSetting.isFullQuickView) {
                //Nếu người dùng cấu hình hiển thị đầy đủ thông tin của văn bản hồ sơ
                require(['documentView'], function (DocumentView) {
                    $(".document-preview").empty();
                    var documentView = new DocumentView({
                        id: data.id,
                        el: $el,
                        tab: null,
                        isCreateDocument: false,
                        documentViewType: egov.enum.documentViewType.preView
                    });

                    $(".document-preview").bindResources();
                });
                return;
            }

            //Nếu người dùng cấu hình chỉ hiển thị thông tin tóm tăt văn bản hồ sơ
            renderQuickView(template, data);
        },

        _sortBy: function () {
            /// <summary>
            /// Sắp xếp danh sách văn bản
            /// Note: danh sách văn bản này là chuyển thể từ object mảng hoặc là danh sách kiểu egov.models.documentList
            /// </summary>
            ///<param>Danh sách văn vản (documents, model)</param>
            var columnSorting = this.sorting;

            //Gán hàm sắp xếp comparator của Backbone để sắp xếp model theo
            this.model.comparator = function (doc, doc2) {
                var result = 0
                    , index = 0
                    , leng = columnSorting.length;

                while (index < leng && result === 0) {
                    var columnName = columnSorting[index].columnName;

                    var value, value2;
                    if (columnName === "DateReceived1") {
                        value = Date.parse(doc.get(columnName));
                        value2 = Date.parse(doc2.get(columnName));
                    } else if (columnName === "DateCreated") {
                        value = Date.parse(doc.get(columnName), "dd/MM/yyyy");
                        value2 = Date.parse(doc2.get(columnName), "dd/MM/yyyy");
                    }
                    else if (columnName === "InOutCode") {
                        value = parseInt(doc.get(columnName));
                        value2 = parseInt(doc2.get(columnName));
                    }
                    else {
                        value = doc.get(columnName);
                        value2 = doc2.get(columnName);
                    }

                    if (value === null || value == '') {
                        result = 1;
                    } else if (value2 === null || value2 == '') {
                        result = -1;
                    } else if (value > value2) {
                        result = -1;
                    } else if (value < value2) {
                        result = 1;
                    }

                    result = columnSorting[index].isDesc ? result : -result;
                    index++;
                }

                return result;
            };

            this.model.sort();
        },

        _getLastestDocuments: function (callback, isAuto) {
            /// <summary>
            /// Load danh sách các Document mới và các document đã bị remove của node hiện tại.
            /// </summary>

            this.sorting = this.sorting || this.defaultSorting;
            // Lấy ra document mới nhất đã load.
            var data = this.data,
                that = this;

            if (this.model && this.model.length > 0) {
                var lastDocument = this.model.sortBy(function (itm) {
                    return itm.get("DateReceived");
                }).reverse()[0];

                if (lastDocument) {
                    var lastUpdate = lastDocument.get('DateReceived1');
                    if (lastUpdate == undefined) {
                        lastUpdate = lastDocument.get('DateReceived');
                    }
                    data.lastDate = lastUpdate;
                }

                data.currentDocCopyIds = this.model.pluck('DocumentCopyId');
            }

            egov.request.getLastestDocuments({
                data: data,
                success: function (result) {
                    if (!result || (!result.documents || typeof result.documents !== 'object')) {
                        return;
                    }
                    //Nếu danh sách văn bản không có thì mặc định sẽ load ra danh sách luôn
                    if (!that.model || that.model.length <= 0) {
                        isAuto = false;
                    }

                    //Nếu tự động load danh sách mời về thì add tạm vào danh sách  thêm mới, xóa
                    if (isAuto) {
                        that.tmpCallback = callback;
                        if (result.documents && result.documents.length > 0) {
                            that.documentListAdd = that.documentListAdd.concat(result.documents);
                        }

                        if (result.removeds && result.removeds.length > 0) {
                            that.documentListRemove = that.documentListRemove.concat(result.removeds);
                        }

                        var command = function (isAppendToTop) {
                            if ((that.documentListAdd && that.documentListAdd.length > 0)
                                || (that.documentListRemove && that.documentListRemove.length > 0)) {

                                that._addOrRemove(true, that.documentListAdd, that.documentListRemove,
                                    function (isAuto, addDocNews, removeDocs, isAppendToTop, isFirstSelected) {
                                        if (typeof that.tmpCallback == 'function') {
                                            if (isAppendToTop) {
                                                addDocNews = addDocNews.reverse();
                                            }

                                            var addDocIds = _.pluck(addDocNews, "DocumentCopyId");
                                            var addModels = that.model.select(function (doc) {
                                                return _.contains(addDocIds, doc.get("DocumentCopyId"));
                                            });
                                            that.tmpCallback(isAuto, addModels, removeDocs, isAppendToTop, isFirstSelected, true);
                                        }
                                        that._resetTempDocIsAutoGetNews();
                                    },
                                    false, isAppendToTop);
                            }
                        }

                        var isAppendToTop = false;
                        command(isAppendToTop);

                        return;
                    }

                    that._resetTempDocIsAutoGetNews();
                    var isAppendToTop = true;
                    var isFirstSelected = true;
                    that._addOrRemove(isAuto, result.documents, result.removeds, callback, isAppendToTop, isFirstSelected);

                    that._hideProcessing();
                },
                error: function () {
                    //if (isAuto) {
                    //    callback(null, null, null, null, null, false);
                    //}
                    callback(null, null, null, null, null, false);

                    egov.pubsub.publish(egov.events.status.error,
                        _resource.message.error.loadNewerDocuments);
                }
            });
        },

        _addOrRemove: function (isAuto, addDocs, removeDocs, callback, hasSort, isAppendToTop) {
            var that = this,
                addDocNews = [],
                removeDocs2 = [],
                isFirstSelected = isAuto ? false : true;

            if (removeDocs && removeDocs.length > 0) {
                that.hasChanged = true;
                if (that.model && that.model.length > 0) {
                    var selectedModels = that.model.select(function (doc) {
                        return doc.get('Selected') == true;
                    });

                    if (selectedModels && selectedModels.length > 0) {
                        var filterModels = _.filter(selectedModels, function (doc) {
                            return _.contains(removeDocs, doc.get("DocumentCopyId"));
                        });
                        isFirstSelected = filterModels.length > 0 && selectedModels.length === filterModels.length;
                    }

                    that.model.remove(removeDocs);
                    if (isAuto && isFirstSelected && that.model.length > 0) {
                        that.model.at(0).set("Selected", true);
                    }
                }

                removeDocs2 = removeDocs;
            }

            if (addDocs && addDocs.length > 0) {
                _.each(addDocs, function (doc) {
                    var docInCache = null;
                    if (that.model && that.model.length > 0) {
                        docInCache = that.model.detect(function (cacheDoc) {
                            return cacheDoc.get("DocumentCopyId") == doc.DocumentCopyId;
                        });
                    }

                    if (docInCache) {
                        docInCache.set(doc);
                    } else {
                        addDocNews.push(doc);
                        that.hasChanged = true;
                        doc = new egov.models.document(doc);
                        if (!that.model || that.model.length <= 0) {
                            that.model = new egov.models.documentList;
                        }

                        that.model.add(doc);
                    }
                });
            }

            // Rebind lại danh sách khi có sự thay đổi văn bản
            if ((addDocs && addDocs.length > 0)
                || (removeDocs && removeDocs.length > 0)) {
                if (hasSort)
                    that._sortBy();
            }

            // that.unread = 0;
            //if (that.model && that.model.length > 0) {
            //    that.unread = that.model.select(function (doc) {
            //        return doc.get("IsViewed") == false;
            //    }).length;
            //}

            // that.rebindUnreadForNode();
            if (that.hasChanged) {
                var documents = that.model.toJSON();
                that._updateDocumentsToCache(documents, that.columnSetting, function () {
                    if (typeof callback === 'function') {
                        callback(isAuto, addDocNews, removeDocs2, isAppendToTop, isFirstSelected, true);
                    }
                });
            }
            else {
                if (typeof callback === 'function') {
                    callback(isAuto, addDocNews, removeDocs2, isAppendToTop, isFirstSelected, true);
                }
            }
        },

        _getDocuments: function (successFunc, isLoaded) {
            /// <summary>
            /// Xử lý lấy danh sách văn bản:
            ///    - Trước tiên lấy từ Cache Client.
            ///    - Không có thì gọi về server lấy và lưu lại xuống client
            /// </summary>
            /// <param name="successFunc"></param>
            var that = this,
                commandServer = function () {
                    //Hàm gọi về server lấy dữ liệu
                    that._getDocumentsFromServer(function () {
                        that._getDocuments(successFunc, true);
                    });
                };

            egov.dataManager.getDocumentsCache({
                success: function (cacheDocuments) {
                    if (!cacheDocuments) {
                        commandServer();
                        return;
                    }

                    if (cacheDocuments.userId !== egov.setting.userId) {
                        commandServer();
                        return;
                    }

                    that.cacheDocuments = cacheDocuments;

                    var userCache = cacheDocuments['localCache'];
                    var treeCaches = userCache['documentTrees'];
                    var nodeId = that.nodeId;

                    if (that.isStoreTree) {
                        treeCaches = userCache['documentStore'];
                    }

                    var paras = that.node.model.get('params');
                    var nodes = _.find(treeCaches, function (item) {
                        return item.nodeId === nodeId && item.nodeParams == paras;
                    });

                    if (!nodes) {
                        commandServer();
                        return;
                    }

                    var documents = nodes['documents'];
                    if (!document || documents.length <= 0) {
                        if (!isLoaded) {
                            commandServer();
                            return;
                        }
                    }

                    var settings = nodes['settings'];

                    // Xử lý danh sách các cột theo cấu hình
                    that.columnSetting = _.sortBy(settings['columnSetting'], function (itm) {
                        return itm.Order;
                    });

                    // Gán lại các cấu hình của node vào view
                    that.doctypeColumnSettings = settings['doctypeColumnSettings'];
                    that.isDoctypeFilter = settings['isFilterByDocFieldDocType'];
                    that.isOverdueFilter = settings['isOverdueFilter'];
                    that.isDateFilter = settings['isDateFilter'];
                    that.dateFilterView = settings['dateFilterView'];
                    that.dateFilter = settings['dateFilter'];
                    that.currentPage = settings['currentPage'];
                    that.pageSize = settings['pageSize'];
                    that.enablePaging = settings['enablePaging'];

                    var newReportRow = that._bindCreateReportRow();
                    documents = documents || [];
                    newReportRow && documents.unshift(newReportRow);
                    that.model = new egov.models.documentList(documents);
                    that._sortBy();

                    // that.rebindUnreadForNode();

                    ///Đồng bộ về server
                    ///Truyền vào danh sách documentId về server và trả về những văn bản đã xóa hoặc người dùng không giữ nữa và những văn bản mới
                    egov.callback(successFunc, that.documents);
                }
            });
        },

        _bindCreateReportRow: function () {
            var nodeParam = JSON.parse(this.node.model.get("param"));
            if (nodeParam.length === 0) return null;

            var param = nodeParam[0];
            if (param.Key !== "@docTypeId") return null;

            var doctypeId = param.Value;
            if (doctypeId === '') return null;

            var doctype = _.find(egov.setting.allDoctypes, (dt) => {
                return dt.DocTypeId == doctypeId;
            });

            if (!doctype) return null;
            var actionLevel = doctype.ActionLevel;

            var result = { "Status": "Tạo mới", "Compendium": "", "Compendium2": "", "DateCreated": "17/08/2019", "DocTypeId": "", "CategoryBusinessId": 4, "DocumentId": "0", "DatePublished": "", "DateReceived1": "2019-08-17T14:43:55", "DocumentCopyId": 0, "hasUpdateViewed": 0, "ActionLevel": 1 };
            result.Compendium = doctype.DocTypeName;
            result.DocTypeId = doctype.DocTypeId;
            result.ActionLevel = doctype.ActionLevel;
            var now = new Date();
            switch (actionLevel) {
                case 1: // Year
                    result.DatePublished = "Năm " + now.getFullYear();
                    break;
                case 2: // Quarter
                    result.DatePublished = "Quý " + now.quarter();
                    break;
                case 3: // Month
                    result.DatePublished = "Tháng " + now.getMonth();
                    break;
                case 4: // Week
                    result.DatePublished = "Tuần " + now.weekOfYear();
                    break;
                case 5: // Day
                    result.DatePublished = "Ngày " + now.format("dd/M");
                    break;
                default: return null;
            }

            return result;
        },

        _getDocumentStore: function (callback) {
            var that = this,
             data = this.data;
            egov.dataManager.getStorePrivateDocuments(
                data, {
                    success: function (result) {
                        // Gán các document trả về làm model cho danh sách document hiện tại
                        var documents = JSON.parse(result.documents);
                        that.model = new egov.models.documentList(documents);
                        that._sortBy();
                        ///Giao diện desktop thì hiển thị
                        if (!egov.isMobile) {
                            // Xử lý danh sách các cột theo cấu hình
                            var columnSetting = storeColumnSettings;
                            if (!columnSetting) {
                                columnSetting = [];
                            }

                            if (!_.find(columnSetting, function (item) { return item.ColumnName == "STT"; })) {
                                columnSetting.push({ ColumnName: "STT", DisplayName: "", Width: 30, EnableSort: false, Order: -1 });
                            }

                            that.columnSetting = _.sortBy(columnSetting, function (itm) {
                                return itm.Order;
                            });
                        }

                        egov.callback(callback);

                        that._hideProcessing();
                    }
                });
        },

        reloadQuestionList: function () {
            var that = this;
            this._getQuestionList(function (total) {
                that.$el.parent().children().hide();
                that.renderStore();
                egov.callback(that.treeCallback, total);

                that._hideProcessing();
                that.$el.show();
            })
        },

        _getQuestionList: function (callback) {
            var that = this;

            var command = function (result) {
                if (that.docfieldIds) {
                    var listQuestion = result.questionListModel;
                    var rsltQuestions = [];
                    for (var i = 0; i < listQuestion.length; i++) {
                        if (listQuestion[i].Document && listQuestion[i].Document.ListDocFieldId) {
                            if (_.contains(listQuestion[i].Document.ListDocFieldId, that.docfieldIds)) {
                                rsltQuestions.push(listQuestion[i]);
                            }
                        }
                    }
                    result.questionListModel = rsltQuestions;
                }
                // Gán các document trả về làm model cho danh sách document hiện tại
                that.model = new egov.models.questionList(result.questionListModel);

                that._sortBy();
                // Xử lý danh sách các cột theo cấu hình
                var columnSetting = questionColumnSettings;
                if (!columnSetting) {
                    columnSetting = [];
                }

                that.columnSetting = _.sortBy(columnSetting, function (itm) {
                    return itm.Order;
                });

                if (that.isGeneralFAQTree) {
                    that.columnSetting.pop();
                }
                egov.callback(callback, that.model ? that.model.length : 0);

                that._hideProcessing();
            };

            egov.request.getsQuestion({
                data: {
                    isGetGeneral: that.isGeneralFAQTree
                },
                success: function (result) {
                    command(result);
                }
            });
        },

        _getDocumentOnlineRegistration: function (callback) {
            var that = this;
            var command = function (result) {
                if (that.node.model.get("isOnlineRegistrationChildren") == true) {
                    var doctypeId = that.node.model.get("functionId");
                    result = _.filter(result, function (item) {
                        return item.DoctypeId == doctypeId;
                    });
                }
                // Gán các document trả về làm model cho danh sách document hiện tại
                that.model = new egov.models.documentList(result);

                that._sortBy();
                // Xử lý danh sách các cột theo cấu hình

                var columnSetting = onlineRegistrationColumnSettings;
                if (!columnSetting) {
                    columnSetting = [];
                }
                //Giao diện desktop thì hiển thị
                if (!egov.isMobile) {
                    if (!_.find(columnSetting, function (item) { return item.ColumnName == "STT"; })) {
                        columnSetting.push({ ColumnName: "STT", DisplayName: "", Width: 30, EnableSort: false, Order: -1 });
                    }
                }
                that.columnSetting = _.sortBy(columnSetting, function (itm) {
                    return itm.Order;
                });
                egov.callback(callback, that.model ? that.model.length : 0);

                that._hideProcessing();
            };

            var commandMobile = function (result) {
                if (that.node.model.get("isOnlineRegistrationChildren") == true) {
                    var doctypeId = that.node.model.get("functionId");
                    result = _.filter(result, function (item) {
                        return item.DoctypeId == doctypeId;
                    });
                }
                // Gán các document trả về làm model cho danh sách document hiện tại
                that.model = new egov.models.documentList(result);

                egov.callback(callback, that.model ? that.model.length : 0);

                that._hideProcessing();
            };

            if (that.DocumentOnlineType == 2) {
                egov.request.getDocumentOnlineRegistration({
                    success: function (result) {
                        var listDocument = that._processingDataDocumentOnline(result);
                        if (egov.isMobile) {
                            commandMobile(listDocument);
                        }
                        else {
                            command(listDocument);
                        }
                    }
                });
            } else {
                egov.request.getDocumentOnlineCancel({
                    success: function (result) {
                        var listDocument = that._processingDataDocumentOnline(result);

                        if (egov.isMobile) {
                            commandMobile(listDocument);
                        }
                        else {
                            command(listDocument);
                        }
                    }
                });
            }

        },

        _processingDataDocumentOnline: function (data) {
            /// <summary>
            /// hàm xử lý dữ liệu lấy từ hệ thống dịch vụ công về các hồ sơ đăng kí  theo người tiếp nhận
            /// </summary>
            //var doctypeIds = JSON.parse(egov.setting.onlineRegistration.Doctypes);
            //if (doctypeIds.length == 0) {
            //    return [];
            //}
            var result = [];
            if (!data) {
                return [];
            } else {
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    //var doctype = item['Doctype'];
                    //if (!doctype) {
                    //} else {
                    //    if (_.contains(doctypeIds, doctype.LocalDoctypeId)) {
                    //        result.push(item);
                    //    }
                    //}
                    result.push(item);
                }
            }

            return result;
        },

        _getDocumentsFromServer: function (callback) {
            /// <summary>
            /// Lấy danh sách document từ server và lưu vào cache client.
            /// </summary>
            var that = this,
                data = this.data;
            egov.dataManager.getDocuments(data, {
                success: function (result) {
                    // Gán các document trả về làm model cho danh sách document hiện tại
                    var documents = that.isStoreTree ? JSON.parse(result.documents) : result.documents;
                    that.model = new egov.models.documentList(documents);
                    that._sortBy();

                    // Xử lý danh sách các cột theo cấu hình
                    var columnSetting = that.isStoreTree ? storeColumnSettings : result.columnSetting;
                    if (!columnSetting) {
                        columnSetting = [];
                    }

                    ///Giao diện desktop thì hiển thịss
                    if (!egov.isMobile) {
                        if (!_.find(columnSetting, function (item) { return item.ColumnName == "STT"; })) {
                            columnSetting.push({ ColumnName: "STT", DisplayName: "", Width: 30, EnableSort: false, Order: -1 });
                        }
                    }

                    columnSetting = _.sortBy(columnSetting, function (itm) {
                        return itm.Order;
                    });

                    that.isFirstload = true;
                    documents = that.model.toJSON();
                    that._updateDocumentsToCache(documents, columnSetting, callback);

                    that._hideProcessing();
                }
            });
        },

        _updateDocumentsToCache: function (documents, colSettings, callback) {
            /// <summary>
            /// Lưu lại dữ liệu xuống Cache
            /// </summary>
            /// <param name="documents">Danh sách Document</param>
            /// <param name="colSettings">Column settings</param>
            var localCacheName = this.isStoreTree ? 'documentStore' : 'documentTrees',
                nodeId = this.nodeId,
                that = this,
                newCache,
                nodeParams = this.data.paramsQuery,
                settings = {
                    columnSetting: colSettings,
                    doctypeColumnSettings: this.doctypeColumnSettings,
                    isDoctypeFilter: this.isDoctypeFilter,
                    isOverdueFilter: this.isOverdueFilter,
                    isDateFilter: this.isDateFilter,
                    dateFilterView: this.dateFilterView,
                    dateFilter: this.dateFilter,
                    currentPage: this.currentPage,
                    pageSize: this.pageSize,
                    enablePaging: this.enablePaging
                };

            var setNewCache = function () {
                newCache = that._getNewDocumentCache(documents, colSettings, nodeId, nodeParams);
                egov.dataManager.updateDocuments(newCache, callback);
            }

            egov.dataManager.getDocumentsCache({
                success: function (cacheDocuments) {
                    if (!cacheDocuments) {
                        setNewCache();
                        return;
                    }

                    if (!cacheDocuments.userId
                        || cacheDocuments.userId !== egov.setting.userId) {
                        setNewCache();
                        return;
                    }

                    if (!cacheDocuments['localCache']
                        || cacheDocuments['localCache'].length <= 0) {
                        setNewCache();
                        return;
                    }

                    if (!cacheDocuments['localCache'][localCacheName]
                        || cacheDocuments['localCache'][localCacheName].length <= 0) {
                        setNewCache();
                        return;
                    }

                    var userCache = cacheDocuments['localCache'];
                    var treeCaches = userCache[localCacheName];
                    if (!treeCaches) {
                        var newCache = that._getNewDocumentCache()
                    }

                    var nodes = _.find(treeCaches, function (item) {
                        return item.nodeId === nodeId && item.nodeParams == nodeParams;
                    });

                    if (nodes) {
                        documents = that.model.toJSON();
                        nodes.documents = documents;
                        nodeParams: nodeParams,
                        nodes.settings = settings;
                    } else {
                        newCache = {
                            nodeId: nodeId,
                            nodeParams: nodeParams,
                            settings: settings,
                            documents: documents
                        };

                        treeCaches.push(newCache);
                        cacheDocuments.localCache[localCacheName] = treeCaches;
                    }

                    cacheDocuments = $.extend({}, cacheDocuments);
                    egov.dataManager.updateDocuments(cacheDocuments, callback);
                }
            });
        },

        _getNewDocumentCache: function (documents, colSettings, nodeId, nodeParams) {
            /// <summary>
            /// Lưu danh sách document mới vào cache.
            /// Sử dụng khi chọn node mới (chưa có dữ liệu trong cache).
            /// </summary>
            var localCacheName = this.isStoreTree ? 'documentStore' : 'documentTrees'
                , newCache = {
                    userId: egov.setting.userId,
                    localCache: {}
                }
                , nodes = {
                    nodeId: nodeId,
                    nodeParams: nodeParams,
                    settings: {
                        columnSetting: colSettings,
                        doctypeColumnSettings: this.doctypeColumnSettings,
                        isDoctypeFilter: this.isDoctypeFilter,
                        isOverdueFilter: this.isOverdueFilter,
                        isDateFilter: this.isDateFilter,
                        dateFilterView: this.dateFilterView,
                        dateFilter: this.dateFilter,
                        currentPage: this.currentPage,
                        pageSize: this.pageSize,
                        enablePaging: this.enablePaging
                    },
                    documents: documents
                };

            newCache.localCache[localCacheName] = [nodes];

            return newCache;
        },

        _getActionTheoLoVanBan: function (documentCopyIds, callback, callbackError) {
            /// <summary>
            /// Trả về danh sách hướng chuyển theo lô
            /// </summary>
            /// <param name="documentCopyIds">Danh sách các document được chọn</param>
            if (!documentCopyIds || documentCopyIds.length <= 0) {
                egov.callback(callback, []);
                return;
            }

            var data = [];
            //Chỗ này đã cho ajax chạy theo đồng bộ
            egov.request.getActionTheoLoVanBan(
              {
                  data: { documentCopyIds: documentCopyIds },
                  success: function (result) {
                      if (!result || !result.error) {
                          data = result;
                      }

                      egov.callback(callback, data);
                  },
                  error: function (xhr) {
                      egov.callback(callbackError);
                      console.log(xhr.message);
                  }
              });
        },

        transferTheoLo: function (transferAction, documentCopyIds) {
            ///<summary>
            /// Chuyển văn bản theo lô
            ///<param name="transferAction" type="object">Đối tượng hướng chuyển</param>
            ///<param name="documentCopyIds" type="Array"> Danh sách id văn bản chọn để chuyển theo lô</param>
            ///<summary>

            if (!documentCopyIds || documentCopyIds.length <= 0) {
                egov.pubsub.publish(egov.events.status.error, egov.resources.documents.transfer.notSelectedDocument);
                return;
            }

            if (!transferAction) {
                egov.pubsub.publish(egov.events.status.error, egov.resources.documents.transfer.noAction);
                return;
            }

            var that = this
            , hasDisplayPopup = egov.locache.hasSupportLocalStorage //lấy trạng thái có hiển thị popup comment lên hay không
                        ? egov.localStorage.displayPopupTransferTheoLo()
                        : egov.cookie.displayPopupTransferTheoLo()
            , transferTheoLo = function (content) { // Hàm thực thi chuyển văn bản theo lô
                require(['transfer'], function (Transfer) {
                    if (egov.transferForm === undefined) {
                        egov.transferForm = new Transfer;
                    }

                    egov.transferForm.renderTheoLo({
                        action: transferAction,
                        documentCopyIds: documentCopyIds,
                        comment: content,
                        callback: function () {
                            that._removeNotify(documentCopyIds);
                            that.hasPopup = false;
                        },
                        documents: that,
                        callbackCloseForm: function () { that.hasPopup = false; }
                    });
                });
            };

            this.hasPopup = true;
            if (hasDisplayPopup) {
                //Nếu người dùng cho hiển thị popup comment
                this.promptConfirmTransfer({
                    title: egov.resources.documents.transfer.confirmTitle,
                    confirmTitleName: egov.resources.documents.transfer.confirmCheckName,
                    primaryButtonName: egov.resources.documents.transfer.primaryButtonName,
                    callbackPrimary: function (content) {
                        transferTheoLo(content);
                    },
                    isCheckNullOrEmpty: false,
                    callbackConfirm: function (value) {
                        egov.request.setUserConfig({
                            data: { DisplayPopupTransferTheoLo: value },
                            success: function () {
                                //Ghi vào localstorage
                                if (egov.locache.hasSupportLocalStorage) {
                                    egov.localStorage.displayPopupTransferTheoLo(value);

                                } else {
                                    egov.cookie.displayPopupTransferTheoLo(value);
                                }
                            }
                        });
                    },
                    hasAutoComplete: true,
                    addTemplateButtonName: egov.resources.documents.transfer.addTemplateButtonName,
                    callbackCloseForm: function () {
                        that.hasPopup = false;
                    }
                });
            } else {
                //Không cho hiển thị popup comment
                transferTheoLo(null);
            }
        },

        publishTheoLo: function (documentCopyIds, docTypeId, categoryId) {
            ///<summary>
            /// Phát hành ra ngoài theo lô
            ///<param name="documents" type="Array"> Danh sách văn bản chọn để chuyển theo lô</param>
            ///<summary>
            if (!documentCopyIds || documentCopyIds.length <= 0) {
                egov.pubsub.publish(egov.events.status.error, egov.resources.documents.transfer.notSelectedDocument);
                return;
            }

            var that = this;

            require(['publishmentView'], function (publishView) {
                if (egov.publishment === undefined) {
                    egov.publishment = new publishView;
                }

                that.hasPopup = true;
                egov.publishment.renderTheoLo({
                    docTypeId: docTypeId,
                    documentCopyIds: documentCopyIds,
                    parent: that,
                    categoryId: categoryId,
                    callback: function () {
                        that.hasPopup = false;
                    }
                });
            });
        },

        transferMultiple: function (documentCopyIds, actions) {
            if (!documentCopyIds || documentCopyIds.length <= 0) {
                egov.pubsub.publish(egov.events.status.error, egov.resources.documents.transfer.notSelectedDocument);
                return;
            }

            require(['documentMultipleView'], function (documentMultipleView) {
                new documentMultipleView({
                    ids: documentCopyIds,
                    actions: actions
                });
            });
        },

        //showBmm: function () {
        //    var bmmDialog = "<div style='width: 100%; height: 100%;'><iframe style='width: 100%; height: 100%; border:none;' src='http://quanlynhiemvu.langson.gov.vn/Views/CreateMission.aspx?callBackSuccessful=GetListMissions'></iframe><div>";
        //    $(bmmDialog).dialog({
        //        title: "Tạo nhiệm vụ",
        //        height: 450,
        //        width: 1024
        //    });
        //},

        privatePublishTheoLo: function (documentCopyIds, docTypeId, categoryId) {
            ///<summary>
            /// Phát hành nội bộ theo lô
            ///<param name="documents" type="Array"> Danh sách văn bản chọn để chuyển theo lô</param>
            ///<summary>
            if (!documentCopyIds || documentCopyIds.length <= 0) {
                egov.pubsub.publish(egov.events.status.error, egov.resources.documents.transfer.notSelectedDocument);
                return;
            }

            var that = this;
            require(['privatePublishmentView'], function (privatePublishView) {
                if (egov.privatePublishment === undefined) {
                    egov.privatePublishment = new privatePublishView;
                }
                that.hasPopup = true;
                egov.privatePublishment.renderTheoLo({
                    docTypeId: docTypeId,
                    documentCopyIds: documentCopyIds,
                    parent: that,
                    categoryId: categoryId,
                    callback: function () {
                        that.hasPopup = false;
                    }
                });
            });
        },

        _noDocumentSelectedQuickview: function () {
            // Hiển thị thông báo chưa có văn bản được chọn
            $(".document-preview").html('<div style="margin-left:10px">' + egov.resources.documentQuickView.noDocumentSelected + '</div>');
        },

        selectFirstDocumentInList: function () {
            /// <summary>
            /// Mở văn bản đầu tiên trong list, dùng cho phần shortkey
            /// </summary>
            this.$("tbody tr:first").trigger("click");
        },

        setSelected: function (to) {
            /// <summary>
            ///  Gán selected cho 1 vùng document được tính từ row đang được select hiện tại
            /// </summary>
            /// <param name="to" type="DocumentModel">Đến row có số thứ tự</param>

            var last = this.model.indexOf(this.lastSelected);
            var first = this.model.indexOf(to);

            var start = Math.min(first, last);
            var end = Math.max(first, last) + 1;

            this.removeAllSelected();

            for (var i = start; i < end; i++) {
                var doc = this.model.at(i);
                if (doc.get('Selected') == false) {
                    doc.set('Selected', true);
                }
            }
        },

        selectAll: function () {
            if (!this.model || this.model.length <= 0)
                return;

            this.model.each(function (doc) {
                if (doc.get('Selected') == false) {
                    doc.set('Selected', true);
                }
            });
        },

        openDocuments: function (documents) {
            ///<summary>
            /// Mở văn bản
            ///<para name="documents" type="Array"> danh sách đối tượng văn bản</para>
            ///</summary>
            if (!documents || documents.length <= 0) {
                return;
            }
            egov.helper.hideAllContext();
            var that = this;
            var document = documents[0];
            var documentCopyId = document.get('DocumentCopyId'),
                compendium = document.get('Compendium');
            var hasLoadContent = documents.length === 1;
            var command = function () {
                documents.shift();
                that.openDocuments(documents);
                //Kiểm tra xem nếu không phải là văn bản theo dõi thì được phép cập nhật
                if (document.get('UserCurrentId') !== egov.setting.userId
                    && (document.get('Status') === 2
                    || document.get('Status') === 16)) {
                    return;
                }

                if (document.get('IsViewed') == false) {
                    that.setViewedInCache([document], true);
                }

                that._hideProcessing();
            };

            if (this.isStoreTree) {
                document.set("StorePrivateId", this.nodeId);
            }

            if (typeof document.get('DocumentCopyId') == "number") {
                egov.views.home.tab.openDocumentInfo(document, hasLoadContent, function () {
                    remoteOnNotify(documentCopyId);
                    command();
                });
            } else {
                egov.views.home.tab.openDocumentOnline(document.get('id'),
                   document.get('DocTypeName') + " - " + document.get('PersonInfo'),
                    hasLoadContent,
                    function () {
                        command();
                    }
                );
            }
        },

        _bindEventScroll: function () {
            ///<summary>
            /// Ẩn các contextmenu khi scroll
            ///</summary>
            var that = this;
            var $ele = egov.isMobile ? this.$el : this.$el.find('.grid-content');
            $ele.off('scroll').on('scroll', function () {
                that.hideAllContextmenu();
            });
        },

        _resetTempDocIsAutoGetNews: function () {
            this.documentListAdd = [];
            this.documentListRemove = [];
            this.tmpCallback = null;
        },

        hideAllContextmenu: function () {
            ///<summary>
            /// Ẩn các contextmenu
            ///</summary>
            if ($('img.loading').length > 0) {
                $('img.loading').remove();
            }

            if (egov.request && egov.request.aborts) {
                //hủy lấy quyền check tạo context menu
                if (egov.request.aborts['getDocumentPermission']) {
                    egov.request.aborts['getDocumentPermission'].abort();
                }
                //Hủy quyền lấy lại văn bản
                if (egov.request.aborts['getContextItemForUndoTransfering']) {
                    egov.request.aborts['getContextItemForUndoTransfering'].abort();
                }
            }

            if (this.context) {
                this.context.hide();
            }

            this._hideProcessing();
        },

        _eventKeyboard: function (e, $el) {
            ///<sumnary>
            /// Các xử lý phím tắt trên danh sách văn bản
            ///</sumnary>
            ///<param name="e" type="object"></param>
            ///<param name="$el" type="object jquery or string"></param>
            if (!e || egov.isMobile) {
                return;
            }

            if (!($el instanceof jQuery)) {
                $el = $($el);
            }

            var tabIdx = egov.views.home.tab.getActiveTab().index();

            // Chỉ xử lý khi tab hiện tại là tab home và không có popup, dialog nào hiển thị
            if (!this.hasPopup && tabIdx === 0) {

                if (e.keyCode === this.keyCode.enter) {
                    var selectedModels = this.model.select(function (doc) {
                        return doc.get('Selected') == true;
                    });

                    if (selectedModels && selectedModels.length > 0) {
                        this.openDocuments(selectedModels);
                    }
                }

                //Nhấn ctrl và phím a để chọn tất cả văn bản
                if (e.ctrlKey && this.keyCode.a === e.keyCode) {
                    this.selectAll();
                }
            }
        },

        appendBodyEventKey: function () {
            ///<sumnary>
            /// Thêm sự kiện nhấn phím enter khi selected document
            ///</sumnary>
            // var that = this;
            //$('body').unbind("keydown").bind("keydown", function (e) {
            //    that._eventKeyboard(e, that);
            //});
        },

        _removeNotify: function (documentCopyIds) {
            if (!documentCopyIds || documentCopyIds.length <= 0) {
                return;
            }
            remoteOnNotify(documentCopyIds[0]);
            documentCopyIds.shift();
            this._removeNotify(documentCopyIds);
        },

        getActionsTheoLo: function (documentCopyIds, callback) {
            ///<summary>
            /// Lấy các hướng chuyển chung theo danh sách id văn bản
            ///</summary>
            ///<param name="documentCopyIds"> Danh sách id văn bản</param>
            this._getActionTheoLoVanBan(documentCopyIds, callback);
        },

        reloadDocuments: function (callback, isAuto) {
            ///<summary>
            ///  hiển thị lại danh sách văn bản
            ///</summary>
            ///<param name="callback">hàm gọi lại khi thực thị xong</param>
            this.bindView();

            this.isStoreTree ? this.loadNewerDocumentsStore(callback) : this.loadNewerDocuments(callback, isAuto);
        },

        promptConfirmTransfer: function (options) {
            /// <summary>
            /// Hiển thị Prompt giống như javascript
            /// </summary>

            var defaultOptions = {
                title: "",//tiêu đề của dialog
                primaryButtonName: "",//Tên của nút button xử lý chính
                callbackPrimary: function () { },//Hàm xử lý khi click nút button xử lý chính
                isCheckNullOrEmpty: true,//Có kiểm tra giá trị null hay không
                callbackConfirm: function () { },//Hàm xử lý khi người dùng xác nhận trên form
                addTemplateButtonName: "",
                callbackCloseForm: function () { }
            };

            defaultOptions = $.extend(defaultOptions, options);
            var that = this;
            var $el = $('<div><textarea class="form-control" row="5" style="height: 120px"></textarea></div>');
            var textArea = $el.find('textarea');
            $el.dialog({
                title: defaultOptions.title,
                width: '500px',
                height: '150px',
                draggable: true,
                keyboard: true,
                confirm: {
                    id: "confirmTransfer",
                    name: "confirmTransfer",
                    text: defaultOptions.confirmTitleName,
                    style: {
                        'float': 'left',
                        'display': 'inline-block',
                        'font-size': '13px',
                        'font-weight': 'normal'
                    },
                    callback: function (value) {
                        if (typeof defaultOptions.callbackConfirm === 'function') {
                            defaultOptions.callbackConfirm(value);
                        }
                    }
                },
                buttons: [
                            {
                                text: defaultOptions.primaryButtonName,
                                className: 'btn-primary',
                                click: function () {
                                    var value = $(textArea).val();
                                    if (defaultOptions.isCheckNullOrEmpty && (value === '' || value == null)) {
                                        $(textArea).addClass('input-validation-error').first().focus();
                                        return;
                                    }

                                    if (typeof defaultOptions.callbackPrimary === 'function') {
                                        defaultOptions.callbackPrimary(value);
                                    }

                                    $el.dialog('destroy');
                                }
                            },
                            {
                                text: defaultOptions.addTemplateButtonName,
                                click: function () {
                                    that._openDialogCommon($(textArea));
                                }
                            }
                            , {
                                text: egov.resources.common.closeButton,
                                className: 'btn-close',
                                click: function () {
                                    if (typeof defaultOptions.callbackCloseForm === 'function') {
                                        defaultOptions.callbackCloseForm();
                                    }
                                    $el.dialog('destroy');
                                }
                            }
                ]
            });

            this._renderCommonComments($(textArea));
            textArea.focus();
        },

        _renderCommonComments: function ($textComent) {
            /// <summary>
            /// Hiển thị autocomplete ý kiến thường dùng
            /// </summary>
            var that = this;
            if (egov.isMobile) {
                return;
            }

            egov.dataManager.getCommonComment({
                success: function (comments) {
                    egov.dataManager.getTemplateComments({
                        success: function (templateComments) {
                            templateComments = _.pluck(templateComments, "Content");
                            templateComments = _.filter(templateComments, function (item) {
                                return comments.indexOf(item) == -1
                            });

                            if (templateComments.length > 0) {
                                _.each(templateComments, function (item) {
                                    comments.push(item)
                                });
                                egov.dataManager.addCommonComment(comments, true)
                            }
                            that._renderAutocomplete($textComent, comments);
                        }
                    });
                }
            });
        },

        _renderAutocomplete: function ($textComent, sources) {
            /// <summary>
            /// Tạo autocomplete ý kiến thường dùng
            /// </summary>
            //<param name="sources" type="Array">Mảng danh sách giá trị trong kho tìm kiếm</param>
            if ($textComent.length > 0) {
                $textComent.autocomplete({
                    source: function (request, response) {
                        var value = egov.utilities.string.stripVietnameseChars(request.term).toLowerCase();
                        var data = [];
                        for (var i = 0; i < sources.length; i++) {
                            var item = egov.utilities.string.stripVietnameseChars(sources[i]).toLowerCase();
                            if (item.indexOf(value) != -1) {
                                data.push(sources[i]);
                            }
                        }
                        response(data);
                    },
                    autoFocus: true,
                    autoSelectFirst: true
                }).data("autocomplete")._renderItem = function (ul, item) {
                    ul.addClass('dropdown-menu');
                    return $("<li>")
                        .data("item.autocomplete", item)
                        .append("<a href='#'>" + item.value + "</a>")
                        .appendTo(ul);
                }
            };
        },

        _openDialogCommon: function ($el) {
            ///<summary>
            /// Mở dialog danh sách mẫu ý kiến thường dùng cảu người dùng đã soạn trước đó
            ///</summary>
            require(["templateComment"], function (TemplateComment) {
                new TemplateComment({
                    callbackInsertComments: function (commentText) {
                        var val = $el.val();
                        if (val) {
                            val += '\n' + commentText;
                        } else {
                            val = commentText;
                        }

                        $el.val(val);
                    }
                });
            });
        },

        exportToExcel: function () {
            this.exportToFile(1);
        },

        exportToWord: function () {
            this.exportToFile(2);
        },

        exportToXML: function () {
            this.exportToFile(3);
        },

        exportToFile: function (type) {
            /// <summary>
            /// Xuat danh sach ra file file
            /// </summary>
            //<param name="fileType" type="string">Loai file xuat (EXCELL, WORD)</param>

            var seletedModels = this.model.select(function (doc) { return doc.get("Selected") == true; });
            var selectedIds = _.map(seletedModels, function (m) {
                return m.get('DocumentCopyId');
            });
            var data = {
                id: this.data.id,
                docCopyIds: JSON.stringify(selectedIds),
                paramsQuery: this.data.paramsQuery
            };

            require(['filedownload'], function () {
                var link = type == 1 ? '/Home/ExportDocumentListToExcel/' :
                           type == 2 ? '/Home/ExportDocumentListToWord/' :
                            '/Home/ExportDocumentListToXML';
                $.fileDownload(link, {
                    data: data,
                    successCallback: function () {
                        that._hideProcessing();
                    },
                    failCallback: function (response) {
                        var html = $(response);
                        try {
                            var json = JSON.parse(html.text());
                            egov.pubsub.publish(egov.events.status.error, json.error);
                        } catch (e) {
                            egov.pubsub.publish(egov.events.status.error, egov.resources.file.errorDownload);
                        }
                    }
                });
            });
        },

        selectedNextCurrent: function (callback) {
            // Hàm này dùng cho các chức năng xóa, kết thúc văn bản đi 
            // Mục đích cảu hàm này là khi lại bỏ văn bản trên danh sách nếu còn văn bản kế tiếp thì sẽ selected văn bản kế tiếp đo
            // Nếu không thì sẽ seleced văn bản kế trước đó

            if (!this.model || this.model.length <= 0) {
                return;
            }

            //Lấy danh sách văn bản được chọn
            var seletedModels = this.model.select(function (doc) { return doc.get("Selected") == true; });
            if (!seletedModels || seletedModels.length <= 0) {
                return;
            }

            var itemSelected = null;
            //Lấy phần tử cuối của danh sách được chọn và kiểm tra index của văn bản đó trên danh sách văn bản
            var last = this.model.indexOf(seletedModels[seletedModels.length - 1]);
            if (last < this.model.length - 1) {
                //Nếu không phải là văn bản cuối cùng thì phần tử được chọn là kế tiếp của last
                itemSelected = this.model.at(last + 1);
            }
            else {
                //Kiểm tra nếu  danh sách được chọn có 1 và không phải là phần tử cuối cùng thì
                //=> phần tử chọn là phần tử trước last
                if (last > 0 && last == this.model.length - 1 && seletedModels.length === 1) {
                    if (this.model.at(last - 1).get("Selected") == false) {
                        itemSelected = this.model.at(last - 1);
                    }
                }

                if (!itemSelected) {
                    //Kiểm tra phần tử đầu của danh sách chọn
                    var first = this.model.indexOf(seletedModels[0]);
                    if (first == 0) {
                        //Nếu là phần tử đầu tiên thì select các phần tử không được chọn
                        //=> chọn kế tiếp là phần tử đầu tiên cua danh sách không được chọn
                        var notSelected = this.model.select(function (doc) { return doc.get("Selected") == false; });
                        if (notSelected && notSelected.length > 0) {
                            itemSelected = notSelected[0];
                        } else {
                            itemSelected == this.model.at(0);
                        }
                    }
                    else {
                        //=>phần tử chọn là phần tử kế tiếp
                        itemSelected = this.model.at(first - 1);
                    }
                }
            }

            //Chỗ này dùng hàm call back gọi lại để thực thi tiếp 
            //=> do phải tính toán phần tử được chọn tiếp so với phần tử hiện tại
            //   để selected sau khi thao tác xóa phần tử khỏi danh sách, tranh trường hợp selected trước rồi mà trong quá trình loại bỏ phần tử trước đó có lỗi 
            egov.callback(callback, itemSelected);
        },

        _isChoXuLyOrDuThao: function (documentCopyIds) {
            ///<summary>
            ///Kiểm tra danh sách văn có phải là văn bản chờ xử lý hoặc dự thảo
            ///</summary>
            ///<param name="documentCopyIds">Danh sách id văn bản</param>
            if ((!documentCopyIds || documentCopyIds.length <= 0)
                || (!this.model || this.model.length <= 0))
                return false;

            if (typeof documentCopyIds !== "object" && !(documentCopyIds instanceof Array)) {
                documentCopyIds = [documentCopyIds];
            }

            var modelSelecteds = this.model.select(function (doc) {
                return _.contains(documentCopyIds, doc.get("DocumentCopyId"));
            });

            for (var i = 0; i < modelSelecteds.length; i++) {
                if (modelSelecteds[i].view.isChoXuLyOrDuthao() == false) {
                    return false;
                }
            }

            return true;
        },
    });

    //#endregion

    //#region Document Item View

    /// <summary>Đối tượng View thể hiện 1 row trong danh sách văn bản</summary>
    var DocumentItem = Backbone.View.extend({
        tagName: egov.isMobile ? 'li' : 'tr',

        // Class đánh dấu document đang được select
        selectedClass: 'rowSelected',
        documentImportantClass: 'document-important',
        importantClass: 'important',
        unreadClass: 'documentUnread',
        unescapeList: ["Compendium"],//chứa danh sách các attributes trong model được unescape

        // Danh sách các sự kiện khi click vào 1 row
        events: {
            //   'click .document-important': 'setImportant',     // Đặt trạng thái quan trọng cho hồ sơ
            'contextmenu': 'contextmenu',                         // Chuột phải
            'click': 'selected',
            'dblclick': 'dbclick',
            // Mobile
            'tap': 'openDocument',
            'tap .checkbox': 'selected',
            'taphold': "openMultiSelectFunction"
        },

        initialize: function (options) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="options"></param>
            var that = this;
            this.parent = options.parent;
            this.columnConfig = options.config;
            if (this.unescapeList && this.unescapeList.length > 0) {
                for (var i = 0; i < this.unescapeList.length; i++) {
                    this.model.set(this.unescapeList[i], unescape(this.model.get(this.unescapeList[i])));
                }
            }

            this._eventOfProps();

            this.render();

            this._rebindOverdueAndAppointed();

            this.model.view = this;

            return this;
        },

        _eventOfProps: function () {
            var that = this;

            //todo:Tạm bỏ xử lý văn bản quan trọng
            //Trigger văn bản quan trọng
            //this.model.on('change:IsDocumentImportant', function () {
            //    var starClass = '.' + that.documentImportantClass,
            //        important = this.get('IsDocumentImportant');
            //    that.$el.find(starClass).
            //        toggleClass(that.importantClass)
            //        .attr('title', important == true
            //        ? "Bỏ gán quan trọng văn bản này"
            //        : "Gán quan trọng văn bản này");

            //    updateImportant(this.get('DocumentCopyId'), important);
            //    that.parent._insertOrUpdateLocalStorage();
            //});

            this.model.on("change", function () {
                //try {
                //    debugger
                //    that.parent.tree.autoGetDocumentNews();
                //} catch (ex) {console.log(ex);}
            });

            //Trigger trạng thái xem văn bản
            this.model.on('change:IsViewed', function (model, isViewed) {
                if (isViewed) {
                    that.$el.removeClass(that.unreadClass);
                }
                else {
                    that.$el.addClass(that.unreadClass);
                }
            });

            //Trigger selected văn bản
            this.model.on('change:Selected', function (model, selected) {
                if (selected) {
                    that.$el.addClass(that.selectedClass);
                }
                else {
                    that.$el.removeClass(that.selectedClass);
                }

                //try {
                //    debugger
                //    that.parent.tree.autoGetDocumentNews();
                //} catch (ex) { console.log(ex); }

                if (egov.isMobile && egov.views.home.documents.isMultiSelecting) {
                    if (selected) {
                        that.$el.addClass("showMultiselectCheckBox");
                        that.$el.find('.checkbox').addClass('checked');
                    }
                    else {
                        that.$el.removeClass("showMultiselectCheckBox");
                        that.$el.find('.checkbox').removeClass('checked');
                    }
                    var numberSelected = 0;
                    var selectedList = that.parent.model.filter(function (item) {
                        return item.get("Selected") == true
                    });
                    if (selectedList && selectedList.length > 0) {
                        numberSelected = selectedList.length;
                    }
                    if (numberSelected == 0) {
                        egov.views.home.multiselectionbar.hideSelectionBar();
                        return;
                    }
                    egov.views.home.multiselectionbar.add(model, selected, numberSelected + "/" + that.parent.model.length);
                }
                else {
                    that.$el.find('.checkbox').toggleClass('checked');
                    that.$el.find('.checkbox input[type="checkbox"]').prop('checked', selected ? true : false);
                }

            });
        },

        _rebindOverdueAndAppointed: function () {
            this._setColor();
            if (!egov.isMobile) {
                return;
            }
            var status = this.model.get("Status");

            //Định dạng lại cột thời hạn giữ
            var dateOverdure = this.model.get('DateOverdue');

            //Định dạng lại cột thời hạn hạn tổng
            var dateAppointed = this.model.get('DateAppointed');

            if (status != 2 && dateOverdure != '') {
                var dateOver = egov.commonFn.util.getCustomTime(dateAppointed, true);
                this.model.set('MobileDate', dateOver);
            }

        },

        render: function () {
            if (egov.isMobile)
                this.renderMobile();
            else {
                this.renderDefault();
            }
        },

        renderDefault: function () {
            var that = this;
            var color = this.model.get('Color');
            var cssClass = "document-color-" + color;
            var id = "";
            if (this.parent.isQuestionTree) {
                id = "question-" + this.model.get('QuestionId')
            }
            else {
                id = 'documentCopy' + this.model.get('DocumentCopyId')
            }
            this.$el.attr('id', id).addClass(cssClass);

            if (!this.model.get('IsViewed')) {
                this.$el.addClass('documentUnread');
            }

            // Văn bản trung ương
            var organization = this.model.get("Organization");
            var docCode = this.model.get("DocCode");
            var isVbtw = (docCode && docCode.indexOf("VPCP") >= 0)
                            || (organization && (organization.equals("văn phòng chính phủ", true) || organization.startWith("Bộ")));
            if (isVbtw) {
                this.$el.addClass("danger");
            }

            // thêm cột checkbox
            //var checkbox = '<label class="checkbox document-color qtooltip" title="{0}">\
            //                    <input name="checkbox[]" value="1" type="checkbox"><span class="select-row document-color-{1}"><i class="icon-check"></i></span></label>';
            //var checkbox = '<div class="icon-color document-color document-color-{1}"></div>';
            //this.$el.append(checkbox);

            var iconBox = $("<td>");
            iconBox.append('<div class="icon-color"></div>');
            this.$el.append(iconBox);
            iconBox.etip();

            //todo:tạm thời bỏ(không được xóa)
            // Hiển thị văn bản quan trọng
            //var important = $('<div>').addClass('icon-flag document-important qtooltip');
            //if (this.model.get('IsDocumentImportant') == false) {
            //    important.attr('title', egov.resources.documents.title.documentNotImportant);
            //}
            //else {
            //    important.addClass('important').attr('title', egov.resources.documents.title.documentImportant);
            //}
            //important.etip();

            //Todo: tạm thời bỏ
            // thêm cột văn bản quan trọng
            //var importantCol = $("<td style=''></td>");
            //importantCol.append(important);
            //this.$el.append(importantCol);

            // Hiển thị các cột theo cấu hình
            $.each(this.columnConfig, function (index, setting) {
                var data = that.model.get(setting.ColumnName);
                var tdElement = $("<td>");
                if (regexUtcDate.test(data) || (data instanceof Date)) {
                    tdElement.text(Globalize.format(Globalize.parseDate(data), "dd/MM/yyyy HH:mm:ss"));
                }
                else {
                    tdElement.text(data ? data : '');
                }
                tdElement.attr("data-column", setting.ColumnName);
                that.$el.append(tdElement);
            });
        },

        renderMobile: function () {
            var that = this;
            this.$el.addClass('mdl-list__item mdl-list__item--two-line').attr('id', 'documentCopy_' + this.model.get('DocumentCopyId'));
            this.$el.addClass(!this.model.get('IsViewed') ? this.unreadClass : '');
            require([egov.template.documentList.mobileDocumentItem],
                function (MobileTemplate) {
                    if (that.parent.isOnlineRegistration) {
                        that.model.set("UserCurrentFullName", that.model.get("FullName"));
                    }
                    else if (that.parent.isQuestionTree) {
                        that.model.set("UserCurrentFullName", that.model.get("AskPeople"));
                        that.model.set("Compendium", that.model.get("Name"));
                        that.model.set("UserAvatar", String.format(egov.setting.avatarPath, that.model.get("Email")));
                    }
                    else {
                        var userSendId = that.model.get("UserSendId");
                        var currentUsername = that.model.get("UserCurrentFullName");
                        var currentFirstName = that.model.get("UserCurrentFirstName");
                        var avatar = '../AvatarProfile/noavatar.jpg';

                        if (userSendId != undefined) {
                            var userSendUser = _.find(egov.views.home.documents.allUsers, function (user) {
                                return user.value == userSendId;
                            });

                            if (userSendUser == undefined) {
                                currentUsername = username;
                                currentFirstName = username
                            } else {
                                currentUsername = userSendUser.username;
                                currentFirstName = userSendUser.fullname;

                                avatar = userSendUser.avatar;
                            }

                        } else {
                            var currentUser = _.find(egov.views.home.documents.allUsers, function (user) {
                                return user.value == that.model.get("UserCurrentId");
                            });
                            if (currentUser == undefined) {
                                currentUsername = username;
                                currentFirstName = username
                            } else {
                                currentUsername = currentUser.username;
                                currentFirstName = currentUser.fullname;
                                avatar = currentUser.avatar;
                            }
                        }

                        that.model.set("UserCurrentFullName", currentFirstName);
                        that.model.set("UserAvatar", avatar);
                    }

                    var objectDocument = that.model.toJSON();

                    // Định dạng lại các giá trị datetime của đối tượng documentitem
                    Object.getOwnPropertyNames(objectDocument).forEach(function (val, idx, array) {
                        var objItem = objectDocument[val];
                        if (regexUtcDate.test(objItem)) {
                            objItem = Globalize.format(Globalize.parseDate(objItem), "dd/MM/yyyy HH:mm");
                        }
                        objectDocument[val] = objItem;
                    });

                    //Bind dữ liệu ra template
                    that.$el.html($.tmpl(MobileTemplate, objectDocument));
                    that._setColor();
                }
            );

            // Hiển thị văn bản quan trọng
            var important = this.$el.find('.document-important');
            if (this.model.get('IsDocumentImportant') == true) {
                important.attr('title', egov.resources.documents.title.documentNotImportant);
            } else {
                important.addClass('important').attr('title', egov.resources.documents.title.documentImportant);
            }

            important.etip();
        },

        // Set trạng thái đang được chọn, hiển thị preview ( nếu được cấu hình )
        selected: function (e) {
            egov.helper.hideAllContext();
            var isMultiSelect = false;

            if (!e) {
                this.parent.removeAllSelected();
                this.setSelected();
            }
            else {
                egov.helper.destroyClickEvent(e);

                if (e.shiftKey) {
                    this.parent.setSelected(this.model);
                }
                else {
                    isMultiSelect = e.ctrlKey
                                    || (egov.isMobile
                                    ? egov.views.home.documents.isMultiSelecting
                                    : $(e.target).parents('.checkbox').length > 0);
                    if (isMultiSelect) {
                        if (this.$el.hasClass(this.selectedClass) || this.$el.find(".chkCheck .checkbox").hasClass("checked")) {
                            this.unSetSelected();
                        } else {
                            this.setSelected();
                        }
                    } else {
                        this.parent.removeAllSelected();
                        this.setSelected();
                    }
                }
            }

            if (!egov.isMobile && egov.setting.user.userSetting.quickView !== egov.enum.quickViewType.hide) {
                this.showQuickView();
            }

            if (isMultiSelect || (e !== undefined && $(e.target).is('.select-row'))) {
                e.stopPropagation();
            }
        },

        selectedRow: function (e) {
            egov.helper.hideAllContext();
            if (!e) {
                this.parent.removeAllSelected();
                this.setSelected();
                return;
            }

            if (e.shiftKey) {
                this.parent.setSelected(this.model);
                return;
            }

            var isMultiSelect = e.ctrlKey || (egov.isMobile
                               ? $(e.target).is('.select-row')
                               : $(e.target).parents('.checkbox').length > 0);

            if (isMultiSelect) {
                if (this.$el.hasClass(this.selectedClass)) {
                    this.unSetSelected();
                } else {
                    this.setSelected();
                }
            } else {
                this.parent.removeAllSelected();
                this.setSelected();

            }
        },

        dbclick: function () {
            this.parent.hideAllContextmenu();
            if (this.model.get("IsFile")) {
                this.openFile();
            } else {
                this.openDocument();
            }
        },

        // Set văn bản hiện tại về trạng thái đang được chọn
        setSelected: function () {
            this.model.set('Selected', true);
            this.draggableSelected();
            //Chưa row cuối cùng được click
            this.parent.lastSelected = this.model;
        },

        // Bỏ set đang chọn cho văn bản hiện tại
        unSetSelected: function () {
            this.model.set('Selected', false);
            this.parent.lastSelected = this.model;
        },

        toggleSelected: function () {
            if (this.model.get("Selected")) {
                this.unSetSelected();
            } else {
                this.setSelected();
            }
        },

        //Sự kiện kéo văn bản
        draggableSelected: function () {
            var selected = this.model.view.$el;
            var id = selected.attr('id').replace("documentCopy", "");
            var selectedDraggable = selected.find("td[data-column='CitizenName'], td[data-column='Compendium']")

            selectedDraggable.draggable({
                helper: "clone",
                appendTo: "body",
                zIndex: 100,
                cursor: 'move',
                start: function (event, ui) {
                    egov.draggingDocumentCopyId = id;
                },
                stop: function (event, ui) {
                }
            });
        },

        // Mở văn bản được chọn
        openDocument: function () {
            egov.helper.hideAllContext();

            if (egov.mobile) {
                if (egov.views.home.documents.isMultiSelecting) {
                    this.toggleSelected();
                    return;
                }
            }

            if (this.parent.isQuestionTree) {
                this._openQuestion();
                return;
            }

            if (this.parent.isOnlineRegistration) {
                this._openOnlineRegistration();
                return;
            }

            var isDocumentOnline = this.model.get('IsDocumentOnline') == 1;

            if (isDocumentOnline) {
                this._openDocumentOnLine();
                return;
            }

            if (egov.mobile) {
                this._openDocInMobile();
                return;
            }

            this._openDocument();
        },

        openMultiSelectFunction: function (e) {
            if (!egov.views.home.documents.isMultiSelecting) {
                var that = this;
                if (!egov.views.home.multiselectionbar) {
                    require(["multiselection"], function (MultiBar) {
                        egov.views.home.multiselectionbar = new MultiBar({
                            documentList: that.parent
                        });
                    });
                }
                else {
                    egov.views.home.multiselectionbar.showMultiSelectionBar();
                }

                egov.views.home.documents.removeAllSelected();
                egov.views.home.documents.isMultiSelecting = true;
            }
        },

        openFile: function () {
            openAttachment(this.model.get('DocumentCopyId'));
        },

        // Set trạng thái quan trọng cho văn bản.
        setImportant: function (e) {
            var important = this.model.get('IsDocumentImportant');
            this.model.set('IsDocumentImportant', !important);
            egov.helper.destroyClickEvent(e);
        },

        // Hiển thị thông tin xem nhanh
        showQuickView: function (e) {
            egov.helper.destroyClickEvent(e);
            //dang ky qua mang
            if (this.parent.isOnlineRegistration) {
                this.parent.reQuickView(this.model.get("Id"));
                return;
            } else if (this.parent.isQuestionTree) {
                this.parent._reQuickViewQuestionTree(this.model);
                return;
            }
            this.parent.reQuickView(this.model.get("DocumentCopyId"));
        },

        finish: function (e) {
            egov.helper.destroyClickEvent(e);
            this.parent.finishs([this.model]);
        },

        // Chuột phải
        contextmenu: function (e) {
            egov.helper.destroyClickEvent(e);

            if (egov.isMobile || this.parent.isOnlineRegistration) {
                return;
            }

            if (!this.model.get('Selected')) {
                this.parent.removeAllSelected();
            }

            this.setSelected();
            this.parent.contextmenu(e, this);
        },

        _openDocInMobile: function () {
            var documentCopyId = this.model.get('DocumentCopyId'),
                compendium = this.model.get('Compendium'),
                that = this;

            if (egov.mobile.isTablet) {
                $(".dataDetail").removeClass("display");
            }
            this.parent.removeAllSelected();

            require(['tabView'], function (TabView) {
                var tabDocument = new TabView({
                    model: {
                        id: documentCopyId,
                        name: compendium,
                        title: compendium,
                        href: 'tabDocument_' + documentCopyId,
                        documentInfo: that.model
                    }
                });

                remoteOnNotify(documentCopyId);
                that.setSelected();

                if (that.model.get('IsViewed') == false) {
                    that.$el.removeClass("unread");
                    that.parent.setViewed([that.model], true);
                }
            });
        },

        _openDocument: function () {
            var that = this;
            var documentCopyId = this.model.get('DocumentCopyId');

            if (this.parent.isStoreTree) {
                that.model.set("StorePrivateId", this.parent.nodeId);
            }

            egov.views.home.tab.openDocumentNew(
               that.model,
               function () {
                   remoteOnNotify(documentCopyId);

                   if (that._hasUpdateViewed()) {
                       if (that.model.get('IsViewed') == false) {
                           that.parent.setViewed([that.model], true);
                       }
                   }
               }
           );
        },

        _openDocumentOnLine: function () {
            var that = this;
            var documentOnlineId = this.model.get('Status') === 2 ? this.model.get('DocumentCopyId') : this.model.get('Id');
            egov.views.home.tab.openDocumentOnline(this.model.get('Id'),
                this.model.get('DocTypeName') + " - " + this.model.get('PersonInfo'),
                true,
                function () {
                    //Kiểm tra xem nếu không phải là văn bản theo dõi thì được phép cập nhật
                    //Kiểm tra xem nếu là văn bản theo dõi thì khoong được phép cập nhật
                    if (that.model.get('UserCurrentId') !== egov.setting.userId
                        && (that.model.get('Status') === 2
                        || that.model.get('Status') === 16)) {
                        return;
                    }

                    if (that.model.get('IsViewed') == false) {
                        that.parent.setViewedInCache([that.model], true);
                    }
                }
            );
        },

        _openOnlineRegistration: function () {
            egov.views.home.tab.openDocumentOnline(this.model.get('Id'),
               this.model.get('DocCode'), true, null, false, true);
        },

        _openQuestion: function () {
            egov.views.home.tab.openQuestion(this.model)
        },

        _isChoXuLy: function () {
            var status = this.model.get('Status');
            var currentUserId = this.model.get('UserCurrentId');
            var documentCopyType = this.model.get('DocumentCopyType');

            if (status === 2
                 && currentUserId === egov.setting.userId
                 && _.contains([1, 2, 4, 32, 64], documentCopyType)) {
                return true;
            }


            var nodeModel = this.parent.node.model;
            if (nodeModel.get('hasUyQuyen')) {
                if (status === 2
                    && _.contains(nodeModel.get('userUyQuyen'), currentUserId)
                    && _.contains([1, 2, 4, 32, 64], documentCopyType)) {
                    return true;
                }
            }

            return false;
        },

        _hasUpdateViewed: function () {
            // Cấu hình câu truy vấn trong Node chờ xử lý:
            // Thêm select 1 as hasUpdateViewed để xác định trường này cho nhanh.
            return this.model.get("hasUpdateViewed") == 1;
        },

        _setColor: function () {
            var color = getColor(this.model.get("UrgentId"),
                                    this.model.get('DateResponse'),
                                    this.model.get('DateOverdue'),
                                    this.model.get('DateAppointed'),
                                    this.model.get('DocumentCopyType'),
                                    this.model.get('IsSupplemented'),
                                    this.model.get('Status'),
                                    this.model.get('IsSuccess'),
                                    this.model.get("IsReturned"),
                                    this.model.get('IsGettingOut'),
                                    this.model.get('Original'));

            var categoryBussiness = this.model.get("CategoryBusinessId");

            var colorElement = $("<span>");

            this.model.set("Color", color);
            var cssClass = "document-color label ";
            var colorText = '';

            var title = "";
            switch (color) {
                case 2:
                    cssClass += "label-success";
                    colorText += "Đồng XL";
                    title += "Văn bản đồng xử lý, xin ý kiến"
                    break;
                case 3:
                    cssClass += "label-warning";
                    colorText = "Tới Hạn";
                    title += "Văn bản tới hạn";
                    break;
                case 4:
                    cssClass += "label-danger";
                    colorText = "Quá Hạn";
                    title += "Văn bản quá hạn";
                    break;
                case 5:
                    cssClass += "label-warning";
                    colorText = "Khẩn";
                    title += "Văn bản khẩn";
                    break;
                case 6:
                    cssClass += "label-danger";
                    colorText = "Hỏa tốc";
                    title += "Văn bản Hỏa tốc";
                    break;
                case 7:
                    cssClass += "label-warning";
                    colorText = "Bổ sung";
                    title += "Hồ sơ chờ bổ sung";
                    break;
                case 8:
                    cssClass += "label-primary";
                    colorText = "Liên Thông";
                    title += "Hồ sơ / Văn bản đang liên thông";
                    break;
                case 9:
                    cssClass += "label-default";
                    colorText = "Đã Duyệt";
                    title += "Văn bản đã ký duyệt";
                    break;
                default:
                    break;
            }

            if (colorText === "") {
                return;
            }

            colorElement.addClass(cssClass);
            colorElement.text(colorText);
            colorElement.attr("title", title);

            var columnAppend;
            if (egov.isMobile) {
                columnAppend = this.$el.find(".document-tags");
                columnAppend.prepend(colorElement);
                return;
            }

            columnAppend = this.$el.find("td[data-column='Compendium']");
            if (categoryBussiness == 4) {
                columnAppend = this.$el.find("td[data-column='CitizenName']");
            }
            columnAppend.prepend(colorElement);
            colorElement.etip();
        },

        isChoXuLyOrDuthao: function () {
            if ((this.model.get('Status') === 2 || this.model.get('Status') === 1 || this.model.get('Status') === 16)
                 && this.model.get('UserCurrentId') === egov.userId
                 && _.contains([1, 2, 4, 32, 64], this.model.get('DocumentCopyType'))) {
                return true;
            }

            var nodeModel = this.parent.node.model;
            if (nodeModel.get('hasUyQuyen')) {
                if ((this.model.get('Status') === 2 || this.model.get('Status') === 1)
                    && _.contains(nodeModel.get('userUyQuyen'), this.model.get('UserCurrentId'))
                    && _.contains([1, 2, 4, 32, 64], this.model.get('DocumentCopyType'))) {
                    return true;
                }
            }

            return false;
        },
    });

    //#endregion

    //#region Grid Header

    ///Model tao header cho danh sach van ban
    var gridHeaderModel = Backbone.Model.extend({
        defaults: {
            text: '',
            value: '',
            selected: false,
            isDesc: true,
            enableSort: false,
            callback: null
        },
        initialize: function () { }
    });

    ///Tao header cho danh sach van ban
    var gridHeaderCollection = Backbone.Collection.extend({
        model: gridHeaderModel,
        initialize: function () { }
    });

    /// <summary>Đối tượng View thể hiện header danh sách văn bản</summary>
    var GridHeader = Backbone.View.extend({
        tagName: "tr",

        initialize: function (options) {
            if (!(options.model instanceof gridHeaderCollection)) {
                options.model = new gridHeaderCollection(options.model);
            }

            this.model = options.model;
            this.parent = options.parent;
            this.render();

            return this;
        },

        render: function () {
            var that = this;
            this.sortting = false;
            this.itemSelected = null;
            this.model.each(function (gridModel) {
                var item = new GridHeaderItem({
                    model: gridModel,
                    parent: that,
                    callback: function () {
                        that.sortting = false;
                    }
                });
                that.$el.append(item.$el);
            });
        },

        removeSelected: function () {
            this.$el.find('a').removeClass('asc desc');
        }
    });

    /// <summary>Đối tượng View thể hiện item cho  header danh sách văn bản</summary>
    var GridHeaderItem = Backbone.View.extend({
        tagName: "th",

        events: {
            'click': 'selected'
        },

        initialize: function (options) {
            this.model = options.model;
            this.parent = options.parent;
            this.callback = options.callback;
            this.render();
        },

        render: function () {
            ///<summary>
            /// Render ra giao diện
            ///</summary>

            this.$el.attr('data-column', this.model.get('value'));

            if (this.model.get('enableSort') == false) {
                if (this.model.get('value') === 'STT') {
                    this.$el.append('<div class="icon-color document-color"></div>');
                } else if (this.model.get('value') === 'Color') {
                    this.$el.append('<div class="icon-flag"></div>');
                } else {
                    this.$el.append(this.model.get('text'));
                }
            } else {
                this.$el.append('<a href="#" data-column="' + this.model.get('value') + '" class="sort">' + this.model.get('text') + '</a>');
            }
        },

        selected: function (e) {
            ///<summary>
            /// Select để sắp xếp
            ///</summary>

            egov.helper.destroyClickEvent(e);

            if (this.parent.sortting || this.model.get('enableSort') == false) {
                //that._hideProcessing();
                return;
            }

            this.parent.sortting = true;

            if (typeof this.model.get('callback') === 'function') {
                this.parent.removeSelected();
                var $a = this.$el.find('a.sort');
                if (this.model.get('isDesc') == true) {
                    this.model.set('isDesc', false);
                    $a.removeClass('asc desc').addClass('asc');
                } else {
                    this.model.set('isDesc', true);
                    $a.removeClass('asc desc').addClass('desc');
                }

                this.model.get('callback')(this.model.toJSON());
            }

            egov.callback(this.callback);

            //this._hideProcessing();
        }
    });

    //#endregion

    //#region Privates

    function renderGridHeader(settings, container) {
        /// <summary>Render ra header cho danh sách document theo cấu hình</summary>
        /// <param name="settings" type="object">Cau hinh</param>
        /// <param name="container" type="object">element jquery</param>
        container.find("colgroup").empty();
        $.each(settings, function (index, setting) {
            if (setting.Width && setting.Width > 0) {
                container.find("colgroup").append('<col style="width: ' + setting.Width + 'px"/>');
            } else {
                container.find("colgroup").append('<col/>');
            }
        });
    }

    function updateImportant(documentCopyId, important) {
        /// <summary>Set tính quan trọng cho văn bản</summary>
        /// <param name="documentCopyId" type="int">Id document copy.</param>
        /// <param name="important" type="bool">Xác định văn bản có quan trọng hay không.</param>
        egov.request.setDocumentImportant({
            data: {
                documentCopyId: documentCopyId,
                isImportant: important
            }
        });
    }

    function updateViewed(documentCopyId, viewed, callbackSuccess, callbackError) {
        /// <summary>
        ///  Set trạng thái xem cho văn bản
        ///</summary>
        /// <param name="documentCopyId" type="int">Id document copy.</param>
        /// <param name="viewed" type="bool">Xác định trang thái văn bản.</param>
        /// <param name="callbackSuccess" type="function">Ham thuc thi khi thanh cong</param>
        /// <param name="callbackError" type="function">Ham thuc thi khi loi</param>
        viewed = viewed == true;
        egov.request.setDocumentViewed(
           {
               data: { id: documentCopyId, viewed: viewed },
               success: function (result) {
                   if (!result) {
                       return;
                   }

                   if (result.error) {
                       egov.pubsub.publish(egov.events.status.error, result.error);
                       egov.callback(callbackError);
                   } else {
                       egov.callback(callbackSuccess);
                   }
               },
               error: function () {
                   egov.callback(callbackError);
               }
           }
        );
    }

    function finish(documentCopyId, storeId, comment, isThongBao, callbackSuccess, callbackError) {
        /// <summary>
        ///  Ket thuc văn bản
        ///</summary>
        /// <param name="documentCopyId" type="int">Id document copy.</param>
        /// <param name="callbackSuccess" type="function">Ham thuc thi khi thanh cong</param>
        /// <param name="callbackError" type="function">Ham thuc thi khi loi</param>
        egov.request.finish(
          {
              data: {
                  documentCopyId: documentCopyId,
                  storePrivateId: storeId,
                  comment: comment,
                  isThongBao: isThongBao
              },
              success: function (result) {
                  if (result.error) {
                      egov.pubsub.publish(egov.events.status.error, egov.resources.document.finish.error);

                      egov.callback(callbackError);
                  } else {
                      egov.callback(callbackSuccess);
                  }
              },
              error: function () {
                  egov.callback(callbackError);
              }
          }
        );
    }

    function renderQuickView(template, obj, callback) {
        /// <summary>Hiển thị quickview</summary>
        /// <param name="template" type="string">template hiển thị</param>
        /// <param name="obj" type="object">Object pare vào template</param>
        /// <param name="callback" type="function">Hàm Callback khi bind ra giao diện</param>
        require([template], function (QuickView) {
            $(".document-preview").html($.tmpl(QuickView, obj));
            $(".document-preview").bindResources();
            egov.callback(callback);
        });
    }

    function remoteOnNotify(documentCopyId) {
        try {
            var eGovNotify;
            if (egov.isMobile) {
                eGovNotify = egov.mobile.notification.eGovNotify;
            } else {
                eGovNotify = window.parent.eGovNotify;
            }
            eGovNotify.removeModelById(documentCopyId, true);
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    ///So sánh với ngày hiện tại
    function compareDateNow(date) {
        if (String.isNullOrEmpty(date)) {
            return null;
        }

        date = new Date(date);
        var dateNow = new Date();
        var diff = ((date.getTime() - dateNow.getTime()) / 1000);
        var day_diff = Math.ceil(diff / (60 * 60 * 24)); //Làm tròn lên

        return day_diff;
    }

    function search(str, searchTerm) {
        /// <summary>Tìm kiếm trong chuỗi</summary>
        ///<param name="value" type="string"> Chuỗi so sanh để tìm</param>
        ///<param name="value2" type="string">Chuỗi muốn tìm</param>
        var tmp = str.toLowerCase();
        searchTerm = searchTerm.toLowerCase();
        if (tmp.contains(searchTerm)) {
            return true;
        }

        return false;
    }

    function getQuickViewTemplate() {
        /// <summary>
        /// Trả về template cho phần xem trước văn bản.
        /// </summary>
        /// <returns type=""></returns>
        if (egov.enum.quickViewType.below === egov.setting.user.userSetting.quickView) {
            return egov.template.documentList.quickViewBelow;
        }
        return egov.template.documentList.quickViewRight;
    }

    function downloadAttachment(id) {
        /// <summary>
        /// Tải về file đính kèm.
        /// </summary>
        /// <param name="id">Id của file cần tải</param>
        egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

        require(['filedownload'], function () {
            var link = '/StorePrivate/DownloadAttachment/' + id;
            $.fileDownload(link, {
                successCallback: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                },
                failCallback: function (response) {
                    var html = $(response);
                    try {
                        var json = JSON.parse(html.text());
                        egov.pubsub.publish(egov.events.status.error, json.error);
                    } catch (e) {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.file.errorDownload);
                    }
                }
            });
        });
    };

    function openAttachment(id) {

        if (Plugin) {
            window.Plugin.appendPlugin(function () {
                Plugin.openStoreAttachment(id);
            });
        }
    }

    function removeAttachment(fileId, callback) {
        egov.request.storePrivateRemoveFile({
            data: {
                id: fileId
            },
            success: function (result) {
                if (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                    } else {
                        egov.callback(callback);
                    }
                }
            },
            error: function () {
                egov.pubsub.publish(egov.events.status.error, _resource.errorDownload);
            },
            complete: function () {
                egov.pubsub.publish(egov.events.status.destroy);
            }
        });
    }

    function getColor(ugentId, dateResponse, dateOverdue, dateAppointed, documentCopyType,
                        isSupplemented, status, isSuccess, isReturned, isGettingOut, original) {
        /// <summary>
        /// Trả về màu sắc của văn bản
        /// </summary>
        /// <returns type="int">
        /// - 1: Trạng thái mặc định, Văn bản trong hạn, đã kết thúc, đang xử lý, đã duyệt, đã trả kết quả, không có hạn xử lý,...
        /// - 2: Hồ sơ/Văn bản đồng xử lý, xin ý kiến.
        /// - 3: Hồ sơ/Văn bản tới hạn
        /// - 4: Hồ sơ/Văn bản quá hạn xử lý.
        /// - 5: Văn bản Khẩn
        /// - 6: Văn bản thượng khẩn.
        /// - 7: Hồ sơ chờ bổ sung.
        /// - 8: Hồ sơ đang liên thông.
        /// - 9: Hồ sơ/Văn bản đã duyệt
        /// </returns>
        if (status == 4 || status == 1 || status == 8 || isReturned || !dateAppointed) {
            if (documentCopyType == 2 || documentCopyType == 4) {
                return 2;
            }

            if (original === 2) {
                return 8;
            }

            return 1; // Bình thường: không hiển thị màu
        }

        if (isSuccess && !isReturned) {
            return 9;
        }

        if (ugentId == 3) {
            return 6; // Thượng khẩn
        }

        if (ugentId == 2) {
            return 5; // Khẩn
        }

        var dayOverDue = compareDateNow(dateOverdue);
        var dayAppointed = compareDateNow(dateAppointed);
        var dayResponse = compareDateNow(dateResponse);

        if (status == 16) {
            if (isSupplemented != null) {
                return 7;
            }

            if (isGettingOut) {
                return 8;
            }

            return 1;
        }

        if (dayAppointed <= 0) {
            return 4; //văn bản quá hạn
        }

        if (dayAppointed <= 2 && dayAppointed > 0) {
            //dayOverDue <= 1 && dayOverDue > 0 tienbv sửa về nếu hạn trong ngày thì mới báo sắp hết hạn
            //văn bản sắp tới han;
            return 3;
        }

        if (documentCopyType == 2 || documentCopyType == 4) {
            return 2;
        }

        if (original === 2) {
            return 8;
        }

        return 1;
    }

    //#endregion

    return DocumentsView;
});