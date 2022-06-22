define([
    egov.template.ImportWordView,
], function (ImportViewWord) {
    var ImportWordView = Backbone.View.extend({

        template: ImportViewWord,

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
            that.cid = options.cid;
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
                width: 600,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Cấu hình file word",
                buttons: [
                      {
                          text: "Nhập file",
                          className: "btn-success",
                          click: function () {
                              that.document._importDataFromWord({
                                  $uploadFile: that.$el.find(".upload")
                              })
                              that.$el.dialog("hide");
                          }
                      },
                      {
                          text: egov.resources.common.closeButton,
                          click: function () {
                              that.$el.dialog("hide");
                          }
                      }
                ]
            };

            that.$el.dialog(dialogSetting);

            that.$el.find('input[type="file"]').change(function (e) {
                var fileName = e.target.files[0].name;

                that.$el.find(".FilePath").val(fileName);
            });

            that.$el.find('#btnUpload').click(function (e) {
                that.$el.find('.upload').click();
            });

            return that;
        },
    });

    return ImportWordView;
});