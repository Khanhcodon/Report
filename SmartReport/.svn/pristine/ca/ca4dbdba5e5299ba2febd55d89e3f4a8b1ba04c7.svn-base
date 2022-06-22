define([
    egov.template.question.forward
],
function (Template) {

    "use strict";

    var QuestionForward = Backbone.View.extend({

        className: 'forwardDialog',

        template: Template,

        // Các sự kiện
        events: {
            'click .listUsers li': "selectUser",
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            this.question = options.question;
            this.questionId = options.questionId;
            var that = this;
            egov.request.getForwardList({
                success: function (result) {
                    that.render(that.template, result);
                },
                error: function (e) {
                    debugger
                }
            })
        },

        render: function (template, listUser) {
            var that = this;

            that.$el.append($.tmpl(template, { users: listUser }));
            var dialogSetting = {
                title: egov.resources.question.transfer,
                width: '750px',
                draggable: true,
                keyboard: true,
                height: "auto"
            };

            dialogSetting.buttons = [
                {
                    id: "sendTransfer",
                    text: egov.resources.buttons.confirm,
                    className: 'btn-primary',
                    disableProcess: true,
                    click: function () {
                        var userId = that.$el.find(".listUsers .selected").data("userid");
                        that.forward(userId);
                    }
                },
                {
                    id: "closeTransfer",
                    text: egov.resources.common.closeButton,
                    className: 'btn-close',
                    click: function () {
                        that.$el.dialog('hide');
                    }
                }
            ];

            this.dialogSetting = dialogSetting;
            that.$el.bindResources();
            that.$el.dialog(dialogSetting);

            that.$el.find(".listUsers li").click(function (e) {
                that.selectUser(e);
            });

            that.$el.find(".listUsers li").dblclick(function (e) {
                that.selectUser(e);
                that.forward($(e.currentTarget).data('userid'));
            });
        },

        selectUser: function (e) {
            var $target = $(e.currentTarget);
            $target.parent().children().removeClass("selected").find(".checkbox").removeClass("checked");
            $target.addClass("selected").find(".checkbox").addClass("checked");
        },

        forward: function (userId) {
            var that = this;
            var comment = this.$(".txtTransferComment").val();
            if (comment.trim() == "") {
                comment = egov.resources.question.defaultTransferComment
            };

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.forwarding);

            egov.request.forwardQuestion({
                data: {
                    questionId: that.questionId,
                    userId: userId,
                    comment: comment
                },
                success: function (e) {
                    $('tr#question-' + that.questionId).remove();
                    that.question.tab.close();
                    that.$el.dialog('hide');
                    egov.pubsub.publish(egov.events.status.success, egov.resources.question.forwardsuccess);

                },
                error: function (e) {
                    debugger
                }
            })
        },
    });

    return QuestionForward;
})