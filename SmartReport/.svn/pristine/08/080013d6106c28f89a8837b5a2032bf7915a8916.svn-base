define([egov.template.document.dateResponseAddresses],
    function (template) {
    
    var DateResponeAddresses = Backbone.View.extend({
        tagName: "div",
        className: "form-horizontal",
        template: template,
        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            var that = this;

            // Hạn xử lý chọn mặc định.
            this.defaultDateResponse = options.dateResponse;

            this.offices = options.listAddress;
            this.success = options.callback;

            this.render();
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            var that = this;
            var model = [];
            _.each(this.offices, function (o) {
                model.push(o.toJSON());
            });

            this.$el.html($.tmpl(this.template, model));
            
            this.$(".datepicker").attr("placeholder", this.defaultDateResponse? this.defaultDateResponse.format("dd/MM/yyyy") : "Chưa đặt hạn" );
            this.$(".datepicker").datepicker();

            // Bind dateReponse đã chọn từ trước
            _.each(this.offices, function (office) {
                if (office.get("isSelected") && office.get("DateResponse")) {
                    that.$el.find("#" + office.get("AddressId")).find(".datepicker").datepicker("setDate", office.get("DateResponse"));
                }
            });

            var dialogSetting = {
                width: 450,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Đặt hạn hồi báo",
                buttons: [
                   {
                       text: "Lưu",
                       className: "btn-success",
                       click: function () {
                           that.save();
                       }
                   },
                   {
                       text: egov.resources.common.closeButton,
                       click: function () {
                           that.$el.dialog("destroy");
                       }
                   },
                ]
            };

            that.$el.dialog(dialogSetting);

            return that;
        },

        save: function () {
            var that = this;
            this.$(".addess-item").each(function (idx, addressEl) {
                var id = $(this).attr("id");
                var date = $(this).find(".datepicker").datepicker("getDate");

                var office = _.find(that.offices, function (o) {
                    return o.get("AddressId") == id;
                });

                if (office && date) {
                    office.set("DateResponse", date);
                    office.set("DateResponseFormat", date.format("dd/MM/yyyy"));
                }
            });

            that.$el.dialog("destroy");
            that.success();
        }
    });

    function getFormattedDate(date) {
        var year = date.getFullYear();

        var month = (1 + date.getMonth()).toString();
        month = month.length > 1 ? month : '0' + month;
        var day = date.getDate().toString();
        day = day.length > 1 ? day : '0' + day;

        return day + '/' + month + '/' + year;
    }

    return DateResponeAddresses;
});