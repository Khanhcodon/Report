(function (eG) {
    eG.initialize = function () {
        var that = this;

        that.isLoading = false;
        that.$el = $(".overview");
        that.$status = $(".alert");

        that.otherOverduesElement = $("#docOtherOverdue tbody");
        that.otherOverdueTemp = $("#otherOverduesTemp");

        that.currentOverduesElement = $("#docOverdue tbody");
        that.currentOverdueTemp = $("#overduesTemp");

        that.followDocumentElement = $("#followDocument tbody");
        that.followDocumentTemp = $("#followTemp");

        eG.GetStatistics();
    };

    eG.GetStatistics = function () {
        var that = this;
        var data = that.getReportCondition();
        if (that.isLoading) {
            return;
        }

        $.ajax({
            url: "/Statistics/GetPrivateStatictis",
            data: data,
            beforeSend: function () {
                that.isLoading = true;
                that.$status.fadeIn("fast");
            },
            success: function (result) {
                that.isLoading = false;

                that.$el.find(".overdue-total").text(result.currentOverdues == undefined ? 0 : result.currentOverdues.length);
                that.$el.find(".otherOverdue-total").text(result.otherOverdues == undefined ? 0 : result.otherOverdues.length);
                that.$el.find(".follow-total").text(result.followDocuments == undefined ? 0 : result.followDocuments.length);

                that.currentOverdues = that._parseDocColor(result.currentOverdues);
                that.otherOverdues = that._parseDocColor(result.otherOverdues);
                that.followDocuments = that._parseDocColor(result.followDocuments);

                that.render();
                that.$status.fadeOut("fast");
            },
            complete: function () {
                that.$status.fadeOut("fast");
                that.isLoading = false;
            }
        });
    },

    eG.render = function () {
        var that = this;

        eG._showOtherOverdueFilterAndGroup();

        eG._showOverdueByFilterAndGroup();

        eG._bindEvents();
    },

    eG._bindEvents = function () {
        var $el = this.$el;
        var that = this;
        that.type = "3"; // mặc định hiển thị tháng hiện tại

        $el.find("#btnReport").click(function () {
            that.GetStatistics();
        });

        $el.find("#from, #to").change(function () {
            that.GetStatistics();
        });

        $el.find("#dateConditions li a").click(function () {
            var type = $(this).attr("type");
            that.type = type;
            formatDateAndShowDatepicker(type);

            $("#dateConditionCurrent").text($(this).text());
        });

        $(".statistics-time select").change(function () {
            formatDateAndShowDatepicker(that.type);
        });

        $el.find("#btnExportExcel, #btnExportWord").click(function () {
            var isExcel = $(this).is("#btnExportExcel");
            alert("export");
        });

        $el.find(".overview-filters select").change(function () {
            eG._showOverdueByFilterAndGroup();
        });

        $el.find(".btn-viewDocument").click(function (e) {
            var element = $(e.target).closest("a");
            var id = element.attr("value");
            var group = element.attr("data-group")
            var docs = group == "follow" ? that.followDocuments : that.currentOverdues;

            var doc = _.find(docs, function (item) {
                return item.DocumentCopyId;
            });

            if (doc != undefined && window.parent != undefined && typeof window.parent.openDocument == "function") {
                window.parent.openDocument(id, doc.Compendium);
            }
        });

        $el.find("#filterFollow").change(function () {
            that._filterFollow();
        });
    }

    eG.getReportCondition = function () {
        return {
            from: $("#from").datepicker("getDate").startOf("day").format("yyyy-MM-ddThh:mm:ss"),
            to: $("#to").datepicker("getDate").endOf("day").format("yyyy-MM-ddThh:mm:ss")
        };
    },

    eG._showOverdueByFilterAndGroup = function () {
        var docTypeFilterValue, deptFilterValue, userFilterValue, groupBy, that,
            groupByColumn, sortByColumn, overdues;

        that = this;

        // Cảnh báo người dùng hiện tại
        that.currentOverduesElement.html($.tmpl(that.currentOverdueTemp, that.currentOverdues));

        // Cảnh báo theo dõi
        that.followDocumentElement.html($.tmpl(that.followDocumentTemp, that.followDocuments));
        that._filterFollow();

        // Cảnh báo nhân viên
        docTypeFilterValue = that.filterDoctype.val();
        deptFilterValue = that.filterDept.val();
        userFilterValue = that.filterUser.val();

        groupBy = that.groupBy.val();

        overdues = _.filter(that.otherOverdues, function (doc) {
            var result = true;
            if (docTypeFilterValue != 0) {
                result &= doc.DoctypeId == docTypeFilterValue;
            }

            if (deptFilterValue != 0) {
                result &= doc.CurrentDepartmentExt == deptFilterValue;
            }

            if (userFilterValue != 0) {
                result &= doc.CurrentUserId == userFilterValue;
            }

            return result;
        });

        groupByColumn = groupBy == 1 ? "DoctypeName" : (groupBy == 2 ? "CurrentDepartmentExt" : "CurrentUser");

        overdues = _.groupBy(_.sortBy(overdues, groupByColumn), groupByColumn);

        that.otherOverduesElement.empty();

        for (var group in overdues) {
            that.otherOverduesElement.append("<tr class='overdue-group'><td colspan='7'>" + group + " - " + overdues[group].length + " Văn bản (Hồ sơ)</td></tr>");

            var data = _.groupBy(overdues[group], "CurrentUser");
            that.otherOverduesElement.append($.tmpl(that.otherOverdueTemp, data));
        }

        if (groupBy == 1) {
            $(".doctype-col").hide();
            $(".dept-col").show();
        } else {
            $(".doctype-col").show();
            $(".dept-col").hide();
        }
    },

    eG._showOtherOverdueFilterAndGroup = function () {
        var that = this;

        that.filterDoctype = $("#ddlFilterDocTypes");
        that.filterDept = $("#ddlFilterDepartments");
        that.filterUser = $("#ddlFilterUsers");

        that.groupBy = $("#ddlGroupBy");

        $(".overview-filters select option.newitem").remove();

        if (that.otherOverdues == undefined || that.otherOverdues.length == 0) {
            $(".overview-filters").hide();
        } else {
            $(".overview-filters").show();
        }

        that.users = _.map(that.otherOverdues, function (i) {
            var obj = {
                CurrentUserId: i.CurrentUserId,
                CurrentUser: i.CurrentUser
            };
            return obj;
        });
        that.users = _.uniq(that.users, function (item) {
            return item.CurrentUserId;
        });

        that.depts = _.map(that.otherOverdues, function (i) {
            var obj = {
                CurrentDepartmentId: i.CurrentDepartmentExt,
                CurrentDepartmentExt: i.CurrentDepartmentExt
            };
            return obj;
        });
        that.depts = _.uniq(that.depts, function (item) {
            return item.CurrentDepartmentId;
        });

        that.doctypes = _.map(that.otherOverdues, function (i) {
            var obj = {
                DoctypeId: i.DoctypeId,
                DoctypeName: i.DoctypeName
            };
            return obj;
        });
        that.doctypes = _.uniq(that.doctypes, function (item) {
            return item.DoctypeId;
        });

        that.filterDoctype.append($.tmpl("<option value='${DoctypeId}' class='newitem'>${DoctypeName}</option>", that.doctypes));
        that.filterDept.append($.tmpl("<option value='${CurrentDepartmentId}' class='newitem'>${CurrentDepartmentExt}</option>", that.depts));
        that.filterUser.append($.tmpl("<option value='${CurrentUserId}' class='newitem'>${CurrentUser}</option>", that.users));
    },

    eG._parseDocColor = function (docs) {
        var overdueDay = eG.overdueDay || 0; // hạn tổng
        var overdueCurrentDay = eG.overdueCurrentDay || 0;  // hạn giữ

        _.each(docs, function (doc) {
            var colorTerm = "doc-";
            //CurrentNodeKeepTime = currentNodeKeepTime,
            //CurrentNodePermitTime = currentNode.TimeInNode / 24,// Do trong db luôn lưu dạng giờ nên / 24
            //TotalKeepTime = totalKeepTime,
            //TotalPermitTime = document.ExpireProcess ?? 0,
            //Deadline = document.ExpireProcess.HasValue ? document.ExpireProcess.Value - totalKeepTime : 0,
            //DeadlineCurrent = (currentNode.TimeInNode / 24) - currentNodeKeepTime,
            //CurrentDepartmentExt = currentDepartment
            var deadline = doc.Deadline;
            var deadlineCurrent = doc.DeadlineCurrent;

            if (deadlineCurrent > overdueCurrentDay) {
                doc.color = "doc-overdue";
            } else if (deadline > overdueDay) {
                doc.coloe = "doc-overdueTotal"
            }
        });

        return docs;
    },

    eG._filterFollow = function () {

        var type = $("#filterFollow").val();
        if (type == "0") {
            $("#followDocument tr").show();
        } else if (type == "1") {
            $("#followDocument tr td.follow-dateappointed").each(function () {
                if ($(this).text() == "") {
                    $(this).parents("tr").hide();
                } else {
                    $(this).parents("tr").show();
                }
            });
        } else {
            $("#followDocument tr td.follow-dateappointed").each(function () {
                if ($(this).text() == "") {
                    $(this).parents("tr").show();
                } else {
                    $(this).parents("tr").hide();
                }
            });
        }
    }
})
(this.eGovOverview = this.eGovOverview || {});

