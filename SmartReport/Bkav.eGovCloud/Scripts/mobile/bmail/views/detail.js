define(function () {

    "use strict";

    var BmailViewDetail = Backbone.View.extend({
        el: '#mail-detail',
        allFilePart: "",

        events: {
            "click .attachment-download": "_downloadAttachment"
        },

        initialize: function (options) {
            this.model = new bmail.models.maildetail(options.model);
            this.mailId = options.id;
            this.folderId = options.folderId;
            this.$el.show();
            this.render();
        },

        render: function () {
            var that = this;
            // bmail.current.mailDetail = this;

            egov.mobile.showProcessBar();

            require(["bmailViewToolbar", egov.template.bmail.detail], function (Toolbar, Template) {

                that.$el.html($.tmpl(Template, that.model.toJSON()));

                if (that.mailItem && (that.mailItem.parent.folderPath.indexOf("drafts") == 0 || that.mailItem.isSentBox)) {
                    that.$el.find("#mailSender").hide();
                }

                var toolbar = new Toolbar({
                    mailId: that.model.id,
                    mail: that
                });

                toolbar.$el.appendTo(that.$(".mail-header"));

                that.getMoreData(that.mailId);

                if (that.mailItem) {
                    that.mailItem.addSelected();
                }

                egov.mobile.hideProcessBar();
            });
        },

        getMoreData: function (mailId) {
            var that = this;
            bmail.request.readMail(mailId, function (data) {
                var mail = new MsgUtil(data);

                that.model.set({
                    "content": mail.body,
                    "receivers": mail.getMsgTo(),
                    "receiversLabel": mail.getMsgTo().join("; "),
                    "bcc": mail.getMsgBcc(),
                    "cc": mail.getMsgBcc(),
                    "images": mail.inlineImages,
                    "attachments": mail.attach,
                    "id": mailId
                });

                that.reRender();
            });
        },

        reRender: function () {
            var that = this;
            var $mailBody = this.$el.find("#mailBody");
            $mailBody.html(this.model.get("content"));
            this.$el.find(".mailsenderaccount").html(this.model.get("receiversLabel"));
            if (this.model.get("receivers").length > 2) {
                this.$el.find(".btnshowallaccount").removeClass("hidden");
            }

            this.renderImages();

            this.renderBase64Images();

            this.renderAttachments();

            egov.mobile.hideProcessBar();
        },

        renderImages: function () {
            var that = this;

            var images = this.model.get("images");
            if (images) {
                var bmailServer = new URL(egov.connections.BmailLink);
                var homeService = bmailServer.origin + "/service/home/";
                _.each(images, function (image) {
                    var $image = $("img[src='cid:" + image.ci + "']");
                    var imgUrl = that.makeDownloadUrl({
                        filename: image.filename,
                        part: image.part
                    });

                    $image.attr("src", imgUrl);
                });
            }
        },

        renderBase64Images: function () {
            var images = this.$("img[dfsrc]");
            _.each(images, function (image) {
                var base64String = $(image).attr("dfsrc");
                $(image).removeAttr("dfsrc");
                image.src = base64String;
            })
        },

        showImage: function (e) {
            var base64String = $(e.currentTarget).attr("src");
            window.open(base64String);
        },

        renderAttachments: function () {
            var that = this;
            var attachments = this.model.get("attachments");
            if (!attachments || attachments.length === 0) {
                that.$("#wrapAttachment").hide();
                return;
            }

            var allFilePart = "";
            var totalSize = 0;
            this.$("#mailAttach").removeClass("hidden");
            this.$("#mailAttach #mailAttachDetail").html("");

            require([egov.template.bmail.attachmentItem], function (template) {
                _.each(attachments, function (attachment) {
                    var downloadUrl = '';
                    var linkFile = that.makeDownloadUrl({
                        filename: attachment.fileName,
                        part: attachment.part,
                        disp: "a"
                    });
                    //if (!egov.mobile.iseGovApp) {
                    //    downloadUrl = linkFile;
                    //} else {
                    //    downloadUrl = linkFile + "','" + attachment.fileName;
                    //}

                    attachment.downloadUrl = linkFile;
                });

                that.$("#mailAttachDetail").append($.tmpl(template, attachments));
            });
        },

        _downloadAttachment: function (e) {
            var target = $(e.target).closest(".attachment-download");
            var linkDownload = target.attr("data-url");
            var size = target.attr('data-size');
            var fileName = target.attr('data-name');

            egov.dialog.confirmActions({
                title: fileName,
                message: "Dung lượng file: " + size,
                buttons: [
                    {
                        text: 'Mở',
                        callback: function () {
                            downloadAttachment(linkDownload, fileName, true);
                        }
                    },
                    {
                        text: 'Tải về',
                        callback: function () {
                            downloadAttachment(linkDownload, fileName, false);
                        }
                    }
                ]
            });
        },

        showAllAccount: function (e) {
            $(e.currentTarget).toggleClass("wraptext");
        },

        sendMailTo: function (e) {
            window.open("mailto:" + $(e.currentTarget).text());
        },

        scrollContent: function (e) {
            if ($(e.currentTarget).scrollTop() > 200) {
                $("#btnmailuptotop").addClass("display");
            }
            else {
                $("#btnmailuptotop").removeClass("display");
            }
        },

        makeDownloadUrl: function (param) {
            /// <summary>
            /// Tạo link download từ Server bmail
            /// </summary>
            /// <param name="param">Các tham số:
            /// - id: mailId
            /// - filename: Tên file
            /// - part: filePart
            /// - disp: 
            /// - fmt: đuôi file (để tải toàn bộ)

            /// </param>
            /// <returns type="string"></returns>
            var bmailServer = new URL(egov.connections.BmailLink);
            var homeService = bmailServer.origin + "/service/home/";
            var url = homeService + "~/";
            if (param.filename) {
                url += param.filename;
            }
            url += "?id=" + this.model.id;
            if (param.part) {
                url += "&part=" + param.part;
            }
            if (param.disp) {
                url += "&disp=" + param.disp;
            }
            if (param.fmt) {
                url += "&fmt=" + param.fmt;
            }
            url += "&authToken=" + bmail.authenCookie;

            return url;
        }
    });


    //#region Private Methods

    function downloadAttachment(link, fileName, isOpen) {
        isOpen ? egov.mobile.download(link, fileName) : egov.mobile.onlyDownload(link, fileName);
    };

    //#endregion

    return BmailViewDetail;
});
