define(function () {

    "use strict";

    var _resource = egov.resources.document.attachment;
    var imgExt = ['.jpg', '.jpeg', '.bmp', '.png', '.ico', '.gif'];

    //#region Model

    egov.models.attachment = Backbone.Model.extend({
        defaults: {
            Id: 0,
            Name: '',
            Extension: '',
            Size: 0,
            Versions: [],
            fileData: undefined,
            isRemoved: false,
            hasRemoved: false,
            isNew: false,
            isMofified: false,
            isOpen: false,
            icon: '',
            base64: '',
            isPhatHanhOrKetThuc: false //HopCV:dùng cho việc thêm xóa văn bản đã được phát hành hoặc kết thúc 
        },

        initialize: function () {
            var extension = this.get('Extension');
            if (extension.indexOf('.') !== 0) {
                extension = "." + extension;
            }
            var icon;
            if (extension === '.doc' || extension === '.docx') {
                icon = 'icon-file-word';
            }
            else if (extension === '.xls' || extension === '.xlsx') {
                icon = 'icon-file-excel';
            }
            else if (extension === '.pdf') {
                icon = 'icon-file-pdf';
            }
            else if (extension === '.txt') {
                icon = 'icon-text';
            }
            else if (extension === '.zip' || extension === '.rar' || extension === '.7z') {
                icon = 'icon-file-zip';
            }
            else if (extension === '.ppt' || extension === '.pptx') {
                icon = 'icon-file-powerpoint';
            }
            else if (extension === '.html') {
                icon = 'icon-chrome';
            }
            else if (imgExt.indexOf(extension) != -1) {
                icon = 'icon-image2';
            }
            else {
                icon = 'icon-file4';
            }
            this.set('icon', icon);
        }
    });

    egov.models.attachmentCollection = Backbone.Collection.extend({
        model: egov.models.attachment
    });

    //#endregion

    //#region Views

    /// <summary>Đối tượng view thể hiện danh sách các file đính kèm của văn bản</summary>
    var AttachmentsView = Backbone.View.extend({

        modifiedFiles: {},

        className: ".document-attachment",

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="options" type="object">Các đối tượng {el: container}</param>
            this.$el = options.el;
            this.hasPermission = options.hasPermission;
            this.documentId = options.documentId;
            this.documentCopyId = options.documentCopyId;
            this.storePrivateId = options.storePrivateId;

            var that = this,
                attachment;
            this.model.on('add', function (newItem, model) {
                attachment = new AttachmentItem({
                    model: newItem,
                    parent: that
                });

                if (that.$el.closest(that.className).is(":hidden")) {
                    that.$el.closest(that.className).show();
                }
                attachment.$el.addClass("newFile");
                that.$el.find("ul").append(attachment.$el);
            }, this);

            this.model.on('remove', function (removed) {
                removed.view.remove();
            }, this);

            this.render();

            this.hasSupportedFileReader = window.File && window.FileReader && window.FileList && window.Blob;

            return this;
        },

        render: function () {
            /// <summary>
            /// Hiển thị danh sách file đính kèm.
            /// </summary>
            var that = this,
                attachment;

            this.model.each(function (att) {
                attachment = new AttachmentItem({
                    model: att,
                    parent: that
                });

                that.$el.find("ul").append(attachment.$el);
            });
        },

        confirmAttachments: function (callback) {
            /// <summary>
            /// Lưu lại các file đang được sửa.
            /// </summary>
            /// <param name="callback">Hàm thực thi sau khi hoàn thành.</param>
            var that = this;
            if (that.model.length == 0 || egov.isMobile) {
                egov.callback(callback);
                return;
            }
        },

        updateAttachment: function (itemId, file) {
            var that = this;
            if (this.hasSupportedFileReader) {
                var msg = "";
                if (egov.setting.maxFileSize && file.size > egov.setting.maxFileSize) {
                    msg += file.name + ": " + _resource.file.lenghtIsNotAllow + _resource.file.maxLength + egov.setting.maxFileSize / 1048576 + "Mb";
                } else if (!(egov.setting.acceptFileTypes.test(file.type) || egov.setting.acceptFileTypes.test('.' + file.name.split('.').pop()))) {
                    msg += file.name + ": " + _resource.file.typeIsNotAllow;
                }
                if (msg !== "") {
                    egov.pubsub.publish(egov.events.status.error, msg);
                } else {
                    var fReader = new FileReader();
                    fReader.onloadstart = function () {
                        egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                    }
                    fReader.onload = function (readerEvt) {
                        that.modifiedFiles[itemId] = btoa(readerEvt.target.result);
                    }
                    fReader.onloadend = function () {
                        egov.pubsub.publish(egov.events.status.destroy);
                    }
                    fReader.onerror = function (errmsg) {
                        console.log(errmsg);
                    }
                    fReader.readAsBinaryString(file);
                }

            } else {
                console.log('The File APIs are not fully supported in this browser.');
            }
        }
    });

    /// <summary>Đối tượng thể hiện 1 file đính kèm</summary>
    var AttachmentItem = Backbone.View.extend({
        tagName: 'li',
        className: 'mdl-list__item mdl-list__item--two-line',
        selectedClass: 'rowSelected',

        fileName: "",
        fileExtension: "",
        uploadingTemplate: '<div class="progress"><div class="progress-bar" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div></div>',

        events: {
            'click .attachment-remove': 'remove',
            'click .attachment-download': '_openDownload',
            'change .attachment-update .fileupload': 'update'
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (option) {
            this.parent = option.parent;
            this.fileExtension = this.model.get("Extension");
            this.fileName = this.model.get("Name");
            this.render();
        },

        /// <summary>Bind template</summary>
        render: function () {
            this.parent.$el.closest(".document-attachment-mobile").show();
            var that = this;

            require([egov.template.document.attachment], function (AttachmentTemplate) {
                that.$el.append($.tmpl(AttachmentTemplate, that.model.toJSON()));

                if (that.model.get("isRemoved")) {
                    that.$el.addClass("disabled");
                }

                if (that.model.get("isNew")) {
                    that.$(".attachment-view").css("visibility", "hidden");
                }

                if (!that.parent.hasPermission
                    && !that.model.get("isPhatHanhOrKetThuc")) {
                    that.$(".attachment-remove").hide();
                }

                if (that.model.get("isUploading")) {
                    that.$el.append(that.uploadingTemplate);
                }
            });

            this.model.on("change:Name", function (model, name) {
                that.$(".detail-attachment-name").text(name);
                that.$el.addClass("updatedfile");
            });
            this.model.on("change:icon", function (item, iconClass) {
                that.$(".attachment-name .icon").attr("class", "icon " + iconClass);
            });
            this.model.on("change:isUploading", function () {
                that.$(".progress").remove();
            });

            this.model.view = this;
            return this;
        },

        update: function (e) {
            if (!e) {
                return;
            }

            var file = e.currentTarget.files[0];
            var extention = file.name.substr(file.name.lastIndexOf(".") + 1).toLowerCase();
            if (extention != this.fileExtension) {
                var $dialog = $("<span>" + String.format(_resource.confirmToUploadWithOtherName, file.name, this.fileName) + "</span>");
                $dialog.dialog({
                    title: _resource.notEqualName,
                    width: '400px',
                    top: '100px',
                    buttons: [
                        {
                            text: egov.resources.common.closeButton,
                            className: 'btn-info',
                            disableProcess: true,
                            click: function () {
                                $dialog.dialog("hide");
                            }
                        }
                    ]
                });
            }
            else {

                this.parent.updateAttachment(this.model.get("Id"), file);
                this.$el.addClass("updatedfile");
            }

            e.preventDefault();
        },

        remove: function (e) {
            if (!this.parent.hasPermission && !this.model.get("isPhatHanhOrKetThuc")) {
                egov.pubsub.publish(egov.events.status.error, egov.resources.attachment.notPermision);
                return;
            }

            var that = this;
            if (this.model.get('isNew')) {
                var command = function () {
                    // Xóa file vừa tải lên  
                    that.parent.model.remove(that.model);
                    if (that.parent.model.length == 0) {
                        that.$el.parents(".document-attachment").hide();
                    }

                    that.$el.remove();
                };

                if (this.model.get("isPhatHanhOrKetThuc")) {
                    egov.request.removeAttachment({
                        data: { id: this.model.get('Id'), documentCopyId: this.parent.documentCopyId },
                        success: function (result) {
                            if (result) {
                                if (result.error) {
                                    egov.pubsub.publish(egov.events.status.error, result.error);
                                } else {
                                    egov.pubsub.publish(egov.events.status.success, result.success);
                                    command();
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
                } else {
                    command();
                }
            } else {
                if (!this.model.get('isRemoved')) {
                    this.$el.find('.attachment-name').text(this.model.get("Name") + _resource.removed);
                    this.model.set("hasRemoved", true)
                    this.model.set('isRemoved', true);
                    this.$el.addClass("disabled");
                }
            }

            e.preventDefault();
        },

        _openDownload: function (e) {
            var fileName = this.model.get("Name");
            var size = this.model.get("Size");
            var fileid = this.model.get("Id");

            if (fileid == 0 || !$.isNumeric(fileid)) {
                return;
            }

            egov.dialog.confirmActions({
                title: fileName,
                message: "Dung lượng file: " + size,
                buttons: [
                    {
                        text: 'Mở',
                        callback: function () {
                            downloadAttachment(fileid, fileName, true);

                        }
                    },
                    {
                        text: 'Tải về',
                        callback: function () {
                            downloadAttachment(fileid, fileName, false);
                        }
                    }
                ]
            });

            e.preventDefault();
        },

        download: function (e) {
            downloadAttachment(fileid, this.model.get("Name"), true);
        }
    });

    //#endregion

    //#region Private Methods

    function downloadAttachment(id, fileName, isOpen) {
        /// <summary>
        /// Tải về file đính kèm.
        /// </summary>
        /// <param name="id">Id của file cần tải</param>
        // egov.message.processing(egov.resources.common.processing, false);

        var link = '/Attachment/DownloadAttachment/' + id;
        return isOpen ? egov.mobile.download(link, fileName) : egov.mobile.onlyDownload(link);
    };

    //#endregion

    return AttachmentsView;
});