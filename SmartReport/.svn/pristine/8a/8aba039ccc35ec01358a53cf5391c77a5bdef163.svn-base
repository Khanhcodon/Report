define([
    'jquery',
    'libs/eForm/jquery/jquery.global',
    'libs/eForm/jsutilt',
    'libs/eForm/eForm.Libdata',
    'libs/eForm/eForm.Controls',
    'libs/eForm/eForm.Lib',
    'libs/eForm/eForm.DB',
    'libs/eForm/eForm.Tool',
    'libs/eForm/Resize',
    'libs/eForm/bkav.egate.applet',
    'libs/eForm/bkav.egate',
    'libs/eForm/bkav.egate.compiler'
], function ($) {

    var EditFormDynamicView = Backbone.View.extend({
        initialize: function (options) {
            this.frame = options.frame,
            this.doctypeId = options.doctypeId,
            this.contentId = options.contentId,
            this.isMain = options.isMain,
            this.contentName = options.contentName;

            this.open();
        },

        open: function () {
            var _this = this;
            // egov.views.home.includeFormLibs();
            var $content = $('.content[data-contentid=' + _this.contentId + ']', $('#' + this.frame).contents());
            var formModel = $content.siblings("#Contents").val();
            if (formModel == null || formModel == "") {
                $.ajax({
                    url: "/Document/GetFormContent",
                    type: "POST",
                    data: { contentId: _this.contentId, isCreate: false, doctypeId: _this.doctypeId },
                    success: function (result) {
                        var form = JSON.parse(result);
                        _this.formTmp = result;
                        var itemModel = {
                            collections: form.JssCatalog,
                            schema: form.JssForm,
                            formid: form.FormId,
                            maxRow: form.MaxRow
                        };
                        _this.loadForm(itemModel, false);
                    }
                });
            } else {
                $.ajax({
                    url: "/Document/ParseDynamicFormContent",
                    type: "POST",
                    data: { json: formModel, isFormContent: false },
                    success: function (result) {
                        var form = JSON.parse(result);
                        _this.formTmp = result;
                        var itemModel = {
                            collections: form.JssCatalog,
                            schema: form.JssForm,
                            formid: form.FormId,
                            maxRow: form.MaxRow
                        };
                        _this.loadForm(itemModel, false);
                    }
                });
            }
        },

        update: function () {
            if (!validateAll()) {
                return "";
            }
            var formTmp = this.formTmp;
            if (formTmp != "") {
                var json = JSON.parse(formTmp);
                var formJson = {};
                var docFieldJson = eForm.database.JsonSerialize3(json.FormId);
                formJson.FormId = json.FormId;
                formJson.GlobalCode = json.GlobalCode;
                formJson.Description = json.Description;
                formJson.DocFieldJson = JSON.parse(docFieldJson);

                var form = {};
                form.Content = JSON.stringify(formJson);
                form.ContentName = this.contentName;
                form.FormTypeId = 2;
                form.IsMain = this.isMain;
                var docContentId = parseInt(this.contentId) != this.contentId ? 0 : this.contentId;
                form.DocumentContentId = docContentId;
                var $content = $('.content[data-contentid=' + this.contentId + ']', $('#' + this.frame).contents());
                $content.siblings("#Contents").val(JSON.stringify(form));
                return JSON.stringify(form);
            }
            return "{}";
        },

        rebindForm: function (json) {
            $.ajax({
                url: "/Document/ParseDynamicFormContent",
                type: "POST",
                data: { json: json, isFormContent: false },
                success: function (result) {
                    var form = JSON.parse(result);
                    var itemModel = {
                        collections: form.JssCatalog,
                        schema: form.JssForm,
                        formid: form.FormId,
                        maxRow: form.MaxRow
                    };
                    var target = $('.content[data-contentid=' + JSON.parse(json).DocumentContentId + ']', $('#' + this.frame).contents());
                    $(target).addClass("pnl_root sub_pnl_root").attr("id", "div" + form.FormId);
                    bindForm(itemModel, "div" + form.FormId, true);
                    $(target).parent().find("#Contents").val(result);
                }
            });
        },

        loadForm: function (formModel, isView) {
            if (isView == undefined) {
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
    });

    //bind dữ liêu cho form động
    var bindForm = function (model, divRoot, isView) {
        // Khởi tạo efTools
        eForm.efTools.init(null, divRoot, model.formid);
        // Tạo danh mục catatalog
        fformModel.fromCatalog(model.collections); // egate.compiler.js
        // Chuẩn bị view cho form động + tạo database cho form động từ model.schema
        if (isView) {
            eForm.efTools.ViewForm(model.schema, model.maxRow); // eForm.Tool.js
        }
        else {
            eForm.efTools.LoadForm(model.schema, model.maxRow); // eForm.Tool.js
        }

        // Chuẩn bị model cho form động
        var partModel = fformModel.fromSchema(eForm.database.GetAll(model.formid), model.formid); // egate.compiler.js
        $.extend(fformModel, partModel);
        // Dùng knockoutjs gắn view + model vào với nhau để hiển thị lên web.
        ko.applyBindings(fformModel, document.getElementById(divRoot));
    };

    // Kiểm tra dữ liệu nhập đầu vào
    var validateAll = function () {
        var isValid = true;
        var invalidObj = null;
        // Form động
        $('.ffield').each(function () {
            $(this).blur();
        });

        $('.cssErr').each(function () {
            if ($(this).css('display') != 'none' && isValid) {
                invalidObj = $(this);
                isValid = false;
            }
        });
        if (!isValid) {
            $('#' + invalidObj.attr('controlValidate')).focus();
            return false;
        }
        return true;
    };

    return EditFormDynamicView;
});