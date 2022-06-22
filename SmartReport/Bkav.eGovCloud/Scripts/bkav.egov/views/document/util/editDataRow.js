define([egov.template.document.addIndicator], function (template) {
    
    var EditDataRow = Backbone.View.extend({
        tagName: "p",
        initialize: function (options) {
            var that = this;
            that.doc = options.document;
            that.isdepart = options.isdepart
            that.departmentId = options.departmentId;
            that.allCatalogValues = [];
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
                width: 800,
                height: 600,
                draggable: true,
                keyboard: true,
                modal: true,
                title: "Sửa dữ liệu",
                buttons: [
                      {
                          text: "Sửa dữ liệu",
                          className: "btn-success",
                          click: function () {
                              var selecteds = that.doc.dataFormTable.getSelectedLast();
                              if (!selecteds || selecteds.length < 4) {
                                  return;
                              }
                              var rowIndex = selecteds[0];
                              //that.doc.dataFormTable.alter("insert_row", rowIndex, data.length);
                              var dataSource = that.doc.dataFormTable.getSourceData();
                              var dataRow = dataSource[rowIndex];
                              if (dataRow) {
                                  var keys = Object.keys(dataRow);
                                  for (var i = 0; i < keys.length; i++) {
                                      if (that.$el.find("#" + keys[i] + "_1").length > 0) {
                                          dataRow[keys[i]] = that.$el.find("#" + keys[i] + "_1").val()
                                      } else {
                                          if (that.textareaIds.indexOf(keys[i]) != -1) {
                                              dataRow[keys[i]] = CKEDITOR.instances[keys[i]].getData();
                                          }
                                      }
                                     
                                  }
                              }
                              that.doc.dataFormTable.loadData(dataSource);
                              that.doc.dataFormTable.render();
                              that.$el.remove();
                              that.$el.dialog("hide");
                          }
                      },
                       {
                           text: egov.resources.common.closeButton,
                           click: function () {
                               that.$el.dialog("hide");
                               that.$el.remove();
                           }
                       },
                ]
            };

            that.$el.dialog(dialogSetting);
            that.renderDataForm();
        },

        renderDataForm: function () {
            var that = this;
            var config = JSON.parse(that.doc.configHandsontable);
            that.config = config.extra.columnSetting;
            var selecteds = that.doc.dataFormTable.getSelectedLast();
            if (!selecteds || selecteds.length < 4) {
                return;
            }
            that.model = that.doc.dataFormTable.getSourceData()[selecteds[0]];
            that.renderData();
        },
        renderTemplate: function () {
            var rowIndex = 1;
            var that = this;
            var tmpl = ''
            var keys = Object.keys(that.config);
            var cellData = '';
            var readOnlyCount = 0;

            // 20200204 VuHQ START tìm trường hợp đặc biệt (2 field đầu readonly)
            for (var i = 0; i < keys.length; i++) {
                if (i == 0 || i == 1) {
                    var item = that.config[keys[i]];
                    if (item.ReadOnly)
                        readOnlyCount++;
                }
            }
            // 20200204 VuHQ END tìm trường hợp đặc biệt (2 field đầu readonly)
            tmpl += '';
            var isInline = false;
            var inlineStr = '';
            var inlineNo = 0;
            var inlineControls = '';
            var textareaId = [];
            for (var i = 0; i < keys.length; i++) {
                tmpl += '<div class="form-group">';

                var item = that.config[keys[i]];
                var fieldNameAscii = keys[i].split("!!")[0];
                var fieldName = keys[i].split("!!")[1];
                cellData = that.model[fieldNameAscii]

                // 20200204 VuHQ START tìm trường hợp đặc biệt (2 field đầu readonly)
                //if ((i == 0 || i == 1) && readOnlyCount == 2) {
                //    tmpl += '<input id="' + fieldNameAscii + '_' + rowIndex + '" type="hidden"  value="' + cellData + '" name="' + fieldNameAscii + '"/>';
                //    if (i == 1)
                //        tmpl += (isCreate ? that.model.attributes[i - 1] : that.model.attributes[keys[i - 1].split("!!")[0]]) + "(" + cellData + ")";
                //    continue;
                //}
                if (item.ReadOnly && item.IsInline) {

                    inlineControls += '<input id="' + fieldNameAscii + '_' + rowIndex + '" type="hidden"  value="' + cellData + '" name="' + fieldNameAscii + '"/>';
                    inlineStr += (!isInline ? "" : " - ") + cellData;
                    isInline = true;

                    continue;
                }
                else {
                    isInline = false;
                    tmpl += '<label for="inline_' + inlineNo + '" style="font-size: 13px; padding-left: 0px !important; padding-top: 20px !important">' + inlineStr + '</label>';
                    tmpl += inlineControls;
                    inlineStr = '';
                    inlineControls = '';
                    inlineNo = 1;
                }

                // 20200204 VuHQ END tìm trường hợp đặc biệt (2 field đầu readonly)

                var strReadOnly = item.ReadOnly ? "readonly" : "";
                var strHidden = item.Hidden ? "hidden" : (item.HiddenMobile ? "hidden" : "");

                if (item.TypeHandson == "checkbox") {
                    if (cellData == "true" || cellData == true) {
                        tmpl += '<label ' + strHidden + ' for="' + fieldNameAscii + '_' + rowIndex + '" style="font-size: 13px; padding-left: 0px !important">' + fieldName + '</label> '
                        tmpl += '<input ' + strReadOnly + ' type="checkbox" class="" data-role="switch" id="' + fieldNameAscii + '_' + rowIndex + '" name="' + fieldNameAscii + '" value="true" checked>';
                    }
                    else
                    {
                        tmpl += '<label ' + strHidden + ' for="' + fieldNameAscii + '_' + rowIndex + '" style="font-size: 13px; padding-left: 0px !important">   ' + fieldName + '</label> '
                        tmpl += '<input ' + strReadOnly + ' type="checkbox" class="" data-role="switch" id="' + fieldNameAscii + '_' + rowIndex + '" name="' + fieldNameAscii + '" value="false">';
                    }
                } else if (item.TypeHandson == "text") {
                    if (item.TypeName == "Kí tự - Kí tự dài") {
                        //textareaId.push(fieldNameAscii + '_' + rowIndex)
                        //old
                        textareaId.push(fieldNameAscii)
                        tmpl += '<label class="mbsc-control-w mbsc-input"' + strHidden + '  for="' + fieldNameAscii + '_' + rowIndex + '">';
                        //tmpl += '<label class="mbsc-control-w mbsc-input  for="' + fieldNameAscii + '_' + rowIndex + '">';
                        tmpl += strReadOnly != "readonly" ? fieldName : cellData;
                        //tmpl += '<textarea id="' + fieldNameAscii + '_' + rowIndex + '" name=""  rows="4" class="form-control"></textarea>'
                        tmpl += '<textarea id="' + fieldNameAscii + '" name="' + fieldNameAscii + '"  rows="4" class="form-control"></textarea>'
                        tmpl += '</label>'
                    } else {
                        tmpl += '<label class="mbsc-control-w mbsc-input"' + strHidden + '  for="' + fieldNameAscii + '_' + rowIndex + '">';
                        tmpl += strReadOnly != "readonly" ? fieldName : cellData;
                        tmpl += '<input id="' + fieldNameAscii + '_' + rowIndex + '" class="form-control" type="' + (strReadOnly != "readonly" ? "text" : "hidden") + '"  value="' + cellData + '" name="' + fieldNameAscii + '"/>\
                               </label>'
                    }
                } else if (item.TypeHandson == "numeric") {
                    tmpl += '<label class="mbsc-control-w mbsc-input" ' + strHidden + '  for="' + fieldNameAscii + '_' + rowIndex + '">';
                    tmpl += strReadOnly != "readonly" ? fieldName : cellData;
                    tmpl += '<input ' + strReadOnly + ' class="form-control" id="' + fieldNameAscii + '_' + rowIndex + '" type="' + (strReadOnly != "readonly" ? "number" : "hidden") + '" step="any" value="' + cellData + '" name="' + fieldNameAscii + '" pattern="' + item.RegEx + '" title="Sai định dạng"/>\
                               </label>';
                } else if (item.TypeHandson == "dropdown") {
                    var formId = that.doc.model.get("DocumentContents")[0].FormId;
                    $.ajaxSetup({ async: false });
                    $.getJSON('/Admin/Form/GetCatalogValues',
                        { catalogName: item.CatalogName, row: i, col: 1, formId: formId, isXoay: false },
                        function (data, textStatus, jqXHR) {
                            var source = JSON.parse(data.catalogValues);
                            var catalogValuesAscii = JSON.parse(data.catalogValuesAscii);
                            if (strReadOnly != "readonly") {
                                tmpl += '<label ' + strHidden + ' for="' + fieldNameAscii + '_' + rowIndex + '">' + fieldName + '<select class="form-control" id="' + fieldNameAscii + '_' + rowIndex + '" name="' + fieldNameAscii + '"class="select-mobiscroll ' + (strReadOnly == "readonly" ? "disabled" : "") + '">';
                                _.each(source, function (sourceDetail, index) {
                                    tmpl += '<option';
                                    tmpl += ' value="' + sourceDetail + '"';
                                    tmpl += (cellData == sourceDetail ? " selected" : "") + '> ' + sourceDetail;
                                    tmpl += '</option>';
                                });
                                tmpl+='</select>'
                                // 20200228 START CatalogName => CatalogKey START
                                var catalogInfos = JSON.parse(data.catalogInfos);
                                jQuery.each(catalogInfos, function (i, val) {
                                    that.allCatalogValues.push(val);
                                });

                                that.allCatalogValues = _.uniq(that.allCatalogValues);
                                // 20200228 START CatalogName => CatalogKey END
                            }

                            else {
                                tmpl += '<label class="mbsc-control-w mbsc-input" ' + strHidden + '  for="' + fieldNameAscii + '_' + rowIndex + '">' + cellData +
                                    '<input class="form-control" id="' + fieldNameAscii + '_' + rowIndex + '" type="hidden"  value="' + cellData + '" name="' + fieldNameAscii + '"/>\
                               </label>'
                            }

                            tmpl += '</select></label>';
                        });
                    $.ajaxSetup({ async: true });
                } else if (item.TypeHandson == "date") {
                    var dateData = new Date(cellData);
                    var dateStr = dateData.getFullYear() + "-" + ("0" + (dateData.getMonth() + 1)).slice(-2) + "-" + ("0" + dateData.getDate()).slice(-2);
                    tmpl += '<label class="mbsc-control-w mbsc-input" ' + strHidden + ' for="' + fieldNameAscii + '_' + rowIndex + '">' + fieldName + ' </label>';
                    //tmpl += strReadOnly != "readonly" ? fieldName : dateStr ;
                    tmpl += '<input class="form-control" ' + strReadOnly + ' type="' + (strReadOnly != "readonly" ? "date" : "hidden") + '" name="' + fieldNameAscii + '" id="' + fieldNameAscii + '_' + rowIndex + '" value="' + dateStr + '" \
                        />';
                } else {

                }

                tmpl += '</div>';

            }

            //rowIndex++;
            tmpl += "";
            that.textareaIds = textareaId;
            console.log(textareaId);
            return tmpl;
        },
        // 20190103 VuHQ END

        renderData: function () {
            var that = this;
            var data = that.model;
            var template = that.renderTemplate();
            $.template("FormContentTemplate", template);
            this.$el.html($.tmpl("FormContentTemplate", data));
            if (that.textareaIds.length) {
                for (var i = 0; i < that.textareaIds.length; i++) {
                    that.renderCkeditor(that.textareaIds[i], data[that.textareaIds[i]])
                }
            }
        },
        renderCkeditor: function (id, explicitTemplate) {
            CKEDITOR.editorConfig = function (config) {
                config.language = 'vi-vn';
                config.uiColor = '#F7B42C';
                config.height = 200;
                config.toolbarCanCollapse = true;
            };
            CKEDITOR.editorConfig = function( config ) {
                config.toolbarGroups = [
                    { name: 'document', groups: [ 'mode', 'document', 'doctools' ] },
                    { name: 'clipboard', groups: [ 'clipboard', 'undo' ] },
                    { name: 'editing', groups: [ 'find', 'selection', 'spellchecker', 'editing' ] },
                    { name: 'forms', groups: ['forms'] },
                    { name: 'styles', groups: [ 'styles' ] },
                    { name: 'colors', groups: [ 'colors' ] },
                    { name: 'tools', groups: [ 'tools' ] },
                    { name: 'others', groups: [ 'others' ] },
                    { name: 'about', groups: [ 'about' ] },
                    '/',
                    { name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
                    { name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi', 'paragraph' ] },
                    { name: 'links', groups: [ 'links' ] },
                    { name: 'insert', groups: [ 'insert' ] }
                ];
            };
            CKEDITOR.replace(id, {
                height: 400,
                allowedContent: true,
                extraPlugins: 'ruler, showborders, highcharts',
                contentsCss: "body {font-size: 16px;}"
            });
           
            CKEDITOR.instances[id].setData(explicitTemplate);
        }
    });
    function uuidv4() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
    return EditDataRow;
});