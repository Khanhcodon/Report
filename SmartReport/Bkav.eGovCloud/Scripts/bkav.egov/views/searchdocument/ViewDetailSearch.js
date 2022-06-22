$(document).ready(function () {
    $('#autoClick').trigger('click');
    $('#autoClickCheck').trigger('click');
});

function ViewDetailSearch(docId) {
    var that = this;
    $.ajax({
        url: "/ReportViewer/GetDocumentDetail",
        data: { id: docId },
        beforeSend: function () { },
        success: function (result) {
            if (result.CategoryBusinessId == 8) {
                var resultNote = result.Note;
                $('#BodyContentSearch').append(resultNote);
            } else {
                var dataProcessInfo = JSON.parse(result.ProcessInfo);
                var dataKey = _.keys(dataProcessInfo.header);
                var obj = {};
                if (dataProcessInfo != undefined && dataProcessInfo.headerFooter != undefined) {
                    var dataHeader = JSON.parse(dataProcessInfo.headerFooter);
                    obj.FormHeader = dataHeader.FormHeader;
                    obj.FormFooter = dataHeader.FormFooter
                }
                for (var i = 0 ; i < dataProcessInfo.extra.headerSetting.length ; i++) {
                    var string = "";
                    for (var j = 0 ; j < dataProcessInfo.extra.headerSetting[i].length; j++)
                        if (dataProcessInfo.extra.headerSetting[i][j] != " " && dataProcessInfo.extra.headerSetting[i][j] != ""
                            && dataProcessInfo.extra.headerSetting[i][j] != null) {
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
                                _.each(dataProcessInfo.extra.headerSetting[i], function (e) {

                                })
                                _.each(dataProcessInfo.headerNested, function (element) {
                                    if (element.row == i && element.col == j) {
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
                                    }
                                });
                            }
                            if (i == dataProcessInfo.extra.headerSetting.length - 1)
                                if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                    string = string + "<th width='" + width + "' hidden>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                else string = string + "<th width='" + width + "'>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";

                            //check string
                            if (!string.includes(dataProcessInfo.extra.headerSetting[i][j])) {
                                // render data
                                string = string + "<th width='" + width + "'>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                            }
                        }


                    //iframe
                    $('#bodyContentSearch').find('#tblListSearch').find('thead').append("<tr>" + string + "</tr>");

                    $('#bodyContentSearch > #tblListSearch > thead > tr > th').each(function (i, item) {
                        $this = $(item);
                        $this.addClass('htBold');
                        $this.addClass('htCenter');
                    });
                    //hiepns
                }

                $('.HeaderContent .HeaderContentBody').append(obj.FormHeader);
                $('.FooterContent .FooterContentBody').append(obj.FormFooter);

                //render ra dữ liệu 
                var dataProcessInfoNew = JSON.parse(result.ProcessInfo);
                var dataKeyNew = _.keys(dataProcessInfoNew.header);
                var noteSS = JSON.parse(result.Note);
                var dataSS = dataProcessInfoNew.data;
                dataProcessInfoNew.data = noteSS;

                var items = _.map(dataKeyNew, function (item) {
                    return item.split("!!", 1);
                });
                var stringResult;

                if (dataProcessInfoNew.classCells) {
                    for (var i = 0 ; i < dataProcessInfoNew.data.length ; i++) {
                        stringResult = "";
                        for (var j = 0; j < items.length; j++) {
                            if (dataProcessInfoNew.extra.columnSetting[dataKeyNew[j]]["Hidden"])
                                stringResult = stringResult + "<td class='" + dataProcessInfoNew.classCells[i][j] + "' hidden >${" + items[j] + "}</td>";
                            else stringResult = stringResult + "<td>${" + items[j] + "}</td>";
                        }
                        var template = "<tr>" + stringResult + "</tr>";

                        var objData = dataProcessInfoNew.data[i];

                        var resultSearch;
                        if (typeof (objData) == "object") {
                            for (var key in objData) {
                                var formatNumber = objData[key];
                                if (Number(formatNumber)) {
                                    var stringSearch = template.search(key) - 3;
                                    if (template.search(key) != -1) {//true
                                        var strend = template.slice(stringSearch);
                                        template = template.substring(0, stringSearch);
                                        template += " class='textRight' " + strend;
                                    }
                                }

                            }
                        }
                        formatObject(objData);
                        var data = $.tmpl(template, dataProcessInfoNew.data[i]);
                        $('#bodyContentSearch').find('#tblListSearch').find('tbody').append(data);

                    }
                } else {
                    for (var i = 0; i < dataProcessInfoNew.data.length; i++) {
                        stringResult = "";
                        for (var j = 0; j < items.length; j++) {
                            if (dataProcessInfoNew.extra.columnSetting[dataKeyNew[j]]["Hidden"])
                                stringResult = stringResult + "<td hidden >${" + items[j] + "}</td>";
                            else stringResult = stringResult + "<td>${" + items[j] + "}</td>";
                        }
                        var template = "<tr>" + stringResult + "</tr>";

                        var objData = dataProcessInfoNew.data[i];
                        formatObject(objData);
                        var data = $.tmpl(template, dataProcessInfoNew.data[i]);
                        $('#bodyContentSearch').find('#tblListSearch').find('tbody').append(data);
                    }
                }
            }
        },
        error: function (xhr) { },
        complete: function () { }
    });
}
function formatNumbers(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
}
function formatObject(obj) {
    if (typeof (obj) == "object") {
        for (var key in obj) {
            var formatNumber = obj[key];
            if (Number(formatNumber)) {
                //if (isFloat(formatNumber) || isInteger(formatNumber)) {
                if (formatNumber >= 1000) {
                    var num = formatNumbers(formatNumber);
                    obj[key] = num;
                }
                //}
            }

        }
    }
}

function ViewDetailSearchCheckFile(docID, attID) {
    $('#ViewFileAtt').append('<iframe id="fileShowJframe" src="/AttachmentPreview/Index?DocumentId='
        + docID + '&AttachmentId=' + attID + '" style="width: 100%; height: 860px;; border: none;"></iframe>');
}

function ViewDetailFile(docID, attID) {
    $('#ViewFiles').hide();
    $('#ViewFileAtt').show();
    $('#ViewFileAtt').find('iframe').remove();
    $('#ViewFileAtt').append('<iframe id="fileShowJframe" src="/AttachmentPreview/Index?DocumentId='
        + docID + '&AttachmentId=' + attID + '" style="width: 100%; height: 860px;; border: none;"></iframe>');
}

$('.BackFile').click(function (e) {
    $('#ViewFiles').show();
    $('#ViewFileAtt').hide();
})