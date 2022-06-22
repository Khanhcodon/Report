/// <reference path="eReport.js" />

(function () {

    //#region Private Fields

    var regexUtcDate = /\d{4}-[0-1]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-6]\d$/;
    var DEFAULT_PAGESIZE = 50;

    var reportUrls = {
        gets: "/ReportViewer/GetAll",
        getsByParent: "/ReportViewer/GetReportByParent",
        getReport: "/ReportViewer/GetReport",
        getReportData: "/ReportViewer/GetReportData",
        getGroups: "/ReportViewer/GetGroups",
        getGroupData: "/ReportViewer/GetGroupData",
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
            "dblclick_toggle" : true
        },
        types: {
            "valid_children": ["root"],
            "types": {
                "root": {
                    "hover_node": false,
                    "dblclick_toggle" : true,
                    "select_node": function () { return false; }
                }
            }
        }
    };

    //#endregion

    var eReport = Backbone.View.extend({
        el: "#eGovReport",
        template: "#treeTemplate",
        reportTemplate: '<table class="table table-hover table-main persist-area" id="tblListDocument"><colgroup></colgroup><thead></thead><tbody></tbody></table>',
        pagingTemplate: '<nav aria-label="Page navigation" class="pager"><div class="col-md-10"><ul class="pagination pull-right"></ul></div>\
                        <div class="col-md-6"><div style="width: 100px;"><div class="input-group go-to-page" style="margin: 21px 0px;"><input type="text" class="form-control input-sm"/><span class="input-group-btn"><a class="btn btn-default btn-sm" type="button">Đi đến</a></span></div></div></div></nav>',
        reportTreeEl: $("#tree ul.report"),
        defaultGroup: { displayName: "Không hiển thị nhóm", columnName: "default", groupBy: "default" },
        events: {
            "click #reportGroups li": "_showGroup",
            "click .report-group a": "_bindGroupData",
            "click .report-group span": "_exportGroup",
            "click #dateConditions li a": "_changeDateCondition",
            "change .statistics-time select": "_changeDate",
            "click #btnReport": "_renderReport",
            "click #btnExports li a": "_export",
            "click .btnViewDocument": "_viewDocument"
        },

        initialize: function () {
            $.jstree.defaults.core.dblclick_toggle = true;
            this.render();
        },

        render: function () {
            var that = this;
            this._layout();
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
            var isLoadedChildren, parentId, that = this;
            this.reportTreeEl.append($.tmpl($(this.template), this.model));
            $("#tree").jstree(objDefineTree).bind('open_node.jstree', function (event, data) {
                destroyEvent(event);
                isLoadedChildren = data.inst._get_children(data.rslt.obj).length > 0 && data.rslt.obj.attr("id") != 0;
                if (isLoadedChildren) {
                    return;
                }

                data.rslt.obj.children('a:first').addClass('jstree-loading');
                parentId = parseInt(data.rslt.obj.attr("id"));

                that._getReportsByParent(parentId, function (result) {
                    data.inst.unlock();
                    data.rslt.obj.children('a:first').removeClass('jstree-loading');
                    if (result) {
                        var reports = _.sortBy(result, function (item) { return item.order; });
                        if (data.rslt.obj.children('ul:first').length === 0) {
                            $('<ul></ul>').appendTo(data.rslt.obj);
                        } else {
                            data.rslt.obj.children('ul:first').empty();
                        }
                        $(that.template).tmpl(reports).appendTo(data.rslt.obj.children('ul:first'));
                        data.rslt.obj.children('ul:first').children('li:last-child').addClass('jstree-last');
                    }
                });
            }).bind('select_node.jstree', function (event, data) {
                destroyEvent(event);
                var reportId = data.rslt.obj.attr("id");
                that.currentId = reportId;
                that.currentPage = 1;
                that.total = 0;
                that.currentPage = 1;
                that.sortBy = "";
                that.GroupBy = "default";
                that.treeGroupBy = data.rslt.obj.find("a").attr("data-groupname");
                that.treeGroupValue = data.rslt.obj.find("a").attr("data-group-value");
                that.groupId = data.rslt.obj.find("a").attr("group-id");
                that.isShowTotal = data.rslt.obj.find("a").attr("data-is-show-total");
                that._renderReport();
            }).bind("dblclick.jstree", function (event) {
                var target = $(event.target).closest("li");
                var expandBtn = target.find(".jstree-icon").first();
                expandBtn.click();
            });
        },

        //#endregion

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

                    that.report = result;

                    that.displaySettings = result.displaySettings;
                    that._renderShowColumn(that.displaySettings);

                    that.displaySettings.unshift({ displayName: "Stt", columnName: "Stt", width: 35 });
                    that.displaySettings.push({ displayName: "Xem", columnName: "View", width: 50 });

                    that.groupSettings = result.groupSettings;
                    that.groupSettings.push(that.defaultGroup);

                    that.sortSettings = result.sortSettings;
                    that.total = result.total;
                    that._showReport();
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
            var ul = '<ul class="list-group checked-list-box">  </ul>';
            var $ul = $(ul).append($.tmpl('<li class="list-group-item" value="" data-group="${columnName}"><a href="#">${displayName}</a></li>', listColumn));
            this.$("#btnShowColumn").html($ul);
            $('#keepOpenShowColumn').on({
                "click": function (event) {
                    if ($(event.target).closest('.dropdown-toggle').length) {
                        $(this).data('closable', true);
                    } else {
                        $(this).data('closable', false);
                    }
                },
                "hide.bs.dropdown": function (event) {
                    hide = $(this).data('closable');
                    $(this).data('closable', true);
                    return hide;
                }
            });
            //$('#keepOpenShowColumn').on({
            //    "shown.bs.dropdown": function () { this.closable = true; },
            //    "click": function () { this.closable = false; },
            //    "hide.bs.dropdown": function () { return this.closable; }
            //});
            this._renderEventColumn();
        },

        _renderEventColumn: function () {
            var that = this;
            $('.list-group.checked-list-box .list-group-item').each(function () {

                // Settings
                var $widget = $(this),
                    $checkbox = $('<input type="checkbox" class="hidden" checked />'),
                    color = ($widget.data('color') ? $widget.data('color') : "primary"),
                    style = ($widget.data('style') == "button" ? "btn-" : "list-group-item-"),
                    settings = {
                        on: {
                            icon: 'icon-checkmark-circle'
                        },
                        off: {
                            icon: 'icon-radio-unchecked'
                        }
                    };

                $widget.css('cursor', 'pointer')
                $widget.append($checkbox);
              
                // Event Handlers
                $widget.on('click', function () {
                    $checkbox.prop('checked', !$checkbox.is(':checked'));
                    $checkbox.triggerHandler('change');
                    updateDisplay();
                });
                $checkbox.on('change', function () {
                    updateDisplay();
                });


                // Actions
                function updateDisplay() {

                    var isChecked = $checkbox.is(':checked');

                    var columnName = $widget.attr("data-group");
                    that.toggleColumn(columnName, isChecked);

                    // Set the button's state
                    $widget.data('state', (isChecked) ? "on" : "off");

                    // Set the button's icon
                    $widget.find('.state-icon')
                        .attr('class', '')
                        .addClass('state-icon ' + settings[$widget.data('state')].icon);

                    // Update the button's color
                    if (isChecked) {
                        $widget.addClass(style + color + ' active');
                    } else {
                        $widget.removeClass(style + color + ' active');
                    }
                }

                // Initialization
                function init() {

                    if ($widget.data('checked') == true) {
                        $checkbox.prop('checked', !$checkbox.is(':checked'));
                    }

                    updateDisplay();

                    // Inject the icon if applicable
                    if ($widget.find('.state-icon').length == 0) {
                        $widget.prepend('<span class="state-icon ' + settings[$widget.data('state')].icon + '"></span>');
                    }
                }
                init();
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

            this._renderToolbarGroups();
            this._renderHeader();
            this._renderPager();
            this._showTotal();
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
            /// <summary>
            /// Hiển thị header cho danh sách văn bản
            /// </summary>


            // Xử lý colspan
            // <tr> </tr>
           

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
                var displayName =  itm.displayName;
                var displayNames = displayName.split(keyColspanHeader);
                if (displayNames.length == isMultiRowHeader.maxRow) {
                    headerColumns.push({
                        text: displayNames[isMultiRowHeader.maxRow -1],
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

                if (scrollTop > 3 &&(scrollTop < el.height())) {
                    var $tableBodyCell = $('.persist-header th'),
                    $headerCell = $('.persist-header th');
                    $tableBodyCell.each(function (index) {
                        $headerCell.eq(index).width($(this).width());
                    });
                    $header.addClass("floatingHeader");
                    // test check độ cao
                    //console.log("scroll"+scrollTop + "-offset" + offset.top + "-height" + el.height() + "true")
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

            var firstRun = true;
            for (var i = 0; i < maxRow - 1 ; i++) {
                var result = "<tr>";
                if (firstRun) {
                    // Stt
                    result += "<th class='text-center' style='vertical-align: middle' rowspan='" + maxRow + "'>Stt</th>";
                }

                // Hiển thị các cột theo cấu hình
                _.each(colspanArray[i], function (item) {
                    if (item) {
                        result += "<th class='text-center' style='vertical-align: middle' colspan='" + item.colSpan + "' rowspan='" + item.rowSpan + "' data-column='" + item.data + "'>" + item.data + "</th>";
                    }
                })

                if (firstRun) {
                    if (this.hasViewDocument) {
                        result += "<th class='text-center' style='vertical-align: middle' rowspan='" + maxRow + "'><a href='#' class='btnViewDocument' data-id='${DocumentCopyId}' data-title='${Compendium}'>Xem</a></th>";
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
                    that._renderData();
                }
            });
        },

        _renderData: function () {
            var documentListElement = this.$('tbody'),
                  that = this,
                  idx = DEFAULT_PAGESIZE * (this.currentPage - 1);

            documentListElement.empty();

            var documents = this.data;

            if (!documents || documents.length <= 0) {
                var colspan = this.displaySettings.length;
                documentListElement
                    .append("<tr><td colspan=" + colspan + ">" + egov.resources.documents.noDocumentCopyItem + "</td></tr>");
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
                document.Stt = ++idx;
                documentListElement.append($.tmpl(that.documentTemplate, document));
            });

            that.fixHeader();
            hideProgress();
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

            container.find(".pagination li[page='" + this.currentPage + "']").addClass("active");
        },

        _showTotal: function () {
            if (this.isShowTotal == "false") {
                $("#showTotalDocument").hide();
                return;
            }
            $("#showTotalDocument").show();
            this.$("#total").text(this.total);
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
                PageSize: DEFAULT_PAGESIZE
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
            var result = "<tr>";

            // Stt
            result += "<td class='text-center'>${Stt}</td>";

            // Hiển thị các cột theo cấu hình
            $.each(newcolumnSettings, function (index, setting) {
                var justify = setting.justify ? setting.justify : "left"
                result += "<td style='text-align:" + justify + "' data-column='" + setting.columnName + "'>${" + setting.columnName + "}</td>";
            });

            if (this.hasViewDocument) {
                result += "<td><a href='#' class='btnViewDocument' data-id='${DocumentCopyId}' data-title='${Compendium}'>Xem</a></td>";
            } else {
                result += "<td></td>";
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
            if (target.parent().is(".active")) {
                return;
            }

            target.parent().siblings().removeClass("active");
            target.parent().addClass("active");

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
            var groupBy = "";
            if (this.ColumnGroupName && this.ColumnGroupName != "default") {
                groupBy = this.ColumnGroupName;
            }
            var target = $(e.target).closest("a");
            var exporttype = target.attr("export-type");
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

        _formatDateAndShowDatepicker: function (type) {
            var from, to;
            $(".statistics-time select").addClass("hidden");
            switch (type) {
                case "TrongNgay": // trong ngày
                    from = new Date().startOf("day");
                    to = new Date().endOf("day");
                    break;
                case "TrongTuan": // trong tuần
                    from = new Date().startOf("week");
                    to = new Date().endOf("week");
                    break;
                case "TrongThang": // Trong tháng
                    $(".statistics-time #monthSelect").removeClass("hidden");
                    var currentMonth = $(".statistics-time #monthSelect").val();
                    from = new Date().month(currentMonth).startOf("month");
                    to = new Date().month(currentMonth).endOf("month");
                    break;
                case "TrongQuy": // Trong quý
                    var quarter = $(".statistics-time #quarterSelect").val();
                    var firstMonthInQuarter = (quarter - 1) * 3 + 1;
                    from = new Date().date(1).month(firstMonthInQuarter).startOf("quarter");
                    to = new Date().date(1).month(firstMonthInQuarter).endOf("quarter");
                    $(".statistics-time #quarterSelect").removeClass("hidden");
                    break;
                case "TrongNam": // Trong năm
                    $(".statistics-time #yearSelect").removeClass("hidden");
                    var currentYear = $(".statistics-time #yearSelect").val();
                    from = new Date().year(currentYear).startOf("year");
                    to = new Date().year(currentYear).endOf("year");
                    break;
                case "TuyChon":
                default:
                    from = to = new Date();
                    break;
            }

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
            this._formatDateAndShowDatepicker(this.type);
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
            var justify = this.model.get('justify')? this.model.get('justify') : "left";

            this.$el.attr("style", "text-align:" + justify);
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

    window.eReport = new eReport;

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

        $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);

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
    }

    function bindYearSelect() {
        var currentYear = new Date().year();
        var yearSelect = $("#yearSelect");
        var from = currentYear - 4;
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
        $("#yearSelect option[value='" + currentYear + "']").attr("selected", "selected");
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
})();