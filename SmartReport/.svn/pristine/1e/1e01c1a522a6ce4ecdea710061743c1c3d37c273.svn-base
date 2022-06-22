define([
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
], function () {
    //<summary>Class hiển thị form động</summary>
    var FormDynamicView = Backbone.View.extend({

        initialize: function (options) {
            this.target = options.target;
            this.formJson = options.formJson;
            this.isView = options.isView;
            this.frame = options.frame;
            this.render();
        },

        render: function () {
            eForm.Lib.Init();
            var _this = this;
            var form = JSON.parse(_this.formJson);
            var itemModel = {
                collections: form.JssCatalog,
                schema: form.JssForm,
                formid: form.FormId,
                maxRow: form.MaxRow
            };
            var target = $(_this.target, $('#' + this.frame).contents());
            // var target = $(_this.target);
            target.addClass("pnl_root sub_pnl_root").attr("id", "div" + form.FormId);
            var divId = "div" + form.FormId;
            var $div = $('<div></div>');
            $div.attr('id', divId).appendTo('body');
            bindForm(itemModel, "div" + form.FormId, _this.isView);
            target.parent().find("#ContentFields").val(_this.formJson);
            target.html($("#" + divId).html());
            $("#" + divId).remove();
        },
    });

    //Hiển thị dữ liệu ra form
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

    return FormDynamicView;
});