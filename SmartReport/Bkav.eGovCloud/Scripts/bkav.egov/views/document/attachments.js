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
            this.isTheoLo = options.isTheoLo || false;

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

                that.isTheoLo ? that.$el.find("ul .btnInsertAttachment").before(attachment.$el) : that.$el.find(".attachment-list").append(attachment.$el);
            }, this);

            this.model.on('remove', function (removed) {
                removed.view.remove();
            }, this);

            this.render();

            this.hasSupportedFileReader = window.File && window.FileReader && window.FileList && window.Blob;
            this.registerToGlobal();
            return this;
        },

        render: function () {
            /// <summary>
            /// Hiển thị danh sách file đính kèm.
            /// </summary>
            var that = this,
                attachment;

            if (this.model.length > 0) {
                this.$el.parent().show();
            }

            var mWidth = this.model.length > 2 ? "32%" : "48%";

            this.model.each(function (att) {
                attachment = new AttachmentItem({
                    model: att,
                    parent: that
                });

                attachment.$el.css("maxWidth", mWidth);

                that.$el.find(".attachment-list").append(attachment.$el);
            });

            that.$(".attachment-item").css("maxWidth", mWidth);
        },

        registerToGlobal: function () {
            var that = this, att;
            if (window.top != undefined) {
                window.top.openFileInDesktop = function (attachmentId) {
                    /*
                     * egov.view.document.attachments
                     * Mở file đính kèm từ giao diện preview
                     */

                    att = that.model.detect(function (a) {
                        return a.get("Id") === attachmentId;
                    });

                    if (att != undefined && att.view != undefined && window.Plugin) {
                        window.Plugin.appendPlugin(function () {
                            window.Plugin.openAttachment(att.view);
                        });
                    }
                }
            }
        },

        confirmAttachments: function (callback, isPublishing) {
            /// <summary>
            /// Lưu lại các file đang được sửa.
            /// </summary>
            /// <param name="callback">Hàm thực thi sau khi hoàn thành.</param>
            var that = this;
            if (that.model.length == 0 || egov.isMobile) {
                egov.callback(callback);
                return;
            }
            that.isPublishing = isPublishing;

            if (Plugin) {
                window.Plugin.appendPlugin(function () {
                    Plugin.confirmAttachments(that, function () {
                        egov.callback(callback);
                    })
                });
            }
        },

        downloadAllAttachment: function () {
            /// <summary>
            /// Tải về file đính kèm.
            /// </summary>
            var id = this.documentId;
            var that = this;

            if (id === undefined || id == "00000000-0000-0000-0000-000000000000") {
                egov.pubsub.publish(egov.events.status.error, egov.resources.file.errorDownload);
                return;
            }

            if (!egov.isMobile) {
                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                require(['filedownload'], function () {
                    var link = '/Attachment/DownloadAttachments?id=' + id + "&storePrivateId=" + that.storePrivateId,
                        html,
                        json;
                    $.fileDownload(link, {
                        successCallback: function () {
                            egov.pubsub.publish(egov.events.status.destroy);
                        },
                        failCallback: function (response) {
                            html = $(response);
                            try {
                                json = JSON.parse(html.text());
                                egov.pubsub.publish(egov.events.status.error, json.error);
                            } catch (e) {
                                egov.pubsub.publish(egov.events.status.error, egov.resources.file.errorDownload);
                            }
                        }
                    });
                });
            }
            else {
                var link = '/Attachment/DownloadAttachments/' + id;
                egov.mobile.downloads(link);
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
        tagName: 'tr',
        selectedClass: 'rowSelected',

        fileName: "",
        fileExtension: "",
        uploadingTemplate: '<div class="progress"><div class="progress-bar" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div></div>',

        events: {
            'click': 'selected',
            'contextmenu': 'contextmenu',
            'click .viewAttachment': 'open',
            'click .attachment-open': 'open',
            'dblclick': 'open',

            'click .attachment-view': 'view',
            'touchend .attachment-name': 'view',

            'click .attachment-remove': 'remove',
            'click .attachment-download': 'download',
            'tap .attachment-download': 'download',
            'tap .attachment-download-all': 'downloadAll',
            'click .attachment-download-all': 'downloadAll',
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
            var that = this;

            require([that.parent.isTheoLo ? egov.template.document.attachmentTheoLo : egov.template.document.attachment], function (AttachmentTemplate) {
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
                    that.$el.find('td:first-child').append(that.uploadingTemplate);
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

            this.$(".attachment-function a").etip();

            this.model.view = this;
            return this;
        },

        /// <summary>Chọn văn bản liên quan</summary>
        selected: function (e) {
            egov.helper.hideAllContext();
            if (this.$el.contextobj) {
                this.$el.contextobj.hide();
            }
            if (this.$el.hasClass(this.selectedClass)) {
                this.$el.removeClass(this.selectedClass);
                return;
            }
            this.$el.addClass(this.selectedClass);
            this.$el.siblings('.' + this.selectedClass).removeClass(this.selectedClass);
        },

        /// <summary>Mở file</summary>
        open: function (e, version) {
            if (egov.isMobile) {
                return;
            }

            var that, id;
            that = this;

            if (that.model.get('isRemoved')) {
                egov.pubsub.publish(egov.events.status.error, _resource.fileIsRemoved);
                return;
            }

            if (Plugin) {
                window.Plugin.appendPlugin(function () {
                    Plugin.openAttachment(that, version);
                });
            }
        },

        view: function (e) {
            var that, id;
            that = this;

            if (that.model.get('isNew')) {
                return;
            }

            if (that.model.get('isRemoved')) {
                egov.pubsub.publish(egov.events.status.error, _resource.fileIsRemoved);
                return;
            }

            if (window.top != undefined && _.isFunction(window.top.openFilePreview)) {
                window.top.openFilePreview(that.parent.documentId, that.model.get("Id"));
                return;
            }
        },

        update: function (e) {
            if (!e) {
                return;
            }
            egov.helper.destroyClickEvent(e);
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
        },

        remove: function (e) {
            //if (!e) {
            //    return;
            //}

            if (!this.parent.hasPermission && !this.model.get("isPhatHanhOrKetThuc")) {
                egov.pubsub.publish(egov.events.status.error, egov.resources.attachment.notPermision);
                return;
            }

            egov.helper.destroyClickEvent(e);

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

                ///HopCV: loại bỏ văn bản được phát hành hoặc kết thúc
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
        },

        download: function (e) {
            if (!e) {
                return;
            }
            egov.helper.destroyClickEvent(e);
            if (this.model.get("Id") == 0 || !$.isNumeric(this.model.get("Id"))) {
                return;
            }
            downloadAttachment(this.model.get("Id"), this.parent.storePrivateId, this.model.get("Name"));
        },

        /// <summary>Chuột phải</summary>
        contextmenu: function (e) {
            if (!e) {
                return;
            }

            egov.helper.destroyClickEvent(e);
            if (!this.$el.hasClass(this.selectedClass)) {
                this.selected();
            }

            var contextModel = new egov.models.contextMenu({
                trigger: 'right',
                data: null,
                style: {},
                position: {
                    my: 'left top',
                    at: 'left bottom',
                    of: 'event'
                },
                hasCache: false,
                isShowLoading: false
            });

            var contextItems = this._getContextItems();
            contextModel.set('data', contextItems);
            this.$el.contextmenu(contextModel, e);
        },

        //<summery>Tạo context menu/summery>
        _getContextItems: function () {
            var that = this,
                contextItems = new egov.models.contextMenuList();

            // Cho phép mở file để sửa
            contextItems.add({
                text: _resource.openFile,
                value: 'openattach',
                iconClass: 'icon-enter',
                callback: function () {
                    that.open();
                }
            });

            if (that.model.get('isNew')) {
                // Xóa file vừa tải lên
                contextItems.add({
                    text: _resource.deleteFile,
                    value: 'remove',
                    iconClass: that.model.get('isPhatHanhOrKetThuc') ? 'icon-trash' : 'icon-close',
                    callback: function () {
                        if (that.model.get('isPhatHanhOrKetThuc')) {
                            that.remove();
                        } else {
                            that.parent.model.remove(that.model);
                            if (that.parent.model.length == 0) {
                                that.$el.parents(".document-attachment").hide();
                            }

                            that.$el.remove();
                        }
                    }
                });
            } else {
                contextItems.add({
                    text: _resource.download,
                    value: 'download',
                    iconClass: 'icon-download3',
                    callback: function () {
                        downloadAttachment(that.model.get('Id'));
                    }
                });

                if (!that.model.get('isRemoved')) {
                    if (that.parent.hasPermission) {
                        contextItems.add({
                            text: _resource.deleteFile,
                            value: 'remove',
                            iconClass: 'icon-trash',
                            callback: function () {
                                that.$el.find('.attachment-name').text(that.model.get("Name") + _resource.removed);
                                //that.$el.find('td').last().text(_resource.removed);
                                that.model.set("hasRemoved", true);
                                that.model.set('isRemoved', true);
                                that.$el.addClass("disabled");

                            }
                        });
                    }
                }
                else {
                    if (that.parent.hasPermission) {
                        contextItems.add({
                            text: _resource.restoreFile,
                            value: 'undo',
                            iconClass: 'icon-arrow',
                            callback: function () {
                                that.$el.find('.attachment-name').text(that.model.get("Name") + "(" + _resource.using + ")");
                                //that.$el.find("td").last().text(_resource.using);
                                that.model.set("hasRemoved", false);
                                that.model.set('isRemoved', false);
                                that.$el.removeClass("disabled");

                            }
                        });
                    }
                }

                // Hiển thị các phiên bản
                $.each(that.model.get('Versions'), function (j, version) {
                    contextItems.add({ name: '---' });
                    contextItems.add({
                        text: String.format(_resource.version, version.User, version.CreateDate),
                        value: 'openattach',
                        iconClass: 'icon-stack',
                        callback: function () {
                            that.open(null, version.Version);
                        }
                    });
                });
            }

            return contextItems;
        },

        _isReadableFile: function () {
            var extension = this.fileExtension.toLowerCase();
            var readableExtension = ["pdf", "png", "jpg", "jpeg", "bmp", "gif", "doc", "docx", "xls", "xlsx"];

            return readableExtension.indexOf(extension) >= 0;
        }
    });

    //#endregion

    //#region Private Methods

    function downloadAttachment(id, storePrivateId, fileName) {
        /// <summary>
        /// Tải về file đính kèm.
        /// </summary>
        /// <param name="id">Id của file cần tải</param>
        // egov.message.processing(egov.resources.common.processing, false);
        if (!egov.isMobile) {
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
            var downloadLink = '/Attachment/DownloadAttachment?id=' + id + "&storePrivateId=" + storePrivateId;
            require(['filedownload'], function () {
                var link = downloadLink,
                    html,
                    json;

                $.fileDownload(link, {
                    successCallback: function () {
                        egov.pubsub.publish(egov.events.status.destroy);
                    },
                    failCallback: function (response) {
                        html = $(response);
                        try {
                            json = JSON.parse(html.text());
                            egov.pubsub.publish(egov.events.status.error, json.error);
                        } catch (e) {
                            egov.pubsub.publish(egov.events.status.error, egov.resources.file.errorDownload);
                        }
                    }
                });
            });
        }
        else {
            var link = '/Attachment/DownloadAttachment/' + id;
            egov.mobile.download(link, fileName);
        }
    };

    function checkBrowserIsChrome() {
        if (typeof window.external.createNotification === 'function')
            return false;

        return (navigator.userAgent.indexOf('Chrome') > -1 && navigator.userAgent.indexOf('Bchrome') == -1);
    }

    //#endregion

    return AttachmentsView;
});