define([egov.template.returnResult], function (Template) {

    var _resource = egov.resources.document.returnResult;

    var ReturnResultView = Backbone.View.extend({

        template: Template,

        events: {
            'input input.paper-name': '_addPaper',
            'input input.fee-name': '_addFee',
        },

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            this.document = options.document;
            this.success = options.callback;
            var that = this;
            that.hasShowPrintPreview = true;

            egov.request.getReturnResult({
                data: { documentCopyId: that.document.model.get("DocumentCopyId") },
                success: function (result) {
                    var IsTypeReturn = false;
                    if (that.document.model.get("TypeReturned")!= null) {
                        result["TypeReturned"] = that.document.model.get("TypeReturned");
                        IsTypeReturn = true;
                    }
                    result["IsTypeReturn"] = IsTypeReturn
                    that.model = result;
                    that.render();
                }
            });
            return that;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            this.$el.html($.tmpl(this.template, this.model));
            this.$el.bindResources();
            var that = this;
            var dialogSetting = {
                width: 800,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: _resource.dialogTitle,
                buttons: [
                    {
                        text: _resource.updateAndPrintButton,
                        className: "btn-primary " + (that.model.PrintTemplates.length == 0 ? "hidden" : ""),
                        disabled: that.model.PrintTemplates.length == 0,
                        click: function () {
                            that.$el.dialog("hide");
                            that._updateAndPrint();
                        }
                    },
                    {
                        text: _resource.updateButton,
                        className: "btn-primary",
                        click: function () {
                            that.$el.dialog("hide");
                            that._update();
                        }
                    },
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that.$el.dialog("hide");
                        }
                    },
                ]
            };

            if (that.model.PrintTemplates.length > 0) {
                dialogSetting.confirm = {
                    className: "pull-left",
                    text: egov.resources.common.showPreviewPrint,
                    checked: true,
                    click: function (hasShowPrintPreview) {
                        that.hasShowPrintPreview = hasShowPrintPreview;
                    }
                };
            }

            that.$el.dialog(dialogSetting);
            that.$(".resultComment").focus();

            that.$newPaper = that.$(".ul-papers>li:last-child").clone(true);
            that.$newFee = that.$(".ul-fees>li:last-child").clone(true);

            return that;
        },

        _update: function (success) {
            /// <summary>
            /// Cập nhật trả kết quả
            /// </summary>
            /// <param name="success">Hàm callback sau khi trả kết quả thành công</param>
            var that, documentCopyId, comment, papers, fees, isFinish;

            that = this;
            documentCopyId = that.document.model.get("DocumentCopyId");
            comment = that.$(".resultComment").val();
            isFinish = that.$("#finishProcess").is(":checked");

            papers = [];
            fees = [];
            that.$(".ul-papers > li").each(function (paperElement) {
                var checked = $(this).find(":checked");
                var paperName = checked.val() == 0 ? $(this).find(".paper-name").val() : $(this).find(".paper-name").text();
                if (checked.length > 0 && paperName !== "") {
                    papers.push({
                        PaperName: paperName,
                        Amount: $(this).find(".paper-amount").val(),
                        Type: egov.enum.feeType.TraCongDan,
                        IsRequired: true,
                        DocumentId: that.$("[name='DocumentId']").val()
                    });
                }
            });
            that.$(".ul-fees > li").each(function (paperElement) {
                var checked = $(this).find(":checked");
                var feeName = checked.val() == 0 ? $(this).find(".fee-name").val() : $(this).find(".fee-name").text();
                if (checked.length > 0 && feeName !== "") {
                    fees.push({
                        FeeName: feeName,
                        Price: $(this).find(".fee-price").val(),
                        Type: egov.enum.paperType.TraCongDan,
                        IsRequired: true,
                        DocumentId: that.$("[name='DocumentId']").val()
                    });
                }
            });

            egov.request.updateReturn({
                data: {
                    documentCopyId: documentCopyId,
                    note: comment,
                    isFinish: isFinish,
                    fees: JSON.stringify(fees),
                    papers: JSON.stringify(papers)
                },
                success: function (result) {
                    egov.callback(success, result);
                    egov.callback(that.success, result);
                    that.$el.dialog("hide");
                }
            });
        },

        _updateAndPrint: function () {
            /// <summary>
            /// Trả kết quả và in
            /// </summary>
            var that = this;
            var success = function (result) {
                that.print();
            };

            this._update(success);
        },

        print: function () {
            var that = this;
            var templateId = that.$("#ddlTemplate").val();
            var documentCopyId = that.document.model.get("DocumentCopyId");

            require(["print"], function (printView) {
                var printView = new printView({
                    docCopyId: [documentCopyId],
                    docId: that.document.model.get("DocumentId"),
                    suppId: 0,
                    printId: templateId,
                    displayPreview: that.hasShowPrintPreview
                });
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
            }
        }
    });

    return ReturnResultView;
});