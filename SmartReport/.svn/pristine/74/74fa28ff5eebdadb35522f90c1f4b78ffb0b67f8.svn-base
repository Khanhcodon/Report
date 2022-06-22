define(['jquery',
    'backbone',
    'text!templates/document/FormHtml/mainform-template.html',
    'text!templates/document/FormHtml/subform-template.html',
    'tmpl'
],
function ($, Backbone, MainForm, SubForm) {
    "use strict";

    var HtmlModel = Backbone.Model.extend({
        defaults: {
            ContentName: "",
            DocumentContentDetails: null,
            DocumentContentId: 0,
            DocumentId: null,
            FormTypeId: 1,
            IsMain: false,
            Version: 0,
            Content: ''
        },

        initialize: function () { }
    });

    /// <summary>Đối tượng thể hiện một biểu mẫu html</summary>
    var HtmlForm = Backbone.View.extend({
        tagName: 'div',
        events: {
            'touchend .subformTitle': 'toggleContent'
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (option) {
            this.model = new HtmlModel(option.model);
            this.document = option.document;
            this.render();
        },

        /// <summary>Hiển thị nội dung biểu mẫu trn form văn bản</summary>
        render: function () {
            var _this = this;

            if (_this.model.get('Content') !== '') {
                //main form
                if (_this.model.get('IsMain') === true) {
                    _this.$el.attr('id', 'mainForm');
                    _this.renderMainForm();
                }
                else {
                    _this.renderSubFrom();
                }
            } else {
                $.get('/Document/GetFormContent',
                    {
                        contentId: _this.model.get('DocumentContentId'),
                        isCreate: false,
                        doctypeId: _this.document.model.get('DocTypeId')
                    }
                ).done(function (result) {
                    _this.model.set('Content', result);
                    //main form
                    if (_this.model.get('IsMain') === true) {
                        _this.$el.attr('id', 'mainForm');
                        _this.renderMainForm();
                    }
                    else {
                        _this.renderSubFrom();
                    }
                });
            }
        },

        renderMainForm: function () {
            this.$el.append($.tmpl(MainForm, this.model.toJSON()));
        },

        renderSubFrom: function () {
            if (this.$el.find('#subForm').length === 0) {
                this.$el.append("<div id ='subForm'></div>")
            }
            this.$subForm = this.$('#subForm');
            this.$subForm.append($.tmpl(SubForm, this.model.toJSON()));
        },

        /// <summary>Hiển thị nội dung biểu mẫu trên form sửa.</summary>
        renderDialog: function () {
            var _this = this;
            var content = this.$el;
            content.editor();
            content.dialog({
                height: '400px',
                width: '900px',
                draggable: true,
                title: this.model.ContentName,
                buttons: [
                    {
                        text: 'Đóng',
                        click: function () {
                            content.editor('destroy');
                            _this.document.renderForm();
                            content.dialog('destroy');
                        }
                    },
                    {
                        text: 'Lưu',
                        className: 'btn-primary',
                        click: function () {
                            content.editor('destroy');
                            _this.model.Content = content.html();
                            _this.document.renderForm();
                            content.dialog('destroy');
                        }
                    }
                ]
            });
        },

        toggleContent: function (e) {
            e = e || window.event;
            e.preventDefault();
            if ($('.showOrHideContent').html().indexOf("▼") >= 0) {
                $('.showOrHideContent').html($('.showOrHideContent').html().replace("▼", "►"));
                $('.showOrHideContent').attr("title", "Hiển thị biểu mẫu");
                this.$('.content').hide();
            } else {
                $('.showOrHideContent').html($('.showOrHideContent').html().replace("►", "▼"));
                $('.showOrHideContent').attr("title", "Ẩn biểu mẫu");
                this.$('.content').show();
            }
        }
    });

    //Thay đổi trạng thái 
    var changeNavi = function (titleObj) {
        if ($(titleObj).html() != "") {
            if ($(titleObj).html().indexOf("▼") >= 0) {
                $(titleObj).html($(titleObj).html().replace("▼", "►"));
                $(titleObj).attr("title", "Hiển thị biểu mẫu");
            } else {
                $(titleObj).html($(titleObj).html().replace("►", "▼"));
                $(titleObj).attr("title", "Ẩn biểu mẫu");
            }
        }
    };

    return HtmlForm;
});