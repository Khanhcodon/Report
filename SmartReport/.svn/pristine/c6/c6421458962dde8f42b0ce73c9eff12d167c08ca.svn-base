define([egov.template.businessLicenseView], function (TemplateView) {
    var BusinessLicenseDocView = Backbone.View.extend({

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
                $(".document-business-license").show();
                $(".document-business-license").find("ul").html($.tmpl(that.template, value));

                var $remove = $(".document-business-license").find("ul").find(".license-remove");
                $remove.on("click", function () {
                    that.remove();
                })
                var $info = $(".document-business-license").find("ul").find(".license-info");
                $info.on("click", function () {
                    that.info();
                })
            }
           
            return this;
        },

        info: function () {
            var that = this;
            require(["BusinessLicenseView"], function (returnView) {
                new returnView({ document: that.document });
            })
        },

        remove: function () {
            var value = this.model;
            if (value) {
                var cf = confirm("Bạn có muốn xóa giấy phép này?");
                if (cf) {
                    egov.request.removeBusinessLicense({
                        data: {
                            "businessLicenseId": value.BusinessLicenseId
                        },
                        success: function (result) {
                            $(".document-business-license").find("ul").html("");
                            console.log(result);
                        },
                        error: function (error) {
                        }
                    })
                }
            }
        }
    });

    return BusinessLicenseDocView;
});