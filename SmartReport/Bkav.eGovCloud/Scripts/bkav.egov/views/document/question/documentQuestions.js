define([
    egov.template.question.documentQuestions,
],
function (Template) {

    "use strict";

    var DocumentQuestions = Backbone.View.extend({

        className: 'documentQuestions',

        template: Template,

        // Các sự kiện
        events: {
            'click .listQuestion li .basicInfo': "selectQuestion",
            'click .lblshowadvancededit': "showAdvancedEdit",
            'change .chkShowAdvancedEdit': "advancedEdit",
            'click .formanswer .btnAnswer': 'answerQuestion',
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            this.documentId = options.documentId;
            this.document = options.document;
            var that = this;
            return;
            egov.request.getsHolderList({
                data: {
                    documentId: that.documentId
                },
                success: function (result) {
                    that.render(result);
                },
                error: function (e) {
                    debugger
                }
            });
        },

        render: function (listQuestion) {
            var that = this;
            if (listQuestion instanceof Array && listQuestion.length > 0) {
                that.$el.append($.tmpl(that.template, { questions: listQuestion }));
                that.document.$("#divQuestions").html(that.$el).show();
            }
        },

        selectQuestion: function (e) {
            $(e.currentTarget).closest("li").toggleClass("selected");
        },

        showAdvancedEdit: function(e){
            $(e.currentTarget).prev(".chkShowAdvancedEdit").click();
        },

        advancedEdit: function (e) {
            var $target = $(e.currentTarget);
            if ($target.is(":checked")) {
                this._enableEditor($target.closest(".formanswer"));
            }
            else {
                this._disableEditor($target.closest(".formanswer"));
            }
        },

        answerQuestion: function (e) {
            egov.helper.destroyClickEvent(e);
            var $closestLi = $(e.currentTarget).closest("li");
            var answer = "";
            if (this.isShowAdvanceEdit) {
                answer = $closestLi.find(".divAnswer").html();
                if (answer.trim() == "") {
                    $closestLi.find(".divAnswer").focus();
                    return;
                }
            }
            else {
                answer = $closestLi.find(".txtAnswer").val();
                if (answer.trim() == "") {
                    $closestLi.find(".txtAnswer").focus();
                    return;
                }
            }
            egov.request.answerQuestion({
                data: {
                    questionId: $closestLi.data("questionid"),
                    answer: escape(answer),
                    isActive: true
                },
                success: function (result) {
                    $closestLi.remove();
                },
                error: function (err) {
                    console.log(err);
                }
            })
        },

        _disableEditor: function ($target) {
            /// <summary>
            /// Hủy bỏ editor
            /// </summary>
            $target.editor('destroy');
            this.isShowAdvanceEdit = false;
            $target.find('.advanceeditor').hide();
            $target.find('.txtAnswer').show();
        },

        _enableEditor: function ($target) {
            /// <summary>
            /// Hiển thị editor
            /// </summary>
            var $el = $target.find('.advanceeditor'),
                $toolbar = $el.find(".alhToolbar"),
                that = this;
            $el.find(".divAnswer").editor($toolbar, function () {
                that.isShowAdvanceEdit = true;
                $target.find('.txtAnswer').hide();
                $el.show();
                $(".divAnswer").focus();
            });
        },
    });

    return DocumentQuestions;
})