define([
    egov.template.document.relation
],
function (RelationsTemplate) {
    "use strict";

    //#region Relation View

    var RelationsView = Backbone.View.extend({

        className: ".document-relation",

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            this.$(".relation-list").empty();
            this.document = options.document;
            var that = this;

            this.hasPermission = options.hasPermission;
            this.model.on("add", function (newItm, model) {
                newItm.set('IsNew', true);
                var relationItm = new RelationItem({
                    model: newItm,
                    parent: that
                });

                if (that.$el.closest(that.className).is(":hidden")) {
                    that.$el.closest(that.className).show();
                }

                that.$el.find(".relation-list").append(relationItm.$el);
                that.hasChangeRelation = true;
            }, this);

            this.model.on("remove", function () {
                that.hasChangeRelation = true;
            }, this);

            this.model.on("reset", function () {
                if (that.hasPermission) {
                    that.$el.find("ul").html("");
                }
            }, this);

            this.render();
        },

        /// <summary>Hiển thị danh sách văn bản liên quan</summary>
        render: function () {
            var that = this,
                relationItm;

            if (this.model.length > 0) {
                this.$el.parents('.document-relation').show();
                this.model.each(function (relation) {
                    relationItm = new RelationItem({
                        model: relation,
                        parent: that
                    });
                    that.$el.find(".relation-list").append(relationItm.$el);
                });
            }
            return this;
        },

        //<summery>Xác nhận lại văn bản liên quan</summery>
        /// <param name="callback" type="function">Hàm thực thi sau khi confirm</param>
        confirmRelations: function (callback) {
            if (this.model.length > 0) {
                this.confirmView = new ConfirmRelation({
                    model: this.model,
                    callback: callback
                });
                return;
            }
            egov.callback(callback);
        },
    });

    /// <summary>Đối tượng thể hiện một văn bản liên quan</summary>
    var RelationItem = Backbone.View.extend({
        tagName: 'tr',
        template: RelationsTemplate,
        selectedClass: 'rowSelected',
        relationModels: [],

        // Các sự kiện
        events: {
            'click': 'selected',
            'click .relation-open': '_openDocument',
            'click .relation-info': '_infoDetail',
            'click .relation-remove': '_remove',

            'dblclick': '_openDocument',
            'contextmenu': 'contextmenu',
            'tap': '_openDocument',
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (option) {
            this.parent = option.parent;
            this.render();
        },

        /// <summary>Render ra giao diện</summary>
        render: function () {
            // this.$el.addClass("list-group-item");
            this.$el.append($.tmpl(this.template, this.model.toJSON()));

            if (!this.parent.hasPermission && !this.model.get("IsNew")) {
                this.$(".relation-remove").hide();
            }

            return this;
        },

        /// <summary>Chọn văn bản liên quan</summary>
        selected: function () {
            egov.helper.hideAllContext();
            if (this.$el.contextobj) {
                this.$el.contextobj.hide();
            }
            this.$el.addClass(this.selectedClass);
            this.$el.siblings('.' + this.selectedClass).removeClass(this.selectedClass);
        },

        /// <summary>Mở văn bản liên quan</summary>
        _openDocument: function () {
            var that = this,
                docId = this.model.get('RelationCopyId'),
                compendium = this.model.get('Compendium'),
                elementId = '#tabDocument_' + docId;

            egov.helper.hideAllContext();
            ///Giao diện bình thường
            if (!egov.isMobile) {
                egov.views.home.tab.openDocument(docId, compendium);
            }
            else { ///Giao diện mobile- tablet
                if ($(this.documentView).find(elementId).length > 0) {
                    $(that.documentView).children().removeClass('active');
                    $(elementId).addClass('active');

                } else {
                    require(['views/home/tablet-mobile/tab-tablet-mobile'], function (TabMobileAndTabLet) {
                        new TabMobileAndTabLet({
                            model: {
                                id: 'tabDocument_' + docId,
                                name: compendium,
                                title: compendium,
                                href: href
                            },
                            relationTab: true
                        });
                    });
                }
            }
        },

        /// <summary>Xóa văn bản liên quan hiện tại</summary>
        _remove: function () {
            egov.helper.hideAllContext();
            this.model.set("IsRemoved", true);
            //this.parent.model.remove(this.model);
            var remainRels = this.parent.model.filter(function (rel) {
                return !rel.get("IsRemoved");
            });
            if (remainRels.length == 0) {
                this.$el.parents(".document-relation").hide();
            }
            this.$el.remove();
        },

        /// <summary>Menu chuột phải</summary>
        contextmenu: function (e) {
            if (!e) {
                return;
            }

            var that = this,
                contextItems,
                contextModel;

            egov.helper.destroyClickEvent(e);

            if (!this.$el.hasClass(this.selectedClass)) {
                this.selected();
            }

            contextItems = new egov.models.contextMenuList();
            contextItems.add({
                text: egov.resources.document.relation.contextmenu.open,
                value: 'openRelation',
                iconClass: 'icon-enter',
                callback: function () {
                    that._openDocument();
                }
            });

            if (this.parent.hasPermission) {
                contextItems.add({
                    text: egov.resources.document.relation.contextmenu.delete,
                    value: 'deleteRelation',
                    iconClass: 'icon-close',
                    callback: function () {
                        that._remove();
                    }
                });
            }

            contextModel = new egov.models.contextMenu({
                trigger: 'right',
                data: contextItems,
                style: {},
                position: {
                    my: 'left top',
                    at: 'left bottom',
                    of: 'event'
                },
                isShowLoading: false
            });

            //this.$el.ad
            this.$el.contextmenu(contextModel, e);
        },

        _infoDetail: function () {
            egov.helper.hideAllContext();
            var that = this,
                objModel,
                relationCopyId = this.model.get("RelationCopyId"),
                exist;

            if (this.relationModels.length > 0) {
                objModel = _.find(this.relationModels, function (item) {
                    return item.DocumentCopyId === relationCopyId;
                });

                if (objModel) {
                    getDocumentDetail(objModel);
                    return;
                }
            }

            egov.request.getDocumentDetail({
                data: { id: relationCopyId },
                success: function (result) {
                    if (result.error) {
                        // egov.message.error(egov.resources.document.relation.documentNotExist);
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.relation.documentNotExist);
                        return;
                    }

                    getDocumentDetail(result);
                    exist = _.contains(that.relationModels, function (item) {
                        return item.DocumentCopyId === relationCopyId;
                    });
                    if (!exist) {
                        result.DocumentCopyId = relationCopyId;
                        that.relationModels.push(result);
                    }
                }
            });
        }
    });

    //#endregion

    //#region Confirm Relation

    /// <summary>Đối tượng view xác nhận gửi kèm văn bản liên quan cho người tiếp theo</summary>
    var ConfirmRelation = Backbone.View.extend({

        events: {
            "change .checkAll>input[type=checkbox]": "checkAll",
            "click": "select",
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (option) {
            this.callback = option.callback;
            var flag = false;
            this.model.each(function (item) {
                if (!item.get("IsRemoved")) {
                    flag = true;
                }
            });
            if (flag) {
                this.render();
            } else {
                egov.callback(this.callback);
            }
        },

        select: function (e) {
            if (!this.$(e.target).is("input[type=checkbox]")) {
                var $relation = this.$(e.target).closest("tr").find("input[type=checkbox]");
                if ($relation.closest("tr").find(".checkAll").length > 0) {
                    return;
                }
                $relation.attr('checked', !$relation.is(":checked"));
            }
        },

        /// <summary>Hiển thị dialog</summary>
        render: function () {
            var that = this,
                ignoreConfirmRelation = egov.setting.userSetting.IgnoreConfirmRelation;
            // Nếu thiết lập không hiển thị dialog confirm thì mặc định thêm tất cả và bỏ qua form xác nhận.
            //var ignoreConfirmRelation = egov.locache.hasSupportLocalStorage
            //    ? egov.localStorage.getIgnoreConfirmRelation()
            //    : egov.cookie.getIgnoreConfirmRelation();

            //Do đưa "Không hỏi lại" vào userconfig, mỗi lần f5 đều đã lấy lại rồi nên không đưa vào cache nữa
            if (ignoreConfirmRelation) {
                this.model.each(function (relation) {
                    relation.set("Compendium", relation.get("Compendium"));
                    relation.set('IsAddNext', true);
                });
                egov.callback(that.callback);
                return;
            }

            // Nếu đã từng mở rồi thì hiển thị lại
            if (this.$el.modalView) {
                this.$el.dialog('show');
                return;
            }
            require([egov.template.document.confirmRelation], function (ConfirmRelation) {
                that.model.each(function (item) {
                    item.set("Compendium", unescape(item.get("Compendium")));;
                });

                that.$el.append($.tmpl(ConfirmRelation, { relations: that.model.toJSON() }));
                //  this.$('table').table({ resizable: true });
                that.$el.dialog({
                    draggable: true,
                    width: '600px',
                    //height: '300px',
                    ignoreText: egov.resources.document.relation.ignoreConfirm,
                    title: egov.resources.document.relation.confirmRelationTitle,
                    confirm: {
                        text: egov.resources.document.ignoreConfirmRelation,
                        className: "ignoreConfirmRelation",
                        style: { float: "left" },
                        click: function (result) {
                            result ? that.$(".ignoreConfirmRelationWarning").show() : that.$(".ignoreConfirmRelationWarning").hide();
                            ignoreConfirmRelation = result;
                        }
                    },
                    buttons: [
                        {
                            text: egov.resources.common.confirmButton,
                            className: 'btn-primary',
                            click: function () {
                                that.$("tr .checkbox").each(function () {
                                    var relationId = parseInt($(this).find('input[type="checkbox"]').val());
                                    var relation = that.model.detect(function (rel) {
                                        return rel.get('RelationCopyId') === relationId;
                                    });
                                    if (relation) {
                                        relation.set('IsAddNext', $(this).children("input[type=checkbox]").is(":checked"));
                                        relation.set("Compendium", escape(relation.get("Compendium")));
                                    }
                                });

                                if (that.$el.modalView.isIgnore()) {
                                    if (egov.locache.hasSupportLocalStorage) {
                                        egov.localStorage.setIgnoreConfirmRelation(true);
                                    }
                                    else {
                                        egov.cookie.setIgnoreConfirmRelation(true);
                                    }
                                }

                                //Set lại có hỏi lại hay không vào settings và send lên server
                                if (ignoreConfirmRelation) {
                                    egov.setting.userSetting.IgnoreConfirmRelation = ignoreConfirmRelation;
                                    egov.request.setUserConfig({
                                        dat: {
                                            IgnoreConfirmRelation: ignoreConfirmRelation
                                        }
                                    });
                                }
                                that.$el.dialog('hide');
                                egov.callback(that.callback);
                            }
                        }
                //,
                //{
                //    text: egov.resources.common.cancelButton,
                //    click: function () {
                //        that.$el.dialog('hide');
                //        egov.callback(that.callback);
                //    }
                //}
                    ]
                });

                return this;
            });
        },

        checkAll: function (e) {
            var $relations = this.$("tbody").find("input[type=checkbox]");
            $relations.attr('checked', this.$(e.target).is(":checked"));
        }
    });

    //#endregion

    ///Parse thông tin văn bản và show lên dialog
    var getDocumentDetail = function (document) {
        require(['documentDetail'], function (DocumentDetail) {
            //render thông tin chi tiết văn bản
            var documentDetail = new DocumentDetail({ model: document });
            //Thiết lập dialog
            var settings = {
                width: 800,
                height: 310,
                title: egov.resources.documents.title.documentDetail,
                buttons: [{
                    text: egov.resources.common.cancelButton,
                    className: 'btn-primary',
                    click: function () {
                        documentDetail.$el.dialog('destroy');
                    }
                }],
                draggable: true,
                keyboard: true,
            };

            ///show dialog
            documentDetail.$el.dialog(settings);
        });
    }

    return RelationsView;

});