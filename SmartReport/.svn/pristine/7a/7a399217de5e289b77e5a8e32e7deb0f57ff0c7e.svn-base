define([
    'storeTree',
    'documentsView',
    'treeBase',
    'contextMenuView'
],

function (StoreTree, DocumentsView, eGovTree) {
    "use strict";

    //#region View

    /// <summary>Đối tượng view quản lý các tree: document tree, store tree, plugin tree</summary>
    var Tree = Backbone.View.extend({
        // Dom element
        el: "#menu-document",

        documentDetail: '#document-detail',
        documentList: '.document-list',
        $documentListId: $('#documentList'),
        nameGroupDocument: '#nameGroupDocument',
        //  rootChildren: '#child0',
        rootChildren: '#menu-document',
        totalUnread: '.totalunread',
        systemTree: '.system-tree',

        rootId: 0,
        isShowDocumentList: false,

        hasFocusHome: true,// có focus về tab home hay không
        autogetNew: null,

        initialize: function () {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            egov.views.home.tree = this;
            this.isLoadedStoreTree = false;

            this.render();
        },

        render: function () {
            /// <summary>
            /// Bind tree
            /// </summary>

            this._registertGlobalApi();
            this._initInterval();

            this.processFunctions = this.parseFunctions(egov.setting.processFunction);

            this.renderDocumentTree();
            //this.renderStoreTree();
            
            return this;
        },

        parseFunctions: function (allFunctions) {
            var result = [];
            _.each(allFunctions, function (func) {
                if (func.parentId === 0) {
                    var children = _.filter(allFunctions, function (child) {
                        return child.parentId == func.id;
                    });
                    func.children = children;
                    result.push(func);
                }
            });

            return result;
        },

        reloadDocumentTree: function (nodeId) {
            /// <summary>
            /// Load lại cây văn bản
            /// Các trường hợp reload lại cây văn bản:
            /// - Load lại khi đăng nhập, hoặc reload trang.
            /// - Khi nhận được notify văn bản mới: egov.notification.js.
            /// - Khi bàn giao, xử lý văn bản: egov.documents.js reloadDocument
            ///
            /// Khi reload: reload cả các node cha và con thuộc node hiện tại.
            /// </summary>
            if (egov.isMobile) {
                return;
            }
            var that = this;
            var nodeIds, parent;

            if (!nodeId) {
                nodeIds = this.documentTree.model.pluck('functionId');
            } else {
                parent = that.documentTree.model.detect(function (n) {
                    return n.get("id") === nodeId;
                });

                var children = parent.get("children");
                children && children.length > 0 && (nodeIds = children.pluck("id"));
            }

            egov.request.getTotalDocumentUnreadMultiFunction({
                data: { ids: JSON.stringify(nodeIds) },
                success: function (result) {
                    if (!result) {
                        egov.log(egov.events.status.error, egov.resources.treeDocument.message.error.syncData);
                        return;
                    }

                    result.forEach(function (node) {
                        var child;
                        if (nodeId) {
                            parent = that.documentTree.model.detect(function (n) {
                                return n.get("id") === nodeId;
                            });

                            child = parent.get("children").detect(function (n) {
                                return n.get("id") == node.id;
                            });
                        } else {
                            child = that.documentTree.model.detect(function (n) {
                                return n.get("id") === node.id;
                            });
                        }

                        if (child) {
                            child.set('totalDocumentUnread', node.totalUnread);
                            child.set('totalDocument', node.total)
                        }
                    });
                }
            });

            //if (this.selectedNode) {
            //    this.selectedNode.model.documentsView.loadNewerDocuments();
            //}
        },

        reloadUnread: function (node, totalUnread, totalDocument, callback) {
            /// <summary>
            /// Reload lại văn bản chưa đọc của node
            /// Note: Hàm này khi sử dụng phải truyền node có kiểu thể hiện là 1 model item của cây.
            ///       và totalUnread có kiểu là number
            /// </summary>
            ///<param name='node' type='egov.models.TreeModel'>Node muốn cập nhật số văn bản chưa đọc<\param>
            ///<param name='totalUnread' type='number'>Số văn bản chưa đọc<\param>
            ///<param name='totalDocument' type='number'>Số văn bản<\param>
            ///<param name='callback' type='function'>Hàm thực thi khi thành công<\param>
            if (!node && !(node.model instanceof egov.models.TreeModel)) {
                return;
            }

            totalUnread = totalUnread || 0;
            totalDocument = totalDocument || 0;

            node.model.set('totalDocumentUnread', totalUnread);
            node.model.set('totalDocument', totalDocument);

            var parentId = node.model.get("parentId");
            if (parentId && parseInt(parentId) > 0) {
                if (totalDocument <= 0 && node.model.get("hasTransferTheoLo") == true) {
                    node.$el.hide();
                }
            }

            egov.callback(callback);
        },

        renderDocumentTree: function () {
            /// <summary>
            /// Ren ra cây văn bản.
            /// </summary>
            var that = this;

            if (!this.processFunctions || this.processFunctions.length === 0) {
                egov.pubsub.publish(egov.events.status.warning, egov.resources.treeDocument.noTreeNode);
                return;
            }

            that.model = new egov.models.TreeList(this.processFunctions);
            that._sortBy();
            that._documentTree();
        },

        _registertGlobalApi: function () {
            egov.pubsub.subscribe(egov.events.tree.reload, this.reloadDocumentTree, this);
            egov.pubsub.subscribe("tree.reloadSelected", this._reloadSelectedNode, this);
        },

        _initInterval: function () {
            if (this._interval) {
                clearInterval(this._interval);
            }

            this._interval = setInterval(function () {
                egov.pubsub.publish(egov.events.tree.reload);
                egov.pubsub.publish("tree.reloadSelected");
            }, 2 * 60 * 1000);
        },

        _sortBy: function () {
            var result = [];
            var treeGroups = this.model.groupBy(function (t) {
                return t.get("treeGroupOrder");
            });
            _.each(treeGroups, function (nodes, key) {
                nodes = _(nodes).sortBy(function (node) {
                    return node.get("order");
                });

                result.push(nodes);
            });

            this.model = new egov.models.TreeList(_.flatten(result));
        },

        removeDocuments: function (documentCopyIds) {
            // Todo: cần viết hàm này vào documents.js

            if (!documentCopyIds || documentCopyIds.length <= 0) {
                return;
            }

            if (typeof documentCopyIds !== 'object' && !(documentCopyIds instanceof Array)) {
                documentCopyIds = [documentCopyIds];
            }

            if (!this.selectedNode) {
                return;
            }

            var nodeView = this.selectedNode.model.documentsView;

            if (documentCopyIds && documentCopyIds.length > 0) {
                var command = function (itemSeleted) {
                    var selectedModels = nodeView.model.select(function (doc) {
                        return doc.get("Selected") == true
                            && !_.contains(documentCopyIds, doc.get("DocumentCopyId"));
                    })

                    if (itemSeleted && selectedModels.length == 0) {
                        itemSeleted.set("Selected", true);
                    }
                    nodeView.removeDocumentByIds(documentCopyIds);
                };

                nodeView.selectedNextCurrent(function (itemSeleted) {
                    command(itemSeleted);
                });
            }

            this._reloadSelectedNode();
            this.reloadDocumentTree();
        },

        _documentTree: function () {
            /// <summary>
            /// Hiển thị ra cây văn bản theo cấu trúc
            /// </summary>
            var that = this;

            that.documentTree = new eGovTree({
                el: that.rootChildren,
                model: that.model,
                isDocumentTree: true,
                open: function (node) {
                    that._openDocumentTreeNode(node);
                },
                select: function (node) {
                    that._renderDocumentsView(node, function () {
                        if (node.model.get("isOpen")) {
                            var children = node.model.get("children");
                            if (children && children.length > 0) {
                                // that._rebindChildrenView(node, children, true);
                            }
                        } else {
                            node.$el.find('.pull-left:first').removeClass('open closed');
                            node.$el.find('a.list-group-item:first').attr('data-open', 'false');
                            node.$('ul.collapse:first').removeClass("in");
                            if (node.model.get("totalDocument") > 0 && node.model.get("parentId") <= 0) {
                                node.model.set("isOpen", false);
                                node.$el.find('.pull-left:first').addClass("closed");
                            }
                        }

                        that.isAuto = false;
                        that.hasFocusHome = true;

                        that.documentTree.updateToCache();
                        egov.pubsub.publish(egov.events.status.destroy);
                    }, that.isAuto);
                },
                contextmenu: function (node) {
                    that._renderDocumentsView(node, function () {
                        if (node.model.get("isOpen")) {
                            var children = node.model.get("children");
                            if (children && children.length > 0) {
                                // that._rebindChildrenView(node, children, true);
                            }
                        } else {
                            node.$el.find('.pull-left:first').removeClass('open closed');
                            node.$el.find('a.list-group-item:first').attr('data-open', 'false');
                            node.$('ul.collapse:first').removeClass("in");
                            if (node.model.get("totalDocument") > 0 && node.model.get("parentId") <= 0) {
                                node.model.set("isOpen", false);
                                node.$el.find('.pull-left:first').addClass("closed");
                            }
                        }

                        that.documentTree.updateToCache();
                    });
                },
                close: function (node) {
                    that.documentTree.updateToCache();
                }
            });

            egov.pubsub.publish("tree.reloadSelected");
            egov.pubsub.publish(egov.events.tree.reload);
        },

        _renderDocumentsView: function (node, callback, isAuto) {
            ///<summary>
            /// Event khi click vào node trên cây văn bản
            ///</summary> 
            ///<param name="node" type="object"> node trên cây văn bản</param>
            ///<param name="callback" type="function"> Hàm gọi lại khi đã thực thi xong xử lý khi click node trên cân văn bản</param>
            var that = this;
            that.selectedNode = node;

            if (!egov.isMobile) {
                if (this.hasFocusHome) {
                    egov.views.home.tab.rootTab.active();
                }
            }

            if (node.model.documentsView) {
                // Nếu đã chọn thì render lại và load danh sách các document mới
                node.model.documentsView.reloadDocuments(callback, isAuto);
            } else {
                // Gán danh sách document tương ứng cho node hiện tại
                node.model.documentsView = new DocumentsView({
                    node: node,
                    tree: that,
                    isStoreTree: false,
                    treeCallback: callback
                });
            }
        },

        _selectNode: function (node) {
            /// <summary>
            /// Hàm nay chủ yếu là để check hiển thị node lên
            /// </summary>
            var that = this;
            that.selectedNode = node;

            if (node.model.documentsView) {
                // Nếu đã chọn thì render lại và load danh sách các document mới
                node.model.documentsView.$el.show();
            } else {
                // Gán danh sách document tương ứng cho node hiện tại
                node.model.documentsView = new DocumentsView({
                    node: node,
                    tree: that,
                    isStoreTree: false
                });
            }
        },

        _reloadSelectedNode: function () {
            var selectedNode = this.documentTree.selectedNode;
            if (!selectedNode) {
                if (!this.model || this.model.length <= 0)
                    return;

                selectedNode = this.model.findWhere({ isSelected: true });
                if (!selectedNode) {
                    selectedNode = this.model.at(0);
                }
            }

            //Biến check xem có focus lại vào tab home 
            //=> đối với trường hợp tự động tải mới thì mà đang ở tab xử lý văn bản thì để nguyên không cho focus lại tab home
            this.hasFocusHome = false;

            //Biến check là tự động tải văn bản mới về 
            //=> trường hợp người sử dụng đang focus 1 văn bản trên danh sách văn bản thì không focus lại phần tử đầu của danh sách
            this.isAuto = true;

            selectedNode.view.select();
        },

        destroyAutoGetDocNews: function () {
            if (this.autogetNew) {
                window.clearInterval(this.autogetNew);
            }
        },

        _openDocumentTreeNode: function (node) {
            /// <summary>
            /// Mở node con
            /// </summary>
            if (node.model.get('isLoadChildren')) {
                return;
            }

            var that, loading, $el, currentNodeId;
            that = this;
            loading = $('<img src="../Content/Images/ajax-loader.gif" class="loading" width="20" height="20" style="position:fixed; z-index:100; " />');
            currentNodeId = node.model.get('functionId');
            $el = node.$el.find('#child' + currentNodeId);

            // Hiển thị loadding
            $el.append(loading);
            egov.request.getDocumentTree(
                {
                    data: { id: currentNodeId },
                    success: function (children) {
                        node.model.set('isLoadChildren', true);

                        $el.find('.loading').remove();
                        if (children && children.length > 0) {
                            node.$children.empty();
                            that._renderChildren(node, children);
                        } else {
                            node.model.set('isOpen', false);
                        }

                        that.documentTree.updateToCache();
                    },
                    error: function () {
                        $el.find('.loading').remove();
                    }
                });
        },

        _renderChildren: function (node, childrenData) {
            /// <summary>
            /// Hiện thị các node con theo node cha
            /// </summary>  
            var children, count, i, leng, newNode;
            if (node.model.get("totalDocument") <= 0 && node.model.get("hasTransferTheoLo") == true) {
                node.model.set("isOpen", false);
                node.$el.find('.pull-left:first').removeClass('open closed');
                node.$el.find('a.list-group-item:first').attr('data-open', 'false');
                node.$('ul.collapse:first').removeClass("in");
                return;
            }

            // Sắp xếp lại danh sách các node theo order
            childrenData = childrenData.sort(function (a, b) {
                if (b.order == a.order) {
                    return b.name >= a.name ? -1 : 1;
                }
                return b.order > a.order ? -1 : 1;
            });

            children = new egov.models.TreeList([]);
            count = 0;
            leng = childrenData.length;

            // Xử lý lại thứ tự đúng cho danh sách các function
            // Do khi cấu hình sẽ có một số node đc cấu hình là lọc theo lĩnh vực loại hồ sơ
            // Như thế sẽ sinh ra các node lĩnh vực con có cùng order.
            for (i = 0; i < leng; i++) {
                childrenData[i]["id"] = childrenData[i]["functionId"] + childrenData[i]["params"];
                childrenData[i]["isLoadChildren"] = false;

                newNode = new egov.models.TreeModel(childrenData[i]);
                node.add(newNode, false);

                children.add(newNode);
            }

            node.model.set("children", children);

            this.reloadDocumentTree(node.model.get("id"));
        },

        _rebindChildrenView: function (node, data, hasAddNew) {
            /// <summary>
            /// Hiện thị các node con theo node cha
            /// </summary>  
            var children, count, i, leng, newNode;

            // Sắp xếp lại danh sách các node theo order
            data = data.sort(function (a, b) {
                if (b.order === a.order) {
                    return b.name >= a.name ? -1 : 1;
                }
                return b.order > a.order ? -1 : 1;
            });

            children = [];
            count = 0;
            leng = data.length;
            var docModels = (node.model && node.model.documentsView && node.model.documentsView.model)
            ? node.model.documentsView.model.toJSON() : [];

            // Xử lý lại thứ tự đúng cho danh sách các function
            // Do khi cấu hình sẽ có một số node đc cấu hình là lọc theo lĩnh vực loại hồ sơ
            // Như thế sẽ sinh ra các node lĩnh vực con có cùng order.
            for (i = 0; i < leng; i++) {
                if (docModels.length > 0) {
                    //Các node con cùng hướng chuyển do lấy theo quy trình và node thuộc quy trình 
                    //mà đối tượng văn bản không có lên tạm thời để vậy chờ trao đổi tiếp
                    var chilParams = data[i]["params"] ? JSON.parse(data[i]["params"]) : null;
                    //Hopcv: Xử lý sô văn bản của các node con khi đã cache node con tại client
                    //=> hiển thị những node có văn bản
                    if (chilParams && chilParams.length > 0) {
                        var index = 0, tmpdocModels = docModels;
                        while (index < chilParams.length && tmpdocModels.length > 0) {
                            var value = chilParams[index].Value,
                                compareName = chilParams[index].CompareName;
                            tmpdocModels = _.filter(tmpdocModels, function (item) {
                                return item[compareName] == value;
                            });
                            index++;
                        }

                        // data[i]["totalDocument"] = tmpdocModels.length;
                        var totalDocumentUnread = 0;
                        if (tmpdocModels.length > 0) {
                            totalDocumentUnread = _.filter(tmpdocModels, function (item) {
                                return item["IsViewed"] == false;
                            }).length;
                        }

                        // data[i]["totalDocumentUnread"] = totalDocumentUnread;
                    }
                } else {
                    //data[i]["totalDocument"] = 0;
                    //data[i]["totalDocumentUnread"] = 0;
                }

                data[i]["id"] = data[i]["functionId"] + data[i]["params"];
                data[i]["isLoadChildren"] = false;

                if (data[i]["totalDocument"] <= 0) {
                    count++;
                }

                var key = data[i]["params"] + "_" + data[i]["functionId"];
                var tmpNode = node.nodeChildren[key];

                if (tmpNode) {
                    //tmpNode.model.set('totalDocument', data[i]["totalDocument"]);
                    //tmpNode.model.set('totalDocumentUnread', data[i]["totalDocumentUnread"]);
                    if (tmpNode.model.get('totalDocument') <= 0) {
                        if (tmpNode.model.get('hasTransferTheoLo') == true) {
                            tmpNode.$el.hide();
                            this._selectNode(node);
                        }
                    }
                    else {
                        tmpNode.$el.show();
                        tmpNode.$el.find('.pull-left').removeClass('open closed');
                        tmpNode.$el.find('a.list-group-item').attr('data-open', 'false');
                        tmpNode.$('ul.collapse:first').removeClass("in");
                    }
                    data[i] = tmpNode.model.toJSON();
                } else {
                    if (hasAddNew) {
                        newNode = new egov.models.TreeModel(data[i]);
                        node.add(newNode, false);
                        if (newNode.get('totalDocument') <= 0) {
                            if (newNode.get('hasTransferTheoLo') == true) {
                                newNode.view.$el.hide();
                                this._selectNode(node);
                            }
                        }

                        data[i] = newNode.toJSON();
                    }
                }

                children.push(data[i]);
            }

            node.model.set("children", children);

            //Nếu tất cả các node con không có văn bản nào thì ẩn chức năng đóng mở node cha đi
            if (node.model.get("hasTransferTheoLo") == true) {
                if (leng == count) {
                    node.$el.find('.pull-left:first').removeClass('open closed');
                    node.$el.find('a.list-group-item:first').attr('data-open', 'false');
                    node.$('ul.collapse:first').removeClass("in");
                } else {
                    node.$el.find('.pull-left:first').removeClass('open closed').addClass("open");
                    node.$el.find('a.list-group-item:first').attr('data-open', 'true');
                    node.$('ul.collapse:first').addClass("in");
                }
            }
        },

        //============sổ văn bản===========

        renderStoreTree: function () {
            /// <summary>
            /// ren ra danh sách hồ sơ cuả người dùng
            /// </summary>
            var that = this;
            this.storeTree = new StoreTree({
                eGovTree: this,
                callback: function (node) {
                    that.destroyAutoGetDocNews();
                    that._destroyReloadOnlineRegistration();

                    if (!egov.isMobile) {
                        egov.views.home.tab.rootTab.active();
                    }

                    //   egov.views.home.tab.rootTab.model.set('title', node.model.get('name'));
                    if (node.model.get('isStorePrivateRoot') || (node.model.get('isStoreShareRoot'))) {
                        return;
                    }

                    if (node.model.documentsView) {
                        // Nếu đã chọn thì render lại và load danh sách các document mới
                        node.model.documentsView.reloadDocuments();
                    } else {
                        // Gán danh sách document tương ứng cho node hiện tại
                        node.model.documentsView = new DocumentsView({
                            node: node,
                            tree: that,
                            isStoreTree: true
                        });
                    }

                    egov.currentTree.documentViews = node.model.documentsView;
                }
            });
        },

        renderFAQTree: function () {
            /// <summary>
            /// Hỏi đáp trực tuyến
            /// </summary>
            var that = this;
            //if (egov.isMobile) {
            //    return;
            //}
            var that = this;
            $("#questionNode").show();
            egov.dataManager.getNodeQuestion({
                success: function (data) {
                    if (data) {
                        that._faqTree(data);
                    }
                    that.isLoadedFAQTree = true;
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.treeStore.message.error.selectStore);
                }
            });

            egov.views.isLoadedFAQTree = true;
        },

        _faqTree: function (result) {
            var that = this;
            var documentFAQTree = testObject(result.documentNode);

            var faqTrees = [
                {
                    id: 'generalFAQTree',
                    name: egov.resources.tree.question.general,
                    generalFAQTree: true,
                    state: 'in',
                    children: null,
                    showTotalInTreeType: 3,
                    status: 0,
                    isGetGeneral: true,
                    totalDocument: 10,
                },
                {
                    id: 'documentFAQTree',
                    name: egov.resources.tree.question.document,
                    documentFAQTree: true,
                    state: 'in',
                    children: documentFAQTree,
                    showTotalInTreeType: 3,
                    status: 0,
                    totalDocument: 10,
                }
            ];
            var treeModels = new egov.models.QuestionTreeList(faqTrees);

            this.faqTree = new eGovTree({
                el: '#faqList',
                model: treeModels,
                isDocumentTree: false,
                isFAQ: true,
                open: function (node) {
                    that.destroyAutoGetDocNews();
                },
                select: function (node) {
                    //that.destroyAutoGetDocNews();
                    if (!egov.isMobile) {
                        egov.views.home.tab.rootTab.active();
                    }
                    if (node.model.documentsView) {
                        // Nếu đã chọn thì render lại và load danh sách các câu hỏi mới
                        node.model.documentsView.reloadQuestionList();
                    } else {
                        var nodeQuestion = node.model.toJSON();
                        // Gán danh sách document tương ứng cho node hiện tại
                        node.model.documentsView = new DocumentsView({
                            node: node,
                            tree: that,
                            isQuestionTree: true,
                            docfieldIds: Number(nodeQuestion.id) || null
                        });
                    }

                    reloadTotalFAQ(that.faqTree.model);
                    egov.currentTree.documentViews = node.model.documentsView;
                },
            });
            window.setTimeout(function () {
                reloadTotalFAQ(treeModels);
            }, 1000);
        },

        //===== đăng ký qua mạng
        renderOnlineRegistration: function () {
            var that = this;
            //if (egov.isMobile) {
            //    return;
            //}

            if (egov.setting
                && egov.setting.onlineRegistration
                && egov.setting.onlineRegistration.Active) {
                that.isRenderOnlineRegistrationFirst = true;//check load lần đau tiên
                var treeModels = new egov.models.TreeList([{
                    functionId: "onlineRegistration_0",
                    parentId: 0,
                    name: egov.setting.onlineRegistration.Name,
                    paramId: 0,
                    state: "closed",
                    children: [],
                    isLoadChildren: false,
                    showTotalInTreeType: 3,  //hiển thị tổng só văn bản
                    isOnlineRegistration: true,
                    isOnlineRegistrationChildren: false,
                    nodeModel: null,
                    totalDocument: 0
                },
                {
                    functionId: "onlineRegistration_1",
                    parentId: 0,
                    name: "Hồ sơ đăng ký bị hủy",
                    paramId: 0,
                    state: "closed",
                    children: [],
                    isLoadChildren: false,
                    showTotalInTreeType: 3,  //hiển thị tổng só văn bản
                    isOnlineRegistration: true,
                    isOnlineRegistrationChildren: false,
                    nodeModel: null,
                    totalDocument: 0
                }
                ]);

                var commandSelected = function (node, callback) {
                    that.destroyAutoGetDocNews();
                    if (that.hasFocusHome && !egov.isMobile) {
                        egov.views.home.tab.rootTab.active();
                    }
                    var documentOnlineType = 2
                    var functionIdNode = node.model.toJSON().functionId
                    if (functionIdNode == "onlineRegistration_1" || node.model.toJSON().parentId == "onlineRegistration_1") {
                        documentOnlineType = 8
                    } else if (functionIdNode == "onlineRegistration_0" || node.model.toJSON().parentId == "onlineRegistration_0") {
                        documentOnlineType = 2
                    }
                    // Gán danh sách document tương ứng cho node hiện tại
                    node.model.documentsView = new DocumentsView({
                        node: node,
                        tree: that,
                        isOnlineRegistration: true,
                        DocumentOnlineType: documentOnlineType,
                        treeCallback: function (total) {
                            if (that.isRenderOnlineRegistrationFirst) {
                                that.isRenderOnlineRegistrationFirst = false;
                                egov.callback(callback, node);
                                return;
                            }
                            //debugger
                            node.rebindTotal(total);

                            if (node.model.get("isOnlineRegistrationChildren") == false
                                && node.model.get("isOpen") == true
                                && _.isNull(node.nodeChildren)) {
                                var model = node.model.documentsView.model.toJSON();
                                for (var key in node.nodeChildren) {
                                    var total = _.filter(model, function (item) {
                                        return item.DoctypeId == key;
                                    }).length;
                                    if (total > 0) {
                                        node.nodeChildren[key].model.view.rebindTotal(total);
                                    } else {
                                        node.nodeChildren[key].model.view.remove();
                                    }
                                }
                            }
                            egov.callback(callback, node);
                        }
                    });
                    egov.currentTree.documentViews = node.model.documentsView;
                    that._autoReloadOnlineRegistration();
                    egov.pubsub.publish(egov.events.status.destroy);
                };

                var commandOpened = function (node) {
                    node.model.set("isLoadChildren", true);
                    if (node && node.model
                        && node.model.documentsView
                        && node.model.documentsView.model
                        && node.model.documentsView.model.length > 0) {
                        var model = node.model.documentsView.model.toJSON();
                        var doctypes = [];
                        var doctypeIds = [];
                        model.forEach(function (item) {
                            if (!item['Doctype']) {
                                return
                            }
                            var doctype = item['Doctype'];
                            var exist = false;
                            var tmp = {
                                DocTypeName: doctype.DocTypeName,
                                DoctypeId: doctype.DoctypeId,
                            };

                            // Kiểm tra xem người dùng hiện tại có quyền tiếp nhận thủ tục này hay không
                            //var doctypesConfigs = JSON.parse(egov.setting.onlineRegistration.Doctypes);
                            //if (doctypesConfigs.length) {
                            //    exist = !_.contains(doctypesConfigs, doctype.LocalDoctypeId);
                            //}

                            if (doctypeIds.length > 0) {
                                exist = _.contains(doctypeIds, doctype.DoctypeId);
                            }

                            if (!exist) {
                                doctypes.push(tmp);
                                doctypeIds.push(doctype.DoctypeId)
                            }
                        });

                        if (doctypes && doctypes.length > 0) {
                            for (var i = 0; i < doctypes.length; i++) {
                                var total = _.filter(model, function (item) {
                                    return item.DoctypeId == doctypes[i].DoctypeId;
                                }).length;

                                var newNode = new egov.models.TreeModel({
                                    functionId: doctypes[i].DoctypeId,
                                    parentId: 0,
                                    state: "",
                                    name: doctypes[i].DocTypeName,
                                    totalDocument: total,
                                    isLoadChildren: false,
                                    showTotalInTreeType: 3,
                                    isOnlineRegistration: true,
                                    isOnlineRegistrationChildren: true
                                });
                                node.add(newNode);
                            }
                        }

                    }
                };

                that.onlineRegistrationTree = new eGovTree({
                    el: '#onlineRegistration',
                    model: treeModels,
                    isDocumentTree: false,
                    isOnlineRegistration: true,

                    select: function (node) {
                        commandSelected(node);
                    },
                    open: function (node) {
                        if (node.model.get("isLoadChildren") == true
                            || node.model.get("isOnlineRegistrationChildren") == true)
                            return;

                        if (node && node.model
                            && node.model.documentsView
                            && node.model.documentsView.model
                            && node.model.documentsView.model.length > 0) {
                            commandOpened(node);
                        } else {
                            commandSelected(node, function (node) {
                                if (node)
                                    node.model.view.open();
                            });
                        }
                    }
                });

                window.setTimeout(function () {
                    getTotalOnlineRegistration(function (result) {
                        if (that.onlineRegistrationTree && result) {
                            that.onlineRegistrationTree.model.at(0).view.rebindTotal(result)
                        }
                    });
                    getTotalOnlineCancel(function (result) {
                        if (that.onlineRegistrationTree && result) {
                            that.onlineRegistrationTree.model.at(1).view.rebindTotal(result)
                        }
                    })
                }, 1000);
            }
        },

        reloadOnlineRegistration: function (callback) {
            if (this.onlineRegistrationTree) {
                this.onlineRegistrationTree.selectedNode.view.select();
            }
            egov.callback(callback);
        },

        _autoReloadOnlineRegistration: function () {
            if (egov.isMobile) {
                return;
            }

            this._destroyReloadOnlineRegistration();
            var that = this;
            var timeInterVal = 1 * 20 * 1000; //// 20s tự động đồng bộ 1 lần

            this.autoReloadOnlineRegistration = window.setInterval(function () {
                that.hasFocusHome = false;
                that.reloadOnlineRegistration();
                that.hasFocusHome = true;
            }, timeInterVal);
        },

        _destroyReloadOnlineRegistration: function () {
            if (this.autoReloadOnlineRegistration) {
                window.clearInterval(this.autoReloadOnlineRegistration);
            }
        }
    });

    function getTotalOnlineRegistration(callback) {
        egov.request.getTotalOnlineRegistration({
            success: function (result) {
                if (typeof callback === 'function') {
                    callback(result);
                }
            },
            error: function (xhr) {
                if (typeof callback === 'function') {
                    callback(0);
                }
            }
        });
    }

    function getTotalOnlineCancel(callback) {
        egov.request.getTotalOnlineCancel({
            success: function (result) {
                if (typeof callback === 'function') {
                    callback(result);
                }
            },
            error: function (xhr) {
                if (typeof callback === 'function') {
                    callback(0);
                }
            }
        });
    }

    // Lấy tổng số câu hỏi chung
    function getsTotalGeneralQuestion(callback) {
        egov.request.getTotalGeneralQuestion({
            success: function (result) {
                if (typeof callback === 'function') {
                    callback(result);
                }
            },
            error: function (xhr) {
                if (typeof callback === 'function') {
                    callback(0);
                }
            }
        });
    }

    // lấy tổng số câu hỏi theo hồ sơ
    function getsTotalDocumentQuestion(callback) {
        egov.request.getTotalDocumentQuestion({
            success: function (result) {
                if (typeof callback === 'function') {
                    callback(result);
                }
            },
            error: function (xhr) {
                if (typeof callback === 'function') {
                    callback(0);
                }
            }
        });
    }

    //#endregion

    //#region Private Functions

    ///Thêm function cho array
    Array.prototype.remove = function (value) {
        var idx = this.indexOf(value);
        if (idx != -1) {
            return this.splice(idx, 1);
        }
        return false;
    }

    function reloadTotalFAQ(node) {
        getsTotalDocumentQuestion(function (result) {
            if (result) {
                node.at(1).view.rebindTotal(result.total)
            };
        });

        getsTotalGeneralQuestion(function (result) {
            if (result) {
                node.at(0).view.rebindTotal(result.total)
            };
        });
    }

    function testObject(list) {
        var listFAQ = [];
        for (var i = list.length - 1; i >= 0; i--) {
            var object = {
                id: list[i].id,
                name: list[i].name,
                state: 'in',
                children: null,
                showTotalInTreeType: 3,
                status: 0,
                isGetGeneral: true,
                totalDocument: 10,
            }
            listFAQ.push(object);
        }
        return listFAQ;
    }

    //#endregion

    window.eGovTree = Tree;
    return Tree;
});