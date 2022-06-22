define([
    egov.template.ImportExcelView,
], function (ImportView) {
    var ImportExcelView = Backbone.View.extend({

        template: ImportView,

        tagName: "p",

        events: {
        },

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            var that = this;
            that.document = options.document
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
                title: "Cấu hình file excel",
                buttons: [
                      {
                          text: "Nhập file",
                          className: "btn-success",
                          click: function () {
                              that.document._importDataFromExcel({
                                  $uploadFile: that.$el.find(".upload"),
                                  headerAI: that.$el.find("#startTitle").val() + "," + that.$el.find("#endTitle").val() + ',' + that.$el.find("#startData").val() + ',' + that.$el.find("#endData").val()
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
            that.$el.find('#autoEndData').change(function () {
                if ($(this).is(":checked")) {
                    that.$el.find("#endData").hide(500)
                } else {
                    that.$el.find("#endData").show(500);
                }
            });
            that.$el.find('input[name="rdImportType"]').change(function (e) {
                var selectedValue = that.$el.find('input[name = rdImportType]:checked').val();
                if (selectedValue == 2)
                    $('.classImportTypeMissingNumber').show()
             else{
                    $('.classImportTypeMissingNumber').hide();
                    $('#startTitle').val('');
                    $('#endTitle').val('');
                    $('#startData').val('');
                    $('#endData').val('');
                }
            });

            return that;
        },
    });

    return ImportExcelView;
});