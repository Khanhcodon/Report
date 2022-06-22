define([
    'knockout',
    'eFormJqueryGlobal',
    'eFormJsutilt',
    'eFormLibdata',
    'eFormControls',
    'eFormLib',
    'eFormDB',
    'eFormTool',
    'eFormResize',
    'eFormBkavEgateApplet',
    'eFormBkavEgate',
    'eFormBkavEgateCompiler',
    'eFormJqueryMaskedinput',
    'eFormJqueryMeioMask'
],
function (ko) {
    "use strict";
    window.ko = ko;

    var bindForm = function (model, divRoot) {
        eForm.Lib.Init();
        eForm.efTools.init(null, divRoot, model.formid);
        // eForm.efTools.LoadForm(model.schema, model.maxRow);

        // Tạo danh mục catatalog
        fformModel.fromCatalog(model.collections); // egate.compiler.js

        // Chuẩn bị view cho form động + tạo database cho form động từ model.schema
        eForm.efTools.LoadForm(model.schema, model.maxRow); // eForm.Tool.js

        // Chuẩn bị model cho form động
        var partModel = fformModel.fromSchema(eForm.database.GetAll(model.formid), model.formid); // egate.compiler.js
        $.extend(fformModel, partModel);

        // Dùng knockoutjs gắn view + model vào với nhau để hiển thị lên web.
        ko.applyBindings(fformModel, document.getElementById(divRoot));
        // eForm.setMask();
        eForm.efTools.ArrowKeys.init();
    }

    /// <summary>Đối tượng View thể hiện một biểu mẫu động.</summary>
    var DynamicForm = Backbone.View.extend({

        // Khởi tạo
        initialize: function (option) {
            this.id = option.id;
            this.document = option.document;
            this.isView = false;
            var that = this;
            var isCreate = this.document.isCreate == true;
            egov.request.getFormContent({
                data: { contentId: that.id, isCreate: isCreate, doctypeId: that.document.model.get('DocTypeId') },
                success: function (result) {
                    var form = JSON.parse(result);
                    //Kiểm tra chuỗi json có phải 1 mảng hay không
                    $("head").append("<style>.icontainer:hover {background: none !important;}#pnl_root, .pnl_root {background-color: transparent;}</style>");
                    that.model = (form instanceof Array) ? form : [form];
                    that.render();
                    return;
                    if (form instanceof Array) {
                        that.model = [];
                        for (var i = 0; i < form.length; i++) {
                            that.formTmp = form[i];
                            var itemModel = {
                                collections: form[i].JssCatalog,
                                schema: form[i].JssForm,
                                formid: form[i].FormId.replace(/-/gi, ''),
                                maxRow: form[i].MaxRow
                            };
                            that.model.push(itemModel);
                            that.render();
                        }
                    } else {
                        that.formTmp = result;
                        var itemModel = {
                            collections: form.JssCatalog,
                            schema: form.JssForm,
                            formid: form.FormId.replace(/-/gi, ''),
                            maxRow: form.MaxRow
                        };
                        that.model = [itemModel];
                        that.render();
                    }
                }
            });
        },

        /// <summary>Hiển thị form trong document form: form hiển thị dạng View</summary>
        render: function () {
            var that = this;
            this.$el.css({
                "margin-bottom": "80px"
            });
            for (var i = 0; i < this.model.length; i++) {
                var form = this.model[i];
                var formIndex = Math.floor((Math.random() * 99999) + 1);
                var itemModel = {
                    collections: form.JssCatalog,
                    schema: form.JssForm,
                    formid: form.FormId,
                    maxRow: form.MaxRow
                };

                form.originalFormId = form.FormId;
                form.FormId = formIndex + form.FormId;
                itemModel.formid = form.FormId.replace(/-/gi, '');

                var formElementId = "div" + formIndex + "_" + itemModel.formid;
                this.$el.addClass("pnl_root sub_pnl_root").append("<div id='" + formElementId + "'></div>");

                bindForm(itemModel, formElementId);
            }
            if (egov.mobile) {
                egov.mobile.niceScrollIOS(this.document.$(".autoscroll"));
            }
            return this;
        },

        loadForm: function (formModel, isView) {
            if (this.isView == undefined) {
                isView = false;
            }
            var _this = this;
            var settings = {};
            settings.height = 600;
            settings.width = 1000;
            settings.title = _this.contentName;
            if (!isView) {
                settings.buttons = [
                    {
                        text: "Cập nhật",
                        click: function () {
                            var form = _this.update();
                            if (form !== "") {
                                egov.views.base.dialog.close();
                                _this.rebindForm(form);
                            }
                        }
                    },
                    {
                        text: "Đóng",
                        click: function () {
                            $("#div" + formModel.formid).remove();
                            egov.views.base.dialog.close();
                        }
                    }
                ];
            }
            $("#div" + formModel.formid).remove();
            var _dialogEdit = $('<div></div>').attr("id", "div" + formModel.formid);
            _dialogEdit.addClass("pnl_root sub_pnl_root").appendTo('body');//

            ///khởi tạo các tham số cho form động
            eForm.Lib.Init();
            bindForm(formModel, "div" + formModel.formid, false);
            egov.views.base.dialog.openexist($("#div" + formModel.formid), settings, null, null, false);
        },

        serialize: function () {
            var result = [];
            for (var i = 0; i < this.model.length; i++) {
                var form = this.model[i];
                var formJson = "{";
                var formIdStr = form.FormId.replace(/[\-&]+/g, '');
                var docFieldJson = eForm.database.JsonSerialize3(formIdStr);
                formJson += "\"FormId\": \"" + form.originalFormId + "\",";
                formJson += "\"GlobalCode\":\"" + form.GlobalCode + "\",";
                formJson += "\"Description\":\"" + form.Description + "\",";
                formJson += "\"DocFieldJson\":" + docFieldJson;
                formJson += "}";
                form.Content = formJson;
                form.FormTypeId = egov.enum.formType.dynamic;
                result.push(JSON.stringify(form));
            }

            return result;
        }
    });

    return DynamicForm;
});