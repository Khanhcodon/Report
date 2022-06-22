/*
    Bkavegov.report.js
    author: TienBV@bkav.com
    Date: 14/06/2013
    Description: thư viện xử lý soạn mẫu báo cáo.
    Requirement:
        - jQuery 1.7 or later.
        - Jquery ui 1.8.2 or later (draggable, droppable, resizable).
        - jQuery layout.
        - jQuery tmpl.
        - jQuery colorpicker
        - underscore.min.js
        - bkav.egov.report.config.js
        - bkav.egov.report.template.js
        - bkav.egov.report.field.js
        - bkav.egov.report.panel.js
        - bkav.egov.report.hotkey.js
    Browser: 
        - Firefox (Gecko): 11
        - Chrome: 7
        - Internet Explorer 10
        - Opera 12.02
        - Safari 6.0.2
*/

(function ($, _) {
    var eReport = function (panelId, json, isReport) {
        this.content = json;
        this.panelId = panelId;
        this.config = eReport.config;
        this.template = eReport.template;
        this.field = eReport.field;
        this.isReport = isReport;
    };

    eReport.prototype.init = function () {
        this.panel = new eReport.panel(this.panelId, this.content, this.isReport);
        this.panel.init(this.reportId);
    };

    /// <summary>Trả về nội báo cáo theo dạng json</summary>
    eReport.prototype.get = function () {
        var config = this.config;
        _disableEffect(config);
        _fixTable(config);
        var result = {};
        result.Fields = [];
        $(config.toolbox.dataField_class).find(config.toolbox.field_class).each(function () {
            var itm = {
                Name: $(this).text(),
                Key: $(this).attr("value"),
                Type: 1,
                DataType: $(this).attr("datatype")
            };
            if (itm.DataType == '') {
                itm.DataType = 'string';
            }
            result.Fields.push(itm);
        });

        $(config.toolbox.commonField_class).find(config.toolbox.field_class).each(function () {
            var itm = { Name: $(this).text(), Key: $(this).attr("value"), Type: 2, DataType: $(this).attr("datatype") };
            if (itm.DataType == '') {
                itm.DataType = 'string';
            }
            result.Fields.push(itm);
        });

        $(config.toolbox.groupField_class).find(config.toolbox.field_class).each(function () {
            var itm = { Name: $(this).text(), Key: $(this).attr("value"), Type: 3, DataType: $(this).attr("datatype") };
            if (itm.DataType == '') {
                itm.DataType = 'string';
            }
            result.Fields.push(itm);
        });

        result.UsedFields = _getUsedFields(result.Fields, config);

        result.Summaries = _getSummaryFields(config);

        _insertWordStyleToTable(config);

        result.pageSettup = {
            pageSize: function () {
                var pageSize = $(config.optionPanel.changePageSizeClass).val();
                var pageOrientation = $(config.optionPanel.changePageOrientationClass + ":checked").val();
                var size = pageSize + "-" + pageOrientation + "-size";
                return size;
            }
        };

        result.Content = {};
        result.Content.header = $(config.contentPanel.header_class).find(config.contentPanel.content_class)[0].outerHTML;
        result.Content.footer = $(config.contentPanel.footer_class).find(config.contentPanel.content_class)[0].outerHTML;
        result.Content.detail = $(config.contentPanel.detail_class).find(config.contentPanel.content_class)[0].outerHTML;
        result.Content.gHeader = $(config.contentPanel.groupHeader_class).find(config.contentPanel.content_class)[0].outerHTML;
        result.Content.gFooter = $(config.contentPanel.groupFooter_class).find(config.contentPanel.content_class)[0].outerHTML;

        return result;
    };

    //#region Private Methods

    /// <summary>Remove các hiệu ứng kéo thả, drop, drag</summary>
    var _disableEffect = function (config) {
        $(".ui-droppable, .ui-resizable, .ui-draggable")
        .resizable("destroy")
        .droppable("destroy")
        .draggable("destroy");
        $(config.contentPanel.textSelected_class).removeClass(config.contentPanel.textSelected_className);
        $(config.contentPanel.cellSelected_class).removeClass(config.contentPanel.cellSelected_className);
    };

    /// <summary>Chuẩn lại kích thước thực của các cell của table. Do trong quá trình resize tổng width của các cell vượt quá kích thước của table</summary>
    var _fixTable = function (config) {
        $("td, th").each(function () {
            $(this).width(this.clientWidth); // clientWidth là width nhìn thấy trên giao diện, width là width được set strong style
        });
        $(config.table.header_class).css("top", $(config.table.header_class).parent().height() - $(config.table.header_class).height());
    };

    /// <summary>Trả về danh sách các trường dữ liệu đang được sử dụng trong form</summary>
    var _getUsedFields = function (fields, config) {
        var result = [];
        $(".used-field").each(function () {
            var key = $(this).text();
            if ($(this).attr("type") === "data") {
                var field = _.find(fields, function (itm) {
                    return "[" + itm.Key + "]" == key;
                });
                if (field != undefined) {
                    var itm = {
                        Name: field.Name,
                        Key: key,
                        Type: field.Type,
                        Datatype: $(this).attr("datatype"),
                        Format: $(this).attr("format"),
                        Style: _getStyle($(this))
                    };
                    if (itm.DataType == '') {
                        itm.DataType = 'string';
                    }
                    result.push(itm);
                }
            }
            else if ($(this).attr("type") === "image") {
                var itm = {
                    Name: $(this).find("img").attr("src").replace('data:image/jpeg;base64,', ''),
                    Key: $(this).html(),
                    Type: 5,
                    Datatype: "string",
                    Style: _getStyle($(this))
                };
                result.push(itm);
            }
            else {
                if ($(this).parents("table").length == 0) {
                    _formatCssUnit($(this), config.unit);
                    var itm = {
                        Name: key,
                        Key: key,
                        Type: 4,
                        Datatype: "string",
                        Style: _getStyle($(this))
                    };
                    result.push(itm);
                }
            }
        });
        return result;
    };

    var _getStyle = function (obj) {
        var result = {};
        result.Top = obj.position().top * 0.75;
        result.Left = obj.position().left * 0.75;
        result.Width = obj.width() * 0.75;
        result.Height = obj.height() * 0.75;
        result.FullStyle = obj.attr('style');
        return result;
    };

    var _formatCssUnit = function (obj, unit) {
        var css = ["font-size", "left", "top", "width", "height"];
        css.forEach(function (itm) {
            var value = obj.css(itm);
            if (value.indexOf("px") >= 0) {
                var valueInPt = parseFloat(value) * 0.75; // 0.75 = 72/96 (72 là số pt trong 1 inch, 96 là số px trong 1 inch)
                obj.css(itm, valueInPt + unit);
            }
        });
    };

    var _insertWordStyleToTable = function (config) {
        var table = $(config.table.header_class);
        if (table != undefined && table.length != 0) {
            var top = table.position().top * 0.75 + "pt";
            var left = table.position().left * 0.75 + "pt";
            var style = $(table).attr("style");
            style += "; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:" + left + "; mso-table-top:" + top;
            $(table).attr("style", style);
        }
    };

    var _getSummaryFields = function (config) {
        var result = [];
        $("[type='summary']").each(function () {
            var sumRow = $(this).parents("tr");
            var sumType = 3; // tính toán dữ liệu trên toàn bộ báo cáo
            if (sumRow.hasClass(config.contentPanel.groupHeader_className) || sumRow.hasClass(config.contentPanel.groupFooter_className)) {
                sumType = 2; // tính toán dữ liệu trong group
            } else if (sumRow.hasClass("detail")) {
                sumType = 1; // tính toán dữ liệu trên dòng
            }

            var itm = {};
            itm.Key = $(this).text();
            itm.Datatype = $(this).attr("datatype");
            itm.Value = $(this).attr("value");
            itm.Field = $(this).attr("field");
            itm.FieldWith = $(this).attr("fieldw");
            itm.Format = $(this).attr("format");
            itm.Type = sumType;
            result.push(itm);
        });
        return result;
    };

    //#endregion

    window.eReport = eReport;
})
(window.jQuery, _);
