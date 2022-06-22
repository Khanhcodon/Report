define(function () {

    "use strict";

    var BmailToolbar = Backbone.View.extend({

        mailId: 0,
        className: "rownavbar custom_nav",

        sender: "",
        subject: "",

        buttons: {
            isShowReply: true,
            isShowReplyAll: true,
            isShowDelete: false,
            isShowDeletePer: false,
            isShowEdit: true,
            isShowForward: true,
            isShowSpam: false,
            isShowUnSpam: false,
            isShowUnread: false,
            isShowRestore: false,
            mailId: 0
        },

        events: {
            //"click #backToList": "backToList",
            "touchend #ReplyBT": "reply",
            "touchend #ReplyMore": "replyAll",
            "touchend #btndelete": "deleteMail",
            "touchend #btndeleteper": "deleteMailPer",
            "touchend #btnedit": "edit",
            "touchend #btnforward": "forward",
            "touchend #btnspam": "markSpam",
            "touchend #btnunspam": "unMarkSpam",
            "touchend #btnunread": "markunread",
            "touchend #btnrestore": "restore",
        },

        initialize: function (options) {
            this.mailId = options.mailId;
            this.mail = options.mail;
            this.folderId = this.mail.folderId;
            this.initData(options.mailId);

            return this.render();
        },

        initData: function () {
            this.buttons.mailId = this.mailId;
            this.sender = this.mail.model.get("sender");
            this.subject = this.mail.model.get("subject");

            return;
            if (bmail.current.folder) {
                this.model = bmail.current.folder.toolbar.model;
                this.buttons.isShowReply = this.model.indexOf("reply") > -1;
                this.buttons.isShowReplyAll = this.model.indexOf("replyall") > -1;
                this.buttons.isShowDelete = this.model.indexOf("delete") > -1;
                this.buttons.isShowDeletePer = this.model.indexOf("deletePer") > -1;
                this.buttons.isShowEdit = this.model.indexOf("edit") > -1;
                this.buttons.isShowForward = this.model.indexOf("forward") > -1;
                this.buttons.isShowSpam = this.model.indexOf("spam") > -1;
                this.buttons.isShowUnSpam = this.model.indexOf("unspam") > -1;
                this.buttons.isShowUnread = this.model.indexOf("unread") > -1;
                this.buttons.isShowRestore = this.model.indexOf("restore") > -1;
            }
        },

        render: function () {
            var that = this;
            require([egov.template.bmail.toolbar], function (Template) {
                that.$el.html($.tmpl(Template, that.buttons));
            });

            return this;
        },

        //backToList: function (e) {
        //    bmail.views.main.showListMail(e);
        //},

        reply: function (e) {
            var data = {
                to: this.sender.fulladdress,
                subject: "Re: " + this.subject,
                sendInfo: String.format("Vào {0} {1} ({2}) đã gửi: ", this.mail.model.get("detailDate"), this.mail.model.get("sender").fullname, this.mail.model.get("sender").fulladdress),
                oldContentMail: this.mail.model.get("content"),
                isReply: true
            }

            this.editOrReply(data);
        },

        replyAll: function (e) {
            var data = {
                to: this.mail.model.get("receivers").join(";").replaceAll("\"", ""),
                subject: "Re: " + this.subject,
                oldContentMail: this.mail.model.get("content"),
                isReply: true,
                folderId: this.folderId
            }
            this.editOrReply(data);
        },

        deleteMail: function (e) {
            var mailId = this.mail.model.get("id");
            egov.pubsub.publish("bmail.moveToTrash", mailId);
        },

        deleteMailPer: function (e) {
            this.mail.deleteMailPer();
        },

        edit: function (e) {
            var data = {
                to: this.mail.model.get("receivers").join(";"),
                subject: this.subject,
                oldContentMail: this.mail.model.get("content"),
                isEdit: true,
                idMailDraft: this.mailId
            }
            this.editOrReply(data);
        },

        forward: function (e) {
            var model = this.mail.model;
            var data = {
                to: "",
                subject: "FW: " + this.subject,
                sendInfo: String.format("Vào {0} {1} ({2}) đã gửi: ", model.get("detailDate"), model.get("sender").fullname, model.get("sender").fulladdress),
                oldContentMail: model.get("content"),
                isForward: true
            }
            this.editOrReply(data);
        },

        markSpam: function (e) {
            bmail.current.mail.markSpam();
        },

        unMarkSpam: function (e) {
            bmail.current.mail.unMarkSpam();
        },

        markunread: function (e) {
            bmail.current.mail.markUnRead();
        },

        restore: function (e) {
            bmail.current.mail.restore();
        },

        editOrReply: function (data) {
            /// <summary>
            /// Tạo mới trang để chỉnh sửa hoặc trả lời mail
            /// </summary>
            /// <param name="data">Object: {to: địa chỉ người gửi, subject: Tiêu đề mail, oldContentMail: Content mail hiện tại}</param>

            var that = this;

            egov.mobile.showDetailPage("compose");

            if (egov && egov.bmailView.ComposeMailView) {
                egov.bmailView.ComposeMailView.render(data);
            } else {
                require(["bmailCreateOrReply"], function (CreateOrReply) {
                    egov.bmailView.ComposeMailView = new CreateOrReply(data);
                });
            }
        }
    });

    return BmailToolbar;
});
