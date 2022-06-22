define(function () {
    "use strict";

    /// <summary>Đối tượng thể hiện một biểu mẫu html</summary>
    var KNTCForm = Backbone.View.extend({
        events: {
            "contextmenu": "contextmenu"
        },

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="option">Các tham số.</param>
            var that = this,
                isCreate;
            this.document = option.document;
            this.isView = false;
            this.scanFile = option.scanFile;

            if (option.contentHTML) {
                this.model.Content = option.contentHTML;
            }

            that.isCreate = that.document.isCreate == undefined ? false : that.document.isCreate;
            that.isCreate = that.document.isInsertImagePacket ? true : that.isCreate;//Nếu là chèn ảnh theo lô vào văn bản, coi là văn bản tạo mới để sửa được nội dung form

            if (this.model.Content || this.model.ContentUrl) {
                this.model.Content = unescape(this.model.Content);
                return that.render();
            }

            egov.request.getFormContent({
                data: {
                    contentId: that.document.isCreate && !that.document.copiedDocument ? that.model.FormId : that.model.DocumentContentId,
                    isCreate: that.document.isCreate && !that.document.copiedDocument || false,
                    doctypeId: that.document.model.get('DocTypeId')
                },
                success: function (result) {
                    if (!result) {
                        return;
                    }
                    that.model.Content = unescape(result);
                    that.render();
                },
                error: function (e) {
                    egov.log(e);
                }
            });
            return this;
        },

        render: function () {
            /// <summary>
            /// Hiển thị nội dung biểu mẫu trên form văn bản.
            /// </summary>
            /// <returns type=""></returns>
            if (this.model.ContentUrl) {
                var $content = $('<iframe class="content-iframe" style="border: 0px; width:100%; min-height: 350px;" src="' + this.model.ContentUrl + '"></iframe>');
                $content.load(function () {
                    setIframeHeight(this);
                }).appendTo(this.$el);
            } else {
                this.$el.append(unescape(this.model.Content));
            }
            if (egov.mobile) {
                egov.mobile.niceScrollIOS(this.document.$(".autoscroll"));
            }
            this.enableEditor();
            return this;
        },

        renderDialog: function () {
            /// <summary>
            /// Hiển thị nội dung biểu mẫu trên form sửa
            /// </summary>
            var that = this,
                $dialog,
                $editIframe,
                $content,
                contentHeight;

            $dialog = $('<div id="egov-dialog"></div>');
            if (!that.model.Url) {
                contentHeight = that.$el.height();
                $content = that.$el.clone();
                $dialog.html($content);
                that._openDialog($dialog,
                    function () {
                        that.model.Content = $content.bmailEditor.getContent();
                        that.model.IsEdited = true;
                        that.document._renderForm(true);
                        $dialog.dialog('destroy');
                    },
                    function () {
                        $dialog.dialog('destroy');
                        that.document._renderForm();
                    },
                    function () {
                        var $parent = $dialog.parent(".modal-body");
                        $parent.css({
                            'padding-top': 0,
                            'position': 'initial'
                        });
                        $content.editor(contentHeight, function (bmailEditor) {
                            $content.bmailEditor = bmailEditor;
                        });
                    })
            }
            else {
                $editIframe = $('<iframe id="bwss-iframe" style="border: 0px; min-height: 300px; width:100%" src="' + that.model.Url + '"></iframe>');
                $editIframe.load(function () {
                    setIframeHeight(this);
                }).appendTo($dialog);

                that._openDialog($dialog, undefined,
                    function () {
                        that._editKqklMrContent();
                        that._editDocContent(function () {
                            $dialog.dialog('destroy');
                            that.document._renderForm(true);
                        });
                    },
                    function () {
                        $editIframe.parents(".modal-dialog").css({
                            "position": "absolute",
                            "top": "-25px",
                            "left": "18%"
                        });
                        $editIframe.parents(".modal-body").css({
                            "padding": 0
                        });

                        $(".modal-header,.modal-footer").css({
                            "padding": "5px"
                        });

                        $('.modal-header .close').click(function () {
                            that._editKqklMrContent();
                            that._editDocContent(function () {
                                $dialog.dialog('destroy');
                                that.document._renderForm(true);
                            });
                        });
                    });
            }
        },

        contextmenu: function (e) {
            /// <summary>
            /// Hiển thị chuột phải trên form văn bản: hiển thị các phiên bản
            /// </summary>
            if (this.model.Version <= 1) {
                return;
            }

            var that = this;
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
            egov.request.getDocumentContentVersion({
                data: {
                    documentContentId: that.model.DocumentContentId,
                    version: that.model.Version
                },
                success: function (result) {
                    if (result) {
                        var contextItems = that._getContextItems(result);
                        contextModel.set('data', contextItems);
                        that.$el.contextmenu(contextModel, e);
                    }
                }
            });
        },

        openDetail: function (documentContentVersion) {
            var $dialog = $("<div>");
            var dialogSeting = {
                width: 900,
                height: 600,
                title: String.format(egov.resources.document.content.version, documentContentVersion.CreatedByUserName, documentContentVersion.CreatedOnDate),
                buttons: [
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            $dialog.dialog("close");
                            return;
                        }
                    }
                ]
            };
            $dialog.html(unescape(documentContentVersion.Content));
            $dialog.dialog(dialogSeting);
        },

        _editKqklMrContent: function () {
            var that = this,
                regex = /(\n|\r|\t)/gm,
                htmlReceived = document.getElementById("bwss-iframe").contentWindow.GetHtmlContent();
            htmlReceived = htmlReceived.replace(regex, "");
            var jsonObj = JSON.parse(htmlReceived);
            that.model.Content = _.unescape(jsonObj.content);
            that.model.IsEdited = true;
        },

        disableEditor: function () {
            /// <summary>
            /// Hủy bỏ editor
            /// </summary>
            this.$el.editor('destroy');
        },

        enableEditor: function () {
            /// <summary>
            /// Hiển thị editor
            /// </summary>
            if (!this.document.isCreate && (this.document.model.get("CategoryBusinessId") != 1 || this.document.model.get("WorkflowId") != 0)) {
                return;
            }

            var $el = this.$el,
                that = this,
                dynamicHeight;

            dynamicHeight = 350; // that.document.$el.height() - that.document.$(".staticPart").height();
            if (that.scanFile) {
                if (that.scanFile.get("Extension").toLowerCase() === '.pdf') {
                    egov.request.createImagesFromBeginAndLastPdfPages({
                        data: { id: that.scanFile.get("Id") },
                        beforeSend: function () {
                            egov.pubsub.publish(egov.events.status.processing, "Đang xử lý...");
                        },
                        success: function (data) {
                            if (data) {
                                if (data.error) {
                                    // egov.message.error(data.error);
                                    egov.pubsub.publish(egov.events.status.error, data.error);
                                } else {
                                    var images = data.images;
                                    
                                    if (data.documentInfo) {
                                        that.document.model.set("Compendium", data.documentInfo.Compendium);
                                        that.document.model.set("DocCode", data.documentInfo.DocCode);
                                        that.document.model.set("Organization", data.documentInfo.Organization);
                                        that.document.model.set("DatePublished", data.documentInfo.DatePublished);
                                        that.document.reRenderInfo();
                                    }

                                    var $imgTemp = "";
                                    for (var i = 0; i < images.length; i++) {
                                        var $img = '<div><p>' + '<img style="width:100%;" src="' + images[i] + '" />' + '</p></div>';
                                        $imgTemp = $imgTemp + $img;
                                    }

                                    that.model.Content = $imgTemp + that.model.Content;
                                    that.$el.prepend($imgTemp);
                                    that.$el.editor(dynamicHeight, function (bmailEditor) {
                                        that.$el.bmailEditor = bmailEditor;
                                        that.document._setFocus();
                                    });
                                    that.model.IsEdited = true;
                                }
                            }
                        },
                        error: function () {
                            // egov.message.error(_resource.errorDownload);
                            egov.pubsub.publish(egov.events.status.error, _resource.errorDownload);
                        },
                        complete: function () {
                            // egov.message.hide();
                            egov.pubsub.publish(egov.events.status.destroy);
                        }
                    });
                    return;
                }

                return;
            }
            if (!egov.isMobile) {
                this.$el.editor(dynamicHeight, function (bmailEditor) {
                    that.$el.bmailEditor = bmailEditor;
                    that.document._setFocus();
                });
            }
            else {
                this.$el.attr("contenteditable", true);
            }
            this.model.IsEdited = true;
        },

        getContent: function (e) {
            /// <summary>
            /// Lấy nội dung form khi chỉnh sửa
            /// </summary>
            /// <returns type=""></returns>
            egov.helper.destroyClickEvent(e);
            // this.disableEditor();

            this.model.Content = this.$el.bmailEditor ? this.$el.bmailEditor.getContent() : this.$el.html();
            return this.model.Content;
        },

        //<summery>Tạo context menu/summery>
        _getContextItems: function (result) {
            var contextItems = new egov.models.contextMenuList();
            var that = this;
            _.each(result, function (documentContentVersion) {
                documentContentVersion.Content = unescape(documentContentVersion.Content);
                contextItems.add({
                    text: String.format(egov.resources.document.content.version, documentContentVersion.CreatedByUserName, documentContentVersion.CreatedOnDate),
                    value: documentContentVersion.DocumentContentDetailId,
                    iconClass: 'icon-eye3',
                    callback: function () {
                        that.openDetail(documentContentVersion);
                    }
                });
            });
            return contextItems;
        },

        _editDocContent: function (callback) {
            egov.request.editDocumentContent({
                data: {
                    contentId: that.model.DocumentContentId,
                    content: escape(that.model.Content)
                },
                success: function (result) {
                    egov.callback(callback, result);
                }
            });
        },

        _openDialog: function ($dialog, submitEvent, closeEvent, callback) {
            var that,
                buttons = [],
                btn;

            that = this;

            if (submitEvent) {
                btn = {
                    text: window.getResource('egov.resources.main.submitBtn'),
                    className: 'btn-primary',
                    click: function () {
                        egov.callback(submitEvent);
                    }
                };
                buttons.push(btn);
            }

            if (closeEvent) {
                btn = {
                    text: window.getResource('egov.resources.main.closeBtn'),
                    className: 'btn-close',
                    click: function () {
                        egov.callback(closeEvent);
                    }
                };
                buttons.push(btn);
            }

            $dialog.dialog({
                overflow: 'visible',
                height: '520px',
                width: '900px',
                draggable: true,
                resize: true,
                title: that.model.ContentName,
                buttons: buttons
            });
            egov.callback(callback);
        }
    });

    function setIframeHeight(iframe) {
        if (iframe) {
            var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
            if (iframeWin.document.body) {
                iframe.height = iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;
            }
        }
    };

    return KNTCForm;
});