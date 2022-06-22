define([egov.template.invoice.invoiceview], function (TemplateView) {
    var InvoiceShowView = Backbone.View.extend({

        template: TemplateView,

        events: {
            "click .license-remove": "remove"
        },

        initialize: function (options) {
            this.document = options.document;
            this.model = options.model;
            this.render();
            return this;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            var that = this;
            var value = that.model;
            if (value) {
                var $documentInvoice = this.document.$el.find(".document-invoice");;
               $documentInvoice.show();
               $documentInvoice.find("ul").html($.tmpl(that.template, value));

                var $remove = $documentInvoice.find("ul").find(".license-remove");
                $remove.on("click", function () {
                    that.remove();
                })
                var $info = $documentInvoice.find("ul").find(".license-info");
                $info.on("click", function () {
                    that.info();
                })
            }
           
            return this;
        },

        info: function () {
            var that = this;
            egov.request.getDetailInvoice({
                data: {
                    fkey: that.document.model.get('DocCode')
                },
                success: function (result) {
                    new InvoiceViewDetail({data: result.data});
                },
                error: function (error) {
                }
            });
        },

        remove: function () {
            var that = this;
            var value = true;
            if (value) {
                var cf = confirm("Bạn có muốn xóa Biên lai này?");
                if (cf) {
                    egov.request.removeInvoice({
                        data: {
                            "fKey": that.document.model.get('DocCode')
                        },
                        success: function (result) {
                            console.log(result);
                            if (result.data.indexOf("OK") > -1) {
                                that.document.$el.find("#wrapInvoice").hide();
                                egov.pubsub.publish(egov.events.status.success, 'Xoa biên lai thành công');
                            }
                        },
                        error: function (error) {
                        }
                    })
                }
            }
        }
    });

    var InvoiceViewDetail = Backbone.View.extend({
        tagName: "p",

        events: {
            "click .license-remove": "remove"
        },

        initialize: function (options) {
            this.data = options.data;
            this.render();
            return this;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            var that = this;
            this.data = removeTagScript(this.data);
            that.$el.html(this.data);
            var dialogSetting = {
                width: 850,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Chi tiết biên lai",
                buttons: [
                       {
                           text: egov.resources.common.closeButton,
                           click: function () {
                               that.$el.dialog("hide");
                               var document = that.document
                           }
                       },
                ]
            };
            that.$el.dialog(dialogSetting);

            return this;
        },

    });

    function removeTagScript(text) {
        var SCRIPT_REGEX = /<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi;
        while (SCRIPT_REGEX.test(text)) {
            text = text.replace(SCRIPT_REGEX, "");
        }

        return text;
    }

    return InvoiceShowView;
});