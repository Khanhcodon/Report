define(['text!templates/modal.html'],
    function (template) {
        "use strict";

        //Định nghĩa kiểu các control
        var control = {
            button: 0,
            checkbox: 1,
            textbox: 2
        };

        /// <summary>Lớp thể hiện một modal</summary>
        egov.views.modal = Backbone.View.extend({
            className: 'modal',
            template: template,

            valueConfirm: false,

            events: {
                'click .checkbox.ignore': 'ignore',
                'click .close': '_close'
            },

            /// <summary>Khởi tạo</summary>
            initialize: function () {
                this.$el.html($.tmpl(this.template, this.model.toJSON()));

                this.$buttons = this.$('.modal-footer');
                var that = this;
                this.$el.on('hidden.bs.modal', function (e) {
                    if (typeof that.model.get('hidden') === 'function') {
                        that.model.get('hidden')();
                    }
                });

                this.$el.on('hide.bs.modal', function (e) {
                    if (typeof that.model.get('hide') === 'function') {
                        that.model.get('hide')();
                    }
                });

                this.$el.on('loaded.bs.modal', function (e) {
                    if (typeof that.model.get('loaded') === 'function') {
                        that.model.get('loaded')();
                    }
                });

                // Todo: tính kích thước màn hình và set max-height, scroll auto cho body.
                // Check cả windows resize và tính lại cho phù hợp

                this.$('.modal-body').html(this.model.get('content'));

                this.render();
            },

            /// <summary>Hiển thị modal mới</summary>
            render: function () {
                var option = this.model.toJSON();
                this.$el.modal(option);

                this.$('.modal-dialog').width(option.width);
                this.$('.modal-body').height(option.height);
                if (option.top) {
                    this.$('.modal-dialog').css("top", option.top);
                }
                // hiển thị các button
                this.$buttons.empty();
                if (this.model.get('footer')) {
                    this.$buttons.append(this.model.get('footer'));
                }

                if (this.model.get('confirm')) {
                    this.$buttons.append(this._parseConfirm(this.model.get('confirm')));
                }
                
                var buttons = this.model.get('buttons'),
                    leng = buttons.length;
                if (leng == 0) {
                    this.$buttons.hide();
                } else {
                    for (var i = 0; i < leng; i++) {
                        var button = this._parseButton(buttons[i]);
                        this.$buttons.append(button);
                    }
                }

                if (this.model.get("helpContent")) {
                    this.$(".help-content").attr("href", "/Content/help/index.html#" + this.model.get("helpContent"));
                    this.$(".help-content").attr("target", "_blank");
                }

                // Hiển thị ignore
                if (this.model.get('ignoreText') === '') {
                    this.$('.checkbox.ignore').hide();
                }

                if (this.model.get('draggable') && typeof $.fn['draggable'] === 'function') {
                    this.$(".modal-content").draggable({
                        containment: "document",
                        handle: this.$(".modal-header")
                    });
                }
                if (this.model.get('resizeable')) {
                    $('#modal').on('show.bs.modal', function () {
                        $(this).find('.modal-body').css({
                            width: 'auto', //probably not needed
                            height: 'auto', //probably not needed 
                            'max-height': '100%'
                        });
                    });
                }

                return this;
            },

            reRender: function (model) {
                this.model = model;
                this.render();
            },

            ignore: function (e) {
                this.$('.checkbox.ignore').toggleClass('checked');
                egov.helper.destroyClickEvent(e);
            },

            /// <summary>Ẩn modal</summary>
            hide: function () {
                this.$el.modal('hide');
            },

            /// <summary>Hiển thi modal đã có</summary>
            show: function () {
                this.$el.modal('show')
            },

            /// <summary>Hủy modal</summary>
            destroy: function () {
                this.hide();
                this.$el.remove();
            },

            /// <summary>Không hiển thị lần sau</summary>
            isIgnore: function () {
                return this.$('.checkbox.ignore').hasClass('checked');
            },

            /// <summary>Hiển thị tất cả các nút của Dialog</summary>
            disableButtons: function () {
                this.$buttons.find("button").attr("disabled");
            },

            enableButtons: function () {
                this.$buttons.find("button").removeAttr("disabled");
            },

            /// <summary>Tạo button element theo cấu hình</summary>
            _parseButton: function (button) {
                var result = $('<button>').attr('type', 'button').addClass('btn');
                result.text(button.text);
                result.attr('data-disable', button.disableProcess ? true : false);
                // add class nếu cấu hình
                if (button.className && button.className !== '') {
                    result.addClass(button.className);
                }
                else {
                    result.addClass('btn-default');
                }

                if (button.style) {
                    result.css(button.style);
                }

                // add id nếu cấu hình
                if (button.id && button.id !== '') {
                    result.attr('id', button.id);
                }

                if (button.disabled) {
                    result.attr("disabled", "disabled");
                }

                var _this = this;
                // Sự kiện click
                result.click(function () {
                    if (button.disableProcess) {
                        var buttons = _this.$buttons.find('button[data-disable="true"]');
                        $(buttons).each(function () {
                            $(this).attr("disabled", "disabled");
                        });
                    }
                    // _this.disableButtons();

                    if (typeof button.click === 'function') {
                        button.click(function () {
                            if (button.disableProcess) {
                                $(buttons).each(function () {
                                    $(this).removeAttr("disabled");
                                });
                            }
                        });
                    }

                    //kiểm tra nếu là nút button chính và có confirm xác nhận
                    // Xử lý nút check trên dialog
                    if (result.hasClass('btn-primary') && _this.model.get('confirm')) {
                        if (typeof _this.model.get('confirm').callback === 'function' && _this.valueConfirm) {
                            _this.model.get('confirm').callback(!_this.valueConfirm);
                        }
                    }
                });

                return result;
            },

            //Tạo checkbox xác nhận
            _parseConfirm: function (confirm) {
                var defaultObj = {

                };
                var checkboxTemplate =
                    '<label>'
                    + ' <div class="checkbox document-color">'
                    + '    <input type="checkbox" />'
                    + '     <span class="document-color-1">'
                    + '       <i class="icon-check"></i>'
                    + '    </span>'
                    + '  </div>'
                    + '   <span>' + confirm.text + '</span>'
                    + '</label>';

                var result = $(checkboxTemplate);

                // add id nếu cấu hình
                if (confirm.id && confirm.id !== '') {
                    result.find("input[type='checkbox']").attr('id', confirm.id);
                }

                if (confirm.name) {
                    result.find("input[type='checkbox']").attr('name', confirm.name);
                }

                if (confirm.checked) {
                    result.find("input[type='checkbox']").attr("checked", "checked");
                }

                if (confirm.disabled) {
                    result.find("input[type='checkbox']").attr("disabled", "disabled");
                }

                if (confirm.className && confirm.className !== '') {
                    result.addClass(confirm.className);
                }

                if (confirm.style) {
                    result.css(confirm.style);
                }
                var _this = this;
                result.find("input[type='checkbox']").on('click', function () {
                    _this.valueConfirm = $(this).prop('checked');
                    egov.callback(confirm.click, _this.valueConfirm);
                });

                //Check có tự động cho click xác nhận trên form hay không
                if (confirm.hasAutoClick) {
                    //Chỗ này để time out vì khi bind ra form cần 1 khoảng thời gian để hiển thị lên khi đã bind giao diện thì mới cho click
                    window.setTimeout(function () {
                        result.find("input[type='checkbox']").prop('checked', true);
                        _this.valueConfirm = true;
                        egov.callback(confirm.click, true);
                    }, 500);
                }

                return result;
            },

            ///Click nút x đóng dialog
            _close: function () {
                if (this.$('.closeDialog, .btn-close, .btn-cancel')) {
                    this.$('.closeDialog, .btn-close, .btn-cancel').trigger('click');
                } else {
                    this.destroy();
                }
            }
        });

        return egov.views.modal;
    });