(function () {
    bindDatepicker();
    bindYearSelect();
    bindDefaultSelect();

    formatDateAndShowDatepicker("2");
    eGovOverview.initialize();
})
();

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

function formatDateAndShowDatepicker(type) {
    var from, to;
    $(".statistics-time select").addClass("hidden");
    switch (type) {
        case "1": // trong ngày
            from = new Date().startOf("day");
            to = new Date().endOf("day");
            break;
        case "2": // trong tuần
            from = new Date().startOf("week");
            to = new Date().endOf("week");
            break;
        case "3": // Trong tháng
            $(".statistics-time #monthSelect").removeClass("hidden");
            var currentMonth = $(".statistics-time #monthSelect").val();
            from = new Date().month(currentMonth).startOf("month");
            to = new Date().month(currentMonth).endOf("month");
            break;
        case "4": // Trong quý
            var quarter = $(".statistics-time #quarterSelect").val();
            var firstMonthInQuarter = (quarter - 1) * 3 + 1;
            from = new Date().month(firstMonthInQuarter).startOf("quarter");
            to = new Date().month(firstMonthInQuarter).endOf("quarter");
            $(".statistics-time #quarterSelect").removeClass("hidden");
            break;
        case "5": // Trong năm
            $(".statistics-time #yearSelect").removeClass("hidden");
            var currentYear = $(".statistics-time #yearSelect").val();
            from = new Date().year(currentYear).startOf("year");
            to = new Date().year(currentYear).endOf("year");
            break;
        case "6":
        default:
            from = to = new Date();
            break;
    }

    from = from.format("dd/MM/yyyy");
    to = to.format("dd/MM/yyyy");

    $("#from").val(from);
    $("#to").val(to);
    $("#from, #to").trigger("change");
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

function parseDate(date, format) {
    if (date instanceof Date) {
        return (new Date(date)).format(format);
    }
    return "";
}