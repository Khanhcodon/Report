
define([], function () {

    var DoctypePaper = Backbone.View.extend({
        className: 'doctype-detail-items',

        events: {
            'click .delete-paper': "_deletePaper",
            'click .delete-fee': "_deleteFee",
            'input input.paper-name': '_addPaper',
            'input input.fee-name': '_addFee',
        },

        initialize: function (options) {
            var that = this;
            this.DoctypeId = options.doctypeId;
            this.DoctypeName = options.doctypeName;
            this.Type = options.type;
            this.Papers = [];
            this.Fees = [];
            this.callback = options.callback;

            egov.request.getDoctypePaperAndFees({
                data: { doctypeId: options.doctypeId, type: options.type },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.message);
                    } else {
                        that.Papers = result.papers;
                        that.Fees = result.fees;

                        that.render();
                    }
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, "Có lỗi xảy ra, vui lòng thử lại.");
                }
            });
        },

        render: function () {
            var that = this;
            var data = { DoctypeId: this.DoctypeId, DoctypeName: this.DoctypeName, Type: this.Type, Papers: this.Papers, Fees: this.Fees };
            require([egov.template.doctype.paperFees], function (template) {
                that.$el.append($.tmpl(template, data));
                that.openDialog();
            });
        },

        openDialog: function () {
            var that = this;
            this.$newPaper = this.$(".ul-papers>li:last-child").clone(true);
            this.$newFee = this.$(".ul-fees>li:last-child").clone(true);

            this.$el.dialog({
                width: 800,
                height: 300,
                title: "Quản lý giấy tờ và lệ phí mặc định",
                buttons: [
                    {
                        text: "Cập nhật",
                        className: "btn-primary",
                        click: function () {
                            that.submit();
                        }
                    },
                    {
                        text: "Đóng",
                        className: "btn-default",
                        click: function () {
                            that.$el.dialog("destroy");
                        }
                    }
                ]
            });
        },

        submit: function () {
            var that = this;
            var data = this.serialize();
            egov.request.updateDoctypePaperAndFees({
                data: {
                    doctypeId: that.DoctypeId,
                    type: that.Type,
                    papers: JSON.stringify(data.papers),
                    fees: JSON.stringify(data.fees)
                },
                success: function (result) {
                    that.$el.dialog("destroy");
                    egov.callback(that.callback(result));
                }
            });
        },

        serialize: function () {
            var result = { papers: [], fees: [] };
            var that = this;
            var paperElements = this.$(".ul-papers .doc-paper");
            var feeElements = this.$(".ul-fees .doc-fee");

            paperElements.each(function (e) {
                var paperEl = $(this);
                var newPaper = {};
                var amount = paperEl.find(".paper-amount").val();
                var name = (paperEl.find(".paper-name").is("input") ? paperEl.find(".paper-name").val() : paperEl.find(".paper-name").text());
                if (amount !== "" && amount !== "0" && name !== "") {
                    newPaper.PaperId = paperEl.find(".paper-id").val();
                    newPaper.IsRequired = paperEl.find(".paper-id").is(":checked");
                    newPaper.PaperName = name;
                    newPaper.Amount = paperEl.find(".paper-amount").val();
                    result.papers.push(newPaper);
                }
            });

            feeElements.each(function () {
                var feeEl = $(this);
                var newFee = {};
                var price = feeEl.find(".fee-price").val();
                var name = (feeEl.find(".fee-name").is("input") ? feeEl.find(".fee-name").val() : feeEl.find(".fee-name").text());
                if (price !== "" && price !== "0" && name !== "") {
                    newFee.FeeId = feeEl.find(".fee-id").val();
                    newFee.IsRequired = feeEl.find(".fee-id").is(":checked");
                    newFee.FeeName = name;
                    newFee.Price = price;
                    result.fees.push(newFee);
                }
            });

            return result;
        },

        _deletePaper: function (e) {
            var target = $(e.target).parents(".doc-paper");
            target.remove();
        },

        _deleteFee: function (e) {
            var target = $(e.target).parents(".doc-fee");
            target.remove();
        },

        _addPaper: function (e) {
            var target = this.$(e.target),
                parent = target.parent("li"),
                currentText;

            if (parent.is(":last-child")) {
                this.$(".ul-papers").append(this.$newPaper.clone(true));
                return;
            }

            if (!target.val()) {
                parent.siblings(":last-child").remove();
            }
        },

        _addFee: function (e) {
            var target = this.$(e.target),
                parent = target.parent("li");

            if (parent.is(":last-child")) {
                this.$(".ul-fees").append(this.$newFee.clone(true));
                return;
            }

            if (!target.val()) {
                parent.siblings(":last-child").remove();
            }
        }
    });


    return DoctypePaper;
});
