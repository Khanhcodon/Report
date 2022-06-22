define([], function () {
    var Export = Backbone.View.extend({
        initialize: function (options) {
            var that = this;
            that.doc = options.document;
        },

        getFormSolieu: function () {
            var that = this;
            var config = JSON.parse(that.doc.configHandsontable);
            var data = that.doc.dataFormTable.getSourceData()
            var dataProcessInfo = config;
            var dataKey = _.keys(dataProcessInfo.header);
            var obj = {};
            if (dataProcessInfo != undefined && dataProcessInfo.headerFooter != undefined) {
                var dataHeader = JSON.parse(dataProcessInfo.headerFooter);
                obj.FormHeader = dataHeader.FormHeader;
                obj.FormFooter = dataHeader.FormFooter
            }

            var $elDoc = $('<div><table border="1" cellpadding="5" cellspacing="0" ><thead></thead><tbody></tbody></table></div>');
            for (var i = 0 ; i < dataProcessInfo.extra.headerSetting.length ; i++) {
                var string = "";
                for (var j = 0 ; j < dataProcessInfo.extra.headerSetting[i].length; j++)
                    if (dataProcessInfo.extra.headerSetting[i][j] != " " && dataProcessInfo.extra.headerSetting[i][j] != "") {
                        var width = dataProcessInfo.colWidths[j] ? dataProcessInfo.colWidths[j] : "";

                        if (dataProcessInfo.extra.mergedCells.length) {
                            _.each(dataProcessInfo.extra.mergedCells, function (element) {
                                if (element.row == i && element.col == j)
                                    if (i == dataProcessInfo.extra.headerSetting.length - 1 && element.colspan) {
                                        for (var k = 0; k < element.colspan - 1; k++) {
                                            if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                                string = string + "<th width='" + width + "' hidden>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                            else string = string + "<th width='" + width + "'>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                        }
                                    } else {
                                        if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                            string = string + "<th width='" + width + "' colspan = '" + element.colspan + "' rowspan ='" + element.rowspan + "' hidden>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                        else string = string + "<th width='" + width + "' colspan = '" + element.colspan + "' rowspan ='" + element.rowspan + "' >" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                    }
                            });
                        } else if (dataProcessInfo.headerNested) {
                            _.each(dataProcessInfo.headerNested, function (element) {
                                if (element.row == i && element.col == j)
                                    if (i == dataProcessInfo.extra.headerSetting.length - 1 && element.colspan) {
                                        for (var k = 0; k < element.colspan - 1; k++) {
                                            if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                                string = string + "<th width='" + width + "' hidden>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                            else string = string + "<th width='" + width + "'>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                        }
                                    }
                                    else {
                                        if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                            string = string + "<th width='" + width + "' colspan = '" + element.colspan + "' rowspan ='" + element.rowspan + "' hidden>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                        else string = string + "<th width='" + width + "' colspan = '" + element.colspan + "' rowspan ='" + element.rowspan + "' >" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                    }
                            });

                        }

                        if (i == dataProcessInfo.extra.headerSetting.length - 1)
                            if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                string = string + "<th width='" + width + "' hidden>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                            else string = string + "<th width='" + width + "'>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                    }
                //iframe
                $elDoc.find("thead").append("<tr>" + string + "</tr>");
                $elDoc.find('thead > tr > th').each(function (i, item) {
                    $this = $(item);
                    $this.addClass('htBold');
                    $this.addClass('htCenter');
                });

                var dataKey = _.keys(dataProcessInfo.header);
                var noteSS = data;
                var dataSS = dataProcessInfo.data;

                dataProcessInfo.data = noteSS;

                var items = _.map(dataKey, function (item) {
                    return item.split("!!", 1);
                });

                var string;
                if (dataProcessInfo.classCells) {
                    for (var i = 0 ; i < dataProcessInfo.data.length ; i++) {
                        string = "";
                        for (var j = 0; j < items.length; j++) {
                            if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                string = string + that.getTemplateTrTag(dataProcessInfo.classCells[i][j], "${" + items[j] + "}");
                            else string = string + "<td>${" + items[j] + "}</td>";
                        }
                        var template = "<tr>" + string + "</tr>";
                        var data = $.tmpl(template, dataProcessInfo.data[i]);
                        $elDoc.find('tbody').append(data);
                    }
                } else {
                    for (var i = 0; i < dataProcessInfo.data.length; i++) {
                        string = "";
                        for (var j = 0; j < items.length; j++) {
                            if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                string = string + "<td hidden >${" + items[j] + "}</td>";
                            else string = string + "<td>${" + items[j] + "}</td>";
                        }
                        var template = "<tr>" + string + "</tr>";
                        var data = $.tmpl(template, dataProcessInfo.data[i]);
                        $elDoc.find('tbody').append(data);
                    }
                }
            }
            
            if (config.extra && config.extra.hiddenColumns) {
                for (var  i = config.extra.hiddenColumns.length - 1; i >= 0; i--) {
                    var temp = 'td:eq(' + config.extra.hiddenColumns[i] + '),th:eq(' + config.extra.hiddenColumns[i] + ')'
                    $elDoc.find('tr').find(temp).remove();
                }
            }
            return $elDoc.html();
        },

        getTemplateTrTag: function (classCell, key) {
            if (!classCell) {
                return "<td>${" + key + "}</td>";
            }
            var $tr = $("<tr><td></td></tr>");
            if (classCell.indexOf("htCenter")) {
                $tr.find("td").css({ "text-align": "center" })
            }
            if (classCell.indexOf("htLeft")) {
                $tr.find("td").css({ "text-align": "left" })
            }
            if (classCell.indexOf("htRight")) {
                $tr.find("td").css({ "text-align": "right" })
            }
            if (classCell.indexOf("htBold")) {
                $tr.find("td").css({ "font-weight": "bold" })
            }
            if (classCell.indexOf("htItalic")) {
                $tr.find("td").css({ "font-weight": "italic" })
            }
            $tr.find("td").text(key)
            return $tr.html();
        }
    });

    return Export;
});