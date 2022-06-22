define(function () {
    var documentExt = null;
    var rowIndex = 1;
    var isCreate = null;
    var FormModel = Backbone.Model.extend({
        defaults: {
            isSerialize: false
        },
    });

    var FormList = Backbone.Collection.extend({
        model: FormModel
    });

    var FormTemplate = Backbone.View.extend({
        events: {
        },

        FormList: new FormList,

        initialize: function (options) {
            var that = this;
            this.rowIndex = 0;
            that.config = options.config;
            that.callBack = options.callback;
            that.$document = options.document;
            documentExt = options.document;
            that.btnAdd = that.$document.$el.find("#addItemForm");

            this.$listForm = that.$document.$el.find("#formContentmbsc");
            this.models = options.models;

            this.allCatalogValues = options.allCatalogValues;

            isCreate = options.isCreate;
            if (!isCreate) {
                that.btnAdd.hide();
            }
            this.listenTo(this.FormList, 'add', this.addOne);
            this.listenTo(this.FormList, 'reset', this.addAll);
            this.listenTo(this.FormList, 'all', this.render);

            that.btnAdd.click(function (e) {
                that.addItem()
            })

            this.FormList.reset(options.models);

            that.callBack()
        },

        addOne: function (model) {
            var view = new FormTemplateItem({ model: model, config: this.config, allCatalogValues: this.allCatalogValues });
            this.$listForm.append(view.$el);

            if (this.rowIndex == 0)
                this.allCatalogValues = view.allCatalogValues;

            this.rowIndex++;
        },

        addAll: function (models) {
            var that = this;
            this.FormList.each(this.addOne, that);
        },

        addItem: function () {
            var model = this.renderDefault();
            this.FormList.add(new FormModel(model));
            this.$listForm.trigger('mbsc-enhance');
        },

        serializeForm: function () {
            this.FormList.forEach(function (item) {
                item.set("isSerialize", !item.get("isSerialize"))
            });

            return this.FormList.toJSON();
        },

        renderData: function () {
            var that = this;
            var data = that.models.toJSON();
        },

        renderDefault: function () {
            var that = this;
            var defaultModel = {};
            var keys = Object.keys(that.config);
            for (var i = 0; i < keys.length; i++) {
                //var item = that.config[keys[i]];
                //defaultModel[keys[i]] = that.config[keys[i]].defaultValue;
                defaultModel[i] = that.config[keys[i]].DefaultValue;
            }
            
            return defaultModel;
        }
    });

    var FormTemplateItem = Backbone.View.extend({
        tag: "div",
        events: {
        },

        initialize: function (options) {
            this.config = options.config;
            this.model = options.model;
            this.allCatalogValues = options.allCatalogValues;

            this.listenTo(this.model, 'change:isSerialize', this.serialize);

            this.$el.addClass("mbsc-form-group form-item")
            this.renderData();

            that = this;
        },

        // 20190103 VuHQ START
        //renderTemplate: function () {
        //    var that = this;
        //    var tmpl = ''
        //    debugger;
        //    var keys = Object.keys(that.config);

        //    for (var i = 0; i < keys.length; i++) {
        //        var item = that.config[keys[i]];
        //        if (item.type == "bool") {
        //            tmpl += ' <label class="mbsc-switch-success">' + item.name +
        //                            '<input type="checkbox" data-role="switch" name="' + keys[i] + '" {{if ' + keys[i] + ' == "1"}} checked {{/if}}></label>'
        //        } else if (item.type == "string") {
        //            tmpl += '<label for="' + keys[i] + '">' + item.name +
        //                           '<input id="' + keys[i] + '" type="text"  value="${' + keys[i] + '}" name="' + keys[i] + '" />\
        //                       </label>'
        //        } else if (item.type == "select") {
        //            var a = '<label>' + item.name +
        //               '<select name="' + keys[i] + '" class="select-mobiscroll">\
        //                   <option value="1">Atlanta</option>\
        //               </select>\
        //           </label>';
        //        } else if (item.type == "title") {
        //            tmpl += ' <div class="mbsc-form-group-title">${' + keys[i] + '}</div>';
        //            tmpl += ' <input type="hidden" value="${' + keys[i] + '}" name="' + keys[i] + '" />';
        //        } else if (item.type == "number") {
        //            tmpl += '<label for="' + keys[i] + '">' + item.name +
        //                         '<input id="' + keys[i] + '" type="number" placeholder="' + item.name + '" value="${' + keys[i] + '}" name="' + keys[i] + '" />\
        //                       </label>'
        //        }
        //    }
        //    tmpl += "";

        //    return tmpl;
        //},
        renderTemplate: function () {
            var that = this;
            var tmpl = ''
            var keys = Object.keys(that.config);
            var cellData = '';
            var readOnlyCount = 0;

            // 20200204 VuHQ START tìm trường hợp đặc biệt (2 field đầu readonly)
            for (var i = 0; i < keys.length; i++) {
                if (i == 0 || i == 1) {
                    var item = that.config[keys[i]];
                    if (item.ReadOnly)
                        readOnlyCount++;
                }
            }
            // 20200204 VuHQ END tìm trường hợp đặc biệt (2 field đầu readonly)
            tmpl += "<hr/>";
            var isInline = false;
            var inlineStr = '';
            var inlineNo = 0;
            var inlineControls = '';

            for (var i = 0; i < keys.length; i++) {
                var item = that.config[keys[i]];
                var fieldNameAscii = keys[i].split("!!")[0];
                var fieldName = keys[i].split("!!")[1];
                if (that.model.attributes != undefined) {
                    if (isCreate)
                        cellData = that.model.attributes[i];
                    else
                        cellData = that.model.attributes[fieldNameAscii]
                }

                // 20200204 VuHQ START tìm trường hợp đặc biệt (2 field đầu readonly)
                //if ((i == 0 || i == 1) && readOnlyCount == 2) {
                //    tmpl += '<input id="' + fieldNameAscii + '_' + rowIndex + '" type="hidden"  value="' + cellData + '" name="' + fieldNameAscii + '"/>';
                //    if (i == 1)
                //        tmpl += (isCreate ? that.model.attributes[i - 1] : that.model.attributes[keys[i - 1].split("!!")[0]]) + "(" + cellData + ")";
                //    continue;
                //}
                if (item.ReadOnly && item.IsInline) {
                    
                    inlineControls += '<input id="' + fieldNameAscii + '_' + rowIndex + '" type="hidden"  value="' + cellData + '" name="' + fieldNameAscii + '"/>';
                    inlineStr += (!isInline ? "" : " - ") + cellData;
                    isInline = true;

                    continue;
                }
                else {
                    isInline = false;
                    tmpl += '<label for="inline_' + inlineNo + '" style="font-size: 13px; padding-left: 0px !important; padding-top: 20px !important">' + inlineStr + '</label>';
                    tmpl += inlineControls;
                    inlineStr = '';
                    inlineControls = '';
                    inlineNo = 1;
                }

                // 20200204 VuHQ END tìm trường hợp đặc biệt (2 field đầu readonly)

                var strReadOnly = item.ReadOnly ? "readonly" : "";
                var strHidden = item.Hidden ? "hidden" : (item.HiddenMobile ? "hidden" : "");

                if (item.TypeHandson == "checkbox") {
                    if (cellData == "true" || cellData == true)
                        tmpl += '<label ' + strHidden + ' for="' + fieldNameAscii + '_' + rowIndex + '" style="font-size: 13px; padding-left: 0px !important">' + fieldName +
                            '<input ' + strReadOnly + ' type="checkbox" data-role="switch" id="' + fieldNameAscii + '_' + rowIndex + '" name="' + fieldNameAscii + '" checked></label>';
                    else
                        tmpl += '<label ' + strHidden + ' for="' + fieldNameAscii + '_' + rowIndex + '" style="font-size: 13px; padding-left: 0px !important">' + fieldName +
                            '<input ' + strReadOnly + ' type="checkbox" data-role="switch" id="' + fieldNameAscii + '_' + rowIndex + '" name="' + fieldNameAscii + '"></label>';
                } else if (item.TypeHandson == "text") {
                    tmpl += '<label class="mbsc-control-w mbsc-input"' + strHidden + '  for="' + fieldNameAscii + '_' + rowIndex + '">';
                    tmpl += strReadOnly != "readonly" ? fieldName : cellData;
                    tmpl += '<input id="' + fieldNameAscii + '_' + rowIndex + '" type="' + (strReadOnly != "readonly" ? "text" : "hidden") + '"  value="' + cellData + '" name="' + fieldNameAscii + '"/>\
                               </label>'
                } else if (item.TypeHandson == "numeric") {
                    tmpl += '<label class="mbsc-control-w mbsc-input" ' + strHidden + '  for="' + fieldNameAscii + '_' + rowIndex + '">';
                    tmpl += strReadOnly != "readonly" ? fieldName : cellData;
                    tmpl += '<input ' + strReadOnly + ' id="' + fieldNameAscii + '_' + rowIndex + '" type="' + (strReadOnly != "readonly" ? "number" : "hidden") + '" step="any" value="' + cellData + '" name="' + fieldNameAscii + '" pattern="' + item.RegEx + '" title="Sai định dạng"/>\
                               </label>';
                } else if (item.TypeHandson == "dropdown") {
                    var formId = documentExt.model.get("DocumentContents")[0].FormId;
                    $.ajaxSetup({ async: false });
                    $.getJSON('/Admin/Form/GetCatalogValues',
                        { catalogName: item.CatalogName, row: i, col: 1, formId: formId, isXoay: false },
                        function (data, textStatus, jqXHR) {
                            var source = JSON.parse(data.catalogValues);
                            var catalogValuesAscii = JSON.parse(data.catalogValuesAscii);
                            if (strReadOnly != "readonly") {
                                tmpl += '<label ' + strHidden + ' for="' + fieldNameAscii + '_' + rowIndex + '">' + fieldName + '<select id="' + fieldNameAscii + '_' + rowIndex + '" name="' + fieldNameAscii + '"class="select-mobiscroll ' + (strReadOnly == "readonly" ? "disabled" : "") + '">';
                                _.each(source, function (sourceDetail, index) {
                                    tmpl += '<option';
                                    tmpl += ' value="' + sourceDetail + '"';
                                    tmpl += (cellData == sourceDetail ? " selected" : "") + '> ' + sourceDetail;
                                    tmpl += '</option>';
                                });

                                // 20200228 START CatalogName => CatalogKey START
                                var catalogInfos = JSON.parse(data.catalogInfos);
                                jQuery.each(catalogInfos, function (i, val) {
                                    that.allCatalogValues.push(val);
                                });

                                that.allCatalogValues = _.uniq(that.allCatalogValues);
                                // 20200228 START CatalogName => CatalogKey END
                            }

                            else {
                                tmpl += '<label class="mbsc-control-w mbsc-input" ' + strHidden + '  for="' + fieldNameAscii + '_' + rowIndex + '">' + cellData +
                                    '<input id="' + fieldNameAscii + '_' + rowIndex + '" type="hidden"  value="' + cellData + '" name="' + fieldNameAscii + '"/>\
                               </label>'
                            }
                            
                            tmpl += '</select></label>';
                        });
                    $.ajaxSetup({ async: true });
                } else if (item.TypeHandson == "date") {
                    var dateData = new Date(cellData);
                    var dateStr = dateData.getFullYear() + "-" + ("0" + (dateData.getMonth() + 1)).slice(-2) + "-" + ("0" + dateData.getDate()).slice(-2);
                    tmpl += '<label class="mbsc-control-w mbsc-input" ' + strHidden + ' for="' + fieldNameAscii + '_' + rowIndex + '">';
                    tmpl += strReadOnly != "readonly" ? fieldName : dateStr;
                    tmpl += '<input ' + strReadOnly + ' type="' + (strReadOnly != "readonly" ? "date" : "hidden") + '" name="' + fieldNameAscii + '" id="' + fieldNameAscii + '_' + rowIndex + '" value="' + dateStr + '" \
                        style="width: 120px; display: inline" class="date" /></label>';
                } else {

                }
            }

            rowIndex++;
            tmpl += "";
            
            return tmpl;
        },
        // 20190103 VuHQ END

        renderData: function () {
            var that = this;
            var data = that.model.toJSON();
            var template = that.renderTemplate();
            $.template("FormContentTemplate", template);

            this.$el.html($.tmpl("FormContentTemplate", data));
        },

        serialize: function () {
            var that = this
            for (var attr in that.model.attributes) {
                control = that.$el.find('[name="' + attr + '"]');
                if (control.length > 0 && (control.is("input") || control.is("select") || control.is("textarea"))) {
                    if (control.is((":checkbox"))) {
                        var isChecked = control.is(":checked");
                        if (isChecked) {
                            that.model.set(attr, 1);
                        } else {
                            that.model.set(attr, 0);
                        }
                        continue;
                    }
                    val = control.val();
                    if (val === '' && that.model.get(attr) == null) {
                        continue;
                    }

                    that.model.set(attr, val);
                }
            }
        },

        confiDefault: function () {
            var config = {
                "hovatenchuho": {
                    "type": "string",
                    "name": "Tên chủ hộ",
                    "defaultValue": "",
                    "index": 2
                },
                "hongheo": {
                    "type": "bool",
                    "name": "Là hộ nghèo", "defaultValue": 0, "index": 3
                },
                "chatluongnuocsinhhoatdangsudung_nuochopvesinh": {
                    "type": "bool", "name": "Sử dụng nước hợp vệ sinh",
                    "defaultValue": 0, "index": 4
                },
                "chatluongnuocsinhhoatdangsudung_nuocsach": {
                    "type": "bool", "name": " Sử dụng nước sạch", "defaultValue": 0, "index": 5
                },
                "nguoncapnuoc_congtrinhcnnl": { "type": "bool", "name": "Nguồn cấp nước nhỏ lẻ", "defaultValue": 0, "index": 6 },
                "nguoncapnuoc_congtrinhcntt": { "type": "bool", "name": " Nguồn cấp nước tập trung", "defaultValue": 0, "index": 7 }
            }
        }
    });

    //#endregion

    return FormTemplate;
});