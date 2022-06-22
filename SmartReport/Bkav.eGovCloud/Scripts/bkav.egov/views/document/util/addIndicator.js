define([egov.template.document.addIndicator], function (template) {
    var AddIndicator = Backbone.View.extend({
        tagName: "p",
        initialize: function (options) {
            var that = this;
            that.doc = options.document;
            that.isdepart = options.isdepart
            that.departmentId = options.departmentId;
            if (that.isdepart) {
                that.renderDepartment();
            } else {
                that.renderForm();
            }
        },
        template: template,
        renderForm: function () {
            var that = this;
            that.$el.html($.tmpl(this.template, {}));
            var dialogSetting = {
                width: 600,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Thêm chỉ tiêu",
                buttons: [
                      {
                          text: "Chọn chỉ tiêu",
                          className: "btn-success",
                          click: function () {
                              that.$el.find("#IndicatorFrame").contents().find("#clickCheckIndicator").click();
                              var data = that.$el.find("#IndicatorFrame").contents().find("#dataChecked").val();
                              var selecteds = that.doc.dataFormTable.getSelectedLast();
                              if (!selecteds || selecteds.length < 4) {
                                  return;
                              }
                              data = JSON.parse(data);
                              var rowIndex = selecteds[0];
                              that.doc.dataFormTable.alter("insert_row", rowIndex, data.length);
                              var dataSource = that.doc.dataFormTable.getSourceData();
                              var config = JSON.parse(that.doc.configHandsontable);
                              dataSource = that.doc.renderDataAddIndicator(config, dataSource, data, rowIndex, data.length);
                              that.doc.dataFormTable.loadData(dataSource);
                              that.doc.dataFormTable.render();
                              that.$el.dialog("hide");
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

            that.$el.dialog(dialogSetting);
            that.renderIndicator();
        },

        renderDepartment: function () {
            var that = this;
            that.$el.html($.tmpl(this.template, {}));
            var dialogSetting = {
                width: 800,
                height: "auto",
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Thêm địa bàn",
                buttons: [
                      {
                          text: "Chọn Địa bàn",
                          className: "btn-success",
                          click: function () {
                              that.$el.find("#IndicatorFrame").contents().find("#clickCheckIndicator").click();
                              var data = that.$el.find("#IndicatorFrame").contents().find("#dataChecked").val();
                              var selecteds = that.doc.dataFormTable.getSelectedLast();
                              if (!selecteds || selecteds.length < 4) {
                                  return;
                              }
                              data = JSON.parse(data);
                              var rowIndex = selecteds[0];
                              that.doc.dataFormTable.alter("insert_row", rowIndex, data.length);
                              var dataSource = that.doc.dataFormTable.getSourceData();
                              var config = JSON.parse(that.doc.configHandsontable);
                              dataSource = that.doc.renderDataAddIndicator(config, dataSource, data, rowIndex, data.length, true);
                              that.doc.dataFormTable.loadData(dataSource);
                              that.doc.dataFormTable.render();
                              that.$el.dialog("hide");
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

            that.$el.dialog(dialogSetting);
            that.renderLocality();
        },

        renderIndicator: function () {
            var that = this;
            that.$el.find('#indicatorBodyTree').html('<iframe src="/IndicatorCatalogValueView/index" frameborder="0" scrolling="no" width="100%" height="600" id="IndicatorFrame"></iframe>');
        },

        renderLocality: function () {
            var that = this;
            that.$el.find('#indicatorBodyTree').html('<iframe src="/IndicatorCatalogValueView/localityIndicator" frameborder="0" scrolling="no" width="100%" height="600" id="IndicatorFrame"></iframe>');
        }
    });

    return AddIndicator;
});