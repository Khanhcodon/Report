define([
    egov.template.document.anticipate
],
function (AnticipateTemplate) {
    "use strict";

    egov.models.userAction = Backbone.Model.extend({
        defaults: {
            id: 0,
            value: 0,
            label: '',
            fullname: '',
            department: '',
            username: '',
            position: '',
            isMainProcess: false,
            isCoProcess: false
        },

        initialize: function () {
            this.set('id', this.get('value'));
        }
    });

    egov.models.userActionCollection = Backbone.Collection.extend({
        model: egov.models.userAction
    });

    var Anticipate = Backbone.View.extend({
        tagName: "div",

        events: {
            'change #dropdownAction': '_changeAction',
            'change #dropdownUserTransfer': '_changeUserTransfer',
            'change #dropdownActionPlan': '_changeActionPlan',
        },

        /// <summary>Khởi tạo</summary>
        initialize: function () {
            this.destinationPlan = null;
            this.$el.append(AnticipateTemplate);

            this.$el.bindResources();

            return this;
        },

        render: function (options) {
            this._clear();
            this.documentCopyId = options.documentCopyId || 0;
            this.doctypeId = options.doctypeId;
            this.isCreatingDocument = options.documentCopyId <= 0;
            this._openDialog();
            return this;
        },

        _openDialog: function () {
            var that = this;
            var nameQuery = this.isCreatingDocument ? "getDocumentCreateAction" : "getDocumentEditAction";
            var data = this.isCreatingDocument
                        ? { documentTypeId: this.doctypeId, isPhanloai: false }
                        : { documentCopyId: this.documentCopyId, isGetSpecials: false };

            egov.request[nameQuery]({
                data: data,
                success: function (result) {
                    if (!result) {
                        // egov.message.error("Có lỗi trong quá trình.");
                        egov.pubsub.publish(egov.events.status.error, egov.resources.common.error);

                        return;
                    }

                    if (result.error) {
                        // egov.message.error(result.error);
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }

                    that.actions = _.filter(result, function (item) { return !item.isspecial; });
                    if (that.actions.length > 0) {
                        $.each(that.actions, function (i, item) {
                            that.$('#dropdownAction').append('<option value="' + item.id + '">' + item.name + '</option>');
                        });
                    }
                    that._renderDialog();
                },
                error: function (xhr) {
                    console.log(xhr.message);
                }
            });
        },

        _renderDialog: function () {
            var that = this;
            var dialogSetting = {
                title: egov.resources.document.anticipate.name,
                width: '790px',
                height: "275px",
                draggable: true,
                keyboard: true,
                buttons: [
                    {
                        id: "addAnticipate",
                        text: egov.resources.document.addAnticipate,
                        className: 'btn-primary',
                        disableProcess: true,
                        click: function (callback) {
                            if (egov.transferPlanForm) {
                                that.destinationPlan = egov.transferPlanForm.serialize();
                            }
                            that.$el.dialog('destroy');
                        }
                    },
                    {
                        id: "closeAnticipate",
                        text: egov.resources.main.closeBtn,
                        className: 'btn-close',
                        click: function () {
                            that.$el.dialog('destroy');
                        }
                    }
                ]
            };

            this.$el.dialog(dialogSetting);
        },

        _clear: function () {
            this.$(".transferplan-form").empty();
            this.actions = null;
            this.selectedAction = null;
            this.userTranfers = null;
            this.selectedUser = null;
            this.actionsPlans = null;
            this.selectedActionsPlan = null;
            this.$("#dropdownAction").empty().append('<option value="">' + egov.resources.document.anticipate.choosereceive + '</option>');
            this.$("#dropdownUserTransfer").empty().append('<option value="">' + egov.resources.document.anticipate.choosereceiver + '</option>');
            this.$("#dropdownActionPlan").empty().append('<option value="">' + egov.resources.document.anticipate.chooseanticipate + '</option>');
        },

        _changeAction: function (e) {
            ///<summary>
            /// Thay đổi hướng chuyển của người dùng hiện tại
            ///</summary>
            if (!e) {
                return;
            }

            this.$(".transfer-form").empty();
            var that = this;
            var actionId = this.$("#dropdownAction").val();

            if (!actionId) {
                return;
            }

            this.selectedAction = _.find(this.actions, function (item) { return item.id === actionId; });
            if (this.selectedAction) {
                this.userTranfers = null;
                this.selectedUser = null;

                var data = {
                    actionId: this.selectedAction.id,
                    workflowId: this.selectedAction.workflowId,
                    documentCopyId: this.documentCopyId
                };

                egov.request.getUserByAction({
                    data: data,
                    success: function (result) {
                        if (!result) {
                            return;
                        }

                        that.userTranfers = result;
                        that.$("#dropdownUserTransfer").empty().append('<option value="">Chọn người nhận</option>');
                        $.each(result, function (i, item) {
                            that.$("#dropdownUserTransfer").append('<option value="' + item.value + '">' + item.label + '</option>');
                        });
                    },
                    error: function (xhr, status, error) {
                        // egov.message.show('Bạn không có quyền xử lý văn bản!');
                        egov.pubsub.publish(egov.events.status.error, 'Bạn không có quyền xử lý văn bản!');
                    }
                });
            }
        },

        _changeUserTransfer: function (e) {
            ///<summary>
            /// Chọn người nhận tiếp trong hướng chuyển
            ///</summary>

            if (!e) {
                return;
            }

            this.$(".transfer-form").empty();
            if (!this.userTranfers || !this.selectedAction) {
                return;
            }

            var that = this;
            var userId = this.$('#dropdownUserTransfer').val();
            if (!userId) {
                return;
            }

            this.selectedUser = _.find(this.userTranfers, function (item) {
                return item.value == userId;
            });

            if (!this.selectedUser) {
                return;
            }

            egov.request.getActionsTransferPlan({
                data: {
                    workflowId: this.selectedAction.workflowId,
                    userId: this.selectedUser.value,
                    currentNodeId: this.selectedAction.nextNodeId
                },
                success: function (result) {
                    if (!result) {
                        // egov.message.show('Bạn không có quyền xử lý văn bản!');
                        egov.pubsub.publish(egov.events.status.error, 'Bạn không có quyền xử lý văn bản!');
                        return;
                    }
                    that.actionsPlans = result;
                    that.$("#dropdownActionPlan").empty().append('<option value="">Chọn hướng dự kiến</option>');
                    $.each(result, function (i, item) {
                        that.$("#dropdownActionPlan").append('<option value="' + item.id + '">' + item.name + '</option>');
                    });
                }
            });
        },

        _changeActionPlan: function (e) {
            ///<summary>
            /// Lấy hướng chuyển của người dùng đã chọn
            ///</summary>
            if (!e) {
                return;
            }

            this.$(".transfer-form").empty();
            if ((!this.actions
                || !this.selectedAction)
                || (!this.userTranfers
                || !this.selectedUser)) {
                return;
            }

            var actionPlanId = this.$('#dropdownActionPlan').val(),
                that = this;
            this.actionsPlan = _.find(this.actionsPlans, function (item) {
                return item.id == actionPlanId;
            });

            if (this.actionsPlan) {
                egov.request.getUserByAction({
                    data: {
                        actionId: that.actionsPlan.id,
                        workflowId: that.actionsPlan.workflowid,
                        documentCopyId: 0,
                        userId: that.selectedUser.value
                    },
                    success: function (result) {
                        if (!result) {
                            // egov.message.error("Không có hướng chuyển nào.");
                            egov.pubsub.publish(egov.events.status.error, "Không có hướng chuyển nào.");
                            return;
                        }

                        that.model = new egov.models.actionUserList(result);
                        that._bindView();
                    }
                });
            }
        },

        _bindView: function () {
            /// <summary>
            /// Hiển thị lên form ban giao theo cấu hình
            ///</summary>
            this.$(".transferplan-form").empty();
            if (this.model && this.model.length > 0) {
                var that = this;
                require(['transfer'], function (Transfer) {
                    egov.transferPlanForm = new Transfer;
                    egov.transferPlanForm.renderAnticipate({
                        action: that.actionsPlan,
                        model: that.model,
                        parent: that,
                        callback: function (html) {
                            that.$(".transferplan-form").append(html);
                        }
                    });
                });
            }
        },

        getDestinationPlan: function () {
            /// <summary>
            /// Lấy cấu hình dự kiến chuyển
            ///</summary>
            return this.destinationPlan;
        }
    });

    return Anticipate;
});