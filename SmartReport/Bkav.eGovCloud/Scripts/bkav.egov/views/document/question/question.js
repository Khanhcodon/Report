define([
    egov.isMobile ? egov.template.question.mobileDetail : egov.template.question.detail
],
function (Template) {

    "use strict";

    var Question = Backbone.View.extend({

        className: "tabquestion",

        template: Template,

        isGeneral: false,

        document: null,

        // Các sự kiện
        events: {
            'click #btnAnswer': 'answerQuestion',
            'click #btnForward': 'forwardQuestion',
            'click #btnReject': 'rejectQuestion',
            'click #btnRejectAnswer': "rejectAnswer",
            'click #btnViewDocumentDetail': "openDocument",
            "click .divUserComment .title": "toggleCommentList",
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            this.questionId = options.questionId;
            this.model = options.question;
            this.tab = options.tab;

            this.initData();
            this.render();
        },

        initData: function () {
            /// <summary>
            /// 
            /// </summary>
            this.model.set("IsAdminHolder", parent.egov.isAdmin);
            this.isGeneral = this.model.get("IsGeneralQuestion");
            this.document = this.model.get("Document");
            var answerHolder = this.model.get("AnswerHolder");
            if (answerHolder) {
                this.model.set("isMe", answerHolder.UserId == egov.setting.userId);
            }
            var commentUsers = this.model.get("UserComments");
            _.each(commentUsers, function (commentUser) {
                commentUser.Date = (new Date(commentUser.CommentDate)).format("hh:mm dd/MM/yyyy");
            });

        },

        render: function () {
            /// <summary>
            /// 
            /// </summary>

            var that = this;
            if (egov.mobile) {
                this.model.set("UserAvatar", egov.mobile.showUserAvatar ? String.format(egov.setting.avatarPath, getUsername(this.model.get("Email"))) : getUserAvatar());
                this.model.set("DateLabel", egov.commonFn.util.getDetailDate(this.model.get("Date"), "hh:mm dd/MM/yyyy"));
            }
            that.$el.html($.tmpl(Template, that.model.toJSON()));
            if (egov.isMobile) {
                that.$(".mdl-button").materialButton();
                var $questionDetail = that.$(".comment-detail");
                $questionDetail.doubleTap(function () {
                    $questionDetail.toggleClass("collapseView");
                })
            }
            else {
                this.enableEditor();
            }
            return this;
        },

        answerQuestion: function () {
            var $answer = this.$el.find("#txtAnswer");
            var answer = ""
            if (egov.isMobile) { $answer.val() } else {
                answer = this.$el.find(".editor-iframe").eq(0).contents().find("body").html()
            };
            if (answer.trim() == "") {
                $answer.focus();
                return;
            }
            var that = this;
            var questionId = this.questionId;

            egov.request.answerQuestion({
                data: {
                    questionId: questionId,
                    answer: escape(answer),
                    isActive: true
                },
                success: function (e) {
                    if (egov.isMobile) {
                        egov.mobile.hideDetailPage();
                        egov.views.home.tree.getNewDocuments();
                        that.$el.remove();
                    }
                    else {
                        $('tr#question-' + questionId).remove();
                        that.tab.close();
                    }
                },
                error: function (e) {
                    debugger
                }
            })
        },

        rejectQuestion: function (comment, callback) {
            var that = this;

            egov.message.prompt(egov.resources.question.reject, egov.resources.question.rejectConfirm, function (comment) {
                if (comment === '') {
                    // Chưa nhập ý kiến bắt nhập lại
                    that.rejectQuestion(egov.resources.question.rejectConfirm);
                    return;
                }

                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

                var questionId = that.questionId;

                egov.request.rejectQuestion({
                    data: {
                        questionId: questionId,
                        comment: comment
                    },
                    success: function (e) {
                        $('tr#question-' + questionId).remove();
                        that.tab.close();
                        egov.pubsub.publish(egov.events.status.destroy);
                    },
                    error: function (e) {
                        debugger
                    }
                })
            });
        },

        rejectAnswer: function () {
            var that = this;

            egov.message.prompt(egov.resources.question.reject, egov.resources.question.rejectConfirm, function (comment) {
                if (comment === '') {
                    // Chưa nhập ý kiến bắt nhập lại
                    that.rejectQuestion(egov.resources.question.rejectConfirm);
                    return;
                }

                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

                var questionId = that.questionId;

                egov.request.rejectAnswer({
                    data: {
                        questionId: questionId,
                        comment: comment
                    },
                    success: function (result) {
                        $('tr#question-' + questionId).remove();
                        that.tab.close();
                        egov.pubsub.publish(egov.events.status.destroy);
                    },
                    error: function (e) {
                        debugger
                    }
                })
            });
        },

        forwardQuestion: function () {
            var that = this;

            require(['questionForward'], function (QuestionForward) {
                var forward = new QuestionForward({
                    questionId: that.questionId,
                    question: that
                });
                that.$el.append(forward.$el);
            })
        },

        disableEditor: function () {
            /// <summary>
            /// Hủy bỏ editor
            /// </summary>
            this.$el.editor('destroy');
        },

        enableEditor: function () {
            /// <summary>
            /// Hiển thị editor
            /// </summary>
            var $el = this.$el,
                $toolbar = this.$el.find(".alhToolbar"),
                that = this;
            this.$el.find(".txtAnswer").editor($toolbar, function () {
                $(".txtAnswer").focus();
            });
            this.model.IsEdited = true;
        },

        openDocument: function (e) {
            /// <summary>
            /// Mở chi tiết hồ sơ
            /// </summary>
            var that = this;
            if (this.isGeneral) {
                return;
            }

        },

        toggleCommentList: function () {
            this.$(".divUserComment").toggleClass("open");
        },

    });

    return Question;
})