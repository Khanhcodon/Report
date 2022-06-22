define([
    egov.template.document.editTemplateMailOrSms],
function (Template) {
    "use strict";

    var tempModel = Backbone.Model.extend({
        defaults: {
            templateId: 0,
            templateName: "",
            isMail: true,
            content: "",
            email: "",
            phone: "",
            userNameRecive: ""
        }
    });

    var EditTempMailOrSmsView = Backbone.View.extend({
        template: Template,

        initialize: function () {
            return this;
        },

        render: function (options) {
            this.model = new tempModel(options.model);
            this.$el.html($.tmpl(this.template, this.model.toJSON()));

            if (this.model.get("isMail")) {
                this._enableEditor();
            }

            this._renderDialog();

            return this;
        },

        _renderDialog: function () {
            var that = this;
            var dialogSetting = {
                title: this.model.get("templateName"),
                width: '750px',
                draggable: true,
                keyboard: true,
                height: "auto",
                buttons: [
                         {
                             id: "sendMailOrSms",
                             text: egov.resources.common.confirmButton,
                             className: 'btn-primary',
                            // disableProcess: true,
                             click: function (callback) {
                                 that._send(callback);
                             }
                         },
                          {
                              id: "closeSend",
                              text: egov.resources.common.closeButton,
                              className: 'btn-close',
                              click: function () {
                                  that.$el.dialog('hide');
                              }
                          }]
            };

            this.$el.dialog(dialogSetting);
        },

        _send: function (callback) {
            var that = this;
            var content = this._getContent();
            if (content == null || content == undefined)
                return;

            var data = {
                content: content
            };
            var requestName;

            if (this.model.get('isMail')) {
                data["email"] = this.$('#email').val();
                if (!validateEmail(data.email)) {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.validateEmail);
                    return;
                }

                data["subject"] = this.model.get('templateName');
                requestName = "sendMailToPeople";
            }
            else {
                data["phone"] = this.$('#phone').val();
                if (!validatePhone(data["phone"])) {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.validatePhone);
                    return;
                }

                requestName = "sendSmsToPeople";
            }
           
            egov.request[requestName]({
                data: data,
                success: function (result) {
                    egov.callback(callback);
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }

                    if (result.success) {
                        egov.pubsub.publish(egov.events.status.success, result.success);
                        that.$el.dialog('hide');
                    }
                },
                error: function (xhr) {
                    egov.pubsub.publish(egov.events.status.error, xhr.message);
                }
            });

        },

        _disableEditor: function () {
            /// <summary>
            /// Hủy bỏ editor
            /// </summary>
            this.$("#content").editor('destroy');
        },

        _enableEditor: function () {
            this.$("#content").editor(this.$("#toolbar"));
        },

        _getContent: function () {
            var content = "";

            if (this.model.get("isMail")) {
                this._disableEditor();
                content = this.$("#content").html();
            } else {
                content = this.$("#content").val();
            }
            return escape(content);
        }
    });

    function validateEmail(email) {
        var pattern = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
        return pattern.test(email);
    }

    function validatePhone(phone) {
        var pattern = /^\+?[0-9]{8,14}$/i;
        return pattern.test(phone);
    }

    return EditTempMailOrSmsView;
});