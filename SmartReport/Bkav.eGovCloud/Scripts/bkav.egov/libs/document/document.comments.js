/*
* egovcloud.document.comments.js
*
* Copyright 2012: Bkav - Bso - Phòng 2 - eGov Core
* Created by: TienBV@bkav.com
* Edited by: 
*
* Mô tả về lớp egovcloud.document.comments.js
* Xử lý hiển thị các ý kiến xử lý
*
* Required:
*   - jQuery >= 1.4.
*   - jQuery.tmpl.min.js
*/

(function ($) {
    window.Comments = function (json) {
        this.json = json;
        this.comments = {};
        this.insertTemplate = "#CommentInsertTemplate";
        this.templateId = "#CommentTemplate";
        this.processCommentContainer = ".comments";
        this.coProcessCommentContainer = '.process-status'; //".process-info";
        this.commentType = { Common: 1, Consulted: 2, Contribution: 3, Supplementary: 4, Signed: 5, Success: 6 };
    };

    /// <summary>Xử lý chuỗi json trả về từ server</summary>
    window.Comments.prototype.init = function () {
        var result = { CoProcessor: [], Processor: [] };
        var commentObj = JSON.parse(this.json);
        var commentTypeEnum = this.commentType;
        for (var i = 0; i < commentObj.length; i++) {
            var comment = commentObj[i];
            if (comment.CommentType == commentTypeEnum.Common) {
                content = JSON.parse(comment.Content);
                comment.Content = content;
                result.Processor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.Consulted) {
                content = JSON.parse(comment.Content);
                comment.Content = content;
                comment.Content.Transfers = [];
                result.CoProcessor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.Contribution) { // && comment.UserSendId == comment.UserReceiveId 
                comment.Content = JSON.parse(comment.Content);
                comment.Content.Transfers = [];
                result.CoProcessor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.Supplementary) {
                //result.CoProcessor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.Signed) {
                //result.CoProcessor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.Success) {
                //.CoProcessor = comment;
            }
        }
        this.comments = result;
    };

    /// <summary>Hiển thị các ý kiến xử lý</summary>
    window.Comments.prototype.show = function () {
        this.init();
        $(this.templateId).tmpl(this.comments.Processor).appendTo(this.processCommentContainer);
        if (this.comments.CoProcessor.length > 0) {
            $(this.templateId).tmpl(this.comments.CoProcessor).appendTo(this.coProcessCommentContainer);
            $(this.coProcessCommentContainer).find(".coProcess-count").text(this.comments.CoProcessor.length);
            $(this.coProcessCommentContainer).show();
        }
        else {
            $(this.coProcessCommentContainer).hide();
        }
        bindCommentEvent();
        $(this.processCommentContainer).find(".comment .comment-text").first().addClass("comment-open");
    };

    ///<summary>Thêm ý kiến xử lý vào danh sách</summary>
    window.Comments.prototype.insert = function (comment) {
        this.json = comment;
        this.init();
        $(this.processCommentContainer + " .main-title").after($(this.templateId).tmpl(this.comments.Processor));
    };

    function bindCommentEvent() {
        $(".comment").each(function () {
            var comment = $(this).find(".comment-text");
            $(comment).dotdotdot({ height: 30 });
            $(this).click(function () {
                $(comment).toggleClass("comment-open");
                if (!$(comment).hasClass("comment-open")) {
                    $(comment).dotdotdot({ height: 30 });
                }
                else {
                    $(comment).trigger("destroy");
                }
            });
        });

        $('.comments .main-title').click(function () {
            var firstContent = $(".comment-text").first();
            if (!firstContent.hasClass("comment-open")) {
                $(".comment-text").addClass("comment-open");
            }
            else {
                $(".comment-text").removeClass("comment-open");
            }
            return false;
        });
    }

})(window.jQuery)