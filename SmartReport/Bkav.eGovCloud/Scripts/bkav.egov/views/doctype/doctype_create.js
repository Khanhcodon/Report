
define([egov.template.doctypeCreate, egov.template.invoice.importinvoice], function (doctypeCreate, template) {
    var DoctypeModel = Backbone.Model.extend({
        defaults: {
            DocTypeId: "",
            DocTypeName: "",
            ActionLevel: 1,
            DocFieldId: 0,
            FormCategoryId: "",
            DocFieldName: "",
            isShow: true,
            Pinned: false
        },
    });

    var DocfieldModel = Backbone.Model.extend({
        defaults: {
            ID: "",
            isActive: false,
            DocFieldName: "",
            Count: 0,
            doctypes: []
        },
    });

    var DocTypeCollection = Backbone.Collection.extend({
        model: DoctypeModel,
        keyBusiness: "CategoryBusinessId",
        sortKey: "ActionLevel",
        FormCategoryIdSort: "FormCategoryId",
        reverseSortDirection: true,
        comparator: function (item) {
            return [item.get(this.keyBusiness), 1 / item.get(this.sortKey), 1 / item.get(this.FormCategoryIdSort)]
        },
        showDoctype: function (docfieldId, key) {
            var that = this;
            this.forEach(function (model) {
                model.set({ isActive: false })
            });

            var doctypeall = this.filter(function (model) {
                var docTypeName = model.get('DocTypeName').toLowerCase();
                return docTypeName.indexOf(key.toLowerCase()) > -1;
            });

            var doctypes = doctypeall.filter(function (model) {
                return (model.get('DocFieldId') == docfieldId || docfieldId == 0);
            });
            if (docfieldId == -1) {
                doctypes = doctypeall.filter(function (model) {
                    return (model.get('Pinned') == true);
                });
            }
            doctypes.sort();

            if (doctypes.length) {
                doctypes.forEach(function (model) {
                    model.set({ isActive: true })
                })
            }
            return {
                Count: doctypes.length,
                Docfield: _.groupBy(doctypeall, function (doctype) {
                    return doctype.get("DocFieldId");
                })
            };
        }
    });

    var DocfieldsCollection = Backbone.Collection.extend({
        model: DocfieldModel,
        removeActive: function () {
            var modelActives = this.where({ isActive: true });
            if (modelActives.length) {
                modelActives.forEach(function (model) {
                    model.set({ isActive: false })
                })
            }
        },
    });

    var app = app || {};

    var DoctypeCreate = Backbone.View.extend({
        className: '.createDoctypeDialog',

        events: {
        },

        initialize: function (options) {
            this.docfields = new DocfieldsCollection(),
            this.listenTo(this.docfields, 'add', this.addDocfield);
            this.listenTo(this.docfields, 'reset', this.resetDocfield);
            this.model = options.model;
        },

        templateDocfield: '<li class=""><a href="#" name="${DocFieldName}"><i class="fa fa-info"></i> ${DocFieldName} <span class="label label-danger pull-right">${Count}</span></a></li>',
        template: doctypeCreate,

        render: function () {
            var that = this;
            this.$el.html($.tmpl(this.template, { Avatar: egov.setting.user.avatar, FullName: egov.setting.user.fullName }));
            var that = this;
            var dialogSetting = {
                width: "90%",
                height: 500,
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Tạo mới báo cáo",
                buttons: [
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that.$el.dialog("hide");
                        }
                    },
                ]
            };
            that.$el.dialog(dialogSetting);
            app.$dialog = that.$el;
            $.ajax({
                url: '/homesmreport/GetDoctypePinned',
                type: "GET",
                data: {},
                success: function (res) {
                    var userSetting = JSON.parse(res);
                    if (userSetting) {
                        var pinnedDocTypes = userSetting.PinnedDocTypes;
                        var doctypes = that.model;
                        that.model = _.map(doctypes, function (doctype) {
                            if (_.contains(pinnedDocTypes, doctype.DocTypeId)) {
                                doctype.Pinned = true;
                            } else {
                                doctype.Pinned = false;
                            }
                            return doctype;
                        });
                        app.viewdoctypes = new DocTypesView({ doctypes: that.model });
                        that.renderDocfield(that.model);
                    } else {
                        var doctypes = that.model;
                        that.model = _.map(doctypes, function (doctype) {
                            doctype.Pinned = false;
                            return doctype;
                        });
                        app.viewdoctypes = new DocTypesView({ doctypes: that.model });
                        that.renderDocfield(that.model);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var doctypes = that.model;
                    that.model = _.map(doctypes, function (doctype) {
                        doctype.Pinned = false;
                        return doctype;
                    });
                    app.viewdoctypes = new DocTypesView({ doctypes: that.model });
                    that.renderDocfield(that.model);
                }
            });
        },

        addDocfield: function (model) {
            var that = this;
            var $listDocField = that.$el.find("#listDocField");
            var $listDoctypeCreate = that.$("#listDoctypeCreate");

            var docfield = new DocFieldCreate({ model: model, elem: $listDoctypeCreate, listDocfield: that.docfields })
            $listDocField.append(docfield.$el);
        },

        renderDocfield: function (doctypes) {
            var that = this;

            var pinnedDocTypes,
            commonDoctypes,
            $pinnedDocTypes,
            $commonDoctypes,
            docFieldIds;
            if (doctypes === null || doctypes.length === 0) {
                return;
            }

            pinnedDocTypes = _.filter(doctypes, function (item) {
                return item.Pinned;
            });
            //_renderDoctypeGroup(pinnedDocTypes, $pinnedDocTypes, true);
            //$pinnedDocTypes.append("<li class='divider'></li>");

            commonDoctypes = doctypes;

            docFieldIds = _.uniq(_.pluck(commonDoctypes, "DocFieldId"));

            var docFieldGroups = _.groupBy(commonDoctypes, function (doctype) {
                return doctype.DocFieldName;
            });


            var modelDocfieldAll = new DocfieldModel({
                DocFieldId: 0,
                Count: doctypes.length,
                DocFieldName: "Tất cả",
                isActive: true
            });
            var modelDocfieldPinned = new DocfieldModel({
                DocFieldId: -1,
                Count: pinnedDocTypes.length,
                DocFieldName: "Báo cáo thường dùng",
                isActive: false,
            });

            that.docfields.add(modelDocfieldAll);
            that.docfields.add(modelDocfieldPinned);

            _.each(docFieldGroups, function (doctypesModel, docfieldName) {
                var modelDocfield = new DocfieldModel({
                    DocFieldId: doctypesModel[0].DocFieldId,
                    Count: doctypesModel.length,
                    DocFieldName: docfieldName,
                    isActive: false
                });

                that.docfields.add(modelDocfield);
            });

            app.docfieldCollection = that.docfields;
        }
    });

    var DocFieldCreate = Backbone.View.extend({
        tagName: 'li',

        events: {
            "click": "active"
        },

        initialize: function (options) {
            this.model = options.model;
            this.collections = options.listDocfield;
            this.listenTo(this.model, 'change', this.render);
            this.$elemParent = options.elem;
            this.render();
        },

        templateDocfield: '<a href="#" name="${DocFieldName}"><i class="fa fa-info"></i> ${DocFieldName} <span class="label label-danger pull-right">${Count}</span></a>',

        render: function () {
            var that = this;
            that.renderDocfield(that.model);
            if (that.model.get("isActive")) {
                that.$el.addClass("active");
                that.renderDoctypes();
            } else {
                that.$el.removeClass("active");
            }
        },

        renderDocfield: function (doctypes) {
            var that = this;
            that.$el.html($.tmpl(this.templateDocfield, this.model.toJSON()))
        },

        active: function () {
            var that = this;
            that.collections.removeActive();
            that.model.set({ isActive: true });
        },

        renderDoctypes: function () {
            var that = this;
            var count = app.viewdoctypes.renderDoctypes(that.model.get("DocFieldId"));
            that.model.set({ Count: count.Count });
        }
    });

    var DocTypesView = Backbone.View.extend({
        el: '#leftDoctype',

        events: {
            "keyup #searchDoctype": "searchDoctype"
        },

        docfieldId: 0,

        initialize: function (options) {
            this.DoctypeCollection = new DocTypeCollection();

            this.$elemTable = this.$("#listDoctypeCreate");
            this.input = this.$("#searchDoctype");
            this.listenTo(this.DoctypeCollection, 'add', this.addOne);
            this.listenTo(this.DoctypeCollection, 'reset', this.addAll);
            this.listenTo(this.DoctypeCollection, 'all', this.render);
            this.DoctypeCollection.reset(options.doctypes);
        },

        render: function () {

        },

        addOne: function (model) {
            var view = new DocTypeItemView({ model: model });
            this.$elemTable.append(view.$el);
        },

        addAll: function (models) {
            this.DoctypeCollection.each(this.addOne, this);
        },

        addDoctype: function (model) {
        },

        searchDoctype: function (e) {
            var that = this;
            var countObject = this.renderDoctypes(that.docfieldId);
            var count = countObject.Count;
            var Docfields = countObject.Docfield;

            _.each(Docfields, function (doctypes, docfieldId) {
                var docfieldModel = app.docfieldCollection.find(function (item) {
                    return item.get("DocFieldId") == docfieldId
                });
                if (docfieldModel) {
                    docfieldModel.set({ Count: doctypes.length })
                }
            });
        },

        renderDoctypes: function (docfieldId) {
            var that = this;
            var key = that.input.val();

            this.docfieldId = docfieldId;
            return this.DoctypeCollection.showDoctype(that.docfieldId, key)
        },
    });

    var DocTypeItemView = Backbone.View.extend({
        tagName: 'tr',

        events: {
            "click .create-report": "createDocument",
            "click .pinned-doctype": "_setDoctypePinedToUser"
        },

        initialize: function (options) {
            var className = this.getIcon(this.model.get("ActionLevel"))

            this.model = options.model
            this.model.set({ ViewPeriod: className })
            this.listenTo(this.model, 'change:isActive', this.toggle);
            this.listenTo(this.model, 'change:Pinned', this.render);
            this.render();

        },

        templateDocType: '<td class="inbox-small-cells pinned-doctype">{{if Pinned}}<i class="icon icon-star3" style="color:#ffac00">{{else}}<i class="icon icon-star "  style="color:#ffac00">{{/if}}</i></td>\
                        <td >{{if CategoryBusinessId == 4 }}<span class="label label-primary">Số liệu</span> {{/if}} {{if CategoryBusinessId == 8}}  <span class="label label-danger">Thuyết minh</span> {{/if}}{{if CategoryBusinessId == 64}} <span class="label label-success">Chính phủ</span> {{/if}}</td>\
                    <td>{{if FormCategoryId == 1 }} <span class="label label-warning">Thường</span> {{else FormCategoryId == 3}} <span class="label label-warning">Tổng hợp</span>{{/if}}</td>\
                    <td class="view-message dont-show">${DocTypeName} {{html ViewPeriod}}</td>\
                    <td class="inbox-small-cells"><a class="label label-info pull-right create-report">Lập mới</a></i></td>',

        render: function () {
            var that = this;
            that.renderDoctype(that.model);
            that.$el.attr("id", that.model.get("DocTypeId"));
            that.$el.attr("name", that.model.get("DocTypeName"));
            if (that.model.get("isActive")) {
                that.$el.show();
            } else {
                that.$el.hide();
            }
        },

        getIcon: function (level) {
            switch (level) {
                case 1:
                    return '<span class="label label-success pull-right">Năm</span>';
                case 2:
                    return '<span class="label label-success pull-right">6 tháng</span>';
                case 3:
                    return '<span class="label label-info pull-right">Quý</span>';
                case 4:
                    return '<span class="label label-info pull-right">Tháng</span>';
                case 5:
                    return '<span class="label label-warning pull-right">Tuần</span>';
                case 6:
                    return '<span class="label label-warning pull-right">Ngày</span>';
                case 8:
                    return '<span class="label label-success pull-right">9 Tháng</span>';
                default:
                    return '<span class="label label-danger pull-right">Khẩn cấp</span>';
            }
        },

        toggle: function () {
            var that = this;
            if (that.model.get("isActive")) {
                that.$el.show();
            } else {
                that.$el.hide();
            }
        },

        renderDoctype: function (doctypes) {
            var that = this;
            that.$el.html($.tmpl(this.templateDocType, this.model.toJSON()))
        },

        _setDoctypePinedToUser: function () {
            /// <summary>
            /// Pin/bỏ pin loại văn bản
            /// </summary>
            var pinned = this.model.get("Pinned");
            this.model.set("Pinned", !pinned);

            var doctypeId = this.model.get("DocTypeId");
            if (doctypeId == undefined || doctypeId === "") {
                return;
            }

            $.ajax({
                url: '/Account/PinDocType',
                type: "Post",
                data: {
                    docTypeId: doctypeId
                }
            });
        },

        createDocument: function () {
            egov.views.home.tab.addDocument(this.model.get("DocTypeId"), this.model.get("DocTypeName"), null, false, null, null);
            app.$dialog.dialog("hide");
        }
    });

    return DoctypeCreate;
});
