define([
    'bmailEvent',
    'bmailUtil',
    'bmailMsgUtil',
    'bmailDetail'
],
function () {

    "use strict";

    var CreateOrReply = Backbone.View.extend({

        mailInfo: {},

        isShowInPopup: egov.showPageInPopup,
        isReply: false,
        isCreate: true,

        to: "",
        subject: "",
        content: "",
        idnt: null,
        caret: [],

        $content: null,

        el: "#mail-compose",
        className: "mail-detail createmail autoscroll trans trans-right",

        dataChanged: false, //Biến này để kiểm tra xem nội dung mail có thay đổi hay không? để lưu nháp
        hasRender: false,

        events: {
            "touchend #btnCloseCompose": "_closeCompose",
            "click #sendMail": "sendMail",
            "click #saveDraft": "saveDraft",

            "click #Content": "focusContent",
            "focus #Content": "focusContent",
            "keyup #Content": "focusContent",

            'touchend .viewOldMail': '_showOldMail'
        },

        initialize: function (data) {
            var that = this;
            if (data) {
                this.mail = data.mail;
            }

            this.render(data);
        },

        render: function (data) {
            var that = this;
            if (data.idMailDraft) {
                this.idMailDraft = data.idMailDraft;
            }
            else {
                this.idMailDraft = null;
            }

            this.idnt = null;
            this.irt = null;
            this.isCreate = true;
            this.midPart = [];
            if (!data) {
                data = {
                    to: "",
                    subject: "",
                    oldContentMail: "",
                }
            }

            data.serverUpload = egov.connections.BmailLink + "/service/upload";

            this.isReply = data.isReply == true;
            this.isEdit = data.isEdit == true;
            this.isForward = data.isForward == true;
            this.isSendPF = !this.isForward && egov.bmailView.currentFolder && egov.bmailView.currentFolder.get('isPublicFolder') == true;

            if (this.isReply || this.isEdit || this.isForward) {
                this.$el.parent().children().hide();
                this.isCreate = false;
            }

            this.mail = data.mail;
            setTimeout(
            require([egov.template.bmail.createOrReply], function (Template) {
                that.$el.html($.tmpl(Template, data));
                that.$el.show();

                that.addEventForPage();
                that.bindDataContent(data.oldContentMail, data.sendInfo);

                that.$content = that.$("#Content");
                that.$content.css("min-height", that.$content[0].scrollHeight);

                that.$title = that.$("#mailSubject");
                that.isCreate ? that.$title.focus() : that.$content.focus();
            }.bind(this)), 250);
        },

        sendMail: function (e) {
            /// <summary>
            /// Gửi mail, cần valid thông tin trước khi gửi
            /// </summary>
            /// <param name="e"></param>
            var that = this;
            e.preventDefault();
            if (this.validForm()) {
                var attachPart = null;

                egov.mobile.showProcessBar();

                var msg = this.createMailData(this.to);
                if ($("#mailAttachDetail").children().length > 0) {
                    attachPart = [];
                    _.each($("#mailAttachDetail").children(), function (item) {
                        attachPart.push($(item).attr("midpart"));
                    });
                }

                var success = function (code) {
                    if (code == 0) {
                        if (that.idMailDraft) {
                            bmail.request.msgAction("delete", that.idMailDraft, function () { });
                        }
                        //Gửi mail thành công

                        egov.mobile.showStatus("Gửi mail thành công.");
                        that._closeCompose();
                        // egov.mobile.hideDetailPage();
                        egov.pubsub.publish("maillist.show", { fuck: "doannx" });
                        egov.pubsub.publish("maillist.refresh");
                    }
                    else {
                        egov.mobile.showStatus("Gửi mail lỗi.");
                    }

                    egov.mobile.hideProcessBar();
                };

                this.isSendPF
                        ? bmail.request.sendPFMail(msg, this.idMailDraft, egov.bmailView.currentFolder.get("id"), attachPart, success)
                        : bmail.request.sendMail(msg, this.idMailDraft, attachPart, success);
            }
        },

        saveDraft: function (e, ignoreComfirm, attachId) {
            /// <summary>
            /// Có thay đổi thì lưu nháp
            /// </summary>
            /// <param name="e"></param>
            /// <param name="ignoreComfirm">Bỏ qua kiểm tra văn bản có thay đổi hay không</param>
            /// <param name="attachId">attachId</param>
            var that = this;
            var cookie_user = bmail.userCookie;
            var authen_cookie = bmail.authenCookie;

            if (this.hasChange() || ignoreComfirm) {
                var hasAttach = attachId != undefined, subject = "", content = "", emailTo = "";

                emailTo = "<e t='f' a='" + cookie_user + "' p='" + cookie_user + "' /><e t='t' a='" + this.to + "'/>";
                subject = Util.getSubject(this.subject);
                content = this.content.replace(/</gi, "&lt;");
                content = content.replace(/>/gi, "&gt;");
                if (content.indexOf('&nbsp') != -1) {
                    content = content.replace(/&nbsp;/gi, '&#160;');
                }
                content = "<mp ct='text/html'><content>" + content + "</content></mp>";

                var response = Util.saveDraft(cookie_user, authen_cookie, emailTo, subject, content, attachId, that.idMailDraft, that.idnt, that.irt, that.midPart, hasAttach);
                if (response.Body.SaveDraftResponse && response.Body.SaveDraftResponse.m && response.Body.SaveDraftResponse.m[0]) {
                    if (hasAttach) {
                        var that = this;
                        if (response.Body.SaveDraftResponse.m[0].mp
                            && response.Body.SaveDraftResponse.m[0].mp[0].mp) {
                            var saveDaftMessage = response.Body.SaveDraftResponse.m[0];
                            var mid = saveDaftMessage.id;
                            that.idMailDraft = mid;

                            if (saveDaftMessage.idnt) {
                                that.idnt = saveDaftMessage.idnt;
                            }
                            if (saveDaftMessage.mid) {
                                that.irt = saveDaftMessage.mid;
                                if (that.irt != "") {
                                    that.irt = that.irt.substring(1, that.irt.length - 1);
                                }
                            }
                            var l = saveDaftMessage.mp[0].mp.length;
                            if (l > 0) {
                                that.$("#mailAttach").removeClass("hidden");
                                that.$("#mailAttach #mailAttachDetail").html("");
                                that.midPart = [];
                            }
                            require([egov.template.bmail.attachmentItem], function (template) {
                                for (var i = 0; i < l; i++) {
                                    if (saveDaftMessage.mp[0].mp[i].cd
                                        && saveDaftMessage.mp[0].mp[i].cd == "attachment") {
                                        var attachPart = saveDaftMessage.mp[0].mp[i].part;
                                        that.midPart.push(attachPart);
                                        var attachModel = new bmail.models.attachment({
                                            part: attachPart,
                                            fileName: saveDaftMessage.mp[0].mp[i].filename,
                                            isCreate: true
                                        });
                                        var attachItem = new AttachmentItem({
                                            mail: that,
                                            model: attachModel,
                                            template: template
                                        });

                                        that.$("#mailAttachDetail").append(attachItem.$el);
                                    }
                                }
                            });
                        }
                        else if (response.Body && response.Body.Fault && response.Body.Fault.Detail && response.Body.Fault.Detail.Error && response.Body.Fault.Detail.Error.Code == "mail.MESSAGE_TOO_BIG") {
                            egov.mobile.showStatus(bmail.resources.detail.file.toolarge);
                        }
                        else {
                            egov.mobile.showStatus(bmail.resources.detail.file.tryagain);
                        }
                    }
                    else {
                        egov.mobile.showStatus(bmail.resources.savedraft.success);
                    }
                } else {
                    egov.mobile.showStatus(bmail.resources.savedraft.error);
                }
            }
        },

        hasChange: function () {
            return this.dataChanged;
        },

        validForm: function () {
            this.setDataInfo();

            if (this.to == "") {
                this.$("#mailTo").focus();
                return false;
            }
            else if (this.subject == "") {
                this.$("#mailSubject").focus();
                return false;
            }
            else if (this.content == "") {
                this.$("#Content").focus();
                return false;
            }

            return true;
        },

        bindDataContent: function (oldContent, sendInfo) {
            if (this.isReply || this.isForward) {
                this.$(".oldSenderInfo").html(sendInfo);
                this.$(".oldMailContent").html(oldContent);
            }

            this.$("#mailAttach").addClass("hidden");
            this.$("#mailAttach #mailAttachDetail").html("");

            if (bmail.user && bmail.user.signature.useSignature) {
                this.$("#signal").html(bmail.user.signature.content);
            }

            this.$el.addClass("display");
        },

        addEventForPage: function () {
            var that = this,
                progressImgUrl = "/Scripts/mobile/bmail/emoticon/progress.gif";

            PageEvents.AddressInputKeyEvent("#mailTo", "keyup", "#suggestions-To", "#To");

            //#region Insert File
            this.$('#uploadFileForm').fileupload({
                xhrFields: {
                    withCredentials: true
                },
                //forceIframeTransport: true,
                progressall: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    console.log("uploaded: " + progress + "%");
                },
                done: function (e, data) {
                    var textResponse = $(data.result).text();
                    var result = textResponse.replace("function doit() { window.parent._uploadManager.loaded(200,'null','", "").replace("'); }", "");
                    var attachId = [];
                    var ArrayId = result.split(",");
                    var lengofArrayId = ArrayId.length;
                    for (var i = 0; i < lengofArrayId; i++) {
                        attachId.push(ArrayId[i]);
                    }

                    that.dataChanged = true;

                    // that.saveDraft(undefined, true, attachId);
                },
                error: function (e, data) {
                    console.error(data);
                }
            });

            //#endregion
        },

        focusContent: function (e) {
            if (!e) {
                return;
            }

            this.caret.push(getCaretPosition(e.target));
        },

        setDataInfo: function () {
            this.to = this.$("#mailTo").val().trim();
            this.subject = this.$("#mailSubject").text().trim();

            this.content = this.$("#Content").html().trim();
            if (this.isReply || this.isForward) {
                this.content += this.$("#oldMailContent").html();
            }
        },

        createMailData: function (to) {
            /// <summary>
            /// Tạo mail để gửi
            /// </summary>

            var msg = new Array();

            var mps = new Array();

            var html = this.content;

            var textplain = html.replace(/<br>/, '\n').replace(/<style([\s\S]*?)<\/style>/gi, ' ').replace(/<script([\s\S]*?)<\/script>/gi, ' ')
            .replace(/\s+/gm, ' ');

            mps.push({
                ct: "text/plain",
                content: textplain
            }, {
                ct: "text/html",
                content: html
            });
            var mpart = new Array();
            mpart.push({
                ct: "multipart/alternative",
                mp: mps
            });
            var toAddress = to;
            toAddress = toAddress.replace(/;$/, "");

            var newMail = {
                from: bmail.user.email,
                from_displayName: bmail.user.displayName,
                to: toAddress,
                cc: $("#mailCc").val(),
                bcc: $("#mailBcc").val(),
                subject: Util.getSubject(this.subject),
                mp: mpart
            };

            msg.push(newMail);

            return msg;
        },

        _showOldMail: function () {
            this.$('.viewOldMail').hide();
            this.$('#oldMailContent').show();
        },

        _closeCompose: function (e) {
            this.isCreate
                    ? egov.pubsub.publish("maillist.show", { fuck: "doannx" })
                    : egov.mobile.showDetailPage("bmail");

            this.$el.empty();

            e && e.preventDefault();
        }
    });

    return CreateOrReply;
});
