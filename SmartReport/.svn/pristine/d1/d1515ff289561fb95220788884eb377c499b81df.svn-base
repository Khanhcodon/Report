define([], function () {

    "use strict";

    /// <summary> Class hỗ trợ bổ sung</summary>
    var Supplementary = Backbone.View.extend({

        events: {
            'change #ddlSuppType': 'changeType',
            'change #IsUnsuccess': '_insertUnsussessMessage',
            'blur .added': 'changeType',
            'input input.paper-name': '_addPaper',
            'input input.fee-name': '_addFee',
            'input #suppComment': '_changeRequire',
            'click .removeRequired': "_removeCurrentRequired",
            'click .doctypeManager': "_openDoctypePaperAndFeeManager"
        },

        initialize: function (options) {
            this.document = options.document;
            this.success = options.callback;
            this.hasShowPrintPreview = true;

            this.open(options.suppItem, this.success, options.message);
            return this;
        },

        open: function (suppItem, success, message) {
            /// <summary>
            /// Mở form tiếp nhận bổ sung
            /// </summary>
            var that = this;
            that.message = message;

            that.id = suppItem == undefined ? 0 : suppItem.model.get("SupplementaryId");
            that.id |= 0;
            that.suppItem = suppItem;
            that.isReceived = suppItem ? (suppItem.model.get("UserReceivedId") > 0) : false;
            that.suppData = {};

            var request = that.id === 0 ? egov.request.createSupplementary : egov.request.receiveSupplementary;
            var requestData = that.id === 0 ? { docCopyId: that.document.model.get("DocumentCopyId") } : { supplementaryId: that.id };
            request({
                data: requestData,
                success: function (result) {
                    if (result.error) {
                        egov.callback(success);
                        return;
                    }

                    if (result.Supplementary == undefined) {
                        // Thêm mới yêu cầu bổ sung
                        result.Supplementary = {
                            SupplementaryId: 0,
                            SupplementaryDetail: [],
                            OffsetDay: 1,
                            DocumentCopyId: that.document.model.get("DocumentCopyId"),
                            DocumentId: that.document.model.get("DocumentId")
                        }

                        result.Comment = "";
                    }

                    that.hasReceiveSupplementary = result.HasReceiveSupplementary;

                    var template = that.isReceived ? egov.template.supplementary.receivedSupplementary : egov.template.supplementary.receiveSupplement;
                    require([template], function (suppTemplate) {
                        that.template = suppTemplate;

                        that.model = new egov.models.supplementaryList(result.Supplementary == undefined ? [] : result.Supplementary.SupplementaryDetail);
                        
                        that.suppData = result;
                        that.render();
                    });
                }
            });
        },

        render: function () {
            var that = this,
                settings = {},
                data = that.suppData;

            settings.width = 800;
            settings.title = egov.resources.document.supplementary.title;
            settings.draggable = true;
            settings.keyboard = true;
            settings.buttons = [
                {
                    text: egov.resources.document.supplementary.cancelReveiced,
                    className: "btn-warning " + ((that.isReceived || data.Supplementary.UserReceivedId != egov.userId) ? "hidden" : ""),
                    click: function () {
                        that.cancelReceived();
                    }
                },
                {
                    text: egov.resources.document.supplementary.updateAndPrintButton,
                    className: "btn-primary " + ((that.message || that.isReceived || !data.PrintTemplates || data.PrintTemplates.length == 0 || !data.HasReceiveSupplementary) ? "hidden" : ""),
                    click: function () {
                        that.sendAndPrint();
                    }
                },
                {
                    text: egov.resources.document.supplementary.print,
                    className: "btn-primary " + ((!that.isReceived || that.message || !data.PrintTemplates || data.PrintTemplates.length == 0) ? "hidden" : ""),
                    click: function () {
                        that.print();
                    }
                },
                {
                    text: egov.resources.common.updateButton,
                    className: "btn-primary " + (that.isReceived ? "hidden" : ""),
                    click: function () {
                        if (that.hasReceiveSupplementary) {
                            that.send(that.success);
                            return;
                        }

                        that.sendRequire(that.success);
                    }
                },
                {
                    text: egov.resources.common.closeButton,
                    click: function () {
                        that.$el.dialog("hide");
                    }
                }
            ];

            if (!(that.message || !data.PrintTemplates || data.PrintTemplates.length == 0)) {
                settings.confirm = {
                    className: "pull-left",
                    text: egov.resources.common.showPreviewPrint,
                    checked: true,
                    click: function (hasShowPrintPreview) {
                        that.hasShowPrintPreview = hasShowPrintPreview;
                    }
                };
            }

            data.message = that.message;
            data.model = that.model.toJSON();
            data.currentRequire = that.model.detect(function (itm) {
                return itm.get("UserSendId") == egov.userId;
            });

            data.currentRequire = data.currentRequire == undefined ? undefined : data.currentRequire.toJSON();

            that.currentRequire = data.currentRequire;
            that.$el.empty();

            if (data.Comment === undefined) {
                data.Comment = "";
            }

            that.$el.html($.tmpl(that.template, data));

            that.$el.bindResources();
            that.$el.dialog(settings);

            that.$("#ddlSuppType option[value='" + data.Supplementary.SupplementType + "']").attr("selected", "selected");
            that.changeType();
            that.$('#suppComment').focus();

            that.$newPaper = that.$(".ul-papers>li:last-child").clone(true);
            that.$newFee = that.$(".ul-fees>li:last-child").clone(true);
        },

        changeType: function () {
            /// <summary>
            /// Thay đổi cách tính lại ngày hẹn trả
            /// </summary>
            var that = this;
            var suppType = this.$("#ddlSuppType").val();
            if (suppType == egov.enum.supplementaryType.add) {
                this.$('.offset-day').show();
            } else {
                this.$('.offset-day').hide();
            }
        },

        _insertUnsussessMessage: function (e) {
            var isUnsuccessElement = $(e.target).closest("#IsUnsuccess");
            if (isUnsuccessElement.is(":checked") && this.$("#suppComment").val() === "") {
                this.$("#suppComment").val(egov.resources.document.supplementary.noAdditional);
            }
        },

        serialize: function () {
            var result = {};
            var that = this;
            var detailId = 0;
            var comment = "";
            var papers = [];
            var fees = [];

            that.$(".comment-validate").hide();
            detailId = that.$("#currentDetailId").val();

            result.comment = that.$("#suppComment").val();
            result.detailId = detailId;
            result.supplementType = that.$("#ddlSuppType").val();
            result.offsetDay = that.$('.added').val();
            result.isSuccess = !that.$("#IsUnsuccess").is(":checked");

            that.$(".ul-papers > li").each(function (paperElement) {
                var checked = $(this).find(":checked");
                var paperName = $(this).find(".paper-name").is("input")? $(this).find(".paper-name").val() : $(this).find(".paper-name").text();
                if (checked.length > 0 && paperName !== "") {
                    papers.push({
                        DocPaperId: $(this).find(".paper-id").val(),
                        PaperName: paperName,
                        Amount: $(this).find(".paper-amount").val(),
                        Type: egov.enum.feeType.ThuongBosung,
                        IsRequired: true,
                        DocumentId: that.$("[name='DocumentId']").val()
                    });
                }
            });
            that.$(".ul-fees > li").each(function (paperElement) {
                var checked = $(this).find(":checked");
                var feeName = $(this).find(".fee-name").is("input") ? $(this).find(".fee-name").val() : $(this).find(".fee-name").text();
                if (checked.length > 0 && feeName !== "" && $(this).find(".fee-price").val() !== "0") {
                    fees.push({
                        DocFeeId: $(this).find(".fee-id").val(),
                        FeeName: feeName,
                        Price: $(this).find(".fee-price").val(),
                        Type: egov.enum.paperType.ThuongBosung,
                        IsRequired: true,
                        DocumentId: that.$("[name='DocumentId']").val()
                    });
                }
            });

            result.papers = JSON.stringify(papers);
            result.fees = JSON.stringify(fees);

            return result;
        },

        send: function (success) {
            /// <summary>
            /// Tiếp nhận bổ sung
            /// </summary>
            var that = this;
            var model = that.serialize();
            model.suppId = this.id;

            egov.request.supplementaryReceive({
                data: model,
                beforeSend: function () {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.updating);
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }

                    that.suppItem.model.set("UserReceivedId", result.supplementary.UserReceivedId);
                    that.document.changeDateAppointed(result.supplementary.NewDateAppointed);

                    egov.pubsub.publish(egov.events.status.destroy);
                    egov.callback(success);
                },
                complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                    that.$el.dialog("hide");
                }
            });
        },

        sendRequire: function () {
            /// <summary>
            /// Gửi yêu cầu bổ sung
            /// </summary>
            var that = this;
            var model = that.serialize();
            model.supplementaryId = this.id;
            model.docCopyId = this.document.model.get("DocumentCopyId");
            egov.request.sendRequiredSupplementary({
                data: model,
                beforeSend: function () {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.updating);
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }
                    egov.pubsub.publish(egov.events.status.destroy);

                    //Kiểm tra nếu hủy thì xóa ở danh sách yêu cầu bổ sung document
                    if (result.isRemoved) {
                        that.document.removeCurrentRequiredSupplementary(that.suppItem);
                        return;
                    }

                    // Kiểm tra nếu là yêu cầu bổ sung mới thì hiển thị lên danh sách yêu cầu bổ sung của document
                    if (result.supplementary && model.supplementaryId === 0) {
                        that.document.insertReceiveSupplementary(result.supplementary);
                    }
                },
                complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                    that.$el.dialog("hide");
                }
            });
        },

        cancelReceived: function () {
            var that = this;
            egov.request.cancelReceiveSupplementary({
                data: {
                    suppId: this.id
                },
                beforeSend: function () {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.updating);
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.error(egov.events.status.error, result.error);
                        return;
                    }


                    that.suppItem.model.set("UserReceivedId", result.supplementary.UserReceivedId);
                    that.document.changeDateAppointed(result.supplementary.OldDateAppointed);
                },
                complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                    that.$el.dialog("hide");
                }
            });
        },

        sendAndPrint: function () {
            /// <summary>
            /// Cập nhật và in
            /// </summary>
            var that = this;
            var success = function (result) {
                that.print();
            };
            that.send(success);
        },

        print: function () {
            var that = this;
            var templateId = that.$("#ddlTemplate").val();
            var documentCopyId = that.document.model.get("DocumentCopyId");

            require(["print"], function (printView) {
                var printView = new printView({
                    docCopyId: [documentCopyId],
                    docId: that.model.get("DocumentId"),
                    suppId: that.id,
                    printId: templateId,
                    displayPreview: that.hasShowPrintPreview
                });
                printView.open(templateId);
            });

            that.$el.dialog("hide");
        },

        _addPaper: function (e) {
            /// <summary>
            /// Thêm giấy tờ
            /// </summary>
            /// <param name="e"></param>
            var target = this.$(e.target),
                parent = target.parent("li");
            if (parent.is(":last-child")) {
                this.$(".ul-papers").append(this.$newPaper.clone(true));
                return;
            }

            if (!target.val()) {
                parent.siblings(":last-child").remove();
            }
        },

        _addFee: function (e) {
            /// <summary>
            /// Thêm lệ phí
            /// </summary>
            /// <param name="e"></param>
            var target = this.$(e.target),
                parent = target.parent("li");
            if (parent.is(":last-child")) {
                this.$(".ul-fees").append(this.$newFee.clone(true));
                return;
            }

            if (!target.val()) {
                parent.siblings(":last-child").remove();
            }
        },

        _changeRequire: function (e) {
            var target = this.$(e.target).closest("#suppComment");
            var detailId = target.next("input:hidden").val();
            if (detailId == undefined) {
                return;
            }

            this.$(".comment-" + detailId).text(target.val());
        },

        _removeCurrentRequired: function () {
            this.$("#suppComment").empty();
        },

        _openDoctypePaperAndFeeManager: function () {
            var doctypeId = this.document.model.get("DocTypeId");
            var doctypeName = this.document.model.get("DocTypeName");
            var type = 2;
            var that = this;

            var success = function (result) {
                var papers = result.papers;
                var fees = result.fees;
                that.suppData.Papers = JSON.parse(papers);
                that.suppData.Fee = JSON.parse(fees);
                that.suppData.Comment = that.$("#suppComment").val();
                that.render();
            };

            require(["doctypePaperFee"], function (doctypePaperFeeView) {
                var doctypePaperAndFeeView = new doctypePaperFeeView({
                    doctypeId: doctypeId,
                    doctypeName: doctypeName,
                    type: type,
                    callback: success
                });
            });
        }
    });

    return Supplementary;
});