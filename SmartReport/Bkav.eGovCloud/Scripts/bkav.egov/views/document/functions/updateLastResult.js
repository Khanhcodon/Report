define([egov.template.updateLastResult], function (Template) {

    var UpdateLastResultView = Backbone.View.extend({

        template: Template,

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            this.document = options.document;
            this.success = options.callback;
            this.message = options.message;
            this.isNotNeedSign = options.IsNotNeedSign;

            this.render();
            return this;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            var that = this;
            this.$el.html($.tmpl(this.template, { message: this.message, isNotNeedSign: that.isNotNeedSign }));
            this.$el.bindResources();
            var that = this;
            that.$datePublish = that.$(".updateLastResultDatePublish");
            that.$code = that.$(".updateLastResultCode");
            that.$comment = that.$(".updateLastResultComment");
            var dialogSetting = {
                width: 500,
                height: 351,
                draggable: true,
                keyboard: true,
                modal: true,
                title: egov.resources.document.updateLastResult.dialogTitle,
                buttons: [
                    {
                        text: egov.resources.common.updateButton,
                        className: "btn-primary",
                        click: function () {
                            that._update();
                        }
                    },
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that.$el.dialog("hide");
                        }
                    },
                ]
            };
            that.$el.dialog(dialogSetting);
            that.$comment.focus();
            that.$datePublish.datepicker();
        },

        _update: function () {
            var that = this;
            var datePublish = that.$datePublish.val();
            var code = that.$code.val();
            var comment = that.$comment.val();
            if (that.isNotNeedSign) {
                var comment = "Số đến:" + code + '/n Phát hành ngày:' + datePublish + '/n Ý kiến:' + this.$comment.val();
            }
            var kq = $("[name='IsSuccess']:checked").val();
            var result = kq == "true" ? true : false;
            
            egov.pubsub.publish(egov.events.status.processing, "Đang xử lý");

            egov.request.updateLastResult({
                data: {
                    result: result,
                    comment: comment,
                    documentCopyId: that.document.model.get("DocumentCopyId"),
                    isNotNeedSign: that.isNotNeedSign ? true : false
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish("status.error", result.error);
                        return;
                    }
                    that.$el.dialog("hide");
                    egov.callback(that.success);
                }
            });
        }
    });

    return UpdateLastResultView;
});