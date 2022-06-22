define([
    'jquery',
    egov.template.document.checkCitizenInfo
],
function ($, Template) {
    "use strict";
    var _resource = egov.resources.document.documentOnline;
    //#region Relation View

    var CheckCitizenInfo = Backbone.View.extend({

        id: "checkCitizenInfo",
        items: [],

        initialize: function (options) {
            this.template = Template;
            this.document = options.document;

            this.render();
        },

        events: {
            'click thead .col-extend-info': 'changeColInfo'
        },

        render: function () {
            var that = this;
            this.renderDialog();
            //if (!that.model.isCheckExistOnly) {
            //    this.$(".registerShowEle").show();
            //    this.renderRegisterDocument();
            //}

            this.renderExistDocument();
        },

        renderDialog: function () {
            var that = this;
            var dialogSetting = {
                title: egov.resources.searching.result,
                width: '1000px',
                draggable: true,
                keyboard: true,
                height: "auto",
                buttons: [
                    {
                        id: "closeCheckCitizenInfo",
                        className: 'btn-close',
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that.$el.dialog('destroy');
                        }
                    }
                ]
            };

            this.$el.html($.tmpl(this.template, this.model));
            if (this.model.isOnline) {
                this.$el.addClass("isOnlineDialog");
            }
            else {
                dialogSetting.buttons.unshift({
                    id: "insertRelationDocs",
                    className: 'btn-primary disabled',
                    text: _resource.insertRelations,
                    click: function () {
                        that.insertRelationDocs(function () {
                            that.$el.dialog('destroy');
                        })
                    }
                });
            }
            this.$el.dialog(dialogSetting);
        },

        renderRegisterDocument: function () {
            var that = this;
            var $registerZone = this.$(".registerDocumentZone");
            var samePeopleDocuments = egov.views.home.documents.model.models.filter(function (item) {
                return item.get("IdCard") == that.model.idCard;
            });
            if (samePeopleDocuments.length > 0) {
                _.each(samePeopleDocuments, function (item) {
                    var model = {
                        docCode: item.get("DocCode"),
                        compendium: item.get("Compendium"),
                        dateCreated: item.get("DateRecieved"),
                        docId: item.get("Id"),
                        isWaitting: true,
                        statusText: ""
                    }
                    var checkItem = new CheckCitizenInfoItem({
                        parent: that.document,
                        model: model,
                        dialog: that.$el,
                        isCurrent: item.get("id") == that.document.id
                    });
                    $registerZone.append(checkItem.$el);
                })
            }
            else {
                $registerZone.append("<span class='noData'>" + _resource.noData + "</span>");
            }
        },

        renderExistDocument: function () {
            var that = this;
            var $acceptDocumentZone = this.$(".acceptDocumentZone");
            that.items = [];
            egov.request.checkDocument({
                data: that.model,
                success: function (results) {
                    if (results.length > 0) {
                        that.$("#insertRelationDocs").removeClass("disabled");
                        var existRelations = [];
                        if (!that.model.isOnline && that.document.relations) {
                            existRelations = that.document.relations.model.filter(function (item) {
                                return item.get("IsRemoved") == false;
                            });
                        }

                        _.each(results, function (item, idx) {
                            var isExist = existRelations.find(function (d) {
                                return d.id == item.docCopyId;
                            });
                            var checkItem = new CheckCitizenInfoItem({
                                parent: that.document,
                                model: item,
                                dialog: that.$el,
                                curentCopyId: that.document.id,
                                isCheck: isExist != undefined
                            });
                            $acceptDocumentZone.append(checkItem.$el);
                            that.items.push(checkItem);
                        })
                    }
                    else {
                        $acceptDocumentZone.append("<span class='noData'>" + _resource.noData + "</span>");
                    }
                    egov.pubsub.publish(egov.events.status.destroy);
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, _resource.getExistDocumentError);
                }
            })
        },

        insertRelationDocs: function (callback) {
            if (this.model.isOnline) {
                return
            }
            var that = this;
            that.document.relations.model.reset();

            var relationItem = this.items.filter(function (item) {
                return item.model.isCheck == true;
            });

            _.each(relationItem, function (item) {
                that.document.relations.model.add(new egov.models.relation({
                    RelationCopyId: item.model.docCopyId,
                    RelationId: item.model.documentId,
                    RelationType: 1,
                    IsAddNext: false,
                    IsNew: true,
                    Compendium: item.model.compendium,
                    CitizenName: item.model.citizenName,
                    DocCode: item.model.docCode,
                    DateCreated: item.model.dateCreated,
                    CategoryName: item.model.categoryName
                }));
            });

            egov.callback(callback);
        },

        changeColInfo: function (e) {
            var $wrap = this.$("#checkCitizenInfoDialog");
            var $target = $(e.currentTarget);
            $wrap.find(".col-extend-info").hide();
            if ($target.hasClass("phone")) {
                $wrap.find(".col-extend-info.email").show();
            }
            else if ($target.hasClass("idcard")) {
                $wrap.find(".col-extend-info.phone").show();
            }
            else {
                $wrap.find(".col-extend-info.idcard").show();
            }
        }
    });

    var CheckCitizenInfoItem = Backbone.View.extend({
        curentDocCopyId: 0,
        isCurrent: false,
        isCheck: false,
        tagName: "tr",
        className: "checkItem",
        initialize: function (options) {
            var that = this;
            this.parent = options.parent;
            this.curentDocCopyId = options.curentCopyId;
            this.model.isCheck = options.isCheck;
            this.model.isCurrent = this.model.docCopyId == that.parent.id;
            if (this.model.isCurrent) {
                this.model.canView = false;
            }
            this.model.createdDate = new Date(this.model.dateCreated).format("dd/MM/yyyy");
            if (!this.model.statusText) {
                this.model.statusText = egov.resources.search["status" + this.model.status];
            }
            this.dialog = options.dialog;
            this.render();
        },

        events: {
            'click': 'select',
            'dblclick': 'openDocument',
            'click .chkrelationdocument': "addRelation"
        },

        render: function () {
            var that = this;

            require([egov.template.document.checkCitizenInfoItem], function (TemplateItem) {
                that.$el.html($.tmpl(TemplateItem, that.model));
            });
        },

        select: function (e) {
            if (this.$el.hasClass("selected")) {
                this.$el.removeClass("selected");
            }
            else {
                this.$el.siblings().removeClass("selected");
                this.$el.addClass("selected");
            }
        },

        openDocument: function (e) {
            this.select();
            if (this.model.canView) {
                egov.views.home.tab.openDocument(this.modeldocCopyId, this.modelcompendium, true, function () {
                    that.dialog.dialog("destroy");
                });
            }
        },

        addRelation: function (e) {
            if ($(e.currentTarget).closest(".checkbox").hasClass("checked")) {
                $(e.currentTarget).closest(".checkbox").removeClass("checked");
            }
            else {
                $(e.currentTarget).closest(".checkbox").addClass("checked");
            }

            this.model.isCheck = $(e.currentTarget).is(":checked");
        },

    });

    //#endregion

    return CheckCitizenInfo;
});