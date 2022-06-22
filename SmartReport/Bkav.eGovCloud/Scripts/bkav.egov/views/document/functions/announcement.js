define([
egov.template.document.announcement
], function (Template) {
    "use strict";

    var _resource = egov.resources.document.announcement;

    var AnnouncementView = Backbone.View.extend({
        template: Template,

        events: {
            'click .private-anoun .checkbox': 'uncheckPrivateAnoun'
        },

        initialize: function () {
            /// <summary>
            /// Khởi tạo.
            /// </summary>
            /// <returns type=""></returns>
            this.$el.html(this.template);
            this.$el.bindResources();
            this.$dg = this.$('.dg-view');
            this.$privateAnounc = this.$('.private-anoun ul:first');

            return this;
        },

        render: function (documentCopyId) {
            /// <summary>
            /// Mở form thông báo
            /// </summary>
            /// <param name="documentCopyId">Văn bản được chọn</param>
            /// <returns type=""></returns>
            this.$privateAnounc.empty();
            this.documentCopyId = documentCopyId;
            this._destroyDg();
            this._openDialog();
            return this;
        },

        uncheckPrivateAnoun: function (e) {
            /// <summary>
            /// Bỏ check đồng gửi
            /// </summary>
            /// <param name="e">event</param>
            if (egov.views.dg) {
                var target = $(e.target).parent().find(':checkbox');
                if (target.length === 0) return;
                egov.views.dg.uncheckPrivateAnoun(target.val());
                this._selectDg();
            }
        },

        _openDialog: function () {
            /// <summary>
            /// Mở dialog thông báo
            /// </summary>
            this._showDg();
            var that = this;

            this.$el.dialog({
                title: _resource.dialogTitle,
                width: "650px",
                height: "220px",
                draggable: true,
                buttons: [
                    {
                        text: _resource.announcementButton,
                        className: 'btn-primary',
                        click: function () {
                            that._announcement();
                        }
                    },
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that._destroyDg();
                            that.$el.dialog('hide');
                        }
                    }
                ]
            });
        },

        _showDg: function () {
            /// <summary>
            /// Hiển thị form đồng gửi
            /// </summary>
            var that = this;
            if (!that.$dg.is(':not(:empty)')) {
                if (!egov.views.dg) {
                    require(['transferExtend'], function (transferExtend) {
                        egov.views.dg = new transferExtend;
                        egov.views.dg.render(false, false, function () {
                            that._selectDg();
                        }, function () {
                            that.$dg.append(egov.views.dg.$el);
                        });
                    });
                }
                else {
                    egov.views.dg.render(false, false, function () {
                        that._selectDg();
                    });
                    that.$dg.append(egov.views.dg.$el);
                }
            }
        },

        _selectDg: function () {
            /// <summary>
            /// Hiển thị select đồng gửi
            /// </summary>
            this.$privateAnounc.empty();
            egov.views.dg.selectDg(this.$privateAnounc);
        },

        _announcement: function () {
            /// <summary>
            /// Gửi thông báo
            /// </summary>
            var destination = egov.views.dg.getDestination();

            // Lấy danh sách đồng gửi.
            var usersConsult = egov.views.dg.getUserConsults();

            if (usersConsult.length === 0) {
                // egov.message.warning(_resource.noReceiver);
                egov.pubsub.publish(egov.events.status.warning, _resource.noReceiver);
                return;
            }

            var that = this;

            // egov.message.processing(egov.resources.transfering, false);
            egov.pubsub.publish(egov.events.status.processing, egov.resources.transfering);

            egov.request.TransferAnnouncement({
                data: {
                    documentCopyId: this.documentCopyId,
                    ccUsers: usersConsult,
                    targetForComments: JSON.stringify(destination)
                },
                success: function (result) {
                    // success
                    if (result.error) {
                        // egov.message.error(_resource.sendFail);
                        egov.pubsub.publish(egov.events.status.error, _resource.sendFail);
                    } else {
                        // egov.message.success(_resource.sendSuccess);
                        egov.pubsub.publish(egov.events.status.success, _resource.sendSuccess);

                        that.$el.dialog('hide');
                        that._destroyDg();
                    }
                },
                error: function () {
                    // error
                    // egov.message.error(_resource.sendFail);
                    egov.pubsub.publish(egov.events.status.error, _resource.sendFail);
                }
            });
        },

        _resetForm: function () {
            this._destroyDg();
            $("#FilterDepartment").jstree("uncheck_node", $("#FilterDepartment").find("li"));
            $("#DeptForJobtitle").jstree("uncheck_node", $("#DeptForJobtitle").find("li"));
            $("#JobtitleForDept").find("input[type=checkbox]").removeAttr("checked");
        },

        _destroyDg: function () {
            ///<summay>
            /// Hủy bỏ hiển thị cây người dùng phòng ban, cây phong ban, cây chức danh phòng ban
            ///</summay>
            if (egov.views.dg) {
                egov.views.dg.destroy();
            }
        },
    });

    return AnnouncementView;
});