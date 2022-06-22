define([
    egov.template.document.warningCompilation,
], function ( Template) {
    // View tạo mới doanh nghiệp
    var WarningCompilationView = Backbone.View.extend({

        template: Template,

        el: "p",

        events: {
        },

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            this.document = options.document;
            this.TimeKey = options.TimeKey;
            this.render();
            this.toggle = false
            this.ValidCompilation = true

            return this;
        },
        events: {
            "click #warningCompilationDetail": "toggleDetail",
            "click .xemluongbaocao": "viewFlow",
            "click .xemchitiet": "viewDetail"
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            var that = this;
            var userCreateId = that.document.model.attributes.UserCreatedId ? that.document.model.attributes.UserCreatedId : 0
            var doctypeId = that.document.model.attributes.DocTypeId
            $.ajax({
                url: "/documentreport/GetWarningCompilation/",
                data: {
                    doctypeId: doctypeId,
                    timekey: that.TimeKey,
                    userCreateId: userCreateId
                },
                dataType: "json",
                type: "Get",
                success: function (data) {
                    if (data) {
                        if (data.error) {
                            return;
                        }
                        var totalReported = _.filter(data, function (item) {
                            return item.reported;
                        });
                        var total = data.length;
                        var tmp = $.tmpl(that.template, { data: data, totalReported: totalReported.length, total: total });
                        that.document.$el.find("#warningTonghop").html(tmp);
                        that.document.$el.find("#warningTonghop").removeClass("hidden");
                        that.document.$el.find("#wrapWarningCompilation").addClass("hidden");

                        that.document.$el.find("#warningCompilationDetail").click(function (e) {
                            that.toggleDetail(e);
                        });

                        if (totalReported.length != total) {
                            that.ValidCompilation = false
                        } else {
                            that.ValidCompilation = true
                        }

                        $(".xemluongbaocao").click(function (e) {
                            that.viewFlow(e);
                        });
                        $(".xemchitiet").click(function (e) {
                            that.viewDetail(e);
                        });
                    }
                },
                error: function () {
                    that.ValidCompilation = true
                }
            });
           
            return this;
        },
        reload: function (timekey) {
            var that = this;
            this.TimeKey = timekey;
            this.render();
        },
        viewFlow: function (e) {
            var that = this;
            var $target = $(e.target).closest(".xemluongbaocao");
            var docId = $target.attr("data-id");
            that.document._viewLeaf(docId)
        },

        viewDetail: function (e) {

        },

        toggleDetail: function (e) {
            var that = this;
            this.toggle = !this.toggle;

            var $target = $(e.target).closest("#warningCompilationDetail");
            if (this.toggle) {
                that.document.$el.find("#wrapWarningCompilation").removeClass("hidden");
                $target.html('<span class="icon icon-download4"></span> <span>Ẩn chi tiết</span>');
            } else {
                that.document.$el.find("#wrapWarningCompilation").addClass("hidden");
                $target.html('<span class="icon icon-download4"></span> <span>Xem chi tiết</span>');
            }
        }
    });

    return WarningCompilationView;
});