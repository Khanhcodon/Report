define(function () {
    "use strict";

    /// <summary> Đối tượng view thể hiện 1 tree</summary>
    egov.views.base.Tree = Backbone.View.extend({

        className: "tree-view-el",

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="option"></param>
            this.el = option.el;
            this.$el.addClass("tree-view-el");
            this.isDocumentTree = option.isDocumentTree;
            this.isOnlineRegistration = option.isOnlineRegistration;//đăng ký qua mang
            this.isStoreTree = option.isStoreTree;
            this.isFAQ = option.isFAQ;
            this.hasDroppable = option.isDroppable;
            this.callback = {
                select: option.select,
                open: option.open,
                reload: option.reload,
                contextmenu: option.contextmenu,
                close: option.close,
                droppable: option.droppable,
                dblclick: option.dblclick
            };

            this.render();

            return this;
        },

        render: function () {
            /// <summary>
            /// Bind tree
            /// </summary>
            var that = this;
            that.hasChange = false;

            if (that.isDocumentTree) {
                that.selectedNode = null;
            }
            else if (that.isOnlineRegistration) {
                that.onlineRegistrationModel = [];
                that.onlineRegistrationModelSelected = null;
            }
            else {
                that.storesModel = [];
                that.storeModelSelected = null;
            }

            that._setDefaultNode();

            that.model.each(function (node) {
                var newNode = new egov.views.base.TreeItem({
                    model: node,
                    parent: that,
                    callback: that.callback,
                    isDocumentTree: that.isDocumentTree,
                    isOnlineRegistration: that.isOnlineRegistration,
                    isFAQ: that.isFAQ,
                    isStoreTree: that.isStoreTree,
                    hasDroppbale: that.hasDroppable
                });
                node.view = newNode;
                if (that.isDocumentTree) {
                    that.$el.find("#child" + node.get('treeGroupId')).append(newNode.$el);
                } else {
                    if (that.isOnlineRegistration) {
                        that.onlineRegistrationModel.push(newNode);
                    } else {
                        that.storesModel.push(newNode);
                    }
                    that.$el.append(newNode.$el);
                }
            });

            that.model.on("change", function () {
                that.hasChange = true;
            }, that);
        },

        unCurrentSelectedNode: function () {
            /// <summary>
            /// Bỏ trạng thái selected của node đang được chọn hiện tại
            /// </summary>
            var selectedNode = this.selectedNode;
            if (selectedNode) {
                selectedNode.set("isSelected", false);
            }
        },

        _setDefaultNode: function () {
            /// <summary>
            /// Select node mặc định: node đang được chọn gần nhất hoặc node đầu tiên
            /// </summary>
            if (!this.isDocumentTree || this.isOnlineRegistration) {
                return;
            }
            // -- New Code 02/02 -- Văn bản chờ xử lý được chọn đầu tiên khi vào trang -- //
            // -- this.model.at(2) : tức "Văn bản chờ xử lý" -- //
            var selectedNode = this.model.findWhere({ isSelected: true });
            //if (!selectedNode) {
                selectedNode = this.model.at(2);
                selectedNode.set("isSelected", true);
           // }
        },

        selectCurrentNode: function () {
            ///<summary>
            /// Hàm này dùng khi người dung chuyển các ứng dụng khi chọn lại tab văn bản thì đồng bộ node đang được chọn
            ///</summary>
            if (!this.selectedNode) {
                this.selectedNode = this.model.at(0);
            }

            this.selectedNode.view.select();
        },

        updateToCache: function () {
            /// <summary>
            /// Cập nhật cây văn bản vào cache
            /// </summary>
            if (!this.isDocumentTree
                || !this.hasChange
                || this.isOnlineRegistration) {
                return;
            }

            var treeData = this.model.toJSON();
            egov.dataManager.updateTree(treeData);
            this.hasChange = false;
        }
    });

    /// <summary>Lớp view thể hiện 1 node trên cây</summary>
    egov.views.base.TreeItem = Backbone.View.extend({
        openClass: 'in',
        selectClass: 'active',
        displayClass: "display",

        // Dom tagname element
        tagName: 'li',

        className: "mdl-list__item",

        events: {
            'click .closed': 'open',       // Click mở node
            'click .open': 'close',        // Click Đóng node
            'tap .closed': 'open',         // Touchend trên tablet-mobile để mở node
            'tap .open': 'close',          // Touchend trên tablet-mobile để đóng node
            'contextmenu a': 'contextMenu', // Giữ touch để hiển thị contextmenu trên mobile tablet
        },

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="option"></param>
            this.parent = option.parent;
            this.parentNode = option.parentNode
            this.callback = option.callback;
            this.isDocumentTree = option.isDocumentTree;
            this.isOnlineRegistration = option.isOnlineRegistration
            this.isSlected = option.isSlected || false;
            this.hasDroppbale = option.hasDroppbale;

            this.nodeChildren = {}; //chứa các model node con 
            if (option.isDocumentTree) {
                this.model.set("isDocumentTree", true);
            }
            else if (option.isOnlineRegistration) {
                this.model.set("isOnlineRegistration", true);
            }
            else if (option.isStoreTree) {
                this.model.set("isStoreTree", true);
            }
            else if (option.isFAQ) {
                this.isFAQ = true;
                this.model.set("isFAQ", true);
            }

            if (this.isDocumentTree || this.isOnlineRegistration) {
                this.$el.attr('id', this.model.get('functionId'));
            } else {
                var storeId = this.model.get('storePrivateId');
                this.$el.attr('id', storeId)
                    .attr('data-storeprivateid', storeId);
            }

            this.$el.attr('data-parentid', this.model.get('parentId'));

            // Bind ra node theo template và model
            this.render();

            //các events trên đối tượng cây văn bản
            this._eventOfProps();
            return this;
        },

        render: function () {
            /// <summary>
            /// Bind node
            /// </summary>
            var that, template, children, i, leng;

            that = this;

            template = getTreeTemplate(that.isDocumentTree, that.isOnlineRegistration, that.isFAQ);
            that.$children = that.$('ul.collapse:first');
            require([template], function (Template) {
                that.$el.html($.tmpl(Template, that.model.toJSON()));
                
                //Xử lý cho sổ hồ sơ
                if (!that.isDocumentTree) {
                    children = that.model.get('children');
                    if (children && children.length > 0) {
                        children.each(function (child) {
                            that.add(child);
                        });
                        that.$children.removeClass('in');
                    }

                    if (that.isOnlineRegistration) {
                        that._showTotalInTreeType();
                    }
                }

                // Gán node đang được select mặc định
                if (that.model.get("isSelected")) {
                    var $element = egov.isMobile ? that.$el : that.$el.find('a:first');
                    $element.toggleClass(that.selectClass);
                    that.parent.selectedNode = that.model;
                }

                if (that.isDocumentTree) {
                    that.$el.children().attr('data-params', that.model.get("params"));
                    that._showTotalInTreeType();
                    if (that.model.get('isOpen')) {
                        that.$('.closed').click();
                    }
                }
                if (egov.isMobile) {
                    //that.$(".mdl-button").materialButton();
                }
                if (that.hasDroppbale) {
                   that.setDroppable();
                }

                that.$el.bindResources();
            });
            
        },

        _eventOfProps: function () {

            var that = this;

            // Sự kiện mở node
            this.on('open', function () {
                egov.helper.hideAllContext();
                egov.callback(that.callback.open, that);
                if (that.hasDroppbale) {
                    that.setDroppable();
                }
            }, this);

            // Sự kiện đóng node
            this.on('close', function () {
                egov.helper.hideAllContext();
                egov.callback(that.callback.close, that);
            }, this);

            // Sự kiện chọn node
            this.on('select', function () {
                egov.helper.hideAllContext();
                if (egov.mobile) {
                    egov.mobile.autoHideMainMemu();
                }
                egov.callback(that.callback.select, that);
            }, this);
                
            this.on('reload', function () {
                egov.callback(that.callback.reload, that);
            }, this);

            this.on('contextmenu', function (event) {
                egov.helper.hideAllContext();
                if (typeof that.callback.contextmenu === 'function') {
                    that.callback.contextmenu(that, event);
                }
            }, this);

            this.model.on('change:name', function (model, name) {
                that.render();
            }, this);

            this.model.on('change:totalDocumentUnread', function (model) {
                that._showTotalInTreeType();
                if (that.parentNode && that.parentNode.model) {
                    var childrens = that.parentNode.model.get("children");
                    var itm = _.find(childrens, function (i) {
                        return i.params == model.get("params")
                            && i.functionId == model.get("functionId");
                    });

                    if (itm) {
                        itm.totalDocumentUnread = model.get("totalDocumentUnread");
                        that.parent.hasChange = true;
                    }
                }
            }, this);

            this.model.on('change:totalDocument', function (model) {
                that._showTotalInTreeType();
                if (that.parentNode && that.parentNode.model) {
                    var childrens = that.parentNode.model.get("children");
                    var itm = _.find(childrens, function (i) {
                        return i.params == model.get("params")
                            && i.functionId == model.get("functionId");
                    });

                    if (itm) {
                        itm.totalDocument = model.get("totalDocument");
                        that.parent.hasChange = true;
                    }
                }
            }, this);

            this.model.on('change:isSelected', function (model, isSelected) {
                var $element = egov.isMobile ? this.$el : this.$el.find('a:first');
                if (isSelected) {
                    $element.addClass(this.selectClass);
                } else {
                    $element.removeClass(this.selectClass);
                }
            }, this);

            this.$el.dbclick(
                function (e) {
                    that.select(e);
                },
                function (e) {
                    if (typeof that.callback.dblclick === 'function') {
                        that.callback.dblclick(that, event);
                        return;
                    }
                    var target = $(e.target);
                    var $el = target.parent("a.list-group-item").find('.closed , .open');
                    $el.click();
                });
        },

        add: function (newItemModel, isShowParent) {
            ///<summary>
            /// Thêm node con cho node hiện tại
            ///<para name="newItemModel">Đối tượng node item</para>
            ///</summary>
            isShowParent = isShowParent || false;
            var that, nodeNameId, newNode;
            that = this;
            nodeNameId = (this.isDocumentTree || this.isOnlineRegistration) ? 'functionId' : 'storePrivateId';

            if (this.$('ul.collapse:first').length === 0) {
                this.$children = $('<ul class="nav panel-collapse collapse">');
                this.$el.append(this.$children);
            } else {
                this.$children = this.$('ul.collapse:first');
            }

            newItemModel.set('parentId', this.model.get(nodeNameId));
            newNode = new egov.views.base.TreeItem({
                model: newItemModel,
                isDocumentTree: that.isDocumentTree,
                isOnlineRegistration: that.isOnlineRegistration,
                callback: that.callback,
                parent: that.parent,
                parentNode: that
            });

            if (this.isDocumentTree || this.isOnlineRegistration) {
                var key;
                if (this.isOnlineRegistration) {
                    key = newItemModel.get('functionId');
                } else {
                    key = newItemModel.get('params') + "_" + newItemModel.get('functionId');
                }

                this.nodeChildren[key] = newNode;
            }

            newItemModel.view = newNode; // gán lại view cho đối tượng model
            this.$children.append(newNode.$el);

            if (isShowParent) {
                this.$children.parent('li').find('a:first .pull-left').removeClass('open closed').addClass('open');
            }

            return newNode;
        },

        remove: function () {
            ///<summary>
            /// Xóa node khỏi cây
            ///</summary>
            this.$el.remove();
        },

        select: function (event) {
            ///<summary> 
            /// Chọn node hiện tại
            ///</summary>
            egov.helper.hideAllContext();
            egov.helper.destroyClickEvent(event);
            this._removeOtherSelected();
            // bỏ selected tất cả các node đang được chọn
            // Gán selected cho node hiện tại
            if (egov.isMobile) {
                if (event && event.originalEvent) {
                    this.selectMobile();
                }
                egov.commonFn.event.changeTitleForMobile("documents", this.model.get("name"));
                egov.mobile.hidePanel();
            }

            //gán lại node dang được chọn trên sổ hồ sơ => tạo sổ hồ sơ khi taạo mới ăn ản và kết thúc xử lý
            if (!this.isDocumentTree) {
                this.parent.storeModelSelected = this;
            }
            this.parent.unCurrentSelectedNode();
            this.model.set('isSelected', true);
            this.parent.selectedNode = this.model;
            this.triggerSelectNode();
        },

        setDroppable: function (event) {
            var that = this;
            that.$("a").droppable({
                drop: function (e) {
                    var node = $(e.target).closest("li");
                    that.callback.droppable(node);
                    $(node).css("border", "none");

                },
                over: function (e) {
                    var target = $(e.target).closest("li");
                    var storePrivateId = target.attr("id");
                    if (storePrivateId && storePrivateId != 0) {
                        $(target).css("border", "solid 1px #2b2f31");
                    } 
                },
                out: function (e) {
                    var target = $(e.target).closest("li");
                    $(target).css("border","none");
                }
            });
        },

        selectMobile: function () {
            this._removeOtherSelectedMobbile();
            this.$el.parent().removeClass(this.displayClass);
            $(egov.views.home.tree.documentList).addClass(this.displayClass);
        },

        triggerSelectNode: function () {
            this.trigger('select');
        },

        open: function (event) {
            ///<summary> 
            /// Mở node hiện tại
            ///</summary>
            egov.helper.hideAllContext();
            egov.helper.destroyClickEvent(event);
            if (event) {
                var target = $(event.target);
                target.removeClass('open closed').addClass('open')
                      .closest('a').attr('data-open', 'true');
                if (egov.isMobile) {
                    target.html("expand_more");
                }
            }

            this.$('.panel-collapse:first').addClass('in');

            if (this.isDocumentTree && this.parent) {
                if (!this.model.get('isOpen')) {
                    this.model.set('isOpen', true);
                }
            }

            this.triggerOpenNode();
        },

        triggerOpenNode: function () {
            this.trigger('open');
        },

        close: function (event) {
            ///<summary> 
            /// Đóng node
            ///</summary>

            egov.helper.hideAllContext();
            var target = $(event.target);

            if (this.isDocumentTree && this.parent) {
                if (this.model.get('isOpen')) {
                    this.model.set('isOpen', false);
                }
            }

            this.$('.panel-collapse.in:first').removeClass('in');

            target.removeClass('open closed')
                .addClass('closed').closest('a')
                .attr('data-open', 'false');
            if (egov.isMobile) {
                target.html("chevron_right");
            }
            this.triggerCloseNode();

            egov.helper.destroyClickEvent(event);
        },

        triggerCloseNode: function () {
            this.trigger('close');
        },

        contextMenu: function (event) {
            ///<summary> 
            /// Tạo contextmenu
            ///</summary> 
            egov.helper.destroyClickEvent(event);
            this._removeOtherSelected();
            this.$('a:first').addClass(this.selectClass);
            //gán lại node dang được chọn trên sổ hồ sơ=> tạo sổ hồ sơ khi tạo mới văn bản và kết thúc xử lý
            if (!this.isDocumentTree) {
                this.parent.storeModelSelected = this;
            }

            this.trigger('contextmenu');
        },

        _removeOtherSelected: function () {
            ///<summary> 
            /// Bỏ selected tất cả các node khác: sử dụng cho event dbclick đã override
            ///</summary>
            if (egov.views
                && egov.views.home
                && egov.views.home.tree
                && egov.views.home.tree.$el) {
                egov.views.home.tree.$el.find("." + this.selectClass)
                    .removeClass(this.selectClass);
            } else {
                $("." + this.selectClass).removeClass(this.selectClass);
            }
        },

        _removeOtherSelectedMobbile: function () {
            this.$el.parent().find("." + this.selectClass).removeClass(this.selectClass);
        },

        _showTotalInTreeType: function () {
            ///<summary>
            /// Hiển thị số văn bản trên cây văn bản theo cấu hình
            ///</summary> 
            var type = this.model.get('showTotalInTreeType'),
                unread = this.model.get('totalDocumentUnread'),
                total = this.model.get('totalDocument'),
                text = "",
                title = "";

            //Trạng thái hiện thị số văn bản trên node của cây văn bản
            var showTotalInTreeType = {
                none: 0,        //Không hiển thị
                unread: 1,      //Văn bản chưa đọc
                unreadOnAll: 2, //Chưa đọc / Tất cả
                all: 3,          //Tất cả
            };

            switch (type) {
                case showTotalInTreeType.none:
                    break;

                case showTotalInTreeType.unreadOnAll: //văn bản chưa đọc/ tổng số văn bản
                    text = unread + "/" + total;
                    title = String.format(egov.resources.tree.displayUnReadOnAll, unread, total);
                    break;

                case showTotalInTreeType.all:// tất cả văn bản
                    text = total <= 0 ? "" : total;
                    title = String.format(egov.resources.tree.displayAll, total);
                    break;

                default://Số văn bản chưa đọc
                    text = unread <= 0 ? "" : unread;
                    title = String.format(egov.resources.tree.displayUnRead, unread);
                    break;
            }

            if (egov.isMobile) {
                this.$el.find('.totalUnread:first').attr('data-badge', unread >= 100 ? "99+" : unread);
            }
            else {
                this.$el.find('.totalUnread:first').text(text)
                .attr('data-totalunread', this.model.get('totalDocumentUnread'))
                .attr('data-total', this.model.get('totalDocument'))
                .attr('title', title);

                this.$el.find('.qtooltip').etip();
            }

        },

        rebindTotal: function (total) {
            debugger
            this.model.set("totalDocument", total);
            this._showTotalInTreeType();
        }
    });

    //#region Private Methods

    function getTreeTemplate(isDocumentTree, isOnlineRegistration, isFAQ) {
        /// <summary>
        /// Trả về template cho cây văn bản, hồ sơ
        /// </summary>
        /// <param name="isDocumentTree">Giá trị xác định là cây văn bản hay cây hồ sơ</param>
        /// <returns type=""></returns>
        var egovTemplate = egov.template.tree;
        if (egov.isMobile) {
            if (isDocumentTree || isOnlineRegistration) {
                return egovTemplate.mobile;
            }
            else if (isFAQ) {
                return egovTemplate.questionMobile;
            }
            return egovTemplate.storeTreeMobile;
        }
        else {
            if (isDocumentTree || isOnlineRegistration) {
                return egovTemplate.documentTree;
            }
            else if (isFAQ) {
                return egovTemplate.question;
            }
            return egovTemplate.storeTree;
        }

    }


    //#endregion

    return egov.views.base.Tree;
});