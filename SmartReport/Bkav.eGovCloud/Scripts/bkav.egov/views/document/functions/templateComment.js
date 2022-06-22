define(function () {
    "use strict";

    var TemplateCommentModel = Backbone.Model.extend({
        defaults: {
            id: 0,
            CommonCommentId: 0,
            Content: '',
            UserId: 0,
            Selected: false
        },

        initialize: function () {
            this.set('id', this.get('CommonCommentId'));
        }
    });

    var TemplateCommentCollection = Backbone.Collection.extend({
        model: TemplateCommentModel
    });

    var TemplateCommentView = Backbone.View.extend({

        tagName: "div",

        initialize: function (options) {
            this.callbackInsertComments = options.callbackInsertComments;
            this.callbackEnableButton = options.callbackEnableButton
            this.render();

            return this;
        },

        render: function (options) {
            var that = this;
            require([egov.template.document.templateComment], function (TemplateCommentTemplate) {
                that.$el.empty().append(TemplateCommentTemplate);
                that.$el.bindResources();
                that._open();
            });
        },

        unSelectedAll: function () {
            this.model.each(function (doc) {
                if (doc.get('Selected') == true) {
                    doc.set('Selected', false);
                }
            });
        },

        _open: function () {
            var that = this;
            egov.dataManager.getTemplateComments({
                success: function (data) {
                    that.model = new TemplateCommentCollection(data);
                    that._renderDialog();
                    egov.callback(that.callbackEnableButton);
                }
            });
        },

        _bindView: function () {
            var that = this;
            this.$('table tbody').empty();
            this.model.each(function (result) {
                var item = new TemplateCommentItem({
                    model: result,
                    parent: that
                });
                that.$('table tbody').append(item.$el);
            });
        },

        _renderDialog: function () {
            var that = this;
            var dialogSetting = {
                title: egov.resources.templateComment.titleDialog,
                width: '650px',
                draggable: true,
                keyboard: true,
                height: "250px",
                buttons: [
                    {
                        id: "addTemplateComment",
                        text: egov.resources.templateComment.btnAddTemplateComment,
                        className: 'btn-info',
                        style: {
                            left: "10px",
                            position: "absolute"
                        },
                        click: function (callback) {
                            that._add(callback);
                        }
                    },
                    {
                        id: "selectedTemplateComment",
                        text: egov.resources.templateComment.btnSelected,
                        className: 'btn-primary',
                        click: function (callback) {
                            var selected = that.model.where({ Selected: true });
                            if (selected && selected.length > 0) {
                                var content = selected[0].get("Content");
                                if (selected.length > 1) {
                                    for (var i = 1; i < selected.length; i++) {
                                        content += '\n' + selected[i].get("Content");
                                    }
                                }

                                that.callbackInsertComments(content);
                            }
                            that.$el.dialog('hide');
                        }
                    },
                    {
                        id: "closeTemplateComment",
                        className: 'btn-close',
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that.$el.dialog('hide');
                        }
                    }
                ]
            };

            this._bindView();
            this.$el.dialog(dialogSetting);
        },

        _add: function (callback) {
            var that = this;
            egov.message.prompt(egov.resources.templateComment.addDialog.title,
              egov.resources.templateComment.addDialog.btnCreate,
                function (content) {
                    if (content == null || content === '') {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.templateComment.addDialog.errorMessage);

                        if (typeof callback === 'function') {
                            callback();
                        }
                        return;
                    }

                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);
                    egov.request.createTemplateComments({
                        data: {
                            content: content
                        },
                        success: function (result) {
                            if (result.success) {
                                egov.pubsub.publish(egov.events.status.success, result.message);

                                that.model.add(result.data);
                                that._bindView();
                                egov.dataManager.updateTemplateComments(result.data, false);
                            } else {
                                egov.pubsub.publish(egov.events.status.error, result.message);
                                egov.callback(callback);
                            }
                        },
                        error: function () {
                            egov.pubsub.publish(egov.events.status.error, egov.resources.document.sendComment.sendFail);

                            egov.callback(callback);
                        }
                    });
                }, true
            );
        },
    });

    var TemplateCommentItem = Backbone.View.extend({

        tagName: "tr",

        selectedClass: 'rowSelected',

        events: {
            "dblclick": "_append",
            "click": "_selected",
            "click .tempComment-update": "_edit",
            "click .tempComment-remove": "_remove",
            "contextmenu": "_contextmenu"
        },

        initialize: function (option) {
            this.parent = option.parent;
            var that = this;
            this.model.on('change:Selected', function (model, selected) {
                that.$el.toggleClass(that.selectedClass);
            });

            this.model.on('change:Content', function (model, selected) {
                that.parent._bindView();
            });

            require([egov.template.document.templateCommentItem], function (TemplateCommentItemTemplate) {
                that.$el.append($.tmpl(TemplateCommentItemTemplate, that.model.toJSON()));
            });

            return this;
        },

        _append: function (e) {
            if (!e) {
                return;
            }
            egov.helper.destroyClickEvent(e);

            if (typeof this.parent.callbackInsertComments === "function") {
                this.parent.callbackInsertComments(this.model.get("Content"));
                this.parent.$el.dialog('hide');
            }
        },

        _edit: function (e) {
            if (!e) {
                return;
            }
            egov.helper.destroyClickEvent(e);
            egov.helper.hideAllContext();

            var that = this;
            egov.message.prompt(egov.resources.templateComment.editDialog.title,
                egov.resources.templateComment.editDialog.btnEdit,
                function (content) {
                    if (content == null || content === '') {

                        // egov.message.error(egov.resources.templateComment.editDialog.errorMessage);
                        egov.pubsub.publish(egov.events.status.error, egov.resources.templateComment.editDialog.errorMessage);

                        egov.callback(callback);

                        return;
                    }

                    // egov.message.processing(egov.resources.common.transfering, false);
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

                    egov.request.updateTemplateComments({
                        data: {
                            id: that.model.get('id'),
                            content: content
                        },
                        success: function (result) {
                            if (result.success) {

                                // egov.message.success(result.message);
                                egov.pubsub.publish(egov.events.status.success, result.message);

                                that.model.set("Content", content);
                                egov.dataManager.updateTemplateComments(that.parent.model.toJSON(), true);
                            } else {

                                egov.message.error(result.message);
                                egov.pubsub.publish(egov.events.status.error, result.message);

                                egov.callback(callback);
                            }
                        },
                        error: function () {

                            // egov.message.error(egov.resources.document.sendComment.sendFail);
                            egov.pubsub.publish(egov.events.status.error, egov.resources.document.sendComment.sendFail);

                            egov.callback(callback);
                        }
                    });
                },
                true,
                that.model.get("Content"));
        },

        _remove: function (e) {
            if (e) {
                egov.helper.destroyClickEvent(e);
                egov.helper.hideAllContext();
                this._selected();
                var that = this;
                egov.request.deleteTemplateComments({
                    data: { id: that.model.get('id') },
                    success: function (result) {
                        if (result.success == true) {

                            // egov.message.success(result.message);
                            egov.pubsub.publish(egov.events.status.success, result.message);

                            that.parent.model.remove(that.model);
                            that.parent._bindView();
                            egov.dataManager.updateTemplateComments(that.parent.model.toJSON(), true);
                        } else {
                            // egov.message.error(result.message);
                            egov.pubsub.publish(egov.events.status.error, result.message);

                            egov.callback(callback);
                        }
                    },
                    error: function () {
                        // egov.message.error(egov.resources.document.sendComment.sendFail);
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.sendComment.sendFail);
                    }
                });
            }
        },

        _selected: function (e) {
            if (e) {
                if (e.ctrlKey) {
                    if (this.model.get("Selected")) {
                        this._unSetSelected();
                    } else {
                        this._setSelected();
                    }
                }
                else {
                    if (!this.model.get("Selected")) {
                        this.parent.unSelectedAll();
                    }
                    this._setSelected();
                }
            }
        },

        _setSelected: function () {
            this.model.set('Selected', true);
        },

        _unSetSelected: function () {
            this.model.set('Selected', false);
        },

        /// <summary>Chuột phải</summary>
        _contextmenu: function (e) {
            if (!e) {
                return;
            }
            egov.helper.destroyClickEvent(e);
            egov.helper.hideAllContext();
            this._selected(e);
            var that = this;
            var contextModel = new egov.models.contextMenu({
                trigger: 'right',
                data: that._getContextItems(e),
                style: {
                    "z-index": 99999
                },
                position: {
                    my: 'left top',
                    at: 'left bottom',
                    of: 'event'
                },
                hasCache: false,
                isShowLoading: false
            });

            this.$el.contextmenu(contextModel, e);
        },

        _getContextItems: function (e) {
            var contextItems = new egov.models.contextMenuList();
            var that = this;
            // Cho phép mở file để sửa
            contextItems.add([{
                text: egov.resources.templateComment.contextmenu.selected,
                value: 'selected',
                iconClass: 'icon-eye3',
                callback: function () {
                    that._append(e);
                }
            },
            {
                text: egov.resources.templateComment.contextmenu.edit,
                value: 'edit',
                iconClass: 'icon-info2',
                callback: function () {
                    that._edit(e);
                }
            }, {
                text: egov.resources.templateComment.contextmenu.delete,
                value: 'remove',
                iconClass: 'icon-cross',
                callback: function () {
                    that._remove(e);
                }
            }
            ]);

            return contextItems;
        }
    });

    return TemplateCommentView;
});