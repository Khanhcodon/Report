define([egov.template.document.multi], function (Template) {

    var template = `<div class='document-list row'></div>
                    <div class ='actions-multi row'>
                        <textarea class ='form-control input-sm pull-left' id='commonComment' rows="1" placeholder="Nhập ý kiến chung"></textarea>
                        <button class ="btn btn-default btn-sm pull-right dropdown-toggle btnClose" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            Đóng</button>
                        <div class ="btn-group dropup pull-right">
                            <button class ="btn btn-primary btn-sm dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            Chuyển</button>
                            <ul class ="dropdown-menu dropdown-menu-right actions" style="width: 250px;">
                            </ul>
                        </div>
                    </div>`;
    var insertFileTemplate = ` <li class="fileinput-button list-group-item btnInsertAttachment" title="Thêm đính kèm">
                                    <a href="#">+</a>
                                    <input type="file" class="fileupload" data-url="/Attachment/UploadTemp"
                                           multiple="multiple" name="files" style="cursor: default;" />
                                </li>`;

    var fileUploadSetting = egov.setting.fileUploadSettings;
    var _resource = egov.resources;

    var MultiDocument = Backbone.View.extend({
        className: 'multi-document',
        events: {
            "click .actions li": "_openTransfer",
            "click .btnClose": "_removeDialog",
            "click .btnRemove": "_remove"
        },

        initialize: function (options) {
            this.documentCopyIds = options.ids;
            this.actions = options.actions;
            this._renderTemplate();
            this.isTransferTheoLo = true;

            this._getData(this.render.bind(this));
        },

        render: function (model) {
            this.model = model;
            this._renderDialog();
            this._renderActions();

            _.each(this.model, function (document) {
                this._renderDocument(document);
            }.bind(this));
        },

        getAttachmentsForSign: function () {
            var result = [], docAttachments;
            _.each(this.model, function (document) {
                docAttachments = document.attachments.model.models;
                if (docAttachments.length === 0) return;

                $.each(docAttachments, function (i, item) {
                    if (egov.fileExtension.isForSign(item.get("Name")) && item.get("hasRemoved") !== true && item.get("isRemoved") !== true) {
                        item.set("DocumentCopyId", document.DocumentCopyId);
                        item.set("Compendium", document.Compendium);
                        result.push(item);
                    }
                });
            }.bind(this));

            return result;
        },

        uploadSignFiles: function (signedFiles, success) {
            var that = this, newSignFiles = [];
            var groupByDocument = _.groupBy(signedFiles, "documentCopyId");
            var total = _.keys(groupByDocument).length;
            var idx = 0;
            _.each(groupByDocument, function (files, documentCopyId) {
                that._updateSignFileByDocument(files, documentCopyId, function () {
                    idx++;
                    if (idx == total) {
                        that._confirmAttachments(function () {
                            that._serialize();
                            success();
                        });
                    }
                });
            });
        },

        _updateSignFileByDocument: function (signedFiles, documentCopyId, success) {
            var that = this, newSignFiles = [];
            var document = _.find(that.model, function (d) {
                return d.DocumentCopyId == documentCopyId;
            });

            if (!document) {
                egov.callback(success);
                return;
            };

            _.each(signedFiles, function (signedFile) {

                var oldAttachment = document.attachments.model.find(function (item) {
                    return String(item.get("Id")) === String(signedFile.id);
                });

                var fileName = oldAttachment.get("Name");

                var isSignedFile = fileName.endWith("_Signed.pdf", true);
                if (!isSignedFile || oldAttachment.get("isNew")) {
                    // Nếu mớới thêm hoặc file gốc là doc thì thêm mới file ký
                    newSignFiles.push(signedFile);
                } else {
                    // Nếu file gốc là pdf thì tạo phiên bản mới.
                    document.attachments.modifiedFiles[signedFile.id] = signedFile.value;
                }
            });

            if (newSignFiles.length === 0) {
                egov.callback(success);
                return;
            }

            egov.request.uploadTempScan({
                data: {
                    files: JSON.stringify(newSignFiles)
                },
                success: function (result) {
                    $.each(result, function (index, file) {
                        if (file.error !== "") {
                            egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                        } else {
                            var newAttachment = new egov.models.attachment({
                                Id: file.key,
                                Name: file.name,
                                Size: file.size,
                                Extension: file.extension,
                                Versions: [{
                                    Version: 1,
                                    User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                                }],
                                isNew: true,
                            });

                            document.attachments.model.add(newAttachment);
                        }
                    });
                    egov.callback(success);
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, "Lỗi trong quá trình upload file đã kí lên server! Vui lòng thử lại.");
                    throw "";
                }
            });
        },

        _renderTemplate: function () {
            this.$el.html(template);
            this.$documentContainer = this.$('.document-list');
            this.$actionContainer = this.$('.actions-multi ul');
        },

        _renderDocument: function (document) {
            this.$documentContainer.append($.tmpl(Template, document));
            var $documentElement = this.$('#doc-' + document.DocumentCopyId);
            var $commentElement = $documentElement.find('#commentList');
            var $attachmentElement = $documentElement.find("#wrapAttachment");

            this._renderAttachments($attachmentElement, document);
            this._renderComments($commentElement, document);
        },

        _renderActions: function () {
            this.$actionContainer.html($.tmpl('<li class="list-group-item" data-id="${id}">${name}</li>', this.actions));
        },

        _renderAttachments: function (container, document) {
            /// <summary>
            /// Hiển thị các file đính kèm của document
            /// </summary>
            var that = this;
            require(['docAttachment'], function (Attachment) {
                var attachs = new egov.models.attachmentCollection(_.where(document.Attachments, { "isRemoved": false }));

                document.attachments = new Attachment({
                    hasPermission: true,
                    model: attachs,
                    el: container,
                    documentId: document.DocumentId,
                    documentCopyId: document.DocumentCopyId,
                    storePrivateId: document == undefined ? null : document.StorePrivateId,
                    isTheoLo: true
                });

                container.find('ul').append(insertFileTemplate);

                that._insertAttachment(container, document);
            });
        },

        _insertAttachment: function (container, document) {
            /// <summary>
            /// Thêm tài liệu đính kèm
            /// </summary>
            var that = this;
            var newAttachment;
            var existFile;

            container.find('.fileupload').fileupload({
                dataType: 'json',
                pasteZone: null,
                add: function (e, data) {
                    var file = data.files[0];
                    var msg = "";
                    if (fileUploadSetting.maxFileSize && file.size > fileUploadSetting.maxFileSize) {
                        msg += file.name + ": " + _resource.file.lenghtIsNotAllow + _resource.file.maxLength + fileUploadSetting.maxFileSize / 1048576 + "Mb";
                    } else if (!(egov.setting.acceptFileTypes.test(file.type) || egov.setting.acceptFileTypes.test('.' + file.name.split('.').pop()))) {
                        msg += file.name + ": " + _resource.file.typeIsNotAllow;
                    } else {
                        newAttachment = new egov.models.attachment({
                            Id: 0,
                            Name: file.name,
                            Size: file.size,
                            Extension: '.' + file.name.split('.').pop(),
                            Versions: [{
                                Version: 1,
                                User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                            }],
                            isNew: true,
                            isUploading: true
                        });

                        data.attachment = newAttachment;
                        document.attachments.model.add(newAttachment);
                    }
                    if (msg !== "") {
                        egov.pubsub.publish(egov.events.status.error, msg);
                    } else {
                        data.submit();
                    }
                },
                start: function () {
                    // egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                },
                progress: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    if (data.attachment && data.attachment.view) {
                        data.attachment.view.$(".progress-bar").css("width", progress + "%");
                    }
                },
                stop: function (e, data) {
                    egov.pubsub.publish(egov.events.status.destroy);
                },
                done: function (e, data) {
                    $.each(data.result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            document.attachments.model.remove(data.attachment);
                            egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                        } else {
                            var newFile = document.attachments.model.detect(function (f) {
                                return f.get('isNew') && f.get('Name') === file.name;
                            });
                            if (newFile) {
                                newFile.set('Id', file.key);
                                newFile.set('isUploading', false);
                            }
                        }
                    });
                },
                fail: function (e, data) {
                    egov.pubsub.publish(egov.events.status.error, _resource.file.errorUpload);
                    $(data.id).remove();
                }
            });
        },

        _renderComments: function (container, document) {
            var that = this;
            var comments = parseComment(document.CommentList, egov.setting.allUsers);
            var lastComment = _.last(comments);

            require([egov.template.document.commentMulti], function (CommentMobileTemplate) {
                container.append($.tmpl(CommentMobileTemplate, lastComment));

            });
        },

        _getData: function (success) {
            egov.request.getMultiDocument({
                data: {
                    ids: this.documentCopyIds
                },
                success: function (result) {
                    success(result.documents);
                },
                error: function () { }
            });
        },

        _renderDialog: function () {
            var that = this;
            var dialogOptions = {
                title: "Duyệt và xử lý văn bản theo lô",
                draggable: true,
                width: "1024px",
                height: '450px'
            };
            this.$el.dialog(dialogOptions);
        },

        _removeDialog: function () {
            this.$el.dialog("destroy");
        },

        _remove: function (e) {
            var doc = $(e.target).parents('.document').remove();
        },

        _hideDialog: function () {
            this.$el.dialog("hide");
        },

        _showDialog: function () {
            this.$el.dialog("show");
        },

        _openTransfer: function (e) {
            var that = this;
            var target = $(e.target).closest('li');
            var actionId = target.attr('data-id');
            var transferAction = _.find(this.actions, function (a) {
                return a.id === actionId;
            });

            this._confirmAttachments(function () {
                this._serialize();
                require(['transfer'], function (Transfer) {
                    if (egov.transferForm === undefined) {
                        egov.transferForm = new Transfer;
                    }

                    that._hideDialog();

                    egov.transferForm.renderTheoLo({
                        action: transferAction,
                        documentCopyIds: that.documentCopyIds,
                        comment: that.comments,
                        document: that,
                        callback: function () {
                            that._removeDialog();
                        },
                        callbackCloseForm: function () {
                            that._showDialog();
                        }
                    });
                });
            }.bind(this));
        },

        _confirmAttachments: function (success) {
            var total = this.model.length;
            var idx = 0;
            _.each(this.model, function (document) {
                document.attachments.confirmAttachments(function () {
                    idx++;
                    if (idx === total) {
                        success();
                    }
                });
            }.bind(this));
        },

        _serialize: function () {
            var commonComment = this.$("#commonComment").val();
            var documentCopyId, comment, doc, modifiedFiles, selectedFiles, removeFiles;
            this.comments = {};
            this.modifyAttachments = {};
            this.documentCopyIds = [];
            this.newFiles = {};
            this.removeFiles = {};

            var that = this;
            this.$(".document-list .document").each(function () {
                selectedFiles = {}
                documentCopyId = $(this).attr('data-id');
                comment = $(this).find(".comment").val();
                if (String.isNullOrEmpty(comment) && !String.isNullOrEmpty(commonComment)) {
                    comment = commonComment;
                }

                that.documentCopyIds.push(documentCopyId);
                that.comments[documentCopyId] = comment;


                doc = _.find(that.model, function (d) { return d.DocumentCopyId == documentCopyId });
                modifiedFiles = doc.attachments.modifiedFiles;
                // File mới
                doc.attachments.model.each(function (file) {
                    if (file.get('isNew') && !file.get('isRemoved')) {
                        selectedFiles[file.get('Id')] = { name: file.get('Name') }
                    }
                });

                removeFiles = doc.attachments.model.select(function (file) {
                    return file.get('isRemoved');
                });
                removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });

                // Cập nhật nội dung file đã sửa với những file vừa upload lên.
                // Đồng thời xóa file đó trong danh sách file đang chỉnh sửa.
                $.each(selectedFiles, function (keyname, value) {
                    if (modifiedFiles[keyname]) {
                        value.content = modifiedFiles[keyname];
                        delete modifiedFiles[keyname];
                    }
                });

                that.newFiles[documentCopyId] = selectedFiles;
                that.removeFiles[documentCopyId] = removeFiles;
                that.modifyAttachments[documentCopyId] = modifiedFiles;
            });
        }
    });

    var commentType = { Common: 1, Consulted: 2, Contribution: 3, Supplementary: 4, Signed: 5, Success: 6, Finished: 7, Reopen: 8 };
    function parseComment(comments, allUsers) {
        var result = [];
        var content;
        var commentTypeEnum = commentType;
        var idx = 1;
        _.each(comments, function (comment) {
            if (typeof comment.Content == "string") {
                comment.Content = comment.Content.split('\\n').join('<br />');
                content = JSON.parse(comment.Content);
            }

            comment.Description = "";

            if (comment.CommentType == commentTypeEnum.Common) {
                var transfers = content.Transfers;

                transfers = _.sortBy(transfers, function (item) { return item.type; });
                comment.Content = content;
                if (comment.Content.Transfers.length == 1 && comment.Content.Transfers[0].type == "2") {
                    comment.Content.Transfers[0].type = "1"; // gửi 1 đồng xử lý thì hiển thị là xử lý chính
                }

                if (comment.Content.Transfers.length > 0) {
                    comment.Description = String.format("Tới {0}", transfers[0].label);
                    if (transfers.length > 1) {
                        var otherNumber = 0;
                        for (var j = 1; j < transfers.length; j++) {
                            var peoples = transfers[j].label;
                            otherNumber += peoples.split(",").length;
                        }

                        comment.Description += String.format(" và {0} ĐXL", otherNumber);
                    }
                } else {
                    comment.Description = "Hệ thống gửi";
                }
            }

            if (comment.CommentType == commentTypeEnum.Consulted) {
                content = JSON.parse(comment.Content);
                comment.Content = content;
                comment.Content.Transfers = [];
                comment.Description = "Trả lời đồng xử lý";
            }

            if (comment.CommentType == commentTypeEnum.Contribution) {
                comment.Content = JSON.parse(comment.Content);
                comment.Content.Transfers = [];
                comment.Description = "Trả lời xin ý kiến";
            }

            if (comment.CommentType == commentTypeEnum.Finished || comment.CommentType == commentTypeEnum.Reopen) {
                comment.Content = JSON.parse(comment.Content);
                comment.Content.Transfers = [];
            }

            comment.UserSend = _.find(allUsers, function (user) {
                return user.value === comment.UserSendId;
            }) || {};
            comment.avatar = comment.UserSend.avatar;

            comment.Date = Date.parseFromIsoString(comment.DateCreated).relativeDate();
            comment.stt = idx++;
            result.push(comment);
        });
        return result;
    }

    return MultiDocument;
});