define(['jquery', 'editor'],
    function ($, editor) {

        var EditFormHTMLView = Backbone.View.extend({
            initialize: function (options) {
                this.frame = options.frame;
                this.contentId = options.contentId;
                this.doctypeId = options.doctypeId;
                this.open();
            },

            open: function () {
                var _this = this;
                var _content = $('.content[data-contentid=' + _this.contentId + ']', $('#' + this.frame).contents());
                var dialogEdit = $('<div id="editHTML" name="editHTML"></div>');
                var settings = {
                    height: 500,
                    width: 1000,
                    title: "Chỉnh sửa HTML"
                };
                settings.buttons = [
                        {
                            text: "Cập nhật",
                            click: function () {
                                _this.update(_content);
                                egov.views.base.dialog.close();
                            }
                        },
                        {
                            text: "Đóng",
                            click: function () {
                                egov.views.base.dialog.close();
                            }
                        }
                ];
                if (_content.html() === null || _content.html() === "") {
                    $.ajax({
                        url: "/Document/GetFormContent",
                        type: "GET",
                        data: {
                            contentId: _this.contentId,
                            isCreate: false,
                            doctypeId: _this.doctypeId
                        },
                        success: function (result) {
                            dialogEdit.remove();
                            dialogEdit.html(result).appendTo('body');
                            _content.html(result).css({ 'display': 'none' });
                            egov.views.base.dialog.open(dialogEdit[0], settings);
                            enableEditor(dialogEdit[0]);
                        }
                    });
                } else {
                    dialogEdit.remove();
                    dialogEdit.html(_content.html()).appendTo('body');
                    egov.views.base.dialog.open(dialogEdit[0], settings);
                    enableEditor(dialogEdit[0]);
                }
            },

            ///summary> Cập nhật Hml đã chỉnh sửa</summary>
            update: function (_content) {
                var _this = $(this);
                var newContentHTML = CKEDITOR.instances.editHTML.getData();
                _content.html(newContentHTML);
                egov.message.notification('Chỉnh sửa nội dung thành công!.', egov.message.messageTypes.success);

                //huy editor
                if (editor)
                    editor.destroy();
            },
        });

        ///<summary>Chuyển 1 thẻ html vào trong editor</summary>
        var enableEditor = function (div, onloaded) {
            if (editor) {
                try {
                    var name = editor.name;
                    CKEDITOR.instances[name].destroy();
                } catch (e) {
                    editor.destroy();
                }
            }
            editor = CKEDITOR.replace(
                div,
                {
                    height: $(div).height(),
                    toolbar: "Basic"
                }
            );
            if (onloaded && typeof onloaded === 'function') {
                editor.on('instanceReady', onloaded);
            }
        };

        return EditFormHTMLView;
    });