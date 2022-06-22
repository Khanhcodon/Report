define([], function () {
    "use strict";

    var CommentView = Backbone.View.extend({

        initialize: function (options) {
            this.frame = options.frame;
            this.docCopyId = options.docCopyId;
            this.isToolbar = options.isToolbar;

            this.open();
            this.delegateEvents();
        },

        ///<summary>Mỏ Dialog nhập comment</summary>
        open: function () {
            var _this = this;
            var dialog = egov.views.base.dialog;
            var settings = { // dialog setting
                width: 500,
                title: "Gửi ý kiến",
                buttons: [
                    {
                        text: "Đồng ý",
                        click: function () {
                            _this.send(_this.docCopyId, _this.isToolbar);
                        }
                    },
                    {
                        text: "Bỏ qua",
                        click: function () {
                            dialog.close();
                        }
                    }
                ]
            };
            var htmlAdd = "<textarea tabindex='1' style='min-height: 0px;width:98%' rows='5' id='newComment' cols='20'></textarea>";
            dialog.open(htmlAdd, settings);
        },

        ///<summary>Gửi comment</summary>
        send: function (documentCopyId, isToolbar) {
            isToolbar = true;
            var dialog = egov.views.base.dialog;
            var frameDocument;
            var _this = this;
            var frame = _this.frame;
            var token = $("input[name='__RequestVerificationToken']", "#DocumentSendComment").val();
            var comment = $("#newComment");
            if (_this.frame) {
                frameDocument = document.getElementById(_this.frame).contentWindow;
            }
            if (comment.val() === '') {
                comment.css({
                    "border": "1px solid #ff0000",
                    "background-color": "#ffeeee"
                });
                egov.message.show('Bạn chưa nhập nội dung ý kiến!');
                return;
            }
            $.ajax({
                type: "POST",
                data: {
                    documentCopyId: documentCopyId,
                    comment: comment.val(),
                    isToolbar: isToolbar,
                    __RequestVerificationToken: token
                },
                url: '/Document/SendComment',
                beforeSend: function () {
                    egov.message.notification('Đang gửi...', egov.message.messageTypes.processing, false);
                },
                success: function (data) {
                    if (data.error) {
                        egov.message.show(data.error);
                    }
                    else {
                        //Xử lý việc load lại danh sách comment khi gọi chức năng gửi ý kiến từ toolbar(xem chi tiết vb)
                        if (isToolbar && frameDocument) {
                            frameDocument.insertCommentView(data.Data);
                        }
                        egov.message.notification('Gửi ý kiến thành công.', egov.message.messageTypes.success);
                        dialog.close();
                    }
                },
                error: function (xhr) {
                    egov.message.notification(xhr.statusText, egov.message.messageTypes.error);
                }
            });
        },

        addComment: function (content, frame) {

        }
    });

    return CommentView;
});