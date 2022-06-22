define([
    'text!templates/tree/tree-template.html',
    'text!templates/tree/treeStorePrivate-template.html'
], function (TreeTemplate, TreeStorePrivateTemplate) {
    "use strict";

    /// <summary> Đối tượng view thể hiện 1 tree</summary>
    egov.views.base.Tree = Backbone.View.extend({

        // Khởi tạo: render ra các tree
        initialize: function (option) {
            this.options = option;
            this.el = option.container;
            this.isDocumentTree = option.isDocumentTree;
            this.isSelectFirst = option.isSelectFirst;
            this.callback = {
                select: option.select,
                open: option.open,
                reload: option.reload,
                contextmenu: option.contextmenu
            };
            this.render();
        },

        // Render ra các tree
        render: function () {
            var that = this,
                firstNode,
                newNode,
                i = 0;

            that.model.each(function (node) {
                newNode = that._addNode(node);
                if (i === 0) {
                    firstNode = newNode;
                    i = 1;
                }
                node.view = newNode;
            });

            if (that.isSelectFirst) {
                firstNode.select();
            }

            that._handleModelTrigger();

            return that;
        },

        _handleModelTrigger: function () {
            /// <summary>
            /// Detect các thay đổi trên model
            /// </summary>

            this.model.on("add", function (nodeModel) {
                nodeModel.view = this._addNode(nodeModel);
            }, this);
        },

        _addNode: function (nodeModel) {
            /// <summary>
            /// Thêm node
            /// </summary>
            /// <param name="nodeModel"></param>
            var newNode,
                parentId,
                parentNode,
                that = this;

            parentId = nodeModel.get('parentId');
            if (parentId !== 0) {
                parentNode = that.model.detect(function (node) {
                    return node.get("functionId") === parentId;
                });
                if (parentNode) {
                    newNode = parentNode.view.add(nodeModel);
                }
            } else {
                newNode = new egov.views.base.TreeItem({
                    model: nodeModel,
                    callback: that.callback,
                    isDocumentTree: that.isDocumentTree
                });
                that.$el.append(newNode.el);
            }

            return newNode;
        }
    });

    /// <summary>Lớp view thể hiện 1 node trên cây</summary>
    egov.views.base.TreeItem = Backbone.View.extend({
        openClass: 'in',
        selectClass: 'active',

        // Dom tagname element
        tagName: 'li',

        // Các sự kiện trong node
        events: {
            'click .closed': 'open',       //Click mở node
            'click .open': 'close',        // Click Đóng node
            'touchend a': 'select',        //Touchend trên tablet-mobile để select node
            'touchend .closed': 'open',    //Touchend trên tablet-mobile để mở node
            'touchend .open': 'close',     //Touchend trên tablet-mobile để đóng node
            'contextmenu a': 'contextMenu' //Giữ  touch để hiển thị contextmenu trên mobile tablet
        },

        // Render node
        initialize: function (option) {
            var id;

            this.options = option;
            this.callback = option.callback;
            this.isDocumentTree = option.isDocumentTree;

            id = this.isDocumentTree ? 'functionId' : 'storePrivateId';
            this.$el.attr('id', this.model.get(id));

            // Bind ra node theo template và model
            this.render();
            this.contextMenu();

            this._handleTrigger();
            this._handleClick();
        },

        // Thêm một nút con
        add: function (newItemModel) {
            var that = this;
            if (that.$('ul.panel-collapse:first').length === 0) {
                that.$children = $('<ul class="nav panel-collapse collapse in">');
                that.$el.append(that.$children);
            }
            else {
                that.$children = that.$('ul.panel-collapse:first');
                that.$children.addClass('in');
            }

            if (this.isDocumentTree) {
                newItemModel.set('parentId', this.model.get('functionId'));
            } else {
                newItemModel.set('parentId', this.model.get('storePrivateId'));
            }

            var newNode = new egov.views.base.TreeItem({
                model: newItemModel,
                isDocumentTree: this.isDocumentTree,
                callback: this.callback,
                publish: egov.pubsub.publish
            });

            that.$children.append(newNode.el);
            return newNode;
        },

        remove: function () {
            this.$el.remove();
        },

        render: function () {
            this.$children = this.$('ul.collapse:first');
            if (this.isDocumentTree) {
                this.$el.html($.tmpl(TreeTemplate, this.model.toJSON()));
                this.$el.find('.qtooltip').etip();
            }
            else {
                this.$el.html($.tmpl(TreeStorePrivateTemplate, this.model.toJSON()));
            }
            var children = this.model.get('children');
            if (children.length > 0) {
                var leng = children.length;
                for (var i = 0; i < leng; i++) {
                    if (this.isDocumentTree) {
                        this.add(new egov.models.TreeModel(children[i]));
                    }
                    else {
                        this.add(new egov.models.StorePrivateModel(children[i]));
                    }
                }
                this.$children.removeClass('in');
            }
            return this;
        },

        isActive: function () {
            /// <summary>
            /// Trả về giá trị xác định node đang được select hay không.
            /// </summary>

            return this.$('a:first').hasClass(this.selectClass);
        },

        // Chọn node hiện tại
        select: function (event) {
            egov.pubsub.publish(egov.events.hideAllContext);
            egov.pubsub.publish(egov.events.destroyClickEvent, event);
            this.trigger('select');
        },

        selectNew: function (event) {
            egov.pubsub.publish(egov.events.hideAllContext);
            if (this.isActive()) {
                this.isActivated = true;
            } else {
                this.isActivated = false;
                // bỏ selected tất cả các node đang được chọn
                this.removeOtherSelected();

                // Gán selected cho node hiện tại
                this.$('a:first').addClass(this.selectClass);
            }

            egov.pubsub.publish(egov.events.destroyClickEvent, event);
        },

        displayDocList: function (event) {
            this.trigger('select');
        },

        // Mở node hiện tại
        open: function (event) {
            egov.pubsub.publish(egov.events.hideAllContext);
            var target = $(event.target);
            target.closest('a').attr('data-open', 'true');
            target.removeClass('closed').addClass('open');
            this.$('.panel-collapse:first').addClass('in');

            egov.pubsub.publish(egov.events.destroyClickEvent, event);
            this.trigger('open');
        },

        // Đóng node hiện tại
        close: function (event) {
            egov.pubsub.publish(egov.events.hideAllContext);
            var target = $(event.target);
            target.closest('a').attr('data-open', 'false');
            // target.removeClass('icon-arrow-down9 open').addClass('icon-arrow-right9 closed');
            target.removeClass('open').addClass('closed');
            this.$('.panel-collapse.in:first').removeClass('in', 200);

            egov.pubsub.publish(egov.events.destroyClickEvent, event);
        },

        // Chuột phải
        contextMenu: function (event) {

            egov.pubsub.publish(egov.events.destroyClickEvent, event);
            this.trigger('contextmenu');
        },

        removeOtherSelected: function () {
            /// <summary>
            /// Bỏ selected tất cả các node khác 
            /// </summary>
            $("#menu-document ." + this.selectClass).removeClass(this.selectClass);
        },

        // Bỏ selected tất cả các node khác: sử dụng cho event dbclick đã override
        removeOtherSelectednew: function () {
            this.$el.parent().find("." + this.selectClass).removeClass(this.selectClass);
        },

        // Thiết lập lại giá trị unread cho node hiện tại.
        /// <param name="value" type="int">Giá trị mới</param>
        setTotalUnread: function (value) {
            if (typeof value === 'number') {
                this.$el.find('.totalUnread').text(value > 99 ? '99+' : value)
                    .attr('data-totalunread', value)
                    .attr('title', value + ' chưa đọc / ' + this.model.get('totalDocument') + ' văn bản');

                updateTotalUnread(this.model.get('id'), value);
            }
        },

        ///Khi dblclick đóng mở trên cây văn bản
        toggle: function (event) {
            if (!event) {
                return;
            }

            egov.pubsub.publish(egov.events.hideAllContext);
            var target = $(event.target);
            if (!target.attr('data-open') || target.attr('data-open') === "false") {
                ///thay đổi trang thai attribute data-open = true 
                target.attr('data-open', 'true');
                //Tìm phần tử có class closed 
                var closed = target.parent("a.list-group-item").find('.closed');
                closed.removeClass('icon-arrow-right9 closed').addClass('icon-arrow-down9 open');

                ///Hiển thị các node con của node hiện tại
                this.$('.panel-collapse:first').addClass('in');

                egov.pubsub.publish(egov.events.destroyClickEvent, event);
                this.trigger('open');
                egov.message.notification('hide');
            }
            else {
                ///thay đổi trang thai attribute data-open = false 
                target.attr('data-open', 'false');
                var open = target.parent("a.list-group-item").find('.open');
                open.removeClass('icon-arrow-down9 open').addClass('icon-arrow-right9 closed');
                //Đóng các node con của node hiện tại
                this.$('.panel-collapse.in:first').removeClass('in');

                egov.pubsub.publish(egov.events.destroyClickEvent, event);
            }
        },

        _handleClick: function () {
            /// <summary>
            /// Điều khiển sự kiện click, dblclick
            /// </summary>
            var that = this,
                target;

            that.$('.node-name').dbclick({
                preclick: function (e) {
                    that.selectNew(e);
                },
                click: function (e) {
                    that.select(e);
                },
                dblclick: function (e) {
                    target = $(e.target);
                    if (!target.attr('data-open') || target.attr('data-open') === "false") {
                        that.open(e);
                    } else {
                        that.close(e);
                    }
                }
            });
        },

        _handleTrigger: function () {
            /// <summary>
            /// Điều khiển các sự kiện trên node
            /// </summary>

            var that = this;

            // Sự kiện mở node
            this.on('open', function () {
                egov.callback(that.callback.open, that);
                that.isOpened = true;
            });

            // Sự kiện chọn node
            this.on('select', function () {
                egov.callback(that.callback.select, {
                    node: that,
                    isActivated: that.isActivated
                });
            });

            this.on('reload', function () {
                egov.callback(that.callback.reload);
            });

            this.on('contextmenu', function (event) {
                egov.callback(that.callback.contextmenu, that);
            });

            this.model.on('change:name', function (model, name) {
                that.render();
            }, this);

            this.model.on('change:children', function (mode, children) {
                children.each(function (child) {
                    that.add(child);
                });
            }, this);

            this.model.on('change:totalDocumentUnread', function (model, unread) {
                that.setTotalUnread(unread);
            }, this);

            this.model.on("remove", function () {
                this.$el.remove();
            }, this)
        }
    });

    //#region Model

    //#region Private Methods

    function updateTotalUnread(nodeId, unread) {
        ///<summary>Cập nhật số văn bản chưa đọc cho node</summary>
        ///<param name="unread" type="number">Số văn bản chưa đọc</param>
        if (!nodeId || nodeId <= 0) {
            return;
        }

        if (!unread || unread < 0) {
            unread = 0;
        }

        egov.locache.get('treeOfUsers', function (treeOfUsers) {
            if (!treeOfUsers) {
                return;
            }

            for (var i = 0; i < treeOfUsers.length; i++) {
                if (egov.setting.userId === treeOfUsers[i].userId) {
                    if (!treeOfUsers[i].value || treeOfUsers[i].value.length <= 0) {
                        return;
                    }

                    for (var j = 0; j < treeOfUsers[i].value.length; j++) {
                        if (treeOfUsers[i].value[j].id === nodeId) {
                            treeOfUsers[i].value[j].totalDocumentUnread = unread;
                            break;
                        }
                    }

                    egov.locache.update('treeOfUsers', treeOfUsers);
                    break;
                }
            }
        });
    }

    //#endregion

    return egov.views.base.Tree;
});