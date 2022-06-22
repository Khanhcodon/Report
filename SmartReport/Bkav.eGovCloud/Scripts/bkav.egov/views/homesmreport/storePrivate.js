define([
    'treeBase',
    'contextMenuView'
],

function (eGovTree) {
    "use strict";
    var _resource = egov.resources.document;

    //#region View

    /// <summary>Đối tượng view quản lý các tree: document tree, store tree, plugin tree</summary>
    var StoreTree = Backbone.View.extend({
        // Dom element
        el: "#menu-document",

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            this.callback = option.callback;
            this.parent = option.eGovTree;
            this.isLoadedStoreTree = false;
            this.selected = null;
            this.render();
        },

        render: function () {
            /// <summary>
            /// Render danh sách hồ sơ cá nhân của người dùng
            /// </summary>
            var that = this;
            that.isDialogShowing = false;
            egov.dataManager.getStorePrivate({
                success: function (data) {
                    if (data) {
                        that.model = data;
                        that._storeTree('#storeList', that.callback);
                    }
                    that.isLoadedStoreTree = true;
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.treeStore.message.error.selectStore);
                }
            });

            that.isLoadedStoreTree = true;
            return this;
        },

        renderDialog: function (selectFunction) {
            if ($('#storeDialog').length > 0) {
                $('#storeDialog').remove();
            }
            var that = this;

            that.selectFunction = selectFunction;
            that.isDialogShowing = true;
            that.storeDialogElement = $("<div id='storeDialog'>").append("<ul class='nav' style='margin: 0 -15px;'></ul>");
            that.storeDialogElement.appendTo("body");

            that._storeTree('#storeDialog ul', function (node) {
                that.selected = node.model.get("storePrivateId");
            });

            that.storeDialogElement.dialog({
                title: egov.resources.document.docStore.dialogTitle,
                draggable: true,
                width: "400px",
                height: "250px",
                buttons: [
                    {
                        text: egov.resources.document.docStore.saveButton,
                        className: 'btn-primary',
                        disableProcess: false,
                        click: function () {
                            if (that.selected == null || that.selected == 0) {
                                egov.pubsub.publish(egov.events.status.warning, egov.resources.document.docStore.noChooseStore);
                                return;
                            }

                            that.closeDialogAndRebind();
                            egov.callback(selectFunction, that.selected);
                        }
                    },
                    {
                        text: egov.resources.document.docStore.notSaveButton,
                        className: 'btn-info',
                        disableProcess: false,
                        click: function () {
                            that.closeDialogAndRebind();
                            egov.callback(selectFunction, null);
                        }
                    },
                    {
                        text: egov.resources.common.closeButton,
                        className: '',
                        disableProcess: true,
                        click: function () {
                            that.storeDialogElement.dialog("destroy");
                        }
                    }
                ]
            });

            var checked = egov.setting.userSetting.hasHideLuuSo ? "checked" : "";
            $("#storeDialog").parent().next().prepend('<div class="pull-left"><label class="checkbox document-color"><input id="hasHideSaveStore" name="checkbox[]" value="2378" type="checkbox" ' + checked + '><span class="document-color-1"><i class="icon-check"></i></span></label><span for="hasHideSaveStore">Bỏ hiển thị lưu sổ</span></div>');
            $("#hasHideSaveStore").change(function () {
                that._hasHideLuuSo($(this).is(":checked"))
            })
        },

        _hasHideLuuSo: function (hasHideLuuSo) {
            egov.request.hasHideSaveStore({
                data: { hasHideStore: hasHideLuuSo },
                success: function (data) {
                    if (data.status) {
                        egov.setting.userSetting.hasHideLuuSo = data.data;
                    }
                },
                error: function () {
                    egov.pubsub.publish("Có lỗi trong việc cấu hình lưu sổ");
                }
            });
        },

        closeDialogAndRebind: function () {
            if (this.storeDialogElement) {
                this.storeDialogElement.dialog("destroy");
                this.render();
            }
        },

        _storeTree: function (el, callback) {
            /// <summary>
            /// Hiển thị ra giao diên store
            /// </summary>
            /// <param name="result" type="object">Đối tượng store</param>
            var that = this;
            $(el).empty();
            var storePrivates = createObjectTree(that.model.storePrivate);
            var storeSharies = createObjectTree(that.model.storeShare);
            var stores = [
                {
                    id: 'StorePrivateRoot',
                    name: egov.resources.treeStore.nameStorePrivateRoot,
                    isStorePrivateRoot: true,
                    state: 'in',
                    children: storePrivates,
                    isStoreShared: false,
                    status: 0
                },
                {
                    id: 'StoreShareRoot',
                    name: egov.resources.treeStore.nameStoreShareRoot,
                    isStoreShareRoot: true,
                    state: 'in',
                    children: storeSharies,
                    isStoreShared: true,
                    status: 0
                }
            ];

            var treeModels = new egov.models.StorePrivateList(stores);
            that.storeTree = new eGovTree({
                el: el,
                model: treeModels,
                isDocumentTree: false,
                isStoreTree: true,
                isDroppable: true,
                open: function () {

                },
                select: function (node) { 
                    egov.callback(callback, node);
                },
                dblclick: function (node) {
                    that.selected = node.model.get("storePrivateId");
                    if (typeof that.selectFunction === "function") {
                        debugger
                        that.closeDialogAndRebind();
                        egov.callback(that.selectFunction, that.selected);
                    }
                },
                contextmenu: function (node) {
                    addContextMenu(node);
                },
                droppable: function (node) {
                    debugger;
                    var storePrivateId = node.attr("id");
                    var documentCopyId = egov.draggingDocumentCopyId;
                    that._addDocumentToStore(storePrivateId, documentCopyId);
                }
            });
        },

        _addDocumentToStore: function (storePrivateId, documentCopyId) {
            var that = this;
            if (storePrivateId && storePrivateId != 0 && documentCopyId) {
                egov.request.SaveDocumentToStorePrivate({
                    data: {
                        storePrivateId: storePrivateId,
                        documentCopyId: documentCopyId
                    },
                    success: function (result) {
                        egov.pubsub.publish(egov.events.status.success, _resource.docStore.success);
                        //that.$(el).dialog('hide');
                    },
                    error: function (error) {
                        egov.pubsub.publish(egov.events.status.error, _resource.docStore.error);
                    }
                });
            }
        }
    });

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

    function addContextMenu(node) {
        var contextSetting = {
            style: { zIndex: 9999 },
            position: {
                of: 'event'
            }
        };

        if (node.model.get('isStorePrivateRoot') || node.model.get('isStoreShareRoot')) {
            contextSetting.data = [{
                text: egov.resources.treeStore.contextmenu.createStore,
                icon: 'add.png',
                iconClass: 'icon-add-to-list',
                callback: function () {
                    createEditPrivateStore(node, true);
                }
            }];
        }
        else {
            var isActivated = node.model.get('status') == 0 ? true : false;
            ///Nếu là hồ sơ chia sẻ
            if (node.model.get('isPrivate') == false) {
                contextSetting.data = [{
                    text: egov.resources.treeStore.contextmenu.addStorePrivateAttachment,
                    icon: "add.png",
                    iconClass: 'icon-add-to-list',
                    callback: function () {
                        addStorePrivateAttachment(node.model.get('storePrivateId'));
                    }
                },
                {
                    text: "Thông tin hồ sơ",
                    icon: "edit.png",
                    iconClass: 'icon-picasa',
                    callback: function () {
                        createEditPrivateStore(node, false);
                    }
                }];
            }
            else {
                contextSetting.data = [
                    {
                        text: egov.resources.treeStore.contextmenu.createStore,
                        icon: "add.png",
                        iconClass: 'icon-add-to-list',
                        callback: function () {
                            createEditPrivateStore(node, true);
                        }
                    },
                    {
                        text: egov.resources.treeStore.contextmenu.addStorePrivateAttachment,
                        icon: "add.png",
                        iconClass: 'icon-add-to-list',
                        callback: function () {
                            addStorePrivateAttachment(node.model.get('storePrivateId'));
                        }
                    },
                    {
                        text: isActivated ? egov.resources.treeStore.contextmenu.closeStore : egov.resources.treeStore.contextmenu.openStore,
                        icon: isActivated ? "left_close.png" : "right_open.png",
                        iconClass: isActivated ? 'icon-lock' : 'icon-lock-open',
                        callback: function () {
                            var message = isActivated ? egov.resources.treeStore.contextmenu.messageCloseStore : egov.resources.treeStore.contextmenu.messageOpenStore
                            egov.message.show(message, null,
                                egov.message.messageButtons.YesNo,
                                function () {
                                    openOrCloseStore(node, node.model.get('storePrivateId'), isActivated);
                                },
                                null,
                                {
                                    width: "400px"
                                }
                            );
                        }
                    },
                    {
                        text: egov.resources.treeStore.contextmenu.updateStore,
                        icon: "edit.png",
                        iconClass: 'icon-picasa',
                        callback: function () {
                            createEditPrivateStore(node, false);
                        }
                    },
                    {
                        name: '---'
                    },
                    {
                        text: egov.resources.treeStore.contextmenu.deleteStore,
                        icon: "cancel.png",
                        iconClass: 'icon-trash',
                        callback: function () {
                            egov.message.show(
                                egov.resources.treeStore.message.confirm.deleteStore, null,
                                egov.message.messageButtons.YesNo,
                                function () {
                                    deleteStore(node, node.model.get('storePrivateId'));
                                },
                                null,
                                {
                                    width: "400px"
                                }
                            );
                        }
                    }
                ];
            }
        }

        node.$('a:first').contextmenu(contextSetting);
    }

    function createEditPrivateStore(node, isCreate) {
        var createOrUpdate; // = egov.views.home.tree.createOrUpdateStore;

        var settings = {
            width: 490,
            height: 365,
            title: isCreate ? egov.resources.treeStore.title.createStore : egov.resources.treeStore.title.detailSotore,
            buttons: [
                 {
                     text: isCreate ? egov.resources.common.addButton : egov.resources.common.updateButton,
                     className: 'btn-primary' + (node.model.get('isPrivate') == false ? " hidden" : ""),
                     click: function () {
                         var store = createOrUpdate.serialize().toJSON();
                         if (store === null) {
                             return;
                         }

                         egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

                         createOrUpdateStoreForm(node, createOrUpdate, isCreate, store);
                     }
                 },
                  {
                      text: egov.resources.common.cancelButton,
                      className: 'btn-close',
                      click: function () {
                          createOrUpdate.$el.dialog('hide');
                      }
                  }
            ],
            draggable: true,
            keyboard: true,
        };

        require(['newStorePrivateView'], function (NewStoreForm) {
            var model = isCreate ? new egov.models.StorePrivateModel() : node.model;

            if (isCreate) {
                model.set('parentId', node.model.get('storePrivateId'));
                model.set("userIdJoined", []);
            }

            if (!isCreate && model.get('isPrivate') == false) {
                // Thêm người tạo hồ sơ vào danh sách khi xem hồ sơ chia sẻ
                // Để biết được ai là người tạo
                model.get("userIdJoined").unshift(model.get("userCreatedId"));
            }

            createOrUpdate = new NewStoreForm({
                model: model,
                isCreate: isCreate
            });

            // egov.message.notification('hide');
            egov.pubsub.publish(egov.events.status.destroy);

            createOrUpdate.$el.dialog(settings);
            createOrUpdate.setFocus();
        });
    }

    function createOrUpdateStoreForm(node, createOrUpdate, isCreate, store) {
        var queryName = isCreate ? 'createStorePrivate' : 'updateStorePrivate';
        egov.request[queryName]({
            data: store,
            success: function (data) {
                if (data) {
                    if (data.error) {
                        egov.pubsub.publish(egov.events.status.error, data.message);
                    }
                    else if (data.validateMessage) {
                        createOrUpdate.$name.addClass('input-validation-error').focus().siblings('span').show();
                        egov.pubsub.publish(egov.events.status.error, data.validateMessage);
                    } else {
                        createOrUpdate.$el.dialog('hide');

                        if (isCreate) {
                            var stores = data.data;
                            var isFirst = true;
                            _.each(stores, function (store) {
                                var model = new egov.models.StorePrivateModel(store);
                                node.add(model, isFirst);
                                node.$children.addClass('in');
                                isFirst = false;
                            });
                        }
                        else {
                            node.model.set('name', store.storePrivateName);
                            node.model.set('userIdJoined', store.userIdJoined);
                            node.model.set('descStorePrivate', store.descStorePrivate);
                            node.model.set('isStoreShared', store.userIdJoined.length > 0 || store.descStorePrivate.length > 0);
                        }

                        egov.pubsub.publish(egov.events.status.success, isCreate ? "Tạo mới thành công!" : "Cập nhật thành công!");
                    }
                }
            },
            error: function () {
                var message = isCreate ? egov.resources.treeStore.message.error.createStore
                                       : egov.resources.treeStore.message.error.updateStore;
                egov.pubsub.publish(egov.events.status.error, message);
            }
        });
    }

    function createObjectTree(list) {
        //list = _.sortBy(list, function (o) { return o.Level; });
        for (var i = list.length - 1; i >= 0; i--) {
            var parent = _.find(list, function (o) { return o.id == list[i].parentId; });
            if (parent) {
                if (!parent.children) {
                    parent.children = [];
                }
                parent.children.unshift(list[i]);
                parent.state = 'closed';
            } else {
                list[i].root = true;
                list[i].state = '';
            }
        }
        list = _.filter(list, function (o) { return o.root; });
        return list;
    }

    function addStorePrivateAttachment(id) {
        require(['addStoreAttachment'], function (AddStoreAttachment) {
            var attachForm = new AddStoreAttachment({
                id: id
            });

            var settings = {
                title: egov.resources.treeStore.title.addStorePrivateAttachment,
                width: 425,
                height: 205,
                draggable: true,
                keyboard: true,
                buttons: [
                    {
                        text: egov.resources.common.addButton,
                        className: 'btn-primary',
                        click: function () {
                            if ($('#attachmentName').val() !== '') {
                                $('#uploadStorePrivateAttachment').click();
                            }
                        }
                    },
                    {
                        text: egov.resources.common.cancelButton,
                        click: function () {
                            attachForm.$el.dialog('destroy');
                        }
                    }
                ]
            };

            attachForm.$el.dialog(settings);
            attachForm.setFocus();
        });
    }

    function openOrCloseStore(node, storePrivateId, isActivated) {
        var queryName = isActivated ? 'closeStorePrivate' : 'openStorePrivate';
        egov.request[queryName]({
            data: { id: storePrivateId },
            success: function (data) {
                if (data) {
                    if (data.error) {
                        egov.pubsub.publish(egov.events.status.error, data.message);
                    } else {
                        if (isActivated) {
                            node.$el.find('a:first').removeClass('active').addClass('disable');
                            node.model.set('status', 1);
                        } else {
                            node.$el.find('a:first').removeClass('disable').addClass('active');
                            node.model.set('status', 0);
                        }
                        egov.pubsub.publish(egov.events.status.success,
                                isActivated
                                ? egov.resources.treeStore.message.success.closeStore
                                : egov.resources.treeStore.message.success.openStore);
                    }
                }
            },
            error: function () {
                egov.pubsub.publish(egov.events.status.error,
                    isActivated
                        ? egov.resources.treeStore.message.error.closeStore
                        : egov.resources.treeStore.message.error.openStore);
            }
        });
    }

    function deleteStore(node, storePrivateId) {
        egov.request.deleteStorePrivate({
            data: { id: storePrivateId },
            success: function (data) {
                if (data) {
                    if (data.error) {
                        egov.pubsub.publish(egov.events.status.error, data.message);
                    } else {
                        egov.pubsub.publish(egov.events.status.success, egov.resources.treeStore.message.success.deleteStore);
                        node.remove();
                    }
                }
            },
            error: function () {
                egov.pubsub.publish(egov.events.status.error, egov.resources.treeStore.message.error.deleteStore);
            }
        });
    }

    //#endregion

    return StoreTree;
});