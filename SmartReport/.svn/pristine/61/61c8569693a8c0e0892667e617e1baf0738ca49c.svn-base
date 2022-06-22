define([
    egov.template.renewals
], function (Template) {
    "use strict";

    var _resource = egov.resources.document.renewals;

    /// <summary> Class hỗ trợ gia hạn</summary>
    var RenewalsView = Backbone.View.extend({

        events: {
            "change [name='renewalsType']": "_changeRenewalsType"
        },

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <returns type="">this</returns>
            this.document = option.document;
            this.template = Template;
            this.documentCopyId = this.document.model.get("DocumentCopyId");
            this.render();
            return this;
        },

        render: function () {
            var that = this;
            var dateAppointed = that.document.model.get("DateAppointed");
            var currentDateAppointed = dateAppointed == null? new Date : new Date(that.document.model.get("DateAppointed"));
            
            that.model = {
                dateAppointed: currentDateAppointed.format("dd/MM/yyyy"),
                timeAppointed: currentDateAppointed.toLocaleTimeString()
            };

            var success = function (result) {
                that.model.printTemplates = JSON.parse(result.success);
                that.$el.html($.tmpl(that.template, that.model));
                that.$el.bindResources();
                that.$comment = that.$(".renewalsComment");
                that.$date = that.$(".renewalsDate");
                that.$time = that.$(".renewalsTime");

                that.$comment.focus();
                that.$date.datepicker();
                that.$date.datepicker("setDate", currentDateAppointed);
                that.$time.timepicker({ 'timeFormat': 'H:i:s' });

                that._openDialog();
            };

            egov.request.getPrintTemplates({
                data: { processType: egov.enum.printProcessType.GiaHan },
                success: success
            });
            return this;
        },

        _openDialog: function () {
            /// <summary>
            /// Mở dialog thông báo
            /// </summary>
            var that = this;

            this.$el.attr("help-content-page", "renewals")

            this.$el.dialog({
                title: "Thay đổi hạn xử lý",
                width: "450px",
                height: "auto",
                draggable: true,
                buttons: [
                    {
                        text: _resource.renewalsAndPrintButton,
                        className: 'btn-primary hidden' + (that.model.printTemplates.length === 0 ? "hidden" : ""),
                        click: function () {
                            that._renewalsAndPrint();
                            that.$el.dialog('hide');
                        }
                    },
                    {
                        text: _resource.renewalsButton,
                        className: 'btn-primary',
                        click: function () {
                            that._renewals();
                        }
                    },
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that.$el.dialog('hide');
                        }
                    }
                ]
            });

        },

        _renewals: function (successCallback) {
            var that = this,
                success,
                newDate,
                date,
                renewalsType;
            var oldDateAppointed = that.document.model.get("DateAppointed");
            if (that.$comment.val() === "" && oldDateAppointed && oldDateAppointed != "") {
                egov.pubsub.publish(egov.events.status.error, "Vui lòng nhập lý do thay đổi hạn xử lý.");
                return;
            }

            date = that.$date.val() + "T" + that.$time.val();
            newDate = Globalize.parseDate(date, "dd/MM/yyyyTHH:mm:ss");
            renewalsType = that.$("[name='renewalsType']").val();
            success = function (result) {
                if (result.success) {
                    egov.pubsub.publish(egov.events.status.success, "Thay đổi hạn xử lý thành công.");
                    that.$el.dialog('hide');
                    if (renewalsType == 1) {
                        that.document.changeDateAppointed(result.success);
                    }
                    egov.callback(successCallback);
                }
                else {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.renewals.error);
                }
            };

            egov.request.renewals({
                data: {
                    documentCopyId: that.documentCopyId,
                    comment: that.$comment.val(),
                    newDate: newDate.toISOString(),
                    renewalsType: renewalsType
                },
                success: success,                
            });
        },

        _renewalsAndPrint: function () {
            if (this.model.printTemplates.length === 0) {
                return;
            }
            var templateId = this.$(".ddlPrintTemplate").val();
            var docId = this.document.model.get("DocumentId");
            var that = this;
            this._renewals(function () {
                require(["print"], function (printView) {
                    var printView = new printView({
                        docCopyId: [that.documentCopyId],
                        docId: docId,
                        paperAddIds: "",
                        feeAddIds: "",
                        printId: templateId
                    });
                });
            });
        },

        _changeRenewalsType: function () {
            this.$("#printers").toggle();
        }
    });

    return RenewalsView;
});