define([
egov.template.document.consult
], function (ConsultTemplate) {
    "use strict";

    var _resource = egov.resources.document.consult;

    /// <summary> Class hỗ trợ xin ý kiến</summary>
    var ConsultView = Backbone.View.extend({
        template: ConsultTemplate,
        events: {
            'click .private-anoun .checkbox': 'uncheckPrivateAnoun'
        },

        initialize: function () {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <returns type="">this</returns>
            this.$el.html(this.template);
            this.$el.bindResources();

            // Giá trị xác định form xin ý kiến hiện tại là cho context menu (false: cho toolbar)
            this.forContext = false;
            this.$dg = this.$('.dg-view');
            this.$privateAnounc = this.$('.private-anoun ul:first');
            this.$comment = this.$('.comment');
            return this;
        },

        render: function (option) {
            /// <summary>
            /// Hiển thị form xin ý kiến
            /// </summary>
            /// <param name="option">Param</param>
            /// <returns type=""></returns>
            this._clear();
            this.document = option.document;
            this._openDialog();
        },

        renderForContext: function (documentCopyId) {
            /// <summary>
            /// Hiển thị form chuyển ý kiến cho contextmenu
            /// </summary>
            /// <param name="documentCopyId">DocumentCopyId được chọn</param>
            this._clear();
            this.forContext = true;
            this.documentCopySelected = documentCopyId;
            this._openDialog();
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
                width: "780px",
                height: "290px",
                draggable: true,
                buttons: [
                   {
                       text: _resource.consultButton,
                       className: 'btn-primary',
                       click: function () {
                           that._consult();
                       }
                   },
                    {
                        text: egov.resources.common.closeButton,
                        className: 'btn-close',
                        click: function () {
                            that.$el.dialog('hide');
                        }
                    }
                ]
            });
            this.$el.bindResources();
        },

        _consult: function () {
            /// <summary>
            /// Gửi xin ý kiến
            /// </summary>
            // Lấy danh sách đồng gửi. 

            var comment = this.$comment.val();
            if (comment === '') {
                egov.message.warning(_resource.noComment);
                this.$comment.focus();
                return;
            }

            var usersConsult = egov.views.dg.getUserConsults();
            if (!usersConsult || usersConsult.length === 0) {
                egov.message.warning(_resource.noReceiver);
                return;
            }

            var destination = egov.views.dg.getDestination();
            _.each(destination, function (des) {
                des.type = egov.enum.transferType.xyk;
            });
            
            if (this.forContext) {
                this._consultForContext(comment, usersConsult, destination);
            } else {
                var doc = this.document.serialize(),// Lấy Document.              
                    selectedFiles = {};  // Lấy danh sách file được thêm vào

                if (this.document.attachments
                    && (this.document.attachments.model
                    && this.document.attachments.model.length > 0)) {
                    this.document.attachments.model.each(function (file) {
                        if (file.get('isNew')) {
                            selectedFiles[file.get('Id')] = { name: file.get('Name') }
                        }
                    });
                }

                // Lấy danh sách file được xóa đi.
                var removeFiles = this.document.attachments.model.select(function (file) {
                    return file.get('isRemoved');
                });

                this._consultFortolbar(doc, comment, usersConsult, selectedFiles, removeFiles, destination);
            }
        },

        _consultFortolbar: function (doc, comment, usersConsult, selectedFiles, removeFiles, destination) {
            /// <summary>
            /// Gửi xin ý kiến trên toolbar
            /// </summary>
            var that = this;
            egov.message.processing(egov.resources.common.transfering, false);
            egov.request.TransferConsult({
                data: {
                    "doc": JSON.stringify(doc),
                    "files": JSON.stringify(selectedFiles),
                    "modifiedFiles": JSON.stringify(this.document.attachments.modifiedFiles),
                    removeAttachmentIds: removeFiles,
                    usersConsult: usersConsult,
                    contentRequest: comment,
                    targetForComments: JSON.stringify(destination)
                },
                success: function (result) {
                    if (result.success) {
                        egov.message.success(_resource.sendSuccess);
                        that.$el.dialog('hide');
                    } else {
                        egov.message.error(_resource.sendFail);
                    }
                },
                error: function () {
                    egov.message.error(_resource.sendFail);
                }
            });
        },

        _consultForContext: function (comment, usersConsult, destination) {
            /// <summary>
            /// Gửi xin ý kiến khi form mở cho contextmenu
            /// </summary>
            /// <param name="comment">Nội dung xin ý kiến</param>
            /// <param name="userconsult">Danh sách người nhận</param>
            /// <param name="destination">Target comment</param>
            egov.message.processing(egov.resources.common.transfering, false);
            egov.request.TransferConsultContext({
                data: {
                    documentCopyId: this.documentCopySelected,
                    usersConsult: usersConsult,
                    contentRequest: comment,
                    targetForComments: JSON.stringify(destination)
                },
                success: function (result) {
                    if (result.success) {
                        egov.message.success(_resource.sendSuccess);
                    } else {
                        egov.message.error(_resource.sendFail);
                    }
                },
                error: function () {
                    egov.message.error(_resource.sendFail);
                }
            });
        },

        _showDg: function () {
            /// <summary>
            /// Hiển thị form đồng gửi
            /// </summary>
            var that = this;
            if (!that.$dg.is(':not(:empty)')) {
                require(['transferExtend'], function (transferExtend) {
                    if (!egov.views.dg) {
                        egov.views.dg = new transferExtend;
                    }

                    egov.views.dg.render(false, false, function () {
                        that._selectDg();

                    }, function () {
                        that.$dg.append(egov.views.dg.$el);
                    });

                    that.$comment.focus();
                });
            }
        },

        _selectDg: function () {
            /// <summary>
            /// Hiển thị select đồng gửi
            /// </summary>
            this.$privateAnounc.empty();
            if (egov.views.dg) {
                egov.views.dg.selectDg(this.$privateAnounc);
            }
        },

        _clear: function () {
            /// <summary>
            /// Chuyển các value được xét trong quá trình thao tác về mặc định
            /// </summary>
            this._destroyDg();
            this.$privateAnounc.empty();
        },

        _destroyDg: function () {
            ///<summay>
            /// Hủy bỏ hiển thị cây người dùng phòng ban, cây phong ban, cây chức danh phòng ban
            ///</summay>
            if (egov.views.dg) {
                egov.views.dg.destroy();
            }
        }
    });

    return ConsultView;
});