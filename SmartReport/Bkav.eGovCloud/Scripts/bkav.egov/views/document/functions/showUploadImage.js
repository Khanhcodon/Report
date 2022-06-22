define([
    egov.template.ShowUploadImageHtml,
], function (view) {

    var ShowUploadImage = Backbone.View.extend({

        template: view,

        tagName: "p",

        events: {
        },

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            var that = this;
            that.document = options.document;
            this.render();
            return this;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            this.$el.html(this.template);
            var that = this;
            var dialogSetting = {
                width: "auto",
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Tải ảnh",
                buttons: [
                      {
                          text: egov.resources.common.closeButton,
                          click: function () {
                              that.$el.dialog("hide");
                          }
                      }
                ]
            };

            that.$el.dialog(dialogSetting);
            that.$("#container-upload").append(' <iframe scrolling="no" style="width: 100%; height: 100%; border: none;"  src="/DocumentReport/UploadImage" ></iframe> ');

            return that;
        },
    });

    return ShowUploadImage;
});