define(['jquery', 'backbone'],
function ($, Backbone) {
    "use strict";

    /// <summary>Đối tượng thể hiện một biểu mẫu html</summary>
    var HtmlForm = Backbone.View.extend({

        events: {
        },

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="option">Các tham số.</param>
            var that = this,
                isCreate;
            this.formId = option.formId;
            this.document = option.document;
            this.isView = false;

            isCreate = that.document.isCreate == undefined ? false : that.document.isCreate;

            if (isCreate) {
                egov.request.getFormUrl({
                    data: {
                        formId: that.formId
                    },
                    success: function (result) {
                        if (!result.success) {
                            return;
                        }
                        that.model.ContentUrl = result.formUrl;
                        that.render();
                    },
                    error: function (e) {
                        debugger
                    }
                });
            }
            else {
                if (this.model.Content || this.model.ContentUrl) {
                    this.model.Content = unescape(this.model.Content);
                    return that.render();
                }
            }

            return this;
        },

        render: function () {
            /// <summary>
            /// Hiển thị nội dung biểu mẫu trên form văn bản.
            /// </summary>
            /// <returns type=""></returns>
            var that = this;
            if (this.model.ContentUrl) {
                var $content = $('<iframe class="content-iframe" style="border: 0px; width:100%;" src="' + this.model.ContentUrl + '"></iframe>');
                $content.load(function () {
                    that.urlFormFrame = this.contentWindow;
                    setIframeHeight(this);
                    that.model.Content = unescape(this.contentDocument.body.innerHTML);
                    that.document.forms[that.formId] = that;
                }).appendTo(this.$el);
            }
            else {
                this.$el.append(unescape(this.model.Content));
            }
            if (egov.mobile) {
                egov.mobile.niceScrollIOS(this.document.$(".autoscroll"));
            }
            return this;
        },

        getContent: function (e) {
            /// <summary>
            /// Lấy nội dung form khi chỉnh sửa
            /// </summary>
            /// <returns type=""></returns>
            egov.helper.destroyClickEvent(e);
            this.model.Content = this.$el.find("iframe")[0].contentWindow.$("html").html();
            return this.model.Content;
        }
    });

    function setIframeHeight(iframe) {
        if (iframe) {
            var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
            if (iframeWin.document.body) {
                iframe.height = iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;
            }
        }
    };

    return HtmlForm;
});