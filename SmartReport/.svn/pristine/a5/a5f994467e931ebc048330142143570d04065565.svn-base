define([], function () {

    "use strict";

    var _resource,
    DocumentModel,
    _template;

    _resource = egov.resources.document;
    DocumentModel = egov.models.document;
    _template = egov.template.document;

    var InsertImagePacket = Backbone.View.extend({

        events: {
            "input #InOutCode": "_disableToolbar"
        },

        initialize: function (option) {
            var currentImage,
                newAttachment;

            if (egov.waitingImagePacket) {
                currentImage = _.find(egov.waitingImagePacket, function (item) {
                    return !item.opened;
                });
            }
            if (currentImage) {
                currentImage.opened = true;
            }

            newAttachment = new egov.models.attachment({
                Id: currentImage.file.key,
                Name: currentImage.file.name,
                Size: currentImage.file.size,
                Extension: '.' + currentImage.file.name.split('.').pop(),
                Versions: [{
                    Version: 1,
                    User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                }],
                isNew: true
            });
            this.tab = option.tab;
            this.model = new DocumentModel();
            this.scanFile = newAttachment;
            this.documentViewType = egov.enum.documentViewType.default;
            this.isInsertImagePacket = true;
            this.render();
        },

        render: function () {
            /// <summary>
            /// Render tab
            /// </summary>
            var that = this;
            require([egov.template.document.insertImagePacket], function (insertImagePacket) {
                that.$el.html($.tmpl(insertImagePacket));
                that._renderToolbar(true);
                that._renderAttachment();
                that._renderForm();
                that._autocomplete();
                that.$("#InOutCode").focus();
            });
        },

        _renderForm: function () {
            /// <summary>
            /// Render form
            /// </summary>
            var that = this;

            require(['htmlForm'], function (HtmlForm) {
                var content = {
                    Content: "",
                    ContentName: "",
                    DocumentContentDetails: Array[0],
                    DocumentContentId: 0,
                    DocumentId: "00000000-0000-0000-0000-000000000000",
                    FormId: 0,
                    FormTypeId: 1,
                    IsMain: true,
                    Version: 0,
                };

                var htmlForm = new HtmlForm({
                    model: content,
                    document: that,
                    scanFile: that.scanFile,
                    contentHTML: "<div></div>"
                });
                that.$("#divContent").append(htmlForm.$el);
                that.form = htmlForm;
            })
        },

        _renderToolbar: function (isQuickToolbar) {
            /// <summary>
            /// render toolbar 
            /// </summary>
            /// <param name="isQuickToolbar">toolbar bind lúc đầu hay toolbar đầy đủ của 1 công văn</param>
            var that = this;
            this.isQuickToolbar = isQuickToolbar; //toolbar hiện tại đang là toolbar bind lúc đầu hay toolbar đầy đủ của 1 công văn
            if (that.toolbar) {
                that.toolbar.reRender(isQuickToolbar ? egov.enum.defaultToolbar.InsertImagePacket : false, that.model.get('DocumentPermissions'));
                return;
            }

            require(['documentToolbar'], function (Toolbar) {
                that.toolbar = new Toolbar({
                    el: that.$('.toolbar'),
                    model: that.model.get('DocumentPermissions'),
                    document: that,
                    defaultToolbarType: isQuickToolbar ? egov.enum.defaultToolbar.InsertImagePacket : undefined
                });
            })
        },

        _renderAttachment: function () {
            /// <summary>
            /// Hiển thị các file đính kèm của document
            /// </summary>
            var that = this;
            require(['docAttachment'], function (Attachment) {
                var attacts = new egov.models.attachmentCollection(that.scanFile);
                that.model.set('Attachments', attacts);
                that.attachments = new Attachment({
                    hasPermission: true,
                    model: attacts,
                    el: that.$('#wrapAttachment'),
                    documentId: that.model.get("DocumentId")
                });
            });
        },

        _renderRelation: function () {
            /// <summary>
            /// Hiển thị các văn bản liên quan của document
            /// </summary>
            var that = this;
            require(['docRelation'], function (Relation) {
                var relations = new egov.models.relationList(that.model.get('RelationModels'));
                that.model.set('Relations', relations);
                that.relations = new Relation({
                    document: that.model,
                    hasPermission: true,// Chỉ có quyền remove, phục hồi khi là người đang giữ công văn
                    model: relations,
                    el: egov.isMobile ? that.$('#tblDocRelations') : that.$('#wrapDocumentRelation')
                });
            });
        },

        isValid: function () {
            if (this.$("#InOutCode").val() && this.$("#Compendium").val()) {
                return true;
            }
            return false;
        },

        _autocomplete: function () {
            /// <summary>
            /// autocomplet inoutcode
            /// </summary>
            var that = this,
                $code = that.$("#InOutCode"),
                $compendium = that.$("#Compendium");

            var settings = {
                minLength: 2,
                source: function (request, response) {
                    $.ajax({
                        url: "/document/FilterInOutCode/",
                        data: {
                            code: $code.val(),
                            compendium: $compendium.val()
                        },
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            response($.map(data, function (obj) {
                                return {
                                    label: obj["InOutCode"],
                                    value: obj["DocCode"] + " - " + obj["InOutCode"],
                                    compendium: obj["Compendium"],
                                    docCode: obj["DocCode"],
                                    inOutCode: obj["InOutCode"],
                                    documentCopyId: obj["DocumentCopyId"],

                                };
                            }));
                        }
                    });
                },
                autoFocus: true,
                autoSelectFirst: true,
                select: function (event, ui) {
                    $code.val(ui.item.value);
                    $compendium.val(ui.item.compendium);
                    var id = ui.item.documentCopyId;
                    that.model.set("Compendium", ui.item.compendium);
                    that.model.set("DocumentCopyId", id);
                    egov.dataManager.getDocumentEditInfo(id, {
                        success: function (data) {
                            if (data) {
                                if (data.error) {
                                    egov.pubsub.publish(egov.events.status.error, data.error);
                                    return;
                                }
                                that.model.set(data);
                                that._renderToolbar();
                                that._renderRelation();
                            }
                        }, error: function (xhr) {
                            egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                        }, complete: function () {
                            egov.pubsub.publish(egov.events.status.destroy);
                        }
                    });
                }
            };

            if ($code.length === 0) {
                return;
            }

            $code.autocomplete(settings).data("autocomplete")._renderItem = function (ul, item) {
                ul.addClass('dropdown-menu');
                return $("<li>")
                    .data("item.autocomplete", item)
                    .append("<a href='#'>" + item.label + "</a>")
                    .appendTo(ul);
            };

            $compendium.autocomplete(settings).data("autocomplete")._renderItem = function (ul, item) {
                ul.addClass('dropdown-menu');
                return $("<li>")
                    .data("item.autocomplete", item)
                    .append("<a href='#'>" + item.compendium + "</a>")
                    .appendTo(ul);
            };
        },

        _autoCompleteComment: function () {
            egov.dataManager.getCommonComment({
                success: function (comments) {
                    egov.dataManager.getTemplateComments({
                        success: function (templateComments) {
                            templateComments = _.pluck(templateComments, "Content");
                            templateComments = _.filter(templateComments, function (item) {
                                return comments.indexOf(item) == -1
                            });

                            if (templateComments.length > 0) {
                                _.each(templateComments, function (item) {
                                    comments.push(item)
                                });
                                egov.dataManager.addCommonComment(comments, true)
                            }

                            this.$Comment = this.$("[name='Comment']");
                            var that = this;
                            if (this.$Comment.length > 0) {
                                that.$Comment.autocomplete({
                                    source: function (request, response) {
                                        var value = egov.utilities.string.stripVietnameseChars(request.term).toLowerCase();
                                        var data = [];
                                        for (var i = 0; i < comments.length; i++) {
                                            var item = egov.utilities.string.stripVietnameseChars(comments[i]).toLowerCase();
                                            if (item.indexOf(value) != -1) {
                                                data.push(comments[i]);
                                            }
                                        }
                                        response(data);
                                    },
                                    autoFocus: true,
                                    autoSelectFirst: true
                                }).data("autocomplete")._renderItem = function (ul, item) {
                                    ul.addClass('dropdown-menu');
                                    return $("<li>")
                                        .data("item.autocomplete", item)
                                        .append("<a href='#'>" + item.value + "</a>")
                                        .appendTo(ul);
                                }
                            };
                        }
                    });
                }
            });
        },

        transfer: function (action) {
            /// <summary>
            /// Bàn giao văn bản theo hướng chuyển.
            /// </summary>
            /// <param name="action" type="object">Hướng chuyển.</param>
            var that = this,
                comment,
                transferAction;

            //this.$Comment = this.$Comment || $();
            comment = this.$("#Comment").val();
            transferAction = action;

            require(['transfer'], function (Transfer) {
                if (egov.transferForm === undefined) {
                    egov.transferForm = new Transfer;
                }
                egov.transferForm.render({
                    action: transferAction,
                    document: that,
                    isQuickTransfer: true,
                    callback: function () {
                        //add comment vao cache
                        //that._addCommentCommon(comment);
                    }
                });

            });
        },

        _disableToolbar: function () {
            if (!this.isQuickToolbar) {
                this._renderToolbar(true);
            }
        },

        RemoveDocument: function () {
            /// <summary>
            /// Hủy văn bản
            /// </summary>
            var that,
                documentCopyIds;

            that = this;
            documentCopyIds = parseInt(that.model.get("DocumentCopyId"));
            egov.request.removeDocument({
                data: {
                    documentCopyIds: [documentCopyIds]
                },
                success: function (result) {
                    that.tab.close();
                }
            });
        },

    });

    return InsertImagePacket;
})