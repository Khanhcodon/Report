define([
    egov.template.question.mobileQuestion
],
function (Template) {

    id: "questionList",

    "use strict";

    var QuestionMobile = Backbone.View.extend({

        initialize: function (options) {
            this.parent = options.parent;
            this.render();
        },

        events: {

        },

        render: function () {
            var that = this;
            $.each(this.model.models, function (idx) {
                var questionItem = new QuestionMobileItem({
                    model: this,
                    parent: that,
                });

                if (egov.mobile.isTablet && idx == 0) {
                    questionItem.select();
                }

                that.$el.append(questionItem.$el);
            })
        }
    });

    var QuestionMobileItem = Backbone.View.extend({

        $questionDetail: $("#document-detail"),

        className: "list-group-item two-line questionItem",

        initialize: function (options) {
            this.parent = options.parent;
            this.render();
        },

        events: {
            "click": "select"
        },

        render: function () {
            this.model.set("UserAvatar", egov.mobile.showUserAvatar ? String.format(egov.setting.avatarPath, getUsername(this.model.get("Email"))) : getUserAvatar());
            this.model.set("DateLabel", egov.commonFn.util.getCustomTime(this.model.get("Date")));

            this.$el.append($.tmpl(Template, this.model.toJSON()))
        },

        select: function () {
            var that = this;
            require(['question'], function (Question) {
                var questionId = that.model.get("QuestionId");
                var question = new Question({
                    questionId: questionId,
                    question: that.model,
                    id: "tabquestion_" + questionId
                });
                question.$el.addClass("display");
                egov.mobile.showDetailPage();
                that.$questionDetail.append(question.$el);
            })
            this.addSelect();
        },

        addSelect: function () {
            this.$el.siblings().removeClass("rowSelected");
            this.$el.addClass("rowSelected");
        }
    });

    return QuestionMobile;
});
