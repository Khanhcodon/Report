function ViewProcess(documentId) {
    var that = this;
    $.ajax({
        url: "/ReportViewer/GetDocumentDetail",
        data: { id: documentId },
        success: function (result) {

            //clear
            $('#bodyContentViewProsessStatic').find('#tblListDocumentProcessStatic').find('thead tr').remove();
            $('#bodyContentViewProsessStatic').find('#tblListDocumentProcessStatic').find('thead').append('<tr class="persist-header"></tr>');
            $('#bodyContentViewProsessStatic').find('#tblListDocumentProcessStatic').find('tbody tr').remove();
            $('#bodyContentViewProsessStatic').find('#tblListDocumentProcessStatic').find('tbody tr').append('<tr></tr>');

            if (result.CategoryBusinessId == 8) {
                var resultNote = result.Note;
                $('#BodyContentStatic').append(resultNote);
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

                            //check string
                            if (!string.includes(dataProcessInfo.extra.headerSetting[i][j])) {
                                // render data
                                string = string + "<th width='" + width + "'>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                            }
                        }

                    //iframe
                    $('#bodyContentViewProsessStatic').find('#tblListDocumentProcessStatic').find('thead').append("<tr>" + string + "</tr>");

                    $('#bodyContentViewProsessStatic > #tblListDocumentProcessStatic > thead > tr > th').each(function (i, item) {
                        $this = $(item);
                        $this.addClass('htBold');
                        $this.addClass('htCenter');
                    });
                    //hiepns
                }

                $('.HeaderContentStatic .HeaderContentBodyStatic').append(obj.FormHeader);
                $('.FooterContentStatic .FooterContentBodyStatic').append(obj.FormFooter);

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
                        //if (typeof (objData) == "object") {
                        //    for (var key in objData) {
                        //        var formatNumber = objData[key];
                        //        if (Number(formatNumber) || formatNumber == 0) {
                        //            //if (isFloat(formatNumber) || isInteger(formatNumber)) {
                        //            var stringSearch = template.search(key) - 3;
                        //            if (template.search(key) != -1) {//true
                        //                var strend = template.slice(stringSearch);
                        //                template = template.substring(0, stringSearch);
                        //                template += " class='textRight' " + strend;
                        //            }


                        //            //}
                        //        }

                        //    }
                        //}
                        formatObject(objData);
                        var data = $.tmpl(template, dataProcessInfoNew.data[i]);
                        $('#bodyContentViewProsessStatic').find('#tblListDocumentProcessStatic').find('tbody').append(data);

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
                        $('#bodyContentViewProsessStatic').find('#tblListDocumentProcessStatic').find('tbody').append(data);
                    }
                }
            }
        },
        error: function (xhr) { },
        complete: function () {
            
        }
    });
}

function ViewTimeReport(documnetId, DepartmentName, CategoryBusinessId) {
    var that = this;
    $.ajax({
        url: "/Dashboard/ViewTimeReports",
        data: {
            id: documnetId,
            departmentName: DepartmentName,
            categoryBusinessId: CategoryBusinessId
        },
        success: function (result) {
            $('#ViewModalListDocument').find('#tblListDocumentListDocument').find('tbody tr').remove();
            var stt = 0;
            for (var i = 0 ; i < result.length; i++) {
                var parserTotal = result[i];
                var arrayDoctype = [],objDoctype = {};
                objDoctype.Stt = i;
                objDoctype.DocumentId = parserTotal.DocumentId;
                objDoctype.Compendium = parserTotal.Compendium;
                objDoctype.InOutPlace = parserTotal.InOutPlace;
                objDoctype.UserCreatedName = parserTotal.UserCreatedName;
                if (parserTotal.CategoryBusinessId == 4) {
                    objDoctype.CategoryBusinessId = "Báo cáo số liệu"
                } else if (parserTotal.CategoryBusinessId == 8) {
                    objDoctype.CategoryBusinessId = "Báo cáo thuyết minh"
                }


                if (parserTotal.Status == 0 && (parserTotal.CategoryBusinessId == 4
                            || parserTotal.CategoryBusinessId == 8)) {
                    // chua xu ly
                    objDoctype.Status = "Chưa xử lý";
                }else if (parserTotal.StatusReport != undefined) {
                    if (parserTotal.Status == 2 && (parserTotal.StatusReport == 2 || parserTotal.StatusReport == 1)
                        && (parserTotal.CategoryBusinessId == 4 || parserTotal.CategoryBusinessId == 8
                        || parserTotal.CategoryBusinessId == 64)) {
                        // dang xu ly
                        objDoctype.Status = "Đang xử lý";
                    }
                    else if (parserTotal.StatusReport == 4 && (parserTotal.Status == 2 || parserTotal.Status == 4)
                            && (parserTotal.CategoryBusinessId == 4 || parserTotal.CategoryBusinessId == 8 ||
                            parserTotal.CategoryBusinessId == 64)) {
                        // da xu ly
                        objDoctype.Status = "Đã báo cáo";
                    }
                    else if (parserTotal.Status == 8 || parserTotal.Status == 1) {
                        continue;
                    } else {
                        continue;
                    }
                    objDoctype.Stt = stt;
                    objDoctype.departmentName = parserTotal.InOutPlace;
                    objDoctype.NameDocType = parserTotal.DocTypeNameDocType;
                    objDoctype.actionLevel = parserTotal.ActionLevel;
                    objDoctype.userName = parserTotal.UserCreatedName;
                    objDoctype.Name = parserTotal.Name;
                    //xl date
                    var newDate = new Date(parserTotal.DateCreated);
                    let date = ("0" + newDate.getDate()).slice(-2);
                    let month = ("0" + (newDate.getMonth() + 1)).slice(-2);
                    let year = newDate.getFullYear();
                    let hours = newDate.getHours();
                    let minutes = newDate.getMinutes();
                    let seconds = newDate.getSeconds();
                    var dateStrEnd = year + "-" + month + "-" + date + " " + hours + ":" + minutes + ":" + seconds;
                    //end date
                    objDoctype.DateCreated = dateStrEnd;
                    arrayDoctype.push(objDoctype);
                    var data = $.tmpl($('#templaceListDocument'), arrayDoctype);
                    stt++;
                    $('#ViewModalListDocument').find('#tblListDocumentListDocument').find('tbody').append(data);
                    arrayDoctype = [];
                }        
            }
        },
        error: function (xhr) { },
        complete: function () {

        }
    });
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
    function formatNumbers(num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
    }

    function CloseModalProsessStatic() {
        HeaderFooter();
        $('#bodyContentViewProsessStatic > #tblListDocumentProcessStatic > thead > tr').remove();
        $('#bodyContentViewProsessStatic > #tblListDocumentProcessStatic > tbody > tr').remove();
    }
    function CloseModalListDocument() {
        $('#tblListDocumentListDocument > #tblListDocumentProcessStatic > tbody > tr').remove();
    }

    function HeaderFooter() {
        $('#BodyContentStatic .HeaderContentStatic .HeaderContentBodyStatic').remove();
        $('#BodyContentStatic .FooterContentStatic .FooterContentBodyStatic').remove();
        $('#BodyContentStatic .HeaderContentStatic').append('<div class="HeaderContentStatic"></div>');
        $('#BodyContentStatic .FooterContentStatic').append('<div class="FooterContentStatic"></div>');
    }