define([],
    function () {
        "use strict";

        var ContinueProcess = Backbone.View.extend({

            tagName: "div",
            className: "egov-continue",

            initialize: function (options) {
                var that = this;

                that.document = options.document;
                that.render();
                return that;
            },

            render: function () {
                var that = this;
                require([egov.template.document.continueProcess], function (continueProcessTmp) {
                    that.$el.append(continueProcessTmp);
                    that.$el.dialog({
                        title: "Tiếp tục xử lý",
                        width: 550,
                        height: "auto",
                        buttons: [
                            {
                                text: egov.resources.common.updateButton,
                                className: "btn-primary",
                                click: function () {
                                    that.send();
                                }
                            },
                            {
                                text: egov.resources.common.closeButton,
                                click: function () {
                                    that.$el.dialog("hide");
                                }
                            }
                        ]
                    });

                    that.$("#comment").focus();
                    that.$("input.datetime").datepicker({
                        dateFormat: "dd/mm/yy"
                    });

                    var date = Globalize.parseDate(that.document.model.get("DateAppointed"));
                    that.$("input.datetime").datepicker("setDate", date.format("dd/MM/yyyy"));
                    that.time = date.format("THH:mm:ss");
                });
            },

            send: function () {
                $("#error").hide();
                var comment = this.$("#comment").val();
                if (comment == "") {
                    $("#error").show();
                    this.$("#comment").focus();
                    return false;
                }

                var dateAppointed = this.$("input.datetime").val();
                var date;
                if (!String.isNullOrEmpty(dateAppointed)) {
                    date = Globalize.parseDate(dateAppointed + this.time, "dd/MM/yyyyTHH:mm:ss");
                }

                var that = this;
                egov.pubsub.publish(egov.events.status.processing, "Đang xử lý");

                egov.request.continueProcess({
                    data: { documentCopyId: this.document.model.get("DocumentCopyId"), comment: comment, dateAppointed: date.toISOString() },
                    success: function (result) {
                        that.document.closeTabAndReloadTreeNode();
                        egov.pubsub.publish(egov.events.status.success, "Đã cập nhật hồ sơ thành công.");
                    },
                    error: function () {
                        egov.pubsub.publish(egov.events.status.error, "Chức năng hiện không thực hiện được, vui lòng thử lại.");
                    }
                })

                this.$el.dialog("hide");
            }
        });

        return ContinueProcess;
    }
);