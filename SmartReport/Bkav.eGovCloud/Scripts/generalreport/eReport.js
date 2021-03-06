/// <reference path="eReport.js" />
(function () {
    //#region Private Fields
    var regexUtcDate = /\d{4}-[0-1]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-6]\d$/;
    var DEFAULT_PAGESIZE = 15000;
    var TimeCurrent = "";
    var reportUrls = {
        gets: "/ReportViewer/GetAll",
        getsByParent: "/ReportViewer/GetReportByParent",
        getReport: "/ReportViewer/GetReport",
        getReportData: "/ReportViewer/GetReportData",
        getGroups: "/ReportViewer/GetGroups",
        getGroupData: "/ReportViewer/GetGroupData",
        getFileData: "/ReportViewer/GetFileData",
        getDataEform: "/ReportViewer/GetDataEform",
        "export": "/ReportViewer/Export?ReportId="
    };
    var keyColspanHeader = "/";

    var isFunc = function (func) {
        return typeof func === "function";
    };

    var objDefineTree = {
        plugins: ["themes", "html_data", "ui", "types"],
        ui: { select_multiple_modifier: false },
        core: {
            "dblclick_toggle": true
        },
        types: {
            "valid_children": ["root"],
            "types": {
                "root": {
                    "hover_node": false,
                    "dblclick_toggle": true,
                    "select_node": function () { return false; }
                }
            }
        }
    };

    //#endregion
    var layoutGroup = Backbone.View.extend({
        el: "#LayoutContent",
        template: "#treeTemplate",
        initialize: function (options) {
            //$.jstree.defaults.core.dblclick_toggle = true;
            var that = this;
            that.model = options.model;
            that.ReportGeneral = options.report;
            this.render();
        },
        
    })


    var eReport = Backbone.View.extend({
        el: "#reportContent",
        template: "#treeTemplate",
        reportTemplate: '<table border="1" cellspacing="0" class="table table-hover table-main persist-area" id="tblListDocument"><colgroup></colgroup><thead></thead><tbody></tbody></table>',
        pagingTemplate: '<nav aria-label="Page navigation" class="pager"><div class="col-md-10"><ul class="pagination pull-right"></ul></div>\
                        <div class="col-md-6"><div style="width: 100px;"><div class="input-group go-to-page" style="margin: 21px 0px;"><input type="text" class="form-control input-sm"/><span class="input-group-btn"><a class="btn btn-default btn-sm" type="button">Đi đến</a></span></div></div></div></nav>',
        reportTreeEl: $("#reportTree"),
        defaultGroup: { displayName: "Không hiển thị nhóm", columnName: "default", groupBy: "default" },
        events: {
            "click #reportGroups li": "_showGroup",
            "click .report-group a": "_bindGroupData",
            "click .report-group span": "_exportGroup",
            "click #dateConditions a": "_changeDateCondition",
            "change select.statistics-time": "_changeDate",
            "change #dateSelect": "_changeDate",
            "click #btnReport": "_renderReport",
            "click #btnExports a": "_export",
            "click .btnViewDocument": "_viewDocument",
            "click #btnWord": "_export",
            "click #btnDownload": "_exportHtml",
            "click #btnDieuHanhNoiBo": "_getHtml",
            "click #readReport": "_readToText"
        },

        initialize: function (options) {
            //$.jstree.defaults.core.dblclick_toggle = true;
            var that = this;
            that.dashboardCallback = options.callback;
            this.render();
        },

        render: function () {
            var that = this;
            //this._layout();
            this._initTime();
            this.currentPage = 1;

            this._getReportTrees(function (allReport) {
                that.model = allReport;
                that._renderReportTree();
            });
        },

        //#region Layout

        _layout: function () {
            this.$("#container").layout({
                resizable: true,
                closable: false,
                west__size: 260,
                west__minSize: 220,
                west__maxSize: 500,
                west__spacing_open: 1,
                spacing_closed: 2,
                west__resizable: true,
                west__paneSelector: "#left",
                center__paneSelector: "#right"
            });
        },

        _renderReportTree: function () {
            var that = this;
            var isLoadedChildren, parentId, that = this;
            this.reportTreeEl.append($.tmpl($(this.template), this.model));
            window.GetReportsByParent = function (parentId, $ul) {
                that._getReportsByParent(parentId, function (result) {
                    if (result) {
                        var reports = _.sortBy(result, function (item) { return item.order; });
                        $(that.template).tmpl(reports).appendTo($ul);
                    }
                });
            };

            var url_string = window.location.href;
            var url = new URL(url_string);
            var reportId = url.searchParams.get("reportId");
            if (reportId) {
                that.currentId = reportId;
                var isMobile = window.matchMedia("only screen and (max-width: 760px)").matches;
                if (!isMobile) {
                    $("body").addClass("sidebar-collapse");
                }
                that._renderReportParams();
            } else {
                $("body").removeClass("sidebar-collapse");
            }

            window.ShowReport = function ($el) {
                var isLabel = $el.attr('data-isLabel');
                var children = $el.attr('data-children');
                if (isLabel == "false") {

                    destroyEvent(event);
                    var reportId = $el.attr("data-id");
                    //debugger
                    window.currentId = reportId;
                    window.tenbaocao = $el.attr("data-groupdisplayname");
                    that.currentId = reportId;
                    that.currentPage = 1;
                    that.total = 0;
                    that.sortBy = "";
                    that.GroupBy = "default";
                    that.treeGroupBy = $el.attr("data-groupname");
                    that.treeGroupValue = $el.attr("data-group-value");
                    that.groupId = $el.attr("group-id");
                    that.isShowTotal = $el.attr("data-is-show-total");
                    if (children != "undefined" && children.length > 0) {
                        $el.find(".btnToggleTree").hide();
                        var childrenObject = JSON.parse(children);
                        that.renderLayoutGroup(childrenObject);
                    } else {
                        that._renderReport();
                    }
                }
            }
        },

        renderLayoutGroup: function (modelGroup) {
            var that = this;
            var reportParams = this._getParams();
            var template = $("#reportGroupTree").tmpl(modelGroup);
            $("#contentReport").html(template);
            $("#layoutContent").removeClass("d-none");
            $("#reportDataContent").addClass("d-none");
            $("#layoutContent").find(".group-data").click(function () {
                var dataKey = $(this).attr("data-key");
                var dataGroup = $(this).attr("data-group-value");

                $("#layoutContent").addClass("d-none");
                $("#reportDataContent").removeClass("d-none");

                that.treeGroupBy = dataKey;
                that.treeGroupValue = dataGroup;
                
                $.ajax({
                    url: reportUrls.getReport,
                    data: reportParams,
                    beforeSend: function () {
                        showProgress();
                    },
                    success: function (result) {
                        if (result.isFile == 1 && dataKey == 'AttachmentId' &&  $('#btnGroupFile').hasClass("opentFileGroup")) {
                            that._renderFile();
                        } else if (result.isFile == 1 && dataKey == 'AttachmentId' && $('#btnGroupFile').hasClass("closeFileGroup")) {
                            $('#btnFile').removeClass('opentFile');
                            $('#btnFile').addClass('closeFile');
                            $("#btnFile").css("display", "block");
                            $('#btnGroupFile').removeClass('closeFileGroup');
                            $('#btnGroupFile').addClass('opentFileGroup');
                            that._renderFile();
                        }
                        else {
                            that._renderReport();
                        }
                    },
                    error: function (xhr) { },
                    complete: function () { }
                });
            })
        },

        _renderReportParams: function () {
            var that = this;
           
            that.currentPage = 1;
            that.total = 0;
            that.sortBy = "";
            that.GroupBy = "default";
            that.treeGroupBy = "";
            that.treeGroupValue = "";
            that.groupId = "";
            that.isShowTotal = "false";
            that._renderReport();
        },

        //#endregion

        //showfile
        _renderFile: function(){
            var that = this;
            var fileParams = this._getParams();

            $.ajax({
                url: reportUrls.getFileData,
                data: fileParams,
                beforeSend: function () {
                    showProgress();
                },
                success: function (result) {
                    //call ajax
                    $.ajax({
                        url: '/AttachmentPreview/Index',
                        data: result,
                        beforeSend: function () {
                            showProgress();
                        },
                        success: function (_result) {
                            $('#reportDataContent').find('#fileShowJframe').remove();
                            $('#reportDataContent').find('section').hide();
                            $('#reportDataContent').find('.opentFile').show();
                            $('#reportDataContent').append('<iframe id="fileShowJframe" src="/AttachmentPreview/Index?DocumentId='
                                + result.DocumentId + '&AttachmentId=' + result.AttachmentId + '" style="width: 100%; height: 100%; border: none;"></iframe>');
                        },
                        error: function (xhr) {

                        },
                        complete: function () {
                            //var frame = document.getElementById('fileShowJframe');
                            //frame.contentWindow.postMessage('SendSmalFile', '*'); 
                        }
                    });
                },
                error: function (xhr) {

                },
                complete: function () {

                }
            });
        },

        //show report
        _renderReport: function () {
            var that = this;
            var reportParams = this._getParams();

            $.ajax({
                url: reportUrls.getReport,
                data: reportParams,
                beforeSend: function () {
                    showProgress();
                },
                success: function (result) {
                    if (!result) {

                    }
                    $('#reportDataContent').find('section').show();
                    $('#fileShowJframe').remove();
                    $('#reportDataContent').find('.opentFile').hide();
                    $('#reportDataContent').find('.closeFile').hide();
                    that.$("#headerReport").html(result.header);
                    that.$("#footerReport").html(result.footer);

                    that.pivotConfig = result.PivotConfig;

                    window.htmlColumn = result.ColumnConfig;

                    that.report = result;

                    that.displaySettings = result.displaySettings;
                    that._renderShowColumn(that.displaySettings);

                    //that.displaySettings.unshift({ displayName: "Stt", columnName: "Stt", width: 35 });
                    //that.displaySettings.push({ displayName: "Xem", columnName: "View", width: 50 });

                    that.groupSettings = result.groupSettings;
                    that.groupSettings.push(that.defaultGroup);

                    that.sortSettings = result.sortSettings;
                    that.total = result.total;
                    that.type = result.description;
                    that._formatDateAndShowDatepicker(that.type);
                    that.isThuyetMinh = result.isHCMC;
                    that.isFile = result.isFile;
 
                    that._showReport();
                    // Show dashboard
                    //that.dashboardCallback(result.description);
                },
                error: function (xhr) { },
                complete: function () { }
            });
        },

        _reset: function () {
            this.GroupBy = "";
            this.$("#groupName").text(this.defaultGroup.displayName);
            this.$("#reportGroups").empty();
            this.$("#reportViewer").html(this.reportTemplate);
        },

        _renderShowColumn(listColumn) {
            var $template = '<div class="form-group check-column">\
                                <input type="checkbox" class="columnCheck chk-col-grey" checked id="${columnName}">\
                                <label for="${columnName}" class="control-sidebar-subheading" data-group="${columnName}">${displayName}</label>\
                            </div>'
            $("#filterColumn").html($.tmpl($template, listColumn));
            this._renderEventColumn();
        },

        _renderEventColumn: function () {
            var that = this;
            $('#filterColumn .check-column .columnCheck').click(function (e) {
                var $checkColumn = $(e.target).closest(".check-column");

                var isChecked = $checkColumn.find(".columnCheck:checked").length > 0;
                var columnName = $checkColumn.find(".control-sidebar-subheading").attr("data-group");
                that.toggleColumn(columnName, isChecked);
            });

            $('#get-checked-data').on('click', function (event) {
                event.preventDefault();
                var checkedItems = {}, counter = 0;
                $("#check-list-box li.active").each(function (idx, li) {
                    checkedItems[counter] = $(li).text();
                    counter++;
                });
                $('#display-json').html(JSON.stringify(checkedItems, null, '\t'));
            });
        },

        toggleColumn: function (dataColumn, isChecked) {
            var $th = $("th[data-column='" + dataColumn + "']");
            var $td = $("td[data-column='" + dataColumn + "']");
            if (isChecked) {
                $th.show();
                $td.show();
            } else {
                $th.hide();
                $td.hide();
            }
        },

        _showReport: function (success) {
            this._reset();

            this.$(".reportName").text(this.report.reportName);
            this.$(".reportDesc").text(this.report.description);
            this.$(".lastUpdate").text(this.report.lastUpdate);
            if (!this.isThuyetMinh) {
                this._renderToolbarGroups();
                this._renderHeader();
                this._renderPager();
                this._showTotal();
            }
            this._getReportData();
        },

        _renderToolbarGroups: function () {
            var that = this;
            if (!this.groupSettings || this.groupSettings.length === 0) {
                this.$("#reportGroups").parents('li').hide();
                return;
            }
            this.$("#reportGroups").parents('li').show();
            this.$("#reportGroups").append($.tmpl('<li value="${groupBy}" data-group="${columnName}"><a href="#">${displayName}</a></li>', this.groupSettings));
        },

        _renderHeader: function () {
            renderGridHeader(this.displaySettings, this.$("#reportViewer"));
            // Xử lý dữ liệu
            var displayNames = _.pluck(this.displaySettings, "displayName");
            var isMultiRowHeader = this._getConfigHeaderColumn(displayNames);
            if (isMultiRowHeader.isMultiRow) {
                this.$("#reportViewer").find('table thead').append(this._renderColspanHeader(displayNames, isMultiRowHeader.maxRow))
            }
            // Cột sắp xếp mặc định trong cấu hình
            // Todo: cần config cái này trong cấu hình
            var headerColumns = [], that = this;

            _.each(this.displaySettings, function (itm) {
                var displayName = itm.displayName;
                var displayNames = displayName.split(keyColspanHeader);
                if (displayNames.length == isMultiRowHeader.maxRow) {
                    headerColumns.push({
                        text: displayNames[isMultiRowHeader.maxRow - 1],
                        value: (itm.sortName == undefined || itm.sortName == "") ? itm.columnName : itm.sortName,
                        isDesc: false,
                        enableSort: itm.enableSort,
                        justify: itm.justify,
                        callback: function (option) {
                            that._sort(option.value, option.isDesc);
                        }
                    })
                }
            });

            this.gridHeader = new GridHeader({ model: new gridHeaderCollection(headerColumns), parent: that });
            this.gridHeader.$el.addClass("persist-header");
            this.$("#reportViewer").find('table thead').append(this.gridHeader.$el);

            //  this._enableResize();
        },

        updateTableHeaders: function () {
            $(".persist-area").each(function () {
                var el = $(this),
                    offset = el.offset(),
                    scrollTop = $("#reportViewer").scrollTop(),
                    $header = $('.persist-header');

                if (scrollTop > 3 && (scrollTop < el.height())) {
                    var $tableBodyCell = $('.persist-header th'),
                    $headerCell = $('.persist-header th');
                    $tableBodyCell.each(function (index) {
                        $headerCell.eq(index).width($(this).width());
                    });
                    $header.addClass("floatingHeader");
                } else {
                    $header.removeClass("floatingHeader")
                    //console.log("scroll" + scrollTop + "-offset" + offset.top + "-height" + el.height() + "false")
                };
            });
        },

        fixHeader: function () {
            var that = this;
            $("#reportViewer")
               .scroll(that.updateTableHeaders)
               .trigger("scroll");
            //$(".persist-area").each(function () {
            //    clonedHeaderRow = $(".persist-header", this);
            //    clonedHeaderRow
            //      .before(clonedHeaderRow.clone())
            //      .css("width", clonedHeaderRow.width())
            //      .addClass("floatingHeader");
            //    var $tableBodyCell = $('.persist-header:first th'),

            //    $headerCell = $('.persist-header:last th');
            //    $tableBodyCell.each(function (index) {
            //        $headerCell.eq(index).width($(this).width());
            //    });
            //});

            //$("#reportViewer")
            //   .scroll(that.updateTableHeaders)
            //   .trigger("scroll");
        },

        _getConfigHeaderColumn: function (displayNames) {
            var isMultiRow = false;
            var maxRow = 1;
            _.each(displayNames, function (item) {
                var displayNameItems = item.split(keyColspanHeader);
                if (displayNameItems.length > 1) {
                    isMultiRow = true;
                    maxRow = displayNameItems.length;
                }
            })
            return {
                isMultiRow: isMultiRow,
                maxRow: maxRow
            }
        },

        _renderColspanHeader: function (displayNames, maxRow) {
            var resultFinish = "";
            var newcolumnSettings = _.filter(displayNames, function (item) {
                return (item != "Stt") && (item != "Xem") && (item != "Color");
            });

            var colspanArray = this._renderDataHeader(newcolumnSettings, maxRow);

            var firstRun = false;
            for (var i = 0; i < maxRow - 1 ; i++) {
                var result = "<tr>";
                if (firstRun) {
                    // Stt
                    result += "<th class='text-center' style='vertical-align: middle; font-weight:bold;border-color:#000000; white-space:nowrap' rowspan='" + maxRow + "'>Stt</th>";
                }

                // Hiển thị các cột theo cấu hình
                _.each(colspanArray[i], function (item) {
                    if (item) {
                        result += "<th class='text-center' style='vertical-align: middle;font-weight:bold;border-color:#000000; white-space:nowrap' colspan='" + item.colSpan + "' rowspan='" + item.rowSpan + "' data-column='" + item.data + "'>" + item.data + "</th>";
                    }
                });

                if (firstRun) {
                    if (this.hasViewDocument) {
                        result += "<th class='text-center' style='vertical-align: middle;font-weight:bold;border-color:#000000; white-space:nowrap' rowspan='" + maxRow + "'><a href='#' class='btnViewDocument' data-id='${DocumentCopyId}' data-title='${Compendium}'>Xem</a></th>";
                    }
                }

                result += "</tr>";
                resultFinish += result;
                firstRun = false;
            }

            return resultFinish;
        },

        _renderDataHeader: function (newcolumnSettings, maxRow) {
            var displayArray = [];
            // Xử lý dữ liệu về dang object có xử lý rowspan
            _.each(newcolumnSettings, function (item) {
                var displayNameItems = item.split(keyColspanHeader);
                displayNameItemsReverse = displayNameItems//.reverse();
                var arrayChildren = [];
                _.each(displayNameItemsReverse, function (item) {
                    if (displayNameItemsReverse.length > 1) {
                        if (item == displayNameItemsReverse[displayNameItemsReverse.length - 1]) {
                            arrayChildren.push({ data: item, rowSpan: maxRow - displayNameItemsReverse.length + 1 });
                        } else {
                            arrayChildren.push({ data: item, rowSpan: 1 });
                        }
                    } else {
                        arrayChildren.push({ data: item, rowSpan: maxRow });
                    }
                })
                displayArray.push(arrayChildren);
            })
            // lọc dữ liệu thành các hàng
            var displayArrayUnzip = _.unzip(displayArray);
            //displayArrayUnzip = displayArrayUnzip//.reverse();
            // Xử lý colspan
            var colspanArray = [];
            _.each(displayArrayUnzip, function (item) {
                item = item.filter(function (n) { return n != undefined });
                var rows = _.uniq(item, function (item, key, a) {
                    return item.data;
                });
                var countByData = _.countBy(item, "data");
                var colspanArrayRow = [];
                _.each(rows, function (itemRow) {
                    if (itemRow) {
                        itemRow["colSpan"] = countByData[itemRow.data];
                        colspanArrayRow.push(itemRow);
                    }
                });
                colspanArray.push(colspanArrayRow);
            });

            return colspanArray;
        },

        _sort: function (sortBy, isDesc) {
            if (sortBy === "") {
                return;
            }

            this.sortBy = sortBy;
            this.isDesc = isDesc;

            this.isGrouping ? this._getGroupData() : this._getReportData();
        },

        _getReportData: function () {
            var that = this;
            $.ajax({
                url: reportUrls.getReportData,
                beforeSend: function () {
                    showProgress();
                },
                data: that._getParams(),
                success: function (result) {
                    that.data = result;

                    //cấu hình loại báo cáo ở đây
                    if (that.isFile == 1) {
                        //là hiển thị file báo cáo

                    } else if (that.isFile == 2) {
                        // là hiển thị dạng file tài liệu
                        that._renderFileTaiLieu();
                    } else if (that.isFile == 4) {
                        // là hiển thị dạng báo cáo thuyết minh
                        that._renderThuyetMinh();
                    } else if (that.isShowTotal == "true") {
                        that._renderDataPivot();
                    } else {
                        that._renderData();
                    }

                    if (that.isFile == 4) {
                        that.$("#headerReport").hide();
                        that.$("#footerReport").hide();
                    } else if (that.isFile == 2) {
                        that.$("#headerReport").show();
                        that.$("#footerReport").show();
                    } else {

                    }

                    //if (that.isThuyetMinh) {
                    //    that._renderThuyetMinh();
                    //} else if (that.isShowTotal == "true") {
                    //    that._renderDataPivot();
                    //} else {
                    //    that._renderData();
                    //}

                    $(".reportName").hide()
                }
            });
        },

        _renderFileTaiLieu: function () {
            var that = this;
            //var data = this.data[0];
            //var dataProcessInfo = this.data[0].ProcessInfo;
            var reportParams = this._getParams();

            $.ajax({
                url: reportUrls.getDataEform,
                data: reportParams,
                beforeSend: function () {
                    showProgress();
                },
                success: function (result) {
                    if (result.length > 0) {
                        var dataProcessInfo = JSON.parse(result[0].ProcessInfo);
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

                                    // exit merged 
                                    if (dataProcessInfo.extra.mergedCells.length > 0) {
                                        if (dataProcessInfo.headerNested) {
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
                                        else if (dataProcessInfo.extra.mergedCells.length) {
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

                                        }
                                    } else {
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
                                        }
                                        else if (dataProcessInfo.headerNested) {
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
                                    }

                                    if (i == dataProcessInfo.extra.headerSetting.length - 1)
                                        if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                            string = string + "<th width='" + width + "' hidden>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                        else string = string + "<th width='" + width + "'>" + dataProcessInfo.extra.headerSetting[i][j] + "</th >";
                                }
                            $('#reportViewer').find('#tblListDocument').find('thead').append("<tr>" + string + "</tr>");
                            $('#reportViewer > #tblListDocument > thead > tr > th').each(function (i, item) {
                                $this = $(item);
                                $this.addClass('htBold');
                                $this.addClass('htCenter');
                            });
                            //hiepns
                        }
                        //var countTr = $('#reportViewer').find('#tblListDocument').find('thead').find('tr').length;
                        //if (countTr > 2) {
                        //    $('#reportViewer > #tblListDocument > thead > tr:last').remove();
                        //}
                        $('#reportViewerMax .HeaderContent .HeaderContentBody').html(obj.FormHeader);
                        $('#reportViewerMax .FooterContent .FooterContentBody').html(obj.FormFooter);


                        //render data
                        var dataProcessInfoNew = JSON.parse(result[0].ProcessInfo);
                        var dataKeyNew = _.keys(dataProcessInfoNew.header);
                        var noteSS = JSON.parse(result[0].Note);
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
                                var data = $.tmpl(template, dataProcessInfoNew.data[i]);
                                $('#reportViewer').find('#tblListDocument').find('tbody').append(data);
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
                                var data = $.tmpl(template, dataProcessInfoNew.data[i]);
                                $('#reportViewer').find('#tblListDocument').find('tbody').append(data);
                            }
                        }
                    }
                },
                error: function (xhr) { },
                complete: function () { }
            });
            /*
            $.ajax({
                url: reportUrls.getDataEform,
                data: reportParams,
                beforeSend: function () {
                    showProgress();
                },
                success: function (result) {
                    if (result.length > 0) {
                        var dataProcessInfo = JSON.parse(result[0].ProcessInfo);
                        var dataKey = _.keys(dataProcessInfo.header);
                        var noteSS = JSON.parse(result[0].Note);
                        var dataSS = dataProcessInfo.data;
                        //so sanh 2 array lay du lieu do vao 

                        //var dataConvert = _.map(dataSS, function (element) {
                        //    var findNoteSS = _.findWhere(noteSS, { madinhdanh: element.madinhdanh });
                        //    var findDataSS = _.findWhere(dataSS, { madinhdanh: element.madinhdanh });
                        //    findNoteSS.dvt = findDataSS.dvt;
                        //    return _.extend(element, findNoteSS);
                        //});

                        dataProcessInfo.data = noteSS;

                        //for (var i = 0 ; i < dataProcessInfo.data.length ; i++) {
                        //    dataProcessInfo.data[i].pos = i;
                        //} 
                        //hiepns

                        var items = _.map(dataKey, function (item) {
                            return item.split("!!", 1);
                        });

                        var string;
                        if (dataProcessInfo.classCells) {
                            for (var i = 0 ; i < dataProcessInfo.data.length ; i++) {
                                string = "";
                                for (var j = 0; j < items.length; j++) {
                                    if (dataProcessInfo.extra.columnSetting[dataKey[j]]["Hidden"])
                                        string = string + "<td class='" + dataProcessInfo.classCells[i][j] + "' hidden >${" + items[j] + "}</td>";
                                    else string = string + "<td>${" + items[j] + "}</td>";
                                }
                                var template = "<tr>" + string + "</tr>";
                                var data = $.tmpl(template, dataProcessInfo.data[i]);
                                $('#reportViewer').find('#tblListDocument').find('tbody').append(data);
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
                                $('#reportViewer').find('#tblListDocument').find('tbody').append(data);
                            }
                        }
                    }
                },
                error: function (xhr) { },
                complete: function () { }
            }); */

        },

        _renderDataPivot: function () {
            var that = this;
            $("#reportViewer").attr('style', "width: 100%; height: 100%");
            var configReport = that.pivotConfig;
            configReport = configReport ? JSON.parse(configReport) : {};
            configReport["dataSource"] = { data: that.data}
            var pivot = new WebDataRocks({
                container: "#reportViewer",
                toolbar: false,
                customizeCell: customizeCellFunction,
                report: configReport
            });
            pivot.expandAllData();

            setTimeout(function () {
                pivot.exportTo("html", {}, function (a, b, c) { });
                
            }, 500)
        },
        _renderThuyetMinh: function () {
            if (this.data.Data.length == 0) {
                $("#notData").css("display", "block");
            } else {
                var data = this.data.Data[0].Note;
                if (data) {
                    $("#reportViewer").html(data)
                }
            }    
        },

        _renderData: function () {
            var documentListElement = this.$("#reportViewer").find('tbody'),
                  that = this,
                  idx = DEFAULT_PAGESIZE * (this.currentPage - 1);

            documentListElement.empty();

            var documents = this.data;

            if (!documents || documents.length <= 0) {
                var colspan = this.displaySettings.length;
                documentListElement
                    .append("<tr><td colspan=" + colspan + ">" + "Không có dữ liệu báo cáo" + "</td></tr>");
                hideProgress();
                return;
            }

            var doc = documents[0];
            this.hasViewDocument = doc["Compendium"] != null && doc["DocumentCopyId"] != null;
            if (!this.hasViewDocument) {
                console.log("Câu truy vấn cần lấy ra Compendium và DocumentCopyId để xem văn bản từ báo cáo.");
            }

            this.documentTemplate = this._buildDocumentTemplate();
            documents.forEach(function (document) {
            //    document.Stt = ++idx;
                document.IsBold = document.IsBold ? true : false;
                document.IsChild = document.IsChild ? true : false;
                documentListElement.append($.tmpl(that.documentTemplate, document));
            });

            that.fixHeader();
            hideProgress();
        },

        _formatData: function (data, type) {
            switch (type) {
                case "1":
                    if ($.isNumber(data)) {
                        data = addCommas(data)
                    }
                    break;
                case "2":
                    if ($.isNumber(data)) {
                        data.toFixed(2)
                        data = addCommas(data)
                    }
                    break;
                default:
            }
            function addCommas(nStr) {
                nStr += '';
                x = nStr.split('.');
                x1 = x[0];
                x2 = x.length > 1 ? '.' + x[1] : '';
                var rgx = /(\d+)(\d{3})/;
                while (rgx.test(x1)) {
                    x1 = x1.replace(rgx, '$1' + ',' + '$2');
                }
                return x1 + x2;
            }
        },

        _renderPager: function (isGroup, trGroup) {
            var total = isGroup ? this.totalGroup : this.total;
            var pageSize = DEFAULT_PAGESIZE;
            var pager = this._buildPager(total, pageSize);
            if (pager === "") { return; }

            var container = this.$("#reportViewer");
            if (isGroup) {
                container = $("<tr><td colspan='" + this.displaySettings.length + "'></td></tr>");
                container.attr("group-value", this.GroupValue);
                trGroup.after(container);
                container = container.find('td');
                this.isGrouping = true;
            }

            //$(".pager").parents("tr").remove();
            container.append(pager);

            container.find(".pagination li[page='" + this.currentPage + "'] > a").addClass("current");
        },

        _showTotal: function () {
            if (this.isShowTotal == "false") {
                $("#showTotalDocument").hide();
                return;
            }
            //$("#showTotalDocument").show();
            //this.$("#total").text(this.total);
        },

        _getParams: function () {

            return {
                ReportId: this.currentId,
                Time: "TuyChon",
                FromDate: $("#from").datepicker("getDate").startOf("day").toServerString(),
                ToDate: $("#to").datepicker("getDate").endOf("day").toServerString(),
                // Test
                //FromDate: "2017-01-01T00:00:00",
                //ToDate: "2018-03-01T00:00:00",
                TreeGroupValue: this.treeGroupValue,
                TreeGroupName: this.treeGroupBy,
                GroupId: this.groupId,
                sortBy: this.sortBy,
                isDesc: this.isDesc,
                Page: this.currentPage,
                PageSize: DEFAULT_PAGESIZE,
                TimeKey: this.TimeKey
            };
        },

        _enableResize: function () {
            /// <summary>
            /// Resize các cột
            /// </summary>
            var that = this;
            this.$el.find('#tblListDocument').grid({
                isUsingCustomScroll: false,
                isResizeColumn: true,
                isFixHeightContent: true,
                isAddHoverRow: false,
                isUseCookie: true,
                isRenderPanelGrid: false,
                onresizefinish: function () {
                    var data = {};
                    var columnNoWidth = [];
                    var totalWidth = 0;
                    var tableHeader = that.$el.find('.grid-header th');
                    var tableHeaderCol = that.$el.find('.grid-header colgroup col');
                    var tableContent = that.$el.find('.grid-content table');

                    tableHeaderCol.each(function (idx, item) {
                        var column = tableHeader.eq(idx);
                        if ($(item).width() == 0) {
                            columnNoWidth.push(column.attr('data-column'));
                        } else {
                            totalWidth += $(item).width();
                        }
                        data[column.attr('data-column')] = $(item).width();
                    });

                    //Chiều rông mới của danhh sách văn bản
                    var newTableContentWidth = tableContent.width();

                    if (columnNoWidth.length > 0) {
                        var allWidth = newTableContentWidth - totalWidth;
                        var widthCol = allWidth / columnNoWidth.length;
                        for (var g = 0; g < columnNoWidth.length; g++) {
                            data[columnNoWidth[g]] = widthCol;
                        }
                    }
                }
            });
        },

        _buildDocumentTemplate: function () {
            // Lấy ra danh sách các cột theo cấu hình (trừ những cột được thêm mặc định như Stt, Màu)
            var newcolumnSettings = _.filter(this.displaySettings, function (item) {
                return (item.columnName != "Stt") && (item.columnName != "View") && (item.columnName != "Color");
            });
            var result = "<tr {{if IsBold}}style='font-weight: bold;'{{/if}}>";

            // Stt
            //result += "<td class='text-center'>{{if IsBold || IsChild}}{{else}}${Stt}{{/if}}</td>";
            var isF = true;
            // Hiển thị các cột theo cấu hình
            $.each(newcolumnSettings, function (index, setting) {
                var justify = setting.justify ? setting.justify : "left";
                if (isF) {
                    result += "<td style='text-align:" + justify + ";vertical-align: middle;border-color:#000000; white-space:nowrap' data-column='" + setting.columnName + "'>{{if IsChild}} -- {{/if}} ${" + setting.columnName + "}</td>";
                } else {
                    result += "<td style='text-align:" + justify + ";vertical-align: middle;border-color:#000000; white-space:nowrap' data-column='" + setting.columnName + "'>${" + setting.columnName + "}</td>";
                }
                isF = false;
            });

            if (this.hasViewDocument) {
                result += "<td><a href='#' class='btnViewDocument' data-id='${DocumentCopyId}' data-title='${Compendium}'>Xem</a></td>";
            } else {
                //result += "<td></td>";
            }

            return result;
        },

        //#region Events

        _showGroup: function (e) {
            var that = this;
            var target = $(e.target).closest("li");
            var groupBy = target.attr("value");
            var columnName = target.data("group");
            this.ColumnGroupName = columnName;
            if (groupBy === this.defaultGroup.columnName) {
                this._showReport();
                return;
            }

            var group = _.find(this.groupSettings, function (group) {
                return group.groupBy === groupBy;
            });

            if (!group) {
                return;
            }

            this.$("#groupName").text(group.displayName);
            this.$(".pager").remove();

            var param = this._getParams();
            param.GroupBy = groupBy;
            this.GroupBy = groupBy;
            $.ajax({
                url: reportUrls.getGroups,
                beforeSend: function () {
                    showProgress();
                },
                data: param,
                success: function (result) {
                    if (!result) {

                    }

                    that.groups = result;
                    that._renderGroups();
                }
            });
        },

        _bindGroupData: function (e) {
            var that = this;
            var target = $(e.target).closest("a");
            var trGroup = target.parents("tr");
            var groupValue = target.attr("value");
            var loaded = trGroup.attr("loaded");
            if (loaded === "true") {
                trGroup.siblings("tr[group-value='" + groupValue + "']").toggle();
                return;
            }

            this.currentPage = 1;
            trGroup.attr("loaded", "true");

            this.GroupValue = groupValue;
            this._getGroupData();
        },

        _changeDateCondition: function (e) {
            var that = this;
            var target = $(e.target);
            var type = target.attr("type");
            that.type = type;
            that.$("#dateConditionCurrent").text(target.text());

            that._changeDate();
        },

        _changeDate: function (e) {
            this._formatDateAndShowDatepicker(this.type);
        },

        _gotoPage: function (e) {
            var target = $(e.target).closest("a");
            var page = target.attr("value");
            if (target.is(".current")) {
                return;
            }

            target.siblings().removeClass("current");
            target.addClass("current");

            this.currentPage = page;

            if (this.isGrouping) {
                var groupValue = target.parents("tr").attr("group-value");
                this.GroupValue = groupValue;
                this._getGroupData();

            } else {
                this._renderReport();
            }
        },

        _exportGroup: function (e) {
            var groupBy = "";
            if (this.ColumnGroupName && this.ColumnGroupName != "default") {
                groupBy = this.ColumnGroupName;
            }
            var target = $(e.target).closest("span");
            var value = target.attr("value");
            var exporttype = target.attr("type-export");
            var data = this._getParams();
            var exportUrl = reportUrls.export + data.ReportId;
            exportUrl += '&time=' + data.Time;
            exportUrl += '&fromDate=' + data.FromDate;
            exportUrl += '&toDate=' + data.ToDate;
            exportUrl += '&treeGroupValue=' + data.TreeGroupValue;
            exportUrl += '&GroupBy=' + groupBy;
            exportUrl += '&GroupId=' + data.GroupId;
            exportUrl += '&GroupValue=' + value;
            exportUrl += '&ExportType=' + exporttype;
            exportUrl += '&SortBy=' + data.sortBy;
            exportUrl += '&IsDesc=' + data.isDesc;

            window.open(exportUrl);
        },

        _export: function (e) {
            var that = this;
            var groupBy = "";
            if (this.ColumnGroupName && this.ColumnGroupName != "default") {
                groupBy = this.ColumnGroupName;
            }
            var target = $(e.target).closest("a");
            var exporttype = target.attr("export-type");
            if (exporttype == "25") {
                that._exportHtml();
                return;
            }
            if (exporttype == "5") {
                that._exportPDF();
                return;
            }
            var data = this._getParams();
            var exportUrl = reportUrls.export + data.ReportId;
            exportUrl += '&time=' + data.Time;
            exportUrl += '&fromDate=' + data.FromDate;
            exportUrl += '&toDate=' + data.ToDate;
            exportUrl += '&treeGroupValue=' + data.TreeGroupValue;
            exportUrl += '&GroupBy=' + groupBy;
            exportUrl += '&GroupId=' + data.GroupId;
            exportUrl += '&ExportType=' + exporttype;
            exportUrl += '&SortBy=' + data.sortBy;
            exportUrl += '&IsDesc=' + data.isDesc;

            window.open(exportUrl);
        },

        _getHtml: function () {
            console.log("oke");
            var that = this;

            var $canvases = $("canvas").each(function () {
                var $canvas = $(this);
                var $parent = $canvas.parent();
                var dataURL = $canvas.get(0).toDataURL();

                var img = $('<img >'); //Equivalent: $(document.createElement('img'))
                img.attr('src', dataURL);
                $parent.append(img)
                //debugger
                $canvas.remove();
            })
            var document = $("#reportViewer").html();

            var content = '<!DOCTYPE html>' + document;

            var converted = htmlDocx.asBlob(content, { orientation: "portrait" });


            var name = this.report.reportName;
            settings = {
                content: document,
                name: that.currentId
            }
            $.ajax({
                type: "post",
                url: "/ReportViewer/ExportDocxHtml",
                data: settings,
                success: function (result) {
                    if (result) {
                        var nameFile = window.tenbaocao+"-"+ window.kybaocao;
                        $(".nameFile").attr("data-name", result);
                        $(".nameFile").text(nameFile);
                        $(".nameFile").css("color", "blue");
                        $(".downFile").attr("href", result).attr("target", "_blank")[0];
                        window.url_file = result;
                        console.log("result :" + result);
                        //$("<a>").attr("href", result).attr("target", "_blank")[0].click();
                        // window.open(result, "_blank");
                    }
                }
            });
        },

        _exportHtml: function () {
            var that = this;

            var $canvases = $("canvas").each(function () {
                var $canvas = $(this);
                var $parent = $canvas.parent();
                var dataURL = $canvas.get(0).toDataURL();

                var img = $('<img >'); //Equivalent: $(document.createElement('img'))
                img.attr('src', dataURL);
                $parent.append(img)
                //debugger
                $canvas.remove();
            })

            $("#reportDataContent").append("<div id='fullFile' style='display:block'></div>");
            $("#fullFile").append("<div id='headerFullFile' style='color:black'></div>");
            var one = $("#reportDataContent").find($("#canvas_content")).html();
            $("#headerFullFile").append(one);
            var two = $("#reportDataContent").find($("section")[2]).html();
            var three = $("#reportDataContent").find($("section")[3]).html();
            $("#fullFile").append(two, three);
          

            var document = $("#fullFile").html();

            var content = '<!DOCTYPE html>' + document ;

            var converted = htmlDocx.asBlob(content, { orientation: "portrait" });

            
            var name = this.report.reportName;
            settings = {
                content: document,
                name: that.currentId
            }
            $.ajax({
                type: "post",
                url: "/ReportViewer/ExportDocxHtml",
                data: settings,
                success: function (result) {
                    if (result) {
                        var nameFile = result.substring(6);
                        $(".nameFile").text(nameFile);
                        $(".downFile").attr("href", result).attr("target", "_blank")[0];
                        console.log("result : " + result);
                        $("<a>").attr("href", result).attr("target", "_blank")[0].click();
                        // window.open(result, "_blank");
                    }
                }
            });
        },

        _exportPDF: function myfunction() {
            var that = this;
            var document = $("#reportViewer").html();
            var name = this.report.reportName
            settings = {
                content: document,
                name: name + that.currentId
            }
            $.ajax({
                type: "post",
                url: "/ReportViewer/ExportPdfHtml",
                data: settings,
                success: function (result) {
                    if (result) {

                        $("<a>").attr("href", result).attr("target", "_blank")[0].click();
                        // window.open(result, "_blank");
                    }
                }
            });
        },

        _readToText: function () {
            var document = $("#footerReport").html();
            var name = this.report.reportName
            settings = {
                content: document,
                name: name
            }
            $.ajax({
                type: "Post",
                url: "/VoiceToText/ReadBCTH",
                data: settings,
                success: function (result) {
                    if (result) {
                            var audio = $("#playWav");
                            $("#srcAudio").attr("src", result.path);

                            audio[0].pause();
                            audio[0].load();
                            audio[0].oncanplaythrough = audio[0].play();
                        // window.open(result, "_blank");
                    }
                }
            });
        },

        _viewDocument: function (e) {
            var target = $(e.target);
            var documentCopyId = target.attr("data-id");
            var compendium = target.attr("data-title");

            if (top && isFunc(top.openDocument)) {
                top.openDocument(documentCopyId, compendium);
            }
        },

        //#endregion

        //#region Groups

        _renderGroups: function () {
            hideProgress();
            this.$("#tblListDocument tbody").empty();
            var colspan = this.displaySettings.length;
            var groupTemplate = "<tr class='report-group' group='${GroupValue}'><td colspan='" + colspan + "'><a href='#' value='${GroupValue}'>${GroupName} (${Count})</a> \
                            <span class='pull-right' type-export='5' value='${GroupValue}'><i class='icon icon-file-pdf'></i></span> \
                            <span class='pull-right' type-export='15' value='${GroupValue}'><i class='icon icon-file-excel'></i></span> \
                            <span class='pull-right' type-export='12' value='${GroupValue}'><i class='icon icon-file-word'></i></span> \
                            <span class='pull-right' type-export='5' value='${GroupValue}'>Tải về</span> </td></tr>";
            var groupElements = $.tmpl(groupTemplate, this.groups);
            groupElements.each(function (i, group) {
                $(group).attr("group", safeCharanter($(group).attr("group")));
            });
            this.$("#tblListDocument tbody").append(groupElements);
        },

        _renderGroupData: function (groupValue, data) {
            var that = this;
            hideProgress();
            var trGroup = this.$(".report-group[group='" + groupValue + "']");
            if (trGroup.length === 0) {
                return;
            }

            trGroup.siblings("tr[group-value='" + groupValue + "']").remove();

            this._renderPager(true, trGroup);

            var idx = DEFAULT_PAGESIZE * (this.currentPage - 1);
            _.each(data, function (d) {
                d.Stt = ++idx;
            });

            var newRows = $.tmpl(this.documentTemplate, data);
            newRows.each(function (i, row) {
                $(row).attr("group-value", groupValue);
            });

            trGroup.after(newRows);
            that.fixHeader();
        },

        //#endregion

        //#region Data

        _getReportTrees: function (complete) {
            $.ajax({
                url: reportUrls.gets,
                success: function (result) {
                    if (isFunc(complete)) {
                        complete(result);
                    }
                }
            });
        },

        _getReportsByParent: function (parentId, complete) {
            $.ajax({
                url: reportUrls.getsByParent,
                data: { id: parentId },
                success: function (result) {
                    if (isFunc(complete)) {
                        complete(result);
                    }
                }
            });
        },

        //#endregion

        //#region Private

        _formatDateAndShowDatepicker: function (type, reportName) {
            var that = this;
            var from, to;
            $("select.statistics-time").addClass("d-none");
            $("#dateSelect").addClass("d-none");;
            $('#yearSelect').removeClass("d-none");
            this.yearData = $('#yearSelect').val();
            switch (type) {
                case "TrongNgay": // trong ngày
                    $('#yearSelect').addClass("d-none");
                    $('#dateSelect').removeClass("d-none");
                    var dates = $("#dateSelect").val().split("/");
                    
                    if (dates.length = 3) {
                        var d = dates[0] < 10 ? '0' + Number(dates[0]) : dates[0];
                        var m = dates[1];
                        var y = dates[2];
                        that.TimeKey = y + '' + m + '' + d;
                    }
 
                    from = new Date().startOf("date")
                    to = new Date().endOf("date");

                    //$(".reportName").text(reportName + " Ngày " + $("#from").val());
                    TimeCurrent = " Ngày " + $("#from").val()
                    break;
                case "TrongTuan": // trong tuần
                    $(".statistics-time#weekSelect").removeClass("d-none");
                    from = new Date().startOf("week");
                    to = new Date().endOf("week");
                    var currentWeek = $(".statistics-time#weekSelect").val();
                    that.TimeKey = this.yearData + "" + currentWeek;

                    //if (currentWeek < 10) {
                    //    that.TimeKey = this.yearData + "0" + currentWeek;
                    //} else {
                    //    that.TimeKey = this.yearData + "" + currentWeek;
                    //}
                    break;
                case "TrongThang": // Trong tháng
                    $(".statistics-time#monthSelect").removeClass("d-none");
                    var currentMonth = $(".statistics-time#monthSelect").val();
                    from = new Date().month(currentMonth).startOf("month");
                    to = new Date().month(currentMonth).endOf("month");
                    if (currentMonth < 10) {
                        that.TimeKey = this.yearData + "0" + currentMonth;
                    } else {
                        that.TimeKey = this.yearData + "" + currentMonth;
                    }

                    //$(".reportName").text(reportName + " Tháng " + $("#monthSelect").val());
                    //TimeCurrent = " Tháng " + $("#monthSelect").val();

                    break;
                case "TrongQuy": // Trong quý
                    var quarter = $(".statistics-time#quarterSelect").val();
                    var firstMonthInQuarter = (quarter - 1) * 3 + 1;
                    from = new Date().date(1).month(firstMonthInQuarter).startOf("quarter");
                    to = new Date().date(1).month(firstMonthInQuarter).endOf("quarter");
                    $(".statistics-time#quarterSelect").removeClass("d-none");
                    //$(".reportName").text(reportName + " Quý " + $("#quarterSelect").val());
                    TimeCurrent = " Quý " + $("#quarterSelect").val();
                    that.TimeKey = this.yearData + "" + quarter;
                    break;
                case "NuaNam":
                    $(".statistics-time#halfSelect").removeClass("d-none");
                    var half = $(".statistics-time#halfSelect").val();
                    var firstMonthInHalf = (half - 1) * 6 + 1;
                    from = new Date().date(1).month(firstMonthInHalf).startOf("quarter");
                    to = new Date().date(1).month(firstMonthInHalf).endOf("quarter");
                    that.TimeKey = this.yearData + "" + half;
                    break;
                case "TrongNam": // Trong năm
                    $(".statistics-time#yearSelect").removeClass("d-none");
                    var currentYear = $(".statistics-time#yearSelect").val();
                    from = new Date().year(currentYear).startOf("year");
                    to = new Date().year(currentYear).endOf("year");
                    //$(".reportName").text(reportName + " Năm " + $("#yearSelect").val());
                    TimeCurrent = " Năm " + $("#yearSelect").val();
                    that.TimeKey = this.yearData;
                    break;
                case "ChinThang": // Chín tháng              
                    $(".statistics-time#nineSelect").removeClass("d-none");
                    from = new Date().startOf("week");
                    to = new Date().endOf("week");
                    var currentWeek = $(".statistics-time#nineSelect").val();
                    that.TimeKey = this.yearData + "0" + currentWeek;
                    break;
                case "TuyChon":
                default:
                    from = to = new Date();
                    break;
            }
            window.kybaocao = that.TimeKey;

            from = from.format("dd/MM/yyyy");
            to = to.format("dd/MM/yyyy");

            $("#from").val(from);
            $("#to").val(to);
        },

        _initTime: function () {
            bindDatepicker();
            bindYearSelect();
            bindDefaultSelect();
            this.type = "TrongThang";
            this._formatDateAndShowDatepicker(this.type, "Chưa Chọn báo cáo");
        },

        _buildPager: function (total, pageSize) {
            var that = this;
            var result = $(this.pagingTemplate);
            var pages = Math.floor(total / pageSize);

            if (total % pageSize != 0) {
                pages++;
            }

            if (pages === 1 || pages === 0) {
                return "";
            }


            var currentPage = Number(that.currentPage);
            var pageRange = 2;
            var totalPage = pages;

            var rangeStart = currentPage - pageRange;
            var rangeEnd = currentPage + pageRange;

            if (rangeEnd > totalPage) {
                rangeEnd = totalPage;
                rangeStart = totalPage - pageRange * 2;
                rangeStart = rangeStart < 1 ? 1 : rangeStart;
            }

            if (rangeStart <= 1) {
                rangeStart = 1;
                rangeEnd = Math.min(pageRange * 2 + 1, totalPage);
            }

            var prevNumber = currentPage == 1 ? 1 : currentPage - 1;
            var nextNumber = currentPage == totalPage ? totalPage : currentPage + 1;

            var pagePrev = $('<li class=""><a href="#" value="' + prevNumber + '">&lt;</a></li>');
            var pageFirst = $('<li class="btn-prev"><a href="#" value="' + 1 + '">&lt;&lt;</a></li>');
            if (currentPage != 1) {
                pagePrev.click(function (e) {
                    that._gotoPage(e);
                });
                pageFirst.click(function (e) {
                    that._gotoPage(e);
                });
            }
            var pageNext = $('<li class=""><a href="#" value="' + nextNumber + '">&gt;</a></li>');
            var pageLast = $('<li class="btn-next"><a href="#" value="' + totalPage + '">&gt;&gt;</a></li>');
            if (currentPage != totalPage) {
                pageNext.click(function (e) {
                    that._gotoPage(e);
                });
                pageLast.click(function (e) {
                    that._gotoPage(e);
                });
            }
            var pageEllipsisBefore = $('<li ><span href="#" value="' + nextNumber + '">...</span></li>');
            var pageEllipsisAfter = $('<li ><span href="#" value="' + nextNumber + '">...</span></li>');

            result.find("ul").append(pageFirst);
            result.find("ul").append(pagePrev);
            if (rangeStart != 1) {
                result.find("ul").append(pageEllipsisBefore);
            }
            for (var page = rangeStart; page < rangeEnd + 1; page++) {
                var pageItem = $('<li page="' + page + '"><a href="#" value="' + page + '">' + page + '</a></li>');
                pageItem.click(function (e) {
                    that._gotoPage(e);
                });
                result.find("ul").append(pageItem);
            }

            if (rangeEnd != totalPage) {
                result.find("ul").append(pageEllipsisAfter);
            }
            result.find("ul").append(pageNext);
            result.find("ul").append(pageLast);
            result.find(".go-to-page").find("a").click(function (e) {
                var target = $(e.target).closest("a");
                var value = result.find(".go-to-page").find("input").val();
                if (!value || Number(value) == that.currentPage) {
                    return;
                }
                target.attr("value", value);
                that._gotoPage(e);
            })
            result.find(".go-to-page").find("input").keypress(function (event) {
                var key = window.event ? event.keyCode : event.which;
                if (key == 13) {
                    result.find(".go-to-page").find("a").click();
                };
                var pageCurr = result.find(".go-to-page").find("input").val() + this.value;
                if (pageCurr > totalPage) {
                    return false;
                }
                if (event.keyCode === 8 || event.keyCode === 46) {
                    return true;
                } else if (key < 48 || key > 57) {
                    return false;
                } else {
                    return true;
                }
            })
            return result;
        },

        _getGroupData: function () {
            var that = this;
            var param = this._getParams();
            param.GroupBy = this.GroupBy;
            param.GroupValue = this.GroupValue;

            $.ajax({
                url: reportUrls.getGroupData,
                beforeSend: function () {
                    showProgress();
                },
                data: param,
                success: function (result) {
                    if (!result) {

                    }

                    that.totalGroup = result.total;
                    that._renderGroupData(safeCharanter(param.GroupValue), result.data);
                }
            });
        }

        //#endregion
    });

    //#region Grid Header

    ///Model tao header cho danh sach van ban
    var gridHeaderModel = Backbone.Model.extend({
        defaults: {
            text: '',
            value: '',
            selected: false,
            isDesc: true,
            enableSort: false,
            callback: null
        },
        initialize: function () { }
    });

    ///Tao header cho danh sach van ban
    var gridHeaderCollection = Backbone.Collection.extend({
        model: gridHeaderModel,
        initialize: function () { }
    });

    /// <summary>Đối tượng View thể hiện header danh sách văn bản</summary>
    var GridHeader = Backbone.View.extend({
        tagName: "tr",

        initialize: function (options) {
            if (!(options.model instanceof gridHeaderCollection)) {
                options.model = new gridHeaderCollection(options.model);
            }

            this.model = options.model;
            this.parent = options.parent;
            this.render();

            return this;
        },

        render: function () {
            var that = this;
            this.isSortting = false;
            this.itemSelected = null;
            this.model.each(function (gridModel) {
                var item = new GridHeaderItem({
                    model: gridModel,
                    parent: that,
                    callback: function (option) {
                        that.isSortting = false;
                        if (isFunc(gridModel.get("callback"))) {
                            gridModel.get("callback")(option);
                        }
                    }
                });
                that.$el.append(item.$el);
            });
        },

        removeSelected: function () {
            this.$el.find('a').removeClass('asc desc');
        }
    });

    /// <summary>Đối tượng View thể hiện item cho  header danh sách văn bản</summary>
    var GridHeaderItem = Backbone.View.extend({
        tagName: "th",

        events: {
            'click': 'selected'
        },

        initialize: function (options) {
            this.model = options.model;
            this.parent = options.parent;
            this.callback = options.callback;
            this.render();
        },

        render: function () {
            ///<summary>
            /// Render ra giao diện
            ///</summary>

            this.$el.attr('data-column', this.model.get('value'));
            //var justify = this.model.get('justify') ? this.model.get('justify') : "left";
            var justify = "center";
            var style = "vertical-align: middle;font-weight:bold;text-align:" + justify;
            this.$el.attr("style", style);
            if (this.model.get('enableSort') == false) {
                if (this.model.get('value') === 'STT') {
                    this.$el.append('<div class="icon-color document-color"></div>');
                } else if (this.model.get('value') === 'Color') {
                    this.$el.append('<div class="icon-flag"></div>');
                } else {
                    this.$el.append(this.model.get('text'));
                }
            } else {
                this.$el.append('<a href="#" data-column="' + this.model.get('value') + '" class="sort">' + this.model.get('text') + '</a>');
            }
        },

        selected: function (e) {
            ///<summary>
            /// Select để sắp xếp
            ///</summary>

            destroyEvent(e);

            if (this.parent.isSortting || this.model.get('enableSort') == false) {
                return;
            }

            this.parent.isSortting = true;

            if (isFunc(this.callback)) {
                var $a = this.$el.find('a.sort');
                if (this.model.get('isDesc') == true) {
                    this.model.set('isDesc', false);
                    $a.removeClass('asc desc').addClass('asc');
                } else {
                    this.model.set('isDesc', true);
                    $a.removeClass('asc desc').addClass('desc');
                }

                this.callback(this.model.toJSON());
            }
        }
    });

    //#endregion

    window.eReport = eReport;

    function destroyEvent(e) {
        if (e) {
            if (e.preventDefault) {
                e.preventDefault();
            } else {
                e.returnValue = false;
            }

            if (e.stopPropagation) {
                e.stopPropagation();
            } else {
                e.cancelBubble = true;
            }
        }
    }

    function renderGridHeader(settings, container) {
        /// <summary>Render ra header cho danh sách document theo cấu hình</summary>
        /// <param name="settings" type="object">Cấu hình cột</param>
        /// <param name="container" type="object">element jquery</param>
        container.find("colgroup").empty();
        $.each(settings, function (index, setting) {
            if (setting.width && setting.width > 0) {
                container.find("colgroup").append('<col style="width: ' + setting.width + 'px"/>');
            } else {
                container.find("colgroup").append('<col/>');
            }
        });
    }

    function bindDatepicker() {

        //$.datepicker.setDefaults($.datepicker.regional["vi-VN"]);

        $("#from").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 2,
            dateFormat: "d/mm/yy",
            onClose: function (selectedDate) {
                $("#to").datepicker("option", "minDate", selectedDate);
            }
        });
        $("#to").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 2,
            dateFormat: "d/mm/yy",
            onClose: function (selectedDate) {
                $("#from").datepicker("option", "maxDate", selectedDate);
            }
        });
        $("#dateSelect").datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 2,
            dateFormat: "dd/mm/yy",
            onClose: function (selectedDate) {
                $("#from").datepicker("option", "maxDate", selectedDate);
            }
        });
    }

    function bindYearSelect() {
        var currentYear = new Date().year();
        var yearSelect = $("#yearSelect");
        var from = currentYear - 20;
        yearSelect.append("<option value='-1' selected='selected'>Tùy chọn năm</option>")
        for (var i = from ; i <= currentYear; i++) {   
            yearSelect.append($("<option>").val(i).text("Năm " + i));
        }
    }

    function bindDefaultSelect() {
        var currentMonth = new Date().month();
        var currentQuarter = new Date().quarter();
        var currentYear = new Date().year();

        $("#monthSelect option[value='" + currentMonth + "']").attr("selected", "selected");
        $("#quarterSelect option[value='" + currentQuarter + "']").attr("selected", "selected");
        //$("#yearSelect option[value='" + currentYear + "']").attr("selected", "selected");
    }

    function safeCharanter(value) {
        return value.replace(/[!"#$%&'()*+,.\/:;<=>?@[\\\]^`{|}~]/g, "");
    }

    function showProgress() {
        $(".status").show();
    }

    function hideProgress() {
        $(".status").hide();
    }

    function customizeCellFunction(cellBuilder, cellData) {
        console.log(cellData.type);
        console.log(cellData);

        if (cellData.type == "header") {
            cellBuilder.addClass("wraptext-header");
        }
    }
})();